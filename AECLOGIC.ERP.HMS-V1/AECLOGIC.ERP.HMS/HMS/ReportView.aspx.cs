using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AECLOGIC.ERP.HMS
{
    public partial class ReportView : AECLOGIC.ERP.COMMON.WebFormMaster
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
            ReportViewer1.ShowCredentialPrompts = false;
            ReportViewer1.DocumentMapCollapsed = true;
            string UserName = System.Configuration.ConfigurationManager.AppSettings["ReportsUserName"];
            string Password = System.Configuration.ConfigurationManager.AppSettings["ReportsPassword"];
            ReportViewer1.ServerReport.ReportPath = ReportName;// ReportName;
            ReportViewer1.ServerReport.ReportServerUrl = new Uri(System.Configuration.ConfigurationManager.AppSettings["ReportURL"]);
            ReportViewer1.LocalReport.EnableExternalImages = true;
            ReportViewer1.ServerReport.Refresh();
        }
    }
}