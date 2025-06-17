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
    public partial class WhohaveSystems : AECLOGIC.ERP.COMMON.WebFormMaster
    {
        AttendanceDAC objEmployee = new AttendanceDAC();
        AttendanceDAC objRights = new AttendanceDAC();
        HRCommon objHrCommon = new HRCommon();
       
        int mid = 0;
        bool viewall;
        string menuname;
        string menuid;
        string ReturnVal = "";
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
          
            Ajax.Utility.RegisterTypeForAjax(typeof(AjaxDAL));
            if (!IsPostBack)
            {
                GetParentMenuId();
                BindWorkSites();
                BindDepartments();
                EmployeBind(objHrCommon);
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
              
                menuid = MenuId.ToString();
                mid = Convert.ToInt32(ds.Tables[0].Rows[0]["MenuId"].ToString());
            }
            return MenuId;
        }
        public void BindWorkSites()
        {

            try
            {
               
               DataSet ds = objRights.GetWorkSiteByEmpID( Convert.ToInt32(Session["UserId"]), Convert.ToInt32(Session["CompanyID"]), Convert.ToInt32(Session["RoleId"]));
                ViewState["WorkSites"] = ds;
                if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0)
                {

                    ddlworksites.DataSource = ds.Tables[0];
                    ddlworksites.DataTextField = "Site_Name";
                    ddlworksites.DataValueField = "Site_ID";
                    ddlworksites.DataBind();
                    if (Convert.ToInt32(Session["MonitorSite"]) != 0)
                    {
                        ddlworksites.Items.FindByValue(Session["MonitorSite"].ToString()).Selected = true;
                        ddlworksites.Enabled = false;
                    }
                    else
                    {
                        ddlworksites.Items.Insert(0, new ListItem("---ALL---", "0"));
                    }
                }
                else
                {
                    ddlworksites.Items.Insert(0, new ListItem("---ALL---", "0"));
                }
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
               
              DataSet  ds = (DataSet)objRights.GetDaprtmentList();
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


        void EmployeBind(HRCommon objHrCommon)
        {

            try
            {
                objHrCommon.PageSize = EmpListPaging.ShowRows;
                objHrCommon.CurrentPage = EmpListPaging.CurrentPage;
                objHrCommon.SiteID = Convert.ToInt32(ddlworksites.SelectedItem.Value);
                objHrCommon.DeptID = Convert.ToDouble(ddldepartments.SelectedItem.Value);

               
                gdvAttend.DataSource = null;
                gdvAttend.DataBind();
                DataSet ds = (DataSet)objEmployee.GetEmployeesWhohaveSystems(objHrCommon);
                if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0)
                {
                    gdvAttend.DataSource = ds;
                    gdvAttend.DataBind();
                }
                EmpListPaging.Bind(objHrCommon.CurrentPage, objHrCommon.TotalPages, objHrCommon.NoofRecords, objHrCommon.PageSize);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            EmployeBind(objHrCommon);
        }
        protected void gdvAttend_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                CheckBox chkSelect = (CheckBox)e.Row.FindControl("chkSelect");
                Label lblSts = (Label)e.Row.FindControl("lblChkvalue");
                Label lbEmpID = (Label)e.Row.FindControl("lblEmpID");
                if (lblSts.Text == "True")
                {
                    chkSelect.Checked = true;
                }
                else
                {
                    chkSelect.Checked = false;
                }
                chkSelect.Attributes.Add("onclick", "javascript:return InsUpdWhohavesys('" + chkSelect.ClientID + "','" + lbEmpID.Text + "');");
            }
        }
    }
}