using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using TPABALI.Web;
using AECLOGIC.HMS.BLL;
using AECLOGIC.ERP.COMMON;
using AECLOGIC.ERP.HMS.HRClasses;
using Aeclogic.Common.DAL;
using System.Collections;
namespace AECLOGIC.ERP.HMSV1
{
    public partial class MonthlyPaymentsProcessedV4V1 : AECLOGIC.ERP.COMMON.WebFormMaster
    {
        AECLOGIC.HMS.BLL.AttendanceDAC objEmployee = new AttendanceDAC();
        AttendanceDAC objRights = new AttendanceDAC();
        HRCommon objHrCommon = new HRCommon();
        static int SearchCompanyID;
        static int Siteid, SiteidGrid, MonthGrid, YearGrid;
        int mid = 0;
        bool viewall;
        string menuname;
        string menuid;
        bool Editable;
        DataSet dsBind = new DataSet();
        static int scmonth = 0, scyear = 0;
        static string lastsync = "";
        int OrderID = 0, Direction = 0; TableRow tblRow; int EMPIDPram = 0; int stmonth = 0; int edmonth = 0; static int lnkM;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                SearchCompanyID = Convert.ToInt32(Session["CompanyID"]);
                if (!IsPostBack)
                {
                    //  lnkViewAttendance.Visible = false;
                    try
                    {
                        try
                        {
                            ViewState["WSID"] = 0;
                            if (Convert.ToInt32(Session["RoleId"].ToString()) == 7)
                            {
                                try
                                {
                                    DataSet ds = clViewCPRoles.HR_DailyAttStatus(Convert.ToInt32(Session["UserId"]));
                                    ViewState["WSID"] = ds.Tables[0].Rows[0]["ID"].ToString();
                                }
                                catch { }
                            }
                        }
                        catch { }
                        BindYears();
                        BindPager();
                        bool Access = Convert.ToBoolean(ViewState["ViewAll"]);
                        OrderID = 2;
                        ViewState["OrderID"] = 2;
                        DataSet startdate = AttendanceDAC.GetStartDate();
                        ViewState["startdate"] = 1;
                        ViewState["enddate"] = 1;
                        DataSet enddate = AttendanceDAC.GetEndDate();
                        ViewState["startdate"] = startdate.Tables[0].Rows[0][0].ToString();
                        ViewState["enddate"] = enddate.Tables[0].Rows[0][0].ToString();
                        if (Access == true)
                        {
                        }
                        else
                        {
                            int EmpID = Convert.ToInt32(Session["UserId"]);
                            EmpListPaging.Visible = false;
                        }
                        scmonth = Convert.ToInt32(ddlMonth.SelectedValue);
                        scyear = Convert.ToInt32(ddlYear.SelectedValue);
                    }
                    catch
                    {
                        Response.Redirect("Logon.aspx");
                    }
                    // SyncCount();
                }
            }
            catch (Exception ex)
            {
                clsErrorLog.HMSEventLog(ex, "Salaries", "Page_Load", "001");
            }
        }
        protected override void OnInit(EventArgs e)
        {
            ModuleID = 1;
            base.OnInit(e);
            EmpListPaging.FirstClick += new Paging.PageFirst(EmpListPaging_FirstClick);
            EmpListPaging.PreviousClick += new Paging.PagePrevious(EmpListPaging_FirstClick);
            EmpListPaging.NextClick += new Paging.PageNext(EmpListPaging_FirstClick);
            EmpListPaging.LastClick += new Paging.PageLast(EmpListPaging_FirstClick);
            EmpListPaging.ChangeClick += new Paging.PageChange(EmpListPaging_FirstClick);
            EmpListPaging.ShowRowsClick += new Paging.ShowRowsChange(EmpListPaging_ShowRowsClick);
            EmpListPaging.CurrentPage = 1;
            Paging1.FirstClick += new Paging.PageFirst(EmpListPaging_FirstClick);
            Paging1.PreviousClick += new Paging.PagePrevious(EmpListPaging_FirstClick);
            Paging1.NextClick += new Paging.PageNext(EmpListPaging_FirstClick);
            Paging1.LastClick += new Paging.PageLast(EmpListPaging_FirstClick);
            Paging1.ChangeClick += new Paging.PageChange(EmpListPaging_FirstClick);
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
            pngBasicInActive.FirstClick += new Paging.PageFirst(BasicInactivePaging_FirstClick);
            pngBasicInActive.NextClick += new Paging.PageNext(BasicInactivePaging_FirstClick);
            pngBasicInActive.PreviousClick += new Paging.PagePrevious(BasicInactivePaging_FirstClick);
            pngBasicInActive.ChangeClick += new Paging.PageChange(BasicInactivePaging_FirstClick);
            pngBasicInActive.LastClick += new Paging.PageLast(BasicInactivePaging_FirstClick);
            pngBasicInActive.ShowRowsClick += new Paging.ShowRowsChange(BasicInactivePaging_ShowRowsClick);
            pngBasicInActive.CurrentPage = 1;
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
        void BasicInactivePaging_ShowRowsClick(object sender, EventArgs e)
        {
            pngBasicInActive.CurrentPage = 1;
            BindBasicInActiveEmployees();
        }
        void BasicInactivePaging_FirstClick(object sender, EventArgs e)
        {
            objHrCommon.CurrentPage = pngBasicInActive.CurrentPage;
            objHrCommon.PageSize = pngBasicInActive.ShowRows;
            BindBasicInActiveEmployees();
        }
        void EmpListPaging_ShowRowsClick(object sender, EventArgs e)
        {
            EmpListPaging.CurrentPage = 1;
            Paging1.CurrentPage = 1;
            BindPager();
            if (gvPaySlip.Rows.Count > 0)
                EmployeBind(objHrCommon);
        }
        void EmpListPaging_FirstClick(object sender, EventArgs e)
        {
            BindPager();
            if (gvPaySlip.Rows.Count > 0)
                EmployeBind(objHrCommon);
        }
        void BindPager()
        {
            objHrCommon.CurrentPage = Paging1.CurrentPage;
            objHrCommon.PageSize = Paging1.ShowRows;
            //EmployeBind(objHrCommon);
            lnkunsync_Click(objHrCommon);
        }
        //protected void btnmisngemp_Click(object sender, EventArgs e)
        //{
        //    //BindPagermsg();
        //}
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute]
        public static string[] GetCompletionList(string prefixText, int count, string contextKey)
        {
            DataSet ds = AttendanceDAC.GetsalariesGoogleSearchWorkSite(prefixText);
            return ConvertStingArray(ds);
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute]
        public static string[] GetCompletionListdept(string prefixText, int count, string contextKey)
        {
            DataSet ds = AttendanceDAC.GetDepartmentGoogleSerc(prefixText, SearchCompanyID, Siteid);
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
        private void PaybleDaysStatusChecking(int siteidgrid, int monthgrid, int yeargrid)
        {
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[4];
                sqlParams[0] = new SqlParameter("@wsid", siteidgrid);
                sqlParams[1] = new SqlParameter("@month", monthgrid);
                sqlParams[2] = new SqlParameter("@year", yeargrid);
                sqlParams[3] = new SqlParameter("@empid", DBNull.Value);
                DataSet dss = SQLDBUtil.ExecuteDataset("sh_payabledaysstatus", sqlParams);
                if (dss.Tables[0].Rows[0][0].ToString() == "Already Synchronized")
                {
                    objHrCommon.Month = monthgrid;
                    objHrCommon.Year = yeargrid;
                    objHrCommon.SiteID = siteidgrid;
                    EmployeBind(objHrCommon);
                }
                else
                {
                    gvPaySlip.DataSource = null;
                    gvPaySlip.DataBind();
                    EmpListPaging.Visible = false;
                    AlertMsg.MsgBox(Page, dss.Tables[0].Rows[0][0].ToString(), AlertMsg.MessageType.Warning);
                }
            }
            catch (Exception ex)
            {
                clsErrorLog.HMSEventLog(ex, "Salaries", "PaybleDaysStatusChecking", "005");
            }
        }
        public void BindEmp(int EmpID, int Month, int Year)
        {
            AttendanceDAC ADAC = new AttendanceDAC();
            DataSet ds = ADAC.HR_EmpSalriesListByEmpID(EmpID, Month, Year, objHrCommon);
            gvPaySlip.DataSource = ds;
            gvPaySlip.DataBind(); try { gvPaySlip.FooterRow.Visible = false; }
            catch { }
            EmpListPaging.Visible = false;
        }
        protected void btnempsearch_Click(object sender, EventArgs e)
        {
            try
            {
                EmpListPaging.CurrentPage = 1;
                EmployeBind(objHrCommon);
            }
            catch { }
        }
        void EmployeBind(HRCommon objHrCommon)
        {
            try
            {
                objHrCommon.PageSize = EmpListPaging.ShowRows;
                objHrCommon.CurrentPage = EmpListPaging.CurrentPage;
                int Month = MonthGrid;
                int Year = YearGrid;
                string stDate, enddate;
                stDate = 01 + "/" + Month + "/" + Year;
                int lastdayinmonth = DateTime.DaysInMonth(Year, Month);
                enddate = lastdayinmonth + "/" + Month + "/" + Year;
                DateTime startdate = CodeUtilHMS.ConvertToDate(stDate, CodeUtilHMS.DateFormat.DayMonthYear);
                DateTime EndDate = CodeUtilHMS.ConvertToDate(enddate, CodeUtilHMS.DateFormat.DayMonthYear);
                gvPaySlip.DataSource = null;
                gvPaySlip.DataBind();
                string Name = txtEmpName.Text;
                gvPaySlip.Visible = true;
                int EmpID = 0;
                if (txtEmpName.Text == "" || txtEmpName.Text == null)
                {
                    txtEmpNameHidden.Value = "";
                }
                //if (txtEmpID.Text != "" || txtEmpID.Text != string.Empty)
                //{
                //    EmpID = Convert.ToInt32(txtEmpID.Text);
                //}
                else if (txtEmpName.Text != "" || txtEmpName.Text != null)
                {
                    EmpID = Convert.ToInt32(txtEmpNameHidden.Value == "" ? "0" : txtEmpNameHidden.Value);
                }
                else
                {
                    EmpID = 0;
                }
                OrderID = Convert.ToInt32(ViewState["OrderID"]);
                dsBind = USP_Salaries_All_4(objHrCommon, startdate, EndDate, EmpID);
                ViewState["NoOfRecords"] = objHrCommon.NoofRecords;
                if (dsBind != null && dsBind.Tables.Count != 0 && dsBind.Tables[0].Rows.Count > 0 && dsBind.Tables[0].Rows[0][0].ToString() != "SP_SalariesXXXXXXX reports Error in Process - Consult Technical Resource")
                {
                    gvPaySlip.DataSource = dsBind;
                    EmpListPaging.Bind(objHrCommon.CurrentPage, objHrCommon.TotalPages, objHrCommon.NoofRecords, objHrCommon.PageSize);
                    EmpListPaging.Visible = true;
                    dvview.Visible = true;
                    btnApprove.Visible = true;
                }
                else
                {
                    dvview.Visible = false;
                    gvPaySlip.EmptyDataText = "No Records Found";
                    EmpListPaging.Visible = false;
                    // AlertMsg.MsgBox(Page, "SP_SalariesXXXXXXX reports Error in Process - Consult Technical Resource");
                    AlertMsg.MsgBox(Page, "No Records Found", AlertMsg.MessageType.Warning);
                    btnApprove.Visible = false;
                }
                gvPaySlip.DataBind();
            }
            catch (Exception e)
            {
               // throw e;
            }
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionList_EmpName(string prefixText, int count, string contextKey)
        {
            DataSet ds = new DataSet();
            if (lastsync != "")
                ds = AttendanceDAC.HMS_Service_DLL_Employee_By_WS_Dept_googlesearch_AttSal(prefixText.Trim(), SiteidGrid, 0, scmonth, scyear);
            else
                ds = AttendanceDAC.HMS_Service_DLL_Employee_By_WS_Dept_googlesearch_AttPay(prefixText.Trim(), SiteidGrid, 0, scmonth, scyear);
            DataTable dt = ds.Tables[0];
            List<string> items = new List<string>(count);
            var rtval = new Dictionary<string, string>();
            foreach (DataRow row in dt.Rows)
            {
                string str = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(row["NAME"].ToString(), row["ID"].ToString());
                items.Add(str);
            }
            return items.ToArray(); ;// txtItems.ToArray();
        }
        public void BindYears()
        {
            FIllObject.FillDropDown(ref ddlYear, "HMS_YearWise");
            DataSet ds = AttendanceDAC.GetCalenderYear();
            if (ds.Tables[0].Rows[0]["PreviousMonth"].ToString() == "0")
            {
                ddlMonth.SelectedValue = "12";
                int CurrentYear = Convert.ToInt32(ds.Tables[0].Rows[0]["CurrentYear"]);
                int PreviousYear = CurrentYear - 1;
                ddlYear.Items.FindByValue(PreviousYear.ToString()).Selected = true;
            }
            //if we are in same year and same month
            else
            {
                ddlMonth.SelectedValue = ds.Tables[0].Rows[0]["PreviousMonth"].ToString();
                if (ddlYear.Items.FindByValue(ds.Tables[0].Rows[0]["CurrentYear"].ToString()) != null)
                {
                    ddlYear.Items.FindByValue(ds.Tables[0].Rows[0]["CurrentYear"].ToString()).Selected = true;
                }
                else
                {
                    ddlYear.SelectedIndex = 0;
                    //ddlYear.Items.Count - 1
                }
            }
            //#endregion set defalult month and year
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
                    ddlYear.SelectedIndex = 0;
                }
                else
                {
                    ddlYear.SelectedValue = (e.Row.DataItem as DataRowView)["year"].ToString();
                }
                if ((e.Row.DataItem as DataRowView)["Month"].ToString() == "")
                {
                    bindyear();
                    ddlMonth.SelectedIndex = 0;
                }
                else
                {
                    ddlMonth.SelectedValue = (e.Row.DataItem as DataRowView)["Month"].ToString();
                }
            }
        }
        public DataSet bindyear()
        {
            FIllObject.FillDropDown(ref ddlYear, "HMS_YearWise");
            DataSet ds = AttendanceDAC.GetCalenderYear();
            if (ds.Tables[0].Rows[0]["PreviousMonth"].ToString() == "0")
            {
                ddlMonth.SelectedValue = "12";
                int CurrentYear = Convert.ToInt32(ds.Tables[0].Rows[0]["CurrentYear"]);
                int PreviousYear = CurrentYear - 1;
                ddlYear.Items.FindByValue(PreviousYear.ToString()).Selected = true;
            }
            //if we are in same year and same month
            else
            {
                ddlMonth.SelectedValue = ds.Tables[0].Rows[0]["PreviousMonth"].ToString();
                if (ddlYear.Items.FindByValue(ds.Tables[0].Rows[0]["CurrentYear"].ToString()) != null)
                {
                    ddlYear.Items.FindByValue(ds.Tables[0].Rows[0]["CurrentYear"].ToString()).Selected = true;
                }
                else
                {
                    ddlYear.SelectedIndex = 0;
                    //ddlYear.Items.Count - 1
                }
            }
            DataSet ds1 = SqlHelper.ExecuteDataset("HMS_YearWise");
            return ds1;
        }
        private DataSet USP_Salaries_All_4(HRCommon objHrCommon, DateTime startdate, DateTime enddate, int Empid)
        {
            DataSet ds = null;
            try
            {
                DataSet dss = SQLDBUtil.ExecuteDataset("sh_dynamicviewscreation_rev4");
                if (dss.Tables.Count > 0)
                {
                    SqlParameter[] sqlParams = new SqlParameter[8];
                    sqlParams[0] = new SqlParameter("@CurrentPage", objHrCommon.CurrentPage);
                    sqlParams[1] = new SqlParameter("@PageSize", objHrCommon.PageSize);
                    sqlParams[2] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
                    sqlParams[2].Direction = ParameterDirection.ReturnValue;
                    sqlParams[3] = new SqlParameter("@NoofRecords", System.Data.SqlDbType.Int);
                    sqlParams[3].Direction = ParameterDirection.Output;
                    sqlParams[4] = new SqlParameter("@empid", Empid);
                    sqlParams[5] = new SqlParameter("@paystartdate", startdate);
                    sqlParams[6] = new SqlParameter("@payenddate", enddate);
                    sqlParams[7] = new SqlParameter("@WSID", SiteidGrid);
                    ds = SQLDBUtil.ExecuteDataset("USP_Salaries_all_Rev4", sqlParams);
                    objHrCommon.NoofRecords = (int)sqlParams[3].Value;
                    objHrCommon.TotalPages = (int)sqlParams[2].Value;
                }
            }
            catch (Exception ex) { }
            return ds;
        }
        decimal TotalSpecialAmount = 0;
        protected string GetOTNetAmount(decimal Price)
        {
            string amt = string.Empty;
            Price = Convert.ToDecimal(Price.ToString("N2"));
            TotalOTAmount += Price;
            if (Price != 0)
            {
                amt = Price.ToString("N2");
            }
            else
            {
                amt = "0";
            }
            return amt;
        }
        decimal TotalOTAmount = 0;
        protected string GetOTTotalAmount()
        {
            return TotalOTAmount.ToString("N2");
        }
        decimal TotalAmount = 0;
        protected string GetNetAmount(decimal Price)
        {
            string amt = string.Empty;
            Price = Convert.ToDecimal(Price.ToString("N2"));
            TotalAmount += Price;
            if (Price != 0)
            {
                amt = Price.ToString("N2");
            }
            else
            {
                amt = "0";
            }
            return amt;
        }
        protected string GetAmount()
        {
            return TotalAmount.ToString("N2");
        }
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (GridViewRow grv in gvPaySlip.Rows)
                {
                    TextBox lblSpecial_Foot = (TextBox)grv.FindControl("lblSpecial");//PaySlipID // lblPaySlipID
                    Label lblPaySlipID = (Label)grv.FindControl("lblPaySlipID");//PaySlipID // lblPaySlipID
                    AttendanceDAC.HMS_EMPUpdateSalarySpecial(Convert.ToInt32(lblPaySlipID.Text), Convert.ToDecimal(lblSpecial_Foot.Text), Convert.ToInt32(Session["UserId"]));
                }
                AlertMsg.MsgBox(Page, "Submited", AlertMsg.MessageType.Success);
                // btnSearch_Click(sender, e);
            }
            catch
            {
            }
        }
        public bool GetStatus(string paymentStatus)
        {
            if (paymentStatus == "Paid")
            {
                return false;
            }
            else
                return true;
        }
        protected string GetSpecialTotalAmount()
        {
            return TotalSpecialAmount.ToString("N2");
        }
        protected string GetSpecialNetAmount(decimal Price)
        {
            string amt = string.Empty;
            Price = Convert.ToDecimal(Price.ToString("N2"));
            TotalSpecialAmount += Price;
            if (Price != 0)
            {
                amt = Price.ToString("N2");
            }
            else
            {
                amt = "0";
            }
            return amt;
        }
        decimal TotalAbsPAmount = 0;
        protected string GetAbsPTotalAmount()
        {
            return TotalAbsPAmount.ToString("N2");
        }
        protected string GetAbsPNetAmount(decimal Price)
        {
            string amt = string.Empty;
            Price = Convert.ToDecimal(Price.ToString("N2"));
            TotalAbsPAmount += Price;
            if (Price != 0)
            {
                amt = Price.ToString("N2");
            }
            else
            {
                amt = "0";
            }
            return amt;
        }
        decimal TotalEmpPAmount = 0;
        protected string GetEmpPTotalAmount()
        {
            return TotalEmpPAmount.ToString("N2");
        }
        protected string GetEmPPNetAmount(decimal Price)
        {
            string amt = string.Empty;
            Price = Convert.ToDecimal(Price.ToString("N2"));
            TotalEmpPAmount += Price;
            if (Price != 0)
            {
                amt = Price.ToString("N2");
            }
            else
            {
                amt = "0";
            }
            return amt;
        }
        //Grid Total For  Contributions
        decimal TotalContributions = 0;
        protected string GetContributions()
        {
            return TotalContributions.ToString("N2");
        }
        protected string GetContributionsAmount(decimal Price)
        {
            string amt = string.Empty;
            Price = Convert.ToDecimal(Price.ToString("N2"));
            TotalContributions += Price;
            if (Price != 0)
            {
                amt = Price.ToString("N2");
            }
            else
            {
                amt = "0";
            }
            return amt;
        }
        //Grid Total For  DeductSatatutory
        decimal TotalDeductSatatutory = 0;
        protected string GetDeductSatatutory()
        {
            return TotalDeductSatatutory.ToString("N2");
        }
        protected string GetDeductSatatutoryAmount(decimal Price)
        {
            string amt = string.Empty;
            Price = Convert.ToDecimal(Price.ToString("N2"));
            TotalDeductSatatutory += Price;
            if (Price != 0)
            {
                amt = Price.ToString("N2");
            }
            else
            {
                amt = "0";
            }
            return amt;
        }
        //Grid Total For  TDS
        decimal TotalTDS = 0;
        protected string GetTDS()
        {
            return TotalTDS.ToString("N2");
        }
        protected string GetTDSAmount(decimal Price)
        {
            string amt = string.Empty;
            Price = Convert.ToDecimal(Price.ToString("N2"));
            TotalTDS += Price;
            if (Price != 0)
            {
                amt = Price.ToString("N2");
            }
            else
            {
                amt = "0";
            }
            return amt;
        }
        //Grid Total For  PF
        decimal TotalPF = 0;
        protected string GetPF()
        {
            return TotalPF.ToString("N2");
        }
        protected string GetPFAmount(decimal Price)
        {
            string amt = string.Empty;
            Price = Convert.ToDecimal(Price.ToString("N2"));
            TotalPF += Price;
            if (Price != 0)
            {
                amt = Price.ToString("N2");
            }
            else
            {
                amt = "0";
            }
            return amt;
        }
        //Grid Total For  ESI
        decimal TotalESI = 0;
        protected string GetESI()
        {
            return TotalESI.ToString("N2");
        }
        protected string GetESIAmount(decimal Price)
        {
            string amt = string.Empty;
            Price = Convert.ToDecimal(Price.ToString("N2"));
            TotalESI += Price;
            if (Price != 0)
            {
                amt = Price.ToString("N2");
            }
            else
            {
                amt = "0";
            }
            return amt;
        }
        //Grid Total For  PT
        decimal TotalPT = 0;
        protected string GetPT()
        {
            return TotalPT.ToString("N2");
        }
        protected string GetPTAmount(decimal Price)
        {
            string amt = string.Empty;
            Price = Convert.ToDecimal(Price.ToString("N2"));
            TotalPT += Price;
            if (Price != 0)
            {
                amt = Price.ToString("N2");
            }
            else
            {
                amt = "0";
            }
            return amt;
        }
        decimal TotAdvance = 0;
        protected string GetTotAdvance()
        {
            return TotAdvance.ToString("N2");
        }
        protected string GetAdvance(decimal Advance)
        {
            string amt = string.Empty;
            Advance = Convert.ToDecimal(Advance.ToString("N2"));
            TotAdvance += Advance;
            if (Advance != 0)
            {
                amt = Advance.ToString("N2");
            }
            else
            {
                amt = "0";
            }
            return amt;
        }
        decimal TotMobileBill = 0;
        protected string GetTotMobileBill()
        {
            return TotMobileBill.ToString("N2");
        }
        protected string GetMobileBill(decimal Mobile)
        {
            string amt = string.Empty;
            Mobile = Convert.ToDecimal(Mobile.ToString("N2"));
            TotMobileBill += Mobile;
            if (Mobile != 0)
            {
                amt = Mobile.ToString("N2");
            }
            else
            {
                amt = "0";
            }
            return amt;
        }
        protected void btnApprove_Click(object sender, EventArgs e)
        {
            try
            {
                string stDate, enddate;
                stDate = 01 + "/" + MonthGrid + "/" + YearGrid;
                int lastdayinmonth = DateTime.DaysInMonth(YearGrid, MonthGrid);
                enddate = lastdayinmonth + "/" + MonthGrid + "/" + YearGrid;
                // objHrCommon.SiteID = Convert.ToInt32(ddlworksite.SelectedValue);
                DateTime startdate = CodeUtilHMS.ConvertToDate(stDate, CodeUtilHMS.DateFormat.DayMonthYear);
                DateTime EndDate = CodeUtilHMS.ConvertToDate(enddate, CodeUtilHMS.DateFormat.DayMonthYear);
                USP_Salaries_All_Save4(startdate, EndDate, "");
                SqlParameter[] SP = new SqlParameter[6];
                SP[0] = new SqlParameter("@Userid", Convert.ToInt32(Session["UserId"]));
                SP[1] = new SqlParameter("@wsid", SiteidGrid);
                SP[2] = new SqlParameter("@fcase", 1);
                SP[3] = new SqlParameter("@Year", YearGrid);
                SP[4] = new SqlParameter("@Process", "Salary Caluculation");
                SP[5] = new SqlParameter("@Month", MonthGrid);
                int i = SQLDBUtil.ExecuteNonQuery("sh_Paydayslastsynctime", SP);
                AlertMsg.MsgBox(Page, "Saved Successfully!", AlertMsg.MessageType.Success);
                //CodeUtil.ShowMessage(Page, "Saved Successfully!", CodeUtil.MessageType.Success, "");
            }
            catch (Exception ex)
            {
                AlertMsg.MsgBox(Page, ex.Message, AlertMsg.MessageType.Error);
            }
        }
        private DataSet USP_Salaries_All_Save4(DateTime startdate, DateTime enddate, string Empid)
        {
            DataSet ds = null;
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[5];
                sqlParams[0] = new SqlParameter("@empid", Empid);
                sqlParams[1] = new SqlParameter("@paystartdate", startdate);
                sqlParams[2] = new SqlParameter("@payenddate", enddate);
                sqlParams[3] = new SqlParameter("@WSID", SiteidGrid);
                sqlParams[4] = new SqlParameter("@createdby", Convert.ToInt32(Session["UserId"]));
                SQLDBUtil.ExecuteNonQuery("USP_Salaries_All_Save_REV4", sqlParams);
            }
            catch { }
            return ds;
        }
        private void BindZeroPayableDaysEmployees()
        {
            if (ddlMonth.SelectedIndex != 0)
            {
            }
            else
            {
                AlertMsg.MsgBox(Page, "Select Month", AlertMsg.MessageType.Warning);
                return;
            }
            if (Convert.ToInt32(ddlYear.SelectedValue) != 0)
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
            SP[0] = new SqlParameter("@Year", Convert.ToInt32(ddlYear.SelectedValue));
            SP[1] = new SqlParameter("@Month", Convert.ToInt32(ddlMonth.SelectedValue));
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
        protected void gvunsync_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                SiteidGrid = Convert.ToInt32(e.CommandArgument);
                if (e.CommandName == "com")
                {
                    try
                    {
                        GridViewRow gvr = (GridViewRow)(((Control)e.CommandSource).NamingContainer);
                        int rowindex = gvr.RowIndex;
                        DropDownList ddlmonthgrid = (DropDownList)gvr.FindControl("ddlmonthgrid");
                        DropDownList ddlyeargrid = (DropDownList)gvr.FindControl("ddlyeargrid");
                        Label lbllastsyncgrid = (Label)gvr.FindControl("lbllastsyncgrid");
                        lastsync = lbllastsyncgrid.Text;
                        ddlMonth.SelectedValue = ddlmonthgrid.SelectedValue;
                        ddlYear.SelectedValue = ddlyeargrid.SelectedValue;
                        MonthGrid = Convert.ToInt32(ddlmonthgrid.SelectedValue);
                        YearGrid = Convert.ToInt32(ddlyeargrid.SelectedValue);
                        txtEmpName.Text = String.Empty;
                        PaybleDaysStatusChecking(SiteidGrid, Convert.ToInt32(ddlmonthgrid.SelectedValue), Convert.ToInt32(ddlyeargrid.SelectedValue));
                        //Paging1.CurrentPage = 1;
                        BindPager();
                        LinkButton lnkdisplaygrid = (LinkButton)gvunsync.Rows[rowindex].FindControl("lnkCompute");
                        lnkdisplaygrid.CssClass = "btn btn-success";
                    }
                    catch (Exception ex)
                    {
                        AlertMsg.MsgBox(Page, ex.Message, AlertMsg.MessageType.Error);
                    }
                }
                if (e.CommandName == "MissNo")
                {
                    objHrCommon.CurrentPage = Pagingmsg.CurrentPage;
                    objHrCommon.PageSize = Pagingmsg.ShowRows;
                    ViewState["WSID"] = SiteidGrid.ToString();
                    //GridViewRow gvr = (GridViewRow)(((Control)e.CommandSource).NamingContainer);
                    //DropDownList ddlmonthgrid = (DropDownList)gvr.FindControl("ddlmonthgrid");
                    //DropDownList ddlyeargrid = (DropDownList)gvr.FindControl("ddlyeargrid");
                    //ddlMonth.SelectedValue = ddlmonthgrid.SelectedValue;
                    //ddlYear.SelectedValue = ddlyeargrid.SelectedValue;
                    BindingMissingEmployees();
                }
                if (e.CommandName == "ZPD")
                {
                    objHrCommon.CurrentPage = PagingzeroPayabledaysemp.CurrentPage;
                    objHrCommon.PageSize = PagingzeroPayabledaysemp.ShowRows;
                    ViewState["WSID"] = SiteidGrid.ToString();
                    //GridViewRow gvr = (GridViewRow)(((Control)e.CommandSource).NamingContainer);
                    //DropDownList ddlmonthgrid = (DropDownList)gvr.FindControl("ddlmonthgrid");
                    //DropDownList ddlyeargrid = (DropDownList)gvr.FindControl("ddlyeargrid");
                    //ddlMonth.SelectedValue = ddlmonthgrid.SelectedValue;
                    //ddlYear.SelectedValue = ddlyeargrid.SelectedValue;
                    BindZeroPayableDaysEmployees();
                }
                if (e.CommandName == "Wages")
                {
                    objHrCommon.CurrentPage = Pagingmsg.CurrentPage;
                    objHrCommon.PageSize = Pagingmsg.ShowRows;
                    ViewState["WSID"] = SiteidGrid.ToString();
                    //GridViewRow gvr = (GridViewRow)(((Control)e.CommandSource).NamingContainer);
                    //DropDownList ddlmonthgrid = (DropDownList)gvr.FindControl("ddlmonthgrid");
                    //DropDownList ddlyeargrid = (DropDownList)gvr.FindControl("ddlyeargrid");
                    //ddlMonth.SelectedValue = ddlmonthgrid.SelectedValue;
                    //ddlYear.SelectedValue = ddlyeargrid.SelectedValue;
                    BindBasicInActiveEmployees();
                }
            }
            catch { }
            if (e.CommandName == "FS")
            {
                objHrCommon.CurrentPage = Pagingfs.CurrentPage;
                objHrCommon.PageSize = Pagingfs.ShowRows;
                ViewState["WSID"] = SiteidGrid.ToString();
                BindingFSEmployees();
            }
        }
        private void BindingFSEmployees()
        {
            SqlParameter[] SP = new SqlParameter[7];
            SP[0] = new SqlParameter("@Year", Convert.ToInt32(ddlYear.SelectedValue));
            SP[1] = new SqlParameter("@Month", Convert.ToInt32(ddlMonth.SelectedValue));
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
        private void BindingMissingEmployees()
        {
            SqlParameter[] SP = new SqlParameter[7];
            SP[0] = new SqlParameter("@Year", Convert.ToInt32(ddlYear.SelectedValue));
            SP[1] = new SqlParameter("@Month", Convert.ToInt32(ddlMonth.SelectedValue));
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
                lblmsg1.Visible = true;
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
                lblmsg1.Visible = true;
                gvmsngemp.Visible = false;
                Pagingmsg.Visible = false;
            }
        }
        private void BindBasicInActiveEmployees()
        {
            SqlParameter[] SP = new SqlParameter[7];
            SP[0] = new SqlParameter("@Year", Convert.ToInt32(ddlYear.SelectedValue));
            SP[1] = new SqlParameter("@Month", Convert.ToInt32(ddlMonth.SelectedValue));
            SP[2] = new SqlParameter("@CurrentPage", objHrCommon.CurrentPage);
            SP[3] = new SqlParameter("@PageSize", objHrCommon.PageSize);
            SP[4] = new SqlParameter("@NoOfRecords", System.Data.SqlDbType.Int);
            SP[4].Direction = ParameterDirection.Output;
            SP[5] = new SqlParameter("returnvalue", System.Data.SqlDbType.Int);
            SP[5].Direction = ParameterDirection.ReturnValue;
            if (SiteidGrid != 0)
                SP[6] = new SqlParameter("@Siteid", SiteidGrid);
            else
                SP[6] = new SqlParameter("@Siteid", ViewState["WSID"]);
            DataSet ds = SQLDBUtil.ExecuteDataset("sh_GetBasicInactiveemployee", SP);
            if (ds.Tables[0].Rows.Count > 0)
            {
                gvBasicInactive.DataSource = ds.Tables[0];
                gvBasicInactive.Visible = true;
                lblBasicInactive.Visible = true;
                pngBasicInActive.Visible = true;
                gvBasicInactive.DataBind();
                int totpage = Convert.ToInt32(SP[5].Value);
                int noofrec = Convert.ToInt32(SP[4].Value);
                objHrCommon.TotalPages = totpage;
                objHrCommon.NoofRecords = noofrec;
                pngBasicInActive.Bind(objHrCommon.CurrentPage, objHrCommon.TotalPages, objHrCommon.NoofRecords, objHrCommon.PageSize);
            }
            else
            {
                gvBasicInactive.DataSource = null;
                lblBasicInactive.Visible = true;
                pngBasicInActive.Visible = false;
                gvBasicInactive.Visible = false;
                gvBasicInactive.DataBind();
            }
        }
        protected void lnkunsync_Click(HRCommon objHrCommon)
        {
            SqlParameter[] SP = new SqlParameter[8];
            SP[0] = new SqlParameter("@Year", Convert.ToInt32(ddlYear.SelectedValue));
            SP[1] = new SqlParameter("@Month", Convert.ToInt32(ddlMonth.SelectedValue));
            SP[2] = new SqlParameter("@fcase", 3);
            SP[3] = new SqlParameter("@Process", "Salary Caluculation");
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
        protected void lnksync_Click(object sender, EventArgs e)
        {
            SqlParameter[] SP = new SqlParameter[4];
            SP[0] = new SqlParameter("@Year", Convert.ToInt32(ddlYear.SelectedValue));
            SP[1] = new SqlParameter("@Month", Convert.ToInt32(ddlMonth.SelectedValue));
            SP[2] = new SqlParameter("@fcase", 3);
            SP[3] = new SqlParameter("@Process", "Salary Caluculation");
            DataSet ds = SQLDBUtil.ExecuteDataset("sh_Paydayslastsynctime", SP);
            if (ds.Tables[1].Rows.Count > 0)
            {
                tblsync.Visible = true;
                gvsync.DataSource = ds.Tables[1];
            }
            else
                gvsync.DataSource = null;
            gvsync.DataBind();
        }
        public int CheckLeapYear(int intyear)
        {
            if (intyear % 4 == 0 && intyear % 100 != 0 || intyear % 400 == 0)
                return 1; // It is a leap year
            else
                return 0; // Not a leap year
        }
        public void BindCalendarStr()
        {
            try
            {
                if (txtEmpName.Text == "" || txtEmpName.Text == null)
                {
                    txtEmpNameHidden.Value = "";
                }
                string Name = null;
                try
                {
                    if (EMPIDPram == 0 || EMPIDPram == null)
                    {
                        if (txtEmpName.Text != "" || txtEmpName.Text != null)
                        {
                            EMPIDPram = Convert.ToInt32(txtEmpNameHidden.Value == "" ? "0" : txtEmpNameHidden.Value);
                        }
                        //if (txtEmpName.Text.Trim() != "")
                        Name = "";
                    }
                }
                catch { }
                int month = Convert.ToInt32(ddlMonth.SelectedValue == "" ? "0" : ddlMonth.SelectedValue);
                int monthtext = month;
                int year = Convert.ToInt32(ddlYear.SelectedValue == "" ? "0" : ddlYear.SelectedValue);
                int yeartext = year; int year1 = year;
                int startdate = 1;
                // DataSet startdate = AttendanceDAC.GetStartDate();
                if (startdate != 1)
                {
                    if (month == 1)
                    {
                        month = 12;
                        year = year - 1;
                        year1 = year + 1;
                    }
                    else
                        month = month - 1;
                }
                string st = month + "/" + startdate + "/" + year;
                DateTime dt = CODEUtility.ConvertToDate(st, DateFormat.MonthDayYear);// DateTime.ParseExact(st, "MM/dd/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                DateTime dtEnd = dt.AddMonths(1);
                int EmpNatureID = 0;
                int DepartmentID = 0, WorkSiteID = 0;
                try { DepartmentID = 0; }//Convert.ToInt32(ddlDepartment_hid.Value == "" ? "0" : ddlDepartment_hid.Value); }
                catch { } try { WorkSiteID = SiteidGrid; }
                catch { }
                DateTime StartDate = dt, EndDate = dtEnd;
                List<DateTime> dateList = new List<DateTime>();
                int DayInterval = 1;
                int TotalPages = 1;//returnVale
                int NoofRecords = 100;// return value
                int PageSize = EmpListPaging.ShowRows;
                int CurrentPage = EmpListPaging.CurrentPage;
                DataSet dsEPMData = AttendanceDAC.HR_GetAttandanceStrip(EMPIDPram, WorkSiteID, DepartmentID, EmpNatureID, StartDate, EndDate
                    , CurrentPage, PageSize, ref NoofRecords, ref TotalPages, Name);
                if (dsEPMData.Tables[1].Rows.Count == 0)
                    EmpListPaging.Visible = false;
                else
                    EmpListPaging.Visible = true;
                EmpListPaging.Bind(CurrentPage, TotalPages, NoofRecords, PageSize);
                tblPay.Rows.Clear();
                tblPay.Style.Add("border", "solid red 1px");
                tblPay.Style.Add("border-collapse", "collapse");
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
                        tblPay.Controls.Add(rowNew);
                        TableCell cellNew0 = new TableCell();
                        TableCell cellNew = new TableCell();
                        TableCell cellNew2 = new TableCell();
                        rowNew.Style.Add("border", " solid navy 1px");
                        cellNew.Style.Add("background-color", "#87cefa");
                        cellNew2.Style.Add("background-color", "#ffffff");
                        //cellNew1.Style.Add("background-color", "#87cefa");
                        cellNew.Style.Add("font-weight", "bold");
                        //cellNew1.Style.Add("font-weight", "bold");
                        cellNew.Style.Add("Text-align", "Center");
                        //cellNew1.Style.Add("Text-align", "Center");
                        for (int row = 0; row < 1; row++)
                        {
                            for (int col = 0; col < 3; col++)
                            {
                                // Create a new TableCell object.                       
                                if (col > 0)
                                {
                                    switch (Convert.ToInt32(monthtext))
                                    {
                                        case 1:
                                            cellNew0.Text = "".ToString();
                                            //int year1 = year + 1;                                         
                                            cellNew.Text = "January".ToString() + " " + year1;
                                            cellNew2.Text = "Attendance in Advance for Jan 21 - Jan 31";
                                            cellNew.ColumnSpan = 20;
                                            cellNew2.ColumnSpan = 15;
                                            break;
                                        case 2:
                                            cellNew0.Text = "".ToString();
                                            cellNew.Text = "February".ToString() + " " + year;
                                            cellNew2.Text = "Attendance in Advance for Feb 21 - Feb 28";
                                            cellNew.ColumnSpan = 20;
                                            cellNew2.ColumnSpan = 15;
                                            break;
                                        case 3:
                                            cellNew0.Text = "".ToString();
                                            cellNew.Text = "March".ToString() + " " + year;
                                            cellNew2.Text = "Attendance in Advance for Mar 21 - Mar 31";
                                            cellNew2.ColumnSpan = 15;
                                            cellNew.ColumnSpan = 20;
                                            break;
                                        case 4:
                                            cellNew0.Text = "".ToString();
                                            cellNew.Text = "April".ToString() + " " + year;
                                            cellNew2.Text = "Attendance in Advance for Apr 21 - Apr 30";
                                            cellNew2.ColumnSpan = 15;
                                            cellNew.ColumnSpan = 20;
                                            break;
                                        case 5:
                                            cellNew0.Text = "".ToString();
                                            cellNew.Text = "May".ToString() + year;
                                            cellNew2.Text = "Attendance in Advance for May 21 - May 31";
                                            cellNew2.ColumnSpan = 15;
                                            cellNew.ColumnSpan = 20;
                                            break;
                                        case 6:
                                            cellNew0.Text = "".ToString();
                                            cellNew.Text = "June".ToString() + " " + year;
                                            cellNew2.Text = "Attendance in Advance for June 21 - June 30";
                                            cellNew2.ColumnSpan = 15;
                                            cellNew.ColumnSpan = 20;
                                            break;
                                        case 7:
                                            cellNew0.Text = "".ToString();
                                            cellNew.Text = "July".ToString() + " " + year;
                                            cellNew2.Text = "Attendance in Advance for July 21 - July 31";
                                            cellNew2.ColumnSpan = 15;
                                            cellNew.ColumnSpan = 20;
                                            break;
                                        case 8:
                                            cellNew0.Text = "".ToString();
                                            cellNew.Text = "Augest".ToString() + " " + year;
                                            cellNew2.Text = "Attendance in Advance for Aug 21 - Aug 31";
                                            cellNew2.ColumnSpan = 15;
                                            cellNew.ColumnSpan = 20;
                                            break;
                                        case 9:
                                            cellNew0.Text = "".ToString();
                                            cellNew.Text = "September".ToString() + " " + year;
                                            cellNew2.Text = "Attendance in Advance for Sep 21 - Sep 30";
                                            cellNew2.ColumnSpan = 15;
                                            cellNew.ColumnSpan = 20;
                                            break;
                                        case 10:
                                            cellNew0.Text = "".ToString();
                                            cellNew.Text = "October".ToString() + " " + year;
                                            cellNew2.Text = "Attendance in Advance for Oct 21 - Oct 31";
                                            cellNew2.ColumnSpan = 15;
                                            cellNew.ColumnSpan = 20;
                                            break;
                                        case 11:
                                            cellNew0.Text = "".ToString();
                                            cellNew.Text = "November".ToString() + " " + year;
                                            cellNew2.Text = "Attendance in Advance for Nov 21 - Nov 30";
                                            cellNew2.ColumnSpan = 15;
                                            cellNew.ColumnSpan = 20;
                                            break;
                                        case 12:
                                            cellNew0.Text = "".ToString();
                                            cellNew.Text = "December".ToString() + " " + year;
                                            cellNew2.Text = "Attendance in Advance for Dec 21 - Dec 31";
                                            cellNew2.ColumnSpan = 15;
                                            cellNew.ColumnSpan = 20;
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
                            rowNew.Controls.Add(cellNew2);
                            //rowNew.Controls.Add(cellNew1);
                            int x = CheckLeapYear(year1);
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
                        tblPay.Rows.Add(tblHeadRow);
                    if (tblDepartRow != null)
                        tblPay.Rows.Add(tblDepartRow);
                    tblPay.Rows.Add(tblRow);
                    string dtl = ddlMonth.SelectedItem.Value + "/0" + startdate + "/" + ddlYear.SelectedItem.Value;
                    //lblDates.Text = dtl;
                    isFirst = false;
                }
            }
            catch { }
        }
        public void BindMonthStr()
        {
            try
            {
                if (txtEmpName.Text == "" || txtEmpName.Text == null)
                {
                    txtEmpNameHidden.Value = "";
                }
                string Name = null;
                try
                {
                    if (EMPIDPram == 0 || EMPIDPram == null)
                    {
                        //if (txtEmpID.Text.Trim() != "")
                        //    EMPIDPram = Convert.ToInt32(txtEmpID.Text);
                        if (txtEmpName.Text != "" || txtEmpName.Text != null)
                        {
                            EMPIDPram = Convert.ToInt32(txtEmpNameHidden.Value == "" ? "0" : txtEmpNameHidden.Value);
                        }
                        //if (txtEmpName.Text.Trim() != "")
                        Name = "";
                    }
                }
                catch { }
                int month = Convert.ToInt32(ddlMonth.SelectedValue == "" ? "0" : ddlMonth.SelectedValue);
                int monthtext = month;
                int year = Convert.ToInt32(ddlYear.SelectedValue == "" ? "0" : ddlYear.SelectedValue);
                int yeartext = year; int year1 = year;
                int startdate = 21;
                // DataSet startdate = AttendanceDAC.GetStartDate();
                if (startdate != 1)
                {
                    if (month == 1)
                    {
                        month = 12;
                        year = year - 1;
                        year1 = year + 1;
                    }
                    else
                        month = month - 1;
                }
                string st = month + "/" + startdate + "/" + year;
                DateTime dt = CODEUtility.ConvertToDate(st, DateFormat.MonthDayYear);// DateTime.ParseExact(st, "MM/dd/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                DateTime dtEnd = dt.AddMonths(1);
                int EmpNatureID = 0;
                int DepartmentID = 0, WorkSiteID = 0;
                try { DepartmentID = 0; }//Convert.ToInt32(ddlDepartment_hid.Value == "" ? "0" : ddlDepartment_hid.Value); }
                catch { } try { WorkSiteID = SiteidGrid; }
                catch { }
                DateTime StartDate = dt, EndDate = dtEnd;
                List<DateTime> dateList = new List<DateTime>();
                int DayInterval = 1;
                int TotalPages = 1;//returnVale
                int NoofRecords = 100;// return value
                int PageSize = EmpListPaging.ShowRows;
                int CurrentPage = EmpListPaging.CurrentPage;
                try
                {
                    if (Convert.ToInt32(ViewState["WSID"]) > 0)
                        WorkSiteID = Convert.ToInt32(ViewState["WSID"]);
                }
                catch { }
                DataSet dsEPMData = AttendanceDAC.HR_GetAttandanceByPaging(EMPIDPram, WorkSiteID, DepartmentID, EmpNatureID, StartDate, EndDate
                    , CurrentPage, PageSize, ref NoofRecords, ref TotalPages, Name, 0);
                if (dsEPMData.Tables[1].Rows.Count == 0)
                    EmpListPaging.Visible = false;
                else
                    EmpListPaging.Visible = true;
                EmpListPaging.Bind(CurrentPage, TotalPages, NoofRecords, PageSize);
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
                    // tblHeadRow.Style.Add("border", "solid red 1px");
                    //tblHeadRow.Style.Add("border-collapse", "collapse");
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
                        cellNew.Style.Add("background-color", "#87cefa");
                        //cellNew1.Style.Add("background-color", "#87cefa");
                        cellNew.Style.Add("font-weight", "bold");
                        cellNew1.Style.Add("font-weight", "bold");
                        cellNew.Style.Add("Text-align", "Center");
                        cellNew1.Style.Add("Text-align", "Center");
                        for (int row = 0; row < 1; row++)
                        {
                            for (int col = 0; col < 3; col++)
                            {
                                // Create a new TableCell object.                       
                                if (col > 0)
                                {
                                    switch (Convert.ToInt32(monthtext))
                                    {
                                        case 1:
                                            cellNew0.Text = "".ToString();
                                            //int year1 = year + 1;
                                            cellNew.Text = "December".ToString() + " " + year;
                                            cellNew1.Text = "January".ToString() + " " + year1;
                                            break;
                                        case 2:
                                            cellNew0.Text = "".ToString();
                                            cellNew.Text = "January".ToString() + "  " + year;
                                            cellNew1.Text = "February".ToString() + " " + year;
                                            cellNew.ColumnSpan = 11;
                                            cellNew1.ColumnSpan = 20;
                                            break;
                                        case 3:
                                            cellNew0.Text = "".ToString();
                                            cellNew.Text = "February".ToString() + " " + year;
                                            cellNew1.Text = "March".ToString() + " " + year;
                                            cellNew.ColumnSpan = 8;
                                            cellNew1.ColumnSpan = 20;
                                            break;
                                        case 4:
                                            cellNew0.Text = "".ToString();
                                            cellNew.Text = "March".ToString() + " " + year;
                                            cellNew1.Text = "April".ToString() + " " + year;
                                            break;
                                        case 5:
                                            cellNew0.Text = "".ToString();
                                            cellNew.Text = "April".ToString() + " " + year;
                                            cellNew1.Text = "May".ToString() + year;
                                            cellNew.ColumnSpan = 10;
                                            cellNew1.ColumnSpan = 20;
                                            break;
                                        case 6:
                                            cellNew0.Text = "".ToString();
                                            cellNew.Text = "May".ToString() + " " + year;
                                            cellNew1.Text = "June".ToString() + " " + year;
                                            break;
                                        case 7:
                                            cellNew0.Text = "".ToString();
                                            cellNew.Text = "June".ToString() + " " + year;
                                            cellNew1.Text = "July".ToString() + " " + year;
                                            cellNew.ColumnSpan = 10;
                                            cellNew1.ColumnSpan = 20;
                                            break;
                                        case 8:
                                            cellNew0.Text = "".ToString();
                                            cellNew.Text = "July".ToString() + " " + year;
                                            cellNew1.Text = "Augest".ToString() + " " + year;
                                            break;
                                        case 9:
                                            cellNew0.Text = "".ToString();
                                            cellNew.Text = "Augest".ToString() + " " + year;
                                            cellNew1.Text = "September".ToString() + " " + year;
                                            break;
                                        case 10:
                                            cellNew0.Text = "".ToString();
                                            cellNew.Text = "September".ToString() + " " + year;
                                            cellNew1.Text = "October".ToString() + " " + year;
                                            cellNew.ColumnSpan = 10;
                                            cellNew1.ColumnSpan = 20;
                                            break;
                                        case 11:
                                            cellNew0.Text = "".ToString();
                                            cellNew.Text = "October".ToString() + " " + year;
                                            cellNew1.Text = "November".ToString() + " " + year;
                                            break;
                                        case 12:
                                            cellNew0.Text = "".ToString();
                                            cellNew.Text = "November".ToString() + " " + year;
                                            cellNew1.Text = "December".ToString() + " " + year;
                                            cellNew.ColumnSpan = 10;
                                            cellNew1.ColumnSpan = 20;
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
                            rowNew.Controls.Add(cellNew1);
                            int x = CheckLeapYear(year1);
                            if (x == 1 && monthtext == 2)
                            {
                                cellNew.ColumnSpan = 29;
                                cellNew1.ColumnSpan = 20;
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
                    string dtl = ddlMonth.SelectedItem.Value + "/0" + startdate + "/" + ddlYear.SelectedItem.Value;
                    //lblDates.Text = dtl;
                    isFirst = false;
                }
            }
            catch { }
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
            tcName.Style.Add("font-weight", "bold");
            tcName.Style.Add("border", " solid navy 1px");
            //tcName.Style.Add("background-color", "#87cefa");
            if (stmonth == edmonth)
            {
                tblAtt.Style.Add("background-color", "light Blue");
            }
            else
                tcName.Style.Add("background-color", "#87cefa");
            tcName.Style.Add("text-align", "center");
            if (tcName.Text != "")
            {
                if (Convert.ToInt32(tcName.Text) >= 21)
                {
                    tcName.Style.Add("background-color", "#ffffff");
                }
                else
                {
                    tcName.Style.Add("background-color", "#87cefa");
                }
            }
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
                tcName.Style.Add("background-color", "#A1F9DB");
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
                    tcName.Style.Add("font-weight", "bold");
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
                    tcName.Style.Add("background-color", "#87cefa");
            else
                tcName.Style.Add("background-color", "#A1F9DB");
            tcName.Style.Add("text-align", "center");
            tcName.Width = Width;
            tblRow.Cells.Add(tcName);
        }
        protected void lblCalenderstrip_Click(object sender, EventArgs e)
        {
            lblAbC.ForeColor = System.Drawing.Color.Blue;
            lblCalenderstrip.ForeColor = System.Drawing.Color.Red;
            BindCalendarStr();
        }
        protected void lblAbC_Click(object sender, EventArgs e)
        {
            lblCalenderstrip.ForeColor = System.Drawing.Color.Blue;
            lblAbC.ForeColor = System.Drawing.Color.Blue;
            BindMonthStr();
            BindCalendarStr();
            lblAbC.Enabled = true; lblCalenderstrip.Enabled = false; lblCalenderstrip.Visible = true;
        }
        protected void btnfetch_Click(object sender, EventArgs e)
        {
            gvmsngemp.Visible = false;
            lblmsg1.Visible = false;
            Paging1.Visible = false;
            gvzeroPayabledaysemp.Visible = false;
            lblmsg.Visible = false;
            PagingzeroPayabledaysemp.Visible = false;
            lblBasicInactive.Visible = false;
            pngBasicInActive.Visible = false;
            gvBasicInactive.Visible = false;
            Paging1.CurrentPage = 1;
            BindPager();
        }
    }
}