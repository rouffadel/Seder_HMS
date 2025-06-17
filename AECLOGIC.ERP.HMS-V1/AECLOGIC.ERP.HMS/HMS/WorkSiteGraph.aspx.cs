using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace AECLOGIC.ERP.HMS
{
    public partial class WorkSiteGraph : AECLOGIC.ERP.COMMON.WebFormMaster
    {
        protected override void OnInit(EventArgs e)
        {
            ModuleID = 1;
            base.OnInit(e);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            Ajax.Utility.RegisterTypeForAjax(typeof(AjaxDAL));
            ClientScript.RegisterStartupScript(typeof(System.String), "str", "<script type='text/javascript'>testDs();</script>");

        }
    }
}