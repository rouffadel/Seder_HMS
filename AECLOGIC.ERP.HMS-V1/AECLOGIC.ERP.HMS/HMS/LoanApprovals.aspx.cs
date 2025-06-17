using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using AECLOGIC.HMS.BLL;
using System.Data.SqlClient;
using AECLOGIC.ERP.COMMON;
using Aeclogic.Common.DAL;
using System.Collections.Generic;
namespace AECLOGIC.ERP.HMS
{
    public partial class LoanApprovals : AECLOGIC.ERP.COMMON.WebFormMaster
    {
        HRCommon objHrCommon = new HRCommon();
        static int SiteSearch = 0;
        static int Userid;
        static int ModID;
        static int CompanyID = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["CompanyID"].ToString());
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Userid = Convert.ToInt32(Convert.ToInt32(Session["UserId"]).ToString());
                // GetParentMenuId();
                BindYears();
            }
        }
        private void BindApprovedGrid()
        {
            objHrCommon.PageSize = EmpListPaging.ShowRows;
            objHrCommon.CurrentPage = EmpListPaging.CurrentPage;
            int EmpID = 0, Month, Year;
            if (TxtEmp.Text == "" || TxtEmp.Text == null)
            {
                ddlEmp_hid.Value = "";
            }
            if (TxtEmp.Text != "" || TxtEmp.Text != null)
            {
                EmpID = Convert.ToInt32(ddlEmp_hid.Value == "" ? "0" : ddlEmp_hid.Value);
            }
            if (ddlmonth.SelectedIndex == 0)
            {
                //AlertMsg.MsgBox(Page, "Please Select Month", AlertMsg.MessageType.Warning);
                //return;
                Month = 0;
            }
            else
            {
                Month = Convert.ToInt32(ddlmonth.SelectedValue);
            }
            if (ddlYear.SelectedIndex == 0)
            {
                AlertMsg.MsgBox(Page, "Please Select Year", AlertMsg.MessageType.Warning);
                return;
            }
            else
            {
                Year = Convert.ToInt32(ddlYear.SelectedValue);
            }
            SqlParameter[] objParam = new SqlParameter[8];
            objParam[0] = new SqlParameter("@month", Month);
            objParam[1] = new SqlParameter("@year", Year);
            if (EmpID != 0)
                objParam[2] = new SqlParameter("@Empid", EmpID);
            else
                objParam[2] = new SqlParameter("@Empid", DBNull.Value);
            objParam[3] = new SqlParameter("@CurrentPage", objHrCommon.CurrentPage);
            objParam[4] = new SqlParameter("@PageSize", objHrCommon.PageSize);
            objParam[5] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
            objParam[5].Direction = ParameterDirection.ReturnValue;
            objParam[6] = new SqlParameter("@NoofRecords", System.Data.SqlDbType.Int);
            objParam[6].Direction = ParameterDirection.Output;
            objParam[7] = new SqlParameter("@siteid", ddlWorksite_hid.Value);
            DataSet ds = SQLDBUtil.ExecuteDataset("sh_loanrecoverydetails_Approved", objParam);
            if (ds.Tables[0].Rows.Count > 0)
            {
                objHrCommon.NoofRecords = (int)objParam[6].Value;
                objHrCommon.TotalPages = (int)objParam[5].Value;
                gvloanApprvd.DataSource = ds.Tables[0];
                EmpListPaging.Bind(objHrCommon.CurrentPage, objHrCommon.TotalPages, objHrCommon.NoofRecords, objHrCommon.PageSize);
                EmpListPaging.Visible = true;
                gvloanApprvd.DataBind();
            }
            else
            {
                gvloanApprvd.DataSource = null;
                gvloanApprvd.DataBind();
            }
        }
        //public int GetParentMenuId()
        //{
        //    string URL = Request.Url.Segments[Request.Url.Segments.Length - 1] + Request.Url.Query;
        //    int RoleId = Convert.ToInt32(Session["RoleId"].ToString());
        //    int ModuleId = ModuleID; ;
        //    DataSet ds = AttendanceDAC.GetAllowed(RoleId, ModuleId, URL);
        //    int MenuId = 0;
        //    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        //    {
        //        MenuId = Convert.ToInt32(ds.Tables[0].Rows[0]["Under"]);
        //        ViewState["ViewAll"] = (bool)ds.Tables[0].Rows[0]["ViewAll"];
        //        ViewState["Editable"] = Editable = (bool)ds.Tables[0].Rows[0]["Editable"];
        //        viewall = (bool)ViewState["ViewAll"];
        //        menuname = ds.Tables[0].Rows[0]["menuname"].ToString();
        //        menuid = MenuId.ToString();
        //        mid = Convert.ToInt32(ds.Tables[0].Rows[0]["MenuId"].ToString());
        //        btnApprove.Enabled = Convert.ToBoolean(ds.Tables[0].Rows[0]["ViewAll"].ToString());
        //        gvShow.Columns[14].Visible = Convert.ToBoolean(ds.Tables[0].Rows[0]["Editable"].ToString());
        //        gvShow.Columns[15].Visible = Convert.ToBoolean(ds.Tables[0].Rows[0]["Editable"].ToString());
        //        gvShow.Columns[16].Visible = false;
        //        gvShowRej.Columns[17].Visible = Convert.ToBoolean(ds.Tables[0].Rows[0]["Editable"].ToString());
        //        btnApprove.Enabled = btnRApprove.Enabled = btnTransferAcc.Enabled = Convert.ToBoolean(ds.Tables[0].Rows[0]["ViewAll"].ToString());
        //        btnApprove.Visible = btnRApprove.Visible = btnTransferAcc.Visible = Convert.ToBoolean(ds.Tables[0].Rows[0]["ViewAll"].ToString());
        //        btnSave.Enabled = btnSubmit.Enabled = btnTransferAcc.Enabled = BtnEditSave.Enabled = btnApprove.Enabled = btnRApprove.Enabled = btnRejReaSave.Enabled = Editable;
        //        btnSave.Visible = btnSubmit.Visible = btnTransferAcc.Visible = BtnEditSave.Visible = btnApprove.Visible = btnRApprove.Visible = btnRejReaSave.Visible = Editable;
        //    }
        //    return MenuId;
        //}
        protected override void OnInit(EventArgs e)
        {
            ModuleID = 1;
            base.OnInit(e);
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
            EmpListPaging.CurrentPage = 1;
            BindPager();
            BindApprovedGrid();
        }
        void EmpListPaging_FirstClick(object sender, EventArgs e)
        {
            BindPager();
            BindApprovedGrid();
        }
        void BindPager()
        {
            objHrCommon.PageSize = EmpListPaging.ShowRows;
            objHrCommon.CurrentPage = EmpListPaging.CurrentPage;
            BindGrid(objHrCommon);
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            EmpListPaging.CurrentPage = 1;
            if (Request.QueryString.Count > 0)
            {
                int id = Convert.ToInt32(Request.QueryString["key"].ToString());
                if (id == 1)//Addnew
                {
                    BindApprovedGrid();
                }
            }
            else
                BindPager();
        }
        public void BindYears()
        {
            DataSet dss = PayRollMgr.GetFinancialYear();
            ddlYear.DataSource = dss;
            ddlYear.DataValueField = "AssessmentYear";
            ddlYear.DataTextField = "AssessmentYear";
            ddlYear.DataBind();
            ddlYear.Items.Insert(0, new ListItem("--Select--", "0"));
            ddlYear.SelectedIndex = 0;
            //DataSet ds = AttendanceDAC.GetCalenderYear();
            //if (ds.Tables[0].Rows[0]["PreviousMonth"].ToString() == "0")
            //{
            //    ddlmonth.SelectedValue = "12";
            //    int CurrentYear = Convert.ToInt32(ds.Tables[0].Rows[0]["CurrentYear"]);
            //    int PreviousYear = CurrentYear - 1;
            //    ddlYear.Items.FindByValue(PreviousYear.ToString()).Selected = true;
            //}
            ////if we are in same year and same month
            //else
            //{
            //    ddlmonth.SelectedValue = ds.Tables[0].Rows[0]["PreviousMonth"].ToString();
            //    if (ddlYear.Items.FindByValue(ds.Tables[0].Rows[0]["CurrentYear"].ToString()) != null)
            //    {
            //        ddlYear.Items.FindByValue(ds.Tables[0].Rows[0]["CurrentYear"].ToString()).Selected = true;
            //    }
            //    else
            //    {
            //        ddlYear.SelectedIndex = 0;
            //        //ddlYear.Items.Count - 1
            //    }
            //}
            //#endregion set defalult month and year
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
        protected void GetWork(object sender, EventArgs e)
        {
            SiteSearch = Convert.ToInt32(ddlWorksite_hid.Value == "" ? "0" : ddlWorksite_hid.Value);
        }
        public void BindGrid(HRCommon objHrCommon)
        {
            try
            {
                objHrCommon.PageSize = EmpListPaging.ShowRows;
                objHrCommon.CurrentPage = EmpListPaging.CurrentPage;
                int EmpID = 0, Month, Year, siteid = 0;
                //if (TxtEmp.Text == "" || TxtEmp.Text == null)
                //{
                //    ddlEmp_hid.Value = "";
                //    AlertMsg.MsgBox(Page, "Please Select Employee", AlertMsg.MessageType.Warning);
                //    return;
                //}
                //else
                if (TxtEmp.Text != "" || TxtEmp.Text != null)
                {
                    EmpID = Convert.ToInt32(ddlEmp_hid.Value == "" ? "0" : ddlEmp_hid.Value);
                }
                if (ddlWorksite_hid.Value == "")
                {
                    AlertMsg.MsgBox(Page, "Please Select Worksite", AlertMsg.MessageType.Warning);
                    return;
                }
                else
                    siteid =Convert.ToInt32(ddlWorksite_hid.Value);
                if (ddlmonth.SelectedIndex == 0)
                {
                    AlertMsg.MsgBox(Page, "Please Select Month", AlertMsg.MessageType.Warning);
                    return;
                }
                else
                {
                    Month = Convert.ToInt32(ddlmonth.SelectedValue);
                }
                if (ddlYear.SelectedIndex == 0)
                {
                    AlertMsg.MsgBox(Page, "Please Select Year", AlertMsg.MessageType.Warning);
                    return;
                }
                else
                {
                    Year = Convert.ToInt32(ddlYear.SelectedValue);
                }
                SqlParameter[] objParam = new SqlParameter[8];
                objParam[0] = new SqlParameter("@month", Month);
                objParam[1] = new SqlParameter("@year", Year);
                if (EmpID != 0)
                    objParam[2] = new SqlParameter("@Empid", EmpID);
                else
                    objParam[2] = new SqlParameter("@Empid", DBNull.Value);
                objParam[3] = new SqlParameter("@CurrentPage", objHrCommon.CurrentPage);
                objParam[4] = new SqlParameter("@PageSize", objHrCommon.PageSize);
                objParam[5] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
                objParam[5].Direction = ParameterDirection.ReturnValue;
                objParam[6] = new SqlParameter("@NoofRecords", System.Data.SqlDbType.Int);
                objParam[6].Direction = ParameterDirection.Output;
                objParam[7] = new SqlParameter("@siteid", siteid);
                DataSet ds = SQLDBUtil.ExecuteDataset("sh_loanrecoverydetails", objParam);
                //if (ds.Tables.Count == 1)
                //{
                //    AlertMsg.MsgBox(Page, "Loan Already Approved", AlertMsg.MessageType.Info);
                //}
                //else
                if (ds.Tables.Count == 0)
                {
                    AlertMsg.MsgBox(Page, "There is No loan Pending for this Employee", AlertMsg.MessageType.Info);
                }
                else
                {
                    int tblcount = ds.Tables.Count;
                    if (ds.Tables[tblcount - 1].Rows.Count > 0)
                    {
                        objHrCommon.NoofRecords = (int)objParam[6].Value;
                        objHrCommon.TotalPages = (int)objParam[5].Value;
                        gvloans.DataSource = ds.Tables[tblcount - 1];
                        EmpListPaging.Bind(objHrCommon.CurrentPage, objHrCommon.TotalPages, objHrCommon.NoofRecords, objHrCommon.PageSize);
                        EmpListPaging.Visible = true;
                        gvloans.DataBind();
                        btnsave.Visible = true;
                    }
                    else
                    {
                        gvloans.DataSource = null;
                        EmpListPaging.Visible = false;
                        gvloans.DataBind();
                        btnsave.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                AlertMsg.MsgBox(Page, ex.Message, AlertMsg.MessageType.Warning);
            }
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionList_Employee(string prefixText, int count, string contextKey)
        {
            DataSet ds = AttendanceDAC.HR_GoogleSearchEmp(prefixText.Trim());//WSID
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
        protected void txtRecDed_TextChanged(object sender, EventArgs e)
        {
            TextBox txt = sender as TextBox;
            GridViewRow gvRow = txt.NamingContainer as GridViewRow;
            Label lblCumDed = (Label)gvRow.FindControl("lblCumDed");
            Label lblCumBal = (Label)gvRow.FindControl("lblCumBal");
            TextBox txtRecDed = (TextBox)gvRow.FindControl("txtRecDed");
            if (txtRecDed.Text != "")
            {
                Double Reded = Convert.ToDouble(txtRecDed.Text);
                Double CumDed = Convert.ToDouble(lblCumDed.Text);
                if (Reded <= CumDed)
                {
                    lblCumBal.Text = Convert.ToDouble(CumDed - Reded).ToString();
                }
                else
                {
                    AlertMsg.MsgBox(Page, "Enter Amount Lessthan Cumded", AlertMsg.MessageType.Warning);
                    txtRecDed.Focus();
                    return;
                }
            }
            else
            {
                AlertMsg.MsgBox(Page, "Please Enter Amount in RecDed", AlertMsg.MessageType.Warning);
                txtRecDed.Focus();
                return;
            }
        }
        protected void btnsave_Click(object sender, EventArgs e)
        {
            try
            {
                int i = 0;
                foreach (GridViewRow gvRow in gvloans.Rows)
                {
                    CheckBox chk = new CheckBox();
                    chk = (CheckBox)gvRow.FindControl("chkEToTransfer");
                    if (chk.Checked)
                    {
                        Label lblmonthid = (Label)gvRow.FindControl("lblmonthid");
                        Label lblEmpid = (Label)gvRow.FindControl("lblEmpid");
                        Label lblEmpname = (Label)gvRow.FindControl("lblEmpname");
                        Label lblloanid = (Label)gvRow.FindControl("lblloanid");
                        Label lblcurrinst = (Label)gvRow.FindControl("lblcurrinst");
                        Label lblPrevPending = (Label)gvRow.FindControl("lblPrevPending");
                        Label lblCumDed = (Label)gvRow.FindControl("lblCumDed");
                        Label lblAppxSal = (Label)gvRow.FindControl("lblAppxSal");
                        TextBox txtRecDed = (TextBox)gvRow.FindControl("txtRecDed");
                        Label lblCumBal = (Label)gvRow.FindControl("lblCumBal");
                        int id = 0;
                        SqlParameter[] parm = new SqlParameter[14];
                        parm[0] = new SqlParameter("@MonthID", lblmonthid.Text);
                        parm[1] = new SqlParameter("@Month", Convert.ToInt32(ddlmonth.SelectedValue));
                        parm[2] = new SqlParameter("@Year", Convert.ToInt32(ddlYear.SelectedValue));
                        parm[3] = new SqlParameter("@Empid", Convert.ToInt32(lblEmpid.Text));
                        parm[4] = new SqlParameter("@Name", lblEmpname.Text);
                        parm[5] = new SqlParameter("@loanid", lblloanid.Text);
                        parm[6] = new SqlParameter("@CurrInst", lblcurrinst.Text);
                        parm[7] = new SqlParameter("@PrevPending", Convert.ToDouble(lblPrevPending.Text));
                        parm[8] = new SqlParameter("@CumDed", Convert.ToDouble(lblCumDed.Text));
                        parm[9] = new SqlParameter("@AppxSal", Convert.ToDouble(lblAppxSal.Text));
                        parm[10] = new SqlParameter("@RecDed", Convert.ToDouble(txtRecDed.Text));
                        parm[11] = new SqlParameter("@CumBal", Convert.ToDouble(lblCumDed.Text));
                        parm[12] = new SqlParameter("@CreatedBy", AECLOGIC.ERP.COMMON.clSession.cmnUserId);
                        parm[13] = new SqlParameter("@ID", id);
                        i = SQLDBUtil.ExecuteNonQuery("sh_loanrecoveryApproval", parm);
                    }
                }
                if (i != 0)
                {
                    BindPager();
                    AlertMsg.MsgBox(Page, "Saved ", AlertMsg.MessageType.Success);
                }
            }
            catch (Exception ex)
            {
                AlertMsg.MsgBox(Page, ex.Message, AlertMsg.MessageType.Warning);
            }
        }
        protected void gvloans_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                CheckBox chkMail = (CheckBox)e.Row.FindControl("chkESelectAll");
                chkMail.Attributes.Add("onclick", String.Format("javascript:SelectAll(this,'{0}','chkApproval');", gvloans.ClientID));
            }
        }
        protected void gvloans_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "App")
            {
                GridViewRow gvRow = (GridViewRow)(((Control)e.CommandSource).NamingContainer);
                Label lblmonthid = (Label)gvRow.FindControl("lblmonthid");
                Label lblEmpid = (Label)gvRow.FindControl("lblEmpid");
                Label lblEmpname = (Label)gvRow.FindControl("lblEmpname");
                Label lblloanid = (Label)gvRow.FindControl("lblloanid");
                Label lblcurrinst = (Label)gvRow.FindControl("lblcurrinst");
                Label lblPrevPending = (Label)gvRow.FindControl("lblPrevPending");
                Label lblCumDed = (Label)gvRow.FindControl("lblCumDed");
                Label lblAppxSal = (Label)gvRow.FindControl("lblAppxSal");
                TextBox txtRecDed = (TextBox)gvRow.FindControl("txtRecDed");
                Label lblCumBal = (Label)gvRow.FindControl("lblCumBal");
                int id = 0;
                SqlParameter[] parm = new SqlParameter[14];
                parm[0] = new SqlParameter("@MonthID", lblmonthid.Text);
                parm[1] = new SqlParameter("@Month", Convert.ToInt32(ddlmonth.SelectedValue));
                parm[2] = new SqlParameter("@Year", Convert.ToInt32(ddlYear.SelectedValue));
                parm[3] = new SqlParameter("@Empid", Convert.ToInt32(lblEmpid.Text));
                parm[4] = new SqlParameter("@Name", lblEmpname.Text);
                parm[5] = new SqlParameter("@loanid", lblloanid.Text);
                parm[6] = new SqlParameter("@CurrInst", lblcurrinst.Text);
                parm[7] = new SqlParameter("@PrevPending", Convert.ToDouble(lblPrevPending.Text));
                parm[8] = new SqlParameter("@CumDed", Convert.ToDouble(lblCumDed.Text));
                parm[9] = new SqlParameter("@AppxSal", Convert.ToDouble(lblAppxSal.Text));
                parm[10] = new SqlParameter("@RecDed", Convert.ToDouble(txtRecDed.Text));
                parm[11] = new SqlParameter("@CumBal", Convert.ToDouble(lblCumDed.Text));
                parm[12] = new SqlParameter("@CreatedBy", AECLOGIC.ERP.COMMON.clSession.cmnUserId);
                parm[13] = new SqlParameter("@ID", id);
                int i = SQLDBUtil.ExecuteNonQuery("sh_loanrecoveryApproval", parm);
                BindPager();
                AlertMsg.MsgBox(Page, "Saved ", AlertMsg.MessageType.Success);
            }
        }
    }
}