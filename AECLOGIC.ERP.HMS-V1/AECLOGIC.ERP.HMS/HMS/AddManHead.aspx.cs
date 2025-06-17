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
using Aeclogic.Common.DAL;

namespace AECLOGIC.ERP.HMS
{
    public partial class CMS_AddManHead : AECLOGIC.ERP.COMMON.WebFormMaster
    {
      
        int mid = 0;
        bool viewall, Editable;
        static int SearchCompanyID;
        static int Empdeptid=0;
        static int DeptID = 0;
        string menuname;
        string menuid;
        private GridSort objSort;

        AttendanceDAC objAtt;
        DataSet ds;
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
            lblStatus.Text = String.Empty;
            SearchCompanyID = Convert.ToInt32(Session["CompanyID"]);
            objAtt = new AttendanceDAC();
            if (!IsPostBack)
            {
                GetParentMenuId();
                objSort = new GridSort();
                ViewState["Sort"] = objSort;
                GetWorkSites(0);
                ddlWSSearch.Items[0].Selected = true;
                GetDepartments(0);
                mainview.ActiveViewIndex = 0;
                ddlWS.Enabled = true;
                ddlDept.Enabled = true;

                if (Request.QueryString.Count > 0)
                {
                    int id = Convert.ToInt32(Request.QueryString["id"].ToString());
                    if (id == 0)
                    {
                        mainview.ActiveViewIndex = 0;
                        ddlWS.Enabled = true;
                        ddlDept.Enabled = true;
                    }
                    else
                        if (id == 1)
                        {
                            mainview.ActiveViewIndex = 1;
                            ddlWS.Enabled = false;
                            ddlDept.Enabled = false;

                        }
                   
                }
                BindGrid();
            }
        }
        public int GetParentMenuId()
        {
            string URL = Request.Url.Segments[Request.Url.Segments.Length - 1] + Request.Url.Query;
            int RoleId = Convert.ToInt32(Session["RoleId"].ToString());
            int ModuleId = ModuleID;;


            DataSet ds = AttendanceDAC.GetAllowed(RoleId, ModuleId, URL);
            int MenuId = 0;
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                MenuId = Convert.ToInt32(ds.Tables[0].Rows[0]["Under"]);
                ViewState["Editable"] = Editable = (bool)ds.Tables[0].Rows[0]["Editable"];
                gdvWS.Columns[3].Visible = Convert.ToBoolean(ds.Tables[0].Rows[0]["Editable"].ToString());
                //for remove extra standard line
                //gdvWS.Columns[4].Visible = Convert.ToBoolean(ds.Tables[0].Rows[0]["Editable"].ToString());
                gdvWS.Columns[5].Visible = Convert.ToBoolean(ds.Tables[0].Rows[0]["Editable"].ToString());
                ViewState["ViewAll"] = (bool)ds.Tables[0].Rows[0]["ViewAll"];
                viewall = (bool)ViewState["ViewAll"];
                menuname = ds.Tables[0].Rows[0]["menuname"].ToString();
                btnSubmit.Enabled = (bool)ds.Tables[0].Rows[0]["Editable"];
                this.Title = ds.Tables[0].Rows[0]["Title"].ToString();
                btnSubmit.Enabled = Editable;
            }
            return MenuId;
        }
        private void BindGrid()
        {
            int WS=0;
            int Dept = 0;
            if (Convert.ToInt32(ddlWSSearch.SelectedValue )!= 0)
            {
                WS = Convert.ToInt32(ddlWSSearch.SelectedValue);
            }
            objHrCommon.PageSize = AdvancedLeaveAppOthPaging.ShowRows;
            objHrCommon.CurrentPage = AdvancedLeaveAppOthPaging.CurrentPage;
            SqlParameter[] sqlParams = new SqlParameter[6];
            sqlParams[0] = new SqlParameter("@CurrentPage", objHrCommon.CurrentPage);
            sqlParams[1] = new SqlParameter("@PageSize", objHrCommon.PageSize);
            sqlParams[2] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
            sqlParams[2].Direction = ParameterDirection.ReturnValue;
            sqlParams[3] = new SqlParameter("@NoofRecords", System.Data.SqlDbType.Int);
            sqlParams[3].Direction = ParameterDirection.Output;

            sqlParams[4] = new SqlParameter("@WSId", WS);
            sqlParams[5] = new SqlParameter("@DeptId", Dept);
            ds = SQLDBUtil.ExecuteDataset("HR_GetDeptHeadsAll1", sqlParams);
            objHrCommon.NoofRecords = (int)sqlParams[3].Value;
            objHrCommon.TotalPages = (int)sqlParams[2].Value;
            gdvWS.DataSource = ds;
            gdvWS.DataBind();
            AdvancedLeaveAppOthPaging.Bind(objHrCommon.CurrentPage, objHrCommon.TotalPages, objHrCommon.NoofRecords, objHrCommon.PageSize);
            gdvWS_Sorting1(gdvWS, new GridViewSortEventArgs("Site_Name", SortDirection.Ascending));
        }

        protected void GetWork(object sender, EventArgs e)
        {
            
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@Search", txtSearchWorksite.Text);
            param[1] = new SqlParameter("@CompanyID", Convert.ToInt32(Session["CompanyID"]));
            FIllObject.FillDropDown(ref ddlWS, "G_GET_WorkSitebyFilter",param);
            ListItem itmSelected = ddlWS.Items.FindByText(txtSearchWorksite.Text);
            if (itmSelected != null)
            {
                ddlWS.SelectedItem.Selected = false;
                itmSelected.Selected = true;
            }
            ddlWS_SelectedIndexChanged(sender, e);
            txtdept.Focus();
        }

        protected void GetHeadEmp(object sender, EventArgs e)
        {
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@Search", txtSearchEmp.Text);
            param[1] = new SqlParameter("@CompanyID", Convert.ToInt32(Session["CompanyID"]));
            param[2] = new SqlParameter("@DeptID", Empdeptid);
           
            FIllObject.FillDropDown(ref ddlHead, "G_GET_EmpNamesbyFilter", param);
            ListItem itmSelected = ddlHead.Items.FindByText(txtSearchEmp.Text);
            if (itmSelected != null)
            {
                ddlHead.SelectedItem.Selected = false;
                itmSelected.Selected = true;
            }
            txtSearchEmp.Focus();
        }

        protected void GetDept(object sender, EventArgs e)
        {
            try { 
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@Search", txtdept.Text);
            param[1] = new SqlParameter("@CompanyID", SearchCompanyID);
            param[2] = new SqlParameter("@DeptID", Empdeptid);

            FIllObject.FillDropDown(ref ddlDept, "G_GET_DesignationbyFilter", param);
            ListItem itmSelected = ddlDept.Items.FindByText(txtdept.Text);
            if (itmSelected != null)
            {
                ddlDept.SelectedItem.Selected = false;
                itmSelected.Selected = true;
            }
            ddlDept_SelectedIndexChanged(sender, e);
            if (txtdept.Text != "") { ddlDept.SelectedIndex = 1; }
            }
            catch { }
        }
        private void GetDepartments(int Deptid)
        {
            ds = new DataSet();
            ds = objAtt.GetDepartments(Deptid);
            ddlDept.DataSource = ds.Tables[0];
            ddlDept.DataTextField = "Deptname";
            ddlDept.DataValueField = "DepartmentUId";
            ddlDept.DataBind();
            ddlDept.Items.Insert(0, new ListItem("--Select--", "0"));
            BindHeads(0);
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


            ds = AttendanceDAC.GetWorkSite_By_Data();
            ddlWSSearch.DataSource = ds.Tables[0];
            ddlWSSearch.DataTextField = "Site_Name";
            ddlWSSearch.DataValueField = "Site_ID";
            ddlWSSearch.DataBind();
            ddlWSSearch.Items.Insert(0, new ListItem("--ALL--", "0"));
        }

        public void BindHeads(int deptid)
        {

            AttendanceDAC objAtt = new AttendanceDAC();
            DataSet ds = objAtt.GetHeads(deptid, Convert.ToInt32(Session["CompanyID"]));

            ddlHead.DataSource = ds.Tables[0];
            ddlHead.DataTextField = "Name";
            ddlHead.DataValueField = "EmpID";
            ddlHead.DataBind();
            ddlHead.Items.Insert(0, new ListItem("--Select--", "0"));

        }
        protected void gdvWS_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{
            //    LinkButton lnkEdt = (LinkButton)e.Row.FindControl("lnkEdit");
            //    LinkButton lnkVac = (LinkButton)e.Row.FindControl("lnkVac");
            //    lnkEdt.Enabled = lnkVac.Enabled = Editable;
            //}
            foreach (GridViewRow gvr in gdvWS.Rows)
            {
                Label lblWSID = (Label)gvr.Cells[0].FindControl("lblPrjID");
                Label lblDeptID = (Label)gvr.Cells[2].FindControl("lblDeptID");
                Label lblHeadID = (Label)gvr.Cells[4].FindControl("lblHeadID");
                LinkButton lnkEdit = (LinkButton)gvr.Cells[6].FindControl("lnkEdit");
                LinkButton lnkVac = (LinkButton)gvr.Cells[7].FindControl("lnkVac");
                lnkEdit.CommandArgument = lblWSID.Text + "@" + lblDeptID.Text + "@" + lblHeadID.Text;
                lnkVac.CommandArgument = lblWSID.Text + "@" + lblDeptID.Text + "@" + lblHeadID.Text;
                lnkVac.Attributes.Add("onclick", "javascript:return confirm('Do you want to create vacancy for this department?');");

            }
        }
        protected void gdvWS_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            if (e.CommandName == "Edt")
            {
                try
                {
                    Control ctrl = e.CommandSource as Control;
                    GridViewRow row = ctrl.Parent.NamingContainer as GridViewRow;
                    Label lblwsid = (Label)gdvWS.Rows[row.RowIndex].FindControl("lblwsid");
                    ddlWS.SelectedValue = lblwsid.Text.ToString();

                    //ddlWS.SelectedValue = e.CommandArgument.ToString().Split('@')[0];
                    txtSearchWorksite.Text = ddlWS.SelectedItem.ToString();
                    ddlDept.SelectedValue = e.CommandArgument.ToString().Split('@')[1];
                    txtdept.Text = ddlDept.SelectedItem.Text;
                    try
                    {
                        ddlHead.SelectedValue = e.CommandArgument.ToString().Split('@')[2];
                        txtSearchEmp.Text = ddlHead.SelectedItem.ToString();
                    }
                    catch { ddlHead.SelectedValue = "0"; txtSearchEmp.Text = ""; }
                    hdn.Value = e.CommandArgument.ToString().Split('@')[0] + "@" + e.CommandArgument.ToString().Split('@')[1] + "@" + e.CommandArgument.ToString().Split('@')[2];
                    btnSubmit.Text = "Update";
                    mainview.ActiveViewIndex = 0;
                }
                catch (Exception DeptEdt)
                {
                    //AlertMsg.MsgBox(Page, DeptEdt.Message.ToString(),AlertMsg.MessageType.Error);
                    lblStatus.Text = DeptEdt.Message.ToString();
                    lblStatus.ForeColor = System.Drawing.Color.Red;
                }


            }
            if (e.CommandName == "Vac")
            {
                try
                {
                    objAtt = new AttendanceDAC();
                    int UserID = Convert.ToInt32( Convert.ToInt32(Session["UserId"]).ToString());
                   
                    objAtt.DeleteDeptHead(Convert.ToInt32(e.CommandArgument.ToString().Split('@')[0]), Convert.ToInt32(e.CommandArgument.ToString().Split('@')[1]), Convert.ToInt32(e.CommandArgument.ToString().Split('@')[2]), UserID);

                    ds = new DataSet();
                    ds = objAtt.GetDeptHeads(0, 0);
                    gdvWS.DataSource = ds.Tables[0];
                    gdvWS.DataBind();
                    txtdept.Text = txtSearchEmp.Text = txtSearchWorksite.Text = "";
                }
                catch (Exception DeptVac)
                {
                   // AlertMsg.MsgBox(Page, DeptVac.Message.ToString(),AlertMsg.MessageType.Error);
                    lblStatus.Text = DeptVac.Message.ToString();
                    lblStatus.ForeColor = System.Drawing.Color.Red;
                }

            }

            if (e.CommandName == "Del")
            {
                try
                {
                    SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings["strConn"].ToString());
                    SqlDataAdapter adp;
                    objAtt = new AttendanceDAC();
                    DataSet EMPResultSet = ExecuteQuery("SELECT COUNT(*) FROM T_G_EmployeeMaster WHERE DeptNo='" + Convert.ToInt32(e.CommandArgument.ToString().Split('@')[1]) + "' AND Categary='" + Convert.ToInt32(e.CommandArgument.ToString().Split('@')[0]) + "' AND Status='y' ");

                    if (EMPResultSet.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        objAtt.PerminantDeleteDeptHead(Convert.ToInt32(e.CommandArgument.ToString().Split('@')[0]), Convert.ToInt32(e.CommandArgument.ToString().Split('@')[1]), Convert.ToInt32(e.CommandArgument.ToString().Split('@')[2]));
                        BindGrid();
                        txtdept.Text = txtSearchEmp.Text = txtSearchWorksite.Text = "";
                    }
                    else
                    {
                        //AlertMsg.MsgBox(Page, "Unable to delete department.\n\nReason: one or more employees associated with this department.");
                        lblStatus.Text = "Unable to delete department.\n\nReason: one or more employees associated with this department.";
                        lblStatus.ForeColor = System.Drawing.Color.Red;
                    }
                    
                }
                catch (Exception DeptDel)
                {
                   // AlertMsg.MsgBox(Page, DeptDel.Message.ToString(),AlertMsg.MessageType.Error);
                    lblStatus.Text = DeptDel.Message.ToString();
                    lblStatus.ForeColor = System.Drawing.Color.Red;
                }

            }


        }
        protected void ddlWS_SelectedIndexChanged(object sender, EventArgs e)
        {
            objAtt = new AttendanceDAC();
            ds = new DataSet();
            ds = objAtt.GetDeptHeads(Convert.ToInt32(ddlWS.SelectedValue), 0);
            gdvWS.DataSource = ds.Tables[0];
            gdvWS.DataBind();
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            int UserID = Convert.ToInt32( Convert.ToInt32(Session["UserId"]).ToString());
            objAtt = new AttendanceDAC();
            int i = 0;
            try
            {
                if(ddlWS.SelectedIndex == 0 && ddlHead.SelectedIndex == 0 && ddlDept.SelectedIndex == 0)
                {
                   // AlertMsg.MsgBox(Page, "Please Fill Mandatory Field");
                    lblStatus.Text = "Please Fill Mandatory Field";
                     lblStatus.ForeColor = System.Drawing.Color.Red;
                }
                else { 
                if (btnSubmit.Text.ToLower().Trim() == "assign" || btnSubmit.Text.ToLower().Trim() == "update")
                {

                    i = objAtt.AddDeptHead(Convert.ToInt32(ddlWS.SelectedValue), Convert.ToInt32(ddlDept.SelectedValue), Convert.ToInt32(ddlHead.SelectedValue), UserID);

                    BindGrid();
                    GetWorkSites(0);
                    if (btnSubmit.Text.ToLower().Trim() == "assign")
                    {
                        ClientScript.RegisterStartupScript(typeof(System.String), "str", "<script type='text/javascript'>alert('Assigned')</script>");

                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(typeof(System.String), "str", "<script type='text/javascript'>alert('Updated')</script>");

                    }

                }
                txtdept.Text = txtSearchEmp.Text = txtSearchWorksite.Text = "";
                }
            }
            catch (Exception ex)
            {
              //  AlertMsg.MsgBox(Page, "Sorry for the inconvinience. Try again.\nError:" + ex.Message.ToString(),AlertMsg.MessageType.Error);

                lblStatus.Text = "Sorry for the inconvinience. Try again.\nError:" + ex.Message;
                     lblStatus.ForeColor = System.Drawing.Color.Red;

            }


        }
        protected void ddlDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            int deptid = Convert.ToInt32(ddlDept.SelectedItem.Value);
            BindHeads(0);
        }

        protected void gdvWS_Sorting1(object sender, GridViewSortEventArgs e)
        {

            try
            {
                //SortGrid Object from ViewState
                objSort = (GridSort)ViewState["Sort"];

                //Get dataSet from ViewState
                DataSet dsHeads = (DataSet)ViewState["DataSet"];
                DataView dvHeads = dsHeads.Tables[0].DefaultView;
                //Get SortExpresssion from sordGrid Object
                dvHeads.Sort = objSort.GetSortExpression(e.SortExpression);
                gdvWS.DataSource = dvHeads;
                gdvWS.DataBind();
                //reset SortGrid object which is in ViewState
                ViewState["Sort"] = objSort;

            }
            catch (Exception ex)
            {

            }
        } 
        
        public DataSet ExecuteQuery(string QueryString)
        {
            string ConnectingString = null;
            ConnectingString = ConfigurationManager.AppSettings["strConn"].ToString();
            SqlConnection conn = new SqlConnection(ConnectingString);
            SqlDataAdapter DBAdapter = null;
            DataSet ResultDataSet = new DataSet();
            try
            {
                DBAdapter = new SqlDataAdapter(QueryString, conn);
                DBAdapter.Fill(ResultDataSet);
                conn.Close();
            }
            catch (Exception ex)
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                Response.Write("Unable to Connect to the DataBase");
            }
            return ResultDataSet;
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
           // DeptID = 0;
            DataSet ds = AttendanceDAC.GetEmployee(prefixText, SearchCompanyID, Empdeptid);
            return ConvertStingArray(ds);// txtItems.ToArray();
        }

        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionListDep(string prefixText, int count, string contextKey)
        {
            DataSet ds = AttendanceDAC.GetSearchDesiginationFilter(prefixText, SearchCompanyID, Empdeptid);
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

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindGrid();
        }
    }
}