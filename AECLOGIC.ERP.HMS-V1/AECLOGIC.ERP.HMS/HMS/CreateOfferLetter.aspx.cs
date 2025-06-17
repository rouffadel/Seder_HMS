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
using System.Web.Mail;
using System.Net.Mime;
using System.Data.SqlClient;
using AECLOGIC.HMS.BLL;
namespace AECLOGIC.ERP.HMS
{
    public partial class CreateAplicantOfferLetter : AECLOGIC.ERP.COMMON.WebFormMaster
    {
        int mid = 0;
        bool viewall;
        string menuname;
        string menuid;
        string sDesignation = "Site Manager";
        double Salary = 12000;

        private string siteurl = System.Configuration.ConfigurationManager.AppSettings["SiteUrl"].ToString();
        string RegEmailID = System.Configuration.ConfigurationManager.AppSettings["RegEmailID"].ToString();
        string SMTPServer = System.Configuration.ConfigurationManager.AppSettings["SMTPServer"].ToString();
        string WebSiteID = System.Configuration.ConfigurationManager.AppSettings["WebSiteID"].ToString();
        string Company = System.Configuration.ConfigurationManager.AppSettings["Company"].ToString();

        AttendanceDAC objApp = new AttendanceDAC();
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
                BindCategories();
                BindDesignations();
                DataSet ds;
                txtAppDate.Text = DateTime.Now.ToString(ConfigurationManager.AppSettings["DateFormat"]);
                if (Request.QueryString.Count > 0)
                {
                    
                    ds = AttendanceDAC.GetWorkSite(0, '1', Convert.ToInt32(Session["CompanyID"]));
                    ddlProject.DataSource = ds.Tables[0];
                    ddlProject.DataTextField = "Site_Name";
                    ddlProject.DataValueField = "Site_ID";
                    ddlProject.DataBind();
                    ds = objApp.GetApplicantdetails(Convert.ToInt32(Request.QueryString["id"]));


                    if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
                    {
                        lblName.Text = ds.Tables[0].Rows[0]["name"].ToString();
                        lblAddress.Text = ds.Tables[0].Rows[0]["Address"].ToString();
                        lblCity.Text = ds.Tables[0].Rows[0]["City"].ToString();
                        lblState.Text = ds.Tables[0].Rows[0]["State"].ToString();
                        lblCountry.Text = ds.Tables[0].Rows[0]["Country"].ToString(); ;
                        lblPin.Text = " - " + ds.Tables[0].Rows[0]["pin"].ToString(); ;
                        lblPhone.Text = ds.Tables[0].Rows[0]["Phone"].ToString();
                        ddlDesig.SelectedValue = ds.Tables[0].Rows[0]["DesigID"].ToString();
                        ddlCategory.SelectedValue = ds.Tables[0].Rows[0]["Trade"].ToString();
                        ////txtDesig.Text = ds.Tables[0].Rows[0]["Position"].ToString();
                        txtSalary.Text = ds.Tables[0].Rows[0]["ExpectedCTC"].ToString();
                        txtAppDate.Text = DateTime.Now.ToString(ConfigurationManager.AppSettings["DateFormat"]);
                        txtDOJ.Text = DateTime.Now.ToString(ConfigurationManager.AppSettings["DateFormat"]);

                       
                        lblDate.Text = "Date : " + DateTime.Now.GetDateTimeFormats()[10];

                        ds = objApp.GetEmpDoc(0, 2, 0);
                         string strText="";
                        if (ds.Tables[0].Rows.Count == 0)
                        {
                            AlertMsg.MsgBox(Page, "Format Not Found");
                        }
                        else
                        {
                          
                            strText = ds.Tables[0].Rows[0]["value"].ToString();
                            txtRTB.Text = strText;
                            Page.RegisterStartupScript("ll", "<script language='javascript' type='text/javascript'> LoadDiv();</script>");
                           
                        }
                        
                    }
                }
                txtAppDate.Attributes.Add("onblur", "javascript:return ChangeAppDate();");

            }
        }
        private void BindCategories()
        {
            DataSet ds = objApp.GetCategories();
            DataTable dt = ds.Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                ddlCategory.Items.Add(new ListItem(dr["Category"].ToString(), dr["CateId"].ToString()));
            }
            ddlCategory.Items.Insert(0, new ListItem("---Select---", "0"));
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                string filename = "", ext = "", path = "", ThumbPath = "";
                filename = fupdempimag.PostedFile.FileName;
                if (filename != "")
                    ext = filename.Split('.')[filename.Split('.').Length - 1];
                string str = txtRTB.Text.Replace("\"", "").Replace("'", "");
                objHrCommon.AppID = Convert.ToInt32(Request.QueryString["id"]);
                objHrCommon.Salary = Convert.ToDouble(txtSalary.Text);
                ////objHrCommon.Designation = txtDesig.Text;
                objHrCommon.ReqDate = CODEUtility.ConvertToDate(txtDOJ.Text.Trim(), DateFormat.DayMonthYear);
                objHrCommon.OfferLetter = str;
                objHrCommon.UserID =  Convert.ToInt32(Session["UserId"]);
                if (ddljobtype.SelectedItem.Value == "1")
                {
                    objHrCommon.JobType = 1;
                }
                else
                {
                    objHrCommon.JobType = 0;
                }
                objHrCommon.ImageType = ext;
                objHrCommon.SiteID = Convert.ToInt32(ddlProject.SelectedValue);
                int DesigID = Convert.ToInt32(ddlDesig.SelectedValue);
                int TradeID = Convert.ToInt32(ddlCategory.SelectedValue);

                objApp.AddApplicantOfferDetails(objHrCommon, DesigID, TradeID);

                if (filename != "")
                {

                    path = Server.MapPath(".\\ApplicantImages\\" + Convert.ToInt32(Request.QueryString["id"]) + "." + ext);
                    ThumbPath = Server.MapPath(".\\ApplicantImages\\" + Convert.ToInt32(Request.QueryString["id"]) + "_thumb." + ext);
                    fupdempimag.PostedFile.SaveAs(path);
                    PicUtil.ConvertPic(path, 128, 160, path);
                    PicUtil.ConvertPic(path, 19, 24, ThumbPath);
                }
                //SendMailToApplicant();

                Response.Redirect("ViewOfferLetterList.aspx");


            }
            catch (Exception Ex)
            {

                throw Ex;
            }
            //42337



        }
        public void SendMailToApplicant()
        {

            try
            {
                string ToMailID = string.Empty;
                string Name = string.Empty;
                string Designation = string.Empty;
                string Salary = string.Empty;
                string DateOfJoin = string.Empty;
                string JobType = string.Empty;
                DataSet ds = objApp.GetAppOfferDetails(Convert.ToInt32(Request.QueryString["id"]));
                if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
                {
                    ToMailID = ds.Tables[0].Rows[0]["Email"].ToString();
                    Name = ds.Tables[0].Rows[0]["name"].ToString();
                    Designation = ds.Tables[0].Rows[0]["Designation"].ToString();
                    Salary = ds.Tables[0].Rows[0]["Salary"].ToString();
                    DateOfJoin = ds.Tables[0].Rows[0]["ReqDOJ"].ToString();
                    JobType = ds.Tables[0].Rows[0]["JobType1"].ToString();
                }
                int ApplicantID = Convert.ToInt32(Request.QueryString["id"]);
                MailMessage msgMail = new MailMessage();

                msgMail.To = ToMailID;
                msgMail.From = RegEmailID;
                msgMail.Subject = "Offer Letter Confirmation";
                string bodyMessage = "";
                string imagescr = siteurl + "Images/logo1";
                string imagebackground = siteurl + "Images/middal_bg";
                string date = DateTime.Now.ToString();
                string date1 = Convert.ToDateTime(date).ToString("MMMM dd, yyyy hh:mm tt");
                string date2 = Convert.ToDateTime(date).ToString("MMMM dd, yyyy");
                bodyMessage = bodyMessage + "<table width='560' border='0' cellspacing='0' cellpadding='0' align='center' style='font-family:Arial, Helvetica, sans-serif; font-size:11px; color:#333333; border:#999999 solid 1px;'>";
                bodyMessage = bodyMessage + " <tr>";
                bodyMessage = bodyMessage + "<td><img src=" + imagescr + " /></td>";
                bodyMessage = bodyMessage + " </tr>";
                bodyMessage = bodyMessage + "<td style='font-size:16px; font-weight:bold; color:#666666; padding-left:34px;line-height:29px;color:#ffffff'> " + Company + " Offer Letter Confirmation</td>";
                bodyMessage = bodyMessage + "</tr>";
                bodyMessage = bodyMessage + "</table></td>";
                bodyMessage = bodyMessage + "</tr>";
                bodyMessage = bodyMessage + "<tr>";
                bodyMessage = bodyMessage + "<td background=" + imagebackground + ">";
                bodyMessage = bodyMessage + "<table width='100%' border='0' cellspacing='0' cellpadding='0'>";
                bodyMessage = bodyMessage + "<tr valign='top'>";
                bodyMessage = bodyMessage + "<td style='padding:10px'>";
                //bodyMessage = bodyMessage + "<p style='padding:0px 0px 15px 0px; margin:0px; font-size:14px; font-weight:bold; color:#999999;'>" + date2 + "</p>";
                bodyMessage = bodyMessage + "<p style='padding:0px 0px 15px 0px; margin:0px; font-family:Arial, Helvetica, sans-serif; font-size:12px; color:#333333;'>Hi " + Name + ",</p>";
                bodyMessage = bodyMessage + "<p style='padding:0px 0px 15px 0px; margin:0px; font-family:Arial, Helvetica, sans-serif; font-size:12px; color:#333333;'>Welcome to " + Company + "</p>";

                bodyMessage = bodyMessage + "<p style='padding:0px 0px 15px 0px; margin:0px; font-family:Arial, Helvetica, sans-serif; font-size:12px; color:#333333;'>" + Company + " Offers to you</p>";

                bodyMessage = bodyMessage + "<p style='padding:0px 0px 15px 0px; margin:0px; font-family:Arial, Helvetica, sans-serif; font-size:12px; color:#333333;'>Your Designation : " + Designation + " </p>";

                bodyMessage = bodyMessage + "<p style='padding:0px 0px 15px 0px; margin:0px; font-family:Arial, Helvetica, sans-serif; font-size:12px; color:#333333;'>Your Salary : " + Salary + " </p>";

                bodyMessage = bodyMessage + "<p style='padding:0px 0px 15px 0px; margin:0px; font-family:Arial, Helvetica, sans-serif; font-size:12px; color:#333333;'>Date Of Join : " + DateOfJoin + " </p>";

                bodyMessage = bodyMessage + "<p style='padding:0px 0px 15px 0px; margin:0px; font-family:Arial, Helvetica, sans-serif; font-size:12px; color:#333333;'>JobType : " + JobType + " </p>";
                bodyMessage = bodyMessage + "<p style='padding:0px 0px 15px 0px; margin:0px; font-family:Arial, Helvetica, sans-serif; font-size:12px; color:#333333;'>Can you please click the link below to verify that this is your offerletter confirmation </p>";

                string VerificationURL = siteurl + "OfferConfirmation.aspx?id=" + ApplicantID;
                bodyMessage = bodyMessage + "<p style='padding:0px 0px 15px 0px; margin:0px; font-family:Arial, Helvetica, sans-serif; font-size:12px; color:#333333;'><a href=" + VerificationURL + " target='_blank'>" + VerificationURL + "</a></p>";
                bodyMessage = bodyMessage + "<p style='padding:0px 0px 15px 0px; margin:0px; font-family:Arial, Helvetica, sans-serif; font-size:12px; color:#333333;'>Thank  you.</p>";
                bodyMessage = bodyMessage + "<p style='padding:0px 0px 15px 0px; margin:0px; font-family:Arial, Helvetica, sans-serif; font-size:12px; color:#333333;'>From,<br /> " + Company + "<br /><a href='" + WebSiteID + "' target='_blank'>" + WebSiteID + "</a></p>";
                bodyMessage = bodyMessage + "<td>";
                bodyMessage = bodyMessage + " </tr>";

                bodyMessage = bodyMessage + "</table>";
                bodyMessage = bodyMessage + "</td>";
                bodyMessage = bodyMessage + "</tr>";
                bodyMessage = bodyMessage + "</table>";
                msgMail.BodyFormat = MailFormat.Html;
                msgMail.Body = bodyMessage;
                SmtpMail.SmtpServer = SMTPServer;
                SmtpMail.Send(msgMail);

            }
            catch (Exception)
            {

                throw;
            }

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("ViewSelectedApplicantList.aspx");
        }
        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("ViewSelectedApplicantList.aspx");
        }

        private void BindDesignations()
        {
            DataSet ds = objApp.GetDesignations();
            ddlDesig.DataSource = ds;
            ddlDesig.DataTextField = "Designation";
            ddlDesig.DataValueField = "DesigId";
            ddlDesig.DataBind();
            ddlDesig.Items.Insert(0, new ListItem("---ALL---", "0"));
          
        }
    }
}