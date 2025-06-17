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

namespace AECLOGIC.ERP.HMS
{
    public partial class Salaries : AECLOGIC.ERP.COMMON.WebFormMaster
    {
        #region Variables
        AttendanceDAC objEmployee = new AttendanceDAC();
        AttendanceDAC objRights = new AttendanceDAC();
        HRCommon objHrCommon = new HRCommon();
        DataSet ds = new DataSet();
        static int SearchCompanyID;
        static int Siteid;
        int mid = 0;
        bool viewall;
        string menuname;
        string menuid;
        bool Editable;
        DataSet dsBind = new DataSet();
        int OrderID = 0, Direction = 0;
        #endregion Variables

        #region Events
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if ( Convert.ToInt32(Session["UserId"]) == null)
                {
                    Response.Redirect("Logon.aspx?SessionExpire=" + true + 1);
                }

                topmenu.MenuId = GetParentMenuId();
                topmenu.ModuleId = ModuleID; ;
                topmenu.RoleID = Convert.ToInt32(Session["RoleId"].ToString());
                topmenu.SelectedMenu = Convert.ToInt32(mid.ToString());
                SearchCompanyID = Convert.ToInt32(Session["CompanyID"]);

                topmenu.DataBind();
                Session["menuname"] = menuname;
                Session["menuid"] = menuid;
                Session["MId"] = mid;
                if (!IsPostBack)
                {
                    lnkViewAttendance.Visible = false;
                    try
                    {
                        try
                        {

                            ViewState["WSID"] = 0;
                            if (Convert.ToInt32(Session["RoleId"].ToString()) == 7)
                            {
                                try
                                {

                                    //string Worksit = Session["Site"].ToString();
                                    DataSet ds = clViewCPRoles.HR_DailyAttStatus( Convert.ToInt32(Session["UserId"]));
                                    ViewState["WSID"] = ds.Tables[0].Rows[0]["ID"].ToString();
                                    txtSearchWorksite.Text = ds.Tables[0].Rows[0]["Name"].ToString();
                                    txtSearchWorksite.ReadOnly = true;

                                }
                                catch { }
                            }
                        }
                        catch { }
                        //pratap added here date:13-04-2016                    
                        txtEmpID.Attributes.Add("onkeydown", "return controlEnter(event)");
                        txtEmpName.Attributes.Add("onkeydown", "return controlEnter(event)");

                        // ddlMonth.SelectedIndex = 0;
                        //ddlYear.SelectedIndex = 0;

                        BindEmpNatures();
                        BindWorkSites();
                        // BindDepartments(); 
                        BindYears();
                        bool Access = Convert.ToBoolean(ViewState["ViewAll"]);
                        OrderID = 2;
                        ViewState["OrderID"] = 2;
                        DataSet startdate = AttendanceDAC.GetStartDate();
                        ViewState["startdate"] = 1;

                        ViewState["startdate"] = startdate.Tables[0].Rows[0][0].ToString();
                        if (Access == true)
                        {
                            EmployeBind(objHrCommon);
                        }
                        else
                        {
                            int EmpID =  Convert.ToInt32(Session["UserId"]);
                            int Month = Convert.ToInt32(ddlMonth.SelectedValue);
                            int Year = Convert.ToInt32(ddlYear.SelectedValue);
                            try
                            {
                                // for Jan 2016 selection pay slip showing by Gana
                                // DataSet startdate = AttendanceDAC.GetStartDate();
                                if (Convert.ToInt32(startdate.Tables[0].Rows[0][0]) != 1)
                                {
                                    if (Month == 1)
                                    {
                                        Month = 12;
                                        Year = Year - 1;
                                    }
                                    else
                                        Month = Month - 1;
                                }

                            }
                            catch { }
                            BindEmp(EmpID, Month, Year);
                            rbActive.Enabled = rbInActive.Enabled = false;
                            ddldepartments.Enabled = ddlworksites.Enabled = btnOutPutExcel.Enabled = false;
                            EmpListPaging.Visible = false;


                        }
                        ddlMonth_SelectedIndexChanged(sender, e);
                        if (Session["Site"].ToString() != "1")
                        {
                            ddlworksites.ClearSelection();
                            ddlworksites.Items.FindByValue(Session["Site"].ToString()).Selected = true;
                            ddlworksites.Enabled = false;
                            objHrCommon.SiteID = Convert.ToInt32(Session["Site"].ToString());
                            EmployeBind(objHrCommon);
                        }
                    }
                    catch
                    {
                        Response.Redirect("Logon.aspx");
                    }
                }
            }
            catch (Exception ex)
            {
                clsErrorLog.HMSEventLog(ex, "Salaries", "Page_Load", "001");
            }
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
        protected void BtnExportGrid_Click(object sender, EventArgs args)
        {
            try
            {
                //objHrCommon.PageSize = Convert.ToInt32(ViewState["NoOfRecords"]);
                //objHrCommon.CurrentPage = 1;
                int? SiteID = null;
                if (ddlworksites.SelectedValue != "0")
                    SiteID = Convert.ToInt32(ddlworksites.SelectedItem.Value);
                int? DeptID = null;
                if (ddldepartments.SelectedValue != "0")
                    DeptID = Convert.ToInt32(ddldepartments.SelectedItem.Value);
                int? Month = null;
                if (ddlMonth.SelectedValue != "0")
                    Month = Convert.ToInt32(ddlMonth.SelectedItem.Value);
                int Year = Convert.ToInt32(ddlYear.SelectedItem.Value);
                DataSet startdate = null;
                try
                { // for Jan 2016 selection pay slip showing by Gana
                    startdate = AttendanceDAC.GetStartDate();
                    if (Convert.ToInt32(startdate.Tables[0].Rows[0][0]) != 1)
                    {
                        if (Month == 1)
                        {
                            Month = 12;
                            Year = Year - 1;
                        }
                        else
                            Month = Month - 1;
                    }
                }
                catch { }
                char Status = 'n';
                if (rbActive.Checked)
                {
                    Status = 'y';
                }
                int? EmpNat = null;
                if (ddlEmpNature.SelectedValue != "0")
                    EmpNat = Convert.ToInt32(ddlEmpNature.SelectedValue);
                //else
                //{
                //    objHrCommon.CurrentStatus = 'n';
                //}
                try
                {
                    if (Convert.ToInt32(ViewState["WSID"]) > 0)
                        objHrCommon.SiteID = Convert.ToInt32(ViewState["WSID"]);
                }
                catch { }
                if (chkPaySlip.Checked)
                {
                    int? empid = null;

                    try { 
                    if(txtEmpID.Text.Trim()!="")
                        empid= Convert.ToInt32(txtEmpID.Text);
                    if (empid > 0)
                    {
                        SiteID = null;
                        DeptID = null;
                    }
                    }
                    catch { }
                    string st = Month + "/" + startdate.Tables[0].Rows[0][0].ToString() + "/" + Year;
                    //  DateTime dt = DateTime.ParseExact(st, "MM/dd/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                    DateTime dt = CODEUtility.ConvertToDate(st, DateFormat.MonthDayYear);
                    DataSet ds = Leaves.HR_GetEMPSalariesExporttoExcel(dt, empid, SiteID, DeptID);
                    //FileName + ".xls", "application/vnd.xls"
                    SqlDataReader dr = Leaves.GetEmployeesForSalariesExportToExcel(SiteID, DeptID, Month, Year, Status, EmpNat);//, Convert.ToInt32(ViewState["NoOfRecords"]));
                    ExportFileUtil.ExportToFile(dr, "", "#EFEFEF", "#E6e6e6", "PaySlip_Details", txtSearchWorksite.Text.Trim(), txtSearchdept.Text.Trim(), ddlMonth.SelectedItem.Text + " " + ddlYear.SelectedItem.Text, ds);

                }
                else
                {
                    if (!rbView.Checked)
                    {

                        SqlDataReader dr = objEmployee.GetEmployeesForSalariesExportToExcel(SiteID, DeptID, Month, Year, Status, EmpNat);//, Convert.ToInt32(ViewState["NoOfRecords"]));
                        ExportFileUtil.ExportToExcel(dr, "", "#EFEFEF", "#E6e6e6", "PaySlip");


                        //  string st = Month + "/" + startdate.Tables[0].Rows[0][0].ToString() + "/" + Year;
                        // //  DateTime dt = DateTime.ParseExact(st, "MM/dd/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                        // DateTime dt = CODEUtility.ConvertToDate(st, DateFormat.MonthDayYear);
                        //DataSet  ds= Leaves.HR_GetEMPSalariesExporttoExcel(dt, null, null, null);
                        // //FileName + ".xls", "application/vnd.xls"
                        //ExportFileUtil.ExportToFile_SalaryReport(ds, "#EFEFEF", "#E6e6e6", "#EFEFEF", "Slaaries.xls", "application/vnd.xls", txtSearchWorksite.Text.Trim(), txtSearchdept.Text.Trim());
                    }
                    else
                    {


                        SqlDataReader dr = Leaves.GetEmployeesForSalariesExportToExcel(SiteID, DeptID, Month, Year, Status, EmpNat);//, Convert.ToInt32(ViewState["NoOfRecords"]));
                        ExportFileUtil.ExportToExcel(dr, "", "#EFEFEF", "#E6e6e6", "PaySlip_Details");

                    }
                }

                //ExportFileUtil.ExportToWord(dr, "PaySlip");
                // ExportFileUtil.ExportToPDF(dr, "#EFEFEF", "#E6e6e6", "PaySlip");
            }
            catch (Exception ex)
            {
                clsErrorLog.HMSEventLog(ex, "Salaries", "BtnExportGrid_Click", "002");
            }
        }
        protected void BtnExportGrid_Dtl_Click(object sender, EventArgs args)
        {
            try
            {
                //objHrCommon.PageSize = Convert.ToInt32(ViewState["NoOfRecords"]);
                //objHrCommon.CurrentPage = 1;
                int? SiteID = null;
                if (ddlworksites.SelectedValue != "0")
                    SiteID = Convert.ToInt32(ddlworksites.SelectedItem.Value);
                int? DeptID = null;
                if (ddldepartments.SelectedValue != "0")
                    DeptID = Convert.ToInt32(ddldepartments.SelectedItem.Value);
                int? Month = null;
                if (ddlMonth.SelectedValue != "0")
                    Month = Convert.ToInt32(ddlMonth.SelectedItem.Value);
                int Year = Convert.ToInt32(ddlYear.SelectedItem.Value);
                DataSet startdate = null;
                try
                { // for Jan 2016 selection pay slip showing by Gana
                    startdate = AttendanceDAC.GetStartDate();
                    if (Convert.ToInt32(startdate.Tables[0].Rows[0][0]) != 1)
                    {
                        if (Month == 1)
                        {
                            Month = 12;
                            Year = Year - 1;
                        }
                        else
                            Month = Month - 1;
                    }
                }
                catch { }
                char Status = 'n';
                if (rbActive.Checked)
                {
                    Status = 'y';
                }
                int? EmpNat = null;
                if (ddlEmpNature.SelectedValue != "0")
                    EmpNat = Convert.ToInt32(ddlEmpNature.SelectedValue);
                try
                {
                    if (Convert.ToInt32(ViewState["WSID"]) > 0)
                        objHrCommon.SiteID = Convert.ToInt32(ViewState["WSID"]);
                }
                catch { }
                //else
                //{
                //    objHrCommon.CurrentStatus = 'n';
                //}
                if (!rbView.Checked)
                {
                    //SqlDataReader dr = objEmployee.GetEmployeesForSalariesExportToExcel(SiteID, DeptID, Month, Year, Status, EmpNat);//, Convert.ToInt32(ViewState["NoOfRecords"]));
                    //ExportFileUtil.ExportToExcel(dr, "", "#EFEFEF", "#E6e6e6", "PaySlip");
                    try
                    {
                        DataSet ds = new DataSet();
                        SqlDataReader dr = Leaves.GetEmployeesForSalariesExportToExcel(SiteID, DeptID, Month, Year, Status, EmpNat);//, Convert.ToInt32(ViewState["NoOfRecords"]));
                        ExportFileUtil.ExportToFile(dr, "", "#EFEFEF", "#E6e6e6", "PaySlip_Details", "", "", "", ds);

                        //string st = Month + "/" + startdate.Tables[0].Rows[0][0].ToString() + "/" + Year;
                        ////  DateTime dt = DateTime.ParseExact(st, "MM/dd/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                        //DateTime dt = CODEUtility.ConvertToDate(st, DateFormat.MonthDayYear);
                        //DataSet ds = Leaves.HR_GetEMPSalariesExporttoExcel(dt, null, SiteID, DeptID);
                        ////FileName + ".xls", "application/vnd.xls"
                        //ExportFileUtil.ExportToExcel_Salaries(dr,ds, "#EFEFEF", "#E6e6e6", "#EFEFEF", "Salaries_Details", txtSearchWorksite.Text.Trim(), txtSearchdept.Text.Trim(), ddlMonth.SelectedItem.Text + " " + ddlYear.SelectedItem.Text);
                    }
                    catch (Exception ex) { AlertMsg.MsgBox(Page, ex.Message.ToString(),AlertMsg.MessageType.Error); }
                }
                else
                {
                    SqlDataReader dr = Leaves.GetEmployeesForSalariesExportToExcel(SiteID, DeptID, Month, Year, Status, EmpNat);//, Convert.ToInt32(ViewState["NoOfRecords"]));
                    ExportFileUtil.ExportToExcel(dr, "", "#EFEFEF", "#E6e6e6", "PaySlip_Details");
                }

                //ExportFileUtil.ExportToWord(dr, "PaySlip");
                // ExportFileUtil.ExportToPDF(dr, "#EFEFEF", "#E6e6e6", "PaySlip");
            }
            catch (Exception ex)
            {
                clsErrorLog.HMSEventLog(ex, "Salaries", "BtnExportGrid_Dtl_Click", "003");
            }
        }
        protected void gvPaySlip_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            if (e.CommandName == "Run")
            {
                int EmpId = Convert.ToInt32(e.CommandArgument);
                int Month = Convert.ToInt32(ddlMonth.SelectedValue);
                int Year = Convert.ToInt32(ddlYear.SelectedValue);
                // for Jan 2016 selection pay slip showing by Gana-- Commented by CH.V.N.Ravi kumar on 21/04/2016
                //if (Month == 1)
                //{
                //    Month = 12;
                //    Year = Year - 1;
                //}
                //else
                //    Month = Month - 1;
                //string Date = ddlMonth.SelectedItem.Value + "/" + "1" + "/" + ddlYear.SelectedItem.Value;
                DataSet startdate = AttendanceDAC.GetStartDate();
                String[] srt = txtTodate.Text.Split('/');
                //string st =  + "/" + srt[0] + "/" + Year;
                string Date = srt[1] + "/" + startdate.Tables[0].Rows[0][0].ToString() + "/" + srt[2];
                DateTime dt = CODEUtility.ConvertToDate(Date, DateFormat.MonthDayYear);
                dt = dt.AddDays(-1);
                string[] sdt = dt.ToString().Split('/');
                Date = sdt[1] + '/' + sdt[0] + '/' + sdt[2];
                AttendanceDAC.HR_SavePaySLIP(EmpId, Date, 0, "Salaries");
                btnSearch_Click(sender, e);
            }
            if (e.CommandName == "Status")
            {
                int EmpID = Convert.ToInt32(e.CommandArgument);
                int Month = Convert.ToInt32(ddlMonth.SelectedValue);
                int Year = Convert.ToInt32(ddlYear.SelectedValue);
                // for Jan 2016 selection pay slip showing by Gana
                if (Month == 1)
                {
                    Month = 12;
                    Year = Year - 1;
                }
                else
                    Month = Month - 1;
                AttendanceDAC.UpdPaisStatus(EmpID, Month, Year);
                btnSearch_Click(sender, e);
            }
        }
        protected void btnSync_Click(object sender, EventArgs e)
        {

            try
            {
                int valofSync = 0;
                try
                {
                    Button btn = sender as Button;
                    if (btn.ID.Trim().ToLower() == "btnsync")
                        valofSync = 0;
                    else
                        valofSync = 1;
                }
                catch { }
                DataSet startdate = AttendanceDAC.GetStartDate();

                /////////////////// CH . V. N. Ravi Kumar ////////////21/04/2016
                // for Jan 2016 selection pay slip showing by Gana
                int Month = 0;
                if (ddlMonth.SelectedValue != "0")
                    Month = Convert.ToInt32(ddlMonth.SelectedItem.Value);
                int Year = Convert.ToInt32(ddlYear.SelectedItem.Value);
                try
                { //DataSet startdate = AttendanceDAC.GetStartDate();
                    if (Convert.ToInt32(startdate.Tables[0].Rows[0][0]) != 1)
                    {


                        //if (Month == 1)
                        //{
                        //    Month = 12;
                        //    Year = Year - 1;
                        //}
                        //else
                        //    Month = Month - 1;
                    }
                }
                catch { }
                ////////////////////////////// END

                // string st = ddlMonth.SelectedItem.Value + "/" + startdate.Tables[0].Rows[0][0].ToString() + "/" + ddlYear.SelectedItem.Value;

                String[] srt = txtTodate.Text.Split('/');
                string st = srt[1] + "/" + srt[0] + "/" + Year;
                //  DateTime dt = DateTime.ParseExact(st, "MM/dd/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                DateTime dt = CODEUtility.ConvertToDate(st, DateFormat.MonthDayYear);
                //if(Convert.ToInt32(srt[0])==21)
                //  dt = dt.AddDays(-1);
                string[] sdt = dt.ToString().Split('/');
                string[] dtt = sdt[2].ToString().Split(' ');
                //int mm = 0;
                //if (Convert.ToInt32(sdt[1]) > 21)
                //    mm = Convert.ToInt32(sdt[0]) - 1;
                //else
                //    mm = Convert.ToInt32(sdt[0]);
                if (txtTodate.Text != String.Empty)
                {

                    st = sdt[0] + '/' + sdt[1] + '/' + dtt[0];
                }
                else
                {
                    st = sdt[0] + '/' + sdt[1] + '/' + dtt[0];
                }

                if (txtEmpID.Text != "")
                {
                    int EmpID = 0;
                    try
                    {
                        EmpID = Convert.ToInt32(txtEmpID.Text);

                        //DateTime addmonth = dt.AddMonths(1);
                        //DateTime edt = DateTime.ParseExact(addmonth.ToString(), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

                        //lblDates.Text = dt + " - " +edt;
                        DataSet dsPaymentStatus = new DataSet();
                        // dsPaymentStatus = AttendanceDAC.SalPaymentStatus(EmpID, Convert.ToInt32(ddlMonth.SelectedItem.Value), Convert.ToInt32(ddlYear.SelectedItem.Value));
                        //  if (dsPaymentStatus.Tables[0].Rows[0]["Status"].ToString() == "0")




                        if (EmpID != 0)
                        {
                            AttendanceDAC.HR_SavePaySLIP(EmpID, st, valofSync, "Salaries");
                            btnSearch_Click(sender, e);
                            if (valofSync == 0)
                                AlertMsg.MsgBox(Page, "Data Synchronized!");
                            else
                                AlertMsg.MsgBox(Page, "Salaries posted to accounts!");
                        }
                        else
                        {
                            AlertMsg.MsgBox(Page, "Salary Paid!");
                        }

                    }
                    catch (Exception ex) { AlertMsg.MsgBox(Page, "InValid Data!"); }
                }
                else
                {
                    int SiteID = Convert.ToInt32(ddlworksites.SelectedItem.Value);
                    int DeptID = Convert.ToInt32(ddldepartments.SelectedItem.Value);
                    int EmpNatureID = Convert.ToInt32(ddlEmpNature.SelectedValue);
                    try
                    {
                        if (Convert.ToInt32(ViewState["WSID"]) > 0)
                            SiteID = Convert.ToInt32(ViewState["WSID"]);
                    }
                    catch { }
                    int empid = 0;// Convert.ToInt32(dr["EmpID"]);
                    DataSet ds = AttendanceDAC.HR_GetEMPIDsByWSID(SiteID, DeptID, EmpNatureID, txtEmpName.Text);
                    foreach (DataRow dr in ds.Tables[0].Rows)
                        try { empid = Convert.ToInt32(dr["EmpID"]); AttendanceDAC.HR_SavePaySLIP(Convert.ToInt32(dr["EmpID"]), st, valofSync, "Salaries"); }
                        catch { }
                    btnSearch_Click(sender, e);
                    if (valofSync == 0)
                        AlertMsg.MsgBox(Page, "Data Synchronized!");
                    else
                        AlertMsg.MsgBox(Page, "Salaries posted to accounts!");
                    //AlertMsg.MsgBox(Page, "Enter EmpID and Click Sync!");
                }
            }
            catch (Exception ex)
            {
                clsErrorLog.HMSEventLog(ex, "Salaries", "btnSync_Click", "004");
            }

            if (Convert.ToInt32(gvPaySlip.Rows.Count) == 1)
            {
                lnkViewAttendance.Visible = true;
            }
        }
        protected void chkHis_CheckedChanged(object sender, EventArgs e)
        {
            ViewState["SortDir"] = SortDirection.Ascending;
            Direction = 0;
            if (chkHis.Checked)
            {
                OrderID = 1;
                ViewState["OrderID"] = 1;
                gvPaySlip.Columns[0].Visible = true;
                EmployeBind(objHrCommon);
                btnSearch_Click(sender, e);
            }
            else
            {
                //OrderID = 0;
                gvPaySlip.Columns[0].Visible = false;
                EmployeBind(objHrCommon);

            }
        }
        protected void gvPaySlip_Sorting(object sender, GridViewSortEventArgs e)
        {
            if (ViewState["SortDir"] == null)
                e.SortDirection = SortDirection.Ascending;
            else
                e.SortDirection = (SortDirection)ViewState["SortDir"];

            switch (e.SortExpression)
            {
                case "Name":
                    OrderID = 0;
                    ViewState["OrderID"] = 0;
                    break;

                case "HisID":
                    OrderID = 1;
                    ViewState["OrderID"] = 1;
                    break;
                case "EmpId":
                    OrderID = 2;
                    ViewState["OrderID"] = 2;
                    break;

            }

            if (e.SortDirection == SortDirection.Ascending)
            {
                Direction = 0;
                ViewState["SortDir"] = SortDirection.Descending;
            }
            else
            {
                Direction = 1;
                ViewState["SortDir"] = SortDirection.Ascending;
            }
            EmployeBind(objHrCommon);

        }
        protected void btnDetailedRptExpToXL_Click(object sender, EventArgs e)
        {
            objHrCommon.SiteID = Convert.ToInt32(ddlworksites.SelectedItem.Value);
            objHrCommon.DeptID = Convert.ToInt32(ddldepartments.SelectedItem.Value);
            objHrCommon.Month = Convert.ToInt32(ddlMonth.SelectedItem.Value);
            objHrCommon.Year = Convert.ToInt32(ddlYear.SelectedItem.Value);
            // for Jan 2016 selection pay slip showing by Gana
            if (objHrCommon.Month == 1)
            {
                objHrCommon.Month = 12;
                objHrCommon.Year = objHrCommon.Year - 1;
            }
            else
                objHrCommon.Month = objHrCommon.Month - 1;

            int? EmpNat = null;
            if (ddlEmpNature.SelectedValue != "0")
                EmpNat = Convert.ToInt32(ddlEmpNature.SelectedValue);
            if (rbActive.Checked)
                objHrCommon.CurrentStatus = 'y';
            else
                objHrCommon.CurrentStatus = 'n';
            try
            {
                if (Convert.ToInt32(ViewState["WSID"]) > 0)
                    objHrCommon.SiteID = Convert.ToInt32(ViewState["WSID"]);
            }
            catch { }
            if (rbView.Checked != true)
            {
                SqlDataReader dr = objEmployee.GetEmployeesDtlRpt(objHrCommon, EmpNat);
                ExportFileUtil.ExportToExcel(dr, "", "#EFEFEF", "#E6e6e6", "Detailed_Rpt");
            }
            else
            {
                SqlDataReader dr = Leaves.GetEmployeesDtlRpt(objHrCommon, EmpNat);
                ExportFileUtil.ExportToExcel(dr, "", "#EFEFEF", "#E6e6e6", "Detailed_Rpt");
            }
        }
        //Added by Rijwan:22-03-2016
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
        protected void ddlworksites_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindDeparmetBySite(Convert.ToInt32(ddlworksites.SelectedValue));
            Siteid = Convert.ToInt32(ddlworksites.SelectedValue);
            try
            {
                if (Convert.ToInt32(ViewState["WSID"]) > 0)
                    Siteid = Convert.ToInt32(ViewState["WSID"]);
            }
            catch { }
        }
        protected void ddlMonth_SelectedIndexChanged(object sender, EventArgs e)
        {

            string st = ddlMonth.SelectedValue + "/" + ViewState["startdate"].ToString() + "/" + ddlYear.SelectedValue;
            //  DateTime dt = DateTime.ParseExact(st, "MM/dd/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            DateTime dt = CODEUtility.ConvertToDate(st, DateFormat.MonthDayYear);
            //if(Convert.ToInt32(srt[0])==21)
            dt = dt.AddDays(-1);
            String[] ddt = dt.ToString().Split('/');
            string[] dst = ddt[2].ToString().Split(' ');
            string ss = ddt[1] + "/" + ddt[0] + "/" + dst[0];
            txtTodate.Text = ss;


        }
        protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                txtTodate.Text = ViewState["startdate"].ToString() + "/" + ddlMonth.SelectedValue + "/" + ddlYear.SelectedValue;
            }
            catch { }

        }
        protected void lnkViewAttendance_Click(object sender, EventArgs e)
        {
            int Empid = 0;
            if (txtEmpID.Text != "" || txtEmpID.Text != null)
            {
                Empid = Convert.ToInt32(txtEmpID.Text);
            }
            else
            {
                foreach (GridViewRow row in gvPaySlip.Rows)
                {
                    Empid = Convert.ToInt32(row.FindControl("EmpId"));

                }
            }
            string month = ddlMonth.SelectedValue.ToString();
            int year = Convert.ToInt32(ddlYear.SelectedValue);


            // Response.Redirect("ViewAttendance.aspx?Empid=" + Empid + "&Month=" + month + "&Year=" + year);

            string url = "ViewAttendance.aspx?Empid=" + Empid + "&Month=" + month + "&Year=" + year;
            string fullURL = "window.open('" + url + "', '_blank' );";
            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", fullURL, true);

        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtEmpID.Text == "")
                {
                    objHrCommon.SiteID = Convert.ToInt32(ddlworksites.SelectedItem.Value);
                    objHrCommon.DeptID = Convert.ToDouble(ddldepartments.SelectedItem.Value);
                    try
                    {
                        if (Convert.ToInt32(ViewState["WSID"]) > 0)
                            objHrCommon.SiteID = Convert.ToInt32(ViewState["WSID"]);
                    }
                    catch { }
                    EmployeBind(objHrCommon);
                    //EmpListPaging.Visible = true;
                    try { gvPaySlip.FooterRow.Visible = true; }
                    catch { }

                    //bool Access = Convert.ToBoolean(ViewState["ViewAll"]);
                    //if (Access == true)
                    //{
                    //    EmployeBind(objHrCommon);
                    //}
                    //else
                    //{
                    //    
                    //}
                }
                else
                {
                    int EmpID = 0;
                    try { EmpID = Convert.ToInt32(txtEmpID.Text); }
                    catch { AlertMsg.MsgBox(Page, "Check the data you have given..!"); }
                    int Month = Convert.ToInt32(ddlMonth.SelectedValue);
                    int Year = Convert.ToInt32(ddlYear.SelectedValue);
                    try
                    {
                        //DataSet startdate = AttendanceDAC.GetStartDate();
                        //if (Convert.ToInt32(startdate.Tables[0].Rows[0][0]) != 1)
                        //{
                        //    // for Jan 2016 selection pay slip showing by Gana
                        if (Month == 1)
                        {
                            Month = 12;
                            Year = Year - 1;
                        }
                        else
                            Month = Month - 1;
                        // }
                    }
                    catch { }
                    BindEmp(EmpID, Month, Year);

                    //EmpListPaging.Visible = false;
                    try { gvPaySlip.FooterRow.Visible = false; }
                    catch { }
                }
            }
            catch (Exception ex)
            {
                clsErrorLog.HMSEventLog(ex, "Salaries", "btnSearch_Click", "005");
            }
            if (Convert.ToInt32(gvPaySlip.Rows.Count) == 1)
            {
                lnkViewAttendance.Visible = true;
            }
        }
        protected void rbActive_CheckedChanged(object sender, EventArgs e)
        {
            EmployeBind(objHrCommon);
        }
        protected void rbInActive_CheckedChanged(object sender, EventArgs e)
        {
            EmployeBind(objHrCommon);
        }
        protected void txtDay_TextChanged(object sender, EventArgs e)
        {
            EmployeBind(objHrCommon);
        }
        #endregion Events

        #region Methods
        void BindPager()
        {
            objHrCommon.PageSize = EmpListPaging.CurrentPage;
            objHrCommon.CurrentPage = EmpListPaging.ShowRows;
            EmployeBind(objHrCommon);
        }
        public int GetParentMenuId()
        {
            string URL = Request.Url.Segments[Request.Url.Segments.Length - 1] + Request.Url.Query;
            int RoleId = Convert.ToInt32(Session["RoleId"].ToString());
            int ModuleId = ModuleID; ;

            DataSet ds = new DataSet();

            ds = AttendanceDAC.GetAllowed(RoleId, ModuleId, URL);
            int MenuId = 0;
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                MenuId = Convert.ToInt32(ds.Tables[0].Rows[0]["Under"]);
                ViewState["Editable"] = Editable = (bool)ds.Tables[0].Rows[0]["Editable"];
                ViewState["ViewAll"] = (bool)ds.Tables[0].Rows[0]["ViewAll"];
                ViewState["ViewAll"] = (bool)ds.Tables[0].Rows[0]["ViewAll"];
                viewall = (bool)ViewState["ViewAll"];
                menuname = ds.Tables[0].Rows[0]["menuname"].ToString();
                menuid = MenuId.ToString();
                btnOutPutExcel.Enabled = (bool)ds.Tables[0].Rows[0]["Editable"];
                mid = Convert.ToInt32(ds.Tables[0].Rows[0]["MenuId"].ToString());
                //gvPaySlip.Columns[14].Visible = (bool)ds.Tables[0].Rows[0]["Editable"];
                //GVIndentStatus.Columns[7].Visible = Convert.ToBoolean(ds.Tables[0].Rows[0]["Editable"].ToString());
                //viewall = (bool)ViewState["ViewAll"];
                btnSync.Enabled = Editable;
            }
            return MenuId;
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
        protected void Worksidechangewithdep(object sender, EventArgs e)
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@Search", txtSearchWorksite.Text);
            FIllObject.FillDropDown(ref ddlworksites, "HR_GetGoogleSearchWorkSite_By_EmpSalaries", param);
            ListItem itmSelected = ddlworksites.Items.FindByText(txtSearchWorksite.Text);
            if (itmSelected != null)
            {
                ddlworksites.SelectedItem.Selected = false;
                itmSelected.Selected = true;
                //FillProjects();
            }
            ddlworksites_SelectedIndexChanged(sender, e);
            txtSearchdept.Text = "";
        }
        protected void GetDepartment(object sender, EventArgs e)
        {
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@Search", txtSearchdept.Text);
            param[1] = new SqlParameter("@CompanyID", Convert.ToInt32(Session["CompanyID"]));
            param[2] = new SqlParameter("@SiteID", ddlworksites.SelectedItem.Value);
            FIllObject.FillDropDown(ref ddldepartments, "HMS_googlesearch_GetDepartmentBySite", param);
            ListItem itmSelected = ddldepartments.Items.FindByText(txtSearchdept.Text);
            if (itmSelected != null)
            {
                ddldepartments.SelectedItem.Selected = false;
                itmSelected.Selected = true;
            }
        }
        public void BindEmpNatures()
        {
            DataSet ds = new DataSet();
            ds = Leaves.GetEmpNatureList(1);
            ddlEmpNature.DataSource = ds;
            ddlEmpNature.DataTextField = "Nature";
            ddlEmpNature.DataValueField = "NatureOfEmp";
            ddlEmpNature.DataBind();
            ddlEmpNature.Items.Insert(0, new ListItem("---All---", "0"));
        }
        public void BindEmp(int EmpID, int Month, int Year)
        {
            DataSet ds = new DataSet();
            AttendanceDAC ADAC = new AttendanceDAC();
            objHrCommon.SiteID = Convert.ToInt32(ddlworksites.SelectedItem.Value);
            objHrCommon.DeptID = Convert.ToDouble(ddldepartments.SelectedItem.Value);
            ds = ADAC.HR_EmpSalriesListByEmpID(EmpID, Month, Year, objHrCommon);

            gvPaySlip.DataSource = ds;
            gvPaySlip.DataBind(); try { gvPaySlip.FooterRow.Visible = false; }
            catch { }
            EmpListPaging.Visible = false;
        }
        public void BindYears()
        {
            ds = AttendanceDAC.GetCalenderYear();

            int i = 0;
            int Maxyear = Convert.ToInt32(ds.Tables[0].Rows[0]["MaxYear"].ToString());
            for (int Minyear = Convert.ToInt32(ds.Tables[0].Rows[0]["MinYear"].ToString()); Minyear <= Maxyear; Minyear++)
            {
                ddlYear.Items.Insert(i, new ListItem(Convert.ToString(Minyear), Convert.ToString(Minyear)));
                i = i + 1;
            }

            #region set defalult month and year
            //if we are changed to new year
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
            #endregion set defalult month and year
        }
        void EmployeBind(HRCommon objHrCommon)
        {
            try
            {
                objHrCommon.PageSize = EmpListPaging.ShowRows;
                objHrCommon.CurrentPage = EmpListPaging.CurrentPage;
                objHrCommon.SiteID = Convert.ToInt32(ddlworksites.SelectedItem.Value);
                objHrCommon.DeptID = Convert.ToInt32(ddldepartments.SelectedItem.Value);
                objHrCommon.Month = Convert.ToInt32(ddlMonth.SelectedItem.Value);
                objHrCommon.Year = Convert.ToInt32(ddlYear.SelectedItem.Value);
                //
                try
                {
                    if (Convert.ToInt32(ViewState["WSID"]) > 0)
                        objHrCommon.SiteID = Convert.ToInt32(ViewState["WSID"]);
                }
                catch { }
                // for Jan 2016 selection pay slip showing by Gana

                if (objHrCommon.Month == 1)
                {
                    objHrCommon.Month = 12;
                    objHrCommon.Year = objHrCommon.Year - 1;
                }
                else
                    objHrCommon.Month = objHrCommon.Month - 1;
                int? EmpNatureID = null;
                if (ddlEmpNature.SelectedValue != "0")
                    EmpNatureID = Convert.ToInt32(ddlEmpNature.SelectedValue);
                gvSalaDetails.DataSource = null;
                gvSalaDetails.DataBind();
                gvPaySlip.DataSource = null;
                gvPaySlip.DataBind();
                string Name = txtEmpName.Text;
                if (!rbView.Checked)
                {
                    gvSalaDetails.Visible = false;
                    gvPaySlip.Visible = true;
                    if (rbActive.Checked)
                        objHrCommon.CurrentStatus = 'y';
                    else
                        objHrCommon.CurrentStatus = 'n';
                    OrderID = Convert.ToInt32(ViewState["OrderID"]);
                    dsBind = (DataSet)objEmployee.GetEmployeesForSalariesWithHisID(objHrCommon, OrderID, Direction, Name, EmpNatureID, Convert.ToInt32(Session["CompanyID"]));
                    ViewState["NoOfRecords"] = objHrCommon.NoofRecords;
                    if (dsBind != null && dsBind.Tables.Count != 0 && dsBind.Tables[0].Rows.Count > 0)
                    {
                        gvPaySlip.DataSource = dsBind;
                        EmpListPaging.Bind(objHrCommon.CurrentPage, objHrCommon.TotalPages, objHrCommon.NoofRecords, objHrCommon.PageSize);
                        lblResultCount.Text = ViewState["NoOfRecords"].ToString() + " Items";
                        EmpListPaging.Visible = true;
                    }
                    else
                    {
                        gvPaySlip.EmptyDataText = "No Records Found";
                        EmpListPaging.Visible = false;
                    }
                    gvPaySlip.DataBind();
                }
                else
                {
                    gvSalaDetails.Visible = true;
                    gvPaySlip.Visible = false;
                    objHrCommon.CurrentStatus = 'y';
                    //string Name = txtEmpName.Text;
                    OrderID = Convert.ToInt32(ViewState["OrderID"]);
                    dsBind = (DataSet)Leaves.GetEmployeesForSalariesWithHisID(objHrCommon, OrderID, Direction, Name, EmpNatureID, Convert.ToInt32(Session["CompanyID"]));
                    ViewState["NoOfRecords"] = objHrCommon.NoofRecords;
                    if (dsBind != null && dsBind.Tables.Count != 0 && dsBind.Tables[0].Rows.Count > 0)
                    {
                        gvSalaDetails.DataSource = dsBind;
                        EmpListPaging.Bind(objHrCommon.CurrentPage, objHrCommon.TotalPages, objHrCommon.NoofRecords, objHrCommon.PageSize);
                        lblResultCount.Text = ViewState["NoOfRecords"].ToString() + " Items";
                        EmpListPaging.Visible = true;
                    }
                    else
                    {
                        gvSalaDetails.EmptyDataText = "No Records Found";
                        EmpListPaging.Visible = false;
                    }
                    gvSalaDetails.DataBind();

                }

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (GridViewRow grv in gvPaySlip.Rows)
                {

                    TextBox lblSpecial_Foot = (TextBox)grv.FindControl("lblSpecial");//PaySlipID // lblPaySlipID
                    Label lblPaySlipID = (Label)grv.FindControl("lblPaySlipID");//PaySlipID // lblPaySlipID
                    AttendanceDAC.HMS_EMPUpdateSalarySpecial(Convert.ToInt32(lblPaySlipID.Text), Convert.ToDecimal(lblSpecial_Foot.Text),  Convert.ToInt32(Session["UserId"]));
                }
                AlertMsg.MsgBox(Page, "Submited");
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
        public void BindWorkSites()
        {

            FIllObject.FillDropDown(ref ddlworksites, "HR_GetWorkSite_By_EmpSalaries");

            //try
            //{
            //    DataSet ds = new DataSet();
            //    ds = AttendanceDAC.GetWorkSite(0, '1', Convert.ToInt32(Session["CompanyID"]));
            //    ViewState["WorkSites"] = ds;
            //    if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0)
            //    {
            //        ddlworksites.DataSource = ds.Tables[0];
            //        ddlworksites.DataTextField = "Site_Name";
            //        ddlworksites.DataValueField = "Site_ID";
            //        ddlworksites.DataBind();
            //    }
            //    ddlworksites.Items.Insert(0, new ListItem("---ALL---", "0"));

            //}
            //catch (Exception e)
            //{
            //    throw e;
            //}
        }
        //public void BindDepartments()
        //{
        //    try
        //    {
        //        DataSet ds = new DataSet();
        //        ds = (DataSet)objRights.GetDaprtmentList();
        //        ViewState["Departments"] = ds;
        //        if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0)
        //        {
        //            ddldepartments.DataValueField = "DepartmentUId";
        //            ddldepartments.DataTextField = "DeptName";
        //            //ddldepartments.DataTextField = "DepartmentName";
        //            ddldepartments.DataSource = ds;
        //            ddldepartments.DataBind();
        //            ddldepartments.Items.Insert(0, new ListItem("---ALL---", "0"));
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        throw e;
        //    }
        //}


        //Grid Total For  NetAmount
        decimal TotalAmount = 0;
        protected string GetAmount()
        {
            return TotalAmount.ToString("N2");
        }
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
        //Grid Total For  NetAmount
        decimal TotalOTAmount = 0;
        protected string GetOTTotalAmount()
        {
            return TotalOTAmount.ToString("N2");
        }
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
        // 

        decimal TotalSpecialAmount = 0;
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
        //
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
        public void BindDeparmetBySite(int Site)
        {
            DataSet ds = AttendanceDAC.BindDeparmetBySite(Site, Convert.ToInt32(Session["CompanyID"]));
            ddldepartments.DataSource = ds;
            ddldepartments.DataTextField = "DeptName";
            ddldepartments.DataValueField = "DepartmentUId";
            ddldepartments.DataBind();
            ddldepartments.Items.Insert(0, new ListItem("---ALL---", "0", true));
        }
        public string DocNavigateUrl(string EmpId)
        {
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
            string Date;
            //string Date = "21" + "/" + ddlMonth.SelectedItem.Value + "/" + ddlYear.SelectedItem.Value;
            if (txtTodate.Text != String.Empty)
            {
                String[] srt = txtTodate.Text.Split('/');
                Date = srt[0] + "/" + Month + "/" + Year;
            }
            else
            {
                Date = ViewState["startdate"] + "/" + Month + "/" + Year;
            }
            string ReturnVal = "";
            ReturnVal = String.Format("PaySlipPreview.aspx?id={0}&Date={1}", EmpId, Date);
            return ReturnVal;
        }
        public static string DataRowTotable(DataRow dr)
        {
            return dr["Name"].ToString();
        }
        #endregion Methods
    }
}
