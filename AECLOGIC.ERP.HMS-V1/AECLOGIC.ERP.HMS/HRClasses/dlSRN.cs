using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Aeclogic.Common.DAL;

namespace AECLOGIC.ERP.HMS
{
     public class dlSRN
    {

             #region Generic Function
            
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

             public static  DataSet GetMMS_SRN_gvItemDetails(int SRNID, int Id)
             {

                 return SQLDBUtil.ExecuteDataset("[MMS_SRN_gvItemDetails]", new SqlParameter[] { new SqlParameter("@SRNID", SRNID), new SqlParameter("@Id", Id) });

             }
             public  static DataSet GetMMS_MIS_lstWODetails(int VendorID, int ProjectId, int PageId,int ModuleID,int CompanyID)
             {

                 return SQLDBUtil.ExecuteDataset("MMS_MIS_lstWODetails", new SqlParameter[] { new SqlParameter("@VendorID", VendorID), new SqlParameter("@ProjectId", ProjectId), new SqlParameter("@PageId", PageId), 
                                                                         new SqlParameter("@ModuleID", ModuleID) , new SqlParameter("@CompanyID", CompanyID)});
             }
             public static DataSet MMS_DDL_SearchVendor(string SearchKeyWord)
             {

                 return SQLDBUtil.ExecuteDataset("MMS_DDL_SearchVendor", new SqlParameter[] { new SqlParameter("@SEARCH", SearchKeyWord) });

             }
             public static DataSet MMS_DDL_SearchVehicle(string SearchKeyWord)
             {

                 return SQLDBUtil.ExecuteDataset("MMS_DDL_SearchVehicle", new SqlParameter[] { new SqlParameter("@SEARCH", SearchKeyWord) });

             }
             public static DataSet GetMMS_MIS_lstItemDetails(int ResourceID, int PageId)
             {

                 return SQLDBUtil.ExecuteDataset("MMS_MIS_lstItemDetails", new SqlParameter[] { new SqlParameter("@ResourceID", ResourceID), new SqlParameter("@PageId", PageId) });

             }
             public static DataSet GetMMS_SRN_Grid(int SRNID)
             {

                 return SQLDBUtil.ExecuteDataset("MMS_EditSRN", new SqlParameter[] { new SqlParameter("@SRNID", SRNID) });

             }
             public static DataSet GetMMS_SRN_EditlstItemDetails(int RecordIDText, int PageId)
             {

                 return SQLDBUtil.ExecuteDataset("[MMS_SRN_EditlstItemDetails]", new SqlParameter[] { new SqlParameter("@RecordIDText", RecordIDText), new SqlParameter("@PageId", PageId) });

             }
             public static DataSet GetMMS_SRN_EditlstPODetails(int RecordIDText, int PageId)
             {

                 return SQLDBUtil.ExecuteDataset("MMS_SRN_EditlstPODetails", new SqlParameter[] { new SqlParameter("@RecordIDText", RecordIDText), new SqlParameter("@PageId", PageId) });

             }
             public static DataSet GetMMS_GDNPurchaseOrderdetails(int PODetails, int ITEMID)
             {

                 SqlParameter[] sqlPrms = new SqlParameter[2];
                 sqlPrms[0] = new SqlParameter("@PODetails", PODetails);
                 sqlPrms[1] = new SqlParameter("@ITEMID", ITEMID);
                 return SQLDBUtil.ExecuteDataset("MMS_GDNPurchaseOrderdetails", sqlPrms);

             }
             public static DataSet GetMMS_HMS_GDNPurchaseOrderdetails(int PODetails, int ITEMID)
             {

                 SqlParameter[] sqlPrms = new SqlParameter[2];
                 sqlPrms[0] = new SqlParameter("@PODetails", PODetails);
                 sqlPrms[1] = new SqlParameter("@ITEMID", ITEMID);
                 return SQLDBUtil.ExecuteDataset("HMS_GDNPurchaseOrderdetails", sqlPrms);

             }
             public static DataSet GetMMS_SRN_New(dlSRN objCommon, int? SRNId, int? WONO, int? WorkSite, int? VendorId, int ID, int ModuleId)
             {

                 SqlParameter[] param = new SqlParameter[10];

                 if (SRNId != 0)
                     param[0] = new SqlParameter("@SRNID", SRNId);
                 else
                     param[0] = new SqlParameter("@SRNID", System.Data.SqlDbType.Int);

                 param[1] = new SqlParameter("@CurrentPage", objCommon.CurrentPage);
                 param[2] = new SqlParameter("@PageSize", objCommon.PageSize);
                 param[3] = new SqlParameter("@NoofRecords", System.Data.SqlDbType.Int);
                 param[3].Direction = ParameterDirection.Output;

                 if (WONO != 0)
                     param[4] = new SqlParameter("@WONo", WONO);
                 else
                     param[4] = new SqlParameter("@WONo", System.Data.SqlDbType.Int);


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
                 param[9] = new SqlParameter("@ModuleId", ModuleId);
                
                 DataSet ds= SQLDBUtil.ExecuteDataset("MMS_SRN_New", param);
                 objCommon.NoofRecords= (int)param[3].Value;
                 objCommon.TotalPages = (int)param[5].Value;
                 return ds;
            }
            public static DataSet MMS_GetSrnItems(int SrnId, int ID)
             {

                 SqlParameter[] sqlPrms = new SqlParameter[2];
                 sqlPrms[0] = new SqlParameter("@SrnId", SrnId);
                 sqlPrms[1] = new SqlParameter("@ID", ID);
                 return SQLDBUtil.ExecuteDataset("MMS_GetSrnItems", sqlPrms);
             }
             public static void MMS_UpdateSrnItems(int SrnItemId, int ID, decimal RcvdQty, int RcvdBy, string Comments, int Distance, int EmpId)
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
             public static void MMS_SRN_SendBack(int SRNID,int UserId)
             {

                 SQLDBUtil.ExecuteNonQuery("MMS_SRN_SendBack", new SqlParameter[] { new SqlParameter("@SRNID", SRNID),new SqlParameter("@UserId",UserId) });
             }
             public  static DataSet MMS_SRN_ViewItems(int SRNID)
             {

                 return SQLDBUtil.ExecuteDataset("[MMS_SRN_ViewItems]", new SqlParameter[] { new SqlParameter("@SRNID", SRNID) });
             }
             public static void MMS_DeleteSRN(int SRNID)
             {

                 SQLDBUtil.ExecuteNonQuery("[MMS_DeleteSRN]", new SqlParameter[] { new SqlParameter("@SRNID", SRNID) });

             }
             public static DataSet MMS_CheckPicInSRNNItems(int SRNID)
             {

                 return SQLDBUtil.ExecuteDataset("MMS_CheckPicInSRNNItems", new SqlParameter[] { new SqlParameter("@SRNID", SRNID) });
             }
             public static DataSet MMS_gvSrnItemDetails(int SRNID)
             {

                 return SQLDBUtil.ExecuteDataset("MMS_gvSrnItemDetails", new SqlParameter[] { new SqlParameter("@SRNID", SRNID) });
             }
             public static DataSet MMS_SrnGridItemDetailsFupload(int EDNID)
             {

                 return SQLDBUtil.ExecuteDataset("MMS_SrnGridItemDetailsFupload", new SqlParameter[] { new SqlParameter("@EDNID", EDNID) });

             }
            public static DataSet MMS_gvAllSRNsBind(DataSet dsSRNs, int Id)
             {
                 return SQLDBUtil.ExecuteDataset("MMS_gvAllSRNsBind", new SqlParameter[] { new SqlParameter("@SRNIDs", dsSRNs.GetXml()), new SqlParameter("@Id", Id) });
             }
             public static DataSet GetMMS_GridItemDetailsFupload(int EDNID)
             {
                 try
                 {
                     return SQLDBUtil.ExecuteDataset("MMS_GridItemDetailsFupload", new SqlParameter[] { new SqlParameter("@EDNID", EDNID) });
                 }
                 catch (Exception e)
                 {
                     throw e;
                 }
            }
             public static int MMS_InsertSRNs(int SRNId, int CreatedBy, DateTime CreatedOn, int VId, int Vendor_ID, int Origin, int Destination, int DestRepr, string TripSheet, int WO, int WorkSite, DateTime StartDate, int OriginRepr, int ModuleId)
             {
                 SqlParameter[] p = new SqlParameter[15];
                 p[0] = new SqlParameter("@CreatedBy", CreatedBy);
                 p[1] = new SqlParameter("@Destination", Destination);
                 if (VId == 0)
                     p[2] = new SqlParameter("@VId", SqlDbType.Int);
                 else
                     p[2] = new SqlParameter("@VId", VId);
                 p[3] = new SqlParameter("@Vendor_ID", Vendor_ID);

                 p[4] = new SqlParameter("@Origin", Origin);
                 p[5] = new SqlParameter("@DestRepr", DestRepr);
                 if (TripSheet != null)
                     p[6] = new SqlParameter("@TripSheet", TripSheet);
                 else
                     p[6] = new SqlParameter("@TripSheet", SqlDbType.VarChar);

                 p[7] = new SqlParameter("@WO", WO);
                 p[8] = new SqlParameter("@StartDate", StartDate);
                 if (OriginRepr == 0)
                     p[9] = new SqlParameter("@OriginRepr", SqlDbType.Int);
                 else
                     p[9] = new SqlParameter("@OriginRepr", OriginRepr);
                 p[10] = new SqlParameter("@CreatedOn", CreatedOn);
                 p[11] = new SqlParameter("@RetrunValue", SqlDbType.Int);
                 p[11].Direction = ParameterDirection.ReturnValue;
                 if (SRNId != 0)
                     p[12] = new SqlParameter("@SRNId", SRNId);
                 else
                     p[12] = new SqlParameter("@SRNId", SqlDbType.Int);

                 p[13] = new SqlParameter("@WorkSite", WorkSite);
                 p[14] = new SqlParameter("@ModuleId", ModuleId);

                 SQLDBUtil.ExecuteNonQuery("MMS_InsertSRNs", p);
                 int val;
                 return val = Convert.ToInt32(p[11].Value);
             }
             public static int MMS_UpdateBalqtyonPurchaseOrderdetails(int POdetId, double Balqty)
             {
                 SqlParameter[] p = new SqlParameter[3];
                 p[0] = new SqlParameter("@POdetId", POdetId);
                 p[1] = new SqlParameter("@Balqty", Balqty);
                 p[2] = new SqlParameter("@RetrunValue", SqlDbType.Int);
                 p[2].Direction = ParameterDirection.ReturnValue;

                 SQLDBUtil.ExecuteNonQuery("MMS_UpdateBalqtyonPurchaseOrderdetails", p);
                 int val;
                 return val = Convert.ToInt32(p[2].Value);
             }

             public static void MMS_Ins_Upd_SRNItems(int SRNItemID, int CreatedBy, int SRNID, int PodetID, int ResourceID, decimal Qty, int Au_ID, decimal ItemRate, decimal ReqQty)
             {
                 SqlParameter[] p = new SqlParameter[9];
                 p[0] = new SqlParameter("@CreatedBy", CreatedBy);
                 p[1] = new SqlParameter("@SRNItemID", SRNItemID);
                 p[2] = new SqlParameter("@SRNID", SRNID);
                 p[3] = new SqlParameter("@PodetID", PodetID);
                 p[4] = new SqlParameter("@ResourceID", ResourceID);
                 p[5] = new SqlParameter("@Qty", Qty);
                 p[6] = new SqlParameter("@Au_ID", Au_ID);
                 p[7] = new SqlParameter("@ItemRate", ItemRate);
                 p[8] = new SqlParameter("@ReqQty", ReqQty);

                 SQLDBUtil.ExecuteNonQuery("MMS_Ins_Upd_SRNItems", p);

             }

             public static void MMS_Upd_SRNItems(int SRNItemID, decimal Qty, decimal ReqQty)
             {
                 SqlParameter[] p = new SqlParameter[3];
                 p[0] = new SqlParameter("@SRNItemID", SRNItemID);
                 p[1] = new SqlParameter("@RcvdQty", Qty);
                 p[2] = new SqlParameter("@ReqQty", ReqQty);
                 SQLDBUtil.ExecuteNonQuery("MMS_UpdateSDNItemQtys", p);
             }
             public static int EMS_IncorporateMachine(int EDNID, int UserId, int Relation)
             {
                 SqlParameter[] sqlParams = new SqlParameter[4];
                 sqlParams[0] = new SqlParameter("@EdnID", EDNID);
                 sqlParams[1] = new SqlParameter("@EmpID", UserId);
                 sqlParams[2] = new SqlParameter("@Relation", Relation);
                 sqlParams[3] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
                 sqlParams[3].Direction = ParameterDirection.ReturnValue;
                 SQLDBUtil.ExecuteNonQuery("EMS_CreateMachineryFromEDNs", sqlParams);
                 return (int)sqlParams[3].Value;
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
             public static DataSet MMS_GETSRNITEMID(int SRNItemID)
             {
                
                 DataSet ds= SQLDBUtil.ExecuteDataset("MMS_GETSRNITEMID", new SqlParameter[] { new SqlParameter("@SRNItemID", SRNItemID) });
                 return ds;
             }
             public static void HR_InsUpReminder(int RID, int? SRNID, DateTime? ValidFrom, DateTime? ValidUpto, int RemindDays, string RemindText, int? WONO, int ModuleID, DateTime? DueDate)
             {
                 try
                 {
                     SqlParameter[] p = new SqlParameter[9];
                     p[0] = new SqlParameter("@RID", RID);
                     p[1] = new SqlParameter("@SRNID", SRNID);
                     p[2] = new SqlParameter("@ValidFrom", ValidFrom);
                     p[3] = new SqlParameter("@ValidUpto", ValidUpto);
                     p[4] = new SqlParameter("@RemindDays", RemindDays);
                     p[5] = new SqlParameter("@RemindText", RemindText);
                     p[6] = new SqlParameter("@WONO", WONO);
                     p[7] = new SqlParameter("@ModuleID", ModuleID);
                     p[8] = new SqlParameter("@DueDate", DueDate);
                     SQLDBUtil.ExecuteNonQuery("HR_InsUpReminder", p);
                 }
                 catch
                 {

                 }
             }

             public static int HR_InsUpdReminder(int RID, int? SRNID, DateTime? ValidFrom, DateTime? ValidUpto, int RemindDays, string RemindText, int? WONO, int ModuleID, DateTime? DueDate)
             {
                 try
                 {
                     SqlParameter[] p = new SqlParameter[10];
                     p[0] = new SqlParameter("@RID", RID);
                     p[1] = new SqlParameter("@SRNID", SRNID);
                     p[2] = new SqlParameter("@ValidFrom", ValidFrom);
                     p[3] = new SqlParameter("@ValidUpto", ValidUpto);
                     p[4] = new SqlParameter("@RemindDays", RemindDays);
                     p[5] = new SqlParameter("@RemindText", RemindText);
                     p[6] = new SqlParameter("@WONO", WONO);
                     p[7] = new SqlParameter("@ModuleID", ModuleID);
                     p[8] = new SqlParameter("@DueDate", DueDate);
                     p[9] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
                     p[9].Direction = ParameterDirection.ReturnValue;
                     SQLDBUtil.ExecuteNonQuery("HR_InsUpReminder", p);
                     return Convert.ToInt32(p[9].Value);
                 }
                 catch (Exception e)
                 {
                     throw e;
                 }
             }

          public static DataSet HR_GetReminders(int ModuleID)
             {
                
                 DataSet ds= SQLDBUtil.ExecuteDataset("HR_GetReminders", new SqlParameter[] { new SqlParameter("@ModuleID", ModuleID) });
                 return ds;
             }
           public static DataSet HR_DelReminder(int RID)
             {
                
                 DataSet ds= SQLDBUtil.ExecuteDataset("HR_DelReminder", new SqlParameter[] { new SqlParameter("@RID", RID) });
                 return ds;
             }
          public static DataSet HR_GetReminderByID(int RID)
             {
                
                 DataSet ds= SQLDBUtil.ExecuteDataset("HR_GetReminderByID", new SqlParameter[] { new SqlParameter("@RID", RID) });
                 return ds;
             }
          public static DataSet HR_GetReminderBySRND(int SRNID)
             {
                
                 DataSet ds= SQLDBUtil.ExecuteDataset("HR_GetReminderBySRND", new SqlParameter[] { new SqlParameter("@SRNID", SRNID) });
                 return ds;
             }
         
         }
    }

