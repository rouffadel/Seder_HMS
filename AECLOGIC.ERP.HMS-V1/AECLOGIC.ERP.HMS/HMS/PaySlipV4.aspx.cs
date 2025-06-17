using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Aeclogic.Common.DAL;
using System.Globalization;

namespace AECLOGIC.ERP.HMS
{
    public partial class PaySlipV4 : WebFormMaster
    {

         
        private static string Company = ConfigurationSettings.AppSettings["Company"].ToString();
        private static string Address = ConfigurationSettings.AppSettings["CompanyAddress"].ToString();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {


                if (Request.QueryString.Count > 0)
                {

                    BindDetails();
                }
            }
        }
        public void BindDetails()
        {
            int EmpID = Convert.ToInt32(Request.QueryString[0]);
            string[] sdt = Request.QueryString[1].ToString().Split('/');
            string st;

            st = sdt[1] + "/" + sdt[0] + "/" + sdt[2];
           



            ViewState["Year"] = sdt[1];
            ViewState["Month"] = sdt[2];
            
            SqlParameter[] sqlParams = new SqlParameter[3];
            sqlParams[0] = new SqlParameter("@Empid", EmpID);
            sqlParams[1] = new SqlParameter("@Month", sdt[1]);
            sqlParams[2] = new SqlParameter("@Year", sdt[2]);


            DataSet ds = SQLDBUtil.ExecuteDataset("sh_PaySlipDetailsRev4", sqlParams);

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[1].Rows.Count > 0)
            {
                grdWages.DataSource = ds.Tables[1];
                grdWages.DataBind();
            }
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[2].Rows.Count > 0)
            {
                grdAllowances.DataSource = ds.Tables[2];
                grdAllowances.DataBind();
            }

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[3].Rows.Count > 0)
            {
                grdNonCTC.DataSource = ds.Tables[3];
                grdNonCTC.DataBind();
            }

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[4].Rows.Count > 0)
            {
                grdDuductSatatutory.DataSource = ds.Tables[4];
                grdDuductSatatutory.DataBind();
            }


           


            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                lblEmpID.Text = ds.Tables[0].Rows[0]["EmpID"].ToString();
                lblName.Text = ds.Tables[0].Rows[0]["name"].ToString();
                lblDept.Text = ds.Tables[0].Rows[0]["DepartmentName"].ToString();
                lbldesig.Text = ds.Tables[0].Rows[0]["Designation"].ToString();
                lblDOJ.Text = ds.Tables[0].Rows[0]["Dateofjoin"].ToString();
                lblbankACno.Text = ds.Tables[0].Rows[0]["BankACNo"].ToString();
                lblPancardNo.Text = ds.Tables[0].Rows[0]["PanNumber"].ToString();
                int iMonthNo = Convert.ToInt32(ds.Tables[0].Rows[0]["Month"].ToString());
                DateTime dtDate = new DateTime(2000, iMonthNo, 1);
                string sMonthName = dtDate.ToString("MMM");
                string sMonthFullName = dtDate.ToString("MMMM");
                lblmonthslip.Text = sMonthFullName + "-" + ds.Tables[0].Rows[0]["Year"].ToString();
                lblNODW.Text = ds.Tables[0].Rows[0]["PayAbleDays"].ToString();
                lblArreardays.Text = ds.Tables[0].Rows[0]["PendingPayableDays"].ToString();
                lblCompanyName.Text = Company;
                lblworksite.Text = ds.Tables[0].Rows[0]["Site_Name"].ToString();
                lblGross.Text = (Convert.ToDecimal(TotalAllowances.ToString("N2")) +Convert.ToDecimal(TotalWages.ToString("N2")) + Convert.ToDecimal(NonCTC.ToString("N2"))).ToString();
                
            }
            NumberToWords num = new NumberToWords();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                lblBenfitAmount.Text = ds.Tables[0].Rows[0]["BenfitAmt"].ToString();
            else
                lblBenfitAmount.Text = 0.ToString();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                lblDueAmount.Text = ds.Tables[0].Rows[0]["DAmount"].ToString();
            else
                lblDueAmount.Text = 0.ToString();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                lblAbsDue.Text = ds.Tables[0].Rows[0]["AbsDue"].ToString();
            else
                lblAbsDue.Text = 0.ToString();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                lblWords.Text = num.changeNumericToWords(ds.Tables[0].Rows[0]["Payable"].ToString());
                lblTakeHome.Text = ds.Tables[0].Rows[0]["Payable"].ToString();
            }
            else
            {
                lblWords.Text = num.changeNumericToWords("0");
                lblTakeHome.Text = 0.ToString();
            }
           
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[5].Rows.Count > 0)
            {
                lblAbsAmt.Text = "(" + ds.Tables[5].Rows[0]["Amount"].ToString() + ")";
            }
            else
                lblAbsAmt.Text = 0.ToString();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[6].Rows.Count > 0)
            {
                lblLRecovery.Text = "(" + ds.Tables[6].Rows[0]["Amount"].ToString() + ")";
            }
            else
                lblLRecovery.Text = 0.ToString();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[7].Rows.Count > 0)
            {
                lblOT.Text = ds.Tables[7].Rows[0]["Amount"].ToString();
            }
            else
                lblOT.Text = 0.ToString();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[8].Rows.Count > 0)
            {
                lblEmpPenalities.Text = "(" + ds.Tables[8].Rows[0]["Amount"].ToString() + ")";
            }
            else
                lblEmpPenalities.Text = 0.ToString();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[9].Rows.Count > 0)
            {
                //lblLRecovery.Text = "(" + ds.Tables[9].Rows[0]["Amount"].ToString() + ")";
            }
            else
            {
                // lblTakeHome.Text = 0.ToString();
            }
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[10].Rows.Count > 0)
            {
                //lblOT.Text = ds.Tables[10].Rows[0]["Amount"].ToString();
            }
            else
            {
             //   lblTakeHome.Text = 0.ToString();
            }
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[11].Rows.Count > 0)
            {
                //lblEmpPenalities.Text = "(" + ds.Tables[11].Rows[0]["Amount"].ToString() + ")";
            }
            else
            { 
                //lblTakeHome.Text = 0.ToString(); 
            }

        }


        //Grid Total For  wages
        decimal TotalWages = 0;

        protected string GetWages()
        {
            return TotalWages.ToString("N2");
        }

        protected string GetAmtWages(decimal Price)
        {

            string amt = string.Empty;
            Price = Convert.ToDecimal(Price.ToString("N2"));
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
        protected override void OnInit(EventArgs e)
        {
            // ModuleID = 1;
            base.OnInit(e);
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

        protected void lnkViewAttendance_Click(object sender, EventArgs e)
        {
            int Empid = Convert.ToInt32(Request.QueryString[0]);
            string month = ViewState["Month"].ToString();
            int year = Convert.ToInt32(ViewState["Year"]);

            Response.Redirect("ViewAttendance.aspx?Empid=" + Empid + "&Month=" + month + "&Year=" + year);
        }
    }
}
