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
using AECLOGIC.ERP.HMS;

namespace AECLOGIC.ERP.HMSV1
{
    public partial class EmpAdvanceApprovalV1 : AECLOGIC.ERP.COMMON.WebFormMaster
    {
        int mid = 0;
        bool viewall;
        string menuname;
        static int SearchCompanyID = 1;
        static int Empdeptid = 0;
        static string EmpName = string.Empty;
        string menuid;
        bool Editable;
        static int SiteSearch;
        int empid;
        //static int CompanyID;
        HRCommon objHrCommon = new HRCommon();
        static int CompanyID = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["CompanyID"].ToString());
        AttendanceDAC objatt = new AttendanceDAC();
        static int ModID;
        static int Userid;
        protected override void OnInit(EventArgs e)
        {
            ModuleID = 1;
            base.OnInit(e);
            // btnSave.Attributes.Add("onclick", "javascript:return ValidateSave('" + txtCategoryName.ClientID + "');");
            EmpListPaging.FirstClick += new Paging.PageFirst(EmpListPaging_FirstClick);
            EmpListPaging.PreviousClick += new Paging.PagePrevious(EmpListPaging_FirstClick);
            EmpListPaging.NextClick += new Paging.PageNext(EmpListPaging_FirstClick);
            EmpListPaging.LastClick += new Paging.PageLast(EmpListPaging_FirstClick);
            EmpListPaging.ChangeClick += new Paging.PageChange(EmpListPaging_FirstClick);
            EmpListPaging.ShowRowsClick += new Paging.ShowRowsChange(EmpListPaging_ShowRowsClick);
            EmpListPaging.CurrentPage = 1;
            ModID = ModuleID;
        }
        void EmpListPaging_ShowRowsClick(object sender, EventArgs e)
        {
            BindPager();
        }
        void EmpListPaging_FirstClick(object sender, EventArgs e)
        {
            BindPager();
        }
        void BindPager()
        {
            objHrCommon.PageSize = EmpListPaging.ShowRows;
            objHrCommon.CurrentPage = EmpListPaging.CurrentPage;
            BindGrid(objHrCommon);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            Userid = Convert.ToInt32(Convert.ToInt32(Session["UserId"]).ToString());
            if (!IsPostBack)
            {
                //BindWorksites();
                Page.Form.Attributes.Add("enctype", "multipart/form-data");
                grdApproval.Visible = true;
                tblApproval.Visible = true;
                // pnlAll.Visible = true;
                //pnltblMain.Visible = false;
                tblMain.Visible = false;
                //pnltblAdvance.Visible = false;
                tblAdvance.Visible = false;
                tblASave.Visible = false;
                if (Request.QueryString.Count > 0)
                {
                    int id = 0;
                    if (Request.QueryString.AllKeys.Contains("key"))
                    {
                        id = Convert.ToInt32(Request.QueryString["key"].ToString());
                    }
                    if (id == 4)
                    {
                        lblstatus.Visible = false;
                        ddlStatus.Visible = false;
                    }
                    if (id == 7)
                    {
                        btnapprove.Visible = true;
                    }
                    else
                        btnapprove.Visible = false;
                }
                BindPager();
                txtSearchWorksite.Text = "";
            }
        }
        protected string ViewDetails(string LoanID)
        {
            return "javascript:return window.open('ViewAdvanceDetails.aspx?Id=" + LoanID + "', '_blank')";
        }
        protected void grdApproval_RowEditing(object sender, GridViewEditEventArgs e)
        {
        }
        public void BindGrid(HRCommon objHrCommon)
        {
            tblremarks.Visible = false;
            int EmpID = 0;
            if (txtEmpID.Text != "")
            {
                EmpID = Convert.ToInt32(txtEmpID.Text);
            }
            int WS = 0;
            try
            {
                // WS = Convert.ToInt32(ddlWs.SelectedValue);
                WS = Convert.ToInt32(ddlWs_hid.Value == "" ? "0" : ddlWs_hid.Value);
            }
            catch { }
            int DeptNo = 0;
            try
            {
                DeptNo = Convert.ToInt32(ddlDept.SelectedValue);
            }
            catch { }
            int Status = Convert.ToInt32(ddlStatus.SelectedItem.Value);
            int appstatus = 0;
            if (Request.QueryString.Count > 0)
            {
                int id = 0;
                if (Request.QueryString.AllKeys.Contains("key"))
                {
                    id = Convert.ToInt32(Request.QueryString["key"].ToString());
                }
                if (Request.QueryString.AllKeys.Contains("Apr"))
                {
                    appstatus = Convert.ToInt32(Request.QueryString["Apr"].ToString());
                }
                if (id == 4)
                {
                    Status = 4;
                }
                if (id == 5)
                {
                    Status = 5;
                }
                if (id == 6)
                {
                    Status = 6;
                }
                if (id == 7)
                {
                    Status = 7;
                }
                if (id == 8)
                {
                    Status = 8;
                }
                if (id == 3)
                {
                    Status = 3;
                }
                if (id == 0)
                {
                    Status = 0;
                }
            }
            DataSet ds = AttendanceDAC.HR_GetEmpAdvacesApproval(WS, DeptNo, EmpID, txtEmpName.Text, Status, objHrCommon, Convert.ToInt32(Session["CompanyID"]), appstatus);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                if (Status == 1 || Status == 4 || Status == 5)
                {
                    grdApproval.Columns[9].Visible = true;
                    grdApproval.Columns[10].Visible = true;
                    if (Status == 4)
                        grdApproval.Columns[6].Visible = true;
                    else
                        grdApproval.Columns[6].Visible = false;
                }
                else if (Status == 2)
                {
                    grdApproval.Columns[9].Visible = false;
                    grdApproval.Columns[10].Visible = true;
                }
                else if (Status == 3)
                {
                    grdApproval.Columns[9].Visible = true;
                    grdApproval.Columns[10].Visible = false;
                }
                grdApproval.DataSource = ds;
                grdApproval.DataBind();
                GetParentMenuId();
                EmpListPaging.Bind(objHrCommon.CurrentPage, objHrCommon.TotalPages, objHrCommon.NoofRecords, objHrCommon.PageSize);
            }
            else
            {
                grdApproval.DataSource = null;
                grdApproval.DataBind();
                EmpListPaging.Visible = false;
            }
        }
        public void GetParentMenuId()
        {
            string URL = Request.Url.Segments[Request.Url.Segments.Length - 1] + Request.Url.Query;
            int RoleId = Convert.ToInt32(Session["RoleId"].ToString());  
            int ModuleId = ModID;


            ProcDept objProc = new ProcDept();
            DataSet ds = ProcDept.GetAllowed(RoleId, ModuleId, URL);
            int MenuId = 0;

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                MenuId = Convert.ToInt32(ds.Tables[0].Rows[0]["Under"]);
                Editable = (bool)ds.Tables[0].Rows[0]["Editable"];
               
                if (Convert.ToBoolean(ds.Tables[0].Rows[0]["ViewAll"].ToString()) == false)
                {
                    grdApproval.Columns[12].Visible = false;// Convert.ToBoolean(ds.Tables[0].Rows[0]["Allowed"].ToString());
                    grdApproval.Columns[16].Visible = Convert.ToBoolean(ds.Tables[0].Rows[0]["Allowed"].ToString());
                    grdApproval.Columns[17].Visible = Convert.ToBoolean(ds.Tables[0].Rows[0]["Allowed"].ToString());

                    grdApproval.Columns[13].Visible = false; Convert.ToBoolean(ds.Tables[0].Rows[0]["Editable"].ToString());
                    grdApproval.Columns[14].Visible = Convert.ToBoolean(ds.Tables[0].Rows[0]["Editable"].ToString());
                    grdApproval.Columns[15].Visible = Convert.ToBoolean(ds.Tables[0].Rows[0]["Editable"].ToString());

                }
            }
           // return MenuId;
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            txtSearchWorksite.Text = string.Empty;
            txtdept.Text = string.Empty;
            EmpListPaging.CurrentPage = 1;
            BindPager();
        }
        protected string FormatMonth(object Status)
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
        protected string Format(object Status)
        {
            string retValue = "";
            string input = Status.ToString();
            if (input == "1")
            {
                retValue = "Instant";
            }
            if (input == "2")
            {
                retValue = "EMIs";
            }
            return retValue;
        }
        protected string Formatip(object Status)
        {
            string retValue = "";
            string input = Status.ToString();
            if (input == "0")
            {
                retValue = "NotPaid";
            }
            if (input == "1")
            {
                retValue = "Paid";
            }
            if (input == "False")
            {
                retValue = "NotPaid";
            }
            if (input == "True")
            {
                retValue = "Paid";
            }
            return retValue;
        }
        protected void grdApproval_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int status = 0;
            int LoanID = Convert.ToInt32(e.CommandArgument.ToString());
            int LoanTansID = 0;
            string Remarks = "";
            GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).Parent.Parent;
            TextBox txtremarks = (TextBox)row.FindControl("txtRemarks");
            if (txtremarks.Text.Trim() != "")
            {
                Remarks = txtremarks.Text.Trim();
            }
            if (e.CommandName == "Edit")
            {
                try
                {
                    //BindEmpList();
                    // int LoanID = Convert.ToInt32(e.CommandArgument);
                    ViewState["LoanID"] = LoanID;
                    BindYears();
                    btnFind.Visible = txtFilter.Visible = false;
                    DataSet ds = AttendanceDAC.HR_GetLoanDetails(LoanID);
                    txtSal.Text = Convert.ToDouble(ds.Tables[0].Rows[0]["Salary"]).ToString();
                    //  ddlEmp.SelectedValue = Convert.ToInt32(ds.Tables[0].Rows[0]["EmpID"]).ToString();
                    ddlAdjType.SelectedValue = Convert.ToInt32(ds.Tables[0].Rows[0]["LoanType"]).ToString();
                    int InstallmentType = Convert.ToInt32(ds.Tables[0].Rows[0]["InstallmentType"]);
                    TxtEmpy.Text = ds.Tables[0].Rows[0]["EmpName"].ToString();
                    ViewState["Empid"] = Convert.ToInt32(ds.Tables[0].Rows[0]["EmpID"]);
                    tblMain.Visible = true;
                    tblApproval.Visible = false;
                    tblLsave.Visible = false;
                    tblReduceSave.Visible = false;
                    tblAdvance.Visible = false;
                    tblReduce.Visible = false;
                    tblASave.Visible = false;
                    tblLoan.Visible = false;
                    int TypeofLoan = Convert.ToInt32(ds.Tables[0].Rows[0]["LoanType"].ToString());
                    int InstalmentType = Convert.ToInt32(ds.Tables[0].Rows[0]["InstallmentType"].ToString());
                    if (TypeofLoan == 1)
                    {
                        tblAdvance.Visible = true;
                        tblASave.Visible = true;
                        txtAdvAmt.Text = ds.Tables[0].Rows[0]["LoanAmount"].ToString();
                        txtAdvNM.Text = Convert.ToInt32(ds.Tables[0].Rows[0]["NoofEMIs"]).ToString();
                        ddlRecoverType.SelectedValue = Convert.ToInt32(ds.Tables[0].Rows[0]["RecoveryType"]).ToString();
                        txtAdvInstAmt.Text = Convert.ToInt32(ds.Tables[0].Rows[0]["InstallmentAmt"]).ToString();
                        txtAPaidOn.Text = ds.Tables[0].Rows[0]["IssuedOn"].ToString();
                        ddlYear.SelectedValue = Convert.ToInt32(ds.Tables[0].Rows[0]["RecoverYear"]).ToString();
                        ddlMonth.SelectedValue = Convert.ToInt32(ds.Tables[0].Rows[0]["RecoverMonth"]).ToString();
                        txtAdvGSal.Text = Convert.ToDouble(ds.Tables[0].Rows[0]["InstallmentPeriodSalary"]).ToString();
                        txtremarks.Text = ds.Tables[0].Rows[0]["Remarks"].ToString();
                    }
                    if (TypeofLoan == 2 && InstalmentType == 1)
                    {
                        tblLoan.Visible = true;
                        tblLsave.Visible = true;
                        ddlLoanRecoveryType.SelectedValue = Convert.ToInt32(ds.Tables[0].Rows[0]["InstallmentType"]).ToString();
                        txtLGrossSal.Text = Convert.ToDouble(ds.Tables[0].Rows[0]["InstallmentPeriodSalary"]).ToString();
                        txtLAmt.Text = ds.Tables[0].Rows[0]["LoanAmount"].ToString();
                        txtLRecoveryMonths.Text = Convert.ToInt32(ds.Tables[0].Rows[0]["NoofEMIs"]).ToString();
                        txtLIAmt.Text = Convert.ToInt32(ds.Tables[0].Rows[0]["InstallmentAmt"]).ToString();
                        txtST.Text = ds.Tables[0].Rows[0]["ServiceTax"].ToString();
                        txtLRI.Text = ds.Tables[0].Rows[0]["InterestRate"].ToString();
                        txtLPaidOn.Text = ds.Tables[0].Rows[0]["IssuedOn"].ToString();
                        ddlCurrentYear.SelectedValue = Convert.ToInt32(ds.Tables[0].Rows[0]["RecoverYear"]).ToString();
                        ddlCurrentMonth.SelectedValue = Convert.ToInt32(ds.Tables[0].Rows[0]["RecoverMonth"]).ToString();
                        txtLIns.Text = ds.Tables[0].Rows[0]["InterestRate"].ToString();
                    }
                    if (TypeofLoan == 2 && InstalmentType == 2)
                    {
                        tblReduceSave.Visible = true; tblReduce.Visible = true;
                        //pnltblReduce.Visible = true;
                        txtTAPaidOn.Text = ds.Tables[0].Rows[0]["IssuedOn"].ToString();
                        txtReduceEMIs.Text = Convert.ToInt32(ds.Tables[0].Rows[0]["NoofEMIs"]).ToString();
                        txtReduceLoanAmount.Text = ds.Tables[0].Rows[0]["LoanAmount"].ToString();
                        txtReduceRI.Text = ds.Tables[0].Rows[0]["InterestRate"].ToString();
                        txtReduceST.Text = ds.Tables[0].Rows[0]["ServiceTax"].ToString();
                        ddlRecoveryMonths.SelectedValue = Convert.ToInt32(ds.Tables[0].Rows[0]["RecoverMonth"]).ToString();
                    }
                }
                catch { AlertMsg.MsgBox(Page, "Unable to Some values!"); }
            }
            else if (e.CommandName == "HRext")
            {
                        Label lblHRext = (Label)row.FindControl("lblHRext");
                        string url = "../HMS/HRLoan/" + LoanID.ToString()+"." + lblHRext.Text;
                string fullURL = "window.open('" + url + "', '_blank' );";
                ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", fullURL, true);
            }
            else if (e.CommandName == "Approve")
            {
                if (Request.QueryString.Count > 0)
                {
                    int id = 0;
                    if (Request.QueryString.AllKeys.Contains("key"))
                    {
                        id = Convert.ToInt32(Request.QueryString["key"].ToString());
                    }
                    if (id == 0)
                    {
                        status = 5;
                        AttendanceDAC.HR_UpdAdvStatus(LoanID, status, Convert.ToInt32(Session["UserId"]), Remarks);
                        AlertMsg.MsgBox(Page, "Process Done LoanID#" + LoanID.ToString());
                        BindPager();
                    }
                    else if (id == 4)
                    {
                        status = 7;
                        string filename = "", HRext = string.Empty, path = "";
                        FileUpload fuc = (FileUpload)row.FindControl("fuUploadProof");
                        filename = fuc.PostedFile.FileName;
                        if (filename != "")
                        {
                            HRext = filename.Split('.')[filename.Split('.').Length - 1];
                        }
                        else
                        {
                            HRext = "";
                        }
                        AttendanceDAC.HR_UpdAdvStatus(LoanID, status, Convert.ToInt32(Session["UserId"]), Remarks);
                        if (filename != "")
                        {
                            SqlParameter[] parms1 = new SqlParameter[2];
                            parms1[0] = new SqlParameter("@loanID", LoanID);
                            parms1[1] = new SqlParameter("@HRext", HRext);
                            SQLDBUtil.ExecuteDataset("sh_LoanHRFileupload", parms1);
                            path = Server.MapPath("~\\hms\\HRLoan\\" + LoanID + "." + HRext);
                            fuc.PostedFile.SaveAs(path);
                        }
                        AlertMsg.MsgBox(Page, "Process Done LoanID#" + LoanID.ToString());
                        BindPager();
                    }
                    else if (id == 6)
                    {
                        status = 4;
                        AttendanceDAC.HR_UpdAdvStatus(LoanID, status, Convert.ToInt32(Session["UserId"]), Remarks);
                        AlertMsg.MsgBox(Page, "Process Done LoanID#" + LoanID.ToString());
                        BindPager();
                    }
                    else if (id == 5)
                    {
                        status = 6;
                        AttendanceDAC.HR_UpdAdvStatus(LoanID, status, Convert.ToInt32(Session["UserId"]), Remarks);
                        AlertMsg.MsgBox(Page, "Process Done LoanID#" + LoanID.ToString());
                        BindPager();
                    }
                    else if (id == 7)
                    {
                        status = 8;
                        AttendanceDAC.HR_UpdAdvStatus(LoanID, status, Convert.ToInt32(Session["UserId"]), Remarks);
                        AlertMsg.MsgBox(Page, "Process Done LoanID#" + LoanID.ToString());
                        BindPager();
                    }
                    else if (id == 8)
                    {
                        status = 2;
                        AttendanceDAC.HR_UpdAdvStatus(LoanID, status, Convert.ToInt32(Session["UserId"]), Remarks);
                        AlertMsg.MsgBox(Page, "Process Done LoanID#" + LoanID.ToString());
                        BindPager();
                    }
                }
                else
                {
                    status = 5;
                    AttendanceDAC.HR_UpdAdvStatus(LoanID, status, Convert.ToInt32(Session["UserId"]), Remarks);
                    AlertMsg.MsgBox(Page, "Process Done LoanID#" + LoanID.ToString());
                    BindPager();
                }
            }
            else if (e.CommandName == "view")
            {
                GridViewRow gvr = (GridViewRow)(((Control)e.CommandSource).NamingContainer);
                LinkButton lnkRevise = (LinkButton)gvr.FindControl("lnkView");
                lnkRevise.CssClass = "btn btn-success";
                tblremarks.Visible = true;
                SqlParameter[] parms1 = new SqlParameter[1];
                parms1[0] = new SqlParameter("@loanID", LoanID);
                DataSet ds = SQLDBUtil.ExecuteDataset("Sh_LoanRemarksView", parms1);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    gvRemarks.DataSource = ds.Tables[0];
                }
                else
                    gvRemarks.DataSource = null;
                gvRemarks.DataBind();
            }
            else if (e.CommandName == "Reject")
            {
                status = 3;
                AttendanceDAC.HR_UpdAdvStatus(LoanID, status, Convert.ToInt32(Session["UserId"]), Remarks);
                AlertMsg.MsgBox(Page, "Process Done LoanID#" + LoanID.ToString());
                BindPager();
            }
            else if (e.CommandName == "Del")
            {
                status = 0;
                AttendanceDAC.HR_UpdAdvStatus(LoanID, status, Convert.ToInt32(Session["UserId"]), Remarks);
                AlertMsg.MsgBox(Page, "Process Done LoanID#" + LoanID.ToString());
                BindPager();
            }
            if (status == 2)
            {
                // here i was added the advance  posting in AMS --> 14-05-2016
                DateTime TransTime = DateTime.Now;
                DataSet ds = AttendanceDAC.HR_GetEmpLoanDetatils(LoanID);
                int EmpID = 0; double CreditAmt = 0;int TransId = 0;
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    EmpID = Convert.ToInt32(ds.Tables[0].Rows[0]["EmpID"].ToString());
                    CreditAmt = Convert.ToDouble(ds.Tables[0].Rows[0]["LoanAmount"].ToString());
                    TransId = Convert.ToInt32(ds.Tables[0].Rows[0]["TransID"].ToString());
                }
                if (TransId == 0)
                {
                    string Reks = "Employee Advance for Employee : " + EmpID + "with loan id = " + LoanID;
                    AttendanceDAC ADAC = new AttendanceDAC();
                    LoanTansID = ADAC.HR_InsUpAdvanceTransaction(0, TransTime, Reks, CompanyID, EmpID, Convert.ToInt32(Session["UserId"]), CreditAmt);
                    ADAC.HR_UpdateLoanTransID(LoanID, LoanTansID);
                }
            }
        }
        public void BindYears()
        {
            DataSet ds = AttendanceDAC.GetCalenderYear();
            int i = 0;
            int Maxyear = Convert.ToInt32(ds.Tables[0].Rows[0]["MaxYear"].ToString());
            for (int Minyear = Convert.ToInt32(ds.Tables[0].Rows[0]["MinYear"].ToString()); Minyear <= Maxyear; Minyear++)
            {
                ddlYear.Items.Insert(i, new ListItem(Convert.ToString(Minyear), Convert.ToString(Minyear)));
                ddlCurrentYear.Items.Insert(i, new ListItem(Convert.ToString(Minyear), Convert.ToString(Minyear)));
                ddlReducingYear.Items.Insert(i, new ListItem(Convert.ToString(Minyear), Convert.ToString(Minyear)));
                i = i + 1;
            }
            ddlMonth.SelectedValue = ds.Tables[0].Rows[0]["PreviousMonth"].ToString();
            ViewState["PreviousMonth"] = ddlMonth.SelectedValue;
            ddlYear.SelectedValue = ds.Tables[0].Rows[0]["CurrentYear"].ToString();
            ViewState["CurrentYear"] = ddlYear.SelectedValue;
            ddlCurrentMonth.SelectedValue = ds.Tables[0].Rows[0]["PreviousMonth"].ToString();
            ddlCurrentYear.SelectedValue = ds.Tables[0].Rows[0]["CurrentYear"].ToString();//ddlYear.SelectedValue;
        }
        protected void txtLRecoveryMonths_TextChanged(object sender, EventArgs e)
        {
        }
        protected void ddlWs_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindDeparmetBySite(Convert.ToInt32(ddlWs.SelectedValue));
        }
        public void BindDeparmetBySite(int Site)
        {
            SiteSearch = Site;
            DataSet ds = AttendanceDAC.BindDeparmetBySite(Site, Convert.ToInt32(Session["CompanyID"]));
            ddlDept.DataSource = ds;
            ddlDept.DataTextField = "DeptName";
            ddlDept.DataValueField = "DepartmentUId";
            ddlDept.DataBind();
            ddlDept.Items.Insert(0, new ListItem("---ALL---", "0", true));
        }
        protected void btnFind_Click(object sender, EventArgs e)
        {
            //comemted by nadeem due to hidden field
            if (txtFilter.Text == "")
            {
                string EmpName = string.Empty;
                String SearchKey = string.Empty;
                DataSet ds = AttendanceDAC.HR_FilterSearchAll_googlesearch(SearchKey, EmpName);
            }
        }
        protected void ddlAdjType_SelectedIndexChanged(object sender, EventArgs e)
        {
            tblASave.Visible = true;
            if (Convert.ToInt32(ddlAdjType.SelectedValue) == 1)//Advance
            {
                tblAdvance.Visible = true;
                // pnltblAdvance.Visible = true;
                tblLoan.Visible = false;
                //pnltblLoan.Visible = false;
                tblReduce.Visible = false;
                //pnltblReduce.Visible = false;
                tblLsave.Visible = false;
                //tblReduceSave.Visible = false;
            }
            else
            {
                try
                {
                    DataSet ds = AttendanceDAC.HR_GetLoanOptions();
                    txtLRI.Text = ds.Tables[0].Rows[0][0].ToString();
                    txtReduceRI.Text = ds.Tables[0].Rows[0][2].ToString();
                    txtST.Text = ds.Tables[0].Rows[0][1].ToString();
                    txtReduceST.Text = ds.Tables[0].Rows[0][1].ToString();
                }
                catch { AlertMsg.MsgBox(Page, "Options Not set yet!"); }
            }
            if (Convert.ToInt32(ddlAdjType.SelectedValue) == 2)//Loan
            {
                tblAdvance.Visible = false;
                //pnltblAdvance.Visible = false;
                tblLoan.Visible = true;
                //pnltblLoan.Visible = true;
                tblASave.Visible = false;
                ddlLoanRecoveryType.SelectedIndex = 0;
                tblReduce.Visible = false;
                // pnltblReduce.Visible = false;
            }
            if (Convert.ToInt32(ddlAdjType.SelectedValue) == 3)//Travel Advance
            {
                tblAdvance.Visible = true;
                //pnltblAdvance.Visible = true;
                tblLoan.Visible = false;
                //pnltblLoan.Visible = false;
                ddlRecoverType.SelectedIndex = 1;
                tblReduce.Visible = false;
                //pnltblReduce.Visible = false;
            }
        }
        protected void ddlRecoverType_SelectedIndexChanged(object sender, EventArgs e)
        {
            int RType = Convert.ToInt32(ddlRecoverType.SelectedValue);
            if (RType == 1)
            {
                txtAdvNM.ReadOnly = true;
                txtAdvNM.Text = "1";
                if (txtAdvNM.Text != "")
                {
                    try
                    {
                        txtAdvInstAmt.Text = Convert.ToDouble(Convert.ToDouble((txtAdvAmt.Text)) / Convert.ToInt32((txtAdvNM.Text))).ToString();
                        txtAdvGSal.Text = Convert.ToDouble(Convert.ToDouble(txtSal.Text) - Convert.ToDouble(txtAdvInstAmt.Text)).ToString();
                        try
                        {
                            int IMEIs = Convert.ToInt32(txtAdvNM.Text);
                            if (IMEIs == 1)
                            {
                                ddlRecoverType.SelectedValue = "1";
                            }
                            else
                            {
                                ddlRecoverType.SelectedValue = "2";
                            }
                        }
                        catch { }
                    }
                    catch { AlertMsg.MsgBox(Page, "Enter Values Properly..!"); }
                }
            }
            else
            {
                txtAdvNM.ReadOnly = false;
                txtAdvNM.Text = "";
            }
        }
        protected void btnReduceSave_Click(object sender, EventArgs e)
        {
            try
            {
                // int EmpID = Convert.ToInt32(ddlEmp.SelectedValue);
                int EmpID = Convert.ToInt32(Convert.ToInt32(ddlEmp_hid.Value == "" ? "0" : ddlEmp_hid.Value));
                if (EmpID > 0)
                {
                    int Month = Convert.ToInt32(ddlMonth.SelectedValue);
                    int Year = Convert.ToInt32(ddlYear.SelectedValue);
                    DataSet ds = objatt.HR_EmpSalriesListByEmpID(EmpID, Month, Year);
                    string PaymentStatus = ds.Tables[0].Rows[0]["paymentStatus"].ToString();
                    if (PaymentStatus == "Paid")
                        AlertMsg.MsgBox(Page, "Selected month salary paid..!");
                    else
                        TravelAdvance();
                }
                else
                {
                    AlertMsg.MsgBox(Page, "Select Employee!");
                }
            }
            catch (Exception ex2)
            {
                AlertMsg.MsgBox(Page, ex2.Message.ToString(), AlertMsg.MessageType.Error);
            }
        }
        public void TravelAdvance()
        {
            int Status = 1;
            //  int EmpID = Convert.ToInt32(ddlEmp.SelectedValue);
            int EmpID = Convert.ToInt32(Convert.ToInt32(ddlEmp_hid.Value == "" ? "0" : ddlEmp_hid.Value));
            int Month = Convert.ToInt32(ddlRecoveryMonths.SelectedValue);
            int Year = Convert.ToInt32(ddlReducingYear.SelectedValue);
            DataSet dst = AttendanceDAC.HR_ValidatePayment(EmpID, Month, Year);
            if (dst != null && dst.Tables.Count > 0 && dst.Tables[0].Rows.Count > 0)
            {
                Status = Convert.ToInt32(dst.Tables[0].Rows[0]["Status"]);
            }
            if ((Status == 0) || (dst.Tables[0].Rows.Count == 0))
            {
                DataSet dsLoanDetail = new DataSet("LoanDataSet");
                DataTable dtTD = new DataTable("LoanTable");
                dtTD.Columns.Add(new DataColumn("LoanID", typeof(System.Int32)));
                dtTD.Columns.Add(new DataColumn("InstallmentNo", typeof(System.Int32)));
                dtTD.Columns.Add(new DataColumn("EMIAmount", typeof(System.Double)));
                dtTD.Columns.Add(new DataColumn("EmpID", typeof(System.Int32)));
                dtTD.Columns.Add(new DataColumn("Status", typeof(System.Int32)));
                dtTD.Columns.Add(new DataColumn("Intrest", typeof(System.Double)));
                dtTD.Columns.Add(new DataColumn("ServiceTax", typeof(System.Double)));
                dtTD.Columns.Add(new DataColumn("PrincipalAmt", typeof(System.Double)));
                dsLoanDetail.Tables.Add(dtTD);
                int LoanID;
                int TransID;
                if (Convert.ToInt32(ViewState["LoanID"]) == 0)
                {
                    LoanID = 0;
                    TransID = 0;
                }
                else
                {
                    LoanID = Convert.ToInt32(ViewState["LoanID"]);
                    DataSet ds = AttendanceDAC.HR_getTransID(LoanID);
                    TransID = Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());
                }
                EmpID = Convert.ToInt32(Convert.ToInt32(ddlEmp_hid.Value == "" ? "0" : ddlEmp_hid.Value));
                int InstallmentType = 2;// Convert.ToInt32(ddlReduce.SelectedValue);
                int RecoverYear = Convert.ToInt32(ddlReducingYear.SelectedValue);
                int RecoverMonth = Convert.ToInt32(ddlRecoveryMonths.SelectedValue);
                int IssuedBy = Convert.ToInt32(Session["UserId"]);
                //  if (Convert.ToInt32(ddlReducePaymentType.SelectedValue) == 1) { CrLedgerId = Convert.ToInt32(lstReduceCash.SelectedValue); } else { CrLedgerId = Convert.ToInt32(lstReduceBank.SelectedValue); }
                int RecoveryType = 2; double CreditAmt = Convert.ToDouble(txtReduceLoanAmount.Text);
                int LoanType = Convert.ToInt32(ddlAdjType.Text); DateTime LoanIssuedDate = DateTime.Now;
                // int PaidLedgerID = CrLedgerId;
                string Remarks = ddlAdjType.SelectedItem.Text; double InstallmentAmt = 0;
                double LoanAmount = Convert.ToDouble(txtReduceLoanAmount.Text);
                int NoofEMIs = Convert.ToInt32(txtReduceEMIs.Text);
                double InterestRate = Convert.ToDouble(txtReduceRI.Text);
                double ServiceTax = Convert.ToDouble(txtReduceST.Text);
                DateTime TransTime = CODEUtility.ConvertToDate(txtTAPaidOn.Text.Trim(), DateFormat.DayMonthYear);
                AttendanceDAC ADAC = new AttendanceDAC();
                // TransID = Convert.ToInt32(ADAC.HR_InsUpAdvanceTransaction(TransID, TransTime, Remarks, CompanyID, EmpID,  Convert.ToInt32(Session["UserId"]), CreditAmt));//ledgerid for debit transaction needed,vocherid
                TransID = 0;
                string LoanRemarks = "";
                int LoanIDNo = Convert.ToInt32(ADAC.HR_InsUpLoansReturnLoanID(LoanID, EmpID, LoanAmount, LoanType, LoanIssuedDate, RecoverYear, RecoverMonth, NoofEMIs, InstallmentAmt, InterestRate, TransID, RecoveryType, IssuedBy, ServiceTax, InstallmentType, LoanRemarks, ""));
                int RowCount = 0;
                double Principle = LoanAmount;
                for (int i = 0; i < NoofEMIs; i++)
                {
                    Double PerMonth = LoanAmount / NoofEMIs;
                    int amt = Convert.ToInt32(Principle) / 100;
                    double MI = Convert.ToDouble(amt * (Convert.ToDouble(txtReduceRI.Text) / 10));
                    Double ST = MI * (ServiceTax / 1000);
                    double EMI = PerMonth + MI + ST;
                    Principle = Principle - PerMonth;
                    RowCount = RowCount + 1;
                    DataRow drTD = dtTD.NewRow();
                    drTD["LoanID"] = LoanIDNo;
                    drTD["InstallmentNo"] = RowCount;
                    drTD["EMIAmount"] = EMI;
                    drTD["EmpID"] = EmpID;
                    drTD["Status"] = 0;
                    drTD["Intrest"] = MI;
                    drTD["ServiceTax"] = ST;
                    drTD["PrincipalAmt"] = PerMonth;
                    dtTD.Rows.Add(drTD);
                }
                dsLoanDetail.AcceptChanges();
                DataSet dsTD = AttendanceDAC.HR_InsLoanDetailsXML(dsLoanDetail);
                tblMain.Visible = false;
                //pnltblMain.Visible = false;
                tblReduce.Visible = false;
                //pnltblReduce.Visible = false;
                tblReduceSave.Visible = false;
                //BindPager(1, 2, 2);
            }
            else
            {
                AlertMsg.MsgBox(Page, "Selected month salary already paid.");
            }
        }
        protected void txtAdvNM_TextChanged(object sender, EventArgs e)
        {
            if (txtAdvNM.Text != "")
            {
                try
                {
                    txtAdvInstAmt.Text = Convert.ToDouble(Convert.ToDouble((txtAdvAmt.Text)) / Convert.ToInt32((txtAdvNM.Text))).ToString();
                    txtAdvGSal.Text = Convert.ToDouble(Convert.ToDouble(txtSal.Text) - Convert.ToDouble(txtAdvInstAmt.Text)).ToString();
                    try
                    {
                        int IMEIs = Convert.ToInt32(txtAdvNM.Text);
                        if (IMEIs == 1)
                        {
                            ddlRecoverType.SelectedValue = "1";
                        }
                        else
                        {
                            ddlRecoverType.SelectedValue = "2";
                        }
                    }
                    catch { }
                }
                catch { AlertMsg.MsgBox(Page, "Enter Values Properly..!"); }
            }
        }
        protected void btnAdvSave_Click(object sender, EventArgs e)
        {
            try
            {
                //int EmpID = Convert.ToInt32(ddlEmp.SelectedValue);
                int EmpID = Convert.ToInt32(Convert.ToInt32(ddlEmp_hid.Value == "" ? "0" : ddlEmp_hid.Value));
                if (EmpID == 0)
                {
                    EmpID = Convert.ToInt32(ViewState["Empid"]);
                }
                if (EmpID > 0)
                {
                    int Month = Convert.ToInt32(ddlMonth.SelectedItem.Value);
                    int Year = Convert.ToInt32(ddlYear.SelectedItem.Value);
                    DataSet ds = objatt.HR_EmpSalriesListByEmpID(EmpID, Month, Year);
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        string PaymentStatus = ds.Tables[0].Rows[0]["paymentStatus"].ToString();
                        if (PaymentStatus == "Paid")
                        {
                            if (Month != 12)
                            {
                                InsertAdvance(Month + 1, Year);
                            }
                            else
                            {
                                Month = 1;
                                InsertAdvance(Month, Year + 1);
                            }
                        }
                        else
                        {
                            InsertAdvance(Month, Year);
                        }
                    }
                    else
                    {
                        InsertAdvance(Month, Year);
                    }
                }
                else
                {
                    AlertMsg.MsgBox(Page, "Select Employee!");
                }
            }
            catch (Exception ex)
            {
                AlertMsg.MsgBox(Page, ex.Message.ToString(), AlertMsg.MessageType.Error);
            }
        }
        public void InsertAdvance(int Month, int Year)
        {
            int Status = 1;
            //  int EmpID = Convert.ToInt32(ddlEmp.SelectedValue);
            int EmpID = Convert.ToInt32(Convert.ToInt32(ddlEmp_hid.Value == "" ? "0" : ddlEmp_hid.Value));
            if (EmpID == 0)
            {
                EmpID = Convert.ToInt32(ViewState["Empid"]);
            }
            DataSet dsLoanDetail = new DataSet("LoanDataSet");
            DataTable dtTD = new DataTable("LoanTable");
            dtTD.Columns.Add(new DataColumn("LoanID", typeof(System.Int32)));
            dtTD.Columns.Add(new DataColumn("InstallmentNo", typeof(System.Int32)));
            dtTD.Columns.Add(new DataColumn("EMIAmount", typeof(System.Double)));
            dtTD.Columns.Add(new DataColumn("EmpID", typeof(System.Int32)));
            dtTD.Columns.Add(new DataColumn("Status", typeof(System.Int32)));
            dtTD.Columns.Add(new DataColumn("Intrest", typeof(System.Double)));
            dtTD.Columns.Add(new DataColumn("ServiceTax", typeof(System.Double)));
            dtTD.Columns.Add(new DataColumn("PrincipalAmt", typeof(System.Double)));
            dsLoanDetail.Tables.Add(dtTD);
            int LoanID;
            int TransID; //Payment TransID
            if (Convert.ToInt32(ViewState["LoanID"]) == 0)
            {
                LoanID = 0;
                TransID = 0;
            }
            else
            {
                LoanID = Convert.ToInt32(ViewState["LoanID"]);
                DataSet ds = AttendanceDAC.HR_getTransID(LoanID);
                TransID = Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());
            }
            double LoanAmount = Convert.ToDouble(txtAdvAmt.Text);
            int LoanType = Convert.ToInt32(ddlAdjType.Text);
            DateTime LoanIssuedDate = DateTime.Now;
            int RecoverYear = Year;
            int RecoverMonth = Month;
            int NoofEMIs = Convert.ToInt32(txtAdvNM.Text);
            double InstallmentAmt = Convert.ToDouble(txtAdvInstAmt.Text);
            double InterestRate = 0;
            tblLoan.Visible = false;
            tblAdvance.Visible = false;
            tblMain.Visible = false;
            double ServiceTax = 0;
            string Remarks = ddlAdjType.SelectedItem.Text;
            double CreditAmt = Convert.ToDouble(txtAdvAmt.Text);
            int InstallmentType = 0;
            int RecoveryType = Convert.ToInt32(ddlRecoverType.SelectedValue);
            int IssuedBy = Convert.ToInt32(Session["UserId"]);
            DateTime TransTime = CODEUtility.ConvertToDate(txtAPaidOn.Text.Trim(), DateFormat.DayMonthYear);
            AttendanceDAC ADAC = new AttendanceDAC();
            TransID = 0;
            string LoanRemarks = txtremarks.Text;
            int LoanIDNo = Convert.ToInt32(ADAC.HR_InsUpLoansReturnLoanID(LoanID, EmpID, LoanAmount, LoanType, LoanIssuedDate, RecoverYear, RecoverMonth, NoofEMIs, InstallmentAmt, InterestRate, TransID, RecoveryType, IssuedBy, ServiceTax, InstallmentType, LoanRemarks, ""));
            int RowCount = 0;
            double Principle = LoanAmount;
            for (int i = 0; i < NoofEMIs; i++)
            {
                Double PerMonth = LoanAmount / NoofEMIs;
                int amt = Convert.ToInt32(Principle) / 100;
                double EMI = PerMonth;// +MI + ST;
                Principle = Principle - PerMonth;
                RowCount = RowCount + 1;
                DataRow drTD = dtTD.NewRow();
                drTD["LoanID"] = LoanIDNo;
                drTD["InstallmentNo"] = RowCount;
                drTD["EMIAmount"] = EMI;
                drTD["EmpID"] = EmpID;
                drTD["Status"] = 0;
                drTD["Intrest"] = 0;
                drTD["ServiceTax"] = 0;
                drTD["PrincipalAmt"] = PerMonth;
                dtTD.Rows.Add(drTD);
            }
            dsLoanDetail.AcceptChanges();
            DataSet dsTD = AttendanceDAC.HR_InsLoanDetailsXML(dsLoanDetail);
            DataSet dsLaonID = AttendanceDAC.HR_GetLoanDetailsByID(LoanIDNo, Convert.ToInt32(Session["UserId"]), ConfigurationManager.AppSettings["CompanyID"]);
            tblASave.Visible = false;
            int Emp = 0;
            try
            {
                if (txtEmpID.Text != "")
                {
                    Emp = Convert.ToInt32(txtEmpID.Text);
                }
            }
            catch { Emp = 0; }
            int SMonth;
            SMonth = Convert.ToInt32(ddlMonth.SelectedItem.Value);
            if (Month != SMonth)
            {
                AlertMsg.MsgBox(Page, "Selected month salary already paid,Process Done for next month of the selected month LoanID#" + LoanIDNo.ToString());//+ " TransactionID#" + TransID.ToString()
            }
            else
            {
                AlertMsg.MsgBox(Page, "Process Done LoanID#" + LoanIDNo.ToString());//+ " TransactionID#" + TransID.ToString()
            }
            tblASave.Visible = false;
            BindPager();
        }
        protected void btnAdvCancel_Click(object sender, EventArgs e)
        {
            Refresh();
        }
        public void Refresh()
        {
            txtAdvAmt.Text = txtAdvGSal.Text = txtAdvInstAmt.Text = txtAdvNM.Text = "";
            txtFilter.Text = txtLAmt.Text = txtLGrossSal.Text = txtLIAmt.Text = txtLIns.Text = "";
            txtLRecoveryMonths.Text = txtLRI.Text = txtSal.Text = "";
            ddlAdjType.SelectedIndex = 0;
            //  ddlEmp.SelectedIndex = 0;
            TxtEmpy.Text = String.Empty;
        }
        protected void ddlLoanRecoveryType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(ddlLoanRecoveryType.SelectedValue) == 2)
            {
                tblReduce.Visible = true;
                tblMain.Visible = true;
                tblAdvance.Visible = false;
                tblLoan.Visible = false;
                tblLsave.Visible = false;
            }
            else
            {
                tblReduce.Visible = false;
                tblLsave.Visible = true;
            }
        }
        protected void txtLRI_TextChanged(object sender, EventArgs e)
        {
            if (txtLRI.Text != "")
            {
                Double ST = Convert.ToDouble(txtST.Text);
                Double amt = Convert.ToDouble(txtLAmt.Text) / 100;
                double MI = Convert.ToDouble(amt * (Convert.ToDouble(txtLRI.Text) / 10));
                Double STAmt = MI + (MI * (ST / 1000));
                txtLIns.Text = STAmt.ToString();
                double installAmt = Convert.ToDouble(Convert.ToDouble(txtLAmt.Text) / Convert.ToDouble(txtLRecoveryMonths.Text));
                Double LIamt = Convert.ToDouble(installAmt + Convert.ToDouble(txtLIns.Text));
                txtLIAmt.Text = LIamt.ToString();
                txtLGrossSal.Text = Convert.ToDouble(Convert.ToDouble(txtSal.Text) - Convert.ToDouble(txtLIAmt.Text)).ToString();
            }
        }
        protected void btnLSave_Click(object sender, EventArgs e)
        {
            try
            {
                // int EmpID = Convert.ToInt32(ddlEmp.SelectedValue);
                int EmpID = Convert.ToInt32(Convert.ToInt32(ddlEmp_hid.Value == "" ? "0" : ddlEmp_hid.Value));
                if (EmpID > 0)
                {
                    int Month = Convert.ToInt32(ddlMonth.SelectedValue);
                    int Year = Convert.ToInt32(ddlYear.SelectedValue);
                    DataSet ds = objatt.HR_EmpSalriesListByEmpID(EmpID, Month, Year);
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        string PaymentStatus = ds.Tables[0].Rows[0]["paymentStatus"].ToString();
                        if (PaymentStatus == "Paid")
                        {
                            if (Month != 12)
                            {
                                Loan(Month + 1, Year);
                            }
                            else
                            {
                                Loan(Month + 1, Year + 1);
                            }
                        }
                        else
                        {
                            Loan(Month, Year);
                        }
                    }
                    else
                    {
                        if (Month != 12)
                        {
                            Loan(Month + 1, Year);
                        }
                        else
                        {
                            Loan(Month + 1, Year + 1);
                        }
                    }
                }
                else
                {
                    AlertMsg.MsgBox(Page, "Select Employee!");
                }
            }
            catch (Exception ex1)
            {
                AlertMsg.MsgBox(Page, ex1.Message.ToString(), AlertMsg.MessageType.Error);
            }
        }
        protected void btnLCancel_Click(object sender, EventArgs e)
        {
            Refresh();
        }
        public bool HRVisible(string ext)
        {
            if (ext.Trim() == "NUll" || ext.Trim() == string.Empty || ext.Trim() == "0" || ext.Trim() == "False")
                return false;
            else
            {
                if (Request.QueryString.Count > 0)
                {
                    int id = 0;
                    if (Request.QueryString.AllKeys.Contains("key"))
                    {
                        id = Convert.ToInt32(Request.QueryString["key"].ToString());
                    }
                    if (id == 7 || id == 8)
                        return true;
                }
                return false;
            }
        }
        public void Loan(int Month, int Year)
        {
            int Status = 1;
            //   int EmpID = Convert.ToInt32(ddlEmp.SelectedValue);
            int EmpID = Convert.ToInt32(Convert.ToInt32(ddlEmp_hid.Value == "" ? "0" : ddlEmp_hid.Value));
            DataSet dsLoanDetail = new DataSet("LoanDataSet");
            DataTable dtTD = new DataTable("LoanTable");
            dtTD.Columns.Add(new DataColumn("LoanID", typeof(System.Int32)));
            dtTD.Columns.Add(new DataColumn("InstallmentNo", typeof(System.Int32)));
            dtTD.Columns.Add(new DataColumn("EMIAmount", typeof(System.Double)));
            dtTD.Columns.Add(new DataColumn("EmpID", typeof(System.Int32)));
            dtTD.Columns.Add(new DataColumn("Status", typeof(System.Int32)));
            dtTD.Columns.Add(new DataColumn("Intrest", typeof(System.Double)));
            dtTD.Columns.Add(new DataColumn("ServiceTax", typeof(System.Double)));
            dtTD.Columns.Add(new DataColumn("PrincipalAmt", typeof(System.Double)));
            dsLoanDetail.Tables.Add(dtTD);
            int LoanID;
            int TransID;
            if (Convert.ToInt32(ViewState["LoanID"]) == 0)
            {
                LoanID = 0;
                TransID = 0;
            }
            else
            {
                LoanID = Convert.ToInt32(ViewState["LoanID"]);
                DataSet ds = AttendanceDAC.HR_getTransID(LoanID);
                TransID = Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());
            }
            // EmpID = Convert.ToInt32(ddlEmp.SelectedValue);
            EmpID = Convert.ToInt32(Convert.ToInt32(ddlEmp_hid.Value == "" ? "0" : ddlEmp_hid.Value));
            double LoanAmount = Convert.ToDouble(txtLAmt.Text);
            int LoanType = Convert.ToInt32(ddlAdjType.Text);
            DateTime LoanIssuedDate = DateTime.Now;
            int RecoverYear = Year;// Convert.ToInt32(ddlCurrentYear.SelectedValue);
            int RecoverMonth = Month;// Convert.ToInt32(ddlCurrentMonth.SelectedValue);
            int NoofEMIs = Convert.ToInt32(txtLRecoveryMonths.Text);
            double InstallmentAmt = Convert.ToDouble(txtLIAmt.Text);
            double InterestRate = Convert.ToDouble(txtLRI.Text);
            double ServiceTax = Convert.ToDouble(txtST.Text);
            tblLsave.Visible = false;
            tblLoan.Visible = false;
            //pnltblLoan.Visible = false;
            tblAdvance.Visible = false;
            //pnltblAdvance.Visible = false;
            tblMain.Visible = false;
            // pnltblMain.Visible = false;
            string Remarks = ddlAdjType.SelectedItem.Text;
            double CreditAmt = Convert.ToDouble(txtLAmt.Text);
            DataSet dsLedID = AttendanceDAC.HR_EmpLedID(EmpID);
            int LedgerId = Convert.ToInt32(dsLedID.Tables[0].Rows[0][0]);
            DataSet VID = AttendanceDAC.HR_VocherID(CompanyID, EmpID);
            int IssuedBy = Convert.ToInt32(Session["UserId"]);
            int RecoveryType = 2;
            DateTime TransTime = CODEUtility.ConvertToDate(txtLPaidOn.Text.Trim(), DateFormat.DayMonthYear);
            int InstallmentType = 1;// Fixed Loan
            AttendanceDAC ADAC = new AttendanceDAC();
            //   TransID = Convert.ToInt32(ADAC.HR_InsUpAdvanceTransaction(TransID, TransTime, Remarks, CompanyID, EmpID,  Convert.ToInt32(Session["UserId"]), CreditAmt));//ledgerid for debit transaction needed,vocherid
            TransID = 0;
            string LoanRemarks = "";
            int LoanIDNo = Convert.ToInt32(ADAC.HR_InsUpLoansReturnLoanID(LoanID, EmpID, LoanAmount, LoanType, LoanIssuedDate, RecoverYear, RecoverMonth, NoofEMIs, InstallmentAmt, InterestRate, TransID, RecoveryType, IssuedBy, ServiceTax, InstallmentType, LoanRemarks, ""));
            int RowCount = 0;
            double Principle = LoanAmount;
            for (int i = 0; i < NoofEMIs; i++)
            {
                Double PerMonth = LoanAmount / NoofEMIs;
                int amt = Convert.ToInt32(Principle) / 100;
                double MI = Convert.ToDouble(amt * (Convert.ToDouble(txtLRI.Text) / 10));
                Double ST = MI * (ServiceTax / 1000);
                double EMI = PerMonth + MI + ST;
                Principle = Principle - PerMonth;
                RowCount = RowCount + 1;
                DataRow drTD = dtTD.NewRow();
                drTD["LoanID"] = LoanIDNo;
                drTD["InstallmentNo"] = RowCount;
                drTD["EMIAmount"] = EMI;
                drTD["EmpID"] = EmpID;
                drTD["Status"] = 0;
                drTD["Intrest"] = MI;
                drTD["ServiceTax"] = ST;
                drTD["PrincipalAmt"] = PerMonth;
                dtTD.Rows.Add(drTD);
            }
            dsLoanDetail.AcceptChanges();
            DataSet dsTD = AttendanceDAC.HR_InsLoanDetailsXML(dsLoanDetail);
            //tblLoanView.Visible = true;
            int SMonth;
            SMonth = Convert.ToInt32(ddlMonth.SelectedItem.Value);
            if (Month != SMonth)
            {
                AlertMsg.MsgBox(Page, "Selected month salary already paid,Process Done for next month of the selected month LoanID#" + LoanIDNo.ToString());//+ " TransactionID#" + TransID.ToString()
            }
            else
            {
                AlertMsg.MsgBox(Page, "Process Done LoanID#" + LoanIDNo.ToString());//+ " TransactionID#" + TransID.ToString()
            }
            Response.Redirect("EmpAdvanceApproval.aspx");
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "alert('Record Inserted..');window.location='EmpAdvanceApproval.aspx';", true);
            //}
            //else
            //{
            //    AlertMsg.MsgBox(Page, "Selected month salary already paid.");
            //}
        }
        protected void gvApproval_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow
      && (
          e.Row.RowState == DataControlRowState.Alternate
          || e.Row.RowState == DataControlRowState.Normal
          || e.Row.RowState == DataControlRowState.Selected
      ))
            {
                LinkButton lnlEdit = (LinkButton)e.Row.FindControl("lnlEdit");
                // System.Data.DataRow dataRecord = (System.Data.DataRow)e.Row.DataItem;
                //if (rbAdvanceView.SelectedItem.Value == "0")
                //{
                //    lnlEdit.Visible = false;
                //}
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (Request.QueryString.Count > 0)
                {
                    int id = -1;
                    FileUpload fuUploadProof = (FileUpload)e.Row.FindControl("fuUploadProof");
                    fuUploadProof.Visible = false;
                    if (Request.QueryString.AllKeys.Contains("key"))
                    {
                        id = Convert.ToInt32(Request.QueryString["key"].ToString());
                    }
                    if (id == 7)
                    {
                        CheckBox chkSelect = (CheckBox)e.Row.FindControl("chkSelect");
                        chkSelect.Visible = true;
                        btnapprove.Visible = true;
                    }
                    else
                    {
                        CheckBox chkSelect = (CheckBox)e.Row.FindControl("chkSelect");
                        chkSelect.Visible = false;
                        btnapprove.Visible = false;
                    }
                    if (id == 4)
                        fuUploadProof.Visible = true;
                    //if (id == 7 || id == 8)
                    //{
                    //    LinkButton lnkHRext = (LinkButton)e.Row.FindControl("lnkHRext");
                    //    lnkHRext.Text = "View";
                    //    lnkHRext.Visible = true;
                    //}
                    if (id == 4 || id == 7 || id == 8)
                    {
                        LinkButton lnkApprove = (LinkButton)e.Row.FindControl("lnkApprove");
                        lnkApprove.Text = "Approve";
                        lnkApprove.Visible = true;
                        LinkButton lnkReject = (LinkButton)e.Row.FindControl("lnkReject");
                        lnkReject.Visible = true;
                        LinkButton lnkEdit = (LinkButton)e.Row.FindControl("lnlEdit");
                        lnkEdit.Visible = true;
                    }
                    if (id == 5)
                    {
                        LinkButton lnkApprove = (LinkButton)e.Row.FindControl("lnkApprove");
                        lnkApprove.Text = "PM Approval";
                        lnkApprove.Visible = true;
                        LinkButton lnkReject = (LinkButton)e.Row.FindControl("lnkReject");
                        lnkReject.Visible = true;
                        LinkButton lnkEdit = (LinkButton)e.Row.FindControl("lnlEdit");
                        lnkEdit.Visible = true;
                    }
                    if (id == 6)
                    {
                        LinkButton lnkApprove = (LinkButton)e.Row.FindControl("lnkApprove");
                        lnkApprove.Text = "Approve";
                        lnkApprove.Visible = true;
                        LinkButton lnkReject = (LinkButton)e.Row.FindControl("lnkReject");
                        lnkReject.Visible = true;
                        LinkButton lnkEdit = (LinkButton)e.Row.FindControl("lnlEdit");
                        lnkEdit.Visible = true;
                    }
                    if (id == 3 || id == 0)
                    {
                        LinkButton lnkApprove = (LinkButton)e.Row.FindControl("lnkApprove");
                        lnkApprove.Visible = false;
                        LinkButton lnkReject = (LinkButton)e.Row.FindControl("lnkReject");
                        lnkReject.Visible = false;
                        LinkButton lnkEdit = (LinkButton)e.Row.FindControl("lnlEdit");
                        lnkEdit.Visible = false;
                    }
                }
            }
        }
        protected void GetEmpList(object sender, EventArgs e)
        {
            // EmpName = Convert.ToInt32(ddlEmp_hid.Value == "" ? "0" : ddlEmp_hid.Value);
            // EmpID = Convert.ToInt32(ddlEmp_hid.Value == "" ? "0" : ddlEmp_hid.Value);
            int EmpID = Convert.ToInt32(Convert.ToInt32(ddlEmp_hid.Value == "" ? "0" : ddlEmp_hid.Value));
            DataSet ds = AttendanceDAC.HR_GetEmpSal(EmpID);
            if (ds.Tables[0].Rows.Count > 0)
                txtSal.Text = Convert.ToInt32(Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString()) / 12).ToString();
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionEmpList(string prefixText, int count, string contextKey)
        {
            DataSet ds = AttendanceDAC.HR_FilterSearchAll_googlesearch(prefixText, EmpName);
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
            //SqlParameter[] param = new SqlParameter[2];
            //param[0] = new SqlParameter("@Search", txtSearchWorksite.Text);
            //param[1] = new SqlParameter("@CompanyID", Convert.ToInt32(Session["CompanyID"]));
            //// FIllObject.FillDropDown(ref ddlWorksite, "G_GET_WorkSitebyFilter", param);
            //FIllObject.FillDropDown(ref ddlWs, "G_GET_WorkSitebyFilter", param);
            //ListItem itmSelected = ddlWs.Items.FindByText(txtSearchWorksite.Text);
            //if (itmSelected != null)
            //{
            //    ddlWs.SelectedItem.Selected = false;
            //    itmSelected.Selected = true;
            //}
            //ddlWs_SelectedIndexChanged(sender, e);
            //txtdept.Focus();
            SqlParameter[] par = new SqlParameter[1];
            par[0] = new SqlParameter("@Search", txtSearchWorksite.Text);
            FIllObject.FillDropDown(ref ddlWs, "HR_GetWorkSite_By_LoanAdv_googlesearch", par);
            ListItem itmSelected = ddlWs.Items.FindByText(txtSearchWorksite.Text);
            if (itmSelected != null)
            {
                ddlWs.SelectedItem.Selected = false;
                itmSelected.Selected = true;
            }
            ddlWs_SelectedIndexChanged(sender, e);
            txtdept.Focus();
        }
        protected void GetDept(object sender, EventArgs e)
        {
            //SqlParameter[] param = new SqlParameter[3];
            //param[0] = new SqlParameter("@Search", txtdept.Text);
            //param[1] = new SqlParameter("@CompanyID", SearchCompanyID);
            //param[2] = new SqlParameter("@DeptID", Empdeptid);
            //FIllObject.FillDropDown(ref ddlDept, "G_GET_DesignationbyFilter", param);
            SqlParameter[] parm = new SqlParameter[3];
            parm[0] = new SqlParameter("@search ", txtdept.Text);
            parm[1] = new SqlParameter("@SiteID", Site);
            parm[2] = new SqlParameter("@CompanyID", CompanyID);
            FIllObject.FillDropDown(ref ddlDept, "HMS_GetDepartmentBySite_googlesearch", parm);
            ListItem itmSelected = ddlDept.Items.FindByText(txtdept.Text);
            if (itmSelected != null)
            {
                ddlDept.SelectedItem.Selected = false;
                itmSelected.Selected = true;
            }
            ddlDept_SelectedIndexChanged(sender, e);
        }
        protected void ddlDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            int deptid = Convert.ToInt32(ddlDept.SelectedItem.Value);
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionList(string prefixText, int count, string contextKey)
        {
            //DataSet ds = AttendanceDAC.GetGoogleABCSearchWorkSite(prefixText, SearchCompanyID);
            //return ConvertStingArray(ds);// txtItems.ToArray();
            //DataSet ds = AttendanceDAC.GetSearchworksite_by_LoanAdv(prefixText);
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
        public static string[] GetCompletionListEmp(string prefixText, int count, string contextKey)
        {
            DataSet ds = AttendanceDAC.GetEmployee(prefixText, SearchCompanyID, Empdeptid);
            return ConvertStingArray(ds);// txtItems.ToArray();
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionListDep(string prefixText, int count, string contextKey)
        {
            //DataSet ds = AttendanceDAC.GetSearchDesiginationFilter(prefixText, SearchCompanyID, Empdeptid);
            //return ConvertStingArray(ds);// txtItems.ToArray();
            DataSet ds = AttendanceDAC.BindDeparmetBySite_googlesearch(prefixText, SiteSearch, CompanyID);
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
        public static string[] ConvertStingArray(DataSet ds)
        {
            string[] rtval = Array.ConvertAll(ds.Tables[0].Select(), new Converter<DataRow, string>(DataRowToDouble));
            return rtval;
        }
        public static string DataRowToDouble(DataRow dr)
        {
            return dr["Name"].ToString();
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
        string ReturnVal = "";
        public string DocNavigateUrl(string Ext, string LoanID)
        {
            ReturnVal = "";
            if (LoanID != "" && Ext != "")
            {
                ReturnVal = "../HMS/EmpLoan/" + Convert.ToInt32(LoanID) + '.' + Ext;
            }
            return ReturnVal;
        }
        protected void btnapprove_Click(object sender, EventArgs e)
        {
            int status = 8;
            if (grdApproval.Rows.Count > 0)
            {
                foreach (GridViewRow gvr in grdApproval.Rows)
                {
                    CheckBox chkPrereq = new CheckBox();
                    chkPrereq = (CheckBox)gvr.Cells[0].FindControl("chkSelect");
                    Label LoanID = new Label();
                    LoanID = (Label)gvr.Cells[1].FindControl("lblLoanID");
                    TextBox txtRemarks = new TextBox();
                    txtRemarks = (TextBox)gvr.Cells[1].FindControl("txtRemarks");
                    AttendanceDAC.HR_UpdAdvStatus(Convert.ToInt32(LoanID.Text), status, Convert.ToInt32(Session["UserId"]), txtRemarks.Text.Trim());
                }
                AlertMsg.MsgBox(Page, "Approved Sucessfully");
            }
            else
                AlertMsg.MsgBox(Page, "No Records found", AlertMsg.MessageType.Warning);
            BindPager();
        }
    }
}