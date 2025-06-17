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
using System.Collections.Generic;
using AECLOGIC.HMS.BLL;
using AECLOGIC.ERP.COMMON;

namespace AECLOGIC.ERP.HMS
{
    public partial class ExceRptReporting : AECLOGIC.ERP.COMMON.WebFormMaster
    {
        AttendanceDAC objEmployee = new AttendanceDAC();
        AttendanceDAC objRights = new AttendanceDAC();
        HRCommon objHrCommon = new HRCommon();
          
        int mid = 0;
        static int SearchCompanyID;
        static int Siteid;
        bool viewall;
        string menuname;
        string menuid;

        protected override void OnInit(EventArgs e)
        {
            ModuleID = 1;
            base.OnInit(e);
            // btnSave.Attributes.Add("onclick", "javascript:return ValidateSave('" + txtCategoryName.ClientID + "');");
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
            EmployeBind(); 
        }
        protected void Page_Load(object sender, EventArgs e)
        {
           
            SearchCompanyID = Convert.ToInt32(Session["CompanyID"]);
           
           

            if (!IsPostBack)
            {
                BindWorkSite();
               
                EmployeBind();
            }
        }

        public void BindWorkSite()
        {
              
         DataSet ds = AttendanceDAC.GetWorkSite(0, '1', Convert.ToInt32(Session["CompanyID"]));
            ddlworksites.DataSource = ds.Tables[0];
            ddlworksites.DataTextField = "Site_Name";
            ddlworksites.DataValueField = "Site_ID";
            ddlworksites.DataBind();
            ddlworksites.Items.Insert(0, new ListItem("---ALL---", "0", true));
        }

        public void EmployeBind()
        {

            try
            {

                objHrCommon.PageSize = EmpListPaging.ShowRows;
                objHrCommon.CurrentPage = EmpListPaging.CurrentPage;
                int? SiteID = null;
                int? DeptID = null;

                if (ddlworksites.SelectedItem.Value != "0")
                {
                    SiteID = Convert.ToInt32(ddlworksites.SelectedItem.Value);
                }
                if (ddldepartments.SelectedItem.Value != "0")
                {
                    DeptID = Convert.ToInt32(ddldepartments.SelectedItem.Value);
                }
                objHrCommon.OldEmpID = null;
                if (txtOldEmpID.Text != "")
                    objHrCommon.OldEmpID = txtOldEmpID.Text;

                int? EmpID = null;
                if (txtEmpID.Text != "")
                    EmpID = Convert.ToInt32(txtEmpID.Text);
                  
                gveditkbipl.DataSource = null;
                gveditkbipl.DataBind();
                
                DataSet ds = ExceReports.ExceRptReportingtoByPaging(objHrCommon, SiteID, DeptID, txtusername.Text, EmpID, Convert.ToInt32(Session["CompanyID"]));

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
        protected void Worksidechangewithdep(object sender, EventArgs e)
        {
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@Search", txtSearchWorksite.Text);
            param[1] = new SqlParameter("@CompanyID", Convert.ToInt32(Session["CompanyID"]));
            
            FIllObject.FillDropDown(ref ddlworksites, "G_GET_WorkSitebyFilter", param);
            ListItem itmSelected = ddlworksites.Items.FindByText(txtSearchWorksite.Text);
            if (itmSelected != null)
            {
                ddlworksites.SelectedItem.Selected = false;
                itmSelected.Selected = true;
                //FillProjects();
            }
            ddlworksites_SelectedIndexChanged(sender, e);
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
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            EmployeBind();
        }

        protected void ddlworksites_SelectedIndexChanged(object sender, EventArgs e)
        {
            Siteid = Convert.ToInt32(ddlworksites.SelectedValue);
          BindDeparmetBySite(Convert.ToInt32(ddlworksites.SelectedValue));
        }
        //Added by Rijwan:22-03-2016
        [System.Web.Services.WebMethodAttribute(),System.Web.Script.Services.ScriptMethodAttribute]
        public static string[] GetCompletionList(string prefixText, int count, string contextKey)
        {
            DataSet ds = AttendanceDAC.GetGoogleABCSearchWorkSite(prefixText, SearchCompanyID);
            return ConvertStingArray(ds);
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute]
        public static string[] GetCompletionListdept(string prefixText, int count, string contextKey)
        {
            DataSet ds = AttendanceDAC.GetDepartmentGoogleSerc(prefixText, SearchCompanyID,Siteid);
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