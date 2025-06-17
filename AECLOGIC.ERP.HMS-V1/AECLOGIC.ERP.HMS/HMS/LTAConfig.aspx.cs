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
using System.Data.SqlClient;
using System.Collections.Generic;
using AECLOGIC.HMS.BLL;
using AECLOGIC.ERP.COMMON;
namespace AECLOGIC.ERP.HMS
{
    public partial class LTAConfig : AECLOGIC.ERP.COMMON.WebFormMaster
    {
        AttendanceDAC objEmployee = new AttendanceDAC();
        AttendanceDAC objRights = new AttendanceDAC();
        HRCommon objHrCommon = new HRCommon();
        int mid = 0;
        bool viewall;
        string menuname;
        string menuid;
        static int WSID = 0;
        static int CompanyID;
        static int Site = 0;
        string ReturnVal = "";
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
        protected void Page_Load(object sender, EventArgs e)
        {
            CompanyID = Convert.ToInt32(Session["CompanyID"]);
            Session["MId"] = mid;
            Ajax.Utility.RegisterTypeForAjax(typeof(AjaxDAL));
            if (!IsPostBack)
            {
                BindNonCTCComponents();
                EmployeBind(objHrCommon);
            }
        }
        public void BindNonCTCComponents()
        {
            DataSet dscomp = PayRollMgr.GetNonCTCComponentsList();
            ddlNCTCComponent.DataSource = dscomp;
            ddlNCTCComponent.DataValueField = "CompID";
            ddlNCTCComponent.DataTextField = "LongName";
            ddlNCTCComponent.DataBind();
        }
        void EmployeBind(HRCommon objHrCommon)
        {
            try
            {
                int? CompID = null;
                objHrCommon.PageSize = EmpListPaging.ShowRows;
                objHrCommon.CurrentPage = EmpListPaging.CurrentPage;
                //objHrCommon.SiteID = Convert.ToInt32(ddlworksites.SelectedItem.Value);
                objHrCommon.SiteID = Convert.ToInt32(Convert.ToInt32(ddlworksites_hid.Value == "" ? "0" : ddlworksites_hid.Value));
               // objHrCommon.DeptID = Convert.ToDouble(ddldepartments.SelectedItem.Value);
                objHrCommon.DeptID = Convert.ToDouble(Convert.ToInt32(ddldepartments_hid.Value == "" ? "0" : ddldepartments_hid.Value));
                if (txtEmpid.Text == null || txtEmpid.Text == "")
                    objHrCommon.EmpID = 0;
                else
                    objHrCommon.EmpID = Convert.ToDouble(txtEmpid.Text);
                if (ddlNCTCComponent.SelectedItem != null)
                {
                    if (ddlNCTCComponent.SelectedItem.Value != "0")
                    {
                        CompID = Convert.ToInt32(ddlNCTCComponent.SelectedItem.Value);
                    }
                }
                gdvAttend.DataSource = null;
                gdvAttend.DataBind();
               DataSet ds = (DataSet)objEmployee.GetLTAConfigList(objHrCommon, CompID);
                if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0)
                {
                    gdvAttend.DataSource = ds;
                    gdvAttend.DataBind();
                }
                EmpListPaging.Bind(objHrCommon.CurrentPage, objHrCommon.TotalPages, objHrCommon.NoofRecords, objHrCommon.PageSize);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            EmployeBind(objHrCommon);
        }
        protected void gdvAttend_RowDataBound(object sender, GridViewRowEventArgs e)
       {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                RadioButtonList chkSelect = (RadioButtonList)e.Row.FindControl("rbtlstType");
                Label lblSts = (Label)e.Row.FindControl("lblChkvalue");
                Label lbEmpID = (Label)e.Row.FindControl("lblEmpID");
                if (lblSts.Text != "")
                {
                    chkSelect.SelectedValue = lblSts.Text;
                }
                chkSelect.SelectedIndex = 0;
                chkSelect.Attributes.Add("onclick", "javascript:return InsUpdLTAConfig('" + chkSelect.ClientID + "','" + lbEmpID.Text + "');");
                for (int i = 1; i < chkSelect.Items.Count;i++)
                {
                    chkSelect.Items[i].Enabled = false;
                }
                //added by Ravitheja on 25/03/2016 for bug number Raised by koti HMS68.Payroll >> Monthly Salaries >> View 
                //http://scc.aeclogic.com/HMS/HMS/Salaries.aspx
                // Non CTC components are calculating wrongly (when we select Non CTC configure as Quarterly).Koti told only monthly Radibutton is needed..so remaing are not enabled.checked by Narendra
                // chkSelect.Attributes.Add("onclick", "javascript:return confirm('Test');");
            }
        }
        protected void ddlNCTCComponent_SelectedIndexChanged(object sender, EventArgs e)
        {
            EmployeBind(objHrCommon);
        }
        [System.Web.Services.WebMethodAttribute(),System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetEmpidList(string prefixText)
        {
            return ConvertStingArray(AttendanceDAC.GetEmpid_By_Search(prefixText));
        }
        public static string[] ConvertStingArray(DataSet ds)
        {
            String[] rtval = Array.ConvertAll(ds.Tables[0].Select(), new Converter<DataRow, string>(DataRowToDouble));
            return rtval;
        }
        public static string DataRowToDouble(DataRow dr)
        {
            return dr["EmpId"].ToString();
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionList(string prefixText, int count, string contextKey)
        {
            DataSet ds = AttendanceDAC.HR_GetWorkSite_By_LTAConfigList_googlesearch(prefixText.Trim(), WSID);
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
        protected void GetWork(object sender, EventArgs e)
        {
            CompanyID = Convert.ToInt32(Session["CompanyID"]);
            WSID = Convert.ToInt32(ddlworksites_hid.Value == "" ? "0" : ddlworksites_hid.Value); ;
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionList_Department(string prefixText, int count, string contextKey)
        {
            DataSet ds = AttendanceDAC.HR_googlesearch_GetDepartmentBySite(prefixText.Trim(), WSID, CompanyID);
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
        protected void GetDept(object sender, EventArgs e)
        {
            Site = 0;
            Site = Convert.ToInt32(ddldepartments_hid.Value == "" ? "0" : ddldepartments_hid.Value); ;
            CompanyID = Convert.ToInt32(Session["CompanyID"]);
        }
    }
}