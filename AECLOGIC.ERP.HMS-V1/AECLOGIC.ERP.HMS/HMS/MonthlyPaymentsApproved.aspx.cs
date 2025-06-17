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
using Aeclogic.Common.DAL;

namespace AECLOGIC.ERP.HMS
{
    public partial class MonthlyPaymentsApproved : AECLOGIC.ERP.COMMON.WebFormMaster
    {
        AECLOGIC.HMS.BLL.AttendanceDAC objEmployee = new AttendanceDAC();
        AttendanceDAC objRights = new AttendanceDAC();
        HRCommon objHrCommon = new HRCommon();
          
        static int SearchCompanyID;
        static int Siteid;
        int mid = 0;
        bool viewall;
        string menuname;
        string menuid;
        bool Editable;
        DataSet dsBind = new DataSet();
        int OrderID = 0, Direction = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
               
                SearchCompanyID = Convert.ToInt32(Session["CompanyID"]);

               
                if (!IsPostBack)
                {
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
                                    //txtSearchWorksite.Text = ds.Tables[0].Rows[0]["Name"].ToString();
                                    //txtSearchWorksite.ReadOnly = true;

                                }
                                catch { }
                            }
                        }
                        catch { }
                        //pratap added here date:13-04-2016                    
                        txtEmpID.Attributes.Add("onkeydown", "return controlEnter(event)");
                        txtEmpName.Attributes.Add("onkeydown", "return controlEnter(event)");

                       
                        BindYears();
                        FIllObject.FillDropDown(ref ddlworksite, "HR_GetWorkSite");
                        ddlworksite.SelectedValue = "1";
                        bool Access = Convert.ToBoolean(ViewState["ViewAll"]);
                        OrderID = 2;
                        ViewState["OrderID"] = 2;
                        DataSet startdate = AttendanceDAC.GetStartDate();
                        ViewState["startdate"] = 1;
                        ViewState["enddate"] = 1;
                        DataSet enddate = AttendanceDAC.GetEndDate();

                        ViewState["startdate"] = startdate.Tables[0].Rows[0][0].ToString();
                        ViewState["enddate"] = enddate.Tables[0].Rows[0][0].ToString();
                        if (Access == true)
                        {
                            EmployeBind(objHrCommon);
                        }
                        else
                        {
                            int EmpID =  Convert.ToInt32(Session["UserId"]);

                            EmpListPaging.Visible = false;


                        }


                        EmployeBind(objHrCommon);

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
        public static string DataRowTotable(DataRow dr)
        {
            return dr["Name"].ToString();
        }
      

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {

                EmployeBind(objHrCommon);
            }
            catch (Exception ex)
            {
                clsErrorLog.HMSEventLog(ex, "Salaries", "btnSearch_Click", "005");
            }

        }

        public void BindEmp(int EmpID, int Month, int Year)
        {
              
            AttendanceDAC ADAC = new AttendanceDAC();

            DataSet ds = ADAC.HR_EmpSalriesListByEmpID(EmpID, Month, Year, objHrCommon);

            gvPaySlip.DataSource = ds;
            gvPaySlip.DataBind(); try { gvPaySlip.FooterRow.Visible = false; }
            catch { }
            EmpListPaging.Visible = false;
        }
        void EmployeBind(HRCommon objHrCommon)
        {
            try
            {
                objHrCommon.PageSize = EmpListPaging.ShowRows;
                objHrCommon.CurrentPage = EmpListPaging.CurrentPage;

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
                string stDate, enddate;
                stDate = ViewState["startdate"] + "/" + Month + "/" + Year;
                enddate = ViewState["enddate"] + "/" + ddlMonth.SelectedItem.Value + "/" + ddlYear.SelectedItem.Value;


                DateTime startdate = CodeUtilHMS.ConvertToDate(stDate, CodeUtilHMS.DateFormat.DayMonthYear);
                DateTime EndDate = CodeUtilHMS.ConvertToDate(enddate, CodeUtilHMS.DateFormat.DayMonthYear);
                gvPaySlip.DataSource = null;
                gvPaySlip.DataBind();
                string Name = txtEmpName.Text;

                gvPaySlip.Visible = true;
                int EmpID = 0;

                if (txtEmpName.Text == "" || txtEmpName.Text == null)
                {
                    txtEmpNameHidden.Value = "";
                }
                if (txtEmpID.Text != "" || txtEmpID.Text != string.Empty)
                {
                    EmpID = Convert.ToInt32(txtEmpID.Text);
                }
                else if (txtEmpName.Text != "" || txtEmpName.Text != null)
                {
                    EmpID = Convert.ToInt32(txtEmpNameHidden.Value == "" ? "0" : txtEmpNameHidden.Value);
                }

                else
                {
                    EmpID = 0;
                }

                objHrCommon.SiteID = Convert.ToInt32(ddlworksite.SelectedValue);

                OrderID = Convert.ToInt32(ViewState["OrderID"]);
                dsBind = USP_Salaries_All_Approved(objHrCommon, startdate, EndDate, EmpID);
                ViewState["NoOfRecords"] = objHrCommon.NoofRecords;
                if (dsBind != null && dsBind.Tables.Count != 0 && dsBind.Tables[0].Rows.Count > 0)
                {
                    gvPaySlip.DataSource = dsBind;
                    EmpListPaging.Bind(objHrCommon.CurrentPage, objHrCommon.TotalPages, objHrCommon.NoofRecords, objHrCommon.PageSize);
                    EmpListPaging.Visible = true;
                }
                else
                {
                    gvPaySlip.EmptyDataText = "No Records Found";
                    EmpListPaging.Visible = false;
                }
                gvPaySlip.DataBind();


            }
            catch (Exception e)
            {
                throw e;
            }
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
        public void BindYears()
        {

            FIllObject.FillDropDown(ref ddlYear, "HMS_YearWise");


           DataSet ds = AttendanceDAC.GetCalenderYear();

           
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
            //#endregion set defalult month and year
        }

        private DataSet USP_Salaries_All_Approved(HRCommon objHrCommon, DateTime startdate, DateTime enddate, int Empid)
        {
            DataSet ds = null;
            try
            {

                SqlParameter[] sqlParams = new SqlParameter[8];
                sqlParams[0] = new SqlParameter("@CurrentPage", objHrCommon.CurrentPage);
                sqlParams[1] = new SqlParameter("@PageSize", objHrCommon.PageSize);
                sqlParams[2] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
                sqlParams[2].Direction = ParameterDirection.ReturnValue;
                sqlParams[3] = new SqlParameter("@NoofRecords", System.Data.SqlDbType.Int);
                sqlParams[3].Direction = ParameterDirection.Output;

                sqlParams[4] = new SqlParameter("@empid", Empid);
                sqlParams[5] = new SqlParameter("@paystartdate", startdate);
                sqlParams[6] = new SqlParameter("@payenddate", enddate);
                sqlParams[7] = new SqlParameter("@WSID", objHrCommon.SiteID);

                ds = SQLDBUtil.ExecuteDataset("USP_Salaries_All_Approved", sqlParams);
                objHrCommon.NoofRecords = (int)sqlParams[3].Value;
                objHrCommon.TotalPages = (int)sqlParams[2].Value;

            }
            catch { }
            return ds;


        }


        decimal TotalSpecialAmount = 0;
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

        decimal TotalOTAmount = 0;
        protected string GetOTTotalAmount()
        {
            return TotalOTAmount.ToString("N2");
        }
        decimal TotalAmount = 0;
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
        protected string GetAmount()
        {
            return TotalAmount.ToString("N2");
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

       

    }
}