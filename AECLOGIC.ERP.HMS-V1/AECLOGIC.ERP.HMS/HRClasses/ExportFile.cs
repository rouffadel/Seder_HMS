using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web;

/// <summary>
/// Summary description for ExportToFile
/// </summary>
public class ExportFileUtil
{
    public ExportFileUtil()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    #region SQLDataReader

    public static void ExportToFile(SqlDataReader sqlDataReader, String ItemColor, String AltItemColor, String FileName, String ContentType)
    {
        ExportToFile(sqlDataReader, ItemColor, AltItemColor, "", FileName, ContentType);
    }

    public static void ExportToFile(SqlDataReader sqlDataReader, String ItemColor, String FileName, String ContentType)
    {
        ExportToFile(sqlDataReader, ItemColor, "", "", FileName, ContentType);
    }

    public static void ExportToFile(SqlDataReader sqlDataReader, String FileName, String ContentType)
    {
        ExportToFile(sqlDataReader, "", "", "", FileName, ContentType);
    }

    public static void ExportToExcel(SqlDataReader sqlDataReader, String ItemColor, String AltItemColor, String FileName)
    {
        ExportToFile(sqlDataReader, ItemColor, AltItemColor, "", FileName + ".xls", "application/vnd.xls");
    }

    public static void ExportToExcel(SqlDataReader sqlDataReader, String ItemColor, String AltItemColor, String CrossItemColor, String FileName)
    {
        ExportToFile(sqlDataReader, ItemColor, AltItemColor, CrossItemColor, FileName + ".xls", "application/vnd.xls");
    }

    public static void ExportToFile(SqlDataReader sqlDataReader, String ItemColor, String AltItemColor, String CrossItemColor, String FileName, String Head, String SubHead, String Titil, DataSet ds)
    {
        ExportToFile(sqlDataReader, ItemColor, AltItemColor, CrossItemColor, FileName + ".xls", "application/vnd.xls", Head, SubHead, Titil, ds);

    }

    public static void ExportToFile(String FileName, String Head, String SubHead, String Titil, DataSet ds, DateTime StartDate, DateTime EndDate)
    {
        ExportToFile(FileName + ".xls", "application/vnd.xls", Head, SubHead, Titil, ds, StartDate, EndDate);

    }

    public static void ExportToExcel(SqlDataReader sqlDataReader, String ItemColor, String FileName)
    {
        ExportToFile(sqlDataReader, ItemColor, "", "", FileName + ".xls", "application/vnd.xls");
    }

    public static void ExportToExcel(SqlDataReader sqlDataReader, String FileName)
    {
        ExportToFile(sqlDataReader, "", "", "", FileName + ".xls", "application/vnd.xls");
    }

    public static void ExportToPDF(SqlDataReader sqlDataReader, String ItemColor, String AltItemColor, String FileName)
    {
        ExportToFile(sqlDataReader, ItemColor, AltItemColor, "", FileName + ".pdf", "application/pdf");
    }

    public static void ExportToPDF(SqlDataReader sqlDataReader, String ItemColor, String FileName)
    {
        ExportToFile(sqlDataReader, ItemColor, "", "", FileName + ".pdf", "application/pdf");
    }

    public static void ExportToPDF(SqlDataReader sqlDataReader, String FileName)
    {
        ExportToFile(sqlDataReader, "", "", "", FileName + ".pdf", "application/pdf");
    }

    public static void ExportToWord(SqlDataReader sqlDataReader, String ItemColor, String AltItemColor, String FileName)
    {
        ExportToFile(sqlDataReader, ItemColor, AltItemColor, "", FileName + ".doc", "application/ms-word");
    }

    public static void ExportToWord(SqlDataReader sqlDataReader, String ItemColor, String FileName)
    {
        ExportToFile(sqlDataReader, ItemColor, "", "", FileName + ".doc", "application/ms-word");
    }

    public static void ExportToWord(SqlDataReader sqlDataReader, String FileName)
    {
        ExportToFile(sqlDataReader, "", "", "", FileName + ".doc", "application/ms-word");
    }


    public static void ExportToFile(SqlDataReader sqlDataReader, String ItemColor, String AltItemColor, String CrossItemColor, String FileName, String ContentType)
    {

        //Clear the response
        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + FileName);
        HttpContext.Current.Response.Charset = "";
        HttpContext.Current.Response.ContentType = ContentType;
        HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.UTF8;

        string strLine;

        HttpContext.Current.Response.Write("<table>");
        HttpContext.Current.Response.Write("<tr style='color:White; background-color:Navy; font-weight:bold;'>");
        for (int i = 0; i <= sqlDataReader.FieldCount - 1; i++)
        {

            HttpContext.Current.Response.Write("<td>" + sqlDataReader.GetName(i).ToString() + "</td>");

        }
        HttpContext.Current.Response.Write("</tr>");

        if (ItemColor == "")
            ItemColor = "#ffffff";
        if (AltItemColor == "")
            AltItemColor = "#ffffff";

        int cnt = 0;
        while (sqlDataReader.Read())
        {

            HttpContext.Current.Response.Write("<tr>");


            for (int i = 0; i <= sqlDataReader.FieldCount - 1; i++)
            {
                if (cnt % 2 == 0)
                {
                    if (i % 2 == 0)
                        HttpContext.Current.Response.Write("<td style='background-color:" + ItemColor + ";'>" + sqlDataReader.GetValue(i).ToString() + "</td>");
                    else
                        HttpContext.Current.Response.Write("<td  style='background-color:" + AltItemColor + ";'>" + sqlDataReader.GetValue(i).ToString() + "</td>");
                }
                else
                {
                    if (i % 2 == 0)
                    {

                        HttpContext.Current.Response.Write("<td  style='background-color:" + (CrossItemColor == "" ? ItemColor : AltItemColor) + ";'>" + sqlDataReader.GetValue(i).ToString() + "</td>");
                    }
                    else
                    {
                        HttpContext.Current.Response.Write("<td   style='background-color:" + (CrossItemColor == "" ? AltItemColor : CrossItemColor) + ";'>" + sqlDataReader.GetValue(i).ToString() + "</td>");
                    }
                }
            }
            HttpContext.Current.Response.Write("</tr>");
            cnt++;
        }

        HttpContext.Current.Response.Write("</table>");

        sqlDataReader.Close();

        HttpContext.Current.Response.End();

    }


    public static void ExportToFile(SqlDataReader sqlDataReader, String ItemColor, String AltItemColor, String CrossItemColor, String FileName, String ContentType, String Head, String SubHead, String Titil, DataSet ds)
    {
        string TabllinColor = "";
        float TotalColumn = 0;
        decimal Absvalue = 0;
        decimal TotalAdjustAmount = 0;

        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + FileName);
        HttpContext.Current.Response.Charset = "";
        HttpContext.Current.Response.ContentType = ContentType;
        HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.UTF8;

        string strLine;

        HttpContext.Current.Response.Write("<table>");
        int CountColNames = 5;
        int CountSalary = ds.Tables[0].Rows.Count + // Wag
            ds.Tables[2].Rows.Count + // alow
            ds.Tables[4].Rows.Count + // Non
            +1;
        int CountColAdditiONAL = 4;
        int CountColdEDUCTION = 7;

        int CountClounms = CountColNames + CountSalary + CountColAdditiONAL + CountColdEDUCTION + 1;
        if (Titil.Trim() != "")
        {
            HttpContext.Current.Response.Write("<tr style='color:Black; background-color:White; font-weight:bold;  font-size:30px; align-content:center; border-bottom-width:medium;'>");
            HttpContext.Current.Response.Write("<td colspan=" + CountClounms + " >" + "Monthly Salaries Of : " + Titil.ToString() + "</td>");//colspan=" + CountClounms + " 
            HttpContext.Current.Response.Write("</tr>");

        }
       
        if (SubHead.Trim() != "")
        {
            HttpContext.Current.Response.Write("<tr style='Black:White; background-color:White; font-weight:bold;font-size:22px; align-content:center; border-bottom-width:medium; '>");
            HttpContext.Current.Response.Write("<td colspan=" + CountClounms + ">" + SubHead.ToString() + "</td>");// colspan=" + CountClounms + "
            HttpContext.Current.Response.Write("</tr>");

        }

        HttpContext.Current.Response.Write("<tr style='color:White; background-color:Navy; font-weight:bold; font-size:20px; border-bottom-width:medium;'>");

        HttpContext.Current.Response.Write("<td>" + "Sl No" + "</td>");
        HttpContext.Current.Response.Write("<td>" + "EMPID" + "</td>");
        HttpContext.Current.Response.Write("<td>" + "Employee" + "</td>");
        HttpContext.Current.Response.Write("<td>" + "No WD" + "</td>");
        HttpContext.Current.Response.Write("<td>" + "No PD" + "</td>");
        HttpContext.Current.Response.Write("<td>" + "Adj Days" + "</td>");
        HttpContext.Current.Response.Write("<td colspan=" + CountSalary + " >" + "Salary And Fees" + "</td>");
        HttpContext.Current.Response.Write("<td colspan=" + CountColAdditiONAL + " >" + "Additions" + "</td>");
        HttpContext.Current.Response.Write("<td colspan=" + CountColdEDUCTION + " >" + "Deductions" + "</td>");
        HttpContext.Current.Response.Write("</tr>");



        HttpContext.Current.Response.Write("<tr style='color:White; background-color:Navy; font-weight:bold; font-size:20px; border-bottom-width:medium;'>");

        HttpContext.Current.Response.Write("<td>" + "" + "</td>");
        HttpContext.Current.Response.Write("<td>" + "" + "</td>");
        HttpContext.Current.Response.Write("<td>" + "" + "</td>");
        HttpContext.Current.Response.Write("<td>" + "" + "</td>");
        HttpContext.Current.Response.Write("<td>" + "" + "</td>");
        HttpContext.Current.Response.Write("<td>" + "" + "</td>");
        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            HttpContext.Current.Response.Write("<td>" + dr["LongName"].ToString() + "</td>");
        }

        foreach (DataRow dr in ds.Tables[2].Rows)
        {
            HttpContext.Current.Response.Write("<td>" + dr["LongName"].ToString() + "</td>");
        }
        foreach (DataRow dr in ds.Tables[4].Rows)
        {
            HttpContext.Current.Response.Write("<td>" + dr["LongName"].ToString() + "</td>");
        }
        HttpContext.Current.Response.Write("<td>" + "Total" + "</td>");

        HttpContext.Current.Response.Write("<td>" + "O H" + "</td>");
        HttpContext.Current.Response.Write("<td>" + "Over Time" + "</td>");
        HttpContext.Current.Response.Write("<td>" + "Spl Add" + "</td>");
        HttpContext.Current.Response.Write("<td>" + "Abs Days" + "</td>");
        HttpContext.Current.Response.Write("<td>" + "Pen Days" + "</td>");
        HttpContext.Current.Response.Write("<td>" + "Abs Value" + "</td>");
        HttpContext.Current.Response.Write("<td>" + "Pen Value" + "</td>");
        HttpContext.Current.Response.Write("<td>" + "GOSI" + "</td>");
        HttpContext.Current.Response.Write("<td>" + "Loans" + "</td>");
        HttpContext.Current.Response.Write("<td>" + "Spl Det" + "</td>");
        HttpContext.Current.Response.Write("<td>" + "Total " + "</td>");
        HttpContext.Current.Response.Write("<td>" + "Adjust Amount" + "</td>");
        HttpContext.Current.Response.Write("<td>" + "Net Amount" + "</td>");
        HttpContext.Current.Response.Write("</tr>");
        if (ItemColor == "")
            ItemColor = "#ffffff";
        if (AltItemColor == "")
            AltItemColor = "#ffffff";
        if (CrossItemColor == "")
            CrossItemColor = "#E6e6e6";

        CrossItemColor = "#AED6F1";
        AltItemColor = "#EBF5FB";
        string CrossItemColor2 = "#D5DBDB";
        string AltItemColor2 = "#F4F6F6";
        string TabllinColor2 = "";
        int SlNo = 0;
        System.Collections.Hashtable htdat = new System.Collections.Hashtable();
        htdat.Add(1, "");
        htdat.Add(2, "TOTAL");
        htdat.Add(3, "");
        htdat.Add(4, "");
        htdat.Add(5, "");
        htdat.Add(6, "");
        int Valfoloop = 5;
        foreach (DataRow dr in ds.Tables[7].Rows)
        {
            try
            {
                DataRow[] dreps = ds.Tables[7].Select("EMPID='" + dr["EMPID"].ToString() + "'");
                int DifDays = 0; decimal ActaulNetAmount = 0, DiffActaulNetAmount = 0, SpecialAmt = 0, EMPPanalities = 0, AdjustAmount = 0;
                if (dreps.Length > 0)
                {
                    SlNo++;
                    HttpContext.Current.Response.Write("<tr style='color:Black; background-color:White; font-weight:bold; font-size:18px;    border-bottom-width:medium;'>");

                    if (SlNo % 2 == 0)
                    {
                        TabllinColor = CrossItemColor; TabllinColor2 = CrossItemColor2;
                    }
                    else
                    {
                        TabllinColor = AltItemColor; TabllinColor2 = AltItemColor2;
                    }
                    HttpContext.Current.Response.Write("<td style='background-color:" + TabllinColor + "; border: 1px solid black;'>" + SlNo.ToString() + "</td>");

                    HttpContext.Current.Response.Write("<td style='background-color:" + TabllinColor + "; border: 1px solid black;'>" + dreps[0]["empidcode"].ToString() + "</td>");
                    HttpContext.Current.Response.Write("<td style='background-color:" + TabllinColor + "; border: 1px solid black;'>" + dreps[0]["Name"].ToString() + "</td>");
                    HttpContext.Current.Response.Write("<td style='background-color:" + TabllinColor + "; border: 1px solid black;'>" + dreps[0]["AWD"].ToString() + "</td>");
                    HttpContext.Current.Response.Write("<td style='background-color:" + TabllinColor + "; border: 1px solid black;'>" + dreps[0]["PWD"].ToString() + "</td>");
                    HttpContext.Current.Response.Write("<td style='background-color:" + TabllinColor + "; border: 1px solid black;'>" + dreps[0]["AdjDays"].ToString() + "</td>"); // need to write adj days here
                    DifDays = Convert.ToInt32(dreps[0]["DiffD"]);
                    ActaulNetAmount = Convert.ToDecimal(dreps[0]["NetAmount"]);
                    AdjustAmount = Convert.ToDecimal(dreps[0]["DAmount"]);
                    TotalAdjustAmount = TotalAdjustAmount + AdjustAmount;
                    SpecialAmt = Convert.ToDecimal(dreps[0]["SpecialAmt"]);
                    EMPPanalities = Convert.ToDecimal(dreps[0]["EMPPanalities"]);
                    int Valht = 7;
                    decimal WagesTotal = 0, TotalAmount = 0;
                    foreach (DataRow drW in ds.Tables[0].Rows) // Wages Basic
                    {
                        DataRow[] drsW = ds.Tables[1].Select("EMPID='" + dr["EMPID"].ToString() + "' and WagesID='" + drW["WagesID"] + "'");
                        decimal TemWg = 0;
                        if (drsW.Length > 0)
                            TemWg = Math.Round(Convert.ToDecimal(drsW[0]["BasicValue"]), 2);
                        if (!htdat.Contains(Valht))
                            htdat.Add(Valht, Math.Round(TemWg, 2));
                        else
                            htdat[Valht] = Convert.ToDecimal(htdat[Valht]) + Math.Round(TemWg, 2);

                        Valht++;
                        HttpContext.Current.Response.Write("<td style='background-color:" + TabllinColor2 + "; border: 1px solid black;'>" + TemWg.ToString("N2") + "</td>");
                        WagesTotal = WagesTotal + TemWg;
                    }
                    TotalAmount = WagesTotal;
                    decimal AllowAmount = 0;
                    foreach (DataRow drA in ds.Tables[2].Rows)
                    {
                        DataRow[] drsA = ds.Tables[3].Select("EMPID='" + dr["EMPID"].ToString() + "' and AllowId='" + drA["AllowId"] + "'");
                        decimal TemA = 0;
                        if (drsA.Length > 0)
                            TemA = Math.Round(WagesTotal * Convert.ToDecimal(drsA[0]["Rate"]), 2);

                        if (!htdat.Contains(Valht))
                            htdat.Add(Valht, Math.Round(TemA, 2));
                        else
                            htdat[Valht] = Convert.ToDecimal(htdat[Valht]) + Math.Round(TemA, 2);

                        Valht++;
                        HttpContext.Current.Response.Write("<td style='background-color:" + TabllinColor2 + "; border: 1px solid black;'>" + TemA.ToString("N2") + "</td>");
                        AllowAmount = AllowAmount + TemA;
                    }
                    TotalAmount = TotalAmount + AllowAmount;
                    decimal NOnCTCAmount = 0;
                    foreach (DataRow drN in ds.Tables[4].Rows)
                    {
                        DataRow[] drsN = ds.Tables[5].Select("EMPID='" + dr["EMPID"].ToString() + "' and NonCTCCompID='" + drN["CompID"] + "'");
                        decimal TemN = 0;
                        if (drsN.Length > 0)
                            TemN = Math.Round(Convert.ToDecimal(drsN[0]["Amount"]), 2);
                        if (!htdat.Contains(Valht))
                            htdat.Add(Valht, Math.Round(TemN, 2));
                        else
                            htdat[Valht] = Convert.ToDecimal(htdat[Valht]) + Math.Round(TemN, 2);
                        Valht++;
                        HttpContext.Current.Response.Write("<td style='background-color:" + TabllinColor2 + "; border: 1px solid black;'>" + TemN.ToString("N2") + "</td>");
                        NOnCTCAmount = NOnCTCAmount + TemN;
                    }
                    TotalAmount = TotalAmount + NOnCTCAmount;

                    DiffActaulNetAmount = TotalAmount - ActaulNetAmount;
                    if (DiffActaulNetAmount > 0)
                    { }
                    if (!htdat.Contains(Valht))
                        htdat.Add(Valht, Math.Round(TotalAmount, 2));
                    else
                        htdat[Valht] = Convert.ToDecimal(htdat[Valht]) + Math.Round(TotalAmount, 2);

                    Valht++;

                    HttpContext.Current.Response.Write("<td style='background-color:" + TabllinColor2 + "; border: 1px solid black;'>" + TotalAmount.ToString("N2") + "</td>");
                    DataRow[] ovrs = ds.Tables[6].Select("EMPID='" + dr["EMPID"].ToString() + "'");
                    decimal TempOTHr = 0, TempOTAmt = 0;
                    if (ovrs.Length > 0)
                    {
                        TempOTHr = Convert.ToDecimal(ovrs[0]["OTH"]);
                        TempOTAmt = Convert.ToDecimal(ovrs[0]["OTAmount"]);
                            //Convert.ToDecimal(ovrs[0]["OTH"]) * Convert.ToDecimal(ovrs[0]["Rate"]);

                    }
                    if (!htdat.Contains(Valht))
                        htdat.Add(Valht, Math.Round(TempOTHr, 0));
                    else
                        htdat[Valht] = Convert.ToDecimal(htdat[Valht]) + Math.Round(TempOTHr, 0);
                    Valht++;
                    if (!htdat.Contains(Valht))
                        htdat.Add(Valht, Math.Round(TempOTAmt, 2));
                    else
                        htdat[Valht] = Convert.ToDecimal(htdat[Valht]) + Math.Round(TempOTAmt,2);
                    Valht++;
                    if (!htdat.Contains(Valht))
                        htdat.Add(Valht, Math.Round(SpecialAmt,2));
                    else
                        htdat[Valht] = Convert.ToDecimal(htdat[Valht]) + Math.Round(SpecialAmt,2);
                    Valht++;
                    HttpContext.Current.Response.Write("<td style='background-color:" + TabllinColor + "; border: 1px solid black;'>" + TempOTHr.ToString("N2") + "</td>");
                    HttpContext.Current.Response.Write("<td style='background-color:" + TabllinColor + "; border: 1px solid black;'>" + TempOTAmt.ToString("N2") + "</td>");
                    HttpContext.Current.Response.Write("<td style='background-color:" + TabllinColor + "; border: 1px solid black;'>" + SpecialAmt.ToString("N2") + "</td>");
                    TotalColumn = TotalColumn + (float)TotalAmount;
                    DataRow[] AbPnalities = ds.Tables[8].Select("EMPID='" + dr["EMPID"].ToString() + "'");
                    int APDays = 0; decimal APAMount = 0; int Pendays = 0; decimal PenAMount = 0;
                    if (AbPnalities.Length > 0)
                    {
                        Pendays = 0;
                        APDays = Convert.ToInt32(AbPnalities[0]["AbsentDays"]);
                        APAMount = Math.Round(Convert.ToDecimal(AbPnalities[0]["AbsentAmount"]), 2);
                        Pendays = Convert.ToInt32(AbPnalities[0]["PenDays"]);
                        PenAMount = Math.Round(Convert.ToDecimal(AbPnalities[0]["PenAmount"]), 2);
                    }

                    Absvalue = Absvalue + (DiffActaulNetAmount + APAMount);
                    if (!htdat.Contains(Valht))
                        htdat.Add(Valht, (APDays));
                    else
                        htdat[Valht] = Convert.ToDecimal(htdat[Valht]) + (APDays);
                    // Valht++;
                    Valht++;
                    if (!htdat.Contains(Valht))
                        htdat.Add(Valht, (Pendays));
                    else
                        htdat[Valht] = Convert.ToDecimal(htdat[Valht]) + (Pendays);
                    // Valht++;
                    Valht++;
                    if (!htdat.Contains(Valht))
                        htdat.Add(Valht, Math.Round((APAMount), 2));
                    else
                        htdat[Valht] = Convert.ToDecimal(htdat[Valht]) + Math.Round((APAMount), 2);
                    Valht++;
                    if (!htdat.Contains(Valht))
                        htdat.Add(Valht, Math.Round((PenAMount), 2));
                    else
                        htdat[Valht] = Convert.ToDecimal(htdat[Valht]) + Math.Round((PenAMount), 2);
                    Valht++;    
                    DataRow[] gosi = ds.Tables[11].Select("EMPID='" + dr["EMPID"].ToString() + "'");
                    decimal Tempgosi = 0;
                    if (gosi.Length > 0)
                    {
                        Tempgosi = Convert.ToDecimal(gosi[0]["GOSIAmount"]);
                    }
                    if (!htdat.Contains(Valht))
                        htdat.Add(Valht, 0);
                    else
                        htdat[Valht] = Convert.ToDecimal(htdat[Valht]) + Tempgosi;
                    Valht++;

                    HttpContext.Current.Response.Write("<td style='background-color:" + TabllinColor2 + "; border: 1px solid black;'>" + (APDays).ToString("N0") + "</td>");
                    HttpContext.Current.Response.Write("<td style='background-color:" + TabllinColor2 + "; border: 1px solid black;'>" + (Pendays).ToString("N0") + "</td>");
                    HttpContext.Current.Response.Write("<td style='background-color:" + TabllinColor2 + "; border: 1px solid black;'>" + (APAMount).ToString("N2") + "</td>");
                    HttpContext.Current.Response.Write("<td style='background-color:" + TabllinColor2 + "; border: 1px solid black;'>" + (PenAMount).ToString("N2") + "</td>");
                    HttpContext.Current.Response.Write("<td style='background-color:" + TabllinColor2 + "; border: 1px solid black;'>" + Tempgosi + "</td>");
                    decimal loansandadva = 0;
                    try
                    {
                        DataRow[] londadddr = ds.Tables[10].Select("EMPID='" + dr["EMPID"].ToString() + "'");
                        if (londadddr.Length > 0)
                            loansandadva = Convert.ToDecimal(londadddr[0]["LoanAmt"]);
                    }
                    catch (Exception)
                    {
                    }
                    if (!htdat.Contains(Valht))
                        htdat.Add(Valht, Math.Round(loansandadva, 2));
                    else
                        htdat[Valht] = Convert.ToDecimal(htdat[Valht]) + Math.Round(loansandadva, 2);
                    Valht++;
                    if (!htdat.Contains(Valht))
                        htdat.Add(Valht, EMPPanalities);
                    else
                        htdat[Valht] = Convert.ToDecimal(htdat[Valht]) + EMPPanalities;
                    decimal TotalDed = (loansandadva + EMPPanalities + APAMount + PenAMount + Tempgosi);
                    Valht++;
                    if (!htdat.Contains(Valht))
                        htdat.Add(Valht, TotalDed);
                    else
                        htdat[Valht] = Convert.ToDecimal(htdat[Valht]) + TotalDed;

                    Valht++;
                    if (!htdat.Contains(Valht))
                        htdat.Add(Valht, AdjustAmount);
                    else
                        htdat[Valht] = Convert.ToDecimal(htdat[Valht]) + AdjustAmount;
                    decimal finalNetamt = Math.Ceiling(ActaulNetAmount); 
                    if (finalNetamt < 0)
                        finalNetamt = 0;

                    Valht++;
                    if (!htdat.Contains(Valht))
                        htdat.Add(Valht, finalNetamt);
                    else
                        htdat[Valht] = Convert.ToDecimal(htdat[Valht]) + finalNetamt;
                    HttpContext.Current.Response.Write("<td style='background-color:" + TabllinColor2 + "; border: 1px solid black;'>" + loansandadva + "</td>");
                    HttpContext.Current.Response.Write("<td style='background-color:" + TabllinColor2 + "; border: 1px solid black;'>" + EMPPanalities.ToString("N2") + "</td>");
                    HttpContext.Current.Response.Write("<td style='background-color:" + TabllinColor + "; border: 1px solid black;'>" + TotalDed.ToString("N2") + "</td>");
                    HttpContext.Current.Response.Write("<td style='background-color:" + TabllinColor + "; border: 1px solid black;'>" + AdjustAmount.ToString("N2") + "</td>");
                    HttpContext.Current.Response.Write("<td style='background-color:" + TabllinColor + "; border: 1px solid black;'>" + finalNetamt.ToString("N2") + "</td>");

                    HttpContext.Current.Response.Write("</tr>");
                    Valht++;
                    Valfoloop = Valht;
                }
            }
            catch { }
        }

        HttpContext.Current.Response.Write("<tr style='color:Black; background-color:White; font-weight:bold; font-size:20px;   border-bottom-width:medium;'>");

        for (int i = 1; i <= Valfoloop; i++)
        {
            try
            {
                if (htdat.Contains(i))
                    if (i > 6)
                        HttpContext.Current.Response.Write("<td style='background-color:" + TabllinColor2 + "; border: 1px solid black;'>" + Convert.ToDecimal(htdat[i]).ToString("N0") + "</td>");
                    else
                        HttpContext.Current.Response.Write("<td style='background-color:" + TabllinColor2 + "; border: 1px solid black;'>" + htdat[i].ToString() + "</td>");
                else
                    HttpContext.Current.Response.Write("<td>" + "" + "</td>");
            }
            catch { }
        }
        HttpContext.Current.Response.Write("</tr>");

       
        HttpContext.Current.Response.Write("</table>");

        sqlDataReader.Close();

        HttpContext.Current.Response.End();

    }

    public static void ExportToFile(String FileName, String ContentType, String Head, String SubHead, String Titil, DataSet ds, DateTime StartDate, DateTime EndDate)
    {

        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + FileName);
        HttpContext.Current.Response.Charset = "";
        HttpContext.Current.Response.ContentType = ContentType;
        HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.UTF8;

        string strLine;

        HttpContext.Current.Response.Write("<table>");


        int CountClounms = 35;
        if (Titil.Trim() != "")
        {
            HttpContext.Current.Response.Write("<tr style='color:Black; background-color:White; font-weight:bold;  font-size:30px; align-content:center; border-bottom-width:medium;'>");
            HttpContext.Current.Response.Write("<td colspan=" + CountClounms + " >" + "Monthly Attendance Of : " + Titil.ToString() + "</td>");//colspan=" + CountClounms + " 
            HttpContext.Current.Response.Write("</tr>");

        }
        if (Head.Trim() != "")
        {
            HttpContext.Current.Response.Write("<tr style='color:Black; background-color:White; font-weight:bold; font-size:26px; align-content:center; border-bottom-width:medium;'>");
            HttpContext.Current.Response.Write("<td colspan=" + CountClounms + ">" + Head.ToString() + "</td>");//colspan=" + CountClounms + " 
            HttpContext.Current.Response.Write("</tr>");

        }
        if (SubHead.Trim() != "")
        {
            HttpContext.Current.Response.Write("<tr style='Black:White; background-color:White; font-weight:bold;font-size:22px; align-content:center; border-bottom-width:medium; '>");
            HttpContext.Current.Response.Write("<td colspan=" + CountClounms + ">" + SubHead.ToString() + "</td>");// colspan=" + CountClounms + "
            HttpContext.Current.Response.Write("</tr>");

        }


        Boolean isFirst = true;
        int DeptID = 0;
        System.Collections.Hashtable ht = new System.Collections.Hashtable();
        String HeadText = "";
        String DepaartText = "";
        String RowText = "";
        DateTime dt = StartDate; 
        
        int DayInterval = 1;
        foreach (DataRow drEMP in ds.Tables[2].Rows)
        {

            HeadText = "";
            DepaartText = "";
            RowText = "<tr>";
            HeadText = "<tr>";
           
            ht = new System.Collections.Hashtable();
            if (isFirst)
                HeadText = HeadText + "<td Style='font-weight:bold; border;solid navy 1px;width;300px; '>Name</td>";
            if (DeptID != Convert.ToInt32(drEMP["DepID"]))
            {
                DepaartText = DepaartText + "<tr>";
                DepaartText = DepaartText + "<td Style='font-weight:bold; border;solid navy 1px; '>" + drEMP["DepName"].ToString() + "</td>";
                DeptID = Convert.ToInt32(drEMP["DepID"]);
                DepaartText = DepaartText + "</tr>";
            }
            RowText = RowText + "<td Style=' border;solid navy 1px; '>" + drEMP["Name"].ToString() + "</td>";
            StartDate = dt;
            while (StartDate.AddDays(DayInterval - 1) < EndDate)
            {
                if (isFirst)
                {
                    HeadText = HeadText + "<td Style='font-weight:bold; background-color:#87cefa;text-align:center; border;solid navy 1px; '>" + StartDate.Day.ToString() + "</td>";
                }
                try
                {
                   

                    DataRow[] drsAtt = ds.Tables[1].Select("Date = '" + StartDate + "' and EMPID='" + drEMP["ID"] + "'");
                    if (drsAtt.Length > 0)
                    {

                        switch (Convert.ToInt32(drsAtt[0]["Status"]))
                        {
                            case 1:
                                RowText = RowText + "<td Style='font-weight:bold; background-color:#87cefa; color:red; text-align:center; border;solid navy 1px; '>" + drsAtt[0]["ShortName"].ToString() + "</td>";
                                break;
                            case 2:
                                RowText = RowText + "<td Style='font-weight:bold; background-color:#87cefa; color:green; text-align:center; border;solid navy 1px; '>" + drsAtt[0]["ShortName"].ToString() + "</td>";
                                break;
                            case 7:
                                RowText = RowText + "<td Style='font-weight:bold; background-color:#87cefa; color:green; text-align:center; border;solid navy 1px; '>" + drsAtt[0]["ShortName"].ToString() + "</td>";
                                break;
                            case 8:
                                RowText = RowText + "<td Style='font-weight:bold; background-color:#87cefa; color:green; text-align:center; border;solid navy 1px; '>" + drsAtt[0]["ShortName"].ToString() + "</td>";
                                break;
                            case 9:
                                RowText = RowText + "<td Style='font-weight:bold; background-color:#87cefa; color:Blue; text-align:center; border;solid navy 1px; '>" + drsAtt[0]["ShortName"].ToString() + "</td>";
                                break;
                            default:
                                RowText = RowText + "<td Style='font-weight:bold; background-color:#87cefa; color:Gray; text-align:center; border;solid navy 1px; '>" + drsAtt[0]["ShortName"].ToString() + "</td>";
                                break;
                                
                        }
                        if (ht.ContainsKey(drsAtt[0]["Status"]))
                            ht[drsAtt[0]["Status"]] = Convert.ToInt32(ht[drsAtt[0]["Status"]]) + 1;
                        else
                            ht.Add(drsAtt[0]["Status"], 1);
                    }
                    else
                        RowText = RowText + "<td Style='font-weight:bold; background-color:#87cefa; color:red; text-align:center; border;solid navy 1px; '>" + "-" + "</td>";

                    if (ht.ContainsKey(0))
                        ht[0] = Convert.ToInt32(ht[0]) + 1;
                    else
                        ht.Add(0, 1);
                    StartDate = StartDate.AddDays(DayInterval);
                }
                catch { }
            }
            if (isFirst)
                HeadText = HeadText + "<td Style='font-weight:bold; color:Green; text-align:center; border;solid navy 1px; '>" + "Scope" + "</td>";
            string ValueNo = "0";
            if (ht.ContainsKey(0))
                ValueNo = ht[0].ToString();
            RowText = RowText + "<td Style='font-weight:bold; color:Green; text-align:center; border;solid navy 1px; '>" + ValueNo.ToString() + "</td>";

            foreach (DataRow drAM in ds.Tables[0].Rows)
            {
                if (isFirst)
                {
                    string Namestring = drAM["Name"].ToString();
                    switch (Convert.ToInt32(drAM["ID"]))
                    {
                        case 0:
                            HeadText = HeadText + "<td Style='font-weight:bold; background-color:#A1F9DB; color:Green; text-align:center; border;solid navy 1px; '>" + Namestring.ToString() + "</td>";
                            break;
                        case 1:
                            HeadText = HeadText + "<td Style='font-weight:bold; background-color:#A1F9DB; color:red; text-align:center; border;solid navy 1px; '>" + Namestring.ToString() + "</td>";
                            break;
                        case 2:
                            HeadText = HeadText + "<td Style='font-weight:bold; background-color:#A1F9DB; color:Green; text-align:center; border;solid navy 1px; '>" + Namestring.ToString() + "</td>";
                            break;
                        case 7:
                            HeadText = HeadText + "<td Style='font-weight:bold; background-color:#A1F9DB; color:Green; text-align:center; border;solid navy 1px; '>" + Namestring.ToString() + "</td>";
                            break;
                        case 8:
                            HeadText = HeadText + "<td Style='font-weight:bold; background-color:#A1F9DB; color:Green; text-align:center; border;solid navy 1px; '>" + Namestring.ToString() + "</td>";
                            break;
                        case 9:
                            HeadText = HeadText + "<td Style='font-weight:bold; background-color:#A1F9DB; color:Blue; text-align:center; border;solid navy 1px; '>" + Namestring.ToString() + "</td>";
                            break;
                        default:
                            HeadText = HeadText + "<td Style='font-weight:bold; background-color:#A1F9DB; color:Gray; text-align:center; border;solid navy 1px; '>" + Namestring.ToString() + "</td>";
                            break;
                    }

                }
                ValueNo = "0";
                if (ht.ContainsKey(drAM["ID"]))
                    ValueNo = ht[drAM["ID"]].ToString();
                switch (Convert.ToInt32(drAM["ID"]))
                {
                    case 0:
                        RowText = RowText + "<td Style='font-weight:bold; background-color:#A1F9DB; color:Green; text-align:center; border;solid navy 1px; '>" + ValueNo.ToString() + "</td>";
                        break;
                    case 1:
                        RowText = RowText + "<td Style='font-weight:bold; background-color:#A1F9DB; color:Red; text-align:center; border;solid navy 1px; '>" + ValueNo.ToString() + "</td>";
                        break;
                    case 2:
                        RowText = RowText + "<td Style='font-weight:bold; background-color:#A1F9DB; color:Green; text-align:center; border;solid navy 1px; '>" + ValueNo.ToString() + "</td>";
                        break;
                    case 7:
                        RowText = RowText + "<td Style='font-weight:bold; background-color:#A1F9DB; color:Green; text-align:center; border;solid navy 1px; '>" + ValueNo.ToString() + "</td>";
                        break;
                    case 8:
                        RowText = RowText + "<td Style='font-weight:bold; background-color:#A1F9DB; color:Green; text-align:center; border;solid navy 1px; '>" + ValueNo.ToString() + "</td>";
                        break;
                    case 9:
                        RowText = RowText + "<td Style='font-weight:bold; background-color:#A1F9DB; color:Blue; text-align:center; border;solid navy 1px; '>" + ValueNo.ToString() + "</td>";
                        break;
                    default:
                        RowText = RowText + "<td Style='font-weight:bold; background-color:#A1F9DB; color:Gray; text-align:center; border;solid navy 1px; '>" + ValueNo.ToString() + "</td>";
                        break;
                }

            }
            if (isFirst)
            { HeadText = HeadText + "</tr>"; HttpContext.Current.Response.Write(HeadText); }
            if (DepaartText.Trim() != "")
                HttpContext.Current.Response.Write(DepaartText);
            RowText = RowText + "</tr>";
            HttpContext.Current.Response.Write(RowText);
            isFirst = false;
        }


        HttpContext.Current.Response.Write("</table>");

        ds.Clear();

        HttpContext.Current.Response.End();

    }


    internal static void ExportToFileRev43(DataSet sqlDataSet, String ItemColor, String AltItemColor, String CrossItemColor, String FileName, String ContentType, String Head, String SubHead, String Titil, DataSet ds)
    {

        
        //Clear the response
        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + FileName);
        HttpContext.Current.Response.Charset = "";
        HttpContext.Current.Response.ContentType = ContentType;
        HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.UTF8;
        HttpContext.Current.Response.Write("<style>  .txt " + "\r\n" + " {mso-style-parent:style0;mso-number-format:\"" + @"\@" + "\"" + ";} " + "\r\n" + "</style>");
        //Initialize the string that is used to build the file.
        HttpContext.Current.Response.Write("<table>");
        //Enumerate the field names and the records that are used to build 
        //the file.
        int CountSalary = sqlDataSet.Tables[0].Rows.Count;

        int CountClounms = CountSalary;

        if (Head.Trim() != "")
        {
            HttpContext.Current.Response.Write("<tr style='color:Black; background-color:White; font-weight:bold; font-size:26px; align-content:center; border-bottom-width:medium;'>");
            HttpContext.Current.Response.Write("<td colspan=" + CountClounms + ">" + Head.ToString() + "</td>");//colspan=" + CountClounms + " 
            HttpContext.Current.Response.Write("</tr>");
        }

        HttpContext.Current.Response.Write("<tr style='color:White; font-weight:bold; font-size:20px; border-bottom-width:medium;'>");


        for (int i = 0; i <= CountClounms - 1; i++)
        {
            HttpContext.Current.Response.Write("<td Style='font-weight:bold; background-color:#87cefa;text-align:center; border;solid navy 1px; '>" + sqlDataSet.Tables[0].Rows[i][1].ToString() + "</td>");
        }

        HttpContext.Current.Response.Write("</tr>");

        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {//write in new row
            HttpContext.Current.Response.Write("<tr>");
            for (int j = 0; j < ds.Tables[0].Columns.Count; j++)
            {
                if (j == 4 || j == ds.Tables[0].Columns.Count-2)
                    HttpContext.Current.Response.Write("<td class='txt' Style='border: 1px solid black;Black:White; background-color:White;'>");
                else 
                    HttpContext.Current.Response.Write("<td Style='border: 1px solid black;Black:White; background-color:White;'>");
                HttpContext.Current.Response.Write(ds.Tables[0].Rows[i][j].ToString());
                HttpContext.Current.Response.Write("</td>");

            }

            HttpContext.Current.Response.Write("</tr>");
        }
        HttpContext.Current.Response.Write("</table>");

        HttpContext.Current.Response.End();

    }
    internal static void ExportToFileRev42(DataSet sqlDataSet, String ItemColor, String AltItemColor, String CrossItemColor, String FileName, String ContentType, String Head, String SubHead, String Titil, DataSet ds)
    {


        //Clear the response
        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + FileName);
        HttpContext.Current.Response.Charset = "";
        HttpContext.Current.Response.ContentType = ContentType;
        HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.UTF8;

        //Initialize the string that is used to build the file.
        HttpContext.Current.Response.Write("<table>");
        //Enumerate the field names and the records that are used to build 
        //the file.
        int CountSalary = sqlDataSet.Tables[0].Columns.Count;
        int CountClounms = CountSalary;

        if (Head.Trim() != "")
        {
            HttpContext.Current.Response.Write("<tr style='color:Black; background-color:White; font-weight:bold; font-size:26px; align-content:center; border-bottom-width:medium;'>");
            HttpContext.Current.Response.Write("<td colspan=" + CountClounms + ">" + Head.ToString() + "</td>");//colspan=" + CountClounms + " 
            HttpContext.Current.Response.Write("</tr>");
        }

        HttpContext.Current.Response.Write("<tr style='color:White;  font-weight:bold; font-size:20px; border-bottom-width:medium;'>");


        for (int i = 0; i <= CountClounms - 1; i++)
        {
            HttpContext.Current.Response.Write("<td Style='font-weight:bold; background-color:#87cefa;text-align:center; border;solid navy 1px; '>" + sqlDataSet.Tables[0].Rows[0][i].ToString() + "</td>");
        }

        HttpContext.Current.Response.Write("</tr>");
        string TabllinColor = "";
       
        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {//write in new row
            if (i % 2 == 0)
            {
                TabllinColor = "#EBF5FB";
                
            }
            else
            {
                TabllinColor = "#AED6F1"; 
            }
            HttpContext.Current.Response.Write("<tr>");
            for (int j = 0; j < ds.Tables[0].Columns.Count; j++)
            {

                HttpContext.Current.Response.Write("<td Style='border: 1px solid black;Black:White; background-color:" + TabllinColor + ";'>");
                HttpContext.Current.Response.Write(ds.Tables[0].Rows[i][j].ToString());
                HttpContext.Current.Response.Write("</td>");

            }

            HttpContext.Current.Response.Write("</TR>");
        }
        HttpContext.Current.Response.Write("</table>");

        HttpContext.Current.Response.End();

    }


    #endregion SQLDataReader


    #region DataTable

    public static void ExportToFile(DataTable dataTable, String ItemColor, String AltItemColor, String FileName, String ContentType)
    {
        ExportToFile(dataTable, ItemColor, AltItemColor, "", FileName, ContentType);
    }

    public static void ExportToFile(DataTable dataTable, String ItemColor, String FileName, String ContentType)
    {
        ExportToFile(dataTable, ItemColor, "", "", FileName, ContentType);
    }

    public static void ExportToFile(DataTable dataTable, String FileName, String ContentType)
    {
        ExportToFile(dataTable, "", "", "", FileName, ContentType);
    }

    public static void ExportToExcel(DataTable dataTable, String ItemColor, String AltItemColor, String FileName)
    {
        ExportToFile(dataTable, ItemColor, AltItemColor, "", FileName + ".xls", "application/vnd.xls");
    }

    public static void ExportToExcel(DataTable dataTable, String ItemColor, String AltItemColor, String CrossItemColor, String FileName)
    {
        ExportToFile(dataTable, ItemColor, AltItemColor, CrossItemColor, FileName + ".xls", "application/vnd.xls");
    }

    public static void ExportToExcel(DataTable dataTable, String ItemColor, String FileName)
    {
        ExportToFile(dataTable, ItemColor, "", "", FileName + ".xls", "application/vnd.xls");
    }

    public static void ExportToExcel(DataTable dataTable, String FileName)
    {
        ExportToFile(dataTable, "", "", "", FileName + ".xls", "application/vnd.xls");
    }

    public static void ExportToPDF(DataTable dataTable, String ItemColor, String AltItemColor, String FileName)
    {
        ExportToFile(dataTable, ItemColor, AltItemColor, "", FileName + ".pdf", "application/pdf");
    }

    public static void ExportToPDF(DataTable dataTable, String ItemColor, String FileName)
    {
        ExportToFile(dataTable, ItemColor, "", "", FileName + ".pdf", "application/pdf");
    }

    public static void ExportToPDF(DataTable dataTable, String FileName)
    {
        ExportToFile(dataTable, "", "", "", FileName + ".pdf", "application/pdf");
    }

    public static void ExportToWord(DataTable dataTable, String ItemColor, String AltItemColor, String FileName)
    {
        ExportToFile(dataTable, ItemColor, AltItemColor, "", FileName + ".doc", "application/ms-word");
    }

    public static void ExportToWord(DataTable dataTable, String ItemColor, String FileName)
    {
        ExportToFile(dataTable, ItemColor, "", "", FileName + ".doc", "application/ms-word");
    }

    public static void ExportToWord(DataTable dataTable, String FileName)
    {
        ExportToFile(dataTable, "", "", "", FileName + ".doc", "application/ms-word");
    }


    public static void ExportToFile(DataTable dataTable, String ItemColor, String AltItemColor, String CrossItemColor, String FileName, String ContentType)
    {

        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + FileName);
        HttpContext.Current.Response.Charset = "";
        HttpContext.Current.Response.ContentType = ContentType;
        HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.UTF8;

        string strLine;

        HttpContext.Current.Response.Write("<table>");
        HttpContext.Current.Response.Write("<tr style='color:White; background-color:Navy; font-weight:bold;'>");

        foreach (DataColumn dataColumn in dataTable.Columns)
        {
            if(dataColumn.Caption.Equals(""))
                HttpContext.Current.Response.Write("<td>" + dataColumn.ColumnName + "</td>");
            else
                HttpContext.Current.Response.Write("<td>" + dataColumn.Caption + "</td>");
        }

        HttpContext.Current.Response.Write("</tr>");

        if (ItemColor == "")
            ItemColor = "#ffffff";
        if (AltItemColor == "")
            AltItemColor = "#ffffff";

        int cnt = 0;
        foreach (DataRow dataRow in dataTable.Rows)
        {

            HttpContext.Current.Response.Write("<tr>");


            for (int i = 0; i <= dataTable.Columns.Count - 1; i++)
            {
                if (cnt % 2 == 0)
                {
                    if (i % 2 == 0)
                        HttpContext.Current.Response.Write("<td style='background-color:" + ItemColor + ";'>" + dataRow[dataTable.Columns[i]].ToString() + "</td>");
                    else
                        HttpContext.Current.Response.Write("<td  style='background-color:" + AltItemColor + ";'>" + dataRow[dataTable.Columns[i]].ToString() + "</td>");
                }
                else
                {
                    if (i % 2 == 0)
                    {

                        HttpContext.Current.Response.Write("<td  style='background-color:" + (CrossItemColor == "" ? ItemColor : AltItemColor) + ";'>" + dataRow[dataTable.Columns[i]].ToString() + "</td>");
                    }
                    else
                    {
                        HttpContext.Current.Response.Write("<td   style='background-color:" + (CrossItemColor == "" ? AltItemColor : CrossItemColor) + ";'>" + dataRow[dataTable.Columns[i]].ToString() + "</td>");
                    }
                }
            }
            HttpContext.Current.Response.Write("</tr>");
            cnt++;
        }

        HttpContext.Current.Response.Write("</table>");

        //Clean up.

        HttpContext.Current.Response.End();

    }
    #endregion DataTable



    
}