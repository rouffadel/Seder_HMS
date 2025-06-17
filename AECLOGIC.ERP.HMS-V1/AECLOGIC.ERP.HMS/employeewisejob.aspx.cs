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
    public partial class employeewisejob : AECLOGIC.ERP.COMMON.WebFormMaster
    {
        int Id = 1;
        int mid = 0;
        bool viewall;
        string menuname;
        string menuid;
        String MyString;
        string extension;
        bool Editable;
        int ModuleId;
        static int CompanyID;
        string str = string.Empty;
        HRCommon objCommon = new HRCommon();
        HRCommon objHrCommon = new HRCommon();
        //string menuid; static int CompanyID;  int mid = 0; string menuname; bool viewall; bool Editable;
      //  string ModuleId = System.Configuration.ConfigurationManager.AppSettings["ModuleId"];
        protected override void OnInit(EventArgs e)
        {
            ModuleId = 1;
            base.OnInit(e);
            // btnSave.Attributes.Add("onclick", "javascript:return ValidateSave('" + txtCategoryName.ClientID + "');");
            //Pagetax.FirstClick += new Paging.PageFirst(PageTax_FirstClick);
            //Pagetax.PreviousClick += new Paging.PagePrevious(PageTax_FirstClick);
            //Pagetax.NextClick += new Paging.PageNext(PageTax_FirstClick);
            //Pagetax.LastClick += new Paging.PageLast(PageTax_FirstClick);
            //Pagetax.ChangeClick += new Paging.PageChange(PageTax_FirstClick);
            //Pagetax.ShowRowsClick += new Paging.ShowRowsChange(PageTax_ShowRowsClick);
            //Pagetax.CurrentPage = 1;
        }
        // void PageTax_ShowRowsClick(object sender, EventArgs e)
        //{
        //    Pagetax.CurrentPage = 1;
        //    BindPager();
        //}
        //void PageTax_FirstClick(object sender, EventArgs e)
        //{
        //    BindPager();
        //}
        //void BindPager()
        //{

        //    objCommon.PageSize = Pagetax.CurrentPage;
        //    objCommon.CurrentPage = Pagetax.ShowRows;
        //    // bindgvjobterm();
        //}
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                topmenu.MenuId = GetParentMenuId();
                topmenu.ModuleId =ModuleId;
                topmenu.RoleID = Convert.ToInt32(Session["RoleId"].ToString());
                topmenu.SelectedMenu = Convert.ToInt32(mid.ToString());
                topmenu.DataBind();
                Session["menuname"] = menuname;
                Session["menuid"] = menuid;
                Session["MId"] = mid;
                CompanyID = Convert.ToInt32(Session["CompanyID"].ToString());

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
        protected void btnSub_Click(object sender, EventArgs e)
        {
            int ds;
            int empid=0; int Descid=0; int Respid=0;
            SqlParameter[] p = new SqlParameter[3];
            if (txtEmployeeName.Text.Trim()!="")
            {
                empid = Convert.ToInt32(txtEmployeeName_hid.Value == "" ? "0" : txtEmployeeName_hid.Value); ;
            }
            p[0] = new SqlParameter("@empid", empid);
            if (txtJobDescr.Text.Trim() != "")
            {
                Descid = Convert.ToInt32(txtJobDescr_hid.Value == "" ? "0" : txtJobDescr_hid.Value); ;
                p[1] = new SqlParameter("@Descid", Descid);
          
            }
            //p[1] = new SqlParameter("@Descid", Convert.ToInt32(txtJobDescr.Text.Trim()));
            if(txtResponsibilities.Text.Trim()!="")
            {
                Respid = Convert.ToInt32(ddlEmp_hid.Value == "" ? "0" : ddlEmp_hid.Value);
            }
            p[2] = new SqlParameter("@Respid", Respid);

            ds = SQLDBUtil.ExecuteNonQuery("T_HMS_EmployeeWise_JobDesc_JobResp_insert", p);
            if (ds > 0)
            {

                AlertMsg.MsgBox(Page, "Record Inserted Sucess");
            }
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
           
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {

        }

        protected void gvemp_RowCommand(object sender, GridViewCommandEventArgs e)
        {
        
        }

        protected void gvemp_RowDataBound(object sender, GridViewRowEventArgs e)
        {
        
        }

        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionListEmployee(string prefixText, int count, string contextKey)
        {
            DataSet ds = AttendanceDAC.GetGoogleSearch_by_EmpName_All(prefixText.Trim());
      
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
        public static string[] GetCompletionjobdespList(string prefixText, int count, string contextKey)
        {

            DataSet ds = AttendanceDAC.get_employewise_jobdescription(prefixText.Trim());//WSID
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
        public static string[] GetCompletionjobWorksiteList(string prefixText, int count, string contextKey)
        {

            DataSet ds = AttendanceDAC.getworksite(prefixText.Trim());//WSID
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