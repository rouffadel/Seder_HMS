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
using AECLOGIC.HMS.BLL;
namespace AECLOGIC.ERP.HMS
{
    public partial class Tasks : AECLOGIC.ERP.COMMON.WebFormMaster
    {
        int mid = 0;
        bool viewall;
        string menuname;
        string menuid;
        AttendanceDAC objTasks = new AttendanceDAC();
        HRCommon objHrCommon = new HRCommon();
         
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
                if (Session["Type"].ToString() == "1" || Session["Type"].ToString() == "5" || Session["Type"].ToString() == "6")
                {
                    btnsubmit.Visible = true;

                }
                else
                {
                    btnsubmit.Visible = false;
                }
                mainview.ActiveViewIndex = 1;
                BindTasks();
                lbldate.Text = DateTime.Now.GetDateTimeFormats()[10];
            }
            btnsubmit.Attributes.Add("onclick", "javascript:return validatesave();");
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
                 
                gvTasks.Columns[3].Visible = Convert.ToBoolean(ds.Tables[0].Rows[0]["Editable"].ToString());
                gvTasks.Columns[4].Visible = Convert.ToBoolean(ds.Tables[0].Rows[0]["Editable"].ToString());
                
            }
            return MenuId;
        }
        protected void btnsubmit_Click(object sender, EventArgs e)
        {
            if (ViewState["TaskID"] != null && ViewState["TaskID"].ToString() != "0")
            {
                objHrCommon.TaskID = Convert.ToInt32(ViewState["TaskID"].ToString());
            }
            objHrCommon.TaskName = txtTaskName.Text.Trim();
            objHrCommon.DueDate = CODEUtility.ConvertToDate(txtDueDate.Text.Trim(), DateFormat.DayMonthYear);
            objHrCommon.EmpID = Convert.ToInt32(Request.QueryString["EmpID"].ToString());
            objHrCommon.UserID = Convert.ToInt32( Convert.ToInt32(Session["UserId"]).ToString());
            int i = objTasks.InsUpdTasks(objHrCommon);
            if (i == 1)
            {
                AlertMsg.MsgBox(Page, "Responsibilities Assigned Successfully");
            }
            else
            {
                AlertMsg.MsgBox(Page, "Responsibilities Edit Successfully");
            }
            BindTasks();
        }
        protected void gvTasks_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Edt")
            {
                if (Session["Type"].ToString() == "1" || Session["Type"].ToString() == "5" || Session["Type"].ToString() == "6")
                {
                    int TaskID = Convert.ToInt32(e.CommandArgument);
                    ViewState["TaskID"] = TaskID;
                    BindTaskDetails(TaskID);
                    btnsubmit.Text = "Update";
                    mainview.ActiveViewIndex = 0;
                }
                else
                {
                    AlertMsg.MsgBox(Page, "You have no  rights ");
                }





            }
            if (e.CommandName == "Del")
            {

                if (Session["Type"].ToString() == "1" || Session["Type"].ToString() == "5" || Session["Type"].ToString() == "6")
                {
                    int TaskID = Convert.ToInt32(e.CommandArgument);
                    objTasks.DeleteTask(TaskID);
                    AlertMsg.MsgBox(Page, "Deleted Successfully");
                    BindTasks();
                }
                else
                {
                    AlertMsg.MsgBox(Page, "You have no  rights ");
                }
            }
        }
        public void BindTasks()
        {
            mainview.ActiveViewIndex = 1;
            int EmpID = Convert.ToInt32(Request.QueryString["EmpID"].ToString());
           DataSet ds = objTasks.GetTasksList(EmpID);
            if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0)
            {
                gvTasks.DataSource = ds;
            }
            gvTasks.DataBind();

        }
        public void BindTaskDetails(int TaskID)
        {
            DataSet ds = objTasks.GetTasksDetails(TaskID);
            if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0)
            {
                txtTaskName.Text = ds.Tables[0].Rows[0]["TaskName"].ToString();
                txtDueDate.Text = ds.Tables[0].Rows[0]["DueDate"].ToString();
                if (ds.Tables[0].Rows[0]["Status"].ToString() == "1")
                {
                    chkStatus.Checked = true;
                }
                else
                {
                    chkStatus.Checked = false;
                }
            }

        }

        protected void lnkRes_Click(object sender, EventArgs e)
        {
            int EmpID = Convert.ToInt32(Request.QueryString["EmpID"].ToString());
            Response.Redirect("Responsibility.aspx?EmpID=" + EmpID);
        }
        protected void lnkTodoList_Click(object sender, EventArgs e)
        {
            int EmpID = Convert.ToInt32(Request.QueryString["EmpID"].ToString());
            Response.Redirect("TodoList.aspx?EmpID=" + EmpID);
        }
        protected void lnknew_Click(object sender, EventArgs e)
        {
            if (Session["Type"].ToString() == "1" || Session["Type"].ToString() == "5" || Session["Type"].ToString() == "6")
            {
                mainview.ActiveViewIndex = 0;
            }
            else
            {
                AlertMsg.MsgBox(Page, "You have no  rights ");
            }
        }

    }
}