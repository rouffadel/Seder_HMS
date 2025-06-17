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
    public partial class GetServiceItems : AECLOGIC.ERP.COMMON.WebFormMaster
    {
        #region LoadEvents
        int mid = 0; bool viewall; string menuname; string menuid;
        SRNService objSRN = new SRNService();
        protected void Page_Load(object sender, EventArgs e)
        {
           


            if (!IsPostBack)
            {

                if (Request.QueryString["SRN"] != null && Request.QueryString["SRN"] != string.Empty)
                {
                    ViewState["SRNID"] = Convert.ToInt32(Request.QueryString["SRN"].ToString());
                    if (Convert.ToInt32(Request.QueryString["ID"].ToString()) == 0)
                        GridBinding(Convert.ToInt32(ViewState["SRNID"]));
                    else
                        BindGridgvItemDetails(ViewState["SRNID"].ToString());
                }
            }
        }
        #endregion LoadEvents
        protected override void OnInit(EventArgs e)
        {
            ModuleID = 1;
            base.OnInit(e);
        }

        #region MyMethods
        public string GenerateSRN(string SRNID)
        {
            return "SRN#" + Convert.ToDouble(SRNID).ToString("#0000");
        }

        protected void GridBinding(int SRNID)
        {
            DataSet ds = objSRN.MMS_SRN_ViewItems(SRNID);
            if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0)
            {
                gvItems.DataSource = ds;
            }
            gvItems.DataBind();
        }

        private void BindGridgvItemDetails(string SRNID)
        {
            DataSet ds = objSRN.GetMMS_GridItemDetailsFupload(int.Parse(SRNID));
            if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0)
            {
                gvItems.DataSource = ds;
                gvItems.DataBind();
            }
        }
        #endregion MyMethods
    }
}
