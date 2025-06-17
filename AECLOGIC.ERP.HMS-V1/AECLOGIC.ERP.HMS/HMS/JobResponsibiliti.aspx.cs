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

namespace AECLOGIC.ERP.HMS.HMS
{
    public partial class JobResponsibiliti : AECLOGIC.ERP.COMMON.WebFormMaster
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

            objCommon.PageSize = Pagetax.ShowRows;
            objCommon.CurrentPage = Pagetax. CurrentPage;
            bindgvjobterm(objCommon);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
               
                CompanyID = Convert.ToInt32(Session["CompanyID"].ToString());
                if (!Page.IsPostBack)
                {
                    binddesgitems();



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
                AlertMsg.MsgBox(Page, ex.Message.ToString(),AlertMsg.MessageType.Error);
            }
        }
    
        void bindgvjobterm(HRCommon objCommon)
        {
            try
            {
                objCommon.CurrentPage = Pagetax.CurrentPage;
                objCommon.PageSize = Pagetax.ShowRows;
                SqlParameter[] p = new SqlParameter[6];
                p[0] = new SqlParameter("@CurrentPage", objCommon.CurrentPage);
                p[1] = new SqlParameter("@PageSize", objCommon.PageSize);
                p[2] = new SqlParameter("@NoofRecords", System.Data.SqlDbType.Int);
                p[2].Direction = ParameterDirection.Output;
                p[3] = new SqlParameter("@ItemName", txtJobresponce.Text);
                if (RespID == 0)
                    p[4] = new SqlParameter("@RespID", SqlDbType.Int);
                else
                    p[4] = new SqlParameter("@RespID", RespID);
                DataSet ds = SqlHelper.ExecuteDataset("HMS_job_Responsibility", p);
                objCommon.TotalPages = (int)p[1].Value;
                objCommon.NoofRecords = (int)p[2].Value;
                ViewState["dataset"] = ds;
                gvjobterm.DataSource = ds;
                gvjobterm.DataBind();
                Pagetax.Bind(objCommon.CurrentPage, objCommon.TotalPages, objCommon.NoofRecords, objCommon.PageSize);
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
            // 
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
             
           DataSet ds = SQLDBUtil.ExecuteDataset("T_HMS_JobTitlesitems");
            ddDesignation.DataSource = ds.Tables[0];
            ddDesignation.DataTextField = "name";
            ddDesignation.DataValueField = "JobTitleID";
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

                gvjobterm.Visible = true;

                txtJobresponce.Text = "";
                ddDesignation.SelectedIndex = 0;
                dvadd.Visible = false;
                dvView.Visible = true;
               // Response.Redirect("~/hms/JobResponsibiliti.aspx");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "alert('Done...');window.location='JobResponsibiliti.aspx';", true);
                gvjobterm.Visible = true;
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
                    
                    Edit();
                    btnSave.Text = "Update";
                }

            }
            catch (Exception ex)
            {
                AlertMsg.MsgBox(Page, ex.Message.ToString(),AlertMsg.MessageType.Error);
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            int ItemName; int ItemTitle = 0;
            if (txtSearchWorksite.Text != "")
            {
                ItemName = Convert.ToInt32(ddlsresp_hid.Value == "" ? "0" : ddlsresp_hid.Value);
            }
            if (txtTitles.Text != "")
            {
                ItemTitle = Convert.ToInt32(ddlTitle_hid.Value == "" ? "0" : ddlTitle_hid.Value);
            }
            try
            {
                objCommon.CurrentPage = Pagetax.CurrentPage;
                objCommon.PageSize = Pagetax.ShowRows;
                SqlParameter[] p = new SqlParameter[6];
                p[0] = new SqlParameter("@CurrentPage", objCommon.CurrentPage);
                p[1] = new SqlParameter("@PageSize", objCommon.PageSize);
                p[2] = new SqlParameter("@NoofRecords", System.Data.SqlDbType.Int);
                p[2].Direction = ParameterDirection.Output;
                p[3] = new SqlParameter("@ItemName", txtSearchWorksite.Text);
                p[4] = new SqlParameter("@RespID", ItemTitle);
                p[5] = new SqlParameter();
                p[5].Direction = ParameterDirection.ReturnValue;
                DataSet ds = SqlHelper.ExecuteDataset("HMS_job_Responsibility", p);
                objCommon.NoofRecords = (int)p[2].Value;
                objCommon.TotalPages = (int)p[5].Value;
                gvjobterm.DataSource = ds;
                gvjobterm.DataBind();
                Pagetax.Bind(objCommon.CurrentPage, objCommon.TotalPages, objCommon.NoofRecords, objCommon.PageSize);
            }
            catch (Exception ex)
            {
                AlertMsg.MsgBox(Page, ex.Message.ToString(),AlertMsg.MessageType.Error);
            }
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionjobRespList(string prefixText, int count, string contextKey)
        {

            DataSet ds = AttendanceDAC.get_employewise_resp(prefixText.Trim());//WSID
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
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionListitle(string prefixText, int count, string contextKey)
        {
           
            DataSet ds = AttendanceDAC.get_employewise_TitleRes(prefixText);
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