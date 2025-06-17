using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using Aeclogic.Common.DAL;
using System.IO;
using AECLOGIC.HMS.BLL;
using System.Configuration;
using System.Collections;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using AECLOGIC.ERP.COMMON;
using System.Text;
using System.Drawing;
namespace AECLOGIC.ERP.HMS
{
    public partial class MonthlyWorkingDaysAndPayableDaysV4 : AECLOGIC.ERP.COMMON.WebFormMaster
    {
        TableRow tblRow;
        int mid = 0;
        bool viewall;
        static int wsid; int stmonth = 0; int edmonth = 0; int days = 0;
        string menuname;
        static int SearchCompanyID;
        static int Siteid = 0;
        static int EDeptid;
        static int EWsid;
        static int Empid;
        string menuid;
        bool Editable;
        static int scmonth = 0, scyear = 0;
        DataSet dsEPMData = new DataSet();
        DataSet dsCurrentDetails= new DataSet();
        HRCommon objHrCommon = new HRCommon();
        protected override void OnInit(EventArgs e)
        {
            ModuleID = 1;
            base.OnInit(e);
            taskpaging.FirstClick += new Paging.PageFirst(EmpListPaging_FirstClick);
            taskpaging.NextClick += new Paging.PageNext(EmpListPaging_FirstClick);
            taskpaging.PreviousClick += new Paging.PagePrevious(EmpListPaging_FirstClick);
            taskpaging.ChangeClick += new Paging.PageChange(EmpListPaging_FirstClick);
            taskpaging.LastClick += new Paging.PageLast(EmpListPaging_FirstClick);
            taskpaging.ShowRowsClick += new Paging.ShowRowsChange(EmpListPaging_ShowRowsClick);
            taskpaging.CurrentPage = 1;
            Paging1.FirstClick += new Paging.PageFirst(EmpListPaging_FirstClick);
            Paging1.NextClick += new Paging.PageNext(EmpListPaging_FirstClick);
            Paging1.PreviousClick += new Paging.PagePrevious(EmpListPaging_FirstClick);
            Paging1.ChangeClick += new Paging.PageChange(EmpListPaging_FirstClick);
            Paging1.LastClick += new Paging.PageLast(EmpListPaging_FirstClick);
            Paging1.ShowRowsClick += new Paging.ShowRowsChange(EmpListPaging_ShowRowsClick);
            Paging1.CurrentPage = 1;
            Pagingmsg.FirstClick += new Paging.PageFirst(MissingEmployees_FirstClick);
            Pagingmsg.PreviousClick += new Paging.PagePrevious(MissingEmployees_FirstClick);
            Pagingmsg.NextClick += new Paging.PageNext(MissingEmployees_FirstClick);
            Pagingmsg.LastClick += new Paging.PageLast(MissingEmployees_FirstClick);
            Pagingmsg.ChangeClick += new Paging.PageChange(MissingEmployees_FirstClick);
            Pagingmsg.ShowRowsClick += new Paging.ShowRowsChange(MissingEmployees_ShowRowsClick);
            Pagingmsg.CurrentPage = 1;
            PagingzeroPayabledaysemp.FirstClick += new Paging.PageFirst(ZeroPayableDays_FirstClick);
            PagingzeroPayabledaysemp.NextClick += new Paging.PageNext(ZeroPayableDays_FirstClick);
            PagingzeroPayabledaysemp.PreviousClick += new Paging.PagePrevious(ZeroPayableDays_FirstClick);
            PagingzeroPayabledaysemp.ChangeClick += new Paging.PageChange(ZeroPayableDays_FirstClick);
            PagingzeroPayabledaysemp.LastClick += new Paging.PageLast(ZeroPayableDays_FirstClick);
            PagingzeroPayabledaysemp.ShowRowsClick += new Paging.ShowRowsChange(ZerpPayableDays_ShowRowsClick);
            PagingzeroPayabledaysemp.CurrentPage = 1;
            Pagingfs.FirstClick += new Paging.PageFirst(FSEmployees_FirstClick);
            Pagingfs.PreviousClick += new Paging.PagePrevious(FSEmployees_FirstClick);
            Pagingfs.NextClick += new Paging.PageNext(FSEmployees_FirstClick);
            Pagingfs.LastClick += new Paging.PageLast(FSEmployees_FirstClick);
            Pagingfs.ChangeClick += new Paging.PageChange(FSEmployees_FirstClick);
            Pagingfs.ShowRowsClick += new Paging.ShowRowsChange(FSEmployees_ShowRowsClick);
            Pagingfs.CurrentPage = 1;
        }
        void FSEmployees_ShowRowsClick(object sender, EventArgs e)
        {
            Pagingfs.CurrentPage = 1;
            objHrCommon.CurrentPage = Pagingfs.CurrentPage;
            objHrCommon.PageSize = Pagingfs.ShowRows;
            BindingFSEmployees();
        }
        void FSEmployees_FirstClick(object sender, EventArgs e)
        {
            objHrCommon.CurrentPage = Pagingfs.CurrentPage;
            objHrCommon.PageSize = Pagingfs.ShowRows;
            BindingFSEmployees();
        }
        void MissingEmployees_ShowRowsClick(object sender, EventArgs e)
        {
            Pagingmsg.CurrentPage = 1;
            objHrCommon.CurrentPage = Pagingmsg.CurrentPage;
            objHrCommon.PageSize = Pagingmsg.ShowRows;
            BindingMissingEmployees();
        }
        void MissingEmployees_FirstClick(object sender, EventArgs e)
        {
            objHrCommon.CurrentPage = Pagingmsg.CurrentPage;
            objHrCommon.PageSize = Pagingmsg.ShowRows;
            BindingMissingEmployees();
        }
        void ZerpPayableDays_ShowRowsClick(object sender, EventArgs e)
        {
            PagingzeroPayabledaysemp.CurrentPage = 1;
            objHrCommon.CurrentPage = PagingzeroPayabledaysemp.CurrentPage;
            objHrCommon.PageSize = PagingzeroPayabledaysemp.ShowRows;
            BindZeroPayableDaysEmployees();
        }
        void ZeroPayableDays_FirstClick(object sender, EventArgs e)
        {
            objHrCommon.CurrentPage = PagingzeroPayabledaysemp.CurrentPage;
            objHrCommon.PageSize = PagingzeroPayabledaysemp.ShowRows;
            BindZeroPayableDaysEmployees();
        }
        #region Bindpager
        void EmpListPaging_FirstClick(object sender, EventArgs e)
        {
            BindPager();
            if (ViewState["WSID"] != null)
            {
                objHrCommon.SiteID = Convert.ToInt32(ViewState["WSID"]);
                Bindgrid(objHrCommon);
            }
            BindZeroPayableDaysEmployees();
        }
        void EmpListPaging_ShowRowsClick(object sender, EventArgs e)
        {
            taskpaging.CurrentPage = 1;
            Paging1.CurrentPage = 1;
            PagingzeroPayabledaysemp.CurrentPage = 1;
            BindPager();
            if (ViewState["WSID"] != null)
            {
                objHrCommon.SiteID = Convert.ToInt32(ViewState["WSID"]);
                Bindgrid(objHrCommon);
            }
            BindZeroPayableDaysEmployees();
        }
        public void BindPager()
        {
            objHrCommon.CurrentPage = Paging1.CurrentPage;
            objHrCommon.PageSize = Paging1.ShowRows;
            lnkunsync_Click(objHrCommon);
        }
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                SearchCompanyID = Convert.ToInt32(Session["CompanyID"]);
                if (!IsPostBack)
                {
                    gdvAdjDiffDet.Visible = false;
                    gdvMonthReport.Visible = false;
                    gdvCurrentDetails.Visible = false;
                    lblAdj.Visible = false;
                    lblPaidDays.Visible = false;
                    lblActualEligibility.Visible = false;
                    DataSet startdate = SQLDBUtil.ExecuteDataset("G_StartdateForAdvAttendance");
                    ViewState["startdate"] = 1;
                    ViewState["enddate"] = 1;
                    DataSet enddate = SQLDBUtil.ExecuteDataset("G_EnddateForAdvAttendance");
                    ViewState["startdate"] = startdate.Tables[0].Rows[0][0].ToString();
                    ViewState["enddate"] = enddate.Tables[0].Rows[0][0].ToString();
                    bindyear();
                    BindPager();
                    scmonth = Convert.ToInt32(ddlmonth.SelectedValue);
                    scyear = Convert.ToInt32(ddlyear.SelectedValue);
                    tblunsync.Visible = true;
                    dvView.Visible = false;
                }
            }
            catch (Exception ex)
            {
                clsErrorLog.HMSEventLog(ex, "HRLeaveApplication", "Page_Load", "001");
            }
        }
        public void Bindgrid(HRCommon objHrCommon)
        {
            try
            {
                SqlParameter[] SP = new SqlParameter[5];
                SP[0] = new SqlParameter("@wsid", objHrCommon.SiteID);
                SP[1] = new SqlParameter("@fcase", 2);
                SP[2] = new SqlParameter("@Month", Convert.ToInt32(ddlmonth.SelectedValue));
                SP[3] = new SqlParameter("@Year", Convert.ToInt32(ddlyear.SelectedValue));
                SP[4] = new SqlParameter("@Process", "Payable Days Sync");
                DataSet ds1 = SQLDBUtil.ExecuteDataset("sh_Paydayslastsynctime", SP);
                if (ds1.Tables[0].Rows.Count > 0)
                {
                    lbllastsync.Text = "Last Sync was Done for " + ds1.Tables[0].Rows[0]["Site_Name"].ToString() + " at " + ds1.Tables[0].Rows[0]["date"].ToString();
                }
                else
                    lbllastsync.Text = String.Empty;
                // }
                int EmpID = 0; int Month = 0; int Year = 0; int siteid = 0;
                gvmnwrkngdays.DataSource = null;
                gvmnwrkngdays.DataBind();
                string EmpID1 = "0", deptid = "0";
                if (txtemp.Text != "")
                {
                    string[] words = txtemp.Text.Split('-');
                    EmpID1 = words[0];
                }
                siteid = objHrCommon.SiteID;
                ViewState["siteid"] = siteid;
                string stDate, enddate;
                stDate = 01 + "/" + ddlmonth.SelectedItem.Value + "/" + ddlyear.SelectedItem.Value;
                int lastdayinmonth = DateTime.DaysInMonth(Convert.ToInt32(ddlyear.SelectedItem.Value), Convert.ToInt32(ddlmonth.SelectedItem.Value));
                enddate = lastdayinmonth + "/" + ddlmonth.SelectedItem.Value + "/" + ddlyear.SelectedItem.Value;
                DateTime startdate = CodeUtilHMS.ConvertToDate(stDate, CodeUtilHMS.DateFormat.DayMonthYear);
                DateTime EndDate = CodeUtilHMS.ConvertToDate(enddate, CodeUtilHMS.DateFormat.DayMonthYear);
                objHrCommon.CurrentPage = taskpaging.CurrentPage;
                objHrCommon.PageSize = taskpaging.ShowRows;
                SqlParameter[] p = new SqlParameter[9];
                p[0] = new SqlParameter("@CurrentPage", objHrCommon.CurrentPage);
                p[1] = new SqlParameter("@PageSize", objHrCommon.PageSize);
                p[2] = new SqlParameter("@NoOfRecords", System.Data.SqlDbType.Int);
                p[2].Direction = ParameterDirection.Output;
                p[3] = new SqlParameter("returnvalue", System.Data.SqlDbType.Int);
                p[3].Direction = ParameterDirection.ReturnValue;
                p[4] = new SqlParameter("@EmpID", EmpID1);
                p[5] = new SqlParameter("@PayStartDt", startdate);
                p[6] = new SqlParameter("@PayEndDt", EndDate);
                p[7] = new SqlParameter("@WSID", objHrCommon.SiteID);
                p[8] = new SqlParameter("@deptid", 0);
                DataSet ds = SqlHelper.ExecuteDataset("HMS_MonthlyWorkingDays_Rev4", p);
                if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0)
                {
                    gvmnwrkngdays.DataSource = ds;
                    gvmnwrkngdays.DataBind();
                    ViewState["EmpGrid"] = ds;
                    dvView.Visible = true;
                }
                else
                    dvView.Visible = false;
                int totpage = Convert.ToInt32(p[3].Value);
                int noofrec = Convert.ToInt32(p[2].Value);
                objHrCommon.TotalPages = totpage;
                objHrCommon.NoofRecords = noofrec;
                taskpaging.Visible = true;
                taskpaging.Bind(objHrCommon.CurrentPage, objHrCommon.TotalPages, objHrCommon.NoofRecords, objHrCommon.PageSize);
            }
            catch { }
        }
        public DataSet bindyear()
        {
            FIllObject.FillDropDown(ref ddlyear, "HMS_YearWise");
            DataSet ds = AttendanceDAC.GetCalenderYear();
            if (ds.Tables[0].Rows[0]["PreviousMonth"].ToString() == "0")
            {
                ddlmonth.SelectedValue = "12";
                int CurrentYear = Convert.ToInt32(ds.Tables[0].Rows[0]["CurrentYear"]);
                int PreviousYear = CurrentYear - 1;
                ddlyear.Items.FindByValue(PreviousYear.ToString()).Selected = true;
            }
            //if we are in same year and same month
            else
            {
                ddlmonth.SelectedValue = ds.Tables[0].Rows[0]["PreviousMonth"].ToString();
                if (ddlyear.Items.FindByValue(ds.Tables[0].Rows[0]["CurrentYear"].ToString()) != null)
                {
                    ddlyear.Items.FindByValue(ds.Tables[0].Rows[0]["CurrentYear"].ToString()).Selected = true;
                }
                else
                {
                    ddlyear.SelectedIndex = 0;
                    //ddlYear.Items.Count - 1
                }
            }
            DataSet ds1 = SqlHelper.ExecuteDataset("HMS_YearWise");
            return ds1;
        }
        protected void btnsearch_Click(object sender, EventArgs e)
        {
            gvzeroPayabledaysemp.DataSource = null;
            gvzeroPayabledaysemp.DataBind();
            gvmsngemp.Visible = false;
            gdvAdjDiffDet.Visible = false;
            gdvMonthReport.Visible = false;
            gdvCurrentDetails.Visible = false;
            lblAdj.Visible = false;
            lblPaidDays.Visible = false;
            lblActualEligibility.Visible = false;
            lblmsg1.Visible = false;
            Paging1.Visible = false;
            gvzeroPayabledaysemp.Visible = false;
            lblmsg.Visible = false;
            PagingzeroPayabledaysemp.Visible = false;
            Paging1.CurrentPage = 1;
            BindPager();
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetEmpDetail(string prefixText, int count, string contextKey)
        {
            DataSet ds = AttendanceDAC.HMS_Service_DLL_Employee_By_WS_Dept_googlesearch_AttPay(prefixText.Trim(),wsid,0,scmonth,scyear);
            DataTable dt = ds.Tables[0];
            List<string> items = new List<string>(count);
            var rtval = new Dictionary<string, string>();
            foreach (DataRow row in dt.Rows)
            {
                string str = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(row["Name"].ToString(), row["ID"].ToString());
                items.Add(str);
            }
            return items.ToArray();
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute]
        public static string[] GetCompletionList(string prefixText, int count, string contextKey)
        {
            DataSet ds = AttendanceDAC.GetWorkSites(prefixText);
            return ConvertStingArray(ds);
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute]
        public static string[] GetCompletionListdept(string prefixText, int count, string contextKey)
        {
            DataSet ds = AttendanceDAC.GetDepartment(prefixText, SearchCompanyID, Siteid);
            return ConvertStingArray(ds);
        }
        public static string[] ConvertStingArray(DataSet ds)
        {
            string[] rtval = Array.ConvertAll(ds.Tables[0].Select(), new Converter<DataRow, string>(DataRowTotable));
            return rtval;
        }
        public static string DataRowTotable(DataRow dr)
        {
            return dr["Name"].ToString();
        }
        private void PullFrmAtt(GridViewRow row)
        {
            DropDownList ddlmonthgrid = (DropDownList)row.FindControl("ddlmonthgrid");
            DropDownList ddlyeargrid = (DropDownList)row.FindControl("ddlyeargrid");
            Label lblwsid = (Label)row.FindControl("lblwsid");
            if (ddlmonthgrid.SelectedIndex != 0)
            {
            }
            else
            {
                AlertMsg.MsgBox(Page, "Select Month",AlertMsg.MessageType.Warning);
                return;
            }
            if (Convert.ToInt32(ddlyeargrid.SelectedValue) != 0)
            {
            }
            else
            {
                AlertMsg.MsgBox(Page, "Select Year", AlertMsg.MessageType.Warning);
                return;
            }
            SqlParameter[] sqlParams = new SqlParameter[4];
            sqlParams[0] = new SqlParameter("@wsid", Convert.ToInt32(lblwsid.Text));
            sqlParams[1] = new SqlParameter("@month", ddlmonth.SelectedValue);
            sqlParams[2] = new SqlParameter("@year", ddlyear.SelectedValue);
            string EmpID1 = "";
            if (txtemp.Text != "")
            {
                string[] words = txtemp.Text.Split('-');
                EmpID1 = words[0];
            }
            if (EmpID1 != "")
                sqlParams[3] = new SqlParameter("@empid", EmpID1);
            else
                sqlParams[3] = new SqlParameter("@empid", DBNull.Value);
            DataSet dss = SQLDBUtil.ExecuteDataset("sh_Advacnceattendancestatus", sqlParams);
            if (dss.Tables[0].Rows[0][0].ToString() == "Already Synchronized")
            {
                SqlParameter[] sqlParams1 = new SqlParameter[3];
                sqlParams1[0] = new SqlParameter("@wsid", Convert.ToInt32(lblwsid.Text));
                sqlParams1[1] = new SqlParameter("@month", ddlmonth.SelectedValue);
                sqlParams1[2] = new SqlParameter("@year", ddlyear.SelectedValue);
                DataSet ds1 = SQLDBUtil.ExecuteDataset("sh_AdvAttendanceZeroChekcingbefore", sqlParams1);
                if (ds1.Tables.Count == 0)
                {
                    AdvanceAttendanceChecking(row);
                }
                else
                {
                    gvmnwrkngdays.DataSource = null;
                    gvmnwrkngdays.DataBind();
                    taskpaging.Visible = false;
                    AlertMsg.MsgBox(Page, ds1.Tables[0].Rows[0][0].ToString());
                }
            }
            else
            {
                gvmnwrkngdays.DataSource = null;
                gvmnwrkngdays.DataBind();
                taskpaging.Visible = false;
                AlertMsg.MsgBox(Page, dss.Tables[0].Rows[0][0].ToString());
            }
        }
        private void AdvanceAttendanceChecking(GridViewRow row)
        {
            DropDownList ddlmonthgrid = (DropDownList)row.FindControl("ddlmonthgrid");
            DropDownList ddlyeargrid = (DropDownList)row.FindControl("ddlyeargrid");
            Label lblwsid = (Label)row.FindControl("lblwsid");
            int Month = 0; int Year = 0;
            //int siteid = 0;
            if (Convert.ToInt32(ddlmonthgrid.SelectedValue) != 0 && Convert.ToInt32(ddlyeargrid.SelectedValue) != 0)
            {
                if (Convert.ToInt32(lblwsid.Text) != 0)
                {
                    ViewState["siteid"] = Convert.ToInt32(lblwsid.Text);
                    string EmpID1 = "";
                    SqlParameter[] sqlParams = new SqlParameter[4];
                    sqlParams[0] = new SqlParameter("@Month", Convert.ToInt32(ddlmonthgrid.SelectedValue));
                    sqlParams[1] = new SqlParameter("@year", Convert.ToInt32(ddlyeargrid.SelectedValue));
                    sqlParams[2] = new SqlParameter("@wsid", Convert.ToInt32(lblwsid.Text));
                    if (txtemp.Text != "")
                    {
                        string[] words = txtemp.Text.Split('-');
                        EmpID1 = words[0];
                    }
                    if (EmpID1 != "")
                        sqlParams[3] = new SqlParameter("@empidd", EmpID1);
                    else
                        sqlParams[3] = new SqlParameter("@empidd", DBNull.Value);
                    DataSet ds = SQLDBUtil.ExecuteDataset("USP_Monthlypaybledays_Rev4", sqlParams);
                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        SqlParameter[] SP = new SqlParameter[6];
                        SP[0] = new SqlParameter("@Userid", Convert.ToInt32(Session["UserId"]));
                        SP[1] = new SqlParameter("@wsid", Convert.ToInt32(lblwsid.Text));
                        SP[2] = new SqlParameter("@fcase", 1);
                        SP[3] = new SqlParameter("@Year", Convert.ToInt32(ddlyeargrid.SelectedValue));
                        SP[4] = new SqlParameter("@Process", "Payable Days Sync");
                        SP[5] = new SqlParameter("@Month", Convert.ToInt32(ddlmonthgrid.SelectedValue));
                        int i = SQLDBUtil.ExecuteNonQuery("sh_Paydayslastsynctime", SP);
                        AlertMsg.MsgBox(Page, "Computed !");
                        BindPager();
                    }
                    else
                    {
                        AlertMsg.MsgBox(Page, "Not Computed.Check Attendance!", AlertMsg.MessageType.Warning);
                    }
                }
                else
                    AlertMsg.MsgBox(Page, "WorkSite Manditory", AlertMsg.MessageType.Warning);
            }
            else
                AlertMsg.MsgBox(Page, "Select Month/Year", AlertMsg.MessageType.Warning);
        }
        protected void lnkunsync_Click(HRCommon objHrCommon)
        {
            SqlParameter[] SP = new SqlParameter[8];
            SP[0] = new SqlParameter("@Year", Convert.ToInt32(ddlyear.SelectedValue));
            SP[1] = new SqlParameter("@Month", Convert.ToInt32(ddlmonth.SelectedValue));
            SP[2] = new SqlParameter("@fcase", 3);
            SP[3] = new SqlParameter("@Process", "Payable Days Sync");
            SP[4] = new SqlParameter("@CurrentPage", objHrCommon.CurrentPage);
            SP[5] = new SqlParameter("@PageSize", objHrCommon.PageSize);
            SP[6] = new SqlParameter("@NoOfRecords", System.Data.SqlDbType.Int);
            SP[6].Direction = ParameterDirection.Output;
            SP[7] = new SqlParameter("returnvalue", System.Data.SqlDbType.Int);
            SP[7].Direction = ParameterDirection.ReturnValue;
            DataSet ds = SQLDBUtil.ExecuteDataset("sh_Paydayslastsynctime_Payabledays", SP);
            if (ds.Tables[0].Rows.Count > 0)
            {
                gvunsync.DataSource = ds.Tables[0];
                tblunsync.Visible = true;
            }
            else
                gvunsync.DataSource = null;
            gvunsync.DataBind();
            int totpage = Convert.ToInt32(SP[7].Value);
            int noofrec = Convert.ToInt32(SP[6].Value);
            objHrCommon.TotalPages = totpage;
            objHrCommon.NoofRecords = noofrec;
            Paging1.Visible = true;
            Paging1.Bind(objHrCommon.CurrentPage, objHrCommon.TotalPages, objHrCommon.NoofRecords, objHrCommon.PageSize);
        }
        protected void lbllastsync_Click(object sender, EventArgs e)
        {
            HRCommon objHrCommon = new HRCommon();
            SqlParameter[] SP = new SqlParameter[4];
            SP[0] = new SqlParameter("@Year", Convert.ToInt32(ddlyear.SelectedValue));
            SP[1] = new SqlParameter("@Month", Convert.ToInt32(ddlmonth.SelectedValue));
            SP[2] = new SqlParameter("@fcase", 4);
            SP[3] = new SqlParameter("@wsid", objHrCommon.SiteID);
            DataSet ds = SQLDBUtil.ExecuteDataset("sh_Paydayslastsynctime", SP);
            string msg;
            msg = "No.Of Syncs : " + ds.Tables[0].Rows.Count + "\n";
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                msg += ds.Tables[0].Rows[i]["date"].ToString() + "\n";
            }
            AlertMsg.MsgBox(Page, msg);
        }
        protected void gvmnwrkngdays_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string name = e.CommandArgument.ToString();
            string[] words = name.Split('-');
            int Empid = Convert.ToInt32(words[0]);
            int month = Convert.ToInt32(ddlmonth.SelectedValue);
            int year = Convert.ToInt32(ddlyear.SelectedValue);
            gdvAdjDiffDet.Visible = false;
            gdvMonthReport.Visible = false;
            gdvCurrentDetails.Visible = false;
            lblAdj.Visible = false;
            lblPaidDays.Visible = false;
            lblActualEligibility.Visible = false;
            GridViewRow row = (GridViewRow)(((Control)e.CommandSource).NamingContainer);
            if (e.CommandName == "att")
            {
                CalenderMonth(Empid, year, month);
            }
            if (e.CommandName == "Pay")
            {
                LinkButton lnkPayable = (LinkButton)gvmnwrkngdays.Rows[row.RowIndex].Cells[4].FindControl("lnkPay");
                if (Convert.ToInt32(lnkPayable.Text) > 0)
                    PayableCalculationDetails(Empid, year, month, 1);
            }
            if (e.CommandName == "PPay")
            {
                LinkButton lnkPendPay = (LinkButton)gvmnwrkngdays.Rows[row.RowIndex].Cells[5].FindControl("lnkPendPay");
                string s = lnkPendPay.Text.Replace("(", "").Replace(")", "");
                if (Convert.ToInt32(s) > 0)
                    MonthlyPayableCalculationDetails(Empid, year, month);
                PayableCalculationDetails(Empid, year, month, 2);
                PrePayableCalculationDetails(Empid, year, month, 2);
            }
        }
        private void MonthlyPayableCalculationDetails(int EmpID, int Year, int Month)
        {
            gdvAdjDiffDet.Visible = true;
            gdvAdjDiffDet.DataSource = null;
            gdvAdjDiffDet.DataBind();
            SqlParameter[] objParam = new SqlParameter[3];
            objParam[0] = new SqlParameter("@month", Month);
            objParam[1] = new SqlParameter("@year", Year);
            objParam[2] = new SqlParameter("@Empid", EmpID);
            DataSet dsAdjDiffDet = SQLDBUtil.ExecuteDataset("sh_AdjDiffDaysDet", objParam);
            if (dsAdjDiffDet != null && dsAdjDiffDet.Tables.Count > 0 && dsAdjDiffDet.Tables[0].Rows.Count > 0)
            {
                gdvAdjDiffDet.DataSource = dsAdjDiffDet;
            }
            else
                gdvAdjDiffDet.DataSource = null;
            gdvAdjDiffDet.DataBind();
        }
        private void PayableCalculationDetails(int EmpID, int Year, int Month, int ID)
        {
            gdvMonthReport.Visible = true;
            gdvMonthReport.DataSource = null;
            gdvMonthReport.DataBind();
            lblPaidDays.Visible = true;
            if (ID == 2)
                lblAdj.Visible = true;
            SqlParameter[] objParam = new SqlParameter[4];
            objParam[0] = new SqlParameter("@month", Month);
            objParam[1] = new SqlParameter("@year", Year);
            objParam[2] = new SqlParameter("@Empid", EmpID);
            objParam[3] = new SqlParameter("@id", ID);
            dsEPMData = SQLDBUtil.ExecuteDataset("sh_MonthAttendanceDetails", objParam);
            if (dsEPMData != null && dsEPMData.Tables.Count > 0 && dsEPMData.Tables[0].Rows.Count > 0)
            {
                gdvMonthReport.DataSource = dsEPMData.Tables[0];
            }
            else
                gdvMonthReport.DataSource = null;
            gdvMonthReport.DataBind();
        }
        private void PrePayableCalculationDetails(int EmpID, int Year, int Month, int ID)
        {
            gdvCurrentDetails.Visible = true;
            gdvCurrentDetails.DataSource = null;
            gdvCurrentDetails.DataBind();
            lblActualEligibility.Visible = true;
            SqlParameter[] objParam = new SqlParameter[4];
            objParam[0] = new SqlParameter("@month", Month);
            objParam[1] = new SqlParameter("@year", Year);
            objParam[2] = new SqlParameter("@Empid", EmpID);
            objParam[3] = new SqlParameter("@id", ID);
            dsCurrentDetails = SQLDBUtil.ExecuteDataset("sh_MonthAttendanceDetails_Prev", objParam);
            if (dsCurrentDetails != null && dsCurrentDetails.Tables.Count > 0 && dsCurrentDetails.Tables[0].Rows.Count > 0)
            {
                gdvCurrentDetails.DataSource = dsCurrentDetails.Tables[0];
            }
            else
                gdvCurrentDetails.DataSource = null;
            gdvCurrentDetails.DataBind();
        }
        int siteid = 0;
        protected void gvunsync_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            siteid = Convert.ToInt32(e.CommandArgument);
            gdvAdjDiffDet.Visible = false;
            gdvMonthReport.Visible = false;
            gdvCurrentDetails.Visible = false;
            lblAdj.Visible = false;
            lblPaidDays.Visible = false;
            lblActualEligibility.Visible = false;
            if (e.CommandName == "com")
            {
                GridViewRow gvr = (GridViewRow)(((Control)e.CommandSource).NamingContainer);
                DropDownList ddlmonthgrid = (DropDownList)gvr.FindControl("ddlmonthgrid");
                DropDownList ddlyeargrid = (DropDownList)gvr.FindControl("ddlyeargrid");
                ddlmonth.SelectedValue = ddlmonthgrid.SelectedValue;
                ddlyear.SelectedValue = ddlyear.SelectedValue;
                foreach (GridViewRow row in gvunsync.Rows)
                {
                    LinkButton lnkdisplay = (LinkButton)row.FindControl("lnkdisplay");
                    lnkdisplay.CssClass = "btn btn-primary";
                }
                Wsgrid(gvr);
                PullFrmAtt(gvr);
                gvmnwrkngdays.DataSource = null;
                gvmnwrkngdays.DataBind();
                txtemp.Text = String.Empty;
            }
            if (e.CommandName == "dis")
            {
                txtemp.Text = String.Empty;
                foreach (GridViewRow row in gvunsync.Rows)
                {
                    LinkButton lnkdisplay = (LinkButton)row.FindControl("lnkdisplay");
                    lnkdisplay.CssClass = "btn btn-primary";
                }
                GridViewRow gvr = (GridViewRow)(((Control)e.CommandSource).NamingContainer);
                LinkButton lnkdisplaygrid = (LinkButton)gvr.FindControl("lnkdisplay");
                lnkdisplaygrid.CssClass = "btn btn-warning";
                DropDownList ddlmonthgrid = (DropDownList)gvr.FindControl("ddlmonthgrid");
                DropDownList ddlyeargrid = (DropDownList)gvr.FindControl("ddlyeargrid");
                Label lblwsid = (Label)gvr.FindControl("lblwsid");
                scmonth =Convert.ToInt32(ddlmonthgrid.SelectedValue);
                scyear = Convert.ToInt32 (ddlyeargrid.SelectedValue);
                ddlmonth.SelectedValue = ddlmonthgrid.SelectedValue;
                ddlyear.SelectedValue = ddlyeargrid.SelectedValue;
                wsid = Convert.ToInt32(lblwsid.Text);
                taskpaging.CurrentPage = 1;
                objHrCommon.CurrentPage = taskpaging.CurrentPage;
                objHrCommon.PageSize = taskpaging.ShowRows;
                objHrCommon.SiteID = siteid;
                ViewState["WSID"] = siteid;
                Bindgrid(objHrCommon);
            }
            if (e.CommandName == "MissNo")
            {
                Pagingmsg.CurrentPage = 1;
                objHrCommon.CurrentPage = Pagingmsg.CurrentPage;
                objHrCommon.PageSize = Pagingmsg.ShowRows;
                ViewState["WSID"] = siteid;
                BindingMissingEmployees();
            }
            if (e.CommandName == "ZPD")
            {
                PagingzeroPayabledaysemp.CurrentPage = 1;
                objHrCommon.CurrentPage = PagingzeroPayabledaysemp.CurrentPage;
                objHrCommon.PageSize = PagingzeroPayabledaysemp.ShowRows;
                ViewState["WSID"] = siteid;
                BindZeroPayableDaysEmployees();
            }
            if (e.CommandName == "FS")
            {
                Pagingfs.CurrentPage = 1;
                objHrCommon.CurrentPage = Pagingfs.CurrentPage;
                objHrCommon.PageSize = Pagingfs.ShowRows;
                ViewState["WSID"] = siteid;
                BindingFSEmployees();
            }
        }
        private void BindingFSEmployees()
        {
            SqlParameter[] SP = new SqlParameter[7];
            SP[0] = new SqlParameter("@Year", Convert.ToInt32(ddlyear.SelectedValue));
            SP[1] = new SqlParameter("@Month", Convert.ToInt32(ddlmonth.SelectedValue));
            SP[2] = new SqlParameter("@CurrentPage", objHrCommon.CurrentPage);
            SP[3] = new SqlParameter("@PageSize", objHrCommon.PageSize);
            SP[4] = new SqlParameter("@NoOfRecords", System.Data.SqlDbType.Int);
            SP[4].Direction = ParameterDirection.Output;
            SP[5] = new SqlParameter("returnvalue", System.Data.SqlDbType.Int);
            SP[5].Direction = ParameterDirection.ReturnValue;
            SP[6] = new SqlParameter("@Siteid", ViewState["WSID"]);
            DataSet ds = SQLDBUtil.ExecuteDataset("sh_GetFSemployee", SP);
            if (ds.Tables[0].Rows.Count > 0)
            {
                gvfsemp.DataSource = ds.Tables[0];
                gvfsemp.DataBind();
                gvfsemp.Visible = true;
                lblsFS.Visible = true;
                int totpage = Convert.ToInt32(SP[5].Value);
                int noofrec = Convert.ToInt32(SP[4].Value);
                objHrCommon.TotalPages = totpage;
                objHrCommon.NoofRecords = noofrec;
                Pagingfs.Visible = true;
                Pagingfs.Bind(objHrCommon.CurrentPage, objHrCommon.TotalPages, objHrCommon.NoofRecords, objHrCommon.PageSize);
            }
            else
            {
                gvfsemp.DataSource = null;
                gvfsemp.DataBind();
                lblsFS.Visible = true;
                gvfsemp.Visible = false;
                Pagingfs.Visible = false;
            }
        }
        private static void Wsgrid(GridViewRow row)
        {
            DropDownList ddlmonthgrid = (DropDownList)row.FindControl("ddlmonthgrid");
            DropDownList ddlyeargrid = (DropDownList)row.FindControl("ddlyeargrid");
            Label lblwsid = (Label)row.FindControl("lblwsid");
            Label lbllastsyncgrid = (Label)row.FindControl("lbllastsyncgrid");
            SqlParameter[] SP = new SqlParameter[5];
            SP[0] = new SqlParameter("@wsid", Convert.ToInt32(lblwsid.Text));
            SP[1] = new SqlParameter("@fcase", 2);
            SP[2] = new SqlParameter("@Month", Convert.ToInt32(ddlmonthgrid.SelectedValue));
            SP[3] = new SqlParameter("@Year", Convert.ToInt32(ddlyeargrid.SelectedValue));
            SP[4] = new SqlParameter("@Process", "Payable Days Sync");
            DataSet ds1 = SQLDBUtil.ExecuteDataset("sh_Paydayslastsynctime_Payabledays", SP);
            if (ds1.Tables[0].Rows.Count > 0)
            {
                lbllastsyncgrid.Text = ds1.Tables[0].Rows[0]["date"].ToString();
            }
            else
                lbllastsyncgrid.Text = String.Empty;
        }
        protected void gvunsync_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DropDownList ddlmonthgrid = (DropDownList)e.Row.FindControl("ddlmonthgrid");
                DropDownList ddlyeargrid = (DropDownList)e.Row.FindControl("ddlyeargrid");
                LinkButton lnkCompute = (LinkButton)e.Row.FindControl("lnkCompute");
                ddlyeargrid.SelectedValue = (e.Row.DataItem as DataRowView)["year"].ToString();
                ddlmonthgrid.SelectedValue = (e.Row.DataItem as DataRowView)["Month"].ToString();
                if ((e.Row.DataItem as DataRowView)["year"].ToString() == "")
                {
                    bindyear();
                    ddlyear.SelectedIndex = 0;
                }
                else
                {
                    ddlyear.SelectedValue = (e.Row.DataItem as DataRowView)["year"].ToString();
                }
                if ((e.Row.DataItem as DataRowView)["Month"].ToString() == "")
                {
                    bindyear();
                    ddlmonth.SelectedIndex = 0;
                }
                else
                {
                    ddlmonth.SelectedValue = (e.Row.DataItem as DataRowView)["Month"].ToString();
                }
            }
        }
        protected void btnempsearch_Click(object sender, EventArgs e)
        {
            objHrCommon.CurrentPage = taskpaging.CurrentPage;
            objHrCommon.PageSize = taskpaging.ShowRows;
            gdvAdjDiffDet.Visible = false;
            gdvMonthReport.Visible = false;
            gdvCurrentDetails.Visible = false;
            lblAdj.Visible = false;
            lblPaidDays.Visible = false;
            lblActualEligibility.Visible = false;
            taskpaging.CurrentPage = 1;
            Bindgrid(objHrCommon);
        }
        private void BindingMissingEmployees()
        {
            SqlParameter[] SP = new SqlParameter[7];
            SP[0] = new SqlParameter("@Year", Convert.ToInt32(ddlyear.SelectedValue));
            SP[1] = new SqlParameter("@Month", Convert.ToInt32(ddlmonth.SelectedValue));
            SP[2] = new SqlParameter("@CurrentPage", objHrCommon.CurrentPage);
            SP[3] = new SqlParameter("@PageSize", objHrCommon.PageSize);
            SP[4] = new SqlParameter("@NoOfRecords", System.Data.SqlDbType.Int);
            SP[4].Direction = ParameterDirection.Output;
            SP[5] = new SqlParameter("returnvalue", System.Data.SqlDbType.Int);
            SP[5].Direction = ParameterDirection.ReturnValue;
            SP[6] = new SqlParameter("@Siteid", ViewState["WSID"]);
            DataSet ds = SQLDBUtil.ExecuteDataset("HMS_Get_employeenotpresent", SP);
            if (ds.Tables[0].Rows.Count > 0)
            {
                gvmsngemp.DataSource = ds.Tables[0];
                gvmsngemp.DataBind();
                gvmsngemp.Visible = true;
                lblmsg.Visible = true;
                int totpage = Convert.ToInt32(SP[5].Value);
                int noofrec = Convert.ToInt32(SP[4].Value);
                objHrCommon.TotalPages = totpage;
                objHrCommon.NoofRecords = noofrec;
                Pagingmsg.Visible = true;
                Pagingmsg.Bind(objHrCommon.CurrentPage, objHrCommon.TotalPages, objHrCommon.NoofRecords, objHrCommon.PageSize);
            }
            else
            {
                gvmsngemp.DataSource = null;
                gvmsngemp.DataBind();
                lblmsg.Visible = true;
                gvmsngemp.Visible = false;
                Pagingmsg.Visible = false;
            }
        }
        protected void btnzeropayabledaysemp_Click(object sender, EventArgs e)
        {
            BindZeroPayableDaysEmployees();
        }
        private void BindZeroPayableDaysEmployees()
        {
            if (ddlmonth.SelectedIndex != 0)
            {
            }
            else
            {
                AlertMsg.MsgBox(Page, "Select Month", AlertMsg.MessageType.Warning);
                return;
            }
            if (Convert.ToInt32(ddlyear.SelectedValue) != 0)
            {
            }
            else
            {
                AlertMsg.MsgBox(Page, "Select Year", AlertMsg.MessageType.Warning);
                return;
            }
            objHrCommon.CurrentPage = PagingzeroPayabledaysemp.CurrentPage;
            objHrCommon.PageSize = PagingzeroPayabledaysemp.ShowRows;
            SqlParameter[] SP = new SqlParameter[7];
            SP[0] = new SqlParameter("@Year", Convert.ToInt32(ddlyear.SelectedValue));
            SP[1] = new SqlParameter("@Month", Convert.ToInt32(ddlmonth.SelectedValue));
            SP[2] = new SqlParameter("@CurrentPage", objHrCommon.CurrentPage);
            SP[3] = new SqlParameter("@PageSize", objHrCommon.PageSize);
            SP[4] = new SqlParameter("@NoOfRecords", System.Data.SqlDbType.Int);
            SP[4].Direction = ParameterDirection.Output;
            SP[5] = new SqlParameter("returnvalue", System.Data.SqlDbType.Int);
            SP[5].Direction = ParameterDirection.ReturnValue;
            SP[6] = new SqlParameter("@SiteId", ViewState["WSID"]);
            DataSet ds = SQLDBUtil.ExecuteDataset("HMS_Get_employeeZeroPayabledays", SP);
            if (ds.Tables[0].Rows.Count > 0)
            {
                gvzeroPayabledaysemp.DataSource = ds.Tables[0];
                lblmsg.Visible = true;
                gvzeroPayabledaysemp.Visible = true;
            }
            else
                gvzeroPayabledaysemp.DataSource = null;
            gvzeroPayabledaysemp.DataBind();
            int totpage = Convert.ToInt32(SP[5].Value);
            int noofrec = Convert.ToInt32(SP[4].Value);
            objHrCommon.TotalPages = totpage;
            objHrCommon.NoofRecords = noofrec;
            PagingzeroPayabledaysemp.Bind(objHrCommon.CurrentPage, objHrCommon.TotalPages, objHrCommon.NoofRecords, objHrCommon.PageSize);
        }
        protected void CalenderMonth(int EMPID, int year, int Month)
        {
            try
            {
                gdvAdjDiffDet.Visible = false;
                gdvMonthReport.Visible = false;
                gdvCurrentDetails.Visible = false;
                lblAdj.Visible = false;
                lblPaidDays.Visible = false;
                lblActualEligibility.Visible = false;
                string Name = null;
                int month = Convert.ToInt32(Month);
                int monthtext = month;
                int year1 = Convert.ToInt32(year);
                int yeartext = year; int year2 = year;
                int startdate = 1;
                if (startdate != 1)
                {
                    if (month == 1)
                    {
                        month = 12;
                        year = year - 1;
                        year2 = year + 1;
                    }
                    else
                        month = month - 1;
                }
                string st = month + "/" + startdate + "/" + year;
                DateTime dt = CODEUtility.ConvertToDate(st, DateFormat.MonthDayYear);// DateTime.ParseExact(st, "MM/dd/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                DateTime dtEnd = dt.AddMonths(1);
                int EmpNatureID = 0;
                int DepartmentID = 0, WorkSiteID = 0;
                DateTime StartDate = dt, EndDate = dtEnd;
                List<DateTime> dateList = new List<DateTime>();
                int DayInterval = 1;
                int TotalPages = 1;//returnVale
                int NoofRecords = 100;// return value
                int PageSize = taskpaging.ShowRows;
                int CurrentPage = taskpaging.CurrentPage;
                try
                {
                    if (Convert.ToInt32(ViewState["WSID"]) > 0)
                        WorkSiteID = Convert.ToInt32(ViewState["WSID"]);
                }
                catch { }
                DataSet dsEPMData = AttendanceDAC.HR_GetAttandanceStrip(EMPID, WorkSiteID, DepartmentID, EmpNatureID, StartDate, EndDate
                    , CurrentPage, PageSize, ref NoofRecords, ref TotalPages, Name);
                tblAtt.Rows.Clear();
                tblAtt.Style.Add("border", "solid red 1px");
                tblAtt.Style.Add("border-collapse", "collapse");
                //2 
                Boolean isFirst = true;
                TableRow tblHeadRow = new TableRow();
                TableRow tblDepartRow = new TableRow();
                tblRow = new TableRow();
                int DeptID = 0;
                Hashtable ht = new Hashtable();
                int WidthP = 30;
                int WidthPName = 300;
                foreach (DataRow drEMP in dsEPMData.Tables[2].Rows)
                {
                    tblHeadRow = new TableRow();
                    //nookesh start
                    if (isFirst)
                    {
                        TableRow rowNew = new TableRow();
                        tblAtt.Controls.Add(rowNew);
                        TableCell cellNew0 = new TableCell();
                        TableCell cellNew = new TableCell();
                        TableCell cellNew1 = new TableCell();
                        cellNew.ColumnSpan = 11;
                        cellNew1.ColumnSpan = 20;
                        rowNew.Style.Add("border", " solid navy 1px");
                        //cellNew.Style.Add("background-color", "#87cefa");
                        //cellNew1.Style.Add("background-color", "#87cefa");
                        cellNew.Style.Add("font-weight", "bold");
                        cellNew1.Style.Add("font-weight", "bold");
                        cellNew.Style.Add("Text-align", "Center");
                        cellNew1.Style.Add("Text-align", "Center");
                        for (int row = 0; row < 1; row++)
                        {
                            for (int col = 0; col < 3; col++)
                            {
                                if (col > 0)
                                {
                                    switch (Convert.ToInt32(monthtext))
                                    {
                                        case 1:
                                            cellNew0.Text = "".ToString();
                                            //int year1 = year + 1;                                         
                                            cellNew.Text = "January".ToString() + " " + year2;
                                            cellNew.ColumnSpan = 31;
                                            break;
                                        case 2:
                                            cellNew0.Text = "".ToString();
                                            cellNew.Text = "February".ToString() + " " + year;
                                            cellNew.ColumnSpan = 28;
                                            break;
                                        case 3:
                                            cellNew0.Text = "".ToString();
                                            cellNew.Text = "March".ToString() + " " + year;
                                            cellNew.ColumnSpan = 31;
                                            break;
                                        case 4:
                                            cellNew0.Text = "".ToString();
                                            cellNew.Text = "April".ToString() + " " + year;
                                            cellNew.ColumnSpan = 30;
                                            break;
                                        case 5:
                                            cellNew0.Text = "".ToString();
                                            cellNew.Text = "May".ToString() + year;
                                            cellNew.ColumnSpan = 31;
                                            break;
                                        case 6:
                                            cellNew0.Text = "".ToString();
                                            cellNew.Text = "June".ToString() + " " + year;
                                            cellNew.ColumnSpan = 30;
                                            break;
                                        case 7:
                                            cellNew0.Text = "".ToString();
                                            cellNew.Text = "July".ToString() + " " + year;
                                            cellNew.ColumnSpan = 31;
                                            break;
                                        case 8:
                                            cellNew0.Text = "".ToString();
                                            cellNew.Text = "Augest".ToString() + " " + year;
                                            cellNew.ColumnSpan = 31;
                                            break;
                                        case 9:
                                            cellNew0.Text = "".ToString();
                                            cellNew.Text = "September".ToString() + " " + year;
                                            cellNew.ColumnSpan = 30;
                                            break;
                                        case 10:
                                            cellNew0.Text = "".ToString();
                                            cellNew.Text = "October".ToString() + " " + year;
                                            cellNew.ColumnSpan = 31;
                                            break;
                                        case 11:
                                            cellNew0.Text = "".ToString();
                                            cellNew.Text = "November".ToString() + " " + year;
                                            cellNew.ColumnSpan = 30;
                                            break;
                                        case 12:
                                            cellNew0.Text = "".ToString();
                                            cellNew.Text = "December".ToString() + " " + year;
                                            cellNew.ColumnSpan = 31;
                                            break;
                                        default:
                                            cellNew.Text = "".ToString();
                                            break;
                                    }
                                }
                                else
                                    cellNew.Text = "".ToString();
                            }
                            rowNew.Controls.Add(cellNew0);
                            rowNew.Controls.Add(cellNew);
                            //rowNew.Controls.Add(cellNew1);
                            int x = CheckLeapYear(year2);
                            if (x == 1 && monthtext == 2)
                            {
                                cellNew.ColumnSpan = 29;
                                //cellNew1.ColumnSpan = 20;
                            }
                        }
                        //nookesh
                    }
                    tblRow = new TableRow();
                    tblDepartRow = null;
                    ht = new Hashtable();
                    if (isFirst)
                        // for Header
                        CellNameWriting_Head(ref tblHeadRow, WidthPName, "Name");
                    CellNameWriting(ref tblRow, WidthPName, drEMP["Name"].ToString());
                    StartDate = dt;
                    while (StartDate.AddDays(DayInterval - 1) < EndDate)
                    {
                        string stdt = StartDate.ToString();
                        string[] stm = stdt.ToString().Split('/');
                        stmonth = Convert.ToInt32(stm[0]);
                        string eddt = EndDate.ToString();
                        string[] edt = eddt.ToString().Split('/');
                        edmonth = Convert.ToInt32(edt[0]);
                        if (isFirst)
                        {
                            // for Header Dates
                            CellNameWriting_ForDates(ref tblHeadRow, WidthP, StartDate.Day.ToString());
                        }
                        //string filter = "Date > '" + daDateFrom + "' AND DateTo <= '" + daDateTo + "'";
                        try
                        {
                            DataRow[] drsAtt = dsEPMData.Tables[1].Select("Date = '" + StartDate + "' and EMPID='" + drEMP["ID"] + "'");
                            if (drsAtt.Length > 0)
                            {
                                switch (Convert.ToInt32(drsAtt[0]["Status"]))
                                {
                                    case 1:
                                        CellNameWriting_Red(ref tblRow, WidthP, drsAtt[0]["ShortName"].ToString(), false, true);
                                        break;
                                    case 2:
                                        if (Convert.ToInt32(drsAtt[0]["isOutTime"]) == 0)
                                            CellNameWriting_Green_P(ref tblRow, WidthP, drsAtt[0]["ShortName"].ToString(), false, true, false);
                                        else
                                            CellNameWriting_Green_P(ref tblRow, WidthP, drsAtt[0]["ShortName"].ToString(), false, true, true);
                                        break;
                                    case 7:
                                        CellNameWriting_Green(ref tblRow, WidthP, drsAtt[0]["ShortName"].ToString(), false, true);
                                        break;
                                    case 8:
                                        CellNameWriting_Green(ref tblRow, WidthP, drsAtt[0]["ShortName"].ToString(), false, true);
                                        break;
                                    case 9:
                                        CellNameWriting_Blue(ref tblRow, WidthP, drsAtt[0]["ShortName"].ToString(), false, true);
                                        break;
                                    default:
                                        CellNameWriting_Gray(ref tblRow, WidthP, drsAtt[0]["ShortName"].ToString(), false, true);
                                        break;
                                        ;
                                }
                                if (ht.ContainsKey(drsAtt[0]["Status"]))
                                    ht[drsAtt[0]["Status"]] = Convert.ToInt32(ht[drsAtt[0]["Status"]]) + 1;
                                else
                                    ht.Add(drsAtt[0]["Status"], 1);
                            }
                            else
                                CellNameWriting_Red(ref tblRow, WidthP, "-", false, true);
                            if (ht.ContainsKey(0))
                                ht[0] = Convert.ToInt32(ht[0]) + 1;
                            else
                                ht.Add(0, 1);
                            StartDate = StartDate.AddDays(DayInterval);
                            //dateList.Add(StartDate);
                        }
                        catch { }
                    }
                    if (isFirst)
                        CellNameWriting_Green(ref tblHeadRow, WidthP, "Scope", true);
                    string ValueNo = "0";
                    if (ht.ContainsKey(0))
                        ValueNo = ht[0].ToString();
                    CellNameWriting_Green(ref tblRow, WidthP, ValueNo, false);
                    foreach (DataRow drAM in dsEPMData.Tables[0].Rows)
                    {
                        if (isFirst)
                        {
                            // for Header
                            //CellNameWriting_ForDates(ref tblHeadRow, WidthP, drAM["Name"].ToString());
                            string Namestring = drAM["Name"].ToString();
                            switch (Convert.ToInt32(drAM["ID"]))
                            {
                                case 0:
                                    CellNameWriting_Green(ref tblHeadRow, WidthP, Namestring, true, false);
                                    break;
                                case 1:
                                    CellNameWriting_Red(ref tblHeadRow, WidthP, Namestring, true, false);
                                    break;
                                case 2:
                                    CellNameWriting_Green(ref tblHeadRow, WidthP, Namestring, true, false);
                                    break;
                                case 7:
                                    CellNameWriting_Green(ref tblHeadRow, WidthP, Namestring, true, false);
                                    break;
                                case 8:
                                    CellNameWriting_Green(ref tblHeadRow, WidthP, Namestring, true, false);
                                    break;
                                case 9:
                                    CellNameWriting_Blue(ref tblHeadRow, WidthP, Namestring, true, false);
                                    break;
                                default:
                                    CellNameWriting_Gray(ref tblHeadRow, WidthP, Namestring, true, false);
                                    break;
                                    ;
                            }
                        }
                        ValueNo = "0";
                        if (ht.ContainsKey(drAM["ID"]))
                            ValueNo = ht[drAM["ID"]].ToString();
                        switch (Convert.ToInt32(drAM["ID"]))
                        {
                            case 0:
                                CellNameWriting_Green(ref tblRow, WidthP, ValueNo, false, false);
                                break;
                            case 1:
                                CellNameWriting_Red(ref tblRow, WidthP, ValueNo, false, false);
                                break;
                            case 2:
                                CellNameWriting_Green(ref tblRow, WidthP, ValueNo, false, false);
                                break;
                            case 7:
                                CellNameWriting_Green(ref tblRow, WidthP, ValueNo, false, false);
                                break;
                            case 8:
                                CellNameWriting_Green(ref tblRow, WidthP, ValueNo, false, false);
                                break;
                            case 9:
                                CellNameWriting_Blue(ref tblRow, WidthP, ValueNo, false, false);
                                break;
                            default:
                                CellNameWriting_Gray(ref tblRow, WidthP, ValueNo, false, false);
                                break;
                                ;
                        }
                    }
                    if (isFirst)
                        tblAtt.Rows.Add(tblHeadRow);
                    if (tblDepartRow != null)
                        tblAtt.Rows.Add(tblDepartRow);
                    tblAtt.Rows.Add(tblRow);
                    //string dtl = ddlMonth.SelectedItem.Value + "/0" + startdate + "/" + ddlYear.SelectedItem.Value;
                    isFirst = false;
                }
                // string val = tblAtt.g.ToString();
                // HtmlElementCollection tables = this.WB.Document.GetElementsByTagName("table");
            }
            catch { }
        }
        public int CheckLeapYear(int intyear)
        {
            if (intyear % 4 == 0 && intyear % 100 != 0 || intyear % 400 == 0)
                return 1; // It is a leap year
            else
                return 0; // Not a leap year
        }
        private void CellNameWriting_Head(ref TableRow tblRow, int Width, string NameCell)
        {
            TableCell tcName = new TableCell();
            tcName.Text = NameCell;
            tcName.Style.Add("font-weight", "bold");
            tcName.Style.Add("border", " solid navy 1px");
            tcName.Width = Width;
            tblRow.Cells.Add(tcName);
        }
        private void CellNameWriting(ref TableRow tblRow, int Width, string NameCell)
        {
            TableCell tcName = new TableCell();
            tcName.Text = NameCell;
            tcName.Style.Add("border", " solid navy 1px");
            //tcName.Style.Add("font-weight", "bold");
            tcName.Width = Width;
            tblRow.Cells.Add(tcName);
        }
        private void CellNameWriting_ForDates(ref TableRow tblRow, int Width, string NameCell)
        {
            TableCell tcName = new TableCell();
            tcName.Text = NameCell;
            days = Convert.ToInt32(NameCell);
            tcName.Style.Add("font-weight", "bold");
            tcName.Style.Add("border", " solid navy 1px");
            //tcName.Style.Add("background-color", "#87cefa");
            if (stmonth == edmonth)
            {
                tblAtt.Style.Add("background-color", "light Blue");
            }
            else
                if (days > 20)
                    tcName.Style.Add("background-color", "#CCCCC");
                else
                    tcName.Style.Add("background-color", "#87cefa");
            tcName.Style.Add("text-align", "center");
            tcName.Width = Width;
            tblRow.Cells.Add(tcName);
        }
        private void CellNameWriting_Red(ref TableRow tblRow, int Width, string NameCell, Boolean IsHead, Boolean IsAtt)
        {
            TableCell tcName = new TableCell();
            tcName.Text = NameCell;
            tcName.Style.Add("color", "red");
            if (IsHead)
                tcName.Style.Add("font-weight", "bold");
            tcName.Style.Add("border", " solid navy 1px");
            if (IsAtt)
                if (stmonth == edmonth)
                {
                    tblAtt.Style.Add("background-color", "light Blue");
                }
                else
                    tcName.Style.Add("background-color", "#87cefa");
            else
                if (days > 20)
                    tcName.Style.Add("background-color", "#CCCCC");
                else
                    tcName.Style.Add("background-color", "#87cefa");
            tcName.Style.Add("text-align", "center");
            tcName.Width = Width;
            tblRow.Cells.Add(tcName);
        }
        private void CellNameWriting_Green(ref TableRow tblRow, int Width, string NameCell, Boolean IsHead)
        {
            TableCell tcName = new TableCell();
            tcName.Text = NameCell;
            tcName.Style.Add("color", "green");
            if (IsHead)
                if (stmonth == edmonth)
                {
                    tblAtt.Style.Add("background-color", "light Blue");
                }
                else
                    if (days > 20)
                        tcName.Style.Add("background-color", "#CCCCC");
                    else
                        tcName.Style.Add("background-color", "#87cefa");
            tcName.Style.Add("border", " solid navy 1px");
            tcName.Style.Add("text-align", "center");
            tcName.Width = Width;
            tblRow.Cells.Add(tcName);
        }
        private void CellNameWriting_Green(ref TableRow tblRow, int Width, string NameCell, Boolean IsHead, Boolean IsAtt)
        {
            TableCell tcName = new TableCell();
            tcName.Text = NameCell;
            tcName.Style.Add("color", "green");
            if (IsHead)
                tcName.Style.Add("font-weight", "bold");
            tcName.Style.Add("border", " solid navy 1px");
            if (IsAtt)
                if (stmonth == edmonth)
                {
                    tblAtt.Style.Add("background-color", "light Blue");
                }
                else
                    if (days > 20)
                        tcName.Style.Add("background-color", "#CCCCC");
                    else
                        tcName.Style.Add("background-color", "#87cefa");
            else
                tcName.Style.Add("background-color", "#A1F9DB");
            tcName.Style.Add("text-align", "center");
            tcName.Width = Width;
            tblRow.Cells.Add(tcName);
        }
        private void CellNameWriting_Green_P(ref TableRow tblRow, int Width, string NameCell, Boolean IsHead, Boolean IsAtt, Boolean isOuttym)
        {
            TableCell tcName = new TableCell();
            tcName.Text = NameCell;
            if (isOuttym)
                tcName.Style.Add("color", "green");
            else
            {
                tcName.Style.Add("color", "Navy"); tcName.Style.Add("font-weight", "bold");
            }
            if (IsHead)
                tcName.Style.Add("font-weight", "bold");
            tcName.Style.Add("border", " solid navy 1px");
            if (IsAtt)
                if (stmonth == edmonth)
                {
                    tblAtt.Style.Add("background-color", "light Blue");
                }
                else
                    if (days > 20)
                        tcName.Style.Add("background-color", "#CCCCC");
                    else
                        tcName.Style.Add("background-color", "#87cefa");
            else
                tcName.Style.Add("background-color", "#A1F9DB");
            tcName.Style.Add("text-align", "center");
            tcName.Width = Width;
            tblRow.Cells.Add(tcName);
        }
        private void CellNameWriting_Blue(ref TableRow tblRow, int Width, string NameCell, Boolean IsHead, Boolean IsAtt)
        {
            TableCell tcName = new TableCell();
            tcName.Text = NameCell;
            tcName.Style.Add("color", "blue");
            if (IsHead)
                tcName.Style.Add("font-weight", "bold");
            tcName.Style.Add("border", " solid navy 1px");
            if (IsAtt)
                if (stmonth == edmonth)
                {
                    tblAtt.Style.Add("background-color", "light Blue");
                }
                else
                    if (days > 20)
                        tcName.Style.Add("background-color", "#CCCCC");
                    else
                        tcName.Style.Add("background-color", "#87cefa");
            else
                tcName.Style.Add("background-color", "#A1F9DB");
            tcName.Style.Add("text-align", "center");
            tcName.Width = Width;
            tblRow.Cells.Add(tcName);
        }
        private void CellNameWriting_Gray(ref TableRow tblRow, int Width, string NameCell, Boolean IsHead, Boolean IsAtt)
        {
            TableCell tcName = new TableCell();
            tcName.Text = NameCell;
            tcName.Style.Add("color", "Ornge");
            if (IsHead)
                tcName.Style.Add("font-weight", "bold");
            tcName.Style.Add("border", " solid navy 1px");
            if (IsAtt)
                if (stmonth == edmonth)
                {
                    tblAtt.Style.Add("background-color", "light Blue");
                }
                else
                    if (days > 20)
                        tcName.Style.Add("background-color", "#CCCCC");
                    else
                        tcName.Style.Add("background-color", "#87cefa");
            else
                tcName.Style.Add("background-color", "#A1F9DB");
            tcName.Style.Add("text-align", "center");
            tcName.Width = Width;
            tblRow.Cells.Add(tcName);
        }
        protected void gdvMonthReport_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.RowIndex >= 0)
                {
                    if (gdvMonthReport.DataSource != null)
                    {
                        int i = 1;
                        foreach (DataRow drEMP in dsEPMData.Tables[1].Rows)
                        {
                            int Pay = int.Parse(drEMP["Pay"].ToString());
                            if (Pay == 1)
                            {
                                e.Row.Cells[i + 1].BackColor = Color.Green;
                            }
                            else if (Pay == 0)
                            {
                                e.Row.Cells[i + 1].BackColor = Color.Orange;
                            }
                            i = i + 1;
                        }
                    }
                }
            }
        }
        protected void gdvCurrentDetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.RowIndex >= 0)
                {
                    if (gdvCurrentDetails.DataSource != null)
                    {
                        int i = 1;
                        foreach (DataRow drEMP in dsCurrentDetails.Tables[1].Rows)
                        {
                            int Pay = int.Parse(drEMP["Pay"].ToString());
                            if (Pay == 1)
                            {
                                e.Row.Cells[i + 1].BackColor = Color.Green;
                            }
                            else if (Pay == 0)
                            {
                                e.Row.Cells[i + 1].BackColor = Color.Orange;
                            }
                            i = i + 1;
                        }
                    }
                }
            }
        }
    }
}