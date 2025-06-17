using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using AECLOGIC.HMS.BLL;
using AECLOGIC.ERP.COMMON;
using System.Data.SqlClient;
using Aeclogic.Common.DAL;
using System.Collections.Generic;
namespace AECLOGIC.ERP.HMS
{
    public partial class Clearence : AECLOGIC.ERP.COMMON.WebFormMaster
    {
        static int WS;
        static int Siteid;
        static int EDeptid = 0;
        static int SearchCompanyID;
        static int WSiteid;
        string ModuleId = System.Configuration.ConfigurationManager.AppSettings["ModuleId"];
        int CompanyID = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["CompanyID"]);
        HRCommon objHrCommon = new HRCommon();
        AttendanceDAC objAtt = new AttendanceDAC();
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
        void EmpListPaging_FirstClick(object sender, EventArgs e)
        {
            BindPager();
        }
        void EmpListPaging_ShowRowsClick(object sender, EventArgs e)
        {
            EmpListPaging.CurrentPage = 1;
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
                int? EmpID = null;
                int? Dept = null;
                int? WS = null;
                if (txtSearchemp.Text.Trim() != "")
                {
                    EmpID = Convert.ToInt32(ddlWorksite_hid.Value == "" ? "0" : ddlWorksite_hid.Value);
                }
                if (TxtDept.Text.Trim() != "")
                {
                    Dept = Convert.ToInt32(TxtDept_hid.Value == "" ? "0" : TxtDept_hid.Value);
                }
                if (Txtwrk.Text.Trim() != "")
                {
                    WS = Convert.ToInt32(Txtwrk_hid.Value == "" ? "0" : Txtwrk_hid.Value);
                }
                DataSet ds = AttendanceDAC.HR_Getemployeeclearence_EMP(objHrCommon, EmpID, WS, Dept, Convert.ToInt32(Session["CompanyID"]));
                if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0)
                {
                    gdvWS.DataSource = ds;
                    gdvWS.DataBind();
                }
                else
                {
                    gdvWS.DataSource = null;
                    gdvWS.DataBind();
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
            if (!IsPostBack)
            {
                BindPager();
            }
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            EmpListPaging.CurrentPage = 1;
            EmployeBind(objHrCommon);
        }
        protected void gdvWS_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int ID = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            Label lblProdId = (Label)row.FindControl("Label2");
            string name = lblProdId.Text;
            if (e.CommandName == "Receive")
            {
                int EmpID = Convert.ToInt32(e.CommandArgument);
                Label lblEmpID = (Label)gdvWS.FindControl("lblEmpID");
                Response.Redirect("clearenceview.aspx?Empid=" + EmpID + "&key=" + 1 + "&Name=" + name);
            }
            if (e.CommandName == "incostod")
            {
                int EmpID = Convert.ToInt32(e.CommandArgument);
                Label lblEmpID = (Label)gdvWS.FindControl("lblEmpID");
                AlertMsg.CallScriptFunction(Page, "showApp('" + EmpID + "&key=" + 1 + "&Name=" + name + "');");
            }
        }
        //for employee
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionList(string prefixText, int count, string contextKey)
        {
            DataSet ds = AttendanceDAC.HR_GoogleSerac_allemployee(prefixText);
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
        //fror worksite
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletiondeptWorkList(string prefixText, int count, string contextKey)
        {
            DataSet ds = AttendanceDAC.HR_GetWorkSite_By_empVsAirTicketsAuth(prefixText);
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
        //for department
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletiondeptList(string prefixText, int count, string contextKey)
        {
            DataSet ds = AttendanceDAC.GetSearchDesiginationFilterActive(prefixText, 1, Siteid);
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
        protected void GetDept(object sender, EventArgs e)
        {
            WSiteid = Convert.ToInt32(Txtwrk_hid.Value == "" ? "0" : Txtwrk_hid.Value);
            Siteid = Convert.ToInt32(Txtwrk_hid.Value == "" ? "0" : Txtwrk_hid.Value);
        }
    }
}