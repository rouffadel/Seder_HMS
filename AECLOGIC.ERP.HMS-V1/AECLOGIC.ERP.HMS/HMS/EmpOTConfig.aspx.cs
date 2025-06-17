using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using AECLOGIC.HMS.BLL;
using AECLOGIC.ERP.COMMON;
using System.Collections.Generic;
using AECLOGIC.ERP.HMS.HRClasses;
namespace AECLOGIC.ERP.HMS
{
    public partial class EmpOTConfig : AECLOGIC.ERP.COMMON.WebFormMaster
    {
        #region Declaration
        int mid = 0;
        bool viewall;
        string menuname;
        static int WSID = 0;
        static int CompanyID;
        static int Site = 0;
        string menuid;
        HRCommon objHrCommon = new HRCommon();
        AttendanceDAC objatt = new AttendanceDAC();
        #endregion Declaration
        #region Paging
        protected override void OnInit(EventArgs e)
        {
            ModuleID = 1;
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
              lblStatus.Text = String.Empty;
            CompanyID = Convert.ToInt32(Session["CompanyID"]);
            if (!IsPostBack)
            {
                FillEmpNature();
                BindPager();
                try
                {
                    ViewState["WSID"] = 0;
                    if (Convert.ToInt32(Session["RoleId"].ToString()) == 7)
                    {
                        try
                        {
                            DataSet ds = clViewCPRoles.HR_DailyAttStatus( Convert.ToInt32(Session["UserId"]));
                            ViewState["WSID"] = ds.Tables[0].Rows[0]["ID"].ToString();
                            txtSearchWorksite.Text = ds.Tables[0].Rows[0]["Name"].ToString();
                            txtSearchWorksite.ReadOnly = true;
                        }
                        catch { }
                    }
                }
                catch { }
            }
          }
          catch (Exception ex)
          {
              clsErrorLog.HMSEventLog(ex, "EMPOTConfig", "Page_Load", "001");
          }
        }
        #endregion PageLoad
        #region Fill DropDows And CheckBoxS
        public void FillEmpNature()
        {
            DataSet ds = Leaves.GetEmpNatureList(1);
            ddlEmpNature.DataSource = ds;
            ddlEmpNature.DataTextField = "Nature";
            ddlEmpNature.DataValueField = "NatureOfEmp";
            ddlEmpNature.DataBind();
            ddlEmpNature.Items.Insert(0, new ListItem("---All---", "0"));
        }
        #endregion Fill DropDows And CheckBox
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
                    if (Convert.ToInt32(ddlWorkSite_hid.Value == "" ? "0" : ddlWorkSite_hid.Value) != 0)
                        WS = Convert.ToInt32(Convert.ToInt32(ddlWorkSite_hid.Value == "" ? "0" : ddlWorkSite_hid.Value));
                }
                int? Dept = null;
                if (txtdepartment.Text.Trim() != "")
                {
                if (Convert.ToInt32(ddlDept_hid.Value == "" ? "0" : ddlDept_hid.Value) != 0)
                    Dept = Convert.ToInt32(Convert.ToInt32(ddlDept_hid.Value == "" ? "0" : ddlDept_hid.Value));}
                int? EmpNatureID = null;
                if (ddlEmpNature.SelectedValue != "0")
                    EmpNatureID = Convert.ToInt32(ddlEmpNature.SelectedValue);
                int? EmpID = null;
                int? DesigID = null;
                if (textDesg.Text.Trim() != "")
                {
                    if (Convert.ToInt32(ddlDesif2_hid.Value == "" ? "0" : ddlDesif2_hid.Value) != 0)
                        DesigID = Convert.ToInt32(Convert.ToInt32(ddlDesif2_hid.Value == "" ? "0" : ddlDesif2_hid.Value));
                }
                try
                {
                    if (Convert.ToInt32(ViewState["WSID"]) > 0)
                        objHrCommon.SiteID = Convert.ToInt32(ViewState["WSID"]);
                }
                catch { }
                try {
                    if(txtEMPID.Text.Trim() !="")
                        EmpID = Convert.ToInt32(txtEMPID.Text);
                }
                catch { }
              DataSet  ds = AttendanceDAC.GetEmployessForOT(objHrCommon, WS, Dept, EmpNatureID, EmpID, Convert.ToInt32(Session["CompanyID"]), DesigID);
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
                clsErrorLog.HMSEventLog(ex, "EMPOTConfig", "BindEMPDetails", "002");
            }
        }
        #endregion SuportingMethods
        #region Events
        protected void btnSearch_Click(object sender, EventArgs e)
        {
          try{
              EmpMessAttPaging.CurrentPage = 1;
              BindPager();
          }
          catch (Exception ex)
          {
              clsErrorLog.HMSEventLog(ex, "EMPOTConfig", "btnSearch", "003");
          }
        }
        protected void btnAll_Click(object sender, EventArgs e)
        {
            try
            {
                int count = 0;
                foreach (GridViewRow gvr in grdEmpMessAtt.Rows)
                {
                    CheckBox chk = (CheckBox)gvr.FindControl("chkAll");             //Gridview checkbox list
                    Label lblEmpId = (Label)gvr.Cells[0].FindControl("lblEmpID");
                    int EmpID = int.Parse(lblEmpId.Text);
                    int UserID =  Convert.ToInt32(Session["UserId"]);
                    TextBox txtOTHrs = (TextBox)gvr.FindControl("txtOTHrs");
                    if (chk.Checked)
                    {
                        AttendanceDAC.InsUpdEmpOTConfig(EmpID, 1, UserID,Convert.ToDecimal(txtOTHrs.Text));
                        count++;
                    }
                    else
                    {
                        AttendanceDAC.InsUpdEmpOTConfig(EmpID, 0, UserID, Convert.ToDecimal(txtOTHrs.Text));
                        count++;
                    }
                }
                if(count>0)
                {
                    lblStatus.Text = "Saved!";
                    lblStatus.ForeColor = System.Drawing.Color.Green;
                }
            }
            catch (Exception ex)
            {
              //  AlertMsg.MsgBox(Page, ex.Message.ToString(),AlertMsg.MessageType.Error);
                lblStatus.Text = ex.Message;
                lblStatus.ForeColor = System.Drawing.Color.Red;
            }
            BindPager();
        }
        #endregion Events
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionList(string prefixText, int count, string contextKey)
        {
            DataSet ds = AttendanceDAC.HR_GetWorkSite_By_googlesearch_EmpList(prefixText.Trim(), WSID);
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
            DataSet ds = AttendanceDAC.GetGoogleSerachDesignations(prefixText.Trim());
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