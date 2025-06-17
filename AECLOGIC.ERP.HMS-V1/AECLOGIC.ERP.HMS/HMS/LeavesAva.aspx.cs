using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;
using AECLOGIC.HMS.BLL;


namespace AECLOGIC.ERP.HMS
{
    public partial class LeavesAva : AECLOGIC.ERP.COMMON.WebFormMaster
    {
        AttendanceDAC objatt = new AttendanceDAC();
        protected override void OnInit(EventArgs e)
        {
            ModuleID = 1;
            base.OnInit(e);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
          
            if (!IsPostBack)
            {
                BindWorkSite();
                BindDepartments();
                BindGrid(0, 0);
            }
        }
    
        public void BindDepartments()
        {
             
           DataSet    ds = objatt.GetDaprtmentList();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ddldept.DataTextField = "Deptname";
                ddldept.DataValueField = "departmentUId";
                ddldept.DataSource = ds;
                ddldept.DataBind();
                ddldept.Items.Insert(0, new ListItem("---ALL---", "0"));
            }
        }
        public void BindWorkSite()
        {

            DataSet ds = AttendanceDAC.GetWorkSite(0, '1', Convert.ToInt32(Session["CompanyID"]));
            if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0)
            {
                ddlworksite.DataTextField = "Site_Name";
                ddlworksite.DataValueField = "Site_ID";
                ddlworksite.DataSource = ds;
                ddlworksite.DataBind();
            }
            ddlworksite.Items.Insert(0, new ListItem("---All---", "0"));

        }
        public void BindGrid(int SiteId, int DeptId)
        {

            DataSet ds = Leaves.GetApplicableLeavesDetails(SiteId, DeptId);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                grdsearch.DataSource = ds;
                grdsearch.DataBind();
            }

        }
        protected void btnsearch_Click(object sender, EventArgs e)
        {
            int SiteId = Convert.ToInt32(ddlworksite.SelectedItem.Value);
            int DeptId = Convert.ToInt32(ddldept.SelectedItem.Value);
            BindGrid(SiteId, DeptId);
        }
    }
}
