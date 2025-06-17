using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

using AECLOGIC.HMS.BLL;
using Aeclogic.Common.DAL;
/// <summary>
/// Summary description for PayRoll
/// </summary>
/// 

namespace AECLOGIC.ERP.HMS
{
    public class PayRollMgr
    {
        public PayRollMgr()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        #region Coomon
        //States

        public static DataSet GetStatesList()
        {
            return SQLDBUtil.ExecuteDataset("Get_States");
        }
        #endregion

        #region  FinacilYear
        
        public static int InsUpdateFinacilYear(int FinYearId, DateTime FromDate, DateTime TODate, string Name)
        {
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[5];
                sqlParams[0] = new SqlParameter("@FinYearId", FinYearId);
                sqlParams[1] = new SqlParameter("@FromDate", FromDate);
                sqlParams[2] = new SqlParameter("@TODate", TODate);
                sqlParams[3] = new SqlParameter("@Name", Name);
                sqlParams[4] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
                sqlParams[4].Direction = ParameterDirection.ReturnValue;
                SQLDBUtil.ExecuteNonQuery("HR_InsUpd_FinancialYear", sqlParams);
                return Convert.ToInt32(sqlParams[4].Value);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static DataSet GetFinacialYearList()
        {
            return SQLDBUtil.ExecuteDataset("HR_Get_FinancialYearList");
        }
        public static DataSet GetFinacilYearDetails(int FinYearId)
        {
            try
            {
                
                DataSet ds= SQLDBUtil.ExecuteDataset("HR_Get_FinancialYearDetails", new SqlParameter[] { new SqlParameter("@FinYearId", FinYearId) });
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet GetFinancialYear()
        {
            return SQLDBUtil.ExecuteDataset("ACC_GetYear");
        }

        #endregion

        #region Wages

        public static void InsUpdaWages(int WagesID, string Name, string LongName, int CompanyID)
        {
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[4];
                sqlParams[0] = new SqlParameter("@WagesID", WagesID);
                sqlParams[1] = new SqlParameter("@Name", Name);
                sqlParams[2] = new SqlParameter("@LongName", LongName);
                sqlParams[3] = new SqlParameter("@CompanyID", CompanyID);
                SQLDBUtil.ExecuteNonQuery("HR_InsUpd_Wages", sqlParams);
            }
            catch (Exception e)
            {

            }
        }
        public static int InsUpdWages(int WagesID, string Name, string LongName, int CompanyID)
        {
            try
            {
                SqlParameter[] parm = new SqlParameter[5];
                parm[0] = new SqlParameter("@WagesID", WagesID);
                parm[1] = new SqlParameter("@Name", Name);
                parm[2] = new SqlParameter("@LongName", LongName);
                parm[3] = new SqlParameter("@CompanyID", CompanyID);
                parm[4] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
                parm[4].Direction = ParameterDirection.ReturnValue;
                SQLDBUtil.ExecuteNonQuery("HR_InsUpd_Wages", parm);
                return Convert.ToInt32(parm[4].Value);

            }
            catch (Exception)
            {

                throw;
            }
        }
        public static DataSet GetWagesList()
        {
            return SQLDBUtil.ExecuteDataset("HR_InsUpd_WagesList");
        }
        public static DataSet GetEmpWages(int EmpId, int EmpSalID)
        {
            return SQLDBUtil.ExecuteDataset("HR_EmpWagesList", new SqlParameter[] { new SqlParameter("@EmpId", EmpId), new SqlParameter("@EmpSalID", EmpSalID) });
        }

        public static DataSet GetEmpWagesByContribution(int ContriID)
        {
            return SQLDBUtil.ExecuteDataset("HR_GetEmpWagesByContribution", new SqlParameter[] { new SqlParameter("@ContriID", ContriID) });
        }

        public static DataSet GetEmpWagesByDeduction(int ContriID)
        {
            return SQLDBUtil.ExecuteDataset("HR_GetEmpWagesByDeduction", new SqlParameter[] { new SqlParameter("@ContriID", ContriID) });
        }

        public static DataSet GetWagesDetails(int WagesID)
        {
            try
            {
                
                DataSet ds= SQLDBUtil.ExecuteDataset("HR_InsUpd_WagesDetails", new SqlParameter[] { new SqlParameter("@WagesID", WagesID) });
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static int HR_OTNamesAddEdit(int OTID, string OTName,string OTLName, int CompanyID)
        {
            try
            {
                SqlParameter[] parm = new SqlParameter[5];
                parm[0] = new SqlParameter("@OTID", OTID);
                parm[1] = new SqlParameter("@OTLName", OTLName);
                parm[2] = new SqlParameter("@OTName", OTName);
                parm[3] = new SqlParameter("@CompanyID", CompanyID);
                parm[4] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
                parm[4].Direction = ParameterDirection.ReturnValue;
                SQLDBUtil.ExecuteNonQuery("HR_OTNamesAddEdit", parm);
                return Convert.ToInt32(parm[4].Value);

            }
            catch (Exception)
            {

                throw;
            }
        }
        public static DataSet GetOTDetails(int OTID)
        {
            try
            {
                SqlParameter[] pr=new SqlParameter[1];
                pr[0] = new SqlParameter("@OTID", SqlDbType.Int);
                if (OTID > 0)
                    pr[0].Value = OTID;
                return SQLDBUtil.ExecuteDataset("HR_GetOTNames", pr);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Wages_Calculations

        public static int InsUpdWages_Calculations(int WPID, int FinYearId, int WagesID, double CentageOnCTC)
        {
            try
            {
                SqlParameter[] parm = new SqlParameter[5];
                parm[0] = new SqlParameter("@WPID", WPID);
                parm[1] = new SqlParameter("@WagesID", WagesID);
                parm[2] = new SqlParameter("@FinYearId", FinYearId);
                parm[3] = new SqlParameter("@CentageOnCTC", CentageOnCTC);
                parm[4] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
                parm[4].Direction = ParameterDirection.ReturnValue;
                SQLDBUtil.ExecuteNonQuery("HR_InsUpd_Wages_Calculations", parm);
                return Convert.ToInt32(parm[4].Value);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static int HR_OTConfigurationCreation(int OTConfigID, int LeavTypeID, string OTName,
            double OTValue, string Remarks, int OTID, int CalON, double OTSDValue)
        {
            try
            {
                SqlParameter[] parm = new SqlParameter[9];
                parm[0] = new SqlParameter("@OTConfigID", OTConfigID);
                parm[1] = new SqlParameter("@LeavTypeID", LeavTypeID);
                parm[2] = new SqlParameter("@OTName", OTName);
                parm[3] = new SqlParameter("@OTValue", OTValue);
                parm[4] = new SqlParameter("@Remarks", Remarks);
                parm[5] = new SqlParameter("@OTID", OTID);
                parm[6] = new SqlParameter("@CalON", CalON);
                parm[7] = new SqlParameter("@OTSDValue", OTSDValue);
                parm[8] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
                parm[8].Direction = ParameterDirection.ReturnValue;
                SQLDBUtil.ExecuteNonQuery("HR_OTConfigurationCreation", parm);
                return Convert.ToInt32(parm[8].Value);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static void InsUpdateWages_Calculations(int WPID, int FinYearId, int WagesID, double CentageOnCTC)
        {
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[4];
                sqlParams[0] = new SqlParameter("@WPID", WPID);
                sqlParams[1] = new SqlParameter("@WagesID", WagesID);
                sqlParams[2] = new SqlParameter("@FinYearId", FinYearId);
                sqlParams[3] = new SqlParameter("@CentageOnCTC", CentageOnCTC);

                SQLDBUtil.ExecuteNonQuery("HR_InsUpd_Wages_Calculations", sqlParams);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static DataSet GetWages_CalculationsList(int FinYearID)
        {
            return SQLDBUtil.ExecuteDataset("HR_Get_Wages_CalculationsList", new SqlParameter[] { new SqlParameter("@FinYearID", FinYearID) });
        }
        public static DataSet GetWages_CalculationsDetails(int WPID)
        {
            try
            {
                
                DataSet ds= SQLDBUtil.ExecuteDataset("HR_Get_Wages_CalculationsDetails", new SqlParameter[] { new SqlParameter("@WPID", WPID) });
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void WagesDisplayoder(int ID, int Order)
        {
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[2];
                sqlParams[0] = new SqlParameter("@ID", ID);
                sqlParams[1] = new SqlParameter("@Order", Order);
                SQLDBUtil.ExecuteNonQuery("HR_WagesCalDisplayoder", sqlParams);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static DataSet HR_GetMasterOT(int OTConfigID)
        {
            SqlParameter[] pr = new SqlParameter[1];
            pr[0] = new SqlParameter("@OTConfigID", SqlDbType.Int);
            if (OTConfigID > 0)
                pr[0].Value = OTConfigID;
            return SQLDBUtil.ExecuteDataset("HR_GetMasterOT", pr);
        }

        #endregion
        #region Allowances

        public static int InsUpdAllowances(int AllowId, string Name, string LongName, int CompanyID)
        {
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[5];
                sqlParams[0] = new SqlParameter("@AllowId", AllowId);
                sqlParams[1] = new SqlParameter("@Name", Name);
                sqlParams[2] = new SqlParameter("@LongName", LongName);
                sqlParams[3] = new SqlParameter("@CompanyID", CompanyID);
                sqlParams[4] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
                sqlParams[4].Direction = ParameterDirection.ReturnValue;
                SQLDBUtil.ExecuteNonQuery("HR_InsUpdAllowances", sqlParams);
                return Convert.ToInt32(sqlParams[4].Value);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static void InsUpdateAllowances(int AllowId, string Name, string LongName, int CompanyID)
        {
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[4];
                sqlParams[0] = new SqlParameter("@AllowId", AllowId);
                sqlParams[1] = new SqlParameter("@Name", Name);
                sqlParams[2] = new SqlParameter("@LongName", LongName);
                sqlParams[3] = new SqlParameter("@CompanyID", CompanyID);
                SQLDBUtil.ExecuteNonQuery("HR_InsUpd_Allowances", sqlParams);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static DataSet GetAllowancesList()
        {
            return SQLDBUtil.ExecuteDataset("HR_Get_AllowancesList");
        }


        public static DataSet GetAllowancesListByPaging(HRCommon objHrCommon)
        {
            SqlParameter[] sqlParams = new SqlParameter[4];
            sqlParams[0] = new SqlParameter("@CurrentPage", objHrCommon.CurrentPage);
            sqlParams[1] = new SqlParameter("@PageSize", objHrCommon.PageSize);
            sqlParams[2] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
            sqlParams[2].Direction = ParameterDirection.ReturnValue;
            sqlParams[3] = new SqlParameter("@NoofRecords", System.Data.SqlDbType.Int);
            sqlParams[3].Direction = ParameterDirection.Output;
            
            DataSet ds= SQLDBUtil.ExecuteDataset("HR_Get_AllowancesListByPaging", sqlParams);
           objHrCommon.NoofRecords= (int)sqlParams[3].Value;
            objHrCommon.TotalPages = (int)sqlParams[2].Value;
            return ds;

        }


        public static DataSet GetEmpAllowancesList(int EmpId, int EmpSalID)
        {
            return SQLDBUtil.ExecuteDataset("HR_EmpAllowancesList", new SqlParameter[] { new SqlParameter("@EmpId", EmpId), new SqlParameter("@EmpSalID", EmpSalID) });
        }
        public static DataSet GetEmpNonCTCList(int EmpId, int EmpSalID)
        {
            return SQLDBUtil.ExecuteDataset("HR_EmpNonCTCList", new SqlParameter[] { new SqlParameter("@EmpId", EmpId), new SqlParameter("@EmpSalID", EmpSalID) });
        }
        public static DataSet GetEmpAllowancesByContribution(int ContriID)
        {
            return SQLDBUtil.ExecuteDataset("HR_GetEmpAllowancesByContribution", new SqlParameter[] { new SqlParameter("@ContriID", ContriID) });
        }
        public static DataSet GetEmpAllowancesByDeduction(int ContriID)
        {
            return SQLDBUtil.ExecuteDataset("HR_GetEmpAllowancesByDeduction", new SqlParameter[] { new SqlParameter("@ContriID", ContriID) });
        }
        public static DataSet GetAllowancesDetails(int AllowId)
        {
            try
            {
                
                DataSet ds= SQLDBUtil.ExecuteDataset("HR_Get_AllowancesDetails", new SqlParameter[] { new SqlParameter("@AllowId", AllowId) });
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void AllowancesDisplayoder(int ID, int Order)
        {
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[2];
                sqlParams[0] = new SqlParameter("@ID", ID);
                sqlParams[1] = new SqlParameter("@Order", Order);
                SQLDBUtil.ExecuteNonQuery("HR_Allowance_Displayoder", sqlParams);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
        #region Allowance_Calculations
        public static int InsUpdAllowance_Calculation(int AllowCalcId, int AllowId, int FinYearId, double Percentage, int CalculatedOn, double LimitedTo)
        {
            try
            {
                SqlParameter[] parm = new SqlParameter[7];
                parm[0] = new SqlParameter("@AllowCalcId", AllowCalcId);
                parm[1] = new SqlParameter("@AllowId", AllowId);
                parm[2] = new SqlParameter("@FinYearId", FinYearId);
                parm[3] = new SqlParameter("@Percentage", Percentage);
                parm[4] = new SqlParameter("@CalculatedOn", CalculatedOn);
                parm[5] = new SqlParameter("@LimitedTo", LimitedTo);
                parm[6] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
                parm[6].Direction = ParameterDirection.ReturnValue;
                SQLDBUtil.ExecuteNonQuery("HR_InsUpd_Allowance_Calculations", parm);
                return Convert.ToInt32(parm[6].Value);

            }
            catch (Exception)
            {

                throw;
            }
        }

        public static void InsUpdateAllowance_Calculations(int AllowCalcId, int AllowId, int FinYearId, double Percentage, int CalculatedOn, double LimitedTo)
        {
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[6];
                sqlParams[0] = new SqlParameter("@AllowCalcId", AllowCalcId);
                sqlParams[1] = new SqlParameter("@AllowId", AllowId);
                sqlParams[4] = new SqlParameter("@FinYearId", FinYearId);
                sqlParams[2] = new SqlParameter("@Percentage", Percentage);
                sqlParams[5] = new SqlParameter("@CalculatedOn", CalculatedOn);
                sqlParams[3] = new SqlParameter("@LimitedTo", LimitedTo);
                SQLDBUtil.ExecuteNonQuery("HR_InsUpd_Allowance_Calculations", sqlParams);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static DataSet GetAllowance_CalculationsList(int FinYearID)
        {
            return SQLDBUtil.ExecuteDataset("HR_Get_Allowance_CalculationsList", new SqlParameter[] { new SqlParameter("@FinYearID", FinYearID) });
        }
        public static DataSet GetAllowance_CalculationsDetails(int AllowCalcId)
        {
            try
            {
                
                DataSet ds= SQLDBUtil.ExecuteDataset("HR_Get_Allowance_CalculationsDetails", new SqlParameter[] { new SqlParameter("@AllowCalcId", AllowCalcId) });
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static void AllowancesCalDisplayoder(int ID, int Order)
        {
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[2];
                sqlParams[0] = new SqlParameter("@ID", ID);
                sqlParams[1] = new SqlParameter("@Order", Order);
                SQLDBUtil.ExecuteNonQuery("HR_Allowance_CalculationsDisplayoder", sqlParams);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region ReimburseItems


        public static int InsUpdReimbursementItems(int RMItemId, string Name, int CompanyID)
        {
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[4];
                sqlParams[0] = new SqlParameter("@RMItemId", RMItemId);
                sqlParams[1] = new SqlParameter("@Name", Name);
                sqlParams[2] = new SqlParameter("@CompanyID", CompanyID);
                sqlParams[3] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
                sqlParams[3].Direction = ParameterDirection.ReturnValue;
                SQLDBUtil.ExecuteNonQuery("HR_InsUpd_ReimburseItems", sqlParams);
                return Convert.ToInt32(sqlParams[3].Value);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static int InsUpdEmpPenalties(int RMItemId, string Name, int CompanyID)
        {
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[4];
                sqlParams[0] = new SqlParameter("@RMItemId", RMItemId);
                sqlParams[1] = new SqlParameter("@Name", Name);
                sqlParams[2] = new SqlParameter("@CompanyID", CompanyID);
                sqlParams[3] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
                sqlParams[3].Direction = ParameterDirection.ReturnValue;
                SQLDBUtil.ExecuteNonQuery("HR_InsUpd_EmpPenalties", sqlParams);
                return Convert.ToInt32(sqlParams[3].Value);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static DataSet GetReimbursementItemsList()
        {
            return SQLDBUtil.ExecuteDataset("HR_Get_ReimburseItemsList");
        }
        public static DataSet GetEmployeePenalties()
        {
            return SQLDBUtil.ExecuteDataset("HR_Get_EmpPenalties");
        }
        public static DataSet GetEmpPenaltiesItemsListByPaging(HRCommon objHrCommon)
        {
            SqlParameter[] sqlParams = new SqlParameter[4];
            sqlParams[0] = new SqlParameter("@CurrentPage", objHrCommon.CurrentPage);
            sqlParams[1] = new SqlParameter("@PageSize", objHrCommon.PageSize);
            sqlParams[2] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
            sqlParams[2].Direction = ParameterDirection.ReturnValue;
            sqlParams[3] = new SqlParameter("@NoofRecords", System.Data.SqlDbType.Int);
            sqlParams[3].Direction = ParameterDirection.Output;
            
            DataSet ds= SQLDBUtil.ExecuteDataset("HR_Get_EmpPenaltiesListByPaging", sqlParams);
           objHrCommon.NoofRecords= (int)sqlParams[3].Value;
            objHrCommon.TotalPages = (int)sqlParams[2].Value;
            return ds;

        }
        public static DataSet GetReimbursementItemsListByPaging(HRCommon objHrCommon)
        {
            SqlParameter[] sqlParams = new SqlParameter[4];
            sqlParams[0] = new SqlParameter("@CurrentPage", objHrCommon.CurrentPage);
            sqlParams[1] = new SqlParameter("@PageSize", objHrCommon.PageSize);
            sqlParams[2] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
            sqlParams[2].Direction = ParameterDirection.ReturnValue;
            sqlParams[3] = new SqlParameter("@NoofRecords", System.Data.SqlDbType.Int);
            sqlParams[3].Direction = ParameterDirection.Output;
            
            DataSet ds= SQLDBUtil.ExecuteDataset("HR_Get_ReimburseItemsListByPaging", sqlParams);
           objHrCommon.NoofRecords= (int)sqlParams[3].Value;
            objHrCommon.TotalPages = (int)sqlParams[2].Value;
            return ds;

        }

        public static DataSet GetEmpPenaltiesDetails(int RMItemId)
        {
            try
            {
                
                DataSet ds= SQLDBUtil.ExecuteDataset("HR_Get_EmpPenaltiesDetails", new SqlParameter[] { new SqlParameter("@RMItemId", RMItemId) });
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet GetReimbursementItemsDetails(int RMItemId)
        {
            try
            {
                
                DataSet ds= SQLDBUtil.ExecuteDataset("HR_Get_ReimburseItemsDetails", new SqlParameter[] { new SqlParameter("@RMItemId", RMItemId) });
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region CompanyContributionItems

        public static void InsUpdateCoyContributionItems(int AllowId, string Name, string LongName, int Access, int CompanyID)
        {
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[5];
                sqlParams[0] = new SqlParameter("@ContItemid", AllowId);
                sqlParams[1] = new SqlParameter("@Name", Name);
                sqlParams[2] = new SqlParameter("@LongName", LongName);
                sqlParams[3] = new SqlParameter("@Access", Access);
                sqlParams[4] = new SqlParameter("@CompanyID", CompanyID);
                SQLDBUtil.ExecuteNonQuery("HR_InsUpd_CoyContributionItems", sqlParams);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static int InsUpdCoyContributionItems(int AllowId, string Name, string LongName, int Access, int CompanyID)
        {
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[6];
                sqlParams[0] = new SqlParameter("@ContItemid", AllowId);
                sqlParams[1] = new SqlParameter("@Name", Name);
                sqlParams[2] = new SqlParameter("@LongName", LongName);
                sqlParams[3] = new SqlParameter("@Access", Access);
                sqlParams[4] = new SqlParameter("@CompanyID", CompanyID);
                sqlParams[5] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
                sqlParams[5].Direction = ParameterDirection.ReturnValue;
                SQLDBUtil.ExecuteNonQuery("HR_InsUpd_CoyContributionItems", sqlParams);
                return Convert.ToInt32(sqlParams[5].Value);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static DataSet GetCoyContributionItemsList(int AccessID)
        {
            return SQLDBUtil.ExecuteDataset("HR_Get_CoyContributionItemsList", new SqlParameter[] { new SqlParameter("@AccessID", AccessID) });
        }
        public static DataSet GetEmpCoyContributionItemsList(int EmpId, int EmpSalID)
        {
            return SQLDBUtil.ExecuteDataset("HR_Get_EmpCoyContributionItemsList", new SqlParameter[] { new SqlParameter("@EmpId", EmpId), new SqlParameter("@EmpSalID", EmpSalID) });
        }

        public static DataSet GetEmpContributionByContribution(int ContriID)
        {
            return SQLDBUtil.ExecuteDataset("HR_GetEmpContributionByContribution", new SqlParameter[] { new SqlParameter("@ContriID", ContriID) });
        }

        public static DataSet GetCoyContributionItemsDetails(int ContItemid)
        {
            try
            {
                
                DataSet ds= SQLDBUtil.ExecuteDataset("HR_Get_CoyContributionItemsDetails", new SqlParameter[] { new SqlParameter("@ContItemid", ContItemid) });
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void CoyContributionItemsDisplayoder(int ID, int Order)
        {
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[2];
                sqlParams[0] = new SqlParameter("@ID", ID);
                sqlParams[1] = new SqlParameter("@Order", Order);
                SQLDBUtil.ExecuteNonQuery("HR_CoyContributionItemsDisplayoder", sqlParams);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region CompanyContribution

        public static int InsUpdateCoyContribution(int CCIId, int Itemid, int FinYearId, int WagesID, double ContrRate, double WageLimit, double AmountLimit)
        {
            
            SqlParameter[] sqlParams = new SqlParameter[8];
            sqlParams[0] = new SqlParameter("@CCIId", CCIId);
            sqlParams[1] = new SqlParameter("@Itemid", Itemid);
            sqlParams[2] = new SqlParameter("@FinYearId", FinYearId);
            sqlParams[3] = new SqlParameter("@WagesID", SqlDbType.Int);
            sqlParams[4] = new SqlParameter("@ContrRate", ContrRate);
            sqlParams[5] = new SqlParameter("@WageLimit", WageLimit);
            if (WagesID != -1)
                sqlParams[3].Value = WagesID;
            sqlParams[6] = new SqlParameter("@AmountLimit", AmountLimit);
            sqlParams[7] = new SqlParameter("Returnvalue", System.Data.SqlDbType.Int);
            sqlParams[7].Direction = ParameterDirection.ReturnValue;
            SQLDBUtil.ExecuteNonQuery("HR_InsUpd_CoyContribution", sqlParams);
            return Convert.ToInt32(sqlParams[7].Value);
        }

        public static DataSet GetCoyContributionList(int FinYearID)
        {
            return SQLDBUtil.ExecuteDataset("HR_Get_CoyContributionList", new SqlParameter[] { new SqlParameter("@FinYearID", FinYearID) });
        }
        public static DataSet GetCoyContributionDetails(int CCIId)
        {
            try
            {
                
                DataSet ds= SQLDBUtil.ExecuteDataset("HR_Get_CoyContributionDetails", new SqlParameter[] { new SqlParameter("@CCIId", CCIId) });
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void CoyContributionDisplayoder(int ID, int Order)
        {
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[2];
                sqlParams[0] = new SqlParameter("@ID", ID);
                sqlParams[1] = new SqlParameter("@Order", Order);
                SQLDBUtil.ExecuteNonQuery("HR_CoyContributionDisplayoder", sqlParams);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion



        #region DeductStatutory

        public static void InsUpdateDeductStatutory(int DedID, int Itemid, int FinYearId, int WagesID, double ContrRate, double WageLimit, double AmountLimit)
        {
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[7];
                sqlParams[0] = new SqlParameter("@DedID", DedID);
                sqlParams[1] = new SqlParameter("@Itemid", Itemid);
                sqlParams[2] = new SqlParameter("@FinYearId", FinYearId);
                sqlParams[3] = new SqlParameter("@WagesID", SqlDbType.Int);
                if (WagesID != -1)
                    sqlParams[3].Value = WagesID;
                sqlParams[4] = new SqlParameter("@ContrRate", ContrRate);
                sqlParams[5] = new SqlParameter("@WageLimit", WageLimit);
                sqlParams[6] = new SqlParameter("@AmountLimit", AmountLimit);

                SQLDBUtil.ExecuteNonQuery("HR_InsUpd_DeductStatutory", sqlParams);
            }
            catch (Exception e)
            {

            }
        }

        public static int InsUpdDeductStatutory(int DedID, int Itemid, int FinYearId, int WagesID, double ContrRate, double WageLimit, double AmountLimit)
        {
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[8];
                sqlParams[0] = new SqlParameter("@DedID", DedID);
                sqlParams[1] = new SqlParameter("@Itemid", Itemid);
                sqlParams[2] = new SqlParameter("@FinYearId", FinYearId);
                sqlParams[3] = new SqlParameter("@WagesID", SqlDbType.Int);
                if (WagesID != -1)
                    sqlParams[3].Value = WagesID;
                sqlParams[4] = new SqlParameter("@ContrRate", ContrRate);
                sqlParams[5] = new SqlParameter("@WageLimit", WageLimit);
                sqlParams[6] = new SqlParameter("@AmountLimit", AmountLimit);

                sqlParams[7] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
                sqlParams[7].Direction = ParameterDirection.ReturnValue;

                SQLDBUtil.ExecuteNonQuery("HR_InsUpd_DeductStatutory", sqlParams);
                return Convert.ToInt32(sqlParams[7].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static DataSet GetDeductStatutoryList(int FinYearID)
        {
            return SQLDBUtil.ExecuteDataset("HR_Get_DeductStatutoryList", new SqlParameter[] { new SqlParameter("@FinYearID", FinYearID) });
        }

        public static DataSet GetEmpDeductStatutoryList(int EmpId, int EmpSalID)
        {
            return SQLDBUtil.ExecuteDataset("HR_Get_EmpDeductionItemsList", new SqlParameter[] { new SqlParameter("@EmpId", EmpId), new SqlParameter("@EmpSalID", EmpSalID) });
        }

        public static DataSet GetEmpDeductionByContribution(int ContriID)
        {
            return SQLDBUtil.ExecuteDataset("HR_GetEmpDeductionByContribution", new SqlParameter[] { new SqlParameter("@ContriID", ContriID) });
        }

        public static DataSet GetDeductStatutoryDetails(int DedID)
        {
            try
            {
                
                DataSet ds= SQLDBUtil.ExecuteDataset("HR_Get_DeductStatutoryDetails", new SqlParameter[] { new SqlParameter("@DedID", DedID) });
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region DeductProfTax

       
        public static int InsUpdateDeductProfTax(int PTId, int StateId, int FinYearId, int WagesID, double AmtMin, double? AmtMax, double Amount)
        {
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[8];
                sqlParams[0] = new SqlParameter("@PTId", PTId);
                sqlParams[1] = new SqlParameter("@StateId", StateId);
                sqlParams[2] = new SqlParameter("@FinYearId", FinYearId);
                sqlParams[3] = new SqlParameter("@WagesID", SqlDbType.Int);
                if (WagesID != -1)
                    sqlParams[3].Value = WagesID;
                sqlParams[4] = new SqlParameter("@AmtMin", AmtMin);
                sqlParams[5] = new SqlParameter("@AmtMax", SqlDbType.Decimal);
                if (AmtMax != null)
                    sqlParams[5].Value = AmtMax;
                sqlParams[6] = new SqlParameter("@Amount", Amount);
                sqlParams[7] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
                sqlParams[7].Direction = ParameterDirection.ReturnValue;
                SQLDBUtil.ExecuteNonQuery("HR_InsUpd_DeductProfTax", sqlParams);
                return Convert.ToInt32(sqlParams[7].Value);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static DataSet GetDeductProfTaxList(int FinYearID)
        {
            return SQLDBUtil.ExecuteDataset("HR_Get_DeductProfTaxList", new SqlParameter[] { new SqlParameter("@FinYearID", FinYearID) });
        }
        public static DataSet GetDeductProfTaxDetails(int PTId)
        {
            try
            {
                
                DataSet ds= SQLDBUtil.ExecuteDataset("HR_Get_DeductProfTaxDetails", new SqlParameter[] { new SqlParameter("@PTId", PTId) });
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region  IT_Sections
        public static void InsUpdateIT_Sections(int SectionID, string SectionName, double Sectionlimit, int AssYearId)
        {
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[4];
                sqlParams[0] = new SqlParameter("@SectionID", SectionID);
                sqlParams[1] = new SqlParameter("@SectionName", SectionName);
                sqlParams[2] = new SqlParameter("@Sectionlimit", Sectionlimit);
                sqlParams[3] = new SqlParameter("@AssYearId", AssYearId);
                SQLDBUtil.ExecuteNonQuery("HR_InsUpd_IT_Sections", sqlParams);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static DataSet GetIT_SectionsList(int FinYearID)
        {
            return SQLDBUtil.ExecuteDataset("HR_Get_IT_SectionsList", new SqlParameter[] { new SqlParameter("@FinYearID", FinYearID) });
        }
        public static DataSet GetIT_SectionsDetails(int SectionID)
        {
            try
            {
                
                DataSet ds= SQLDBUtil.ExecuteDataset("HR_Get_IT_SectionsDetails", new SqlParameter[] { new SqlParameter("@SectionID", SectionID) });
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        #endregion

        #region  IT_Deductions
        public static int InsUpdateIT_Deductions(int DedID, int EmpID, int SectionID, string ItemName, string Proof, double Amount, int AssYearId)
        {
            try
            {

                SqlParameter[] sqlParams = new SqlParameter[7];
                sqlParams[0] = new SqlParameter("@DedID", DedID);
                sqlParams[0].Direction = ParameterDirection.InputOutput;
                sqlParams[1] = new SqlParameter("@EmpID", EmpID);
                sqlParams[2] = new SqlParameter("@SectionID", SectionID);
                sqlParams[3] = new SqlParameter("@ItemName", ItemName);
                sqlParams[4] = new SqlParameter("@Proof", Proof);
                sqlParams[5] = new SqlParameter("@Amount", Amount);
                sqlParams[6] = new SqlParameter("@AssYearId", AssYearId);
                SQLDBUtil.ExecuteNonQuery("HR_InsUpd_IT_Deductions", sqlParams);
                return DedID = Convert.ToInt32(sqlParams[0].Value);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static DataSet GetIT_DeductionsList(int EmpID, int FinYearID)
        {
            
            DataSet ds= SQLDBUtil.ExecuteDataset("HR_Get_IT_DeductionsList", new SqlParameter[] { new SqlParameter("@EmpID", EmpID), new SqlParameter("@FinYearID", FinYearID) });
            return ds;

        }
        public static DataSet GetIT_DeductionsDetails(int DedID)
        {
            try
            {
                
                DataSet ds= SQLDBUtil.ExecuteDataset("HR_Get_IT_DeductionsDetails", new SqlParameter[] { new SqlParameter("@DedID", DedID) });
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region  IT_Savings
        public static int InsUpdateIT_Savings(int DedID, int EmpID, int SectionID, string ItemName, double Amount, int AssYearId, string Proof, int UserID)
        {
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[8];
                sqlParams[0] = new SqlParameter("@ITSID", DedID);
                sqlParams[0].Direction = ParameterDirection.InputOutput;
                sqlParams[1] = new SqlParameter("@EmpID", EmpID);
                sqlParams[2] = new SqlParameter("@SectionID", SectionID);
                sqlParams[3] = new SqlParameter("@ItemName", ItemName);
                sqlParams[4] = new SqlParameter("@Amount", Amount);
                sqlParams[5] = new SqlParameter("@AssYearId", AssYearId);
                sqlParams[6] = new SqlParameter("@Proof", Proof);
                sqlParams[7] = new SqlParameter("@UserID", UserID);

                SQLDBUtil.ExecuteNonQuery("HR_InsUpd_IT_Savings", sqlParams);
                return DedID = Convert.ToInt32(sqlParams[0].Value);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static DataSet GetIT_SavingsList(int? EmpID, int Year)
        {
            
            DataSet ds= SQLDBUtil.ExecuteDataset("HR_Get_IT_SavingsList", new SqlParameter[] { new SqlParameter("@EmpID", EmpID), new SqlParameter("@AssYearId", Year) });
            return ds;

        }
        public static DataSet GetIT_SavingsDetails(int ITSID)
        {
            try
            {
                
                DataSet ds= SQLDBUtil.ExecuteDataset("HR_Get_IT_SavingsDetails", new SqlParameter[] { new SqlParameter("@ITSID", ITSID) });
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        #endregion

        #region PaySlip

        public static DataSet GetPaySLIP(int EmpId, DateTime Date)
        {
            try
            {
                

                DataSet ds= SQLDBUtil.ExecuteDataset("HR_GetPaySLIP", new SqlParameter[] { new SqlParameter("@EmpId", EmpId), new SqlParameter("@Date", Date), new SqlParameter("@Formtype", "Salaries") });

                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet GetPaySLIP_CTH(int EmpId, DateTime Date)
        {
            try
            {
                

                DataSet ds= SQLDBUtil.ExecuteDataset("HR_GetPaySLIP_CTH", new SqlParameter[] { new SqlParameter("@EmpId", EmpId), new SqlParameter("@Date", Date) });

                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region AssessmentYear
        public static int InsUpdateAssessmentYear(int? AssYearId, DateTime FromDate, DateTime TODate, string AssessmentYear)
        {
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[5];
                sqlParams[0] = new SqlParameter("@AssYearID", AssYearId);
                sqlParams[1] = new SqlParameter("@FrmDate", FromDate);
                sqlParams[2] = new SqlParameter("@ToDate", TODate);
                sqlParams[3] = new SqlParameter("@AssYear", AssessmentYear);
                sqlParams[4] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
                sqlParams[4].Direction = ParameterDirection.ReturnValue;
                SQLDBUtil.ExecuteNonQuery("HR_InsUpdAssessmentYear", sqlParams);
                return Convert.ToInt32(sqlParams[4].Value);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static int InsUpdate_Encashment_AL(HRCommon objHrCommon, int Id, int empid, DateTime TODate, int lop2, int Aal, string empname, int lop2v, int occuranceoop, DateTime ActionDt)
        {
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[13];
                sqlParams[0] = new SqlParameter("@id", Id);
                sqlParams[1] = new SqlParameter("@empid", empid);
                sqlParams[2] = new SqlParameter("@lvrd", TODate);
                sqlParams[3] = new SqlParameter("@lopt2", lop2);
                sqlParams[4] = new SqlParameter("@Aal", Aal);
                sqlParams[5] = new SqlParameter("@empname", empname);

                sqlParams[6] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
                sqlParams[6].Direction = ParameterDirection.ReturnValue;
                sqlParams[7] = new SqlParameter("@CurrentPage", objHrCommon.CurrentPage);
                sqlParams[8] = new SqlParameter("@PageSize", objHrCommon.PageSize);
                sqlParams[9] = new SqlParameter("@NoofRecords", objHrCommon.NoofRecords);
                sqlParams[10] = new SqlParameter("@lop2v", lop2v);
                sqlParams[11] = new SqlParameter("@occuranceoop", occuranceoop);
                sqlParams[12] = new SqlParameter("@ActionDt", ActionDt);
                SQLDBUtil.ExecuteNonQuery("USP_HMS_Encashment_AL_Insert_Update_Delete", sqlParams);

                return Convert.ToInt32(sqlParams[6].Value);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static DataSet InsUpdate_Encashment_AL_Grid(HRCommon objHrCommon,int Id, int empid, DateTime TODate, int lop2, int Aal, string empname)
        {
            try
            {
                if (Id != 5) {
                    
                    DataSet ds= SQLDBUtil.ExecuteDataset("USP_HMS_Encashment_AL_Insert_Update_Delete", new SqlParameter[] 
                     { 
                    new SqlParameter("@id", Id), 
                    new SqlParameter("@empid", empid), 
                     new SqlParameter("@lvrd", 0), 
                    new SqlParameter("@lopt2", lop2), 
                     new SqlParameter("@Aal", Aal), 
                     new SqlParameter("@empname", empname),
                    new SqlParameter("@CurrentPage", objHrCommon.CurrentPage), 
                     new SqlParameter("@PageSize", objHrCommon.PageSize), 
                    new SqlParameter("@NoofRecords", objHrCommon.NoofRecords), 
                     });
                    return ds;
                }
                else { 

                SqlParameter[] sqlParams = new SqlParameter[10];
                sqlParams[0] = new SqlParameter("@CurrentPage", objHrCommon.CurrentPage);
                sqlParams[1] = new SqlParameter("@PageSize", objHrCommon.PageSize);
                sqlParams[2] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
                sqlParams[2].Direction = ParameterDirection.ReturnValue;
                sqlParams[3] = new SqlParameter("@NoofRecords", System.Data.SqlDbType.Int);
                sqlParams[3].Direction = ParameterDirection.Output;
                sqlParams[4] = new SqlParameter("@id", Id);
                sqlParams[5] = new SqlParameter("@empid", empid);
                sqlParams[6] = new SqlParameter("@lvrd", 0);
                sqlParams[7] = new SqlParameter("@lopt2", lop2);
                sqlParams[8] = new SqlParameter("@Aal", Aal);
                sqlParams[9] = new SqlParameter("@empname", empname);
                
                DataSet ds= SQLDBUtil.ExecuteDataset("USP_HMS_Encashment_AL_Insert_Update_Delete", sqlParams);
               objHrCommon.NoofRecords= (int)sqlParams[3].Value;
                objHrCommon.TotalPages = (int)sqlParams[2].Value;
                return ds;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet GetAssessmentYearList()
        {
            return SQLDBUtil.ExecuteDataset("HR_Get_AssessmentYearList");
        }
        public static DataSet HR_Get_AssessmentYearList_New(int assigyearid)
        {
            SqlParameter[] p = new SqlParameter[1];
            p[0] = new SqlParameter("@AssYearId", assigyearid);
           // DataSet ds = SqlHelper.ExecuteDataset("HR_Get_AssessmentYearList_New", p);
            return SQLDBUtil.ExecuteDataset("HR_Get_AssessmentYearList_New",p);
        }
        public static DataSet GetAssessmentYearDetails(int AssYearId)
        {
            try
            {
                
                DataSet ds= SQLDBUtil.ExecuteDataset("HR_Get_AssessmentYearDetails", new SqlParameter[] { new SqlParameter("@AssYearId", AssYearId) });
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region AssesseType
        public static void InsUpdateAssesseType(int AssesseId, string Assesse, bool SrCitezen, char Gender, int Age)
        {
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[5];
                sqlParams[0] = new SqlParameter("@AssesseId", AssesseId);
                sqlParams[1] = new SqlParameter("@Assesse", Assesse);
                sqlParams[2] = new SqlParameter("@SrCitezen", SrCitezen);
                sqlParams[3] = new SqlParameter("@Gender", Gender);
                sqlParams[4] = new SqlParameter("@Age", Age);

                SQLDBUtil.ExecuteNonQuery("HR_InsUpd_AssesseType", sqlParams);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static int InsUpdAssessType(int AssesseId, string Assesse, bool SrCitezen, char Gender, int Age)
        {
            try
            {
                SqlParameter[] parm = new SqlParameter[6];
                parm[0] = new SqlParameter("@AssesseId", AssesseId);
                parm[1] = new SqlParameter("@Assesse", Assesse);
                parm[2] = new SqlParameter("@SrCitezen", SrCitezen);
                parm[3] = new SqlParameter("@Gender", Gender);
                parm[4] = new SqlParameter("@Age", Age);
                parm[5] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
                parm[5].Direction = ParameterDirection.ReturnValue;
                SQLDBUtil.ExecuteDataset("HR_InsUpd_AssesseType", parm);
                return Convert.ToInt32(parm[5].Value);

            }
            catch (Exception)
            {

                throw;
            }
        }

        public static DataSet GetAssessmentTypeList()
        {
            return SQLDBUtil.ExecuteDataset("HR_Get_AssesseTypeList");
        }
        public static DataSet GetAssessmentTypeDetails(int AssesseId)
        {
            try
            {
                
                DataSet ds= SQLDBUtil.ExecuteDataset("HR_Get_AssesseTypeDetails", new SqlParameter[] { new SqlParameter("@AssesseId", AssesseId) });
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region TDS

        public static int InsUpdTDS(int TDSId, int AssesseId, int AssYearId, double RangeFrom, double RangeTo, double Rate)
        {
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[7];
                sqlParams[0] = new SqlParameter("@TDSId", TDSId);
                sqlParams[1] = new SqlParameter("@AssesseId", AssesseId);
                sqlParams[2] = new SqlParameter("@AssYearId", AssYearId);
                sqlParams[3] = new SqlParameter("@RangeFrom", RangeFrom);
                sqlParams[4] = new SqlParameter("@RangeTo", RangeTo);
                sqlParams[5] = new SqlParameter("@Rate", Rate);
                sqlParams[6] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
                sqlParams[6].Direction = ParameterDirection.ReturnValue;
                SQLDBUtil.ExecuteNonQuery("HR_InsUpd_TDS", sqlParams);
                return Convert.ToInt32(sqlParams[6].Value);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static void InsUpdateTDS(int TDSId, int AssesseId, int AssYearId, double RangeFrom, double RangeTo, double Rate)
        {
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[6];
                sqlParams[0] = new SqlParameter("@TDSId", TDSId);
                sqlParams[1] = new SqlParameter("@AssesseId", AssesseId);
                sqlParams[2] = new SqlParameter("@AssYearId", AssYearId);
                sqlParams[3] = new SqlParameter("@RangeFrom", RangeFrom);
                sqlParams[4] = new SqlParameter("@RangeTo", RangeTo);
                sqlParams[5] = new SqlParameter("@Rate", Rate);
                SQLDBUtil.ExecuteNonQuery("HR_InsUpd_TDS", sqlParams);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static DataSet GetTDSList(int FinYearID)
        {
            return SQLDBUtil.ExecuteDataset("HR_Get_TDSList", new SqlParameter[] { new SqlParameter("@FinYearID", FinYearID) });
        }
        public static DataSet GetTDSDetails(int TDSId)
        {
            try
            {
                
                DataSet ds= SQLDBUtil.ExecuteDataset("HR_Get_TDSDetails", new SqlParameter[] { new SqlParameter("@TDSId", TDSId) });
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region SrCitezen



        public static void InsUpdateSrCitezen(int AssYearId, char Gender, int AgeFrom, int SrCitizenID)
        {
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[4];
                sqlParams[0] = new SqlParameter("@AssYearId", AssYearId);
                sqlParams[1] = new SqlParameter("@Gender", Gender);
                sqlParams[2] = new SqlParameter("@AgeFrom", AgeFrom);
                sqlParams[3] = new SqlParameter("@SrCitizenID", SrCitizenID);


                SQLDBUtil.ExecuteNonQuery("HR_InsUpd_SrCitezen", sqlParams);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static DataSet GetSrCitezenList()
        {
            return SQLDBUtil.ExecuteDataset("HR_Get_SrCitezenList");
        }
        public static DataSet GetSrCitezenDetails(int SrCitizenID)
        {
            try
            {
                
                DataSet ds= SQLDBUtil.ExecuteDataset("HR_Get_SrCitezenDetails", new SqlParameter[] { new SqlParameter("@SrCitizenID", SrCitizenID) });
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion



        #region NonCTCComponents

        public static int InsUpdateNonCTCComponents(int CompID, string Name, string LongName, int CompanyID, int Yearly, int NoofYears)
        {
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[7];
                sqlParams[0] = new SqlParameter("@CompID", CompID);
                sqlParams[1] = new SqlParameter("@Name", Name);
                sqlParams[2] = new SqlParameter("@LongName", LongName);
                sqlParams[3] = new SqlParameter("@CompanyID", CompanyID);
                sqlParams[4] = new SqlParameter("@Yearly", Yearly);
                sqlParams[5] = new SqlParameter("@NoofYears", NoofYears);
                sqlParams[6] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
                sqlParams[6].Direction = ParameterDirection.ReturnValue;
                SQLDBUtil.ExecuteNonQuery("HR_InsUpd_NonCTCComponents1", sqlParams);
                return Convert.ToInt32(sqlParams[6].Value);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static DataSet GetNonCTCComponentsList()
        {
            return SQLDBUtil.ExecuteDataset("HR_Get_NonCTCComponentsList");
        }


        public static DataSet GetNonCTCComponentsListByPaging(HRCommon objHrCommon)
        {
            SqlParameter[] sqlParams = new SqlParameter[4];
            sqlParams[0] = new SqlParameter("@CurrentPage", objHrCommon.CurrentPage);
            sqlParams[1] = new SqlParameter("@PageSize", objHrCommon.PageSize);
            sqlParams[2] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
            sqlParams[2].Direction = ParameterDirection.ReturnValue;
            sqlParams[3] = new SqlParameter("@NoofRecords", System.Data.SqlDbType.Int);
            sqlParams[3].Direction = ParameterDirection.Output;
            
            DataSet ds= SQLDBUtil.ExecuteDataset("HR_Get_NonCTCComponentsListByPaging", sqlParams);
           objHrCommon.NoofRecords= (int)sqlParams[3].Value;
            objHrCommon.TotalPages = (int)sqlParams[2].Value;
            return ds;

        }
        public static DataSet GetNonCTCComponentsDetails(int CompID)
        {
            try
            {
                
                DataSet ds= SQLDBUtil.ExecuteDataset("HR_Get_NonCTCComponents", new SqlParameter[] { new SqlParameter("@CompID", CompID) });
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public static DataSet GetEmpNonCTCComponentsList(int EmpId, int EmpSalID)
        {
            return SQLDBUtil.ExecuteDataset("HR_EmpEmpNonCTCComponentsList", new SqlParameter[] { new SqlParameter("@EmpId", EmpId), new SqlParameter("@EmpSalID", EmpSalID) });
        }

        #endregion

        #region Gratuity

        public static DataSet GratuityListByPaging(int EmpId, HRCommon objHrCommon)
        {
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[5];
                sqlParams[0] = new SqlParameter("@EmpId", EmpId);
                sqlParams[1] = new SqlParameter("@CurrentPage", objHrCommon.CurrentPage);
                sqlParams[2] = new SqlParameter("@PageSize", objHrCommon.PageSize);
                sqlParams[3] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
                sqlParams[3].Direction = ParameterDirection.ReturnValue;
                sqlParams[4] = new SqlParameter("@NoofRecords", System.Data.SqlDbType.Int);
                sqlParams[4].Direction = ParameterDirection.Output;
                
                DataSet ds= SQLDBUtil.ExecuteDataset("HR_GetGratuityByEmpID", sqlParams);
               objHrCommon.NoofRecords= (int)sqlParams[4].Value;
                objHrCommon.TotalPages = (int)sqlParams[3].Value;
                return ds;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static DataSet EmployeeGratuityListByPaging(HRCommon objHrCommon)
        {
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[4];
                sqlParams[0] = new SqlParameter("@CurrentPage", objHrCommon.CurrentPage);
                sqlParams[1] = new SqlParameter("@PageSize", objHrCommon.PageSize);
                sqlParams[2] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
                sqlParams[2].Direction = ParameterDirection.ReturnValue;
                sqlParams[3] = new SqlParameter("@NoofRecords", System.Data.SqlDbType.Int);
                sqlParams[3].Direction = ParameterDirection.Output;
                
                DataSet ds= SQLDBUtil.ExecuteDataset("HR_GetGratuity", sqlParams);
               objHrCommon.NoofRecords= (int)sqlParams[3].Value;
                objHrCommon.TotalPages = (int)sqlParams[2].Value;
                return ds;
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        #endregion Gratuity
    }
}