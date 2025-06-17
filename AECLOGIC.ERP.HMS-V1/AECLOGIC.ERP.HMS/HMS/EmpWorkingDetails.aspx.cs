using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using AECLOGIC.HMS.BLL;
using AECLOGIC.ERP.COMMON;

namespace AECLOGIC.ERP.HMS
{
    public partial class EmpWorkingDetails : AECLOGIC.ERP.COMMON.WebFormMaster
    {
        #region Declaration
        HRCommon objHrCommon = new HRCommon();
        AttendanceDAC objRights = new AttendanceDAC();
        static int SearchCompanyID;
        static int Siteid;
        int mid = 0;
        bool viewall;
        string menuname;
        string menuid;

        #endregion Declaration

        #region OnInIt
        protected override void OnInit(EventArgs e)
        {
            ModuleID = 1;
            base.OnInit(e);
            EmpWorkingDetailsPaging.FirstClick += new Paging.PageFirst(EmpWorkingDetailsPaging_FirstClick);
            EmpWorkingDetailsPaging.PreviousClick += new Paging.PagePrevious(EmpWorkingDetailsPaging_FirstClick);
            EmpWorkingDetailsPaging.NextClick += new Paging.PageNext(EmpWorkingDetailsPaging_FirstClick);
            EmpWorkingDetailsPaging.LastClick += new Paging.PageLast(EmpWorkingDetailsPaging_FirstClick);
            EmpWorkingDetailsPaging.ChangeClick += new Paging.PageChange(EmpWorkingDetailsPaging_FirstClick);
            EmpWorkingDetailsPaging.ShowRowsClick += new Paging.ShowRowsChange(EmpWorkingDetailsPaging_ShowRowsClick);
            EmpWorkingDetailsPaging.CurrentPage = 1;
        }
        void EmpWorkingDetailsPaging_ShowRowsClick(object sender, EventArgs e)
        {
            EmpWorkingDetailsPaging.CurrentPage = 1;
            BindPager();
        }
        void EmpWorkingDetailsPaging_FirstClick(object sender, EventArgs e)
        {
            BindPager();
        }
        void BindPager()
        {
            objHrCommon.PageSize = EmpWorkingDetailsPaging.CurrentPage;
            objHrCommon.CurrentPage = EmpWorkingDetailsPaging.ShowRows;
            BindEmpWorkingDetails(objHrCommon);
        }
        #endregion OnInIt

        #region PageLoad

        protected void Page_Load(object sender, EventArgs e)
        {
            SearchCompanyID = Convert.ToInt32(Session["CompanyID"]);
            if (!IsPostBack)
            {

                StatusChange();
                BindWorkSites();
              
                BindEmpNatures();
                BindYears();
                BindPager();
            }
        }

        #endregion PageLoad

        #region SupportingMethods

      
        public void BindWorkSites()
        {
            try
            {
                FIllObject.FillDropDown(ref ddlworksites, "HR_GetWorkSite_By_EmpList");
               

            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public void BindEmpNatures()
        {
              
     DataSet  ds = Leaves.GetEmpNatureList(1);
            ddlEmpNature.DataSource = ds;
            ddlEmpNature.DataTextField = "Nature";
            ddlEmpNature.DataValueField = "NatureOfEmp";
            ddlEmpNature.DataBind();
            ddlEmpNature.Items.Insert(0, new ListItem("---All---", "0"));
        }
        public void BindYears()
        {
              
           DataSet ds = AttendanceDAC.GetCalenderYear();

            int i = 0;
            int Maxyear = Convert.ToInt32(ds.Tables[0].Rows[0]["MaxYear"].ToString());
           
            ddlMonth.SelectedValue = ds.Tables[0].Rows[0]["CurrentMonth"].ToString();
           

            int  year = DateTime.Now.Year;
            for(i=2008;i<= year;i++)
            {
                ddlYear.Items.Add(i.ToString());
            }

            ddlYear.SelectedValue = year.ToString();

        }

        protected void Worksidechangewithdep(object sender, EventArgs e)
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@Search", txtSearchWorksite.Text);
            FIllObject.FillDropDown(ref ddlworksites, "HR_GetGoogleSearchWorkSite_By_EmpSalaries", param);
            ListItem itmSelected = ddlworksites.Items.FindByText(txtSearchWorksite.Text);
            if (itmSelected != null)
            {
                ddlworksites.SelectedItem.Selected = false;
                itmSelected.Selected = true;
                //FillProjects();
            }
            ddlworksites_SelectedIndexChanged(sender, e);
            txtSearchdept.Text = "";
        }
        protected void GetDepartment(object sender, EventArgs e)
        {
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@Search", txtSearchdept.Text);
            param[1] = new SqlParameter("@CompanyID", Convert.ToInt32(Session["CompanyID"]));
            param[2] = new SqlParameter("@SiteID", ddlworksites.SelectedItem.Value);
            FIllObject.FillDropDown(ref ddldepartments, "HMS_googlesearch_GetDepartmentBySite", param);
            ListItem itmSelected = ddldepartments.Items.FindByText(txtSearchdept.Text);
            if (itmSelected != null)
            {
                ddldepartments.SelectedItem.Selected = false;
                itmSelected.Selected = true;
            }
            else { ddldepartments.SelectedIndex = 1; }
            
        }
        public void BindEmpWorkingDetails(HRCommon objHrCommon)
        {
            try
            {
                objHrCommon.PageSize = EmpWorkingDetailsPaging.ShowRows;
                objHrCommon.CurrentPage = EmpWorkingDetailsPaging.CurrentPage;
                int? WSID = null;
                if (ddlworksites.SelectedValue != "0" && ddlworksites.SelectedValue != "")
                    WSID = Convert.ToInt32(ddlworksites.SelectedValue);
                int? DeptID = null;
                if (ddldepartments.SelectedValue != "0")
                    DeptID = Convert.ToInt32(ddldepartments.SelectedValue);
                int Year = Convert.ToInt32(ddlYear.SelectedValue);
                int? Month = null;
                if (ddlMonth.SelectedValue != "0")
                    Month = Convert.ToInt32(ddlMonth.SelectedValue);
                string Name = txtusername.Text.Trim();
                int Status = 1;         // Emp Active
                if (rblStatus.SelectedValue == "2")
                    Status = 2;         // Emp Inactive
                if (rblStatus.SelectedValue == "3")
                    Status = 3;         // Emp Working History
                int? EmpNat = null;
                if (ddlEmpNature.SelectedValue != "0")
                    EmpNat = Convert.ToInt32(ddlEmpNature.SelectedValue);
                int? Empid = null;
                if(txtempid.Text!="")
                    Empid = Convert.ToInt32(txtempid.Text);

                DataSet ds = objRights.GetEmployeesWorkDetails(objHrCommon, WSID, DeptID, Name, Month, Year, Status, EmpNat, Convert.ToInt32(Session["CompanyID"]), Empid);
                if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0)
                {
                    grdEmpWrkDetails.DataSource = ds;
                    EmpWorkingDetailsPaging.Bind(objHrCommon.CurrentPage, objHrCommon.TotalPages, objHrCommon.NoofRecords, objHrCommon.PageSize);
                    EmpWorkingDetailsPaging.Visible = true;
                }
                else
                {
                    grdEmpWrkDetails.EmptyDataText = "No Records Found";
                    EmpWorkingDetailsPaging.Visible = false;
                }
                grdEmpWrkDetails.DataBind();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public void StatusChange()
        {
            if (rblStatus.SelectedValue == "1")
            {
                grdEmpWrkDetails.Columns[7].HeaderText = "Date of join";
                grdEmpWrkDetails.Columns[7].Visible = true;
                grdEmpWrkDetails.Columns[8].Visible = false;
            }
            else if (rblStatus.SelectedValue == "2")
            {
                grdEmpWrkDetails.Columns[7].HeaderText = "Date of join";
                grdEmpWrkDetails.Columns[8].HeaderText = "Date of releave";
                grdEmpWrkDetails.Columns[7].Visible = true;
                grdEmpWrkDetails.Columns[8].Visible = true;

            }
            else
            {
                grdEmpWrkDetails.Columns[7].HeaderText = "From date";
                grdEmpWrkDetails.Columns[8].HeaderText = "To date";
                grdEmpWrkDetails.Columns[7].Visible = true;
                grdEmpWrkDetails.Columns[8].Visible = true;
            }

        }

        #endregion SupportingMethods

        #region Events

        protected void rblStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            StatusChange();
            BindPager();
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindPager();
            StatusChange();
        }

        #endregion Events

        //Added by Rijwan:22-03-2016
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute]
        public static string[] GetCompletionList(string prefixText, int count, string contextKey)
        {
            DataSet ds = AttendanceDAC.GetsalariesGoogleSearchWorkSite(prefixText);
            return ConvertStingArray(ds);
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute]
        public static string[] GetCompletionListdept(string prefixText, int count, string contextKey)
        {
            DataSet ds = AttendanceDAC.GetDepartmentGoogleSerc(prefixText, SearchCompanyID, Siteid);
            return ConvertStingArray(ds);
        }

        public static string[] ConvertStingArray(DataSet ds)
        {
            string[] rtval = Array.ConvertAll(ds.Tables[0].Select(), new Converter<DataRow, string>(DataRowTotable));
            return rtval;
        }
        public static string DataRowTotable(DataRow dr)
        {
            return dr["Name"].ToString();
        }
        protected void ddlworksites_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindDeparmetBySite(Convert.ToInt32(ddlworksites.SelectedValue));
            Siteid = Convert.ToInt32(ddlworksites.SelectedValue);
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