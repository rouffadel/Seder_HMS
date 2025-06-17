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
    public partial class Appointment : AECLOGIC.ERP.COMMON.WebFormMaster
    {

        string sDesignation = "Site Manager";
        double Salary = 12000;
        DataSet ds = new DataSet();
        AttendanceDAC objApp = new AttendanceDAC();
        protected override void OnInit(EventArgs e)
        {
            ModuleID = 1;
            base.OnInit(e);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtAppDate.Text = DateTime.Now.ToString(ConfigurationManager.AppSettings["DateFormat"]);

                if (Request.QueryString.Count > 0)
                {

                    if (Request.QueryString["DocID"].ToString() == "1")
                    {

                        BindWorkSites();
                         ds = objApp.GetAppDetails(Convert.ToInt32(Request.QueryString["id"]));

                        if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
                        {
                            lblName.Text = ds.Tables[0].Rows[0]["name"].ToString();
                            lblAddress.Text = ds.Tables[0].Rows[0]["ResAddress"].ToString().Replace("\n", "<br/>");
                            lblCity.Text = ds.Tables[0].Rows[0]["ResCity"].ToString();
                            lblState.Text = ds.Tables[0].Rows[0]["ResState"].ToString();
                            lblCountry.Text = ds.Tables[0].Rows[0]["ResCountry"].ToString(); ;
                            lblPin.Text = " - " + ds.Tables[0].Rows[0]["ResPIN"].ToString(); ;
                            lblPhone.Text = ds.Tables[0].Rows[0]["ResPhone"].ToString();
                            if (ds.Tables[0].Rows[0]["image"].ToString() != "")
                                imgPhoto.ImageUrl = "./EmpImages/" + Request.QueryString["id"] + "." + ds.Tables[0].Rows[0]["image"].ToString();
                            lblDate.Text = "Date : " + Convert.ToDateTime(ds.Tables[0].Rows[0]["DOJ"]).GetDateTimeFormats()[10];//DateTime.Now.GetDateTimeFormats()[10];
                            txtAppDate.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["DOJ"]).ToString(ConfigurationManager.AppSettings["DateFormat"]);

                            lblDesig.Text = ds.Tables[0].Rows[0]["designation"].ToString();
                        
                            txtSalary.Text = ds.Tables[0].Rows[0]["salary"].ToString();
                            txtDOJ.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["DOJ"]).ToString(ConfigurationManager.AppSettings["DateFormat"]);
                            ddlProject.SelectedValue = ds.Tables[0].Rows[0]["SiteID"].ToString();
                            ds = objApp.GetEmpDoc(Convert.ToInt32(Request.QueryString["id"]), Convert.ToInt32(Request.QueryString["DocID"]), Convert.ToInt32(Request.QueryString["EmpDocID"]));
                            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                            {
                                string strText = ds.Tables[0].Rows[0]["value"].ToString();
                                txtRTB.Text = strText;
                            }
                            else
                            {
                                Response.Redirect("EmployeeList.aspx");
                            }

                            Page.RegisterStartupScript("ll", "<script language='javascript' type='text/javascript'> LoadDiv();</script>");
                        }
                        else
                        {
                            int id = Convert.ToInt32(Request.QueryString["id"]);
                            Page.RegisterStartupScript("ll", "<script language='javascript' type='text/javascript'> ChkAppDetails(" + Request.QueryString["id"] + ");</script>");

                        }


                    }

                    else
                    {
                        int EmpID = Convert.ToInt32(Request.QueryString["id"].ToString());
                        int DocID = Convert.ToInt32(Request.QueryString["DocID"].ToString());
                        string DocName = Request.QueryString["DocName"].ToString();
                        int EmpDocID = Convert.ToInt32(Request.QueryString["EmpDocID"].ToString());
                        Response.Redirect("EditgenEmpDocuments.aspx?EmpID=" + EmpID + "&DocID=" + DocID + "&DocName=" + DocName + "&EmpDocID=" + EmpDocID);

                    }
                    txtAppDate.Attributes.Add("onblur", "javascript:return ChangeAppDate();");
                }
            }
        }
        private void BindWorkSites()
        {
            ds = AttendanceDAC.GetWorkSite(0, '1', Convert.ToInt32(Session["CompanyID"]));
            ddlProject.DataSource = ds.Tables[0];
            ddlProject.DataTextField = "Site_Name";
            ddlProject.DataValueField = "Site_ID";
            ddlProject.DataBind();
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet dsEmpDetails = objApp.GetAppDetails(Convert.ToInt32(Request.QueryString["id"]));
                string str = txtRTB.Text.Replace("\"", "").Replace("'", "");

                string StrText = txtRTB.Text.Replace("[designation]".ToUpper(), lblDesig.Text).Replace("[salary]".ToUpper(), txtSalary.Text + " ( " + CODEUtility.NumberToText(Convert.ToInt32(txtSalary.Text)) + " Rupees Only)").Replace("[doj]".ToUpper(), CODEUtility.ConvertToDate(txtDOJ.Text.Trim(), DateFormat.DayMonthYear).ToShortDateString())

                    .Replace("[WORKSITE]", dsEmpDetails.Tables[0].Rows[0]["Site_Name"].ToString()).Replace("[DEPARTMENT]", dsEmpDetails.Tables[0].Rows[0]["DepartmentName"].ToString()).Replace("[COMPANY NAME]", ConfigurationSettings.AppSettings["Company"].ToString()).Replace("[DATE]", DateTime.Now.ToString("dd/MM/yyyy"));
                objApp.AddUpdateEmpDocs(Convert.ToInt32(Request.QueryString["id"]), Convert.ToInt32(Request.QueryString["DocID"]), 1, StrText, Convert.ToInt32(ddlProject.SelectedValue), lblDesig.Text, CODEUtility.ConvertToDate(txtDOJ.Text.Trim(), DateFormat.DayMonthYear), Convert.ToInt32(txtSalary.Text), CODEUtility.ConvertToDate(txtAppDate.Text, DateFormat.DayMonthYear));
                //objApp.AddUpdateEmpDocs(16, 1, 1, str, Convert.ToInt32(ddlProject.SelectedValue), txtDesig.Text, CODEUtility.Convertdate(txtDOJ.Text), Convert.ToInt32(txtSalary.Text));
                txtRTB.Text = str;
                Response.Redirect("EmployeeList.aspx");
            }
            catch (Exception ex)
            {
                AlertMsg.MsgBox(Page, "Sorry for the inconvinience. Try again.\nError:" + ex.Message.ToString(),AlertMsg.MessageType.Error);
            }
        }
        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("EmployeeList.aspx");
        }
    }
}