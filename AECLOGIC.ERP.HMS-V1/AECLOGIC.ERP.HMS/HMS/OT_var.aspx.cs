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
    public partial class OT_var : AECLOGIC.ERP.COMMON.WebFormMaster
    {
        DataSet ds = new DataSet();
        int mid = 0;
        bool viewall, Editable;
        string menuname;
        string menuid;
        HRCommon objHrCommon = new HRCommon();
        protected override void OnInit(EventArgs e)
        {
            ModuleID = 1;
            base.OnInit(e);
        
            EmpListPaging.FirstClick += new Paging.PageFirst(EmpListPaging_FirstClick);
            EmpListPaging.PreviousClick += new Paging.PagePrevious(EmpListPaging_FirstClick);
            EmpListPaging.NextClick += new Paging.PageNext(EmpListPaging_FirstClick);
            EmpListPaging.LastClick += new Paging.PageLast(EmpListPaging_FirstClick);
            EmpListPaging.ChangeClick += new Paging.PageChange(EmpListPaging_FirstClick);
            EmpListPaging.ShowRowsClick += new Paging.ShowRowsChange(EmpListPaging_ShowRowsClick);
            EmpListPaging.CurrentPage = 1;
        }
        void EmpListPaging_ShowRowsClick(object sender, EventArgs e)
        {
            EmpListPaging.CurrentPage = 1;
            BindPager();
        }
        void EmpListPaging_FirstClick(object sender, EventArgs e)
        {
            BindPager();
        }
        void BindPager()
        {

            objHrCommon.PageSize = EmpListPaging.CurrentPage;
            objHrCommon.CurrentPage = EmpListPaging.ShowRows;
            EmployeBind(objHrCommon);
        }
        void EmployeBind(HRCommon objHrCommon)
        {

            try
            {
                objHrCommon.PageSize = EmpListPaging.ShowRows;
                objHrCommon.CurrentPage = EmpListPaging.CurrentPage;

                DataSet ds = new DataSet();
                ds = AttendanceDAC.HR_GetOT_Variables(objHrCommon);
                if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0)
                {
                    gvRMItem.DataSource = ds;
                    gvRMItem.DataBind();
                    EmpListPaging.Visible = true;
                }
                else
                {
                    EmpListPaging.Visible = false;
                    gvRMItem.DataSource = null;
                    gvRMItem.DataBind();
                }
                EmpListPaging.Bind(objHrCommon.CurrentPage, objHrCommon.TotalPages, objHrCommon.NoofRecords, objHrCommon.PageSize);

            }
            catch (Exception e)
            {
                throw e;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                string id =  Convert.ToInt32(Session["UserId"]).ToString();
            }
            catch
            {
                Response.Redirect("Home.aspx");
            }


            topmenu.MenuId = GetParentMenuId();
            topmenu.ModuleId = ModuleID;;
            topmenu.RoleID = Convert.ToInt32(Session["RoleId"].ToString());
            topmenu.SelectedMenu = Convert.ToInt32(mid.ToString());
            topmenu.DataBind();
            Session["menuname"] = menuname;
            Session["menuid"] = menuid;
            Session["MId"] = mid;
            if (!IsPostBack)
            {

                ViewState["CateId"] = "";
                if (Convert.ToInt32(Request.QueryString["key"]) == 1)
                {
                    this.Title = "Add OT Variables";
                    tblNew.Visible = true;
                    tblEdit.Visible = false;
                    gvRMItem.Visible = false;

                }
                else
                {
                    tblNew.Visible = false;
                    tblEdit.Visible = true;
                    gvRMItem.Visible = true;
                    EmployeBind(objHrCommon);
                }

            }
        }
        public int GetParentMenuId()
        {
            string URL = Request.Url.Segments[Request.Url.Segments.Length - 1] + Request.Url.Query;
            int RoleId = Convert.ToInt32(Session["RoleId"].ToString());
            int ModuleId = ModuleID;;

            DataSet ds = new DataSet();

            ds = AttendanceDAC.GetAllowed(RoleId, ModuleId, URL);
            int MenuId = 0;
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                MenuId = Convert.ToInt32(ds.Tables[0].Rows[0]["Under"]);
                gvRMItem.Columns[1].Visible = Convert.ToBoolean(ds.Tables[0].Rows[0]["Editable"].ToString());
                ViewState["ViewAll"] = (bool)ds.Tables[0].Rows[0]["ViewAll"];
                viewall = (bool)ViewState["ViewAll"];
                btnSubmit.Enabled = Editable = Convert.ToBoolean(ds.Tables[0].Rows[0]["Editable"].ToString());
                menuname = ds.Tables[0].Rows[0]["menuname"].ToString();
                menuid = MenuId.ToString();
                mid = Convert.ToInt32(ds.Tables[0].Rows[0]["MenuId"].ToString());
            }
            return MenuId;
        }
        //public void BindGrid()
        //{
        //    ds = AttendanceDAC.GetDesignationsList(true);
        //    gvRMItem.DataSource = ds;
        //    gvRMItem.DataBind();

        //}
        public void BindDetails(int ID)
        {
            tblEdit.Visible = false;
            tblNew.Visible = true;
            ds = AttendanceDAC.GetOT_VarDetails(ID);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                txtName.Text = ds.Tables[0].Rows[0]["name"].ToString();
                txtval.Text = ds.Tables[0].Rows[0]["value"].ToString();

            }
        }
        protected void gvWages_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int ID = Convert.ToInt32(e.CommandArgument);
            ViewState["CateId"] = ID;

            if (e.CommandName == "Edt")
            {
                BindDetails(ID);
            }
            else
            {
                try
                {
                    AttendanceDAC.HR_Upd_OTVAr(ID, txtName.Text.Trim(), txtval.Text.Trim());
                    EmployeBind(objHrCommon);

                }
                catch (Exception DelDesig)
                {
                    AlertMsg.MsgBox(Page, DelDesig.Message.ToString(),AlertMsg.MessageType.Error);
                }
            }
        }

        // public string GetText()
        //{

        //    if (rblDesg.SelectedValue=="1")
        //    {
        //         return "InActive";
        //    }
        //    else
        //    {
        //       return "Active";
        //    }
        //}
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                int CateId = 0;
                if (ViewState["CateId"].ToString() != null && ViewState["CateId"].ToString() != string.Empty)
                {
                    CateId = Convert.ToInt32(ViewState["CateId"].ToString());
                }
                int Output = AttendanceDAC.InsUpdOT_Vars(CateId, txtName.Text.Trim(), txtval.Text.Trim());
                EmployeBind(objHrCommon);
                tblNew.Visible = false;
                tblEdit.Visible = true;
                gvRMItem.Visible = true;
                Clear();
                if (Output == 1)
                {
                    AlertMsg.MsgBox(Page, "Done.!!");
                }
                else if (Output == 2)
                {
                    AlertMsg.MsgBox(Page, "Already Exists.!");

                }
                else
                    AlertMsg.MsgBox(Page, "Updated.!");


            }
            catch (Exception AddDesignation)
            {
                AlertMsg.MsgBox(Page, AddDesignation.Message.ToString(),AlertMsg.MessageType.Error);
            }

        }
        public void Clear()
        {
            txtName.Text = "";
            ViewState["CateId"] = "";
        }
        protected void lnkAdd_Click(object sender, EventArgs e)
        {
            tblNew.Visible = true;
            tblEdit.Visible = false;

        }
        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            tblEdit.Visible = true;
            tblNew.Visible = false;

        }
        protected void rblDesg_SelectedIndexChanged(object sender, EventArgs e)
        {
            EmployeBind(objHrCommon);
        }
        protected void gvRMItem_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton lnkEdt = (LinkButton)e.Row.FindControl("lnkEdit");
                LinkButton lnkDel = (LinkButton)e.Row.FindControl("lnkDel");

                lnkEdt.Enabled = lnkDel.Enabled = Editable;
            }
        }
    }
}
