using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using AECLOGIC.HMS.BLL;

namespace AECLOGIC.ERP.HMS
{
    public partial class PersonalPaySlip : AECLOGIC.ERP.COMMON.WebFormMaster
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

                BindYears();
            }
        }
    
        public void BindYears()
        {
           DataSet ds = AttendanceDAC.GetCalenderYear();

            int i = 0;
            int Maxyear = Convert.ToInt32(ds.Tables[0].Rows[0]["MaxYear"].ToString());
            for (int Minyear = Convert.ToInt32(ds.Tables[0].Rows[0]["MinYear"].ToString()); Minyear <= Maxyear; Minyear++)
            {
                ddlYear.Items.Insert(i, new ListItem(Convert.ToString(Minyear), Convert.ToString(Minyear)));
                i = i + 1;
            }
            ddlMonth.SelectedValue = ds.Tables[0].Rows[0]["PreviousMonth"].ToString();
            ddlYear.SelectedValue = ds.Tables[0].Rows[0]["CurrentYear"].ToString();

        }
        protected void btnView_Click(object sender, EventArgs e)
        {
            try
            {
                int EmpId =  Convert.ToInt32(Session["UserId"]);
                int EmpID =  Convert.ToInt32(Session["UserId"]);
                int Month = Convert.ToInt32(ddlMonth.SelectedValue);
                int Year = Convert.ToInt32(ddlYear.SelectedValue);
                DataSet startdate = AttendanceDAC.GetStartDate();
                // for Jan 2016 selection pay slip showing by Gana
                //if (Month == 1)
                //{
                //    Month = 12;
                //    Year = Year - 1;
                //}
                //else
                //    Month = Month - 1;
                string Date = Month + "/" + 01 + "/" + Year;
                DateTime date = CODEUtility.ConvertToDate(Date, DateFormat.MonthDayYear);



                DataSet ds = PayRollMgr.GetPaySLIP(EmpId, date);

                if (ds.Tables[0].Rows[0]["MontlyCTC"].ToString() != "")
                {
                   // Date = startdate.Tables[0].Rows[0][0].ToString() + "/" + Month + "/" + Year;
                 

                    Date = 01 + "/" + Month + "/" + Year;

                    string strScript = "<script> ";
                    strScript += "var newWindow = window.open('PaySlipV4.aspx?id=" + EmpId + "&Date=" + Date + " ','','Width:1000px; Height:1200px; center:yes; resizable=true;');";

                    strScript += "</script>";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "PopupCP", strScript, false);
                }

                else
                   AlertMsg.MsgBox(Page, "Please select proper month.!");
            }
            catch (Exception)
            {
                AlertMsg.MsgBox(Page, "Please select month.!");
            }

        }



    }
}
