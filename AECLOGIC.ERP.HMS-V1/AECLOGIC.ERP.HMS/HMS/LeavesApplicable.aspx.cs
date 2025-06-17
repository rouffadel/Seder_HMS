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
    public partial class LeavesApplicable : AECLOGIC.ERP.COMMON.WebFormMaster
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
           
            if (!IsPostBack)
            {
                GetParentMenuId();
                ViewState["ApplicableID"] = "";
                BindGrid();
                BindLeaveType();
                BindProfileType();
            }
        }
        public int GetParentMenuId()
        {
            string URL = Request.Url.Segments[Request.Url.Segments.Length - 1] + Request.Url.Query;
            int RoleId = Convert.ToInt32(Session["RoleId"].ToString());
            int ModuleId = ModuleID;;

              

           DataSet ds = AttendanceDAC.GetAllowed(RoleId, ModuleId, URL);
            int MenuId = 0;
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                MenuId = Convert.ToInt32(ds.Tables[0].Rows[0]["Under"]);
                gvLeaveApplicable.Columns[4].Visible = Convert.ToBoolean(ds.Tables[0].Rows[0]["Editable"].ToString());
                gvLeaveApplicable.Columns[5].Visible = Convert.ToBoolean(ds.Tables[0].Rows[0]["Editable"].ToString());
             

            }
            return MenuId;
        }
        public void BindGrid()
        {
           DataSet ds = Leaves.GetApplicableLeavesList();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                gvLeaveApplicable.DataSource = ds;
            }
            gvLeaveApplicable.DataBind();

        }
        public void BindLeaveType()
        {

            DataSet ds = Leaves.GetLeavesList();
            ddlLeaveType.DataSource = ds.Tables[0];
            ddlLeaveType.DataTextField = "LeaveType";
            ddlLeaveType.DataValueField = "LeaveTypeID";
            ddlLeaveType.DataBind();
            ddlLeaveType.Items.Insert(0, new ListItem("--Select--", "0"));

        }
        public void BindProfileType()
        {
            DataSet ds = Leaves.GetEmpNatureList(1);
            ddlLeaveProfiler.DataSource = ds.Tables[0];
            ddlLeaveProfiler.DataTextField = "Nature";
            ddlLeaveProfiler.DataValueField = "NatureOfEmp";
            ddlLeaveProfiler.DataBind();
            ddlLeaveProfiler.Items.Insert(0, new ListItem("--Select--", "0"));
        }

        public void BindDetails(int ID)
        {
            DataSet ds = Leaves.GetApplicableLeavesDetails(ID);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                txtApplicableDays.Text = ds.Tables[0].Rows[0]["AllocatedDays"].ToString();
                ddlLeaveProfiler.SelectedValue = ds.Tables[0].Rows[0]["LeaveProfilerID"].ToString();
                ddlLeaveType.SelectedValue = ds.Tables[0].Rows[0]["LeaveTypeID"].ToString();


                if (ds.Tables[0].Rows[0]["IsActive"].ToString() == "1")
                {
                    chkStatus.Checked = true;
                }
                else
                {
                    chkStatus.Checked = false;
                }
            }

        }
        protected void gvLeaveApplicable_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int ID = Convert.ToInt32(e.CommandArgument);
            ViewState["ApplicableID"] = ID;
            if (e.CommandName == "Edt")
            {
                BindDetails(ID);
            }
            else
            {

            }
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            int ApplicableID = 0;
            int Status = 1;
            if (ViewState["ApplicableID"].ToString() != null && ViewState["ApplicableID"].ToString() != string.Empty)
            {
                ApplicableID = Convert.ToInt32(ViewState["ApplicableID"].ToString());
            }
            if (chkStatus.Checked == false)
            {
                Status = 0;
            }
            Leaves.InsUpdateApplicableLeaves(ApplicableID, Convert.ToInt32(ddlLeaveType.SelectedItem.Value), Convert.ToInt32(ddlLeaveProfiler.SelectedItem.Value), Convert.ToInt32(txtApplicableDays.Text), Convert.ToInt32( Convert.ToInt32(Session["UserId"]).ToString()), Status);
            if (ApplicableID == 0)
            {
                AlertMsg.MsgBox(Page, "Done.! ");
            }
            else
            {
                AlertMsg.MsgBox(Page, "Updated");
            }
            BindGrid();
            Clear();
        }
        public void Clear()
        {
            ddlLeaveProfiler.SelectedIndex = 0;
            ddlLeaveType.SelectedIndex = 0;
            txtApplicableDays.Text = "";
        }
    }
}
