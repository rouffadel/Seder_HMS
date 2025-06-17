using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using AECLOGIC.HMS.BLL;
using AECLOGIC.ERP.COMMON;
using System.Data.SqlClient;
using Aeclogic.Common.DAL;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Web.UI.HtmlControls;
namespace AECLOGIC.ERP.HMS
{
    public partial class HRProvisions : AECLOGIC.ERP.COMMON.WebFormMaster
    {
        int mid = 0;
        bool viewall, Editable;
        string menuname;
        string menuid;
        static int WS;
        static int Siteid;
        static int EDeptid = 0;
        static int SearchCompanyID;
        static int WSiteid;
        string ModuleId = System.Configuration.ConfigurationManager.AppSettings["ModuleId"];
        int CompanyID = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["CompanyID"]);
        HRCommon objHrCommon = new HRCommon();
        AttendanceDAC objAtt = new AttendanceDAC();
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
        }
        void EmpListPaging_FirstClick(object sender, EventArgs e)
        {
            BindPager();
        }
        void EmpListPaging_ShowRowsClick(object sender, EventArgs e)
        {
            EmpListPaging.CurrentPage = 1;
            BindPager();
        }
        void BindPager()
        {
            objHrCommon.PageSize = EmpListPaging.CurrentPage;
            objHrCommon.CurrentPage = EmpListPaging.ShowRows;
            EmployeBind(objHrCommon);
        }
        void EmployeBind(HRCommon objHrCommon)
        {
            try
            {
                objHrCommon.PageSize = EmpListPaging.ShowRows;
                objHrCommon.CurrentPage = EmpListPaging.CurrentPage;
                int? EmpID = null;
                int? PrjID = null;
                int? Dept = null;
                int? WS = null;
                int? Year = DateTime.Now.Year;
                int? Month = DateTime.Now.Month;
                if (Txtwrk.Text.Trim() != "")
                {
                    WS = Convert.ToInt32(Txtwrk_hid.Value == "" ? "0" : Txtwrk_hid.Value);
                }
                if (TxtDept.Text.Trim() != "")
                {
                    Dept = Convert.ToInt32(TxtDept_hid.Value == "" ? "0" : TxtDept_hid.Value);
                }
                if (txtSearchemp.Text.Trim() != "")
                {
                    EmpID = Convert.ToInt32(txtSearchemp_hid.Value == "" ? "0" : txtSearchemp_hid.Value);
                }
                if (ddlyear.SelectedValue != "")
                {
                    Year = Convert.ToInt32(ddlyear.SelectedItem.Text);
                }
                if (Convert.ToInt32(ddlmonth.SelectedValue) > 0)
                {
                    Month = Convert.ToInt32(ddlmonth.SelectedValue);
                }
                if (Request.QueryString.Count > 0)
                {
                    int appstatus = 0;
                    if (Request.QueryString.AllKeys.Contains("Apr"))
                    {
                        appstatus = Convert.ToInt32(Request.QueryString["Apr"].ToString());
                    }
                    SqlParameter[] sqlParams = new SqlParameter[12];
                    sqlParams[0] = new SqlParameter("@CurrentPage", objHrCommon.CurrentPage);
                    sqlParams[1] = new SqlParameter("@PageSize", objHrCommon.PageSize);
                    sqlParams[2] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
                    sqlParams[2].Direction = ParameterDirection.ReturnValue;
                    sqlParams[3] = new SqlParameter("@NoofRecords", System.Data.SqlDbType.Int);
                    sqlParams[3].Direction = ParameterDirection.Output;
                    sqlParams[4] = new SqlParameter("@wsid", WS);
                    sqlParams[5] = new SqlParameter("@Projectid", PrjID);
                    sqlParams[6] = new SqlParameter("@deptno", Dept);
                    sqlParams[7] = new SqlParameter("@empid", EmpID);
                    sqlParams[8] = new SqlParameter("@year", Year);
                    sqlParams[9] = new SqlParameter("@month", Month);
                    sqlParams[10] = new SqlParameter("@bitstatus", Request.QueryString["key"].ToString());
                    sqlParams[11] = new SqlParameter("@appstatus", appstatus);
                    DataSet ds = SQLDBUtil.ExecuteDataset("sh_HRProvisionDetails", sqlParams);
                    objHrCommon.NoofRecords = (int)sqlParams[3].Value;
                    objHrCommon.TotalPages = (int)sqlParams[2].Value;
                    if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        gvProvisions.DataSource = ds;
                        gvProvisions.DataBind();
                    }
                    else
                    {
                        gvProvisions.DataSource = null;
                        gvProvisions.DataBind();
                    }
                }
                else
                {
                    DataSet ds = AttendanceDAC.HR_GetProvisions(objHrCommon, WS, PrjID, Dept, EmpID, Year, Month);
                    if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        gvProvisions.DataSource = ds;
                        gvProvisions.DataBind();
                    }
                    else
                    {
                        gvProvisions.DataSource = null;
                        gvProvisions.DataBind();
                    }
                }
                EmpListPaging.Bind(objHrCommon.CurrentPage, objHrCommon.TotalPages, objHrCommon.NoofRecords, objHrCommon.PageSize);
            }
            catch (Exception e)
            {
               // throw e;
            }
        }
        public string PONavigateUrl(string empid)
        {
            bool Fals = false;
            return "javascript:return window.open('EmpSalhikesV2.aspx?Empid=" + empid + "' , '_blank')";
        }
        public string PONavigateUrl1(string empid)
        {
            bool Fals = false;
            return "javascript:return window.open('LeavesAvailableDetails.aspx?EMPID=" + empid + "&LID=0' , '_blank')";
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetParentMenuId();
                BindPager();
                //FIllObject.FillDropDown(ref ddlyear, "HR_Get_FinancialYearList", "FinYearID", "Name");
                FIllObject.FillDropDown(ref ddlyear, "HMS_YearWise", "ID", "Name");
            }
            if (Request.QueryString.Count > 0)
            {
                if (Request.QueryString["key"].ToString() == "4")
                {
                    btnapprove.Visible = false;
                }
                else
                    btnapprove.Visible = true;
                if (Request.QueryString["key"].ToString() == "3")
                    btnapprove.Text = "A/C to Post";
                else
                    btnapprove.Text = "Approve All";
            }
            else
            {
                btnapprove.Visible = true;
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
                viewall = (bool)ViewState["ViewAll"];
                menuname = ds.Tables[0].Rows[0]["menuname"].ToString();
                menuid = MenuId.ToString();
                mid = Convert.ToInt32(ds.Tables[0].Rows[0]["MenuId"].ToString());
            }
            return MenuId;
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            EmpListPaging.CurrentPage = 1;
            EmployeBind(objHrCommon);
        }
        //for worksite
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletiondeptWorkList(string prefixText, int count, string contextKey)
        {
            DataSet ds = AttendanceDAC.HR_GetWorkSite(prefixText);
            DataTable dt = ds.Tables[0];
            List<string> items = new List<string>(count);
            var rtval = new Dictionary<string, string>();
            foreach (DataRow row in dt.Rows)
            {
                string str = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(row["Name"].ToString(), row["ID"].ToString());
                items.Add(str);
            }
            return items.ToArray(); 
        }
        //for department
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletiondeptList(string prefixText, int count, string contextKey)
        {
            DataSet ds = AttendanceDAC.GetDept_search(prefixText);
            DataTable dt = ds.Tables[0];
            List<string> items = new List<string>(count);
            var rtval = new Dictionary<string, string>();
            foreach (DataRow row in dt.Rows)
            {
                string str = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(row["Name"].ToString(), row["ID"].ToString());
                items.Add(str);
            }
            return items.ToArray(); 
        }
        //for employee
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionList(string prefixText, int count, string contextKey)
        {
            DataSet ds = AttendanceDAC.Getemp_Search(prefixText);
            DataTable dt = ds.Tables[0];
            List<string> items = new List<string>(count);
            var rtval = new Dictionary<string, string>();
            foreach (DataRow row in dt.Rows)
            {
                string str = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(row["Name"].ToString(), row["ID"].ToString());
                items.Add(str);
            }
            return items.ToArray(); 
        }
        protected void gvProvisions_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                CheckBox chkHeader = (CheckBox)e.Row.FindControl("chkHeader");
                chkHeader.Attributes.Add("onclick", String.Format("javascript:return SelectAll(this,'{0}','chkPrereq');", gvProvisions.ClientID));
                //if (Request.QueryString["key"].ToString() == "4")
                //{
                //    if (e.Row.Cells.Count > 0)
                //    {
                //        e.Row.Cells[14].Visible = true;
                //    }
                //}
                //else
                //    e.Row.Cells[14].Visible = false;
            }
        }
        protected void btnapprove_Click(object sender, EventArgs e)
        {
            int c = 0;
            foreach (GridViewRow gvr in gvProvisions.Rows)
            {
                CheckBox chkPrereq = new CheckBox();
                Label EmpID = new Label();
                Label Month = new Label();
                Label Year = new Label();
                Label ALAccured = new Label();
                Label Amount = new Label();
                Label Sitename = new Label();
                Label lblNoOfTickets = new Label();
                Label lblAirTicketAmount = new Label();
                Label lblID = new Label();
                Label lblGrossSalary = new Label();
                Year = (Label)gvr.Cells[5].FindControl("lblyear");
                Month = (Label)gvr.Cells[6].FindControl("lblmnth");
                EmpID = (Label)gvr.Cells[4].FindControl("lblempid");
                ALAccured = (Label)gvr.Cells[11].FindControl("lblALAccured");
                Amount = (Label)gvr.Cells[8].FindControl("lblAmount");
                lblNoOfTickets = (Label)gvr.Cells[8].FindControl("lblNoOfTickets");
                lblAirTicketAmount = (Label)gvr.Cells[8].FindControl("lblAirTicketAmount");
                chkPrereq = (CheckBox)gvr.Cells[0].FindControl("chkPrereq");
                lblID = (Label)gvr.Cells[9].FindControl("lblID");
                lblGrossSalary = (Label)gvr.Cells[10].FindControl("lblGrossSalary");
                if (chkPrereq.Checked)
                {
                    if (Convert.ToDouble(Amount.Text) > 0 || Convert.ToDouble(lblAirTicketAmount.Text) > 0)
                    {
                        SqlParameter[] parms = new SqlParameter[11];
                        parms[0] = new SqlParameter("@Empid", EmpID.Text);
                        if (Convert.ToInt32(ddlmonth.SelectedValue) > 0)
                            parms[1] = new SqlParameter("@Month", ddlmonth.SelectedValue);
                        else
                            parms[1] = new SqlParameter("@Month", DateTime.Now.Month);
                        if (Convert.ToInt32(ddlyear.SelectedValue) > 0)
                            parms[2] = new SqlParameter("@year", ddlyear.SelectedItem.Text);
                        else
                            parms[2] = new SqlParameter("@year", DateTime.Now.Year);
                        parms[3] = new SqlParameter("@ALAccured", Convert.ToDouble(ALAccured.Text));
                        if (Convert.ToDouble(Amount.Text) > 0)
                            parms[4] = new SqlParameter("@Amount", Convert.ToDouble(Amount.Text));
                        else
                            parms[4] = new SqlParameter("@Amount", DBNull.Value);
                        parms[5] = new SqlParameter("@CompanyId", Convert.ToInt32(Session["CompanyID"]));
                        if (lblNoOfTickets.Text != null && lblNoOfTickets.Text !="")
                        {
                            parms[6] = new SqlParameter("@NoOfTickets", Convert.ToDouble(lblNoOfTickets.Text));
                        }
                        else
                        {
                            parms[6] = new SqlParameter("@NoOfTickets", DBNull.Value);
                        }
                        if (lblAirTicketAmount.Text != "")
                            parms[7] = new SqlParameter("@AirTicketAmount", Convert.ToDouble(lblAirTicketAmount.Text));
                        else
                            parms[7] = new SqlParameter("@AirTicketAmount", DBNull.Value);
                        parms[10] = new SqlParameter("@GrossSalary",Convert.ToDouble( lblGrossSalary.Text));
                        if (Request.QueryString.Count > 0)
                        {
                            if (Request.QueryString["Key"].ToString() == "1")
                            {
                                parms[8] = new SqlParameter("@bitstatus", "2");
                                parms[9] = new SqlParameter("@id", lblID.Text);
                                SqlHelper.ExecuteNonQuery("sh_HRProvision_Insertion", parms);
                            }
                            else if (Request.QueryString["Key"].ToString() == "2")
                            {
                                parms[8] = new SqlParameter("@bitstatus", "3");
                                parms[9] = new SqlParameter("@id", lblID.Text);
                                SqlHelper.ExecuteNonQuery("sh_HRProvision_Insertion", parms);
                            }
                            else if (Request.QueryString["Key"].ToString() == "3")
                            {
                                parms[8] = new SqlParameter("@bitstatus", "4");
                                parms[9] = new SqlParameter("@id", lblID.Text);
                                SqlHelper.ExecuteNonQuery("sh_ProvisionAccountPosting", parms);
                            }
                        }
                        else
                        {
                            parms[8] = new SqlParameter("@bitstatus", "1");
                            parms[9] = new SqlParameter("@id", "0");
                            SqlHelper.ExecuteNonQuery("sh_HRProvision_Insertion", parms);
                        }
                        c = c + 1;
                    }
                }
                else if(c==0)
                    c = 0;
            }
            if (c > 0)
            {
                gvProvisions.DataSource = null;
                gvProvisions.DataBind();
                BindPager();
                if(Request.QueryString.Count>0)
                {
                    if(Request.QueryString["key"].ToString() == "3")
                        AlertMsg.MsgBox(Page, "Account posted Successfully");
                    else
                AlertMsg.MsgBox(Page, "Approved Successfully");
                }
                else
                    AlertMsg.MsgBox(Page, "Inserted Successfully");
            }
            else
            {
                BindPager();
                AlertMsg.MsgBox(Page, "Select atleast one Request", AlertMsg.MessageType.Warning);
            }
        }
        protected void lnkPending_Click(object sender, EventArgs e)
        {
            BindPager();
        }
        protected void lnkApproved_Click(object sender, EventArgs e)
        {
            Response.Redirect("HRProvisionsView.aspx");
        }
    }
}