using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
namespace AECLOGIC.ERP.HMS
{
    public partial class ApplicationError : AECLOGIC.ERP.COMMON.WebFormMaster
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           
            string CompanyName = ConfigurationManager.AppSettings["Company"];
            string ModuleID = ConfigurationManager.AppSettings["ModuleId"];
            ErrorID.HRef = "http://www.aeclogic.com/ClientResponses.aspx?ID=" + CrypHelper.Encode(ModuleID) + "&CN=" + CrypHelper.Encode(CompanyName.Trim());
        }
        protected override void OnInit(EventArgs e)
        {
            ModuleID = 1;
            base.OnInit(e);
        }

    }
}