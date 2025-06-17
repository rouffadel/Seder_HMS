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
using System.Data.SqlClient;
using AECLOGIC.HMS.BLL;
using AECLOGIC.ERP.HMS.HRClasses;


namespace AECLOGIC.ERP.HMS
{
    public partial class EmployeeOrder : AECLOGIC.ERP.COMMON.WebFormMaster
    {
        AttendanceDAC objEmployee = new AttendanceDAC();
        HRCommon objHrCommon = new HRCommon();

         
        AttendanceDAC objRights = new AttendanceDAC();
        int mid = 0;
        bool viewall;
        static int SearchCompanyID;
        static int Siteid;
        static int Empdeptid = 0;
        string menuname;
        string menuid;
        protected override void OnInit(EventArgs e)
        {
            ModuleID = 1;
            base.OnInit(e);
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                GetParentMenuId();
                BindWorkSites();
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
            }
            
        }
        public int GetParentMenuId()
        {
            string URL = Request.Url.Segments[Request.Url.Segments.Length - 1] + Request.Url.Query;
            int RoleId = Convert.ToInt32(Session["RoleId"].ToString());
            int ModuleId = ModuleID;;

            SearchCompanyID = Convert.ToInt32(Session["CompanyID"]);

          DataSet  ds = AttendanceDAC.GetAllowed(RoleId, ModuleId, URL);
            int MenuId = 0;
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                MenuId = Convert.ToInt32(ds.Tables[0].Rows[0]["Under"]);
               
                //btnDown.Enabled = btnDown.Enabled = btnFirst.Enabled = btnLast.Enabled = btnSubmit.Enabled = btnUp.Enabled = (bool)ds.Tables[0].Rows[0]["Editable"];
                //btnDown.Visible = btnDown.Visible = btnFirst.Visible = btnLast.Visible = btnSubmit.Visible = btnUp.Visible = (bool)ds.Tables[0].Rows[0]["Editable"];
               
            }
            return MenuId;
        }
        public void BindWorkSites()
        {

            try
            {

                DataSet ds = AttendanceDAC.GetWorkSite_By_EmpOrder();
                if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ddlworksites.DataSource = ds.Tables[0];
                    ddlworksites.DataTextField = "Site_Name";
                    ddlworksites.DataValueField = "Site_ID";
                    ddlworksites.DataBind();
                }
                ddlworksites.Items.Insert(0, new ListItem("---ALL---", "0"));

            }
            catch (Exception e)
            {
                throw e;
            }
        }
        
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            dvemplist.Visible = true;
            int DeptID = Convert.ToInt32(ddldepartments.SelectedItem.Value);
            int WSID;
            if (!string.IsNullOrEmpty(ViewState["WSID"].ToString()))
            {
         
                WSID = Convert.ToInt32(ddlworksites.SelectedItem.Value);
            }
            else
                WSID = Convert.ToInt32(ddlworksites.SelectedItem.Value);
          
            lstEmployee.DataSource = objEmployee.GetEmpDetailsByPreOrder(DeptID, WSID);
            
            lstEmployee.DataTextField = "Name";
            lstEmployee.DataValueField = "EmpId";
            lstEmployee.DataBind();
            
        }

        protected void btnUp_Click(object sender, EventArgs e)
        {
            try
            {
                if (lstEmployee.SelectedIndex != 0 && lstEmployee.SelectedIndex != -1)
                {
                    ListItem item = lstEmployee.SelectedItem;
                    int index = lstEmployee.SelectedIndex;
                    lstEmployee.Items.RemoveAt(index);
                    lstEmployee.Items.Insert(index - 1, item);
                }

            }
            catch (Exception)
            {
                throw;
            }
        }
        protected void btnDown_Click(object sender, EventArgs e)
        {
            try
            {
                if (lstEmployee.Items.Count != 0)
                {

                    if (lstEmployee.SelectedIndex != lstEmployee.Items.Count - 1 && lstEmployee.SelectedIndex != -1)
                    {
                        ListItem item = lstEmployee.SelectedItem;
                        int index = lstEmployee.SelectedIndex;
                        lstEmployee.Items.RemoveAt(index);
                        lstEmployee.Items.Insert(index + 1, item);
                    }

                }

            }
            catch (Exception)
            {
                throw;
            }
        }
        protected void btnFirst_Click(object sender, EventArgs e)
        {
            try
            {
                if (lstEmployee.SelectedIndex != 0 && lstEmployee.SelectedIndex != -1)
                {

                    ListItem item = lstEmployee.SelectedItem;
                    int index = lstEmployee.SelectedIndex;
                    lstEmployee.Items.RemoveAt(index);
                    lstEmployee.Items.Insert(0, item);
                }

            }
            catch (Exception)
            {
                throw;
            }
        }
        protected void btnLast_Click(object sender, EventArgs e)
        {


            try
            {
                int Count = lstEmployee.Items.Count;
                if (lstEmployee.Items.Count != 0)
                {

                    if (lstEmployee.SelectedIndex != lstEmployee.Items.Count - 1 && lstEmployee.SelectedIndex != -1)
                    {

                        ListItem item = lstEmployee.SelectedItem;
                        int index = lstEmployee.SelectedIndex;

                        lstEmployee.Items.RemoveAt(index);
                        lstEmployee.Items.Insert(Count - 1, item);
                    }

                }

            }
            catch (Exception)
            {
                throw;
            }
        }

        protected void GetWork(object sender, EventArgs e)
        {

            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@Search", txtSearchWorksite.Text);
            FIllObject.FillDropDown(ref ddlworksites, "HR_GetWorkSite_By_EmpOrderFilter", param);
            ListItem itmSelected = ddlworksites.Items.FindByText(txtSearchWorksite.Text);
            if (itmSelected != null)
            {
                ddlworksites.SelectedItem.Selected = false;
                itmSelected.Selected = true;
            }
            ddlworksites_SelectedIndexChanged(sender, e);
        }

        protected void GetDept(object sender, EventArgs e)
        {
            try { 
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@Search", txtdept.Text);
            param[1] = new SqlParameter("@CompanyID", SearchCompanyID);
            param[2] = new SqlParameter("@SiteID", Siteid);

            FIllObject.FillDropDown(ref ddldepartments, "HR_GetDepartmentBySiteFilter", param);
            ListItem itmSelected = ddldepartments.Items.FindByText(txtdept.Text);
            if (itmSelected != null )
            {
                ddldepartments.SelectedItem.Selected = false;
                itmSelected.Selected = true;
            }
            if (txtdept.Text != "0") { ddldepartments.SelectedIndex = 1; }
            }
            catch { }
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                int Count = lstEmployee.Items.Count;
                for (int i = 0; i < Count; i++)
                {
                    int EmpID = Convert.ToInt32(lstEmployee.Items[i].Value.ToString());
                    int Order = i + 1;
                    objEmployee.EmpDisplayoder(EmpID, Order);

                }
                AlertMsg.MsgBox(Page, "Employee(s) reordered successfully");
            }
            catch (Exception)
            {

                throw;
            }

        }
        //Added By Rijwan for Worksite Google Search:13-03-2016 
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionList(string prefixText, int count, string contextKey)
        {
            DataSet ds = AttendanceDAC.GetWorkSiteActive(prefixText);
            return ConvertStingArray(ds);// txtItems.ToArray();
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionListDep(string prefixText, int count, string contextKey)
        {
            DataSet ds = AttendanceDAC.GetSearchDesiginationFilterActive(prefixText, SearchCompanyID, Siteid);
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
        protected void ddlworksites_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindDeparmetBySite(Convert.ToInt32(ddlworksites.SelectedValue));
            ViewState["SiteidS"] = Convert.ToInt32(ddlworksites.SelectedValue);
            Siteid = Convert.ToInt32(ViewState["SiteidS"]);
        }
        public void BindDeparmetBySite(int Site)
        {
            DataSet ds = AttendanceDAC.BindDeparmetBySite(Site, Convert.ToInt32(Session["CompanyID"]));
            ddldepartments.DataSource = ds;
            ddldepartments.DataTextField = "DeptName";
            ddldepartments.DataValueField = "DepartmentUId";
            ddldepartments.DataBind();
            ddldepartments.Items.Insert(0, new ListItem("---ALL---", "0", true));
        }
    }
}