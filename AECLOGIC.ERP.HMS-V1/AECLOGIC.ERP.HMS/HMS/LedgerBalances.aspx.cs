using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using AECLOGIC.HMS.BLL;
using System.Collections.Generic;
using AECLOGIC.ERP.COMMON;
using Aeclogic.Common.DAL;
using AECLOGIC.ERP.HMS.HRClasses;


//using CommonDoc;
namespace AECLOGIC.ERP.HMS
{
    public partial class LedgerBalances : AECLOGIC.ERP.COMMON.WebFormMaster
    {
      

        decimal TotalCr; decimal TotalDr;

        Paging_Objects PG = new Paging_Objects();

        protected override void OnInit(EventArgs e)
        {
            ModuleID = 1;
            base.OnInit(e);

            pgledger.FirstClick += new Paging.PageFirst(EmpListPaging_FirstClick);
            pgledger.PreviousClick += new Paging.PagePrevious(EmpListPaging_FirstClick);
            pgledger.NextClick += new Paging.PageNext(EmpListPaging_FirstClick);
            pgledger.LastClick += new Paging.PageLast(EmpListPaging_FirstClick);
            pgledger.ChangeClick += new Paging.PageChange(EmpListPaging_FirstClick);
            pgledger.ShowRowsClick += new Paging.ShowRowsChange(EmpListPaging_ShowRowsClick);
            pgledger.CurrentPage = 1;
        }

        void EmpListPaging_ShowRowsClick(object sender, EventArgs e)
        {
            pgledger.CurrentPage = 1;
            BindPager();
        }

        void EmpListPaging_FirstClick(object sender, EventArgs e)
        {
            BindPager();
        }

        void BindPager()
        {
            PG.PageSize = pgledger.CurrentPage;
            PG.CurrentPage = pgledger.ShowRows;
            BindGrid();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
          
            if (!IsPostBack)
            {
                GetParentMenuId();
                try
                {

                    ViewState["WSID"] = 0;
                    if (Convert.ToInt32(Session["RoleId"].ToString()) == 7)
                    {
                        try
                        {

                            DataSet ds = clViewCPRoles.HR_DailyAttStatus( Convert.ToInt32(Session["UserId"]));
                            ViewState["WSID"] = ds.Tables[0].Rows[0]["ID"].ToString();
                            txtSearchWorksite.Text = ds.Tables[0].Rows[0]["Name"].ToString();
                            txtSearchWorksite.ReadOnly = true;

                        }
                        catch { }
                    }
                }
                catch { }
                BindEmpList();
                BindGrid();
            }
        }
        public int GetParentMenuId()
        {
            string URL = Request.Url.Segments[Request.Url.Segments.Length - 1] + Request.Url.Query;
            int RoleId = Convert.ToInt32(Session["RoleId"].ToString());
            int ModuleId = ModuleID; ;

            DataSet ds = new DataSet();

            ds = AttendanceDAC.GetAllowed(RoleId, ModuleId, URL);
            int MenuId = 0;
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                MenuId = Convert.ToInt32(ds.Tables[0].Rows[0]["Under"]);
                ViewState["Editable"] = (bool)ds.Tables[0].Rows[0]["Editable"];
                ViewState["ViewAll"] = (bool)ds.Tables[0].Rows[0]["ViewAll"];
               
            }
            return MenuId;
        }
        public void BindEmpList()
        {
               
            string EmpName = string.Empty;
            int WorkSite = Convert.ToInt32(null);
            if (Convert.ToInt32(Session["Site"]) != 0)
            {
                WorkSite = Convert.ToInt32(Session["Site"]);
            }
            DataSet ds1 = AttendanceDAC.GetGoogleSearch_ForGeneralEmployee(WorkSite);
            int Dept = Convert.ToInt32(null);

            int? EmpId = null;
            bool i = (bool)ViewState["ViewAll"];
            if (i == false)
            {
                EmpId =  Convert.ToInt32(Session["UserId"]);
                ddlEmployee.Enabled = false;
                txtSearchWorksite.Text = ds1.Tables[0].Rows[0][0].ToString();
                txtSearchWorksite.Enabled = false;
                BtnEmpSearch.Enabled = false;
            }
            else
            {
                ddlEmployee.Enabled = true;
                BtnEmpSearch.Enabled = true;
            }
           
           DataSet ds = AttendanceDAC.HR_GetEmpLedersByEmpId(EmpId, Convert.ToInt32(Session["CompanyID"]));
            ddlEmployee.DataSource = ds.Tables[0];
            ddlEmployee.DataTextField = "ledger";
            ddlEmployee.DataValueField = "ledgerid";
            ddlEmployee.DataBind();
            if (i == true)
                ddlEmployee.Items.Insert(0, new ListItem("---SELECT---", "0", true));

        }
    

        protected void GvLedgerBalances_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "View")
            {
                LinkButton lnk = new LinkButton();
                lnk = (LinkButton)e.CommandSource;
                GridViewRow selectedRow = (GridViewRow)lnk.Parent.Parent;

                string strScript = "<script> ";
                strScript += "var newWindow = window.showModalDialog('LedgerBalancesDetails.aspx?id=" + e.CommandArgument + "', '','dialogHeight: 400px; dialogWidth: 800px; edge: Raised; center: Yes; resizable: Yes; status: No;location:0;');";
                strScript += "</script>";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "PopupCP", strScript, false);
            }
        }
        protected void GvLedgerBalances_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }
        public string TotalBalance;
        public string TotalCredits;
        public string TotalDebits;
        public void BindGrid()
        {
            int CompanyID = Convert.ToInt32(Session["CompanyID"]);
            DataSet dsGroup = AttendanceDAC.HR_GetEmployeeGroup();
            int GroupID = Convert.ToInt32(dsGroup.Tables[0].Rows[0][0]);
               
            SqlParameter[] p = new SqlParameter[8];
            p[0] = new SqlParameter("@CompanyID", CompanyID);
            p[1] = new SqlParameter("@GroupID", GroupID);
            //   try { p[2] = new SqlParameter("@WorkSiteID", Convert.ToInt32(ddlWorksite.SelectedValue)); }
            int WorkSite = 1;
            try
            {
                WorkSite = Convert.ToInt32((Convert.ToInt32(ddlWorksite_hid.Value == "" ? "0" : ddlWorksite_hid.Value)));
            }
            catch { }
            try
            {
                if (Convert.ToInt32(ViewState["WSID"]) > 0)
                    WorkSite = Convert.ToInt32(ViewState["WSID"]);
            }
            catch { }
            try { p[2] = new SqlParameter("@WorkSiteID", WorkSite); }
            catch { p[2] = new SqlParameter("@WorkSiteID", 1); }
            p[3] = new SqlParameter("@CurrentPage", pgledger.CurrentPage);
            p[4] = new SqlParameter("@PageSize", pgledger.ShowRows);
            p[5] = new SqlParameter("@NoofRecords", 3);
            p[5].Direction = ParameterDirection.Output;
            p[6] = new SqlParameter();
            p[6].Direction = ParameterDirection.ReturnValue;
            p[7] = new SqlParameter("@LedgerID", Convert.ToInt32(ddlEmployee.SelectedValue));
            DataSet ds = (DataSet)SqlHelper.ExecuteDataset("HR_ACC_LedgerBalances", p);
            if (ds != null)
            {
                if (ds.Tables.Count > 1 && ds.Tables[1].Rows.Count > 0)
                {
                    TotalBalance = ds.Tables[1].Rows[0]["Balance"].ToString();
                    TotalCredits = ds.Tables[1].Rows[0]["Credit"].ToString();
                    TotalDebits = ds.Tables[1].Rows[0]["Debit"].ToString();
                }
            }
            GvLedgerBalances.DataSource = ds;
            GvLedgerBalances.DataBind();
            int totpage = Convert.ToInt32(p[6].Value);
            int noofrec = Convert.ToInt32(p[5].Value);
            PG.TotalPages = totpage;
            PG.NoofRecords = noofrec;
            pgledger.Bind(pgledger.CurrentPage, PG.TotalPages, PG.NoofRecords, PG.PageSize);
            //txtLedgerName.Text = "";
        }
      

        protected void BtnEmpSearch_Click(object sender, EventArgs e)
        {
            BindGrid();
        }
     

        protected void ddlEmployee_SelectedIndexChanged(object sender, EventArgs e)
        {
           
            BindGrid();
        }

        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionList(string prefixText, int count, string contextKey)
        {
           
            DataSet ds = AttendanceDAC.GetGoogleSearch_by_WorkSite_by_Lenders(prefixText.Trim());
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

       

    }
}
