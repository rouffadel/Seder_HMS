using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Linq;
using System.Collections.Generic;
using System.Globalization;
using DataAccessLayer;
//using SMSConfig;
using System.Data.SqlClient;
using System.Web.Caching;
using Aeclogic.Common.DAL;
namespace DataAccessLayer
{
    public class GDN
    {
        public static int UpdateGdnItemStatus(int GDNID, int GoodsStatus)
        {
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[2];
                sqlParams[0] = new SqlParameter("@GDNID", GDNID);
                sqlParams[1] = new SqlParameter("@GoodsStatus", GoodsStatus);
                return Convert.ToInt32(SQLDBUtil.ExecuteScalar("MMS_UpdateGDNStatus", sqlParams));
            }
            catch (Exception e)
            {
                clsErrorLog.Log(e);
                clsErrorLog.Log(e); throw e;
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
                clsErrorLog.Log(e); throw e;
            }
        }
        public DataSet GetGoodsItems()
        {
            try
            {
                DataSet ds= (DataSet)HttpContext.Current.Cache["MMS_GetGoodsItems"];
                if (ds== null)
                {
                    ds= SQLDBUtil.ExecuteDataset("MMS_GetGoodsItems");
                    HttpContext.Current.Cache.Insert("MMS_GetGoodsItems", ds, null, DateTime.Now.AddMinutes(1), Cache.NoSlidingExpiration);
                }
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet GetMMS_GDNEdit(Common objCommon, int? VendorId, int? PONo, int? GDN, DateTime? FromDate, DateTime? ToDate, int? WorkSite, int? Material, int? Vehicle)
        {
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[13];
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
                sqlParams[12] = new SqlParameter("@ModuleID", 1);
               
                DataSet ds= SQLDBUtil.ExecuteDataset("MMS_GDNEdit", sqlParams);
                objCommon.NoofRecords= (int)sqlParams[3].Value;
                objCommon.TotalPages = (int)sqlParams[2].Value;
                return ds;
            }
            catch (Exception e)
            {
                clsErrorLog.Log(e); throw e;
            }
        }


        public DataSet GetMMS_GDNSearch(Common objCommon, string SerachText)
        {
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[5];
                sqlParams[0] = new SqlParameter("@CurrentPage", objCommon.CurrentPage);
                sqlParams[1] = new SqlParameter("@PageSize", objCommon.PageSize);
                sqlParams[2] = new SqlParameter("@ReturnValue", System.Data.SqlDbType.Int);
                sqlParams[2].Direction = ParameterDirection.ReturnValue;
                sqlParams[3] = new SqlParameter("@NoofRecords", System.Data.SqlDbType.Int);
                sqlParams[3].Direction = ParameterDirection.Output;
                sqlParams[4] = new SqlParameter("@SerachText", SerachText);
               
                DataSet ds= SQLDBUtil.ExecuteDataset("MMS_GDNSearch", sqlParams);
                objCommon.NoofRecords= (int)sqlParams[3].Value;
                objCommon.TotalPages = (int)sqlParams[2].Value;
                return ds;
            }
            catch (Exception e)
            {
                clsErrorLog.Log(e); throw e;
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
                clsErrorLog.Log(e); throw e;
            }
        }
        public int GetMMS_GDNOffers(int PODetails)
        {
            try
            {

                return Convert.ToInt32(SQLDBUtil.ExecuteScalar("MMS_GDNOffers", new SqlParameter[] { new SqlParameter("@PODetails", PODetails) }));
            }
            catch (Exception e)
            {
                clsErrorLog.Log(e); throw e;
            }
        }
        public DataSet GetMMS_lstPODetails(int VendorID, int ProjectId, int PageId)
        {
            try
            {
                return SQLDBUtil.ExecuteDataset("MMS_lstPODetails", new SqlParameter[] { new SqlParameter("@VendorID", VendorID), new SqlParameter("@ProjectId", ProjectId), new SqlParameter("@PageId", PageId) });
            }
            catch (Exception e)
            {
                clsErrorLog.Log(e); throw e;
            }
        }

        public DataSet GetMMS_lstSSODetails(int VendorID, int ProjectId)
        {
            try
            {
                return SQLDBUtil.ExecuteDataset("MMS_lstSSODetails", new SqlParameter[] { new SqlParameter("@VendorID", VendorID), new SqlParameter("@ProjectId", ProjectId)});
            }
            catch (Exception e)
            {
                clsErrorLog.Log(e); throw e;
            }
        }
        public DataSet GetMMS_lstItemDetails(int ResourceID, int PageId)
        {
            try
            {
                return SQLDBUtil.ExecuteDataset("MMS_lstItemDetails", new SqlParameter[] { new SqlParameter("@ResourceID", ResourceID), new SqlParameter("@PageId", PageId) });
            }
            catch (Exception e)
            {
                clsErrorLog.Log(e); throw e;
            }
        }
        public DataSet GetMMS_EditlstItemDetails(int RecordIDText, int PageId)
        {
            try
            {
                return SQLDBUtil.ExecuteDataset("MMS_EditlstItemDetails", new SqlParameter[] { new SqlParameter("@RecordIDText", RecordIDText), new SqlParameter("@PageId", PageId) });
            }
            catch (Exception e)
            {
                clsErrorLog.Log(e); throw e;
            }
        }

        public DataSet MMS_Get_PO_Units(int poid, int woid)
        {
            try
            {
                return SQLDBUtil.ExecuteDataset("MMS_Get_PO_Units", new SqlParameter[] { new SqlParameter("@poid", poid), new SqlParameter("@woid", woid) });
            }
            catch (Exception e)
            { throw e; }
        }


        public DataSet GetMMS_EditlstPODetails(int RecordIDText, int PageId)
        {
            try
            {
                return SQLDBUtil.ExecuteDataset("MMS_EditlstPODetails", new SqlParameter[] { new SqlParameter("@RecordIDText", RecordIDText), new SqlParameter("@PageId", PageId) });
            }
            catch (Exception e)
            {
                clsErrorLog.Log(e); throw e;
            }
        }
        public DataSet GetMMS_GDN_Grid(int RecordIDText, int Pageid)
        {
            try
            {
                return SQLDBUtil.ExecuteDataset("MMS_GDN_GridEdit", new SqlParameter[] { new SqlParameter("@RecordIDText", RecordIDText), new SqlParameter("@Pageid", Pageid) });
            }
            catch (Exception e)
            {
                clsErrorLog.Log(e); throw e;
            }
        }

        public DataSet GetMMS_gvItemDetails(int GDNID, int Id)
        {
            try
            {
                return SQLDBUtil.ExecuteDataset("MMS_gvItemDetails", new SqlParameter[] { new SqlParameter("@GDNID", GDNID), new SqlParameter("@Id", Id) });
            }
            catch (Exception e)
            {
                clsErrorLog.Log(e); throw e;
            }
        }

        public DataSet GetMMS_gvSSNItemDetails(int SSNID)
        {
            try
            {
                return SQLDBUtil.ExecuteDataset("MMS_gvSSNItemDetails", new SqlParameter[] { new SqlParameter("@SSNID", SSNID)});
            }
            catch (Exception e)
            {
                clsErrorLog.Log(e); throw e;
            }
        }

        public DataSet GetMMS_gvCheckList()
        {
            try
            {
                return SQLDBUtil.ExecuteDataset("MMS_gvCheckList");
            }
            catch (Exception e)
            {
                clsErrorLog.Log(e); throw e;
            }
        }

        public DataSet MMS_FillListBoxItemDetails(int POID)
        {
            try
            {
                return SQLDBUtil.ExecuteDataset("MMS_FillListBoxItemDetails", new SqlParameter[] { new SqlParameter("@POID", POID) });
            }
            catch (Exception e)
            {
                clsErrorLog.Log(e); throw e;
            }
        }
        public DataSet GetMMS_GetGDNOtherCharges(int GDNID)
        {
            try
            {
                return SQLDBUtil.ExecuteDataset("MMS_GetGDNOtherCharges", new SqlParameter[] { new SqlParameter("@GDNID", GDNID) });
            }
            catch (Exception e)
            {
                clsErrorLog.Log(e); throw e;
            }
        }

        public DataSet GetDoodsItems(int GDNID)
        {
            try
            {
                return SQLDBUtil.ExecuteDataset("MMS_GetDoodsItems", new SqlParameter[] { new SqlParameter("@GDNID", GDNID) });
            }
            catch (Exception e)
            {
                clsErrorLog.Log(e); throw e;
            }
        }

        public DataSet ClosePo(int podetid,int EmpId)
        {
            try
            {
                return SQLDBUtil.ExecuteDataset("MMS_ClosePoFromGDN", new SqlParameter[] { new SqlParameter("@podetid", podetid), new SqlParameter("@EmpId", EmpId) });
            }
            catch (Exception e)
            {
                clsErrorLog.Log(e); throw e;
            }
        }

        public DataSet MMS_CheckPicInGDNItems(int GDNID, int Id)
        {
            try
            {
                return SQLDBUtil.ExecuteDataset("MMS_CheckPicInGDNItems", new SqlParameter[] { new SqlParameter("@GDNID", GDNID), new SqlParameter("@ID", Id) });
            }
            catch (Exception e)
            {
                clsErrorLog.Log(e); throw e;
            }
        }

        public DataSet MMS_DeleteGdn(int GdnId, int UserId)
        {
            try
            {
               
              DataSet  ds=SQLDBUtil.ExecuteDataset("MMS_DeleteGdn", new SqlParameter[] { new SqlParameter("@GdnId", GdnId), new SqlParameter("@UserId", UserId) });
                return ds;
            }
            catch (Exception e)
            {
                clsErrorLog.Log(e); throw e;
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
                clsErrorLog.Log(e); throw e;
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
                clsErrorLog.Log(e); throw e;
            }
        }

        public DataSet GetMMS_gvVehicle(Common objCommon)
        {
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[4];
                sqlParams[0] = new SqlParameter("@CurrentPage", objCommon.CurrentPage);
                sqlParams[1] = new SqlParameter("@PageSize", objCommon.PageSize);
                sqlParams[2] = new SqlParameter("@ReturnValue", System.Data.SqlDbType.Int);
                sqlParams[2].Direction = ParameterDirection.ReturnValue;
                sqlParams[3] = new SqlParameter("@NoofRecords", System.Data.SqlDbType.Int);
                sqlParams[3].Direction = ParameterDirection.Output;
               
                DataSet ds= SQLDBUtil.ExecuteDataset("MMS_gvVehicle", sqlParams);
                objCommon.NoofRecords= (int)sqlParams[3].Value;
                objCommon.TotalPages = (int)sqlParams[2].Value;
                return ds;
            }
            catch (Exception e)
            {
                clsErrorLog.Log(e); throw e;
            }
        }

        public DataSet MMS_SMS(int GDNID)
        {
            try
            {
                return SQLDBUtil.ExecuteDataset("MMS_SMS", new SqlParameter[] { new SqlParameter("@GDNID", GDNID) });
            }
            catch (Exception e)
            {
                clsErrorLog.Log(e); throw e;
            }
        }
        public DataSet GetMMS_lstWODetails(int VendorID, int ProjectId, int PageId)
        {
            try
            {
                return SQLDBUtil.ExecuteDataset("MMS_lstWODetails", new SqlParameter[] { new SqlParameter("@VendorID", VendorID), new SqlParameter("@ProjectId", ProjectId), new SqlParameter("@PageId", PageId) });
            }
            catch (Exception e)
            {
                clsErrorLog.Log(e); throw e;
            }
        }

        public DataSet GetMobileNo(int? EmpId1, int? EmpId2, int? EmpId3, int? EmpId4)
        {
            SqlParameter[] par = new SqlParameter[4];
            if (EmpId1 != 0)
            {
                par[0] = new SqlParameter("@EmpId1", EmpId1);
            }
            else
                par[0] =  new SqlParameter("@EmpId1",System.Data.SqlDbType.Int);
            if (EmpId2 != 0)
            {
                par[1] =new SqlParameter("@EmpId2", EmpId2);
            }
            else
            {
                par[1] =new SqlParameter("@EmpId2",System.Data.SqlDbType.Int);
            }
            if (EmpId3 != 0)
            {
                par[2] = new SqlParameter("@EmpId3", EmpId3);
            }
            else 
            {
                par[2] = new SqlParameter("@EmpId3", System.Data.SqlDbType.Int);
            }
            par[3] = new SqlParameter("@EmpId4", EmpId4);
            DataSet ds= SQLDBUtil.ExecuteDataset("MMS_GdnSMS", par);
            return ds;

        }
        public DataSet GetWoNo(int? Wo, int? Po)
        {
           
            SqlParameter[] par = new SqlParameter[2];
            par[0] = new SqlParameter("@WO", Wo);
            par[1] = new SqlParameter("@PO", Po);
            DataSet ds= SQLDBUtil.ExecuteDataset("MMS_GetPONos", par);
            return ds;
        }

       

       
        public DataSet GetWorkSite(int EmpId)
        {
           
            SqlParameter[] p = new SqlParameter[1];
            p[0] = new SqlParameter("@EmpId", EmpId);
           DataSet ds=SQLDBUtil.ExecuteDataset("MMS_GetWorkSite", p);
            return ds;
        }

        public DataSet UpLoadProofs(int WSID, string TripSheet, string RC, int? POId, DateTime StartDate, int EmpId, int PodetId, int ItemId, int Origin, int Destination, double Qty)
        {
            try
            {
                SqlParameter[] par = new SqlParameter[11];
                par[0] = new SqlParameter("@WSID", WSID);
                if(TripSheet!="")
                par[1] = new SqlParameter("@TS", TripSheet);
                else
                par[1] = new SqlParameter("@TS",SqlDbType.VarChar );
                if(RC!="")
                par[2] = new SqlParameter("@RC", RC);
                else
                par[2] = new SqlParameter("@RC", SqlDbType.VarChar);
                par[3] = new SqlParameter("@POId", POId);
                par[4] = new SqlParameter("@StartDate", StartDate);
                par[5] = new SqlParameter("@EnteredBy", EmpId);
                par[6] = new SqlParameter("@PodetID", PodetId);
                par[7] = new SqlParameter("@ItemId", ItemId);
                par[8] = new SqlParameter("@Origin", Origin);
                par[9] = new SqlParameter("@Destination", Destination);
                par[10] = new SqlParameter("@Qty", Qty);
                DataSet ds= SQLDBUtil.ExecuteDataset ("MMS_InsertQuickGDN", par);
                return ds;
            }
            catch (Exception e)
            {
                clsErrorLog.Log(e); throw e;
            }
        }
        public void InsertExtension(string ext, int ProofID)
        {
            try
            {
                SqlParameter[] par = new SqlParameter[2];
                par[0] = new SqlParameter("@ext", ext);
                par[1] = new SqlParameter("@ProofID", ProofID);
                SQLDBUtil.ExecuteNonQuery("MMS_InsertExt", par);
            }
            catch (Exception e)
            { clsErrorLog.Log(e); throw e; }
        }

        public void InsertQuickGRNExtension(string ext, int GDNItemId)
        {
            try
            {
                SqlParameter[] par = new SqlParameter[2];
                if (ext == "")
                {
                    par[0] = new SqlParameter("@ext", System.Data.SqlDbType.VarChar);
                }
                else
                {
                    par[0] = new SqlParameter("@ext", ext);
                }
                par[1] = new SqlParameter("@GDNItemId", GDNItemId);
                SQLDBUtil.ExecuteNonQuery("MMS_InsertQuickGRNExt", par);
            }
            catch (Exception e)
            { clsErrorLog.Log(e); throw e; }
        }


        public DataSet MMS_DDL_getPOIDByWS(int WSID, int EmpId,int State)
        {
           
            SqlParameter[] p = new SqlParameter[3];
            p[0] = new SqlParameter("@WSID", WSID);
            p[1] = new SqlParameter("@EMPID", EmpId);
            p[2] = new SqlParameter("@State", State);
            DataSet ds= SQLDBUtil.ExecuteDataset("MMS_DDL_getPOIDByAssignment", p);
            return ds;
        }

        public DataSet MMS_DDL_getPOIDByWorksite(int WSID)
        {
           
            SqlParameter[] p = new SqlParameter[1];
            p[0] = new SqlParameter("@WSID", WSID);
            DataSet ds= SQLDBUtil.ExecuteDataset("MMS_DDL_getPOIDByWorkSite", p);
            return ds;
        }
        public DataSet GetMMS_POItemDetails(int ResourceID)
        {
            try
            {
                return SQLDBUtil.ExecuteDataset("[MMS_POItemDetails]", new SqlParameter[] { new SqlParameter("@ResourceID", ResourceID) });
            }
            catch (Exception e)
            {
                clsErrorLog.Log(e); throw e;
            }
        }
        public DataSet getPODetId(int POID, int ItemId)
        {
            try
            {
               
                SqlParameter[] par = new SqlParameter[2];
                par[0] = new SqlParameter("@POID", POID);
                par[1] = new SqlParameter("@ItemId", ItemId);
              DataSet  ds=SQLDBUtil.ExecuteDataset("MMS_GetPodetId", par);
                return ds;
            }
            catch (Exception e)
            { clsErrorLog.Log(e); throw e; }
        }
        public DataSet MMS_GetQuickGDN(Common objCommon, int? POID, int? WSID, string TS, string RC,int EmpId,int State)
        {
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[10];
                sqlParams[0] = new SqlParameter("@CurrentPage", objCommon.CurrentPage);
                sqlParams[1] = new SqlParameter("@PageSize", objCommon.PageSize);
                sqlParams[2] = new SqlParameter("@ReturnValue", System.Data.SqlDbType.Int);
                sqlParams[2].Direction = ParameterDirection.ReturnValue;
                sqlParams[3] = new SqlParameter("@NoofRecords", System.Data.SqlDbType.Int);
                sqlParams[3].Direction = ParameterDirection.Output;
                if(POID==0)
                {
                    sqlParams[4] = new SqlParameter("@POID", System.Data.SqlDbType.Int);
                }
                else
                {
                    sqlParams[4] = new SqlParameter("@POID", POID);
                }
                if (WSID == 0)
                {
                    sqlParams[5] = new SqlParameter("@WSID", System.Data.SqlDbType.Int);
                }
                else
                {
                    sqlParams[5] = new SqlParameter("@WSID", WSID);
                }

                if (TS != "")
                    sqlParams[6] = new SqlParameter("@TS", TS);
                else
                    sqlParams[6] = new SqlParameter("@TS", SqlDbType.VarChar);
                if (RC != "")
                    sqlParams[7] = new SqlParameter("@RC", RC);
                else
                    sqlParams[7] = new SqlParameter("@RC", SqlDbType.VarChar);
                sqlParams[8] = new SqlParameter("@EmpId", EmpId);
                sqlParams[9] = new SqlParameter("@State", State);
               
                DataSet ds= SQLDBUtil.ExecuteDataset("MMS_GetQuickGDN", sqlParams);
                objCommon.NoofRecords= (int)sqlParams[3].Value;
                objCommon.TotalPages = (int)sqlParams[2].Value;
                return ds;
            }
            catch (Exception e)
            {
                clsErrorLog.Log(e); throw e;
            }
        }

        public DataSet MMS_GetQuickGDNForReceiver(Common objCommon, int? POID, int? WSID, string TS, string RC, int EmpId, int State)
        {
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[10];
                sqlParams[0] = new SqlParameter("@CurrentPage", objCommon.CurrentPage);
                sqlParams[1] = new SqlParameter("@PageSize", objCommon.PageSize);
                sqlParams[2] = new SqlParameter("@ReturnValue", System.Data.SqlDbType.Int);
                sqlParams[2].Direction = ParameterDirection.ReturnValue;
                sqlParams[3] = new SqlParameter("@NoofRecords", System.Data.SqlDbType.Int);
                sqlParams[3].Direction = ParameterDirection.Output;
                if (POID == 0)
                {
                    sqlParams[4] = new SqlParameter("@POID", System.Data.SqlDbType.Int);
                }
                else
                {
                    sqlParams[4] = new SqlParameter("@POID", POID);
                }
                if (WSID == 0)
                {
                    sqlParams[5] = new SqlParameter("@WSID", System.Data.SqlDbType.Int);
                }
                else
                {
                    sqlParams[5] = new SqlParameter("@WSID", WSID);
                }

                if (TS != "")
                    sqlParams[6] = new SqlParameter("@TS", TS);
                else
                    sqlParams[6] = new SqlParameter("@TS", SqlDbType.VarChar);
                if (RC != "")
                    sqlParams[7] = new SqlParameter("@RC", RC);
                else
                    sqlParams[7] = new SqlParameter("@RC", SqlDbType.VarChar);
                sqlParams[8] = new SqlParameter("@EmpId", EmpId);
                sqlParams[9] = new SqlParameter("@State", State);
               
                DataSet ds= SQLDBUtil.ExecuteDataset("MMS_GetQuickGDNForReceiver", sqlParams);
                objCommon.NoofRecords= (int)sqlParams[3].Value;
                objCommon.TotalPages = (int)sqlParams[2].Value;
                return ds;
            }
            catch (Exception e)
            {
                clsErrorLog.Log(e); throw e;
            }
        }

        public DataSet GetDetailsforGDN(int ProofId,int EmpId)
        {
            try
            {

                DataSet ds = SQLDBUtil.ExecuteDataset("MMS_CreateQuickGDN", new SqlParameter[] { new SqlParameter("@ProofId", ProofId), new SqlParameter("@EmpId", EmpId) });
                return ds;
            }
            catch (Exception e)
            {
                clsErrorLog.Log(e); throw e;
            }
        }

        public DataSet MMS_DeleteQuickGDN(int ProofId)
        { 
            try
            {

                DataSet ds = SQLDBUtil.ExecuteDataset("MMS_DeleteQuickGDN", new SqlParameter[] { new SqlParameter("@ProofId", ProofId) });
                return ds;
            }
            catch (Exception e)
            {
                clsErrorLog.Log(e); throw e;
            }

        }


        public DataSet CreateQuickGRN(int WSID, string TripSheet, string RC, int? POId, DateTime StartDate, int EmpId, int Origin, int Destination, double Qty,double POQty, DataSet dsGDNS,int? vehicle,string Invoice)
        {
            try
            {
               
                SqlParameter[] par = new SqlParameter[12];
                par[0] = new SqlParameter("@WSID", WSID);
                if (TripSheet != "")
                    par[1] = new SqlParameter("@TS", TripSheet);
                else
                    par[1] = new SqlParameter("@TS", SqlDbType.VarChar);
                if (RC != "")
                    par[2] = new SqlParameter("@RC", RC);
                else
                    par[2] = new SqlParameter("@RC", SqlDbType.VarChar);
                par[3] = new SqlParameter("@POId", POId);
                par[4] = new SqlParameter("@StartDate", StartDate);
                par[5] = new SqlParameter("@CreatededBy", EmpId);
                par[6] = new SqlParameter("@Origin", Origin);
                par[7] = new SqlParameter("@Destination", Destination);
                par[8] = new SqlParameter("@POQty", POQty);
                par[9] = new SqlParameter("@GDNIDs", dsGDNS.GetXml());
                par[10] = new SqlParameter("@Vehicle", vehicle);
                if (Invoice != "")
                    par[11] = new SqlParameter("@Invoice", Invoice);
                else
                    par[11] = new SqlParameter("@Invoice", SqlDbType.VarChar);
                DataSet ds= SQLDBUtil.ExecuteDataset("MMS_CreateQuickGRNByXML", par);
                return ds;
             
            }
            catch (Exception e)
            {
                clsErrorLog.Log(e); throw e;
            }
        }
        public DataSet GetItemRate(int ItemId, int PodetId)
        {
            try 
            {
               
                SqlParameter[] par = new SqlParameter[2];
                par[0] = new SqlParameter("@Itemid", ItemId);
                par[1] = new SqlParameter("@PodetID", PodetId);
                DataSet ds= SQLDBUtil.ExecuteDataset("MMS_GetItemRate", par);
                return ds;
            }
            catch(Exception e)
            {
                clsErrorLog.Log(e); throw e;
            }
        }

    }
}