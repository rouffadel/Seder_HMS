using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AECLOGIC.ERP.HMS
{
    public partial class ViewImage : AECLOGIC.ERP.COMMON.WebFormMaster
    {
        protected override void OnInit(EventArgs e)
        {
            ModuleID = 1;
            base.OnInit(e);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!IsPostBack)
            //{
            try
            {
                Image1.ImageUrl = "ContactImages/" + Request.QueryString[0].ToString() + "." + Request.QueryString[1].ToString();
            }
            catch
            {
                AlertMsg.MsgBox(Page, "Unable to Print Image!");
            }
            //}

        }
    }
}