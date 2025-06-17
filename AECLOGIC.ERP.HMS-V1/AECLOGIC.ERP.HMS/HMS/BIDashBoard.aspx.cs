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
using System.Web.UI.WebControls;


namespace AECLOGIC.ERP.HMS
{
    public partial class BIDashBoard : AECLOGIC.ERP.COMMON.WebFormMaster
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
            sp[1] = new SqlParameter("@RedirectUri", "http://localhost:13526/HMS/BIDashBoard.aspx");
            dsClientDetails = SQLDBUtil.ExecuteDataset("PowerBIConfigurationDetails", sp);
            //Need an Authorization Code from Azure AD before you can get an access token to be able to call Power BI operations
            //You get the Authorization Code when you click Get Tile (see below).
            //After you call AcquireAuthorizationCode(), Azure AD redirects back to this page with an Authorization Code.
            if (Request.Params.Get("code") != null)
            {
                //After you get an AccessToken, you can call Power BI API operations such as Get Tile
                Session["AccessToken"] = GetAccessToken(
                    Request.Params.GetValues("code")[0],
                    dsClientDetails.Tables[0].Rows[0]["Clientid"].ToString(),
                    dsClientDetails.Tables[0].Rows[0]["ClientSecretKey"].ToString(),
                    dsClientDetails.Tables[0].Rows[0]["RedirectUri"].ToString());

                //Redirect again to get rid of code=
                Response.Redirect("/hms/BIDashBoard.aspx");
            }

            //After the redirect above to get rid of code=, Session["authResult"] does not equal null, which means you have an
            //Access Token. With the Acccess Token, you can call the Power BI Get Tiles operation. Get Tiles returns information
            //about a tile, not the actual tile visual. You get the tile visual later with some JavaScript. See postActionLoadTile()
            //in Default.aspx.
            if (Session["AccessToken"] != null)
            {
                //You need the Access Token in an HTML element so that the JavaScript can load a tile visual into an IFrame.
                //Without the Access Token, you can not access the tile visual.
                accessToken.Value = Session["AccessToken"].ToString();

                //Get first dashbaord. Sample assumes one dashbaord with one tile
                string dashboardId = GetDashboard(0);
                //GetDashboard(0);

                //You can get the Dashboard ID with the Get Dashboards operation. Or go to your dashboard, and get it from the url for the dashboard.
                //The dashboard id is at the end if the url. For example, https://msit.powerbi.com/groups/me/dashboards/00b7e871-cb98-48ed-bddc-0000c000e000              
                //In this sample, you get the first tile in the first dashbaord. In a production app, you would create a more robost
                //solution
                //for (int i = 0; i < 2;i++)
                 //   GetTile(dashboardId, i);

                //GetDashboard(0);
            }
        }

        protected void getTileButton_Click(object sender, EventArgs e)
        {
            //You need an Authorization Code from Azure AD so that you can get an Access Token
            //Values are hard-coded for sample purposes.
            GetAuthorizationCode();
        }


        //Get a dashbaord id. In this sample, you get the first tile.
        protected string GetDashboard(int index)
        {
            string dashboardId = string.Empty;

            //Configure tiles request
            System.Net.WebRequest request = System.Net.WebRequest.Create(
                String.Format("{0}Dashboards",
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
                }
            }

            return dashboardId;
        }


        //Get a tile from a dashboard. In this sample, you get the first tile.
        protected void GetTile(string dashboardId, int index)
        {
            //Configure tiles request
            System.Net.WebRequest request = System.Net.WebRequest.Create(
                String.Format("{0}Dashboards/{"+index+"}/Tiles",
                baseUri,
                dashboardId)) as System.Net.HttpWebRequest;

            request.Method = "GET";
            request.ContentLength = 0;
            request.Headers.Add("Authorization", String.Format("Bearer {0}", accessToken.Value));

            //Get tiles response from request.GetResponse()
            using (var response = request.GetResponse() as System.Net.HttpWebResponse)
            {
                //Get reader from response stream
                using (var reader = new System.IO.StreamReader(response.GetResponseStream()))
                {
                    //Deserialize JSON string
                    PBITiles tiles = JsonConvert.DeserializeObject<PBITiles>(reader.ReadToEnd());

                    //Sample assumes at least one Dashboard with one Tile.
                    //You could write an app that lists all tiles in a dashboard
                    //if (tiles.value.Length > 0)
                    //    tileEmbedUrl.Text = tiles.value[index].embedUrl;

                    if (tiles.value.Length > 0)
                    {
                        tilecount.Value = tiles.value.Length.ToString();
                        for (int i = 0; i < tiles.value.Length; i++)
                        {
                            TextBox txt = new TextBox();
                            txt.ID = "txt" + (i + 1).ToString();
                            txt.Text = tiles.value[i].embedUrl;
                            txt.Width = 1;
                            divMain.Controls.Add(txt);
                            divMain.Controls.Add(new LiteralControl("<iframe id='iframe" + (i + 1) + "' width='300px' height='400px' src=''></iframe>"));
                        }


                    }
                }
            }
        }

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
    }

    //Power BI Dashboards used to deserialize the Get Dashboards response.
    public class PBIDashboards1
    {
        public PBIDashboard[] value { get; set; }
    }
    public class PBIDashboard
    {
        public string id { get; set; }
        public string displayName { get; set; }
    }

    //Power BI Tiles used to deserialize the Get Tiles response.
    public class PBITiles
    {
        public PBITile[] value { get; set; }
    }
    public class PBITile
    {
        public string id { get; set; }
        public string title { get; set; }
        public string embedUrl { get; set; }
    }
}