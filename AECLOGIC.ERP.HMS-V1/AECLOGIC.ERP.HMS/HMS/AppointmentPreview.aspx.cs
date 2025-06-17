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
    public partial class AppointmentPreview : AECLOGIC.ERP.COMMON.WebFormMaster
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                AttendanceDAC objApp = new AttendanceDAC();
                if (Request.QueryString["DocID"].ToString() == "1")
                {
                    DataSet ds = objApp.GetAppDetails(Convert.ToInt32(Request.QueryString["id"]));
                    lblEmpID.Text = ds.Tables[0].Rows[0]["lblEmpID"].ToString();
                    lblName.Text = ds.Tables[0].Rows[0]["name"].ToString();
                    lblAddress.Text = ds.Tables[0].Rows[0]["ResAddress"].ToString().Replace("\n", "<br/>");
                    lblCity.Text = ds.Tables[0].Rows[0]["ResCity"].ToString();
                    lblState.Text = ds.Tables[0].Rows[0]["ResState"].ToString();
                    lblCountry.Text = ds.Tables[0].Rows[0]["ResCountry"].ToString();
                    if (ds.Tables[0].Rows[0]["ResPIN"].ToString().Trim() != String.Empty)
                        lblPin.Text = " - " + ds.Tables[0].Rows[0]["ResPIN"].ToString();
                    lblPhone.Text = ds.Tables[0].Rows[0]["ResPhone"].ToString();

                    if (ds.Tables[0].Rows[0]["image"].ToString() != "")
                        imgPhoto.ImageUrl = "./EmpImages/" + Request.QueryString["id"] + "." + ds.Tables[0].Rows[0]["image"].ToString();
                    lblDoj.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["doj"]).GetDateTimeFormats()[10];

                    lblSite.Text = ds.Tables[0].Rows[0]["categary"].ToString();
                    DataSet dsApp = objApp.GetEmpDoc(Convert.ToInt32(Request.QueryString["id"]), Convert.ToInt32(Request.QueryString["DocID"]), Convert.ToInt32(Request.QueryString["EmpDocID"]));
                    lblDate.Text = "Date : " + Convert.ToDateTime(dsApp.Tables[0].Rows[0]["IssueDate"]).GetDateTimeFormats()[10];
                    lblAppDate.Text = Convert.ToDateTime(dsApp.Tables[0].Rows[0]["IssueDate"]).GetDateTimeFormats()[10];

                    string StrText = dsApp.Tables[0].Rows[0]["value"].ToString().Replace("\"", "").Replace("'", "").Replace("[designation]", ds.Tables[0].Rows[0]["Designation"].ToString()).Replace("[salary]", Convert.ToDouble(ds.Tables[0].Rows[0]["salary"]).ToString("#,#0") + "/- CTC ( " + CODEUtility.NumberToText(Convert.ToInt32(ds.Tables[0].Rows[0]["salary"])) + " Rupees Only)").Replace("[doj]", Convert.ToDateTime(ds.Tables[0].Rows[0]["DOJ"]).GetDateTimeFormats()[10]).Replace("[company]", ConfigurationManager.AppSettings["Company"]);

                    TextEditor.InnerHtml = StrText.Replace("%", "");
                }
                else
                {
                    int EmpID = Convert.ToInt32(Request.QueryString["id"].ToString());
                    int DocID = Convert.ToInt32(Request.QueryString["DocID"].ToString());
                    int EmpDocID = Convert.ToInt32(Request.QueryString["EmpDocID"].ToString());
                    Response.Redirect("GenEmpDocumentPreview.aspx?EmpID=" + EmpID + "&DocID=" + DocID + "&EmpDocID=" + EmpDocID);
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }


        }
        protected override void OnInit(EventArgs e)
        {
            ModuleID = 1;
            base.OnInit(e);
        }
    }
}