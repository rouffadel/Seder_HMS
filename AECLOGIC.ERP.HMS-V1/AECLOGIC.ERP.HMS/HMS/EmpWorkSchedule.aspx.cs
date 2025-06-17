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
    public partial class EmpWorkSchedule : AECLOGIC.ERP.COMMON.WebFormMaster
    {
        #region PageLoad
        protected void Page_Load(object sender, EventArgs e)
        {
            TRMonths();
            TRYears();
            TRDays();
            TRMinSec();
            //TRMin();
            TRHours();
            GetTaskIDDetails();
        }
        #endregion PageLoad
        protected override void OnInit(EventArgs e)
        {
            ModuleID = 1;
            base.OnInit(e);
        }
        #region SupportingMethods
        public void GetTaskIDDetails()
        {
            int Id = int.Parse(Request.QueryString["TaskID"].ToString());
             
            DataSet ds = AttendanceDAC.HR_GetToDolistByToDOID(Id);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                lblSubDetails.Text = ds.Tables[0].Rows[0]["Subject"].ToString();
                txtTskDetails.Text = ds.Tables[0].Rows[0]["Task"].ToString();
                //lblTaskDetails.Text = ds.Tables[0].Rows[0]["Task"].ToString();
                lblPriorityDetails.Text = ds.Tables[0].Rows[0]["Priority"].ToString();
                lblStartDateDetails.Text = ds.Tables[0].Rows[0]["StartDate"].ToString();
                lblDueDateDetails.Text = ds.Tables[0].Rows[0]["DueDate"].ToString();
                lblAssignedBy.Text = ds.Tables[0].Rows[0]["AssignedBy"].ToString();
            }

        }
        public void TRMonths()
        {
            //for (int i = 1; i < DateTime.Now.Year; i++)
            for (int i = 0; i <= 12; i++)
            {
                ddlMnths.Items.Add(i.ToString());
            }
        }
        public void TRYears()
        {
            //for (int i = 1; i < DateTime.Now.Year; i++)
            for (int i = 0; i <= 5; i++)
            {
                ddlYears.Items.Add(i.ToString());
            }
        }
        public void TRDays()
        {
            //for (int i = 1; i < DateTime.Now.Year; i++)
            for (int i = 0; i <= 31; i++)
            {
                ddlDays.Items.Add(i.ToString());
            }
        }
        public void TRHours()
        {
            //for (int i = 1; i < DateTime.Now.Year; i++)
            for (int i = 0; i <= 12; i++)
            {
                ddlHours.Items.Add(i.ToString());
                ddlStrtHr.Items.Add(i.ToString());
                ddlEndHr.Items.Add(i.ToString());



            }
        }
        public void TRMinSec()
        {
            //for (int i = 1; i < DateTime.Now.Year; i++)
            for (int i = 0; i <= 59; i++)
            {
                ddlMin.Items.Add(i.ToString());
                ddlSec.Items.Add(i.ToString());
                ddlStrtMin.Items.Add(i.ToString());
                ddlEndMin.Items.Add(i.ToString());

            }
        }

        #endregion SupportingMethods
    }
}