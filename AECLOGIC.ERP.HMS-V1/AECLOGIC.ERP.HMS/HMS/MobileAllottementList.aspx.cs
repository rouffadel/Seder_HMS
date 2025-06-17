using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AECLOGIC.HMS.BLL;
using System.Data;

namespace AECLOGIC.ERP.HMS
{
    public partial class MobileAllottementList : WebFormMaster
    {
        protected override void OnInit(EventArgs e)
        {
          //  ModuleID = 1;
            base.OnInit(e);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString.Count > 0)
                {
                    tblNotAlloted.Visible = false;
                    tblAlloted.Visible = true;
                    // int ID = Convert.ToInt32(Request.QueryString[0]);
                    
                  DataSet ds = AttendanceDAC.HR_GetNotAllottedMobileSims();
                    gvSimView.DataSource = ds;
                    gvSimView.DataBind();
                }
                else
                {
                    tblAlloted.Visible = false;
                    tblNotAlloted.Visible = true;
                    DataSet ds = AttendanceDAC.HR_GetEmpSims();
                    gvRSimsView.DataSource = ds;
                    gvRSimsView.DataBind();
                }
            }
        }
    }
}