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
using AECLOGIC.HMS.BLL;
using AECLOGIC.ERP.COMMON;
using System.Data.SqlClient;
using Aeclogic.Common.DAL;

namespace AECLOGIC.ERP.HMS
{
    public partial class CMS_AddEditWorksite : AECLOGIC.ERP.COMMON.WebFormMaster
    {
        int mid = 0;
        bool viewall, Editable;
        string menuname;
        string menuid;
        char status;
        public static bool StatusSearch;
        private GridSort objSort;
        DataSet dsWorkSites; 
        string ModuleId = System.Configuration.ConfigurationManager.AppSettings["ModuleId"];
        int CompanyID = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["CompanyID"]);
        HRCommon objHrCommon =new HRCommon();
        protected override void OnInit(EventArgs e)
        {
            ModuleID = 1;
            base.OnInit(e);
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
            EmployeBind(objHrCommon);
        }
        void EmployeBind(HRCommon objHrCommon)
        {

            try
            {
               
                    
                objHrCommon.PageSize = EmpListPaging.ShowRows;
                objHrCommon.CurrentPage = EmpListPaging.CurrentPage;
                bool Status = false;
                if (rbShowActive.Checked)
                {
                    Status = true;
                }
              
                DataSet ds = AttendanceDAC.GetWorkSiteByCmpIDStatusByPaging(objHrCommon, Convert.ToInt32(Session["CompanyID"]), Status, txtSWorksitename.Text);
                if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0)
                {
                    gdvWS.DataSource = ds;
                    gdvWS.DataBind();
                }
                else
                {
                    gdvWS.DataSource = null;
                    gdvWS.DataBind();
                }
                EmpListPaging.Bind(objHrCommon.CurrentPage, objHrCommon.TotalPages, objHrCommon.NoofRecords, objHrCommon.PageSize);

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public string GetText()
        {

            if (rbShowActive.Checked)
            {
                return "Delete";
            }
            else
            {
                return "Activate";
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
           
            if (rbShowActive.Checked)
                status = '1';
            else
                status = '0';
            if (rbShowActive.Checked)
                StatusSearch = true;
            else
                StatusSearch = false;
            if (!IsPostBack)
            {
                GetParentMenuId();
                ddlState.Items.Insert(0, new ListItem("---Select---", "0"));
                objSort = new GridSort();
                ViewState["Sort"] = objSort;
                EmployeBind(objHrCommon);
              
                BindCountry();
                //mainview.ActiveViewIndex = 1;
                EditView.Visible = true;
                Newvieew.Visible = false;
                if (Request.QueryString.Count > 0)
                {
                    int id = Convert.ToInt32(Request.QueryString["id"].ToString());
                    if (id == 1)
                    {
                        Newvieew.Visible = true;
                        EditView.Visible = false;
                    }
                }

            }
        }

        private void BindCountry()
        {
            FIllObject.FillDropDown(ref ddlCountry, "HMS_Get_Country");
        }
        public int GetParentMenuId()
        {
            
            string URL = Request.Url.Segments[Request.Url.Segments.Length - 1] + Request.Url.Query;
            int RoleId = Convert.ToInt32(Session["RoleId"]);
            int ModuleId = ModuleID;;
            DataSet ds = AttendanceDAC.GetAllowed(RoleId, ModuleId, URL);
            int MenuId = 0;
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                MenuId = Convert.ToInt32(ds.Tables[0].Rows[0]["Under"]);
                ViewState["ViewAll"] = (bool)ds.Tables[0].Rows[0]["ViewAll"];
                //lnkState.Visible = Convert.ToBoolean(ds.Tables[0].Rows[0]["Editable"]);
              //  btnSubmit.Enabled = Convert.ToBoolean(ds.Tables[0].Rows[0]["Editable"]);
               // Editable = Convert.ToBoolean(ds.Tables[0].Rows[0]["Editable"]);
               // viewall = (bool)ViewState["ViewAll"];
              //  btnSubmit.Enabled = Editable;
            }
            return MenuId;
        }
        private void BindGrid()
        {

            dsWorkSites = new DataSet();
            dsWorkSites = AttendanceDAC.GetWorkSite(0, status, Convert.ToInt32(Session["CompanyID"]));
            ViewState["DataSet"] = dsWorkSites;
            gdvWS.DataSource = dsWorkSites;
            gdvWS.DataBind();
           // gdvWS_Sorting(gdvWS, new GridViewSortEventArgs("ResourceName", SortDirection.Ascending));

        }
        public void BindStates(int CountryId)
        {
            try
            {
                SqlParameter[] parms = new SqlParameter[1];
                parms[0] = new SqlParameter("@CountryId", CountryId);
                FIllObject.FillDropDown(ref ddlState, "HMS_GETStatesByCountry", parms);
            }
            catch (Exception ex)
            {

                throw ex;
            }
           
           
        }
        protected void gdvWS_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{
            //    LinkButton lnkEdt = (LinkButton)e.Row.FindControl("lnkEdit");
            //    LinkButton lnkDel = (LinkButton)e.Row.FindControl("lnkDel");

            //    lnkEdt.Enabled = lnkDel.Enabled = Editable;
            //}
        }
        protected void gdvWS_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Edt")
            {
                int siteid = Convert.ToInt32(e.CommandArgument);
                dsWorkSites = new DataSet();
                if (rbShowActive.Checked)
                    status = '1';
                else
                    status = '0';
                dsWorkSites = SQLDBUtil.ExecuteDataset("HR_GetWorkSite_new", new SqlParameter[] 
                { new SqlParameter("@WSID", siteid),
                    new SqlParameter("@WSStatus", status), 
                    new SqlParameter("@CompanyID", Convert.ToInt32(Session["CompanyID"])) 
                });
               // dsWorkSites = AttendanceDAC.GetWorkSite(siteid, Convert.ToChar(status), Convert.ToInt32(Session["CompanyID"]));
                if (dsWorkSites != null && dsWorkSites.Tables.Count != 0 && dsWorkSites.Tables[0].Rows.Count != 0)
                {
                   
                    txtWS.Text = dsWorkSites.Tables[0].Rows[0]["Site_Name"].ToString();
                    int stateid= Convert.ToInt32(dsWorkSites.Tables[0].Rows[0]["STATEID"]);

                   // DataSet dsC = AttendanceDAC.HMS_GetCountryByStateID(stateid);
                    if (dsWorkSites.Tables[0].Rows.Count > 0)
                        ddlCountry.SelectedValue = dsWorkSites.Tables[0].Rows[0]["CountryID"].ToString();
                    else
                        ddlCountry.SelectedValue = "0";
                    if (dsWorkSites.Tables[1].Rows.Count > 0)
                    {
                        ddlState.DataSource = dsWorkSites.Tables[1];
                        ddlState.DataTextField = dsWorkSites.Tables[1].Columns[1].ToString();
                        ddlState.DataValueField = dsWorkSites.Tables[1].Columns[0].ToString();
                        ddlState.DataBind();
                        ddlState.Items.Insert(0, new ListItem("Select", "0"));
                    }
                    else
                        BindStates(Convert.ToInt32(ddlCountry.SelectedValue));
                    try { ddlState.SelectedValue = stateid.ToString(); }
                    catch { ddlState.SelectedValue = "0"; };
                    txtaddress.Text = dsWorkSites.Tables[0].Rows[0]["Site_Address"].ToString().Replace("<br/>", "\n");
                    if (dsWorkSites.Tables[0].Rows[0]["WSStatus"].ToString() == "1")
                        chkStatus.Checked = true;
                    else
                        chkStatus.Checked = false;
                    status = Convert.ToChar(dsWorkSites.Tables[0].Rows[0]["WSStatus"]);
                    hdn.Value = e.CommandArgument.ToString();
                    btnSubmit.Text = "Update";
                    // EmployeBind(objHrCommon);
                    EditView.Visible = false;
                    Newvieew.Visible = true;
                    
                }
               
            }
            else
            {
                bool Status = true;
                if (rbShowActive.Checked)
                    Status = false;
                int siteid = Convert.ToInt32(e.CommandArgument);
                if (siteid != 1)
                {
                    try
                    {
                        AttendanceDAC.HR_Upd_WorkSiteStatus(siteid, Status);
                        EmployeBind(objHrCommon);
                        AlertMsg.MsgBox(Page, "Done..!");
                    }
                    catch (Exception Inactive)
                    {
                        AlertMsg.MsgBox(Page, Inactive.Message.ToString(),AlertMsg.MessageType.Error);
                    }

                }
                else
                {
                    AlertMsg.MsgBox(Page, "You are NOT assigned the worksite to Edit/Delete");
                    return;
                }
            }
        }

       
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            
             if(!String.IsNullOrEmpty( Convert.ToInt32(Session["UserId"]).ToString()))
            {
                int EmpID =  Convert.ToInt32(Session["UserId"]);
            }
            try
            {
                status='0';
                if (chkStatus.Checked == true)
                {
                    status = '1';
                }
                else
                {
                    status = '0';
                }
                int n = 0;
                if (btnSubmit.Text.ToLower().Trim() == "save")
                {
                    int STATEID = Convert.ToInt32(ddlState.SelectedItem.Value);

                    int CompanyID = Convert.ToInt32(Session["CompanyID"]);
                    n = AttendanceDAC.AddWorkSite(txtWS.Text.Trim(), txtaddress.Text.Trim().Replace("\n", "<br/>"), status, STATEID, CompanyID);
                    if (n == 1)
                    {
                        string strScript = "<script language='javascript' type='text/javascript' >alert('Worksite Added!'); </script>";
                        Page.RegisterStartupScript("suc", strScript);
                        txtWS.Text = string.Empty;
                        txtaddress.Text = string.Empty;
                    }
                    else
                    {
                        if (n == 0)
                        {
                            string strScript = "<script language='javascript' type='text/javascript' >alert('Worksite Addtion Failed.Worksite Already Exists'); </script>";
                            Page.RegisterStartupScript("fail", strScript);
                        }
                    }

                }
                else
                {
                    if (btnSubmit.Text.ToLower().Trim() == "update")
                    {
                        int CompanyID = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["CompanyID"].ToString());
                        n = AttendanceDAC.UpdateWorkSite(Convert.ToInt32(hdn.Value), txtWS.Text, status, txtaddress.Text.Replace("\n", "<br/>"), Convert.ToInt32(ddlState.SelectedValue), CompanyID);
                        if (n == 1)
                        {
                            string strScript = "<script language='javascript' type='text/javascript' >alert('Worksite Updated! '); </script>";
                            Page.RegisterStartupScript("suc", strScript);
                            txtWS.Text = "";
                            txtaddress.Text = "";
                        }
                        else
                        {
                            if (n == 0)
                            {
                                string strScript = "<script language='javascript' type='text/javascript' >alert('Worksite Updation Failed.Worksite with this name already exists'); </script>";
                                Page.RegisterStartupScript("fail1", strScript);
                            }

                        }

                    }
                }
                if (rbShowActive.Checked)
                    status = '1';
                else
                    status = '0';
                EmployeBind(objHrCommon);
                EditView.Visible = true;
                Newvieew.Visible = false;
            }
            catch (Exception ex)
            {

                AlertMsg.MsgBox(Page, "Sorry for the inconvinience. Try again.\nError:" + ex.Message.ToString(),AlertMsg.MessageType.Error);
            }
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            EmployeBind(objHrCommon);
        }



        protected void rbShow_CheckedChanged(object sender, EventArgs e)
        {
            if (rbShowActive.Checked)
                StatusSearch = true;
            else
                StatusSearch = false;
            EmpListPaging.CurrentPage = 1;
            EmployeBind(objHrCommon);
        }

        protected void gdvWS_Sorting(object sender, GridViewSortEventArgs e)
        {



            //try
            //{
            //    //SortGrid Object from ViewState
            //    objSort = (GridSort)ViewState["Sort"];
            //    //Get dataSet from ViewState
            //    dsWorkSites = (DataSet)ViewState["DataSet"];
            //    DataView dvWorkSites = dsWorkSites.Tables[0].DefaultView;
            //    //Get SortExpresssion from sordGrid Object
            //    dvWorkSites.Sort = objSort.GetSortExpression(e.SortExpression);
            //    gdvWS.DataSource = dvWorkSites;
            //    gdvWS.DataBind();
            //    //reset SortGrid object which is in ViewState
            //    ViewState["Sort"] = objSort;
            //}
            //catch (Exception ex)
            //{
            //}
        }

        protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            int CountryId=Convert.ToInt32(ddlCountry.SelectedValue);
            BindStates(CountryId);
        }


        [System.Web.Services.WebMethodAttribute(),System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetSearchVendorDetails(string prefixText, int count, string contextKey)
        {
            return ConvertStingArray(AttendanceDAC.GetSearchWorksite(prefixText,StatusSearch));
        }
        public static string[] ConvertStingArray(DataSet ds)
        {
            string[] rtval = Array.ConvertAll(ds.Tables[0].Select(), new Converter<DataRow, string>(DataRowToDouble));
            return rtval;
        }


        public static string DataRowToDouble(DataRow dr)
        {
            return dr["Site_Name"].ToString();
        }

       

        protected void btnFresh_Click(object sender, EventArgs e)
        {
            int CountryId = Convert.ToInt32(ddlCountry.SelectedValue);
            BindStates(CountryId);
        }
    }

}