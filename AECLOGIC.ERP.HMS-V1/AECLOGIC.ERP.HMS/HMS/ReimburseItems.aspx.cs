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
    public partial class ReimburseItems : AECLOGIC.ERP.COMMON.WebFormMaster
    {
        int mid = 0;
        bool viewall;
        string menuname;
        string menuid;
        HRCommon objHrCommon = new HRCommon();
        protected override void OnInit(EventArgs e)
        {
            ModuleID = 1;
            base.OnInit(e);
            ReimburseItemsPaging.FirstClick += new Paging.PageFirst(ReimburseItemsPaging_FirstClick);
            ReimburseItemsPaging.PreviousClick += new Paging.PagePrevious(ReimburseItemsPaging_FirstClick);
            ReimburseItemsPaging.NextClick += new Paging.PageNext(ReimburseItemsPaging_FirstClick);
            ReimburseItemsPaging.LastClick += new Paging.PageLast(ReimburseItemsPaging_FirstClick);
            ReimburseItemsPaging.ChangeClick += new Paging.PageChange(ReimburseItemsPaging_FirstClick);
            ReimburseItemsPaging.ShowRowsClick += new Paging.ShowRowsChange(ReimburseItemsPaging_ShowRowsClick);
            ReimburseItemsPaging.CurrentPage = 1;
        }
        void ReimburseItemsPaging_ShowRowsClick(object sender, EventArgs e)
        {
            ReimburseItemsPaging.CurrentPage = 1;
            BindPager();
        }
        void ReimburseItemsPaging_FirstClick(object sender, EventArgs e)
        {
            BindPager();
        }
        void BindPager()
        {
            objHrCommon.PageSize = ReimburseItemsPaging.CurrentPage;
            objHrCommon.CurrentPage = ReimburseItemsPaging.ShowRows;
            BindGrid(objHrCommon);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Convert.ToInt32(Request.QueryString["key"]) == 1)
            {
                tblNew.Visible = true;
                pnltblNew.Visible = true;
                tblEdit.Visible = false;
            }
            if (!IsPostBack)
            {
                GetParentMenuId();
                ViewState["RMItemId"] = "";
                if (Convert.ToInt32(Request.QueryString["key"]) == 0)
                {
                    tblEdit.Visible = true;
                    BindPager();
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
                gvRMItem.Columns[1].Visible = Convert.ToBoolean(ds.Tables[0].Rows[0]["Editable"].ToString());
                btnSubmit.Enabled = (bool)ds.Tables[0].Rows[0]["Editable"];
            }
            return MenuId;
        }
        void BindGrid(HRCommon objHrCommon)
        {
            tblEdit.Visible = true;
            tblNew.Visible = false;
            pnltblNew.Visible = false;
            try
            {
                objHrCommon.PageSize = ReimburseItemsPaging.ShowRows;
                objHrCommon.CurrentPage = ReimburseItemsPaging.CurrentPage;
                DataSet ds = PayRollMgr.GetReimbursementItemsListByPaging(objHrCommon);
                if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0)
                {
                    gvRMItem.DataSource = ds;
                    gvRMItem.DataBind();
                    ReimburseItemsPaging.Visible = true;
                }
                else
                {
                    ReimburseItemsPaging.Visible = false;
                    gvRMItem.DataSource = null;
                    gvRMItem.DataBind();
                }
                ReimburseItemsPaging.Bind(objHrCommon.CurrentPage, objHrCommon.TotalPages, objHrCommon.NoofRecords, objHrCommon.PageSize);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public void BindDetails(int ID)
        {
            tblEdit.Visible = false;
            tblNew.Visible = true;
            pnltblNew.Visible = true;
          DataSet  ds = PayRollMgr.GetReimbursementItemsDetails(ID);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                txtName.Text = ds.Tables[0].Rows[0]["Name"].ToString();
            }
        }
        protected void gvWages_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int ID = Convert.ToInt32(e.CommandArgument);
            ViewState["RMItemId"] = ID;
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
            try
            {
                int WagesID = 0;
                if (ViewState["RMItemId"].ToString() != null && ViewState["RMItemId"].ToString() != string.Empty)
                {
                    WagesID = Convert.ToInt32(ViewState["RMItemId"].ToString());
                }
                int CompanyID = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["CompanyID"].ToString());
                int OutPut = PayRollMgr.InsUpdReimbursementItems(WagesID, txtName.Text.Trim(), CompanyID);
                if (OutPut == 1)
                    AlertMsg.MsgBox(Page, "Inserted sucessfully.!");
                else if (OutPut == 2)
                    AlertMsg.MsgBox(Page, "Already exists.!");
                else
                    AlertMsg.MsgBox(Page, "Updated sucessfully.!");
                BindPager();
                Clear();
            }
            catch (Exception ReItems)
            {
                AlertMsg.MsgBox(Page, ReItems.Message.ToString(),AlertMsg.MessageType.Error);
            }
        }
        public void Clear()
        {
            txtName.Text = "";
            ViewState["RMItemId"] = "";
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
    }
}