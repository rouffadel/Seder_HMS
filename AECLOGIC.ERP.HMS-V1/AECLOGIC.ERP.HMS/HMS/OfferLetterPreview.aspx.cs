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
using System.Text.RegularExpressions;

namespace AECLOGIC.ERP.HMS
{
    public partial class OfferLetterPreview : WebFormMaster
    {
        AttendanceDAC objApp = new AttendanceDAC();
        HRCommon objHrCommon = new HRCommon();
        static string strurl = string.Empty;
        protected override void OnInit(EventArgs e)
        {
          //  ModuleID = 1;
            base.OnInit(e);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataSet ds;
                // strurl = Request.UrlReferrer.ToString();
                if (Request.QueryString.Count > 0)
                {
                    ds = new DataSet();
                    ds = AttendanceDAC.GetWorkSite(0, '1', Convert.ToInt32(Session["CompanyID"]));

                    ds = objApp.GetAppOfferDetails(Convert.ToInt32(Request.QueryString["id"]));

                    if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
                    {
                        lblName.Text = ds.Tables[0].Rows[0]["name"].ToString();
                        lblAddress.Text = ds.Tables[0].Rows[0]["Address"].ToString();
                        lblCity.Text = ds.Tables[0].Rows[0]["City"].ToString();
                        lblState.Text = ds.Tables[0].Rows[0]["State"].ToString();
                        lblCountry.Text = ds.Tables[0].Rows[0]["Country"].ToString(); ;
                        lblPin.Text = " - " + ds.Tables[0].Rows[0]["pin"].ToString(); ;
                        lblPhone.Text = ds.Tables[0].Rows[0]["Phone"].ToString();

                        imgPhoto.ImageUrl = "./ApplicantImages/" + Request.QueryString["id"] + "." + ds.Tables[0].Rows[0]["ImageType"].ToString();

                        lblDate.Text = "Date : " + DateTime.Now.GetDateTimeFormats()[10];

                       

                        string StrText = ds.Tables[0].Rows[0]["OfferLetter"].ToString();


                        string str1 = StrText.Replace("COMPANY NAME", ConfigurationManager.AppSettings["Company"]).Replace("DESIGNATION", ds.Tables[0].Rows[0]["Designation"].ToString()).Replace("SALARY", Convert.ToDouble(ds.Tables[0].Rows[0]["Salary"]).ToString("#,#0") + "/- CTC( " + CODEUtility.NumberToText(Convert.ToInt32(ds.Tables[0].Rows[0]["Salary"]))).Replace("doj", ds.Tables[0].Rows[0]["ReqDOJ"].ToString()).Replace("[", "").Replace("]", "");


                        TextEditor.InnerHtml = str1.Replace("%", "");
                        txtRTB.Text = str1;

                        lblSite.Text = ds.Tables[0].Rows[0]["Site_Name"].ToString() + "<br/>" + ds.Tables[0].Rows[0]["Site_Address"].ToString();


                        lblAppDate.Text = ds.Tables[0].Rows[0]["ReqDOJ"].ToString();
                        Page.RegisterStartupScript("ll", "<script language='javascript' type='text/javascript'> LoadDiv();</script>");
                    }
                }


            }
        }



        protected void Button1_Click(object sender, EventArgs e)
        {
            //Response.Write("<script language='javascript'> { window.close();}</script>");
            //Button3.Attributes.Add("onclick", "window.close();");
            // Response.Redirect(strurl);
        }
        private static string ExtractHtmlInnerText(string htmlText)
        {
            //Match any Html tag (opening or closing tags) 
            // followed by any successive whitespaces
            //consider the Html text as a single line

            Regex regex = new Regex("(<.*?>\\s*)+", RegexOptions.Singleline);

            // replace all html tags (and consequtive whitespaces) by spaces
            // trim the first and last space

            string resultText = regex.Replace(htmlText, " ").Trim().Split(' ')[0];

            return resultText;
        }
        //private static string ReplaceHtmlInnerText(string htmlText)
        //{
        //    // Get first word.
        //    string firstWord = ExtractHtmlInnerTextFirstWord(htmlText);

        //    // Add span around first character of first word.
        //    string replacedFirstWord = firstWord.Replace(firstWord[0].ToString(), "<span class=\"my-style\">" + firstWord[0] + "</span>");

        //    // Replace only first occurrence of word.
        //    var regex = new Regex(Regex.Escape(firstWord));
        //    string replacedText = regex.Replace(htmlText, replacedFirstWord, 1);

        //    return replacedText;
        //}
    }
}
