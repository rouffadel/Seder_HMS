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
    public partial class SiteEmployeeWorkDetails : AECLOGIC.ERP.COMMON.WebFormMaster
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Bind();
            }

        }
        protected override void OnInit(EventArgs e)
        {
            ModuleID = 1;
            base.OnInit(e);
        }
        public void Bind()
        {
            int DeptNo = Convert.ToInt32(Request.QueryString[0].ToString());
            int SiteID = Convert.ToInt32(Request.QueryString[1].ToString());
            int Shift = Convert.ToInt32(Request.QueryString[2].ToString());
            int empid = Convert.ToInt32(Request.QueryString[3].ToString());
            DataSet ds = AttendanceDAC.HR_GetSiteEmpWorkstatus(DeptNo, SiteID, Shift, empid);
            if (ds != null & ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                RpteView.DataSource = ds;
                RpteView.DataBind();
            }
            else
            {
                AlertMsg.MsgBox(Page, "No Employee Working in that Shift", AlertMsg.MessageType.Info);
                Response.Redirect("Attendance.aspx");
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("Attendance.aspx");
        }
    }
}
