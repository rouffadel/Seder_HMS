using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AECLOGIC.HMS.BLL;
using System.Data;
using System.Configuration;

namespace AECLOGIC.ERP.HMS
{
    public partial class AttendanceMark : AECLOGIC.ERP.COMMON.WebFormMaster
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
                BindWS();
                BindGrid();
                BindGrid2(DateTime.Now);
                txtDay.Text = DateTime.Now.ToString(ConfigurationManager.AppSettings["DateFormat"]);
            }
        }
         public void BindWS()
        {
            DataSet ds = Attendance.HR_GetWorkSite_By_SMSEmpAttendance();
            ddlworksite.DataSource=ds;
             ddlworksite.DataTextField="Site_Name" ;
             ddlworksite.DataValueField = "Site_ID";
             ddlworksite.DataBind();
         }
        public void BindGrid()
        {
          
            DataSet ds = AttendanceDAC.HR_AttendanceMarkCount(Convert.ToInt32(Session["CompanyID"]));
            gvMark.DataSource = ds;
            gvMark.DataBind();
          
        }
        public void BindGrid2(DateTime Date)
        {
            DataSet ds = null;
            ds = AttendanceDAC.HR_Get_AttendanceMarkingdetails(Date, Convert.ToInt32(Session["CompanyID"]),Convert.ToInt32(ddlworksite.SelectedValue));
            if(ds.Tables[0].Rows.Count==0)
            {
                gvListOfMark.DataSource = null;
                gvListOfMark.DataBind();
            }
            else
            {
                gvListOfMark.DataSource = ds;
                gvListOfMark.DataBind();
            }
           

        }
        protected void txtDay_TextChanged(object sender, EventArgs e)
        {
           
            DateTime Date2 = CODEUtility.ConvertToDate(txtDay.Text.Trim(), DateFormat.DayMonthYear);
            BindGrid2(Date2);
        }
        protected string GetTotal()
        {
            DataSet ds = AttendanceDAC.HR_AttendanceMarkCount(Convert.ToInt32(Session["CompanyID"]));
            string total = ds.Tables[1].Rows[0][0].ToString();
            return total.ToString();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            DateTime Date = CODEUtility.ConvertToDate(txtDay.Text.Trim(), DateFormat.DayMonthYear);
            BindGrid2(Date);
        }

        protected void ddlworksite_SelectedIndexChanged(object sender, EventArgs e)
        {
            DateTime Date = CODEUtility.ConvertToDate(txtDay.Text.Trim(), DateFormat.DayMonthYear);
            int WorkSite = Convert.ToInt32(ddlworksite.SelectedValue);
            BindGrid2(Date);
        }
    }
}