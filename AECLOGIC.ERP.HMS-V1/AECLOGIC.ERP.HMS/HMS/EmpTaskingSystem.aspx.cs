using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using AECLOGIC.HMS.BLL;
using SMSConfig;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Net.Mime;
using System.Configuration;
using System.Collections;
//using System.Net.Mail;
using System.Net;
using System.Web.Mail;

namespace AECLOGIC.ERP.HMS
{
    public partial class EmpTaskingSystem : AECLOGIC.ERP.COMMON.WebFormMaster
    {
        private GridSort objSort;
        static string strurl = string.Empty;
        AttendanceDAC objRights = new AttendanceDAC();

        private string siteurl = System.Configuration.ConfigurationManager.AppSettings["SiteUrl"].ToString();
        string RegEmailID = System.Configuration.ConfigurationManager.AppSettings["RegEmailID"].ToString();
        string SMTPServer = System.Configuration.ConfigurationManager.AppSettings["SMTPServer"].ToString();
        string WebSiteID = System.Configuration.ConfigurationManager.AppSettings["WebSiteID"].ToString();
        string Company = System.Configuration.ConfigurationManager.AppSettings["Company"].ToString();
        string SMSUserID = System.Configuration.ConfigurationManager.AppSettings["SMSUserID"].ToString();
        string SMSPassword = System.Configuration.ConfigurationManager.AppSettings["SMSPassword"].ToString();

        int Id = 1;
        int mid = 0;
        bool viewall;
        string menuname;
        string menuid;
        String MyString;
        string extension;
        bool Editable;

        int CompanyID = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["CompanyID"].ToString());
        protected override void OnInit(EventArgs e)
        {
            ModuleID = 1;
            base.OnInit(e);
        }

        protected void Page_Load(object sender, EventArgs e)
        {



            int Roleid = int.Parse(Session["RoleId"].ToString());
            if (Roleid == 6 || Roleid == 8)
            {
                btnMailto.Enabled = btnsave4.Enabled = btnSend.Enabled = btnSMS2.Enabled = btnSMS3.Enabled = btnSMSsend.Enabled = btnMailto2.Enabled = true;
            }
            else
            {
                btnMailto.Enabled = btnsave4.Enabled = btnSend.Enabled = btnSMS2.Enabled = btnSMS3.Enabled = btnSMSsend.Enabled = btnMailto2.Enabled = false; ;

            }


            if (!IsPostBack)
            {

                strurl = Request.UrlReferrer.ToString();
                objSort = new GridSort();
                ViewState["Sort"] = objSort;
                if (Request.QueryString.Count > 0)
                {
                    int id = Convert.ToInt32(Request.QueryString["key"].ToString());
                    if (id == 1)
                    {
                        tblUpdated.Visible = true;
                        BindGrid2();
                    }
                    if (id == 2)
                    {
                        tblNotUpdated.Visible = true;
                        BindGrid3();
                    }
                    if (id == 3)
                    {
                        tblNewJoin.Visible = true;
                        BindGrid4();
                    }
                    if (id == 4)
                    {
                        tblUpdateToday.Visible = true;
                        BindGrid5();
                    }
                }
                else
                {
                    tblUpdaters.Visible = true;
                    BindGrid1();
                }

            }
        }
  
        public void BindGrid1()
        {
            DataSet ds = AttendanceDAC.HR_EmpTaskSystem(Convert.ToInt32(Session["CompanyID"]));
            gvUpdaters.DataSource = ds.Tables[0];
            gvUpdaters.DataBind();
        }
        public void BindGrid2()
        {
           
            DataSet ds1 = AttendanceDAC.HR_EmpTaskSystem(Convert.ToInt32(Session["CompanyID"]));

            gvUpdated.DataSource = ds1.Tables[1];
            ViewState["DataSet"] = ds1.Tables[1];
            gvUpdated.DataBind();

        }
        public void BindGrid3()
        {
           
            DataSet ds2 = AttendanceDAC.HR_EmpTaskSystem(Convert.ToInt32(Session["CompanyID"]));
            gvNotUpdated.DataSource = ds2.Tables[5];
            gvNotUpdated.DataBind();
            tblSMSnotUpdated.Visible = true;
        }
        public void BindGrid4()
        {
           
            DataSet ds2 = AttendanceDAC.HR_EmpTaskSystem(Convert.ToInt32(Session["CompanyID"]));
            gvNewJoin.DataSource = ds2.Tables[3];
            gvNewJoin.DataBind();
        }

        public void BindGrid5()
        {
            tblSms3.Visible = false;
            tblMailNew3.Visible = false;

            DataSet ds2 = AttendanceDAC.HR_EmpTaskSystem(Convert.ToInt32(Session["CompanyID"]));
            gvUpdateToday.DataSource = ds2.Tables[4];
            gvUpdateToday.DataBind();
        }
        public void BindWorkSites()
        {
            try
            {

                DataSet ds = AttendanceDAC.GetWorkSite(0, '1', Convert.ToInt32(Session["CompanyID"]));
                ViewState["WorkSites"] = ds;

            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public string GetWorkSite(string WSid)
        {
            string retVal = "";
            try
            {

                DataSet ds = AttendanceDAC.GetWorkSite(0, '1', Convert.ToInt32(Session["CompanyID"]));
                retVal = ds.Tables[0].Select("Site_ID='" + WSid + "'")[0]["Site_Name"].ToString();
            }
            catch { }
            return retVal;
        }
        protected void btnback_Click(object sender, EventArgs e)
        {
            Response.Redirect(strurl);
        }
        protected void gvUpdated_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "sms")
            {
                tblUpdatedSMS.Visible = true;
                tblMailto.Visible = false;
                int EmpID = Convert.ToInt32(e.CommandArgument);

                DataSet ds = AttendanceDAC.HR_EmpMobile(EmpID);
                lblmsg.Text = ds.Tables[0].Rows[0]["name"].ToString();
                txtCompany.Text = ds.Tables[0].Rows[0]["Mobile2"].ToString();
                txtPersonal.Text = ds.Tables[0].Rows[0]["Mobile1"].ToString();
                if (txtCompany.Text == "")
                {
                    rbPersonal.Checked = true;
                    rbCompany.Checked = false;
                }
                else { rbCompany.Checked = true; }
                if (txtPersonal.Text == "" || txtPersonal.Text == "0123456789")
                {
                    txtPersonal.Text = "";
                    rbCompany.Checked = true;
                }
                if (txtPersonal.Text != "" && txtCompany.Text != "")
                {
                    rbCompany.Checked = true;
                    rbPersonal.Checked = false;
                }
            }
            if (e.CommandName == "mail")
            {
                tblMailto.Visible = true;
                tblUpdatedSMS.Visible = false;
                int EmpID = Convert.ToInt32(e.CommandArgument);

                DataSet ds = AttendanceDAC.HR_GetMailID(EmpID);
                lblmailto1.Text = ds.Tables[0].Rows[0]["name"].ToString();
                lblMailidTo.Text = ds.Tables[0].Rows[0]["MailId"].ToString();

            }

        }
        protected void btnSend_Click(object sender, EventArgs e)
        {
            try
            {
                SMSCAPI m = new SMSCAPI();
                string msg = txtMessage.Text;
                string mobile;
                if (rbCompany.Checked == true)
                {
                    mobile = txtCompany.Text;
                }
                else
                {
                    mobile = txtPersonal.Text;
                }
                //  m.SendSMS(SMSUserID, SMSPassword, mobile, msg);
            }
            catch (Exception EmpTaskSys)
            {
                AlertMsg.MsgBox(Page, EmpTaskSys.Message.ToString(),AlertMsg.MessageType.Error);
            }
        }

        protected void btnMailto_Click(object sender, EventArgs e)
        {
            try
            {
                string ToMailID = lblMailidTo.Text;
                string MailContent = txtContent.Text;
                SendMailToApplicant(ToMailID, MailContent);
                Response.Redirect("EmpTaskingSystem.aspx?key=1");
            }
            catch (Exception EmpTas)
            {
                AlertMsg.MsgBox(Page, EmpTas.Message.ToString(),AlertMsg.MessageType.Error);
            }
        }

        protected void rbCompany_CheckedChanged(object sender, EventArgs e)
        {
            rbPersonal.Checked = false;
            rbCompany.Checked = true;
        }
        protected void rbPersonal_CheckedChanged(object sender, EventArgs e)
        {
            rbPersonal.Checked = true;
            rbCompany.Checked = false;
        }
        protected void btnSMSsend_Click(object sender, EventArgs e)
        {

            try
            {
                foreach (GridViewRow gvr in gvNotUpdated.Rows)
                {
                    CheckBox chksms = (CheckBox)gvr.Cells[6].FindControl("chkMail2All");
                    if (chksms.Checked == true)
                    {
                        Label lblEmpID = (Label)gvr.Cells[0].FindControl("lblEmpId");
                        int EmpID = Convert.ToInt32(lblEmpID.Text);

                        DataSet ds = AttendanceDAC.HR_GetMailID(EmpID);
                        string ToMailID = ds.Tables[0].Rows[0]["MailId"].ToString();
                        SendMailToApplicant(ToMailID);

                    }
                    CheckBox chksms2 = (CheckBox)gvr.Cells[7].FindControl("chkSMS2All");
                    if (chksms2.Checked == true)
                    {
                        Label lblEmpID = (Label)gvr.Cells[0].FindControl("lblEmpId");
                        int EmpID = Convert.ToInt32(lblEmpID.Text);

                        DataSet ds = AttendanceDAC.HR_EmpMobile(EmpID);
                        string mobileno = ds.Tables[1].Rows[0][0].ToString();
                        SMSCAPI m = new SMSCAPI();
                        string msg = txtNotUPdateSMS.Text;
                        // m.SendSMS(SMSUserID, SMSPassword, mobileno, msg);
                    }
                }
                AlertMsg.MsgBox(Page, "Done!");
                txtNotUPdateSMS.Text = "";
                Response.Redirect("EmpTaskingSystem.aspx?key=2");
            }
            catch (Exception ex)
            {
                AlertMsg.MsgBox(Page, ex.Message.ToString(),AlertMsg.MessageType.Error);
            }
        }
        public void SendMailToApplicant(string ToMailID)
        {
            try
            {
                siteurl = System.Configuration.ConfigurationManager.AppSettings["SiteUrl"].ToString();
                RegEmailID = System.Configuration.ConfigurationManager.AppSettings["RegEmailID"].ToString();
                SMTPServer = System.Configuration.ConfigurationManager.AppSettings["SMTPServer"].ToString();
                WebSiteID = System.Configuration.ConfigurationManager.AppSettings["WebSiteID"].ToString();
                Company = System.Configuration.ConfigurationManager.AppSettings["Company"].ToString();
                

                   
                MailMessage msgMail = new MailMessage();
                msgMail.To = ToMailID;
                msgMail.From = RegEmailID;
                msgMail.Subject = "Please Update Your Task Daily";
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
                bodyMessage = bodyMessage + "<td style='font-size:16px; font-weight:bold; color:#666666; padding-left:34px;line-height:29px;color:#ffffff'>" + Company + " Task Management System</td>";
                bodyMessage = bodyMessage + "</tr>";
                bodyMessage = bodyMessage + "</table></td>";
                bodyMessage = bodyMessage + "</tr>";
                bodyMessage = bodyMessage + "<tr>";
                bodyMessage = bodyMessage + "<td background=" + imagebackground + ">";
                bodyMessage = bodyMessage + "<table width='100%' border='0' cellspacing='0' cellpadding='0'>";
                bodyMessage = bodyMessage + "<tr valign='top'>";
                bodyMessage = bodyMessage + "<td style='padding:10px'>";
                bodyMessage = bodyMessage + "<p style='padding:0px 0px 15px 0px; margin:0px; font-family:Arial, Helvetica, sans-serif; font-size:12px; color:#333333;'> " + txtNotUPdateSMS.Text + ",</p>";
                bodyMessage = bodyMessage + "<p style='padding:0px 0px 15px 0px; margin:0px; font-family:Arial, Helvetica, sans-serif; font-size:12px; color:#333333;'>Thank  you.</p>";
                bodyMessage = bodyMessage + "<p style='padding:0px 0px 15px 0px; margin:0px; font-family:Arial, Helvetica, sans-serif; font-size:12px; color:#333333;'>From,<br /> " + Company + "<br /><a href='" + WebSiteID + "' target='_blank'>" + WebSiteID + "</a></p>";
                bodyMessage = bodyMessage + "<td>";
                bodyMessage = bodyMessage + " </tr>";
                bodyMessage = bodyMessage + "</table>";
                bodyMessage = bodyMessage + "</td>";
                bodyMessage = bodyMessage + "</tr>";
                bodyMessage = bodyMessage + "</table>";
                // msgMail.IsBodyHtml = true;
                msgMail.BodyFormat = MailFormat.Html;
                msgMail.Body = bodyMessage;

               
                SmtpMail.SmtpServer = SMTPServer;
                SmtpMail.Send(msgMail);
            }
            catch (Exception ex)
            {
                AlertMsg.MsgBox(Page, ex.Message.ToString() + " " + ex.Source.ToString());
                // throw;
            }

        }
        public void SendMailToApplicant(string ToMailID, string MailContent)
        {
            try
            {
                siteurl = System.Configuration.ConfigurationManager.AppSettings["SiteUrl"].ToString();
                RegEmailID = System.Configuration.ConfigurationManager.AppSettings["RegEmailID"].ToString();
                SMTPServer = System.Configuration.ConfigurationManager.AppSettings["SMTPServer"].ToString();
                WebSiteID = System.Configuration.ConfigurationManager.AppSettings["WebSiteID"].ToString();
                Company = System.Configuration.ConfigurationManager.AppSettings["Company"].ToString();
                   
                MailMessage msgMail = new MailMessage();
                // System.Net.Mail.MailMessage msgMail = new System.Net.Mail.MailMessage(RegEmailID, ToMailID);

                msgMail.To = ToMailID;
                msgMail.From = RegEmailID;
                msgMail.Subject = "Please Update Your Task Daily";
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
                bodyMessage = bodyMessage + "<td style='font-size:16px; font-weight:bold; color:#666666; padding-left:34px;line-height:29px;color:#ffffff'> " + Company + " Task Management System</td>";
                bodyMessage = bodyMessage + "</tr>";
                bodyMessage = bodyMessage + "</table></td>";
                bodyMessage = bodyMessage + "</tr>";
                bodyMessage = bodyMessage + "<tr>";
                bodyMessage = bodyMessage + "<td background=" + imagebackground + ">";
                bodyMessage = bodyMessage + "<table width='100%' border='0' cellspacing='0' cellpadding='0'>";
                bodyMessage = bodyMessage + "<tr valign='top'>";
                bodyMessage = bodyMessage + "<td style='padding:10px'>";
                bodyMessage = bodyMessage + "<p style='padding:0px 0px 15px 0px; margin:0px; font-family:Arial, Helvetica, sans-serif; font-size:12px; color:#333333;'> " + MailContent + ",</p>";
                bodyMessage = bodyMessage + "<p style='padding:0px 0px 15px 0px; margin:0px; font-family:Arial, Helvetica, sans-serif; font-size:12px; color:#333333;'>Thank  you.</p>";
                bodyMessage = bodyMessage + "<p style='padding:0px 0px 15px 0px; margin:0px; font-family:Arial, Helvetica, sans-serif; font-size:12px; color:#333333;'>From,<br /> " + Company + "<br /><a href='" + WebSiteID + "' target='_blank'>" + WebSiteID + "</a></p>";
                bodyMessage = bodyMessage + "<td>";
                bodyMessage = bodyMessage + " </tr>";
                bodyMessage = bodyMessage + "</table>";
                bodyMessage = bodyMessage + "</td>";
                bodyMessage = bodyMessage + "</tr>";
                bodyMessage = bodyMessage + "</table>";
                // msgMail.IsBodyHtml = true;
                msgMail.BodyFormat = MailFormat.Html;
                msgMail.Body = bodyMessage;
                SmtpMail.SmtpServer = SMTPServer;
                SmtpMail.Send(msgMail);
                
            }
            catch (Exception ex)
            {
                AlertMsg.MsgBox(Page, ex.Message.ToString() + " " + ex.Source.ToString());
                //throw;
            }

        }
       
        protected void gvNotUpdated_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    string strScript = "SelectDeSelectHeader(" + ((CheckBox)e.Row.Cells[0].FindControl("chkSmS")).ClientID + ");";
                    ((CheckBox)e.Row.Cells[0].FindControl("chkSmS")).Attributes.Add("onclick", strScript);

                }
                else if (e.Row.RowType == DataControlRowType.Header)
                {
                    CheckBox chkMail = (CheckBox)e.Row.FindControl("chkHMail2All");
                    CheckBox chkSMS = (CheckBox)e.Row.FindControl("chkHSMS2All");
                    chkMail.Attributes.Add("onclick", String.Format("javascript:return SelectAll(this,'{0}','chkMail2All');", gvNotUpdated.ClientID));
                    chkSMS.Attributes.Add("onclick", String.Format("javascript:return SelectAll(this,'{0}','chkSMS2All');", gvNotUpdated.ClientID));
                }
            }
            catch (Exception Ex)
            {
                //report error
            }
        }
        protected void btnSMS2_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (GridViewRow gvr in gvNotUpdated.Rows)
                {
                    CheckBox chksms = (CheckBox)gvr.Cells[6].FindControl("chkSmS");
                    if (chksms.Checked == true)
                    {
                        Label lblEmpID = (Label)gvr.Cells[0].FindControl("lblEmpId");
                        int EmpID = Convert.ToInt32(lblEmpID.Text);
                           
                      DataSet  ds = AttendanceDAC.HR_EmpMobile(EmpID);
                        string mobileno = ds.Tables[1].Rows[0][0].ToString();
                        SMSCAPI m = new SMSCAPI();
                        string msg = txtNotUPdateSMS.Text;
                        //    m.SendSMS(SMSUserID, SMSPassword, mobileno, msg);
                    }
                }
                AlertMsg.MsgBox(Page, "Done!");
                txtNotUPdateSMS.Text = "";
            }
            catch (Exception ex1)
            {
                AlertMsg.MsgBox(Page, ex1.Message.ToString(),AlertMsg.MessageType.Error);
            }
        }
        protected void btnSMS3_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (GridViewRow gvr in gvNotUpdated.Rows)
                {
                    CheckBox chksms = (CheckBox)gvr.Cells[6].FindControl("chkSmS");
                    if (chksms.Checked == true)
                    {
                        Label lblEmpID = (Label)gvr.Cells[0].FindControl("lblEmpId");
                        int EmpID = Convert.ToInt32(lblEmpID.Text);

                        DataSet ds = AttendanceDAC.HR_EmpMobile(EmpID);
                        string mobileno = ds.Tables[1].Rows[0][0].ToString();
                        SMSCAPI m = new SMSCAPI();
                        string msg = txtNotUPdateSMS.Text;
                        //   m.SendSMS(SMSUserID, SMSPassword, mobileno, msg);
                    }
                }
                AlertMsg.MsgBox(Page, "Done!");
                txtNotUPdateSMS.Text = "";
            }
            catch (Exception ex3)
            {
                AlertMsg.MsgBox(Page, ex3.Message.ToString(),AlertMsg.MessageType.Error);
            }
        }
        protected void gvNewJoin_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "sms")
            {
                tblsms2.Visible = true;
                tblMail4.Visible = false;
                int EmpID = Convert.ToInt32(e.CommandArgument);

                DataSet ds = AttendanceDAC.HR_EmpMobile(EmpID);
                lblmsg2.Text = ds.Tables[0].Rows[0]["name"].ToString();
                txtCompany2.Text = ds.Tables[0].Rows[0]["Mobile2"].ToString();
                txtPersonal2.Text = ds.Tables[0].Rows[0]["Mobile1"].ToString();
                if (txtCompany2.Text == "")
                {
                    rbPersonal2.Checked = true;
                    rbCompany2.Checked = false;
                }
                else { rbCompany2.Checked = true; }
                if (txtPersonal2.Text == "" || txtPersonal2.Text == "0123456789")
                {
                    txtPersonal2.Text = "";
                    rbCompany2.Checked = true;
                }
                if (txtPersonal2.Text != "" && txtCompany2.Text != "")
                {
                    rbCompany2.Checked = true;
                    rbPersonal2.Checked = false;
                }
            }
            if (e.CommandName == "mail")
            {
                tblMail4.Visible = true;
                tblsms2.Visible = false;
                int EmpID = Convert.ToInt32(e.CommandArgument);

                DataSet ds = AttendanceDAC.HR_GetMailID(EmpID);
                lblMailto4.Text = ds.Tables[0].Rows[0]["name"].ToString();
                lblMailId4.Text = ds.Tables[0].Rows[0]["MailId"].ToString();


            }


        }
        protected void gvUpdateToday_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "sms")
            {
                tblSms3.Visible = true;
                tblMailNew3.Visible = false;
                int EmpID = Convert.ToInt32(e.CommandArgument);

                DataSet ds = AttendanceDAC.HR_EmpMobile(EmpID);
                lblsms3.Text = ds.Tables[0].Rows[0]["name"].ToString();
                txtCompany3.Text = ds.Tables[0].Rows[0]["Mobile2"].ToString();
                txtPersonal3.Text = ds.Tables[0].Rows[0]["Mobile1"].ToString();
                if (txtCompany3.Text == "")
                {
                    rbPersonal3.Checked = true;
                    rbCompany3.Checked = false;
                }
                else { rbCompany3.Checked = true; }
                if (txtPersonal3.Text == "" || txtPersonal3.Text == "0123456789")
                {
                    txtPersonal3.Text = "";
                    rbCompany3.Checked = true;
                }
                if (txtPersonal3.Text != "" && txtCompany3.Text != "")
                {
                    rbCompany3.Checked = true;
                    rbPersonal3.Checked = false;
                }
            }
            if (e.CommandName == "mail")
            {

                tblSms3.Visible = false;
                tblMailNew3.Visible = true;
                int EmpID = Convert.ToInt32(e.CommandArgument);

                DataSet ds = AttendanceDAC.HR_GetMailID(EmpID);
                lblMailto2.Text = ds.Tables[0].Rows[0]["name"].ToString();
                lblMailidto2.Text = ds.Tables[0].Rows[0]["MailId"].ToString();
            }

        }
        protected void rbCompany3_CheckedChanged(object sender, EventArgs e)
        {
            rbPersonal3.Checked = false;
            rbCompany3.Checked = true;
        }
        protected void rbPersonal3_CheckedChanged(object sender, EventArgs e)
        {
            rbPersonal3.Checked = true;
            rbCompany3.Checked = false;
        }
        protected void rbCompany2_CheckedChanged(object sender, EventArgs e)
        {
            rbPersonal2.Checked = false;
            rbCompany2.Checked = true;
        }
        protected void rbPersonal2_CheckedChanged(object sender, EventArgs e)
        {
            rbPersonal2.Checked = true;
            rbCompany2.Checked = false;
        }

        protected void gvUpdated_Sorting(object sender, GridViewSortEventArgs e)
        {
            try
            {
                objSort = (GridSort)ViewState["Sort"];

                DataSet ds = AttendanceDAC.HR_EmpTaskSystem(Convert.ToInt32(Session["CompanyID"]));

                DataView dv = ds.Tables[1].DefaultView;
                dv.Sort = objSort.GetSortExpression(e.SortExpression);
                gvUpdated.DataSource = dv;
                gvUpdated.DataBind();
                ViewState["Sort"] = objSort;
            }
            catch (Exception ex)
            {
            }
        }
        protected void gvUpdaters_Sorting(object sender, GridViewSortEventArgs e)
        {
            try
            {
                objSort = (GridSort)ViewState["Sort"];

                DataSet ds = AttendanceDAC.HR_EmpTaskSystem(Convert.ToInt32(Session["CompanyID"]));
                DataView dv = ds.Tables[0].DefaultView;
                dv.Sort = objSort.GetSortExpression(e.SortExpression);
                gvUpdaters.DataSource = dv;
                gvUpdaters.DataBind();
                ViewState["Sort"] = objSort;
            }
            catch (Exception ex)
            {
            }
        }
        protected void gvNotUpdated_Sorting(object sender, GridViewSortEventArgs e)
        {
            try
            {
                objSort = (GridSort)ViewState["Sort"];

                DataSet ds = AttendanceDAC.HR_EmpTaskSystem(Convert.ToInt32(Session["CompanyID"]));
                DataView dv = ds.Tables[5].DefaultView;
                dv.Sort = objSort.GetSortExpression(e.SortExpression);
                gvNotUpdated.DataSource = dv;
                gvNotUpdated.DataBind();
                ViewState["Sort"] = objSort;
            }
            catch (Exception ex)
            {
            }
        }
        protected void gvNewJoin_Sorting(object sender, GridViewSortEventArgs e)
        {
            try
            {
                objSort = (GridSort)ViewState["Sort"];

                DataSet ds = AttendanceDAC.HR_EmpTaskSystem(Convert.ToInt32(Session["CompanyID"]));

                DataView dv = ds.Tables[3].DefaultView;
                dv.Sort = objSort.GetSortExpression(e.SortExpression);
                gvNewJoin.DataSource = dv;

                gvNewJoin.DataBind();

                ViewState["Sort"] = objSort;

            }
            catch (Exception ex)
            {


            }
        }
        protected void gvUpdateToday_Sorting(object sender, GridViewSortEventArgs e)
        {
            try
            {
                objSort = (GridSort)ViewState["Sort"];

                DataSet ds = AttendanceDAC.HR_EmpTaskSystem(Convert.ToInt32(Session["CompanyID"]));

                DataView dv = ds.Tables[4].DefaultView;
                dv.Sort = objSort.GetSortExpression(e.SortExpression);
                gvUpdateToday.DataSource = dv;
                gvUpdateToday.DataBind();
                ViewState["Sort"] = objSort;

            }
            catch (Exception ex)
            {


            }
        }
        protected void btnMailto4_Click(object sender, EventArgs e)
        {
            try
            {
                string ToMailID = lblMailId4.Text;
                string MailContent = txtContent4.Text;
                SendMailToApplicant(ToMailID, MailContent);
                Response.Redirect("EmpTaskingSystem.aspx?key=3");
            }
            catch (Exception ex2)
            {
                AlertMsg.MsgBox(Page, ex2.Message.ToString(),AlertMsg.MessageType.Error);
            }
        }
        protected void btnMailto2_Click(object sender, EventArgs e)
        {
            try
            {
                string ToMailID = lblMailto2.Text;
                string MailContent = txtMailto2.Text;
                SendMailToApplicant(ToMailID, MailContent);
                Response.Redirect("EmpTaskingSystem.aspx?key=4");
            }
            catch (Exception ex4)
            {
                AlertMsg.MsgBox(Page, ex4.Message.ToString(),AlertMsg.MessageType.Error);
            }
        }
       
    }
}
