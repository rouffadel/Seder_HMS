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
    public partial class EmpTypeOfMessConfiguration : AECLOGIC.ERP.COMMON.WebFormMaster
    {
        #region Declaration
        int mid = 0;
        bool viewall;
        string menuname;
        string menuid;
        #endregion Declaration
        protected override void OnInit(EventArgs e)
        {
            ModuleID = 1;
            base.OnInit(e);
        }
        #region PageLoad
        protected void Page_Load(object sender, EventArgs e)
        {
           
            if (!IsPostBack)
            {
                GetParentMenuId();
                if (Convert.ToInt32(Request.QueryString["id"]) == 1)
                {
                    dvAdd.Visible = true;
                }
                else
                    dvEdit.Visible = true;
                ViewState["MID"] = "";
                if (Convert.ToInt32(Request.QueryString["id"]) == 0)
                {
                    BindGrid();
                }
            }
        }
        #endregion PageLoad

        #region SuportingMethods
        public int GetParentMenuId()
        {
            string URL = Request.Url.Segments[Request.Url.Segments.Length - 1] + Request.Url.Query;
            int RoleId = Convert.ToInt32(Session["RoleId"].ToString());
            int ModuleId = ModuleID;;

              

          DataSet   ds = AttendanceDAC.GetAllowed(RoleId, ModuleId, URL);
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
            int Status = 1;
            if (rblStatus.SelectedValue != "1")
                Status = 0;

            DataSet ds = AttendanceDAC.GetTypeOfMessCofigs(Status);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                gvLeaveProfile.DataSource = ds;
            }
            gvLeaveProfile.DataBind();
        }
        public void BindDetails(int MID)
        {

            DataSet ds = AttendanceDAC.GetTypeofMessDetails(MID);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                txtName.Text = ds.Tables[0].Rows[0]["Name"].ToString();
                txtSName.Text = ds.Tables[0].Rows[0]["ShortName"].ToString();
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
        public void Clear()
        {
            txtName.Text = "";
            txtSName.Text = "";
            ViewState["MID"] = "";
        }
        public string GetText()
        {

            if (rblStatus.SelectedValue == "1")
            {
                return "IsActive";
            }
            else
            {
                return "Active";
            }
        }
        #endregion SuportingMethods

        #region Events
        protected void btnSubmit_Click(object sender, EventArgs e)
        {


            try
            {
                int? MID = null;
                int Status = 1;
                if (ViewState["MID"].ToString() != null && ViewState["MID"].ToString() != string.Empty)
                {
                    MID = Convert.ToInt32(ViewState["MID"].ToString());
                }
                if (chkStatus.Checked == false)
                {
                    Status = 0;
                }
                int OutPut = AttendanceDAC.InsUpdateTypeofMess(MID, txtName.Text.Trim(), txtSName.Text, Status, Convert.ToInt32( Convert.ToInt32(Session["UserId"]).ToString()));
                if (OutPut == 1)
                    AlertMsg.MsgBox(Page, "Inserted sucessfully.!");
                else if (OutPut == 2)
                    AlertMsg.MsgBox(Page, "Already exists.!");
                else
                    AlertMsg.MsgBox(Page, "Updated sucessfully.!");

                BindGrid();
                Clear();
                dvEdit.Visible = true;
                dvAdd.Visible = false;
            }
            catch (Exception ex)
            {
                AlertMsg.MsgBox(Page, ex.Message.ToString(),AlertMsg.MessageType.Error);
            }
        }
        protected void gvLeaveProfile_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int MID = Convert.ToInt32(e.CommandArgument);
            ViewState["MID"] = MID;
            if (e.CommandName == "Edt")
            {
                BindDetails(MID);
                dvAdd.Visible = true;
                dvEdit.Visible = false;

            }
            if (e.CommandName == "Del")
            {
                GridViewRow gvr = (GridViewRow)((LinkButton)e.CommandSource).Parent.Parent;
                Label lblName = (Label)gvLeaveProfile.Rows[gvr.RowIndex].FindControl("lblMessName");
                string Name = lblName.Text;
                Label lblSName = (Label)gvLeaveProfile.Rows[gvr.RowIndex].FindControl("lblSName");
                string SName = lblSName.Text;
                LinkButton lnkdel = (LinkButton)gvLeaveProfile.Rows[gvr.RowIndex].FindControl("lnkDel");
                string StatusOfMessType = lnkdel.Text;
                int Status = 1;
                if (StatusOfMessType != "Active")
                    Status = 0;

                AttendanceDAC.InsUpdateTypeofMess(MID, Name, SName, Status, Convert.ToInt32( Convert.ToInt32(Session["UserId"]).ToString()));
            }
            BindGrid();
        }
        protected void rblStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGrid();
        }
        #endregion Events
    }
}