using AECLOGIC.HMS.BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
namespace AECLOGIC.ERP.HMS.HMS
{
    public partial class ShiftCongifbyNature : AECLOGIC.ERP.COMMON.WebFormMaster
    {
        int mid = 0;
        bool viewall;
        string menuname;
        string menuid;
        bool Editable;
        protected override void OnInit(EventArgs e)
        {
            ModuleID = 1;
            base.OnInit(e);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                lblStatus.Text = String.Empty;
                if (!IsPostBack)
                {
                    GetParentMenuId();
                    LoadDropdowns();
                    if (Request.QueryString.Count > 0)
                    {
                        tblTimeView.Visible = false;
                        tblshiftEdit.Visible = true;
                    }
                    else
                    {
                        tblTimeView.Visible = true;
                        tblshiftEdit.Visible = false;
                        BindGrid();
                        ViewState["SID"] = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                clsErrorLog.HMSEventLog(ex, "ShiftCongifbyNature", "Page_Load", "001");
            }
        }
        public int GetParentMenuId()
        {
            string URL = Request.Url.Segments[Request.Url.Segments.Length - 1] + Request.Url.Query;
            int RoleId = Convert.ToInt32(Session["RoleId"].ToString());
            int ModuleId = ModuleID; ;
            DataSet ds = AttendanceDAC.GetAllowed(RoleId, ModuleId, URL);
            int MenuId = 0;
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                MenuId = Convert.ToInt32(ds.Tables[0].Rows[0]["Under"]);
                Editable = (bool)ds.Tables[0].Rows[0]["Editable"];
                btnSave.Enabled = Editable;
            }
            return MenuId;
        }
        private void BindGrid()
        {
            try
            {
                DataSet ds = AttendanceDAC.HR_GetShiftConfigration(Convert.ToInt32(ddlShift.SelectedValue), Convert.ToInt32(ddlEmpNature.SelectedValue), Convert.ToInt32(ddlWks.SelectedValue),  Convert.ToInt32(Session["UserId"]));
                gvTimeView.DataSource = ds;
                gvTimeView.DataBind();
            }
            catch (Exception ex)
            {
                clsErrorLog.HMSEventLog(ex, "ShiftCongifbyNature", "BindGrid", "002");
            }
        }
        protected void gvTimeView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int ID = Convert.ToInt32(e.CommandArgument);
                ViewState["SID"] = ID;
                if (e.CommandName == "Edt")
                {
                    tblTimeView.Visible = false;
                    tblshiftEdit.Visible = true;
                    DataSet ds = AttendanceDAC.HR_GetShiftConfigration_ByID(ID);
                    txtName.Text = ds.Tables[0].Rows[0]["DayName"].ToString();
                    txtName.ReadOnly = true;
                    #region InTime
                    string ITime = ds.Tables[0].Rows[0]["InTime"].ToString();// 7:45 2
                    string[] ITM = ITime.Split(new[] { ':' });
                    string TT = ITM[1];
                    ddlInTime.SelectedValue = Convert.ToString(ITM[0]);
                    txtITMinutes.Text = TT.Substring(0, 2);
                    ddlInTimeFormat.SelectedItem.Text = TT.Substring(3, 2);
                    #endregion InTime
                    #region OutTime
                    string OTime = ds.Tables[0].Rows[0]["OutTime"].ToString();
                    string[] OTM = OTime.Split(new[] { ':' });
                    string OT = OTM[1];
                    ddlOutTime.SelectedValue = Convert.ToString(OTM[0]);
                    txtOTMinutes.Text = OT.Substring(0, 2);
                    ddlOutTimeFormat.SelectedItem.Text = OT.Substring(3, 2);
                    #endregion OutTime
                }
            }
            catch (Exception ex)
            {
                clsErrorLog.HMSEventLog(ex, "ShiftCongifbyNature", "gvTimeView_RowCommand", "003");
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            { 
                string InTime;
                string InMints;
                string OutTime;
                string OutMints;
                string InFormate;
                string OutFormate;
                int IntimeMin = Convert.ToInt32(txtITMinutes.Text);
                int OuttimeMin = Convert.ToInt32(txtOTMinutes.Text);
                if (IntimeMin > 59 || IntimeMin < 0)
                {
                   // AlertMsg.MsgBox(Page, "Minutes can take 0 to 59 Only.!");
                    lblStatus.Text = "Minutes can take 0 to 59 Only.!";
                    lblStatus.ForeColor = System.Drawing.Color.Red;
                    return;
                }
                if (OuttimeMin > 59 || OuttimeMin < 0)
                {
                   // AlertMsg.MsgBox(Page, "Minutes can take 0 to 59 Only.!");
                    lblStatus.Text = "Minutes can take 0 to 59 Only.!";
                    lblStatus.ForeColor = System.Drawing.Color.Red;
                    return;
                }
                if (ddlInTime.SelectedIndex != 0)
                {
                    InTime = Convert.ToString(ddlInTime.SelectedValue);
                    InMints = txtITMinutes.Text;
                    InFormate = ddlInTimeFormat.SelectedItem.Text;
                }
                else
                {
                 //    AlertMsg.MsgBox(Page, "Select InTime");
                    lblStatus.Text = "Select InTime";
                    lblStatus.ForeColor = System.Drawing.Color.Red;
                    return;
                }
                if (ddlOutTime.SelectedIndex != 0)
                {
                    OutTime = Convert.ToString(ddlOutTime.SelectedValue);
                    OutMints = txtOTMinutes.Text;
                    OutFormate = ddlOutTimeFormat.SelectedItem.Text;
                }
                else
                {
                    // AlertMsg.MsgBox(Page, "Select OutTime");
                    lblStatus.Text = "Select OutTime";
                    lblStatus.ForeColor = System.Drawing.Color.Red;
                    return;
                }
                int OutPut = AttendanceDAC.HR_UpdateShiftConfigration(Convert.ToInt32(ViewState["SID"]), InTime + ":" + InMints + " " + InFormate, OutTime + ":" + OutMints + " " + OutFormate,  Convert.ToInt32(Session["UserId"]));
                if (OutPut == 1)
                {
                    // AlertMsg.MsgBox(Page, "Done.!");
                    lblStatus.Text = "Done.!";
                    lblStatus.ForeColor = System.Drawing.Color.Green;
                }
                else if (OutPut == 2)
                {
                    // AlertMsg.MsgBox(Page, "Already Exists.!");
                    lblStatus.Text = "Already Exists.!";
                    lblStatus.ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    // AlertMsg.MsgBox(Page, "Updated.!");
                    lblStatus.Text = "Updated.!";
                    lblStatus.ForeColor = System.Drawing.Color.Green;
                }
                tblTimeView.Visible = true;
                tblshiftEdit.Visible = false;
                BindGrid();
            }
            catch (Exception ex)
            {
                clsErrorLog.HMSEventLog(ex, "ShiftCongifbyNature", "btnSave_Click", "004");
            }
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                BindGrid();
            }
            catch (Exception ex)
            {
                clsErrorLog.HMSEventLog(ex, "ShiftCongifbyNature", "btnSearch_Click", "004");
            }
        }
        void LoadDropdowns()
        {
            try
            {
                DataSet ds = Leaves.GetEmpNatureList(1);
                ddlEmpNature.DataSource = ds;
                ddlEmpNature.DataTextField = "Nature";
                ddlEmpNature.DataValueField = "NatureOfEmp";
                ddlEmpNature.DataBind();
            }
            catch (Exception ex)
            {
                clsErrorLog.HMSEventLog(ex, "ShiftCongifbyNature", "LoadDropdowns", "005");
            }
            try
            {
                DataSet ds = AttendanceDAC.HR_GetNoofWeeks();
                ddlWks.DataSource = ds;
                ddlWks.DataTextField = "WeekName";
                ddlWks.DataValueField = "WeekNo";
                ddlWks.DataBind();
            }
            catch (Exception ex)
            {
                clsErrorLog.HMSEventLog(ex, "ShiftCongifbyNature", "LoadDropdowns", "006");
            }
            try
            {
                DataSet ds = AttendanceDAC.HR_GetShiftTimings(0);
                ddlShift.DataSource = ds;
                ddlShift.DataTextField = "Name";
                ddlShift.DataValueField = "ShiftID";
                ddlShift.DataBind();
            }
            catch (Exception ex)
            {
                clsErrorLog.HMSEventLog(ex, "ShiftCongifbyNature", "LoadDropdowns", "006");
            }
        }
    }
}