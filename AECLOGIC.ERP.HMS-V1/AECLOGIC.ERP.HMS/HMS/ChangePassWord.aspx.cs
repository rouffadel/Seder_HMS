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
    public partial class ChangePassWord : AECLOGIC.ERP.COMMON.WebFormMaster
    {
        AttendanceDAC objAtt = new AttendanceDAC();
        HRCommon objcommon = new HRCommon();

        protected override void OnInit(EventArgs e)
        {
            ModuleID = 1;
            base.OnInit(e);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
           
        }
        protected void btnsubmit_Click(object sender, EventArgs e)
        {
            try
            {
                objcommon.EmpID =  Convert.ToInt32(Session["UserId"]);
                objcommon.OldPassWord = FormsAuthentication.HashPasswordForStoringInConfigFile(txtOldPassword.Text.Trim(), "MD5");
                objcommon.NewPassWord = FormsAuthentication.HashPasswordForStoringInConfigFile(txtNewPassword.Text.Trim(), "MD5");
                int Res = Convert.ToInt32(objAtt.Updatepassword(objcommon));
                if (Res == 1)
                {
                    AlertMsg.MsgBox(Page, "Password Changed Successfully");
                }
                else
                {
                    AlertMsg.MsgBox(Page, "Your details are invalid");
                }
            }
            catch (Exception ex)
            {

                AlertMsg.MsgBox(Page, ex.Message.ToString(),AlertMsg.MessageType.Error);
            }
        }
        protected void btncancel_Click(object sender, EventArgs e)
        {
            try
            {
                txtOldPassword.Text = "";
                txtNewPassword.Text = "";
                txtReenterPassword.Text = "";
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}