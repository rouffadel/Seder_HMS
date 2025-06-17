using System;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;


public partial class _Default : AECLOGIC.ERP.COMMON.WebFormMaster 
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // example of forms based authentication
        // check if user is authenticated because all files in folder are marked to allow
        // anonymous access (see web.config in UploadPage folder).  This is because of 
        // the flash bug that does not send cookies set in non IE browswers
      
        //if (!User.Identity.IsAuthenticated)
        //{
        //    FormsAuthentication.RedirectToLoginPage();
        //    return;
        //}
      
        // allows the javascript function to do a postback and call the onClick method
        // associated with the linkButton LinkButton1.
        string jscript = "function UploadComplete(){" + ClientScript.GetPostBackEventReference(LinkButton1, "") + "};";
        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "FileCompleteUpload", jscript, true);

        // Adds query string info to the upload page
        // you can also do something like:
        // we UrlEncode it because of how LoadVars works with flash,
        // we want a string to show up like this 'CategoryID=3&UserID=4' in
        // the uploadPage variable in flash.  If we passed this string without
        // UrlEncode then flash would take UserID as a seperate LoadVar variable
        // instead of passing it into the uploadPage variable.
        // then in the httpHandler we get the CategoryID and UserID values from 
        // the query string. See Upload.cs in App_Code
        //flashUpload.QueryParameters = "categoryId=14"; // Server.UrlEncode(Request.QueryString.ToString());

        // example use of cookieless sessions (flash does not send cookies set in non IE browswers
        Session["temp"] = "hi";
        int PoWo = Convert.ToInt32(Request.QueryString[0]);
        Session["PoWo"] = PoWo;
        // sends the identity of the user to the upload page using query string parameters. Used with forms authtication.

        //FormsIdentity cIdentity = User.Identity as FormsIdentity;
        //string encryptString = FormsAuthentication.Encrypt(cIdentity.Ticket);
        //flashUpload.QueryParameters = string.Format("User={0}", encryptString);  
        //Upload up = new Upload();
        //up.PoId = poid.ToString();
    }

    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        //int poid = Convert.ToInt32(Request.QueryString[0]); 
        //Upload up = new Upload();
        //up.PoId = poid.ToString();
        // Do something that needs to be done such as refresh a gridView
        // say you had a gridView control called gvMyGrid displaying all 
        // the files uploaded. Refresh the data by doing a databind here.
        // gvMyGrid.DataBind();
    }
}
