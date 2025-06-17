<%@ Application Language="C#" %>
<script RunAt="server">

    void Application_Start(object sender, EventArgs e)
    {
        Application.Lock();
        Application["OnlineUsers"] = 0;
        Application.UnLock();
    }

    void Application_Error(object sender, EventArgs e)
    {

        // Code that runs when an unhandled error occurs

        Exception objErr = Server.GetLastError().InnerException;
        if (objErr != null)
        {

            WebFormMaster.WriteToErrorLog("Error Caught in Application_Error event", objErr);
            HttpContext.Current.Server.ClearError();
            string err = "Error Caught in Application_Error event\n" +
            "Error in: " + Request.Url.ToString() +
            "\nError Message:" + objErr.Message.ToString() +
            "\nStack Trace:" + objErr.StackTrace.ToString();

            HttpContext.Current.Application.Add("CurError", err);
            HttpContext.Current.Response.Redirect("~/ApplicationError.aspx");
        }

    }
    void Application_End(object sender, EventArgs e)
    {
        Application.Lock();
        Application["OnlineUsers"] = Convert.ToInt32(Application["OnlineUsers"]) - 1;
        // WebFormMaster.UpdatelogoutStatus( Convert.ToInt32(Session["UserId"]), Convert.ToInt32(Session["LoginID"]));
        Application.UnLock();
    }

    //void Application_Error(object sender, EventArgs e) 
    //{ 
    //    // Code that runs when an unhandled error occurs
    //}

    void Session_Start(object sender, EventArgs e)
    {
        Application.Lock();
        Application["OnlineUsers"] = Convert.ToInt32(Application["OnlineUsers"]) + 1;
        Application.UnLock();

    }

    void Session_End(object sender, EventArgs e)
    {
        Application.Lock();
        Session.Abandon();
        Session.Clear();
        //WebFormMaster.UpdatelogoutStatus( Convert.ToInt32(Session["UserId"]), Convert.ToInt32(Session["LoginID"]));
        if (Convert.ToInt32(Application["OnlineUsers"]) > 0)
        {
            Application["OnlineUsers"] = Convert.ToInt32(Application["OnlineUsers"]) - 1;
        }
         Convert.ToInt32(Session["UserId"]) = null;
        Application.UnLock();

    }
       
</script>
