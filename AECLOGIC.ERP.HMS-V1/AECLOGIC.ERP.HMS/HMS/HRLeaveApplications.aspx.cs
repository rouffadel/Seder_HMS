using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using AECLOGIC.HMS.BLL;
using System.Configuration;
using AECLOGIC.ERP.COMMON;
using System.Data.SqlClient;
using System.IO;
using Aeclogic.Common.DAL;
namespace AECLOGIC.ERP.HMSV1
{
    public partial class HRLeaveApplicationsV1 : AECLOGIC.ERP.COMMON.WebFormMaster
    {
        int mid = 0;
        bool viewall;
        string menuname;
        static int SearchCompanyID;
        static int Siteid = 0;
        static int EDeptid;
        static int EWsid;
        static int Empid;
        string menuid;
        bool Editable;
        static int WSId = 0;
        HRCommon objHrCommon = new HRCommon();
        string connectionString = ConfigurationManager.ConnectionStrings["ERPConnectionString"].ConnectionString;
        static int CompanyID = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["CompanyID"].ToString());
        static int ModID;
        static int Userid;
        protected override void OnInit(EventArgs e)
        {
            ModuleID = 1;
            base.OnInit(e);
            #region InProcesspaging
            HRLeaveAppInprocessPaging.FirstClick += new Paging.PageFirst(HRLeaveAppInprocessPaging_FirstClick);
            HRLeaveAppInprocessPaging.PreviousClick += new Paging.PagePrevious(HRLeaveAppInprocessPaging_FirstClick);
            HRLeaveAppInprocessPaging.NextClick += new Paging.PageNext(HRLeaveAppInprocessPaging_FirstClick);
            HRLeaveAppInprocessPaging.LastClick += new Paging.PageLast(HRLeaveAppInprocessPaging_FirstClick);
            HRLeaveAppInprocessPaging.ChangeClick += new Paging.PageChange(HRLeaveAppInprocessPaging_FirstClick);
            HRLeaveAppInprocessPaging.ShowRowsClick += new Paging.ShowRowsChange(HRLeaveAppInprocessPaging_ShowRowsClick);
            HRLeaveAppInprocessPaging.CurrentPage = 1;
            #endregion InProcesspaging
            #region Grantedpaging
            HRLeaveAppGrantedPaging.FirstClick += new Paging.PageFirst(HRLeaveAppGrantedPaging_FirstClick);
            HRLeaveAppGrantedPaging.PreviousClick += new Paging.PagePrevious(HRLeaveAppGrantedPaging_FirstClick);
            HRLeaveAppGrantedPaging.NextClick += new Paging.PageNext(HRLeaveAppGrantedPaging_FirstClick);
            HRLeaveAppGrantedPaging.LastClick += new Paging.PageLast(HRLeaveAppGrantedPaging_FirstClick);
            HRLeaveAppGrantedPaging.ChangeClick += new Paging.PageChange(HRLeaveAppGrantedPaging_FirstClick);
            HRLeaveAppGrantedPaging.ShowRowsClick += new Paging.ShowRowsChange(HRLeaveAppGrantedPaging_ShowRowsClick);
            HRLeaveAppGrantedPaging.CurrentPage = 1;
            #endregion Grantedpaging
            #region Rejectedpaging
            HRLeaveAppRejPaging.FirstClick += new Paging.PageFirst(HRLeaveAppRejPaging_FirstClick);
            HRLeaveAppRejPaging.PreviousClick += new Paging.PagePrevious(HRLeaveAppRejPaging_FirstClick);
            HRLeaveAppRejPaging.NextClick += new Paging.PageNext(HRLeaveAppRejPaging_FirstClick);
            HRLeaveAppRejPaging.LastClick += new Paging.PageLast(HRLeaveAppRejPaging_FirstClick);
            HRLeaveAppRejPaging.ChangeClick += new Paging.PageChange(HRLeaveAppRejPaging_FirstClick);
            HRLeaveAppRejPaging.ShowRowsClick += new Paging.ShowRowsChange(HRLeaveAppRejPaging_ShowRowsClick);
            HRLeaveAppRejPaging.CurrentPage = 1;
            #endregion Rejectedpaging
            HRLeaveAppCanPaging.FirstClick += new Paging.PageFirst(HRLeaveAppCanPaging_FirstClick);
            HRLeaveAppCanPaging.PreviousClick += new Paging.PagePrevious(HRLeaveAppCanPaging_FirstClick);
            HRLeaveAppCanPaging.NextClick += new Paging.PageNext(HRLeaveAppCanPaging_FirstClick);
            HRLeaveAppCanPaging.LastClick += new Paging.PageLast(HRLeaveAppCanPaging_FirstClick);
            HRLeaveAppCanPaging.ChangeClick += new Paging.PageChange(HRLeaveAppCanPaging_FirstClick);
            HRLeaveAppCanPaging.ShowRowsClick += new Paging.ShowRowsChange(HRLeaveAppCanPaging_ShowRowsClick);
            HRLeaveAppCanPaging.CurrentPage = 1;
            ModID = ModuleID;
        }
        #region Rejected
        void HRLeaveAppRejPaging_ShowRowsClick(object sender, EventArgs e)
        {
            HRLeaveAppRejPaging.CurrentPage = 1;
            BindPager();
        }
        void HRLeaveAppRejPaging_FirstClick(object sender, EventArgs e)
        {
            if (hdnSearchRej.Value == "1")
                HRLeaveAppRejPaging.CurrentPage = 1;
            BindPager();
            hdnSearchRej.Value = "0";
        }
        #endregion Rejected
        void HRLeaveAppCanPaging_ShowRowsClick(object sender, EventArgs e)
        {
            HRLeaveAppCanPaging.CurrentPage = 1;
            //  BindPager();
        }
        void HRLeaveAppCanPaging_FirstClick(object sender, EventArgs e)
        {
            //if (hdnSearchRej.Value == "1")
            HRLeaveAppCanPaging.CurrentPage = 1;
            //BindPager();
            //hdnSearchRej.Value = "0";
        }
        #region Granted
        void HRLeaveAppGrantedPaging_ShowRowsClick(object sender, EventArgs e)
        {
            HRLeaveAppGrantedPaging.CurrentPage = 1;
            BindPager();
        }
        void HRLeaveAppGrantedPaging_FirstClick(object sender, EventArgs e)
        {
            if (hdnSearchGranted.Value == "1")
                HRLeaveAppGrantedPaging.CurrentPage = 1;
            BindPager();
            hdnSearchGranted.Value = "0";
        }
        #endregion Granted
        #region InProcess
        void HRLeaveAppInprocessPaging_ShowRowsClick(object sender, EventArgs e)
        {
            HRLeaveAppInprocessPaging.CurrentPage = 1;
            BindPager();
        }
        void HRLeaveAppInprocessPaging_FirstClick(object sender, EventArgs e)
        {
            if (hdnSearchInProcess.Value == "1")
                HRLeaveAppInprocessPaging.CurrentPage = 1;
            BindPager();
            hdnSearchInProcess.Value = "0";
        }
        void BindPager()
        {
            if (Convert.ToInt32(Request.QueryString["key"]) == 1)
            {
                Empid = 0;
                if (txtSerachWorkApr.Text != "")
                {
                    EWsid = Convert.ToInt32(txtSerachWorkApr.Text.Substring(2, 5));
                }
                else { EWsid = 0; }
                if (Empid == 0)
                {
                    if (txtSearchEmpApr.Text != "")
                    {
                        Empid = Convert.ToInt32(txtSearchEmpApr.Text.Substring(0, 4));
                    }
                    else { Empid = 0; }
                }
                if (txtSearchDeptApr.Text != "") { EDeptid = Convert.ToInt32(txtSearchDeptApr.Text.Substring(0, 4)); } else { EDeptid = 0; }
                objHrCommon.PageSize = HRLeaveAppGrantedPaging.CurrentPage;
                objHrCommon.CurrentPage = HRLeaveAppGrantedPaging.ShowRows;
                BindGrantedLeaves(objHrCommon);
            }
            else if (Convert.ToInt32(Request.QueryString["key"]) == 2)
            {
                if (txtSerachWorkRej.Text == "") { EWsid = 0; }
                if (txtSerachEmpRej.Text != "") { Empid = Convert.ToInt32(txtSerachEmpRej.Text.Substring(0, 4)); } else { Empid = 0; }
                if (txtSearchDeptRej.Text != "") { EDeptid = Convert.ToInt32(txtSearchDeptRej.Text.Substring(0, 4)); } else { EDeptid = 0; }
                objHrCommon.PageSize = HRLeaveAppRejPaging.CurrentPage;
                objHrCommon.CurrentPage = HRLeaveAppRejPaging.ShowRows;
                BindRejectedLeaves(objHrCommon);
            }
            else if (Convert.ToInt32(Request.QueryString["key"]) == 3 || Convert.ToInt32(Request.QueryString["key"]) == 4 || Convert.ToInt32(Request.QueryString["key"]) == 7)
            {
                if (txtSearchWorksite.Text == "") { EWsid = 0; }
                if (txtSearchEmp.Text != "") { Empid = Convert.ToInt32(txtSearchEmp.Text.Substring(0, 4)); } else { Empid = 0; }
                if (txtdept.Text != "") { EDeptid = Convert.ToInt32(txtdept.Text.Substring(0, 4)); } else { EDeptid = 0; }
                objHrCommon.PageSize = HRLeaveAppInprocessPaging.CurrentPage;
                objHrCommon.CurrentPage = HRLeaveAppInprocessPaging.ShowRows;
                BindLeaveDetails(objHrCommon);
            }
            else if (Convert.ToInt32(Request.QueryString["key"]) == 6)
            {
                if (txtcanceledWS.Text == "") { EWsid = 0; }
                if (txtCanceledEmp.Text != "") { Empid = Convert.ToInt32(txtCanceledEmp.Text.Substring(0, 4)); } else { Empid = 0; }
                if (txtCanceledDept.Text != "") { EDeptid = Convert.ToInt32(txtCanceledDept.Text.Substring(0, 4)); } else { EDeptid = 0; }
                objHrCommon.PageSize = HRLeaveAppCanPaging.CurrentPage;
                objHrCommon.CurrentPage = HRLeaveAppCanPaging.ShowRows;
                BindCanceledLeaves(objHrCommon);
            }
            else
            {
                if (txtSearchWorksite.Text == "") { EWsid = 0; }
                if (txtSearchEmp.Text != "") { Empid = Convert.ToInt32(txtSearchEmp.Text.Substring(0, 4)); } else { Empid = 0; }
                if (txtdept.Text != "") { EDeptid = Convert.ToInt32(txtdept.Text.Substring(0, 4)); } else { EDeptid = 0; }
                objHrCommon.PageSize = HRLeaveAppInprocessPaging.CurrentPage;
                objHrCommon.CurrentPage = HRLeaveAppInprocessPaging.ShowRows;
                BindLeaveDetails(objHrCommon);
            }
        }
        #endregion InProcess
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                SearchCompanyID = Convert.ToInt32(Session["CompanyID"]);
                Userid = Convert.ToInt32(Convert.ToInt32(Session["UserId"]).ToString());
                if (!IsPostBack)
                {
                    BindYears();
                    lnkCloseCal.Visible = false;
                    ViewState["LID"] = 0;
                    int EmpID = Convert.ToInt32(Session["UserId"]);
                    ViewState["EmpID"] = EmpID;
                    BindPager();
                    tblHR.Visible = true;
                    tblHREdit.Visible = false;
                    if (Request.QueryString.Count > 0)
                    {
                        int id = Convert.ToInt32(Request.QueryString["key"].ToString());
                        if (id == 1)
                        {
                            ViewState["IsCFrwd"] = false;
                            ViewState["LevTypID"] = 0;
                            BindPager();
                            //BindGrantedLeaves();
                            tblGrantedLeaves.Visible = true;
                            tblHR.Visible = false;
                            tblHREdit.Visible = false;
                            if (Request.QueryString["Empid"] != null && Request.QueryString["Empid"] != string.Empty)
                            {
                                Empid = Convert.ToInt32(Request.QueryString["Empid"].Substring(0, 4));
                                btnSubmitGrant_Click(null, null);
                            }
                        }
                        if (id == 2)
                        {
                            tblGrantedLeaves.Visible = false;
                            tblHR.Visible = false;
                            tblHREdit.Visible = false;
                            tblRejected.Visible = true;
                            BindPager();
                            //BindRejectedLeaves();
                        }
                        if (id == 6)
                        {
                            tblGrantedLeaves.Visible = false;
                            tblHR.Visible = false;
                            tblHREdit.Visible = false;
                            tblRejected.Visible = false;
                            tblCanceled.Visible = true;
                            BindPager();
                        }
                    }
                }
                int key = 0;
                if (Request.QueryString.Count > 0)
                {
                    key = Convert.ToInt32(Request.QueryString["key"]);
                    if (key == 1 || key == 3 || key == 7 || key == 8)
                    {
                        gvLeaveApptoHr.Columns[19].Visible = false;
                    }
                    if (key == 4)
                    {
                        gvLeaveApptoHr.Columns[17].Visible = false;
                        gvLeaveApptoHr.Columns[18].Visible = false;
                        gvLeaveApptoHr.Columns[19].Visible = true;
                    }
                }
                else
                {
                    gvLeaveApptoHr.Columns[19].Visible = false;
                }
            }
            catch (Exception ex)
            {
                clsErrorLog.HMSEventLog(ex, "HRLeaveApplication", "Page_Load", "001");
            }
        }
        void BindRejectedLeaves(HRCommon objHrCommon)
        {
            try
            {
                objHrCommon.PageSize = HRLeaveAppRejPaging.ShowRows;
                objHrCommon.CurrentPage = HRLeaveAppRejPaging.CurrentPage;
                int? Month = null;
                int? Year = null;
                //if (ddlMonthRej.SelectedIndex != 0)
                Month = Convert.ToInt32(ddlMonthRej.SelectedValue);
                // if (ddlYearRej.SelectedIndex != 0)
                Year = Convert.ToInt32(ddlYearRej.SelectedValue);
                DataSet ds = AttendanceDAC.HR_GetLeaveRejectedByPaging(objHrCommon, Month, Year, Convert.ToInt32(Session["CompanyID"]), Empid, EDeptid, EWsid);
                if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0)
                {
                    gvRejected.DataSource = ds;
                    HRLeaveAppRejPaging.Bind(objHrCommon.CurrentPage, objHrCommon.TotalPages, objHrCommon.NoofRecords, objHrCommon.PageSize);
                }
                else
                {
                    gvRejected.EmptyDataText = "No Records Found";
                    HRLeaveAppRejPaging.Visible = false;
                }
                gvRejected.DataBind();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        void BindCanceledLeaves(HRCommon objHrCommon)
        {
            try
            {
                tblCanceled.Visible = true;
                objHrCommon.PageSize = HRLeaveAppCanPaging.ShowRows;
                objHrCommon.CurrentPage = HRLeaveAppCanPaging.CurrentPage;
                int? Month = null;
                int? Year = null;
                //if (ddlMonthRej.SelectedIndex != 0)
                Month = Convert.ToInt32(ddlCanceledMnth.SelectedValue);
                // if (ddlYearRej.SelectedIndex != 0)
                Year = Convert.ToInt32(ddlyearCanceled.SelectedValue);
                DataSet ds = AttendanceDAC.HR_GetLeaveCanceledByPaging(objHrCommon, Month, Year, Convert.ToInt32(Session["CompanyID"]), Empid, EDeptid, EWsid);
                if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0)
                {
                    gvCanceled.DataSource = ds;
                    HRLeaveAppCanPaging.Bind(objHrCommon.CurrentPage, objHrCommon.TotalPages, objHrCommon.NoofRecords, objHrCommon.PageSize);
                }
                else
                {
                    gvCanceled.EmptyDataText = "No Records Found";
                    HRLeaveAppCanPaging.Visible = false;
                }
                gvCanceled.DataBind();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        void BindGrantedLeaves(HRCommon objHrCommon)
        {
            try
            {
                objHrCommon.PageSize = HRLeaveAppGrantedPaging.ShowRows;
                objHrCommon.CurrentPage = HRLeaveAppGrantedPaging.CurrentPage;
                int? Month = null;
                int? Year = null;
                //if (ddlMonthGrant.SelectedIndex != 0)
                Month = Convert.ToInt32(ddlMonthGrant.SelectedValue);
                //if (ddlYearGrant.SelectedIndex != 0)
                Year = Convert.ToInt32(ddlYearGrant.SelectedValue);
                DataSet ds = HR_GetGrantedLeavesByPagingV1(objHrCommon, Month, Year, Convert.ToInt32(Session["CompanyID"]), Empid, EDeptid, EWsid);
                if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0)
                {
                    gvGranted.DataSource = ds;
                    HRLeaveAppGrantedPaging.Bind(objHrCommon.CurrentPage, objHrCommon.TotalPages, objHrCommon.NoofRecords, objHrCommon.PageSize);
                }
                else
                {
                    gvGranted.EmptyDataText = "No Records Found";
                    HRLeaveAppGrantedPaging.Visible = false;
                }
                gvGranted.DataBind();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public DataSet HR_GetGrantedLeavesByPagingV1(HRCommon objHrCommon, int? Month, int? Year, int CompanyID, int Empid, int Deptid, int Wsid)
        {
            int Userids = Convert.ToInt32(Session["UserId"].ToString());
            SqlParameter[] sqlParams = new SqlParameter[11];
            sqlParams[0] = new SqlParameter("@CurrentPage", objHrCommon.CurrentPage);
            sqlParams[1] = new SqlParameter("@PageSize", objHrCommon.PageSize);
            sqlParams[2] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
            sqlParams[2].Direction = ParameterDirection.ReturnValue;
            sqlParams[3] = new SqlParameter("@NoofRecords", System.Data.SqlDbType.Int);
            sqlParams[3].Direction = ParameterDirection.Output;
            sqlParams[4] = new SqlParameter("@Month", Month);
            sqlParams[5] = new SqlParameter("@Year", Year);
            sqlParams[6] = new SqlParameter("@CompanyID", CompanyID);
            sqlParams[7] = new SqlParameter("@Empid", Empid);
            sqlParams[8] = new SqlParameter("@DeptNo", Deptid);
            sqlParams[9] = new SqlParameter("@Wsid", Wsid);
            sqlParams[10] = new SqlParameter("@UserId", Userid);
            DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetGrantedLeavesByPaging", sqlParams);
            objHrCommon.NoofRecords = (int)sqlParams[3].Value;
            objHrCommon.TotalPages = (int)sqlParams[2].Value;
            return ds;
        }
        public void BindYears()
        {
            DataSet ds = AttendanceDAC.GetCalenderYear();
            int i = 0;
            int Maxyear = Convert.ToInt32(ds.Tables[0].Rows[0]["MaxYear"].ToString());
            for (int Minyear = Convert.ToInt32(ds.Tables[0].Rows[0]["MinYear"].ToString()); Minyear <= Maxyear; Minyear++)
            {
                if (Convert.ToInt32(Request.QueryString["key"]) == 1)
                {
                    ddlYearGrant.Items.Insert(0, new ListItem(Convert.ToString(Minyear), Convert.ToString(Minyear)));
                    i = i + 1;
                }
                else if (Convert.ToInt32(Request.QueryString["key"]) == 2)
                {
                    ddlYearRej.Items.Insert(0, new ListItem(Convert.ToString(Minyear), Convert.ToString(Minyear)));
                    i = i + 1;
                }
                else if (Convert.ToInt32(Request.QueryString["key"]) == 6)
                {
                    ddlyearCanceled.Items.Insert(0, new ListItem(Convert.ToString(Minyear), Convert.ToString(Minyear)));
                    i = i + 1;
                }
                else
                {
                    ddlYear.Items.Insert(0, new ListItem(Convert.ToString(Minyear), Convert.ToString(Minyear)));
                    i = i + 1;
                }
                if (Convert.ToInt32(Request.QueryString["key"]) == 1)
                {
                    ddlMonthGrant.SelectedValue = ds.Tables[0].Rows[0]["CurrentMonth"].ToString();
                    ddlYearGrant.SelectedValue = ds.Tables[0].Rows[0]["CurrentYear"].ToString();
                }
                else if (Convert.ToInt32(Request.QueryString["key"]) == 2)
                {
                    ddlMonthRej.SelectedValue = ds.Tables[0].Rows[0]["CurrentMonth"].ToString();
                    ddlYearRej.SelectedValue = ds.Tables[0].Rows[0]["CurrentYear"].ToString();
                }
                else if (Convert.ToInt32(Request.QueryString["key"]) == 6)
                {
                    ddlCanceledMnth.SelectedValue = ds.Tables[0].Rows[0]["CurrentMonth"].ToString();
                    ddlyearCanceled.SelectedValue = ds.Tables[0].Rows[0]["CurrentYear"].ToString();
                }
                else
                {
                    ddlMonth.SelectedValue = "0";//ds.Tables[0].Rows[0]["CurrentMonth"].ToString();
                    ddlYear.SelectedValue = ds.Tables[0].Rows[0]["CurrentYear"].ToString();
                }
            }
        }
        protected void btnSubmitGrant_Click(object sender, EventArgs e)
        {
            try
            {
                HRLeaveAppGrantedPaging.CurrentPage = 1;
                BindPager();
            }
            catch (Exception ex)
            {
                clsErrorLog.HMSEventLog(ex, "HRLeaveApplication", "btnSubmitGrant_Click", "002");
            }
        }
        protected void btnSubmitRej_Click(object sender, EventArgs e)
        {
            try
            {
                HRLeaveAppRejPaging.CurrentPage = 1;
                BindPager();
            }
            catch (Exception ex)
            {
                clsErrorLog.HMSEventLog(ex, "HRLeaveApplication", "btnSubmitRej_Click", "003");
            }
        }
        void BindLeaveDetails(HRCommon objHrCommon)
        {
            try
            {
                objHrCommon.PageSize = HRLeaveAppInprocessPaging.ShowRows;
                objHrCommon.CurrentPage = HRLeaveAppInprocessPaging.CurrentPage;
                int? Month = null;
                int? Year = null;
                int? status = null;
                //if (ddlMonth.SelectedValue != "0")
                Month = Convert.ToInt32(ddlMonth.SelectedValue);
                //if (ddlYear.SelectedValue != "0")
                Year = Convert.ToInt32(ddlYear.SelectedValue);
                if (Request.QueryString.Count > 0)
                {
                    int key = 0;
                    key = Convert.ToInt32(Request.QueryString["key"]);
                    if (key == 3)
                    {
                        status = 4;
                    }
                    if (key == 4)
                    {
                        status = 5;
                    }
                    if (key == 8)
                    {
                        status = 8;
                    }
                    if (key == 7)
                    {
                        status = 7;
                    }
                }
                DataSet ds = HR_GetLeaveAppsToHRByPagingV1(objHrCommon, Month, Year, Convert.ToInt32(Session["CompanyID"]), Empid, EDeptid, EWsid, status);//,0,0,0);
                if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0)
                {
                    gvLeaveApptoHr.DataSource = ds;
                    HRLeaveAppInprocessPaging.Bind(objHrCommon.CurrentPage, objHrCommon.TotalPages, objHrCommon.NoofRecords, objHrCommon.PageSize);
                }
                else
                {
                    gvLeaveApptoHr.EmptyDataText = "No Records Found";
                    HRLeaveAppInprocessPaging.Visible = false;
                }
                gvLeaveApptoHr.DataBind();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public DataSet HR_GetLeaveAppsToHRByPagingV1(HRCommon objHrCommon, int? Month, int? Year, int CompanyID, int Empid, int Deptid, int Wsid, int? status)
        {
            int Userids = Convert.ToInt32(Session["UserId"].ToString());
            SqlParameter[] sqlParams = new SqlParameter[12];
            sqlParams[0] = new SqlParameter("@CurrentPage", objHrCommon.CurrentPage);
            sqlParams[1] = new SqlParameter("@PageSize", objHrCommon.PageSize);
            sqlParams[2] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
            sqlParams[2].Direction = ParameterDirection.ReturnValue;
            sqlParams[3] = new SqlParameter("@NoofRecords", System.Data.SqlDbType.Int);
            sqlParams[3].Direction = ParameterDirection.Output;
            sqlParams[4] = new SqlParameter("@Month", Month);
            sqlParams[5] = new SqlParameter("@Year", Year);
            sqlParams[6] = new SqlParameter("@CompanyID", CompanyID);
            sqlParams[7] = new SqlParameter("@Empid", Empid);
            sqlParams[8] = new SqlParameter("@DeptNo", Deptid);
            sqlParams[9] = new SqlParameter("@Wsid", Wsid);
            sqlParams[10] = new SqlParameter("@status", status);
            sqlParams[11] = new SqlParameter("@UserId", Userids);
            DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetLeaveAppsToHRByPaging", sqlParams);
            objHrCommon.NoofRecords = (int)sqlParams[3].Value;
            objHrCommon.TotalPages = (int)sqlParams[2].Value;
            return ds;
        }
        public string DocNavigateUrlnew(string ProofID, string Lid)
        {
            string ReturnVal = "";
            string[] str;
            string ss = "";
            if (ProofID != string.Empty)
            {
                if (ProofID.Contains('.'))
                {
                    str = ProofID.Split('.');
                    ss = Lid + '.' + str[1];
                }
            }
            if (ss == ProofID)
                ReturnVal = "~/hms/LeaveApplications/" + ProofID;
            else
                ReturnVal = ProofID;
            return ReturnVal;
        }
        public bool Visble(string Ext)
        {
            if (Ext != "")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                HRLeaveAppInprocessPaging.CurrentPage = 1;
                BindPager();
            }
            catch (Exception ex)
            {
                clsErrorLog.HMSEventLog(ex, "HRLeaveApplication", "btnSubmit_Click", "004");
            }
        }
        protected string FormatInput(object Status)
        {
            string retValue = "";
            string input = Status.ToString();
            if (input == "-1")
            {
                retValue = "--SELECT--";
            }
            if (input == "0")
            {
                retValue = "Applied";
            }
            if (input == "1")
            {
                retValue = "In-Process";
            }
            if (input == "2")
            {
                retValue = "Granted";
            }
            if (input == "3")
            {
                retValue = "Rejected";
            }
            return retValue;
        }
        public void Leave_Updstatus(int LID, int Status)
        {
            SqlParameter[] objParam = new SqlParameter[4];
            objParam[0] = new SqlParameter("@LID", LID);
            objParam[1] = new SqlParameter("@Status", Status);
            objParam[2] = new SqlParameter("@GrantedBy", Session["UserId"]);
            objParam[3] = new SqlParameter("@Grantedon", DateTime.Now);
            SQLDBUtil.ExecuteNonQuery("sh_UpdLeavestatus", objParam);
            if (Status == 4 || Status == 5 || Status == 7 || Status == 8)
            {
                AlertMsg.MsgBox(Page, "Approved !");
            }
            else if (Status == 3)
            {
                AlertMsg.MsgBox(Page, "Rejected !");
            }
        }
        protected void gvLeaveApptoHr_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                ViewState["IsCFrwd"] = false;
                if (e.CommandName == "Apprv")
                {
                    int LID = Convert.ToInt32(e.CommandArgument);
                    int key = 0;
                    if (Request.QueryString.Count > 0)
                    {
                        key = Convert.ToInt32(Request.QueryString["key"]);
                        if (key == 3)
                        {
                            Leave_Updstatus(LID, 7);
                        }
                        if (key == 8)
                        {
                            Leave_Updstatus(LID, 5);
                        }
                        if (key == 7)
                        {
                            Leave_Updstatus(LID, 8);
                        }                        
                    }
                    else
                    {
                        Leave_Updstatus(LID, 4);
                    }
                    BindPager();
                }
                else if (e.CommandName == "Rejct")
                {
                    int LID = Convert.ToInt32(e.CommandArgument);
                    Leave_Updstatus(LID, 3);
                    BindPager();
                }
                else if (e.CommandName == "edt")
                {
                    int LID = Convert.ToInt32(e.CommandArgument);
                    int EmpID = Convert.ToInt32(Session["UserId"]);
                    ViewState["LID"] = LID;
                    DataSet ds = AttendanceDAC.HR_GetLeaveDetailsByID(LID);
                    try
                    {
                        txtGrantedFrom.Text = CodeUtilHMS.ConvertToDate_ddMMMyyy(ds.Tables[0].Rows[0]["GrantedFrom"].ToString(), CodeUtilHMS.DateFormat.DayMonthYear).ToString("dd MMM yyyy");
                        // Convert.ToDateTime(ds.Tables[0].Rows[0]["GrantedFrom"]).ToString("dd MMM yyyy"); 
                    }
                    catch { txtGrantedFrom.Text = ""; }
                    // ddlEmp.SelectedValue = ds.Tables[0].Rows[0]["AssignerEmpID"].ToString() == "" ? "0" : ds.Tables[0].Rows[0]["AssignerEmpID"].ToString();
                    try
                    {
                        txtGrantedUntil.Text = CodeUtilHMS.ConvertToDate_ddMMMyyy(ds.Tables[0].Rows[0]["GrantedUntil"].ToString(), CodeUtilHMS.DateFormat.DayMonthYear).ToString("dd MMM yyyy");
                        //Convert.ToDateTime(ds.Tables[0].Rows[0]["GrantedUntil"]).ToString("dd MMM yyyy"); 
                    }
                    catch { txtGrantedUntil.Text = ""; }
                    lblLevFrom.Text = CodeUtilHMS.ConvertToDate_ddMMMyyy(ds.Tables[0].Rows[0]["LeaveFrom"].ToString(), CodeUtilHMS.DateFormat.DayMonthYear).ToString("dd MMM yyyy");
                    //ds.Tables[0].Rows[0]["LeaveFrom"].ToString();//Convert.ToDateTime(ds.Tables[0].Rows[0]["LeaveFrom"]).ToString(ConfigurationManager.AppSettings["DateFormat"]);
                    lblLevUntil.Text = CodeUtilHMS.ConvertToDate_ddMMMyyy(ds.Tables[0].Rows[0]["LeaveUntil"].ToString(), CodeUtilHMS.DateFormat.DayMonthYear).ToString("dd MMM yyyy");
                    //ds.Tables[0].Rows[0]["LeaveUntil"].ToString();//Convert.ToDateTime(ds.Tables[0].Rows[0]["LeaveUntil"]).ToString(ConfigurationManager.AppSettings["DateFormat"]);
                    lblReason.Text = ds.Tables[0].Rows[0]["Reason"].ToString();
                    lblEmpID.Text = ds.Tables[0].Rows[0]["EmpID"].ToString();
                    lblName.Text = ds.Tables[0].Rows[0]["name"].ToString();
                    lblAppliedOn.Text = CodeUtilHMS.ConvertToDate_ddMMMyyy(ds.Tables[0].Rows[0]["AppliedOn"].ToString(), CodeUtilHMS.DateFormat.DayMonthYear).ToString("dd MMM yyyy");
                    //ds.Tables[0].Rows[0]["AppliedOn"].ToString();
                    lblempreply.Text = ds.Tables[0].Rows[0]["CommentReply"].ToString();
                    if (ds.Tables[0].Rows[0]["GrantedFrom"].ToString().Trim() == "")
                        txtGrantedFrom.Text = CodeUtilHMS.ConvertToDate_ddMMMyyy(ds.Tables[0].Rows[0]["LeaveFrom"].ToString(), CodeUtilHMS.DateFormat.DayMonthYear).ToString("dd MMM yyyy");
                    else
                        txtGrantedFrom.Text = CodeUtilHMS.ConvertToDate_ddMMMyyy(ds.Tables[0].Rows[0]["GrantedFrom"].ToString(), CodeUtilHMS.DateFormat.DayMonthYear).ToString("dd MMM yyyy");
                    if (ds.Tables[0].Rows[0]["GrantedUntil"].ToString().Trim() == "")
                        txtGrantedUntil.Text = CodeUtilHMS.ConvertToDate_ddMMMyyy(ds.Tables[0].Rows[0]["LeaveUntil"].ToString(), CodeUtilHMS.DateFormat.DayMonthYear).ToString("dd MMM yyyy");
                    else
                        txtGrantedUntil.Text = CodeUtilHMS.ConvertToDate_ddMMMyyy(ds.Tables[0].Rows[0]["GrantedUntil"].ToString(), CodeUtilHMS.DateFormat.DayMonthYear).ToString("dd MMM yyyy");
                    lblLeaveType.Text = ds.Tables[0].Rows[0]["LeaveType"].ToString();
                    lblDateofJoin.Text = ds.Tables[0].Rows[0]["DateOfJoin"].ToString();
                    lblAvailable.Text = ds.Tables[0].Rows[0]["TotLeave"].ToString();
                    // added here by pratap date:23-04-2016
                    string Path;
                    if (ds.Tables[0].Rows[0]["Proof"].ToString() != "")
                    {
                        //Path = "../LeaveApplications/" +  Convert.ToInt32(Session["UserId"]) + "/" + ds.Tables[0].Rows[0]["Proof"];                        
                        Path = "../LeaveApplications/" + Convert.ToInt32(lblEmpID.Text) + "/" + ds.Tables[0].Rows[0]["Proof"];
                    }
                    else
                    {
                        Path = "";
                        lblProof.Text = "No proof avaliable.";
                    }
                    hlnkProof.HRef = DocNavigateUrl(Path);
                    DateTime LeaveFrom = CodeUtilHMS.ConvertToDate_ddMMMyyy(lblLevFrom.Text.Trim(), CodeUtilHMS.DateFormat.ddMMMyyyy);
                    DateTime LeaveUntil = CodeUtilHMS.ConvertToDate_ddMMMyyy(lblLevUntil.Text.Trim(), CodeUtilHMS.DateFormat.ddMMMyyyy);
                    TimeSpan span = LeaveUntil.Subtract(LeaveFrom);
                    int totdays = span.Days + 1;
                    lblnoofdays.Text = totdays.ToString();
                    DateTime From = CodeUtilHMS.ConvertToDate_ddMMMyyy(txtGrantedFrom.Text.Trim(), CodeUtilHMS.DateFormat.ddMMMyyyy);
                    DateTime Until = CodeUtilHMS.ConvertToDate_ddMMMyyy(txtGrantedUntil.Text.Trim(), CodeUtilHMS.DateFormat.ddMMMyyyy);
                    TimeSpan SPAN = Until.Subtract(From);
                    int TOTdays = SPAN.Days + 1;
                    lblGrantedDays.Text = TOTdays.ToString();
                    if (TOTdays > totdays)
                    {
                        btnsave.Enabled = false;
                        AlertMsg.MsgBox(this.Page, "Please Check GrantedDays", AlertMsg.MessageType.Warning);
                    }
                    txtComment.Text = ds.Tables[0].Rows[0]["Comment"].ToString();
                    //  ddlStatus.SelectedValue = ds.Tables[0].Rows[0]["Status"].ToString();
                    ViewState["Status"] = ds.Tables[0].Rows[0]["Status"].ToString();
                    tblHR.Visible = false;
                    tblHREdit.Visible = true;
                    tblRejected.Visible = false;
                    tblCanceled.Visible = false;
                    lnkCloseCal.Visible = false;
                    SqlParameter[] sqlParams = new SqlParameter[1];
                    sqlParams[0] = new SqlParameter("@lid", LID);
                    DataSet dscmnts = SQLDBUtil.ExecuteDataset("sh_GetLeaveApps_Cmnts", sqlParams);
                    if (dscmnts != null && dscmnts.Tables.Count != 0 && dscmnts.Tables[0].Rows.Count > 0)
                    {
                        gvCmnts.DataSource = dscmnts;
                    }
                    else
                        gvCmnts.DataSource = null;
                    gvCmnts.DataBind();
                    try
                    {
                        //Boolean IsCFrwd = false;
                        ViewState["IsCFrwd"] = false;
                        txtOpenningBal.Text = "0.00";
                        ViewState["LevTypID"] = Convert.ToInt32(ds.Tables[0].Rows[0]["LevID"]);
                        divLeaves.Visible = false;
                        sqlParams = new SqlParameter[2];
                        sqlParams[0] = new SqlParameter("@LID", Convert.ToInt32(ds.Tables[0].Rows[0]["LevID"]));
                        sqlParams[1] = new SqlParameter("@EMPID", Convert.ToInt32(ds.Tables[0].Rows[0]["EmpID"]));
                        DataSet dsleavechek = SQLDBUtil.ExecuteDataset("HR_GetLeaveTyid", sqlParams);
                        if (dsleavechek != null && dsleavechek.Tables.Count != 0 && dsleavechek.Tables[0].Rows.Count > 0)
                        {
                            ViewState["IsCFrwd"] = Convert.ToBoolean(dsleavechek.Tables[0].Rows[0]["IsCFrwd"]);
                            txtOpenningBal.Text = dsleavechek.Tables[0].Rows[0]["Cr"].ToString();
                            divLeaves.Visible = false;
                            if (Convert.ToInt32(ddlStatus.SelectedValue) == 2)
                                if (Convert.ToBoolean(ViewState["IsCFrwd"]) == true)
                                    divLeaves.Visible = true;
                        }
                    }
                    catch { }
                }
            }
            catch (Exception ex)
            {
                clsErrorLog.HMSEventLog(ex, "HRLeaveApplication", "gvLeaveApptoHr_RowCommand", "005");
            }
        }
        protected void btnsave_Click(object sender, EventArgs e)
        {
            try
            {
                int Status = Convert.ToInt32(ddlStatus.SelectedValue);
                if (Status == 2 && txtGrantedFrom.Text == string.Empty && txtGrantedUntil.Text == string.Empty)
                {
                    AlertMsg.MsgBox(this.Page, "Plese Enter Granted From and Granted Until dates!", AlertMsg.MessageType.Warning);
                }
                else
                {
                    int LID = Convert.ToInt32(ViewState["LID"]);
                    int GrantedBy = Convert.ToInt32(Session["UserId"]);
                    DateTime? GrantedFrom;
                    DateTime? GrantedUntil;
                    if (Status == 1 || Status == 0)
                    {
                        if (txtGrantedFrom.Text == string.Empty && txtGrantedUntil.Text == string.Empty)
                        {
                            GrantedFrom = null;
                            GrantedUntil = null;
                        }
                        else
                        {
                            GrantedFrom = CodeUtilHMS.ConvertToDate_ddMMMyyy(txtGrantedFrom.Text.Trim(), CodeUtilHMS.DateFormat.ddMMMyyyy);
                            GrantedUntil = CodeUtilHMS.ConvertToDate_ddMMMyyy(txtGrantedUntil.Text.Trim(), CodeUtilHMS.DateFormat.ddMMMyyyy);
                        }
                    }
                    else
                    {
                        GrantedFrom = CodeUtilHMS.ConvertToDate_ddMMMyyy(txtGrantedFrom.Text.Trim(), CodeUtilHMS.DateFormat.ddMMMyyyy);
                        GrantedUntil = CodeUtilHMS.ConvertToDate_ddMMMyyy(txtGrantedUntil.Text.Trim(), CodeUtilHMS.DateFormat.ddMMMyyyy);
                    }
                    DateTime? GrantedOn;
                    if (GrantedFrom <= GrantedUntil)
                    {
                        if (Status == 2 || Status == 3)
                        {
                            GrantedOn = DateTime.Now;
                        }
                        else
                        {
                            GrantedOn = null;
                        }
                        string Comment = txtComment.Text;
                        //changed the SP from HR_InsUpHrPermission to HR_InsUpHrPermission_Tooverrite_Wo for Overriding the WO with applied Leave (Nookesh/20/04/2016)
                        AttendanceDAC.HR_InsUpHrPermission_Tooverrite_Wo(LID, Status, Comment, GrantedFrom, GrantedUntil, GrantedBy, GrantedOn);
                        tblHR.Visible = true;
                        if (Status == 2)
                        {
                            AlertMsg.MsgBox(Page, "Granted..!", AlertMsg.MessageType.Success);
                        }
                        else if (Status == 3)
                        {
                            AlertMsg.MsgBox(Page, "Rejected..!", AlertMsg.MessageType.Success);
                        }
                        tblHREdit.Visible = false;
                        tblRejected.Visible = false;
                        BindYears();
                        BindPager();
                        //BindLeaveDetails();
                    }
                    else
                    {
                        AlertMsg.MsgBox(Page, "Check Dates !", AlertMsg.MessageType.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                clsErrorLog.HMSEventLog(ex, "HRLeaveApplication", "btnSave_Click", "006");
            }
        }
        protected void gvGranted_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Cncel")
                {
                    GridViewRow gvr = (GridViewRow)(((Control)e.CommandSource).NamingContainer);
                    FileUpload UploadProof1 = (FileUpload)gvr.Cells[14].FindControl("UploadProof");
                    UploadProof1.Attributes.Clear();
                }
                if (e.CommandName == "Upld")
                {
                    GridViewRow gvr = (GridViewRow)(((Control)e.CommandSource).NamingContainer);
                    FileUpload UploadProof1 = (FileUpload)gvr.Cells[14].FindControl("UploadProof");
                    Label lblLID = (Label)gvr.FindControl("lblLID");
                    string fileName = Path.GetFileName(UploadProof1.PostedFile.FileName);
                    string fileExtension = Path.GetExtension(UploadProof1.PostedFile.FileName);
                    //first check if "uploads" folder exist or not, if not create it
                    string fileSavePath = Server.MapPath("uploads");
                    if (!Directory.Exists(fileSavePath))
                        Directory.CreateDirectory(fileSavePath);
                    //after checking or creating folder it's time to save the file
                    fileSavePath = fileSavePath + "\\" + fileName;
                    UploadProof1.PostedFile.SaveAs(fileSavePath);
                    FileInfo fileInfo = new FileInfo(fileSavePath);
                    string contentType = UploadProof1.PostedFile.ContentType;
                    string ext = Path.GetExtension(fileName);
                    string type = String.Empty;
                    if (UploadProof1.HasFile)
                    {
                        try
                        {
                            switch (ext) // this switch code validate the files which allow to upload only PDF file   
                            {
                                case ".pdf":
                                    type = "application/pdf";
                                    break;
                                case ".jpg":
                                    type = "application/img";
                                    break;
                                case ".jpeg":
                                    type = "application/img";
                                    break;
                                case ".png":
                                    type = "application/img";
                                    break;
                                case ".bmp":
                                    type = "application/img";
                                    break;
                            }
                            if (type != String.Empty)
                            {
                                using (Stream fs = UploadProof1.PostedFile.InputStream)
                                {
                                    using (BinaryReader br = new BinaryReader(fs))
                                    {
                                        byte[] bytes = br.ReadBytes((Int32)fs.Length);
                                        using (SqlConnection con = new SqlConnection(connectionString))
                                        {
                                            SqlCommand cmd = new SqlCommand("HR_UploadLeaveApplication", con);
                                            cmd.Connection = con;
                                            cmd.CommandType = CommandType.StoredProcedure;
                                            cmd.Parameters.AddWithValue("@LID", lblLID.Text);
                                            cmd.Parameters.AddWithValue("@FileName", fileName);
                                            cmd.Parameters.AddWithValue("@FileSize", fileInfo.Length.ToString());
                                            cmd.Parameters.AddWithValue("@FileExtension", fileExtension);
                                            cmd.Parameters.AddWithValue("@FilePath", fileSavePath);
                                            con.Open();
                                            cmd.ExecuteNonQuery();
                                            con.Close();
                                            AlertMsg.MsgBox(Page, "File has been uploaded Successfully..!", AlertMsg.MessageType.Success);
                                            return;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                // if file is other than speified extension  
                                AlertMsg.MsgBox(Page, "Select Only PDF or Image Files..!", AlertMsg.MessageType.Warning);
                                return;
                            }
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                    }
                    if (!UploadProof1.HasFile)
                    {
                        //if file uploader has no file selected  
                        AlertMsg.MsgBox(Page, "Please Select File..!", AlertMsg.MessageType.Warning);
                        return;
                    }
                }
                if (e.CommandName == "edt")
                {
                    int LID = Convert.ToInt32(e.CommandArgument);
                    int EmpID = Convert.ToInt32(Session["UserId"]);
                    ViewState["LID"] = LID;
                    DataSet ds = AttendanceDAC.HR_GetLeaveDetailsByID(LID);
                    try { txtGrantedFrom.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["GrantedFrom"]).ToString("dd MMM yyyy"); }
                    catch { txtGrantedFrom.Text = ""; }
                    // ddlEmp.SelectedValue = ds.Tables[0].Rows[0]["AssignerEmpID"].ToString() == "" ? "0" : ds.Tables[0].Rows[0]["AssignerEmpID"].ToString();
                    try { txtGrantedUntil.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["GrantedUntil"]).ToString("dd MMM yyyy"); }
                    catch { txtGrantedUntil.Text = ""; }
                    lblLevFrom.Text = ds.Tables[0].Rows[0]["LeaveFrom"].ToString();//Convert.ToDateTime(ds.Tables[0].Rows[0]["LeaveFrom"]).ToString(ConfigurationManager.AppSettings["DateFormat"]);
                    lblLevUntil.Text = ds.Tables[0].Rows[0]["LeaveUntil"].ToString();//Convert.ToDateTime(ds.Tables[0].Rows[0]["LeaveUntil"]).ToString(ConfigurationManager.AppSettings["DateFormat"]);
                    lblReason.Text = ds.Tables[0].Rows[0]["Reason"].ToString();
                    lblEmpID.Text = ds.Tables[0].Rows[0]["EmpID"].ToString();
                    lblName.Text = ds.Tables[0].Rows[0]["name"].ToString();
                    lblAppliedOn.Text = ds.Tables[0].Rows[0]["AppliedOn"].ToString();
                    lblempreply.Text = ds.Tables[0].Rows[0]["CommentReply"].ToString();
                    txtGrantedFrom.Text = ds.Tables[0].Rows[0]["GrantedFrom"].ToString();
                    txtGrantedUntil.Text = ds.Tables[0].Rows[0]["GrantedUntil"].ToString();
                    lblLeaveType.Text = ds.Tables[0].Rows[0]["LeaveType"].ToString();
                    lblDateofJoin.Text = ds.Tables[0].Rows[0]["DateOfJoin"].ToString();
                    lblAvailable.Text = ds.Tables[0].Rows[0]["TotLeave"].ToString();
                    DateTime LeaveFrom = CodeUtilHMS.ConvertToDate_ddMMMyyy(lblLevFrom.Text.Trim(), CodeUtilHMS.DateFormat.DayMonthYear);
                    DateTime LeaveUntil = CodeUtilHMS.ConvertToDate_ddMMMyyy(lblLevUntil.Text.Trim(), CodeUtilHMS.DateFormat.DayMonthYear);
                    TimeSpan span = LeaveUntil.Subtract(LeaveFrom);
                    int totdays = span.Days + 1;
                    lblnoofdays.Text = totdays.ToString();
                    DateTime From = CodeUtilHMS.ConvertToDate_ddMMMyyy(txtGrantedFrom.Text.Trim(), CodeUtilHMS.DateFormat.DayMonthYear);
                    DateTime Until = CodeUtilHMS.ConvertToDate_ddMMMyyy(txtGrantedUntil.Text.Trim(), CodeUtilHMS.DateFormat.DayMonthYear);
                    TimeSpan SPAN = Until.Subtract(From);
                    int TOTdays = SPAN.Days + 1;
                    string Path;
                    if (ds.Tables[0].Rows[0]["Proof"].ToString() != "")
                    {
                        //Path = "../HMS/LeaveApplications/" +  Convert.ToInt32(Session["UserId"]) + "/" + ds.Tables[0].Rows[0]["Proof"];
                        //Path = "../LeaveApplications/" +  Convert.ToInt32(Session["UserId"]) + "/" + ds.Tables[0].Rows[0]["Proof"];
                        // here change the session of userid to empid by pratap date: 23-04-2016
                        Path = "../LeaveApplications/" + Convert.ToInt32(lblEmpID.Text) + "/" + ds.Tables[0].Rows[0]["Proof"];
                        //in local the above path is not working so we can coment that thing added new path Ravitheja on 25-03-2016
                    }
                    else
                    {
                        Path = "";
                        lblProof.Text = "No proof avaliable.";
                    }
                    hlnkProof.HRef = DocNavigateUrl(Path);
                    lblGrantedDays.Text = TOTdays.ToString();
                    if (TOTdays > totdays)
                    {
                        btnsave.Enabled = false;
                        AlertMsg.MsgBox(this.Page, "Please Check GrantedDays", AlertMsg.MessageType.Warning);
                    }
                    txtComment.Text = ds.Tables[0].Rows[0]["Comment"].ToString();
                    ddlStatus.SelectedValue = ds.Tables[0].Rows[0]["Status"].ToString();
                    ViewState["Status"] = ds.Tables[0].Rows[0]["Status"].ToString();
                    tblHR.Visible = false;
                    tblHREdit.Visible = true;
                    tblRejected.Visible = false;
                    tblGrantedLeaves.Visible = false;
                    lnkCloseCal.Visible = false;                    
                }
                if (e.CommandName == "can")
                {
                    int LID = Convert.ToInt32(e.CommandArgument);
                    SqlParameter[] objParam = new SqlParameter[3];
                    objParam[0] = new SqlParameter("@LID", LID);
                    objParam[1] = new SqlParameter("@Status", 6);
                    objParam[2] = new SqlParameter("@Userid", Convert.ToInt32(Session["UserId"]));
                    SQLDBUtil.ExecuteNonQuery("sh_CancelApprovedLeave", objParam);
                    AlertMsg.MsgBox(this.Page, "Leave Canceled !");
                    BindPager();
                }
                if (e.CommandName == "VS")
                {
                    int EMPID = Convert.ToInt32(e.CommandArgument);
                    GridViewRow gvr = (GridViewRow)(((Control)e.CommandSource).NamingContainer);
                    Label lblLID = (Label)gvr.FindControl("lblLID");
                    SqlParameter[] sqlParams22 = new SqlParameter[1];
                    sqlParams22[0] = new SqlParameter("@empid", EMPID);
                    DataSet ds22 = SQLDBUtil.ExecuteDataset("sh_previousmonthsalarystatus", sqlParams22);
                    if (ds22 != null && ds22.Tables.Count > 0 && ds22.Tables[0].Rows.Count > 0 && ds22.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        SqlParameter[] sqlParams2 = new SqlParameter[1];
                        sqlParams2[0] = new SqlParameter("@empid", EMPID);
                        DataSet ds2 = SQLDBUtil.ExecuteDataset("sh_openingbalancecheckingleaveaccount", sqlParams2);
                        if (ds2 != null && ds2.Tables.Count > 0 && ds2.Tables[0].Rows.Count > 0)
                        {
                            SqlParameter[] SP = new SqlParameter[1];
                            SP[0] = new SqlParameter("@empid", Convert.ToInt32(EMPID));
                            DataSet dss1 = SQLDBUtil.ExecuteDataset("sh_EmpExistsinSederFileds", SP);
                            if (dss1.Tables[0].Rows.Count > 0)
                            {
                                try
                                {
                                    try
                                    {
                                        SqlParameter[] param = new SqlParameter[3];
                                        param[0] = new SqlParameter("@EmpID", EMPID);
                                        param[1] = new SqlParameter("@CompanyID", CompanyID);
                                        param[2] = new SqlParameter("@LID", lblLID.Text);
                                        DataSet ds = SQLDBUtil.ExecuteDataset("sh_VacationSettlementEmpInfo", param);
                                        if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0)
                                        {
                                            Response.Redirect("VacationSettlementRev4.aspx?key=1&id=0&Empid=" + EMPID.ToString() + "&LID=" + lblLID.Text);
                                        }
                                        else
                                        {
                                            AlertMsg.MsgBox(Page, "NO future granted leave to consider for Settlement Make sure the employee has approved leave to process", AlertMsg.MessageType.Warning);
                                        }
                                    }
                                    catch (Exception)
                                    {
                                        //  throw;
                                    }
                                }
                                catch (Exception)
                                {
                                    //throw;
                                }
                            }
                            else
                            {
                                AlertMsg.MsgBox(Page, "No LVRD. Please enter LVRD", AlertMsg.MessageType.Warning);
                            }
                        }
                        else
                        {
                            AlertMsg.MsgBox(Page, "Please Check Leave Account", AlertMsg.MessageType.Warning);
                        }
                    }
                    else
                    {
                        AlertMsg.MsgBox(Page, "Previous month salary not calculated to this employee", AlertMsg.MessageType.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                clsErrorLog.HMSEventLog(ex, "HRLeaveApplication", "gvGranted_RowCommand", "007");
            }
        }
        public string DocNavigateUrl(string Proof)
        {
            if (Proof != "")
                return Proof;
            else
                return null;
        }
        protected void gvRejected_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "edt")
                {
                    int LID = Convert.ToInt32(e.CommandArgument);
                    int EmpID = Convert.ToInt32(Session["UserId"]);
                    ViewState["LID"] = LID;
                    DataSet ds = AttendanceDAC.HR_GetLeaveDetailsByID(LID);
                    try { txtGrantedFrom.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["GrantedFrom"]).ToString("dd MMM yyyy"); }//ConfigurationManager.AppSettings["DateFormat"]}
                    catch { txtGrantedFrom.Text = ""; }
                    // ddlEmp.SelectedValue = ds.Tables[0].Rows[0]["AssignerEmpID"].ToString() == "" ? "0" : ds.Tables[0].Rows[0]["AssignerEmpID"].ToString();
                    try { txtGrantedUntil.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["GrantedUntil"]).ToString("dd MMM yyyy"); }
                    catch { txtGrantedUntil.Text = ""; }
                    lblLevFrom.Text = ds.Tables[0].Rows[0]["LeaveFrom"].ToString();//Convert.ToDateTime(ds.Tables[0].Rows[0]["LeaveFrom"]).ToString(ConfigurationManager.AppSettings["DateFormat"]);
                    lblLevUntil.Text = ds.Tables[0].Rows[0]["LeaveUntil"].ToString();//Convert.ToDateTime(ds.Tables[0].Rows[0]["LeaveUntil"]).ToString(ConfigurationManager.AppSettings["DateFormat"]);
                    lblReason.Text = ds.Tables[0].Rows[0]["Reason"].ToString();
                    lblEmpID.Text = ds.Tables[0].Rows[0]["EmpID"].ToString();
                    lblName.Text = ds.Tables[0].Rows[0]["name"].ToString();
                    lblAppliedOn.Text = ds.Tables[0].Rows[0]["AppliedOn"].ToString();
                    lblempreply.Text = ds.Tables[0].Rows[0]["CommentReply"].ToString();
                    txtGrantedFrom.Text = ds.Tables[0].Rows[0]["GrantedFrom"].ToString();
                    txtGrantedUntil.Text = ds.Tables[0].Rows[0]["GrantedUntil"].ToString();
                    lblDateofJoin.Text = ds.Tables[0].Rows[0]["DateOfJoin"].ToString();
                    lblAvailable.Text = ds.Tables[0].Rows[0]["TotLeave"].ToString();
                    DateTime LeaveFrom = CodeUtilHMS.ConvertToDate_ddMMMyyy(lblLevFrom.Text.Trim(), CodeUtilHMS.DateFormat.MonthDayYear);
                    DateTime LeaveUntil = CodeUtilHMS.ConvertToDate_ddMMMyyy(lblLevUntil.Text.Trim(), CodeUtilHMS.DateFormat.MonthDayYear);
                    TimeSpan span = LeaveUntil.Subtract(LeaveFrom);
                    int totdays = span.Days + 1;
                    lblnoofdays.Text = totdays.ToString();
                    //DateTime From = CodeUtilHMS.ConvertToDate_ddMMMyyy(txtGrantedFrom.Text.Trim(), CodeUtilHMS.DateFormat.ddMMMyyyy);
                    //DateTime Until = CodeUtilHMS.ConvertToDate_ddMMMyyy(txtGrantedUntil.Text.Trim(), CodeUtilHMS.DateFormat.ddMMMyyyy);
                    //TimeSpan SPAN = Until.Subtract(From);
                    //int TOTdays = SPAN.Days + 1;
                    //if (TOTdays > totdays)
                    //{
                    //    btnsave.Enabled = false;
                    //    AlertMsg.MsgBox(this.Page, "Please Check GratedDays");
                    //}
                    //lblGrantedDays.Text = TOTdays.ToString();
                    txtComment.Text = ds.Tables[0].Rows[0]["Comment"].ToString();
                    // ddlStatus.SelectedValue = ds.Tables[0].Rows[0]["Status"].ToString();
                    ViewState["Status"] = ds.Tables[0].Rows[0]["Status"].ToString();
                    lblLeaveType.Text = ds.Tables[0].Rows[0]["LeaveType"].ToString();
                    if (txtGrantedFrom.Text == "" && txtGrantedFrom.Text == string.Empty)
                    {
                        lblGrantedDays.Text = "";
                    }
                    tblHR.Visible = false;
                    tblRejected.Visible = false;
                    tblHREdit.Visible = true;
                    lnkCloseCal.Visible = false;
                }
                if (e.CommandName == "Process")
                {
                    int LID = Convert.ToInt32(e.CommandArgument);
                    int EmpID = Convert.ToInt32(Session["UserId"]);
                    ViewState["LID"] = LID;
                    DataSet ds = AttendanceDAC.HR_GetLeaveDetailsByID(LID);
                    try
                    {
                        txtGrantedFrom.Text = CodeUtilHMS.ConvertToDate_ddMMMyyy(ds.Tables[0].Rows[0]["GrantedFrom"].ToString(), CodeUtilHMS.DateFormat.DayMonthYear).ToString("dd MMM yyyy");
                        // Convert.ToDateTime(ds.Tables[0].Rows[0]["GrantedFrom"]).ToString("dd MMM yyyy"); 
                    }
                    catch { txtGrantedFrom.Text = ""; }
                    // ddlEmp.SelectedValue = ds.Tables[0].Rows[0]["AssignerEmpID"].ToString() == "" ? "0" : ds.Tables[0].Rows[0]["AssignerEmpID"].ToString();
                    try
                    {
                        txtGrantedUntil.Text = CodeUtilHMS.ConvertToDate_ddMMMyyy(ds.Tables[0].Rows[0]["GrantedUntil"].ToString(), CodeUtilHMS.DateFormat.DayMonthYear).ToString("dd MMM yyyy");
                        //Convert.ToDateTime(ds.Tables[0].Rows[0]["GrantedUntil"]).ToString("dd MMM yyyy"); 
                    }
                    catch { txtGrantedUntil.Text = ""; }
                    lblLevFrom.Text = CodeUtilHMS.ConvertToDate_ddMMMyyy(ds.Tables[0].Rows[0]["LeaveFrom"].ToString(), CodeUtilHMS.DateFormat.DayMonthYear).ToString("dd MMM yyyy");
                    //ds.Tables[0].Rows[0]["LeaveFrom"].ToString();//Convert.ToDateTime(ds.Tables[0].Rows[0]["LeaveFrom"]).ToString(ConfigurationManager.AppSettings["DateFormat"]);
                    lblLevUntil.Text = CodeUtilHMS.ConvertToDate_ddMMMyyy(ds.Tables[0].Rows[0]["LeaveUntil"].ToString(), CodeUtilHMS.DateFormat.DayMonthYear).ToString("dd MMM yyyy");
                    //ds.Tables[0].Rows[0]["LeaveUntil"].ToString();//Convert.ToDateTime(ds.Tables[0].Rows[0]["LeaveUntil"]).ToString(ConfigurationManager.AppSettings["DateFormat"]);
                    lblReason.Text = ds.Tables[0].Rows[0]["Reason"].ToString();
                    lblEmpID.Text = ds.Tables[0].Rows[0]["EmpID"].ToString();
                    lblName.Text = ds.Tables[0].Rows[0]["name"].ToString();
                    lblAppliedOn.Text = CodeUtilHMS.ConvertToDate_ddMMMyyy(ds.Tables[0].Rows[0]["AppliedOn"].ToString(), CodeUtilHMS.DateFormat.DayMonthYear).ToString("dd MMM yyyy");
                    //ds.Tables[0].Rows[0]["AppliedOn"].ToString();
                    lblempreply.Text = ds.Tables[0].Rows[0]["CommentReply"].ToString();
                    if (ds.Tables[0].Rows[0]["GrantedFrom"].ToString().Trim() == "")
                        txtGrantedFrom.Text = CodeUtilHMS.ConvertToDate_ddMMMyyy(ds.Tables[0].Rows[0]["LeaveFrom"].ToString(), CodeUtilHMS.DateFormat.DayMonthYear).ToString("dd MMM yyyy");
                    else
                        txtGrantedFrom.Text = CodeUtilHMS.ConvertToDate_ddMMMyyy(ds.Tables[0].Rows[0]["GrantedFrom"].ToString(), CodeUtilHMS.DateFormat.DayMonthYear).ToString("dd MMM yyyy");
                    if (ds.Tables[0].Rows[0]["GrantedUntil"].ToString().Trim() == "")
                        txtGrantedUntil.Text = CodeUtilHMS.ConvertToDate_ddMMMyyy(ds.Tables[0].Rows[0]["LeaveUntil"].ToString(), CodeUtilHMS.DateFormat.DayMonthYear).ToString("dd MMM yyyy");
                    else
                        txtGrantedUntil.Text = CodeUtilHMS.ConvertToDate_ddMMMyyy(ds.Tables[0].Rows[0]["GrantedUntil"].ToString(), CodeUtilHMS.DateFormat.DayMonthYear).ToString("dd MMM yyyy");
                    lblLeaveType.Text = ds.Tables[0].Rows[0]["LeaveType"].ToString();
                    lblDateofJoin.Text = ds.Tables[0].Rows[0]["DateOfJoin"].ToString();
                    lblAvailable.Text = ds.Tables[0].Rows[0]["TotLeave"].ToString();
                    // added here by pratap date:23-04-2016
                    string Path;
                    if (ds.Tables[0].Rows[0]["Proof"].ToString() != "")
                    {
                        //Path = "../LeaveApplications/" +  Convert.ToInt32(Session["UserId"]) + "/" + ds.Tables[0].Rows[0]["Proof"];                        
                        Path = "../LeaveApplications/" + Convert.ToInt32(lblEmpID.Text) + "/" + ds.Tables[0].Rows[0]["Proof"];
                    }
                    else
                    {
                        Path = "";
                        lblProof.Text = "No proof avaliable.";
                    }
                    hlnkProof.HRef = DocNavigateUrl(Path);
                    DateTime LeaveFrom = CodeUtilHMS.ConvertToDate_ddMMMyyy(lblLevFrom.Text.Trim(), CodeUtilHMS.DateFormat.ddMMMyyyy);
                    DateTime LeaveUntil = CodeUtilHMS.ConvertToDate_ddMMMyyy(lblLevUntil.Text.Trim(), CodeUtilHMS.DateFormat.ddMMMyyyy);
                    TimeSpan span = LeaveUntil.Subtract(LeaveFrom);
                    int totdays = span.Days + 1;
                    lblnoofdays.Text = totdays.ToString();
                    DateTime From = CodeUtilHMS.ConvertToDate_ddMMMyyy(txtGrantedFrom.Text.Trim(), CodeUtilHMS.DateFormat.ddMMMyyyy);
                    DateTime Until = CodeUtilHMS.ConvertToDate_ddMMMyyy(txtGrantedUntil.Text.Trim(), CodeUtilHMS.DateFormat.ddMMMyyyy);
                    TimeSpan SPAN = Until.Subtract(From);
                    int TOTdays = SPAN.Days + 1;
                    lblGrantedDays.Text = TOTdays.ToString();
                    if (TOTdays > totdays)
                    {
                        btnsave.Enabled = false;
                        AlertMsg.MsgBox(this.Page, "Please Check GrantedDays", AlertMsg.MessageType.Warning);
                    }
                    txtComment.Text = ds.Tables[0].Rows[0]["Comment"].ToString();
                    //ddlStatus.SelectedValue = ds.Tables[0].Rows[0]["Status"].ToString();
                    ViewState["Status"] = ds.Tables[0].Rows[0]["Status"].ToString();
                    tblHR.Visible = false;
                    tblHREdit.Visible = true;
                    tblRejected.Visible = false;
                    lnkCloseCal.Visible = false;                    
                }
            }
            catch (Exception ex)
            {
                clsErrorLog.HMSEventLog(ex, "HRLeaveApplication", "gvGranted_RowCommand", "008");
            }
        }
        protected void txtGrantedUntil_TextChanged(object sender, EventArgs e)
        {
            try
            {
                DateTime LeaveFrom = CodeUtilHMS.ConvertToDate_ddMMMyyy(lblLevFrom.Text.Trim(), CodeUtilHMS.DateFormat.ddMMMyyyy);
                DateTime LeaveUntil = CodeUtilHMS.ConvertToDate_ddMMMyyy(lblLevUntil.Text.Trim(), CodeUtilHMS.DateFormat.ddMMMyyyy);
                TimeSpan span = LeaveUntil.Subtract(LeaveFrom);
                int totdays = span.Days + 1;
                DateTime From = CodeUtilHMS.ConvertToDate_ddMMMyyy(txtGrantedFrom.Text.Trim(), CodeUtilHMS.DateFormat.ddMMMyyyy);
                DateTime Until = CodeUtilHMS.ConvertToDate_ddMMMyyy(txtGrantedUntil.Text.Trim(), CodeUtilHMS.DateFormat.ddMMMyyyy);
                TimeSpan SPAN = Until.Subtract(From);
                int TOTdays = SPAN.Days + 1;
                lblGrantedDays.Text = TOTdays.ToString();
                if (TOTdays > totdays)
                {
                    btnsave.Enabled = false;
                    AlertMsg.MsgBox(this.Page, "Please Check GrantedDays", AlertMsg.MessageType.Warning);
                }
            }
            catch (Exception ex)
            {
                clsErrorLog.HMSEventLog(ex, "HRLeaveApplication", "txtGrantedUntil_TextChanged", "009");
            }
        }
        #region Rijwancode
        //protected void GetWork(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (txtSearchWorksite.Text != "")
        //        {
        //            CompanyID = Convert.ToInt32(Session["CompanyID"]);
        //            WSId = Convert.ToInt32(ddlWs_hid.Value == "" ? "0" : ddlWs_hid.Value);
        //            Siteid = EWsid = WSId;
        //                //Convert.ToInt32(txtSearchWorksite.Text.Substring(0, 4));
        //        }
        //    }
        //    catch { txtSearchWorksite.Text = ""; }
        //    txtdept.Text = "";
        //    txtSearchEmp.Text = "";
        //}
        protected void GetWork(object sender, EventArgs e)
        {
            CompanyID = Convert.ToInt32(Session["CompanyID"]);
            WSId = Convert.ToInt32(ddlWs_hid.Value == "" ? "0" : ddlWs_hid.Value);
        }
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
        protected void GetDept(object sender, EventArgs e)
        {
            try
            {
                if (txtdept.Text != "")
                {
                    EDeptid = Convert.ToInt32(txtdept.Text.Substring(0, 4));
                }
            }
            catch { txtdept.Text = ""; }
        }
        protected void GetWorkApr(object sender, EventArgs e)
        {
            try
            {
                if (txtSerachWorkApr.Text != "")
                {
                    Siteid = EWsid = Convert.ToInt32(txtSerachWorkApr.Text.Substring(2, 5));
                }
            }
            catch
            {
                //    txtSerachWorkApr.Text = ""; 
            }
            txtSearchDeptApr.Text = "";
            txtSearchEmpApr.Text = "";
        }
        protected void GetDeptApr(object sender, EventArgs e)
        {
            try
            {
                if (txtSearchDeptApr.Text != "")
                {
                    EDeptid = Convert.ToInt32(txtSearchDeptApr.Text.Substring(0, 4));
                }
            }
            catch { txtSearchDeptApr.Text = ""; }
        }
        protected void GetWorkRej(object sender, EventArgs e)
        {
            try
            {
                if (txtSerachWorkRej.Text != "")
                {
                    Siteid = EWsid = Convert.ToInt32(txtSerachWorkRej.Text.Substring(0, 4));
                }
            }
            catch { txtSerachWorkRej.Text = ""; }
            txtSearchDeptRej.Text = "";
            txtSerachEmpRej.Text = "";
        }
        protected void GetDeptRej(object sender, EventArgs e)
        {
            try
            {
                if (txtSearchDeptRej.Text != "")
                {
                    EDeptid = Convert.ToInt32(txtSearchDeptRej.Text.Substring(0, 4));
                }
            }
            catch { txtSearchDeptRej.Text = ""; }
        }
        #endregion Rijwancode
        protected void lnkViewCalender_Click(object sender, EventArgs e)
        {
            Cal.Visible = true;
            lnkViewCalender.Visible = false;
            lnkCloseCal.Visible = true;
        }
        //added by nadeem,07/06/2016, to transfer clearenceview page
        protected void lnkViewclearence_Click(object sender, EventArgs e)
        {
            string EmpID = lblEmpID.Text;
            string Ename = lblName.Text;
            string url = "clearenceview.aspx?Empid=" + EmpID + "&key=" + 1 + "&Name=" + Ename;
            string fullURL = "window.open('" + url + "', '_blank' );";
            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", fullURL, true);
        }
        protected void lnkCloseCal_Click(object sender, EventArgs e)
        {
            Cal.Visible = false;
            lnkViewCalender.Visible = true;
            lnkCloseCal.Visible = false;
        }
        protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                divLeaves.Visible = false;
                if (Convert.ToInt32(ddlStatus.SelectedValue) == 2)
                    if (Convert.ToBoolean(ViewState["IsCFrwd"]) == true)
                        divLeaves.Visible = true;
            }
            catch (Exception ex)
            {
                clsErrorLog.HMSEventLog(ex, "HRLeaveApplication", "ddlStatus_SelectedIndexChanged", "010");
            }
        }
        protected void ddlMonthGrant_SelectedIndexChanged(object sender, EventArgs e)
        {
            HRLeaveAppGrantedPaging.CurrentPage = 1;
        }
        protected void ddlMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            HRLeaveAppInprocessPaging.CurrentPage = 1;
        }
        #region Webservice code by Rijwan
        ////Added By Rijwan for Worksite Google Search 
        //[System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        //public static string[] GetCompletionList(string prefixText, int count, string contextKey)
        //{
        //    //DataSet ds = AttendanceDAC.GetWorkSiteLeaveActive(prefixText);
        //    DataSet ds = AttendanceDAC.GetWorkSites(prefixText, CompanyID, Userid, ModID);
        //    DataTable dt = ds.Tables[0];
        //    List<string> items = new List<string>(count);
        //    var rtval = new Dictionary<string, string>();
        //    foreach (DataRow row in dt.Rows)
        //    {
        //        string str = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(row["Name"].ToString(), row["ID"].ToString());
        //        items.Add(str);
        //    }
        //    return items.ToArray();
        //    //return ConvertStingArray(ds);// txtItems.ToArray();
        //}
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionListDep(string prefixText, int count, string contextKey)
        {
            DataSet ds = AttendanceDAC.GetSearchDePartmentFilterActive_HRLeaveApplications(prefixText, SearchCompanyID, Siteid);
            return ConvertStingArray(ds);// txtItems.ToArray();
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionListEmp(string prefixText, int count, string contextKey)
        {
            DataSet ds = AttendanceDAC.HR_GoogleSearchEmpBySiteDept(prefixText, EWsid, EDeptid, "Y", SearchCompanyID);
            return ConvertStingArray(ds);
        }
        public static string[] ConvertStingArray(DataSet ds)
        {
            string[] rtval = Array.ConvertAll(ds.Tables[0].Select(), new Converter<DataRow, string>(DataRowToDouble));
            return rtval;
        }
        public static string DataRowToDouble(DataRow dr)
        {
            return dr["Name"].ToString();
        }
        #endregion Webservice code by Rijwan
        protected void gvLeaveApptoHr_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    string Names = "";
                    string Grantnames = (e.Row.DataItem as DataRowView)["GrantedByNames"].ToString();
                    string[] words = Grantnames.Split(',');
                    foreach (string word in words)
                    {
                        Names = Names + word.ToString() + "\n ";
                    }
                    e.Row.Cells[12].ToolTip = Names.ToString();
                }
            }
            catch { }
        }
        protected void gvGranted_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if ((e.Row.DataItem as DataRowView)["Reason"].ToString() == "")
                {
                    LinkButton lnkreason = (LinkButton)e.Row.FindControl("lnkReason");
                    lnkreason.Visible = false;
                }
                e.Row.Cells[12].ToolTip = (e.Row.DataItem as DataRowView)["GrantedBy"].ToString();
                e.Row.Cells[7].ToolTip = (e.Row.DataItem as DataRowView)["Reason"].ToString();
                e.Row.Cells[16].ToolTip = (e.Row.DataItem as DataRowView)["LeaveName"].ToString();
            }
            if (Request.QueryString.Count > 0)
            {
                int id = Convert.ToInt32(Request.QueryString["key"].ToString());
                if (id == 1)
                {
                    //LinkButton lnkCancel = (LinkButton)e.Row.FindControl("lnkCancel");
                    //lnkCancel.Visible = false;
                    e.Row.Cells[18].Visible = false;

                    if (e.Row.RowType == DataControlRowType.DataRow)
                    {
                        LinkButton lnkVS = (LinkButton)e.Row.FindControl("lnkVS");
                        LinkButton lnkLeaveType = (LinkButton)e.Row.FindControl("lnkLeaveType");
                        int Empid = Convert.ToInt32((e.Row.DataItem as DataRowView)["Empid"]);
                        DateTime stdt = Convert.ToDateTime((e.Row.DataItem as DataRowView)["GrantedFrom"].ToString());
                        DateTime eddt = Convert.ToDateTime((e.Row.DataItem as DataRowView)["GrantedUntil"].ToString());
                        //DateTime.ParseExact((e.Row.DataItem as DataRowView)["GrantedFrom"].ToString(), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                        //DateTime eddt = DateTime.ParseExact((e.Row.DataItem as DataRowView)["GrantedUntil"].ToString(), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                        if (lnkLeaveType.Text != "LOP")
                        {
                            lnkVS.Visible = false;
                        }
                        else
                        {
                            SqlParameter[] objParam = new SqlParameter[3];
                            objParam[0] = new SqlParameter("@EmpID", Empid);
                            objParam[1] = new SqlParameter("@StartDate", stdt);
                            objParam[2] = new SqlParameter("@Enddate", eddt);
                            int count = Convert.ToInt32(SqlHelper.ExecuteScalar("sh_GetVSStatus", objParam));
                            if (count > 0)
                            {
                                lnkVS.Visible = false;
                            }
                            else
                            {
                                lnkVS.Visible = true;
                            }
                        }
                    }
                }
            }
        }
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (divLeaves.Visible == true)
                {
                    int EmpID = Convert.ToInt32(lblEmpID.Text);
                    int UserId = Convert.ToInt32(Session["UserId"]);
                    decimal Crval = Convert.ToDecimal(txtOpenningBal.Text);
                    int LevID = Convert.ToInt32(ViewState["LevTypID"]);
                    // ViewState["LevTypID"] = false;
                    Leaves.InsUpdateTypeofLeaves(LevID, Crval, 0, EmpID, DateTime.Now);
                }
            }
            catch (Exception ex)
            {
                clsErrorLog.HMSEventLog(ex, "HRLeaveApplication", "btnUpdate_Click", "002");
            }
        }

        protected void lnkUpload_Click(object sender, EventArgs e)
        {
            try
            {

                foreach (GridViewRow gvr in gvGranted.Rows)
                {
                    Label lblLID = (Label)gvr.FindControl("lblLID");
                    FileUpload UploadProof1 = (FileUpload)gvr.Cells[14].FindControl("UploadProof");
                    string fileName = Path.GetFileName(UploadProof1.PostedFile.FileName);
                    string fileExtension = Path.GetExtension(UploadProof1.PostedFile.FileName);
                    //first check if "uploads" folder exist or not, if not create it
                    string fileSavePath = Server.MapPath("uploads");
                    if (!Directory.Exists(fileSavePath))
                        Directory.CreateDirectory(fileSavePath);
                    //after checking or creating folder it's time to save the file
                    fileSavePath = fileSavePath + "\\" + fileName;
                    UploadProof1.PostedFile.SaveAs(fileSavePath);
                    FileInfo fileInfo = new FileInfo(fileSavePath);
                    string contentType = UploadProof1.PostedFile.ContentType;
                    string ext = Path.GetExtension(fileName);
                    string type = String.Empty;
                    if (UploadProof1.HasFile)
                    {
                        try
                        {
                            switch (ext) // this switch code validate the files which allow to upload only PDF file   
                            {
                                case ".pdf":
                                    type = "application/pdf";
                                    break;
                            }
                            if (type != String.Empty)
                            {
                                using (Stream fs = UploadProof1.PostedFile.InputStream)
                                {
                                    using (BinaryReader br = new BinaryReader(fs))
                                    {
                                        byte[] bytes = br.ReadBytes((Int32)fs.Length);
                                        using (SqlConnection con = new SqlConnection(connectionString))
                                        {
                                            SqlCommand cmd = new SqlCommand("HR_UploadDownloadLeaveApplication", con);
                                            cmd.Connection = con;
                                            cmd.CommandType = CommandType.StoredProcedure;
                                            cmd.Parameters.AddWithValue("@LID", lblLID.Text);
                                            cmd.Parameters.AddWithValue("@EmpID", Empid);
                                            cmd.Parameters.AddWithValue("@FileName", fileName);
                                            cmd.Parameters.AddWithValue("@FileSize", fileInfo.Length.ToString());
                                            cmd.Parameters.AddWithValue("@FileExtension", fileExtension);
                                            cmd.Parameters.AddWithValue("@FilePath", fileSavePath);
                                            con.Open();
                                            cmd.ExecuteNonQuery();
                                            con.Close();
                                            AlertMsg.MsgBox(Page, "File has been uploaded Successfully..!", AlertMsg.MessageType.Success);
                                            return;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                // if file is other than speified extension  
                                AlertMsg.MsgBox(Page, "Select Only PDF Files ..!", AlertMsg.MessageType.Warning);
                                return;
                            }
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                    }
                    if (!UploadProof1.HasFile)
                    {
                        //if file uploader has no file selected  
                        AlertMsg.MsgBox(Page, "Please Select File..!", AlertMsg.MessageType.Warning);
                        return;
                    }
                }

            }
            catch (Exception ex)
            {
                //clsErrorLog.HMSEventLog(ex, "HRLeaveApplication", "lnkUpload_Click", "006");
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow gvr in gvGranted.Rows)
            {               
                FileUpload UploadProof1 = (FileUpload)gvr.Cells[14].FindControl("UploadProof");
                UploadProof1.Attributes.Clear();
            }
        }
    }
}
