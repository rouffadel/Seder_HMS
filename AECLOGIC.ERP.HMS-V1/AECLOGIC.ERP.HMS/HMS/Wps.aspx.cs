using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using TPABALI.Web;
using AECLOGIC.HMS.BLL;
using AECLOGIC.ERP.COMMON;

namespace AECLOGIC.ERP.HMS
{
    public partial class Wps : AECLOGIC.ERP.COMMON.WebFormMaster
    {
        AttendanceDAC objEmployee = new AttendanceDAC();
        AttendanceDAC objRights = new AttendanceDAC();
        HRCommon objHrCommon = new HRCommon();
        
        int mid = 0;
        bool viewall;
        string menuname;
        string menuid;
        bool Editable;
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
                try
                {
                    GetParentMenuId();
                    BindEmpNatures();
                    FIllObject.FillDropDown(ref ddlworksites, "HR_GetWorkSite_By_EmpSalriesForWPS");

                 
                    BindYears();

                    EmployeBind(objHrCommon);


                }
                catch
                {
                    Response.Redirect("Home.aspx");
                }
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
               
                menuname = ds.Tables[0].Rows[0]["menuname"].ToString();
                menuid = MenuId.ToString();
                btnOutPutExcel.Enabled = (bool)ds.Tables[0].Rows[0]["Editable"];
                mid = Convert.ToInt32(ds.Tables[0].Rows[0]["MenuId"].ToString());
            }
            return MenuId;
        }
        public void BindEmpNatures()
        {
            
            DataSet ds = objEmployee.GetDesignations();
            ddlDesg.DataSource = ds;
            ddlDesg.DataTextField = "Designation";
            ddlDesg.DataValueField = "DesigId";
            ddlDesg.DataBind();
            ddlDesg.Items.Insert(0, new ListItem("---All---", "0"));
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

            #region set defalult month and year
            
            if (ds.Tables[0].Rows[0]["PreviousMonth"].ToString() == "0")
            {
                ddlMonth.SelectedValue = "12";

                int CurrentYear = Convert.ToInt32(ds.Tables[0].Rows[0]["CurrentYear"]);
                int PreviousYear = CurrentYear - 1;
                ddlYear.Items.FindByValue(PreviousYear.ToString()).Selected = true;

            }
            
            else
            {
                ddlMonth.SelectedValue = ds.Tables[0].Rows[0]["PreviousMonth"].ToString();
                if (ddlYear.Items.FindByValue(ds.Tables[0].Rows[0]["CurrentYear"].ToString()) != null)
                {
                    ddlYear.Items.FindByValue(ds.Tables[0].Rows[0]["CurrentYear"].ToString()).Selected = true;
                }
                else
                {
                    ddlYear.SelectedIndex = ddlYear.Items.Count - 1;
                }
            }
            #endregion set defalult month and year
        }

       
        int OrderID = 0, Direction = 0;
        void EmployeBind(HRCommon objHrCommon)
        {
            try
            {
                objHrCommon.PageSize = EmpListPaging.ShowRows;
                objHrCommon.CurrentPage = EmpListPaging.CurrentPage;
                objHrCommon.SiteID = Convert.ToInt32(ddlworksites.SelectedItem.Value);
                objHrCommon.DeptID = Convert.ToInt32(ddldepartments.SelectedItem.Value);
                objHrCommon.Month = Convert.ToInt32(ddlMonth.SelectedItem.Value);
                objHrCommon.Year = Convert.ToInt32(ddlYear.SelectedItem.Value);
                int? DesgID = null;
                if (ddlDesg.SelectedValue != "0")
                    DesgID = Convert.ToInt32(ddlDesg.SelectedValue);
                gvPaySlip.DataSource = null;
                gvPaySlip.DataBind();

                int? EmpID = null;

                if (txtEmpID.Text.Trim() != "")
                {
                    EmpID = Convert.ToInt32(txtEmpID.Text);
                }


                string Name = txtEmpName.Text;
                DataSet dsBind = (DataSet)objEmployee.GetEmployeesSalForWPS(objHrCommon, Name, DesgID, Convert.ToInt32(Session["CompanyID"]), EmpID);
                ViewState["NoOfRecords"] = objHrCommon.NoofRecords;
                if (dsBind != null && dsBind.Tables.Count != 0 && dsBind.Tables[0].Rows.Count > 0)
                {
                    gvPaySlip.DataSource = dsBind;
                    EmpListPaging.Bind(objHrCommon.CurrentPage, objHrCommon.TotalPages, objHrCommon.NoofRecords, objHrCommon.PageSize);
                    lblResultCount.Text = ViewState["NoOfRecords"].ToString() + " Items";
                    EmpListPaging.Visible = true;
                }
                else
                {
                    gvPaySlip.EmptyDataText = "No Records Found";
                    EmpListPaging.Visible = false;
                }
                gvPaySlip.DataBind();
            }
            catch (Exception e)
            {
                throw e;
            }
        }




        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (txtEmpID.Text == "")
            {
                objHrCommon.SiteID = Convert.ToInt32(ddlworksites.SelectedItem.Value);
                objHrCommon.DeptID = Convert.ToDouble(ddldepartments.SelectedItem.Value);
                EmployeBind(objHrCommon);
                try { gvPaySlip.FooterRow.Visible = true; }
                catch { }
            }
            else
            {
                int EmpID = 0;
                try { EmpID = Convert.ToInt32(txtEmpID.Text); }
                catch { AlertMsg.MsgBox(Page, "Check the data you have given..!"); }
                int Month = Convert.ToInt32(ddlMonth.SelectedValue);
                int Year = Convert.ToInt32(ddlYear.SelectedValue);
                try { gvPaySlip.FooterRow.Visible = false; }
                catch { }
            }
        }

        protected void BtnExportGrid_Click(object sender, EventArgs args)
        {
            int? SiteID = null;
            if (ddlworksites.SelectedValue != "0")
                SiteID = Convert.ToInt32(ddlworksites.SelectedItem.Value);
            int? DeptID = null;
            if (ddldepartments.SelectedValue != "0")
                DeptID = Convert.ToInt32(ddldepartments.SelectedItem.Value);
            int? Month = null;
            if (ddlMonth.SelectedValue != "0")
                Month = Convert.ToInt32(ddlMonth.SelectedItem.Value);
            int Year = Convert.ToInt32(ddlYear.SelectedItem.Value);

            int? EmpNat = null;
            if (ddlDesg.SelectedValue != "0")
                EmpNat = Convert.ToInt32(ddlDesg.SelectedValue);

            int? EmpID = null;
            if (txtEmpID.Text != "")
                EmpID = Convert.ToInt32(txtEmpID.Text.Trim());

            SqlDataReader dr = objEmployee.GetEmpSalWPSExportToExcel(SiteID, DeptID, Month, Year, txtEmpName.Text, EmpNat, Convert.ToInt32(Session["CompanyID"]), EmpID);
            ExportFileUtil.ExportToExcel(dr, "", "#EFEFEF", "#E6e6e6", "WPS");

        }

        protected void ddlworksites_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindDeparmetBySite(Convert.ToInt32(ddlworksites.SelectedValue));
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