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
    public partial class PaySlip : AECLOGIC.ERP.COMMON.WebFormMaster
    {
        AttendanceDAC objEmployee = new AttendanceDAC();
        AttendanceDAC objRights = new AttendanceDAC();
        HRCommon objHrCommon = new HRCommon();
        
        int mid = 0;
        bool viewall;
        string menuname;
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
         
          
            if (!IsPostBack)
            {
                GetParentMenuId();
                BindWorkSites();
                BindDepartments();
                BindYears();
                EmployeBind(objHrCommon);
            }
        }
        public int GetParentMenuId()
        {
            string URL = Request.Url.Segments[Request.Url.Segments.Length - 1] + Request.Url.Query;
            int RoleId = Convert.ToInt32(Session["RoleId"].ToString());
            int ModuleId = ModuleID;;

          DataSet  ds = AttendanceDAC.GetAllowed(RoleId, ModuleId, URL);
            int MenuId = 0;
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                MenuId = Convert.ToInt32(ds.Tables[0].Rows[0]["Under"]);
                gvPaySlip.Columns[6].Visible = Convert.ToBoolean(ds.Tables[0].Rows[0]["Editable"].ToString());
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
                objHrCommon.SiteID = Convert.ToInt32(ddlworksites.SelectedItem.Value);
                objHrCommon.DeptID = Convert.ToDouble(ddldepartments.SelectedItem.Value);
                if (rbActive.Checked)
                {
                    objHrCommon.CurrentStatus = 'y';
                }
                else
                {
                    objHrCommon.CurrentStatus = 'n';
                }
                objHrCommon.FName = txtusername.Text;
                
                gvPaySlip.DataSource = null;
                gvPaySlip.DataBind();
                DataSet ds = (DataSet)objEmployee.GetEmpSalriesList(objHrCommon);
                if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0)
                {
                    gvPaySlip.DataSource = ds;
                    gvPaySlip.DataBind();
                }
                EmpListPaging.Bind(objHrCommon.CurrentPage, objHrCommon.TotalPages, objHrCommon.NoofRecords, objHrCommon.PageSize);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void BindWorkSites()
        {

            try
            {

                DataSet ds = AttendanceDAC.GetWorkSite(0, '1', Convert.ToInt32(Session["CompanyID"]));
                ViewState["WorkSites"] = ds;
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
        public void BindDepartments()
        {
            try
            {

                DataSet ds = (DataSet)objRights.GetDaprtmentList();
                ViewState["Departments"] = ds;
                if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ddldepartments.DataValueField = "DepartmentUId";
                    ddldepartments.DataTextField = "DeptName";
                    ddldepartments.DataSource = ds;
                    ddldepartments.DataBind();
                    ddldepartments.Items.Insert(0, new ListItem("---ALL---", "0"));
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {


        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            objHrCommon.SiteID = Convert.ToInt32(ddlworksites.SelectedItem.Value);
            objHrCommon.DeptID = Convert.ToDouble(ddldepartments.SelectedItem.Value);
            objHrCommon.FName = txtusername.Text;
            EmployeBind(objHrCommon);
        }

        protected void rbActive_CheckedChanged(object sender, EventArgs e)
        {
            EmployeBind(objHrCommon);
        }
        protected void rbInActive_CheckedChanged(object sender, EventArgs e)
        {
            EmployeBind(objHrCommon);
        }
        public string DocNavigateUrl(string EmpId)
        {
            string Date = ddlMonth.SelectedItem.Value + "/" + "1" + "/" + ddlYear.SelectedItem.Value;
            string ReturnVal = "";
            ReturnVal = String.Format("PaySlipPreview.aspx?id={0}&Date={1}", EmpId, Date);
            return ReturnVal;
        }


        protected void ddlMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            EmployeBind(objHrCommon);
        }
        protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            EmployeBind(objHrCommon);
        }
    }
}