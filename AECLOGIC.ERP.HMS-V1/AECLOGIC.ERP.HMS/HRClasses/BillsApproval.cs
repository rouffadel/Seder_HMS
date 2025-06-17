using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Aeclogic.Common.DAL;
namespace DataAccessLayer
{
    public class BillsApproval
    {
        #region Generic Function
        #region	Get Web Config
        public static string getWebConfig(string strSectionName, string strItemName)
        {
            NameValueCollection nvc = (NameValueCollection)ConfigurationSettings.GetConfig(strSectionName);
            return nvc[strItemName];
        }
        #endregion
        #endregion

        public enum GoodsBillStatus { Inactive, Approvals, Approved, Rejected  };

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
            set { _NoofRecords= value; }
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

        private string _Username;
        private string _NewPassWord;
        private int _EmpID;

        public int EmpID
        {
            get { return _EmpID; }
            set { _EmpID = value; }
        }

        public string Username
        {
            get { return _Username; }
            set { _Username = value; }
        }

        public string NewPassWord
        {
            get { return _NewPassWord; }
            set { _NewPassWord = value; }
        }

        public DataSet GetMMS_BillsGoodsApproved(BillsApproval objCommon, int? VendorID, int? PONo, int? BillNo, int ModuleId,int id,int? WorkSiteID)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[10];
                param[0] = new SqlParameter("@ModuleId", 1);
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
               
                DataSet ds= SQLDBUtil.ExecuteDataset("MMS_BillsGoodsApproved", param);
                objCommon.NoofRecords = (int)param[3].Value;
                objCommon.TotalPages = (int)param[7].Value;
                return ds;
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public DataSet GetMMS_BillsGoodsRejected(BillsApproval objCommon, int? VendorID, int? PONo, int? BillNo, int ModuleId, int? WorkSiteID)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[9];
                param[0] = new SqlParameter("@ModuleId", 1);
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
               
                DataSet ds= SQLDBUtil.ExecuteDataset("MMS_BillsGoodsRejected", param);
                objCommon.NoofRecords= (int)param[3].Value;
                objCommon.TotalPages = (int)param[7].Value;
                return ds;
            }
            catch (Exception e)
            {
                throw e;
            }

        }


        public DataSet GetMMS_BillsGoodsApproval(BillsApproval objCommon, int? VendorID, int? PONo, int? BillNo, int ModuleId,int? WorkSiteID)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[9];
                param[0] = new SqlParameter("@ModuleId", 1);
                param[1] = new SqlParameter("@CurrentPage", objCommon.CurrentPage);
                param[2] = new SqlParameter("@PageSize", objCommon.PageSize);
                param[3] = new SqlParameter("@NoofRecords", System.Data.SqlDbType.Int);
                param[3].Direction = ParameterDirection.Output;
                if (VendorID!=0)
                param[4] = new SqlParameter("@VendorId", VendorID);
                else
                    param[4] = new SqlParameter("@VendorId", System.Data.SqlDbType.Int);
                
                param[5] = new SqlParameter("@PONo", PONo);
                param[6] = new SqlParameter("@BillNo", BillNo);
                param[7] = new SqlParameter("Returnvalue", System.Data.SqlDbType.Int);
                param[7].Direction = ParameterDirection.ReturnValue;
                if(WorkSiteID!=0)
                param[8] = new SqlParameter("@WorkSiteID", WorkSiteID);
                 else
                    param[8] = new SqlParameter("@WorkSiteID", System.Data.SqlDbType.Int);
         

               
                DataSet ds= SQLDBUtil.ExecuteDataset("MMS_BillsGoodsApproval", param);
                objCommon.NoofRecords = (int)param[3].Value;
                objCommon.TotalPages = (int)param[7].Value;
                return ds;
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public DataSet MMS_ChangeBillStatus(int? BillNo, int BillStatus, int EmpId)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[3];
                param[0] = new SqlParameter("@BillNo", BillNo);
                param[1] = new SqlParameter("@BillStatus", BillStatus);
                param[2] = new SqlParameter("@EmpId", EmpId);
                return SQLDBUtil.ExecuteDataset("MMS_ChangeBillStatus", param);
            }
            catch(Exception e)
            {
                    throw e;
            }
        }

        public DataSet MMS_ApproveBill(int BillNo, int CompanyId, int EmpId)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[3];
                param[0] = new SqlParameter("@BillNo", BillNo);
                param[1] = new SqlParameter("@CompanyId", CompanyId);
                param[2] = new SqlParameter("@EmpId", EmpId);
                return SQLDBUtil.ExecuteDataset("MMS_ApproveBill", param);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public DataSet GetMMS_SalesBillsApproval(BillsApproval objCommon, int? VendorID, int? SOId, int? BillNo, int ModuleId, int? type)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[9];
                param[0] = new SqlParameter("@ModuleId", ModuleId);
                param[1] = new SqlParameter("@CurrentPage", objCommon.CurrentPage);
                param[2] = new SqlParameter("@PageSize", objCommon.PageSize);
                param[3] = new SqlParameter("@NoofRecords", System.Data.SqlDbType.Int);
                param[3].Direction = ParameterDirection.Output;
                if (VendorID != 0)
                    param[4] = new SqlParameter("@VendorId", VendorID);
                else
                    param[4] = new SqlParameter("@VendorId", System.Data.SqlDbType.Int);
                param[5] = new SqlParameter("@SOId", SOId);
                param[6] = new SqlParameter("@BillNo", BillNo);
                param[7] = new SqlParameter("Returnvalue", System.Data.SqlDbType.Int);
                param[7].Direction = ParameterDirection.ReturnValue;
                param[8] = new SqlParameter("@type", type);

               
                DataSet ds= SQLDBUtil.ExecuteDataset("MMS_SalesBillsApproval", param);
                objCommon.NoofRecords= (int)param[3].Value;
                objCommon.TotalPages = (int)param[7].Value;
                return ds;
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public DataSet GetMMS_SalesBillsApproved(BillsApproval objCommon, int? VendorID, int? SOId, int? BillNo, int ModuleId, int id, int? type)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[10];
                param[0] = new SqlParameter("@ModuleId", ModuleId);
                param[1] = new SqlParameter("@CurrentPage", objCommon.CurrentPage);
                param[2] = new SqlParameter("@PageSize", objCommon.PageSize);
                param[3] = new SqlParameter("@NoofRecords", System.Data.SqlDbType.Int);
                param[3].Direction = ParameterDirection.Output;
                param[4] = new SqlParameter("@VendorId", VendorID);
                param[5] = new SqlParameter("@SOId", SOId);
                param[6] = new SqlParameter("@BillNo", BillNo);
                param[7] = new SqlParameter("Returnvalue", System.Data.SqlDbType.Int);
                param[7].Direction = ParameterDirection.ReturnValue;
                param[8] = new SqlParameter("@id", id);
                param[9] = new SqlParameter("@type", type);
               
                DataSet ds= SQLDBUtil.ExecuteDataset("MMS_SalesBillsApproved", param);
                objCommon.NoofRecords= (int)param[3].Value;
                objCommon.TotalPages = (int)param[7].Value;
                return ds;
            }
            catch (Exception e)
            {
                throw e;
            }

        }
        public int GetVendorId(int? Wo)
        {
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[1];
                sqlParams[0] = new SqlParameter("@Wo", Wo);
                return Convert.ToInt32(SQLDBUtil.ExecuteScalar("MMS_GetWorkOrderVendorId", sqlParams));
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public DataSet GetMMS_SalesBillsRejected(BillsApproval objCommon, int? VendorID, int? SOId, int? BillNo, int ModuleId, int? type)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[9];
                param[0] = new SqlParameter("@ModuleId", ModuleId);
                param[1] = new SqlParameter("@CurrentPage", objCommon.CurrentPage);
                param[2] = new SqlParameter("@PageSize", objCommon.PageSize);
                param[3] = new SqlParameter("@NoofRecords", System.Data.SqlDbType.Int);
                param[3].Direction = ParameterDirection.Output;
                param[4] = new SqlParameter("@VendorId", VendorID);
                param[5] = new SqlParameter("@SOId", SOId);
                param[6] = new SqlParameter("@BillNo", BillNo);
                param[7] = new SqlParameter("Returnvalue", System.Data.SqlDbType.Int);
                param[7].Direction = ParameterDirection.ReturnValue;
                param[8] = new SqlParameter("@type", type);

               
                DataSet ds= SQLDBUtil.ExecuteDataset("MMS_SalesBillsRejected", param);
                objCommon.NoofRecords= (int)param[3].Value;
                objCommon.TotalPages = (int)param[7].Value;
                return ds;
            }
            catch (Exception e)
            {
                throw e;
            }

        }
     
        public DataSet MMS_ApproveSOBill(int BillNo, int CompanyId, int EmpId)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[3];
                param[0] = new SqlParameter("@BillNo", BillNo);
                param[1] = new SqlParameter("@CompanyId", CompanyId);
                param[2] = new SqlParameter("@EmpId", EmpId);
                return SQLDBUtil.ExecuteDataset("MMS_ApproveSOBill", param);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
     
        public DataSet MMS_ChangeSOBillStatus(int? BillNo, int BillStatus, int EmpId)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[3];
                param[0] = new SqlParameter("@BillNo", BillNo);
                param[1] = new SqlParameter("@BillStatus", BillStatus);
                param[2] = new SqlParameter("@EmpId", EmpId);
                return SQLDBUtil.ExecuteDataset("MMS_ChangeSOBillStatus", param);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public DataSet MMS_BillGDNs(int BillNo)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@BillNo", BillNo);
                return SQLDBUtil.ExecuteDataset("MMS_Bills", param);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public int MMS_Bills_Upd_GdnItems(int GDNId, int GDNItemId, DateTime DATE, decimal DispQty, decimal InQty, decimal AcptQty, int TripSheet, int Worksite, int vendor,int WO, int WOVendor, int distance)
        {
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[12];
                sqlParams[0] = new SqlParameter("@GDNId", GDNId);
                sqlParams[1] = new SqlParameter("@GDNItemId", GDNItemId);
                sqlParams[2] = new SqlParameter("@DATE", DATE);
                sqlParams[3] = new SqlParameter("@DispQty", DispQty);
                sqlParams[4] = new SqlParameter("@InQty", InQty);
                sqlParams[5] = new SqlParameter("@AcptQty", AcptQty);
                sqlParams[6] = new SqlParameter("@TripSheet", TripSheet);
                sqlParams[7] = new SqlParameter("@Worksite", Worksite);
                sqlParams[8] = new SqlParameter("@vendorid", vendor);
                if (WOVendor != 0)
                    sqlParams[10] = new SqlParameter("@WOVendor", WOVendor);
                else
                    sqlParams[10] = new SqlParameter("@WOVendor", SqlDbType.Int);
                if (WO != 0)
                    sqlParams[9] = new SqlParameter("@WO", WO);
                else
                   sqlParams[9] = new SqlParameter("@WO", SqlDbType.Int);
                
                if (distance!=0)
                    sqlParams[11] = new SqlParameter("@Distance", distance);
                 else
                    sqlParams[11] = new SqlParameter("@Distance", distance);


                SQLDBUtil.ExecuteNonQuery("MMS_Bills_Upd_GdnItems", sqlParams);
                return 1;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        
        public DataSet MMS_Bills_GDN_Update(int GDNId)
        {
            try
            {
                return SQLDBUtil.ExecuteDataset("MMS_Bills_GDN_Update", new SqlParameter[] { new SqlParameter("@GDNId", GDNId) });
            }
            catch (Exception e)
            {
               throw e;
            }
        }
    }
}
