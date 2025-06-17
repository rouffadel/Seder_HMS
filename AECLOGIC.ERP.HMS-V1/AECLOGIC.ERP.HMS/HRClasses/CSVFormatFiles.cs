using Aeclogic.Common.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;

namespace AECLOGIC.ERP.HMS.HRClasses
{
    public class CSVFormatFiles
    {

        public static void ExportGridToCSV(DataSet ds, string fileName)
        {

            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.Buffer = true;
            HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + fileName + ".csv");
            HttpContext.Current.Response.Charset = "";
            HttpContext.Current.Response.ContentType = "application/text";


            StringBuilder columnbind = new StringBuilder();
            foreach (DataColumn col in ds.Tables[0].Columns)
            {
                columnbind.Append(col.Caption + ',');
            }
            columnbind.Append("\r\n");
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                foreach (DataColumn col in ds.Tables[0].Columns)
                {
                    columnbind.Append(dr[col.ColumnName].ToString() + ',');
                }
                columnbind.Append("\r\n");
            }



            //for (int k = 0; k < GridView1.Columns.Count; k++)
            //{

            //    columnbind.Append(GridView1.Columns[k].HeaderText + ',');
            //}

            //columnbind.Append("\r\n");
            //for (int i = 0; i < GridView1.Rows.Count; i++)
            //{
            //    for (int k = 0; k < GridView1.Columns.Count; k++)
            //    {

            //        columnbind.Append(GridView1.Rows[i].Cells[k].Text + ',');
            //    }

            //    columnbind.Append("\r\n");
            //}
            HttpContext.Current.Response.Output.Write(columnbind.ToString());
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.End();

        }

        public static void WriteHeader(DataSet ds, ref StringBuilder columnbind)
        {
            int CountColNames = 5;
            int CountSalary = ds.Tables[0].Rows.Count + // Wag
                ds.Tables[2].Rows.Count + // alow
                ds.Tables[4].Rows.Count + // Non
                +1;
            int CountColAdditiONAL = 4;
            int CountColdEDUCTION = 7;

            int CountClounms = CountColNames + CountSalary + CountColAdditiONAL + CountColdEDUCTION + 1;
            columnbind.Append("Sl No,EMPID,Employee,No WD,No PD,Adj Days,Salary And Fees,");
            for (int i = 0; i < CountSalary - 1; i++)
                columnbind.Append(',');
            columnbind.Append("Additions,");
            for (int i = 0; i < CountColAdditiONAL - 2; i++)
                columnbind.Append(',');
            columnbind.Append("Deductions,");
            for (int i = 0; i < CountColdEDUCTION - 1; i++)
                columnbind.Append(',');
            columnbind.Append("\r\n");
            columnbind.Append(",,,,,,");
            foreach (DataRow dr in ds.Tables[0].Rows)
                columnbind.Append(dr["LongName"].ToString() + ",");
            foreach (DataRow dr in ds.Tables[2].Rows)
                columnbind.Append(dr["LongName"].ToString() + ",");
            foreach (DataRow dr in ds.Tables[4].Rows)
                columnbind.Append(dr["LongName"].ToString() + ",");
            columnbind.Append("Total,O H,Over Time,Spl Add,Abs Days,Pen Days,Abs Value,Pen Value,GOSI,Loans,Spl Det,Total,Adjust Amount,Net Amount");
            columnbind.Append("\r\n");
        }
        public static void ExportGridToCSV_All(DataSet ds, string fileName, String MonthYear)
        {

            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.Buffer = true;
            HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + fileName + ".csv");
            HttpContext.Current.Response.Charset = "";
            HttpContext.Current.Response.ContentType = "application/text";
            StringBuilder columnbind = new StringBuilder();
            columnbind.Append(MonthYear + ",");
            columnbind.Append("\r\n");
            int SiteID = 0;
            int SlNo = 0;
            //string TabllinColor = "";
            float TotalColumn = 0;
            decimal Absvalue = 0;
            decimal TotalAdjustAmount = 0;
            System.Collections.Hashtable htdat = null;

            int Valfoloop = 5;
            foreach (DataRow dr in ds.Tables[7].Rows)
            {
                try
                {
                    if (Convert.ToInt32(dr["wsid"]) != SiteID)
                    {

                        SiteID = Convert.ToInt32(dr["wsid"]);
                        Footer_Totals(ref htdat, ref Valfoloop, ref columnbind);
                        columnbind.Append("\r\n");
                        columnbind.Append(dr["Site_Name"].ToString() + ",");
                        columnbind.Append("\r\n");
                        WriteHeader(ds, ref columnbind);
                    }

                    int DifDays = 0; decimal ActaulNetAmount = 0, DiffActaulNetAmount = 0, SpecialAmt = 0, EMPPanalities = 0, AdjustAmount = 0;

                    SlNo++;
                    columnbind.Append(SlNo.ToString() + "," + dr["empidcode"].ToString() + "," + dr["Name"].ToString() + "," + dr["AWD"].ToString() + ","
                        + dr["PWD"].ToString() + "," + dr["AdjDays"].ToString() + ",");


                    DifDays = Convert.ToInt32(dr["DiffD"]);
                    ActaulNetAmount = Convert.ToDecimal(dr["NetAmount"]);
                    AdjustAmount = Convert.ToDecimal(dr["DAmount"]);
                    TotalAdjustAmount = TotalAdjustAmount + AdjustAmount;
                    SpecialAmt = Convert.ToDecimal(dr["SpecialAmt"]);
                    EMPPanalities = Convert.ToDecimal(dr["EMPPanalities"]);

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
                        columnbind.Append(TemWg.ToString("N2") + ",");
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
                        columnbind.Append(TemA.ToString("N2") + ",");
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
                        columnbind.Append(TemN.ToString("N2") + ",");
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

                    columnbind.Append(TotalAmount.ToString("N2") + ",");
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
                        htdat[Valht] = Convert.ToDecimal(htdat[Valht]) + Math.Round(TempOTAmt, 2);
                    Valht++;
                    if (!htdat.Contains(Valht))
                        htdat.Add(Valht, Math.Round(SpecialAmt, 2));
                    else
                        htdat[Valht] = Convert.ToDecimal(htdat[Valht]) + Math.Round(SpecialAmt, 2);
                    Valht++;
                    columnbind.Append(TempOTHr.ToString("N2") + ",");
                    columnbind.Append(TempOTAmt.ToString("N2") + ",");
                    columnbind.Append(SpecialAmt.ToString("N2") + ",");
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

                    columnbind.Append((APDays).ToString("N0") + ",");
                    columnbind.Append((Pendays).ToString("N0") + ",");
                    columnbind.Append((APAMount).ToString("N2") + ",");
                    columnbind.Append((PenAMount).ToString("N2") + ",");
                    columnbind.Append(Tempgosi + ",");
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
                    columnbind.Append(loansandadva + ",");
                    columnbind.Append(EMPPanalities.ToString("N2") + ",");
                    columnbind.Append(TotalDed.ToString("N2") + ",");
                    columnbind.Append(AdjustAmount.ToString("N2") + ",");
                    columnbind.Append(finalNetamt.ToString("N2") + ",");


                    Valht++;
                    Valfoloop = Valht;





                    columnbind.Append("\r\n");
                }
                catch { }
            }

            //foreach (DataColumn col in ds.Tables[0].Columns)
            //{
            //    columnbind.Append(col.Caption + ',');
            //}
            //columnbind.Append("\r\n");
            //foreach (DataRow dr in ds.Tables[0].Rows)
            //{
            //    foreach (DataColumn col in ds.Tables[0].Columns)
            //    {
            //        columnbind.Append(dr[col.ColumnName].ToString() + ',');
            //    }
            //    columnbind.Append("\r\n");
            //}




            HttpContext.Current.Response.Output.Write(columnbind.ToString());
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.End();

        }

        private static void Footer_Totals(ref System.Collections.Hashtable htdat, ref int Valfoloop, ref StringBuilder columnbind)
        {
            if (htdat != null)
            {
                for (int i = 1; i <= Valfoloop; i++)
                {
                    try
                    {
                        if (htdat.Contains(i))
                            if (i > 6)
                                columnbind.Append(Convert.ToDecimal(htdat[i]).ToString("N0") + ",");
                            // HttpContext.Current.Response.Write("<td style='background-color:" + TabllinColor2 + "; border: 1px solid black;'>" + Convert.ToDecimal(htdat[i]).ToString("N0") + "</td>");
                            else
                                columnbind.Append(htdat[i].ToString() + ",");
                        // HttpContext.Current.Response.Write("<td style='background-color:" + TabllinColor2 + "; border: 1px solid black;'>" + htdat[i].ToString() + "</td>");
                        else
                            columnbind.Append(",");
                        //HttpContext.Current.Response.Write("<td>" + "" + "</td>");
                    }
                    catch { }
                }
            }
            htdat = new System.Collections.Hashtable();
            htdat.Add(1, "");
            htdat.Add(2, "TOTAL");
            htdat.Add(3, "");
            htdat.Add(4, "");
            htdat.Add(5, "");
            htdat.Add(6, "");
            Valfoloop = 5;
        }

        public static void ExportGridToCSV(string fileName, string MonthYear, int MID, int YID)
        {

            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.Buffer = true;
            HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + fileName + ".csv");
            HttpContext.Current.Response.Charset = "";
            HttpContext.Current.Response.ContentType = "application/text";


            StringBuilder columnbind = new StringBuilder();
            columnbind.Append(MonthYear + ",");
            columnbind.Append("\r\n");
            DataSet dsmain = Leaves.sh_GetWSSummary_ds(MID, YID);
            foreach (DataRow drmain in dsmain.Tables[0].Rows)
            {

                try
                {
                    columnbind.Append(drmain["WSName"] + ",");
                    columnbind.Append("\r\n");
                    DataSet ds = Leaves.USP_Salaries_All_Approved_Search_4(Convert.ToInt32(drmain["WSID"]), MID, YID, 1, 10000);
                    foreach (DataColumn col in ds.Tables[0].Columns)
                    {
                        columnbind.Append(col.Caption + ',');
                    }
                    columnbind.Append("\r\n");
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        foreach (DataColumn col in ds.Tables[0].Columns)
                        {
                            columnbind.Append(dr[col.ColumnName].ToString() + ',');
                        }
                        columnbind.Append("\r\n");
                    }
                    columnbind.Append("\r\n");
                    columnbind.Append("\r\n");
                }
                catch { }
            }


            //for (int k = 0; k < GridView1.Columns.Count; k++)
            //{

            //    columnbind.Append(GridView1.Columns[k].HeaderText + ',');
            //}

            //columnbind.Append("\r\n");
            //for (int i = 0; i < GridView1.Rows.Count; i++)
            //{
            //    for (int k = 0; k < GridView1.Columns.Count; k++)
            //    {

            //        columnbind.Append(GridView1.Rows[i].Cells[k].Text + ',');
            //    }

            //    columnbind.Append("\r\n");
            //}
            HttpContext.Current.Response.Output.Write(columnbind.ToString());
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.End();

        }


    }
}