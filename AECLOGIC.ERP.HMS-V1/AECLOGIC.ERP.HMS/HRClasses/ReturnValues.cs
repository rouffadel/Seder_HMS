using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using Aeclogic.Common.DAL;
using System.Web.UI.HtmlControls;
using System.Configuration;
using DataAccessLayer;

public static class ReturnValues
{
    
    public static DataSet Company_decimalpoints(int companyId)
    {

       
        DataSet ds= SQLDBUtil.ExecuteDataset("Get_No_decimal", new SqlParameter[] { new SqlParameter("@CompanyID", companyId) });
        return ds;

    }

    public static string GetCompanyName(int companyId)
    {
        string CompanyName = string.Empty;
        SqlParameter[] parms = new SqlParameter[1];
        parms[0] = new SqlParameter("@CompanyID", companyId);
        SqlDataReader dr = SqlHelper.ExecuteReader("Get_Companies", parms);
        if (dr.HasRows)
        {
            dr.Read();
            CompanyName = dr[1].ToString();
        }
        dr.Close();
        return CompanyName;
    }

    public static string Get_Out(int OutPutIndex, string Query, params SqlParameter[] commandParameters)
    {
        SqlDataReader Dr;
        string Return_Output = string.Empty;
        Dr = SqlHelper.ExecuteReader(Query, commandParameters);
        if (Dr.HasRows)
        {
            Dr.Read();
            Return_Output = Dr[OutPutIndex].ToString();
        }
        Dr.Close();
        return Return_Output;
    }

   

    public static string OptionVisibility(int companyid, int option)
    {
        SqlParameter[] parms = new SqlParameter[3];
        parms[0] = new SqlParameter("@ID", 1);
        parms[1] = new SqlParameter();
        parms[1].ParameterName = "@companyid";
        parms[1].Value = companyid;
        parms[2] = new SqlParameter("@OPTION", option);
        string Vis = ReturnValues.Get_Out(2, "ACC_USEROPTIONS", parms);
        return Vis;
    }

   
    public static bool AllowedFormats(string Extension)
    {
        bool val = false;
        string[] Formats = { ".PNG", ".JPG", ".JPEG", ".BMP", ".GIF" };
        for (int i = 1; i < Formats.Length; i++)
        {
            if (Extension.ToUpper() == Formats[i].ToString())
            {
                val = true;
                break;
            }
        }
        return val;
    }

}


public class Enumrations
{
    public const string Contra = "0000";
    public const string Journal = "0001";
    public const string Payment = "0002";
    public const string Receipt = "0003";
    public const string Reversal = "0004";
    public const string PettyAdvances = "0005";
    public const string DebitNote = "0006";
    public const string CreditNote = "0007";
    public const string Sales = "0008";
    public const string Purchase = "0009";


    public const string Save_Voucher = "Voucher Saved Successfully";
    public const string Update_Voucher = "Voucher Updated Successfully";

    public enum VocherTypes
    {
        Journal = 1, Contra = 2, Payment = 3, Receipt = 4, Reversal = 5, PettyAdvances = 6, DebitNote = 7, CreditNote = 8, Sales = 9, Purchase = 10
    }

    public const int Cost_Center = 1;
    public const int Allow_Zero_Entries = 2;
    public const int Ask_Contact_for_ledger = 4;
    public const int Use_Payment_Receipt_As_Contra = 5;
    public const int Allow_Cash_in_Journals = 6;
    public const int Warn_on_NegativeCashBalance = 7;
    public const int Cost_Center_for_Ledger = 8;

    public enum UserOptions
    {
        Cost_Center = 1,
        Allow_Zero_Entries = 2,
        Ask_Contact_for_ledger = 4,
        Use_Payment_Receipt_As_Contra = 5,
        Allow_Cash_in_Journals = 6,
        Warn_on_NegativeCashBalance = 7,
        Cost_Center_for_Ledger = 8
    }
}