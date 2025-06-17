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
using AECLOGIC.ERP.COMMON;

namespace AECLOGIC.ERP.HMS
{
    public partial class TaskAssignment : AECLOGIC.ERP.COMMON.WebFormMaster
    {
        AttendanceDAC objRights = new AttendanceDAC();
        HRCommon objHrCommon = new HRCommon();
        DataSet dstemp = new DataSet();
        AttendanceDAC objAtt = new AttendanceDAC();
        int mid = 0;
        bool viewall;
        static int? userid;
        static int companyid;
        static int? Roleids;
        static int Dept = 0;
        string menuname;
        string menuid;
        protected override void OnInit(EventArgs e)
        {
            ModuleID = 1;
            base.OnInit(e);
        
            #region AllTypes
            EmpTaskAssignPaging.FirstClick += new Paging.PageFirst(EmpTaskAssignPaging_FirstClick);
            EmpTaskAssignPaging.PreviousClick += new Paging.PagePrevious(EmpTaskAssignPaging_FirstClick);
            EmpTaskAssignPaging.NextClick += new Paging.PageNext(EmpTaskAssignPaging_FirstClick);
            EmpTaskAssignPaging.LastClick += new Paging.PageLast(EmpTaskAssignPaging_FirstClick);
            EmpTaskAssignPaging.ChangeClick += new Paging.PageChange(EmpTaskAssignPaging_FirstClick);
            EmpTaskAssignPaging.ShowRowsClick += new Paging.ShowRowsChange(EmpTaskAssignPaging_ShowRowsClick);
            EmpTaskAssignPaging.CurrentPage = 1;
            #endregion AllTypes

            #region Scheduled
            EmpSchPaging.FirstClick += new Paging.PageFirst(EmpSchPaging_FirstClick);
            EmpSchPaging.PreviousClick += new Paging.PagePrevious(EmpSchPaging_FirstClick);
            EmpSchPaging.NextClick += new Paging.PageNext(EmpSchPaging_FirstClick);
            EmpSchPaging.LastClick += new Paging.PageLast(EmpSchPaging_FirstClick);
            EmpSchPaging.ChangeClick += new Paging.PageChange(EmpSchPaging_FirstClick);
            EmpSchPaging.ShowRowsClick += new Paging.ShowRowsChange(EmpSchPaging_ShowRowsClick);
            EmpSchPaging.CurrentPage = 1;
            #endregion Scheduled
        }
        #region EmpTaskAll
        void EmpTaskAssignPaging_ShowRowsClick(object sender, EventArgs e)
        {
            EmpTaskAssignPaging.CurrentPage = 1;
            BindPager();
        }
        void EmpTaskAssignPaging_FirstClick(object sender, EventArgs e)
        {
            BindPager();
        }
        #endregion EmpTaskAll
        #region EmpSch
        void EmpSchPaging_ShowRowsClick(object sender, EventArgs e)
        {
            EmpSchPaging.CurrentPage = 1;
            BindPager();
        }
        void EmpSchPaging_FirstClick(object sender, EventArgs e)
        {
            BindPager();
        }
        #endregion EmpSch
        void BindPager()
        {
            if (trgrdToDolist.Visible == true)
            {
                objHrCommon.PageSize = EmpTaskAssignPaging.CurrentPage;
                objHrCommon.CurrentPage = EmpTaskAssignPaging.ShowRows;
                TaskAssignedEmployees(objHrCommon);
            }
            else if (trgrdSch.Visible == true)
            {
                objHrCommon.PageSize = EmpSchPaging.CurrentPage;
                objHrCommon.CurrentPage = EmpSchPaging.ShowRows;
                GetScheduledDetails(objHrCommon);
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {

           
            Roleids = Convert.ToInt32(Session["RoleId"].ToString());
            companyid = Convert.ToInt32(Session["CompanyID"]);
            userid =  Convert.ToInt32(Session["UserId"]);
           
            CheckedChanged();
            if (!IsPostBack)
            {
                GetParentMenuId();
                FillDepts();
                FillWS();
                BindPager();
                Ajax.Utility.RegisterTypeForAjax(typeof(TaskAssignment));
                txtStartDate.Text = DateTime.Now.ToString(ConfigurationManager.AppSettings["DateFormat"]);
                txtDueDate.Text = DateTime.Now.ToString(ConfigurationManager.AppSettings["DateFormat"]);
                int EmpID =  Convert.ToInt32(Session["UserId"]);
                
                BindEmpList();
                if (Request.QueryString.Count > 0)
                {

                    int Id = int.Parse(Request.QueryString["key"].ToString());
                    if (Id == 1)
                    {
                        tblTodo.Visible = true;
                        tblviewTodo.Visible = false;
                        tblTaskHistory.Visible = false;


                    }

                }
                else
                {
                    tblTodo.Visible = false;
                    tblviewTodo.Visible = true;
                    tblTaskHistory.Visible = false;
                   
                }

                ViewState["ToDoID"] = 0;
                int ToDoID = Convert.ToInt32(ViewState["ToDoID"]);


                btnSave.Visible = btnCancel.Visible = true;

                btnCancel.Enabled = true;

                ViewState["EmpID"] = 0;
                BindPager();
               
            }
        }
        public void TaskAssignedEmployees(HRCommon objHrCommon)
        {
            try
            {
                objHrCommon.PageSize = EmpTaskAssignPaging.ShowRows;
                objHrCommon.CurrentPage = EmpTaskAssignPaging.CurrentPage;
                
                string Status = null;
                if (ddlEmpTaskStatus.SelectedValue != "0")
                {
                    Status = ddlEmpTaskStatus.SelectedItem.Text;
                    if (Status == "Opend/Scheduled")
                        Status = "Scheduled";
                    else if (Status == "Closed")
                        Status = "Completed";
                }
                int? WSID = null;
                int? DeptID = null;
                int? EmpID = null;
                if (ddlAllWS.SelectedValue != "0")
                    WSID = Convert.ToInt32(ddlAllWS.SelectedValue);
                if (ddlAllDept.SelectedValue != "0")
                    DeptID = Convert.ToInt32(ddlAllDept.SelectedValue);
                if (txtAllEmpID.Text != "")
                    EmpID = Convert.ToInt32(txtAllEmpID.Text);
                string EmpName = txtAllEmpName.Text;
                DataSet ds = AttendanceDAC.HR_GetToDolistByAssignerStatus(objHrCommon,  Convert.ToInt32(Session["UserId"]), Status, WSID, DeptID, EmpID, EmpName);
                if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0)
                {
                    gvTodoList.DataSource = ds;
                    EmpTaskAssignPaging.Bind(objHrCommon.CurrentPage, objHrCommon.TotalPages, objHrCommon.NoofRecords, objHrCommon.PageSize);
                }
                else
                {
                    gvTodoList.EmptyDataText = "No Records found.!";
                    EmpTaskAssignPaging.Visible = false;
                }
                gvTodoList.DataBind();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public void BindEmpList()
        {
            
            string EmpName = string.Empty;
            DataSet ds = AttendanceDAC.HR_GetEmployeeBySearch(EmpName);
            ddlEmp.DataSource = ds.Tables[1];
            ddlEmp.DataTextField = "name";
            ddlEmp.DataValueField = "EmpID";
            ddlEmp.DataBind();
            ddlEmp.Items.Insert(0, new ListItem("---SELECT---", "0", true));

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

                gvTodoList.Columns[7].Visible = Convert.ToBoolean(ds.Tables[0].Rows[0]["Editable"].ToString());
                gvTodoList.Columns[8].Visible = Convert.ToBoolean(ds.Tables[0].Rows[0]["Editable"].ToString());
               
                btnSave.Enabled = (bool)ds.Tables[0].Rows[0]["Editable"];
               
            }
            return MenuId;
        }

        
        
        public string GetWorkSite(string WSid)
        {
            string retVal = "";
            try
            {
                
              DataSet  ds = AttendanceDAC.GetWorkSite(0, '1', Convert.ToInt32(Session["CompanyID"]));
                retVal = ds.Tables[0].Select("Site_ID='" + WSid + "'")[0]["Site_Name"].ToString();
            }
            catch { }
            return retVal;
        }
        public void rbSelect_CheckedChanged(Object sender, EventArgs e)
        {

            tblEmpResult.Visible = false;

            tblTodo.Visible = true;
            tblviewTodo.Visible = false;
            RadioButton chk = (RadioButton)sender;
            GridViewRow gvr = (GridViewRow)(((Control)sender).NamingContainer);
            Label txt = (Label)gvr.Cells[1].FindControl("lblempid");
            if (chk.Checked == true)
            {
                int EmpID = Convert.ToInt32(txt.Text);
                ViewState["EmpID"] = EmpID;

                DataSet ds = AttendanceDAC.HR_EmpMobile(EmpID);
                txtEmp.Text = ds.Tables[0].Rows[0]["name"].ToString();
                btnSave.Visible = btnCancel.Visible = true;
                btnSave.Enabled = Convert.ToBoolean(ViewState["Editable"]);
            }
        }
       

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            double EmpID; tblTodo.Visible = true;

            tblviewTodo.Visible = false;
            try
            {

                EmpID = 0;

                int SiteID = 0;
                double DeptID = 0;
                string FName = txtEmp.Text;



                DataSet ds = AttendanceDAC.SearchEmpList(SiteID, Convert.ToInt32(DeptID), FName, Convert.ToInt32(EmpID));
                if (ds != null && ds.Tables.Count != 0)
                {
                    ddlEmp.DataSource = ds;
                    ddlEmp.DataTextField = "name";
                    ddlEmp.DataValueField = "EmpID";
                    ddlEmp.DataBind();
                    ddlEmp.Items.Insert(0, new ListItem("---SELECT---", "0", true));
                }

            }
            catch (Exception ex)
            {

            }
        }
        public void BindGrid()
        {
            BindPager();
            
        }
        public void refresh()
        {
            txtTask.Text = "";
            txtSubject.Text = "";
            txtStartDate.Text = "";
            txtDueDate.Text = "";
            txtEmp.Text = "";
            ddlPriority.SelectedIndex = 0;

        }
        public void BindHistory()
        {
            int ToDoID = Convert.ToInt32(ViewState["ToDoID"]);

            DataSet ds = AttendanceDAC.HR_GetTaskHistory(ToDoID);
            gvTaskHistory.DataSource = ds;
            gvTaskHistory.DataBind();
            tblTaskHistory.Visible = true;
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            refresh();
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                int ToDoID = Convert.ToInt32(ViewState["ToDoID"].ToString());
                ViewState["EmpID"] = Convert.ToInt32(ddlEmp.SelectedValue);
                int EmpID = Convert.ToInt32(ViewState["EmpID"]);
                if (EmpID == 0)
                {
                    btnSearch_Click(sender, e);
                }
                string Subject = txtSubject.Text;
                DateTime StartDate = CODEUtility.ConvertToDate(txtStartDate.Text.Trim(), DateFormat.DayMonthYear);
                DateTime DueDate = CODEUtility.ConvertToDate(txtDueDate.Text.Trim(), DateFormat.DayMonthYear);
                int Prior = Convert.ToInt32(ddlPriority.SelectedValue);
                
                string Task = txtTask.Text;
                int AssignerEmpID =  Convert.ToInt32(Session["UserId"]);

                DataSet ds = AttendanceDAC.HR_EmpMobile(AssignerEmpID);
                string Status = "Not Started";

                string Complete = "0%";
                string AssignedBy = ds.Tables[0].Rows[0]["name"].ToString();
               
                DateTime? RemindOn = null;
                string RemindTime = string.Empty;
                AttendanceDAC.HR_InsUpToDoList(ToDoID, EmpID, Subject, StartDate, DueDate, Status, Prior, Complete, Task, AssignedBy, AssignerEmpID, RemindOn, RemindTime);
                tblviewTodo.Visible = true;
                tblTodo.Visible = false;
                tblTaskHistory.Visible = false;
                BindPager();
               
            }
            catch (Exception TaskAss)
            {
                AlertMsg.MsgBox(Page, TaskAss.Message.ToString(),AlertMsg.MessageType.Error);
            }
        }
        protected void gvTodoList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "view")
                {
                    tblTodo.Visible = true;
                    tblviewTodo.Visible = false; txtEmp.Visible = false;
                    int EmpID =  Convert.ToInt32(Session["UserId"]);
                    int ToDoID = Convert.ToInt32(e.CommandArgument);
                    ViewState["ToDoID"] = ToDoID;

                    DataSet ds = AttendanceDAC.HR_GetEmpToDoList(ToDoID);
                    txtDueDate.Text = ds.Tables[0].Rows[0]["DueDate"].ToString();
                    txtStartDate.Text = ds.Tables[0].Rows[0]["StartDate"].ToString();
                    txtSubject.Text = ds.Tables[0].Rows[0]["Subject"].ToString();
                    lblSubject.Text = ds.Tables[0].Rows[0]["Subject"].ToString();
                    txtTask.Text = ds.Tables[0].Rows[0]["Task"].ToString();
                    lblTask.Text = ds.Tables[0].Rows[0]["Task"].ToString();
                    txtEmp.Text = ds.Tables[0].Rows[0]["AssignedTo"].ToString();
                    int AssingedTo = Convert.ToInt32(ds.Tables[0].Rows[0]["EmpID"]);
                    ddlEmp.SelectedValue = ds.Tables[0].Rows[0]["EmpID"].ToString();
                    ViewState["EmpID"] = AssingedTo;
                    btnSave.Enabled = true;
                    BindHistory();
                    tblEmpResult.Visible = false;
                   
                }
                if (e.CommandName == "Del")
                {
                    int ToDoID = Convert.ToInt32(e.CommandArgument);
                    AttendanceDAC.HR_DelToDoList(ToDoID);
                    BindGrid();
                    BindHistory();
                }
            }
            catch (Exception DelTaskAss)
            {
                AlertMsg.MsgBox(Page, DelTaskAss.Message.ToString(),AlertMsg.MessageType.Error);
            }
        }

      

        protected void txtEmp_TextChanged(object sender, EventArgs e)
        {
            btnSearch_Click(sender, e);
        }


        #region Workschedule

        #region SupportingMethods

        public void GetScheduledDetails(HRCommon objHrCommon)
        {
            objHrCommon.PageSize = EmpSchPaging.ShowRows;
            objHrCommon.CurrentPage = EmpSchPaging.CurrentPage;
            int? WSID = null;
            if (ddlSecWS.SelectedValue != "0")
                WSID = Convert.ToInt32(ddlSecWS.SelectedValue);
            int? DeptID = null;
            if (ddlSecDept.SelectedValue != "0")
                DeptID = Convert.ToInt32(ddlSecDept.SelectedValue);
            int? EmpID = null;
            if (txtSchEmpID.Text != "")
                EmpID = Convert.ToInt32(txtSchEmpID.Text);
            string EmpName = txtSchEmpName.Text;
            
          DataSet  ds = AttendanceDAC.GetScheduledRplyDetails(objHrCommon, WSID, DeptID, EmpID, EmpName);
            if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0)
            {
                grdSch.DataSource = ds;
                EmpSchPaging.Bind(objHrCommon.CurrentPage, objHrCommon.TotalPages, objHrCommon.NoofRecords, objHrCommon.PageSize);
            }
            else
            {
                grdSch.EmptyDataText = "No Records found.!";
                EmpSchPaging.Visible = false;
            }
            grdSch.DataBind();

        }
        public void CheckedChanged()
        {
            if (chkSche.Checked)
            {
                trgrdSch.Visible = true;
                trgrdToDolist.Visible = false;
                trgrdToDolistPaging.Visible = false;
                trgrdSchPaging.Visible = true;
                trAll.Visible = false;
                trSchedule.Visible = true;
                BindPager();
            }
            else
            {
                trgrdSch.Visible = false;
                trgrdToDolist.Visible = true;
                trgrdToDolistPaging.Visible = true;
                trgrdSchPaging.Visible = false;
                trAll.Visible = true;
                trSchedule.Visible = false;

                BindPager();

            }
        }
        //Rijwan:23-03-2016
        protected void Worksidechangewithdep(object sender, EventArgs e)
        {
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@Search", txtSearchWorksite.Text);
            param[1] = new SqlParameter("@CompanyID", Convert.ToInt32(Session["CompanyID"]));
            param[2] = new SqlParameter("@EmpID",  Convert.ToInt32(Session["UserId"]));
            param[3] = new SqlParameter("@Role", Convert.ToInt32(Session["RoleId"]));
           
            FIllObject.FillDropDown(ref ddlAllWS, "HR_GoogleSearcGetWorksiteByEmpID", param);
            ListItem itmSelected = ddlAllWS.Items.FindByText(txtSearchWorksite.Text);
            if (itmSelected != null)
            {
                ddlAllWS.SelectedItem.Selected = false;
                itmSelected.Selected = true;
           
            }
            txtSearchdept.Text = "";
        }
        protected void GetDepartmentSearch(object sender, EventArgs e)
        {
            //int Dept = 0;
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@Search", txtSearchdept.Text);
            param[1] = new SqlParameter("@DeptID", Dept);
            FIllObject.FillDropDown(ref ddlAllDept, "HR_GoogleSearc_GetDepartments", param);
            ListItem itmSelected = ddlAllDept.Items.FindByText(txtSearchdept.Text);
            if (itmSelected != null)
            {
                ddlAllDept.SelectedItem.Selected = false;
                itmSelected.Selected = true;
            }
            if (txtSearchdept.Text != "") { ddlAllDept.SelectedIndex = 1; }
            
        }
        public void FillWS()
        {
            try
            {

                DataSet ds = objRights.GetWorkSiteByEmpID( Convert.ToInt32(Session["UserId"]), Convert.ToInt32(Session["CompanyID"]), Convert.ToInt32(Session["RoleId"]));
                ViewState["WorkSites"] = ds;
                if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ddlAllWS.DataSource = ds.Tables[0];
                    ddlAllWS.DataTextField = "Site_Name";
                    ddlAllWS.DataValueField = "Site_ID";
                    ddlAllWS.DataBind();

                    ddlSecWS.DataSource = ds.Tables[0];
                    ddlSecWS.DataTextField = "Site_Name";
                    ddlSecWS.DataValueField = "Site_ID";
                    ddlSecWS.DataBind();

                }
                ddlAllWS.Items.Insert(0, new ListItem("---ALL---", "0"));
                ddlSecWS.Items.Insert(0, new ListItem("---ALL---", "0"));


            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public void FillDepts()
        {

            DataSet ds = objRights.GetDepartments(0);
            ddlAllDept.DataSource = ds.Tables[0];
            ddlAllDept.DataTextField = "Deptname";
            ddlAllDept.DataValueField = "DepartmentUId";
            ddlAllDept.DataBind();
            ddlAllDept.Items.Insert(0, new ListItem("---All---", "0", true));

            ddlSecDept.DataSource = ds.Tables[0];
            ddlSecDept.DataTextField = "Deptname";
            ddlSecDept.DataValueField = "DepartmentUId";
            ddlSecDept.DataBind();
            ddlSecDept.Items.Insert(0, new ListItem("---All---", "0", true));
        }

        #endregion SupportingMethods

        #region Events

        protected void chkSche_CheckedChanged(object sender, EventArgs e)
        {
            CheckedChanged();
        }
        protected void ddlEmpTaskStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindPager();
        }

        #endregion Events

        #endregion Workschedule
        protected void btnSechSubmit_Click(object sender, EventArgs e)
        {
            BindPager();
        }

        //Added by Rijwan:22-03-2016
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute]
        public static string[] GetCompletionList(string prefixText, int count, string contextKey)
        {
            DataSet ds = AttendanceDAC.Googlesearch_GetWorkSiteByEmpID(prefixText,companyid,userid,Roleids);
            return ConvertStingArray(ds);
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute]
        public static string[] GetCompletionListdept(string prefixText, int count, string contextKey)
        {
            DataSet ds = AttendanceDAC.GoogleSerch_TaskAssignment_GetDaprtment(prefixText, Dept);
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
        protected void btnAllSubmit_Click(object sender, EventArgs e)
        {

        }
    }
}
