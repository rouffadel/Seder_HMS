using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aeclogic.Common.DAL;

namespace AECLOGIC.HMS.BLL
{
    public class AttendanceDAC
    {
        interface ISample
        {

        }
        public static DataSet GetHMS_DDL_WorkSite(int EmpID, int ModID, int CompanyID)
        {
            SqlParameter[] parms = new SqlParameter[3];
            parms[0] = new SqlParameter("@EMPID", EmpID);
            parms[1] = new SqlParameter("@ModID", ModID);
            parms[2] = new SqlParameter("@CompanyID", CompanyID);
            DataSet dsrt = SQLDBUtil.ExecuteDataset("DDL_WorkSite_All", parms);
            return dsrt;
        }
        public static DataSet GetWorkSites(String SearchKey, int CompanyID, int EmpId, int ModID_Per)
        {
            try
            {
                SqlParameter[] par = new SqlParameter[4];
                par[0] = new SqlParameter("@Search", SearchKey);
                par[1] = new SqlParameter("@CompanyID", CompanyID);
                par[2] = new SqlParameter("@EMPID", EmpId);
                par[3] = new SqlParameter("@ModID", ModID_Per);
                return SQLDBUtil.ExecuteDataset("DDL_WorkSite_All", par);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet HMS_googlesearch_employeeworkstatus(String SearchKey)
        {
            try
            {
                SqlParameter[] par = new SqlParameter[1];
                par[0] = new SqlParameter("@Search", SearchKey);
                return SQLDBUtil.ExecuteDataset("HMS_googlesearch_employeeworkstatus", par);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet GetEmployeesByAll_googlesearch(String SearchKey)
        {
            try
            {
                SqlParameter[] par = new SqlParameter[1];
                par[0] = new SqlParameter("@search", SearchKey);
                return SQLDBUtil.ExecuteDataset("GetEmployeesByAll_googlesearch", par);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet GetWorkSites(String SearchKey, int CompanyID, int EmpId, int ModID_Per, int wsid)
        {
            try
            {
                SqlParameter[] par = new SqlParameter[5];
                par[0] = new SqlParameter("@Search", SearchKey);
                par[1] = new SqlParameter("@CompanyID", CompanyID);
                par[2] = new SqlParameter("@EMPID", EmpId);
                par[3] = new SqlParameter("@ModID", ModID_Per);
                par[4] = new SqlParameter("@wsid", DBNull.Value);
                return SQLDBUtil.ExecuteDataset("DDL_WorkSite_All", par);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet GETEMPCOSTCENTERS(int EmpID, int ModID)
        {
            SqlParameter[] parms = new SqlParameter[2];
            parms[0] = new SqlParameter("@EMPID", EmpID);
            parms[1] = new SqlParameter("@ModID", ModID);
            DataSet ds = SQLDBUtil.ExecuteDataset("MMS_GetEmpWS", parms);
            return ds;
        }
        public static DataSet GetEmployeeRolesByPaging(int? WorksiteID, string EmpName, int Empid, int? DeptID, Common ObjCommon)
        {
            SqlParameter[] p = new SqlParameter[8];
            p[0] = new SqlParameter("@worksiteid", WorksiteID);
            p[1] = new SqlParameter("@EmpName", EmpName);
            p[2] = new SqlParameter("@DeptId", DeptID);
            p[3] = new SqlParameter("@CurrentPage", ObjCommon.CurrentPage);
            p[4] = new SqlParameter("@PageSize", ObjCommon.PageSize);
            p[5] = new SqlParameter("@NoofRecords", 3);
            p[5].Direction = ParameterDirection.Output;
            p[6] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
            p[6].Direction = ParameterDirection.ReturnValue;
            p[7] = new SqlParameter("@Empid", Empid);
            DataSet Ds = SQLDBUtil.ExecuteDataset("AMS_GetEmployeeRolesByPaging", p);
            ObjCommon.NoofRecords = (int)p[5].Value;
            ObjCommon.TotalPages = (int)p[6].Value;
            return Ds;
        }
        public static int MAPCOSTCENTER(int EmpID, int Status, int CostCenterID, int ModID)
        {
            SqlParameter[] parms = new SqlParameter[4];
            parms[0] = new SqlParameter("@EMPID", EmpID);
            parms[1] = new SqlParameter("@STATUS", Status);
            parms[2] = new SqlParameter("@COSTCENTER", CostCenterID);
            parms[3] = new SqlParameter("@ModID", ModID);
            return SQLDBUtil.ExecuteNonQuery("MMS_MapWorksite", parms, "");
        }
        public static DataSet GetDepartments()
        {
            DataSet ds = SQLDBUtil.ExecuteDataset("AMS_GETDEPARTMENTS");
            return ds;
        }
        public DataSet GetOperations()
        {
            try
            {
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetOperations");
                return ds;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public int InsUpdateDeleteOperations(int ID, string Operation, int UserId, int Status)
        {
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[4];
                sqlParams[0] = new SqlParameter("@ID", ID);
                sqlParams[1] = new SqlParameter("@Operation", Operation);
                sqlParams[2] = new SqlParameter("@UserId", UserId);
                sqlParams[3] = new SqlParameter("@Status", Status);
                SQLDBUtil.ExecuteNonQuery("HR_InsUpdateDeleteOperations", sqlParams);
                return 1;
            }
            catch (Exception e)
            {
                return -1;
            }
        }
        public static DataSet GetContraccttype_googlesearch(String SearchKeys, int CID)
        {
            try
            {
                SqlParameter[] sqlPrms = new SqlParameter[2];
                sqlPrms[0] = new SqlParameter("@SEARCH", SearchKeys);
                sqlPrms[1] = new SqlParameter("@CID", CID);
                return SQLDBUtil.ExecuteDataset("HR_getContactTypes_googlesearch", sqlPrms);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static DataSet BindEmpdetails(int Empid)
        {
            SqlParameter[] parm = new SqlParameter[1];
            parm[0] = new SqlParameter("@Empid ", Empid);
            DataSet ds = SQLDBUtil.ExecuteDataset("EmpDetailsWithDetails", parm);
            return ds;
        }
        public static DataSet GetEmpSalDetails(int EmpSalID)
        {
            try
            {
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_GET_EmphikeSalDetails", new SqlParameter[] { new SqlParameter("@EmpSalID", EmpSalID) });
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet GetSearchDesigination(String SearchKey)
        {
            try
            {
                SqlParameter[] par = new SqlParameter[1];
                par[0] = new SqlParameter("@Search", SearchKey);
                return SQLDBUtil.ExecuteDataset("HR_Designation_SearchCompany", par);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet GetSearchworksite_by_LoanAdv(String SearchKey)
        {
            try
            {
                SqlParameter[] par = new SqlParameter[1];
                par[0] = new SqlParameter("@Search", SearchKey);
                return SQLDBUtil.ExecuteDataset("HR_GetWorkSite_By_LoanAdv_googlesearch", par);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet HR_GetWorkSite_By_Applicants_googlesearch(String SearchKey)
        {
            try
            {
                SqlParameter[] par = new SqlParameter[1];
                par[0] = new SqlParameter("@Search", SearchKey);
                return SQLDBUtil.ExecuteDataset("HR_GetWorkSite_By_Applicants_googlesearch", par);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet GetGoogleblackName(String SearchKey)
        {
            try
            {
                SqlParameter[] par = new SqlParameter[1];
                par[0] = new SqlParameter("@Search", SearchKey);
                return SQLDBUtil.ExecuteDataset("HR_Getgooglesearch_Black", par);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet HR_GetWorkSite_By_OfferList_googlesearch(String SearchKey)
        {
            try
            {
                SqlParameter[] par = new SqlParameter[1];
                par[0] = new SqlParameter("@Search", SearchKey);
                return SQLDBUtil.ExecuteDataset("HR_GetWorkSite_By_OfferList_googlesearch", par);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet HR_GetWorkSite_By_AcceptedOfferList_googlesearch(String SearchKey)
        {
            try
            {
                SqlParameter[] par = new SqlParameter[1];
                par[0] = new SqlParameter("@Search", SearchKey);
                return SQLDBUtil.ExecuteDataset("HR_GetWorkSite_By_AcceptedOfferList_googlesearch", par);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet HR_GetWorkSite_googlesearch_By_WO(String SearchKey)
        {
            try
            {
                SqlParameter[] par = new SqlParameter[1];
                par[0] = new SqlParameter("@Search", SearchKey);
                return SQLDBUtil.ExecuteDataset("HR_GetWorkSite_googlesearch_By_WO", par);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet GetSearchEmpName(string SearchKey)
        {
            try
            {
                SqlParameter[] par = new SqlParameter[1];
                par[0] = new SqlParameter("@search", SearchKey);
                return SQLDBUtil.ExecuteDataset("HMS_Name_basedon_dept_Googlesearch", par);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet GetSearchEmpName_dept(string SearchKey)
        {
            try
            {
                SqlParameter[] par = new SqlParameter[1];
                par[0] = new SqlParameter("@search", SearchKey);
                return SQLDBUtil.ExecuteDataset("HMS_Name_dept_Googlesearch", par);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet GetSearchName_APPDetails(string SearchKey)
        {
            try
            {
                SqlParameter[] par = new SqlParameter[1];
                par[0] = new SqlParameter("@search", SearchKey);
                return SQLDBUtil.ExecuteDataset("HMS_Name_basedon_AppDetailsGooglesearch", par);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet GetSearchdesigination_googlesearch(String SearchKey)
        {
            try
            {
                SqlParameter[] par = new SqlParameter[1];
                par[0] = new SqlParameter("@Search", SearchKey);
                return SQLDBUtil.ExecuteDataset("HR_GetDesignations_googlesearch", par);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet HR_googlesearch_GetDesignations(String SearchKey)
        {
            try
            {
                SqlParameter[] par = new SqlParameter[1];
                par[0] = new SqlParameter("@Search", SearchKey);
                return SQLDBUtil.ExecuteDataset("HR_googlesearch_GetDesignations", par);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet Getfrmsearchworksite_by_EmpLIst_googlesearch(String SearchKey)
        {
            try
            {
                SqlParameter[] par = new SqlParameter[1];
                par[0] = new SqlParameter("@Search", SearchKey);
                return SQLDBUtil.ExecuteDataset("HR_GetWorkSite_By_EmpList_googlesearch", par);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet HR_GetWorkSite_By_googlesearch_EmpList(String SearchKey, int WSID)
        {
            try
            {
                SqlParameter[] par = new SqlParameter[2];
                par[0] = new SqlParameter("@Search", SearchKey);
                par[1] = new SqlParameter("@WSID", WSID);
                return SQLDBUtil.ExecuteDataset("HR_GetWorkSite_By_googlesearch_EmpList", par);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet GetDepartmentListgooglesearch(String SearchKey)
        {
            try
            {
                SqlParameter[] par = new SqlParameter[1];
                par[0] = new SqlParameter("@Search", SearchKey);
                return SQLDBUtil.ExecuteDataset("HR_GetDepartmentList_googlesearch", par);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet HR_GetMobilesWS_DeptFilter_googlesearch(String SearchKey, int WS)
        {
            try
            {
                SqlParameter[] par = new SqlParameter[2];
                par[0] = new SqlParameter("@Search", SearchKey);
                par[1] = new SqlParameter("@WS", WS);
                return SQLDBUtil.ExecuteDataset("HR_GetMobilesWS_DeptFilter_googlesearch", par);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet GetGoogleSearch_ForGeneralEmployee(int prefixText)
        {
            try
            {
                SqlParameter[] sqlPrms = new SqlParameter[1];
                sqlPrms[0] = new SqlParameter("@SEARCH", prefixText);
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetWorkSite_ForSession", sqlPrms);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet GetWorkSite_EmpPenalties(String SearchKey, int CompanyID)
        {
            try
            {
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetWorkSite_EmpPenaltiesGS", new SqlParameter[] { new SqlParameter("@Search", SearchKey), new SqlParameter("@WSID", 0), new SqlParameter("@WSStatus", 1), new SqlParameter("@CompanyID", CompanyID) });
                return ds;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static DataSet GetWorkSite(int WSId, char Staus, int CompanyID)
        {
            try
            {
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetWorkSite", new SqlParameter[] 
                { new SqlParameter("@WSID", WSId),
                    new SqlParameter("@WSStatus", Staus), 
                    new SqlParameter("@CompanyID", CompanyID) 
                });
                return ds;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static DataSet GetWorkSite_by_Wsid(string SearchKey, int WSId, char Staus, int CompanyID)
        {
            try
            {
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetWorkSite_googlesearch_basedon_Wsid", new SqlParameter[] 
                {
                    new SqlParameter("@search",SearchKey),
                    new SqlParameter("@WSID", WSId),
                    new SqlParameter("@WSStatus", Staus), 
                    new SqlParameter("@CompanyID", CompanyID) 
                });
                return ds;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static DataSet HR_GetWorkSite_basedon_Wsid_googlesearch(string SearchKey, int WSId, char Staus, int CompanyID)
        {
            try
            {
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetWorkSite_basedon_Wsid_googlesearch", new SqlParameter[] 
                {
                    new SqlParameter("@search",SearchKey),
                    new SqlParameter("@WSID", WSId),
                    new SqlParameter("@WSStatus", Staus), 
                    new SqlParameter("@CompanyID", CompanyID) 
                });
                return ds;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static DataSet HR_GetEmpdetails(string SearchKey)
        {
            SqlParameter[] parm = new SqlParameter[1];
            parm[0] = new SqlParameter("@search", SearchKey);
            return SQLDBUtil.ExecuteDataset("HR_GetEmp_googleserch", parm);
        }
        public static DataSet HR_GetEmp(string SearchKey)
        {
            SqlParameter[] parm = new SqlParameter[1];
            parm[0] = new SqlParameter("@search", SearchKey);
            return SQLDBUtil.ExecuteDataset("HR_GetEmp_googlesearch", parm);
        }
        public static DataSet GetWorkSite_By_googlesearch_Emp(string SearchKey)
        {
            SqlParameter[] parm = new SqlParameter[1];
            parm[0] = new SqlParameter("@search", SearchKey);
            return SQLDBUtil.ExecuteDataset("HR_GetWorkSite_googlesearch_By_Emp", parm);
        }
        public static DataSet GetWorkSite_By_googlesearch_Emp(string SearchKey, int WSID)
        {
            SqlParameter[] parm = new SqlParameter[2];
            parm[0] = new SqlParameter("@search", SearchKey);
            parm[1] = new SqlParameter("@WSID", WSID);
            return SQLDBUtil.ExecuteDataset("HR_GetWorkSite_By_LoanAdv_siteid", parm);
        }
        public static DataSet MMS_DDL_WorkSite_googlesearch(string SearchKey, int CompanyID, int EMPID)
        {
            SqlParameter[] parm = new SqlParameter[3];
            parm[0] = new SqlParameter("@search", SearchKey);
            parm[1] = new SqlParameter("@CompanyID", CompanyID);
            parm[2] = new SqlParameter("@EMPID", EMPID);
            return SQLDBUtil.ExecuteDataset("MMS_DDL_WorkSite_googlesearch", parm);
        }
        public static DataSet get_employewise_jobdescription(String SearchKey)
        {
            try
            {
                SqlParameter[] par = new SqlParameter[1];
                par[0] = new SqlParameter("@Search", SearchKey);
                return SQLDBUtil.ExecuteDataset("get_employewise_jobdescription", par);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet get_employewise_Title(String SearchKey)
        {
            try
            {
                SqlParameter[] par = new SqlParameter[1];
                par[0] = new SqlParameter("@Search", SearchKey);
                return SQLDBUtil.ExecuteDataset("T_HMS_JobTitlesitems_Search1", par);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet get_employewise_TitleRes(String SearchKey)
        {
            try
            {
                SqlParameter[] par = new SqlParameter[1];
                par[0] = new SqlParameter("@Search", SearchKey);
                return SQLDBUtil.ExecuteDataset("T_HMS_JobTitlesitemsRes_Search1 ", par);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet get_employewise_resp(String SearchKey)
        {
            try
            {
                SqlParameter[] par = new SqlParameter[1];
                par[0] = new SqlParameter("@Search", SearchKey);
                return SQLDBUtil.ExecuteDataset("get_employewise_resp", par);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet getworksite(String SearchKey)
        {
            try
            {
                SqlParameter[] par = new SqlParameter[1];
                par[0] = new SqlParameter("@Search", SearchKey);
                return SQLDBUtil.ExecuteDataset("HR_GetWorkSite_googlesearch_By_Emp", par);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet get_employewise_desgi(String SearchKey)
        {
            try
            {
                SqlParameter[] par = new SqlParameter[1];
                par[0] = new SqlParameter("@Search", SearchKey);
                return SQLDBUtil.ExecuteDataset("get_employewise_desgi", par);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet get_employewise_spec(String SearchKey)
        {
            try
            {
                SqlParameter[] par = new SqlParameter[1];
                par[0] = new SqlParameter("@Search", SearchKey);
                return SQLDBUtil.ExecuteDataset("get_employewise_spec", par);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet GetWorkSite_By_EmpList()
        {
            try
            {
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetWorkSite_By_EmpList");
                return ds;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static DataSet GetWorkSite_Transfer()
        {
            try
            {
                DataSet ds = SQLDBUtil.ExecuteDataset("GetWorkSite_Transfer");
                return ds;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static DataSet GetWorkSite_By_Data()
        {
            try
            {
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetWorkSite_By_Data");
                return ds;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static DataSet GetWorkSite_By_EmpOrder()
        {
            try
            {
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetWorkSite_By_EmpOrder");
                return ds;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static DataSet GetWorkSite_By_MessType()
        {
            try
            {
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetWorkSite_By_MessType");
                return ds;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static DataSet GetWorkSite_By_NMR()
        {
            try
            {
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetWorkSite_By_NMR");
                return ds;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static DataSet HMS_GetCountryByStateID(int stateid)
        {
            try
            {
                DataSet ds = SQLDBUtil.ExecuteDataset("HMS_GetCountryByStateID", new SqlParameter[] { new SqlParameter("@stateid", stateid) });
                return ds;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static DataSet GetWorksitesByCompanyID(int? CompanyID, Char Status)
        {
            SqlParameter[] parm = new SqlParameter[2];
            parm[0] = new SqlParameter("@CompanyID", CompanyID);
            parm[1] = new SqlParameter("@Status", Status);
            return SQLDBUtil.ExecuteDataset("HMS_GetWorksitesByCompanyID", parm);
        }
        public static DataSet GetEmpMonthlyAttByEmpID(int EmpID, int Month, int Year)
        {
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[3];
                sqlParams[0] = new SqlParameter("@EmpID", EmpID);
                sqlParams[1] = new SqlParameter("@Month", Month);
                sqlParams[2] = new SqlParameter("@year", Year);
                DataSet ds = SQLDBUtil.ExecuteDataset("HMS_GET_AttendanceMonthwiseByEmpID", sqlParams);
                return ds;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public int InsUpdLeaveAppLevels(int EmpID, int RecEmpID, int Level1, int Level2)
        {
            try
            {
                int result;
                SqlParameter[] sqlParams = new SqlParameter[5];
                sqlParams[0] = new SqlParameter("@EmpID", EmpID);
                sqlParams[1] = new SqlParameter("@RecEmpID", RecEmpID);
                sqlParams[2] = new SqlParameter("@Level1", Level1);
                sqlParams[3] = new SqlParameter("@Level2", Level2);
                sqlParams[4] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
                sqlParams[4].Direction = ParameterDirection.ReturnValue;
                SQLDBUtil.ExecuteNonQuery("HR_LeaveAprovalLevels", sqlParams);
                result = Convert.ToInt16(sqlParams[4].Value);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet GetWorkSiteByEmpID(int? EmpID, int CompnanyID, int? Role)
        {
            try
            {
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetWorksiteByEmpID", new SqlParameter[] { new SqlParameter("@EmpID", EmpID), new SqlParameter("@CompanyID", CompnanyID), new SqlParameter("@Role", Role) });
                return ds;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static DataSet Googlesearch_GetWorkSiteByEmpID(string SearchKey, int CompnanyID, int? EmpID, int? Role)
        {
            try
            {
                SqlParameter[] par = new SqlParameter[4];
                par[0] = new SqlParameter("@search", SearchKey);
                par[1] = new SqlParameter("@CompanyID", CompnanyID);
                par[2] = new SqlParameter("@EmpID", EmpID);
                par[3] = new SqlParameter("@Role", Role);
                return SQLDBUtil.ExecuteDataset("HR_GoogleSearcGetWorksiteByEmpID", par);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public DataSet GetWorkSite_By_Employees(int CompnanyID, int? Role)
        {
            try
            {
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetWorksite_By_Employees", new SqlParameter[] { new SqlParameter("@CompanyID", CompnanyID), new SqlParameter("@Role", Role) });
                return ds;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static DataSet GetWorkSite_GoogleSech_By_Employees(string SearchKey, int CompnanyID, int? Role)
        {
            try
            {
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_GoogleSearch_GetWorksite_By_Employees",
                 new SqlParameter[] { new SqlParameter("@search", SearchKey), new SqlParameter("@CompanyID", CompnanyID), new SqlParameter("@Role", Role) });
                return ds;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public DataSet GetLeaveApproveEmps(int EmpID)
        {
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[1];
                sqlParams[0] = new SqlParameter("@EmpID", EmpID);
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetLeaveApprovedEmps", sqlParams);
                return ds;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public DataSet GetEmpByWSAndDept(int? WS, int? Dept)
        {
            try
            {
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetEmployeesBySiteAndDept", new SqlParameter[] { new SqlParameter("@WorkSite", WS), new SqlParameter("@Dept", Dept) });
                return ds;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static DataSet GetSearchEmpName_NMRName(string SearchKey)
        {
            try
            {
                SqlParameter[] par = new SqlParameter[1];
                par[0] = new SqlParameter("@search", SearchKey);
                return SQLDBUtil.ExecuteDataset("HMS_Name_basedon_NMRName__Googlesearch", par);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet HR_GetWorkSite_googlesearch_By_EmpList(string SearchKey, int WSID)
        {
            try
            {
                SqlParameter[] par = new SqlParameter[2];
                par[0] = new SqlParameter("@search", SearchKey);
                par[1] = new SqlParameter("@WSID", WSID);
                return SQLDBUtil.ExecuteDataset("HR_GetWorkSite_googlesearch_By_EmpList", par);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet HR_GetWorkSite_By_LTAConfigList_googlesearch(string SearchKey, int WSID)
        {
            try
            {
                SqlParameter[] par = new SqlParameter[2];
                par[0] = new SqlParameter("@search", SearchKey);
                par[1] = new SqlParameter("@WSID", WSID);
                return SQLDBUtil.ExecuteDataset("HR_GetWorkSite_By_LTAConfigList_googlesearch", par);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static void UpdateAirlineStatus(int? ID, bool status)
        {
            SqlParameter[] parm = new SqlParameter[2];
            parm[0] = new SqlParameter("@ID", ID);
            parm[1] = new SqlParameter("@active", status);
            SQLDBUtil.ExecuteNonQuery("HR_Upd_AirlineStatus", parm);
        }
        public static DataSet GetAirLiens_Search(String SearchKey)
        {
            SqlParameter[] parm = new SqlParameter[1];
            parm[0] = new SqlParameter("@search ", SearchKey);
            DataSet ds = SQLDBUtil.ExecuteDataset("GetAirLiens_Search", parm);
            return ds;
        }
        public static DataSet GetRelationtype_Search(String SearchKey)
        {
            SqlParameter[] parm = new SqlParameter[1];
            parm[0] = new SqlParameter("@search ", SearchKey);
            DataSet ds = SQLDBUtil.ExecuteDataset("GetRelationType_Search", parm);
            return ds;
        }
        public static DataSet GetPassengerType_Search(String SearchKey)
        {
            SqlParameter[] parm = new SqlParameter[1];
            parm[0] = new SqlParameter("@search ", SearchKey);
            DataSet ds = SQLDBUtil.ExecuteDataset("GetPassengerType_Search", parm);
            return ds;
        }
        public static DataSet GetBookingClass_Search(String SearchKey)
        {
            SqlParameter[] parm = new SqlParameter[1];
            parm[0] = new SqlParameter("@search ", SearchKey);
            DataSet ds = SQLDBUtil.ExecuteDataset("GetBookingClass_Search", parm);
            return ds;
        }
        public static DataSet Get_City_Search(String SearchKey)
        {
            SqlParameter[] parm = new SqlParameter[1];
            parm[0] = new SqlParameter("@search ", SearchKey);
            DataSet ds = SQLDBUtil.ExecuteDataset("Get_City_Search", parm);
            return ds;
        }
        public static void UpdateDepartmentStatus(int? DeptID, bool Status)
        {
            SqlParameter[] parm = new SqlParameter[2];
            parm[0] = new SqlParameter("@DeptID", DeptID);
            parm[1] = new SqlParameter("@Status", Status);
            SQLDBUtil.ExecuteNonQuery("HR_Upd_DepartmentStatus", parm);
        }
        public static void UpdateBookIngClassStatus(int? BookIngClassID, int Status)
        {
            SqlParameter[] parm = new SqlParameter[2];
            parm[0] = new SqlParameter("@BookIngClassID", BookIngClassID);
            parm[1] = new SqlParameter("@Status", Status);
            SQLDBUtil.ExecuteNonQuery("HR_Upd_BookingClassStatus", parm);
        }
        public static void UpdatePassengerTypeStatus(int? PassengerTypeID, int Status)
        {
            SqlParameter[] parm = new SqlParameter[2];
            parm[0] = new SqlParameter("@ID", PassengerTypeID);
            parm[1] = new SqlParameter("@Status", Status);
            SQLDBUtil.ExecuteNonQuery("[HR_Upd_PassengerTypeStatus]", parm);
        }
        public static void UpdateRelationTypeStatus(int? PassengerTypeID, int Status)
        {
            SqlParameter[] parm = new SqlParameter[2];
            parm[0] = new SqlParameter("@ID", PassengerTypeID);
            parm[1] = new SqlParameter("@Status", Status);
            SQLDBUtil.ExecuteNonQuery("HR_Upd_RelationStatus", parm);
        }
        public static void UpdateEmpNatureStatus(int? empnatureID, bool Status)
        {
            SqlParameter[] parm = new SqlParameter[2];
            parm[0] = new SqlParameter("@empnatureID", empnatureID);
            parm[1] = new SqlParameter("@Status", Status);
            SQLDBUtil.ExecuteNonQuery("HR_Upd_EmpNatureStatus", parm);
        }
        #region EmpSim
        public static void InsUpdateEmpSIMS(int EmpID, Int64 MobileNo, int UserID, decimal AmountLimit)
        {
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[4];
                sqlParams[0] = new SqlParameter("@EmpID", EmpID);
                sqlParams[1] = new SqlParameter("@MobileNo", MobileNo);
                sqlParams[2] = new SqlParameter("@AmountLimit", AmountLimit);
                sqlParams[3] = new SqlParameter("@UserID", UserID);
                SQLDBUtil.ExecuteNonQuery("HR_InsUpd_SIMS", sqlParams);
            }
            catch (Exception e)
            {
            }
        }
        public static DataSet GetEmpSimsList(int EmpID)
        {
            DataSet ds = SQLDBUtil.ExecuteDataset("HR_GET_SIMSList", new SqlParameter[] { new SqlParameter("@EmpID", EmpID) });
            return ds;
        }
        public static DataSet GetEmpSimsDetails(int CSID)
        {
            try
            {
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_GET_SIMSDetails", new SqlParameter[] { new SqlParameter("@CSID", CSID) });
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region EmpSimBills
        public static DataSet GetSimBillsList(int EmpID, int Month, int Year)
        {
            DataSet ds = SQLDBUtil.ExecuteDataset("HR_Get_SIMBillsList", new SqlParameter[] { new SqlParameter("@EmpID", EmpID), new SqlParameter("@Month", Month), new SqlParameter("@Year", Year) });
            return ds;
        }
        public static DataSet GetSimBillsDetails(int BillID)
        {
            try
            {
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_Get_SIMBillsDetails", new SqlParameter[] { new SqlParameter("@BillID", BillID) });
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static void InsUpdateSimBills(int CSID, int EmpID, int Month, int Year, decimal BillAmount, int Userid)
        {
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[6];
                sqlParams[0] = new SqlParameter("@CSID", CSID);
                sqlParams[1] = new SqlParameter("@EmpID", EmpID);
                sqlParams[2] = new SqlParameter("@Month", Month);
                sqlParams[3] = new SqlParameter("@Year", Year);
                sqlParams[4] = new SqlParameter("@BillAmount", BillAmount);
                sqlParams[5] = new SqlParameter("@Userid", Userid);
                SQLDBUtil.ExecuteNonQuery("HR_InsUpd_SIMBills", sqlParams);
            }
            catch (Exception e)
            {
            }
        }
        public static void HR_InsUpMobileBills(int SID, int Month, int Year, double BillAmount, int UserID, double? ExtAmt)
        {
            SqlParameter[] parm = new SqlParameter[6];
            parm[0] = new SqlParameter("@SID", SID);
            parm[1] = new SqlParameter("@Month", Month);
            parm[2] = new SqlParameter("@Year", Year);
            parm[3] = new SqlParameter("@BillAmount", BillAmount);
            parm[4] = new SqlParameter("@UserID", UserID);
            parm[5] = new SqlParameter("@ExtAmt", ExtAmt);
            SQLDBUtil.ExecuteNonQuery("HR_InsUpMobileBills", parm);
        }
        #endregion
        public static DataSet GetDirectorsList(int CompnayID)
        {
            try
            {
                SqlParameter[] parm = new SqlParameter[1];
                parm[0] = new SqlParameter("@CompanyID", CompnayID);
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetDirectorsList", parm);
                return ds;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public DataSet HR_EmpSalriesListByEmpID(int EmpID, int Month, int Year, HRCommon objHrCommon)
        {
            try
            {
                SqlParameter[] objParam = new SqlParameter[5];
                objParam[0] = new SqlParameter("@EmpID", EmpID);
                objParam[1] = new SqlParameter("@Month", Month);
                objParam[2] = new SqlParameter("@Year", Year);
                objParam[3] = new SqlParameter("@SiteID", objHrCommon.SiteID);
                objParam[4] = new SqlParameter("@DeptID", objHrCommon.DeptID);
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_EmpSalriesListByEmpID_RK", objParam);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet GetEmployeesForSalaries(HRCommon objHrCommon)
        {
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[9];
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
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_EmpSalriesList", sqlParams);
                objHrCommon.NoofRecords = (int)sqlParams[3].Value;
                objHrCommon.TotalPages = (int)sqlParams[2].Value;
                return ds;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public DataSet GetEmployeesForSalariesWithHisID(HRCommon objHrCommon, int OrderId, int Direction, string Name, int? EmpNatID, int CompanyID)
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
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_EmpSalriesListWithHisID", sqlParams);
                objHrCommon.NoofRecords = (int)sqlParams[3].Value;
                objHrCommon.TotalPages = (int)sqlParams[2].Value;
                return ds;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static void HMS_EMPUpdateSalarySpecial(int PaySlipID, decimal SpecialAmt, int RcmdBy)
        {
            try
            {
                SqlParameter[] parm = new SqlParameter[3];
                parm[0] = new SqlParameter("@PaySlipID", PaySlipID);
                parm[1] = new SqlParameter("@SpecialAmt", SpecialAmt);
                parm[2] = new SqlParameter("@RcmdBy", RcmdBy);
                SQLDBUtil.ExecuteNonQuery("HMS_EMPUpdateSalarySpecial", parm);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static void UpdPaisStatus(int EmpID, int Month, int Year)
        {
            try
            {
                SqlParameter[] parm = new SqlParameter[3];
                parm[0] = new SqlParameter("@EmpID", EmpID);
                parm[1] = new SqlParameter("@Month", Month);
                parm[2] = new SqlParameter("@Year", Year);
                SQLDBUtil.ExecuteNonQuery("HR_UpdateSalPaymentStatus", parm);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public SqlDataReader GetEmployeesForSalaries(HRCommon objHrCommon, int PageSize)
        {
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[9];
                sqlParams[0] = new SqlParameter("@CurrentPage", objHrCommon.CurrentPage);
                sqlParams[1] = new SqlParameter("@PageSize", PageSize);
                sqlParams[2] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
                sqlParams[2].Direction = ParameterDirection.ReturnValue;
                sqlParams[3] = new SqlParameter("@NoofRecords", System.Data.SqlDbType.Int);
                sqlParams[3].Direction = ParameterDirection.Output;
                sqlParams[4] = new SqlParameter("@SiteID", objHrCommon.SiteID);
                sqlParams[5] = new SqlParameter("@DeptID", objHrCommon.DeptID);
                sqlParams[6] = new SqlParameter("@EmpStatus", objHrCommon.CurrentStatus);
                sqlParams[7] = new SqlParameter("@Month", objHrCommon.Month);
                sqlParams[8] = new SqlParameter("@Year", objHrCommon.Year);
                SqlDataReader dr;
                dr = SQLDBUtil.ExecuteDataReader("HR_EmpSalriesList", sqlParams);
                return dr;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public SqlDataReader GetEmployeesForSalariesExportToExcel(int? SiteID, int? Dept, int? Month, int Year, char Status, int? EmpNat)
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
                SqlDataReader dr;
                dr = SQLDBUtil.ExecuteDataReader("HR_EmpSalriesListExpotyExcel", sqlParams);
                return dr;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public SqlDataReader GetEmployeesDtlRpt(HRCommon objHrCommon, int? EmpNat)
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
                SqlDataReader dr;
                dr = SQLDBUtil.ExecuteDataReader("HR_EmpSalriesDetailRept", sqlParams);
                return dr;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static DataSet HR_GetEMPIDsByWSID(int SiteID, int DeptID, int EmpNatureID, string EmpName)
        {
            SqlParameter[] parm = new SqlParameter[4];
            parm[0] = new SqlParameter("@SiteID", SqlDbType.Int);
            if (SiteID > 0)
                parm[0].Value = SiteID;
            parm[1] = new SqlParameter("@DeptID", SqlDbType.Int);
            if (DeptID > 0)
                parm[1].Value = DeptID;
            parm[2] = new SqlParameter("@EmpNatureID", SqlDbType.Int);
            if (EmpNatureID > 0)
                parm[2].Value = EmpNatureID;
            parm[3] = new SqlParameter("@EmpName", EmpName);
            DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetEMPIDsByWSID", parm);
            return ds;
        }
        public static void HR_SavePaySLIP(int EmpId, string Date, int IsAccount, string Formtype)
        {
            try
            {
                SqlParameter[] parm = new SqlParameter[4];
                parm[0] = new SqlParameter("@EmpId", EmpId);
                parm[1] = new SqlParameter("@Date", Date);
                parm[2] = new SqlParameter("@IsAccount", IsAccount);
                if (Formtype != string.Empty)
                    parm[3] = new SqlParameter("@Formtype", Formtype);
                else
                    parm[3] = new SqlParameter("@Formtype", DBNull.Value);
                SQLDBUtil.ExecuteNonQuery("HR_SavePaySLIP", parm);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static void HR_SavePaySLIP_CTH(int EmpId, DateTime Date)
        {
            try
            {
                SqlParameter[] parm = new SqlParameter[2];
                parm[0] = new SqlParameter("@EmpId", EmpId);
                parm[1] = new SqlParameter("@Date", Date);
                SQLDBUtil.ExecuteNonQuery("HR_SavePaySLIP_CTH", parm);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static DataSet HR_AbsentPenalitiesByPaging_vacationsettlement(HRCommon objHrCommon, int Wsid, int desdid, int deptid, int month, int year, int empid)
        {
            SqlParameter[] sqlParams = new SqlParameter[10];
            sqlParams[0] = new SqlParameter("@worksite", Wsid);
            sqlParams[1] = new SqlParameter("@designation", desdid);
            sqlParams[2] = new SqlParameter("@deptno", deptid);
            sqlParams[3] = new SqlParameter("@month", month);
            sqlParams[4] = new SqlParameter("@year", year);
            sqlParams[5] = new SqlParameter("@EmpId", empid);
            sqlParams[6] = new SqlParameter("@CurrentPage", 1);
            sqlParams[7] = new SqlParameter("@PageSize", 30);
            sqlParams[8] = new SqlParameter("@NoofRecords", 3);
            sqlParams[8].Direction = ParameterDirection.Output;
            sqlParams[9] = new SqlParameter();
            sqlParams[9].Direction = ParameterDirection.ReturnValue;
            DataSet ds = SQLDBUtil.ExecuteDataset("USP_HMS_AbsentPenalities", sqlParams);
            objHrCommon.NoofRecords = (int)sqlParams[8].Value;
            objHrCommon.TotalPages = (int)sqlParams[9].Value;
            return ds;
        }
        public static DataSet SalPaymentStatus(int EmpID, int Month, int Year)
        {
            SqlParameter[] parm = new SqlParameter[3];
            parm[0] = new SqlParameter("@EmpID", EmpID);
            parm[1] = new SqlParameter("@Month", Month);
            parm[2] = new SqlParameter("@Year", Year);
            DataSet ds = SQLDBUtil.ExecuteDataset("HR_SalPaymentStatus", parm);
            return ds;
        }
        public static int InsUpdateEmpSal(int EmpSalID, int EmpId, int Salary, int UserID, DateTime FromDate)
        {
            try
            {
                int retval;
                SqlParameter[] sqlParams = new SqlParameter[6];
                sqlParams[0] = new SqlParameter("@EmpSalID", EmpSalID);
                sqlParams[1] = new SqlParameter("@EmpId", EmpId);
                sqlParams[2] = new SqlParameter("@Salary", Salary);
                sqlParams[3] = new SqlParameter("@FromDate", FromDate);
                sqlParams[4] = new SqlParameter("@UserID", UserID);
                sqlParams[5] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
                sqlParams[5].Direction = ParameterDirection.ReturnValue;
                SQLDBUtil.ExecuteDataset("HR_InsUpd_EmphikeSal", sqlParams);
                retval = (int)sqlParams[5].Value;
                return retval;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static DataSet GetMonthlyPresentReport(int Month, int Year, int SiteID)
        {
            try
            {
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetMonthlyPresentReport", new SqlParameter[] { new SqlParameter("@Month", Month), new SqlParameter("@Year", Year), new SqlParameter("@SiteId", SiteID) });
                return ds;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public DataSet GetWorkSiteOrder(int WSId, char Staus)
        {
            try
            {
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetWorkSiteOrder", new SqlParameter[] { new SqlParameter("@WSID", WSId), new SqlParameter("@WSStatus", Staus) });
                return ds;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public DataSet GetWorkSitesBypaging(HRCommon objHrCommon)
        {
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[6];
                sqlParams[0] = new SqlParameter("@CurrentPage", objHrCommon.CurrentPage);
                sqlParams[1] = new SqlParameter("@PageSize", objHrCommon.PageSize);
                sqlParams[2] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
                sqlParams[2].Direction = ParameterDirection.ReturnValue;
                sqlParams[3] = new SqlParameter("@NoofRecords", System.Data.SqlDbType.Int);
                sqlParams[3].Direction = ParameterDirection.Output;
                sqlParams[4] = new SqlParameter("@Status", objHrCommon.CurrentStatus);
                sqlParams[5] = new SqlParameter("@SiteName", objHrCommon.SiteName);
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetWorkSitesByPage", sqlParams);
                objHrCommon.NoofRecords = (int)sqlParams[3].Value;
                objHrCommon.TotalPages = (int)sqlParams[2].Value;
                return ds;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public DataSet GetValidUserDetails(HRCommon objHrCommon)
        {
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[2];
                sqlParams[0] = new SqlParameter("@UserName", objHrCommon.Username);
                sqlParams[1] = new SqlParameter("@password", objHrCommon.NewPassWord);
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_ValidUser", sqlParams);
                return ds;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public DataSet Validate_Login(string Login_Name, string Password, string MachineName, int ModuleId)
        {
            try
            {
                SqlParameter[] objParam = new SqlParameter[4];
                objParam[0] = new SqlParameter("@Login_Name", Login_Name);
                objParam[1] = new SqlParameter("@Password", Password);
                objParam[2] = new SqlParameter("@MacName", MachineName);
                objParam[3] = new SqlParameter("@ModuleId", ModuleId);
                DataSet ds = SQLDBUtil.ExecuteDataset("CP_HRLogin", objParam);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet CP_Login(string Login_Name, string Password, string MachineName, int ModuleId, string HostIP, string CompanyName)
        {
            try
            {
                SqlParameter[] objParam = new SqlParameter[6];
                objParam[0] = new SqlParameter("@Login_Name", Login_Name);
                objParam[1] = new SqlParameter("@Password", Password);
                objParam[2] = new SqlParameter("@MacName", MachineName);
                objParam[3] = new SqlParameter("@ModuleId", ModuleId);
                objParam[4] = new SqlParameter("@HostIP", HostIP);
                objParam[5] = new SqlParameter("@CompanyName", CompanyName);
                DataSet ds = SQLDBUtil.ExecuteDataset("CP_Login", objParam);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static void UpdateEmpDocIDStatus(int EmpDocID)
        {
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[1];
                sqlParams[0] = new SqlParameter("@EmpDocID", EmpDocID);
                SQLDBUtil.ExecuteNonQuery("HR_UpdEmpDocIDStatus", sqlParams);
            }
            catch (Exception e)
            {
            }
        }
        public static DataSet GetEmpSalList(int EmpID)
        {
            DataSet ds = SQLDBUtil.ExecuteDataset("HR_GET_EmphikeSalList", new SqlParameter[] { new SqlParameter("@EmpId", EmpID) });
            return ds;
        }
        public static DataSet GetEmpDetailsForDocsDetails(int EmpID)
        {
            DataSet ds = SQLDBUtil.ExecuteDataset("GetEmpDetailsForDocsDetails", new SqlParameter[] { new SqlParameter("@EmpId", EmpID) });
            return ds;
        }
        public DataSet PageHelp(string URL, int ModuleId)
        {
            try
            {
                SqlParameter[] objParam = new SqlParameter[2];
                objParam[0] = new SqlParameter("@URL", URL);
                objParam[1] = new SqlParameter("@ModuleId", ModuleId);
                DataSet ds = SQLDBUtil.ExecuteDataset("CP_Get_PageHelp", objParam);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet GetEmpDetailsBYEmpID(int EmpID)
        {
            try
            {
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetEmpDetailsBYEmpID", new SqlParameter[] { new SqlParameter("@EmpID", EmpID) });
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet GetAppTerms()
        {
            try
            {
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetAppTerms");
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet getPasswords()
        {
            try
            {
                DataSet ds = SQLDBUtil.ExecuteDataset("GET_Paswords");
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet GetEmployeeDetails(int EmpID)
        {
            return SQLDBUtil.ExecuteDataset("HR_Get_EmployeeDetails", new SqlParameter[] { new SqlParameter("@EmpID", EmpID) });
        }
        public DataSet GetEmployees(int deptid)
        {
            try
            {
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_getEmployees", new SqlParameter[] { new SqlParameter("@DeptID", deptid) });
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet GetHeads(int deptid, int CompanyID)
        {
            try
            {
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetHeads", new SqlParameter[] { new SqlParameter("@DeptID", deptid),
                new SqlParameter("@CompanyID", CompanyID)});
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet GetPMs()
        {
            try
            {
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_ProjectMangers");
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet GetPMsForVacant()
        {
            try
            {
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_ProjectMangersVacant");
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet GetEmpDetailsForOC()
        {
            try
            {
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_EmpDetailsForOC1");
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet GetAttendanceForOC(int CompanyID)
        {
            try
            {
                SqlParameter[] parm = new SqlParameter[1];
                parm[0] = new SqlParameter("@CompanyID", CompanyID);
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetAttendanceforOrgChart", parm);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet T_HR_GetNMRAttendanceCount()
        {
            try
            {
                DataSet ds = SQLDBUtil.ExecuteDataset("T_HR_GetNMRAttendanceCount");
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet GetNMRAttendanceForOC()
        {
            try
            {
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetNMRAttendanceforOrgChart");
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet GetProjectManagersForOC(int CompanyID)
        {
            try
            {
                SqlParameter[] parm = new SqlParameter[1];
                parm[0] = new SqlParameter("@CompanyID", CompanyID);
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_PrjManagersForOC", parm);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Hashtable GetEmpAttendenceDetails(int PrjID)
        {
            Hashtable htAttDetails = new Hashtable();
            try
            {
                SqlParameter[] spParms = new SqlParameter[5];
                spParms[0] = new SqlParameter("@Site_ID", PrjID);
                spParms[1] = new SqlParameter("@Present", SqlDbType.Int);
                spParms[1].Direction = ParameterDirection.Output;
                spParms[2] = new SqlParameter("@Total", SqlDbType.Int);
                spParms[2].Direction = ParameterDirection.Output;
                spParms[3] = new SqlParameter("@TotPresent", SqlDbType.Int);
                spParms[3].Direction = ParameterDirection.Output;
                spParms[4] = new SqlParameter("@NoOfEmp", SqlDbType.Int);
                spParms[4].Direction = ParameterDirection.Output;
                SQLDBUtil.ExecuteDataset("GetEmpAttDetailsBySite", spParms);
                htAttDetails.Add("Present", spParms[1].Value);
                htAttDetails.Add("Total", spParms[2].Value);
                htAttDetails.Add("TotalPresent", spParms[3].Value);
                htAttDetails.Add("TotalEmp", spParms[4].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return htAttDetails;
        }
        public DataSet GetPrjManagersForOC(int PrjID)
        {
            try
            {
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_PrjManagersForOC1", new SqlParameter[] { new SqlParameter("@Prjid", PrjID) });
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet GetNMS()
        {
            try
            {
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_GET_NMR", new SqlParameter[] { new SqlParameter("@Status", true) });
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet GetDeptHeadscount(int PrjID)
        {
            try
            {
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_DeptHeadsFor0Ccount", new SqlParameter[] { new SqlParameter("@Prjid", PrjID) });
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet GetMachinesForMOC(int PrjID)
        {
            try
            {
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetMachinesForMOC", new SqlParameter[] { new SqlParameter("@Prjid", PrjID) });
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet GetDepartments(int DeptID)
        {
            try
            {
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetDepartments", new SqlParameter[] { new SqlParameter("@DeptID", DeptID) });
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet GoogleSerch_TaskAssignment_GetDaprtment(String SearchKey, int Deptid)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[2];
                param[0] = new SqlParameter("@Search", SearchKey);
                param[1] = new SqlParameter("@DeptID", Deptid);
                return SQLDBUtil.ExecuteDataset("HR_GoogleSearc_GetDepartments", param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet GetDepartmentsByPreOrder(int DeptID)
        {
            try
            {
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetDepartmentsByPreorder", new SqlParameter[] { new SqlParameter("@DeptID", DeptID) });
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet GetAppDetails(int EmpID)
        {
            try
            {
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetAppDetails", new SqlParameter[] { new SqlParameter("@EmpID", EmpID) });
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet GetAppOfferDetails(int AppID)
        {
            try
            {
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetAppOfferDetails", new SqlParameter[] { new SqlParameter("@AppID", AppID) });
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet GetMasterDocs(int DocID, int ModuleID)
        {
            try
            {
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetMasterDocs", new SqlParameter[] { new SqlParameter("@DocID", DocID), new SqlParameter("@ModuleID", ModuleID) });
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet GetCustodianDocsList()
        {
            try
            {
                DataSet ds = SQLDBUtil.ExecuteDataset("HMS_CustodianDocsList");
                return ds;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static int InsUpdDocs(int DocId, string DocName, string DocProcedure, int UserID, string Ext)
        {
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[6];
                sqlParams[0] = new SqlParameter("@DocID", DocId);
                sqlParams[1] = new SqlParameter("@DocName", DocName);
                sqlParams[2] = new SqlParameter("@EmpID", UserID);
                sqlParams[3] = new SqlParameter("@DocProcedure", DocProcedure);
                sqlParams[4] = new SqlParameter("ReturnValue", SqlDbType.Int);
                sqlParams[4].Direction = ParameterDirection.ReturnValue;
                sqlParams[5] = new SqlParameter("@Ext", Ext);
                SQLDBUtil.ExecuteNonQuery("HMS_InsUpd_CustodianDocs", sqlParams);
                int I = Convert.ToInt32(sqlParams[4].Value);
                return I;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static DataSet GetCustodianDocsDetails(int DocID)
        {
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[1];
                sqlParams[0] = new SqlParameter("@DocID", DocID);
                DataSet ds = SQLDBUtil.ExecuteDataset("HMS_GetCustodianDocsDetails", sqlParams);
                return ds;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public DataSet GetEmpDoc(int EmpID, int DocID, int EmpDocID)
        {
            try
            {
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetEmpDoc", new SqlParameter[] { new SqlParameter("@EmpID", EmpID), new SqlParameter("@DocId", DocID), new SqlParameter("@EmpDocID", EmpDocID) });
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet GetEmpDocs(int EmpID)
        {
            try
            {
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetEmpDocs", new SqlParameter[] { new SqlParameter("@EmpID", EmpID) });
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet GetEmpDocsByPaging(HRCommon objHrCommon, int EmpID, string DocName)
        {
            SqlParameter[] sqlParams = new SqlParameter[6];
            sqlParams[0] = new SqlParameter("@CurrentPage", objHrCommon.CurrentPage);
            sqlParams[1] = new SqlParameter("@PageSize", objHrCommon.PageSize);
            sqlParams[2] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
            sqlParams[2].Direction = ParameterDirection.ReturnValue;
            sqlParams[3] = new SqlParameter("@NoofRecords", System.Data.SqlDbType.Int);
            sqlParams[3].Direction = ParameterDirection.Output;
            sqlParams[4] = new SqlParameter("@EmpID", EmpID);
            sqlParams[5] = new SqlParameter("@DocName", DocName);
            DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetEmpDocsByPaging", sqlParams);
            objHrCommon.NoofRecords = (int)sqlParams[3].Value;
            objHrCommon.TotalPages = (int)sqlParams[2].Value;
            return ds;
        }
        public DataSet GetMgnr(int SiteID, int DeptID, int Type, int EmpID, int CompanyID)
        {
            try
            {
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetMgnrs", new SqlParameter[] { new SqlParameter("@SiteID", SiteID), 
                    new SqlParameter("@DeptID", DeptID), 
                    new SqlParameter("@Type", Type), 
                    new SqlParameter("@EmpID", EmpID),
                new SqlParameter("@CompanyID", CompanyID)});
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet GetWSManger(int WSID)
        {
            try
            {
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_getWSManger", new SqlParameter[] { new SqlParameter("@WSID", WSID) });
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet GetDeptHeadForReporting(int WSID, int DeptID, int? EmpID)
        {
            try
            {
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetDeptHeadsForReporting", new SqlParameter[] { new SqlParameter("@WSID", WSID), new SqlParameter("@DeptId", DeptID), new SqlParameter("@EmpId", EmpID) });
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet GetTopMgmt()
        {
            try
            {
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetTopMgmt");
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet GetWSMangervacant(int WSID)
        {
            try
            {
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_getWSMangervacant", new SqlParameter[] { new SqlParameter("@WSID", WSID) });
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet GetEmpDetailsByPreOrder(int DeptID, int WSID)
        {
            try
            {
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetEmpDetailsByPreOrder", new SqlParameter[] { new SqlParameter("@DeptID", DeptID), new SqlParameter("@WSID", WSID) });
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet GetEmpDetails(int DeptID, int WSID)
        {
            try
            {
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetEmpDetails", new SqlParameter[] { new SqlParameter("@DeptID", DeptID), new SqlParameter("@WSID", WSID) });
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet GetEmpDetails(int DeptID, int WSID, int EmpID, string EmpName)
        {
            try
            {
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetEmpDetails", new SqlParameter[] { new SqlParameter("@DeptID", DeptID), new SqlParameter("@WSID", WSID), new SqlParameter("@EmpID", EmpID), new SqlParameter("@Name", EmpName) });
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet Get_OMS_ManPower_Requisition()
        {
            DataSet ds = SQLDBUtil.ExecuteDataset("OMS_GET_MANPOWER_REQUISITION");
            return ds;
        }
        public static DataSet GetEmpDetailsByPaging(HRCommon objHrCommon, int DeptID, int WSID, int EmpID, string EmpName, int CompanyID, int specid)
        {
            SqlParameter[] sqlParams = new SqlParameter[10];
            sqlParams[0] = new SqlParameter("@CurrentPage", objHrCommon.CurrentPage);
            sqlParams[1] = new SqlParameter("@PageSize", objHrCommon.PageSize);
            sqlParams[2] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
            sqlParams[2].Direction = ParameterDirection.ReturnValue;
            sqlParams[3] = new SqlParameter("@NoofRecords", System.Data.SqlDbType.Int);
            sqlParams[3].Direction = ParameterDirection.Output;
            sqlParams[4] = new SqlParameter("@DeptID", DeptID);
            sqlParams[5] = new SqlParameter("@WSID", WSID);
            sqlParams[6] = new SqlParameter("@EmpID", EmpID);
            sqlParams[7] = new SqlParameter("@Name", EmpName);
            sqlParams[8] = new SqlParameter("@CompanyID", CompanyID);
            sqlParams[9] = new SqlParameter("@specid", specid);
            DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetEmpDetailsByPaging", sqlParams);
            objHrCommon.NoofRecords = (int)sqlParams[3].Value;
            objHrCommon.TotalPages = (int)sqlParams[2].Value;
            return ds;
        }
        public static DataSet OMS_GET_MANPOWER_REQUISITION(HRCommon objHrCommon, int CompanyID, int Key
            , int ProjectID, int DesignID, int CatID, DateTime FromDt, DateTime ToDt)
        {
            SqlParameter[] sqlParams = new SqlParameter[11];
            sqlParams[0] = new SqlParameter("@CurrentPage", objHrCommon.CurrentPage);
            sqlParams[1] = new SqlParameter("@PageSize", objHrCommon.PageSize);
            sqlParams[2] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
            sqlParams[2].Direction = ParameterDirection.ReturnValue;
            sqlParams[3] = new SqlParameter("@NoofRecords", System.Data.SqlDbType.Int);
            sqlParams[3].Direction = ParameterDirection.Output;
            sqlParams[4] = new SqlParameter("@CompanyID", CompanyID);
            sqlParams[5] = new SqlParameter("@key", Key);
            if (ProjectID != 0)
                sqlParams[6] = new SqlParameter("@ProjectID", ProjectID);
            else
                sqlParams[6] = new SqlParameter("@ProjectID", System.Data.SqlDbType.Int);
            if (DesignID != 0)
                sqlParams[7] = new SqlParameter("@DesignID", DesignID);
            else
                sqlParams[7] = new SqlParameter("@DesignID", System.Data.SqlDbType.Int);
            if (CatID != 0)
                sqlParams[8] = new SqlParameter("@CatID", CatID);
            else
                sqlParams[8] = new SqlParameter("@CatID", System.Data.SqlDbType.Int);
            sqlParams[9] = new SqlParameter("@FromDt", FromDt);
            sqlParams[10] = new SqlParameter("@ToDt", ToDt);
            DataSet ds = SQLDBUtil.ExecuteDataset("OMS_GET_MANPOWER_REQUISITION", sqlParams);
            objHrCommon.NoofRecords = (int)sqlParams[3].Value;
            objHrCommon.TotalPages = (int)sqlParams[2].Value;
            return ds;
        }
        public static DataSet HMS_GET_MANPOWER_REQUISITION(HRCommon objHrCommon, int CompanyID, int Key
            , int WorksiteID, int ProjectID, int DesignID, int CatID, DateTime FromDt, DateTime ToDt)
        {
            SqlParameter[] sqlParams = new SqlParameter[12];
            sqlParams[0] = new SqlParameter("@CurrentPage", objHrCommon.CurrentPage);
            sqlParams[1] = new SqlParameter("@PageSize", objHrCommon.PageSize);
            sqlParams[2] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
            sqlParams[2].Direction = ParameterDirection.ReturnValue;
            sqlParams[3] = new SqlParameter("@NoofRecords", System.Data.SqlDbType.Int);
            sqlParams[3].Direction = ParameterDirection.Output;
            sqlParams[4] = new SqlParameter("@CompanyID", CompanyID);
            sqlParams[5] = new SqlParameter("@key", Key);
            if (WorksiteID != 0)
                sqlParams[6] = new SqlParameter("@WorksiteID", WorksiteID);
            else
                sqlParams[6] = new SqlParameter("@WorksiteID", System.Data.SqlDbType.Int);
            if (ProjectID != 0)
                sqlParams[7] = new SqlParameter("@ProjectID", ProjectID);
            else
                sqlParams[7] = new SqlParameter("@ProjectID", System.Data.SqlDbType.Int);
            if (DesignID != 0)
                sqlParams[8] = new SqlParameter("@DesignID", DesignID);
            else
                sqlParams[8] = new SqlParameter("@DesignID", System.Data.SqlDbType.Int);
            if (CatID != 0)
                sqlParams[9] = new SqlParameter("@CatID", CatID);
            else
                sqlParams[9] = new SqlParameter("@CatID", System.Data.SqlDbType.Int);
            sqlParams[10] = new SqlParameter("@FromDt", FromDt);
            sqlParams[11] = new SqlParameter("@ToDt", ToDt);
            DataSet ds = SQLDBUtil.ExecuteDataset("HMS_GET_MANPOWER_REQUISITION", sqlParams);
            objHrCommon.NoofRecords = (int)sqlParams[3].Value;
            objHrCommon.TotalPages = (int)sqlParams[2].Value;
            return ds;
        }
        public static DataSet HMS_GET_MANPOWER_REQUISITION_1(HRCommon objHrCommon, int CompanyID, int Key
            )
        {
            SqlParameter[] sqlParams = new SqlParameter[6];
            sqlParams[0] = new SqlParameter("@CurrentPage", objHrCommon.CurrentPage);
            sqlParams[1] = new SqlParameter("@PageSize", objHrCommon.PageSize);
            sqlParams[2] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
            sqlParams[2].Direction = ParameterDirection.ReturnValue;
            sqlParams[3] = new SqlParameter("@NoofRecords", System.Data.SqlDbType.Int);
            sqlParams[3].Direction = ParameterDirection.Output;
            sqlParams[4] = new SqlParameter("@CompanyID", CompanyID);
            sqlParams[5] = new SqlParameter("@key", Key);
            DataSet ds = SQLDBUtil.ExecuteDataset("HMS_GET_MANPOWER_REQUISITION", sqlParams);
            objHrCommon.NoofRecords = (int)sqlParams[3].Value;
            objHrCommon.TotalPages = (int)sqlParams[2].Value;
            return ds;
        }
        public DataSet GetExecutivDirectors(int CompanyID)
        {
            try
            {
                SqlParameter[] parm = new SqlParameter[1];
                parm[0] = new SqlParameter("@CompanyID", CompanyID);
                DataSet ds = SQLDBUtil.ExecuteDataset("[HR_GetExecutivDirectors]", parm);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet GetDirectors(int CompanyID)
        {
            try
            {
                SqlParameter[] parm = new SqlParameter[1];
                parm[0] = new SqlParameter("@CompanyID", CompanyID);
                DataSet ds = SQLDBUtil.ExecuteDataset("[HR_GetDirectors]", parm);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet GetDutiesList(int Empid)
        {
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[1];
                sqlParams[0] = new SqlParameter("@EmpID", Empid);
                DataSet ds = SQLDBUtil.ExecuteDataset("[HR_GetDutiesList]", sqlParams);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int UpdateCMD(HRCommon obj)
        {
            try
            {
                int result;
                SqlParameter[] sqlParams = new SqlParameter[2];
                sqlParams[0] = new SqlParameter("@EmpID", obj.EmpID);
                sqlParams[1] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
                sqlParams[1].Direction = ParameterDirection.ReturnValue;
                SQLDBUtil.ExecuteNonQuery("HR_UpdateCMD", sqlParams);
                result = Convert.ToInt16(sqlParams[1].Value);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet GetShiftsList()
        {
            return SQLDBUtil.ExecuteDataset("EMS_Get_ShiftList");
        }
        public DataSet GetTodayAttendance(int DeptID, int WSID)
        {
            try
            {
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetTodayAttendance", new SqlParameter[] { new SqlParameter("@DeptID", DeptID), new SqlParameter("@WSID", WSID) });
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet GetTodayAttendanceByPaging(HRCommon objHrCommon, int? WSID, int? DeptID)
        {
            SqlParameter[] sqlParams = new SqlParameter[6];
            sqlParams[0] = new SqlParameter("@CurrentPage", objHrCommon.CurrentPage);
            sqlParams[1] = new SqlParameter("@PageSize", objHrCommon.PageSize);
            sqlParams[2] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
            sqlParams[2].Direction = ParameterDirection.ReturnValue;
            sqlParams[3] = new SqlParameter("@NoofRecords", System.Data.SqlDbType.Int);
            sqlParams[3].Direction = ParameterDirection.Output;
            sqlParams[4] = new SqlParameter("@WSID", WSID);
            sqlParams[5] = new SqlParameter("@DeptID", DeptID);
            DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetTodayAttendance_Paging", sqlParams);
            objHrCommon.NoofRecords = (int)sqlParams[3].Value;
            objHrCommon.TotalPages = (int)sqlParams[2].Value;
            return ds;
        }
        public DataSet HR_GetTodayAttendance_Paging_Empid(HRCommon objHrCommon, int? WSID, int? DeptID, int? empid, string Empname)
        {
            SqlParameter[] sqlParams = new SqlParameter[8];
            sqlParams[0] = new SqlParameter("@CurrentPage", objHrCommon.CurrentPage);
            sqlParams[1] = new SqlParameter("@PageSize", objHrCommon.PageSize);
            sqlParams[2] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
            sqlParams[2].Direction = ParameterDirection.ReturnValue;
            sqlParams[3] = new SqlParameter("@NoofRecords", System.Data.SqlDbType.Int);
            sqlParams[3].Direction = ParameterDirection.Output;
            sqlParams[4] = new SqlParameter("@WSID", WSID);
            sqlParams[5] = new SqlParameter("@DeptID", DeptID);
            sqlParams[6] = new SqlParameter("@empid", empid);
            sqlParams[7] = new SqlParameter("@EmpName", Empname);
            DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetTodayAttendance_Paging_Empid", sqlParams);
            objHrCommon.NoofRecords = (int)sqlParams[3].Value;
            objHrCommon.TotalPages = (int)sqlParams[2].Value;
            return ds;
        }
        public SqlDataReader GetTodayAttendanceForExportExcel(int? DeptID, int? WSID, int? ShiftID)
        {
            try
            {
                SqlDataReader dr;
                dr = SQLDBUtil.ExecuteDataReader("HR_GetTodayAttendanceForExportExcel", new SqlParameter[] { new SqlParameter("@DeptID", DeptID), new SqlParameter("@WSID", WSID), new SqlParameter("@ShiftID", ShiftID) });
                return dr;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet HR_GetTodayAttendanceByShift(int DeptID, int WSID, int Shift, int CompanyID)
        {
            try
            {
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetTodayAttendanceByShift",
                     new SqlParameter[] { new SqlParameter("@DeptID", DeptID), 
                    new SqlParameter("@WSID", WSID), 
                    new SqlParameter("@Shift", Shift),
                    new SqlParameter("@CompanyID", CompanyID)});
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet GetAttendanceType()
        {
            try
            {
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetAttendanceType");
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet GetGoogleSearchAttendanceType(String SearchKey)
        {
            try
            {
                SqlParameter[] parm = new SqlParameter[1];
                parm[0] = new SqlParameter("@search", SearchKey);
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetAttendanceType_googlesearch", parm);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet GetTodayAttendanceforEditing(int DeptID, int WSID, DateTime Date, string Name, int CompanyID)
        {
            try
            {
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetTodayAttendanceforEditing", new SqlParameter[] 
                { new SqlParameter("@DeptID", DeptID), 
                    new SqlParameter("@WSID", WSID), 
                    new SqlParameter("@date", Date), 
                    new SqlParameter("@EmpName", Name),
                new SqlParameter("@CompanyID", CompanyID)});
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet GetTodayAttendanceforEditing_By_Empid(int DeptID, int WSID, DateTime Date, string Name, int CompanyID, string empid, HRCommon objHrCommon)
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
                sqlParams[4] = new SqlParameter("@DeptID", DeptID);
                sqlParams[5] = new SqlParameter("@WSID", WSID);
                sqlParams[6] = new SqlParameter("@date", Date);
                sqlParams[7] = new SqlParameter("@EmpName", Name);
                sqlParams[8] = new SqlParameter("@CompanyID", CompanyID);
                sqlParams[9] = new SqlParameter("@Empid", empid);
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetTodayAttendanceforEditing_By_Empid", sqlParams);
                objHrCommon.NoofRecords = (int)sqlParams[3].Value;
                objHrCommon.TotalPages = (int)sqlParams[2].Value;
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet GetTodayNMRAttendance(int DeptID, int WSID)
        {
            try
            {
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetTodayNMRAttendance", new SqlParameter[] { new SqlParameter("@DeptID", DeptID), new SqlParameter("@WSID", WSID) });
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet HR_GetTodayNMRAttendanceforEditing(int DeptID, int WSID, DateTime Date, string Name)
        {
            try
            {
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetTodayNMRAttendanceforEditing", new SqlParameter[] { new SqlParameter("@DeptID", DeptID), new SqlParameter("@WSID", WSID), new SqlParameter("@date", Date), new SqlParameter("@EmpName", Name) });
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet GetAttendanceByMonth(int Month, int Year)
        {
            try
            {
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetAttendanceByMonth", new SqlParameter[] { new SqlParameter("@Month", Month), new SqlParameter("@Year", Year) });
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet GetAttendanceByMonth_Cursor(int Month, int Year, int Dept, int WS, int empid, string Name, int? EmpNatureID)
        {
            try
            {
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetAttendanceByMonth_Cursor", new SqlParameter[] { new SqlParameter("@Month", Month),
                    new SqlParameter("@Year", Year), new SqlParameter("@WSID", WS), new SqlParameter("@DeptID", Dept), 
                    new SqlParameter("@Userid", empid), new SqlParameter("@Name", Name) , new SqlParameter("@EmpNatureID", EmpNatureID) });
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet HR_GetHolidayNonPayRules(int empid, DateTime date)
        {
            try
            {
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetHolidayNonPayRules", new SqlParameter[] { new SqlParameter("@Date", date), new SqlParameter("@EmpID", empid) });
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet GetAttendanceByDay_Cursor(DateTime Day, int Dept, int WS, int empid, string Name, int CompnayID, int CurrentPage, int PageSize, ref int NoofRecords, ref int TotalPages, int Projectid)
        {
            try
            {
                SqlParameter[] objParam = new SqlParameter[11];
                objParam[0] = new SqlParameter("@Date", Day);
                objParam[1] = new SqlParameter("@WSID", WS);
                objParam[2] = new SqlParameter("@DeptID", Dept);
                objParam[3] = new SqlParameter("@Userid", empid);
                objParam[4] = new SqlParameter("@Name", Name);
                objParam[5] = new SqlParameter("@CompanyID", CompnayID);
                objParam[6] = new SqlParameter("@CurrentPage", CurrentPage);
                objParam[7] = new SqlParameter("@PageSize", PageSize);
                objParam[8] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
                objParam[8].Direction = ParameterDirection.ReturnValue;
                objParam[9] = new SqlParameter("@NoofRecords", System.Data.SqlDbType.Int);
                objParam[9].Direction = ParameterDirection.Output;
                if (Projectid != 0)
                    objParam[10] = new SqlParameter("@Projectid", Projectid);
                else
                    objParam[10] = new SqlParameter("@Projectid", System.Data.SqlDbType.Int);
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetAttendanceByDay_Cursor", objParam);
                NoofRecords = (int)objParam[9].Value;
                TotalPages = (int)objParam[8].Value;
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public SqlDataReader GetAttendanceByDay_CursorReader(DateTime Day, int Dept, int WS, int empid, string Name)
        {
            try
            {
                SqlDataReader dr;
                dr = SQLDBUtil.ExecuteDataReader("HR_GetAttendanceByDay_Cursor", new SqlParameter[] { new SqlParameter("@Date", Day), new SqlParameter("@WSID", WS), new SqlParameter("@DeptID", Dept), new SqlParameter("@Userid", empid), new SqlParameter("@Name", Name) });
                return dr;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #region GetCalenderYear
        public static DataSet GetCalenderYear()
        {
            try
            {
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetCalenderYears");
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        public DataSet GetNMRAttendanceByDay_Cursor(DateTime Day, int Dept, int WS, int empid)
        {
            try
            {
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetNMRAttendanceByDay_Cursor", new SqlParameter[] { new SqlParameter("@Date", Day), new SqlParameter("@WSID", WS), new SqlParameter("@DeptID", Dept), new SqlParameter("@Userid", empid) });
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet GetNMRAttendanceByMonth_Cursor(int Month, int Year, int Dept, int WS, int empid)
        {
            try
            {
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetNMRAttendanceByMonth_Cursor", new SqlParameter[] { new SqlParameter("@Month", Month), new SqlParameter("@Year", Year), new SqlParameter("@WSID", WS), new SqlParameter("@DeptID", Dept), new SqlParameter("@Userid", empid) });
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet HR_GetNMR_WS_DeptFilter(int WS)
        {
            try
            {
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetNMR_WS_DeptFilter", new SqlParameter[] { new SqlParameter("@WSID", WS) });
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet GetDeptHeads(int WS, int Dept)
        {
            try
            {
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetDeptHeads1", new SqlParameter[] { new SqlParameter("@WSId", WS), new SqlParameter("@DeptId", Dept) });
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet GetDeptHeadsForOC(int PrjID)
        {
            try
            {
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_DeptHeadsForOC", new SqlParameter[] { new SqlParameter("@Prjid", PrjID) });
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet GetDeptHeadsForTransfer(int? EmpID, int? WS, int? Dept)
        {
            try
            {
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetDeptHeadsForTransfer", new SqlParameter[] { new SqlParameter("@EmpID", EmpID), new SqlParameter("@WSId", WS), new SqlParameter("@DeptId", Dept) });
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void UPDSetAsDeptHead(int WS, int Dept, int EmpID, int UserID)
        {
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[4];
                sqlParams[0] = new SqlParameter("@SiteID", WS);
                sqlParams[1] = new SqlParameter("@DeptID", Dept);
                sqlParams[2] = new SqlParameter("@EmpID", EmpID);
                sqlParams[3] = new SqlParameter("@UserID", UserID);
                SQLDBUtil.ExecuteNonQuery("HR_SetAsDeptHead", sqlParams);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void UPDATESetAsDeptHead(int WS, int Dept, int EmpID, int UserID)
        {
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[4];
                sqlParams[0] = new SqlParameter("@SiteID", WS);
                sqlParams[1] = new SqlParameter("@DeptID", Dept);
                sqlParams[2] = new SqlParameter("@EmpID", EmpID);
                sqlParams[3] = new SqlParameter("@UserID", UserID);
                SQLDBUtil.ExecuteNonQuery("HR_SetAsDepartHead", sqlParams);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet GetDeptHeadsAll(int WS, int Dept)
        {
            try
            {
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetDeptHeadsAll1", new SqlParameter[] { new SqlParameter("@WSId", WS), new SqlParameter("@DeptId", Dept) });
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet GetDeptHeadsTransfer(int WS, int Dept)
        {
            try
            {
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetDeptHeadsTransfer", new SqlParameter[] { new SqlParameter("@WSId", WS), new SqlParameter("@DeptId", Dept) });
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet GetDeptHead(int WS, int Dept, int EmpID)
        {
            try
            {
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetDeptHead", new SqlParameter[] { new SqlParameter("@WSId", WS), new SqlParameter("@DeptId", Dept), new SqlParameter("@EmpID", EmpID) });
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static void UpdateSiteDeptChanges(int EmpID, int SiteID, int DeptID, int Mgnr, int UserID)
        {
            try
            {
                SQLDBUtil.ExecuteNonQuery("HR_UpdSiteDeptChanges", new SqlParameter[] { 
                                        new SqlParameter("@EmpID", EmpID), 
                                        new SqlParameter("@SiteID",SiteID),
                                        new SqlParameter("@DeptID", DeptID),
                                        new SqlParameter("@Mgnr", Mgnr),
                                        new SqlParameter("@UserID", UserID)});
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int AddEmpDetails(string empname)
        {
            try
            {
                int result;
                SqlParameter[] sqlParams = new SqlParameter[2];
                sqlParams[0] = new SqlParameter("@EmpName", empname);
                sqlParams[1] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
                sqlParams[1].Direction = ParameterDirection.ReturnValue;
                SQLDBUtil.ExecuteNonQuery("HR_AddEmpDetails", sqlParams);
                result = Convert.ToInt16(sqlParams[1].Value);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int UserNameAvailable(HRCommon obj)
        {
            try
            {
                int result;
                SqlParameter[] sqlParams = new SqlParameter[3];
                sqlParams[0] = new SqlParameter("@Username", obj.Username);
                sqlParams[1] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
                sqlParams[1].Direction = ParameterDirection.ReturnValue;
                sqlParams[2] = new SqlParameter("@EmpID", obj.EmpID);
                SQLDBUtil.ExecuteNonQuery("HR_CheckUsername", sqlParams);
                result = Convert.ToInt16(sqlParams[1].Value);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void UpdatePWD(HRCommon obj)
        {
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[2];
                sqlParams[0] = new SqlParameter("@EmpID", obj.EmpID);
                sqlParams[1] = new SqlParameter("@Pwd", obj.OldPassWord);
                SQLDBUtil.ExecuteNonQuery("HR_UpdateMD5Password", sqlParams);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void DeptDisplayoder(int DeptID, int Order)
        {
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[2];
                sqlParams[0] = new SqlParameter("@DeptID", DeptID);
                sqlParams[1] = new SqlParameter("@Order", Order);
                SQLDBUtil.ExecuteNonQuery("HR_DeptDisplayoder", sqlParams);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void WorksiteDisplayoder(int SiteID, int Order)
        {
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[2];
                sqlParams[0] = new SqlParameter("@SiteID", SiteID);
                sqlParams[1] = new SqlParameter("@Order", Order);
                SQLDBUtil.ExecuteNonQuery("HR_WorkSiteDisplayoder", sqlParams);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void EmpDisplayoder(int EmpID, int Order)
        {
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[2];
                sqlParams[0] = new SqlParameter("@EmpID", EmpID);
                sqlParams[1] = new SqlParameter("@Order", Order);
                SQLDBUtil.ExecuteNonQuery("HR_EMPDisplayoder", sqlParams);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void Insert_Emp_Edu_Exp_Details(int EmpID, int AppID)
        {
            SqlParameter[] Param = new SqlParameter[2];
            Param[0] = new SqlParameter("@appid", AppID);
            Param[1] = new SqlParameter("@empID", EmpID);
            SQLDBUtil.ExecuteNonQuery("Ins_Emp_Edu_Exp", Param);
        }
        public int InsertEmpDetails(HRCommon objHrCommon)
        {
            try
            {
                SqlParameter[] objParam = new SqlParameter[25];
                objParam[0] = new SqlParameter("@AppID", objHrCommon.AppID);
                objParam[1] = new SqlParameter("@DeptNo", objHrCommon.DeptID);
                objParam[2] = new SqlParameter("@FName", objHrCommon.FName);
                objParam[3] = new SqlParameter("@MName", objHrCommon.MName);
                objParam[4] = new SqlParameter("@LName", objHrCommon.LName);
                objParam[5] = new SqlParameter("@Designation", objHrCommon.Designation);
                objParam[6] = new SqlParameter("@Mobile1", objHrCommon.Mobile);
                objParam[7] = new SqlParameter("@Mailid", objHrCommon.Email);
                objParam[8] = new SqlParameter("@Categary", objHrCommon.SiteID);
                objParam[9] = new SqlParameter("@Image", objHrCommon.ImageType);
                objParam[10] = new SqlParameter("@DOB", objHrCommon.DOB);
                objParam[11] = new SqlParameter("@OfferLetter", objHrCommon.OfferLetter);
                objParam[13] = new SqlParameter("@pAddress", objHrCommon.Address);
                objParam[14] = new SqlParameter("@pcity", objHrCommon.City);
                objParam[15] = new SqlParameter("@pstate", objHrCommon.State);
                objParam[16] = new SqlParameter("@PerCountry", objHrCommon.Country);
                objParam[17] = new SqlParameter("@PerPhone", objHrCommon.Phone);
                objParam[18] = new SqlParameter("@PerPin", objHrCommon.Pin);
                objParam[19] = new SqlParameter("@SameAddress", objHrCommon.SameAddress);
                objParam[20] = new SqlParameter("@Salary", objHrCommon.Salary);
                objParam[21] = new SqlParameter("@DOJ", objHrCommon.ReqDate);
                objParam[22] = new SqlParameter("@Qualification", objHrCommon.Qualification);
                objParam[23] = new SqlParameter("@DesigID", objHrCommon.DesigID);
                objParam[24] = new SqlParameter("@TradeID", objHrCommon.TradeID);
                objParam[12] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
                objParam[12].Direction = ParameterDirection.ReturnValue;
                SQLDBUtil.ExecuteNonQuery("HR_InsUpdateT_G_EmployeeMaster", objParam);
                int EmpID = Convert.ToInt16(objParam[12].Value);
                return EmpID;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int InsUpdate_EmployeeDetails(HRCommon objHrCommon)
        {
            SqlParameter[] objParam = new SqlParameter[38];
            objParam[0] = new SqlParameter("@EmpID	", objHrCommon.EmpID);
            objParam[1] = new SqlParameter("@DeptNo", objHrCommon.DeptID);
            objParam[2] = new SqlParameter("@FName", objHrCommon.FName);
            objParam[3] = new SqlParameter("@MName", objHrCommon.MName);
            objParam[4] = new SqlParameter("@LName", objHrCommon.LName);
            objParam[5] = new SqlParameter("@UserName", objHrCommon.Username);
            objParam[6] = new SqlParameter("@PassWord", objHrCommon.UserPWD);
            objParam[7] = new SqlParameter("@Designation", objHrCommon.Designation);
            objParam[8] = new SqlParameter("@Mobile1", objHrCommon.Mobile);
            objParam[9] = new SqlParameter("@Mobile2", objHrCommon.Mobile2);
            objParam[37] = new SqlParameter("@MailID", objHrCommon.Email);
            objParam[10] = new SqlParameter("@AltMail", objHrCommon.AEmail);
            objParam[11] = new SqlParameter("@SkypeID", objHrCommon.SkypeID);
            objParam[13] = new SqlParameter("@Type", objHrCommon.Type);
            objParam[14] = new SqlParameter("@Categary", objHrCommon.SiteID);
            objParam[15] = new SqlParameter("@Mgnr", objHrCommon.Mgnr);
            objParam[16] = new SqlParameter("@image", objHrCommon.ImageType);
            objParam[17] = new SqlParameter("@DesigID", objHrCommon.DesigID);
            objParam[18] = new SqlParameter("@DOB", objHrCommon.DOB);
            objParam[19] = new SqlParameter("@Mole1", objHrCommon.Mole1);
            objParam[20] = new SqlParameter("@Mole2", objHrCommon.Mole2);
            objParam[21] = new SqlParameter("@pAddress", objHrCommon.Address);
            objParam[22] = new SqlParameter("@pcity", objHrCommon.City);
            objParam[23] = new SqlParameter("@pstate", objHrCommon.State);
            objParam[24] = new SqlParameter("@PerCountry", objHrCommon.Country);
            objParam[25] = new SqlParameter("@PerPhone", objHrCommon.Phone);
            objParam[26] = new SqlParameter("@PerPin", objHrCommon.Pin);
            objParam[27] = new SqlParameter("@ResAddress", objHrCommon.PRAddress);
            objParam[28] = new SqlParameter("@SameAddress", objHrCommon.SameAddress);
            objParam[29] = new SqlParameter("@ResCity", objHrCommon.PrCity);
            objParam[30] = new SqlParameter("@ResState", objHrCommon.PrState);
            objParam[31] = new SqlParameter("@ResCountry", objHrCommon.PrCountry);
            objParam[32] = new SqlParameter("@ResPin", objHrCommon.PrPin);
            objParam[33] = new SqlParameter("@ResPhone", objHrCommon.PrPhone);
            objParam[34] = new SqlParameter("@Bloodgroup", objHrCommon.BGroup);
            objParam[35] = new SqlParameter("@Salary", objHrCommon.Salary);
            objParam[36] = new SqlParameter("@DOJ", objHrCommon.ReqDate);
            objParam[12] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
            objParam[12].Direction = ParameterDirection.ReturnValue;
            SQLDBUtil.ExecuteNonQuery("InsUpdate_T_G_EmployeeMaster", objParam);
            int EmpID = Convert.ToInt16(objParam[12].Value);
            return EmpID;
        }
        public int AddDeptHead(int WSId, int DeptID, int EmpId, int UserID)
        {
            try
            {
                int result;
                SqlParameter[] sqlParams = new SqlParameter[5];
                sqlParams[0] = new SqlParameter("@PrjID", WSId);
                sqlParams[1] = new SqlParameter("@DeptID", DeptID);
                sqlParams[3] = new SqlParameter("@EmpID", EmpId);
                sqlParams[4] = new SqlParameter("@UserID", UserID);
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
        public static int InsUpd_Achievements(HRCommon objHrCommon)
        {
            try
            {
                int result;
                SqlParameter[] sqlParams = new SqlParameter[6];
                sqlParams[0] = new SqlParameter("@AchievementID", objHrCommon.AchievmentID);
                sqlParams[1] = new SqlParameter("@EmpID", objHrCommon.EmpID);
                sqlParams[2] = new SqlParameter("@Duties", objHrCommon.Duties);
                sqlParams[3] = new SqlParameter("@Achievements", objHrCommon.Achievemets);
                sqlParams[4] = new SqlParameter("@UserID", objHrCommon.UserID);
                sqlParams[5] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
                sqlParams[5].Direction = ParameterDirection.ReturnValue;
                SQLDBUtil.ExecuteNonQuery("HR_InsUpd_Achievements", sqlParams);
                result = Convert.ToInt16(sqlParams[5].Value);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet GetHeadDetails(int WSId, int DeptID, int EmpId)
        {
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[3];
                sqlParams[0] = new SqlParameter("@PrjID", WSId);
                sqlParams[1] = new SqlParameter("@DeptID", DeptID);
                sqlParams[2] = new SqlParameter("@EmpID", EmpId);
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_DeptHeadDetailsForOC", sqlParams);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet GetAchievementDetails(HRCommon obj)
        {
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[1];
                sqlParams[0] = new SqlParameter("@EmpID", obj.EmpID);
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetAchievemet", sqlParams);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void AddAcademicDetails(HRCommon objHrCommon)
        {
            try
            {
                SqlParameter[] objParam = new SqlParameter[7];
                objParam[0] = new SqlParameter("@AppID", objHrCommon.AppID);
                objParam[1] = new SqlParameter("@Qualification", objHrCommon.Qualification);
                objParam[2] = new SqlParameter("@Institute", objHrCommon.Institute);
                objParam[3] = new SqlParameter("@YOP", objHrCommon.YOP);
                objParam[4] = new SqlParameter("@Specialization", objHrCommon.Specialization);
                objParam[5] = new SqlParameter("@Percentage", objHrCommon.Percentage);
                objParam[6] = new SqlParameter("@Mode", objHrCommon.Mode);
                SQLDBUtil.ExecuteNonQuery("HR_InsUpdate_T_G_Applicant_EducationDetails", objParam);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void UpdateSiteDeptChanges(HRCommon objHrCommon)
        {
            try
            {
                SqlParameter[] objParam = new SqlParameter[6];
                objParam[0] = new SqlParameter("@EmpID", objHrCommon.EmpID);
                objParam[1] = new SqlParameter("@Designation", objHrCommon.Designation);
                objParam[2] = new SqlParameter("@SiteID", objHrCommon.SiteID);
                objParam[3] = new SqlParameter("@DeptID", objHrCommon.DeptID);
                objParam[4] = new SqlParameter("@Mgnr", objHrCommon.Mgnr);
                objParam[5] = new SqlParameter("@UserID", objHrCommon.UserID);
                SQLDBUtil.ExecuteNonQuery("HR_UpdateSiteDeptChanges", objParam);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public int UpdSiteDeptChanges(HRCommon objHrCommon)
        {
            try
            {
                SqlParameter[] objParam = new SqlParameter[8];
                objParam[0] = new SqlParameter("@EmpID", objHrCommon.EmpID);
                objParam[1] = new SqlParameter("@Designation", objHrCommon.Designation);
                objParam[2] = new SqlParameter("@SiteID", objHrCommon.SiteID);
                objParam[3] = new SqlParameter("@DeptID", objHrCommon.DeptID);
                objParam[4] = new SqlParameter("@Mgnr", objHrCommon.Mgnr);
                objParam[5] = new SqlParameter("@UserID", objHrCommon.UserID);
                objParam[6] = new SqlParameter("@Ext", objHrCommon.Ext);
                objParam[7] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
                objParam[7].Direction = ParameterDirection.ReturnValue;
                SQLDBUtil.ExecuteNonQuery("HR_UpdSiteDeptChanges", objParam);
                int result = Convert.ToInt32(objParam[7].Value);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void AddApplicantOfferDetails(HRCommon objHrCommon, int DesigID, int TradeID)
        {
            try
            {
                SqlParameter[] objParam = new SqlParameter[11];
                objParam[0] = new SqlParameter("@AppID", objHrCommon.AppID);
                objParam[1] = new SqlParameter("@Salary", objHrCommon.Salary);
                objParam[2] = new SqlParameter("@ReqDOJ", objHrCommon.ReqDate);
                objParam[3] = new SqlParameter("@Designation", objHrCommon.Designation);
                objParam[4] = new SqlParameter("@JobType", objHrCommon.JobType);
                objParam[5] = new SqlParameter("@offerletter", objHrCommon.OfferLetter);
                objParam[6] = new SqlParameter("@CreatedBY", objHrCommon.UserID);
                objParam[7] = new SqlParameter("@ImageType", objHrCommon.ImageType);
                objParam[8] = new SqlParameter("@ProjectID", objHrCommon.SiteID);
                objParam[9] = new SqlParameter("@DesigID", DesigID);
                objParam[10] = new SqlParameter("@TradeID", TradeID);
                SQLDBUtil.ExecuteNonQuery("HR_InsUpdate_ApplicantOffer", objParam);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void UpdateOfferStatus(HRCommon objHrCommon)
        {
            try
            {
                SqlParameter[] objParam = new SqlParameter[2];
                objParam[0] = new SqlParameter("@AppID", objHrCommon.AppID);
                objParam[1] = new SqlParameter("@AccDOJ", objHrCommon.AccDated);
                SQLDBUtil.ExecuteNonQuery("HR_Update_OfferStatus", objParam);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public SqlDataReader GetEduDetails(int AppId)
        {
            return SQLDBUtil.ExecuteDataReader("HR_GetApplicantEducationalList", new SqlParameter[] { new SqlParameter("@AppID", AppId) });
        }
        public SqlDataReader GetAcademicDetails(int AppId)
        {
            return SQLDBUtil.ExecuteDataReader("HR_GetApplicantExperienceList", new SqlParameter[] { new SqlParameter("@AppID", AppId) });
        }
        public DataSet GetEduDetails(int AppId, int App)
        {
            return SQLDBUtil.ExecuteDataset("HR_GetApplicantExperienceList", new SqlParameter[] { new SqlParameter("@AppID", AppId) });
        }
        public DataSet GetApplicantdetails(int ApplicantID)
        {
            return SQLDBUtil.ExecuteDataset("HR_GetApplicantDetails", new SqlParameter[] { new SqlParameter("@AppID", ApplicantID) });
        }
        public DataSet GetAcademicDetails(int AppId, int App)
        {
            return SQLDBUtil.ExecuteDataset("HR_GetApplicantEducationalList", new SqlParameter[] { new SqlParameter("@AppID", AppId) });
        }
        public DataSet GetRemarksDetails(int AppId)
        {
            return SQLDBUtil.ExecuteDataset("HR_GetRemarksList", new SqlParameter[] { new SqlParameter("@AppID", AppId) });
        }
        public void DelAcademicDetails(int AppEduID)
        {
            SQLDBUtil.ExecuteNonQuery("HR_DelApplicantEducationDetails", new SqlParameter[] { new SqlParameter("@AppEduID", AppEduID) });
        }
        public void DelEmpExperience(int AppExpID)
        {
            SQLDBUtil.ExecuteNonQuery("HR_DelApplicantExperience", new SqlParameter[] { new SqlParameter("@AppExpID", AppExpID) });
        }
        public static DataSet GetApplicantListByPaging(HRCommon objHrCommon, int? WSID, int CompanyID)
        {
            SqlParameter[] sqlParams = new SqlParameter[8];
            sqlParams[0] = new SqlParameter("@CurrentPage", objHrCommon.CurrentPage);
            sqlParams[1] = new SqlParameter("@PageSize", objHrCommon.PageSize);
            sqlParams[2] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
            sqlParams[2].Direction = ParameterDirection.ReturnValue;
            sqlParams[3] = new SqlParameter("@NoofRecords", System.Data.SqlDbType.Int);
            sqlParams[3].Direction = ParameterDirection.Output;
            sqlParams[4] = new SqlParameter("@PosID", objHrCommon.PosID);
            sqlParams[5] = new SqlParameter("@Status", objHrCommon.AppStatus);
            sqlParams[6] = new SqlParameter("@WSID", WSID);
            sqlParams[7] = new SqlParameter("@CompanyID", CompanyID);
            DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetApplicantListByPaging", sqlParams);
            objHrCommon.NoofRecords = (int)sqlParams[3].Value;
            objHrCommon.TotalPages = (int)sqlParams[2].Value;
            return ds;
        }
        public DataSet GetApplicantList(HRCommon objHrCommon)
        {
            SqlParameter[] objParam = new SqlParameter[2];
            objParam[0] = new SqlParameter("@PosID", objHrCommon.PosID);
            objParam[1] = new SqlParameter("@Status", objHrCommon.AppStatus);
            return SQLDBUtil.ExecuteDataset("HR_GetApplicantList", objParam);
        }
        public static DataSet ApplicantOfferListByPaging(HRCommon objHrCommon, int? WSID, int? PosID, int CompanyID)
        {
            SqlParameter[] sqlParams = new SqlParameter[7];
            sqlParams[0] = new SqlParameter("@CurrentPage", objHrCommon.CurrentPage);
            sqlParams[1] = new SqlParameter("@PageSize", objHrCommon.PageSize);
            sqlParams[2] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
            sqlParams[2].Direction = ParameterDirection.ReturnValue;
            sqlParams[3] = new SqlParameter("@NoofRecords", System.Data.SqlDbType.Int);
            sqlParams[3].Direction = ParameterDirection.Output;
            sqlParams[4] = new SqlParameter("@WSID", WSID);
            sqlParams[5] = new SqlParameter("@POSID", PosID);
            sqlParams[6] = new SqlParameter("@CompanyID", CompanyID);
            DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetApplicantOfferListByPaging", sqlParams);
            objHrCommon.NoofRecords = (int)sqlParams[3].Value;
            objHrCommon.TotalPages = (int)sqlParams[2].Value;
            return ds;
        }
        public static DataSet ApplicantOfferListExportToXL(int? WSID, int? PosID)
        {
            SqlParameter[] sqlParams = new SqlParameter[2];
            sqlParams[0] = new SqlParameter("@WSID", WSID);
            sqlParams[1] = new SqlParameter("@POSID", PosID);
            DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetApplicantOfferListExportToXL", sqlParams);
            return ds;
        }
        public DataSet ApplicantOfferList()
        {
            return SQLDBUtil.ExecuteDataset("HR_GetApplicantOfferList");
        }
        public static DataSet AcceptedOfferListByPaging(HRCommon objHrCommon, int? WSID, int? POSID, int Status, int CompanyID)
        {
            SqlParameter[] sqlParams = new SqlParameter[8];
            sqlParams[0] = new SqlParameter("@CurrentPage", objHrCommon.CurrentPage);
            sqlParams[1] = new SqlParameter("@PageSize", objHrCommon.PageSize);
            sqlParams[2] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
            sqlParams[2].Direction = ParameterDirection.ReturnValue;
            sqlParams[3] = new SqlParameter("@NoofRecords", System.Data.SqlDbType.Int);
            sqlParams[3].Direction = ParameterDirection.Output;
            sqlParams[4] = new SqlParameter("@WSID", WSID);
            sqlParams[5] = new SqlParameter("@POSID", POSID);
            sqlParams[6] = new SqlParameter("@Status", Status);
            sqlParams[7] = new SqlParameter("@CompanyID", CompanyID);
            DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetAcceptedOfferListByPaging", sqlParams);
            objHrCommon.NoofRecords = (int)sqlParams[3].Value;
            objHrCommon.TotalPages = (int)sqlParams[2].Value;
            return ds;
        }
        public DataSet AcceptedOfferList()
        {
            return SQLDBUtil.ExecuteDataset("HR_GetAcceptedOfferList");
        }
        public void AddempExperience(HRCommon objHrCommon)
        {
            try
            {
                SqlParameter[] objParam = new SqlParameter[8];
                objParam[0] = new SqlParameter("@AppID", objHrCommon.AppID);
                objParam[1] = new SqlParameter("@Organization", objHrCommon.Organization);
                objParam[2] = new SqlParameter("@City", objHrCommon.City);
                objParam[3] = new SqlParameter("@Type", objHrCommon.Type);
                objParam[4] = new SqlParameter("@FromDate", objHrCommon.FromDate);
                objParam[5] = new SqlParameter("@ToDate", objHrCommon.ToDate);
                objParam[6] = new SqlParameter("@Designation", objHrCommon.Designation);
                objParam[7] = new SqlParameter("@CurrentCTC", objHrCommon.CurrentCTC);
                SQLDBUtil.ExecuteNonQuery("HR_InsUpdate_T_G_Applicant_Experience", objParam);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void DeleteApplicant(HRCommon objHrCommon)
        {
            try
            {
                SqlParameter[] objParam = new SqlParameter[1];
                objParam[0] = new SqlParameter("@AppID", objHrCommon.AppID);
                SQLDBUtil.ExecuteNonQuery("HR_DeleteApplicant", objParam);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public int AddApplicantStatus(HRCommon objHrCommon)
        {
            try
            {
                SqlParameter[] objParam = new SqlParameter[6];
                objParam[0] = new SqlParameter("@AppID", objHrCommon.AppID);
                objParam[1] = new SqlParameter("@status", objHrCommon.AppStatus);
                objParam[2] = new SqlParameter("@Remarks", objHrCommon.Remarks);
                objParam[3] = new SqlParameter("@EmpID", objHrCommon.UserID);
                objParam[5] = new SqlParameter("@Ranking", objHrCommon.Ranking);
                objParam[4] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
                objParam[4].Direction = ParameterDirection.ReturnValue;
                SQLDBUtil.ExecuteNonQuery("HR_InsUpdate_T_G_UpdateStatus", objParam);
                int result = Convert.ToInt16(objParam[4].Value);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public int AddApplicant(HRCommon objHrCommon)
        {
            try
            {
                int EntryType = 2;
                SqlParameter[] objParam = new SqlParameter[29];
                objParam[0] = new SqlParameter("@MName", objHrCommon.MName);
                objParam[1] = new SqlParameter("@LName", objHrCommon.LName);
                objParam[2] = new SqlParameter("@Email", objHrCommon.Email);
                objParam[3] = new SqlParameter("@Mobile", objHrCommon.Mobile);
                objParam[4] = new SqlParameter("@Phone", objHrCommon.Phone);
                objParam[5] = new SqlParameter("@DOB", objHrCommon.DOB);
                objParam[6] = new SqlParameter("@Father", objHrCommon.Father);
                objParam[7] = new SqlParameter("@Passport", objHrCommon.Passport);
                objParam[8] = new SqlParameter("@PDOI", objHrCommon.PDOI);
                objParam[9] = new SqlParameter("@PDOIPlace", objHrCommon.PDOIPlace);
                objParam[10] = new SqlParameter("@PDOE", objHrCommon.PDOE);
                objParam[11] = new SqlParameter("@PosID", objHrCommon.PosID);
                objParam[28] = new SqlParameter("@Trade", objHrCommon.TradeID);
                objParam[12] = new SqlParameter("@MaritalStatus", objHrCommon.MaritalSta);
                objParam[13] = new SqlParameter("@CurrentCTC", objHrCommon.CurrentCTC);
                objParam[14] = new SqlParameter("@ExpectedCTC", objHrCommon.ExpectedCT);
                objParam[15] = new SqlParameter("@Resume", objHrCommon.Resume);
                objParam[16] = new SqlParameter("@Status", objHrCommon.Status);
                objParam[17] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
                objParam[17].Direction = ParameterDirection.ReturnValue;
                objParam[18] = new SqlParameter("@AppID", objHrCommon.AppID);
                objParam[19] = new SqlParameter("@FName", objHrCommon.FName);
                objParam[20] = new SqlParameter("@Address", objHrCommon.Address);
                objParam[21] = new SqlParameter("@City", objHrCommon.City);
                objParam[22] = new SqlParameter("@State", objHrCommon.State);
                objParam[23] = new SqlParameter("@Country", objHrCommon.Country);
                objParam[24] = new SqlParameter("@Pin", objHrCommon.Pin);
                objParam[25] = new SqlParameter("@CLocation", objHrCommon.CurrentLocation);
                objParam[26] = new SqlParameter("@Gender", objHrCommon.Gender);
                objParam[27] = new SqlParameter("@EntryType", EntryType);
                SQLDBUtil.ExecuteNonQuery("HR_InsUpdate_T_G_Applications", objParam);
                return Convert.ToInt32(objParam[17].Value);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public int CreatePosting(HRCommon objHrCommon, int? WSID, double FrmExp, double? ToExp, int DesigID, int Trade)
        {
            try
            {
                SqlParameter[] objParam = new SqlParameter[17];
                objParam[0] = new SqlParameter("@PosID", objHrCommon.PosID);
                objParam[1] = new SqlParameter("@Position", objHrCommon.Position);
                objParam[2] = new SqlParameter("@DeptID", objHrCommon.DeptID);
                objParam[3] = new SqlParameter("@Description", objHrCommon.Description);
                objParam[4] = new SqlParameter("@Posts", objHrCommon.Posts);
                objParam[5] = new SqlParameter("@FromDate", objHrCommon.FromDate);
                objParam[6] = new SqlParameter("@ToDate", objHrCommon.ToDate);
                objParam[7] = new SqlParameter("@Timings", objHrCommon.Timings);
                objParam[8] = new SqlParameter("@Qualifications", objHrCommon.Qualification);
                objParam[9] = new SqlParameter("@InterviewTypeID", objHrCommon.InterviewTypeID);
                objParam[10] = new SqlParameter("@Status", objHrCommon.Status);
                objParam[11] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
                objParam[11].Direction = ParameterDirection.ReturnValue;
                objParam[12] = new SqlParameter("@WsID", WSID);
                objParam[13] = new SqlParameter("@ExpFrom", FrmExp);
                objParam[14] = new SqlParameter("@ExpTo", ToExp);
                objParam[15] = new SqlParameter("@DesigID", DesigID);
                objParam[16] = new SqlParameter("@Trade", Trade);
                SQLDBUtil.ExecuteNonQuery("HR_InsUpdate_Job_Openings", objParam);
                return Convert.ToInt32(objParam[11].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int CreateApplicantOffer(HRCommon objHrCommon)
        {
            try
            {
                SqlParameter[] objParam = new SqlParameter[10];
                objParam[0] = new SqlParameter("@AppID", objHrCommon.AppID);
                objParam[1] = new SqlParameter("@Salary", objHrCommon.Salary);
                objParam[2] = new SqlParameter("@ReqDOJ", objHrCommon.ReqDate);
                objParam[3] = new SqlParameter("@AccDOJ", objHrCommon.AccDated);
                objParam[4] = new SqlParameter("@Designation", objHrCommon.Designation);
                objParam[5] = new SqlParameter("@JobType", objHrCommon.Type);
                objParam[6] = new SqlParameter("@Status", objHrCommon.Status);
                objParam[7] = new SqlParameter("@Remarks", objHrCommon.Remarks);
                objParam[8] = new SqlParameter("@OfferLetter", objHrCommon.OfferLetter);
                objParam[9] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
                objParam[9].Direction = ParameterDirection.ReturnValue;
                SQLDBUtil.ExecuteNonQuery("HR_InsUpdate_ApplicantOffer", objParam);
                return Convert.ToInt32(objParam[9].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int CreateUserRoles(HRCommon objHrCommon)
        {
            try
            {
                SqlParameter[] objParam = new SqlParameter[4];
                objParam[0] = new SqlParameter("@EmpID", objHrCommon.EmpID);
                objParam[1] = new SqlParameter("@RoleID", objHrCommon.RoleID);
                objParam[2] = new SqlParameter("@UserID", objHrCommon.UserID);
                objParam[3] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
                objParam[3].Direction = ParameterDirection.ReturnValue;
                SQLDBUtil.ExecuteNonQuery("HR_InsUpdate_UserRoles", objParam);
                return Convert.ToInt32(objParam[3].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void DeleteUserRoles(HRCommon objHrCommon)
        {
            try
            {
                SqlParameter[] objParam = new SqlParameter[1];
                objParam[0] = new SqlParameter("@EmpID", objHrCommon.EmpID);
                SQLDBUtil.ExecuteNonQuery("HR_Del_UserRoles", objParam);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int Updatepassword(HRCommon objHrCommon)
        {
            try
            {
                SqlParameter[] objParam = new SqlParameter[4];
                objParam[0] = new SqlParameter("@EmpID", objHrCommon.EmpID);
                objParam[1] = new SqlParameter("@OldPassword", objHrCommon.OldPassWord);
                objParam[2] = new SqlParameter("@NewPassword", objHrCommon.NewPassWord);
                objParam[3] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
                objParam[3].Direction = ParameterDirection.ReturnValue;
                SQLDBUtil.ExecuteNonQuery("HR_Updatepassword", objParam);
                return Convert.ToInt32(objParam[3].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int ResetUserNamePassword(HRCommon objHrCommon)
        {
            try
            {
                SqlParameter[] objParam = new SqlParameter[5];
                objParam[0] = new SqlParameter("@EmpID", objHrCommon.EmpID);
                objParam[1] = new SqlParameter("@Username", objHrCommon.Username);
                objParam[2] = new SqlParameter("@NewPassWord", objHrCommon.NewPassWord);
                objParam[4] = new SqlParameter("@UserID", objHrCommon.UserID);
                objParam[3] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
                objParam[3].Direction = ParameterDirection.ReturnValue;
                SQLDBUtil.ExecuteNonQuery("HR_ResetUserNamePassword", objParam);
                return Convert.ToInt32(objParam[3].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet GetDaprtmentList()
        {
            try
            {
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetDepartmentList");
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet GetDaprtmentList_InEmployee()
        {
            try
            {
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetDepartmentList_InEmployee");
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet GetDaprtmentListForNewNMR(String SearchKey, int siteid, int Companyid)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[3];
                param[0] = new SqlParameter("@Search", SearchKey);
                param[1] = new SqlParameter("@SiteID", siteid);
                param[2] = new SqlParameter("@CompanyID", Companyid);
                return SQLDBUtil.ExecuteDataset("HR_NewNMRSearchgoogle_GetDepartmentList", param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet GetDesignations()
        {
            try
            {
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetDesignations");
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet GetGoogleSerachDesignations(String SearchKey)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@Search", SearchKey);
                return SQLDBUtil.ExecuteDataset("HR_GetSearchgoogleDesignations", param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet GetGoogleSerachEmployee(String SearchKey)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@Search", SearchKey);
                return SQLDBUtil.ExecuteDataset("HR_GoogleSearch_ProjectMangers", param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet GetGoogleSerachAllEmployee(String SearchKey)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@Search", SearchKey);
                return SQLDBUtil.ExecuteDataset("HR_GoogleSearchAll_Employee", param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet GetGoogleSerachCategory(String SearchKey)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@Search", SearchKey);
                return SQLDBUtil.ExecuteDataset("HR_GoogleSearc_GetCategories", param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet GetEmployeeTypes()
        {
            try
            {
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetEmployeeTypes");
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet GetCategories()
        {
            try
            {
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetCategories");
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet GetUserRolesList()
        {
            try
            {
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetUserRolesList");
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet GetUserRolesDetails(HRCommon obj)
        {
            try
            {
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetUserRolesDetails", new SqlParameter[] { new SqlParameter("@EmpID", obj.EmpID) });
                return ds;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public DataSet SearchEmpList(HRCommon obj)
        {
            try
            {
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetEmployeesByRoles", new SqlParameter[] { new SqlParameter("@WSId", obj.SiteID), new SqlParameter("@depId", obj.DeptID), new SqlParameter("@EmpName", obj.FName), new SqlParameter("@EmpId", Convert.ToInt32(obj.EmpID)) });
                return ds;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static DataSet SearchEmpListByPaging(HRCommon objHrCommon, int CompanyID)
        {
            SqlParameter[] sqlParams = new SqlParameter[9];
            sqlParams[0] = new SqlParameter("@CurrentPage", objHrCommon.CurrentPage);
            sqlParams[1] = new SqlParameter("@PageSize", objHrCommon.PageSize);
            sqlParams[2] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
            sqlParams[2].Direction = ParameterDirection.ReturnValue;
            sqlParams[3] = new SqlParameter("@NoofRecords", System.Data.SqlDbType.Int);
            sqlParams[3].Direction = ParameterDirection.Output;
            sqlParams[4] = new SqlParameter("@WSId", objHrCommon.SiteID);
            sqlParams[5] = new SqlParameter("@depId", objHrCommon.DeptID);
            sqlParams[6] = new SqlParameter("@EmpName", objHrCommon.FName);
            sqlParams[7] = new SqlParameter("@EmpId", Convert.ToInt32(objHrCommon.EmpID));
            sqlParams[8] = new SqlParameter("@CompanyID", CompanyID);
            DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetEmployeesByRolesByPaging", sqlParams);
            objHrCommon.NoofRecords = (int)sqlParams[3].Value;
            objHrCommon.TotalPages = (int)sqlParams[2].Value;
            return ds;
        }
        public DataSet GetPositionList()
        {
            try
            {
                return SQLDBUtil.ExecuteDataset("HR_GetPositionList");
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public DataSet GetPositionList_ViewApplicantList()
        {
            try
            {
                return SQLDBUtil.ExecuteDataset("HR_GetPositionList_ViewApplicantList");
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public DataSet GetPositionList_OfferApplicantList()
        {
            try
            {
                return SQLDBUtil.ExecuteDataset("HR_GetPositionList_OfferApplicantList");
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static DataSet GetJobOpeningsListByStatus(HRCommon objHrCommon, int Status, int WsID, string Exp, string Position, DateTime? Date, int CompanyID)
        {
            SqlParameter[] sqlParams = new SqlParameter[10];
            sqlParams[0] = new SqlParameter("@CurrentPage", objHrCommon.CurrentPage);
            sqlParams[1] = new SqlParameter("@PageSize", objHrCommon.PageSize);
            sqlParams[2] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
            sqlParams[2].Direction = ParameterDirection.ReturnValue;
            sqlParams[3] = new SqlParameter("@NoofRecords", System.Data.SqlDbType.Int);
            sqlParams[3].Direction = ParameterDirection.Output;
            sqlParams[4] = new SqlParameter("@Status", Status);
            sqlParams[5] = new SqlParameter("@Position", Position);
            sqlParams[6] = new SqlParameter("@Exp", Exp);
            sqlParams[7] = new SqlParameter("@WsID", WsID);
            sqlParams[8] = new SqlParameter("@Date", Date);
            sqlParams[9] = new SqlParameter("@CompanyID", CompanyID);
            DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetPositionListByPaging", sqlParams);
            objHrCommon.NoofRecords = (int)sqlParams[3].Value;
            objHrCommon.TotalPages = (int)sqlParams[2].Value;
            return ds;
        }
        public DataSet GetJobOpeningsList(int Status)
        {
            try
            {
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetPositionList", new SqlParameter[] { new SqlParameter("@Status", Status) });
                return ds;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public DataSet GetJobDetails(int PosID)
        {
            try
            {
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetJobDetails", new SqlParameter[] { new SqlParameter("@PosID", PosID) });
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet GetInterViewType()
        {
            try
            {
                return SQLDBUtil.ExecuteDataset("HR_GetInterViewTypeList");
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static void NewInterViewType(string InterviewType)
        {
            try
            {
                SqlParameter[] objParam = new SqlParameter[1];
                objParam[0] = new SqlParameter("@InterviewType", InterviewType);
                SQLDBUtil.ExecuteNonQuery("HR_AddInterViewType", objParam);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public DataSet GetEmployeesWorkStatus(HRCommon objHrCommon, int CompanyID, int empid)
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
                sqlParams[4] = new SqlParameter("@SiteID", objHrCommon.SiteID);
                sqlParams[5] = new SqlParameter("@DeptID", objHrCommon.DeptID);
                sqlParams[6] = new SqlParameter("@EmpName", objHrCommon.FName);
                sqlParams[7] = new SqlParameter("@EmpStatus", objHrCommon.CurrentStatus);
                sqlParams[8] = new SqlParameter("@CompanyID", CompanyID);
                sqlParams[9] = new SqlParameter("@empid", empid);
                DataSet ds = SQLDBUtil.ExecuteDataset("[HR_SearchEmpWorkStatus]", sqlParams);
                objHrCommon.NoofRecords = (int)sqlParams[3].Value;
                objHrCommon.TotalPages = (int)sqlParams[2].Value;
                return ds;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public DataSet GetEmployeesByPage(HRCommon objHrCommon)
        {
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[9];
                sqlParams[0] = new SqlParameter("@CurrentPage", objHrCommon.CurrentPage);
                sqlParams[1] = new SqlParameter("@PageSize", objHrCommon.PageSize);
                sqlParams[2] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
                sqlParams[2].Direction = ParameterDirection.ReturnValue;
                sqlParams[3] = new SqlParameter("@NoofRecords", System.Data.SqlDbType.Int);
                sqlParams[3].Direction = ParameterDirection.Output;
                sqlParams[4] = new SqlParameter("@SiteID", objHrCommon.SiteID);
                sqlParams[5] = new SqlParameter("@DeptID", objHrCommon.DeptID);
                sqlParams[6] = new SqlParameter("@EmpName", objHrCommon.FName);
                sqlParams[7] = new SqlParameter("@EmpStatus", objHrCommon.CurrentStatus);
                sqlParams[8] = new SqlParameter("@OldEmpID", objHrCommon.OldEmployeeID);
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_SearchEmpListByPage", sqlParams);
                objHrCommon.NoofRecords = (int)sqlParams[3].Value;
                objHrCommon.TotalPages = (int)sqlParams[2].Value;
                return ds;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public DataSet GetEmployeesByPageOrderByAssID(HRCommon objHrCommon, int OrderId, int Direction, int? EmpNatureID, int? EmpID, int CompanyID, int? DesigID, int? CatID)
        {
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[17];
                sqlParams[0] = new SqlParameter("@CurrentPage", objHrCommon.CurrentPage);
                sqlParams[1] = new SqlParameter("@PageSize", objHrCommon.PageSize);
                sqlParams[2] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
                sqlParams[2].Direction = ParameterDirection.ReturnValue;
                sqlParams[3] = new SqlParameter("@NoofRecords", System.Data.SqlDbType.Int);
                sqlParams[3].Direction = ParameterDirection.Output;
                sqlParams[4] = new SqlParameter("@SiteID", objHrCommon.SiteID);
                sqlParams[5] = new SqlParameter("@DeptID", objHrCommon.DeptID);
                sqlParams[6] = new SqlParameter("@EmpName", objHrCommon.FName);
                sqlParams[7] = new SqlParameter("@EmpStatus", objHrCommon.CurrentStatus);
                sqlParams[8] = new SqlParameter("@OldEmpID", objHrCommon.OldEmpID);
                sqlParams[9] = new SqlParameter("@OrdeID", OrderId);
                sqlParams[10] = new SqlParameter("@Direction", Direction);
                sqlParams[11] = new SqlParameter("@EmpNatureID", EmpNatureID);
                sqlParams[12] = new SqlParameter("@EmpID", EmpID);
                sqlParams[13] = new SqlParameter("@CompanyID", CompanyID);
                sqlParams[14] = new SqlParameter("@DesigID", DesigID);
                sqlParams[15] = new SqlParameter("@CatID", CatID);
                sqlParams[16] = new SqlParameter("@CountryID", objHrCommon.CountryID);
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_SearchEmpListByPageOrderByAssId", sqlParams);
                objHrCommon.NoofRecords = (int)sqlParams[3].Value;
                objHrCommon.TotalPages = (int)sqlParams[2].Value;
                return ds;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static DataSet HR_EmpListForCustoDocs(HRCommon objHrCommon, int EmpID, int CatID)
        {
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[2];
                sqlParams[0] = new SqlParameter("@SiteID", CatID);
                sqlParams[1] = new SqlParameter("@EmpID", EmpID);
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_EmpListForCustoDocs", sqlParams);
                return ds;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static DataSet HR_EmpListForCustoDocs_Paging(HRCommon objHrCommon, int EmpID, int CatID, int doctype, int custodystatus)
        {
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[8];
                sqlParams[0] = new SqlParameter("@CurrentPage", objHrCommon.CurrentPage);
                sqlParams[1] = new SqlParameter("@PageSize", objHrCommon.PageSize);
                sqlParams[2] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
                sqlParams[2].Direction = ParameterDirection.ReturnValue;
                sqlParams[3] = new SqlParameter("@NoofRecords", System.Data.SqlDbType.Int);
                sqlParams[3].Direction = ParameterDirection.Output;
                sqlParams[4] = new SqlParameter("@SiteID", CatID);
                sqlParams[5] = new SqlParameter("@EmpID", EmpID);
                sqlParams[6] = new SqlParameter("@doctype", doctype);
                sqlParams[7] = new SqlParameter("@custodystatus", custodystatus);
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_EmpListForCustoDocs_Paging", sqlParams);
                objHrCommon.NoofRecords = (int)sqlParams[3].Value;
                objHrCommon.TotalPages = (int)sqlParams[2].Value;
                return ds;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static DataSet GetWorkSites(int CompanyID)
        {
            try
            {
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetWorkSite_search", new SqlParameter[] { new SqlParameter("@WSID", "0"), new SqlParameter("@WSStatus", "1"), new SqlParameter("@CompanyID", CompanyID) });
                return ds;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static DataSet GetSiteIDByEmpid(int Empid)
        {
            try
            {
                SqlParameter[] objParam = new SqlParameter[1];
                objParam[0] = new SqlParameter("@Empid", Empid);
                DataSet ds = SQLDBUtil.ExecuteDataset("HMS_GetSiteIDByEmpID", objParam);
                return ds;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static DataSet GetEmpProofs(int EmpID)
        {
            try
            {
                SqlParameter[] objParam = new SqlParameter[1];
                objParam[0] = new SqlParameter("@EmpID", EmpID);
                DataSet ds = SQLDBUtil.ExecuteDataset("HMS_GetEmpProofs", objParam);
                return ds;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static int HMS_InsUpdEMpProofs(int ProofID, int DocID, int UserID, int EmpID, string Ext, int SiteID, int InvHolderID)
        {
            try
            {
                SqlParameter[] objParam = new SqlParameter[8];
                objParam[0] = new SqlParameter("@ProofID", ProofID);
                objParam[1] = new SqlParameter("@DocID", DocID);
                if (Ext != "")
                {
                    objParam[2] = new SqlParameter("@Ext", Ext);
                }
                else
                {
                    objParam[2] = new SqlParameter("@Ext", System.Data.SqlDbType.VarChar);
                }
                objParam[3] = new SqlParameter("@UserID", UserID);
                objParam[5] = new SqlParameter("@EmpID", EmpID);
                objParam[4] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
                objParam[4].Direction = ParameterDirection.ReturnValue;
                if (InvHolderID != 0)
                {
                    objParam[6] = new SqlParameter("@InvHolderID", InvHolderID);
                }
                else
                {
                    objParam[6] = new SqlParameter("@InvHolderID", System.Data.SqlDbType.Int);
                }
                if (SiteID != 0)
                {
                    objParam[7] = new SqlParameter("@SiteID", SiteID);
                }
                else
                {
                    objParam[7] = new SqlParameter("@SiteID", System.Data.SqlDbType.Int);
                }
                SQLDBUtil.ExecuteNonQuery("HMS_InsUpdEMpProofs", objParam);
                int result = Convert.ToInt16(objParam[4].Value);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public static DataSet GetInvCustodyHolder(int SiteID)
        {
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[1];
                if (SiteID != 0)
                {
                    sqlParams[0] = new SqlParameter("@Categary", SiteID);
                }
                else
                {
                    sqlParams[0] = new SqlParameter("@Categary", 0);
                }
                DataSet ds = SQLDBUtil.ExecuteDataset("HMS_Get_InvCustodyHolder", sqlParams);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet CreateUserRoles(int? EmpID, int? ModuleID, int? RoleID, int? UserID, int? NoOfLicences)
        {
            return SQLDBUtil.ExecuteDataset("CP_InsUpdate_UserRoles", new SqlParameter[] { new SqlParameter("@EmpID", EmpID), new SqlParameter("@ModuleID", ModuleID), new SqlParameter("@RoleID", RoleID), new SqlParameter("@UserID", UserID), new SqlParameter("@NoOfLicences", NoOfLicences) });
        }
        public static DataSet HR_EmpGetEmpDetailsByID(int EmpID, int Status)
        {
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[2];
                sqlParams[0] = new SqlParameter("@EmpID", EmpID);
                sqlParams[1] = new SqlParameter("@Status", Status);
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_EmpGetEmpDetailsByID", sqlParams);
                return ds;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static DataSet GetEmployeesListBysite(HRCommon objHrCommon, int SiteID, int DeptID, int EMPID, string EMPName)
        {
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[8];
                sqlParams[0] = new SqlParameter("@CurrentPage", objHrCommon.CurrentPage);
                sqlParams[1] = new SqlParameter("@PageSize", objHrCommon.PageSize);
                sqlParams[2] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
                sqlParams[2].Direction = ParameterDirection.ReturnValue;
                sqlParams[3] = new SqlParameter("@NoofRecords", System.Data.SqlDbType.Int);
                sqlParams[3].Direction = ParameterDirection.Output;
                sqlParams[4] = new SqlParameter("@SiteID", SiteID);
                sqlParams[5] = new SqlParameter("@DeptID", DeptID);
                sqlParams[6] = new SqlParameter("@EMPID", SqlDbType.Int);
                if (EMPID > 0)
                    sqlParams[6].Value = EMPID;
                sqlParams[7] = new SqlParameter("@EMPName", EMPName);
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_Get_EmployeesList_Paging", sqlParams);
                objHrCommon.NoofRecords = (int)sqlParams[3].Value;
                objHrCommon.TotalPages = (int)sqlParams[2].Value;
                return ds;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public DataSet GetEmployeesForPayslip(HRCommon objHrCommon)
        {
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[8];
                sqlParams[0] = new SqlParameter("@CurrentPage", objHrCommon.CurrentPage);
                sqlParams[1] = new SqlParameter("@PageSize", objHrCommon.PageSize);
                sqlParams[2] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
                sqlParams[2].Direction = ParameterDirection.ReturnValue;
                sqlParams[3] = new SqlParameter("@NoofRecords", System.Data.SqlDbType.Int);
                sqlParams[3].Direction = ParameterDirection.Output;
                sqlParams[4] = new SqlParameter("@SiteID", objHrCommon.SiteID);
                sqlParams[5] = new SqlParameter("@DeptID", objHrCommon.DeptID);
                sqlParams[6] = new SqlParameter("@EmpName", objHrCommon.FName);
                sqlParams[7] = new SqlParameter("@EmpStatus", objHrCommon.CurrentStatus);
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_SearchEmpListForPayslip", sqlParams);
                objHrCommon.NoofRecords = (int)sqlParams[3].Value;
                objHrCommon.TotalPages = (int)sqlParams[2].Value;
                return ds;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public DataSet GetNMRByPage(HRCommon objHrCommon, int? DesigID, int? CatID, int fcase)
        {
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[12];
                sqlParams[0] = new SqlParameter("@CurrentPage", objHrCommon.CurrentPage);
                sqlParams[1] = new SqlParameter("@PageSize", objHrCommon.PageSize);
                sqlParams[2] = new SqlParameter("@NoofRecords", System.Data.SqlDbType.Int);
                sqlParams[2].Direction = ParameterDirection.Output;
                sqlParams[3] = new SqlParameter("@NMRId", System.Data.SqlDbType.Int);
                if (objHrCommon.EmpID != 0)
                    sqlParams[3].Value = objHrCommon.EmpID;
                sqlParams[4] = new SqlParameter("@SiteID", System.Data.SqlDbType.Int);
                if (objHrCommon.SiteID != 0)
                    sqlParams[4].Value = objHrCommon.SiteID;
                sqlParams[5] = new SqlParameter("@DeptID", System.Data.SqlDbType.Int);
                if (objHrCommon.DeptID != 0)
                    sqlParams[5].Value = objHrCommon.DeptID;
                sqlParams[6] = new SqlParameter("@NMRName", objHrCommon.FName);
                sqlParams[7] = new SqlParameter("@Status", objHrCommon.Status);
                sqlParams[8] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
                sqlParams[8].Direction = ParameterDirection.ReturnValue;
                sqlParams[9] = new SqlParameter("@DesigID", DesigID);
                sqlParams[10] = new SqlParameter("@CatID", CatID);
                sqlParams[11] = new SqlParameter("@case", fcase);
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_SEARCH_NMRByPage", sqlParams);
                objHrCommon.NoofRecords = (int)sqlParams[2].Value;
                objHrCommon.TotalPages = (int)sqlParams[8].Value;
                return ds;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public void UpdateNMR(HRCommon objHrCommon, int nmrid, int Emmmployeeid)
        {
            try
            {
                SqlParameter[] objParam = new SqlParameter[11];
                objParam[1] = new SqlParameter("@Status", objHrCommon.Status);
                objParam[0] = new SqlParameter("@NMRId", System.Data.SqlDbType.Int);
                if (objHrCommon.EmpID != 0)
                {
                    objParam[0].Value = nmrid;
                }
                objParam[2] = new SqlParameter("@SiteID", System.Data.SqlDbType.Int);
                objParam[2].Value = objHrCommon.SiteID;
                objParam[3] = new SqlParameter("@DeptID", System.Data.SqlDbType.Int);
                objParam[3].Value = objHrCommon.DeptID;
                objParam[4] = new SqlParameter("@NMRName", objHrCommon.FName);
                objParam[5] = new SqlParameter("@CreatedBy", objHrCommon.UserID);
                objParam[6] = new SqlParameter("@Contact", objHrCommon.Mobile);
                objParam[7] = new SqlParameter("@Address", objHrCommon.Address);
                objParam[8] = new SqlParameter("@CateID", objHrCommon.TradeID);
                objParam[9] = new SqlParameter("@DesigID", objHrCommon.DesigID);
                objParam[10] = new SqlParameter("@EmployeeID", Emmmployeeid);
                SQLDBUtil.ExecuteNonQuery("HR_SET_NMR", objParam);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void UpdateNMRStatus(HRCommon objHrCommon)
        {
            try
            {
                SqlParameter[] objParam = new SqlParameter[3];
                objParam[0] = new SqlParameter("@NMRId", objHrCommon.EmpID);
                objParam[1] = new SqlParameter("@Status", objHrCommon.Status);
                objParam[2] = new SqlParameter("@ModifiedBy", objHrCommon.UserID);
                SQLDBUtil.ExecuteNonQuery("HR_SETSTATUS_NMR", objParam);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet GetEmployeesListBysiteAndDept(int SiteID, int DeptID)
        {
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[2];
                sqlParams[0] = new SqlParameter("@SiteID", SiteID);
                sqlParams[1] = new SqlParameter("@DeptID", DeptID);
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_Get_EmployeesListBySiteAndDept", sqlParams);
                return ds;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public DataSet SearchContactsList(HRCommon objHrCommon)
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
                sqlParams[4] = new SqlParameter("@CID", objHrCommon.CID);
                sqlParams[5] = new SqlParameter("@RefName", objHrCommon.RepName);
                sqlParams[6] = new SqlParameter("@GID", objHrCommon.GID);
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_SearchContactsByPage", sqlParams);
                objHrCommon.NoofRecords = (int)sqlParams[3].Value;
                objHrCommon.TotalPages = (int)sqlParams[2].Value;
                return ds;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public DataSet GetEmployeeList()
        {
            try
            {
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_Get_Employees");
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet GetRoles()
        {
            try
            {
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_Get_Roles");
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int DeleteDeptHead(int WSId, int DeptID, int EmpId, int UserID)
        {
            try
            {
                SqlParameter[] objParam = new SqlParameter[5];
                objParam[0] = new SqlParameter("@PrjID", WSId);
                objParam[1] = new SqlParameter("@DeptID", DeptID);
                objParam[2] = new SqlParameter("@EmpID", EmpId);
                objParam[4] = new SqlParameter("@UserID", UserID);
                objParam[3] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
                objParam[3].Direction = ParameterDirection.ReturnValue;
                SQLDBUtil.ExecuteNonQuery("HR_DeleteDeptHead", objParam);
                return Convert.ToInt32(objParam[3].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int PerminantDeleteDeptHead(int WSId, int DeptID, int EmpId)
        {
            try
            {
                SqlParameter[] objParam = new SqlParameter[4];
                objParam[0] = new SqlParameter("@PrjID", WSId);
                objParam[1] = new SqlParameter("@DeptID", DeptID);
                objParam[2] = new SqlParameter("@EmpID", EmpId);
                objParam[3] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
                objParam[3].Direction = ParameterDirection.ReturnValue;
                SQLDBUtil.ExecuteNonQuery("HR_PerDeleteDeptHead", objParam);
                return Convert.ToInt32(objParam[3].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void insertAttendance(int empID, int status, DateTime date)
        {
            try
            {
                SqlParameter[] objParam = new SqlParameter[3];
                objParam[0] = new SqlParameter("@empID", empID);
                objParam[1] = new SqlParameter("@status", status);
                objParam[2] = new SqlParameter("@date", date);
                SQLDBUtil.ExecuteNonQuery("hr_insertAtt", objParam);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet GetAppStatus(int EmpId)
        {
            try
            {
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetJobDetails", new SqlParameter[] { new SqlParameter("@EmpId", EmpId) });
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int UpdateAppStatus(int EmpId, int status)
        {
            try
            {
                SqlParameter[] objParam = new SqlParameter[3];
                objParam[0] = new SqlParameter("@EmpId", EmpId);
                objParam[1] = new SqlParameter("@status", status);
                objParam[2] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
                objParam[2].Direction = ParameterDirection.ReturnValue;
                SQLDBUtil.ExecuteNonQuery("HR_UpdateAppStatus", objParam);
                return Convert.ToInt32(objParam[2].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int AddAppDetails(int EmpId, string Address, string City, string State, String Country, String Phone, String PIN, bool SameAddress, string PerAddress, string PerCity, string PerState, String PerCountry, String PerPhone, String PerPIN, string Bloodgroup, int Salary, DateTime DOJ)
        {
            try
            {
                SqlParameter[] objParam = new SqlParameter[18];
                objParam[0] = new SqlParameter("@EmpId", EmpId);
                objParam[1] = new SqlParameter("@ResAddress", Address);
                objParam[2] = new SqlParameter("@ResCity", City);
                objParam[4] = new SqlParameter("@ResState", State);
                objParam[5] = new SqlParameter("@ResCountry", Country);
                objParam[6] = new SqlParameter("@ResPhone", Phone);
                objParam[3] = new SqlParameter("@ResPin", PIN);
                if (SameAddress)
                {
                    objParam[7] = new SqlParameter("@SameAdd", 1);
                }
                else
                {
                    objParam[7] = new SqlParameter("@SameAdd", 0);
                }
                objParam[8] = new SqlParameter("@PerAddress", PerAddress);
                objParam[9] = new SqlParameter("@PerCity", PerCity);
                objParam[10] = new SqlParameter("@PerState", PerState);
                objParam[11] = new SqlParameter("@PerCountry", PerCountry);
                objParam[12] = new SqlParameter("@PerPhone", PerPhone);
                objParam[13] = new SqlParameter("@PerPin", PerPIN);
                objParam[14] = new SqlParameter("@Bloodgroup", Bloodgroup);
                objParam[15] = new SqlParameter("@Salary", Salary);
                objParam[16] = new SqlParameter("@DOJ", DOJ);
                objParam[17] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
                objParam[17].Direction = ParameterDirection.ReturnValue;
                SQLDBUtil.ExecuteNonQuery("HR_AddAppDetails", objParam);
                return Convert.ToInt32(objParam[17].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int AddApplicantDetails(int EmpId, string Address, int City, int State, int Country, String Phone, String PIN,
            bool SameAddress, string PerAddress, int PerCity, int PerState, int PerCountry, String PerPhone, String PerPIN,
            string Bloodgroup, int Salary, DateTime DOJ, string PerDoorNo, string PerBuilding, string PerStreet, string PerArea,
            string ResDoorNo, string ResBuilding, string ResStreet, string ResArea, int Contract_Yr, int Contract_Month, int Contract_days, DateTime EOC_date)
        {
            try
            {
                SqlParameter[] objParam = new SqlParameter[30];
                objParam[0] = new SqlParameter("@EmpId", EmpId);
                objParam[1] = new SqlParameter("@ResAddress", Address);
                objParam[2] = new SqlParameter("@ResCity", City);
                objParam[4] = new SqlParameter("@ResState", State);
                objParam[5] = new SqlParameter("@ResCountry", Country);
                objParam[6] = new SqlParameter("@ResPhone", Phone);
                objParam[3] = new SqlParameter("@ResPin", PIN);
                if (SameAddress)
                {
                    objParam[7] = new SqlParameter("@SameAdd", 1);
                }
                else
                {
                    objParam[7] = new SqlParameter("@SameAdd", 0);
                }
                objParam[8] = new SqlParameter("@PerAddress", PerAddress);
                objParam[9] = new SqlParameter("@PerCity", PerCity);
                objParam[10] = new SqlParameter("@PerState", PerState);
                objParam[11] = new SqlParameter("@PerCountry", PerCountry);
                objParam[12] = new SqlParameter("@PerPhone", PerPhone);
                objParam[13] = new SqlParameter("@PerPin", PerPIN);
                objParam[14] = new SqlParameter("@Bloodgroup", Bloodgroup);
                objParam[15] = new SqlParameter("@Salary", Salary);
                objParam[16] = new SqlParameter("@DOJ", DOJ);
                objParam[17] = new SqlParameter("@PerDoorNo", PerDoorNo);
                objParam[18] = new SqlParameter("@PerBuilding", PerBuilding);
                objParam[19] = new SqlParameter("@PerStreet", PerStreet);
                objParam[20] = new SqlParameter("@PerArea", PerArea);
                objParam[21] = new SqlParameter("@ResDoorNo", ResDoorNo);
                objParam[22] = new SqlParameter("@ResBuilding", ResBuilding);
                objParam[23] = new SqlParameter("@ResStreet", ResStreet);
                objParam[24] = new SqlParameter("@ResArea", ResArea);
                objParam[25] = new SqlParameter("@Contract_Yr", Contract_Yr);
                objParam[26] = new SqlParameter("@Contract_Month", Contract_Month);
                objParam[27] = new SqlParameter("@Contract_days", Contract_days);
                DateTime date2 = new DateTime(0001, 01, 01, 0, 0, 0);
                int result = DateTime.Compare(EOC_date, date2);
                if (result == 0)
                    objParam[28] = new SqlParameter("@EOC_date", System.Data.SqlDbType.DateTime);
                else
                    objParam[28] = new SqlParameter("@EOC_date", EOC_date);
                objParam[29] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
                objParam[29].Direction = ParameterDirection.ReturnValue;
                SQLDBUtil.ExecuteNonQuery("HR_AddApplicantDetails", objParam);
                return Convert.ToInt32(objParam[29].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int UpdateAppDetails(int EmpId, string Address, string City, string State, String Country, String Phone, String PIN, bool SameAddress, string PerAddress, string PerCity, string PerState, String PerCountry, String PerPhone, String PerPIN, string Bloodgroup, int Salary, DateTime DOJ)
        {
            try
            {
                SqlParameter[] objParam = new SqlParameter[18];
                objParam[0] = new SqlParameter("@EmpId", EmpId);
                objParam[1] = new SqlParameter("@ResAddress", Address);
                objParam[2] = new SqlParameter("@ResCity", City);
                objParam[4] = new SqlParameter("@ResState", State);
                objParam[5] = new SqlParameter("@ResCountry", Country);
                objParam[6] = new SqlParameter("@ResPhone", Phone);
                objParam[3] = new SqlParameter("@ResPin", PIN);
                if (SameAddress)
                {
                    objParam[7] = new SqlParameter("@SameAdd", 1);
                }
                else
                {
                    objParam[7] = new SqlParameter("@SameAdd", 0);
                }
                objParam[8] = new SqlParameter("@PerAddress", PerAddress);
                objParam[9] = new SqlParameter("@PerCity", PerCity);
                objParam[10] = new SqlParameter("@PerState", PerState);
                objParam[11] = new SqlParameter("@PerCountry", PerCountry);
                objParam[12] = new SqlParameter("@PerPhone", PerPhone);
                objParam[13] = new SqlParameter("@PerPin", PerPIN);
                objParam[14] = new SqlParameter("@Bloodgroup", Bloodgroup);
                objParam[15] = new SqlParameter("@Salary", Salary);
                objParam[16] = new SqlParameter("@DOJ", DOJ);
                objParam[17] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
                objParam[17].Direction = ParameterDirection.ReturnValue;
                SQLDBUtil.ExecuteNonQuery("HR_UpdateAppDetails", objParam);
                return Convert.ToInt32(objParam[17].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int UpdateApplicantDetails(int EmpId, string Address, int City, int State, int Country, String Phone, String PIN,
            bool SameAddress, string PerAddress, int PerCity, int PerState, int PerCountry, String PerPhone, String PerPIN,
            string Bloodgroup, int Salary, DateTime DOJ, string PerDoorNo, string PerBuilding, string PerStreet, string PerArea,
            string ResDoorNo, string ResBuilding, string ResStreet, string ResArea, int Contract_Yr, int Contract_Month, int Contract_days, DateTime EOC_date)
        {
            try
            {
                SqlParameter[] objParam = new SqlParameter[30];
                objParam[0] = new SqlParameter("@EmpId", EmpId);
                objParam[1] = new SqlParameter("@ResAddress", Address);
                objParam[2] = new SqlParameter("@ResCity", City);
                objParam[4] = new SqlParameter("@ResState", State);
                objParam[5] = new SqlParameter("@ResCountry", Country);
                objParam[6] = new SqlParameter("@ResPhone", Phone);
                objParam[3] = new SqlParameter("@ResPin", PIN);
                if (SameAddress)
                {
                    objParam[7] = new SqlParameter("@SameAdd", 1);
                }
                else
                {
                    objParam[7] = new SqlParameter("@SameAdd", 0);
                }
                objParam[8] = new SqlParameter("@PerAddress", PerAddress);
                objParam[9] = new SqlParameter("@PerCity", PerCity);
                objParam[10] = new SqlParameter("@PerState", PerState);
                objParam[11] = new SqlParameter("@PerCountry", PerCountry);
                objParam[12] = new SqlParameter("@PerPhone", PerPhone);
                objParam[13] = new SqlParameter("@PerPin", PerPIN);
                objParam[14] = new SqlParameter("@Bloodgroup", Bloodgroup);
                objParam[15] = new SqlParameter("@Salary", Salary);
                objParam[16] = new SqlParameter("@DOJ", DOJ);
                objParam[17] = new SqlParameter("@PerDoorNo", PerDoorNo);
                objParam[18] = new SqlParameter("@PerBuilding", PerBuilding);
                objParam[19] = new SqlParameter("@PerStreet", PerStreet);
                objParam[20] = new SqlParameter("@PerArea", PerArea);
                objParam[21] = new SqlParameter("@ResDoorNo", ResDoorNo);
                objParam[22] = new SqlParameter("@ResBuilding", ResBuilding);
                objParam[23] = new SqlParameter("@ResStreet", ResStreet);
                objParam[24] = new SqlParameter("@ResArea", ResArea);
                objParam[25] = new SqlParameter("@Contract_Yr", Contract_Yr);
                objParam[26] = new SqlParameter("@Contract_Month", Contract_Month);
                objParam[27] = new SqlParameter("@Contract_days", Contract_days);
                DateTime date2 = new DateTime(0001, 01, 01, 0, 0, 0);
                int result = DateTime.Compare(EOC_date, date2);
                if (result == 0)
                    objParam[28] = new SqlParameter("@EOC_date", System.Data.SqlDbType.DateTime);
                else
                    objParam[28] = new SqlParameter("@EOC_date", EOC_date);
                objParam[29] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
                objParam[29].Direction = ParameterDirection.ReturnValue;
                SQLDBUtil.ExecuteNonQuery("HR_UpdateApplicantDetails", objParam);
                return Convert.ToInt32(objParam[29].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int AddUpdateEmpDocs(int empID, int DocID, int type, string value, int project, string designation, DateTime doj, int salary, DateTime IssueDate)
        {
            try
            {
                SqlParameter[] objParam = new SqlParameter[10];
                objParam[0] = new SqlParameter("@EmpId", empID);
                objParam[1] = new SqlParameter("@DocId", DocID);
                objParam[2] = new SqlParameter("@Type", type);
                objParam[4] = new SqlParameter("@Value", value);
                objParam[5] = new SqlParameter("@Project", project);
                objParam[6] = new SqlParameter("@desig", designation);
                objParam[3] = new SqlParameter("@Doj", doj);
                objParam[7] = new SqlParameter("@salary", salary);
                objParam[8] = new SqlParameter("@IssueDate", IssueDate);
                objParam[9] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
                objParam[9].Direction = ParameterDirection.ReturnValue;
                SQLDBUtil.ExecuteNonQuery("HR_AddUpdateEmpDocs", objParam);
                return Convert.ToInt32(objParam[9].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static int AddUpdateEmpDocsGeneral(int empID, int DocID, int type, string value, DateTime IssueDate, string DocName, int EmpDocID)
        {
            try
            {
                SqlParameter[] objParam = new SqlParameter[8];
                objParam[0] = new SqlParameter("@EmpId", empID);
                objParam[1] = new SqlParameter("@DocId", DocID);
                objParam[2] = new SqlParameter("@Type", type);
                objParam[3] = new SqlParameter("@Value", value);
                objParam[4] = new SqlParameter("@IssueDate", IssueDate);
                objParam[5] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
                objParam[5].Direction = ParameterDirection.ReturnValue;
                objParam[6] = new SqlParameter("@DocName", DocName);
                objParam[7] = new SqlParameter("@EmpDocID", EmpDocID);
                SQLDBUtil.ExecuteNonQuery("HR_AddUpdateEmpDocsGeneral", objParam);
                return Convert.ToInt32(objParam[5].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int InsertFullAtt(int empID, int status, DateTime date, string InTime, string OutTime, string Remarks, int SiteID, int UserID)
        {
            try
            {
                SqlParameter[] objParam = new SqlParameter[9];
                objParam[0] = new SqlParameter("@empID", empID);
                objParam[1] = new SqlParameter("@status", status);
                objParam[2] = new SqlParameter("@date", date);
                objParam[3] = new SqlParameter("@InTime", InTime);
                objParam[4] = new SqlParameter("@OutTime", OutTime);
                objParam[5] = new SqlParameter("@Remarks", Remarks);
                objParam[6] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
                objParam[6].Direction = ParameterDirection.ReturnValue;
                objParam[7] = new SqlParameter("@SiteID", SiteID);
                objParam[8] = new SqlParameter("@UserID", UserID);
                SQLDBUtil.ExecuteNonQuery("hr_insertFullAtt", objParam);
                return Convert.ToInt32(objParam[6].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet HR_ShowAttandance(int EmpID)
        {
            try
            {
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_ShowAttandance", new SqlParameter[] { new SqlParameter("@EmpId", EmpID) });
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int UpdateFullAtt(int empID, int status, DateTime date, string InTime, string OutTime, string Remarks, int SiteID, int UserID)
        {
            try
            {
                SqlParameter[] objParam = new SqlParameter[9];
                objParam[0] = new SqlParameter("@empID", empID);
                objParam[1] = new SqlParameter("@status", status);
                objParam[2] = new SqlParameter("@date", date);
                objParam[3] = new SqlParameter("@InTime", InTime);
                objParam[4] = new SqlParameter("@OutTime", OutTime);
                objParam[5] = new SqlParameter("@Remarks", Remarks);
                objParam[6] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
                objParam[6].Direction = ParameterDirection.ReturnValue;
                objParam[7] = new SqlParameter("@SiteID", SiteID);
                objParam[8] = new SqlParameter("@UserID", UserID);
                SQLDBUtil.ExecuteNonQuery("hr_UpdateFullAtt", objParam);
                return Convert.ToInt32(objParam[6].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void hr_UpdateNMRFullAtt(int empID, int status, DateTime date, string InTime, string OutTime, string Remarks)
        {
            try
            {
                SqlParameter[] objParam = new SqlParameter[6];
                objParam[0] = new SqlParameter("@empID", empID);
                objParam[1] = new SqlParameter("@status", status);
                objParam[2] = new SqlParameter("@date", date);
                objParam[3] = new SqlParameter("@InTime", InTime);
                objParam[4] = new SqlParameter("@OutTime", OutTime);
                objParam[5] = new SqlParameter("@Remarks", Remarks);
                SQLDBUtil.ExecuteNonQuery("hr_UpdateNMRFullAtt", objParam);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int InsertNMRFullAtt(int empID, int status, DateTime date, string InTime, string OutTime, string Remarks)
        {
            try
            {
                SqlParameter[] objParam = new SqlParameter[7];
                objParam[0] = new SqlParameter("@NMRID", empID);
                objParam[1] = new SqlParameter("@status", status);
                objParam[2] = new SqlParameter("@date", date);
                objParam[3] = new SqlParameter("@InTime", InTime);
                objParam[4] = new SqlParameter("@OutTime", OutTime);
                objParam[5] = new SqlParameter("@Remarks", Remarks);
                objParam[6] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
                objParam[6].Direction = ParameterDirection.ReturnValue;
                SQLDBUtil.ExecuteNonQuery("hr_insertNMRFullAtt", objParam);
                return Convert.ToInt32(objParam[6].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet HMS_InsUpdateAttendanceXML(DataSet dsAttendance, int UserID)
        {
            try
            {
                return SQLDBUtil.ExecuteDataset("HMS_InsUpdateAttendanceXML", new SqlParameter[] { new SqlParameter("@Attendance", dsAttendance.GetXml()), new SqlParameter("@UserID", UserID) });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet HMS_InsUpdateAttendanceXMLFromDevice(DataSet dsAttendance, int UserID)
        {
            try
            {
                return SQLDBUtil.ExecuteDataset("HMS_InsUpdateAttendanceXMLFromDevice", new SqlParameter[] { new SqlParameter("@Attendance", dsAttendance.GetXml()), new SqlParameter("@UserID", UserID) });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet HR_GetUnOutPined(HRCommon objHrCommon, int CompanyID)
        {
            try
            {
                SqlParameter[] parm = new SqlParameter[5];
                parm[0] = new SqlParameter("@CurrentPage", objHrCommon.CurrentPage);
                parm[1] = new SqlParameter("@PageSize", objHrCommon.PageSize);
                parm[2] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
                parm[2].Direction = ParameterDirection.ReturnValue;
                parm[3] = new SqlParameter("@NoofRecords", System.Data.SqlDbType.Int);
                parm[3].Direction = ParameterDirection.Output;
                parm[4] = new SqlParameter("@CompanyID", CompanyID);
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetUnOutPined_paging", parm);
                objHrCommon.NoofRecords = (int)parm[3].Value;
                objHrCommon.TotalPages = (int)parm[2].Value;
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int AddPrjManager(int WSID, int EmpID, int UserID)
        {
            try
            {
                int result;
                SqlParameter[] sqlParams = new SqlParameter[4];
                sqlParams[0] = new SqlParameter("@WSID", WSID);
                sqlParams[2] = new SqlParameter("@EmpID", EmpID);
                sqlParams[3] = new SqlParameter("@UserID", UserID);
                sqlParams[1] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
                sqlParams[1].Direction = ParameterDirection.ReturnValue;
                SQLDBUtil.ExecuteNonQuery("HR_AddPrjManager", sqlParams);
                result = Convert.ToInt16(sqlParams[1].Value);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int UpdatePrjManager(int WSID, int EmpID, int UserID)
        {
            try
            {
                int result;
                SqlParameter[] sqlParams = new SqlParameter[4];
                sqlParams[0] = new SqlParameter("@PrjID", WSID);
                sqlParams[2] = new SqlParameter("@EmpID", EmpID);
                sqlParams[3] = new SqlParameter("@UserID", UserID);
                sqlParams[1] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
                sqlParams[1].Direction = ParameterDirection.ReturnValue;
                SQLDBUtil.ExecuteNonQuery("HR_UpdatePrjManager", sqlParams);
                result = Convert.ToInt16(sqlParams[1].Value);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int UpdateStatus(int WSID, int EmpID, int UserID)
        {
            try
            {
                int result;
                SqlParameter[] sqlParams = new SqlParameter[4];
                sqlParams[0] = new SqlParameter("@PrjID", WSID);
                sqlParams[2] = new SqlParameter("@EmpID", EmpID);
                sqlParams[3] = new SqlParameter("@UserID", UserID);
                sqlParams[1] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
                sqlParams[1].Direction = ParameterDirection.ReturnValue;
                SQLDBUtil.ExecuteNonQuery("HR_UpdatePrjManagerStatus", sqlParams);
                result = Convert.ToInt16(sqlParams[1].Value);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void UpdateEmployeeStatus(HRCommon obj)
        {
            try
            {
                int result;
                SqlParameter[] sqlParams = new SqlParameter[3];
                sqlParams[0] = new SqlParameter("@EmpID", obj.EmpID);
                sqlParams[1] = new SqlParameter("@Status", obj.CurrentStatus);
                sqlParams[2] = new SqlParameter("@UserID", obj.UserID);
                SQLDBUtil.ExecuteNonQuery("HR_UpdateEmployeeStatus", sqlParams);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static int UpdateWorkSite(int WSID, string WSName, char status, string address, int STATEID, int CompanyID)
        {
            try
            {
                SqlParameter[] objParam = new SqlParameter[7];
                objParam[0] = new SqlParameter("@PrjID", WSID);
                objParam[1] = new SqlParameter("@PrjName", WSName);
                objParam[2] = new SqlParameter("@status", status);
                objParam[4] = new SqlParameter("@address", address);
                objParam[3] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
                objParam[3].Direction = ParameterDirection.ReturnValue;
                objParam[5] = new SqlParameter("@STATEID", STATEID);
                objParam[6] = new SqlParameter("@CompanyID", CompanyID);
                SQLDBUtil.ExecuteNonQuery("HR_UpdateWorkSite", objParam);
                return Convert.ToInt32(objParam[3].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static int AddWorkSite(string Prj, string address, char status, int STATEID, int CompanyID)
        {
            try
            {
                int result;
                SqlParameter[] sqlParams = new SqlParameter[6];
                sqlParams[0] = new SqlParameter("@WorkSite", Prj);
                sqlParams[1] = new SqlParameter("@Address", address);
                sqlParams[2] = new SqlParameter("@status", status);
                sqlParams[4] = new SqlParameter("@STATEID", STATEID);
                sqlParams[3] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
                sqlParams[3].Direction = ParameterDirection.ReturnValue;
                sqlParams[5] = new SqlParameter("@CompanyID", CompanyID);
                SQLDBUtil.ExecuteNonQuery("HR_AddWorkSite", sqlParams);
                result = Convert.ToInt16(sqlParams[3].Value);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet AddClearence(int id, string ItemName, int DeptID)
        {
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[3];
                sqlParams[0] = new SqlParameter("@id", id);
                sqlParams[1] = new SqlParameter("@ItemName", ItemName);
                sqlParams[2] = new SqlParameter("@DeptID", DeptID);
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_Clearence", sqlParams);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet GetEmployeeClearenceDetails(int EmptyID)
        {
            try
            {
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetEmployeeClearenceDetails", new SqlParameter[] { new SqlParameter("@ID", EmptyID) });
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static int AddVacationSettlement(Double A1, Double A2, Double A3, Double A4, Double A5, Double A6, Double A7, Double D1, Double D2, Double D3, Double D4, Double D5, Double D6, Double D7, int Empid, int Companyid, string Remarks, Double Totalamt, string Ledger, int month, int year, string A6Remarks, string A7Remarks, string D6Remarks, string D7Remarks, double gratuity, string form)
        {
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[28];
                sqlParams[0] = new SqlParameter("@A1", A1);
                sqlParams[1] = new SqlParameter("@A2", A2);
                sqlParams[2] = new SqlParameter("@A3", A3);
                sqlParams[3] = new SqlParameter("@A4", A4);
                sqlParams[16] = new SqlParameter("@A5", A5);
                sqlParams[18] = new SqlParameter("@A6", A6);
                sqlParams[19] = new SqlParameter("@A7", A7);
                sqlParams[4] = new SqlParameter("@D1", D1);
                sqlParams[5] = new SqlParameter("@D2", D2);
                sqlParams[6] = new SqlParameter("@D3", D3);
                sqlParams[7] = new SqlParameter("@D4", D4);
                sqlParams[8] = new SqlParameter("@D5", D5);
                sqlParams[20] = new SqlParameter("@D6", D6);
                sqlParams[21] = new SqlParameter("@D7", D7);
                sqlParams[9] = new SqlParameter("@Empid", Empid);
                sqlParams[10] = new SqlParameter("@Companyid", Companyid);
                sqlParams[11] = new SqlParameter("@TotAmt", Totalamt);
                sqlParams[12] = new SqlParameter("@Ledger", Ledger);
                sqlParams[13] = new SqlParameter("@month", month);
                sqlParams[14] = new SqlParameter("@Year", year);
                sqlParams[15] = new SqlParameter("@Remarks", Remarks);
                sqlParams[17] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
                sqlParams[17].Direction = ParameterDirection.ReturnValue;
                sqlParams[22] = new SqlParameter("@A6Remarks", A6Remarks);
                sqlParams[23] = new SqlParameter("@A7Remarks", A7Remarks);
                sqlParams[24] = new SqlParameter("@D6Remarks", D6Remarks);
                sqlParams[25] = new SqlParameter("@D7Remarks", D7Remarks);
                sqlParams[26] = new SqlParameter("@Gratuity", gratuity);
                sqlParams[27] = new SqlParameter("@form", form);
                SQLDBUtil.ExecuteNonQuery("HMS_InsVacationSettlement", sqlParams);
                int id = (int)sqlParams[17].Value;
                return id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #region Contacts
        public int InsUpdContacts(HRCommon objHrCommon)
        {
            try
            {
                SqlParameter[] objParam = new SqlParameter[12];
                objParam[0] = new SqlParameter("@CID", objHrCommon.CID);
                objParam[1] = new SqlParameter("@ContactID", objHrCommon.ContactID);
                objParam[2] = new SqlParameter("@Reference", objHrCommon.Reference);
                objParam[3] = new SqlParameter("@RepName", objHrCommon.RepName);
                objParam[4] = new SqlParameter("@Phone1", objHrCommon.ConPhone1);
                objParam[5] = new SqlParameter("@Phone2", objHrCommon.ConPhone2);
                objParam[6] = new SqlParameter("@Phone3", objHrCommon.ConPhone3);
                objParam[7] = new SqlParameter("@Address", objHrCommon.ContacsAddress);
                objParam[8] = new SqlParameter("@Notes", objHrCommon.Notes);
                objParam[9] = new SqlParameter("@Others", objHrCommon.Others);
                objParam[11] = new SqlParameter("@UserID", objHrCommon.UserID);
                objParam[10] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
                objParam[10].Direction = ParameterDirection.ReturnValue;
                SQLDBUtil.ExecuteNonQuery("HR_InsUpdContacts", objParam);
                int Res = Convert.ToInt16(objParam[10].Value);
                return Res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet GetCategoriesList()
        {
            try
            {
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetCategoriesList");
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet GetContactsList()
        {
            try
            {
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetContactsList");
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet GetContactsDetails(int ContactId)
        {
            try
            {
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetContactDetails", new SqlParameter[] { new SqlParameter("@ContactID", ContactId) });
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void DeleteContact(int ContactID)
        {
            try
            {
                SqlParameter[] objParam = new SqlParameter[1];
                objParam[0] = new SqlParameter("@ContactID", ContactID);
                SQLDBUtil.ExecuteNonQuery("HR_DeleteContact", objParam);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion Contacts
        #region Rules&Responsibilities
        public int InsUpdResponsibilities(HRCommon objHrCommon)
        {
            try
            {
                int result;
                SqlParameter[] sqlParams = new SqlParameter[5];
                sqlParams[0] = new SqlParameter("@ResID", objHrCommon.ResponID);
                sqlParams[1] = new SqlParameter("@EmpID", objHrCommon.EmpID);
                sqlParams[2] = new SqlParameter("@Responsible", objHrCommon.Responsible);
                sqlParams[4] = new SqlParameter("@UserID", objHrCommon.UserID);
                sqlParams[3] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
                sqlParams[3].Direction = ParameterDirection.ReturnValue;
                SQLDBUtil.ExecuteNonQuery("HR_InsUpd_Responsiblties", sqlParams);
                result = Convert.ToInt16(sqlParams[3].Value);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int InsUpdTasks(HRCommon objHrCommon)
        {
            try
            {
                int result;
                SqlParameter[] sqlParams = new SqlParameter[7];
                sqlParams[0] = new SqlParameter("@TaskID", objHrCommon.TaskID);
                sqlParams[1] = new SqlParameter("@TaskName", objHrCommon.TaskName);
                sqlParams[2] = new SqlParameter("@EmpID", objHrCommon.EmpID);
                sqlParams[4] = new SqlParameter("@AssignedBy", objHrCommon.UserID);
                sqlParams[5] = new SqlParameter("@Status", objHrCommon.Status);
                sqlParams[6] = new SqlParameter("@DueDate", objHrCommon.DueDate);
                sqlParams[3] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
                sqlParams[3].Direction = ParameterDirection.ReturnValue;
                SQLDBUtil.ExecuteNonQuery("HR_InsUpd_Tasks", sqlParams);
                result = Convert.ToInt16(sqlParams[3].Value);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int InsUpdTodoListMasetr(HRCommon objHrCommon)
        {
            try
            {
                int result;
                SqlParameter[] sqlParams = new SqlParameter[5];
                sqlParams[0] = new SqlParameter("@ListID", objHrCommon.ListID);
                sqlParams[1] = new SqlParameter("@ListItem", objHrCommon.ListName);
                sqlParams[2] = new SqlParameter("@EmpID", objHrCommon.EmpID);
                sqlParams[4] = new SqlParameter("@Authority", objHrCommon.Authority);
                sqlParams[3] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
                sqlParams[3].Direction = ParameterDirection.ReturnValue;
                SQLDBUtil.ExecuteNonQuery("HR_InsUpd_TodoList_Master", sqlParams);
                result = Convert.ToInt16(sqlParams[3].Value);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int InsUpdCheckList(HRCommon objHrCommon)
        {
            try
            {
                int result;
                SqlParameter[] sqlParams = new SqlParameter[9];
                sqlParams[0] = new SqlParameter("@CheckListID", objHrCommon.ChkListID);
                sqlParams[1] = new SqlParameter("@TodoListID", objHrCommon.ListID);
                sqlParams[2] = new SqlParameter("@SiteID", objHrCommon.SiteID);
                sqlParams[4] = new SqlParameter("@Authority", objHrCommon.Authority);
                sqlParams[5] = new SqlParameter("@StartDate", objHrCommon.FromDate);
                sqlParams[6] = new SqlParameter("@EndDate", objHrCommon.ToDate);
                sqlParams[7] = new SqlParameter("@Status", objHrCommon.StatusID);
                sqlParams[8] = new SqlParameter("@EmpID", objHrCommon.EmpID);
                sqlParams[3] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
                sqlParams[3].Direction = ParameterDirection.ReturnValue;
                SQLDBUtil.ExecuteNonQuery("HR_InsUpd_CheckList", sqlParams);
                result = Convert.ToInt16(sqlParams[3].Value);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet GetTasksList(int EmpID)
        {
            try
            {
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_Get_TasksList", new SqlParameter[] { new SqlParameter("@EmpID", EmpID) });
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet GetTodoList()
        {
            try
            {
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_Get_TodoList_Master");
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet GetStatusList()
        {
            try
            {
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_Get_Status");
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet GetTodoListDetails(int ListID)
        {
            try
            {
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_Get_TodoListDetails_Master", new SqlParameter[] { new SqlParameter("@ListID", ListID) });
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet GetCheckList(int SiteID)
        {
            try
            {
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_Get_CheckList", new SqlParameter[] { new SqlParameter("@SiteID", SiteID) });
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet GetTasksDetails(int TaskID)
        {
            try
            {
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_Get_TasksDetails", new SqlParameter[] { new SqlParameter("@TaskID", TaskID) });
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void DeleteTask(int TaskID)
        {
            try
            {
                SqlParameter[] objParam = new SqlParameter[1];
                objParam[0] = new SqlParameter("@TaskID", TaskID);
                SQLDBUtil.ExecuteNonQuery("HR_Del_Tasks", objParam);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void DeleteTodoList(int TodoListID)
        {
            try
            {
                SqlParameter[] objParam = new SqlParameter[1];
                objParam[0] = new SqlParameter("@TodoListID", TodoListID);
                SQLDBUtil.ExecuteNonQuery("HR_Del_TodoList", objParam);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet GetResponsibility(int EmpID)
        {
            try
            {
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_Get_Responsibilityt", new SqlParameter[] { new SqlParameter("@EmpID", EmpID) });
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet ApplicantList(int Type)
        {
            try
            {
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetApplicantsByEntryType", new SqlParameter[] { new SqlParameter("@Status", Type) });
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion Rules&Responsibilities
        # region Documents
        public static int Ins_UpdDocument(HRCommon objHrCommon, int ModuleID, int ClassID, int CompanyID)
        {
            try
            {
                SqlParameter[] objParam = new SqlParameter[8];
                objParam[0] = new SqlParameter("@DocID", objHrCommon.DocID);
                objParam[1] = new SqlParameter("@DocName", objHrCommon.DocName);
                objParam[2] = new SqlParameter("@DocText", objHrCommon.DocText);
                objParam[4] = new SqlParameter("@UserID", objHrCommon.UserID);
                objParam[3] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
                objParam[3].Direction = ParameterDirection.ReturnValue;
                objParam[5] = new SqlParameter("@ModuleID", ModuleID);
                objParam[6] = new SqlParameter("@ClassID", ClassID);
                objParam[7] = new SqlParameter("@CompanyID", CompanyID);
                SQLDBUtil.ExecuteNonQuery("HR_InsUpd_Documents", objParam);
                return Convert.ToInt32(objParam[3].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet GetDocumentDetails(int DocID, int? EmpID)
        {
            try
            {
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetDocumentDetails", new SqlParameter[] { new SqlParameter("@DocID", DocID), new SqlParameter("@EmpID", EmpID) });
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet HR_GetDocumentDetailsByEmpDocID(int DocID, int EmpID, int EmpDocID)
        {
            try
            {
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetDocumentDetailsByEmpDocID", new SqlParameter[] { new SqlParameter("@DocID", DocID), new SqlParameter("@EmpID", EmpID), new SqlParameter("@EmpDocID", EmpDocID) });
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet GetDocumentsListByPaging(HRCommon objHrCommon, int ModuleID, string DocName, int? ClassID, int CompanyID, bool status,int Docid)
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
                sqlParams[4] = new SqlParameter("@ModuleID", ModuleID);
                sqlParams[5] = new SqlParameter("@Template", DocName);
                sqlParams[6] = new SqlParameter("@ClassID", ClassID);
                sqlParams[7] = new SqlParameter("@CompanyID", CompanyID);
                sqlParams[8] = new SqlParameter("@IsActive", status);
                sqlParams[9] = new SqlParameter("@DOCID", Docid);
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetDocumentsListBypaging", sqlParams);
                objHrCommon.NoofRecords = (int)sqlParams[3].Value;
                objHrCommon.TotalPages = (int)sqlParams[2].Value;
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet GetApplicantscount()
        {
            try
            {
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_Get_Applicantscount");
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static void UpdateDocStatus(int DocID, int Status)
        {
            try
            {
                SqlParameter[] objParam = new SqlParameter[2];
                objParam[0] = new SqlParameter("@DocID", DocID);
                objParam[1] = new SqlParameter("@Status", Status);
                SQLDBUtil.ExecuteNonQuery("HR_UpdateDocStatus", objParam);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region ForClientsOC
        public DataSet GetPrjManagersForClientsOC(int CompanyID)
        {
            try
            {
                SqlParameter[] parm = new SqlParameter[1];
                parm[0] = new SqlParameter("@CompanyID", CompanyID);
                DataSet ds = SQLDBUtil.ExecuteDataset("CMS_ClientsList", parm);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet GetofficesForClientOC(int Orgid)
        {
            try
            {
                DataSet ds = SQLDBUtil.ExecuteDataset("CMS_ClientOfficesList", new SqlParameter[] { new SqlParameter("@Orgid", Orgid) });
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet GetDeptHeadsForClientOC(int ClientID)
        {
            try
            {
                DataSet ds = SQLDBUtil.ExecuteDataset("[CMS_DeptHeadsForClientOC]", new SqlParameter[] { new SqlParameter("@ClientID", ClientID) });
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet GetHeadDetailsForClients(int ClientID, int DeptID, int EmpId, int officeID)
        {
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[4];
                sqlParams[0] = new SqlParameter("@ClientID", ClientID);
                sqlParams[1] = new SqlParameter("@DeptID", DeptID);
                sqlParams[2] = new SqlParameter("@EmpID", EmpId);
                sqlParams[3] = new SqlParameter("@officeID", officeID);
                DataSet ds = SQLDBUtil.ExecuteDataset("CMS_DeptHeadDetailsForClientsOC", sqlParams);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion ForClientsOC
        #region Airline
        public static int InsUpdAirlineNm(int ID, string Name, int Status, int userid)
        {
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[5];
                sqlParams[0] = new SqlParameter("@ID", ID);
                sqlParams[1] = new SqlParameter("@Name", Name);
                sqlParams[2] = new SqlParameter("@active", Status);
                sqlParams[3] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
                sqlParams[3].Direction = ParameterDirection.ReturnValue;
                sqlParams[4] = new SqlParameter("@userid", userid);
                SQLDBUtil.ExecuteNonQuery("T_HMS_InsUpd_Airline", sqlParams);
                return Convert.ToInt32(sqlParams[3].Value);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        #endregion Airline
        #region Desigination master
        public static int InsUpdDepartment(int DeptID, string DeptName, int Status)
        {
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[4];
                sqlParams[0] = new SqlParameter("@DeptID", DeptID);
                sqlParams[1] = new SqlParameter("@DeptName", DeptName);
                sqlParams[2] = new SqlParameter("@status", Status);
                sqlParams[3] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
                sqlParams[3].Direction = ParameterDirection.ReturnValue;
                SQLDBUtil.ExecuteNonQuery("HR_InsUpd_Department", sqlParams);
                return Convert.ToInt32(sqlParams[3].Value);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static int InsUpdBookingClass(int BookingClassID, string BookingClassName, int Status, int UserID)
        {
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[5];
                sqlParams[0] = new SqlParameter("@ID", BookingClassID);
                sqlParams[1] = new SqlParameter("@Name", BookingClassName);
                sqlParams[2] = new SqlParameter("@active", Status);
                sqlParams[3] = new SqlParameter("@userid", UserID);
                sqlParams[4] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
                sqlParams[4].Direction = ParameterDirection.ReturnValue;
                SQLDBUtil.ExecuteNonQuery("T_HMS_InsUpd_BookingClass", sqlParams);
                return Convert.ToInt32(sqlParams[4].Value);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static int InsUpdPassengerType(int PassengerTypeID, string PassengerTypeName, int Status, int UserID)
        {
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[5];
                sqlParams[0] = new SqlParameter("@ID", PassengerTypeID);
                sqlParams[1] = new SqlParameter("@Name", PassengerTypeName);
                sqlParams[2] = new SqlParameter("@active", Status);
                sqlParams[3] = new SqlParameter("@userid", UserID);
                sqlParams[4] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
                sqlParams[4].Direction = ParameterDirection.ReturnValue;
                SQLDBUtil.ExecuteNonQuery("T_HMS_InsUpd_PassengerType", sqlParams);
                return Convert.ToInt32(sqlParams[4].Value);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static int InsUpdRelationType(int RelationTypeID, string RelationTypeName, int Status, int UserID)
        {
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[5];
                sqlParams[0] = new SqlParameter("@ID", RelationTypeID);
                sqlParams[1] = new SqlParameter("@Name", RelationTypeName);
                sqlParams[2] = new SqlParameter("@active", Status);
                sqlParams[3] = new SqlParameter("@userid", UserID);
                sqlParams[4] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
                sqlParams[4].Direction = ParameterDirection.ReturnValue;
                SQLDBUtil.ExecuteNonQuery("T_HMS_InsUpd_Relation", sqlParams);
                return Convert.ToInt32(sqlParams[4].Value);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static int InsUpdDesigination(int DesigId, string Name)
        {
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[3];
                sqlParams[0] = new SqlParameter("@DesigId", DesigId);
                sqlParams[1] = new SqlParameter("@Name", Name);
                sqlParams[2] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
                sqlParams[2].Direction = ParameterDirection.ReturnValue;
                SQLDBUtil.ExecuteNonQuery("HR_InsUpd_DesignationMaster", sqlParams);
                return Convert.ToInt32(sqlParams[2].Value);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        #region AirLInes by kaushal
        public static int T_HMS_CRUD_ConfigAirTicket(int id, int from_cityid, int to_cityid, int airlineID, int PassengertypeID, int bookingClassID, decimal fare_rate, int userid, int status)
        {
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[10];
                sqlParams[0] = new SqlParameter("@ID", id);
                sqlParams[1] = new SqlParameter("@from_CityID", from_cityid);
                sqlParams[2] = new SqlParameter("@To_CityID", to_cityid);
                sqlParams[3] = new SqlParameter("@AirlineID", airlineID);
                sqlParams[4] = new SqlParameter("@Passenger_typeID", PassengertypeID);
                sqlParams[5] = new SqlParameter("@Booking_ClassID", bookingClassID);
                sqlParams[6] = new SqlParameter("@Fare_rate", fare_rate);
                sqlParams[7] = new SqlParameter("@userID", userid);
                sqlParams[8] = new SqlParameter("@active", status);
                sqlParams[9] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
                sqlParams[9].Direction = ParameterDirection.ReturnValue;
                SQLDBUtil.ExecuteNonQuery("T_HMS_CRUD_ConfigAirTicket", sqlParams);
                return Convert.ToInt32(sqlParams[9].Value);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static int T_HMS_empVsAirTicketsAuth_Ins_upd(int id, int empid, int relationID, int PassengertypeID, int bookingClassID, int from_cityid, int to_cityid, int FrequencyID, int userid, int ApprovedBY, int status, decimal Tickets)
        {
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[13];
                sqlParams[0] = new SqlParameter("@ID", id);
                sqlParams[1] = new SqlParameter("@empid", empid);
                sqlParams[2] = new SqlParameter("@relationID", relationID);
                sqlParams[3] = new SqlParameter("@PassengerTypeID", PassengertypeID);
                sqlParams[4] = new SqlParameter("@bookingClassID", bookingClassID);
                sqlParams[5] = new SqlParameter("@from_cityID", from_cityid);
                sqlParams[6] = new SqlParameter("@TO_cityID", to_cityid);
                sqlParams[7] = new SqlParameter("@FrequencyID", FrequencyID);
                sqlParams[8] = new SqlParameter("@created_by", userid);
                sqlParams[9] = new SqlParameter("@ApprovedBy", ApprovedBY);
                sqlParams[10] = new SqlParameter("@acitve", status);
                sqlParams[11] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
                sqlParams[11].Direction = ParameterDirection.ReturnValue;
                sqlParams[12] = new SqlParameter("@Tickets", Tickets);
                SQLDBUtil.ExecuteNonQuery("T_HMS_empVsAirTicketsAuth_Ins_upd", sqlParams);
                return Convert.ToInt32(sqlParams[11].Value);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static int Ins_Upd_T_HMS_vacationsattlement(int id, int empid, DateTime ReJoin_Date, decimal CTC, DateTime LOS_date, DateTime DOS_date, int DW, int DD, int DEff, int TotalService, decimal ALGross, int Attendance, string OT_hr, string A1, string A2, string A3, string A4, string A5, string A6, string A7, string D1, string D2, string D3, string D4, string D5, string D6, string D7, string NetAmt, int userid)
        {
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[30];
                sqlParams[0] = new SqlParameter("@ID", id);
                sqlParams[1] = new SqlParameter("@Emp_id", empid);
                sqlParams[2] = new SqlParameter("@ReJoin_Date", ReJoin_Date);
                sqlParams[3] = new SqlParameter("@CTC", CTC);
                sqlParams[4] = new SqlParameter("@LOS_date", LOS_date);
                sqlParams[5] = new SqlParameter("@DOS_date", DOS_date);
                sqlParams[6] = new SqlParameter("@DW", DW);
                sqlParams[7] = new SqlParameter("@DD", DD);
                sqlParams[8] = new SqlParameter("@DEff", DEff);
                sqlParams[9] = new SqlParameter("@TotalService", TotalService);
                sqlParams[10] = new SqlParameter("@ALGross", ALGross);
                sqlParams[11] = new SqlParameter("@Attendance", Attendance);
                sqlParams[12] = new SqlParameter("@OT_hr", OT_hr);
                sqlParams[13] = new SqlParameter("@A1", A1);
                sqlParams[14] = new SqlParameter("@A2", A2);
                sqlParams[15] = new SqlParameter("@A3", A3);
                sqlParams[16] = new SqlParameter("@A4", A4);
                sqlParams[17] = new SqlParameter("@A5", A5);
                sqlParams[18] = new SqlParameter("@A6", A6);
                sqlParams[19] = new SqlParameter("@A7", A7);
                sqlParams[20] = new SqlParameter("@D1", D1);
                sqlParams[21] = new SqlParameter("@D2", D2);
                sqlParams[22] = new SqlParameter("@D3", D3);
                sqlParams[23] = new SqlParameter("@D4", D4);
                sqlParams[24] = new SqlParameter("@D5", D5);
                sqlParams[25] = new SqlParameter("@D6", D6);
                sqlParams[26] = new SqlParameter("@D7", D7);
                sqlParams[27] = new SqlParameter("@Net_Amt", NetAmt);
                sqlParams[28] = new SqlParameter("@userid", userid);
                sqlParams[29] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
                sqlParams[29].Direction = ParameterDirection.ReturnValue;
                SQLDBUtil.ExecuteNonQuery("Ins_Upd_T_HMS_vacationsattlement", sqlParams);
                return Convert.ToInt32(sqlParams[29].Value);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static int T_HMS_InsUpd_AbsentPenalties(int id, int noOfdays, int Occurnace, int penalities, int userid, int status)
        {
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[7];
                sqlParams[0] = new SqlParameter("@ID", id);
                sqlParams[1] = new SqlParameter("@noOfDays", noOfdays);
                sqlParams[2] = new SqlParameter("@occurance", Occurnace);
                sqlParams[3] = new SqlParameter("@penality", penalities);
                sqlParams[4] = new SqlParameter("@userID", userid);
                sqlParams[5] = new SqlParameter("@active", status);
                sqlParams[6] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
                sqlParams[6].Direction = ParameterDirection.ReturnValue;
                SQLDBUtil.ExecuteNonQuery("T_HMS_InsUpd_AbsentPenalties", sqlParams);
                return Convert.ToInt32(sqlParams[6].Value);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static int T_HMS_AbsentPenalties_Actions(int id, string proc_name)
        {
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[1];
                sqlParams[0] = new SqlParameter("@ID", id);
                SQLDBUtil.ExecuteNonQuery(proc_name, sqlParams);
                return 1;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static DataSet T_HMS_AbsentPenalties_GetList(int id, string proc_name)
        {
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[1];
                sqlParams[0] = new SqlParameter("@ID", id);
                DataSet ds = SQLDBUtil.ExecuteDataset(proc_name, sqlParams);
                return ds;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static DataSet T_HMS_Get_EmptListofAbsen(int Month, int Year, int DepID, int WSiteID, string proc_name)
        {
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[4];
                sqlParams[0] = new SqlParameter("@Month", Month);
                sqlParams[1] = new SqlParameter("@Year", Year);
                if (DepID != 0)
                {
                    sqlParams[2] = new SqlParameter("@deptID", DepID);
                }
                else
                {
                    sqlParams[2] = new SqlParameter("@deptID", SqlDbType.Int);
                }
                if (WSiteID != 0)
                {
                    sqlParams[3] = new SqlParameter("@worksiteId", WSiteID);
                }
                else
                {
                    sqlParams[3] = new SqlParameter("@worksiteId", SqlDbType.Int);
                }
                DataSet ds = SQLDBUtil.ExecuteDataset(proc_name, sqlParams);
                return ds;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        #endregion
        public static int InsUpdOT_Vars(int Id, string Name, string val)
        {
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[4];
                sqlParams[0] = new SqlParameter("@Id", Id);
                sqlParams[1] = new SqlParameter("@Name", Name);
                sqlParams[2] = new SqlParameter("@val", val);
                sqlParams[3] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
                sqlParams[3].Direction = ParameterDirection.ReturnValue;
                SQLDBUtil.ExecuteNonQuery("HR_InsUpd_OT_Vars", sqlParams);
                return Convert.ToInt32(sqlParams[2].Value);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static DataSet GetDesignationsList()
        {
            DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetDesignations");
            return ds;
        }
        public static DataSet GetDesignationsDetails(int DesigId)
        {
            try
            {
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetDesignationsDetails", new SqlParameter[] { new SqlParameter("@DesigId", DesigId) });
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region OT
        public static DataSet GetOT_VarDetails(int Id)
        {
            try
            {
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_Get_OTvarsDetails", new SqlParameter[] { new SqlParameter("@Id", Id) });
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region employeetype
        public static DataSet GetEmployeeTypesList()
        {
            DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetEmployeeTypes");
            return ds;
        }
        public static DataSet GetEmployeeTypesListDetails(int EmptyID)
        {
            try
            {
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetEmployeeTypesDetails", new SqlParameter[] { new SqlParameter("@EmptyID", EmptyID) });
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Categorie master
        public static int InsUpdCategories(int CateId, string Name)
        {
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[3];
                sqlParams[0] = new SqlParameter("@CateId", CateId);
                sqlParams[1] = new SqlParameter("@Name", Name);
                sqlParams[2] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
                sqlParams[2].Direction = ParameterDirection.ReturnValue;
                SQLDBUtil.ExecuteNonQuery("HR_InsUpd_CategoryMaster", sqlParams);
                return Convert.ToInt32(sqlParams[2].Value);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static DataSet GetCategoriesLists()
        {
            return SQLDBUtil.ExecuteDataset("HR_GetCategories");
        }
        public static DataSet GetCategoriesDetails(int CateId)
        {
            try
            {
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetCategoriesDetails", new SqlParameter[] { new SqlParameter("@CateId", CateId) });
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        public DataSet HR_GetAllottedMobileLimitAmounts(HRCommon objHrCommon, int CompanyID)
        {
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[8];
                sqlParams[0] = new SqlParameter("@CurrentPage", objHrCommon.CurrentPage);
                sqlParams[1] = new SqlParameter("@PageSize", objHrCommon.PageSize);
                sqlParams[2] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
                sqlParams[2].Direction = ParameterDirection.ReturnValue;
                sqlParams[3] = new SqlParameter("@NoofRecords", System.Data.SqlDbType.Int);
                sqlParams[3].Direction = ParameterDirection.Output;
                sqlParams[4] = new SqlParameter("@SiteID", objHrCommon.SiteID);
                sqlParams[5] = new SqlParameter("@DeptID", objHrCommon.DeptID);
                sqlParams[6] = new SqlParameter("@EmpName", objHrCommon.FName);
                sqlParams[7] = new SqlParameter("@CompanyID", CompanyID);
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetAllottedMobileLimitAmounts", sqlParams);
                objHrCommon.NoofRecords = (int)sqlParams[3].Value;
                objHrCommon.TotalPages = (int)sqlParams[2].Value;
                return ds;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static DataSet HR_GetMobilesWS_DeptFilter(int WS)
        {
            try
            {
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetMobilesWS_DeptFilter", new SqlParameter[] { new SqlParameter("@WS", WS) });
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet GetEmpSalriesList(HRCommon objHrCommon)
        {
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[8];
                sqlParams[0] = new SqlParameter("@CurrentPage", objHrCommon.CurrentPage);
                sqlParams[1] = new SqlParameter("@PageSize", objHrCommon.PageSize);
                sqlParams[2] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
                sqlParams[2].Direction = ParameterDirection.ReturnValue;
                sqlParams[3] = new SqlParameter("@NoofRecords", System.Data.SqlDbType.Int);
                sqlParams[3].Direction = ParameterDirection.Output;
                sqlParams[4] = new SqlParameter("@SiteID", objHrCommon.SiteID);
                sqlParams[5] = new SqlParameter("@DeptID", objHrCommon.DeptID);
                sqlParams[6] = new SqlParameter("@EmpName", objHrCommon.FName);
                sqlParams[7] = new SqlParameter("@EmpStatus", objHrCommon.CurrentStatus);
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_SearchEmpListForPayslip", sqlParams);
                objHrCommon.NoofRecords = (int)sqlParams[3].Value;
                objHrCommon.TotalPages = (int)sqlParams[2].Value;
                return ds;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public DataSet GetEmployeesByPageMobileBillReport(HRCommon objHrCommon, int Month, int Year)
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
                sqlParams[4] = new SqlParameter("@SiteID", objHrCommon.SiteID);
                sqlParams[5] = new SqlParameter("@DeptID", objHrCommon.DeptID);
                sqlParams[6] = new SqlParameter("@EmpName", objHrCommon.FName);
                sqlParams[7] = new SqlParameter("@EmpStatus", objHrCommon.CurrentStatus);
                sqlParams[8] = new SqlParameter("@Month", Month);
                sqlParams[9] = new SqlParameter("@Year", Year);
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_SearchEmpListByPageForMobileBillRPT", sqlParams);
                objHrCommon.NoofRecords = (int)sqlParams[3].Value;
                objHrCommon.TotalPages = (int)sqlParams[2].Value;
                return ds;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public void UpdateAppIDStaus(int AppID, int Status)
        {
            try
            {
                SqlParameter[] objParam = new SqlParameter[2];
                objParam[0] = new SqlParameter("@AppID", AppID);
                objParam[1] = new SqlParameter("@Status", Status);
                SQLDBUtil.ExecuteNonQuery("HR_UpdateStatusT_G_Applications", objParam);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet GetUcMenu(int ModuleId, int RoleId)
        {
            try
            {
                DataSet ds = SQLDBUtil.ExecuteDataset("CP_GetModuleMenu", new SqlParameter[] { new SqlParameter("@ModuleId", ModuleId), new SqlParameter("@RoleId", RoleId) });
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet GetLinks(int MenuId, int RoleID)
        {
            try
            {
                SqlParameter[] p = new SqlParameter[2];
                p[0] = new SqlParameter("@MenuID", MenuId);
                p[1] = new SqlParameter("@RoleID", RoleID);
                DataSet ds = SQLDBUtil.ExecuteDataset("GetLinks", p);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet GetAllowed(int RoleId, int ModuleId, string URL)
        {
            SqlParameter[] objParam = new SqlParameter[3];
            objParam[0] = new SqlParameter("@RoleId", RoleId);
            objParam[1] = new SqlParameter("@ModuleId", ModuleId);
            objParam[2] = new SqlParameter("@URL", URL);
            DataSet ds = SQLDBUtil.ExecuteDataset("CP_GetPageAccess", objParam);
            return ds;
        }
        public static DataSet CP_SuperUser_Login(string Login_Name, string Password)
        {
            SqlParameter[] objParam = new SqlParameter[3];
            objParam[0] = new SqlParameter("@LoginName", Login_Name);
            objParam[1] = new SqlParameter("@Password", Password);
            DataSet ds = SQLDBUtil.ExecuteDataset("CP_SuperUser_Login", objParam);
            return ds;
        }
        public static DataSet HR_EmpTransferDetails(int EmpID)
        {
            try
            {
                SqlParameter[] objParam = new SqlParameter[1];
                objParam[0] = new SqlParameter("@EmpID", EmpID);
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_EmpTransferDetails", objParam);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #region EmployeeWorkStatus
        public static DataSet HR_EmpTaskSystem(int CompanyID)
        {
            try
            {
                SqlParameter[] parm = new SqlParameter[1];
                parm[0] = new SqlParameter("@CompanyID", CompanyID);
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_EmpTaskSystem", parm);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet HR_EmpMobile(int EmpID)
        {
            SqlParameter[] objParam = new SqlParameter[1];
            objParam[0] = new SqlParameter("@EmpID", EmpID);
            DataSet ds = SQLDBUtil.ExecuteDataset("HR_EmpMobile", objParam);
            return ds;
        }
        public static DataSet HR_GetMailID(int EmpID)
        {
            SqlParameter[] objParam = new SqlParameter[1];
            objParam[0] = new SqlParameter("@EmpID", EmpID);
            DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetMailID", objParam);
            return ds;
        }
        public static DataSet HR_GetUpdaters()
        {
            try
            {
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetUpdaters");
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet HR_GetUpdatedYesterday()
        {
            try
            {
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetUpdatedYesterday");
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet HR_GetTaskUpdates()
        {
            try
            {
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetTaskUpdates");
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet HR_GetNotUpdatedYesterday()
        {
            try
            {
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetNotUpdatedYesterday");
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static void UpdateWorkTask(int EmpID, DateTime Date, string Task)
        {
            try
            {
                SqlParameter[] objParam = new SqlParameter[3];
                objParam[0] = new SqlParameter("@EmpID", EmpID);
                objParam[1] = new SqlParameter("@Date", Date);
                objParam[2] = new SqlParameter("@Task", Task);
                SQLDBUtil.ExecuteNonQuery("HR_UpdateEmpWorkStatus", objParam);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static void DeleteWorkTask(int EmpworkID)
        {
            try
            {
                SqlParameter[] objParam = new SqlParameter[1];
                objParam[0] = new SqlParameter("@EmpworkID", EmpworkID);
                SQLDBUtil.ExecuteNonQuery("HR_DelEmpWorkStatus", objParam);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static void InsertWorkTask(int EmpworkID, int EmpID, DateTime Date, string Task)
        {
            try
            {
                SqlParameter[] objParam = new SqlParameter[4];
                objParam[0] = new SqlParameter("@EmpworkID", EmpworkID);
                objParam[1] = new SqlParameter("@EmpID", EmpID);
                objParam[2] = new SqlParameter("@Date", Date);
                objParam[3] = new SqlParameter("@Task", Task);
                SQLDBUtil.ExecuteNonQuery("HR_InsWorkStatus", objParam);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet GetWorkStatus(HRCommon objHrCommon, int EmpID, int CompanyID)
        {
            SqlParameter[] objParam = new SqlParameter[6];
            objParam[0] = new SqlParameter("@CurrentPage", objHrCommon.CurrentPage);
            objParam[1] = new SqlParameter("@PageSize", objHrCommon.PageSize);
            objParam[2] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
            objParam[2].Direction = ParameterDirection.ReturnValue;
            objParam[3] = new SqlParameter("@NoofRecords", System.Data.SqlDbType.Int);
            objParam[3].Direction = ParameterDirection.Output;
            objParam[4] = new SqlParameter("@EmpID", EmpID);
            objParam[5] = new SqlParameter("@CompanyID", CompanyID);
            DataSet ds = SQLDBUtil.ExecuteDataset("HR_EmpWorkTask", objParam);
            objHrCommon.NoofRecords = (int)objParam[3].Value;
            objHrCommon.TotalPages = (int)objParam[2].Value;
            return ds;
        }
        public static DataSet HR_WorkStatusCount(int EmpID)
        {
            SqlParameter[] objParam = new SqlParameter[1];
            objParam[0] = new SqlParameter("@EmpID", EmpID);
            DataSet ds = SQLDBUtil.ExecuteDataset("HR_WorkStatusCount", objParam);
            return ds;
        }
        public static DataSet GetWorkStatusByID(int EmpworkID)
        {
            SqlParameter[] objParam = new SqlParameter[1];
            objParam[0] = new SqlParameter("@EmpworkID", EmpworkID);
            DataSet ds = SQLDBUtil.ExecuteDataset("HR_EmpWorkTaskByID", objParam);
            return ds;
        }
        #endregion
        #region Advertisement
        public static DataSet GetAdvertisements()
        {
            DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetAdvertisements");
            return ds;
        }
        public static DataSet GetAdvertisementById(int AdvtID)
        {
            SqlParameter[] objParam = new SqlParameter[1];
            objParam[0] = new SqlParameter("@AdvtID", AdvtID);
            DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetAdvertisementByID", objParam);
            return ds;
        }
        public static void InsUpAdvt(int AdvtID, DateTime Date, string AdvtName, string Description)
        {
            try
            {
                SqlParameter[] objParam = new SqlParameter[4];
                objParam[0] = new SqlParameter("@AdvtID", AdvtID);
                objParam[1] = new SqlParameter("@Date", Date);
                objParam[2] = new SqlParameter("@AdvtName", AdvtName);
                objParam[3] = new SqlParameter("@Description", Description);
                SQLDBUtil.ExecuteNonQuery("HR_InsUpAdvtment", objParam);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet GetSampleAdvt()
        {
            try
            {
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetSampleAdvt");
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static void DeleteAdvt(int AdvtID, int Status)
        {
            try
            {
                SqlParameter[] objParam = new SqlParameter[2];
                objParam[0] = new SqlParameter("@AdvtID", AdvtID);
                objParam[1] = new SqlParameter("@Status", Status);
                SQLDBUtil.ExecuteNonQuery("HR_DelAdvertisement", objParam);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region AdvancedLeaveApp
        public static DataSet GetHR_LeaveAcceptance(int Leave1, int Leave2)
        {
            SqlParameter[] objParam = new SqlParameter[2];
            objParam[0] = new SqlParameter("@Leave1", Leave1);
            objParam[1] = new SqlParameter("@Leave2", Leave2);
            DataSet ds = SQLDBUtil.ExecuteDataset("GetHR_LeaveAcceptance", objParam);
            return ds;
        }
        public static DataSet GetAvalableLeaves(int? EmpID)
        {
            try
            {
                SqlParameter[] objParam = new SqlParameter[1];
                objParam[0] = new SqlParameter("@EmpID", EmpID);
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetAvalableLeaves", objParam);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet GetAvalableLeaves_LC(int? EmpID)
        {
            try
            {
                SqlParameter[] objParam = new SqlParameter[1];
                objParam[0] = new SqlParameter("@EmpID", EmpID);
                DataSet ds = SQLDBUtil.ExecuteDataset("[HR_GetAvalableLeaves_LC]", objParam);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet GetAvlLeavesCount(int? EmpID, int Leavetype)
        {
            try
            {
                SqlParameter[] objParam = new SqlParameter[2];
                objParam[0] = new SqlParameter("@EmpID", EmpID);
                objParam[1] = new SqlParameter("@Leavetype", Leavetype);
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetAvlLeavesCount", objParam);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet GetAvalableLeavesByPaging(HRCommon objHrCommon, int? EmpID, int? WS, int? Dept, int CompanyID)
        {
            SqlParameter[] sqlParams = new SqlParameter[8];
            sqlParams[0] = new SqlParameter("@CurrentPage", objHrCommon.CurrentPage);
            sqlParams[1] = new SqlParameter("@PageSize", objHrCommon.PageSize);
            sqlParams[2] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
            sqlParams[2].Direction = ParameterDirection.ReturnValue;
            sqlParams[3] = new SqlParameter("@NoofRecords", System.Data.SqlDbType.Int);
            sqlParams[3].Direction = ParameterDirection.Output;
            sqlParams[4] = new SqlParameter("@EmpID", EmpID);
            sqlParams[5] = new SqlParameter("@WS", WS);
            sqlParams[6] = new SqlParameter("@Dept", Dept);
            sqlParams[7] = new SqlParameter("@CompanyID", CompanyID);
            DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetAvalableLeavesByPaging_ToolTip", sqlParams);
            objHrCommon.NoofRecords = (int)sqlParams[3].Value;
            objHrCommon.TotalPages = (int)sqlParams[2].Value;
            return ds;
        }
        public static DataSet HR_LeaveAppReply(int LID)
        {
            try
            {
                SqlParameter[] objParam = new SqlParameter[1];
                objParam[0] = new SqlParameter("@LID", LID);
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_LeaveAppReply", objParam);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static void HR_LeaveAppEmpReply(string CommentReply, int LID)
        {
            try
            {
                SqlParameter[] objParam = new SqlParameter[2];
                objParam[0] = new SqlParameter("@CommentReply", CommentReply);
                objParam[1] = new SqlParameter("@LID", LID);
                SQLDBUtil.ExecuteNonQuery("HR_LeaveAppEmpReply", objParam);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static int LeavesDaysCount(int EmpID, DateTime LeaveFrm, DateTime LeaveUtl)
        {
            try
            {
                SqlParameter[] parm = new SqlParameter[4];
                parm[0] = new SqlParameter("@EmpID", EmpID);
                parm[1] = new SqlParameter("@LeaveFrom", LeaveFrm);
                parm[2] = new SqlParameter("@LeaveUntil", LeaveUtl);
                parm[3] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
                parm[3].Direction = ParameterDirection.ReturnValue;
                SQLDBUtil.ExecuteNonQuery("HR_LeaveDaysCount", parm);
                return Convert.ToInt32(parm[3].Value);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static DataSet HR_SMS_LeaveApplicatonSent(int EmpID)
        {
            SqlParameter[] parm = new SqlParameter[1];
            parm[0] = new SqlParameter("@EmpID", EmpID);
            DataSet ds = SQLDBUtil.ExecuteDataset("HR_SMS_LeaveApplicatonSent", parm);
            return ds;
        }
        public static void SMSUpDateLAJobCode(int LID, string JobCode)
        {
            try
            {
                SqlParameter[] par = new SqlParameter[2];
                par[0] = new SqlParameter("@LID", LID);
                par[1] = new SqlParameter("@JobCode", JobCode);
                SQLDBUtil.ExecuteNonQuery("HR_SMSUpDateLAJobCode", par);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public static int HR_InsUpLeaveApplication_SaveProof(HRCommon objHrCommon, int EmpID, DateTime AppliedOn, DateTime LeaveFrom, DateTime LeaveUntil, string Reason, int Status, int LeaveType, string Proof)
        {
            try
            {
                SqlParameter[] objParam = new SqlParameter[10];
                objParam[0] = new SqlParameter("@LID", SqlDbType.Int);
                objParam[0].Direction = ParameterDirection.InputOutput;
                objParam[0].Value = objHrCommon.LID;
                objParam[1] = new SqlParameter("@EmpID", EmpID);
                objParam[2] = new SqlParameter("@AppliedOn", AppliedOn);
                objParam[3] = new SqlParameter("@LeaveFrom", LeaveFrom);
                objParam[4] = new SqlParameter("@LeaveUntil", LeaveUntil);
                objParam[5] = new SqlParameter("@Reason", Reason);
                objParam[6] = new SqlParameter("@Status", Status);
                objParam[7] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
                objParam[7].Direction = ParameterDirection.ReturnValue;
                objParam[8] = new SqlParameter("@LeaveType", LeaveType);
                objParam[9] = new SqlParameter("@Proof", Proof);
                SQLDBUtil.ExecuteNonQuery("HR_InsUpLeaveApplicationwith_proof", objParam);
                if (objParam[0] != null)
                {
                    objHrCommon.LID = Convert.ToInt32(objParam[0].Value);
                }
                return Convert.ToInt32(objParam[7].Value);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static void HR_InsUpHrPermission(int LID, int Status, string Comment, DateTime? GrantedFrom, DateTime? GrantedUntil, int GrantedBy, DateTime? GrantedOn)
        {
            try
            {
                SqlParameter[] objParam = new SqlParameter[7];
                objParam[0] = new SqlParameter("@LID", LID);
                objParam[1] = new SqlParameter("@Status", Status);
                objParam[2] = new SqlParameter("@Comment", Comment);
                objParam[3] = new SqlParameter("@GrantedFrom", GrantedFrom);
                objParam[4] = new SqlParameter("@GrantedUntil", GrantedUntil);
                objParam[5] = new SqlParameter("@GrantedBy", GrantedBy);
                objParam[6] = new SqlParameter("@GrantedOn", GrantedOn);
                SQLDBUtil.ExecuteNonQuery("HR_InsUpHrPermission", objParam);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static void HR_InsUpHrPermission_Tooverrite_Wo(int LID, int Status, string Comment, DateTime? GrantedFrom, DateTime? GrantedUntil, int GrantedBy, DateTime? GrantedOn)
        {
            try
            {
                SqlParameter[] objParam = new SqlParameter[7];
                objParam[0] = new SqlParameter("@LID", LID);
                objParam[1] = new SqlParameter("@Status", Status);
                objParam[2] = new SqlParameter("@Comment", Comment);
                objParam[3] = new SqlParameter("@GrantedFrom", GrantedFrom);
                objParam[4] = new SqlParameter("@GrantedUntil", GrantedUntil);
                objParam[5] = new SqlParameter("@GrantedBy", GrantedBy);
                objParam[6] = new SqlParameter("@GrantedOn", GrantedOn);
                SQLDBUtil.ExecuteNonQuery("HR_InsUpHrPermission_Tooverrite_Wo", objParam);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static void InsLeavepermission(int LID, int EmpID, DateTime? AppliedOn, DateTime? Leavefrom, DateTime? LeaveUntill, DateTime? GrantedFrom, DateTime? GrantedUntil, int GrantedBy, string Proof)
        {
            try
            {
                SqlParameter[] objParam = new SqlParameter[9];
                objParam[0] = new SqlParameter("@LID", LID);
                objParam[1] = new SqlParameter("@EmpID", EmpID);
                objParam[2] = new SqlParameter("@AppliedOn", AppliedOn);
                objParam[3] = new SqlParameter("@LeaveFrom", Leavefrom);
                objParam[4] = new SqlParameter("@LeaveUntil", LeaveUntill);
                objParam[5] = new SqlParameter("@GrantedFrom", GrantedFrom);
                objParam[6] = new SqlParameter("@GrantedUntil", GrantedUntil);
                objParam[7] = new SqlParameter("@GrantedBy", GrantedBy);
                objParam[8] = new SqlParameter("@Proof", Proof);
                SQLDBUtil.ExecuteNonQuery("HR_InsLeaveApplication", objParam);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet HR_GetLeaveDetails(int EmpID)
        {
            try
            {
                SqlParameter[] objParam = new SqlParameter[1];
                objParam[0] = new SqlParameter("@EmpID", EmpID);
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetLeaveDetails", objParam);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet HR_GetLeaveDetailsByPaging(HRCommon objHrCommon, int EmpID, int wsid, string FileName, int Month, int Year, int CompanyID)
        {
            SqlParameter[] sqlParams = new SqlParameter[10];
            sqlParams[0] = new SqlParameter("@CurrentPage", objHrCommon.CurrentPage);
            sqlParams[1] = new SqlParameter("@PageSize", objHrCommon.PageSize);
            sqlParams[2] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
            sqlParams[2].Direction = ParameterDirection.ReturnValue;
            sqlParams[3] = new SqlParameter("@NoofRecords", System.Data.SqlDbType.Int);
            sqlParams[3].Direction = ParameterDirection.Output;
            sqlParams[4] = new SqlParameter("@EmpID", EmpID);
            sqlParams[5] = new SqlParameter("@Month", Month);
            sqlParams[6] = new SqlParameter("@Year", Year);
            sqlParams[7] = new SqlParameter("@CompanyID", CompanyID);
            sqlParams[8] = new SqlParameter("@WSID", wsid);
            sqlParams[9] = new SqlParameter("@FileName", FileName);

            DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetLeaveDetailsByPaging", sqlParams);
            objHrCommon.NoofRecords = (int)sqlParams[3].Value;
            objHrCommon.TotalPages = (int)sqlParams[2].Value;
            return ds;
        }
        public static void HR_DelLeaveApplication(int LID)
        {
            try
            {
                SqlParameter[] objParam = new SqlParameter[1];
                objParam[0] = new SqlParameter("@LID", LID);
                SQLDBUtil.ExecuteNonQuery("HR_DelLeaveApplication", objParam);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet HR_GetLeaveRejected()
        {
            try
            {
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetLeaveRejected");
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet HR_GetLeaveRejectedByPaging(HRCommon objHrCommon, int? Month, int? Year, int CompanyID, int Empid, int Deptid, int Wsid)
        {
            SqlParameter[] sqlParams = new SqlParameter[10];
            sqlParams[0] = new SqlParameter("@CurrentPage", objHrCommon.CurrentPage);
            sqlParams[1] = new SqlParameter("@PageSize", objHrCommon.PageSize);
            sqlParams[2] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
            sqlParams[2].Direction = ParameterDirection.ReturnValue;
            sqlParams[3] = new SqlParameter("@NoofRecords", System.Data.SqlDbType.Int);
            sqlParams[3].Direction = ParameterDirection.Output;
            sqlParams[4] = new SqlParameter("@Month", Month);
            sqlParams[5] = new SqlParameter("@Year", Year);
            sqlParams[6] = new SqlParameter("@CompanyID", CompanyID);
            sqlParams[7] = new SqlParameter("@Empid", Empid);
            sqlParams[8] = new SqlParameter("@DeptNo", Deptid);
            sqlParams[9] = new SqlParameter("@Wsid", Wsid);
            DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetLeaveRejectedByPaging", sqlParams);
            objHrCommon.NoofRecords = (int)sqlParams[3].Value;
            objHrCommon.TotalPages = (int)sqlParams[2].Value;
            return ds;
        }
        public static DataSet HR_GetLeaveCanceledByPaging(HRCommon objHrCommon, int? Month, int? Year, int CompanyID, int Empid, int Deptid, int Wsid)
        {
            SqlParameter[] sqlParams = new SqlParameter[10];
            sqlParams[0] = new SqlParameter("@CurrentPage", objHrCommon.CurrentPage);
            sqlParams[1] = new SqlParameter("@PageSize", objHrCommon.PageSize);
            sqlParams[2] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
            sqlParams[2].Direction = ParameterDirection.ReturnValue;
            sqlParams[3] = new SqlParameter("@NoofRecords", System.Data.SqlDbType.Int);
            sqlParams[3].Direction = ParameterDirection.Output;
            sqlParams[4] = new SqlParameter("@Month", Month);
            sqlParams[5] = new SqlParameter("@Year", Year);
            sqlParams[6] = new SqlParameter("@CompanyID", CompanyID);
            sqlParams[7] = new SqlParameter("@Empid", Empid);
            sqlParams[8] = new SqlParameter("@DeptNo", Deptid);
            sqlParams[9] = new SqlParameter("@Wsid", Wsid);
            DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetLeaveCanceledByPaging", sqlParams);
            objHrCommon.NoofRecords = (int)sqlParams[3].Value;
            objHrCommon.TotalPages = (int)sqlParams[2].Value;
            return ds;
        }
        public static DataSet HR_GetGrantedLeaves()
        {
            try
            {
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetGrantedLeaves");
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet HR_GetGrantedLeavesByPaging(HRCommon objHrCommon, int? Month, int? Year, int CompanyID, int Empid, int Deptid, int Wsid)
        {
            SqlParameter[] sqlParams = new SqlParameter[10];
            sqlParams[0] = new SqlParameter("@CurrentPage", objHrCommon.CurrentPage);
            sqlParams[1] = new SqlParameter("@PageSize", objHrCommon.PageSize);
            sqlParams[2] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
            sqlParams[2].Direction = ParameterDirection.ReturnValue;
            sqlParams[3] = new SqlParameter("@NoofRecords", System.Data.SqlDbType.Int);
            sqlParams[3].Direction = ParameterDirection.Output;
            sqlParams[4] = new SqlParameter("@Month", Month);
            sqlParams[5] = new SqlParameter("@Year", Year);
            sqlParams[6] = new SqlParameter("@CompanyID", CompanyID);
            sqlParams[7] = new SqlParameter("@Empid", Empid);
            sqlParams[8] = new SqlParameter("@DeptNo", Deptid);
            sqlParams[9] = new SqlParameter("@Wsid", Wsid);
            DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetGrantedLeavesByPaging", sqlParams);
            objHrCommon.NoofRecords = (int)sqlParams[3].Value;
            objHrCommon.TotalPages = (int)sqlParams[2].Value;
            return ds;
        }
        public static DataSet HR_GetLeaveAppsToHR()
        {
            try
            {
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetLeaveAppsToHR");
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet HR_GetLeaveAppsToHRByPaging(HRCommon objHrCommon, int? Month, int? Year, int CompanyID, int Empid, int Deptid, int Wsid, int? status)
        {
            SqlParameter[] sqlParams = new SqlParameter[11];
            sqlParams[0] = new SqlParameter("@CurrentPage", objHrCommon.CurrentPage);
            sqlParams[1] = new SqlParameter("@PageSize", objHrCommon.PageSize);
            sqlParams[2] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
            sqlParams[2].Direction = ParameterDirection.ReturnValue;
            sqlParams[3] = new SqlParameter("@NoofRecords", System.Data.SqlDbType.Int);
            sqlParams[3].Direction = ParameterDirection.Output;
            sqlParams[4] = new SqlParameter("@Month", Month);
            sqlParams[5] = new SqlParameter("@Year", Year);
            sqlParams[6] = new SqlParameter("@CompanyID", CompanyID);
            sqlParams[7] = new SqlParameter("@Empid", Empid);
            sqlParams[8] = new SqlParameter("@DeptNo", Deptid);
            sqlParams[9] = new SqlParameter("@Wsid", Wsid);
            sqlParams[10] = new SqlParameter("@status", status);
            DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetLeaveAppsToHRByPaging", sqlParams);
            objHrCommon.NoofRecords = (int)sqlParams[3].Value;
            objHrCommon.TotalPages = (int)sqlParams[2].Value;
            return ds;
        }
        public static DataSet HR_GetLeaveDetailsByID(int LID)
        {
            try
            {
                SqlParameter[] objParam = new SqlParameter[1];
                objParam[0] = new SqlParameter("@LID", LID);
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetLeaveDetailsByID", objParam);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet HR_GetLeaveTypes()
        {
            try
            {
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetLeaveTypes");
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Feedback
        public static DataSet GetFeedback()
        {
            try
            {
                DataSet ds = SQLDBUtil.ExecuteDataset("GetFeedback");
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet GetFeedbackByPaging(HRCommon objHrCommon)
        {
            SqlParameter[] sqlParams = new SqlParameter[4];
            sqlParams[0] = new SqlParameter("@CurrentPage", objHrCommon.CurrentPage);
            sqlParams[1] = new SqlParameter("@PageSize", objHrCommon.PageSize);
            sqlParams[2] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
            sqlParams[2].Direction = ParameterDirection.ReturnValue;
            sqlParams[3] = new SqlParameter("@NoofRecords", System.Data.SqlDbType.Int);
            sqlParams[3].Direction = ParameterDirection.Output;
            DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetFeedbackByPaging", sqlParams);
            objHrCommon.NoofRecords = (int)sqlParams[3].Value;
            objHrCommon.TotalPages = (int)sqlParams[2].Value;
            return ds;
        }
        public static DataSet GetT_HR_FeedbackBind(int EmpID)
        {
            try
            {
                SqlParameter[] objParam = new SqlParameter[1];
                objParam[0] = new SqlParameter("@EmpID", EmpID);
                DataSet ds = SQLDBUtil.ExecuteDataset("GetT_HR_FeedbackBind", objParam);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static void InsUpFeedback(int EmpID, string FeedBackType, string UserType, string Name, string Mobile, string Comment, DateTime Date)
        {
            try
            {
                SqlParameter[] objParam = new SqlParameter[7];
                objParam[0] = new SqlParameter("@EmpID", EmpID);
                objParam[1] = new SqlParameter("@FeedBackType", FeedBackType);
                objParam[2] = new SqlParameter("@UserType", UserType);
                objParam[3] = new SqlParameter("@Name", Name);
                objParam[4] = new SqlParameter("@Mobile", Mobile);
                objParam[5] = new SqlParameter("@Comment", Comment);
                objParam[6] = new SqlParameter("@Date", Date);
                SQLDBUtil.ExecuteNonQuery("InsUpFeedback", objParam);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static void InsUpFeedbackAnonimus(string FeedBackType, string UserType, string Comment, DateTime Date)
        {
            try
            {
                SqlParameter[] objParam = new SqlParameter[4];
                objParam[0] = new SqlParameter("@FeedBackType", FeedBackType);
                objParam[1] = new SqlParameter("@UserType", UserType);
                objParam[2] = new SqlParameter("@Comment", Comment);
                objParam[3] = new SqlParameter("@Date", Date);
                SQLDBUtil.ExecuteNonQuery("InsUpFeedbackAnonimus", objParam);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static void HR_DelFeedback(int FBId)
        {
            try
            {
                SqlParameter[] objParam = new SqlParameter[1];
                objParam[0] = new SqlParameter("@FBId", FBId);
                SQLDBUtil.ExecuteNonQuery("HR_DelFeedback", objParam);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region EmpTermination
        public static DataSet CP_Get_ModulesList()
        {
            try
            {
                DataSet ds = SQLDBUtil.ExecuteDataset("CP_Get_ModulesList");
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet SearchEmpList(int? siteId, int? DeptId, string Name, int? EmpId)
        {
            try
            {
                DataSet ds = SQLDBUtil.ExecuteDataset("CP_GetEmployeesSearch", new SqlParameter[] { new SqlParameter("@WSId", siteId), new SqlParameter("@depId", DeptId), new SqlParameter("@EmpName", Name), new SqlParameter("@EmpId", EmpId) });
                return ds;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static DataSet SearchEmpListByPaging(HRCommon objHrCommon, int siteId, int DeptId, string Name, int EmpId, int CompanyID)
        {
            SqlParameter[] sqlParams = new SqlParameter[9];
            sqlParams[0] = new SqlParameter("@CurrentPage", objHrCommon.CurrentPage);
            sqlParams[1] = new SqlParameter("@PageSize", objHrCommon.PageSize);
            sqlParams[2] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
            sqlParams[2].Direction = ParameterDirection.ReturnValue;
            sqlParams[3] = new SqlParameter("@NoofRecords", System.Data.SqlDbType.Int);
            sqlParams[3].Direction = ParameterDirection.Output;
            sqlParams[4] = new SqlParameter("@WSId", siteId);
            sqlParams[5] = new SqlParameter("@depId", DeptId);
            sqlParams[6] = new SqlParameter("EmpName", Name);
            sqlParams[7] = new SqlParameter("@EmpId", EmpId);
            sqlParams[8] = new SqlParameter("@CompanyID", CompanyID);
            DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetEmployeesSearchByPaging", sqlParams);
            objHrCommon.NoofRecords = (int)sqlParams[3].Value;
            objHrCommon.TotalPages = (int)sqlParams[2].Value;
            return ds;
        }
        public static DataSet HR_EmpTermination(int EmpID)
        {
            try
            {
                SqlParameter[] objParam = new SqlParameter[1];
                objParam[0] = new SqlParameter("@EmpID", EmpID);
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_EmpTermination", objParam);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet HR_HeadInfoOnEmpTermination(int EmpID)
        {
            try
            {
                SqlParameter[] objParam = new SqlParameter[1];
                objParam[0] = new SqlParameter("@EmpID", EmpID);
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_HeadInfoOnEmpTermination", objParam);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static void HR_EmpToTerminate(int EmpID)
        {
            try
            {
                SqlParameter[] objParam = new SqlParameter[1];
                objParam[0] = new SqlParameter("@EmpID", EmpID);
                SQLDBUtil.ExecuteNonQuery("HR_EmpToTerminate", objParam);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static void HMS_EmpToTerminate(int EmpID, int UserID)
        {
            try
            {
                SqlParameter[] objParam = new SqlParameter[2];
                objParam[0] = new SqlParameter("@EmpID", EmpID);
                objParam[1] = new SqlParameter("@UserID", UserID);
                SQLDBUtil.ExecuteNonQuery("HMS_EmpToTerminate", objParam);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet HR_InformToAccDept()
        {
            try
            {
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_InformToAccDept");
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region AttandanceMarkCount
        public static DataSet HR_AttendanceMarkCount(int CompanyID)
        {
            try
            {
                SqlParameter[] parm = new SqlParameter[1];
                parm[0] = new SqlParameter("@CompanyID", CompanyID);
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_AttendanceMarkCount", parm);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet HR_Get_AttendanceMarkingdetails(DateTime Date, int CompanyID, int WSid)
        {
            try
            {
                SqlParameter[] objParam = new SqlParameter[3];
                objParam[0] = new SqlParameter("@Date", Date);
                objParam[1] = new SqlParameter("@CompanyID", CompanyID);
                objParam[2] = new SqlParameter("@WSid", WSid);
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_Get_AttendanceMarkingdetails", objParam);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region EmpToDoList
        public static DataSet HR_GetEmpToDoList(int ToDoID)
        {
            try
            {
                SqlParameter[] objParam = new SqlParameter[1];
                objParam[0] = new SqlParameter("@ToDoID", ToDoID);
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetEmpToDoList", objParam);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet HR_GetEmployeeBySearch(string EmpName)
        {
            try
            {
                SqlParameter[] objParam = new SqlParameter[1];
                objParam[0] = new SqlParameter("@EmpName", EmpName);
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetEmployeeBySearch", objParam);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet HR_GetToDolistByToDOID(int ToDoId)
        {
            try
            {
                SqlParameter[] objParam = new SqlParameter[1];
                objParam[0] = new SqlParameter("@ToDOLstID", ToDoId);
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetToDoListByToDoID", objParam);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static void HR_DelToDoList(int ToDoID)
        {
            try
            {
                SqlParameter[] objParam = new SqlParameter[1];
                objParam[0] = new SqlParameter("@ToDoID", ToDoID);
                SQLDBUtil.ExecuteNonQuery("HR_DelToDoList", objParam);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static void HR_InsUpToDoList(int ToDoID, int EmpID, string Subject, DateTime StartDate, DateTime DueDate, string Status, int Priority, string Complete, string Task, string AssignedBy, int AssignerEmpID, DateTime? RemindOn, string RemindTime)
        {
            try
            {
                SqlParameter[] objParam = new SqlParameter[13];
                objParam[0] = new SqlParameter("@ToDoID", ToDoID);
                objParam[1] = new SqlParameter("@EmpID", EmpID);
                objParam[2] = new SqlParameter("@Subject", Subject);
                objParam[3] = new SqlParameter("@StartDate", StartDate);
                objParam[4] = new SqlParameter("@DueDate", DueDate);
                objParam[5] = new SqlParameter("@Status", Status);
                objParam[6] = new SqlParameter("@Priority", Priority);
                objParam[7] = new SqlParameter("@Complete", Complete);
                objParam[8] = new SqlParameter("@Task", Task);
                objParam[9] = new SqlParameter("@AssignedBy", AssignedBy);
                objParam[10] = new SqlParameter("@AssignerEmpID", AssignerEmpID);
                objParam[11] = new SqlParameter("@RemindOn", RemindOn);
                objParam[12] = new SqlParameter("@RemindTime", RemindTime);
                SQLDBUtil.ExecuteNonQuery("HR_InsUpToDoList", objParam);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static void HR_InsTaskHistory(int TaskNo, int ToDoID, string Subject, string Status, DateTime ReportedOn, string AssignedBy, string Complete, string Report)
        {
            try
            {
                SqlParameter[] objParam = new SqlParameter[8];
                objParam[0] = new SqlParameter("@TaskNo", TaskNo);
                objParam[1] = new SqlParameter("@ToDoID", ToDoID);
                objParam[2] = new SqlParameter("@Subject", Subject);
                objParam[3] = new SqlParameter("@Status", Status);
                objParam[4] = new SqlParameter("@ReportedOn", ReportedOn);
                objParam[5] = new SqlParameter("@AssignedBy", AssignedBy);
                objParam[6] = new SqlParameter("@Complete", Complete);
                objParam[7] = new SqlParameter("@Report", Report);
                SQLDBUtil.ExecuteNonQuery("HR_InsTaskHistory", objParam);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet HR_GetTaskHistory(int ToDoID)
        {
            try
            {
                SqlParameter[] objParam = new SqlParameter[1];
                objParam[0] = new SqlParameter("@ToDoID", ToDoID);
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetTaskHistory", objParam);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet HR_GetToDolistByAssignerStatus(HRCommon objHrCommon, int AssignerEmpID, string Status, int? WSID, int? DeptID, int? EmpID, string EmpName)
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
                sqlParams[4] = new SqlParameter("@AssignerEmpID", AssignerEmpID);
                sqlParams[5] = new SqlParameter("@TaskStatus", Status);
                sqlParams[6] = new SqlParameter("@WSID", WSID);
                sqlParams[7] = new SqlParameter("@DeptID", DeptID);
                sqlParams[8] = new SqlParameter("@EmpID", EmpID);
                sqlParams[9] = new SqlParameter("@EmpName", EmpName);
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetToDolistByAssigner", sqlParams);
                objHrCommon.NoofRecords = (int)sqlParams[3].Value;
                objHrCommon.TotalPages = (int)sqlParams[2].Value;
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet HR_GetToDolistByAssigner(int AssignerEmpID)
        {
            try
            {
                SqlParameter[] objParam = new SqlParameter[1];
                objParam[0] = new SqlParameter("@AssignerEmpID", AssignerEmpID);
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetToDolistByAssigner", objParam);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet HR_ToDoListReminder(int EmpID)
        {
            try
            {
                SqlParameter[] objParam = new SqlParameter[1];
                objParam[0] = new SqlParameter("@EmpID", EmpID);
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_ToDoListReminder", objParam);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Reimbuuse&Contribution
        public static DataSet HR_GetReimburseAmount()
        {
            try
            {
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetReimburseAmount");
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static void HR_InsUpContributions(int EmpID, int Month, int Year, int ContributionID, string ContributionAmount, int TransID)
        {
            try
            {
                SqlParameter[] objParam = new SqlParameter[6];
                objParam[0] = new SqlParameter("@EmpID", EmpID);
                objParam[1] = new SqlParameter("@Month", Month);
                objParam[2] = new SqlParameter("@Year", Year);
                objParam[3] = new SqlParameter("@ContributionID", ContributionID);
                objParam[4] = new SqlParameter("@ContributionAmount", ContributionAmount);
                objParam[5] = new SqlParameter("@TransID", TransID);
                SQLDBUtil.ExecuteNonQuery("HR_InsUpContributions", objParam);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet HR_SearchReimburseEmployee(int RMItemId, int Month, int Year)
        {
            try
            {
                SqlParameter[] objParam = new SqlParameter[3];
                objParam[0] = new SqlParameter("@RMItemId", RMItemId);
                objParam[1] = new SqlParameter("@Month", Month);
                objParam[2] = new SqlParameter("@Year", Year);
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_SearchReimburseEmployee", objParam);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet HR_SerchEmp_Reimburse(string EmpName, int CompanyID)
        {
            try
            {
                SqlParameter[] objParam = new SqlParameter[2];
                objParam[0] = new SqlParameter("@EmpName", EmpName);
                objParam[1] = new SqlParameter("@CompanyID", CompanyID);
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_SerchEmp_Reimburse", objParam);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet HR_googlesearch_emp(string SearchKey, int Status)
        {
            try
            {
                SqlParameter[] objParam = new SqlParameter[2];
                objParam[0] = new SqlParameter("@search", SearchKey);
                objParam[1] = new SqlParameter("@Status", Status);
                DataSet ds = SQLDBUtil.ExecuteDataset("GetEmployeesByTravel_Exp_googlesearch", objParam);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet GetEmployeesByTravel_googlesearch_Exp(string SearchKey)
        {
            try
            {
                SqlParameter[] objParam = new SqlParameter[1];
                objParam[0] = new SqlParameter("@search", SearchKey);
                DataSet ds = SQLDBUtil.ExecuteDataset("GetEmployeesByTravel_googlesearch_Exp", objParam);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet HR_EmpRepaySearch(int WorkSite, int Dept, int CompanyID)
        {
            try
            {
                SqlParameter[] objParam = new SqlParameter[3];
                objParam[0] = new SqlParameter("@WorkSite", WorkSite);
                objParam[1] = new SqlParameter("@Dept", Dept);
                objParam[2] = new SqlParameter("@CompanyID", CompanyID);
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_EmpRepaySearch", objParam);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet HR_EmpRepaySearch_EmpPenalties(String Searchkey, int WorkSite, int Dept, int CompanyID)
        {
            try
            {
                SqlParameter[] objParam = new SqlParameter[4];
                objParam[0] = new SqlParameter("@WorkSite", WorkSite);
                objParam[1] = new SqlParameter("@Dept", Dept);
                objParam[2] = new SqlParameter("@CompanyID", CompanyID);
                objParam[3] = new SqlParameter("@Search", Searchkey);
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_EmpRepaySearch_EmpPenaltiesGS", objParam);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet HR_GetPayRollItems()
        {
            try
            {
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetPayRollItems");
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet GetAu()
        {
            try
            {
                DataSet ds = SQLDBUtil.ExecuteDataset("GetAu");
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet sh_GetAuReimbursment()
        {
            try
            {
                DataSet ds = SQLDBUtil.ExecuteDataset("sh_GetAuReimbursment");
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static void HR_InsUpEmpContribution(int EmpID, int Month, int Year, int ContributionID, string ContributionAmount, int TransID)
        {
            try
            {
                SqlParameter[] objParam = new SqlParameter[6];
                objParam[0] = new SqlParameter("@EmpID", EmpID);
                objParam[1] = new SqlParameter("@Month", Month);
                objParam[2] = new SqlParameter("@Year", Year);
                objParam[3] = new SqlParameter("@ContributionID", ContributionID);
                objParam[4] = new SqlParameter("@ContributionAmount", ContributionAmount);
                objParam[5] = new SqlParameter("@TransID", TransID);
                SQLDBUtil.ExecuteNonQuery("HR_InsUpEmpContribution", objParam);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static void HR_InsUpEmpReimbursment(int RID, int EmpID, int Month, int Year, int ReimburseID, int UOMID, int Quantity, string UnitRate, string Explanation, string Proof, string Amount, int UserID, string UnitType, string ReimburseType, DateTime ReportedOn)
        {
            try
            {
                SqlParameter[] objParam = new SqlParameter[15];
                objParam[0] = new SqlParameter("@RID", RID);
                objParam[1] = new SqlParameter("@EmpID", EmpID);
                objParam[2] = new SqlParameter("@Month", Month);
                objParam[3] = new SqlParameter("@Year", Year);
                objParam[4] = new SqlParameter("@ReimburseID", ReimburseID);
                objParam[5] = new SqlParameter("@UOMID", UOMID);
                objParam[6] = new SqlParameter("@Quantity", Quantity);
                objParam[7] = new SqlParameter("@UnitRate", UnitRate);
                objParam[8] = new SqlParameter("@Explanation", Explanation);
                objParam[9] = new SqlParameter("@Proof", Proof);
                objParam[10] = new SqlParameter("@Amount", Amount);
                objParam[11] = new SqlParameter("@UserID", UserID);
                objParam[12] = new SqlParameter("@UnitType", UnitType);
                objParam[13] = new SqlParameter("@ReimburseType", ReimburseType);
                objParam[14] = new SqlParameter("@ReportedOn", ReportedOn);
                SQLDBUtil.ExecuteNonQuery("HR_InsUpEmpReimbursment", objParam);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet HR_GetEmpReimbursement()
        {
            try
            {
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetEmpReimbursement");
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static void HR_DelEmpReimburse(int RID)
        {
            try
            {
                SqlParameter[] objParam = new SqlParameter[1];
                objParam[0] = new SqlParameter("@RID", RID);
                SQLDBUtil.ExecuteNonQuery("HR_DelEmpReimburse", objParam);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet HR_GetEmpReimbursementByEmpID(int EmpID)
        {
            try
            {
                SqlParameter[] objParam = new SqlParameter[1];
                objParam[0] = new SqlParameter("@EmpID", EmpID);
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetEmpReimbursementByEmpID", objParam);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet HR_GetReimbursByRID(int RID)
        {
            try
            {
                SqlParameter[] objParam = new SqlParameter[1];
                objParam[0] = new SqlParameter("@RID", RID);
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetReimbursByRID", objParam);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet HR_ApproveEmpReimburse(DataSet ds, int EmpID)
        {
            try
            {
                return SQLDBUtil.ExecuteDataset("HR_ApproveEmpReimburse", new SqlParameter[] { new SqlParameter("@RIDs", ds.GetXml()), new SqlParameter("@EmpID", EmpID) });
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static DataSet HR_GetApprovedEmpReimburs()
        {
            try
            {
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetApprovedEmpReimburs");
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static void HR_DelApproveEmpReimburs(int TID)
        {
            try
            {
                SqlParameter[] objParam = new SqlParameter[1];
                objParam[0] = new SqlParameter("@TID", TID);
                SQLDBUtil.ExecuteNonQuery("HR_DelApproveEmpReimburs", objParam);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static void HR_InsUpApproveEmpReimburse(int TID, string RIds, int EmpID, Double TotAmount, DateTime ApprovedOn, string ReimburseTypes)
        {
            try
            {
                SqlParameter[] objParam = new SqlParameter[6];
                objParam[0] = new SqlParameter("@TID", TID);
                objParam[1] = new SqlParameter("@RIDs", RIds);
                objParam[2] = new SqlParameter("@EmpID", EmpID);
                objParam[3] = new SqlParameter("@TotAmount", TotAmount);
                objParam[4] = new SqlParameter("@ApprovedOn", ApprovedOn);
                objParam[5] = new SqlParameter("@ReimburseTypes", ReimburseTypes);
                SQLDBUtil.ExecuteNonQuery("HR_InsUpApproveEmpReimburse", objParam);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet HR_ApprovedReimburseByEmpID(int EmpID)
        {
            try
            {
                SqlParameter[] objParam = new SqlParameter[1];
                objParam[0] = new SqlParameter("@EmpID", EmpID);
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_ApprovedReimburseByEmpID", objParam);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static void HR_InsEmpReimburseTransaction(DateTime TransTime, string Remarks, int CompanyID, int EmpID, DateTime ActualDate)
        {
            try
            {
                SqlParameter[] objParam = new SqlParameter[5];
                objParam[0] = new SqlParameter("@TransTime", TransTime);
                objParam[1] = new SqlParameter("@Remarks", Remarks);
                objParam[2] = new SqlParameter("@CompanyID", CompanyID);
                objParam[3] = new SqlParameter("@EmpID", EmpID);
                objParam[4] = new SqlParameter("@TotAmount", ActualDate);
                SQLDBUtil.ExecuteNonQuery("HR_InsEmpReimburseTransaction", objParam);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet HR_GetMaxTransID()
        {
            try
            {
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetMaxTransID");
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet HR_InsUpdRemItems(DataSet DsRemItems)
        {
            try
            {
                return SQLDBUtil.ExecuteDataset("HMS_RemItemsXML", new SqlParameter[] { new SqlParameter("@RemItems", DsRemItems.GetXml()) });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet HR_EmpPenalty_Items(DataSet DsRemItems)
        {
            try
            {
                return SQLDBUtil.ExecuteDataset("HMS_EmpPenaltiesXML", new SqlParameter[] { new SqlParameter("@RemItems", DsRemItems.GetXml()) });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet HMS_TransDetailsXML(DataSet dsTransDetail)
        {
            try
            {
                return SQLDBUtil.ExecuteDataset("HMS_TransDetailsXML", new SqlParameter[] { new SqlParameter("@TDItems", dsTransDetail.GetXml()) });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet HMS_TransXML(DataSet dsApprove, string Remarks, int UserID)
        {
            try
            {
                SqlParameter[] objParam = new SqlParameter[3];
                objParam[0] = new SqlParameter("@TransItems", dsApprove.GetXml());
                objParam[1] = new SqlParameter("@Remarks", Remarks);
                objParam[2] = new SqlParameter("@UserID", UserID);
                DataSet ds = SQLDBUtil.ExecuteDataset("HMS_TransXML", objParam);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet HMS_TranserAccXML(DataSet dsTransferDetail, string Remarks, double TotAmt, int UserId)
        {
            try
            {
                SqlParameter[] objParam = new SqlParameter[4];
                objParam[0] = new SqlParameter("@TransItems", dsTransferDetail.GetXml());
                objParam[1] = new SqlParameter("@Remarks", Remarks);
                objParam[2] = new SqlParameter("@TotAmt", TotAmt);
                objParam[3] = new SqlParameter("@UserID", UserId);
                DataSet ds = SQLDBUtil.ExecuteDataset("HMS_TranserAccXML", objParam);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet HMS_BenefitAccXML(DataSet dsTransferDetail, string Remarks, double TotAmt, int UserId)
        {
            try
            {
                SqlParameter[] objParam = new SqlParameter[4];
                objParam[0] = new SqlParameter("@TransItems", dsTransferDetail.GetXml());
                objParam[1] = new SqlParameter("@Remarks", Remarks);
                objParam[2] = new SqlParameter("@TotAmt", TotAmt);
                objParam[3] = new SqlParameter("@UserID", UserId);
                DataSet ds = SQLDBUtil.ExecuteDataset("sh_BeniftAcPost", objParam);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet HMS_EmpPenlityTranserAccXML(DataSet dsTransferDetail, string Remarks, double TotAmt, int UserId)
        {
            try
            {
                SqlParameter[] objParam = new SqlParameter[4];
                objParam[0] = new SqlParameter("@TransItems", dsTransferDetail.GetXml());
                objParam[1] = new SqlParameter("@Remarks", Remarks);
                objParam[2] = new SqlParameter("@TotAmt", TotAmt);
                objParam[3] = new SqlParameter("@UserID", UserId);
                DataSet ds = SQLDBUtil.ExecuteDataset("HMS_Emp_Penaliey_TranserAccXML", objParam);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public object HR_GetMaxTransIDObj()
        {
            try
            {
                return SqlHelper.ExecuteScalar("HR_GetMaxTransID");
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static void HR_InsUpEmpReimDescription(int ReimburseID, int AUID, int Qty, double UnitRate, string Description, DateTime DateOfSpent, DateTime DOS, int EmpID)
        {
            try
            {
                SqlParameter[] objParam = new SqlParameter[8];
                objParam[0] = new SqlParameter("@ReimburseID", ReimburseID);
                objParam[1] = new SqlParameter("@AUID", AUID);
                objParam[2] = new SqlParameter("@Qty", Qty);
                objParam[3] = new SqlParameter("@UnitRate", UnitRate);
                objParam[4] = new SqlParameter("@Description", Description);
                objParam[5] = new SqlParameter("@DateOfSpent", DateOfSpent);
                objParam[6] = new SqlParameter("@DOS", DOS);
                objParam[7] = new SqlParameter("@EmpID", EmpID);
                SQLDBUtil.ExecuteNonQuery("HR_InsUpEmpReimDescription", objParam);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet HR_EmpReimEmployees(int CompanyID)
        {
            try
            {
                SqlParameter[] parm = new SqlParameter[1];
                parm[0] = new SqlParameter("@CompanyID", CompanyID);
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_EmpReimEmployees", parm);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet HR_EmpPenlity_Employees(int CompanyID)
        {
            try
            {
                SqlParameter[] parm = new SqlParameter[1];
                parm[0] = new SqlParameter("@CompanyID", CompanyID);
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_EmpPenality_Employees", parm);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet HR_EmpReimEmployeesByPaging(HRCommon objHrCommon)
        {
            SqlParameter[] sqlParams = new SqlParameter[4];
            sqlParams[0] = new SqlParameter("@CurrentPage", objHrCommon.CurrentPage);
            sqlParams[1] = new SqlParameter("@PageSize", objHrCommon.PageSize);
            sqlParams[2] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
            sqlParams[2].Direction = ParameterDirection.ReturnValue;
            sqlParams[3] = new SqlParameter("@NoofRecords", System.Data.SqlDbType.Int);
            sqlParams[3].Direction = ParameterDirection.Output;
            DataSet ds = SQLDBUtil.ExecuteDataset("HR_EmpReimEmployeesByPaging", sqlParams);
            objHrCommon.NoofRecords = (int)sqlParams[3].Value;
            objHrCommon.TotalPages = (int)sqlParams[2].Value;
            return ds;
        }
        public static DataSet HR_AbsentPenalitiesByPaging(HRCommon objHrCommon)
        {
            SqlParameter[] sqlParams = new SqlParameter[10];
            sqlParams[0] = new SqlParameter("@worksite", objHrCommon.SiteID);
            sqlParams[1] = new SqlParameter("@designation", objHrCommon.DesigID);
            sqlParams[2] = new SqlParameter("@deptno", objHrCommon.DeptID);
            sqlParams[3] = new SqlParameter("@month", objHrCommon.Month);
            sqlParams[4] = new SqlParameter("@year", objHrCommon.Year);
            sqlParams[5] = new SqlParameter("@EmpId", objHrCommon.EmpID);
            sqlParams[6] = new SqlParameter("@CurrentPage", objHrCommon.CurrentPage);
            sqlParams[7] = new SqlParameter("@PageSize", objHrCommon.PageSize);
            sqlParams[8] = new SqlParameter("@NoofRecords", 3);
            sqlParams[8].Direction = ParameterDirection.Output;
            sqlParams[9] = new SqlParameter();
            sqlParams[9].Direction = ParameterDirection.ReturnValue;
            DataSet ds = SQLDBUtil.ExecuteDataset("USP_HMS_AbsentPenalities", sqlParams);
            objHrCommon.NoofRecords = (int)sqlParams[8].Value;
            objHrCommon.TotalPages = (int)sqlParams[9].Value;
            return ds;
        }
        public static DataSet HR_EmpPenalityEmployeesByPaging(HRCommon objHrCommon)
        {
            SqlParameter[] sqlParams = new SqlParameter[4];
            sqlParams[0] = new SqlParameter("@CurrentPage", objHrCommon.CurrentPage);
            sqlParams[1] = new SqlParameter("@PageSize", objHrCommon.PageSize);
            sqlParams[2] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
            sqlParams[2].Direction = ParameterDirection.ReturnValue;
            sqlParams[3] = new SqlParameter("@NoofRecords", System.Data.SqlDbType.Int);
            sqlParams[3].Direction = ParameterDirection.Output;
            DataSet ds = SQLDBUtil.ExecuteDataset("HR_EmpPenality_EmployeesByPaging", sqlParams);
            objHrCommon.NoofRecords = (int)sqlParams[3].Value;
            objHrCommon.TotalPages = (int)sqlParams[2].Value;
            return ds;
        }
        public static DataSet HR_EmpReimburseAmtPayable(int EmpID)
        {
            try
            {
                SqlParameter[] objParam = new SqlParameter[1];
                objParam[0] = new SqlParameter("@EmpID", EmpID);
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_EmpReimburseAmtPayable", objParam);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet HR_EmpRecmndAmtPayable(int EmpID)
        {
            try
            {
                SqlParameter[] objParam = new SqlParameter[1];
                objParam[0] = new SqlParameter("@EmpID", EmpID);
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_EmpRecmndAmtPayable", objParam);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet HR_EmpFinalAmtPayable(int EmpID)
        {
            try
            {
                SqlParameter[] objParam = new SqlParameter[1];
                objParam[0] = new SqlParameter("@EmpID", EmpID);
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_EmpFinalAmtPayable", objParam);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet HR_Emp_Penalty_AmtPayable(int EmpID)
        {
            try
            {
                SqlParameter[] objParam = new SqlParameter[1];
                objParam[0] = new SqlParameter("@EmpID", EmpID);
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_EmpPenaltiesAmtPayable", objParam);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet HR_EmpReimEmployeesByEmpID(int EmpID)
        {
            try
            {
                SqlParameter[] objParam = new SqlParameter[1];
                objParam[0] = new SqlParameter("@EmpID", EmpID);
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_EmpReimEmployeesByEmpID", objParam);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet HR_Emp_Penality_ByEmpID(int EmpID)
        {
            try
            {
                SqlParameter[] objParam = new SqlParameter[1];
                objParam[0] = new SqlParameter("@EmpID", EmpID);
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_Emp_Penalty_ByEmpID", objParam);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static void HR_UpdateReimItems(int ReimburseID, int AUID, double Qty, double UnitRate, string Description, DateTime DateOfSpent, string Proof, int PK)
        {
            try
            {
                SqlParameter[] objParam = new SqlParameter[8];
                objParam[0] = new SqlParameter("@ReimburseID", ReimburseID);
                objParam[1] = new SqlParameter("@AUID", AUID);
                objParam[2] = new SqlParameter("@Qty", Qty);
                objParam[3] = new SqlParameter("@UnitRate", UnitRate);
                objParam[4] = new SqlParameter("@Description", Description);
                objParam[5] = new SqlParameter("@DateOfSpent", DateOfSpent);
                objParam[6] = new SqlParameter("@Proof", Proof);
                objParam[7] = new SqlParameter("@PK", PK);
                SQLDBUtil.ExecuteNonQuery("HR_UpdateReimItems", objParam);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static void HR_UpdateEmpPenalityItems(int ReimburseID, int AUID, double Qty, double UnitRate, string Description, DateTime DateOfSpent, string Proof, int PK)
        {
            try
            {
                SqlParameter[] objParam = new SqlParameter[8];
                objParam[0] = new SqlParameter("@ReimburseID", ReimburseID);
                objParam[1] = new SqlParameter("@AUID", AUID);
                objParam[2] = new SqlParameter("@Qty", Qty);
                objParam[3] = new SqlParameter("@UnitRate", UnitRate);
                objParam[4] = new SqlParameter("@Description", Description);
                objParam[5] = new SqlParameter("@DateOfSpent", DateOfSpent);
                objParam[6] = new SqlParameter("@Proof", Proof);
                objParam[7] = new SqlParameter("@PK", PK);
                SQLDBUtil.ExecuteNonQuery("HR_Update_Emp_Penalities_Items", objParam);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet HR_RecmndStatus(int PK)
        {
            try
            {
                SqlParameter[] objParam = new SqlParameter[1];
                objParam[0] = new SqlParameter("@PK", PK);
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_ARecmndStatus", objParam);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet HR_FinalApprvalStatus_Desc(int PK)
        {
            try
            {
                SqlParameter[] objParam = new SqlParameter[1];
                objParam[0] = new SqlParameter("@PK", PK);
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_FinalApprvalStatus_Desc", objParam);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet HR_ApproveStatus(int PK)
        {
            try
            {
                SqlParameter[] objParam = new SqlParameter[1];
                objParam[0] = new SqlParameter("@PK", PK);
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_ApproveStatus", objParam);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet HR_EmpPenality_ApproveStatus(int PK)
        {
            try
            {
                SqlParameter[] objParam = new SqlParameter[1];
                objParam[0] = new SqlParameter("@PK", PK);
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_Emp_Penlity_ApproveStatus", objParam);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet HR_EmpPenality_GMApproveStatus(int PK)
        {
            try
            {
                SqlParameter[] objParam = new SqlParameter[1];
                objParam[0] = new SqlParameter("@PK", PK);
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_Emp_Penlity_GMApproveStatus", objParam);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet HR_Emp_Penlity_CFoApproveStatus(int PK)
        {
            try
            {
                SqlParameter[] objParam = new SqlParameter[1];
                objParam[0] = new SqlParameter("@PK", PK);
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_Emp_Penlity_CFoApproveStatus", objParam);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet HR_EmpLedger(int CompanyID, int EmpID)
        {
            try
            {
                SqlParameter[] objParam = new SqlParameter[2];
                objParam[0] = new SqlParameter("@CompanyID", CompanyID);
                objParam[1] = new SqlParameter("@EmpId", EmpID);
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_EmpLedger", objParam);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet HR_LedgerIDOfReimItem(int RMItemId)
        {
            try
            {
                SqlParameter[] objParam = new SqlParameter[1];
                objParam[0] = new SqlParameter("@RMItemId", RMItemId);
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_LedgerIDOfReimItem", objParam);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static void HR_ReimMakeStatueApprove(int ERID)
        {
            try
            {
                SqlParameter[] objParam = new SqlParameter[1];
                objParam[0] = new SqlParameter("@ERID", ERID);
                SQLDBUtil.ExecuteNonQuery("HR_ReimMakeStatueApprove", objParam);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet HR_EmpLedID(int EmpID)
        {
            try
            {
                SqlParameter[] objParam = new SqlParameter[1];
                objParam[0] = new SqlParameter("@EmpID", EmpID);
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_EmpLedID", objParam);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet HR_EmpReimNotApprovedByEmpID(int EmpID)
        {
            try
            {
                SqlParameter[] objParam = new SqlParameter[1];
                objParam[0] = new SqlParameter("@EmpID", EmpID);
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_EmpReimNotApprovedByEmpID", objParam);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet HR_Emppenlity_NotApprovedByEmpID(int EmpID)
        {
            try
            {
                SqlParameter[] objParam = new SqlParameter[1];
                objParam[0] = new SqlParameter("@EmpID", EmpID);
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_EmpPenlty_NotApprovedByEmpID", objParam);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet HR_EmpReimNotApprovedByEmpIDByPaging(HRCommon objHrCommon, int EmpID, int CompanyID, int status)
        {
            SqlParameter[] sqlParams = new SqlParameter[7];
            sqlParams[0] = new SqlParameter("@CurrentPage", objHrCommon.CurrentPage);
            sqlParams[1] = new SqlParameter("@PageSize", objHrCommon.PageSize);
            sqlParams[2] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
            sqlParams[2].Direction = ParameterDirection.ReturnValue;
            sqlParams[3] = new SqlParameter("@NoofRecords", System.Data.SqlDbType.Int);
            sqlParams[3].Direction = ParameterDirection.Output;
            sqlParams[4] = new SqlParameter("@EmpID", EmpID);
            sqlParams[5] = new SqlParameter("@CompanyID", CompanyID);
            sqlParams[6] = new SqlParameter("@status", status);
            DataSet ds = SQLDBUtil.ExecuteDataset("HR_EmpReimNotApprovedByEmpIDByPaging", sqlParams);
            objHrCommon.NoofRecords = (int)sqlParams[3].Value;
            objHrCommon.TotalPages = (int)sqlParams[2].Value;
            return ds;
        }
        public static DataSet HR_EmpPenlityNotApprovedByEmpIDByPaging(HRCommon objHrCommon, int EmpID, int CompanyID)
        {
            SqlParameter[] sqlParams = new SqlParameter[6];
            sqlParams[0] = new SqlParameter("@CurrentPage", objHrCommon.CurrentPage);
            sqlParams[1] = new SqlParameter("@PageSize", objHrCommon.PageSize);
            sqlParams[2] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
            sqlParams[2].Direction = ParameterDirection.ReturnValue;
            sqlParams[3] = new SqlParameter("@NoofRecords", System.Data.SqlDbType.Int);
            sqlParams[3].Direction = ParameterDirection.Output;
            sqlParams[4] = new SqlParameter("@EmpID", EmpID);
            sqlParams[5] = new SqlParameter("@CompanyID", CompanyID);
            DataSet ds = SQLDBUtil.ExecuteDataset("HR_Emp_Penlity_NotApprovedByEmpIDByPaging", sqlParams);
            objHrCommon.NoofRecords = (int)sqlParams[3].Value;
            objHrCommon.TotalPages = (int)sqlParams[2].Value;
            return ds;
        }
        public static DataSet HR_EmpRecmnd()
        {
            try
            {
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_EmpRecmnd");
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet HR_EmpReimNotApproved()
        {
            try
            {
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_EmpReimNotApproved");
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet HR_EmpPenalties_NotApproved()
        {
            try
            {
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_EmpPenalties_NotApproved");
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet HR_EmpReimNotApprovedByPaging(HRCommon objHrCommon, int CompanyID)
        {
            SqlParameter[] sqlParams = new SqlParameter[5];
            sqlParams[0] = new SqlParameter("@CurrentPage", objHrCommon.CurrentPage);
            sqlParams[1] = new SqlParameter("@PageSize", objHrCommon.PageSize);
            sqlParams[2] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
            sqlParams[2].Direction = ParameterDirection.ReturnValue;
            sqlParams[3] = new SqlParameter("@NoofRecords", System.Data.SqlDbType.Int);
            sqlParams[3].Direction = ParameterDirection.Output;
            sqlParams[4] = new SqlParameter("@CompanyID", CompanyID);
            DataSet ds = SQLDBUtil.ExecuteDataset("HR_EmpReimNotApprovedByPaging", sqlParams);
            objHrCommon.NoofRecords = (int)sqlParams[3].Value;
            objHrCommon.TotalPages = (int)sqlParams[2].Value;
            return ds;
        }
        public static DataSet HR_Emp_Penalty_NotApprovedByPaging(HRCommon objHrCommon, int CompanyID)
        {
            SqlParameter[] sqlParams = new SqlParameter[5];
            sqlParams[0] = new SqlParameter("@CurrentPage", objHrCommon.CurrentPage);
            sqlParams[1] = new SqlParameter("@PageSize", objHrCommon.PageSize);
            sqlParams[2] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
            sqlParams[2].Direction = ParameterDirection.ReturnValue;
            sqlParams[3] = new SqlParameter("@NoofRecords", System.Data.SqlDbType.Int);
            sqlParams[3].Direction = ParameterDirection.Output;
            sqlParams[4] = new SqlParameter("@CompanyID", CompanyID);
            DataSet ds = SQLDBUtil.ExecuteDataset("HR_Emp_Penalty_NotApprovedByPaging", sqlParams);
            objHrCommon.NoofRecords = (int)sqlParams[3].Value;
            objHrCommon.TotalPages = (int)sqlParams[2].Value;
            return ds;
        }
        public static DataSet HR_EmpReimRejected()
        {
            try
            {
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_EmpReimRejected");
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet HR_EmpPanalityRejected()
        {
            try
            {
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_EmpPenalties_Rejected");
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet HR_EmpRecmndByPaging(HRCommon objHrCommon, int CompnayID)
        {
            SqlParameter[] sqlParams = new SqlParameter[5];
            sqlParams[0] = new SqlParameter("@CurrentPage", objHrCommon.CurrentPage);
            sqlParams[1] = new SqlParameter("@PageSize", objHrCommon.PageSize);
            sqlParams[2] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
            sqlParams[2].Direction = ParameterDirection.ReturnValue;
            sqlParams[3] = new SqlParameter("@NoofRecords", System.Data.SqlDbType.Int);
            sqlParams[3].Direction = ParameterDirection.Output;
            sqlParams[4] = new SqlParameter("@CompanyID", CompnayID);
            DataSet ds = SQLDBUtil.ExecuteDataset("HR_EmpRecmndByPaging", sqlParams);
            objHrCommon.NoofRecords = (int)sqlParams[3].Value;
            objHrCommon.TotalPages = (int)sqlParams[2].Value;
            return ds;
        }
        public static DataSet HR_EmpReimRejectedByPaging(HRCommon objHrCommon, int CompnayID)
        {
            SqlParameter[] sqlParams = new SqlParameter[5];
            sqlParams[0] = new SqlParameter("@CurrentPage", objHrCommon.CurrentPage);
            sqlParams[1] = new SqlParameter("@PageSize", objHrCommon.PageSize);
            sqlParams[2] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
            sqlParams[2].Direction = ParameterDirection.ReturnValue;
            sqlParams[3] = new SqlParameter("@NoofRecords", System.Data.SqlDbType.Int);
            sqlParams[3].Direction = ParameterDirection.Output;
            sqlParams[4] = new SqlParameter("@CompanyID", CompnayID);
            DataSet ds = SQLDBUtil.ExecuteDataset("HR_EmpReimRejectedByPaging", sqlParams);
            objHrCommon.NoofRecords = (int)sqlParams[3].Value;
            objHrCommon.TotalPages = (int)sqlParams[2].Value;
            return ds;
        }
        public static DataSet HR_EmpPenalityRejectedByPaging(HRCommon objHrCommon, int CompnayID)
        {
            SqlParameter[] sqlParams = new SqlParameter[5];
            sqlParams[0] = new SqlParameter("@CurrentPage", objHrCommon.CurrentPage);
            sqlParams[1] = new SqlParameter("@PageSize", objHrCommon.PageSize);
            sqlParams[2] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
            sqlParams[2].Direction = ParameterDirection.ReturnValue;
            sqlParams[3] = new SqlParameter("@NoofRecords", System.Data.SqlDbType.Int);
            sqlParams[3].Direction = ParameterDirection.Output;
            sqlParams[4] = new SqlParameter("@CompanyID", CompnayID);
            DataSet ds = SQLDBUtil.ExecuteDataset("HR_Emp_Penality_RejectedByPaging", sqlParams);
            objHrCommon.NoofRecords = (int)sqlParams[3].Value;
            objHrCommon.TotalPages = (int)sqlParams[2].Value;
            return ds;
        }
        public static DataSet HR_RejectedStatus(int PK, int UserID)
        {
            try
            {
                SqlParameter[] objParam = new SqlParameter[2];
                objParam[0] = new SqlParameter("@PK", PK);
                objParam[1] = new SqlParameter("@UserID", UserID);
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_RejectedStatus", objParam);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet HR_EmpPenlity_RejectedStatus(int PK, int UserID)
        {
            try
            {
                SqlParameter[] objParam = new SqlParameter[2];
                objParam[0] = new SqlParameter("@PK", PK);
                objParam[1] = new SqlParameter("@UserID", UserID);
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_EmpPanalty_RejectedStatus", objParam);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet HR_EmpReimburseViewAmtApproved(int ERID)
        {
            try
            {
                SqlParameter[] objParam = new SqlParameter[1];
                objParam[0] = new SqlParameter("@ERID", ERID);
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_EmpReimburseViewAmtApproved", objParam);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet HR_EmpPenality_ViewAmtApproved(int ERID)
        {
            try
            {
                SqlParameter[] objParam = new SqlParameter[1];
                objParam[0] = new SqlParameter("@ERID", ERID);
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_Emp_Penalities_ViewAmtApproved", objParam);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet HR_EmpReimburseAmtRejected(int ERID)
        {
            try
            {
                SqlParameter[] objParam = new SqlParameter[1];
                objParam[0] = new SqlParameter("@ERID", ERID);
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_EmpReimburseAmtRejected", objParam);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet HR_EmpPenality_AmtRejected(int ERID)
        {
            try
            {
                SqlParameter[] objParam = new SqlParameter[1];
                objParam[0] = new SqlParameter("@ERID", ERID);
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_EmpPenality_AmtRejected", objParam);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet HR_EmpReimburseRejectedView(int EmpID)
        {
            try
            {
                SqlParameter[] objParam = new SqlParameter[1];
                objParam[0] = new SqlParameter("@EmpID", EmpID);
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_EmpReimburseRejectedView", objParam);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet HR_EmpPenality_RejectedView(int EmpID)
        {
            try
            {
                SqlParameter[] objParam = new SqlParameter[1];
                objParam[0] = new SqlParameter("@EmpID", EmpID);
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_EmpPenalties_RejectedView", objParam);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet HR_SearchReimburseEmp()
        {
            try
            {
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_SearchReimburseEmp");
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet GetEmployeesByCompID(int CompanyID)
        {
            try
            {
                SqlParameter[] parm = new SqlParameter[1];
                parm[0] = new SqlParameter("@CompanyID", CompanyID);
                DataSet ds = SQLDBUtil.ExecuteDataset("GetEmployeesByCompID", parm);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet HR_EditReimburseItems(int PK)
        {
            try
            {
                SqlParameter[] objParam = new SqlParameter[1];
                objParam[0] = new SqlParameter("@PK", PK);
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_EditReimburseItems", objParam);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet HR_Edit_Emp_Penalities_Items(int PK)
        {
            try
            {
                SqlParameter[] objParam = new SqlParameter[1];
                objParam[0] = new SqlParameter("@PK", PK);
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_Edit_Emp_Penalties_Items", objParam);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet HR_EmpReimEmpTranserByEmpID(int EmpID)
        {
            try
            {
                SqlParameter[] objParam = new SqlParameter[1];
                objParam[0] = new SqlParameter("@EmpID", EmpID);
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_EmpReimEmpTranserByEmpID", objParam);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet HR_EmpReimEmpTranserByEmpIDByPaging(HRCommon objHrCommon, int? EmpID)
        {
            SqlParameter[] sqlParams = new SqlParameter[5];
            sqlParams[0] = new SqlParameter("@CurrentPage", objHrCommon.CurrentPage);
            sqlParams[1] = new SqlParameter("@PageSize", objHrCommon.PageSize);
            sqlParams[2] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
            sqlParams[2].Direction = ParameterDirection.ReturnValue;
            sqlParams[3] = new SqlParameter("@NoofRecords", System.Data.SqlDbType.Int);
            sqlParams[3].Direction = ParameterDirection.Output;
            sqlParams[4] = new SqlParameter("@EmpID", EmpID);
            DataSet ds = SQLDBUtil.ExecuteDataset("HR_EmpReimEmpTranserByEmpIDByPaging", sqlParams);
            objHrCommon.NoofRecords = (int)sqlParams[3].Value;
            objHrCommon.TotalPages = (int)sqlParams[2].Value;
            return ds;
        }
        public static DataSet HR_EmpPenlity_EmpTranserByEmpIDByPaging(HRCommon objHrCommon, int? EmpID)
        {
            SqlParameter[] sqlParams = new SqlParameter[5];
            sqlParams[0] = new SqlParameter("@CurrentPage", objHrCommon.CurrentPage);
            sqlParams[1] = new SqlParameter("@PageSize", objHrCommon.PageSize);
            sqlParams[2] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
            sqlParams[2].Direction = ParameterDirection.ReturnValue;
            sqlParams[3] = new SqlParameter("@NoofRecords", System.Data.SqlDbType.Int);
            sqlParams[3].Direction = ParameterDirection.Output;
            sqlParams[4] = new SqlParameter("@EmpID", EmpID);
            DataSet ds = SQLDBUtil.ExecuteDataset("HR_Emp_Penalty_EmpTranserByEmpIDByPaging", sqlParams);
            objHrCommon.NoofRecords = (int)sqlParams[3].Value;
            objHrCommon.TotalPages = (int)sqlParams[2].Value;
            return ds;
        }
        public static void HR_UpdateAsApproved(int UserID, int ERID)
        {
            try
            {
                SqlParameter[] objParam = new SqlParameter[2];
                objParam[0] = new SqlParameter("@UserID", UserID);
                objParam[1] = new SqlParameter("@ERID", ERID);
                SQLDBUtil.ExecuteNonQuery("HR_UpdateAsApproved", objParam);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static void HR_UpdateAsRecmnd(int UserID, int ERID)
        {
            try
            {
                SqlParameter[] objParam = new SqlParameter[2];
                objParam[0] = new SqlParameter("@UserID", UserID);
                objParam[1] = new SqlParameter("@ERID", ERID);
                SQLDBUtil.ExecuteNonQuery("HR_UpdateAsRecmnd", objParam);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static void HR_UpdateFinalAppr_reimb(int UserID, int ERID)
        {
            try
            {
                SqlParameter[] objParam = new SqlParameter[2];
                objParam[0] = new SqlParameter("@UserID", UserID);
                objParam[1] = new SqlParameter("@ERID", ERID);
                SQLDBUtil.ExecuteNonQuery("HR_UpdateFinalAppr_reimb", objParam);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static void HR_EmpPenality_UpdateAsApproved(int UserID, int ERID)
        {
            try
            {
                SqlParameter[] objParam = new SqlParameter[2];
                objParam[0] = new SqlParameter("@UserID", UserID);
                objParam[1] = new SqlParameter("@ERID", ERID);
                SQLDBUtil.ExecuteNonQuery("HR_EmpPenlity_UpdateAsApproved", objParam);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static void HR_EmpPenality_GMUpdateAsApproved(int UserID, int ERID)
        {
            try
            {
                SqlParameter[] objParam = new SqlParameter[2];
                objParam[0] = new SqlParameter("@UserID", UserID);
                objParam[1] = new SqlParameter("@ERID", ERID);
                SQLDBUtil.ExecuteNonQuery("HR_EmpPenlity_GMUpdateAsApproved", objParam);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static void HR_EmpPenlity_CFoUpdateAsApproved(int UserID, int ERID)
        {
            try
            {
                SqlParameter[] objParam = new SqlParameter[2];
                objParam[0] = new SqlParameter("@UserID", UserID);
                objParam[1] = new SqlParameter("@ERID", ERID);
                SQLDBUtil.ExecuteNonQuery("HR_EmpPenlity_CFoUpdateAsApproved", objParam);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static void HR_SetStatusTransfered(int ERID)
        {
            try
            {
                SqlParameter[] objParam = new SqlParameter[1];
                objParam[0] = new SqlParameter("@ERID", ERID);
                SQLDBUtil.ExecuteNonQuery("HR_SetStatusTransfered", objParam);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static void HR_EmpPenlitySetStatusTransfered(int ERID)
        {
            try
            {
                SqlParameter[] objParam = new SqlParameter[1];
                objParam[0] = new SqlParameter("@ERID", ERID);
                SQLDBUtil.ExecuteNonQuery("HR_Emp_Penality_SetStatusTransfered", objParam);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet HR_ReimburseTransferd()
        {
            try
            {
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_ReimburseTransferd");
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet HR_Emp_Penalty_Transferd()
        {
            try
            {
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_Employee_Penalty_Transferd");
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet HR_ReimburseTransferdByPaging(HRCommon objHrCommon, int CompanyID)
        {
            SqlParameter[] sqlParams = new SqlParameter[5];
            sqlParams[0] = new SqlParameter("@CurrentPage", objHrCommon.CurrentPage);
            sqlParams[1] = new SqlParameter("@PageSize", objHrCommon.PageSize);
            sqlParams[2] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
            sqlParams[2].Direction = ParameterDirection.ReturnValue;
            sqlParams[3] = new SqlParameter("@NoofRecords", System.Data.SqlDbType.Int);
            sqlParams[3].Direction = ParameterDirection.Output;
            sqlParams[4] = new SqlParameter("@CompanyID", CompanyID);
            DataSet ds = SQLDBUtil.ExecuteDataset("HR_ReimburseTransferdByPaging", sqlParams);
            objHrCommon.NoofRecords = (int)sqlParams[3].Value;
            objHrCommon.TotalPages = (int)sqlParams[2].Value;
            return ds;
        }
        public static DataSet HR_Emp_PenlityTransferdByPaging(HRCommon objHrCommon, int CompanyID)
        {
            SqlParameter[] sqlParams = new SqlParameter[5];
            sqlParams[0] = new SqlParameter("@CurrentPage", objHrCommon.CurrentPage);
            sqlParams[1] = new SqlParameter("@PageSize", objHrCommon.PageSize);
            sqlParams[2] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
            sqlParams[2].Direction = ParameterDirection.ReturnValue;
            sqlParams[3] = new SqlParameter("@NoofRecords", System.Data.SqlDbType.Int);
            sqlParams[3].Direction = ParameterDirection.Output;
            sqlParams[4] = new SqlParameter("@CompanyID", CompanyID);
            DataSet ds = SQLDBUtil.ExecuteDataset("HR_PenlatityTransferdByPaging", sqlParams);
            objHrCommon.NoofRecords = (int)sqlParams[3].Value;
            objHrCommon.TotalPages = (int)sqlParams[2].Value;
            return ds;
        }
        public static DataSet HR_EmpReimburseViewTransfered(int ERID)
        {
            try
            {
                SqlParameter[] objParam = new SqlParameter[1];
                objParam[0] = new SqlParameter("@ERID", ERID);
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_EmpReimburseViewTransfered", objParam);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet HR_EmpPenlity_ViewTransfered(int ERID)
        {
            try
            {
                SqlParameter[] objParam = new SqlParameter[1];
                objParam[0] = new SqlParameter("@ERID", ERID);
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_Emp_Penalty_ViewTransfered", objParam);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet HR_EmpReimRejectedByEmpID(int EmpID)
        {
            try
            {
                SqlParameter[] objParam = new SqlParameter[1];
                objParam[0] = new SqlParameter("@EmpID", EmpID);
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_EmpReimRejectedByEmpID", objParam);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet HR_EmpPenlity_RejectedByEmpID(int EmpID)
        {
            try
            {
                SqlParameter[] objParam = new SqlParameter[1];
                objParam[0] = new SqlParameter("@EmpID", EmpID);
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_Emp_Penality_RejectedByEmpID", objParam);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet HR_EmpRecmndByEmpIDByPaging(HRCommon objHrCommon, int EmpID, int CompanyID)
        {
            SqlParameter[] sqlParams = new SqlParameter[6];
            sqlParams[0] = new SqlParameter("@CurrentPage", objHrCommon.CurrentPage);
            sqlParams[1] = new SqlParameter("@PageSize", objHrCommon.PageSize);
            sqlParams[2] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
            sqlParams[2].Direction = ParameterDirection.ReturnValue;
            sqlParams[3] = new SqlParameter("@NoofRecords", System.Data.SqlDbType.Int);
            sqlParams[3].Direction = ParameterDirection.Output;
            sqlParams[4] = new SqlParameter("@EmpID", EmpID);
            sqlParams[5] = new SqlParameter("@CompanyID", CompanyID);
            DataSet ds = SQLDBUtil.ExecuteDataset("HR_EmpRecmndByEmpIDByPaging", sqlParams);
            objHrCommon.NoofRecords = (int)sqlParams[3].Value;
            objHrCommon.TotalPages = (int)sqlParams[2].Value;
            return ds;
        }
        public static DataSet HR_EmpReimRejectedByEmpIDByPaging(HRCommon objHrCommon, int EmpID, int CompanyID)
        {
            SqlParameter[] sqlParams = new SqlParameter[6];
            sqlParams[0] = new SqlParameter("@CurrentPage", objHrCommon.CurrentPage);
            sqlParams[1] = new SqlParameter("@PageSize", objHrCommon.PageSize);
            sqlParams[2] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
            sqlParams[2].Direction = ParameterDirection.ReturnValue;
            sqlParams[3] = new SqlParameter("@NoofRecords", System.Data.SqlDbType.Int);
            sqlParams[3].Direction = ParameterDirection.Output;
            sqlParams[4] = new SqlParameter("@EmpID", EmpID);
            sqlParams[5] = new SqlParameter("@CompanyID", CompanyID);
            DataSet ds = SQLDBUtil.ExecuteDataset("HR_EmpReimRejectedByEmpIDByPaging", sqlParams);
            objHrCommon.NoofRecords = (int)sqlParams[3].Value;
            objHrCommon.TotalPages = (int)sqlParams[2].Value;
            return ds;
        }
        public static DataSet HR_EmpPenlity_RejectedByEmpIDByPaging(HRCommon objHrCommon, int EmpID, int CompanyID)
        {
            SqlParameter[] sqlParams = new SqlParameter[6];
            sqlParams[0] = new SqlParameter("@CurrentPage", objHrCommon.CurrentPage);
            sqlParams[1] = new SqlParameter("@PageSize", objHrCommon.PageSize);
            sqlParams[2] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
            sqlParams[2].Direction = ParameterDirection.ReturnValue;
            sqlParams[3] = new SqlParameter("@NoofRecords", System.Data.SqlDbType.Int);
            sqlParams[3].Direction = ParameterDirection.Output;
            sqlParams[4] = new SqlParameter("@EmpID", EmpID);
            sqlParams[5] = new SqlParameter("@CompanyID", CompanyID);
            DataSet ds = SQLDBUtil.ExecuteDataset("HR_Emp_Penalty_RejectedByEmpIDByPaging", sqlParams);
            objHrCommon.NoofRecords = (int)sqlParams[3].Value;
            objHrCommon.TotalPages = (int)sqlParams[2].Value;
            return ds;
        }
        public static DataSet HR_ReimburseTransferdByEmpID(int EmpID)
        {
            try
            {
                SqlParameter[] objParam = new SqlParameter[1];
                objParam[0] = new SqlParameter("@EmpID", EmpID);
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_ReimburseTransferdByEmpID", objParam);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet HR_EmpPenlity_TransferdByEmpID(int EmpID)
        {
            try
            {
                SqlParameter[] objParam = new SqlParameter[1];
                objParam[0] = new SqlParameter("@EmpID", EmpID);
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_Emp_Penalty_TransferdByEmpID", objParam);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet HR_ReimburseTransferdByEmpIDByPaging(HRCommon objHrCommon, int EmpID, int CompanyID)
        {
            SqlParameter[] sqlParams = new SqlParameter[6];
            sqlParams[0] = new SqlParameter("@CurrentPage", objHrCommon.CurrentPage);
            sqlParams[1] = new SqlParameter("@PageSize", objHrCommon.PageSize);
            sqlParams[2] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
            sqlParams[2].Direction = ParameterDirection.ReturnValue;
            sqlParams[3] = new SqlParameter("@NoofRecords", System.Data.SqlDbType.Int);
            sqlParams[3].Direction = ParameterDirection.Output;
            sqlParams[4] = new SqlParameter("@EmpID", EmpID);
            sqlParams[5] = new SqlParameter("@CompanyID", CompanyID);
            DataSet ds = SQLDBUtil.ExecuteDataset("HR_ReimburseTransferdByEmpIDByPaging", sqlParams);
            objHrCommon.NoofRecords = (int)sqlParams[3].Value;
            objHrCommon.TotalPages = (int)sqlParams[2].Value;
            return ds;
        }
        public static DataSet HR_Emp_Penlity_TransferdByEmpIDByPaging(HRCommon objHrCommon, int EmpID, int CompanyID)
        {
            SqlParameter[] sqlParams = new SqlParameter[6];
            sqlParams[0] = new SqlParameter("@CurrentPage", objHrCommon.CurrentPage);
            sqlParams[1] = new SqlParameter("@PageSize", objHrCommon.PageSize);
            sqlParams[2] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
            sqlParams[2].Direction = ParameterDirection.ReturnValue;
            sqlParams[3] = new SqlParameter("@NoofRecords", System.Data.SqlDbType.Int);
            sqlParams[3].Direction = ParameterDirection.Output;
            sqlParams[4] = new SqlParameter("@EmpID", EmpID);
            sqlParams[5] = new SqlParameter("@CompanyID", CompanyID);
            DataSet ds = SQLDBUtil.ExecuteDataset("HR_ReimburseTransferdByEmpIDByPaging", sqlParams);
            objHrCommon.NoofRecords = (int)sqlParams[3].Value;
            objHrCommon.TotalPages = (int)sqlParams[2].Value;
            return ds;
        }
        public static void HR_ReimburseRejectReason(string Reason, int PK)
        {
            try
            {
                SqlParameter[] objParam = new SqlParameter[2];
                objParam[0] = new SqlParameter("@Reason", Reason);
                objParam[1] = new SqlParameter("@PK", PK);
                SQLDBUtil.ExecuteNonQuery("HR_ReimburseRejectReason", objParam);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static void HR_EmpPenalityRejectReason(string Reason, int PK)
        {
            try
            {
                SqlParameter[] objParam = new SqlParameter[2];
                objParam[0] = new SqlParameter("@Reason", Reason);
                objParam[1] = new SqlParameter("@PK", PK);
                SQLDBUtil.ExecuteNonQuery("HR_EmpPenalty_RejectReason", objParam);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet HR_GetReason(int PK)
        {
            try
            {
                SqlParameter[] objParam = new SqlParameter[1];
                objParam[0] = new SqlParameter("@PK", PK);
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetReason", objParam);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet HR_ReimburseStatusDetails(int EmpID, int Status)
        {
            try
            {
                SqlParameter[] objParam = new SqlParameter[2];
                objParam[0] = new SqlParameter("@EmpID", EmpID);
                objParam[1] = new SqlParameter("@Status", Status);
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_ReimburseStatusDetails", objParam);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet HR_EmpReimburseStatus(int EmpID)
        {
            try
            {
                SqlParameter[] objParam = new SqlParameter[1];
                objParam[0] = new SqlParameter("@EmpID", EmpID);
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_EmpReimburseStatus", objParam);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet HR_EmpReimburseStatusByEmpIDByPaging(HRCommon objHrCommon, int EmpID)
        {
            SqlParameter[] sqlParams = new SqlParameter[5];
            sqlParams[0] = new SqlParameter("@CurrentPage", objHrCommon.CurrentPage);
            sqlParams[1] = new SqlParameter("@PageSize", objHrCommon.PageSize);
            sqlParams[2] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
            sqlParams[2].Direction = ParameterDirection.ReturnValue;
            sqlParams[3] = new SqlParameter("@NoofRecords", System.Data.SqlDbType.Int);
            sqlParams[3].Direction = ParameterDirection.Output;
            sqlParams[4] = new SqlParameter("@EmpID", EmpID);
            DataSet ds = SQLDBUtil.ExecuteDataset("HR_EmpReimburseStatusByUserIDByPaging", sqlParams);
            objHrCommon.NoofRecords = (int)sqlParams[3].Value;
            objHrCommon.TotalPages = (int)sqlParams[2].Value;
            return ds;
        }
        #endregion
        #region Advances&Loan
        public static DataSet HR_InsLoanDetailsXML(DataSet dsLoanDetail)
        {
            try
            {
                return SQLDBUtil.ExecuteDataset("HR_InsLoanDetailsXML", new SqlParameter[] { new SqlParameter("@TLoanDetails", dsLoanDetail.GetXml()) });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet HR_FilterSearch(string EmpName)
        {
            try
            {
                SqlParameter[] objParam = new SqlParameter[1];
                objParam[0] = new SqlParameter("@EmpName", EmpName);
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_FilterSearch", objParam);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet HR_GetEmpSal(int EmpID)
        {
            try
            {
                SqlParameter[] objParam = new SqlParameter[1];
                objParam[0] = new SqlParameter("@EmpID", EmpID);
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetEmpSal", objParam);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static void HR_InsUpLoans(int LoanID, int EmpID, Double LoanAmount, int LoanType, DateTime LoanIssuedDate, int RecoverYear, int RecoverMonth, int NoofEMIs, double InstallmentAmt, double InterestRate, int TransID, int RecoveryType, int IssuedBy, int PaymentType, int PaidLedgerID, double ServiceTax, int InstallmentType)
        {
            try
            {
                SqlParameter[] objParam = new SqlParameter[17];
                objParam[0] = new SqlParameter("@LoanID", LoanID);
                objParam[1] = new SqlParameter("@EmpID", EmpID);
                objParam[2] = new SqlParameter("@LoanAmount", LoanAmount);
                objParam[3] = new SqlParameter("@LoanType", LoanType);
                objParam[4] = new SqlParameter("@LoanIssuedDate", LoanIssuedDate);
                objParam[5] = new SqlParameter("@RecoverYear", RecoverYear);
                objParam[6] = new SqlParameter("@RecoverMonth", RecoverMonth);
                objParam[7] = new SqlParameter("@NoofEMIs", NoofEMIs);
                objParam[8] = new SqlParameter("@InstallmentAmt", InstallmentAmt);
                objParam[9] = new SqlParameter("@InterestRate", InterestRate);
                objParam[10] = new SqlParameter("@TransID", TransID);
                objParam[11] = new SqlParameter("@RecoveryType", RecoveryType);
                objParam[12] = new SqlParameter("@IssuedBy", IssuedBy);
                objParam[13] = new SqlParameter("@PaymentType", PaymentType);
                objParam[14] = new SqlParameter("@PaidLedgerID", PaidLedgerID);
                objParam[15] = new SqlParameter("@ServiceTax", ServiceTax);
                objParam[16] = new SqlParameter("@InstallmentType", InstallmentType);
                SQLDBUtil.ExecuteNonQuery("HR_InsUpLoans", objParam);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int HR_InsUpLoansReturnLoanID(int LoanID, int EmpID, double LoanAmount, int LoanType, DateTime LoanIssuedDate, int RecoverYear,
                                                int RecoverMonth, int NoofEMIs, double InstallmentAmt, double InterestRate, int TransID, int RecoveryType,
                                                    int IssuedBy, double ServiceTax, int InstallmentType, string Remarks,string ext)
        {
            try
            {
                SqlParameter[] objParam = new SqlParameter[18];
                objParam[0] = new SqlParameter("@LoanID", LoanID);
                objParam[1] = new SqlParameter("@EmpID", EmpID);
                objParam[2] = new SqlParameter("@LoanAmount", LoanAmount);
                objParam[3] = new SqlParameter("@LoanType", LoanType);
                objParam[4] = new SqlParameter("@LoanIssuedDate", LoanIssuedDate);
                objParam[5] = new SqlParameter("@RecoverYear", RecoverYear);
                objParam[6] = new SqlParameter("@RecoverMonth", RecoverMonth);
                objParam[7] = new SqlParameter("@NoofEMIs", NoofEMIs);
                objParam[8] = new SqlParameter("@InstallmentAmt", InstallmentAmt);
                objParam[9] = new SqlParameter("@InterestRate", InterestRate);
                objParam[10] = new SqlParameter("@TransID", TransID);
                objParam[11] = new SqlParameter("@RecoveryType", RecoveryType);
                objParam[12] = new SqlParameter("@IssuedBy", IssuedBy);
                objParam[13] = new SqlParameter("@ServiceTax", ServiceTax);
                objParam[14] = new SqlParameter("@InstallmentType", InstallmentType);
                objParam[15] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
                objParam[15].Direction = ParameterDirection.ReturnValue;
                objParam[16] = new SqlParameter("@LoanRemarks", Remarks);
                objParam[17] = new SqlParameter("@ext", ext);
                SQLDBUtil.ExecuteNonQuery("HR_InsUpLoansReturnLoanID", objParam);
                return Convert.ToInt32(objParam[15].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int HR_InsUpLoansReturnLoanID_AmntReqOn(int LoanID, int EmpID, double LoanAmount, int LoanType, DateTime LoanIssuedDate, int RecoverYear, int RecoverMonth, int NoofEMIs, double InstallmentAmt, double InterestRate, int TransID, int RecoveryType, int IssuedBy, double ServiceTax, int InstallmentType, DateTime AmountReqOn, string loanRemarks,string ext)
        {
            try
            {
                SqlParameter[] objParam = new SqlParameter[19];
                objParam[0] = new SqlParameter("@LoanID", LoanID);
                objParam[1] = new SqlParameter("@EmpID", EmpID);
                objParam[2] = new SqlParameter("@LoanAmount", LoanAmount);
                objParam[3] = new SqlParameter("@LoanType", LoanType);
                objParam[4] = new SqlParameter("@LoanIssuedDate", LoanIssuedDate);
                objParam[5] = new SqlParameter("@RecoverYear", RecoverYear);
                objParam[6] = new SqlParameter("@RecoverMonth", RecoverMonth);
                objParam[7] = new SqlParameter("@NoofEMIs", NoofEMIs);
                objParam[8] = new SqlParameter("@InstallmentAmt", InstallmentAmt);
                objParam[9] = new SqlParameter("@InterestRate", InterestRate);
                objParam[10] = new SqlParameter("@TransID", TransID);
                objParam[11] = new SqlParameter("@RecoveryType", RecoveryType);
                objParam[12] = new SqlParameter("@IssuedBy", IssuedBy);
                objParam[13] = new SqlParameter("@ServiceTax", ServiceTax);
                objParam[14] = new SqlParameter("@InstallmentType", InstallmentType);
                objParam[16] = new SqlParameter("@AmountReqOn", AmountReqOn);
                objParam[15] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
                objParam[15].Direction = ParameterDirection.ReturnValue;
                objParam[17] = new SqlParameter("@LoanRemarks", loanRemarks);
                objParam[18] = new SqlParameter("@ext", ext);
                SQLDBUtil.ExecuteNonQuery("HR_InsUpLoansReturnLoanID_AmntReqOn", objParam);
                return Convert.ToInt32(objParam[15].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet HR_GetEmpLoans(int EmpID)
        {
            try
            {
                SqlParameter[] objParam = new SqlParameter[1];
                objParam[0] = new SqlParameter("@EmpID", EmpID);
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetEmpLoans", objParam);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet HR_GetLoanDetailsByID(int LoanID, int UserId, string CompanyID)
        {
            try
            {
                SqlParameter[] objParam = new SqlParameter[3];
                objParam[0] = new SqlParameter("@LoanID", LoanID);
                objParam[1] = new SqlParameter("@UserId", UserId);
                objParam[2] = new SqlParameter("@CompanyID", Convert.ToInt32(CompanyID));
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetLoanDetailsByID", objParam);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet HR_GetReduecingLoans()
        {
            try
            {
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetReduecingLoans");
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet HR_GetLoans()
        {
            try
            {
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetLoans");
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static void HR_DelLoan(int LoanID)
        {
            try
            {
                SqlParameter[] objParam = new SqlParameter[1];
                objParam[0] = new SqlParameter("@LoanID", LoanID);
                SQLDBUtil.ExecuteNonQuery("HR_DelLoan", objParam);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet HR_GetLoanDetails(int LoanID)
        {
            try
            {
                SqlParameter[] objParam = new SqlParameter[1];
                objParam[0] = new SqlParameter("@LoanID", LoanID);
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetLoanDetails", objParam);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static void ChangeTransStatus(int TransID)
        {
            try
            {
                SqlParameter[] objParam = new SqlParameter[1];
                objParam[0] = new SqlParameter("@TransID", TransID);
                SQLDBUtil.ExecuteNonQuery("HR_UpdateTransStatus", objParam);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet HR_GetEmpIDByLoanID(int LoanID)
        {
            try
            {
                SqlParameter[] objParam = new SqlParameter[1];
                objParam[0] = new SqlParameter("@LoanID", LoanID);
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetEmpIDByLoanID", objParam);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int HR_InsUpAdvanceTransaction(int TransID, DateTime TransTime, string Remarks, int CompanyID, int EmpID, int UserId, Double Amount)
        {
            try
            {
                SqlParameter[] objParam = new SqlParameter[7];
                objParam[0] = new SqlParameter("@TransID", TransID);
                objParam[1] = new SqlParameter("@TransTime", TransTime);
                objParam[2] = new SqlParameter("@Remarks", Remarks);
                objParam[3] = new SqlParameter("@CompanyID", CompanyID);
                objParam[4] = new SqlParameter("@UserId", UserId);
                objParam[5] = new SqlParameter("@EmpID", EmpID);
                objParam[6] = new SqlParameter("@Amount", Amount);
                objParam[0].Direction = ParameterDirection.InputOutput;
                SQLDBUtil.ExecuteNonQuery("HR_InsUpAdvanceTransaction", objParam);
                return Convert.ToInt32(objParam[0].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet HR_VocherID(int CompanyID, int EmpID)
        {
            try
            {
                SqlParameter[] objParam = new SqlParameter[2];
                objParam[0] = new SqlParameter("@CompanyID", CompanyID);
                objParam[1] = new SqlParameter("@EmpId", EmpID);
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_VocherID", objParam);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet HR_getTransID(int LoanID)
        {
            try
            {
                SqlParameter[] objParam = new SqlParameter[1];
                objParam[0] = new SqlParameter("@LoanID", LoanID);
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_getTransID", objParam);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet HR_GetCostCenters()
        {
            try
            {
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetCostCenters");
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet HR_CashLedgers()
        {
            try
            {
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_CashLedgers");
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet HR_BankLedgers()
        {
            try
            {
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_BankLedgers");
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet HR_GetEmpAdvaces(int WS, int Dept, int EmpID, string EmpName, int Status, HRCommon objHrCommon, int LoanType, int InstallmentType, int CompanyID)
        {
            try
            {
                SqlParameter[] objParam = new SqlParameter[12];
                objParam[0] = new SqlParameter("@WS", WS);
                objParam[1] = new SqlParameter("@Dept", Dept);
                objParam[2] = new SqlParameter("@EmpID", EmpID);
                objParam[3] = new SqlParameter("@EmpName", EmpName);
                objParam[4] = new SqlParameter("@CurrentPage", objHrCommon.CurrentPage);
                objParam[5] = new SqlParameter("@PageSize", objHrCommon.PageSize);
                objParam[6] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
                objParam[6].Direction = ParameterDirection.ReturnValue;
                objParam[7] = new SqlParameter("@NoofRecords", System.Data.SqlDbType.Int);
                objParam[7].Direction = ParameterDirection.Output;
                objParam[8] = new SqlParameter("@Status", Status);//1-unRecoverd , 0-recoverd
                objParam[9] = new SqlParameter("@LoanType", LoanType);//1-Advance , 2-Loan
                objParam[10] = new SqlParameter("@InstallmentType", InstallmentType);//1-Flat , 2-Reduced
                objParam[11] = new SqlParameter("@CompanyID", CompanyID);
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetEmpAdvaces", objParam);
                objHrCommon.NoofRecords = (int)objParam[7].Value;
                objHrCommon.TotalPages = (int)objParam[6].Value;
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet HR_GetEmpAdvacesApproval(int WS, int Dept, int EmpID, string EmpName, int Status, HRCommon objHrCommon, int CompanyID,int appstatus)
        {
            try
            {
                SqlParameter[] objParam = new SqlParameter[11];
                objParam[0] = new SqlParameter("@WS", WS);
                objParam[1] = new SqlParameter("@Dept", Dept);
                objParam[2] = new SqlParameter("@EmpID", EmpID);
                objParam[3] = new SqlParameter("@EmpName", EmpName);
                objParam[4] = new SqlParameter("@CurrentPage", objHrCommon.CurrentPage);
                objParam[5] = new SqlParameter("@PageSize", objHrCommon.PageSize);
                objParam[6] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
                objParam[6].Direction = ParameterDirection.ReturnValue;
                objParam[7] = new SqlParameter("@NoofRecords", System.Data.SqlDbType.Int);
                objParam[7].Direction = ParameterDirection.Output;
                objParam[8] = new SqlParameter("@Status", Status);//1 Pending,2 Approved ,3 Rejected  
                objParam[9] = new SqlParameter("@CompanyID", CompanyID);
                objParam[10] = new SqlParameter("@appstatus", appstatus);
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetEmpAdvacesApproval", objParam);
                objHrCommon.NoofRecords = (int)objParam[7].Value;
                objHrCommon.TotalPages = (int)objParam[6].Value;
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static void HR_UpdAdvStatus(int LoanID, int Status, int RecmndBy,string Remarks)
        {
            SqlParameter[] parm = new SqlParameter[4];
            parm[0] = new SqlParameter("@LoanID", LoanID);
            parm[1] = new SqlParameter("@Status", Status);
            parm[2] = new SqlParameter("@RecomndBy", RecmndBy);
            parm[3] = new SqlParameter("@Remarks", Remarks);
            SQLDBUtil.ExecuteNonQuery("HR_UpdAdvStatus", parm);
        }
        public static DataSet HR_GetEmpLoanDetatils(int LoanID)
        {
            SqlParameter[] parm = new SqlParameter[1];
            parm[0] = new SqlParameter("@LoanID", LoanID);
            DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetEmpLoanDetatils", parm);
            return ds;
        }
        public void HR_UpdateLoanTransID(int LoanID, int TransID)
        {
            SqlParameter[] parm = new SqlParameter[2];
            parm[0] = new SqlParameter("@LoanID", LoanID);
            parm[1] = new SqlParameter("@TransID", TransID);
            SQLDBUtil.ExecuteNonQuery("HR_UpdateLoanTransID", parm);
        }
        public static DataSet HR_GetEmpTravelAdvaces()
        {
            try
            {
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetEmpTravelAdvaces");
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet HR_GetFlatLoans()
        {
            try
            {
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetFlatLoans");
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Employee Roles
        public static DataSet GetPerferences(int EmpID, int ModuleID, int MenuID)
        {
            SqlParameter[] p = new SqlParameter[3];
            p[0] = new SqlParameter("@EmpID", EmpID);
            p[1] = new SqlParameter("@ModuleID", ModuleID);
            p[2] = new SqlParameter("@MenuID", MenuID);
            DataSet ds = SQLDBUtil.ExecuteDataset("ACC_GetPreferences", p);
            return ds;
        }
        #endregion
        #region MMSpages
        public DataSet GetMMS_SRN_gvItemDetails(int SRNID, int Id)
        {
            try
            {
                return SQLDBUtil.ExecuteDataset("[MMS_SRN_gvItemDetails]", new SqlParameter[] { new SqlParameter("@SRNID", SRNID), new SqlParameter("@Id", Id) });
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public DataSet GetMMS_MIS_lstWODetails(int VendorID, int ProjectId, int PageId)
        {
            try
            {
                return SQLDBUtil.ExecuteDataset("MMS_MIS_lstWODetails", new SqlParameter[] { new SqlParameter("@VendorID", VendorID), new SqlParameter("@ProjectId", ProjectId), new SqlParameter("@PageId", PageId) });
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public DataSet MMS_DDL_SearchVendor(string SearchKeyWord)
        {
            try
            {
                return SQLDBUtil.ExecuteDataset("MMS_DDL_SearchVendor", new SqlParameter[] { new SqlParameter("@SEARCH", SearchKeyWord) });
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public DataSet MMS_DDL_SearchVehicle(string SearchKeyWord)
        {
            try
            {
                return SQLDBUtil.ExecuteDataset("MMS_DDL_SearchVehicle", new SqlParameter[] { new SqlParameter("@SEARCH", SearchKeyWord) });
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public DataSet GetMMS_MIS_lstItemDetails(int ResourceID, int PageId)
        {
            try
            {
                return SQLDBUtil.ExecuteDataset("MMS_MIS_lstItemDetails", new SqlParameter[] { new SqlParameter("@ResourceID", ResourceID), new SqlParameter("@PageId", PageId) });
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public DataSet GetMMS_SRN_Grid(int SRNID)
        {
            try
            {
                return SQLDBUtil.ExecuteDataset("MMS_EditSRN", new SqlParameter[] { new SqlParameter("@SRNID", SRNID) });
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public DataSet GetMMS_SRN_EditlstItemDetails(int RecordIDText, int PageId)
        {
            try
            {
                return SQLDBUtil.ExecuteDataset("[MMS_SRN_EditlstItemDetails]", new SqlParameter[] { new SqlParameter("@RecordIDText", RecordIDText), new SqlParameter("@PageId", PageId) });
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public DataSet GetMMS_SRN_EditlstPODetails(int RecordIDText, int PageId)
        {
            try
            {
                return SQLDBUtil.ExecuteDataset("MMS_SRN_EditlstPODetails", new SqlParameter[] { new SqlParameter("@RecordIDText", RecordIDText), new SqlParameter("@PageId", PageId) });
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public DataSet GetMMS_GDNPurchaseOrderdetails(int PODetails, int ITEMID)
        {
            try
            {
                SqlParameter[] sqlPrms = new SqlParameter[2];
                sqlPrms[0] = new SqlParameter("@PODetails", PODetails);
                sqlPrms[1] = new SqlParameter("@ITEMID", ITEMID);
                return SQLDBUtil.ExecuteDataset("MMS_GDNPurchaseOrderdetails", sqlPrms);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public DataSet MMS_GetSrnItems(int SrnId, int ID)
        {
            try
            {
                SqlParameter[] sqlPrms = new SqlParameter[2];
                sqlPrms[0] = new SqlParameter("@SrnId", SrnId);
                sqlPrms[1] = new SqlParameter("@ID", ID);
                return SQLDBUtil.ExecuteDataset("MMS_GetSrnItems", sqlPrms);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public void MMS_UpdateSrnItems(int SrnItemId, int ID, decimal RcvdQty, int RcvdBy, string Comments, int Distance, int EmpId)
        {
            try
            {
                SqlParameter[] sqlPrms = new SqlParameter[7];
                sqlPrms[0] = new SqlParameter("@SrnItemId", SrnItemId);
                sqlPrms[1] = new SqlParameter("@Id", ID);
                sqlPrms[2] = new SqlParameter("@RcvdQty", RcvdQty);
                sqlPrms[3] = new SqlParameter("@RecivedBy", RcvdBy);
                sqlPrms[4] = new SqlParameter("@Comments", Comments);
                if (Distance != 0)
                    sqlPrms[5] = new SqlParameter("@Distance", Distance);
                else
                    sqlPrms[5] = new SqlParameter("@Distance", System.Data.SqlDbType.Int);
                sqlPrms[6] = new SqlParameter("@EmpId", EmpId);
                SQLDBUtil.ExecuteNonQuery("MMS_UpdateSrnItems", sqlPrms);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public void MMS_SRN_SendBack(int SRNID)
        {
            try
            {
                SQLDBUtil.ExecuteNonQuery("MMS_SRN_SendBack", new SqlParameter[] { new SqlParameter("@SRNID", SRNID) });
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public DataSet MMS_SRN_ViewItems(int SRNID)
        {
            try
            {
                return SQLDBUtil.ExecuteDataset("[MMS_SRN_ViewItems]", new SqlParameter[] { new SqlParameter("@SRNID", SRNID) });
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public void MMS_DeleteSRN(int SRNID)
        {
            try
            {
                SQLDBUtil.ExecuteNonQuery("[MMS_DeleteSRN]", new SqlParameter[] { new SqlParameter("@SRNID", SRNID) });
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public DataSet MMS_CheckPicInSRNNItems(int SRNID)
        {
            try
            {
                return SQLDBUtil.ExecuteDataset("MMS_CheckPicInSRNNItems", new SqlParameter[] { new SqlParameter("@SRNID", SRNID) });
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public DataSet MMS_gvSrnItemDetails(int SRNID)
        {
            try
            {
                return SQLDBUtil.ExecuteDataset("MMS_gvSrnItemDetails", new SqlParameter[] { new SqlParameter("@SRNID", SRNID) });
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public DataSet MMS_SrnGridItemDetailsFupload(int RecordIDText)
        {
            try
            {
                return SQLDBUtil.ExecuteDataset("MMS_SrnGridItemDetailsFupload", new SqlParameter[] { new SqlParameter("@RecordIDText", RecordIDText) });
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public DataSet MMS_gvAllSRNsBind(DataSet dsSRNs, int Id)
        {
            try
            {
                return SQLDBUtil.ExecuteDataset("MMS_gvAllSRNsBind", new SqlParameter[] { new SqlParameter("@SRNIDs", dsSRNs.GetXml()), new SqlParameter("@Id", Id) });
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public void MMS_InsertSRNInvoice(string ext, int SRNItemID)
        {
            try
            {
                SqlParameter[] sqlPrms = new SqlParameter[2];
                sqlPrms[0] = new SqlParameter("@ext", ext);
                sqlPrms[1] = new SqlParameter("@SRNItemID", SRNItemID);
                SQLDBUtil.ExecuteNonQuery("MMS_InsertSRNInvoice", sqlPrms);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static DataSet MMS_DDL_EmployeeMaster()
        {
            try
            {
                DataSet ds = SQLDBUtil.ExecuteDataset("MMS_DDL_EmployeeMaster");
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet MMS_ChangeSRNBillStatus(int BillNo, int BillStatus, int EmpId)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[3];
                param[0] = new SqlParameter("@BillNo", BillNo);
                param[1] = new SqlParameter("@BillStatus", BillStatus);
                param[2] = new SqlParameter("@EmpId", EmpId);
                return SQLDBUtil.ExecuteDataset("MMS_ChangeSRNBillStatus", param);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public DataSet MMS_SRNApproveBill(int BillNo, int CompanyId, int EmpId, int ModuleID)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[4];
                param[0] = new SqlParameter("@BillNo", BillNo);
                param[1] = new SqlParameter("@CompanyId", CompanyId);
                param[2] = new SqlParameter("@EmpId", EmpId);
                param[3] = new SqlParameter("@ModuleId", ModuleID);
                return SQLDBUtil.ExecuteDataset("MMS_SRNApproveBill", param);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public DataSet MMS_BillSRNs(int BillNo)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@BillNo", BillNo);
                return SQLDBUtil.ExecuteDataset("MMS_BillSRNs", param);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        #endregion
        #region Contacts
        public static int HR_InsUpContactType(string ContactType)
        {
            try
            {
                SqlParameter[] sqlPrms = new SqlParameter[2];
                sqlPrms[0] = new SqlParameter("@ContactType", ContactType);
                sqlPrms[1] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
                sqlPrms[1].Direction = ParameterDirection.ReturnValue;
                SQLDBUtil.ExecuteNonQuery("HR_InsUpContactType", sqlPrms);
                return Convert.ToInt32(sqlPrms[1].Value);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static int HR_InsUpContactsDetails(int ContactID, int CID, string FName, string MName, string LName, string Phone1, string Phone2, string EMailID, string web, string Address, string Notes, int UserID, int GID, string ext, string IM, string Designation, string Fax, int? PhExt)
        {
            try
            {
                SqlParameter[] objParam = new SqlParameter[18];
                objParam[0] = new SqlParameter("@ContactID", ContactID);
                objParam[1] = new SqlParameter("@CID", CID);
                objParam[2] = new SqlParameter("@FName", FName);
                objParam[3] = new SqlParameter("@MName", MName);
                objParam[4] = new SqlParameter("@LName", LName);
                objParam[5] = new SqlParameter("@Phone1", Phone1);
                objParam[6] = new SqlParameter("@Phone2", Phone2);
                objParam[7] = new SqlParameter("@EMailID", EMailID);
                objParam[8] = new SqlParameter("@Address", Address);
                objParam[9] = new SqlParameter("@web", web);
                objParam[10] = new SqlParameter("@Notes", Notes);
                objParam[11] = new SqlParameter("@UserID", UserID);
                objParam[12] = new SqlParameter("@GID", GID);
                objParam[13] = new SqlParameter("@ext", ext);
                objParam[14] = new SqlParameter("@IM", IM);
                objParam[15] = new SqlParameter("@Designation", Designation);
                objParam[16] = new SqlParameter("@Fax", Fax);
                objParam[17] = new SqlParameter("@PhExt", PhExt);
                objParam[0].Direction = ParameterDirection.InputOutput;
                SQLDBUtil.ExecuteNonQuery("HR_InsUpContactsDetails", objParam);
                return Convert.ToInt32(objParam[0].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet HR_GetContactList()
        {
            try
            {
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetContactList");
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region LedgerBalances
        public static DataSet HR_GetEmpLeders()
        {
            try
            {
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetEmpLeders");
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet HR_GetEmpLedersByEmpId(int? EmpID, int CompanyID)
        {
            try
            {
                SqlParameter[] parm = new SqlParameter[2];
                parm[0] = new SqlParameter("@EmpID", EmpID);
                parm[1] = new SqlParameter("@CompID", CompanyID);
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetEmpLedersByEmpId", parm);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet GetWSByEmpID(int? EmpID)
        {
            try
            {
                SqlParameter[] parm = new SqlParameter[1];
                parm[0] = new SqlParameter("@EmpID", EmpID);
                DataSet dsGetWSByEmpID = SQLDBUtil.ExecuteDataset("HMS_GetWSByEmpID", parm);
                return dsGetWSByEmpID;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public static void InsUpdPerferences(int EmpID, int ModuleID, int MenuID, int Status)
        {
            SqlParameter[] parm = new SqlParameter[4];
            parm[0] = new SqlParameter("@EmpID", EmpID);
            parm[1] = new SqlParameter("@MenuID", MenuID);
            parm[2] = new SqlParameter("@ModuleID", ModuleID);
            parm[3] = new SqlParameter("@PrevStatus", Status);
            SQLDBUtil.ExecuteNonQuery("ACC_InsUpdPreferences", parm);
        }
        #endregion
        #region SiteWise
        public static DataSet HR_ShiftWiseEmp(int WS, int Dept, string Emp, int Shift, int DisID)
        {
            SqlParameter[] p = new SqlParameter[5];
            p[0] = new SqlParameter("@SiteID", WS);
            p[1] = new SqlParameter("@DeptID", Dept);
            p[2] = new SqlParameter("@EmpName", Emp);
            p[3] = new SqlParameter("@Shift", Shift);
            p[4] = new SqlParameter("@DesigID", DisID);
            DataSet ds = SQLDBUtil.ExecuteDataset("HR_ShiftWiseEmp", p);
            return ds;
        }
        public static DataSet HR_ShiftWiseEmpByPaging(HRCommon objHrCommon, int WS, int Dept, int Empid, int Shift, int DisID, int CompanyID)
        {
            SqlParameter[] sqlParams = new SqlParameter[10];
            sqlParams[0] = new SqlParameter("@CurrentPage", objHrCommon.CurrentPage);
            sqlParams[1] = new SqlParameter("@PageSize", objHrCommon.PageSize);
            sqlParams[2] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
            sqlParams[2].Direction = ParameterDirection.ReturnValue;
            sqlParams[3] = new SqlParameter("@NoofRecords", System.Data.SqlDbType.Int);
            sqlParams[3].Direction = ParameterDirection.Output;
            sqlParams[4] = new SqlParameter("@SiteID", WS);
            sqlParams[5] = new SqlParameter("@DeptID", Dept);
            sqlParams[6] = new SqlParameter("@Empid", Empid);
            sqlParams[7] = new SqlParameter("@Shift", Shift);
            sqlParams[8] = new SqlParameter("@DesigID", DisID);
            sqlParams[9] = new SqlParameter("@CompanyID", CompanyID);
            DataSet ds = SQLDBUtil.ExecuteDataset("HR_ShiftWiseEmpByPaging", sqlParams);
            objHrCommon.NoofRecords = (int)sqlParams[3].Value;
            objHrCommon.TotalPages = (int)sqlParams[2].Value;
            return ds;
        }
        public static void HR_UpdateShift(int Shift, int EmpID)
        {
            SqlParameter[] parm = new SqlParameter[2];
            parm[0] = new SqlParameter("@Shift", Shift);
            parm[1] = new SqlParameter("@EmpID", EmpID);
            SQLDBUtil.ExecuteNonQuery("HR_UpdateShift", parm);
        }
        public static void HR_JumbleShifts(int? DesgID, int? WSID, int? DeptID, int ShiftID)
        {
            SqlParameter[] parm = new SqlParameter[4];
            parm[0] = new SqlParameter("@DesgID", DesgID);
            parm[1] = new SqlParameter("@WSID", WSID);
            parm[2] = new SqlParameter("@DeptID", DeptID);
            parm[3] = new SqlParameter("@ShiftID", ShiftID);
            SQLDBUtil.ExecuteNonQuery("HR_JumbleShifts", parm);
        }
        #endregion
        #region Bonus
        public static void HR_InsUpBonus(int BID, DateTime Date, double ShiftATarget, double ShiftBTarget, double ShiftCTarget, double ShiftACollection, double ShiftBCollection, double ShiftCCollection, double ShiftARevenue, double ShiftBRevenue, double ShiftCRevenue, double BonusPercent, double ShiftABonus, double ShiftBBonus, double ShiftCBonus, int SubmittedBy, DateTime SubmittedOn, int UpdatedBy, DateTime UpdatedOn)
        {
            SqlParameter[] parm = new SqlParameter[19];
            parm[0] = new SqlParameter("@BID", BID);
            parm[1] = new SqlParameter("@Date", Date);
            parm[2] = new SqlParameter("@ShiftATarget", ShiftATarget);
            parm[3] = new SqlParameter("@ShiftBTarget", ShiftBTarget);
            parm[4] = new SqlParameter("@ShiftCTarget", ShiftCTarget);
            parm[5] = new SqlParameter("@ShiftACollection", ShiftACollection);
            parm[6] = new SqlParameter("@ShiftBCollection", ShiftBCollection);
            parm[7] = new SqlParameter("@ShiftCCollection", ShiftCCollection);
            parm[8] = new SqlParameter("@ShiftARevenue", ShiftARevenue);
            parm[9] = new SqlParameter("@ShiftBRevenue", ShiftBRevenue);
            parm[10] = new SqlParameter("@ShiftCRevenue", ShiftCRevenue);
            parm[11] = new SqlParameter("@BonusPercent", BonusPercent);
            parm[12] = new SqlParameter("@ShiftABonus", ShiftABonus);
            parm[13] = new SqlParameter("@ShiftBBonus", ShiftBBonus);
            parm[14] = new SqlParameter("@ShiftCBonus", ShiftCBonus);
            parm[15] = new SqlParameter("@SubmittedBy", SubmittedBy);
            parm[16] = new SqlParameter("@SubmittedOn", SubmittedOn);
            parm[17] = new SqlParameter("@UpdatedBy", UpdatedBy);
            parm[18] = new SqlParameter("@UpdatedOn", UpdatedOn);
            SQLDBUtil.ExecuteNonQuery("HR_InsUpBonus", parm);
        }
        public static void HR_DelBonus(int BID)
        {
            SqlParameter[] parm = new SqlParameter[1];
            parm[0] = new SqlParameter("@BID", BID);
            SQLDBUtil.ExecuteNonQuery("HR_DelBonus", parm);
        }
        public static DataSet HR_GetBonus()
        {
            DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetBonus");
            return ds;
        }
        public static DataSet HR_GetBonusDetailsByID(int BID)
        {
            SqlParameter[] parm = new SqlParameter[1];
            parm[0] = new SqlParameter("@BID", BID);
            DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetBonusDetailsByID", parm);
            return ds;
        }
        public static void HR_InsUpDefult(double DBPercent, double DShftAT, double DShftBT, double DShftCT)
        {
            SqlParameter[] parm = new SqlParameter[4];
            parm[0] = new SqlParameter("@BP", DBPercent);
            parm[1] = new SqlParameter("@ShftAT", DShftAT);
            parm[2] = new SqlParameter("@ShftBT", DShftBT);
            parm[3] = new SqlParameter("@ShftCT", DShftCT);
            SQLDBUtil.ExecuteNonQuery("HR_InsUpDefult", parm);
        }
        public static DataSet HR_getBonusDefaults()
        {
            DataSet ds = SQLDBUtil.ExecuteDataset("HR_getBonusDefaults");
            return ds;
        }
        #endregion
        #region HiredWorkOrderBilling
        public static DataSet HR_SearchHiredWO(int WS, int AuID)
        {
            try
            {
                SqlParameter[] objParam = new SqlParameter[16];
                objParam[0] = new SqlParameter("@venid", System.Data.SqlDbType.Int);
                objParam[1] = new SqlParameter("@itemid", System.Data.SqlDbType.Int);
                objParam[2] = new SqlParameter("@projectid", WS);
                objParam[3] = new SqlParameter("@postartdate", System.Data.SqlDbType.DateTime);
                objParam[4] = new SqlParameter("@poEnddate", System.Data.SqlDbType.DateTime);
                objParam[5] = new SqlParameter("@PONo", System.Data.SqlDbType.Int);
                objParam[6] = new SqlParameter("@Category", 96);
                objParam[7] = new SqlParameter("@Min", System.Data.SqlDbType.Decimal);
                objParam[8] = new SqlParameter("@Max", System.Data.SqlDbType.Decimal);
                objParam[9] = new SqlParameter("@desc", "");
                objParam[10] = new SqlParameter("@CurrentPage", 1);
                objParam[11] = new SqlParameter("@PageSize", 20);
                objParam[12] = new SqlParameter("@EmpId", System.Data.SqlDbType.Int);
                objParam[13] = new SqlParameter("@POStatus", 1);
                objParam[14] = new SqlParameter("@NoofRecords", 20);
                objParam[15] = new SqlParameter("@AuID", AuID);
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_SearchHiredWO", objParam);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet HR_GetBilledPayments(int WS, int Month, int Year)
        {
            SqlParameter[] objParam = new SqlParameter[3];
            objParam[0] = new SqlParameter("@WS", WS);
            objParam[1] = new SqlParameter("@Month", Month);
            objParam[2] = new SqlParameter("@year", Year);
            DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetBilledPayments", objParam);
            return ds;
        }
        public static DataSet HR_WOViewToCancel(int WS, int CompnayID)
        {
            SqlParameter[] objParam = new SqlParameter[2];
            objParam[0] = new SqlParameter("@projectID", WS);
            objParam[1] = new SqlParameter("@CompanyID", CompnayID);
            DataSet ds = SQLDBUtil.ExecuteDataset("HR_WOViewToCancel", objParam);
            return ds;
        }
        public static DataSet HR_SearchWo(int PONO)
        {
            SqlParameter[] objParam = new SqlParameter[1];
            objParam[0] = new SqlParameter("@PONO", PONO);
            DataSet ds = SQLDBUtil.ExecuteDataset("HR_SearchWo", objParam);
            return ds;
        }
        public static DataSet HR_GetWODetails(int PONO)
        {
            SqlParameter[] objParam = new SqlParameter[1];
            objParam[0] = new SqlParameter("@PONO", PONO);
            DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetWODetails", objParam);
            return ds;
        }
        public static void HR_IncrementHireAmt(int WONo, int VendorID, int HireType, double Amount, double POAmount, DateTime FromDate, int UpdateBy)
        {
            SqlParameter[] parm = new SqlParameter[7];
            parm[0] = new SqlParameter("@WONo", WONo);
            parm[1] = new SqlParameter("@VendorID", VendorID);
            parm[2] = new SqlParameter("@HireType", HireType);
            parm[3] = new SqlParameter("@Amount", Amount);
            parm[4] = new SqlParameter("@POAmount", POAmount);
            parm[5] = new SqlParameter("@FromDate", FromDate);
            parm[6] = new SqlParameter("@UpdateBy", UpdateBy);
            SQLDBUtil.ExecuteNonQuery("HR_IncrementHireAmt", parm);
        }
        public static void HR_InsUpHiredLandBuildings(int HBLID, int PODetID, int WONo, DateTime WoDate, int VendorID, string VendorName, int Item, int HireType, DateTime? HireFrom, double MonthlyRent, string ItemAddress, string ItemSpecification, int SubmittedBy, int ForWorkSite, double Advance, bool IsAdvanceRent, string Unit)
        {
            SqlParameter[] parm = new SqlParameter[17];
            parm[0] = new SqlParameter("@HBLID", HBLID);
            parm[1] = new SqlParameter("@PODetID", PODetID);
            parm[2] = new SqlParameter("@WONo", WONo);
            parm[3] = new SqlParameter("@WoDate", WoDate);
            parm[4] = new SqlParameter("@VendorID", VendorID);
            parm[5] = new SqlParameter("@VendorName", VendorName);
            parm[6] = new SqlParameter("@Item", Item);
            parm[7] = new SqlParameter("@HireType", HireType);
            parm[8] = new SqlParameter("@HireFrom", HireFrom);
            parm[9] = new SqlParameter("@MonthlyRent", MonthlyRent);
            parm[10] = new SqlParameter("@ItemAddress", ItemAddress);
            parm[11] = new SqlParameter("@ItemSpecification", ItemSpecification);
            parm[12] = new SqlParameter("@SubmittedBy", SubmittedBy);
            parm[13] = new SqlParameter("@ForWorkSite", ForWorkSite);
            parm[14] = new SqlParameter("@Advance", Advance);
            parm[15] = new SqlParameter("@IsAdvanceRent", IsAdvanceRent);
            parm[16] = new SqlParameter("@Unit", Unit);
            SQLDBUtil.ExecuteNonQuery("HR_InsUpHiredLandBuildings", parm);
        }
        public static DataSet HR_GetHiredLandBulidings(int? WS, int CompanyID)
        {
            SqlParameter[] objParam = new SqlParameter[2];
            objParam[0] = new SqlParameter("@WS", WS);
            objParam[1] = new SqlParameter("@CompanyID", CompanyID);
            DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetHiredLandBulidings", objParam);
            return ds;
        }
        public static DataSet HR_GetItemAddress(int WONo)
        {
            SqlParameter[] objParam = new SqlParameter[1];
            objParam[0] = new SqlParameter("@WONo", WONo);
            DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetItemAddress", objParam);
            return ds;
        }
        public static DataSet HR_GetSigleWODetails(int WS, int WONO)
        {
            SqlParameter[] objParam = new SqlParameter[2];
            objParam[0] = new SqlParameter("@WS", WS);
            objParam[1] = new SqlParameter("@WONO", WONO);
            DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetSigleWODetails", objParam);
            return ds;
        }
        public static void MMS_CLOSEPO(int PONO, int ItemId, int UserId)
        {
            SqlParameter[] parm = new SqlParameter[3];
            parm[0] = new SqlParameter("@PONO", PONO);
            parm[1] = new SqlParameter("@itemid", ItemId);
            parm[2] = new SqlParameter("@EmpId", UserId);
            SQLDBUtil.ExecuteNonQuery("MMS_CLOSEPO", parm);
        }
        public static void HR_InsUpHireAdvance(int WONO, int UserID, double Amount, int LedgerID)
        {
            try
            {
                SqlParameter[] parm = new SqlParameter[4];
                parm[0] = new SqlParameter("@WONO", WONO);
                parm[1] = new SqlParameter("@UserID", UserID);
                parm[2] = new SqlParameter("@Amount", Amount);
                parm[3] = new SqlParameter("@LedgerID", LedgerID);
                SQLDBUtil.ExecuteNonQuery("HR_InsUpHireAdvance", parm);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static void HR_InsUpHireAdvanceRent(int WONO, int UserID, double Amount)
        {
            SqlParameter[] parm = new SqlParameter[3];
            parm[0] = new SqlParameter("@WONO", WONO);
            parm[1] = new SqlParameter("@UserID", UserID);
            parm[2] = new SqlParameter("@Amount", Amount);
            SQLDBUtil.ExecuteNonQuery("HR_InsUpHireAdvanceRent", parm);
        }
        public static DataSet HR_GetHiredLandBulidingsByID(int WS, int WONO)
        {
            SqlParameter[] objParam = new SqlParameter[2];
            objParam[0] = new SqlParameter("@WS", WS);
            objParam[1] = new SqlParameter("@WONO", WONO);
            DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetHiredLandBulidingsByID", objParam);
            return ds;
        }
        public static DataSet HR_GetHireBills(int Status, int CompnayID)
        {
            SqlParameter[] objParam = new SqlParameter[2];
            objParam[0] = new SqlParameter("@Status", Status);
            objParam[1] = new SqlParameter("@CompanyID", CompnayID);
            DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetHireBills", objParam);
            return ds;
        }
        public static DataSet HR_GetHireBillsByAccStatus(int Status, int Accstatus, int CompnayID)
        {
            SqlParameter[] objParam = new SqlParameter[3];
            objParam[0] = new SqlParameter("@Status", Status);
            objParam[1] = new SqlParameter("@Accstatus", Accstatus);
            objParam[2] = new SqlParameter("@CompanyID", CompnayID);
            DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetHireBillsByAccStatus", objParam);
            return ds;
        }
        public static void HR_HiredBillApproval(int BillingID, int CompanyId, int EmpId)
        {
            SqlParameter[] parm = new SqlParameter[3];
            parm[0] = new SqlParameter("@BillingID", BillingID);
            parm[1] = new SqlParameter("@CompanyId", CompanyId);
            parm[2] = new SqlParameter("@EmpId", EmpId);
            SQLDBUtil.ExecuteNonQuery("HR_HiredBillApproval", parm);
        }
        public static void HR_ChangeHRHiredBillStatus(int BillingID, int Status)
        {
            SqlParameter[] parm = new SqlParameter[2];
            parm[0] = new SqlParameter("@BillingID", BillingID);
            parm[1] = new SqlParameter("@Status", Status);
            SQLDBUtil.ExecuteNonQuery("HR_ChangeHRHiredBillStatus", parm);
        }
        public static void HR_DelHiredLandsBuildings(int WONO)
        {
            SqlParameter[] parm = new SqlParameter[1];
            parm[0] = new SqlParameter("@WONO", WONO);
            SQLDBUtil.ExecuteNonQuery("HR_DelHiredLandsBuildings", parm);
        }
        public static DataSet HR_GetSDLedgers()
        {
            DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetSDLedgers");
            return ds;
        }
        public static DataSet HR_GetSiteEmpWorkstatus(int DeptNo, int SiteID, int Shift, int empid)
        {
            SqlParameter[] objParam = new SqlParameter[4];
            objParam[0] = new SqlParameter("@DeptNo", DeptNo);
            objParam[1] = new SqlParameter("@SiteID", SiteID);
            objParam[2] = new SqlParameter("@Shift", Shift);
            objParam[3] = new SqlParameter("@empid", empid);
            DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetSiteEmpWorkstatus", objParam);
            return ds;
        }
        #endregion
        #region Monitoring Sites
        public static DataSet T_HR_EmpFilterSearch(int DeptNo, int SiteID, int EmpID, string EmpName)
        {
            SqlParameter[] objParam = new SqlParameter[4];
            objParam[0] = new SqlParameter("@MWSID", SiteID);
            objParam[1] = new SqlParameter("@MDeptID", DeptNo);
            objParam[2] = new SqlParameter("@EmpID", EmpID);
            objParam[3] = new SqlParameter("@EmpName", EmpName);
            DataSet ds = SQLDBUtil.ExecuteDataset("T_HR_EmpFilterSearch", objParam);
            return ds;
        }
        public static DataSet HR_GetMonitoringBySiteID(int MWSID)
        {
            SqlParameter[] objParam = new SqlParameter[1];
            objParam[0] = new SqlParameter("@MWSID", MWSID);
            DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetMonitoringBySiteID", objParam);
            return ds;
        }
        public static void HR_InsUpMonitorEngineer(int MIID, int EmpID, int WSID, DateTime MonitorFrom, DateTime? MonitorUpto, int UserId, int ModuleID)
        {
            SqlParameter[] parm = new SqlParameter[7];
            parm[0] = new SqlParameter("@MEID", MIID);
            parm[1] = new SqlParameter("@EmpID", EmpID);
            parm[2] = new SqlParameter("@WSID", WSID);
            parm[3] = new SqlParameter("@MonitorFrom", MonitorFrom);
            parm[4] = new SqlParameter("@MonitorUpto", MonitorUpto);
            parm[5] = new SqlParameter("@UserId", UserId);
            parm[6] = new SqlParameter("@ModuleId", ModuleID);
            SQLDBUtil.ExecuteNonQuery("HR_InsUpMonitorEngineer", parm);
        }
        public static DataSet HR_GetMonitorEngineer(int EmpID)
        {
            SqlParameter[] objParam = new SqlParameter[1];
            objParam[0] = new SqlParameter("@EmpID", EmpID);
            DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetMonitorEngineer", objParam);
            return ds;
        }
        public static DataSet HR_GetMonitoringEngineers()
        {
            DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetMonitoringEngineers");
            return ds;
        }
        public static void HR_DelMonitorEngineer(int MIID)
        {
            SqlParameter[] parm = new SqlParameter[1];
            parm[0] = new SqlParameter("@MIID", MIID);
            SQLDBUtil.ExecuteNonQuery("HR_DelMonitorEngineer", parm);
        }
        public static DataSet HR_EmpSalDetailsExpotToXLS(int? WS)
        {
            SqlParameter[] objParam = new SqlParameter[1];
            objParam[0] = new SqlParameter("@SiteID", WS);
            DataSet ds = SQLDBUtil.ExecuteDataset("HR_EmpSalDetailsExportToExcel", objParam);
            return ds;
        }
        public static DataSet GetEmpDetailsForRpt(HRCommon objHrCommon, int? WS, int CompanyID)
        {
            SqlParameter[] sqlParams = new SqlParameter[6];
            sqlParams[0] = new SqlParameter("@CurrentPage", objHrCommon.CurrentPage);
            sqlParams[1] = new SqlParameter("@PageSize", objHrCommon.PageSize);
            sqlParams[2] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
            sqlParams[2].Direction = ParameterDirection.ReturnValue;
            sqlParams[3] = new SqlParameter("@NoofRecords", System.Data.SqlDbType.Int);
            sqlParams[3].Direction = ParameterDirection.Output;
            sqlParams[4] = new SqlParameter("@SiteID", WS);
            sqlParams[5] = new SqlParameter("@CompanyID", CompanyID);
            DataSet ds = SQLDBUtil.ExecuteDataset("HR_EmpSalDetails", sqlParams);
            objHrCommon.NoofRecords = (int)sqlParams[3].Value;
            objHrCommon.TotalPages = (int)sqlParams[2].Value;
            return ds;
        }
        #endregion
        public static DataSet HR_GetEmployeeGroup()
        {
            DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetEmployeeGroup");
            return ds;
        }
        #region  Category Rate Configuration
        public int InsUpdCategoryRateConfig(int SiteID, int CategoryID, int DesignationID, decimal Rate, int UserID, decimal WHS)
        {
            try
            {
                int result;
                SqlParameter[] sqlParams = new SqlParameter[7];
                sqlParams[0] = new SqlParameter("@SiteID", SiteID);
                sqlParams[1] = new SqlParameter("@CategoryID", CategoryID);
                sqlParams[3] = new SqlParameter("@DesignationID", DesignationID);
                sqlParams[4] = new SqlParameter("@UserID", UserID);
                sqlParams[5] = new SqlParameter("@Rate", Rate);
                sqlParams[6] = new SqlParameter("@WorkingHrs", WHS);
                sqlParams[2] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
                sqlParams[2].Direction = ParameterDirection.ReturnValue;
                SQLDBUtil.ExecuteNonQuery("HR_InsUpdCategoryRate", sqlParams);
                result = Convert.ToInt16(sqlParams[2].Value);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet GetCategoryRateConfigList(int SiteID, int Category)
        {
            try
            {
                SqlParameter[] objParam = new SqlParameter[2];
                objParam[0] = new SqlParameter("@SiteID", SiteID);
                objParam[1] = new SqlParameter("@Category", Category);
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetCategoryRateList", objParam);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet GetCategoryRateConfigListByPaging(HRCommon objHrCommon, int? SiteID, string Category)
        {
            SqlParameter[] sqlParams = new SqlParameter[6];
            sqlParams[0] = new SqlParameter("@CurrentPage", objHrCommon.CurrentPage);
            sqlParams[1] = new SqlParameter("@PageSize", objHrCommon.PageSize);
            sqlParams[2] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
            sqlParams[2].Direction = ParameterDirection.ReturnValue;
            sqlParams[3] = new SqlParameter("@NoofRecords", System.Data.SqlDbType.Int);
            sqlParams[3].Direction = ParameterDirection.Output;
            sqlParams[4] = new SqlParameter("@SiteID", SiteID);
            sqlParams[5] = new SqlParameter("@Category", Category);
            DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetCategoryRateListByPaging", sqlParams);
            objHrCommon.NoofRecords = (int)sqlParams[3].Value;
            objHrCommon.TotalPages = (int)sqlParams[2].Value;
            return ds;
        }
        public DataSet GetCategoryRateConfig(int SiteID, int CategoryID, int DesignationID)
        {
            try
            {
                SqlParameter[] objParam = new SqlParameter[3];
                objParam[0] = new SqlParameter("@SiteID", SiteID);
                objParam[1] = new SqlParameter("@CategoryID", CategoryID);
                objParam[2] = new SqlParameter("@DesignationID", DesignationID);
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetCategoryRate", objParam);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        public static void HR_UpdateUserNamePasswordByJoin(int EmpID, string UserName, string Password)
        {
            SqlParameter[] parm = new SqlParameter[3];
            parm[0] = new SqlParameter("@EmpID", EmpID);
            parm[1] = new SqlParameter("@UserName", UserName);
            parm[2] = new SqlParameter("@Password", Password);
            SQLDBUtil.ExecuteNonQuery("HR_UpdateUserNamePasswordByJoin", parm);
        }
        public static void HR_UpdateEmpType(int EmpID)
        {
            SqlParameter[] parm = new SqlParameter[1];
            parm[0] = new SqlParameter("@EmpID", EmpID);
            SQLDBUtil.ExecuteNonQuery("HR_UpdateEmpType", parm);
        }
        public static DataSet HR_CheckMailID(string MailID)
        {
            try
            {
                SqlParameter[] objParam = new SqlParameter[1];
                objParam[0] = new SqlParameter("@MailID", MailID);
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_CheckMailID", objParam);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static int HR_InsUpRentalDocs(int HRDocID, int EmpID, double Amount, string Proof, DateTime FromDate, DateTime? ToDate, int UserID)
        {
            int result;
            SqlParameter[] parm = new SqlParameter[8];
            parm[0] = new SqlParameter("@HRDocID", HRDocID);
            parm[1] = new SqlParameter("@EmpID", EmpID);
            parm[2] = new SqlParameter("@Amount", Amount);
            parm[3] = new SqlParameter("@Proof", Proof);
            parm[4] = new SqlParameter("@FromDate", FromDate);
            parm[5] = new SqlParameter("@ToDate", ToDate);
            parm[6] = new SqlParameter("@UserID", UserID);
            parm[7] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
            parm[7].Direction = ParameterDirection.ReturnValue;
            SQLDBUtil.ExecuteNonQuery("HR_InsUpRentalDocs", parm);
            result = Convert.ToInt16(parm[7].Value);
            return result;
        }
        public static DataSet HR_GetRentalDocsByPaging(HRCommon objHrCommon, int WS, int Dept, int EmpID, string EmpName)
        {
            SqlParameter[] sqlParams = new SqlParameter[8];
            sqlParams[0] = new SqlParameter("@CurrentPage", objHrCommon.CurrentPage);
            sqlParams[1] = new SqlParameter("@PageSize", objHrCommon.PageSize);
            sqlParams[2] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
            sqlParams[2].Direction = ParameterDirection.ReturnValue;
            sqlParams[3] = new SqlParameter("@NoofRecords", System.Data.SqlDbType.Int);
            sqlParams[3].Direction = ParameterDirection.Output;
            sqlParams[4] = new SqlParameter("@WS", WS);
            sqlParams[5] = new SqlParameter("@Dept", Dept);
            sqlParams[6] = new SqlParameter("@EmpID", EmpID);
            sqlParams[7] = new SqlParameter("@EmpName", EmpName);
            DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetRentalDocsByPaging", sqlParams);
            objHrCommon.NoofRecords = (int)sqlParams[3].Value;
            objHrCommon.TotalPages = (int)sqlParams[2].Value;
            return ds;
        }
        public static DataSet HR_GetRentalDocs(int WS, int Dept, int EmpID, string EmpName)
        {
            SqlParameter[] parm = new SqlParameter[4];
            parm[0] = new SqlParameter("@WS", WS);
            parm[1] = new SqlParameter("@Dept", Dept);
            parm[2] = new SqlParameter("@EmpID", EmpID);
            parm[3] = new SqlParameter("@EmpName", EmpName);
            DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetRentalDocs", parm);
            return ds;
        }
        public static DataSet HR_GetRentalDocsByID(int HRDocID)
        {
            try
            {
                SqlParameter[] objParam = new SqlParameter[1];
                objParam[0] = new SqlParameter("@HRDocID", HRDocID);
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetRentalDocsByID", objParam);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet HR_GetRentalDocsByUserIDByPaging(HRCommon objHrCommon, int UserId)
        {
            SqlParameter[] sqlParams = new SqlParameter[5];
            sqlParams[0] = new SqlParameter("@CurrentPage", objHrCommon.CurrentPage);
            sqlParams[1] = new SqlParameter("@PageSize", objHrCommon.PageSize);
            sqlParams[2] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
            sqlParams[2].Direction = ParameterDirection.ReturnValue;
            sqlParams[3] = new SqlParameter("@NoofRecords", System.Data.SqlDbType.Int);
            sqlParams[3].Direction = ParameterDirection.Output;
            sqlParams[4] = new SqlParameter("@Status", UserId);
            DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetRentalDocsByUserIDByPaging", sqlParams);
            objHrCommon.NoofRecords = (int)sqlParams[3].Value;
            objHrCommon.TotalPages = (int)sqlParams[2].Value;
            return ds;
        }
        public static DataSet HR_GetRentalDocsByUserID(int UserId)
        {
            try
            {
                SqlParameter[] objParam = new SqlParameter[1];
                objParam[0] = new SqlParameter("@UserId", UserId);
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetRentalDocsByUserID", objParam);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet HR_FilterSearchAll(string EmpName)
        {
            SqlParameter[] parm = new SqlParameter[1];
            parm[0] = new SqlParameter("@EmpName", EmpName);
            DataSet ds = SQLDBUtil.ExecuteDataset("HR_FilterSearchAll", parm);
            return ds;
        }
        public static DataSet HR_FilterSearchAll_googlesearch(String SearchKey, string EmpName)
        {
            SqlParameter[] parm = new SqlParameter[2];
            parm[0] = new SqlParameter("@search", SearchKey);
            parm[1] = new SqlParameter("@EmpName", EmpName);
            DataSet ds = SQLDBUtil.ExecuteDataset("HR_FilterSearchAll_googlesearch", parm);
            return ds;
        }
        public static DataSet BindDeparmetBySite(int Site, int CompanyID)
        {
            SqlParameter[] parm = new SqlParameter[2];
            parm[0] = new SqlParameter("@SiteID", Site);
            parm[1] = new SqlParameter("@CompanyID", CompanyID);
            DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetDepartmentBySite", parm);
            return ds;
        }
        public static DataSet BindemployeeBySite(int Site)
        {
            SqlParameter[] parm = new SqlParameter[1];
            parm[0] = new SqlParameter("@SiteID", Site);
            DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetemployeeBySite", parm);
            return ds;
        }
        public static DataSet Bindresp()
        {
            DataSet ds = SQLDBUtil.ExecuteDataset("get_emp_resp");
            return ds;
        }
        public static DataSet Bindjobdes()
        {
            DataSet ds = SQLDBUtil.ExecuteDataset("get_jobdescription");
            return ds;
        }
        public static DataSet BindDeparmetBySite_googlesearch(String SearchKey, int Site, int CompanyID)
        {
            SqlParameter[] parm = new SqlParameter[3];
            parm[0] = new SqlParameter("@search ", SearchKey);
            parm[1] = new SqlParameter("@SiteID", Site);
            parm[2] = new SqlParameter("@CompanyID", CompanyID);
            DataSet ds = SQLDBUtil.ExecuteDataset("HMS_GetDepartmentBySite_googlesearch", parm);
            return ds;
        }
        public static DataSet HR_googlesearch_GetDepartmentBySite(String SearchKey, int Site, int CompanyID)
        {
            SqlParameter[] parm = new SqlParameter[3];
            parm[0] = new SqlParameter("@search ", SearchKey);
            parm[1] = new SqlParameter("@SiteID", Site);
            parm[2] = new SqlParameter("@CompanyID", CompanyID);
            DataSet ds = SQLDBUtil.ExecuteDataset("HR_googlesearch_GetDepartmentBySite", parm);
            return ds;
        }
        public static DataSet HR_GetWorkSite_By_EmpForRamzan_googlesearch(String SearchKey, int WSID)
        {
            SqlParameter[] parm = new SqlParameter[2];
            parm[0] = new SqlParameter("@search ", SearchKey);
            parm[1] = new SqlParameter("@WSID", WSID);
            DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetWorkSite_By_EmpForRamzan_googlesearch", parm);
            return ds;
        }
        public static DataSet HR_GetWorkSite_By_MobileBills_googlesearch(String SearchKey, int WSID)
        {
            SqlParameter[] parm = new SqlParameter[2];
            parm[0] = new SqlParameter("@search ", SearchKey);
            parm[1] = new SqlParameter("@WSID", WSID);
            DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetWorkSite_By_MobileBills_googlesearch", parm);
            return ds;
        }
        public static DataSet Department_googlesearch_by_siteid(String SearchKey, int Site, int CompanyID)
        {
            SqlParameter[] parm = new SqlParameter[3];
            parm[0] = new SqlParameter("@search ", SearchKey);
            parm[1] = new SqlParameter("@SiteID", Site);
            parm[2] = new SqlParameter("@CompanyID", CompanyID);
            DataSet ds = SQLDBUtil.ExecuteDataset("HMS_Department_googlesearch_by_siteid", parm);
            return ds;
        }
        public static DataSet Dept_googlesearch_site(String SearchKey, int Site, int CompanyID)
        {
            SqlParameter[] parm = new SqlParameter[2];
            parm[0] = new SqlParameter("@search ", SearchKey);
            parm[1] = new SqlParameter("@SiteID", Site);
            DataSet ds = SQLDBUtil.ExecuteDataset("HMS_Dept_googlesearch_site", parm);
            return ds;
        }
        public static DataSet GetEmployeesByCompID_googlesearch(String SearchKey, int CompanyID)
        {
            try
            {
                SqlParameter[] parm = new SqlParameter[2];
                parm[0] = new SqlParameter("@search", SearchKey);
                parm[1] = new SqlParameter("@CompanyID", CompanyID);
                DataSet ds = SQLDBUtil.ExecuteDataset("GetEmployeesByCompID_googlesearch", parm);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet Getshifts_googlesearch(String SearchKey)
        {
            try
            {
                SqlParameter[] parm = new SqlParameter[1];
                parm[0] = new SqlParameter("@search", SearchKey);
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetShifts_googlesearch", parm);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet HR_DailyAttStatus(DateTime Date, int DeptID, int Site, int CompanyID, int projectid)
        {
            SqlParameter[] parm = new SqlParameter[5];
            parm[0] = new SqlParameter("@Date", Date);
            parm[1] = new SqlParameter("@DeptID", DeptID);
            parm[2] = new SqlParameter("@Site", Site);
            parm[3] = new SqlParameter("@CompanyID", CompanyID);
            parm[4] = new SqlParameter("@ProjectID", projectid);
            DataSet ds = SQLDBUtil.ExecuteDataset("HR_DailyAttStatus_RT_04_16_2016", parm);
            return ds;
        }
        #region Options
        public static DataSet HR_GetDefaultOptionsByPaging(HRCommon objHrCommon)
        {
            SqlParameter[] sqlParams = new SqlParameter[4];
            sqlParams[0] = new SqlParameter("@CurrentPage", objHrCommon.CurrentPage);
            sqlParams[1] = new SqlParameter("@PageSize", objHrCommon.PageSize);
            sqlParams[2] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
            sqlParams[2].Direction = ParameterDirection.ReturnValue;
            sqlParams[3] = new SqlParameter("@NoofRecords", System.Data.SqlDbType.Int);
            sqlParams[3].Direction = ParameterDirection.Output;
            DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetDefaultOptionsByPaging", sqlParams);
            objHrCommon.NoofRecords = (int)sqlParams[3].Value;
            objHrCommon.TotalPages = (int)sqlParams[2].Value;
            return ds;
        }
        public static DataSet HR_GetDefaultOptions()
        {
            DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetDefaultOptions");
            return ds;
        }
        public static void HR_DelOptions(int ID)
        {
            SqlParameter[] parm = new SqlParameter[1];
            parm[0] = new SqlParameter("@ID", ID);
            SQLDBUtil.ExecuteNonQuery("HR_DelOptions", parm);
        }
        public static void HR_InsUpOptions(int OptionID, string Purpose, string Value, int UserId)
        {
            SqlParameter[] parm = new SqlParameter[4];
            parm[0] = new SqlParameter("@OptionID", OptionID);
            parm[1] = new SqlParameter("@Purpose", Purpose);
            parm[2] = new SqlParameter("@Value", Value);
            parm[3] = new SqlParameter("@UpdatedBy", UserId);
            SQLDBUtil.ExecuteNonQuery("HR_InsUpOptions", parm);
        }
        public static DataSet HR_GetOptionsByID(int OptionID)
        {
            SqlParameter[] parm = new SqlParameter[1];
            parm[0] = new SqlParameter("@OptionID", OptionID);
            DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetOptionsByID", parm);
            return ds;
        }
        public static DataSet HR_GetLoanOptions()
        {
            DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetLoanOptions");
            return ds;
        }
        #endregion
        #region Sim & Mobile
        public static DataSet HR_GetMobileProviders(string Vendor)
        {
            SqlParameter[] parm = new SqlParameter[1];
            parm[0] = new SqlParameter("@VendorName", Vendor);
            DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetMobileProviders", parm);
            return ds;
        }
        public static DataSet HR_GetMobileSims(Boolean Status)
        {
            SqlParameter[] parm = new SqlParameter[1];
            parm[0] = new SqlParameter("@Status", Status);
            DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetMobileSims", parm);
            return ds;
        }
        public static DataSet HR_GetMobileSimsByPaging(HRCommon objHrCommon, Boolean Status, int CompanyID)
        {
            SqlParameter[] sqlParams = new SqlParameter[6];
            sqlParams[0] = new SqlParameter("@CurrentPage", objHrCommon.CurrentPage);
            sqlParams[1] = new SqlParameter("@PageSize", objHrCommon.PageSize);
            sqlParams[2] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
            sqlParams[2].Direction = ParameterDirection.ReturnValue;
            sqlParams[3] = new SqlParameter("@NoofRecords", System.Data.SqlDbType.Int);
            sqlParams[3].Direction = ParameterDirection.Output;
            sqlParams[4] = new SqlParameter("@Status", Status);
            sqlParams[5] = new SqlParameter("@CompnayID", CompanyID);
            DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetMobileSimsByPaging", sqlParams);
            objHrCommon.NoofRecords = (int)sqlParams[3].Value;
            objHrCommon.TotalPages = (int)sqlParams[2].Value;
            return ds;
        }
        public static int HR_InsUpMobileSims(int PID, int Provider, long SimNo, bool Status, int Worksite, DateTime StartDate, DateTime? Upto, int Category)
        {
            SqlParameter[] parm = new SqlParameter[9];
            parm[0] = new SqlParameter("@PID", PID);
            parm[1] = new SqlParameter("@Provider", Provider);
            parm[2] = new SqlParameter("@SimNo", SimNo);
            parm[3] = new SqlParameter("@Status", Status);
            parm[4] = new SqlParameter("@Worksite", Worksite);
            parm[5] = new SqlParameter("@ServiceFrom", StartDate);
            parm[6] = new SqlParameter("@Upto", Upto);
            parm[7] = new SqlParameter("@Category", Category);
            parm[8] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
            parm[8].Direction = ParameterDirection.ReturnValue;
            SQLDBUtil.ExecuteNonQuery("HR_InsUpMobileSims", parm);
            return (int)parm[8].Value;
        }
        public static DataSet HR_GetEmpSims()
        {
            DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetEmpSims");
            return ds;
        }
        public static DataSet HR_GetEmpSimsByPaging(HRCommon objHrCommon, int CompanyID)
        {
            SqlParameter[] sqlParams = new SqlParameter[5];
            sqlParams[0] = new SqlParameter("@CurrentPage", objHrCommon.CurrentPage);
            sqlParams[1] = new SqlParameter("@PageSize", objHrCommon.PageSize);
            sqlParams[2] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
            sqlParams[2].Direction = ParameterDirection.ReturnValue;
            sqlParams[3] = new SqlParameter("@NoofRecords", System.Data.SqlDbType.Int);
            sqlParams[3].Direction = ParameterDirection.Output;
            sqlParams[4] = new SqlParameter("@CompanyID", CompanyID);
            DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetEmpSimsByPaging", sqlParams);
            objHrCommon.NoofRecords = (int)sqlParams[3].Value;
            objHrCommon.TotalPages = (int)sqlParams[2].Value;
            return ds;
        }
        public static void HR_InsUpEmpSims(int SID, long SIMNo, int AllottedTo, DateTime AllottedFrom, DateTime? Upto, int UpdatedBy, double AmountLimit)
        {
            SqlParameter[] parm = new SqlParameter[7];
            parm[0] = new SqlParameter("@SID", SID);
            parm[1] = new SqlParameter("@SIMNo", SIMNo);
            parm[2] = new SqlParameter("@AllottedTo", AllottedTo);
            parm[3] = new SqlParameter("@AllottedFrom", AllottedFrom);
            parm[4] = new SqlParameter("@Upto", Upto);
            parm[5] = new SqlParameter("@UpdatedBy", UpdatedBy);
            parm[6] = new SqlParameter("@AmountLimit", AmountLimit);
            SQLDBUtil.ExecuteNonQuery("HR_InsUpEmpSims", parm);
        }
        public static int HR_InsUpdEmpSims(int SID, long SIMNo, int AllottedTo, DateTime AllottedFrom, DateTime? Upto, int UpdatedBy, double AmountLimit)
        {
            SqlParameter[] parm = new SqlParameter[8];
            parm[0] = new SqlParameter("@SID", SID);
            parm[1] = new SqlParameter("@SIMNo", SIMNo);
            parm[2] = new SqlParameter("@AllottedTo", AllottedTo);
            parm[3] = new SqlParameter("@AllottedFrom", AllottedFrom);
            parm[4] = new SqlParameter("@Upto", Upto);
            parm[5] = new SqlParameter("@UpdatedBy", UpdatedBy);
            parm[6] = new SqlParameter("@AmountLimit", AmountLimit);
            parm[7] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
            parm[7].Direction = ParameterDirection.ReturnValue;
            SQLDBUtil.ExecuteNonQuery("HR_InsUpEmpSims", parm);
            return Convert.ToInt32(parm[7].Value);
        }
        public static DataSet HR_GetRegtdSims()
        {
            DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetRegtdSims");
            return ds;
        }
        public static DataSet HR_GetNotAllottedSims(int Category)
        {
            SqlParameter[] parm = new SqlParameter[1];
            parm[0] = new SqlParameter("@Category", Category);
            DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetNotAllottedSims", parm);
            return ds;
        }
        public static DataSet HR_GetAllottedSims(int Category)
        {
            SqlParameter[] parm = new SqlParameter[1];
            parm[0] = new SqlParameter("@Category", Category);
            DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetAllottedSims", parm);
            return ds;
        }
        public static DataSet HR_GetEmployeesBySite(long SimID)
        {
            SqlParameter[] parm = new SqlParameter[1];
            parm[0] = new SqlParameter("@SimID", SimID);
            DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetEmployeesBySite", parm);
            return ds;
        }
        public static DataSet HR_GetSimCount()
        {
            DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetSimCount");
            return ds;
        }
        public static void HR_UpdateAmountLimit(int SID, double AmountLimit, int UserID)
        {
            SqlParameter[] parm = new SqlParameter[3];
            parm[0] = new SqlParameter("@SID", SID);
            parm[1] = new SqlParameter("@AmountLimit", AmountLimit);
            parm[2] = new SqlParameter("@UserID", UserID);
            SQLDBUtil.ExecuteNonQuery("HR_UpdateAmountLimit", parm);
        }
        public static DataSet HR_GetEmpSimByID(int SID)
        {
            SqlParameter[] parm = new SqlParameter[1];
            parm[0] = new SqlParameter("@SID", SID);
            DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetEmpSimByID", parm);
            return ds;
        }
        public static DataSet HR_GetMobileSimByID(int PID)
        {
            SqlParameter[] parm = new SqlParameter[1];
            parm[0] = new SqlParameter("@PID", PID);
            DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetMobileSimByID", parm);
            return ds;
        }
        public static DataSet HR_GetEmployeesBySimID(int SimID)
        {
            SqlParameter[] parm = new SqlParameter[1];
            parm[0] = new SqlParameter("@SimID", SimID);
            DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetEmployeesBySimID", parm);
            return ds;
        }
        public static DataSet HR_GetMobilesBills(HRCommon objHrCommon, int CompanyID)
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
                sqlParams[4] = new SqlParameter("@SiteID", objHrCommon.SiteID);
                sqlParams[5] = new SqlParameter("@DeptID", objHrCommon.DeptID);
                sqlParams[6] = new SqlParameter("@EmpName", objHrCommon.FName);
                sqlParams[7] = new SqlParameter("@Month", objHrCommon.Month);
                sqlParams[8] = new SqlParameter("@year", objHrCommon.Year);
                sqlParams[9] = new SqlParameter("@CompanyID", CompanyID);
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetMobilesBills", sqlParams);
                objHrCommon.NoofRecords = (int)sqlParams[3].Value;
                objHrCommon.TotalPages = (int)sqlParams[2].Value;
                return ds;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static DataSet HR_GetNotAllottedMobileSims()
        {
            DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetNotAllottedMobileSims");
            return ds;
        }
        #endregion
        #region ContactGroup
        public static int HR_InsUpContactGroup(int GID, string GroupName)
        {
            SqlParameter[] parm = new SqlParameter[3];
            parm[0] = new SqlParameter("@GID", GID);
            parm[1] = new SqlParameter("@GroupName", GroupName);
            parm[2] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
            parm[2].Direction = ParameterDirection.ReturnValue;
            SQLDBUtil.ExecuteNonQuery("HR_InsUpContactGroup", parm);
            return Convert.ToInt32(parm[2].Value);
        }
        public static DataSet HR_GetContactGroup()
        {
            DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetContactGroup");
            return ds;
        }
        public static DataSet HR_GetConatactDetails(int CID)
        {
            SqlParameter[] parm = new SqlParameter[1];
            parm[0] = new SqlParameter("@CID", CID);
            DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetConatactDetails", parm);
            return ds;
        }
        #endregion
        #region Config
        public static DataSet HR_GetEmpNature()
        {
            DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetEmpNature");
            return ds;
        }
        public static DataSet HR_GetWOConfigByEmpNature(int NatureID)
        {
            SqlParameter[] parm = new SqlParameter[1];
            parm[0] = new SqlParameter("@NatureID", NatureID);
            DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetWOConfigByEmpNature", parm);
            return ds;
        }
        public static DataSet HMS_checkLeaveCOnfig(int NatureID, int calYear)
        {
            SqlParameter[] parm = new SqlParameter[2];
            parm[0] = new SqlParameter("@EmpnatureID", NatureID);
            parm[1] = new SqlParameter("@calender_year", calYear);
            DataSet ds = SQLDBUtil.ExecuteDataset("HMS_checkLeaveCOnfig", parm);
            return ds;
        }
        #endregion
        #region ToDoList Messages
        public static DataSet HR_GetNotStarted(int AssignerID)
        {
            SqlParameter[] parm = new SqlParameter[1];
            parm[0] = new SqlParameter("@AssignerID", AssignerID);
            DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetNotStarted", parm);
            return ds;
        }
        public static DataSet HR_DueDateFinished(int AssignerID)
        {
            SqlParameter[] parm = new SqlParameter[1];
            parm[0] = new SqlParameter("@AssignerID", AssignerID);
            DataSet ds = SQLDBUtil.ExecuteDataset("HR_DueDateFinished", parm);
            return ds;
        }
        public static DataSet HR_TaskCompleted(int AssignerID)
        {
            SqlParameter[] parm = new SqlParameter[1];
            parm[0] = new SqlParameter("@AssignerID", AssignerID);
            DataSet ds = SQLDBUtil.ExecuteDataset("HR_TaskCompleted", parm);
            return ds;
        }
        public static DataSet HR_NotUpdatingTask(int AssignerID)
        {
            SqlParameter[] parm = new SqlParameter[1];
            parm[0] = new SqlParameter("@AssignerID", AssignerID);
            DataSet ds = SQLDBUtil.ExecuteDataset("HR_NotUpdatingTask", parm);
            return ds;
        }
        public static void HR_InsToDoListMessage(int ToDoID, int MsgType)
        {
            SqlParameter[] parm = new SqlParameter[2];
            parm[0] = new SqlParameter("@ToDoID", ToDoID);
            parm[1] = new SqlParameter("@MsgType", MsgType);
            SQLDBUtil.ExecuteNonQuery("HR_InsToDoListMessage", parm);
        }
        #endregion
        public static DataSet HR_GetInActiveStatus(int EmpID)
        {
            SqlParameter[] parm = new SqlParameter[1];
            parm[0] = new SqlParameter("@EmpID", EmpID);
            DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetInActiveStatus", parm);
            return ds;
        }
        public static DataSet HR_SearchEmpBySiteDept(int WS, int DeptNo, string Status, int CompanyID)
        {
            SqlParameter[] parm = new SqlParameter[4];
            parm[0] = new SqlParameter("@WS", WS);
            parm[1] = new SqlParameter("@Dept", DeptNo);
            parm[2] = new SqlParameter("@Status", Status);
            parm[3] = new SqlParameter("@CompanyID", CompanyID);
            DataSet ds = SQLDBUtil.ExecuteDataset("HR_SearchEmpBySiteDept", parm);
            return ds;
        }
        public static DataSet HR_GetEmpReport(int EmpID, int Month, int Year, int FinyearID)
        {
            SqlParameter[] parm = new SqlParameter[4];
            parm[0] = new SqlParameter("@EmpID", EmpID);
            parm[1] = new SqlParameter("@Month", Month);
            parm[2] = new SqlParameter("@Year", Year);
            parm[3] = new SqlParameter("@FinYearID", FinyearID);
            DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetEmpReport", parm);
            return ds;
        }
        public static DataSet CP_GetTutorials(int ModuleId, int? MenuId)
        {
            try
            {
                SqlParameter[] par = new SqlParameter[2];
                par[0] = new SqlParameter("@ModuleId", ModuleId);
                if (MenuId != 0)
                {
                    par[1] = new SqlParameter("@MenuId", MenuId);
                }
                else
                {
                    par[1] = new SqlParameter("@MenuId", System.Data.SqlDbType.Int);
                }
                DataSet ds = SQLDBUtil.ExecuteDataset("MMS_GetTutorials", par);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet HR_GetEmpAdvanceByMonth(int EmpID, int Month, int Year)
        {
            SqlParameter[] parm = new SqlParameter[3];
            parm[0] = new SqlParameter("@EmpID", EmpID);
            parm[1] = new SqlParameter("@Month", Month);
            parm[2] = new SqlParameter("@Year", Year);
            DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetEmpAdvanceByMonth", parm);
            return ds;
        }
        public static DataSet HR_GetEmpMobileExpByMonth(int EmpID, int Month, int Year)
        {
            SqlParameter[] parm = new SqlParameter[3];
            parm[0] = new SqlParameter("@EmpID", EmpID);
            parm[1] = new SqlParameter("@Month", Month);
            parm[2] = new SqlParameter("@Year", Year);
            DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetEmpMobileExpByMonth", parm);
            return ds;
        }
        public static void HR_StatusChangeRemarks(int EmpID, string Remarks, DateTime? RejoinOn, int Status, int UserID, int EmpDeActType)
        {
            try
            {
                SqlParameter[] parm = new SqlParameter[6];
                parm[0] = new SqlParameter("@EmpID", EmpID);
                parm[1] = new SqlParameter("@Remarks", Remarks);
                parm[2] = new SqlParameter("@RejoinOn", RejoinOn);
                parm[3] = new SqlParameter("@Status", Status);
                parm[4] = new SqlParameter("@UserID", UserID);
                parm[5] = new SqlParameter("@EmpDeActType", EmpDeActType);
                SQLDBUtil.ExecuteNonQuery("HR_StatusChangeRemarks", parm);
            }
            catch { }
        }
        public static DataSet HR_GetAllowanceWages(int Month, int Year, int CompanyID)
        {
            SqlParameter[] parm = new SqlParameter[3];
            parm[0] = new SqlParameter("@Month", Month);
            parm[1] = new SqlParameter("@Year", Year);
            parm[2] = new SqlParameter("@CompanyID", CompanyID);
            DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetAllowanceWages", parm);
            return ds;
        }
        public static DataSet HR_GetBanks()
        {
            DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetBanks");
            return ds;
        }
        public static DataSet HR_GetBankBranches(int BankID)
        {
            SqlParameter[] parm = new SqlParameter[1];
            parm[0] = new SqlParameter("@BankID", BankID);
            DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetBankBranches", parm);
            return ds;
        }
        public static DataSet HR_ValidatePayment(int EmpID, int Month, int Year)
        {
            SqlParameter[] parm = new SqlParameter[3];
            parm[0] = new SqlParameter("@EmpID", EmpID);
            parm[1] = new SqlParameter("@Month", Month);
            parm[2] = new SqlParameter("@Year", Year);
            DataSet ds = SQLDBUtil.ExecuteDataset("HR_ValidatePayment", parm);
            return ds;
        }
        public static void HMS_InsupFamilyDetailsXml(DataSet FD, int EmpID, int Del)
        {
            SqlParameter[] parm = new SqlParameter[3];
            parm[0] = new SqlParameter("@FDetails", FD.GetXml());
            parm[1] = new SqlParameter("@EmpID", EmpID);
            parm[2] = new SqlParameter("@Del", Del);
            SQLDBUtil.ExecuteNonQuery("HMS_InsupFamilyDetailsXml", parm);
        }
        public static DataSet HR_GetFamilyDetails(int EmpID)
        {
            SqlParameter[] parm = new SqlParameter[1];
            parm[0] = new SqlParameter("@EmpID", EmpID);
            DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetFamilyDetails", parm);
            return ds;
        }
        public static DataSet HR_getPartName(int SRNID)
        {
            SqlParameter[] parm = new SqlParameter[1];
            parm[0] = new SqlParameter("@SRNID", SRNID);
            DataSet ds = SQLDBUtil.ExecuteDataset("HR_getPartName", parm);
            return ds;
        }
        public static DataSet HR_ChkMail(string MailID)
        {
            SqlParameter[] parm = new SqlParameter[1];
            parm[0] = new SqlParameter("@MailID", MailID);
            DataSet ds = SQLDBUtil.ExecuteDataset("HR_ChkMail", parm);
            return ds;
        }
        public static DataSet CMS_Get_City()
        {
            DataSet ds = SQLDBUtil.ExecuteDataset("CMS_Get_City");
            return ds;
        }
        public static int HR_InsNewLocation(int CityID, string Location, int StateID)
        {
            SqlParameter[] parm = new SqlParameter[4];
            parm[0] = new SqlParameter("@CityID", CityID);
            parm[1] = new SqlParameter("@NewLocation", Location);
            parm[2] = new SqlParameter("@StateId", StateID);
            parm[3] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
            parm[3].Direction = ParameterDirection.ReturnValue;
            SQLDBUtil.ExecuteNonQuery("HR_InsNewLocation", parm);
            return Convert.ToInt32(parm[3].Value);
        }
        public static int HR_InsertNewLocation(string Location, int StateID)
        {
            SqlParameter[] parm = new SqlParameter[3];
            parm[0] = new SqlParameter("@NewLocation", Location);
            parm[1] = new SqlParameter("@StateId", StateID);
            parm[2] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
            parm[2].Direction = ParameterDirection.ReturnValue;
            SQLDBUtil.ExecuteNonQuery("HR_InsertNewLocation", parm);
            return Convert.ToInt32(parm[2].Value);
        }
        public static DataSet HR_GetMonitoringByPID(int MIID)
        {
            SqlParameter[] parm = new SqlParameter[1];
            parm[0] = new SqlParameter("@MIID", MIID);
            return SQLDBUtil.ExecuteDataset("HR_GetMonitoringByMIID", parm);
        }
        public static DataSet HR_GetDesignationsByStatus(HRCommon objHrCommon, bool Status, int desig)
        {
            SqlParameter[] sqlParams = new SqlParameter[6];
            sqlParams[0] = new SqlParameter("@CurrentPage", objHrCommon.CurrentPage);
            sqlParams[1] = new SqlParameter("@PageSize", objHrCommon.PageSize);
            sqlParams[2] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
            sqlParams[2].Direction = ParameterDirection.ReturnValue;
            sqlParams[3] = new SqlParameter("@NoofRecords", System.Data.SqlDbType.Int);
            sqlParams[3].Direction = ParameterDirection.Output;
            sqlParams[4] = new SqlParameter("@Status", Status);
            sqlParams[5] = new SqlParameter("@desig", desig);
            DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetDesignationsByStatus", sqlParams);
            objHrCommon.NoofRecords = (int)sqlParams[3].Value;
            objHrCommon.TotalPages = (int)sqlParams[2].Value;
            return ds;
        }
        public DataSet GetWorkSiteByActiveEmpID(int? EmpID, int CompnanyID, int? Role)
        {
            try
            {
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetWorksite_By_EmpID_ActiveEmp", new SqlParameter[] { new SqlParameter("@EmpID", EmpID), new SqlParameter("@CompanyID", CompnanyID), new SqlParameter("@Role", Role) });
                return ds;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static DataSet GetWorkSiteByActiveEmpID_googlesearch(String SearchKey, int EmpID, int CompanyID, int Role)
        {
            SqlParameter[] parm = new SqlParameter[4];
            parm[0] = new SqlParameter("@search ", SearchKey);
            parm[1] = new SqlParameter("@EmpID", EmpID);
            parm[2] = new SqlParameter("@CompanyID", CompanyID);
            parm[3] = new SqlParameter("@Role", Role);
            DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetWorksite_By_EmpID_ActiveEmp_googlesearch", parm);
            return ds;
        }
        public static DataSet GetWorkSiteByActiveEmpID_wsid_googlesearch(String SearchKey, int EmpID, int CompanyID, int Role, int WSID)
        {
            SqlParameter[] parm = new SqlParameter[5];
            parm[0] = new SqlParameter("@search ", SearchKey);
            parm[1] = new SqlParameter("@EmpID", EmpID);
            parm[2] = new SqlParameter("@CompanyID", CompanyID);
            parm[3] = new SqlParameter("@Role", Role);
            parm[4] = new SqlParameter("@WSID", WSID);
            DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetWorksite_By_EmpID_Wsid_ActiveEmp_googlesearch", parm);
            return ds;
        }
        public static DataSet T_HMS_GetVacation(HRCommon objHrCommon, int empid, string ename, int WSid, int Deptid, int month, int year, DateTime stdate, DateTime enddate,int LID)
        {
            SqlParameter[] sqlParams = new SqlParameter[14];
            sqlParams[0] = new SqlParameter("@CurrentPage", objHrCommon.CurrentPage);
            sqlParams[1] = new SqlParameter("@PageSize", objHrCommon.PageSize);
            sqlParams[2] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
            sqlParams[2].Direction = ParameterDirection.ReturnValue;
            sqlParams[3] = new SqlParameter("@NoofRecords", System.Data.SqlDbType.Int);
            sqlParams[3].Direction = ParameterDirection.Output;
            sqlParams[4] = new SqlParameter("@Empid", empid);
            sqlParams[5] = new SqlParameter("@Ename", ename);
            sqlParams[6] = new SqlParameter("@WSid", WSid);
            sqlParams[7] = new SqlParameter("@Deptid", Deptid);
            sqlParams[8] = new SqlParameter("@month", month);
            sqlParams[9] = new SqlParameter("@year", year);
            sqlParams[10] = new SqlParameter("@StDate", stdate);
            sqlParams[11] = new SqlParameter("@EndDate", enddate);
            sqlParams[12] = new SqlParameter("@leavetype", DBNull.Value);
            sqlParams[13] = new SqlParameter("@lid", LID);
            DataSet ds = SQLDBUtil.ExecuteDataset("T_HMS_GetEmployeeVacation", sqlParams);
            objHrCommon.NoofRecords = (int)sqlParams[3].Value;
            objHrCommon.TotalPages = (int)sqlParams[2].Value;
            return ds;
        }
        public static DataSet T_HMS_ConfigAirTicketdetailsbyID_status(HRCommon objHrCommon, bool Status, int id)
        {
            SqlParameter[] sqlParams = new SqlParameter[6];
            sqlParams[0] = new SqlParameter("@CurrentPage", objHrCommon.CurrentPage);
            sqlParams[1] = new SqlParameter("@PageSize", objHrCommon.PageSize);
            sqlParams[2] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
            sqlParams[2].Direction = ParameterDirection.ReturnValue;
            sqlParams[3] = new SqlParameter("@NoofRecords", System.Data.SqlDbType.Int);
            sqlParams[3].Direction = ParameterDirection.Output;
            sqlParams[4] = new SqlParameter("@Status", Status);
            if (id == 0)
                sqlParams[5] = new SqlParameter("@ID", System.Data.SqlDbType.Int);
            else
                sqlParams[5] = new SqlParameter("@ID", id);
            DataSet ds = SQLDBUtil.ExecuteDataset("T_HMS_ConfigAirTicketdetailsbyID_status", sqlParams);
            objHrCommon.NoofRecords = (int)sqlParams[3].Value;
            objHrCommon.TotalPages = (int)sqlParams[2].Value;
            return ds;
        }
        public static DataSet T_HMS_ConfigAirTicketdetailsbyID_status_Search(HRCommon objHrCommon, bool Status, int id, int AirLinesId, int Pasngrtypeid, int Bookingcls, int frmcity, int tocity)
        {
            SqlParameter[] sqlParams = new SqlParameter[11];
            sqlParams[0] = new SqlParameter("@CurrentPage", objHrCommon.CurrentPage);
            sqlParams[1] = new SqlParameter("@PageSize", objHrCommon.PageSize);
            sqlParams[2] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
            sqlParams[2].Direction = ParameterDirection.ReturnValue;
            sqlParams[3] = new SqlParameter("@NoofRecords", System.Data.SqlDbType.Int);
            sqlParams[3].Direction = ParameterDirection.Output;
            sqlParams[4] = new SqlParameter("@Status", Status);
            if (id == 0)
                sqlParams[5] = new SqlParameter("@ID", System.Data.SqlDbType.Int);
            else
                sqlParams[5] = new SqlParameter("@ID", id);
            sqlParams[6] = new SqlParameter("@AirLinesId", AirLinesId);
            sqlParams[7] = new SqlParameter("@PassengertypeID", Pasngrtypeid);
            sqlParams[8] = new SqlParameter("@BookingClassID", Bookingcls);
            sqlParams[9] = new SqlParameter("@FromCityId", frmcity);
            sqlParams[10] = new SqlParameter("@ToCityID", tocity);
            DataSet ds = SQLDBUtil.ExecuteDataset("T_HMS_ConfigAirTicketdetailsbyID_status_Search", sqlParams);
            objHrCommon.NoofRecords = (int)sqlParams[3].Value;
            objHrCommon.TotalPages = (int)sqlParams[2].Value;
            return ds;
        }
        public static DataSet T_HMS_empVsAirTicketsAuth_LISTbyID_status(HRCommon objHrCommon, bool Status, int id, int? WSid, int? Deptno)
        {
            SqlParameter[] sqlParams = new SqlParameter[8];
            sqlParams[0] = new SqlParameter("@CurrentPage", objHrCommon.CurrentPage);
            sqlParams[1] = new SqlParameter("@PageSize", objHrCommon.PageSize);
            sqlParams[2] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
            sqlParams[2].Direction = ParameterDirection.ReturnValue;
            sqlParams[3] = new SqlParameter("@NoofRecords", System.Data.SqlDbType.Int);
            sqlParams[3].Direction = ParameterDirection.Output;
            sqlParams[4] = new SqlParameter("@Status", Status);
            if (id == 0)
                sqlParams[5] = new SqlParameter("@ID", System.Data.SqlDbType.Int);
            else
                sqlParams[5] = new SqlParameter("@ID", id);
            sqlParams[6] = new SqlParameter("@WSid", WSid);
            sqlParams[7] = new SqlParameter("@DeptNo", Deptno);
            DataSet ds = SQLDBUtil.ExecuteDataset("T_HMS_empVsAirTicketsAuth_LISTbyID_status", sqlParams);
            objHrCommon.NoofRecords = (int)sqlParams[3].Value;
            objHrCommon.TotalPages = (int)sqlParams[2].Value;
            return ds;
        }
        public static DataSet T_HMS_empVsAirTicketsAuth_LISTbyID_status_New(HRCommon objHrCommon, bool Status, int id, int? WSid, int? Deptno, int? EmpID)
        {
            SqlParameter[] sqlParams = new SqlParameter[9];
            sqlParams[0] = new SqlParameter("@CurrentPage", objHrCommon.CurrentPage);
            sqlParams[1] = new SqlParameter("@PageSize", objHrCommon.PageSize);
            sqlParams[2] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
            sqlParams[2].Direction = ParameterDirection.ReturnValue;
            sqlParams[3] = new SqlParameter("@NoofRecords", System.Data.SqlDbType.Int);
            sqlParams[3].Direction = ParameterDirection.Output;
            sqlParams[4] = new SqlParameter("@Status", Status);
            if (id == 0)
                sqlParams[5] = new SqlParameter("@ID", System.Data.SqlDbType.Int);
            else
                sqlParams[5] = new SqlParameter("@ID", id);
            sqlParams[6] = new SqlParameter("@WSid", WSid);
            sqlParams[7] = new SqlParameter("@DeptNo", Deptno);
            if (EmpID == 0)
                sqlParams[8] = new SqlParameter("@EmpID", System.Data.SqlDbType.Int);
            else
                sqlParams[8] = new SqlParameter("@EmpID", EmpID);
            DataSet ds = SQLDBUtil.ExecuteDataset("T_HMS_empVsAirTicketsAuth_LISTbyID_status_New", sqlParams);
            objHrCommon.NoofRecords = (int)sqlParams[3].Value;
            objHrCommon.TotalPages = (int)sqlParams[2].Value;
            return ds;
        }
        public static DataSet HR_GetOT_Variables(HRCommon objHrCommon)
        {
            SqlParameter[] sqlParams = new SqlParameter[4];
            sqlParams[0] = new SqlParameter("@CurrentPage", objHrCommon.CurrentPage);
            sqlParams[1] = new SqlParameter("@PageSize", objHrCommon.PageSize);
            sqlParams[2] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
            sqlParams[2].Direction = ParameterDirection.ReturnValue;
            sqlParams[3] = new SqlParameter("@NoofRecords", System.Data.SqlDbType.Int);
            sqlParams[3].Direction = ParameterDirection.Output;
            DataSet ds = SQLDBUtil.ExecuteDataset("HR_Get_OTvarspaging", sqlParams);
            objHrCommon.NoofRecords = (int)sqlParams[3].Value;
            objHrCommon.TotalPages = (int)sqlParams[2].Value;
            return ds;
        }
        public static DataSet HR_GetGetEmployeeTypeByStatus(HRCommon objHrCommon, bool Status, int EmpTyID)
        {
            SqlParameter[] sqlParams = new SqlParameter[6];
            sqlParams[0] = new SqlParameter("@CurrentPage", objHrCommon.CurrentPage);
            sqlParams[1] = new SqlParameter("@PageSize", objHrCommon.PageSize);
            sqlParams[2] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
            sqlParams[2].Direction = ParameterDirection.ReturnValue;
            sqlParams[3] = new SqlParameter("@NoofRecords", System.Data.SqlDbType.Int);
            sqlParams[3].Direction = ParameterDirection.Output;
            sqlParams[4] = new SqlParameter("@Status", Status);
            sqlParams[5] = new SqlParameter("@EmpTyID", EmpTyID);
            DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetEmployeeTypeByStatus", sqlParams);
            objHrCommon.NoofRecords = (int)sqlParams[3].Value;
            objHrCommon.TotalPages = (int)sqlParams[2].Value;
            return ds;
        }
        public static DataSet HR_GetAirlineByStatus(HRCommon objHrCommon, bool status)
        {
            SqlParameter[] sqlParams = new SqlParameter[5];
            sqlParams[0] = new SqlParameter("@CurrentPage", objHrCommon.CurrentPage);
            sqlParams[1] = new SqlParameter("@PageSize", objHrCommon.PageSize);
            sqlParams[2] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
            sqlParams[2].Direction = ParameterDirection.ReturnValue;
            sqlParams[3] = new SqlParameter("@NoofRecords", System.Data.SqlDbType.Int);
            sqlParams[3].Direction = ParameterDirection.Output;
            sqlParams[4] = new SqlParameter("@active", status);
            DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetAirlineByStatus", sqlParams);
            objHrCommon.NoofRecords = (int)sqlParams[3].Value;
            objHrCommon.TotalPages = (int)sqlParams[2].Value;
            return ds;
        }
        public static DataSet HR_GetAirlineByStatus_New(HRCommon objHrCommon, bool status, int id)
        {
            SqlParameter[] sqlParams = new SqlParameter[6];
            sqlParams[0] = new SqlParameter("@CurrentPage", objHrCommon.CurrentPage);
            sqlParams[1] = new SqlParameter("@PageSize", objHrCommon.PageSize);
            sqlParams[2] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
            sqlParams[2].Direction = ParameterDirection.ReturnValue;
            sqlParams[3] = new SqlParameter("@NoofRecords", System.Data.SqlDbType.Int);
            sqlParams[3].Direction = ParameterDirection.Output;
            sqlParams[4] = new SqlParameter("@active", status);
            sqlParams[5] = new SqlParameter("@id", id);
            DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetAirlineByStatus_New", sqlParams);
            objHrCommon.NoofRecords = (int)sqlParams[3].Value;
            objHrCommon.TotalPages = (int)sqlParams[2].Value;
            return ds;
        }
        public static DataSet HR_GetDepartmentsByStatus(HRCommon objHrCommon, bool Status ,int deptid)
        {
            SqlParameter[] sqlParams = new SqlParameter[6];
            sqlParams[0] = new SqlParameter("@CurrentPage", objHrCommon.CurrentPage);
            sqlParams[1] = new SqlParameter("@PageSize", objHrCommon.PageSize);
            sqlParams[2] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
            sqlParams[2].Direction = ParameterDirection.ReturnValue;
            sqlParams[3] = new SqlParameter("@NoofRecords", System.Data.SqlDbType.Int);
            sqlParams[3].Direction = ParameterDirection.Output;
            sqlParams[4] = new SqlParameter("@Status", Status);
            sqlParams[5] = new SqlParameter("@deptid", deptid);
            DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetDepartmentsByStatus", sqlParams);
            objHrCommon.NoofRecords = (int)sqlParams[3].Value;
            objHrCommon.TotalPages = (int)sqlParams[2].Value;
            return ds;
        }
        public static DataSet HR_DepartmentBySiteFilter(String SearchKey)
        {
            try
            {
                SqlParameter[] sqlPrms = new SqlParameter[1];
                sqlPrms[0] = new SqlParameter("@SEARCH", SearchKey);
                return SQLDBUtil.ExecuteDataset("HR_DepartmentBySiteFilter", sqlPrms);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static DataSet HR_CategoriesBySiteFilter(String SearchKey)
        {
            try
            {
                SqlParameter[] sqlPrms = new SqlParameter[1];
                sqlPrms[0] = new SqlParameter("@SEARCH", SearchKey);
                return SQLDBUtil.ExecuteDataset("HR_CategoriesBySiteFilter", sqlPrms);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static DataSet SH_EmpGrades_position(String SearchKey,bool status)
        {
            try
            {
                SqlParameter[] sqlPrms = new SqlParameter[2];
                sqlPrms[0] = new SqlParameter("@SEARCH", SearchKey);
                sqlPrms[1] = new SqlParameter("@status", status);
                return SQLDBUtil.ExecuteDataset("SH_EmpGrades_position", sqlPrms);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static DataSet SH_Biometric(String SearchKey, bool status)
        {
            try
            {
                SqlParameter[] sqlPrms = new SqlParameter[2];
                sqlPrms[0] = new SqlParameter("@SEARCH", SearchKey);
                sqlPrms[1] = new SqlParameter("@status", status);
                return SQLDBUtil.ExecuteDataset("SH_Biometric", sqlPrms);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static DataSet SH_EmployeeType(String SearchKey, bool status)
        {
            try
            {
                SqlParameter[] sqlPrms = new SqlParameter[2];
                sqlPrms[0] = new SqlParameter("@SEARCH", SearchKey);
                sqlPrms[1] = new SqlParameter("@status", status);
                return SQLDBUtil.ExecuteDataset("SH_EmployeeType", sqlPrms);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static DataSet SH_DocMaster(String SearchKey, bool status)
        {
            try
            {
                SqlParameter[] sqlPrms = new SqlParameter[2];
                sqlPrms[0] = new SqlParameter("@SEARCH", SearchKey);
                sqlPrms[1] = new SqlParameter("@status", status);
                return SQLDBUtil.ExecuteDataset("SH_DocMaster", sqlPrms);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static DataSet SH_CalenderYear(String SearchKey)
        {
            try
            {
                SqlParameter[] sqlPrms = new SqlParameter[1];
                sqlPrms[0] = new SqlParameter("@SEARCH", SearchKey);
                return SQLDBUtil.ExecuteDataset("SH_CalenderYear", sqlPrms);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static DataSet SH_EmpGrades_Medical(String SearchKey, bool status)
        {
            try
            {
                SqlParameter[] sqlPrms = new SqlParameter[2];
                sqlPrms[0] = new SqlParameter("@SEARCH", SearchKey);
                sqlPrms[1] = new SqlParameter("@status", status);
                return SQLDBUtil.ExecuteDataset("SH_EmpGrades_Medical", sqlPrms);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static DataSet HR_DesiginationBySiteFilter(String SearchKey)
        {
            try
            {
                SqlParameter[] sqlPrms = new SqlParameter[1];
                sqlPrms[0] = new SqlParameter("@SEARCH", SearchKey);
                return SQLDBUtil.ExecuteDataset("HR_DesiginationBySiteFilter", sqlPrms);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static DataSet HR_GetBookingClassByStatus(HRCommon objHrCommon, int Status, int id)
        {
            SqlParameter[] sqlParams = new SqlParameter[6];
            sqlParams[0] = new SqlParameter("@CurrentPage", objHrCommon.CurrentPage);
            sqlParams[1] = new SqlParameter("@PageSize", objHrCommon.PageSize);
            sqlParams[2] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
            sqlParams[2].Direction = ParameterDirection.ReturnValue;
            sqlParams[3] = new SqlParameter("@NoofRecords", System.Data.SqlDbType.Int);
            sqlParams[3].Direction = ParameterDirection.Output;
            sqlParams[4] = new SqlParameter("@Status", Status);
            sqlParams[5] = new SqlParameter("@id", id);
            DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetBookingByStatus", sqlParams);
            objHrCommon.NoofRecords = (int)sqlParams[3].Value;
            objHrCommon.TotalPages = (int)sqlParams[2].Value;
            return ds;
        }
        public static DataSet HR_GetPassengerTypeByStatus(HRCommon objHrCommon, int Status, int id)
        {
            SqlParameter[] sqlParams = new SqlParameter[6];
            sqlParams[0] = new SqlParameter("@CurrentPage", objHrCommon.CurrentPage);
            sqlParams[1] = new SqlParameter("@PageSize", objHrCommon.PageSize);
            sqlParams[2] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
            sqlParams[2].Direction = ParameterDirection.ReturnValue;
            sqlParams[3] = new SqlParameter("@NoofRecords", System.Data.SqlDbType.Int);
            sqlParams[3].Direction = ParameterDirection.Output;
            sqlParams[4] = new SqlParameter("@Status", Status);
            sqlParams[5] = new SqlParameter("@id", id);
            DataSet ds = SQLDBUtil.ExecuteDataset("HR_Get_PassengerTypeByStatus", sqlParams);
            objHrCommon.NoofRecords = (int)sqlParams[3].Value;
            objHrCommon.TotalPages = (int)sqlParams[2].Value;
            return ds;
        }
        public static DataSet HR_GetRelationTypeByStatus(HRCommon objHrCommon, int Status, int id)
        {
            SqlParameter[] sqlParams = new SqlParameter[6];
            sqlParams[0] = new SqlParameter("@CurrentPage", objHrCommon.CurrentPage);
            sqlParams[1] = new SqlParameter("@PageSize", objHrCommon.PageSize);
            sqlParams[2] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
            sqlParams[2].Direction = ParameterDirection.ReturnValue;
            sqlParams[3] = new SqlParameter("@NoofRecords", System.Data.SqlDbType.Int);
            sqlParams[3].Direction = ParameterDirection.Output;
            sqlParams[4] = new SqlParameter("@Status", Status);
            sqlParams[5] = new SqlParameter("@id", id);
            DataSet ds = SQLDBUtil.ExecuteDataset("HR_Get_RelationTypeByStatus", sqlParams);
            objHrCommon.NoofRecords = (int)sqlParams[3].Value;
            objHrCommon.TotalPages = (int)sqlParams[2].Value;
            return ds;
        }
        public static DataSet HR_GetCatogoryByStatus(HRCommon objHrCommon, bool Status, int categid)
        {
            SqlParameter[] sqlParams = new SqlParameter[6];
            sqlParams[0] = new SqlParameter("@CurrentPage", objHrCommon.CurrentPage);
            sqlParams[1] = new SqlParameter("@PageSize", objHrCommon.PageSize);
            sqlParams[2] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
            sqlParams[2].Direction = ParameterDirection.ReturnValue;
            sqlParams[3] = new SqlParameter("@NoofRecords", System.Data.SqlDbType.Int);
            sqlParams[3].Direction = ParameterDirection.Output;
            sqlParams[4] = new SqlParameter("@Status", Status);
            sqlParams[5] = new SqlParameter("@categid", categid);
            DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetCatogoryByStatus", sqlParams);
            objHrCommon.NoofRecords = (int)sqlParams[3].Value;
            objHrCommon.TotalPages = (int)sqlParams[2].Value;
            return ds;
        }
        public static DataSet HR_GetWorkSiteByStatus(HRCommon objHrCommon, bool Status)
        {
            SqlParameter[] sqlParams = new SqlParameter[5];
            sqlParams[0] = new SqlParameter("@CurrentPage", objHrCommon.CurrentPage);
            sqlParams[1] = new SqlParameter("@PageSize", objHrCommon.PageSize);
            sqlParams[2] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
            sqlParams[2].Direction = ParameterDirection.ReturnValue;
            sqlParams[3] = new SqlParameter("@NoofRecords", System.Data.SqlDbType.Int);
            sqlParams[3].Direction = ParameterDirection.Output;
            sqlParams[4] = new SqlParameter("@Status", Status);
            DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetWorkSiteByStatus", sqlParams);
            objHrCommon.NoofRecords = (int)sqlParams[3].Value;
            objHrCommon.TotalPages = (int)sqlParams[2].Value;
            return ds;
        }
        public static DataSet GetWorkSiteByCmpIDStatusByPaging(HRCommon objHrCommon, int CompanyID, bool Status, string Wsname)
        {
            SqlParameter[] sqlParams = new SqlParameter[7];
            sqlParams[0] = new SqlParameter("@CurrentPage", objHrCommon.CurrentPage);
            sqlParams[1] = new SqlParameter("@PageSize", objHrCommon.PageSize);
            sqlParams[2] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
            sqlParams[2].Direction = ParameterDirection.ReturnValue;
            sqlParams[3] = new SqlParameter("@NoofRecords", System.Data.SqlDbType.Int);
            sqlParams[3].Direction = ParameterDirection.Output;
            sqlParams[4] = new SqlParameter("@Status", Status);
            sqlParams[5] = new SqlParameter("@CompanyID", CompanyID);
            sqlParams[6] = new SqlParameter("@WsName", Wsname);
            DataSet ds = SQLDBUtil.ExecuteDataset("HMS_GetWorkSiteByCompanyIDStatusPaging", sqlParams);
            objHrCommon.NoofRecords = (int)sqlParams[3].Value;
            objHrCommon.TotalPages = (int)sqlParams[2].Value;
            return ds;
        }
        public static DataSet HR_GetProvisions(HRCommon objHrCommon, int? WS, int? PrjID, int? Dept, int? EmpID, int? Year, int? Month)
        {
            SqlParameter[] sqlParams = new SqlParameter[10];
            sqlParams[0] = new SqlParameter("@CurrentPage", objHrCommon.CurrentPage);
            sqlParams[1] = new SqlParameter("@PageSize", objHrCommon.PageSize);
            sqlParams[2] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
            sqlParams[2].Direction = ParameterDirection.ReturnValue;
            sqlParams[3] = new SqlParameter("@NoofRecords", System.Data.SqlDbType.Int);
            sqlParams[3].Direction = ParameterDirection.Output;
            sqlParams[4] = new SqlParameter("@wsid", WS);
            sqlParams[5] = new SqlParameter("@Projectid", PrjID);
            sqlParams[6] = new SqlParameter("@deptno", Dept);
            sqlParams[7] = new SqlParameter("@empid", EmpID);
            sqlParams[8] = new SqlParameter("@year", Year);
            sqlParams[9] = new SqlParameter("@month", Month);
            DataSet ds = SQLDBUtil.ExecuteDataset("sh_ProvisionCalculation", sqlParams);
            objHrCommon.NoofRecords = (int)sqlParams[3].Value;
            objHrCommon.TotalPages = (int)sqlParams[2].Value;
            return ds;
        }
        public static DataSet HMS_GetEmpClearencePaging(HRCommon objHrCommon, string ItemName)
        {
            SqlParameter[] sqlParams = new SqlParameter[5];
            sqlParams[0] = new SqlParameter("@CurrentPage", objHrCommon.CurrentPage);
            sqlParams[1] = new SqlParameter("@PageSize", objHrCommon.PageSize);
            sqlParams[2] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
            sqlParams[2].Direction = ParameterDirection.ReturnValue;
            sqlParams[3] = new SqlParameter("@NoofRecords", System.Data.SqlDbType.Int);
            sqlParams[3].Direction = ParameterDirection.Output;
            sqlParams[4] = new SqlParameter("@ItemName", ItemName);
            DataSet ds = SQLDBUtil.ExecuteDataset("HMS_GetEmpClearencePaging", sqlParams);
            objHrCommon.NoofRecords = (int)sqlParams[3].Value;
            objHrCommon.TotalPages = (int)sqlParams[2].Value;
            return ds;
        }
        public static DataSet HR_Getemployeeclearence(HRCommon objHrCommon, int? Dept, int CompanyID, string ItemName)
        {
            SqlParameter[] sqlParams = new SqlParameter[8];
            sqlParams[0] = new SqlParameter("@CurrentPage", objHrCommon.CurrentPage);
            sqlParams[1] = new SqlParameter("@PageSize", objHrCommon.PageSize);
            sqlParams[2] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
            sqlParams[2].Direction = ParameterDirection.ReturnValue;
            sqlParams[3] = new SqlParameter("@NoofRecords", 3);
            sqlParams[3].Direction = ParameterDirection.Output;
            sqlParams[4] = new SqlParameter("@Dept", Dept);
            sqlParams[5] = new SqlParameter("@CompanyID", CompanyID);
            if (ItemName != string.Empty)
                sqlParams[6] = new SqlParameter("@ItemName", ItemName);
            else
                sqlParams[6] = new SqlParameter("@ItemName", DBNull.Value);
            sqlParams[7] = new SqlParameter("@Status", objHrCommon.Status);
            DataSet ds = SQLDBUtil.ExecuteDataset("HR_Getemployeeclearence", sqlParams);
            objHrCommon.NoofRecords = (int)sqlParams[3].Value;
            objHrCommon.TotalPages = (int)sqlParams[2].Value;
            return ds;
        }
        public static DataSet HR_Getemployeeclearence_EMP(HRCommon objHrCommon, int? EmpID, int? WS, int? Dept, int CompanyID)
        {
            SqlParameter[] sqlParams = new SqlParameter[8];
            sqlParams[0] = new SqlParameter("@CurrentPage", objHrCommon.CurrentPage);
            sqlParams[1] = new SqlParameter("@PageSize", objHrCommon.PageSize);
            sqlParams[2] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
            sqlParams[2].Direction = ParameterDirection.ReturnValue;
            sqlParams[3] = new SqlParameter("@NoofRecords", System.Data.SqlDbType.Int);
            sqlParams[3].Direction = ParameterDirection.Output;
            sqlParams[4] = new SqlParameter("@EmpID", EmpID);
            sqlParams[5] = new SqlParameter("@WS", WS);
            sqlParams[6] = new SqlParameter("@Dept", Dept);
            sqlParams[7] = new SqlParameter("@CompanyID", CompanyID);
            DataSet ds = SQLDBUtil.ExecuteDataset("HR_Getemployeeclearence_EMP", sqlParams);
            objHrCommon.NoofRecords = (int)sqlParams[3].Value;
            objHrCommon.TotalPages = (int)sqlParams[2].Value;
            return ds;
        }
        public static DataSet HR_GetemployeeBlack_EMP(HRCommon objHrCommon, int? EmpID, int? WS, int? Dept, int CompanyID)
        {
            SqlParameter[] sqlParams = new SqlParameter[8];
            sqlParams[0] = new SqlParameter("@CurrentPage", objHrCommon.CurrentPage);
            sqlParams[1] = new SqlParameter("@PageSize", objHrCommon.PageSize);
            sqlParams[2] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
            sqlParams[2].Direction = ParameterDirection.ReturnValue;
            sqlParams[3] = new SqlParameter("@NoofRecords", System.Data.SqlDbType.Int);
            sqlParams[3].Direction = ParameterDirection.Output;
            sqlParams[4] = new SqlParameter("@EmpID", EmpID);
            sqlParams[5] = new SqlParameter("@WS", WS);
            sqlParams[6] = new SqlParameter("@Dept", Dept);
            sqlParams[7] = new SqlParameter("@CompanyID", CompanyID);
            DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetBlacklist_EMP", sqlParams);
            objHrCommon.NoofRecords = (int)sqlParams[3].Value;
            objHrCommon.TotalPages = (int)sqlParams[2].Value;
            return ds;
        }
        public static void HR_Upd_DesigStatus(int DisgID, bool Status)
        {
            SqlParameter[] parm = new SqlParameter[2];
            parm[0] = new SqlParameter("@DisgID", DisgID);
            parm[1] = new SqlParameter("@Status", Status);
            SQLDBUtil.ExecuteNonQuery("HR_Upd_DesigStatus", parm);
        }
        public static void HMS_ActiveInActiveItems(int ID, bool Status, string spName)
        {
            SqlParameter[] parm = new SqlParameter[2];
            parm[0] = new SqlParameter("@DisgID", ID);
            parm[1] = new SqlParameter("@Status", Status);
            SQLDBUtil.ExecuteNonQuery(spName, parm);
        }
        public static void HR_Upd_OTVAr(int ID, string name, string val)
        {
            SqlParameter[] parm = new SqlParameter[3];
            parm[0] = new SqlParameter("@DisgID", ID);
            parm[1] = new SqlParameter("@Status", name);
            parm[2] = new SqlParameter("@Status", val);
            SQLDBUtil.ExecuteNonQuery("HR_Upd_OTVar", parm);
        }
        public static void HR_Upd_DesigStatus_employeetypes(int EmptyID, bool Status)
        {
            SqlParameter[] parm = new SqlParameter[2];
            parm[0] = new SqlParameter("@EmptyID", EmptyID);
            parm[1] = new SqlParameter("@Status", Status);
            SQLDBUtil.ExecuteNonQuery("HR_Upd_DesigStatus_employeetypes", parm);
        }
        public static void HR_Upd_CategoryStatus(int CatID, bool Status)
        {
            SqlParameter[] parm = new SqlParameter[2];
            parm[0] = new SqlParameter("@CatID", CatID);
            parm[1] = new SqlParameter("@Status", Status);
            SQLDBUtil.ExecuteNonQuery("HR_Upd_CategoryStatus", parm);
        }
        public static void HR_Upd_WorkSiteStatus(int CatID, bool Status)
        {
            SqlParameter[] parm = new SqlParameter[2];
            parm[0] = new SqlParameter("@CatID", CatID);
            parm[1] = new SqlParameter("@Status", Status);
            SQLDBUtil.ExecuteNonQuery("HR_Upd_WorkSiteStatus", parm);
        }
        public static int HR_InsUpShiftTimeings(int ShiftID, string Name, string InTime, string OutTime, int Status)
        {
            try
            {
                SqlParameter[] parm = new SqlParameter[6];
                parm[0] = new SqlParameter("@ShiftID", ShiftID);
                parm[1] = new SqlParameter("@Name", Name);
                parm[2] = new SqlParameter("@InTime", InTime);
                parm[3] = new SqlParameter("@OutTime", OutTime);
                parm[4] = new SqlParameter("@Status", Status);
                parm[5] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
                parm[5].Direction = ParameterDirection.ReturnValue;
                SQLDBUtil.ExecuteNonQuery("HR_InsUpShiftTimeings", parm);
                return Convert.ToInt32(parm[5].Value);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static DataSet HR_GetShiftTimings(int ShiftID)
        {
            SqlParameter[] parm = new SqlParameter[1];
            parm[0] = new SqlParameter("@ShiftID", ShiftID);
            DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetShiftTimings", parm);
            return ds;
        }
        public static DataSet HR_GetShiftConfigration(int ShiftID, int NatureID, int WkID, int UID)
        {
            SqlParameter[] parm = new SqlParameter[4];
            parm[0] = new SqlParameter("@ShiftID", ShiftID);
            parm[1] = new SqlParameter("@NatureID", NatureID);
            parm[2] = new SqlParameter("@WkID", WkID);
            parm[3] = new SqlParameter("@UID", UID);
            DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetShiftConfigration", parm);
            return ds;
        }
        public static DataSet HR_GetShiftConfigration_ByID(int ShiftConfig)
        {
            SqlParameter[] parm = new SqlParameter[1];
            parm[0] = new SqlParameter("@ShiftConfig", ShiftConfig);
            DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetShiftConfigration_ByID", parm);
            return ds;
        }
        public static DataSet HR_GetNoofWeeks()
        {
            return SQLDBUtil.ExecuteDataset("HR_GetNoofWeeks");
        }
        public static int HR_UpdateShiftConfigration(int ShiftConfigID, string inTy, string OutTy, int UID)
        {
            SqlParameter[] parm = new SqlParameter[4];
            parm[0] = new SqlParameter("@ShiftConfigID", ShiftConfigID);
            parm[1] = new SqlParameter("@inTy", inTy);
            parm[2] = new SqlParameter("@OutTy", OutTy);
            parm[3] = new SqlParameter("@UID", UID);
            return SQLDBUtil.ExecuteNonQuery("HR_UpdateShiftConfigration", parm);
        }
        #region AttendanceDevice
        public int HR_InsUpAttendanceDevice(int DeviceID, string DeviceName, string Location, int Status, int CreatedBy)
        {
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[6];
                sqlParams[0] = new SqlParameter("@DeviceID", DeviceID);
                sqlParams[1] = new SqlParameter("@DeviceName", DeviceName);
                sqlParams[2] = new SqlParameter("@Location", Location);
                sqlParams[3] = new SqlParameter("@Status", Status);
                sqlParams[4] = new SqlParameter("@CreatedBy", CreatedBy);
                sqlParams[5] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
                sqlParams[5].Direction = ParameterDirection.ReturnValue;
                SQLDBUtil.ExecuteNonQuery("HR_InsUpdAttendanceDevice", sqlParams);
                return Convert.ToInt32(sqlParams[5].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet HR_GetAttendanceDeviceByPaging(HRCommon objHrCommon, bool Status, int BIOID)
        {
            SqlParameter[] sqlParams = new SqlParameter[6];
            sqlParams[0] = new SqlParameter("@CurrentPage", objHrCommon.CurrentPage);
            sqlParams[1] = new SqlParameter("@PageSize", objHrCommon.PageSize);
            sqlParams[2] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
            sqlParams[2].Direction = ParameterDirection.ReturnValue;
            sqlParams[3] = new SqlParameter("@NoofRecords", System.Data.SqlDbType.Int);
            sqlParams[3].Direction = ParameterDirection.Output;
            sqlParams[4] = new SqlParameter("@Status", Status);
            sqlParams[5] = new SqlParameter("@BIOID", BIOID);
            DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetAttendanceDeviceByPaging", sqlParams);
            objHrCommon.NoofRecords = (int)sqlParams[3].Value;
            objHrCommon.TotalPages = (int)sqlParams[2].Value;
            return ds;
        }
        public static DataSet GetAttendanceDeviceDetails(int DeviceID)
        {
            try
            {
                SqlParameter[] objParam = new SqlParameter[1];
                objParam[0] = new SqlParameter("@DeviceID", DeviceID);
                DataSet dsWorkSiteDetails = SQLDBUtil.ExecuteDataset("HMS_GetAttendanceDeviceDetails", objParam);
                return dsWorkSiteDetails;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static void HMS_Upd_AttendanceDeviceStatus(int DeviceID, bool Status)
        {
            SqlParameter[] parm = new SqlParameter[2];
            parm[0] = new SqlParameter("@DeviceID", DeviceID);
            parm[1] = new SqlParameter("@Status", Status);
            SQLDBUtil.ExecuteNonQuery("HMS_Upd_AttendanceDeviceStatus", parm);
        }
        public static int HMS_Upd_AttendanceDeviceHMSEmpID(int DHID, int DeviceEmpID, int HMSEmpID)
        {
            SqlParameter[] parm = new SqlParameter[4];
            parm[0] = new SqlParameter("@DHID", DHID);
            parm[1] = new SqlParameter("@DeviceEmpID", DeviceEmpID);
            parm[2] = new SqlParameter("@HMSEmpID", HMSEmpID);
            parm[3] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
            parm[3].Direction = ParameterDirection.ReturnValue;
            SQLDBUtil.ExecuteNonQuery("HMS_InsUpdDeviceHMSEmpID", parm);
            return Convert.ToInt32(parm[3].Value);
        }
        public static DataSet GetDeviceDetails()
        {
            DataSet ds = SQLDBUtil.ExecuteDataset("GetDeviceDetails");
            return ds;
        }
        public static DataSet HR_GetEmployeeAttendanceDeviceByPaging(HRCommon objHrCommon, int Status, int DeviceID, int CompanyID)
        {
            SqlParameter[] sqlParams = new SqlParameter[7];
            sqlParams[0] = new SqlParameter("@CurrentPage", objHrCommon.CurrentPage);
            sqlParams[1] = new SqlParameter("@PageSize", objHrCommon.PageSize);
            sqlParams[2] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
            sqlParams[2].Direction = ParameterDirection.ReturnValue;
            sqlParams[3] = new SqlParameter("@NoofRecords", System.Data.SqlDbType.Int);
            sqlParams[3].Direction = ParameterDirection.Output;
            sqlParams[4] = new SqlParameter("@Status", Status);
            sqlParams[5] = new SqlParameter("@DeviceID ", DeviceID);
            sqlParams[6] = new SqlParameter("@CompanyID ", CompanyID);
            DataSet ds = SQLDBUtil.ExecuteDataset("HMS_GetEmployeeListFromDevice", sqlParams);
            objHrCommon.NoofRecords = (int)sqlParams[3].Value;
            objHrCommon.TotalPages = (int)sqlParams[2].Value;
            return ds;
        }
        public static int InsDevHmsEmpID(int? DHID, int? DeviceID, int DeviceEmpID, int HmsEmpID)
        {
            SqlParameter[] parm = new SqlParameter[5];
            parm[0] = new SqlParameter("@DHID", DHID);
            parm[1] = new SqlParameter("@deviceID", DeviceID);
            parm[2] = new SqlParameter("@deviceEmpID", DeviceEmpID);
            parm[3] = new SqlParameter("@hmsEmpID", HmsEmpID);
            parm[4] = new SqlParameter("ReturnvValue", System.Data.SqlDbType.Int);
            parm[4].Direction = ParameterDirection.ReturnValue;
            SQLDBUtil.ExecuteNonQuery("HMS_InsUpdDeviceHMSEmpID", parm);
            return Convert.ToInt32(parm[4].Value);
        }
        #endregion AttendanceDevice
        public static int AddNewState(string NewState, int Country)
        {
            try
            {
                SqlParameter[] objParam = new SqlParameter[3];
                objParam[0] = new SqlParameter("@NewState", NewState);
                objParam[1] = new SqlParameter("@Country", Country);
                objParam[2] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
                objParam[2].Direction = ParameterDirection.ReturnValue;
                SQLDBUtil.ExecuteNonQuery("HR_AddUpdateEmpDocsGeneral", objParam);
                return Convert.ToInt32(objParam[2].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #region PF
        public static DataSet GetPFDetails()
        {
            return SQLDBUtil.ExecuteDataset("CMS_GetPFCode");
        }
        public static int InsUpdGetPFAccount(int? PFID, int OfficeID, int AccountNO, int? EmpID, int CreatedBy)
        {
            SqlParameter[] parm = new SqlParameter[6];
            parm[0] = new SqlParameter("@PFID", PFID);
            parm[1] = new SqlParameter("@OfficeID", OfficeID);
            parm[2] = new SqlParameter("@AccountNO", AccountNO);
            parm[3] = new SqlParameter("ReturnvValue", System.Data.SqlDbType.Int);
            parm[3].Direction = ParameterDirection.ReturnValue;
            parm[4] = new SqlParameter("@EmpID", EmpID);
            parm[5] = new SqlParameter("@CreatedBy", CreatedBy);
            SQLDBUtil.ExecuteNonQuery("HR_InsUpdEmpPFAccount", parm);
            return Convert.ToInt32(parm[3].Value);
        }
        #endregion PF
        #region Reports
        public static DataSet ESIDataset(DateTime? Fromdate, DateTime? Todate, int? WorksiteID, int? DeptID, int? EmpID, string EmpName, int CompanyID)
        {
            SqlParameter[] parm = new SqlParameter[7];
            parm[0] = new SqlParameter("@FromDate", Fromdate);
            parm[1] = new SqlParameter("@ToDate", Todate);
            parm[2] = new SqlParameter("@WSID", WorksiteID);
            parm[3] = new SqlParameter("@DeptID", DeptID);
            parm[4] = new SqlParameter("@EmpID", EmpID);
            parm[5] = new SqlParameter("@EmpName", EmpName);
            parm[6] = new SqlParameter("@CompanyID", CompanyID);
            return SQLDBUtil.ExecuteDataset("HMS_Report_ESI", parm);
        }
        public static DataSet PFDataset(DateTime? Fromdate, DateTime? Todate, int? WorksiteID, int? DeptID, int? EmpID, string EmpName, int CompanyID)
        {
            SqlParameter[] parm = new SqlParameter[7];
            parm[0] = new SqlParameter("@FromDate", Fromdate);
            parm[1] = new SqlParameter("@ToDate", Todate);
            parm[2] = new SqlParameter("@WSID", WorksiteID);
            parm[3] = new SqlParameter("@DeptID", DeptID);
            parm[4] = new SqlParameter("@EmpID", EmpID);
            parm[5] = new SqlParameter("@EmpName", EmpName);
            parm[6] = new SqlParameter("@CompanyID", CompanyID);
            return SQLDBUtil.ExecuteDataset("HMS_Report_PF", parm);
        }
        public static DataSet PTDataset(DateTime? Fromdate, DateTime? Todate, int? WorksiteID, int? DeptID, int? EmpID, string EmpName, int CompanyID)
        {
            SqlParameter[] parm = new SqlParameter[7];
            parm[0] = new SqlParameter("@FromDate", Fromdate);
            parm[1] = new SqlParameter("@ToDate", Todate);
            parm[2] = new SqlParameter("@WSID", WorksiteID);
            parm[3] = new SqlParameter("@DeptID", DeptID);
            parm[4] = new SqlParameter("@EmpID", EmpID);
            parm[5] = new SqlParameter("@EmpName", EmpName);
            parm[6] = new SqlParameter("@CompanyID", CompanyID);
            DataSet ds = SQLDBUtil.ExecuteDataset("HMS_Report_PT", parm);
            return ds;
        }
        public static DataSet TDSDataset(DateTime? Fromdate, DateTime? Todate, int? WorksiteID, int? DeptID, int? EmpID, string EmpName, int CompanyID)
        {
            SqlParameter[] parm = new SqlParameter[7];
            parm[0] = new SqlParameter("@FromDate", Fromdate);
            parm[1] = new SqlParameter("@ToDate", Todate);
            parm[2] = new SqlParameter("@WSID", WorksiteID);
            parm[3] = new SqlParameter("@DeptID", DeptID);
            parm[4] = new SqlParameter("@EmpID", EmpID);
            parm[5] = new SqlParameter("@EmpName", EmpName);
            parm[6] = new SqlParameter("@CompanyID", CompanyID);
            return SQLDBUtil.ExecuteDataset("HMS_Report_TDS", parm);
        }
        #endregion Reports
        #region EmployeeType
        public static int InsUpdEmployeeType(ref int? EmpTypeID, string EmpType, int Status, int EmpID)
        {
            try
            {
                SqlParameter[] parm = new SqlParameter[5];
                parm[0] = new SqlParameter("@EmpTypeID", EmpTypeID);
                parm[1] = new SqlParameter("@EmpType", EmpType);
                parm[2] = new SqlParameter("@Status", Status);
                parm[3] = new SqlParameter("@CreatedBy", EmpID);
                parm[4] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
                parm[4].Direction = ParameterDirection.ReturnValue;
                SQLDBUtil.ExecuteNonQuery("HMS_InsUpdEmpType", parm);
                return Convert.ToInt32(parm[4].Value);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion EmployeeType
        #region Search Employee
        public static DataSet GetEmployees(String SearchKey)
        {
            try
            {
                SqlParameter[] sqlPrms = new SqlParameter[1];
                sqlPrms[0] = new SqlParameter("@SEARCH", SearchKey);
                return SQLDBUtil.ExecuteDataset("HMS_Service_SearchEmployee", sqlPrms);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static DataSet GetDepartment(String SearchKey, int SearchCompanyID, int Siteid)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[3];
                param[0] = new SqlParameter("@Search", SearchKey);
                param[1] = new SqlParameter("@CompanyID", SearchCompanyID);
                param[2] = new SqlParameter("@SiteID", Siteid);
                return SQLDBUtil.ExecuteDataset("HMS_googlesearch_GetDepartment", param);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static DataSet GetEmployee(String SearchKey, int SearchCompanyID, int Empdeptid)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[3];
                param[0] = new SqlParameter("@Search", SearchKey);
                param[1] = new SqlParameter("@CompanyID", SearchCompanyID);
                param[2] = new SqlParameter("@DeptID", Empdeptid);
                return SQLDBUtil.ExecuteDataset("G_GET_EmpNamesbyFilter", param);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static DataSet GetDepartmentGoogleSerc(String SearchKey, int SearchCompanyID, int Siteid)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[3];
                param[0] = new SqlParameter("@Search", SearchKey);
                param[1] = new SqlParameter("@CompanyID", SearchCompanyID);
                param[2] = new SqlParameter("@SiteID", Siteid);
                return SQLDBUtil.ExecuteDataset("HMS_googlesearch_GetDepartmentBySite", param);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static DataSet HR_GoogleSearchEmpBySiteDept(String SearchKey, int WS, int DeptNo, string Status, int CompanyID)
        {
            try
            {
                SqlParameter[] parm = new SqlParameter[5];
                parm[0] = new SqlParameter("@Search", SearchKey);
                parm[1] = new SqlParameter("@WS", WS);
                parm[2] = new SqlParameter("@Dept", DeptNo);
                parm[3] = new SqlParameter("@Status", Status);
                parm[4] = new SqlParameter("@CompanyID", CompanyID);
                return SQLDBUtil.ExecuteDataset("HR_GoogleSerac_SearchEmpBySiteDept", parm);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static DataSet HR_GoogleSearchEmpBlack(String SearchKey, int Empid)
        {
            try
            {
                SqlParameter[] parm = new SqlParameter[2];
                parm[0] = new SqlParameter("@Search", SearchKey);
                parm[1] = new SqlParameter("@Empid", DBNull.Value);
                return SQLDBUtil.ExecuteDataset("HMS_googlesearch_Black", parm);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static DataSet GetGoogleSearch(String SearchKey, int Empid)
        {
            try
            {
                SqlParameter[] parm = new SqlParameter[2];
                parm[0] = new SqlParameter("@Search", SearchKey);
                parm[1] = new SqlParameter("@Empid", DBNull.Value);
                return SQLDBUtil.ExecuteDataset("[HMS_GoogleSearch]", parm);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static DataSet HR_GoogleSerac_jobwisedes(String SearchKey, int WS)
        {
            try
            {
                SqlParameter[] parm = new SqlParameter[2];
                parm[0] = new SqlParameter("@Search", SearchKey);
                parm[1] = new SqlParameter("@WS", WS);
                return SQLDBUtil.ExecuteDataset("HR_GoogleSerac_jobwisedes", parm);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static DataSet HR_GoogleSerac_allemployee(String SearchKey)
        {
            try
            {
                SqlParameter[] parm = new SqlParameter[1];
                parm[0] = new SqlParameter("@Search", SearchKey);
                return SQLDBUtil.ExecuteDataset("HR_GoogleSerac_allemployee", parm);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static DataSet HR_GoogleSearchEmp(String SearchKey)
        {
            try
            {
                SqlParameter[] parm = new SqlParameter[1];
                parm[0] = new SqlParameter("@Search", SearchKey);
                return SQLDBUtil.ExecuteDataset("HR_GoogleSearch_EmpIDName", parm);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static DataSet GetGoogleABCSearchWorkSite(String SearchKey, int SearchCompanyID)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[2];
                param[0] = new SqlParameter("@Search", SearchKey);
                param[1] = new SqlParameter("@CompanyID", SearchCompanyID);
                return SQLDBUtil.ExecuteDataset("G_GET_WorkSitebyFilter", param);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static DataSet GetWorkSites(String SearchKey)
        {
            try
            {
                SqlParameter[] par = new SqlParameter[1];
                par[0] = new SqlParameter("@Search", SearchKey);
                return SqlHelper.ExecuteDataset("GEN_Service_SearchWorkSites", par);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet GetsalariesGoogleSearchWorkSite(String SearchKey)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@Search", SearchKey);
                return SQLDBUtil.ExecuteDataset("HR_GetGoogleSearchWorkSite_By_EmpSalaries", param);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static DataSet GetGGoogleSear_By_RentalDocs(String SearchKey)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@Search", SearchKey);
                return SQLDBUtil.ExecuteDataset("HR_GetWorkSiteGoogleSear_By_RentalDocs", param);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static DataSet GetGoogleABCSearchPosition(String SearchKey)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@Search", SearchKey);
                return SQLDBUtil.ExecuteDataset("HR_GetPositionListFilterGS", param);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static DataSet GetWorkSiteActive(String SearchKey)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@Search", SearchKey);
                return SQLDBUtil.ExecuteDataset("HR_GetWorkSite_By_EmpOrderFilter", param);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static DataSet GET_WorkSiteby_Search(String SearchKey)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@Search", SearchKey);
                return SQLDBUtil.ExecuteDataset("GET_WorkSiteby_Search", param);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static DataSet HR_GetWorkSite(String SearchKey)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@Search", SearchKey);
                return SQLDBUtil.ExecuteDataset("sh_Worksite", param);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static DataSet GetDept_search(String SearchKey)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@Search", SearchKey);
                return SQLDBUtil.ExecuteDataset("sh_Dept", param);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static DataSet Getemp_Search(String SearchKey)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@Search", SearchKey);
                return SQLDBUtil.ExecuteDataset("sh_Emp", param);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static DataSet Getemployee_Search(String SearchKey)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@Search", SearchKey);
                return SQLDBUtil.ExecuteDataset("sh_Employee", param);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static DataSet GetWorkSiteLeaveActive(String SearchKey)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@Search", SearchKey);
                return SQLDBUtil.ExecuteDataset("HR_GetWorkSite_By_GetAvailableLeavesFilter", param);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static DataSet HR_GetWorkSite_By_empVsAirTicketsAuth(String SearchKey)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@Search", SearchKey);
                return SQLDBUtil.ExecuteDataset("HR_GetWorkSite_By_empVsAirTicketsAuth", param);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static DataSet GetWorkSiteAvailableLeaveActive(String SearchKey)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@Search", SearchKey);
                return SQLDBUtil.ExecuteDataset("HR_GetWorkSite_By_GetAvailableLeavesFilter", param);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static DataSet GetSearchDesiginationFilter(String SearchKey, int SearchCompanyID, int Empdeptid)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[3];
                param[0] = new SqlParameter("@Search", SearchKey);
                param[1] = new SqlParameter("@CompanyID", SearchCompanyID);
                param[2] = new SqlParameter("@DeptID", Empdeptid);
                return SQLDBUtil.ExecuteDataset("G_GET_DesignationbyFilter", param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet GetSearchDesiginationFilterActive(String SearchKey, int SearchCompanyID, int Siteid)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[3];
                param[0] = new SqlParameter("@Search", SearchKey);
                param[1] = new SqlParameter("@CompanyID", SearchCompanyID);
                param[2] = new SqlParameter("@SiteID", Siteid);
                return SQLDBUtil.ExecuteDataset("HR_GetDepartmentBySiteFilter", param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet GetSearchDeptBlack(String SearchKey, int SearchCompanyID, int Siteid)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@Search", SearchKey);
                return SQLDBUtil.ExecuteDataset("HR_Getgooglesearchdept_Black", param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet GetSearchDePartmentFilterActive_HRLeaveApplications(String SearchKey, int SearchCompanyID, int Siteid)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[3];
                param[0] = new SqlParameter("@Search", SearchKey);
                param[1] = new SqlParameter("@CompanyID", SearchCompanyID);
                param[2] = new SqlParameter("@SiteID", Siteid);
                return SQLDBUtil.ExecuteDataset("HR_GetDepartmentBySiteFilter_HRLeaveApplications", param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet GetEmployees_By_WS_Dept(String SearchKey, int siteid, int Deptid)
        {
            try
            {
                SqlParameter[] sqlPrms = new SqlParameter[3];
                sqlPrms[0] = new SqlParameter("@SEARCH", SearchKey);
                sqlPrms[1] = new SqlParameter("@Siteid", siteid);
                if (siteid == 0)
                {
                    sqlPrms[1] = new SqlParameter("@Siteid", SqlDbType.Int);
                }
                sqlPrms[2] = new SqlParameter("@Deptid", Deptid);
                if (Deptid == 0)
                {
                    sqlPrms[2] = new SqlParameter("@Deptid", SqlDbType.Int);
                }
                return SQLDBUtil.ExecuteDataset("HMS_Service_SearchEmployee_By_WS_Dept", sqlPrms);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static DataSet GetEmployees_DLL_By_WS_Dept(int siteid, int Deptid)
        {
            try
            {
                SqlParameter[] sqlPrms = new SqlParameter[2];
                sqlPrms[0] = new SqlParameter("@Siteid", siteid);
                if (siteid == 0)
                {
                    sqlPrms[0] = new SqlParameter("@Siteid", SqlDbType.Int);
                }
                sqlPrms[1] = new SqlParameter("@Deptid", Deptid);
                if (Deptid == 0)
                {
                    sqlPrms[1] = new SqlParameter("@Deptid", SqlDbType.Int);
                }
                return SQLDBUtil.ExecuteDataset("HMS_Service_DLL_Employee_By_WS_Dept", sqlPrms);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static DataSet HMS_Service_DLL_Employee_By_WS_Dept_googlesearch(String SearchKey, int siteid, int Deptid)
        {
            try
            {
                SqlParameter[] sqlPrms = new SqlParameter[3];
                sqlPrms[0] = new SqlParameter("@search", SearchKey);
                sqlPrms[1] = new SqlParameter("@Siteid", siteid);
                if (siteid == 0)
                {
                    sqlPrms[1] = new SqlParameter("@Siteid", SqlDbType.Int);
                }
                sqlPrms[2] = new SqlParameter("@Deptid", Deptid);
                if (Deptid == 0)
                {
                    sqlPrms[2] = new SqlParameter("@Deptid", SqlDbType.Int);
                }
                return SQLDBUtil.ExecuteDataset("HMS_Service_DLL_Employee_By_WS_Dept_googlesearch", sqlPrms);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static DataSet HMS_Service_DLL_Employee_By_WS_Dept_googlesearch_AttAdv(String SearchKey, int siteid, int Deptid,int month,int year)
        {
            try
            {
                SqlParameter[] sqlPrms = new SqlParameter[5];
                sqlPrms[0] = new SqlParameter("@search", SearchKey);
                sqlPrms[1] = new SqlParameter("@Siteid", siteid);
                if (siteid == 0)
                {
                    sqlPrms[1] = new SqlParameter("@Siteid", SqlDbType.Int);
                }
                sqlPrms[2] = new SqlParameter("@Deptid", Deptid);
                if (Deptid == 0)
                {
                    sqlPrms[2] = new SqlParameter("@Deptid", SqlDbType.Int);
                }
                sqlPrms[3] = new SqlParameter("@month", month);
                sqlPrms[4] = new SqlParameter("@year", year);
                return SQLDBUtil.ExecuteDataset("HMS_Service_DLL_Employee_By_WS_Dept_googlesearch_AttAdv", sqlPrms);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static DataSet HMS_Service_DLL_Employee_By_WS_Dept_googlesearch_AttPay(String SearchKey, int siteid, int Deptid, int month, int year)
        {
            try
            {
                SqlParameter[] sqlPrms = new SqlParameter[5];
                sqlPrms[0] = new SqlParameter("@search", SearchKey);
                sqlPrms[1] = new SqlParameter("@Siteid", siteid);
                if (siteid == 0)
                {
                    sqlPrms[1] = new SqlParameter("@Siteid", SqlDbType.Int);
                }
                sqlPrms[2] = new SqlParameter("@Deptid", Deptid);
                if (Deptid == 0)
                {
                    sqlPrms[2] = new SqlParameter("@Deptid", SqlDbType.Int);
                }
                sqlPrms[3] = new SqlParameter("@month", month);
                sqlPrms[4] = new SqlParameter("@year", year);
                return SQLDBUtil.ExecuteDataset("HMS_Service_DLL_Employee_By_WS_Dept_googlesearch_AttPay", sqlPrms);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static DataSet HMS_Service_DLL_Employee_By_WS_Dept_googlesearch_AttSal(String SearchKey, int siteid, int Deptid, int month, int year)
        {
            try
            {
                SqlParameter[] sqlPrms = new SqlParameter[5];
                sqlPrms[0] = new SqlParameter("@search", SearchKey);
                sqlPrms[1] = new SqlParameter("@Siteid", siteid);
                if (siteid == 0)
                {
                    sqlPrms[1] = new SqlParameter("@Siteid", SqlDbType.Int);
                }
                sqlPrms[2] = new SqlParameter("@Deptid", Deptid);
                if (Deptid == 0)
                {
                    sqlPrms[2] = new SqlParameter("@Deptid", SqlDbType.Int);
                }
                sqlPrms[3] = new SqlParameter("@month", month);
                sqlPrms[4] = new SqlParameter("@year", year);
                return SQLDBUtil.ExecuteDataset("HMS_Service_DLL_Employee_By_WS_Dept_googlesearch_AttSal", sqlPrms);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static DataSet GetSearchWorkSite_google(String SearchKey, int SearchCompanyID)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[2];
                param[0] = new SqlParameter("@Search", SearchKey);
                param[1] = new SqlParameter("@CompanyID", SearchCompanyID);
                return SQLDBUtil.ExecuteDataset("G_GET_WorkSitebyFilter", param);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        #endregion Search Employee
        #region SMSEMPTransfer
        public static DataSet GetSMSTransferEmpDetails(HRCommon objHrCommon, int? SiteID, int? DeptID, int? EmpID, string EmpName)
        {
            SqlParameter[] sqlParams = new SqlParameter[8];
            sqlParams[0] = new SqlParameter("@CurrentPage", objHrCommon.CurrentPage);
            sqlParams[1] = new SqlParameter("@PageSize", objHrCommon.PageSize);
            sqlParams[2] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
            sqlParams[2].Direction = ParameterDirection.ReturnValue;
            sqlParams[3] = new SqlParameter("@NoofRecords", System.Data.SqlDbType.Int);
            sqlParams[3].Direction = ParameterDirection.Output;
            sqlParams[4] = new SqlParameter("@SiteID", SiteID);
            sqlParams[5] = new SqlParameter("@DeptID", DeptID);
            sqlParams[6] = new SqlParameter("@EmpID", EmpID);
            sqlParams[7] = new SqlParameter("@Name", EmpName);
            DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetSMSEMPTransfer", sqlParams);
            objHrCommon.NoofRecords = (int)sqlParams[3].Value;
            objHrCommon.TotalPages = (int)sqlParams[2].Value;
            return ds;
        }
        public static void UpdSMSEMPTransferStatus(int TransID)
        {
            try
            {
                SQLDBUtil.ExecuteNonQuery("HR_UpdSMSEMPTransfer", new SqlParameter[] { new SqlParameter("@TransID", TransID) });
            }
            catch (Exception)
            {
            }
        }
        #endregion SMSEMPTransfer
        #region SMSAtt
        public static DataSet GetAttendanceBySMSByPaging(HRCommon objHrCommon, int? WS, int? Dept, int? Shift, int CompnayID)
        {
            SqlParameter[] sqlParams = new SqlParameter[8];
            sqlParams[0] = new SqlParameter("@CurrentPage", objHrCommon.CurrentPage);
            sqlParams[1] = new SqlParameter("@PageSize", objHrCommon.PageSize);
            sqlParams[2] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
            sqlParams[2].Direction = ParameterDirection.ReturnValue;
            sqlParams[3] = new SqlParameter("@NoofRecords", System.Data.SqlDbType.Int);
            sqlParams[3].Direction = ParameterDirection.Output;
            sqlParams[4] = new SqlParameter("@Site", WS);
            sqlParams[5] = new SqlParameter("@Dept", Dept);
            sqlParams[6] = new SqlParameter("@Shift", Shift);
            sqlParams[7] = new SqlParameter("@CompanyID", CompnayID);
            DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetSMSEmpAttendanceByPaging", sqlParams);
            objHrCommon.NoofRecords = (int)sqlParams[3].Value;
            objHrCommon.TotalPages = (int)sqlParams[2].Value;
            return ds;
        }
        public static void UpdateSMSEmpStatus(int TransID, int MarkedBy)
        {
            SqlParameter[] parm = new SqlParameter[2];
            parm[0] = new SqlParameter("@TransID", TransID);
            parm[1] = new SqlParameter("@UserID", MarkedBy);
            SQLDBUtil.ExecuteNonQuery("HR_UpdateSMSEmpByTransID", parm);
        }
        public static void HR_MarkAttandaceBySMS(int EmpId, int Status, int SiteID, int UserID, string InTime)
        {
            SQLDBUtil.ExecuteDataset("HR_MarkAttandaceBySMS", new SqlParameter[] {
                 new SqlParameter("@EmpId", EmpId), new SqlParameter("@Status", Status), 
                 new SqlParameter("@SiteID", SiteID), new SqlParameter("@UserID", UserID),new SqlParameter("@InTime", InTime) });
        }
        #endregion
        #region Admin
        public static DataSet Getstaus(int ModuleId, int RoleId, int EmpId)
        {
            SqlParameter[] p = new SqlParameter[3];
            p[0] = new SqlParameter("@ModuleId", ModuleId);
            p[1] = new SqlParameter("@RoleId", RoleId);
            p[2] = new SqlParameter("@EmpId", EmpId);
            DataSet ds = SQLDBUtil.ExecuteDataset("HMS_GetStatus", p);
            return ds;
        }
        #endregion Admin
        #region Bank
        public static int InsBankMaster(string BankName, int EmpID)
        {
            try
            {
                SqlParameter[] Parms = new SqlParameter[3];
                Parms[0] = new SqlParameter("@BankName", BankName);
                Parms[1] = new SqlParameter("@Empid", EmpID);
                Parms[2] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
                Parms[2].Direction = ParameterDirection.ReturnValue;
                SQLDBUtil.ExecuteNonQuery("HMS_InsUpdBankMaster", Parms);
                return Convert.ToInt32(Parms[2].Value);
            }
            catch (Exception ex) { throw ex; }
        }
        public static int InsertBankBranchs(int? BankId, string BranchName, int? CountryId, int? StateId, int? CityId)
        {
            SqlParameter[] p = new SqlParameter[6];
            p[0] = new SqlParameter("@BankId", BankId);
            p[1] = new SqlParameter("@BranchName", BranchName);
            p[2] = new SqlParameter("@CountryId", CountryId);
            p[3] = new SqlParameter("@StateId", StateId);
            p[4] = new SqlParameter("@CityId", CityId);
            p[5] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
            p[5].Direction = ParameterDirection.ReturnValue;
            SQLDBUtil.ExecuteNonQuery("HMS_InsBankBranches", p);
            return Convert.ToInt32(p[5].Value);
        }
        public static DataSet GetCountry()
        {
            return SQLDBUtil.ExecuteDataset("PM_Country");
        }
        #endregion Bank
        public static DataSet G_EMP_GteEmployeePic(int empID)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@EmpID", empID);
                return SQLDBUtil.ExecuteDataset("G_EMP_GteEmployeePic", param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet DMS_GetDocClass(int ModuleID)
        {
            try
            {
                DataSet ds = SQLDBUtil.ExecuteDataset("DMS_GetDocClass", new SqlParameter[] { new SqlParameter("@ModuleID", ModuleID) });
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet PM_State(int? CountryId)
        {
            SqlParameter[] parms = new SqlParameter[1];
            parms[0] = new SqlParameter("@CountryId", CountryId);
            DataSet ds = SQLDBUtil.ExecuteDataset("PM_State", parms);
            return ds;
        }
        public static DataSet GetAllCities(int? StateId)
        {
            SqlParameter[] parms = new SqlParameter[1];
            parms[0] = new SqlParameter("@StateId", StateId);
            DataSet ds = SQLDBUtil.ExecuteDataset("G_GetAllCitiesByState", parms);
            return ds;
        }
        public static DataSet GetPFAccountDetails(int? EmpID)
        {
            SqlParameter[] parm = new SqlParameter[1];
            parm[0] = new SqlParameter("@EmpID", EmpID);
            return SQLDBUtil.ExecuteDataset("HR_GetPFAcDetails", parm);
        }
        #region NewState
        public static int InsNewState(string State, int CountryID)
        {
            SqlParameter[] parm = new SqlParameter[3];
            parm[0] = new SqlParameter("@NewState", State);
            parm[1] = new SqlParameter("@Country", CountryID);
            parm[2] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
            parm[2].Direction = ParameterDirection.ReturnValue;
            SQLDBUtil.ExecuteNonQuery("PM_Ins_State", parm);
            return Convert.ToInt32(parm[2].Value);
        }
        #endregion NewState
        #region MapWS
        public static DataSet GetEmployeeRolesByPaging(HRCommon objHrCommon, int? WorksiteID, string EmpName, int? DeptID, int CompanyID)
        {
            SqlParameter[] p = new SqlParameter[8];
            p[0] = new SqlParameter("@worksiteid", WorksiteID);
            p[1] = new SqlParameter("@EmpName", EmpName);
            p[2] = new SqlParameter("@DeptId", DeptID);
            p[3] = new SqlParameter("@CurrentPage", objHrCommon.CurrentPage);
            p[4] = new SqlParameter("@PageSize", objHrCommon.PageSize);
            p[5] = new SqlParameter("@NoofRecords", System.Data.SqlDbType.Int);
            p[5].Direction = ParameterDirection.Output;
            p[6] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
            p[6].Direction = ParameterDirection.ReturnValue;
            p[7] = new SqlParameter("@CompanyID", CompanyID);
            DataSet ds = SQLDBUtil.ExecuteDataset("HMS_GetEmployeeRolesByPaging", p);
            objHrCommon.NoofRecords = (int)p[5].Value;
            objHrCommon.TotalPages = (int)p[6].Value;
            return ds;
        }
        public static DataSet GETEMPWorksite(int EmpID)
        {
            SqlParameter[] parms = new SqlParameter[1];
            parms[0] = new SqlParameter("@EMPID", EmpID);
            DataSet ds = SQLDBUtil.ExecuteDataset("HR_GETEMPWorksite", parms);
            return ds;
        }
        public static void MAPWorksite(int EmpID, int Status, int WSID)
        {
            SqlParameter[] parms = new SqlParameter[3];
            parms[0] = new SqlParameter("@EMPID", EmpID);
            parms[1] = new SqlParameter("@STATUS", Status);
            parms[2] = new SqlParameter("@SiteID", WSID);
            SQLDBUtil.ExecuteNonQuery("HR_MAPWorksite", parms);
        }
        #endregion MapWS
        #region CountryStateCity
        public static int InsCountry(string CountryName, string Nationality)
        {
            try
            {
                SqlParameter[] objParam = new SqlParameter[3];
                objParam[0] = new SqlParameter("@CountryName", CountryName);
                objParam[1] = new SqlParameter("@Nationality", Nationality);
                objParam[2] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
                objParam[2].Direction = ParameterDirection.ReturnValue;
                SQLDBUtil.ExecuteNonQuery("HMS_InsCountry", objParam);
                return Convert.ToInt32(objParam[2].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static int InsState(int CountryID, string StateName)
        {
            try
            {
                SqlParameter[] parm = new SqlParameter[3];
                parm[0] = new SqlParameter("@CountryID", CountryID);
                parm[1] = new SqlParameter("@StateName", StateName);
                parm[2] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
                parm[2].Direction = ParameterDirection.ReturnValue;
                SQLDBUtil.ExecuteNonQuery("HMS_InsUpdState", parm);
                return Convert.ToInt32(parm[2].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static int InsCity(int StateID, string CityName)
        {
            try
            {
                SqlParameter[] prm = new SqlParameter[3];
                prm[0] = new SqlParameter("@StateID", StateID);
                prm[1] = new SqlParameter("@CityName", CityName);
                prm[2] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
                prm[2].Direction = ParameterDirection.ReturnValue;
                SQLDBUtil.ExecuteNonQuery("HMS_InsCity", prm);
                return Convert.ToInt32(prm[2].Value);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion CountryStateCity
        #region EmpPreTDS
        public static DataSet GetEmpPreTDS(HRCommon objHrCommon, int? SiteID, int? DeptID, int? EmpID, int CompanyID)
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
                sqlParams[6] = new SqlParameter("@EmpName", objHrCommon.FName);
                sqlParams[7] = new SqlParameter("@OldEmpID", objHrCommon.OldEmpID);
                sqlParams[8] = new SqlParameter("@EmpID", EmpID);
                sqlParams[9] = new SqlParameter("@CompanyID", CompanyID);
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_EmpListForTDS", sqlParams);
                objHrCommon.NoofRecords = (int)sqlParams[3].Value;
                objHrCommon.TotalPages = (int)sqlParams[2].Value;
                return ds;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static void InsUpdateEmpPreTDS(int EmpID, double TDSAomunt, string ext, int UserID, int FinYearID)
        {
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[5];
                sqlParams[0] = new SqlParameter("@EmpID", EmpID);
                sqlParams[1] = new SqlParameter("@TDS", TDSAomunt);
                sqlParams[2] = new SqlParameter("@Ext", ext);
                sqlParams[3] = new SqlParameter("@UserID", UserID);
                sqlParams[4] = new SqlParameter("@FinYearID", FinYearID);
                SQLDBUtil.ExecuteNonQuery("HR_insUpdEmpPrevTDSDetails", sqlParams);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        #endregion EmpPreTDS
        public SqlDataReader drHR_GetMobilesBills(HRCommon objHrCommon)
        {
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[9];
                sqlParams[0] = new SqlParameter("@CurrentPage", objHrCommon.CurrentPage);
                sqlParams[1] = new SqlParameter("@PageSize", objHrCommon.PageSize);
                sqlParams[2] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
                sqlParams[2].Direction = ParameterDirection.ReturnValue;
                sqlParams[3] = new SqlParameter("@NoofRecords", System.Data.SqlDbType.Int);
                sqlParams[3].Direction = ParameterDirection.Output;
                sqlParams[4] = new SqlParameter("@SiteID", objHrCommon.SiteID);
                sqlParams[5] = new SqlParameter("@DeptID", objHrCommon.DeptID);
                sqlParams[6] = new SqlParameter("@EmpName", objHrCommon.FName);
                sqlParams[7] = new SqlParameter("@Month", objHrCommon.Month);
                sqlParams[8] = new SqlParameter("@year", objHrCommon.Year);
                SqlDataReader dr = null;
                dr = SQLDBUtil.ExecuteDataReader("HR_GetMobilesBills", sqlParams);
                return dr;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        #region Mess
        public static DataSet GetTypeOfMessCofigs(int Status)
        {
            SqlParameter[] parm = new SqlParameter[1];
            parm[0] = new SqlParameter("@Status", Status);
            return SQLDBUtil.ExecuteDataset("HR_GetMessTypeDetails", parm);
        }
        public static DataSet GetTypeofMessDetails(int MID)
        {
            try
            {
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetMessTypeDetailsByMessID", new SqlParameter[] { new SqlParameter("@MID", MID) });
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static int InsUpdateTypeofMess(int? MID, string Name, string Sname, int Status, int UserId)
        {
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[6];
                sqlParams[0] = new SqlParameter("@MID", MID);
                sqlParams[1] = new SqlParameter("@Name", Name);
                sqlParams[2] = new SqlParameter("@ShortName", Sname);
                sqlParams[3] = new SqlParameter("@Status", Status);
                sqlParams[4] = new SqlParameter("@UserID", UserId);
                sqlParams[5] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
                sqlParams[5].Direction = ParameterDirection.ReturnValue;
                SQLDBUtil.ExecuteNonQuery("HR_InsUpdTypeOfMess", sqlParams);
                return Convert.ToInt32(sqlParams[5].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static int InsUpdateListofMessConfig(int? MCID, int MID, int WS, int EmpNatureID, DateTime FrmDate, DateTime Todate, double Amount, int Status, int UserID)
        {
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[10];
                sqlParams[0] = new SqlParameter("@MCID", MCID);
                sqlParams[1] = new SqlParameter("@MID", MID);
                sqlParams[2] = new SqlParameter("@WSID", WS);
                sqlParams[3] = new SqlParameter("@EmpNatureID", EmpNatureID);
                sqlParams[4] = new SqlParameter("@FromDate", FrmDate);
                sqlParams[5] = new SqlParameter("@ToDate", Todate);
                sqlParams[6] = new SqlParameter("@Amount", Amount);
                sqlParams[7] = new SqlParameter("@Status", Status);
                sqlParams[8] = new SqlParameter("@UserID", UserID);
                sqlParams[9] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
                sqlParams[9].Direction = ParameterDirection.ReturnValue;
                SQLDBUtil.ExecuteNonQuery("HR_InsUpdListofMessSetUp", sqlParams);
                return Convert.ToInt32(sqlParams[9].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet GetMessTypes(HRCommon objHrCommon, int? WS, int? MessTpe, int? Month, int? Year, int? Status, int? EmpNatID)
        {
            SqlParameter[] sqlParams = new SqlParameter[10];
            sqlParams[0] = new SqlParameter("@CurrentPage", objHrCommon.CurrentPage);
            sqlParams[1] = new SqlParameter("@PageSize", objHrCommon.PageSize);
            sqlParams[2] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
            sqlParams[2].Direction = ParameterDirection.ReturnValue;
            sqlParams[3] = new SqlParameter("@NoofRecords", System.Data.SqlDbType.Int);
            sqlParams[3].Direction = ParameterDirection.Output;
            sqlParams[4] = new SqlParameter("@WSID", WS);
            sqlParams[5] = new SqlParameter("@MessType", MessTpe);
            sqlParams[6] = new SqlParameter("@Month", Month);
            sqlParams[7] = new SqlParameter("@Year", Year);
            sqlParams[8] = new SqlParameter("@Status", Status);
            sqlParams[9] = new SqlParameter("@EmpNatID", EmpNatID);
            DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetEmpMessTypes", sqlParams);
            objHrCommon.NoofRecords = (int)sqlParams[3].Value;
            objHrCommon.TotalPages = (int)sqlParams[2].Value;
            return ds;
        }
        public static DataSet GetSetupMesstypeDetailsByMCID(int MCID)
        {
            SqlParameter[] parm = new SqlParameter[1];
            parm[0] = new SqlParameter("@MCID", MCID);
            return SQLDBUtil.ExecuteDataset("HR_GetSetupMessTypeDetailsByMessID", parm);
        }
        public static void ChangeStatusMesstypeDetails(int MCID, int Status)
        {
            SqlParameter[] parm = new SqlParameter[2];
            parm[0] = new SqlParameter("@MCID", MCID);
            parm[1] = new SqlParameter("@Status", Status);
            SQLDBUtil.ExecuteNonQuery("HR_ChagngeStatusOfMessTypeByID", parm);
        }
        public static DataSet GetEmpMessDetails(HRCommon objHrCommon, int? WS, int? Dept, int? EmpNatureID, DateTime? date, int? EmpID, int CompanyID)
        {
            SqlParameter[] sqlParams = new SqlParameter[10];
            sqlParams[0] = new SqlParameter("@CurrentPage", objHrCommon.CurrentPage);
            sqlParams[1] = new SqlParameter("@PageSize", objHrCommon.PageSize);
            sqlParams[2] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
            sqlParams[2].Direction = ParameterDirection.ReturnValue;
            sqlParams[3] = new SqlParameter("@NoofRecords", System.Data.SqlDbType.Int);
            sqlParams[3].Direction = ParameterDirection.Output;
            sqlParams[4] = new SqlParameter("@WSID", WS);
            sqlParams[5] = new SqlParameter("@DeptID", Dept);
            sqlParams[6] = new SqlParameter("@EmpNatureID", EmpNatureID);
            sqlParams[7] = new SqlParameter("@Date", date);
            sqlParams[8] = new SqlParameter("@EmpID", EmpID);
            sqlParams[9] = new SqlParameter("@CompanyID", CompanyID);
            DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetEmployessForMessAttendance", sqlParams);
            objHrCommon.NoofRecords = (int)sqlParams[3].Value;
            objHrCommon.TotalPages = (int)sqlParams[2].Value;
            return ds;
        }
        public static void InsUpdEmpMessAtt(int MID, int EmpID, int Status, int WSID, int UserID, DateTime date)
        {
            SqlParameter[] parm = new SqlParameter[6];
            parm[0] = new SqlParameter("@MID", MID);
            parm[1] = new SqlParameter("@EmpID", EmpID);
            parm[2] = new SqlParameter("@Status", Status);
            parm[3] = new SqlParameter("@WorkSiteID", WSID);
            parm[4] = new SqlParameter("@UserID", UserID);
            parm[5] = new SqlParameter("@Date", date);
            SQLDBUtil.ExecuteNonQuery("HR_EmpMessAttendance", parm);
        }
        public static DataSet GetMessAttDetails(int? WS, int? Dept, int? EmpNatID, DateTime Date, int? EmployeeID)
        {
            SqlParameter[] parm = new SqlParameter[5];
            parm[0] = new SqlParameter("@WSID", WS);
            parm[1] = new SqlParameter("@DeptID", Dept);
            parm[2] = new SqlParameter("@EmpNatureID", EmpNatID);
            parm[3] = new SqlParameter("@Date", Date);
            parm[4] = new SqlParameter("@EmpID", EmployeeID);
            return SQLDBUtil.ExecuteDataset("HR_GetAttDetails", parm);
        }
        #endregion Mess
        #region EmpExp
        public int InsUpdEmpQua(string Qua, string Institute, int YOP, string Spcilization, double Percentage, int EduType, int EmpID, int EduID)
        {
            try
            {
                int result;
                SqlParameter[] sqlParams = new SqlParameter[9];
                sqlParams[0] = new SqlParameter("@Qualification", Qua);
                sqlParams[1] = new SqlParameter("@Institue", Institute);
                sqlParams[2] = new SqlParameter("@YOP", YOP);
                sqlParams[3] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
                sqlParams[3].Direction = ParameterDirection.ReturnValue;
                sqlParams[4] = new SqlParameter("@Specilization", Spcilization);
                sqlParams[5] = new SqlParameter("@Percentage", Percentage);
                sqlParams[6] = new SqlParameter("@EduType", EduType);
                sqlParams[7] = new SqlParameter("@EmpID", EmpID);
                sqlParams[8] = new SqlParameter("@EduID", EduID);
                SQLDBUtil.ExecuteNonQuery("HR_InsUpdEmpQualification", sqlParams);
                result = Convert.ToInt16(sqlParams[3].Value);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int InsUpdEmpExp(string Org, int CityID, int Type, DateTime Fromdate, DateTime Todate, string Designation, int EmpID, int ExpID, double CTC)
        {
            try
            {
                int result;
                SqlParameter[] sqlParams = new SqlParameter[10];
                sqlParams[0] = new SqlParameter("@Organization", Org);
                sqlParams[1] = new SqlParameter("@City", CityID);
                sqlParams[2] = new SqlParameter("@Type", Type);
                sqlParams[3] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
                sqlParams[3].Direction = ParameterDirection.ReturnValue;
                sqlParams[4] = new SqlParameter("@FromDate", Fromdate);
                sqlParams[5] = new SqlParameter("@ToDate", Todate);
                sqlParams[6] = new SqlParameter("@Designation", Designation);
                sqlParams[7] = new SqlParameter("@EmpID", EmpID);
                sqlParams[8] = new SqlParameter("@ExpID", ExpID);
                sqlParams[9] = new SqlParameter("@CTC", CTC);
                SQLDBUtil.ExecuteNonQuery("HR_InsUpdEmpExp", sqlParams);
                result = Convert.ToInt16(sqlParams[3].Value);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet GetEducationDetails(int? EmpID)
        {
            SqlParameter[] parm = new SqlParameter[1];
            parm[0] = new SqlParameter("@EmpID", EmpID);
            return SQLDBUtil.ExecuteDataset("HMS_GetEmpQulDetails", parm);
        }
        public static DataSet GetEmpExpDetails(int? EmpID)
        {
            SqlParameter[] parm = new SqlParameter[1];
            parm[0] = new SqlParameter("@EmpID", EmpID);
            return SQLDBUtil.ExecuteDataset("HMS_GetEmpExpDetails", parm);
        }
        public static DataSet GetEduDetailsByEduID(int EduID)
        {
            SqlParameter[] parm = new SqlParameter[1];
            parm[0] = new SqlParameter("@EduID", EduID);
            return SQLDBUtil.ExecuteDataset("HMS_GetEmpQualificationDetailsByEduID", parm);
        }
        public static DataSet GetExpDetailsByEXpID(int ExpID)
        {
            SqlParameter[] parm = new SqlParameter[1];
            parm[0] = new SqlParameter("@ExpID", ExpID);
            return SQLDBUtil.ExecuteDataset("HMS_GetEmpExpDetailsByExpID", parm);
        }
        #endregion EmpExp
        #region EmpWorkDetails
        public DataSet GetEmployeesWorkDetails(HRCommon objHrCommon, int? WSID, int? DeptID, string Name, int? Month, int Year, int Status, int? EmpNat, int CompanyID, int? Empid)
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
                sqlParams[4] = new SqlParameter("@SiteID", WSID);
                sqlParams[5] = new SqlParameter("@DeptID", DeptID);
                sqlParams[6] = new SqlParameter("@EmpName", Name);
                sqlParams[7] = new SqlParameter("@Month", Month);
                sqlParams[8] = new SqlParameter("@Year", Year);
                sqlParams[9] = new SqlParameter("@EmpStatus", Status);
                sqlParams[10] = new SqlParameter("@EmpNat", EmpNat);
                sqlParams[11] = new SqlParameter("@CompnayID", CompanyID);
                sqlParams[12] = new SqlParameter("@Empid", Empid);
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetEmpDetailsByDOJandDOT", sqlParams);
                objHrCommon.NoofRecords = (int)sqlParams[3].Value;
                objHrCommon.TotalPages = (int)sqlParams[2].Value;
                return ds;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        #endregion EmpWorkDetails
        #region Dashboard
        public static DataSet GetDashBoard()
        {
            try
            {
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetDashBoard");
                return ds;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        #endregion Dashboard
        #region ApplicantStatus
        public static int InsUpdAppStatus(int? AppStatusID, string AppStatusName, int Status, int UserID)
        {
            SqlParameter[] parm = new SqlParameter[5];
            parm[0] = new SqlParameter("@AppStatusID", AppStatusID);
            parm[1] = new SqlParameter("@AppStatusName", AppStatusName);
            parm[2] = new SqlParameter("@Status", Status);
            parm[3] = new SqlParameter("@UserID", UserID);
            parm[4] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
            parm[4].Direction = ParameterDirection.ReturnValue;
            SQLDBUtil.ExecuteNonQuery("HR_InsUpdApplicantStatus", parm);
            return Convert.ToInt16(parm[4].Value);
        }
        public static DataSet GetApplicantStatus(int Status)
        {
            SqlParameter[] parm = new SqlParameter[1];
            parm[0] = new SqlParameter("@Status", Status);
            return SQLDBUtil.ExecuteDataset("HR_GetApplicantStausDetailsByStatus", parm);
        }
        public static DataSet GetApplicantStatusByAppStaID(int AppStatusID)
        {
            SqlParameter[] parm = new SqlParameter[1];
            parm[0] = new SqlParameter("@AppStatusID", AppStatusID);
            return SQLDBUtil.ExecuteDataset("HR_GetApplicantStatusDetailsByStatusID", parm);
        }
        public static void UpdateApplicantStatus(int AppStatusID, int Status)
        {
            SqlParameter[] parm = new SqlParameter[2];
            parm[0] = new SqlParameter("@AppStatusID", AppStatusID);
            parm[1] = new SqlParameter("@Status", Status);
            SQLDBUtil.ExecuteNonQuery("HR_UpdateApplicantStatus", parm);
        }
        #endregion ApplicantStatus
        #region SimReport
        public static DataSet SimReportDataset(int? WSID, int? DeptID, int? FrmMnth, int FrmYear, int? ToMnth, int? ToYear, int? EmpID, string EmpName)
        {
            SqlParameter[] parm = new SqlParameter[8];
            parm[0] = new SqlParameter("@WSID", WSID);
            parm[1] = new SqlParameter("@DeptID", DeptID);
            parm[2] = new SqlParameter("@FrmMonth", FrmMnth);
            parm[3] = new SqlParameter("@FrmYear", FrmYear);
            parm[4] = new SqlParameter("@ToMonth", ToMnth);
            parm[5] = new SqlParameter("@ToYear", ToYear);
            parm[6] = new SqlParameter("@EmpID", EmpID);
            parm[7] = new SqlParameter("@EmpName", EmpName);
            return SQLDBUtil.ExecuteDataset("HR_EmpSimbillReport", parm);
        }
        #endregion SimReport
        public static DataSet EmpMonthLeaveCount(int EmpID, int Month, int Year)
        {
            SqlParameter[] parm = new SqlParameter[3];
            parm[0] = new SqlParameter("@EmpID", EmpID);
            parm[1] = new SqlParameter("@Month", Month);
            parm[2] = new SqlParameter("@Year", Year);
            return SQLDBUtil.ExecuteDataset("HR_LeaveTotal", parm);
        }
        public static DataSet GetEmpHisDetails(HRCommon objHrCommon, int Order, int Direction, char EmpStatus, int? EmpID)
        {
            SqlParameter[] sqlParams = new SqlParameter[8];
            sqlParams[0] = new SqlParameter("@CurrentPage", objHrCommon.CurrentPage);
            sqlParams[1] = new SqlParameter("@PageSize", objHrCommon.PageSize);
            sqlParams[2] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
            sqlParams[2].Direction = ParameterDirection.ReturnValue;
            sqlParams[3] = new SqlParameter("@NoofRecords", System.Data.SqlDbType.Int);
            sqlParams[3].Direction = ParameterDirection.Output;
            sqlParams[4] = new SqlParameter("@OrdeID", Order);
            sqlParams[5] = new SqlParameter("@Direction", Direction);
            sqlParams[6] = new SqlParameter("@EmpStatus", EmpStatus);
            sqlParams[7] = new SqlParameter("@EmpID", EmpID);
            DataSet ds = SQLDBUtil.ExecuteDataset("HR_EmpHistory", sqlParams);
            objHrCommon.NoofRecords = (int)sqlParams[3].Value;
            objHrCommon.TotalPages = (int)sqlParams[2].Value;
            return ds;
        }
        #region EmpSchedule
        public static int InsUpdEmpSchedule(int ToDoID, int EmpID, DateTime FrmDate, DateTime ToDate, int Year, int Month, int Day, int Hour, int Min,
            int Sec, string StrtTime, string EndTime, string Rply)
        {
            SqlParameter[] parm = new SqlParameter[14];
            parm[0] = new SqlParameter("@ToDoID", ToDoID);
            parm[1] = new SqlParameter("@EmpID", EmpID);
            parm[2] = new SqlParameter("@FromDate", FrmDate);
            parm[3] = new SqlParameter("@Todate", ToDate);
            parm[4] = new SqlParameter("@year", Year);
            parm[5] = new SqlParameter("@Month", Month);
            parm[6] = new SqlParameter("@Day", Day);
            parm[7] = new SqlParameter("@Hour", Hour);
            parm[8] = new SqlParameter("@Min", Min);
            parm[9] = new SqlParameter("@Sec", Sec);
            parm[10] = new SqlParameter("@StartTime", StrtTime);
            parm[11] = new SqlParameter("@EndTime", EndTime);
            parm[12] = new SqlParameter("@Rply", Rply);
            parm[13] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
            parm[13].Direction = ParameterDirection.ReturnValue;
            SQLDBUtil.ExecuteNonQuery("HR_InsUpdEmpSchedule", parm);
            return Convert.ToInt16(parm[13].Value);
        }
        public static DataSet HR_GetToDolistDetailsByToDOID(int ToDoId)
        {
            try
            {
                SqlParameter[] objParam = new SqlParameter[1];
                objParam[0] = new SqlParameter("@ToDOLstID", ToDoId);
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetToDoListDetailsByToDoID", objParam);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet GetScheduledRplyDetails(HRCommon objHrCommon, int? WSID, int? DeptID, int? EmpID, string EmpName)
        {
            SqlParameter[] sqlParams = new SqlParameter[8];
            sqlParams[0] = new SqlParameter("@CurrentPage", objHrCommon.CurrentPage);
            sqlParams[1] = new SqlParameter("@PageSize", objHrCommon.PageSize);
            sqlParams[2] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
            sqlParams[2].Direction = ParameterDirection.ReturnValue;
            sqlParams[3] = new SqlParameter("@NoofRecords", System.Data.SqlDbType.Int);
            sqlParams[3].Direction = ParameterDirection.Output;
            sqlParams[4] = new SqlParameter("@WSID", WSID);
            sqlParams[5] = new SqlParameter("@DeptID", DeptID);
            sqlParams[6] = new SqlParameter("@EmpId", EmpID);
            sqlParams[7] = new SqlParameter("@EmpName", EmpName);
            DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetScheduledRplyDetails", sqlParams);
            objHrCommon.NoofRecords = (int)sqlParams[3].Value;
            objHrCommon.TotalPages = (int)sqlParams[2].Value;
            return ds;
        }
        #endregion EmpSchedule
        public static DataSet GetLogoutDetails(int EmpID, string HostIP)
        {
            try
            {
                SqlParameter[] objParam = new SqlParameter[2];
                objParam[0] = new SqlParameter("@EmpID", EmpID);
                objParam[1] = new SqlParameter("@HostIP", HostIP);
                DataSet ds = SQLDBUtil.ExecuteDataset("CP_LogOut", objParam);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet GetEmployeesWhohaveSystems(HRCommon objHrCommon)
        {
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[6];
                sqlParams[0] = new SqlParameter("@CurrentPage", objHrCommon.CurrentPage);
                sqlParams[1] = new SqlParameter("@PageSize", objHrCommon.PageSize);
                sqlParams[2] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
                sqlParams[2].Direction = ParameterDirection.ReturnValue;
                sqlParams[3] = new SqlParameter("@NoofRecords", System.Data.SqlDbType.Int);
                sqlParams[3].Direction = ParameterDirection.Output;
                sqlParams[4] = new SqlParameter("@SiteID", objHrCommon.SiteID);
                sqlParams[5] = new SqlParameter("@DeptID", objHrCommon.DeptID);
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetEmployeesWhohaveSystems", sqlParams);
                objHrCommon.NoofRecords = (int)sqlParams[3].Value;
                objHrCommon.TotalPages = (int)sqlParams[2].Value;
                return ds;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public DataSet GetLTAConfigList(HRCommon objHrCommon, int? CompID)
        {
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[8];
                sqlParams[0] = new SqlParameter("@CurrentPage", objHrCommon.CurrentPage);
                sqlParams[1] = new SqlParameter("@PageSize", objHrCommon.PageSize);
                sqlParams[2] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
                sqlParams[2].Direction = ParameterDirection.ReturnValue;
                sqlParams[3] = new SqlParameter("@NoofRecords", System.Data.SqlDbType.Int);
                sqlParams[3].Direction = ParameterDirection.Output;
                sqlParams[4] = new SqlParameter("@SiteID", objHrCommon.SiteID);
                sqlParams[5] = new SqlParameter("@DeptID", objHrCommon.DeptID);
                sqlParams[6] = new SqlParameter("@CTCCompID", CompID);
                sqlParams[7] = new SqlParameter("@Empid", objHrCommon.EmpID);
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetLTAConfigList_By_Empid", sqlParams);
                objHrCommon.NoofRecords = (int)sqlParams[3].Value;
                objHrCommon.TotalPages = (int)sqlParams[2].Value;
                return ds;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static DataSet GetCompany(String SearchKey)
        {
            try
            {
                SqlParameter[] par = new SqlParameter[1];
                par[0] = new SqlParameter("@Search", SearchKey);
                return SQLDBUtil.ExecuteDataset("GEN_Service_SearchCompany", par);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet GEN_SearchCompany(String SearchKey)
        {
            try
            {
                SqlParameter[] par = new SqlParameter[1];
                par[0] = new SqlParameter("@Search", SearchKey);
                return SQLDBUtil.ExecuteDataset("GEN_SearchCompany", par);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet HR_GetCompanyList()
        {
            DataSet ds = SQLDBUtil.ExecuteDataset("Get_Companies");
            return ds;
        }
        public static int InsertCallerDiary(int DiaryID, string Caller, string Company, int Seeking, string Message)
        {
            try
            {
                SqlParameter[] objParam = new SqlParameter[6];
                objParam[0] = new SqlParameter("@DiaryID", DiaryID);
                objParam[1] = new SqlParameter("@Caller", Caller);
                objParam[2] = new SqlParameter("@Company", Company);
                objParam[3] = new SqlParameter("@Seeking", Seeking);
                objParam[4] = new SqlParameter("@Message", Message);
                objParam[5] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
                objParam[5].Direction = ParameterDirection.ReturnValue;
                SQLDBUtil.ExecuteNonQuery("HR_INSUPD_CallDiary", objParam);
                return (int)objParam[5].Value;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet GetEmpCallDiary(HRCommon objHrCommon, int? EmpID, int CompanyID, DateTime FromDate, DateTime ToDate, string Caller, string Company)
        {
            SqlParameter[] objParam = new SqlParameter[10];
            objParam[0] = new SqlParameter("@CurrentPage", objHrCommon.CurrentPage);
            objParam[1] = new SqlParameter("@PageSize", objHrCommon.PageSize);
            objParam[2] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
            objParam[2].Direction = ParameterDirection.ReturnValue;
            objParam[3] = new SqlParameter("@NoofRecords", System.Data.SqlDbType.Int);
            objParam[3].Direction = ParameterDirection.Output;
            objParam[4] = new SqlParameter("@EmpID", EmpID);
            objParam[5] = new SqlParameter("@CompanyID", CompanyID);
            objParam[6] = new SqlParameter("@FromDate", FromDate);
            objParam[7] = new SqlParameter("@ToDate", ToDate);
            if (Caller != "")
            {
                objParam[8] = new SqlParameter("@Caller", Caller);
            }
            else
            {
                objParam[8] = new SqlParameter("@Caller", System.Data.SqlDbType.VarChar);
            }
            if (Company != "")
            {
                objParam[9] = new SqlParameter("@Company", Company);
            }
            else
            {
                objParam[9] = new SqlParameter("@Company", System.Data.SqlDbType.VarChar);
            }
            DataSet ds = SQLDBUtil.ExecuteDataset("HR_EmpCallDiary", objParam);
            objHrCommon.NoofRecords = (int)objParam[3].Value;
            objHrCommon.TotalPages = (int)objParam[2].Value;
            return ds;
        }
        public static DataSet GetEmpCallDiaryDets(int DiaryID)
        {
            SqlParameter[] objParam = new SqlParameter[1];
            objParam[0] = new SqlParameter("@DiaryID", DiaryID);
            DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetEmpCallDiaryDets", objParam);
            return ds;
        }
        public static int InsertVisitorLog(int VlogID, DateTime LogDate, string TimeIN, string VisitorName, string CompanyName,
                                           Int64? MobileNum, string Purpose, string Designation, string TimeOut, string Remarks, int UserID)
        {
            try
            {
                SqlParameter[] objParam = new SqlParameter[12];
                objParam[0] = new SqlParameter("@VlogID", VlogID);
                objParam[1] = new SqlParameter("@LogDate", LogDate);
                objParam[2] = new SqlParameter("@TimeIN", TimeIN);
                objParam[3] = new SqlParameter("@VisitorName", VisitorName);
                objParam[4] = new SqlParameter("@CompanyName", CompanyName);
                objParam[5] = new SqlParameter("@MobileNum", MobileNum);
                objParam[6] = new SqlParameter("@Purpose", Purpose);
                objParam[7] = new SqlParameter("@Designation", Designation);
                objParam[8] = new SqlParameter("@TimeOut", TimeOut);
                objParam[9] = new SqlParameter("@Remarks", Remarks);
                objParam[10] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
                objParam[10].Direction = ParameterDirection.ReturnValue;
                objParam[11] = new SqlParameter("@UserID", UserID);
                SQLDBUtil.ExecuteNonQuery("HR_INSUPD_VisitorLog", objParam);
                return (int)objParam[10].Value;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet GetEmpVisitorLog(HRCommon objHrCommon, DateTime FromDate, DateTime ToDate, string Caller, string Company)
        {
            SqlParameter[] objParam = new SqlParameter[8];
            objParam[0] = new SqlParameter("@CurrentPage", objHrCommon.CurrentPage);
            objParam[1] = new SqlParameter("@PageSize", objHrCommon.PageSize);
            objParam[2] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
            objParam[2].Direction = ParameterDirection.ReturnValue;
            objParam[3] = new SqlParameter("@NoofRecords", System.Data.SqlDbType.Int);
            objParam[3].Direction = ParameterDirection.Output;
            objParam[4] = new SqlParameter("@FromDate", FromDate);
            objParam[5] = new SqlParameter("@ToDate", ToDate);
            if (Caller != "")
            {
                objParam[6] = new SqlParameter("@Caller", Caller);
            }
            else
            {
                objParam[6] = new SqlParameter("@Caller", System.Data.SqlDbType.VarChar);
            }
            if (Company != "")
            {
                objParam[7] = new SqlParameter("@Company", Company);
            }
            else
            {
                objParam[7] = new SqlParameter("@Company", System.Data.SqlDbType.VarChar);
            }
            DataSet ds = SQLDBUtil.ExecuteDataset("HR_EmpVisitorLog", objParam);
            objHrCommon.NoofRecords = (int)objParam[3].Value;
            objHrCommon.TotalPages = (int)objParam[2].Value;
            return ds;
        }
        public static DataSet GetEmpVisitorLogDets(int VlogID)
        {
            SqlParameter[] objParam = new SqlParameter[1];
            objParam[0] = new SqlParameter("@VlogID", VlogID);
            DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetEmpVisitorLogDets", objParam);
            return ds;
        }
        public static int InsertEmpLog(int ElogID, DateTime LogDate, string TimeIN, int EmpID, string CompanyName,
                                          string Purpose, string TimeOut, string Remarks, int UserID, int WsID)
        {
            try
            {
                SqlParameter[] objParam = new SqlParameter[11];
                objParam[0] = new SqlParameter("@ElogID", ElogID);
                objParam[1] = new SqlParameter("@LogDate", LogDate);
                objParam[2] = new SqlParameter("@TimeIN", TimeIN);
                objParam[3] = new SqlParameter("@EmpID", EmpID);
                objParam[4] = new SqlParameter("@CompanyName", CompanyName);
                objParam[5] = new SqlParameter("@Purpose", Purpose);
                objParam[6] = new SqlParameter("@TimeOut", TimeOut);
                objParam[7] = new SqlParameter("@Remarks", Remarks);
                objParam[8] = new SqlParameter("@WsID", WsID);
                objParam[9] = new SqlParameter("", System.Data.SqlDbType.Int);
                objParam[9].Direction = ParameterDirection.ReturnValue;
                objParam[10] = new SqlParameter("@UserID", UserID);
                SQLDBUtil.ExecuteNonQuery("HR_INSUPD_EmpLog", objParam);
                return (int)objParam[9].Value;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet GetEmpLog(HRCommon objHrCommon, DateTime FromDate, DateTime ToDate, int? EmpID, int? WsID)
        {
            SqlParameter[] objParam = new SqlParameter[8];
            objParam[0] = new SqlParameter("@CurrentPage", objHrCommon.CurrentPage);
            objParam[1] = new SqlParameter("@PageSize", objHrCommon.PageSize);
            objParam[2] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
            objParam[2].Direction = ParameterDirection.ReturnValue;
            objParam[3] = new SqlParameter("@NoofRecords", System.Data.SqlDbType.Int);
            objParam[3].Direction = ParameterDirection.Output;
            objParam[4] = new SqlParameter("@FromDate", FromDate);
            objParam[5] = new SqlParameter("@ToDate", ToDate);
            objParam[6] = new SqlParameter("@WsID", WsID);
            objParam[7] = new SqlParameter("@EmpID", EmpID);
            DataSet ds = SQLDBUtil.ExecuteDataset("HR_EmpLog", objParam);
            objHrCommon.NoofRecords = (int)objParam[3].Value;
            objHrCommon.TotalPages = (int)objParam[2].Value;
            return ds;
        }
        public static DataSet GetEmpLogDets(int ElogID)
        {
            SqlParameter[] objParam = new SqlParameter[1];
            objParam[0] = new SqlParameter("@ElogID", ElogID);
            DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetEmpLogDets", objParam);
            return ds;
        }
        public static void UpdateEmpLog(int ElogID, string TimeOut,
                                          string Remarks)
        {
            try
            {
                SqlParameter[] objParam = new SqlParameter[3];
                objParam[0] = new SqlParameter("@ElogID", ElogID);
                objParam[1] = new SqlParameter("@TimeOut", TimeOut);
                objParam[2] = new SqlParameter("@Remarks", Remarks);
                SQLDBUtil.ExecuteNonQuery("HR_UPD_EmpLog", objParam);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static void UpdateVisLog(int VlogID, string TimeOut,
                                         string Remarks)
        {
            try
            {
                SqlParameter[] objParam = new SqlParameter[3];
                objParam[0] = new SqlParameter("@VlogID", VlogID);
                objParam[1] = new SqlParameter("@TimeOut", TimeOut);
                objParam[2] = new SqlParameter("@Remarks", Remarks);
                SQLDBUtil.ExecuteNonQuery("HR_UPD_VisitorLog", objParam);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet GetSearchCompany(String SearchKey)
        {
            try
            {
                SqlParameter[] par = new SqlParameter[1];
                par[0] = new SqlParameter("@Search", SearchKey);
                return SQLDBUtil.ExecuteDataset("HR_Service_SearchCompany", par);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet GetSearchVisitor(String SearchKey)
        {
            try
            {
                SqlParameter[] par = new SqlParameter[1];
                par[0] = new SqlParameter("@Search", SearchKey);
                return SQLDBUtil.ExecuteDataset("HR_Service_SearchVisitor", par);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static void InsUpdEmpOTConfig(int EmpID, int Status, int UserID, decimal OTHrs)
        {
            SqlParameter[] parm = new SqlParameter[4];
            parm[0] = new SqlParameter("@EmpID", EmpID);
            parm[1] = new SqlParameter("@Status", Status);
            parm[2] = new SqlParameter("@UserID", UserID);
            parm[3] = new SqlParameter("@OTHrs", OTHrs);
            SQLDBUtil.ExecuteNonQuery("HR_EmpOTConfig", parm);
        }
        public static DataSet GetEmployessForOT(HRCommon objHrCommon, int? WS, int? Dept, int? EmpNatureID, int? EmpID, int CompanyID, int? DesigID)
        {
            SqlParameter[] sqlParams = new SqlParameter[10];
            sqlParams[0] = new SqlParameter("@CurrentPage", objHrCommon.CurrentPage);
            sqlParams[1] = new SqlParameter("@PageSize", objHrCommon.PageSize);
            sqlParams[2] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
            sqlParams[2].Direction = ParameterDirection.ReturnValue;
            sqlParams[3] = new SqlParameter("@NoofRecords", System.Data.SqlDbType.Int);
            sqlParams[3].Direction = ParameterDirection.Output;
            sqlParams[4] = new SqlParameter("@WSID", WS);
            sqlParams[5] = new SqlParameter("@DeptID", Dept);
            sqlParams[6] = new SqlParameter("@EmpNatureID", EmpNatureID);
            sqlParams[7] = new SqlParameter("@EmpID", EmpID);
            sqlParams[8] = new SqlParameter("@CompanyID", CompanyID);
            sqlParams[9] = new SqlParameter("@DesigID", DesigID);
            DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetEmployessForOT", sqlParams);
            objHrCommon.NoofRecords = (int)sqlParams[3].Value;
            objHrCommon.TotalPages = (int)sqlParams[2].Value;
            return ds;
        }
        public static void InsUpdEmpRamzanConfig(int EmpID, int Status, int UserID, int SpDayID)
        {
            SqlParameter[] parm = new SqlParameter[4];
            parm[0] = new SqlParameter("@EmpID", EmpID);
            parm[1] = new SqlParameter("@Status", Status);
            parm[2] = new SqlParameter("@UserID", UserID);
            parm[3] = new SqlParameter("@SpDayID", SpDayID);
            SQLDBUtil.ExecuteNonQuery("HR_EmpRamzanConfig", parm);
        }
        public static DataSet GetEmployessForRamzan(HRCommon objHrCommon, int? WS, int? Dept, int? EmpNatureID, int? EmpID, int CompanyID, int? DesigID, int SpDayID)
        {
            SqlParameter[] sqlParams = new SqlParameter[11];
            sqlParams[0] = new SqlParameter("@CurrentPage", objHrCommon.CurrentPage);
            sqlParams[1] = new SqlParameter("@PageSize", objHrCommon.PageSize);
            sqlParams[2] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
            sqlParams[2].Direction = ParameterDirection.ReturnValue;
            sqlParams[3] = new SqlParameter("@NoofRecords", System.Data.SqlDbType.Int);
            sqlParams[3].Direction = ParameterDirection.Output;
            sqlParams[4] = new SqlParameter("@WSID", WS);
            sqlParams[5] = new SqlParameter("@DeptID", Dept);
            sqlParams[6] = new SqlParameter("@EmpNatureID", EmpNatureID);
            sqlParams[7] = new SqlParameter("@EmpID", EmpID);
            sqlParams[8] = new SqlParameter("@CompanyID", CompanyID);
            sqlParams[9] = new SqlParameter("@DesigID", DesigID);
            sqlParams[10] = new SqlParameter("@SpDayID", SpDayID);
            DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetEmployessForRamzan", sqlParams);
            objHrCommon.NoofRecords = (int)sqlParams[3].Value;
            objHrCommon.TotalPages = (int)sqlParams[2].Value;
            return ds;
        }
        public static int InsUpdateRamzan(int EmpSalID, decimal WorkingHrs, DateTime FromDate, DateTime ToDate, int UserID, string SpDayName)
        {
            try
            {
                int retval;
                SqlParameter[] sqlParams = new SqlParameter[7];
                sqlParams[0] = new SqlParameter("@RID", EmpSalID);
                sqlParams[1] = new SqlParameter("@WorkingHrs", WorkingHrs);
                sqlParams[2] = new SqlParameter("@FromDate", FromDate);
                sqlParams[3] = new SqlParameter("@ToDate", ToDate);
                sqlParams[4] = new SqlParameter("@UserID", UserID);
                sqlParams[5] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
                sqlParams[5].Direction = ParameterDirection.ReturnValue;
                sqlParams[6] = new SqlParameter("@SpDayName", SpDayName);
                SQLDBUtil.ExecuteNonQuery("HR_InsUpd_RamzanDate", sqlParams);
                retval = (int)sqlParams[5].Value;
                return retval;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static DataSet GetRamzanDates(int? RID)
        {
            SqlParameter[] sqlParams = new SqlParameter[1];
            sqlParams[0] = new SqlParameter("@RID", RID);
            DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetRamzanDates", sqlParams);
            return ds;
        }
        public static DataSet HR_GetRamzanDates_ddl(int? RID)
        {
            SqlParameter[] sqlParams = new SqlParameter[1];
            sqlParams[0] = new SqlParameter("@RID", RID);
            DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetRamzanDates_ddl", sqlParams);
            return ds;
        }
        public DataSet GetTodayAttendanceMultiple(int? DeptID, int? WSID, DateTime Date, int? DesigID, int? EmpID, string Empname, HRCommon objHrCommon)
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
                sqlParams[4] = new SqlParameter("@DeptID", DeptID);
                sqlParams[5] = new SqlParameter("@WSID", WSID);
                sqlParams[6] = new SqlParameter("@Date", Date);
                sqlParams[7] = new SqlParameter("@DesigID", DesigID);
                sqlParams[8] = new SqlParameter("@EmpID", EmpID);
                sqlParams[9] = new SqlParameter("@Empname", Empname);
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetTodayAttendanceMultiple", sqlParams);
                objHrCommon.NoofRecords = (int)sqlParams[3].Value;
                objHrCommon.TotalPages = (int)sqlParams[2].Value;
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static int InsUpdateMultipleAtt(int EmpID, string Intime, string Outtime, string Remarks, DateTime Date, int UserID, int MID)
        {
            try
            {
                int retval;
                SqlParameter[] sqlParams = new SqlParameter[8];
                sqlParams[0] = new SqlParameter("@EmpID", EmpID);
                sqlParams[1] = new SqlParameter("@Intime", Intime);
                sqlParams[2] = new SqlParameter("@Outtime", Outtime);
                sqlParams[3] = new SqlParameter("@Date", Date);
                sqlParams[4] = new SqlParameter("@UserID", UserID);
                sqlParams[5] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
                sqlParams[5].Direction = ParameterDirection.ReturnValue;
                sqlParams[6] = new SqlParameter("@Remarks", Remarks);
                sqlParams[7] = new SqlParameter("@MID", MID);
                SQLDBUtil.ExecuteNonQuery("HR_InsUpdMultipleAttendance", sqlParams);
                retval = (int)sqlParams[5].Value;
                return retval;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static int InsUpdateMultipleAtt_MultiDay(int EmpID, string Intime, string Outtime, string Remarks, DateTime Date, int UserID, int MID)
        {
            try
            {
                int retval;
                SqlParameter[] sqlParams = new SqlParameter[8];
                sqlParams[0] = new SqlParameter("@EmpID", EmpID);
                sqlParams[1] = new SqlParameter("@Intime", Intime);
                sqlParams[2] = new SqlParameter("@Outtime", Outtime);
                sqlParams[3] = new SqlParameter("@Date", Date);
                sqlParams[4] = new SqlParameter("@UserID", UserID);
                sqlParams[5] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
                sqlParams[5].Direction = ParameterDirection.ReturnValue;
                sqlParams[6] = new SqlParameter("@Remarks", Remarks);
                sqlParams[7] = new SqlParameter("@MID", MID);
                SQLDBUtil.ExecuteNonQuery("HR_InsUpdMultipleAttendance_MultiDay", sqlParams);
                retval = (int)sqlParams[5].Value;
                return retval;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static int UpdateMultipleAtt(int EmpID, string Intime, string Outtime, string Remarks, int UserID, int MID, DateTime? Date)
        {
            try
            {
                int retval;
                SqlParameter[] sqlParams = new SqlParameter[8];
                sqlParams[0] = new SqlParameter("@EmpID", EmpID);
                sqlParams[1] = new SqlParameter("@Intime", Intime);
                sqlParams[2] = new SqlParameter("@Outtime", Outtime);
                sqlParams[3] = new SqlParameter("@UserID", UserID);
                sqlParams[4] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
                sqlParams[4].Direction = ParameterDirection.ReturnValue;
                sqlParams[5] = new SqlParameter("@Remarks", Remarks);
                sqlParams[6] = new SqlParameter("@MID", MID);
                sqlParams[7] = new SqlParameter("@Date", Date);
                SQLDBUtil.ExecuteNonQuery("HR_UpdMultipleAttendance", sqlParams);
                retval = (int)sqlParams[4].Value;
                return retval;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public DataSet GetEmpOTPayList(HRCommon objHrCommon, int? WSID, int? DeptID, int? DesigID, int? Month, int? Year, int CompanyID, int? EMPID ,int status)
        {
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[12];
                sqlParams[0] = new SqlParameter("@CurrentPage", objHrCommon.CurrentPage);
                sqlParams[1] = new SqlParameter("@PageSize", objHrCommon.PageSize);
                sqlParams[2] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
                sqlParams[2].Direction = ParameterDirection.ReturnValue;
                sqlParams[3] = new SqlParameter("@NoofRecords", System.Data.SqlDbType.Int);
                sqlParams[3].Direction = ParameterDirection.Output;
                sqlParams[4] = new SqlParameter("@SiteID", WSID);
                sqlParams[5] = new SqlParameter("@DeptID", DeptID);
                sqlParams[6] = new SqlParameter("@DesigID", DesigID);
                sqlParams[7] = new SqlParameter("@Month", Month);
                sqlParams[8] = new SqlParameter("@Year", Year);
                sqlParams[9] = new SqlParameter("@CompanyID", CompanyID);
                sqlParams[10] = new SqlParameter("@EMPID", EMPID);
                sqlParams[11] = new SqlParameter("@ApproveStatus", status);
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_EmpOTPaymentList", sqlParams);
                objHrCommon.NoofRecords = (int)sqlParams[3].Value;
                objHrCommon.TotalPages = (int)sqlParams[2].Value;
                return ds;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static int InsUpdNRIDocTypes(int ID, string DocName, int UserID)
        {
            try
            {
                int retval;
                SqlParameter[] sqlParams = new SqlParameter[4];
                sqlParams[0] = new SqlParameter("@ID", ID);
                sqlParams[1] = new SqlParameter("@DocName", DocName);
                sqlParams[2] = new SqlParameter("@UserID", UserID);
                sqlParams[3] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
                sqlParams[3].Direction = ParameterDirection.ReturnValue;
                SQLDBUtil.ExecuteNonQuery("HR_InsUpdNRIDocTypes", sqlParams);
                retval = (int)sqlParams[3].Value;
                return retval;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static DataSet GetNRIDocTypes(int? ID)
        {
            SqlParameter[] sqlParams = new SqlParameter[1];
            sqlParams[0] = new SqlParameter("@ID", ID);
            DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetNRIDocTypes", sqlParams);
            return ds;
        }
        public static int InsUpdNRIDocs(int ID, int EmpID, int DocID, string DocNo, string Issuer, DateTime Expiry,
                                        string AltDocNo1, string AltDocNo2, string Description, string Ext, int UserID)
        {
            try
            {
                int retval;
                SqlParameter[] sqlParams = new SqlParameter[12];
                sqlParams[0] = new SqlParameter("@ID", ID);
                sqlParams[1] = new SqlParameter("@EmpID", EmpID);
                sqlParams[2] = new SqlParameter("@DocID", DocID);
                sqlParams[3] = new SqlParameter("@DocNo", DocNo);
                sqlParams[4] = new SqlParameter("@Issuer", Issuer);
                sqlParams[5] = new SqlParameter("@Expiry", Expiry);
                sqlParams[6] = new SqlParameter("@AltDocNo1", AltDocNo1);
                sqlParams[7] = new SqlParameter("@AltDocNo2", AltDocNo2);
                sqlParams[8] = new SqlParameter("@Description", Description);
                sqlParams[9] = new SqlParameter("@Ext", Ext);
                sqlParams[10] = new SqlParameter("@UserID", UserID);
                sqlParams[11] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
                sqlParams[11].Direction = ParameterDirection.ReturnValue;
                SQLDBUtil.ExecuteNonQuery("HR_InsUpdNRIDocs", sqlParams);
                retval = (int)sqlParams[11].Value;
                return retval;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static DataSet GetNRIDocs(int? ID)
        {
            SqlParameter[] sqlParams = new SqlParameter[1];
            sqlParams[0] = new SqlParameter("@ID", ID);
            DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetNRIDocs", sqlParams);
            return ds;
        }
        public DataSet GetEmployeesByWSDCity()
        {
            try
            {
                DataSet ds = SQLDBUtil.ExecuteDataset("CMS_Get_City");
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet GetEmployeesByWSDEptNature(int? WS, int? Dept, int? EmpNatureID)
        {
            try
            {
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetEmpByWSDEptNature", new SqlParameter[] { new SqlParameter("@WSID", WS), new SqlParameter("@DeptID", Dept), 
                                                                       new SqlParameter("@EmpNatureID", EmpNatureID) });
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet GetEmployeesByWSDEptNatureOT(int? WS, int? Dept, int? EmpNatureID)
        {
            try
            {
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetEmpByWSDEptNatureOT", new SqlParameter[] { new SqlParameter("@WSID", WS), new SqlParameter("@DeptID", Dept), 
                                                                       new SqlParameter("@EmpNatureID", EmpNatureID) });
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet GetAttMultiplelist(DateTime? Date, int? Month, int? Year, int EmpID, int ReptType)
        {
            try
            {
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetAttMultiplelist", new SqlParameter[] { new SqlParameter("@Date", Date),
                                                                         new SqlParameter("@Month", Month), 
                                                                         new SqlParameter("@Year", Year), 
                                                                         new SqlParameter("@EmpID", EmpID), 
                                                                         new SqlParameter("@ReptType", ReptType) });
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #region Taxation
        public static DataSet HR_GetSatuatoryItems()
        {
            try
            {
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetSatuatoryItems");
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet HR_GetSRNItems(int ResourceID)
        {
            try
            {
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetSRNItems", new SqlParameter[] { new SqlParameter("@ResourceID", ResourceID) });
                return ds;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static DataSet HR_GetReconsiledSRNItems()
        {
            try
            {
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetReconsiledSRNItems");
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet HR_SearchReconsiledItems(int EmpID)
        {
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[1];
                sqlParams[0] = new SqlParameter("@EmpID", EmpID);
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_SearchReconsiledItems", sqlParams);
                return ds;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static DataSet EMS_GetMechinariesOwnByResourceID(int ResourceID)
        {
            try
            {
                DataSet ds = SQLDBUtil.ExecuteDataset("EMS_GetMechinariesOwnByResourceID", new SqlParameter[] { new SqlParameter("@ResourceID", ResourceID) });
                return ds;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static int HR_InsUpReconsolisedItems(int SRNItemID, int SRNID, DateTime From, DateTime To, int CreatedBy, int EmpID
                                                     , string Numeber, string AltNumber, string IssuePlace, string Issuer, string Remarks, string Ext)
        {
            try
            {
                int retval;
                SqlParameter[] objParam = new SqlParameter[13];
                objParam[0] = new SqlParameter("@SRNItemID", SRNItemID);
                objParam[1] = new SqlParameter("@SRNID", SRNID);
                objParam[2] = new SqlParameter("@From", From);
                objParam[3] = new SqlParameter("@To", To);
                objParam[4] = new SqlParameter("@CreatedBy", CreatedBy);
                objParam[5] = new SqlParameter("@EmpID", EmpID);
                objParam[6] = new SqlParameter("@Numeber", Numeber);
                objParam[7] = new SqlParameter("@AltNumber", AltNumber);
                objParam[8] = new SqlParameter("@IssuePlace", IssuePlace);
                objParam[9] = new SqlParameter("@Issuer", Issuer);
                objParam[10] = new SqlParameter("@Remarks", Remarks);
                objParam[11] = new SqlParameter("@Ext", Ext);
                objParam[12] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
                objParam[12].Direction = ParameterDirection.ReturnValue;
                SQLDBUtil.ExecuteNonQuery("HR_InsUpReconsolisedItems", objParam);
                retval = (int)objParam[12].Value;
                return retval;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static int HR_InsUpReconsolisedItemsBy_ID(int ID, int SRNItemID, int SRNID, string From, string To, int CreatedBy, int EmpID
                                                  , string Numeber, string AltNumber, int IssuePlace, string Issuer, string Remarks, string Ext, int chkhji)
        {
            try
            {
                int retval;
                SqlParameter[] objParam = new SqlParameter[15];
                objParam[13] = new SqlParameter("@ID", ID);
                objParam[0] = new SqlParameter("@SRNItemID", SRNItemID);
                objParam[1] = new SqlParameter("@SRNID", SRNID);
                if (From == string.Empty)
                {
                    objParam[2] = new SqlParameter("@SFrom", DBNull.Value);
                }
                else
                { objParam[2] = new SqlParameter("@SFrom", From); }
                if (To == string.Empty)
                {
                    objParam[3] = new SqlParameter("@STo", DBNull.Value);
                }
                else
                {
                    objParam[3] = new SqlParameter("@STo", To);
                }
                objParam[4] = new SqlParameter("@CreatedBy", CreatedBy);
                objParam[5] = new SqlParameter("@EmpID", EmpID);
                objParam[6] = new SqlParameter("@Numeber", Numeber);
                objParam[7] = new SqlParameter("@AltNumber", AltNumber);
                objParam[8] = new SqlParameter("@IssuePlace", IssuePlace);
                objParam[9] = new SqlParameter("@Issuer", Issuer);
                objParam[10] = new SqlParameter("@Remarks", Remarks);
                objParam[11] = new SqlParameter("@Ext", Ext);
                objParam[12] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
                objParam[12].Direction = ParameterDirection.ReturnValue;
                objParam[14] = new SqlParameter("@chkhji", chkhji);
                SQLDBUtil.ExecuteNonQuery("HR_InsUpReconsolisedItems_RT_14_04_2016", objParam);
                retval = (int)objParam[12].Value;
                return retval;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet HR_SRNIDbySRNItemID(int SRNItemID, int hijri)
        {
            try
            {
                SqlParameter[] objParam = new SqlParameter[2];
                objParam[0] = new SqlParameter("@SRNItemID", SRNItemID);
                objParam[1] = new SqlParameter("@Hijri", hijri);
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_SRNIDbySRNItemID", objParam);
                return ds;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static int HR_InsUpClearenceEMP(HRCommon objCommon, int id, int clrearingemployeeid, string Remarks, int ClearID, bool isapproved, int empid, int cidmast)
        {
            try
            {
                SqlParameter[] objParam = new SqlParameter[11];
                objParam[0] = new SqlParameter("@ID", id);
                objParam[1] = new SqlParameter("@clrearingemployeeid", clrearingemployeeid);
                objParam[2] = new SqlParameter("@Remarks", Remarks);
                objParam[3] = new SqlParameter("@ClearID", ClearID);
                objParam[4] = new SqlParameter("@isapproved", isapproved);
                objParam[5] = new SqlParameter("@empid", empid);
                objParam[6] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
                objParam[6].Direction = ParameterDirection.ReturnValue;
                if (cidmast == 0)
                    objParam[7] = new SqlParameter("@CIDMast", DBNull.Value);
                else
                    objParam[7] = new SqlParameter("@CIDMast", cidmast);
                objParam[8] = new SqlParameter("@CurrentPage", objCommon.CurrentPage);
                objParam[9] = new SqlParameter("@PageSize", objCommon.PageSize);
                objParam[10] = new SqlParameter("@NoofRecords", System.Data.SqlDbType.Int);
                objParam[10].Direction = ParameterDirection.Output;
                SQLDBUtil.ExecuteNonQuery("HR_InsUpClearenceEMP", objParam);
                int retval = (int)objParam[6].Value;
                return retval;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static void HR_DelReconsledItem(int SRNItemID)
        {
            try
            {
                SqlParameter[] objParam = new SqlParameter[1];
                objParam[0] = new SqlParameter("@SRNItemID", SRNItemID);
                SQLDBUtil.ExecuteNonQuery("HR_DelReconsledItem", objParam);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet HR_GetReconsiledSRNItems(HRCommon objCommon, int ResourceID, int? WsID, int? DeptID, int? DesigID, int? EmpID, string DocName, int Hijri)
        {
            try
            {
                SqlParameter[] P = new SqlParameter[11];
                P[0] = new SqlParameter("@CurrentPage", objCommon.CurrentPage);
                P[1] = new SqlParameter("@PageSize", objCommon.PageSize);
                P[2] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
                P[2].Direction = ParameterDirection.ReturnValue;
                P[3] = new SqlParameter("@NoofRecords", System.Data.SqlDbType.Int);
                P[3].Direction = ParameterDirection.Output;
                P[4] = new SqlParameter("@ResourceID", ResourceID);
                P[5] = new SqlParameter("@WsID", WsID);
                P[6] = new SqlParameter("@DeptID", DeptID);
                P[7] = new SqlParameter("@DesigID", DesigID);
                P[8] = new SqlParameter("@EmpID", EmpID);
                P[9] = new SqlParameter("@DocName", DocName);
                P[10] = new SqlParameter("@Hijri", Hijri);
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetReconsiledSRNItems", P);
                objCommon.NoofRecords = (int)P[3].Value;
                objCommon.TotalPages = (int)P[2].Value;
                return ds;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static DataSet HR_GetEmpStaturyItems(int SRNItemID)
        {
            try
            {
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetEmpStaturyItems", new SqlParameter[] { new SqlParameter("@SRNItemID", SRNItemID) });
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet SP_PM_SearchCategoriesByService()
        {
            try
            {
                DataSet ds = SQLDBUtil.ExecuteDataset("SP_PM_SearchCategoriesByService");
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static void HR_InsNewSatuatoryItems(int ResurceID)
        {
            try
            {
                SqlParameter[] objParam = new SqlParameter[1];
                objParam[0] = new SqlParameter("@ResurceID", ResurceID);
                SQLDBUtil.ExecuteNonQuery("HR_InsNewSatuatoryItems", objParam);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet PM_GroupWiseItems_IndentByService(int GroupID)
        {
            try
            {
                DataSet ds = SQLDBUtil.ExecuteDataset("PM_GroupWiseItems_IndentByService", new SqlParameter[] { new SqlParameter("@GroupId", GroupID) });
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet GetEmpByWSDEptNatureForDoc(int? WS, int? Dept, int? DesigID, int? ResourceID, int? ResolveType)
        {
            try
            {
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetEmpByWSDEptNatureForDocs", new SqlParameter[] { new SqlParameter("@WSID", WS), new SqlParameter("@DeptID", Dept), 
                                               new SqlParameter("@DesigID", DesigID),new SqlParameter("@ResourceID", ResourceID),new SqlParameter("@ResolveType", ResolveType) });
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region OtherDetails
        public static void HR_InsUpPassportDts(int EmpID, string PassportNo, string Issuer, string IssuePlace, DateTime IssueDate, DateTime ExpiryDate, string PasportRemarks, string Ext)
        {
            try
            {
                SqlParameter[] objParam = new SqlParameter[8];
                objParam[0] = new SqlParameter("@EmpID", EmpID);
                objParam[1] = new SqlParameter("@PassportNo", PassportNo);
                objParam[2] = new SqlParameter("@Issuer", Issuer);
                objParam[3] = new SqlParameter("@IssuePlace", IssuePlace);
                objParam[4] = new SqlParameter("@IssueDate", IssueDate);
                objParam[5] = new SqlParameter("@ExpiryDate", ExpiryDate);
                objParam[6] = new SqlParameter("@PasportRemarks", PasportRemarks);
                objParam[7] = new SqlParameter("@PassportExt", Ext);
                SQLDBUtil.ExecuteNonQuery("HR_InsUpdPassportOtherDts", objParam);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static void InsUpdEmergDetails(int EmpID, string EmergContact, string EmergContactName, string EmergRelation, string EmergResiPhone, DateTime? Dateofmarriage, string EmergEmail
                                                          , string OREmergContact, string OREmergContactName, string OREmergRelation, string OREmergResiPhone, string OREmergEmail, int MaritalStat)
        {
            try
            {
                SqlParameter[] objParam = new SqlParameter[13];
                objParam[0] = new SqlParameter("@EmpID", EmpID);
                objParam[1] = new SqlParameter("@EmergContact", EmergContact);
                objParam[2] = new SqlParameter("@EmergContactName", EmergContactName);
                objParam[3] = new SqlParameter("@EmergRelation", EmergRelation);
                objParam[4] = new SqlParameter("@EmergResiPhone", EmergResiPhone);
                objParam[5] = new SqlParameter("@Dateofmarriage", Dateofmarriage);
                objParam[6] = new SqlParameter("@EmergEmail", EmergEmail);
                objParam[7] = new SqlParameter("@OREmergContact", OREmergContact);
                objParam[8] = new SqlParameter("@OREmergContactName", OREmergContactName);
                objParam[9] = new SqlParameter("@OREmergRelation", OREmergRelation);
                objParam[10] = new SqlParameter("@OREmergResiPhone", OREmergResiPhone);
                objParam[11] = new SqlParameter("@MaritalStat", MaritalStat);
                objParam[12] = new SqlParameter("@OREmergEmail", OREmergEmail);
                SQLDBUtil.ExecuteNonQuery("HR_InsUpdEmergDetails", objParam);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static void HR_InsUpdInsuranceDetails(int EmpID, string PolicyNo, decimal MonthlyPremium, string CertificateNo, DateTime IssueDate, DateTime ExpiryDate, string PasportRemarks, string Ext)
        {
            try
            {
                SqlParameter[] objParam = new SqlParameter[8];
                objParam[0] = new SqlParameter("@EmpID", EmpID);
                objParam[1] = new SqlParameter("@PolicyNo", PolicyNo);
                objParam[2] = new SqlParameter("@IssueDate", IssueDate);
                objParam[3] = new SqlParameter("@ExpiryDate", ExpiryDate);
                objParam[4] = new SqlParameter("@Remarks", PasportRemarks);
                objParam[5] = new SqlParameter("@Ext", Ext);
                objParam[6] = new SqlParameter("@MonthlyPremium", MonthlyPremium);
                objParam[7] = new SqlParameter("@CertificateNo", CertificateNo);
                SQLDBUtil.ExecuteNonQuery("HR_InsUpdInsuranceDetails", objParam);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static int HR_InsUpdWPSDetails(int CompanyID, string AgentID, int ID)
        {
            try
            {
                SqlParameter[] objParam = new SqlParameter[4];
                objParam[0] = new SqlParameter("@CompanyID", CompanyID);
                objParam[1] = new SqlParameter("@AgentID", AgentID);
                objParam[2] = new SqlParameter("@ID", ID);
                objParam[3] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
                objParam[3].Direction = ParameterDirection.ReturnValue;
                SQLDBUtil.ExecuteNonQuery("HR_InsUpdWPSAgent", objParam);
                return (int)objParam[3].Value;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet GetPassportDetails(int EmpID)
        {
            try
            {
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetPassportDetails", new SqlParameter[] { new SqlParameter("@EmpID", EmpID) });
                return ds;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static int UPdateimg(int EmpID)
        {
            try
            {
                SQLDBUtil.ExecuteNonQuery("HR_Getimgupdateexecute", new SqlParameter[] { new SqlParameter("@EmpID", EmpID) });
                return 1;
            }
            catch (Exception e)
            {
                return 0;
            }
        }
        public static DataSet GetEmergDetails(int EmpID)
        {
            try
            {
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetEmergencDetails", new SqlParameter[] { new SqlParameter("@EmpID", EmpID) });
                return ds;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static DataSet GetInsuranceDetails(int EmpID)
        {
            try
            {
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetInsuranceDetails", new SqlParameter[] { new SqlParameter("@EmpID", EmpID) });
                return ds;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static DataSet GetWPSDetails(int? ID)
        {
            try
            {
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetWPSNo", new SqlParameter[] { new SqlParameter("@ID", ID) });
                return ds;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        #endregion
        public DataSet GetEmployeesSalForWPS(HRCommon objHrCommon, string Name, int? DesgID, int CompanyID, int? EmpID)
        {
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[12];
                sqlParams[0] = new SqlParameter("@CurrentPage", objHrCommon.CurrentPage);
                sqlParams[1] = new SqlParameter("@PageSize", objHrCommon.PageSize);
                sqlParams[2] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
                sqlParams[2].Direction = ParameterDirection.ReturnValue;
                sqlParams[3] = new SqlParameter("@NoofRecords", System.Data.SqlDbType.Int);
                sqlParams[3].Direction = ParameterDirection.Output;
                sqlParams[4] = new SqlParameter("@SiteID", objHrCommon.SiteID);
                sqlParams[5] = new SqlParameter("@DeptID", objHrCommon.DeptID);
                sqlParams[6] = new SqlParameter("@Month", objHrCommon.Month);
                sqlParams[7] = new SqlParameter("@Year", objHrCommon.Year);
                sqlParams[8] = new SqlParameter("@EmpName", Name);
                sqlParams[9] = new SqlParameter("@DesgID", DesgID);
                sqlParams[10] = new SqlParameter("@CompanyID", CompanyID);
                sqlParams[11] = new SqlParameter("@EmpID", EmpID);
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_EmpSalriesForWPS", sqlParams);
                objHrCommon.NoofRecords = (int)sqlParams[3].Value;
                objHrCommon.TotalPages = (int)sqlParams[2].Value;
                return ds;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public SqlDataReader GetEmpSalWPSExportToExcel(int? SiteID, int? DeptID, int? Month, int? Year, string Name, int? DesgID, int CompanyID, int? EmpID)
        {
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[8];
                sqlParams[0] = new SqlParameter("@SiteID", SiteID);
                sqlParams[1] = new SqlParameter("@DeptID", DeptID);
                sqlParams[2] = new SqlParameter("@Month", Month);
                sqlParams[3] = new SqlParameter("@Year", Year);
                sqlParams[4] = new SqlParameter("@EmpName", Name);
                sqlParams[5] = new SqlParameter("@DesgID", DesgID);
                sqlParams[6] = new SqlParameter("@CompanyID", CompanyID);
                sqlParams[7] = new SqlParameter("@EmpID", EmpID);
                SqlDataReader dr;
                dr = SQLDBUtil.ExecuteDataReader("HR_EmpSalriesForWPSExptExcel", sqlParams);
                return dr;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public DataSet GetResourceHR()
        {
            try
            {
                DataSet ds = SQLDBUtil.ExecuteDataset("GetResourceHR");
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int InsUpdTradeVsResource(int DesigID, int ResourceID, int ID)
        {
            try
            {
                SqlParameter[] objParam = new SqlParameter[4];
                objParam[0] = new SqlParameter("@DesigID", DesigID);
                objParam[1] = new SqlParameter("@ResourceID", ResourceID);
                objParam[2] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
                objParam[2].Direction = ParameterDirection.ReturnValue;
                objParam[3] = new SqlParameter("@ID", ID);
                SQLDBUtil.ExecuteNonQuery("HR_InsUpdTradeVsResource_RT_18_04_2016", objParam);
                return (int)objParam[2].Value;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet GetTradeVsResource(HRCommon objHrCommon, int? ResID, int? DesigID)
        {
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[6];
                sqlParams[0] = new SqlParameter("@CurrentPage", objHrCommon.CurrentPage);
                sqlParams[1] = new SqlParameter("@PageSize", objHrCommon.PageSize);
                sqlParams[2] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
                sqlParams[2].Direction = ParameterDirection.ReturnValue;
                sqlParams[3] = new SqlParameter("@NoofRecords", System.Data.SqlDbType.Int);
                sqlParams[3].Direction = ParameterDirection.Output;
                sqlParams[4] = new SqlParameter("@ResID", ResID);
                sqlParams[5] = new SqlParameter("@DesigID", DesigID);
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_GETTradeVsResource", sqlParams);
                objHrCommon.NoofRecords = (int)sqlParams[3].Value;
                objHrCommon.TotalPages = (int)sqlParams[2].Value;
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet GetEmployeesTotDets(int EmpID)
        {
            try
            {
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetEmployeeTotLog", new SqlParameter[] { new SqlParameter("@EmpID", EmpID) });
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet HR_GetCalenderYears()
        {
            try
            {
                DataSet ds = SQLDBUtil.ExecuteDataset("ACC_GetYear");
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet HR_NMRSalriesListByEmpID(int EmpID, int Month, int Year)
        {
            try
            {
                SqlParameter[] objParam = new SqlParameter[3];
                objParam[0] = new SqlParameter("@EmpID", EmpID);
                objParam[1] = new SqlParameter("@Month", Month);
                objParam[2] = new SqlParameter("@Year", Year);
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_NMRSalriesListByEmpID", objParam);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void Insert_Attend_Function(int empid, string dt_start, string dt_end, string dates, int chk, int chkPH)
        {
            try
            {
                SqlParameter[] parm = new SqlParameter[6];
                parm[0] = new SqlParameter("@empID", empid);
                parm[1] = new SqlParameter("@strt_dt", dt_start);
                parm[2] = new SqlParameter("@Todate", dt_end);
                parm[3] = new SqlParameter("@absentdates", dates);
                parm[4] = new SqlParameter("@IsWO", chk);
                parm[5] = new SqlParameter("@IsPH", chk);
                SQLDBUtil.ExecuteNonQuery("Insert_Attend_Function", parm);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public void JOBS_HMS_LeaveCredits(int calenderYr, DateTime dt_start)
        {
            try
            {
                SqlParameter[] parm = new SqlParameter[2];
                parm[0] = new SqlParameter("@CalenderYear", calenderYr);
                parm[1] = new SqlParameter("@Date", dt_start);
                SQLDBUtil.ExecuteNonQuery("JOBS_HMS_LeaveCreditsBYEMP_New", parm);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public void JOBS_HMS_Timesheetstatus(int calenderYr)
        {
            try
            {
                SqlParameter[] parm = new SqlParameter[1];
                parm[0] = new SqlParameter("@YearID", calenderYr);
                SQLDBUtil.ExecuteNonQuery("Insert_Attend_Function_JobManual", parm);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static DataSet HMS_countEMPatt_Status(int EmpID, int Month, int Year)
        {
            try
            {
                SqlParameter[] objParam = new SqlParameter[3];
                objParam[0] = new SqlParameter("@EmpID", EmpID);
                objParam[1] = new SqlParameter("@Month", Month);
                objParam[2] = new SqlParameter("@Year", Year);
                DataSet ds = SQLDBUtil.ExecuteDataset("HMS_countEMPatt_Status", objParam);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet GetNMRSalaries(HRCommon objHrCommon, string Name, int CompanyID, int CurrentStatus)
        {
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[11];
                sqlParams[0] = new SqlParameter("@CurrentPage", objHrCommon.CurrentPage);
                sqlParams[1] = new SqlParameter("@PageSize", objHrCommon.PageSize);
                sqlParams[2] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
                sqlParams[2].Direction = ParameterDirection.ReturnValue;
                sqlParams[3] = new SqlParameter("@NoofRecords", System.Data.SqlDbType.Int);
                sqlParams[3].Direction = ParameterDirection.Output;
                sqlParams[4] = new SqlParameter("@SiteID", objHrCommon.SiteID);
                sqlParams[5] = new SqlParameter("@DeptID", objHrCommon.DeptID);
                sqlParams[6] = new SqlParameter("@EmpStatus", CurrentStatus);
                sqlParams[7] = new SqlParameter("@Month", objHrCommon.Month);
                sqlParams[8] = new SqlParameter("@Year", objHrCommon.Year);
                sqlParams[9] = new SqlParameter("@EmpName", Name);
                sqlParams[10] = new SqlParameter("@CompanyID", CompanyID);
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_NMRSalriesList", sqlParams);
                objHrCommon.NoofRecords = (int)sqlParams[3].Value;
                objHrCommon.TotalPages = (int)sqlParams[2].Value;
                return ds;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public DataSet GetWorkSiteByEmpID_ByTransaction(int? EmpID, int CompnanyID, int? Role)
        {
            try
            {
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetWorksiteByEmpID_ByTransaction", new SqlParameter[] { new SqlParameter("@EmpID", EmpID), new SqlParameter("@CompanyID", CompnanyID), new SqlParameter("@Role", Role) });
                return ds;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public DataSet GetDepartments_ByTransaction(int DeptID)
        {
            try
            {
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetDepartments_ByTransaction", new SqlParameter[] { new SqlParameter("@DeptID", DeptID) });
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet GetDesignations_ByTransaction()
        {
            try
            {
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetDesignations_ByTransaction");
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet HR_GetSatuatoryItems_ByTransaction()
        {
            try
            {
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetSatuatoryItems_ByTransaction");
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet GetEmployeesByWSDEptNature_ByTransaction(int? WS, int? Dept, int? EmpNatureID, int? ResourceID)
        {
            try
            {
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetEmpByWSDEptNature_ByTransaction", new SqlParameter[] { new SqlParameter("@WSID", WS), new SqlParameter("@DeptID", Dept), 
                                                                       new SqlParameter("@EmpNatureID", EmpNatureID),new SqlParameter("@ResourceID", ResourceID) });
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet GetEmpid_By_Search(string prefixText)
        {
            try
            {
                SqlParameter[] sqlPrms = new SqlParameter[1];
                sqlPrms[0] = new SqlParameter("@SEARCH", prefixText);
                DataSet ds = SQLDBUtil.ExecuteDataset("GetEmpid_By_Search", sqlPrms);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet GetEmpid_By_GoogleSearch(string prefixText)
        {
            try
            {
                SqlParameter[] sqlPrms = new SqlParameter[1];
                sqlPrms[0] = new SqlParameter("@SEARCH", prefixText);
                DataSet ds = SQLDBUtil.ExecuteDataset("GetEmpid_By_GoogleSearch", sqlPrms);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet GetGoogleSearch_by_Empid(string prefixText)
        {
            try
            {
                SqlParameter[] sqlPrms = new SqlParameter[1];
                sqlPrms[0] = new SqlParameter("@SEARCH", prefixText);
                DataSet ds = SQLDBUtil.ExecuteDataset("GetGoogleSearch_by_Empid", sqlPrms);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet GetGoogleSearch_by_Empid_All(string prefixText)
        {
            try
            {
                SqlParameter[] sqlPrms = new SqlParameter[1];
                sqlPrms[0] = new SqlParameter("@SEARCH", prefixText);
                DataSet ds = SQLDBUtil.ExecuteDataset("GetGoogleSearch_by_Empid_All", sqlPrms);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet GetGoogleSearch_by_Empid_All_ByEMPID(string prefixText, int WSID)
        {
            try
            {
                SqlParameter[] sqlPrms = new SqlParameter[2];
                sqlPrms[0] = new SqlParameter("@SEARCH", prefixText);
                if (WSID == 0)
                    sqlPrms[1] = new SqlParameter("@WSID", SqlDbType.Int);
                else
                    sqlPrms[1] = new SqlParameter("@WSID", WSID);
                DataSet ds = SQLDBUtil.ExecuteDataset("GetGoogleSearch_by_Empid_All_ByEMPID", sqlPrms);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet GetGoogleSearch_by_EmpName_All(string prefixText)
        {
            try
            {
                SqlParameter[] sqlPrms = new SqlParameter[1];
                sqlPrms[0] = new SqlParameter("@SEARCH", prefixText);
                DataSet ds = SQLDBUtil.ExecuteDataset("GetGoogleSearch_by_EmpName_All", sqlPrms);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet HR_GetDepartment_googlesearch(string prefixText)
        {
            try
            {
                SqlParameter[] sqlPrms = new SqlParameter[1];
                sqlPrms[0] = new SqlParameter("@SEARCH", prefixText);
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetDepartment_googlesearch", sqlPrms);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet GetSearch_by_EmpName(string prefixText)
        {
            try
            {
                SqlParameter[] sqlPrms = new SqlParameter[1];
                sqlPrms[0] = new SqlParameter("@SEARCH", prefixText);
                DataSet ds = SQLDBUtil.ExecuteDataset("GetSearch_by_EmpName", sqlPrms);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet Get_Search_by_Empid(string prefixText)
        {
            try
            {
                SqlParameter[] sqlPrms = new SqlParameter[1];
                sqlPrms[0] = new SqlParameter("@SEARCH", prefixText);
                DataSet ds = SQLDBUtil.ExecuteDataset("Get_Search_by_Empid", sqlPrms);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet GetGoogleSearch_by_WorkSite(string prefixText)
        {
            try
            {
                SqlParameter[] sqlPrms = new SqlParameter[1];
                sqlPrms[0] = new SqlParameter("@SEARCH", prefixText);
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetWorkSite_By_EmpSalDetails_googlesearch", sqlPrms);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet GetGoogleSearch_by_WorkSite_by_Lenders(string prefixText)
        {
            try
            {
                SqlParameter[] sqlPrms = new SqlParameter[1];
                sqlPrms[0] = new SqlParameter("@SEARCH", prefixText);
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetWorkSite_By_ACC_LedgerBalances_googlesearch", sqlPrms);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet GetGoogleSearch_by_EmpName(string prefixText)
        {
            try
            {
                SqlParameter[] sqlPrms = new SqlParameter[1];
                sqlPrms[0] = new SqlParameter("@SEARCH", prefixText);
                DataSet ds = SQLDBUtil.ExecuteDataset("GetGoogleSearch_by_EmpName", sqlPrms);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet GetGoogleSearch_by_Worksite_by_position(string prefixText, int Status)
        {
            try
            {
                SqlParameter[] sqlPrms = new SqlParameter[2];
                sqlPrms[0] = new SqlParameter("@SEARCH", prefixText);
                sqlPrms[1] = new SqlParameter("@Status", Status);
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetWorkSite_By_Position_googlesearch", sqlPrms);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet GetSearchWorksite(string prefixText, bool status)
        {
            SqlParameter[] sqlParams = new SqlParameter[7];
            sqlParams[0] = new SqlParameter("@CurrentPage", 1);
            sqlParams[1] = new SqlParameter("@PageSize", 10);
            sqlParams[2] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
            sqlParams[2].Direction = ParameterDirection.ReturnValue;
            sqlParams[3] = new SqlParameter("@NoofRecords", System.Data.SqlDbType.Int);
            sqlParams[3].Direction = ParameterDirection.Output;
            sqlParams[4] = new SqlParameter("@Status", status);
            sqlParams[5] = new SqlParameter("@CompanyID", 1);
            sqlParams[6] = new SqlParameter("@SEARCH", prefixText);
            DataSet ds = SQLDBUtil.ExecuteDataset("HR_Designation_GetWorkSite", sqlParams);
            return ds;
        }
        public static DataSet GetStartDate()
        {
            try
            {
                DataSet ds = SQLDBUtil.ExecuteDataset("G_StartdateForSalariesAttendance");
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet GetEndDate()
        {
            try
            {
                DataSet ds = SQLDBUtil.ExecuteDataset("G_EnddateForSalariesAttendance");
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet HR_GetWorkSite_By_SMSEmpAttendance()
        {
            try
            {
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetWorkSite_By_SMSEmpAttendance");
                return ds;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static DataSet HMS_AbsentPenlityTranserAccXML(DataSet dsTransferDetail, string Remarks, double TotAmt, int UserId, int Month, int Year)
        {
            try
            {
                SqlParameter[] objParam = new SqlParameter[6];
                objParam[0] = new SqlParameter("@TransItems", dsTransferDetail.GetXml());
                objParam[1] = new SqlParameter("@Remarks", Remarks);
                objParam[2] = new SqlParameter("@TotAmt", TotAmt);
                objParam[3] = new SqlParameter("@UserID", UserId);
                objParam[4] = new SqlParameter("@Month", Month);
                objParam[5] = new SqlParameter("@year", Year);
                DataSet ds = SQLDBUtil.ExecuteDataset("HMS_Absent_Penalities_TranserAccXML", objParam);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static void UpdateEmployeeStatus(int EmpID, char status, int userid)
        {
            try
            {
                SqlParameter[] parm = new SqlParameter[3];
                parm[0] = new SqlParameter("@empID", EmpID);
                parm[1] = new SqlParameter("@Status", status);
                parm[2] = new SqlParameter("@UserID", userid);
                SQLDBUtil.ExecuteNonQuery("HR_UpdateEmployeeStatus", parm);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static DataSet HR_GetAttandanceDayStrength(int EMPID, int WSID, int DeptID, int EMPNAture,
          DateTime StDate, DateTime EdDate, int CurrentPage, int PageSize, ref int NoofRecords, ref int TotalPages, string EmpName)
        {
            try
            {
                SqlParameter[] objParam = new SqlParameter[11];
                objParam[0] = new SqlParameter("@EMPID", SqlDbType.Int);
                if (EMPID > 0)
                    objParam[0].Value = EMPID;
                objParam[1] = new SqlParameter("@WSID", SqlDbType.Int);
                if (WSID > 0)
                    objParam[1].Value = WSID;
                objParam[2] = new SqlParameter("@DeptID", SqlDbType.Int);
                if (DeptID > 0)
                    objParam[2].Value = DeptID;
                objParam[3] = new SqlParameter("@EMPNAture", SqlDbType.Int);
                if (EMPNAture > 0)
                    objParam[3].Value = EMPNAture;
                objParam[4] = new SqlParameter("@StDate", StDate);
                objParam[5] = new SqlParameter("@EdDate", EdDate);
                objParam[6] = new SqlParameter("@CurrentPage", CurrentPage);
                objParam[7] = new SqlParameter("@PageSize", PageSize);
                objParam[8] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
                objParam[8].Direction = ParameterDirection.ReturnValue;
                objParam[9] = new SqlParameter("@NoofRecords", System.Data.SqlDbType.Int);
                objParam[9].Direction = ParameterDirection.Output;
                objParam[10] = new SqlParameter("@Name", EmpName);
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetAttandanceDayStrength", objParam);
                NoofRecords = (int)objParam[9].Value;
                TotalPages = (int)objParam[8].Value;
                return ds;
            }
            catch (Exception ex)
            {
            }
            return null;
        }
        public static DataSet HR_GetAttandanceByPaging(int EMPID, int WSID, int DeptID, int EMPNAture,
            DateTime StDate, DateTime EdDate, int CurrentPage, int PageSize, ref int NoofRecords, ref int TotalPages, string EmpName, int Projectid)
        {
            try
            {
                SqlParameter[] objParam = new SqlParameter[12];
                objParam[0] = new SqlParameter("@EMPID", SqlDbType.Int);
                if (EMPID > 0)
                    objParam[0].Value = EMPID;
                objParam[1] = new SqlParameter("@WSID", SqlDbType.Int);
                if (WSID > 0)
                    objParam[1].Value = WSID;
                objParam[2] = new SqlParameter("@DeptID", SqlDbType.Int);
                if (DeptID > 0)
                    objParam[2].Value = DeptID;
                objParam[3] = new SqlParameter("@EMPNAture", SqlDbType.Int);
                if (EMPNAture > 0)
                    objParam[3].Value = EMPNAture;
                objParam[4] = new SqlParameter("@StDate", StDate);
                objParam[5] = new SqlParameter("@EdDate", EdDate);
                objParam[6] = new SqlParameter("@CurrentPage", CurrentPage);
                objParam[7] = new SqlParameter("@PageSize", PageSize);
                objParam[8] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
                objParam[8].Direction = ParameterDirection.ReturnValue;
                objParam[9] = new SqlParameter("@NoofRecords", System.Data.SqlDbType.Int);
                objParam[9].Direction = ParameterDirection.Output;
                objParam[10] = new SqlParameter("@Name", EmpName);
                if (Projectid != 0)
                    objParam[11] = new SqlParameter("@Projectid", Projectid);
                else
                    objParam[11] = new SqlParameter("@Projectid", System.Data.SqlDbType.Int);
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetAttandanceByPaging", objParam);
                NoofRecords = (int)objParam[9].Value;
                TotalPages = (int)objParam[8].Value;
                return ds;
            }
            catch (Exception ex)
            {
            }
            return null;
        }
        public static DataSet HR_GetAttandanceStrip(int EMPID, int WSID, int DeptID, int EMPNAture,
           DateTime StDate, DateTime EdDate, int CurrentPage, int PageSize, ref int NoofRecords, ref int TotalPages, string EmpName)
        {
            try
            {
                SqlParameter[] objParam = new SqlParameter[11];
                objParam[0] = new SqlParameter("@EMPID", SqlDbType.Int);
                if (EMPID > 0)
                    objParam[0].Value = EMPID;
                objParam[1] = new SqlParameter("@WSID", SqlDbType.Int);
                if (WSID > 0)
                    objParam[1].Value = WSID;
                objParam[2] = new SqlParameter("@DeptID", SqlDbType.Int);
                if (DeptID > 0)
                    objParam[2].Value = DeptID;
                objParam[3] = new SqlParameter("@EMPNAture", SqlDbType.Int);
                if (EMPNAture > 0)
                    objParam[3].Value = EMPNAture;
                objParam[4] = new SqlParameter("@StDate", StDate);
                objParam[5] = new SqlParameter("@EdDate", EdDate);
                objParam[6] = new SqlParameter("@CurrentPage", CurrentPage);
                objParam[7] = new SqlParameter("@PageSize", PageSize);
                objParam[8] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
                objParam[8].Direction = ParameterDirection.ReturnValue;
                objParam[9] = new SqlParameter("@NoofRecords", System.Data.SqlDbType.Int);
                objParam[9].Direction = ParameterDirection.Output;
                objParam[10] = new SqlParameter("@Name", EmpName);
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetAttandanceStrip", objParam);
                NoofRecords = (int)objParam[9].Value;
                TotalPages = (int)objParam[8].Value;
                return ds;
            }
            catch (Exception ex)
            {
            }
            return null;
        }
        public static DataSet HR_AbsentPenalitiesSearch(HRCommon objHrCommon)
        {
            SqlParameter[] sqlParams = new SqlParameter[10];
            sqlParams[0] = new SqlParameter("@worksite", objHrCommon.SiteID);
            sqlParams[1] = new SqlParameter("@designation", objHrCommon.DesigID);
            sqlParams[2] = new SqlParameter("@deptno", objHrCommon.DeptID);
            sqlParams[3] = new SqlParameter("@month", objHrCommon.Month);
            sqlParams[4] = new SqlParameter("@year", objHrCommon.Year);
            sqlParams[5] = new SqlParameter("@EmpId", objHrCommon.EmpID);
            sqlParams[6] = new SqlParameter("@CurrentPage", objHrCommon.CurrentPage);
            sqlParams[7] = new SqlParameter("@PageSize", objHrCommon.PageSize);
            sqlParams[8] = new SqlParameter("@NoofRecords", 3);
            sqlParams[8].Direction = ParameterDirection.Output;
            sqlParams[9] = new SqlParameter();
            sqlParams[9].Direction = ParameterDirection.ReturnValue;
            DataSet ds = SQLDBUtil.ExecuteDataset("USP_HMS_AbsentPenalities_search", sqlParams);
            objHrCommon.NoofRecords = (int)sqlParams[8].Value;
            objHrCommon.TotalPages = (int)sqlParams[9].Value;
            return ds;
        }
        public static DataSet HR_GetSRNItemsDetail(int? WO, int ResourceID, HRCommon objHrCommon)
        {
            SqlParameter[] sqlParams = new SqlParameter[6];
            sqlParams[0] = new SqlParameter("@ResourceID", ResourceID);
            sqlParams[1] = new SqlParameter("@CurrentPage", objHrCommon.CurrentPage);
            sqlParams[2] = new SqlParameter("@PageSize", objHrCommon.PageSize);
            sqlParams[3] = new SqlParameter("@NoofRecords", System.Data.SqlDbType.Int);
            sqlParams[3].Direction = ParameterDirection.Output;
            sqlParams[4] = new SqlParameter();
            sqlParams[4].Direction = ParameterDirection.ReturnValue;
            sqlParams[5] = new SqlParameter("@WONO", WO);
            DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetSRNItems", sqlParams);
            objHrCommon.NoofRecords = (int)sqlParams[3].Value;
            objHrCommon.TotalPages = (int)sqlParams[4].Value;
            return ds;
        }
        public static void CompleteProcessExpatDocumentation(int ID)
        {
            try
            {
                SqlParameter[] parm = new SqlParameter[1];
                parm[0] = new SqlParameter("@IDs", ID);
                SQLDBUtil.ExecuteNonQuery("USP_UpdateEmpStatutoryItems", parm);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static DataSet GetGratuityDetails(HRCommon objcommon, int CompanyID, int SiteID, string empid, int Month, int Year, DateTime cDate, int accpost)
        {
            SqlParameter[] sqlParams = new SqlParameter[11];
            sqlParams[0] = new SqlParameter("@CompanyID", CompanyID);
            sqlParams[1] = new SqlParameter("@CurrentPage", objcommon.CurrentPage);
            sqlParams[2] = new SqlParameter("@PageSize", objcommon.PageSize);
            sqlParams[3] = new SqlParameter("@NoofRecords", System.Data.SqlDbType.Int);
            sqlParams[3].Direction = ParameterDirection.Output;
            sqlParams[4] = new SqlParameter();
            sqlParams[4].Direction = ParameterDirection.ReturnValue;
            sqlParams[5] = new SqlParameter("@WorkSite", SiteID);
            if (empid != string.Empty)
                sqlParams[6] = new SqlParameter("@empid", empid);
            else
                sqlParams[6] = new SqlParameter("@empid", null);
            sqlParams[7] = new SqlParameter("@smonth", Month);
            sqlParams[8] = new SqlParameter("@syear", Year);
            sqlParams[9] = new SqlParameter("@CutDate", cDate);
            sqlParams[10] = new SqlParameter("@accpost", accpost);
            DataSet ds = SQLDBUtil.ExecuteDataset("USP_HMS_GratuityCaluculations", sqlParams);
            objcommon.NoofRecords = (int)sqlParams[3].Value;
            objcommon.TotalPages = (int)sqlParams[4].Value;
            return ds;
        }
        public static DataSet GetGratuityDetailsSearch(HRCommon objcommon, int CompanyID, int SiteID, int Deptid, string empid, int Month, int Year, DateTime cDate, int accpost, int id)
        {
            SqlParameter[] sqlParams = new SqlParameter[13];
            sqlParams[0] = new SqlParameter("@CompanyID", CompanyID);
            sqlParams[1] = new SqlParameter("@CurrentPage", objcommon.CurrentPage);
            sqlParams[2] = new SqlParameter("@PageSize", objcommon.PageSize);
            sqlParams[3] = new SqlParameter("@NoofRecords", System.Data.SqlDbType.Int);
            sqlParams[3].Direction = ParameterDirection.Output;
            sqlParams[4] = new SqlParameter();
            sqlParams[4].Direction = ParameterDirection.ReturnValue;
            sqlParams[5] = new SqlParameter("@WorkSite", SiteID);
            if (empid != string.Empty)
                sqlParams[6] = new SqlParameter("@empid", empid);
            else
                sqlParams[6] = new SqlParameter("@empid", null);
            sqlParams[7] = new SqlParameter("@smonth", Month);
            sqlParams[8] = new SqlParameter("@syear", Year);
            sqlParams[9] = new SqlParameter("@CutDate", cDate);
            sqlParams[10] = new SqlParameter("@accpost", accpost);
            sqlParams[11] = new SqlParameter("@Deptid", Deptid);
            sqlParams[12] = new SqlParameter("@id", id);
            DataSet ds = SQLDBUtil.ExecuteDataset("USP_HMS_GratuityCaluculationsSearch", sqlParams);
            objcommon.NoofRecords = (int)sqlParams[3].Value;
            objcommon.TotalPages = (int)sqlParams[4].Value;
            return ds;
        }
        public static DataSet HMS_GratuityTranserAccXML(DataSet dsTransferDetail, int UserId, string Remarks, DateTime cutdate)
        {
            try
            {
                SqlParameter[] objParam = new SqlParameter[4];
                objParam[0] = new SqlParameter("@TransItems", dsTransferDetail.GetXml());
                objParam[1] = new SqlParameter("@Remarks", Remarks);
                objParam[2] = new SqlParameter("@UserID", UserId);
                objParam[3] = new SqlParameter("@cutdate", cutdate);
                DataSet ds = SQLDBUtil.ExecuteDataset("HMS_Gratuity_TranserAccXML", objParam);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet HR_GetEmployeeGratuity_LOP(HRCommon objcommon, string EmpId, string EmpName)
        {
            SqlParameter[] sqlParams = new SqlParameter[7];
            sqlParams[0] = new SqlParameter("@EmpName", EmpName);
            sqlParams[1] = new SqlParameter("@CurrentPage", objcommon.CurrentPage);
            sqlParams[2] = new SqlParameter("@PageSize", objcommon.PageSize);
            sqlParams[3] = new SqlParameter("@NoofRecords", 3);
            sqlParams[3].Direction = ParameterDirection.Output;
            sqlParams[4] = new SqlParameter();
            sqlParams[4].Direction = ParameterDirection.ReturnValue;
            if (EmpId != string.Empty)
                sqlParams[5] = new SqlParameter("@empid", EmpId);
            else
                sqlParams[5] = new SqlParameter("@empid", null);
            sqlParams[6] = new SqlParameter("@id", 5);
            DataSet ds = SQLDBUtil.ExecuteDataset("USP_HMS_Gratuity_LOP_Insert_Update_Delete", sqlParams);
            objcommon.NoofRecords = (int)sqlParams[3].Value;
            objcommon.TotalPages = (int)sqlParams[4].Value;
            return ds;
        }
        public static DataSet GetGratuity_LOP_Details(int ID)
        {
            throw new NotImplementedException();
        }
        public static int InsUpdGratuity_LOP(int Pk_OBID, string EmpId, string EmpName, int LOP, int id)
        {
            try
            {
                int retval;
                SqlParameter[] sqlParams = new SqlParameter[5];
                sqlParams[0] = new SqlParameter("@EmpId", EmpId);
                sqlParams[1] = new SqlParameter("@EmpName", EmpName);
                sqlParams[2] = new SqlParameter("@LOP", LOP);
                sqlParams[3] = new SqlParameter("@id", id);
                sqlParams[4] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
                sqlParams[4].Direction = ParameterDirection.ReturnValue;
                sqlParams[3] = new SqlParameter("@pk_obid", Pk_OBID);
                SQLDBUtil.ExecuteDataset("USP_HMS_Gratuity_LOP_Insert_Update_Delete", sqlParams);
                retval = (int)sqlParams[4].Value;
                return retval;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static DataSet HR_ddl_SearchEmpBySiteDept(int WS)
        {
            SqlParameter[] sqlParams = new SqlParameter[1];
            sqlParams[0] = new SqlParameter("@WS", WS);
            DataSet ds = SQLDBUtil.ExecuteDataset("HR_ddl_SearchEmpBySiteDept", sqlParams);
            return ds;
        }
        public DataSet GetAttMultiplelist_WithSync(DateTime? Date, int? Month, int? Year, int EmpID, int ReptType, int SyncToNorLog)
        {
            try
            {
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetAttMultiplelist", new SqlParameter[] { new SqlParameter("@Date", Date),
                                                                         new SqlParameter("@Month", Month), 
                                                                         new SqlParameter("@Year", Year), 
                                                                         new SqlParameter("@EmpID", EmpID), 
                                                                         new SqlParameter("@ReptType", ReptType), 
                                                                         new SqlParameter("@SyncToNorLog",SyncToNorLog)   });
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int MultiLog_To_NormalLog_Sync(DateTime Date, int EmpID, int UserID)
        {
            try
            {
                int retval;
                SqlParameter[] sqlParams = new SqlParameter[4];
                sqlParams[0] = new SqlParameter("@Date", Date);
                sqlParams[1] = new SqlParameter("@EmpID", EmpID);
                sqlParams[2] = new SqlParameter("@UserID", UserID);
                sqlParams[3] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
                sqlParams[3].Direction = ParameterDirection.ReturnValue;
                SQLDBUtil.ExecuteNonQuery("HMS_MulLogtoNorLogSyncAttendance", sqlParams);
                retval = (int)sqlParams[3].Value;
                return retval;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static void HR_Upd_emptypeStatus(int EmptyID, bool Status)
        {
            SqlParameter[] parm = new SqlParameter[2];
            parm[0] = new SqlParameter("@EmptyID", EmptyID);
            parm[1] = new SqlParameter("@Status", Status);
            SQLDBUtil.ExecuteNonQuery("HR_Upd_emptypeStatus", parm);
        }
        public static DataSet GetGratuityDetails_Monthly(HRCommon objcommon, int CompanyID, int SiteID, string empid, int Month, int Year, int accpost)
        {
            SqlParameter[] sqlParams = new SqlParameter[10];
            sqlParams[0] = new SqlParameter("@CompanyID", CompanyID);
            sqlParams[1] = new SqlParameter("@CurrentPage", objcommon.CurrentPage);
            sqlParams[2] = new SqlParameter("@PageSize", objcommon.PageSize);
            sqlParams[3] = new SqlParameter("@NoofRecords", 3);
            sqlParams[3].Direction = ParameterDirection.Output;
            sqlParams[4] = new SqlParameter();
            sqlParams[4].Direction = ParameterDirection.ReturnValue;
            sqlParams[5] = new SqlParameter("@WorkSite", SiteID);
            if (empid != string.Empty)
                sqlParams[6] = new SqlParameter("@empid", empid);
            else
                sqlParams[6] = new SqlParameter("@empid", null);
            sqlParams[7] = new SqlParameter("@smonth", Month);
            sqlParams[8] = new SqlParameter("@syear", Year);
            sqlParams[9] = new SqlParameter("@accpost", accpost);
            DataSet ds = SQLDBUtil.ExecuteDataset("USP_HMS_GratuityCaluculations_Monthly", sqlParams);
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows[0][0].ToString() != "Already calculated for this employee for this month")
            {
                objcommon.NoofRecords = (int)sqlParams[3].Value;
                objcommon.TotalPages = (int)sqlParams[4].Value;
            }
            return ds;
        }
        public static DataSet GetGratuityDetails_Final(HRCommon objcommon, int CompanyID, int SiteID, string empid, int accpost, int activetype, DateTime Finaldate)
        {
            SqlParameter[] sqlParams = new SqlParameter[10];
            sqlParams[0] = new SqlParameter("@CompanyID", CompanyID);
            sqlParams[1] = new SqlParameter("@CurrentPage", objcommon.CurrentPage);
            sqlParams[2] = new SqlParameter("@PageSize", objcommon.PageSize);
            sqlParams[3] = new SqlParameter("@NoofRecords", 3);
            sqlParams[3].Direction = ParameterDirection.Output;
            sqlParams[4] = new SqlParameter();
            sqlParams[4].Direction = ParameterDirection.ReturnValue;
            sqlParams[5] = new SqlParameter("@WorkSite", SiteID);
            if (empid != string.Empty)
                sqlParams[6] = new SqlParameter("@empid", empid);
            else
                sqlParams[6] = new SqlParameter("@empid", null);
            sqlParams[7] = new SqlParameter("@accpost", accpost);
            sqlParams[8] = new SqlParameter("@ActiveType", activetype);
            sqlParams[9] = new SqlParameter("@FinalDate", Finaldate);
            DataSet ds = SQLDBUtil.ExecuteDataset("USP_HMS_GratuityCaluculations_Final", sqlParams);
            objcommon.NoofRecords = (int)sqlParams[3].Value;
            objcommon.TotalPages = (int)sqlParams[4].Value;
            return ds;
        }
        public static DataSet HMS_GratuityTranserAccXML_Final(DataSet dsTransferDetail, int UserId, string Remarks)
        {
            try
            {
                SqlParameter[] objParam = new SqlParameter[3];
                objParam[0] = new SqlParameter("@TransItems", dsTransferDetail.GetXml());
                objParam[1] = new SqlParameter("@Remarks", Remarks);
                objParam[2] = new SqlParameter("@UserID", UserId);
                DataSet ds = SQLDBUtil.ExecuteDataset("HMS_Gratuity_TranserAccXML_Final", objParam);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet HMS_GratuityTranserAccXML_Month(DataSet dsTransferDetail, int UserId, string Remarks, DateTime cutdate)
        {
            try
            {
                SqlParameter[] objParam = new SqlParameter[4];
                objParam[0] = new SqlParameter("@TransItems", dsTransferDetail.GetXml());
                objParam[1] = new SqlParameter("@Remarks", Remarks);
                objParam[2] = new SqlParameter("@UserID", UserId);
                objParam[3] = new SqlParameter("@cutdate", cutdate);
                DataSet ds = SQLDBUtil.ExecuteDataset("HMS_Gratuity_TranserAccXML_Monthly", objParam);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static void HR_SavePaySLIP_Vacation(int EmpId, DateTime Date, int IsAccount)
        {
            try
            {
                SqlParameter[] parm = new SqlParameter[3];
                parm[0] = new SqlParameter("@EmpId", EmpId);
                parm[1] = new SqlParameter("@Date", Date);
                parm[2] = new SqlParameter("@IsAccount", IsAccount);
                SQLDBUtil.ExecuteNonQuery("HR_SavePaySLIP", parm);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static DataSet empVsAirTicketsAuth_LISTbyID(int Empid)
        {
            try
            {
                SqlParameter[] objParam = new SqlParameter[1];
                objParam[0] = new SqlParameter("@Empid", Empid);
                DataSet ds = SQLDBUtil.ExecuteDataset("USP_Ticket_Fare", objParam);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet HMS_GetTicket_Fare(int AirlineID, int Passenger_typeID, int Booking_ClassID, int To_CityID)
        {
            SqlParameter[] parm = new SqlParameter[4];
            parm[0] = new SqlParameter("@AirlineID", AirlineID);
            parm[1] = new SqlParameter("@Passenger_typeID", Passenger_typeID);
            parm[2] = new SqlParameter("@Booking_ClassID", Booking_ClassID);
            parm[3] = new SqlParameter("@To_CityID", To_CityID);
            DataSet ds = SQLDBUtil.ExecuteDataset("T_HMS_GetTicket_Fare", parm);
            return ds;
        }
        public static DataSet HMS_EmpVsAirTicketsAuth_Details(int ID)
        {
            SqlParameter[] parm = new SqlParameter[1];
            parm[0] = new SqlParameter("@ID", ID);
            DataSet ds = SQLDBUtil.ExecuteDataset("T_HMS_EmpVsAirTicketsAuth_Details", parm);
            return ds;
        }
        public static int T_HMS_Insert_EmpVsAirTicketsUseage_Details(int Empid, int ID, int UserId, string XMLData)
        {
            int n = 0;
            try
            {
                SqlParameter[] parm = new SqlParameter[4];
                parm[0] = new SqlParameter("@Empid", Empid);
                parm[1] = new SqlParameter("@ID", ID);
                parm[2] = new SqlParameter("@UserId", UserId);
                parm[3] = new SqlParameter("@xmlDet", XMLData.ToString());
                n = SQLDBUtil.ExecuteNonQuery("T_HMS_Insert_EmpVsAirTicketsUseage_Details", parm);
            }
            catch (Exception)
            {
                throw;
            }
            return n;
        }
        public static DataSet Get_EmpVsAirTicketsUseage_Details(int EmpID)
        {
            SqlParameter[] parm = new SqlParameter[1];
            parm[0] = new SqlParameter("@EmpID", EmpID);
            DataSet ds = SQLDBUtil.ExecuteDataset("T_HMS_Get_EmpVsAirTicketsUseage_Details", parm);
            return ds;
        }
        public static int Get_Total_Working_Days_Emp(int EmpID, string Date)
        {
            int nDays = 0;
            SqlParameter[] parm = new SqlParameter[3];
            parm[0] = new SqlParameter("@EmpID", EmpID);
            parm[1] = new SqlParameter("@Date", Date);
            parm[2] = new SqlParameter("@Wdays", System.Data.SqlDbType.Int);
            parm[2].Direction = ParameterDirection.Output;
            DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetNumofPayableDays_Airlines", parm);
            nDays = (int)parm[2].Value;
            return nDays;
        }
        public static DataSet GetAirline_ByCityWise(int CityID)
        {
            SqlParameter[] sqlParams = new SqlParameter[1];
            sqlParams[0] = new SqlParameter("@CityID", CityID);
            DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetAirline_ByCityWise", sqlParams);
            return ds;
        }
        public static DataSet GetDueDateBy_EMPID(int EmpID)
        {
            SqlParameter[] sqlParams = new SqlParameter[1];
            sqlParams[0] = new SqlParameter("@EmpID", EmpID);
            DataSet ds = SQLDBUtil.ExecuteDataset("HMS_GetDueDate", sqlParams);
            return ds;
        }
        public static int GetMinimum_WorkingDaysByEmpid(int EmpID)
        {
            int nDays = 0;
            SqlParameter[] sqlParams = new SqlParameter[2];
            sqlParams[0] = new SqlParameter("@EmpID", EmpID);
            sqlParams[1] = new SqlParameter("@Wdays", System.Data.SqlDbType.Int);
            sqlParams[1].Direction = ParameterDirection.Output;
            SQLDBUtil.ExecuteNonQuery("HMS_GetMinimum_WorkingDaysByEmpid", sqlParams);
            nDays = Convert.ToInt32(sqlParams[1].Value);
            return nDays;
        }
        public static int GetSalary_Syncd(int EmpID)
        {
            int nSync = 0;
            SqlParameter[] sqlParams = new SqlParameter[2];
            sqlParams[0] = new SqlParameter("@EmpID", EmpID);
            sqlParams[1] = new SqlParameter("@Sync", System.Data.SqlDbType.Int);
            sqlParams[1].Direction = ParameterDirection.Output;
            DataSet ds = SQLDBUtil.ExecuteDataset("HMS_GetSalaries_SYNked_CurrentMonth", sqlParams);
            nSync = (int)sqlParams[1].Value;
            return nSync;
        }
        public static int T_HMS_empVsAirTicketAccount_Sync(int id)
        {
            int nSync = 0;
            SqlParameter[] sqlParams = new SqlParameter[2];
            sqlParams[0] = new SqlParameter("@ID", id);
            sqlParams[1] = new SqlParameter("@Sync", System.Data.SqlDbType.Int);
            sqlParams[1].Direction = ParameterDirection.Output;
            SQLDBUtil.ExecuteNonQuery("T_HMS_empVsAirTicketAccount_Sync", sqlParams);
            nSync = (int)sqlParams[1].Value;
            return nSync;
        }
        public static DataSet HR_GetEmployee_google(String SearchKey)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@Search", SearchKey);
                return SQLDBUtil.ExecuteDataset("HR_GetEmployee_google", param);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static DataSet HR_GetDepartmentBySite_google(String SearchKey)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@Search", SearchKey);
                return SQLDBUtil.ExecuteDataset("HR_GetDepartmentBySite_google", param);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static DataSet HR_Getworksiteby_googlesearch(String SearchKey)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@Search", SearchKey);
                return SQLDBUtil.ExecuteDataset("HR_Getworksiteby_googlesearch", param);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static DataSet GetGoogleSearchEmployeeName(String SearchKey)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@Search", SearchKey);
                return SQLDBUtil.ExecuteDataset("HMS_GoogleSearch_EmployeeEvaluation", param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet GetGoogleSearchCriteriaName(String SearchKey)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@Search", SearchKey);
                return SQLDBUtil.ExecuteDataset("HMS_Googlesearch_Criteria", param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet HR_Getemployeeclearence_EMP_BlackList(HRCommon objHrCommon, int? EmpID, int? WS, int? Dept, int CompanyID)
        {
            SqlParameter[] sqlParams = new SqlParameter[8];
            sqlParams[0] = new SqlParameter("@CurrentPage", objHrCommon.CurrentPage);
            sqlParams[1] = new SqlParameter("@PageSize", objHrCommon.PageSize);
            sqlParams[2] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
            sqlParams[2].Direction = ParameterDirection.ReturnValue;
            sqlParams[3] = new SqlParameter("@NoofRecords", System.Data.SqlDbType.Int);
            sqlParams[3].Direction = ParameterDirection.Output;
            sqlParams[4] = new SqlParameter("@EmpID", EmpID);
            sqlParams[5] = new SqlParameter("@WS", WS);
            sqlParams[6] = new SqlParameter("@Dept", Dept);
            sqlParams[7] = new SqlParameter("@CompanyID", CompanyID);
            DataSet ds = SQLDBUtil.ExecuteDataset("HR_Getemployeeclearence_EMP_BlackList", sqlParams);
            objHrCommon.NoofRecords = (int)sqlParams[3].Value;
            objHrCommon.TotalPages = (int)sqlParams[2].Value;
            return ds;
        }
        public static DataSet HR_GoogleSearchEmpBySiteDept_Evalution(string SearchKey, int WS, int DeptNo, string Status, int CompanyID)
        {
            try
            {
                SqlParameter[] parm = new SqlParameter[5];
                parm[0] = new SqlParameter("@Search", SearchKey);
                parm[1] = new SqlParameter("@WS", WS);
                parm[2] = new SqlParameter("@Dept", DeptNo);
                parm[3] = new SqlParameter("@Status", Status);
                parm[4] = new SqlParameter("@CompanyID", CompanyID);
                return SQLDBUtil.ExecuteDataset("HR_GoogleSerac_SearchEmpBySiteDept_evalution", parm);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public DataSet HR_EmpSalriesListByEmpID(int? EmpID, int Month, int Year)
        {
            try
            {
                SqlParameter[] objParam = new SqlParameter[3];
                objParam[0] = new SqlParameter("@EmpID", EmpID);
                objParam[1] = new SqlParameter("@Month", Month);
                objParam[2] = new SqlParameter("@Year", Year);
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_EmpSalriesListByEmpID", objParam);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet GetDefault_UOM()
        {
            try
            {
                DataSet ds = SQLDBUtil.ExecuteDataset("HMS_GetDefault_UOM");
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static void HR_SavePaySLIP2(int EmpId, string Date, int IsAccount, string Formtype)
        {
            try
            {
                SqlParameter[] parm = new SqlParameter[4];
                parm[0] = new SqlParameter("@EmpId", EmpId);
                parm[1] = new SqlParameter("@Date", Date);
                parm[2] = new SqlParameter("@IsAccount", IsAccount);
                if (Formtype != string.Empty)
                    parm[3] = new SqlParameter("@Formtype", Formtype);
                else
                    parm[3] = new SqlParameter("@Formtype", DBNull.Value);
                SQLDBUtil.ExecuteNonQuery("HR_SavePaySLIP", parm);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static DataSet Bindname_googlesearch(string SearchKey)
        {
            SqlParameter[] parm = new SqlParameter[1];
            parm[0] = new SqlParameter("@SNAME ", SearchKey);
            DataSet ds = SQLDBUtil.ExecuteDataset("HMS_NAME_GOOGLESEARCH", parm);
            return ds;
        }
        public static DataSet BindCONname_googlesearch(string SearchKey)
        {
            SqlParameter[] parm = new SqlParameter[1];
            parm[0] = new SqlParameter("@SNAME ", SearchKey);
            DataSet ds = SQLDBUtil.ExecuteDataset("HMS_CONNAME_GOOGLESEARCH", parm);
            return ds;
        }
        public static DataSet CheckEmployeeConfiguration(int EmpID)
        {
            try
            {
                SqlParameter[] objParam = new SqlParameter[1];
                objParam[0] = new SqlParameter("@EmpID", EmpID);
                DataSet ds = SQLDBUtil.ExecuteDataset("USP_CheckEmployeeConfigurationDetails", objParam);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static void OMS_Insert_ManPower_Requisition(int CompanyID, int ResID, int DesgId, int CatID,
          DateTime Date, decimal ReqNos, int CreatedBy, int @WorksiteID, int ProjectID, string ReqReference)
        {
            try
            {
                SqlParameter[] p = new SqlParameter[10];
                p[0] = new SqlParameter("@CompanyID", CompanyID);
                p[1] = new SqlParameter("@ResID", ResID);
                p[2] = new SqlParameter("@DesignID", DesgId);
                p[3] = new SqlParameter("@CatID", CatID);
                p[4] = new SqlParameter("@ReqOnDate", Date);
                p[5] = new SqlParameter("@ReqNos", ReqNos);
                p[6] = new SqlParameter("@CreatedBy", CreatedBy);
                p[7] = new SqlParameter("@WorksiteID", WorksiteID);
                p[8] = new SqlParameter("@ProjectID", ProjectID);
                p[9] = new SqlParameter("@ReqReference", ReqReference);
                SQLDBUtil.ExecuteNonQuery("HMS_Insert_Manpower_Requisition", p);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet HR_GetAttLogDetails(int AttID)
        {
            try
            {
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetAttLogDetails", new SqlParameter[] { new SqlParameter("@AttID", AttID) });
                return ds;
            }
            catch (Exception ex)
            {
            }
            return null;
        }
        public static int AddVacationSettlementRev4(Double A1, Double A2, Double A3, Double A4, Double A5, Double A6, Double A7, Double D1, Double D2, Double D3, Double D4, Double D5, Double D6, Double D7, int Empid, int Companyid, string Remarks, Double Totalamt, string Ledger, int month, int year, string A6Remarks, string A7Remarks, string D6Remarks, string D7Remarks, double gratuity, string form, string Presentdays, string NoOfDays, int Status, DateTime SettlementDate, Double AdjAmt, Double EmpPen, int ExitType
            , string A1Label, string A2Label, string A3Label, string A4Label, string A5Label, string D1Label, string D2Label, string D3Label,
            string D4Label, string D5Label, string GratutityRemarks,string D3Remarks, string LoanProof, int LoanCheck, string AirticketProof, 
            string A1Remarks, string A2Remarks, string A3Remarks, string A4Remarks, string A5Remarks, string D1Remarks, string D2Remarks, string D4Remarks, string D5Remarks, string D8Remarks, string D9Remarks, Double D3LoanAmount, string GratuityRemarks1)
        {
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[63];
                sqlParams[0] = new SqlParameter("@A1", A1);
                sqlParams[1] = new SqlParameter("@A2", A2);
                sqlParams[2] = new SqlParameter("@A3", A3);
                sqlParams[3] = new SqlParameter("@A4", A4);
                sqlParams[16] = new SqlParameter("@A5", A5);
                sqlParams[18] = new SqlParameter("@A6", A6);
                sqlParams[19] = new SqlParameter("@A7", A7);
                sqlParams[4] = new SqlParameter("@D1", D1);
                sqlParams[5] = new SqlParameter("@D2", D2);
                sqlParams[6] = new SqlParameter("@D3", D3);
                sqlParams[7] = new SqlParameter("@D4", D4);
                sqlParams[8] = new SqlParameter("@D5", D5);
                sqlParams[20] = new SqlParameter("@D6", D6);
                sqlParams[21] = new SqlParameter("@D7", D7);
                sqlParams[9] = new SqlParameter("@Empid", Empid);
                sqlParams[10] = new SqlParameter("@Companyid", Companyid);
                sqlParams[11] = new SqlParameter("@TotAmt", Totalamt);
                sqlParams[12] = new SqlParameter("@Ledger", Ledger);
                sqlParams[13] = new SqlParameter("@month", month);
                sqlParams[14] = new SqlParameter("@Year", year);
                sqlParams[15] = new SqlParameter("@Remarks", Remarks);
                sqlParams[17] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
                sqlParams[17].Direction = ParameterDirection.ReturnValue;
                sqlParams[22] = new SqlParameter("@A6Remarks", A6Remarks);
                sqlParams[23] = new SqlParameter("@A7Remarks", A7Remarks);
                sqlParams[24] = new SqlParameter("@D6Remarks", D6Remarks);
                sqlParams[25] = new SqlParameter("@D7Remarks", D7Remarks);
                sqlParams[26] = new SqlParameter("@Gratuity", gratuity);
                sqlParams[27] = new SqlParameter("@form", form);
                if (@Presentdays != "")
                    sqlParams[28] = new SqlParameter("@PresentDays", Presentdays);
                else
                    sqlParams[28] = new SqlParameter("@PresentDays", 0);
                if (NoOfDays != "")
                    sqlParams[29] = new SqlParameter("@NoOfDayInMonth", NoOfDays);
                else
                    sqlParams[29] = new SqlParameter("@NoOfDayInMonth", 0);
                sqlParams[30] = new SqlParameter("@Status", Status);
                sqlParams[31] = new SqlParameter("@SettlementDate", SettlementDate);
                sqlParams[32] = new SqlParameter("@AdjAmt", AdjAmt);
                sqlParams[33] = new SqlParameter("@EmpPen", EmpPen);
                sqlParams[34] = new SqlParameter("@ExitType", ExitType);
                sqlParams[35] = new SqlParameter("@A1Label", A1Label);
                sqlParams[36] = new SqlParameter("@A2Label", A2Label);
                sqlParams[37] = new SqlParameter("@A3Label", A3Label);
                sqlParams[38] = new SqlParameter("@A4Label", A4Label);
                sqlParams[39] = new SqlParameter("@A5Label", A5Label);
                sqlParams[40] = new SqlParameter("@D1Label", D1Label);
                sqlParams[41] = new SqlParameter("@D2Label", D2Label);
                sqlParams[42] = new SqlParameter("@D3Label", D3Label);
                sqlParams[43] = new SqlParameter("@D4Label", D4Label);
                sqlParams[44] = new SqlParameter("@D5Label", D5Label);
                sqlParams[45] = new SqlParameter("@GratuityRemarks", GratutityRemarks);
                sqlParams[46] = new SqlParameter("@D3Remarks", D3Remarks);
                sqlParams[47] = new SqlParameter("@LoanProof", LoanProof);
                sqlParams[48] = new SqlParameter("@LoanCheck", LoanCheck);
                sqlParams[49] = new SqlParameter("@AirticketProof", AirticketProof);
                sqlParams[50] = new SqlParameter("@A1Remarks", A1Remarks);
                sqlParams[51] = new SqlParameter("@A2Remarks", A2Remarks);
                sqlParams[52] = new SqlParameter("@A3Remarks", A3Remarks);
                sqlParams[53] = new SqlParameter("@A4Remarks", A4Remarks);
                sqlParams[54] = new SqlParameter("@A5Remarks", A5Remarks);
                sqlParams[55] = new SqlParameter("@D1Remarks", D1Remarks);
                sqlParams[56] = new SqlParameter("@D2Remarks", D2Remarks);
                sqlParams[57] = new SqlParameter("@D4Remarks", D4Remarks);
                sqlParams[58] = new SqlParameter("@D5Remarks", D5Remarks);
                sqlParams[59] = new SqlParameter("@D8Remarks", D8Remarks);
                sqlParams[60] = new SqlParameter("@D9Remarks", D9Remarks);
                sqlParams[61] = new SqlParameter("@D3LoanAmount", D3LoanAmount);
                sqlParams[62] = new SqlParameter("@GratuityRemarks1", GratuityRemarks1);

                SQLDBUtil.ExecuteNonQuery("HMS_InsVacationSettlementRev4", sqlParams);
                int id = (int)sqlParams[17].Value;
                
                return id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet GetSearchEmpNameInactive(string SearchKey)
        {
            try
            {
                SqlParameter[] par = new SqlParameter[1];
                par[0] = new SqlParameter("@search", SearchKey);
                return SQLDBUtil.ExecuteDataset("HMS_Name_basedon_dept_Googlesearch_Inactive", par);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet GetAttendanceByDay_CursorInactive(DateTime Day, int Dept, int WS, int empid, string Name, int CompnayID, int CurrentPage, int PageSize, ref int NoofRecords, ref int TotalPages)
        {
            try
            {
                SqlParameter[] objParam = new SqlParameter[10];
                objParam[0] = new SqlParameter("@Date", Day);
                objParam[1] = new SqlParameter("@WSID", WS);
                objParam[2] = new SqlParameter("@DeptID", Dept);
                objParam[3] = new SqlParameter("@Userid", empid);
                objParam[4] = new SqlParameter("@Name", Name);
                objParam[5] = new SqlParameter("@CompanyID", CompnayID);
                objParam[6] = new SqlParameter("@CurrentPage", CurrentPage);
                objParam[7] = new SqlParameter("@PageSize", PageSize);
                objParam[8] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
                objParam[8].Direction = ParameterDirection.ReturnValue;
                objParam[9] = new SqlParameter("@NoofRecords", System.Data.SqlDbType.Int);
                objParam[9].Direction = ParameterDirection.Output;
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetAttendanceByDay_Cursor_Inactive", objParam);
                NoofRecords = (int)objParam[9].Value;
                TotalPages = (int)objParam[8].Value;
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet HR_GetAttandanceByPagingInactive(int EMPID, int WSID, int DeptID, int EMPNAture,
            DateTime StDate, DateTime EdDate, int CurrentPage, int PageSize, ref int NoofRecords, ref int TotalPages, string EmpName)
        {
            try
            {
                SqlParameter[] objParam = new SqlParameter[11];
                objParam[0] = new SqlParameter("@EMPID", SqlDbType.Int);
                if (EMPID > 0)
                    objParam[0].Value = EMPID;
                objParam[1] = new SqlParameter("@WSID", SqlDbType.Int);
                if (WSID > 0)
                    objParam[1].Value = WSID;
                objParam[2] = new SqlParameter("@DeptID", SqlDbType.Int);
                if (DeptID > 0)
                    objParam[2].Value = DeptID;
                objParam[3] = new SqlParameter("@EMPNAture", SqlDbType.Int);
                if (EMPNAture > 0)
                    objParam[3].Value = EMPNAture;
                objParam[4] = new SqlParameter("@StDate", StDate);
                objParam[5] = new SqlParameter("@EdDate", EdDate);
                objParam[6] = new SqlParameter("@CurrentPage", CurrentPage);
                objParam[7] = new SqlParameter("@PageSize", PageSize);
                objParam[8] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
                objParam[8].Direction = ParameterDirection.ReturnValue;
                objParam[9] = new SqlParameter("@NoofRecords", System.Data.SqlDbType.Int);
                objParam[9].Direction = ParameterDirection.Output;
                objParam[10] = new SqlParameter("@Name", EmpName);
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetAttandanceByPagingInactive", objParam);
                NoofRecords = (int)objParam[9].Value;
                TotalPages = (int)objParam[8].Value;
                return ds;
            }
            catch (Exception ex)
            {
            }
            return null;
        }
        public static DataSet GetProject(string prefixText, int siteid)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[2];
                param[0] = new SqlParameter("@Search", prefixText);
                if (siteid != 0)
                    param[1] = new SqlParameter("@siteid", siteid);
                else
                    param[1] = new SqlParameter("@siteid", System.Data.SqlDbType.Int);
                return SQLDBUtil.ExecuteDataset("sh_GETProjectbySearch", param);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public DataSet GetAttendanceByDay_Cursor_Unpuntual(DateTime Day, int Dept, int WS, int empid, string Name, int CompnayID, int CurrentPage, int PageSize, ref int NoofRecords, ref int TotalPages, int Projectid)
        {
            try
            {
                SqlParameter[] objParam = new SqlParameter[11];
                objParam[0] = new SqlParameter("@Date", Day);
                objParam[1] = new SqlParameter("@WSID", WS);
                objParam[2] = new SqlParameter("@DeptID", Dept);
                objParam[3] = new SqlParameter("@Userid", empid);
                objParam[4] = new SqlParameter("@Name", Name);
                objParam[5] = new SqlParameter("@CompanyID", CompnayID);
                objParam[6] = new SqlParameter("@CurrentPage", CurrentPage);
                objParam[7] = new SqlParameter("@PageSize", PageSize);
                objParam[8] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
                objParam[8].Direction = ParameterDirection.ReturnValue;
                objParam[9] = new SqlParameter("@NoofRecords", System.Data.SqlDbType.Int);
                objParam[9].Direction = ParameterDirection.Output;
                if (Projectid != 0)
                    objParam[10] = new SqlParameter("@Projectid", Projectid);
                else
                    objParam[10] = new SqlParameter("@Projectid", System.Data.SqlDbType.Int);
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetAttendanceByDay_Cursor_Unpuntual", objParam);
                NoofRecords = (int)objParam[9].Value;
                TotalPages = (int)objParam[8].Value;
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static void sa_InsertPrepaidSecurityDep(int WONO, int UserID, double SecDepAmt, int LedgerID, double PrePadiRentAmt, int CompanyID,
            int prepaidMonths, bool IsWoRenewal)
        {
            try
            {
                //@WONO int,@UserID int,@SecDepAmt numeric(18,2),@LedgerId int=null,@PrePadiRentAmt numeric(18,2)=null,@CompanyId
                SqlParameter[] parm = new SqlParameter[8];
                parm[0] = new SqlParameter("@WONO", WONO);
                parm[1] = new SqlParameter("@UserID", UserID);
                parm[2] = new SqlParameter("@SecDepAmt", SecDepAmt);
                parm[3] = new SqlParameter("@LedgerID", LedgerID);
                parm[4] = new SqlParameter("@PrePadiRentAmt", PrePadiRentAmt);
                parm[5] = new SqlParameter("@CompanyId", CompanyID);
                parm[6] = new SqlParameter("@PrePaidRentQty", prepaidMonths);
                parm[7] = new SqlParameter("@IsWoRenewal", IsWoRenewal);                        
                SQLDBUtil.ExecuteNonQuery("sa_InsertPrepaidSecurityDep", parm);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet sh_GetHirePrepaidDet(int WONo)
        {
            SqlParameter[] objParam = new SqlParameter[1];
            objParam[0] = new SqlParameter("@WONo", WONo);
            DataSet ds = SQLDBUtil.ExecuteDataset("sh_GetHirePrepaidDet", objParam);
            return ds;
        }
        public static void sa_ClosedWOPrePaidRent(int wONO, int CompanyID, int UserId)
        {
            SqlParameter[] parm = new SqlParameter[3];
            parm[0] = new SqlParameter("@WONO", wONO);
            parm[1] = new SqlParameter("@CompanyId", CompanyID);
            parm[2] = new SqlParameter("@UserID", UserId);
            SQLDBUtil.ExecuteNonQuery("sa_ClosedWOPrePaidRent", parm);
        }
        public static void sa_PrePaidExpensesAdjustedTransaction(int WONO, int SRNItemID, int ResourceID, int UserID,
            int tModuleID,int CompanyId)
        {
            SqlParameter[] parms = new SqlParameter[6];
            parms[0] = new SqlParameter("@WONO", WONO);
            parms[1] = new SqlParameter("@SRNItemID", SRNItemID);
            parms[2] = new SqlParameter("@ResourceID", ResourceID);
            parms[3] = new SqlParameter("@UserID", UserID);
            parms[4] = new SqlParameter("@ModuleID", tModuleID);
            parms[5] = new SqlParameter("@CompanyId", CompanyId);
            SQLDBUtil.ExecuteNonQuery("sa_PrePaidExpensesAdjustedTransaction", parms, "");
        }
    }
}
