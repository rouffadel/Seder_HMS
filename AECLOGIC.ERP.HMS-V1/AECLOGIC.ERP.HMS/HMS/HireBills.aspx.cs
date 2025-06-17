using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Aeclogic.Common.DAL;
//using CompanyDac;
using AECLOGIC.HMS.BLL;
using DataAccessLayer;
using AECLOGIC.ERP.COMMON;
using System.Collections.Generic;
namespace AECLOGIC.ERP.HMS
{
    public partial class HireBills : AECLOGIC.ERP.COMMON.WebFormMaster
    {
        #region LoadEvents
        int stateid; int StatusId; int EmpId = 0;
       static int  WSId=0;
         static char Staus='1';
         static int CompanyID;
        DataAccessLayer.ServiceApproval objServiceApproval = new DataAccessLayer.ServiceApproval();
        protected override void OnInit(EventArgs e)
        {
            if (Request.QueryString["ModuleID"] != null)
            {
                ModuleID = Convert.ToInt32(Request.QueryString["ModuleID"]);
            }
            else
            {
                ModuleID = 1;
            }
            base.OnInit(e);
            pgGoods.FirstClick += new Paging.PageFirst(TasksPaging_FirstClick);
            pgGoods.PreviousClick += new Paging.PagePrevious(TasksPaging_FirstClick);
            pgGoods.NextClick += new Paging.PageNext(TasksPaging_FirstClick);
            pgGoods.LastClick += new Paging.PageLast(TasksPaging_FirstClick);
            pgGoods.ChangeClick += new Paging.PageChange(TasksPaging_FirstClick);
            pgGoods.ShowRowsClick += new Paging.ShowRowsChange(TasksPaging_ShowRowsClick);
            pgGoods.CurrentPage = 1;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
                CompanyID = Convert.ToInt32(Session["CompanyID"]);
                if (Request.QueryString.Count > 0)
                {
                    stateid = Convert.ToInt32(Request.QueryString["state"]);//.Trim();
                    if (stateid == 2)
                    { lblStatus.Visible = true; ddlStatus.Visible = true; }
                    else
                    { lblStatus.Visible = false; ddlStatus.Visible = false; }
                }
                else
                { lblStatus.Visible = false; ddlStatus.Visible = false; }
                if (!IsPostBack)
                {                    
                    lnkApprovals.NavigateUrl = string.Format("HireBills.aspx?state=1&ModuleID={0}", ModuleID);
                    lnkApproved.NavigateUrl = string.Format("HireBills.aspx?state=2&ModuleID={0}", ModuleID);
                    lnkRejected.NavigateUrl = string.Format("HireBills.aspx?state=3&ModuleID={0}",  ModuleID);
                    SetUpScreen();
                }
                selected();
                BindDataGrid(objServiceApproval);
        }
        #endregion LoadEvents
        #region GridPaging
        void TasksPaging_ShowRowsClick(object sender, EventArgs e)
        {
            pgGoods.CurrentPage = 1;
            BindPager();
        }
        void TasksPaging_FirstClick(object sender, EventArgs e)
        {
            BindPager();
        }
        void BindPager()
        {
            objServiceApproval.PageSize = pgGoods.CurrentPage;
            objServiceApproval.CurrentPage = pgGoods.ShowRows;
            BindDataGrid(objServiceApproval);
        }
        #endregion GridPaging
        private void BindDataGrid(DataAccessLayer.ServiceApproval objCommon)
        {
            StatusId = Convert.ToInt32(ddlStatus.SelectedValue);
            int ModuleId = Convert.ToInt32(Application["ModuleId"]);
            objCommon.PageSize = pgGoods.ShowRows; ;
            objCommon.CurrentPage = pgGoods.CurrentPage;
            int WSId;
            if (Convert.ToInt32(ddlWorkSites_hid.Value == "" ? "0" : ddlWorkSites_hid.Value) != 0)
                WSId = Convert.ToInt32(Convert.ToInt32(ddlWorkSites_hid.Value == "" ? "0" : ddlWorkSites_hid.Value));
            else
                WSId = 0;
            int VendorID;
            if (ddlVendor.SelectedIndex != 0)
                VendorID = Convert.ToInt32(ddlVendor.SelectedValue);
            else
                VendorID = 0;
            string State = "1";
            try
            {
                if (Request.QueryString.Count > 0)
                    State = Request.QueryString["state"].Trim();
            }
            catch (Exception e)
            {
                throw e;
            }
            DataSet ds = null;
            switch (State)
            {
                case "1":
                    ds = AttendanceDAC.HR_GetHireBills(1, Convert.ToInt32(Session["CompanyID"]));
                    break;
                case "2":
                    ds = AttendanceDAC.HR_GetHireBillsByAccStatus(2, StatusId, Convert.ToInt32(Session["CompanyID"]));
                    break;
                case "3":
                    ds = AttendanceDAC.HR_GetHireBills(0, Convert.ToInt32(Session["CompanyID"]));
                    break;
                default:
                    ds = AttendanceDAC.HR_GetHireBills(1, Convert.ToInt32(Session["CompanyID"]));
                    break;
            }
            if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
            {
                gvAutoBilling.DataSource = ds; gvAutoBilling.DataSource = ds;
            }
            gvAutoBilling.DataBind(); gvAutoBilling.DataBind();
            pgGoods.Visible = true;
            pgGoods.Bind(objCommon.CurrentPage, objCommon.TotalPages, objCommon.NoofRecords, objCommon.PageSize);//, objCommon.PageSize);
        }
        #region OnClick
        protected void lnkGoods_Click(object sender, EventArgs e)
        {
            Response.Redirect("BillsApproval.aspx#");
        }
        protected void btClear_Click(object sender, EventArgs e)
        {
            ddlVendor.SelectedIndex = 0;
            txtPONo.Text = "";
            txtBillNo.Text = "";
            txtSearchWorksite.Text = "";
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            pgGoods.CurrentPage = 1;
            BindDataGrid(objServiceApproval);
        }
        #endregion OnClick
        protected void GVAutoBilling_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int BillNo = 0;
            BillNo = Convert.ToInt32(e.CommandArgument);
            EmpId =  Convert.ToInt32(Session["UserId"]);
            int CompanyId = 10;
            if (e.CommandName == "Approve")
            {
                AttendanceDAC.HR_HiredBillApproval(BillNo, CompanyId, EmpId);
            }
            else if (e.CommandName == "Reject")
            {
                AttendanceDAC.HR_ChangeHRHiredBillStatus(BillNo, 0);
            }
            else if (e.CommandName == "Confirm")
            {
                AttendanceDAC.HR_ChangeHRHiredBillStatus(BillNo, 2);
            }
            BindPager();
        }
        protected void gvAutoBilling_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton lnk = (LinkButton)e.Row.Cells[7].Controls[1];
                try
                {
                    switch (Request.QueryString["state"].Trim())
                    {
                        case "1":
                            lnk.Text = "Approve";
                            lnk.CommandName = "Approve";
                            break;
                        case "2":
                            lnk.Text = "Reject";
                            lnk.CommandName = "Reject";
                            //if (StatusId == 2)
                            //    lnk.Text = "Confirmed";
                            break;
                        case "3":
                            lnk.Text = "Confirm";
                            lnk.CommandName = "Confirm";
                            break;
                        default:
                            break;
                    }
                }
                catch
                {
                }
            }
        }
        private void SetUpScreen()
        {
            objServiceApproval.CurrentPage = pgGoods.CurrentPage;
            pgGoods.Visible = false;
            FillDropDownVendor();
        }
        public void selected()
        {
            string URL = Request.Url.Segments[Request.Url.Segments.Length - 1] + Request.Url.Query;
            if (URL == "ServiceApproval.aspx?state=1" || URL == "ServiceApproval.aspx")
                lnkApprovals.CssClass = "lnkselected";
            else if (URL == "ServiceApproval.aspx?state=2")
            {
                lnkApproved.CssClass = "lnkselected";
                lnkApprovals.CssClass = "";
            }
            else if (URL == "ServiceApproval.aspx?state=3")
            {
                lnkRejected.CssClass = "lnkselected";
                lnkApprovals.CssClass = "";
            }
        }
        private void FillDropDownVendor()
        {
           DataSet ds = SqlHelper.ExecuteDataset("MMS_DDL_ServiceBillsVendors");
            ddlVendor.DataSource = ds;
            ddlVendor.DataTextField = ds.Tables[0].Columns[0].ToString();
            ddlVendor.DataValueField = ds.Tables[0].Columns[1].ToString();
            ddlVendor.Items.Insert(0, new ListItem("Select Vendor Details", "0"));
            ddlVendor.DataBind();
        }
        AttendanceDAC objAtt = new AttendanceDAC();
        protected void btnApprove_Click(object sender, EventArgs e)
        {
            BindPager();
        }
        public string PONavigateUrl(string POID)
        {
            int val = 1;
            bool chk = true;
            return "javascript:return window.open('ProPurchaseOrderPrint.aspx?id=" + POID + "&PON=" + val + "&tot=" + chk + "' , '_blank')";
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionList(string prefixText, int count, string contextKey)
        {
            DataSet ds = AttendanceDAC.HR_GetWorkSite_basedon_Wsid_googlesearch(prefixText.Trim(),WSId,Staus,CompanyID);
            DataTable dt = ds.Tables[0];
            List<string> items = new List<string>(count);
            var rtval = new Dictionary<string, string>();
            foreach (DataRow row in dt.Rows)
            {
                string str = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(row["Name"].ToString(), row["ID"].ToString());
                items.Add(str);
            }
            return items.ToArray();
        }
    }
}