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
namespace AECLOGIC.ERP.HMS.HMS
{
    public partial class jobDescription : AECLOGIC.ERP.COMMON.WebFormMaster
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

            objCommon.PageSize = PageTax.CurrentPage;
            objCommon.CurrentPage = PageTax.ShowRows;
            
             empbind(objHrCommon);
           
        }
        void  empbind( HRCommon objHrCommon)
        {
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
                p[4] = new SqlParameter("@descid", DBNull.Value);
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
                AlertMsg.MsgBox(Page, ex.Message);
            }
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
                AlertMsg.MsgBox(Page, ex.Message);
            }
}
        
        public int GetParentMenuId()
        {
            string URL = Request.Url.Segments[Request.Url.Segments.Length - 1] + Request.Url.Query;
            int RoleId = Convert.ToInt32(Session["RoleId"].ToString());
            int ModuleId = ModuleID; ;
            DataSet ds = new DataSet();
            ds = AttendanceDAC.GetAllowed(RoleId, ModuleId, URL);
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
        void bindgvjobterm(int descid)
        {
            try
            {
                objCommon.CurrentPage = PageTax.CurrentPage;
                objCommon.PageSize = PageTax.ShowRows;
                SqlParameter[] p = new SqlParameter[5];
                p[0] = new SqlParameter("@CurrentPage", objCommon.CurrentPage);
                p[1] = new SqlParameter("@PageSize", objCommon.PageSize);
                p[2] = new SqlParameter("@NoofRecords", System.Data.SqlDbType.Int);
                p[2].Direction = ParameterDirection.Output;
                p[3] = new SqlParameter("@ItemName", txtJobDescr.Text);
                p[4] = new SqlParameter("@descid", descid);
                gvjobterm.DataSource = null;
                gvjobterm.DataBind();
                DataSet ds = SqlHelper.ExecuteDataset("HMS_job_description", p);
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


            
        //DataSet ds = new DataSet();
        //ds = SQLDBUtil.ExecuteDataset("HMS_DescriptionsofJob");
        //gvjobterm.DataSource = ds.Tables[0];
        //gvjobterm.DataBind();

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
            if(btnADD.Text=="Save")
                 p[3] = new SqlParameter("@descId",DBNull.Value);
            else
                p[3] = new SqlParameter("@descId", txtdescid.Text);

            int s = SQLDBUtil.ExecuteNonQuery("T_HMS_insert_DescriptionofJob", p);

          

            if (s > 0)
            {

                AlertMsg.MsgBox(Page, "Done");


                Response.Write("sucess");
                gvjobterm.Visible = true;

                txtJobDescr.Text = "";
                ddDesignation.SelectedIndex = 0;
                dvadd.Visible = false;
                dvView.Visible = true;
                Response.Redirect("~/JobDescription.aspx");
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {

        //    try
        //    {
        //        objCommon.CurrentPage = PageTax.CurrentPage;
        //        objCommon.PageSize = PageTax.ShowRows;
        //        SqlParameter[] p = new SqlParameter[4];
        //        p[0] = new SqlParameter("@CurrentPage", objCommon.CurrentPage);
        //        p[1] = new SqlParameter("@PageSize", objCommon.PageSize);
        //        p[2] = new SqlParameter("@NoofRecords", System.Data.SqlDbType.Int);
        //        p[2].Direction = ParameterDirection.Output;
        //        p[3] = new SqlParameter("@ItemName", txtSearchWorksite.Text.Trim());

        //        DataSet ds = SqlHelper.ExecuteDataset("HMS_job_description", p);
        //        objCommon.NoofRecords = (int)p[1].Value;
        //        objCommon.TotalPages = (int)p[2].Value;
        //        gvjobterm.DataSource = ds;
        //        gvjobterm.DataBind();


        //        PageTax.Bind(objCommon.CurrentPage, objCommon.TotalPages, objCommon.NoofRecords, objCommon.PageSize);
        //    }
        //    catch (Exception ex)
        //    {
        //        AlertMsg.MsgBox(Page, ex.Message);
        //    }

            empbind(objHrCommon);
        }
        
       

        protected void gvjobterm_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            try
            {
                descid = Convert.ToInt32(e.CommandArgument);
                txtdescid.Text = descid.ToString();

                if (e.CommandName == "Edt")
                {
                    bindgvjobterm(descid);
                    btnADD.Text = "Update";
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
               

                lnkEdt.Enabled  = Editable;
            }
        }

        

     

    
    }
     
        }








