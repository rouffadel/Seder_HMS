using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AECLOGIC.HMS.BLL;
using System.Data;
using System.Configuration;
using Aeclogic.Common.DAL;

namespace AECLOGIC.ERP.HMS
{
    public partial class EmpToDoList : AECLOGIC.ERP.COMMON.WebFormMaster
    {
        int mid = 0; bool viewall; string menuname; string menuid;
        protected override void OnInit(EventArgs e)
        {
            ModuleID = 1;
            base.OnInit(e);
        }
        protected void Page_Load(object sender, EventArgs e)
        {

            pnlEmpToDoAdd.Visible = true;
            if (!IsPostBack)
            {
                #region Schedule
                 GetParentMenuId();
                TRMonths();
                TRYears();
                TRDays();
                TRHours();
                TRMinSec();
                #endregion Schedule
                BindEmpList();
                chkReminder.Checked = false;
                txtReminder.Enabled = ddlReminder.Enabled = false;
                txtReminder.Text = DateTime.Now.ToString(ConfigurationManager.AppSettings["DateFormat"]);
                txtStartDate.Text = DateTime.Now.ToString(ConfigurationManager.AppSettings["DateFormat"]);
                txtDueDate.Text = DateTime.Now.ToString(ConfigurationManager.AppSettings["DateFormat"]);
                ViewState["ToDoID"] = 0;
                int ToDoID = Convert.ToInt32(ViewState["ToDoID"]);
                int EmpID =  Convert.ToInt32(Session["UserId"]);
                 
              DataSet  ds = Common.HR_GetToDolistCount(EmpID, Convert.ToInt32(Session["CompanyID"]));
                int xx = Convert.ToInt32(ds.Tables[0].Rows[0][0]);
                if (Request.QueryString.Count > 0)
                {
                    //pnlEmpToDoAdd.Visible = true;
                    tblSchedule.Visible = false;
                    int Id = int.Parse(Request.QueryString["key"].ToString());
                    if (Id == 1)
                    {
                        //pnlEmpToDoAdd.Visible = true;
                        lblComplete.Visible = false;
                        ddlComplete.Visible = false;
                        tblTodo.Visible = true;
                        tblviewTodo.Visible = false;
                        lblTaskReport.Visible = false;
                        txtReport.Visible = false;
                        refresh();
                        tblTaskHistory.Visible = false;
                        btnBack.Visible = false;
                    }
                    if (Id == 2)
                    {
                        //pnlEmpToDoAdd.Visible = true;
                        txtStartDate.Text = DateTime.Now.ToString(ConfigurationManager.AppSettings["DateFormat"]);
                        txtDueDate.Text = DateTime.Now.ToString(ConfigurationManager.AppSettings["DateFormat"]);
                        tblTodo.Visible = false;
                        tblviewTodo.Visible = true;
                        tblTaskHistory.Visible = false;
                        BindGrid();
                        if (xx == 0)
                        {
                            Response.Redirect("EmpToDoList.aspx?key=1");
                        }
                        if (Convert.ToInt32(Request.QueryString["Id"]) != 0)
                        {
                            int ToDoId = int.Parse(Request.QueryString["Id"].ToString());
                            chkReminder.Checked = txtReminder.Enabled = ddlReminder.Enabled = false;
                            tblTodo.Visible = true;
                            tblviewTodo.Visible = false;
                            lblComplete.Visible = true;
                            ddlComplete.Visible = true;
                            ViewState["ToDoID"] = ToDoId;
                            DataSet ds1 = AttendanceDAC.HR_GetEmpToDoList(ToDoId);
                            txtDueDate.Text = ds1.Tables[0].Rows[0]["DueDate"].ToString();
                            txtStartDate.Text = ds1.Tables[0].Rows[0]["StartDate"].ToString();
                            txtSubject.Text = ds1.Tables[0].Rows[0]["Subject"].ToString();
                            txtTask.Text = ds1.Tables[0].Rows[0]["Task"].ToString();
                            txtReport.Text = "";
                            string Compl = ds1.Tables[0].Rows[0]["Complete"].ToString();
                            string Prior = ds1.Tables[0].Rows[0]["Priority"].ToString();
                            string Statu = ds1.Tables[0].Rows[0]["Status"].ToString();
                            txtAssigned.Text = ds1.Tables[0].Rows[0]["AssignedBy"].ToString();
                            if (Compl == "0%") { ddlComplete.SelectedIndex = 0; } if (Compl == "25%") { ddlComplete.SelectedIndex = 1; } if (Compl == "50%") { ddlComplete.SelectedIndex = 2; }
                            if (Compl == "75%") { ddlComplete.SelectedIndex = 3; } if (Compl == "100%") { ddlComplete.SelectedIndex = 4; }
                            if (Prior == "Normal") { ddlPriority.SelectedIndex = 0; } if (Prior == "Low") { ddlPriority.SelectedIndex = 1; } if (Prior == "High") { ddlPriority.SelectedIndex = 2; }
                            if (Prior == "Urgent") { ddlPriority.SelectedIndex = 3; lblPriority.ForeColor = System.Drawing.Color.Orange; } if (Prior == "Very Urgent") { ddlPriority.SelectedIndex = 4; lblPriority.ForeColor = System.Drawing.Color.Red; }
                            if (Statu == "Not Started") { ddlStatus.SelectedIndex = 0; } if (Statu == "Completed") { ddlStatus.SelectedIndex = 1; } if (Statu == "In Progress") { ddlStatus.SelectedIndex = 2; }
                            BindHistory(ToDoId);
                            lblTaskReport.Visible = txtReport.Visible = true;
                            btnBack.Visible = true;
                            txtStartDate.Enabled = txtDueDate.Enabled = false;
                            txtSubject.ReadOnly = txtAssigned.ReadOnly = txtDueDate.ReadOnly = txtStartDate.ReadOnly = txtTask.ReadOnly = true;
                            ddlEmp.SelectedValue = ds1.Tables[0].Rows[0]["AssignerEmpID"].ToString() == "" ? "0" : ds1.Tables[0].Rows[0]["AssignerEmpID"].ToString();
                            lblStartDate.Text = ds1.Tables[0].Rows[0]["StartDate"].ToString();
                            lblDueDate.Text = ds1.Tables[0].Rows[0]["DueDate"].ToString();
                            lblAssignedBy.Text = ds1.Tables[0].Rows[0]["AssignedBy"].ToString();
                            lblPriority.Text = ds1.Tables[0].Rows[0]["Priority"].ToString();
                            lblSubject.Text = ds1.Tables[0].Rows[0]["Subject"].ToString();
                            lblTask.Text = ds1.Tables[0].Rows[0]["Task"].ToString();
                            lblTask.Visible = lblSubject.Visible = lblAssignedBy.Visible = lblComplete.Visible = lblDueDate.Visible = lblStartDate.Visible = lblPriority.Visible = true;
                            txtDueDate.Visible = txtStartDate.Visible = ddlPriority.Visible = txtTask.Visible = txtSubject.Visible = false;
                            ddlEmp.Visible = false;
                        }
                    }

                }
                else
                {
                    tblSchedule.Visible = false;
                    pnlEmpToDoAdd.Visible = false;
                    tblTodo.Visible = false;
                    tblviewTodo.Visible = true;
                    tblTaskHistory.Visible = false;
                    BindGrid();
                    if (xx == 0)
                    {
                        Response.Redirect("EmpToDoList.aspx?key=1");
                    }
                }
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

                gvTodoList.Columns[7].Visible = Convert.ToBoolean(ds.Tables[0].Rows[0]["Editable"].ToString());
                gvTodoList.Columns[8].Visible = Convert.ToBoolean(ds.Tables[0].Rows[0]["Editable"].ToString());
               
                btnSave.Enabled = (bool)ds.Tables[0].Rows[0]["Editable"];
                ViewState["Editable"] = (bool)ds.Tables[0].Rows[0]["Editable"];
              
            }
            return MenuId;
        }
        public void BindGrid()
        {
             
            int EmpID =  Convert.ToInt32(Session["UserId"]);
            DataSet ds = Common.HR_GetToDolistCount(EmpID, Convert.ToInt32(Session["CompanyID"]));
            gvTodoList.DataSource = ds.Tables[1];
            gvTodoList.DataBind();
        }
        public void BindHistory()
        {
            int ToDoID = Convert.ToInt32(ViewState["ToDoID"]);

            DataSet ds = AttendanceDAC.HR_GetTaskHistory(ToDoID);
            gvTaskHistory.DataSource = ds;
            gvTaskHistory.DataBind();
            tblTaskHistory.Visible = true;
        }
        public void BindHistory(int ToDoID)
        {

            DataSet ds = AttendanceDAC.HR_GetTaskHistory(ToDoID);
            gvTaskHistory.DataSource = ds;
            gvTaskHistory.DataBind();
            tblTaskHistory.Visible = true;
        }
        public void refresh()
        {
            txtTask.Text = "";
            txtSubject.Text = "";
            txtStartDate.Text = DateTime.Now.ToString(ConfigurationManager.AppSettings["DateFormat"]);
            txtDueDate.Text = DateTime.Now.ToString(ConfigurationManager.AppSettings["DateFormat"]);
            ddlComplete.SelectedIndex = 0;
            ddlPriority.SelectedIndex = 0;
            ddlStatus.SelectedIndex = 0;
            txtAssigned.Text = "";
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
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            refresh();
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                int ToDoID = Convert.ToInt32(ViewState["ToDoID"].ToString());
                if (ToDoID == 0)
                {
                    int EmpID =  Convert.ToInt32(Session["UserId"]);
                    int AssignerEmpID = Convert.ToInt32(ddlEmp.SelectedValue);
                    string Subject = txtSubject.Text;
                    DateTime StartDate = CODEUtility.ConvertToDate(txtStartDate.Text.Trim(), DateFormat.DayMonthYear);
                    DateTime DueDate = CODEUtility.ConvertToDate(txtDueDate.Text.Trim(), DateFormat.DayMonthYear);//Convert.ToDateTime(txtStartDate.Text);//Convert.ToDateTime(txtDueDate.Text);
                    string Status = ddlStatus.SelectedItem.Text;
                    int Priority = Convert.ToInt32(ddlPriority.SelectedValue);
                    string Complete = ddlComplete.SelectedItem.Text;
                    string Task = txtTask.Text;
                    // string AssignedBy = txtAssigned.Text;
                    string AssignedBy = ddlEmp.SelectedItem.Text;
                    DateTime RemindOn = CODEUtility.ConvertToDate(txtReminder.Text.Trim(), DateFormat.DayMonthYear);
                    string RemindTime = ddlReminder.SelectedItem.Text;
                    AttendanceDAC.HR_InsUpToDoList(ToDoID, EmpID, Subject, StartDate, DueDate, Status, Priority, Complete, Task, AssignedBy, AssignerEmpID, RemindOn, RemindTime);
                    tblviewTodo.Visible = true;
                    tblTodo.Visible = false;
                   
                    DataSet ds1 = Common.HR_GetToDolistCount(EmpID, Convert.ToInt32(Session["CompanyID"]));
                    gvTodoList.DataSource = ds1.Tables[1];
                    gvTodoList.DataBind();
                    int TaskNo = 0;
                    DateTime ReportedOn = DateTime.Now;
                    string Report = txtReport.Text;
                    if (Report != "" || Report != string.Empty)
                    {

                        AttendanceDAC.HR_InsTaskHistory(TaskNo, ToDoID, Subject, Status, ReportedOn, AssignedBy, Complete, Report);
                        BindHistory();
                    }
                    tblTaskHistory.Visible = false;
                    btnBack.Visible = false;
                }
                else
                {
                    int EmpID =  Convert.ToInt32(Session["UserId"]);
                    int AssignerEmpID = Convert.ToInt32(ddlEmp.SelectedValue);
                    string Subject = txtSubject.Text;
                    DateTime StartDate = CODEUtility.ConvertToDate(txtStartDate.Text.Trim(), DateFormat.DayMonthYear);
                    DateTime DueDate = CODEUtility.ConvertToDate(txtDueDate.Text.Trim(), DateFormat.DayMonthYear);//Convert.ToDateTime(txtStartDate.Text);//Convert.ToDateTime(txtDueDate.Text);
                    string Status = ddlStatus.SelectedItem.Text;
                    int Priority = Convert.ToInt32(ddlPriority.SelectedValue);
                    string Complete = ddlComplete.SelectedItem.Text;
                    string Task = txtTask.Text;
                    string AssignedBy = ddlEmp.SelectedItem.Text;
                    DateTime RemindOn = CODEUtility.ConvertToDate(txtReminder.Text.Trim(), DateFormat.DayMonthYear);
                    string RemindTime = ddlReminder.SelectedItem.Text;
                    AttendanceDAC.HR_InsUpToDoList(ToDoID, EmpID, Subject, StartDate, DueDate, Status, Priority, Complete, Task, AssignedBy, AssignerEmpID, RemindOn, RemindTime);
                    tblviewTodo.Visible = true;
                    tblTodo.Visible = false;
                    
                    DataSet ds1 = Common.HR_GetToDolistCount(EmpID, Convert.ToInt32(Session["CompanyID"]));
                    gvTodoList.DataSource = ds1.Tables[1];
                    gvTodoList.DataBind();
                    int TaskNo = 0;
                    DateTime ReportedOn = DateTime.Now;
                    string Report = txtReport.Text;
                    if (Report != "" || Report != string.Empty)
                    {
                        AttendanceDAC.HR_InsTaskHistory(TaskNo, ToDoID, Subject, Status, ReportedOn, AssignedBy, Complete, Report);
                        BindHistory();
                    }
                    tblTaskHistory.Visible = false;
                    btnBack.Visible = false;

                }
                if (Request.QueryString.Count > 0)
                {
                    if (Convert.ToInt32(Request.QueryString["Id"]) != 0)
                    {
                        Response.Redirect("EmpToDoList.aspx");
                    }
                }
                pnlEmpToDoAdd.Visible = false;

            }
            catch (Exception EmpToDoLst)
            {
                AlertMsg.MsgBox(Page, EmpToDoLst.Message.ToString(),AlertMsg.MessageType.Error);
            }
        }
        protected void gvTodoList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                #region Schedule
                if (e.CommandName == "Schedule")
                {
                    tblSchedule.Visible = true;
                    pnlEmpToDoAdd.Visible = false;
                    tblviewTodo.Visible = false;
                    int ToDoID = Convert.ToInt32(e.CommandArgument);
                    ViewState["ToDoID"] = ToDoID;
                    DataSet dsSch = AttendanceDAC.HR_GetToDolistDetailsByToDOID(ToDoID);
                    if (dsSch != null && dsSch.Tables.Count > 0 && dsSch.Tables[0].Rows.Count > 0)
                    {
                        ddlYears.SelectedItem.Text = dsSch.Tables[0].Rows[0]["Years"].ToString();
                        ddlMnths.SelectedItem.Text = dsSch.Tables[0].Rows[0]["Months"].ToString();
                        ddlDays.SelectedItem.Text = dsSch.Tables[0].Rows[0]["Days"].ToString();
                        ddlHours.SelectedItem.Text = dsSch.Tables[0].Rows[0]["Hours"].ToString();
                        ddlMin.SelectedItem.Text = dsSch.Tables[0].Rows[0]["MIN"].ToString();
                        ddlSec.SelectedItem.Text = dsSch.Tables[0].Rows[0]["Sec"].ToString();
                        txtFrmDate.Text = dsSch.Tables[0].Rows[0]["FrmDate"].ToString();
                        String StartTime = dsSch.Tables[0].Rows[0]["StartTime"].ToString();
                        string[] str = new string[3];
                        str = StartTime.Split(':');
                        string StrtAMPM = str[2];
                        string StrtMin = str[1];
                        string StrtHrs = str[0];
                        ddlStrtHr.SelectedItem.Text = StrtHrs;
                        ddlStrtMin.SelectedItem.Text = StrtMin;
                        ddlStrtAMPM.SelectedItem.Text = StrtAMPM;
                        txtToDate.Text = dsSch.Tables[0].Rows[0]["ToDate"].ToString();
                        string EndTime = dsSch.Tables[0].Rows[0]["EndTime"].ToString();
                        string[] strEnd = new string[3];
                        strEnd = EndTime.Split(':');
                        string EndAMPM = strEnd[2];
                        string EndMin = strEnd[1];
                        string EndHrs = strEnd[0];
                        ddlEndHr.SelectedItem.Text = EndHrs;
                        ddlEndMin.SelectedItem.Text = EndMin;
                        ddlEndAMPM.SelectedItem.Text = EndAMPM;
                        txtReason.Text = dsSch.Tables[0].Rows[0]["Reply"].ToString();
                        btnSubSchedule.Enabled = false;
                        ddlYears.Enabled = false;
                        ddlMnths.Enabled = false;
                        ddlDays.Enabled = false;
                        ddlHours.Enabled = false;
                        ddlMin.Enabled = false;
                        ddlSec.Enabled = false;

                        txtFrmDate.Enabled = false;
                        ddlStrtHr.Enabled = false;
                        ddlStrtMin.Enabled = false;
                        ddlStrtAMPM.Enabled = false;
                        txtToDate.Enabled = false;
                        ddlEndHr.Enabled = false;
                        ddlEndMin.Enabled = false;
                        ddlEndAMPM.Enabled = false;
                        txtReason.Enabled = false;
                    }
                    else
                    {

                        ddlYears.Enabled = true;
                        ddlMnths.Enabled = true;
                        ddlDays.Enabled = true;
                        ddlHours.Enabled = true;
                        ddlMin.Enabled = true;
                        ddlSec.Enabled = true;

                        txtFrmDate.Enabled = true;
                        ddlStrtHr.Enabled = true;
                        ddlStrtMin.Enabled = true;
                        ddlStrtAMPM.Enabled = true;
                        txtToDate.Enabled = true;
                        ddlEndHr.Enabled = true;
                        ddlEndMin.Enabled = true;
                        ddlEndAMPM.Enabled = true;
                        txtReason.Enabled = true;
                        btnSubSchedule.Enabled = true;

                    }


                    DataSet ds = AttendanceDAC.HR_GetToDolistByToDOID(ToDoID);
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        lblSubDetails.Text = ds.Tables[0].Rows[0]["Subject"].ToString();
                        txtTskDetails.Text = ds.Tables[0].Rows[0]["Task"].ToString();
                        lblPriorityDetails.Text = ds.Tables[0].Rows[0]["Priority"].ToString();
                        lblStartDateDetails.Text = ds.Tables[0].Rows[0]["StartDate"].ToString();
                        lblDueDateDetails.Text = ds.Tables[0].Rows[0]["DueDate"].ToString();
                        lblAssignedByDetails.Text = ds.Tables[0].Rows[0]["AssignedBy"].ToString();
                    }


                }
                #endregion Schedule

                #region View
                if (e.CommandName == "view")
                {
                    tblTodo.Visible = true;
                    tblviewTodo.Visible = false;
                    lblComplete.Visible = true;
                    ddlComplete.Visible = true;
                    chkReminder.Checked = txtReminder.Enabled = ddlReminder.Enabled = false;
                    int EmpID =  Convert.ToInt32(Session["UserId"]);
                    int ToDoID = Convert.ToInt32(e.CommandArgument);
                    ViewState["ToDoID"] = ToDoID;

                    DataSet ds = AttendanceDAC.HR_GetEmpToDoList(ToDoID);
                    txtDueDate.Text = ds.Tables[0].Rows[0]["DueDate"].ToString();
                    txtStartDate.Text = ds.Tables[0].Rows[0]["StartDate"].ToString();
                    txtSubject.Text = ds.Tables[0].Rows[0]["Subject"].ToString();
                    txtTask.Text = ds.Tables[0].Rows[0]["Task"].ToString();
                    txtReport.Text = "";
                    lblStartDate.Text = ds.Tables[0].Rows[0]["StartDate"].ToString();
                    lblDueDate.Text = ds.Tables[0].Rows[0]["DueDate"].ToString();
                    lblAssignedBy.Text = ds.Tables[0].Rows[0]["AssignedBy"].ToString();
                    lblPriority.Text = ds.Tables[0].Rows[0]["Priority"].ToString();
                    lblSubject.Text = ds.Tables[0].Rows[0]["Subject"].ToString();
                    lblTask.Text = ds.Tables[0].Rows[0]["Task"].ToString();
                    string Compl = ds.Tables[0].Rows[0]["Complete"].ToString();
                    string Prior = ds.Tables[0].Rows[0]["Priority"].ToString();
                    string Statu = ds.Tables[0].Rows[0]["Status"].ToString();
                    ddlEmp.SelectedValue = ds.Tables[0].Rows[0]["AssignerEmpID"].ToString() == "" ? "0" : ds.Tables[0].Rows[0]["AssignerEmpID"].ToString();
                    txtAssigned.Text = ds.Tables[0].Rows[0]["AssignedBy"].ToString();
                    if (Compl == "0%") { ddlComplete.SelectedIndex = 0; } if (Compl == "25%") { ddlComplete.SelectedIndex = 1; } if (Compl == "50%") { ddlComplete.SelectedIndex = 2; }
                    if (Compl == "75%") { ddlComplete.SelectedIndex = 3; } if (Compl == "100%") { ddlComplete.SelectedIndex = 4; }
                    if (Prior == "Normal") { ddlPriority.SelectedIndex = 0; } if (Prior == "Low") { ddlPriority.SelectedIndex = 1; } if (Prior == "High") { ddlPriority.SelectedIndex = 2; }
                    if (Prior == "Urgent") { ddlPriority.SelectedIndex = 3; lblPriority.ForeColor = System.Drawing.Color.Orange; } if (Prior == "Very Urgent") { ddlPriority.SelectedIndex = 4; lblPriority.ForeColor = System.Drawing.Color.Red; }
                    if (Statu == "Not Started") { ddlStatus.SelectedIndex = 0; } if (Statu == "Completed") { ddlStatus.SelectedIndex = 1; } if (Statu == "In Progress") { ddlStatus.SelectedIndex = 2; }
                    BindHistory();
                    lblTaskReport.Visible = txtReport.Visible = true;
                    btnBack.Visible = false;
                    txtStartDate.Enabled = txtDueDate.Enabled = false;
                    //txtSubject.ReadOnly = txtAssigned.ReadOnly = txtDueDate.ReadOnly = txtStartDate.ReadOnly = txtTask.ReadOnly = true;
                    txtSubject.Visible = txtAssigned.Visible = txtDueDate.Visible = txtStartDate.Visible = txtTask.Visible = false;
                    lblTask.Visible = lblSubject.Visible = lblAssignedBy.Visible = lblComplete.Visible = lblDueDate.Visible = lblStartDate.Visible = lblPriority.Visible = true;
                    ddlPriority.Visible = txtTask.Visible = txtSubject.Visible = false;
                    ddlEmp.Visible = false;
                }
                #endregion View

                #region Delete
                if (e.CommandName == "Del")
                {
                    int ToDoID = Convert.ToInt32(e.CommandArgument);
                    AttendanceDAC.HR_DelToDoList(ToDoID);
                    BindGrid();
                    BindHistory();
                }
                #endregion Delete
            }
            catch (Exception EmpToDolstDel)
            {
                AlertMsg.MsgBox(Page, EmpToDolstDel.Message.ToString(),AlertMsg.MessageType.Error);
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("AdminDefault.aspx");
        }
        protected void chkReminder_CheckedChanged(object sender, EventArgs e)
        {
            if (chkReminder.Checked == true)
            {
                txtReminder.Enabled = ddlReminder.Enabled = true;
            }
            else
            {
                txtReminder.Enabled = ddlReminder.Enabled = false;
            }
        }

        #region EmpWorkSchedule
        public void TRMonths()
        {
            //for (int i = 1; i < DateTime.Now.Year; i++)
            for (int i = 0; i <= 12; i++)
            {
                ddlMnths.Items.Add(i.ToString());
            }
        }
        public void TRYears()
        {
            //for (int i = 1; i < DateTime.Now.Year; i++)
            for (int i = 0; i <= 5; i++)
            {
                ddlYears.Items.Add(i.ToString());
            }
        }
        public void TRDays()
        {
            //for (int i = 1; i < DateTime.Now.Year; i++)
            for (int i = 0; i <= 31; i++)
            {
                ddlDays.Items.Add(i.ToString());
            }
        }
        public void TRHours()
        {
            //for (int i = 1; i < DateTime.Now.Year; i++)
            for (int i = 0; i <= 12; i++)
            {
                ddlHours.Items.Add(i.ToString());
                ddlStrtHr.Items.Add(i.ToString());
                ddlEndHr.Items.Add(i.ToString());



            }
        }
        public void TRMinSec()
        {
            //for (int i = 1; i < DateTime.Now.Year; i++)
            for (int i = 0; i <= 59; i++)
            {
                ddlMin.Items.Add(i.ToString());
                ddlSec.Items.Add(i.ToString());
                ddlStrtMin.Items.Add(i.ToString());
                ddlEndMin.Items.Add(i.ToString());

            }
        }
        protected void btnCancelSchedule_Click(object sender, EventArgs e)
        {
            Response.Redirect("EmpToDoList.aspx");
        }
        protected void btnSubSchedule_Click(object sender, EventArgs e)
        {
            int ToDoID = Convert.ToInt32(ViewState["ToDoID"]);
            int EmpID =  Convert.ToInt32(Session["UserId"]);
            DateTime FrmDate = CODEUtility.ConvertToDate(txtFrmDate.Text, DateFormat.DayMonthYear);
            DateTime ToDate = CODEUtility.ConvertToDate(txtToDate.Text, DateFormat.DayMonthYear);
            int Year = Convert.ToInt32(ddlYears.SelectedValue);
            int Month = Convert.ToInt32(ddlMnths.SelectedValue);
            int Day = Convert.ToInt32(ddlDays.SelectedValue);
            int Hour = Convert.ToInt32(ddlHours.SelectedValue);
            int Min = Convert.ToInt32(ddlMin.SelectedValue);
            int Sec = Convert.ToInt32(ddlSec.SelectedValue);

            string StrtTimeHR = ddlStrtHr.SelectedItem.Text;
            string StrtMin = ddlStrtMin.SelectedItem.Text;
            string StrtAMPM = ddlStrtAMPM.SelectedItem.Text;
            string StartTime = StrtTimeHR + ":" + StrtMin + ":" + StrtAMPM;

            string EndTimeHR = ddlEndHr.SelectedItem.Text;
            string EndMin = ddlEndMin.SelectedItem.Text;
            string EndAMPM = ddlEndAMPM.SelectedItem.Text;
            string EndTime = EndTimeHR + ":" + EndMin + ":" + EndAMPM;

            string Rply = txtReason.Text;
            int OutPut = AttendanceDAC.InsUpdEmpSchedule(ToDoID, EmpID, FrmDate, ToDate, Year, Month, Day, Hour, Min, Sec, StartTime, EndTime, Rply);
            if (OutPut == 1)
            {
                AlertMsg.MsgBox(Page, "Inserted sucessfully.!");
                Response.Redirect("EmpToDoList.aspx");
            }
            else
                AlertMsg.MsgBox(Page, "Already exists.!");
        }
        #endregion EmpWorkSchedule



    }
}
