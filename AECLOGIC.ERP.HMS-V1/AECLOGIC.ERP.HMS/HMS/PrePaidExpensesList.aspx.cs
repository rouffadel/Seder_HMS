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
using AECLOGIC.HMS.BLL;
using AECLOGIC.ERP.COMMON;
using Aeclogic.Common.DAL;
namespace AECLOGIC.ERP.HMS
{
    public partial class PrePaidExpensesList : AECLOGIC.ERP.COMMON.WebFormMaster
    {
        HRCommon objHrCommon = new HRCommon();
        string menuid = null; string menuname = null; int mid = 0; bool viewall, Editable;
        protected override void OnInit(EventArgs e)
        {
            if (Request.QueryString["ModuleID"] != null)
            {
                ModuleID = Convert.ToInt32(Request.QueryString["Moduleid"]);
            }
            else
            {
                ModuleID = 1;
            }
            base.OnInit(e);
            EmpListPaging.FirstClick += new Paging.PageFirst(EmpListPaging_FirstClick);
            EmpListPaging.PreviousClick += new Paging.PagePrevious(EmpListPaging_FirstClick);
            EmpListPaging.NextClick += new Paging.PageNext(EmpListPaging_FirstClick);
            EmpListPaging.LastClick += new Paging.PageLast(EmpListPaging_FirstClick);
            EmpListPaging.ChangeClick += new Paging.PageChange(EmpListPaging_FirstClick);
            EmpListPaging.ShowRowsClick += new Paging.ShowRowsChange(EmpListPaging_ShowRowsClick);
            EmpListPaging.CurrentPage = 1;
            PagingDet.FirstClick += new Paging.PageFirst(EmpListPaging_FirstClick);
            PagingDet.PreviousClick += new Paging.PagePrevious(EmpListPaging_FirstClick);
            PagingDet.NextClick += new Paging.PageNext(EmpListPaging_FirstClick);
            PagingDet.LastClick += new Paging.PageLast(EmpListPaging_FirstClick);
            PagingDet.ChangeClick += new Paging.PageChange(EmpListPaging_FirstClick);
            PagingDet.ShowRowsClick += new Paging.ShowRowsChange(EmpListPaging_ShowRowsClick);
            PagingDet.CurrentPage = 1;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            try { string id = Session["UserId"].ToString(); }
            catch { Response.Redirect("Login.aspx"); }
            try
            {
                //topmenu.MenuId = GetParentMenuId();
                //topmenu.ModuleId = ModuleID; ;
                //topmenu.RoleID = Convert.ToInt32(Session["RoleId"]);
                //topmenu.SelectedMenu = Convert.ToInt32(mid);
                //topmenu.DataBind();
                //Session["menuname"] = menuname;
                //Session["menuid"] = menuid;
                //Session["MId"] = mid;
                EmpListPaging.Visible = false;
                PagingDet.Visible = false;
                if (!IsPostBack)
                {
                    bindddl();
                    if (Request.QueryString.Count > 0)
                    {
                        int id = Convert.ToInt32(Request.QueryString["key"]);
                        if (id == 1)
                        {
                            hdMpEtID.Value = "0";
                            btnSubmit.Text = "Save";
                            NewView.Visible = true;
                            EditView.Visible = false;
                            MonthWiseDet.Visible = false;
                        }
                        else
                        {
                            MonthWiseDet.Visible = false;
                            NewView.Visible = false;
                            EditView.Visible = true;
                            BindPager();
                        }
                    }
                    else
                    {
                        MonthWiseDet.Visible = false;
                        NewView.Visible = false;
                        EditView.Visible = true;
                        BindPager();
                    }
                }
                if(ModuleID==2)
                {
                    txtSModuleID.Visible = true;
                }
                else
                {
                    txtSModuleID.Visible = false;
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
                SqlParameter[] parms = new SqlParameter[13];
                parms[0] = new SqlParameter("@fCase", fCase);
                parms[1] = new SqlParameter("@MpEtID", hdMpEtID.Value);
                parms[2] = new SqlParameter("@TransID", ddlTransID.SelectedValue.Trim());
                parms[3] = new SqlParameter("@Transamt", txtTransamt.Text.Trim());
                parms[4] = new SqlParameter("@ResourceID", ddlResourceID.SelectedValue.Trim());
                parms[5] = new SqlParameter("@ModuleID", ddlModuleID.SelectedValue.Trim()); DateTime dtcn = ConverttoDateFormat(txtcn.Text.Trim(), DateFormat.ddMMMyyyy);
                parms[6] = new SqlParameter("@cn", dtcn);
                parms[7] = new SqlParameter("@cb", ddlcb.SelectedValue.Trim());
                parms[8] = new SqlParameter("@NoOfMonths", ddlNoOfMonths.SelectedValue.Trim());
                parms[9] = new SqlParameter("@BillTransID", ddlBillTransID.SelectedValue.Trim());
                parms[10] = new SqlParameter("@wsiD", ddlwsiD.SelectedValue.Trim());
                parms[11] = new SqlParameter("@prjID", ddlprjID.SelectedValue.Trim());
                parms[12] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int); parms[12].Direction = ParameterDirection.ReturnValue;
                int Output = SqlHelper.ExecuteNonQuery("SP_tbl_MonthlyPrePaidExpensesTrans_Insert_Update_Delete_VIEW_Paging_Select", parms);
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
                if (e.CommandName == "MpEtID")
                {                    
                    ViewState["MpEtID"] = ID;
                    BindPagerDet();
                }
                else if (e.CommandName == "Edt") { BindDetails(ID); btnSubmit.Text = "Update"; }
                else if (e.CommandName == "Del")
                {
                    SqlParameter[] parms = new SqlParameter[2];
                    parms[0] = new SqlParameter("@MpEtID", ID);
                    parms[1] = new SqlParameter("@fCase", 3);
                    int Output = SqlHelper.ExecuteNonQuery("SP_tbl_MonthlyPrePaidExpensesTrans_Insert_Update_Delete_VIEW_Paging_Select", parms);
                    if (Output == 4) AlertMsg.MsgBox(Page, "Record Deleted!");
                }
                else { GridBind(objHrCommon); }
            }
            catch (Exception ex) { clsErrorLog.HMSEventLog(ex, "ReqfromOMS", "gvEV_RowCommand", "003"); }
        }
        public void clear() { hdMpEtID.Value = "0"; ddlTransID.SelectedValue = "0"; txtTransamt.Text = ""; ddlResourceID.SelectedValue = "0"; ddlModuleID.SelectedValue = "0"; txtcn.Text = ""; ddlcb.SelectedValue = "0"; ddlNoOfMonths.SelectedValue = "0"; ddlBillTransID.SelectedValue = "0"; ddlwsiD.SelectedValue = "0"; ddlprjID.SelectedValue = "0"; }
        // paginig details
        void PagingDet_ShowRowsClick(object sender, EventArgs e)
        {
            EmpListPaging.CurrentPage = 1;
            BindPagerDet();
        }
        void PagingDet_FirstClick(object sender, EventArgs e)
        {
            BindPagerDet();
        }
        public void BindPagerDet()
        {
            objHrCommon.PageSize = PagingDet.CurrentPage;
            objHrCommon.CurrentPage = PagingDet.ShowRows;
            int MpEtID = Convert.ToInt32(ViewState["MpEtID"]);
            GridBindDet(objHrCommon, MpEtID);
            NewView.Visible = false;
            MonthWiseDet.Visible = true;
        }
        void GridBindDet(HRCommon objHrCommon, int MpEtID)
        {
            try
            {
                objHrCommon.PageSize = EmpListPaging.ShowRows;
                objHrCommon.CurrentPage = EmpListPaging.CurrentPage; SqlParameter[] parms = new SqlParameter[6];
                parms[0] = new SqlParameter("@fCase", 6);
                parms[1] = new SqlParameter("@CurrPage", objHrCommon.CurrentPage);
                parms[2] = new SqlParameter("@PageSize", objHrCommon.PageSize);
                parms[3] = new SqlParameter("@NrRecords", System.Data.SqlDbType.Int);
                parms[3].Direction = ParameterDirection.Output;
                parms[4] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
                parms[4].Direction = ParameterDirection.ReturnValue;
                parms[5] = new SqlParameter("@MpEtID", MpEtID);
                DataSet ds = SqlHelper.ExecuteDataset("SP_tbl_MonthlyPrePaidExpensesTrans_Insert_Update_Delete_VIEW_Paging_Select", parms);
                objHrCommon.NoofRecords = (int)parms[3].Value;
                objHrCommon.TotalPages = (int)parms[4].Value;
                if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0)
                {
                    gvDet.DataSource = ds;
                    gvDet.DataBind();
                }
                else
                {
                    gvDet.DataSource = null;
                    gvDet.DataBind();
                }
                PagingDet.Visible = false;
                PagingDet.Bind(objHrCommon.CurrentPage, objHrCommon.TotalPages, objHrCommon.NoofRecords, objHrCommon.PageSize);
            }
            catch (Exception ex) { clsErrorLog.HMSEventLog(ex, "ReqfromOMS", "GridBind", "004"); }
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
        public void BindPager()
        {
            objHrCommon.PageSize = EmpListPaging.CurrentPage;
            objHrCommon.CurrentPage = EmpListPaging.ShowRows;
            GridBind(objHrCommon);
            NewView.Visible = false;
            MonthWiseDet.Visible = false;
        }
        void GridBind(HRCommon objHrCommon)
        {
            try
            {
                objHrCommon.PageSize = EmpListPaging.ShowRows;
                objHrCommon.CurrentPage = EmpListPaging.CurrentPage; SqlParameter[] parms = new SqlParameter[10];
                parms[0] = new SqlParameter("@fCase", 4);
                parms[1] = new SqlParameter("@CurrPage", objHrCommon.CurrentPage);
                parms[2] = new SqlParameter("@PageSize", objHrCommon.PageSize);
                parms[3] = new SqlParameter("@NrRecords", System.Data.SqlDbType.Int);
                parms[3].Direction = ParameterDirection.Output;
                parms[4] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
                parms[4].Direction = ParameterDirection.ReturnValue; int hdResourceID = 0; 
                if (txtSResourceID.Text.Trim() != "") { hdResourceID = Convert.ToInt32(hdSResourceID.Value == "" ? "0" : hdSResourceID.Value); }
                if (hdResourceID != 0)
                    parms[6] = new SqlParameter("@ResourceID", hdResourceID); 
                else
                    parms[6] = new SqlParameter("@ResourceID", SqlDbType.Int);
                if (ModuleID == 2)
                {
                    int hdModuleID = 0; if (txtSModuleID.Text.Trim() != "")
                    { hdModuleID = Convert.ToInt32(hdSModuleID.Value == "" ? "0" : hdSModuleID.Value); }
                    if (hdModuleID != 0) parms[7] = new SqlParameter("@ModuleID", hdModuleID);
                    else parms[7] = new SqlParameter("@ModuleID", SqlDbType.Int);
                }
                else
                {
                    parms[7] = new SqlParameter("@ModuleID", ModuleID);
                }
                int hdwsiD = 0; if (txtSwsiD.Text.Trim() != "") { hdwsiD = Convert.ToInt32(hdSwsiD.Value == "" ? "0" : hdSwsiD.Value); } if (hdwsiD != 0) parms[8] = new SqlParameter("@wsiD", hdwsiD); else parms[8] = new SqlParameter("@wsiD", SqlDbType.Int);
                int hdprjID = 0; if (txtSprjID.Text.Trim() != "") { hdprjID = Convert.ToInt32(hdSprjID.Value == "" ? "0" : hdSprjID.Value); } if (hdprjID != 0) parms[9] = new SqlParameter("@prjID", hdwsiD); else parms[9] = new SqlParameter("@prjID", SqlDbType.Int);
                DataSet ds = SqlHelper.ExecuteDataset("SP_tbl_MonthlyPrePaidExpensesTrans_Insert_Update_Delete_VIEW_Paging_Select", parms);
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
                ViewState["Editable"] = Editable = (bool)ds.Tables[0].Rows[0]["Editable"];
                ViewState["ViewAll"] = (bool)ds.Tables[0].Rows[0]["ViewAll"];
                btnSubmit.Enabled = Convert.ToBoolean(ds.Tables[0].Rows[0]["Editable"]);
                viewall = (bool)ViewState["ViewAll"];
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
                parms[0] = new SqlParameter("@MpEtID", ID);
                parms[1] = new SqlParameter("@prjID", 1);
                parms[2] = new SqlParameter("@fCase", 5);
                DataSet ds = SqlHelper.ExecuteDataset("SP_tbl_MonthlyPrePaidExpensesTrans_Insert_Update_Delete_VIEW_Paging_Select", parms);
                if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0)
                {
                    EditView.Visible = false; NewView.Visible = true; hdMpEtID.Value = ds.Tables[0].Rows[0]["MpEtID"].ToString(); ddlTransID.SelectedValue = ds.Tables[0].Rows[0]["TransID"].ToString(); txtTransamt.Text = ds.Tables[0].Rows[0]["Transamt"].ToString(); ddlResourceID.SelectedValue = ds.Tables[0].Rows[0]["ResourceID"].ToString(); ddlModuleID.SelectedValue = ds.Tables[0].Rows[0]["ModuleID"].ToString(); txtcn.Text = ds.Tables[0].Rows[0]["cn"].ToString(); ddlcb.SelectedValue = ds.Tables[0].Rows[0]["cb"].ToString(); ddlNoOfMonths.SelectedValue = ds.Tables[0].Rows[0]["NoOfMonths"].ToString(); ddlBillTransID.SelectedValue = ds.Tables[0].Rows[0]["BillTransID"].ToString(); ddlwsiD.SelectedValue = ds.Tables[0].Rows[0]["wsiD"].ToString(); ddlprjID.SelectedValue = ds.Tables[0].Rows[0]["prjID"].ToString();
                }
                else { }
            }
            catch (Exception ex) { clsErrorLog.HMSEventLog(ex, "ReqfromOMS", "BindDetails", "005"); }
        }
        public static DateTime Convertdate(string StrDate) { DateTime dt = new DateTime(); if (StrDate != "") { dt = new DateTime(Convert.ToInt32(StrDate.Split('/')[2]), Convert.ToInt32(StrDate.Split('/')[1]), Convert.ToInt32(StrDate.Split('/')[0])); } return dt; }
        public static DataSet SearchResourceID(String SearchKey)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[2];
                param[0] = new SqlParameter("@sName", SearchKey);
                param[1] = new SqlParameter("@fCase", 2);
                DataSet ds = SqlHelper.ExecuteDataset("tbl_MonthlyPrePaidExpensesTrans_ddl", param);
                return ds;
            }
            catch (Exception e) { throw e; }
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionResourceID(string prefixText, int count, string contextKey)
        {
            DataSet ds = SearchResourceID(prefixText);
            DataTable dt = ds.Tables[0];
            List<string> items = new List<string>(count);
            var rtval = new Dictionary<string, string>();
            foreach (DataRow row in dt.Rows)
            {
                string str = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(row["Name"].ToString(), row["ID"].ToString());
                items.Add(str);
            } return items.ToArray();
        }
        public static DataSet SearchModuleID(String SearchKey)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[2];
                param[0] = new SqlParameter("@sName", SearchKey);
                param[1] = new SqlParameter("@fCase", 3);
                DataSet ds = SqlHelper.ExecuteDataset("tbl_MonthlyPrePaidExpensesTrans_ddl", param);
                return ds;
            }
            catch (Exception e) { throw e; }
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionModuleID(string prefixText, int count, string contextKey)
        {
            DataSet ds = SearchModuleID(prefixText);
            DataTable dt = ds.Tables[0];
            List<string> items = new List<string>(count);
            var rtval = new Dictionary<string, string>();
            foreach (DataRow row in dt.Rows)
            {
                string str = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(row["Name"].ToString(), row["ID"].ToString());
                items.Add(str);
            } return items.ToArray();
        }
        public static DataSet SearchwsiD(String SearchKey)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[2];
                param[0] = new SqlParameter("@sName", SearchKey);
                param[1] = new SqlParameter("@fCase", 5);
                DataSet ds = SqlHelper.ExecuteDataset("tbl_MonthlyPrePaidExpensesTrans_ddl", param);
                return ds;
            }
            catch (Exception e) { throw e; }
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionwsiD(string prefixText, int count, string contextKey)
        {
            DataSet ds = SearchwsiD(prefixText);
            DataTable dt = ds.Tables[0];
            List<string> items = new List<string>(count);
            var rtval = new Dictionary<string, string>();
            foreach (DataRow row in dt.Rows)
            {
                string str = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(row["Name"].ToString(), row["ID"].ToString());
                items.Add(str);
            } return items.ToArray();
        }
        public static DataSet SearchprjID(String SearchKey)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[2];
                param[0] = new SqlParameter("@sName", SearchKey);
                param[1] = new SqlParameter("@fCase", 6);
                DataSet ds = SqlHelper.ExecuteDataset("tbl_MonthlyPrePaidExpensesTrans_ddl", param);
                return ds;
            }
            catch (Exception e) { throw e; }
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionprjID(string prefixText, int count, string contextKey)
        {
            DataSet ds = SearchprjID(prefixText);
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
            //SqlParameter[] parms1 = new SqlParameter[1];
            //parms1[0] = new SqlParameter("@fCase", 1);
            //FIllObject.FillDropDown(ref ddlTransID, "tbl_MonthlyPrePaidExpensesTrans_ddl", parms1);
            SqlParameter[] parms2 = new SqlParameter[1];
            parms2[0] = new SqlParameter("@fCase", 2);
            FIllObject.FillDropDown(ref ddlResourceID, "tbl_MonthlyPrePaidExpensesTrans_ddl", parms2);
            SqlParameter[] parms3 = new SqlParameter[1];
            parms3[0] = new SqlParameter("@fCase", 3);
            FIllObject.FillDropDown(ref ddlModuleID, "tbl_MonthlyPrePaidExpensesTrans_ddl", parms3);
            SqlParameter[] parms4 = new SqlParameter[1];
            parms4[0] = new SqlParameter("@fCase", 4);
            FIllObject.FillDropDown(ref ddlcb, "tbl_MonthlyPrePaidExpensesTrans_ddl", parms4);
            //SqlParameter[] parms5 = new SqlParameter[1];
            //parms5[0] = new SqlParameter("@fCase", 5);
            //FIllObject.FillDropDown(ref ddlNoOfMonths, "tbl_MonthlyPrePaidExpensesTrans_ddl", parms5);
            //SqlParameter[] parms6 = new SqlParameter[1];
            //parms6[0] = new SqlParameter("@fCase", 6);
            //FIllObject.FillDropDown(ref ddlBillTransID, "tbl_MonthlyPrePaidExpensesTrans_ddl", parms6);
            SqlParameter[] parms7 = new SqlParameter[1];
            parms7[0] = new SqlParameter("@fCase", 5);
            FIllObject.FillDropDown(ref ddlwsiD, "tbl_MonthlyPrePaidExpensesTrans_ddl", parms7);
            SqlParameter[] parms8 = new SqlParameter[1];
            parms8[0] = new SqlParameter("@fCase", 6);
            FIllObject.FillDropDown(ref ddlprjID, "tbl_MonthlyPrePaidExpensesTrans_ddl", parms8);
            SqlParameter[] parms9 = new SqlParameter[1];
            //parms9[0] = new SqlParameter("@fCase", 9);
            //FIllObject.FillDropDown(ref ddlia, "tbl_MonthlyPrePaidExpensesTrans_ddl", parms9);
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
        protected void gvDet_RowCommand(object sender, GridViewCommandEventArgs e)
        {
        }
    }
}
