using System;
using System.Data;
using System.Data.SqlClient;
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
    public partial class TestOrgChart : AECLOGIC.ERP.COMMON.WebFormMaster
    {
        AttendanceDAC objAtt = new AttendanceDAC();
        protected override void OnInit(EventArgs e)
        {
            ModuleID = 1;
            base.OnInit(e);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            Ajax.Utility.RegisterTypeForAjax(typeof(AjaxDAL));
            if (!IsPostBack)
            {
                BindDepartments();
                BindWorkSite();

                ClientScript.RegisterStartupScript(typeof(Page), "str", "<script type='text/javascript'>init();</script>");
            }

        }


        protected void btnSearch_Click(object sender, EventArgs e)
        {
            ClientScript.RegisterStartupScript(typeof(System.String), "str", "<script type='text/javascript'>init();</script>");

        }

        private void BindDepartments()
        {
            DataSet dsdept = objAtt.GetDepartments(0);
            ddlDepartment.DataSource = dsdept.Tables[0];
            ddlDepartment.DataTextField = "DeptName";
            ddlDepartment.DataValueField = "DepartmentUId";
            ddlDepartment.DataBind();
            ddlDepartment.Items.Insert(0, new ListItem("---ALL---", "0", true));

        }

        private void BindWorkSite()
        {
            DataSet dsWS = objAtt.GetWorkSiteByEmpID( Convert.ToInt32(Session["UserId"]), Convert.ToInt32(Session["CompanyID"]), Convert.ToInt32(Session["RoleId"]));
            ddlWorksite.DataSource = dsWS.Tables[0];
            ddlWorksite.DataTextField = "Site_Name";
            ddlWorksite.DataValueField = "Site_ID";
            ddlWorksite.DataBind();
            if (dsWS == null || dsWS.Tables.Count == 0 || dsWS.Tables[0].Rows.Count == 0)
            {
                ddlWorksite.Items.Insert(0, new ListItem("---ALL---", "0"));
            }


        }
    }
}