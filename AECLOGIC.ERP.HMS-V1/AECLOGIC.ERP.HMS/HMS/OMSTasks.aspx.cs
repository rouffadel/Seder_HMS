using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace AECLOGIC.ERP.HMS
{
    public partial class OMSTasks : AECLOGIC.ERP.COMMON.WebFormMaster
    {
        protected override void OnInit(EventArgs e)
        {
            ModuleID = 1;
            base.OnInit(e);
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnCall_Click(object sender, EventArgs e)
        {
            AECLOGIC.ERP.HMS.OMSWebService.OMSServiceSoapClient objService = new AECLOGIC.ERP.HMS.OMSWebService.OMSServiceSoapClient();
           
        }
        protected void btnCallSAP_Click(object sender, EventArgs e)
        {
            AECLOGIC.ERP.HMS.SAPWebService.BAPI_MATERIAL_GET_ALLPortTypeClient obj = new AECLOGIC.ERP.HMS.SAPWebService.BAPI_MATERIAL_GET_ALLPortTypeClient();
        }
    }
}