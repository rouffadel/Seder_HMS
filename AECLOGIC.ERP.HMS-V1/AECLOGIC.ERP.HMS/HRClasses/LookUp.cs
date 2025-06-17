using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Drawing;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using DataAccessLayer;
using System.Web.Caching;
using Aeclogic.Common.DAL;
/// <summary>
/// Summary description for LookUp
/// </summary>
public class LookUp
{
    public LookUp()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public enum EntityRelationships
    {
        Resources = 1,
        SpaceType,
        StockPlanType,
        StockPlan,
        SpaceDesign,
        EmployeeMaster,
        AU,
        DespRqmtType,
        TptVehicle,
        VendorDetails,
        PurchaseOrder,
        Drivers,
        TransitGuidlines,
        AcmParts,
        RankMaster,
        ClassingMaster,
        SubResources,
        LendingType,
        ToolingType,
        WorkSite,
        Locations,
        WorkOrder,
        GdnItems,
        Code,
        Mode,
        GDN,
        Executive,
        Security,
        Qa,
        VendorInfo,
        VendorWO,
        ServiceVendor
    }
    const double cacheDuration = 120;
    public enum GoodsStatus
    {
        SupplyerYard = 1,
        Dispatch,
        Arrival,
        QA,
        Recive,
        Rejected,
        Transfer
    }
    public enum SaleOrderType
    {
        Goods= 1,
        Services
    }
    public enum RequestStatusDescriptions
    {
        NotSubmitted = 1,
        Pending = 2,
        Approved = 3,
        Denied = 4,
        Cancelled = 5,
        Closed = 6,
        PartialClosed = 7
    }
    public enum ServiceStatus
    { 
        Waiting=1,
        Received,
        Approved
    }
    public enum TransMode
    {
       
        Transfer = 1,
        Close
    }
    public struct Entity
    {
        public int ID;
        public string Name;
    }
    public enum LookupCacheKeys
    {
        Materials,
        XConfig
    }
    public enum LookupSessionKeys
    {
        SavedSearchCriteria
    }
    public enum Roles
    {
        PM = 1,
        SM,
        QA,
        SE
    }
    public static List<MMS_DDL_EmployeeExecutiveResult> PopulateEntityDropDown(EntityRelationships EntityRelationship, int ID)
    {
        MMSIDataContext dc = new MMSIDataContext();
        switch (EntityRelationship)
        {
            case EntityRelationships.Executive:
                var Executive = dc.MMS_DDL_EmployeeExecutive(ID);
                return Executive.ToList();
            default:
                return null;
        }
    }


    public static byte[] UpLoadImageFile(string FileName, ref bool Alert)
    {
      
        byte[] content = null;
        if (FileName != string.Empty)
        {
            FileInfo imageInfo = new FileInfo(FileName.Trim());
            if (!imageInfo.Exists)
                Alert = true;
            else
            {

                content = new byte[imageInfo.Length];
                FileStream imagestream = imageInfo.OpenRead();
                imagestream.Read(content, 0, content.Length);
                imagestream.Close();
            }
        }
        else
            Alert = true;
        return content;
    }
    public static DataSet GetMaterialRecordsfoAcParts()
    {

        try
        {
           
            DataSet ds= (DataSet)HttpContext.Current.Cache["MMS_DDL_Resources"];
            if (ds== null)
            {
                ds= SQLDBUtil.ExecuteDataset("MMS_DDL_Resources");
                HttpContext.Current.Cache.Insert("MMS_DDL_Resources", ds, null, DateTime.Now.AddMinutes(1), Cache.NoSlidingExpiration);
            }
            return ds;
           
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static DataSet GetMaterialRecords()
    {

        try
        {
           
            DataSet ds= (DataSet)HttpContext.Current.Cache["EMS_GetResources"];
            if (ds== null)
            {
                ds= SQLDBUtil.ExecuteDataset("EMS_GetResources");
                HttpContext.Current.Cache.Insert("EMS_GetResources", ds, null, DateTime.Now.AddMinutes(1), Cache.NoSlidingExpiration);
            }
            return ds;

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

   
}


