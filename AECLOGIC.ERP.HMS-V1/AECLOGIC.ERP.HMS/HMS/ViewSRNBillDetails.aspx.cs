using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using DataAccessLayer;

namespace AECLOGIC.ERP.HMS
{
    public partial class ViewSRNBillDetails : AECLOGIC.ERP.COMMON.WebFormMaster
    {

        #region GloabalSection
        decimal TotAmountReWO;
        public decimal TotInwardQty;
        public decimal TotAccQty;
        public decimal TotAmt;
        public decimal TotDispQty;
        #endregion

        ServiceApproval objServiceApproval = new ServiceApproval();
        protected override void OnInit(EventArgs e)
        {
            ModuleID = 1;
            base.OnInit(e);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString.Count > 0)
            {
                DataSet ds = objServiceApproval.MMS_BillSRNs(Convert.ToInt32(Request.QueryString["id"]));
                if (ds.Tables[1].Rows.Count > 0)
                {
                    TotInwardQty = (decimal)ds.Tables[1].Rows[0][0];
                    TotAccQty = (decimal)ds.Tables[1].Rows[0][1];
                    TotAmt = (decimal)ds.Tables[1].Rows[0][2];
                    TotDispQty = (decimal)ds.Tables[1].Rows[0][3];
                }
                gvDetailReport.DataSource = ds.Tables[0];
                gvDetailReport.DataBind();

            }
        }

        #region MyMethods

        public string GenerateGRN(string SRNitem)
        {
            return Convert.ToDouble(SRNitem).ToString("#0000");
            // return "GRN#" + Convert.ToDouble(GDNItemID).ToString("#0000");
        }

        public string GenerateGRNReport(string SRNItem)
        {
            // return "GRN#" + Convert.ToDouble(GDNItemID).ToString("#0000");
            string strReturn;
            strReturn = String.Format("window.showModalDialog('Reports/GRNReport.aspx?ID={0}' ,'','dialogWidth:750px; dialogHeight:850px; center:yes');", SRNItem);
            return strReturn;
        }

        public string GenerateGDN(string SRN)
        {
            //return "GDN#" + Convert.ToDouble(GDNID).ToString("#0000");
            return Convert.ToDouble(SRN).ToString("#0000");
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
    }

}