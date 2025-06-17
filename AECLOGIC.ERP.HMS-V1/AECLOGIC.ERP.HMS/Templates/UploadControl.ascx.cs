using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AECLOGIC.ERP.COMMON
{
    public partial class UploadControl : System.Web.UI.UserControl
    {
        string _FileUploadPath = "";

        public string FileUploadPath
        {
            get { return _FileUploadPath; }
            set { _FileUploadPath = value; Session["ucUploadFilePath"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string jscript = "function UploadComplete(){" + Page.ClientScript.GetPostBackEventReference(LinkButton1, "") + "};";
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "FileCompleteUpload", jscript, true);
            }


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
}