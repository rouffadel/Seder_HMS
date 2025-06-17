using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using Aeclogic.Common.DAL;
using System.IO;
using AECLOGIC.HMS.BLL;
using System.Configuration;
using System.Collections;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using AECLOGIC.ERP.COMMON;

namespace AECLOGIC.ERP.HMS
{
    public partial class MonthlyPayrollAuthorisations : AECLOGIC.ERP.COMMON.WebFormMaster
    {
        #region Variables
        AttendanceDAC objEmployee = new AttendanceDAC();
        AttendanceDAC objRights = new AttendanceDAC();
        HRCommon objHrCommon = new HRCommon();
          
        static int SearchCompanyID;
        static int Siteid;
      
        int OrderID = 0, Direction = 0;
        #endregion Variables

        #region Events
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
               
                SearchCompanyID = Convert.ToInt32(Session["CompanyID"]);

              
                if (!IsPostBack)
                {

                    BindYears();
                    DataSet startdate = AttendanceDAC.GetStartDate();
                    ViewState["startdate"] = 1;
                    ViewState["enddate"] = 1;
                    DataSet enddate = AttendanceDAC.GetEndDate();

                    ViewState["startdate"] = startdate.Tables[0].Rows[0][0].ToString();
                    ViewState["enddate"] = enddate.Tables[0].Rows[0][0].ToString();
                    BindPager();
                 }    
            }
            catch (Exception ex)
            {
                clsErrorLog.HMSEventLog(ex, "Salaries", "Page_Load", "001");
            }
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
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute]
        public static string[] GetCompletionList(string prefixText, int count, string contextKey)
        {
            DataSet ds = AttendanceDAC.GetWorkSites(prefixText);
            return ConvertStingArray(ds);
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute]
        public static string[] GetCompletionListdept(string prefixText, int count, string contextKey)
        {
            DataSet ds = AttendanceDAC.GetDepartment(prefixText, SearchCompanyID, Siteid);
            return ConvertStingArray(ds);
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetEmpdetails(string prefixText, int count, string contextKey)
        {
            DataSet ds = AttendanceDAC.HR_GetEmpdetails(prefixText.Trim());
            DataTable dt = ds.Tables[0];
            return ConvertStingArray(ds);
        }
        public static string[] ConvertStingArray(DataSet ds)
        {
            string[] rtval = Array.ConvertAll(ds.Tables[0].Select(), new Converter<DataRow, string>(DataRowTotable));
            return rtval;
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            EmployeBind(objHrCommon);
        }
        void BindPager()
        {
            objHrCommon.CurrentPage = EmpListPaging.CurrentPage;
            objHrCommon.PageSize = EmpListPaging.ShowRows;
            EmployeBind(objHrCommon);
        }
        public void BindYears()
        {

            FIllObject.FillDropDown(ref ddlYear, "HMS_YearWise");


          DataSet  ds = AttendanceDAC.GetCalenderYear();

           
            if (ds.Tables[0].Rows[0]["PreviousMonth"].ToString() == "0")
            {
                ddlMonth.SelectedValue = "12";

                int CurrentYear = Convert.ToInt32(ds.Tables[0].Rows[0]["CurrentYear"]);
                int PreviousYear = CurrentYear - 1;
                ddlYear.Items.FindByValue(PreviousYear.ToString()).Selected = true;

            }
            //if we are in same year and same month
            else
            {
                ddlMonth.SelectedValue = ds.Tables[0].Rows[0]["PreviousMonth"].ToString();
                if (ddlYear.Items.FindByValue(ds.Tables[0].Rows[0]["CurrentYear"].ToString()) != null)
                {
                    ddlYear.Items.FindByValue(ds.Tables[0].Rows[0]["CurrentYear"].ToString()).Selected = true;
                }
                else
                {
                    ddlYear.SelectedIndex = 0;
                    //ddlYear.Items.Count - 1
                }
            }
            //#endregion set defalult month and year
        }
       
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
        void EmployeBind(HRCommon objHrCommon)
        
        {
            try
            {
                objHrCommon.PageSize = EmpListPaging.ShowRows;
                objHrCommon.CurrentPage = EmpListPaging.CurrentPage;
                int EmpID = 0; int siteid = 0; int deptid = 0;
                string dept = txtSearchdept.Text;
                gvmonthlypayroll.DataSource = null;
                gvmonthlypayroll.DataBind();
                string[] words = txtSearchdept.Text.Split('-');
                string deptid1 = words[0];
                string[] word = txtemp.Text.Split('-');
                string EmpID1 = word[0];
                if (txtSearchWorksite.Text != "" && txtSearchWorksite.Text != string.Empty)
                    siteid = Convert.ToInt32(txtSearchWorksite.Text.Substring(2, 4));
                ViewState["siteid"] = siteid;

                int Month = 0;
                if (ddlMonth.SelectedValue != "0")
                    Month = Convert.ToInt32(ddlMonth.SelectedItem.Value);
                int Year = Convert.ToInt32(ddlYear.SelectedItem.Value);

                if (Month == 1)
                {
                    Month = 12;
                    Year = Year - 1;
                }
                else
                    Month = Month - 1;
                string stDate, enddate;
                stDate = ViewState["startdate"] + "/" + Month + "/" + Year;
                enddate = ViewState["enddate"] + "/" + ddlMonth.SelectedItem.Value + "/" + ddlYear.SelectedItem.Value;


                DateTime startdate = CodeUtilHMS.ConvertToDate(stDate, CodeUtilHMS.DateFormat.DayMonthYear);
                DateTime EndDate = CodeUtilHMS.ConvertToDate(enddate, CodeUtilHMS.DateFormat.DayMonthYear);
                
                SqlParameter[] p = new SqlParameter[9];
                p[0] = new SqlParameter("@CurrentPage", objHrCommon.CurrentPage);
                p[1] = new SqlParameter("@PageSize", objHrCommon.PageSize);
                p[2] = new SqlParameter("@NoOfRecords", System.Data.SqlDbType.Int);
                p[2].Direction = ParameterDirection.Output;
                p[3] = new SqlParameter("returnvalue", System.Data.SqlDbType.Int);
                p[3].Direction = ParameterDirection.ReturnValue;
                p[4] = new SqlParameter("@EmpID", EmpID1);
                p[5] = new SqlParameter("@Wsid", siteid);
                p[6] = new SqlParameter("@Deptid", deptid1);

                p[7] = new SqlParameter("@paystartdate", startdate);
                p[8] = new SqlParameter("@payenddate", EndDate);

            
                DataSet ds = SqlHelper.ExecuteDataset("HMS_MonthlyPayroll", p);
                if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0)
                {
                    gvmonthlypayroll.DataSource = ds;
                    gvmonthlypayroll.DataBind();
                   
                }
                int totpage = Convert.ToInt32(p[1].Value);
                int noofrec = Convert.ToInt32(p[2].Value);

                objHrCommon.TotalPages = totpage;
                objHrCommon.NoofRecords = noofrec;
                EmpListPaging.Visible = true;
                EmpListPaging.Bind(objHrCommon.CurrentPage, objHrCommon.TotalPages, objHrCommon.NoofRecords, objHrCommon.PageSize);
            }
            catch (Exception e)
            {
                throw e;
            }
        }        
       
        public static string DataRowTotable(DataRow dr)
        {
            return dr["Name"].ToString();
        }
        #endregion Methods

        protected void gvmonthlypayroll_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[0].Visible = false;
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[0].Visible = false;
            }
        }
    }
}
