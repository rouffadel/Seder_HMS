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
    public partial class TypeOfLeaves : AECLOGIC.ERP.COMMON.WebFormMaster
    {
        Leaves objLeave = new Leaves();
        int mid = 0;
        bool viewall;
        string menuname;
        string menuid;
        protected override void OnInit(EventArgs e)
        {
            ModuleID = 1;
            base.OnInit(e);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
           try{ if (Convert.ToInt32(Request.QueryString["key"]) == 1)
            {
                tblNew.Visible = true;
                tblEdit.Visible = false;
            }
            if (!IsPostBack)
            {
                GetParentMenuId();
                ViewState["LeaveTypeID"] = "";
                if (Convert.ToInt32(Request.QueryString["key"]) == 0)
                {
                    tblEdit.Visible = true;
                    BindGrid();
                }
            }
           }
           catch (Exception ex)
           {
               clsErrorLog.HMSEventLog(ex, "TypeOfleaves", "Page_Load", "001");
           }
        }
        public int GetParentMenuId()
        {
            string URL = Request.Url.Segments[Request.Url.Segments.Length - 1] + Request.Url.Query;
            int RoleId = Convert.ToInt32(Session["RoleId"].ToString());
            int ModuleId = ModuleID;;
          DataSet  ds = AttendanceDAC.GetAllowed(RoleId, ModuleId, URL);
            int MenuId = 0;
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                MenuId = Convert.ToInt32(ds.Tables[0].Rows[0]["Under"]);
                gvLeaveProfile.Columns[2].Visible = Convert.ToBoolean(ds.Tables[0].Rows[0]["Editable"].ToString());
                btnSubmit.Enabled = (bool)ds.Tables[0].Rows[0]["Editable"];
            }
            return MenuId;
        }
        public void BindGrid()
        {
         try{ DataSet      ds = Leaves.GetTypeofLeavesList_By_Status(Convert.ToInt32(rblstStatus.SelectedValue));
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                gvLeaveProfile.DataSource = ds;
            }
            gvLeaveProfile.DataBind();
         }
         catch (Exception ex)
         {
             clsErrorLog.HMSEventLog(ex, "TypeOfleaves", "BindGrid", "002");
         }
        }
        public void BindDetails(int ID)
        {
         try{   tblEdit.Visible = false;
            tblNew.Visible = true;
            DataSet ds = Leaves.GetTypeofLeavesDetails(ID);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                txtName.Text = ds.Tables[0].Rows[0]["Name"].ToString();
                txtSName.Text = ds.Tables[0].Rows[0]["ShortName"].ToString();
                ddlPayType.SelectedValue = ds.Tables[0].Rows[0]["Status"].ToString();
                chkisAccruable.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["isAccruable"]);
                chkC_Fwd.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["CFwd"]);
                chkisEncashable.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["isEncashable"]);
                ddlGender.SelectedValue = ds.Tables[0].Rows[0]["Gender"].ToString();
                txtPayCoeff.Text = ds.Tables[0].Rows[0]["PayCoeff"].ToString();
                txtMinServiceYrs.Text = ds.Tables[0].Rows[0]["MinServiceYrs"].ToString();
                txtMaxEntitlementYr.Text = ds.Tables[0].Rows[0]["MaxEntitlementYr"].ToString();
                txtLabourLawRef.Text = ds.Tables[0].Rows[0]["LabourLawRef"].ToString();
                txtRemarks.Text = ds.Tables[0].Rows[0]["Remarks"].ToString();
                if (ds.Tables[0].Rows[0]["IsActive"].ToString() == "True")
                    chkStatus.Checked = true;
                else
                    chkStatus.Checked = false;
            }
         }
         catch (Exception ex)
         {
             clsErrorLog.HMSEventLog(ex, "TypeOfleaves", "BindDetails", "003");
         }
        }
        protected void gvLeaveProfile_RowCommand(object sender, GridViewCommandEventArgs e)
        {
         try{   int ID = Convert.ToInt32(e.CommandArgument);
            ViewState["LeaveTypeID"] = ID;
            if (e.CommandName == "Edt")
            {
                BindDetails(ID);
            }
            else
            {
            }
         }
         catch (Exception ex)
         {
             clsErrorLog.HMSEventLog(ex, "TypeOfleaves", "gvLeaveProfile_RowCommand", "004");
         }
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                      int LeaveTypeID = 0;
                            int Status = 1;
                            if (ViewState["LeaveTypeID"].ToString() != null && ViewState["LeaveTypeID"].ToString() != string.Empty)
                            {
                                LeaveTypeID = Convert.ToInt32(ViewState["LeaveTypeID"].ToString());
                            }
                            if (chkStatus.Checked == false)
                            {
                                Status = 0;
                            }
                            Leaves.InsUpdateTypeofLeaves(LeaveTypeID, txtName.Text.Trim(), txtSName.Text, Convert.ToInt32( Convert.ToInt32(Session["UserId"]).ToString()), Status, Convert.ToInt32(ddlPayType.SelectedItem.Value),
                                Convert.ToInt32(ddlGender.SelectedValue),
                                Convert.ToInt32(chkisAccruable.Checked),
                                Convert.ToInt32(chkC_Fwd.Checked),
                                Convert.ToDecimal(txtMinServiceYrs.Text),
                                Convert.ToDecimal(txtMaxEntitlementYr.Text),
                                txtLabourLawRef.Text, txtRemarks.Text,
                                 Convert.ToInt32(chkisEncashable.Checked),
                                Convert.ToDecimal(txtPayCoeff.Text)
                                );
                            if (LeaveTypeID == 0)
                            {
                                AlertMsg.MsgBox(Page, "Done.! ");
                            }
                            else
                            {
                                AlertMsg.MsgBox(Page, "Updated");
                            }
                            BindGrid();
                            Clear();
                            tblEdit.Visible = true;
                            tblNew.Visible = false;
            }
            catch (Exception TypeOfLeaves)
            {
                AlertMsg.MsgBox(Page, TypeOfLeaves.Message.ToString(),AlertMsg.MessageType.Error);
            }
        }
        public void Clear()
        {
            txtName.Text = "";
            txtSName.Text = "";
            ViewState["LeaveTypeID"] = "";
        }
        protected void lnkAdd_Click(object sender, EventArgs e)
        {
            tblNew.Visible = true;
            tblEdit.Visible = false;
        }
        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            tblEdit.Visible = true;
            tblNew.Visible = false;
        }
        protected void rblstStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGrid();
        }
    }
}