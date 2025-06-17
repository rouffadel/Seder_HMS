using System;
using System.Data;
using System.Configuration;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using AECLOGIC.HMS.BLL;
namespace AECLOGIC.ERP.HMS
{
    public partial class AttendanceDefault : AECLOGIC.ERP.COMMON.WebFormMaster
    {
        private static string _Companyname = ConfigurationSettings.AppSettings["Company"].ToString();
        AttendanceDAC objAtt = new AttendanceDAC();
        int mid = 0;
        bool viewall;
        string menuname;
        string menuid;
        protected override void OnInit(EventArgs e)
        {
            ModuleID = 1;
            base.OnInit(e);
        }

        private void DataGridOperationsBind()
        {
            //DataSet ds = new DataSet();
            DataSet ds = (DataSet)objAtt.GetOperations();
            if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0)
            {
                gdvOperations.DataSource = ds;
                gdvOperations.DataBind();
            }
            else
                gdvOperations.Visible = false;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                string id =  Convert.ToInt32(Session["UserId"]).ToString();
            }
            catch
            {
                Response.Redirect("Home.aspx");
            }
            topmenu.MenuId = GetParentMenuId();
            topmenu.ModuleId = ModuleID;;
            topmenu.RoleID = Convert.ToInt32(Session["RoleId"].ToString());
            topmenu.SelectedMenu = Convert.ToInt32(mid.ToString());
            topmenu.DataBind();
            //Session["menuname"] = menuname;
            //Session["menuid"] = menuid;
            if (!IsPostBack)
            {

                DataGridOperationsBind();
                if ( Convert.ToInt32(Session["UserId"]).ToString() != "83")
                {
                    gdvOperations.Columns[2].Visible = false;
                    gdvOperations.Columns[3].Visible = false;
                }
                lblcompany.Text = _Companyname;
            }
        }
        public int GetParentMenuId()
        {
            string URL = Request.Url.Segments[Request.Url.Segments.Length - 1] + Request.Url.Query;
            int RoleId = Convert.ToInt32(Session["RoleId"].ToString());
            int ModuleId = ModuleID;;

           // DataSet ds = new DataSet();

            DataSet ds = AttendanceDAC.GetAllowed(RoleId, ModuleId, URL);
            int MenuId = 0;
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                MenuId = Convert.ToInt32(ds.Tables[0].Rows[0]["Under"]);
                gdvOperations.Columns[2].Visible = Convert.ToBoolean(ds.Tables[0].Rows[0]["Editable"].ToString());
                gdvOperations.Columns[3].Visible = Convert.ToBoolean(ds.Tables[0].Rows[0]["Editable"].ToString());
                //ViewState["Editable"] = (bool)ds.Tables[0].Rows[0]["Editable"];
                //ViewState["ViewAll"] = (bool)ds.Tables[0].Rows[0]["ViewAll"];
                //viewall = (bool)ViewState["ViewAll"];
                //menuname = ds.Tables[0].Rows[0]["menuname"].ToString();
                //menuid = MenuId.ToString();
                //mid = Convert.ToInt32(ds.Tables[0].Rows[0]["MenuId"].ToString());
            }
            return MenuId;
        }
        protected void gdvOperations_RowEditing(object sender, GridViewEditEventArgs e)
        {
            try
            {
                gdvOperations.EditIndex = e.NewEditIndex;
                DataGridOperationsBind();
                dvOperations.Visible = false;
            }
            catch
            {
            }
        }
        protected void gdvOperations_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                GridViewRow row = (GridViewRow)gdvOperations.Rows[e.RowIndex];

                Label lblOperation = (Label)row.FindControl("lblOperation");
                int ID = int.Parse(gdvOperations.DataKeys[e.RowIndex][0].ToString());
                objAtt.InsUpdateDeleteOperations(ID, lblOperation.Text, 1, 3);
                DataGridOperationsBind();
                dvOperations.Visible = false;
            }
            catch
            {
            }

        }
        protected void gdvOperations_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                GridViewRow row = (GridViewRow)gdvOperations.Rows[e.RowIndex];

                TextBox lblOperation = (TextBox)row.FindControl("txtOperation");

                int ID = int.Parse(gdvOperations.DataKeys[e.RowIndex][0].ToString());
                objAtt.InsUpdateDeleteOperations(ID, lblOperation.Text, 1, 2);
                gdvOperations.EditIndex = -1;
                DataGridOperationsBind();
                dvOperations.Visible = false;
            }
            catch
            {
            }
        }
        protected void gdvOperations_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Add")
                {
                    string RecordIDText = e.CommandArgument.ToString().Replace("&nbsp;", ""); //thisGrid.Items[e.Item.ItemIndex].Cells[1].Text.Replace("&nbsp;", "");
                    GridViewRow gvr = (GridViewRow)(((Control)e.CommandSource).NamingContainer);
                    //int index = gvr.RowIndex;
                    TextBox lblOperation = (TextBox)gvr.FindControl("txtOperation");
                    ViewState["RecordIDText"] = RecordIDText;
                    dvOperations.Visible = true;
                }
            }
            catch
            {
            }
        }
        protected void gdvOperations_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            try
            {
                gdvOperations.EditIndex = -1;
                DataGridOperationsBind();
                dvOperations.Visible = false;
            }
            catch
            {
            }
        }
        protected void btnAdd_OnClick(object sender, EventArgs e)
        {
            try
            {
                objAtt.InsUpdateDeleteOperations(Convert.ToInt32(ViewState["RecordIDText"]), txtOperations.Text, 1, 1);
                DataGridOperationsBind();
                dvOperations.Visible = false;
            }
            catch
            {
            }
        }
    }
}