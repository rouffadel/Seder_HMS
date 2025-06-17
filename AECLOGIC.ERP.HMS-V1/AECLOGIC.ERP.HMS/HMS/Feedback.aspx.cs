using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AECLOGIC.HMS.BLL;
using System.Data;
using System.Configuration;
using AECLOGIC.HMS.BLL;

namespace AECLOGIC.ERP.HMS
{
    public partial class Feedback : AECLOGIC.ERP.COMMON.WebFormMaster
    {
        AttendanceDAC objAtt = new AttendanceDAC();
         
        int mid = 0;
        bool viewall, Editable;
        string menuname;
        string menuid;
        protected override void OnInit(EventArgs e)
        {
            ModuleID = 1;
            base.OnInit(e);
        }
        protected void Page_Load(object sender, EventArgs e)
        {

           
            lblDate.Text = "Date: " + DateTime.Now.ToString(ConfigurationManager.AppSettings["DateDisplayFormat"]);
          
            if (!IsPostBack)
            {
                GetParentMenuId();
                ViewState["FBId"] = 0;
                int FBNo = Convert.ToInt32(ViewState["FBId"]);
                int EmpID =  Convert.ToInt32(Session["UserId"]);
                BindData(EmpID);
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
               Editable = (bool)ds.Tables[0].Rows[0]["Editable"];
                btnSubmit.Enabled = Editable;
            }
            return MenuId;
        }
        public void BindData(int EmpID)
        {

            DataSet ds = AttendanceDAC.GetT_HR_FeedbackBind(EmpID);
            if (ds.Tables[0].Rows.Count > 0)
            {
                txtName.Text = ds.Tables[0].Rows[0]["EmpName"].ToString();
                txtPhone.Text = ds.Tables[0].Rows[0]["Mobile1"].ToString();
            }
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlUser.SelectedItem.Text == "Anonimous")
                {
                    mvanonimus.Visible = false;
                    try
                    {
                        int EmpID =  Convert.ToInt32(Session["UserId"]);
                        int FBId;
                        int FBNo = Convert.ToInt32(ViewState["FBId"]);
                        if (FBNo == 0)
                        {
                            FBId = 0;
                        }
                        else
                        {
                            FBId = FBNo;
                        }
                        // string fbtype = ddlFbtype.SelectedItem.Text;
                        string FeedBackType = ddlFbtype.SelectedItem.Text;
                        string UserType = ddlUser.SelectedItem.Text;
                        string Name = txtName.Text;
                       
                        string Comment = txtFeedback.Text;
                        DateTime Date = DateTime.Now;
                        AttendanceDAC.InsUpFeedback(EmpID, FeedBackType, UserType, Name, txtPhone.Text, Comment, Date);

                        AlertMsg.MsgBox(Page, "Feedback is posted.!");
                          Response.Redirect("ViewFeedback.aspx");
                       
                    }
                    catch (Exception ex)
                    {
                    }
                }
                else
                {
                    try
                    {
                        int EmpID =  Convert.ToInt32(Session["UserId"]);
                        int FBId;
                        int FBNo = Convert.ToInt32(ViewState["FBId"]);
                        if (FBNo == 0)
                        {
                            FBId = 0;
                        }
                        else
                        {
                            FBId = FBNo;
                        }
                        string FeedBackType = ddlFbtype.SelectedItem.Text;
                        string UserType = ddlUser.SelectedItem.Text;
                        string Name = txtName.Text;
                        // int Mobile = Convert.ToInt64(txtPhone.Text.ToString());
                        string Comment = txtFeedback.Text;
                        DateTime Date = DateTime.Now;
                        AttendanceDAC.InsUpFeedback(EmpID, FeedBackType, UserType, Name, txtPhone.Text, Comment, Date);
                       string page = "ViewFeedback.aspx";
                       
                        AlertMsg.MsgBox(Page, "Done.!");
                       Response.Redirect("ViewFeedback.aspx");
                    }
                    catch (Exception ex)
                    {
                    }
                }
            }
            catch (Exception FeedBack)
            {
                AlertMsg.MsgBox(Page, FeedBack.Message.ToString(),AlertMsg.MessageType.Error);
            }
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {

            txtFeedback.Text = txtName.Text = txtPhone.Text = "";
        }

        protected void ddlUser_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlUser.SelectedIndex == 2)
            {
                mvanonimus.Visible = false;
            }
        }
       
    }
}