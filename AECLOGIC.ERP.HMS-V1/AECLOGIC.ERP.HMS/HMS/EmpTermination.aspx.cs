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
using System.Web.Mail;
using System.Net.Mime;
using System.Configuration;
using System.Collections;
using AECLOGIC.ERP.COMMON;
namespace AECLOGIC.ERP.HMS
{
    public partial class EmpTermination : AECLOGIC.ERP.COMMON.WebFormMaster
    {
        AttendanceDAC objAtt = new AttendanceDAC();
        int mid = 0;
        bool viewall;
        static int WSId=0;
        static int DeptId = 0;
        static char Staus = '1';
        string menuname;
        static int CompanyID=Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["CompanyID"].ToString());
       // static int CompanyID;
        static int Siteid;
        string menuid;
        private string siteurl = System.Configuration.ConfigurationManager.AppSettings["SiteUrl"].ToString();
        string RegEmailID = System.Configuration.ConfigurationManager.AppSettings["RegEmailID"].ToString();
        string SMTPServer = System.Configuration.ConfigurationManager.AppSettings["SMTPServer"].ToString();
        string WebSiteID = System.Configuration.ConfigurationManager.AppSettings["WebSiteID"].ToString();
        string Company = System.Configuration.ConfigurationManager.AppSettings["Company"].ToString();
        string SMSUserID = System.Configuration.ConfigurationManager.AppSettings["SMSUserID"].ToString();
        string SMSPassword = System.Configuration.ConfigurationManager.AppSettings["SMSPassword"].ToString();
        HRCommon objHrCommon = new HRCommon();
        protected override void OnInit(EventArgs e)
        {
            ModuleID = 1;
            base.OnInit(e);
            #region Approvedpaging
            EmpTerminationPaging.FirstClick += new Paging.PageFirst(EmpTerminationPaging_FirstClick);
            EmpTerminationPaging.PreviousClick += new Paging.PagePrevious(EmpTerminationPaging_FirstClick);
            EmpTerminationPaging.NextClick += new Paging.PageNext(EmpTerminationPaging_FirstClick);
            EmpTerminationPaging.LastClick += new Paging.PageLast(EmpTerminationPaging_FirstClick);
            EmpTerminationPaging.ChangeClick += new Paging.PageChange(EmpTerminationPaging_FirstClick);
            EmpTerminationPaging.ShowRowsClick += new Paging.ShowRowsChange(EmpTerminationPaging_ShowRowsClick);
            EmpTerminationPaging.CurrentPage = 1;
            #endregion Approvedpaging
        }
        #region Paging
        void EmpTerminationPaging_ShowRowsClick(object sender, EventArgs e)
        {
            EmpTerminationPaging.CurrentPage = 1;
            BindPager();
        }
        void EmpTerminationPaging_FirstClick(object sender, EventArgs e)
        {
            if (hdnSearchChange.Value == "1")
                EmpTerminationPaging.CurrentPage = 1;
            BindPager();
            hdnSearchChange.Value = "0";
        }
        void BindPager()
        {
            objHrCommon.PageSize = EmpTerminationPaging.ShowRows;
            objHrCommon.CurrentPage = EmpTerminationPaging.CurrentPage;
            //EmpReimburseStatusByEmpIDByPaging(objHrCommon, EmpID);
            SearchEmpList(objHrCommon);
        }
        #endregion Paging
        protected void Page_Load(object sender, EventArgs e)
        {
            EmpTerminationPaging.Visible = false;
            if (Request.QueryString["EmpID"] != null && Request.QueryString["EmpID"] != string.Empty)
            {
                ViewState["ID"] = Convert.ToInt32(Request.QueryString["EmpID"].ToString());
            }
            try
            {
                tblEmpResult.Visible = true;
                tblTerminate.Visible = false;
                grdEmployees.Visible = true;
                Label1.Visible = false;
                if (!IsPostBack)
                {
                    GetParentMenuId();
                    tblSearch.Visible = true;
                    if (Request.QueryString.Count > 0)
                    {
                        BindPager();
                    }
                    ViewState["EmpID"] = 0;
                }
            }
            catch
            {
            }
        }
        public int GetParentMenuId()
        {
            string URL = Request.Url.Segments[Request.Url.Segments.Length - 1] + Request.Url.Query;
            int RoleId = Convert.ToInt32(Session["RoleId"].ToString());
            int ModuleId = ModuleID;;
          DataSet  ds = AttendanceDAC.GetAllowed(RoleId, ModuleId, URL);
            int MenuId = 0;
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                MenuId = Convert.ToInt32(ds.Tables[0].Rows[0]["Under"]);
                btnSMSMail.Enabled = btnTerminate.Enabled = (bool)ds.Tables[0].Rows[0]["Editable"];
            }
            return MenuId;
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
        public void rbSelect_CheckedChanged(Object sender, EventArgs e)
        {
            tblSearch.Visible = false;
            tblEmpResult.Visible = false;
            grdEmployees.Visible = false;
            tblTerminate.Visible = true;
            RadioButton chk = (RadioButton)sender;
            GridViewRow gvr = (GridViewRow)(((Control)sender).NamingContainer);
            Label txt = (Label)gvr.Cells[1].FindControl("lblempid");
            if (chk.Checked == true)
            {
                int EmpID = Convert.ToInt32(txt.Text);
                ViewState["EmpID"] = EmpID;
                DataSet ds = AttendanceDAC.HR_EmpTermination(EmpID);
                dvTermination.DataSource = ds;
                dvTermination.DataBind();
                chk.Checked = false;
                if (ds.Tables[0].Rows.Count > 0)
                {
                    tblTerminate.Visible = true;
                }
                else
                {
                    //tblTerminate.Visible = false;
                    AlertMsg.MsgBox(Page, "no records found");
                }
            }
        }
        void SearchEmpList(HRCommon objHrCommon)
        {
            int EmpID = 0;
            int DeptId = 0;
            int SiteID = 0;
            Label1.Visible = true;
            try
            {
                if (txtempid.Text.Trim() == String.Empty)
                {
                    EmpID = 0;
                }
                else
                {
                        EmpID = Convert.ToInt32(txtempid.Text);
                }
                if (WSId != 0)
                    SiteID = Convert.ToInt32(WSId);
                if (txtSearchdept.Text != "")
                    DeptId = Convert.ToInt32(ddldepartments_hid.Value == "" ? "0" : ddldepartments_hid.Value);
                else
                    DeptId = 0;
                //double DeptID = Convert.ToDouble(ddldepartments.SelectedItem.Value);
                string FName = "";
                if (txtusername.Text != "")
                    FName = txtusername.Text;
              DataSet ds = AttendanceDAC.SearchEmpListByPaging(objHrCommon, SiteID, DeptId, FName, EmpID, Convert.ToInt32(Session["CompanyID"]));
                if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0)
                {
                    grdEmployees.DataSource = ds;
                  EmpTerminationPaging.Bind(objHrCommon.CurrentPage, objHrCommon.TotalPages, objHrCommon.NoofRecords, objHrCommon.PageSize);
                }
                else
                {
                    grdEmployees.EmptyDataText = "No Records Found";
                    EmpTerminationPaging.Visible = false;
                    Label1.Visible = false;
                }
                grdEmployees.DataBind();
            }
            catch
            {
            }
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            EmpTerminationPaging.CurrentPage = 1;
            BindPager();
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute]
        public static string[] GetCompletionList(string prefixText, int count, string contextKey)
        {
            WSId = 0;
            DataSet ds = AttendanceDAC.GetWorkSite_by_Wsid(prefixText, WSId, Staus, CompanyID);
            DataTable dt = ds.Tables[0];
            List<string> items = new List<string>(count);
            var rtval = new Dictionary<string, string>();
            foreach (DataRow row in dt.Rows)
            {
                string str = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(row["Name"].ToString(), row["Id"].ToString());
                items.Add(str);
            }
            return items.ToArray(); ;// txtItems.ToArray()
        }
         [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute]
        public static string[] GetCompletionListemp(string prefixText, int count, string contextKey)
        {
            DataSet ds = AttendanceDAC.GetSearchEmpName_dept(prefixText);
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
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionListdept(string prefixText, int count, string contextKey)
        {
            DataSet ds = AttendanceDAC.BindDeparmetBySite_googlesearch(prefixText, WSId, CompanyID);
            DataTable dt = ds.Tables[0];
            List<string> items = new List<string>(count);
            var rtval = new Dictionary<string, string>();
            foreach (DataRow row in dt.Rows)
            {
                string str = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(row["DepartmentName"].ToString(), row["DepartmentUId"].ToString());
                items.Add(str);
            }
            return items.ToArray(); ;// txtItems.ToArray()
        }
        protected void Worksidechangewithdep(object sender, EventArgs e)
       {
           CompanyID = Convert.ToInt32(Session["CompanyID"]);
           WSId = Convert.ToInt32(ddlworksites_hid.Value == "" ? "0" : ddlworksites_hid.Value); ;
        }
        protected void GetDepartmentSearch(object sender, EventArgs e)
        {
            DeptId = Convert.ToInt32(ddldepartments_hid.Value == "" ? "0" : ddldepartments_hid.Value); ;
        }  
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            tblSearch.Visible = true;
            tblTerminate.Visible = false;
            grdEmployees.Visible = true;
            tblEmpResult.Visible = true;
            Label1.Visible = true;
        }
        protected void btnSMSMail_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow gvr in gvSMSMailEmp.Rows)
            {
                CheckBox chkSmsEmp = (CheckBox)gvr.Cells[4].FindControl("chkSmsEmp");
                CheckBox chkMailEmp = (CheckBox)gvr.Cells[5].FindControl("chkMailEmp");
                if (chkSmsEmp.Checked == true)
                {
                    Label txt = (Label)gvr.Cells[0].FindControl("lblempid");
                    int EmpID = Convert.ToInt32(txt.Text);
                   DataSet ds = AttendanceDAC.HR_EmpTermination(EmpID);
                    try
                    {
                        AttendanceDAC.HMS_EmpToTerminate(EmpID,  Convert.ToInt32(Session["UserId"]));
                    }
                    catch (Exception EmpToTerminate)
                    {
                        AlertMsg.MsgBox(Page, EmpToTerminate.ToString());
                    }
                    string mobileno = ds.Tables[0].Rows[0]["Mobile"].ToString();
                    SMSCAPI m = new SMSCAPI();
                    string msg = "your services are terminated  due to not delivering duties and responsibilities as desired by Management.";
                    if (mobileno != "" && mobileno != "0123456789")
                    {
                        m.SendSMS(SMSUserID, SMSPassword, mobileno, msg);
                    }
                }
                if (chkMailEmp.Checked == true)
                {
                    Label txt = (Label)gvr.Cells[0].FindControl("lblempid");
                    int EmpID = Convert.ToInt32(txt.Text);
                    DataSet ds = AttendanceDAC.HR_EmpTermination(EmpID);
                    String ToMailID = ds.Tables[0].Rows[0]["Mailid"].ToString();
                    String MailContent = "your services are terminated due to not delivering duties and responsibilities as desired by Management.";
                    if (ToMailID != "")
                    {
                        SendMailToEmp(ToMailID, MailContent);
                    }
                }
            }
            foreach (GridViewRow gvr in gvSMSMailHead.Rows)
            {
                CheckBox chkSmsHead = (CheckBox)gvr.Cells[4].FindControl("chkSmsHead");
                CheckBox chkMailHead = (CheckBox)gvr.Cells[5].FindControl("chkMailHead");
                if (chkSmsHead.Checked == true)
                {
                    Label txt = (Label)gvr.Cells[0].FindControl("lblempid");
                    int EmpID = Convert.ToInt32(txt.Text);
                    int Emp = Convert.ToInt32(ViewState["EmpID"]);
                    DataSet ds = AttendanceDAC.HR_EmpTermination(Emp);
                    DataSet dsHead = AttendanceDAC.HR_EmpTermination(EmpID);
                    string mobileHead = dsHead.Tables[0].Rows[0]["Mobile"].ToString();
                    string msgToHead = String.Format("E{0} [{1}] terminated -By {2}", Emp.ToString() + "_" + ds.Tables[0].Rows[0]["name"].ToString().Replace(" ", "_"), ds.Tables[0].Rows[0]["DepartmentName"].ToString().Replace(" ", "_"), "E" +  Convert.ToInt32(Session["UserId"]).ToString());
                    //string msgToHead = "The Employee by name " + ds.Tables[0].Rows[0]["name"].ToString() + " Has Terminated from Services of " + Company + "";
                    SMSCAPI m = new SMSCAPI();
                    if (mobileHead != "")
                    {
                        m.SendSMS(SMSUserID, SMSPassword, mobileHead, msgToHead);
                    }
                }
                if (chkMailHead.Checked == true)
                {
                    Label txt = (Label)gvr.Cells[0].FindControl("lblempid");
                    int EmpID = Convert.ToInt32(txt.Text);
                    int Emp = Convert.ToInt32(ViewState["EmpID"]);
                    DataSet ds = AttendanceDAC.HR_EmpTermination(Emp);
                    DataSet dsHead = AttendanceDAC.HR_EmpTermination(EmpID);
                    string ToMail = dsHead.Tables[0].Rows[0]["Mailid"].ToString();
                    string ContentMail = "The Employee by name " + ds.Tables[0].Rows[0]["name"].ToString() + " Has Terminated from Services of " + Company + "";
                    if (ToMail != "")
                    {
                        SendMailToHead(ToMail, ContentMail);
                    }
                }
            }
            foreach (GridViewRow gvr in gvSMSMailAccDept.Rows)
            {
                CheckBox chkSmsAcc = (CheckBox)gvr.Cells[4].FindControl("chkSmsAcc");
                CheckBox chkMailAcc = (CheckBox)gvr.Cells[5].FindControl("chkMailAcc");
                if (chkSmsAcc.Checked == true)
                {
                    using (DataSet dsAcc = AttendanceDAC.HR_InformToAccDept())
                    {
                        if (dsAcc != null && dsAcc.Tables.Count > 0 && dsAcc.Tables[0].Rows.Count > 0)
                            foreach (DataRow dr in dsAcc.Tables[0].Rows)
                            {
                                int Emp = Convert.ToInt32(ViewState["EmpID"]);
                                DataSet ds = AttendanceDAC.HR_EmpTermination(Emp);
                                int HeadEmpID = Convert.ToInt32(dr["EmpId"].ToString());
                                DataSet dsAccHead = AttendanceDAC.HR_EmpTermination(HeadEmpID);
                                string AccHeadMobile = dsAccHead.Tables[0].Rows[0]["Mobile"].ToString();
                                int SiteID = Convert.ToInt32(ds.Tables[0].Rows[0]["Categary"].ToString());
                                DataSet dsSite = AttendanceDAC.GetWorkSite(SiteID, '1', Convert.ToInt32(Session["CompanyID"]));
                                //E{0}_{1} [{1}/{2}] terminated, plz settle A/c-{4}
                                string msgToAccHead = String.Format("E{0} [{1}/{2}] terminated, plz settle A/c-{3}", Emp.ToString() + "_" + ds.Tables[0].Rows[0]["name"].ToString().Replace(" ", "_"), ds.Tables[0].Rows[0]["DepartmentName"].ToString().Replace(" ", "_"), dsSite.Tables[0].Rows[0]["Site_Name"].ToString().Replace(" ", "_"), "E" +  Convert.ToInt32(Session["UserId"]).ToString());
                                //string msgToAccHead = "The Employee by name " + ds.Tables[0].Rows[0]["name"].ToString() + " from: " + ds.Tables[0].Rows[0]["DepartmentName"].ToString() + " Department of " + dsSite.Tables[0].Rows[0]["Site_Name"].ToString() + " Site, Has Terminated from Services of" + Company + ", Please settele his account";
                                SMSCAPI m = new SMSCAPI();
                                m.SendSMS(SMSUserID, SMSPassword, AccHeadMobile, msgToAccHead);
                            }
                    }
                }
                if (chkMailAcc.Checked == true)
                {
                    using (DataSet dsAcc = AttendanceDAC.HR_InformToAccDept())
                    {
                        if (dsAcc != null && dsAcc.Tables.Count > 0 && dsAcc.Tables[0].Rows.Count > 0)
                            foreach (DataRow dr in dsAcc.Tables[0].Rows)
                            {
                                int Emp = Convert.ToInt32(ViewState["EmpID"]);
                                DataSet ds = AttendanceDAC.HR_EmpTermination(Emp);
                                int HeadEmpID = Convert.ToInt32(dr["EmpId"].ToString());
                                DataSet dsAccHead = AttendanceDAC.HR_EmpTermination(HeadEmpID);
                                int SiteID = Convert.ToInt32(ds.Tables[0].Rows[0]["Categary"].ToString());
                                DataSet dsSite = AttendanceDAC.GetWorkSite(SiteID, '1', Convert.ToInt32(Session["CompanyID"]));
                                string ToMailIDofAccDept = dsAccHead.Tables[0].Rows[0]["Mailid"].ToString();
                                string MailContentToAccDept = "This is to Inform that The Employee by name " + ds.Tables[0].Rows[0]["name"].ToString() + "from" + ds.Tables[0].Rows[0]["DepartmentName"].ToString() + " Department of" + dsSite.Tables[0].Rows[0]["Site_Name"].ToString() + "Site, Has Terminated from Services of " + Company + ", Please Settele his Account";
                                SendMailToAccDept(ToMailIDofAccDept, MailContentToAccDept);
                            }
                    }
                }
            }
            int EmpId = Convert.ToInt32(ViewState["EmpID"]);
            try
            {
                AttendanceDAC.HMS_EmpToTerminate(EmpId,  Convert.ToInt32(Session["UserId"]));
            }
            catch (Exception exEmpTer)
            {
                AlertMsg.MsgBox(Page, exEmpTer.ToString());
                return;
            }
            Response.Redirect("CreateGenEmpdocuments.aspx?EmpId=" + EmpId + "&DocID=" + 2 + "&DocName=" + "Termination Letter");
            //Response.Redirect("TerminationLetterPreview.aspx?EmpID=" + EmpId);
        }
        protected void btnTerminate_Click(object sender, EventArgs e)
        {
            tblTerminate.Visible = false;
            tblSearch.Visible = false;
            tblEmpResult.Visible = false;
            btnTerminate.Visible = false;
            tblSMSMail.Visible = true;
            int EmpId = Convert.ToInt32(ViewState["EmpID"]);
            DataSet ds1 = AttendanceDAC.HR_EmpTermination(EmpId);
            if (ds1.Tables[0].Rows.Count > 0)
            {
                if (ds1.Tables[0].Rows[0]["Mgnr"].ToString() != "")
                {
                    int DeptHead = Convert.ToInt32(ds1.Tables[0].Rows[0]["Mgnr"]);
                    DataSet ds2 = AttendanceDAC.HR_HeadInfoOnEmpTermination(DeptHead);
                    gvSMSMailHead.DataSource = ds2;
                    gvSMSMailHead.DataBind();
                }
                else
                {
                }
                DataSet ds3 = AttendanceDAC.HR_InformToAccDept();
                gvSMSMailEmp.DataSource = ds1;
                gvSMSMailEmp.DataBind();
                gvSMSMailAccDept.DataSource = ds3;
                gvSMSMailAccDept.DataBind();
            }
            else
            {
                AlertMsg.MsgBox(Page, "there is no records");
            }
            DateTime IssueDate = DateTime.Now;
            string DocName = "Termination Letter";
            int EmpDocID = 0;
            string value = "";
            try
            {
                AttendanceDAC.AddUpdateEmpDocsGeneral(EmpId, 2, 1, value, IssueDate, DocName, EmpDocID);
            }
            catch (Exception ex)
            {
                AlertMsg.MsgBox(Page, ex.Message.ToString(),AlertMsg.MessageType.Error);
            }
        }
        public void SendMailToEmp(string ToMailID, string MailContent)//string HeadMail wrote by hari 
        {
            try
            {
                // ToMailID = string.Empty;
                MailMessage msgMail = new MailMessage();
                msgMail.To = ToMailID;
                msgMail.From = RegEmailID;
                msgMail.Subject = "Termination Letter";
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
                bodyMessage = bodyMessage + "<td style='font-size:16px; font-weight:bold; color:#666666; padding-left:34px;line-height:29px;color:#ffffff'> Termination From " + Company + "</td>";
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
                bodyMessage = bodyMessage + "<p style='padding:0px 0px 15px 0px; margin:0px; font-family:Arial, Helvetica, sans-serif; font-size:12px; color:#333333;'>From,<br />" + Company + "<br /><a href='" + WebSiteID + "' target='_blank'>" + WebSiteID + "</a></p>";
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
            catch (Exception ex)
            {
                AlertMsg.MsgBox(Page, ex.Message.ToString(),AlertMsg.MessageType.Error);
            }
        }
        public void SendMailToHead(string ToMail, string ContentMail)
        {
            try
            {
                //string ToMailID = string.Empty;
                MailMessage msgMail = new MailMessage();
                msgMail.To = ToMail;
                msgMail.From = RegEmailID;
                msgMail.Subject = "Information Letter";
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
                bodyMessage = bodyMessage + "<td style='font-size:16px; font-weight:bold; color:#666666; padding-left:34px;line-height:29px;color:#ffffff'>" + Company + "</td>";
                bodyMessage = bodyMessage + "</tr>";
                bodyMessage = bodyMessage + "</table></td>";
                bodyMessage = bodyMessage + "</tr>";
                bodyMessage = bodyMessage + "<tr>";
                bodyMessage = bodyMessage + "<td background=" + imagebackground + ">";
                bodyMessage = bodyMessage + "<table width='100%' border='0' cellspacing='0' cellpadding='0'>";
                bodyMessage = bodyMessage + "<tr valign='top'>";
                bodyMessage = bodyMessage + "<td style='padding:10px'>";
                bodyMessage = bodyMessage + "<p style='padding:0px 0px 15px 0px; margin:0px; font-family:Arial, Helvetica, sans-serif; font-size:12px; color:#333333;'> " + ContentMail + ",</p>";
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
            catch (Exception exToHead)
            {
                AlertMsg.MsgBox(Page, exToHead.Message.ToString(),AlertMsg.MessageType.Error);
            }
        }
        public void SendMailToAccDept(string ToMailIDofAccDept, string MailContentToAccDept)
        {
            try
            {
                //string ToMailID = string.Empty;
                MailMessage msgMail = new MailMessage();
                msgMail.To = ToMailIDofAccDept;
                msgMail.From = RegEmailID;
                msgMail.Subject = "Information Letter";
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
                bodyMessage = bodyMessage + "</tr>";
                bodyMessage = bodyMessage + "</table></td>";
                bodyMessage = bodyMessage + "</tr>";
                bodyMessage = bodyMessage + "<tr>";
                bodyMessage = bodyMessage + "<td background=" + imagebackground + ">";
                bodyMessage = bodyMessage + "<table width='100%' border='0' cellspacing='0' cellpadding='0'>";
                bodyMessage = bodyMessage + "<tr valign='top'>";
                bodyMessage = bodyMessage + "<td style='padding:10px'>";
                bodyMessage = bodyMessage + "<p style='padding:0px 0px 15px 0px; margin:0px; font-family:Arial, Helvetica, sans-serif; font-size:12px; color:#333333;'> " + MailContentToAccDept + ",</p>";
                bodyMessage = bodyMessage + "<p style='padding:0px 0px 15px 0px; margin:0px; font-family:Arial, Helvetica, sans-serif; font-size:12px; color:#333333;'>Thank  you.</p>";
                bodyMessage = bodyMessage + "<p style='padding:0px 0px 15px 0px; margin:0px; font-family:Arial, Helvetica, sans-serif; font-size:12px; color:#333333;'>From,<br />" + Company + "<br /><a href='" + WebSiteID + "' target='_blank'>" + WebSiteID + "</a></p>";
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
            catch (Exception exToAccDept)
            {
                AlertMsg.MsgBox(Page, exToAccDept.Message.ToString(),AlertMsg.MessageType.Error);
            }
        }
    }
}
