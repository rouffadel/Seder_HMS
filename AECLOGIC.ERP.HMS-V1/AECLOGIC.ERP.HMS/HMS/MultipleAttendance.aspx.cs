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
namespace AECLOGIC.ERP.HMS
{
    public partial class MultipleAttendance : AECLOGIC.ERP.COMMON.WebFormMaster
    {
        int mid = 0;
        bool viewall;
        string menuname;
        string menuid;
        static int CompanyID = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["CompanyID"].ToString());
        //  static int CompanyID=1;
        static int WSId = 0;
        // static int  EmpID=0;
        static int SiteSearch;
        static int SiteID;
        static int Deptid = 0;
        static char Staus = '1';
        AttendanceDAC objAtt = new AttendanceDAC();
        HRCommon objHrCommon = new HRCommon();
        static int ModID;
        static int Userid;
        protected override void OnInit(EventArgs e)
        {
            ModuleID = 1;
            base.OnInit(e);
            ViewApplilstPaging.FirstClick += new Paging.PageFirst(ViewApplilstPaging_FirstClick);
            ViewApplilstPaging.PreviousClick += new Paging.PagePrevious(ViewApplilstPaging_FirstClick);
            ViewApplilstPaging.NextClick += new Paging.PageNext(ViewApplilstPaging_FirstClick);
            ViewApplilstPaging.LastClick += new Paging.PageLast(ViewApplilstPaging_FirstClick);
            ViewApplilstPaging.ChangeClick += new Paging.PageChange(ViewApplilstPaging_FirstClick);
            ViewApplilstPaging.ShowRowsClick += new Paging.ShowRowsChange(ViewApplilstPaging_ShowRowsClick);
            ViewApplilstPaging.CurrentPage = 1;
            ModID = ModuleID;
        }
        void ViewApplilstPaging_ShowRowsClick(object sender, EventArgs e)
        {
            ViewApplilstPaging.CurrentPage = 1;
            BindPager();
        }
        void ViewApplilstPaging_FirstClick(object sender, EventArgs e)
        {
            BindPager();
        }
        void BindPager()
        {
            objHrCommon.PageSize = ViewApplilstPaging.CurrentPage;
            objHrCommon.CurrentPage = ViewApplilstPaging.ShowRows;
            BindGrid();
            //ApplicantListDataBind(objHrCommon);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            Ajax.Utility.RegisterTypeForAjax(typeof(AjaxDAL));
            CompanyID = Convert.ToInt32(Session["CompanyID"]);
            Userid = Convert.ToInt32(Convert.ToInt32(Session["UserId"]).ToString());
            if (!IsPostBack)
            {
                Ajax.Utility.RegisterTypeForAjax(typeof(AjaxDAL));
                try
                {
                    txtDate.Text = DateTime.Now.ToString("dd MMM yyyy");
                    string URL = Request.Url.Segments[Request.Url.Segments.Length - 1] + Request.Url.Query;
                    int RoleId = Convert.ToInt32(Session["RoleId"].ToString());
                    int ModuleId = ModuleID; ;
                    DataSet ds = AttendanceDAC.GetAllowed(RoleId, ModuleId, URL);
                    //dstemp = GetWorkSites(objAtt, dstemp);
                    //dstemp = GetDepartments(objAtt, dstemp);
                    BindDesignations();
                    DataSet dstemp = BindGrid();
                }
                catch
                {
                }
            }
        }
        public void BindDesignations()
        {
            DataSet ds = (DataSet)objAtt.GetDesignations();
            ddlDesif2.DataSource = ds;
            ddlDesif2.DataTextField = "Designation";
            ddlDesif2.DataValueField = "DesigId";
            ddlDesif2.DataBind();
            ddlDesif2.Items.Insert(0, new ListItem("---ALL---", "0", true));
        }
        private DataSet BindGrid()
        {
            objHrCommon.PageSize = ViewApplilstPaging.ShowRows;
            objHrCommon.CurrentPage = ViewApplilstPaging.CurrentPage;
            int? DeptID = null;
            int? WsID = null;
            if (textdept.Text.Trim() != "")
            {
                DeptID = Convert.ToInt32(ddlDepartment_hid.Value == "" ? "0" : ddlDepartment_hid.Value);
            }
            if (txtSearchWorksite.Text.Trim() != "")
            {
                WsID = Convert.ToInt32(ddlWorksite_hid.Value == "" ? "0" : ddlWorksite_hid.Value);
            }
            int? DesigID = null;
            if (textdesg.Text.Trim() != "")
            {
                DesigID = Convert.ToInt32(ddlDesif2_hid.Value == "" ? "0" : ddlDesif2_hid.Value);
            }
            int? EmpID = null;
            if (txtEmpID.Text.Trim() != "")
            {
                EmpID = Convert.ToInt32(txtEmpID.Text.Trim());
            }
            DataSet dsAtt = objAtt.GetTodayAttendanceMultiple(DeptID, WsID, CodeUtilHMS.ConvertToDate_ddMMMyyy(txtDate.Text.Trim(), CodeUtilHMS.DateFormat.ddMMMyyyy), DesigID, EmpID, txtEmpName.Text, objHrCommon);
            if (dsAtt.Tables.Count > 0 && dsAtt.Tables[0].Rows.Count > 0)
            {
                gdvAttend.DataSource = dsAtt.Tables[0];
                gdvAttend.DataBind();
                btnSaveAll.Visible = true;
            }
            else
            {
                gdvAttend.DataSource = null;
                gdvAttend.DataBind();
                btnSaveAll.Visible = false;
            }
            ViewApplilstPaging.Bind(objHrCommon.CurrentPage, objHrCommon.TotalPages, objHrCommon.NoofRecords, objHrCommon.PageSize);
            return dsAtt;
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionList(string prefixText, int count, string contextKey)
        {
            WSId = 0;
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
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionList_dept(string prefixText, int count, string contextKey)
        {
            DataSet ds = AttendanceDAC.BindDeparmetBySite_googlesearch(prefixText, WSId, CompanyID);
            DataTable dt = ds.Tables[0];
            List<string> items = new List<string>(count);
            var rtval = new Dictionary<string, string>();
            foreach (DataRow row in dt.Rows)
            {
                string str = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(row["DepartmentName"].ToString(), row["DepartmentUId"].ToString());
                items.Add(str);
            }
            return items.ToArray(); ;// txtItems.ToArray();
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionList_desg(string prefixText, int count, string contextKey)
        {
            DataSet ds = AttendanceDAC.HR_googlesearch_GetDesignations(prefixText);
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
        public static string[] GetCompletionList_empid(string prefixText, int count, string contextKey)
        {
            DataSet ds = AttendanceDAC.GetGoogleSearch_by_Empid(prefixText);
            return ConvertStingArray(ds);// txtItems.ToArray();
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
        protected void GetWork(object sender, EventArgs e)
        {
            CompanyID = Convert.ToInt32(Session["CompanyID"]);
            WSId = Convert.ToInt32(ddlWorksite_hid.Value == "" ? "0" : ddlWorksite_hid.Value); ;
        }
        protected void Getdept(object sender, EventArgs e)
        {
            Deptid = Convert.ToInt32(ddlDepartment_hid.Value == "" ? "0" : ddlDepartment_hid.Value);
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindGrid();
        }
        protected void gdvAttend_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                CheckBox chkSelect = (CheckBox)e.Row.FindControl("chkSelect");
                CheckBox chkOut = (CheckBox)e.Row.FindControl("chkOut");
                TextBox txtOut = (TextBox)e.Row.FindControl("txtOUT");
                TextBox txtRemarks = (TextBox)e.Row.FindControl("txtRemarks");
                TextBox txt = (TextBox)e.Row.FindControl("txtIN");
                Label lblMID = (Label)e.Row.FindControl("lblMID");
                LinkButton lnkSave = (LinkButton)e.Row.FindControl("lnkSave");
                if (txtOut.Text != "")
                {
                    // chkOut.Checked = true;
                    // chkOut.Enabled = false;
                }
                if (txt.Text == "")
                {
                    chkOut.Enabled = true;
                    chkOut.Checked = false;
                    txtOut.Text = "";
                }
                if (lblMID.Text != "0" && txtOut.Text != "")
                {
                    lnkSave.Enabled = false;
                }
                string EmpId = gdvAttend.DataKeys[e.Row.RowIndex].Value.ToString();
                chkSelect.Attributes.Add("onclick", "javascript:return GetInTime('" + chkSelect.ClientID + "','" + DateTime.Now.ToShortTimeString() + "','" + txt.ClientID + "','" + txtOut.ClientID + "','" + EmpId + "');");
                chkOut.Attributes.Add("onclick", "javascript:return GetOutTime('" + chkOut.ClientID + "','" + DateTime.Now.ToShortTimeString() + "','" + txtOut.ClientID + "','" + txt.ClientID + "','" + EmpId + "');");
            }
        }
        protected void gdvAttend_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "save")
            {
                int EmpId = Convert.ToInt32(e.CommandArgument);
                int retval;
                LinkButton lnkSave = new LinkButton();
                GridViewRow selectedRow = (GridViewRow)(((Control)e.CommandSource).NamingContainer);
                if (txtDate.Text == "")
                {
                    AlertMsg.MsgBox(Page, "select Date ", AlertMsg.MessageType.Warning);
                    txtDate.Focus();
                    return;
                }
                DateTime dtselectDate = CodeUtilHMS.ConvertToDate_ddMMMyyy(txtDate.Text, CodeUtilHMS.DateFormat.ddMMMyyyy);
                DateTime dtNow = CODEUtility.ConvertToDate(DateTime.Now.ToString("dd/MM/yyyy"), DateFormat.DayMonthYear);
                if (dtselectDate != dtNow)
                {
                    AlertMsg.MsgBox(Page, "select Date sould be current date", AlertMsg.MessageType.Warning);
                    txtDate.Focus();
                    return;
                }
                TextBox txtIN = (TextBox)selectedRow.FindControl("txtIN");
                TextBox txtOUT = (TextBox)selectedRow.FindControl("txtOUT");
                // change done.
                if (txtIN.Text != null && txtIN.Text != "" && txtOUT.Text != null && txtOUT.Text != "")
                {
                    if (Convert.ToDateTime(txtIN.Text) < Convert.ToDateTime(txtOUT.Text))
                    {
                        txtOUT.Text = txtOUT.Text;
                    }
                    else
                    {
                        AlertMsg.MsgBox(Page, "OUT TIME should be greater than IN TIME", AlertMsg.MessageType.Warning);
                        txtOUT.Text = string.Empty;
                        txtOUT.Focus();
                        return;
                    }
                }
                TextBox txtremarks = (TextBox)selectedRow.FindControl("txtRemarks");
                Label lblMID = (Label)selectedRow.FindControl("lblMID");
                retval = Convert.ToInt32(AttendanceDAC.InsUpdateMultipleAtt(EmpId, txtIN.Text.Trim(), txtOUT.Text.Trim(), txtremarks.Text, CodeUtilHMS.ConvertToDate_ddMMMyyy(txtDate.Text.Trim(), CodeUtilHMS.DateFormat.ddMMMyyyy), Convert.ToInt32(Session["UserId"]), Convert.ToInt32(lblMID.Text)));
                if (retval == 3)
                {
                    AlertMsg.MsgBox(Page, "Intime/Outtime exist in previous entries in the same day", AlertMsg.MessageType.Info);
                }
                else
                {
                    AlertMsg.MsgBox(Page, "Done");
                }
                BindGrid();
            }
        }
        protected void btnSaveAll_Click(object sender, EventArgs e)
        {
            int retval = 0, SubRec = 0, NotSubRec = 0, count = 0;
            foreach (GridViewRow gvr in gdvAttend.Rows)
            {
                CheckBox chk = (CheckBox)gvr.FindControl("chkSelect");
                if (chk.Checked)
                {
                    count++;
                    Label lblEmpID = (Label)gvr.FindControl("lblEmpID");
                    TextBox txtOut = (TextBox)gvr.FindControl("txtOUT");
                    TextBox txtRemarks = (TextBox)gvr.FindControl("txtRemarks");
                    TextBox txt = (TextBox)gvr.FindControl("txtIN");
                    Label lblMID = (Label)gvr.FindControl("lblMID");
                    retval = Convert.ToInt32(AttendanceDAC.InsUpdateMultipleAtt(Convert.ToInt32(lblEmpID.Text), txt.Text.Trim(), txtOut.Text.Trim(), txtRemarks.Text, CodeUtilHMS.ConvertToDate_ddMMMyyy(txtDate.Text.Trim(), CodeUtilHMS.DateFormat.ddMMMyyyy), Convert.ToInt32(Session["UserId"]), Convert.ToInt32(lblMID.Text)));
                }
                if (retval == 3)
                {
                    NotSubRec = NotSubRec + 1;
                }
                else
                {
                    SubRec = SubRec + 1;
                }
            }
            AlertMsg.MsgBox(Page, count.ToString() + " Records done");
            BindGrid();
        }
        protected void btnfill_Click(object sender, EventArgs e)
        {
            if (txttime.Text != null && txttime.Text != "" && txtouttime.Text != null && txtouttime.Text != "")
            {
                if (Convert.ToDateTime(txttime.Text) <= Convert.ToDateTime(txtouttime.Text))
                {
                    try
                    {
                        foreach (GridViewRow gvr in gdvAttend.Rows)
                        {
                            CheckBox chk = (CheckBox)gvr.FindControl("chkSelect");
                            if (chk.Checked == true)
                            {
                                TextBox txt = (TextBox)gvr.FindControl("txtIN");
                                TextBox txtOut = (TextBox)gvr.FindControl("txtOUT");
                                txt.Text = txttime.Text;
                                txtOut.Text = txtouttime.Text;
                            }
                        }
                    }
                    catch
                    {
                    }
                }
                else
                {
                    AlertMsg.MsgBox(Page, "OUT TIME should be greater than IN TIME", AlertMsg.MessageType.Warning);
                    txtouttime.Text = string.Empty;
                    txtouttime.Focus();
                    return;
                }
            }
        }
        protected void chktmrw_CheckedChanged(object sender, EventArgs e)
        {
            if (chktmrw.Checked)
            {
                btnfill.Visible = false;
                btnSaveAll.Visible = false;
                btnSavetmrw.Visible = true;
            }
            else
            {
                btnfill.Visible = true;
                btnSaveAll.Visible = true;
                btnSavetmrw.Visible = false;
            }
        }
        protected void btnSavetmrw_Click(object sender, EventArgs e)
        {
            if (txttime.Text != null && txttime.Text != "" && txtouttime.Text != null && txtouttime.Text != "")
            {
                int retval = 0, SubRec = 0, NotSubRec = 0, count = 0;
                foreach (GridViewRow gvr in gdvAttend.Rows)
                {
                    CheckBox chk = (CheckBox)gvr.FindControl("chkSelect");
                    if (chk.Checked)
                    {
                        count++;
                        Label lblEmpID = (Label)gvr.FindControl("lblEmpID");
                        Label lblMID = (Label)gvr.FindControl("lblMID");
                        retval = Convert.ToInt32(AttendanceDAC.InsUpdateMultipleAtt_MultiDay(Convert.ToInt32(lblEmpID.Text), txttime.Text.Trim(),
                            txtouttime.Text.Trim(), "MultiDay Insert", CodeUtilHMS.ConvertToDate_ddMMMyyy(txtDate.Text.Trim(), CodeUtilHMS.DateFormat.ddMMMyyyy), Convert.ToInt32(Session["UserId"]), 0));
                    }
                    if (retval == 3)
                    {
                        NotSubRec = NotSubRec + 1;
                    }
                    else
                    {
                        SubRec = SubRec + 1;
                    }
                }
                AlertMsg.MsgBox(Page, SubRec.ToString() + " Records done");
                if (NotSubRec > 0)
                {
                    AlertMsg.MsgBox(Page, NotSubRec.ToString() + " Intime/Outtime exist in previous entries in the same day", AlertMsg.MessageType.Info);
                }
            }
            else
            {
                AlertMsg.MsgBox(Page, "Intime /Outtime should not be empty");
            }
        }
    }
}