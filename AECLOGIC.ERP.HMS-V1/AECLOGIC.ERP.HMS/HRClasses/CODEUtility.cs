using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// Summary description for CODEUtility
/// </summary>
/// 


public enum DateFormat
{
    DayMonthYear = 0,
    MonthDayYear = 1,
    YearMonthDay = 2
}

public static class CODEUtility
{
    public static DateTime Convertdate(string StrDate)
    {
        DateTime dt = new DateTime();
        if (StrDate != "")
        {
            dt = new DateTime(Convert.ToInt32(StrDate.Split('/')[2]), Convert.ToInt32(StrDate.Split('/')[1]), Convert.ToInt32(StrDate.Split('/')[0]));
        }
        return dt;
    }
   
    public static string NumberToText(double number)
    {
        return NumberToText(Convert.ToInt32(number));
    }
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
    //    string js = "<script language='javascript' type='text/javascript'>window.alert('" + msg + "');</script>";
    //    myPage.RegisterClientScriptBlock("msg", js);
    //}
    public static string NumberToText(int number)
    {
        if (number == 0) return "Zero";

        if (number == -2147483648) return "Minus Two Hundred and Fourteen Crore Seventy Four Lakh Eighty Three Thousand Six Hundred and Forty Eight";

        int[] num = new int[4];
        int first = 0;
        int u, h, t;
        System.Text.StringBuilder sb = new System.Text.StringBuilder();

        if (number < 0)
        {
            sb.Append("Minus ");
            number = -number;
        }

        string[] words0 = { "", "One ", "Two ", "Three ", "Four ", "Five ", "Six ", "Seven ", "Eight ", "Nine " };

        string[] words1 = { "Ten ", "Eleven ", "Twelve ", "Thirteen ", "Fourteen ", "Fifteen ", "Sixteen ", "Seventeen ", "Eighteen ", "Nineteen " };

        string[] words2 = { "Twenty ", "Thirty ", "Forty ", "Fifty ", "Sixty ", "Seventy ", "Eighty ", "Ninety " };

        string[] words3 = { "Thousand ", "Lakh ", "Crore " };

        num[0] = number % 1000; // units
        num[1] = number / 1000;
        num[2] = number / 100000;
        num[1] = num[1] - 100 * num[2]; // thousands
        num[3] = number / 10000000; // crores
        num[2] = num[2] - 100 * num[3]; // lakhs

        for (int i = 3; i > 0; i--)
        {
            if (num[i] != 0)
            {
                first = i;
                break;
            }
        }

        for (int i = first; i >= 0; i--)
        {
            if (num[i] == 0) continue;

            u = num[i] % 10; // ones
            t = num[i] / 10;
            h = num[i] / 100; // hundreds
            t = t - 10 * h; // tens

            if (h > 0) sb.Append(words0[h] + "Hundred ");

            if (u > 0 || t > 0)
            {
                if (h > 0 || i == 0) sb.Append("and ");

                if (t == 0)
                    sb.Append(words0[u]);
                else if (t == 1)
                    sb.Append(words1[u]);
                else
                    sb.Append(words2[t - 2] + words0[u]);
            }

            if (i != 0) sb.Append(words3[i - 1]);

        }
        return sb.ToString().TrimEnd();
    }

     public static DateTime ConvertToDate(string Date, DateFormat Format)
        {
           
            DateTime dt = new DateTime();
            Date = Date.Replace('-', '/');
            if (Date != "")
            {
                switch (Format)
                {
                    case DateFormat.DayMonthYear:
                        dt = new DateTime(Convert.ToInt32(Date.Split('/')[2]), Convert.ToInt32(Date.Split('/')[1]), Convert.ToInt32(Date.Split('/')[0]));

                        break;
                    case DateFormat.MonthDayYear:
                        dt = new DateTime(Convert.ToInt32(Date.Split('/')[2]), Convert.ToInt32(Date.Split('/')[0]), Convert.ToInt32(Date.Split('/')[1]));
                        break;
                    case DateFormat.YearMonthDay:
                        dt = new DateTime(Convert.ToInt32(Date.Split('/')[0]), Convert.ToInt32(Date.Split('/')[1]), Convert.ToInt32(Date.Split('/')[2]));
                        break;
                    default:
                        break;
                }
            }
            return dt;
        }

}
