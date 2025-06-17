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
    public partial class VewFeedback : AECLOGIC.ERP.COMMON.WebFormMaster
    {
        int mid = 0;
        bool viewall, Editable;
        string menuname;
        string menuid;
        HRCommon objHrCommon = new HRCommon();

        protected override void OnInit(EventArgs e)
        {
            ModuleID = 1;
            base.OnInit(e);
        
            ViewFeedbackPaging.FirstClick += new Paging.PageFirst(ViewFeedbackPaging_FirstClick);
            ViewFeedbackPaging.PreviousClick += new Paging.PagePrevious(ViewFeedbackPaging_FirstClick);
            ViewFeedbackPaging.NextClick += new Paging.PageNext(ViewFeedbackPaging_FirstClick);
            ViewFeedbackPaging.LastClick += new Paging.PageLast(ViewFeedbackPaging_FirstClick);
            ViewFeedbackPaging.ChangeClick += new Paging.PageChange(ViewFeedbackPaging_FirstClick);
            ViewFeedbackPaging.ShowRowsClick += new Paging.ShowRowsChange(ViewFeedbackPaging_ShowRowsClick);
            ViewFeedbackPaging.CurrentPage = 1;
        }
        void ViewFeedbackPaging_ShowRowsClick(object sender, EventArgs e)
        {
            ViewFeedbackPaging.CurrentPage = 1;
            BindPager();
        }
        void ViewFeedbackPaging_FirstClick(object sender, EventArgs e)
        {
            if (hdnSearchChange.Value == "1")
                ViewFeedbackPaging.CurrentPage = 1;
            BindPager();
            hdnSearchChange.Value = "0";
        }
        void BindPager()
        {

            objHrCommon.PageSize = ViewFeedbackPaging.CurrentPage;
            objHrCommon.CurrentPage = ViewFeedbackPaging.ShowRows;
            BindGrid(objHrCommon);

        }
        protected void Page_Load(object sender, EventArgs e)
        {
           
            if (!IsPostBack)
            {
                GetParentMenuId();
                BindPager();
            }
        }
       
        public void BindGrid(HRCommon objHrCommon)
        {
            try
            {
                objHrCommon.PageSize = ViewFeedbackPaging.ShowRows;
                objHrCommon.CurrentPage = ViewFeedbackPaging.CurrentPage;

                
              DataSet  ds = AttendanceDAC.GetFeedbackByPaging(objHrCommon);
                if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0)
                {
                    gvFeedbacks.DataSource = ds;
                    ViewFeedbackPaging.Bind(objHrCommon.CurrentPage, objHrCommon.TotalPages, objHrCommon.NoofRecords, objHrCommon.PageSize);
                }
                else
                {
                    gvFeedbacks.EmptyDataText = "No Records Found";
                    ViewFeedbackPaging.Visible = false;
                }
                gvFeedbacks.DataBind();


            }
            catch (Exception e)
            {
                throw e;
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
                ViewState["Editable"] = Editable = (bool)ds.Tables[0].Rows[0]["Editable"];
                
            }
            return MenuId;
        }
        protected void gvFeedbacks_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {

                if (e.CommandName == "Del")
                {
                    int FBID = Convert.ToInt32(e.CommandArgument);
                    AttendanceDAC.HR_DelFeedback(FBID);
                    BindPager();
                    //BindGrid();

                }
            }
            catch (Exception VewFedBak)
            {
                AlertMsg.MsgBox(Page, VewFedBak.Message.ToString(),AlertMsg.MessageType.Error);
            }
        }
        protected void gvFeedbacks_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton lnk = (LinkButton)e.Row.FindControl("lnkDelete");
                lnk.Enabled = Editable;
            }
            foreach (GridViewRow gvr in gvFeedbacks.Rows)
            {
                int Role = 6;//Convert.ToInt32(Session["RoleId"].ToString());
                LinkButton lnkUpd = (LinkButton)gvr.Cells[8].FindControl("lnkDelete");
                if (Role == 6)
                {
                    lnkUpd.Enabled = true;
                }
                else
                {
                    lnkUpd.Enabled = false;
                }
                lnkUpd.Enabled = Convert.ToBoolean(ViewState["Editable"]);
            }
        }
    }
}