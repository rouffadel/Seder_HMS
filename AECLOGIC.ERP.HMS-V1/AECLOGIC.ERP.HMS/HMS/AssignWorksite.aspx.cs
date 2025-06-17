using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using AECLOGIC.HMS.BLL;
using System.Data;
using System.Data.SqlClient;
using AECLOGIC.ERP.COMMON;
using Aeclogic.Common.DAL;
using AECLOGIC.ERP.HMS.HRClasses;


namespace AECLOGIC.ERP.HMS
{
    public partial class AssignWorksite : AECLOGIC.ERP.COMMON.WebFormMaster
    {
        Common PG = new Common();
        MasterPage objMaster;
        private GridSort objSort;
        Common1 obj = new Common1();
        protected override void OnInit(EventArgs e)
        {
            ModuleID = Convert.ToInt32(Request.QueryString["modid"]);
            base.OnInit(e);
            MPPaging.FirstClick += new Paging.PageFirst(EmpListPaging_FirstClick);
            MPPaging.PreviousClick += new Paging.PagePrevious(EmpListPaging_FirstClick);
            MPPaging.NextClick += new Paging.PageNext(EmpListPaging_FirstClick);
            MPPaging.LastClick += new Paging.PageLast(EmpListPaging_FirstClick);
            MPPaging.ChangeClick += new Paging.PageChange(EmpListPaging_FirstClick);
            MPPaging.ShowRowsClick += new Paging.ShowRowsChange(EmpListPaging_ShowRowsClick);
            MPPaging.CurrentPage = 1;
        }

        void EmpListPaging_ShowRowsClick(object sender, EventArgs e)
        {
            MPPaging.CurrentPage = 1;
            BindPager();
        }

        void EmpListPaging_FirstClick(object sender, EventArgs e)
        {
            BindPager();
        }

        void BindPager()
        {
            PG.PageSize = MPPaging.CurrentPage;
            PG.CurrentPage = MPPaging.ShowRows;
            BindGrid(PG);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                objMaster = this.Master;
                if (!IsPostBack)
                {
                    GetParentMenuId();
                    objSort = new GridSort();
                    ViewState["Sort"] = objSort;
                    int EmpId =  Convert.ToInt32(Session["UserId"]);
                    FIllObject.FillDropDown(ref ddlDropWorkSite, "AMS_GetWorkSites_Search", "--ALL--");
                    FIllObject.FillCheckBoxList(ref ChkCostCenter, GetWorksites());
                    int worksite = Convert.ToInt32(ddlDropWorkSite.SelectedValue);
                    FIllObject.FillDropDown(ref ddlDept, "AMS_GETDEPARTMENTS_Search", "--ALL--");
                    ChkCostCenter.Visible = false;
                    BtnSave.Visible = false;
                }
            }
            catch (Exception ex)
            {
                AlertMsg.MsgBox(Page, ex.Message.ToString(),AlertMsg.MessageType.Error);

            }
        }
        protected void rbtnSelect_CheckedChanged(object sender, EventArgs e)
        {
            ChkCostCenter.DataSource = null;
            ChkCostCenter.DataBind();
            foreach (ListItem Item in ChkCostCenter.Items)
            {
                Item.Selected = false;
            }
            GetSelectedRecord();
        }
        private void GetSelectedRecord()
        {
            for (int i = 0; i < GVIndent.Rows.Count; i++)
            {

                RadioButton rb = (RadioButton)GVIndent.Rows[i].Cells[0].FindControl("RadioButton1");


                if (rb != null)
                {

                    if (rb.Checked)
                    {
                        Label lblempid = (Label)GVIndent.Rows[i].Cells[0].FindControl("lblempid");
                        foreach (ListItem Item in ChkCostCenter.Items)
                        {
                            Item.Selected = false;
                        }
                        int EMPID = Convert.ToInt32(lblempid.Text.Trim());
                        DataSet DS = AttendanceDAC.GETEMPCOSTCENTERS(EMPID, ModuleID);

                        foreach (DataRow ROW in DS.Tables[0].Rows)
                        {
                            foreach (ListItem Item in ChkCostCenter.Items)
                            {
                                if (ROW[0].ToString() == Item.Value)
                                {
                                    Item.Selected = true;
                                }
                            }
                        }

                    }

                }

            }
        }
        public int GetParentMenuId()
        {
            int Id=1;
            string URL;
            URL = Request.Url.Segments[Request.Url.Segments.Length - 1] + Request.Url.Query;
            Session["CurrentPage"] = URL;
            int RoleId = Convert.ToInt32(Session["RoleId"].ToString());
            int ModuleId = ModuleID;
            DataSet ds = obj.GetAllowed(RoleId, ModuleId, URL, Id);
            int MenuId = 0;
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                MenuId = Convert.ToInt32(ds.Tables[0].Rows[0]["Under"]);
                this.Title = ds.Tables[0].Rows[0]["title"].ToString();

            }
            return MenuId;
        }
        protected void chkAll_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAll.Checked == true)
            {
                foreach (ListItem li in ChkCostCenter.Items)
                {
                    li.Selected = true;
                }
            }
            else
            {
                foreach (ListItem li in ChkCostCenter.Items)
                {
                    li.Selected = false;
                }
            }
        }


        public void BindGrid(Common ObjPager)
        {

            ChkCostCenter.Visible = false;
            int? WorksiteID = null;
            string EmpName = "";
            int? DeptID = null;
            int Empid = 0;

            if (ddlDropWorkSite.SelectedIndex != 0 && ddlDropWorkSite.SelectedValue != "")
                WorksiteID = Convert.ToInt32(ddlDropWorkSite.SelectedValue);

            if (ddlDept.SelectedIndex != 0 && ddlDept.SelectedValue != "")
                DeptID = Convert.ToInt32(ddlDept.SelectedValue);
            try
            {
                Empid = Convert.ToInt32(txtSearchEmployee.Text.Substring(0, 4));
            }
            catch { }
            ObjPager.CurrentPage = MPPaging.CurrentPage;
            ObjPager.PageSize = MPPaging.ShowRows;
            DataSet Ds = AttendanceDAC.GetEmployeeRolesByPaging(WorksiteID, EmpName, Empid, DeptID, ObjPager);
            if (Ds != null && Ds.Tables[0].Rows.Count > 0 && Ds.Tables.Count > 0)
            {
                BtnSave.Visible = true;
                GVIndent.DataSource = Ds;
               
            }
            else
            {
                GVIndent.DataSource = null;
                
            }
            GVIndent.DataBind();
            MPPaging.Bind(MPPaging.CurrentPage, ObjPager.TotalPages, ObjPager.NoofRecords, ObjPager.PageSize);

            ViewState["DataSet"] = Ds;

            if (Request.QueryString["SE"] != "" && Request.QueryString["SE"] != null)
            {
                foreach (GridViewRow row in GVIndent.Rows)
                {
                    DropDownList DrpRoles = new DropDownList();
                    DrpRoles = (DropDownList)row.FindControl("ddlRoles");
                    if (DrpRoles.SelectedItem.Text == "Administrator")
                    {
                        DrpRoles.Enabled = false;
                    }
                }
            }
            if (GVIndent.Rows.Count > 0)
                ChkCostCenter.Visible = true;
        }
        protected void BtnSearch_Click(object sender, EventArgs e)
        {
            tbl1Grid.Visible = true;

            BindGrid(PG);
        }
        protected void GVIndent_Sorting(object sender, GridViewSortEventArgs e)
        {
            objSort = (GridSort)ViewState["Sort"];
            DataSet ds = (DataSet)ViewState["DataSet"];
            DataView dv = ds.Tables[0].DefaultView;
            dv.Sort = objSort.GetSortExpression(e.SortExpression);
            GVIndent.DataSource = dv;
            GVIndent.DataBind();
            ViewState["Sort"] = objSort;
        }
        protected void GVIndent_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            foreach (GridViewRow row in GVIndent.Rows)
            {
                CheckBox chk = new CheckBox();
                chk = (CheckBox)row.FindControl("Chk");
                chk.Checked = false;
            }
            if (e.CommandName == "Ed")
            {
                LinkButton lnk = new LinkButton();
                lnk = (LinkButton)e.CommandSource;
                GridViewRow selectedRow = (GridViewRow)lnk.Parent.Parent;
                CheckBox chk = new CheckBox();
                chk = (CheckBox)selectedRow.FindControl("Chk");
                chk.Checked = true;

                foreach (ListItem Item in ChkCostCenter.Items)
                {
                    Item.Selected = false;
                }

                int EMPID = Convert.ToInt32(e.CommandArgument);
                DataSet DS = AttendanceDAC.GETEMPCOSTCENTERS(EMPID, ModuleID);

                foreach (DataRow ROW in DS.Tables[0].Rows)
                {
                    foreach (ListItem Item in ChkCostCenter.Items)
                    {
                        if (ROW[0].ToString() == Item.Value)
                        {
                            Item.Selected = true;
                        }
                    }
                }
            }

        }
        protected void GVIndent_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            foreach (GridViewRow gvr in GVIndent.Rows)
            {
                LinkButton lnkUpd = (LinkButton)gvr.Cells[5].FindControl("EditE");
            }
        }
        protected void BtnSave_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow row in GVIndent.Rows)
            {
                RadioButton rdb = new RadioButton();
                Label lblEmpid = new Label();

                rdb = (RadioButton)row.FindControl("RadioButton1");
                lblEmpid = (Label)row.FindControl("lblempid");

                if (rdb.Checked)
                {
                    foreach (ListItem item in ChkCostCenter.Items)
                    {

                        int Status = 0;
                        if (item.Selected)
                            Status = 1;
                        int EmpID = Convert.ToInt32(lblEmpid.Text);
                        int CostCenterID = Convert.ToInt32(item.Value);
                        AttendanceDAC.MAPCOSTCENTER(EmpID, Status, CostCenterID, ModuleID);

                    }
                }
            }

            AlertMsg.MsgBox(Page, "Saved");
        }
        public static DataSet GetWorksites()
        {
            try
            {
                DataSet dsworksite = SQLDBUtil.ExecuteDataset("AMS_GetWorkSites");
                return dsworksite;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute]
        public static string[] GetCompletionListEmp(string prefixText, int count, string contextKey)
        {
            DataSet ds = AttendanceDAC.GetGoogleSerachAllEmployee(prefixText);
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
        public static DataSet GetDepartments()
        {
            return AttendanceDAC.GetDepartments();
        }
    }
}