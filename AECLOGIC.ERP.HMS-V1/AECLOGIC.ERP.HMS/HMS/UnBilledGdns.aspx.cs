using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Aeclogic.Common.DAL;
using System.Data.SqlClient;
using DataAccessLayer;
using AECLOGIC.ERP.COMMON;

namespace AECLOGIC.ERP.HMS
{
    public partial class UnBilledGdns : AECLOGIC.ERP.COMMON.WebFormMaster
    {
        #region GloabalSection
        decimal TotAmountReWO;
        public decimal TotInwardQty;
        public decimal TotAccQty;
        public decimal TotAmt;
        public decimal TotDispQty;
        #endregion
        Paging_Objects PG = new Paging_Objects();
        protected override void OnInit(EventArgs e)
        {
            ModuleID = 1;
            base.OnInit(e);
       
            this.Title = "Un Billed Gdn's";
            taskPaging.FirstClick += new Paging.PageFirst(taskPaging_FirstClick);
            taskPaging.PreviousClick += new Paging.PagePrevious(taskPaging_FirstClick);
            taskPaging.NextClick += new Paging.PageNext(taskPaging_FirstClick);
            taskPaging.LastClick += new Paging.PageLast(taskPaging_FirstClick);
            taskPaging.ChangeClick += new Paging.PageChange(taskPaging_FirstClick);
            taskPaging.ShowRowsClick += new Paging.ShowRowsChange(taskPaging_ShowRowsClick);
            taskPaging.CurrentPage = 1;
        }
        void taskPaging_ShowRowsClick(object sender, EventArgs e)
        {
            taskPaging.CurrentPage = 1;
            BindPager();
        }
        void taskPaging_FirstClick(object sender, EventArgs e)
        {
            BindPager();
        }
        void BindPager()
        {
            PG.PageSize = taskPaging.CurrentPage;
            PG.CurrentPage = taskPaging.ShowRows;

            BindItems(PG);
            MyAccordion.SelectedIndex = -1;
        }
        void BindItems(Paging_Objects Obj)
        {
            Obj.PageSize = taskPaging.ShowRows;
            Obj.CurrentPage = taskPaging.CurrentPage;
            SqlParameter[] param = new SqlParameter[7];
            param[0] = new SqlParameter("@CurrentPage", Obj.CurrentPage);
            param[1] = new SqlParameter("@PageSize", Obj.PageSize);
            param[2] = new SqlParameter("@NoOfRecords", 0);
            param[2].Direction = ParameterDirection.Output;
            if (ddlVendor.SelectedIndex != 0)
                param[3] = new SqlParameter("@VendorId", ddlVendor.SelectedValue);
            else
                param[3] = new SqlParameter("@VendorId", SqlDbType.Int);

            if (ddlWorkSite.SelectedIndex != 0)
                param[4] = new SqlParameter("@WorkSite", ddlWorkSite.SelectedValue);
            else
                param[4] = new SqlParameter("@WorkSite", SqlDbType.Int);

            if (txtGdn.Text != "")
                param[5] = new SqlParameter("@GdnId", Convert.ToInt32(txtGdn.Text));
            else
                param[5] = new SqlParameter("@GdnId", SqlDbType.Int);
            param[6] = new SqlParameter();
            param[6].Direction = ParameterDirection.ReturnValue;
            DataSet ds = SQLDBUtil.ExecuteDataset("MMS_UnBilledGDNs", param);
            if (ds.Tables[1].Rows.Count > 0)
            {
                TotInwardQty = (decimal)ds.Tables[1].Rows[0][0];
                TotAccQty = (decimal)ds.Tables[1].Rows[0][1];
                TotAmt = (decimal)ds.Tables[1].Rows[0][2];
                TotDispQty = (decimal)ds.Tables[1].Rows[0][3];
            }
            gvDetailReport.DataSource = ds.Tables[0];
            gvDetailReport.DataBind();

            int totpage = Convert.ToInt32(param[6].Value);
            int noofrec = Convert.ToInt32(param[2].Value);

            Obj.TotalPages = totpage;
            Obj.NoofRecords = noofrec;
            taskPaging.Bind(Obj.CurrentPage, Obj.TotalPages, Obj.NoofRecords, Obj.PageSize);//, Obj.PageSize);
        }
        //BillsApproval objBillsApproval = new BillsApproval();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                FillDropDowns();
                BindGrid();
            }
            selected();
        }

        private void FillDropDowns()
        {
            FIllObject.FillDropDown(ref ddlWorkSite, "PM_GetPOWorkSites", "---All---");
            FIllObject.FillDropDown(ref ddlVendor, "PM_GetPOVendors", "---All---");
        }

        private void BindGrid()
        {
            BindPager();
        }

        #region MyMethods

        public string GenerateGRN(string GDNItemID)
        {
            return Convert.ToDouble(GDNItemID).ToString("#0000");
            // return "GRN#" + Convert.ToDouble(GDNItemID).ToString("#0000");
        }

        public string GenerateGRNReport(string GDNItemID)
        {
            // return "GRN#" + Convert.ToDouble(GDNItemID).ToString("#0000");
            string strReturn;
            strReturn = String.Format("window.showModalDialog('Reports/GRNReport.aspx?ID={0}' ,'','dialogWidth:750px; dialogHeight:850px; center:yes');", GDNItemID);
            return strReturn;
        }

        public string GenerateGDN(string GDNID)
        {
            //return "GDN#" + Convert.ToDouble(GDNID).ToString("#0000");
            return Convert.ToDouble(GDNID).ToString("#0000");
        }



        private void ResetTextboxesRecursive(Control ctrl)
        {
            if (ctrl is TextBox)
                (ctrl as TextBox).Text = string.Empty;
            else
            {
                foreach (Control childControl in ctrl.Controls)
                {
                    this.ResetTextboxesRecursive(childControl);
                }
            }
            if (ctrl is DropDownList)
                (ctrl as DropDownList).SelectedIndex = -1;
            else
            {
                foreach (Control childControl in ctrl.Controls)
                {
                    this.ResetTextboxesRecursive(childControl);
                }
            }
        }

        #endregion MyMethods
        protected void btnGet_Click(object sender, EventArgs e)
        {
            BindPager();

        }
        
        public void selected()
        {
            string URL = Request.Url.Segments[Request.Url.Segments.Length - 1] + Request.Url.Query;
            if (URL == "UnBilledGdns.aspx")
            {
                lnkApprovals.CssClass = "";
                lnkApproved.CssClass = "";
                lnkApprovals.CssClass = "";
                lnkUnbilled.CssClass = "lnkselected";
            }

        }
    }
}