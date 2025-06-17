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
    public partial class EmpWorksiteMonthWise : AECLOGIC.ERP.COMMON.WebFormMaster
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
        }
        protected void Page_Load(object sender, EventArgs e)
        {
             try
            {
                lblStatus.Text = String.Empty;
                if (!IsPostBack)
                {
                    bindyear();
                    if (Request.QueryString.Count>0)
                    {
                        string Empid=""; int Month=0, Year=0;
                        if (Request.QueryString["Empid"] != null && Request.QueryString["Empid"] != string.Empty)
                        {
                            Empid =Request.QueryString["Empid"].ToString();
                        }
                        if (Request.QueryString["Month"] != null && Request.QueryString["Month"] != string.Empty)
                        {
                            Month = Convert.ToInt32(Request.QueryString["Month"].ToString());
                        }
                        if (Request.QueryString["Year"] != null && Request.QueryString["Year"] != string.Empty)
                        {
                            Year = Convert.ToInt32(Request.QueryString["Year"].ToString());
                        }
                        if (Empid != "" && Month != 0 && Year != 0)
                        {
                            ddlmonth.SelectedValue = Month.ToString();
                            ddlyear.SelectedValue = Year.ToString();
                            BindPager(Empid, Month, Year);
                        }
                    }
                }
             }
            catch 
            {
            }
        }
        void bindyear()
        {
            FIllObject.FillDropDown(ref ddlyear, "HMS_YearWise");
            DataSet ds = AttendanceDAC.GetCalenderYear();
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
            return items.ToArray(); ;// txtItems.ToArray();
        }
        protected void btnsearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtemp.Text == "")
                {
                    AlertMsg.MsgBox(Page, "Select Employee !", AlertMsg.MessageType.Warning);
                   // lblStatus.Text = "Select Employee";
                    lblStatus.ForeColor = System.Drawing.Color.Red;
                    return;
                }
                if (ddlmonth.SelectedIndex == 0)
                {
                    AlertMsg.MsgBox(Page, "Select Month !", AlertMsg.MessageType.Warning);
                    //lblStatus.Text = "Select Month";
                    lblStatus.ForeColor = System.Drawing.Color.Red;
                    return;
                }
                string EmpID = ""; int Month = 0; int Year = 0; int siteid = 0;
                gvEmp.DataSource = null;
                gvEmp.DataBind();
                string[] words = txtemp.Text.Split('-');
                EmpID = words[0];
                Month = Convert.ToInt32(ddlmonth.SelectedValue);
                Year = Convert.ToInt32(ddlyear.SelectedValue);
                BindPager(EmpID, Month, Year);
            }
            catch
            { }
        }
        void BindPager(string EmpID, int Month, int Year)
        {
            SqlParameter[] p = new SqlParameter[3];
            p[0] = new SqlParameter("@Empid",Convert.ToInt32(EmpID));
            p[1] = new SqlParameter("@Month", Month);
            p[2] = new SqlParameter("@Year", Year);
            DataSet ds = SqlHelper.ExecuteDataset("sh_Attendancedetails", p);
            if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0)
            {
                gvEmp.DataSource = ds;
            }
            else
                gvEmp.DataSource = null;
            gvEmp.DataBind();
        }
    }
}