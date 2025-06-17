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
namespace AECLOGIC.ERP.HMSV1
{
    public partial class MonthlyPaymentsApprovedV4V1 : AECLOGIC.ERP.COMMON.WebFormMaster
    {
        AECLOGIC.HMS.BLL.AttendanceDAC objEmployee = new AttendanceDAC();
        AttendanceDAC objRights = new AttendanceDAC();
        HRCommon objHrCommon = new HRCommon();
        static int SearchCompanyID;
        static int Siteid;
        int mid = 0;
        bool viewall;
        string menuname;
        string menuid;
        bool Editable;
        int OrderID = 0, Direction = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                SearchCompanyID = Convert.ToInt32(Session["CompanyID"]);
                if (!IsPostBack)
                {
                    Page.Form.Attributes.Add("enctype", "multipart/form-data");
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
                        //pratap added here date:13-04-2016                    
                        txtEmpName.Attributes.Add("onkeydown", "return controlEnter(event)");
                        SqlParameter[] sqlParams = new SqlParameter[1];
                        sqlParams[0] = new SqlParameter("@fcase", 1);
                        FIllObject.FillDropDown(ref ddlYear, "sh_salaryapprovedyr", sqlParams);
                        bool Access = Convert.ToBoolean(ViewState["ViewAll"]);
                        OrderID = 2;
                        ViewState["OrderID"] = 2;
                    }
                    catch
                    {
                        Response.Redirect("Logon.aspx");
                    }
                    BindExcelFormat();
                }
            }
            catch (Exception ex)
            {
                clsErrorLog.HMSEventLog(ex, "Salaries", "Page_Load", "001");
            }
        }
        private void BindExcelFormat()
        {
            FIllObject.FillDropDown(ref ddlFormat, "sh_SalariesExcelFormat");
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
        }
        void EmpListPaging_ShowRowsClick(object sender, EventArgs e)
        {
            EmpListPaging.CurrentPage = 1;
            BindPager();
        }
        void EmpListPaging_FirstClick(object sender, EventArgs e)
        {
            BindPager();
        }
        void BindPager()
        {
            objHrCommon.PageSize = EmpListPaging.CurrentPage;
            objHrCommon.CurrentPage = EmpListPaging.ShowRows;
            EmployeBind(objHrCommon);
        }
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
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                SQLDBUtil.ExecuteDataset("sh_dynamicviewscreation_rev4");
                EmpListPaging.CurrentPage = 1;
                SalaryCalculationStatusChecking();
            }
            catch (Exception ex)
            {
                clsErrorLog.HMSEventLog(ex, "Salaries", "btnSearch_Click", "005");
            }
        }
        private void SalaryCalculationStatusChecking()
        {
            SqlParameter[] sqlParams = new SqlParameter[4];
            sqlParams[0] = new SqlParameter("@wsid", ddlworksite.SelectedValue);
            sqlParams[1] = new SqlParameter("@month", ddlMonth.SelectedValue);
            sqlParams[2] = new SqlParameter("@year", ddlYear.SelectedValue);
            sqlParams[3] = new SqlParameter("@empid", DBNull.Value);
            DataSet dss = SQLDBUtil.ExecuteDataset("sh_SalaryCaluculationstatus", sqlParams);
            if (dss.Tables[0].Rows[0][0].ToString() == "Already Synchronized")
                EmployeBind(objHrCommon);
            else
            {
                gvPaySlip.DataSource = null;
                gvPaySlip.DataBind();
                EmpListPaging.Visible = false;
                AlertMsg.MsgBox(Page, dss.Tables[0].Rows[0][0].ToString(), AlertMsg.MessageType.Warning);
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
        void EmployeBind(HRCommon objHrCommon)
        {
            try
            {
                objHrCommon.PageSize = EmpListPaging.ShowRows;
                objHrCommon.CurrentPage = EmpListPaging.CurrentPage;
                gvPaySlip.DataSource = null;
                gvPaySlip.DataBind();
                string Name = txtEmpName.Text;
                gvPaySlip.Visible = true;
                int EmpID = 0;
                if (txtEmpName.Text == "" || txtEmpName.Text == null)
                {
                    txtEmpNameHidden.Value = "";
                }
                else if (txtEmpName.Text != "" || txtEmpName.Text != null)
                {
                    EmpID = Convert.ToInt32(txtEmpNameHidden.Value == "" ? "0" : txtEmpNameHidden.Value);
                }
                else
                {
                    EmpID = 0;
                }
                DataSet dsBind = USP_Salaries_All_Approved_Search_4(Convert.ToInt32(ddlworksite.SelectedValue), Convert.ToInt32(ddlMonth.SelectedValue), Convert.ToInt32(ddlYear.SelectedValue), EmpID);
                ViewState["NoOfRecords"] = objHrCommon.NoofRecords;
                if (dsBind != null && dsBind.Tables.Count != 0 && dsBind.Tables[0].Rows.Count > 0)
                {
                    gvPaySlip.DataSource = dsBind;
                    EmpListPaging.Bind(objHrCommon.CurrentPage, objHrCommon.TotalPages, objHrCommon.NoofRecords, objHrCommon.PageSize);
                    EmpListPaging.Visible = true;
                    gvPaySlip.DataBind();
                    SqlParameter[] sqlParams = new SqlParameter[4];
                    sqlParams[0] = new SqlParameter("@wsid", ddlworksite.SelectedValue);
                    sqlParams[1] = new SqlParameter("@Month", ddlMonth.SelectedValue);
                    sqlParams[2] = new SqlParameter("@Year", ddlYear.SelectedValue);
                    string transid = SqlHelper.ExecuteScalar("sh_GetTranId_ApprvdSal", sqlParams).ToString(); ;
                    lnkTransid.Text = transid;
                }
                else
                {
                    gvPaySlip.EmptyDataText = "No Records Found";
                    EmpListPaging.Visible = false;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionList_EmpName(string prefixText, int count, string contextKey)
        {
            DataSet ds = AttendanceDAC.GetSearchEmpName(prefixText);
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
        private DataSet USP_Salaries_All_Approved4(HRCommon objHrCommon, DateTime startdate, DateTime enddate, int Empid)
        {
            DataSet ds = null;
            try
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
                sqlParams[7] = new SqlParameter("@WSID", objHrCommon.SiteID);
                ds = SQLDBUtil.ExecuteDataset("USP_Salaries_All_Approved_Rev4", sqlParams);
                objHrCommon.NoofRecords = (int)sqlParams[3].Value;
                objHrCommon.TotalPages = (int)sqlParams[2].Value;
            }
            catch { }
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
                btnSearch_Click(sender, e);
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
        protected void btnExcelExport_Click(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToInt32(ddlFormat.SelectedValue) == 1)
                {
                    DataSet startdate = null;
                    startdate = AttendanceDAC.GetStartDate();
                    string Month = ddlMonth.SelectedValue;
                    string Year = ddlYear.SelectedValue;
                    string st = Month + "/" + startdate.Tables[0].Rows[0][0].ToString() + "/" + Year;
                    DateTime dt = CODEUtility.ConvertToDate(st, DateFormat.MonthDayYear);
                    DataSet ds = HR_GetEMPSalariesExporttoExcel_Rev4(dt, null, (Convert.ToInt32(ddlworksite.SelectedValue)), null);
                    char Status = 'n';
                    SqlDataReader dr = Leaves.GetEmployeesForSalariesExportToExcel((Convert.ToInt32(ddlworksite.SelectedValue)), 100, Convert.ToInt32(Month), Convert.ToInt32(Year), Status, 1);//, Convert.ToInt32(ViewState["NoOfRecords"]));
                    ExportFileUtil.ExportToFile(dr, "", "#EFEFEF", "#E6e6e6", "Salaries_Details", ddlworksite.Text.Trim(), ddlworksite.SelectedItem.ToString(), ddlMonth.SelectedItem.Text + " " + ddlYear.SelectedItem.Text, ds);
                    Response.Redirect("MonthlyPaymentsApprovedV4.aspx", false);
                }
                else if (Convert.ToInt32(ddlFormat.SelectedValue) == 2)
                    ExcelExport2(objHrCommon);
                else if (Convert.ToInt32(ddlFormat.SelectedValue) == 3)
                    ExcelExport3(objHrCommon);
                else if (Convert.ToInt32(ddlFormat.SelectedValue) == 4)
                {
                    //DataSet startdate = null;
                    //startdate = AttendanceDAC.GetStartDate();
                    //string Month = ddlMonth.SelectedValue;
                    //string Year = ddlYear.SelectedValue;
                    //string st = Month + "/" + startdate.Tables[0].Rows[0][0].ToString() + "/" + Year;
                    //DateTime dt = CODEUtility.ConvertToDate(st, DateFormat.MonthDayYear);
                    //DataSet ds = HR_GetEMPSalariesExporttoExcel_Rev4(dt, null, (Convert.ToInt32(ddlworksite.SelectedValue)), null);
                    char Status = 'y';
                    //SqlDataReader dr = Leaves.GetEmployeesForSalariesExportToExcel_NewV4(Convert.ToInt32(ddlworksite.SelectedValue), null, Convert.ToInt32(ddlMonth.SelectedValue),
                    //    Convert.ToInt32(ddlYear.SelectedValue), Status, null);//, Convert.ToInt32(ViewState["NoOfRecords"]));
                    //if (dr.HasRows)
                    //    ExportFileUtil.ExportToExcel(dr, "", "#EFEFEF", "#E6e6e6", "PaySlip_Details_AMS");
                    //else
                    //    AlertMsg.MsgBox(Page, "Records not found", AlertMsg.MessageType.Info);
                    int? siteid = null;
                    if (Convert.ToInt32(ddlworksite.SelectedValue) > 0)
                        siteid = Convert.ToInt32(ddlworksite.SelectedValue);
                    CSVFormatFiles.ExportGridToCSV(Leaves.GetEmployeesForSalariesExportToExcel_NewV4_ds(siteid, null, Convert.ToInt32(ddlMonth.SelectedValue),
                        Convert.ToInt32(ddlYear.SelectedValue), Status, null), "SCC_BankFormat");
                    //  ExportFileUtil.ExportToFile(dr, "", "#EFEFEF", "#E6e6e6", "PaySlip_Details_AMS", ddlworksite.Text.Trim(), ddlworksite.SelectedItem.ToString(), ddlMonth.SelectedItem.Text + " " + ddlYear.SelectedItem.Text, ds);
                    // Response.Redirect("MonthlyPaymentsApprovedV4.aspx", false);
                }
                else if (Convert.ToInt32(ddlFormat.SelectedValue) == 5)
                {
                    CSVFormatFiles.ExportGridToCSV(Leaves.sh_GetWSSummary_ds(Convert.ToInt32(ddlMonth.SelectedValue),
                       Convert.ToInt32(ddlYear.SelectedValue)), "SCC_SalarySummary");
                }
                else if (Convert.ToInt32(ddlFormat.SelectedValue) == 6)
                {
                    DataSet startdate = null;
                    startdate = AttendanceDAC.GetStartDate();
                    string Month = ddlMonth.SelectedValue;
                    string Year = ddlYear.SelectedValue;
                    string st = Month + "/" + startdate.Tables[0].Rows[0][0].ToString() + "/" + Year;
                    DateTime dt = CODEUtility.ConvertToDate(st, DateFormat.MonthDayYear);
                    //DataSet ds = HR_GetEMPSalariesExporttoExcel_Rev4(dt, null, null, null);
                    //char Status = 'n';
                    //SqlDataReader dr = Leaves.GetEmployeesForSalariesExportToExcel((Convert.ToInt32(ddlworksite.SelectedValue)), 100, Convert.ToInt32(Month), Convert.ToInt32(Year), Status, 1);//, Convert.ToInt32(ViewState["NoOfRecords"]));
                    //ExportFileUtil.ExportToFile(dr, "", "#EFEFEF", "#E6e6e6", "Salaries_Details", ddlworksite.Text.Trim(), ddlworksite.SelectedItem.ToString(), ddlMonth.SelectedItem.Text + " " + ddlYear.SelectedItem.Text, ds);
                    //Response.Redirect("MonthlyPaymentsApprovedV4.aspx", false);
                    //CSVFormatFiles.ExportGridToCSV_All(ds, "SCC_SalarySummary_Details", ddlMonth.SelectedItem.Text + " " + ddlYear.SelectedItem.Text);
                    CSVFormatFiles.ExportGridToCSV("SCC_SalarySummary_Details", ddlMonth.SelectedItem.Text + " " + ddlYear.SelectedItem.Text,
                        Convert.ToInt32(ddlMonth.SelectedValue), Convert.ToInt32(ddlYear.SelectedValue));
                }
            }
            catch (Exception ex)
            {
                clsErrorLog.HMSEventLog(ex, "Salaries", "btnExcelExport_Click", "006");
            }
        }
        public static DataSet HR_GetEMPSalariesExporttoExcel_Rev4(DateTime Date, int? EMPID, int? WSID, int? Dept)
        {
            SqlParameter[] sqlParams = new SqlParameter[4];
            sqlParams[0] = new SqlParameter("@Date", Date);
            sqlParams[1] = new SqlParameter("@EMPID", EMPID);
            sqlParams[2] = new SqlParameter("@WSID", WSID);
            sqlParams[3] = new SqlParameter("@Dept", Dept);
            return SQLDBUtil.ExecuteDataset("[HR_GetEMPSalariesExporttoExcel_Rev4]", sqlParams);
        }
        private void ExcelExport3(HRCommon objHrCommon)
        {
            try
            {
                DataSet dss = SQLDBUtil.ExecuteDataset("USP_APprovedSalariesColumns");
                DataSet ds = USP_Salaries_All_Approved_43(Convert.ToInt32(ddlworksite.SelectedValue), Convert.ToInt32(ddlMonth.SelectedValue), Convert.ToInt32(ddlYear.SelectedValue));
                ExportFileUtil.ExportToFileRev43(dss, "", "#EFEFEF", "#E6e6e6", "PaySlip_Details" + ".xls", "application/vnd.xls", "Monthly Salaries Of :" + Convert.ToString(ddlworksite.SelectedItem) + "-" + Convert.ToString(ddlMonth.SelectedItem) + "-" + Convert.ToString(ddlYear.SelectedItem), "", "", ds);
                Response.Redirect("MonthlyPaymentsApprovedV4.aspx", false);
            }
            catch (Exception ex)
            {
                clsErrorLog.HMSEventLog(ex, "Salaries", "btnExcelExport_Click", "006");
            }
        }
        private void ExcelExport2(HRCommon objHrCommon)
        {
            try
            {
                DataSet dss = SQLDBUtil.ExecuteDataset("USP_APprovedSalariesColumns2");
                DataSet ds = USP_Salaries_All_Approved_42(Convert.ToInt32(ddlworksite.SelectedValue), Convert.ToInt32(ddlMonth.SelectedValue), Convert.ToInt32(ddlYear.SelectedValue));
                ExportFileUtil.ExportToFileRev42(dss, "", "#EFEFEF", "#E6e6e6", "PaySlip_Details" + ".xls", "application/vnd.xls", "Monthly Salaries Of :" + Convert.ToString(ddlworksite.SelectedItem) + "-" + Convert.ToString(ddlMonth.SelectedItem) + "-" + Convert.ToString(ddlYear.SelectedItem), "", "", ds);
                Response.Redirect("MonthlyPaymentsApprovedV4.aspx", false);
            }
            catch (Exception ex)
            {
                clsErrorLog.HMSEventLog(ex, "Salaries", "btnExcelExport_Click", "006");
            }
        }
        //private void ExportToFileRev4(DataSet sqlDataSet, String ItemColor, String AltItemColor, String CrossItemColor, String FileName, String Head, String SubHead, String Titil, DataSet ds)
        //{
        //    ExportToFileRev4(sqlDataSet, ItemColor, AltItemColor, CrossItemColor, FileName + ".xls", "application/vnd.xls", Head, SubHead, Titil, ds);
        //}
        //private void ExportToFileRev4(DataSet sqlDataSet, String ItemColor, String AltItemColor, String CrossItemColor, String FileName, String ContentType, String Head, String SubHead, String Titil, DataSet ds)
        //{
        //    //Clear the response
        //    HttpContext.Current.Response.Clear();
        //    HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + FileName);
        //    HttpContext.Current.Response.Charset = "";
        //    HttpContext.Current.Response.ContentType = ContentType;
        //    HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.UTF8;
        //    //Initialize the string that is used to build the file.
        //    HttpContext.Current.Response.Write("<table>");
        //    //Enumerate the field names and the records that are used to build 
        //    //the file.
        //    int CountSalary = sqlDataSet.Tables[0].Rows.Count;
        //    int CountClounms = CountSalary;
        //    if (Head.Trim() != "")
        //    {
        //        HttpContext.Current.Response.Write("<tr style='color:Black; background-color:White; font-weight:bold; font-size:26px; align-content:center; border-bottom-width:medium;'>");
        //        HttpContext.Current.Response.Write("<td colspan=" + CountClounms + ">" + Head.ToString() + "</td>");//colspan=" + CountClounms + " 
        //        HttpContext.Current.Response.Write("</tr>");
        //    }
        //    HttpContext.Current.Response.Write("<tr style='color:White; background-color:Navy; font-weight:bold; font-size:20px; border-bottom-width:medium;'>");
        //    for (int i = 0; i <= CountClounms-1; i++)
        //    {
        //        HttpContext.Current.Response.Write("<td>" + sqlDataSet.Tables[0].Rows[i][1].ToString() + "</td>");
        //    }
        //    HttpContext.Current.Response.Write("</tr>");
        //    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        //    {//write in new row
        //        HttpContext.Current.Response.Write("<tr>");
        //        for (int j = 0; j < ds.Tables[0].Columns.Count; j++)
        //        {
        //            HttpContext.Current.Response.Write("<td>");
        //            HttpContext.Current.Response.Write(ds.Tables[0].Rows[i][j].ToString());
        //            HttpContext.Current.Response.Write("</td>");
        //        }
        //        HttpContext.Current.Response.Write("</TR>");
        //    }
        //    HttpContext.Current.Response.Write("</table>");
        //    HttpContext.Current.Response.End();
        //}
        private void releaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
                AlertMsg.MsgBox(Page, "Exception Occured while releasing object " + ex.ToString(), AlertMsg.MessageType.Warning);
            }
            finally
            {
                GC.Collect();
            }
        }
        private DataSet USP_Salaries_All_Approved_43(int? SiteID, int? Month, int Year)//, int PageSize)
        {
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[7];
                sqlParams[0] = new SqlParameter("@CurrentPage", 1);
                sqlParams[1] = new SqlParameter("@PageSize", 10000);
                sqlParams[2] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
                sqlParams[2].Direction = ParameterDirection.ReturnValue;
                sqlParams[3] = new SqlParameter("@NoofRecords", System.Data.SqlDbType.Int);
                sqlParams[3].Direction = ParameterDirection.Output;
                sqlParams[4] = new SqlParameter("@wsid", SiteID);
                sqlParams[5] = new SqlParameter("@Month", Month);
                sqlParams[6] = new SqlParameter("@Year", Year);
                DataSet ds = SQLDBUtil.ExecuteDataset("USP_Salaries_Approved_Rev4", sqlParams);
                return ds;
            }
            catch (Exception e)
            {
                clsErrorLog.HMSEventLog(e, "Salaries", "btnExcelExport_Click", "006");
                throw e;
            }
        }
        private DataSet USP_Salaries_All_Approved_42(int? SiteID, int? Month, int Year)//, int PageSize)
        {
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[7];
                sqlParams[0] = new SqlParameter("@CurrentPage", 1);
                sqlParams[1] = new SqlParameter("@PageSize", 10000);
                sqlParams[2] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
                sqlParams[2].Direction = ParameterDirection.ReturnValue;
                sqlParams[3] = new SqlParameter("@NoofRecords", System.Data.SqlDbType.Int);
                sqlParams[3].Direction = ParameterDirection.Output;
                sqlParams[4] = new SqlParameter("@wsid", SiteID);
                sqlParams[5] = new SqlParameter("@Month", Month);
                sqlParams[6] = new SqlParameter("@Year", Year);
                DataSet ds = SQLDBUtil.ExecuteDataset("USP_Salaries_Approved_Rev42", sqlParams);
                return ds;
            }
            catch (Exception e)
            {
                clsErrorLog.HMSEventLog(e, "Salaries", "btnExcelExport_Click", "006");
                throw e;
            }
        }
        private DataSet USP_Salaries_All_Approved_Search_4(int? SiteID, int? Month, int Year, int Empid)//, int PageSize)
        {
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[8];
                sqlParams[0] = new SqlParameter("@CurrentPage", objHrCommon.CurrentPage);
                sqlParams[1] = new SqlParameter("@PageSize", objHrCommon.PageSize);
                sqlParams[2] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
                sqlParams[2].Direction = ParameterDirection.ReturnValue;
                sqlParams[3] = new SqlParameter("@NoofRecords", System.Data.SqlDbType.Int);
                sqlParams[3].Direction = ParameterDirection.Output;
                sqlParams[4] = new SqlParameter("@wsid", SiteID);
                sqlParams[5] = new SqlParameter("@Month", Month);
                sqlParams[6] = new SqlParameter("@Year", Year);
                if (Empid != 0)
                    sqlParams[7] = new SqlParameter("@Empid", Empid);
                else
                    sqlParams[7] = new SqlParameter("@Empid", DBNull.Value);
                DataSet ds = SQLDBUtil.ExecuteDataset("USP_Salaries_Approved_Rev4", sqlParams);
                objHrCommon.NoofRecords = (int)sqlParams[3].Value;
                objHrCommon.TotalPages = (int)sqlParams[2].Value;
                return ds;
            }
            catch (Exception e)
            {
                clsErrorLog.HMSEventLog(e, "Salaries", "btnExcelExport_Click", "006");
                throw e;
            }
        }
        public string DocNavigateUrl(string EmpId)
        {
            int Month = 0;
            if (ddlMonth.SelectedValue != "0")
                Month = Convert.ToInt32(ddlMonth.SelectedItem.Value);
            int Year = Convert.ToInt32(ddlYear.SelectedItem.Value);
            string Date = 01 + "/" + Month + "/" + Year;
            string ReturnVal = "";
            ReturnVal = String.Format("PaySlipV4.aspx?id={0}&Date={1}", EmpId, Date);
            return ReturnVal;
        }
        protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlParameter[] sqlParams = new SqlParameter[2];
            sqlParams[0] = new SqlParameter("@year", ddlYear.SelectedValue);
            sqlParams[1] = new SqlParameter("@fcase", 4);
            FIllObject.FillDropDown(ref ddlMonth, "sh_salaryapprovedyr", sqlParams);
        }
        protected void ddlMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlParameter[] sqlParams = new SqlParameter[3];
            sqlParams[0] = new SqlParameter("@year", ddlYear.SelectedValue);
            sqlParams[1] = new SqlParameter("@fcase", 5);
            sqlParams[2] = new SqlParameter("@month", ddlMonth.SelectedValue);
            FIllObject.FillDropDown(ref ddlworksite, "sh_salaryapprovedyr", sqlParams);
        }
        protected void gvPaySlip_PreRender(object sender, EventArgs e)
        {
            if (gvPaySlip.Rows.Count > 0)
            {
                GridViewRow row = gvPaySlip.Rows[gvPaySlip.Rows.Count - 1];
                LinkButton lnkView = (LinkButton)row.FindControl("lnkView");
                lnkView.Visible = false;
            }
        }
        protected void gvPaySlip_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (gvPaySlip.Rows.Count > 0)
            {
                //if (e.Row.RowType == DataControlRowType.DataRow)
                //{
                //    gvPaySlip.Columns[3].ItemStyle.Width = 300;
                //}
                //if (e.Row.RowType == DataControlRowType.Header)
                //{
                //    TableCell cell = e.Row.Cells[3];
                //    cell.Width = new Unit("200px");
                //    for (int i = 1; i <= e.Row.Cells.Count - 1; i++)
                //    {
                //        TableCell cell2 = e.Row.Cells[i];
                //        cell2.Width = new Unit("10px");
                //    }
                //}
            }
        }
        protected void lnkTransid_Click(object sender, EventArgs e)
        {
            string url = "../AMS/display.aspx?id=" + Convert.ToInt32(lnkTransid.Text);
            string fullURL = "window.open('" + url + "', '_blank' );";
            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", fullURL, true);
        }
        protected void btnNoSalaryEmployees_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(ddlYear.SelectedValue) == 0)
            {
                AlertMsg.MsgBox(Page, "Select Year", AlertMsg.MessageType.Warning);
            }
            else if (Convert.ToInt32(ddlMonth.SelectedValue) == 0)
            {
                AlertMsg.MsgBox(Page, "Select Month.", AlertMsg.MessageType.Warning);
            }
            else
            {
                Response.Redirect("NoSalaryEmployees.aspx?Month=" + Convert.ToInt32(ddlMonth.SelectedValue) + "&Year=" + Convert.ToInt32(ddlYear.SelectedValue));
            }
        }
    }
}