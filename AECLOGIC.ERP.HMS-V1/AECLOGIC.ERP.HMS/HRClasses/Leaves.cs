using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using DataAccessLayer;
using AECLOGIC.HMS.BLL;
using Aeclogic.Common.DAL;
/// <summary>
/// Summary description for Leaves
/// </summary>
public class Leaves
{
    public Leaves()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    #region Leaves available
    public static DataSet GetApplicableLeavesDetails(int SiteID, int DeptID, int EmpID)
    {
        try
        {
            DataSet ds = SQLDBUtil.ExecuteDataset("T_G_Leaves_GetAvailableLeavesList", new SqlParameter[] { new SqlParameter("@SiteID", SiteID), new SqlParameter("@DeptNo", DeptID), new SqlParameter("@EmpID", EmpID) });
            return ds;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public static DataSet GetApplicableLeavesDetailsByPaging(HRCommon objHrCommon, int SiteID, int DeptID, int? EmpID, string EmpName, int CompanyID, int CalenYearID)
    {
        SqlParameter[] sqlParams = new SqlParameter[10];
        sqlParams[0] = new SqlParameter("@CurrentPage", objHrCommon.CurrentPage);
        sqlParams[1] = new SqlParameter("@PageSize", objHrCommon.PageSize);
        sqlParams[2] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
        sqlParams[2].Direction = ParameterDirection.ReturnValue;
        sqlParams[3] = new SqlParameter("@NoofRecords", System.Data.SqlDbType.Int);
        sqlParams[3].Direction = ParameterDirection.Output;
        sqlParams[4] = new SqlParameter("@SiteID", SiteID);
        sqlParams[5] = new SqlParameter("@DeptNo", DeptID);
        sqlParams[6] = new SqlParameter("@EmpID", EmpID);
        sqlParams[7] = new SqlParameter("@EmpName", EmpName);
        sqlParams[8] = new SqlParameter("@CompanyID", CompanyID);
        sqlParams[9] = new SqlParameter("@CalenYearID", CalenYearID);
        DataSet ds = SQLDBUtil.ExecuteDataset("HR_Leaves_GetAvailableLeavesListByPaging", sqlParams);
        objHrCommon.NoofRecords = (int)sqlParams[3].Value;
        objHrCommon.TotalPages = (int)sqlParams[2].Value;
        return ds;
    }
    public static DataSet GetApplicableLeavesDetailsByPaging(int? EmpID, int LevTyID, int YID)
    {
        SqlParameter[] sqlParams = new SqlParameter[3];
        sqlParams[0] = new SqlParameter("@EmpID", EmpID);
        if (LevTyID > 0)
            sqlParams[1] = new SqlParameter("@LevTyID", LevTyID);
        else
            sqlParams[1] = new SqlParameter("@LevTyID", SqlDbType.Int);
        if (YID == 0)
            sqlParams[2] = new SqlParameter("@YID", SqlDbType.Int);
        else
            sqlParams[2] = new SqlParameter("@YID", YID);
        return SQLDBUtil.ExecuteDataset("HR_Leaves_GetAvailableLeavesListByPaging_Details", sqlParams);
    }
    #endregion Leaves available
    #region Configuration
    public static void InsUpdateLeaves(int LeaveTypeID, string LeaveType, int Days, int UserId, int Status)
    {
        try
        {
            SqlParameter[] sqlParams = new SqlParameter[5];
            sqlParams[0] = new SqlParameter("@LeaveTypeID", LeaveTypeID);
            sqlParams[1] = new SqlParameter("@LeaveType", LeaveType);
            sqlParams[2] = new SqlParameter("@UserId", UserId);
            sqlParams[3] = new SqlParameter("@IsActive", Status);
            sqlParams[4] = new SqlParameter("@Days", Days);
            SQLDBUtil.ExecuteNonQuery("T_G_Leaves_InsUpdateLeaveTypes", sqlParams);
        }
        catch (Exception e)
        {
            throw e;
        }
    }
    public static DataSet GetLeavesList()
    {
        return SQLDBUtil.ExecuteDataset("T_G_Leaves_GetLeaveTypesList");
    }
    public static DataSet GetLeavesDetails(int LeaveTypeID)
    {
        try
        {
            DataSet ds = SQLDBUtil.ExecuteDataset("T_G_Leaves_GetLeaveTypesDetails", new SqlParameter[] { new SqlParameter("@LeaveTypeID", LeaveTypeID) });
            return ds;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion Configuration
    public static DataSet GetHolidayPaidRulesList()
    {
        return SQLDBUtil.ExecuteDataset("HR_GET_HolidayPaidRulesList");
    }
    #region Natureof Employeement
    public static int InsUpdateEmpNature(int NatureID, string Nature, string Desc, int UserId, int Status)
    {
        try
        {
            SqlParameter[] sqlParams = new SqlParameter[6];
            sqlParams[0] = new SqlParameter("@NatureOfEmp", NatureID);
            sqlParams[1] = new SqlParameter("@Nature", Nature);
            sqlParams[4] = new SqlParameter("@NoofWD", Desc);
            sqlParams[2] = new SqlParameter("@UserId", UserId);
            sqlParams[3] = new SqlParameter("@IsActive", Status);
            sqlParams[5] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
            sqlParams[5].Direction = ParameterDirection.ReturnValue;
            SQLDBUtil.ExecuteNonQuery("T_G_Leaves_InsUpdateEmpNature", sqlParams);
            return Convert.ToInt32(sqlParams[5].Value);
        }
        catch (Exception e)
        {
            return 0;
        }
    }
    public static DataSet GetHolidayPaidRulesDetails(int RuleId)
    {
        try
        {
            DataSet ds = SQLDBUtil.ExecuteDataset("HR_GET_HolidayPaidRulesDetails", new SqlParameter[] { new SqlParameter("@RuleId", RuleId) });
            return ds;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public static void InsUpdateHolidayPaidRules(int RuleId, string RuleName, int Comb1, int Comb2, int Comb3, int PaidStatus)
    {
        try
        {
            SqlParameter[] sqlParams = new SqlParameter[6];
            sqlParams[0] = new SqlParameter("@RuleId", RuleId);
            sqlParams[1] = new SqlParameter("@RuleName", RuleName);
            sqlParams[4] = new SqlParameter("@Comb1", Comb1);
            sqlParams[2] = new SqlParameter("@Comb2", Comb2);
            sqlParams[5] = new SqlParameter("@Comb3", Comb3);
            sqlParams[3] = new SqlParameter("@IsPaid", PaidStatus);
            SQLDBUtil.ExecuteNonQuery("HR_InsUpd_HolidayPaidRules", sqlParams);
        }
        catch (Exception e)
        {
            throw e;
        }
    }
    public static DataSet GetEmpNatureList(int status)
    {
        return SQLDBUtil.ExecuteDataset("T_G_Leaves_GetEmpNatureList", new SqlParameter[] { new SqlParameter("@status", status) });
    }
    public static DataSet GetEmpNaturelist_Searchworksite(String SearchKey, int status)
    {
        try
        {
            SqlParameter[] par = new SqlParameter[2];
            par[0] = new SqlParameter("@Search", SearchKey);
            par[1] = new SqlParameter("@status", status);
            return SQLDBUtil.ExecuteDataset("T_G_Leaves_GetEmpNatureList_googlesearch", par);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public static DataSet GetEmpNatureDetails(int NatureID)
    {
        try
        {
            DataSet ds = SQLDBUtil.ExecuteDataset("T_G_Leaves_GetEmpNatureDetails", new SqlParameter[] { new SqlParameter("@NatureOfEmp", NatureID) });
            return ds;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion Natureof Employeement
    #region Type of Leaves
    public static void InsUpdateTypeofLeaves(int LTID, decimal Cr, decimal Dr, int UID, DateTime ActionDate)
    {
        try
        {
            SqlParameter[] sqlParams = new SqlParameter[5];
            sqlParams[0] = new SqlParameter("@LTID", LTID);
            sqlParams[1] = new SqlParameter("@Cr", Cr);
            sqlParams[2] = new SqlParameter("@Dr", Dr);//@Status
            sqlParams[3] = new SqlParameter("@UID", UID);//@Status
            sqlParams[4] = new SqlParameter("@ActionDate", ActionDate);//@Status
            SQLDBUtil.ExecuteNonQuery("HR_Leaves_OpenningBalUpdate", sqlParams);
        }
        catch (Exception e)
        {
        }
    }
    public static void HR_LeaveSyncCal(int EMPID, DateTime Date, int UID)
    {
        try
        {
            SqlParameter[] sqlParams = new SqlParameter[3];
            sqlParams[0] = new SqlParameter("@EMPID", EMPID);
            sqlParams[1] = new SqlParameter("@Date", Date);
            sqlParams[2] = new SqlParameter("@UID", UID);//@Status
            SQLDBUtil.ExecuteNonQuery("HR_LeaveSyncCal", sqlParams);
        }
        catch (Exception e)
        {
        }
    }
    public static void InsUpdateTypeofLeaves(int ID, string Name, string Sname, int UserId, int Status,
        int PayType, int Gender, int IsAccure, int IsCFrwd, decimal MinServiceYr, decimal MaxEntileYr
        , string LabLaw, string Remarks, int isEncashable, decimal PayCoeff)
    {
        try
        {
            SqlParameter[] sqlParams = new SqlParameter[14];
            sqlParams[0] = new SqlParameter("@LeaveType", ID);
            sqlParams[1] = new SqlParameter("@Name", Name);
            sqlParams[2] = new SqlParameter("@ShortName", Sname);//@Status
            sqlParams[3] = new SqlParameter("@Status", PayType);//@Status
            sqlParams[4] = new SqlParameter("@IsActive", Status);
            sqlParams[5] = new SqlParameter("@Gender", Gender);
            sqlParams[6] = new SqlParameter("@IsAccure", IsAccure);
            sqlParams[7] = new SqlParameter("@IsCFrwd", IsCFrwd);
            sqlParams[8] = new SqlParameter("@MinServiceYr", MinServiceYr);
            sqlParams[9] = new SqlParameter("@MaxEntileYr", MaxEntileYr);
            sqlParams[10] = new SqlParameter("@LabLaw", LabLaw);
            sqlParams[11] = new SqlParameter("@Remarks", Remarks);
            sqlParams[12] = new SqlParameter("@isEncashable", isEncashable);
            sqlParams[13] = new SqlParameter("@PayCoeff", PayCoeff);
            SQLDBUtil.ExecuteNonQuery("T_G_Leaves_InsUpdateTypeOfLeaves", sqlParams);
        }
        catch (Exception e)
        {
            throw e;
        }
    }
    public static DataSet GetTypeofLeavesList()
    {
        return SQLDBUtil.ExecuteDataset("T_G_Leaves_GetTypeOfLeavesList");
    }
    public static DataSet BindGrdLeaveTypesBasedOnGender(int EmpID)
    {
        SqlParameter[] sqlParams = new SqlParameter[1];
        sqlParams[0] = new SqlParameter("@EmpID", EmpID);
        return SQLDBUtil.ExecuteDataset("T_G_Leaves_GetTypeOfLeavesList_BasedOnGender", sqlParams);
    }
    public static DataSet T_G_Leaves_GetTypeOfLeavesList_BasedOnGender_Employeeportal(int EmpID)
    {
        SqlParameter[] sqlParams = new SqlParameter[1];
        sqlParams[0] = new SqlParameter("@EmpID", EmpID);
        return SQLDBUtil.ExecuteDataset("T_G_Leaves_GetTypeOfLeavesList_BasedOnGender_Employeeportal", sqlParams);
    }
    public static DataSet GetTypeofLeavesList_By_Status(int Status)
    {
        SqlParameter[] sqlParams = new SqlParameter[1];
        sqlParams[0] = new SqlParameter("@Staus", Status);
        return SQLDBUtil.ExecuteDataset("T_G_Leaves_GetTypeOfLeavesList_By_Status", sqlParams);
    }
    public static DataSet GetTypeofLeavesList_By_StatusLT(int LT)
    {
        SqlParameter[] sqlParams = new SqlParameter[1];
        sqlParams[0] = new SqlParameter("@LT", LT);
        return SQLDBUtil.ExecuteDataset("sph_Leaves_GetTypeOfLeavesList_By_StatusLT", sqlParams);
    }
    public static DataSet T_G_Leaves_GetTypeOfLeavesList_OT()
    {
        return SQLDBUtil.ExecuteDataset("T_G_Leaves_GetTypeOfLeavesList_OT");
    }
    public static void HR_OTCalculation(int EmpId, DateTime StDate, DateTime EdDate, int IsAccount)
    {
        try
        {
            SqlParameter[] parm = new SqlParameter[4];
            parm[0] = new SqlParameter("@StartDate", StDate);
            parm[1] = new SqlParameter("@EndDate", EdDate);
            parm[2] = new SqlParameter("@EmpId", EmpId);
            parm[3] = new SqlParameter("@IsAccount", IsAccount);
            int val = SQLDBUtil.ExecuteNonQuery("HR_OTCalculation", parm);
        }
        catch (Exception e)
        {
            throw e;
        }
    }
    public static DataSet HR_GetEMPSalariesExporttoExcel(DateTime Date, int? EMPID, int? WSID, int? Dept)
    {
        SqlParameter[] sqlParams = new SqlParameter[4];
        sqlParams[0] = new SqlParameter("@Date", Date);
        sqlParams[1] = new SqlParameter("@EMPID", EMPID);
        sqlParams[2] = new SqlParameter("@WSID", WSID);
        sqlParams[3] = new SqlParameter("@Dept", Dept);
        return SQLDBUtil.ExecuteDataset("HR_GetEMPSalariesExporttoExcel", sqlParams);
    }
    public static DataSet HR_GetEMPSalariesExporttoExcel_New(DateTime Date, int? EMPID, int? WSID, int? Dept)
    {
        SqlParameter[] sqlParams = new SqlParameter[4];
        sqlParams[0] = new SqlParameter("@Date", Date);
        sqlParams[1] = new SqlParameter("@EMPID", EMPID);
        sqlParams[2] = new SqlParameter("@WSID", WSID);
        sqlParams[3] = new SqlParameter("@Dept", Dept);
        return SQLDBUtil.ExecuteDataset("HR_GetEMPSalariesExporttoExcel_New", sqlParams);
    }
    public static DataSet GetTypeofLeavesDetails(int LeaveType)
    {
        try
        {
            DataSet ds = SQLDBUtil.ExecuteDataset("T_G_Leaves_GetTypeOfLeavesDetails", new SqlParameter[] { new SqlParameter("@LeaveType", LeaveType) });
            return ds;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion Type of Leaves
    #region Type of Holidays
    public static void InsUpdateTypeofHolidays(int ID, string Name, string Sname, int UserId, int Status)
    {
        try
        {
            SqlParameter[] sqlParams = new SqlParameter[4];
            sqlParams[0] = new SqlParameter("@HDType", ID);
            sqlParams[1] = new SqlParameter("@Name", Name);
            sqlParams[2] = new SqlParameter("@ShortName", Sname);
            sqlParams[3] = new SqlParameter("@IsActive", Status);
            SQLDBUtil.ExecuteNonQuery("T_G_Leaves_InsUpdateTypeOfHolidays", sqlParams);
        }
        catch (Exception e)
        {
            throw e;
        }
    }
    public static DataSet GetTypeofHolidaysList(int status)
    {
        if(status==2) // to get all rows        
        return SQLDBUtil.ExecuteDataset("T_G_Leaves_GetTypeOfHolidaysList",
            new SqlParameter[] { new SqlParameter("@status",  SqlDbType.Int) });
        else
            return SQLDBUtil.ExecuteDataset("T_G_Leaves_GetTypeOfHolidaysList",
            new SqlParameter[] { new SqlParameter("@status", status)});
    }
    public static DataSet GetTypeofHolidaysDetails(int HDType)
    {
        try
        {
            DataSet ds = SQLDBUtil.ExecuteDataset("T_G_Leaves_GetTypeOfHolidaysDetails", new SqlParameter[] { new SqlParameter("@HDType", HDType) });
            return ds;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion Type of Holidays
    #region Leaves Allocated R Applicable
    public static void InsUpdateApplicableLeaves(int ID, int LeaveTypeID, int ProfilerID, int Days, int UserId, int Status)
    {
        try
        {
            SqlParameter[] sqlParams = new SqlParameter[6];
            sqlParams[0] = new SqlParameter("@AllocatedID", ID);
            sqlParams[1] = new SqlParameter("@LeaveTypeID", LeaveTypeID);
            sqlParams[2] = new SqlParameter("@LeaveProfilerID", ProfilerID);
            sqlParams[5] = new SqlParameter("@AllocatedDays", Days);
            sqlParams[3] = new SqlParameter("@IsActive", Status);
            sqlParams[4] = new SqlParameter("@UserID", UserId);
            SQLDBUtil.ExecuteNonQuery("T_G_Leaves_InsUpdateLeavesAllocated", sqlParams);
        }
        catch (Exception e)
        {
            throw e;
        }
    }
    public static DataSet GetApplicableLeavesList()
    {
        return SQLDBUtil.ExecuteDataset("T_G_Leaves_GetLeavesAllocatedList");
    }
    public static DataSet GetApplicableLeavesDetails(int AllocatedID)
    {
        try
        {
            DataSet ds = SQLDBUtil.ExecuteDataset("T_G_Leaves_GetLeavesAllocatedDetails", new SqlParameter[] { new SqlParameter("@AllocatedID", AllocatedID) });
            return ds;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion Leaves Allocated R Applicable
    #region Leaves available
    public static DataSet GetLeaveCombination()
    {
        return SQLDBUtil.ExecuteDataset("HR_GET_LeaveCombinations");
    }
    public static void InsUpdateLeaveCombination(int Leave1, int Leave2, int Status)
    {
        try
        {
            SqlParameter[] sqlParams = new SqlParameter[3];
            sqlParams[0] = new SqlParameter("@Leave1", Leave1);
            sqlParams[1] = new SqlParameter("@Leave2", Leave2);
            sqlParams[2] = new SqlParameter("@Status", Status);
            SQLDBUtil.ExecuteNonQuery("HR_InsUpd_LeaveCombinations", sqlParams);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public static DataSet GetApplicableLeavesDetails(int SiteID, int DeptID)
    {
        try
        {
            DataSet ds = SQLDBUtil.ExecuteDataset("T_G_Leaves_GetAvailableLeavesList", new SqlParameter[] { new SqlParameter("@SiteID", SiteID), new SqlParameter("@DeptNo", DeptID) });
            return ds;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion Leaves available
    #region List of Holidays
    public static void InsUpdateListofHD(int HDId, string Holiday, DateTime Date, int HDType, int ProfileId, int Status)
    {
        try
        {
            SqlParameter[] sqlParams = new SqlParameter[6];
            sqlParams[0] = new SqlParameter("@HDId", HDId);
            sqlParams[1] = new SqlParameter("@Holiday", Holiday);
            sqlParams[4] = new SqlParameter("@Date", Date);
            sqlParams[2] = new SqlParameter("@HDType", HDType);
            sqlParams[5] = new SqlParameter("@ProfileId", ProfileId);
            sqlParams[3] = new SqlParameter("@IsActive", Status);
            SQLDBUtil.ExecuteNonQuery("T_G_Leaves_InsUpdateListofHOD", sqlParams);
        }
        catch (Exception e)
        {
            throw e;
        }
    }
    public static DataSet GetListofHDList(int Year, int Nature)
    {
        SqlParameter[] sqlParams = new SqlParameter[2];
        sqlParams[0] = new SqlParameter("@Year", Year);
        sqlParams[1] = new SqlParameter("@Nature", Nature);
        DataSet ds = SQLDBUtil.ExecuteDataset("T_G_Leaves_GetHODList", sqlParams);
        return ds;
    }
    public static DataSet GetListofHDDetails(int HDId)
    {
        try
        {
            DataSet ds = SQLDBUtil.ExecuteDataset("T_G_Leaves_GetHODDetails", new SqlParameter[] { new SqlParameter("@HDId", HDId) });
            return ds;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion List of Holidays
    #region LeaveEntitlement
    public static void InsUpdateLeaveEntitlement(int LEId, int LeaveType, int NatureOfEmp, int AllotmentCycle, decimal MaxLeaveEligibility
        , int MinDaysOfWork, decimal MaxLeaveElgYear, int Gender, int IsAccure, int IsCFrwd, int isEncashable, decimal PayCoeff
        , decimal MinService, int PayType)
    {
        try
        {
            SqlParameter[] sqlParams = new SqlParameter[14];
            sqlParams[0] = new SqlParameter("@LEId", LEId);
            sqlParams[1] = new SqlParameter("@LeaveType", LeaveType);
            sqlParams[4] = new SqlParameter("@NatureOfEmp", NatureOfEmp);
            sqlParams[2] = new SqlParameter("@AllotmentCycle", AllotmentCycle);
            sqlParams[5] = new SqlParameter("@MaxLeaveEligibility", MaxLeaveEligibility);
            sqlParams[3] = new SqlParameter("@MinDaysOfWork", MinDaysOfWork);
            sqlParams[6] = new SqlParameter("@MaxLeaveElgYear", MaxLeaveElgYear);
            sqlParams[7] = new SqlParameter("@Gender", Gender);
            sqlParams[8] = new SqlParameter("@IsAccure", IsAccure);
            sqlParams[9] = new SqlParameter("@IsCFrwd", IsCFrwd);
            sqlParams[10] = new SqlParameter("@isEncashable", isEncashable);
            sqlParams[11] = new SqlParameter("@PayCoeff", PayCoeff);
            sqlParams[12] = new SqlParameter("@MinService", MinService);
            sqlParams[13] = new SqlParameter("@PayType", PayType);
            SQLDBUtil.ExecuteNonQuery("HR_InsUpd_LeaveEntitlement", sqlParams);
        }
        catch (Exception e)
        {
        }
    }
    public static DataSet GetEntitlementList(int empnature)
    {
        SqlParameter[] sqlParams = new SqlParameter[1];
        if (empnature == 0)
            sqlParams[0] = new SqlParameter("@employee_nature", System.Data.SqlDbType.Int);
        else
            sqlParams[0] = new SqlParameter("@employee_nature", empnature);
        return SQLDBUtil.ExecuteDataset("HR_Get_LeaveEntitlementList", sqlParams);
    }
    public static DataSet GetLeaveEntitlementDetails(int LEId)
    {
        try
        {
            DataSet ds = SQLDBUtil.ExecuteDataset("HR_Get_LeaveEntitlementDetails", new SqlParameter[] { new SqlParameter("@LEId", LEId) });
            return ds;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion
    #region LeaveAllotmentList
    public static DataSet GetLeaveAllotmentList()
    {
        return SQLDBUtil.ExecuteDataset("HR_Get_LeaveAllotmentList");
    }
    #endregion
    public static DataSet GetEmployeesForSalariesWithHisID(HRCommon objHrCommon, int OrderId, int Direction, string Name, int? EmpNatID, int CompanyID)
    {
        try
        {
            SqlParameter[] sqlParams = new SqlParameter[14];
            sqlParams[0] = new SqlParameter("@CurrentPage", objHrCommon.CurrentPage);
            sqlParams[1] = new SqlParameter("@PageSize", objHrCommon.PageSize);
            sqlParams[2] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
            sqlParams[2].Direction = ParameterDirection.ReturnValue;
            sqlParams[3] = new SqlParameter("@NoofRecords", System.Data.SqlDbType.Int);
            sqlParams[3].Direction = ParameterDirection.Output;
            sqlParams[4] = new SqlParameter("@SiteID", objHrCommon.SiteID);
            sqlParams[5] = new SqlParameter("@DeptID", objHrCommon.DeptID);
            sqlParams[6] = new SqlParameter("@EmpStatus", objHrCommon.CurrentStatus);
            sqlParams[7] = new SqlParameter("@Month", objHrCommon.Month);
            sqlParams[8] = new SqlParameter("@Year", objHrCommon.Year);
            sqlParams[9] = new SqlParameter("@OrdeID", OrderId);
            sqlParams[10] = new SqlParameter("@Direction", Direction);
            sqlParams[11] = new SqlParameter("@EmpName", Name);
            sqlParams[12] = new SqlParameter("@EmpNatureID", EmpNatID);
            sqlParams[13] = new SqlParameter("@CompanyID", CompanyID);
            DataSet ds = SQLDBUtil.ExecuteDataset("HR_EmpSalriesListWithHisID_gana", sqlParams);
            objHrCommon.NoofRecords = (int)sqlParams[3].Value;
            objHrCommon.TotalPages = (int)sqlParams[2].Value;
            return ds;
        }
        catch (Exception e)
        {
            throw e;
        }
    }
    public static SqlDataReader GetEmployeesForSalariesExportToExcel(int? SiteID, int? Dept, int? Month, int Year, char Status, int? EmpNat)//, int PageSize)
    {
        try
        {
            SqlParameter[] sqlParams = new SqlParameter[6];
            sqlParams[0] = new SqlParameter("@SiteID", SiteID);
            sqlParams[1] = new SqlParameter("@DeptID", Dept);
            sqlParams[2] = new SqlParameter("@EmpStatus", Status);
            sqlParams[3] = new SqlParameter("@Month", Month);
            sqlParams[4] = new SqlParameter("@Year", Year);
            sqlParams[5] = new SqlParameter("@EmpNat", EmpNat);
            SqlDataReader dr = SQLDBUtil.ExecuteDataReader("HR_EmpSalriesListExpotyExcel_GanaCash", sqlParams);
            return dr;
        }
        catch (Exception e)
        {
            throw e;
        }
    }
    public static SqlDataReader GetEmployeesForSalariesExportToExcel_NewV4(int? SiteID, int? Dept, int? Month, int Year, char Status, int? EmpNat)//, int PageSize)
    {
        try
        {
            SqlParameter[] sqlParams = new SqlParameter[6];
            sqlParams[0] = new SqlParameter("@SiteID", SiteID);
            sqlParams[1] = new SqlParameter("@DeptID", Dept);
            sqlParams[2] = new SqlParameter("@EmpStatus", 'y');
            sqlParams[3] = new SqlParameter("@Month", Month);
            sqlParams[4] = new SqlParameter("@Year", Year);
            sqlParams[5] = new SqlParameter("@EmpNat", EmpNat);
            SqlDataReader dr = SQLDBUtil.ExecuteDataReader("HR_EmpSalriesListExpotyExcel_GanaCash_NewV4", sqlParams);
            return dr;
        }
        catch (Exception e)
        {
            throw e;
        }
    }
    public static DataSet GetEmployeesForSalariesExportToExcel_NewV4_ds(int? SiteID, int? Dept, int? Month, int Year, char Status, int? EmpNat)//, int PageSize)
    {
        try
        {
            SqlParameter[] sqlParams = new SqlParameter[6];
            sqlParams[0] = new SqlParameter("@SiteID", SiteID);
            sqlParams[1] = new SqlParameter("@DeptID", Dept);
            sqlParams[2] = new SqlParameter("@EmpStatus", 'y');
            sqlParams[3] = new SqlParameter("@Month", Month);
            sqlParams[4] = new SqlParameter("@Year", Year);
            sqlParams[5] = new SqlParameter("@EmpNat", EmpNat);
            return SQLDBUtil.ExecuteDataset("HR_EmpSalriesListExpotyExcel_GanaCash_NewV4", sqlParams);
            // return dr;
        }
        catch (Exception e)
        {
            throw e;
        }
    }
    public static SqlDataReader sh_GetWSSummary(int MID, int YID)
    {
        try
        {
            SqlParameter[] sqlParams = new SqlParameter[2];
            sqlParams[0] = new SqlParameter("@MID", MID);
            sqlParams[1] = new SqlParameter("@YID", YID);
            SqlDataReader dr = SQLDBUtil.ExecuteDataReader("sh_GetWSSummary", sqlParams);
            return dr;
        }
        catch (Exception e)
        {
            throw e;
        }
    }
    public static DataSet sh_GetWSSummary_ds(int MID, int YID)
    {
        try
        {
            SqlParameter[] sqlParams = new SqlParameter[2];
            sqlParams[0] = new SqlParameter("@MID", MID);
            sqlParams[1] = new SqlParameter("@YID", YID);
            return SQLDBUtil.ExecuteDataset("sh_GetWSSummary", sqlParams);
            // return dr;
        }
        catch (Exception e)
        {
            throw e;
        }
    }
    public static DataSet USP_Salaries_All_Approved_Search_4(int? SiteID, int? Month, int Year, int CurrentPage, int PageSize)//, int PageSize)
    {
        try
        {
            SqlParameter[] sqlParams = new SqlParameter[8];
            sqlParams[0] = new SqlParameter("@CurrentPage", CurrentPage);
            sqlParams[1] = new SqlParameter("@PageSize", PageSize);
            sqlParams[2] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
            sqlParams[2].Direction = ParameterDirection.ReturnValue;
            sqlParams[3] = new SqlParameter("@NoofRecords", System.Data.SqlDbType.Int);
            sqlParams[3].Direction = ParameterDirection.Output;
            sqlParams[4] = new SqlParameter("@wsid", SiteID);
            sqlParams[5] = new SqlParameter("@Month", Month);
            sqlParams[6] = new SqlParameter("@Year", Year);
            sqlParams[7] = new SqlParameter("@Empid", DBNull.Value);
            return SQLDBUtil.ExecuteDataset("USP_Salaries_Approved_Rev4", sqlParams);
            //objHrCommon.NoofRecords = (int)sqlParams[3].Value;
            //objHrCommon.TotalPages = (int)sqlParams[2].Value;
            //return ds;
        }
        catch (Exception e)
        {
            clsErrorLog.HMSEventLog(e, "Salaries", "btnExcelExport_Click", "006");
            throw e;
        }
    }
    public static SqlDataReader GetEmployeesDtlRpt(HRCommon objHrCommon, int? EmpNat)
    {
        try
        {
            SqlParameter[] sqlParams = new SqlParameter[6];
            sqlParams[0] = new SqlParameter("@SiteID", objHrCommon.SiteID);
            sqlParams[1] = new SqlParameter("@DeptID", objHrCommon.DeptID);
            sqlParams[2] = new SqlParameter("@EmpStatus", objHrCommon.CurrentStatus);
            sqlParams[3] = new SqlParameter("@Month", objHrCommon.Month);
            sqlParams[4] = new SqlParameter("@Year", objHrCommon.Year);
            sqlParams[5] = new SqlParameter("@EmpNat", EmpNat);
            SqlDataReader dr = SQLDBUtil.ExecuteDataReader("HR_EmpSalriesListExpotyExcel_Gana", sqlParams);
            return dr;
        }
        catch (Exception e)
        {
            throw e;
        }
    }
    internal static DataSet HR_GetEMPGradesDetailsforDDL()
    {
        try
        {
            DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetEMPGradesDetailsforDDL");
            return ds;
        }
        catch (Exception e)
        {
            throw e;
        }
    }
    public static DataSet HR_GetShiftsBreaks()
    {
        return SQLDBUtil.ExecuteDataset("HR_GetShiftsBreaks");
    }
    public static void InsUpdateLeaveEntitlement(decimal BreackVal)
    {
        try
        {
            SqlParameter[] sqlParams = new SqlParameter[1];
            sqlParams[0] = new SqlParameter("@BreackVal", BreackVal);
            SQLDBUtil.ExecuteNonQuery("HR_UpdateShiftsBreaks", sqlParams);
        }
        catch (Exception ex)
        {
        }
    }
    public static DataSet HMS_RPT_GetWorkSitewiseAtt(int? WSID, DateTime Data)
    {
        try
        {
            SqlParameter[] sqlParams = new SqlParameter[2];
            sqlParams[0] = new SqlParameter("@WSID", WSID);
            sqlParams[1] = new SqlParameter("@Data", Data);
            DataSet ds = SQLDBUtil.ExecuteDataset("HMS_RPT_GetWorkSitewiseAtt", sqlParams);
            return ds;
        }
        catch (Exception ex)
        {
        }
        return null;
    }
    public static DataSet HR_OTCalculation_Details(DateTime StartDate, DateTime EndDate, int EmpId, int IsAccount)
    {
        try
        {
            SqlParameter[] sqlParams = new SqlParameter[4];
            sqlParams[0] = new SqlParameter("@StartDate", StartDate);
            sqlParams[1] = new SqlParameter("@EndDate", EndDate);
            sqlParams[2] = new SqlParameter("@EmpId", EmpId);
            sqlParams[3] = new SqlParameter("@IsAccount", IsAccount);
            DataSet ds = SQLDBUtil.ExecuteDataset("HR_OTCalculation_Details", sqlParams);
            return ds;
        }
        catch (Exception ex)
        {
        }
        return null;
    }
    public static DataSet sh_getoptionbyid(int id)
    {
        try
        {
            SqlParameter[] sqlParams = new SqlParameter[1];
            sqlParams[0] = new SqlParameter("@id", id);
            DataSet ds = SQLDBUtil.ExecuteDataset("sh_getoptionbyid", sqlParams);
            return ds;
        }
        catch (Exception ex)
        {
        }
        return null;
    }
    public static void sh_updateOptionByid(int ID, string val)
    {
        try
        {
            SqlParameter[] sqlParams = new SqlParameter[2];
            sqlParams[0] = new SqlParameter("@ID", ID);
            sqlParams[1] = new SqlParameter("@val", val);
            SQLDBUtil.ExecuteNonQuery("sh_updateOptionByid", sqlParams);
        }
        catch (Exception ex)
        {
        }
    }
}
