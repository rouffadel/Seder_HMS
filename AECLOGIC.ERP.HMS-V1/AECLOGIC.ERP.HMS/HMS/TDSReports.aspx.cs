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
    public partial class TDSReports : AECLOGIC.ERP.COMMON.WebFormMaster
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
            topmenu.MenuId = GetParentMenuId();
            topmenu.ModuleId = ModuleID;;
            topmenu.RoleID = Convert.ToInt32(Session["RoleId"].ToString());
            topmenu.SelectedMenu = Convert.ToInt32(mid.ToString());
            topmenu.DataBind();
            Session["menuname"] = menuname;
            Session["menuid"] = menuid;
            Session["MId"] = mid;
            if (!IsPostBack)
            {
                txtFrom.Text = DateTime.Now.AddDays(-(DateTime.Now.Day) + 1).ToString("dd/MM/yyyy");
                txtTo.Text = DateTime.Now.AddDays((DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month) - (DateTime.Now.Day))).ToString("dd/MM/yyyy");
                BindWS();
                BindDepartments();
                ShowReport();

            }
        }
        #endregion PageLoad

        #region Supporting Methods

        public int GetParentMenuId()
        {
            if ( Convert.ToInt32(Session["UserId"]) == null)
            {
                Response.Redirect("Home.aspx");
            }
            string URL = Request.Url.Segments[Request.Url.Segments.Length - 1] + Request.Url.Query;
            int RoleId = Convert.ToInt32(Session["RoleId"].ToString());
            int ModuleId = ModuleID;;

            DataSet ds = new DataSet();

            ds = AttendanceDAC.GetAllowed(RoleId, ModuleId, URL);
            int MenuId = 0;
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                MenuId = Convert.ToInt32(ds.Tables[0].Rows[0]["Under"]);
                ViewState["Editable"] = (bool)ds.Tables[0].Rows[0]["Editable"];
                ViewState["ViewAll"] = (bool)ds.Tables[0].Rows[0]["ViewAll"];
                viewall = (bool)ViewState["ViewAll"];
                menuname = ds.Tables[0].Rows[0]["menuname"].ToString();
                menuid = MenuId.ToString();
                mid = Convert.ToInt32(ds.Tables[0].Rows[0]["MenuId"].ToString());
            }
            return MenuId;
        }
        public void BindWS()
        {
            DataSet ds = new DataSet();
            ds = AttendanceDAC.GetWorkSite(0, '1', Convert.ToInt32(Session["CompanyID"]));
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
            RaiseRDLCReport(".\\RDLC_Reports\\TDS.rdlc", AttendanceDAC.TDSDataset(Fromdate, Todate, WSID, DeptID, EmpID, EmpName, Convert.ToInt32(Session["CompanyID"])), "TDS Report");
        }
        void RaiseRDLCReport(string RpNamePath, DataSet rpds, string RpName)
        {
            Microsoft.Reporting.WebForms.ReportDataSource rptDataSource = null;
            TDSRV.LocalReport.ReportPath = RpNamePath;
            TDSRV.LocalReport.DisplayName = RpName;
            this.Title = "Report-[" + RpName + "]";
            rptDataSource = new Microsoft.Reporting.WebForms.ReportDataSource("TDS", rpds.Tables[0]);
            this.TDSRV.LocalReport.DataSources.Clear();
            this.TDSRV.LocalReport.DataSources.Add(rptDataSource);
            this.TDSRV.LocalReport.Refresh();
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