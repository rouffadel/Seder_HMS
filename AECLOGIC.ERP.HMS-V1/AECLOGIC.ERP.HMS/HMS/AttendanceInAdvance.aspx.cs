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
namespace AECLOGIC.ERP.HMS
{
    public partial class AttendanceInAdvance : AECLOGIC.ERP.COMMON.WebFormMaster
    {
        static int? Roleid;
        TableRow tblRow;
        HRCommon objHrCommon = new HRCommon();
        HRCommon objHrCommon1 = new HRCommon();
        GridViewRow gvr;
        static int scmonth = 0, scyear = 0;
        static int wsid; int stmonth = 0; int edmonth = 0; int days = 0;
        protected override void OnInit(EventArgs e)
        {
            ModuleID = 1;
            base.OnInit(e);
            EmployeeChangesPaging.FirstClick += new Paging.PageFirst(EmployeeChangesPaging_FirstClick);
            EmployeeChangesPaging.PreviousClick += new Paging.PagePrevious(EmployeeChangesPaging_FirstClick);
            EmployeeChangesPaging.NextClick += new Paging.PageNext(EmployeeChangesPaging_FirstClick);
            EmployeeChangesPaging.LastClick += new Paging.PageLast(EmployeeChangesPaging_FirstClick);
            EmployeeChangesPaging.ChangeClick += new Paging.PageChange(EmployeeChangesPaging_FirstClick);
            EmployeeChangesPaging.ShowRowsClick += new Paging.ShowRowsChange(EmployeeChangesPaging_ShowRowsClick);
            EmployeeChangesPaging.CurrentPage = 1;
            Paging1.FirstClick += new Paging.PageFirst(EmployeeChangesPaging_FirstClick);
            Paging1.NextClick += new Paging.PageNext(EmployeeChangesPaging_FirstClick);
            Paging1.PreviousClick += new Paging.PagePrevious(EmployeeChangesPaging_FirstClick);
            Paging1.ChangeClick += new Paging.PageChange(EmployeeChangesPaging_FirstClick);
            Paging1.LastClick += new Paging.PageLast(EmployeeChangesPaging_FirstClick);
            Paging1.ShowRowsClick += new Paging.ShowRowsChange(EmployeeChangesPaging_ShowRowsClick);
            Paging1.CurrentPage = 1;
            EmployeeChangesPaging.FirstClick += new Paging.PageFirst(EmployeeChangesPaging_FirstClick1);
            EmployeeChangesPaging.PreviousClick += new Paging.PagePrevious(EmployeeChangesPaging_FirstClick1);
            EmployeeChangesPaging.NextClick += new Paging.PageNext(EmployeeChangesPaging_FirstClick1);
            EmployeeChangesPaging.LastClick += new Paging.PageLast(EmployeeChangesPaging_FirstClick1);
            EmployeeChangesPaging.ChangeClick += new Paging.PageChange(EmployeeChangesPaging_FirstClick1);
            EmployeeChangesPaging.ShowRowsClick += new Paging.ShowRowsChange(EmployeeChangesPaging_ShowRowsClick1);
            EmployeeChangesPaging.CurrentPage = 1;
            Pagingmsg.FirstClick += new Paging.PageFirst(MissingEmployees_FirstClick);
            Pagingmsg.PreviousClick += new Paging.PagePrevious(MissingEmployees_FirstClick);
            Pagingmsg.NextClick += new Paging.PageNext(MissingEmployees_FirstClick);
            Pagingmsg.LastClick += new Paging.PageLast(MissingEmployees_FirstClick);
            Pagingmsg.ChangeClick += new Paging.PageChange(MissingEmployees_FirstClick);
            Pagingmsg.ShowRowsClick += new Paging.ShowRowsChange(MissingEmployees_ShowRowsClick);
            Pagingmsg.CurrentPage = 1;
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
        void EmployeeChangesPaging_FirstClick1(object sender, EventArgs e)
        {
            objHrCommon.CurrentPage = EmployeeChangesPaging.CurrentPage;
            objHrCommon.PageSize = EmployeeChangesPaging.ShowRows;
            BIndGrid(gvr);
        }
        void EmployeeChangesPaging_ShowRowsClick1(object sender, EventArgs e)
        {
            EmployeeChangesPaging.CurrentPage = 1;
            BIndGrid(gvr);
        }
        public void BindPagermsg()
        {
            objHrCommon1.CurrentPage = Pagingmsg.CurrentPage;
            objHrCommon1.PageSize = Pagingmsg.ShowRows;
            msgemployee_Click(objHrCommon1);
        }
        protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(ddlmonth.SelectedValue) > 0 && Convert.ToInt32(ddlyear.SelectedValue) > 0)
                CheckLastWorkingDay();
        }
        protected void ddlMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(ddlmonth.SelectedValue) > 0 && Convert.ToInt32(ddlyear.SelectedValue) > 0)
                CheckLastWorkingDay();
        }
        void EmployeeChangesPaging_ShowRowsClick(object sender, EventArgs e)
        {
            EmployeeChangesPaging.CurrentPage = 1;
            Paging1.CurrentPage = 1;
            BindPager();
        }
        void EmployeeChangesPaging_FirstClick(object sender, EventArgs e)
        {
            BindPager();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            Roleid = Convert.ToInt32(Session["RoleId"].ToString());
            try
            {
                Ajax.Utility.RegisterTypeForAjax(typeof(AjaxDAL));
                if (!IsPostBack)
                {
                    dvsearch.Visible = false;
                    DataSet startdate = SQLDBUtil.ExecuteDataset("G_StartdateForAdvAttendance");
                    ViewState["startdate"] = 1;
                    ViewState["enddate"] = 1;
                    DataSet enddate = SQLDBUtil.ExecuteDataset("G_EnddateForAdvAttendance");
                    ViewState["startdate"] = startdate.Tables[0].Rows[0][0].ToString();
                    ViewState["enddate"] = enddate.Tables[0].Rows[0][0].ToString();
                    FillAttandanceTypeNew();
                    BindAtttypes();
                    bindyear();
                    if (Convert.ToInt32(ddlmonth.SelectedValue) > 0 && Convert.ToInt32(ddlyear.SelectedValue) > 0)
                    {
                        CheckLastWorkingDay();
                        scmonth = Convert.ToInt32(ddlmonth.SelectedValue);
                        scyear = Convert.ToInt32(ddlyear.SelectedValue);
                    }
                    BindPager();
                    EmployeeChangesPaging.Visible = false;
                }
            }
            catch
            {
            }
        }
        private void CheckLastWorkingDay()
        {
            SqlParameter[] sqlParams = new SqlParameter[2];
            sqlParams[0] = new SqlParameter("@Year", Convert.ToInt32(ddlyear.SelectedValue));
            sqlParams[1] = new SqlParameter("@Month", Convert.ToInt32(ddlmonth.SelectedValue));
            DataSet ds = SQLDBUtil.ExecuteDataset("sh_EmployeeLastWorkingDayWorkSiteStatus", sqlParams);
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0 && ds.Tables[0].Rows[0][0].ToString() == "Already Synchronised Successfully")
            {
                btnLastWorkingDay.Text = "Reset Cost Center";
            }
            else
            {
                btnLastWorkingDay.Text = "Set Cost Center";
            }
        }
        private void BindAtttypes()
        {
            DataSet ds = AttendanceDAC.GetAttendanceType();
            ViewState["AttTypes"] = ds;
        }
        public DataSet FillAttandanceType()
        {
            return (DataSet)ViewState["AttTypes"];
        }
        private void FillAttandanceTypeNew()
        {
            DataSet ds = AttendanceDAC.GetAttendanceType();
            ddlAttType.DataSource = ds;
            ddlAttType.DataValueField = "ID";
            ddlAttType.DataTextField = "ShortName";
            ddlAttType.DataBind();
            ddlAttType.Items.Insert(0, new ListItem("--AttType--", "0"));
        }
        private void BIndGrid(GridViewRow gvr)
        {
            int siteid = 0; int Month = 0; int Year = 0, Empid = 0;
            if (TxtEmp.Text != "" && TxtEmp.Text != string.Empty && ddlEmp_hid.Value != "" && ddlEmp_hid.Value != string.Empty)
            {
                Empid = Convert.ToInt32(ddlEmp_hid.Value);
                SqlParameter[] par = new SqlParameter[1];
                par[0] = new SqlParameter("@EmpID", Empid);
                SqlDataReader Dr;
                Dr = SqlHelper.ExecuteReader("HR_WorksiteByEmpID_AdvAttendance", par);
                if (Dr.HasRows)
                {
                    while (Dr.Read())
                        siteid = Convert.ToInt32(Dr["Categary"]);
                }
            }
            else
            {
                ddlEmp_hid.Value = "";
            }
            if (ddlEmp_hid.Value == "")
            {
                if (ddlmonth.SelectedIndex != 0)
                {
                }
                else
                {
                    AlertMsg.MsgBox(Page, "Select Month");
                    return;
                }
                if (Convert.ToInt32(ddlyear.SelectedValue) > 0)
                {
                }
                else
                {
                    AlertMsg.MsgBox(Page, "Select Year");
                    return;
                }
            }
            try
            {
                if (gvr != null)
                {
                    Label lblwsid = (Label)gvr.FindControl("lblwsid");
                    DropDownList ddlmonthgrid = (DropDownList)gvr.FindControl("ddlmonthgrid");
                    DropDownList ddlyeargrid = (DropDownList)gvr.FindControl("ddlyeargrid");
                    wsid = Convert.ToInt32(lblwsid.Text);
                    ViewState["AttYear"]=ddlyeargrid.SelectedValue;
                    ViewState["AttMonth"] = ddlmonthgrid.SelectedValue;
                }
                objHrCommon.PageSize = EmployeeChangesPaging.ShowRows;
                objHrCommon.CurrentPage = EmployeeChangesPaging.CurrentPage;
                SqlParameter[] sqlParams = new SqlParameter[8];
                sqlParams[0] = new SqlParameter("@CurrentPage", objHrCommon.CurrentPage);
                sqlParams[1] = new SqlParameter("@PageSize", objHrCommon.PageSize);
                sqlParams[2] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
                sqlParams[2].Direction = ParameterDirection.ReturnValue;
                sqlParams[3] = new SqlParameter("@NoofRecords", System.Data.SqlDbType.Int);
                sqlParams[3].Direction = ParameterDirection.Output;
                sqlParams[4] = new SqlParameter("@WSID", Convert.ToInt32(wsid));
                sqlParams[5] = new SqlParameter("@Year", Convert.ToInt32(ViewState["AttYear"]));
                sqlParams[6] = new SqlParameter("@Month", Convert.ToInt32(ViewState["AttMonth"]));
                sqlParams[7] = new SqlParameter("@Empid", Empid);
                DataSet ds = SQLDBUtil.ExecuteDataset("sp_PivotedResultSet_AttendanceAdvance", sqlParams);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ViewState["ds"] = ds.Tables[0];
                    gvAdvAttendance.DataSource = ds.Tables[0];
                    gvAdvAttendance.DataBind();
                    dvsearch.Visible = true;
                    int dtdayscount = ds.Tables[0].Columns.Count - 3;
                    if (dtdayscount == 8)
                    {
                        gvAdvAttendance.Columns[11].Visible = false;
                        gvAdvAttendance.Columns[12].Visible = false;
                        gvAdvAttendance.Columns[13].Visible = false;
                    }
                    else if (dtdayscount == 9)
                    {
                        gvAdvAttendance.Columns[12].Visible = false;
                        gvAdvAttendance.Columns[13].Visible = false;
                    }
                    else if (dtdayscount == 10)
                    {
                        gvAdvAttendance.Columns[13].Visible = false;
                    }
                    else
                    {
                        gvAdvAttendance.Columns[11].Visible = true;
                        gvAdvAttendance.Columns[12].Visible = true;
                        gvAdvAttendance.Columns[13].Visible = true;
                    }
                    objHrCommon.NoofRecords = (int)sqlParams[3].Value;
                    objHrCommon.TotalPages = (int)sqlParams[2].Value;
                    EmployeeChangesPaging.Bind(objHrCommon.CurrentPage, objHrCommon.TotalPages, objHrCommon.NoofRecords, objHrCommon.PageSize);
                    EmployeeChangesPaging.Visible = true;
                }
                else
                {
                    dvsearch.Visible = false;
                    gvAdvAttendance.DataSource = null;
                    gvAdvAttendance.DataBind();
                    AlertMsg.MsgBox(Page, "Click PULL From Attandance to populate data",AlertMsg.MessageType.Warning);
                }
            }
            catch { }
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionList_Employee(string prefixText, int count, string contextKey)
        {
            DataSet ds = AttendanceDAC.HMS_Service_DLL_Employee_By_WS_Dept_googlesearch_AttAdv(prefixText.Trim(), wsid, 0,scmonth,scyear);//WSID
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
        public static string[] ConvertStingArray(DataSet ds)
        {
            string[] rtval = Array.ConvertAll(ds.Tables[0].Select(), new Converter<DataRow, string>(DataRowTotable));
            return rtval;
        }
        public static string DataRowTotable(DataRow dr)
        {
            return dr["Name"].ToString();
        }
        protected void btnsearch_Click(object sender, EventArgs e)
        {
            lblmsg.Visible = false;
            gvmsngemp.Visible = false;
            Pagingmsg.Visible = false;
            gvmsngemp.DataSource = null;
            gvmsngemp.DataBind();
            gvAdvAttendance.DataSource = null;
            gvAdvAttendance.DataBind();
            EmployeeChangesPaging.Visible = false;
            SqlParameter[] SPP = new SqlParameter[2];
            SPP[0] = new SqlParameter("@Year", Convert.ToInt32(ddlyear.SelectedValue));
            SPP[1] = new SqlParameter("@Month", Convert.ToInt32(ddlmonth.SelectedValue));
            DataSet ds = SQLDBUtil.ExecuteDataset("sh_EmployeeLastWorkingDayWorkSiteStatus", SPP);
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0 && ds.Tables[0].Rows[0][0].ToString() == "Already Synchronised Successfully")
            {
                btnLastWorkingDay.Text = "Reset Cost Center";
                Paging1.CurrentPage = 1;
                BindPager();
            }
            else
            {
                btnLastWorkingDay.Text = "Set Cost Center";
                gvunsync.DataSource = null;
                gvunsync.DataBind();
                Paging1.Visible = false;
                AlertMsg.MsgBox(Page, ds.Tables[0].Rows[0][0].ToString(),AlertMsg.MessageType.Warning);
            }
        }
        protected void gvAdvAttendance_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "UPD")
            {
                int totcols, visiblecolcount = 0;
                totcols = gvAdvAttendance.Columns.Count;
                for (int i = 0; i < totcols; i++)
                {
                    if (gvAdvAttendance.Columns[i].Visible == true)
                    {
                        visiblecolcount++;
                    }
                }
                int EmpID = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                string ddlAttendance = "ddlAttendance"; int y = 21; int Month = 0; int Year = 0;
                for (int i = 1; i <= visiblecolcount - 2; i++)
                {
                    string Date1 = y + "/" + ddlmonth.SelectedItem.Value + "/" + ddlyear.SelectedItem.Value;
                    DateTime Date2 = CodeUtilHMS.ConvertToDate(Date1, CodeUtilHMS.DateFormat.DayMonthYear);
                    DropDownList ddl = (DropDownList)row.FindControl(ddlAttendance + y);
                    SqlParameter[] sqlParams = new SqlParameter[5];
                    sqlParams[0] = new SqlParameter("@Empid", EmpID);
                    sqlParams[1] = new SqlParameter("@Status", ddl.SelectedValue);
                    sqlParams[2] = new SqlParameter("@date", Date2);
                    sqlParams[3] = new SqlParameter("@Siteid", wsid);
                    sqlParams[4] = new SqlParameter("@MarkedBy", AECLOGIC.ERP.COMMON.clSession.cmnUserId);
                    SQLDBUtil.ExecuteNonQuery("HMS_InsAdvAttendacne", sqlParams);
                    y = y + 1;
                }
                AlertMsg.MsgBox(Page, "Saved !");
            }
            if (e.CommandName == "att")
            {
                int Empid = Convert.ToInt32(e.CommandArgument);
                int month = Convert.ToInt32(ddlmonth.SelectedValue);
                int year = Convert.ToInt32(ddlyear.SelectedValue);
                CalenderMonth(Empid, year, month);
            }
        }
        protected void CalenderMonth(int EMPID, int year, int Month)
        {
            try
            {
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
                int PageSize = EmployeeChangesPaging.ShowRows;
                int CurrentPage = EmployeeChangesPaging.CurrentPage;
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
        protected void gvAdvAttendance_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            int colno = 0;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int y = 21; string ddlAttendance = "ddlAttendance";
                DataTable dt = (DataTable)ViewState["ds"];
                var colCount = dt.Columns.Count;
                for (int i = 1; i <= colCount - 3; i++)
                {
                    DropDownList ddl = (DropDownList)e.Row.FindControl(ddlAttendance + y);
                    ddl.SelectedValue = Convert.ToInt32((e.Row.DataItem as DataRowView)[y.ToString()]).ToString();
                    y = y + 1;
                }
            }
        }
        protected void lnkunsync_Click(object sender, EventArgs e)
        {
            SqlParameter[] SP = new SqlParameter[4];
            SP[0] = new SqlParameter("@Year", Convert.ToInt32(ddlyear.SelectedValue));
            SP[1] = new SqlParameter("@Month", Convert.ToInt32(ddlmonth.SelectedValue));
            SP[2] = new SqlParameter("@fcase", 3);
            SP[3] = new SqlParameter("@Process", "Attendance In Advance");
            DataSet ds = SQLDBUtil.ExecuteDataset("sh_Paydayslastsynctime", SP);
            if (ds.Tables[0].Rows.Count > 0)
            {
                gvunsync.DataSource = ds.Tables[0];
                tblunsync.Visible = true;
            }
            else
                gvunsync.DataSource = null;
            gvunsync.DataBind();
        }
        protected void btnreset_Click(object sender, EventArgs e)
        {
            TxtEmp.Text = String.Empty;
            gvAdvAttendance.DataSource = null;
            gvAdvAttendance.DataBind();
            bindyear();
            gvmsngemp.DataSource = null;
            gvmsngemp.DataBind();
            gvmsngemp.Visible = false;
        }
        protected void lnkunsync_Click(HRCommon objHrCommon)
        {
            SqlParameter[] SP = new SqlParameter[8];
            SP[0] = new SqlParameter("@Year", Convert.ToInt32(ddlyear.SelectedValue));
            SP[1] = new SqlParameter("@Month", Convert.ToInt32(ddlmonth.SelectedValue));
            SP[2] = new SqlParameter("@fcase", 3);
            SP[3] = new SqlParameter("@Process", "Attendance In Advance");
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
        public void BindPager()
        {
            objHrCommon.CurrentPage = Paging1.CurrentPage;
            objHrCommon.PageSize = Paging1.ShowRows;
            lnkunsync_Click(objHrCommon);
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
        int siteid = 0;
        protected void gvunsync_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            siteid = Convert.ToInt32(e.CommandArgument);
            if (e.CommandName == "com")
            {
                try
                {
                    int Empid = 0;
                    GridViewRow gvr = (GridViewRow)(((Control)e.CommandSource).NamingContainer);
                    DropDownList ddlmonthgrid = (DropDownList)gvr.FindControl("ddlmonthgrid");
                    DropDownList ddlyeargrid = (DropDownList)gvr.FindControl("ddlyeargrid");
                    ddlmonth.SelectedValue = ddlmonthgrid.SelectedValue;
                    ddlyear.SelectedValue = ddlyear.SelectedValue;
                    SqlParameter[] sqlParams = new SqlParameter[4];
                    sqlParams[0] = new SqlParameter("@WSID", siteid);
                    sqlParams[1] = new SqlParameter("@Month", ddlmonthgrid.SelectedItem.Value);
                    sqlParams[2] = new SqlParameter("@Year", ddlyeargrid.SelectedItem.Value);
                    sqlParams[3] = new SqlParameter("@Empid", Empid);
                    DataSet dsatt = SQLDBUtil.ExecuteDataset("HMS_AdvAttendance", sqlParams);
                    if (dsatt.Tables.Count > 0)
                    {
                        if (dsatt.Tables[0].Rows[0][0].ToString() != "" || dsatt.Tables[1].Rows[0][0].ToString() != "")
                        {
                            //clsErrorLog.HMSEventLog(ex, "AttendanceinAdvance.aspx", "btnPull_Click", "006");
                            string path = System.Web.HttpContext.Current.Server.MapPath(".\\Logs\\") + "HMSEventLog.txt";
                            AlertMsg.MsgBox(Page, "Error Log generated. Kindly forward error log file to AEC Technical Team. The file can be found at " + path
                                + dsatt.Tables[0].Rows[0][0].ToString() + dsatt.Tables[1].Rows[0][0].ToString());
                        }
                        else
                        {
                            SqlParameter[] SP = new SqlParameter[6];
                            SP[0] = new SqlParameter("@Userid", Convert.ToInt32(AECLOGIC.ERP.COMMON.clSession.cmnUserId));
                            SP[1] = new SqlParameter("@wsid", siteid);
                            SP[2] = new SqlParameter("@fcase", 1);
                            SP[3] = new SqlParameter("@Year", ddlyeargrid.SelectedValue);
                            SP[4] = new SqlParameter("@Process", "Attendance In Advance");
                            SP[5] = new SqlParameter("@Month", ddlmonthgrid.SelectedValue);
                            int i = SQLDBUtil.ExecuteNonQuery("sh_Paydayslastsynctime", SP);
                            BindPager();
                            AlertMsg.MsgBox(Page, "PULL From Attendance Done !");
                        }
                    }
                }
                catch (Exception)
                { }
            }
            if (e.CommandName == "dis")
            {
                EmployeeChangesPaging.CurrentPage = 1;
                TxtEmp.Text = String.Empty;
                foreach (GridViewRow row in gvunsync.Rows)
                {
                    LinkButton lnkdisplay = (LinkButton)row.FindControl("lnkdisplay");
                    lnkdisplay.CssClass = "btn btn-primary";
                }
                gvr = (GridViewRow)(((Control)e.CommandSource).NamingContainer);
                LinkButton lnkdisplaygrid = (LinkButton)gvr.FindControl("lnkdisplay");
                lnkdisplaygrid.CssClass = "btn btn-warning";
                BIndGrid(gvr);
                //ViewState["gvr"] = gvr;
            }
            if (e.CommandName == "MissNo")
            {
                Pagingmsg.CurrentPage = 1;
                objHrCommon.CurrentPage = Pagingmsg.CurrentPage;
                objHrCommon.PageSize = Pagingmsg.ShowRows;
                ViewState["WSID"] = siteid;
                BindingMissingEmployees();
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
            try
            {
                int Empid = 0;
                EmployeeChangesPaging.CurrentPage = 1;
                if (TxtEmp.Text != "" && TxtEmp.Text != string.Empty && ddlEmp_hid.Value != "" && ddlEmp_hid.Value != string.Empty)
                {
                    Empid = Convert.ToInt32(ddlEmp_hid.Value);
                }
                else
                {
                    AlertMsg.MsgBox(Page, "Please Enter Employee Name",AlertMsg.MessageType.Warning);
                }
                objHrCommon.PageSize = EmployeeChangesPaging.ShowRows;
                objHrCommon.CurrentPage = EmployeeChangesPaging.CurrentPage;
                SqlParameter[] sqlParams = new SqlParameter[8];
                sqlParams[0] = new SqlParameter("@CurrentPage", objHrCommon.CurrentPage);
                sqlParams[1] = new SqlParameter("@PageSize", objHrCommon.PageSize);
                sqlParams[2] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
                sqlParams[2].Direction = ParameterDirection.ReturnValue;
                sqlParams[3] = new SqlParameter("@NoofRecords", System.Data.SqlDbType.Int);
                sqlParams[3].Direction = ParameterDirection.Output;
                sqlParams[4] = new SqlParameter("@WSID", Convert.ToInt32(0));
                sqlParams[5] = new SqlParameter("@Year", Convert.ToInt32(ddlyear.SelectedValue));
                sqlParams[6] = new SqlParameter("@Month", Convert.ToInt32(ddlmonth.SelectedValue));
                sqlParams[7] = new SqlParameter("@Empid", Empid);
                DataSet ds = SQLDBUtil.ExecuteDataset("sp_PivotedResultSet_AttendanceAdvance", sqlParams);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ViewState["ds"] = ds.Tables[0];
                    gvAdvAttendance.DataSource = ds.Tables[0];
                    gvAdvAttendance.DataBind();
                    int dtdayscount = ds.Tables[0].Columns.Count - 3;
                    if (dtdayscount == 8)
                    {
                        gvAdvAttendance.Columns[11].Visible = false;
                        gvAdvAttendance.Columns[12].Visible = false;
                        gvAdvAttendance.Columns[13].Visible = false;
                    }
                    else if (dtdayscount == 9)
                    {
                        gvAdvAttendance.Columns[12].Visible = false;
                        gvAdvAttendance.Columns[13].Visible = false;
                    }
                    else if (dtdayscount == 10)
                    {
                        gvAdvAttendance.Columns[13].Visible = false;
                    }
                    else
                    {
                        gvAdvAttendance.Columns[11].Visible = true;
                        gvAdvAttendance.Columns[12].Visible = true;
                        gvAdvAttendance.Columns[13].Visible = true;
                    }
                    objHrCommon.NoofRecords = (int)sqlParams[3].Value;
                    objHrCommon.TotalPages = (int)sqlParams[2].Value;
                    EmployeeChangesPaging.Bind(objHrCommon.CurrentPage, objHrCommon.TotalPages, objHrCommon.NoofRecords, objHrCommon.PageSize);
                    EmployeeChangesPaging.Visible = true;
                }
                else
                {
                    gvAdvAttendance.DataSource = null;
                    gvAdvAttendance.DataBind();
                    AlertMsg.MsgBox(Page, "Click PULL From Attandance to populate data",AlertMsg.MessageType.Warning);
                }
            }
            catch { }
        }
        protected void btnLastWorkingDay_Click(object sender, EventArgs e)
        {
            gvmsngemp.DataSource = null;
            gvmsngemp.DataBind();
            gvmsngemp.Visible = false;
            if (ddlmonth.SelectedIndex != 0)
            {
            }
            else
            {
                AlertMsg.MsgBox(Page, "Select Month");
                return;
            }
            if (Convert.ToInt32(ddlyear.SelectedValue) != 0)
            {
            }
            else
            {
                AlertMsg.MsgBox(Page, "Select Year");
                return;
            }
            SqlParameter[] SP = new SqlParameter[3];
            SP[0] = new SqlParameter("@Year", Convert.ToInt32(ddlyear.SelectedValue));
            SP[1] = new SqlParameter("@Month", Convert.ToInt32(ddlmonth.SelectedValue));
            SP[2] = new SqlParameter("@CreatedBy", Convert.ToInt32(AECLOGIC.ERP.COMMON.clSession.cmnUserId.ToString()));
            DataSet ds = SQLDBUtil.ExecuteDataset("sh_EmployeeLastWorkingDayWorkSite", SP);
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                AlertMsg.MsgBox(Page, ds.Tables[0].Rows[0][0].ToString());
            }
            else
            {
                AlertMsg.MsgBox(Page, "Saved Successfully");
            }
        }
        protected void btnmisngemp_Click(object sender, EventArgs e)
        {
            BindPagermsg();
        }
        protected void msgemployee_Click(HRCommon objHrCommon1)
        {
            if (ddlmonth.SelectedIndex != 0)
            {
            }
            else
            {
                AlertMsg.MsgBox(Page, "Select Month",AlertMsg.MessageType.Warning);
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
            SqlParameter[] SP = new SqlParameter[6];
            SP[0] = new SqlParameter("@Year", Convert.ToInt32(ddlyear.SelectedValue));
            SP[1] = new SqlParameter("@Month", Convert.ToInt32(ddlmonth.SelectedValue));
            SP[2] = new SqlParameter("@CurrentPage", objHrCommon1.CurrentPage);
            SP[3] = new SqlParameter("@PageSize", objHrCommon1.PageSize);
            SP[4] = new SqlParameter("@NoOfRecords", System.Data.SqlDbType.Int);
            SP[4].Direction = ParameterDirection.Output;
            SP[5] = new SqlParameter("returnvalue", System.Data.SqlDbType.Int);
            SP[5].Direction = ParameterDirection.ReturnValue;
            DataSet ds = SQLDBUtil.ExecuteDataset("HMS_Get_employeenotpresent", SP);
            if (ds.Tables[0].Rows.Count > 0)
            {
                gvmsngemp.DataSource = ds.Tables[0];
                gvmsngemp.Visible = true;
                lblmsg.Visible = true;
            }
            else
                gvmsngemp.DataSource = null;
            gvmsngemp.DataBind();
            int totpage = Convert.ToInt32(SP[3].Value);
            int noofrec = Convert.ToInt32(SP[4].Value);
            objHrCommon1.TotalPages = totpage;
            objHrCommon1.NoofRecords = noofrec;
            Pagingmsg.Visible = true;
            Pagingmsg.Bind(objHrCommon1.CurrentPage, objHrCommon1.TotalPages, objHrCommon1.NoofRecords, objHrCommon1.PageSize);
        }
        protected void btnApplySelected_Click(object sender, EventArgs e)
        {
            try
            {
                int i = 0;
                if (ddlAttType.SelectedValue != "0")
                {
                    foreach (GridViewRow gvRow in gvAdvAttendance.Rows)
                    {
                        CheckBox chk = new CheckBox();
                        chk = (CheckBox)gvRow.FindControl("chkSelect");
                        if (chk.Checked)
                        {
                            Label lblEmpid = (Label)gvRow.FindControl("lblemp");
                            int id = 0;
                            SqlParameter[] parm = new SqlParameter[4];
                            parm[0] = new SqlParameter("@Month", Convert.ToInt32(ddlmonth.SelectedValue));
                            parm[1] = new SqlParameter("@Year", Convert.ToInt32(ddlyear.SelectedValue));
                            parm[2] = new SqlParameter("@Empid", Convert.ToInt32(lblEmpid.Text));
                            parm[3] = new SqlParameter("@status", Convert.ToInt32(ddlAttType.SelectedValue));
                            i = SQLDBUtil.ExecuteNonQuery("sh_attendanceinadvanceupdate", parm);
                        }
                    }
                    if (i != 0)
                    {
                        TxtEmp.Text = String.Empty;
                        BIndGrid(null);
                        AlertMsg.MsgBox(Page, "Saved ", AlertMsg.MessageType.Success);
                    }
                }
                else
                    AlertMsg.MsgBox(Page, "Select LeaveType ", AlertMsg.MessageType.Warning);
            }
            catch (Exception ex)
            {
                AlertMsg.MsgBox(Page, ex.Message, AlertMsg.MessageType.Warning);
            }
        }
    }
}
