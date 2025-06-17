using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AECLOGIC.HMS.BLL;

namespace AECLOGIC.ERP.HMS
{
    public partial class AddInterviewType : AECLOGIC.ERP.COMMON.WebFormMaster
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected override void OnInit(EventArgs e)
        {
            ModuleID = 1;
            base.OnInit(e);
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string InterviewType = txtType.Text;
            AttendanceDAC.NewInterViewType(InterviewType);
            AlertMsg.MsgBox(Page, "Done.!!");
            Response.Write("<script language='javascript'> { window.close();}</script>");
            btnBack.Attributes.Add("onclick", "window.close();");
        }
        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Write("<script language='javascript'> { window.close();}</script>");
            btnBack.Attributes.Add("onclick", "window.close();");
            // Response.Redirect("CreatePosting.aspx");
        }
    }
}