using AECLOGIC.HMS.BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AECLOGIC.ERP.HMS
{
    public partial class MackAttan : AECLOGIC.ERP.COMMON.WebFormMaster
    {
        protected override void OnInit(EventArgs e)
        {
            ModuleID = 1;
            base.OnInit(e);
           
            
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            try { 

            if (!IsPostBack)
            {
                BindWorkSite();
                BindDepartments();
            }
            
            }
            catch { }
        }
        public void BindWorkSite()
        {
             
          DataSet  ds = AttendanceDAC.GetWorkSite(0, '1', Convert.ToInt32(Session["CompanyID"]));
            ddlworksites.DataSource = ds.Tables[0];
            ddlworksites.DataTextField = "Site_Name";
            ddlworksites.DataValueField = "Site_ID";
            ddlworksites.DataBind();
            ddlworksites.Items.Insert(0, new ListItem("---ALL---", "0", true));
        }

        public void BindDepartments()
        {
             
            AttendanceDAC objEmployee = new AttendanceDAC();
            DataSet ds = objEmployee.GetDepartments(0);
            ddldepartments.DataSource = ds.Tables[0];
            ddldepartments.DataTextField = "DeptName";
            ddldepartments.DataValueField = "DepartmentUId";
            ddldepartments.DataBind();
            ddldepartments.Items.Insert(0, new ListItem("---ALL---", "0", true));
            ds = null;
            objEmployee = null;
        }
        int mid = 0;
        bool viewall;
        string menuname;
        string menuid;
      

        public void EmployeBind()
        {

            try
            {

                HRCommon objHrCommon = new HRCommon();
                objHrCommon.PageSize = 10;
                objHrCommon.CurrentPage = 1;
                int? SiteID = null;
                int? DeptID = null;

                if (ddlworksites.SelectedItem.Value != "0")
                {
                    SiteID = Convert.ToInt32(ddlworksites.SelectedItem.Value);
                }
                if (ddldepartments.SelectedItem.Value != "0")
                {
                    DeptID = Convert.ToInt32(ddldepartments.SelectedItem.Value);
                }
                objHrCommon.OldEmpID = null;
                if (txtOldEmpID.Text != "")
                    objHrCommon.OldEmpID = txtOldEmpID.Text;

                int? EmpID = null;
                if (txtEmpID.Text != "")
                    EmpID = Convert.ToInt32(txtEmpID.Text);
                DataSet ds = ExceReports.ExceRptReportingtoByPaging(objHrCommon, SiteID, DeptID, txtusername.Text, EmpID, Convert.ToInt32(Session["CompanyID"]));
                lblEMPName.Text = ds.Tables[0].Rows[0]["name"].ToString();
                hfID.Value = ds.Tables[0].Rows[0]["EmpId"].ToString();
                ds = null;
                 objHrCommon =null;
            }
            catch (Exception e)
            {
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            EmployeBind();
        }
    }
}