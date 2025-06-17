using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
namespace AECLOGIC.ERP.HMS
{
    public partial class Download : AECLOGIC.ERP.COMMON.WebFormMaster
    {
        protected override void OnInit(EventArgs e)
        {
            ModuleID = 1;
            base.OnInit(e);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            String strFile = Request.QueryString["file"].Trim();

            String path = Server.MapPath("") + strFile;
            //get file object as FileInfo  

            System.IO.FileInfo file = new System.IO.FileInfo(path);
            //-- if the file exists on the server 

            if (file.Exists) //set appropriate headers  
            {
                Response.Clear();

                Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name);


                Response.AddHeader("Content-Length", file.Length.ToString());

                Response.ContentType = "application/octet-stream";

                Response.WriteFile(file.FullName);

                Response.End();
                //if file does not exist  

            }

            else
            {

                Response.Write("This file does not exist.");

            } //nothing in the URL as HTTP GET
        }
    }
}