using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using Aeclogic.Common.DAL;
/// <summary>
/// Summary description for DirectPO
/// </summary>
public static class DirectPO_Obj
{
    public static int InsertIndent(int empid, string projectid, string indent_number, int indenttype, int PrjId, SqlTransaction Trans, int CompanyID)
    {
        SqlParameter[] p = new SqlParameter[8];
        p[0] = new SqlParameter("@ID", 1);
        if(empid!=0)
             p[1] = new SqlParameter("@EMPID", empid);
        else
            p[1] = new SqlParameter("@EMPID", SqlDbType.Int);

        p[2] = new SqlParameter("@ProjectId", projectid);//for Worksite
        p[3] = new SqlParameter("@Indent_Number", indent_number);
        p[4] = new SqlParameter("@Indent_Type", indenttype);
        p[5] = new SqlParameter("", 0); p[5].Direction = ParameterDirection.ReturnValue;
        if (PrjId != 0)
            p[6] = new SqlParameter("@PrjId", PrjId); //for project
        else
            p[6] = new SqlParameter("@PrjId", SqlDbType.Int);
        if (CompanyID != null)
            p[7] = new SqlParameter("@CompanyID", CompanyID);
        else
            p[7] = new SqlParameter("@CompanyID", SqlDbType.Int);
        SqlHelper.ExecuteNonQuery(Trans, "PM_DIRECTPO_INSERT_INDENT", p);
        int Indentid = 0;
        Indentid = Convert.ToInt32(p[5].Value);
        return Indentid;
    }

    public static int InsertIndentItems(string itemid, string indentid,int uom, double qty, string spec, string purpose, DateTime requiredon, double amt,int sUom, SqlTransaction Trans)
    {
        SqlParameter[] p = new SqlParameter[10];
        p[0] = new SqlParameter("@ID", 2);
        p[1] = new SqlParameter("@INDENTID", indentid);
        p[2] = new SqlParameter("@QTY", qty);
        p[3] = new SqlParameter("@SPEC", spec);
        p[4] = new SqlParameter("@PURPOSE", purpose);
        p[5] = new SqlParameter("@REQUIREDON", requiredon);
        p[6] = new SqlParameter("@AMOUNT", amt);
        p[7] = new SqlParameter("@ITEMID", itemid);
        p[8] = new SqlParameter("@AUID", uom);
        if(sUom!=0)
        p[9] = new SqlParameter("@AUIDs", sUom);
        else
            p[9] = new SqlParameter("@AUIDs", SqlDbType.Int);


        int Indentitemid = SqlHelper.ExecuteNonQuery(Trans, "PM_DIRECTPO_INSERT_INDENT", p);
        return Indentitemid;
    }

    public static int InsertEnquiry(string INDENTID, int empid, string vendorid, SqlTransaction Trans)
    {
        SqlParameter[] p = new SqlParameter[5];
        p[0] = new SqlParameter("@ID", 1);
        p[1] = new SqlParameter("@INDENTID", INDENTID);
        if(empid!=0)
            p[2] = new SqlParameter("@EMPID", empid);
        else
            p[2] = new SqlParameter("@EMPID", SqlDbType.Int);

        p[3] = new SqlParameter("@vendorid", vendorid);
        p[4] = new SqlParameter("", 0); p[4].Direction = ParameterDirection.ReturnValue;

        int Indentitemid = SqlHelper.ExecuteNonQuery(Trans, "PM_DIRECTPO_INSERT_ENQUIRY", p);
        Indentitemid = Convert.ToInt32(p[4].Value);
        return Indentitemid;
    }

    public static int InsertEnq(string INDENTID, int empid, int vendorid, SqlTransaction Trans)
    {
        SqlParameter[] p = new SqlParameter[5];
        p[0] = new SqlParameter("@ID", 1);
        p[1] = new SqlParameter("@INDENTID", INDENTID);
        if (empid != 0)
            p[2] = new SqlParameter("@EMPID", empid);
        else
            p[2] = new SqlParameter("@EMPID", SqlDbType.Int);

        p[3] = new SqlParameter("@vendorid", vendorid);
        p[4] = new SqlParameter("", 0); p[4].Direction = ParameterDirection.ReturnValue;

        int Indentitemid = SqlHelper.ExecuteNonQuery(Trans, "PM_DIRECTPO_INSERT_ENQUIRY", p);
        Indentitemid = Convert.ToInt32(p[4].Value);
        return Indentitemid;
    }

    public static int InsertOffer(string Enqid, string vendorid, SqlTransaction Trans, int empid, decimal TDS, string Deliverydesc, string PaymentDesc, string MOTDesc)
    {
        SqlParameter[] p = new SqlParameter[9];
        p[0] = new SqlParameter("@ID", 1);
        p[1] = new SqlParameter("@EnqId", Enqid);
        p[2] = new SqlParameter("@vendorid", vendorid);
        if(empid!=0)
            p[3] = new SqlParameter("@EMPID", empid);
        else
            p[3] = new SqlParameter("@EMPID", SqlDbType.Int);

        if (TDS != 0)
            p[5] = new SqlParameter("@TDS", TDS);
        else
            p[5] = new SqlParameter("@TDS", SqlDbType.Decimal);

        p[4] = new SqlParameter("", 0); p[4].Direction = ParameterDirection.ReturnValue;

        if (Deliverydesc!= "")
            p[6] = new SqlParameter("@DeliveryDesc", Deliverydesc);
        else
            p[6] = new SqlParameter("@DeliveryDesc", SqlDbType.VarChar);
        if (PaymentDesc!= "")
            p[7] = new SqlParameter("@PaymentDesc", PaymentDesc);
        else
            p[7] = new SqlParameter("@PaymentDesc", SqlDbType.VarChar);
        if (MOTDesc != "")
            p[8] = new SqlParameter("@TransportDesc", MOTDesc);
        else
            p[8] = new SqlParameter("@TransportDesc", SqlDbType.VarChar);
        SqlHelper.ExecuteNonQuery(Trans, "PM_DIRECTPO_INSERT_OFFER", p);
        int Offerid = 0;
        Offerid = Convert.ToInt32(p[4].Value);
        return Offerid;
    }

    public static void InsertOfferItems(string offerid, string Indentid, SqlTransaction Trans)
    {
        SqlParameter[] p = new SqlParameter[3];
        p[0] = new SqlParameter("@ID", 2);
        p[1] = new SqlParameter("@offerid", offerid);
        p[2] = new SqlParameter("@indentid", Indentid);
        SqlHelper.ExecuteNonQuery(Trans, "PM_DIRECTPO_INSERT_OFFER", p);
    }

    public static void InsertOfferTax(string taxid, double value, string offerid, SqlTransaction Trans, string ID)
    {
        SqlParameter[] p = new SqlParameter[4];
        p[0] = new SqlParameter("@ID", ID);
        p[1] = new SqlParameter("@offerid", offerid);
        p[2] = new SqlParameter("@taxid", taxid);
        p[3] = new SqlParameter("@rate", value);
        SqlHelper.ExecuteNonQuery(Trans, "PM_DIRECTPO_INSERT_OFFER", p);
    }

    public static void InsertOfferTerms(string termid, string Term, string offerid, SqlTransaction Trans)
    {
        SqlParameter[] p = new SqlParameter[4];
        p[0] = new SqlParameter("@ID", 5);
        p[1] = new SqlParameter("@offerid", offerid);
        p[2] = new SqlParameter("@term", Term);
        p[3] = new SqlParameter("@termid", termid);
        SqlHelper.ExecuteNonQuery(Trans, "PM_DIRECTPO_INSERT_OFFER", p);
    }

    public static int InsertPO(string offerid, int empid, string poname, double poval, SqlTransaction trans, int Paymenttype, DateTime? podate, int CompanyID)
    {
        SqlParameter[] p = new SqlParameter[9];
        p[0] = new SqlParameter("@ID", 1);
        p[1] = new SqlParameter("@OfferID", offerid);
        if(empid!=0)
            p[2] = new SqlParameter("@EMPID", empid);
        else
            p[2] = new SqlParameter("@EMPID", SqlDbType.Int);

        p[3] = new SqlParameter("@PONAME", poname);
        p[4] = new SqlParameter("@POValue", poval);
        p[5] = new SqlParameter("", 1); p[5].Direction = ParameterDirection.ReturnValue;
        p[6] = new SqlParameter("@PaymentType", Paymenttype);
        p[7] = new SqlParameter("@PoDt", podate);

        if (CompanyID != null)
            p[8] = new SqlParameter("@CompanyID", CompanyID);
        else
            p[8] = new SqlParameter("@CompanyID", SqlDbType.Int);
        SqlHelper.ExecuteNonQuery(trans, "PM_DIRECTPO_INSERT_PODETAILS", p);
        int POID = Convert.ToInt32(p[5].Value);
        return POID;
    }

    public static void InsertPODetails(string poid, string indentid, SqlTransaction trans)
    {
        SqlParameter[] p = new SqlParameter[3];
        p[0] = new SqlParameter("@ID", 2);
        p[1] = new SqlParameter("@POID", poid);
        p[2] = new SqlParameter("@indentid", indentid);
        SqlHelper.ExecuteNonQuery(trans, "PM_DIRECTPO_INSERT_PODETAILS", p);
    }

    public static DataSet GetParentGroups(int Type)
    {
        try
        {
            SqlParameter[] p = new SqlParameter[1];
            p[0] = new SqlParameter("@Type", Type);
         
            DataSet ds= SqlHelper.ExecuteDataset("PM_GetParentGroups", p);
            return ds;

        }
        catch (Exception ex)
        {

            throw ex;
        }
    }

    public static DataSet HMSGetParentGroups(int Type,int ModuleID)
    {
        try
        {
            SqlParameter[] p = new SqlParameter[2];
            p[0] = new SqlParameter("@Type", 0);
            p[1] = new SqlParameter("@ModuleID", 0);

         
            DataSet ds= SqlHelper.ExecuteDataset("HR_GetParentGroups", p);
            return ds;

        }
        catch (Exception ex)
        {

            throw ex;
        }
    }

}
