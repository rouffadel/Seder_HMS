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
    public partial class TypeOfHolidays : AECLOGIC.ERP.COMMON.WebFormMaster
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
            if (Convert.ToInt32(Request.QueryString["key"]) == 1)
            {
                tblNew.Visible = true;
                tblEdit.Visible = false;
            }
            if (!IsPostBack)
            {
                GetParentMenuId();
                ViewState["HolidayID"] = "";
                if (Convert.ToInt32(Request.QueryString["key"]) == 0)
                {
                    tblEdit.Visible = true;
                    BindGrid();
                }
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
                gvLeaveProfile.Columns[2].Visible = Convert.ToBoolean(ds.Tables[0].Rows[0]["Editable"].ToString());
                btnSubmit.Enabled = (bool)ds.Tables[0].Rows[0]["Editable"];
            }
            return MenuId;
        }
        //By ravitheja for active and inactive
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
        public void BindGrid()
        {//By ravi theja for active and Inactive list
            DataSet ds = Leaves.GetTypeofHolidaysList(Convert.ToInt32(rblstStatus.SelectedValue));
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                gvLeaveProfile.DataSource = ds;
            }
            gvLeaveProfile.DataBind();
        }
        public void BindDetails(int ID)
        {
            tblEdit.Visible = false;
            tblNew.Visible = true;
            DataSet ds = Leaves.GetTypeofHolidaysDetails(ID);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                txtName.Text = ds.Tables[0].Rows[0]["Name"].ToString();
                txtSName.Text = ds.Tables[0].Rows[0]["ShortName"].ToString();
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
            ViewState["HolidayID"] = ID;
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
                    BindGrid();
                    AlertMsg.MsgBox(Page, "Completed!");
                }
                catch (Exception DeptHead)
                {
                    AlertMsg.MsgBox(Page, DeptHead.Message.ToString(), AlertMsg.MessageType.Error);
                }
            }
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                int HolidayID = 0;
                int Status = 1;
                if (ViewState["HolidayID"].ToString() != null && ViewState["HolidayID"].ToString() != string.Empty)
                {
                    HolidayID = Convert.ToInt32(ViewState["HolidayID"].ToString());
                }
                if (chkStatus.Checked == false)
                {
                    Status = 0;
                }
                Leaves.InsUpdateTypeofHolidays(HolidayID, txtName.Text.Trim(), txtSName.Text, Convert.ToInt32(Convert.ToInt32(Session["UserId"]).ToString()), Status);
                if (HolidayID == 0)
                {
                    AlertMsg.MsgBox(Page, "Done.! ");
                }
                else
                {
                    AlertMsg.MsgBox(Page, "Updated");
                }
                BindGrid();
                Clear();
                // Added by Rijwan for redirect view page :10-02-2016
                tblEdit.Visible = true;
                tblNew.Visible = false;
            }
            catch (Exception TypeOfHolidays)
            {
                AlertMsg.MsgBox(Page, TypeOfHolidays.Message.ToString(), AlertMsg.MessageType.Error);
            }
        }
        public void Clear()
        {
            txtName.Text = "";
            txtSName.Text = "";
            ViewState["HolidayID"] = "";
        }
        protected void rblstStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGrid();
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
