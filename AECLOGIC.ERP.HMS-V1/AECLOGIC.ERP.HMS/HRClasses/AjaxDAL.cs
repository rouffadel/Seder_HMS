using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Configuration;
using System.Collections;
using DataAccessLayer;
using System.Data.SqlClient;
using AECLOGIC.HMS.BLL;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.IO;
using System.Text;
using Aeclogic.Common.DAL;

/// <summary>
public class AjaxDAL
{
    /// Summary description for AjaxDAL
    /// </summary>
    AttendanceDAC objAtt = new AttendanceDAC();

    public AjaxDAL()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    [Ajax.AjaxMethod()]
    public DataRow GetEmployee(string EmpId)
    {
        try
        {
           
            DataSet ds= SQLDBUtil.ExecuteDataset("HR_GetEmployeeOC", new SqlParameter[] { new SqlParameter("@EmpId", EmpId) });
            return ds.Tables[0].Rows[0];
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    [Ajax.AjaxMethod()]
    public string UpdateVS(string ID, bool Access)
    {
        string returnString = "Changed";
        try
        {
            SQLDBUtil.ExecuteNonQuery("sh_UpdateVSVacationConfig", new SqlParameter[] { new SqlParameter("@ID", ID), new SqlParameter("@Allowed", Access) });

        }
        catch (Exception ex)
        {
            returnString = ex.Message;
        }
        return returnString;
    }

    [Ajax.AjaxMethod()]
    public string UpdateFS(string ID, bool Access)
    {
        string returnString = "Changed";
        try
        {
            SQLDBUtil.ExecuteNonQuery("sh_UpdateFSVacationConfig", new SqlParameter[] { new SqlParameter("@ID", ID), new SqlParameter("@Allowed", Access) });

        }
        catch (Exception ex)
        {
            returnString = ex.Message;
        }
        return returnString;
    }


    [Ajax.AjaxMethod()]
    public DataRow GetClientEmployee(string EmpId)
    {
        try
        {
           
            DataSet ds= SQLDBUtil.ExecuteDataset("CMS_GetClientEmployeeCOC", new SqlParameter[] { new SqlParameter("@EmpId", EmpId) });
            return ds.Tables[0].Rows[0];
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    [Ajax.AjaxMethod()]
    public string UpdateWages(string EmpId, string WagesId, bool Access)
    {
        string returnString = "Changed";
        try
        {
            SQLDBUtil.ExecuteNonQuery("HR_InsUpd_EmpWages", new SqlParameter[] { new SqlParameter("@EmpId", EmpId), new SqlParameter("@WagesID", WagesId), new SqlParameter("@IsActive", Access) });

        }
        catch (Exception ex)
        {
            returnString = ex.Message;
        }
        return returnString;
    }

    [Ajax.AjaxMethod()]
    public string UpdateWagesPercent(string EmpId, string WagesId, bool Access, string CentageID, string UserID, string Centage, string EmpSalID, string empsaltext)
    {
        if (Centage == "" || Centage == null)
        {
            Centage = "0";
        }
        string returnString = "Changed";
        try
        {
            SQLDBUtil.ExecuteNonQuery("HR_InsUpd_EmpWagesCentage", new SqlParameter[] { new SqlParameter("@EmpId", EmpId),
                                                                                        new SqlParameter("@WagesID", WagesId), 
                                                                                        new SqlParameter("@IsActive", Access),
                                                                                        new SqlParameter("@CentageID",Convert.ToInt32(CentageID)),
                                                                                        new SqlParameter("@UserID",Convert.ToInt32(UserID)), 
                                                                                        new SqlParameter("@Centage",Convert.ToDecimal(Centage)) , 
                                                                                        new SqlParameter("@EmpSalID", EmpSalID),
                                                                                        new SqlParameter("@empsalary", Convert.ToDecimal(empsaltext))});

        }
        catch (Exception ex)
        {
            returnString = ex.Message;
        }
        return returnString;
    }

    [Ajax.AjaxMethod()]
    public string UpdateAllowances(string EmpId, string AllowId, bool Access)
    {
        string returnString = "Changed";
        try
        {
            SQLDBUtil.ExecuteNonQuery("HR_InsUpd_EmpAllowances", new SqlParameter[] { new SqlParameter("@EmpId", EmpId), new SqlParameter("@AllowId", AllowId), new SqlParameter("@IsActive", Access) });

        }
        catch (Exception ex)
        {
            returnString = ex.Message;
        }
        return returnString;
    }

    [Ajax.AjaxMethod()]
    public string UpdateNonCTC(string EmpId, string AllowId, bool Access)
    {
        string returnString = "Changed";
        try
        {
            SQLDBUtil.ExecuteNonQuery("HR_InsUpd_EmpNoNCTCComponents_Configure", new SqlParameter[] { new SqlParameter("@EmpId", EmpId), new SqlParameter("@AllowId", AllowId), new SqlParameter("@IsActive", Access) });

        }
        catch (Exception ex)
        {
            returnString = ex.Message;
        }
        return returnString;
    }






   
    [Ajax.AjaxMethod()]
    public string UpdateAllowancesPercent(string EmpId, string AllowID, bool Access, string CentageID, string UserID, string Centage, string EmpSalID, bool ITExemptions)
    {
        string returnString = "Changed";
        if (Centage == "" || Centage == null)
        {
            Centage = "0";
            //returnString = "Plz Enter ";
        }
        try
        {
            SQLDBUtil.ExecuteNonQuery("HR_InsUpd_EmpAllowancesCentage", new SqlParameter[] { new SqlParameter("@EmpId", EmpId),
                                                                                        new SqlParameter("@AllowID", AllowID), 
                                                                                        new SqlParameter("@IsActive", Access),
                                                                                        new SqlParameter("@CentageID",Convert.ToInt32(CentageID)),
                                                                                        new SqlParameter("@UserID",Convert.ToInt32(UserID)), 
                                                                                        new SqlParameter("@Centage",Convert.ToDecimal(Centage)) , 
                                                                                        new SqlParameter("@EmpSalID", EmpSalID), 
                                                                                        new SqlParameter("@ITExemptions", ITExemptions) });

        }
        catch (Exception ex)
        {
            returnString = ex.Message;
        }
        return returnString;
    }

   
    [Ajax.AjaxMethod()]
    public string UpdateEmpContribution(string EmpId, string ItemId, bool Access)
    {
        string returnString = "Changed";
        try
        {
            SQLDBUtil.ExecuteNonQuery("HR_InsUpd_EmpContribution", new SqlParameter[] { new SqlParameter("@EmpId", EmpId), new SqlParameter("@ItemId", ItemId), new SqlParameter("@IsActive", Access) });

        }
        catch (Exception ex)
        {
            returnString = ex.Message;
        }
        return returnString;
    }


    [Ajax.AjaxMethod()]
    public string UpdateEmpContributionPercent(string EmpId, string ItemID, bool Access, string CentageID, string UserID, string Centage, string EmpSalID, bool ITExemptions)
    {
        string returnString = "Changed";
        if (Centage == "" || Centage == null)
        {
            Centage = "0";
        }
        try
        {
            SQLDBUtil.ExecuteNonQuery("HR_InsUpd_EmpContributionCentage", new SqlParameter[] { new SqlParameter("@EmpId", EmpId),
                                                                                        new SqlParameter("@ItemID", ItemID), 
                                                                                        new SqlParameter("@IsActive", Access),
                                                                                        new SqlParameter("@CentageID",Convert.ToInt32(CentageID)),
                                                                                        new SqlParameter("@UserID",Convert.ToInt32(UserID)), 
                                                                                        new SqlParameter("@Centage",Convert.ToDecimal(Centage)) , 
                                                                                        new SqlParameter("@EmpSalID", EmpSalID), 
                                                                                        new SqlParameter("@ITExemptions", ITExemptions)});

        }
        catch (Exception ex)
        {
            returnString = ex.Message;
        }
        return returnString;
    }
    [Ajax.AjaxMethod()]
    public string UpdateEmpDeductions(string EmpId, string ItemId, bool Access)
    {
        string returnString = "Changed";
        try
        {
            SQLDBUtil.ExecuteNonQuery("HR_InsUpd_EmpDeduction", new SqlParameter[] { new SqlParameter("@EmpId", EmpId), new SqlParameter("@ItemId", ItemId), new SqlParameter("@IsActive", Access) });

        }
        catch (Exception ex)
        {
            returnString = ex.Message;
        }
        return returnString;
    }
  
    [Ajax.AjaxMethod()]
    public string UpdateEmpDeductionsPercent(string EmpId, string ItemID, bool Access, string CentageID, string UserID, string Centage, string EmpSalID, bool ITExemptions)
    {
        string returnString = "Changed";
        if (Centage == "" || Centage == null)
        {
            Centage = "0";
        }
        try
        {
            SQLDBUtil.ExecuteNonQuery("HR_InsUpd_EmpDeductionCentage", new SqlParameter[] { new SqlParameter("@EmpId", EmpId),
                                                                                        new SqlParameter("@ItemID", ItemID), 
                                                                                        new SqlParameter("@IsActive", Access),
                                                                                        new SqlParameter("@CentageID",Convert.ToInt32(CentageID)),
                                                                                        new SqlParameter("@UserID",Convert.ToInt32(UserID)), 
                                                                                        new SqlParameter("@Centage",Convert.ToDecimal(Centage)) , 
                                                                                        new SqlParameter("@EmpSalID", EmpSalID), 
                                                                                        new SqlParameter("@ITExemptions", ITExemptions) });

        }
        catch (Exception ex)
        {
            returnString = ex.Message;
        }
        return returnString;
    }
    [Ajax.AjaxMethod()]
    public string HR_PINOutTime(string EmpId)
    {
        string returnString = "Changed";
        try
        {
            SQLDBUtil.ExecuteNonQuery("HR_PINOutTime", new SqlParameter[] { new SqlParameter("@EmpId", EmpId) });

        }
        catch (Exception ex)
        {
            returnString = ex.Message;
        }
        return returnString;
    }
    [Ajax.AjaxMethod()]
    public DataRow CheckAvailable(string Status, string EmpId)
    {
       
        DataSet ds= SQLDBUtil.ExecuteDataset("HR_ChkLeaveCombination", new SqlParameter[] { new SqlParameter("@Status", Status), new SqlParameter("@EmpId", EmpId) });
        return ds.Tables[0].Rows[0];
    }


    [Ajax.AjaxMethod()]
    public DataRow HR_MarkAttandace(string EmpId, string Status, int SiteID, int UserID,int Projectid)
    {

        DataSet ds = SQLDBUtil.ExecuteDataset("HR_MarkAttandace", new SqlParameter[] { new SqlParameter("@EmpId", EmpId), new SqlParameter("@Status", Status), new SqlParameter("@SiteID", SiteID), new SqlParameter("@UserID", UserID), new SqlParameter("@projectid", Projectid) });
        return ds.Tables[0].Rows[0];
    }




    [Ajax.AjaxMethod()]
    public DataRow HR_MarkAttandaceBySMS(string EmpId, string Status, int SiteID, int UserID, string InTime, int TransID)
    {
       
        DataSet ds= SQLDBUtil.ExecuteDataset("HR_MarkAttandaceBySMS", new SqlParameter[] { new SqlParameter("@EmpId", EmpId), new SqlParameter("@Status", Status), new SqlParameter("@SiteID", SiteID), new SqlParameter("@UserID", UserID), new SqlParameter("@InTime", InTime),new SqlParameter("@TransID",TransID) });
        return ds.Tables[0].Rows[0];
    }
    [Ajax.AjaxMethod()]
    public DateTime GetServerDate()
    {
        return DateTime.Now;
    }
    [Ajax.AjaxMethod()]
    public string GetServerDateIn24HrsFormate()
    {
        DateTime currentTime = DateTime.Now;
        return currentTime.ToString("HH:mm");
             
    }


    [Ajax.AjaxMethod()]
    public string UpdateFullAtt(string empID, string status, string date, string InTime, string OutTime, string Remarks, int SiteID, int UserID)
    {
        string Rtval = "Update";
        try
        {
            SQLDBUtil.ExecuteNonQuery("hr_UpdateFullAtt", new SqlParameter[] { new SqlParameter("@EmpId", empID), new SqlParameter("@Status", status), new SqlParameter("@date", date), new SqlParameter("@InTime", InTime), new SqlParameter("@OutTime", OutTime), new SqlParameter("@Remarks", Remarks), new SqlParameter("@SiteID", SiteID), new SqlParameter("@UserID", UserID) });

        }
        catch (Exception ex) { Rtval = ex.Message; }
        return Rtval;
    }

    [Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.Read)]
    public DateTime[] UpdateAttendance(string AStatus, string EmpId, string InTime, string OutTime, string Remarks, string SiteID)
    {
        DateTime[] dates = new DateTime[2];

        int i = Convert.ToInt32(objAtt.InsertFullAtt(Convert.ToInt32(EmpId), Convert.ToInt32(AStatus), DateTime.Now, InTime, OutTime, Remarks, Convert.ToInt32(SiteID), Convert.ToInt32(EmpId)));
     

        return dates;
    }
    [Ajax.AjaxMethod()]
    public DateTime[] InsUpNMRAttendance(int empID, int status, string InTime, string OutTime, string Remarks)
    {
        DateTime[] dates = new DateTime[2];

        int i = Convert.ToInt32(objAtt.InsertNMRFullAtt(Convert.ToInt32(empID), Convert.ToInt32(status), DateTime.Now, InTime, OutTime, Remarks));

        return dates;
    }
    [Ajax.AjaxMethod()]
    public string HR_IsVerified(bool Checked, int ERItemID)
    {
        string returnString = "Changed";
        try
        {
            SQLDBUtil.ExecuteNonQuery("HR_IsVerified", new SqlParameter[] { new SqlParameter("@Checked", Checked), new SqlParameter("@ERItemID", ERItemID) });

        }
        catch (Exception ex)
        {
            returnString = ex.Message;
        }
        return returnString;
    }
    [Ajax.AjaxMethod()]
    public string HR_UpdateOutTime(int EmpID)
    {
        string returnString = "Changed";
        try
        {
           
            DataSet ds= SQLDBUtil.ExecuteDataset("HR_UpdateOutTime", new SqlParameter[] { new SqlParameter("@EmpID", EmpID) });
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0 && ds.Tables[0].Rows[0][0].ToString() != "")
            {
                return ds.Tables[0].Rows[0][0].ToString();
            }
            else
            {
                return "";
            }

        }
        catch (Exception ex)
        {
            return returnString = ex.Message;
        }

    }

    [Ajax.AjaxMethod()]
    public string HR_UpdateRemarks(string Remarks, int EmpID)
    {
        string returnString = "Changed";
        try
        {
            SQLDBUtil.ExecuteNonQuery("HR_UpdateRemarks", new SqlParameter[] { new SqlParameter("@Remarks", Remarks), new SqlParameter("@EmpID", EmpID) });

        }
        catch (Exception ex)
        {
            returnString = ex.Message;
        }
        return returnString;
    }

    [Ajax.AjaxMethod()]
    public string HR_UpdateNMROutTime(int EmpID)
    {
        string returnString = "Changed";
        try
        {
            SQLDBUtil.ExecuteNonQuery("HR_UpdateNMROutTime", new SqlParameter[] { new SqlParameter("@EmpID", EmpID) });

        }
        catch (Exception ex)
        {
            returnString = ex.Message;
        }
        return returnString;
    }
    [Ajax.AjaxMethod]
    public DataRow getClosingBalancing(string CompanyId, string LedgerID)
    {
        SqlParameter[] p = new SqlParameter[2];
        p[0] = new SqlParameter("@CompanyID", CompanyId);
        p[1] = new SqlParameter("@LedgerID", LedgerID);

        DataSet DsLedgers = new DataSet();
        DsLedgers = SQLDBUtil.ExecuteDataset("Acc_LedgerClosingBalnce", p);
        // return DsLedgers; 
        return DsLedgers.Tables[0].Rows[0];
    }
    [Ajax.AjaxMethod]
    public static void HR_UpdAttendance(DataSet ds, int AttStatus, int UserID, DateTime? Date)
    {
        SqlParameter[] parm = new SqlParameter[4];
        parm[0] = new SqlParameter("@RemItems", ds.GetXml());
        parm[1] = new SqlParameter("@Status", AttStatus);
        parm[2] = new SqlParameter("@UserID", UserID);
        parm[3] = new SqlParameter("@Date", Date);
        SQLDBUtil.ExecuteNonQuery("HR_UpdAttendance", parm);
    }
    [Ajax.AjaxMethod]
    public static void HR_UpdateEditAttendance(DataSet ds, int AttStatus, int UserID, DateTime? Date)
    {
        SqlParameter[] parm = new SqlParameter[4];
        parm[0] = new SqlParameter("@RemItems", ds.GetXml());
        parm[1] = new SqlParameter("@Status", AttStatus);
        parm[2] = new SqlParameter("@UserID", UserID);
        parm[3] = new SqlParameter("@Date", Date);
        SQLDBUtil.ExecuteNonQuery("HR_UpdateEditAttendance", parm);
    }
   
    [Ajax.AjaxMethod()]
    public string HR_UpdateOutTimeEdit(int EmpID, string Date)
    {
        string returnString = "Changed";
        try
        {
           
            DataSet ds= SQLDBUtil.ExecuteDataset("HR_UpdateOutTimeEdit", new SqlParameter[] { new SqlParameter("@EmpID", EmpID), new SqlParameter("@Date", CodeUtilHMS.ConvertToDate_ddMMMyyy(Date,CodeUtilHMS.DateFormat.ddMMMyyyy)) });
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0 && ds.Tables[0].Rows[0][0].ToString() != "")
            {
                return ds.Tables[0].Rows[0][0].ToString();
            }
            else
            {
                return "";
            }
        }
        catch (Exception ex)
        {
            returnString = ex.Message;
        }
        return returnString;
    }
    #region LedgerReport.aspx

    [Ajax.AjaxMethod]
    public string[] SetUpDates(string transPeriod)
    {
        string[] strdates = new string[2];
        strdates[1] = DateTime.Now.ToString(ConfigurationManager.AppSettings["DateFormat"]);
        switch (transPeriod)
        {
            case "1"://TOday
                strdates[0] = DateTime.Now.ToString(ConfigurationManager.AppSettings["DateFormat"]);
                break;
            case "7"://WEEK
                strdates[0] = DateTime.Now.AddDays(-7).ToString(ConfigurationManager.AppSettings["DateFormat"]);

                break;
            case "15"://fortnight
                strdates[0] = DateTime.Now.AddDays(-15).ToString(ConfigurationManager.AppSettings["DateFormat"]);

                break;
            case "30"://month
                strdates[0] = DateTime.Now.AddMonths(-1).ToString(ConfigurationManager.AppSettings["DateFormat"]);
                break;
            case "90"://3Months
                strdates[0] = DateTime.Now.AddMonths(-3).ToString(ConfigurationManager.AppSettings["DateFormat"]);

                break;
            case "180"://half Year
                strdates[0] = DateTime.Now.AddMonths(-6).ToString(ConfigurationManager.AppSettings["DateFormat"]);
                break;
            case "365"://Year
                strdates[0] = DateTime.Now.AddYears(-1).ToString(ConfigurationManager.AppSettings["DateFormat"]);
                break;
            default:
                strdates[0] = DateTime.Now.ToString(ConfigurationManager.AppSettings["DateFormat"]);
                break;
        }
        return strdates;
    }

    [Ajax.AjaxMethod]
    public string[] getFinanceYear(string ID)
    {
        string[] strdates = new string[2];
        SqlParameter[] parm = new SqlParameter[3];
        parm[0] = new SqlParameter("@FinYearId", ID);
        parm[1] = new SqlParameter("@FromDate", SqlDbType.DateTime);
        parm[2] = new SqlParameter("@ToDate", SqlDbType.DateTime);
        parm[1].Direction = parm[2].Direction = ParameterDirection.InputOutput;
        SQLDBUtil.ExecuteNonQuery("G_GetFinYear", parm);
        strdates[0] = Convert.ToDateTime(parm[1].Value).ToString(ConfigurationManager.AppSettings["DateFormat"]);
        strdates[1] = Convert.ToDateTime(parm[2].Value).ToString(ConfigurationManager.AppSettings["DateFormat"]);
        return strdates;
    }

    [Ajax.AjaxMethod]
    public string[] getLedgerReportDateSetUp()
    {
        string[] strdates = new string[2];
        strdates[0] = DateTime.Now.ToString(ConfigurationManager.AppSettings["DateFormat"]);
        strdates[1] = DateTime.Now.ToString(ConfigurationManager.AppSettings["DateFormat"]);
        return strdates;
    }

    #endregion LedgerReport.aspx
    #region EmpRentalDocs
    [Ajax.AjaxMethod]
    public string HR_SetUpApproval(string HRDocID, string UserID)
    {
        string returnString = "Changed";
        try
        {
            int HRDocId = Convert.ToInt32(HRDocID);
            int UserId = Convert.ToInt32(UserID);

            SQLDBUtil.ExecuteNonQuery("HR_SetUpApproval", new SqlParameter[] { new SqlParameter("@HRDocID", HRDocId), new SqlParameter("@UserID", UserId) });

        }
        catch (Exception ex)
        {
            returnString = ex.Message;
        }
        return returnString;
    }

    #endregion
    #region WeekOffConfig
    [Ajax.AjaxMethod]
    public string HR_InsUpWOConfigByEmpNature(string WeekNo, string Day, string IsActive, string EmpNatureID)
    {
        string returnString = "Changed";
        try
        {
            SQLDBUtil.ExecuteNonQuery("HR_InsUpWOConfigByEmpNature", new SqlParameter[] { new SqlParameter("@WeekNo", Convert.ToInt32(WeekNo)), new SqlParameter("@Day", Convert.ToInt32(Day)), new SqlParameter("@IsActive", Convert.ToInt32(IsActive)), new SqlParameter("@EmpNatureID", Convert.ToInt32(EmpNatureID)) });

        }
        catch (Exception ex)
        {
            returnString = ex.Message;
        }
        return returnString;
    }

    #endregion

    [Ajax.AjaxMethod()]
    public string WagesPercentCalculation(string CentageValue, string EmpSal)
    {
        string returnString = "0";
        Decimal Value = Convert.ToDecimal(CentageValue);
        Decimal Sal = Convert.ToDecimal(EmpSal) / 12;

        try
        {
            returnString = ((Value / Sal)).ToString();
        }
        catch (Exception ex)
        {
            returnString = ex.Message;
        }
        return returnString;
    }
    [Ajax.AjaxMethod()]
    public string AllowancesPercentCalculation(string CentageValue, string EmpSal, string allowid, string empid, string EmpSalID)
    {
        string returnString = "0";


        try
        {
           
            SqlParameter[] parm = new SqlParameter[4];
            parm[0] = new SqlParameter("@AllowID", Convert.ToInt32(allowid));
            parm[1] = new SqlParameter("@EmpID", Convert.ToInt32(empid));
            parm[2] = new SqlParameter("@Value", Convert.ToDecimal(CentageValue));
            parm[3] = new SqlParameter("@EmpSalID", Convert.ToInt32(EmpSalID));
            DataSet ds= SQLDBUtil.ExecuteDataset("HR_AllowCalManual", parm);

            int Status = Convert.ToInt32(ds.Tables[0].Rows[0]["AmtStatus"]);
            decimal Centage = Convert.ToDecimal(ds.Tables[0].Rows[0]["Centage"]);

            if (Status == 1)
            {
                returnString = Convert.ToString(Centage);
            }
            else
            {
                returnString = Convert.ToString(Centage);
            }

        }
        catch (Exception ex)
        {
            returnString = ex.Message;
        }
        return returnString;
    }

    [Ajax.AjaxMethod()]
    public string ContrPercentageCal(string CentageValue, string EmpSal, string ItemID, string empid, string EmpSalID)
    {
        string returnString = "0";
        try
        {
           
            SqlParameter[] parm = new SqlParameter[4];
            parm[0] = new SqlParameter("@ItemID", Convert.ToInt32(ItemID));
            parm[1] = new SqlParameter("@EmpID", Convert.ToInt32(empid));
            parm[2] = new SqlParameter("@Value", Convert.ToDecimal(CentageValue));
            parm[3] = new SqlParameter("@EmpSalID", Convert.ToInt32(EmpSalID));
            DataSet ds= SQLDBUtil.ExecuteDataset("HR_ContributionCalManual", parm);
            int Status = Convert.ToInt32(ds.Tables[0].Rows[0]["AmtStatus"]);
            decimal Centage = Convert.ToDecimal(ds.Tables[0].Rows[0]["Centage"]);
            if (Status == 1)
            {
                returnString = Convert.ToString(Centage);
            }
            else
            {
                returnString = "0";
            }
        }
        catch (Exception ex)
        {
            returnString = ex.Message;
        }
        return returnString;
    }

    [Ajax.AjaxMethod()]
    public string DedudPercentageCal(string CentageValue, string EmpSal, string ItemID, string empid, string EmpSalID)
    {
        string returnString = "0";
        try
        {
           
            SqlParameter[] parm = new SqlParameter[4];
            parm[0] = new SqlParameter("@ItemID", Convert.ToInt32(ItemID));
            parm[1] = new SqlParameter("@EmpID", Convert.ToInt32(empid));
            parm[2] = new SqlParameter("@Value", Convert.ToDecimal(CentageValue));
            parm[3] = new SqlParameter("@EmpSalID", Convert.ToInt32(EmpSalID));
            DataSet ds= SQLDBUtil.ExecuteDataset("HR_DeductionCalManual", parm);
            int Status = Convert.ToInt32(ds.Tables[0].Rows[0]["AmtStatus"]);
            decimal Centage = Convert.ToDecimal(ds.Tables[0].Rows[0]["Centage"]);
            if (Status == 1)
            {
                returnString = Convert.ToString(Centage);
            }
            else
            {
                returnString = "0";
            }
        }
        catch (Exception ex)
        {
            returnString = ex.Message;
        }
        return returnString;
    }

    [Ajax.AjaxMethod()]
    public string ConfigWagesContributiion(string ContributionID, string WagesId, bool Access)
    {
        string returnString = "Changed";
        try
        {
            SQLDBUtil.ExecuteNonQuery("HR_InsUpd_ConfigWagesContributiion", new SqlParameter[] { new SqlParameter("@ContributionID", ContributionID), new SqlParameter("@WagesID", WagesId), new SqlParameter("@IsActive", Access) });
        }
        catch (Exception ex)
        {
            returnString = ex.Message;
        }
        return returnString;
    }
    [Ajax.AjaxMethod()]
    public string ConfigAllowancesContributiion(string ContributionID, string AllowId, bool Access)
    {
        string returnString = "Changed";
        try
        {
            SQLDBUtil.ExecuteNonQuery("HR_InsUpd_ConfigAllowancesContributiion", new SqlParameter[] { new SqlParameter("@ContributionID", ContributionID), new SqlParameter("@AllowId", AllowId), new SqlParameter("@IsActive", Access) });
        }
        catch (Exception ex)
        {
            returnString = ex.Message;
        }
        return returnString;
    }
    [Ajax.AjaxMethod()]
    public string ConfigContribution(string ContributionID, string ItemId, bool Access)
    {
        string returnString = "Changed";
        try
        {
            SQLDBUtil.ExecuteNonQuery("HR_InsUpd_ConfigContributiion", new SqlParameter[] { new SqlParameter("@ContributionID", ContributionID), new SqlParameter("@ItemId", ItemId), new SqlParameter("@IsActive", Access) });
        }
        catch (Exception ex)
        {
            returnString = ex.Message;
        }
        return returnString;
    }
    [Ajax.AjaxMethod()]
    public string ConfigDeductionsContributiion(string ContributionID, string ItemId, bool Access)
    {
        string returnString = "Changed";
        try
        {
            SQLDBUtil.ExecuteNonQuery("HR_InsUpd_ConfigDeductionsContributiion", new SqlParameter[] { new SqlParameter("@ContributionID", ContributionID), new SqlParameter("@ItemId", ItemId), new SqlParameter("@IsActive", Access) });
        }
        catch (Exception ex)
        {
            returnString = ex.Message;
        }
        return returnString;
    }

    #region empchanges

    [Ajax.AjaxMethod()]
    public static void UpdateSiteDeptChanges(string EmpID, string SiteID, string DeptID, string Mgnr, string UserID)      
    {

        try
        {

            SQLDBUtil.ExecuteNonQuery("HR_UpdateSiteDeptChanges", new SqlParameter[] { 
                 
                                        new SqlParameter("@EmpID", Convert.ToInt32(EmpID)), 
                                        new SqlParameter("@SiteID", Convert.ToInt32(SiteID)),
                                        new SqlParameter("@DeptID", Convert.ToInt32(DeptID)),
                                        new SqlParameter("@Mgnr", Convert.ToInt32(Mgnr)),
                                        new SqlParameter("@UserID", Convert.ToInt32(UserID))});
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    [Ajax.AjaxMethod()]
    public static void UpdSiteDeptChanges(string EmpID, string SiteID, string DeptID, string Mgnr, string UserID)    
    {

        try
        {

            SQLDBUtil.ExecuteNonQuery("HR_UpdSiteDeptChanges", new SqlParameter[] { 
                 
                                        new SqlParameter("@EmpID", Convert.ToInt32(EmpID)), 
                                        new SqlParameter("@SiteID", Convert.ToInt32(SiteID)),
                                        new SqlParameter("@DeptID", Convert.ToInt32(DeptID)),
                                        new SqlParameter("@Mgnr", Convert.ToInt32(Mgnr)),
                                        new SqlParameter("@UserID", Convert.ToInt32(UserID))});
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    [Ajax.AjaxMethod()]
    public int AddDeptHead(string WSId, string DeptID, string EmpId, string UserID)
    {
        try
        {
            int result;
            SqlParameter[] sqlParams = new SqlParameter[5];
            sqlParams[0] = new SqlParameter("@PrjID", Convert.ToInt32(WSId));
            sqlParams[1] = new SqlParameter("@DeptID", Convert.ToInt32(DeptID));
            sqlParams[3] = new SqlParameter("@EmpID", Convert.ToInt32(EmpId));
            sqlParams[4] = new SqlParameter("@UserID", Convert.ToInt32(UserID));
            sqlParams[2] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
            sqlParams[2].Direction = ParameterDirection.ReturnValue;
            SQLDBUtil.ExecuteNonQuery("HR_AddDeptHead", sqlParams);
            result = Convert.ToInt16(sqlParams[2].Value);
            return result;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    [Ajax.AjaxMethod()]

    public DataSet GetDeptHead(string WS, string Dept, string EmpID)
    {
        try
        {
           
            DataSet ds= SQLDBUtil.ExecuteDataset("HR_GetDeptHead", new SqlParameter[] { new SqlParameter("@WSId", WS), new SqlParameter("@DeptId", Dept), new SqlParameter("@EmpID", EmpID) });
            return ds;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    [Ajax.AjaxMethod()]

    public static void UPDSetAsDeptHead(string WS, string Dept, string EmpID, string UserID)
    {
        try
        {
            SqlParameter[] sqlParams = new SqlParameter[4];
            sqlParams[0] = new SqlParameter("@SiteID", Convert.ToInt32(WS));
            sqlParams[1] = new SqlParameter("@DeptID", Convert.ToInt32(Dept));
            sqlParams[2] = new SqlParameter("@EmpID", Convert.ToInt32(EmpID));
            sqlParams[3] = new SqlParameter("@UserID", Convert.ToInt32(UserID));
            SQLDBUtil.ExecuteNonQuery("HR_SetAsDeptHead", sqlParams);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    [Ajax.AjaxMethod()]

    public static void UPDATESetAsDeptHead(string WS, string Dept, string EmpID, string UserID)
    {
        try
        {
            SqlParameter[] sqlParams = new SqlParameter[4];
            sqlParams[0] = new SqlParameter("@SiteID", Convert.ToInt32(WS));
            sqlParams[1] = new SqlParameter("@DeptID", Convert.ToInt32(Dept));
            sqlParams[2] = new SqlParameter("@EmpID", Convert.ToInt32(EmpID));
            sqlParams[3] = new SqlParameter("@UserID", Convert.ToInt32(UserID));
            SQLDBUtil.ExecuteNonQuery("HR_SetAsDepartHead", sqlParams);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }



    [Ajax.AjaxMethod()]
    public static DataSet HR_EmpTransferDetails(string EmpID)
    {
        try
        {
           
            SqlParameter[] objParam = new SqlParameter[1];
            objParam[0] = new SqlParameter("@EmpID", Convert.ToInt32(EmpID));
            DataSet ds= SQLDBUtil.ExecuteDataset("HR_EmpTransferDetails", objParam);
            return ds;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }



    [Ajax.AjaxMethod()]

    public string GetDeptHeadsForTransfer(string EmpID, string Dept, string WS)
    {
        string DeptHeadID;

        int? LocEmpID = null;

        if (EmpID != "0")
        {
            LocEmpID = Convert.ToInt32(EmpID);
        }

        try
        {
           
            DataSet ds= SQLDBUtil.ExecuteDataset("HR_GetDeptHead", new SqlParameter[] { new SqlParameter("@EmpID", Convert.ToInt32(LocEmpID)), new SqlParameter("@WSId", Convert.ToInt32(WS)), new SqlParameter("@DeptId", Convert.ToInt32(Dept)) });
            DeptHeadID = ds.Tables[0].Rows[0]["HeadID"].ToString();
            return DeptHeadID;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion empchanges

    #region SMS
    [Ajax.AjaxMethod()]
    public void UpdSMSEMPTransferStatus(string TransID)
    {
        try
        {
            SQLDBUtil.ExecuteNonQuery("HR_UpdSMSEMPTransfer", new SqlParameter[] { new SqlParameter("@TransID", Convert.ToInt32(TransID)) });
        }
        catch (Exception ex)
        {
        }
    }

    [Ajax.AjaxMethod()]
    public void UpdateSMSEmpStatus(string TransID, int MarkedBy)
    {
        SqlParameter[] parm = new SqlParameter[2];
        parm[0] = new SqlParameter("@TransID", Convert.ToInt32(TransID));
        parm[1] = new SqlParameter("@UserID", MarkedBy);

        SQLDBUtil.ExecuteNonQuery("UpdateSMSEmpByTransID", parm);
    }


    #endregion SMS

    [Ajax.AjaxMethod()]
    public string HR_UpdateYesterDayOutTime(int EmpID)
    {
        string returnString = "Changed";
        try
        {
           
            DataSet ds= SQLDBUtil.ExecuteDataset("HR_UpdateYesterDayOutTime", new SqlParameter[] { new SqlParameter("@EmpID", EmpID) });
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0 && ds.Tables[0].Rows[0][0].ToString() != "")
            {
                return ds.Tables[0].Rows[0][0].ToString();
            }
            else
            {
                return "";
            }

        }
        catch (Exception ex)
        {
            return returnString = ex.Message;
        }

    }
    [Ajax.AjaxMethod()]
    public string HR_InsUpdWhohavesys(string Status, string EmpID)
    {
        string returnString = "";

        try
        {
           
            DataSet ds= SQLDBUtil.ExecuteDataset("HR_InsUpd_WhohaveSystem", new SqlParameter[] { new SqlParameter("@EmpID", EmpID), new SqlParameter("@Status", Status) });
            return returnString;
        }
        catch (Exception ex)
        {
            return returnString = ex.Message;
        }

    }


    [Ajax.AjaxMethod()]
    public string ConfigWagesDeduction(string ContributionID, string WagesId, bool Access)
    {
        string returnString = "Changed";
        try
        {
            SQLDBUtil.ExecuteNonQuery("HR_InsUpd_ConfigWagesDeduction", new SqlParameter[] { new SqlParameter("@DeductionID", ContributionID), new SqlParameter("@WagesID", WagesId), new SqlParameter("@IsActive", Access) });
        }
        catch (Exception ex)
        {
            returnString = ex.Message;
        }
        return returnString;
    }

    [Ajax.AjaxMethod()]
    public string ConfigAllowancesDeduction(string ContributionID, string AllowId, bool Access)
    {
        string returnString = "Changed";
        try
        {
            SQLDBUtil.ExecuteNonQuery("HR_InsUpd_ConfigAllowancesDeduction", new SqlParameter[] { new SqlParameter("@DeductionID", ContributionID), new SqlParameter("@AllowId", AllowId), new SqlParameter("@IsActive", Access) });
        }
        catch (Exception ex)
        {
            returnString = ex.Message;
        }
        return returnString;
    }



    [Ajax.AjaxMethod()]
    public string HR_InsUpdLTAConfig(string Status, string EmpID, string CompID)
    {
        string returnString = "Done";

        try
        {
           
            DataSet ds= SQLDBUtil.ExecuteDataset("HR_InsUpdLTAConfig", new SqlParameter[] { new SqlParameter("@EmpID", EmpID), new SqlParameter("@Status", Status), new SqlParameter("@CompID", CompID) });
            return returnString;
        }
        catch (Exception ex)
        {
            return returnString = ex.Message;
        }

    }

    [Ajax.AjaxMethod()]
    public string UpdateNOnCTCComponents(string EmpId, string CompID, bool Access, string EmpSalID, string Amount)
    {
        string returnString = "Changed";

        try
        {
            SQLDBUtil.ExecuteNonQuery("HR_InsUpd_EmpNonCTCComponents", new SqlParameter[] { new SqlParameter("@EmpId", EmpId),
                                                                                        new SqlParameter("@NonCTCCompID", CompID), 
                                                                                        new SqlParameter("@IsActive", Access),
                                                                                        new SqlParameter("@EmpSalID", EmpSalID), 
                                                                                        new SqlParameter("@Amount", Amount) });

        }
        catch (Exception ex)
        {
            returnString = ex.Message;
        }
        return returnString;
    }


    [Ajax.AjaxMethod()]
    public DataTable  GetEmpCountBYSite(int CompanyID)
    {
        try
        {
           
            SqlParameter[] par = new SqlParameter[1];
            par[0] = new SqlParameter("@CompanyID", CompanyID);
            DataSet ds= SQLDBUtil.ExecuteDataset("HR_GetEmpCountBYSite", par);
            return ds.Tables[0];
        }
        catch (Exception ex)
        {

            throw ex;
        }
    }

    [Ajax.AjaxMethod()]
    public DataTable GetEmpCountBYSiteminMax(int CompanyID)
    {
        try
        {
           
            SqlParameter[] par = new SqlParameter[1];
            par[0] = new SqlParameter("@CompanyID", CompanyID);
            DataSet ds= SQLDBUtil.ExecuteDataset("HR_GetEmpCountBYSiteminmax", par);
            return ds.Tables[0];
        }
        catch (Exception ex)
        {

            throw ex;
        }
    }



    [Ajax.AjaxMethod()]
    public DataTable TestEmpOrgChart(string wsID,string DeptID)
    {
        try
        {
            wsID=wsID == "" ? "1" : wsID;
            DeptID = DeptID == "" ? "0" : DeptID;
           
            DataTable dt = new DataTable();
            SqlParameter[] par = new SqlParameter[2];
            par[0] = new SqlParameter("@WsID", Convert.ToInt32(wsID));
            par[1] = new SqlParameter("@DeptID", Convert.ToInt32(DeptID));

            DataSet ds= SQLDBUtil.ExecuteDataset("HR_TestEmpOrgChart",par);
            return ds.Tables[0];
        }
        catch (Exception ex)
        {

            throw ex;
        }
    }

    [Ajax.AjaxMethod()]
    public DataSet GetStartDate()
    {
        try
        {
            DataSet ds= SQLDBUtil.ExecuteDataset("G_StartdateForSalariesAttendance");
            return ds;

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
   


    
}
