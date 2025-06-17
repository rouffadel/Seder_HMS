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
    public partial class NonCTCComponents : AECLOGIC.ERP.COMMON.WebFormMaster
    {

         
        int mid = 0;
        bool viewall;
        string menuname;
        string menuid;
        bool Editable;
        int CompanyID = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["CompanyID"].ToString());
        HRCommon objHrCommon = new HRCommon();

        protected override void OnInit(EventArgs e)
        {
            ModuleID = 1;
            base.OnInit(e);
        
            AllowancePaging.FirstClick += new Paging.PageFirst(AllowancePaging_FirstClick);
            AllowancePaging.PreviousClick += new Paging.PagePrevious(AllowancePaging_FirstClick);
            AllowancePaging.NextClick += new Paging.PageNext(AllowancePaging_FirstClick);
            AllowancePaging.LastClick += new Paging.PageLast(AllowancePaging_FirstClick);
            AllowancePaging.ChangeClick += new Paging.PageChange(AllowancePaging_FirstClick);
            AllowancePaging.ShowRowsClick += new Paging.ShowRowsChange(AllowancePaging_ShowRowsClick);
            AllowancePaging.CurrentPage = 1;
        }
        void AllowancePaging_ShowRowsClick(object sender, EventArgs e)
        {
            AllowancePaging.CurrentPage = 1;
            BindPager();
        }
        void AllowancePaging_FirstClick(object sender, EventArgs e)
        {
            BindPager();
        }
        void BindPager()
        {

            objHrCommon.PageSize = AllowancePaging.CurrentPage;
            objHrCommon.CurrentPage = AllowancePaging.ShowRows;
            BindGrid(objHrCommon);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
           

            if (Convert.ToInt32(Request.QueryString["key"]) == 1)
            {
                tblNew.Visible = true;
                pnltblNew.Visible = true;
                tblEdit.Visible = false;
                gvAllowances.Visible = false;


            }
            if (Convert.ToInt32(Request.QueryString["key"]) == 2)
            {
                tblEdit.Visible = false;
                tblNew.Visible = false;
                pnltblNew.Visible = false;
                gvAllowances.Visible = false;

            }
           
            if (!IsPostBack)
            {
                GetParentMenuId();
                ViewState["AllowId"] = "";
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
                gvAllowances.Columns[2].Visible = Editable = Convert.ToBoolean(ds.Tables[0].Rows[0]["Editable"].ToString());
               
                btnSubmit.Enabled = Editable;
            }
            return MenuId;
        }

        void BindGrid(HRCommon objHrCommon)
        {


            try
            {

                objHrCommon.PageSize = AllowancePaging.ShowRows;
                objHrCommon.CurrentPage = AllowancePaging.CurrentPage;


                DataSet ds = PayRollMgr.GetNonCTCComponentsListByPaging(objHrCommon);
                if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0)
                {
                    gvAllowances.DataSource = ds;
                    gvAllowances.DataBind();

                    if (Convert.ToInt32(Request.QueryString["key"]) != 1)
                    {
                        gvAllowances.Visible = true;
                        tblEdit.Visible = true;
                        tblNew.Visible = false;
                        pnltblNew.Visible = false;
                    }
                    else
                    {
                        gvAllowances.Visible = false;
                        tblEdit.Visible = false;
                        tblNew.Visible = true;
                        pnltblNew.Visible = true;
                    }

                }
                else
                {
                    tblEdit.Visible = false;
                    gvAllowances.Visible = false;
                    gvAllowances.DataSource = null;
                    gvAllowances.DataBind();
                }
                AllowancePaging.Bind(objHrCommon.CurrentPage, objHrCommon.TotalPages, objHrCommon.NoofRecords, objHrCommon.PageSize);


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
            DataSet ds = PayRollMgr.GetNonCTCComponentsDetails(ID);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                txtName.Text = ds.Tables[0].Rows[0]["Name"].ToString();
                txtSName.Text = ds.Tables[0].Rows[0]["LongName"].ToString();
                rbtlstType.SelectedValue = ds.Tables[0].Rows[0]["Yearly"].ToString();
                txtNoOFYears.Text = ds.Tables[0].Rows[0]["NoofYears"].ToString();

            }
        }
        protected void gvWages_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int ID = Convert.ToInt32(e.CommandArgument);
            ViewState["AllowId"] = ID;
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
                int AllowId = 0;
                if (ViewState["AllowId"].ToString() != null && ViewState["AllowId"].ToString() != string.Empty)
                {
                    AllowId = Convert.ToInt32(ViewState["AllowId"].ToString());
                }
                int CompanyID = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["CompanyID"].ToString());
                int Output = PayRollMgr.InsUpdateNonCTCComponents(AllowId, txtName.Text.Trim(), txtSName.Text, CompanyID, Convert.ToInt32(rbtlstType.SelectedItem.Value), Convert.ToInt32(txtNoOFYears.Text));


                if (Output == 1)
                    AlertMsg.MsgBox(Page, "Inserted sucessfully.!");
                else if (Output == 2)
                    AlertMsg.MsgBox(Page, "Already exists.!");
                else
                    AlertMsg.MsgBox(Page, "Updated sucessfully.!");

                BindPager();
                Clear();

            }
            catch (Exception Allowances)
            {
                AlertMsg.MsgBox(Page, "Ledger already Exists");
            }
        }
        public void Clear()
        {
            txtName.Text = "";
            txtSName.Text = "";
            ViewState["AllowId"] = "";
        }
        protected void gvAllowances_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton lnkedt = (LinkButton)e.Row.FindControl("lnkEdit");
                lnkedt.Enabled = Editable;
            }
        }
    }
}