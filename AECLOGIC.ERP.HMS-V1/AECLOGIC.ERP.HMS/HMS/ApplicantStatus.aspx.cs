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
    public partial class ApplicantStatus : AECLOGIC.ERP.COMMON.WebFormMaster
    {
        #region Declaration
        #endregion Declaration

        #region Pageload
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                rbStatus.SelectedValue = "1";
                ViewState["AppStatusID"] = "";
                BindAppStatusDetails();
            }
        }
        #endregion Pageload
        protected override void OnInit(EventArgs e)
        {
            ModuleID = 1;
            base.OnInit(e);
        }

        #region SupportingMethods
        public void InsUpdAppStatus()
        {
            int? AppStatusID = null;
            if (ViewState["AppStatusID"].ToString() != null && ViewState["AppStatusID"].ToString() != string.Empty)
                AppStatusID = Convert.ToInt32(ViewState["AppStatusID"]);
            string AppStatusName = txtAppStatus.Text;
            int Status = 0;
            if (chkStatus.Checked)
                Status = 1;
            int UserID =  Convert.ToInt32(Session["UserId"]);
            int OutPut = AttendanceDAC.InsUpdAppStatus(AppStatusID, AppStatusName, Status, UserID);
            if (OutPut == 1)
                AlertMsg.MsgBox(Page, "Inserted sucessfully.!");
            else if (OutPut == 2)
                AlertMsg.MsgBox(Page, "Already exists.!");
            else
                AlertMsg.MsgBox(Page, "Updated sucessfully.!");
        }
        public void BindAppStatusDetails()
        {
            int Status = 0;
            if (rbStatus.SelectedValue == "1")
                Status = 1;
            DataSet ds = AttendanceDAC.GetApplicantStatus(Status);
            if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0)
            {
                grdAppStatus.DataSource = ds;
            }
            else
            {
                grdAppStatus.DataSource = null;
                grdAppStatus.EmptyDataText = "No records found.!";
            }
            grdAppStatus.DataBind();
        }
        public string GetText()
        {
            if (rbStatus.SelectedValue == "1")
                return "InActive";
            else
                return "Active";
        }
        #endregion SupportingMethods

        #region Events
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            InsUpdAppStatus();
            BindAppStatusDetails();
        }
        protected void grdAppStatus_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Edt")
            {
                int AppStatusID = Convert.ToInt32(e.CommandArgument);
                ViewState["AppStatusID"] = AppStatusID;
                DataSet ds = AttendanceDAC.GetApplicantStatusByAppStaID(AppStatusID);
                if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0)
                {
                    txtAppStatus.Text = ds.Tables[0].Rows[0]["AppStatusName"].ToString();
                    if (ds.Tables[0].Rows[0]["Status"].ToString() == "1")
                        chkStatus.Checked = true;
                    else
                        chkStatus.Checked = false;
                }
            }
            if (e.CommandName == "Status")
            {
                int AppStatusID = Convert.ToInt32(e.CommandArgument);
                ViewState["AppStatusID"] = AppStatusID;
                GridViewRow gvr = (GridViewRow)(((Control)e.CommandSource).NamingContainer);
                LinkButton lnkbutton = (LinkButton)gvr.FindControl("lnkStatus");
                int Status = 0;
                if (lnkbutton.Text == "Active")
                    Status = 1;
                AttendanceDAC.UpdateApplicantStatus(AppStatusID, Status);
                BindAppStatusDetails();
            }
        }
        protected void rbStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindAppStatusDetails();
        }
        #endregion Events
    }
}