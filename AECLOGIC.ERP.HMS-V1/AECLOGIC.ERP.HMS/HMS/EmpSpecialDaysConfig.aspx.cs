using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using AECLOGIC.HMS.BLL;
using AECLOGIC.ERP.COMMON;
namespace AECLOGIC.ERP.HMS
{
    public partial class EmpRamzanConfig : AECLOGIC.ERP.COMMON.WebFormMaster
    {
        #region Declaration
        int mid = 0;
        bool viewall;
        string menuname;
        static int WSID = 0;
        static int Site = 0;
        static int CompanyID;
        string menuid;
        HRCommon objHrCommon = new HRCommon();
        AttendanceDAC objatt = new AttendanceDAC();
        static int ModID;
        static int Userid;
        #endregion Declaration
        #region Paging
        protected override void OnInit(EventArgs e)
        {
            ModuleID = 1;
            ModID = ModuleID;
            base.OnInit(e);
            EmpMessAttPaging.FirstClick += new Paging.PageFirst(EmpMessAttPaging_FirstClick);
            EmpMessAttPaging.PreviousClick += new Paging.PagePrevious(EmpMessAttPaging_FirstClick);
            EmpMessAttPaging.NextClick += new Paging.PageNext(EmpMessAttPaging_FirstClick);
            EmpMessAttPaging.LastClick += new Paging.PageLast(EmpMessAttPaging_FirstClick);
            EmpMessAttPaging.ChangeClick += new Paging.PageChange(EmpMessAttPaging_FirstClick);
            EmpMessAttPaging.ShowRowsClick += new Paging.ShowRowsChange(EmpMessAttPaging_ShowRowsClick);
            EmpMessAttPaging.CurrentPage = 1;
        }
        void EmpMessAttPaging_ShowRowsClick(object sender, EventArgs e)
        {
            EmpMessAttPaging.CurrentPage = 1;
            BindPager();
        }
        void EmpMessAttPaging_FirstClick(object sender, EventArgs e)
        {
            BindPager();
        }
        void BindPager()
        {
            objHrCommon.PageSize = EmpMessAttPaging.CurrentPage;
            objHrCommon.CurrentPage = EmpMessAttPaging.ShowRows;
            BindEmpDetails(objHrCommon);
        }
        #endregion Paging
        #region PageLoad
        protected void Page_Load(object sender, EventArgs e)
        {
          try{ 
            CompanyID = Convert.ToInt32(Session["CompanyID"]);
            Userid = Convert.ToInt32( Convert.ToInt32(Session["UserId"]).ToString());
            if (!IsPostBack)
            {
                txtEmpID.Attributes.Add("onkeydown", "return controlEnter(event)");
                MainView.ActiveViewIndex = 0;
                ViewState["RID"] = "";
                // FillWS();
                FillEmpNature();
                BindDll();
                // BindDesignations();
                BindPager();
            }
          }
          catch (Exception ex)
          {
              clsErrorLog.HMSEventLog(ex, "EmpSpecialDaysConfig", "Page_Load", "001");
          }
        }
        #endregion PageLoad
                public void FillEmpNature()
        {
          try{    
           DataSet ds = Leaves.GetEmpNatureList(1);
            ddlEmpNature.DataSource = ds;
            ddlEmpNature.DataTextField = "Nature";
            ddlEmpNature.DataValueField = "NatureOfEmp";
            ddlEmpNature.DataBind();
            ddlEmpNature.Items.Insert(0, new ListItem("---All---", "0"));
          }
          catch (Exception ex)
          {
              clsErrorLog.HMSEventLog(ex, "EmpSpecialDaysConfig", "Page_Load", "002");
          }
        }
        #region SuportingMethods
        public void BindEmpDetails(HRCommon objHrCommon)
        {
            try
            {
                objHrCommon.PageSize = EmpMessAttPaging.ShowRows;
                objHrCommon.CurrentPage = EmpMessAttPaging.CurrentPage;
                int? WS = null;
                if (txtSearchWorksite.Text.Trim() != "")
                {
                    WS = Convert.ToInt32(ddlWorkSite_hid.Value == "" ? "0" : ddlWorkSite_hid.Value);
                }
                int? Dept = null;
                if (txtdepartment.Text.Trim() != "")
                {
                    Dept = Convert.ToInt32(ddlDept_hid.Value == "" ? "0" : ddlDept_hid.Value);
                }
                int? EmpNatureID = null;
                if (ddlEmpNature.SelectedValue != "0")
                    EmpNatureID = Convert.ToInt32(ddlEmpNature.SelectedValue);
                int? EmpID = null;
                if (txtEmpID.Text != "")
                 EmpID = Convert.ToInt32(txtEmpID.Text); 
                int? DesigID = null;
                if (TxtDsg.Text.Trim() != "")
                {
                    DesigID = Convert.ToInt32(ddlDesif2_hid.Value == "" ? "0" : ddlDesif2_hid.Value);
                }
              DataSet  ds = AttendanceDAC.GetEmployessForRamzan(objHrCommon, WS, Dept, EmpNatureID, EmpID, Convert.ToInt32(Session["CompanyID"]), DesigID, Convert.ToInt32(ddlSpecialDays.SelectedValue));
                if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0)
                {
                    grdEmpMessAtt.DataSource = ds;
                    EmpMessAttPaging.Bind(objHrCommon.CurrentPage, objHrCommon.TotalPages, objHrCommon.NoofRecords, objHrCommon.PageSize);
                }
                else
                {
                    grdEmpMessAtt.EmptyDataText = "No Records Found";
                    EmpMessAttPaging.Visible = false;
                }
                grdEmpMessAtt.DataBind();
            }
            catch (Exception ex)
            {
                clsErrorLog.HMSEventLog(ex, "EmpSpecialDaysConfig", "BindEmpDetails", "002");
            }
        }
        #endregion SuportingMethods
        #region Events
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            int EmpID = 0;
            if (txtEmpID.Text != "")
            EmpID = Convert.ToInt32(txtEmpID.Text);
            EmpMessAttPaging.CurrentPage = 1;
          try{  BindPager();
          }
          catch (Exception ex)
          {
              clsErrorLog.HMSEventLog(ex, "EmpSpecialDaysConfig", "btnSearch_Click", "003");
          }
        }
        protected void btnAll_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (GridViewRow gvr in grdEmpMessAtt.Rows)
                {
                    CheckBox chk = (CheckBox)gvr.FindControl("chkAll");             //Gridview checkbox list
                    Label lblEmpId = (Label)gvr.Cells[0].FindControl("lblEmpID");
                    int EmpID = int.Parse(lblEmpId.Text);
                    int UserID =  Convert.ToInt32(Session["UserId"]);
                    if (chk.Checked)
                    {
                        AttendanceDAC.InsUpdEmpRamzanConfig(EmpID, 1, UserID, Convert.ToInt32(ddlSpecialDays.SelectedValue));
                    }
                    else
                    {
                        AttendanceDAC.InsUpdEmpRamzanConfig(EmpID, 0, UserID, Convert.ToInt32(ddlSpecialDays.SelectedValue));
                    }
                }
                AlertMsg.MsgBox(Page, "Done");
            }
            catch (Exception ex)
            {
                AlertMsg.MsgBox(Page, ex.Message.ToString(),AlertMsg.MessageType.Error);
            }
            BindPager();
        }
        protected void gvWages_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int ID = Convert.ToInt32(e.CommandArgument);
            ViewState["Id"] = ID;
            if (e.CommandName == "Edt")
            {
                BindDetails(ID);
            }
        }
        public void Clear()
        {
            txtSpDayName.Text = string.Empty;
            ViewState["Id"] = "";
            txtWHS.Text = string.Empty;
            txtFromDate.Text = string.Empty;
            txtToDate.Text = string.Empty;
        }
        public void BindGrid()
        {
            DataSet dslst = AttendanceDAC.GetRamzanDates(null);
            gvramzan.DataSource = dslst;
            gvramzan.DataBind();
            BindDll();
        }
        void BindDll()
        {
            try
            {
                DataSet dslst = AttendanceDAC.HR_GetRamzanDates_ddl(null);
                ddlSpecialDays.DataSource = dslst;
                ddlSpecialDays.DataTextField = "SpDayName";
                ddlSpecialDays.DataValueField = "RID";
                ddlSpecialDays.DataBind();
                ddlSpecialDays.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                clsErrorLog.HMSEventLog(ex, "EmpSpecialDaysConfig", "BindDll", "004");
            }
        }
        public void BindDetails(int ID)
        {
            DataSet dsdets = new DataSet();
            dsdets = AttendanceDAC.GetRamzanDates(ID);
            txtFromDate.Text = dsdets.Tables[0].Rows[0]["FromDate"].ToString();
            txtToDate.Text = dsdets.Tables[0].Rows[0]["ToDate"].ToString();
            txtWHS.Text = dsdets.Tables[0].Rows[0]["WorkingHrs"].ToString();
            txtSpDayName.Text = dsdets.Tables[0].Rows[0]["SpDayName"].ToString();
            ViewState["RID"] = dsdets.Tables[0].Rows[0]["RID"].ToString();
        }
        #endregion Events
        protected void lnkEmp_Click(object sender, EventArgs e)
        {
            MainView.ActiveViewIndex = 0;
        }
        protected void lnkDates_Click(object sender, EventArgs e)
        {
            MainView.ActiveViewIndex = 1;
            BindGrid();
        }
        protected void btnSubmit_Click1(object sender, EventArgs e)
        {
           try{ if (CODEUtility.ConvertToDate(txtFromDate.Text.Trim(), DateFormat.DayMonthYear) >= DateTime.Now.Date && CODEUtility.ConvertToDate(txtToDate.Text.Trim(), DateFormat.DayMonthYear) >= DateTime.Now.Date)
            {
                if (CODEUtility.ConvertToDate(txtFromDate.Text.Trim(), DateFormat.DayMonthYear) < CODEUtility.ConvertToDate(txtToDate.Text.Trim(), DateFormat.DayMonthYear))
                {
                    int RID = 0;
                    if (ViewState["RID"].ToString() != null && ViewState["RID"].ToString() != string.Empty)
                    {
                        RID = Convert.ToInt32(ViewState["RID"].ToString());
                    }
                    int returnval;
                    returnval = Convert.ToInt32(AttendanceDAC.InsUpdateRamzan(RID,
                                                                              Convert.ToDecimal(txtWHS.Text),
                                                                              CODEUtility.ConvertToDate(txtFromDate.Text.Trim(), DateFormat.DayMonthYear),
                                                                              CODEUtility.ConvertToDate(txtToDate.Text.Trim(), DateFormat.DayMonthYear),
                                                                               Convert.ToInt32(Session["UserId"]), txtSpDayName.Text));
                    BindGrid();
                    Clear();
                    if (returnval == 1)
                    {
                        AlertMsg.MsgBox(Page, "Done ");
                    }
                    else
                    {
                        AlertMsg.MsgBox(Page, "Check Dates");
                    }
                    ViewState["RID"] = "";
                }
                else
                {
                    AlertMsg.MsgBox(Page, "Check Dates");
                }
            }
            else
            {
                AlertMsg.MsgBox(Page, "Old dates Can not be selected");
                Clear();
            }
           }
           catch (Exception ex)
           {
               clsErrorLog.HMSEventLog(ex, "EmpSpecialDaysConfig", "btnSubmit_Click1", "005");
           }
        }
        //added by nadeem for google search.date=06/04/2016...
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionList(string prefixText, int count, string contextKey)
        {
            DataSet ds = AttendanceDAC.GetWorkSites(prefixText, CompanyID, Userid, ModID);
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
            WSID = Convert.ToInt32(ddlWorkSite_hid.Value == "" ? "0" : ddlWorkSite_hid.Value); ;
            //  WSId = Convert.ToInt32(ddlWs_hid.Value == "" ? "0" : ddlWs_hid.Value); ;
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
            return items.ToArray(); ;// txtItems.ToArray();
        }
        protected void GetDept(object sender, EventArgs e)
        {
            Site = 0;
            Site = Convert.ToInt32(ddlDept_hid.Value == "" ? "0" : ddlDept_hid.Value); ;
            CompanyID = Convert.ToInt32(Session["CompanyID"]);
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionList_Desigination(string prefixText, int count, string contextKey)
        {
            DataSet ds = AttendanceDAC.GetSearchdesigination_googlesearch(prefixText.Trim());
            DataTable dt = ds.Tables[0];
            List<string> items = new List<string>(count);
            var rtval = new Dictionary<string, string>();
            foreach (DataRow row in dt.Rows)
            {
                string str = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(row["NAME"].ToString(), row["ID"].ToString());
                items.Add(str);
            }
            return items.ToArray();
        }
    }
}