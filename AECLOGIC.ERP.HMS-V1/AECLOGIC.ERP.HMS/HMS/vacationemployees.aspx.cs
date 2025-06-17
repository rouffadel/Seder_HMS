using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using AECLOGIC.HMS.BLL;
using AECLOGIC.ERP.COMMON;
using System.Text;
using System.Data.SqlClient;
using Aeclogic.Common.DAL;

namespace AECLOGIC.ERP.HMS 
{
    public partial class vacationemployees : AECLOGIC.ERP.COMMON.WebFormMaster
    {
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
                int EmpID = 0;
                
                    EmpID = Convert.ToInt32(ddlsemp_hid.Value == "" ? "0" : ddlsemp_hid.Value);
                    SqlParameter[] sqlParams = new SqlParameter [5];
                    sqlParams[0] = new SqlParameter("@CurrentPage", objHrCommon.CurrentPage);
                    sqlParams[1] = new SqlParameter("@PageSize", objHrCommon.PageSize);
                    sqlParams[2] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
                    sqlParams[2].Direction = ParameterDirection.ReturnValue;
                    sqlParams[3] = new SqlParameter("@NoofRecords", System.Data.SqlDbType.Int);
                    sqlParams[3].Direction = ParameterDirection.Output;
                    if (EmpID == 0)
                        sqlParams[4] = new SqlParameter("@EmpID", System.Data.SqlDbType.Int);
                    else
                        sqlParams[4] = new SqlParameter("@EmpID", EmpID);
                    DataSet ds = SQLDBUtil.ExecuteDataset("sh_currentvactionemployees", sqlParams);
                    objHrCommon.NoofRecords = (int)sqlParams[3].Value;
                    objHrCommon.TotalPages = (int)sqlParams[2].Value;

                    if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        gvVacation.DataSource = ds;
                        gvVacation.DataBind();
                        EmpListPaging.Visible = true;
                    }
                    else
                    {
                        EmpListPaging.Visible = false;
                        gvVacation.DataSource = null;
                        gvVacation.DataBind();
                    }
                    EmpListPaging.Bind(objHrCommon.CurrentPage, objHrCommon.TotalPages, objHrCommon.NoofRecords, objHrCommon.PageSize);
                }
             
            catch { }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindPager();
            }
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionemployeeList(string prefixText, int count, string contextKey)
        {
            
            DataSet ds = AttendanceDAC.HR_GoogleSearchEmpBySiteDept(prefixText, 0, 0, null, 1);
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

        protected void btnsearch_Click(object sender, EventArgs e)
        {
            BindPager();
        }
    }
}