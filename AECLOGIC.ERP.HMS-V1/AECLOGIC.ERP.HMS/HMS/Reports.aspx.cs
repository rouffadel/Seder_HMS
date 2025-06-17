using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AECLOGIC.ERP.HMS.HMS
{
    public partial class Reports : AECLOGIC.ERP.COMMON.WebFormMaster
    {

        protected override void OnInit(EventArgs e)
        {
            ModuleID = 1;
            base.OnInit(e);
        }

        protected void Page_Load(object sender, EventArgs e)
        {

            try
            {
                if (!IsPostBack)
                    LoadReports();
            }
            catch { }
        }

        private void LoadReports()
        {
          
            string ReportName = Request.QueryString["ReportName"];
            string UserName = System.Configuration.ConfigurationManager.AppSettings["ReportsUserName"];
            string Password = System.Configuration.ConfigurationManager.AppSettings["ReportsPassword"];
            SRSREPortViewer.ServerReport.ReportPath = ReportName; //"/AMS/ChartOfAccounts";
            SRSREPortViewer.ServerReport.ReportServerUrl = new Uri(System.Configuration.ConfigurationManager.AppSettings["ReportURL"]);
            SRSREPortViewer.DocumentMapCollapsed = true;
            SRSREPortViewer.ServerReport.Refresh();
        }
        protected void btn_Click(object sender, EventArgs e)
        {
            LoadReports();
        }
    }
}