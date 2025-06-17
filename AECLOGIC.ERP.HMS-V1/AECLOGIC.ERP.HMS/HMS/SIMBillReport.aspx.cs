using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Configuration;
using AECLOGIC.HMS.BLL;
using AECLOGIC.ERP.COMMON;

namespace AECLOGIC.ERP.HMS
{
    public partial class PhoneBillReport : AECLOGIC.ERP.COMMON.WebFormMaster
    {
        AttendanceDAC objEmployee = new AttendanceDAC();
        AttendanceDAC objRights = new AttendanceDAC();
        HRCommon objHrCommon = new HRCommon();
         
        int mid = 0;
        bool viewall;
        string menuname;
        static int WSID = 0;
        static int SiteID = 0;
        static int CompanyID;
        string menuid;
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
        void EmpListPaging_ShowRowsClick(object sender, EventArgs e)
        {
            EmpListPaging.CurrentPage = 1;
            BindPager();
        }
        void EmpListPaging_FirstClick(object sender, EventArgs e)
        {
            BindPager();
        }
        void BindPager()
        {

            objHrCommon.PageSize = EmpListPaging.CurrentPage;
            objHrCommon.CurrentPage = EmpListPaging.ShowRows;
            EmployeBind(objHrCommon);
        }


        protected void Page_Load(object sender, EventArgs e)
        {

           
            CompanyID = Convert.ToInt32(Session["CompanyID"]);
          
            if (!IsPostBack)
            {

              
                BindYears();
                EmployeBind(objHrCommon);
                tblPay.Visible = false;
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
                ddlYearPay.Items.Insert(i, new ListItem(Convert.ToString(Minyear), Convert.ToString(Minyear)));
                i = i + 1;
            }
            ddlMonth.SelectedValue = ds.Tables[0].Rows[0]["PreviousMonth"].ToString();
            ddlYear.SelectedValue = ds.Tables[0].Rows[0]["CurrentYear"].ToString();

            ddlMonthPay.SelectedValue = ds.Tables[0].Rows[0]["PreviousMonth"].ToString();
            ddlYearPay.SelectedValue = ds.Tables[0].Rows[0]["CurrentYear"].ToString();

        }
        void EmployeBind(HRCommon objHrCommon)
        {

            try
            {
                objHrCommon.PageSize = EmpListPaging.ShowRows;
                objHrCommon.CurrentPage = EmpListPaging.CurrentPage;
              
                objHrCommon.SiteID = Convert.ToInt32(Convert.ToInt32(ddlworksites_hid.Value == "" ? "0" : ddlworksites_hid.Value));
              
                objHrCommon.DeptID = Convert.ToDouble(Convert.ToInt32(ddldepartments_hid.Value == "" ? "0" : ddldepartments_hid.Value));
                objHrCommon.FName = txtusername.Text;
                objHrCommon.Month = Convert.ToInt32(ddlMonth.SelectedValue);
                objHrCommon.Year = Convert.ToInt32(ddlYear.SelectedValue);
                 
                gveditkbipl.DataSource = null;
                gveditkbipl.DataBind();
                DataSet ds = AttendanceDAC.HR_GetMobilesBills(objHrCommon, Convert.ToInt32(Session["CompanyID"]));
                
                if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0)
                {
                    gveditkbipl.DataSource = ds;
                    gveditkbipl.DataBind();
                }
                EmpListPaging.Bind(objHrCommon.CurrentPage, objHrCommon.TotalPages, objHrCommon.NoofRecords, objHrCommon.PageSize);

            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public string GetWorkSite(string WSid)
        {
            string retVal = "";
            try
            {
                DataSet ds = (DataSet)ViewState["WorkSites"];
                retVal = ds.Tables[0].Select("Site_ID='" + WSid + "'")[0]["Site_Name"].ToString();
            }
            catch { }
            return retVal;
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
        public string GetDepartment(string DeptId)
        {
            string retVal = "";
            try
            {
                DataSet ds = (DataSet)ViewState["Departments"];
                retVal = ds.Tables[0].Select("DepartmentUId='" + DeptId + "'")[0]["DepartmentName"].ToString();
            }
            catch { }
            return retVal;
        }
        
        protected void EmpdataBound()
        {
            SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings["strConn"]);
            cn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cn;
            cmd.CommandType = CommandType.Text;
            //cmd.CommandText = "select * from kbipemp where Status='y' and [Type]!=1";
            cmd.CommandText = "select * from T_G_EmployeeMaster where Status='y' order by fname";
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = null;     
            da.Fill(ds, "temp");
            gveditkbipl.DataSource = ds;
            gveditkbipl.DataBind();
            cn.Close();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
           
            objHrCommon.SiteID = Convert.ToInt32(Convert.ToInt32(ddlworksites_hid.Value == "" ? "0" : ddlworksites_hid.Value));
           
            objHrCommon.DeptID = Convert.ToDouble(Convert.ToInt32(ddldepartments_hid.Value == "" ? "0" : ddldepartments_hid.Value));
            objHrCommon.FName = txtusername.Text;
            EmployeBind(objHrCommon);
        }

        protected void btnUpdateAll_Click(object sender, EventArgs e)
        {
            try
            {
                int Month = Convert.ToInt32(ddlMonth.SelectedValue);
                int Year = Convert.ToInt32(ddlYear.SelectedValue);
                foreach (GridViewRow gvr in gveditkbipl.Rows)
                {
                    CheckBox chk = (CheckBox)gvr.Cells[0].FindControl("chkSelect");
                    if (chk.Checked == true)
                    {
                        Label lblEmpId = (Label)gvr.Cells[1].FindControl("lblEmpId");
                        TextBox txtBillAmount = (TextBox)gvr.Cells[9].FindControl("txtBillAmount");
                        Label lblAmountLimit = (Label)gvr.Cells[8].FindControl("lblAmountLimit");
                        Label lblCSID = (Label)gvr.Cells[11].FindControl("lblCSID");
                        AttendanceDAC.InsUpdateSimBills(Convert.ToInt32(lblCSID.Text), Convert.ToInt32(lblEmpId.Text), Month, Year, Convert.ToDecimal(txtBillAmount.Text),  Convert.ToInt32(Session["UserId"]));
                        AlertMsg.MsgBox(Page, "Succeesfully Updated");
                    }
                }
                AlertMsg.MsgBox(Page, "Succeesfully Updated");

            }
            catch (Exception ex)
            {

                AlertMsg.MsgBox(Page, "Sorry for the inconvinience. Try again.\nError:" + ex.Message.ToString(),AlertMsg.MessageType.Error);
            }
        }

        protected void chkPay_CheckedChanged(object sender, CommandEventArgs e)
        {
            foreach (GridViewRow gvr in gveditkbipl.Rows)
            {
                gvr.BackColor = System.Drawing.Color.White;
                gvr.ForeColor = System.Drawing.Color.Black;
                gvr.Font.Bold = false;
                CheckBox chkPay = (CheckBox)gvr.Cells[0].FindControl("chkPay");
                if (chkPay.Checked == true)
                {
                    tblPay.Visible = true;
                    gvr.BackColor = System.Drawing.Color.Orange;
                    gvr.ForeColor = System.Drawing.Color.Black;
                    gvr.Font.Bold = true;
                    // Label lblEmpID = (Label)gvr.Cells[4].FindControl("lblEmpId");
                    Label lblEmpName = (Label)gvr.Cells[5].FindControl("lblEmpName");
                    Label lblOverUsed = (Label)gvr.Cells[13].FindControl("lblOverUsed");
                    Label lblSiMno = (Label)gvr.Cells[8].FindControl("lblMobile2");

                    lblEmpIDPay.Text = gvr.Cells[4].Text;
                    lblNamePay.Text = lblEmpName.Text;
                    lblAmountPay.Text = lblOverUsed.Text;
                    lblSimPay.Text = lblSiMno.Text;
                     
                    chkPay.Checked = false;

                }
            }
        }
       
        protected void gveditkbipl_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            LinkButton lnk = (LinkButton)e.CommandSource;
            GridViewRow gvr = (GridViewRow)lnk.Parent.Parent;
            Label lblOverUsed = (Label)gvr.Cells[13].FindControl("lblOverUsed");
            foreach (GridViewRow gvrow in gveditkbipl.Rows)
            {
                gvrow.BackColor = System.Drawing.Color.White;
                gvrow.ForeColor = System.Drawing.Color.Black;
                gvrow.Font.Bold = false;
            }
            if (lblOverUsed.Text != "")
            {
                tblPay.Visible = true;
                gvr.BackColor = System.Drawing.Color.Orange;
                gvr.ForeColor = System.Drawing.Color.Black;
                gvr.Font.Bold = true;
                Label lblEmpName = (Label)gvr.Cells[5].FindControl("lblEmpName");

                Label lblSiMno = (Label)gvr.Cells[8].FindControl("lblMobile2");

                lblEmpIDPay.Text = gvr.Cells[4].Text;
                lblNamePay.Text = lblEmpName.Text;
                lblAmountPay.Text = lblOverUsed.Text;
                lblSimPay.Text = lblSiMno.Text;
            }
            else
            {
                tblPay.Visible = false;
            }
        }

        protected void BtnExportGrid_Click(object sender, EventArgs args)
        {
            objHrCommon.PageSize = EmpListPaging.ShowRows;
            objHrCommon.CurrentPage = EmpListPaging.CurrentPage;
            objHrCommon.SiteID = Convert.ToInt32(Convert.ToInt32(ddlworksites_hid.Value == "" ? "0" : ddlworksites_hid.Value));
            objHrCommon.DeptID = Convert.ToDouble(Convert.ToInt32(ddldepartments_hid.Value == "" ? "0" : ddldepartments_hid.Value));
            objHrCommon.FName = txtusername.Text;
            objHrCommon.Month = Convert.ToInt32(ddlMonth.SelectedValue);
            objHrCommon.Year = Convert.ToInt32(ddlYear.SelectedValue);


            SqlDataReader dr = objEmployee.drHR_GetMobilesBills(objHrCommon);
            ExportFileUtil.ExportToExcel(dr, "", "#EFEFEF", "#E6e6e6", "Monthly SIM Bills");
           
        }

     

        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionList(string prefixText, int count, string contextKey)
        {
           
            DataSet ds = AttendanceDAC.HR_GetWorkSite_By_MobileBills_googlesearch(prefixText.Trim(), WSID);
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
            CompanyID = Convert.ToInt32(Session["CompanyID"]);
            WSID = Convert.ToInt32(ddlworksites_hid.Value == "" ? "0" : ddlworksites_hid.Value); ;
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
        protected void GetDept(object sender, EventArgs e)
        {
            SiteID = 0;
            SiteID = Convert.ToInt32(ddlworksites_hid.Value == "" ? "0" : ddlworksites_hid.Value); ;
            CompanyID = Convert.ToInt32(Session["CompanyID"]);
        }
    }
}
