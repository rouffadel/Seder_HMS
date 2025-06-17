using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AECLOGIC.ERP.HMS.HRClasses
{
    public class clHMSLog
    {
        public static void LogFileData(String ErrCode, string EventorMethod, string Page, string Error,
            string Path, string UID, string UName)//Server.MapPath("Log.txt")
        {

            System.IO.StreamReader strd = new System.IO.StreamReader(Path);
            string Line = strd.ReadToEnd() + " \n ";
            strd.Close();
            Line = Line + " \n UserID: " + UID + " \t User Namee: " + UName + "\t Date: " + DateTime.Today.ToLongDateString();
            Line = Line + " \n Error Code: " + ErrCode + " \t Event or Method: " + EventorMethod + " \t Page: " + Page + "\t Event: " + Error + "\t Code: " + Error;
            System.IO.StreamWriter stWt = new System.IO.StreamWriter(Path);
            stWt.Write(Line);
            stWt.Close();
        }
    }
}