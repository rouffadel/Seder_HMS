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
using Aeclogic.Common.DAL;
namespace AECLOGIC.ERP.HMS
{
    public partial class AttendancePrintBetweenDates : Page
    {
        int WSID, DeptID, EmpID, Month, Year, EmpIDQryStrng;
            DateTime FromDate,ToDate;
        string Date, EmpName;

        TableRow tblRow;
        TableCell tcName, tcDesig, tcDate, tcStatus, tcShift, tcEmpID, tcLnkbtn, tcScope, tcAbsent, tcOD, tcCL, tcEL, tcSL, tcLeavesApp, tcUnStatus, tcDDate;
        AttendanceDAC objAtt = new AttendanceDAC();
       
        protected void Page_Load(object sender, EventArgs e)
        {


            try
            {
                //if (Convert.ToInt32(Request.QueryString["id"].ToString()) == 1) // 1 means Dayreport
                //{
                //    WSID = Convert.ToInt32(Request.QueryString["WSID"].ToString());
                //    DeptID = Convert.ToInt32(Request.QueryString["DeptID"].ToString());
                //    Date = Request.QueryString["Date"].ToString();
                //    EmpName = Request.QueryString["EmpName"].ToString();
                //    EmpIDQryStrng = Convert.ToInt32(Request.QueryString["Empid"].ToString());
                //    lblDate.Text = Date;
                //    GetDayReport();
                //}
                //else if (Convert.ToInt32(Request.QueryString["id"].ToString()) == 2) // 2 means Monthreport
                //{
                FromDate = Convert.ToDateTime(Request.QueryString["FromDate"].ToString());
                ToDate = Convert.ToDateTime(Request.QueryString["ToDate"].ToString());
                    EmpIDQryStrng = Convert.ToInt32(Request.QueryString["Empid"].ToString());
                    lblDate.Text = Month.ToString() + "-" + Year.ToString();
                    GetBetweenDatesReport();
                //}
                DataSet dsws = AttendanceDAC.GetWorkSite(WSID, '1', Convert.ToInt32(Session["CompanyID"]));
                lblWS.Text = dsws.Tables[0].Rows[0]["Site_Name"].ToString();
            }
            catch { }

        }


        protected void GetBetweenDatesReport()
        {

            DataSet ds = new DataSet();
            DateTime Intime = DateTime.Now, OutTime = DateTime.Now;
            TimeSpan Work;
                        int TotalPages = 1;//returnVale
              int NoofRecords = 100000;

            DateTime dt = FromDate;

            SqlParameter[] objParam = new SqlParameter[7];
            objParam[0] = new SqlParameter("@FromDate", FromDate);
            objParam[1] = new SqlParameter("@ToDate", ToDate);

            objParam[2] = new SqlParameter("@Empid", EmpIDQryStrng);
            objParam[3] = new SqlParameter("@CurrentPage", 1);
            objParam[4] = new SqlParameter("@PageSize", 10000);
            objParam[5] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
            objParam[5].Direction = ParameterDirection.ReturnValue;
            objParam[6] = new SqlParameter("@NoofRecords", System.Data.SqlDbType.Int);
            objParam[6].Direction = ParameterDirection.Output;

            ds = SQLDBUtil.ExecuteDataset("sh_GetAttendanceByBetweenDates", objParam);
            NoofRecords = (int)objParam[6].Value;
            TotalPages = (int)objParam[5].Value;


            //if (Date != "")
            //{
            //    int TotalPages = 1;//returnVale
            //    int NoofRecords = 100;// return value
            //    int PageSize = 50000;
            //    int CurrentPage = 1;
            //    ds = objAtt.GetAttendanceByDay_Cursor(dt, DeptID, WSID, EmpIDQryStrng, "", Convert.ToInt32(Session["CompanyID"]), CurrentPage, PageSize, ref NoofRecords, ref TotalPages, 0);
            //}
            if (ds != null && ds.Tables.Count > 0)
            {
                int count = ds.Tables[0].Rows.Count;

                tblRow = new TableRow();

                tcName = new TableCell();
                tcName.Text = "Name";
                tcName.Style.Add("font-weight", "bold");
                tcName.Width = 200;
                tblRow.Cells.Add(tcName);

                tcDDate = new TableCell();
                tcDDate.Text = "Date";
                tcDDate.Style.Add("font-weight", "bold");
                tcDDate.Width = 70;
                tblRow.Cells.Add(tcDDate);

                tcStatus = new TableCell();
                tcStatus.Text = "Status";
                tcStatus.Style.Add("font-weight", "bold");
                tcStatus.Width = 70;
                tblRow.Cells.Add(tcStatus);

                tcDate = new TableCell();
                tcDate.Text = "Shift";
                tcDate.Style.Add("font-weight", "bold");
                tcDate.Width = 70;
                tblRow.Cells.Add(tcDate);

                tcDate = new TableCell();
                tcDate.Text = "In Time";
                tcDate.Style.Add("font-weight", "bold");
                tcDate.Width = 70;
                tblRow.Cells.Add(tcDate);

                tcDate = new TableCell();
                tcDate.Text = "Out Time";
                tcDate.Style.Add("font-weight", "bold");
                tcDate.Width = 70;
                tblRow.Cells.Add(tcDate);

                tcDate = new TableCell();
                tcDate.Text = "Total Work";
                tcDate.Style.Add("font-weight", "bold");
                tcDate.Width = 90;
                tblRow.Cells.Add(tcDate);

                tcUnStatus = new TableCell();
                tcUnStatus.Text = "Late IN/Early OUT";
                tcUnStatus.Style.Add("font-weight", "bold");
                tcUnStatus.Width = 100;
                tblRow.Cells.Add(tcUnStatus);

                tcStatus = new TableCell();
                tcStatus.Text = "Remarks";
                tcStatus.Style.Add("font-weight", "bold");
                tcStatus.Width = 90;
                tblRow.Cells.Add(tcStatus);




                tblAtt.Rows.Add(tblRow);


                string Department = String.Empty;

                for (int j = 0; j < count; j++)
                {
                    tblRow = new TableRow();
                    tcName = new TableCell();
                    if (ds.Tables[0].Rows.Count != 0)
                    {

                        if (ds.Tables[0].Rows[j][6].ToString().Trim().ToLower() != Department)
                        {

                            Department = ds.Tables[0].Rows[j][6].ToString().Trim().ToLower();
                            tcDesig = new TableCell();
                            if (ds.Tables[0].Rows[j][6].ToString() != null && ds.Tables[0].Rows[j][6].ToString() != "" && ds.Tables[0].Rows[j][6].ToString() != string.Empty)
                            {
                                tcDesig.Text = ds.Tables[0].Rows[j][6].ToString();
                                tcDesig.Style.Add(HtmlTextWriterStyle.FontWeight, "Bold");
                                tcDesig.Style.Add(HtmlTextWriterStyle.Color, "Black");
                                tcDesig.ColumnSpan = tblAtt.Rows[0].Cells.Count;
                                tblRow.Cells.Add(tcDesig);
                                tblAtt.Rows.Add(tblRow);
                            }
                            tblRow = new TableRow();
                        }

                        //tcEmpID = new TableCell();
                        //tcEmpID.Text = ds.Tables[0].Rows[j][0].ToString();
                        //tcEmpID.Width = 40;
                        //tcEmpID.Style.Add("font-weight", "bold");
                        //tblRow.Cells.Add(tcEmpID);


                        tcName.Text = ds.Tables[0].Rows[j][3].ToString();
                        tcName.Width = 200;
                        tblRow.Cells.Add(tcName);

                        tcDDate = new TableCell();
                        tcDDate.Text = ds.Tables[0].Rows[j]["Date"].ToString();
                        tcDDate.Width = 180;
                        tblRow.Cells.Add(tcDDate);

                        tcStatus = new TableCell();
                        tcStatus.Text = ds.Tables[0].Rows[j][1].ToString();
                        tcStatus.Width = 80;
                        tblRow.Cells.Add(tcStatus);




                        tcShift = new TableCell();
                        try { tcShift.Text = ds.Tables[0].Rows[j][8].ToString(); }
                        catch { }
                        tcShift.Width = 80;
                        tblRow.Cells.Add(tcShift);





                        tcDate = new TableCell();
                        if (ds.Tables[0].Rows[j][4].ToString() != "")
                        {
                            tcDate.Text = ds.Tables[0].Rows[j][4].ToString();
                            Intime = Convert.ToDateTime(ds.Tables[0].Rows[j][4].ToString());
                        }
                        else
                            tcDate.Text = "-";
                        tcDate.Width = 80;
                        tblRow.Cells.Add(tcDate);

                        tcDate = new TableCell();
                        if (ds.Tables[0].Rows[j][5].ToString() != "")
                        {
                            tcDate.Text = ds.Tables[0].Rows[j][5].ToString();
                            OutTime = Convert.ToDateTime(ds.Tables[0].Rows[j][5].ToString());
                        }
                        else
                            tcDate.Text = "-";
                        tcDate.Width = 80;
                        tblRow.Cells.Add(tcDate);
                        Work = OutTime.Subtract(Intime);

                        tcDate = new TableCell();
                        if (ds.Tables[0].Rows[j][5].ToString() != "" || ds.Tables[0].Rows[j][4].ToString() != "")
                        {
                            tcDate.Text = Work.ToString() + " Hrs";
                        }
                        if (ds.Tables[0].Rows[j][5].ToString() == "" || ds.Tables[0].Rows[j][4].ToString() == "")
                        {
                            tcDate.Text = "00:00:00 Hrs";
                        }
                        tcDate.Width = 80;
                        tblRow.Cells.Add(tcDate);


                        tcUnStatus = new TableCell();
                        tcUnStatus.Text = ds.Tables[0].Rows[j]["Late"].ToString();
                        tcUnStatus.Width = 100;
                        tblRow.Cells.Add(tcUnStatus);

                        tcDate = new TableCell();
                        if (ds.Tables[0].Rows[j][7].ToString() != "")
                            tcDate.Text = ds.Tables[0].Rows[j][7].ToString();
                        else
                            tcDate.Text = "NA";
                        tcDate.Width = 80;
                        tblRow.Cells.Add(tcDate);
                        //tblRow.Cells.Add(tcEmpID);



                        tblAtt.Rows.Add(tblRow);


                    }
                }
            }


        }
        void GetAttandanceandDisplay()
        {
            
                

                DateTime StartDate = FromDate, EndDate = ToDate;
                List<DateTime> dateList = new List<DateTime>();
                int DayInterval = 1;
                int TotalPages = 1;//returnVale
                int NoofRecords = 100;// return value
                int PageSize = 50000;
                int CurrentPage = 1;

                DataSet dsEPMData = AttendanceDAC.HR_GetAttandanceByPaging(EmpIDQryStrng, 0, 0, 0, StartDate, EndDate
                    , CurrentPage, PageSize, ref NoofRecords, ref TotalPages, "",0);


                tblAtt.Rows.Clear();
                tblAtt.Style.Add("border", "solid red 1px");
                tblAtt.Style.Add("border-collapse", "collapse");
                //2 
                Boolean isFirst = true;
                TableRow tblHeadRow = new TableRow();
                TableRow tblDepartRow = new TableRow();
                tblRow = new TableRow();
                int DeptID_loop = 0;
                Hashtable ht = new Hashtable();
                int WidthP = 30;
                int WidthPName = 300;
                foreach (DataRow drEMP in dsEPMData.Tables[2].Rows)
                {
                    tblHeadRow = new TableRow();
                    
                    tblRow = new TableRow();
                    tblDepartRow = null;
                    ht = new Hashtable();
                    if (isFirst)
                        // for Header
                        CellNameWriting_Head(ref tblHeadRow, WidthPName, "Name");
                    if (DeptID_loop != Convert.ToInt32(drEMP["DepID"]))
                    {
                        tblDepartRow = new TableRow();
                        tblDepartRow.Style.Add("border", "solid red 1px");

                        CellNameWriting_Head(ref tblDepartRow, WidthPName, drEMP["DepName"].ToString());
                        DeptID_loop = Convert.ToInt32(drEMP["DepID"]);
                        //tblAtt.Rows.Add(tblRow);
                        //tblRow = new TableRow();
                    }
                    CellNameWriting(ref tblRow, WidthPName, drEMP["Name"].ToString());
                    StartDate = FromDate;
                    while (StartDate.AddDays(DayInterval - 1) < EndDate)
                    {
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
                                        CellNameWriting_Green(ref tblRow, WidthP, drsAtt[0]["ShortName"].ToString(), false, true);
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

                    isFirst = false;
                }
            
               
          
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
                tcName.Style.Add("background-color", "#87cefa");
            else
                tcName.Style.Add("background-color", "#A1F9DB");
            tcName.Style.Add("text-align", "center");
            tcName.Width = Width;
            tblRow.Cells.Add(tcName);

        }
        protected void GetMonthlyReport()
        {
            tblAtt.Rows.Clear();
            //DataSet ds = new DataSet();
            AttendanceDAC objAtt = new AttendanceDAC();

            if (Month == 1)
            {
                Month = 12;
                Year = Year - 1;
            }
            else
                Month = Month - 1;

            DataSet ds = objAtt.GetAttendanceByMonth_Cursor(Month, Year, DeptID, WSID, EmpIDQryStrng, "", null);


            string Department = String.Empty;

            int count = ds.Tables.Count;
            if (ds != null && ds.Tables.Count != 0)
            {

                System.Collections.Generic.List<DateTime> listHolidays = new System.Collections.Generic.List<DateTime>();
                foreach (DataRow dr in ds.Tables[ds.Tables.Count - 1].Rows)
                {
                    listHolidays.Add(Convert.ToDateTime(dr["Date"]));
                }

                Double Present = 0; int Scope = 0;
                if (Month != 0)
                {
                   

                    DataSet startdate = AttendanceDAC.GetStartDate();
                    string st = Month + "/" + startdate.Tables[0].Rows[0][0].ToString() + "/" + Year;
                    DateTime dt = DateTime.ParseExact(st, "M/dd/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                    DateTime dtEnd = dt.AddMonths(1);



                    tblRow = new TableRow();

                    tcName = new TableCell();
                    tcName.Text = "Name";
                    tcName.Style.Add("font-weight", "bold");
                    tcName.Width = 300;
                    tblRow.Cells.Add(tcName);

                   

                    for (int i = dt.Day; dt != dtEnd;)
                    {
                        tcDate = new TableCell();
                        tcDate.Text = i.ToString();
                        tcDate.Style.Add("font-weight", "bold");
                        tcDate.Width = 60;
                        tblRow.Cells.Add(tcDate);
                        dt = dt.AddDays(1);
                        i = dt.Day;
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



                    //dt = Convert.ToDateTime(Month + "/1/" + Year);
                    //dtEnd = dt.AddMonths(1);

                    st = Month + "/" + Convert.ToInt32(startdate.Tables[0].Rows[0][0].ToString()) + "/" + Year;
                    dt = DateTime.ParseExact(st, "M/dd/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                    dtEnd = dt.AddMonths(1);



                    for (int j = 0; j < count - 1; j++)
                    {
                        tblRow = new TableRow();
                        tcName = new TableCell();
                        tcEmpID = new TableCell();
                        if (ds.Tables[0].Rows.Count != 0)
                        {
                            if (ds.Tables[0].Rows[j][6].ToString().Trim().ToLower() != Department)
                            {

                                Department = ds.Tables[0].Rows[j][6].ToString().Trim().ToLower();
                                tcDesig = new TableCell();
                                if (ds.Tables[0].Rows[j][6].ToString() != null && ds.Tables[0].Rows[j][6].ToString() != "" && ds.Tables[0].Rows[j][6].ToString() != string.Empty)
                                {
                                    tcDesig.Text = ds.Tables[0].Rows[j][6].ToString();
                                    tcDesig.Style.Add(HtmlTextWriterStyle.FontWeight, "Bold");
                                    tcDesig.Style.Add(HtmlTextWriterStyle.Color, "Black");
                                    tcDesig.ColumnSpan = tblAtt.Rows[0].Cells.Count;
                                    tblRow.Cells.Add(tcDesig);
                                    tblAtt.Rows.Add(tblRow);
                                }
                                tblRow = new TableRow();
                            }
                            int LA = 0;
                            if (ds.Tables[0].Rows[j][0].ToString() != null && ds.Tables[0].Rows[j][0].ToString() != "" && ds.Tables[0].Rows[j][0].ToString() != string.Empty)
                            {
                                tcEmpID.Text = ds.Tables[0].Rows[j][0].ToString();
                                //    tcEmpID.Width = 300;
                                //    tblRow.Cells.Add(tcEmpID);
                                int EmpID = Convert.ToInt32(ds.Tables[0].Rows[j][0].ToString());
                                //DataSet dsLeaveTotal = new DataSet();
                                //int Month = Convert.ToInt32(ddlMonth.SelectedValue);
                                //int Year = Convert.ToInt32(ddlYear.SelectedValue);
                                DataSet dsLeaveTotal = AttendanceDAC.EmpMonthLeaveCount(EmpID, Month, Year);

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
                            }

                            if (ds.Tables[0].Rows[j][3].ToString() != null && ds.Tables[0].Rows[j][3].ToString() != "" && ds.Tables[0].Rows[j][3].ToString() != string.Empty)
                            {
                                tcName.Text = ds.Tables[0].Rows[j][3].ToString();
                                tcName.Width = 300;
                                tblRow.Cells.Add(tcName);
                            }


                            Present = 0; Scope = 0;
                            int Absent, OD, CL, EL, SL;
                            Absent = OD = CL = EL = SL = 0;
                            for (int i = 1; dt != dtEnd; i++)               // Dates no of days
                            {
                                tcDate = new TableCell();
                                DateTime dtCurrent = new DateTime(Year, Month, i);
                                if (listHolidays.Contains(dtCurrent))
                                {
                                    tcDate.Text = "PH";
                                    tcDate.Style.Add("background-color", "lightgreen");
                                }
                                else if (dtCurrent.DayOfWeek == 0)
                                {
                                    tcDate.Text = "WO";
                                    tcDate.Style.Add("background-color", "lightblue");
                                }
                                else
                                {
                                    Scope++;
                                }

                                if (ds.Tables[0].Rows.Count > 0)
                                {


                                    for (int k = 0; k < ds.Tables[0].Rows.Count; k++)    // No of Emp
                                    {

                                        if (ds.Tables[0].Rows[k][2].ToString() != "")
                                        {
                                            if (dt == Convert.ToDateTime(ds.Tables[0].Rows[k][2].ToString()).Date)
                                            {
                                                tcDate.Text = ds.Tables[0].Rows[k][1].ToString();
                                                if (ds.Tables[0].Rows[k][1].ToString() == "P" || ds.Tables[0].Rows[k][1].ToString() == "OD")
                                                {
                                                    tcDate.Style.Add("color", "green");

                                                    //Present++;
                                                }

                                                else if (ds.Tables[0].Rows[k][1].ToString() == "WO")
                                                {
                                                    tcDate.Style.Add("color", "red");
                                                }
                                                else if (ds.Tables[0].Rows[k][1].ToString() == "HD")
                                                {
                                                    tcDate.Style.Add("color", "green");

                                                    Present = Present + 0.5;
                                                }
                                                if (ds.Tables[0].Rows[k][1].ToString() == "P")
                                                {
                                                    Present++;
                                                }
                                                if (ds.Tables[0].Rows[k][1].ToString() == "A")
                                                {
                                                    Absent++;
                                                }
                                                if (ds.Tables[0].Rows[k][1].ToString() == "OD")
                                                {
                                                    OD++;
                                                }
                                                if (ds.Tables[0].Rows[k][1].ToString() == "CL")
                                                {
                                                    CL++;
                                                }
                                                if (ds.Tables[0].Rows[k][1].ToString() == "EL")
                                                {
                                                    EL++;
                                                }
                                                if (ds.Tables[0].Rows[k][1].ToString() == "SL")
                                                {
                                                    SL++;
                                                }
                                                break;
                                            }
                                            else
                                            {
                                                tcDate.Style.Add("color", "red");
                                                if (listHolidays.Contains(dtCurrent))
                                                {
                                                    tcDate.Text = "PH";
                                                }
                                                else if (dtCurrent.DayOfWeek == 0)
                                                {
                                                    tcDate.Text = "WO";
                                                }
                                                else
                                                {
                                                    tcDate.Text = "-";
                                                }
                                            }
                                        }
                                        else
                                        {
                                            if (listHolidays.Contains(dtCurrent))
                                            {
                                                tcDate.Text = "PH";
                                            }
                                            else if (dtCurrent.DayOfWeek == 0)
                                            {
                                                tcDate.Text = "WO";
                                            }
                                            else
                                            {
                                                tcDate.Text = "-";
                                            }
                                        }


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

                            TableCell tc1 = new TableCell();



                            if (DateTime.Now.Month.ToString() != Month.ToString())
                            {

                                tcLnkbtn = new TableCell();
                                tcLnkbtn.Text = "PaySlip";
                                tcLnkbtn.Visible = false;
                                tcLnkbtn.Style.Add("font-weight", "bold");
                                tcLnkbtn.Width = 200;
                                tblRow.Cells.Add(tcLnkbtn);

                                LinkButton lnkbtn = new LinkButton();
                                lnkbtn.Text = "PaySlip";
                                lnkbtn.PostBackUrl = "~/PaySlipPreview.aspx?id=" + tcEmpID.Text + "&Date=" + Convert.ToDateTime(Month.ToString() + "/1/" + Year.ToString());
                                tcLnkbtn.Controls.Add(lnkbtn);
                            }
                            dt = Convert.ToDateTime(Month.ToString() + "/1/" + Year.ToString());
                            dtEnd = dt.AddMonths(1);

                            tblAtt.Rows.Add(tblRow);


                        }
                    }
                }
            }
        }

        private string Convertdate(string StrDate)
        {
            if (StrDate != "")
            {
                StrDate = StrDate.Split('/')[1].ToString() + "/" + StrDate.Split('/')[0].ToString() + "/" + StrDate.Split('/')[2].ToString();
            }
            return StrDate;
        }
    }
}