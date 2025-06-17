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
    public partial class Directors : AECLOGIC.ERP.COMMON.WebFormMaster
    {
        AttendanceDAC objDirectors = new AttendanceDAC();
        HRCommon objHrCommon = new HRCommon();
        protected override void OnInit(EventArgs e)
        {
            ModuleID = 1;
            base.OnInit(e);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetParentMenuId();
                BindGrid();
            }
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
                btnSave.Enabled = (bool)ds.Tables[0].Rows[0]["Editable"];
                gveditkbipl.Columns[7].Visible = Convert.ToBoolean(ds.Tables[0].Rows[0]["Editable"].ToString());
                gveditkbipl.Columns[8].Visible = Convert.ToBoolean(ds.Tables[0].Rows[0]["Editable"].ToString());
                gveditkbipl.Columns[9].Visible = Convert.ToBoolean(ds.Tables[0].Rows[0]["Editable"].ToString());
                gveditkbipl.Columns[10].Visible = Convert.ToBoolean(ds.Tables[0].Rows[0]["Editable"].ToString());
                gveditkbipl.Columns[11].Visible = Convert.ToBoolean(ds.Tables[0].Rows[0]["Editable"].ToString());


            }
            return MenuId;
        }
        public void BindGrid()
        {
            DataSet dsdirectors = objDirectors.GetDirectors(Convert.ToInt32(Session["CompanyID"]));
            gvDirector.DataSource = dsdirectors.Tables[0];
            gvDirector.DataMember = dsdirectors.Tables[0].TableName;
            gvDirector.DataTextField = "Name";
            gvDirector.DataValueField = "EmpId";
            DataRow[] drSelRows = dsdirectors.Tables[0].Select("cmd='1'");
            if (dsdirectors.Tables[0].Rows.Count > 0)
                try
                {
                    gvDirector.SelectedValue = drSelRows[0][0].ToString();
                }
                catch { AlertMsg.MsgBox(Page, "The CMD You Mentioned is not in the list! Select from the above list!"); }
            gvDirector.DataBind();
            gveditkbipl.DataSource = AttendanceDAC.GetDirectorsList(Convert.ToInt32(Session["CompnayID"]));
            gveditkbipl.DataBind();
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            objHrCommon.EmpID = Convert.ToInt32(gvDirector.SelectedItem.Value);
            objDirectors.UpdateCMD(objHrCommon);
            AlertMsg.MsgBox(Page, "Selected person is CMD !");
            BindGrid();

        }
        protected void gveditkbipl_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {
                GridViewRow gvr = (GridViewRow)gveditkbipl.Rows[Convert.ToInt32(e.CommandArgument)]; ;
                int Id = Convert.ToInt32(gveditkbipl.DataKeys[gvr.RowIndex].Value);
                Response.Redirect("CreateEmployee.aspx?Id=" + Id);
            }
        }
    }
}