using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AECLOGIC.HMS.BLL;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace AECLOGIC.ERP.HMS
{
    public partial class MonitoringSites : AECLOGIC.ERP.COMMON.WebFormMaster
    {
        int Id = 1;
        int mid = 0;
        bool viewall;
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
                ViewState["MIID"] = 0;
                GetWorkSites();
                GetDepartments();
                BindEmpList();
                if (Request.QueryString.Count > 0)
                {
                    tblEditing.Visible = false;
                    tblAdd.Visible = true;
                }
                else
                {
                    BindGrid();
                    tblEditing.Visible = true;
                    tblAdd.Visible = false;
                }
                txtMFrom.Text = DateTime.Now.ToString(ConfigurationManager.AppSettings["DateFormat"]);
            }
        }
        private void GetWorkSites()
        {
            
            AttendanceDAC ADAC = new AttendanceDAC();
           DataSet ds = AttendanceDAC.GetWorkSite(0, '1', Convert.ToInt32(Session["CompanyID"]));
            ddlWs.DataSource = ds.Tables[0];
            ddlWs.DataTextField = "Site_Name";
            ddlWs.DataTextField = "Site_Name";
            ddlWs.DataValueField = "Site_ID";
            ddlWs.DataBind();
            ddlWs.Items.Insert(0, new ListItem("---Select---", "0", true));

            ddlMWS.DataSource = ds.Tables[0];
            ddlMWS.DataTextField = "Site_Name";
            ddlMWS.DataTextField = "Site_Name";
            ddlMWS.DataValueField = "Site_ID";
            ddlMWS.DataBind();
            ddlMWS.Items.Insert(0, new ListItem("---Select---", "0", true));
        }

        private void BindGrid()
        {

            DataSet ds = AttendanceDAC.HR_GetMonitoringEngineers();
            gvView.DataSource = ds;
            gvView.DataBind();
        }
        private void GetDepartments()
        {
            
            AttendanceDAC ADAC = new AttendanceDAC();
            DataSet ds = ADAC.GetDepartments(0);
            ddlDept.DataSource = ds.Tables[0];
            ddlDept.DataTextField = "Deptname";
            //ddlDept.DataTextField = "DepartmentName";
            ddlDept.DataValueField = "DepartmentUId";
            ddlDept.DataBind();
            ddlDept.Items.Insert(0, new ListItem("---ALL---", "0", true));

        }
        public void BindEmpList()
        {
            
            string EmpName = string.Empty;
            int WorkSite = Convert.ToInt32(null);
            int Dept = Convert.ToInt32(null);
            DataSet ds = AttendanceDAC.HR_SearchReimburseEmp();

            //ds = AttendanceDAC.HR_SearchReimburseEmp(Convert.ToInt32(Session["CompanyID"]));
            ddlME.DataSource = ds.Tables[0];
            ddlME.DataTextField = "name";
            ddlME.DataValueField = "EmpID";
            ddlME.DataBind();
            lblCount.Text = ds.Tables[0].Rows.Count.ToString() + " Items Found!"; ;
            ddlME.Items.Insert(0, new ListItem("---SELECT---", "0", true));
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
               
                btnSubmit.Enabled = Convert.ToBoolean(ds.Tables[0].Rows[0]["Editable"].ToString());
                gvView.Columns[5].Visible = Convert.ToBoolean(ds.Tables[0].Rows[0]["Editable"].ToString());
            }
            return MenuId;
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            int MIID = Convert.ToInt32(ViewState["MIID"]);
            int EmpID = Convert.ToInt32(ddlME.SelectedValue);
            int WSID = Convert.ToInt32(ddlMWS.SelectedValue);
            DateTime MonitorFrom = CODEUtility.ConvertToDate(txtMFrom.Text.Trim(), DateFormat.DayMonthYear);
            DateTime? MonitorUpto = null;
            if (txtMUpto.Text != "")
            {
                MonitorUpto = CODEUtility.ConvertToDate(txtMUpto.Text.Trim(), DateFormat.DayMonthYear);//Convert.ToDateTime(txtStartDate.Text);//Convert.ToDateTime(txtDueDate.Text);
            }
            int ModuleId = ModuleID;;
            AttendanceDAC.HR_InsUpMonitorEngineer(MIID, EmpID, WSID, MonitorFrom, MonitorUpto,  Convert.ToInt32(Session["UserId"]), ModuleID);
            tblEditing.Visible = true;
            tblAdd.Visible = false;
            BindGrid();
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            int DeptNo = Convert.ToInt32(ddlDept.SelectedValue);
            int SiteID = Convert.ToInt32(ddlWs.SelectedValue);
            int EmpID = 0; string EmpName = "";
            if (txtEmpID.Text != "")
            {
                try
                {
                    EmpID = Convert.ToInt32(txtEmpID.Text);
                }
                catch
                {
                    AlertMsg.MsgBox(Page, "Check The Data you have given!");
                }
            }
            try
            {
                EmpName = txtEmpName.Text;
            }
            catch
            {
                AlertMsg.MsgBox(Page, "Check The Data you have given!");
            }
            DataSet ds = AttendanceDAC.T_HR_EmpFilterSearch(DeptNo, SiteID, EmpID, EmpName);
            ddlME.DataSource = ds;
            ddlME.DataTextField = "Name";
            ddlME.DataValueField = "EmpID";
            ddlME.DataBind();
            lblCount.Text = ds.Tables[0].Rows.Count.ToString() + " Items Found!";
            if (ddlME.Items.Count != 1)
            {
                ddlME.Items.Insert(0, new ListItem("---SELECT---", "0", true));
            }
        }
        protected void gvView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Edt")
            {
                BindEmpList();
                GetWorkSites();
                int PID = Convert.ToInt32(e.CommandArgument);
                tblAdd.Visible = true;
                tblEditing.Visible = false;
                DataSet ds = AttendanceDAC.HR_GetMonitoringByPID(PID);
                if (ddlME.Items.FindByValue(ds.Tables[0].Rows[0][0].ToString()) != null)
                {
                    ddlME.SelectedValue = ds.Tables[0].Rows[0][0].ToString();
                }
                if (ddlMWS.Items.FindByValue(ds.Tables[0].Rows[0][1].ToString()) != null)
                {
                    ddlMWS.SelectedValue = ds.Tables[0].Rows[0][1].ToString();
                }
                txtMFrom.Text = ds.Tables[0].Rows[0][2].ToString();
                txtMUpto.Text = ds.Tables[0].Rows[0][3].ToString();
                ViewState["MIID"] = PID;
            }
            if (e.CommandName == "Del")
            {
                int PID = Convert.ToInt32(e.CommandArgument);
                AttendanceDAC.HR_DelMonitorEngineer(PID);
                BindGrid();
            }
        }

    }
}
