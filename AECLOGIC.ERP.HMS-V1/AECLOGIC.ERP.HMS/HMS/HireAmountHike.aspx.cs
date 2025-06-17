using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;
using AECLOGIC.HMS.BLL;

namespace AECLOGIC.ERP.HMS
{
    public partial class HireAmountHike : AECLOGIC.ERP.COMMON.WebFormMaster
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
                if (Request.QueryString.Count > 0)
                {
                    ViewState["Type"] = 0;
                    //int PoWo = Convert.ToInt32(Request.QueryString[0]);

                    int WO = Convert.ToInt32(Request.QueryString[0]);
                    double Amount = Convert.ToDouble(Request.QueryString[1]);
                    int Type = Convert.ToInt32(Request.QueryString[2]);
                    ViewState["Type"] = Type;
                    lblPO.Text = WO.ToString();
                    txtAmount.Text = Amount.ToString();
                }
            }

        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            int WONo = Convert.ToInt32(lblPO.Text);
            double Amount = Convert.ToDouble(txtAmount.Text);
            int UpdateBy =  Convert.ToInt32(Session["UserId"]);
            int HireType = Convert.ToInt32(ViewState["Type"]);
            DataSet ds = AttendanceDAC.HR_GetWODetails(WONo);
            int VendorID = Convert.ToInt32(ds.Tables[0].Rows[0]["vendor_id"].ToString());
            double POAmount = Convert.ToDouble(ds.Tables[0].Rows[0]["AMOUNT"].ToString());
            DateTime FromDate = Convert.ToDateTime(ds.Tables[0].Rows[0]["orgDate"].ToString());
            AttendanceDAC.HR_IncrementHireAmt(WONo, VendorID, HireType, Amount, POAmount, FromDate, UpdateBy);
            ClientScript.RegisterStartupScript(typeof(System.String), "str", "<script type='text/javascript'>alert('Done')</script>");

        }
    }
}
