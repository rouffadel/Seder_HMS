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
    public partial class CompanyContribution : AECLOGIC.ERP.COMMON.WebFormMaster
    {
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
                BindItem();
                BindYears();
                btnSubmit.Visible = false;
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
                btnSubmit.Enabled = (bool)ds.Tables[0].Rows[0]["Editable"];
            }
            return MenuId;
        }
        public void BindItem()
        {
            DataSet ds = AttendanceDAC.HR_GetPayRollItems();
            ddlItem.DataSource = ds.Tables[0];
            ddlItem.DataTextField = "LongName";
            ddlItem.DataValueField = "Itemid";
            ddlItem.DataBind();
            ddlItem.Items.Insert(0, new ListItem("---SELECT---", "0", true));
        }
        public void BindYears()
        {
            DataSet ds = AttendanceDAC.GetCalenderYear();
            int i = 0;
            int Maxyear = Convert.ToInt32(ds.Tables[0].Rows[0]["MaxYear"].ToString());
            for (int Minyear = Convert.ToInt32(ds.Tables[0].Rows[0]["MinYear"].ToString()); Minyear <= Maxyear; Minyear++)
            {
                ddlYear.Items.Insert(i, new ListItem(Convert.ToString(Minyear), Convert.ToString(Minyear)));
                i = i + 1;
            }
            if (ds.Tables[0].Rows[0]["PreviousMonth"].ToString() == "0")
            {
                ddlMonth.SelectedValue = "12";
                int CurrentYear = Convert.ToInt32(ds.Tables[0].Rows[0]["CurrentYear"]);
                int PreviousYear = CurrentYear - 1;
                ddlYear.Items.FindByValue(PreviousYear.ToString()).Selected = true;
            }
            //if we are in same year and same month
            else
            {
                ddlMonth.SelectedValue = ds.Tables[0].Rows[0]["CurrentMonth"].ToString();
                if (ddlYear.Items.FindByValue(ds.Tables[0].Rows[0]["CurrentYear"].ToString()) != null)
                {
                    ddlYear.Items.FindByValue(ds.Tables[0].Rows[0]["CurrentYear"].ToString()).Selected = true;
                }
                else
                {
                    ddlYear.SelectedIndex = ddlYear.Items.Count - 1;
                }
            }
        }
        protected string GetTotal()
        {
            int RMItemId = Convert.ToInt32(ddlItem.SelectedItem.Value);
            int Month = Convert.ToInt32(ddlMonth.SelectedItem.Value);
            int Year = Convert.ToInt32(ddlYear.SelectedItem.Value);
            DataSet ds = AttendanceDAC.HR_SearchReimburseEmployee(RMItemId, Month, Year);
            string total = ds.Tables[1].Rows[0][0].ToString();
            return total.ToString();
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            int RMItemId = Convert.ToInt32(ddlItem.SelectedItem.Value);
            int Month = Convert.ToInt32(ddlMonth.SelectedItem.Value);
            int Year = Convert.ToInt32(ddlYear.SelectedItem.Value);
            DataSet ds = AttendanceDAC.HR_SearchReimburseEmployee(RMItemId, Month, Year);
            gvEmpList.DataSource = ds;
            gvEmpList.DataBind();
            if (ds.Tables[0].Rows.Count > 0)
            {
                btnSubmit.Visible = true;
            }
            else
            {
                btnSubmit.Visible = false;
            }
        }
        protected void gvEmpList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.Header)
                {
                    CheckBox chkEmp = (CheckBox)e.Row.FindControl("chkAll");
                    chkEmp.Attributes.Add("onclick", String.Format("javascript:return SelectAll(this,'{0}','chkEmp');", gvEmpList.ClientID));
                }
            }
            catch (Exception Ex)
            {
            }
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow gvr in gvEmpList.Rows)
            {
                CheckBox chmEmp = (CheckBox)gvr.Cells[0].FindControl("chkEmp");
                if (chmEmp.Checked == true)
                {
                    Label lblEmpID = (Label)gvr.Cells[1].FindControl("lblEmpID");
                    Label lblEmpName = (Label)gvr.Cells[2].FindControl("lblEmpName");
                    Label lblAmount = (Label)gvr.Cells[4].FindControl("lblCount");
                    Label lblYear = (Label)gvr.Cells[5].FindControl("lblYear");
                    Label lblMonth = (Label)gvr.Cells[6].FindControl("lblMonth");
                    Label lblItem = (Label)gvr.Cells[6].FindControl("lblItemID");
                    Label lblTransID = (Label)gvr.Cells[6].FindControl("lblTransID");
                    int EmpID = Convert.ToInt32(lblEmpID.Text);
                    int Month = Convert.ToInt32(lblMonth.Text);
                    int Year = Convert.ToInt32(lblYear.Text);
                    int ContributionID = Convert.ToInt32(lblItem.Text);
                    string ContributionAmount = lblAmount.Text;
                    int TransID = Convert.ToInt32(lblTransID.Text);
                    AttendanceDAC.HR_InsUpEmpContribution(EmpID, Month, Year, ContributionID, ContributionAmount, TransID);
                }
            }
            AlertMsg.MsgBox(Page, "Done!");
        }
    }
}