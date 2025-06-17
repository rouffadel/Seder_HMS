using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using AECLOGIC.HMS.BLL;

namespace AECLOGIC.ERP.HMS
{
    public partial class EmpSimBillReport : AECLOGIC.ERP.COMMON.WebFormMaster
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

        #region PageLoad
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!IsPostBack)
            {
                BindYears();
                BindWorkSites();
                BindDepartments();
                ShowReport();
            }
        }
        #endregion PageLoad

        #region SupportingMethods
        public void BindYears()
        {

            DataSet ds = AttendanceDAC.GetCalenderYear();

            int i = 0;
            int Maxyear = Convert.ToInt32(ds.Tables[0].Rows[0]["MaxYear"].ToString());
            for (int Minyear = Convert.ToInt32(ds.Tables[0].Rows[0]["MinYear"].ToString()); Minyear <= Maxyear; Minyear++)
            {
                ddlFrmYear.Items.Insert(i, new ListItem(Convert.ToString(Minyear), Convert.ToString(Minyear)));
                ddlToYear.Items.Insert(i, new ListItem(Convert.ToString(Minyear), Convert.ToString(Minyear)));
                i = i + 1;
            }
            ddlFrmMnth.SelectedValue = ds.Tables[0].Rows[0]["PreviousMonth"].ToString();
            ddlFrmYear.SelectedValue = ds.Tables[0].Rows[0]["CurrentYear"].ToString();

            ddlToMnth.SelectedValue = ds.Tables[0].Rows[0]["PreviousMonth"].ToString();
            ddlToYear.SelectedValue = ds.Tables[0].Rows[0]["CurrentYear"].ToString();

        }
        public void BindWorkSites()
        {

            try
            {

                DataSet ds = AttendanceDAC.GetWorkSite(0, '1', Convert.ToInt32(Session["CompanyID"]));
                ViewState["WorkSites"] = ds;
                if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ddlWS.DataSource = ds.Tables[0];
                    ddlWS.DataTextField = "Site_Name";
                    ddlWS.DataValueField = "Site_ID";
                    ddlWS.DataBind();
                }
                ddlWS.Items.Insert(0, new ListItem("---ALL---", "0"));

            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public void BindDepartments()
        {
            try
            {

                DataSet ds = (DataSet)objRights.GetDaprtmentList();
                ViewState["Departments"] = ds;
                if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ddlDept.DataValueField = "DepartmentUId";
                    ddlDept.DataTextField = "DeptName";
                    ddlDept.DataSource = ds;
                    ddlDept.DataBind();
                    ddlDept.Items.Insert(0, new ListItem("---ALL---", "0"));
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
     
        void ShowReport()
        {
            int? WSID = null;
            int? DeptID = null;
            int? FrmMnth = null;
            int? ToMnth = null;
            int? EmpID = null;

            if (ddlWS.SelectedIndex != 0)
                WSID = Convert.ToInt32(ddlWS.SelectedValue);
            if (ddlDept.SelectedValue != "0")
                DeptID = Convert.ToInt32(ddlDept.SelectedValue);
            if (ddlFrmMnth.SelectedValue != "0")
                FrmMnth = Convert.ToInt32(ddlFrmMnth.SelectedValue);
            int FrmYear = Convert.ToInt32(ddlFrmYear.SelectedValue);
            if (ddlToMnth.SelectedValue != "0")
                ToMnth = Convert.ToInt32(ddlToMnth.SelectedValue);
            int ToYear = Convert.ToInt32(ddlToYear.SelectedValue);
            if (txtEmpID.Text != "")
                EmpID = Convert.ToInt32(txtEmpID.Text);
            string EmpName = txtEmpName.Text;
            RaiseRDLCReport(".\\RDLC_Reports\\SimBill.rdlc", AttendanceDAC.SimReportDataset(WSID, DeptID, FrmMnth, FrmYear, ToMnth, ToYear, EmpID, EmpName), "EmpSim Report");
        }
        void RaiseRDLCReport(string RpNamePath, DataSet rpds, string RpName)
        {
            Microsoft.Reporting.WebForms.ReportDataSource rptDataSource = null;
            SimBillRV.LocalReport.ReportPath = RpNamePath;
            SimBillRV.LocalReport.DisplayName = RpName;
            this.Title = "Report-[" + RpName + "]";
            rptDataSource = new Microsoft.Reporting.WebForms.ReportDataSource("SimBill", rpds.Tables[0]);
            this.SimBillRV.LocalReport.DataSources.Clear();
            this.SimBillRV.LocalReport.DataSources.Add(rptDataSource);
            this.SimBillRV.LocalReport.Refresh();
        }
        #endregion SupportingMethods

        #region Events
        protected void btnShow_Click(object sender, EventArgs e)
        {
            ShowReport();
        }
        #endregion Events
    }
}