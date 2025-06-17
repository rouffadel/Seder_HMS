using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using Aeclogic.Common.DAL;
using System.IO;
using AECLOGIC.HMS.BLL;
using System.Configuration;
using System.Collections;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using AECLOGIC.ERP.COMMON;
namespace AECLOGIC.ERP.HMS
{
    public partial class AbsPenalities : AECLOGIC.ERP.COMMON.WebFormMaster
    {
        #region Variables
        int mid = 0;
        bool viewall, Editable;
        static int SearchCompanyID;
        static int Empdeptid = 0;
        string menuname;
        string menuid;
        private GridSort objSort;
        int CompanyID = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["CompanyID"].ToString());
        AttendanceDAC objAtt;
        //DataSet ds;
        HRCommon objHrCommon = new HRCommon();
        #endregion Variables
        #region Events
        void EmpReimbursementAprovedPaging_ShowRowsClick(object sender, EventArgs e)
        {
            EmpReimbursementAprovedPaging.CurrentPage = 1;
            BindPager();
        }
        void EmpReimbursementAprovedPaging_FirstClick(object sender, EventArgs e)
        {
            BindPager();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                btnsearch.Attributes.Add("onclick", "javascript:return ValidateSave();");
                SearchCompanyID = Convert.ToInt32(Session["CompanyID"]);
                objAtt = new AttendanceDAC();
                if (!IsPostBack)
                {
                    GetWorkSites(0);
                    //BindDesignations();
                    BindYears();
                    EmpReimbursementAprovedPaging.Visible = false;
                }
                if (Request.QueryString.Count > 0)
                {
                    ddlMonth.SelectedValue = Request.QueryString["Month"];
                    ddlYear.SelectedValue = Request.QueryString["Year"];
                    txtEmpID.Text = Request.QueryString["EMPID"];
                    AttendanceDAC objEmployee = new AttendanceDAC();
                    DataSet ds = objEmployee.GetWorkSiteByEmpID(Convert.ToInt32( txtEmpID.Text), Convert.ToInt32(Session["CompanyID"]), Convert.ToInt32(null));
                    if (ds != null && ds.Tables[0].Rows.Count > 0 && ds.Tables[0].Rows[0][0].ToString() != "")
                        ddlWS.SelectedValue = ds.Tables[0].Rows[0][0].ToString();
                    else
                        ddlWS.SelectedValue = "1";
                    objHrCommon.PageSize = EmpReimbursementAprovedPaging.ShowRows;
                    objHrCommon.CurrentPage = EmpReimbursementAprovedPaging.CurrentPage;
                    if (Convert.ToInt32(ddlWS.SelectedIndex) <= 0)
                    {
                        AlertMsg.MsgBox(Page, "Select worksite",AlertMsg.MessageType.Warning);
                    }
                    else if (Convert.ToInt32(ddlMonth.SelectedValue) > 0 && Convert.ToInt32(ddlYear.SelectedValue) > 0)
                        EmployeeApproved(objHrCommon);
                    else
                    {
                        AlertMsg.MsgBox(Page, "Select Month/Year", AlertMsg.MessageType.Warning);
                    }
                    EmpReimbursementAprovedPaging.Bind(objHrCommon.CurrentPage, objHrCommon.TotalPages, objHrCommon.NoofRecords, objHrCommon.PageSize);
                }
            }
            catch (Exception ex)
            {
                clsErrorLog.HMSEventLog(ex, "Absent Penalities", "Page_Load", "001");
            }
        }
        protected void btnsearch_Click(object sender, EventArgs e)
        {
            try
            {
                EmpReimbursementAprovedPaging.CurrentPage = 1;
                gvViewApproved.Columns[12].Visible = true;
                objHrCommon.CurrentPage = EmpReimbursementAprovedPaging.CurrentPage;
                objHrCommon.PageSize = EmpReimbursementAprovedPaging.ShowRows;
                objHrCommon.SiteID = Convert.ToInt32(ddlWS.SelectedItem.Value);
                objHrCommon.DeptID = 0;
                if (Convert.ToInt32(ddlMonth.SelectedValue) > 0 && Convert.ToInt32(ddlYear.SelectedValue) > 0)
                {
                    objHrCommon.Month = Convert.ToInt32(ddlMonth.SelectedValue);
                    //if (objHrCommon.Month == 1)
                    //{
                    //    objHrCommon.Month = 12;
                    //}
                    //else
                    //    objHrCommon.Month = objHrCommon.Month - 1;
                }
                else
                {
                    objHrCommon.Month = 0;
                }
                if (Convert.ToInt32(ddlYear.SelectedValue) > 0)
                {
                    objHrCommon.Year = Convert.ToInt32(ddlYear.SelectedItem.Text);
                    //if (Convert.ToInt32(ddlMonth.SelectedValue) == 1)
                    //{
                    //    objHrCommon.Year = objHrCommon.Year - 1;
                    //}
                }
                else
                {
                    objHrCommon.Year = 0;
                }
                if (txtEmpID.Text.Trim() != string.Empty)
                    objHrCommon.EmpID = Convert.ToInt32(txtEmpID.Text.Trim());
                else
                    objHrCommon.EmpID = 0;
                 DataSet ds = AttendanceDAC.HR_AbsentPenalitiesSearch(objHrCommon);
                if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0)
                {
                    gvViewApproved.DataSource = null;
                    gvViewApproved.DataSource = ds;
                    gvViewApproved.DataBind();
                    EmpReimbursementAprovedPaging.Visible = true;
                }
                else
                {
                    gvViewApproved.EmptyDataText = "No Records Found";
                    EmpReimbursementAprovedPaging.Visible = false;
                    gvViewApproved.DataBind();
                }
                EmpReimbursementAprovedPaging.Bind(objHrCommon.CurrentPage, objHrCommon.TotalPages, objHrCommon.NoofRecords, objHrCommon.PageSize);
            }
            catch (Exception ex)
            {
                clsErrorLog.HMSEventLog(ex, "Absent Penalities", "btnsearch_Click", "002");
            }
        }
        protected void btnTransferAcc_Click(object sender, EventArgs e)
        {
            try
            {
                if (gvViewApproved.Rows.Count > 0)
                {
                    string APPEmpid = "";
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
                    dtTDT.Columns.Add(new DataColumn("TotOccurance", typeof(System.Int32)));
                    dtTDT.Columns.Add(new DataColumn("Penalities", typeof(System.Int32)));
                    dtTDT.Columns.Add(new DataColumn("EmpName", typeof(System.String)));
                    dtTDT.Columns.Add(new DataColumn("Absents", typeof(System.Int32)));
                    dsTransferDetail.Tables.Add(dtTDT);
                    int EmpID = 0;
                    Double TotAmt = 0;
                    int b = 0;
                    foreach (GridViewRow gvRow in gvViewApproved.Rows)
                    {
                        CheckBox chk = new CheckBox();
                        chk = (CheckBox)gvRow.FindControl("chkToTransfer");
                        if (chk.Checked)
                        {
                            Label lblEmp = (Label)gvRow.FindControl("lblEmpID");
                            Label lblERID = (Label)gvRow.FindControl("lblERID");
                            Label lblAmount = (Label)gvRow.FindControl("lblAmount");
                            Label lblLimited = (Label)gvRow.FindControl("lblLimited");
                            Label lbltotaloccurance = (Label)gvRow.FindControl("lbltotaloccurance");
                            Label lblAbsents = (Label)gvRow.FindControl("lblAbsents");
                            Label lblempname = (Label)gvRow.FindControl("lblempname");
                            Label lblpenalities = (Label)gvRow.FindControl("lblpenalities");
                            int ERID = 0;
                            EmpID = Convert.ToInt32(lblEmp.Text);
                            double Amt = Convert.ToDouble(lblAmount.Text);
                            TotAmt = Amt;
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
                            dr["TotOccurance"] = lbltotaloccurance.Text;
                            dr["Penalities"] = lblpenalities.Text;
                            dr["EmpName"] = lblempname.Text;
                            dr["Absents"] = lblAbsents.Text;
                            dtTDT.Rows.Add(dr);
                            string Remarks = "Absent Penality - " + Convert.ToString(ddlMonth.SelectedItem) + " - " + Convert.ToString(ddlYear.SelectedItem.Text);
                            dsTransferDetail.AcceptChanges();
                            if (Convert.ToString(lblLimited) != "Terminated" && TotAmt > 0)
                            {
                                DataSet ds = AttendanceDAC.HMS_AbsentPenlityTranserAccXML(dsTransferDetail, Remarks, TotAmt, Convert.ToInt32(Session["UserId"]), Convert.ToInt32(ddlMonth.SelectedValue), Convert.ToInt32(ddlYear.SelectedItem.Text));
                                if (ds.Tables.Count > 0)
                                    b = 1;
                                else
                                {
                                    APPEmpid = APPEmpid + ">>" + ds.Tables[0].Rows[0]["EmpID"].ToString();
                                }
                            }
                        }
                    }
                    if (b == 1)
                    {
                        AlertMsg.MsgBox(Page, "Approved Successfully", AlertMsg.MessageType.Success);
                        btnsearch_Click(null, null);
                    }
                    if (APPEmpid != String.Empty)
                        AlertMsg.MsgBox(Page, "Check these Employees:" + APPEmpid, AlertMsg.MessageType.Warning);
                }
                else
                    AlertMsg.MsgBox(Page, "No Records Found", AlertMsg.MessageType.Warning);
            }
            catch (Exception ex)
            {
                clsErrorLog.HMSEventLog(ex, "Absent Penalities", "btnTransferAcc_Click", "003");
            }
        }
        protected void gvViewApproved_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.Header)
                {
                    CheckBox chkMail = (CheckBox)e.Row.FindControl("chkSelectAll");
                    chkMail.Attributes.Add("onclick", String.Format("javascript:SelectAll(this,'{0}','chkApproval');", gvViewApproved.ClientID));
                }
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    LinkButton lnkApprove = (LinkButton)e.Row.FindControl("lnkApprove");
                    CheckBox chkToTransfer = (CheckBox)e.Row.FindControl("chkToTransfer");
                    int year = Convert.ToInt32((e.Row.DataItem as DataRowView)["Year"]);
                    int Month = Convert.ToInt32((e.Row.DataItem as DataRowView)["MonthNumber"]);
                  int Empid = Convert.ToInt32((e.Row.DataItem as DataRowView)["Empid"]);
                  SqlParameter[] objParam = new SqlParameter[3];
                  objParam[0] = new SqlParameter("@Year", year);
                  objParam[1] = new SqlParameter("@Month", Month);
                  objParam[2] = new SqlParameter("@EmpID", Empid);
               int count=  Convert.ToInt32(SqlHelper.ExecuteScalar("sh_GetAbsPenaApprvd", objParam));
                    if(count>0)
                    {
                        //lnkApprove.Visible = false;
                        lnkApprove.Visible = false;
                        chkToTransfer.Visible= false;
                    }
                    else
                    {
                        lnkApprove.Visible = true;
                        chkToTransfer.Visible = true;
                    }
                }
            }
            catch 
            {
            }
        }
        protected void btnSync_Click(object sender, EventArgs e)
        {
            try
            {
                EmpReimbursementAprovedPaging.CurrentPage = 1;
                objHrCommon.PageSize = EmpReimbursementAprovedPaging.ShowRows;
                objHrCommon.CurrentPage = EmpReimbursementAprovedPaging.CurrentPage;
                gvViewApproved.Columns[12].Visible = false;
                if (Convert.ToInt32(ddlWS.SelectedIndex) <= 0)
                {
                    AlertMsg.MsgBox(Page, "Select worksite", AlertMsg.MessageType.Warning);
                }
                else if (Convert.ToInt32(ddlMonth.SelectedValue) > 0 && Convert.ToInt32(ddlYear.SelectedValue) > 0)
                    EmployeeApproved(objHrCommon);
                else
                {
                    AlertMsg.MsgBox(Page, "Select Month/Year", AlertMsg.MessageType.Warning);
                }
                EmpReimbursementAprovedPaging.Bind(objHrCommon.CurrentPage, objHrCommon.TotalPages, objHrCommon.NoofRecords, objHrCommon.PageSize);
            }
            catch (Exception ex)
            {
                clsErrorLog.HMSEventLog(ex, "Absent Penalities", "btnSync_Click", "004");
            }
        }
        #endregion Events
        #region Methods
        void BindPager()
        {
            objHrCommon.PageSize = EmpReimbursementAprovedPaging.CurrentPage;
            objHrCommon.CurrentPage = EmpReimbursementAprovedPaging.ShowRows;
            EmployeeApproved(objHrCommon);
        }
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
        }
        public void BindYears()
        {
           // FIllObject.FillDropDown(ref ddlYear, "HMS_YearWise");
            DataSet ds = PayRollMgr.GetFinancialYear();
            ddlYear.DataSource = ds;
            ddlYear.DataValueField = "AssessmentYear";
            ddlYear.DataTextField = "AssessmentYear";
            ddlYear.DataBind();
            ddlYear.Items.Insert(0, new ListItem("--Select--", "0"));
            ddlYear.SelectedIndex = 0;
        }
        public int GetParentMenuId()
        {
            string URL = Request.Url.Segments[Request.Url.Segments.Length - 1] + Request.Url.Query;
            int RoleId = Convert.ToInt32(Session["RoleId"].ToString());
            int ModuleId = ModuleID; ;
            //DataSet ds = new DataSet();
            //ds = AttendanceDAC.GetAllowed(RoleId, ModuleId, URL);
            //int MenuId = 0;
            //if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            //{
            //    MenuId = Convert.ToInt32(ds.Tables[0].Rows[0]["Under"]);
            //    ViewState["Editable"] = (bool)ds.Tables[0].Rows[0]["Editable"];
            //    ViewState["ViewAll"] = (bool)ds.Tables[0].Rows[0]["ViewAll"];
            //    gvRMItem.Columns[1].Visible = Convert.ToBoolean(ds.Tables[0].Rows[0]["Editable"].ToString());
            //    viewall = (bool)ViewState["ViewAll"];
            //    menuname = ds.Tables[0].Rows[0]["menuname"].ToString();
            //    menuid = MenuId.ToString();
            //    btnSubmit.Enabled = (bool)ds.Tables[0].Rows[0]["Editable"];
            //    mid = Convert.ToInt32(ds.Tables[0].Rows[0]["MenuId"].ToString());
            //}
            return 1;// need to change
        }
        protected void GetWork(object sender, EventArgs e)
        {
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@Search", txtSearchWorksite.Text);
            param[1] = new SqlParameter("@CompanyID", Convert.ToInt32(Session["CompanyID"]));
            FIllObject.FillDropDown(ref ddlWS, "G_GET_WorkSitebyFilter", param);
            ListItem itmSelected = ddlWS.Items.FindByText(txtSearchWorksite.Text);
            if (itmSelected != null)
            {
                ddlWS.SelectedItem.Selected = false;
                itmSelected.Selected = true;
            }
        }
        //protected void GetDep(object sender, EventArgs e)
        //{
        //    SqlParameter[] param = new SqlParameter[1];
        //    param[0] = new SqlParameter("@Search", txtsearchDept.Text);
        //    FIllObject.FillDropDown(ref ddlDept, "HR_GetSearchgoogleDesignations", param);
        //    ListItem itmSelected = ddlDept.Items.FindByText(txtsearchDept.Text);
        //    if (itmSelected != null)
        //    {
        //        ddlDept.SelectedItem.Selected = false;
        //        itmSelected.Selected = true;
        //    }
        //}
        //private void BindDesignations()
        //{
        //    DataSet ds = objAtt.GetDesignations();
        //    ddlDept.DataSource = ds.Tables[0];
        //    ddlDept.DataTextField = "Designation";
        //    ddlDept.DataValueField = "DesigId";
        //    ddlDept.DataBind();
        //    ddlDept.Items.Insert(0, new ListItem("--Select--", "0"));
        //}
        private void GetWorkSites(int SiteID)
        {
            DataSet ds = AttendanceDAC.GetWorkSite(SiteID, '1', Convert.ToInt32(Session["CompanyID"]));
            ddlWS.DataSource = ds.Tables[0];
            ddlWS.DataTextField = "Site_Name";
            ddlWS.DataValueField = "Site_ID";
            ddlWS.DataBind();
            ddlWS.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        protected string GetMonth(object Status)
        {
            string retValue = "";
            string input = Status.ToString();
            if (input == "1")
            {
                retValue = "January";
            }
            if (input == "2")
            {
                retValue = "February";
            }
            if (input == "3")
            {
                retValue = "March";
            }
            if (input == "4")
            {
                retValue = "April";
            }
            if (input == "5")
            {
                retValue = "May";
            }
            if (input == "6")
            {
                retValue = "June";
            }
            if (input == "7")
            {
                retValue = "July";
            }
            if (input == "8")
            {
                retValue = "August";
            }
            if (input == "9")
            {
                retValue = "September";
            }
            if (input == "10")
            {
                retValue = "October";
            }
            if (input == "11")
            {
                retValue = "November";
            }
            if (input == "12")
            {
                retValue = "December";
            }
            return retValue;
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionList(string prefixText, int count, string contextKey)
        {
            DataSet ds = AttendanceDAC.GetGoogleABCSearchWorkSite(prefixText, SearchCompanyID);
            return ConvertStingArray(ds);// txtItems.ToArray();
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionListDesg(string prefixText, int count, string contextKey)
        {
            DataSet ds = AttendanceDAC.GetGoogleSerachDesignations(prefixText);
            return ConvertStingArray(ds);// txtItems.ToArray();
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
        void EmployeeApproved(HRCommon objHrCommon)
        {
            try
            {
                objHrCommon.PageSize = EmpReimbursementAprovedPaging.ShowRows;
                objHrCommon.CurrentPage = EmpReimbursementAprovedPaging.CurrentPage;
                objHrCommon.SiteID = Convert.ToInt32(ddlWS.SelectedItem.Value);
                objHrCommon.DeptID = 0;
                objHrCommon.Month = Convert.ToInt32(ddlMonth.SelectedValue);
                objHrCommon.Year = Convert.ToInt32(ddlYear.SelectedItem.Text);
                if (objHrCommon.Month == 1)
                {
                    objHrCommon.Month = 12;
                    objHrCommon.Year = objHrCommon.Year - 1;
                }
                else
                    objHrCommon.Month = objHrCommon.Month - 1;
                if (txtEmpID.Text.Trim() != string.Empty)
                    objHrCommon.EmpID = Convert.ToInt32(txtEmpID.Text.Trim());
                else
                    objHrCommon.EmpID = 0;
                 DataSet ds = AttendanceDAC.HR_AbsentPenalitiesByPaging(objHrCommon);
                if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0)
                {
                    gvViewApproved.DataSource = ds;
                    gvViewApproved.DataBind();
                }
                else
                {
                    gvViewApproved.EmptyDataText = "No Records Found";
                    EmpReimbursementAprovedPaging.Visible = false;
                    gvViewApproved.DataBind();
                }
                EmpReimbursementAprovedPaging.Bind(objHrCommon.CurrentPage, objHrCommon.TotalPages, objHrCommon.NoofRecords, objHrCommon.PageSize);
            }
            catch (Exception ex)
            {
                clsErrorLog.HMSEventLog(ex, "Absent Penalities", "EmployeeApproved", "001");
            }
        }
        #endregion Methods
        protected void gvViewApproved_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "App")
            {
                GridViewRow gvRow = (GridViewRow)(((Control)e.CommandSource).NamingContainer);
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
                dtTDT.Columns.Add(new DataColumn("TotOccurance", typeof(System.Int32)));
                dtTDT.Columns.Add(new DataColumn("Penalities", typeof(System.Int32)));
                dtTDT.Columns.Add(new DataColumn("EmpName", typeof(System.String)));
                dtTDT.Columns.Add(new DataColumn("Absents", typeof(System.Int32)));
                dsTransferDetail.Tables.Add(dtTDT);
                int EmpID = 0;
                Double TotAmt = 0;
                int b = 0;
                Label lblEmp = (Label)gvRow.FindControl("lblEmpID");
                Label lblERID = (Label)gvRow.FindControl("lblERID");
                Label lblAmount = (Label)gvRow.FindControl("lblAmount");
                Label lblLimited = (Label)gvRow.FindControl("lblLimited");
                Label lbltotaloccurance = (Label)gvRow.FindControl("lbltotaloccurance");
                Label lblAbsents = (Label)gvRow.FindControl("lblAbsents");
                Label lblempname = (Label)gvRow.FindControl("lblempname");
                Label lblpenalities = (Label)gvRow.FindControl("lblpenalities");
                int ERID = 0;
                EmpID = Convert.ToInt32(lblEmp.Text);
                double Amt = Convert.ToDouble(lblAmount.Text);
                TotAmt = Amt;
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
                dr["TotOccurance"] = lbltotaloccurance.Text;
                dr["Penalities"] = lblpenalities.Text;
                dr["EmpName"] = lblempname.Text;
                dr["Absents"] = lblAbsents.Text;
                dtTDT.Rows.Add(dr);
                string Remarks = "Absent Penality - " + Convert.ToString(ddlMonth.SelectedItem) + " - " + Convert.ToString(ddlYear.SelectedItem.Text);
                dsTransferDetail.AcceptChanges();
                if (Convert.ToString(lblLimited) != "Terminated" && TotAmt > 0)
                {
                    DataSet ds = AttendanceDAC.HMS_AbsentPenlityTranserAccXML(dsTransferDetail, Remarks, TotAmt, Convert.ToInt32(Session["UserId"]), Convert.ToInt32(ddlMonth.SelectedValue), Convert.ToInt32(ddlYear.SelectedItem.Text));
                    if (ds.Tables.Count > 0)
                        b = 1;
                }
                if (b == 1)
                    AlertMsg.MsgBox(Page, "Approved Successfullhy", AlertMsg.MessageType.Success);
                btnSync_Click(null, null);
            }
        }
    }
}