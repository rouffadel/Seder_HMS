using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.IO;
using System.Xml;
/// <summary>
/// Summary description for clsErrorLog
/// </summary>
public class clsErrorLog
{
    public static bool Log(Exception objException)
    {
        string sErrorMessage = string.Empty;
        string sErrorLogFileName;
        sErrorLogFileName = GetLogFilePath();
        bool bReturn = false;
        string strException = string.Empty;
        try
        {
            StreamWriter sw = new StreamWriter(sErrorLogFileName, true);
            sErrorMessage = "Source        : " + objException.Source.ToString().Trim() + "\r\n";
            sErrorMessage = sErrorMessage + "Method        : " + objException.TargetSite.Name.ToString() + "\r\n";
            sErrorMessage = sErrorMessage + "Date        : " + DateTime.Now.ToLongTimeString() + "\r\n";
            sErrorMessage = sErrorMessage + "Time        : " + DateTime.Now.ToShortDateString() + "\r\n";
            sErrorMessage = sErrorMessage + "Error        : " + objException.Message.ToString().Trim() + "\r\n";
            sErrorMessage = sErrorMessage + "Stack Trace    : " + objException.StackTrace.ToString().Trim();
            sErrorMessage = sErrorMessage + "\r\n" + "^^------------------------------------------------------------------^^";
            sw.WriteLine(sErrorMessage);
            sw.Flush();
            sw.Close();
            bReturn = true;
        }
        catch (Exception)
        {
            bReturn = false;
        }
        return bReturn;
    }

    private static string GetLogFilePath()
    {
        try
        {
            string retFilePath = (System.Web.HttpContext.Current.Server.MapPath(".\\ErrorLog\\") + "MMSErrorLog.txt");
            if (File.Exists(retFilePath) == true)
                return retFilePath;
            else
            {
                FileStream fs = new FileStream(retFilePath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                fs.Close();
            }

            return retFilePath;
        }
        catch (Exception)
        {
            return string.Empty;
        }
    }


    public static bool HMSEventLog(Exception objException, String PageName, String EventName, String ErrorNo)
    {
        string sErrorMessage = string.Empty;
        string sErrorLogFileName;
        sErrorLogFileName = GetLogFilePath_HMS();
        bool bReturn = false;
        string strException = string.Empty;
        try
        {
            StreamWriter sw = new StreamWriter(sErrorLogFileName, true);
            sErrorMessage = "Page        : " + PageName.Trim() + "\r\t" + "Event        : " + EventName.Trim() + "\tError: " + ErrorNo + "\r\n";
            sErrorMessage = sErrorMessage + "Source        : " + objException.Source.ToString().Trim() + "\r\n";
            sErrorMessage = sErrorMessage + "Method        : " + objException.TargetSite.Name.ToString() + "\r\n";
            sErrorMessage = sErrorMessage + "Date        : " + DateTime.Now.ToShortDateString() + "\r\n";
            sErrorMessage = sErrorMessage + "Time        : " + DateTime.Now.ToLongTimeString() + "\r\n";
            sErrorMessage = sErrorMessage + "Error        : " + objException.Message.ToString().Trim() + "\r\n";
            sErrorMessage = sErrorMessage + "Stack Trace    : " + objException.StackTrace.ToString().Trim();
            sErrorMessage = sErrorMessage + "Code Line    : " + objException.HResult.ToString().Trim();
            sErrorMessage = sErrorMessage + "\r\n" + "^^------------------------------------------------------------------^^";
            sw.WriteLine(sErrorMessage);
            sw.Flush();
            sw.Close();
            bReturn = true;
        }
        catch (Exception)
        {
            bReturn = false;
        }
        return bReturn;
    }
    private static string GetLogFilePath_HMS()
    {
        try
        {
            string retFilePath = (System.Web.HttpContext.Current.Server.MapPath(".\\Logs\\") + "HMSEventLog.txt");
            if (File.Exists(retFilePath) == true)
                return retFilePath;
            else
            {
                FileStream fs = new FileStream(retFilePath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                fs.Close();
            }

            return retFilePath;
        }
        catch (Exception)
        {
            return string.Empty;
        }
    }
    /// <summary>
    /// errorlog Xml
    /// </summary>
    private const string ERROR_FILE = "ErrorLog/error.xml";
    public static void RaiseError(string message)
    {
        CheckFileExists();

        XmlDocument doc = new XmlDocument();
        XmlNode child;
        doc.Load(System.Web.HttpContext.Current.Server.MapPath(ERROR_FILE));

        XmlNode node = doc.CreateNode(XmlNodeType.Element, "error", "");
        child = doc.CreateNode(XmlNodeType.CDATA, "msg", "");
        child.InnerText = message;
        node.AppendChild(child);

        child = doc.CreateNode(XmlNodeType.Element, "date", "");
        child.InnerText = DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString();
        node.AppendChild(child);

        child = doc.CreateNode(XmlNodeType.CDATA, "url", "");
        child.InnerText = System.Web.HttpContext.Current.Request.RawUrl;
        node.AppendChild(child);

        doc.DocumentElement.AppendChild(node);

        try
        {
            doc.Save(System.Web.HttpContext.Current.Server.MapPath(ERROR_FILE));
        }
        catch
        {
        }
        doc = null;
        node = null;
        child = null;

        System.Web.HttpContext.Current.Response.Clear();
        System.Web.HttpContext.Current.Response.End();
    }

    private static void CheckFileExists()
    {
        if (!File.Exists(System.Web.HttpContext.Current.Server.MapPath(ERROR_FILE)))
        {
            XmlTextWriter writer = new XmlTextWriter(System.Web.HttpContext.Current.Server.MapPath(ERROR_FILE), System.Text.Encoding.UTF8);
            writer.WriteStartDocument();
            writer.WriteStartElement("errorlog");
            writer.WriteEndElement();
            writer.WriteEndDocument();
            writer.Flush();
            writer.Close();
            writer = null;
        }
    }

    public static bool UpdatationReport(string Method, string PageName, string EventName, string EmpId, string Remark)
    {

        string ReportFileName;
        ReportFileName = GetReportFilePath();
        bool bReturn = false;
        string strException = string.Empty;
        try
        {
            System.IO.StreamReader strd = new System.IO.StreamReader(ReportFileName);
            string Report = strd.ReadToEnd() + " \n ";
            strd.Close();
            Report = Report + "Method        : " + Method + "\r\n";
            Report = Report + "Page Name        : " + PageName + "\r\n";
            Report = Report + "Event Name        : " + EventName + "\r\n";
            Report = Report + "Date        : " + DateTime.Now.ToLongTimeString() + "\r\n";
            Report = Report + "Time        : " + DateTime.Now.ToShortDateString() + "\r\n";
            Report = Report + "User        : " + EmpId + "\r\n";
            Report = Report + "Remark    : " + Remark + "\r\n";
            Report = Report + "\r\n" + "^^------------------------------------------------------------------^^";
            System.IO.StreamWriter stWt = new System.IO.StreamWriter(ReportFileName);
            stWt.Write(Report);
            stWt.Close();

            bReturn = true;
        }
        catch (Exception)
        {
            bReturn = false;
        }
        return bReturn;
    }

    private static string GetReportFilePath()
    {
        try
        {
            string retFilePath = (System.Web.HttpContext.Current.Server.MapPath(".\\ErrorLog\\") + "MMSReportLog.txt");
            if (File.Exists(retFilePath) == true)
                return retFilePath;
            else
            {
                FileStream fs = new FileStream(retFilePath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                fs.Close();
            }

            return retFilePath;
        }
        catch (Exception)
        {
            return string.Empty;
        }
    }
}