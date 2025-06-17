using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Aeclogic.Common.DAL;

namespace AECLOGIC.ERP.HMS
{
    public class SRNService
    {

        #region Generic Function
        #region	Get Web Config
       
        #endregion
        #endregion

        public enum GoodsBillStatus { Inactive, Approvals, Approved, Rejected };

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
        public DataSet GetMMS_SRN_gvItemDetails(int SRNID, int Id)
        {
            try
            {
                return SQLDBUtil.ExecuteDataset("[MMS_SRN_gvItemDetails]", new SqlParameter[] { new SqlParameter("@SRNID", SRNID), new SqlParameter("@Id", Id) });
            }
            catch (Exception e)
            {
                clsErrorLog.Log(e); throw e;
            }
        }
        public DataSet GetMMS_MIS_lstWODetails(int VendorID, int ProjectId, int PageId,int ModuleID)
        {
            try
            {
                return SQLDBUtil.ExecuteDataset("MMS_MIS_lstWODetails", new SqlParameter[] { new SqlParameter("@VendorID", VendorID), new SqlParameter("@ProjectId", ProjectId), new SqlParameter("@PageId", PageId), new SqlParameter("@ModuleID", ModuleID) });
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
        public DataSet GetMMS_MIS_lstItemDetails(int ResourceID, int PageId)
        {
            try
            {
                return SQLDBUtil.ExecuteDataset("MMS_MIS_lstItemDetails", new SqlParameter[] { new SqlParameter("@ResourceID", ResourceID), new SqlParameter("@PageId", PageId) });
            }
            catch (Exception e)
            {
                clsErrorLog.Log(e); throw e;
            }
        }
        public DataSet GetMMS_SRN_Grid(int SRNID)
        {
            try
            {
                return SQLDBUtil.ExecuteDataset("MMS_EditSRN", new SqlParameter[] { new SqlParameter("@SRNID", SRNID)});
            }
            catch (Exception e)
            {
                clsErrorLog.Log(e); throw e;
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
                clsErrorLog.Log(e); throw e;
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

        public DataSet GetMMS_SDNPurchaseOrderdetails(int PODetails, int ITEMID)
        {
            try
            {
                SqlParameter[] sqlPrms = new SqlParameter[2];
                sqlPrms[0] = new SqlParameter("@PODetails", PODetails);
                sqlPrms[1] = new SqlParameter("@ITEMID", ITEMID);
                return SQLDBUtil.ExecuteDataset("MMS_SDNPurchaseOrderdetails", sqlPrms);
            }
            catch (Exception e)
            {
                clsErrorLog.Log(e); throw e;
            }
        }

        public DataSet GetMMS_SRN_New(SRNService objCommon, int? SRNId, int? WONO, int? WorkSite, int? VendorId, int ID, int ModuleId, DateTime? FromDate, DateTime? ToDate)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[12];
                if (SRNId != 0)
                param[0] = new SqlParameter("@SRNID", SRNId);
                else
                    param[0] = new SqlParameter("@SRNID", System.Data.SqlDbType.Int);

                param[1] = new SqlParameter("@CurrentPage", objCommon.CurrentPage);
                param[2] = new SqlParameter("@PageSize", objCommon.PageSize);
                param[3] = new SqlParameter("@NoofRecords", System.Data.SqlDbType.Int);
                param[3].Direction = ParameterDirection.Output;
                if(WONO!=0)
                     param[4] = new SqlParameter("@WONo", WONO);
                else
                    param[4] = new SqlParameter("@WONo", System.Data.SqlDbType.Int);

              
                param[5] = new SqlParameter("Returnvalue", System.Data.SqlDbType.Int);
                param[5].Direction = ParameterDirection.ReturnValue;

                if (WorkSite!=0)
                    param[6] = new SqlParameter("@Worksite", WorkSite);
                else
                    param[6] = new SqlParameter("@Worksite", System.Data.SqlDbType.Int);

                if(VendorId!=0)
                    param[7] = new SqlParameter("@VendorId", VendorId);
                else
                    param[7] = new SqlParameter("@VendorId", System.Data.SqlDbType.Int);

                    param[8] = new SqlParameter("@ID", ID);
                    if (ModuleId != 0)
                        param[9] = new SqlParameter("@ModuleId", ModuleId);
                    else
                    param[9] = new SqlParameter("@ModuleId", System.Data.SqlDbType.Int);
                param[10] = new SqlParameter("@FromDate", FromDate);
                param[11] = new SqlParameter("@ToDate", ToDate);
                
                DataSet ds= SQLDBUtil.ExecuteDataset("MMS_SRN_New", param);
                objCommon.NoofRecords= (int)param[3].Value;
                objCommon.TotalPages = (int)param[5].Value;
                return ds;
            }
            catch (Exception e)
            {
                throw e;
            }

        }


        public DataSet GetMMS_SSN_New(SRNService objCommon, int? SSNId, int? SONO, int? WorkSite, int? VendorId, int ID, int ModuleId, DateTime? FromDate, DateTime? ToDate)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[12];
                if (SSNId != 0)
                    param[0] = new SqlParameter("@SSNID", SSNId);
                else
                    param[0] = new SqlParameter("@SSNID", System.Data.SqlDbType.Int);

                param[1] = new SqlParameter("@CurrentPage", objCommon.CurrentPage);
                param[2] = new SqlParameter("@PageSize", objCommon.PageSize);
                param[3] = new SqlParameter("@NoofRecords", System.Data.SqlDbType.Int);
                param[3].Direction = ParameterDirection.Output;
                if (SONO != 0)
                    param[4] = new SqlParameter("@SONo", SONO);
                else
                    param[4] = new SqlParameter("@SONo", System.Data.SqlDbType.Int);


                param[5] = new SqlParameter("Returnvalue", System.Data.SqlDbType.Int);
                param[5].Direction = ParameterDirection.ReturnValue;

                if (WorkSite != 0)
                    param[6] = new SqlParameter("@Worksite", WorkSite);
                else
                    param[6] = new SqlParameter("@Worksite", System.Data.SqlDbType.Int);

                if (VendorId != 0)
                    param[7] = new SqlParameter("@VendorId", VendorId);
                else
                    param[7] = new SqlParameter("@VendorId", System.Data.SqlDbType.Int);

                param[8] = new SqlParameter("@ID", ID);
                if (ModuleId != 0)
                    param[9] = new SqlParameter("@ModuleId", ModuleId);
                else
                    param[9] = new SqlParameter("@ModuleId", System.Data.SqlDbType.Int);
                param[10] = new SqlParameter("@FromDate", FromDate);
                param[11] = new SqlParameter("@ToDate", ToDate);
                
                DataSet ds= SQLDBUtil.ExecuteDataset("MMS_SSN_New", param);
                objCommon.NoofRecords= (int)param[3].Value;
                objCommon.TotalPages = (int)param[5].Value;
                return ds;
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
                clsErrorLog.Log(e); throw e;
            }
        }

        public DataSet MMS_GetSGDNItems(int SGDNId)
        {
            try
            {
                SqlParameter[] sqlPrms = new SqlParameter[1];
                sqlPrms[0] = new SqlParameter("@SGDNId", SGDNId);

                return SQLDBUtil.ExecuteDataset("MMS_GetSGDNItems", sqlPrms);
            }
            catch (Exception e)
            {
                clsErrorLog.Log(e); throw e;
            }
        }

        public DataSet MMS_GetSsnItems(int SsnId, int ID)
        {
            try
            {
                SqlParameter[] sqlPrms = new SqlParameter[2];
                sqlPrms[0] = new SqlParameter("@SsnId", SsnId);
                sqlPrms[1] = new SqlParameter("@ID", ID);
                return SQLDBUtil.ExecuteDataset("MMS_GetSsnItems", sqlPrms);
            }
            catch (Exception e)
            {
                clsErrorLog.Log(e); throw e;
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
                if(Comments!="")
                 sqlPrms[4] = new SqlParameter("@Comments", Comments);
                    else
                 sqlPrms[4] = new SqlParameter("@Comments", System.Data.SqlDbType.Int);
                
                if (Distance != 0)
                    sqlPrms[5] = new SqlParameter("@Distance", Distance);
                else
                    sqlPrms[5] = new SqlParameter("@Distance", System.Data.SqlDbType.Int);

                sqlPrms[6] = new SqlParameter("@EmpId", EmpId);

                SQLDBUtil.ExecuteNonQuery("MMS_UpdateSrnItems", sqlPrms);
            }
            catch (Exception e)
            {
                clsErrorLog.Log(e); throw e;
            }
        }


        public void MMS_UpdateSGDNItems(int SgdnItemId,  decimal AccptQty, int RcvdBy, string Comments, int EmpId)
        {
            try
            {
                SqlParameter[] sqlPrms = new SqlParameter[5];
                sqlPrms[0] = new SqlParameter("@SgdnItemId", SgdnItemId);

                sqlPrms[1] = new SqlParameter("@AccptQty", AccptQty);
                sqlPrms[2] = new SqlParameter("@ApprovedBy", RcvdBy);
                
                if (Comments != "")
                    sqlPrms[3] = new SqlParameter("@Comments", Comments);
                else
                    sqlPrms[3] = new SqlParameter("@Comments", System.Data.SqlDbType.Int);

                sqlPrms[4] = new SqlParameter("@EmpId", EmpId);

                SQLDBUtil.ExecuteNonQuery("MMS_UpdateSgdnItems", sqlPrms);
            }
            catch (Exception e)
            {
                clsErrorLog.Log(e); throw e;
            }
        }



        public void MMS_UpdateSsnItems(int SsnItemId, int ID, decimal RcvdQty, int RcvdBy, string Comments, int EmpId, double Distance)
        {
            try
            {
                SqlParameter[] sqlPrms = new SqlParameter[7];
                sqlPrms[0] = new SqlParameter("@SsnItemId", SsnItemId);
                sqlPrms[1] = new SqlParameter("@Id", ID);
                sqlPrms[2] = new SqlParameter("@RcvdQty", RcvdQty);
                sqlPrms[3] = new SqlParameter("@RecivedBy", RcvdBy);
               
                if (Comments != "")
                    sqlPrms[4] = new SqlParameter("@Comments", Comments);
                else
                    sqlPrms[4] = new SqlParameter("@Comments", System.Data.SqlDbType.Int);

                sqlPrms[5] = new SqlParameter("@EmpId", EmpId);
                sqlPrms[6] = new SqlParameter("@Distance", Distance);

                SQLDBUtil.ExecuteNonQuery("MMS_UpdateSSNItems", sqlPrms);
            }
            catch (Exception e)
            {
                clsErrorLog.Log(e); throw e;
            }
        }
       
        
        public int MMS_SRN_SendBack(int SRNID, int UserId)
        {
            try
            {
                SqlParameter[] par = new SqlParameter[3];
                par[0] = new SqlParameter("@SRNID", SRNID);
                par[1] = new SqlParameter("@UserId", UserId);
                par[2] = new SqlParameter("Returnvalue", System.Data.SqlDbType.Int);
                par[2].Direction = ParameterDirection.ReturnValue;
                SQLDBUtil.ExecuteNonQuery("MMS_SRN_SendBack",par);
                int val = Convert.ToInt32(par[2].Value);
                return val;
            }
            catch (Exception e)
            {
                clsErrorLog.Log(e); throw e;
            }
        }

        public int MMS_SGDN_SendBack(int SGDNID, int UserId)
        {
            try
            {
                SqlParameter[] par = new SqlParameter[3];
                par[0] = new SqlParameter("@SGDNID", SGDNID);
                par[1] = new SqlParameter("@UserId", UserId);
                par[2] = new SqlParameter("Returnvalue", System.Data.SqlDbType.Int);
                par[2].Direction = ParameterDirection.ReturnValue;
                SQLDBUtil.ExecuteNonQuery("MMS_SGDN_SendBack", par);
                int val = Convert.ToInt32(par[2].Value);
                return val;
            }
            catch (Exception e)
            {
                clsErrorLog.Log(e); throw e;
            }
        }

        public int MMS_SSN_SendBack(int SSNID, int UserId)
        {
            try
            {
                SqlParameter[] par = new SqlParameter[3];
                par[0] = new SqlParameter("@SSNID", SSNID);
                par[1] = new SqlParameter("@UserId", UserId);
                par[2] = new SqlParameter("Returnvalue", System.Data.SqlDbType.Int);
                par[2].Direction = ParameterDirection.ReturnValue;
                SQLDBUtil.ExecuteNonQuery("MMS_SSN_SendBack", par);
                int val = Convert.ToInt32(par[2].Value);
                return val;
            }
            catch (Exception e)
            {
                clsErrorLog.Log(e); throw e;
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
                clsErrorLog.Log(e); throw e;
            }
        }

        public DataSet MMS_SSN_ViewItems(int SSNID)
        {
            try
            {
                return SQLDBUtil.ExecuteDataset("[MMS_SSN_ViewItems]", new SqlParameter[] { new SqlParameter("@SSNID", SSNID) });
            }
            catch (Exception e)
            {
                clsErrorLog.Log(e); throw e;
            }
        }
       
        public int MMS_DeleteSRN(int SRNID, int UserId)
        {
            try
            {
                SqlParameter[] par = new SqlParameter[3];
                par[0] = new SqlParameter("@SRNID", SRNID);
                par[1] = new SqlParameter("@UserId", UserId);
                par[2] = new SqlParameter("Returnvalue", System.Data.SqlDbType.Int);
                par[2].Direction = ParameterDirection.ReturnValue;
                SQLDBUtil.ExecuteNonQuery("MMS_DeleteSRN", par);
                int val = Convert.ToInt32(par[2].Value);
                return val;
            }
            catch (Exception e)
            {
                clsErrorLog.Log(e); throw e;
            }
        }

        public int MMS_DeleteSSN(int SSNID, int UserId)
        {
            try
            {
                SqlParameter[] par = new SqlParameter[3];
                par[0] = new SqlParameter("@SSNID", SSNID);
                par[1] = new SqlParameter("@UserId", UserId);
                par[2] = new SqlParameter("Returnvalue", System.Data.SqlDbType.Int);
                par[2].Direction = ParameterDirection.ReturnValue;
                SQLDBUtil.ExecuteNonQuery("[MMS_DeleteSSN]", par);
                int val = Convert.ToInt32(par[2].Value);
                return val;
            }
            catch (Exception e)
            {
                clsErrorLog.Log(e); throw e;
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
               clsErrorLog.Log(e); throw e;
            }
        }

        public DataSet MMS_CheckPicInSGDNItems(int SGDNID)
        {
            try
            {
                return SQLDBUtil.ExecuteDataset("MMS_CheckPicInSGDNItems", new SqlParameter[] { new SqlParameter("@SGDNID", SGDNID) });
            }
            catch (Exception e)
            {
                clsErrorLog.Log(e); throw e;
            }
        }

        public DataSet MMS_CheckPicInSSNItems(int SSNID)
        {
            try
            {
                return SQLDBUtil.ExecuteDataset("MMS_CheckPicInSSNItems", new SqlParameter[] { new SqlParameter("@SSNID", SSNID) });
            }
            catch (Exception e)
            {
                clsErrorLog.Log(e); throw e;
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
                 clsErrorLog.Log(e); throw e;
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

        public DataSet MMS_gvAllSGDNsBind(DataSet dsSGDNs)
        {
            try
            {

                return SQLDBUtil.ExecuteDataset("MMS_gvAllSGDNsBind", new SqlParameter[] { new SqlParameter("@SGDNIDs", dsSGDNs.GetXml()) });

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public DataSet MMS_gvAllSSNsBind(DataSet dsSSNs, int Id)
        {
            try
            {

                return SQLDBUtil.ExecuteDataset("MMS_gvAllSSNsBind", new SqlParameter[] { new SqlParameter("@SSNIDs", dsSSNs.GetXml()), new SqlParameter("@Id", Id) });

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static DataSet MMS_GETSRNITEMID(int SRNItemID)
        {
            
            DataSet ds= SQLDBUtil.ExecuteDataset("MMS_GETSRNITEMID", new SqlParameter[] { new SqlParameter("@SRNItemID", SRNItemID) });
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

        public DataSet MMS_GetGdnsFromXML(DataSet GDNSDataSet)
        {
            try
            {

                return SQLDBUtil.ExecuteDataset("MMS_GetGdnsFromXML", new SqlParameter[] { new SqlParameter("@SRNItemID", GDNSDataSet.GetXml()) });

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public DataSet MMS_GetSSNsFromXML(DataSet GDNSDataSet)
        {
            try
            {

                return SQLDBUtil.ExecuteDataset("MMS_GetSSNsFromXML", new SqlParameter[] { new SqlParameter("@SSNItemID", GDNSDataSet.GetXml()) });

            }
            catch (Exception e)
            {
                throw e;
            }
        }


       
        public DataSet GetMMS_GridItemDetailsFupload(int p)
        {
            throw new NotImplementedException();
        }

        public DataSet MMS_GetBillNos(int Mode,int  ModuleID)
        {
            try
            {
                return SQLDBUtil.ExecuteDataset("MMS_GETPURCHASEBILLNOS", new SqlParameter[] { new SqlParameter("@Mode", Mode), new SqlParameter("@ModuleID", ModuleID) });
            }
            catch (Exception e)
            {
                throw e;
            }

        }
        public DataSet MMS_SerchGDNsForBillsAdjusting(SRNService objCommon, int? VendorID, int? PONo, int? BillNo, int? WSId, int? Gdnid, int Mode, int ModuleID,int Companyid)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[8];
                param[0] = new SqlParameter("@Mode", Mode);

                if (VendorID != 0)
                    param[1] = new SqlParameter("@VENDORID", VendorID);
                else
                    param[1] = new SqlParameter("@VENDORID", System.Data.SqlDbType.Int);
                if (PONo != 0)
                    param[2] = new SqlParameter("@PONO", PONo);
                else
                    param[2] = new SqlParameter("@PONO", System.Data.SqlDbType.Int);
                if (BillNo != 0)
                    param[3] = new SqlParameter("@BillNo", BillNo);
                else
                    param[3] = new SqlParameter("@BillNo", System.Data.SqlDbType.Int);
                if (WSId != 0)
                    param[4] = new SqlParameter("@WorksiteId", WSId);
                else
                    param[4] = new SqlParameter("@WorksiteId", System.Data.SqlDbType.Int);
                if (Gdnid != 0)
                    param[5] = new SqlParameter("@GDNID", Gdnid);
                else
                    param[5] = new SqlParameter("@GDNID", System.Data.SqlDbType.Int);
                param[6] = new SqlParameter("@ModuleID", ModuleID);

                if (Companyid != 0)
                    param[7] = new SqlParameter("@CompanyID", Companyid);
                else
                    param[7] = new SqlParameter("@CompanyID", System.Data.SqlDbType.Int);
                
                
                DataSet ds= SQLDBUtil.ExecuteDataset("MMS_SERCHBILLEDGDNS", param);
                return ds;
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public DataSet MMS_GetGdnsfromBill(int BillNo, int Mode)
        {
            try
            {
                return SQLDBUtil.ExecuteDataset("MMS_GDNSINBILL", new SqlParameter[] { new SqlParameter("@BILLNO", BillNo), new SqlParameter("@MODE", Mode) });
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public DataSet MMS_GetBillDates(int BillNo, int Mode)
        {
            try
            {
                
                SqlParameter[] par = new SqlParameter[2];
                par[0] = new SqlParameter("@BillNo", BillNo);
                par[1] = new SqlParameter("@MOde", Mode);
                DataSet ds= SQLDBUtil.ExecuteDataset("MMS_GetBillDates", par);
                return ds;
            }
            catch (Exception e)
            { throw e; }
        }

        public int MMS_MakeGdnUnBilled(int GdnId, int Mode, int EmpId)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[3];
                param[0] = new SqlParameter("@GDNID", GdnId);
                param[1] = new SqlParameter("@MODE", Mode);
                param[2] = new SqlParameter("@EMPID", EmpId);
                SQLDBUtil.ExecuteNonQuery("MMS_MAKEGDNUNBILLED", param);
                return 1;
            }
            catch
            { return 1; }
        } 

        public DataSet MMS_ForcegdntoBill(int BillNo, int GdnId, int Mode, int EmpId)
        {
            try
            {
                
                SqlParameter[] param = new SqlParameter[4];
                param[0] = new SqlParameter("@BILLNO", BillNo);
                param[1] = new SqlParameter("@GDNID", GdnId);
                param[2] = new SqlParameter("@MODE", Mode);
                param[3] = new SqlParameter("@EMPID", EmpId);

                DataSet ds= SQLDBUtil.ExecuteDataset("MMS_FORCEUNBILLEDGDNTOBILLING", param);
                return ds;
            }
            catch (Exception e)
            { throw e; }
        }


        public void MMS_UpdateBillPeriod(int SRNBillNO, DateTime? FromDate, DateTime? ToDate, int Mode)
        {
            SqlParameter[] par = new SqlParameter[4];
            par[0] = new SqlParameter("@BillNo", SRNBillNO);
            par[1] = new SqlParameter("@FromDate", FromDate);
            par[2] = new SqlParameter("@ToDate", ToDate);
            par[3] = new SqlParameter("@Mode", Mode);
            SQLDBUtil.ExecuteNonQuery("MMS_ServiceBillChangePeriod", par);
        }


    }


}
