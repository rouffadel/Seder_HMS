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
    public partial class Responsibility : AECLOGIC.ERP.COMMON.WebFormMaster
    {

        AttendanceDAC objRespon = new AttendanceDAC();
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
                int EmpID = Convert.ToInt32(Request.QueryString["EmpID"].ToString());
                int EmployID =  Convert.ToInt32(Session["UserId"]);
               
                int xx = Convert.ToInt32(Session["Type"].ToString());
                if (Session["Type"].ToString() == "1" || Session["Type"].ToString() == "5" || Session["Type"].ToString() == "6" || EmpID == EmployID || EmployID == 83)
                {
                    btnsubmit.Visible = true;

                }
                else
                {
                    btnsubmit.Visible = false;
                }
                lbldate.Text = DateTime.Now.GetDateTimeFormats()[10];
                BindResponsibilties();


            }
        }

        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            if (Session["Type"].ToString() == "1" || Session["Type"].ToString() == "5" || Session["Type"].ToString() == "6")
            {
                btnsubmit.Text = "Edit";
            }
            else
            {
                AlertMsg.MsgBox(Page, "You have no  rights ");
            }
        }
        protected void btnsubmit_Click(object sender, EventArgs e)
        {
            if (ViewState["ResponID"] != null && ViewState["ResponID"].ToString() != "0")
            {
                objHrCommon.ResponID = Convert.ToInt32(ViewState["ResponID"].ToString());
            }
            objHrCommon.Responsible = txtResponsibility.Text ;
            objHrCommon.EmpID = Convert.ToInt32(Request.QueryString["EmpID"].ToString());
            objHrCommon.UserID = Convert.ToInt32( Convert.ToInt32(Session["UserId"]).ToString());
            objRespon.InsUpdResponsibilities(objHrCommon);
            if (btnsubmit.Text == "Edit")
            {
                AlertMsg.MsgBox(Page, "Done!");
            }
            else
            {
                AlertMsg.MsgBox(Page, "Done!");
            }

            Response.Write("<script language='javascript'> { window.close();}</script>");
            btnsubmit.Attributes.Add("onclick", "window.close();");
        }

        protected void lnkTasks_Click(object sender, EventArgs e)
        {
            int EmpID = Convert.ToInt32(Request.QueryString["EmpID"].ToString());
            Response.Redirect("Tasks.aspx?EmpID=" + EmpID);
        }
        protected void lnkTodoList_Click(object sender, EventArgs e)
        {
            int EmpID = Convert.ToInt32(Request.QueryString["EmpID"].ToString());
            Response.Redirect("TodoList.aspx?EmpID=" + EmpID);
        }

        public void BindResponsibilties()
        {
            int EmpID = Convert.ToInt32(Request.QueryString["EmpID"].ToString());
           DataSet ds = objRespon.GetResponsibility(EmpID);
            if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0)
            {
                txtResponsibility.Text = ds.Tables[0].Rows[0]["Responsible"].ToString().Replace("<br/>", "\n");
            }
            else
            {
                txtResponsibility.Text = string.Empty;
            }


        }
    }
}
