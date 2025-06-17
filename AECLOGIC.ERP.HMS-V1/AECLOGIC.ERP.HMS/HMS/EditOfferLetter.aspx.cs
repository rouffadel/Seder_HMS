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
    public partial class EditOfferLetter : AECLOGIC.ERP.COMMON.WebFormMaster
    {

        string sDesignation = "Site Manager";
        double Salary = 12000;

        private string siteurl = System.Configuration.ConfigurationManager.AppSettings["SiteUrl"].ToString();

        string RegEmailID = System.Configuration.ConfigurationManager.AppSettings["RegEmailID"].ToString();
        string SMTPServer = System.Configuration.ConfigurationManager.AppSettings["SMTPServer"].ToString();


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
                    ds = new DataSet();
                    ds = AttendanceDAC.GetWorkSite(0, '1', Convert.ToInt32(Session["CompanyID"]));
                    ddlProject.DataSource = ds.Tables[0];
                    ddlProject.DataTextField = "Site_Name";
                    ddlProject.DataValueField = "Site_ID";
                    ddlProject.DataBind();
                    ds = objApp.GetAppOfferDetails(Convert.ToInt32(Request.QueryString["id"]));
                    //ds = objApp.GetAppDetails(16);

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
                        ddlCategory.SelectedValue = ds.Tables[0].Rows[0]["TradeID"].ToString();
                        ////txtDesig.Text = ds.Tables[0].Rows[0]["Position"].ToString();
                        txtSalary.Text = ds.Tables[0].Rows[0]["Salary"].ToString();

                        txtDOJ.Text = DateTime.Now.ToString(ConfigurationManager.AppSettings["DateFormat"]);

                        //if (ds.Tables[0].Rows[0]["image"].ToString() != "")
                        //imgPhoto.ImageUrl = "./EmpImages/" + Request.QueryString["id"] + "." + ds.Tables[0].Rows[0]["ImageType"].ToString();
                        //imgPhoto.ImageUrl = "./EmpImages/14.jpg";

                        lblDate.Text = "Date : " + DateTime.Now.GetDateTimeFormats()[10];

                        ds = objApp.GetEmpDoc(0, 2, 0);
                        string strText = "";
                        if (ds.Tables[0].Rows.Count == 0)
                        {
                            AlertMsg.MsgBox(Page, "Format Not Found");
                        }
                        else
                        {
                            string StrText = ds.Tables[0].Rows[0]["Value"].ToString().Replace("\"", "").Replace("'", "");//Replace("[designation]", ds.Tables[0].Rows[0]["Designation"].ToString()).Replace("[salary]", Convert.ToDouble(ds.Tables[0].Rows[0]["Salary"]).ToString("#,#0") + "/- CTC ( " + CODEUtility.NumberToText(Convert.ToInt32(ds.Tables[0].Rows[0]["Salary"])) + " Rupees Only)").Replace("[doj]", CODEUtility.Convertdate(ds.Tables[0].Rows[0]["ReqDOJ"].ToString()).ToString()).Replace("[company]", ConfigurationManager.AppSettings["Company"]);

                            TextEditor.InnerHtml = StrText.Replace("%", "");
                            txtRTB.Text = StrText;
                        }



                       
                        //TextEditor.InnerHtml = strText;

                        Page.RegisterStartupScript("ll", "<script language='javascript' type='text/javascript'> LoadDiv();</script>");
                    }
                }
                txtAppDate.Attributes.Add("onblur", "javascript:return ChangeAppDate();");

            }
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {


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
                int DesigID = Convert.ToInt32(ddlDesig.SelectedValue);
                int TradeID = Convert.ToInt32(ddlCategory.SelectedValue);
                objHrCommon.SiteID = Convert.ToInt32(ddlProject.SelectedValue);
                objApp.AddApplicantOfferDetails(objHrCommon, DesigID, TradeID);



                Response.Redirect("ViewOfferLetterList.aspx");
                // SendMailToApplicant();


            }
            catch (Exception ex)
            {

                AlertMsg.MsgBox(Page, "Sorry for the inconvinience. Try again.\nError:" + ex.Message.ToString(),AlertMsg.MessageType.Error);
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
        private void BindDesignations()
        {
            DataSet ds = objApp.GetDesignations();
            ddlDesig.DataSource = ds;
            ddlDesig.DataTextField = "Designation";
            ddlDesig.DataValueField = "DesigId";
            ddlDesig.DataBind();
            ddlDesig.Items.Insert(0, new ListItem("---Select---", "0"));
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("ViewOfferLetterList.aspx");
        }
    }
}