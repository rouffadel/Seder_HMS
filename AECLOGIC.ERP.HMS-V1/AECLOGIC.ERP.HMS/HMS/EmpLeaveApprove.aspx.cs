using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AECLOGIC.HMS.BLL;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using AECLOGIC.ERP.COMMON;


namespace AECLOGIC.ERP.HMS
{
    public partial class EmpLeaveApprove : AECLOGIC.ERP.COMMON.WebFormMaster
    {
        #region Declaration

        HRCommon objHrCommon = new HRCommon();
        AttendanceDAC objEmployee = new AttendanceDAC();
        int mid = 0;
        bool viewall;
        string menuname;
        string menuid;
        string ReturnVal = "";
        #endregion

        #region OnInIt
        protected override void OnInit(EventArgs e)
        {
            ModuleID = 1;
            base.OnInit(e);
            EmpLeaveApprovePaging.FirstClick += new Paging.PageFirst(EmpLeaveApprovePaging_FirstClick);
            EmpLeaveApprovePaging.PreviousClick += new Paging.PagePrevious(EmpLeaveApprovePaging_FirstClick);
            EmpLeaveApprovePaging.NextClick += new Paging.PageNext(EmpLeaveApprovePaging_FirstClick);
            EmpLeaveApprovePaging.LastClick += new Paging.PageLast(EmpLeaveApprovePaging_FirstClick);
            EmpLeaveApprovePaging.ChangeClick += new Paging.PageChange(EmpLeaveApprovePaging_FirstClick);
            EmpLeaveApprovePaging.ShowRowsClick += new Paging.ShowRowsChange(EmpLeaveApprovePaging_ShowRowsClick);
            EmpLeaveApprovePaging.CurrentPage = 1;
        }
        void EmpLeaveApprovePaging_ShowRowsClick(object sender, EventArgs e)
        {
            EmpLeaveApprovePaging.CurrentPage = 1;
            BindPager();
        }
        void EmpLeaveApprovePaging_FirstClick(object sender, EventArgs e)
        {
            BindPager();
        }
        void BindPager()
        {
            objHrCommon.PageSize = EmpLeaveApprovePaging.CurrentPage;
            objHrCommon.CurrentPage = EmpLeaveApprovePaging.ShowRows;
            EmployeBind(objHrCommon);
        }
        #endregion

        #region PageLoad
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindWorkSites();
                BindDepartments();
                BindEmpNatures();
                BindPager();
            }
        }

        #endregion PageLoad

        #region SupportingMethods
     
        void EmployeBind(HRCommon objHrCommon)
        {

            try
            {
                objHrCommon.PageSize = EmpLeaveApprovePaging.ShowRows;
                objHrCommon.CurrentPage = EmpLeaveApprovePaging.CurrentPage;
                objHrCommon.SiteID = Convert.ToInt32(ddlworksites.SelectedItem.Value);
                objHrCommon.DeptID = Convert.ToDouble(ddldepartments.SelectedItem.Value);
                int? EmpNatureID = null;
                if (ddlEmpNature.SelectedValue != "0")
                    EmpNatureID = Convert.ToInt32(ddlEmpNature.SelectedValue);
                objHrCommon.CurrentStatus = 'y';
                objHrCommon.FName = txtusername.Text;
                 
                grdEmpLeaveApp.DataSource = null;
                grdEmpLeaveApp.DataBind();
                int? EmpID = null;
                if (txtEmpID.Text != "")
                {
                    EmpID = Convert.ToInt32(txtEmpID.Text);
                }
              DataSet  ds = (DataSet)objEmployee.GetEmployeesByPageOrderByAssID(objHrCommon, 0, 0, EmpNatureID, EmpID, Convert.ToInt32(Session["CompanyID"]), null, null);
                if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0)
                {
                    grdEmpLeaveApp.DataSource = ds;
                    grdEmpLeaveApp.DataBind();
                }
                EmpLeaveApprovePaging.Bind(objHrCommon.CurrentPage, objHrCommon.TotalPages, objHrCommon.NoofRecords, objHrCommon.PageSize);

            }
            catch (Exception e)
            {
                throw e;
            }
        }
        #region BindDDL
        public void BindWorkSites()
        {
            try
            {

                DataSet ds = objEmployee.GetWorkSiteByEmpID( Convert.ToInt32(Session["UserId"]), Convert.ToInt32(Session["CompanyID"]), Convert.ToInt32(Session["RoleId"]));
                if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ddlworksites.DataSource = ds.Tables[0];
                    ddlworksites.DataTextField = "Site_Name";
                    ddlworksites.DataValueField = "Site_ID";
                    ddlworksites.DataBind();
                    ddlworksites.Items.Insert(0, new ListItem("---All---", "0"));
                }
            }
            catch (Exception e)
            {
                throw e;
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
        public void BindDepartments()
        {
            try
            {

                DataSet ds = (DataSet)objEmployee.GetDaprtmentList();
                if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ddldepartments.DataValueField = "DepartmentUId";
                    ddldepartments.DataTextField = "DeptName";
                    ddldepartments.DataSource = ds;
                    ddldepartments.DataBind();
                    ddldepartments.Items.Insert(0, new ListItem("---All---", "0"));
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        #endregion
        #region BindDDLOnGrid
        public DataSet BindRec()
        {
            return (DataSet)ViewState["RetRec"];
        }
        public static ArrayList alRec = new ArrayList();
      
        #endregion
        #endregion

        #region Events

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindPager();
        }

        #endregion

        protected void grdEmpLeaveApp_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblEmpId = (Label)e.Row.Cells[0].FindControl("lblEmpID");
                DropDownList grdddlRec1 = (DropDownList)e.Row.Cells[5].FindControl("ddlReco1");
                DropDownList grdddlRec2 = (DropDownList)e.Row.Cells[6].FindControl("ddlReco2");
                DropDownList grdddlRec3 = (DropDownList)e.Row.Cells[7].FindControl("ddlReco3");

                int EmpID = int.Parse(lblEmpId.Text);
              DataSet  RetRec = objEmployee.GetLeaveApproveEmps(EmpID);
                grdddlRec1.DataSource = grdddlRec2.DataSource = grdddlRec3.DataSource = RetRec;
                grdddlRec1.DataBind();
                grdddlRec2.DataBind();
                grdddlRec3.DataBind();
            }
        }
        protected void grdEmpLeaveApp_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Upd")
            {
                GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).Parent.Parent;
                DropDownList ddlRec1 = (DropDownList)grdEmpLeaveApp.Rows[row.RowIndex].FindControl("ddlReco1");
                int RecEmpID = int.Parse(ddlRec1.SelectedValue);
                DropDownList ddlRec2 = (DropDownList)grdEmpLeaveApp.Rows[row.RowIndex].FindControl("ddlReco2");
                int Level1 = int.Parse(ddlRec2.SelectedValue);
                DropDownList ddlRec3 = (DropDownList)grdEmpLeaveApp.Rows[row.RowIndex].FindControl("ddlReco3");
                int Level2 = int.Parse(ddlRec3.SelectedValue);
                int EmpID = Convert.ToInt32(e.CommandArgument);
                int OutPut = objEmployee.InsUpdLeaveAppLevels(EmpID, RecEmpID, Level1, Level2);
                if (OutPut == 1)
                    AlertMsg.MsgBox(Page, "Inserted sucessfully.!");
                else if (OutPut == 3)
                    AlertMsg.MsgBox(Page, "Updated sucessfully.!");
            }
        }
    }

}