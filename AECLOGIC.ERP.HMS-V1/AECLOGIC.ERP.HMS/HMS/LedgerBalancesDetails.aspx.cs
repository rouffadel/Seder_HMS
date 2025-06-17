using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Collections.Generic;//using CommonDoc;
using AECLOGIC.HMS.BLL;
using Aeclogic.Common.DAL;
using AECLOGIC.ERP.COMMON;

namespace AECLOGIC.ERP.HMS
{
    public partial class LedgerBalancesDetails : AECLOGIC.ERP.COMMON.WebFormMaster
    {
        int mid = 0;
        bool viewall; string menuname; string menuid;
        decimal TotalCr; decimal TotalDr; decimal GrandTotalCr; decimal GrandTotalDr;
        int OrderId = 0; double DateVal;
        Paging_Objects PG = new Paging_Objects();
        AjaxDAL objAjax = new AjaxDAL();
        DataSet ds1 = new DataSet();
        int CompanyID = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["CompanyID"].ToString());

        protected override void OnInit(EventArgs e)
        {
            ModuleID = 1;
            base.OnInit(e);
        
            pgLedgerReport.FirstClick += new Paging.PageFirst(EmpListPaging_FirstClick);
            pgLedgerReport.PreviousClick += new Paging.PagePrevious(EmpListPaging_FirstClick);
            pgLedgerReport.NextClick += new Paging.PageNext(EmpListPaging_FirstClick);
            pgLedgerReport.LastClick += new Paging.PageLast(EmpListPaging_FirstClick);
            pgLedgerReport.ChangeClick += new Paging.PageChange(EmpListPaging_FirstClick);
            pgLedgerReport.ShowRowsClick += new Paging.ShowRowsChange(EmpListPaging_ShowRowsClick);
            pgLedgerReport.CurrentPage = 1;
        }

        void EmpListPaging_ShowRowsClick(object sender, EventArgs e)
        {
            pgLedgerReport.CurrentPage = 1;
            BindPager();
        }

        void EmpListPaging_FirstClick(object sender, EventArgs e)
        {
            BindPager();
        }

        void BindPager()
        {
            PG.PageSize = pgLedgerReport.CurrentPage;
            PG.CurrentPage = pgLedgerReport.ShowRows;
            BindBalanceLedgerDetails();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Ajax.Utility.RegisterTypeForAjax(typeof(AjaxDAL));
            if (!IsPostBack)
            {
                int ModuleId = ModuleID;;
                int EmpID = Convert.ToInt32( Convert.ToInt32(Session["UserId"]).ToString());
                int MenuID = Convert.ToInt32(Session["menuid"].ToString());
                ds1 = AttendanceDAC.GetPerferences(EmpID, ModuleId, MenuID);
                if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
                    rbTransPeriod.SelectedValue = ds1.Tables[0].Rows[0]["PrevStatus"].ToString();
                DateVal = Convert.ToDouble(rbTransPeriod.SelectedValue);
                ddlFinYear.Enabled = txtTo.Enabled = txtFrom.Enabled = false;

                SqlParameter[] Parms = new SqlParameter[1];
                Parms[0] = new SqlParameter("@Empid",  Convert.ToInt32(Session["UserId"]));
                  
               DataSet ds = FIllObject.FillDropDown(ref ddlCostCenter, "ACC_GetCostCenters", "---All---", Parms);

                SqlParameter[] parm = new SqlParameter[1];
                parm[0] = new SqlParameter("@companyid", CompanyID);
                ds = FIllObject.FillDropDown(ref ddlVouchertype, "ACC_GetVouchers", "---All---", parm);
               


                pgLedgerReport.CurrentPage = 1;
                BindPager();
                FIllObject.FillDropDown(ref ddlFinYear, "HR_Get_FinancialYearList", "FinYearID", "Name");

            }

            if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
                rbTransPeriod.SelectedValue = ds1.Tables[0].Rows[0]["PrevStatus"].ToString();
            else
                DateVal = Convert.ToDouble(rbTransPeriod.SelectedValue);


            if (Convert.ToInt32(rbTransPeriod.SelectedValue) > 0)
            {
                txtFrom.Text = DateTime.Today.AddDays(-DateVal).ToString("dd/MM/yyyy");
                txtTo.Text = DateTime.Now.ToString("dd/MM/yyyy");
            }
            ddlFinYear.Attributes.Add("onchange", "javascript:return ChangefinYear('" + txtFrom.ClientID + "','" + txtTo.ClientID + "','" + ddlFinYear.ClientID + "');");
            foreach (ListItem li in rbTransPeriod.Items)
                li.Attributes.Add("onclick", "javascript:return dateConfig('" + li.Value + "','" + txtFrom.ClientID + "','" + txtTo.ClientID + "','" + ddlFinYear.ClientID + "');");

            if (rbTransPeriod.SelectedValue == "0")
            {
                txtTo.Enabled = txtFrom.Enabled = true;
            }
            else if (rbTransPeriod.SelectedValue == "-1")
            {
                ddlFinYear.Enabled = true;
            }


        }

      

        protected string GetTotalCr()
        {
            return TotalCr.ToString("N2");
        }

        protected string GetTotalDr()
        {
            return TotalDr.ToString("N2");
        }

        protected string GetAmtCr(decimal Price)
        {
            string amt = string.Empty;
            Price = Convert.ToDecimal(Price.ToString("N2"));
            TotalCr += Price;
            if (Price != 0)
            {
                amt = Price.ToString("N2");
            }

            return amt;
        }

        protected string GetAmtDr(decimal Price)
        {
            string amt = string.Empty;
            Price = Convert.ToDecimal(Price.ToString("N2"));//Session["DP"].ToString()
            TotalDr += Price;
            if (Price != 0)
            {
                amt = Price.ToString("N2");
            }
            return amt;
        }

        public string BindTransDetails(string TransID, string Remarks)
        {
            string St = "";
            if (ViewState["TransDetails"] != null)
            {
                DataTable dtTransDetails = (DataTable)ViewState["TransDetails"];
                DataRow[] drTransDetails = dtTransDetails.Select("TransId='" + TransID + "'");

                foreach (DataRow Drow in drTransDetails)
                {

                    // St = St + "<span class='Ledgers'><a target='_blank' href='LedgerDetails.aspx?id=" + Drow["LedgerID"].ToString() + "' >" + Drow["Ledger"].ToString() + "</a></span>";
                    St = St + "<span class='Ledgers'><a target='_blank' href='LedgerBalancesDetails.aspx?LedgerId=" + Drow["LedgerId"].ToString() + "' >" + Drow["Ledger"].ToString() + "</a></span>";

                    if (Convert.ToDouble(Drow["DebitAmt"].ToString()) > 0)
                    {
                        St = St + "  " + "<b>Dr</b>" + " " + Convert.ToDouble(Drow["DebitAmt"].ToString());
                    }
                    else if (Convert.ToDouble(Drow["CreditAmt"].ToString()) > 0)
                    {
                        St = St + "  " + "<b>Cr</b>" + " " + Convert.ToDouble(Drow["CreditAmt"].ToString());
                    }
                    St = St + "</br>";
                }
            }
            return St += " " + Remarks;
        }

        public void BindBalanceLedgerDetails()
        {

            try
            {
                int LedgerId = Convert.ToInt32(Request.QueryString["LedgerId"].ToString());

                lblClosingBalance.Text = "0";
                lblOpeningBalance.Text = "0";
                System.Data.SqlTypes.SqlDateTime getDate;
                getDate = System.Data.SqlTypes.SqlDateTime.Null;


                SqlParameter[] Parms = new SqlParameter[7];
                Parms[0] = new SqlParameter("@Companyid", CompanyID);
                Parms[1] = new SqlParameter("@LedgerId", LedgerId);
                Parms[2] = new SqlParameter("@VoucherID", SqlDbType.Int);
                if (ddlVouchertype.SelectedIndex > 0)
                {
                    Parms[2] = new SqlParameter("@VoucherID", ddlVouchertype.SelectedValue);
                }
                Parms[3] = new SqlParameter();
                Parms[3].ParameterName = "@startdate";
                if (txtFrom.Text.Trim() == "")
                {
                    Parms[3].Value = getDate;
                }
                else { Parms[3].Value = CODEUtility.ConvertToDate(txtFrom.Text, DateFormat.DayMonthYear); }
                Parms[3].SqlDbType = SqlDbType.DateTime;

                Parms[4] = new SqlParameter();
                Parms[4].ParameterName = "@Endate";
                if (txtTo.Text.Trim() == "")
                {
                    Parms[4].Value = getDate;
                }
                else { Parms[4].Value = CODEUtility.ConvertToDate(txtTo.Text, DateFormat.DayMonthYear); }
                Parms[4].SqlDbType = SqlDbType.DateTime;

                Parms[5] = new SqlParameter("@CostCenterid", SqlDbType.Int);
                if (ddlCostCenter.SelectedValue != "0")
                    Parms[5].Value = ddlCostCenter.SelectedValue;

                Parms[6] = new SqlParameter("@Empid", SqlDbType.Int);
                //if (!(bool)ViewState["ViewAll"])
                //    Parms[6].Value = Session["LoginId"];


                  
              DataSet  Ds = SqlHelper.ExecuteDataset("ACC_GETOPENINGBALANCES1", Parms);

                lblLedgerName.Text = Ds.Tables[0].Rows[0][2].ToString();

                lblOpeningBalance.Text = Ds.Tables[0].Rows[0][0].ToString();
                lblClosingBalance.Text = Ds.Tables[0].Rows[0][1].ToString();
                string companyid = CompanyID.ToString();
                DataRow dr;
                dr = objAjax.getClosingBalancing(companyid, Request.QueryString["LedgerId"].ToString());
                lblMobile.Text = dr["Mobile"].ToString();
                BindLedgerDetails();

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public decimal TotalAmountCr;
        public decimal TotalAmountDr;
        public void BindLedgerDetails()
        {
            int LedgerId = Convert.ToInt32(Request.QueryString["LedgerId"].ToString());

            System.Data.SqlTypes.SqlDateTime getDate;
            getDate = System.Data.SqlTypes.SqlDateTime.Null;

            SqlParameter[] Parms = new SqlParameter[11];

            Parms[0] = new SqlParameter("@CompanyID", CompanyID);
            Parms[1] = new SqlParameter("@LedgerId", LedgerId);
            Parms[6] = new SqlParameter("@VoucherID", SqlDbType.Int);
            if (ddlVouchertype.SelectedIndex > 0)
            {
                Parms[6] = new SqlParameter("@VoucherID", ddlVouchertype.SelectedValue);
            }

            Parms[2] = new SqlParameter();
            Parms[2].ParameterName = "@FromDate";
            if (txtFrom.Text.Trim() == "")
            { Parms[2].Value = getDate; }
            else { Parms[2].Value = CODEUtility.ConvertToDate(txtFrom.Text, DateFormat.DayMonthYear); }
            Parms[2].SqlDbType = SqlDbType.DateTime;

            Parms[3] = new SqlParameter();
            Parms[3].ParameterName = "@ToDate";
            if (txtTo.Text.Trim() == "")
            {
                Parms[3].Value = getDate;
            }
            else { Parms[3].Value = CODEUtility.ConvertToDate(txtTo.Text, DateFormat.DayMonthYear); }

            Parms[3].SqlDbType = SqlDbType.DateTime;

            Parms[4] = new SqlParameter("@Empid", SqlDbType.Int);
            //if (!(bool)ViewState["ViewAll"])
            //    Parms[4].Value = Session["LoginId"];
            Parms[5] = new SqlParameter("@CostCenterid", SqlDbType.Int);
            // if (!(bool)ViewState["ViewAll"])
            if (ddlCostCenter.SelectedValue != "0")
                Parms[5].Value = ddlCostCenter.SelectedValue;
            Parms[7] = new SqlParameter("@CurrentPage", pgLedgerReport.CurrentPage);
            Parms[8] = new SqlParameter("@PageSize", pgLedgerReport.ShowRows);
            Parms[9] = new SqlParameter("@NoofRecords", 3);
            Parms[9].Direction = ParameterDirection.Output;
            Parms[10] = new SqlParameter();
            Parms[10].Direction = ParameterDirection.ReturnValue;


            DataSet ds = SqlHelper.ExecuteDataset("ACC_LedgerSummaryByPage", Parms);
            ViewState["TransDetails"] = ds.Tables[2];
            if (ds.Tables.Count > 1)
            {
                TotalAmountCr = Convert.ToDecimal(ds.Tables[1].Rows[0]["totalCredit"].ToString() == "" ? "0" : ds.Tables[1].Rows[0]["totalCredit"].ToString());
                TotalAmountDr = Convert.ToDecimal(ds.Tables[1].Rows[0]["totalDebit"].ToString() == "" ? "0" : ds.Tables[1].Rows[0]["totalDebit"].ToString());
            }
            GV.DataSource = ds.Tables[0];
            GV.DataBind();
            pgLedgerReport.Visible = true;
            int totpage = Convert.ToInt32(Parms[10].Value);
            int noofrec = Convert.ToInt32(Parms[9].Value);
            PG.TotalPages = totpage;
            PG.NoofRecords = noofrec;


            pgLedgerReport.Bind(pgLedgerReport.CurrentPage, PG.TotalPages, PG.NoofRecords, PG.PageSize);//,PG.PageSize);
        }

        protected void BtnGet_Click(object sender, EventArgs e)
        {
            BindBalanceLedgerDetails();
            PreserveUserPreferences();
        }

        protected void PreserveUserPreferences()
        {

            int ModuleId = ModuleID;;
            int EmpID = Convert.ToInt32( Convert.ToInt32(Session["UserId"]).ToString());
            int MenuID = Convert.ToInt32(Session["mid"].ToString());
            int status = Convert.ToInt32(rbTransPeriod.SelectedValue);
            AttendanceDAC.InsUpdPerferences(EmpID, ModuleId, MenuID, status);
        }
        
    }
}
