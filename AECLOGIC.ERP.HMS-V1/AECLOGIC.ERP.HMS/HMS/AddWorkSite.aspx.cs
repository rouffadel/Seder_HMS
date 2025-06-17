using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccessLayer;
using AECLOGIC.ERP.COMMON;
using Aeclogic.Common.DAL;
using System.Data.SqlClient;
using AECLOGIC.HMS.BLL;
using System.Data;
using System.Collections;
using System.IO;

namespace AECLOGIC.ERP.HMS
{
    public partial class CMS_AddWorkSite : AECLOGIC.ERP.COMMON.WebFormMaster
    {
        private GridSort objSort;
        DataSet dsWorkSites;
        AttendanceDAC objAtt;
        DataSet ds;
        int mid = 0; 
        static int SearchCompanyID;
        static int Empdeptid = 0;
        bool viewall, Editable;
        string menuname;
        string menuid;
        HRCommon objHrCommon = new HRCommon();

        protected override void OnInit(EventArgs e)
        {
            ModuleID = 1;
            base.OnInit(e);
            AdvancedLeaveAppOthPaging.FirstClick += new Paging.PageFirst(AdvancedLeaveAppOthPaging_FirstClick);
            AdvancedLeaveAppOthPaging.PreviousClick += new Paging.PagePrevious(AdvancedLeaveAppOthPaging_FirstClick);
            AdvancedLeaveAppOthPaging.NextClick += new Paging.PageNext(AdvancedLeaveAppOthPaging_FirstClick);
            AdvancedLeaveAppOthPaging.LastClick += new Paging.PageLast(AdvancedLeaveAppOthPaging_FirstClick);
            AdvancedLeaveAppOthPaging.ChangeClick += new Paging.PageChange(AdvancedLeaveAppOthPaging_FirstClick);
            AdvancedLeaveAppOthPaging.ShowRowsClick += new Paging.ShowRowsChange(AdvancedLeaveAppOthPaging_ShowRowsClick);
            AdvancedLeaveAppOthPaging.CurrentPage = 1;
        }
        void AdvancedLeaveAppOthPaging_ShowRowsClick(object sender, EventArgs e)
        {
            AdvancedLeaveAppOthPaging.CurrentPage = 1;
            BindGrid();
        }
        void AdvancedLeaveAppOthPaging_FirstClick(object sender, EventArgs e)
        {
            BindGrid();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            objAtt = new AttendanceDAC();
            SearchCompanyID = Convert.ToInt32(Session["CompanyID"]);
            if (!IsPostBack)
            {
                GetParentMenuId();
                objSort = new GridSort();
                ViewState["Sort"] = objSort;
                GetManagers();
                btnAddEmp.Visible = false;
                ddlNewEmp.Visible = false;
                GetWorkSites(0);
                bindworksite();
                BindGrid();
                mainview.ActiveViewIndex = 0;
                ddlWS.Enabled = true;
                if (Request.QueryString.Count > 0)
                {
                    int id = Convert.ToInt32(Request.QueryString["id"].ToString());
                    if (id == 0)
                    {
                        mainview.ActiveViewIndex = 0;
                        ddlWS.Enabled = true;
                    }
                    else
                        if (id == 1)
                        {
                            mainview.ActiveViewIndex = 1;
                            ddlWS.Enabled = false;

                        }

                }

            }

            btnSubmit.Attributes.Add("onclick", "javascript:return validate();");
        }
        public int GetParentMenuId()
        {
            string URL = Request.Url.Segments[Request.Url.Segments.Length - 1] + Request.Url.Query;
            int RoleId = Convert.ToInt32(Session["RoleId"].ToString());
            int ModuleId = ModuleID;;
            DataSet  ds = AttendanceDAC.GetAllowed(RoleId, ModuleId, URL);
            int MenuId = 0;
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                MenuId = Convert.ToInt32(ds.Tables[0].Rows[0]["Under"]);
                ViewState["ViewAll"] = (bool)ds.Tables[0].Rows[0]["ViewAll"];
                btnSubmit.Enabled = (bool)ds.Tables[0].Rows[0]["Editable"];
                ViewState["Editable"] = Editable = (bool)ds.Tables[0].Rows[0]["Editable"];
                viewall = (bool)ViewState["ViewAll"];
                menuname = ds.Tables[0].Rows[0]["menuname"].ToString();
                menuid = MenuId.ToString();
                mid = Convert.ToInt32(ds.Tables[0].Rows[0]["MenuId"].ToString());
                btnAddEmp.Enabled = btnSubmit.Enabled = Editable;
            }
            return MenuId;
        }
    void bindworksite()
        {
            DataSet  ds = SQLDBUtil.ExecuteDataset("getworksites");
            ddlWSSearch.DataSource = ds.Tables[0];
            ddlWSSearch.DataTextField = "Site_Name";
            ddlWSSearch.DataValueField = "id";
            ddlWSSearch.DataBind();
            ddlWSSearch.Items.Insert(0, new ListItem("----Select---", "0"));
        }

    private void BindGrid()
        {

            int WS = 0;

        if(Convert.ToInt32(ddlWSSearch.SelectedValue)!=0)
        {
            WS = Convert.ToInt32(ddlWSSearch.SelectedValue);
        }

            objHrCommon.PageSize = AdvancedLeaveAppOthPaging.ShowRows;
            objHrCommon.CurrentPage = AdvancedLeaveAppOthPaging.CurrentPage;
            SqlParameter[] sqlParams = new SqlParameter[5];
            sqlParams[0] = new SqlParameter("@CurrentPage", objHrCommon.CurrentPage);
            sqlParams[1] = new SqlParameter("@PageSize", objHrCommon.PageSize);
            sqlParams[2] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
            sqlParams[2].Direction = ParameterDirection.ReturnValue;
            sqlParams[3] = new SqlParameter("@NoofRecords", System.Data.SqlDbType.Int);
            sqlParams[3].Direction = ParameterDirection.Output;
            sqlParams[4] = new SqlParameter("@WSId", WS);
            DataSet ds = SQLDBUtil.ExecuteDataset("HR_getWSMangervacant1", sqlParams);
            objHrCommon.NoofRecords = (int)sqlParams[3].Value;
            objHrCommon.TotalPages = (int)sqlParams[2].Value;
            gdvWS.DataSource = ds;
            gdvWS.DataBind();
            AdvancedLeaveAppOthPaging.Bind(objHrCommon.CurrentPage, objHrCommon.TotalPages, objHrCommon.NoofRecords, objHrCommon.PageSize);
        }
        protected void GetWorkForSearch(object sender, EventArgs e)
        {

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@Search", txtSearchWorksite.Text);
            param[1] = new SqlParameter("@CompanyID", Convert.ToInt32(Session["CompanyID"]));
            FIllObject.FillDropDown(ref ddlWS, "G_GET_WorkSitebyFilter", param);
            ListItem itmSelected = ddlWS.Items.FindByText(txtSearchWorksite.Text);
            if (itmSelected != null)
            {
                ddlWS.SelectedItem.Selected = false;
                itmSelected.Selected = true;
            }
            
        }
        protected void GetEmployeeSearch(object sender, EventArgs e)
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@Search", txtSearchEmp.Text);
            FIllObject.FillDropDown(ref ddlManager, "HR_GoogleSearch_ProjectMangers", param);

            ListItem itmSelected = ddlManager.Items.FindByText(txtSearchEmp.Text);
            if (itmSelected != null)
            {
                ddlManager.SelectedItem.Selected = false;
                itmSelected.Selected = true;
            }
            if (txtSearchEmp.Text != "") { ddlManager.SelectedIndex = 1; }

        }
        private void GetWorkSites(int SiteID)
        {
            ds = new DataSet();
            ds = AttendanceDAC.GetWorkSite(SiteID, '1', Convert.ToInt32(Session["CompanyID"]));
            ddlWS.DataSource = ds.Tables[0];
            ddlWS.DataTextField = "Site_Name";
            ddlWS.DataValueField = "Site_ID";
            ddlWS.DataBind();
            ddlWS.Items.Insert(0, new ListItem("--Select--", "0"));
        }

        private void GetManagers()
        {
            ds = new DataSet();
            ds = objAtt.GetPMs();
            ddlManager.DataSource = ds.Tables[0];
            ddlManager.DataTextField = "Name";
            ddlManager.DataValueField = "EmpID";
            ddlManager.DataBind();
            ddlManager.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        protected void gdvWS_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton lnkEdt = (LinkButton)e.Row.FindControl("lnkEdit");
                LinkButton lnkVacant = (LinkButton)e.Row.FindControl("lnkVacant");

                lnkEdt.Enabled = lnkVacant.Enabled = Editable;
            }
            foreach (GridViewRow gvr in gdvWS.Rows)
            {
                Label lblWSID = (Label)gvr.Cells[0].FindControl("lblPrjID");
                Label lblMgnrID = (Label)gvr.Cells[1].FindControl("lblMgnrId");
                Label lblCategary = (Label)gvr.Cells[1].FindControl("lblCategary");
                LinkButton lnkEdit = (LinkButton)gvr.Cells[4].FindControl("lnkEdit");
                lnkEdit.CommandArgument = lblWSID.Text + "@" + lblMgnrID.Text + "@" + lblCategary.Text;
                LinkButton lnkVac = (LinkButton)gvr.Cells[4].FindControl("lnkVacant");
                lnkVac.CommandArgument = lblWSID.Text + "@" + lblMgnrID.Text + "@" + lblCategary.Text;
                lnkEdit.Enabled = Convert.ToBoolean(ViewState["Editable"]);
                lnkVac.Enabled = Convert.ToBoolean(ViewState["Editable"]);
            }
        }
        protected void gdvWS_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Edt")
            {
               // Label myHyperLink = e.Row.FindControl("lblwsid") as Label;
                Control ctrl = e.CommandSource as Control;
                GridViewRow row = ctrl.Parent.NamingContainer as GridViewRow;
                Label lblwsid = (Label)gdvWS.Rows[row.RowIndex].FindControl("lblwsid");
                ddlWS.SelectedValue = lblwsid.Text.ToString();
               // txtSearchWorksite.Text = lblwsid.Text.ToString();
                //ddlWS.SelectedValue = e.CommandArgument.ToString().Split('@')[0];
               
                txtSearchWorksite.Text = ddlWS.SelectedItem.Text;
                ddlManager.SelectedValue = e.CommandArgument.ToString().Split('@')[1];
                txtSearchEmp.Text = ddlManager.SelectedItem.Text;
                hdn.Value = e.CommandArgument.ToString().Split('@')[0] + "@" + e.CommandArgument.ToString().Split('@')[2];
                btnSubmit.Text = "Update";
                mainview.ActiveViewIndex = 0;
                ddlWS.Enabled = false;
            }
            if (e.CommandName == "Vac")
            {
                try
                {
                    int UserID = Convert.ToInt32( Convert.ToInt32(Session["UserId"]).ToString());
                    int SiteID = Convert.ToInt32(e.CommandArgument.ToString().Split('@')[0]);
                    int EmpID = Convert.ToInt32(e.CommandArgument.ToString().Split('@')[1]);
                    objAtt.UpdateStatus(SiteID, EmpID, UserID);
                    BindGrid();
                    AlertMsg.MsgBox(Page, "Vacant Created!");
                }
                catch (Exception HeadVacant)
                {
                    AlertMsg.MsgBox(Page, HeadVacant.Message.ToString(),AlertMsg.MessageType.Error);
                }


            }
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            int UserID = Convert.ToInt32( Convert.ToInt32(Session["UserId"]).ToString());
            try
            {
                int n = 0;

                objAtt = new AttendanceDAC();
                if (btnSubmit.Text.Trim() == "Assign")
                {
                    n = objAtt.AddPrjManager(Convert.ToInt32(ddlWS.SelectedValue), Convert.ToInt32(ddlManager.SelectedValue), UserID);
                    AlertMsg.MsgBox(Page, " Done.!");
                    if (n == 1)
                    {
                        string strScript = "<script language='javascript' type='text/javascript' >alert('Manager Added Successfully'); </script>";
                        Page.RegisterStartupScript("suc", strScript);
                        ds = new DataSet();
                        ds = objAtt.GetWSMangervacant(0);
                        gdvWS.DataSource = ds.Tables[0];
                        gdvWS.DataBind();
                    }

                }
                else
                {
                    if (btnSubmit.Text.Trim() == "Update")
                    {
                        n = objAtt.UpdatePrjManager(Convert.ToInt32(ddlWS.SelectedValue), Convert.ToInt32(ddlManager.SelectedValue), UserID);
                        AlertMsg.MsgBox(Page, "Updated");
                        if (n > 0)
                        {
                            string strScript = "<script language='javascript' type='text/javascript' >alert('Manager Updated Successfully'); </script>";
                            Page.RegisterStartupScript("suc", strScript);
                            ds = new DataSet();
                            ds = objAtt.GetWSMangervacant(0);
                            gdvWS.DataSource = ds.Tables[0];
                            gdvWS.DataBind();
                        }

                    }
                }
                BindGrid();
            }
            catch (Exception ex)
            {

                AlertMsg.MsgBox(Page, "Sorry for the inconvinience. Try again.\nError:" + ex.Message.ToString(),AlertMsg.MessageType.Error);
            }
            txtSearchWorksite.Text = "";
            ddlManager.SelectedIndex = 0;
            txtSearchEmp.Text = "";
        }
        protected void gdvWS_Sorting(object sender, GridViewSortEventArgs e)
        {
            try
            {
                //SortGrid Object from ViewState
                objSort = (GridSort)ViewState["Sort"];

                //Get dataSet from ViewState
                dsWorkSites = (DataSet)ViewState["DataSet"];
                DataView dvWorkSites = dsWorkSites.Tables[0].DefaultView;
                //Get SortExpresssion from sordGrid Object
                dvWorkSites.Sort = objSort.GetSortExpression(e.SortExpression);
                gdvWS.DataSource = dvWorkSites;
                gdvWS.DataBind();
                //reset SortGrid object which is in ViewState
                ViewState["Sort"] = objSort;

            }
            catch (Exception ex)
            {

            }
        }
        protected void lnkNew_Click(object sender, EventArgs e)
        {
            ddlNewEmp.Visible = true;
            btnAddEmp.Visible = true;
            lnkNew.Visible = false;
            DataSet ds = AttendanceDAC.HR_SearchReimburseEmp();
            ddlNewEmp.DataSource = ds.Tables[0];
            ddlNewEmp.DataTextField = "name";
            ddlNewEmp.DataValueField = "EmpID";
            ddlNewEmp.DataBind();
            ddlNewEmp.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionList(string prefixText, int count, string contextKey)
        {
            DataSet ds = AttendanceDAC.GetGoogleABCSearchWorkSite(prefixText, SearchCompanyID);
            return ConvertStingArray(ds);// txtItems.ToArray();
        }

        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionListEmp(string prefixText, int count, string contextKey)
        {
            DataSet ds = AttendanceDAC.GetGoogleSerachEmployee(prefixText);
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
        protected void btnAddEmp_Click(object sender, EventArgs e)
        {
            int EmpID = Convert.ToInt32(ddlNewEmp.SelectedValue);
            AttendanceDAC.HR_UpdateEmpType(EmpID);
            lnkNew.Visible = true;
            ddlNewEmp.Visible = false;
            btnAddEmp.Visible = false;
            GetManagers();
            AlertMsg.MsgBox(Page, "Done.!");
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindGrid();
        }
    }
}