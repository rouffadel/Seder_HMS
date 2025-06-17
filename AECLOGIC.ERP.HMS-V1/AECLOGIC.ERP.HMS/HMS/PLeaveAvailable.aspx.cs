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
    public partial class PLeaveAvailable : AECLOGIC.ERP.COMMON.WebFormMaster
    {
        AttendanceDAC objRights = new AttendanceDAC();
         
        protected override void OnInit(EventArgs e)
        {
            ModuleID = 1;
            base.OnInit(e);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
           
            if (!IsPostBack)
            {

                BindGrid(1, 0, Convert.ToInt32( Convert.ToInt32(Session["UserId"]).ToString()));
            }
        }
      
        public void BindGrid(int SiteID, int DeptID, int EmpID)
        {
          DataSet  ds = Leaves.GetApplicableLeavesDetails(SiteID, DeptID, EmpID);
            if (ds.Tables.Count > 0)
            {
                gvLeavesAvailable.DataSource = ds;

            }
            gvLeavesAvailable.DataBind();
        }
    }
}
