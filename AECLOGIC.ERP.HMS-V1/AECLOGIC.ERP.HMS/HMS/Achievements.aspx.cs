using System;
using System.Data;
using AECLOGIC.HMS.BLL;
namespace AECLOGIC.ERP.HMS
{
    public partial class Achievements : AECLOGIC.ERP.COMMON.WebFormMaster
    {
        int mid = 0;
        bool viewall;
        string menuname;
        string menuid;
        HRCommon objHrCommon = new HRCommon();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lbldate.Text = DateTime.Now.GetDateTimeFormats()[10];
                if (Request.QueryString.Count > 0)
                {

                    ViewState["EmpID"] = Request.QueryString["EmpID"];
                    int empid = Convert.ToInt32(ViewState["EmpID"]);
                    BindDetails();
                }

            }
        }
        protected override void OnInit(EventArgs e)
        {
            ModuleID = 1;
            base.OnInit(e);
        }
        public void BindDetails()
        {
            if (ViewState["EmpID"] != null && ViewState["EmpID"] != string.Empty)
            {
                objHrCommon.EmpID = Convert.ToInt32(ViewState["EmpID"]);
            }
            DataSet ds = AttendanceDAC.GetAchievementDetails(objHrCommon);
            if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0)
            {
                txtachievements.Text = ds.Tables[0].Rows[0]["Achievements"].ToString();
                txtduties.Text = ds.Tables[0].Rows[0]["Duties"].ToString();
            }



        }

        protected void btnsubmit_Click(object sender, EventArgs e)
        {
            try
            {
                int AchievmentID = 0;
                if (ViewState["AchievmentID"] != null && ViewState["AchievmentID"] != string.Empty)
                {
                    objHrCommon.AchievmentID = Convert.ToInt32(ViewState["AchievmentID"]);
                }
                else
                {
                    objHrCommon.AchievmentID = AchievmentID;
                }

                objHrCommon.Duties = txtduties.Text.Trim();
                objHrCommon.Achievemets = txtachievements.Text.Trim();
                objHrCommon.UserID =  Convert.ToInt32(Session["UserId"]);
                objHrCommon.EmpID = Convert.ToInt32(ViewState["EmpID"]);
                int res = Convert.ToInt32(AttendanceDAC.InsUpd_Achievements(objHrCommon));
                if (res == 1)
                {
                    AlertMsg.MsgBox(Page, "Assigned Successfully");
                }
                else
                {

                    AlertMsg.MsgBox(Page, "Updated Successfully");
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
