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
    public partial class HMS_Loan_clearence : AECLOGIC.ERP.COMMON.WebFormMaster
    {
        int mid = 0;
        bool viewall, Editable;
        string menuname;
        string menuid;
        static int WS;
        string name;
        string machineEmpname;
        HRCommon objHrCommon = new HRCommon();
        int EmpID;
        string ModuleId = System.Configuration.ConfigurationManager.AppSettings["ModuleId"];
        int CompanyID = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["CompanyID"]);
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
             BindGrid_gvhms(objHrCommon);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblname.Text = Session["Empname"].ToString();
                EmpListPaging.Visible = false;
                BindGrid_gvhms(objHrCommon);
            }
        }
        protected void lnkhms_search(object sender, EventArgs e)
        {
            BindGrid_gvhms(objHrCommon);
            EmpListPaging.Visible = true;
        }
        public void BindGrid_gvhms(HRCommon objHrCommon)
        {
            EmpID = Convert.ToInt32(Request.QueryString["Empid"].ToString());
            objHrCommon.PageSize = EmpListPaging.ShowRows;
            objHrCommon.CurrentPage = EmpListPaging.CurrentPage;
            DataSet ds = AttendanceDAC.HR_GetEmpAdvaces(0, 0, EmpID, "", 1, objHrCommon, 1, 0, Convert.ToInt32(Session["CompanyID"]));
            if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0)
            {
                gvhms.DataSource = ds;
                gvhms.DataBind();
            }
            else
            {
                gvhms.DataSource = ds;
                gvhms.DataBind();
            }
            EmpListPaging.Bind(objHrCommon.CurrentPage, objHrCommon.TotalPages, objHrCommon.NoofRecords, objHrCommon.PageSize);
        }
        protected string FormatMonth(object Status)
        {
            string retValue = "";
            string input = Status.ToString();
            if (input == "1")
            {
                retValue = "January";
            }
            if (input == "2")
            {
                retValue = "February";
            }
            if (input == "3")
            {
                retValue = "March";
            }
            if (input == "4")
            {
                retValue = "April";
            }
            if (input == "5")
            {
                retValue = "May";
            }
            if (input == "6")
            {
                retValue = "June";
            }
            if (input == "7")
            {
                retValue = "July";
            }
            if (input == "8")
            {
                retValue = "August";
            }
            if (input == "9")
            {
                retValue = "September";
            }
            if (input == "10")
            {
                retValue = "October";
            }
            if (input == "11")
            {
                retValue = "November";
            }
            if (input == "12")
            {
                retValue = "December";
            }
            return retValue;
        }
        protected void btnback_Click(object sender, EventArgs e)
        {
            EmpID = Convert.ToInt32(Request.QueryString["Empid"].ToString());
            Session["Empid"] = EmpID;
            // Session["Empname"] = Request.QueryString["Name"].ToString();
            string Ename = Session["Empname"].ToString();
            //  Response.Redirect("clearenceview.aspx?Empid=Empid");
            Response.Redirect("clearenceview.aspx?Empid=" + EmpID + "&key=" + 1 + "&Name=" + Ename);
        }
    }
}