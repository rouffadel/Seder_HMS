using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AECLOGIC.ERP.HMS
{
    public partial class QuotationPreview : AECLOGIC.ERP.COMMON.WebFormMaster
    {
        string filename; string path; int WOID;
        protected void Page_Load(object sender, EventArgs e)
        {
            WOID = Convert.ToInt32(Session["WOID"]);
            if (Request.QueryString.Count > 0)
            {
                if (Convert.ToInt32(Request.QueryString["id"]) == WOID)
                    filename = Request.QueryString["type"].ToString();

            }
            ShowQuotations(filename);
        }
        protected override void OnInit(EventArgs e)
        {
            ModuleID = 1;
            base.OnInit(e);
        }

        public string ShowQuotations(string filename)
        {
            string returnfile = "";
            WOID = Convert.ToInt32(Session["OfferId"]);
            path = Server.MapPath("./Lands-Buildings/" + WOID + "/" + filename);

            if (filename != null)
            {
                returnfile = "./Lands-Buildings/" + WOID + "/" + filename;
            }
            return returnfile;
        }
    }
}