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
    public partial class MonthlyWorkingDaysAndPayableDays : AECLOGIC.ERP.COMMON.WebFormMaster
    {
        int mid = 0;
        bool viewall;
        string menuname;
        static int SearchCompanyID;
        static int Siteid = 0;
        static int EDeptid;
        static int EWsid;
        static int Empid;
        string menuid;
        bool Editable;
        HRCommon objHrCommon = new HRCommon();
       protected override void OnInit(EventArgs e)
        {
            ModuleID = 1;
            base.OnInit(e);
            taskpaging.FirstClick += new Paging.PageFirst(EmpListPaging_FirstClick);
            taskpaging.NextClick += new Paging.PageNext(EmpListPaging_FirstClick);
            taskpaging.PreviousClick += new Paging.PagePrevious(EmpListPaging_FirstClick);
            taskpaging.ChangeClick += new Paging.PageChange(EmpListPaging_FirstClick);
            taskpaging.LastClick += new Paging.PageLast(EmpListPaging_FirstClick);
            taskpaging.ShowRowsClick += new Paging.ShowRowsChange(EmpListPaging_ShowRowsClick);
            taskpaging.CurrentPage = 1;
        }
        #region Bindpager
        void EmpListPaging_FirstClick(object sender, EventArgs e)
        {
            BindPager();
        }
        void EmpListPaging_ShowRowsClick(object sender, EventArgs e)
        {
            taskpaging.CurrentPage = 1;
            BindPager();
        }
        public void BindPager()
        {
            objHrCommon.CurrentPage = taskpaging.CurrentPage;
            objHrCommon.PageSize = taskpaging.ShowRows;
            Bindgrid(objHrCommon);
        }
        #endregion
       protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
               
                SearchCompanyID = Convert.ToInt32(Session["CompanyID"]);
              
                if (!IsPostBack)
                {
                    DataSet startdate = AttendanceDAC.GetStartDate();
                    ViewState["startdate"] = 1;
                    ViewState["enddate"] = 1;
                    DataSet enddate = AttendanceDAC.GetEndDate();

                    ViewState["startdate"] = startdate.Tables[0].Rows[0][0].ToString();
                    ViewState["enddate"] = enddate.Tables[0].Rows[0][0].ToString();
                    bindyear();
                   BindPager();

                }
            }
            catch (Exception ex)
            {
                clsErrorLog.HMSEventLog(ex, "HRLeaveApplication", "Page_Load", "001");
            }
        }
  
        public void Bindgrid(HRCommon objHrCommon)
        {
            int EmpID = 0; int Month = 0; int Year = 0; int siteid = 0;
            gvmnwrkngdays.DataSource = null;
            gvmnwrkngdays.DataBind();
             string[] words = txtemp.Text.Split('-');
                    string EmpID1 = words[0];
                    string[] word = txtSearchdept.Text.Split('-');
                    string deptid = word[0];
                    if (txtSearchWorksite.Text != "" && txtSearchWorksite.Text != string.Empty)
                        siteid = Convert.ToInt32(txtSearchWorksite.Text.Substring(2, 4));
                    ViewState["siteid"] = siteid;

                   
                    if (ddlmonth.SelectedValue != "0")
                        Month = Convert.ToInt32(ddlmonth.SelectedItem.Value);
                     Year = Convert.ToInt32(ddlyear.SelectedItem.Value);

                    if (Month == 1)
                    {
                        Month = 12;
                        Year = Year - 1;
                    }
                    else
                        Month = Month - 1;
                    string stDate, enddate;
                    stDate = ViewState["startdate"] + "/" + Month + "/" + Year;
                    enddate = ViewState["enddate"] + "/" + ddlmonth.SelectedItem.Value + "/" + ddlyear.SelectedItem.Value;


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
            p[5] = new SqlParameter("@PayStartDt", startdate);
            p[6] = new SqlParameter("@PayEndDt", EndDate);
            p[7] = new SqlParameter("@WSID", siteid);
            p[8] = new SqlParameter("@deptid", deptid);
            DataSet ds = SqlHelper.ExecuteDataset("HMS_MonthlyWorkingDays", p);
            if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0)
            {
                gvmnwrkngdays.DataSource = ds;
                gvmnwrkngdays.DataBind();
            }
            int totpage = Convert.ToInt32(p[1].Value);
            int noofrec = Convert.ToInt32(p[2].Value);

            objHrCommon.TotalPages = totpage;
            objHrCommon.NoofRecords = noofrec;
            taskpaging.Visible = true;
            taskpaging.Bind(objHrCommon.CurrentPage, objHrCommon.TotalPages, objHrCommon.NoofRecords, objHrCommon.PageSize);
        }
       
        
        void bindyear()
        {
            FIllObject.FillDropDown(ref ddlyear, "HMS_YearWise");
          DataSet   ds = AttendanceDAC.GetCalenderYear();
            if (ds.Tables[0].Rows[0]["PreviousMonth"].ToString() == "0")
            {
                ddlmonth.SelectedValue = "12";

                int CurrentYear = Convert.ToInt32(ds.Tables[0].Rows[0]["CurrentYear"]);
                int PreviousYear = CurrentYear - 1;
                ddlyear.Items.FindByValue(PreviousYear.ToString()).Selected = true;

            }
            //if we are in same year and same month
            else
            {
                ddlmonth.SelectedValue = ds.Tables[0].Rows[0]["PreviousMonth"].ToString();
                if (ddlyear.Items.FindByValue(ds.Tables[0].Rows[0]["CurrentYear"].ToString()) != null)
                {
                    ddlyear.Items.FindByValue(ds.Tables[0].Rows[0]["CurrentYear"].ToString()).Selected = true;
                }
                else
                {
                    ddlyear.SelectedIndex = 0;
                    //ddlYear.Items.Count - 1
                }
            }
        }
        protected void btnsearch_Click(object sender, EventArgs e)
        {
            BindPager();
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetEmpDetail(string prefixText, int count, string contextKey)
        {         
            DataSet ds = AttendanceDAC.HR_GetEmp(prefixText.Trim());
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
         public static string[] ConvertStingArray(DataSet ds)
        {
            string[] rtval = Array.ConvertAll(ds.Tables[0].Select(), new Converter<DataRow, string>(DataRowTotable));
            return rtval;
        }
        public static string DataRowTotable(DataRow dr)
        {
            return dr["Name"].ToString();
        }

        protected void btnSynch_Click(object sender, EventArgs e)
        {
            int Month = 0; int Year = 0; int siteid = 0;
            if (Convert.ToInt32(ddlmonth.SelectedValue) != 0 && Convert.ToInt32(ddlyear.SelectedValue) != 0)
            {
                if (txtSearchWorksite.Text != "" && txtSearchWorksite.Text != string.Empty)
                //{
                    siteid = Convert.ToInt32(txtSearchWorksite.Text.Substring(2, 4));
                    ViewState["siteid"] = siteid;


                    if (ddlmonth.SelectedValue != "0")
                        Month = Convert.ToInt32(ddlmonth.SelectedItem.Value);
                    Year = Convert.ToInt32(ddlyear.SelectedItem.Value);

                    if (Month == 1)
                    {
                        Month = 12;
                        Year = Year - 1;
                    }
                    else
                        Month = Month - 1;
                    string stDate, enddate;
                    stDate = ViewState["startdate"] + "/" + Month + "/" + Year;
                    enddate = ViewState["enddate"] + "/" + ddlmonth.SelectedValue + "/" + ddlyear.SelectedItem.Value;


                    DateTime startdate = CodeUtilHMS.ConvertToDate(stDate, CodeUtilHMS.DateFormat.DayMonthYear);
                    DateTime EndDate = CodeUtilHMS.ConvertToDate(enddate, CodeUtilHMS.DateFormat.DayMonthYear);
                    SqlParameter[] sqlParams = new SqlParameter[2];

                    if(Empid!=0)
                    sqlParams[0] = new SqlParameter("@empid", Empid);
                    else
                        sqlParams[0] = new SqlParameter("@empid", DBNull.Value);

                    sqlParams[1] = new SqlParameter("@enddate", EndDate);

                    DataSet ds = SQLDBUtil.ExecuteDataset("USP_Monthlypaybledays", sqlParams);
                    BindPager();                    
              
            }
            else
                AlertMsg.MsgBox(Page, "Select Month/Year");
            
        }
      

        }
    }
