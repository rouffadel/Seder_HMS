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
    public partial class OfferConfirmation : AECLOGIC.ERP.COMMON.WebFormMaster
    {
        string sDesignation = "Site Manager";
        double Salary = 12000;
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
                DataSet ds;
                if (Request.QueryString.Count > 0)
                {
                    ds = new DataSet();
                    ds = AttendanceDAC.GetWorkSite(0, '1', Convert.ToInt32(Session["CompanyID"]));
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

                        //if (ds.Tables[0].Rows[0]["image"].ToString() != "")
                        imgPhoto.ImageUrl = "./ApplicantImages/" + Request.QueryString["id"] + "." + ds.Tables[0].Rows[0]["ImageType"].ToString();
                        //imgPhoto.ImageUrl = "./EmpImages/14.jpg";

                        lblDate.Text = "Date : " + DateTime.Now.GetDateTimeFormats()[10];

                        //ds = objApp.GetEmpDocs(16, 1);

                        string StrText = ds.Tables[0].Rows[0]["OfferLetter"].ToString().Replace("\"", "").Replace("'", "").Replace("[designation]", ds.Tables[0].Rows[0]["Designation"].ToString()).Replace("[salary]", Convert.ToDouble(ds.Tables[0].Rows[0]["Salary"]).ToString("#,#0") + "/- CTC ( " + CODEUtility.NumberToText(Convert.ToInt32(ds.Tables[0].Rows[0]["Salary"])) + " Rupees Only)").Replace("[doj]", ds.Tables[0].Rows[0]["ReqDOJ"].ToString()).Replace("[company]", ConfigurationManager.AppSettings["Company"]);

                        TextEditor.InnerHtml = StrText.Replace("%", "");
                        txtRTB.Text = StrText;
                        //TextEditor.InnerHtml = strText;
                        lblSite.Text = ds.Tables[0].Rows[0]["Site_Name"].ToString();
                        lblAppDate.Text = ds.Tables[0].Rows[0]["ReqDOJ"].ToString();
                        Page.RegisterStartupScript("ll", "<script language='javascript' type='text/javascript'> LoadDiv();</script>");
                    }
                }

            }
        }

        public void lnkaccept_Click(object sender, EventArgs e)
        {
            objHrCommon.AppID = Convert.ToInt32(Request.QueryString["id"]);
            if (txtDOJ.Text != null && txtDOJ.Text != string.Empty)
            {
                objHrCommon.AccDated = CODEUtility.ConvertToDate(txtDOJ.Text.Trim(), DateFormat.DayMonthYear);
            }
            objApp.UpdateOfferStatus(objHrCommon);
            tdaccept.Visible = false;
        }
        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("");
        }
    }
}
