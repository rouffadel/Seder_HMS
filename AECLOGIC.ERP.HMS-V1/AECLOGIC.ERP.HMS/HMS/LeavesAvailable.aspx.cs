using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using AECLOGIC.HMS.BLL;
using System.Data.SqlClient;
using AECLOGIC.ERP.COMMON;
using AECLOGIC.ERP.HMS.HRClasses;
using Aeclogic.Common.DAL;
namespace AECLOGIC.ERP.HMS
{
    public partial class LeavesAvailable : AECLOGIC.ERP.COMMON.WebFormMaster
    {
        int mid = 0;
        bool viewall;
        string menuname;
        string menuid;
        static int Siteid;
        static int SearchCompanyID;
        AttendanceDAC objRights = new AttendanceDAC();
        HRCommon objHrCommon = new HRCommon();
        static int CompanyID = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["CompanyID"].ToString());
        static int ModID;
        static int Userid;
        protected override void OnInit(EventArgs e)
        {
            ModuleID = 1;
            base.OnInit(e);
            LeavesAvaliablePaging.FirstClick += new Paging.PageFirst(LeavesAvaliablePaging_FirstClick);
            LeavesAvaliablePaging.PreviousClick += new Paging.PagePrevious(LeavesAvaliablePaging_FirstClick);
            LeavesAvaliablePaging.NextClick += new Paging.PageNext(LeavesAvaliablePaging_FirstClick);
            LeavesAvaliablePaging.LastClick += new Paging.PageLast(LeavesAvaliablePaging_FirstClick);
            LeavesAvaliablePaging.ChangeClick += new Paging.PageChange(LeavesAvaliablePaging_FirstClick);
            LeavesAvaliablePaging.ShowRowsClick += new Paging.ShowRowsChange(LeavesAvaliablePaging_ShowRowsClick);
            LeavesAvaliablePaging.CurrentPage = 1;
            ModID = ModuleID;
        }
        void LeavesAvaliablePaging_ShowRowsClick(object sender, EventArgs e)
        {
            LeavesAvaliablePaging.CurrentPage = 1;
            BindPager();
        }
        void LeavesAvaliablePaging_FirstClick(object sender, EventArgs e)
        {
            BindPager();
        }
        void BindPager()
        {
            objHrCommon.PageSize = LeavesAvaliablePaging.CurrentPage;
            objHrCommon.CurrentPage = LeavesAvaliablePaging.ShowRows;
            BindGrid(objHrCommon);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                ViewState["RefUrl"] = Request.UrlReferrer.ToString();
                string str = ViewState["RefUrl"].ToString();
                Userid = Convert.ToInt32(Convert.ToInt32(Session["UserId"]).ToString());
                if (!IsPostBack)
                {
                    HiddenField hidden = (HiddenField)dtlWOProgress.Parent.FindControl("HyperLink2");
                    BindWorkSites();
                    BindPager();
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
                }
            }
            catch (Exception ex)
            {
                clsErrorLog.HMSEventLog(ex, "LeavesAvailable", "Page_Load", "001");
            }
        }
        void BindGrid(HRCommon objHrCommon)
        {
            try
            {
                objHrCommon.PageSize = LeavesAvaliablePaging.ShowRows;
                objHrCommon.CurrentPage = LeavesAvaliablePaging.CurrentPage;
                int DeptID = 0;
                if (ddldepartments.SelectedIndex != 0)
                    DeptID = Convert.ToInt32(ddldepartments.SelectedValue);
                int SiteID = 0;
                SiteID = Convert.ToInt32(ddlWs_hid.Value == "" ? "0" : ddlWs_hid.Value);
                int? EmpID = null;
                string EmpName = "";
                Nullable<int> empid;
                if (Session["Empid"] == null || Session["Empid"] == "")
                {
                    EmpID = null;
                    if (txtEmpName.Text != "")
                    {
                        EmpID = Convert.ToInt32(txtempname_hid.Value == "" ? "0" : txtempname_hid.Value);
                    }
                }
                else
                {
                    EmpID = Convert.ToInt32(Session["Empid"]);
                }
                try
                {
                    if (Convert.ToInt32(ViewState["WSID"]) > 0)
                        SiteID = Convert.ToInt32(ViewState["WSID"]);
                }
                catch { }
                DataSet ds = Leaves.GetApplicableLeavesDetailsByPaging(objHrCommon, SiteID, DeptID, EmpID, EmpName, Convert.ToInt32(Session["CompanyID"]), 0);
                if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0)
                {
                    //gvLeavesAvailable.DataSource = ds;
                    ViewState["DataSet"] = ds;
                    dtlWOProgress.DataSource = ds;
                    LeavesAvaliablePaging.Bind(objHrCommon.CurrentPage, objHrCommon.TotalPages, objHrCommon.NoofRecords, objHrCommon.PageSize);
                }
                else
                {
                    LeavesAvaliablePaging.Visible = false;
                }
                dtlWOProgress.DataBind();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public void BindWorkSites()
        {
            try
            {
                FIllObject.FillDropDown(ref ddlworksites, "HR_GetWorkSite_By_GetAvailableLeavesList");
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public DataView BindTransdetails(string TransId)
        {
            DataSet dstrans = (DataSet)ViewState["DataSet"];
            DataView dv = dstrans.Tables[1].DefaultView;
            dv.RowFilter = "EMPID='" + TransId + "'";
            return dv;
        }
        protected void dtlWOProgress_ItemCommand(object source, DataListCommandEventArgs e)
        {
            try
            {
                GridView gvexpand = (GridView)e.Item.FindControl("gvexpand");
                gvexpand.Visible = true;
                if (e.CommandName == "Grade")
                {
                    tblgrade.Visible = true;
                    DataSet ds = clempGradesConfig.HR_GetEMPGradesDetails(true);
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        gvEMPTrade.DataSource = ds;
                    }
                    gvEMPTrade.DataBind();
                }
                if (e.CommandName == "det")
                {
                    tbldetails.Visible = true;
                    int empid = Convert.ToInt32(e.CommandArgument);
                    SqlParameter[] sqlParams = new SqlParameter[5];
                    sqlParams[0] = new SqlParameter("@CurrentPage", 1);
                    sqlParams[1] = new SqlParameter("@PageSize", 10);
                    sqlParams[2] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
                    sqlParams[2].Direction = ParameterDirection.ReturnValue;
                    sqlParams[3] = new SqlParameter("@NoofRecords", System.Data.SqlDbType.Int);
                    sqlParams[3].Direction = ParameterDirection.Output;
                    sqlParams[4] = new SqlParameter("@EmpID", empid);
                    DataSet ds = SQLDBUtil.ExecuteDataset("HR_EmpSalDetails_leaveAc", sqlParams);
                    if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        gvReport.DataSource = ds;
                        gvReport.DataBind();
                    }
                }
            }
            catch (Exception ex) { AlertMsg.MsgBox(Page, ex.Message.ToString(), AlertMsg.MessageType.Error); }
        }
        protected void dtlWOProgress_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            GridView gvexpand = (GridView)e.Item.FindControl("gvexpand");
            gvexpand.Visible = false;
            //Unnamed_Click(sender, e);
            try
            {
                if (e.Item.ItemType == ListItemType.Header)
                {
                    // ((CheckBox)e.Item.FindControl("cbSelectAll")).Attributes.Add("onclick", "javascript:SelectAll('" + ((CheckBox)e.Item.FindControl("cbSelectAll")).ClientID + "')");
                }
            }
            catch (Exception ex) { AlertMsg.MsgBox(Page, ex.Message.ToString(), AlertMsg.MessageType.Error); }
        }
        static int WSID = 0;
        protected void GetWork(object sender, EventArgs e)
        {
            try
            {
                WSID = 0;
                if (txtSearchWorksite.Text.Trim() != "")
                    WSID = Convert.ToInt32(ddlWs_hid.Value == "" ? "0" : ddlWs_hid.Value);
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@Search", txtSearchWorksite.Text);
                FIllObject.FillDropDown(ref ddlworksites, "HR_GetWorkSite_By_GetAvailableLeavesFilter", param);
                ListItem itmSelected = ddlworksites.Items.FindByText(txtSearchWorksite.Text);
                if (itmSelected != null)
                {
                    ddlworksites.SelectedItem.Selected = false;
                    itmSelected.Selected = true;
                }
                ddlworksites_SelectedIndexChanged(sender, e);
            }
            catch { }
        }
        protected void GetDept(object sender, EventArgs e)
        {
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@Search", txtdept.Text);
            param[1] = new SqlParameter("@CompanyID", SearchCompanyID);
            param[2] = new SqlParameter("@SiteID", Siteid);
            FIllObject.FillDropDown(ref ddldepartments, "HR_GetDepartmentBySiteFilter", param);
            ListItem itmSelected = ddldepartments.Items.FindByText(txtdept.Text);
            if (itmSelected != null)
            {
                ddldepartments.SelectedItem.Selected = false;
                itmSelected.Selected = true;
            }
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                Session["Empid"] = null;
                LeavesAvaliablePaging.CurrentPage = 1;
                BindPager();
            }
            catch (Exception ex)
            {
                clsErrorLog.HMSEventLog(ex, "LeavesAvailable", "btnSearch_Click", "002");
            }
        }
        protected void btnSync_Click(object sender, EventArgs e)
        {
            try
            {
                int UID = Convert.ToInt32(Session["UserId"]);
                if (txtEmpName.Text != "")
                {
                    int SiteID = Convert.ToInt32(ddlWs_hid.Value == "" ? "0" : ddlWs_hid.Value);
                    int DeptID = Convert.ToInt32(ddldepartments.SelectedItem.Value);
                    if (SiteID > 0)
                    {
                        int EmpNatureID = 0; //Convert.ToInt32(ddlEmpNature.SelectedValue);
                        DataSet ds = AttendanceDAC.HR_GetEMPIDsByWSID(SiteID, DeptID, EmpNatureID, "");
                        int Count = 0;
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow dr in ds.Tables[0].Rows)
                            {
                                Count++;
                                Leaves.HR_LeaveSyncCal(Convert.ToInt32(dr["EmpID"]), DateTime.Today, UID);
                            }
                            AlertMsg.MsgBox(Page, "Synchronization  Done " + Count.ToString() + "/" + ds.Tables[0].Rows.Count);
                            lblStatus.Text = "";
                        }
                        BindPager();
                    }
                    else
                    { AlertMsg.MsgBox(Page, "Select Worksite"); }
                }
                else
                {
                    int EmpID = Convert.ToInt32(txtempname_hid.Value == "" ? "0" : txtempname_hid.Value);
                    Leaves.HR_LeaveSyncCal(Convert.ToInt32(EmpID), DateTime.Today, UID);
                    BindPager();
                }
            }
            catch (Exception ex)
            {
                clsErrorLog.HMSEventLog(ex, "LeavesAvailable", "btnSync_Click", "003");
            }
        }
        protected void ddlworksites_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindDeparmetBySite(Convert.ToInt32(ddlworksites.SelectedValue));
            LeavesAvaliablePaging.CurrentPage = 1;
            ViewState["SiteidS"] = Convert.ToInt32(ddlworksites.SelectedValue);
            Siteid = Convert.ToInt32(ViewState["SiteidS"]);
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionList(string prefixText, int count, string contextKey)
        {
            DataSet ds = AttendanceDAC.GetWorkSites(prefixText, CompanyID, Userid, ModID, 0);
            DataTable dt = ds.Tables[0];
            List<string> items = new List<string>(count);
            var rtval = new Dictionary<string, string>();
            foreach (DataRow row in dt.Rows)
            {
                string str = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(row["Name"].ToString(), row["ID"].ToString());
                items.Add(str);
            }
            return items.ToArray(); ;
            //return ConvertStingArray(ds);// txtItems.ToArray();
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionListDep(string prefixText, int count, string contextKey)
        {
            DataSet ds = AttendanceDAC.GetSearchDesiginationFilterActive(prefixText, SearchCompanyID, Siteid);
            return ConvertStingArray(ds);// txtItems.ToArray();
        }
        //for empid
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionEmpList(string prefixText, int count, string contextKey)
        {
            DataSet ds = AttendanceDAC.GetGoogleSearch_by_Empid_All_ByEMPID(prefixText.Trim(), WSID);
            return ConvertStingArray(ds);// txtItems.ToArray();
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionEmpNameList(string prefixText, int count, string contextKey)
        {
            DataSet ds = AttendanceDAC.GetGoogleSearch_by_Empid_All_ByEMPID(prefixText.Trim(), WSID);
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
            string[] rtval = Array.ConvertAll(ds.Tables[0].Select(), new Converter<DataRow, string>(DataRowToDouble));
            return rtval;
        }
        public static string DataRowToDouble(DataRow dr)
        {
            return dr["Name"].ToString();
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
        protected void ddldepartments_SelectedIndexChanged(object sender, EventArgs e)
        {
            LeavesAvaliablePaging.CurrentPage = 1;
        }
        protected void Unnamed_Click(object sender, EventArgs e)
        {
            GridView gvexpand = (GridView)dtlWOProgress.Parent.FindControl("gvexpand");
            if (gvexpand != null)
            {
                gvexpand.Visible = true;
            }
        }
    }
}
