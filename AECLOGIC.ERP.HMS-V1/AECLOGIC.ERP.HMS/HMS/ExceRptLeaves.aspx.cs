using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AECLOGIC.HMS.BLL;
using System.Data;
using System.Data.SqlClient;

namespace AECLOGIC.ERP.HMS
{
    public partial class ExceRptLeaves : AECLOGIC.ERP.COMMON.WebFormMaster
    {
        int mid = 0;
        bool viewall;
        string menuname;
        string menuid;
        static DateTime? Date;
        static int? Month = null;
        static int? Year = null;
        static int WSID = 0;
        static int Site = 0;
        static int CompanyID;
        
        AttendanceDAC objAtt = new AttendanceDAC();
        protected override void OnInit(EventArgs e)
        {
            ModuleID = 1;
            base.OnInit(e);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
           
            CompanyID = Convert.ToInt32(Session["CompanyID"]);
          
            if (!IsPostBack)
            {
                txtDay.Text = DateTime.Now.ToString("dd/MM/yyyy");
               
                BindYears();
                btnMonthReport_Click(sender, e);
            }
        }

      

        public void BindYears()
        {
               
           DataSet ds = AttendanceDAC.GetCalenderYear();

            int i = 0;
            int Maxyear = Convert.ToInt32(ds.Tables[0].Rows[0]["MaxYear"].ToString());
            for (int Minyear = Convert.ToInt32(ds.Tables[0].Rows[0]["MinYear"].ToString()); Minyear <= Maxyear; Minyear++)
            {
                ddlYear.Items.Insert(0, new ListItem(Convert.ToString(Minyear), Convert.ToString(Minyear)));
                i = i + 1;
            }
            ddlMonth.SelectedValue = ds.Tables[0].Rows[0]["CurrentMonth"].ToString();
            ddlYear.SelectedValue = ds.Tables[0].Rows[0]["CurrentYear"].ToString();

        }

        protected void btnDaySearch_Click(object sender, EventArgs e)
        {
            MainView.ActiveViewIndex = 0;

            if (txtDay.Text.Trim() != "")
            {
                Date = CODEUtility.ConvertToDate(txtDay.Text.Trim(), DateFormat.DayMonthYear);
            }

            BindGrid();

        }
        protected void btnMonthReport_Click(object sender, EventArgs e)
        {
            Date = null;
            if (ddlMonth.SelectedItem.Value != "0")
            {
                Month = Convert.ToInt32(ddlMonth.SelectedItem.Value);
            }
            else
            {
                Month = null;
            }
            if (ddlYear.SelectedItem.Value != "0")
            {
                Year = Convert.ToInt32(ddlYear.SelectedItem.Value);
            }
            BindGrid();
        }

        public void BindGrid()
        {
            MainView.ActiveViewIndex = 0;

               
            int? EmpID = null;

            int? SiteID = null;
            int? DeptID = null;
            int Hours = 0;

            if (txtEmpID.Text.Trim() != "")
            {
                EmpID = Convert.ToInt32(txtEmpID.Text);
            }
           

            if (txtSearchWorksite.Text.Trim() != "")
            {
                SiteID = Convert.ToInt32(ddlWorksite_hid.Value == "" ? "0" : ddlWorksite_hid.Value);

            }

           
            if (txtdepartment.Text.Trim() != "")
            {
                SiteID = Convert.ToInt32(ddlDepartment_hid.Value == "" ? "0" : ddlDepartment_hid.Value);

            }


           DataSet ds = ExceReports.ExceRptLeaves(Date, Month, Year, SiteID, EmpID, DeptID);
            grdAllEmployees.DataSource = ds;
            grdAllEmployees.DataBind();

        }

        public void BindIndDetails(int EmpID)
        {
            btnClsoe.Visible = true;
               

            int? SiteID = null;
            int? DeptID = null;
            int Hours = 0;

          
            if (txtSearchWorksite.Text.Trim() != "")
            {
                SiteID = Convert.ToInt32(ddlWorksite_hid.Value == "" ? "0" : ddlWorksite_hid.Value);

            }
            
            if (txtdepartment.Text.Trim() != "")
            {
                SiteID = Convert.ToInt32(ddlDepartment_hid.Value == "" ? "0" : ddlDepartment_hid.Value);

            }


            DataSet ds = ExceReports.ExceRptLeavesByEmpID(Date, Month, Year, SiteID, EmpID);
            GrdByMonth.DataSource = ds;
            GrdByMonth.DataBind();

        }

        protected void grdAllEmployees_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "detail")
            {
                MainView.ActiveViewIndex = 1;
                BindIndDetails(Convert.ToInt32(e.CommandArgument));
            }
        }
        protected void btnClsoe_Click(object sender, EventArgs e)
        {
            btnClsoe.Visible = false;
            btnMonthReport_Click(sender, e);
        }

       

        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionList(string prefixText, int count, string contextKey)
        {
          
            DataSet ds = AttendanceDAC.HR_GetWorkSite_googlesearch_By_EmpList(prefixText.Trim(), WSID);
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
            WSID = Convert.ToInt32(ddlWorksite_hid.Value == "" ? "0" : ddlWorksite_hid.Value); ;
          //  WSId = Convert.ToInt32(ddlWs_hid.Value == "" ? "0" : ddlWs_hid.Value); ;
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
           
            Site = 0;
            Site = Convert.ToInt32(ddlDepartment_hid.Value == "" ? "0" : ddlDepartment_hid.Value); ;
            CompanyID = Convert.ToInt32(Session["CompanyID"]);
           
        }


    }
}