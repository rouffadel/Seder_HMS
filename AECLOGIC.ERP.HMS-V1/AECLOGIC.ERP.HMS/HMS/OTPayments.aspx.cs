using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AECLOGIC.HMS.BLL;
using System.Data;
using System.Data.SqlClient;
using Aeclogic.Common.DAL;
using System.IO;
using System.Configuration;
using System.Collections;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using AECLOGIC.ERP.COMMON;
namespace AECLOGIC.ERP.HMS
{
    public partial class OTPayments : AECLOGIC.ERP.COMMON.WebFormMaster
    {
        int mid = 0;
        bool viewall;
        string menuname;
        string menuid;
        static int SearchCompanyID;
        static int? ORoleid;
        static int ODeptid = 0;
        static int WSiteid;
        static int Ouserid;
        AttendanceDAC objAtt = new AttendanceDAC();
        HRCommon objHrCommon = new HRCommon();
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
            BindGrid(objHrCommon);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                lblStatus.Text = String.Empty;
                DataSet dstemp = new DataSet();
                ORoleid = Convert.ToInt32(Session["RoleId"].ToString());
                SearchCompanyID = Convert.ToInt32(Session["CompanyID"]);
                Ouserid = Convert.ToInt32(Session["UserId"]);
                if (!IsPostBack)
                {
                    BindYears();
                    if (Request.QueryString.Count > 0)
                    {
                        if (Request.QueryString.AllKeys.Contains("key"))
                        {
                            btnSearch.Visible = true;
                            int id = Convert.ToInt32(Request.QueryString["key"].ToString());
                            if (id == 3)//Addnew
                            {
                                btnSync.Visible = false;
                                BindPager();
                                btnApproveSalas.Text = "GM Approval";
                                btnApproveSalas.Visible = true;
                            }
                            if (id == 4)
                            {
                                btnSync.Visible = false;
                                BindPager();
                                btnApproveSalas.Text = "CFO Approval";
                                btnApproveSalas.Visible = true;
                            }
                            if (id == 5)
                            {
                                btnSync.Visible = false;
                                btnApproveSalas.Visible = false;
                            }
                        }
                    }
                    else
                    {
                        btnSync.Visible = true;
                        btnSearch.Visible = false;
                        btnApproveSalas.Visible = true;
                    }
                    string URL = Request.Url.Segments[Request.Url.Segments.Length - 1] + Request.Url.Query;
                    int RoleId = Convert.ToInt32(Session["RoleId"].ToString());
                    int ModuleId = ModuleID; ;
                    DataSet ds = AttendanceDAC.GetAllowed(RoleId, ModuleId, URL);
                    dstemp = GetWorkSites(objAtt, dstemp);
                    dstemp = GetDepartments(objAtt, dstemp);
                    BindDesignations();
                    if (Session["Site"].ToString() != "")
                        ddlWorksite.Items.FindByValue(Session["Site"].ToString()).Selected = true;
                    if (Request.QueryString.Count > 0)
                    {
                        if (Request.QueryString.AllKeys.Contains("EMPID") && Request.QueryString.AllKeys.Contains("Month") && Request.QueryString.AllKeys.Contains("Year"))
                        {
                            int Month = Convert.ToInt32(Request.QueryString["Month"]);
                            int Year = Convert.ToInt32(Request.QueryString["Year"]);
                            txtEMPID.Text = Request.QueryString["EMPID"];
                            ddlMonth.SelectedValue = Month.ToString();
                            ddlYear.SelectedValue = Year.ToString();
                        }
                    }
                    BindPager();
                }
            }
            catch (Exception ex)
            {
                clsErrorLog.HMSEventLog(ex, "OTPayment", "Page_Load", "001");
            }
        }
        public void BindYears()
        {
            try
            {
                FIllObject.FillDropDown(ref ddlYear, "sh_FinancialYearList", "id", "name");
                ddlMonth.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                clsErrorLog.HMSEventLog(ex, "OTPayment", "BindYear", "002");
            }
        }
        public void BindDesignations()
        {
            try
            {
                DataSet ds = (DataSet)objAtt.GetDesignations();
                ddlDesif2.DataSource = ds;
                ddlDesif2.DataTextField = "Designation";
                ddlDesif2.DataValueField = "DesigId";
                ddlDesif2.DataBind();
                ddlDesif2.Items.Insert(0, new ListItem("---ALL---", "0", true));
            }
            catch (Exception ex)
            {
                clsErrorLog.HMSEventLog(ex, "OTPayment", "BindDesignations", "003");
            }
        }
        private DataSet GetDepartments(AttendanceDAC objAtt, DataSet dstemp)
        {
            dstemp = objAtt.GetDepartments(0);
            return dstemp;
        }
        private DataSet GetWorkSites(AttendanceDAC objAtt, DataSet dstemp)
        {
            dstemp = objAtt.GetWorkSiteByEmpID(Convert.ToInt32(Session["UserId"]), Convert.ToInt32(Session["CompanyID"]), Convert.ToInt32(Session["RoleId"]));
            ddlWorksite.DataSource = dstemp.Tables[0];
            ddlWorksite.DataTextField = "Site_Name";
            ddlWorksite.DataValueField = "Site_ID";
            ddlWorksite.DataBind();
            ddlWorksite.Items.Insert(0, new ListItem("---SELECT---", "0", true));
            ddlWorksite.SelectedValue = "1";
            BindDeparmetBySite(Convert.ToInt32(ddlWorksite.SelectedValue));
            return dstemp;
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                EmpListPaging.CurrentPage = 1;
                BindPager();
            }
            catch (Exception ex)
            {
                clsErrorLog.HMSEventLog(ex, "OTPayment", "btnSearch_Click", "004");
            }
        }
        private DataSet BindGrid(HRCommon objHrCommon)
        {
            objHrCommon.PageSize = EmpListPaging.ShowRows;
            objHrCommon.CurrentPage = EmpListPaging.CurrentPage;
            int? DeptID = null;
            int? WsID = null;
            int? Month = 0;
            int? Year = 0;
            if (txtSearchdept.Text.Trim() != "")
                if (ddlDepartment.SelectedItem.Value != "0")
                    DeptID = Convert.ToInt32(ddlDepartment.SelectedItem.Value);
            if (txtSearchWorksite.Text.Trim() != "")
                if (ddlWorksite.SelectedItem.Value != "0")
                    WsID = Convert.ToInt32(ddlWorksite.SelectedItem.Value);
            int? DesigID = null;
            if (txtSearchDesi.Text.Trim() != "")
                if (ddlDesif2.SelectedValue != "0")
                    DesigID = Convert.ToInt32(ddlDesif2.SelectedValue);
            if (ddlMonth.SelectedValue != "0")
                Month = Convert.ToInt32(ddlMonth.SelectedValue);
            if (ddlYear.SelectedValue != "0")
                Year = Convert.ToInt32(ddlYear.SelectedValue);
            int? EMPID = null;
            try
            {
                if (txtEMPID.Text.Trim() != "")
                    EMPID = Convert.ToInt32(txtEMPID.Text);
            }
            catch { }
            // added here by pratap date: 10-01-2017
            if (Request.QueryString.Count > 0)
            {
                if (Request.QueryString.AllKeys.Contains("EMPID") && Request.QueryString.AllKeys.Contains("Month") && Request.QueryString.AllKeys.Contains("Year"))
                {
                    Month = Convert.ToInt32(Request.QueryString["Month"]);
                    Year = Convert.ToInt32(Request.QueryString["Year"]);
                    EMPID = Convert.ToInt32(Request.QueryString["EMPID"]);
                }
            }
            DataSet startdate = AttendanceDAC.GetStartDate();
            if (Month == 1)
            {
                Month = 12;
                Year = Year - 1;
            }
            else
                Month = Month - 1;
            int status = 0;
            if (Request.QueryString.Count > 0)
            {
                if (Request.QueryString.AllKeys.Contains("key"))
                {
                    int id = Convert.ToInt32(Request.QueryString["key"].ToString());
                    if (id == 3)//Addnew
                    {
                        status = Convert.ToInt32(3);
                    }
                    if (id == 4)
                    {
                        status = 4;
                    }
                    if (id == 5)
                    {
                        status = 5;
                    }
                }
            }
            DataSet dsAtt = objAtt.GetEmpOTPayList(objHrCommon, WsID, DeptID, DesigID, Month, Year, Convert.ToInt32(Session["CompanyID"]), EMPID, status);
            if (dsAtt.Tables.Count > 0 && dsAtt.Tables[0].Rows.Count > 0)
            {
                gdvOTpaymentLst.DataSource = dsAtt.Tables[0];
                gdvOTpaymentLst.DataBind();
                EmpListPaging.Bind(objHrCommon.CurrentPage, objHrCommon.TotalPages, objHrCommon.NoofRecords, objHrCommon.PageSize);
                if (Request.QueryString.Count > 0)
                {
                    CallDetails();
                }
            }
            else
            {
                gdvOTpaymentLst.DataSource = null;
                gdvOTpaymentLst.DataBind();
                EmpListPaging.Visible = true;
            }
            return dsAtt;
        }
        decimal TotalOT = 0;
        protected string GetOT()
        {
            return TotalOT.ToString("N2");
        }
        protected void Worksidechangewithdep(object sender, EventArgs e)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[4];
                param[0] = new SqlParameter("@Search", txtSearchWorksite.Text);
                param[1] = new SqlParameter("@CompanyID", Convert.ToInt32(Session["CompanyID"]));
                param[2] = new SqlParameter("@EmpID", Convert.ToInt32(Session["UserId"]));
                param[3] = new SqlParameter("@Role", Convert.ToInt32(Session["RoleId"]));
                FIllObject.FillDropDown(ref ddlWorksite, "HR_GoogleSearcGetWorksiteByEmpID", param);
                ListItem itmSelected = ddlWorksite.Items.FindByText(txtSearchWorksite.Text);
                if (itmSelected != null)
                {
                    ddlWorksite.SelectedItem.Selected = false;
                    itmSelected.Selected = true;
                }
                ddlWorksite_SelectedIndexChanged(sender, e);
                txtSearchdept.Text = txtSearchDesi.Text = "";
                ddlDesif2.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                clsErrorLog.HMSEventLog(ex, "OTPayment", "Worksidechangewithdep", "005");
            }
        }
        protected void GetDepartmentSearch(object sender, EventArgs e)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[3];
                param[0] = new SqlParameter("@Search", txtSearchdept.Text);
                param[1] = new SqlParameter("@CompanyID", SearchCompanyID);
                param[2] = new SqlParameter("@SiteID", ddlWorksite.SelectedValue);
                FIllObject.FillDropDown(ref ddlDepartment, "HMS_googlesearch_GetDepartmentBySite", param);
                ListItem itmSelected = ddlDepartment.Items.FindByText(txtSearchdept.Text);
                if (itmSelected != null)
                {
                    ddlDepartment.SelectedItem.Selected = false;
                    itmSelected.Selected = true;
                }
                if (txtSearchdept.Text != "") { ddlDepartment.SelectedIndex = 1; }
            }
            catch (Exception ex)
            {
                clsErrorLog.HMSEventLog(ex, "OTPayment", "GetDepartmentSearch", "006");
            }
        }
        protected void GetDesignation(object sender, EventArgs e)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@Search", txtSearchDesi.Text);
                FIllObject.FillDropDown(ref ddlDesif2, "HR_GetSearchgoogleDesignations", param);
                ListItem itmSelected = ddlDesif2.Items.FindByText(txtSearchDesi.Text);
                if (itmSelected != null)
                {
                    ddlDesif2.SelectedItem.Selected = false;
                    itmSelected.Selected = true;
                }
            }
            catch (Exception ex)
            {
                clsErrorLog.HMSEventLog(ex, "OTPayment", "GetDesignation", "007");
            }
        }
        protected string GetOTAmount(decimal Price)
        {
            string amt = string.Empty;
            Price = Convert.ToDecimal(Price.ToString("N2"));
            TotalOT += Price;
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
        protected void ddlWorksite_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                BindDeparmetBySite(Convert.ToInt32(ddlWorksite.SelectedValue));
                WSiteid = Convert.ToInt32(ddlWorksite.SelectedValue);
            }
            catch (Exception ex)
            {
                clsErrorLog.HMSEventLog(ex, "OTPayment", "ddlWorksite_SelectedIndexChanged", "008");
            }
        }
        //Added by Rijwan:22-03-2016
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute]
        public static string[] GetCompletionList(string prefixText, int count, string contextKey)
        {
            DataSet ds = AttendanceDAC.Googlesearch_GetWorkSiteByEmpID(prefixText, SearchCompanyID, Ouserid, ORoleid);
            return ConvertStingArray(ds);
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute]
        public static string[] GetCompletionListdept(string prefixText, int count, string contextKey)
        {
            DataSet ds = AttendanceDAC.GetDepartmentGoogleSerc(prefixText, SearchCompanyID, WSiteid);
            return ConvertStingArray(ds);
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute]
        public static string[] GetCompletionListDesi(string prefixText, int count, string contextKey)
        {
            DataSet ds = AttendanceDAC.GetGoogleSerachDesignations(prefixText);
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
        public void BindDeparmetBySite(int Site)
        {
            try
            {
                DataSet ds = AttendanceDAC.BindDeparmetBySite(Site, Convert.ToInt32(Session["CompanyID"]));
                ddlDepartment.DataSource = ds;
                ddlDepartment.DataTextField = "DeptName";
                ddlDepartment.DataValueField = "DepartmentUId";
                ddlDepartment.DataBind();
                ddlDepartment.Items.Insert(0, new ListItem("---ALL---", "0", true));
            }
            catch (Exception ex)
            {
                clsErrorLog.HMSEventLog(ex, "OTPayment", "BindDeparmetBySite", "009");
            }
        }
        protected void btnSync_Click(object sender, EventArgs e)
        {
            int valofSync = 0;
            Button btn = sender as Button;
            if (btn.ID.Trim().ToLower() == "btnsync")
                valofSync = 0;
            else
                valofSync = 1;
            try
            {
                if (Request.QueryString.Count > 0)
                {
                    if (Request.QueryString.AllKeys.Contains("key"))
                    {
                        int id = Convert.ToInt32(Request.QueryString["key"].ToString());
                        if (valofSync == 1)
                        {
                            if (gdvOTpaymentLst.Rows.Count > 0)
                            {
                                if (id == 3)//Addnew
                                {
                                    foreach (GridViewRow row in gdvOTpaymentLst.Rows)
                                    {
                                        Label empid = (Label)row.FindControl("lblempID");
                                        int EID = Convert.ToInt32(empid.Text);
                                        SqlParameter[] p = new SqlParameter[2];
                                        p[0] = new SqlParameter("@ApproveStatus", 4);
                                        p[1] = new SqlParameter("@Empid", EID);
                                        DataSet ds = SQLDBUtil.ExecuteDataset("upd_HR_PaySlip_OT", p);
                                    }
                                    Response.Redirect("OTPayments.aspx?key=4");
                                }
                                if (id == 4)
                                {
                                    foreach (GridViewRow row in gdvOTpaymentLst.Rows)
                                    {
                                        Label empid = (Label)row.FindControl("lblempID");
                                        int EID = Convert.ToInt32(empid.Text);
                                        SqlParameter[] p = new SqlParameter[2];
                                        p[0] = new SqlParameter("@ApproveStatus", 5);
                                        p[1] = new SqlParameter("@Empid", EID);
                                        DataSet ds = SQLDBUtil.ExecuteDataset("upd_HR_PaySlip_OT", p);
                                    }
                                    Response.Redirect("OTPayments.aspx?key=5");
                                }
                            }
                            else
                                AlertMsg.MsgBox(Page, "No Records Found", AlertMsg.MessageType.Warning);
                        }
                    }
                }
                else
                {
                    if (valofSync == 1)
                    {
                        if (gdvOTpaymentLst.Rows.Count > 0)
                        {
                            foreach (GridViewRow row in gdvOTpaymentLst.Rows)
                            {
                                Label empid = (Label)row.FindControl("lblempID");
                                int EID = Convert.ToInt32(empid.Text);
                                SqlParameter[] p = new SqlParameter[2];
                                p[0] = new SqlParameter("@ApproveStatus", 3);
                                p[1] = new SqlParameter("@Empid", EID);
                                DataSet ds = SQLDBUtil.ExecuteDataset("upd_HR_PaySlip_OT", p);
                            }
                            Response.Redirect("OTPayments.aspx?key=3");
                        }
                        else
                            AlertMsg.MsgBox(Page, "No Records Found", AlertMsg.MessageType.Warning);
                    }
                }
                //else
                //{
                DataSet startdate = AttendanceDAC.GetStartDate();
                // for Jan 2016 selection pay slip showing by Gana
                int Month = 0;
                if (ddlMonth.SelectedValue != "0")
                    Month = Convert.ToInt32(ddlMonth.SelectedItem.Value);
                int Year = Convert.ToInt32(ddlYear.SelectedItem.Value);
                if (Month == 1)
                {
                    Month = 12;
                    Year = Year - 1;
                }
                else
                    Month = Month - 1;
                string st = Month + "/" + startdate.Tables[0].Rows[0][0].ToString() + "/" + Year;
                DateTime dt = CODEUtility.ConvertToDate(st, DateFormat.MonthDayYear);
                DateTime ed = dt.AddMonths(1).AddDays(-1);
                if (txtEMPID.Text.Trim() == "")
                {
                    int SiteID = Convert.ToInt32(ddlWorksite.SelectedItem.Value);
                    int DeptID = Convert.ToInt32(ddlDepartment.SelectedItem.Value);
                    int EmpNatureID = 0; //Convert.ToInt32(ddlEmpNature.SelectedValue);
                    DataSet ds = AttendanceDAC.HR_GetEMPIDsByWSID(SiteID, DeptID, EmpNatureID, "");
                    foreach (DataRow dr in ds.Tables[0].Rows)
                        Leaves.HR_OTCalculation(Convert.ToInt32(dr["EmpID"]), dt, ed, valofSync);
                    //btnSearch_Click(sender, e);
                }
                else
                {
                    Leaves.HR_OTCalculation(Convert.ToInt32(txtEMPID.Text), dt, ed, valofSync);
                }
                if (valofSync == 0)
                {
                    AlertMsg.MsgBox(Page, "Data Synchronized!");
                    btnSearch_Click(null, null);
                }
                else
                {
                    if (gdvOTpaymentLst.Rows.Count > 0)
                    {
                        if (Request.QueryString.Count > 0)
                        {
                            if (Request.QueryString.AllKeys.Contains("key"))
                            {
                                btnSearch_Click(null, null);
                            }
                        }
                        else
                        {
                            gdvOTpaymentLst.DataSource = null;
                            gdvOTpaymentLst.DataBind();
                        }
                        AlertMsg.MsgBox(Page, "Approved Successfully!");
                    }
                }
                //}
            }
            catch (Exception ex)
            {
                clsErrorLog.HMSEventLog(ex, "OTPayment", "btnSync_Click", "010");
            }
        }
        protected void gdvWS_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
            }
        }
        protected void CallDetails()
        {
            try
            {
                DataSet startdate = AttendanceDAC.GetStartDate();
                int Month = 0, Year = 0;
                if (Request.QueryString.Count > 0)
                {
                    Month = Convert.ToInt32(Request.QueryString["Month"]);
                    Year = Convert.ToInt32(Request.QueryString["Year"]);
                }
                if (Month == 1)
                {
                    Month = 12;
                    Year = Year - 1;
                }
                else
                    Month = Month - 1;
                string st = Month + "/" + startdate.Tables[0].Rows[0][0].ToString() + "/" + Year;
                DateTime dt = CODEUtility.ConvertToDate(st, DateFormat.MonthDayYear);
                DateTime ed = dt.AddMonths(1).AddDays(-1);
                int EMPID = 0;
                if (Request.QueryString.Count > 0)
                {
                    EMPID = Convert.ToInt32(Request.QueryString["EmpID"]);
                }
                DataSet ds = Leaves.HR_OTCalculation_Details(dt, ed, EMPID, 0);
                TotNoEMP = ds.Tables[0].Compute("Sum(Amount)", "").ToString();
                gvrOTHrs.DataSource = ds.Tables[0];
                gvrOTHrs.DataBind();
                gvrOTSal.DataSource = ds.Tables[1];
                gvrOTSal.DataBind();
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected void gdvWS_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Edt")
                {
                    DataSet startdate = AttendanceDAC.GetStartDate();
                    int Month = 0;
                    if (ddlMonth.SelectedValue != "0")
                        Month = Convert.ToInt32(ddlMonth.SelectedItem.Value);
                    int Year = Convert.ToInt32(ddlYear.SelectedItem.Value);
                    //if (startdate.Tables[0].Rows[0][0].ToString() != "1")
                    //{
                    if (Month == 1)
                    {
                        Month = 12;
                        Year = Year - 1;
                    }
                    else
                        Month = Month - 1;
                    //}
                    string st = Month + "/" + startdate.Tables[0].Rows[0][0].ToString() + "/" + Year;
                    DateTime dt = CODEUtility.ConvertToDate(st, DateFormat.MonthDayYear);
                    DateTime ed = dt.AddMonths(1).AddDays(-1);
                    int EMPID = Convert.ToInt32(e.CommandArgument);
                    DataSet ds = Leaves.HR_OTCalculation_Details(dt, ed, EMPID, 0);
                    TotNoEMP = ds.Tables[0].Compute("Sum(Amount)", "").ToString();
                    gvrOTHrs.DataSource = ds.Tables[0];
                    gvrOTHrs.DataBind();
                    gvrOTSal.DataSource = ds.Tables[1];
                    gvrOTSal.DataBind();
                }
                else
                {
                    bool Status = true;
                }
            }
            catch { }
        }
        public string TotNoEMP = "";
    }
}