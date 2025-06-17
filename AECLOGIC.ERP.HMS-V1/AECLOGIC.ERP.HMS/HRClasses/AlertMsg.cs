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

/// <summary>
/// Summary description for AlertMsg
/// </summary>
public class AlertMsg
{
    public AlertMsg()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    #region "MsgBox"
   


    public static void MsgBox(object curPage, string msg)
    {
        Page myPage = (Page)curPage;
        msg = myPage.Server.HtmlEncode(msg);
        msg = msg.Replace("\r", "\\r");
        msg = msg.Replace("\t", "\\t");
        msg = msg.Replace("\n", "\\n");
        msg = msg.Replace("\a", "\\a");
        msg = msg.Replace("\b", "\\b");
        msg = msg.Replace("\f", "\\f");
        msg = msg.Replace("\v", "\\v");
        msg = msg.Replace("'", " - ");
        string js = "window.alert('" + msg + "');";
        //myPage.RegisterStartupScript("msg", js);
        //myPage.RegisterClientScriptBlock("msg", js);
        // ScriptManager.RegisterClientScriptBlock(myPage, myPage.GetType(), Guid.NewGuid().ToString(), "alert('I am from the Server');", true);
        ScriptManager.RegisterClientScriptBlock(myPage, myPage.GetType(), Guid.NewGuid().ToString(), js, true); 
    }


    public static void GnrlMsgBox(object curPage, string msg)
    {
        Page myPage = (Page)curPage;
        msg = myPage.Server.HtmlEncode(msg);
        msg = msg.Replace("\r", "\\r");
        msg = msg.Replace("\t", "\\t");
        msg = msg.Replace("\n", "\\n");
        msg = msg.Replace("\a", "\\a");
        msg = msg.Replace("\b", "\\b");
        msg = msg.Replace("\f", "\\f");
        msg = msg.Replace("\v", "\\v");
        msg = msg.Replace("'", " - ");
        string js = "window.alert('" + msg + "');";
        //myPage.RegisterStartupScript("msg", js);
        //myPage.RegisterClientScriptBlock("msg", js);
        // ScriptManager.RegisterClientScriptBlock(myPage, myPage.GetType(), Guid.NewGuid().ToString(), "alert('I am from the Server');", true);
        myPage.RegisterStartupScript("msgBox", js);
    }

    public static void CallScriptFunction(object curPage, string func)
    {
        Page myPage = (Page)curPage;

        string js = "<script language='javascript' type='text/javascript'>" + func + "</script>";
        ScriptManager.RegisterStartupScript(myPage, myPage.GetType(), "func", js, false);
        //myPage.RegisterStartupScript("msg", js);
        //myPage.RegisterClientScriptBlock("msg", js);
        // ScriptManager.RegisterClientScriptBlock(myPage, myPage.GetType(), Guid.NewGuid().ToString(), "alert('I am from the Server');", true);
        // myPage.RegisterStartupScript("msgBox", js);
    }



    
    #endregion

    internal static void MsgBox(string p)
    {
        throw new NotImplementedException();
    }
}
