// According to http://msdn2.microsoft.com/en-us/library/system.web.httppostedfile.aspx
// "Files are uploaded in MIME multipart/form-data format. 
// By default, all requests, including form fields and uploaded files, 
// larger than 256 KB are buffered to disk, rather than held in server memory."
// So we can use an HttpHandler to handle uploaded files and not have to worry
// about the server recycling the request do to low memory. 
// don't forget to increase the MaxRequestLength in the web.config.
// If you server is still giving errors, then something else is wrong.
// I've uploaded a 1.3 gig file without any problems. One thing to note, 
// when the SaveAs function is called, it takes time for the server to 
// save the file. The larger the file, the longer it takes.
// So if a progress bar is used in the upload, it may read 100%, but the upload won't
// be complete until the file is saved.  So it may look like it is stalled, but it
// is not.

using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.SessionState;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;


/// <summary>
/// Upload handler for uploading files.
/// </summary>
public class Upload : IHttpHandler, IRequiresSessionState
{

    private string _PoId;

    public string PoId
    {
        get { return _PoId; }
        set { _PoId = value; }
    }
    
    public Upload()
    {
    }

    #region IHttpHandler Members

    public bool IsReusable
    {
        get { return true; }
    }

    public void ProcessRequest(HttpContext context)
    {
       
        string uploadPath = context.Session["ucUploadFilePath"].ToString();

        
       
        try
        {
            String[] Folders = uploadPath.Split(new char[] { '/', '\\' }, StringSplitOptions.RemoveEmptyEntries);
            if (Folders.Length > 1)
            {
                string Dir = Folders[0];
                DirectoryInfo di = null;
                for (int i = 1; i < Folders.Length; i++)
                {
                    Dir += "/" + Folders[i];
                    di = new DirectoryInfo(Dir);
                    if (!di.Exists)
                    {
                        di.Create();
                    }
                }
            }
            for (int j = 0; j < context.Request.Files.Count; j++)
            {
                HttpPostedFile uploadFile = context.Request.Files[j];
                if (uploadFile.ContentLength > 0)
                {
                    
                    string FileName = uploadFile.FileName;
                    string ext = FileName.Split('.')[FileName.Split('.').Length - 1];
                    string names = FileName.Split('.')[FileName.Split('.').Length - 2];
                    string name = names.Split('\\')[names.Split('\\').Length - 1];
                    string fname = name + "." + ext;
                    uploadFile.SaveAs(Path.Combine(uploadPath, fname));
                }
            }
           
            HttpContext.Current.Response.Write(" ");
          
        }
        catch (Exception ex)
        {
            throw ex;
        }

   }

 
    #endregion
}
