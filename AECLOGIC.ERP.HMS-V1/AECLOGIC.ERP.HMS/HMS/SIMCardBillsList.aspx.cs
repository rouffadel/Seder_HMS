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
    public partial class SIMCardBillsList : AECLOGIC.ERP.COMMON.WebFormMaster
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
            if (hdnSearchChange.Value == "1")
                EmpListPaging.CurrentPage = 1;
            BindPager();
            hdnSearchChange.Value = "0";
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
                GetParentMenuId();
             
                BindYears();
                BindPager();
            }

        }
        

        
         
        public int GetParentMenuId()
        {
            string URL = Request.Url.Segments[Request.Url.Segments.Length - 1] + Request.Url.Query;
            int RoleId = Convert.ToInt32(Session["RoleId"].ToString());
            int ModuleId = ModuleID;;

             

           DataSet ds = AttendanceDAC.GetAllowed(RoleId, ModuleId, URL);
            int MenuId = 0;
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                MenuId = Convert.ToInt32(ds.Tables[0].Rows[0]["Under"]);
                
                btnUpdateAll.Visible = (bool)ds.Tables[0].Rows[0]["Editable"];
                
            }
            return MenuId;
        }
        public void BindYears()
        {
            DataSet ds = AttendanceDAC.GetCalenderYear();

            int i = 0;
            int Maxyear = Convert.ToInt32(ds.Tables[0].Rows[0]["MaxYear"].ToString());
            for (int Minyear = Convert.ToInt32(ds.Tables[0].Rows[0]["MinYear"].ToString()); Minyear <= Maxyear; Minyear++)
            {
                ddlYear.Items.Insert(i, new ListItem(Convert.ToString(Minyear), Convert.ToString(Minyear)));
                i = i + 1;
            }
            ddlMonth.SelectedValue = ds.Tables[0].Rows[0]["PreviousMonth"].ToString();
            ddlYear.SelectedValue = ds.Tables[0].Rows[0]["CurrentYear"].ToString();

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
                objHrCommon.Year = Convert.ToInt32(ddlYear.SelectedItem.Value);
                objHrCommon.Month = Convert.ToInt32(ddlMonth.SelectedItem.Value);
                 
                gveditkbipl.DataSource = null;
                gveditkbipl.DataBind();
                DataSet ds = AttendanceDAC.HR_GetMobilesBills(objHrCommon, Convert.ToInt32(Session["CompanyID"]));
                if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0)
                {
                    gveditkbipl.DataSource = ds;
                    EmpListPaging.Bind(objHrCommon.CurrentPage, objHrCommon.TotalPages, objHrCommon.NoofRecords, objHrCommon.PageSize);
                }
                else
                {
                    gveditkbipl.EmptyDataText = "No Records Found";
                    EmpListPaging.Visible = false;
                    btnUpdateAll.Visible = false;
                }
                gveditkbipl.DataBind();

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
            cmd.CommandText = "select * from T_G_EmployeeMaster where Status='y' order by fname";
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = null;
            da.Fill(ds, "temp");
            gveditkbipl.DataSource = ds;
            gveditkbipl.DataBind();
            cn.Close();

        }
        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int Month = Convert.ToInt32(ddlMonth.SelectedValue);
            int Year = Convert.ToInt32(ddlYear.SelectedValue);
            if (e.CommandName == "Edt")
            {
                int ID = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).Parent.Parent;
                CheckBox chk = (CheckBox)gveditkbipl.Rows[row.RowIndex].FindControl("chkSelect");
                if (chk.Checked == true)
                {
                    try
                    {

                        TextBox txtBillAmount = (TextBox)gveditkbipl.Rows[row.RowIndex].FindControl("txtBillAmount");
                        Label lblAmountLimit = (Label)gveditkbipl.Rows[row.RowIndex].FindControl("lblAmountLimit");

                        double BillAmount = Convert.ToDouble(txtBillAmount.Text);
                        double AmtLmt = Convert.ToDouble(lblAmountLimit.Text);
                        double? ExtAmt = null;

                        if (BillAmount - AmtLmt > 0)
                            ExtAmt = BillAmount - AmtLmt;

                        Label lblCSID = (Label)gveditkbipl.Rows[row.RowIndex].FindControl("lblCSID");
                        AttendanceDAC.HR_InsUpMobileBills(ID, Month, Year, Convert.ToDouble(txtBillAmount.Text),  Convert.ToInt32(Session["UserId"]), ExtAmt);//HR_InsUpd_SIMBills
                        EmployeBind(objHrCommon);
                        AlertMsg.MsgBox(Page, "Done!");
                    }
                    catch (Exception ex)
                    {
                        AlertMsg.MsgBox(Page, "Sorry for the inconvinience. Try again.\nError:" + ex.Message.ToString(),AlertMsg.MessageType.Error);
                    }

                }
                else
                {
                    AlertMsg.MsgBox(Page, "Check the record you want to update!");
                }

            }


        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            objHrCommon.SiteID = Convert.ToInt32(Convert.ToInt32(ddlworksites_hid.Value == "" ? "0" : ddlworksites_hid.Value));
            objHrCommon.DeptID = Convert.ToDouble(Convert.ToInt32(ddldepartments_hid.Value == "" ? "0" : ddldepartments_hid.Value));
            objHrCommon.FName = txtusername.Text;
            EmployeBind(objHrCommon);
        }
       
        protected void gveditkbipl_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            foreach (GridViewRow gvr in gveditkbipl.Rows)
            {
                LinkButton lnkUpd = (LinkButton)gvr.Cells[10].FindControl("lnkEdit");
                lnkUpd.Enabled = Convert.ToBoolean(ViewState["Editable"]);
            }
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
                    TextBox txtBillAmount = (TextBox)gvr.Cells[9].FindControl("txtBillAmount");
                    if (txtBillAmount.Text != "")
                    {
                        if (chk.Checked == true)
                        {
                            double BillAmount = double.Parse(txtBillAmount.Text);
                            Label lblEmpId = (Label)gvr.Cells[1].FindControl("lblEmpId");
                            Label lblAmountLimit = (Label)gvr.Cells[8].FindControl("lblAmountLimit");
                            double? ExtAmt = BillAmount - Convert.ToDouble(lblAmountLimit.Text);
                            if (ExtAmt > 0)
                                ExtAmt = BillAmount - Convert.ToDouble(lblAmountLimit.Text);
                            else
                                ExtAmt = null;
                            Label lblCSID = (Label)gvr.Cells[11].FindControl("lblCSID");
                            int SID = int.Parse(lblCSID.Text);
                            int UserID =  Convert.ToInt32(Session["UserId"]);
                            AttendanceDAC.HR_InsUpMobileBills(SID, Month, Year, BillAmount, UserID, ExtAmt);
                        }
                    }
                }


            }
            catch (Exception ex)
            {

                AlertMsg.MsgBox(Page, "Sorry for the inconvinience. Try again.\nError:" + ex.Message.ToString(),AlertMsg.MessageType.Error);
            }
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
            EmpListPaging.CurrentPage = 1;
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

            return items.ToArray(); 

        }
        protected void GetDept(object sender, EventArgs e)
        {
            SiteID = 0;
            SiteID = Convert.ToInt32(ddlworksites_hid.Value == "" ? "0" : ddlworksites_hid.Value); ;
            CompanyID = Convert.ToInt32(Session["CompanyID"]);
            EmpListPaging.CurrentPage = 1;
        }
    }
}
