using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using AECLOGIC.HMS.BLL;
using Aeclogic.Common.DAL;
using AECLOGIC.ERP.COMMON;
namespace AECLOGIC.ERP.HMS
{
    public partial class EmpPayRoleConfigToAll : AECLOGIC.ERP.COMMON.WebFormMaster
    {
        DataSet ds = new DataSet();
        AttendanceDAC obj = new AttendanceDAC();
        AjaxDAL Aj = new AjaxDAL();
        HRCommon objHrCommon = new HRCommon();
        int mid = 0;
        int empid;
        bool viewall;
        bool status = false;
        static int SearchCompanyID;
        static int Siteid;
        string menuname;
        string menuid;
        protected override void OnInit(EventArgs e)
        {
            ModuleID = 1;
            base.OnInit(e);
            AddAttpaging.FirstClick += new Paging.PageFirst(AdvancedLeaveAppPaging_FirstClick);
            AddAttpaging.PreviousClick += new Paging.PagePrevious(AdvancedLeaveAppPaging_FirstClick);
            AddAttpaging.NextClick += new Paging.PageNext(AdvancedLeaveAppPaging_FirstClick);
            AddAttpaging.LastClick += new Paging.PageLast(AdvancedLeaveAppPaging_FirstClick);
            AddAttpaging.ChangeClick += new Paging.PageChange(AdvancedLeaveAppPaging_FirstClick);
            AddAttpaging.ShowRowsClick += new Paging.ShowRowsChange(AdvancedLeaveAppPaging_ShowRowsClick);
            AddAttpaging.CurrentPage = 1;
        }
        void AdvancedLeaveAppPaging_ShowRowsClick(object sender, EventArgs e)
        {
            AddAttpaging.CurrentPage = 1;
            int SiteID = Convert.ToInt32(ddlworksites.SelectedItem.Value);
            int DeptID = Convert.ToInt32(ddldepartments.SelectedItem.Value);
            EmployeBind(SiteID, DeptID);
        }
        void AdvancedLeaveAppPaging_FirstClick(object sender, EventArgs e)
        {
            int SiteID = Convert.ToInt32(ddlworksites.SelectedItem.Value);
            int DeptID = Convert.ToInt32(ddldepartments.SelectedItem.Value);
            EmployeBind(SiteID, DeptID);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                SearchCompanyID = Convert.ToInt32(Session["CompanyID"]);
                if (!IsPostBack)
                {
                    GetParentMenuId();
                    BindWorkSites();
                    EmployeBind(1, 0);
                    Bind();
                }
            }
            catch (Exception ex)
            {
                clsErrorLog.HMSEventLog(ex, "EmpPayRoleConfigToAll", "Page_Load", "001");
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
                ViewState["Editable"] = (bool)ds.Tables[0].Rows[0]["Editable"];
                ViewState["ViewAll"] = (bool)ds.Tables[0].Rows[0]["ViewAll"];
                btnAll.Enabled = (bool)ds.Tables[0].Rows[0]["Editable"];
                //GVIndentStatus.Columns[7].Visible = Convert.ToBoolean(ds.Tables[0].Rows[0]["Editable"].ToString());
                //viewall = (bool)ViewState["ViewAll"];
                mid = Convert.ToInt32(ds.Tables[0].Rows[0]["MenuId"].ToString());
            }
            return MenuId;
        }
        public void BindWorkSites()
        {
            try
            {
                FIllObject.FillDropDown(ref ddlworksites, "HR_GetWorkSite_By_EmpList");
            }
            catch (Exception ex)
            {
                clsErrorLog.HMSEventLog(ex, "EmpPayRoleConfigToAll", "BindWorksites", "001");
            }
        }
        public void EmployeBind(int SiteID, int DeptID)
        {
            objHrCommon.CurrentPage = AddAttpaging.CurrentPage;
            objHrCommon.PageSize = AddAttpaging.ShowRows;
            if (Request.QueryString.Count > 0)
            {
                txtID.Text = Request.QueryString["EmpID"].ToString();
            }
            int EMPID = 0;
            if (txtID.Text.Trim() != "")
                try { EMPID = Convert.ToInt32(txtID.Text); }
                catch { }
            gveditkbipl.DataSource = AttendanceDAC.GetEmployeesListBysite(objHrCommon,SiteID, DeptID, EMPID, txtEMPName.Text);
            gveditkbipl.DataBind();
            AddAttpaging.Visible = true;
            AddAttpaging.Bind(objHrCommon.CurrentPage, objHrCommon.TotalPages, objHrCommon.NoofRecords, objHrCommon.PageSize);
        }
        void Bind()
        {
            try
            {
                ListItem listItem = null;
                //cblWages
                using (DataSet ds = PayRollMgr.GetEmpWages(0, 0))
                {
                    cblWages.Items.Clear();
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        listItem = new ListItem(dr["Name"].ToString(), dr["WagesId"].ToString());
                        cblWages.Items.Add(listItem);
                    }
                }
                //cblAllowences
                using (DataSet ds = PayRollMgr.GetEmpAllowancesList(0, 0))
                {
                    cblAllowences.Items.Clear();
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        listItem = new ListItem(dr["Name"].ToString(), dr["AllowId"].ToString());
                        cblAllowences.Items.Add(listItem);
                    }
                }
                //nonctc components
                using (DataSet ds = PayRollMgr.GetEmpNonCTCList(0, 0))
                {
                    chknonctc.Items.Clear();
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        listItem = new ListItem(dr["Name"].ToString(), dr["CompID"].ToString());
                        chknonctc.Items.Add(listItem);
                    }
                }
                //cblContributions
                using (DataSet ds = PayRollMgr.GetEmpCoyContributionItemsList(0, 0))
                {
                    cblContributions.Items.Clear();
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        listItem = new ListItem(dr["Name"].ToString(), dr["ItemId"].ToString());
                        cblContributions.Items.Add(listItem);
                    }
                }
                //cblDeductions
                using (DataSet ds = PayRollMgr.GetEmpDeductStatutoryList(0, 0))
                {
                    cblDeductions.Items.Clear();
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        listItem = new ListItem(dr["Name"].ToString(), dr["ItemId"].ToString());
                        cblDeductions.Items.Add(listItem);
                    }
                }
            }
            catch (Exception ex)
            {
                clsErrorLog.HMSEventLog(ex, "EmpPayRoleConfigToAll", "Bind", "002");
            }
        }
        protected void Worksidechangewithdep(object sender, EventArgs e)
        {
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@Search", txtSearchWorksite.Text);
            param[1] = new SqlParameter("@CompanyID", Convert.ToInt32(Session["CompanyID"]));
            // FIllObject.FillDropDown(ref ddlWorksite, "G_GET_WorkSitebyFilter", param);
            FIllObject.FillDropDown(ref ddlworksites, "G_GET_WorkSitebyFilter", param);
            ListItem itmSelected = ddlworksites.Items.FindByText(txtSearchWorksite.Text);
            if (itmSelected != null)
            {
                ddlworksites.SelectedItem.Selected = false;
                itmSelected.Selected = true;
                //FillProjects();
            }
            ddlworksites_SelectedIndexChanged(sender, e);
        }
        protected void GetDepartment(object sender, EventArgs e)
        {
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@Search", txtSearchdept.Text);
            param[1] = new SqlParameter("@CompanyID", Convert.ToInt32(Session["CompanyID"]));
            param[2] = new SqlParameter("@SiteID", ddldepartments.SelectedItem.Value);
            FIllObject.FillDropDown(ref ddldepartments, "HMS_googlesearch_GetDepartmentBySite", param);
            ListItem itmSelected = ddldepartments.Items.FindByText(txtSearchdept.Text);
            if (itmSelected != null)
            {
                ddldepartments.SelectedItem.Selected = false;
                itmSelected.Selected = true;
            }
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                int SiteID = Convert.ToInt32(ddlworksites.SelectedItem.Value);
                int DeptID = Convert.ToInt32(ddldepartments.SelectedItem.Value);
                EmployeBind(SiteID, DeptID);
            }
            catch (Exception ex)
            {
                clsErrorLog.HMSEventLog(ex, "btnSearch_click", "Page_Load", "003");
            }
        }
        protected void btnAll_Click(object sender, EventArgs e)
        {
            try
            {
                #region Wages
                //wages
                foreach (ListItem lstItm in cblWages.Items)
                {
                    string wageid = lstItm.Value;
                    bool Access = false;
                    if (lstItm.Selected == true)
                    {
                        Access = true;
                    }
                    foreach (GridViewRow gvr in gveditkbipl.Rows)
                    {
                        CheckBox chkSelect = (CheckBox)gvr.Cells[0].FindControl("chkSelect");
                        if (chkSelect.Checked == true)
                        {
                            Label lblEmpID = (Label)gvr.Cells[1].FindControl("lblEmpID");
                            Aj.UpdateWages(lblEmpID.Text, wageid, Access);
                        }
                    }
                }
                #endregion Wages
                #region Allowances
                //Allowances
                foreach (ListItem lstItm in cblAllowences.Items)
                {
                    string allowid = lstItm.Value;
                    bool Access = false;
                    if (lstItm.Selected == true)
                    {
                        Access = true;
                    }
                    foreach (GridViewRow gvr in gveditkbipl.Rows)
                    {
                        CheckBox chkSelect = (CheckBox)gvr.Cells[0].FindControl("chkSelect");
                        if (chkSelect.Checked == true)
                        {
                            Label lblEmpID = (Label)gvr.Cells[1].FindControl("lblEmpID");
                            Aj.UpdateAllowances(lblEmpID.Text, allowid, Access);
                        }
                    }
                }
                #endregion Allowances
                //Non CTC 
                foreach (ListItem lstItm in chknonctc.Items)
                {
                    string allowid = lstItm.Value;
                    bool Access = false;
                    if (lstItm.Selected == true)
                    {
                        Access = true;
                    }
                    foreach (GridViewRow gvr in gveditkbipl.Rows)
                    {
                        CheckBox chkSelect = (CheckBox)gvr.Cells[0].FindControl("chkSelect");
                        if (chkSelect.Checked == true)
                        {
                            Label lblEmpID = (Label)gvr.Cells[1].FindControl("lblEmpID");
                            Aj.UpdateNonCTC(lblEmpID.Text, allowid, Access);
                        }
                    }
                }
                #region EmpContribution
                //EmpContribution
                foreach (ListItem lstItm in cblContributions.Items)
                {
                    string Contributionid = lstItm.Value;
                    bool Access = false;
                    if (lstItm.Selected == true)
                    {
                        Access = true;
                    }
                    foreach (GridViewRow gvr in gveditkbipl.Rows)
                    {
                        CheckBox chkSelect = (CheckBox)gvr.Cells[0].FindControl("chkSelect");
                        if (chkSelect.Checked == true)
                        {
                            Label lblEmpID = (Label)gvr.Cells[1].FindControl("lblEmpID");
                            Aj.UpdateEmpContribution(lblEmpID.Text, Contributionid, Access);
                        }
                    }
                }
                #endregion EmpContribution
                #region EmpDeductions
                //EmpDeductions
                foreach (ListItem lstItm in cblDeductions.Items)
                {
                    string Deductionid = lstItm.Value;
                    bool Access = false;
                    if (lstItm.Selected == true)
                    {
                        Access = true;
                    }
                    foreach (GridViewRow gvr in gveditkbipl.Rows)
                    {
                        CheckBox chkSelect = (CheckBox)gvr.Cells[0].FindControl("chkSelect");
                        if (chkSelect.Checked == true)
                        {
                            Label lblEmpID = (Label)gvr.Cells[1].FindControl("lblEmpID");
                            Aj.UpdateEmpDeductions(lblEmpID.Text, Deductionid, Access);
                        }
                    }
                }
                #endregion EmpDeductions
                AlertMsg.MsgBox(Page, "Updated.!",AlertMsg.MessageType.Success);
            }
            catch (Exception ex)
            {
                clsErrorLog.HMSEventLog(ex, "EmpPayRoleConfigToAll", "btnAll_Click", "004");
            }
        }
        //Added by Rijwan:22-03-2016
        protected void ddlworksites_SelectedIndexChanged(object sender, EventArgs e)
        {
            Siteid = Convert.ToInt32(ddlworksites.SelectedValue);
            BindDeparmetBySite(Convert.ToInt32(ddlworksites.SelectedValue));
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute]
        public static string[] GetCompletionList(string prefixText, int count, string contextKey)
        {
            DataSet ds = AttendanceDAC.GetGoogleABCSearchWorkSite(prefixText, SearchCompanyID);
            return ConvertStingArray(ds);
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute]
        public static string[] GetCompletionListdept(string prefixText, int count, string contextKey)
        {
            DataSet ds = AttendanceDAC.GetDepartmentGoogleSerc(prefixText, SearchCompanyID, Siteid);
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
        public void BindDeparmetBySite(int Site)
        {
            DataSet ds = AttendanceDAC.BindDeparmetBySite(Site, Convert.ToInt32(Session["CompanyID"]));
            ddldepartments.DataSource = ds;
            ddldepartments.DataTextField = "DeptName";
            ddldepartments.DataValueField = "DepartmentUId";
            ddldepartments.DataBind();
            ddldepartments.Items.Insert(0, new ListItem("---ALL---", "0", true));
        }
        protected void chkSelect_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (!status)
                {
                    GridViewRow row = ((GridViewRow)((CheckBox)sender).NamingContainer);
                    int index = row.RowIndex;
                    Label emp = (Label)gveditkbipl.Rows[index].FindControl("lblEmpID");
                    if (emp.Text == string.Empty || emp.Text == "")
                    {
                    }
                    else
                    {
                        SqlParameter[] parm = new SqlParameter[1];
                        parm[0] = new SqlParameter("@EmpId", emp.Text);
                        DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetEMP_NetSal", parm);
                        if (ds != null && ds.Tables.Count > 0)
                        {
                            int WagRow = 0;
                            foreach (ListItem lstItm in cblWages.Items)
                            {
                                string wageid = lstItm.Value;
                                if (wageid == ds.Tables[0].Rows[WagRow]["WagesID"].ToString() && Convert.ToBoolean(ds.Tables[0].Rows[WagRow]["IsActive"]) == true)
                                {
                                    lstItm.Selected = true;
                                }
                                else
                                {
                                    lstItm.Selected = false;
                                }
                                WagRow = WagRow + 1;
                            }
                            int AllRow = 0;
                            foreach (ListItem lstItm in cblAllowences.Items)
                            {
                                string wageid = lstItm.Value;
                                if (wageid == ds.Tables[1].Rows[AllRow]["AllowId"].ToString() && Convert.ToBoolean(ds.Tables[1].Rows[AllRow]["IsActive"]) == true)
                                {
                                    lstItm.Selected = true;
                                }
                                else
                                {
                                    lstItm.Selected = false;
                                }
                                AllRow = AllRow + 1;
                            }
                            int nonctcrow = 0;
                            foreach (ListItem lstItm in chknonctc.Items)
                            {
                                string wageid = lstItm.Value;
                                if (wageid == ds.Tables[4].Rows[nonctcrow]["CTCCompID"].ToString() && Convert.ToBoolean(ds.Tables[4].Rows[nonctcrow]["LTAType"]) == true)
                                {
                                    lstItm.Selected = true;
                                }
                                else
                                {
                                    lstItm.Selected = false;
                                }
                                nonctcrow = nonctcrow + 1;
                            }
                            int ContrRow = 0;
                            foreach (ListItem lstItm in cblContributions.Items)
                            {
                                string wageid = lstItm.Value;
                                if (wageid == ds.Tables[2].Rows[ContrRow]["Itemid"].ToString() && Convert.ToBoolean(ds.Tables[2].Rows[ContrRow]["IsActive"]) == true)
                                {
                                    lstItm.Selected = true;
                                }
                                else
                                {
                                    lstItm.Selected = false;
                                }
                                ContrRow = ContrRow + 1;
                            }
                            int DedRow = 0;
                            foreach (ListItem lstItm in cblDeductions.Items)
                            {
                                string wageid = lstItm.Value;
                                if (wageid == ds.Tables[3].Rows[DedRow]["Itemid"].ToString() && Convert.ToBoolean(ds.Tables[3].Rows[DedRow]["IsActive"]) == true)
                                {
                                    lstItm.Selected = true;
                                }
                                else
                                {
                                    lstItm.Selected = false;
                                }
                                DedRow = DedRow + 1;
                            }
                        }
                    }
                }
                else
                { }
            }
            catch (Exception ex)
            {
                clsErrorLog.HMSEventLog(ex, "EmpPayRoleConfigToAll", "chkSelect_CheckedChanged", "005");
            }
        }
        protected void chkSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                status = true;
                foreach (ListItem lstItm in cblWages.Items)
                {
                    lstItm.Selected = false;
                }
                foreach (ListItem lstItm in cblAllowences.Items)
                {
                    lstItm.Selected = false;
                }
                foreach (ListItem lstItm in chknonctc.Items)
                {
                    lstItm.Selected = false;
                }
                foreach (ListItem lstItm in cblContributions.Items)
                {
                    lstItm.Selected = false;
                }
                foreach (ListItem lstItm in cblDeductions.Items)
                {
                    lstItm.Selected = false;
                }
            }
            catch (Exception ex)
            {
                clsErrorLog.HMSEventLog(ex, "EmpPayRoleConfigToAll", "chkSelectAll_CheckedChanged", "006");
            }
        }
    }
}