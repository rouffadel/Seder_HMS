using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// Summary description for CodeUtil
/// </summary>
public class CodeUtil
{
    public CodeUtil()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public enum DateFormat
    {
        DayMonthYear = 0,
        MonthDayYear = 1,
        ddMMMyyyy = 2
    }
    public static DateTime ConverttoDate(string StrDate, DateFormat Format)
    {
        DateTime dt = new DateTime();
        if (StrDate != "")
        {
            switch (Format)
            {
                case DateFormat.DayMonthYear:
                    dt = new DateTime(Convert.ToInt32(StrDate.Split('/')[2]), Convert.ToInt32(StrDate.Split('/')[1]), Convert.ToInt32(StrDate.Split('/')[0]));

                    break;
                case DateFormat.MonthDayYear:
                    dt = new DateTime(Convert.ToInt32(StrDate.Split('/')[2]), Convert.ToInt32(StrDate.Split('/')[0]), Convert.ToInt32(StrDate.Split('/')[1]));
                    break;
                default:
                    break;
            }
        }
        return dt;
    }
    public enum Month
    {
        JAN = 1,
        FEB = 2,
        MAR = 3,
        APR = 4,
        MAY = 5,
        JUN = 6,
        JUL = 7,
        AUG = 8,
        SEP = 9,
        OCT = 10,
        NOV = 11,
        DEC = 12
    }

    private static int getMonth(string sMonth)
    {
        int rVal = 0;
        switch (sMonth.ToUpper().Trim())
        {
            case "JAN":
                rVal = Convert.ToInt32(Month.JAN);
                break;
            case "FEB":
                rVal = Convert.ToInt32(Month.FEB);
                break;
            case "MAR":
                rVal = Convert.ToInt32(Month.MAR);
                break;
            case "APR":
                rVal = Convert.ToInt32(Month.APR);
                break;
            case "MAY":
                rVal = Convert.ToInt32(Month.MAY);
                break;
            case "JUN":
                rVal = Convert.ToInt32(Month.JUN);
                break;
            case "JUL":
                rVal = Convert.ToInt32(Month.JUL);
                break;
            case "AUG":
                rVal = Convert.ToInt32(Month.AUG);
                break;
            case "SEP":
                rVal = Convert.ToInt32(Month.SEP);
                break;
            case "OCT":
                rVal = Convert.ToInt32(Month.OCT);
                break;
            case "NOV":
                rVal = Convert.ToInt32(Month.NOV);
                break;
            case "DEC":
                rVal = Convert.ToInt32(Month.DEC);
                break;
        }
        return rVal;
    }

    public static DateTime ConvertToDate(string StrDate, DateFormat Format)
    {
        DateTime dt = new DateTime();
        if (StrDate != "")
        {
            switch (Format)
            {
                case DateFormat.DayMonthYear:
                    dt = new DateTime(Convert.ToInt32(StrDate.Split('/')[2]), Convert.ToInt32(StrDate.Split('/')[1]), Convert.ToInt32(StrDate.Split('/')[0]));

                    break;
                case DateFormat.MonthDayYear:
                    dt = new DateTime(Convert.ToInt32(StrDate.Split('/')[2]), Convert.ToInt32(StrDate.Split('/')[0]), Convert.ToInt32(StrDate.Split('/')[1]));
                    break;
                case DateFormat.ddMMMyyyy:
                    dt = new DateTime(Convert.ToInt32(StrDate.Split(' ')[2]), getMonth(StrDate.Split(' ')[1]), Convert.ToInt32(StrDate.Split(' ')[0]));
                    break;
                default:
                    break;
            }
        }
        return dt;
    }
    public static DateTime ConvertToDate_ddMMMyyy(string StrDate, DateFormat Format)
    {
        DateTime dt = new DateTime();
        if (StrDate != "")
        {
            switch (Format)
            {
                case DateFormat.DayMonthYear:
                    dt = new DateTime(Convert.ToInt32(StrDate.Split('/')[2]), Convert.ToInt32(StrDate.Split('/')[1]), Convert.ToInt32(StrDate.Split('/')[0]));

                    break;
                case DateFormat.MonthDayYear:
                    dt = new DateTime(Convert.ToInt32(StrDate.Split('/')[2]), Convert.ToInt32(StrDate.Split('/')[0]), Convert.ToInt32(StrDate.Split('/')[1]));
                    break;
                case DateFormat.ddMMMyyyy:
                    dt = new DateTime(Convert.ToInt32(StrDate.Split(' ')[2]), getMonth(StrDate.Split(' ')[1]), Convert.ToInt32(StrDate.Split(' ')[0]));
                    break;
                default:
                    break;
            }
        }
        return dt;
    }
    
    

    #region "MsgBox"



    //public static void MsgBox(object curPage, string msg)
    //{
    //    Page myPage = (Page)curPage;
    //    msg = myPage.Server.HtmlEncode(msg);
    //    msg = msg.Replace("\r", "\\r");
    //    msg = msg.Replace("\t", "\\t");
    //    msg = msg.Replace("\n", "\\n");
    //    msg = msg.Replace("\a", "\\a");
    //    msg = msg.Replace("\b", "\\b");
    //    msg = msg.Replace("\f", "\\f");
    //    msg = msg.Replace("\v", "\\v");
    //    msg = msg.Replace("'", " - ");
    //    string js = "window.alert('" + msg + "');";
    //    ScriptManager.RegisterStartupScript(myPage, myPage.GetType(), Guid.NewGuid().ToString(), js, true);
    //}

    public static void CallClientFunction(object curPage, string msg)
    {
        Page myPage = (Page)curPage;
        ScriptManager.RegisterStartupScript(myPage, myPage.GetType(), Guid.NewGuid().ToString(), msg, true);
    }
    #endregion
}

public class CodeUtilHMS
{
    public CodeUtilHMS()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public enum DateFormat
    {
        DayMonthYear = 0,
        MonthDayYear = 1,
        ddMMMyyyy = 2
    }
    public enum EmployeeRoles
    {
        OMs_Admin = 1,
        OMs_Technical = 2,
        OMs_General = 3,
        OMs_Supervisor = 4
    }

    public enum Month
    {
        JAN = 1,
        FEB = 2,
        MAR = 3,
        APR = 4,
        MAY = 5,
        JUN = 6,
        JUL = 7,
        AUG = 8,
        SEP = 9,
        OCT = 10,
        NOV = 11,
        DEC = 12
    }

    private static int getMonth(string sMonth)
    {
        int rVal = 0;
        switch (sMonth.ToUpper().Trim())
        {
            case "JAN":
                rVal = Convert.ToInt32(Month.JAN);
                break;
            case "FEB":
                rVal = Convert.ToInt32(Month.FEB);
                break;
            case "MAR":
                rVal = Convert.ToInt32(Month.MAR);
                break;
            case "APR":
                rVal = Convert.ToInt32(Month.APR);
                break;
            case "MAY":
                rVal = Convert.ToInt32(Month.MAY);
                break;
            case "JUN":
                rVal = Convert.ToInt32(Month.JUN);
                break;
            case "JUL":
                rVal = Convert.ToInt32(Month.JUL);
                break;
            case "AUG":
                rVal = Convert.ToInt32(Month.AUG);
                break;
            case "SEP":
                rVal = Convert.ToInt32(Month.SEP);
                break;
            case "OCT":
                rVal = Convert.ToInt32(Month.OCT);
                break;
            case "NOV":
                rVal = Convert.ToInt32(Month.NOV);
                break;
            case "DEC":
                rVal = Convert.ToInt32(Month.DEC);
                break;
        }
        return rVal;
    }

    public static DateTime ConvertToDate(string StrDate, DateFormat Format)
    {
        DateTime dt = new DateTime();
        if (StrDate != "")
        {
            switch (Format)
            {
                case DateFormat.DayMonthYear:
                    dt = new DateTime(Convert.ToInt32(StrDate.Split('/')[2]), Convert.ToInt32(StrDate.Split('/')[1]), Convert.ToInt32(StrDate.Split('/')[0]));

                    break;
                case DateFormat.MonthDayYear:
                    dt = new DateTime(Convert.ToInt32(StrDate.Split('/')[2]), Convert.ToInt32(StrDate.Split('/')[0]), Convert.ToInt32(StrDate.Split('/')[1]));
                    break;
                case DateFormat.ddMMMyyyy:
                    dt = new DateTime(Convert.ToInt32(StrDate.Split(' ')[2]), getMonth(StrDate.Split(' ')[1]), Convert.ToInt32(StrDate.Split(' ')[0]));
                    break;
                default:
                    break;
            }
        }
        return dt;
    }
    public static DateTime ConvertToDate_ddMMMyyy(string StrDate, DateFormat Format)
    {
        DateTime dt = new DateTime();
        if (StrDate != "")
        {
            switch (Format)
            {
                case DateFormat.DayMonthYear:
                    dt = new DateTime(Convert.ToInt32(StrDate.Split('/')[2]), Convert.ToInt32(StrDate.Split('/')[1]), Convert.ToInt32(StrDate.Split('/')[0]));

                    break;
                case DateFormat.MonthDayYear:
                    dt = new DateTime(Convert.ToInt32(StrDate.Split('/')[2]), Convert.ToInt32(StrDate.Split('/')[0]), Convert.ToInt32(StrDate.Split('/')[1]));
                    break;
                case DateFormat.ddMMMyyyy:
                    dt = new DateTime(Convert.ToInt32(StrDate.Split(' ')[2]), getMonth(StrDate.Split(' ')[1]), Convert.ToInt32(StrDate.Split(' ')[0]));
                    break;
                default:
                    break;
            }
        }
        return dt;
    }
}
