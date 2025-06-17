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
    public partial class AbsentPenalities : AECLOGIC.ERP.COMMON.WebFormMaster
    {
        int mid = 0;
        bool viewall, Editable;
        static int SearchCompanyID;
        static int Empdeptid = 0;
        string menuname;
        string menuid;
        private GridSort objSort;
        int CompanyID = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["CompanyID"].ToString());
        AttendanceDAC objAtt;
        DataSet ds;
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


        }

        #region Approved
        void EmpReimbursementAprovedPaging_ShowRowsClick(object sender, EventArgs e)
        {
            EmpReimbursementAprovedPaging.CurrentPage = 1;
            BindPager();
        }
        void EmpReimbursementAprovedPaging_FirstClick(object sender, EventArgs e)
        {
            BindPager();
        }
        void BindPager()
        {

            objHrCommon.PageSize = EmpReimbursementAprovedPaging.CurrentPage;
            objHrCommon.CurrentPage = EmpReimbursementAprovedPaging.ShowRows;
            EmployeeApproved(objHrCommon);


            //objHrCommon.PageSize = ReimburseItemsPaging.CurrentPage;
            //objHrCommon.CurrentPage = ReimburseItemsPaging.ShowRows;
            //BindGrid(objHrCommon);


        }



        #endregion Approved
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                string id =  Convert.ToInt32(Session["UserId"]).ToString();
            }
            catch
            {
                Response.Redirect("Home.aspx");
            }
            btnsearch.Attributes.Add("onclick", "javascript:return ValidateSave();");
            SearchCompanyID = Convert.ToInt32(Session["CompanyID"]);
            //topmenu.MenuId = GetParentMenuId();
            //topmenu.ModuleId = ModuleID; ;
            //topmenu.RoleID = Convert.ToInt32(Session["RoleId"].ToString());
            //topmenu.SelectedMenu = Convert.ToInt32(mid.ToString());
            //topmenu.DataBind();

            Session["menuname"] = menuname;
            Session["menuid"] = menuid;
            objAtt = new AttendanceDAC();
            if (!IsPostBack)
            {
                GetWorkSites(0);
                BindDesignations();
                BindYears();
                //BindPager();
                EmpReimbursementAprovedPaging.Visible = false;
            }
        }
        public void BindYears()
        {
            ds = AttendanceDAC.GetCalenderYear();

            int i = 0;
            int Maxyear = Convert.ToInt32(ds.Tables[0].Rows[0]["MaxYear"].ToString());
            for (int Minyear = Convert.ToInt32(ds.Tables[0].Rows[0]["MinYear"].ToString()); Minyear <= Maxyear; Minyear++)
            {
                ddlYear.Items.Insert(i, new ListItem(Convert.ToString(Minyear), Convert.ToString(Minyear)));
                i = i + 1;
            }
            // ddlMonth.SelectedValue = ds.Tables[0].Rows[0]["PreviousMonth"].ToString();
            ddlYear.SelectedValue = ds.Tables[0].Rows[0]["CurrentYear"].ToString();

        }
        public int GetParentMenuId()
        {
            string URL = Request.Url.Segments[Request.Url.Segments.Length - 1] + Request.Url.Query;
            int RoleId = Convert.ToInt32(Session["RoleId"].ToString());
            int ModuleId = ModuleID; ;


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
            // FIllObject.FillDropDown(ref ddlWorksite, "G_GET_WorkSitebyFilter", param);
            FIllObject.FillDropDown(ref ddlWS, "G_GET_WorkSitebyFilter", param);
            ListItem itmSelected = ddlWS.Items.FindByText(txtSearchWorksite.Text);
            if (itmSelected != null)
            {
                ddlWS.SelectedItem.Selected = false;
                itmSelected.Selected = true;
            }
        }

        protected void GetDep(object sender, EventArgs e)
        {

            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@Search", txtsearchDept.Text);
            FIllObject.FillDropDown(ref ddlDept, "HR_GetSearchgoogleDesignations", param);
            ListItem itmSelected = ddlDept.Items.FindByText(txtsearchDept.Text);
            if (itmSelected != null)
            {
                ddlDept.SelectedItem.Selected = false;
                itmSelected.Selected = true;
            }
        }
        protected void btnsearch_Click(object sender, EventArgs e)
        {
            try
            {
                objHrCommon.CurrentPage = EmpReimbursementAprovedPaging.CurrentPage;
                objHrCommon.PageSize = EmpReimbursementAprovedPaging.ShowRows;

                objHrCommon.SiteID = Convert.ToInt32(ddlWS.SelectedItem.Value);
                objHrCommon.DeptID = Convert.ToInt32(ddlDept.SelectedItem.Value);
                objHrCommon.Month = Convert.ToInt32(ddlMonth.SelectedValue);
                objHrCommon.Year = Convert.ToInt32(ddlYear.SelectedItem.Value);
                //put a alert for mnth dropdown
                if (ddlMonth.SelectedIndex <= 0) { AlertMsg.MsgBox(Page, "Please Select Month"); }

               // else if (ddlWS.SelectedIndex <= 0) { AlertMsg.MsgBox(Page, "Please Select Work site"); }
                else
                {
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
                }
                //EmpReimbursementAprovedPaging.Visible = false;
                EmpReimbursementAprovedPaging.Bind(objHrCommon.CurrentPage, objHrCommon.TotalPages, objHrCommon.NoofRecords, objHrCommon.PageSize);
            }
            catch
            {

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

                    int ERID = 0;
                    //AttendanceDAC.HR_EmpPenlitySetStatusTransfered(ERID);
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
                    string Remarks = "Absent Penality";
                    dsTransferDetail.AcceptChanges();
                    //Rijwan:18-03-2016
                    if (Convert.ToString(lblLimited) != "Terminated" && TotAmt > 0)
                    {
                        DataSet ds = AttendanceDAC.HMS_AbsentPenlityTranserAccXML(dsTransferDetail, Remarks, TotAmt,  Convert.ToInt32(Session["UserId"]), Convert.ToInt32(ddlMonth.SelectedValue), Convert.ToInt32(ddlYear.SelectedValue));
                        if (ds.Tables.Count > 0)
                            b = 1;
                    }
                    else
                    {
                        // AttendanceDAC.UpdateEmployeeStatus(EmpID, 'N', Convert.ToInt32(Session["UserId"]));
                    }
                }

            }
            if (b == 1)
                AlertMsg.MsgBox(Page, "Approved Successfullhy");
            Response.Redirect("AbsentPenalities.aspx");
        }
        private void BindDesignations()
        {
            //ds = new DataSet();
            ds = objAtt.GetDesignations();
            ddlDept.DataSource = ds.Tables[0];
            ddlDept.DataTextField = "Designation";
            ddlDept.DataValueField = "DesigId";
            ddlDept.DataBind();
            ddlDept.Items.Insert(0, new ListItem("--Select--", "0"));

        }

        private void GetWorkSites(int SiteID)
        {
            //ds = new DataSet();
            ds = AttendanceDAC.GetWorkSite(SiteID, '1', Convert.ToInt32(Session["CompanyID"]));
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
                objHrCommon.DeptID = Convert.ToInt32(ddlDept.SelectedItem.Value);
                objHrCommon.Month = Convert.ToInt32(ddlMonth.SelectedValue);
                objHrCommon.Year = Convert.ToInt32(ddlYear.SelectedItem.Value);
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
                
                ds = AttendanceDAC.HR_AbsentPenalitiesByPaging(objHrCommon);

                if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0)
                {
                    gvViewApproved.DataSource = ds;
                    gvViewApproved.DataBind();
                    //  EmpReimbursementAprovedPaging.Visible = true;

                }
                else
                {
                    gvViewApproved.EmptyDataText = "No Records Found";
                    EmpReimbursementAprovedPaging.Visible = false;
                    gvViewApproved.DataBind();
                }

                EmpReimbursementAprovedPaging.Bind(objHrCommon.CurrentPage, objHrCommon.TotalPages, objHrCommon.NoofRecords, objHrCommon.PageSize);

            }
            catch (Exception e)
            {
                throw e;
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
            }
            catch (Exception Ex)
            {
                //report error
            }
        }

        protected void btnSync_Click(object sender, EventArgs e)
        {
            try
            {
                objHrCommon.PageSize = EmpReimbursementAprovedPaging.ShowRows;
                objHrCommon.CurrentPage = EmpReimbursementAprovedPaging.CurrentPage;
                if (Convert.ToInt32(ddlWS.SelectedIndex) <= 0)
                {
                    AlertMsg.MsgBox(Page, "Select worksite");
                }
                else if (Convert.ToInt32(ddlMonth.SelectedValue) > 0 && Convert.ToInt32(ddlYear.SelectedValue)>0)
                    EmployeeApproved(objHrCommon);
                else
                {
                    AlertMsg.MsgBox(Page, "Select Month/Year");
                }
                EmpReimbursementAprovedPaging.Bind(objHrCommon.CurrentPage, objHrCommon.TotalPages, objHrCommon.NoofRecords, objHrCommon.PageSize);


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}