using System;
using System.IO;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using AECLOGIC.ERP.COMMON;
using Aeclogic.Common.DAL;
using AECLOGIC.HMS.BLL;
namespace AECLOGIC.ERP.HMS
{
    public partial class SalarySignOffStatement : AECLOGIC.ERP.COMMON.WebFormMaster
    {
        HRCommon objHrCommon = new HRCommon();
        string menuid = null; string menuname = null; int mid = 0; bool viewall, Editable;
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
        protected void Page_Load(object sender, EventArgs e)
        {
            try { string id = Session["UserId"].ToString(); }
            catch { Response.Redirect("Login.aspx"); }
            try
            {
                GetParentMenuId();
                Session["menuname"] = menuname;
                Session["menuid"] = menuid;
                Session["MId"] = mid;
                EmpListPaging.Visible = false;
                if (!IsPostBack)
                {
                    bindddl();
                    if (Request.QueryString.Count > 0)
                    {
                        int id = Convert.ToInt32(Request.QueryString["key"]);
                        if (id == 1)
                        {
                            hdID.Value = "0";
                            btnSubmit.Text = "Save";
                            NewView.Visible = true;
                            EditView.Visible = false;
                        }
                        else
                        {
                            NewView.Visible = false;
                            EditView.Visible = true;
                            BindPager();
                        }
                    }
                    else
                    {
                        NewView.Visible = false;
                        EditView.Visible = true;
                        BindPager();
                    }
                }
            }
            catch (Exception ex) { clsErrorLog.HMSEventLog(ex, "ReqfromOMS", "Page_Load", "001"); }
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            GridBind(objHrCommon);
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                int fCase = 0;
                if (btnSubmit.Text == "Save")
                    fCase = 1;
                else
                    fCase = 2;
                SqlParameter[] parms = new SqlParameter[7];
                parms[0] = new SqlParameter("@fCase", fCase);
                parms[1] = new SqlParameter("@ID", hdID.Value);
                parms[2] = new SqlParameter("@EmpID", ddlEmpID.SelectedValue.Trim());
                parms[3] = new SqlParameter("@prifix", txtprifix.Text.Trim());
                parms[4] = new SqlParameter("@DisplayDesignation", txtDisplayDesignation.Text.Trim());
                parms[5] = new SqlParameter("@OrderOfSing", ddlOrderOfSing.SelectedValue.Trim());
                parms[6] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int); parms[6].Direction = ParameterDirection.ReturnValue;
                int Output = SqlHelper.ExecuteNonQuery("SP_Th_SignofSalaryStatement_Insert_Update_Delete_VIEW_Paging_Select", parms);
                if (Output == 1) { AlertMsg.MsgBox(Page, "Saved! "); GridBind(objHrCommon); EditView.Visible = true; NewView.Visible = false; }
                else if (Output == 2) { AlertMsg.MsgBox(Page, "Already Exists!"); }
                else if (Output == 3) { AlertMsg.MsgBox(Page, "Updated!"); EditView.Visible = true; NewView.Visible = false; }
                else if (Output != 1 && Output != 2 && Output != 3) { AlertMsg.MsgBox(Page, "Already Exists!"); }
                if (Output == 1 || Output == 3) clear();
            }
            catch (Exception ex) { clsErrorLog.HMSEventLog(ex, "ReqfromOMS", "btnSubmit_Click", "002"); }
        }
        protected void gvEV_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int ID = Convert.ToInt32(e.CommandArgument);
                if (e.CommandName == "Edt") { BindDetails(ID); btnSubmit.Text = "Update"; }
                else if (e.CommandName == "Del")
                {
                    SqlParameter[] parms = new SqlParameter[2];
                    parms[0] = new SqlParameter("@ID", ID);
                    parms[1] = new SqlParameter("@fCase", 3);
                    int Output = SqlHelper.ExecuteNonQuery("SP_Th_SignofSalaryStatement_Insert_Update_Delete_VIEW_Paging_Select", parms);
                    if (Output == 4) AlertMsg.MsgBox(Page, "Record Deleted!");
                }
                else { GridBind(objHrCommon); }
            }
            catch (Exception ex) { clsErrorLog.HMSEventLog(ex, "ReqfromOMS", "gvEV_RowCommand", "003"); }
        }
        public void clear() { hdID.Value = "0"; ddlEmpID.SelectedValue = "0"; txtprifix.Text = ""; txtDisplayDesignation.Text = ""; ddlOrderOfSing.SelectedValue = "0"; }
        void EmpListPaging_ShowRowsClick(object sender, EventArgs e)
        {
            EmpListPaging.CurrentPage = 1;
            BindPager();
        }
        void EmpListPaging_FirstClick(object sender, EventArgs e)
        {
            BindPager();
        }
        public void BindPager()
        {
            objHrCommon.PageSize = EmpListPaging.CurrentPage;
            objHrCommon.CurrentPage = EmpListPaging.ShowRows;
            GridBind(objHrCommon);
            NewView.Visible = false;
        }
        void GridBind(HRCommon objHrCommon)
        {
            try
            {
                objHrCommon.PageSize = EmpListPaging.ShowRows;
                objHrCommon.CurrentPage = EmpListPaging.CurrentPage; SqlParameter[] parms = new SqlParameter[7];
                parms[0] = new SqlParameter("@fCase", 4);
                parms[1] = new SqlParameter("@CurrPage", objHrCommon.CurrentPage);
                parms[2] = new SqlParameter("@PageSize", objHrCommon.PageSize);
                parms[3] = new SqlParameter("@NrRecords", System.Data.SqlDbType.Int);
                parms[3].Direction = ParameterDirection.Output;
                parms[4] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
                parms[4].Direction = ParameterDirection.ReturnValue; int hdEmpID = 0; if (txtSEmpID.Text.Trim() != "") { hdEmpID = Convert.ToInt32(hdSEmpID.Value == "" ? "0" : hdSEmpID.Value); } if (hdEmpID != 0) parms[6] = new SqlParameter("@EmpID", ddlEmpID.SelectedValue); else parms[6] = new SqlParameter("@EmpID", SqlDbType.Int);
                DataSet ds = SqlHelper.ExecuteDataset("SP_Th_SignofSalaryStatement_Insert_Update_Delete_VIEW_Paging_Select", parms);
                objHrCommon.NoofRecords = (int)parms[3].Value;
                objHrCommon.TotalPages = (int)parms[4].Value;
                if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ViewState["Dataset"] = ds;
                    gvEV.DataSource = ds;
                    gvEV.DataBind();
                }
                else
                {
                    gvEV.DataSource = null;
                    gvEV.DataBind();
                }
                EmpListPaging.Visible = false;
                EmpListPaging.Bind(objHrCommon.CurrentPage, objHrCommon.TotalPages, objHrCommon.NoofRecords, objHrCommon.PageSize);
            }
            catch (Exception ex) { clsErrorLog.HMSEventLog(ex, "ReqfromOMS", "GridBind", "004"); }
        }
        public int GetParentMenuId()
        {
            if (Session["UserId"] == null) { Response.Redirect("Logon.aspx"); }
            string URL = Request.Url.Segments[Request.Url.Segments.Length - 1] + Request.Url.Query;
            int RoleId = Convert.ToInt32(Session["RoleId"]);
            int ModuleId = ModuleID; ;
            DataSet ds = new DataSet();
            ds = AttendanceDAC.GetAllowed(RoleId, ModuleId, URL);
            int MenuId = 0;
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                MenuId = Convert.ToInt32(ds.Tables[0].Rows[0]["Under"]);
                //ViewState["Editable"] = Editable = (bool)ds.Tables[0].Rows[0]["Editable"];
                //ViewState["ViewAll"] = (bool)ds.Tables[0].Rows[0]["ViewAll"];
                //btnSubmit.Enabled = Convert.ToBoolean(ds.Tables[0].Rows[0]["Editable"]);
                //viewall = (bool)ViewState["ViewAll"];
                menuname = ds.Tables[0].Rows[0]["menuname"].ToString();
                menuid = MenuId.ToString();
                mid = Convert.ToInt32(ds.Tables[0].Rows[0]["MenuId"]);
                btnSubmit.Enabled = Editable;
            } return MenuId;
        }
        public void BindDetails(int ID)
        {
            try
            {
                SqlParameter[] parms = new SqlParameter[3];
                parms[0] = new SqlParameter("@ID", ID);
                parms[1] = new SqlParameter("@OrderOfSing", 1);
                parms[2] = new SqlParameter("@fCase", 5);
                DataSet ds = SqlHelper.ExecuteDataset("SP_Th_SignofSalaryStatement_Insert_Update_Delete_VIEW_Paging_Select", parms);
                if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0)
                {
                    EditView.Visible = false; NewView.Visible = true; hdID.Value = ds.Tables[0].Rows[0]["ID"].ToString(); ddlEmpID.SelectedValue = ds.Tables[0].Rows[0]["EmpID"].ToString(); txtprifix.Text = ds.Tables[0].Rows[0]["prifix"].ToString(); txtDisplayDesignation.Text = ds.Tables[0].Rows[0]["DisplayDesignation"].ToString(); ddlOrderOfSing.SelectedValue = ds.Tables[0].Rows[0]["OrderOfSing"].ToString();
                }
                else { }
            }
            catch (Exception ex) { clsErrorLog.HMSEventLog(ex, "ReqfromOMS", "BindDetails", "005"); }
        }
        public static DateTime Convertdate(string StrDate) { DateTime dt = new DateTime(); if (StrDate != "") { dt = new DateTime(Convert.ToInt32(StrDate.Split('/')[2]), Convert.ToInt32(StrDate.Split('/')[1]), Convert.ToInt32(StrDate.Split('/')[0])); } return dt; }
        public static DataSet SearchEmpID(String SearchKey)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[2];
                param[0] = new SqlParameter("@sName", SearchKey);
                param[1] = new SqlParameter("@fCase", 1);
                DataSet ds = SqlHelper.ExecuteDataset("Th_SignofSalaryStatement_ddl", param);
                return ds;
            }
            catch (Exception e) { throw e; }
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionEmpID(string prefixText, int count, string contextKey)
        {
            DataSet ds = SearchEmpID(prefixText);
            DataTable dt = ds.Tables[0];
            List<string> items = new List<string>(count);
            var rtval = new Dictionary<string, string>();
            foreach (DataRow row in dt.Rows)
            {
                string str = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(row["Name"].ToString(), row["ID"].ToString());
                items.Add(str);
            } return items.ToArray();
        }
        private void bindddl()
        {
            SqlParameter[] parms1 = new SqlParameter[1];
            parms1[0] = new SqlParameter("@fCase", 1);
            FIllObject.FillDropDown(ref ddlEmpID, "Th_SignofSalaryStatement_ddl", parms1);
            SqlParameter[] parms2 = new SqlParameter[1];
            parms2[0] = new SqlParameter("@fCase", 2);
            FIllObject.FillDropDown(ref ddlOrderOfSing, "Th_SignofSalaryStatement_ddl", parms2);
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
    }
}
