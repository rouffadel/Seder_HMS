using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using DataAccessLayer;
using AECLOGIC.HMS.BLL;
using Aeclogic.Common.DAL;

namespace AECLOGIC.ERP.HMS
{
    public partial class MachineryListReport : AECLOGIC.ERP.COMMON.WebFormMaster
    {
       
        protected override void OnInit(EventArgs e)
        {
            ModuleID = 1;
            base.OnInit(e);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
           
            if (!IsPostBack)
            {
                btnExpExcel.Visible = false;
            }
        }
    
        protected void btnSaveButton_Click(object sender, EventArgs e)
        {
            List<int> IndexList = new List<int>(); //(List<int>)Session["RepCols"];

            foreach (ListItem li in chkListFields.Items)
            {
                gvReport.Columns[Convert.ToInt32(li.Value)].Visible = li.Selected;
                if (!li.Selected)
                    IndexList.Add(Convert.ToInt32(li.Value));
            }
            Session["RepCols"] = IndexList;
            gvReport.Visible = true;
            DataSet ds = SQLDBUtil.ExecuteDataset("EMS_MachineryCostDetails");
            DataTable dt = ds.Tables[0];
            gvReport.DataSource = dt;
            gvReport.DataBind();
            btnExpExcel.Visible = true;

        }
        protected void btnExpExcel_Click(object sender, EventArgs e)
        {

            Response.Redirect("MachineryExporttoExcel.aspx");


        }
    }
}
