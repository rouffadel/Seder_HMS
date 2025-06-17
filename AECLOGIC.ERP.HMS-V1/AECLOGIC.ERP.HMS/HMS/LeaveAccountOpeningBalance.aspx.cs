using System;
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
using System.Collections.Generic;
using AECLOGIC.ERP.COMMON;
using AECLOGIC.ERP.HMS.HRClasses;
using Aeclogic.Common.DAL;
namespace AECLOGIC.ERP.HMS
{
    public partial class LeaveAccountOpeningBalance : AECLOGIC.ERP.COMMON.WebFormMaster
    {
        bool viewall;
        string menuname;
        string menuid;
        int mid = 0;
        HRCommon objHrCommon = new HRCommon();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindEmployee();
            }
        }
        private void BindEmployee()
        {
            objHrCommon.PageSize = EmpListPaging.ShowRows;
            objHrCommon.CurrentPage = EmpListPaging.CurrentPage;
            SqlParameter[] sqlParams = new SqlParameter[5];
            sqlParams[0] = new SqlParameter("@CurrentPage", objHrCommon.CurrentPage);
            sqlParams[1] = new SqlParameter("@PageSize", objHrCommon.PageSize);
            sqlParams[2] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
            sqlParams[2].Direction = ParameterDirection.ReturnValue;
            sqlParams[3] = new SqlParameter("@NoofRecords", System.Data.SqlDbType.Int);
            sqlParams[3].Direction = ParameterDirection.Output;
            if(txtEmpNameHidden.Value!=null && txtEmpNameHidden.Value!="")
                sqlParams[4] = new SqlParameter("@Empid", txtEmpNameHidden.Value);
            else
                sqlParams[4] = new SqlParameter("@Empid", null);
            DataSet ds = SQLDBUtil.ExecuteDataset("sh_bindempleaveACCOB", sqlParams);
            objHrCommon.NoofRecords = (int)sqlParams[3].Value;
            objHrCommon.TotalPages = (int)sqlParams[2].Value;
            if (ds!=null&& ds.Tables.Count>0 &&  ds.Tables[0].Rows.Count > 0)
            {
                gdvLeaveOB.DataSource = ds.Tables[0];
            }
            else
                gdvLeaveOB.DataSource = null;
            gdvLeaveOB.DataBind();
            gdvLeaveDetails.Visible = false; btnSubmit.Visible = false;
            ViewState["OBEmpid"] = null;
            ViewState["cutoffdate"] = null;
            EmpListPaging.Bind(objHrCommon.CurrentPage, objHrCommon.TotalPages, objHrCommon.NoofRecords, objHrCommon.PageSize);
        }
        protected void gdvLeaveOB_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Calc")
                {
                    DateTime date; 
                    GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).Parent.Parent;
                    Label lblEmpID = (Label)gdvLeaveOB.Rows[row.RowIndex].FindControl("lblEmpID");
                    ViewState["OBEmpid"] = lblEmpID.Text;
                    SqlParameter[] parms1 = new SqlParameter[1];
                    parms1[0] = new SqlParameter("@Empid", lblEmpID.Text);
                    DataSet dss = SQLDBUtil.ExecuteDataset("sh_checkLVRD", parms1);
                     if (dss != null && dss.Tables.Count > 0 && dss.Tables[0].Rows.Count > 0)
                     {
                     }
                     else
                     {
                         AlertMsg.MsgBox(Page, "Enter LVRD",AlertMsg.MessageType.Warning);
                         return;
                     }
                    TextBox txtCutOffDate = (TextBox)gdvLeaveOB.Rows[row.RowIndex].FindControl("txtCutOffDate");
                    if (txtCutOffDate.Text != String.Empty)
                    {
                        if (Convert.ToDateTime(DateTime.Now) < Convert.ToDateTime(txtCutOffDate.Text))
                        {
                            AlertMsg.MsgBox(Page, "Future date not allowed!", AlertMsg.MessageType.Warning);
                            return;
                        }
                        date = CodeUtilHMS.ConvertToDate_ddMMMyyy(txtCutOffDate.Text, CodeUtilHMS.DateFormat.ddMMMyyyy);
                    }
                    else
                    {
                        AlertMsg.MsgBox(Page, "Select Cut Off Date");
                        txtCutOffDate.Focus();
                        return;
                    }
                    TextBox txtPayableDays = (TextBox)gdvLeaveOB.Rows[row.RowIndex].FindControl("txtPayableDays");
                    SqlParameter[] parms = new SqlParameter[3];
                    parms[0] = new SqlParameter("@Empid", lblEmpID.Text);
                    parms[1] = new SqlParameter("@CuttOffDate", date);
                    ViewState["cutoffdate"] = date;
                    parms[2] = new SqlParameter("@PayableDays", txtPayableDays.Text.Trim());
                    DataSet ds = SQLDBUtil.ExecuteDataset("Sh_OBLeavesAccount", parms);
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        gdvLeaveDetails.DataSource = ds.Tables[0];
                        gdvLeaveDetails.Visible = true;
                        btnSubmit.Visible = true;
                    }
                    else {
                        gdvLeaveDetails.DataSource = null;
                        btnSubmit.Visible = false;
                        ViewState["OBEmpid"] = null;
                        ViewState["cutoffdate"] = null;
                    }
                    gdvLeaveDetails.DataBind();
                }
            }
            catch { }
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
        void EmpListPaging_ShowRowsClick(object sender, EventArgs e)
        {
            EmpListPaging.CurrentPage = 1;
            BindEmployee();
        }
        void EmpListPaging_FirstClick(object sender, EventArgs e)
        {
            BindEmployee();
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionList_EmpName(string prefixText, int count, string contextKey)
        {
            DataSet ds = AttendanceDAC.GetSearchEmpName(prefixText);
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
                menuname = ds.Tables[0].Rows[0]["menuname"].ToString();
                menuid = MenuId.ToString();
                mid = Convert.ToInt32(ds.Tables[0].Rows[0]["MenuId"].ToString());
            }
            return MenuId;
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindEmployee();
        }
        protected void gdvLeaveDetails_RowCommand(object sender, GridViewCommandEventArgs e)
        {
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
            if (ViewState["OBEmpid"] != null && ViewState["OBEmpid"] != "")
            {
                int i = 0;
                foreach (GridViewRow gvr in gdvLeaveDetails.Rows)
                {
                    Label lblLTypesID = (Label)gvr.FindControl("lblLTypesID");
                    Label lblLeavesCr = (Label)gvr.FindControl("lblLeavesCr");
                    SqlParameter[] parms = new SqlParameter[5];
                    parms[0] = new SqlParameter("@LTypesID", lblLTypesID.Text);
                    parms[1] = new SqlParameter("@LeavesCr", lblLeavesCr.Text);
                    parms[2] = new SqlParameter("@empid", ViewState["OBEmpid"]);
                    parms[3] = new SqlParameter("@cutoffdate", ViewState["cutoffdate"]);
                    parms[4] = new SqlParameter("@Case", i);
                    SQLDBUtil.ExecuteNonQuery("sh_leaveaccountOBUpdate", parms);
                    i = i + 1;
                    btnSubmit.Visible = false;
                }
                AlertMsg.MsgBox(Page,"Saved Successfully");
            }
            }
            catch (Exception)
            {
              //  throw;
            }
        }
    }
}