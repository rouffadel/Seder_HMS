using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

namespace AECLOGIC.ERP.HMS
{
    public partial class PTReport : AECLOGIC.ERP.COMMON.WebFormMaster
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGrid();
            }
        }
        protected override void OnInit(EventArgs e)
        {
            ModuleID = 1;
            base.OnInit(e);
        }

        public void BindGrid()
        {

            DateTime Fromdate = DateTime.Now;
            DateTime Todate = DateTime.Now;

            DataSet ds = ReportsRDLC.Get_PT_Report(Fromdate, Todate);

            grdPTReport.DataSource = ds;
            grdPTReport.DataBind();

        }
    }
}