using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using AECLOGIC.HMS.BLL;
namespace AECLOGIC.ERP.HMS
{
    public partial class DashBoard : AECLOGIC.ERP.COMMON.WebFormMaster
    {
        HRCommon objHrCommon = new HRCommon();
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected override void OnInit(EventArgs e)
        {
            ModuleID = 1;
            base.OnInit(e);
        }
        protected void lstDashBoard_SelectedIndexChanged(object sender, EventArgs e)
        {
            int n = Convert.ToInt32(lstDashBoard.SelectedItem.Value);
            DataSet ds = new DataSet();
            objHrCommon.CurrentPage = 1;
            objHrCommon.PageSize = 100;
            objHrCommon.OldEmpID = "";
            switch (n)
            {
                case 1:

                    break;
                case 2:
                    ds = ExceReports.ExceRptReportingtoByPaging(objHrCommon, null, null, "", null, Convert.ToInt32(Session["CompanyID"]));
                    BindDataSource(ds);
                    break;


            }

        }

        public void BindDataSource(DataSet ds)
        {
            ChkColumns.DataSource = ds.Tables[0].Columns;
            ChkColumns.DataBind();

            for (int i = 0; i < ChkColumns.Items.Count; i++)
            {

                ChkColumns.Items[i].Selected = true;
            }

            if (ds != null)
            {
                grdDynamic.DataSource = ds;
                grdDynamic.DataBind();
            }
            else
            {
                grdDynamic.DataSource = null;
                grdDynamic.DataBind();
                grdDynamic.EmptyDataText = "No Record(s)";
            }

        }
        protected void BtnAnlExcel_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();

            //if (grdDynamic.Columns.Count > 0)
            //{

            // add the columns to the datatable            
            if (grdDynamic.HeaderRow != null)
            {
                for (int i = 0; i < grdDynamic.HeaderRow.Cells.Count; i++)
                {
                    dt.Columns.Add(grdDynamic.HeaderRow.Cells[i].Text);
                }
            }

            //  add each of the data rows to the table
            foreach (GridViewRow row in grdDynamic.Rows)
            {
                DataRow dr;
                dr = dt.NewRow();

                for (int i = 0; i < row.Cells.Count; i++)
                {
                    dr[i] = row.Cells[i].Text.Replace("&nbsp;", "");
                }
                dt.Rows.Add(dr);
            }

            foreach (ListItem li in ChkColumns.Items)
            {

                if (li.Selected != true)
                    dt.Columns.Remove(li.Text);
            }


            ExportFileUtil.ExportToExcel(dt, "#EFEFEF", "#E6e6e6", "Report");
            //}
            //else
            //{
            //    AlertMsg.MsgBox(Page, "Select report type");
            //}

        }
    }
}