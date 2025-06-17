using System;
using System.Data;
using System.Linq;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using AECLOGIC.HMS.BLL;
using SMSConfig;
using AECLOGIC.ERP.COMMON;
using System.Collections.Generic;
using System.Data.SqlClient;
using Aeclogic.Common.DAL;
using AECLOGIC.ERP.HMS;



namespace AECLOGIC.ERP.HMSV1
{
    public partial class EmployeeChangesV1 : AECLOGIC.ERP.COMMON.WebFormMaster
    {
        AttendanceDAC objEmployee = new AttendanceDAC();
        AttendanceDAC objRights = new AttendanceDAC();
        HRCommon objHrCommon = new HRCommon();
        HRCommon objMpReq = new HRCommon();
        HRCommon objMpReqHMS = new HRCommon();
        int mid = 0;
        bool viewall;
        string menuname;
        string menuid;
        static int WSID = 0;
        static int Site = 0;
        static int CompanyID;
        static int siteid, Deptid;
        DataSet ds1 = new DataSet();
        string SMSUserID = System.Configuration.ConfigurationManager.AppSettings["SMSUserID"].ToString();
        string SMSPassword = System.Configuration.ConfigurationManager.AppSettings["SMSPassword"].ToString();
        int cas = 0;
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // This is necessary because Safari and Chrome browsers don't display the Menu control correctly. 
            // All webpages displaying an ASP.NET menu control must inherit this class. 
            if (Request.ServerVariables["http_user_agent"].IndexOf("chrome", StringComparison.CurrentCultureIgnoreCase) != -1)
                Page.ClientTarget = "uplevel";
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Request.QueryString.Count > 0)
                {
                    if (Request.QueryString.AllKeys.Contains("key"))
                    {
                        if (Convert.ToInt32(Request.QueryString["key"].ToString()) == 1)
                        {
                            dvTransEmp.Visible = true;
                            dvTransferedEmp.Visible = false;
                            dvHMS.Visible = false;
                            dvOMS.Visible = false;
                            lnkTransitEmp.Visible = false;
                            lnkConfirmEmp.Visible = false;
                        }
                        else if (Convert.ToInt32(Request.QueryString["key"].ToString()) == 2)
                        {
                            dvTransEmp.Visible = false;
                            dvTransferedEmp.Visible = true;
                            dvHMS.Visible = false;
                            dvOMS.Visible = false;
                            lnkTransitEmp.Visible = true;
                            lnkConfirmEmp.Visible = false;
                        }
                        else if (Convert.ToInt32(Request.QueryString["key"].ToString()) == 3)
                        {
                            dvTransEmp.Visible = false;
                            dvTransferedEmp.Visible = true;
                            dvHMS.Visible = false;
                            dvOMS.Visible = false;
                            lnkTransitEmp.Visible = false;
                            lnkConfirmEmp.Visible = true;
                        }
                        else if (Convert.ToInt32(Request.QueryString["key"].ToString()) == 4)
                        {
                            dvTransEmp.Visible = false;
                            dvTransferedEmp.Visible = false;
                            dvHMS.Visible = true;
                            dvOMS.Visible = false;
                            lnkTransitEmp.Visible = false;
                            lnkConfirmEmp.Visible = false;
                        }
                        else if (Convert.ToInt32(Request.QueryString["key"].ToString()) == 5)
                        {
                            dvTransEmp.Visible = false;
                            dvTransferedEmp.Visible = false;
                            dvHMS.Visible = false;
                            dvOMS.Visible = true;
                            lnkTransitEmp.Visible = false;
                            lnkConfirmEmp.Visible = false;
                        }
                    }
                }
                Ajax.Utility.RegisterTypeForAjax(typeof(AjaxDAL));
                CompanyID = Convert.ToInt32(Session["CompanyID"]);
                if (!IsPostBack)
                {
                    GetParentMenuId();
                    bindddl();
                    bindOMSDLL();
                    ViewState["KeyAction"] = "";
                    EmployeeChangesPaging.Visible = false;
                    BindWorkSites();
                    BindDepartments();
                    LoadDataWSDeptReportingTo();
                    SqlParameter[] param = new SqlParameter[1];
                    if (CompanyID != null)
                        param[0] = new SqlParameter("@CompanyID", CompanyID);
                    else
                        param[0] = new SqlParameter("@CompanyID", SqlDbType.Int);
                    FIllObject.FillDropDown(ref ddlTransitWS, "PM_GetWorkSites", param);
                    BindManPowerPager();
                    BindManPowerPagerHMS();
                    lbnPending_Click(sender, e);
                    if (ModuleID == 1)
                    {
                        lbnApprove_Click(sender, e);
                    }
                    if (ModuleID == 9)
                    {
                        Accordion1.Visible = false;
                        MyAccordion.Visible = false;
                    }
                    else
                    {
                        lbnPending.Visible = false;
                        lbnReject.Visible = false;
                    }
                };
            }
            catch (Exception ex)
            {
                clsErrorLog.HMSEventLog(ex, "EmployeeChanges", "Page_Load", "001");
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
                btnUpdateAll.Enabled = (bool)ds.Tables[0].Rows[0]["Editable"];
                ViewState["Editable"] = (bool)ds.Tables[0].Rows[0]["Editable"];
            }
            return MenuId;
        }
        public static ArrayList EmpDts = new ArrayList();
        protected override void OnInit(EventArgs e)
        {
            if (Request.QueryString.Count > 0)
            {
                ModuleID = int.Parse(Request.QueryString["moduleid"].ToString());
            }
            else
            {
                ModuleID = 1;
            }
            base.OnInit(e);
            // btnSave.Attributes.Add("onclick", "javascript:return ValidateSave('" + txtCategoryName.ClientID + "');");
            EmployeeChangesPaging.FirstClick += new Paging.PageFirst(EmployeeChangesPaging_FirstClick);
            EmployeeChangesPaging.PreviousClick += new Paging.PagePrevious(EmployeeChangesPaging_FirstClick);
            EmployeeChangesPaging.NextClick += new Paging.PageNext(EmployeeChangesPaging_FirstClick);
            EmployeeChangesPaging.LastClick += new Paging.PageLast(EmployeeChangesPaging_FirstClick);
            EmployeeChangesPaging.ChangeClick += new Paging.PageChange(EmployeeChangesPaging_FirstClick);
            EmployeeChangesPaging.ShowRowsClick += new Paging.ShowRowsChange(EmployeeChangesPaging_ShowRowsClick);
            EmployeeChangesPaging.CurrentPage = 1;
            MPReq.FirstClick += new Paging.PageFirst(MPReq_FirstClick);
            MPReq.PreviousClick += new Paging.PagePrevious(MPReq_FirstClick);
            MPReq.NextClick += new Paging.PageNext(MPReq_FirstClick);
            MPReq.LastClick += new Paging.PageLast(MPReq_FirstClick);
            MPReq.ChangeClick += new Paging.PageChange(MPReq_FirstClick);
            MPReq.ShowRowsClick += new Paging.ShowRowsChange(MPReq_ShowRowsClick);
            MPReq.CurrentPage = 1;
            gvMpHMSPageing.FirstClick += new Paging.PageFirst(gvMpHMSPageing_FirstClick);
            gvMpHMSPageing.PreviousClick += new Paging.PagePrevious(gvMpHMSPageing_FirstClick);
            gvMpHMSPageing.NextClick += new Paging.PageNext(gvMpHMSPageing_FirstClick);
            gvMpHMSPageing.LastClick += new Paging.PageLast(gvMpHMSPageing_FirstClick);
            gvMpHMSPageing.ChangeClick += new Paging.PageChange(gvMpHMSPageing_FirstClick);
            gvMpHMSPageing.ShowRowsClick += new Paging.ShowRowsChange(gvMpHMSPageing_ShowRowsClick);
            gvMpHMSPageing.CurrentPage = 1;
        }
        void gvMpHMSPageing_ShowRowsClick(object sender, EventArgs e)
        {
            gvMpHMSPageing.CurrentPage = 1;
            BindManPowerPagerHMS();
        }
        void gvMpHMSPageing_FirstClick(object sender, EventArgs e)
        {
            BindManPowerPagerHMS();
        }
        void BindManPowerPagerHMS()
        {
            objMpReqHMS.PageSize = gvMpHMSPageing.ShowRows;
            objMpReqHMS.CurrentPage = gvMpHMSPageing.CurrentPage;
            HMSManPowerRequistion(objMpReqHMS, 1);
        }
        void MPReq_ShowRowsClick(object sender, EventArgs e)
        {
            MPReq.CurrentPage = 1;
            BindManPowerPager();
        }
        void MPReq_FirstClick(object sender, EventArgs e)
        {
            BindManPowerPager();
        }
        void BindManPowerPager()
        {
            objMpReq.PageSize = MPReq.ShowRows;
            objMpReq.CurrentPage = MPReq.CurrentPage;
            if (ViewState["KeyAction"].ToString() == "")
                OMSManPowerRequistion(objMpReq, -1);
            else
                OMSManPowerRequistion(objMpReq, Convert.ToInt32(ViewState["KeyAction"].ToString()));
        }
        void EmployeeChangesPaging_ShowRowsClick(object sender, EventArgs e)
        {
            EmployeeChangesPaging.CurrentPage = 1;
            BindPager();
        }
        void EmployeeChangesPaging_FirstClick(object sender, EventArgs e)
        {
            if (hdnSearchChange.Value == "1")
                EmployeeChangesPaging.CurrentPage = 1;
            BindPager();
            hdnSearchChange.Value = "0";
        }
        void BindPager()
        {
            objHrCommon.PageSize = EmployeeChangesPaging.ShowRows;
            objHrCommon.CurrentPage = EmployeeChangesPaging.CurrentPage;
            EmployeBind(objHrCommon);
        }
        void EmployeBind(HRCommon objHrCommon)
        {
            try
            {
                objHrCommon.PageSize = EmployeeChangesPaging.ShowRows;
                objHrCommon.CurrentPage = EmployeeChangesPaging.CurrentPage;
                dvgrd.Visible = true;
                int SiteID = Convert.ToInt32(ddlworksites.SelectedItem.Value);
                int DeptID = 0;
                int specid = 0;
                if (ddlSpecialization.SelectedItem != null)
                    specid = Convert.ToInt32(ddlSpecialization.SelectedItem.Value);
                if (txtdepartment.Text != "")
                {
                    DeptID = Convert.ToInt32(ddldepartments_hid.Value == "" ? "0" : ddldepartments_hid.Value);
                }
                int EmpID = 0;
                if (txtEmpID.Text != "")
                    EmpID = int.Parse(txtEmpID.Text);
                string EmpName = txtEmpname.Text;
                DataSet ds = AttendanceDAC.GetEmpDetailsByPaging(objHrCommon, DeptID, SiteID, EmpID, EmpName, Convert.ToInt32(Session["CompanyID"]), specid);
                if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0)
                {
                    gveditkbipl.DataSource = ds;
                    EmployeeChangesPaging.Bind(objHrCommon.CurrentPage, objHrCommon.TotalPages, objHrCommon.NoofRecords, objHrCommon.PageSize);
                    EmployeeChangesPaging.Visible = true;
                }
                else
                {
                    gveditkbipl.EmptyDataText = "No Records Found";
                    EmployeeChangesPaging.Visible = false;
                }
                gveditkbipl.DataBind();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        void OMSManPowerRequistion(HRCommon objMpReq, int Key)
        {
            try
            {
                objMpReq.PageSize = MPReq.ShowRows;
                objMpReq.CurrentPage = MPReq.CurrentPage;
                DateTime Frmdt = CodeUtilHMS.ConvertToDate_ddMMMyyy(txtOMSFromDate.Text.Trim(), CodeUtilHMS.DateFormat.ddMMMyyyy);
                DateTime Todt = CodeUtilHMS.ConvertToDate_ddMMMyyy(txtOMSToDate.Text.Trim(), CodeUtilHMS.DateFormat.ddMMMyyyy);
                DataSet ds = AttendanceDAC.OMS_GET_MANPOWER_REQUISITION(objMpReq, Convert.ToInt32(Session["CompanyID"]), Key
                    , Convert.ToInt32(ddlOMSProject.SelectedValue), Convert.ToInt32(ddlOMSDesignation.SelectedValue),
                    Convert.ToInt32(ddlOMSSpecialisation.SelectedValue), Frmdt, Todt);
                if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0)
                {
                    gvMPReq.DataSource = ds;
                    MPReq.Bind(objMpReq.CurrentPage, objMpReq.TotalPages, objMpReq.NoofRecords, objMpReq.PageSize);
                    MPReq.Visible = true;
                }
                else
                {
                    gvMPReq.DataSource = null;
                    gvMPReq.EmptyDataText = "No Records Found";
                    MPReq.Visible = false;
                }
                gvMPReq.DataBind();
            }
            catch (Exception)
            {
                throw;
            }
        }
        void HMSManPowerRequistion(HRCommon objMpReq, int Key)
        {
            try
            {
                objMpReq.PageSize = gvMpHMSPageing.ShowRows;
                objMpReq.CurrentPage = gvMpHMSPageing.CurrentPage;
                DateTime Frmdt = CodeUtilHMS.ConvertToDate_ddMMMyyy(txtFrom.Text.Trim(), CodeUtilHMS.DateFormat.ddMMMyyyy);
                DateTime Todt = CodeUtilHMS.ConvertToDate_ddMMMyyy(txtTo.Text.Trim(), CodeUtilHMS.DateFormat.ddMMMyyyy);
                DataSet ds = AttendanceDAC.HMS_GET_MANPOWER_REQUISITION(objMpReqHMS, Convert.ToInt32(Session["CompanyID"]), Key,
                    Convert.ToInt32(ddlWorksiteID.SelectedValue), Convert.ToInt32(ddlProjectID.SelectedValue), Convert.ToInt32(ddlDesignation.SelectedValue),
                    Convert.ToInt32(ddlSpecialisation.SelectedValue), Frmdt, Todt);
                if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0)
                {
                    gvMpHMS.DataSource = ds;
                    gvMpHMSPageing.Bind(objMpReq.CurrentPage, objMpReq.TotalPages, objMpReq.NoofRecords, objMpReq.PageSize);
                    gvMpHMSPageing.Visible = true;
                }
                else
                {
                    gvMpHMS.EmptyDataText = "No Records Found";
                    gvMpHMSPageing.Visible = false;
                }
                gvMpHMS.DataBind();
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected void btnOMSMpSearch_Click(object sender, EventArgs e)
        {
            int Keynew = Convert.ToInt32(ViewState["KeyAction"]);
            OMSManPowerRequistion(objMpReq, Keynew);
        }
        protected void btnHMSMpSearch_Click(object sender, EventArgs e)
        {
            HMSManPowerRequistion(objMpReqHMS, 1);
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindPager();
            DropDownList grdddldepartments = (DropDownList)gveditkbipl.FindControl("grdddldepartments");
            if (grdddldepartments != null)
            {
                grdddldepartments.Items.Clear();
                grdddldepartments.Items.Insert(0, new ListItem("---ALL---", "0", true));
            }
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetEmpDetail(string prefixText, int count, string contextKey)
        {
            DataSet ds = AttendanceDAC.HR_GetEmp(prefixText.Trim());
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
        public int GetSiteIndex(string Value)
        {
            return alSites.IndexOf(Value);
        }
        public int GetDeptIndex(string Value)
        {
            return alDepts.IndexOf(Value);
        }
        public int GetDeptHeadIndex(string Value)
        {
            return alHeads.IndexOf(Value);
        }
        public int GetDeptHeadsIndex(string Value)
        {
            return alDeptHeads.IndexOf(Value);
        }
        public DataSet RetSites = new DataSet();
        public DataSet RetDepts = new DataSet();
        public DataSet RetHeads = new DataSet();
        public DataSet RetDeptHeads = new DataSet();
        public static ArrayList alSites = new ArrayList();
        public static ArrayList alDepts = new ArrayList();
        public static ArrayList alHeads = new ArrayList();
        public static ArrayList alDeptHeads = new ArrayList();
        public DataSet BindSites()
        {
            return (DataSet)ViewState["RetSites"];
        }
        public DataSet BindDepts()
        {
            RetDepts = (DataSet)ViewState["RetDepts"];
            return RetDepts;
        }
        public DataSet BindHeads()
        {
            RetHeads = (DataSet)ViewState["RetHeads"];
            return RetHeads;
        }
        public DataSet BindWorkSites()
        {
            try
            {
                DataSet ds = AttendanceDAC.GetWorkSite_Transfer();
                if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ddlworksites.DataSource = ds.Tables[0];
                    ddlworksites.DataTextField = "Site_Name";
                    ddlworksites.DataValueField = "Site_ID";
                    ddlworksites.DataBind();
                }
                ddlworksites.Items.Insert(0, new ListItem("---Select---", "0"));
                RetSites = ds;
                if (Convert.ToInt32(Session["MonitorSite"]) != 0)
                {
                    ddlworksites.Items.FindByValue(Session["MonitorSite"].ToString()).Selected = true;
                    ddlworksites.Enabled = false;
                }
                DataRow drRole;
                drRole = ds.Tables[0].NewRow();
                drRole[0] = 0;
                drRole[1] = "--Select--";
                ds.Tables[0].Rows.InsertAt(drRole, 0);
                ViewState["RetSites"] = ds;
                alSites = new ArrayList();
                foreach (DataRow dr in ds.Tables[0].Rows)
                    alSites.Add(dr["Site_ID"].ToString());
                return RetSites;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public DataSet BindDepartments()
        {
            try
            {
                DataSet ds = (DataSet)objRights.GetDaprtmentList();
                DataRow drRole;
                drRole = ds.Tables[0].NewRow();
                //drRole[0] = 0;
                drRole[1] = "--Select--";
                ds.Tables[0].Rows.InsertAt(drRole, 0);
                ViewState["RetDepts"] = ds;
                alDepts = new ArrayList();
                foreach (DataRow dr in ds.Tables[0].Rows)
                    alDepts.Add(dr["DepartmentUId"].ToString());
                return RetDepts;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        protected void grdddldepartments_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DropDownList grdddldepartments = (DropDownList)sender;
                GridViewRow rownum = (GridViewRow)grdddldepartments.NamingContainer;
                int deptid = Convert.ToInt32(grdddldepartments.SelectedItem.Value);
                Label lblEmpID = (Label)rownum.FindControl("lblEmpID");
                int EmpID = int.Parse(lblEmpID.Text);
                DropDownList grdddlworksites = (DropDownList)rownum.FindControl("grdddlworksites");
                int siteid = Convert.ToInt32(grdddlworksites.SelectedItem.Value);
                DropDownList grdddlHeads = (DropDownList)rownum.FindControl("grdddlHeads");
                grdddlHeads.AutoPostBack = false;
                DataSet dsdeptHeads = objRights.GetDeptHeadsForTransfer(EmpID, siteid, deptid);
                if (dsdeptHeads.Tables.Count > 0)
                {
                    grdddlHeads.DataSource = dsdeptHeads;
                    ViewState["RetHeads"] = objRights.GetDeptHeadsForTransfer(EmpID, siteid, deptid);
                    grdddlHeads.DataSource = ViewState["RetHeads"];
                    grdddlHeads.DataTextField = "name";
                    grdddlHeads.DataValueField = "HeadId";
                    grdddlHeads.DataBind();
                    grdddlHeads.Items.Insert(0, new ListItem("---Select---", "0"));
                }
                else
                {
                    try
                    {
                        ViewState["RetHeads"] = dsdeptHeads;
                        grdddlHeads.DataSource = dsdeptHeads;
                        grdddlHeads.DataTextField = "name";
                        grdddlHeads.DataValueField = "HeadId";
                        grdddlHeads.DataBind();
                    }
                    catch { }
                    AlertMsg.MsgBox(Page, "Selected Person is already Department Head. To Enable Transfer VACATE this person from Department Head Position from Masters>>Departments>>Heads.",AlertMsg.MessageType.Warning);
                    btnLnkDeptHead.Visible = true;
                }
            }
            catch (Exception ex)
            {
                clsErrorLog.HMSEventLog(ex, "EmployeeChanges", "grdddldepartments_SelectedIndexChanged", "002");
            }
        }
        protected void grdddlworksites_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DropDownList grdddlworksites = (DropDownList)sender;
                GridViewRow rownum = (GridViewRow)grdddlworksites.NamingContainer;
                Label lblEmpID = (Label)rownum.FindControl("lblEmpID");
                //int EmpID = Convert.ToInt32(lblEmpID);
                int EmpID = int.Parse(lblEmpID.Text);
                int siteid = Convert.ToInt32(grdddlworksites.SelectedItem.Value);
                DropDownList grdddldepartments = (DropDownList)rownum.FindControl("grdddldepartments");
                int? deptid = null;
                if (grdddldepartments != null)
                {
                    if (grdddldepartments.Text != "")
                        deptid = Convert.ToInt32(grdddldepartments.SelectedItem.Value);
                }
                DropDownList grdddlHeads = (DropDownList)rownum.FindControl("grdddlHeads");
                grdddlHeads.AutoPostBack = false;

                

                DataSet dsdeptHeads = objRights.GetDeptHeadsForTransfer(EmpID, siteid, deptid);
                if (dsdeptHeads.Tables.Count > 0)
                {
                    //DataSet ds = (DataSet)objRights.GetDaprtmentList();
                    SqlParameter[] parms1 = new SqlParameter[1];
                    parms1[0] = new SqlParameter("@SiteId", siteid);
                    DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetDepartmentListWSHead", parms1);
                    grdddldepartments.DataSource = ds;
                    grdddldepartments.DataTextField = "Deptname";
                    grdddldepartments.DataValueField = "DepartmentUId";
                    grdddldepartments.DataBind();
                    grdddldepartments.Items.Insert(0, new ListItem("--Select--", "0"));
                    //BindDepartments();
                }
                if (dsdeptHeads.Tables.Count > 0)
                {
                    grdddlHeads.DataSource = dsdeptHeads;
                    ViewState["RetHeads"] = objRights.GetDeptHeadsForTransfer(EmpID, siteid, deptid);
                    grdddlHeads.DataSource = ViewState["RetHeads"];
                    grdddlHeads.DataTextField = "name";
                    grdddlHeads.DataValueField = "HeadId";
                    grdddlHeads.DataBind();
                    grdddlHeads.Items.Insert(0, new ListItem("---Select---", "0"));
                }
                else
                {
                    AlertMsg.MsgBox(Page, "No Head found in the selected Department! If you wish you can Transfer the Employee to become new Department Head only. Else he can not be transferred without a predefined Head");
                    btnLnkDeptHead.Visible = true;
                }
                // BindDeparmetBySite(Convert.ToInt32(grdddlworksites.SelectedValue));
                grdddlHeads.DataTextField = "name";
                grdddlHeads.DataValueField = "HeadId";
                grdddlHeads.DataBind();
                grdddlHeads.Items.Insert(0, new ListItem("---Select---", "0"));
            }
            catch (Exception ex)
            {
                clsErrorLog.HMSEventLog(ex, "EmployeeChanges", "grdddldepartments_SelectedIndexChanged", "003");
            }
        }
        protected void gveditkbipl_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "UPD")
                {
                    objHrCommon.EmpID = Convert.ToInt32(e.CommandArgument);
                    GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).Parent.Parent;
                    DropDownList RegrdddlHeads = (DropDownList)gveditkbipl.Rows[row.RowIndex].FindControl("RegrdddlHeads");
                    Label txtdesignation = (Label)gveditkbipl.Rows[row.RowIndex].FindControl("txtdesignation");
                    DropDownList grdddlworksites = (DropDownList)gveditkbipl.Rows[row.RowIndex].FindControl("grdddlworksites");
                    DropDownList grdddldepartments = (DropDownList)gveditkbipl.Rows[row.RowIndex].FindControl("grdddldepartments");
                    DropDownList grdddlHeads = (DropDownList)gveditkbipl.Rows[row.RowIndex].FindControl("grdddlHeads");
                    string filename = "", ext = string.Empty, path = "";
                    FileUpload fuc = (FileUpload)gveditkbipl.Rows[row.RowIndex].FindControl("fuUploadProof");
                    filename = fuc.PostedFile.FileName;
                    if (filename != "")
                    {
                        ext = filename.Split('.')[filename.Split('.').Length - 1];
                    }
                    else
                    {
                        ext = "";
                    }
                    try
                    {
                        if (grdddlHeads.Visible == true)
                        {
                            if (grdddlHeads.Items.Count > 0)
                            {
                                objHrCommon.Mgnr = Convert.ToInt32(grdddlHeads.SelectedItem.Value);
                                int Head = Convert.ToInt32(grdddlHeads.SelectedItem.Value);
                                objHrCommon.Designation = txtdesignation.Text;
                                objHrCommon.SiteID = Convert.ToInt32(grdddlworksites.SelectedItem.Value);
                                objHrCommon.DeptID = Convert.ToInt32(grdddldepartments.SelectedItem.Value);
                                objHrCommon.UserID = Convert.ToInt32(Convert.ToInt32(Session["UserId"]).ToString());
                                objHrCommon.Ext = ext;
                                if (Convert.ToInt32(grdddldepartments.SelectedItem.Value) <= 0)
                                {
                                    AlertMsg.MsgBox(Page, "Select Department", AlertMsg.MessageType.Warning);
                                    return;
                                }
                                else if (Convert.ToInt32(grdddlworksites.SelectedItem.Value) <= 0)
                                {
                                    AlertMsg.MsgBox(Page, "Select To Worksite", AlertMsg.MessageType.Warning);
                                    return;
                                }
                                else if (Convert.ToInt32(grdddlHeads.SelectedItem.Value) <= 0)
                                {
                                    AlertMsg.MsgBox(Page, "Department Head must be Selected. Make sure to assign Department Head Position from Masters>>Departments>>Heads from any other Worksite.", AlertMsg.MessageType.Warning);
                                    return;
                                }
                                else
                                {
                                    int Sid = objEmployee.UpdSiteDeptChanges(objHrCommon);
                                    int EmpID = Convert.ToInt32(e.CommandArgument);
                                    if (Sid != 0)
                                    {
                                        if (filename != "")
                                        {
                                            path = Server.MapPath("~\\hms\\EmpTransfer\\" + Sid + "." + ext);
                                            fuc.PostedFile.SaveAs(path);
                                        }
                                    }
                                    AlertMsg.MsgBox(Page, "Transferred !");
                                    BindPager();
                                }
                            }
                            else
                            {
                                AlertMsg.MsgBox(Page, "Heads Can't be transferred");
                                return;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        btnLnkDeptHead.Visible = true;
                        //AlertMsg.MsgBox(Page, ex.Message.ToString(), AlertMsg.MessageType.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                clsErrorLog.HMSEventLog(ex, "EmployeeChanges", "gveditkbipl_RowCommand", "004");
            }
        }
        string ReturnVal = "";
        public string DocNavigateUrl(string Ext, string SID)
        {
            if (SID != "" && Ext != "")
            {
                ReturnVal = "../HMS/EmpTransfer/" + Convert.ToInt32(SID) + '.' + Ext;
            }
            return ReturnVal;
        }
        protected void btnUpdateAll_Click(object sender, EventArgs e)
        {
            try
            {
                int count = 0;
                string empid = "";
                foreach (GridViewRow gvr in gveditkbipl.Rows)
                {
                    CheckBox chk = (CheckBox)gvr.Cells[0].FindControl("chkSelect");
                    if (chk.Checked == true)
                    {
                        Label lblEmpId = (Label)gvr.Cells[1].FindControl("lblEmpID");
                        Label txtdesignation = (Label)gvr.Cells[3].FindControl("txtdesignation");
                        DropDownList grdddlworksites = (DropDownList)gvr.Cells[4].FindControl("grdddlworksites");
                        DropDownList grdddldepartments = (DropDownList)gvr.Cells[5].FindControl("grdddldepartments");
                        DropDownList grdddlHeads = (DropDownList)gvr.Cells[6].FindControl("grdddlHeads");
                        objHrCommon.Mgnr = Convert.ToInt32(grdddlHeads.SelectedItem.Value);
                        objHrCommon.EmpID = Convert.ToInt32(lblEmpId.Text);
                        objHrCommon.Designation = txtdesignation.Text;
                        objHrCommon.SiteID = Convert.ToInt32(grdddlworksites.SelectedItem.Value);
                        objHrCommon.DeptID = Convert.ToInt32(grdddldepartments.SelectedItem.Value);
                        objHrCommon.UserID = Convert.ToInt32(Convert.ToInt32(Session["UserId"]).ToString());
                        string filename = "", ext = string.Empty, path = "";
                        FileUpload fuc = (FileUpload)gvr.Cells[7].FindControl("fuUploadProof");
                        filename = fuc.PostedFile.FileName;
                        if (filename != "")
                        {
                            ext = filename.Split('.')[filename.Split('.').Length - 1];
                        }
                        else
                        {
                            ext = "";
                        }
                        //if (objHrCommon.Mgnr == 0)
                        //{
                        //    AlertMsg.MsgBox(Page, "Please Choose Head to Assign!");
                        //    btnLnkDeptHead.Visible = true;
                        //}
                        //else
                        //{
                        count = count + 1;
                        //objEmployee.UpdSiteDeptChanges(objHrCommon);
                        objHrCommon.Ext = ext;
                        if (Convert.ToInt32(grdddldepartments.SelectedItem.Value) <= 0)
                        {
                            empid = empid + lblEmpId.Text + ",";
                            //AlertMsg.MsgBox(Page, "Select Department", AlertMsg.MessageType.Warning);
                        }
                        else if (Convert.ToInt32(grdddlworksites.SelectedItem.Value) <= 0)
                        {
                            empid = empid + lblEmpId.Text + ",";
                            //AlertMsg.MsgBox(Page, "Select To Worksite", AlertMsg.MessageType.Warning);
                        }
                        else if (Convert.ToInt32(grdddlHeads.SelectedItem.Value) <= 0)
                        {
                            empid = empid + lblEmpId.Text + ",";
                            //AlertMsg.MsgBox(Page, "Select Reporting Person", AlertMsg.MessageType.Warning);
                        }
                        else
                        {
                            int Sid = objEmployee.UpdSiteDeptChanges(objHrCommon);
                            int EmpID = Convert.ToInt32(lblEmpId.Text);
                            if (Sid != 0)
                            {
                                if (filename != "")
                                {
                                    path = Server.MapPath("~\\hms\\EmpTransfer\\" + Sid + "." + ext);
                                    fuc.PostedFile.SaveAs(path);
                                }
                            }
                        }
                        // }
                    }
                }
                if (empid != "")
                {
                    AlertMsg.MsgBox(Page, empid + " Employee(s) not transferred!", AlertMsg.MessageType.Warning);
                }
                else
                    AlertMsg.MsgBox(Page, count + " Employee(s) has been transferred!");
                BindPager();
            }
            catch (Exception ex)
            {
                //AlertMsg.MsgBox(Page, "Sorry for the inconvinience. Try again.\nError:" + ex.Message.ToString(), AlertMsg.MessageType.Error);
            }
        }
        protected void gveditkbipl_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                foreach (GridViewRow gvr in gveditkbipl.Rows)
                {
                    LinkButton lnkUpd = (LinkButton)gvr.Cells[5].FindControl("lnkchange");
                    lnkUpd.Enabled = Convert.ToBoolean(ViewState["Editable"]);
                }
                try
                {
                    if (e.Row.RowType == DataControlRowType.DataRow)
                    {
                        string strScript = "SelectDeSelectHeader(" + ((CheckBox)e.Row.Cells[0].FindControl("chkSelect")).ClientID + ");";
                        ((CheckBox)e.Row.Cells[0].FindControl("chkSelect")).Attributes.Add("onclick", strScript);
                        Label lblEmpId = (Label)e.Row.Cells[1].FindControl("lblEmpID");
                        DropDownList grdddlworksites = (DropDownList)e.Row.Cells[4].FindControl("grdddlworksites");
                        DropDownList grdddldepartments = (DropDownList)e.Row.Cells[5].FindControl("grdddldepartments");
                        DropDownList grdddlHeads = (DropDownList)e.Row.Cells[6].FindControl("grdddlHeads");
                        int EmpID = int.Parse(lblEmpId.Text);
                        int deptid = Convert.ToInt32(grdddldepartments.SelectedItem.Value);
                        int siteid = Convert.ToInt32(grdddlworksites.SelectedItem.Value);
                        RetDeptHeads = objRights.GetDeptHeadsForTransfer(EmpID, siteid, deptid);
                        grdddlHeads.DataSource = RetDeptHeads;
                        grdddlHeads.DataBind();
                        DataSet DSHEAD = objRights.GetDeptHead(siteid, deptid, EmpID);
                        grdddlHeads.Items.Insert(0, new ListItem("---Select---", "0"));
                        if (DSHEAD.Tables[0].Rows.Count > 0)
                        {
                            if (DSHEAD.Tables[0].Rows[0]["HeadID"].ToString() != "0")
                            {
                                grdddlHeads.SelectedValue = DSHEAD.Tables[0].Rows[0]["HeadID"].ToString();
                            }
                            else
                                if (DSHEAD.Tables[0].Rows[0]["ReptHead"].ToString() != "0")
                                {
                                    grdddlHeads.SelectedValue = DSHEAD.Tables[0].Rows[0]["ReptHead"].ToString();
                                }
                                else
                                {
                                    grdddlHeads.SelectedValue = DSHEAD.Tables[0].Rows[0]["MangerID"].ToString();
                                }
                        }
                        else
                        {
                            grdddlHeads.SelectedValue = "0";
                        }
                        int Mgnr = Convert.ToInt32(grdddlHeads.SelectedItem.Value);
                        int Head = Convert.ToInt32(grdddlHeads.SelectedItem.Value);
                        int UserID = Convert.ToInt32(Convert.ToInt32(Session["UserId"]).ToString());
                        LinkButton lnk = (LinkButton)e.Row.FindControl("lnkchange");
                        lnk.Attributes.Add("onclick", "javascript:return HeadsAssign( this,'" + grdddlworksites.ClientID + "','" + grdddldepartments.ClientID + "','" + grdddlHeads.ClientID + "','" + UserID + "','" + EmpID + "','" + Mgnr + "');");
                    }
                }
                catch
                {
                    //AlertMsg.MsgBox(Page, ""); 
                }
            }
            catch (Exception ex)
            {
                clsErrorLog.HMSEventLog(ex, "EmployeeChanges", "gveditkbipl_RowDataBound", "005");
            }
        }
        protected void btnLnkDeptHead_Click(object sender, EventArgs e)
        {
            Response.Redirect("AddManHead.aspx");
        }
        #region SearchEmployee
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionList(string prefixText)// ,int count, string contextKey)
        {
            return ConvertStingArray(AttendanceDAC.GetEmployees_By_WS_Dept(prefixText, siteid, Deptid));
        }
        public static string[] ConvertStingArray(DataSet ds)
        {
            String[] rtval = Array.ConvertAll(ds.Tables[0].Select(), new Converter<DataRow, string>(DataRowToDouble));
            return rtval;
        }
        public static string DataRowToDouble(DataRow dr)
        {
            return dr["Name"].ToString();
        }
        #endregion SearchEmployee
        protected void ddlworksites_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                siteid = Convert.ToInt32(ddlworksites.SelectedValue);
                BindDeparmetBySite(Convert.ToInt32(ddlworksites.SelectedValue));
                EmployeeChangesPaging.CurrentPage = 1;
            }
            catch (Exception ex)
            {
                clsErrorLog.HMSEventLog(ex, "EmployeeChanges", "ddlworksites_SelectedIndexChanged", "006");
            }
        }
        public void BindDeparmetBySite(int Site)
        {
            DataSet ds = AttendanceDAC.BindDeparmetBySite(Site, Convert.ToInt32(Session["CompanyID"]));
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionWorksiteList(string prefixText, int count, string contextKey)
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
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionSpecializationList(string prefixText, int count, string contextKey)
        {
            SqlParameter[] par = new SqlParameter[1];
            par[0] = new SqlParameter("@Search", prefixText.Trim());
            DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetSpecialization_By_googlesearch_EmpList", par);
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
        protected void GetSpecialization(object sender, EventArgs e)
        {
            SqlParameter[] par = new SqlParameter[1];
            par[0] = new SqlParameter("@Search", txtSpecialization.Text);
            FIllObject.FillDropDown(ref ddlSpecialization, "HR_GetSpecialization_By_googlesearch_EmpList", par);
            ListItem itmSelected = ddlSpecialization.Items.FindByText(txtSpecialization.Text);
            if (itmSelected != null)
            {
                ddlSpecialization.SelectedItem.Selected = false;
                itmSelected.Selected = true;
            }
        }
        protected void GetWork(object sender, EventArgs e)
        {
            SqlParameter[] par = new SqlParameter[2];
            par[0] = new SqlParameter("@Search", txtSearchWorksite.Text);
            par[1] = new SqlParameter("@WSID", WSID);
            FIllObject.FillDropDown(ref ddlworksites, "HR_GetWorkSite_By_googlesearch_EmpList", par);
            ListItem itmSelected = ddlworksites.Items.FindByText(txtSearchWorksite.Text);
            if (itmSelected != null)
            {
                ddlworksites.SelectedItem.Selected = false;
                itmSelected.Selected = true;
            }
            CompanyID = Convert.ToInt32(Session["CompanyID"]);
            ddlworksites_SelectedIndexChanged(sender, e);
            WSID = Convert.ToInt32(ddlworksites.SelectedValue);
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
        void LoadDataWSDeptReportingTo()
        {
            try
            {
                DataSet ds = (DataSet)objRights.GetDaprtmentList();
                ddlFillDepartment.DataSource = ds;
                ddlFillDepartment.DataTextField = "Deptname";
                ddlFillDepartment.DataValueField = "DepartmentUId";
                ddlFillDepartment.DataBind();
            }
            catch { }
            try
            {
                DataSet ds = AttendanceDAC.GetWorkSite_Transfer();
                if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ddlFillWS.DataSource = ds.Tables[0];
                    ddlFillWS.DataTextField = "Site_Name";
                    ddlFillWS.DataValueField = "Site_ID";
                    ddlFillWS.DataBind();
                }
            }
            catch { }
            LoadReportingTo();
        }
        void LoadReportingTo()
        {
            try
            {
                DataSet dsdeptHeads = objRights.GetDeptHeadsForTransfer(null,
                Convert.ToInt32(ddlFillWS.SelectedValue), Convert.ToInt32(ddlFillDepartment.SelectedValue));
                if (dsdeptHeads.Tables.Count > 0)
                {
                    ddlFillReportingTo.DataSource = dsdeptHeads;
                    ddlFillReportingTo.DataTextField = "name";
                    ddlFillReportingTo.DataValueField = "HeadId";
                    ddlFillReportingTo.DataBind();
                }
            }
            catch { }
        }
        protected void btnFill_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (GridViewRow gvr in gveditkbipl.Rows)
                {
                    try
                    {
                        CheckBox chk = (CheckBox)gvr.Cells[0].FindControl("chkSelect");
                        if (chk.Checked == true)
                        {
                            DropDownList grdddlworksites = (DropDownList)gvr.FindControl("grdddlworksites");
                            grdddlworksites.SelectedValue = ddlFillWS.SelectedValue;
                            DropDownList grdddldepartments = (DropDownList)gvr.FindControl("grdddldepartments");
                            DataSet ds = (DataSet)objRights.GetDaprtmentList();
                            grdddldepartments.DataSource = ds;
                            grdddldepartments.DataTextField = "Deptname";
                            grdddldepartments.DataValueField = "DepartmentUId";
                            grdddldepartments.DataBind();
                            grdddldepartments.SelectedValue = ddlFillDepartment.SelectedValue;
                            DropDownList grdddlHeads = (DropDownList)gvr.FindControl("grdddlHeads");
                            ds = objRights.GetDeptHeadsForTransfer(null, Convert.ToInt32(grdddlworksites.SelectedValue), Convert.ToInt32(grdddldepartments.SelectedValue));
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                grdddlHeads.DataSource = ds;
                                grdddlHeads.DataTextField = "name";
                                grdddlHeads.DataValueField = "HeadId";
                                grdddlHeads.DataBind();
                                grdddlHeads.SelectedValue = ddlFillReportingTo.SelectedValue;
                            }
                            grdddlHeads.Items.Insert(0, new ListItem("---Select---", "0"));
                        }
                    }
                    catch { }
                }
            }
            catch (Exception ex)
            {
                clsErrorLog.HMSEventLog(ex, "EmployeeChanges", "ddlworksites_SelectedIndexChanged", "007");
            }
        }
        protected void ddlFillWS_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadReportingTo();
        }
        protected void ddlFillDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadReportingTo();
        }
        protected void lnkTransitEmp_Click(object sender, EventArgs e)
        {
            BindGvTransit();
        }
        public void BindGvTransit()
        {
            lblTransist.Visible = true;
            lblTransist.Text = "Transit Employees";
            GVArrived.Visible = false;
            GvTransit.Visible = true;
            //if (ddlTransitWS.SelectedIndex == 0)
            //{
            //    AlertMsg.MsgBox(Page, "Please Select WorkSite", AlertMsg.MessageType.Warning);
            //    return;
            //}
            //else
            //{
            string EmpID = "0";
            if (txtemp.Text != "")
            {
                string[] words = txtemp.Text.Split('-');
                EmpID = words[0];
            }
            SqlParameter[] par = new SqlParameter[3];
            par[0] = new SqlParameter("@wsid", Convert.ToInt32(ddlTransitWS.SelectedValue));
            par[1] = new SqlParameter("@EmpID", Convert.ToInt32(EmpID));
            par[2] = new SqlParameter("@case", cas);
            DataSet ds = SQLDBUtil.ExecuteDataset("HMS_GetTransitEmps", par);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                GvTransit.DataSource = ds;
            }
            else
                GvTransit.DataSource = null;
            GvTransit.DataBind();
            //}
        }
        protected void lnkConfirmEmp_Click(object sender, EventArgs e)
        {
            BindGvArrived();
        }
        public void BindGvArrived()
        {
            lblTransist.Visible = true;
            lblTransist.Text = "Confirmed Arrivals";
            GvTransit.Visible = false;
            GVArrived.Visible = true;
            //if (ddlTransitWS.SelectedIndex == 0)
            //{
            //    AlertMsg.MsgBox(Page, "Please Select WorkSite", AlertMsg.MessageType.Warning);
            //    return;
            //}
            //else
            //{
            string EmpID = "0";
            if (txtemp.Text != "")
            {
                string[] words = txtemp.Text.Split('-');
                EmpID = words[0];
            }
            SqlParameter[] par = new SqlParameter[2];
            par[0] = new SqlParameter("@wsid", Convert.ToInt32(ddlTransitWS.SelectedValue));
            par[1] = new SqlParameter("@EmpID", Convert.ToInt32(EmpID));
            DataSet ds = SQLDBUtil.ExecuteDataset("HMS_GetArrivedEmps", par);
            if (ds.Tables[0].Rows.Count > 0)
            {
                GVArrived.DataSource = ds;
            }
            else
                GVArrived.DataSource = null;
            GVArrived.DataBind();
            //}
        }
        protected void GvTransit_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int Empid = Convert.ToInt32(e.CommandArgument);
            GridViewRow rownum = (GridViewRow)(((Control)e.CommandSource).NamingContainer);
            Label lblSid = (Label)rownum.FindControl("lblSid");
            SqlParameter[] sqlParams = new SqlParameter[3];
            sqlParams[0] = new SqlParameter("@SiteID", Convert.ToInt32(ddlTransitWS.SelectedValue));
            sqlParams[1] = new SqlParameter("@EmpID", Empid);
            sqlParams[2] = new SqlParameter("@Sid", Convert.ToInt32(lblSid.Text));
            SQLDBUtil.ExecuteNonQuery("HMS_GetConfirmEmps", sqlParams);
            BindGvTransit();
        }
        protected void gvMPReq_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Approve")
            {
                int ModuleIDTemp = int.Parse(Request.QueryString["moduleid"].ToString());
                int ID = -1;
                //if (ModuleIDTemp == 9)
                ID = 0;   // approve from OMS            
                int OMSMPReqID = Convert.ToInt32(e.CommandArgument);
                GridViewRow rownum = (GridViewRow)(((Control)e.CommandSource).NamingContainer);
                Label lblOMSMPReqID = (Label)rownum.FindControl("lblOMSMPReqID");
                int UserID = Convert.ToInt32(Convert.ToInt32(Session["UserId"]).ToString());
                SqlParameter[] sqlParams = new SqlParameter[3];
                sqlParams[0] = new SqlParameter("@OMSMPReqID", Convert.ToInt32(lblOMSMPReqID.Text));
                sqlParams[1] = new SqlParameter("@UserID", UserID);
                sqlParams[2] = new SqlParameter("@ID", ID);
                SQLDBUtil.ExecuteNonQuery("Update_Status_OMS_ManPower_Requisition", sqlParams);
                AlertMsg.MsgBox(Page, "Done");
                lbnPending_Click(sender, e);
                //BindGvTransit();
            }
            if (e.CommandName == "Reject")
            {
                int ModuleIDTemp = int.Parse(Request.QueryString["moduleid"].ToString());
                int ID = 1;
                int OMSMPReqID = Convert.ToInt32(e.CommandArgument);
                GridViewRow rownum = (GridViewRow)(((Control)e.CommandSource).NamingContainer);
                Label lblOMSMPReqID = (Label)rownum.FindControl("lblOMSMPReqID");
                int UserID = Convert.ToInt32(Convert.ToInt32(Session["UserId"]).ToString());
                SqlParameter[] sqlParams = new SqlParameter[3];
                sqlParams[0] = new SqlParameter("@OMSMPReqID", Convert.ToInt32(lblOMSMPReqID.Text));
                sqlParams[1] = new SqlParameter("@UserID", UserID);
                sqlParams[2] = new SqlParameter("@ID", ID);
                SQLDBUtil.ExecuteNonQuery("Update_Status_OMS_ManPower_Requisition", sqlParams);
                AlertMsg.MsgBox(Page, "Done");
                lbnReject_Click(sender, e);
            }
        }
        protected void lbnPending_Click(object sender, EventArgs e)
        {
            //if (ModuleID == 9)
            //{
            ViewState["KeyAction"] = "-1"; // OMS PENDING
            OMSManPowerRequistion(objMpReq, -1);
            //}
            lbnApprove.ForeColor = System.Drawing.Color.Blue;
            lbnReject.ForeColor = System.Drawing.Color.Blue;
            lbnPending.ForeColor = System.Drawing.Color.Red;
        }
        protected void lbnApprove_Click(object sender, EventArgs e)
        {
            MPReq.CurrentPage = 1;
            if (ModuleID == 9)
            {
                ViewState["KeyAction"] = "0"; // OMS APPROVAL
                OMSManPowerRequistion(objMpReq, 0);
            }
            else if (ModuleID == 1)
            {
                ViewState["KeyAction"] = "1";  // HMS APPROVAL
                OMSManPowerRequistion(objMpReq, 1);
            }
            lbnApprove.ForeColor = System.Drawing.Color.Red;
            lbnReject.ForeColor = System.Drawing.Color.Blue;
            lbnPending.ForeColor = System.Drawing.Color.Blue;
        }
        protected void lbnReject_Click(object sender, EventArgs e)
        {
            MPReq.CurrentPage = 1;
            if (ModuleID == 9)
            {
                ViewState["KeyAction"] = "2"; // OMS REJECT
                OMSManPowerRequistion(objMpReq, 2);
            }
            else if (ModuleID == 1)
            {
                ViewState["KeyAction"] = "3"; // HMS REJECT
                OMSManPowerRequistion(objMpReq, 3);
            }
            lbnApprove.ForeColor = System.Drawing.Color.Blue;
            lbnReject.ForeColor = System.Drawing.Color.Red;
            lbnPending.ForeColor = System.Drawing.Color.Blue;
        }
        protected void gvMPReq_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    if (ViewState["KeyAction"].ToString() != "")
                    {
                        if (ViewState["KeyAction"].ToString() == "-1") // OMS REJECT
                        {
                            LinkButton lnkApprove = (LinkButton)e.Row.FindControl("lnkApprove");
                            LinkButton lnkReject = (LinkButton)e.Row.FindControl("lnkReject");
                            lnkReject.Visible = true;
                            lnkApprove.Visible = true;
                        }
                        //else if(ViewState["KeyAction"].ToString() == "1")
                        else
                        {
                            LinkButton lnkApprove = (LinkButton)e.Row.FindControl("lnkApprove");
                            LinkButton lnkReject = (LinkButton)e.Row.FindControl("lnkReject");
                            lnkReject.Visible = false;
                            lnkApprove.Visible = false;
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected void gvMpHMS_RowCommand(object sender, GridViewCommandEventArgs e)
        {
        }
        protected void gvMpHMS_RowDataBound(object sender, GridViewRowEventArgs e)
        {
        }
        private void bindOMSDLL()
        {
            SqlParameter[] parms2 = new SqlParameter[1];
            parms2[0] = new SqlParameter("@fCase", 2);
            FIllObject.FillDropDown(ref ddlOMSProject, "T_OMS_Machinery_Requisition_Unplanned_ddl", parms2);
            //Specialisation
            FIllObject.FillDropDown(ref ddlOMSSpecialisation, "HR_GetCategories");
            //Designation
            FIllObject.FillDropDown(ref ddlOMSDesignation, "HR_GetDesignations");
            txtOMSFromDate.Text = DateTime.Now.ToString(DateDisplayFormat);
            txtOMSToDate.Text = DateTime.Now.ToString(DateDisplayFormat);
            DateTime Fromdt = FirstDayOfMonth(DateTime.Now);
            DateTime Todt = LastDayOfMonth(DateTime.Now);
            txtOMSFromDate.Text = Fromdt.ToString(DateDisplayFormat);
            txtOMSToDate.Text = Todt.ToString(DateDisplayFormat);
        }
        public DateTime FirstDayOfMonth(DateTime dateTime)
        {
            return new DateTime(dateTime.Year, dateTime.Month, 1);
        }
        public DateTime LastDayOfMonth(DateTime dateTime)
        {
            DateTime firstDayOfTheMonth = new DateTime(dateTime.Year, dateTime.Month, 1);
            return firstDayOfTheMonth.AddMonths(1).AddDays(-1);
        }
        private void bindddl()
        {
            SqlParameter[] parms1 = new SqlParameter[1];
            parms1[0] = new SqlParameter("@fCase", 1);
            FIllObject.FillDropDown(ref ddlWorksiteID, "T_OMS_Machinery_Requisition_Unplanned_ddl", parms1);
            SqlParameter[] parms2 = new SqlParameter[1];
            parms2[0] = new SqlParameter("@fCase", 2);
            FIllObject.FillDropDown(ref ddlProjectID, "T_OMS_Machinery_Requisition_Unplanned_ddl", parms2);
            //Specialisation
            FIllObject.FillDropDown(ref ddlSpecialisation, "HR_GetCategories");
            //Designation
            FIllObject.FillDropDown(ref ddlDesignation, "HR_GetDesignations");
            txtFrom.Text = DateTime.Now.ToString(DateDisplayFormat);
            txtTo.Text = DateTime.Now.ToString(DateDisplayFormat);
            DateTime Fromdt = FirstDayOfMonth(DateTime.Now);
            DateTime Todt = LastDayOfMonth(DateTime.Now);
            txtFrom.Text = Fromdt.ToString(DateDisplayFormat);
            txtTo.Text = Todt.ToString(DateDisplayFormat);
        }
        protected void ddlWorksiteID_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillProjects();
        }
        private void FillProjects()
        {
            SqlParameter[] p = new SqlParameter[2];
            p[0] = new SqlParameter("@Worksite", ddlWorksiteID.SelectedValue);
            if (CompanyID != null)
                p[1] = new SqlParameter("@CompanyID", CompanyID);
            else
                p[1] = new SqlParameter("@CompanyID", SqlDbType.Int);
            FIllObject.FillDropDown(ref ddlProjectID, "OMS_GetProjectByWorksite", p);
            try
            {
                if (ddlProjectID.Items.Count > 1)
                    ddlProjectID.SelectedIndex = 1;
                else
                    ddlProjectID.SelectedIndex = 0;
            }
            catch { }
        }
        protected void btnempid_Click(object sender, EventArgs e)
        {
            cas = 1;
            BindGvTransit();
        }
        protected void btnEmpdesc_Click(object sender, EventArgs e)
        {
            cas = 2;
            BindGvTransit();
        }
    }
}