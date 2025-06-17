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
using AECLOGIC.HMS.BLL;
using AECLOGIC.ERP.COMMON;

namespace AECLOGIC.ERP.HMSV1
{
    public partial class ReAssignPasswordV1 : AECLOGIC.ERP.COMMON.WebFormMaster
    {

        AttendanceDAC objRights = new AttendanceDAC();
        HRCommon objHrCommon = new HRCommon();
        int mid = 0;
        bool viewall;
        string menuname;
        static int SearchCompanyID;
        static int Siteid;
        string menuid;
        protected override void OnInit(EventArgs e)
        {
            ModuleID = 1;
            base.OnInit(e);
        
            ReasignPaging.FirstClick += new Paging.PageFirst(ReasignPaging_FirstClick);
            ReasignPaging.PreviousClick += new Paging.PagePrevious(ReasignPaging_FirstClick);
            ReasignPaging.NextClick += new Paging.PageNext(ReasignPaging_FirstClick);
            ReasignPaging.LastClick += new Paging.PageLast(ReasignPaging_FirstClick);
            ReasignPaging.ChangeClick += new Paging.PageChange(ReasignPaging_FirstClick);
            ReasignPaging.ShowRowsClick += new Paging.ShowRowsChange(ReasignPaging_ShowRowsClick);
            ReasignPaging.CurrentPage = 1;
        }
        void ReasignPaging_ShowRowsClick(object sender, EventArgs e)
        {
            ReasignPaging.CurrentPage = 1;
            BindPager();
        }
        void ReasignPaging_FirstClick(object sender, EventArgs e)
        {
            BindPager();
        }
        void BindPager()
        {

            objHrCommon.PageSize = ReasignPaging.CurrentPage;
            objHrCommon.CurrentPage = ReasignPaging.ShowRows;
            BindEmployees(objHrCommon);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
               
               
                SearchCompanyID = Convert.ToInt32(Session["CompanyID"]);
               
                if (!IsPostBack)
                {
                    BindWorkSites();
                    BindPager();
                }

            }
            catch (Exception ex)
            {
                AlertMsg.MsgBox(Page, "Sorry for the inconvience. Try again.\nError:" + ex.Message.ToString(),AlertMsg.MessageType.Error);
            }
        }

        protected void GetWork(object sender, EventArgs e)
        {

            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@Search", txtSearchWorksite.Text);
            FIllObject.FillDropDown(ref ddlworksites, "HR_GetWorkSite_By_EmpOrderFilter", param);
            ListItem itmSelected = ddlworksites.Items.FindByText(txtSearchWorksite.Text);
            if (itmSelected != null)
            {
                ddlworksites.SelectedItem.Selected = false;
                itmSelected.Selected = true;
            }
            ddlworksites_SelectedIndexChanged(sender, e);
            txtdept.Text = "";
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
     
     
        public void BindWorkSites()
        {

            try
            {

                FIllObject.FillDropDown(ref ddlworksites, "HR_GetWorkSite_By_EmpList");

            }
            catch (Exception e)
            {
                throw e;
            }
        }
      
        protected void grdEmployees_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (Session["Site"].ToString() == "1")
                {

                    if (e.CommandName == "ReSet")
                    {

                        objHrCommon.EmpID = Convert.ToDouble(e.CommandArgument);
                        GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).Parent.Parent;
                        TextBox txtUserName = (TextBox)grdEmployees.Rows[row.RowIndex].FindControl("txtUserName");
                        TextBox txtPassword = (TextBox)grdEmployees.Rows[row.RowIndex].FindControl("txtPassword");

                        if (txtUserName.Text.Trim() != string.Empty && txtPassword.Text.Trim() != string.Empty)
                        {
                            objHrCommon.Username = txtUserName.Text.Trim();
                            objHrCommon.NewPassWord = FormsAuthentication.HashPasswordForStoringInConfigFile(txtPassword.Text.Trim(), "MD5");
                            objHrCommon.UserID = Convert.ToInt32( Convert.ToInt32(Session["UserId"]).ToString());
                            int Cnt = Convert.ToInt32(objRights.ResetUserNamePassword(objHrCommon));
                            if (Cnt == 0)
                            {
                                AlertMsg.MsgBox(Page, "Username not available");
                            }
                            else
                            {
                                txtUserName.Enabled = false;
                                AlertMsg.MsgBox(Page, "UserName and Password ReSet");

                            }
                        }
                        else
                        {
                            AlertMsg.MsgBox(Page, "UserName and Password Should not be empty ");
                        }
                    }
                }
                else
                {

                    AlertMsg.MsgBox(Page, "You have no role(s) to access this Operation.\nContact Head office HR-Administrator.");
                }



            }
            catch (Exception ex)
            {
                AlertMsg.MsgBox(Page, "Sorry for the inconvience. Try again.\nError:" + ex.Message.ToString(),AlertMsg.MessageType.Error);
            }
        }
        public void BindEmployees(HRCommon objHrCommon)
        {
            try
            {
                objHrCommon.PageSize = ReasignPaging.ShowRows;
                objHrCommon.CurrentPage = ReasignPaging.CurrentPage;

                objHrCommon.SiteID = Convert.ToInt32(ddlworksites.SelectedItem.Value);
                objHrCommon.DeptID = Convert.ToDouble(ddldepartments.SelectedItem.Value);
                objHrCommon.FName = txtusername.Text;

                 
               DataSet ds = AttendanceDAC.SearchEmpListByPaging(objHrCommon, Convert.ToInt32(Session["CompanyID"]));
                if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0)
                {
                    grdEmployees.DataSource = ds;
                    grdEmployees.DataBind();
                    ReasignPaging.Visible = true;
                }
                else
                {
                    ReasignPaging.Visible = false;
                    grdEmployees.DataSource = null;
                    grdEmployees.DataBind();
                }
                ReasignPaging.Bind(objHrCommon.CurrentPage, objHrCommon.TotalPages, objHrCommon.NoofRecords, objHrCommon.PageSize);

            }
            catch (Exception e)
            {
                throw e;
            }
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtempid.Text.Trim() == String.Empty)
                {
                    objHrCommon.EmpID = 0;
                }
                else
                {
                    objHrCommon.EmpID = Convert.ToInt32(txtempid.Text);
                }
                objHrCommon.SiteID = Convert.ToInt32(ddlworksites.SelectedItem.Value);
                objHrCommon.DeptID = Convert.ToDouble(ddldepartments.SelectedItem.Value);
                objHrCommon.FName = txtusername.Text;
               
                ReasignPaging.CurrentPage = 1;

                BindPager();
            }
            catch (Exception ex)
            {
                AlertMsg.MsgBox(Page, "Sorry for the inconvinience. Try again.\nError:" + ex.Message.ToString(),AlertMsg.MessageType.Error);
            }
        }
        protected void grdEmployees_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                TextBox txtUName = (TextBox)e.Row.FindControl("txtUserName");
                if (txtUName.Text != "")
                {
                    txtUName.Enabled = false;
                }
                else
                {
                    txtUName.Enabled = true;
                }
            }
        }

        protected void ddlworksites_SelectedIndexChanged(object sender, EventArgs e)
        {
             BindDeparmetBySite(Convert.ToInt32(ddlworksites.SelectedValue));
             ViewState["SiteidS"] = Convert.ToInt32(ddlworksites.SelectedValue);
             Siteid = Convert.ToInt32(ViewState["SiteidS"]);
            
        }

        //Added By Rijwan for Worksite Google Search 
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionList(string prefixText, int count, string contextKey)
        {
            DataSet ds = AttendanceDAC.GetWorkSiteActive(prefixText);
            return ConvertStingArray(ds);// txtItems.ToArray();
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionListDep(string prefixText, int count, string contextKey)
        {
            DataSet ds = AttendanceDAC.GetSearchDesiginationFilterActive(prefixText, SearchCompanyID, Siteid);
            return ConvertStingArray(ds);// txtItems.ToArray();
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
    }
}
