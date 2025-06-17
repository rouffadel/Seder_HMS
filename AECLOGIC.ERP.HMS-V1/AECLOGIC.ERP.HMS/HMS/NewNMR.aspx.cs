using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using AECLOGIC.HMS.BLL;
using AECLOGIC.ERP.COMMON;
namespace AECLOGIC.ERP.HMS
{
    public partial class NewNMR : AECLOGIC.ERP.COMMON.WebFormMaster
    {
        AttendanceDAC objAtt = new AttendanceDAC();
        AttendanceDAC objEmployee = new AttendanceDAC();
        AttendanceDAC objRights = new AttendanceDAC();
        HRCommon objHrCommon = new HRCommon();
        static int SearchCompanyID;
        static int Siteid,fcase;
        int mid = 0;
        bool viewall;
        string menuname;
        string menuid;
        protected override void OnInit(EventArgs e)
        {
            ModuleID = 1;
            base.OnInit(e);
            btnAdd.Attributes.Add("onclick", "javascript:return validatesave();");
            EmpListPaging.FirstClick += new Paging.PageFirst(EmpListPaging_FirstClick);
            EmpListPaging.PreviousClick += new Paging.PagePrevious(EmpListPaging_FirstClick);
            EmpListPaging.NextClick += new Paging.PageNext(EmpListPaging_FirstClick);
            EmpListPaging.LastClick += new Paging.PageLast(EmpListPaging_FirstClick);
            EmpListPaging.ChangeClick += new Paging.PageChange(EmpListPaging_FirstClick);
            EmpListPaging.ShowRowsClick += new Paging.ShowRowsChange(EmpListPaging_ShowRowsClick);
            EmpListPaging.CurrentPage = 1;
        }
        void EmpListPaging_ShowRowsClick(object sender, EventArgs e)
        {
            EmpListPaging.CurrentPage = 1;
            BindPager();
        }
        void EmpListPaging_FirstClick(object sender, EventArgs e)
        {
            BindPager();
        }
        void BindPager()
        {
            objHrCommon.PageSize = EmpListPaging.CurrentPage;
            objHrCommon.CurrentPage = EmpListPaging.ShowRows;
            EmployeBind(objHrCommon,fcase);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            SearchCompanyID = Convert.ToInt32(Session["CompanyID"]);
            if (!IsPostBack)
            {
                GetParentMenuId();
                BindWorkSites();
                BindDepartments(0);
                EmployeBind(objHrCommon, fcase);
                BindCategories();
                BindDesignations();
            }
        }
        private void BindCategories()
        {
            DataSet ds = objAtt.GetCategories();
            ddlSearCategory.DataSource = ds;
            ddlSearCategory.DataValueField = "CateId";
            ddlSearCategory.DataTextField = "Category";
            ddlSearCategory.DataBind();
            ddlSearCategory.Items.Insert(0, new ListItem("---ALL---", "0"));
        }
        private void BindDesignations()
        {
            DataSet ds = objAtt.GetDesignations();
            ddlSearDesigantion.DataSource = ds;
            ddlSearDesigantion.DataValueField = "DesigId";
            ddlSearDesigantion.DataTextField = "Designation";
            ddlSearDesigantion.DataBind();
            ddlSearDesigantion.Items.Insert(0, new ListItem("---ALL---", "0"));
        }
        public int GetParentMenuId()
        {
            string URL = Request.Url.Segments[Request.Url.Segments.Length - 1] + Request.Url.Query;
            int RoleId = Convert.ToInt32(Session["RoleId"].ToString());
            int ModuleId = ModuleID; ;
          DataSet  ds = AttendanceDAC.GetAllowed(RoleId, ModuleId, URL);
            int MenuId = 0;
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                MenuId = Convert.ToInt32(ds.Tables[0].Rows[0]["Under"]);
                ViewState["Editable"] = (bool)ds.Tables[0].Rows[0]["Editable"];
                gvDisplay.Columns[3].Visible = Convert.ToBoolean(ds.Tables[0].Rows[0]["Editable"].ToString());
                btnAdd.Enabled = (bool)ds.Tables[0].Rows[0]["Editable"];
            }
            return MenuId;
        }
        void EmployeBind(HRCommon objHrCommon,int fcase)
        {
            try
            {
                objHrCommon.PageSize = EmpListPaging.ShowRows;
                objHrCommon.CurrentPage = EmpListPaging.CurrentPage;
                objHrCommon.SiteID = 0;
                objHrCommon.DeptID = 0;
                try
                {
                    objHrCommon.SiteID = Convert.ToInt32(ddlworksites.SelectedItem.Value);
                    objHrCommon.DeptID = Convert.ToDouble(ddldepartments.SelectedItem.Value);
                }
                catch { }
                objHrCommon.Status = chkActive.Checked;
                objHrCommon.EmpID = 0;
                objHrCommon.FName = txtusername.Text.Replace(' ', '%');
                if (chkActive.Checked)
                {
                    gvDisplay.Columns[6].HeaderText = "Deactive";
                }
                else
                {
                    gvDisplay.Columns[6].HeaderText = "Active";
                }
                int? DesigID = null;
                int? CatID = null;
                if (ddlSearDesigantion.SelectedIndex > 0)
                    DesigID = Convert.ToInt32(ddlSearDesigantion.SelectedValue);
                if (ddlSearCategory.SelectedIndex > 0)
                    CatID = Convert.ToInt32(ddlSearCategory.SelectedValue);
                DataSet ds = (DataSet)objEmployee.GetNMRByPage(objHrCommon, DesigID, CatID, fcase);
                ViewState["NMR"] = ds;
                gvDisplay.DataSource = ds;
                gvDisplay.DataBind();
                EmpListPaging.Bind(objHrCommon.CurrentPage, objHrCommon.TotalPages, objHrCommon.NoofRecords, objHrCommon.PageSize);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public string GetWorkSite(string WSid)
        {
            string retVal = "";
            try
            {
                DataSet ds = (DataSet)ViewState["WorkSites"];
                retVal = ds.Tables[0].Select("Site_ID='" + WSid + "'")[0]["Site_Name"].ToString();
            }
            catch { }
            return retVal;
        }
        public string GetDepartment(string DeptId)
        {
            string retVal = "";
            try
            {
                DataSet ds = (DataSet)ViewState["Departments"];
                retVal = ds.Tables[0].Select("DepartmentUId='" + DeptId + "'")[0]["DepartmentName"].ToString();
            }
            catch { }
            return retVal;
        }
        public void BindWorkSites()
        {
            try
            {
                DataSet ds = AttendanceDAC.GetWorkSite(0, '1', Convert.ToInt32(Session["CompanyID"]));
                ViewState["WorkSites"] = ds;
                if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0)
                {
                }
                DataSet ds1 = AttendanceDAC.GetWorkSite_By_NMR();
                if (ds1 != null && ds1.Tables.Count != 0 && ds1.Tables[0].Rows.Count > 0)
                {
                    ddlworksites.DataSource = ds1.Tables[0];
                    ddlworksites.DataTextField = "Site_Name";
                    ddlworksites.DataValueField = "Site_ID";
                    ddlworksites.DataBind();
                    if (Convert.ToInt32(Session["MonitorSite"]) != 0)
                    {
                        ddlworksites.Items.FindByValue(Session["MonitorSite"].ToString()).Selected = true;
                        ddlworksites.Enabled = false;
                    }
                    else
                    {
                        ddlworksites.Items.Insert(0, new ListItem("---Select---", "0"));
                    }
                }
                else
                {
                    ddlworksites.Items.Insert(0, new ListItem("---Select---", "0"));
                }
            }
            catch
            {
            }
        }
        public void BindDepartments(int Site)
        {
            try
            {
                    DataSet ds = AttendanceDAC.BindDeparmetBySite(Site, Convert.ToInt32(Session["CompanyID"]));
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        protected void Worksidechangewithdep(object sender, EventArgs e)
        {
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@Search", txtSearchWorksite.Text);
            param[1] = new SqlParameter("@CompanyID", Convert.ToInt32(Session["CompanyID"]));
            FIllObject.FillDropDown(ref ddlworksites, "G_GET_WorkSitebyFilter", param);
            ListItem itmSelected = ddlworksites.Items.FindByText(txtSearchWorksite.Text);
            if (itmSelected != null)
            {
                ddlworksites.SelectedItem.Selected = false;
                itmSelected.Selected = true;
            }
            ddlworksites_SelectedIndexChanged(sender, e);
            txtSearchdept.Text = "";
        }
        protected void GetDepartmentSearch(object sender, EventArgs e)
        {
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@Search", txtSearchdept.Text);
            param[1] = new SqlParameter("@SiteID", ddlworksites.SelectedValue);
            param[2] = new SqlParameter("@CompanyID", Convert.ToInt32(Session["CompanyID"]));
            FIllObject.FillDropDown(ref ddldepartments, "HR_NewNMRSearchgoogle_GetDepartmentList", param);
            ListItem itmSelected = ddldepartments.Items.FindByText(txtSearchdept.Text);
            if (itmSelected != null)
            {
                ddldepartments.SelectedItem.Selected = false;
                itmSelected.Selected = true;
            }
            if (txtSearchdept.Text != "") { ddldepartments.SelectedIndex = 1; }
        }
        protected void reset()
        {
            txtusername.Text = txtusername0.Text= txtAddress.Text = txtContact.Text = txtSearchWorksite0.Text = txtSearchdept0.Text = txtDesignationSerach0.Text = txtCategorySearch0.Text = "";
        }
        protected void GetDesignation(object sender, EventArgs e)
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@Search", txtDesignationSerach.Text);
            FIllObject.FillDropDown(ref ddlSearDesigantion, "HR_GetSearchgoogleDesignations", param);
            ListItem itmSelected = ddlSearDesigantion.Items.FindByText(txtDesignationSerach.Text);
            if (itmSelected != null)
            {
                ddlSearDesigantion.SelectedItem.Selected = false;
                itmSelected.Selected = true;
            }
        }
        protected void GetCategory(object sender, EventArgs e)
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@Search", txtCategorySearch.Text);
            FIllObject.FillDropDown(ref ddlSearCategory, "HR_GoogleSearc_GetCategories", param);
            ListItem itmSelected = ddlSearCategory.Items.FindByText(txtCategorySearch.Text);
            if (itmSelected != null)
            {
                ddlSearCategory.SelectedItem.Selected = false;
                itmSelected.Selected = true;
            }
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            EmpListPaging.CurrentPage = 1;
            EmployeBind(objHrCommon, fcase);
        }
        protected void gvDisplay_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Edt")
            {
                if (txtSearchdept0.Text != "") { BindDepartments(0); }
                int Id = Convert.ToInt32(e.CommandArgument);
                DataSet ds = (DataSet)ViewState["NMR"];
                DataRow drRow = ds.Tables[0].Select("NMRId='" +Id+ "'")[0];
                objHrCommon.EmpID = Id;
                lblEmpId.Text = Id.ToString();
                txtSearchWorksite0.Text = drRow["Site_Name"].ToString();
                txtSearchdept0.Text = drRow["DepartmentName"].ToString();
                txtusername0.Text = drRow["NMRName"].ToString();
                txtContact.Text = drRow["Contact"].ToString().Trim();
                txtDesignationSerach0.Text = drRow["Designation"].ToString();
                txtCategorySearch0.Text = drRow["Category"].ToString();
                btnAdd.Text = "Update";
                //ddlWorksite_hid.Value =drRow["Siteid"].ToString();
                //department_hid.Value=drRow["Deptid"].ToString();
                //category_hid.Value=drRow["Categoryid"].ToString();
                //desigination_hid.Value = drRow["Desigid"].ToString();
                Session["Siteid"] = drRow["Siteid"].ToString();
                Session["Deptid"] = drRow["Deptid"].ToString();
                Session["Categoryid"] = drRow["Categoryid"].ToString();
                Session["Desigid"] = drRow["Desigid"].ToString();
                Session["EMPID"] = drRow["Employeeid"].ToString();
            }
            if (e.CommandName == "Deactive")
            {
                GridViewRow gvr = (GridViewRow)gvDisplay.Rows[Convert.ToInt32(e.CommandArgument)];
                if (gvDisplay.Columns[6].HeaderText == "Deactive")
                    objHrCommon.Status = false;
                else
                    objHrCommon.Status = true;
                objHrCommon.EmpID = Convert.ToInt32(gvDisplay.DataKeys[gvr.RowIndex].Value);
                objHrCommon.UserID = Convert.ToInt32(Session["UserId"]);
                objEmployee.UpdateNMRStatus(objHrCommon);
                AlertMsg.MsgBox(Page, "Done.");
                EmployeBind(objHrCommon, fcase);
            }
        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            string Message = string.Empty;
            if (btnAdd.Text == "Add")
            {
                objHrCommon.EmpID = 0;
                objHrCommon.Status = true;
            }
            else
            {
                objHrCommon.EmpID = Convert.ToInt32(lblEmpId.Text);
                objHrCommon.Status = chkActive.Checked;
            }
            if (Session["Siteid"] != null)
            {
                if (!string.IsNullOrEmpty(Session["Siteid"].ToString()))
                {
                    if (!string.IsNullOrEmpty(ddlWorksite_hid.Value.ToString()))
                 {
                          objHrCommon.SiteID = Convert.ToInt32(Convert.ToInt32(ddlWorksite_hid.Value));
                 }
                    else
                 {
                     ddlWorksite_hid.Value = Session["Siteid"].ToString();
                 }
                }
            }
            if (Session["Deptid"] != null)
            {
                if (!string.IsNullOrEmpty(Session["Deptid"].ToString()))
                {
                            if (!string.IsNullOrEmpty(department_hid.Value.ToString()))
                         {
                                  objHrCommon.DeptID = Convert.ToInt32(Convert.ToInt32(department_hid.Value));
                         }
                            else
                         {
                               department_hid.Value = Session["Deptid"].ToString();
                         }
                }
            }
            objHrCommon.SiteID = Convert.ToInt32(Convert.ToInt32(ddlWorksite_hid.Value == "" ? "0" : ddlWorksite_hid.Value));
            objHrCommon.DeptID = Convert.ToDouble(Convert.ToInt32(department_hid.Value == "" ? "0" : department_hid.Value));
            if (objHrCommon.SiteID == 0)
            {
                Message = Message + "select Work Site" + Environment.NewLine;
            }
            if (objHrCommon.DeptID==0)
            {
                Message =Message+"Select Department"+Environment.NewLine;
            }
            objHrCommon.FName = txtusername0.Text;
            objHrCommon.Mobile = txtContact.Text;
            objHrCommon.Address = txtAddress.Text;
            objHrCommon.UserID = Convert.ToInt32(Session["UserId"]);
            btnAdd.Text = "Add";
            if (Session["Categoryid"] != null)
            {
                if (!string.IsNullOrEmpty(Session["Categoryid"].ToString()))
                {
                            if (!string.IsNullOrEmpty(category_hid.Value.ToString()))
                         {
                              objHrCommon.TradeID = Convert.ToInt32(Convert.ToInt32(category_hid.Value));
                         }
                            else
                         {
                               category_hid.Value = Session["Categoryid"].ToString();
                         }
                }
            }
            if (Session["Desigid"] != null)
            {
                if (!string.IsNullOrEmpty(Session["Desigid"].ToString()))
                {
                       if (!string.IsNullOrEmpty(desigination_hid.Value.ToString()))
                         {
                              objHrCommon.DesigID = Convert.ToInt32(Convert.ToInt32(desigination_hid.Value));
                         }
                            else
                         {
                               desigination_hid.Value = Session["Desigid"].ToString();
                         }
                }
            }
            objHrCommon.TradeID = Convert.ToInt32(Convert.ToInt32(category_hid.Value == "" ? "0" : category_hid.Value));
            if (objHrCommon.TradeID == 0)
            {
                Message = Message + "Select Category"; 
            }
            objHrCommon.DesigID = Convert.ToInt32(Convert.ToInt32(desigination_hid.Value == "" ? "0" : desigination_hid.Value));
            if (Message==string.Empty)
            {
                if (Session["EMPID"]==string.Empty)
                {
                    objEmployee.UpdateNMR(objHrCommon, Convert.ToInt32(objHrCommon.EmpID),0);
                }
                else
                {
                    objEmployee.UpdateNMR(objHrCommon, Convert.ToInt32(objHrCommon.EmpID), Convert.ToInt32(Session["EMPID"]));
                }
                Message = "DONE.";
            }
            EmployeBind(objHrCommon, fcase);
            txtusername.Text = "";
            AlertMsg.MsgBox(Page, Message);
            reset();
        }
        protected void gvDisplay_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            foreach (GridViewRow gvr in gvDisplay.Rows)
            {
                LinkButton lnkUpd = (LinkButton)gvr.Cells[4].FindControl("lnkSwitch");
                lnkUpd.Enabled = Convert.ToBoolean(ViewState["Editable"]);
            }
        }
        protected void txtContact_TextChanged(object sender, EventArgs e)
        {
            try
            {
                decimal MobileNumber = Convert.ToDecimal(txtContact.Text);
                string Phone1 = txtContact.Text;
                if (Phone1.Length != 10 && Phone1.Length != 11)
                {
                    AlertMsg.MsgBox(Page, "Enter 10/11 Digits Mobile/Phone Number!");
                }
            }
            catch { AlertMsg.MsgBox(Page, "Invalid Phone/Mobile Number!"); }
        }
        //Added by Rijwan:22-03-2016
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionList(string prefixText, int count, string contextKey)
        {
            DataSet ds = AttendanceDAC.GetGoogleABCSearchWorkSite(prefixText,1);
            DataTable dt = ds.Tables[0];
            List<string> items = new List<string>(count);
            var rtval = new Dictionary<string, string>();
            foreach (DataRow row in dt.Rows)
            {
                string str = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(row["Name"].ToString(), row["ID"].ToString());
                items.Add(str);
            }
            return items.ToArray(); ;// txtItems.ToArray();
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionListdept(string prefixText, int count, string contextKey)
        {
            DataSet ds = AttendanceDAC.GetDaprtmentListForNewNMR(prefixText, Siteid, SearchCompanyID);
            DataTable dt = ds.Tables[0];
            List<string> items = new List<string>(count);
            var rtval = new Dictionary<string, string>();
            foreach (DataRow row in dt.Rows)
            {
                string str = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(row["Name"].ToString(), row["ID"].ToString());
                items.Add(str);
            }
            return items.ToArray(); 
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionListdesi(string prefixText, int count, string contextKey)
        {
            DataSet ds = AttendanceDAC.GetGoogleSerachDesignations(prefixText);
            DataTable dt = ds.Tables[0];
            List<string> items = new List<string>(count);
            var rtval = new Dictionary<string, string>();
            foreach (DataRow row in dt.Rows)
            {
                string str = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(row["Name"].ToString(), row["ID"].ToString());
                items.Add(str);
            }
            return items.ToArray(); ;// txtItems.ToArray();
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionListCat(string prefixText, int count, string contextKey)
        {
            DataSet ds = AttendanceDAC.GetGoogleSerachCategory(prefixText);
            DataTable dt = ds.Tables[0];
            List<string> items = new List<string>(count);
            var rtval = new Dictionary<string, string>();
            foreach (DataRow row in dt.Rows)
            {
                string str = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(row["Name"].ToString(), row["ID"].ToString());
                items.Add(str);
            }
            return items.ToArray(); 
        }
        public static string[] ConvertStingArray(DataSet ds)
        {
            string[] rtval = Array.ConvertAll(ds.Tables[0].Select(), new Converter<DataRow, string>(DataRowTotable));
            return rtval;
        }
        public static string DataRowTotable(DataRow dr)
        {
            return dr["Name"].ToString();
        }
        protected void ddlworksites_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindDeparmetBySite(Convert.ToInt32(ddlworksites.SelectedValue));
            Siteid = Convert.ToInt32(ddlworksites.SelectedValue);
        }
        public void BindDeparmetBySite(int Site)
        {
            DataSet ds = AttendanceDAC.BindDeparmetBySite(Site, Convert.ToInt32(Session["CompanyID"]));
            ddldepartments.DataSource = ds;
            ddldepartments.DataTextField = "DeptName";
            ddldepartments.DataValueField = "DepartmentUId";
            ddldepartments.DataBind();
            ddldepartments.Items.Insert(0, new ListItem("---ALL---", "0", true));
        }
        protected void rbemp_CheckedChanged(object sender, EventArgs e)
        {
            if(rbemp.Checked)
            {
                rbnmr.Checked = false;
                fcase = 1;
                EmpListPaging.CurrentPage = 1;
                BindPager();
            }
            else
            {
               rbemp.Checked = false;
               fcase = 2;
               EmpListPaging.CurrentPage = 1;
               BindPager();
            }
        }
    }
}
