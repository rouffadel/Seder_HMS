using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DataAccessLayer;
namespace AECLOGIC.ERP.HMS
{
    public partial class Reminder : AECLOGIC.ERP.COMMON.WebFormMaster
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ViewState["SRNID"] = "";
                BindGrid();
            }
        }
        protected override void OnInit(EventArgs e)
        {
            ModuleID = 1;
            base.OnInit(e);
        }
        private void BindGrid()
        {
            DataSet ds = dlSRN.HR_GetReminders(Convert.ToInt32(Application["ModuleId"]));
            gvView.DataSource = ds;
            gvView.DataBind();
        }
        public string PONavigateUrl(string POID)
        {
            bool Fals = false;
            return "javascript:return window.open('ProPurchaseOrderPrint.aspx?id=" + POID + "&PON=" + 1 + "&tot=" + Fals + "' , '_blank')";
        }
        protected void gvView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Reg")
            {
                Response.Redirect("SRN.aspx?ID=" + e.CommandArgument + "&ReqType=" + 1);
            }
            if (e.CommandName == "Edt")
            {
                tblAdd.Visible = true;
                tblView.Visible = false;
                DataSet ds = dlSRN.HR_GetReminderByID(Convert.ToInt32(e.CommandArgument));
                if (ds.Tables.Count == 1)
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        txtValidFrom.Text = ds.Tables[0].Rows[0]["ValidUpto"].ToString();
                        txtRemindDays.Text = ds.Tables[0].Rows[0]["RemindStart"].ToString();
                        txtReminder.Text = ds.Tables[0].Rows[0]["RemindText"].ToString();
                    }
            }
            if (e.CommandName == "ReNew")
            {
                Response.Redirect("SRN.aspx?ID=" + e.CommandArgument + "&ReqType=" + 2);
            }
            if (e.CommandName == "Del")
            {
                DataSet ds = dlSRN.HR_DelReminder(Convert.ToInt32(e.CommandArgument));
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        BindGrid();
                        AlertMsg.MsgBox(Page, "Done..!");
                    }
                    else
                    {
                        AlertMsg.MsgBox(Page, "WO is not closed yet..Not Possible to Delete.!");
                    }
                }
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                int RID = 0;
                DateTime? ValidFrom = null;
                if (txtValidFrom.Text.Trim() != null)
                    ValidFrom = CODEUtility.ConvertToDate(txtValidFrom.Text.Trim(), DateFormat.DayMonthYear);
                DateTime? ValidUpto = null;
                if (txtValidUpto.Text.Trim() != null)
                    ValidUpto = CODEUtility.ConvertToDate(txtValidUpto.Text.Trim(), DateFormat.DayMonthYear);
                DateTime? DueDate = null;
                if (txtDueDate.Text.Trim() != null)
                    DueDate = CODEUtility.ConvertToDate(txtDueDate.Text.Trim(), DateFormat.DayMonthYear);
                int RemindDays = Convert.ToInt32(txtRemindDays.Text);
                try
                {
                    dlSRN.HR_InsUpReminder(RID, null, ValidFrom, ValidUpto, RemindDays, txtReminder.Text, null, Convert.ToInt32(Application["ModuleId"]), DueDate);
                    BindGrid();
                    reset();
                    AlertMsg.MsgBox(Page, "Save Done!");
                }
                catch (Exception Reminder)
                {
                    AlertMsg.MsgBox(Page, Reminder.Message.ToString(),AlertMsg.MessageType.Error);
                }
            }
            catch (Exception ex)
            {
                AlertMsg.MsgBox(Page, ex.Message.ToString(),AlertMsg.MessageType.Error);
            }
        }
        protected void reset()
        {
            txtValidFrom.Text = "";
            txtValidUpto.Text = "";
            txtDueDate.Text = "";
            txtRemindDays.Text = "";
            txtReminder.Text = "";
        }
    }
}