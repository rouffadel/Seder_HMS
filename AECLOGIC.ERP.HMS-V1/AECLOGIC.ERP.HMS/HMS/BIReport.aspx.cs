using System;
using System.Web;
using System.Web.UI;
using System.Collections.Specialized;
using Newtonsoft.Json;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using AECLOGIC.ERP.HMS.Properties;
using System.Data;
using Aeclogic.Common.DAL;
using System.Data.SqlClient;


namespace AECLOGIC.ERP.HMS
{
    public partial class BIReport : AECLOGIC.ERP.COMMON.WebFormMaster
    {
        string baseUri = "https://api.powerbi.com/beta/myorg/";
        DataSet dsClientDetails = new DataSet();

        protected override void OnInit(EventArgs e)
        {
            ModuleID = 1;
            base.OnInit(e);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                lblMessage.Text = "";
                dsClientDetails.Tables.Clear();
                SqlParameter[] sp = new SqlParameter[2];
                sp[0] = new SqlParameter("@moduleid", Convert.ToInt32(ModuleID));
                sp[1] = new SqlParameter("@RedirectUri", "http://dev.aeclogic.com/HMS/BIReport.aspx");

                dsClientDetails = SQLDBUtil.ExecuteDataset("PowerBIConfigurationDetails", sp);



                //Need an Authorization Code from Azure AD before you can get an access token to be able to call Power BI operations
                //You get the Authorization Code when you click Get Report (see below).
                //After you call AcquireAuthorizationCode(), Azure AD redirects back to this page with an Authorization Code.
                if (Request.Params.Get("code") != null)
                {
                    //After you get an AccessToken, you can call Power BI API operations such as Get Report
                    Session["AccessToken"] = GetAccessToken(
                        Request.Params.GetValues("code")[0],
                        dsClientDetails.Tables[0].Rows[0]["Clientid"].ToString(),
                        dsClientDetails.Tables[0].Rows[0]["ClientSecretKey"].ToString(),
                        dsClientDetails.Tables[0].Rows[0]["RedirectUri"].ToString());

                    //Redirect again to get rid of code=
                    // if (Convert.ToInt32(Request.QueryString["key"]) > 0)
                    Response.Redirect("/HMS/BIReport.aspx?key=" + Request.QueryString["key"]);
                    //else
                    // Response.Redirect("/hms/adminDefault.aspx");
                }

                //After the redirect above to get rid of code=, Session["authResult"] does not equal null, which means you have an
                //Access Token. With the Acccess Token, you can call the Power BI Get Reports operation. Get Reports returns information
                //about a Report, not the actual Report visual. You get the Report visual later with some JavaScript. See postActionLoadReport()
                //in BIReport.aspx.
                if (Session["AccessToken"] != null)
                {
                    //You need the Access Token in an HTML element so that the JavaScript can load a Report visual into an IFrame.
                    //Without the Access Token, you can not access the Report visual.
                    accessToken.Value = Session["AccessToken"].ToString();

                    //In this sample, you get the first Report. In a production app, you would create a more robost
                    //solution

                    //Get first report. 

                    int iIndex = 0;
                    //if (Convert.ToInt32(Request.QueryString["key"]) > 0)
                    //    iIndex = Convert.ToInt32(Request.QueryString["key"]);
                    //else
                    //    iIndex = 0;
                    //if (iIndex > 0)
                    GetReport(iIndex);
                }

                // if (IsPostBack)

                //            GetAuthorizationCode();
            }
            catch (Exception ex)
            {
                clsErrorLog.HMSEventLog(ex, "Power BI", "Page_Load", "001");
                lblMessage.Text = "Technical issue altered kindly Refresh.!!";
            }
        }
        protected void getReportButton_Click(object sender, EventArgs e)
        {
            //You need an Authorization Code from Azure AD so that you can get an Access Token
            //Values are hard-coded for sample purposes.
            GetAuthorizationCode();
        }
        //Get a Report. In this sample, you get the first Report.
        protected void GetReport(int index)
        {
            try
            {
                lblMessage.Text = "";
                //Configure Reports request
                //System.Net.WebRequest request = System.Net.WebRequest.Create(
                //    String.Format("{0}/Reports",
                //    baseUri)) as System.Net.HttpWebRequest;
                string grpid = dsClientDetails.Tables[0].Rows[0]["groupid"].ToString();
                System.Net.WebRequest request = System.Net.WebRequest.Create(
                   String.Format("{0}Groups/{1}/Reports",
                   baseUri,
                   grpid)) as System.Net.HttpWebRequest;

                request.Method = "GET";
                request.ContentLength = 0;
                request.Headers.Add("Authorization", String.Format("Bearer {0}", accessToken.Value));

                //Get Reports response from request.GetResponse()
                using (var response = request.GetResponse() as System.Net.HttpWebResponse)
                {
                    //Get reader from response stream
                    using (var reader = new System.IO.StreamReader(response.GetResponseStream()))
                    {
                        // string ss;
                        // ss = reader.ReadLine();
                        //Deserialize JSON string
                        PBIReports Reports = JsonConvert.DeserializeObject<PBIReports>(reader.ReadToEnd());
                        string guid = Request.QueryString["key"];

                        //2b44ef13-4e00-47be-8191-cdc577621e77
                        for (int i = 0; i < Reports.value.Length; i++)
                        {
                            if (guid == Reports.value[i].id)
                            {
                                ReportEmbedUrl.Text = Reports.value[i].embedUrl;
                                break;
                            }
                            // index++;
                        }


                        //Sample assumes at least one Report.
                        //You could write an app that lists all Reports
                        // if (Reports.value.Length > 0)
                        //  ReportEmbedUrl.Text = Reports.value[index].embedUrl;
                    }
                }
            }
            catch (Exception ex)
            {
                clsErrorLog.HMSEventLog(ex, "Power BI", "GetReport", "002");
                lblMessage.Text = "Technical issue altered kindly Refresh.!!";
            }
        }
        //protected void GetReportss(int index)
        //{
        //    //Configure tiles request
        //    System.Net.WebRequest request = System.Net.WebRequest.Create(
        //        String.Format("{0}Groups/{1}/Reports",
        //        baseUri,
        //        "b7df9cf8-a97c-4c6f-b762-13220b2443f5")) as System.Net.HttpWebRequest;

        //    request.Method = "GET";
        //    request.ContentLength = 0;
        //    request.Headers.Add("Authorization", String.Format("Bearer {0}", accessToken.Value));

        //    //Get tiles response from request.GetResponse()
        //    using (var response = request.GetResponse() as System.Net.HttpWebResponse)
        //    {
        //        //Get reader from response stream
        //        using (var reader = new System.IO.StreamReader(response.GetResponseStream()))
        //        {
        //            //Deserialize JSON string
        //            PBITiles tiles = JsonConvert.DeserializeObject<PBITiles>(reader.ReadToEnd());

        //            //Sample assumes at least one Dashboard with one Tile.
        //            //You could write an app that lists all tiles in a dashboard
        //            if (tiles.value.Length > 0)
        //                ReportEmbedUrl.Text = tiles.value[1].embedUrl;
        //        }
        //    }
        //}
        public void GetAuthorizationCode()
        {
            try
            {
                lblMessage.Text = "";
                //NOTE: Values are hard-coded for sample purposes.
                //Create a query string
                //Create a sign-in NameValueCollection for query string
                var @params = new NameValueCollection
            {
                //Azure AD will return an authorization code. 
                {"response_type", "code"},

                //Client ID is used by the application to identify themselves to the users that they are requesting permissions from. 
                //You get the client id when you register your Azure app.
                {"client_id", dsClientDetails.Tables[0].Rows[0]["Clientid"].ToString()},

                //Resource uri to the Power BI resource to be authorized
                //The resource uri is hard-coded for sample purposes
                {"resource", "https://analysis.windows.net/powerbi/api"},

                //After app authenticates, Azure AD will redirect back to the web app. In this sample, Azure AD redirects back
                //to Default page (BIReport.aspx).
                { "redirect_uri",  dsClientDetails.Tables[0].Rows[0]["RedirectUri"].ToString()}
            };

                //Create sign-in query string
                var queryString = HttpUtility.ParseQueryString(string.Empty);
                queryString.Add(@params);

                //Redirect to Azure AD Authority
                //  Authority Uri is an Azure resource that takes a client id and client secret to get an Access token
                //  QueryString contains 
                //      response_type of "code"
                //      client_id that identifies your app in Azure AD
                //      resource which is the Power BI API resource to be authorized
                //      redirect_uri which is the uri that Azure AD will redirect back to after it authenticates

                //Redirect to Azure AD to get an authorization code
                Response.Redirect(String.Format("https://login.windows.net/common/oauth2/authorize?{0}", queryString));
            }
            catch (Exception ex)
            {
                clsErrorLog.HMSEventLog(ex, "Power BI", "GetAuthorizationCode", "003");
                lblMessage.Text = "Technical issue altered kindly Refresh.!!";
            }
        }
        public string GetAccessToken(string authorizationCode, string clientID, string clientSecret, string redirectUri)
        {
            try
            {
                lblMessage.Text = "";
                //Redirect uri must match the redirect_uri used when requesting Authorization code.
                //Note: If you use a redirect back to Default, as in this sample, you need to add a forward slash
                //such as http://localhost:13526/

                // Get auth token from auth code       
                TokenCache TC = new TokenCache();

                //Values are hard-coded for sample purposes
                string authority = "https://login.windows.net/common/oauth2/authorize";
                AuthenticationContext AC = new AuthenticationContext(authority, TC);
                ClientCredential cc = new ClientCredential(clientID, clientSecret);

                //Set token from authentication result
                return AC.AcquireTokenByAuthorizationCode(
                    authorizationCode,
                    new Uri(redirectUri), cc).AccessToken;
            }
            catch (Exception ex)
            {
                clsErrorLog.HMSEventLog(ex, "Power BI", "GetAccessToken", "004");
                return lblMessage.Text = "Technical issue altered kindly Refresh.!!";
            }
        }
    }

    //Power BI Reports used to deserialize the Get Reports response.
    public class PBIReports
    {
        public PBIReport[] value { get; set; }
    }
    public class PBIReport
    {
        public string id { get; set; }
        public string name { get; set; }
        public string webUrl { get; set; }
        public string embedUrl { get; set; }
    }
}