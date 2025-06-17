using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using AECLOGIC.HMS.BLL;
using DataAccessLayer;
using Aeclogic.Common.DAL;
namespace AECLOGIC.ERP.HMS
{
    public partial class HolidayPaidRules : AECLOGIC.ERP.COMMON.WebFormMaster
    {
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
                pnltblNew.Visible = true;
                tblEdit.Visible = false;
                gvPaidRules.Visible = false;
            }
            if (!IsPostBack)
            {
                ViewState["RuleID"] = "";
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
            int ModuleId = ModuleID;;
          DataSet  ds = AttendanceDAC.GetAllowed(RoleId, ModuleId, URL);
            int MenuId = 0;
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                MenuId = Convert.ToInt32(ds.Tables[0].Rows[0]["Under"]);
                btnSave.Enabled = (bool)ds.Tables[0].Rows[0]["Editable"];
                gvPaidRules.Columns[5].Visible = (bool)ds.Tables[0].Rows[0]["Editable"];
            }
            return MenuId;
        }
        public void BindGrid()
        {
           DataSet ds = Leaves.GetHolidayPaidRulesList();
            gvPaidRules.DataSource = ds;
            gvPaidRules.DataBind();
        }
        public void BindDetails(int RuleID)
        {
            tblEdit.Visible = false;
            tblNew.Visible = true;
            pnltblNew.Visible = true;
            DataSet ds = Leaves.GetHolidayPaidRulesDetails(RuleID);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                txtRuleName.Text = ds.Tables[0].Rows[0]["RuleName"].ToString();
                ddlCombination1.SelectedValue = ds.Tables[0].Rows[0]["Comb1"].ToString();
                ddlCombination2.SelectedValue = ds.Tables[0].Rows[0]["Comb2"].ToString();
                ddlCombination3.SelectedValue = ds.Tables[0].Rows[0]["Comb3"].ToString();
                if (ds.Tables[0].Rows[0]["IsPaid"].ToString() == "True")
                {
                    chkStatus.Checked = true;
                }
                else
                {
                    chkStatus.Checked = false;
                }
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                foreach(GridViewRow row in gvPaidRules.Rows)
                {
                    Label lblName = (Label)row.FindControl("lblName");
                    DropDownList ddlIspaid = (DropDownList)row.FindControl("ddlIsPaid");
                    SqlParameter[] sqlParams = new SqlParameter[2];
                    sqlParams[0] = new SqlParameter("@RuleName", lblName.Text);
                    sqlParams[1] = new SqlParameter("@IsPaid ", ddlIspaid.SelectedValue);
                    SQLDBUtil.ExecuteNonQuery("HR_Upd_HolidayPaidRules", sqlParams);
                }
                BindGrid();
                Clear();
                    AlertMsg.MsgBox(Page, "Updated");
                tblEdit.Visible = true;
                tblNew.Visible = false;
                pnltblNew.Visible = false;
            }
            catch (Exception HolidayPaidRules)
            {
                AlertMsg.MsgBox(Page, HolidayPaidRules.Message.ToString(),AlertMsg.MessageType.Error);
            }
        }
        protected void gvLeaveProfile_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int ID = Convert.ToInt32(e.CommandArgument);
            ViewState["RuleID"] = ID;
            if (e.CommandName == "Edt")
            {
                BindDetails(ID);
            }
            else
            {
            }
        }
        public void Clear()
        {
            txtRuleName.Text = "";
            ddlCombination1.SelectedIndex = 0;
            ddlCombination2.SelectedIndex = 0;
            ddlCombination3.SelectedIndex = 0;
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
        protected void gvPaidRules_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ((DropDownList)e.Row.FindControl("ddlIsPaid")).SelectedValue =
                        DataBinder.Eval(e.Row.DataItem, "IsPaid").ToString();
            }
        }
        protected void ddlIsPaid_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddl = (DropDownList)sender;
            GridViewRow gvr = (GridViewRow)(((Control)sender).NamingContainer);
            Label lblName = (Label)gvr.FindControl("lblName");
            DropDownList ddlIspaid = (DropDownList)gvr.FindControl("ddlIsPaid");
            SqlParameter[] sqlParams = new SqlParameter[2];
            sqlParams[0] = new SqlParameter("@RuleName", lblName.Text);
            sqlParams[1] = new SqlParameter("@IsPaid ", ddlIspaid.SelectedValue);
            SQLDBUtil.ExecuteNonQuery("HR_Upd_HolidayPaidRules", sqlParams);
            AlertMsg.MsgBox(Page, "Updated");
        }
    }
}
