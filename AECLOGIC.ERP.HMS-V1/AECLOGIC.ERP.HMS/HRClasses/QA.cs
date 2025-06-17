using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Aeclogic.Common.DAL;

namespace AECLOGIC.ERP.HMS
{
    public class QA
    {

        public DataSet GetMMS_QA(Common objCommon, int? VendorId, int? PONo, int? GDN, DateTime? FromDate, DateTime? ToDate, int? WorkSite, int? Material, int? Vehicle)
        {
            try
            {
                if (GDN != null)
                {
                    objCommon.CurrentPage = 1;
                    objCommon.PageSize = 10;
                }
                SqlParameter[] sqlParams = new SqlParameter[12];
                sqlParams[0] = new SqlParameter("@CurrentPage", objCommon.CurrentPage);
                sqlParams[1] = new SqlParameter("@PageSize", objCommon.PageSize);
                sqlParams[2] = new SqlParameter("@ReturnValue", System.Data.SqlDbType.Int);
                sqlParams[2].Direction = ParameterDirection.ReturnValue;
                sqlParams[3] = new SqlParameter("@NoofRecords", System.Data.SqlDbType.Int);
                sqlParams[3].Direction = ParameterDirection.Output;
                sqlParams[4] = new SqlParameter("@VendorId", VendorId);
                sqlParams[5] = new SqlParameter("@PONo", PONo);
                sqlParams[6] = new SqlParameter("@GDNId", GDN);
                sqlParams[7] = new SqlParameter("@ToDate", ToDate);
                sqlParams[8] = new SqlParameter("@WorkSite", WorkSite);
                sqlParams[9] = new SqlParameter("@ResourceId", Material);
                sqlParams[10] = new SqlParameter("@VehId", Vehicle);
                sqlParams[11] = new SqlParameter("@FromDate", FromDate);

                DataSet ds = SQLDBUtil.ExecuteDataset("MMS_QA", sqlParams);
                objCommon.NoofRecords = (int)sqlParams[3].Value;
                objCommon.TotalPages = (int)sqlParams[2].Value;
                return ds;
            }
            catch (Exception e)
            {
                clsErrorLog.Log(e); throw e;
            }
        }

        public DataSet GetMMS_QAPOs(int GDNId)
        {
            try
            {
                return SQLDBUtil.ExecuteDataset("MMS_QAPOs", new SqlParameter[] { new SqlParameter("@GDNId", GDNId) });
            }
            catch (Exception e)
            {
                clsErrorLog.Log(e); throw e;
            }
        }
        public DataSet UpdateMMS_QACHECK(int GDNId)
        {
            try
            {
                return SQLDBUtil.ExecuteDataset("UpdateMMS_QACHECK", new SqlParameter[] { new SqlParameter("@GDNId", GDNId) });
            }
            catch (Exception e)
            {
                clsErrorLog.Log(e); throw e;
            }
        }
        public int InsUpdateQa(int ChkdBy, int GDNItemId, string CompComments, string Pass, double PassQty, int UserId)
        {
            try
            {
                SqlParameter[] sqlPrms = new SqlParameter[6];
                sqlPrms[0] = new SqlParameter("@ChkdBy", ChkdBy);
                sqlPrms[1] = new SqlParameter("@GDNItemId", GDNItemId);
                sqlPrms[2] = new SqlParameter("@CompComments", CompComments);
                sqlPrms[3] = new SqlParameter("@Pass", Pass);
                sqlPrms[4] = new SqlParameter("@PassQty", PassQty);
                sqlPrms[5] = new SqlParameter("@UserId ", UserId);

                sqlPrms[6] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
                sqlPrms[6].Direction = ParameterDirection.ReturnValue;
                SQLDBUtil.ExecuteNonQuery("MMS_QA_InsUpdate", sqlPrms);
                return Convert.ToInt32(sqlPrms[4].Value);
            }
            catch (Exception e)
            {
                clsErrorLog.Log(e); throw e;
            }
        }

        public int InsUpdateQaALL(int GDNId, int ChkdBy, int CreatedBy, decimal RcvdQty)
        {
            try
            {
                SqlParameter[] sqlPrms = new SqlParameter[4];
                sqlPrms[0] = new SqlParameter("@GDNId", GDNId);
                sqlPrms[1] = new SqlParameter("@ChkdBy", ChkdBy);
                sqlPrms[2] = new SqlParameter("@CreatedBy", CreatedBy);
                sqlPrms[3] = new SqlParameter("@RcvdQty", RcvdQty);

                sqlPrms[4] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
                sqlPrms[4].Direction = ParameterDirection.ReturnValue;
                SQLDBUtil.ExecuteNonQuery("MMS_QAALL", sqlPrms);
                return Convert.ToInt32(sqlPrms[4].Value);
            }
            catch (Exception e)
            {
                clsErrorLog.Log(e); throw e;
            }
        }
        public DataSet MMS_CompareQTY(int GDNId)
        {

            DataSet ds = SQLDBUtil.ExecuteDataset("MMS_CompareQTY", new SqlParameter[] { new SqlParameter("@GDNId", GDNId) });
            return ds;
        }


        public static DataSet MMS_GETGDNITEMID(int GDNId)
        {

            DataSet ds = SQLDBUtil.ExecuteDataset("MMS_GETGDNITEMID", new SqlParameter[] { new SqlParameter("@GDNId", GDNId) });
            return ds;
        }
        public static DataSet MMS_GetPOTypeId(int PoDetId)
        {

            DataSet ds = SQLDBUtil.ExecuteDataset("MMS_GetPOTypeId", new SqlParameter[] { new SqlParameter("@PoDetId", PoDetId) });
            return ds;
        }
        public static string MMS_CLOSEPO(string PONO, string Itemid)
        {

            try
            {
                SqlParameter[] sqlParams = new SqlParameter[2];
                sqlParams[0] = new SqlParameter("@PONO", PONO);
                sqlParams[1] = new SqlParameter("@Itemid", Itemid);
                SQLDBUtil.ExecuteNonQuery("MMS_CLOSEPO", sqlParams);
                return "";
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
