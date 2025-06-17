using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AECLOGIC.HMS.BLL;
using System.Data;

namespace AECLOGIC.ERP.HMS
{
    public partial class PTReports : AECLOGIC.ERP.COMMON.WebFormMaster
    {
        #region Declaration
        int mid = 0;
        bool viewall;
        string menuname;
        string menuid;
        AttendanceDAC objRights = new AttendanceDAC();
        #endregion Declaration
        protected override void OnInit(EventArgs e)
        {
            ModuleID = 1;
            base.OnInit(e);
        }

        #region Page Load
        protected void Page_Load(object sender, EventArgs e)
        {
           
            if (!IsPostBack)
            {
                txtFrom.Text = DateTime.Now.AddDays(-(DateTime.Now.Day) + 1).ToString("dd/MM/yyyy");
                txtTo.Text = DateTime.Now.AddDays((DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month) - (DateTime.Now.Day))).ToString("dd/MM/yyyy");
                BindWS();
                BindDepartments();
                ShowReport();

            }
        }
        #endregion Page Load

        #region Supporting Methods

   
        public void BindWS()
        {
             
           DataSet ds = AttendanceDAC.GetWorkSite(0, '1', Convert.ToInt32(Session["CompanyID"]));
            ddlWS.DataSource = ds.Tables[0];
            ddlWS.DataTextField = "Site_Name";
            ddlWS.DataValueField = "Site_ID";
            ddlWS.DataBind();
            ddlWS.Items.Insert(0, new ListItem("---ALL---", "0"));

        }
        void ShowReport()
        {
            int? WSID = null;
            DateTime? Fromdate = null;
            DateTime? Todate = null;
            if (txtFrom.Text != "")
                Fromdate = CODEUtility.ConvertToDate(txtFrom.Text.Trim(), DateFormat.DayMonthYear);
            if (txtTo.Text != "")
                Todate = CODEUtility.ConvertToDate(txtTo.Text.Trim(), DateFormat.DayMonthYear);
            if (ddlWS.SelectedIndex != 0)
                WSID = Convert.ToInt32(ddlWS.SelectedValue);
            int? DeptID = null;
            if (ddlDept.SelectedValue != "0")
                DeptID = Convert.ToInt32(ddlDept.SelectedValue);
            int? EmpID = null;
            if (txtEmpID.Text != "")
                EmpID = Convert.ToInt32(txtEmpID.Text);
            string EmpName = txtEmpName.Text;
            RaiseRDLCReport(".\\RDLC_Reports\\PT.rdlc", AttendanceDAC.PTDataset(Fromdate, Todate, WSID, DeptID, EmpID, EmpName, Convert.ToInt32(Session["CompanyID"])), "PT Report");


        }
        void RaiseRDLCReport(string RpNamePath, DataSet rpds, string RpName)
        {
            Microsoft.Reporting.WebForms.ReportDataSource rptDataSource = null;
            PTRV.LocalReport.ReportPath = RpNamePath;
            PTRV.LocalReport.DisplayName = RpName;
            this.Title = "Report-[" + RpName + "]";
            this.PTRV.LocalReport.DataSources.Clear();
            rptDataSource = new Microsoft.Reporting.WebForms.ReportDataSource("PT", rpds.Tables[0]);
            this.PTRV.LocalReport.DataSources.Add(rptDataSource);
            rptDataSource = new Microsoft.Reporting.WebForms.ReportDataSource("PTRange", rpds.Tables[1]);
            this.PTRV.LocalReport.DataSources.Add(rptDataSource);


            this.PTRV.LocalReport.Refresh();
        }
        public void BindDepartments()
        {
            DataSet ds = objRights.GetDaprtmentList();

            DataTable dt = ds.Tables[0];

            foreach (DataRow dr in dt.Rows)
            {
                ddlDept.Items.Add(new ListItem(dr["DeptName"].ToString(), dr["DepartmentUId"].ToString()));
            }
            ddlDept.Items.Insert(0, new ListItem("---ALL---", "0"));
        }

        #endregion Supporting Methods

        #region Events
        protected void btnShow_Click(object sender, EventArgs e)
        {
            ShowReport();
        }

        #endregion Events

    }
}