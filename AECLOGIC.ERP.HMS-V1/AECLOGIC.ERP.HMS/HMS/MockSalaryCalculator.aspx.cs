using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Collections.Generic;
using AECLOGIC.HMS.BLL;
using DataAccessLayer;
using System.IO;
using AECLOGIC.ERP.HMS;
namespace AECLOGIC.ERP.HMS
{
    public partial class MockSalaryCalculator : AECLOGIC.ERP.COMMON.WebFormMaster
    {
        AttendanceDAC objAtt = new AttendanceDAC();
        HRCommon obj = new HRCommon();
        HRCommon objHrCommon = new HRCommon();
        protected void Page_Load(object sender, EventArgs e)
        {

            try
            {
               

                if (!IsPostBack)
                {
                   
                    txtassesmentstrt_date.Text = DateTime.Now.ToString(ConfigurationManager.AppSettings["DateFormat"]);
                    ddlworksites.SelectedValue = "1";
                    BindWorkSite();
                    BindDepartments();
                    BindLeaveTypes();
                    BindYears();
                    BindYears(ddlyaer1);
                    // ddlYear.SelectedItem.Text = DateTime.Now.Year.ToString();
                    chkholidaysConfig.Enabled = false;
                    ChkLeaveEntitlement.Enabled = false;
                    chkWeekOffs.Enabled = false;
                    txtFromData_Leav.Enabled = false;
                    txtToData_Leave.Enabled = false;
                    btnleaveSubmit.Visible = false;
                }

            }
            catch { }
        }
        protected override void OnInit(EventArgs e)
        {
            ModuleID = 1;
            base.OnInit(e);
            //ModalPopupExtenderk.Hide();
        }
      

         protected void  View_LeaveEnti(object sender, EventArgs e)
        {
            try
            {
              DataSet  ds = Leaves.GetEntitlementList(Convert.ToInt32(HDEmpnatureID.Value));
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    gvLeaveEL.DataSource = ds;
                }
                gvLeaveEL.DataBind();

                ModalPopupExtender4.Show();
                //ModalPopupExtender3.Hide ();
                // ModalPopupExtenderk.Hide();
            }
            catch
            {
  ModalPopupExtender4.Show();
            }
        }
        protected void View_holidays(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = Leaves.GetListofHDList(Convert.ToInt32(ddlYear.SelectedItem.Text), Convert.ToInt32(HDEmpnatureID.Value));
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    gvLeaveProfile.DataSource = ds;
                }
                gvLeaveProfile.DataBind();
                ModalPopupExtender3.Show();
                // ModalPopupExtenderk.Hide ();
                //ModalPopupExtender4.Hide();
            }
            catch
            {
                ModalPopupExtender3.Show();
            }
        }
      protected  void View_WeekOffs(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = AttendanceDAC.HR_GetWOConfigByEmpNature(Convert.ToInt32(HDEmpnatureID.Value));
                string[] strWeeks = new string[] { "SUN", "MON", "TUE", "WED", "THU", "FRI", "SAT" };
                for (int i = 1; i <= strWeeks.Length; i++)
                {
                    for (int j = 1; j <= 5; j++)
                    {
                        DataRow[] drS = ds.Tables[0].Select("WeekNo='" + j.ToString() + "' AND Day='" + i.ToString() + "'");
                        string chk = "chk" + j.ToString() + strWeeks[i - 1];
                        CheckBox chkbox = (CheckBox)tblWeeks.FindControl(chk);//"chk1SUN"
                        if (drS.Length > 0)
                            chkbox.Checked = true;
                        else
                            chkbox.Checked = false;

                    }
                }
                ModalPopupExtenderk.Show();
            }
          catch
            {
                ModalPopupExtenderk.Show();
            }
          //ModalPopupExtender4.Hide ();
          //ModalPopupExtender3.Hide();
        }
        public void btn_insert(object sender, EventArgs e)
        {
            try
            {
                //--date 
                string dates = Label11.Text.Trim();
                dates = dates.Replace("<br />", "");
                int chk = 0;
                if (chkweekoff.Checked == true)
                    chk = 1;
                int chkph = 0;
                if (chkPH.Checked == true)
                    chkph = 1;
                int empid = Convert.ToInt32(txtEmpID.Text);
                  AttendanceDAC objEmployee = new AttendanceDAC();
                objEmployee.Insert_Attend_Function(empid, txtFromDay.Text.Trim(), txtToDay.Text.Trim (), dates, chk, chkph);
                AlertMsg.MsgBox(Page, "Attendance is Inserted Forcly!");
                txtFromDay.Text = "";
                txtToDay.Text = "";
                chkweekoff.Checked = false;
                chkPH.Checked = false;
            }
            catch
            {
                AlertMsg.MsgBox(Page, "Please Make all required parameter Valid eg EmpID, FromDate, ToDate!");
            }
        }
        public void btn_Leavecredits(object sender, EventArgs e)
        {
            try
            {
                AttendanceDAC objEmployee = new AttendanceDAC();
                int calenderYr = Convert.ToInt32(ddlYear.SelectedValue);
               objEmployee.JOBS_HMS_LeaveCredits(calenderYr, CODEUtility.ConvertToDate(txtassesmentstrt_date.Text.Trim(), DateFormat.DayMonthYear));
                AlertMsg.MsgBox(Page, "Job executed successfully!!");
            }
            catch
            {
                AlertMsg.MsgBox(Page, "Please Make all required parameter Valid eg Calender Year, Start date!");
            }
        }
        public void btn_TimeSheetJOb(object sender, EventArgs e)
        {
            try
            {
                AttendanceDAC objEmployee = new AttendanceDAC();
                int calenderYr = Convert.ToInt32(ddlYear.SelectedValue);
                objEmployee.JOBS_HMS_Timesheetstatus(calenderYr);
                AlertMsg.MsgBox(Page, "Job executed successfully!!");

            }
            catch
            {
                AlertMsg.MsgBox(Page, "Please Make all required parameter Valid eg Calender Year!");
            }
        }
        public void BindYears()
        {
            try
            {
                 
                DataSet ds = AttendanceDAC.HR_GetCalenderYears();
                if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ddlYear.DataValueField = "AssYearId";
                    ddlYear.DataTextField = "AssessmentYear";
                    ddlYear.DataSource = ds;
                    ddlYear.DataBind();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public void BindYears(DropDownList ddl)
        {
            DataSet ds = AttendanceDAC.GetCalenderYear();

            int i = 0;
            int Maxyear = Convert.ToInt32(ds.Tables[0].Rows[0]["MaxYear"].ToString());
            for (int Minyear = Convert.ToInt32(ds.Tables[0].Rows[0]["MinYear"].ToString()); Minyear <= Maxyear; Minyear++)
            {
                ddl.Items.Insert(i, new ListItem(Convert.ToString(Minyear), Convert.ToString(Minyear)));
                i = i + 1;
            }

            #region set defalult month and year
            //if we are changed to new year
            if (ds.Tables[0].Rows[0]["PreviousMonth"].ToString() == "0")
            {
                ddlMonth.SelectedValue = "12";

                int CurrentYear = Convert.ToInt32(ds.Tables[0].Rows[0]["CurrentYear"]);
                int PreviousYear = CurrentYear - 1;
                ddl.Items.FindByValue(PreviousYear.ToString()).Selected = true;

            }
            //if we are in same year and same month
            else
            {
                ddlMonth.SelectedValue = ds.Tables[0].Rows[0]["PreviousMonth"].ToString();
                if (ddl.Items.FindByValue(ds.Tables[0].Rows[0]["CurrentYear"].ToString()) != null)
                {
                    ddl.Items.FindByValue(ds.Tables[0].Rows[0]["CurrentYear"].ToString()).Selected = true;
                }
                else
                {
                    ddl.SelectedIndex = ddlYear.Items.Count - 1;
                }
            }
            #endregion set defalult month and year
        }
    
        public void BindWorkSite()
        {

            DataSet ds = AttendanceDAC.GetWorkSite(0, '1', Convert.ToInt32(Session["CompanyID"]));
            ddlworksites.DataSource = ds.Tables[0];
            ddlworksites.DataTextField = "Site_Name";
            ddlworksites.DataValueField = "Site_ID";
            ddlworksites.DataBind();
            ddlworksites.Items.Insert(0, new ListItem("---ALL---", "0", true));
            ds = null;
            // objEmployee = null;
        }

        public void BindDepartments()
        {
             
            AttendanceDAC objEmployee = new AttendanceDAC();
            DataSet ds = objEmployee.GetDepartments(0);
            ddldepartments.DataSource = ds.Tables[0];
            ddldepartments.DataTextField = "DeptName";
            //ddldepartments.DataTextField = "DepartmentName";
            ddldepartments.DataValueField = "DepartmentUId";
            ddldepartments.DataBind();
            ddldepartments.Items.Insert(0, new ListItem("---ALL---", "0", true));
            ds = null;
            objEmployee = null;
        }

        public void BindLeaveTypes()
        {
            DataSet ds = Leaves.GetTypeofLeavesList();
            ddlSumLeaveType.DataSource = ds;
            ddlSumLeaveType.DataTextField = "Name";
            ddlSumLeaveType.DataValueField = "LeaveType";
            ddlSumLeaveType.DataBind();
            ddlSumLeaveType.Items.Insert(0, new ListItem("--Select--", "0"));
        }

       
       

        public void EmployeBind()
        {

            try
            {

                HRCommon objHrCommon = new HRCommon();
                objHrCommon.PageSize = 10;
                objHrCommon.CurrentPage = 1;
                int? SiteID = null;
                int? DeptID = null;
                int SiteID1 = 0;
                int DeptID1 = 0;
                if (ddlworksites.SelectedItem.Value != "0")
                {
                    SiteID = Convert.ToInt32(ddlworksites.SelectedItem.Value);
                    SiteID1 = Convert.ToInt32(ddlworksites.SelectedItem.Value);
                }
                if (ddldepartments.SelectedItem.Value != "0")
                {
                    DeptID = Convert.ToInt32(ddldepartments.SelectedItem.Value);
                    DeptID1 = Convert.ToInt32(ddldepartments.SelectedItem.Value);
                }
                objHrCommon.OldEmpID = null;
                if (txtOldEmpID.Text != "")
                    objHrCommon.OldEmpID = txtOldEmpID.Text;

                int? EmpID = null;
                if (txtEmpID.Text != "")
                    EmpID = Convert.ToInt32(txtEmpID.Text);
                //DataSet ds = new DataSet();
                //ds = ExceReports.ExceRptReportingto(objHrCommon, SiteID, DeptID, txtusername.Text);
                DataSet ds = ExceReports.ExceRptReportingtoByPaging(objHrCommon, SiteID, DeptID, txtusername.Text, EmpID, Convert.ToInt32(Session["CompanyID"]));
                if (ds.Tables[0].Rows.Count == 0)
                {
                    AlertMsg.MsgBox(Page, "Employee not found with selected Search Criterea!");
                    return;
                    lblEMPName.Text = "";
                    hfID.Value = "";
                    hfSiteID.Value = "";
                    Labelyr.Text = "";
                    lblDateofJoin.Text = "";
                    lblAvailable.Text = "";
                    LabelEL.Text = "";
                    LabelSL.Text = ""; lblEmpnature.Text = "";
                    HDEmpnatureID.Value = "0";
                }
                HDEmpnatureID.Value = ds.Tables[0].Rows[0]["EempNatureID"].ToString();
                //-------------Check Confi for Leave & vacations
                DataSet dsCheckConfig = AttendanceDAC.HMS_checkLeaveCOnfig(Convert.ToInt32(HDEmpnatureID.Value),Convert.ToInt32 (ddlYear.SelectedItem.Text));
                if (dsCheckConfig.Tables[0].Rows.Count>0)
                {
                    if (dsCheckConfig.Tables[0].Rows[0][0].ToString ()=="0")
                       chkWeekOffs.Checked = false;
                    else
                        chkWeekOffs.Checked = true;
                    if (dsCheckConfig.Tables[0].Rows[0][1].ToString() == "0")
                        chkholidaysConfig.Checked = false;
                    else
                        chkholidaysConfig.Checked = true ;

                    if (dsCheckConfig.Tables[0].Rows[0][2].ToString() == "0")
                    ChkLeaveEntitlement.Checked = false ;
                    else
                     ChkLeaveEntitlement.Checked = true ;
                }
                //--------------END of CHECK
                lblEmpnature.Text = ds.Tables[0].Rows[0]["Empnature"].ToString();
               lblEMPName.Text = ds.Tables[0].Rows[0]["name"].ToString();
                hfID.Value = ds.Tables[0].Rows[0]["EmpId"].ToString();
                hfSiteID.Value = ds.Tables[0].Rows[0]["Site_ID"].ToString();
                DataSet ds1 = AttendanceDAC.GetAvalableLeaves(EmpID);
                DataSet dsL = Leaves.GetApplicableLeavesDetailsByPaging(objHrCommon, SiteID1, DeptID1, EmpID, txtusername.Text.Trim(), Convert.ToInt32(Session["CompanyID"]), Convert.ToInt32(ddlYear.SelectedValue));
                Labelyr.Text = ddlYear.SelectedItem.Text;
                lblDateofJoin.Text = ds1.Tables[0].Rows[0]["DateOfJoin"].ToString();
                lblAvailable.Text = (dsL.Tables[0].Rows[0]["CL"]).ToString();
                LabelEL.Text = dsL.Tables[0].Rows[0]["EL"].ToString();
                LabelSL.Text = dsL.Tables[0].Rows[0]["SL"].ToString();
                ds = null;
                objHrCommon = null;
            }
            catch (Exception e)
            {
                //throw e;
            }
        }

        public string DocNavigateUrl()
        {
            string Date = ddlMonth.SelectedItem.Value + "/" + "1" + "/" + ddlyaer1.SelectedItem.Value;
            string EmpId = hfID.Value;
            string ReturnVal = "";
            ReturnVal = String.Format("PaySlipPreview.aspx?id={0}&Date={1}", EmpId, Date);
            return ReturnVal;

        }
        public string DocNavigateUrlforPayslip()
        {
            string Date = ddlMonth.SelectedItem.Value + "/" + "1" + "/" + ddlyaer1.SelectedItem.Value;
            string EmpId = hfID.Value;
            string ReturnVal = "";
            ReturnVal = String.Format("PaySlipPreview.aspx?id={0}&Date={1}", EmpId, Date);
            return ReturnVal;
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            EmployeBind();
            btnMockCreate_Click(sender, e);
        }
       protected void lnkatt_Viewk_click(object sender, EventArgs e)
        {
            try
            {
                AttendanceDAC att = new AttendanceDAC();
                string Date = ddlMonth.SelectedItem.Value + "/" + "1" + "/" + ddlyaer1.SelectedItem.Value;
                int EmpID = Convert.ToInt32(hfID.Value);
            
                lblmonth.Text = ddlMonth.SelectedItem.Text.ToString().Substring(0, 3) + "-" + ddlyaer1.SelectedItem.Text;
                tblAtt.Rows.Clear();

                DataSet ds = objAtt.GetAttendanceByMonth_Cursor(Convert.ToInt32(ddlMonth.SelectedValue), Convert.ToInt32(ddlyaer1.SelectedValue), Convert.ToInt32(ddldepartments.SelectedValue), Convert.ToInt32(ddlworksites.SelectedValue), EmpID, txtusername.Text.Trim(), null);
                int count = ds.Tables.Count;
                Double Present = 0; int Scope = 0; int Absent = 0; int OD = 0; int CL = 0; int EL = 0; int SL = 0; int LA = 0;
                double OBCL, OBEL, OBSL, CCL, CEL, CSL, AL;
                OBCL = OBEL = OBSL = CCL = CEL = CSL = AL = 0;
                DateTime dt = Convert.ToDateTime(ddlMonth.SelectedValue + "/1/" + ddlyaer1.SelectedItem.Value);
                DateTime dtEnd = dt.AddMonths(1);
                
                TableRow tblRow = new TableRow();
                TableCell tcName = new TableCell();
                TableCell tcEmpID = new TableCell();
                TableCell tcDate = new TableCell();
                for (int i = 1; dt != dtEnd; i++)
                {
                    tcDate = new TableCell();
                    tcDate.Text = i.ToString();
                    tcDate.Style.Add("font-weight", "bold");
                    tcDate.Width = 60;
                    tblRow.Cells.Add(tcDate);
                    dt = dt.AddDays(1);
                }
                dt = Convert.ToDateTime(ddlMonth.SelectedValue + "/1/" + ddlyaer1.SelectedItem.Value);
                tblAtt.Rows.Add(tblRow);

                System.Collections.Generic.List<DateTime> listHolidays = new System.Collections.Generic.List<DateTime>();
                foreach (DataRow dr in ds.Tables[ds.Tables.Count - 1].Rows)
                {
                    listHolidays.Add(Convert.ToDateTime(dr["Date"]));
                }
                 tblRow = new TableRow();
                for (int j = 0; j < count - 1; j++)
                {


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

                                if (ds.Tables[j].Rows[k][2].ToString() != "")
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

                            }

                         

                        }

                        tcDate.Width = 60;
                        tblRow.Cells.Add(tcDate);
                        dt = dt.AddDays(1);
                        tblAtt.Rows.Add(tblRow);
                    }
                }
                //For Applied Holidays rules if any on employee id and fro selected month/year
                DataSet ds_HoliDayRules = AttendanceDAC.HR_GetHolidayNonPayRules(EmpID, CODEUtility.ConvertToDate(Date, DateFormat.DayMonthYear));
                if (ds_HoliDayRules.Tables[0].Rows.Count > 0)
                {
                    pnlNonHoliday.Visible = true;
                    pnlNonHoliday1.Visible = true;
                    pnlNonHoliday3.Visible = true ;
                    pnlNonHoliday4.Visible = true;
                    pnlNonHoliday2.Visible = true;
                    grd_nonPayRules.DataSource = ds_HoliDayRules;
                    grd_nonPayRules.DataBind();
                    lblpayDH.Text  = (Convert.ToInt32(lblpayD.Text) - ds_HoliDayRules.Tables[0].Rows.Count).ToString();
                    lblnonpayDH.Text = (Convert.ToInt32( lblNonD .Text ) + ds_HoliDayRules.Tables[0].Rows.Count).ToString();
                }
                else
                {
                    pnlNonHoliday.Visible = false;
                    pnlNonHoliday1.Visible =false ;
                    pnlNonHoliday2.Visible = false ;
                    pnlNonHoliday3.Visible = false;
                    pnlNonHoliday4.Visible = false;
                }
                ModalPopupExtender2.Show();
            }
           catch (Exception Ex)
            {
                ModalPopupExtender2.Show();
            }
        }
        protected void btnMockCreate_Click(object sender, EventArgs e)
        {
            try
            {
              
                DateTime AppliedOn = DateTime.Today;
              
                AttendanceDAC att = new AttendanceDAC();
                string Date = ddlMonth.SelectedItem.Value + "/" + "1" + "/" + ddlyaer1.SelectedItem.Value;
                int EmpID = Convert.ToInt32(hfID.Value);
              
                AttendanceDAC.HR_SavePaySLIP(EmpID, Date,0,string.Empty);
                BindDetails();
                int mont = Convert.ToInt32(ddlMonth.SelectedItem.Value);
                int yr = Convert.ToInt32(ddlyaer1.SelectedItem.Value);
                DataSet dsATtD = AttendanceDAC.HMS_countEMPatt_Status(EmpID, mont, yr);
                grdattendnacestatus.DataSource = dsATtD;
                grdattendnacestatus.DataBind();
                yr = 0; int payD = 0; int nonPay = 0;
                for (int i = 0; i < dsATtD.Tables[0].Rows.Count; i++)
                {
                    yr = yr + Convert.ToInt32(dsATtD.Tables[0].Rows[i][1]);
                    lblNonD.Text = dsATtD.Tables[0].Rows[i][2].ToString().Substring(19, 3);
                    if (lblNonD.Text != "red")
                        payD = payD + Convert.ToInt32(dsATtD.Tables[0].Rows[i][1]);
                    else
                        nonPay = nonPay + Convert.ToInt32(dsATtD.Tables[0].Rows[i][1]);
                }

                lblcountatt.Text = yr.ToString();
                lblpayD.Text = payD.ToString(); lblNonD.Text = nonPay.ToString();

              

            }
            catch (Exception ex)
            {
                alert_MockMsg("Calcultaion failed!");

            }


        }

        void alert_MockMsg(string msg)
        {
            string s = "javascript:return alert(" + msg + ");";
            ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
        }
        protected void btnPayslip_Click(object sender, EventArgs e)
        {
            string url = DocNavigateUrlforPayslip();// "Popup.aspx";
            string s = "window.open('" + url + "', 'popup_window', 'width=1200,height=800,left=100,top=100,resizable=yes');";
            ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
        }

       

        public void BindDetails()
        {
            int EmpID = Convert.ToInt32(hfID.Value);
            DateTime date = Convert.ToDateTime(ddlMonth.SelectedItem.Value + "/" + "1" + "/" + ddlyaer1.SelectedItem.Value);
          DataSet  ds = PayRollMgr.GetPaySLIP(EmpID, date);
            grdWages.DataSource = ds.Tables[1];
            grdWages.DataBind();

            grdAllowances.DataSource = ds.Tables[2];
            grdAllowances.DataBind();

            grdCoyContrybutions.DataSource = ds.Tables[3];
            grdCoyContrybutions.DataBind();

            grdDuductSatatutory.DataSource = ds.Tables[4];
            grdDuductSatatutory.DataBind();

            grdITExmention.DataSource = ds.Tables[6];
            grdITExmention.DataBind();

            grdITSavings.DataSource = ds.Tables[7];
            grdITSavings.DataBind();

            gvTDS.DataSource = ds.Tables[8];
            gvTDS.DataBind();

            grdNonCTC.DataSource = ds.Tables[11];
            grdNonCTC.DataBind();


            if (ds != null && ds.Tables.Count > 0 && ds.Tables[5].Rows.Count > 0)
            {
                lblEmpID.Text = ds.Tables[5].Rows[0]["EmpID"].ToString();
                lblName.Text = ds.Tables[5].Rows[0]["name"].ToString();
                lblDept.Text = ds.Tables[5].Rows[0]["DepartmentName"].ToString();
                lbldesig.Text = ds.Tables[5].Rows[0]["Design"].ToString();
                lblDOJ.Text = ds.Tables[5].Rows[0]["Dateofjoin"].ToString();
                lblbankACno.Text = ds.Tables[5].Rows[0]["BankACNo"].ToString();
                lblPancardNo.Text = ds.Tables[5].Rows[0]["PanACNo"].ToString();
                lblPFACNO.Text = ds.Tables[5].Rows[0]["PFACNo"].ToString();
                lblmonthslip.Text = ds.Tables[5].Rows[0]["PayMonth"].ToString() + "-" + ds.Tables[5].Rows[0]["PayYear"].ToString(); ;
                lblNODW.Text = ds.Tables[5].Rows[0]["WorkingDays"].ToString();
            }
            lblNoofDays.Text = ds.Tables[9].Rows[0]["PresentDays"].ToString();
            lblCompanyName.Text = Company;
            lblSalary.Text = ds.Tables[9].Rows[0]["MonthlySal"].ToString();

            //Salry before IT

            lblGross.Text = ds.Tables[9].Rows[0]["Gross"].ToString();
            lblDeductions.Text = ds.Tables[9].Rows[0]["Deductions"].ToString();
            lblSalbfIT.Text = ds.Tables[9].Rows[0]["SalaryBeforeIT"].ToString();
            lblITExemption.Text = ds.Tables[9].Rows[0]["TotalAllowances"].ToString();
            lblTDS.Text = ds.Tables[9].Rows[0]["TDS"].ToString();
            lblTakeHome.Text = ds.Tables[9].Rows[0]["NetAmount"].ToString();
            lblLRecovery.Text = ds.Tables[9].Rows[0]["AdvanceRecovery"].ToString();
            lblMobile.Text = ds.Tables[9].Rows[0]["MobileExp"].ToString();
            lblEducess.Text = ds.Tables[9].Rows[0]["EduCess"].ToString();
            lblMess.Text = ds.Tables[9].Rows[0]["MessValue"].ToString();
            //Last Hike Details
            lblDoLI.Text = ds.Tables[10].Rows[0]["FromDate"].ToString();
            // lblNonCTC.Text = ds.Tables[9].Rows[0]["NonCTCCompVal"].ToString();

        }
         
        private static string Company = ConfigurationSettings.AppSettings["Company"].ToString();
        private static string Address = ConfigurationSettings.AppSettings["CompanyAddress"].ToString();

        //Grid Total For  wages
        decimal TotalWages = 0;

        protected string GetWages()
        {
            return TotalWages.ToString("N2");
        }

        protected string GetAmtWages(decimal Price)
        {
            string amt = string.Empty;
            try { Price = Convert.ToDecimal(Price.ToString("N2")); }
            catch { Price = 0; }
            TotalWages += Price;
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
        //Grid Total For Allowances
        decimal TotalAllowances;
        protected string GetAllowances()
        {
            return TotalAllowances.ToString("N2");
        }

        protected string GetAmtAllowances(decimal Price)
        {
            string amt = string.Empty;
            Price = Convert.ToDecimal(Price.ToString("N2"));
            TotalAllowances += Price;
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
        //Grid Total For Contrybutions
        decimal TotalContrybutions;
        protected string GetCoyContrybutions()
        {
            return TotalContrybutions.ToString("N2");
        }

        protected string GetAmtCoyContrybutions(decimal Price)
        {
            string amt = string.Empty;
            Price = Convert.ToDecimal(Price.ToString("N2"));
            TotalContrybutions += Price;
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
        //Grid Total For Satatutory
        decimal TotalSatatutory;
        protected string GetDuductSatatutory()
        {
            return TotalSatatutory.ToString("N2");
        }

        protected string GetAmtDuductSatatutory(decimal Price)
        {
            string amt = string.Empty;
            Price = Convert.ToDecimal(Price.ToString("N2"));
            TotalSatatutory += Price;
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
        //Grid Total For  ITExemptions
        decimal ITExemptions;
        protected string GetExemptions()
        {
            return ITExemptions.ToString("N2");
        }
        protected string GetAmtExemptions(decimal Price)
        {
            string amt = string.Empty;
            Price = Convert.ToDecimal(Price.ToString("N2"));
            ITExemptions += Price;
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

        //IT Savings

        decimal ITSavings;
        protected string GetSavings()
        {
            return ITSavings.ToString("N2");
        }
        protected string GetAmtSavings(decimal Price)
        {
            string amt = string.Empty;
            Price = Convert.ToDecimal(Price.ToString("N2"));
            ITSavings += Price;
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

        //TDS 

        decimal ITTDS;
        protected string GetTDS()
        {
            return ITTDS.ToString("N2");
        }
        protected string GetAmtTDS(decimal Price)
        {
            string amt = string.Empty;
            Price = Convert.ToDecimal(Price.ToString("N2"));
            ITTDS += Price;
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




        //TDS 


        decimal NonCTC;
        protected string GetNonCTC()
        {
            return NonCTC.ToString("N2");
        }
        protected string GetAmtNonCTC(decimal Price)
        {
            string amt = string.Empty;
            Price = Convert.ToDecimal(Price.ToString("N2"));
            NonCTC += Price;
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

        #region MockLeaveCreationMethod
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                btnSearch_Click(sender, e);
                int EmpID = Convert.ToInt32(txtEmpID.Text.Trim());
               ViewState["LID"] = 0;
            int ReqLeaves = Convert.ToInt32(txtReqLeaves.Text);
            ViewState["ReqLeaves"] = ReqLeaves;
             DataSet  ds1 = AttendanceDAC.GetAvlLeavesCount(EmpID, Convert.ToInt32(ddlSumLeaveType.SelectedItem.Value));
            if (ds1.Tables.Count > 0)
            {
              //  gvAvailLeaves.DataSource = ds1;
                int tot = 0;
                if (ds1.Tables[0].Rows.Count > 0)
                {
                    tot = Convert.ToInt32(ds1.Tables[0].Rows[0]["LeaveCount"]);
                }
                ViewState["TotalLeaves"] = tot;
                if (tot == 0)
                {
                    AlertMsg.MsgBox(this.Page, "You Have No Leaves! even though u want leaves, these leaves go to LOP ");
                    txtFromData_Leav.Enabled = false;
                    txtToData_Leave.Enabled = false;
                    btnleaveSubmit.Visible = false;
                }
                if (ReqLeaves > tot)
                {
                    AlertMsg.MsgBox(this.Page, "You Have No Sufficient Leaves! If u want all required leaves, the extra leaves go to LOP ");
                    txtFromData_Leav.Enabled = false;
                    txtToData_Leave.Enabled = false;
                    btnleaveSubmit.Visible = false;
                }
                else
                {
                    AlertMsg.MsgBox(this.Page, "You Have Sufficient Leaves to Apply!");
                    txtFromData_Leav.Enabled = true;
                    txtToData_Leave.Enabled = true;
                    btnleaveSubmit.Visible = true;
                }
            }
        }
             catch
            {
                AlertMsg.MsgBox(this.Page, "enter employee id and click search! ");
                txtFromData_Leav.Enabled = false;
                txtToData_Leave.Enabled = false;
                btnleaveSubmit.Visible = false;
            }

       }

        string SendUC;
        int LID;
        protected void btnApply_Click(object sender, EventArgs e)
        {
            try
            {
                int EmpID;
                if (Convert.ToInt32(ViewState["LID"]) == 0)
                {
                    DateTime LeaveFrom = CODEUtility.ConvertToDate( txtFromData_Leav.Text.Trim (), DateFormat.DayMonthYear);
                    DateTime LeaveUntil = CODEUtility.ConvertToDate( txtToData_Leave.Text.Trim (), DateFormat.DayMonthYear);

                    if (LeaveFrom <= LeaveUntil)
                    {
                        EmpID = Convert.ToInt32(txtEmpID.Text.Trim ());
                        //TimeSpan span = LeaveUntil.Subtract(LeaveFrom);
                        //int totdays = span.Days + 1;
                        int appdays = Convert.ToInt32(txtReqLeaves.Text);
                        int totdays = AttendanceDAC.LeavesDaysCount(EmpID, LeaveFrom, LeaveUntil);


                        if (totdays > appdays)
                        {
                            AlertMsg.MsgBox(this.Page, "Check No.Of Leaves you have Entered!");
                        }
                        else
                        {
                            LeaveFrom = CODEUtility.ConvertToDate(txtFromData_Leav.Text.Trim(), DateFormat.DayMonthYear);
                            LeaveUntil = CODEUtility.ConvertToDate(txtToData_Leave.Text.Trim(), DateFormat.DayMonthYear);
                           DateTime AppliedOn = CODEUtility.ConvertToDate( DateTime.Now.ToString("dd/MM/yyyy"), DateFormat.DayMonthYear);
                            objHrCommon.LID = Convert.ToInt32(ViewState["LID"]);
                            int OutPut = AttendanceDAC.HR_InsUpLeaveApplication(objHrCommon,EmpID , AppliedOn, LeaveFrom, LeaveUntil, "Urgency[By Mock leaves system]", Convert.ToInt32(ViewState["Status"]), Convert.ToInt32(ddlSumLeaveType.SelectedItem.Value),"","");
                            LID = objHrCommon.LID;
                            if (OutPut == 1)
                            {
                                if (LID > 0)
                                {
                                    //EmpID =  Convert.ToInt32(Session["UserId"]);
                                    DataSet Sentds = AttendanceDAC.HR_SMS_LeaveApplicatonSent(EmpID);
                                    if (Sentds != null && Sentds.Tables.Count > 0 && Sentds.Tables[0].Rows.Count > 0)
                                    {
                                        
                                    }

                                }
                                //Leave Approval Directly
                                AttendanceDAC.HR_InsUpHrPermission(LID, 2, txtComment.Text.ToString(), LeaveFrom, LeaveUntil,  Convert.ToInt32(Session["UserId"]), AppliedOn);
                                AlertMsg.MsgBox(Page, "Done Direct Leave Approval!");
                            }
                            else if (OutPut == 2)
                                AlertMsg.MsgBox(Page, "Aleready applied in between these days");
                            else
                                AlertMsg.MsgBox(Page, "Updated.!");
                        
                        }

                    }

                    else
                    {
                        AlertMsg.MsgBox(Page, "Check Dates");
                    }
                }
               
            }
            catch (Exception ex)
            {
                AlertMsg.MsgBox(Page, "Enter employee id and Click Search!");
            }

        }

        #endregion

    }
   
}