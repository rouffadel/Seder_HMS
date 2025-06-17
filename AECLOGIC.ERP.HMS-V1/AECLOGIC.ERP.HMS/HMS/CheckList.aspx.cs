using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using AECLOGIC.HMS.BLL;
namespace AECLOGIC.ERP.HMS
{
    public partial class CheckList : AECLOGIC.ERP.COMMON.WebFormMaster
    {
        AttendanceDAC objTodoList = new AttendanceDAC();
        HRCommon objHrCommon = new HRCommon();
         
        protected override void OnInit(EventArgs e)
        {
            ModuleID = 1;
            base.OnInit(e);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Convert.ToInt32(Request.QueryString["key"]) == 2)
            {
                mainview.ActiveViewIndex = 1;
                BindTodoList();
            }
            else if (Convert.ToInt32(Request.QueryString["key"]) == 3)
            {

                txtAuthority.Text = string.Empty;
                txtListItem.Text = string.Empty                  ;
                btnsubmit.Text = "Submit";
                mainview.ActiveViewIndex = 0;
            }
            else
            {
                mainview.ActiveViewIndex = 2;
            }

            if (!IsPostBack)
            {
                GetParentMenuId();
                GetWorkSites(0);
                GetRoles();
                 
            }
            btnsubmit.Attributes.Add("onclick", "javascript:return validatesave();");
        }
        public int GetParentMenuId()
        {
            string URL = Request.Url.Segments[Request.Url.Segments.Length - 1] + Request.Url.Query;
            int RoleId = Convert.ToInt32(Session["RoleId"].ToString());
            int ModuleId = ModuleID;;

            

            DataSet ds = AttendanceDAC.GetAllowed(RoleId, ModuleId, URL);
            int MenuId = 0;
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                MenuId = Convert.ToInt32(ds.Tables[0].Rows[0]["Under"]);
                gvToddoList.Columns[2].Visible = Convert.ToBoolean(ds.Tables[0].Rows[0]["Editable"].ToString());
                gvToddoList.Columns[3].Visible = Convert.ToBoolean(ds.Tables[0].Rows[0]["Editable"].ToString());
               
                btnCheckSave.Visible = (bool)ds.Tables[0].Rows[0]["Editable"];
                btnsubmit.Visible = (bool)ds.Tables[0].Rows[0]["Editable"];
                lnknew.Visible = (bool)ds.Tables[0].Rows[0]["Editable"];
            }

            return MenuId;
        }
        private void GetWorkSites(int SiteID)
        {
            DataSet ds = AttendanceDAC.GetWorkSite(SiteID, '1', Convert.ToInt32(Session["CompanyID"]));
            ddlWS.DataSource = ds.Tables[0];
            ddlWS.DataTextField = "Site_Name";
            ddlWS.DataValueField = "Site_ID";
            ddlWS.DataBind();
            ddlWS.Items.Insert(0, new ListItem("--Select--", "0"));

        }

        public void BindTodoList()
        {
            mainview.ActiveViewIndex = 1;

          DataSet  ds = objTodoList.GetTodoList();
            if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0)
            {
                gvToddoList.DataSource = ds;
            }
            gvToddoList.DataBind();

        }
        protected void btnsubmit_Click(object sender, EventArgs e)
        {
            if (ViewState["TodoListID"] != null && ViewState["TodoListID"].ToString() != "0")
            {
                objHrCommon.ListID = Convert.ToInt32(ViewState["TodoListID"].ToString());
            }
            objHrCommon.ListName = txtListItem.Text.Trim();
            objHrCommon.Authority = txtAuthority.Text.Trim();
            objHrCommon.EmpID = Convert.ToInt32( Convert.ToInt32(Session["UserId"]).ToString());

            int i = objTodoList.InsUpdTodoListMasetr(objHrCommon);
            if (i == 1)
            {
                AlertMsg.MsgBox(Page, "TodoList Assigned Successfully");
            }
            else
            {
                AlertMsg.MsgBox(Page, "TodoList Edit Successfully");
            }
            BindTodoList();
        }
        public void BindTodoListDetails(int TodoListID)
        {
            DataSet ds = objTodoList.GetTodoListDetails(TodoListID);
            if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0)
            {
                txtListItem.Text = ds.Tables[0].Rows[0]["ListItem"].ToString();
                txtAuthority.Text = ds.Tables[0].Rows[0]["Authority"].ToString();

            }

        }
        protected void gvToddoList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Edt")
            {

              
                if (Convert.ToBoolean(ViewState["Editable"]) == true)
                {

                    int TodoListID = Convert.ToInt32(e.CommandArgument);
                    ViewState["TodoListID"] = TodoListID;
                    BindTodoListDetails(TodoListID);
                    btnsubmit.Text = "Update";
                    mainview.ActiveViewIndex = 0;
                }
                else
                {
                    AlertMsg.MsgBox(Page, "You have no Rights to Edit! ");
                }

            }
            if (e.CommandName == "Del")
            {
                
                if (Convert.ToBoolean(ViewState["Editable"]) == true)
                {

                    int TodoListID = Convert.ToInt32(e.CommandArgument);

                    objTodoList.DeleteTodoList(TodoListID);
                    AlertMsg.MsgBox(Page, "Deleted Successfully");

                    BindTodoList();
                }
                else
                {
                    AlertMsg.MsgBox(Page, "You have no rights to delete! ");
                }
            }
        }
        protected void ddlWS_SelectedIndexChanged(object sender, EventArgs e)
        {
            dvchecklist.Visible = true;
            mainview.ActiveViewIndex = 2;
            int SiteID = Convert.ToInt32(ddlWS.SelectedItem.Value);
            DataSet ds2 = objTodoList.GetCheckList(SiteID);
            if (ds2 != null && ds2.Tables.Count != 0 && ds2.Tables[0].Rows.Count > 0)
            {
                gvCheckList.DataSource = ds2;
            }
            gvCheckList.DataBind();
        }

        public static DataSet dsStatus = new DataSet();
        public static ArrayList alStatus = new ArrayList();
        public int GetStatusIndex(string Value)
        {
            return alStatus.IndexOf(Value);
        }

        public DataSet GetRoles()
        {
            alStatus = new ArrayList();
            dsStatus = objTodoList.GetStatusList();
            foreach (DataRow dr in dsStatus.Tables[0].Rows)
                alStatus.Add(dr["StatusID"].ToString());
            return dsStatus;
        }
        public DataSet BindStatus()
        {

            return dsStatus;
        }
        protected void btnCheckSave_Click(object sender, EventArgs e)
        {
            try
            {

                foreach (GridViewRow gvr in gvCheckList.Rows)
                {

                    Label lblCheckListID = (Label)gvr.Cells[0].FindControl("lblCheckListID");
                    Label lblListItem = (Label)gvr.Cells[1].FindControl("lblListItem");
                    TextBox txtAuthority = (TextBox)gvr.Cells[2].FindControl("txtAuthority");
                    TextBox txtStartDate = (TextBox)gvr.Cells[3].FindControl("txtStartDate");
                    TextBox txtFinishDate = (TextBox)gvr.Cells[4].FindControl("txtFinishDate");
                    DropDownList ddlStatus = (DropDownList)gvr.Cells[5].FindControl("ddlStatus");
                    Label lblTodoListID = (Label)gvr.Cells[6].FindControl("lblTodoListID");

                    if (lblCheckListID.Text != string.Empty && lblCheckListID.Text != string.Empty                  )
                    {
                        objHrCommon.ChkListID = Convert.ToInt32(lblCheckListID.Text);
                    }
                    else
                    {
                        objHrCommon.ChkListID = 0;
                    }
                    objHrCommon.ListName = lblListItem.Text;
                    objHrCommon.Authority = txtAuthority.Text;
                    if (txtStartDate.Text != string.Empty && txtStartDate.Text != string.Empty                  )
                    {
                        objHrCommon.FromDate = CODEUtility.ConvertToDate(txtStartDate.Text.Trim(), DateFormat.DayMonthYear);
                    }
                    if (txtFinishDate.Text != string.Empty && txtFinishDate.Text != string.Empty                  )
                    {
                        objHrCommon.ToDate = CODEUtility.ConvertToDate(txtFinishDate.Text.Trim(), DateFormat.DayMonthYear);
                    }

                    objHrCommon.StatusID = Convert.ToInt32(ddlStatus.SelectedItem.Value);
                    objHrCommon.SiteID = Convert.ToInt32(ddlWS.SelectedItem.Value);
                    objHrCommon.EmpID = Convert.ToInt32( Convert.ToInt32(Session["UserId"]).ToString());
                    objHrCommon.ListID = Convert.ToInt32(lblTodoListID.Text);
                    if (objHrCommon.StatusID != 0)
                    {
                        objTodoList.InsUpdCheckList(objHrCommon);
                    }

                }
                AlertMsg.MsgBox(Page, "Succeesfully Updated");

            }
            catch (Exception ex)
            {

                AlertMsg.MsgBox(Page, "Sorry for the inconvinience. Try again.\nError:" + ex.Message.ToString(),AlertMsg.MessageType.Error);
            }
        }
        protected void lnkNewheck_Click(object sender, EventArgs e)
        {
            txtAuthority.Text = string.Empty                  ;
            txtListItem.Text = string.Empty                  ;
            btnsubmit.Text = "Submit";
            mainview.ActiveViewIndex = 0;

        }
        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            mainview.ActiveViewIndex = 1;
            BindTodoList();
        }
        protected void lnknew_Click(object sender, EventArgs e)
        {
            txtAuthority.Text = string.Empty                  ;
            txtListItem.Text = string.Empty                  ;
            btnsubmit.Text = "Submit";
            mainview.ActiveViewIndex = 0;
        }
    }
}