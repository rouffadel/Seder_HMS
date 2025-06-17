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

namespace AECLOGIC.ERP.HMS
{
    public partial class EmpSalReport : AECLOGIC.ERP.COMMON.WebFormMaster
    {
        AttendanceDAC objEmployee = new AttendanceDAC();
        AttendanceDAC objRights = new AttendanceDAC();
        HRCommon objHrCommon = new HRCommon();
           
        static int SearchCompanyID;
        static int WSiteid;
        static int EDeptid=0;
        int mid = 0;
        bool viewall;
        string menuname;
        string menuid;
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


        protected void Page_Load(object sender, EventArgs e)
        {

           
            if (!IsPostBack)
            {
                BindFinYear();
                EmpListPaging.Visible = false;
                tblInfo.Visible = false;
                gvPaySlip.Visible = false;
                imgEmp.Visible = false;
                BindEmpList();
                BindWorkSites();
                ddlworksites.SelectedValue = "1";
                BindDepartments();
                BindYears();
               
            }
        }
        public void BindFinYear()
        {
            DataSet ds = PayRollMgr.GetFinacialYearList();
            ddlFinyear.DataSource = ds;
            ddlFinyear.DataValueField = "FinYearId";
            ddlFinyear.DataTextField = "Name";
            ddlFinyear.DataBind();
            ddlFinyear.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        public void BindEmpList()
        {
               
            string EmpName = string.Empty;
            int WorkSite = Convert.ToInt32(0);// Default HO
            int Dept = Convert.ToInt32(null);
            //ds = AttendanceDAC.HR_SearchReimburseEmp();
            DataSet ds = AttendanceDAC.HR_SearchEmpBySiteDept(WorkSite, Dept, "y", Convert.ToInt32(Session["CompanyID"]));
            ddlEmp.DataSource = ds.Tables[0];
            ddlEmp.DataTextField = "name";
            ddlEmp.DataValueField = "EmpID";
            ddlEmp.DataBind();
            lblCount.Text = ds.Tables[0].Rows.Count.ToString() + " Employees Found!"; ;
            ddlEmp.Items.Insert(0, new ListItem("---All---", "0", true));
        }
        public string DocNavigateUrl(string EmpId, string Month, string Year)
        {
            string Date = Month.ToString() + "/" + "1" + "/" + Year.ToString();
            string ReturnVal = "";
            ReturnVal = String.Format("PaySlipPreview.aspx?id={0}&Date={1}", EmpId, Date);
            return ReturnVal;
        }
        public decimal TotalAdvance = 0;
        public string GetTotAdvance()
        {
            return TotalAdvance.ToString("N2");
        }
        public string GetAdvance(string EmpId, string Month, string Year)
        {
            decimal Advance = 0;
            DataSet ds = AttendanceDAC.HR_GetEmpAdvanceByMonth(Convert.ToInt32(EmpId), Convert.ToInt32(Month), Convert.ToInt32(Year));
            if (ds.Tables.Count > 0)
            {
                Advance = Convert.ToDecimal(ds.Tables[0].Rows[0][0]);
                TotalAdvance += Convert.ToDecimal(Advance);
            }
            else
            {
                Advance = 0;
            }
            return Advance.ToString("N2");
        }
        decimal TotalMobileExp = 0;
        public string GetTotMobileExp()
        {
            return TotalMobileExp.ToString("N2");
        }
        public string GetMobile(string EmpId, string Month, string Year)
        {
            decimal MobileBill = 0;
            DataSet ds = AttendanceDAC.HR_GetEmpMobileExpByMonth(Convert.ToInt32(EmpId), Convert.ToInt32(Month), Convert.ToInt32(Year));
            if (ds.Tables.Count > 0)
            {
                MobileBill = Convert.ToDecimal(ds.Tables[0].Rows[0][0].ToString());
                TotalMobileExp += MobileBill;
            }
            else
            {
                MobileBill = 0;
            }
            return MobileBill.ToString("N2");
        }
        protected string GetMonth(object Status)
        {
            string retValue = "";
            string input = Status.ToString();
            if (input == "1")
            {
                retValue = "January";
            }
            if (input == "2")
            {
                retValue = "February";
            }
            if (input == "3")
            {
                retValue = "March";
            }
            if (input == "4")
            {
                retValue = "April";
            }
            if (input == "5")
            {
                retValue = "May";
            }
            if (input == "6")
            {
                retValue = "June";
            }
            if (input == "7")
            {
                retValue = "July";
            }
            if (input == "8")
            {
                retValue = "August";
            }
            if (input == "9")
            {
                retValue = "September";
            }
            if (input == "10")
            {
                retValue = "October";
            }
            if (input == "11")
            {
                retValue = "November";
            }
            if (input == "12")
            {
                retValue = "December";
            }
            return retValue;
        }

      
        public void BindEmp(int EmpID, int Month, int Year)
        {
               
            AttendanceDAC ADAC = new AttendanceDAC();
            DataSet ds = ADAC.HR_EmpSalriesListByEmpID(EmpID, Month, Year);

            gvPaySlip.DataSource = ds;
            gvPaySlip.DataBind(); try { gvPaySlip.FooterRow.Visible = false; }
            catch { }
        }
        public void BindYears()
        {
            DataSet ds = AttendanceDAC.GetCalenderYear();

            int i = 0;
            int Maxyear = Convert.ToInt32(ds.Tables[0].Rows[0]["MaxYear"].ToString());
            for (int Minyear = Convert.ToInt32(ds.Tables[0].Rows[0]["MinYear"].ToString()); Minyear <= Maxyear; Minyear++)
            {
                ddlYear.Items.Insert(i, new ListItem(Convert.ToString(Minyear), Convert.ToString(Minyear)));
                i = i + 1;
            }
            // ddlMonth.SelectedValue = ds.Tables[0].Rows[0]["PreviousMonth"].ToString();
            ddlYear.SelectedValue = ds.Tables[0].Rows[0]["CurrentYear"].ToString();

        }

        void EmployeBind(HRCommon objHrCommon)
        {
            try
            {
                objHrCommon.PageSize = EmpListPaging.ShowRows;
                objHrCommon.CurrentPage = EmpListPaging.CurrentPage;
                objHrCommon.SiteID = Convert.ToInt32(ddlworksites.SelectedItem.Value);
                objHrCommon.DeptID = Convert.ToInt32(ddldepartments.SelectedItem.Value);
                // objHrCommon.Month = Convert.ToInt32(ddlMonth.SelectedItem.Value);
                objHrCommon.Year = Convert.ToInt32(ddlYear.SelectedItem.Value);
                   
                int EmpID = 0;
                if (txtEmpID.Text != "")
                {
                    EmpID = Convert.ToInt32(txtEmpID.Text);
                }
                else
                {
                    EmpID = Convert.ToInt32(ddlEmp.SelectedValue);
                }

                int Month = Convert.ToInt32(ddlMonth.SelectedValue);
                int Year = Convert.ToInt32(ddlYear.SelectedValue);
                // for Jan 2016 selection pay slip showing by Gana
                if (Month == 1)
                {
                    Month = 12;
                    Year = Year - 1;
                }
                else if (Month!=0) //by RT for HMS 232 on 15-04-2016
                    Month = Month - 1;
                int FinYearID = Convert.ToInt32(ddlFinyear.SelectedValue);
               
             DataSet   ds = AttendanceDAC.HR_GetEmpReport(EmpID, Month, Year, FinYearID);
                // ViewState["NoOfRecords"] = objHrCommon.NoofRecords;
                if (ds != null && ds.Tables.Count != 0 && ds.Tables[1].Rows.Count > 0)// )
                {
                    lblEmpID.Text = ds.Tables[1].Rows[0]["EmpId"].ToString();
                    lblName.Text = ds.Tables[1].Rows[0]["Name"].ToString();
                    lblDept.Text = ds.Tables[1].Rows[0]["DepartmentName"].ToString();
                    lblDesig.Text = ds.Tables[1].Rows[0]["Designation"].ToString();
                    lblWs.Text = ds.Tables[1].Rows[0]["Site_Name"].ToString();
                    lblDOJ.Text = ds.Tables[1].Rows[0]["DOJ"].ToString();
                    lblDOR.Visible = false;
                    lblRelive.Visible = false;
                    if (ds.Tables[1].Rows[0]["StatusChangeOn"].ToString() != "")
                    {
                        lblDOR.Visible = true;
                        lblRelive.Visible = true;
                        lblRelive.Text = ds.Tables[1].Rows[0]["StatusChangeOn"].ToString();
                    }

                    lblDORejoin.Visible = false;
                    lblRejoin.Visible = false;
                    if (ds.Tables[1].Rows[0]["RejoinOn"].ToString() != "")
                    {
                        lblDORejoin.Visible = true;
                        lblRejoin.Visible = true;
                        lblDORejoin.Text = ds.Tables[1].Rows[0]["RejoinOn"].ToString();
                    }
                    lblQualification.Text = ds.Tables[1].Rows[0]["Qualification"].ToString();
                    imgEmp.Visible = true;
                    try { imgEmp.ImageUrl = "EmpImages/" + lblEmpID.Text + "." + ds.Tables[1].Rows[0]["Image"].ToString(); }
                    catch { AlertMsg.MsgBox(Page, "No Image!"); }
                    lblShowRelive.Text = ds.Tables[1].Rows[0]["Service"].ToString();
                    lblStatus.Text = ds.Tables[1].Rows[0]["Status"].ToString();
                    lblReportType.Text = ds.Tables[1].Rows[0]["ReportType"].ToString();
                    //gvExportExcel.DataSource = ds;
                    //gvExportExcel.DataBind();
                    // lblResultCount.Text = ViewState["NoOfRecords"].ToString() + " Items";

                }
                if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0)
                {
                    gvPaySlip.DataSource = ds.Tables[0];
                    gvPaySlip.DataBind();
                    gvPaySlip.Visible = true;
                }
                else
                {
                    gvPaySlip.DataSource = null;
                    gvPaySlip.DataBind();
                    EmpListPaging.Visible = false;
                    AlertMsg.MsgBox(Page, "No Records Available on Current Year! Change Year!");
                }


                // EmpListPaging.Bind(objHrCommon.CurrentPage, objHrCommon.TotalPages, objHrCommon.NoofRecords, objHrCommon.PageSize);

            }
            catch (Exception e)
            {
                // throw e
                AlertMsg.MsgBox(Page, e.ToString());
            }
        }

        public void BindWorkSites()
        {

            try
            {
                FIllObject.FillDropDown(ref ddlworksites, "HR_GetWorkSite_By_EmpList");

                DataSet ds = AttendanceDAC.GetWorkSite(0, '1', Convert.ToInt32(Session["CompanyID"]));
                ViewState["WorkSites"] = ds;
               

            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public void BindDepartments()
        {
            try
            {

                DataSet ds = (DataSet)objRights.GetDaprtmentList();
                ViewState["Departments"] = ds;
               
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (txtEmpID.Text == "")
            {
                objHrCommon.SiteID = Convert.ToInt32(ddlworksites.SelectedItem.Value);
                objHrCommon.DeptID = Convert.ToDouble(ddldepartments.SelectedItem.Value);
                tblInfo.Visible = true;
                gvPaySlip.Visible = true;
                EmployeBind(objHrCommon);
                EmpListPaging.Visible = false;
                try { gvPaySlip.FooterRow.Visible = true; }
                catch { }

              
            }
            else
            {
                int EmpID = 0;
                try
                {
                    EmpID = Convert.ToInt32(txtEmpID.Text);
                    ddlEmp.SelectedValue = "0";
                }
                catch { AlertMsg.MsgBox(Page, "Check the data you have given..!"); }
                int Year = Convert.ToInt32(ddlYear.SelectedValue);
                tblInfo.Visible = true;
                gvPaySlip.Visible = true;
                EmployeBind(objHrCommon);
                EmpListPaging.Visible = false;
               
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
        //Grid Total For  NetAmount
        decimal TotalAmount = 0;

        protected string GetAmount()
        {
            return TotalAmount.ToString("N2");
        }

        protected string GetNetAmount(string Price, string EmpId, string Month, string Year)
        {
            string amt = string.Empty;
            decimal Advance = 0;
            DataSet ds = AttendanceDAC.HR_GetEmpAdvanceByMonth(Convert.ToInt32(EmpId), Convert.ToInt32(Month), Convert.ToInt32(Year));
            if (ds.Tables.Count > 0)
            {
                Advance = Convert.ToDecimal(ds.Tables[0].Rows[0][0]);
                TotalAdvance += Convert.ToDecimal(Advance);
            }
            else
            {
                Advance = 0;
            }
            decimal MobileBill = 0;
            DataSet dsMob = AttendanceDAC.HR_GetEmpMobileExpByMonth(Convert.ToInt32(EmpId), Convert.ToInt32(Month), Convert.ToInt32(Year));
            if (dsMob.Tables.Count > 0)
            {
                MobileBill = Convert.ToDecimal(dsMob.Tables[0].Rows[0][0].ToString());
                TotalMobileExp += MobileBill;
            }
            else
            {
                MobileBill = 0;
            }

            decimal Amt = Convert.ToDecimal(Price);//.ToString("N2")
            decimal totDed = Advance + MobileBill;
            Amt = Amt - totDed;
            TotalAmount += Amt;
            if (Price != "0")
            {
                amt = Amt.ToString("N2");
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
      
        
        protected void Worksidechangewithdep(object sender, EventArgs e)
        {
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@Search", txtSearchWorksite.Text);
            param[1] = new SqlParameter("@CompanyID", Convert.ToInt32(Session["CompanyID"]));
            FIllObject.FillDropDown(ref ddlworksites, "G_GET_WorkSitebyFilter", param);
            ListItem itmSelected = ddlworksites.Items.FindByText(txtSearchWorksite.Text);
            if (itmSelected != null)
            {
                ddlworksites.SelectedItem.Selected = false;
                itmSelected.Selected = true;
            }
            ddlworksites_SelectedIndexChanged(sender, e);
            txtSearchdept.Text = txtSearchEmp.Text = "";
        }
        protected void GetDepartmentSearch(object sender, EventArgs e)
        {
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@Search", txtSearchdept.Text);
            param[1] = new SqlParameter("@CompanyID", SearchCompanyID);
            param[2] = new SqlParameter("@SiteID", ddlworksites.SelectedValue);
            FIllObject.FillDropDown(ref ddldepartments, "HMS_googlesearch_GetDepartmentBySite", param);
            ListItem itmSelected = ddldepartments.Items.FindByText(txtSearchdept.Text);
            if (itmSelected != null)
            {
                ddldepartments.SelectedItem.Selected = false;
                itmSelected.Selected = true;
            }
            if (txtSearchdept.Text != "") { ddldepartments.SelectedIndex = 1; }
            ddldepartments_SelectedIndexChanged(sender, e);
            EDeptid = Convert.ToInt32(ddldepartments.SelectedItem.Value);
            txtSearchEmp.Text = "";
        }
        protected void BtnExportGrid_Click(object sender, EventArgs args)
        {
            objHrCommon.PageSize = Convert.ToInt32(ViewState["NoOfRecords"]);
            objHrCommon.CurrentPage = 1;
            objHrCommon.SiteID = Convert.ToInt32(ddlworksites.SelectedItem.Value);
            objHrCommon.DeptID = Convert.ToInt32(ddldepartments.SelectedItem.Value);
            
            objHrCommon.Year = Convert.ToInt32(ddlYear.SelectedItem.Value);


            SqlDataReader dr = objEmployee.GetEmployeesForSalaries(objHrCommon, Convert.ToInt32(ViewState["NoOfRecords"]));
            ExportFileUtil.ExportToExcel(dr, "", "#EFEFEF", "#E6e6e6", "PaySlip");
        }
        protected void GetEmployeeSearch(object sender, EventArgs e)
        {
            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@Search", txtSearchEmp.Text);
            param[1] = new SqlParameter("@CompanyID", SearchCompanyID);
            param[2] = new SqlParameter("@WS", ddlworksites.SelectedValue);
            param[3] = new SqlParameter("@Status","Y");
            param[4] = new SqlParameter("@Dept", ddldepartments.SelectedValue);
            FIllObject.FillDropDown(ref ddlEmp, "HR_GoogleSerac_SearchEmpBySiteDept", param);


            ListItem itmSelected = ddlEmp.Items.FindByText(txtSearchEmp.Text);
            if (itmSelected != null)
            {
                ddlEmp.SelectedItem.Selected = false;
                itmSelected.Selected = true;
            }
            if (txtSearchEmp.Text != "") { ddlEmp.SelectedIndex = 0; }

        }
        public string DocNavigateUrl(string EmpId)
        {
            string ReturnVal = "";
            return ReturnVal;
        }
      
        protected void btnSync_Click(object sender, EventArgs e)
        {
            int EmpID = 0;
            if (txtEmpID.Text != "")
            {
                try
                {
                    EmpID = Convert.ToInt32(txtEmpID.Text);
                  
                    btnSearch_Click(sender, e);
                }
                catch { AlertMsg.MsgBox(Page, "InValid Data!"); }

            }
            else
            {
                AlertMsg.MsgBox(Page, "Enter EmpID and Click Sync!");
            }

        }
        protected void ddlworksites_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindDeparmetBySite(Convert.ToInt32(ddlworksites.SelectedValue));
            WSiteid = Convert.ToInt32(ddlworksites.SelectedValue);

               
            string EmpName = string.Empty;
            int WorkSite = Convert.ToInt32(ddlworksites.SelectedValue);
            int Dept = Convert.ToInt32(ddldepartments.SelectedValue);
            string Status = ddlStatus.SelectedItem.Value;
          DataSet  ds = AttendanceDAC.HR_SearchEmpBySiteDept(WorkSite, Dept, Status, Convert.ToInt32(Session["CompanyID"]));
            ddlEmp.DataSource = ds.Tables[0];
            ddlEmp.DataTextField = "name";
            ddlEmp.DataValueField = "EmpID";
            ddlEmp.DataBind();
            lblCount.Text = ds.Tables[0].Rows.Count.ToString() + " Employees Found!"; ;
            ddlEmp.Items.Insert(0, new ListItem("---SELECT---", "0", true));
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
        protected void ddldepartments_SelectedIndexChanged(object sender, EventArgs e)
        {
               
            string EmpName = string.Empty;
            int WorkSite = Convert.ToInt32(ddlworksites.SelectedValue);
            int Dept = Convert.ToInt32(ddldepartments.SelectedValue);
            string Status = ddlStatus.SelectedItem.Value;
            DataSet ds = AttendanceDAC.HR_SearchEmpBySiteDept(WorkSite, Dept, Status, Convert.ToInt32(Session["CompanyID"]));
            ddlEmp.DataSource = ds.Tables[0];
            ddlEmp.DataTextField = "name";
            ddlEmp.DataValueField = "EmpID";
            ddlEmp.DataBind();
            lblCount.Text = ds.Tables[0].Rows.Count.ToString() + " Employees Found!"; ;
            ddlEmp.Items.Insert(0, new ListItem("---SELECT---", "0", true));
        }
        protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
               
            string EmpName = string.Empty;
            int WorkSite = Convert.ToInt32(ddlworksites.SelectedValue);
            int Dept = Convert.ToInt32(ddldepartments.SelectedValue);
            string Status = ddlStatus.SelectedItem.Value;
            DataSet ds = AttendanceDAC.HR_SearchEmpBySiteDept(WorkSite, Dept, Status, Convert.ToInt32(Session["CompanyID"]));
            ddlEmp.DataSource = ds.Tables[0];
            ddlEmp.DataTextField = "name";
            ddlEmp.DataValueField = "EmpID";
            ddlEmp.DataBind();
            lblCount.Text = ds.Tables[0].Rows.Count.ToString() + " Employees Found!"; ;
            ddlEmp.Items.Insert(0, new ListItem("---SELECT---", "0", true));
        }
        protected void ddlEmp_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtEmpID.Text = "";
            EmpListPaging.Visible = false;
            tblInfo.Visible = true;
            gvPaySlip.Visible = true;
            EmployeBind(objHrCommon);

         
        }

        //Added by Rijwan:22-03-2016
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute]
        public static string[] GetCompletionList(string prefixText, int count, string contextKey)
        {
            DataSet ds = AttendanceDAC.GetGoogleABCSearchWorkSite(prefixText, SearchCompanyID);
            return ConvertStingArray(ds);
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute]
        public static string[] GetCompletionListdept(string prefixText, int count, string contextKey)
        {
            DataSet ds = AttendanceDAC.GetDepartmentGoogleSerc(prefixText,SearchCompanyID,WSiteid);
            return ConvertStingArray(ds);
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute]
        public static string[] GetCompletionListEmp(string prefixText, int count, string contextKey)
        {
            DataSet ds = AttendanceDAC.HR_GoogleSearchEmpBySiteDept(prefixText, WSiteid, EDeptid,"Y",SearchCompanyID);
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
        protected void ddlFinyear_SelectedIndexChanged(object sender, EventArgs e)
        {
            // ddlYear.SelectedValue = "0";
            if (ddlEmp.SelectedValue != "0" || txtEmpID.Text != "")
            {
                EmployeBind(objHrCommon);
            }
            else
            {
                AlertMsg.MsgBox(Page, "Select Employee!");
            }

        }
        protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlFinyear.SelectedValue = "0";
            if (ddlEmp.SelectedValue != "0" || txtEmpID.Text != "")
            {
                EmployeBind(objHrCommon);
            }
            else
            {
                AlertMsg.MsgBox(Page, "Select Employee!");
            }
        }
    }
}
