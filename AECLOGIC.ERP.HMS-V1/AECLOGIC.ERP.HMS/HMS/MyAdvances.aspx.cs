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
using AECLOGIC.ERP.HMS.HRClasses;

namespace AECLOGIC.ERP.HMSV1
{
    public partial class MyAdvancesV1 : AECLOGIC.ERP.COMMON.WebFormMaster
    {
        static int SearchCompanyID = 1;
        static int Empdeptid = 0;
        static int WSId = 0;
        static int DeptId = 0;
        static char Staus = '1';
        static string EmpName = string.Empty;
        string menuid;
        bool Editable;
        static int SiteSearch = 0;
        HRCommon objHrCommon = new HRCommon();
        static int CompanyID = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["CompanyID"].ToString());
        AttendanceDAC objatt = new AttendanceDAC();
        protected override void OnInit(EventArgs e)
        {
            ModuleID = 1;
            base.OnInit(e);
            // btnSave.Attributes.Add("onclick", "javascript:return ValidateSave('" + txtCategoryName.ClientID + "');");

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
            int rbStatus = Convert.ToInt32(rbAdvanceView.SelectedValue);
            if (Request.QueryString.Count > 0)
            {
                int id = Convert.ToInt32(Request.QueryString["key"].ToString());

                if (id == 2) { BindPager(rbStatus, 2, 2); }
                if (id == 3) { BindPager(rbStatus, 2, 1); }
                if (id == 4) { BindPager(rbStatus, 3, 0); }
            }
            else
            {
                BindPager(rbStatus, 1, 0);//UnRecoverd Advances
            }

        }
        void EmpListPaging_FirstClick(object sender, EventArgs e)
        {
            int rbStatus = Convert.ToInt32(rbAdvanceView.SelectedValue);
            if (Request.QueryString.Count > 0)
            {
                int id = Convert.ToInt32(Request.QueryString["key"].ToString());
                if (id == 2) { BindPager(rbStatus, 2, 2); }
                if (id == 3) { BindPager(rbStatus, 2, 1); }
                if (id == 4) { BindPager(rbStatus, 3, 0); }
            }
            else
            {
                BindPager(rbStatus, 1, 0);//UnRecoverd Advances
            }
        }
        void BindPager(int Status, int LoanType, int InstallmentType)
        {
            objHrCommon.PageSize = EmpListPaging.ShowRows;
            objHrCommon.CurrentPage = EmpListPaging.CurrentPage;
            BindGrid(objHrCommon, Status, LoanType, InstallmentType);
        }
        protected void Page_Load(object sender, EventArgs e)
        {


            if (!IsPostBack)
            {
                Page.Form.Attributes.Add("enctype", "multipart/form-data");
                GetParentMenuId();

                try
                {

                    ViewState["WSID"] = 0;
                    if (Convert.ToInt32(Session["RoleId"].ToString()) == 7)
                    {
                        try
                        {

                            //string Worksit = Session["Site"].ToString();
                            DataSet ds = clViewCPRoles.HR_DailyAttStatus(Convert.ToInt32(Session["UserId"]));
                            ViewState["WSID"] = ds.Tables[0].Rows[0]["ID"].ToString();
                            txtSearchWorksite.Text = ds.Tables[0].Rows[0]["Name"].ToString();
                            txtSearchWorksite.ReadOnly = true;

                        }
                        catch { }
                    }
                }
                catch { }

                if (Request.QueryString.Count > 0)
                {
                    pnlAll.Visible = true;
                    int id = Convert.ToInt32(Request.QueryString["key"].ToString());
                    if (id == 2)// Reducing Loans
                    {
                        tblLoanView.Visible = true;
                        BindPager(1, 2, 2);

                    }

                    if (id == 3)//FlatLoans
                    {
                        txtLPaidOn.Text = DateTime.Now.ToString(ConfigurationManager.AppSettings["DateFormat"]);
                        tblLoanView.Visible = true;
                        BindPager(1, 2, 1);
                    }
                    if (id == 4)//TravelAdvance
                    {
                        txtTAPaidOn.Text = DateTime.Now.ToString(ConfigurationManager.AppSettings["DateFormat"]);

                        tblLoanView.Visible = true;
                        BindPager(1, 3, 0);
                    }

                    if (id == 1)
                    {
                        ViewState["LoanID"] = 0;
                        //  BindEmpList();

                        tblAdvance.Visible = false;
                        pnltblAdvance.Visible = false;
                        tblLoan.Visible = false;
                        pnltblLoan.Visible = false;
                        Refresh();
                        tblMain.Visible = false;
                        pnltblMain.Visible = false;
                        BindYears();
                        GetEmpList(sender, e);
                        //ddlAdjType.Items.FindByText("Advance").Selected = true;
                        ListItem itmSelected = ddlAdjType.Items.FindByText("Advance");

                        if (itmSelected != null)
                        {
                            ddlAdjType.SelectedItem.Selected = false;
                            itmSelected.Selected = true;
                            //FillProjects();
                        }

                        ddlAdjType_SelectedIndexChanged(sender, e);

                    }



                }
                else
                {
                    txtAPaidOn.Text = DateTime.Now.ToString(ConfigurationManager.AppSettings["DateFormat"]);
                    tblLoanView.Visible = true;
                    BindPager(1, 1, 0);
                }

            }
        }
        public int GetParentMenuId()
        {
            string URL = Request.Url.Segments[Request.Url.Segments.Length - 1] + Request.Url.Query;
            int RoleId = Convert.ToInt32(Session["RoleId"].ToString());
            int ModuleId = ModuleID; ;

            DataSet ds = AttendanceDAC.GetAllowed(RoleId, ModuleId, URL);
            int MenuId = 0;
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                MenuId = Convert.ToInt32(ds.Tables[0].Rows[0]["Under"]);
                Editable = (bool)ds.Tables[0].Rows[0]["Editable"];
                btnAdvSave.Enabled = btnLSave.Enabled = btnReduceSave.Enabled = Editable;
            }
            return MenuId;
        }


        protected void GetWork(object sender, EventArgs e)
        {

            CompanyID = Convert.ToInt32(Session["CompanyID"]);
            WSId = Convert.ToInt32(ddlWs_hid.Value == "" ? "0" : ddlWs_hid.Value); ;

        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionEmpList(string prefixText, int count, string contextKey)
        {

            DataSet ds = AttendanceDAC.HR_FilterSearchAll_googlesearch(prefixText, EmpName);
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

        protected void GetEmpList(object sender, EventArgs e)
        {

            int EmpID = Convert.ToInt32(Convert.ToInt32(Session["UserId"]).ToString());
            ddlEmp_hid.Value = EmpID.ToString();
            DataSet ds = AttendanceDAC.HR_GetEmpSal(EmpID);
            if (ds.Tables[0].Rows.Count > 0)
                txtSal.Text = Convert.ToInt32(Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString()) / 12).ToString();

        }



        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionList(string prefixText, int count, string contextKey)
        {
            // WSId = 0;
            DataSet ds = AttendanceDAC.GetWorkSite_By_googlesearch_Emp(prefixText, WSId);
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
        public static string[] GetCompletionListDep(string prefixText, int count, string contextKey)
        {
            DataSet ds = AttendanceDAC.BindDeparmetBySite_googlesearch(prefixText, WSId, CompanyID);
            DataTable dt = ds.Tables[0];
            List<string> items = new List<string>(count);
            var rtval = new Dictionary<string, string>();
            foreach (DataRow row in dt.Rows)
            {
                string str = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(row["DepartmentName"].ToString(), row["DepartmentUId"].ToString());//DepartmentName
                items.Add(str);
            }

            return items.ToArray(); ;// txtItems.ToArray();
        }



        public static string[] ConvertStingArray(DataSet ds)
        {
            string[] rtval = Array.ConvertAll(ds.Tables[0].Select(), new Converter<DataRow, string>(DataRowToDouble));
            return rtval;
        }
        public static string DataRowToDouble(DataRow dr)
        {
            return dr["Name"].ToString();
        }


        public void BindYears()
        {

            DataSet ds = AttendanceDAC.GetCalenderYear();

            int i = 0;
            int Maxyear = Convert.ToInt32(ds.Tables[0].Rows[0]["MaxYear"].ToString());
            for (int Minyear = Convert.ToInt32(ds.Tables[0].Rows[0]["MinYear"].ToString()); Minyear <= Maxyear; Minyear++)
            {
                if (Minyear >= Convert.ToInt32(ds.Tables[0].Rows[0]["CurrentYear"].ToString()))
                {
                    ddlYear.Items.Insert(i, new ListItem(Convert.ToString(Minyear), Convert.ToString(Minyear)));
                    ddlCurrentYear.Items.Insert(i, new ListItem(Convert.ToString(Minyear), Convert.ToString(Minyear)));
                    ddlReducingYear.Items.Insert(i, new ListItem(Convert.ToString(Minyear), Convert.ToString(Minyear)));
                    i = i + 1;
                }

            }
            ddlMonth.SelectedValue = DateTime.Now.Month.ToString();
            ViewState["PreviousMonth"] = ds.Tables[0].Rows[0]["PreviousMonth"].ToString();
            ddlYear.SelectedValue = DateTime.Now.Year.ToString();
            ViewState["CurrentYear"] = ds.Tables[0].Rows[0]["CurrentYear"].ToString();


            ddlCurrentMonth.SelectedValue = ds.Tables[0].Rows[0]["PreviousMonth"].ToString();
            ddlCurrentYear.SelectedValue = ds.Tables[0].Rows[0]["CurrentYear"].ToString();//ddlYear.SelectedValue;
        }

        public void Refresh()
        {
            txtAdvAmt.Text = txtAdvGSal.Text = txtAdvInstAmt.Text = txtAdvNM.Text = "";
            txtFilter.Text = txtLAmt.Text = txtLGrossSal.Text = txtLIAmt.Text = txtLIns.Text = "";
            txtLRecoveryMonths.Text = txtLRI.Text = txtSal.Text = "";
            ddlAdjType.SelectedIndex = 0;
            //  ddlEmp.SelectedIndex = 0;
            TxtEmpy.Text = String.Empty;
        }

        protected void ddlAdjType_SelectedIndexChanged(object sender, EventArgs e)
        {
            tblASave.Visible = true;
            if (Convert.ToInt32(ddlAdjType.SelectedValue) == 1)//Advance
            {
                tblAdvance.Visible = true;
                pnltblAdvance.Visible = true;
                tblLoan.Visible = false;
                pnltblLoan.Visible = false;
                tblReduce.Visible = false;
                pnltblReduce.Visible = false;
                tblLsave.Visible = false;
                tblReduceSave.Visible = false;
            }
            else
            {
                try
                {
                    DataSet ds = AttendanceDAC.HR_GetLoanOptions();
                    txtLRI.Text = ds.Tables[0].Rows[0][0].ToString();
                    txtReduceRI.Text = ds.Tables[0].Rows[0][2].ToString();
                    txtST.Text = ds.Tables[0].Rows[0][1].ToString();
                    txtReduceST.Text = ds.Tables[0].Rows[0][1].ToString();
                }
                catch { AlertMsg.MsgBox(Page, "Options Not set yet!"); }
            }
            if (Convert.ToInt32(ddlAdjType.SelectedValue) == 2)//Loan
            {

                tblAdvance.Visible = false;
                pnltblAdvance.Visible = false;
                tblLoan.Visible = true;
                pnltblLoan.Visible = true;

                tblASave.Visible = false;
                ddlLoanRecoveryType.SelectedIndex = 0;
                tblReduce.Visible = false;
                pnltblReduce.Visible = false;
            }
            if (Convert.ToInt32(ddlAdjType.SelectedValue) == 3)//Travel Advance
            {
                tblAdvance.Visible = true;
                pnltblAdvance.Visible = true;
                tblLoan.Visible = false;
                pnltblLoan.Visible = false;
                ddlRecoverType.SelectedIndex = 1;
                tblReduce.Visible = false;
                pnltblReduce.Visible = false;
            }
        }


        protected void txtAdvNM_TextChanged(object sender, EventArgs e)
        {
            if (txtAdvNM.Text != "")
            {
                try
                {
                    txtAdvInstAmt.Text = Math.Round(Convert.ToDouble((txtAdvAmt.Text)) / Convert.ToInt32((txtAdvNM.Text)), 2).ToString();
                    // txtAdvGSal.Text = Convert.ToDouble(Convert.ToDouble(txtSal.Text) - Convert.ToDouble(txtAdvInstAmt.Text)).ToString();
                    try
                    {
                        int IMEIs = Convert.ToInt32(txtAdvNM.Text);
                        if (IMEIs == 1)
                        {
                            ddlRecoverType.SelectedValue = "1";
                        }
                        else
                        {
                            ddlRecoverType.SelectedValue = "2";
                        }
                    }
                    catch { }
                }
                catch
                {
                    AlertMsg.MsgBox(Page, "Enter Values Properly..!");
                }
            }
        }
        protected string FormatInput(object Status)
        {
            string retValue = "";
            string input = Status.ToString();
            if (input == "1")
            {
                retValue = "Advance";
            }
            if (input == "2")
            {
                retValue = "Loan";
            }
            if (input == "3")
            {
                retValue = "Travel-Advances";
            }
            return retValue;
        }

        protected string Format(object Status)
        {
            string retValue = "";
            string input = Status.ToString();
            if (input == "1")
            {
                retValue = "Instant";
            }

            if (input == "2")
            {
                retValue = "EMIs";
            }

            return retValue;
        }
        protected string Formatip(object Status)
        {
            string retValue = "";
            string input = Status.ToString();
            if (input == "0")
            {
                retValue = "NotPaid";
            }
            if (input == "1")
            {
                retValue = "Paid";
            }
            if (input == "False")
            {
                retValue = "NotPaid";
            }
            if (input == "True")
            {
                retValue = "Paid";
            }
            return retValue;
        }

        protected string ViewDetails(string LoanID)
        {
            return "javascript:return window.open('ViewAdvanceDetails.aspx?Id=" + LoanID + "', '_blank')";
        }
        protected string FormatMonth(object Status)
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


        protected void txtLRI_TextChanged(object sender, EventArgs e)
        {
            if (txtLRI.Text != "")
            {
                Double ST = Convert.ToDouble(txtST.Text);
                Double amt = Convert.ToDouble(txtLAmt.Text) / 100;
                double MI = Convert.ToDouble(amt * (Convert.ToDouble(txtLRI.Text) / 10));
                Double STAmt = MI + (MI * (ST / 1000));
                txtLIns.Text = STAmt.ToString();
                double installAmt = Convert.ToDouble(Convert.ToDouble(txtLAmt.Text) / Convert.ToDouble(txtLRecoveryMonths.Text));
                Double LIamt = Convert.ToDouble(installAmt + Convert.ToDouble(txtLIns.Text));
                txtLIAmt.Text = LIamt.ToString();
                txtLGrossSal.Text = Convert.ToDouble(Convert.ToDouble(txtSal.Text) - Convert.ToDouble(txtLIAmt.Text)).ToString();
            }
        }

        protected void btnAdvCancel_Click(object sender, EventArgs e)
        {
            Refresh();
        }

        protected void btnLCancel_Click(object sender, EventArgs e)
        {
            Refresh();
        }

        protected bool ValidatePayment(int EmpID, int Month, int Year)
        {
            try
            {
                DataSet ds = AttendanceDAC.HR_ValidatePayment(@EmpID, @Month, @Year);
                if (ds.Tables.Count > 0 && Convert.ToInt32(ds.Tables[0].Rows[0][0]) > 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        public void InsertAdvance(int Month, int Year)
        {
            int Status = 1;
            int EmpID = Convert.ToInt32(Convert.ToInt32(ddlEmp_hid.Value == "" ? "0" : ddlEmp_hid.Value));


            DataSet dsLoanDetail = new DataSet("LoanDataSet");
            DataTable dtTD = new DataTable("LoanTable");
            dtTD.Columns.Add(new DataColumn("LoanID", typeof(System.Int32)));
            dtTD.Columns.Add(new DataColumn("InstallmentNo", typeof(System.Int32)));
            dtTD.Columns.Add(new DataColumn("EMIAmount", typeof(System.Double)));
            dtTD.Columns.Add(new DataColumn("EmpID", typeof(System.Int32)));
            dtTD.Columns.Add(new DataColumn("Status", typeof(System.Int32)));
            dtTD.Columns.Add(new DataColumn("Intrest", typeof(System.Double)));
            dtTD.Columns.Add(new DataColumn("ServiceTax", typeof(System.Double)));
            dtTD.Columns.Add(new DataColumn("PrincipalAmt", typeof(System.Double)));
            dsLoanDetail.Tables.Add(dtTD);
            int LoanID;
            int TransID; //Payment TransID

            if (Convert.ToInt32(ViewState["LoanID"]) == 0)
            {
                LoanID = 0;
                TransID = 0;
            }
            else
            {
                LoanID = Convert.ToInt32(ViewState["LoanID"]);

                DataSet ds = AttendanceDAC.HR_getTransID(LoanID);
                TransID = Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());
            }
            EmpID = Convert.ToInt32(Convert.ToInt32(ddlEmp_hid.Value == "" ? "0" : ddlEmp_hid.Value));
            // EmpID = Convert.ToInt32(ddlEmp.SelectedValue);
            double LoanAmount = Convert.ToDouble(txtAdvAmt.Text);
            int LoanType = Convert.ToInt32(ddlAdjType.Text);
            DateTime LoanIssuedDate = DateTime.Now;
            int RecoverYear = Year;
            int RecoverMonth = Month;
            int NoofEMIs = Convert.ToInt32(txtAdvNM.Text);
            double InstallmentAmt = Convert.ToDouble(txtAdvInstAmt.Text);
            double InterestRate = 0;
            tblLoan.Visible = false;
            pnltblLoan.Visible = false;
            tblAdvance.Visible = false;
            pnltblAdvance.Visible = false;
            tblMain.Visible = false;
            pnltblMain.Visible = false;


            double ServiceTax = 0;
            string Remarks = ddlAdjType.SelectedItem.Text;
            double CreditAmt = Convert.ToDouble(txtAdvAmt.Text);
            int InstallmentType = 0;

            int RecoveryType = Convert.ToInt32(ddlRecoverType.SelectedValue);
            int IssuedBy = Convert.ToInt32(Session["UserId"]);
            DateTime TransTime = CODEUtility.ConvertToDate(txtAPaidOn.Text.Trim(), DateFormat.DayMonthYear);
            string LoanRemarks = txtremarks.Text.Trim();
            AttendanceDAC ADAC = new AttendanceDAC();
            TransID = 0;
            string filename = "", ext = string.Empty, path = "";
            if (fuUploadProof.HasFile)
            {
                filename = fuUploadProof.PostedFile.FileName;
                ext = filename.Split('.')[filename.Split('.').Length - 1];
            }

            int LoanIDNo;

            if (LoanID == 0)
            {
                LoanIDNo = Convert.ToInt32(ADAC.HR_InsUpLoansReturnLoanID_AmntReqOn(LoanID, EmpID, LoanAmount, LoanType, LoanIssuedDate, RecoverYear, RecoverMonth, NoofEMIs, InstallmentAmt, InterestRate, TransID, RecoveryType, IssuedBy, ServiceTax, InstallmentType, TransTime.Date, LoanRemarks, ext));
            }
            else
            {
                LoanIDNo = Convert.ToInt32(ADAC.HR_InsUpLoansReturnLoanID(LoanID, EmpID, LoanAmount, LoanType, LoanIssuedDate, RecoverYear, RecoverMonth, NoofEMIs, InstallmentAmt, InterestRate, TransID, RecoveryType, IssuedBy, ServiceTax, InstallmentType, LoanRemarks, ext));
            }

            if (fuUploadProof.HasFile)
            {

                path = Server.MapPath("~\\hms\\EmpLoan\\" + LoanIDNo + "." + ext);
                fuUploadProof.PostedFile.SaveAs(path);
            }
            int RowCount = 0;
            double Principle = LoanAmount;
            for (int i = 0; i < NoofEMIs; i++)
            {
                Double PerMonth = LoanAmount / NoofEMIs;
                int amt = Convert.ToInt32(Principle) / 100;

                double EMI = PerMonth;// +MI + ST;
                Principle = Principle - PerMonth;
                RowCount = RowCount + 1;
                DataRow drTD = dtTD.NewRow();
                drTD["LoanID"] = LoanIDNo;
                drTD["InstallmentNo"] = RowCount;
                drTD["EMIAmount"] = EMI;
                drTD["EmpID"] = EmpID;
                drTD["Status"] = 0;
                drTD["Intrest"] = 0;
                drTD["ServiceTax"] = 0;
                drTD["PrincipalAmt"] = PerMonth;
                dtTD.Rows.Add(drTD);
            }
            dsLoanDetail.AcceptChanges();
            DataSet dsTD = AttendanceDAC.HR_InsLoanDetailsXML(dsLoanDetail);

            DataSet dsLaonID = AttendanceDAC.HR_GetLoanDetailsByID(LoanIDNo, Convert.ToInt32(Session["UserId"]), ConfigurationManager.AppSettings["CompanyID"]);
            tblASave.Visible = false;
            tblLoanView.Visible = true;

            int Emp = 0;
            try
            {

                if (txtEmpID.Text != "")
                {
                    Emp = Convert.ToInt32(txtEmpID.Text);
                }
            }
            catch { Emp = 0; }


            int SMonth;
            SMonth = Convert.ToInt32(ddlMonth.SelectedItem.Value);
            if (Month != SMonth)
            {
                AlertMsg.MsgBox(Page, "Selected month salary already paid,Process Done for next month of the selected month LoanID#" + LoanIDNo.ToString() + " TransactionID#" + TransID.ToString());

            }
            else
            {
                AlertMsg.MsgBox(Page, "Process Done LoanID#" + LoanIDNo.ToString());
            }

        }
        protected bool ValidateSave()
        {
            DateTime TransTime = CODEUtility.ConvertToDate(txtAPaidOn.Text.Trim(), DateFormat.DayMonthYear);
            int EmpID = Convert.ToInt32(Convert.ToInt32(ddlEmp_hid.Value == "" ? "0" : ddlEmp_hid.Value));
            SqlParameter[] parms1 = new SqlParameter[2];
            parms1[0] = new SqlParameter("@empid", EmpID);
            parms1[1] = new SqlParameter("@FinalDate", TransTime);
            DataSet ds = SQLDBUtil.ExecuteDataset("HMS_GetLoanValidations", parms1);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                int service = 0, LoanExists = 0;
                double GAmt = 0.00;
                double LoanAmount = Convert.ToDouble(txtAdvAmt.Text);
                service = Convert.ToInt32(ds.Tables[0].Rows[0][0]);
                LoanExists = Convert.ToInt32(ds.Tables[0].Rows[0][1]);
                GAmt = Convert.ToInt32(ds.Tables[0].Rows[0][2]);
                if (service < 12)
                {
                    AlertMsg.MsgBox(Page, "Employee should be at least one year working in the Company.", AlertMsg.MessageType.Warning);
                    return false;
                }
                else if (LoanExists == 1)
                {
                    AlertMsg.MsgBox(Page, "Employee has already taken Advance in past one year.", AlertMsg.MessageType.Warning);
                    return false;
                }
                if (LoanAmount > GAmt)
                {
                    AlertMsg.MsgBox(Page, "Advance amount should not be more than End Of Service Benifits :-" + GAmt.ToString(), AlertMsg.MessageType.Warning);
                    return false;
                }
            }
            return true;
        }


        protected void btnAdvSave_Click(object sender, EventArgs e)
        {
            try
            {

                int EmpID = Convert.ToInt32(Convert.ToInt32(Session["UserId"]).ToString());
                if (EmpID > 0)
                {
                    if (ValidateSave())
                    {
                        int Month = Convert.ToInt32(ddlMonth.SelectedItem.Value);
                        int Year = Convert.ToInt32(ddlYear.SelectedItem.Value);

                        DataSet ds = objatt.HR_EmpSalriesListByEmpID(EmpID, Month, Year);
                        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                        {
                            string PaymentStatus = ds.Tables[0].Rows[0]["paymentStatus"].ToString();
                            if (PaymentStatus == "Paid")
                            {
                                if (Month != 12)
                                {
                                    InsertAdvance(Month + 1, Year);
                                }
                                else
                                {
                                    InsertAdvance(Month + 1, Year + 1);
                                }
                            }
                            else
                            {
                                InsertAdvance(Month, Year);
                            }
                        }
                        else
                        {

                            InsertAdvance(Month, Year);
                        }
                    }
                }
                else
                {
                    AlertMsg.MsgBox(Page, "Select Employee!");
                }
            }
            catch (Exception ex)
            {
                AlertMsg.MsgBox(Page, ex.Message.ToString(), AlertMsg.MessageType.Error);
            }


        }

        public void Loan(int Month, int Year)
        {
            int Status = 1;
            int EmpID = Convert.ToInt32(Convert.ToInt32(ddlEmp_hid.Value == "" ? "0" : ddlEmp_hid.Value));


            DataSet dsLoanDetail = new DataSet("LoanDataSet");
            DataTable dtTD = new DataTable("LoanTable");
            dtTD.Columns.Add(new DataColumn("LoanID", typeof(System.Int32)));
            dtTD.Columns.Add(new DataColumn("InstallmentNo", typeof(System.Int32)));
            dtTD.Columns.Add(new DataColumn("EMIAmount", typeof(System.Double)));
            dtTD.Columns.Add(new DataColumn("EmpID", typeof(System.Int32)));
            dtTD.Columns.Add(new DataColumn("Status", typeof(System.Int32)));
            dtTD.Columns.Add(new DataColumn("Intrest", typeof(System.Double)));
            dtTD.Columns.Add(new DataColumn("ServiceTax", typeof(System.Double)));
            dtTD.Columns.Add(new DataColumn("PrincipalAmt", typeof(System.Double)));
            dsLoanDetail.Tables.Add(dtTD);
            int LoanID;
            int TransID;
            if (Convert.ToInt32(ViewState["LoanID"]) == 0)
            {
                LoanID = 0;
                TransID = 0;
            }
            else
            {
                LoanID = Convert.ToInt32(ViewState["LoanID"]);
                DataSet ds = AttendanceDAC.HR_getTransID(LoanID);
                TransID = Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());
            }
            // EmpID = Convert.ToInt32(ddlEmp.SelectedValue);
            EmpID = Convert.ToInt32(Convert.ToInt32(ddlEmp_hid.Value == "" ? "0" : ddlEmp_hid.Value));
            double LoanAmount = Convert.ToDouble(txtLAmt.Text);
            int LoanType = Convert.ToInt32(ddlAdjType.Text);
            DateTime LoanIssuedDate = DateTime.Now;
            int RecoverYear = Year;// Convert.ToInt32(ddlCurrentYear.SelectedValue);
            int RecoverMonth = Month;// Convert.ToInt32(ddlCurrentMonth.SelectedValue);
            int NoofEMIs = Convert.ToInt32(txtLRecoveryMonths.Text);
            double InstallmentAmt = Convert.ToDouble(txtLIAmt.Text);
            double InterestRate = Convert.ToDouble(txtLRI.Text);
            double ServiceTax = Convert.ToDouble(txtST.Text);
            tblLsave.Visible = false;
            tblLoan.Visible = false;
            pnltblLoan.Visible = false;
            tblAdvance.Visible = false;
            pnltblAdvance.Visible = false;
            tblMain.Visible = false;
            pnltblMain.Visible = false;
            string Remarks = ddlAdjType.SelectedItem.Text;
            double CreditAmt = Convert.ToDouble(txtLAmt.Text);
            DataSet dsLedID = AttendanceDAC.HR_EmpLedID(EmpID);
            int LedgerId = Convert.ToInt32(dsLedID.Tables[0].Rows[0][0]);

            DataSet VID = AttendanceDAC.HR_VocherID(CompanyID, EmpID);
            int IssuedBy = Convert.ToInt32(Session["UserId"]);
            int RecoveryType = 2;
            DateTime TransTime = CODEUtility.ConvertToDate(txtLPaidOn.Text.Trim(), DateFormat.DayMonthYear);
            int InstallmentType = 1;// Fixed Loan
            AttendanceDAC ADAC = new AttendanceDAC();
            string filename = "", ext = string.Empty, path = "";
            if (fuUploadProof.HasFile)
            {
                filename = fuUploadProof.PostedFile.FileName;
                ext = filename.Split('.')[filename.Split('.').Length - 1];
            }
            TransID = Convert.ToInt32(ADAC.HR_InsUpAdvanceTransaction(TransID, TransTime, Remarks, CompanyID, EmpID, Convert.ToInt32(Session["UserId"]), CreditAmt));//ledgerid for debit transaction needed,vocherid
            string LoanRemarks = "";
            int LoanIDNo = Convert.ToInt32(ADAC.HR_InsUpLoansReturnLoanID(LoanID, EmpID, LoanAmount, LoanType, LoanIssuedDate, RecoverYear, RecoverMonth, NoofEMIs, InstallmentAmt, InterestRate, TransID, RecoveryType, IssuedBy, ServiceTax, InstallmentType, LoanRemarks, ext));
            if (fuUploadProof.HasFile)
            {

                path = Server.MapPath("~\\hms\\EmpLoan\\" + LoanIDNo + "." + ext);
                fuUploadProof.PostedFile.SaveAs(path);
            }
            int RowCount = 0;
            double Principle = LoanAmount;
            for (int i = 0; i < NoofEMIs; i++)
            {
                Double PerMonth = LoanAmount / NoofEMIs;
                int amt = Convert.ToInt32(Principle) / 100;
                double MI = Convert.ToDouble(amt * (Convert.ToDouble(txtLRI.Text) / 10));
                Double ST = MI * (ServiceTax / 1000);
                double EMI = PerMonth + MI + ST;
                Principle = Principle - PerMonth;
                RowCount = RowCount + 1;
                DataRow drTD = dtTD.NewRow();
                drTD["LoanID"] = LoanIDNo;
                drTD["InstallmentNo"] = RowCount;
                drTD["EMIAmount"] = EMI;
                drTD["EmpID"] = EmpID;
                drTD["Status"] = 0;
                drTD["Intrest"] = MI;
                drTD["ServiceTax"] = ST;
                drTD["PrincipalAmt"] = PerMonth;
                dtTD.Rows.Add(drTD);
            }
            dsLoanDetail.AcceptChanges();
            DataSet dsTD = AttendanceDAC.HR_InsLoanDetailsXML(dsLoanDetail);
            tblLoanView.Visible = true;



            int SMonth;
            SMonth = Convert.ToInt32(ddlMonth.SelectedItem.Value);
            if (Month != SMonth)
            {
                AlertMsg.MsgBox(Page, "Selected month salary already paid,Process Done for next month of the selected month LoanID#" + LoanIDNo.ToString() + " TransactionID#" + TransID.ToString());

            }
            else
            {
                AlertMsg.MsgBox(Page, "Process Done LoanID#" + LoanIDNo.ToString() + " TransactionID#" + TransID.ToString());
            }
            //pratap 
            Response.Redirect("MyAdvances.aspx");

        }
        protected void btnLSave_Click(object sender, EventArgs e)
        {
            try
            {

                // int EmpID = Convert.ToInt32(ddlEmp.SelectedValue);
                int EmpID = Convert.ToInt32(Convert.ToInt32(ddlEmp_hid.Value == "" ? "0" : ddlEmp_hid.Value));
                if (EmpID > 0)
                {
                    int Month = Convert.ToInt32(ddlMonth.SelectedValue);
                    int Year = Convert.ToInt32(ddlYear.SelectedValue);

                    DataSet ds = objatt.HR_EmpSalriesListByEmpID(EmpID, Month, Year);
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        string PaymentStatus = ds.Tables[0].Rows[0]["paymentStatus"].ToString();
                        if (PaymentStatus == "Paid")
                        {
                            if (Month != 12)
                            {
                                Loan(Month + 1, Year);
                            }
                            else
                            {
                                Loan(Month + 1, Year + 1);
                            }
                        }
                        else
                        {
                            Loan(Month, Year);
                        }
                    }
                    else
                    {
                        if (Month != 12)
                        {
                            Loan(Month + 1, Year);
                        }
                        else
                        {
                            Loan(Month + 1, Year + 1);
                        }

                    }

                }
                else
                {
                    AlertMsg.MsgBox(Page, "Select Employee!");
                }
            }
            catch (Exception ex1)
            {
                AlertMsg.MsgBox(Page, ex1.Message.ToString(), AlertMsg.MessageType.Error);
            }


        }

        protected void gvView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "View")
            {
                txtLPaidOn.Text = DateTime.Now.ToString(ConfigurationManager.AppSettings["DateFormat"]);
                txtAPaidOn.Text = DateTime.Now.ToString(ConfigurationManager.AppSettings["DateFormat"]);
                txtTAPaidOn.Text = DateTime.Now.ToString(ConfigurationManager.AppSettings["DateFormat"]);
                BindYears();
                int LoanID = Convert.ToInt32(e.CommandArgument);
                ViewState["LoanID"] = LoanID;
                btnFind.Visible = txtFilter.Visible = false;

                DataSet ds = AttendanceDAC.HR_GetLoanDetails(LoanID);

                ddlRecoverType.SelectedValue = Convert.ToInt32(ds.Tables[0].Rows[0]["RecoveryType"]).ToString();
                //  ddlEmp.SelectedValue = Convert.ToInt32(ds.Tables[0].Rows[0]["EmpID"]).ToString();
                txtAdvAmt.Text = ds.Tables[0].Rows[0]["LoanAmount"].ToString();
                ddlAdjType.SelectedValue = Convert.ToInt32(ds.Tables[0].Rows[0]["LoanType"]).ToString();
                ddlCurrentYear.SelectedValue = Convert.ToInt32(ds.Tables[0].Rows[0]["RecoverYear"]).ToString();
                ddlCurrentMonth.SelectedValue = Convert.ToInt32(ds.Tables[0].Rows[0]["RecoverMonth"]).ToString();
                txtLRecoveryMonths.Text = Convert.ToInt32(ds.Tables[0].Rows[0]["NoofEMIs"]).ToString();
                txtLIAmt.Text = Convert.ToInt32(ds.Tables[0].Rows[0]["InstallmentAmt"]).ToString();
                ddlYear.SelectedValue = Convert.ToInt32(ds.Tables[0].Rows[0]["RecoverYear"]).ToString();
                ddlMonth.SelectedValue = Convert.ToInt32(ds.Tables[0].Rows[0]["RecoverMonth"]).ToString();
                txtAdvNM.Text = Convert.ToInt32(ds.Tables[0].Rows[0]["NoofEMIs"]).ToString();
                txtAdvInstAmt.Text = Convert.ToInt32(ds.Tables[0].Rows[0]["InstallmentAmt"]).ToString();
                txtSal.Text = Convert.ToDouble(ds.Tables[0].Rows[0]["Salary"]).ToString();
                txtAdvGSal.Text = Convert.ToDouble(ds.Tables[0].Rows[0]["InstallmentPeriodSalary"]).ToString();
                txtLGrossSal.Text = Convert.ToDouble(ds.Tables[0].Rows[0]["InstallmentPeriodSalary"]).ToString();
                txtLAmt.Text = ds.Tables[0].Rows[0]["LoanAmount"].ToString();

                DataSet dsL = AttendanceDAC.HR_getTransID(LoanID);
                int WS = Convert.ToInt32(dsL.Tables[1].Rows[0][0].ToString());
                tblMain.Visible = true;
                pnltblMain.Visible = true;

                if (Convert.ToInt32(ddlAdjType.SelectedValue) == 1)
                {
                    tblAdvance.Visible = true;
                    pnltblAdvance.Visible = true;
                    tblASave.Visible = true; tblLsave.Visible = false;
                }
                else
                {
                    tblASave.Visible = false; tblLsave.Visible = true;
                    tblLoan.Visible = true;
                    pnltblLoan.Visible = true;
                }

                int PaymentType = Convert.ToInt32(ds.Tables[0].Rows[0]["PaymentType"]);
            }

        }

        protected void btnFind_Click(object sender, EventArgs e)
        {
            //comemted by nadeem due to hidden field
            if (txtFilter.Text == "")
            {


                string EmpName = string.Empty;
                String SearchKey = string.Empty;

                DataSet ds = AttendanceDAC.HR_FilterSearchAll_googlesearch(SearchKey, EmpName);

            }
        }

        protected void ddlLoanRecoveryType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(ddlLoanRecoveryType.SelectedValue) == 2)
            {
                tblReduce.Visible = true;
                pnltblReduce.Visible = true;
                tblMain.Visible = true;
                pnltblMain.Visible = true;

                tblAdvance.Visible = false;
                pnltblAdvance.Visible = false;
                tblLoan.Visible = false;
                pnltblLoan.Visible = false;
                //txtReduceRI.Text = "18.00"; txtReduceST.Text = "10.30";
                tblReduceSave.Visible = true; tblLsave.Visible = false;
            }
            else
            {
                tblReduce.Visible = false;
                pnltblReduce.Visible = false;
                tblLsave.Visible = true;
                tblReduceSave.Visible = false;
            }
        }
        public void TravelAdvance()
        {
            int Status = 1;
            //  int EmpID = Convert.ToInt32(ddlEmp.SelectedValue);
            int EmpID = Convert.ToInt32(Convert.ToInt32(ddlEmp_hid.Value == "" ? "0" : ddlEmp_hid.Value));
            int Month = Convert.ToInt32(ddlRecoveryMonths.SelectedValue);
            int Year = Convert.ToInt32(ddlReducingYear.SelectedValue);
            DataSet dst = AttendanceDAC.HR_ValidatePayment(EmpID, Month, Year);
            if (dst != null && dst.Tables.Count > 0 && dst.Tables[0].Rows.Count > 0)
            {
                Status = Convert.ToInt32(dst.Tables[0].Rows[0]["Status"]);
            }
            if ((Status == 0) || (dst.Tables[0].Rows.Count == 0))
            {
                DataSet dsLoanDetail = new DataSet("LoanDataSet");
                DataTable dtTD = new DataTable("LoanTable");
                dtTD.Columns.Add(new DataColumn("LoanID", typeof(System.Int32)));
                dtTD.Columns.Add(new DataColumn("InstallmentNo", typeof(System.Int32)));
                dtTD.Columns.Add(new DataColumn("EMIAmount", typeof(System.Double)));
                dtTD.Columns.Add(new DataColumn("EmpID", typeof(System.Int32)));
                dtTD.Columns.Add(new DataColumn("Status", typeof(System.Int32)));
                dtTD.Columns.Add(new DataColumn("Intrest", typeof(System.Double)));
                dtTD.Columns.Add(new DataColumn("ServiceTax", typeof(System.Double)));
                dtTD.Columns.Add(new DataColumn("PrincipalAmt", typeof(System.Double)));
                dsLoanDetail.Tables.Add(dtTD);
                int LoanID;
                int TransID;
                if (Convert.ToInt32(ViewState["LoanID"]) == 0)
                {
                    LoanID = 0;
                    TransID = 0;
                }
                else
                {
                    LoanID = Convert.ToInt32(ViewState["LoanID"]);
                    DataSet ds = AttendanceDAC.HR_getTransID(LoanID);
                    TransID = Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());
                }

                EmpID = Convert.ToInt32(Convert.ToInt32(ddlEmp_hid.Value == "" ? "0" : ddlEmp_hid.Value));
                int InstallmentType = 2;// Convert.ToInt32(ddlReduce.SelectedValue);

                int RecoverYear = Convert.ToInt32(ddlReducingYear.SelectedValue);
                int RecoverMonth = Convert.ToInt32(ddlRecoveryMonths.SelectedValue);


                int IssuedBy = Convert.ToInt32(Session["UserId"]);

                int RecoveryType = 2; double CreditAmt = Convert.ToDouble(txtReduceLoanAmount.Text);
                int LoanType = Convert.ToInt32(ddlAdjType.Text); DateTime LoanIssuedDate = DateTime.Now;

                string Remarks = ddlAdjType.SelectedItem.Text; double InstallmentAmt = 0;
                double LoanAmount = Convert.ToDouble(txtReduceLoanAmount.Text);
                int NoofEMIs = Convert.ToInt32(txtReduceEMIs.Text);
                double InterestRate = Convert.ToDouble(txtReduceRI.Text);
                double ServiceTax = Convert.ToDouble(txtReduceST.Text);
                DateTime TransTime = CODEUtility.ConvertToDate(txtTAPaidOn.Text.Trim(), DateFormat.DayMonthYear);
                AttendanceDAC ADAC = new AttendanceDAC();
                TransID = Convert.ToInt32(ADAC.HR_InsUpAdvanceTransaction(TransID, TransTime, Remarks, CompanyID, EmpID, Convert.ToInt32(Session["UserId"]), CreditAmt));//ledgerid for debit transaction needed,vocherid
                string LoanRemarks = "";
                string filename = "", ext = string.Empty, path = "";
                if (fuUploadProof.HasFile)
                {
                    filename = fuUploadProof.PostedFile.FileName;
                    ext = filename.Split('.')[filename.Split('.').Length - 1];
                }

                int LoanIDNo = Convert.ToInt32(ADAC.HR_InsUpLoansReturnLoanID(LoanID, EmpID, LoanAmount, LoanType, LoanIssuedDate, RecoverYear, RecoverMonth, NoofEMIs, InstallmentAmt, InterestRate, TransID, RecoveryType, IssuedBy, ServiceTax, InstallmentType, LoanRemarks, ext));
                if (fuUploadProof.HasFile)
                {

                    path = Server.MapPath("~\\hms\\EmpLoan\\" + LoanIDNo + "." + ext);
                    fuUploadProof.PostedFile.SaveAs(path);
                }
                int RowCount = 0;
                double Principle = LoanAmount;
                for (int i = 0; i < NoofEMIs; i++)
                {
                    Double PerMonth = LoanAmount / NoofEMIs;
                    int amt = Convert.ToInt32(Principle) / 100;
                    double MI = Convert.ToDouble(amt * (Convert.ToDouble(txtReduceRI.Text) / 10));
                    Double ST = MI * (ServiceTax / 1000);
                    double EMI = PerMonth + MI + ST;
                    Principle = Principle - PerMonth;
                    RowCount = RowCount + 1;
                    DataRow drTD = dtTD.NewRow();
                    drTD["LoanID"] = LoanIDNo;
                    drTD["InstallmentNo"] = RowCount;
                    drTD["EMIAmount"] = EMI;
                    drTD["EmpID"] = EmpID;
                    drTD["Status"] = 0;
                    drTD["Intrest"] = MI;
                    drTD["ServiceTax"] = ST;
                    drTD["PrincipalAmt"] = PerMonth;
                    dtTD.Rows.Add(drTD);
                }
                dsLoanDetail.AcceptChanges();
                DataSet dsTD = AttendanceDAC.HR_InsLoanDetailsXML(dsLoanDetail);
                tblMain.Visible = false;
                pnltblMain.Visible = false;

                tblReduce.Visible = false;
                pnltblReduce.Visible = false;
                tblReduceSave.Visible = false;
                BindPager(1, 2, 2);
            }
            else
            {
                AlertMsg.MsgBox(Page, "Selected month salary already paid.");
            }
        }
        protected void btnReduceSave_Click(object sender, EventArgs e)
        {
            try
            {
                // int EmpID = Convert.ToInt32(ddlEmp.SelectedValue);
                int EmpID = Convert.ToInt32(Convert.ToInt32(ddlEmp_hid.Value == "" ? "0" : ddlEmp_hid.Value));
                if (EmpID > 0)
                {
                    int Month = Convert.ToInt32(ddlMonth.SelectedValue);
                    int Year = Convert.ToInt32(ddlYear.SelectedValue);

                    DataSet ds = objatt.HR_EmpSalriesListByEmpID(EmpID, Month, Year);
                    string PaymentStatus = ds.Tables[0].Rows[0]["paymentStatus"].ToString();
                    if (PaymentStatus == "Paid")
                        AlertMsg.MsgBox(Page, "Selected month salary paid..!");
                    else
                        TravelAdvance();

                }
                else
                {
                    AlertMsg.MsgBox(Page, "Select Employee!");
                }
            }
            catch (Exception ex2)
            {
                AlertMsg.MsgBox(Page, ex2.Message.ToString(), AlertMsg.MessageType.Error);
            }

        }

        protected void gvLoanView_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            if (e.CommandName == "Edt")
            {
                try
                {
                    //BindEmpList();
                    int LoanID = Convert.ToInt32(e.CommandArgument);
                    ViewState["LoanID"] = LoanID;
                    BindYears();
                    btnFind.Visible = txtFilter.Visible = false;

                    DataSet ds = AttendanceDAC.HR_GetLoanDetails(LoanID);
                    txtSal.Text = Convert.ToDouble(ds.Tables[0].Rows[0]["Salary"]).ToString();

                    ddlAdjType.SelectedValue = Convert.ToInt32(ds.Tables[0].Rows[0]["LoanType"]).ToString();
                    int InstallmentType = Convert.ToInt32(ds.Tables[0].Rows[0]["InstallmentType"]);

                    tblMain.Visible = true;
                    pnltblMain.Visible = true;
                    pnlAll.Visible = true;

                    tblLoanView.Visible = false; tblLsave.Visible = false; tblReduceSave.Visible = false;
                    tblAdvance.Visible = false;
                    pnltblAdvance.Visible = false;
                    tblReduce.Visible = false;
                    pnltblReduce.Visible = false;
                    tblASave.Visible = false;
                    //pnltblAdvance.Visible=fa
                    tblLoan.Visible = false;
                    pnltblLoan.Visible = false;
                    int TypeofLoan = Convert.ToInt32(ds.Tables[0].Rows[0]["LoanType"].ToString());
                    int InstalmentType = Convert.ToInt32(ds.Tables[0].Rows[0]["InstallmentType"].ToString());
                    if (TypeofLoan == 1)
                    {
                        tblAdvance.Visible = true;
                        pnltblAdvance.Visible = true;
                        tblASave.Visible = true;
                        txtAdvAmt.Text = ds.Tables[0].Rows[0]["LoanAmount"].ToString();
                        txtAdvNM.Text = Convert.ToInt32(ds.Tables[0].Rows[0]["NoofEMIs"]).ToString();
                        ddlRecoverType.SelectedValue = Convert.ToInt32(ds.Tables[0].Rows[0]["RecoveryType"]).ToString();
                        txtAdvInstAmt.Text = Convert.ToInt32(ds.Tables[0].Rows[0]["InstallmentAmt"]).ToString();
                        txtAPaidOn.Text = ds.Tables[0].Rows[0]["IssuedOn"].ToString();
                        ddlYear.SelectedValue = Convert.ToInt32(ds.Tables[0].Rows[0]["RecoverYear"]).ToString();
                        ddlMonth.SelectedValue = Convert.ToInt32(ds.Tables[0].Rows[0]["RecoverMonth"]).ToString();
                        txtAdvGSal.Text = Convert.ToDouble(ds.Tables[0].Rows[0]["InstallmentPeriodSalary"]).ToString();

                    }
                    if (TypeofLoan == 2 && InstalmentType == 1)
                    {
                        tblLoan.Visible = true;
                        pnltblLoan.Visible = true;
                        tblLsave.Visible = true;
                        ddlLoanRecoveryType.SelectedValue = Convert.ToInt32(ds.Tables[0].Rows[0]["InstallmentType"]).ToString();

                        txtLGrossSal.Text = Convert.ToDouble(ds.Tables[0].Rows[0]["InstallmentPeriodSalary"]).ToString();
                        txtLAmt.Text = ds.Tables[0].Rows[0]["LoanAmount"].ToString();
                        txtLRecoveryMonths.Text = Convert.ToInt32(ds.Tables[0].Rows[0]["NoofEMIs"]).ToString();
                        txtLIAmt.Text = Convert.ToInt32(ds.Tables[0].Rows[0]["InstallmentAmt"]).ToString();
                        txtST.Text = ds.Tables[0].Rows[0]["ServiceTax"].ToString();
                        txtLRI.Text = ds.Tables[0].Rows[0]["InterestRate"].ToString();
                        txtLPaidOn.Text = ds.Tables[0].Rows[0]["IssuedOn"].ToString();
                        ddlCurrentYear.SelectedValue = Convert.ToInt32(ds.Tables[0].Rows[0]["RecoverYear"]).ToString();
                        ddlCurrentMonth.SelectedValue = Convert.ToInt32(ds.Tables[0].Rows[0]["RecoverMonth"]).ToString();
                        txtLIns.Text = ds.Tables[0].Rows[0]["InterestRate"].ToString();
                    }
                    if (TypeofLoan == 2 && InstalmentType == 2)
                    {
                        tblReduceSave.Visible = true; tblReduce.Visible = true; pnltblReduce.Visible = true;
                        txtTAPaidOn.Text = ds.Tables[0].Rows[0]["IssuedOn"].ToString();
                        txtReduceEMIs.Text = Convert.ToInt32(ds.Tables[0].Rows[0]["NoofEMIs"]).ToString();
                        txtReduceLoanAmount.Text = ds.Tables[0].Rows[0]["LoanAmount"].ToString();
                        txtReduceRI.Text = ds.Tables[0].Rows[0]["InterestRate"].ToString();
                        txtReduceST.Text = ds.Tables[0].Rows[0]["ServiceTax"].ToString();
                        ddlRecoveryMonths.SelectedValue = Convert.ToInt32(ds.Tables[0].Rows[0]["RecoverMonth"]).ToString();
                    }
                }
                catch { AlertMsg.MsgBox(Page, "Unable to Some values!"); }
            }



        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {

            int DeptNo = Convert.ToInt32(DeptId);
            int SiteID = Convert.ToInt32(WSId);
            int EmpID = 0; string EmpName = "";
            if (txtEmpID.Text != "")
            {
                try
                {
                    EmpID = Convert.ToInt32(txtEmpID.Text);
                }
                catch
                {
                    AlertMsg.MsgBox(Page, "Check The Data you have given!");
                }
            }
            try
            {
                EmpName = txtEmpName.Text;
            }
            catch
            {
                AlertMsg.MsgBox(Page, "Check The Data you have given!");
            }
            int Emp = 0;
            if (txtEmpID.Text != "")
            {
                Emp = Convert.ToInt32(txtEmpID.Text);
            }
            int rbStatus = Convert.ToInt32(rbAdvanceView.SelectedValue);
            if (Request.QueryString.Count > 0)
            {
                int id = Convert.ToInt32(Request.QueryString["key"].ToString());

                if (id == 2) { BindPager(rbStatus, 2, 2); }
                if (id == 3) { BindPager(rbStatus, 2, 1); }
                if (id == 4) { BindPager(rbStatus, 3, 0); }
            }
            else
            {
                BindPager(rbStatus, 1, 0);//UnRecoverd Advances
            }
            //txtSearchWorksite.Text = string.Empty;
            txtdept.Text = string.Empty;
        }
        public void BindGrid(HRCommon objHrCommon, int Status, int LoanType, int InstallmentType)//int WS, int DeptID, int EmpID, string EmpName, 
        {
            int EmpID = 0;
            if (txtEmpID.Text != "")
            {
                EmpID = Convert.ToInt32(txtEmpID.Text);
            }
            int WS = 0;
            try
            {
                //WS = Convert.ToInt32(ddlWs.SelectedValue);
                WS = Convert.ToInt32(ddlWs_hid.Value == "" ? "0" : ddlWs_hid.Value);
            }
            catch { }
            int DeptNo = 0;
            try
            {
                //  DeptNo = Convert.ToInt32(ddlDept.SelectedValue);
                DeptNo = Convert.ToInt32(DeptId);
            }
            catch { }

            try
            {

                if (Convert.ToInt32(ViewState["WSID"]) > 0)
                    WS = Convert.ToInt32(ViewState["WSID"]);
            }
            catch { }
            EmpID = Convert.ToInt32(Convert.ToInt32(Session["UserId"]).ToString());
            DataSet ds = AttendanceDAC.HR_GetEmpAdvaces(WS, DeptNo, EmpID, txtEmpName.Text, Status, objHrCommon, LoanType, InstallmentType, Convert.ToInt32(Session["CompanyID"]));
            gvLoanView.DataSource = ds;
            gvLoanView.DataBind();
            tblLoanView.Visible = true;
            EmpListPaging.Bind(objHrCommon.CurrentPage, objHrCommon.TotalPages, objHrCommon.NoofRecords, objHrCommon.PageSize);
        }

        protected void rbAdvanceView_SelectedIndexChanged(object sender, EventArgs e)
        {
            int rbStatus = Convert.ToInt32(rbAdvanceView.SelectedValue);
            if (Request.QueryString.Count > 0)
            {
                int id = Convert.ToInt32(Request.QueryString["key"].ToString());
                if (id == 2) { BindPager(rbStatus, 2, 2); }
                if (id == 3) { BindPager(rbStatus, 2, 1); }
                if (id == 4) { BindPager(rbStatus, 3, 0); }
            }
            else
            {
                BindPager(rbStatus, 1, 0);//UnRecoverd Advances
            }
        }

        protected void rbAdvance_SelectedIndexChanged(object sender, EventArgs e)
        {
            int rbStatus = Convert.ToInt32(rbAdvanceView.SelectedValue);
            if (Request.QueryString.Count > 0)
            {
                int id = Convert.ToInt32(Request.QueryString["key"].ToString());
                if (id == 2) { BindPager(rbStatus, 2, 2); }
                if (id == 3) { BindPager(rbStatus, 2, 1); }
                if (id == 4) { BindPager(rbStatus, 3, 0); }
            }
            else
            {
                BindPager(rbStatus, 1, 0);//UnRecoverd Advances
            }
        }
        protected void ddlRecoverType_SelectedIndexChanged(object sender, EventArgs e)
        {
            int RType = Convert.ToInt32(ddlRecoverType.SelectedValue);
            if (txtSal.Text != "")
                txtAdvGSal.Text = txtSal.Text;
            if (RType == 1)
            {
                txtAdvNM.ReadOnly = true;
                txtAdvNM.Text = "1";
                if (txtAdvNM.Text != "")
                {
                    try
                    {
                        txtAdvInstAmt.Text = Convert.ToDouble(Convert.ToDouble((txtAdvAmt.Text)) / Convert.ToInt32((txtAdvNM.Text))).ToString();
                       // txtAdvGSal.Text = Convert.ToDouble(Convert.ToDouble(txtSal.Text) - Convert.ToDouble(txtAdvInstAmt.Text)).ToString();
                        try
                        {
                            int IMEIs = Convert.ToInt32(txtAdvNM.Text);
                            if (IMEIs == 1)
                            {
                                ddlRecoverType.SelectedValue = "1";
                            }
                            else
                            {
                                ddlRecoverType.SelectedValue = "2";
                            }
                        }
                        catch { }
                    }
                    catch { AlertMsg.MsgBox(Page, "Enter Values Properly..!"); }
                }
            }
            else
            {
                txtAdvNM.ReadOnly = false;
                txtAdvNM.Text = "";
            }
        }


        protected void GetDept(object sender, EventArgs e)
        {


            DeptId = Convert.ToInt32(ddlDept_hid.Value == "" ? "0" : ddlDept_hid.Value); ;

        }

    }
}