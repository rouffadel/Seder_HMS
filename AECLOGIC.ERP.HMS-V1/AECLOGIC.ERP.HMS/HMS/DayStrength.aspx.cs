using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using AECLOGIC.HMS.BLL;
using System.Collections.Generic;
using AECLOGIC.ERP.COMMON;
using AECLOGIC.ERP.HMS.HRClasses;
using Aeclogic.Common.DAL;
namespace AECLOGIC.ERP.HMS.HMS
{
    public partial class DayStrength: AECLOGIC.ERP.COMMON.WebFormMaster
    {
        int mid = 0; int EMPIDPram = 0;
        bool viewall;
        string menuname;
        string menuid;
        static int CompanyID = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["CompanyID"].ToString());
        static int Role;
        static int EmpID;
        static int SiteSearch;
        static int status = 1;
        TableRow tblRow;
        TableCell tcName, tcDesig, tcDate, tcStatus, tcShift, tcEmpID, tcLnkbtn, tcScope, tcAbsent, tcOD, tcCL, tcEL, tcSL, tcLeavesApp,
            tcOBCL, tcOBEL, tcOBSL, tcCurCL, tcCurEL, tcCurSL, tcAdjLeaves, tcLOP, tcMarkedBy, tcUpdatedBy;
        static int ModID;
        static int Userid; int stmonth = 0; int edmonth = 0;

        AttendanceDAC objAtt = new AttendanceDAC();
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
           
            ModID = ModuleID;
        }
        void EmpListPaging_ShowRowsClick(object sender, EventArgs e)
        {
            EmpListPaging.CurrentPage = 1;
            GetAttandanceandDisplay();
        }
        void EmpListPaging_FirstClick(object sender, EventArgs e)
        {
            GetAttandanceandDisplay();
        }
        protected void Page_Load(object sender, EventArgs e)
        {

            //By Ravitgheja On 19-03-2016 for Details Dispaly Off The Panalities of atendence
            try
            {
                try
                {
                    string id = AECLOGIC.ERP.COMMON.clSession.cmnUserId.ToString();
                }
                catch
                {
                    Response.Redirect("Logon.aspx");
                }
                if (Request.QueryString["ID"] != null && Request.QueryString["ID"] != string.Empty)
                {

                    ViewState["ID"] = Convert.ToInt32(Request.QueryString["ID"].ToString());
                }
                if (Request.QueryString["ddlYear"] != null && Request.QueryString["ddlYear"] != string.Empty)
                {
                    ViewState["ddlYear"] = Convert.ToInt32(Request.QueryString["ddlYear"].ToString());
                }
                if (Request.QueryString["ddlMonth"] != null && Request.QueryString["ddlMonth"] != string.Empty)
                {
                    ViewState["ddlMonth"] = Convert.ToInt32(Request.QueryString["ddlMonth"].ToString());
                }
                SiteSearch = Convert.ToInt32(ddlWorksite_hid.Value == string.Empty ? "0" : ddlWorksite_hid.Value);
               
                Userid = Convert.ToInt32(AECLOGIC.ERP.COMMON.clSession.cmnUserId.ToString());
                if (!IsPostBack)
                {
                    try
                    {
                        txtDay.Text = DateTime.Now.ToString("dd MMM yyyy");
                        hdn.Value = "day";
                        EmpListPaging.Visible = false;
                        ViewState["dtExpExcel"] = string.Empty;
                        try
                        {
                            ViewState["WSID"] = 0;
                            if (Convert.ToInt32(Session["RoleId"].ToString()) == 7)
                            {
                                try
                                {
                                    DataSet ds = clViewCPRoles.HR_DailyAttStatus(Convert.ToInt32(AECLOGIC.ERP.COMMON.clSession.cmnUserId));
                                    ViewState["WSID"] = ds.Tables[0].Rows[0]["ID"].ToString();
                                    txtSearchWorksite.Text = ds.Tables[0].Rows[0]["Name"].ToString();
                                    txtSearchWorksite.ReadOnly = true;
                                }
                                catch { }
                            }
                            if (Session["Type"].ToString() != "1" && Session["Site"].ToString() != "1")
                            {
                            }
                            else
                            {
                            }
                        }
                        catch
                        {
                            
                        }
                        if (Request.QueryString.Count > 0)
                        {
                            GetMonthlyReport();
                        }
                        else
                        {
                            try
                            {
                                GetDayReport();
                            }
                            catch
                            {
                               
                            }
                        }
                        btnDaySearch.Attributes.Add("onclick", "javascript:return Validate('" + DateTime.Now.ToString("dd MMM yyyy") + "','" + txtDay.Text + "');");
                        try { BindYears(); }
                        catch { AlertMsg.MsgBox(Page, "Unable to bind Years..!"); }
                        tblAttStatus.Visible = true;
                        try
                        {
                            BindTodayStatus(DateTime.Now, 0, 0,0);
                        }
                        catch
                        {
                            AlertMsg.MsgBox(Page, "Unable to bind Today Status..!");
                        }
                    }
                    catch
                    {
                        AlertMsg.MsgBox(Page, "Unable to bind..!");
                    }


                    if (Request.QueryString["Empid"] != null && Request.QueryString["Empid"] != string.Empty)
                    {
                        EMPIDPram = Convert.ToInt32(Request.QueryString["Empid"]);
                        ddlMonth.SelectedValue = Request.QueryString["Month"];
                        ddlYear.SelectedValue = Request.QueryString["Year"].ToString();
                        btnSearch_Click(null, null);

                       
                    }

                }
            }
            catch
            {

            }
        }
        
   

        protected void GetDayReport()
        {
            
            EmpListPaging.Visible = false;
           // DataSet ds = new DataSet();
            AttendanceDAC objAtt = new AttendanceDAC();
            TimeSpan Work;
            DateTime Intime = DateTime.Now, OutTime = DateTime.Now;
            string URL = Request.Url.Segments[Request.Url.Segments.Length - 1] + Request.Url.Query;
            int RoleId = Convert.ToInt32(Session["RoleId"].ToString());
            int ModuleId = ModuleID; ;

            DataSet ds = AttendanceDAC.GetAllowed(RoleId, ModuleId, URL);
            if (txtDay.Text != string.Empty)
            {
                DateTime Date = CodeUtilHMS.ConvertToDate_ddMMMyyy(txtDay.Text, CodeUtilHMS.DateFormat.ddMMMyyyy);
                int WorkSiteID = Convert.ToInt32(ddlWorksite_hid.Value == string.Empty ? "0" : ddlWorksite_hid.Value);
                try
                {
                    if (Convert.ToInt32(ViewState["WSID"]) > 0)
                        WorkSiteID = Convert.ToInt32(ViewState["WSID"]);
                }
                catch { }
                // int DayInterval = 1;
                int TotalPages = 1;//returnVale
                int NoofRecords = 100;// return value
                int PageSize = EmpListPaging.ShowRows;
                int CurrentPage = EmpListPaging.CurrentPage;
                if ((Convert.ToBoolean(ds.Tables[0].Rows[0]["ViewAll"].ToString()) == false))
                {

                    //ddlDepartment.Visible = false; ddlWorksite.Visible = false;
                    txtEmpName.Enabled = txtEmpID.Enabled = false;
                    int empid = Convert.ToInt32(AECLOGIC.ERP.COMMON.clSession.cmnUserId);
                    txtEmpID.Text = empid.ToString();
                    string Name = txtEmpName.Text;


                    ds = objAtt.GetAttendanceByDay_Cursor(Date, Convert.ToInt32(ddlDepartment_hid.Value == string.Empty ? "0" : ddlDepartment_hid.Value), WorkSiteID, empid, Name, Convert.ToInt32(Session["CompanyID"]), CurrentPage, PageSize, ref NoofRecords, ref TotalPages,0);
                }
                else
                {
                    if (txtEmpName.Text == string.Empty || txtEmpName.Text == null)
                    {
                        txtEmpNameHidden.Value = string.Empty;
                    }
                    string Name = null;
                    int empid;
                    if (txtEmpID.Text == string.Empty || txtEmpID.Text == string.Empty)
                    {
                        empid = 0;
                    }
                    else
                    {
                        empid = Convert.ToInt32(txtEmpID.Text);
                    }

                    if (txtEmpName.Text != string.Empty || txtEmpName.Text != null)
                    {
                        empid = Convert.ToInt32(txtEmpNameHidden.Value == string.Empty ? "0" : txtEmpNameHidden.Value);
                    }
                    ds = objAtt.GetAttendanceByDay_Cursor(Date, Convert.ToInt32(ddlDepartment_hid.Value == string.Empty ? "0" : ddlDepartment_hid.Value), WorkSiteID, empid, Name, Convert.ToInt32(Session["CompanyID"]), CurrentPage, PageSize, ref NoofRecords, ref TotalPages,0);

                 
                }
            }
            int count = ds.Tables.Count;
            if (ds != null && ds.Tables.Count > 0)
            {
                tblRow = new TableRow();
               
                tcName = new TableCell();
                tcName.Text = "Name";
                tcName.Style.Add("font-weight", "bold");
                tcName.Width = 200;
                tblRow.Cells.Add(tcName);

                tcStatus = new TableCell();
                tcStatus.Text = "Status";
                tcStatus.Style.Add("font-weight", "bold");
                tcStatus.Width = 120;
                tblRow.Cells.Add(tcStatus);

                tcDate = new TableCell();
                tcDate.Text = "Shift";
                tcDate.Style.Add("font-weight", "bold");
                tcDate.Width = 80;
                tblRow.Cells.Add(tcDate);

                tcDate = new TableCell();
                tcDate.Text = "In Time";
                tcDate.Style.Add("font-weight", "bold");
                tcDate.Width = 80;
                tblRow.Cells.Add(tcDate);

                tcDate = new TableCell();
                tcDate.Text = "Out Time";
                tcDate.Style.Add("font-weight", "bold");
                tcDate.Width = 80;
                tblRow.Cells.Add(tcDate);

                tcDate = new TableCell();
                tcDate.Text = "Total Work";
                tcDate.Style.Add("font-weight", "bold");
                tcDate.Width = 90;
                tblRow.Cells.Add(tcDate);

                tcStatus = new TableCell();
                tcStatus.Text = "Remarks";
                tcStatus.Style.Add("font-weight", "bold");
                tcStatus.Width = 150;
                tblRow.Cells.Add(tcStatus);

                tcMarkedBy = new TableCell();
                tcMarkedBy.Text = "Marked By";
                tcMarkedBy.Style.Add("font-weight", "bold");
                tcMarkedBy.Width = 50;

                tblRow.Cells.Add(tcMarkedBy);

                tcUpdatedBy = new TableCell();
                tcUpdatedBy.Text = "Updated By";
                tcUpdatedBy.Style.Add("font-weight", "bold");
                tcUpdatedBy.Width = 50;
                tblRow.Cells.Add(tcUpdatedBy);


                tblAtt.Rows.Add(tblRow);


                string Department = String.Empty;

                for (int j = 0; j < count; j++)
                {
                    tblRow = new TableRow();
                    tcName = new TableCell();
                    if (ds.Tables[j].Rows.Count != 0)
                    {
                        tcName.Text = ds.Tables[j].Rows[0][3].ToString();
                        tcName.Width = 200;
                        tblRow.Cells.Add(tcName);

                        tcStatus = new TableCell();
                        tcStatus.Text = ds.Tables[j].Rows[0][1].ToString();
                        tcStatus.Width = 80;
                        tblRow.Cells.Add(tcStatus);

                        tcShift = new TableCell();
                        try { tcShift.Text = ds.Tables[j].Rows[0][8].ToString(); }
                        catch { }
                        tcShift.Width = 80;
                        tblRow.Cells.Add(tcShift);

                        tcDate = new TableCell();
                        if (ds.Tables[j].Rows[0][4].ToString() != string.Empty)
                        {
                            tcDate.Text = ds.Tables[j].Rows[0][4].ToString();
                            Intime = Convert.ToDateTime(ds.Tables[j].Rows[0][4].ToString());
                        }
                        else
                            tcDate.Text = "-";
                        tcDate.Width = 80;
                        tblRow.Cells.Add(tcDate);

                        tcDate = new TableCell();
                        if (ds.Tables[j].Rows[0][5].ToString() != string.Empty)
                        {
                            tcDate.Text = ds.Tables[j].Rows[0][5].ToString();
                            OutTime = Convert.ToDateTime(ds.Tables[j].Rows[0][5].ToString());
                        }
                        else
                            tcDate.Text = "-";
                        tcDate.Width = 80;
                        tblRow.Cells.Add(tcDate);
                        Work = OutTime.Subtract(Intime);

                        tcDate = new TableCell();
                        if (ds.Tables[j].Rows[0][5].ToString() != string.Empty || ds.Tables[j].Rows[0][4].ToString() != string.Empty)
                        {
                            tcDate.Text = Work.ToString(@"hh\:mm") + " Hrs";
                        }
                        if (ds.Tables[j].Rows[0][5].ToString() == string.Empty || ds.Tables[j].Rows[0][4].ToString() == string.Empty)
                        {
                            tcDate.Text = "00:00 Hrs";
                        }
                        tcDate.Width = 80;
                        tblRow.Cells.Add(tcDate);

                        tcDate = new TableCell();
                        if (ds.Tables[j].Rows[0][7].ToString() != string.Empty)
                            tcDate.Text = ds.Tables[j].Rows[0][7].ToString();
                        else
                            tcDate.Text = "NA";
                        tcDate.Width = 80;
                        tblRow.Cells.Add(tcDate);
                        //tblRow.Cells.Add(tcEmpID);



                        tcMarkedBy = new TableCell();
                        tcMarkedBy.Text = ds.Tables[j].Rows[0]["MarkedByid"].ToString();
                        tcMarkedBy.ToolTip = ds.Tables[j].Rows[0]["MarkedBy"].ToString();
                        tcMarkedBy.Width = 40;
                        tblRow.Cells.Add(tcMarkedBy);

                        tcUpdatedBy = new TableCell();
                        tcUpdatedBy.Text = ds.Tables[j].Rows[0]["UpdatedByid"].ToString();
                        tcUpdatedBy.ToolTip = ds.Tables[j].Rows[0]["UpdatedBy"].ToString();
                        tcUpdatedBy.Width = 40;
                        tblRow.Cells.Add(tcUpdatedBy);
                        try
                        {
                            tcDate = new TableCell();
                            tcDate.Text = " ";
                            tcDate.Width = 80;
                            tblRow.Cells.Add(tcDate);//,Att.AttID_Hr, isnull((select count(*) from T_HR_Attendance_Log where AttID=Att.AttID_Hr),0) as attCount
                            if (Convert.ToInt32(ds.Tables[j].Rows[0]["attCount"]) > 0)
                            {
                                LinkButton btnEliminaOrdine = new LinkButton();
                                btnEliminaOrdine.ID = "att_ID_" + ds.Tables[j].Rows[0]["AttID_Hr"].ToString();
                                btnEliminaOrdine.CssClass = "btn btn-primary";
                                btnEliminaOrdine.Text = "View";
                                //btnEliminaOrdine.Click += new System.EventHandler(this.btnEliminaOrdine_Click);
                                tcDate.Controls.Add(btnEliminaOrdine);
                            }
                        }
                        catch { }

                        tblAtt.Rows.Add(tblRow);


                    }
                }
            }


        }

        void GetAttandanceandDisplay()
        {
            try
            {
               
                try
                {
                    if (textdept.Text.Trim() == string.Empty)
                        ddlDepartment_hid.Value = "0";
                    if (txtSearchWorksite.Text.Trim() == string.Empty)
                        ddlWorksite_hid.Value = "0";
                }
                catch { }

                if (txtEmpName.Text == string.Empty || txtEmpName.Text == null)
                {
                    txtEmpNameHidden.Value = string.Empty;
                }

                string Name = null;
                try
                {
                    if (EMPIDPram == 0 || EMPIDPram == null)
                    {
                        if (txtEmpID.Text.Trim() != string.Empty)
                            EMPIDPram = Convert.ToInt32(txtEmpID.Text);
                        if (txtEmpName.Text != string.Empty || txtEmpName.Text != null)
                        {
                            EMPIDPram = Convert.ToInt32(txtEmpNameHidden.Value == string.Empty ? "0" : txtEmpNameHidden.Value);
                        }
                        Name = string.Empty;
                    }
                }
                catch { }
                DateTime Date = CodeUtilHMS.ConvertToDate_ddMMMyyy(txtDay.Text.Trim(), CodeUtilHMS.DateFormat.ddMMMyyyy);
                    String Day = Date.Day.ToString();
                     int Day1 = Convert.ToInt32(Day);
                     String MM = Date.Month.ToString();
                     int month = Convert.ToInt32(MM);
                     String YY = Date.Year.ToString();
                     int year = Convert.ToInt32(YY);
                int monthtext = month;
                int yeartext = year; int year1 = year;
                int startdate = 1;
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
                try { DepartmentID = Convert.ToInt32(ddlDepartment_hid.Value == string.Empty ? "0" : ddlDepartment_hid.Value); }
                catch { } try { WorkSiteID = Convert.ToInt32(ddlWorksite_hid.Value == string.Empty ? "0" : ddlWorksite_hid.Value); }
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
                DataSet dsEPMData = AttendanceDAC.HR_GetAttandanceDayStrength(EMPIDPram, WorkSiteID, DepartmentID, EmpNatureID, StartDate, EndDate
                    , CurrentPage, PageSize, ref NoofRecords, ref TotalPages, Name);

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
                        //TableCell cellNew1 = new TableCell();
                        //cellNew.ColumnSpan = 11;
                        //cellNew1.ColumnSpan = 20;

                        rowNew.Style.Add("border", " solid navy 1px");
                        //cellNew.Style.Add("background-color", "#87cefa");                       
                        //cellNew1.Style.Add("background-color", "#87cefa");
                        cellNew.Style.Add("font-weight", "bold");
                        //cellNew1.Style.Add("font-weight", "bold");
                        cellNew.Style.Add("Text-align", "Center");
                        //cellNew1.Style.Add("Text-align", "Center");

                        for (int row = 0; row < 1; row++)
                        {
                            for (int col = 0; col < 3; col++)
                            {
                                if (col > 0)
                                {
                                    switch (Convert.ToInt32(monthtext))
                                    {

                                        case 1:
                                            cellNew0.Text = string.Empty.ToString();
                                            //int year1 = year + 1;                                         
                                            cellNew.Text = "January".ToString() + " " + year1;
                                            cellNew.ColumnSpan = 20;
                                            break;
                                        case 2:
                                            cellNew0.Text = string.Empty.ToString();
                                            cellNew.Text = "February".ToString() + " " + year;
                                            cellNew.ColumnSpan = 20;
                                            break;
                                        case 3:
                                            cellNew0.Text = string.Empty.ToString();
                                            cellNew.Text = "March".ToString() + " " + year;
                                            cellNew.ColumnSpan = 20;
                                            break;
                                        case 4:
                                            cellNew0.Text = string.Empty.ToString();
                                            cellNew.Text = "April".ToString() + " " + year;
                                            cellNew.ColumnSpan = 20;
                                            break;
                                        case 5:
                                            cellNew0.Text = string.Empty.ToString();
                                            cellNew.Text = "May".ToString() + year;
                                            cellNew.ColumnSpan = 20;
                                            break;
                                        case 6:
                                            cellNew0.Text = string.Empty.ToString();
                                            cellNew.Text = "June".ToString() + " " + year;
                                            cellNew.ColumnSpan = 20;
                                            break;
                                        case 7:
                                            cellNew0.Text = string.Empty.ToString();
                                            cellNew.Text = "July".ToString() + " " + year;
                                            cellNew.ColumnSpan = 20;
                                            break;
                                        case 8:
                                            cellNew0.Text = string.Empty.ToString();
                                            cellNew.Text = "Augest".ToString() + " " + year;
                                            cellNew.ColumnSpan = 20;
                                            break;
                                        case 9:
                                            cellNew0.Text = string.Empty.ToString();
                                            cellNew.Text = "September".ToString() + " " + year;
                                            cellNew.ColumnSpan = 20;
                                            break;
                                        case 10:
                                            cellNew0.Text = string.Empty.ToString();
                                            cellNew.Text = "October".ToString() + " " + year;
                                            cellNew.ColumnSpan = 20;
                                            break;
                                        case 11:
                                            cellNew0.Text = string.Empty.ToString();
                                            cellNew.Text = "November".ToString() + " " + year;
                                            cellNew.ColumnSpan = 20;
                                            break;
                                        case 12:
                                            cellNew0.Text = string.Empty.ToString();
                                            cellNew.Text = "December".ToString() + " " + year;
                                            cellNew.ColumnSpan = 20;
                                            break;
                                        default:
                                            cellNew.Text = string.Empty.ToString();
                                            break;
                                    }

                                }
                                else
                                    cellNew.Text = string.Empty.ToString();
                              
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
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionList(string prefixText, int count, string contextKey)
        {
            DataSet ds = AttendanceDAC.GetWorkSites(prefixText, CompanyID, Userid, ModID);

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
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionList_dept(string prefixText, int count, string contextKey)
        {
            DataSet ds = AttendanceDAC.BindDeparmetBySite_googlesearch(prefixText, SiteSearch, CompanyID);
            DataTable dt = ds.Tables[0];
            List<string> items = new List<string>(count);
            var rtval = new Dictionary<string, string>();
            foreach (DataRow row in dt.Rows)
            {
                string str = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(row["DepartmentName"].ToString(), row["DepartmentUId"].ToString());
                items.Add(str);
            }
            return items.ToArray(); ;// txtItems.ToArray();

        }

        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionList_empnature(string prefixText, int count, string contextKey)
        {
            DataSet ds = Leaves.GetEmpNaturelist_Searchworksite(prefixText, status);
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
        protected void btnDaySearch_Click(object sender, EventArgs e)
        {
            try
            {
                gvAttLog.DataSource = null;
                gvAttLog.DataBind();
               // tblAttStatus.Visible = true;
                hdn.Value = "day";
                GetDayReport();
                DateTime Date = CodeUtilHMS.ConvertToDate_ddMMMyyy(txtDay.Text.Trim(), CodeUtilHMS.DateFormat.ddMMMyyyy);

                BindTodayStatus(Date, Convert.ToInt32(ddlDepartment_hid.Value == string.Empty ? "0" : ddlDepartment_hid.Value), Convert.ToInt32(ddlWorksite_hid.Value == string.Empty ? "0" : ddlWorksite_hid.Value),0);
            }
            catch (Exception ex)
            {
                clsErrorLog.HMSEventLog(ex, "ViewAttendance", "btnDaySearch_Click", "002");
            }
        }
        public void BindTodayStatus(DateTime Date, int DeptID, int Site,int projectid)
        {
            try
            {

                if (Convert.ToInt32(ViewState["WSID"]) > 0)
                    Site = Convert.ToInt32(ViewState["WSID"]);
            }
            catch { }
            DataSet ds = AttendanceDAC.HR_DailyAttStatus(Date, DeptID, Site, Convert.ToInt32(Session["CompanyID"]), projectid);

            grdAttStatusCount.DataSource = ds;
            grdAttStatusCount.DataBind();
            DayWSWiseReport();
        }
        public string TotNoEMP = string.Empty;
        public string TotNoofPresent = string.Empty;
        public string TotNoofAbsent = string.Empty;
        public string TotNoofother = string.Empty;
        public string TotUnMarked_rt = string.Empty;
        public string TotMarked_rt = string.Empty;
        void DayWSWiseReport()
        {
            try
            {
                int? WSID = null;
                DateTime dtToday = CodeUtilHMS.ConvertToDate_ddMMMyyy(txtDay.Text, CodeUtilHMS.DateFormat.ddMMMyyyy);
                try
                {

                    if (Convert.ToInt32(ViewState["WSID"]) > 0)
                        WSID = Convert.ToInt32(ViewState["WSID"]);
                }
                catch { }
                DataSet dsws = Leaves.HMS_RPT_GetWorkSitewiseAtt(WSID, dtToday);
                foreach (DataRow item in dsws.Tables[0].Rows)
                {
                    try
                    {
                        item["Marked_rt"] = Convert.ToInt32(item["NoofPresent"]) + Convert.ToInt32(item["NoofAbsent"]) + Convert.ToInt32(item["Noofother"]);
                        item["UnMarked_rt"] = Convert.ToInt32(item["NoofEMP"]) - Convert.ToInt32(item["Marked_rt"]);
                    }
                    catch { }
                }
                try
                {
                    TotNoEMP = dsws.Tables[0].Compute("Sum(NoofEMP)", string.Empty).ToString();
                    TotNoofPresent = dsws.Tables[0].Compute("Sum(NoofPresent)", string.Empty).ToString();
                    TotNoofAbsent = dsws.Tables[0].Compute("Sum(NoofAbsent)", string.Empty).ToString();
                    TotNoofother = dsws.Tables[0].Compute("Sum(Noofother)", string.Empty).ToString();
                    TotUnMarked_rt = dsws.Tables[0].Compute("Sum(UnMarked_rt)", string.Empty).ToString();
                    TotMarked_rt = dsws.Tables[0].Compute("Sum(Marked_rt)", string.Empty).ToString();
                }
                catch { }
                gdWSRPT.DataSource = dsws.Tables[0];
                gdWSRPT.DataBind();
            }
            catch { }
        }
        protected void btnMPrint_Click(object sender, EventArgs e)
        {
            try
            {
                int WSID, DeptID, Month, Year, Empid;
                WSID = Convert.ToInt32(ddlWorksite_hid.Value == string.Empty ? "0" : ddlWorksite_hid.Value);
                DeptID = Convert.ToInt32(ddlDepartment_hid.Value == string.Empty ? "0" : ddlDepartment_hid.Value);
                Month = Convert.ToInt32(ddlMonth.SelectedValue);
                Year = Convert.ToInt32(ddlYear.SelectedValue);
                if (txtEmpID.Text == string.Empty || txtEmpID.Text == String.Empty)
                    Empid = 0;
                else
                    Empid = Convert.ToInt32(txtEmpID.Text);


                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "pop", "window.open('AttendancePrint.aspx?id=" + 2 + "&WSID=" + WSID + "&DeptID=" + DeptID + "&Month=" + Month + "&Year=" + Year + "&Empid=" + Empid + "' , '_blank')", true);
                 }
            catch (Exception ex)
            {
                clsErrorLog.HMSEventLog(ex, "ViewAttendance", "btnMPrint_Click", "008");
            }
        }
        protected void GetWork(object sender, EventArgs e)
        {
            SiteSearch = Convert.ToInt32(ddlWorksite_hid.Value == string.Empty ? "0" : ddlWorksite_hid.Value);
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
        protected void ddlMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                hdn.Value = "MON";
                DataSet startdate = AttendanceDAC.GetStartDate();

                string stdt = ddlMonth.SelectedItem.Value + "/" + startdate.Tables[0].Rows[0][0].ToString() + "/" + ddlYear.SelectedItem.Value;


                lblDates.Text = stdt;
                GetMonthlyReport();
            }
            catch (Exception ex)
            {
                clsErrorLog.HMSEventLog(ex, "ViewAttendance", "ddlMonth_SelectedIndexChanged", "005");
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

            ddlMonth.SelectedValue = DateTime.Now.ToString("MM");

            // ddlMonth.SelectedValue = ds.Tables[0].Rows[0]["CurrentMonth"].ToString();
            ddlYear.SelectedValue = ds.Tables[0].Rows[0]["CurrentYear"].ToString();

        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            //tblAttStatus.Visible = false;
            GetAttandanceandDisplay();
        }
        protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                hdn.Value = "MON";
                DataSet startdate = AttendanceDAC.GetStartDate();
                string dt = ddlMonth.SelectedItem.Value + "/" + startdate.Tables[0].Rows[0][0].ToString() + "/" + ddlYear.SelectedItem.Value;
                lblDates.Text = dt;
                GetMonthlyReport();
            }
            catch (Exception ex)
            {
                clsErrorLog.HMSEventLog(ex, "ViewAttendance", "ddlYear_SelectedIndexChanged", "006");
            }
        }
        protected void GetMonthlyReport()
        {
           
            return;

            int month = Convert.ToInt32(ddlMonth.SelectedValue == string.Empty ? "0" : ddlMonth.SelectedValue);
            int year = Convert.ToInt32(ddlYear.SelectedValue == string.Empty ? "0" : ddlYear.SelectedValue);
            tblAtt.Rows.Clear();
            DataSet ds = new DataSet();
            AttendanceDAC objAtt = new AttendanceDAC();
           
            int? EmpNatureID = null;
           
            if (Session["RoleId"].ToString() == "9" || Session["RoleId"].ToString() == "11")
           
            {
                //By Ravitgheja On 19-03-2016 for Details Dispaly Off The Panalities of atendenceKC
                txtEmpName.Enabled = txtEmpID.Enabled = false;
                int empid = Convert.ToInt32(AECLOGIC.ERP.COMMON.clSession.cmnUserId);
                if (ViewState["ID"] != string.Empty && ViewState["ID"] != string.Empty)
                {
                    empid = Convert.ToInt32(ViewState["ID"]);
                }
                if (ViewState["ddlMonth"] != string.Empty && ViewState["ddlMonth"] != string.Empty)
                {
                    month = Convert.ToInt32(ViewState["ddlMonth"]);
                }
                if (ViewState["ddlYear"] != string.Empty && ViewState["ddlYear"] != string.Empty)
                {
                    year = Convert.ToInt32(ViewState["ddlYear"]);
                }
                string Name = txtEmpName.Text;
                ds = objAtt.GetAttendanceByMonth_Cursor(month, year, Convert.ToInt32(ddlDepartment_hid.Value == string.Empty ? "0" : ddlDepartment_hid.Value), Convert.ToInt32(ddlWorksite_hid.Value == string.Empty ? "0" : ddlWorksite_hid.Value), empid, Name, EmpNatureID);
                // ds = objAtt.GetAttendanceByMonth_Cursor(month, year, Convert.ToInt32(ddlWorksite.SelectedValue), Convert.ToInt32(ddlDepartment.SelectedValue), empid, Name, EmpNatureID);//comented by nadeem using text box.instead of ddl(above lines)on 22/03/2016
            }
            else
            {
                int empid = Convert.ToInt32(AECLOGIC.ERP.COMMON.clSession.cmnUserId);

                // changed by Gana for selection 2016 JAN
                if (month == 1)
                {
                    month = 12;
                    year = year - 1;
                }
                else
                    month = month - 1;

                if (txtEmpID.Text == string.Empty || txtEmpID.Text == string.Empty)
                {
                    if (Session["RoleName"].ToString() == "Administrator")
                        empid = 0;
                    else
                        empid = Convert.ToInt32(AECLOGIC.ERP.COMMON.clSession.cmnUserId);
                }
                else
                {
                    empid = Convert.ToInt32(txtEmpID.Text);
                }

                string Name = txtEmpName.Text;
                ds = objAtt.GetAttendanceByMonth_Cursor(month, year, Convert.ToInt32(ddlDepartment_hid.Value == string.Empty ? "0" : ddlDepartment_hid.Value), Convert.ToInt32(ddlWorksite_hid.Value == string.Empty ? "0" : ddlWorksite_hid.Value), empid, Name, EmpNatureID);
                }

            string Department = String.Empty;

            int count = ds.Tables.Count;
            if (ds != null && ds.Tables.Count != 0)
            {
                //gvHolidays.DataSource = ds.Tables[ds.Tables.Count - 1];
                //gvHolidays.DataBind();

                System.Collections.Generic.List<DateTime> listHolidays = new System.Collections.Generic.List<DateTime>();
                foreach (DataRow dr in ds.Tables[ds.Tables.Count - 1].Rows)
                {
                    listHolidays.Add(Convert.ToDateTime(dr["Date"]));
                }

                Double Present = 0; int Scope = 0; int Absent = 0; int OD = 0; int CL = 0; int EL = 0; int SL = 0; int LA = 0;
                double OBCL, OBEL, OBSL, CCL, CEL, CSL, AL, LOP;
                OBCL = OBEL = OBSL = CCL = CEL = CSL = AL = LOP = 0;
                if (month != 0)
                {
                    DataSet startdate = AttendanceDAC.GetStartDate();
                    string st = month + "/" + startdate.Tables[0].Rows[0][0].ToString() + "/" + year;

                    DateTime dt = CODEUtility.ConvertToDate(st, DateFormat.DayMonthYear);
                    DateTime dtEnd = dt.AddMonths(1);
                    tblRow = new TableRow();

                    #region CellsCreation
                    tcName = new TableCell();
                    tcName.Text = "Name";
                    tcName.Style.Add("font-weight", "bold");
                    tcName.Width = 300;
                    tblRow.Cells.Add(tcName);

                    int monthlastday = DateTime.DaysInMonth(Convert.ToInt32(ddlYear.SelectedValue), Convert.ToInt32(ddlMonth.SelectedValue));
                    for (int i = Convert.ToInt32(startdate.Tables[0].Rows[0][0].ToString()); i <= monthlastday; i++)
                    {
                        tcDate = new TableCell();
                        tcDate.Text = i.ToString();
                        tcDate.Style.Add("font-weight", "bold");
                        tcDate.Width = 60;
                        tblRow.Cells.Add(tcDate);
                        dt = dt.AddDays(1);
                    }


                    for (int i = 1; i < Convert.ToInt32(startdate.Tables[0].Rows[0][0].ToString()); i++)
                    {
                        tcDate = new TableCell();
                        tcDate.Text = i.ToString();
                        tcDate.Style.Add("font-weight", "bold");
                        tcDate.Width = 60;
                        tblRow.Cells.Add(tcDate);
                        dt = dt.AddDays(1);
                    }

                    tcScope = new TableCell();
                    tcScope.Text = "Scope";
                    tcScope.Style.Add("font-weight", "bold");
                    tcScope.Width = 60;
                    tblRow.Cells.Add(tcScope);
                    tblAtt.Rows.Add(tblRow);
                    tblAtt.Controls.Add(tblRow);

                    tcDate = new TableCell();
                    tcDate.Text = "P";
                    tcDate.Style.Add("font-weight", "bold");
                    tcDate.Width = 60;
                    tblRow.Cells.Add(tcDate);
                    tblAtt.Rows.Add(tblRow);

                    tcAbsent = new TableCell();
                    tcAbsent.Text = "A";
                    tcAbsent.Style.Add("font-weight", "bold");
                    tcAbsent.Width = 60;
                    tblRow.Cells.Add(tcAbsent);
                    tblAtt.Rows.Add(tblRow);

                    tcOD = new TableCell();
                    tcOD.Text = "OD";
                    tcOD.Style.Add("font-weight", "bold");
                    tcOD.Width = 60;
                    tblRow.Cells.Add(tcOD);
                    tblAtt.Rows.Add(tblRow);

                    tcCL = new TableCell();
                    tcCL.Text = "CL";
                    tcCL.Width = 60;
                    tcCL.Style.Add("font-weight", "bold");
                    tblRow.Cells.Add(tcCL);
                    tblAtt.Rows.Add(tblRow);

                    tcEL = new TableCell();
                    tcEL.Text = "EL";
                    tcEL.Style.Add("font-weight", "bold");
                    tblRow.Cells.Add(tcEL);
                    tblAtt.Rows.Add(tblRow);

                    tcSL = new TableCell();
                    tcSL.Text = "SL";
                    tcSL.Width = 60;
                    tcSL.Style.Add("font-weight", "bold");
                    tblRow.Cells.Add(tcSL);
                    tblAtt.Rows.Add(tblRow);

                    tcLeavesApp = new TableCell();
                    tcLeavesApp.Text = "LA";
                    tcLeavesApp.Width = 60;
                    tcLeavesApp.Style.Add("font-weight", "bold");
                    tblRow.Cells.Add(tcLeavesApp);
                    tblAtt.Rows.Add(tblRow);

                    tcLeavesApp = new TableCell();
                    tcLeavesApp.Text = "OCL";
                    tcLeavesApp.Width = 60;
                    tcLeavesApp.Style.Add("font-weight", "bold");
                    tblRow.Cells.Add(tcLeavesApp);
                    tblAtt.Rows.Add(tblRow);

                    tcLeavesApp = new TableCell();
                    tcLeavesApp.Text = "OEL";
                    tcLeavesApp.Width = 60;
                    tcLeavesApp.Style.Add("font-weight", "bold");
                    tblRow.Cells.Add(tcLeavesApp);
                    tblAtt.Rows.Add(tblRow);

                    tcLeavesApp = new TableCell();
                    tcLeavesApp.Text = "OSL";
                    tcLeavesApp.Width = 60;
                    tcLeavesApp.Style.Add("font-weight", "bold");
                    tblRow.Cells.Add(tcLeavesApp);
                    tblAtt.Rows.Add(tblRow);

                    tcLeavesApp = new TableCell();
                    tcLeavesApp.Text = "CCL";
                    tcLeavesApp.Width = 60;
                    tcLeavesApp.Style.Add("font-weight", "bold");
                    tblRow.Cells.Add(tcLeavesApp);
                    tblAtt.Rows.Add(tblRow);

                    tcLeavesApp = new TableCell();
                    tcLeavesApp.Text = "CEL";
                    tcLeavesApp.Width = 60;
                    tcLeavesApp.Style.Add("font-weight", "bold");
                    tblRow.Cells.Add(tcLeavesApp);
                    tblAtt.Rows.Add(tblRow);

                    tcLeavesApp = new TableCell();
                    tcLeavesApp.Text = "CSL";
                    tcLeavesApp.Width = 60;
                    tcLeavesApp.Style.Add("font-weight", "bold");
                    tblRow.Cells.Add(tcLeavesApp);
                    tblAtt.Rows.Add(tblRow);

                    tcLeavesApp = new TableCell();
                    tcLeavesApp.Text = "AL";
                    tcLeavesApp.Width = 60;
                    tcLeavesApp.Style.Add("font-weight", "bold");
                    tblRow.Cells.Add(tcLeavesApp);
                    tblAtt.Rows.Add(tblRow);

                    tcLeavesApp = new TableCell();
                    tcLeavesApp.Text = "LOP";
                    tcLeavesApp.Width = 60;
                    tcLeavesApp.Style.Add("font-weight", "bold");
                    tblRow.Cells.Add(tcLeavesApp);
                    tblAtt.Rows.Add(tblRow);
                    #endregion CellsCreation

                    //tcOBCL, tcOBEL, tcOBSL, tcCurCL, tcCurEL, tcCurSL,tcAdjLeaves,tcLOP

                    // dt = Convert.ToDateTime(ddlMonth.SelectedValue + "/21/" + ddlYear.SelectedValue); -- commented by ravi kumar
                    st = month + "/" + Convert.ToInt32(startdate.Tables[0].Rows[0][0].ToString()) + "/" + year;
                    //dt = DateTime.ParseExact(st, "MM/dd/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                    dt = CODEUtility.ConvertToDate(st, DateFormat.DayMonthYear);
                    dtEnd = dt.AddMonths(1);

                    for (int j = 0; j < count - 1; j++)
                    {
                        tblRow = new TableRow();
                        tcName = new TableCell();
                        tcEmpID = new TableCell();
                        if (ds.Tables[j].Rows.Count != 0)
                        {
                            #region Designation
                            if (ds.Tables[j].Rows[0][6].ToString().Trim().ToLower() != Department)
                            {

                                Department = ds.Tables[j].Rows[0][6].ToString().Trim().ToLower();
                                tcDesig = new TableCell();
                                if (ds.Tables[j].Rows[0][6].ToString() != null && ds.Tables[j].Rows[0][6].ToString() != string.Empty && ds.Tables[j].Rows[0][6].ToString() != string.Empty)
                                {
                                    tcDesig.Text = ds.Tables[j].Rows[0][6].ToString();
                                    tcDesig.Style.Add(HtmlTextWriterStyle.FontWeight, "Bold");
                                    tcDesig.Style.Add(HtmlTextWriterStyle.Color, "Black");
                                    tcDesig.ColumnSpan = tblAtt.Rows[0].Cells.Count;
                                    tblRow.Cells.Add(tcDesig);
                                    tblAtt.Rows.Add(tblRow);
                                }
                                tblRow = new TableRow();
                            }

                            #endregion Designation

                            #region LeavesApplied And EmpID
                            if (ds.Tables[j].Rows[0][0].ToString() != null && ds.Tables[j].Rows[0][0].ToString() != string.Empty && ds.Tables[j].Rows[0][0].ToString() != string.Empty)
                            {
                                tcEmpID.Text = ds.Tables[j].Rows[0][0].ToString();
                                #region TypesLeaves
                                int EmpID = Convert.ToInt32(ds.Tables[j].Rows[0][0].ToString());
                                //DataSet dsLeaveTotal = new DataSet();
                                int Month = Convert.ToInt32(ddlMonth.SelectedValue);
                                int Year = Convert.ToInt32(ddlYear.SelectedValue);
                                DataSet dsLeaveTotal = AttendanceDAC.EmpMonthLeaveCount(EmpID, Month, Year);
                                #region LeavesApplied
                                if (dsLeaveTotal != null && dsLeaveTotal.Tables.Count > 0 && dsLeaveTotal.Tables[0].Rows.Count > 0)
                                {
                                    int LeaveTot = 0, Count, TotalDaysInMnth;// FrmMnth, FrmYear, ToMnth, ToYear, FrmDay, ToDay;

                                    for (int i = 0; i < dsLeaveTotal.Tables[0].Rows.Count; i++)
                                    {
                                        TotalDaysInMnth = DateTime.DaysInMonth(Year, Month);

                                        if (Convert.ToInt32(dsLeaveTotal.Tables[0].Rows[i]["FrmMonth"].ToString()) == Convert.ToInt32(dsLeaveTotal.Tables[0].Rows[i]["UntillMonth"].ToString()))
                                        {
                                            Count = (Convert.ToInt32(dsLeaveTotal.Tables[0].Rows[i]["UntillDay"].ToString()) - Convert.ToInt32(dsLeaveTotal.Tables[0].Rows[i]["FrmDay"].ToString())) + 1;
                                            LeaveTot = LeaveTot + Count;
                                            LA = LeaveTot;
                                        }
                                        else if (Convert.ToInt32(dsLeaveTotal.Tables[0].Rows[i]["FrmMonth"].ToString()) != Convert.ToInt32(dsLeaveTotal.Tables[0].Rows[i]["UntillMonth"].ToString()))
                                        {
                                            if (Convert.ToInt32(dsLeaveTotal.Tables[0].Rows[i]["UntillMonth"].ToString()) == Month)
                                            {
                                                LeaveTot = Convert.ToInt32(dsLeaveTotal.Tables[0].Rows[i]["UntillDay"].ToString());
                                                LA = LeaveTot;
                                            }
                                            else
                                            {
                                                Count = (TotalDaysInMnth - Convert.ToInt32(dsLeaveTotal.Tables[0].Rows[i]["FrmDay"].ToString())) + 1;
                                                LeaveTot = LeaveTot + Count;
                                                LA = LeaveTot;
                                            }
                                        }
                                    }
                                }
                                else
                                    LA = 0;
                                #endregion LeavesApplied

                                #region OBLeaves And CurLeaves
                                if (dsLeaveTotal != null && dsLeaveTotal.Tables.Count > 0 && dsLeaveTotal.Tables[1].Rows.Count > 0)
                                {
                                    OBCL = Convert.ToDouble(dsLeaveTotal.Tables[1].Rows[0]["ObCL"].ToString());
                                    OBEL = Convert.ToDouble(dsLeaveTotal.Tables[1].Rows[0]["ObEL"].ToString());
                                    OBSL = Convert.ToDouble(dsLeaveTotal.Tables[1].Rows[0]["ObSL"].ToString());
                                    CCL = Convert.ToDouble(dsLeaveTotal.Tables[1].Rows[0]["CurCL"].ToString());
                                    CEL = Convert.ToDouble(dsLeaveTotal.Tables[1].Rows[0]["CurEL"].ToString());
                                    CSL = Convert.ToDouble(dsLeaveTotal.Tables[1].Rows[0]["CurSL"].ToString());
                                    AL = Convert.ToDouble(dsLeaveTotal.Tables[1].Rows[0]["AdjLeaves"].ToString());
                                    LOP = LA - AL;
                                    if (LOP < 0)
                                    {
                                        LOP = 0;
                                    }
                                }
                                #endregion OBLeaves And CurLeaves
                                #endregion TypesLeaves
                            }
                            #endregion LeavesApplied And EmpID

                            #region EmpIDName

                            if (ds.Tables[j].Rows[0][3].ToString() != null && ds.Tables[j].Rows[0][3].ToString() != string.Empty && ds.Tables[j].Rows[0][3].ToString() != string.Empty)
                            {
                                tcName.Text = ds.Tables[j].Rows[0][3].ToString();
                                tcName.Width = 300;
                                tblRow.Cells.Add(tcName);
                            }
                            #endregion EmpIDName



                            Present = 0; Scope = 0; Absent = 0; OD = CL = EL = SL = 0;
                            for (int i = 1; dt != dtEnd; i++)               // Dates no of days
                            {
                                tcDate = new TableCell();

                                DateTime dtCurrent = new DateTime(Convert.ToInt32(ddlYear.SelectedValue), Convert.ToInt32(ddlMonth.SelectedValue), i);
                                if (listHolidays.Contains(dtCurrent))
                                {
                                    if (ds.Tables[j].Rows[0][9].ToString() == "1")
                                    {
                                        tcDate.Text = "PH";
                                        tcDate.Style.Add("background-color", "lightgreen");
                                    }
                                    else
                                    {
                                        tcDate.Text = "-";
                                    }
                                }
                                else
                                {
                                    Scope++;
                                }

                                if (ds.Tables[j].Rows.Count > 0)
                                {


                                    for (int k = 0; k < ds.Tables[j].Rows.Count; k++)    // No of Emp
                                    {
                                        #region 1
                                        if (ds.Tables[j].Rows[k][2].ToString() != string.Empty)
                                        {
                                            if (dt == Convert.ToDateTime(ds.Tables[j].Rows[k][2].ToString()).Date)
                                            {
                                                tcDate.Text = ds.Tables[j].Rows[k][1].ToString();
                                                if (ds.Tables[j].Rows[k][1].ToString() == "P" || ds.Tables[j].Rows[k][1].ToString() == "OD")
                                                {
                                                    tcDate.Style.Add("color", "green");
                                                    //Present++;
                                                }

                                                else if (ds.Tables[j].Rows[k][1].ToString() == "WO")
                                                {
                                                    tcDate.Style.Add("color", "red");
                                                }
                                                else if (ds.Tables[j].Rows[k][1].ToString() == "HD")
                                                {
                                                    tcDate.Style.Add("color", "green");
                                                    Present = Present + 0.5;
                                                }
                                                if (ds.Tables[j].Rows[k][1].ToString() == "P")
                                                {
                                                    Present++;
                                                }
                                                if (ds.Tables[j].Rows[k][1].ToString() == "A")
                                                {
                                                    Absent++;
                                                }
                                                if (ds.Tables[j].Rows[k][1].ToString() == "OD")
                                                {
                                                    OD++;
                                                }
                                                if (ds.Tables[j].Rows[k][1].ToString() == "CL")
                                                {
                                                    CL++;
                                                }
                                                if (ds.Tables[j].Rows[k][1].ToString() == "EL")
                                                {
                                                    EL++;
                                                }
                                                if (ds.Tables[j].Rows[k][1].ToString() == "SL")
                                                {
                                                    SL++;
                                                }
                                                if (ds.Tables[j].Rows[k][1].ToString() == "LO")
                                                {
                                                    LOP++;
                                                }
                                                break;
                                            }
                                            else
                                            {
                                                tcDate.Style.Add("color", "red");
                                                if (listHolidays.Contains(dtCurrent))
                                                {
                                                    if (ds.Tables[j].Rows[0][9].ToString() == "1")
                                                    {
                                                        tcDate.Text = "PH";
                                                    }
                                                    else
                                                    {
                                                        tcDate.Text = "-";
                                                    }
                                                }
                                                else
                                                {
                                                    tcDate.Text = "-";
                                                }
                                            }
                                        }
                                        #endregion 1

                                        #region 2
                                        else
                                        {
                                            if (listHolidays.Contains(dtCurrent))
                                            {
                                                if (ds.Tables[j].Rows[0][9].ToString() == "1")
                                                {
                                                    tcDate.Text = "PH";
                                                }
                                                else
                                                {
                                                    tcDate.Text = "-";
                                                }
                                            }
                                            else
                                            {
                                                tcDate.Text = "-";
                                            }
                                        }
                                        #endregion 2
                                    }


                                }
                                tcDate.Width = 60;
                                tblRow.Cells.Add(tcDate);
                                dt = dt.AddDays(1);
                            }

                            tcScope = new TableCell();
                            tcScope.Text = Scope.ToString();
                            tcScope.HorizontalAlign = HorizontalAlign.Right;
                            tcScope.Width = 60;
                            tblRow.Cells.Add(tcScope);


                            tcDate = new TableCell();
                            tcDate.Text = Present.ToString();
                            tcDate.HorizontalAlign = HorizontalAlign.Right;
                            tcDate.Width = 60;
                            tblRow.Cells.Add(tcDate);


                            tcAbsent = new TableCell();
                            tcAbsent.Text = Absent.ToString();
                            tcAbsent.HorizontalAlign = HorizontalAlign.Right;
                            tcAbsent.Width = 60;
                            tblRow.Cells.Add(tcAbsent);

                            tcOD = new TableCell();
                            tcOD.Text = OD.ToString();
                            tcOD.HorizontalAlign = HorizontalAlign.Right;
                            tcOD.Width = 60;
                            tblRow.Cells.Add(tcOD);

                            tcCL = new TableCell();
                            tcCL.Text = CL.ToString();
                            tcCL.HorizontalAlign = HorizontalAlign.Right;
                            tcCL.Width = 60;
                            tblRow.Cells.Add(tcCL);

                            tcEL = new TableCell();
                            tcEL.Text = EL.ToString();
                            tcEL.HorizontalAlign = HorizontalAlign.Right;
                            tcEL.Width = 60;
                            tblRow.Cells.Add(tcEL);

                            tcSL = new TableCell();
                            tcSL.Text = SL.ToString();
                            tcSL.HorizontalAlign = HorizontalAlign.Right;
                            tcSL.Width = 60;
                            tblRow.Cells.Add(tcSL);

                            tcLeavesApp = new TableCell();
                            tcLeavesApp.Text = LA.ToString();
                            tcLeavesApp.HorizontalAlign = HorizontalAlign.Right;
                            tcLeavesApp.Width = 60;
                            tblRow.Cells.Add(tcLeavesApp);

                            tcOBCL = new TableCell();
                            tcOBCL.Text = OBCL.ToString();
                            tcOBCL.HorizontalAlign = HorizontalAlign.Right;
                            tcOBCL.Width = 60;
                            tblRow.Cells.Add(tcOBCL);

                            tcOBEL = new TableCell();
                            tcOBEL.Text = OBEL.ToString();
                            tcOBEL.HorizontalAlign = HorizontalAlign.Right;
                            tcOBEL.Width = 60;
                            tblRow.Cells.Add(tcOBEL);

                            tcOBSL = new TableCell();
                            tcOBSL.Text = OBSL.ToString();
                            tcOBSL.HorizontalAlign = HorizontalAlign.Right;
                            tcOBSL.Width = 60;
                            tblRow.Cells.Add(tcOBSL);

                            tcCurCL = new TableCell();
                            tcCurCL.Text = CCL.ToString();
                            tcCurCL.HorizontalAlign = HorizontalAlign.Right;
                            tcCurCL.Width = 60;
                            tblRow.Cells.Add(tcCurCL);

                            tcCurEL = new TableCell();
                            tcCurEL.Text = CEL.ToString();
                            tcCurEL.HorizontalAlign = HorizontalAlign.Right;
                            tcCurEL.Width = 60;
                            tblRow.Cells.Add(tcCurEL);

                            tcCurSL = new TableCell();
                            tcCurSL.Text = CSL.ToString();
                            tcCurSL.HorizontalAlign = HorizontalAlign.Right;
                            tcCurSL.Width = 60;
                            tblRow.Cells.Add(tcCurSL);

                            tcAdjLeaves = new TableCell();
                            tcAdjLeaves.Text = AL.ToString();
                            tcAdjLeaves.HorizontalAlign = HorizontalAlign.Right;
                            tcAdjLeaves.Width = 60;
                            tblRow.Cells.Add(tcAdjLeaves);

                            tcLOP = new TableCell();
                            tcLOP.Text = LOP.ToString();
                            tcLOP.HorizontalAlign = HorizontalAlign.Right;
                            tcLOP.Width = 60;
                            tblRow.Cells.Add(tcLOP);

                            TableCell tc1 = new TableCell();

                            if (DateTime.Now.Month.ToString() != ddlMonth.SelectedValue)
                            {

                                tcLnkbtn = new TableCell();
                                tcLnkbtn.Text = "PaySlip";
                                tcLnkbtn.Style.Add("font-weight", "bold");
                                tcLnkbtn.Width = 200;
                                tblRow.Cells.Add(tcLnkbtn);

                                HtmlAnchor lnkbtn = new HtmlAnchor();
                                lnkbtn.InnerText = "PaySlip";
                                lnkbtn.Target = "_blank";
                                lnkbtn.HRef = ("~/HMS/PaySlipPreview.aspx?id=" + tcEmpID.Text + "&Date=" + (startdate.Tables[0].Rows[0][0].ToString() + "/" + ddlMonth.SelectedValue + "/" + ddlYear.SelectedValue));
                                tcLnkbtn.Controls.Add(lnkbtn);
                            }
                            dtEnd = dt.AddMonths(1);
                            tblAtt.Rows.Add(tblRow);
                        }
                    }
                }
            }
        }

        //void BindDayStrength()
        //{
             
            //try
            //{
            //    int Month = Convert.ToInt32(ddlMonth.SelectedValue == string.Empty ? "0" : ddlMonth.SelectedValue);                
            //    int Year = Convert.ToInt32(ddlYear.SelectedValue == string.Empty ? "0" : ddlYear.SelectedValue);
            //   hdn.Value = "day";
            //    DateTime Date = CodeUtilHMS.ConvertToDate_ddMMMyyy(txtDay.Text.Trim(), CodeUtilHMS.DateFormat.ddMMMyyyy);
            //      String Day = Date.Day.ToString();
            //      int Day1 = Convert.ToInt32(Day);
            //    int WorkSiteID = Convert.ToInt32(ddlWorksite_hid.Value == string.Empty ? "0" : ddlWorksite_hid.Value);
            //    SqlParameter[] sqlParams = new SqlParameter[5];
            //    sqlParams[0] = new SqlParameter("@Date", Date);
            //    sqlParams[1] = new SqlParameter("@Day", Day1);
            //    sqlParams[2] = new SqlParameter("@Month", Month);
            //    sqlParams[3] = new SqlParameter("@WSID", WorkSiteID);
            //    sqlParams[4] = new SqlParameter("@Year", Year);
            //    ds = SqlHelper.ExecuteDataset("Sp_BindDayStrength", sqlParams);
            //    SQLDBUtil.ExecuteNonQuery("Sp_BindDayStrength", sqlParams);
            //    grdAttStatusCount.DataSource = ds;
            //    grdAttStatusCount.DataBind();
            //    grdAttStatusCount.Visible = true;
            //}
            //catch
            //{

            //}
        //}

        protected void btnDayStrength_Click(object sender, EventArgs e)
        {
            //GetMonthlyReport();
            GetAttandanceandDisplay();
        }
     }
 }
