using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AECLOGIC.HMS.BLL;
using System.Data;
using System.Data.SqlClient;
using AECLOGIC.ERP.COMMON;
using AECLOGIC.ERP.HMS.HRClasses;

namespace AECLOGIC.ERP.HMS
{
    public partial class ExceRptAttendance : AECLOGIC.ERP.COMMON.WebFormMaster
    {

        int mid = 0;
        bool viewall;
        string menuname;
        string menuid;
        static DateTime? Date;
        static int? Month;
        static int? Year;
        static int WHSType;
        static int WSID = 0;
        static int Site = 0;
        static int CompanyID;
        AttendanceDAC objAtt = new AttendanceDAC();
        HRCommon objHrCommon = new HRCommon();

        protected override void OnInit(EventArgs e)
        {
            ModuleID = 1;
            base.OnInit(e);
            ExceRptAttendancePaging.FirstClick += new Paging.PageFirst(ExceRptAttendancePaging_FirstClick);
            ExceRptAttendancePaging.PreviousClick += new Paging.PagePrevious(ExceRptAttendancePaging_FirstClick);
            ExceRptAttendancePaging.NextClick += new Paging.PageNext(ExceRptAttendancePaging_FirstClick);
            ExceRptAttendancePaging.LastClick += new Paging.PageLast(ExceRptAttendancePaging_FirstClick);
            ExceRptAttendancePaging.ChangeClick += new Paging.PageChange(ExceRptAttendancePaging_FirstClick);
            ExceRptAttendancePaging.ShowRowsClick += new Paging.ShowRowsChange(ExceRptAttendancePaging_ShowRowsClick);
            ExceRptAttendancePaging.CurrentPage = 1;
        }
        void ExceRptAttendancePaging_ShowRowsClick(object sender, EventArgs e)
        {
            ExceRptAttendancePaging.CurrentPage = 1;
            BindPager();
        }
        void ExceRptAttendancePaging_FirstClick(object sender, EventArgs e)
        {
            BindPager();
        }
        void BindPager()
        {

            objHrCommon.PageSize = ExceRptAttendancePaging.CurrentPage;
            objHrCommon.CurrentPage = ExceRptAttendancePaging.ShowRows;
            BindGrid(objHrCommon);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            CompanyID = Convert.ToInt32(Session["CompanyID"]);
            if (!IsPostBack)
            {
                txtDay.Text = DateTime.Now.ToString("dd/MM/yyyy");

               
                BindYears();
                btnMonthReport_Click(sender, e);
                BindPager();
                try
                {

                    ViewState["WSID"] = 0;
                    if (Convert.ToInt32(Session["RoleId"].ToString()) == 7)
                    {
                        try
                        {


                            DataSet ds = clViewCPRoles.HR_DailyAttStatus( Convert.ToInt32(Session["UserId"]));
                            ViewState["WSID"] = ds.Tables[0].Rows[0]["ID"].ToString();
                            
                            txtSearchWorksite.Text = ds.Tables[0].Rows[0]["Name"].ToString();
                            txtSearchWorksite.ReadOnly = true;

                        }
                        catch { }
                    }
                }
                catch { }
            }
        }

        
        public void BindYears()
        {
              
        DataSet ds = AttendanceDAC.GetCalenderYear();

            int i = 0;
            int Maxyear = Convert.ToInt32(ds.Tables[0].Rows[0]["MaxYear"].ToString());
            for (int Minyear = Convert.ToInt32(ds.Tables[0].Rows[0]["MinYear"].ToString()); Minyear <= Maxyear; Minyear++)
            {
                ddlYear.Items.Insert(0, new ListItem(Convert.ToString(Minyear), Convert.ToString(Minyear)));
                i = i + 1;
            }
            ddlMonth.SelectedValue = ds.Tables[0].Rows[0]["CurrentMonth"].ToString();
            ddlYear.SelectedValue = ds.Tables[0].Rows[0]["CurrentYear"].ToString();

        }

        protected void btnDaySearch_Click(object sender, EventArgs e)
        {
            MainView.ActiveViewIndex = 0;

            if (txtDay.Text.Trim() != "")
            {
                Date = CODEUtility.ConvertToDate(txtDay.Text.Trim(), DateFormat.DayMonthYear);
            }
            WHSType = Convert.ToInt32(rblst.SelectedItem.Value);
            Month = null;
            Year = null;
            BindGrid(objHrCommon);

        }
        protected void btnMonthReport_Click(object sender, EventArgs e)
        {
            Date = null;
            if (ddlMonth.SelectedItem.Value != "0")
            {
                Month = Convert.ToInt32(ddlMonth.SelectedItem.Value);
            }
            if (ddlYear.SelectedItem.Value != "0")
            {
                Year = Convert.ToInt32(ddlYear.SelectedItem.Value);
            }
            WHSType = Convert.ToInt32(rblst.SelectedItem.Value);

            BindGrid(objHrCommon);
        }

        public void BindGrid(HRCommon objHrCommon)
        {
            try
            {

                objHrCommon.PageSize = ExceRptAttendancePaging.ShowRows;
                objHrCommon.CurrentPage = ExceRptAttendancePaging.CurrentPage;
                MainView.ActiveViewIndex = 0;

                  
                int? EmpID = null;

                int? SiteID = null;
                int? DeptID = null;
                int Hours = 0;

                if (txtEmpID.Text.Trim() != "")
                {
                    EmpID = Convert.ToInt32(txtEmpID.Text);
                }
                if (txtSearchWorksite.Text.Trim() != "")
                {
                    SiteID = Convert.ToInt32(ddlWorksite_hid.Value == "" ? "0" : ddlWorksite_hid.Value);

                }
                if (txtdepartment.Text.Trim() != "")
                {
                    DeptID = Convert.ToInt32(ddlDepartment_hid.Value == "" ? "0" : ddlDepartment_hid.Value);

                }

                if (ddlminhrs.SelectedItem.Value != "0")
                {
                    Hours = Convert.ToInt32(ddlminhrs.SelectedItem.Value);
                }
                try
                {
                    if (Convert.ToInt32(ViewState["WSID"]) > 0)
                        SiteID = Convert.ToInt32(ViewState["WSID"]);
                }
                catch { }
                DataSet ds = ExceReports.ExceRptAttendance(objHrCommon, Date, Month, Year, SiteID, Hours, EmpID, DeptID, WHSType, Convert.ToInt32(Session["CompanyID"]));

                if (ds != null && ds.Tables[0].Rows.Count > 0 && ds.Tables.Count > 0)
                {
                    grdAllEmployees.DataSource = ds;
                    ExceRptAttendancePaging.Bind(objHrCommon.CurrentPage, objHrCommon.TotalPages, objHrCommon.NoofRecords, objHrCommon.PageSize);
                }
                else
                {
                    grdAllEmployees.EmptyDataText = "No Records Found";
                    ExceRptAttendancePaging.Visible = false;
                }
                grdAllEmployees.DataBind();

            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public void BindIndDetails(int EmpID)
        {
            btnClsoe.Visible = true;
              

            int? SiteID = null;
            int? DeptID = null;
            int Hours = 0;

            if (txtSearchWorksite.Text.Trim() != "")
            {
                SiteID = Convert.ToInt32(ddlWorksite_hid.Value == "" ? "0" : ddlWorksite_hid.Value);

            }
            if (txtdepartment.Text.Trim() != "")
            {
                DeptID = Convert.ToInt32(ddlDepartment_hid.Value == "" ? "0" : ddlDepartment_hid.Value);

            }

            if (ddlminhrs.SelectedItem.Value != "0")
            {
                Hours = Convert.ToInt32(ddlminhrs.SelectedItem.Value);
            }
            DataSet ds = ExceReports.ExceRptAttendanceByEmpID(Date, Month, Year, SiteID, Hours, EmpID, WHSType, Convert.ToInt32(Session["CompanyID"]));
            if (ds.Tables.Count > 0)
            {
                GrdByMonth.DataSource = ds;
                GrdByMonth.DataBind();
            }
            else
            {
                GrdByMonth.DataSource = null;
                GrdByMonth.DataBind();
            }
        }

        protected void grdAllEmployees_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "detail")
            {
                MainView.ActiveViewIndex = 1;
                BindIndDetails(Convert.ToInt32(e.CommandArgument));
            }
        }
        protected void btnClsoe_Click(object sender, EventArgs e)
        {
            btnClsoe.Visible = false;
            btnMonthReport_Click(sender, e);
        }

        

        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionList(string prefixText, int count, string contextKey)
        {
        
            DataSet ds = AttendanceDAC.HR_GetWorkSite_By_googlesearch_EmpList(prefixText.Trim(), WSID);
            DataTable dt = ds.Tables[0];
            List<string> items = new List<string>(count);
            var rtval = new Dictionary<string, string>();
            foreach (DataRow row in dt.Rows)
            {
                string str = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(row["Name"].ToString(), row["ID"].ToString());
                items.Add(str);
            }

            return items.ToArray(); ;// txtItems.ToArray();

        }
        protected void GetWork(object sender, EventArgs e)
        {

            CompanyID = Convert.ToInt32(Session["CompanyID"]);
            WSID = Convert.ToInt32(ddlWorksite_hid.Value == "" ? "0" : ddlWorksite_hid.Value); ;
                }

        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionList_Department(string prefixText, int count, string contextKey)
        {
        
            DataSet ds = AttendanceDAC.HR_googlesearch_GetDepartmentBySite(prefixText.Trim(), WSID, CompanyID);
            DataTable dt = ds.Tables[0];
            List<string> items = new List<string>(count);
            var rtval = new Dictionary<string, string>();
            foreach (DataRow row in dt.Rows)
            {
                string str = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(row["Name"].ToString(), row["ID"].ToString());
                items.Add(str);
            }

            return items.ToArray(); ;// txtItems.ToArray();

        }
        protected void GetDept(object sender, EventArgs e)
        {
            Site = 0;
        
            Site = Convert.ToInt32(ddlDepartment_hid.Value == "" ? "0" : ddlDepartment_hid.Value); ;
            CompanyID = Convert.ToInt32(Session["CompanyID"]);

        }
    }
}