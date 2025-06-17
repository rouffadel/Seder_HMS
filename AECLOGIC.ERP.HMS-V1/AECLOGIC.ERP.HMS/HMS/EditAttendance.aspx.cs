using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using AECLOGIC.HMS.BLL;
using System.Configuration;
using System.Globalization;
using AECLOGIC.ERP.COMMON;
using Aeclogic.Common.DAL;
//using AECLOGIC.ERP.HMS;
using AECLOGIC.ERP.HMS.HRClasses;
using System.Drawing;

namespace AECLOGIC.ERP.HMSV1
{
    public partial class EditAttendanceV1 : AECLOGIC.ERP.COMMON.WebFormMaster
    {
        AttendanceDAC objAtt = new AttendanceDAC();
        int mid = 0;
        string menuname;
        string menuid;
        string Name;
        static int CompanyID = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["CompanyID"].ToString());
        static int Role = 0;
        static int EmpID = 0;
        static int WSID;
        static int DeptId = 0;
        static int SiteSearch = 0;
        HRCommon objHrCommon = new HRCommon();
        static int userid;
        static int Modid;
        DataSet dsEPMData = new DataSet();
        DataSet dsCurrentDetails = new DataSet();
        protected override void OnInit(EventArgs e)
        {
            ModuleID = 1;
            base.OnInit(e);
            EditAttpaging.FirstClick += new Paging.PageFirst(AdvancedLeaveAppPaging_FirstClick); 
            EditAttpaging.PreviousClick += new Paging.PagePrevious(AdvancedLeaveAppPaging_FirstClick);
            EditAttpaging.NextClick += new Paging.PageNext(AdvancedLeaveAppPaging_FirstClick);
            EditAttpaging.LastClick += new Paging.PageLast(AdvancedLeaveAppPaging_FirstClick);
            EditAttpaging.ChangeClick += new Paging.PageChange(AdvancedLeaveAppPaging_FirstClick);
            EditAttpaging.ShowRowsClick += new Paging.ShowRowsChange(AdvancedLeaveAppPaging_ShowRowsClick);
            EditAttpaging.CurrentPage = 1;
            Modid = ModuleID;
        }
        void AdvancedLeaveAppPaging_ShowRowsClick(object sender, EventArgs e)
        {
            EditAttpaging.CurrentPage = 1;
            BindPager();
        }
        void AdvancedLeaveAppPaging_FirstClick(object sender, EventArgs e)
        {
            BindPager();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                lblStatus.Text = String.Empty;
                Ajax.Utility.RegisterTypeForAjax(typeof(AjaxDAL));
                if ( Convert.ToInt32(Session["UserId"]) == null)
                {
                    Response.Redirect("Home.aspx");
                }
                else
                {
                    //lblHead.Text = "Time & Attendance >> Employees";
                    //hdn.Value = "0";
                    userid =  Convert.ToInt32(Session["UserId"]);
                    if (!IsPostBack)
                    {
                        GetParentMenuId();
                        FillAttandanceType();
                        gdvMonthReport.Visible = false;
                        gdvMonthReportPrv.Visible = false;
                        lblPaidDays.Visible = false;
                        lblPaidDaysC.Visible = false;
                        try
                        {
                            txtDayCalederExtender.Format = "dd MMM yyyy";// ConfigurationManager.AppSettings["DateFormat"].ToString();
                            txtDay.Text = DateTime.Now.ToString("dd MMM yyyy");
                            txtFromDateCalendarExtender.Format = "dd MMM yyyy";
                            txtToDateCalendarExtender.Format = "dd MMM yyyy";
                        }
                        catch
                        {
                        }
                        EditAttpaging.Visible = false;
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
            }
            catch (Exception ex)
            {
                clsErrorLog.HMSEventLog(ex, "EditAttendance", "Page_Load", "001");
            }
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
                // gdvAttend.Columns[6].Visible = Convert.ToBoolean(ds.Tables[0].Rows[0]["Editable"].ToString());
                // gdvAttend.Columns[7].Visible = Convert.ToBoolean(ds.Tables[0].Rows[0]["Editable"].ToString());
                // ((LinkButton)gdvAttend.FindControl("btnUpdate")).Enabled = false;
                ViewState["Editable"] = (bool)ds.Tables[0].Rows[0]["Editable"];
                //ViewState["ViewAll"] = (bool)ds.Tables[0].Rows[0]["ViewAll"];
                //viewall = (bool)ViewState["ViewAll"];
                //menuname = ds.Tables[0].Rows[0]["menuname"].ToString();
                //menuid = MenuId.ToString();
                //mid = Convert.ToInt32(ds.Tables[0].Rows[0]["MenuId"].ToString());
            }
            return MenuId;
        }
        private void FillAttandanceType()
        {
            DataSet ds = AttendanceDAC.GetAttendanceType();
            ddlAttType.DataSource = ds;
            ddlAttType.DataValueField = "ID";
            ddlAttType.DataTextField = "ShortName";
            ddlAttType.DataBind();
            ddlAttType.Items.Insert(0, new ListItem("--AttType--", "0"));
        }
        private DataSet BindGrid(AttendanceDAC objAtt, DataSet dstemp)
        {
            DateTime Date = CodeUtilHMS.ConvertToDate_ddMMMyyy(txtDay.Text.Trim(), CodeUtilHMS.DateFormat.ddMMMyyyy);
            Name = txtName.Text;
            dstemp = objAtt.GetTodayAttendanceforEditing(0, 0, Date, Name, Convert.ToInt32(Session["CompanyID"]));
            gdvAttend.DataSource = dstemp.Tables[0];
            gdvAttend.DataBind();
            return dstemp;
        }
        public DataSet BindAttendanceType()
        {
            DataSet ds = AttendanceDAC.GetAttendanceType();
            return ds;
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionList(string prefixText, int count, string contextKey)
        {
            WSID = 0;
            DataSet ds = AttendanceDAC.GetWorkSites(prefixText, CompanyID, userid, Modid);
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
          //  CompanyID = Convert.ToInt32(Session["CompanyID"]);
            WSID = Convert.ToInt32(ddlWorksite_hid.Value == "" ? "0" : ddlWorksite_hid.Value); ;
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionList_Department(string prefixText, int count, string contextKey)
        {
            DataSet ds = AttendanceDAC.Department_googlesearch_by_siteid(prefixText, WSID, CompanyID);
            DataTable dt = ds.Tables[0];
            List<string> items = new List<string>(count);
            var rtval = new Dictionary<string, string>();
            foreach (DataRow row in dt.Rows)
            {
                string str = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(row["DeptNameName"].ToString(), row["ID"].ToString());
                items.Add(str);
            }
            return items.ToArray(); ;// txtItems.ToArray();
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetEmpidList(string prefixText)
        {
            return ConvertStingArray(AttendanceDAC.GetEmpid_By_Search(prefixText));
        }
        protected void GetDept(object sender, EventArgs e)
        {
            SiteSearch = 0;
            DeptId = Convert.ToInt32(ddlDepartment_hid.Value == "" ? "0" : ddlDepartment_hid.Value); ;
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionList_empname(string prefixText, int count, string contextKey)
        {
            DataSet ds = AttendanceDAC.GetSearchName_APPDetails(prefixText);
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

            return items.ToArray(); ;// txtItems.ToArray();

        }
        public void chkOut_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chk = (CheckBox)sender;
            GridViewRow gvr = (GridViewRow)(((Control)sender).NamingContainer);
            TextBox txt = (TextBox)gvr.Cells[5].FindControl("txtOUT");
            if (chk.Checked == true)
            {
                txt.Text = DateTime.Now.ToShortTimeString();
            }
            else
            {
                txt.Text = "";
            }
        }
        public void chkOut1_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chk = (CheckBox)sender;
            GridViewRow gvr = (GridViewRow)(((Control)sender).NamingContainer);
            TextBox txt = (TextBox)gvr.Cells[5].FindControl("txtOUT1");
            if (chk.Checked == true)
            {
                txt.Text = DateTime.Now.ToShortTimeString();
            }
            else
            {
                txt.Text = "";
            }
        }
        protected void ddl_SelectedIndexChanged(object sender, EventArgs e)
        {
            // txtDay.Text = txtDay.Text.Replace('-','/');
            DateTime Date = CodeUtilHMS.ConvertToDate_ddMMMyyy(txtDay.Text.Trim(), CodeUtilHMS.DateFormat.ddMMMyyyy);
            Name = txtName.Text;
          DataSet  dstemp = objAtt.GetTodayAttendanceforEditing(Convert.ToInt32(Convert.ToInt32(ddlDepartment_hid.Value == "" ? "0" : ddlDepartment_hid.Value)), Convert.ToInt32(WSID), Date, Name, Convert.ToInt32(Session["CompanyID"]));
            gdvAttend.DataSource = dstemp.Tables[0];
            gdvAttend.DataBind();
        }
        protected void gdvAttend_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DropDownList ddlStatus = (DropDownList)e.Row.FindControl("ddlStatus");
                    Button btnHdn = (Button)e.Row.FindControl("btnHid");
                    CheckBox chkOut = (CheckBox)e.Row.FindControl("chkOut");
                    TextBox txtOut = (TextBox)e.Row.FindControl("txtOUT");
                    TextBox txt = (TextBox)e.Row.FindControl("txtIN");
                    txt.Enabled = Convert.ToBoolean(ViewState["Editable"].ToString());
                    LinkButton lnkUpd = (LinkButton)e.Row.FindControl("btnUpdate");
                    lnkUpd.Enabled = Convert.ToBoolean(ViewState["Editable"].ToString());
                    TextBox txtRemarks = (TextBox)e.Row.FindControl("txtRemarks");
                    Label lblEmp = (Label)e.Row.FindControl("lblEmpID");
                    // int Row = Convert.ToInt32(e.Row.RowIndex);
                    if (btnHdn.CommandArgument != "")
                        ddlStatus.SelectedValue = btnHdn.CommandArgument;
                    if (txtOut.Text != "")
                    {
                        chkOut.Checked = true;
                        chkOut.Enabled = false;
                    }
                    if (txt.Text == "")
                    {
                        chkOut.Enabled = true;
                        chkOut.Checked = false;
                        txtOut.Text = "";
                    }
                    int EmpId = Convert.ToInt32(lblEmp.Text);
                    if (Convert.ToInt32(ddlStatus.SelectedValue) == 2)
                    {
                        txt.Enabled = true;
                        txtOut.Enabled = true;
                    }
                    else
                    {
                        txt.Enabled = false;
                        txtOut.Enabled = false;
                    }
                    chkOut.Attributes.Add("onclick", "javascript:return GetOutTime('" + chkOut.ClientID + "','" + DateTime.Now.ToShortTimeString() + "','" + txtOut.ClientID + "','" + txt.ClientID + "');");
                    ddlStatus.Attributes.Add("onchange", "javascript:return CheckLeaveCombination(this,'" + EmpId + "','" + txt.ClientID + "','" + txtOut.ClientID + "','" + chkOut.ClientID + "','" + e.Row.ClientID + "')");
                }
            }
            catch (Exception ex)
            {
                clsErrorLog.HMSEventLog(ex, "EditAttendance", "gdvAttend_RowDataBound", "002");
            }
        }
        protected void gdvAttend_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                AttendanceDAC objAtt = new AttendanceDAC();
                int rowIndex = Convert.ToInt32(e.CommandArgument.ToString());
                GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).Parent.Parent;
                if (e.CommandName == "upd")
                {
                    Label lblEmp = (Label)gdvAttend.Rows[row.RowIndex].Cells[1].FindControl("lblEmpID");
                    // int Id = Convert.ToInt32(gdvAttend.DataKeys[rowIndex].Value);
                    DropDownList ddlStatus = (DropDownList)gdvAttend.Rows[row.RowIndex].Cells[3].Controls[1];
                    CheckBox chkOut = (CheckBox)gdvAttend.Rows[row.RowIndex].Cells[5].FindControl("chkOut");
                    TextBox txtOut = (TextBox)gdvAttend.Rows[row.RowIndex].Cells[6].FindControl("txtOUT");
                    TextBox txt = (TextBox)gdvAttend.Rows[row.RowIndex].Cells[4].FindControl("txtIN");
                    if (txt.Text != null && txt.Text != "" && txtOut.Text != null && txtOut.Text != "")
                    {
                        if (Convert.ToDateTime(txt.Text) <= Convert.ToDateTime(txtOut.Text))
                        {
                            txtOut.Text = txtOut.Text;
                        }
                        else
                        {
                            AlertMsg.MsgBox(Page, "OUT TIME should be greater than IN TIME", AlertMsg.MessageType.Warning);
                            //lblStatus.Text = "OUT TIME should be greater than IN TIME";
                            //lblStatus.ForeColor = System.Drawing.Color.Red;
                            txtOut.Text = string.Empty;
                            txtOut.Focus();
                            return;
                        }
                    }
                    TextBox txtRemarks = (TextBox)gdvAttend.Rows[row.RowIndex].Cells[7].FindControl("txtRemarks");
                    // int SiteID = Convert.ToInt32(ddlWorksite.SelectedValue);
                    int SiteID = Convert.ToInt32(WSID);
                    int UserID = Convert.ToInt32(Session["UserId"]);
                    int EmpID = Convert.ToInt32(lblEmp.Text);
                    if (Convert.ToInt32(ddlStatus.SelectedValue) == 2)
                    {
                        if (txt.Text == "" || txt.Text == null)
                        {
                            AlertMsg.MsgBox(Page, "Enter INTime", AlertMsg.MessageType.Warning);
                            //lblStatus.Text = "Enter INTime";
                            //lblStatus.ForeColor = System.Drawing.Color.Red;
                            return;
                        }
                        if (txtOut.Text == "" || txtOut.Text == null)
                        {
                            AlertMsg.MsgBox(Page, "Enter OUTTime", AlertMsg.MessageType.Warning);
                            //lblStatus.Text = "Enter OUTTime";
                            //lblStatus.ForeColor = System.Drawing.Color.Red;
                            return;
                        }
                    }
                    
                   //int i = Convert.ToInt32(objAtt.UpdateFullAtt(Convert.ToInt32(Id), Convert.ToInt32(ddlStatus.SelectedValue),Convert.ToDateTime(txtDay.Text), txt.Text, txtOut.Text, txtRemarks.Text));
                     int   i = Convert.ToInt32(objAtt.UpdateFullAtt(Convert.ToInt32(EmpID), Convert.ToInt32(ddlStatus.SelectedValue), CodeUtilHMS.ConvertToDate_ddMMMyyy(txtDay.Text.Trim(), CodeUtilHMS.DateFormat.ddMMMyyyy), txt.Text, txtOut.Text, txtRemarks.Text, SiteID, UserID));
                    if (i == 1)
                    {
                        hdn.Value = "1";
                        AlertMsg.MsgBox(Page, "Done!", AlertMsg.MessageType.Success);
                        //lblStatus.Text = "Done!";
                        //lblStatus.ForeColor = System.Drawing.Color.Green;
                    }
                    if (i == 2)
                    {
                        hdn.Value = "1";
                        AlertMsg.MsgBox(Page, "Updated!", AlertMsg.MessageType.Success);
                        //lblStatus.Text = "Updated!";
                        //lblStatus.ForeColor = System.Drawing.Color.Green;
                    }
                }
            }
            catch (Exception ex)
            {
                clsErrorLog.HMSEventLog(ex, "EditAttendance", "gdvAttend_RowDataBound", "002");
                AlertMsg.MsgBox(Page, "Attendance updation is not allowed while the Salaries are already calculated and posted to Account!", AlertMsg.MessageType.Warning);
            }
        }
        protected void gvEmpMultipleDates_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DropDownList ddlStatus = (DropDownList)e.Row.FindControl("ddlStatus1");
                    Button btnHdn = (Button)e.Row.FindControl("btnHid1");
                    CheckBox chkOut = (CheckBox)e.Row.FindControl("chkOut1");
                    TextBox txtOut = (TextBox)e.Row.FindControl("txtOUT1");
                    TextBox txt = (TextBox)e.Row.FindControl("txtIN1");
                    txt.Enabled = Convert.ToBoolean(ViewState["Editable"].ToString());
                    LinkButton lnkUpd = (LinkButton)e.Row.FindControl("btnUpdate1");
                    lnkUpd.Enabled = Convert.ToBoolean(ViewState["Editable"].ToString());
                    TextBox txtRemarks = (TextBox)e.Row.FindControl("txtRemarks1");
                    Label lblEmp = (Label)e.Row.FindControl("lblEmpID1");
                    // int Row = Convert.ToInt32(e.Row.RowIndex);
                    if (btnHdn.CommandArgument != "")
                        ddlStatus.SelectedValue = btnHdn.CommandArgument;
                    if (txtOut.Text != "")
                    {
                        chkOut.Checked = true;
                        //chkOut.Enabled = false;
                    }
                    if (txt.Text == "")
                    {
                        chkOut.Enabled = true;
                        chkOut.Checked = false;
                        txtOut.Text = "";
                    }
                    int EmpId = Convert.ToInt32(lblEmp.Text);
                    if (Convert.ToInt32(ddlStatus.SelectedValue) == 2)
                    {
                        txt.Enabled = true;
                        txtOut.Enabled = true;
                    }
                    else
                    {
                        txt.Enabled = false;
                        txtOut.Enabled = false;
                    }
                    //chkOut.Attributes.Add("onclick", "javascript:return GetOutTime('" + chkOut.ClientID + "','" + DateTime.Now.ToShortTimeString() + "','" + txtOut.ClientID + "','" + txt.ClientID + "');");
                    //ddlStatus.Attributes.Add("onchange", "javascript:return CheckLeaveCombination(this,'" + EmpId + "','" + txt.ClientID + "','" + txtOut.ClientID + "','" + chkOut.ClientID + "','" + e.Row.ClientID + "')");
                }
            }
            catch (Exception ex)
            {
                clsErrorLog.HMSEventLog(ex, "EditAttendance", "gvEmpMultipleDates_RowDataBound", "002");
            }
        }
        protected void gvEmpMultipleDates_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                AttendanceDAC objAtt = new AttendanceDAC();
                int rowIndex = Convert.ToInt32(e.CommandArgument.ToString());
                GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).Parent.Parent;
                if (e.CommandName == "upd")
                {
                    if (txtFromDate.Text == "")
                    {
                        AlertMsg.MsgBox(Page, "Please select From Date", AlertMsg.MessageType.Warning);
                        return;
                    }
                    else if (txtToDate.Text == "")
                    {
                        AlertMsg.MsgBox(Page, "Please select To Date", AlertMsg.MessageType.Warning);
                        return;
                    }
                    
                    //Label lblEmp = (Label)gvEmpMultipleDates.Rows[row.RowIndex].Cells[1].FindControl("lblEmpID1");
                    DropDownList ddlStatus = (DropDownList)gvEmpMultipleDates.Rows[row.RowIndex].Cells[3].Controls[1];
                    CheckBox chkOut = (CheckBox)gvEmpMultipleDates.Rows[row.RowIndex].Cells[5].FindControl("chkOut1");
                    TextBox txtOut = (TextBox)gvEmpMultipleDates.Rows[row.RowIndex].Cells[6].FindControl("txtOUT1");
                    TextBox txt = (TextBox)gvEmpMultipleDates.Rows[row.RowIndex].Cells[4].FindControl("txtIN1");
                    /*if (txt.Text != null && txt.Text != "" && txtOut.Text != null && txtOut.Text != "")
                    {
                        if (Convert.ToDateTime(txt.Text) <= Convert.ToDateTime(txtOut.Text))
                        {
                            txtOut.Text = txtOut.Text;
                        }
                        else
                        {
                            AlertMsg.MsgBox(Page, "OUT TIME should be greater than IN TIME", AlertMsg.MessageType.Warning);
                            txtOut.Text = string.Empty;
                            txtOut.Focus();
                            return;
                        }
                    }*/
                    TextBox txtRemarks = (TextBox)gvEmpMultipleDates.Rows[row.RowIndex].Cells[7].FindControl("txtRemarks1");
                    //int SiteID = Convert.ToInt32(WSID);
                    int UserID = Convert.ToInt32(Session["UserId"]);
                    //int EmpID = Convert.ToInt32(lblEmp.Text);
                    int EmpID = Convert.ToInt32(txtsingleEmpNameHidden.Value);
                    /*if (Convert.ToInt32(ddlStatus.SelectedValue) == 2)
                    {
                        if (txt.Text == "" || txt.Text == null)
                        {
                            AlertMsg.MsgBox(Page, "Enter INTime", AlertMsg.MessageType.Warning);
                            return;
                        }
                        if (txtOut.Text == "" || txtOut.Text == null)
                        {
                            AlertMsg.MsgBox(Page, "Enter OUTTime", AlertMsg.MessageType.Warning);
                            return;
                        }
                    }*/

                    int i = 0;
                    if (txtsingleEmpNameHidden.Value.ToString() != "")
                    {
                        if (DateValidations().Equals(1))
                            return;
                        // single empid against multiple date entries
                        else if (txtFromDate.Text != "" && txtToDate.Text != "")
                        {
                            DateTime dtFrom = Convert.ToDateTime(txtFromDate.Text).Date;
                            DateTime dtTo = Convert.ToDateTime(txtToDate.Text).Date;
                            int itotdays = (dtTo - dtFrom).Days;
                            DateTime dtDay;
                            for (int j = 0; j <= itotdays; j++)
                            {
                                if (j == 0)
                                    dtDay = dtFrom;
                                else
                                    dtDay = dtFrom.AddDays(j);

                                if (dtDay.DayOfWeek.ToString().Equals("Friday"))
                                    i = Convert.ToInt32(objAtt.UpdateFullAtt(Convert.ToInt32(EmpID), 9, dtDay, "", "", txtRemarks.Text, 0, UserID));
                                else
                                {

                                    i = Convert.ToInt32(objAtt.UpdateFullAtt(Convert.ToInt32(EmpID), Convert.ToInt32(ddlStatus.SelectedValue), dtDay, txt.Text, txtOut.Text, txtRemarks.Text, 0, UserID));
                                }
                            }

                        }
                    } 
                    if (i == 1)
                    {
                        hdn.Value = "1";
                        AlertMsg.MsgBox(Page, "Done!", AlertMsg.MessageType.Success);
                    }
                    if (i == 2)
                    {
                        hdn.Value = "1";
                        AlertMsg.MsgBox(Page, "Updated!", AlertMsg.MessageType.Success);
                    }
                }
            }
            catch (Exception ex)
            {
                clsErrorLog.HMSEventLog(ex, "EditAttendance", "gvEmpMultipleDates_RowCommand", "002");
                AlertMsg.MsgBox(Page, "Attendance updation is not allowed while the Salaries are already calculated and posted to Account!", AlertMsg.MessageType.Warning);
            }
        }
        protected void txtDay_TextChanged(object sender, EventArgs e)
        {
            BindPager();
        }
        protected void txtFromDate_TextChanged(object sender, EventArgs e)
        {
            gvEmpMultipleDates.DataSource = null;
            gvEmpMultipleDates.DataBind();
            gdvMonthReport.DataSource = null;
            gdvMonthReport.DataBind();
            gdvMonthReportPrv.DataSource = null;
            gdvMonthReportPrv.DataBind();
            lblPaidDays.Visible = false;
            lblPaidDaysC.Visible = false;
            if (txtSingleEmpName.Text == "" && txtSingleEmpName.Text == string.Empty)
            {
                txtFromDate.Text = "";
                AlertMsg.MsgBox(Page, "Please Enter Employee ID", AlertMsg.MessageType.Warning);
                return;
            }
            else if(DateValidations().Equals(1))
            {
                return;
            }
        }
        protected void txtToDate_TextChanged(object sender, EventArgs e)
        {
            if (txtSingleEmpName.Text == "" && txtSingleEmpName.Text == string.Empty)
            {
                txtToDate.Text = "";
                AlertMsg.MsgBox(Page, "Please Enter Employee ID", AlertMsg.MessageType.Warning);
                gvEmpMultipleDates.DataSource = null;
                gvEmpMultipleDates.DataBind();
                return;
            }
            else if(DateValidations().Equals(1))
            {
                return;
            }
        }
        
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindPager();
        }
        public int DateValidations()
        {
            int ireturn = 0;
            
            if (txtFromDate.Text.Trim() == "")
            {
                AlertMsg.MsgBox(Page, "Please select From Date", AlertMsg.MessageType.Warning);
                gvEmpMultipleDates.DataSource = null;
                gvEmpMultipleDates.DataBind();
                gdvMonthReport.DataSource = null;
                gdvMonthReport.DataBind();
                gdvMonthReportPrv.DataSource = null;
                gdvMonthReportPrv.DataBind();
                lblPaidDays.Visible = false;
                lblPaidDaysC.Visible = false;
                txtToDate.Text = "";
                return 1;
            }
            DateTime dtpresent = DateTime.Now.Date;
            DateTime dtFrom = CodeUtilHMS.ConvertToDate_ddMMMyyy(txtFromDate.Text.Trim(), CodeUtilHMS.DateFormat.ddMMMyyyy);
            //DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);//Convert.ToDateTime(txtFromDate.Text).Date;
            string strActualFDt, strActualTDt; 
            //DateTime dtActualDt = (dtpresent.Month - 1)
            //int imonp = dtpresent.Month, iyearp = dtpresent.Year;
            //int imonf = dtFrom.Month, iyearf = dtFrom.Year;
            //if (imonp == 1)
            //{
            //    strActualFDt = "12/21/" + (iyearp - 1);
            //}
            //else
            //    strActualFDt = (imonp - 1) + "/21/" + iyearp;

            //strActualTDt = imonp + "/20/" + iyearp;

            //int iA = dtFrom.CompareTo(Convert.ToDateTime(strActualFDt));
            //int iC = Convert.ToDateTime(strActualTDt).CompareTo(dtFrom);
            //if (iA < 0)
            //{
            //    AlertMsg.MsgBox(Page, "From Date should be greater than or equal to " + strActualFDt, AlertMsg.MessageType.Warning);
            //    txtFromDate.Text = "";
            //    return 1;
            //}
            //else if(iA == 1 && iC < 0)
            //{
            //    AlertMsg.MsgBox(Page, "From Date should be between these dates - " + strActualFDt + " and " + strActualTDt, AlertMsg.MessageType.Warning);
            //    txtFromDate.Text = "";
            //    return 1;
            //}
            /* if (!imonf.Equals(imonp) && iyearf.Equals(iyearp))
             {
                 AlertMsg.MsgBox(Page, "Please select the From Date as present month.", AlertMsg.MessageType.Warning);
                 txtFromDate.Text = "";
                 return 1;
             }*/
            if (txtFromDate.Text.Trim() != "" && txtToDate.Text.Trim() == "" && gvEmpMultipleDates.DataSource != null)
            {
                AlertMsg.MsgBox(Page, "Please select To Date", AlertMsg.MessageType.Warning);
                return 1;
            }
            if (txtToDate.Text.Trim() != "")
            {
                DateTime dtTo = CodeUtilHMS.ConvertToDate_ddMMMyyy(txtToDate.Text.Trim(), CodeUtilHMS.DateFormat.ddMMMyyyy);
                //DateTime.ParseExact(txtToDate.Text, "dd/MM/yyyy", null);//Convert.ToDateTime(txtToDate.Text).Date;
                int imont = dtTo.Month, iyeart = dtTo.Year;

                /*if (!imont.Equals(imonp) && iyeart.Equals(iyearp))
                {
                    AlertMsg.MsgBox(Page, "Please select the To Date as present month.", AlertMsg.MessageType.Warning);
                    txtToDate.Text = "";
                    return 1;
                }*/
                int i1 = dtTo.CompareTo(dtFrom);
                if (i1 < 0)
                {
                    AlertMsg.MsgBox(Page, "To Date should be greater than From Date.", AlertMsg.MessageType.Warning);
                    txtToDate.Text = "";
                    return 1;
                }
                
                //int iB = Convert.ToDateTime(strActualTDt).CompareTo(dtTo);
                //int iD = dtTo.CompareTo(Convert.ToDateTime(strActualFDt));
                //if (iB < 0)
                //{
                //    AlertMsg.MsgBox(Page, "To Date should be less than or equal to " + strActualTDt, AlertMsg.MessageType.Warning);
                //    txtToDate.Text = "";
                //    return 1;
                //}
                //else if (iB == 1 && iD < 0)
                //{
                //    AlertMsg.MsgBox(Page, "To Date should be between these dates - " + strActualFDt + " and " + strActualTDt, AlertMsg.MessageType.Warning);
                //    txtToDate.Text = "";
                //    return 1;
                //}

            }
            return ireturn;
        }
        public void BindPager()
        {
            DateTime Date = CodeUtilHMS.ConvertToDate_ddMMMyyy(txtDay.Text.Trim(), CodeUtilHMS.DateFormat.ddMMMyyyy);
            Name = txtName.Text;
            string empid;
            if (txtempid.Text == "" && txtempid.Text == string.Empty)
                empid = null;
            else
                empid = txtempid.Text;
            int imonth = Date.Month; int iyear = Date.Year;
            objHrCommon.CurrentPage = EditAttpaging.CurrentPage;
            objHrCommon.PageSize = EditAttpaging.ShowRows;
            int WSID = Convert.ToInt32(ddlWorksite_hid.Value == "" ? "0" : ddlWorksite_hid.Value);
            
            try
            {
                if (Convert.ToInt32(ViewState["WSID"]) > 0)
                    WSID = Convert.ToInt32(ViewState["WSID"]);
            }
            catch { }
            
            DataSet dssearch = GetTodayAttendanceforEditing_By_EmpidType(Convert.ToInt32(ddlDepartment_hid.Value == "" ? "0" : ddlDepartment_hid.Value), WSID, Date, Name, Convert.ToInt32(Session["CompanyID"]), empid, objHrCommon);
            gdvAttend.DataSource = null;
            gdvAttend.DataBind();
            gdvAttend.DataSource = dssearch.Tables[0];
            gdvAttend.DataBind();
            EditAttpaging.Visible = true;
            EditAttpaging.Bind(objHrCommon.CurrentPage, objHrCommon.TotalPages, objHrCommon.NoofRecords, objHrCommon.PageSize);
        }
        protected DataSet GetTodayAttendanceforEditing_By_EmpidType(int DeptID, int WSID, DateTime Date, string Name, int CompanyID, string empid, HRCommon objHrCommon)
        {
            try
            {
                int chkCase = 0;
                if (chkVAcation.Checked)
                    chkCase = 1;
                SqlParameter[] sqlParams = new SqlParameter[11];
                sqlParams[0] = new SqlParameter("@CurrentPage", objHrCommon.CurrentPage);
                sqlParams[1] = new SqlParameter("@PageSize", objHrCommon.PageSize);
                sqlParams[2] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
                sqlParams[2].Direction = ParameterDirection.ReturnValue;
                sqlParams[3] = new SqlParameter("@NoofRecords", System.Data.SqlDbType.Int);
                sqlParams[3].Direction = ParameterDirection.Output;
                sqlParams[4] = new SqlParameter("@DeptID", DeptID);
                sqlParams[5] = new SqlParameter("@WSID", WSID);
                sqlParams[6] = new SqlParameter("@date", Date);
                sqlParams[7] = new SqlParameter("@EmpName", Name);
                sqlParams[8] = new SqlParameter("@CompanyID", CompanyID);
                sqlParams[9] = new SqlParameter("@Empid", empid);
                sqlParams[10] = new SqlParameter("@Case", chkCase);
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetTodayAttendanceforEditing_By_Empid", sqlParams);
                objHrCommon.NoofRecords = (int)sqlParams[3].Value;
                objHrCommon.TotalPages = (int)sqlParams[2].Value;
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected DataSet GetTodayAttendanceforEditing_By_Empid_new(int DeptID, int WSID, DateTime Date, string Name, int CompanyID, string empid, HRCommon objHrCommon)
        {
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[10];
                sqlParams[0] = new SqlParameter("@CurrentPage", objHrCommon.CurrentPage);
                sqlParams[1] = new SqlParameter("@PageSize", objHrCommon.PageSize);
                sqlParams[2] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
                sqlParams[2].Direction = ParameterDirection.ReturnValue;
                sqlParams[3] = new SqlParameter("@NoofRecords", System.Data.SqlDbType.Int);
                sqlParams[3].Direction = ParameterDirection.Output;
                sqlParams[4] = new SqlParameter("@DeptID", DeptID);
                sqlParams[5] = new SqlParameter("@WSID", WSID);
                sqlParams[6] = new SqlParameter("@date", Date);
                sqlParams[7] = new SqlParameter("@EmpName", Name);
                sqlParams[8] = new SqlParameter("@CompanyID", CompanyID);
                sqlParams[9] = new SqlParameter("@Empid", empid);
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetTodayAttendanceforEditing_By_Empid_new", sqlParams);
                objHrCommon.NoofRecords = (int)sqlParams[3].Value;
                objHrCommon.TotalPages = (int)sqlParams[2].Value;
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void btnSearch1_Click(object sender, EventArgs e)
        {
            gvEmpMultipleDates.DataSource = null;
            gvEmpMultipleDates.DataBind();
           
            if (DateValidations().Equals(1))
                return;

            DateTime Date = Convert.ToDateTime(txtFromDate.Text);
            string strName = txtSingleEmpName.Text;
            string strempid = txtsingleEmpNameHidden.Value;
            if (txtSingleEmpName.Text == "" && txtSingleEmpName.Text == string.Empty)
                strempid = null;
            else
                strempid = txtsingleEmpNameHidden.Value;
            int imonth = Date.Month; int iyear = Date.Year;
            objHrCommon.CurrentPage = 1;
            objHrCommon.PageSize = 10;

            try
            {
                DataSet dssearch = GetTodayAttendanceforEditing_By_Empid_new(0, 0, Date, "", Convert.ToInt32(Session["CompanyID"]), strempid, objHrCommon);
                gvEmpMultipleDates.DataSource = dssearch.Tables[0];
                gvEmpMultipleDates.DataBind();
                lblPaidDays.Visible = true;
                lblPaidDaysC.Visible = true;
                PayableCalculationDetails(Convert.ToInt32(strempid), iyear, imonth, 1);
                PrePayableCalculationDetails(Convert.ToInt32(strempid), iyear, imonth, 2);
            }
            catch { }   
        }
        private void PayableCalculationDetails(int EmpID, int Year, int Month, int ID)
        {
            gdvMonthReport.Visible = true;
            gdvMonthReport.DataSource = null;
            gdvMonthReport.DataBind();

            SqlParameter[] objParam = new SqlParameter[4];
            objParam[0] = new SqlParameter("@month", Month);
            objParam[1] = new SqlParameter("@year", Year);
            objParam[2] = new SqlParameter("@Empid", EmpID);
            objParam[3] = new SqlParameter("@id", ID);
            dsEPMData = SQLDBUtil.ExecuteDataset("sh_MonthAttendanceDetails", objParam);
            if (dsEPMData != null && dsEPMData.Tables.Count > 0 && dsEPMData.Tables[0].Rows.Count > 0)
            {
                gdvMonthReport.DataSource = dsEPMData.Tables[0];
            }
            else
                gdvMonthReport.DataSource = null;
            gdvMonthReport.DataBind();
        }
        private void PrePayableCalculationDetails(int EmpID, int Year, int Month, int ID)
        {
            gdvMonthReportPrv.Visible = true;
            gdvMonthReportPrv.DataSource = null;
            gdvMonthReportPrv.DataBind();
            SqlParameter[] objParam = new SqlParameter[4];
            objParam[0] = new SqlParameter("@month", Month);
            objParam[1] = new SqlParameter("@year", Year);
            objParam[2] = new SqlParameter("@Empid", EmpID);
            objParam[3] = new SqlParameter("@id", ID);
            dsCurrentDetails = SQLDBUtil.ExecuteDataset("sh_MonthAttendanceDetails_Prev", objParam);
            if (dsCurrentDetails != null && dsCurrentDetails.Tables.Count > 0 && dsCurrentDetails.Tables[0].Rows.Count > 0)
            {
                gdvMonthReportPrv.DataSource = dsCurrentDetails.Tables[0];
            }
            else
                gdvMonthReportPrv.DataSource = null;
            gdvMonthReportPrv.DataBind();
        }
        protected void gdvMonthReport_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.RowIndex >= 0)
                {
                    if (gdvMonthReport.DataSource != null)
                    {
                        int i = 1;
                        foreach (DataRow drEMP in dsEPMData.Tables[1].Rows)
                        {
                            int Pay = int.Parse(drEMP["Pay"].ToString());
                            if (Pay == 1)
                            {
                                e.Row.Cells[i + 1].BackColor = Color.Green;
                            }
                            else if (Pay == 0)
                            {
                                e.Row.Cells[i + 1].BackColor = Color.Orange;
                            }
                            i = i + 1;
                        }
                    }
                }
            }
        }
        protected void gdvMonthReportPrv_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.RowIndex >= 0)
                {
                    if (gdvMonthReportPrv.DataSource != null)
                    {
                        int i = 1;
                        foreach (DataRow drEMP in dsCurrentDetails.Tables[1].Rows)
                        {
                            int Pay = int.Parse(drEMP["Pay"].ToString());
                            if (Pay == 1)
                            {
                                e.Row.Cells[i + 1].BackColor = Color.Green;
                            }
                            else if (Pay == 0)
                            {
                                e.Row.Cells[i + 1].BackColor = Color.Orange;
                            }
                            i = i + 1;
                        }
                    }
                }
            }
        }
        public static string[] ConvertStingArray(DataSet ds)
        {
            String[] rtval = Array.ConvertAll(ds.Tables[0].Select(), new Converter<DataRow, string>(DataRowToDouble));
            return rtval;
        }
        public static string DataRowToDouble(DataRow dr)
        {
            return dr["EmpId"].ToString();
        }
        protected void btnApplyselected_Click(object sender, EventArgs e)
        {
            try
            {
                int i = 0, j = 0;
                CheckBox chkAll = null;
                GridViewRow gv = gdvAttend.HeaderRow;
                if (gv.RowType == DataControlRowType.Header)
                {
                    chkAll = (CheckBox)gv.FindControl("chkAll");
                    if (chkAll.Checked)
                    {
                        j = Convert.ToInt32(ddlAttType.SelectedValue);
                    }
                }
                foreach (GridViewRow gvr in gdvAttend.Rows)
                {
                    CheckBox chk = (CheckBox)gvr.FindControl("chkSelect");
                    if (chk.Checked)
                    {
                        Label lblEmpID = (Label)gvr.FindControl("lblEmpID");
                        DropDownList ddlStatus = (DropDownList)gdvAttend.Rows[gvr.RowIndex].Cells[3].Controls[1];
                        if (chkAll.Checked)
                        {
                            ddlStatus.SelectedValue = j.ToString();
                        }
                        CheckBox chkOut = (CheckBox)gdvAttend.Rows[gvr.RowIndex].Cells[5].FindControl("chkOut");
                        TextBox txtOut = (TextBox)gdvAttend.Rows[gvr.RowIndex].Cells[6].FindControl("txtOUT");
                        TextBox txt = (TextBox)gdvAttend.Rows[gvr.RowIndex].Cells[4].Controls[1];
                        if (txttime.Text != null && txttime.Text != "" && ddlStatus.SelectedValue == 2.ToString())
                        {
                            var cultureSource = new CultureInfo("en-US", false);
                            var cultureDest = new CultureInfo("de-DE", false);
                            var dt = DateTime.Parse(txttime.Text, cultureSource);
                            txt.Text = dt.ToString("t", cultureDest);
                            // txt.Text = (Convert.ToDateTime(txttime.Text)).ToString("HH:mm");
                        }
                        if(txtouttime.Text != null && txtouttime.Text != "" && ddlStatus.SelectedValue == 2.ToString() && txt.Text!="")
                        {
                            var cultureSource = new CultureInfo("en-US", false);
                            var cultureDest = new CultureInfo("de-DE", false);
                            var dt = DateTime.Parse(txtouttime.Text, cultureSource);
                            txtOut.Text = dt.ToString("t", cultureDest);
                        }
                        TextBox txtRemarks = (TextBox)gdvAttend.Rows[gvr.RowIndex].Cells[7].FindControl("txtRemarks");
                        int SiteID = Convert.ToInt32(WSID);
                        int UserID =  Convert.ToInt32(Session["UserId"]);
                        int EmpID = Convert.ToInt32(lblEmpID.Text);
                        if (Convert.ToInt32(ddlStatus.SelectedValue) == 2)
                        {
                            txt.Enabled = true;
                            txtOut.Enabled = true;
                        }
                        else
                        {
                            txt.Enabled = false;
                            txtOut.Enabled = false;
                        }
                        try
                        {
                            if (Convert.ToInt32(ViewState["WSID"]) > 0)
                                SiteID = Convert.ToInt32(ViewState["WSID"]);
                        }
                        catch { }
                        if (txt.Text != null && txt.Text != "" && txtOut.Text != null && txtOut.Text != "")
                        {
                            if (Convert.ToDateTime(txt.Text) <= Convert.ToDateTime(txtOut.Text))
                            {
                                txtOut.Text = txtOut.Text;
                            }
                            else
                            {
                                AlertMsg.MsgBox(Page, "OUT TIME should be greater than IN TIME", AlertMsg.MessageType.Warning);
                                //lblStatus.Text = "OUT TIME should be greater than IN TIME";
                                //lblStatus.ForeColor = System.Drawing.Color.Red;
                                txtOut.Text = string.Empty;
                                txtOut.Focus();
                                return;
                            }
                        }
                        i = Convert.ToInt32(objAtt.UpdateFullAtt(Convert.ToInt32(EmpID), Convert.ToInt32(ddlAttType.SelectedValue), CodeUtilHMS.ConvertToDate_ddMMMyyy(txtDay.Text.Trim(), CodeUtilHMS.DateFormat.ddMMMyyyy), txttime.Text, txtouttime.Text, txtRemarks.Text, SiteID, UserID));
                        if (ddlStatus.SelectedValue == 1.ToString())
                            txt.Text = string.Empty;
                    }
                }
                if (i > 0)
                {
                    hdn.Value = "1";
                    AlertMsg.MsgBox(Page, "Applied!", AlertMsg.MessageType.Success);
                    //lblStatus.Text = "Applied!";
                    //lblStatus.ForeColor = System.Drawing.Color.Green;
                    txttime.Text = string.Empty;
                    txtouttime.Text = string.Empty;
                }
            }
            catch (Exception ex)
            {
                clsErrorLog.HMSEventLog(ex, "EditAttendance", "btnApplyselected_Click", "004");
                AlertMsg.MsgBox(Page,"Salary Already Calculated for this month", AlertMsg.MessageType.Warning);
            }
        }
        protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DropDownList ddl = (DropDownList)sender;
                GridViewRow gvr = (GridViewRow)(((Control)sender).NamingContainer);
                TextBox txt = (TextBox)gvr.Cells[4].FindControl("txtIN"); 
                if (ddl.SelectedValue == "2")
                {
                    txt.Text = DateTime.Now.ToShortTimeString();
                }
                else
                {
                    txt.Text = "";
                }
            }
            catch (Exception ex)
            {
                clsErrorLog.HMSEventLog(ex, "EditAttendance", "ddlStatus_SelectedIndexChanged", "005");
            }
        }
        protected void ddlAttType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlAttType.SelectedValue != 2.ToString())
            {
                txttime.Enabled = false;
                txtouttime.Enabled = false;
            }
            else
            {
                txttime.Enabled = true;
                txtouttime.Enabled = true;
            }
        }
    }
}