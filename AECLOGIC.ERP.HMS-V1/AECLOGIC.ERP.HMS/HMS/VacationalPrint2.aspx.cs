using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using AECLOGIC.HMS.BLL;
using AECLOGIC.ERP.COMMON;
using System.Data.SqlClient;
using Aeclogic.Common.DAL;
using System.Globalization;

namespace AECLOGIC.ERP.HMS
{
    public partial class VacationalPrint2 : Page
    {
        string exptdetails = string.Empty;
        int AttMonth, AttYear; int Empid; string date;
        AttendanceDAC objatt = new AttendanceDAC();
        DataSet ds = new DataSet();
        int mid = 0;
        static double Amt = 0.0;
        static int empid = 0;
        static int chk = 0;
        static decimal oty = 0;
        bool viewall, Editable, Checked;
        string menuname;
        string menuid;
        HRCommon objHrCommon = new HRCommon();
        DateTime stdt;
        DateTime eddt;
        //protected override void OnInit(EventArgs e)
        //{
        //    ModuleID = 1;
        //    base.OnInit(e);
        //}
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString.Count > 0)
                {
                    Empid = Convert.ToInt32(Request.QueryString["Empid"]);
                    date = Request.QueryString["Date"].ToString();
                    Checked = Convert.ToBoolean(Request.QueryString["Checked"]);
                }
                EmployeBind();
                QtyChanged(sender, e);
            }

        }


        protected void GVVacation_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            GridView GridView1 = (GridView)sender;
            DataListItem dlItem = (DataListItem)GridView1.Parent.Parent;
            DataListItemEventArgs dle = new DataListItemEventArgs(dlItem);
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int index = e.Row.RowIndex;
                if (index == 0 || index == 1 || index == 7 || index == 9)
                {
                    TextBox txtQtyOrder10 = (TextBox)e.Row.FindControl("txtA1");

                    txtQtyOrder10.ReadOnly = true;
                }
                //if (index == 0)
                //{
                //    LinkButton lnkbtn = (LinkButton)e.Row.FindControl("lnkatt_Viewk");
                //    lnkbtn.Visible = true;
                //}
                //if (index == 11)
                //{
                //    Button btnCals = (Button)e.Row.FindControl("btnCal");
                //    btnCals.Visible = true;
                //}
                if (index == 5 || index == 6 || index == 12 || index == 13)
                {
                    TextBox txtA6 = (TextBox)e.Row.FindControl("txtA6");
                    txtA6.Visible = true;
                }

            }

        }

        protected void GVVacation_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }
        protected void dtlvacation_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            GridView GridView1 = (GridView)e.Item.FindControl("GridView1");

        }

        void EmployeBind()
        {

            try
            {
                //objHrCommon.PageSize = EmpListPaging.ShowRows;
                //objHrCommon.CurrentPage = EmpListPaging.CurrentPage;
                //int empid = 0;
                int month = 0;
                int year = 0;
                string ename = "";


                empid = Empid;



                ename = null;

                if (date != "" || date != string.Empty)
                {


                    month = (DateTime.ParseExact(date, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).Month);
                    //month = Dmonth.Month;
                    year = (DateTime.ParseExact(date, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).Year);
                }

                DataSet startdate = AttendanceDAC.GetStartDate();
                // for Jan 2016 selection pay slip showing by Gana
                int Month, Year;
                Month = (DateTime.ParseExact(date, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).Month);
                //month = Dmonth.Month;
                Year = (DateTime.ParseExact(date, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).Year);
                if (Month == 1)
                {
                    if ((DateTime.ParseExact(date, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).Day) < Convert.ToInt32(startdate.Tables[0].Rows[0][0].ToString()))
                    {
                        Month = 12;
                        Year = Year - 1;
                    }
                }
                else
                {
                    if ((DateTime.ParseExact(date, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).Day) < Convert.ToInt32(startdate.Tables[0].Rows[0][0].ToString()))
                        Month = Month - 1;
                }

                AttMonth = Month;
                AttYear = Year;

                // string st = ddlMonth.SelectedItem.Value + "/" + startdate.Tables[0].Rows[0][0].ToString() + "/" + ddlYear.SelectedItem.Value;
                string st = Month + "/" + startdate.Tables[0].Rows[0][0].ToString() + "/" + Year;
                DateTime stdt = CODEUtility.ConvertToDate(st, DateFormat.MonthDayYear);
                DateTime enddate = DateTime.ParseExact(date, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

                int i;
                try
                {
                    SqlParameter[] p = new SqlParameter[3];

                    p[0] = new SqlParameter("@Year", Year);
                    p[1] = new SqlParameter("@Empid", empid);
                    p[2] = new SqlParameter("@Form", "F");
                    i = Convert.ToInt32(SqlHelper.ExecuteScalar("HMS_CountVacationPost", p));


                }
                catch (Exception ex)
                {
                    throw ex;
                }

                //if (i != 0)
                //{
                //    btnAccPost.Visible = false;
                //    AlertMsg.MsgBox(Page, "Already Posted To Accounts");
                //    return;
                //}

                //else
                //{


                DataSet ds = new DataSet();
                if (!Checked)
                {
                    ds = AttendanceDAC.T_HMS_GetVacation(objHrCommon, empid, ename, 0, 0, month, year, stdt, enddate);
                }
                else
                {
                    SqlParameter[] sqlParams = new SqlParameter[13];
                    sqlParams[0] = new SqlParameter("@CurrentPage", objHrCommon.CurrentPage);
                    sqlParams[1] = new SqlParameter("@PageSize", objHrCommon.PageSize);
                    sqlParams[2] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
                    sqlParams[2].Direction = ParameterDirection.ReturnValue;
                    sqlParams[3] = new SqlParameter("@NoofRecords", System.Data.SqlDbType.Int);
                    sqlParams[3].Direction = ParameterDirection.Output;
                    sqlParams[4] = new SqlParameter("@Empid", empid);
                    sqlParams[5] = new SqlParameter("@Ename", ename);
                    sqlParams[6] = new SqlParameter("@WSid", SqlDbType.Int);
                    sqlParams[7] = new SqlParameter("@Deptid", SqlDbType.Int);
                    sqlParams[8] = new SqlParameter("@month", month);
                    sqlParams[9] = new SqlParameter("@year", year);
                    sqlParams[10] = new SqlParameter("@StDate", stdt);
                    sqlParams[11] = new SqlParameter("@EndDate", enddate);
                    sqlParams[12] = new SqlParameter("@leavetype", DBNull.Value);

                    ds = SQLDBUtil.ExecuteDataset("T_HMS_GetEmployeeVacation_FinalSettlement", sqlParams);
                }
                int tablcnt = ds.Tables.Count;
                if (ds != null && ds.Tables.Count != 0 && ds.Tables[tablcnt - 1].Rows.Count > 0)
                {
                    ViewState["DataSet"] = ds.Tables[tablcnt - 1];
                    dtlvacation.DataSource = ds.Tables[tablcnt - 1];
                    dtlvacation.DataBind();

                    //  btnAccPost.Visible = true;
                    //GVVacation.DataSource = ds;
                    //GVVacation.DataBind();
                    //gvRMItem.DataSource = ds;
                    //gvRMItem.DataBind();
                    //EmpListPaging.Visible = true;
                }
                else
                {
                    dtlvacation.DataSource = null;
                    dtlvacation.DataBind();
                    AlertMsg.MsgBox(Page, "No Record Found");
                    //EmpListPaging.Visible = false;
                    //gvRMItem.DataSource = null;
                    //gvRMItem.DataBind();
                    // btnAccPost.Visible = false;
                }

                // EmpListPaging.Bind(objHrCommon.CurrentPage, objHrCommon.TotalPages, objHrCommon.NoofRecords, objHrCommon.PageSize);

            }

          //  }
            catch (Exception e)
            {
                throw e;
            }


        }
        protected void QtyChanged(object sender, EventArgs e)
        {
            decimal resultD1 = 0, resultD2 = 0;
            decimal totalA = 0, totalD = 0, Total = 0;
            foreach (DataListItem li in dtlvacation.Items)
            {

                GridView gv = (GridView)li.FindControl("GVVacation");

                foreach (GridViewRow row in gv.Rows)
                {

                    TextBox Bookqty = (TextBox)row.FindControl("txtA1");

                    if (row.RowIndex == 1)
                    {
                        TextBox txtQtyOrder10 = (TextBox)row.FindControl("txtA1");
                        oty = Convert.ToDecimal(txtQtyOrder10.Text);

                        DataTable dt = (DataTable)ViewState["DataSet"];


                        // if (oty > Convert.ToInt32(dt.Rows[0]["GrantedDays"]))
                        // { AlertMsg.MsgBox(Page, "Please Enter EAL Less than Granted Days"); chk = 0; return; }
                        //else
                        //{
                        //    chk = 1;
                        //}
                    }


                    if (chk == 1)
                    {
                        int month1 = 0, year1 = 0;
                        if (date != "" || date != string.Empty)
                        {


                            month1 = (DateTime.ParseExact(date, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).Month);
                            //month = Dmonth.Month;
                            year1 = (DateTime.ParseExact(date, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).Year);
                        }

                        if (month1 == 1)
                        {
                            month1 = 12;
                            year1 = year1 - 1;
                        }
                        else { month1 = month1 - 1; }
                        int daysinmonth = System.DateTime.DaysInMonth(year1, month1);

                        int EmpSalID, Value = 0, AmoundD = 0;
                        DataSet dds = AttendanceDAC.GetEmpSalList(empid);
                        EmpSalID = Convert.ToInt32(dds.Tables[0].Rows[dds.Tables[0].Rows.Count - 1]["EmpSalID"]);

                        DataSet ds2 = PayRollMgr.GetEmpAllowancesList(empid, EmpSalID);
                        if (ds2 != null && ds2.Tables.Count > 0)
                        {
                            Value = Convert.ToInt32(ds2.Tables[0].Rows[1]["Value"]); //ds.Tables[1].Rows[0]["Value"].ToString();
                        }

                        resultD1 = oty * (Value / daysinmonth);

                        DataSet ds3 = PayRollMgr.GetEmpNonCTCComponentsList(empid, EmpSalID);
                        if (ds3 != null && ds3.Tables.Count > 0)
                        {
                            AmoundD = Convert.ToInt32(ds3.Tables[0].Rows[0]["Amount"]); //ds.Tables[1].Rows[0]["Value"].ToString();
                        }
                        resultD2 = oty * (AmoundD / daysinmonth);

                    }

                    //if (row.RowIndex == 5)
                    //{
                    //    TextBox txtQtyOrder10 = (TextBox)row.FindControl("txtA1");
                    //    txtQtyOrder10.Text = resultD1.ToString();
                    //}

                    //if (row.RowIndex == 6)
                    //{
                    //    TextBox txtQtyOrder10 = (TextBox)row.FindControl("txtA1");
                    //    txtQtyOrder10.Text = resultD2.ToString();
                    //}


                    //if (row.RowIndex == 0 || row.RowIndex == 1 || row.RowIndex == 2 || row.RowIndex == 3 || row.RowIndex == 4 || row.RowIndex == 5)
                    if (row.RowIndex <= 6)
                    {
                        //if (row.RowIndex == 1) {

                        //}
                        //               else { 
                        TextBox tb = (TextBox)row.FindControl("txtA1");
                        decimal sum;
                        if (decimal.TryParse(tb.Text.Trim(), out sum))
                        {
                            totalA += sum;
                        }
                        //}
                    }
                    else
                    {
                        TextBox tb = (TextBox)row.FindControl("txtA1");
                        decimal sum;
                        if (decimal.TryParse(tb.Text.Trim(), out sum))
                        {
                            totalD += sum;
                        }
                    }
                    Total = totalA - totalD;

                    Label lblval = (Label)gv.FooterRow.FindControl("lblvalue");
                    lblval.Text = Total.ToString();
                    Amt = Convert.ToDouble(Total);
                }


            }

            //BindTransdetails("0");
        }

        public DataView BindTransdetails(string TransId)
        {
            // int resultD1 = 0, resultD2 = 0;
            DataSet d1 = new DataSet();
            DataTable dt = new DataTable();
            DataRow row = dt.NewRow();
            DataColumn column = new DataColumn();
            int Month;
            string[] sdt = date.Split('/');

            Month = Convert.ToInt32(sdt[1]);
            int Year = Convert.ToInt32(sdt[2]);

            int valofSync = 0;
            try
            {
                //  Button btn = sender as Button;
                //if (btn.ID.Trim().ToLower() == "btnsync")
                //    valofSync = 0;
                //else
                //    valofSync = 1;
            }
            catch { }

            //        if (Month == 1)
            //        {
            //            Month = 12;
            //            Year = Year - 1;
            //        }
            //        else
            //            Month = Month - 1;

            //// string st = ddlMonth.SelectedItem.Value + "/" + startdate.Tables[0].Rows[0][0].ToString() + "/" + ddlYear.SelectedItem.Value;

            string st = Month + "/" + sdt[0] + "/" + Year;
            DateTime edt = DateTime.ParseExact(date, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            AttendanceDAC.HR_SavePaySLIP2(Convert.ToInt32(TransId), st, valofSync, "Vactional Settlement");
            // ds = PayRollMgr.GetPaySLIP_CTH(Convert.ToInt32(TransId), Convert.ToDateTime(txtdate.Text.Trim()));HR_AbsentPenalitiesByPaging
            ds = PayRollMgr.GetPaySLIP(Convert.ToInt32(TransId), edt);
            double salamount = 0;
            string Details = "";
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[5].Rows.Count > 0)
            {
                // lblatt.Text = ds.Tables[5].Rows[0]["WorkingDays"].ToString();
                salamount = Convert.ToDouble(ds.Tables[1].Rows[0]["Value"].ToString());
                for (int k = 0; k < ds.Tables[2].Rows.Count; k++)
                {
                    salamount = salamount + Convert.ToDouble(ds.Tables[2].Rows[k]["Value"].ToString());
                }
                for (int k = 0; k < ds.Tables[23].Rows.Count; k++)
                {
                    salamount = salamount + Convert.ToDouble(ds.Tables[23].Rows[k]["Value"].ToString());
                }

                //ds.Tables[9].Rows[0]["NetAmount"].ToString(); //ds.Tables[1].Rows[0]["Value"].ToString();
                Details = ds.Tables[15].Rows[0]["Details"].ToString();
                // lblA2.Text = (Convert.ToDecimal(lblAlGross.Text) * (Convert.ToDecimal(ds.Tables[0].Rows[0]["MontlyCTC"]) / 30)).ToString();
            }

            int month1 = 0, year1 = 0, day = 0;

            if (date != "" || date != string.Empty)
            {


                month1 = (DateTime.ParseExact(date, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).Month);
                //month = Dmonth.Month;
                year1 = (DateTime.ParseExact(date, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).Year);
                day = (DateTime.ParseExact(date, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).Day);
            }

            if (month1 == 1)
            {
                month1 = 12;
                year1 = year1 - 1;
            }
            else
            {
                if (day < 21)
                    month1 = month1 - 1;

            }


            string Absamount = "0";
            string AbsentDetails = "";
            d1 = AttendanceDAC.HR_AbsentPenalitiesByPaging_vacationsettlement(objHrCommon, 0, 0, 0, month1, year1, empid);
            dt.Columns.Add("Description", typeof(string));
            dt.Columns.Add("Amount", typeof(string));
            dt.Columns.Add("Details", typeof(string));
            string Airtktamount = "0";
            string AirtktDetails = "";

            if (!Checked)
            {
                if (d1 != null && d1.Tables.Count > 0 && d1.Tables.Cast<DataTable>().Any(table => table.Rows.Count != 0))
                {
                    Absamount = d1.Tables[0].Rows[0]["Amount"].ToString(); //ds.Tables[1].Rows[0]["Value"].ToString();
                    AbsentDetails = "Occurance=" + d1.Tables[0].Rows[0]["Occurance"].ToString() + "; Absents=" + d1.Tables[0].Rows[0]["Absents"].ToString() + "; Penalities=" + d1.Tables[0].Rows[0]["Penalities"].ToString() + ";Limited=" + d1.Tables[0].Rows[0]["Limited"].ToString() + "; Amount=" + d1.Tables[0].Rows[0]["Amount"].ToString() + ";";
                }


                ButtonField btnMeds = new ButtonField();
                //Initalize the DataField value.
                btnMeds.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                btnMeds.CommandName = "Meds";
                btnMeds.ButtonType = ButtonType.Button;
                btnMeds.Text = "Meds";
                btnMeds.Visible = true;



                //Add the newly created bound field to the GridView.
                dt.Columns.Add("Det", typeof(string));

                int daysinmonth = System.DateTime.DaysInMonth(year1, month1);




                double Total = 0;

                DataTable dstrans = (DataTable)ViewState["DataSet"];
                dt.Rows.Add("A1:Salary For the Current Month Attendance ", String.Format("{0:0.00}", salamount), Details);
                Total = salamount;
                decimal EAL = Convert.ToDecimal(ds.Tables[16].Rows[0]["HRA"].ToString());
                string A2Label = string.Empty;
                A2Label = ds.Tables[19].Rows[0]["A2Label"].ToString();
                dt.Rows.Add("A2:Encashment of AL(EAL)", EAL, A2Label);
                Total = Total + Convert.ToDouble(EAL);
                double OT;
                string otdet = "";
                if (dstrans.Rows[0]["Amount"] != null || dstrans.Rows[0]["Amount"].ToString() != "")
                {
                    otdet = "OT Rate Per Hour = Month Basic Salary(" + String.Format("{0:0.00}", ds.Tables[0].Rows[0]["MontlyCTC"].ToString()) + ")/(DaysInaMonth(" + ds.Tables[24].Rows[0]["daysinmonth"].ToString() + ")*(Work Shift Hours- Break Time)(" + dstrans.Rows[0]["shiftours"].ToString() + "))=" + String.Format("{0:0.00}", Convert.ToDecimal(ds.Tables[0].Rows[0]["MontlyCTC"].ToString()) / (Convert.ToDecimal(ds.Tables[24].Rows[0]["daysinmonth"].ToString()) * Convert.ToDecimal(dstrans.Rows[0]["shiftours"].ToString()))) + " ; OT Amount = OT Rate Per Hour*OT_Hours*Pay-Coeff;";
                }
                else
                {


                }
                Total = Total + Convert.ToDouble(dstrans.Rows[0]["Amount"]);

                dt.Rows.Add("A3:Over-Time Amount", dstrans.Rows[0]["Amount"], otdet);
                dt.Rows.Add("A4:Air Ticket Reimbursement", "0");
                dt.Rows.Add("A5:Exit Entry Visa Reimbursement", "0");
                dt.Rows.Add("A6:", 0);
                dt.Rows.Add("A7:", 0);
                decimal TransAllowance = Convert.ToDecimal(ds.Tables[17].Rows[0]["D1"].ToString());
                Total = Total + Convert.ToDouble(TransAllowance);
                decimal FoodAllowance = Convert.ToDecimal(ds.Tables[18].Rows[0]["D2"].ToString());
                Total = Total + Convert.ToDouble(FoodAllowance);
                dt.Rows.Add("D1: DYNAMISM ", (TransAllowance + FoodAllowance), ds.Tables[21].Rows[0]["D1Label"].ToString() + ds.Tables[22].Rows[0]["D2Label"].ToString());
                Total = Total + Convert.ToDouble(dstrans.Rows[0]["Amount"]);
                dt.Rows.Add("D2: Other manual deductions ", "0", "");
                Total = Total + Convert.ToDouble(dstrans.Rows[0]["Amount"]);
                dt.Rows.Add("D3:OutStanding  Advances", ds.Tables[20].Rows[0]["D3"].ToString());
                //dt.Rows.Add("D4:Deduction for Medical Card", "0");
                dt.Rows.Add("D4:Absent Penalty ", Absamount, AbsentDetails);
                dt.Rows.Add("D5:Expat ", 0, exptdetails, btnMeds);
                dt.Rows.Add("D6:", 0);
                dt.Rows.Add("D7:", 0);
                dt.Rows.Add("Remarks", "");
                //QtyChanged(null,null);
            }
            else
            {
                double Total = 0;
                dt.Rows.Add("A1:Salary For the Current Month Attendance ", "0");
                decimal EAL = Convert.ToDecimal(ds.Tables[16].Rows[0]["HRA"].ToString());
                string A2Label = string.Empty;
                A2Label = ds.Tables[19].Rows[0]["A2Label"].ToString();
                dt.Rows.Add("A2:Encashment of AL(EAL)", EAL, A2Label);

                DataSet dsg = new DataSet();
                dsg = AttendanceDAC.empVsAirTicketsAuth_LISTbyID(Convert.ToInt32(ds.Tables[0].Rows[0]["Empid"].ToString()));
                if (dsg != null && dsg.Tables.Count > 0 && dsg.Tables.Cast<DataTable>().Any(table => table.Rows.Count != 0))
                {
                    Airtktamount = dsg.Tables[0].Rows[0]["Amount"].ToString();
                    AirtktDetails = "From City - " + dsg.Tables[0].Rows[0]["from_city"].ToString() + "; To City-" + dsg.Tables[0].Rows[0]["to_city"].ToString() + "; No. tickets =" + dsg.Tables[0].Rows[0]["Tickets"].ToString() + "; Fare = " + dsg.Tables[0].Rows[0]["fare_rate"].ToString() + ";";

                }
                else
                {
                    Airtktamount = "0";
                }


                Total = Total + Convert.ToDouble(EAL);
                dt.Rows.Add("A3:Over-Time Amount", "0");
                dt.Rows.Add("A4:Air Ticket Reimbursement", Airtktamount, AirtktDetails);
                dt.Rows.Add("A5:Exit Entry Visa Reimbursement", "0");
                dt.Rows.Add("A6:", 0);
                dt.Rows.Add("A7:", 0);
                dt.Rows.Add("D1: DYNAMISM ", "0");
                dt.Rows.Add("D2: Other manual deductions ", "0", "");
                dt.Rows.Add("D3:OutStanding  Advances", "0");
                dt.Rows.Add("D4:Absent Penalty ", "0");
                dt.Rows.Add("D5:Expat ", 0);
                dt.Rows.Add("D6:", 0);
                dt.Rows.Add("D7:", 0);
                dt.Rows.Add("Remarks", "");
            }

            DataView dv = dt.DefaultView;

            return dv;




        }
    }
}