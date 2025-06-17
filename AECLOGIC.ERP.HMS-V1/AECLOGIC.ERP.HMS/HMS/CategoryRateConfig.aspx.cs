using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using AECLOGIC.HMS.BLL;
using AECLOGIC.ERP.COMMON;
namespace AECLOGIC.ERP.HMS
{
    public partial class CategoryRateConfig : AECLOGIC.ERP.COMMON.WebFormMaster
    {
        AttendanceDAC objAtt = new AttendanceDAC();
        int mid = 0;
        bool viewall;
        string menuname;
        string menuid;
        HRCommon objHrCommon = new HRCommon();
        protected override void OnInit(EventArgs e)
        {
            ModuleID = 1;
            base.OnInit(e);
            CategoryConfigPaging.FirstClick += new Paging.PageFirst(CategoryConfigPaging_FirstClick);
            CategoryConfigPaging.PreviousClick += new Paging.PagePrevious(CategoryConfigPaging_FirstClick);
            CategoryConfigPaging.NextClick += new Paging.PageNext(CategoryConfigPaging_FirstClick);
            CategoryConfigPaging.LastClick += new Paging.PageLast(CategoryConfigPaging_FirstClick);
            CategoryConfigPaging.ChangeClick += new Paging.PageChange(CategoryConfigPaging_FirstClick);
            CategoryConfigPaging.ShowRowsClick += new Paging.ShowRowsChange(CategoryConfigPaging_ShowRowsClick);
            CategoryConfigPaging.CurrentPage = 1;
        }
        void CategoryConfigPaging_ShowRowsClick(object sender, EventArgs e)
        {
            CategoryConfigPaging.CurrentPage = 1;
            BindPager();
        }
        void CategoryConfigPaging_FirstClick(object sender, EventArgs e)
        {
            if (hdnSearchChange.Value == "1")
                CategoryConfigPaging.CurrentPage = 1;
            BindPager();
            hdnSearchChange.Value = "0";
        }
        void BindPager()
        {
            objHrCommon.PageSize = CategoryConfigPaging.CurrentPage;
            objHrCommon.CurrentPage = CategoryConfigPaging.ShowRows;
            BindWorkSites();
            GrindBind(objHrCommon);
            FIllObject.FillDropDown(ref ddlWSSearch, "HR_GetWorkSite_By_LabourRate");
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            ddlWSSearch.Attributes.Add("onchange", "javascript:return SearchModified();");
            ddlShowCategory.Attributes.Add("onchange", "javascript:return SearchModified();");
            if (!IsPostBack)
            {
                GetParentMenuId();
                ddlWS.Enabled = ddlCategory.Enabled = ddlDesignation.Enabled = true;
                BindWorkSites();
                BindCategories();
                BindDesignations();
                BindPager();
                //GrindBind(1,1);
                FIllObject.FillDropDown(ref ddlWSSearch, "HR_GetWorkSite_By_LabourRate");
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
                ViewState["Editable"] = (bool)ds.Tables[0].Rows[0]["Editable"];
                ViewState["ViewAll"] = (bool)ds.Tables[0].Rows[0]["ViewAll"];
                btnSubmit.Visible = Convert.ToBoolean(ViewState["Editable"]);
                gdvWS.Columns[7].Visible = Convert.ToBoolean(ViewState["Editable"]);
            }
            return MenuId;
        }
        protected void sel_all_cat(object sender, EventArgs e)
        {
            foreach (ListItem ltItem in ddlShowCategory.Items)
            {
                if (chkallcat.Checked == true)
                    ltItem.Selected = true;
                else
                    ltItem.Selected = false;
            }
        }
        public void GrindBind(HRCommon objHrCommon)
        {
            try
            {
                objHrCommon.PageSize = CategoryConfigPaging.ShowRows;
                objHrCommon.CurrentPage = CategoryConfigPaging.CurrentPage;
                int? SiteID;
                if (ddlWSSearch.SelectedIndex > 0)
                    SiteID = Convert.ToInt32(ddlWSSearch.SelectedValue);
                else
                    SiteID = 1;
                //for multiselection of categories 3-1-16 
                string categry = "0";
                foreach (ListItem ltItem in ddlShowCategory.Items)
                {
                    if (ltItem.Selected == true)
                        categry = categry + "," + ltItem.Value;
                }
                categry = categry + ",0";
                DataSet ds = AttendanceDAC.GetCategoryRateConfigListByPaging(objHrCommon, SiteID, categry);
                if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0)
                {
                    gdvWS.DataSource = ds;
                    CategoryConfigPaging.Bind(objHrCommon.CurrentPage, objHrCommon.TotalPages, objHrCommon.NoofRecords, objHrCommon.PageSize);
                }
                else
                {
                    gdvWS.EmptyDataText = "No Records Found";
                    CategoryConfigPaging.Visible = false;
                }
                gdvWS.DataBind();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        private void BindWorkSites()
        {
            DataSet ds = AttendanceDAC.GetWorkSite(0, '1', Convert.ToInt32(Session["CompanyID"]));
            ddlWS.DataSource = ds.Tables[0];
            ddlWS.DataTextField = "Site_Name";
            ddlWS.DataValueField = "Site_ID";
            ddlWS.DataBind();
            ddlWS.Items.Insert(0, new ListItem("---Select---", "0", true));
        }
        private void BindDesignations()
        {
            DataSet ds = objAtt.GetDesignations();
            DataTable dt = ds.Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                ddlDesignation.Items.Add(new ListItem(dr["Designation"].ToString(), dr["DesigId"].ToString()));
            }
            ddlDesignation.Items.Insert(0, new ListItem("---Select---", "0"));
        }
        private void BindCategories()
        {
            DataSet ds = objAtt.GetCategories();
            DataTable dt = ds.Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                ddlCategory.Items.Add(new ListItem(dr["Category"].ToString(), dr["CateId"].ToString()));
                ddlShowCategory.Items.Add(new ListItem(dr["Category"].ToString(), dr["CateId"].ToString()));
            }
            ddlCategory.Items.Insert(0, new ListItem("---Select---", "0"));
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            int i;
            i = objAtt.InsUpdCategoryRateConfig(Convert.ToInt32(ddlWS.SelectedValue), Convert.ToInt32(ddlCategory.SelectedValue), Convert.ToInt32(ddlDesignation.SelectedValue), Convert.ToDecimal(txtRate.Text), Convert.ToInt32( Convert.ToInt32(Session["UserId"]).ToString()), Convert.ToDecimal(txtWHS.Text));
            if (i == 1)
            {
                AlertMsg.MsgBox(Page, "Saved!");
            }
            else
            {
                AlertMsg.MsgBox(Page, "Updated!");
            }
            ddlWS.SelectedIndex = 0;
            ddlCategory.SelectedIndex = 0;
            ddlDesignation.SelectedIndex = 0;
            txtRate.Text = "";
            txtWHS.Text = "";
            BindPager();
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindPager();
        }
        protected void gdvWS_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Edt")
            {
                GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).Parent.Parent;
                Label lblSiteID = (Label)gdvWS.Rows[row.RowIndex].FindControl("lblSiteID");
                Label lblCategaryID = (Label)gdvWS.Rows[row.RowIndex].FindControl("lblCategaryID");
                Label lblDesgID = (Label)gdvWS.Rows[row.RowIndex].FindControl("lblDesgID");
                Label lblRate = (Label)gdvWS.Rows[row.RowIndex].FindControl("lblRate");
                Label lblWorkingHrs = (Label)gdvWS.Rows[row.RowIndex].FindControl("lblWorkingHrs");
                BindDetails(lblSiteID.Text, lblCategaryID.Text, lblDesgID.Text, lblRate.Text, lblWorkingHrs.Text);
            }
        }
        public void BindDetails(string SiteID, string CategaryID, string DesgID, string Rate, string WHS)
        {
            ddlWS.SelectedValue = SiteID;
            ddlCategory.SelectedValue = CategaryID;
            ddlDesignation.SelectedValue = DesgID;
            txtRate.Text = Rate;
            txtWHS.Text = WHS;
            ddlWS.Enabled = ddlCategory.Enabled = ddlDesignation.Enabled = false;
        }
    }
}