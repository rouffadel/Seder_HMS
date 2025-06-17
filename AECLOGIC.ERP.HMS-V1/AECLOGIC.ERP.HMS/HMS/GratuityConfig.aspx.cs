using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using AECLOGIC.HMS.BLL;
using DataAccessLayer;
using AECLOGIC.ERP.COMMON;
using Aeclogic.Common.DAL;

namespace AECLOGIC.ERP.HMS
{
    public partial class GratuityConfig : AECLOGIC.ERP.COMMON.WebFormMaster
    {
        int mid = 0;
        bool viewall;
        string menuname;
        string menuid; static int CompanyID;
        Common objcommon = new Common();

        protected override void OnInit(EventArgs e)
        {
            ModuleID = 1;
            base.OnInit(e);
        
            PagingList.FirstClick += new Paging.PageFirst(GratuityPaging_FirstClick);
            PagingList.PreviousClick += new Paging.PagePrevious(GratuityPaging_FirstClick);
            PagingList.NextClick += new Paging.PageNext(GratuityPaging_FirstClick);
            PagingList.LastClick += new Paging.PageLast(GratuityPaging_FirstClick);
            PagingList.ChangeClick += new Paging.PageChange(GratuityPaging_FirstClick);
            PagingList.ShowRowsClick += new Paging.ShowRowsChange(GratuityPaging_ShowRowsClick);
            PagingList.CurrentPage = 1;
        }

        void GratuityPaging_ShowRowsClick(object sender, EventArgs e)
        {
            PagingList.CurrentPage = 1;
            BindPager();
        }

        void GratuityPaging_FirstClick(object sender, EventArgs e)
        {
            BindPager();
        }

        void BindPager()
        {

            objcommon.PageSize = PagingList.ShowRows;
            objcommon.CurrentPage = PagingList.CurrentPage;
            BindGratuityConfig(objcommon);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
          
            CompanyID = Convert.ToInt32(Session["CompanyID"].ToString());

            if (!IsPostBack)
            {
                GetParentMenuId();
                ViewState["GID"] = 0;

                int ID = 0;
                if (Request.QueryString.Count > 0)
                {

                    ID = Convert.ToInt32(Request.QueryString["id"].ToString());
                }

                if (ID == 1)
                {
                    MainView.ActiveViewIndex = 0;
                    txtGrFrom.Focus();
                }

                else
                {
                    MainView.ActiveViewIndex = 1;
                    BindPager();

                }
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
                viewall = (bool)ViewState["ViewAll"];
                menuname = ds.Tables[0].Rows[0]["menuname"].ToString();
                menuid = MenuId.ToString();
                mid = Convert.ToInt32(ds.Tables[0].Rows[0]["MenuId"].ToString());
                btnSave.Enabled = (bool)ds.Tables[0].Rows[0]["Editable"];
                gvGratuity.Columns[5].Visible = (bool)ds.Tables[0].Rows[0]["Editable"];
            }
            return MenuId;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            int GID = 0;
            if (ViewState["GID"].ToString() != "0")
            {
                GID = Convert.ToInt32(ViewState["GID"]);
            }
            try
            {
                if (txtGrTo.Text == string.Empty)
                    txtGrTo.Text = "0";
                int output = Common.HMS_InsUpd_GratuityConfig(GID, Convert.ToInt32(txtGrFrom.Text),
                                                                 Convert.ToInt32(txtGrTo.Text),
                                                                 Convert.ToInt32(txtEligibleDays.Text),
                                                                 Convert.ToInt32(txtresignation.Text),
                                                                 Convert.ToInt32(txtTermination.Text),
                                                                 Convert.ToInt32(txtIndispline.Text),
                                                                 Convert.ToInt32(txtContract.Text),
                                                                 CompanyID);

                if (output == 2)
                {
                    AlertMsg.MsgBox(Page, "Already Exists");
                }
                else
                {
                    AlertMsg.MsgBox(Page, "Done");
                    BindGratuityConfig(objcommon);
                }
            }
            catch (Exception ex)
            {

                AlertMsg.MsgBox(Page, "Sorry for the inconvinience. Try again.\nError:" + ex.Message.ToString(),AlertMsg.MessageType.Error);
            }
        }

        private void BindGratuityConfig(Common objcommon)
        {
            objcommon.PageSize = PagingList.ShowRows;
            objcommon.CurrentPage = PagingList.CurrentPage;

            DataSet ds = Common.GetGratuityConfigBypaging(objcommon);
            gvGratuity.DataSource = ds;
            gvGratuity.DataBind();
            PagingList.Bind(objcommon.CurrentPage, objcommon.TotalPages, objcommon.NoofRecords, objcommon.PageSize);
            PagingList.Visible = true;
            MainView.ActiveViewIndex = 1;
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            txtGrFrom.Text = "";
            txtGrTo.Text = "";
            txtEligibleDays.Text = "";
            txtresignation.Text = "";
            txtTermination.Text = "";
            txtIndispline.Text = "";
            txtContract.Text = "";
        }

        protected void gvGratuity_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int GID = Convert.ToInt32(e.CommandArgument);
            if (e.CommandName == "Edt")
            {
                BindDetails(GID);
                btnSave.Text = "Update";

            }
        }
        
        private void BindDetails(int GID)
        {

            DataSet ds = Common.GratuityConfigDetails(GID);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables.Count != null)
            {
                MainView.ActiveViewIndex = 0;
                txtGrFrom.Text = ds.Tables[0].Rows[0]["From"].ToString();
                txtGrTo.Text = ds.Tables[0].Rows[0]["To"].ToString();
                txtEligibleDays.Text = ds.Tables[0].Rows[0]["Accrue"].ToString();
                txtresignation.Text = ds.Tables[0].Rows[0]["Resign"].ToString();
                txtTermination.Text = ds.Tables[0].Rows[0]["Termination"].ToString();
                txtIndispline.Text = ds.Tables[0].Rows[0]["Indiscipline"].ToString();
                txtContract.Text = ds.Tables[0].Rows[0]["ContractCompleation"].ToString();
                ViewState["GID"] = ds.Tables[0].Rows[0]["GID"].ToString();
            }
        }

        
    }
}