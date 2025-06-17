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
    public partial class LeaveProfile : AECLOGIC.ERP.COMMON.WebFormMaster
    {
        Leaves objLeave = new Leaves();
        protected override void OnInit(EventArgs e)
        {
            ModuleID = 1;
            base.OnInit(e);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetParentMenuId();
                BindGrid();
                ViewState["NatureID"] = "";
                mainview.ActiveViewIndex = 1;
                if (Request.QueryString.Count > 0)
                {
                    int id = Convert.ToInt32(Request.QueryString["key"].ToString());
                    if (id == 1)
                        mainview.ActiveViewIndex = 0;
                }
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
        protected void rblstStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGrid();
        }
        public void BindGrid()
        {
            mainview.ActiveViewIndex = 1;
           DataSet ds = Leaves.GetEmpNatureList(Convert.ToInt32(rblstStatus.SelectedValue));
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                gvLeaveProfile.DataSource = ds;
            }
            gvLeaveProfile.DataBind();
        }
        public void BindDetails(int ID)
        {
            DataSet ds = Leaves.GetEmpNatureDetails(ID);
            mainview.ActiveViewIndex = 0;
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                txtNature.Text = ds.Tables[0].Rows[0]["Nature"].ToString();
                txtdays.Text = ds.Tables[0].Rows[0]["NoofWD"].ToString();
                if (ds.Tables[0].Rows[0]["IsActive"].ToString() == "True")
                {
                    chkStatus.Checked = true;
                }
                else
                {
                    chkStatus.Checked = false;
                }
            }
        }
        protected void gvLeaveProfile_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int ID = Convert.ToInt32(e.CommandArgument);
            ViewState["NatureID"] = ID;
            if (e.CommandName == "Edt")
            {
                BindDetails(ID);
            }
            else
            {
                try
                {
                    bool Status;
                    GridViewRow gvr = (GridViewRow)(((Control)e.CommandSource).NamingContainer);
                    LinkButton lnkDel = (LinkButton)gvr.FindControl("lnkDel");
                    if (lnkDel.Text == "Active")
                    {
                        Status = true;
                    }
                    else
                    {
                        Status = false;
                    }
                    AttendanceDAC.UpdateEmpNatureStatus(ID, Status);
                    BindGrid();
                    AlertMsg.MsgBox(Page, "Completed!");
                }
                catch (Exception DeptHead)
                {
                    AlertMsg.MsgBox(Page, DeptHead.Message.ToString(),AlertMsg.MessageType.Error);
                }
            }
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                int NatureID = 0;
                int Status = 1;
                if (ViewState["NatureID"].ToString() != null && ViewState["NatureID"].ToString() != string.Empty)
                {
                    NatureID = Convert.ToInt32(ViewState["NatureID"].ToString());
                }
                if (chkStatus.Checked == false)
                {
                    Status = 0;
                }
                NatureID = Leaves.InsUpdateEmpNature(NatureID, txtNature.Text.Trim(), txtdays.Text, Convert.ToInt32( Convert.ToInt32(Session["UserId"]).ToString()), Status);
                if (NatureID == 1)
                {
                    AlertMsg.MsgBox(Page, "Inserted Successfully!");
                    BindGrid();
                    Clear();
                }
                else if (NatureID == 2)
                    AlertMsg.MsgBox(Page, "Already Existed!");
                else if (NatureID == 3)
                {
                    AlertMsg.MsgBox(Page, "Updated Successfully!");
                    BindGrid();
                    Clear();
                }
                else
                    AlertMsg.MsgBox(Page, "Sorry! Operation not get completed at the moment! try again or contact");
            }
            catch (Exception EmpNature)
            {
                AlertMsg.MsgBox(Page, EmpNature.Message.ToString(),AlertMsg.MessageType.Error);
            }
        }
        public string GetText()
        {
            if (rblstStatus.SelectedValue == "1")
            {
                return "Inactive";
            }
            else
            {
                return "Active";
            }
        }
        public void Clear()
        {
            txtNature.Text = "";
            txtdays.Text = "";
            ViewState["NatureID"] = "";
        }
        protected void lnkAdd_Click(object sender, EventArgs e)
        {
            mainview.ActiveViewIndex = 0;
        }
        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            mainview.ActiveViewIndex = 0;
        }
    }
}