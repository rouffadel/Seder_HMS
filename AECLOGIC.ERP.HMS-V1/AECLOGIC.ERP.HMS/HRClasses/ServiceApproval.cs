using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Aeclogic.Common.DAL;

namespace DataAccessLayer
{
   public class ServiceApproval
    {
        private int _PageSize;
        private int _CurrentPage;
        private int _NoofRecords;
        private int _TotalPages;

        public int TotalPages
        {
            get { return _TotalPages; }
            set { _TotalPages = value; }
        }

        public int NoofRecords
        {
            get { return _NoofRecords; }
            set { _NoofRecords = value; }
        }

        public int CurrentPage
        {
            get { return _CurrentPage; }
            set { _CurrentPage = value; }
        }

        public int PageSize
        {
            get { return _PageSize; }
            set { _PageSize = value; }
        }




        public DataSet MMS_SRNBillsApproval(ServiceApproval objCommon, int? VendorID, int? PONo, int? BillNo, int ModuleId, int WorkSiteID, DateTime? FromDate, DateTime? ToDate)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[11];
                 if (ModuleId != 0)
                    param[0] = new SqlParameter("@ModuleId", ModuleId);
                else 
                {
                    param[0] = new SqlParameter("@ModuleId", System.Data.SqlDbType.Int); 
                }
                param[1] = new SqlParameter("@CurrentPage", objCommon.CurrentPage);
                param[2] = new SqlParameter("@PageSize", objCommon.PageSize);
                param[3] = new SqlParameter("@NoofRecords", System.Data.SqlDbType.Int);
                param[3].Direction = ParameterDirection.Output;
                if (VendorID != 0)
                    param[4] = new SqlParameter("@VendorId", VendorID);
                else
                    param[4] = new SqlParameter("@VendorId", System.Data.SqlDbType.Int);
                param[5] = new SqlParameter("@PONo", PONo);
                param[6] = new SqlParameter("@BillNo", BillNo);
                param[7] = new SqlParameter("Returnvalue", System.Data.SqlDbType.Int);
                param[7].Direction = ParameterDirection.ReturnValue;
                if (WorkSiteID != 0)
                    param[8] = new SqlParameter("@WorkSiteID", WorkSiteID);
                else
                    param[8] = new SqlParameter("@WorkSiteID", System.Data.SqlDbType.Int);
                param[9] = new SqlParameter("@ToDate", ToDate);
                param[10] = new SqlParameter("@FromDate", FromDate);

               
                DataSet ds= SQLDBUtil.ExecuteDataset("MMS_SRNBillsApproval", param);
                objCommon.NoofRecords= (int)param[3].Value;
                objCommon.TotalPages = (int)param[7].Value;
                return ds;
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public DataSet MMS_SRNBillsApproved(ServiceApproval objCommon, int? VendorID, int? PONo, int? BillNo, int ModuleId, int id, int WorkSiteID, DateTime? FromDate, DateTime? ToDate)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[12];
                if (ModuleId != 0)
                    param[0] = new SqlParameter("@ModuleId", ModuleId);
                else
                param[0] = new SqlParameter("@ModuleId", System.Data.SqlDbType.Int);
                param[1] = new SqlParameter("@CurrentPage", objCommon.CurrentPage);
                param[2] = new SqlParameter("@PageSize", objCommon.PageSize);
                param[3] = new SqlParameter("@NoofRecords", System.Data.SqlDbType.Int);
                param[3].Direction = ParameterDirection.Output;
                if (VendorID != 0)
                    param[4] = new SqlParameter("@VendorId", VendorID);
                else
                    param[4] = new SqlParameter("@VendorId", System.Data.SqlDbType.Int);
                param[5] = new SqlParameter("@PONo", PONo);
                param[6] = new SqlParameter("@BillNo", BillNo);
                param[7] = new SqlParameter("Returnvalue", System.Data.SqlDbType.Int);
                param[7].Direction = ParameterDirection.ReturnValue;
                param[8] = new SqlParameter("@id", id);
                if (WorkSiteID != 0)
                    param[9] = new SqlParameter("@WorkSiteID", WorkSiteID);
                else
                    param[9] = new SqlParameter("@WorkSiteID", System.Data.SqlDbType.Int);
                param[10] = new SqlParameter("@ToDate", ToDate);
                param[11] = new SqlParameter("@FromDate", FromDate);
               
                DataSet ds= SQLDBUtil.ExecuteDataset("MMS_SRNBillsApproved", param);
                objCommon.NoofRecords= (int)param[3].Value;
                objCommon.TotalPages = (int)param[7].Value;
                return ds;
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public DataSet MMS_SRNBillsRejected(ServiceApproval objCommon, int? VendorID, int? PONo, int? BillNo, int ModuleId, int WorkSiteID, DateTime? FromDate, DateTime? ToDate)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[11];
                if (ModuleId != 0)
                    param[0] = new SqlParameter("@ModuleId", ModuleId);
                else
                param[0] = new SqlParameter("@ModuleId", System.Data.SqlDbType.Int);
                param[1] = new SqlParameter("@CurrentPage", objCommon.CurrentPage);
                param[2] = new SqlParameter("@PageSize", objCommon.PageSize);
                param[3] = new SqlParameter("@NoofRecords", System.Data.SqlDbType.Int);
                param[3].Direction = ParameterDirection.Output;
                if (VendorID != 0)
                    param[4] = new SqlParameter("@VendorId", VendorID);
                else
                    param[4] = new SqlParameter("@VendorId", System.Data.SqlDbType.Int);
                param[5] = new SqlParameter("@PONo", PONo);
                param[6] = new SqlParameter("@BillNo", BillNo);
                param[7] = new SqlParameter("Returnvalue", System.Data.SqlDbType.Int);
                param[7].Direction = ParameterDirection.ReturnValue;
                if (WorkSiteID != 0)
                    param[8] = new SqlParameter("@WorkSiteID", WorkSiteID);
                else
                    param[8] = new SqlParameter("@WorkSiteID", System.Data.SqlDbType.Int);
                param[9] = new SqlParameter("@ToDate", ToDate);
                param[10] = new SqlParameter("@FromDate", FromDate);
               
                DataSet ds= SQLDBUtil.ExecuteDataset("[MMS_SRNBillsRejected]", param);
                objCommon.NoofRecords= (int)param[3].Value;
                objCommon.TotalPages = (int)param[7].Value;
                return ds;
            }
            catch (Exception e)
            {
                throw e;
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
   }

}
