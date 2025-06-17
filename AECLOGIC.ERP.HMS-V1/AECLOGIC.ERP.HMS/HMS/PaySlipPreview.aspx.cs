using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace AECLOGIC.ERP.HMS
{
    public partial class PaySlipPreview : WebFormMaster
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

             st= sdt[1] + "/" + sdt[0] + "/" + sdt[2];
            DateTime date = CODEUtility.ConvertToDate(st, DateFormat.MonthDayYear);
            
                date = date.AddDays(-1);
          
            string[] sdt1 = date.ToString().Split('/');
            string[] sdt2 = sdt1[2].Split(' ');
            st = sdt1[0] + '/' + sdt1[1] + '/' + sdt2[0];
            date = CODEUtility.ConvertToDate(st, DateFormat.MonthDayYear);
            date = date.AddMonths(1);


         
            ViewState["Year"] = date.Year;
            ViewState["Month"] = date.Month.ToString("d2");
          


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
              
                lblmonthslip.Text = date.ToString("MMM") + "-" + ds.Tables[5].Rows[0]["PayYear"].ToString(); 
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
            //net payble
            lblNetPayable.Text = lblSalbfIT.Text;
            lblTDS.Text = ds.Tables[9].Rows[0]["TDS"].ToString();
            if (Convert.ToInt32(ds.Tables[28].Rows[0]["Takeamt"].ToString()) > 0)
                lblTakeHome.Text = ds.Tables[28].Rows[0]["Takeamt"].ToString();
            else
                lblTakeHome.Text = 0.ToString();
            lblSpecial.Text = ds.Tables[28].Rows[0]["SPecialAmt"].ToString();  
            lblLRecovery.Text = "(" + ds.Tables[9].Rows[0]["AdvanceRecovery"].ToString() + ")";
            lblMobile.Text = ds.Tables[9].Rows[0]["MobileExp"].ToString();
            lblEducess.Text = ds.Tables[9].Rows[0]["EduCess"].ToString();
            lblMess.Text = ds.Tables[9].Rows[0]["MessValue"].ToString();
            //Last Hike Details
            lblDoLI.Text = ds.Tables[10].Rows[0]["FromDate"].ToString();
            lblNonCTC.Text = ds.Tables[9].Rows[0]["NonCTCCompVal"].ToString();
            lblAbsAmt.Text ="("+ ds.Tables[25].Rows[0]["absamt"].ToString()+")";
            lblEmpPenalities.Text = "("+ds.Tables[26].Rows[0]["EMPPAmount"].ToString() + ")";
            lblOT.Text = ds.Tables[27].Rows[0]["OTAmount"].ToString();
            
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
            int Empid =  Convert.ToInt32(Request.QueryString[0]);
            string month = ViewState["Month"].ToString();
            int year = Convert.ToInt32(ViewState["Year"]);

            Response.Redirect("ViewAttendance.aspx?Empid=" + Empid + "&Month=" + month + "&Year=" + year); 
        }
    }
}
