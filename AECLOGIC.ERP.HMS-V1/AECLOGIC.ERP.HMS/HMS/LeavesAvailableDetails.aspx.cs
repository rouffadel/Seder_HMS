using Aeclogic.Common.DAL;
using AECLOGIC.HMS.BLL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
namespace AECLOGIC.ERP.HMS.HMS
{
    public partial class LeavesAvailableDetails : AECLOGIC.ERP.COMMON.WebFormMaster
    {
        static string strurl = string.Empty; int stmonth = 0; int edmonth = 0;
        protected override void OnInit(EventArgs e)
        {
            ModuleID = 1;
            base.OnInit(e);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            //DataSet ds = new DataSet();
            try
            {
                if (!IsPostBack)
                {
                    //try { txtGrantedFrom.Text = DateTime.Today.ToString("dd MMM yyyy"); }
                    //catch { }
                    BindYears();
                    LoadData();
                    ViewState["RefUrl"] = Request.UrlReferrer.ToString();
                    Session["Empid"] = null;
                    // CalendarExtender1.EndDate = DateTime.Now;
                    // dtlWOProgress.Visible = false;
                }
            }
            catch (Exception ex)
            {
                clsErrorLog.HMSEventLog(ex, "LeaveAvailableDetails", "Page_Load", "001");
            }
        }
        public void BindYears()
        {
            try
            {
                DataSet ds = AECLOGIC.HMS.BLL.AttendanceDAC.HR_GetCalenderYears();
                if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ddlYear.DataValueField = "AssYearId";
                    ddlYear.DataTextField = "AssessmentYear";
                    ddlYear.DataSource = ds;
                    ddlYear.DataBind();
                    ddlYear.Items.Insert(0, new ListItem("--All--", "0"));
                    ddlYear.SelectedIndex = 0;// (ddlYear.Items.Count - 1);
                }
            }
            catch (Exception e)
            {
                //throw e;
            }
        }
        private void LoadData()
        {
            try
            {
                int EmpID = Convert.ToInt32(Request.QueryString["EMPID"]);
                int LID = Convert.ToInt32(Request.QueryString["LID"]);
                DataSet ds = Leaves.GetApplicableLeavesDetailsByPaging(EmpID, LID, Convert.ToInt32(ddlYear.SelectedValue));
                ViewState["DataSet"] = ds;
                dtlWOProgress.DataSource = ds;
                dtlWOProgress.DataBind();
                lblEMPName.Text = ds.Tables[2].Rows[0]["EmpName"].ToString(); 
                lblEMPDOJ.Text = ds.Tables[2].Rows[0]["DateOfJoin"].ToString();
                lblGrade.Text = ds.Tables[2].Rows[0]["Grade"].ToString(); ///lblclosingBal.Text = ds.Tables[2].Rows[0]["BalC"].ToString(); 
                txtempname_hid.Value = lblGrade.Text;
                txtempID.Value = Request.QueryString["EMPID"];
                lblLVRD.Text = ds.Tables[2].Rows[0]["LVRD"].ToString();
                if (lblLVRD.Text.Contains("LVRD"))
                    lblLVRD.ForeColor = System.Drawing.Color.Red;
                lblCalByPD.Text = ds.Tables[2].Rows[0]["PDDays"].ToString();
                btnTypeLea.Text = lblGrade.Text;
                if (lblGrade.Text.Contains("Set"))
                    lblGrade.Text = "";
                else
                    btnTypeLea.Text = "";
                lblActDt.Text = ds.Tables[2].Rows[0]["ActionDt"].ToString();
                if (lblActDt.Text.Contains("SET"))
                    lblActDt.ForeColor = System.Drawing.Color.Red;
                lnkActDt.Text = lblActDt.Text;
                if (lblActDt.Text.Contains("Set"))
                {
                    lblActDt.Text = "";
                    AlertMsg.MsgBox(Page, "SET  A/c Start Date");
                    btnsave.Enabled = false;
                }
                else
                    lnkActDt.Text = "";
                lnkLVRD.Text = lblLVRD.Text;
                if (lblLVRD.Text.Contains("Set"))
                {
                    lblLVRD.Text = "";
                    AlertMsg.MsgBox(Page, "SET LVRD ");
                    btnsave.Enabled = false;
                }
                else
                    lnkLVRD.Text = "";
            }
            catch { }
        }
        public string TotCrBal = "0.00";
        public string TotDrBal = "0.00";
        public DataView BindTransdetails(string TransId)
        {
            try
            {
                DataSet dstrans = (DataSet)ViewState["DataSet"];
                TotCrBal = Convert.ToDecimal(dstrans.Tables[1].Compute("Sum(Cr)", "ID='" + TransId + "'")).ToString("0.00");
                TotDrBal = Convert.ToDecimal(dstrans.Tables[1].Compute("Sum(Dr)", "ID='" + TransId + "'")).ToString("0.00");
                DataView dv = dstrans.Tables[1].DefaultView;
                dv.RowFilter = "ID='" + TransId + "'";
                return dv;
            }
            catch
            {
                DataSet dstrans = (DataSet)ViewState["DataSet"];
                DataView dv = null;
                return dv;
            }
        }
        protected void dtlWOProgress_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    Label lblCFWD = (Label)e.Item.FindControl("lblCFWD");
                    if (Convert.ToBoolean(lblCFWD.Text) == true)
                        lblCFWD.Text = "YES";
                    else
                        lblCFWD.Text = "NO";
                }
            }
            catch (Exception ex) { AlertMsg.MsgBox(Page, ex.Message.ToString(),AlertMsg.MessageType.Error); }
        }
        protected void gvEditTasks_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    try
                    {
                        HiddenField _hdn = (HiddenField)e.Row.FindControl("HidOpenID");
                        Label txtCr_lbl = (Label)e.Row.FindControl("txtCr_lbl");
                        TextBox txtCr = (TextBox)e.Row.FindControl("txtCr");
                        txtCr_lbl.Visible = false;
                        txtCr.Visible = true;
                        HiddenField HidOpenID_IsCr = (HiddenField)e.Row.FindControl("HidOpenID_IsCr");
                        if (Convert.ToBoolean(HidOpenID_IsCr.Value) == false)
                        {
                            txtCr_lbl.Visible = true;
                            txtCr.Visible = false;
                            LinkButton ID = (LinkButton)e.Row.FindControl("lnkEdit");
                            ID.Visible = false;
                        }
                        if (Convert.ToBoolean(_hdn.Value) == false)
                        {
                            txtCr_lbl.Visible = true;
                            txtCr.Visible = false;
                            LinkButton ID = (LinkButton)e.Row.FindControl("lnkEdit");
                            ID.Visible = false;
                            TextBox txtActionDt = (TextBox)e.Row.FindControl("txtActionDt");
                            txtActionDt.Visible = false;
                        }
                        else
                        {
                            TextBox txtActionDt = (TextBox)e.Row.FindControl("txtActionDt");
                            txtActionDt.Text = "01 JAN " + DateTime.Today.ToString("yyyy");
                            txtActionDt.ReadOnly = true;
                        }
                    }
                    catch { }
                }
            }
            catch (Exception ex)
            {
                clsErrorLog.HMSEventLog(ex, "LeaveAvailableDetails", "gvEditTasks_RowDataBound", "002");
            }
        }
        // protected void btnsave_Click(object sender, EventArgs e)
        protected void btnsave_Click(object sender, EventArgs e)
        {
            dtlWOProgress.Visible = true;
            btnsave.Enabled = false;
            try
            {
                try
                {
                    // CodeUtil.ConvertToDate(txtActionDt.Text, CodeUtil.DateFormat.ddMMMyyyy);
                    DateTime dtSync = DateTime.Today;
                    if (ddlYear.SelectedIndex == 0)
                        dtSync = DateTime.Today;
                    else
                    {
                        if (DateTime.Today.Year == Convert.ToInt32(ddlYear.SelectedItem.Text))
                            dtSync = DateTime.Today;
                        else
                        {
                            if (Convert.ToInt32(ddlYear.SelectedItem.Text) < 2016)
                            {
                                AlertMsg.MsgBox(Page, "System Start from year 2016. ");
                                return;
                            }
                            else
                            {
                                dtSync = CodeUtilHMS.ConvertToDate("31 DEC " + ddlYear.SelectedItem.Text, CodeUtilHMS.DateFormat.ddMMMyyyy);
                            }
                        }
                    }
                    int UID =  Convert.ToInt32(Session["UserId"]);
                    int EmpID = Convert.ToInt32(Request.QueryString["EMPID"]);
                    Leaves.HR_LeaveSyncCal(EmpID,
                         dtSync, UID);
                    AlertMsg.MsgBox(Page, "Refreshed");
                    LoadData();
                }
                catch (Exception ex)
                {
                    AlertMsg.MsgBox(Page, ex.Message.ToString(),AlertMsg.MessageType.Error);
                }
            }
            catch (Exception ex)
            {
                AlertMsg.MsgBox(Page, ex.Message.ToString(),AlertMsg.MessageType.Error);
            }
            btnsave.Enabled = true;
        }
        protected void gvEditTasks_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Edt") //1 for Update
                {
                    int LTID = Convert.ToInt32(e.CommandArgument);
                    GridViewRow gvr = (GridViewRow)((LinkButton)e.CommandSource).Parent.Parent;
                    HiddenField ID = (HiddenField)gvr.FindControl("HidOpenID");
                    if (Convert.ToBoolean(ID.Value) == true)
                    {
                        TextBox txtCr = (TextBox)gvr.FindControl("txtCr");
                        decimal Cr = Convert.ToDecimal(txtCr.Text);
                        decimal Dr = 0;// Convert.ToDecimal(txtDr.Text);
                        int EmpId =  Convert.ToInt32(Session["UserId"]);
                        TextBox txtActionDt = (TextBox)gvr.FindControl("txtActionDt");
                        DateTime ActionDate = CodeUtilHMS.ConvertToDate(txtActionDt.Text, CodeUtilHMS.DateFormat.ddMMMyyyy);
                        Leaves.InsUpdateTypeofLeaves(LTID, Cr, Dr, EmpId, ActionDate);
                        AlertMsg.MsgBox(Page, "Submit");
                        btnsave_Click(null, null);
                        //LoadData();
                    }
                }
                if (e.CommandName == "date") //1 for Update
                {
                    GridViewRow gvr = (GridViewRow)((LinkButton)e.CommandSource).Parent.Parent;
                    HiddenField Month = (HiddenField)gvr.FindControl("HidMonth");
                    HiddenField year = (HiddenField)gvr.FindControl("Hidyear");
                    int EmpID = Convert.ToInt32(Request.QueryString["EMPID"]);
                    ViewAttendance(Convert.ToInt32(Month.Value), Convert.ToInt32(year.Value), Convert.ToInt32(EmpID));
                }
            }
            catch (Exception ex)
            {
                AlertMsg.MsgBox(Page, ex.Message.ToString(),AlertMsg.MessageType.Error);
            }
        }
        private void ViewAttendance(int month1, int year2, int Empid)
        {
            try
            {
                //int month = month1;
                int monthtext = month1;
                int year = year2;
                int yeartext = year2; int year1 = year2;
                //int startdate = 1;
                DataSet startdate = AttendanceDAC.GetStartDate();
                string st = month1 + "/" + 1 + "/" + year2;
                //DateTime dt = DateTime.ParseExact(st, "MM/dd/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                DateTime dt = CODEUtility.ConvertToDate(st, DateFormat.MonthDayYear);// DateTime.ParseExact(st, "MM/dd/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                DateTime dtEnd = dt.AddMonths(1);
                int EmpNatureID = 0;
                int DepartmentID = 0, WorkSiteID = 0;
                DateTime StartDate = dt, EndDate = dtEnd;
                List<DateTime> dateList = new List<DateTime>();
                int DayInterval = 1;
                int TotalPages = 1;//returnVale
                int NoofRecords = 100;// return value
                int PageSize = 10;
                int CurrentPage = 1;
                try
                {
                    if (Convert.ToInt32(ViewState["WSID"]) > 0)
                        WorkSiteID = Convert.ToInt32(ViewState["WSID"]);
                }
                catch { }
                DataSet dsEPMData = AttendanceDAC.HR_GetAttandanceByPaging(Empid, WorkSiteID, DepartmentID, EmpNatureID, StartDate, EndDate
                    , CurrentPage, PageSize, ref NoofRecords, ref TotalPages, "",0);
                tblAtt.Rows.Clear();
                tblAtt.Style.Add("border", "solid red 1px");
                tblAtt.Style.Add("border-collapse", "collapse");
                //2 
                Boolean isFirst = true;
                TableRow tblHeadRow = new TableRow();
                TableRow tblDepartRow = new TableRow();
                TableRow tblRow;
                tblRow = new TableRow();
                int DeptID = 0;
                Hashtable ht = new Hashtable();
                int WidthP = 30;
                int WidthPName = 300;
                foreach (DataRow drEMP in dsEPMData.Tables[2].Rows)
                {
                    tblHeadRow = new TableRow();
                    if (isFirst)
                    {
                        TableRow rowNew = new TableRow();
                        tblAtt.Controls.Add(rowNew);
                        TableCell cellNew0 = new TableCell();
                        TableCell cellNew = new TableCell();
                        rowNew.Style.Add("border", " solid navy 1px");
                        cellNew.Style.Add("background-color", "#87cefa");
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
                        tblAtt.Rows.Add(tblHeadRow);
                    if (tblDepartRow != null)
                        tblAtt.Rows.Add(tblDepartRow);
                    tblAtt.Rows.Add(tblRow);
                    //string dtl = ddlMonth.SelectedItem.Value + "/0" + startdate + "/" + ddlYear.SelectedItem.Value;
                    //lblDates.Text = dtl;
                    isFirst = false;
                }
            }
            catch
            {
            }
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
        protected void btnback_Click(object sender, EventArgs e)
        {
            object refUrl = ViewState["RefUrl"];
            if (refUrl != null)
            {
                Session["Empid"] = Convert.ToInt32(Request.QueryString["EMPID"]);
                Response.Redirect((string)refUrl);
            }
        }
        protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            try { LoadData(); }
            catch { }
        }
        protected string FormatInput(object Status)
        {
            string retValue = "";
            string input = Status.ToString();
            if (input == "-1")
            {
                retValue = "--SELECT--";
            }
            if (input == "0")
            {
                retValue = "Applied";
            }
            if (input == "1")
            {
                retValue = "In-Process";
            }
            if (input == "2")
            {
                retValue = "Granted";
            }
            if (input == "3")
            {
                retValue = "Rejected";
            }
            return retValue;
        }
        protected void gvGranted_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if ((e.Row.DataItem as DataRowView)["Reason"].ToString() == "")
                {
                    LinkButton lnkreason = (LinkButton)e.Row.FindControl("lnkReason");
                    lnkreason.Visible = false;
                }
                e.Row.Cells[12].ToolTip = (e.Row.DataItem as DataRowView)["GrantedBy"].ToString();
                e.Row.Cells[7].ToolTip = (e.Row.DataItem as DataRowView)["Reason"].ToString();
                e.Row.Cells[16].ToolTip = (e.Row.DataItem as DataRowView)["LeaveName"].ToString();
            }
        }
        protected void lblLVRD_Click(object sender, EventArgs e)
        {
            tbllvrd.Visible = true;
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@EmpID", Convert.ToInt32(Request.QueryString["EMPID"]));
            param[1] = new SqlParameter("@CompanyID", CompanyID);
            DataSet ds = SQLDBUtil.ExecuteDataset("sh_VacationSettlementEmpInfo", param);
            if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0)
            {
                gvGranted.DataSource = ds.Tables[0];
            }
            else
            {
                gvGranted.DataSource = null;
            }
            gvGranted.DataBind();
        }
    }
}