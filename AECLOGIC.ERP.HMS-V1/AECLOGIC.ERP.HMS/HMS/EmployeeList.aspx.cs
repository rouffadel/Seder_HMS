using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Collections.Generic;
using AECLOGIC.HMS.BLL;
using AECLOGIC.ERP.COMMON;
using AECLOGIC.ERP.HMS.HRClasses;
using Aeclogic.Common.DAL;
//using AECLOGIC.HMS.COMMON;
namespace AECLOGIC.ERP.HMSV1
{
    public partial class EmployeeListV1 : AECLOGIC.ERP.COMMON.WebFormMaster
    {
        AttendanceDAC objEmployee = new AttendanceDAC();
        AttendanceDAC objRights = new AttendanceDAC();
        HRCommon objHrCommon = new HRCommon();
        static int SearchCompanyID;
        static int? Roleid;
        static int Siteid;
        bool Editable;
        string ReturnVal = "";
        protected override void OnInit(EventArgs e)
        {
            ModuleID = 1;
            EmpListPaging.FirstClick += new Paging.PageFirst(EmpListPaging_FirstClick);
            EmpListPaging.PreviousClick += new Paging.PagePrevious(EmpListPaging_FirstClick);
            EmpListPaging.NextClick += new Paging.PageNext(EmpListPaging_FirstClick);
            EmpListPaging.LastClick += new Paging.PageLast(EmpListPaging_FirstClick);
            EmpListPaging.ChangeClick += new Paging.PageChange(EmpListPaging_FirstClick);
            EmpListPaging.ShowRowsClick += new Paging.ShowRowsChange(EmpListPaging_ShowRowsClick);
            EmpListPaging.CurrentPage = 1;
            base.OnInit(e);
        }
        void EmpListPaging_ShowRowsClick(object sender, EventArgs e)
        {
            EmpListPaging.CurrentPage = 1;
            //BindPager();
            BindAdvanceSearch();
        }
        void EmpListPaging_FirstClick(object sender, EventArgs e)
        {
            //BindPager();
            BindAdvanceSearch();
        }
        void BindPager()
        {
            objHrCommon.PageSize = EmpListPaging.CurrentPage;
            objHrCommon.CurrentPage = EmpListPaging.ShowRows;
            EmployeBind(objHrCommon);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                SearchCompanyID = Convert.ToInt32(Session["CompanyID"]);
                if (!IsPostBack)
                {
                    GetParentMenuId();
                    try
                    {
                        ViewState["WSID"] = 0;
                        if (Convert.ToInt32(Session["RoleId"].ToString()) == 7)
                        {
                            try
                            {
                                DataSet ds = clViewCPRoles.HR_DailyAttStatus(Convert.ToInt32(Session["UserId"]));
                                ViewState["WSID"] = ds.Tables[0].Rows[0]["ID"].ToString();
                                txtSearchWorksite.Text = ds.Tables[0].Rows[0]["Name"].ToString();
                                txtSearchWorksite.ReadOnly = true;
                            }
                            catch { }
                        }
                    }
                    catch { }
                    tblRejoin.Visible = false;
                    ViewState["EmpID"] = 0;
                    ViewState["Status"] = 'y';
                    tblStatus.Visible = false;
                   // BindEmpNatures();
                   // BindWorkSites();
                   // BindDeparmetBySite(0);
                    // BindDepartments();
                   
                    //FIllObject.FillDropDown(ref ddlFilterType, "sg_FilterTypes");
                    DataSet dfs = SQLDBUtil.ExecuteDataset("sg_FilterTypes");
                    ddlFilterType.DataTextField = "Name";
                    ddlFilterType.DataValueField = "ID";
                    ddlFilterType.DataSource = dfs;
                    ddlFilterType.DataBind();

                    ddlFilterType.SelectedValue = "4";
                    DataSet dsts = SQLDBUtil.ExecuteDataset("sh_EmployeeFilterColumns");
                    lstColumns.DataSource = dsts;
                    lstColumns.DataBind();
                  //  BindCategories();
                  //  BindDesignations();
                    BindAdvanceSearchPageLoad();
                    OrderID = 2;
                    ViewState["OrderID"] = 2;
                   // EmployeBind(objHrCommon);
                    txtRejoin.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    //pratap added here date:13-04-2016                    
                    txtEmpID.Attributes.Add("onkeydown", "return controlEnter(event)");
                    txtOldEmpID.Attributes.Add("onkeydown", "return controlEnter(event)");
                    txtusername.Attributes.Add("onkeydown", "return controlEnter(event)");
                }
            }
            catch (Exception ex)
            {
                clsErrorLog.HMSEventLog(ex, "EmployeeList", "Page_Load", "001");
            }
        }

        private void BindAdvanceSearchPageLoad()
        {
            objHrCommon.PageSize = EmpListPaging.ShowRows;
            objHrCommon.CurrentPage = EmpListPaging.CurrentPage;
            SqlParameter[] parms = new SqlParameter[10];
            parms[0] = new SqlParameter("@TableName", DBNull.Value);
            parms[1] = new SqlParameter("ColumnNames", "");
            parms[2] = new SqlParameter("@PKCOlumn", "Empid");
            parms[3] = new SqlParameter("@viewTable", "vh_employeeDetails");
            parms[4] = new SqlParameter("@SearchString", "");
            parms[5] = new SqlParameter("@SearchKey", ddlFilterType.SelectedItem.Text);
            parms[6] = new SqlParameter("@CurrPage", objHrCommon.CurrentPage);
            parms[7] = new SqlParameter("@PageSize", objHrCommon.PageSize);
            parms[8] = new SqlParameter("@NrRecords", System.Data.SqlDbType.Int);
            parms[8].Direction = ParameterDirection.Output;
            parms[9] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
            parms[9].Direction = ParameterDirection.ReturnValue;
            DataSet ds = SQLDBUtil.ExecuteDataset("fg_whereContains", parms);
            if (ds.Tables[0].Rows.Count > 0)
            {
                objHrCommon.NoofRecords = (int)parms[8].Value;
                objHrCommon.TotalPages = (int)parms[9].Value;
                gveditkbipl.DataSource = ds.Tables[0];
            }
            else
                gveditkbipl.DataSource = null;
            gveditkbipl.DataBind();
            EmpListPaging.Bind(objHrCommon.CurrentPage, objHrCommon.TotalPages, objHrCommon.NoofRecords, objHrCommon.PageSize);

        }
        public int GetParentMenuId()
        {
            string URL = Request.Url.Segments[Request.Url.Segments.Length - 1] + Request.Url.Query;
            int RoleId = Convert.ToInt32(Session["RoleId"].ToString());
            int ModuleId = ModuleID; ;
            DataSet ds = AttendanceDAC.GetAllowed(RoleId, ModuleId, URL);
            int MenuId = 0;
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                MenuId = Convert.ToInt32(ds.Tables[0].Rows[0]["Under"]);
                ViewState["Editable"] = Editable = (bool)ds.Tables[0].Rows[0]["Editable"];
                //  gveditkbipl.Columns[8].Visible = Convert.ToBoolean(ds.Tables[0].Rows[0]["Editable"].ToString());
                ////  gveditkbipl.Columns[9].Visible = Convert.ToBoolean(ds.Tables[0].Rows[0]["Editable"].ToString());
                //  gveditkbipl.Columns[10].Visible = Convert.ToBoolean(ds.Tables[0].Rows[0]["Editable"].ToString());
                //  gveditkbipl.Columns[11].Visible = Convert.ToBoolean(ds.Tables[0].Rows[0]["Editable"].ToString());
                //  gveditkbipl.Columns[12].Visible = Convert.ToBoolean(ds.Tables[0].Rows[0]["Editable"].ToString());
                //  gveditkbipl.Columns[13].Visible = Convert.ToBoolean(ds.Tables[0].Rows[0]["Editable"].ToString());
            }
            return MenuId;
        }
        int OrderID = 0, Direction = 0;
        private void BindCategories()
        {
            DataSet ds = objEmployee.GetCategories();
            ddlSearCategory.DataSource = ds;
            ddlSearCategory.DataValueField = "CateId";
            ddlSearCategory.DataTextField = "Category";
            ddlSearCategory.DataBind();
            ddlSearCategory.Items.Insert(0, new ListItem("---ALL---", "0"));
        }
        private void BindDesignations()
        {
            DataSet ds = objEmployee.GetDesignations();
            ddlSearDesigantion.DataSource = ds;
            ddlSearDesigantion.DataValueField = "DesigId";
            ddlSearDesigantion.DataTextField = "Designation";
            ddlSearDesigantion.DataBind();
            ddlSearDesigantion.Items.Insert(0, new ListItem("---ALL---", "0"));
        }
        void EmployeBind(HRCommon objHrCommon)
        {
            try
            {
                objHrCommon.PageSize = EmpListPaging.ShowRows;
                objHrCommon.CurrentPage = EmpListPaging.CurrentPage;
                objHrCommon.SiteID = Convert.ToInt32(ddlworksites.SelectedItem.Value);
                objHrCommon.DeptID = Convert.ToDouble(ddldepartments.SelectedItem.Value);
                int? DesigID = null;
                int? CatID = null;
                if (ddlSearDesigantion.SelectedIndex > 0)
                    DesigID = Convert.ToInt32(ddlSearDesigantion.SelectedValue);
                if (ddlSearCategory.SelectedIndex > 0)
                    CatID = Convert.ToInt32(ddlSearCategory.SelectedValue);
                int? EmpNatureID = null;
                if (ddlEmpNature.SelectedValue != "0")
                    EmpNatureID = Convert.ToInt32(ddlEmpNature.SelectedValue);
                if (txtOldEmpID.Text == "")
                    objHrCommon.OldEmpID = null;
                if (rbActive.Checked)
                {
                    objHrCommon.CurrentStatus = 'y';
                }
                else
                {
                    objHrCommon.CurrentStatus = 'n';
                }
                objHrCommon.FName = txtusername.Text;
                gveditkbipl.DataSource = null;
                gveditkbipl.DataBind();
                Nullable<int> empid;
                if (Session["Empid"] == null || Session["Empid"] == "")
                {
                    empid = null;
                }
                else
                {
                    empid = Convert.ToInt32(Session["Empid"]);
                }
                OrderID = Convert.ToInt32(ViewState["OrderID"]);
                try
                {
                    if (Convert.ToInt32(ViewState["WSID"]) > 0)
                        objHrCommon.SiteID = Convert.ToInt32(ViewState["WSID"]);
                }
                catch { }
                int hdCountryID = 0; if (txtSNationality.Text.Trim() != "") { hdCountryID = Convert.ToInt32(hdSNationality.Value == "" ? "0" : hdSNationality.Value); }
                //objHrCommon.CountryID = hdCountryID;
                DataSet ds = (DataSet)objEmployee.GetEmployeesByPageOrderByAssID(objHrCommon, OrderID, Direction, EmpNatureID, empid, Convert.ToInt32(Session["CompanyID"]), DesigID, CatID);
                //ds = (DataSet)objEmployee.GetEmployeesByPage(objHrCommon);
                Session["Empid"] = null;
                //if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0)
                //{
                //    gveditkbipl.DataSource = ds;
                //    gveditkbipl.DataBind();
                //}
                //EmpListPaging.Bind(objHrCommon.CurrentPage, objHrCommon.TotalPages, objHrCommon.NoofRecords, objHrCommon.PageSize);
            }
            catch (Exception ex)
            {
                clsErrorLog.HMSEventLog(ex, "EmployeeList", "EmployeeBind", "002");
            }
        }
        public string BindInActBy(string EmpID)
        {
            string retVal = "";
            try
            {
                DataSet ds = AttendanceDAC.HR_GetInActiveStatus(Convert.ToInt32(EmpID));
                if (ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() != "")
                    {
                        retVal = ds.Tables[0].Rows[0][0].ToString();
                    }
                }
                return retVal;
            }
            catch
            {
            }
            return retVal;
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
                retVal = ds.Tables[0].Select("DepartmentUId='" + DeptId + "'")[0]["DeptName"].ToString();
            }
            catch { }
            return retVal;
        }
        public void BindWorkSites()
        {
            try
            {
                DataSet ds = objRights.GetWorkSite_By_Employees(Convert.ToInt32(Session["CompanyID"]), Convert.ToInt32(Session["RoleId"]));
                //ds = objRights.GetWorkSite(0, '1');
                ViewState["WorkSites"] = ds;
                if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ddlworksites.DataSource = ds.Tables[0];
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
                        ddlworksites.Items.Insert(0, new ListItem("---ALL---", "0"));
                    }
                }
                else
                {
                    ddlworksites.Items.Insert(0, new ListItem("---ALL---", "0"));
                }
            }
            catch (Exception ex)
            {
                clsErrorLog.HMSEventLog(ex, "EmployeeList", "BindWorksite", "003");
            }
        }
        public void BindEmpNatures()
        {
            DataSet ds = Leaves.GetEmpNatureList(1);
            ddlEmpNature.DataSource = ds;
            ddlEmpNature.DataTextField = "Nature";
            ddlEmpNature.DataValueField = "NatureOfEmp";
            ddlEmpNature.DataBind();
            ddlEmpNature.Items.Insert(0, new ListItem("---All---", "0"));
        }
        public void BindDeparmetBySite(int Site)
        {
            DataSet ds = AttendanceDAC.BindDeparmetBySite(Site, Convert.ToInt32(Session["CompanyID"]));
            ViewState["Departments"] = ds;
            ddldepartments.DataSource = ds;
            ddldepartments.DataTextField = "DeptName";
            ddldepartments.DataValueField = "DepartmentUId";
            ddldepartments.DataBind();
            ddldepartments.Items.Insert(0, new ListItem("---ALL---", "0", true));
        }
        protected void EmpdataBound()
        {
            SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings["strConn"]);
            cn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cn;
            cmd.CommandType = CommandType.Text;
            //cmd.CommandText = "select * from kbipemp where Status='y' and [Type]!=1";
            cmd.CommandText = "select * from T_G_EmployeeMaster where Status='y' order by fname";
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = null;
            da.Fill(ds, "temp");
            gveditkbipl.DataSource = ds;
            gveditkbipl.DataBind();
            cn.Close();
        }
        protected void Worksidechangewithdep(object sender, EventArgs e)
        {
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@Search", txtSearchWorksite.Text);
            param[1] = new SqlParameter("@CompanyID", Convert.ToInt32(Session["CompanyID"]));
            param[2] = new SqlParameter("@Role", Convert.ToInt32(Session["RoleId"]));
            // FIllObject.FillDropDown(ref ddlWorksite, "G_GET_WorkSitebyFilter", param);
            FIllObject.FillDropDown(ref ddlworksites, "HR_GoogleSearch_GetWorksite_By_Employees", param);
            ListItem itmSelected = ddlworksites.Items.FindByText(txtSearchWorksite.Text);
            if (itmSelected != null)
            {
                ddlworksites.SelectedItem.Selected = false;
                itmSelected.Selected = true;
                //FillProjects();
            }
            ddlworksites_SelectedIndexChanged(sender, e);
            txtSearchdept.Text = "";
        }
        protected void GetDepartmentSearch(object sender, EventArgs e)
        {
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@Search", txtSearchdept.Text);
            param[1] = new SqlParameter("@CompanyID", Convert.ToInt32(Session["CompanyID"]));
            param[2] = new SqlParameter("@SiteID", ddlworksites.SelectedItem.Value);
            FIllObject.FillDropDown(ref ddldepartments, "HMS_googlesearch_GetDepartmentBySite", param);
            ListItem itmSelected = ddldepartments.Items.FindByText(txtSearchdept.Text);
            if (itmSelected != null)
            {
                ddldepartments.SelectedItem.Selected = false;
                itmSelected.Selected = true;
            }
            if (txtSearchdept.Text != "") { ddldepartments.SelectedIndex = 1; }
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
        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Edt")
                {
                    GridViewRow gvr = (GridViewRow)gveditkbipl.Rows[Convert.ToInt32(e.CommandArgument)]; ;
                    int Id = Convert.ToInt32(gveditkbipl.DataKeys[gvr.RowIndex].Value);
                    Session["Empid"] = Id;
                    Response.Redirect("CreateEmployee.aspx?Id=" + Id);
                }
                if (e.CommandName == "Delete")
                {
                    GridViewRow gvr = (GridViewRow)gveditkbipl.Rows[Convert.ToInt32(e.CommandArgument)]; ;
                    int Id = Convert.ToInt32(gveditkbipl.DataKeys[gvr.RowIndex].Value);
                    Desabial(Id);
                }
                if (e.CommandName == "Status")
                {
                    LinkButton lnkstatus = new LinkButton();
                    lnkstatus = (LinkButton)e.CommandSource;
                    GridViewRow selectedRow = (GridViewRow)lnkstatus.Parent.Parent;
                    lnkstatus = (LinkButton)selectedRow.FindControl("lnkstatus");
                    Label id = (Label)selectedRow.FindControl("lblEmpId");
                    objHrCommon.EmpID = Convert.ToInt32(id.Text);
                    ViewState["EmpID"] = objHrCommon.EmpID;
                    ViewState["Status"] = 'y';
                    int Status;
                    // if (lnkstatus.Text == "Activate")
                    if (lnkstatus.Text == "Active")
                    {
                        objHrCommon.CurrentStatus = 'y';
                        ViewState["Status"] = 'y';
                        Status = 2;
                        tblRejoin.Visible = true;
                        trDeactivate.Visible = false;
                    }
                    else
                    {
                        tblRejoin.Visible = false;
                        objHrCommon.CurrentStatus = 'n';
                        ViewState["Status"] = 'n';
                        Status = 1;
                        trDeactivate.Visible = true;
                    }
                    DataSet ds = AttendanceDAC.HR_EmpGetEmpDetailsByID(Convert.ToInt32(objHrCommon.EmpID), Status);
                    gveditkbipl.DataSource = ds;
                    gveditkbipl.DataBind();
                    tblStatus.Visible = true;
                    // UpdateStatus(objHrCommon);
                }
                if (e.CommandName == "App")
                {
                    AlertMsg.CallScriptFunction(Page, "showApp('" + e.CommandArgument + "');");
                }
                if (e.CommandName == "AddQuaExp")
                {
                    int QuaEmpID = Convert.ToInt32(e.CommandArgument);
                    //Session["QuaEmpID"] = QuaEmpID;
                    Response.Redirect("EmpEduExpDetails.aspx?id=1" + "&EmpID=" + QuaEmpID);
                }
            }
            catch (Exception ex)
            {
                clsErrorLog.HMSEventLog(ex, "EmployeeList", "GridView1_RowCommand", "004");
            }
        }
        protected void UpdateStatus(HRCommon objhrcommon)
        {
            objhrcommon.UserID = Convert.ToInt32(Convert.ToInt32(Session["UserId"]).ToString());
            objHrCommon.CurrentStatus = Convert.ToChar(ViewState["Status"]);
            objEmployee.UpdateEmployeeStatus(objHrCommon);
            EmployeBind(objHrCommon);
        }
        protected void Desabial(int Id)
        {
            try
            {
                SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings["strConn"]);
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "spDeletekbiplPhoneList";
                SqlParameter p1 = new SqlParameter("@Id", Id);
                p1.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(p1);
                int n = cmd.ExecuteNonQuery();
                if (n > 0)
                {
                    AlertMsg.MsgBox(Page, "Delete Employee");
                    ClientScript.RegisterStartupScript(typeof(System.String), "str", "<script type='text/javascript'>alert('Delete Employee')</script>");
                }
                EmpdataBound();
            }
            catch (Exception ex)
            {
                clsErrorLog.HMSEventLog(ex, "EmployeeList", "Desabial", "005");
            }
        }
        protected void btnSearchAssID_Click(object sender, EventArgs e)
        {
            // OrderID = 1;
            gveditkbipl.Columns[1].Visible = true;
            SearchEmp();
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            // OrderID = 0;
            try
            {
                if (chkHis.Checked)
                    gveditkbipl.Columns[1].Visible = true;
                else
                    gveditkbipl.Columns[1].Visible = false;
                EmpListPaging.CurrentPage = 1;
                SearchEmp();
            }
            catch (Exception ex)
            {
                clsErrorLog.HMSEventLog(ex, "EmployeeList", "btnSearch_Click", "006");
            }
        }
        public void SearchEmp()
        {
            try
            {
                if (txtEmpID.Text == "")
                {
                    EmpListPaging.Visible = true;
                    objHrCommon.SiteID = Convert.ToInt32(ddlworksites.SelectedItem.Value);
                    objHrCommon.DeptID = Convert.ToDouble(ddldepartments.SelectedItem.Value);
                    objHrCommon.FName = txtusername.Text;
                    objHrCommon.OldEmpID = txtOldEmpID.Text;
                    try
                    {
                        if (Convert.ToInt32(ViewState["WSID"]) > 0)
                            objHrCommon.SiteID = Convert.ToInt32(ViewState["WSID"]);
                    }
                    catch { }
                    //objHrCommon.OldEmployeeID = txtOldEmpID.Text;
                    EmployeBind(objHrCommon);
                }
                else
                {
                    EmpListPaging.Visible = false;
                    int EmpID = 0;
                    try
                    {
                        EmpID = Convert.ToInt32(txtEmpID.Text);
                        int Status = 1;
                        if (rbInActive.Checked == true)
                            Status = 2;
                        DataSet ds = AttendanceDAC.HR_EmpGetEmpDetailsByID(EmpID, Status);
                        gveditkbipl.DataSource = ds;
                        gveditkbipl.DataBind();
                    }
                    catch { AlertMsg.MsgBox(Page, "Check the data you have given..!"); }
                }
            }
            catch (Exception ex)
            {
                clsErrorLog.HMSEventLog(ex, "EmployeeList", "SearchEMP", "007");
            }
        }
        public static DataSet SearchGetCompletionNationality(String SearchKey)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@sName", SearchKey);
                DataSet ds = SqlHelper.ExecuteDataset("sh_countryfilter", param);
                return ds;
            }
            catch (Exception e) { throw e; }
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionNationality(string prefixText, int count, string contextKey)
        {
            DataSet ds = SearchGetCompletionNationality(prefixText);
            DataTable dt = ds.Tables[0];
            List<string> items = new List<string>(count);
            var rtval = new Dictionary<string, string>();
            foreach (DataRow row in dt.Rows)
            {
                string str = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(row["Name"].ToString(), row["ID"].ToString());
                items.Add(str);
            } return items.ToArray();
        }
        protected void gveditkbipl_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
        }
        protected void rbActive_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                tblRejoin.Visible = tblStatus.Visible = false;
                EmpListPaging.CurrentPage = 1;
                if (txtEmpID.Text == "")
                {
                    EmpListPaging.Visible = true;
                    objHrCommon.SiteID = Convert.ToInt32(ddlworksites.SelectedItem.Value);
                    objHrCommon.DeptID = Convert.ToDouble(ddldepartments.SelectedItem.Value);
                    objHrCommon.FName = txtusername.Text;
                    try
                    {
                        if (Convert.ToInt32(ViewState["WSID"]) > 0)
                            objHrCommon.SiteID = Convert.ToInt32(ViewState["WSID"]);
                    }
                    catch { }
                    EmployeBind(objHrCommon);
                }
                else
                {
                    EmpListPaging.Visible = false;
                    int EmpID = Convert.ToInt32(txtEmpID.Text);
                    DataSet ds = AttendanceDAC.HR_EmpGetEmpDetailsByID(EmpID, 1);//1 shows active person
                    gveditkbipl.DataSource = ds;
                    gveditkbipl.DataBind();
                }
            }
            catch (Exception ex)
            {
                clsErrorLog.HMSEventLog(ex, "EmployeeList", "rbActive_CheckedChanges", "008");
            }
            // EmployeBind(objHrCommon);
        }
        protected void rbInActive_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                tblRejoin.Visible = tblStatus.Visible = false;
                EmpListPaging.CurrentPage = 1;
                if (txtEmpID.Text == "")
                {
                    EmpListPaging.Visible = true;
                    objHrCommon.SiteID = Convert.ToInt32(ddlworksites.SelectedItem.Value);
                    objHrCommon.DeptID = Convert.ToDouble(ddldepartments.SelectedItem.Value);
                    objHrCommon.FName = txtusername.Text;
                    try
                    {
                        if (Convert.ToInt32(ViewState["WSID"]) > 0)
                            objHrCommon.SiteID = Convert.ToInt32(ViewState["WSID"]);
                    }
                    catch { }
                    EmployeBind(objHrCommon);
                }
                else
                {
                    EmpListPaging.Visible = false;
                    int EmpID = Convert.ToInt32(txtEmpID.Text);
                    DataSet ds = AttendanceDAC.HR_EmpGetEmpDetailsByID(EmpID, 2);//2 shows inactive person
                    gveditkbipl.DataSource = ds;
                    gveditkbipl.DataBind();
                }
            }
            catch (Exception ex)
            {
                clsErrorLog.HMSEventLog(ex, "EmployeeList", "rbinActive_CheckChange", "009");
            }
            //EmployeBind(objHrCommon);
        }
        protected void gveditkbipl_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //foreach (GridViewRow gvr in gveditkbipl.Rows)
            //{
            //    LinkButton lnkUpd = (LinkButton)gvr.Cells[9].FindControl("lnkstatus");
            //    lnkUpd.Enabled = Convert.ToBoolean(ViewState["Editable"]);
            //}
        }
        protected void btnStatusRemarks_Click(object sender, EventArgs e)
        {
            try
            {
                string StatusRemarks = txtStatusRemarks.Text;
                if (StatusRemarks != "")
                {
                    EmpListPaging.Visible = false;
                    objHrCommon.EmpID = Convert.ToInt32(ViewState["EmpID"]);
                    DateTime? RejoinOn = null;
                    if (chkRejoin.Checked == true)
                    {
                        try
                        {
                            RejoinOn = CODEUtility.ConvertToDate(txtRejoin.Text.Trim(), DateFormat.DayMonthYear);
                        }
                        catch
                        {
                            AlertMsg.MsgBox(Page, "Enter Rejoin Date!");
                            return;
                        }
                    }
                    if (chkRejoin.Checked == false && rbInActive.Checked != false)
                    { AlertMsg.MsgBox(Page, "Check the rejoin checkbox"); }
                    else
                    {
                        int Status;
                        if (ViewState["Status"].ToString() == "y")
                            Status = 1;
                        else
                            Status = 2;
                        AttendanceDAC.HR_StatusChangeRemarks(Convert.ToInt32(objHrCommon.EmpID), StatusRemarks, RejoinOn, Status, Convert.ToInt32(Session["UserId"]), chkRejoin.Checked ? 0 : Convert.ToInt32(ddlEmpDeAct.SelectedItem.Value));
                        UpdateStatus(objHrCommon);
                        tblStatus.Visible = false;
                        int State = 1;
                        if (ViewState["Status"].ToString() == "n")
                        {
                            State = 2;
                        }
                        DataSet ds = AttendanceDAC.HR_EmpGetEmpDetailsByID(Convert.ToInt32(objHrCommon.EmpID), State);
                        gveditkbipl.DataSource = ds;
                        gveditkbipl.DataBind();
                        chkRejoin.Checked = false;
                        BindPager();
                    }
                }
                else
                {
                    AlertMsg.MsgBox(Page, "Enter Remarks!");
                }
            }
            catch (Exception ex)
            {
                clsErrorLog.HMSEventLog(ex, "EmployeeList", "btnStatusRemarks_Click", "010");
            }
        }
        protected void ddlworksites_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindDeparmetBySite(Convert.ToInt32(ddlworksites.SelectedValue));
            Siteid = Convert.ToInt32(ddlworksites.SelectedValue);
            EmpListPaging.CurrentPage = 1;
        }
        protected void ddldepartments_SelectedIndexChanged(object sender, EventArgs e)
        {
            EmpListPaging.CurrentPage = 1;
        }
        protected void chkHis_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                ViewState["SortDir"] = SortDirection.Ascending;
                Direction = 0;
                if (chkHis.Checked)
                {
                    OrderID = 1;
                    ViewState["OrderID"] = 1;
                    gveditkbipl.Columns[1].Visible = true;
                    SearchEmp();
                }
                else
                {
                    //OrderID = 0;
                    gveditkbipl.Columns[1].Visible = false;
                    SearchEmp();
                }
            }
            catch (Exception ex)
            {
                clsErrorLog.HMSEventLog(ex, "EmployeeList", "chkHis_CheckChanged", "011");
            }
        }
        //Added by Rijwan:22-03-2016
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute]
        public static string[] GetCompletionList(string prefixText, int count, string contextKey)
        {
            DataSet ds = AttendanceDAC.GetWorkSite_GoogleSech_By_Employees(prefixText, SearchCompanyID, Roleid);
            return ConvertStingArray(ds);
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute]
        public static string[] GetCompletionListdept(string prefixText, int count, string contextKey)
        {
            DataSet ds = AttendanceDAC.GetDepartmentGoogleSerc(prefixText, SearchCompanyID, Siteid);
            return ConvertStingArray(ds);
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute]
        public static string[] GetCompletionListdesi(string prefixText, int count, string contextKey)
        {
            DataSet ds = AttendanceDAC.GetGoogleSerachDesignations(prefixText);
            return ConvertStingArray(ds);
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute]
        public static string[] GetCompletionListCat(string prefixText, int count, string contextKey)
        {
            DataSet ds = AttendanceDAC.GetGoogleSerachCategory(prefixText);
            return ConvertStingArray(ds);
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
        protected void gveditkbipl_Sorting(object sender, GridViewSortEventArgs e)
        {
            try
            {
                if (ViewState["SortDir"] == null)
                    e.SortDirection = SortDirection.Ascending;
                else
                    e.SortDirection = (SortDirection)ViewState["SortDir"];
                switch (e.SortExpression)
                {
                    case "Name":
                        OrderID = 0;
                        ViewState["OrderID"] = 0;
                        break;
                    case "HisID":
                        OrderID = 1;
                        ViewState["OrderID"] = 1;
                        break;
                    case "EmpId":
                        OrderID = 2;
                        ViewState["OrderID"] = 2;
                        break;
                }
                if (e.SortDirection == SortDirection.Ascending)
                {
                    Direction = 0;
                    ViewState["SortDir"] = SortDirection.Descending;
                }
                else
                {
                    Direction = 1;
                    ViewState["SortDir"] = SortDirection.Ascending;
                }
                SearchEmp();
            }
            catch (Exception ex)
            {
                clsErrorLog.HMSEventLog(ex, "EmployeeList", "gvedittkbipl_Sorting", "012");
            }
        }
        public string DocNavigateUrl(string Res, string AppID)
        {
            if (AppID != "" && Res != "")
            {
                ReturnVal = "../HMS/Resumes/" + Convert.ToInt32(AppID) + Res;
            }
            return ReturnVal;
        }
        protected void btnAdvanceSearchFilter_Click(object sender, EventArgs e)
        {
            EmpListPaging.CurrentPage = 1;
            if (txtSearchString.Text.Trim() != String.Empty)
                BindAdvanceSearch();
            else
                BindAdvanceSearchPageLoad();
        }
        private void BindAdvanceSearch()
        {
            string message = "";
            objHrCommon.PageSize = EmpListPaging.ShowRows;
            objHrCommon.CurrentPage = EmpListPaging.CurrentPage;
            foreach (ListItem item in lstColumns.Items)
            {
                if (item.Selected)
                {
                    if (message == "")
                        message = item.Text;
                    else
                        message += "," + item.Text;
                }
            }
            if (message.Contains(","))
            {
                message = "(" + message + ")";
            }
            if (message != String.Empty)
            {
                if (txtSearchString.Text.Trim() != String.Empty)
                {
                    DataSet ds = new DataSet();
                    if (chkFilterContainstable.Checked)
                    {
                        SqlParameter[] parms = new SqlParameter[9];
                        parms[0] = new SqlParameter("@TableName", DBNull.Value);
                        parms[1] = new SqlParameter("@ColumnNames", message);
                        parms[2] = new SqlParameter("@viewTable", "vh_employeeDetails");
                        parms[3] = new SqlParameter("@PKCOlumn", "EmpID");
                        parms[4] = new SqlParameter("@SearchString", txtSearchString.Text.Trim());
                        parms[5] = new SqlParameter("@CurrPage", objHrCommon.CurrentPage);
                        parms[6] = new SqlParameter("@PageSize", objHrCommon.PageSize);
                        parms[7] = new SqlParameter("@NrRecords", System.Data.SqlDbType.Int);
                        parms[7].Direction = ParameterDirection.Output;
                        parms[8] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
                        parms[8].Direction = ParameterDirection.ReturnValue;
                        ds = SQLDBUtil.ExecuteDataset("fg_ContainsTableWithRanks", parms);
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            gveditkbipl.DataSource = ds.Tables[0];
                            objHrCommon.NoofRecords = (int)parms[7].Value;
                            objHrCommon.TotalPages = (int)parms[8].Value;
                        }
                        else
                            gveditkbipl.DataSource = null;
                        gveditkbipl.DataBind();
                    }
                    else
                    {
                        SqlParameter[] parms = new SqlParameter[10];
                        parms[0] = new SqlParameter("@TableName", DBNull.Value);
                        parms[1] = new SqlParameter("ColumnNames", message);
                        parms[2] = new SqlParameter("@PKCOlumn", "Empid");
                        parms[3] = new SqlParameter("@viewTable", "vh_employeeDetails");
                        parms[4] = new SqlParameter("@SearchString", txtSearchString.Text.Trim());
                        parms[5] = new SqlParameter("@SearchKey", ddlFilterType.SelectedItem.Text);
                        parms[6] = new SqlParameter("@CurrPage", objHrCommon.CurrentPage);
                        parms[7] = new SqlParameter("@PageSize", objHrCommon.PageSize);
                        parms[8] = new SqlParameter("@NrRecords", System.Data.SqlDbType.Int);
                        parms[8].Direction = ParameterDirection.Output;
                        parms[9] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
                        parms[9].Direction = ParameterDirection.ReturnValue;
                        ds = SQLDBUtil.ExecuteDataset("fg_whereContains", parms);
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            objHrCommon.NoofRecords = (int)parms[8].Value;
                            objHrCommon.TotalPages = (int)parms[9].Value;
                            gveditkbipl.DataSource = ds.Tables[0];
                        }
                        else
                            gveditkbipl.DataSource = null;
                        gveditkbipl.DataBind();
                    }
                    EmpListPaging.Bind(objHrCommon.CurrentPage, objHrCommon.TotalPages, objHrCommon.NoofRecords, objHrCommon.PageSize);
                }
                else
                {
                    AlertMsg.MsgBox(Page, "Enter string for Search", AlertMsg.MessageType.Warning);
                }
            }
            else
            {
                AlertMsg.MsgBox(Page, "Select Columns for filter", AlertMsg.MessageType.Warning);
            }
        }
    }
}