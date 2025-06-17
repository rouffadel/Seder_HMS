using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using DataAccessLayer;
using AECLOGIC.HMS.BLL;

namespace AECLOGIC.ERP.HMS
{
    public partial class StatReports : AECLOGIC.ERP.COMMON.WebFormMaster
    {
        AttendanceDAC obj = new AttendanceDAC();
        static int Status;
        protected override void OnInit(EventArgs e)
        {
            ModuleID = 1;
            base.OnInit(e);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Status = 1;
                lnkESI.CssClass = "lnkselected";
                lnkPF.CssClass = "linksunselected";
                lnkTDS.CssClass = "linksunselected";
                txtDay.Text = DateTime.Now.AddDays(-(DateTime.Now.Day) + 1).ToString("dd/MM/yyyy");
                txtToDate.Text = DateTime.Now.AddDays((DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month) - (DateTime.Now.Day))).ToString("dd/MM/yyyy");
                BindSites();
                BindGrid();
            }
        }

        public void BindSites()
        {

            DataSet ds = new DataSet();
            ds = AttendanceDAC.GetWorkSite(0, '1', Convert.ToInt32(Session["CompanyID"]));
            ddlWorksite.DataSource = ds.Tables[0];
            ddlWorksite.DataTextField = "Site_Name";
            ddlWorksite.DataValueField = "Site_ID";
            ddlWorksite.DataBind();
            ddlWorksite.Items.Insert(0, new ListItem("---ALL---", "0", true));
        }
        public void BindGrid()
        {
            DataSet ds = new DataSet();
            DateTime Fromdate = CODEUtility.ConvertToDate(txtDay.Text, DateFormat.DayMonthYear);
            DateTime Todate = CODEUtility.ConvertToDate(txtToDate.Text, DateFormat.DayMonthYear);
            int? WSID = null;
            if (ddlWorksite.SelectedItem.Value != "0")
            {
                WSID = Convert.ToInt32(ddlWorksite.SelectedItem.Value);
            }
            ds = ReportsRDLC.Get_ESI_PF_TDS_Report(Fromdate, Todate, Status, WSID);
            gvStatReport.DataSource = ds;
            gvStatReport.DataBind();

        }

        decimal TotalESI = 0;
        protected string Getstat()
        {
            return TotalESI.ToString("N2");
        }
        protected string GetstatAmount(decimal Price)
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
        protected void lnkESI_Click(object sender, EventArgs e)
        {
            Status = 1;
            lnkESI.CssClass = "lnkselected";
            lnkPF.CssClass = "linksunselected";
            lnkTDS.CssClass = "linksunselected";
        }
        protected void lnkPF_Click(object sender, EventArgs e)
        {
            Status = 2;
            lnkESI.CssClass = "linksunselected";
            lnkPF.CssClass = "lnkselected";
            lnkTDS.CssClass = "linksunselected";
        }
        protected void lnkTDS_Click(object sender, EventArgs e)
        {
            Status = 3;
            lnkESI.CssClass = "linksunselected";
            lnkPF.CssClass = "linksunselected";
            lnkTDS.CssClass = "lnkselected";
        }
        protected void btnDaySearch_Click(object sender, EventArgs e)
        {
            BindGrid();
        }
    }
}