using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Aeclogic.Common.DAL;

namespace AECLOGIC.ERP.HMS
{

    public class ProcDept
    {
        private int _RoleID;
        private int _MenuId;
        private int _ModuleId;
        private string _URL;
        public string URL
        {
            get { return _URL; }
            set { _URL = value; }

        }
        public int ModuleId
        {
            get { return _ModuleId; }
            set { _ModuleId = value; }

        }
        public int RoleID
        {
            get { return _RoleID; }
            set { _RoleID = value; }

        }
        public int MenuId
        {
            get { return _MenuId; }
            set { _MenuId = value; }
        }
        public static DataSet GetGroupIDByResource(int ResourceID, int CatType)
        {
            try
            {
                SqlParameter[] p = new SqlParameter[2];
                p[0] = new SqlParameter("@ResId", ResourceID);
                p[1] = new SqlParameter("@CatType", CatType);
              
                DataSet ds= SqlHelper.ExecuteDataset("MMS_GetGroupIdByResource", p);
                return ds;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public static DataSet GetAssetTpeIDByResource(int ResourceID, int Type)
        {
            try
            {
                SqlParameter[] p = new SqlParameter[2];
                p[0] = new SqlParameter("@ResId", ResourceID);
                p[1] = new SqlParameter("@IsGoods", Type);
              
                DataSet ds= SqlHelper.ExecuteDataset("MMS_GetAssetTypeIdByResource", p);
                return ds;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public DataSet GetItemDescription()
        {
            try
            {
              
                DataSet ds= SqlHelper.ExecuteDataset("GetItemDescription");
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public DataSet GetEmployeeDetails()
        {
            try
            {
              
                DataSet ds= SqlHelper.ExecuteDataset("SP_PM_GetEmployeeDetails");
                return ds;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public DataSet GetVendorDetails(int vendorid)
        {
            try
            {
                SqlParameter[] p = new SqlParameter[1];
                p[0] = new SqlParameter("@vendorid", vendorid);

              
                DataSet ds= SqlHelper.ExecuteDataset("GetVendorDetails", p);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }


        public DataSet ViewVendorDetails(int vendorid)
        {
            try
            {
                SqlParameter[] p = new SqlParameter[1];
                p[0] = new SqlParameter("@vendorid", vendorid);

              
                DataSet ds= SqlHelper.ExecuteDataset("PMS_ViewVendorDetails", p);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public DataSet GetIndents(int Indentid)
        {
            try
            {
                SqlParameter[] p = new SqlParameter[1];
                p[0] = new SqlParameter("@IndentId", Indentid);
              
                DataSet ds= SqlHelper.ExecuteDataset("SP_PM_GetIndents", p);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public DataSet GetItemDescriptionByID(int Item_ID)
        {
            try
            {
                SqlParameter[] p = new SqlParameter[1];
                p[0] = new SqlParameter("@item_id", Item_ID);
              
                DataSet ds= SqlHelper.ExecuteDataset("GetItemDescriptionByID", p);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public DataSet GetAuDetails()
        {
            try
            {
              
                DataSet ds= SqlHelper.ExecuteDataset("GetAu");
                DataRow dr = ds.Tables[0].NewRow();
                dr["AU_ID"] = 0;
                dr["AU_Name"] = "None";
                ds.Tables[0].Rows.InsertAt(dr, 0);
                ds.AcceptChanges();
                return ds;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public DataSet GetUnitsByResource(int ResId, int? IndentType)
        {
            try
            {
                SqlParameter[] p = new SqlParameter[2];
                p[0] = new SqlParameter("@ResId", ResId);
                p[1] = new SqlParameter("@IndentType", IndentType);
              
                DataSet ds= SqlHelper.ExecuteDataset("GEN_GetUnitsByResource", p);
                DataRow dr = ds.Tables[0].NewRow();
                dr["ID"] = 0;
                dr["Name"] = "--Select--";
                ds.Tables[0].Rows.InsertAt(dr, 0);
                ds.AcceptChanges();
                return ds;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public DataSet GetUnitsByResource(int ResId)
        {
            try
            {
                return GetUnitsByResource(ResId, null);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public DataSet GetWorkSiteDetails()
        {
            try
            {
              
                DataSet ds= SqlHelper.ExecuteDataset("PM_GetWorkSites");
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public DataSet GetCategories(int Category_ID)
        {
            try
            {
                SqlParameter[] p = new SqlParameter[1];
                p[0] = new SqlParameter("@CategoryID", Category_ID);
              
                DataSet ds= SqlHelper.ExecuteDataset("SP_PM_GetCategories", p);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet GetBankId()
        {
            try
            {
                SqlParameter[] p = new SqlParameter[4];
                p[0] = new SqlParameter("@id", 4);
                p[1] = new SqlParameter("@CurrentPage", 1);
                p[2] = new SqlParameter("@PageSize", 100);
                p[3] = new SqlParameter("@NoofRecords", 100);
              
                DataSet ds= SqlHelper.ExecuteDataset("CMS_BankMaster_Insert_Update_Delete_Select", p);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public DataSet SearchItems(string ItemDesc)
        {
            try
            {
                SqlParameter[] p = new SqlParameter[1];
                p[0] = new SqlParameter("@Item", ItemDesc);
              
                DataSet ds= SqlHelper.ExecuteDataset("SP_PM_SearchItems", p);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet SearchVendor(string ItemDesc)
        {
            try
            {
                SqlParameter[] p = new SqlParameter[1];
                p[0] = new SqlParameter("@Vendor", ItemDesc);
              
                DataSet ds= SqlHelper.ExecuteDataset("SP_PM_SearchVendor", p);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet Validate_Login(string Login_Name, string Password, string MachineName, int ModuleId, string HostIP, string CompanyName)
        {
            try
            {
              
                SqlParameter[] objParam = new SqlParameter[6];
                objParam[0] = new SqlParameter("@Login_Name", Login_Name);
                objParam[1] = new SqlParameter("@Password", Password);
                objParam[2] = new SqlParameter("@MacName", MachineName);
                objParam[3] = new SqlParameter("@ModuleId", ModuleId);
                objParam[4] = new SqlParameter("@HostIP", HostIP);
                objParam[5] = new SqlParameter("@CompanyName", CompanyName);

                DataSet ds= SqlHelper.ExecuteDataset("CP_Login", objParam);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static DataSet GetLoginSessions(string Login_Name, int ModuleId)
        {
            try
            {
              
                SqlParameter[] objParam = new SqlParameter[2];
                objParam[0] = new SqlParameter("@Login_Name", Login_Name);
                objParam[1] = new SqlParameter("@ModuleId", ModuleId);
                DataSet ds= SqlHelper.ExecuteDataset("CP_GetLoginSessions", objParam);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void GetLogoutDetails(int EmpID, string HostIP)
        {
            try
            {
              
                SqlParameter[] objParam = new SqlParameter[2];
                objParam[0] = new SqlParameter("@EmpID", EmpID);
                objParam[1] = new SqlParameter("@HostIP", HostIP);
                SqlHelper.ExecuteNonQuery("CP_LogOut", objParam);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int AddAu(string Auname, string PUId, int UserId)
        {
            try
            {
                SqlParameter[] p = new SqlParameter[3];
                p[0] = new SqlParameter("@AuName", Auname);
                if (PUId != "0")
                    p[1] = new SqlParameter("@PUID", PUId);
                else
                    p[1] = new SqlParameter("@PUID", SqlDbType.Int);

                p[2] = new SqlParameter("@UserId", UserId);
              
                int i = SqlHelper.ExecuteNonQuery("AddAU", p);
                return i;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int AddCommonTerm(string Term, string Enq_QuotID)
        {
            try
            {
                SqlParameter[] p = new SqlParameter[2];
                p[0] = new SqlParameter("@Term", Term);
                p[1] = new SqlParameter("@Enq_QuotID", Enq_QuotID);
                int i = SqlHelper.ExecuteNonQuery("SP_PM_AddCommonTerms", p);
                return i;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int AddItem(string ItemDesc, int Category_ID, int Uom, decimal BasicRate, string Extension, int Days)
        {
            try
            {

                SqlParameter[] objParam = new SqlParameter[7];
                objParam[0] = new SqlParameter("@ItemDesc", ItemDesc);
                objParam[1] = new SqlParameter("@CategoryID", Category_ID);
                objParam[2] = new SqlParameter("@Uom", Uom);
                objParam[3] = new SqlParameter("@BasicRate", BasicRate);
                objParam[4] = new SqlParameter("@Extension", Extension);
                objParam[5] = new SqlParameter();
                objParam[5].Direction = ParameterDirection.ReturnValue;
                objParam[6] = new SqlParameter("@Days", Days);
                SqlHelper.ExecuteNonQuery("PM_InsertResource", objParam);
                int i = Convert.ToInt32(objParam[5].Value);

                return i;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int AddItems(int id, int val, int GroupId, int ServiceId, string ItemDesc, int Category_ID, int Uom, decimal BasicRate, string Extension, out int j, out int k)
        {                               //,
            try
            {

                SqlParameter[] objParam = new SqlParameter[11];

                objParam[0] = new SqlParameter("@ItemDesc", ItemDesc);
                objParam[1] = new SqlParameter("@CategoryID", Category_ID);
                objParam[2] = new SqlParameter("@Uom", Uom);
                objParam[3] = new SqlParameter("@BasicRate", BasicRate);
                objParam[4] = new SqlParameter("@Extension", Extension);
                objParam[6] = new SqlParameter("@id", id);

                objParam[10] = new SqlParameter("@val", val);

                objParam[7] = new SqlParameter("@GroupId", GroupId);
                objParam[8] = new SqlParameter("@serviceId", ServiceId);
                objParam[9] = new SqlParameter("@Groupresid", SqlDbType.Int);
                objParam[9].Direction = ParameterDirection.Output;
                objParam[5] = new SqlParameter("@Serviceresid", SqlDbType.Int);
                objParam[5].Direction = ParameterDirection.Output;
                SqlHelper.ExecuteNonQuery("PM_Insert_Resource", objParam);
                j = Convert.ToInt32(objParam[9].Value);
                k = Convert.ToInt32(objParam[5].Value);
                return j;
                return k;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int AssignIndent(int IndentID, int AssignedTo)
        {
            try
            {
                SqlParameter[] p = new SqlParameter[2];
                p[0] = new SqlParameter("@IndentID", IndentID);
                p[1] = new SqlParameter("@AssignedTo", AssignedTo);
                int i = SqlHelper.ExecuteNonQuery("SP_PM_AssignIndent", p);
                return i;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int AddIndent(int RaisedBy, int ProjectID, string IndentNo, int Rbval, string deptid, string req, int PrjId, int CompanyID)
        {
            try
            {
                SqlParameter[] p = new SqlParameter[9];
                if (RaisedBy != 0)
                    p[0] = new SqlParameter("@RaisedBy", RaisedBy);
                else
                    p[0] = new SqlParameter("@RaisedBy", SqlDbType.Int);

                p[1] = new SqlParameter("@ProjectId", ProjectID); // for Worksite
                p[2] = new SqlParameter("@Indent_Number", IndentNo);
                p[3] = new SqlParameter("@IndentID", 2);
                p[3].Direction = ParameterDirection.Output;
                p[4] = new SqlParameter("@Indent_Type", Rbval);
                p[5] = new SqlParameter("@DeptId", deptid);
                if (req != "")
                    p[6] = new SqlParameter("@MessageRequest", req);
                else
                    p[6] = new SqlParameter("@MessageRequest", SqlDbType.VarChar);
                if (PrjId != 0)
                    p[7] = new SqlParameter("@PrjId", PrjId);// for Project
                else
                    p[7] = new SqlParameter("@PrjId", SqlDbType.Int);

                if (CompanyID != null)
                    p[8] = new SqlParameter("@CompanyID", CompanyID);
                else
                    p[8] = new SqlParameter("@CompanyID", SqlDbType.Int);

                int i = SqlHelper.ExecuteNonQuery("SP_PM_AddIndent", p);
                return Convert.ToInt32(p[3].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int EditItemDetails(int Item_Id, string Item_Desc, int Category_ID)
        {
            try
            {
                SqlParameter[] objParam = new SqlParameter[3];
                objParam[0] = new SqlParameter("@Item_Id", Item_Id);
                objParam[1] = new SqlParameter("@Item_Desc", Item_Desc);
                objParam[2] = new SqlParameter("@CategoryID", Category_ID);
                int i = SqlHelper.ExecuteNonQuery("EditItemDetails", objParam);
                return i;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int EditCategory(int Category_ID, string CategoryName, int id, int assettypeid, int ModuleId)
        {
            try
            {
                SqlParameter[] objParam = new SqlParameter[5];
                objParam[0] = new SqlParameter("@CategoryID", Category_ID);
                objParam[1] = new SqlParameter("@CategoryName", CategoryName);
                objParam[2] = new SqlParameter("@id", id);
                objParam[3] = new SqlParameter("@assettypeid", assettypeid);
                if (ModuleId != 0)
                    objParam[4] = new SqlParameter("@ModuleId", ModuleId);
                else
                    objParam[4] = new SqlParameter("@ModuleId", SqlDbType.Int);

                int i = SqlHelper.ExecuteNonQuery("SP_PM_EditCategory", objParam);
                return i;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int EditUOM(int AuID, string AuName, string PUId, int UserId)
        {
            try
            {
                SqlParameter[] objParam = new SqlParameter[4];
                objParam[0] = new SqlParameter("@AUID", AuID);
                objParam[1] = new SqlParameter("@AUName", AuName);
                if (PUId != "0")
                    objParam[2] = new SqlParameter("@PUId", PUId);
                else
                    objParam[2] = new SqlParameter("@PUId", SqlDbType.Int);
                objParam[3] = new SqlParameter("@UserId", UserId);
                int i = SqlHelper.ExecuteNonQuery("PM_EditUOM", objParam);
                return i;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int EditTaxes(int TaxID, string TaxName, int TaxType, int flag, int applicable)
        {
            try
            {
                SqlParameter[] objParam = new SqlParameter[5];
                objParam[0] = new SqlParameter("@TaxID", TaxID);
                objParam[1] = new SqlParameter("@TaxName", TaxName);
                objParam[2] = new SqlParameter("@TaxType", TaxType);
                objParam[3] = new SqlParameter("@flag", flag);
                objParam[4] = new SqlParameter("@Applicable", applicable);

                int i = SqlHelper.ExecuteNonQuery("PM_EditTaxes", objParam);
                return i;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int EditVendorDetails(string Vendorname, string VendorDetails, string contactperson, string mobile, string phone, string fax, string email, int vendor_id, int BranchId, string acc, string tin, int ModifiedBy, int StateId, int BankId, string IFSC, string PayableACName, string PANCardNo, decimal? CreditLimit, decimal? DebitLimit, string ShortName)// int CountryId)string NewCountry,string NewState)
        {
            try
            {
                SqlParameter[] objParam = new SqlParameter[21];

                objParam[0] = new SqlParameter("@vendorname", Vendorname);
                objParam[1] = new SqlParameter("@vendorAddr", VendorDetails);
                objParam[2] = new SqlParameter("@contact_person", contactperson);
                objParam[3] = new SqlParameter("@mobile", mobile);
                objParam[4] = new SqlParameter("@phone", phone);
                objParam[5] = new SqlParameter("@fax", fax);
                objParam[6] = new SqlParameter("@email", email);
                objParam[7] = new SqlParameter("@Vendor_ID", vendor_id);
               
                objParam[8] = new SqlParameter("@BranchName", SqlDbType.VarChar);
                if (acc != "")
                    objParam[9] = new SqlParameter("@acc", acc);
                else
                    objParam[9] = new SqlParameter("@acc", SqlDbType.VarChar);
                objParam[10] = new SqlParameter("@tin", tin);
                if (ModifiedBy != 0)
                    objParam[11] = new SqlParameter("@EmpID", ModifiedBy);
                else
                    objParam[11] = new SqlParameter("@EmpID", SqlDbType.Int);

                objParam[12] = new SqlParameter("@StateId", StateId);
              
                if (BankId != 0)
                    objParam[13] = new SqlParameter("@BankId", BankId);
                else
                    objParam[13] = new SqlParameter("@BankId", SqlDbType.Int);

                if (IFSC != "")
                    objParam[14] = new SqlParameter("@IFSC", IFSC);
                else
                    objParam[14] = new SqlParameter("@IFSC", SqlDbType.VarChar);

                if (PayableACName != "")
                    objParam[15] = new SqlParameter("@PayACName", PayableACName);
                else
                    objParam[15] = new SqlParameter("@PayACName", SqlDbType.VarChar);
                if (PANCardNo != "")
                    objParam[16] = new SqlParameter("@PANCardNo", PANCardNo);
                else
                    objParam[16] = new SqlParameter("@PANCardNo", SqlDbType.VarChar);

                if (DebitLimit != null)
                    objParam[17] = new SqlParameter("@DebitLimit", DebitLimit);
               
                if (CreditLimit != null)
                    objParam[18] = new SqlParameter("@CreditLimit", CreditLimit);
                if (ShortName != "")
                    objParam[19] = new SqlParameter("@ShortName", ShortName);
                else
                    objParam[19] = new SqlParameter("@ShortName", SqlDbType.VarChar);
                if (BranchId != 0)
                    objParam[20] = new SqlParameter("@BranchId", BranchId);
                else
                    objParam[20] = new SqlParameter("@BranchId", SqlDbType.Int);
                int i = SqlHelper.ExecuteNonQuery("EditVendorDetails", objParam);
                return i;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static int UserNameAvailable(string UserName)
        {
            try
            {
                int result;
                SqlParameter[] sqlParams = new SqlParameter[2];
                sqlParams[0] = new SqlParameter("@Username", UserName);
                sqlParams[1] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
                sqlParams[1].Direction = ParameterDirection.ReturnValue;
                SqlHelper.ExecuteNonQuery("G_CheckSupplierUsername", sqlParams);
                result = Convert.ToInt16(sqlParams[1].Value);
                return result;


            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public static DataSet GetAllowed(int RoleId, int ModuleId, string URL)
        {
          
            SqlParameter[] objParam = new SqlParameter[3];
            objParam[0] = new SqlParameter("@RoleId", RoleId);
            objParam[1] = new SqlParameter("@ModuleId", ModuleId);
            objParam[2] = new SqlParameter("@URL", URL);
            DataSet ds= SqlHelper.ExecuteDataset("CP_GetPageAccess", objParam);
            return ds;
        }

       

        public static DataSet GetLinks(int MenuID, int RoleID)
        {
            try
            {
                SqlParameter[] p = new SqlParameter[2];
                p[0] = new SqlParameter("@MenuID", MenuID);
                p[1] = new SqlParameter("@RoleID", RoleID);
              
                DataSet ds= SqlHelper.ExecuteDataset("GetLinks", p);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet GetMenus(int ModuleId)
        {
          
            SqlParameter[] sqlparam = new SqlParameter[1];
            sqlparam[0] = new SqlParameter("@ModuleId", ModuleId);
            DataSet ds= SqlHelper.ExecuteDataset("CP_Get_MenusListForGrid", sqlparam);
            return ds;
        }

        public DataSet GetMenus()
        {
          
            DataSet ds= SqlHelper.ExecuteDataset("CP_Get_MenusListForGrid");
            return ds;
        }


        public static DataSet GetRolesList(int Moduleid)
        {
            try
            {
              
                DataSet ds= SqlHelper.ExecuteDataset("[PMs_Get_RolesList]", new SqlParameter[] { new SqlParameter("@ModuleId", Moduleid) });
                return ds;                

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        #region CREATE USER ROLES

        public static DataSet CreateUserRoles(int? EmpID, int? ModuleId, int? RoleID, int? UserID, double polimit, double daypolimit, double brfactor, double arfactor)
        {
            try
            {
              

                SqlParameter[] parmss = new SqlParameter[8];
                parmss[0] = new SqlParameter("@EmpID", EmpID);
                parmss[7] = new SqlParameter("@RoleID", RoleID);
                parmss[1] = new SqlParameter("@ModuleId", ModuleId);
                if (UserID != 0)
                    parmss[2] = new SqlParameter("@UserID", UserID);
                else
                    parmss[2] = new SqlParameter("@UserID", SqlDbType.Int);

                parmss[3] = new SqlParameter("@polimit", polimit);
                parmss[4] = new SqlParameter("@podaylimit", daypolimit);
                parmss[5] = new SqlParameter("@BillReductionFactor", brfactor);
                parmss[6] = new SqlParameter("@AdvReductionFactor", arfactor);
                DataSet ds= SqlHelper.ExecuteDataset("[PMS_InsUpdate_UserRoles]", parmss);// 
                return ds;                 
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        #endregion

        public static DataSet SearchEmpList(int? siteId, int? DeptId, string Name, int? EmpId, int? ModuleID, Paging_Objects Obj)
        {
            try
            {
                SqlParameter[] p = new SqlParameter[9];
                p[0] = new SqlParameter("@WSId", siteId);
                p[1] = new SqlParameter("@depId", DeptId);
                p[2] = new SqlParameter("@EmpName", Name);
                p[3] = new SqlParameter("@EmpId", EmpId);
                p[4] = new SqlParameter("@ModuleId", ModuleID);
                p[5] = new SqlParameter("@CurrentPage", Obj.CurrentPage);
                p[6] = new SqlParameter("@PageSize", Obj.PageSize);
                p[7] = new SqlParameter("@NoOfRecords", System.Data.SqlDbType.Int);
                p[7].Direction = ParameterDirection.Output;
                p[8] = new SqlParameter("returnvalue", System.Data.SqlDbType.Int);
                p[8].Direction = ParameterDirection.ReturnValue;
              
                DataSet ds= SqlHelper.ExecuteDataset("PMS_GetEmployeesByRoles", p);
                Obj.TotalPages = Convert.ToInt32(p[6].Value);
                Obj.NoofRecords= Convert.ToInt32(p[7].Value);
                return ds;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static DataSet EditEmployeeRole(int EmpId)
        {
          
            SqlParameter[] p = new SqlParameter[1];
            p[0] = new SqlParameter("@EmpId", EmpId);
            DataSet ds= SqlHelper.ExecuteDataset("PM_EditUserRoles", p);
            return ds;
        }

        public static void GetViewAll(int Empid)
        {
            try
            {
              
                SqlParameter[] p = new SqlParameter[1];
                p[0] = new SqlParameter("@EmpId", Empid);
                DataSet ds= SqlHelper.ExecuteDataset("PMS_GetViewAll", p);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public static DataSet Getstaus(int ModuleId, int RoleId, int EmpId)
        {
          
            SqlParameter[] p = new SqlParameter[3];
            p[0] = new SqlParameter("@ModuleId", ModuleId);
            p[1] = new SqlParameter("@RoleId", RoleId);
            p[2] = new SqlParameter("@EmpId", EmpId);
            DataSet ds= SqlHelper.ExecuteDataset("pm_getstatus", p);

            return ds;

        }

        public static DataSet status(int ModuleId, int MenuId, int RoleId)
        {
          

            SqlParameter[] p = new SqlParameter[3];
            p[0] = new SqlParameter("@moduleid", ModuleId);

            p[1] = new SqlParameter("@menuid", MenuId);

            p[2] = new SqlParameter("@roleid", RoleId);
            DataSet ds= SqlHelper.ExecuteDataset("pms_statusview", p);

            return ds;
        }

        #region Splliers UserName Password
        public static int Updatepassword(int EmpID, string OldPassWord, string NewPassWord)
        {
            try
            {
                SqlParameter[] objParam = new SqlParameter[4];
                objParam[0] = new SqlParameter("@EmpID", EmpID);
                objParam[1] = new SqlParameter("@OldPassword", OldPassWord);
                objParam[2] = new SqlParameter("@NewPassword", NewPassWord);
                objParam[3] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
                objParam[3].Direction = ParameterDirection.ReturnValue;
                SqlHelper.ExecuteNonQuery("G_UpdateSupplierpassword", objParam);
                return Convert.ToInt32(objParam[3].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet SearchSuppliersList(string vendorname, string mobile, Paging_Objects Obj)
        {
            try
            {
                SqlParameter[] p = new SqlParameter[6];
                p[0] = new SqlParameter("@vendor_name", vendorname);
                p[1] = new SqlParameter("@mobile", mobile);
                p[2] = new SqlParameter("@CurrentPage", Obj.CurrentPage);
                p[3] = new SqlParameter("@PageSize", Obj.PageSize);
                p[4] = new SqlParameter("@NoOfRecords", System.Data.SqlDbType.Int);
                p[4].Direction = ParameterDirection.Output;
                p[5] = new SqlParameter("returnvalue", System.Data.SqlDbType.Int);
                p[5].Direction = ParameterDirection.ReturnValue;
              

                DataSet ds= SqlHelper.ExecuteDataset("PMS_GetSuppliersList", p);
                Obj.TotalPages = Convert.ToInt32(p[3].Value);
                Obj.NoofRecords= Convert.ToInt32(p[4].Value);
                //{
                //    new SqlParameter("@vendor_name",vendorname),
                //    new SqlParameter("@mobile",mobile)
                //}
                //);
                return ds;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static int ResetUserNamePassword(int UserID, string Username, string NewPassWord)
        {
            try
            {
                SqlParameter[] objParam = new SqlParameter[4];
                objParam[0] = new SqlParameter("@UserID", UserID);
                objParam[1] = new SqlParameter("@Username", Username);
                objParam[2] = new SqlParameter("@NewPassWord", NewPassWord);
                objParam[3] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
                objParam[3].Direction = ParameterDirection.ReturnValue;
                SqlHelper.ExecuteNonQuery("G_ResetSupplierUserNamePassword", objParam);
                return Convert.ToInt32(objParam[3].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        public static DataSet GetVendors(String SearchKey)
        {
            try
            {
                SqlParameter[] sqlPrms = new SqlParameter[1];
                sqlPrms[0] = new SqlParameter("@SEARCH", SearchKey);
                return SqlHelper.ExecuteDataset("[MMS_Service_SearchVendorForAccount]", sqlPrms);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static DataSet GetVendorsByGroups(String SearchKey, string ResID)
        {
            try
            {
                SqlParameter[] sqlPrms = new SqlParameter[2];
                sqlPrms[0] = new SqlParameter("@SEARCH", SearchKey);
                if (ResID != "0")
                    sqlPrms[1] = new SqlParameter("@ResID", Convert.ToInt32(ResID));
                else
                    sqlPrms[1] = new SqlParameter("@ResID", SqlDbType.Int);
                return SqlHelper.ExecuteDataset("MMS_Service_SearchVendorByGroups", sqlPrms);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static DataSet OMS_GetDerivedUnitTypes()
        {
            try
            {
              
                DataSet ds= SqlHelper.ExecuteDataset("OMS_GetDerivedUnitTypes");
                return ds;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }



        public int AddAditionalIndentfromRPO(int RaisedBy, int ProjectID, string IndentNo, int Rbval, string deptid, string req, int PrjId, int CompanyID)
        {
            try
            {
                SqlParameter[] p = new SqlParameter[9];
                if (RaisedBy != 0)
                    p[0] = new SqlParameter("@RaisedBy", RaisedBy);
                else
                    p[0] = new SqlParameter("@RaisedBy", SqlDbType.Int);

                p[1] = new SqlParameter("@ProjectId", ProjectID); // for Worksite
                p[2] = new SqlParameter("@Indent_Number", IndentNo);
                p[3] = new SqlParameter("@IndentID", 2);
                p[3].Direction = ParameterDirection.Output;
                p[4] = new SqlParameter("@Indent_Type", Rbval);
                p[5] = new SqlParameter("@DeptId", deptid);
                if (req != "")
                    p[6] = new SqlParameter("@MessageRequest", req);
                else
                    p[6] = new SqlParameter("@MessageRequest", SqlDbType.VarChar);
                if (PrjId != 0)
                    p[7] = new SqlParameter("@PrjId", PrjId);// for Project
                else
                    p[7] = new SqlParameter("@PrjId", SqlDbType.Int);

                if (CompanyID != null)
                    p[8] = new SqlParameter("@CompanyID", CompanyID);
                else
                    p[8] = new SqlParameter("@CompanyID", SqlDbType.Int);

                int i = SqlHelper.ExecuteNonQuery("SP_PM_AddIndent", p);
                return Convert.ToInt32(p[3].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
