using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AECLOGIC.HMS.BLL;
using System.Data;
using System.Data.SqlClient;
using Aeclogic.Common.DAL;
using System.IO;
using System.Configuration;
using System.Collections;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using AECLOGIC.ERP.COMMON;
namespace AECLOGIC.ERP.HMS
{
    public partial class Shifts : AECLOGIC.ERP.COMMON.WebFormMaster
    {
        AttendanceDAC objAtt = new AttendanceDAC();
        int mid = 0;
        bool viewall;
        string menuname;
        string menuid;
        bool Editable;
        static int CompanyID;
        static int Siteid;
        //int WS, Dept, DisID;
        HRCommon objHrCommon = new HRCommon();
        protected override void OnInit(EventArgs e)
        {
            ModuleID = 1;
            base.OnInit(e);
            ShiftsPaging.FirstClick += new Paging.PageFirst(ShiftsPaging_FirstClick);
            ShiftsPaging.PreviousClick += new Paging.PagePrevious(ShiftsPaging_FirstClick);
            ShiftsPaging.NextClick += new Paging.PageNext(ShiftsPaging_FirstClick);
            ShiftsPaging.LastClick += new Paging.PageLast(ShiftsPaging_FirstClick);
            ShiftsPaging.ChangeClick += new Paging.PageChange(ShiftsPaging_FirstClick);
            ShiftsPaging.ShowRowsClick += new Paging.ShowRowsChange(ShiftsPaging_ShowRowsClick);
            ShiftsPaging.CurrentPage = 1;
        }
        void ShiftsPaging_ShowRowsClick(object sender, EventArgs e)
        {
            ShiftsPaging.CurrentPage = 1;
            BindPager();
        }
        void ShiftsPaging_FirstClick(object sender, EventArgs e)
        {
            if (hdnSearchChange.Value == "1")
                ShiftsPaging.CurrentPage = 1;
            BindPager();
            hdnSearchChange.Value = "0";
        }
        void BindPager()
        {
            objHrCommon.PageSize = ShiftsPaging.ShowRows;
            objHrCommon.CurrentPage = ShiftsPaging.CurrentPage;
            EmployeeShifts(objHrCommon);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (gvEmp.Rows.Count == 0)
            {
                btnMove.Enabled = false;
                lblWarn.Text = "Use Above Controls for Required Records!";
            }
            else
            {
                btnMove.Enabled = true;
                lblWarn.Text = "";
            }
            tblMain.Visible = true;
            if (!IsPostBack)
            {
                GetParentMenuId();
                lblWarn.Text = "";
                trNote.Visible = false;
                trChoose.Visible = false;
                trNote.Visible = false;
                GetWs();
                BindDepartments();
                BindDesignations();
                FIllObject.FillDropDown(ref ddlShift, "HR_GetShifts");
                FIllObject.FillDropDown(ref ddlshifts, "HR_GetShifts");
                GetShift();
                BindEmpList();
                BindPager();
                ViewState["WS"] = 0;
                ViewState["Dept"] = 0;
                ViewState["Shift"] = 0;
                ViewState["DisID"] = 0;
            }
        }
        private void GetShift()
        {
            DataSet dsShift = AttendanceDAC.GetShiftsList();
            ddlMoveShift.DataSource = dsShift.Tables[0];
            ddlMoveShift.DataTextField = "Name";
            ddlMoveShift.DataValueField = "ShiftID";
            ddlMoveShift.DataBind();
            ddlMoveShift.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        public void EmployeeShifts(HRCommon objHrCommon)
        {
            ddlDept.Enabled = ddlName.Enabled = ddlShift.Enabled = true;
            trgv.Visible = true;
            int Empid=0;
            int WS = 0; int Dept = 0; int DisID = 0;
            if (ddlWorksite.SelectedIndex != 0)
                WS = Convert.ToInt32(ddlWorksite.SelectedValue);
            ViewState["WS"] = WS;
            if (ddlDept.SelectedIndex != 0)
                Dept = Convert.ToInt32(ddlDept.SelectedValue);
            ViewState["Dept"] = Dept;
            if (ddlDesignation.SelectedIndex != 0)
                DisID = Convert.ToInt32(ddlDesignation.SelectedValue);
            ViewState["DisID"] = DisID;
            if (textsearchemp.Text != "") { Empid = Convert.ToInt32(textsearchemp.Text.Substring(0,4)); }
            int Shift = Convert.ToInt32(ddlShift.SelectedValue);
            ViewState["Shift"] = Shift;
            DataSet ds = AttendanceDAC.HR_ShiftWiseEmpByPaging(objHrCommon, WS, Dept, Empid, Shift, DisID, Convert.ToInt32(Session["CompanyID"]));
            if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0)
            {
                gvEmp.DataSource = ds;
                ShiftsPaging.Bind(objHrCommon.CurrentPage, objHrCommon.TotalPages, objHrCommon.NoofRecords, objHrCommon.PageSize);
                // tblMove.Visible = true;
                btnMove.Visible = true;
            }
            else
            {
                gvEmp.EmptyDataText = "No Records Found";
                ShiftsPaging.Visible = false;
                tblMove.Visible = false;
                btnMove.Visible = false;
            }
            gvEmp.DataBind();
        }
        public int GetParentMenuId()
        {
            string URL = Request.Url.Segments[Request.Url.Segments.Length - 1] + Request.Url.Query;
            int RoleId = Convert.ToInt32(Session["RoleId"].ToString());
            int ModuleId = ModuleID;;
            DataSet ds = AttendanceDAC.GetAllowed(RoleId, ModuleId, URL);
            int MenuId = 0;
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                MenuId = Convert.ToInt32(ds.Tables[0].Rows[0]["Under"]);
                Editable = (bool)ds.Tables[0].Rows[0]["Editable"];
                btnMove.Enabled = Editable;
            }
            return MenuId;
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute]
        public static string[] GetCompletionList(string prefixText, int count, string contextKey)
        {
            DataSet ds = AttendanceDAC.GetWorkSite_By_googlesearch_Emp(prefixText);
            return ConvertStingArray(ds);
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute]
        public static string[] GetCompletionListdept(string prefixText, int count, string contextKey)
        {
            DataSet ds = AttendanceDAC.GetDepartmentGoogleSerc(prefixText, CompanyID, Siteid);
            return ConvertStingArray(ds);
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute]
        ///////////////////////////
        public static string[] GetCompletionListemp(string prefixText, int count, string contextKey)
    {
        DataSet ds = AttendanceDAC.GetGoogleSerachEmployee(prefixText);
        return ConvertStingArray(ds);
        }
     //
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute]
        public static string[] GetCompletionshiftlist(string prefixText, int count, string contextKey)
        {
            DataSet ds = AttendanceDAC.Getshifts_googlesearch(prefixText);
            return ConvertStingArray(ds);
        }
           [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute]
        public static string[] GetCompletiondesglist(string prefixText, int count, string contextKey)
        {
            DataSet ds = AttendanceDAC.GetGoogleSerachDesignations(prefixText);
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
        protected void Worksidechangewithdep(object sender, EventArgs e)
        {
            SqlParameter[] parm = new SqlParameter[1];
            parm[0] = new SqlParameter("@search", txtSearchWorksite.Text);
            FIllObject.FillDropDown(ref ddlWorksite, "HR_GetWorkSite_googlesearch_By_Emp", parm);
            ListItem itmSelected = ddlWorksite.Items.FindByText(txtSearchWorksite.Text);
            if (itmSelected != null)
            {
                ddlWorksite.SelectedItem.Selected = false;
                itmSelected.Selected = true;
            }
            CompanyID = Convert.ToInt32(Session["CompanyID"]);
            ddlWorksite_SelectedIndexChanged(sender, e);
            Siteid = Convert.ToInt32(ddlWorksite.SelectedValue);
        }
        protected void GetDepartmentSearch(object sender, EventArgs e)
        {
            SqlParameter[] parm = new SqlParameter[2];
            parm[0] = new SqlParameter("@search ", txtSearchdept.Text);
            parm[1] = new SqlParameter("@SiteID", ddlWorksite.SelectedItem.Value);
            FIllObject.FillDropDown(ref ddlDept, "HMS_Department_googlesearch_by_siteid", parm);
            ListItem itmSelected = ddlDept.Items.FindByText(txtSearchdept.Text);
            if (itmSelected != null)
            {
                ddlDept.SelectedItem.Selected = false;
                itmSelected.Selected = true;
            }
        }
        protected void GetempSearch(object sender, EventArgs e)
      {
          SqlParameter[] parm = new SqlParameter[2];
          parm[0] = new SqlParameter("@search", textsearchemp.Text);
          parm[1] = new SqlParameter("@CompanyID", ddlWorksite.SelectedItem.Value);
          FIllObject.FillDropDown(ref ddlName, "GetEmployeesByCompID_googlesearch", parm);
          ListItem itmSelected = ddlName.Items.FindByText(textsearchemp.Text);
          if (itmSelected != null)
          {
              ddlName.SelectedItem.Selected = false;
              itmSelected.Selected = true;
          }
        }
        protected void Getshiftlist(object sender, EventArgs e)
        {
            SqlParameter[] parm = new SqlParameter[1];
            parm[0] = new SqlParameter("@search", textshift.Text);
            FIllObject.FillDropDown(ref ddlShift, "HR_GetShifts_googlesearch", parm);
            ListItem itmSelected = ddlShift.Items.FindByText(textshift.Text);
            if (itmSelected != null)
            {
                ddlShift.SelectedItem.Selected = false;
                itmSelected.Selected = true;
            }
        }
          protected void Getdesglist(object sender, EventArgs e)
        {
                SqlParameter[] param = new SqlParameter[1];
                 param[0] = new SqlParameter("@Search", Textdesg.Text);
                FIllObject.FillDropDown(ref ddlShift, "HR_GetSearchgoogleDesignations", param);
                ListItem itmSelected = ddlDesignation.Items.FindByText(Textdesg.Text);
                if (itmSelected != null)
                {
                    ddlDesignation.SelectedItem.Selected = false;
                    itmSelected.Selected = true;
                }
        }
        public void BindDesignations()
        {
            DataSet  ds = (DataSet)objAtt.GetDesignations();
            ddlDesignation.DataSource = ds;
            ddlDesignation.DataTextField = "Designation";
            ddlDesignation.DataValueField = "DesigId";
            ddlDesignation.DataBind();
            ddlDesignation.Items.Insert(0, new ListItem("---ALL---", "0", true));
            ddlDesif2.DataSource = ds;
            ddlDesif2.DataTextField = "Designation";
            ddlDesif2.DataValueField = "DesigId";
            ddlDesif2.DataBind();
            ddlDesif2.Items.Insert(0, new ListItem("---ALL---", "0", true));
        }
        public void BindDepartments()
        {
            try
            {
                DataSet ds = (DataSet)objAtt.GetDaprtmentList();
                ddlDepart.DataSource = ds;
                ddlDepart.DataTextField = "Deptname";
                ddlDepart.DataValueField = "DepartmentUId";
                ddlDepart.DataBind();
                ddlDepart.Items.Insert(0, new ListItem("---ALL---", "0", true));
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public void GetWs()
        {
            DataSet dstemp = AttendanceDAC.GetWorkSite_By_EmpList();
            ddlWorksite.DataSource = dstemp.Tables[0];
            ddlWorksite.DataTextField = "Site_Name";
            ddlWorksite.DataValueField = "Site_ID";
            ddlWorksite.DataBind();
            if (Convert.ToInt32(Session["MonitorSite"]) != 0)
            {
                ddlWorksite.Items.FindByValue(Session["MonitorSite"].ToString()).Selected = true;
                ddlWorksite.Enabled = false;
            }
            else
            {
                ddlWorksite.Items.Insert(0, new ListItem("---ALL---", "0", true));
                ddlWorksite.SelectedItem.Value = "0";
            }
            ddlWs2.DataSource = dstemp.Tables[0];
            ddlWs2.DataTextField = "Site_Name";
            ddlWs2.DataValueField = "Site_ID";
            ddlWs2.DataBind();
            if (Convert.ToInt32(Session["MonitorSite"]) != 0)
            {
                ddlWorksite.Items.FindByValue(Session["MonitorSite"].ToString()).Selected = true;
                ddlWorksite.Enabled = false;
            }
            else
            {
                ddlWs2.Items.Insert(0, new ListItem("---ALL---", "0", true));
                ddlWs2.SelectedItem.Value = "1";
            }
        }
        public void BindEmpList()
        {
            string EmpName = string.Empty;
            int WorkSite = Convert.ToInt32(null);
            int Dept = Convert.ToInt32(null);
            DataSet ds = AttendanceDAC.GetEmployeesByCompID(Convert.ToInt32(Session["CompanyID"]));
            ddlName.DataSource = ds.Tables[0];
            ddlName.DataTextField = "name";
            ddlName.DataValueField = "EmpID";
            ddlName.DataBind();
            ddlName.Items.Insert(0, new ListItem("---SELECT---", "0", true));
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            ShiftsPaging.CurrentPage = 1;
            BindPager();
            rbChoose.Visible = false;
            trNote.Visible = false;
            gvEmp.Columns[0].Visible = false;
            ddlDept.Enabled = ddlName.Enabled = ddlShift.Enabled  = true;
            trgv.Visible = true;
            tblMove.Visible = false;
            if (gvEmp.Rows.Count == 0)
            {
                btnMove.Enabled = false;
            }
            else
            {
                btnMove.Enabled = true;
                lblWarn.Text = "";
            }
        }
        protected void gvEmp_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.Header)
                {
                    CheckBox chkMail = (CheckBox)e.Row.FindControl("chkSelectAll");
                    chkMail.Attributes.Add("onclick", String.Format("javascript:SelectAll(this,'{0}','chkToTransfer');", gvEmp.ClientID));
                }
            }
            catch (Exception Ex)
            {
                //report error
            }
        }
        protected void btnMove_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow gvr in gvEmp.Rows)
            {
                CheckBox chk = (CheckBox)gvr.Cells[0].FindControl("chkToTransfer");
                if (chk.Checked == true)
                {
                    Label lblEmpID = (Label)gvr.Cells[2].FindControl("lblEmpId");
                    int EmpID = Convert.ToInt32(lblEmpID.Text);
                    int Shift = Convert.ToInt32(ddlMoveShift.Text);
                    if (Shift != 0)
                    {
                        try
                        {
                            AttendanceDAC.HR_UpdateShift(Shift, EmpID);
                        }
                        catch (Exception Shifts)
                        {
                            AlertMsg.MsgBox(Page, Shifts.Message.ToString(),AlertMsg.MessageType.Error);
                            return;
                        }
                        AlertMsg.MsgBox(Page, "Done.!");
                    }
                    else
                        AlertMsg.MsgBox(Page, "Please select shift.!");
                }
            }
            BindPager();
        }
        protected void btnJumble_Click(object sender, EventArgs e)
        {
            int? WSID = null;
            if (ddlWs2.SelectedIndex != 0)
                WSID = Convert.ToInt32(ddlWs2.SelectedValue);
            int? DeptID = null;
            if (ddlDepart.SelectedIndex != 0)
                DeptID = Convert.ToInt32(ddlDepart.SelectedValue);
            int? DesigID = null;
            if (ddlDesif2.SelectedIndex != 0)
                DesigID = Convert.ToInt32(ddlDesignation.SelectedValue);
            if (ddlshifts.SelectedIndex == 0)
            {
                AlertMsg.MsgBox(Page, "Please select shift.!");
                tblJumble.Visible = true;
                tblMain.Visible = false;
                return;
            }
            else
            {
                int ShiftID = Convert.ToInt32(ddlshifts.SelectedValue);
                AttendanceDAC.HR_JumbleShifts(DesigID, WSID, DeptID, ShiftID);
                AlertMsg.MsgBox(Page, "Done!");
                Response.Redirect("Shifts.aspx");
            }
        }
        protected void rbChoose_SelectedIndexChanged(object sender, EventArgs e)
        {
            trNote.Visible = false;
            trChoose.Visible = false;
            int type = Convert.ToInt32(rbChoose.SelectedValue);
            if (type == 1)
            {
                tblJumble.Visible = false;
                gvEmp.Columns[0].Visible = true;
                trgv.Visible = true;
                tblMove.Visible = true;
                if (gvEmp.Rows.Count == 0)
                {
                    btnMove.Enabled = false;
                    tblMove.Visible = false;
                }
                else
                {
                    btnMove.Enabled = true;
                    lblWarn.Text = "";
                }
            }
            if (type == 2)
            {
                tblMain.Visible = false;
                tblJumble.Visible = true;
                gvEmp.Columns[0].Visible = false;
                trgv.Visible = false;
                tblMove.Visible = false;
                btnJumble.Visible = true;
                ddlDept.Enabled = ddlName.Enabled = ddlShift.Enabled = false;
            }
        }
        protected void btnShifting_Click(object sender, EventArgs e)
        {
            ShiftsPaging.CurrentPage = 1;
            BindPager();
            gvEmp.Columns[0].Visible = true;
            trgv.Visible = true;        //in this grid is their
            trChoose.Visible = true;
            rbChoose.Visible = true;
            rbChoose.SelectedValue = "1";
            tblMove.Visible = true;
            trNote.Visible = true;
            if (gvEmp.Rows.Count == 0)
            {
                btnMove.Enabled = false;
                tblMove.Visible = false;
            }
            else
            {
                btnMove.Enabled = true;
                tblMove.Visible = true;
            }
        }
        protected void ddlWorksite_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlParameter[] p = new SqlParameter[1];
            p[0] = new SqlParameter("@SiteID",ddlWorksite.SelectedValue);
            FIllObject.FillDropDown(ref ddlName, "GEN_GetEmployeesByWorkSite", p);
            BindDeparmetBySite(Convert.ToInt32(ddlWorksite.SelectedValue));
        }
        public void BindDeparmetBySite(int Site)
        {
            DataSet ds = AttendanceDAC.BindDeparmetBySite(Site, Convert.ToInt32(Session["CompanyID"]));
            ddlDept.DataSource = ds;
            ddlDept.DataTextField = "DeptName";
            ddlDept.DataValueField = "DepartmentUId";
            ddlDept.DataBind();
            ddlDept.Items.Insert(0, new ListItem("---ALL---", "0", true));
        }
    }
}
