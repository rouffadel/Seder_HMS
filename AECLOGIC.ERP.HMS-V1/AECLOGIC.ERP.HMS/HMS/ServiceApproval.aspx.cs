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
using DataAccessLayer;
using Aeclogic.Common.DAL;
using AECLOGIC.HMS.BLL;
using AECLOGIC.ERP.COMMON;
using System.Collections.Generic;
namespace AECLOGIC.ERP.HMS
{
    public partial class ServiceApprovalPage : AECLOGIC.ERP.COMMON.WebFormMaster
    {
        #region LoadEvents
    static int CompanyID = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["CompanyID"].ToString());
        int EmpId = 0;
        static int WSId = 0;
        static char Staus = '1';
        int mid = 0; string menuname; string menuid;
        int stateid; int StatusId;
        Common1 obj = new Common1();
        DataAccessLayer.ServiceApproval objServiceApproval = new DataAccessLayer.ServiceApproval();


        protected override void OnInit(EventArgs e)
        {
            ModuleID = 1;
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
            {
                
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

                this.Page.MaintainScrollPositionOnPostBack = true;
                if (!IsPostBack)
                {
                    SetUpScreen();

                }
                selected();
            }

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
            DateTime? dtTodate = null;
            DateTime? dtFromDate = null;
            if (txtGdnFromDate.Text != string.Empty)
            {

                dtFromDate = CodeUtil.ConvertToDate(txtGdnFromDate.Text, CodeUtil.DateFormat.ddMMMyyyy); //Convert.ToDateTime(txtGdnFromDate.Text);
                if (txtGdnToDate.Text != string.Empty)
                {
                    dtTodate = CodeUtil.ConvertToDate(txtGdnToDate.Text, CodeUtil.DateFormat.ddMMMyyyy).AddDays(1).AddMilliseconds(-1);
                }
                else
                {
                    dtTodate = CodeUtil.ConvertToDate(txtGdnFromDate.Text, CodeUtil.DateFormat.ddMMMyyyy).AddMilliseconds(-1);
                }
            }
            StatusId = Convert.ToInt32(ddlStatus.SelectedValue);
            int ModuleId = Convert.ToInt32(Application["ModuleId"]);
            objCommon.PageSize = pgGoods.ShowRows; ;
            objCommon.CurrentPage = pgGoods.CurrentPage;
            int WSId;
            if (txtSearchWorksite.Text.Trim() != "")
            {
                WSId = Convert.ToInt32(ddlWorkSites_hid.Value == "" ? "0" : ddlWorkSites_hid.Value);

            }

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
                    ds = objCommon.MMS_SRNBillsApproval(objCommon, VendorID,
                                                 txtPONo.Text == String.Empty ? (int?)null : int.Parse(txtPONo.Text),
                                                 txtBillNo.Text == String.Empty ? (int?)null : int.Parse(txtBillNo.Text), ModuleId, WSId, dtFromDate, dtTodate);
                    break;
                case "2":
                    ds = objCommon.MMS_SRNBillsApproved(objCommon, VendorID,
                                                txtPONo.Text == String.Empty ? (int?)null : int.Parse(txtPONo.Text),
                                                txtBillNo.Text == String.Empty ? (int?)null : int.Parse(txtBillNo.Text), ModuleId, StatusId, WSId, dtFromDate, dtTodate);
                    break;
                case "3":
                    ds = objCommon.MMS_SRNBillsRejected(objCommon, VendorID,
                                              txtPONo.Text == String.Empty ? (int?)null : int.Parse(txtPONo.Text),
                                              txtBillNo.Text == String.Empty ? (int?)null : int.Parse(txtBillNo.Text), ModuleId, WSId, dtFromDate, dtTodate);
                    break;
                default:
                    break;
            }
            if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
            {
                gvAutoBilling.DataSource = ds;
            }
            gvAutoBilling.DataBind();
            pgGoods.Visible = true;
            pgGoods.Bind(objCommon.CurrentPage, objCommon.TotalPages, objCommon.NoofRecords, objCommon.PageSize);
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
            txtSearchWorksite.Text = string.Empty;
            txtGdnFromDate.Text = ""; txtGdnToDate.Text = ""; ddlStatus.SelectedValue = "1";
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
            int CompanyId = Convert.ToInt32(CompanyID);
            int ModuleId = ModuleID;;

            if (e.CommandName == "Approve")
            {
                objServiceApproval.MMS_SRNApproveBill(BillNo, CompanyId, EmpId, ModuleId);
            }
            else if (e.CommandName == "Reject")
            {
                objServiceApproval.MMS_ChangeSRNBillStatus(BillNo, 0, EmpId);
            }
            else if (e.CommandName == "Confirm")
            {
                objServiceApproval.MMS_SRNApproveBill(BillNo, CompanyId, EmpId, ModuleId);
            }
            pgGoods.CurrentPage = 1;
            BindPager();
        }

        protected void gvAutoBilling_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Button lnk = (Button)e.Row.Cells[10].Controls[1];
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
            objServiceApproval.PageSize = 10;// Config.ShowRows;
            pgGoods.Visible = false;
            FillDropDownVendor();
        }



       
        public void selected()
        {
            string URL = Request.Url.Segments[Request.Url.Segments.Length - 1] + Request.Url.Query;
            if (URL == "ServiceApproval.aspx?state=1" || URL == "serviceApproval.aspx")
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
        private void FillDropDownVendor()// no method implementation in look up class
        {
             
          DataSet  ds = SQLDBUtil.ExecuteDataset("MMS_DDL_ServiceBillsVendors");
            ddlVendor.Items.Add(new ListItem("Select Vendor ", "0"));
            ddlVendor.DataSource = ds;// LookUp.PopulateEntityDropDown(LookUp.EntityRelationships.ServiceVendor);
            ddlVendor.DataBind();

        }
       
      
      protected void btnApprove_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow gvr in gvAutoBilling.Rows)
            {
                CheckBox chk = new CheckBox();
                Label lblIndent = new Label();
                HtmlAnchor haBNo = new HtmlAnchor();

                haBNo = (HtmlAnchor)gvAutoBilling.FindControl("Bill");
                chk = (CheckBox)gvAutoBilling.FindControl("chkBill");
                if (chk.Checked)
                {
                    int BillNo = 0;
                    BillNo = Convert.ToInt32(haBNo);
                    EmpId =  Convert.ToInt32(Session["UserId"]);
                    int CompanyId = Convert.ToInt32(CompanyID);

                    if (stateid == 2)//for Arroved
                    {
                        objServiceApproval.MMS_ChangeSRNBillStatus(BillNo, 0, EmpId);
                    }
                    else if (stateid == 3)//for reject
                    {
                        objServiceApproval.MMS_ChangeSRNBillStatus(BillNo, 2, EmpId);
                    }
                    else                 //for Approval
                    {
                        int ModuleId = ModuleID;;

                        objServiceApproval.MMS_SRNApproveBill(BillNo, CompanyId, EmpId, ModuleId);
                    }
                }
            }
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
            DataSet ds = AttendanceDAC.GetWorkSite_by_Wsid(prefixText.Trim(), WSId, Staus, CompanyID);
            DataTable dt = ds.Tables[0];
            List<string> items = new List<string>(count);
            var rtval = new Dictionary<string, string>();
            foreach (DataRow row in dt.Rows)
            {
                string str = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(row["Name"].ToString(), row["ID"].ToString());
                items.Add(str);
            }

            return items.ToArray(); ;// txtItems.ToArray();

        }

    }
}