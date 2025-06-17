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
    public partial class NMRSalaries : AECLOGIC.ERP.COMMON.WebFormMaster
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
                    BindEmpNatures();
                    BindWorkSites();
                    BindYears();

                    if (Session["Site"].ToString() != "1")
                    {
                        ddlworksites.ClearSelection();
                        ddlworksites.Items.FindByValue(Session["Site"].ToString()).Selected = true;
                        ddlworksites.Enabled = false;
                        objHrCommon.SiteID = Convert.ToInt32(Session["Site"].ToString());
                        EmployeBind(objHrCommon);
                    }
                }
                catch
                {
                    Response.Redirect("Home.aspx");
                }
            }
        }
      

        public void BindEmpNatures()
        {
            
           DataSet ds = Leaves.GetEmpNatureList(1);
            ddlEmpNature.DataSource = ds;
            ddlEmpNature.DataTextField = "Nature";
            ddlEmpNature.DataValueField = "NatureOfEmp";
            ddlEmpNature.DataBind();
            ddlEmpNature.Items.Insert(0, new ListItem("---All---", "0"));
        }
        public void BindEmp(int EmpID, int Month, int Year)
        {
            
            AttendanceDAC ADAC = new AttendanceDAC();
            DataSet ds = ADAC.HR_NMRSalriesListByEmpID(EmpID, Month, Year);
            if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0)
            {
                gvPaySlip.DataSource = ds;
            }
            gvPaySlip.DataBind();

            try { gvPaySlip.FooterRow.Visible = false; }
            catch { }
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
            //if we are changed to new year
            if (ds.Tables[0].Rows[0]["PreviousMonth"].ToString() == "0")
            {
                ddlMonth.SelectedValue = "12";

                int CurrentYear = Convert.ToInt32(ds.Tables[0].Rows[0]["CurrentYear"]);
                int PreviousYear = CurrentYear - 1;
                ddlYear.Items.FindByValue(PreviousYear.ToString()).Selected = true;

            }
            //if we are in same year and same month
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

                int CurrentStatus = 1;
                if (rbActive.Checked)
                {
                    CurrentStatus = 1;
                }
                else
                {
                    CurrentStatus = 0;
                }


                gvPaySlip.DataSource = null;
                gvPaySlip.DataBind();
                string Name = txtEmpName.Text;
                DataSet dsBind = (DataSet)objEmployee.GetNMRSalaries(objHrCommon, Name, Convert.ToInt32(Session["CompanyID"]), CurrentStatus);
                ViewState["NoOfRecords"] = objHrCommon.NoofRecords;

                if (dsBind != null && dsBind.Tables.Count != 0 && dsBind.Tables[0].Rows.Count > 0)
                {
                    gvPaySlip.DataSource = dsBind;
                    EmpListPaging.Bind(objHrCommon.CurrentPage, objHrCommon.TotalPages, objHrCommon.NoofRecords, objHrCommon.PageSize);
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
                BindEmp(EmpID, Month, Year);

                //EmpListPaging.Visible = false;
                try { gvPaySlip.FooterRow.Visible = false; }
                catch { }
            }
        }

        protected void rbActive_CheckedChanged(object sender, EventArgs e)
        {
            EmployeBind(objHrCommon);
        }
        protected void rbInActive_CheckedChanged(object sender, EventArgs e)
        {
            EmployeBind(objHrCommon);
        }
        protected void txtDay_TextChanged(object sender, EventArgs e)
        {
            EmployeBind(objHrCommon);
        }
        //Grid Total For  NetAmount
        decimal TotalAmount = 0;

        protected string GetAmount()
        {
            return TotalAmount.ToString("N2");
        }

        protected string GetNetAmount(decimal Price)
        {
            string amt = string.Empty;
            Price = Convert.ToDecimal(Price.ToString("N2"));
            TotalAmount += Price;
            if (Price != 0)
            {
                amt = Price.ToString("N2");
            }
            else
            {
                amt = "0";
            }
            return amt;

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