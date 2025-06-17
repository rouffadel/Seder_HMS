using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using AECLOGIC.HMS.BLL;

namespace AECLOGIC.ERP.HMS
{
    public partial class History : AECLOGIC.ERP.COMMON.WebFormMaster
    {
        AttendanceDAC objDuties = new AttendanceDAC();
        HRCommon objHrCommon = new HRCommon();
        protected override void OnInit(EventArgs e)
        {
            ModuleID = 1;
            base.OnInit(e);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
           
            if (!IsPostBack)
            {

                int EmpID = 0;
                if (Request.QueryString.Count > 0)
                {
                    EmpID = Convert.ToInt32(Request.QueryString["Empid"].ToString());
                }
                BindGrid(EmpID);

            }
        }
     
        public void BindGrid(int EmpID)
        {

         DataSet   dsDuties = objDuties.GetDutiesList(EmpID);
            if (dsDuties != null && dsDuties.Tables.Count != 0 && dsDuties.Tables[0].Rows.Count > 0)
            {
                gvAchievements.DataSource = dsDuties;

            }
            gvAchievements.DataBind();
        }
    }
}
