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
    public partial class Encashment_AL : AECLOGIC.ERP.COMMON.WebFormMaster
    {
        #region Declaration
        int mid = 0;
        bool viewall;
        static int SearchCompanyID;
        string menuname;
        string menuid;
        HRCommon objHrCommon = new HRCommon();
        int EmpidQeryStrng;
        #endregion Declaration
        #region Pageload
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Convert.ToInt32(Request.QueryString["id"]) == 1)
            {
                dvAdd.Visible = true;
                dvEdit.Visible = false;
                EmpidQeryStrng = Convert.ToInt32(Request.QueryString["Empid"]);
                if (Request.QueryString["Empid"] != null || EmpidQeryStrng != 0)
                {
                    EmpidQeryStrng = Convert.ToInt32(Request.QueryString["Empid"]);
                    txtName.Text = EmpidQeryStrng.ToString();
                }
            }
            else
            {
                dvAdd.Visible = false;
                dvEdit.Visible = true;
            }
            if (Convert.ToInt32(Request.QueryString["id"]) == 2)
            {
                dvAdd.Visible = false;
                dvEdit.Visible = true;
                EmpidQeryStrng = Convert.ToInt32(Request.QueryString["Empid"]);
                if (Request.QueryString["Empid"] != null || EmpidQeryStrng != 0)
                {
                    EmpidQeryStrng = Convert.ToInt32(Request.QueryString["Empid"]);
                    txtSearchEmployee.Text = EmpidQeryStrng.ToString();
                }
                lblEmpName.Visible = false;
                txtSearchEmployee.Visible = false;
                btnSearch.Visible = true;
            }
            SearchCompanyID = Convert.ToInt32(Session["CompanyID"]);
            if (!IsPostBack)
            {
                GetParentMenuId();
                ViewState["FinYearId"] = "";
                txtOccuranceOOP.Text = "0";
                EmployeBind(objHrCommon);
            }
        }
        #endregion Pageload
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
        #region Supporting Methods
        public int GetParentMenuId()
        {
            string URL = Request.Url.Segments[Request.Url.Segments.Length - 1] + Request.Url.Query;
            int RoleId = Convert.ToInt32(Session["RoleId"].ToString());
            int ModuleId = ModuleID; ;
           DataSet ds = AttendanceDAC.GetAllowed(RoleId, ModuleId, URL);
            int MenuId = 0;
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                MenuId = Convert.ToInt32(ds.Tables[0].Rows[0]["Under"]);
                gvFinancialYear.Columns[3].Visible = Convert.ToBoolean(ds.Tables[0].Rows[0]["Editable"].ToString());
            }
            return MenuId;
        }
        protected void btnSearch_Click(object sender,EventArgs e)
        {
            EmpListPaging.CurrentPage = 1;
            EmployeBind(objHrCommon);
        }
        void EmployeBind(HRCommon objHrCommon)
        {
            try
            {
                int EMPLid = 0;
                if (Convert.ToInt32(Request.QueryString["id"]) == 2)
                {
                    EmpidQeryStrng = Convert.ToInt32(Request.QueryString["Empid"]);
                    if (Request.QueryString["Empid"] != null || EmpidQeryStrng != 0)
                    {
                        EMPLid = Convert.ToInt32(Request.QueryString["Empid"]);
                    }
                }
                else
                {
                    if (txtSearchEmployee.Text != "")
                    {
                        EMPLid = Convert.ToInt32(txtSearchEmployee.Text.Substring(0, 4));
                    }
                }
                objHrCommon.PageSize = EmpListPaging.ShowRows;
                objHrCommon.CurrentPage = EmpListPaging.CurrentPage;
                //bool Status = false;
                int id = 5;
                DateTime dt = ConverttoDateFormat(txtlvrd.Text.Trim(), DateFormat.DayMonthYear);
              DataSet  ds = PayRollMgr.InsUpdate_Encashment_AL_Grid(objHrCommon, id, EMPLid, dt, 0, 0, null);
                gvFinancialYear.DataSource = ds;
                gvFinancialYear.DataBind();
                EmpListPaging.Bind(objHrCommon.CurrentPage, objHrCommon.TotalPages, objHrCommon.NoofRecords, objHrCommon.PageSize);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public enum DateFormat { DayMonthYear = 0, MonthDayYear = 1, ddMMMyyyy = 2 }
        public static DateTime ConverttoDateFormat(string StrDate, DateFormat Format)
        {
            DateTime dt = new DateTime(); if (StrDate != "")
            {
                switch (Format)
                {
                    case DateFormat.DayMonthYear: dt = new DateTime(Convert.ToInt32(StrDate.Split('/')[2]), Convert.ToInt32(StrDate.Split('/')[1]), Convert.ToInt32(StrDate.Split('/')[0]));
                        break;
                    case DateFormat.MonthDayYear: dt = new DateTime(Convert.ToInt32(StrDate.Split('/')[2]), Convert.ToInt32(StrDate.Split('/')[0]), Convert.ToInt32(StrDate.Split('/')[1]));
                        break;
                    case DateFormat.ddMMMyyyy: dt = new DateTime(Convert.ToInt32(StrDate.Split(' ')[2]), getMonth(StrDate.Split(' ')[1]), Convert.ToInt32(StrDate.Split(' ')[0]));
                        break;
                    default: break;
                }
            } return dt;
        }
        public enum Month { JAN = 1, FEB = 2, MAR = 3, APR = 4, MAY = 5, JUN = 6, JUL = 7, AUG = 8, SEP = 9, OCT = 10, NOV = 11, DEC = 12 }
        private static int getMonth(string sMonth) { int rVal = 0; switch (sMonth.ToUpper().Trim()) { case "JAN":rVal = Convert.ToInt32(Month.JAN); break; case "FEB":rVal = Convert.ToInt32(Month.FEB); break; case "MAR":rVal = Convert.ToInt32(Month.MAR); break; case "APR":rVal = Convert.ToInt32(Month.APR); break; case "MAY":rVal = Convert.ToInt32(Month.MAY); break; case "JUN":rVal = Convert.ToInt32(Month.JUN); break; case "JUL":rVal = Convert.ToInt32(Month.JUL); break; case "AUG":rVal = Convert.ToInt32(Month.AUG); break; case "SEP":rVal = Convert.ToInt32(Month.SEP); break; case "OCT":rVal = Convert.ToInt32(Month.OCT); break; case "NOV":rVal = Convert.ToInt32(Month.NOV); break; case "DEC":rVal = Convert.ToInt32(Month.DEC); break; } return rVal; }
        public void BindDetails(int EmpId)
        {
            objHrCommon.PageSize = 1;
            objHrCommon.CurrentPage = 1;
            objHrCommon.NoofRecords = 1;
            int id = 5;
            DateTime dt = ConverttoDateFormat(txtlvrd.Text.Trim(), DateFormat.ddMMMyyyy);
            dvEdit.Visible = false;
            dvAdd.Visible = true;
            DataSet ds = PayRollMgr.InsUpdate_Encashment_AL_Grid(objHrCommon, id, EmpId, dt, 0, 0, null);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                txtlvrd.Text = ds.Tables[0].Rows[0]["LVRD"].ToString();
                txtlopt.Text = ds.Tables[0].Rows[0]["Lop2"].ToString();
                txtName.Text = ds.Tables[0].Rows[0]["Empid"].ToString();
                txtlop2v.Text = ds.Tables[0].Rows[0]["Lop2V"].ToString();
                txtAAL.Text = ds.Tables[0].Rows[0]["AAl"].ToString();
                txtOccuranceOOP.Text = ds.Tables[0].Rows[0]["occuranceoop"].ToString();
                txtActionDt.Text = ds.Tables[0].Rows[0]["ActionDt"].ToString();
            }
        }
        public void Clear()
        {
            txtlvrd.Text = "";
            txtlopt.Text = "";
            txtAAL.Text = "";
            txtName.Text = "";
            ViewState["Empid"] = "";
        }
        #endregion Supporting Methods
        #region Events
        protected void gvFinancialYear_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int EmpId = Convert.ToInt32(e.CommandArgument);
            ViewState["EmpId"] = EmpId;
            if (e.CommandName == "Edt")
            {
                BindDetails(EmpId);
                btnSubmit.Text = "Update";
                txtName.Enabled = false;
            }
            if (e.CommandName == "Del")
            {
                int id = 3;
                DateTime dt = ConverttoDateFormat(txtlvrd.Text.Trim(), DateFormat.DayMonthYear);
                dvEdit.Visible = false;
                dvAdd.Visible = true;
                DataSet ds = PayRollMgr.InsUpdate_Encashment_AL_Grid(objHrCommon, id, EmpId, dt, 0, 0, null);
                AlertMsg.MsgBox(Page, "Deleted sucessfully.!");
                dvAdd.Visible = false;
                dvEdit.Visible = true;
                EmployeBind(objHrCommon);
            }
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime datetext;
                string Empid; string Empname="";
                if (txtName.Text == "" || txtlvrd.Text == "") 
                { 
                    AlertMsg.MsgBox(Page, "Fill Mandatory Field.. ");
                    dvEdit.Visible = false;
                    dvAdd.Visible = true;
                    return;
                }
                else { 
                    int id = 0;
                    if (btnSubmit.Text == "Submit")
                    {
                        datetext = ConverttoDateFormat(txtlvrd.Text.Trim(), DateFormat.ddMMMyyyy);// ConverttoDateFormat(txtlvrd.Text.Trim(), DateFormat.DayMonthYear);
                        if (EmpidQeryStrng != 0 )
                        {
                            Empid = EmpidQeryStrng.ToString();
                        }
                        else
                        {
                            Empid = txtName.Text.Substring(0, 4);
                            Empname = txtName.Text.Remove(0, 4);
                        }
                         Empname = txtName.Text;
                        id = 1;
                    }
                    else {
                        datetext = ConverttoDateFormat(txtlvrd.Text.Trim(), DateFormat.ddMMMyyyy);// ConverttoDateFormat(txtlvrd.Text.Trim(), DateFormat.DayMonthYear);
                         Empid = txtName.Text;
                        id = 2;
                    }
                    if (txtlopt.Text == string.Empty)
                        txtlopt.Text = "0";
                    if (txtAAL.Text == string.Empty)
                        txtAAL.Text = "0";
                    if (txtlop2v.Text == string.Empty)
                        txtlop2v.Text = "0";
                    if (txtOccuranceOOP.Text == string.Empty)
                        txtOccuranceOOP.Text = "0";
                    int Output = PayRollMgr.InsUpdate_Encashment_AL(objHrCommon, id, Convert.ToInt32(Empid),datetext, Convert.ToInt32(txtlopt.Text), Convert.ToInt32(txtAAL.Text), Empname,
                        Convert.ToInt32(txtlop2v.Text), Convert.ToInt32(txtOccuranceOOP.Text),ConverttoDateFormat(txtActionDt.Text.Trim(), DateFormat.ddMMMyyyy));
                    if (Output == 1)
                        AlertMsg.MsgBox(Page, "Inserted sucessfully.!");
                    else if (Output == 2)
                        AlertMsg.MsgBox(Page, "Already exists.!");
                    else
                        AlertMsg.MsgBox(Page, "Updated sucessfully.!");
                    Clear();
                    dvAdd.Visible = false;
                    dvEdit.Visible = true;
                    EmployeBind(objHrCommon);
                    }
            }
            catch (Exception Cat)
            {
                AlertMsg.MsgBox(Page, Cat.Message.ToString(),AlertMsg.MessageType.Error);
            }
        }
        #endregion Events
        //Added by Rijwan : 13-04-2016
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute]
        public static string[] GetCompletionListEmp(string prefixText, int count, string contextKey)
        {
            DataSet ds = AttendanceDAC.GetGoogleSerachAllEmployee(prefixText);
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
    }
}