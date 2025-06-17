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
using AECLOGIC.ERP.HMS.HRClasses;
namespace AECLOGIC.ERP.HMS
{
    public partial class EmpReimbursement : AECLOGIC.ERP.COMMON.WebFormMaster
    {
        int Id = 1;
        int mid = 0;
        bool viewall;
        string menuname;
        string menuid;
        String MyString;
        string extension;
        bool Editable;
        static int WSID = 0;
        static char WSStatus = '1';
        static int Deptid = 0;
        static int siteid = 0;
        static int CompanyID = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["CompanyID"].ToString());
        HRCommon objHrCommon = new HRCommon();
        protected override void OnInit(EventArgs e)
        {
            ModuleID = 1;
            base.OnInit(e);
            #region Approvedpaging
            EmpReimbursementAprovedPaging.FirstClick += new Paging.PageFirst(EmpReimbursementAprovedPaging_FirstClick);
            EmpReimbursementAprovedPaging.PreviousClick += new Paging.PagePrevious(EmpReimbursementAprovedPaging_FirstClick);
            EmpReimbursementAprovedPaging.NextClick += new Paging.PageNext(EmpReimbursementAprovedPaging_FirstClick);
            EmpReimbursementAprovedPaging.LastClick += new Paging.PageLast(EmpReimbursementAprovedPaging_FirstClick);
            EmpReimbursementAprovedPaging.ChangeClick += new Paging.PageChange(EmpReimbursementAprovedPaging_FirstClick);
            EmpReimbursementAprovedPaging.ShowRowsClick += new Paging.ShowRowsChange(EmpReimbursementAprovedPaging_ShowRowsClick);
            EmpReimbursementAprovedPaging.CurrentPage = 1;
            #endregion Approvedpaging
            #region PendingOrRejected
            EmpReimbursementPendingRejPaging.FirstClick += new Paging.PageFirst(EmpReimbursementPendingRejPaging_FirstClick);
            EmpReimbursementPendingRejPaging.PreviousClick += new Paging.PagePrevious(EmpReimbursementPendingRejPaging_FirstClick);
            EmpReimbursementPendingRejPaging.NextClick += new Paging.PageNext(EmpReimbursementPendingRejPaging_FirstClick);
            EmpReimbursementPendingRejPaging.LastClick += new Paging.PageLast(EmpReimbursementPendingRejPaging_FirstClick);
            EmpReimbursementPendingRejPaging.ChangeClick += new Paging.PageChange(EmpReimbursementPendingRejPaging_FirstClick);
            EmpReimbursementPendingRejPaging.ShowRowsClick += new Paging.ShowRowsChange(EmpReimbursementPendingRejPaging_ShowRowsClick);
            EmpReimbursementPendingRejPaging.CurrentPage = 1;
            #endregion PendingOrRejected
            #region TransferdAmount
            EmpReimbursementTransferdPaging.FirstClick += new Paging.PageFirst(EmpReimbursementTransferdPaging_FirstClick);
            EmpReimbursementTransferdPaging.PreviousClick += new Paging.PagePrevious(EmpReimbursementTransferdPaging_FirstClick);
            EmpReimbursementTransferdPaging.NextClick += new Paging.PageNext(EmpReimbursementTransferdPaging_FirstClick);
            EmpReimbursementTransferdPaging.LastClick += new Paging.PageLast(EmpReimbursementTransferdPaging_FirstClick);
            EmpReimbursementTransferdPaging.ChangeClick += new Paging.PageChange(EmpReimbursementTransferdPaging_FirstClick);
            EmpReimbursementTransferdPaging.ShowRowsClick += new Paging.ShowRowsChange(EmpReimbursementTransferdPaging_ShowRowsClick);
            EmpReimbursementTransferdPaging.CurrentPage = 1;
            #endregion TransferdAmount
        }
        #region Approved
        void EmpReimbursementAprovedPaging_ShowRowsClick(object sender, EventArgs e)
        {
            EmpReimbursementAprovedPaging.CurrentPage = 1;
            BindPager();
        }
        void EmpReimbursementAprovedPaging_FirstClick(object sender, EventArgs e)
        {
            if (hdnSearchChangeAproved.Value == "1")
                EmpReimbursementAprovedPaging.CurrentPage = 1;
            BindPager();
            hdnSearchChangeAproved.Value = "0";
        }
        void BindPager()
        {
            objHrCommon.PageSize = EmpReimbursementAprovedPaging.CurrentPage;
            objHrCommon.CurrentPage = EmpReimbursementAprovedPaging.ShowRows;
            EmployeeApproved(objHrCommon);
        }
        #endregion Approved
        #region PendingRej
        void EmpReimbursementPendingRejPaging_ShowRowsClick(object sender, EventArgs e)
        {
            EmpReimbursementPendingRejPaging.CurrentPage = 1;
            if (Convert.ToInt32(Request.QueryString["key"]) == 2)
                BindPagerPending();
            if (Convert.ToInt32(Request.QueryString["key"]) == 3)
            {
                BindPagerPending();
            }
            if (Convert.ToInt32(Request.QueryString["key"]) == 4)       //A/C Posted
            {
                BindPagerPending();
            }
        }
        void EmpReimbursementPendingRejPaging_FirstClick(object sender, EventArgs e)
        {
            if (hdnSearchChangePendingRej.Value == "1")
                EmpReimbursementPendingRejPaging.CurrentPage = 1;
            if (Convert.ToInt32(Request.QueryString["key"]) == 2)
                BindPagerPending();
            if (Convert.ToInt32(Request.QueryString["key"]) == 3)
            {
                BindPagerPending();                                 //rejected
            }
            if (Convert.ToInt32(Request.QueryString["key"]) == 4)
            {
                BindPagerPending();                                 //A/C Posted
            }
            hdnSearchChangePendingRej.Value = "0";
        }
        void BindPagerPending()
        {
            int EmpID = 0;
            objHrCommon.PageSize = EmpReimbursementPendingRejPaging.CurrentPage;
            objHrCommon.CurrentPage = EmpReimbursementPendingRejPaging.ShowRows;
            if (Convert.ToInt32(Request.QueryString["key"]) == 2)       //Pending
            {
                    EmpID = Convert.ToInt32(emp_hid.Value == "" ? "0" : emp_hid.Value);
                    EmployeeNotApproved(objHrCommon, EmpID,1);
            }
            if (Convert.ToInt32(Request.QueryString["key"]) == 5)       //Recommend
            {
                    EmpID = Convert.ToInt32(emp_hid.Value == "" ? "0" : emp_hid.Value);
                    EmployeeNotApproved(objHrCommon, EmpID,5);
            }
            if (Convert.ToInt32(Request.QueryString["key"]) == 6)       //Final Approval
            {
                EmpID = Convert.ToInt32(emp_hid.Value == "" ? "0" : emp_hid.Value);
                EmployeeNotApproved(objHrCommon, EmpID, 6);
            }
            if (Convert.ToInt32(Request.QueryString["key"]) == 3)       //rejected
            {
                    EmpID = Convert.ToInt32(emp_hid.Value == "" ? "0" : emp_hid.Value);
                    EmployeeNotApproved(objHrCommon, EmpID,3);
            }
            if (Convert.ToInt32(Request.QueryString["key"]) == 4)       //A/C Posted
            {
                    EmpID = Convert.ToInt32(emp_hid.Value == "" ? "0" : emp_hid.Value);
                    EmployeeNotApproved(objHrCommon, EmpID,4);
            }
            if (Convert.ToInt32(Request.QueryString["key"]) == 6)      //Final Approval
            {
                EmpID = Convert.ToInt32(emp_hid.Value == "" ? "0" : emp_hid.Value);
                EmployeeNotApproved(objHrCommon, EmpID, 6);
            }
        }
        #endregion PendingRej
        #region Transferd
        void EmpReimbursementTransferdPaging_ShowRowsClick(object sender, EventArgs e)
        {
            EmpReimbursementTransferdPaging.CurrentPage = 1;
            BindPagerTransAmt();
        }
        void EmpReimbursementTransferdPaging_FirstClick(object sender, EventArgs e)
        {
            if (hdnSearchChangeTransAmt.Value == "1")
                EmpReimbursementTransferdPaging.CurrentPage = 1;
            BindPagerTransAmt();
            hdnSearchChangeTransAmt.Value = "0";
        }
        void BindPagerTransAmt()
        {
            objHrCommon.PageSize = EmpReimbursementTransferdPaging.CurrentPage;
            objHrCommon.CurrentPage = EmpReimbursementTransferdPaging.ShowRows;
            //if (ddlFilterEmp.SelectedIndex == -1)
            EmployeeReimTransferdAmt(objHrCommon);
        }
        #endregion Transferd
        protected void Page_Load(object sender, EventArgs e)
        {
            Label1.Text = String.Empty;
            Ajax.Utility.RegisterTypeForAjax(typeof(AjaxDAL));
            DataTable dtReimburseList = new DataTable();
            if (!IsPostBack)
            {
                Page.Form.Attributes.Add("enctype", "multipart/form-data");
                GetParentMenuId();
                btnSubmit.Visible = false;
                DataSet dsAU = AttendanceDAC.sh_GetAuReimbursment();
                DataRow dr = dsAU.Tables[0].NewRow();
                dr["Au_Id"] = 0;
                dr["Au_Name"] = "---Select---";
                dsAU.Tables[0].Rows.InsertAt(dr, 0);
                dsAU.AcceptChanges();
                ArrayList alUnitIndexes = new ArrayList();
                foreach (DataRow row in dsAU.Tables[0].Rows)
                {
                    alUnitIndexes.Add(row["Au_Id"].ToString().Trim());
                }
                ViewState["alUnitIndexes"] = alUnitIndexes;
                ViewState["dsAU"] = dsAU;
                ViewState["PK"] = 0;
                ViewState["EmpID"] = 0;
                ViewState["ERID"] = 0;
                WSID = 0;
                if (Request.QueryString.Count > 0)
                {
                    int id = Convert.ToInt32(Request.QueryString["key"].ToString());
                    if (id == 1)//Addnew
                    {
                        tblAdd.Visible = true;
                        tblView.Visible = false;
                        BindEmpList();
                        //BindYears();
                        //  GetWorkSites();
                        // GetDepartments();
                        BindItem();
                        dtReimburseList.Columns.Add(new DataColumn("ID", typeof(System.Int32)));
                        dtReimburseList.Columns.Add(new DataColumn("RItemID", typeof(System.Int32)));
                        dtReimburseList.Columns.Add(new DataColumn("RItem", typeof(System.String)));
                        dtReimburseList.Columns.Add(new DataColumn("EmpID", typeof(System.String)));
                        dtReimburseList.Columns.Add(new DataColumn("AUID", typeof(System.String)));
                        dtReimburseList.Columns.Add(new DataColumn("Purpose", typeof(System.String)));
                        dtReimburseList.Columns.Add(new DataColumn("Qty", typeof(System.Double)));
                        dtReimburseList.Columns.Add(new DataColumn("UnitRate", typeof(System.Double)));
                        dtReimburseList.Columns.Add(new DataColumn("Amount", typeof(System.Double)));
                        dtReimburseList.Columns.Add(new DataColumn("DateOfSpent", typeof(System.String)));
                        dtReimburseList.Columns.Add(new DataColumn("Proof", typeof(System.String)));
                        ViewState["ReimItems"] = dtReimburseList;
                        dtReimburseList = (DataTable)ViewState["ReimItems"];
                        gvRemiItems.DataSource = dtReimburseList;
                        gvRemiItems.DataBind();
                    }
                    if (id == 5)//Recmnd
                    {
                     DataSet   ds = AttendanceDAC.HR_EmpRecmnd();
                        BindPagerPending();
                        gvView.Columns[0].Visible = true;
                        gvView.Columns[1].Visible = false;
                        gvView.Visible = true;
                        tblView.Visible = true;
                        SqlParameter[] p = new SqlParameter[1];
                        p[0] = new SqlParameter("@Status", 0);
                    }
                    if (id == 6)//Recmnd
                    {
                        //DataSet ds = AttendanceDAC.HR_EmpRecmnd();
                        BindPagerPending();
                        gvView.Columns[0].Visible = true;
                        gvView.Columns[1].Visible = false;
                        gvView.Visible = true;
                        tblView.Visible = true;
                        SqlParameter[] p = new SqlParameter[1];
                        p[0] = new SqlParameter("@Status", 0);
                    }
                    if (id == 2)//Pending
                    {
                        DataSet ds = AttendanceDAC.HR_EmpReimNotApproved();
                        BindPagerPending();
                        gvView.Columns[0].Visible = true;
                        gvView.Columns[1].Visible = false;
                        gvView.Visible = true;
                        tblView.Visible = true;
                        SqlParameter[] p = new SqlParameter[1];
                        p[0] = new SqlParameter("@Status", 1);
                    }
                    if (id == 4) //transfered
                    {
                        //BindPagerPending();
                        EmpReimbursementPendingRejPaging.Visible = false;
                        DataSet ds = AttendanceDAC.HR_ReimburseTransferd();
                        BindPagerTransAmt();
                        tblTransfered.Visible = true;
                        tblView.Visible = true;
                        gvView.Visible = false;
                        ds = AttendanceDAC.GetEmployeesByCompID(Convert.ToInt32(Session["CompanyID"]));
                    }
                    if (id == 3)//rejected
                    {
                        DataSet ds = AttendanceDAC.HR_EmpReimRejected();
                        BindPagerPending();     //Rej
                        gvView.Columns[0].Visible = false;
                        gvView.Columns[1].Visible = true;
                        gvView.Visible = true;
                        tblView.Visible = true;
                        SqlParameter[] p = new SqlParameter[1];
                        p[0] = new SqlParameter("@Status", 3);
                    }
                }
                else
                {//approved
                    bool IsAllowed = Convert.ToBoolean(ViewState["ViewAll"]);
                    if (IsAllowed == true || IsAllowed == false) //nookesh added for time being to check for roleid IsAllowed == false 
                    {
                        tblView.Visible = false;
                        tblAdd.Visible = false;
                        BindPager();
                        gvViewApproved.Visible = true;
                        tblViewApproved.Visible = true;
                        SqlParameter[] p = new SqlParameter[1];
                        p[0] = new SqlParameter("@Status", 2);
                        FIllObject.FillDropDown(ref ddlTransfer, "GetEmployeesByTravel_Exp", p);
                    }
                    else
                    {
                        Response.Redirect("EmpReimbursement.aspx?key=1");
                    }
                }
                try
                {
                    ViewState["WSID"] = 0;
                    if (Convert.ToInt32(Session["RoleId"].ToString()) == 7)
                    {
                        try
                        {
                            DataSet ds = clViewCPRoles.HR_DailyAttStatus( Convert.ToInt32(Session["UserId"]));
                            ViewState["WSID"] = ds.Tables[0].Rows[0]["ID"].ToString();
                            TxtWorksite.Text = ds.Tables[0].Rows[0]["Name"].ToString();
                            TxtWorksite.ReadOnly = true;
                        }
                        catch { }
                    }
                }
                catch { }
            }
        }
        #region Employeestatus
        //HR_ReimburseTransferdByEmpIDByPaging
        void EmployeeReimTransferdAmtByEmpID(HRCommon objHrCommon, int EmpID)
        {
            try
            {
                objHrCommon.PageSize = EmpReimbursementTransferdPaging.ShowRows;
                objHrCommon.CurrentPage = EmpReimbursementTransferdPaging.CurrentPage;
                //int EmpID = Convert.ToInt32(txtFilterEmpID.Text);
                DataSet ds = AttendanceDAC.HR_ReimburseTransferdByEmpIDByPaging(objHrCommon, EmpID, Convert.ToInt32(Session["CompanyID"]));
                if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0)
                {
                    gvTransfered.DataSource = ds;
                    EmpReimbursementTransferdPaging.Bind(objHrCommon.CurrentPage, objHrCommon.TotalPages, objHrCommon.NoofRecords, objHrCommon.PageSize);
                }
                else
                {
                    gvTransfered.EmptyDataText = "No Records Found";
                    EmpReimbursementTransferdPaging.Visible = false;
                }
                gvTransfered.DataBind();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        void EmployeeReimTransferdAmt(HRCommon objHrCommon)
        {
            try
            {
                objHrCommon.PageSize = EmpReimbursementTransferdPaging.ShowRows;
                objHrCommon.CurrentPage = EmpReimbursementTransferdPaging.CurrentPage;
                DataSet ds = AttendanceDAC.HR_ReimburseTransferdByPaging(objHrCommon, Convert.ToInt32(Session["CompanyID"]));
                if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0)
                {
                    gvTransfered.DataSource = ds;
                    EmpReimbursementTransferdPaging.Bind(objHrCommon.CurrentPage, objHrCommon.TotalPages, objHrCommon.NoofRecords, objHrCommon.PageSize);
                }
                else
                {
                    gvTransfered.EmptyDataText = "No Records Found";
                    EmpReimbursementTransferdPaging.Visible = false;
                }
                gvTransfered.DataBind();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        void EmployeeRecmndByEmpID(HRCommon objHrCommon, int EmpID)
        {
            try
            {
                objHrCommon.PageSize = EmpReimbursementPendingRejPaging.ShowRows;
                objHrCommon.CurrentPage = EmpReimbursementPendingRejPaging.CurrentPage;
                DataSet ds = AttendanceDAC.HR_EmpRecmndByEmpIDByPaging(objHrCommon, EmpID, Convert.ToInt32(Session["CompanyID"]));
                if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0)
                {
                    gvView.DataSource = ds;
                    EmpReimbursementPendingRejPaging.Bind(objHrCommon.CurrentPage, objHrCommon.TotalPages, objHrCommon.NoofRecords, objHrCommon.PageSize);
                }
                else
                {
                    gvView.EmptyDataText = "No Records Found";
                    EmpReimbursementPendingRejPaging.Visible = false;
                }
                gvView.DataBind();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        void EmployeeReimRejectedByEmpID(HRCommon objHrCommon, int EmpID)
        {
            try
            {
                objHrCommon.PageSize = EmpReimbursementPendingRejPaging.ShowRows;
                objHrCommon.CurrentPage = EmpReimbursementPendingRejPaging.CurrentPage;
                DataSet ds = AttendanceDAC.HR_EmpReimRejectedByEmpIDByPaging(objHrCommon, EmpID, Convert.ToInt32(Session["CompanyID"]));
                if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0)
                {
                    gvView.DataSource = ds;
                    EmpReimbursementPendingRejPaging.Bind(objHrCommon.CurrentPage, objHrCommon.TotalPages, objHrCommon.NoofRecords, objHrCommon.PageSize);
                }
                else
                {
                    gvView.EmptyDataText = "No Records Found";
                    EmpReimbursementPendingRejPaging.Visible = false;
                }
                gvView.DataBind();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        void EmployeeRecommend(HRCommon objHrCommon)
        {
            try
            {
                objHrCommon.PageSize = EmpReimbursementPendingRejPaging.ShowRows;
                objHrCommon.CurrentPage = EmpReimbursementPendingRejPaging.CurrentPage;
                DataSet ds = AttendanceDAC.HR_EmpRecmndByPaging(objHrCommon, Convert.ToInt32(Session["CompanyID"]));
                if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0)
                {
                    gvView.DataSource = ds;
                    EmpReimbursementPendingRejPaging.Bind(objHrCommon.CurrentPage, objHrCommon.TotalPages, objHrCommon.NoofRecords, objHrCommon.PageSize);
                }
                else
                {
                    gvView.EmptyDataText = "No Records Found";
                    EmpReimbursementPendingRejPaging.Visible = false;
                }
                gvView.DataBind();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        void EmployeeReimRejected(HRCommon objHrCommon)
        {
            try
            {
                objHrCommon.PageSize = EmpReimbursementPendingRejPaging.ShowRows;
                objHrCommon.CurrentPage = EmpReimbursementPendingRejPaging.CurrentPage;
                DataSet ds = AttendanceDAC.HR_EmpReimRejectedByPaging(objHrCommon, Convert.ToInt32(Session["CompanyID"]));
                if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0)
                {
                    gvView.DataSource = ds;
                    EmpReimbursementPendingRejPaging.Bind(objHrCommon.CurrentPage, objHrCommon.TotalPages, objHrCommon.NoofRecords, objHrCommon.PageSize);
                }
                else
                {
                    gvView.EmptyDataText = "No Records Found";
                    EmpReimbursementPendingRejPaging.Visible = false;
                }
                gvView.DataBind();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        void EmployeeNotApproved(HRCommon objHrCommon, int EmpID, int status)
        {
            try
            {
                objHrCommon.PageSize = EmpReimbursementPendingRejPaging.ShowRows;
                objHrCommon.CurrentPage = EmpReimbursementPendingRejPaging.CurrentPage;
                DataSet ds = AttendanceDAC.HR_EmpReimNotApprovedByEmpIDByPaging(objHrCommon, EmpID, Convert.ToInt32(Session["CompanyID"]), status);
                if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0)
                {
                    gvView.DataSource = ds;
                    EmpReimbursementPendingRejPaging.Bind(objHrCommon.CurrentPage, objHrCommon.TotalPages, objHrCommon.NoofRecords, objHrCommon.PageSize);
                }
                else
                {
                    gvView.EmptyDataText = "No Records Found";
                    EmpReimbursementPendingRejPaging.Visible = false;
                }
                gvView.DataBind();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        void EmployeeNotApproved(HRCommon objHrCommon)
        {
            try
            {
                objHrCommon.PageSize = EmpReimbursementPendingRejPaging.ShowRows;
                objHrCommon.CurrentPage = EmpReimbursementPendingRejPaging.CurrentPage;
                DataSet ds = AttendanceDAC.HR_EmpReimNotApprovedByPaging(objHrCommon, Convert.ToInt32(Session["CompanyID"]));
                if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0)
                {
                    gvView.DataSource = ds;
                    EmpReimbursementPendingRejPaging.Bind(objHrCommon.CurrentPage, objHrCommon.TotalPages, objHrCommon.NoofRecords, objHrCommon.PageSize);
                }
                else
                {
                    gvView.EmptyDataText = "No Records Found";
                    EmpReimbursementPendingRejPaging.Visible = false;
                }
                gvView.DataBind();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        void EmployeeApproved(HRCommon objHrCommon)
        {
            try
            {
                objHrCommon.PageSize = EmpReimbursementAprovedPaging.ShowRows;
                objHrCommon.CurrentPage = EmpReimbursementAprovedPaging.CurrentPage;
                DataSet ds = AttendanceDAC.HR_EmpReimEmployeesByPaging(objHrCommon);
                if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0)
                {
                    gvViewApproved.DataSource = ds;
                    EmpReimbursementAprovedPaging.Bind(objHrCommon.CurrentPage, objHrCommon.TotalPages, objHrCommon.NoofRecords, objHrCommon.PageSize);
                }
                else
                {
                    gvViewApproved.EmptyDataText = "No Records Found";
                    EmpReimbursementAprovedPaging.Visible = false;
                }
                gvViewApproved.DataBind();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        #endregion Employeestatus
        public DataSet GetAUDataSet()
        {
            return (DataSet)ViewState["dsAU"];
        }
        public int GetAUIndex(string AUID)
        {
            ArrayList alUnitIndexes = (ArrayList)ViewState["alUnitIndexes"];
            return alUnitIndexes.IndexOf(AUID.Trim());
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
                ViewState["ViewAll"] = (bool)ds.Tables[0].Rows[0]["ViewAll"];
                ViewState["Editable"] = Editable = (bool)ds.Tables[0].Rows[0]["Editable"];
                viewall = (bool)ViewState["ViewAll"];
                menuname = ds.Tables[0].Rows[0]["menuname"].ToString();
                menuid = MenuId.ToString();
                mid = Convert.ToInt32(ds.Tables[0].Rows[0]["MenuId"].ToString());
                btnApprove.Enabled = Convert.ToBoolean(ds.Tables[0].Rows[0]["ViewAll"].ToString());
                gvShow.Columns[14].Visible = Convert.ToBoolean(ds.Tables[0].Rows[0]["Editable"].ToString());
                gvShow.Columns[15].Visible = Convert.ToBoolean(ds.Tables[0].Rows[0]["Editable"].ToString());
                gvShow.Columns[16].Visible = false;
                gvShowRej.Columns[17].Visible = Convert.ToBoolean(ds.Tables[0].Rows[0]["Editable"].ToString());
                btnApprove.Enabled = btnRApprove.Enabled = btnTransferAcc.Enabled = Convert.ToBoolean(ds.Tables[0].Rows[0]["ViewAll"].ToString());
                btnApprove.Visible = btnRApprove.Visible = btnTransferAcc.Visible = Convert.ToBoolean(ds.Tables[0].Rows[0]["ViewAll"].ToString());
                btnSave.Enabled = btnSubmit.Enabled = btnTransferAcc.Enabled = BtnEditSave.Enabled = btnApprove.Enabled = btnRApprove.Enabled = btnRejReaSave.Enabled = Editable;
                btnSave.Visible = btnSubmit.Visible = btnTransferAcc.Visible = BtnEditSave.Visible = btnApprove.Visible = btnRApprove.Visible = btnRejReaSave.Visible = Editable;
            }
            return MenuId;
        }
        public void BindEmpList()
        {
            string EmpName = string.Empty;
            int WorkSite = Convert.ToInt32(ddlWorksite_hid.Value == "" ? "0" : ddlWorksite_hid.Value);
            int Dept = Convert.ToInt32(ddlDepartment_hid.Value == "" ? "0" : ddlWorksite_hid.Value);
            try
            {
                if (Convert.ToInt32(ViewState["WSID"]) > 0)
                    WorkSite = Convert.ToInt32(ViewState["WSID"]);
            }
            catch { }
            DataSet ds = AttendanceDAC.GetEmployees_DLL_By_WS_Dept(WorkSite, Dept);
            ddlEmp.DataSource = ds.Tables[0];
            ddlEmp.DataTextField = "name";
            ddlEmp.DataValueField = "EmpId";
            ddlEmp.DataBind();
            ddlEmp.Items.Insert(0, new ListItem("---SELECT---", "0", true));
        }
        public void BindReimburseEmp()
        {
            DataSet ds = AttendanceDAC.HR_EmpReimEmployees(Convert.ToInt32(Session["CompanyID"]));
            gvView.DataSource = ds;
            gvView.DataBind();
            gvView.Visible = true;
        }
        protected string FormatInput(object EntryType)
        {
            string retValue = "";
            string input = EntryType.ToString();
            if (input == "1")
            {
                retValue = "Pending";
            }
            if (input == "2")
            {
                retValue = "Approved";
               // gvViewApproved.Columns[5].ControlStyle.ForeColor = System.Drawing.Color.Green;
            }
            if (input == "3")
            {
                retValue = "Rejected";
                gvView.Columns[5].ControlStyle.ForeColor = System.Drawing.Color.Red;
            }
            if (input == "4")
            {
                retValue = "Transfered";
            }
            if (input == "5")
            {
                retValue = "Recommended";
            }
            if (input == "6")
            {
                retValue = "Final Approval";
            }
            return retValue;
        }
        public string DocNavigateUrl(string Proof)
       {
            string ReturnVal = "";
            string Value = Proof.Split('.')[Proof.Split('.').Length - 1];
            ReturnVal = "../hms/EmpReimbureseProof/" + Proof;
            if (ReturnVal == "../hms/EmpReimbureseProof/")
            {
                return null;
            }
            else
            {
                return "javascript:return window.open('" + ReturnVal + "', '_blank')";
            }
        }
        public string ViewItemsNavigateUrl(string ERID)
        {
            return "javascript:return window.open('ViewReimbursements.aspx?key=" + ERID + "', '_blank')";
        }
        public void BindItem()
        {
            DataSet ds = PayRollMgr.GetReimbursementItemsList();
            lstItems.DataSource = ds.Tables[0];
            lstItems.DataTextField = "Name";
            lstItems.DataValueField = "RMItemId";
            lstItems.DataBind();
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (ddlEmp.SelectedIndex > 0)
            //  if (Convert.ToInt32(ddlEmp_hid.Value == "" ? "0" : ddlEmp_hid.Value) > 0)
            {
                int[] Secarrary = lstItems.GetSelectedIndices();
                if (Secarrary.Length > 0)
                {
                    AttendanceDAC ADAC = new AttendanceDAC();
                    DataTable dtReimburseList = new DataTable();
                    DataRow dtRow;
                    dtReimburseList = (DataTable)ViewState["ReimItems"];
                  //   
                    ListItem item = null;
                    string EmpID = ddlEmp.SelectedItem.Value;
                    foreach (int indexItem in lstItems.GetSelectedIndices())
                    {
                        item = lstItems.Items[indexItem];
                        dtRow = dtReimburseList.NewRow();
                        if (ViewState["Id"] != null)
                        {
                            Id = Convert.ToInt32(ViewState["Id"]);
                        }
                        dtRow["ID"] = Id;
                        dtRow["RItemID"] = item.Value;
                        dtRow["RItem"] = item.Text;
                        dtRow["EmpID"] = ddlEmp.SelectedItem.Value;
                        dtRow["Purpose"] = "";
                        dtRow["Qty"] = "1";
                        dtRow["DateOfSpent"] = DateTime.Now.ToString("dd MMM yyyy");
                        dtRow["Proof"] = "";
                        dtReimburseList.Rows.Add(dtRow);
                        item.Selected = false;
                        Id = Id + 1;
                        ViewState["Id"] = Id;
                    }
                    foreach (GridViewRow row in gvRemiItems.Rows)
                    {
                        TextBox txtpur = (TextBox)row.FindControl("txtPurpose");
                        TextBox txtqty = (TextBox)row.FindControl("txtQty");
                        Label txtAmount = (Label)row.FindControl("txtAmount");
                        TextBox txtrate = (TextBox)row.FindControl("txtrate");
                        Label lblItemId = (Label)row.FindControl("lblRItem");
                        TextBox txtSpentOn = (TextBox)row.FindControl("txtSpentOn");
                        DropDownList ddlunits = new DropDownList();
                        ddlunits = (DropDownList)row.FindControl("ddlunits");
                        FileUpload UploadProof = (FileUpload)row.FindControl("UploadProof");
                        DataRow[] drSelected = dtReimburseList.Select("RItem='" + lblItemId.Text + "'");
                        drSelected[0]["Purpose"] = txtpur.Text;
                        drSelected[0]["Qty"] = txtqty.Text;//
                        drSelected[0]["EmpID"] = ddlEmp.SelectedItem.Value;
                        if (txtrate.Text == "") { Convert.ToDouble(txtrate.Text = "0"); }
                        drSelected[0]["UnitRate"] = Convert.ToDouble(txtrate.Text);
                        drSelected[0]["AUID"] = ddlunits.SelectedValue;
                        drSelected[0]["DateOfSpent"] = DateTime.Now.ToString("dd MMM yyyy");
                        drSelected[0]["Proof"] = UploadProof.FileName;
                    }
                    dtReimburseList.AcceptChanges();
                    ViewState["ReimItems"] = dtReimburseList;
                    dtReimburseList = (DataTable)ViewState["ReimItems"];
                    gvRemiItems.DataSource = dtReimburseList;
                    gvRemiItems.DataBind();
                    btnSubmit.Visible = true;
                }
                else
                {
                    AlertMsg.MsgBox(Page, "Select Item",AlertMsg.MessageType.Warning);
                    //Label1.Text = "Select Item";
                    //Label1.ForeColor = System.Drawing.Color.Red;
                }
            }
            else
            {
                AlertMsg.MsgBox(Page, "Select Employee", AlertMsg.MessageType.Warning);
                //Label1.Text = "Select Employee";
                //Label1.ForeColor = System.Drawing.Color.Red;
            }
        }
        protected void txtSearch_Click(object sender, EventArgs e)
        {
            if (txtFilter.Text == "")
            {
                // 
                string EmpName = string.Empty;
                //  int WorkSite = Convert.ToInt32(ddlWorksite.SelectedValue);
                int WorkSite = Convert.ToInt32(Convert.ToInt32(ddlWorksite_hid.Value == "" ? "0" : ddlWorksite_hid.Value));
                //int Dept = Convert.ToInt32(ddlDepartment.SelectedValue);
                int Dept = Convert.ToInt32(Convert.ToInt32(ddlDepartment_hid.Value == "" ? "0" : ddlDepartment_hid.Value));
                try
                {
                    if (Convert.ToInt32(ViewState["WSID"]) > 0)
                        WorkSite = Convert.ToInt32(ViewState["WSID"]);
                }
                catch { }
                DataSet ds = AttendanceDAC.HR_SerchEmp_Reimburse(EmpName, Convert.ToInt32(Session["CompanyID"]));
                //ddlEmp.DataSource = ds.Tables[1];
                ddlEmp.DataSource = ds.Tables[0];
                ddlEmp.DataTextField = "name";
                ddlEmp.DataValueField = "EmpID";
                ddlEmp.DataBind();
                ddlEmp.Items.Insert(0, new ListItem("---SELECT---", "0", true));
            }
            else
            {
                string EmpName = txtFilter.Text;
                //int WorkSite = Convert.ToInt32(ddlWorksite.SelectedValue);
                int WorkSite = Convert.ToInt32(Convert.ToInt32(ddlWorksite_hid.Value == "" ? "0" : ddlWorksite_hid.Value));
                // int Dept = Convert.ToInt32(ddlDepartment.SelectedValue);
                int Dept = Convert.ToInt32(Convert.ToInt32(ddlDepartment_hid.Value == "" ? "0" : ddlDepartment_hid.Value));
                try
                {
                    if (Convert.ToInt32(ViewState["WSID"]) > 0)
                        WorkSite = Convert.ToInt32(ViewState["WSID"]);
                }
                catch { }
                DataSet ds = AttendanceDAC.HR_SerchEmp_Reimburse(EmpName, Convert.ToInt32(Session["CompanyID"]));
                ddlEmp.DataSource = ds.Tables[1];
                ddlEmp.DataTextField = "name";
                ddlEmp.DataValueField = "EmpID";
                ddlEmp.DataBind();
                ddlEmp.Items.Insert(0, new ListItem("---SELECT---", "0", true));
            }
        }
        protected void gvRemiItems_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Del")
            {
                int ID = Convert.ToInt32(e.CommandArgument);
                DataTable dtList = (DataTable)ViewState["ReimItems"];
                DataRow[] drSelected = null;
                drSelected = dtList.Select("ID='" + e.CommandArgument + "'");
                if (drSelected.Length > 0)
                    dtList.Rows.Remove(drSelected[0]);
                dtList.AcceptChanges();
                ViewState["ReimItems"] = dtList;
                gvRemiItems.DataSource = dtList;
                gvRemiItems.DataBind();
            }
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            btnApprove.Visible = false;
            DataSet dsRemItems = new DataSet("RemItemDataSet");
            DataTable dt = new DataTable("RemItemTable");
            dt.Columns.Add(new DataColumn("EmpID", typeof(System.Int32)));
            dt.Columns.Add(new DataColumn("RItemID", typeof(System.Int32)));
            dt.Columns.Add(new DataColumn("Uom", typeof(System.String)));
            dt.Columns.Add(new DataColumn("Purpose", typeof(System.String)));
            dt.Columns.Add(new DataColumn("Quantity", typeof(System.Double)));
            dt.Columns.Add(new DataColumn("Rate", typeof(System.Double)));
            dt.Columns.Add(new DataColumn("SpentOn", typeof(System.String)));
            dt.Columns.Add(new DataColumn("DOS", typeof(System.String)));
            dt.Columns.Add(new DataColumn("Proof", typeof(System.String)));
            dt.Columns.Add(new DataColumn("Status", typeof(System.Int32)));
            dsRemItems.Tables.Add(dt);
            foreach (GridViewRow gvRow in gvRemiItems.Rows)
            {
                Label lblEmpID = (Label)gvRow.Cells[2].FindControl("lblEmpID");
                Label lblRItem = (Label)gvRow.Cells[3].FindControl("lblRItemNo");
                DropDownList ddlunits = (DropDownList)gvRow.Cells[4].FindControl("ddlunits");
                int UnitOfMeasure = int.Parse(ddlunits.Text);
                if (UnitOfMeasure == 0)
                {
                    AlertMsg.MsgBox(Page, "Please select units of measure.!", AlertMsg.MessageType.Warning);
                    //Label1.Text = "Please select units of measure.!";
                    //Label1.ForeColor = System.Drawing.Color.Red;
                    return;
                }
                TextBox txtRate = (TextBox)gvRow.Cells[5].FindControl("txtRate");
                double UnitRate;// = int.Parse(txtRate.Text);
                try
                {
                    UnitRate = double.Parse(txtRate.Text);
                }
                catch (Exception)
                {
                    AlertMsg.MsgBox(Page, "Unitrate can takes numbers only.!", AlertMsg.MessageType.Warning);
                    //Label1.Text = "Unitrate can takes numbers only..!";
                    //Label1.ForeColor = System.Drawing.Color.Red;
                    return;
                }
                if (txtRate.Text == "" || UnitRate <= 0)
                {
                    AlertMsg.MsgBox(Page, "Please enter proper unitrate.!", AlertMsg.MessageType.Warning);
                    //Label1.Text = "Please enter proper unitrate.!";
                    //Label1.ForeColor = System.Drawing.Color.Red;
                    return;
                }
                else
                {
                    UnitRate = double.Parse(txtRate.Text);
                }
                TextBox txtQty = (TextBox)gvRow.Cells[6].FindControl("txtQty");
                double Quantity;
                try
                {
                    Quantity = double.Parse(txtQty.Text);
                }
                catch (Exception)
                {
                    AlertMsg.MsgBox(Page, "Quantity can take numbers only.!", AlertMsg.MessageType.Warning);
                    //Label1.Text = "Quantity can takes numbers only..!";
                    //Label1.ForeColor = System.Drawing.Color.Red;
                    return;
                }
                if (txtQty.Text == "" || Quantity <= 0)
                {
                    AlertMsg.MsgBox(Page, "Please enter proper quntity.!", AlertMsg.MessageType.Warning);
                    //Label1.Text = "Please enter proper quntity.!";
                    //Label1.ForeColor = System.Drawing.Color.Red;
                    return;
                }
                else
                {
                    Quantity = double.Parse(txtQty.Text);
                }
                Label txtAmount = (Label)gvRow.Cells[7].FindControl("txtAmount");
                //ViewState["Amount"]
                TextBox txtPurpose = (TextBox)gvRow.Cells[8].FindControl("txtPurpose");
                Label lblRItemID = (Label)gvRow.Cells[9].FindControl("lblRItemNo");
                TextBox txtSpentOn = (TextBox)gvRow.Cells[10].FindControl("txtSpentOn");
                try
                {
                    DateTime DateOfSpent = CodeUtilHMS.ConvertToDate_ddMMMyyy(txtSpentOn.Text.Trim(), CodeUtilHMS.DateFormat.ddMMMyyyy);
                }
                catch (Exception)
                {
                    AlertMsg.MsgBox(Page, "Please select proper date.!", AlertMsg.MessageType.Warning);
                    //Label1.Text = "Please select proper date.!";
                    //Label1.ForeColor = System.Drawing.Color.Red;
                    return;
                }
                string path = "";
                FileUpload UploadProof = (FileUpload)gvRow.Cells[11].FindControl("UploadProof");
                String MyString = string.Empty;
                string extension = string.Empty;
                if (UploadProof.HasFile)
                {
                    DateTime MyDate = DateTime.Now;
                    MyString = MyDate.ToString("ddMMyyhhmmss");
                    extension = System.IO.Path.GetExtension(UploadProof.PostedFile.FileName).ToLower();
                }
                if (UploadProof.HasFile)
                {
                    path = Server.MapPath("~\\HMS\\EmpReimbureseProof\\" + MyString + extension);
                    UploadProof.PostedFile.SaveAs(path);
                }
                string Proof = MyString + extension;
                DataRow dr = dt.NewRow();
                dr["RItemID"] = Convert.ToInt32(lblRItemID.Text);
                dr["EmpID"] = Convert.ToInt32(lblEmpID.Text);
                ViewState["EmpView"] = Convert.ToInt32(lblEmpID.Text);
                dr["Uom"] = Convert.ToInt32(ddlunits.SelectedValue);
                dr["Rate"] = Convert.ToDouble(txtRate.Text);
                dr["Quantity"] = Convert.ToDouble(txtQty.Text);
                dr["Purpose"] = txtPurpose.Text.ToString();
                dr["SpentOn"] = txtSpentOn.Text;
                dr["DOS"] = DateTime.Now.ToString(ConfigurationManager.AppSettings["DateFormat"]);
                dr["Proof"] = Proof;
                dr["Status"] = 1;
                dt.Rows.Add(dr);
            }
            dsRemItems.AcceptChanges();
            try
            {
                DataSet ds = AttendanceDAC.HR_InsUpdRemItems(dsRemItems);
            }
            catch (Exception ex)
            {
                AlertMsg.MsgBox(Page, ex.Message.ToString(), AlertMsg.MessageType.Error);
                //Label1.Text = ex.Message.ToString();
                //Label1.ForeColor = System.Drawing.Color.Red;
                return;
            }
            gvRemiItems.Visible = false;
            tblAdd.Visible = false;
            tblShow.Visible = true;
            tblView.Visible = true;
            gvView.Visible = true;
            gvShow.Visible = true;
            if (Convert.ToBoolean(ViewState["ViewAll"]) == true)
            {
                Response.Redirect("EmpReimbursement.aspx?key=2");
            }
            else { Response.Redirect("EmpReimbursement.aspx?key=1"); }
        }
        protected void txtUnitrete_TextChanged(object sender, EventArgs e)
        {
            CalculateAmt();
        }
        protected void txtQuantity_TextChanged(object sender, EventArgs e)
        {
            CalculateAmt();
        }
        public void CalculateAmt()
        {
            foreach (GridViewRow gvRow in gvRemiItems.Rows)
            {
                TextBox txtRate = (TextBox)gvRow.Cells[5].FindControl("txtRate");
                double UnitRate;// = int.Parse(txtRate.Text);
                try
                {
                    UnitRate = double.Parse(txtRate.Text);
                }
                catch (Exception)
                {
                    AlertMsg.MsgBox(Page, "Unitrate can takes numbers only.!", AlertMsg.MessageType.Warning);
                    //Label1.Text = "Unitrate can takes numbers only.!";
                    //Label1.ForeColor = System.Drawing.Color.Red;
                    return;
                }
                if (txtRate.Text == "" || UnitRate <= 0)
                {
                    AlertMsg.MsgBox(Page, "Please enter proper unitrate.!", AlertMsg.MessageType.Warning);
                    //Label1.Text = "Please enter proper unitrate.!";
                    //Label1.ForeColor = System.Drawing.Color.Red;
                    return;
                }
                else
                {
                    UnitRate = double.Parse(txtRate.Text);
                }
                TextBox txtQty = (TextBox)gvRow.Cells[6].FindControl("txtQty");
                double Quantity;
                try
                {
                    Quantity = double.Parse(txtQty.Text);
                }
                catch (Exception)
                {
                    AlertMsg.MsgBox(Page, "Quantity can take numbers only.!", AlertMsg.MessageType.Warning);
                    //Label1.Text = "Quantity can take numbers only.!";
                    //Label1.ForeColor = System.Drawing.Color.Red;
                    return;
                }
                if (txtQty.Text == "" || Quantity <= 0)
                {
                    AlertMsg.MsgBox(Page, "Please enter proper quntity.!", AlertMsg.MessageType.Warning);
                    //Label1.Text = "Please enter proper quntity.!";
                    //Label1.ForeColor = System.Drawing.Color.Red;
                    return;
                }
                else
                {
                    Quantity = double.Parse(txtQty.Text);
                }
                double Amount = UnitRate * Quantity;
                Label txtAmount = (Label)gvRow.Cells[7].FindControl("txtAmount");
                txtAmount.Text = Convert.ToString(Amount);
                ViewState["Amount"] = Amount;
            }
        }
        protected void gvView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Edt")
            {
                BindEmpList();
                BindItem();
                tblAdd.Visible = true;
                tblView.Visible = false;
                gvView.Visible = false;
                int EmpID = Convert.ToInt32(e.CommandArgument);
                DataSet ds = AttendanceDAC.HR_EmpReimburseAmtPayable(EmpID);
                ddlEmp.SelectedValue = Convert.ToInt32(ds.Tables[0].Rows[0]["EmpID"]).ToString();
                gvRemiItems.DataSource = ds;
                gvRemiItems.DataBind();
                gvRemiItems.Visible = true;
                btnSubmit.Visible = true;
            }
            if (e.CommandName == "Approve")
            {
                int EmpID = Convert.ToInt32(e.CommandArgument);
                DataSet ds = AttendanceDAC.HR_EmpReimEmployeesByEmpID(EmpID);
                double Amount = Convert.ToDouble(ds.Tables[0].Rows[0]["Amount"]);
            }
        }
        protected void gvView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.Header)
                {
                    CheckBox chkMail = (CheckBox)e.Row.FindControl("chkSelectAll");
                    chkMail.Attributes.Add("onclick", String.Format("javascript:SelectAll(this,'{0}','chkApproval');", gvView.ClientID));
                }
            }
            catch (Exception Ex)
            {
                //report error
            }
        }
        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            tblAdd.Visible = false;
            tblView.Visible = true;
        }
        protected void gvShow_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.Header)
                {
                    CheckBox chkMail = (CheckBox)e.Row.FindControl("chkSelectAll");
                    chkMail.Attributes.Add("onclick", String.Format("javascript:return SelectAll(this,'{0}','chkSelectOne');", gvShow.ClientID));
                }
            }
            catch (Exception Ex)
            {
                //report error
            }
        }
        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            tblAdd.Visible = true;
            tblView.Visible = false;
        }
        protected void gvShow_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Edt")
            {
                btnApprove.Visible = false;
                BindEmpList();
                BindItem();
                tblAdd.Visible = false;
                btnSubmit.Visible = false;
                tblView.Visible = false;
                gvView.Visible = false;
                int PK = Convert.ToInt32(e.CommandArgument);
                ViewState["PK"] = PK;
                DataSet ds = AttendanceDAC.HR_EditReimburseItems(PK);
                ddlEmp.SelectedValue = Convert.ToInt32(ds.Tables[0].Rows[0]["EmpID"]).ToString();
                gvEdit.DataSource = ds;
                gvEdit.DataBind();
                gvRemiItems.Visible = false;
                gvShow.Visible = true;
                tblEdit.Visible = true;
                foreach (GridViewRow gvRow in gvShow.Rows)
                {
                    CheckBox chk = new CheckBox();
                    chk = (CheckBox)gvRow.FindControl("chkSelectOne");
                    Label lbl = (Label)gvRow.FindControl("lblPK");
                    int ERIDToChk = Convert.ToInt32(lbl.Text);
                    if (ERIDToChk == PK)
                    {
                        chk.Checked = true;
                    }
                    else { chk.Checked = false; }
                }
            }
            if (e.CommandName == "Del")
            {
                int PK = Convert.ToInt32(e.CommandArgument);
            }
            if (e.CommandName == "Reject")
            {
                btnApprove.Enabled = false;
                int PK = Convert.ToInt32(e.CommandArgument);
                ViewState["PK"] = PK;
                tblRejReason.Visible = true;
                txtReason.Text = "";
                foreach (GridViewRow gvRow in gvShow.Rows)
                {
                    CheckBox chk = new CheckBox();
                    chk = (CheckBox)gvRow.FindControl("chkSelectOne");
                    Label lbl = (Label)gvRow.FindControl("lblPK");
                    int ERIDToChk = Convert.ToInt32(lbl.Text);
                    if (ERIDToChk == PK)
                    {
                        chk.Checked = true;
                    }
                    else { chk.Checked = false; }
                }
            }
        }
        protected void btnApprove_Click(object sender, EventArgs e)
        {
            string URL = Request.Url.Segments[Request.Url.Segments.Length - 1] + Request.Url.Query;
            if (URL == "EmpReimbursement.aspx?key=2")
            {
                foreach (GridViewRow gvRow in gvView.Rows)
                {
                    CheckBox chk = new CheckBox();
                    chk = (CheckBox)gvRow.FindControl("chkApproval");
                    if (chk.Checked)
                    {
                        Label Emp = (Label)gvRow.FindControl("lblEmpID");
                        int EmpID = Convert.ToInt32(Emp.Text);
                        ViewState["EmpID"] = EmpID;
                        foreach (GridViewRow gvRows in gvShow.Rows)
                        {
                            CheckBox chked = new CheckBox();
                            chked = (CheckBox)gvRows.FindControl("chkSelectOne");
                            if (chked.Checked)//&& ERID==ChkERID
                            {
                                Label lblERID = (Label)gvRows.FindControl("lblERID");
                                int ERID = Convert.ToInt32(lblERID.Text);
                                Label lblERItemID = (Label)gvRows.FindControl("lblPK");
                                int ERItemID = Convert.ToInt32(lblERItemID.Text);
                                AttendanceDAC.HR_RecmndStatus(ERItemID);
                                if (ERID != Convert.ToInt32(ViewState["ERID"]))
                                {
                                    AttendanceDAC.HR_UpdateAsRecmnd( Convert.ToInt32(Session["UserId"]), ERID);
                                    ViewState["ERID"] = ERID;
                                }
                            }
                        }
                    }
                }
                tblShow.Visible = false;
                DataSet dsRefresh = AttendanceDAC.HR_EmpReimNotApproved();// AttendanceDAC.HR_EmpReimNotApprovedByEmpID(Convert.ToInt32(ViewState["EmpID"]));
                BindPagerPending();
                gvView.Visible = true;
                gvView.DataSource = dsRefresh;
                gvView.DataBind();
                gvView.Columns[0].Visible = true;
                gvView.Columns[1].Visible = false;
                tblView.Visible = true;
                tblShow.Visible = true;
             DataSet   ds = AttendanceDAC.HR_EmpReimburseAmtPayable(Convert.ToInt32(ViewState["EmpID"]));
                gvShow.DataSource = ds;
                gvShow.DataBind();
                gvShow.Visible = true;
                if (ds.Tables[0].Rows.Count == 0)
                {
                    btnApprove.Visible = false;
                }
            }
            if (URL == "EmpReimbursement.aspx?key=6")
            {
                foreach (GridViewRow gvRow in gvView.Rows)
                {
                    CheckBox chk = new CheckBox();
                    chk = (CheckBox)gvRow.FindControl("chkApproval");
                    if (chk.Checked)
                    {
                        Label Emp = (Label)gvRow.FindControl("lblEmpID");
                        int EmpID = Convert.ToInt32(Emp.Text);
                        ViewState["EmpID"] = EmpID;
                        foreach (GridViewRow gvRows in gvShow.Rows)
                        {
                            CheckBox chked = new CheckBox();
                            chked = (CheckBox)gvRows.FindControl("chkSelectOne");
                            if (chked.Checked)//&& ERID==ChkERID
                            {
                                Label lblERID = (Label)gvRows.FindControl("lblERID");
                                int ERID = Convert.ToInt32(lblERID.Text);
                                Label lblERItemID = (Label)gvRows.FindControl("lblPK");
                                int ERItemID = Convert.ToInt32(lblERItemID.Text);
                                AttendanceDAC.HR_FinalApprvalStatus_Desc(ERItemID);
                                if (ERID != Convert.ToInt32(ViewState["ERID"]))
                                {
                                    AttendanceDAC.HR_UpdateFinalAppr_reimb(Convert.ToInt32(Session["UserId"]), ERID);
                                    ViewState["ERID"] = ERID;
                                }
                            }
                        }
                    }
                }
                tblShow.Visible = false;
              //  DataSet dsRefresh = AttendanceDAC.HR_EmpReimNotApproved();// AttendanceDAC.HR_EmpReimNotApprovedByEmpID(Convert.ToInt32(ViewState["EmpID"]));
                DataSet dsRefresh = SQLDBUtil.ExecuteDataset("HR_EmpReimFinal");
                BindPagerPending();
                gvView.Visible = true;
                gvView.DataSource = dsRefresh;
                gvView.DataBind();
                gvView.Columns[0].Visible = true;
                gvView.Columns[1].Visible = false;
                tblView.Visible = true;
                tblShow.Visible = true;
                DataSet ds = AttendanceDAC.HR_EmpReimburseAmtPayable(Convert.ToInt32(ViewState["EmpID"]));
                gvShow.DataSource = ds;
                gvShow.DataBind();
                gvShow.Visible = true;
                if (ds.Tables[0].Rows.Count == 0)
                {
                    btnApprove.Visible = false;
                }
            }
            if (URL == "EmpReimbursement.aspx?key=5")
            {
                foreach (GridViewRow gvRow in gvView.Rows)
                {
                    CheckBox chk = new CheckBox();
                    chk = (CheckBox)gvRow.FindControl("chkApproval");
                    if (chk.Checked)
                    {
                        Label Emp = (Label)gvRow.FindControl("lblEmpID");
                        int EmpID = Convert.ToInt32(Emp.Text);
                        ViewState["EmpID"] = EmpID;
                        foreach (GridViewRow gvRows in gvShow.Rows)
                        {
                            CheckBox chked = new CheckBox();
                            chked = (CheckBox)gvRows.FindControl("chkSelectOne");
                            if (chked.Checked)//&& ERID==ChkERID
                            {
                                Label lblERID = (Label)gvRows.FindControl("lblERID");
                                int ERID = Convert.ToInt32(lblERID.Text);
                                Label lblERItemID = (Label)gvRows.FindControl("lblPK");
                                int ERItemID = Convert.ToInt32(lblERItemID.Text);
                                AttendanceDAC.HR_ApproveStatus(ERItemID);
                                if (ERID != Convert.ToInt32(ViewState["ERID"]))
                                {
                                    AttendanceDAC.HR_UpdateAsApproved( Convert.ToInt32(Session["UserId"]), ERID);
                                    ViewState["ERID"] = ERID;
                                }
                            }
                        }
                    }
                }
                tblShow.Visible = false;
                DataSet dsRefresh = AttendanceDAC.HR_EmpReimNotApproved();// AttendanceDAC.HR_EmpReimNotApprovedByEmpID(Convert.ToInt32(ViewState["EmpID"]));
                BindPagerPending();
                gvView.Visible = true;
                gvView.Columns[0].Visible = true;
                gvView.Columns[1].Visible = false;
                tblView.Visible = true;
                tblShow.Visible = true;
                DataSet ds = AttendanceDAC.HR_EmpRecmndAmtPayable(Convert.ToInt32(ViewState["EmpID"]));
                gvShow.DataSource = ds;
                gvShow.DataBind();
                gvShow.Visible = true;
                if (ds.Tables[0].Rows.Count == 0)
                {
                    btnApprove.Visible = false;
                }
            }
            if (URL == "EmpReimbursement.aspx?key=3")
            {
                foreach (GridViewRow gvRow in gvView.Rows)
                {
                    CheckBox chk = new CheckBox();
                    chk = (CheckBox)gvRow.FindControl("chkRSelect");
                    if (chk.Checked)
                    {
                        Label Emp = (Label)gvRow.FindControl("lblEmpID");
                        int EmpID = Convert.ToInt32(Emp.Text);
                        ViewState["EmpID"] = EmpID;
                        foreach (GridViewRow gvRows in gvShowRej.Rows)
                        {
                            CheckBox chked = new CheckBox();
                            chked = (CheckBox)gvRows.FindControl("chkSelectOne");
                            Label lblERID = (Label)gvRows.FindControl("lblERID");
                            int ERID = Convert.ToInt32(lblERID.Text);
                            Label lblERItemID = (Label)gvRows.FindControl("lblPK");
                            int ERItemID = Convert.ToInt32(lblERItemID.Text);
                            if (chked.Checked)//&& ERID==ChkERID
                            {
                                AttendanceDAC.HR_ApproveStatus(ERItemID);
                                if (ERID != Convert.ToInt32(ViewState["ERID"]))
                                {
                                    AttendanceDAC.HR_UpdateAsApproved( Convert.ToInt32(Session["UserId"]), ERID);
                                    ViewState["ERID"] = ERID;
                                }
                            }
                        }
                    }
                }
                tblShow.Visible = false;
                DataSet dsRefresh = AttendanceDAC.HR_EmpReimRejected();// AttendanceDAC.HR_EmpReimNotApprovedByEmpID(Convert.ToInt32(ViewState["EmpID"]));
                BindPagerPending();
                gvView.Columns[0].Visible = false;
                gvView.Visible = true;
                gvView.Columns[1].Visible = true;
                gvView.Columns[0].Visible = false;
                if (dsRefresh.Tables[0].Rows.Count == 0)
                {
                    btnApprove.Visible = false;
                }
                DataSet ds = AttendanceDAC.HR_EmpReimburseRejectedView(Convert.ToInt32(ViewState["EmpID"]));
                gvShowRej.DataSource = ds;
                gvShowRej.DataBind();
                gvShowRej.Visible = true;
                tblShowRej.Visible = true;
            }
        }
        protected void gvViewApproved_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "View")
            {
                tblViewApproved.Visible = false;
                tblAdd.Visible = false;
                tblShow.Visible = true;
                tblView.Visible = false;
                int ERID = Convert.ToInt32(e.CommandArgument);
                DataSet ds = AttendanceDAC.HR_EmpReimburseViewAmtApproved(ERID);
                gvShow.DataSource = ds;
                gvShow.DataBind();
                if (ds.Tables.Count > 0)
                {
                    btnApprove.Visible = false;
                    gvShow.Columns[0].Visible = false;
                    gvShow.Columns[16].Visible = false;
                    gvShow.Columns[17].Visible = false;
                }
                else { Response.Redirect("EmpReimbursement.aspx"); }
            }
        }
        protected void gvRejectedview_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "View")
            {
                btnApprove.Visible = true;
                tblAdd.Visible = false;
                tblShow.Visible = true;
                tblView.Visible = false;
                int ERID = Convert.ToInt32(e.CommandArgument);
                DataSet ds = AttendanceDAC.HR_EmpReimburseAmtRejected(ERID);
                gvShow.DataSource = ds;
                gvShow.DataBind();
                if (ds.Tables[0].Rows.Count == 0) { Response.Redirect("EmpReimbursement.aspx"); }
            }
            if (e.CommandName == "Approve")
            {
                int ERID = Convert.ToInt32(e.CommandArgument);
            }
        }
        public void GetEmpBySiteDept()
        {
            //int WorkSite = Convert.ToInt32(ddlWorksite.SelectedValue);
            int WorkSite = Convert.ToInt32(Convert.ToInt32(ddlWorksite_hid.Value == "" ? "0" : ddlWorksite_hid.Value));
            //  int Dept = Convert.ToInt32(ddlDepartment.SelectedValue);
            int Dept = Convert.ToInt32(Convert.ToInt32(ddlDepartment_hid.Value == "" ? "0" : ddlDepartment_hid.Value));
            try
            {
                if (Convert.ToInt32(ViewState["WSID"]) > 0)
                    WorkSite = Convert.ToInt32(ViewState["WSID"]);
            }
            catch { }
            DataSet ds = AttendanceDAC.HR_EmpRepaySearch(WorkSite, Dept, Convert.ToInt32(Session["CompanyID"]));
            ddlEmp.DataSource = ds;
            ddlEmp.DataTextField = "name";
            ddlEmp.DataValueField = "EmpID";
            ddlEmp.DataBind();
            ddlEmp.Items.Insert(0, new ListItem("---SELECT---", "0", true));
        }
        // for department
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionList_Department(string prefixText, int count, string contextKey)
        {
            Deptid = 0;
            DataSet ds = AttendanceDAC.GoogleSerch_TaskAssignment_GetDaprtment(prefixText.Trim(), Deptid);
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
            BindEmpList();
            CompanyID = Convert.ToInt32(Session["CompanyID"]);
            Deptid = Convert.ToInt32(ddlDepartment_hid.Value == "" ? "0" : ddlDepartment_hid.Value); ;
            btnSubmit.Visible = false;
        }
                //added for worksite
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionWorksiteList(string prefixText, int count, string contextKey)
        {
            DataSet ds = AttendanceDAC.HR_GetWorkSite_basedon_Wsid_googlesearch(prefixText.Trim(), WSID, WSStatus, CompanyID);
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
        protected void GetWorksite(object sender, EventArgs e)
        {
            BindEmpList();
            CompanyID = Convert.ToInt32(Session["CompanyID"]);
            WSID = Convert.ToInt32(ddlWorksite_hid.Value == "" ? "0" : ddlWorksite_hid.Value); ;
            if (TxtWorksite.Text == "") { WSID = 0; }
            btnSubmit.Visible = false;
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionList_Employee(string prefixText, int count, string contextKey)
        {
            Deptid = 0;
            DataSet ds = AttendanceDAC.HMS_Service_DLL_Employee_By_WS_Dept_googlesearch(prefixText.Trim(), WSID, Deptid);//WSID
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
        protected void GetEmployelist(object sender, EventArgs e)
        {
            SqlParameter[] sqlPrms = new SqlParameter[3];
                sqlPrms[0] = new SqlParameter("@search", TxtEmp.Text);
                sqlPrms[1] = new SqlParameter("@Siteid", siteid);
                if (siteid == 0)
                {
                    sqlPrms[1] = new SqlParameter("@Siteid", SqlDbType.Int);
                }
                sqlPrms[2] = new SqlParameter("@Deptid", Deptid);
                if (Deptid == 0)
                {
                    sqlPrms[2] = new SqlParameter("@Deptid", SqlDbType.Int);
                }
           // FIllObject.FillDropDown(ref ddlWorksite, "G_GET_WorkSitebyFilter", param);
            FIllObject.FillDropDown(ref ddlEmp, "HMS_Service_DLL_Employee_By_WS_Dept_googlesearch",sqlPrms);
            ListItem itmSelected = ddlEmp.Items.FindByText(TxtEmp.Text);
            if (itmSelected != null)
            {
                ddlEmp.SelectedItem.Selected = false;
                itmSelected.Selected = true;
            }
            if (TxtEmp.Text != "") { ddlEmp.SelectedIndex = 1;
            }
            btnSubmit.Visible = false;
        }
        protected void gvEdit_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Del")
            {
                int ID = Convert.ToInt32(e.CommandArgument);
                DataTable dtList = (DataTable)ViewState["ReimItems"];
                DataRow[] drSelected = null;
                drSelected = dtList.Select("ID='" + e.CommandArgument + "'");
                if (drSelected.Length > 0)
                    dtList.Rows.Remove(drSelected[0]);
                dtList.AcceptChanges();
                ViewState["ReimItems"] = dtList;
                gvRemiItems.DataSource = dtList;
                gvRemiItems.DataBind();
            }
        }
        protected void BtnEditSave_Click(object sender, EventArgs e)
        {
            string URL = Request.Url.Segments[Request.Url.Segments.Length - 1] + Request.Url.Query;
            foreach (GridViewRow gvRow in gvEdit.Rows)
            {
                DateTime DateOfSpent;
                Label lblEmpID = (Label)gvRow.Cells[2].FindControl("lblEmpID");
                Label lblRItem = (Label)gvRow.Cells[3].FindControl("lblRItemNo");
                DropDownList ddlunits = (DropDownList)gvRow.Cells[4].FindControl("ddlunits");
                FileUpload UploadProof = (FileUpload)gvRow.Cells[11].FindControl("UploadProof");
                int UnitOfMeasure = int.Parse(ddlunits.Text);
                if (UnitOfMeasure == 0)
                {
                    AlertMsg.MsgBox(Page, "Please select units of measure.!", AlertMsg.MessageType.Warning);
                    //Label1.Text = "Please select units of measure.!";
                    //Label1.ForeColor = System.Drawing.Color.Red;
                    return;
                }
                TextBox txtRate = (TextBox)gvRow.Cells[5].FindControl("txtRate");
                double UnitRte;// = int.Parse(txtRate.Text);
                try
                {
                    UnitRte = double.Parse(txtRate.Text);
                }
                catch (Exception)
                {
                    AlertMsg.MsgBox(Page, "Unitrate can takes numbers only.!", AlertMsg.MessageType.Warning);
                    //Label1.Text = "Unitrate can takes numbers only..!";
                    //Label1.ForeColor = System.Drawing.Color.Red;
                    return;
                }
                if (txtRate.Text == "" || UnitRte <= 0)
                {
                    AlertMsg.MsgBox(Page, "Please enter proper unitrate.!", AlertMsg.MessageType.Warning);
                    //Label1.Text = "Please enter proper unitrate.!";
                    //Label1.ForeColor = System.Drawing.Color.Red;
                    return;
                }
                else
                {
                    UnitRte = double.Parse(txtRate.Text);
                }
                TextBox txtQty = (TextBox)gvRow.Cells[6].FindControl("txtQty");
                double Quantity;
                try
                {
                    Quantity = double.Parse(txtQty.Text);
                }
                catch (Exception)
                {
                    AlertMsg.MsgBox(Page, "Quantity can take numbers only.!", AlertMsg.MessageType.Warning);
                    //Label1.Text = "Quantity can takes numbers only..!";
                    //Label1.ForeColor = System.Drawing.Color.Red;
                    return;
                }
                if (txtQty.Text == "" || Quantity <= 0)
                {
                    AlertMsg.MsgBox(Page, "Please enter proper quntity.!", AlertMsg.MessageType.Warning);
                    //Label1.Text = "Please enter proper quntity.!";
                    //Label1.ForeColor = System.Drawing.Color.Red;
                    return;
                }
                else
                {
                    Quantity = double.Parse(txtQty.Text);
                }
                Label txtAmount = (Label)gvRow.Cells[7].FindControl("txtAmount");
                TextBox txtPurpose = (TextBox)gvRow.Cells[8].FindControl("txtPurpose");
                Label lblRItemID = (Label)gvRow.Cells[9].FindControl("lblRItemNo");
                TextBox txtSpentOn = (TextBox)gvRow.Cells[10].FindControl("txtSpentOn");
                try
                {
                    DateOfSpent = CODEUtility.ConvertToDate(txtSpentOn.Text.Trim(), DateFormat.DayMonthYear);
                }
                catch (Exception)
                {
                    AlertMsg.MsgBox(Page, "Please select proper date.!", AlertMsg.MessageType.Warning);
                    //Label1.Text = "Please select proper date.!";
                    //Label1.ForeColor = System.Drawing.Color.Red;
                    return;
                }
                if (UploadProof.HasFile)
                {
                    string path = "";
                    DateTime MyDate = DateTime.Now;
                    MyString = MyDate.ToString("ddMMyyhhmmss");
                    extension = System.IO.Path.GetExtension(UploadProof.PostedFile.FileName).ToLower();
                    path = Server.MapPath("~\\HMS\\EmpReimbureseProof\\" + MyString + extension);
                    UploadProof.PostedFile.SaveAs(path);
                }
                string Proof = MyString + extension;
                int ReimburseID = Convert.ToInt32(lblRItem.Text), AUID = Convert.ToInt32(ddlunits.SelectedValue), PK = Convert.ToInt32(ViewState["PK"]);
                double Qty = Convert.ToDouble(txtQty.Text), UnitRate = Convert.ToDouble(txtRate.Text);
                string Description = txtPurpose.Text;
                DateOfSpent = CODEUtility.ConvertToDate(txtSpentOn.Text.Trim(), DateFormat.DayMonthYear);
                AttendanceDAC.HR_UpdateReimItems(ReimburseID, AUID, Qty, UnitRate, Description, DateOfSpent, Proof, PK);
                if (URL == "EmpReimbursement.aspx?key=2")
                {
                    tblShow.Visible = true;
                    int EmpID = Convert.ToInt32(lblEmpID.Text);
                    DataSet dsShow = AttendanceDAC.HR_EmpReimburseAmtPayable(EmpID);
                    gvShow.Visible = true;
                    gvShow.DataSource = dsShow;
                    gvShow.DataBind();
                    tblShow.Visible = true;
                    tblEdit.Visible = false;
                    btnApprove.Visible = true;
                    tblView.Visible = true;
                    gvView.Visible = true;
                    DataSet  dsPen = AttendanceDAC.HR_EmpReimNotApproved();
                    BindPagerPending();
                    gvView.Columns[0].Visible = true;
                    gvView.Columns[1].Visible = false;
                    foreach (GridViewRow gvRowP in gvView.Rows)
                    {
                        Label Emp = (Label)gvRowP.FindControl("lblEmpID");
                        int EmpIDP = Convert.ToInt32(Emp.Text);
                        if (EmpIDP == EmpID)
                        {
                            CheckBox chk = new CheckBox();
                            chk = (CheckBox)gvRowP.FindControl("chkApproval");
                            chk.Checked = true;
                        }
                    }
                }
                if (URL == "EmpReimbursement.aspx?key=3")
                {
                    int EmpID = Convert.ToInt32(lblEmpID.Text);
                    btnApprove.Visible = true;
                    tblShow.Visible = false;
                    tblShowRej.Visible = true;
                    DataSet dsRview = AttendanceDAC.HR_EmpReimburseRejectedView(EmpID);
                    gvShowRej.DataSource = dsRview;
                    gvShowRej.DataBind();
                    tblEdit.Visible = false;
                    tblView.Visible = true;
                    gvView.Visible = true;
                    DataSet dsRej = AttendanceDAC.HR_EmpReimRejected();
                    BindPagerPending();
                    gvView.Columns[0].Visible = false;
                    gvView.Columns[1].Visible = true;
                    foreach (GridViewRow gvRowView in gvView.Rows)
                    {
                        Label Emp = (Label)gvRowView.FindControl("lblEmpID");
                        int EmpIDIS = Convert.ToInt32(Emp.Text);
                        if (EmpIDIS == EmpID)
                        {
                            CheckBox chk = new CheckBox();
                            chk = (CheckBox)gvRowView.FindControl("chkRSelect");
                            chk.Checked = true;
                        }
                        if (UploadProof.HasFile)
                        {
                            string path = "";
                            DateTime MyDate = DateTime.Now;
                            MyString = MyDate.ToString("ddMMyyhhmmss");
                            extension = System.IO.Path.GetExtension(UploadProof.PostedFile.FileName).ToLower();
                            path = Server.MapPath("~\\HMS\\EmpReimbureseProof\\" + MyString + extension);
                            UploadProof.PostedFile.SaveAs(path);
                        }
                    }
                }
            }
        }
        public void CalculateAmtEdt()
        {
            foreach (GridViewRow gvRow in gvEdit.Rows)
            {
                TextBox txtRate = (TextBox)gvRow.Cells[5].FindControl("txtRate");
                double UnitRate;// = int.Parse(txtRate.Text);
                try
                {
                    UnitRate = double.Parse(txtRate.Text);
                }
                catch (Exception)
                {
                    AlertMsg.MsgBox(Page, "Unitrate can takes numbers only.!", AlertMsg.MessageType.Warning);
                    //Label1.Text = "Unitrate can takes numbers only..!";
                    //Label1.ForeColor = System.Drawing.Color.Red;
                    return;
                }
                if (txtRate.Text == "" || UnitRate <= 0)
                {
                    AlertMsg.MsgBox(Page, "Please enter proper unitrate.!", AlertMsg.MessageType.Warning);
                    //Label1.Text = "Please enter proper unitrate.!";
                    //Label1.ForeColor = System.Drawing.Color.Red;
                    return;
                }
                else
                {
                    UnitRate = double.Parse(txtRate.Text);
                }
                TextBox txtQty = (TextBox)gvRow.Cells[6].FindControl("txtQty");
                double Quantity;
                try
                {
                    Quantity = double.Parse(txtQty.Text);
                }
                catch (Exception)
                {
                    AlertMsg.MsgBox(Page, "Quantity can take numbers only.!", AlertMsg.MessageType.Warning);
                    //Label1.Text = "Quantity can takes numbers only..!";
                    //Label1.ForeColor = System.Drawing.Color.Red;
                    return;
                }
                if (txtQty.Text == "" || Quantity <= 0)
                {
                    AlertMsg.MsgBox(Page, "Please enter proper quntity.!", AlertMsg.MessageType.Warning);
                    //Label1.Text = "Please enter proper quntity.!";
                    //Label1.ForeColor = System.Drawing.Color.Red;
                    return;
                }
                else
                {
                    Quantity = double.Parse(txtQty.Text);
                }
                double Amount = UnitRate * Quantity;
                Label txtAmount = (Label)gvRow.Cells[7].FindControl("txtAmount");
                txtAmount.Text = Convert.ToString(Amount);
                ViewState["Amount"] = Amount;
            }
        }
        protected void txtUnitreteEdit_TextChanged(object sender, EventArgs e)
        {
            CalculateAmtEdt();
        }
        protected void txtQuantityEdit_TextChanged(object sender, EventArgs e)
        {
            CalculateAmtEdt();
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionFilterEmpList(string prefixText, int count, string contextKey)
        {
            DataSet ds = AttendanceDAC.GetEmployeesByTravel_googlesearch_Exp(prefixText.Trim());
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
        protected void GetEmp(object sender, EventArgs e)
        {
            string URL = Request.Url.Segments[Request.Url.Segments.Length - 1] + Request.Url.Query;
            int EmpID = 0;
            DataSet ds;
            if (URL == "EmpReimbursement.aspx?key=4")
            {
                //BindPagerPending();
                EmpID = Convert.ToInt32(emp_hid.Value == "" ? "0" : emp_hid.Value);
                if (EmpID == 0)
                {
                    EmpID = 0;
                    ds = new DataSet();
                    ds = AttendanceDAC.HR_ReimburseTransferdByEmpID(EmpID);
                }
                else
                {
                    ds = new DataSet();
                    ds = AttendanceDAC.HR_ReimburseTransferdByEmpID(EmpID);
                }
                BindPagerTransAmt();
                gvTransfered.DataSource = ds;
                gvTransfered.DataBind();
                gvTransfered.Visible = true;
                btnApprove.Visible = false;
            }
            else
            {
                EmpReimbursementPendingRejPaging.CurrentPage = 1;
             //   txtFilterEmpID.Text = "";
                EmpID = Convert.ToInt32(emp_hid.Value == "" ? "0" : emp_hid.Value);
                ds = new DataSet();
                ds = AttendanceDAC.HR_EmpReimNotApprovedByEmpID(EmpID);
                BindPagerPending();
                gvView.Visible = true;
                gvView.Columns[0].Visible = true;
                int Rows = Convert.ToInt32(ds.Tables[0].Rows.Count);
                if (Rows < 1) { btnApprove.Visible = false; }
                else
                {
                    btnApprove.Visible = true;
                }
                tblShow.Visible = false;
            }
        }
        protected void btnFilter_Click(object sender, EventArgs e)
        {
            EmpReimbursementTransferdPaging.CurrentPage = 1;
            string URL1 = Request.Url.Segments[Request.Url.Segments.Length - 1] + Request.Url.Query;
           // ddlFilterEmp.SelectedIndex = -1;
            if (txtSearchemp.Text == "")
            {
                if (URL1 == "EmpReimbursement.aspx?key=4")
                {
                    //BindPagerPending();
                    int EmpID = 0;
                   DataSet ds = AttendanceDAC.HR_ReimburseTransferdByEmpID(EmpID);
                    BindPagerTransAmt();
                    gvTransfered.DataSource = ds;
                    gvTransfered.DataBind();
                    gvTransfered.Visible = true;
                    btnApprove.Visible = false;
                }
            }
            else
            {
                string URL = Request.Url.Segments[Request.Url.Segments.Length - 1] + Request.Url.Query;
                if (URL == "EmpReimbursement.aspx?key=2")
                {
                    int EmpID = Convert.ToInt32(emp_hid.Value == "" ? "0" : emp_hid.Value);
                    BindPagerPending();
                    gvView.Visible = true;
                    gvView.Columns[0].Visible = true;
                    btnApprove.Visible = false;
                    tblShow.Visible = false;
                }
                if (URL == "EmpReimbursement.aspx?key=3")
                {
                    int EmpID = Convert.ToInt32(emp_hid.Value == "" ? "0" : emp_hid.Value);
               DataSet  ds = AttendanceDAC.HR_EmpReimRejectedByEmpID(EmpID);
                    BindPagerPending();
                    gvView.Visible = true;
                    gvView.Columns[0].Visible = true;
                    btnApprove.Visible = false;
                    tblShow.Visible = false;
                }
                if (URL == "EmpReimbursement.aspx?key=4")
                {
                    //BindPagerPending();
                    int EmpID = Convert.ToInt32(emp_hid.Value == "" ? "0" : emp_hid.Value);
                    DataSet  ds = AttendanceDAC.HR_ReimburseTransferdByEmpID(EmpID);
                    BindPagerTransAmt();
                    gvTransfered.DataSource = ds;
                    gvTransfered.DataBind();
                    gvTransfered.Visible = true;
                    btnApprove.Visible = false;
                }
            }
        }
        protected void ddlTransfer_SelectedIndexChanged(object sender, EventArgs e)
        {
            EmpReimbursementAprovedPaging.CurrentPage = 1;
            int EmpID = Convert.ToInt32(ddlTransfer.SelectedValue);
           // txtTranferSearch.Text = "";
            if (ddlTransfer.SelectedIndex == 0)
            {
                BindPager();
            }
            else
            {
                EmployeeApprovedByEmpID(objHrCommon, EmpID);
                gvViewApproved.Visible = true;
                tblViewApproved.Visible = true;
                gvViewApproved.Columns[0].Visible = true;
                btnTransferAcc.Visible = true;
            }
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionEmpTransferList(string prefixText, int count, string contextKey)
        {
            DataSet ds = AttendanceDAC.GetEmployeesByTravel_googlesearch_Exp(prefixText.Trim());
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
        protected void GetTransferEmp(object sender, EventArgs e)
        {
            SqlParameter[] objParam = new SqlParameter[1];
            objParam[0] = new SqlParameter("@search", txtSearchemp.Text);
            //objParam[1] = new SqlParameter("@Status", Status);
            FIllObject.FillDropDown(ref ddlTransfer, "GetEmployeesByTravel_googlesearch_Exp", objParam);
            ListItem itmSelected = ddlTransfer.Items.FindByText(txtSearchEmpTransfer.Text);
            if (itmSelected != null)
            {
                // ddlFilterEmp.SelectedItem.Selected = false;
                itmSelected.Selected = true;
            }
            ddlTransfer_SelectedIndexChanged(sender, e);
        }
        public void EmployeeApprovedByEmpID(HRCommon objHrCommon, int EmpID)
        {
            try
            {
                objHrCommon.PageSize = EmpReimbursementAprovedPaging.ShowRows;
                objHrCommon.CurrentPage = EmpReimbursementAprovedPaging.CurrentPage;
                DataSet ds = AttendanceDAC.HR_EmpReimEmpTranserByEmpIDByPaging(objHrCommon, EmpID);
                if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0)
                {
                    gvViewApproved.DataSource = ds;
                    EmpReimbursementAprovedPaging.Bind(objHrCommon.CurrentPage, objHrCommon.TotalPages, objHrCommon.NoofRecords, objHrCommon.PageSize);
                }
                else
                {
                    gvViewApproved.EmptyDataText = "No Records Found";
                    EmpReimbursementAprovedPaging.Visible = false;
                }
                gvViewApproved.DataBind();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        protected void btnTranfer_Click(object sender, EventArgs e)
        {
            EmpReimbursementAprovedPaging.CurrentPage = 1;
            if (txtTransferEmpID.Text == "")
            {
                BindPager();
            }
            else
            {
                ddlTransfer.SelectedIndex = 0;
                BindPager();
                int EmpID = Convert.ToInt32(txtTransferEmpID.Text);
                EmployeeApprovedByEmpID(objHrCommon, EmpID);
                gvViewApproved.Visible = true;
                gvViewApproved.Columns[0].Visible = true;
                btnTransferAcc.Visible = true;
                if (gvViewApproved.Rows.Count == 0)
                    btnTransferAcc.Enabled = false;
            }
        }
        protected void gvViewApproved_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.Header)
                {
                    CheckBox chk = (CheckBox)e.Row.FindControl("chkSelectAll");
                    chk.Attributes.Add("onclick", String.Format("javascript:return SelectAll(this,'{0}','chkToTransfer');", gvViewApproved.ClientID));
                }
            }
            catch (Exception Ex)
            {
                //report error
            }
        }
        protected void btnTransferAcc_Click(object sender, EventArgs e)
        {
            DataSet dsTransferDetail = new DataSet("TranserDataSet");
            DataTable dtTDT = new DataTable("TranserTable");
            dtTDT.Columns.Add(new DataColumn("CreditAmt", typeof(System.Double)));
            dtTDT.Columns.Add(new DataColumn("DebitAmt", typeof(System.Double)));
            dtTDT.Columns.Add(new DataColumn("Sequence", typeof(System.Int32)));
            dtTDT.Columns.Add(new DataColumn("EmpID", typeof(System.Int32)));
            dtTDT.Columns.Add(new DataColumn("CompanyID", typeof(System.Int32)));
            dtTDT.Columns.Add(new DataColumn("VocherID", typeof(System.Int32)));
            dtTDT.Columns.Add(new DataColumn("WorkSiteId", typeof(System.Int32)));
            dtTDT.Columns.Add(new DataColumn("ERID", typeof(System.Int32)));
            dsTransferDetail.Tables.Add(dtTDT);
            int EmpID = 0,count=0;
            Double TotAmt = 0;
           foreach (GridViewRow gvRow in gvViewApproved.Rows)
            {
               CheckBox chk = new CheckBox();
               chk = (CheckBox)gvRow.FindControl("chkToTransfer");
                    if (chk.Checked)
                    {
                    chk = (CheckBox)gvRow.FindControl("chkToTransfer");
                        Label lblEmp = (Label)gvRow.FindControl("lblEmpID");
                        Label lblERID = (Label)gvRow.FindControl("lblERID");
                        Label lblAmount = (Label)gvRow.FindControl("lblAmount");
                        int ERID = Convert.ToInt32(lblERID.Text);
                        AttendanceDAC.HR_SetStatusTransfered(ERID);
                        EmpID = Convert.ToInt32(lblEmp.Text);
                        double Amt = Convert.ToDouble(lblAmount.Text);
                        TotAmt = TotAmt + Amt;
                        DataSet dsLed = AttendanceDAC.HR_EmpLedger(CompanyID, EmpID);
                        int VocherID = Convert.ToInt32(dsLed.Tables[0].Rows[0]["VocherID"]);
                        int WorkSiteId = Convert.ToInt32(dsLed.Tables[0].Rows[0]["WorkSiteId"]);
                        DataRow dr = dtTDT.NewRow();
                        dr["EmpID"] = EmpID;
                        dr["CompanyID"] = CompanyID;
                        dr["DebitAmt"] = Amt;
                        dr["CreditAmt"] = 0.00000;
                        dr["VocherID"] = VocherID;
                        dr["WorkSiteId"] = WorkSiteId;
                        dr["ERID"] = ERID;
                        dtTDT.Rows.Add(dr);
                        string Remarks = "Reimbursements";
                        dsTransferDetail.AcceptChanges();
                        DataSet ds = AttendanceDAC.HMS_TranserAccXML(dsTransferDetail, Remarks, TotAmt,  Convert.ToInt32(Session["UserId"]));
                        count = count + 1;
                    }
                }
            if (count > 0)
            {
                AlertMsg.MsgBox(Page, "A/c Posted Done !", AlertMsg.MessageType.Success);
                //Label1.Text = "A/c Posted Done !";
                //Label1.ForeColor = System.Drawing.Color.Green;
            }
            else
            {
                AlertMsg.MsgBox(Page, "Select atleast one Record !", AlertMsg.MessageType.Warning);
                //Label1.Text = "Select atleast one Record ";
                //Label1.ForeColor = System.Drawing.Color.Red;
            }
            //BindPagerTransAmt();
            BindPager();
        }
        protected void gvTransfered_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "View")
            {
                int ERID = Convert.ToInt32(e.CommandArgument);
                tblShow.Visible = true;
             DataSet   ds = AttendanceDAC.HR_EmpReimburseViewTransfered(ERID);
                gvShow.DataSource = ds;
                gvShow.DataBind();
                gvShow.Columns[16].Visible = false;
            }
            if (e.CommandName == "Edit")
            {
                int ERID = Convert.ToInt32(e.CommandArgument);
            }
        }
        protected void chkApproval_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkbox = (CheckBox)sender;
            GridViewRow gvr = (GridViewRow)checkbox.NamingContainer;
            Label txt = (Label)gvr.Cells[2].FindControl("lblEmpID");
            tblAdd.Visible = false;
            tblShow.Visible = true;
            tblView.Visible = true;
            int EmpID = Convert.ToInt32(txt.Text);
            DataSet ds = null;
             string URL = Request.Url.Segments[Request.Url.Segments.Length - 1] + Request.Url.Query;
             if (URL == "EmpReimbursement.aspx?key=2")
             {
                  ds = AttendanceDAC.HR_EmpReimburseAmtPayable(EmpID);
             }
             if (URL == "EmpReimbursement.aspx?key=5")
             {
                 ds = AttendanceDAC.HR_EmpRecmndAmtPayable(EmpID);
             }
             if (URL == "EmpReimbursement.aspx?key=6")
             {
                 ds = AttendanceDAC.HR_EmpFinalAmtPayable(EmpID);
             }
            gvShow.DataSource = ds;
            gvShow.DataBind();
            foreach (GridViewRow gvRow in gvView.Rows)
            {
                CheckBox chk = new CheckBox();
                chk = (CheckBox)gvRow.FindControl("chkApproval");
                Label lbl = (Label)gvRow.FindControl("lblEmpID");
                int ERIDToChk = Convert.ToInt32(lbl.Text);
                if (ERIDToChk == EmpID)
                {
                    chk.Checked = true;
                }
                else { chk.Checked = false; }
            }
            if (gvShow.Rows.Count == 0)
            {
                btnApprove.Visible = false;
            }
            else
            {
                if (URL == "EmpReimbursement.aspx?key=2")
                {
                    btnApprove.Visible = true;
                    btnApprove.Text = "Recommend";
                }
                else if (URL == "EmpReimbursement.aspx?key=5") 
                {
                    btnApprove.Visible = true;
                    btnApprove.Text = "Final Approve";
                }
                else
                {
                    btnApprove.Visible = true;
                    btnApprove.Text = "Approve";
                }
            }
        }
        protected void chkRSelect_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkbox = (CheckBox)sender;
            GridViewRow gvr = (GridViewRow)checkbox.NamingContainer;
            Label txt = (Label)gvr.Cells[2].FindControl("lblEmpID");
            tblAdd.Visible = false;
            //tblShow.Visible = true;
            tblView.Visible = true;
            tblShow.Visible = false;
            int EmpID = Convert.ToInt32(txt.Text);
            DataSet ds = AttendanceDAC.HR_EmpReimburseRejectedView(EmpID);
            gvShowRej.DataSource = ds;
            gvShowRej.DataBind();
            gvShowRej.Visible = true;
            tblShowRej.Visible = true;
            foreach (GridViewRow gvRow in gvView.Rows)
            {
                CheckBox chk = new CheckBox();
                chk = (CheckBox)gvRow.FindControl("chkRSelect");
                Label lbl = (Label)gvRow.FindControl("lblEmpID");
                int ERIDToChk = Convert.ToInt32(lbl.Text);
                if (ERIDToChk == EmpID)
                {
                    chk.Checked = true;
                }
                else { chk.Checked = false; }
            }
            if (gvShowRej.Rows.Count == 0)
            {
                btnApprove.Visible = false;
            }
            else
            {
                btnApprove.Visible = true;
            }
        }
        protected void chkTASelect_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkbox = (CheckBox)sender;
            GridViewRow gvr = (GridViewRow)checkbox.NamingContainer;
            Label txt = (Label)gvr.Cells[1].FindControl("lblERID");
            int ERID = Convert.ToInt32(txt.Text);
            tblShow.Visible = true;
            DataSet ds = AttendanceDAC.HR_EmpReimburseViewTransfered(ERID);
            gvShow.DataSource = ds;
            gvShow.DataBind();
            btnApprove.Visible = false;
            gvShow.Columns[14].Visible = false;
            gvShow.Columns[15].Visible = false;
            foreach (GridViewRow gvRow in gvTransfered.Rows)
            {
                CheckBox chk = new CheckBox();
                chk = (CheckBox)gvRow.FindControl("chkTASelect");
                Label lbl = (Label)gvRow.FindControl("lblERID");
                int ERIDToChk = Convert.ToInt32(lbl.Text);
                if (ERIDToChk == ERID)
                {
                    chk.Checked = true;
                }
                else { chk.Checked = false; }
            }
        }
        protected void gvShowRej_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.Header)
                {
                    CheckBox chkMail = (CheckBox)e.Row.FindControl("chkSelectAll");
                    chkMail.Attributes.Add("onclick", String.Format("javascript:return SelectAll(this,'{0}','chkSelectOne');", gvShowRej.ClientID));
                }
            }
            catch (Exception Ex)
            {
                //report error
            }
        }
        protected void btnRApprove_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow gvRow in gvView.Rows)
            {
                Label Emp = (Label)gvRow.FindControl("lblEmpID");
                int EmpID = Convert.ToInt32(Emp.Text);
                ViewState["EmpID"] = EmpID;
                CheckBox chk = new CheckBox();
                chk = (CheckBox)gvRow.FindControl("chkRSelect");
                if (chk.Checked)
                {
                    foreach (GridViewRow gvRows in gvShowRej.Rows)
                    {
                        CheckBox chked = new CheckBox();
                        chked = (CheckBox)gvRows.FindControl("chkSelectOne");
                        Label lblERID = (Label)gvRows.FindControl("lblERID");
                        int ERID = Convert.ToInt32(lblERID.Text);
                        Label lblERItemID = (Label)gvRows.FindControl("lblPK");
                        int ERItemID = Convert.ToInt32(lblERItemID.Text);
                        if (chked.Checked)//&& ERID==ChkERID
                        {
                            AttendanceDAC.HR_ApproveStatus(ERItemID);
                            if (ERID != Convert.ToInt32(ViewState["ERID"]))
                            {
                                AttendanceDAC.HR_UpdateAsApproved( Convert.ToInt32(Session["UserId"]), ERID);
                                ViewState["ERID"] = ERID;
                            }
                        }
                    }
                }
            }
            tblShow.Visible = false;
            DataSet dsRefresh = AttendanceDAC.HR_EmpReimRejected();// AttendanceDAC.HR_EmpReimNotApprovedByEmpID(Convert.ToInt32(ViewState["EmpID"]));
            BindPagerPending();
            gvView.Visible = true;
            gvView.Columns[0].Visible = true;
            if (dsRefresh.Tables[0].Rows.Count == 0)
            {
                btnApprove.Visible = false;
            }
        }
        protected void gvShowRej_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Edt")
            {
                btnApprove.Visible = false;
                BindEmpList();
                BindItem();
                tblAdd.Visible = false;
                btnSubmit.Visible = false;
                tblView.Visible = false;
                gvView.Visible = false;
                int PK = Convert.ToInt32(e.CommandArgument);
                ViewState["PK"] = PK;
             DataSet   ds = AttendanceDAC.HR_EditReimburseItems(PK);
                ddlEmp.SelectedValue = Convert.ToInt32(ds.Tables[0].Rows[0]["EmpID"]).ToString();
                gvEdit.DataSource = ds;
                gvEdit.DataBind();
                gvRemiItems.Visible = false;
                gvShow.Visible = false;
                tblEdit.Visible = true;
                gvEdit.Visible = true;
                foreach (GridViewRow gvRow in gvShowRej.Rows)
                {
                    CheckBox chk = new CheckBox();
                    chk = (CheckBox)gvRow.FindControl("chkSelectOne");
                    Label lbl = (Label)gvRow.FindControl("lblPK");
                    int ERIDToChk = Convert.ToInt32(lbl.Text);
                    if (ERIDToChk == PK)
                    {
                        chk.Checked = true;
                    }
                    else { chk.Checked = false; }
                }
            }
        }
        protected void btnRejReaSave_Click(object sender, EventArgs e)
        {
            string Reason = txtReason.Text;
            int PK = Convert.ToInt32(ViewState["PK"]);
            AttendanceDAC.HR_ReimburseRejectReason(Reason, PK);
            AlertMsg.MsgBox(Page, "Done!", AlertMsg.MessageType.Success);
            //Label1.Text = " Done !";
            //Label1.ForeColor = System.Drawing.Color.Green;
            tblRejReason.Visible = false;
            btnApprove.Visible = true;
            foreach (GridViewRow gvRow in gvShow.Rows)
            {
                CheckBox chk = new CheckBox();
                chk = (CheckBox)gvRow.FindControl("chkSelectOne");
                if (chk.Checked)
                {
                    Label lblEmp = (Label)gvRow.Cells[3].FindControl("lblEmpID");
                    Label lblPK = (Label)gvRow.Cells[13].FindControl("lblPK");
                    Label lblERID = (Label)gvRow.Cells[14].FindControl("lblERID");
                    int ERID = Convert.ToInt32(lblERID.Text);
                    int EmpID = Convert.ToInt32(lblEmp.Text);
                    AttendanceDAC.HR_RejectedStatus(PK,  Convert.ToInt32(Session["UserId"]));
                    DataSet dsref = AttendanceDAC.HR_EmpReimburseAmtPayable(EmpID);
                    gvShow.DataSource = dsref;
                    gvShow.DataBind();
                    gvShow.Visible = true;
                    tblAdd.Visible = false;
                    tblShow.Visible = true;
                    tblView.Visible = false;
                    DataSet ds = AttendanceDAC.HR_EmpReimNotApproved();
                    BindPagerPending();
                    gvView.Columns[0].Visible = true;
                    gvView.Columns[1].Visible = false;
                    tblView.Visible = true; gvView.Visible = true;
                }
            }
        }
    }
}
