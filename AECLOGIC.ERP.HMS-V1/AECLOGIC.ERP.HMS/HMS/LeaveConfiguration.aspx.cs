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
    public partial class LeaveConfiguration : AECLOGIC.ERP.COMMON.WebFormMaster
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
                ViewState["LeaveTypeID"] = null;
                tblEdit.Visible = true;

                BindGrid();

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
               
                gvLeaveConfig.Columns[2].Visible = Convert.ToBoolean(ds.Tables[0].Rows[0]["Editable"].ToString());
                gvLeaveConfig.Columns[3].Visible = Convert.ToBoolean(ds.Tables[0].Rows[0]["Editable"].ToString());
               
            }
            return MenuId;
        }
        public void BindGrid()
        {
          DataSet  ds = Leaves.GetLeavesList();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                gvLeaveConfig.DataSource = ds;
            }
            gvLeaveConfig.DataBind();
        }
        public void BindDetails(int ID)
        {
            tblEdit.Visible = false;
            tblNew.Visible = true;
            DataSet ds = Leaves.GetLeavesDetails(ID);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                txtLeaveType.Text = ds.Tables[0].Rows[0]["LeaveType"].ToString();
                txtNoOfDays.Text = ds.Tables[0].Rows[0]["Days"].ToString();
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
        protected void gvLeaveConfig_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int ID = Convert.ToInt32(e.CommandArgument);
            ViewState["LeaveTypeID"] = ID;
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
            int LeaveTypeID = 0;
            int Status = 1;
            if (ViewState["LeaveTypeID"] != null)
            {

                LeaveTypeID = Convert.ToInt32(ViewState["LeaveTypeID"].ToString());
            }
            if (chkStatus.Checked == false)
            {
                Status = 0;

            }
            Leaves.InsUpdateLeaves(LeaveTypeID, txtLeaveType.Text.Trim(), Convert.ToInt32(txtNoOfDays.Text.Trim()), Convert.ToInt32( Convert.ToInt32(Session["UserId"]).ToString()), Status);
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
        }
        public void Clear()
        {
            txtLeaveType.Text = "";
            txtNoOfDays.Text = "";
            ViewState["LeaveTypeID"] = null;
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
    }
}
