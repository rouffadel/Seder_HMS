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
    /* NOTE: This code is for sample purposes only. In a production application, you could use a MVC design pattern.
    * In addition, you should provide appropriate exception handling and refactor authentication settings into 
    * a secure configuration. Authentication settings are hard-coded in the sample to make it easier to follow the flow of authentication. 
    * In addition, the sample uses a single web page so that all code is in one location. However, you could refactor the code into
    * your own production model.
    */
    public partial class _Default : AECLOGIC.ERP.COMMON.WebFormMaster
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

            dsClientDetails.Tables.Clear();
            SqlParameter[] sp = new SqlParameter[2];
            sp[0] = new SqlParameter("@moduleid", Convert.ToInt32(ModuleID));
            sp[1] = new SqlParameter("@RedirectUri", "http://localhost:13526/HMS/Default.aspx");
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
                Response.Redirect("/HMS/Default.aspx");
            }

            //After the redirect above to get rid of code=, Session["authResult"] does not equal null, which means you have an
            //Access Token. With the Acccess Token, you can call the Power BI Get Reports operation. Get Reports returns information
            //about a Report, not the actual Report visual. You get the Report visual later with some JavaScript. See postActionLoadReport()
            //in Default.aspx.
            if (Session["AccessToken"] != null)
            {
                //You need the Access Token in an HTML element so that the JavaScript can load a Report visual into an IFrame.
                //Without the Access Token, you can not access the Report visual.
                accessToken.Value = Session["AccessToken"].ToString();

                //In this sample, you get the first Report. In a production app, you would create a more robost
                //solution

                //Get first report. 
                GetReport(0);
                //int iIndex = 0;
                //string dbid= GetDashboard(iIndex);
                //ReportEmbedUrl.Text = "https://app.powerbi.com/embed?dashboardId=" + dbid;
            }
        }

        protected void getReportButton_Click(object sender, EventArgs e)
        {
            //You need an Authorization Code from Azure AD so that you can get an Access Token
            //Values are hard-coded for sample purposes.
            GetAuthorizationCode();
        }
        //Get a dashboard id.
        protected string GetDashboard(int index)
        {
            string dashboardId = string.Empty;

            //Configure tiles request
            System.Net.WebRequest request = System.Net.WebRequest.Create(
                String.Format("{0}/Dashboards",
                baseUri)) as System.Net.HttpWebRequest;

            request.Method = "GET";
            request.ContentLength = 0;
            request.Headers.Add("Authorization", String.Format("Bearer {0}", accessToken.Value));

            //Get dashboards response from request.GetResponse()
            using (var response = request.GetResponse() as System.Net.HttpWebResponse)
            {
                //Get reader from response stream
                using (var reader = new System.IO.StreamReader(response.GetResponseStream()))
                {
                    //Deserialize JSON string
                    PBIDashboards dashboards = JsonConvert.DeserializeObject<PBIDashboards>(reader.ReadToEnd());

                    //Sample assumes at least one Dashboard with one Tile.
                    //You could write an app that lists all tiles in a dashboard
                    dashboardId = dashboards.value[index].id;
                    //c2c6e2e2-6603-4473-ac03-7d010c27e4cb
                    //c2c6e2e2-6603-4473-ac03-7d010c27e4cb
                    //ReportEmbedUrl.Text = "https://app.powerbi.com/groups/me/dashboards/" + dashboardId;
                }
            }

            return dashboardId;
        }

        //Get a Report. In this sample, you get the first Report.
        protected void GetReport(int index)
        {
            //Configure Reports request
            System.Net.WebRequest request = System.Net.WebRequest.Create(
                String.Format("{0}/Groups",
                baseUri)) as System.Net.HttpWebRequest;

            request.Method = "GET";
            request.ContentLength = 0;
            request.Headers.Add("Authorization", String.Format("Bearer {0}", accessToken.Value));

            //Get Reports response from request.GetResponse()
            using (var response = request.GetResponse() as System.Net.HttpWebResponse)
            {
                //Get reader from response stream
                using (var reader = new System.IO.StreamReader(response.GetResponseStream()))
                {
                    //Deserialize JSON string
                    PBIReports Reports = JsonConvert.DeserializeObject<PBIReports>(reader.ReadToEnd());

                    //Sample assumes at least one Report.
                    //You could write an app that lists all Reports
                    if (Reports.value.Length > 0)
                        ReportEmbedUrl.Text = Reports.value[index].embedUrl;
                }
            }
        }
        //protected void Getgroups(string Groupid)
        //{
        //    //Configure Reports request
        //    System.Net.WebRequest request = System.Net.WebRequest.Create(
        //        String.Format("{0}/Groups",
        //        baseUri)) as System.Net.HttpWebRequest;

        //    request.Method = "GET";
        //    request.ContentLength = 0;
        //    request.Headers.Add("Authorization", String.Format("Bearer {0}", accessToken.Value));

        //    //Get Reports response from request.GetResponse()
        //    using (var response = request.GetResponse() as System.Net.HttpWebResponse)
        //    {
        //        //Get reader from response stream
        //        using (var reader = new System.IO.StreamReader(response.GetResponseStream()))
        //        {
        //            //Deserialize JSON string
        //            PBIGroup oGroups = JsonConvert.DeserializeObject<PBIGroup>(reader.ReadToEnd());

        //            //Sample assumes at least one Report.
        //            //You could write an app that lists all Reports
        //            string guid = Request.QueryString["Key"];

        //            //2b44ef13-4e00-47be-8191-cdc577621e77
        //            for (int i = 0; i < oGroups.value.Length; i++)
        //            {
        //                if (guid == oGroups.value[i].id)
        //                {
        //                    ReportEmbedUrl.Text = oGroups.value[i].embedUrl;
        //                    break;
        //                }
                      
        //            }
                    
        //        }
        //    }
        //}

        public void GetAuthorizationCode()
        {
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
        //to Default page (Default.aspx).
        { "redirect_uri", dsClientDetails.Tables[0].Rows[0]["RedirectUri"].ToString()}
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

        //public void GetAuthorizationCode()
        //{
        //    //NOTE: Values are hard-coded for sample purposes.
        //    //Create a query string
        //    //Create a sign-in NameValueCollection for query string
        //    var @params = new NameValueCollection
        //    {
        //        //Azure AD will return an authorization code.
        //        {"response_type", "code"},

        //        //Client ID is used by the application to identify themselves to the users that they are requesting permissions from.
        //        //You get the client id when you register your Azure app.
        //        {"client_id", dsClientDetails.Tables[0].Rows[0]["Clientid"].ToString()},

        //        //Resource uri to the Power BI resource to be authorized
        //        //The resource uri is hard-coded for sample purposes
        //        {"resource", "https://analysis.windows.net/powerbi/api"},

        //        //After app authenticates, Azure AD will redirect back to the web app. In this sample, Azure AD redirects back
        //        //to Default page (Default.aspx).
        //        { "redirect_uri", dsClientDetails.Tables[0].Rows[0]["RedirectUri"].ToString()}
        //    };
        //    //var @params = new NameValueCollection
        //    //{
        //    //    //Azure AD will return an authorization code. 
        //    //    {"response_type", "code"},

        //    //    //Client ID is used by the application to identify themselves to the users that they are requesting permissions from. 
        //    //    //You get the client id when you register your Azure app.
        //    //    {"client_id", dsClientDetails.Tables[0].Rows[0]["Clientid"].ToString()},

        //    //    //Resource uri to the Power BI resource to be authorized
        //    //    //The resource uri is hard-coded for sample purposes
        //    //    {"resource", "https://analysis.windows.net/powerbi/api"},

        //    //    //After app authenticates, Azure AD will redirect back to the web app. In this sample, Azure AD redirects back
        //    //    //to Default page (Default.aspx).
        //    //    { "redirect_uri",  dsClientDetails.Tables[0].Rows[0]["RedirectUri"].ToString()}
        //    //};

        //    //Create sign-in query string
        //    var queryString = HttpUtility.ParseQueryString(string.Empty);
        //    queryString.Add(@params);

        //    //Redirect to Azure AD Authority
        //    //  Authority Uri is an Azure resource that takes a client id and client secret to get an Access token
        //    //  QueryString contains 
        //    //      response_type of "code"
        //    //      client_id that identifies your app in Azure AD
        //    //      resource which is the Power BI API resource to be authorized
        //    //      redirect_uri which is the uri that Azure AD will redirect back to after it authenticates

        //    //Redirect to Azure AD to get an authorization code
        //    Response.Redirect(String.Format("https://login.windows.net/common/oauth2/authorize?{0}", queryString));
        //}
        public string GetAccessToken(string authorizationCode, string clientID, string clientSecret, string redirectUri)
        {
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
        //public string GetAccessToken(string authorizationCode, string clientID, string clientSecret, string redirectUri)
        //{
        //    //Redirect uri must match the redirect_uri used when requesting Authorization code.
        //    //Note: If you use a redirect back to Default, as in this sample, you need to add a forward slash
        //    //such as http://localhost:13526/

        //    // Get auth token from auth code       
        //    TokenCache TC = new TokenCache();

        //    //Values are hard-coded for sample purposes
        //    string authority = "https://login.windows.net/common/oauth2/authorize";
        //    AuthenticationContext AC = new AuthenticationContext(authority, TC);
        //    ClientCredential cc = new ClientCredential(clientID, clientSecret);

        //    //Set token from authentication result
        //    return AC.AcquireTokenByAuthorizationCode(
        //        authorizationCode,
        //        new Uri(redirectUri), cc).AccessToken;
        //}
    }

    //Power BI Reports used to deserialize the Get Reports response.
    public class PBIReports1
    {
        public PBIReport[] value { get; set; }
    }
    public class PBIReport1
    {
        public string id { get; set; }
        public string name { get; set; }
        public string webUrl { get; set; }
        public string embedUrl { get; set; }
    }
    public class PBIReport
    {
        public string id { get; set; }
        public string name { get; set; }
        public string webUrl { get; set; }
        public string embedUrl { get; set; }
    }
    public class PBIDashboards
    {
        public PBIDashboard[] value { get; set; }
    }
    public class PBIDashboard
    {
        public string id { get; set; }
        public string displayName { get; set; }
    }

    public class PBIGroups
    {
        public PBIGroup[] value { get; set; }
    }
    public class PBIGroup
    {
        public string id { get; set; }
        public string displayName { get; set; }
    }

}