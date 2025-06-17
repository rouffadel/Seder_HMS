using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using AECLOGIC.HMS.BLL;
using AECLOGIC.ERP.HMS.HRClasses;
namespace AECLOGIC.ERP.HMS
{
    public partial class LeaveEntitlement : AECLOGIC.ERP.COMMON.WebFormMaster
    {
        int mid = 0;
        bool viewall;
        string menuname;
        string menuid; int Leavetype = 0; int Grade = 0;
        protected override void OnInit(EventArgs e)
        {
            ModuleID = 1;
            base.OnInit(e);
        }
        protected void ddlLeaveType_SelectedIndexChanged(object sender, EventArgs e)
        {
            // throw new NotImplementedException();
            try
            {
                Leavetype = Convert.ToInt32(ddlLeaveType.SelectedValue);
                int LEId = 0;
                if (ViewState["LEId"].ToString() != null && ViewState["LEId"].ToString() != string.Empty)
                    LEId = Convert.ToInt32(ViewState["LEId"].ToString());
                if (LEId == 0)
                {
                    DataSet ds = Leaves.GetTypeofLeavesDetails(Convert.ToInt32(ddlLeaveType.SelectedValue));
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        chkisAccruable.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["isAccruable"]);
                        chkC_Fwd.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["CFwd"]);
                        chkisEncashable.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["isEncashable"]);
                        ddlGender.SelectedValue = ds.Tables[0].Rows[0]["Gender"].ToString();
                        txtPayCoeff.Text = ds.Tables[0].Rows[0]["PayCoeft"].ToString();
                        txtMinDaysofWork.Text = (Convert.ToDecimal(ds.Tables[0].Rows[0]["MinServiceYrs"]) * 360).ToString();
                        txtMinService.Text = (Convert.ToDecimal(ds.Tables[0].Rows[0]["MinServiceYrs"])).ToString();
                        txtMaxLeaveElg.Text = (Convert.ToDecimal(ds.Tables[0].Rows[0]["MaxEntitlementYr"]) / 12).ToString();
                        txtxmaxLeaveElgyear.Text = ds.Tables[0].Rows[0]["MaxEntitlementYr"].ToString();
                        BindLeavesMaster();
                        BindGradeConfig();
                    }
                }
            }
            catch (Exception ex)
            {
                clsErrorLog.HMSEventLog(ex, "leaveEntitlements", "ddlLeaveType_SelectedIndexChanged", "004");
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToInt32(Request.QueryString["key"]) == 1)
                {
                    this.Title = "Add Entitlement";
                    tblNew.Visible = true;
                    pnltblNew.Visible = true;
                    tblEdit.Visible = false;
                    gvLeaveEL.Visible = false;
                    BindLeavesMaster();
                    BindGradeConfig();
                }
                if (!IsPostBack)
                {
                    GetParentMenuId();
                    ViewState["LEId"] = "";
                    if (Convert.ToInt32(Request.QueryString["key"]) == 0)
                    {
                        tblEdit.Visible = true;
                        gvLeaveEL.Visible = true;
                    }
                    BindEmpNture();
                    BindGrid();
                    BindLeaveTypes();
                    BindAllotmentCycle();
                    LoadByPaybledays();
                }
            }
            catch (Exception ex)
            {
                clsErrorLog.HMSEventLog(ex, "LeaveEntitlements", "Page_Load", "001");
            }
        }
        void BindLeavesMaster()
        {
            try
            {
                if (ddlLeaveType.SelectedIndex != -1)
                    Leavetype = Convert.ToInt32(ddlLeaveType.SelectedValue);
                else
                    Leavetype = 0;
               DataSet   ds = Leaves.GetTypeofLeavesList_By_StatusLT(Leavetype);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    gvLeave.DataSource = ds;
                }
                gvLeave.DataBind();
            }
            catch (Exception ex)
            {
                clsErrorLog.HMSEventLog(ex, "TypeOfleaves", "BindGrid", "002");
            }
        }
        public void BindGradeConfig()
        {
            gvEMPTrade.Visible = true;
            bool Status = false;
            if (rblDesg.SelectedValue == "1")
            {
                Status = true;
            }
            if (ddlTrade.SelectedIndex != -1)
                Grade = Convert.ToInt32(ddlTrade.SelectedValue);
            else
                Grade = 0;
          DataSet  ds = clempGradesConfig.HR_GetEMPGradesDetailsLE(Status, Grade);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                gvEMPTrade.DataSource = ds;
            }
            gvEMPTrade.DataBind();
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
                gvLeaveEL.Columns[5].Visible = Convert.ToBoolean(ds.Tables[0].Rows[0]["Editable"].ToString());
                btnSave.Enabled = (bool)ds.Tables[0].Rows[0]["Editable"];
            }
            return MenuId;
        }
        public void BindGrid()
        {
            DataSet  ds = Leaves.GetEntitlementList(Convert.ToInt32(ddlEmpGradeSearch.SelectedValue));
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                gvLeaveEL.DataSource = ds;
            }
            gvLeaveEL.DataBind();
            ddlTrade.Enabled = ddlLeaveType.Enabled = true;
            LoadByPaybledays();
        }
        public void BindEmpNture()
        {
            DataSet ds = Leaves.HR_GetEMPGradesDetailsforDDL();
            ddlEmpGradeSearch.DataSource = ds;
            ddlEmpGradeSearch.DataTextField = "Name";
            ddlEmpGradeSearch.DataValueField = "ID";
            ddlEmpGradeSearch.DataBind();
            //ddlEmpGradeSearch.Items.Insert(0, new ListItem("--Select--", "0"));
            ddlTrade.DataSource = ds;
            ddlTrade.DataValueField = "ID";
            ddlTrade.DataTextField = "Name";
            ddlTrade.DataBind();
            //ddlEmpNature.Items.Insert(0, new ListItem("--Select--", "0"));
            try
            {
                if (Request.QueryString.Count > 0)
                {
                    string st = Request.QueryString["G"].ToString();
                    ddlEmpGradeSearch.ClearSelection(); //making sure the previous selection has been cleared
                    ddlEmpGradeSearch.Items.FindByText(st).Selected = true;
                    // ddlEmpGradeSearch.fi
                    // int LID = Convert.ToInt32(Request.QueryString["LID"]);
                }
            }
            catch { }
        }
        public void BindLeaveTypes()
        {
            DataSet ds = Leaves.GetTypeofLeavesList();
            ddlLeaveType.DataSource = ds;
            ddlLeaveType.DataTextField = "Name";
            ddlLeaveType.DataValueField = "LeaveType";
            ddlLeaveType.DataBind();
            ddlLeaveType.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        public void BindAllotmentCycle()
        {
            DataSet ds = Leaves.GetLeaveAllotmentList();
            ddlAllotmentCycle.DataSource = ds;
            ddlAllotmentCycle.DataTextField = "Allotment";
            ddlAllotmentCycle.DataValueField = "AllotmentCycle";
            ddlAllotmentCycle.DataBind();
            ddlAllotmentCycle.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        public void BindDetails(int LEId)
        {
            tblEdit.Visible = false;
            tblNew.Visible = true;
            pnltblNew.Visible = true;
            DataSet ds = Leaves.GetLeaveEntitlementDetails(LEId);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                txtMaxLeaveElg.Text = ds.Tables[0].Rows[0]["MaxLeaveEligibility"].ToString();
                txtMinDaysofWork.Text = ds.Tables[0].Rows[0]["MinDaysOfWork"].ToString();
                txtMinService.Text = ds.Tables[0].Rows[0]["MinService"].ToString();
                ddlAllotmentCycle.SelectedValue = ds.Tables[0].Rows[0]["AllotmentCycle"].ToString();
                try { ddlLeaveType.SelectedValue = ds.Tables[0].Rows[0]["LeaveType"].ToString(); }
                catch { }
                ddlTrade.SelectedValue = ds.Tables[0].Rows[0]["NatureOfEmp"].ToString();
                ddlTrade.Enabled = ddlLeaveType.Enabled = false;
                txtxmaxLeaveElgyear.Text = ds.Tables[0].Rows[0]["MaxLeaveElgYear"].ToString();
                try
                {
                    txtMaxLeaveElg.Text = (Convert.ToDecimal(txtxmaxLeaveElgyear.Text) / 12).ToString("N2");
                }
                catch { }
                try
                {
                    chkC_Fwd.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["IsCFrwd"]);
                    chkisEncashable.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["isEncashable"]);
                    ddlGender.SelectedValue = ds.Tables[0].Rows[0]["Gender"].ToString();
                    ddlPayType.SelectedValue = ds.Tables[0].Rows[0]["PayType"].ToString();
                    txtPayCoeff.Text = ds.Tables[0].Rows[0]["PayCoeff"].ToString();
                    txtMinService.Text = ds.Tables[0].Rows[0]["MinService"].ToString();
                    chkisAccruable.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["IsAccure"]);
                    BindLeavesMaster();
                    BindGradeConfig();
                }
                catch { }
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToDecimal(txtxmaxLeaveElgyear.Text) <= 365)
                {
                    int LEId = 0;
                    if (ViewState["LEId"].ToString() != null && ViewState["LEId"].ToString() != string.Empty)
                    {
                        LEId = Convert.ToInt32(ViewState["LEId"].ToString());
                    }
                    try
                    {
                        txtMaxLeaveElg.Text = (Convert.ToDecimal(txtxmaxLeaveElgyear.Text) / 12).ToString("N2");
                    }
                    catch { }
                    Leaves.InsUpdateLeaveEntitlement(LEId, Convert.ToInt32(ddlLeaveType.SelectedValue), Convert.ToInt32(ddlTrade.SelectedValue),
                        Convert.ToInt32(ddlAllotmentCycle.SelectedValue), Convert.ToDecimal(txtMaxLeaveElg.Text),0,
                        Convert.ToDecimal(txtxmaxLeaveElgyear.Text)
                        , Convert.ToInt32(ddlGender.SelectedValue),
                    Convert.ToInt32(chkisAccruable.Checked),
                    Convert.ToInt32(chkC_Fwd.Checked),
                     Convert.ToInt32(chkisEncashable.Checked),
                    Convert.ToDecimal(txtPayCoeff.Text),
                    Convert.ToDecimal(txtMinService.Text),
                    Convert.ToInt32(ddlPayType.SelectedValue)
                        );
                    if (LEId == 0)
                    {
                        AlertMsg.MsgBox(Page, "Done.! ");
                    }
                    else
                    {
                        AlertMsg.MsgBox(Page, "Updated");
                    }
                    Response.Redirect("LeaveEntitlement.aspx");
                }
                else
                {
                    AlertMsg.MsgBox(Page, "Max eligibility peryear lessthan or equal to 365");
                }
            }
            catch (Exception LeaveEnti)
            {
                AlertMsg.MsgBox(Page, LeaveEnti.Message.ToString(),AlertMsg.MessageType.Error);
            }
        }
        protected void gvLeaveProfile_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int ID = Convert.ToInt32(e.CommandArgument);
                ViewState["LEId"] = ID;
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
                clsErrorLog.HMSEventLog(ex, "LeaveEntitlements", "gvLeaveProfile_RowCommand", "003");
            }
        }
        public void Clear()
        {
            txtMaxLeaveElg.Text = "";
            txtMinDaysofWork.Text = "";
            ddlAllotmentCycle.SelectedIndex = 0;
            ddlLeaveType.SelectedIndex = 0;
            ddlTrade.SelectedIndex = 0;
            ViewState["LEId"] = "";
        }
        protected void lnkAdd_Click(object sender, EventArgs e)
        {
            tblNew.Visible = true;
            pnltblNew.Visible = true;
            tblEdit.Visible = false;
        }
        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            tblEdit.Visible = true;
            tblNew.Visible = false;
            pnltblNew.Visible = false;
        }
        protected void ddlEmpNature_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                BindGrid();
            }
            catch (Exception ex)
            {
                clsErrorLog.HMSEventLog(ex, "EMPGradeConfig", "btnSave_Click", "002");
            }
        }
        protected void rblDesg_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGradeConfig();
        }
        protected void ddlTrade_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGradeConfig();
        }
        protected void txtxmaxLeaveElgyear_TextChanged(object sender, EventArgs e)
        {
            try
            {
                txtMaxLeaveElg.Text = (Convert.ToDecimal(txtxmaxLeaveElgyear.Text) / 12).ToString("N2");
            }
            catch { }
        }
        protected void chkByworkingdays_CheckedChanged(object sender, EventArgs e)
        {
            //UpdatePaybledays();
        }
        void UpdatePaybledays()
        {
            try
            {
                if (chkByworkingdays.Checked)
                    Leaves.sh_updateOptionByid(6900, "1");
                else
                    Leaves.sh_updateOptionByid(6900, "0");
                //chkByworkingdays.Checked = blval;
            }
            catch { }
        }
        protected void btnSavePD_Click(object sender, EventArgs e)
        {
            try
            {
                UpdatePaybledays();
                AlertMsg.MsgBox(Page, "Done");
            }
            catch (Exception LeaveEnti)
            {
                AlertMsg.MsgBox(Page, LeaveEnti.Message.ToString(),AlertMsg.MessageType.Error);
            }
        }
        void LoadByPaybledays()
        {
            try
            {
                Boolean blval = true;
                DataTable dt = Leaves.sh_getoptionbyid(6900).Tables[0];
                if (dt.Rows[0][0].ToString() == "1")
                    blval = true;
                else
                    blval = false;
                chkByworkingdays.Checked = blval;
            }
            catch { }
        }
    }
}
