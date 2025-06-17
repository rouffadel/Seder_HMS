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
using System.Collections.Generic;
using System.Data.SqlClient;

namespace AECLOGIC.ERP.HMS
{
    public partial class SMSEmpTransfers : AECLOGIC.ERP.COMMON.WebFormMaster
    {
        HRCommon objHrCommon = new HRCommon();
        AttendanceDAC objRights = new AttendanceDAC();


        int mid = 0;
        bool viewall;
        string menuname;
        string menuid;
        static int CompanyID = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["CompanyID"].ToString());
         protected override void OnInit(EventArgs e)
        {
            ModuleID = 1;
            base.OnInit(e);
        
            // btnSave.Attributes.Add("onclick", "javascript:return ValidateSave('" + txtCategoryName.ClientID + "');");
            SMSEMPTransferPaging.FirstClick += new Paging.PageFirst(SMSEMPTransferPaging_FirstClick);
            SMSEMPTransferPaging.PreviousClick += new Paging.PagePrevious(SMSEMPTransferPaging_FirstClick);
            SMSEMPTransferPaging.NextClick += new Paging.PageNext(SMSEMPTransferPaging_FirstClick);
            SMSEMPTransferPaging.LastClick += new Paging.PageLast(SMSEMPTransferPaging_FirstClick);
            SMSEMPTransferPaging.ChangeClick += new Paging.PageChange(SMSEMPTransferPaging_FirstClick);
            SMSEMPTransferPaging.ShowRowsClick += new Paging.ShowRowsChange(SMSEMPTransferPaging_ShowRowsClick);
            SMSEMPTransferPaging.CurrentPage = 1;
        }
        void SMSEMPTransferPaging_ShowRowsClick(object sender, EventArgs e)
        {
            SMSEMPTransferPaging.CurrentPage = 1;
            BindPager();
        }
        void SMSEMPTransferPaging_FirstClick(object sender, EventArgs e)
        {
            if (hdnSearchChange.Value == "1")
                SMSEMPTransferPaging.CurrentPage = 1;
            BindPager();
            hdnSearchChange.Value = "0";
        }
        void BindPager()
        {

            objHrCommon.PageSize = SMSEMPTransferPaging.CurrentPage;
            objHrCommon.CurrentPage = SMSEMPTransferPaging.ShowRows;
            BindEmpDetails(objHrCommon);

        }
        protected void Page_Load(object sender, EventArgs e)
        {
            Ajax.Utility.RegisterTypeForAjax(typeof(AjaxDAL));
            topmenu.MenuId = GetParentMenuId();
            topmenu.ModuleId = ModuleID;;
            topmenu.RoleID = Convert.ToInt32(Session["RoleId"].ToString());
            topmenu.SelectedMenu = Convert.ToInt32(mid.ToString());
            topmenu.DataBind();
            Session["menuname"] = menuname;
            Session["menuid"] = menuid;
            Session["MId"] = mid;


            if (!IsPostBack)
            {

                ViewState["EmpID"] = "";
                BindWS();
                BindDept();
                //BindWorkSites();
                //BindDepartments();
                BindPager();
                //BindEmpDetails();
            }
        }
        public int GetParentMenuId()
        {
            string URL = Request.Url.Segments[Request.Url.Segments.Length - 1] + Request.Url.Query;
            int RoleId = Convert.ToInt32(Session["RoleId"].ToString());
            int ModuleId = ModuleID;;

            DataSet ds = new DataSet();

            ds = AttendanceDAC.GetAllowed(RoleId, ModuleId, URL);
            int MenuId = 0;
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                MenuId = Convert.ToInt32(ds.Tables[0].Rows[0]["Under"]);
                ViewState["Editable"] = (bool)ds.Tables[0].Rows[0]["Editable"];
                ViewState["ViewAll"] = (bool)ds.Tables[0].Rows[0]["ViewAll"];
                viewall = (bool)ViewState["ViewAll"];
                menuname = ds.Tables[0].Rows[0]["menuname"].ToString();
                menuid = MenuId.ToString();
                mid = Convert.ToInt32(ds.Tables[0].Rows[0]["MenuId"].ToString());
            }
            return MenuId;
        }

        #region DDL

        void BindWS()
        {
            DataSet ds = new DataSet();
            ds = AttendanceDAC.GetWorkSite(0, '1', Convert.ToInt32(Session["CompanyID"]));
            if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0)
            {
                ddlWS.DataSource = ds.Tables[0];
                ddlWS.DataTextField = "Site_Name";
                ddlWS.DataValueField = "Site_ID";
                ddlWS.DataBind();

                ddlworksites.DataSource = ds.Tables[0];
                ddlworksites.DataTextField = "Site_Name";
                ddlworksites.DataValueField = "Site_ID";
                ddlworksites.DataBind();
            }
            ddlWS.Items.Insert(0, new ListItem("---Select---", "0"));
            ddlworksites.Items.Insert(0, new ListItem("---Select---", "0"));


        }
        protected void GetWork(object sender, EventArgs e)
        {
		

			 SqlParameter[] par = new SqlParameter[1];
            par[0] = new SqlParameter("@Search", txtSearchWorksite.Text);
            FIllObject.FillDropDown(ref ddlworksites, "HR_GetWorkSite_By_LoanAdv_googlesearch", par);
            ListItem itmSelected = ddlworksites.Items.FindByText(txtSearchWorksite.Text);
            if (itmSelected != null)
            {
                ddlworksites.SelectedItem.Selected = false;
                itmSelected.Selected = true;
            }
            ddlworksites_SelectedIndexChanged(sender, e);
           // txtdepat.Focus();
        }
        protected void ddlworksites_SelectedIndexChanged(object sender, EventArgs e)
        {
           // BindDeparmetBySite(Convert.ToInt32(ddlworksites.SelectedValue));
        }
         protected void Getdepat(object sender, EventArgs e)
        {

         }


        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionList(string prefixText, int count, string contextKey)
        {
            //DataSet ds = AttendanceDAC.GetGoogleABCSearchWorkSite(prefixText, SearchCompanyID);
            //return ConvertStingArray(ds);// txtItems.ToArray();
            DataSet ds = AttendanceDAC.GetWorkSite_EmpPenalties(prefixText, CompanyID);
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
        public static string[] GetCompletionList_depat(string prefixText, int count, string contextKey)
        {
            //DataSet ds = AttendanceDAC.GetGoogleABCSearchWorkSite(prefixText, SearchCompanyID);
            //return ConvertStingArray(ds);// txtItems.ToArray();
            DataSet ds = AttendanceDAC.GetDepartmentListgooglesearch(prefixText);
            DataTable dt = ds.Tables[0];
            List<string> items = new List<string>(count);
            var rtval = new Dictionary<string, string>();
            foreach (DataRow row in dt.Rows)
            {
                string str = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(row["NAME"].ToString(), row["ID"].ToString());
                items.Add(str);
            }

            return items.ToArray(); ;// txtItems.ToArray();
        }









        void BindDept()
        {
            DataSet ds = new DataSet();
            ds = (DataSet)objRights.GetDaprtmentList();
            if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0)
            {
                ddlDept.DataTextField = "Deptname";
                ddlDept.DataValueField = "DepartmentUId";
                //ddlDept.DataTextField = "DepartmentName";
                ddlDept.DataSource = ds;
                ddlDept.DataBind();
                ddlDept.Items.Insert(0, new ListItem("---Select---", "0"));

                ddldepartments.DataValueField = "DepartmentUId";
                ddldepartments.DataTextField = "Deptname";
                //ddldepartments.DataTextField = "DepartmentName";
                ddldepartments.DataSource = ds;
                ddldepartments.DataBind();
                ddldepartments.Items.Insert(0, new ListItem("---Select---", "0"));
            }
        }
        void BindEmp()
        {
            int WSID = Convert.ToInt32(ddlWS.SelectedValue);
            int DeptID = Convert.ToInt32(ddlDept.SelectedValue);
            int EmpID = Convert.ToInt32(ViewState["EmpID"]);
            DataSet dsTransto = new DataSet();

            dsTransto = objRights.GetDeptHeadsForTransfer(EmpID, WSID, DeptID);
            if (dsTransto.Tables.Count > 0)
            {
                ddlReportingTo.DataSource = dsTransto;
                ddlReportingTo.DataTextField = "name";
                ddlReportingTo.DataValueField = "HeadId";
                ddlReportingTo.DataBind();
                ddlReportingTo.Items.Insert(0, new ListItem("---Select---", "0"));
            }
        }

        #endregion DDL
        public void BindEmpDetails(HRCommon objHrCommon)
        {
            try
            {
                objHrCommon.PageSize = SMSEMPTransferPaging.ShowRows;
                objHrCommon.CurrentPage = SMSEMPTransferPaging.CurrentPage;
                dvgrd.Visible = true;
                //comented by nadeem .using textbox instead of ddl
               // int? SiteID = null;
                //if (ddlworksites.SelectedIndex != 0)
                //    SiteID = Convert.ToInt32(ddlworksites.SelectedItem.Value);
                int? SiteID = null;
                if (txtSearchWorksite.Text.Trim() != "")
                {
                    SiteID = Convert.ToInt32(ddlworksites_hid.Value == "" ? "0" : ddlworksites_hid.Value);

                }
                int? DeptID = null;
                if (txtSearchWorksite.Text.Trim() != "")
                {
                    DeptID = Convert.ToInt32(ddldepartments_hid.Value == "" ? "0" : ddldepartments_hid.Value);

                }

                //comented by nadeem .using textbox instead of ddl
                //int? DeptID = null;
                //if (ddldepartments.SelectedIndex != 0)
                //    DeptID = Convert.ToInt32(ddldepartments.SelectedItem.Value);
                int? EmpID = null;
                if (txtEmpID.Text != "")
                    EmpID = int.Parse(txtEmpID.Text);
                string EmpName = txtEmpname.Text;
                DataSet ds = new DataSet();
                ds = AttendanceDAC.GetSMSTransferEmpDetails(objHrCommon, SiteID, DeptID, EmpID, EmpName);
                if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0)
                {
                    grdSMSEmpTrnsfers.DataSource = ds;

                    SMSEMPTransferPaging.Bind(objHrCommon.CurrentPage, objHrCommon.TotalPages, objHrCommon.NoofRecords, objHrCommon.PageSize);

                }
                else
                {
                    grdSMSEmpTrnsfers.EmptyDataText = "No Records Found";
                    SMSEMPTransferPaging.Visible = false;
                }
                grdSMSEmpTrnsfers.DataBind();


            }
            catch (Exception e)
            {
                throw e;
            }

        }


        #region RowCommand
        protected void grdSMSEmpTrnsfers_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            GridViewRow gvrTrandID = (GridViewRow)(((Control)e.CommandSource).NamingContainer);
            Label lblTransID = (Label)gvrTrandID.FindControl("lblTransID");
            int TranID = int.Parse(lblTransID.Text);
            if (e.CommandName == "UPD")
            {
                dvgrd.Visible = false;
                dvTransto.Visible = true;
                ddlReportingTo.Items.Insert(0, new ListItem("---Select---", "0"));
                GridViewRow gvr = (GridViewRow)(((Control)e.CommandSource).NamingContainer);
                Label lblEmpID = (Label)gvr.FindControl("lblEmpID");
                int EmpID = int.Parse(lblEmpID.Text);
                ViewState["EmpID"] = EmpID;
                GetEmpDetails();
                btnTransfer.Attributes.Add("onclick", "javascript:return HeadsAssign( this,'" + ddlWS.ClientID + "','" + ddlDept.ClientID + "','" + ddlReportingTo.ClientID + "','" +  Convert.ToInt32(Session["UserId"]).ToString() + "','" + EmpID + "','0','" + TranID + "');");
            }
            if (e.CommandName == "Del")
            {
                AttendanceDAC.UpdSMSEMPTransferStatus(TranID);
                BindPager();
            }

        }
        void GetEmpDetails()
        {
            int EmpID = Convert.ToInt32(ViewState["EmpID"]);
            DataSet ds = new DataSet();
            ds = objRights.GetEmpDetailsBYEmpID(EmpID);
            lblEmployeeName.Text = ds.Tables[0].Rows[0]["Name"].ToString();
            lblEmpCurrentWorksite.Text = ds.Tables[0].Rows[0]["WorkSite"].ToString();
            lblEmpCurrentDept.Text = ds.Tables[0].Rows[0]["Department"].ToString();
            lblRemark.Text = ds.Tables[0].Rows[0]["Remark"].ToString();



        }
        #endregion RowCommand

        #region RowdataBound

        public DataSet RetHeads = new DataSet();
        public DataSet BindHeads()
        {
            RetHeads = (DataSet)ViewState["RetHeads"];
            return RetHeads;
        }
        public DataSet RetDeptHeads = new DataSet();
        public static ArrayList alDeptHeads = new ArrayList();

        protected void grdSMSEmpTrnsfers_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            foreach (GridViewRow gvr in grdSMSEmpTrnsfers.Rows)
            {
                LinkButton lnkConsol = (LinkButton)gvr.Cells[4].FindControl("lnkConsolidate");
                //lnkConsol.Enabled = Convert.ToBoolean(ViewState["Editable"]);
            }


        }

        #endregion RowdataBound


        protected void ddlReportingTo_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
        protected void ddlDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindEmp();
        }
        protected void btnTransfer_Click(object sender, EventArgs e)
        {
            //if (ddlReportingTo.SelectedIndex > 0 && ddlWS.SelectedIndex > 0 )
            //{
            AlertMsg.MsgBox(Page, "Transfered");
            dvgrd.Visible = true;
            dvTransto.Visible = false;
            BindPager();
            //}
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            dvTransto.Visible = false;
            BindPager();
        }
    }
}