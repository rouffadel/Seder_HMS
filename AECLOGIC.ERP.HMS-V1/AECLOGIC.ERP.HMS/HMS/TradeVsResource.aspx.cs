using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using AECLOGIC.HMS.BLL;
using AECLOGIC.ERP.COMMON;
using System.Data.SqlClient;
namespace AECLOGIC.ERP.HMS
{
    public partial class TradeVsResource : AECLOGIC.ERP.COMMON.WebFormMaster
    {
        private GridSort objSort;
        DataSet dsWorkSites;
        AttendanceDAC objAtt;
        DataSet ds;
        int mid = 0;
        bool viewall, Editable;
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
            BindPager();
        }
        void BindPager()
        {
            objHrCommon.PageSize = CategoryConfigPaging.CurrentPage;
            objHrCommon.CurrentPage = CategoryConfigPaging.ShowRows;
            GrindBind(objHrCommon);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            objAtt = new AttendanceDAC();
            if (!IsPostBack)
            {
                GetParentMenuId();
                BindResources();
                FIllObject.FillDropDown(ref ddlSercResource, "GetResourceHR_By_TradeResource");
                BindTrade();
                BindPager();
            }
        }
        public void BindResources()
        {
            DataSet ds = objAtt.GetResourceHR();
            ddlResource.DataSource = ds;
            ddlResource.DataValueField = "ResourceID";
            ddlResource.DataTextField = "ResourceName";
            ddlResource.DataBind();
            ddlResource.Items.Insert(0, new ListItem("---Select---", "0"));
        }
        public void BindTrade()
        {
            DataSet ds = objAtt.GetDesignations();
            ddlCategory.DataSource = ds;
            ddlCategory.DataValueField = "DesigId";
            ddlCategory.DataTextField = "Designation";
            ddlCategory.DataBind();
            ddlCategory.Items.Insert(0, new ListItem("---Select---", "0"));
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
                ViewState["Editable"] = Editable = (bool)ds.Tables[0].Rows[0]["Editable"];
            }
            return MenuId;
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            int UserID = Convert.ToInt32( Convert.ToInt32(Session["UserId"]).ToString());
            int ID = Convert.ToInt32(hddID.Value==""?"0":hddID.Value);
            try
            {
                int retval;
                retval = Convert.ToInt32(objAtt.InsUpdTradeVsResource(Convert.ToInt32(ddlCategory.SelectedValue), Convert.ToInt32(ddlResource.SelectedValue),ID));
                if (retval == 1)
                {
                    AlertMsg.MsgBox(Page, "Done !");
                }
                else if(retval == 0)
                  {
                        AlertMsg.MsgBox(Page, "All Ready Exists");
                    }
                else
                {
                    AlertMsg.MsgBox(Page, "UpDated !");
                }
                BindPager();
            }
            catch (Exception ex)
            {
                AlertMsg.MsgBox(Page, "Sorry for the inconvinience. Try again.\nError:" + ex.Message.ToString(),AlertMsg.MessageType.Error);
            }
        }
        protected void btnReset_Click(object sender, EventArgs e)
        {
            ddlResource.SelectedIndex = 0;
            ddlCategory.SelectedIndex = 0;
            hddID.Value = "0";
        }
        private void GrindBind(HRCommon objHrCommon)
        {
            objHrCommon.PageSize = CategoryConfigPaging.ShowRows;
            objHrCommon.CurrentPage = CategoryConfigPaging.CurrentPage;
            int? Category = null;
            int? ResID = null;
            if (ddlSercResource.SelectedIndex != 0)
                ResID = Convert.ToInt32(ddlSercResource.SelectedValue);
            if (ddlSercCategory.SelectedIndex != 0)
                Category = Convert.ToInt32(ddlSercCategory.SelectedValue);
            ds = objAtt.GetTradeVsResource(objHrCommon, ResID, Category);
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
        protected void gdvWS_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Edt")
            {
                LinkButton lb = (LinkButton)e.CommandSource;
                GridViewRow gvr = (GridViewRow)lb.NamingContainer;
                HiddenField hd = (HiddenField)gvr.FindControl("hddResvsID");
                string ID = hd.Value; 
                string DesigID = gdvWS.DataKeys[gvr.RowIndex].Value.ToString();
                string ResourceID = e.CommandArgument.ToString();
                hddID.Value = ID;
                ddlCategory.SelectedValue = DesigID;
                ddlResource.SelectedValue = ResourceID;
            }
        }
        protected void gdvWS_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            foreach (GridViewRow gvr in gdvWS.Rows)
            {
                LinkButton lnkUpd = (LinkButton)gvr.Cells[4].FindControl("lnkSwitch");
                lnkUpd.Enabled = Convert.ToBoolean(ViewState["Editable"]);
            }
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindPager();
        }
        protected void ddlSercResource_SelectedIndexChanged(object sender, EventArgs e)
        {
            int resid =Convert.ToInt32(ddlSercResource.SelectedValue);
            SqlParameter[] param=new SqlParameter[1];
            param[0] = new SqlParameter("@Resourceid", resid);
            FIllObject.FillDropDown(ref ddlSercCategory, "HR_GetDesignations_By_ResourceId",param);
        }
    }
}