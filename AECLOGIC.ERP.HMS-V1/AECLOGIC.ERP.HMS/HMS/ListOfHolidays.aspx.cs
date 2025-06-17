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
    public partial class ListOfHolidays : AECLOGIC.ERP.COMMON.WebFormMaster
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

                BindText();
            }
        }
     
        public void BindText()
        {
           DataSet ds = AttendanceDAC.GetDocumentDetails(10, 0);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {

                TextEditor.InnerHtml = ds.Tables[0].Rows[0]["Value"].ToString();
            }
        }
    }
}
