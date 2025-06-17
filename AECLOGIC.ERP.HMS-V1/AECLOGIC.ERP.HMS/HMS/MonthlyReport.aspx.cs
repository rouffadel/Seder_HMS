using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AECLOGIC.HMS.BLL;

namespace AECLOGIC.ERP.HMS
{
    public partial class MonthlyReport : AECLOGIC.ERP.COMMON.WebFormMaster
    {
       
        static int CompanyID;
        static int EmpID = 0;
        static int Role = 0;
        static int WSID = 0;
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
                BindYears();
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
     
        protected void btnMonthReport_Click(object sender, EventArgs e)
        {
            int SiteID = Convert.ToInt32(Convert.ToInt32(ddlWorksite_hid.Value == "" ? "0" : ddlWorksite_hid.Value));
            int year = Convert.ToInt32(ddlYear.SelectedValue);
            int Month = Convert.ToInt32(ddlMonth.SelectedValue);
            BindGrid(Month, year, SiteID);
        }
        public void BindGrid(int Month, int year, int SiteID)
        {

            DataSet ds = AttendanceDAC.GetMonthlyPresentReport(Month, year, SiteID);
            grdMonthlyReport.DataSource = ds.Tables[0];
            grdMonthlyReport.DataBind();

            //lblNoOfEmps.Text = ds.Tables[1].Rows[0]["Totalmem"].ToString();
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionList(string prefixText, int count, string contextKey)
        {
            DataSet ds = AttendanceDAC.GetWorkSiteByActiveEmpID_wsid_googlesearch(prefixText.Trim(), Role, CompanyID, EmpID, WSID);
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
        protected void GetWork(object sender, EventArgs e)
        {

            CompanyID = Convert.ToInt32(Session["CompanyID"]);
            WSID = Convert.ToInt32(ddlWorksite_hid.Value == "" ? "0" : ddlWorksite_hid.Value); ;
            EmpID = Convert.ToInt32(ddlWorksite_hid.Value == "" ? "0" : ddlWorksite_hid.Value); ;
            Role = Convert.ToInt32(ddlWorksite_hid.Value == "" ? "0" : ddlWorksite_hid.Value); ;
        }



    }
}
