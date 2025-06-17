using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using AECLOGIC.HMS.BLL;
using System.Collections;
using DataAccessLayer;
using AECLOGIC.ERP.COMMON;
using Aeclogic.Common.DAL;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Data.SqlClient;
using System.Configuration;

namespace AECLOGIC.ERP.HMS
{
    public partial class jobDescriptions : AECLOGIC.ERP.COMMON.WebFormMaster
    {

        string str = string.Empty;
        HRCommon objCommon = new HRCommon();
        HRCommon objHrCommon = new HRCommon();
        string menuid; static int CompanyID;
        bool Editable;
        int mid = 0; string menuname; bool viewall;
        int descid;
        protected override void OnInit(EventArgs e)
        {
            ModuleID = 1;

            base.OnInit(e);
            // btnSave.Attributes.Add("onclick", "javascript:return ValidateSave('" + txtCategoryName.ClientID + "');");
            PageTax.FirstClick += new Paging.PageFirst(PageTax_FirstClick);
            PageTax.PreviousClick += new Paging.PagePrevious(PageTax_FirstClick);
            PageTax.NextClick += new Paging.PageNext(PageTax_FirstClick);
            PageTax.LastClick += new Paging.PageLast(PageTax_FirstClick);
            PageTax.ChangeClick += new Paging.PageChange(PageTax_FirstClick);
            PageTax.ShowRowsClick += new Paging.ShowRowsChange(PageTax_ShowRowsClick);
            PageTax.CurrentPage = 1;
        }
        void PageTax_ShowRowsClick(object sender, EventArgs e)
        {
            PageTax.CurrentPage = 1;
            BindPager();
        }
        void PageTax_FirstClick(object sender, EventArgs e)
        {
            BindPager();
        }
        void BindPager()
        {

            objCommon.PageSize = PageTax.ShowRows;
            objCommon.CurrentPage = PageTax.CurrentPage;

            empbind(objHrCommon);

        }
        void empbind(HRCommon objHrCommon)
        {
            int ItemName; int ItemTitle = 0;
            if (txtSearchWorksite.Text!="")
            {
                ItemName = Convert.ToInt32(ddlsemp_hid.Value == "" ? "0" : ddlsemp_hid.Value);
            }
            if (txtTitles.Text != "")
            {
                ItemTitle = Convert.ToInt32(ddlTitle_hid.Value == "" ? "0" : ddlTitle_hid.Value);
            }
            try
            {
                objCommon.CurrentPage = PageTax.CurrentPage;
                objCommon.PageSize = PageTax.ShowRows;
                SqlParameter[] p = new SqlParameter[6];
                p[0] = new SqlParameter("@CurrentPage", objCommon.CurrentPage);
                p[1] = new SqlParameter("@PageSize", objCommon.PageSize);
                p[2] = new SqlParameter("@NoofRecords", System.Data.SqlDbType.Int);
                p[2].Direction = ParameterDirection.Output;
                p[3] = new SqlParameter("@ItemName", txtSearchWorksite.Text.Trim());
                p[4] = new SqlParameter("@jobtitleid", ItemTitle);
                p[5] = new SqlParameter();
                p[5].Direction = ParameterDirection.ReturnValue;
                DataSet ds = SqlHelper.ExecuteDataset("HMS_job_description", p);
                objCommon.NoofRecords = (int)p[2].Value;
                objCommon.TotalPages = (int)p[5].Value;
                gvjobterm.DataSource = ds;
                gvjobterm.DataBind();


                PageTax.Bind(objCommon.CurrentPage, objCommon.TotalPages, objCommon.NoofRecords, objCommon.PageSize);
            }
            catch (Exception ex)
            {
                AlertMsg.MsgBox(Page, ex.Message.ToString(),AlertMsg.MessageType.Error);
            }
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
                        btnADD.Text = "Save";

                    }
                    else
                    {
                        dvadd.Visible = false;
                        dvView.Visible = true;
                        txtdescid.Text = "0";
                        BindPager();
                    }

                }
            }
            catch (Exception ex)
            {
                AlertMsg.MsgBox(Page, ex.Message.ToString(),AlertMsg.MessageType.Error);
            }
        }

    
        void bindgvjobterm(int descid)
        {
            try
            {
                objCommon.CurrentPage = 1;
                objCommon.PageSize = PageTax.ShowRows;
                SqlParameter[] p = new SqlParameter[5];
                p[0] = new SqlParameter("@CurrentPage", objCommon.CurrentPage);
                p[1] = new SqlParameter("@PageSize", objCommon.PageSize);
                p[2] = new SqlParameter("@NoofRecords", System.Data.SqlDbType.Int);
                p[2].Direction = ParameterDirection.Output;
                p[3] = new SqlParameter("@ItemName", txtJobDescr.Text);
                p[4] = new SqlParameter("@jobtitleid", descid);
                gvjobterm.DataSource = null;
                gvjobterm.DataBind();
                DataSet ds = SqlHelper.ExecuteDataset("HMS_job_description_Edit", p);
                objCommon.NoofRecords = (int)p[1].Value;
                objCommon.TotalPages = (int)p[2].Value;
                gvjobterm.DataSource = ds;
                gvjobterm.DataBind();
                if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0)
                {
                    dvView.Visible = false;
                    dvadd.Visible = true;
                    ddDesignation.SelectedValue = ds.Tables[0].Rows[0]["JobTitleId"].ToString();
                    txtJobDescr.Text = ds.Tables[0].Rows[0]["Name"].ToString();
                    //txtdescid.Text=ds.Tables[0].Rows[0]["descid"].ToString();
                }

            }
            catch
            {

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
        protected void btnADD_Click(object sender, EventArgs e)
        {
            int id = 0;
            if (btnADD.Text == "Save")
                id = 1;
            else
                id = 2;

            SqlParameter[] p = new SqlParameter[4];

            p[0] = new SqlParameter("@jobdescription", txtJobDescr.Text.Trim());

            p[1] = new SqlParameter("@JobTitleId", ddDesignation.SelectedValue);
            p[2] = new SqlParameter("@Id", id);
            if (btnADD.Text == "Save")
                p[3] = new SqlParameter("@descId", DBNull.Value);
            else
                p[3] = new SqlParameter("@descId", txtdescid.Text);

            int s = SQLDBUtil.ExecuteNonQuery("T_HMS_insert_DescriptionofJob", p);



            if (s > 0)
            {

                //AlertMsg.MsgBox(Page, "Done");


               // Response.Write("sucess");
                

                txtJobDescr.Text = "";
                ddDesignation.SelectedIndex = 0;
                dvadd.Visible = false;
                dvView.Visible = true;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "alert('Done...');window.location='JobDescriptions.aspx';", true);
                gvjobterm.Visible = true;
                //Response.Redirect("~/hms/JobDescriptions.aspx");
                BindPager();
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            empbind(objHrCommon);
        }

        protected void gvjobterm_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                descid = Convert.ToInt32(e.CommandArgument);
                txtdescid.Text = descid.ToString();
                objCommon.CurrentPage = 1;

                if (e.CommandName == "Edt")
                {
                    bindgvjobterm(descid);
                    btnADD.Text = "Update";
                }

            }
            catch (Exception ex)
            {
                AlertMsg.MsgBox(Page, ex.Message.ToString(),AlertMsg.MessageType.Error);
            }
        }

      

        //for employee
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionList(string prefixText, int count, string contextKey)
        {
            
            DataSet ds = AttendanceDAC.get_employewise_jobdescription(prefixText);
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
            
            DataSet ds = AttendanceDAC.get_employewise_Title(prefixText);
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