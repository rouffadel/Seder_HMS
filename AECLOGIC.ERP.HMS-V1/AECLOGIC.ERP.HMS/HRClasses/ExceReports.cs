using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataAccessLayer;
using System.Data;
using System.Data.SqlClient;
using AECLOGIC.HMS.BLL;
using Aeclogic.Common.DAL;
/// <summary>
/// Summary description for ExceReports
/// </summary>
public class ExceReports
{
    public ExceReports()
    {
        //
        // TODO: Add constructor logic here
        //
    }

   
    public static DataSet ExceRptAttendance(HRCommon objHrCommon, DateTime? Date, int? Month, int? Year, int? SiteID, int Hours, int? EmpID, int? DeptID, int WHSType,int CompanyID)
    {
        try
        {
            SqlParameter[] sqlParams = new SqlParameter[13];
            sqlParams[0] = new SqlParameter("@CurrentPage", objHrCommon.CurrentPage);
            sqlParams[1] = new SqlParameter("@PageSize", objHrCommon.PageSize);
            sqlParams[2] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
            sqlParams[2].Direction = ParameterDirection.ReturnValue;
            sqlParams[3] = new SqlParameter("@NoofRecords", System.Data.SqlDbType.Int);
            sqlParams[3].Direction = ParameterDirection.Output;
            sqlParams[4] = new SqlParameter("@Date", Date);
            sqlParams[5] = new SqlParameter("@Month", Month);
            sqlParams[6] = new SqlParameter("@Year", Year);
            sqlParams[7] = new SqlParameter("@SiteID", SiteID);
            sqlParams[8] = new SqlParameter("@Hours", Hours);
            sqlParams[9] = new SqlParameter("@EmpID", EmpID);
            sqlParams[10] = new SqlParameter("@DeptID", DeptID);
            sqlParams[11] = new SqlParameter("@CompanyID", CompanyID);
            sqlParams[12] = new SqlParameter("@WHSType", WHSType);

            DataSet ds = new DataSet();
            ds = SQLDBUtil.ExecuteDataset("HR_ExceRptAttendance", sqlParams);
            if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0)
            {
                objHrCommon.NoofRecords = (int)sqlParams[3].Value;
                objHrCommon.TotalPages = (int)sqlParams[2].Value;
            }
            return ds;
        }
        catch (Exception e)
        {
            throw e;
        }
    }
    public static DataSet ExceRptAttendanceByEmpID(DateTime? Date, int? Month, int? Year, int? SiteID, int Hours, int? EmpID, int WHSType,int CompanyID)
    {
        try
        {
            DataSet ds = new DataSet();
            ds = SQLDBUtil.ExecuteDataset("HR_ExceRptAttendanceByEmpID", new SqlParameter[] { new SqlParameter("@Date", Date),
                                                                                       new SqlParameter("@Month", Month),
                                                                                       new SqlParameter("@Year",Year ),
                                                                                       new SqlParameter("@SiteID", SiteID),
                                                                                       new SqlParameter("@Hours",Hours ),
                                                                                       new SqlParameter("@EmpID",EmpID ) ,
                                                                                       new SqlParameter("@WHSType",WHSType ),
                                                                                       new SqlParameter("@CompanyID",CompanyID)

                                                                                     });
            return ds;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    public static DataSet ExceRptReportingto(HRCommon objHrCommon, int? SiteID, int? DeptID, string EmpName)
    {
        try
        {
            SqlParameter[] sqlParams = new SqlParameter[7];
            sqlParams[0] = new SqlParameter("@CurrentPage", objHrCommon.CurrentPage);
            sqlParams[1] = new SqlParameter("@PageSize", objHrCommon.PageSize);
            sqlParams[2] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
            sqlParams[2].Direction = ParameterDirection.ReturnValue;
            sqlParams[3] = new SqlParameter("@NoofRecords", System.Data.SqlDbType.Int);
            sqlParams[3].Direction = ParameterDirection.Output;
            sqlParams[4] = new SqlParameter("@SiteID", SiteID);
            sqlParams[5] = new SqlParameter("@DeptID", DeptID);
            sqlParams[6] = new SqlParameter("@EmpName", EmpName);


            DataSet ds = new DataSet();
            ds = SQLDBUtil.ExecuteDataset("HR_ExceRptReportingto", sqlParams);
            objHrCommon.NoofRecords = (int)sqlParams[3].Value;
            objHrCommon.TotalPages = (int)sqlParams[2].Value;
            return ds;
        }
        catch (Exception e)
        {
            throw e;
        }
    }
    public static DataSet ExceRptReportingtoByPaging(HRCommon objHrCommon, int? SiteID, int? DeptID, string EmpName, int? EmpID,int CompanyID)
    {
        try
        {
            SqlParameter[] sqlParams = new SqlParameter[10];
            sqlParams[0] = new SqlParameter("@CurrentPage", objHrCommon.CurrentPage);
            sqlParams[1] = new SqlParameter("@PageSize", objHrCommon.PageSize);
            sqlParams[2] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
            sqlParams[2].Direction = ParameterDirection.ReturnValue;
            sqlParams[3] = new SqlParameter("@NoofRecords", System.Data.SqlDbType.Int);
            sqlParams[3].Direction = ParameterDirection.Output;
            sqlParams[4] = new SqlParameter("@SiteID", SiteID);
            sqlParams[5] = new SqlParameter("@DeptID", DeptID);
            sqlParams[6] = new SqlParameter("@EmpName", EmpName);
            sqlParams[7] = new SqlParameter("@EmpID", EmpID);
            sqlParams[8] = new SqlParameter("@OldEmpID", objHrCommon.OldEmpID);
            sqlParams[9] = new SqlParameter("@CompanyID", CompanyID);


            DataSet ds = new DataSet();
            ds = SQLDBUtil.ExecuteDataset("HR_ExceRptReportingtoByPaging", sqlParams);
            objHrCommon.NoofRecords = (int)sqlParams[3].Value;
            objHrCommon.TotalPages = (int)sqlParams[2].Value;
            return ds;
        }
        catch (Exception e)
        {
            throw e;
        }
    }






    public static DataSet ExceRptLeaves(DateTime? Date, int? Month, int? Year, int? SiteID, int? EmpID, int? DeptID)
    {
        try
        {
            DataSet ds = new DataSet();
            ds = SQLDBUtil.ExecuteDataset("HR_ExceRptLeaves", new SqlParameter[] { new SqlParameter("@Date", Date),
                                                                                       new SqlParameter("@Month", Month),
                                                                                       new SqlParameter("@Year",Year ),
                                                                                       new SqlParameter("@SiteID", SiteID),
                                                                                       new SqlParameter("@EmpID",EmpID ) ,
                                                                                       new SqlParameter("@DeptID",DeptID ),
                                                                                     });
            return ds;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static DataSet ExceRptLeavesByEmpID(DateTime? Date, int? Month, int? Year, int? SiteID, int? EmpID)
    {
        try
        {
            DataSet ds = new DataSet();
            ds = SQLDBUtil.ExecuteDataset("HR_ExceRptLeavesByEmpID", new SqlParameter[] { new SqlParameter("@Date", Date),
                                                                                       new SqlParameter("@Month", Month),
                                                                                       new SqlParameter("@Year",Year ),
                                                                                       new SqlParameter("@SiteID", SiteID),
                                                                                       new SqlParameter("@EmpID",EmpID ) 
                                                                                     });
            return ds;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    public static DataSet ExceRptInOutTimesByEmpID(int? Month, int? Year, int? EmpID, string EmpName)
    {
        try
        {
            DataSet ds = new DataSet();
            ds = SQLDBUtil.ExecuteDataset("HR_ExceRptInOutTimesByEmpID", new SqlParameter[] { new SqlParameter("@Month", Month),
                                                                                              new SqlParameter("@Year",Year ),
                                                                                              new SqlParameter("@EmpName",EmpName ),
                                                                                              new SqlParameter("@EmpID",EmpID ) 
                                                                                     });
            return ds;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

}