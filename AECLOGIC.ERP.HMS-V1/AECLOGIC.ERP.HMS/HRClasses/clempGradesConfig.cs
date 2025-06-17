using Aeclogic.Common.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace AECLOGIC.ERP.HMS.HRClasses
{
    public class clempGradesConfig
    {
        public static DataSet HR_GetEMPGradesDetails(bool status)
        {
            SqlParameter[] objParam = new SqlParameter[1];
            objParam[0] = new SqlParameter("@Status", status);
            return SQLDBUtil.ExecuteDataset("HR_GetEMPGradesDetails", objParam);
          
        }
        public static DataSet HR_GetEMPGradesDetails_Filter(bool status,int ID,int MedicalID)
        {
            SqlParameter[] objParam = new SqlParameter[3];
            objParam[0] = new SqlParameter("@Status", status);
            objParam[1] = new SqlParameter("@ID", ID);
            objParam[2] = new SqlParameter("@MeicalID", MedicalID);
            return SQLDBUtil.ExecuteDataset("HR_GetEMPGradesDetails_Filter", objParam);

        }
        public static DataSet HR_GetEMPGradesDetailsLE(bool status,int GID)
        {
            SqlParameter[] objParam = new SqlParameter[2];
            objParam[0] = new SqlParameter("@Status", status);
            objParam[1] = new SqlParameter("@GID",GID);
            return SQLDBUtil.ExecuteDataset("HR_GetEMPGradesDetailsLE", objParam);
           
        }
        public static DataSet HR_GetallEMPTradsddls()
        {
            
            return SQLDBUtil.ExecuteDataset("HR_GetallEMPTradsddls");
           
        }

        public static DataSet HR_GetEMPGradesDetailsforDDL()
        {

            return SQLDBUtil.ExecuteDataset("HR_GetEMPGradesDetailsforDDL");
           
        }
        
        
        public static DataSet HR_GetEMPGradesDetailsbyID(int ID)
        {
            
            SqlParameter[] objParam = new SqlParameter[1];
            objParam[0] = new SqlParameter("@ID", ID);
            return SQLDBUtil.ExecuteDataset("HR_GetEMPGradesDetailsbyID", objParam);
           
        }

        public static void HR_AddEditEMPGrades(int ID, int PositionCategory, string Grade, decimal SalaryFrom, decimal SalaryTo, 
            decimal HRA, decimal Transport,decimal Food,decimal Mobile,int AnnualLeave,int FamilyEntitlement,decimal Tickets
            , decimal ExitEntryVISA, int Medical, int MedicalNos, int status, int UID)
        {
            try
            {
                SqlParameter[] objParam = new SqlParameter[17];
                objParam[0] = new SqlParameter("@ID", ID);
                objParam[1] = new SqlParameter("@PositionCategory", PositionCategory);
                objParam[2] = new SqlParameter("@Grade", Grade);
                objParam[3] = new SqlParameter("@SalaryFrom", SalaryFrom);
                objParam[4] = new SqlParameter("@SalaryTo", SalaryTo);
                objParam[5] = new SqlParameter("@HRA", HRA);
                objParam[6] = new SqlParameter("@Transport", Transport);
                objParam[7] = new SqlParameter("@Food", Food);
                objParam[8] = new SqlParameter("@Mobile", Mobile);
                objParam[9] = new SqlParameter("@AnnualLeave", AnnualLeave);
                objParam[10] = new SqlParameter("@FamilyEntitlement", FamilyEntitlement);
                objParam[11] = new SqlParameter("@Tickets", Tickets);
                objParam[12] = new SqlParameter("@ExitEntryVISA", ExitEntryVISA);
                objParam[13] = new SqlParameter("@Medical", Medical);
                objParam[14] = new SqlParameter("@MedicalNos", MedicalNos);
                objParam[15] = new SqlParameter("@UID", UID);
                objParam[16] = new SqlParameter("@Status", status);    
                SQLDBUtil.ExecuteNonQuery("HR_AddEditEMPGrades", objParam);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

}