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
    public partial class TodoList : AECLOGIC.ERP.COMMON.WebFormMaster
    {
        int mid = 0;
        bool viewall;
        string menuname;
        string menuid;
        AttendanceDAC objTodoList = new AttendanceDAC();
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
                mainview.ActiveViewIndex = 1;
                BindTodoList();

            }
            btnsubmit.Attributes.Add("onclick", "javascript:return validatesave();");
        }
      
        protected void lnkRes_Click(object sender, EventArgs e)
        {
            int EmpID = Convert.ToInt32(Request.QueryString["EmpID"].ToString());
            Response.Redirect("Responsibility.aspx?EmpID=" + EmpID);
        }
        protected void lnkTasks_Click(object sender, EventArgs e)
        {
            int EmpID = Convert.ToInt32(Request.QueryString["EmpID"].ToString());
            Response.Redirect("Tasks.aspx?EmpID=" + EmpID);
        }
        protected void btnsubmit_Click(object sender, EventArgs e)
        {
            if (ViewState["TodoListID"] != null && ViewState["TodoListID"].ToString() != "0")
            {
                objHrCommon.ListID = Convert.ToInt32(ViewState["TodoListID"].ToString());
            }
            objHrCommon.ListName = txtListItem.Text.Trim();
            objHrCommon.Authority = txtAuthority.Text.Trim();
            objHrCommon.EmpID = Convert.ToInt32( Convert.ToInt32(Session["UserId"]).ToString());

            int i = objTodoList.InsUpdTodoListMasetr(objHrCommon);
            if (i == 1)
            {
                AlertMsg.MsgBox(Page, "TodoList Assigned Successfully");
            }
            else
            {
                AlertMsg.MsgBox(Page, "TodoList Edit Successfully");
            }
            BindTodoList();
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
        public void BindTodoList()
        {
            mainview.ActiveViewIndex = 1;
            int SiteID = Convert.ToInt32(Session["Site"].ToString());
          DataSet  ds = objTodoList.GetCheckList(SiteID);
            if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0)
            {
                gvToddoList.DataSource = ds;
            }
            gvToddoList.DataBind();

        }
        public void BindTodoListDetails(int TodoListID)
        {
            DataSet ds = objTodoList.GetTodoListDetails(TodoListID);
            if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0)
            {
                txtListItem.Text = ds.Tables[0].Rows[0]["ListItem"].ToString();
                txtAuthority.Text = ds.Tables[0].Rows[0]["Authority"].ToString();

            }

        }
        protected void gvToddoList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Edt")
            {
                if (Session["Type"].ToString() == "1" || Session["Type"].ToString() == "5" || Session["Type"].ToString() == "6")
                {
                    int TodoListID = Convert.ToInt32(e.CommandArgument);
                    ViewState["TodoListID"] = TodoListID;
                    BindTodoListDetails(TodoListID);
                    btnsubmit.Text = "Update";
                    mainview.ActiveViewIndex = 0;
                }
                else
                {
                    AlertMsg.MsgBox(Page, "You have no edit rights ");
                }

            }
            if (e.CommandName == "Del")
            {
                if (Session["Type"].ToString() == "1" || Session["Type"].ToString() == "5" || Session["Type"].ToString() == "6")
                {
                    int TodoListID = Convert.ToInt32(e.CommandArgument);

                    objTodoList.DeleteTodoList(TodoListID);
                    AlertMsg.MsgBox(Page, "Deleted Successfully");

                    BindTodoList();
                }
                else
                {
                    AlertMsg.MsgBox(Page, "You have no delete rights ");
                }
            }
        }
    }
}
