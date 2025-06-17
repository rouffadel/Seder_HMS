using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AECLOGIC.HMS.BLL;
using System.Collections;
using DataAccessLayer;
using AECLOGIC.ERP.COMMON;
using Aeclogic.Common.DAL;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
namespace AECLOGIC.ERP.HMS
{
    public partial class JobResponsibilities : AECLOGIC.ERP.COMMON.WebFormMaster
    {
        string str = string.Empty;
        HRCommon objCommon = new HRCommon();
        HRCommon objHrCommon = new HRCommon();
        string menuid; static int CompanyID;
        int mid = 0; string menuname; bool viewall; bool Editable; int respid = 0; int RespID;
        protected override void OnInit(EventArgs e)
        {
            ModuleID = 1;

            base.OnInit(e);
            // btnSave.Attributes.Add("onclick", "javascript:return ValidateSave('" + txtCategoryName.ClientID + "');");
            Pagetax.FirstClick += new Paging.PageFirst(PageTax_FirstClick);
            Pagetax.PreviousClick += new Paging.PagePrevious(PageTax_FirstClick);
            Pagetax.NextClick += new Paging.PageNext(PageTax_FirstClick);
            Pagetax.LastClick += new Paging.PageLast(PageTax_FirstClick);
            Pagetax.ChangeClick += new Paging.PageChange(PageTax_FirstClick);
            Pagetax.ShowRowsClick += new Paging.ShowRowsChange(PageTax_ShowRowsClick);
            Pagetax.CurrentPage = 1;
        }
        void PageTax_ShowRowsClick(object sender, EventArgs e)
        {
            Pagetax.CurrentPage = 1;
            BindPager();
        }
        void PageTax_FirstClick(object sender, EventArgs e)
        {
            BindPager();
        }
        void BindPager()
        {

            objCommon.PageSize = Pagetax.CurrentPage;
            objCommon.CurrentPage = Pagetax.ShowRows;
            // bindgvjobterm();
            
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                  topmenu.MenuId = GetParentMenuId();
                topmenu.ModuleId = ModuleID;
                topmenu.RoleID = Convert.ToInt32(Session["RoleId"].ToString());
                topmenu.SelectedMenu = Convert.ToInt32(mid.ToString());
                topmenu.DataBind();
                Session["menuname"] = menuname;
                Session["menuid"] = menuid;
                Session["MId"] = mid;
                CompanyID = Convert.ToInt32(Session["CompanyID"].ToString());
                if (!Page.IsPostBack)
                {
                    binddesgitems();

                    bindgvjobterm();


                    if (Convert.ToInt32(Request.QueryString["key"]) == 1)
                    {
                        this.Title = "Add Categories";
                        dvadd.Visible = true;
                        dvView.Visible = false;
                        btnSave.Text = "Save";
                        
                    }
                    else
                    {
                        dvadd.Visible = false;
                        dvView.Visible = true;
                        txtRespID.Text = "0";
                        BindPager();
                    }

                }
            }
            catch (Exception ex)
            {
                AlertMsg.MsgBox(Page, ex.Message);
            }
            }
        public int GetParentMenuId()
        {
            string URL = Request.Url.Segments[Request.Url.Segments.Length - 1] + Request.Url.Query;
            int RoleId = Convert.ToInt32(Session["RoleId"].ToString());
            //int ModuleID = ModuleID;
            DataSet ds = new DataSet();
            ds = AttendanceDAC.GetAllowed(RoleId, ModuleID, URL);
            int MenuId = 0;
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                MenuId = Convert.ToInt32(ds.Tables[0].Rows[0]["Under"]);
                ViewState["Editable"] = Editable = (bool)ds.Tables[0].Rows[0]["Editable"];
                ViewState["ViewAll"] = (bool)ds.Tables[0].Rows[0]["ViewAll"];
                viewall = (bool)ViewState["ViewAll"];
                menuname = ds.Tables[0].Rows[0]["menuname"].ToString();
                menuid = MenuId.ToString();
                mid = Convert.ToInt32(ds.Tables[0].Rows[0]["MenuId"].ToString());
            }
            return MenuId;
        }
        void bindgvjobterm()
        {
            try
            {
                objCommon.CurrentPage = Pagetax.CurrentPage;
                objCommon.PageSize = Pagetax.ShowRows;
                SqlParameter[] p = new SqlParameter[5];
                p[0] = new SqlParameter("@CurrentPage", objCommon.CurrentPage);
                p[1] = new SqlParameter("@PageSize", objCommon.PageSize);
                p[2] = new SqlParameter("@NoofRecords", System.Data.SqlDbType.Int);
                p[2].Direction = ParameterDirection.Output;
                p[3] = new SqlParameter("@ItemName", txtJobresponce.Text);
                if (RespID == 0)
                    p[4] = new SqlParameter("@RespID", SqlDbType.Int);
                else
                    p[4] = new SqlParameter("@RespID", RespID);
                gvjobterm.DataSource = null;
                gvjobterm.DataBind();
                DataSet ds = SqlHelper.ExecuteDataset("HMS_job_Responsibility", p);
                objCommon.NoofRecords = (int)p[2].Value;
                ViewState["dataset"] = ds;
                gvjobterm.DataSource = ds;
                gvjobterm.DataBind();
               //Pagetax.Bind(objCommon.CurrentPage, objCommon.TotalPages, objCommon.NoofRecords, objCommon.PageSize);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Edit()
        {
            objCommon.CurrentPage = Pagetax.CurrentPage;
            objCommon.PageSize = Pagetax.ShowRows;
            SqlParameter[] p = new SqlParameter[5];
            p[0] = new SqlParameter("@CurrentPage", objCommon.CurrentPage);
            p[1] = new SqlParameter("@PageSize", objCommon.PageSize);
            p[2] = new SqlParameter("@NoofRecords", System.Data.SqlDbType.Int);
            p[2].Direction = ParameterDirection.Output;
            p[3] = new SqlParameter("@ItemName", txtJobresponce.Text);
            p[4] = new SqlParameter("@RespID", RespID);
            gvjobterm.DataSource = null;
            gvjobterm.DataBind();
            DataSet ds = SqlHelper.ExecuteDataset("HMS_job_Responsibility", p);
            //DataSet ds = new DataSet();
           // ds = (DataSet)ViewState["dataset"];
            if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0)
            {
                dvView.Visible = false;
                dvadd.Visible = true;
                ddDesignation.SelectedValue = ds.Tables[0].Rows[0]["JobTitleId"].ToString();
                txtJobresponce.Text = ds.Tables[0].Rows[0]["RespDescription"].ToString();
                // respid = Convert.ToInt32(ds.Tables[0].Rows[0]["RespID"].ToString());
            }

        }

        void binddesgitems()
        {
            DataSet ds = new DataSet();
            ds = SQLDBUtil.ExecuteDataset("T_HMS_JobTitlesitems");
            ddDesignation.DataSource = ds.Tables[0];
            ddDesignation.DataTextField = "name";
            ddDesignation.DataValueField = "Desigid";
            ddDesignation.DataBind();
            ddDesignation.Items.Insert(0, new ListItem("Select", "0"));
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {

            int id = 0;
            if (btnSave.Text == "Save")
                id = 1;
            else
                id = 2;

            SqlParameter[] p = new SqlParameter[4];
            p[0] = new SqlParameter("@RespDescription", txtJobresponce.Text.Trim());
            p[1] = new SqlParameter("@JobTitleId", ddDesignation.SelectedValue);
            p[2] = new SqlParameter("@Id", id);
            if (btnSave.Text == "Save")
                p[3] = new SqlParameter("@respid", DBNull.Value);
            else
                p[3] = new SqlParameter("@respid", txtRespID.Text);

            int s = SQLDBUtil.ExecuteNonQuery("T_HMS_insertresponceofJob", p);
            if (s > 0)
            {

                AlertMsg.MsgBox(Page, "Record Inserted Sucess");


               // Response.Write("sucess");
                gvjobterm.Visible = true;

                txtJobresponce.Text = "";
                ddDesignation.SelectedIndex = 0;
                dvadd.Visible = false;
                dvView.Visible = true;
                Response.Redirect("~/JobResponsibilities.aspx");
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                objCommon.CurrentPage = Pagetax.CurrentPage;
                objCommon.PageSize = Pagetax.ShowRows;
                SqlParameter[] p = new SqlParameter[4];
                p[0] = new SqlParameter("@CurrentPage", objCommon.CurrentPage);
                p[1] = new SqlParameter("@PageSize", objCommon.PageSize);
                p[2] = new SqlParameter("@NoofRecords", System.Data.SqlDbType.Int);
                p[2].Direction = ParameterDirection.Output;
                p[3] = new SqlParameter("@ItemName",txtSearchWorksite.Text);
                //gvjobterm.DataSource = null;
                //gvjobterm.DataBind();


                DataSet ds = SqlHelper.ExecuteDataset("HMS_job_Responsibility", p);
                objCommon.NoofRecords = (int)p[1].Value;
                objCommon.TotalPages = (int)p[2].Value;
                gvjobterm.DataSource = ds;
                gvjobterm.DataBind();


                Pagetax.Bind(objCommon.CurrentPage, objCommon.TotalPages, objCommon.NoofRecords, objCommon.PageSize);
            }
            catch (Exception ex)
            {
                AlertMsg.MsgBox(Page, ex.Message);
            }

        }

        protected void gvjobterm_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                 RespID = Convert.ToInt32(e.CommandArgument);
                txtRespID.Text = RespID.ToString();
                ViewState["RespID"] = RespID;
                if (e.CommandName == "Edt")
                {
                   // bindgvjobterm(RespID);
                    //bindgvjobterm(JobTitleId);
                    Edit();
                    btnSave.Text = "Update";
                }

            }
            catch (Exception ex)
            {
                AlertMsg.MsgBox(Page, ex.Message);
            }
        }

        protected void gvjobterm_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton lnkEdt = (LinkButton)e.Row.FindControl("lnkEdit");
                //LinkButton lnkDel = (LinkButton)e.Row.FindControl("lnkDel");

                lnkEdt.Enabled = Editable;
            }
        }
    }
}