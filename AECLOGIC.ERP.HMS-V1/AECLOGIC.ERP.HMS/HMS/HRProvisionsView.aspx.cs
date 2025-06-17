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
    public partial class HRProvisionsView : AECLOGIC.ERP.COMMON.WebFormMaster
    {
       
        static int WS;
        static int Siteid;
        static int EDeptid = 0;
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
            EmployeesBind(objHrCommon);
        }
        void EmployeesBind(HRCommon objHrCommon)
        {

            try
            {
                objHrCommon.PageSize = EmpListPaging.ShowRows;
                objHrCommon.CurrentPage = EmpListPaging.CurrentPage;
                int? EmpID = null;
                int? Year = null;
                int? Month = null;
                if (Convert.ToInt32(ddlmonth.SelectedValue) > 0)
                {
                    Month = Convert.ToInt32(ddlmonth.SelectedValue);
                }
                if (ddlyear.SelectedValue != "")
                {
                    Year = Convert.ToInt32(ddlyear.SelectedValue);
                }
               
                if (txtSearchemp.Text.Trim() != "")
                {
                    EmpID = Convert.ToInt32(txtSearchemp_hid.Value == "" ? "0" : txtSearchemp_hid.Value);
                }
                SqlParameter[] p = new SqlParameter[7];
                p[0] = new SqlParameter("@CurrentPage", objHrCommon.CurrentPage);
                p[1] = new SqlParameter("@PageSize", objHrCommon.PageSize);
                p[2] = new SqlParameter("@NoOfRecords", System.Data.SqlDbType.Int);
                p[2].Direction = ParameterDirection.Output;
                p[3] = new SqlParameter("returnvalue", System.Data.SqlDbType.Int);
                p[3].Direction = ParameterDirection.ReturnValue;
                p[4] = new SqlParameter("@EMPID", EmpID);
                p[5] = new SqlParameter("@Year", Year);
                p[6] = new SqlParameter("@Month", Month);
                DataSet ds = SqlHelper.ExecuteDataset("sh_HRProvisionCalculationView", p);
                if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0)
                {
                    gvview.DataSource = ds;
                    gvview.DataBind();
                }
                else
                {
                    gvview.DataSource = null;
                    gvview.DataBind();
                }
                objHrCommon.NoofRecords = (int)p[2].Value;
                objHrCommon.TotalPages = (int)p[3].Value;
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
                FIllObject.FillDropDown(ref ddlyear, "HMS_YearWse");
            }
        }
       
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindPager();
        }

        protected void btnback_Click(object sender, EventArgs e)
        {
            Response.Redirect("HRProvisions.aspx");
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionList(string prefixText, int count, string contextKey)
        {
            DataSet ds = AttendanceDAC.Getemployee_Search(prefixText);
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