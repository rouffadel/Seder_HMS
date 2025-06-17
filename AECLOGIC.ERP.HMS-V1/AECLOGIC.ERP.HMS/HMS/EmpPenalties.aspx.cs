using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AECLOGIC.HMS.BLL;
using System.Data;
using System.Data.SqlClient;
using Aeclogic.Common.DAL;
using System.IO;
using System.Configuration;
using System.Collections;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using AECLOGIC.ERP.COMMON;
using AECLOGIC.ERP.HMS;

namespace AECLOGIC.ERP.HMSV1
{
    public partial class EmpPenaltiesV1 : AECLOGIC.ERP.COMMON.WebFormMaster
    {
        int Id = 1;
        int mid = 0;
        bool viewall;
        string menuname;
        string menuid;
        static int SearchCompanyID;
        static int WorkSiteID;
        static int DeptSearch;
        String MyString;
        string extension;
        bool Editable;

        int CompanyID = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["CompanyID"].ToString());
        HRCommon objHrCommon = new HRCommon();
        protected override void OnInit(EventArgs e)
        {
            ModuleID = 1;
            base.OnInit(e);

            #region Approvedpaging
            EmpReimbursementAprovedPaging.FirstClick += new Paging.PageFirst(EmpReimbursementAprovedPaging_FirstClick);
            EmpReimbursementAprovedPaging.PreviousClick += new Paging.PagePrevious(EmpReimbursementAprovedPaging_FirstClick);
            EmpReimbursementAprovedPaging.NextClick += new Paging.PageNext(EmpReimbursementAprovedPaging_FirstClick);
            EmpReimbursementAprovedPaging.LastClick += new Paging.PageLast(EmpReimbursementAprovedPaging_FirstClick);
            EmpReimbursementAprovedPaging.ChangeClick += new Paging.PageChange(EmpReimbursementAprovedPaging_FirstClick);
            EmpReimbursementAprovedPaging.ShowRowsClick += new Paging.ShowRowsChange(EmpReimbursementAprovedPaging_ShowRowsClick);
            EmpReimbursementAprovedPaging.CurrentPage = 1;
            #endregion Approvedpaging

            #region PendingOrRejected
            EmpReimbursementPendingRejPaging.FirstClick += new Paging.PageFirst(EmpReimbursementPendingRejPaging_FirstClick);
            EmpReimbursementPendingRejPaging.PreviousClick += new Paging.PagePrevious(EmpReimbursementPendingRejPaging_FirstClick);
            EmpReimbursementPendingRejPaging.NextClick += new Paging.PageNext(EmpReimbursementPendingRejPaging_FirstClick);
            EmpReimbursementPendingRejPaging.LastClick += new Paging.PageLast(EmpReimbursementPendingRejPaging_FirstClick);
            EmpReimbursementPendingRejPaging.ChangeClick += new Paging.PageChange(EmpReimbursementPendingRejPaging_FirstClick);
            EmpReimbursementPendingRejPaging.ShowRowsClick += new Paging.ShowRowsChange(EmpReimbursementPendingRejPaging_ShowRowsClick);
            EmpReimbursementPendingRejPaging.CurrentPage = 1;
            #endregion PendingOrRejected

            #region TransferdAmount
            EmpReimbursementTransferdPaging.FirstClick += new Paging.PageFirst(EmpReimbursementTransferdPaging_FirstClick);
            EmpReimbursementTransferdPaging.PreviousClick += new Paging.PagePrevious(EmpReimbursementTransferdPaging_FirstClick);
            EmpReimbursementTransferdPaging.NextClick += new Paging.PageNext(EmpReimbursementTransferdPaging_FirstClick);
            EmpReimbursementTransferdPaging.LastClick += new Paging.PageLast(EmpReimbursementTransferdPaging_FirstClick);
            EmpReimbursementTransferdPaging.ChangeClick += new Paging.PageChange(EmpReimbursementTransferdPaging_FirstClick);
            EmpReimbursementTransferdPaging.ShowRowsClick += new Paging.ShowRowsChange(EmpReimbursementTransferdPaging_ShowRowsClick);
            EmpReimbursementTransferdPaging.CurrentPage = 1;
            #endregion TransferdAmount
        }


        #region Approved
        void EmpReimbursementAprovedPaging_ShowRowsClick(object sender, EventArgs e)
        {
            EmpReimbursementAprovedPaging.CurrentPage = 1;
            BindPager();
        }
        void EmpReimbursementAprovedPaging_FirstClick(object sender, EventArgs e)
        {
            if (hdnSearchChangeAproved.Value == "1")
                EmpReimbursementAprovedPaging.CurrentPage = 1;
            BindPager();
            hdnSearchChangeAproved.Value = "0";
        }
        void BindPager()
        {

            objHrCommon.PageSize = EmpReimbursementAprovedPaging.CurrentPage;
            objHrCommon.CurrentPage = EmpReimbursementAprovedPaging.ShowRows;
            //EmployeeApproved(objHrCommon);
            bindpager_Approved();


        }

        #endregion Approved

        #region PendingRej

        void EmpReimbursementPendingRejPaging_ShowRowsClick(object sender, EventArgs e)
        {
            EmpReimbursementPendingRejPaging.CurrentPage = 1;
            if (Convert.ToInt32(Request.QueryString["key"]) == 2)
                BindPagerPending();
            if (Convert.ToInt32(Request.QueryString["key"]) == 3)
            {
                BindPagerPending();
            }
            if (Convert.ToInt32(Request.QueryString["key"]) == 4)       //A/C Posted
            {
                BindPagerPending();
            }
        }
        void EmpReimbursementPendingRejPaging_FirstClick(object sender, EventArgs e)
        {
            if (hdnSearchChangePendingRej.Value == "1")
                EmpReimbursementPendingRejPaging.CurrentPage = 1;
            if (Convert.ToInt32(Request.QueryString["key"]) == 2)
                BindPagerPending();
            if (Convert.ToInt32(Request.QueryString["key"]) == 3)
            {
                BindPagerPending();                                 //rejected
            }
            if (Convert.ToInt32(Request.QueryString["key"]) == 4)
            {
                BindPagerPending();                                 //A/C Posted
            }
            hdnSearchChangePendingRej.Value = "0";
        }
        void BindPagerPending()
        {
            int EmpID = 0;
            objHrCommon.PageSize = EmpReimbursementPendingRejPaging.CurrentPage;
            objHrCommon.CurrentPage = EmpReimbursementPendingRejPaging.ShowRows;

            if (Convert.ToInt32(Request.QueryString["key"]) == 2)       //Pending
            {
                if (ddlFilterEmp.SelectedIndex == -1)
                    EmployeeNotApproved(objHrCommon);
                if (ddlFilterEmp.SelectedIndex != -1)
                {
                    if (txtFilterEmpID.Text == "")
                    {
                        EmpID = Convert.ToInt32(ddlFilterEmp.SelectedValue);
                        EmployeeNotApproved(objHrCommon, EmpID);
                    }
                }
                if (txtFilterEmpID.Text != "")
                {
                    EmpID = Convert.ToInt32(txtFilterEmpID.Text);
                    EmployeeNotApproved(objHrCommon, EmpID);
                }
            }
            if (Convert.ToInt32(Request.QueryString["key"]) == 3)       //rejected
            {
                if (ddlFilterEmp.SelectedIndex == -1 || ddlFilterEmp.SelectedIndex == 0)
                    EmployeeReimRejected(objHrCommon);
                if (ddlFilterEmp.SelectedIndex != -1)
                {
                    if (txtFilterEmpID.Text == "")
                    {
                        EmpID = Convert.ToInt32(ddlFilterEmp.SelectedValue);
                        EmployeeReimRejectedByEmpID(objHrCommon, EmpID);

                    }
                }
                if (txtFilterEmpID.Text != "")
                {
                    EmpID = Convert.ToInt32(txtFilterEmpID.Text);
                    EmployeeReimRejectedByEmpID(objHrCommon, EmpID);
                }
            }
            if (Convert.ToInt32(Request.QueryString["key"]) == 4)       //A/C Posted
            {
                //if (ddlFilterEmp.SelectedIndex == -1)
                //EmployeeNotApproved(objHrCommon);
                if (ddlFilterEmp.SelectedIndex != -1)
                {
                    if (txtFilterEmpID.Text == "")
                    {
                        EmpID = Convert.ToInt32(ddlFilterEmp.SelectedValue);
                        EmployeeNotApproved(objHrCommon, EmpID);
                    }
                }
                if (txtFilterEmpID.Text != "")
                {
                    EmpID = Convert.ToInt32(txtFilterEmpID.Text);
                    EmployeeNotApproved(objHrCommon, EmpID);
                }
            }

        }



        #endregion PendingRej

        #region Transferd
        void EmpReimbursementTransferdPaging_ShowRowsClick(object sender, EventArgs e)
        {
            EmpReimbursementTransferdPaging.CurrentPage = 1;
            BindPagerTransAmt();
        }
        void EmpReimbursementTransferdPaging_FirstClick(object sender, EventArgs e)
        {
            if (hdnSearchChangeTransAmt.Value == "1")
                EmpReimbursementTransferdPaging.CurrentPage = 1;
            BindPagerTransAmt();
            hdnSearchChangeTransAmt.Value = "0";
        }
        void BindPagerTransAmt()
        {

            objHrCommon.PageSize = EmpReimbursementTransferdPaging.CurrentPage;
            objHrCommon.CurrentPage = EmpReimbursementTransferdPaging.ShowRows;
            //if (ddlFilterEmp.SelectedIndex == -1)
            EmployeeReimTransferdAmt(objHrCommon);
            if (ddlFilterEmp.SelectedIndex != -1)
            {
                if (txtFilterEmpID.Text != "")
                {
                    int EmpID = Convert.ToInt32(txtFilterEmpID.Text);
                    EmployeeReimTransferdAmtByEmpID(objHrCommon, EmpID);
                }

            }


        }

        #endregion Transferd

        protected void Page_Load(object sender, EventArgs e)
        {
            Ajax.Utility.RegisterTypeForAjax(typeof(AjaxDAL));
          
            DataTable dtReimburseList = new DataTable();
          
            SearchCompanyID = Convert.ToInt32(Session["CompanyID"]);
         

            if (!IsPostBack)
            {
                GetParentMenuId();

                Page.Form.Attributes.Add("enctype", "multipart/form-data");
                btnSubmit.Visible = false;
                DataSet dsAU = AttendanceDAC.GetAu();
                DataRow dr = dsAU.Tables[0].NewRow();
                dr["Au_Id"] = 0;
                dr["Au_Name"] = "---Select---";
                dsAU.Tables[0].Rows.InsertAt(dr, 0);
                dsAU.AcceptChanges();
                ArrayList alUnitIndexes = new ArrayList();
                foreach (DataRow row in dsAU.Tables[0].Rows)
                {
                    alUnitIndexes.Add(row["Au_Id"].ToString().Trim());
                }
                ViewState["alUnitIndexes"] = alUnitIndexes;
                ViewState["dsAU"] = dsAU;
                ViewState["PK"] = 0;
                ViewState["EmpID"] = 0;
                ViewState["ERID"] = 0;
                ViewState["DefaultUOM"] = "0";
                DataSet deUOM = AttendanceDAC.GetDefault_UOM();
                if (deUOM != null && deUOM.Tables.Count != 0 && deUOM.Tables[0].Rows.Count > 0)
                {
                    ViewState["DefaultUOM"] = deUOM.Tables[0].Rows[0]["Value"].ToString();
                }

                if (Request.QueryString.Count > 0)
                {
                    if (Request.QueryString.AllKeys.Contains("key"))
                    {
                        int id = Convert.ToInt32(Request.QueryString["key"].ToString());
                        if (id == 1)//Addnew
                        {
                            tblAdd.Visible = true;
                            tblView.Visible = false;
                            BindEmpList();
                            //BindYears();
                            GetWorkSites();
                            GetDepartments();
                            BindItem();
                            dtReimburseList.Columns.Add(new DataColumn("ID", typeof(System.Int32)));
                            dtReimburseList.Columns.Add(new DataColumn("RItemID", typeof(System.Int32)));
                            dtReimburseList.Columns.Add(new DataColumn("RItem", typeof(System.String)));
                            dtReimburseList.Columns.Add(new DataColumn("EmpID", typeof(System.String)));
                            dtReimburseList.Columns.Add(new DataColumn("AUID", typeof(System.String)));
                            dtReimburseList.Columns.Add(new DataColumn("Purpose", typeof(System.String)));
                            dtReimburseList.Columns.Add(new DataColumn("Qty", typeof(System.Double)));
                            dtReimburseList.Columns.Add(new DataColumn("UnitRate", typeof(System.Double)));
                            dtReimburseList.Columns.Add(new DataColumn("Amount", typeof(System.Double)));
                            dtReimburseList.Columns.Add(new DataColumn("DateOfSpent", typeof(System.String)));
                            dtReimburseList.Columns.Add(new DataColumn("Proof", typeof(System.String)));
                            ViewState["ReimItems"] = dtReimburseList;
                            dtReimburseList = (DataTable)ViewState["ReimItems"];
                            gvRemiItems.DataSource = dtReimburseList;
                            gvRemiItems.DataBind();

                        }
                        if (id == 2)//Pending
                        {
                         
                            DataSet ds = AttendanceDAC.HR_EmpPenalties_NotApproved();
                            BindPagerPending();
                            gvView.Columns[0].Visible = true;
                            gvView.Columns[1].Visible = false;

                            gvView.Visible = true;
                            tblView.Visible = true;
                            SqlParameter[] p = new SqlParameter[1];
                            p[0] = new SqlParameter("@Status", 1);

                            FIllObject.FillDropDown(ref ddlFilterEmp, "GetEmployees_By_Penalty_Exp", p);

                            
                        }
                        if (id == 4) //transfered accounts posted
                        {
                            BindPagerPending();
                            EmpReimbursementPendingRejPaging.Visible = false;
                          
                            DataSet ds = AttendanceDAC.HR_Emp_Penalty_Transferd();
                           
                            BindPagerTransAmt();
                            tblTransfered.Visible = true;
                            tblView.Visible = true;
                            gvView.Visible = false;
                            ds = AttendanceDAC.GetEmployeesByCompID(Convert.ToInt32(Session["CompanyID"]));

                            ddlFilterEmp.DataSource = ds.Tables[0];
                            ddlFilterEmp.DataTextField = "name";
                            ddlFilterEmp.DataValueField = "EmpID";
                            ddlFilterEmp.DataBind();
                            ddlFilterEmp.Items.Insert(0, new ListItem("---SELECT EMPLOYEE---", "0", true));

                        }
                        if (id == 3)//rejected
                        {
                        
                            DataSet ds  = AttendanceDAC.HR_EmpPanalityRejected();
                            
                            BindPagerPending();     //Rej
                            gvView.Columns[0].Visible = false;
                            gvView.Columns[1].Visible = true;
                            gvView.Visible = true;
                            tblView.Visible = true;


                            SqlParameter[] p = new SqlParameter[1];
                            p[0] = new SqlParameter("@Status", 3);

                            FIllObject.FillDropDown(ref ddlFilterEmp, "GetEmployees_By_Penalty_Exp", p);
                            
                        }
                        if(id==5)
                        {
                            SqlParameter[] p = new SqlParameter[1];
                            p[0] = new SqlParameter("@Status", 5);

                            FIllObject.FillDropDown(ref ddlTransfer, "GetEmployees_By_Penalty_Exp", p);
                            ddlTransfer.SelectedValue = Request.QueryString["EMPID"];
                            EmpReimbursementAprovedPaging.CurrentPage = 1;
                            int EmpID = Convert.ToInt32(ddlTransfer.SelectedValue);
                            txtTranferSearch.Text = "";
                            if (ddlTransfer.SelectedIndex == 0)
                            {
                                tblbtnapprove.Visible = true;
                                //btnapprovegm_Click(sender, e);
                                tblViewApproved.Visible = true;
                                bindpager_Approved();
                                
                            }
                            else
                            {
                                EmployeeApprovedByEmpID(objHrCommon, EmpID);

                                //ds = AttendanceDAC.HR_EmpReimEmpTranserByEmpID(EmpID);
                                //gvViewApproved.DataSource = ds;
                                //gvViewApproved.DataBind();
                                gvViewApproved.Visible = true;
                                tblViewApproved.Visible = true;
                                gvViewApproved.Columns[0].Visible = true;
                                btnTransferAcc.Visible = false;
                            }
                        }
                        if (id == 6)
                        {
                            SqlParameter[] p = new SqlParameter[1];
                            p[0] = new SqlParameter("@Status", 6);

                            FIllObject.FillDropDown(ref ddlTransfer, "GetEmployees_By_Penalty_Exp", p);
                            ddlTransfer.SelectedValue = Request.QueryString["EMPID"];
                            EmpReimbursementAprovedPaging.CurrentPage = 1;
                            int EmpID = Convert.ToInt32(ddlTransfer.SelectedValue);
                            txtTranferSearch.Text = "";
                            if (ddlTransfer.SelectedIndex == 0)
                            {
                                tblViewApproved.Visible = true;
                                bindpager_Approved();
                                tblbtnapprove.Visible = true;
                            }
                            else
                            {
                                EmployeeApprovedByEmpID(objHrCommon, EmpID);

                                //ds = AttendanceDAC.HR_EmpReimEmpTranserByEmpID(EmpID);
                                //gvViewApproved.DataSource = ds;
                                //gvViewApproved.DataBind();
                                gvViewApproved.Visible = true;
                                tblViewApproved.Visible = true;
                                gvViewApproved.Columns[0].Visible = true;
                                btnTransferAcc.Visible = false;
                            }
                        }
                    }

                    else
                    {
                        SqlParameter[] p = new SqlParameter[1];
                        p[0] = new SqlParameter("@Status", 2);

                        FIllObject.FillDropDown(ref ddlTransfer, "GetEmployees_By_Penalty_Exp", p);
                        ddlTransfer.SelectedValue = Request.QueryString["EMPID"];
                        EmpReimbursementAprovedPaging.CurrentPage = 1;
                        int EmpID = Convert.ToInt32(ddlTransfer.SelectedValue);
                        txtTranferSearch.Text = "";
                        if (ddlTransfer.SelectedIndex == 0)
                        {
                            BindPager();
                        }
                        else
                        {
                            EmployeeApprovedByEmpID(objHrCommon, EmpID);
                              
                            //ds = AttendanceDAC.HR_EmpReimEmpTranserByEmpID(EmpID);
                            //gvViewApproved.DataSource = ds;
                            //gvViewApproved.DataBind();
                            gvViewApproved.Visible = true;
                            tblViewApproved.Visible = true;
                            gvViewApproved.Columns[0].Visible = true;
                            btnTransferAcc.Visible = false;
                        }
                    }

                    

                }
                else
                {//approved
                    //bool IsAllowed = Convert.ToBoolean(ViewState["ViewAll"]);
                    //if (IsAllowed == true)
                    //{
                        tblView.Visible = false;
                        tblAdd.Visible = false;
                        BindPager();
                          
                        //ds = AttendanceDAC.HR_EmpReimEmployees();
                        //gvViewApproved.DataSource = ds;
                        //gvViewApproved.DataBind();
                        gvViewApproved.Visible = true;
                        tblViewApproved.Visible = true;

                        SqlParameter[] p = new SqlParameter[1];
                        p[0] = new SqlParameter("@Status", 2);

                        FIllObject.FillDropDown(ref ddlTransfer, "GetEmployees_By_Penalty_Exp", p);

                    //}
                    //else
                    //{
                    //    Response.Redirect("EmpPenalties.aspx?key=1");
                    //}
                }
            }
        }
        #region Employeestatus

        public void bindpager_Approved()
        {
            try
            {
                objHrCommon.PageSize = EmpReimbursementAprovedPaging.ShowRows;
                objHrCommon.CurrentPage = EmpReimbursementAprovedPaging.CurrentPage;

                SqlParameter[] sqlParams = new SqlParameter[5];
                sqlParams[0] = new SqlParameter("@CurrentPage", objHrCommon.CurrentPage);
                sqlParams[1] = new SqlParameter("@PageSize", objHrCommon.PageSize);
                sqlParams[2] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
                sqlParams[2].Direction = ParameterDirection.ReturnValue;
                sqlParams[3] = new SqlParameter("@NoofRecords", System.Data.SqlDbType.Int);
                sqlParams[3].Direction = ParameterDirection.Output;
                if (Request.QueryString.AllKeys.Contains("key"))
                {
                    int id = Convert.ToInt32(Request.QueryString["key"].ToString());
                    if (id == 5)
                    {
                        sqlParams[4] = new SqlParameter("@status", 5);
                    }
                    if (id == 6)
                    {
                        sqlParams[4] = new SqlParameter("@status", 6);
                    }
                }
                else
                    sqlParams[4] = new SqlParameter("@status", 2);



                DataSet ds = SQLDBUtil.ExecuteDataset("HR_EmpPenality_EmployeesByPaging_New", sqlParams);
                objHrCommon.NoofRecords = (int)sqlParams[3].Value;
                objHrCommon.TotalPages = (int)sqlParams[2].Value;
               
                //pratap date:15-03-2016
                //ds = AttendanceDAC.HR_EmpReimEmployeesByPaging(objHrCommon);
                //DataSet ds = AttendanceDAC.HR_EmpPenalityEmployeesByPaging(objHrCommon);

                if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0)
                {
                    gvViewApproved.DataSource = ds;

                    EmpReimbursementAprovedPaging.Bind(objHrCommon.CurrentPage, objHrCommon.TotalPages, objHrCommon.NoofRecords, objHrCommon.PageSize);

                }
                else
                {
                    gvViewApproved.EmptyDataText = "No Records Found";
                    EmpReimbursementAprovedPaging.Visible = false;
                }
                gvViewApproved.DataBind();
            }
            catch { }

        }
        void EmployeeReimTransferdAmtByEmpID(HRCommon objHrCommon, int EmpID)
        {
            try
            {
                objHrCommon.PageSize = EmpReimbursementTransferdPaging.ShowRows;
                objHrCommon.CurrentPage = EmpReimbursementTransferdPaging.CurrentPage;
                //int EmpID = Convert.ToInt32(txtFilterEmpID.Text);

                  
            DataSet ds = AttendanceDAC.HR_Emp_Penlity_TransferdByEmpIDByPaging(objHrCommon, EmpID, Convert.ToInt32(Session["CompanyID"]));

                if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0)
                {
                    gvTransfered.DataSource = ds;

                    EmpReimbursementTransferdPaging.Bind(objHrCommon.CurrentPage, objHrCommon.TotalPages, objHrCommon.NoofRecords, objHrCommon.PageSize);

                }
                else
                {
                    gvTransfered.EmptyDataText = "No Records Found";
                    EmpReimbursementTransferdPaging.Visible = false;
                }
                gvTransfered.DataBind();

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        void EmployeeReimTransferdAmt(HRCommon objHrCommon)
        {
            try
            {
                objHrCommon.PageSize = EmpReimbursementTransferdPaging.ShowRows;
                objHrCommon.CurrentPage = EmpReimbursementTransferdPaging.CurrentPage;

                  
             DataSet   ds = AttendanceDAC.HR_Emp_PenlityTransferdByPaging(objHrCommon, Convert.ToInt32(Session["CompanyID"]));

                if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0)
                {
                    gvTransfered.DataSource = ds;

                    EmpReimbursementTransferdPaging.Bind(objHrCommon.CurrentPage, objHrCommon.TotalPages, objHrCommon.NoofRecords, objHrCommon.PageSize);

                }
                else
                {
                    gvTransfered.EmptyDataText = "No Records Found";
                    EmpReimbursementTransferdPaging.Visible = false;

                }
                gvTransfered.DataBind();


            }
            catch (Exception e)
            {
                throw e;
            }
        }

        void EmployeeReimRejectedByEmpID(HRCommon objHrCommon, int EmpID)
        {
            try
            {
                objHrCommon.PageSize = EmpReimbursementPendingRejPaging.ShowRows;
                objHrCommon.CurrentPage = EmpReimbursementPendingRejPaging.CurrentPage;

                  
                //Rijwan:18-03-2016
                DataSet ds = AttendanceDAC.HR_EmpPenlity_RejectedByEmpIDByPaging(objHrCommon, EmpID, Convert.ToInt32(Session["CompanyID"]));

                if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0)
                {
                    gvView.DataSource = ds;

                    EmpReimbursementPendingRejPaging.Bind(objHrCommon.CurrentPage, objHrCommon.TotalPages, objHrCommon.NoofRecords, objHrCommon.PageSize);

                }
                else
                {
                    gvView.EmptyDataText = "No Records Found";
                    EmpReimbursementPendingRejPaging.Visible = false;
                }
                gvView.DataBind();


            }
            catch (Exception e)
            {
                throw e;
            }
        }
        void EmployeeReimRejected(HRCommon objHrCommon)
        {
            try
            {
                objHrCommon.PageSize = EmpReimbursementPendingRejPaging.ShowRows;
                objHrCommon.CurrentPage = EmpReimbursementPendingRejPaging.CurrentPage;
                //Rijwan:15-03-2016

                DataSet ds = AttendanceDAC.HR_EmpPenalityRejectedByPaging(objHrCommon, Convert.ToInt32(Session["CompanyID"]));

                if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0)
                {
                    gvView.DataSource = ds;

                    EmpReimbursementPendingRejPaging.Bind(objHrCommon.CurrentPage, objHrCommon.TotalPages, objHrCommon.NoofRecords, objHrCommon.PageSize);

                }
                else
                {
                    gvView.EmptyDataText = "No Records Found";
                    EmpReimbursementPendingRejPaging.Visible = false;
                }
                gvView.DataBind();


            }
            catch (Exception e)
            {
                throw e;
            }
        }
        void EmployeeNotApproved(HRCommon objHrCommon, int EmpID)
        {
            try
            {
                objHrCommon.PageSize = EmpReimbursementPendingRejPaging.ShowRows;
                objHrCommon.CurrentPage = EmpReimbursementPendingRejPaging.CurrentPage;
                //Rijwan ;16-03-2016

                DataSet ds = AttendanceDAC.HR_EmpPenlityNotApprovedByEmpIDByPaging(objHrCommon, EmpID, Convert.ToInt32(Session["CompanyID"]));

                if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0)
                {
                    gvView.DataSource = ds;

                    EmpReimbursementPendingRejPaging.Bind(objHrCommon.CurrentPage, objHrCommon.TotalPages, objHrCommon.NoofRecords, objHrCommon.PageSize);

                }
                else
                {
                    gvView.EmptyDataText = "No Records Found";
                    EmpReimbursementPendingRejPaging.Visible = false;
                }
                gvView.DataBind();


            }
            catch (Exception e)
            {
                throw e;
            }
        }
        void EmployeeNotApproved(HRCommon objHrCommon)
        {
            try
            {
                objHrCommon.PageSize = EmpReimbursementPendingRejPaging.ShowRows;
                objHrCommon.CurrentPage = EmpReimbursementPendingRejPaging.CurrentPage;

                  
                //ds = AttendanceDAC.HR_EmpReimNotApprovedByPaging(objHrCommon, Convert.ToInt32(Session["CompanyID"]));
                //pratap date:15-03-2016
                DataSet ds = AttendanceDAC.HR_Emp_Penalty_NotApprovedByPaging(objHrCommon, Convert.ToInt32(Session["CompanyID"]));
                if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0)
                {
                    gvView.DataSource = ds;

                    EmpReimbursementPendingRejPaging.Bind(objHrCommon.CurrentPage, objHrCommon.TotalPages, objHrCommon.NoofRecords, objHrCommon.PageSize);

                }
                else
                {
                    gvView.EmptyDataText = "No Records Found";
                    EmpReimbursementPendingRejPaging.Visible = false;
                }
                gvView.DataBind();


            }
            catch (Exception e)
            {
                throw e;
            }
        }
        void EmployeeApproved(HRCommon objHrCommon)
        {
            try
            {
                objHrCommon.PageSize = EmpReimbursementAprovedPaging.ShowRows;
                objHrCommon.CurrentPage = EmpReimbursementAprovedPaging.CurrentPage;

                  
                //pratap date:15-03-2016
                //ds = AttendanceDAC.HR_EmpReimEmployeesByPaging(objHrCommon);
                DataSet ds = AttendanceDAC.HR_EmpPenalityEmployeesByPaging(objHrCommon);

                if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0)
                {
                    gvViewApproved.DataSource = ds;

                    EmpReimbursementAprovedPaging.Bind(objHrCommon.CurrentPage, objHrCommon.TotalPages, objHrCommon.NoofRecords, objHrCommon.PageSize);

                }
                else
                {
                    gvViewApproved.EmptyDataText = "No Records Found";
                    EmpReimbursementAprovedPaging.Visible = false;
                }
                gvViewApproved.DataBind();


            }
            catch (Exception e)
            {
                throw e;
            }
        }
        #endregion Employeestatus

        public DataSet GetAUDataSet()
        {
            return (DataSet)ViewState["dsAU"];
        }

        public int GetAUIndex(string AUID)
        {
            ArrayList alUnitIndexes = (ArrayList)ViewState["alUnitIndexes"];
            return alUnitIndexes.IndexOf(AUID.Trim());
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
              
                // Editable = (bool)ds.Tables[0].Rows[0]["Editable"];
                //btnApprove.Enabled = Convert.ToBoolean(ds.Tables[0].Rows[0]["ViewAll"].ToString());
                //ViewState["ViewAll"] = Convert.ToBoolean(ds.Tables[0].Rows[0]["ViewAll"].ToString());
                //gvShow.Columns[14].Visible = Convert.ToBoolean(ds.Tables[0].Rows[0]["Editable"].ToString());
                //gvShow.Columns[15].Visible = Convert.ToBoolean(ds.Tables[0].Rows[0]["Editable"].ToString());
                //gvShow.Columns[16].Visible = false;
                //gvShowRej.Columns[17].Visible = Convert.ToBoolean(ds.Tables[0].Rows[0]["Editable"].ToString());
                //btnApprove.Enabled = btnRApprove.Enabled = btnTransferAcc.Enabled = Convert.ToBoolean(ds.Tables[0].Rows[0]["ViewAll"].ToString());
                //btnSave.Enabled = btnSubmit.Enabled = btnTransferAcc.Enabled = BtnEditSave.Enabled = btnApprove.Enabled = btnRApprove.Enabled = btnRejReaSave.Enabled = Editable;
            }
            return MenuId;
        }
        public void BindEmpList()
        {
            string EmpName = string.Empty;
            int WorkSite = Convert.ToInt32(null);
            int Dept = Convert.ToInt32(null);
            DataSet ds = AttendanceDAC.GetEmployeesByCompID(Convert.ToInt32(Session["CompanyID"]));

            //ds = AttendanceDAC.HR_SearchReimburseEmp();
            //ds = AttendanceDAC.HR_SearchReimburseEmp(null,null,Convert.ToInt32(Session["CompanyID"]));
            ddlEmp.DataSource = ds.Tables[0];
            ddlEmp.DataTextField = "name";
            ddlEmp.DataValueField = "EmpID";
            ddlEmp.DataBind();
            ddlEmp.Items.Insert(0, new ListItem("---SELECT---", "0", true));
        }
        public void BindReimburseEmp()
        {
              
            //Rijwan:18-03-2016
            DataSet ds = AttendanceDAC.HR_EmpPenlity_Employees(Convert.ToInt32(Session["CompanyID"]));
            gvView.DataSource = ds;
            gvView.DataBind();
            gvView.Visible = true;
        }
        protected string FormatInput(object EntryType)
        {
            string retValue = "";
            string input = EntryType.ToString();
            if (input == "1")
            {
                retValue = "Pending";
            }
            if (input == "2")
            {
                retValue = "Approved";
                gvViewApproved.Columns[5].ControlStyle.ForeColor = System.Drawing.Color.Green;
            }
            if (input == "3")
            {
                retValue = "Rejected";
                gvView.Columns[5].ControlStyle.ForeColor = System.Drawing.Color.Red;
            }
            if (input == "4")
            {
                retValue = "Transfered";
            }
            return retValue;
        }
        public string DocNavigateUrl(string Proof)
        {
            string ReturnVal = "";
            string Value = Proof.Split('.')[Proof.Split('.').Length - 1];
            ReturnVal = "/EmpPenaltyProof/" + Proof;
            if (ReturnVal == "/EmpPenaltyProof/")
            {
                return null;
            }
            else
            {
                return "javascript:return window.open('" + ReturnVal + "', '_blank')";
            }
            
        }


        public string ViewItemsNavigateUrl(string ERID)
        {
            return "javascript:return window.open('ViewEmpPenalty.aspx?key=" + ERID + "', '_blank')";
        }
        public void BindItem()
        {

            DataSet ds = PayRollMgr.GetEmployeePenalties();
            lstItems.DataSource = ds.Tables[0];
            lstItems.DataTextField = "Name";
            lstItems.DataValueField = "RMItemId";
            lstItems.DataBind();
        }
        private void GetWorkSites()
        {
              
            AttendanceDAC ADAC = new AttendanceDAC();
           DataSet ds = AttendanceDAC.GetWorkSite(0, '1', Convert.ToInt32(Session["CompanyID"]));
            ddlWorksite.DataSource = ds.Tables[0];
            ddlWorksite.DataTextField = "Site_Name";
            ddlWorksite.DataValueField = "Site_ID";
            ddlWorksite.DataBind();
            ddlWorksite.Items.Insert(0, new ListItem("---ALL---", "0", true));
        }
        private void GetDepartments()
        {
              
            AttendanceDAC ADAC = new AttendanceDAC();
            DataSet ds = ADAC.GetDepartments(0);
            ddlDepartment.DataSource = ds.Tables[0];
            ddlDepartment.DataTextField = "DeptName";
            //ddlDepartment.DataTextField = "DepartmentName";
            ddlDepartment.DataValueField = "DepartmentUId";
            ddlDepartment.DataBind();
            ddlDepartment.Items.Insert(0, new ListItem("---ALL---", "0", true));

        }

        protected void gvRemiItems_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            foreach (GridViewRow Row in gvRemiItems.Rows)
            {
                DropDownList ddlunits = new DropDownList();
                ddlunits = (DropDownList)Row.FindControl("ddlunits");
                if (ViewState["DefaultUOM"].ToString() != "")
                    if (ddlunits.Items.FindByValue(ViewState["DefaultUOM"].ToString()) != null)
                        ddlunits.Items.FindByValue(ViewState["DefaultUOM"].ToString()).Selected = true;
                if (ddlunits != null)
                    ddlunits.Enabled = false;
                //ddlunits.SelectedValue = "34";
                // ddlunits.Items.FindByValue("Rs").Selected = true;
            }
          
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {


            if (ddlEmp.SelectedIndex > 0)
            {
                int[] Secarrary = lstItems.GetSelectedIndices();
                if (Secarrary.Length > 0)
                {
                    AttendanceDAC ADAC = new AttendanceDAC();
                    DataTable dtReimburseList = new DataTable();
                    DataRow dtRow;
                    dtReimburseList = (DataTable)ViewState["ReimItems"];
                      
                    ListItem item = null;
                    string EmpID = ddlEmp.SelectedItem.Value;
                    foreach (int indexItem in lstItems.GetSelectedIndices())
                    {
                        item = lstItems.Items[indexItem];
                        dtRow = dtReimburseList.NewRow();
                        if (ViewState["Id"] != null)
                        {
                            Id = Convert.ToInt32(ViewState["Id"]);
                        }
                        dtRow["ID"] = Id;
                        dtRow["RItemID"] = item.Value;
                        dtRow["RItem"] = item.Text;
                        dtRow["EmpID"] = ddlEmp.SelectedItem.Value;
                        dtRow["Purpose"] = "";
                        dtRow["Qty"] = "1";
                        //dtRow["DateOfSpent"] = DateTime.Now.ToString(ConfigurationManager.AppSettings["DateFormat"]);
                        dtRow["DateOfSpent"] = DateTime.Now.ToString("dd MMM yyyy");
                        dtRow["Proof"] = "";
                        dtReimburseList.Rows.Add(dtRow);
                        item.Selected = false;
                        Id = Id + 1;
                        ViewState["Id"] = Id;
                    }
                    foreach (GridViewRow row in gvRemiItems.Rows)
                    {
                        TextBox txtpur = (TextBox)row.FindControl("txtPurpose");
                        TextBox txtqty = (TextBox)row.FindControl("txtQty");
                        Label txtAmount = (Label)row.FindControl("txtAmount");
                        TextBox txtrate = (TextBox)row.FindControl("txtrate");
                        Label lblItemId = (Label)row.FindControl("lblRItem");
                        TextBox txtSpentOn = (TextBox)row.FindControl("txtSpentOn");
                        DropDownList ddlunits = new DropDownList();
                        ddlunits = (DropDownList)row.FindControl("ddlunits");
                        FileUpload UploadProof = (FileUpload)row.FindControl("UploadProof");
                        DataRow[] drSelected = dtReimburseList.Select("RItem='" + lblItemId.Text + "'");
                        drSelected[0]["Purpose"] = txtpur.Text;
                        drSelected[0]["Qty"] = txtqty.Text;//
                        drSelected[0]["EmpID"] = ddlEmp.SelectedItem.Value;
                        if (txtrate.Text == "") { Convert.ToDouble(txtrate.Text = "0"); }
                        drSelected[0]["UnitRate"] = Convert.ToDouble(txtrate.Text);
                        drSelected[0]["AUID"] = ddlunits.SelectedValue;
                        drSelected[0]["DateOfSpent"] = DateTime.Now.ToString("dd/MM/yyyy");
                        drSelected[0]["Proof"] = UploadProof.FileName;
                    }
                    dtReimburseList.AcceptChanges();
                    ViewState["ReimItems"] = dtReimburseList;
                    dtReimburseList = (DataTable)ViewState["ReimItems"];
                    gvRemiItems.DataSource = dtReimburseList;
                    gvRemiItems.DataBind();
                    btnSubmit.Visible = true;
                  
                }
                else
                {
                    AlertMsg.MsgBox(Page, "Select Item,", AlertMsg.MessageType.Warning);
                }
            }
            else
            {
                AlertMsg.MsgBox(Page, "Select Employee", AlertMsg.MessageType.Warning);
            }
        }

        protected void txtSearch_Click(object sender, EventArgs e)
        {
            if (txtFilter.Text == "")
            {
                  
                string EmpName = string.Empty;
                int WorkSite = Convert.ToInt32(ddlWorksite.SelectedValue);
                int Dept = Convert.ToInt32(ddlDepartment.SelectedValue);
                DataSet ds = AttendanceDAC.HR_SerchEmp_Reimburse(EmpName, Convert.ToInt32(Session["CompanyID"]));
                ddlEmp.DataSource = ds.Tables[1];
                ddlEmp.DataTextField = "name";
                ddlEmp.DataValueField = "EmpID";
                ddlEmp.DataBind();
                ddlEmp.Items.Insert(0, new ListItem("---SELECT---", "0", true));
            }
            else
            {
                  
                string EmpName = txtFilter.Text;
                int WorkSite = Convert.ToInt32(ddlWorksite.SelectedValue);
                int Dept = Convert.ToInt32(ddlDepartment.SelectedValue);
                DataSet ds = AttendanceDAC.HR_SerchEmp_Reimburse(EmpName, Convert.ToInt32(Session["CompanyID"]));
                ddlEmp.DataSource = ds.Tables[1];
                ddlEmp.DataTextField = "name";
                ddlEmp.DataValueField = "EmpID";
                ddlEmp.DataBind();
                ddlEmp.Items.Insert(0, new ListItem("---SELECT---", "0", true));
            }
        }
        protected void gvRemiItems_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Del")
            {

                int ID = Convert.ToInt32(e.CommandArgument);
                DataTable dtList = (DataTable)ViewState["ReimItems"];
                DataRow[] drSelected = null;
                drSelected = dtList.Select("ID='" + e.CommandArgument + "'");
                if (drSelected.Length > 0)
                    dtList.Rows.Remove(drSelected[0]);
                dtList.AcceptChanges();
                ViewState["ReimItems"] = dtList;
                gvRemiItems.DataSource = dtList;
                gvRemiItems.DataBind();
                if (gvRemiItems.Rows.Count <= 0)
                {
                    btnSubmit.Visible = false;
                }
            }


        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            btnApprove.Visible = false;
            DataSet dsRemItems = new DataSet("RemItemDataSet");
            DataTable dt = new DataTable("RemItemTable");
            dt.Columns.Add(new DataColumn("EmpID", typeof(System.Int32)));
            dt.Columns.Add(new DataColumn("RItemID", typeof(System.Int32)));
            dt.Columns.Add(new DataColumn("Uom", typeof(System.String)));
            dt.Columns.Add(new DataColumn("Purpose", typeof(System.String)));

            dt.Columns.Add(new DataColumn("Quantity", typeof(System.Double)));
            dt.Columns.Add(new DataColumn("Rate", typeof(System.Double)));
            dt.Columns.Add(new DataColumn("SpentOn", typeof(System.String)));
            dt.Columns.Add(new DataColumn("DOS", typeof(System.String)));
            dt.Columns.Add(new DataColumn("Proof", typeof(System.String)));
            dt.Columns.Add(new DataColumn("Status", typeof(System.Int32)));
            dsRemItems.Tables.Add(dt);
            foreach (GridViewRow gvRow in gvRemiItems.Rows)
            {
                Label lblEmpID = (Label)gvRow.Cells[2].FindControl("lblEmpID");
                Label lblRItem = (Label)gvRow.Cells[3].FindControl("lblRItemNo");
                DropDownList ddlunits = (DropDownList)gvRow.Cells[4].FindControl("ddlunits");
                int UnitOfMeasure = int.Parse(ddlunits.Text);
                if (UnitOfMeasure == 0)
                {
                    AlertMsg.MsgBox(Page, "Please select units of measure.!", AlertMsg.MessageType.Warning);
                    return;
                }

                TextBox txtRate = (TextBox)gvRow.Cells[5].FindControl("txtRate");
                double UnitRate;// = int.Parse(txtRate.Text);
                try
                {
                    UnitRate = double.Parse(txtRate.Text);

                }
                catch (Exception)
                {

                    AlertMsg.MsgBox(Page, "Unitrate can takes numbers only.!", AlertMsg.MessageType.Warning);
                    return;
                }
                if (txtRate.Text == "" || UnitRate <= 0)
                {
                    AlertMsg.MsgBox(Page, "Please enter proper unitrate.!", AlertMsg.MessageType.Warning);
                    return;
                }
                else
                {
                    UnitRate = double.Parse(txtRate.Text);
                }


                TextBox txtQty = (TextBox)gvRow.Cells[6].FindControl("txtQty");
                double Quantity;
                try
                {
                    Quantity = double.Parse(txtQty.Text);

                }
                catch (Exception)
                {

                    AlertMsg.MsgBox(Page, "Quantity can take numbers only.!", AlertMsg.MessageType.Warning);
                    return;
                }
                if (txtQty.Text == "" || Quantity <= 0)
                {
                    AlertMsg.MsgBox(Page, "Please enter proper quntity.!", AlertMsg.MessageType.Warning);
                    return;
                }
                else
                {
                    Quantity = double.Parse(txtQty.Text);

                }



                Label txtAmount = (Label)gvRow.Cells[7].FindControl("txtAmount");
                //ViewState["Amount"]

                TextBox txtPurpose = (TextBox)gvRow.Cells[8].FindControl("txtPurpose");
                Label lblRItemID = (Label)gvRow.Cells[9].FindControl("lblRItemNo");
                TextBox txtSpentOn = (TextBox)gvRow.Cells[10].FindControl("txtSpentOn");

                try
                {

                    DateTime DateOfSpent = CodeUtilHMS.ConvertToDate_ddMMMyyy(txtSpentOn.Text.Trim(), CodeUtilHMS.DateFormat.ddMMMyyyy);

                }
                catch (Exception)
                {

                    AlertMsg.MsgBox(Page, "Please select proper date.!", AlertMsg.MessageType.Warning);
                    return;
                }

                FileUpload UploadProof = (FileUpload)gvRow.Cells[11].FindControl("UploadProof");
                String MyString = string.Empty;
                string extension = string.Empty;
                if (UploadProof.HasFile)
                {
                    DateTime MyDate = DateTime.Now;
                    MyString = MyDate.ToString("ddMMyyhhmmss");
                    extension = System.IO.Path.GetExtension(UploadProof.PostedFile.FileName).ToLower();
                }
                else
                {
                    AlertMsg.MsgBox(Page, "Please upload Proof.!", AlertMsg.MessageType.Warning);
                    return;
                }
                if (UploadProof.HasFile)
                {
                    string storePath = Server.MapPath("~") + "/" + "EmpPenaltyProof/";
                    if (!Directory.Exists(storePath))
                        Directory.CreateDirectory(storePath);
                    UploadProof.PostedFile.SaveAs(storePath + "/" + MyString + extension);
                }
                string Proof = MyString + extension;
                DataRow dr = dt.NewRow();
                dr["RItemID"] = Convert.ToInt32(lblRItemID.Text);
                dr["EmpID"] = Convert.ToInt32(lblEmpID.Text);
                ViewState["EmpView"] = Convert.ToInt32(lblEmpID.Text);
                dr["Uom"] = Convert.ToInt32(ddlunits.SelectedValue);
                dr["Rate"] = Convert.ToDouble(txtRate.Text);
                dr["Quantity"] = Convert.ToDouble(txtQty.Text);
                dr["Purpose"] = txtPurpose.Text.ToString();
                dr["SpentOn"] = txtSpentOn.Text;
                dr["DOS"] = DateTime.Now.ToString(ConfigurationManager.AppSettings["DateFormat"]);
                dr["Proof"] = Proof;
                dr["Status"] = 1;
                dt.Rows.Add(dr);
            }
            dsRemItems.AcceptChanges();
            try
            {
               
                DataSet ds = AttendanceDAC.HR_EmpPenalty_Items(dsRemItems);
            }
            catch (Exception ex)
            {
                AlertMsg.MsgBox(Page, ex.Message.ToString(),AlertMsg.MessageType.Error);
                return;
            }
            gvRemiItems.Visible = false;

            tblAdd.Visible = false;
            tblShow.Visible = true;
            tblView.Visible = true;
            gvView.Visible = true;
            gvShow.Visible = true;
            AlertMsg.MsgBox(Page, "Saved");
            //if (Convert.ToBoolean(ViewState["ViewAll"]) == true)
            //{
            //    Response.Redirect("EmpPenalties.aspx?key=2");
            //}
            //else { Response.Redirect("EmpPenalties.aspx?key=1"); }
        }
        protected void txtUnitrete_TextChanged(object sender, EventArgs e)
        {
            CalculateAmt();

        }
        protected void txtQuantity_TextChanged(object sender, EventArgs e)
        {
            CalculateAmt();
        }
        public void CalculateAmt()
        {
            foreach (GridViewRow gvRow in gvRemiItems.Rows)
            {

                TextBox txtRate = (TextBox)gvRow.Cells[5].FindControl("txtRate");
                double UnitRate;// = int.Parse(txtRate.Text);
                try
                {
                    UnitRate = double.Parse(txtRate.Text);

                }
                catch (Exception)
                {

                    AlertMsg.MsgBox(Page, "Unitrate can takes numbers only.!", AlertMsg.MessageType.Warning);
                    return;
                }
                if (txtRate.Text == "" || UnitRate <= 0)
                {
                    AlertMsg.MsgBox(Page, "Please enter proper unitrate.!", AlertMsg.MessageType.Warning);
                    return;
                }
                else
                {
                    UnitRate = double.Parse(txtRate.Text);
                }


                TextBox txtQty = (TextBox)gvRow.Cells[6].FindControl("txtQty");
                double Quantity;
                try
                {
                    Quantity = double.Parse(txtQty.Text);

                }
                catch (Exception)
                {

                    AlertMsg.MsgBox(Page, "Quantity can take numbers only.!", AlertMsg.MessageType.Warning);
                    return;
                }
                if (txtQty.Text == "" || Quantity <= 0)
                {
                    AlertMsg.MsgBox(Page, "Please enter proper quntity.!", AlertMsg.MessageType.Warning);
                    return;
                }
                else
                {
                    Quantity = double.Parse(txtQty.Text);

                }

                double Amount = UnitRate * Quantity;
                Label txtAmount = (Label)gvRow.Cells[7].FindControl("txtAmount");
                txtAmount.Text = Convert.ToString(Amount);
                ViewState["Amount"] = Amount;

            }
        }
        protected void gvView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {
                BindEmpList();
                //BindYears();
                GetWorkSites();
                GetDepartments();
                BindItem();
                tblAdd.Visible = true;
                tblView.Visible = false;
                // gvView.Visible = false;
                int EmpID = Convert.ToInt32(e.CommandArgument);
                  
                //pratap date:16-03-2016
                //ds = AttendanceDAC.HR_EmpReimburseAmtPayable(EmpID);
              DataSet  ds = AttendanceDAC.HR_Emp_Penalty_AmtPayable(EmpID);
                ddlEmp.SelectedValue = Convert.ToInt32(ds.Tables[0].Rows[0]["EmpID"]).ToString();
                gvRemiItems.DataSource = ds;
                gvRemiItems.DataBind();
                gvRemiItems.Visible = true;
                btnSubmit.Visible = true;
            }
            if (e.CommandName == "Approve")
            {
                int EmpID = Convert.ToInt32(e.CommandArgument);
                  
                //Rijwan date:17-03-2016
                //ds = AttendanceDAC.HR_EmpReimEmployeesByEmpID(EmpID);
                DataSet ds = AttendanceDAC.HR_Emp_Penality_ByEmpID(EmpID);
                double Amount = Convert.ToDouble(ds.Tables[0].Rows[0]["Amount"]);
            }
        }
        protected void gvView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.Header)
                {
                    CheckBox chkMail = (CheckBox)e.Row.FindControl("chkSelectAll");
                    chkMail.Attributes.Add("onclick", String.Format("javascript:SelectAll(this,'{0}','chkApproval');", gvView.ClientID));
                }
            }
            catch (Exception Ex)
            {
                //report error
            }
        }
        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            tblAdd.Visible = false;
            tblView.Visible = true;
        }
        protected void gvShow_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            
            try
            {
                if (e.Row.RowType == DataControlRowType.Header)
                {
                    CheckBox chkMail = (CheckBox)e.Row.FindControl("chkSelectAll");
                    chkMail.Attributes.Add("onclick", String.Format("javascript:return SelectAll(this,'{0}','chkSelectOne');", gvShow.ClientID));
                }
            }
            catch (Exception Ex)
            {
                //report error
            }
        }
        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            tblAdd.Visible = true;
            tblView.Visible = false;
        }
       
        protected void gvShow_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Edt")
            {
                btnApprove.Visible = false;
                BindEmpList();
                GetWorkSites();
                GetDepartments();
                BindItem();
                tblAdd.Visible = false;
                btnSubmit.Visible = false;
               
                int PK = Convert.ToInt32(e.CommandArgument);
                ViewState["PK"] = PK;
                  
               
              DataSet  ds = AttendanceDAC.HR_Edit_Emp_Penalities_Items(PK);
                ddlEmp.SelectedValue = Convert.ToInt32(ds.Tables[0].Rows[0]["EmpID"]).ToString();
                gvEdit.DataSource = ds;
                gvEdit.DataBind();
                gvRemiItems.Visible = false;
                gvShow.Visible = true;
                tblEdit.Visible = true;
                foreach (GridViewRow gvRow in gvShow.Rows)
                {
                    CheckBox chk = new CheckBox();
                    chk = (CheckBox)gvRow.FindControl("chkSelectOne");
                    Label lbl = (Label)gvRow.FindControl("lblPK");
                    int ERIDToChk = Convert.ToInt32(lbl.Text);
                    if (ERIDToChk == PK)
                    {
                        chk.Checked = true;
                    }
                    else { chk.Checked = false; }
                }

            }
            if (e.CommandName == "Del")
            {
                int PK = Convert.ToInt32(e.CommandArgument);
            }
            if (e.CommandName == "Reject")
            {
                btnApprove.Enabled = false;
                int PK = Convert.ToInt32(e.CommandArgument);
                ViewState["PK"] = PK;
                tblRejReason.Visible = true;
                txtReason.Text = "";
                foreach (GridViewRow gvRow in gvShow.Rows)
                {
                    CheckBox chk = new CheckBox();
                    chk = (CheckBox)gvRow.FindControl("chkSelectOne");
                    Label lbl = (Label)gvRow.FindControl("lblPK");
                    int ERIDToChk = Convert.ToInt32(lbl.Text);
                    if (ERIDToChk == PK)
                    {
                        chk.Checked = true;
                    }
                    else { chk.Checked = false; }
                }

            }

        }
       
        protected void btnApprove_Click(object sender, EventArgs e)
        {
            string URL = Request.Url.Segments[Request.Url.Segments.Length - 1] + Request.Url.Query;

            //start
            if (URL == "EmpPenalties.aspx?key=2")
            {

                foreach (GridViewRow gvRow in gvView.Rows)
                {

                    CheckBox chk = new CheckBox();
                    chk = (CheckBox)gvRow.FindControl("chkApproval");
                    if (chk.Checked)
                    {
                        Label Emp = (Label)gvRow.FindControl("lblEmpID");
                        int EmpID = Convert.ToInt32(Emp.Text);
                        ViewState["EmpID"] = EmpID;
                        foreach (GridViewRow gvRows in gvShow.Rows)
                        {
                            CheckBox chked = new CheckBox();
                            chked = (CheckBox)gvRows.FindControl("chkSelectOne");


                            if (chked.Checked)//&& ERID==ChkERID
                            {
                                Label lblERID = (Label)gvRows.FindControl("lblERID");
                                int ERID = Convert.ToInt32(lblERID.Text);
                                Label lblERItemID = (Label)gvRows.FindControl("lblPK");
                                int ERItemID = Convert.ToInt32(lblERItemID.Text);
                                //Rijwan:16-03-2016
                                AttendanceDAC.HR_EmpPenality_GMApproveStatus(ERItemID);
                                if (ERID != Convert.ToInt32(ViewState["ERID"]))
                                {//Rijwan:16-03-2016
                                    AttendanceDAC.HR_EmpPenality_GMUpdateAsApproved(Convert.ToInt32(Session["UserId"]), ERID);
                                    ViewState["ERID"] = ERID;
                                }
                            }

                        }

                    }
                }
                tblShow.Visible = false;

                DataSet dsRefresh = AttendanceDAC.HR_EmpPenalties_NotApproved();// AttendanceDAC.HR_EmpReimNotApprovedByEmpID(Convert.ToInt32(ViewState["EmpID"]));
                BindPagerPending();
                gvView.Visible = true;
                gvView.Columns[0].Visible = true;
                gvView.Columns[1].Visible = false;
                tblView.Visible = true;
                tblShow.Visible = true;

                //Rijwan:16-03-2016
                DataSet ds = AttendanceDAC.HR_Emp_Penalty_AmtPayable(Convert.ToInt32(ViewState["EmpID"]));
                gvShow.DataSource = ds;
                gvShow.DataBind();
                gvShow.Visible = true;

                if (ds.Tables[0].Rows.Count == 0)
                {
                    btnApprove.Visible = false;
                }

            }
            //end
            //commentec by nadeem
            //if (URL == "EmpPenalties.aspx?key=2")
            //{

            //    foreach (GridViewRow gvRow in gvView.Rows)
            //    {

            //        CheckBox chk = new CheckBox();
            //        chk = (CheckBox)gvRow.FindControl("chkApproval");
            //        if (chk.Checked)
            //        {
            //            Label Emp = (Label)gvRow.FindControl("lblEmpID");
            //            int EmpID = Convert.ToInt32(Emp.Text);
            //            ViewState["EmpID"] = EmpID;
            //            foreach (GridViewRow gvRows in gvShow.Rows)
            //            {
            //                CheckBox chked = new CheckBox();
            //                chked = (CheckBox)gvRows.FindControl("chkSelectOne");


            //                if (chked.Checked)//&& ERID==ChkERID
            //                {
            //                    Label lblERID = (Label)gvRows.FindControl("lblERID");
            //                    int ERID = Convert.ToInt32(lblERID.Text);
            //                    Label lblERItemID = (Label)gvRows.FindControl("lblPK");
            //                    int ERItemID = Convert.ToInt32(lblERItemID.Text);
            //                    //Rijwan:16-03-2016
            //                    AttendanceDAC.HR_EmpPenality_ApproveStatus(ERItemID);
            //                    if (ERID != Convert.ToInt32(ViewState["ERID"]))
            //                    {//Rijwan:16-03-2016
            //                        AttendanceDAC.HR_EmpPenality_UpdateAsApproved( Convert.ToInt32(Session["UserId"]), ERID);
            //                        ViewState["ERID"] = ERID;
            //                    }
            //                }

            //            }

            //        }
            //    }
            //    tblShow.Visible = false;
               
            //    DataSet dsRefresh = AttendanceDAC.HR_EmpPenalties_NotApproved();// AttendanceDAC.HR_EmpReimNotApprovedByEmpID(Convert.ToInt32(ViewState["EmpID"]));
            //    BindPagerPending();
            //    gvView.Visible = true;
            //    gvView.Columns[0].Visible = true;
            //    gvView.Columns[1].Visible = false;
            //    tblView.Visible = true;
            //    tblShow.Visible = true;
                  
            //    //Rijwan:16-03-2016
            // DataSet   ds = AttendanceDAC.HR_Emp_Penalty_AmtPayable(Convert.ToInt32(ViewState["EmpID"]));
            //    gvShow.DataSource = ds;
            //    gvShow.DataBind();
            //    gvShow.Visible = true;

            //    if (ds.Tables[0].Rows.Count == 0)
            //    {
            //        btnApprove.Visible = false;
            //    }

            //}
            if (URL == "EmpPenalties.aspx?key=3")
            {
                foreach (GridViewRow gvRow in gvView.Rows)
                {

                    CheckBox chk = new CheckBox();
                    chk = (CheckBox)gvRow.FindControl("chkRSelect");
                    if (chk.Checked)
                    {
                        Label Emp = (Label)gvRow.FindControl("lblEmpID");
                        int EmpID = Convert.ToInt32(Emp.Text);
                        ViewState["EmpID"] = EmpID;
                        foreach (GridViewRow gvRows in gvShowRej.Rows)
                        {
                            CheckBox chked = new CheckBox();
                            chked = (CheckBox)gvRows.FindControl("chkSelectOne");
                            Label lblERID = (Label)gvRows.FindControl("lblERID");
                            int ERID = Convert.ToInt32(lblERID.Text);
                            Label lblERItemID = (Label)gvRows.FindControl("lblPK");
                            int ERItemID = Convert.ToInt32(lblERItemID.Text);

                            if (chked.Checked)//&& ERID==ChkERID
                            {//Rijwan:16-03-2016
                                AttendanceDAC.HR_EmpPenality_ApproveStatus(ERItemID);
                                if (ERID != Convert.ToInt32(ViewState["ERID"]))
                                {//Rijwan:16-03-2016
                                    AttendanceDAC.HR_EmpPenality_UpdateAsApproved( Convert.ToInt32(Session["UserId"]), ERID);
                                    ViewState["ERID"] = ERID;
                                }
                            }
                        }

                    }
                }
                tblShow.Visible = false;
                DataSet dsRefresh = AttendanceDAC.HR_EmpPanalityRejected();// AttendanceDAC.HR_EmpReimNotApprovedByEmpID(Convert.ToInt32(ViewState["EmpID"]));
               
                BindPagerPending();
                gvView.Columns[0].Visible = false;
                gvView.Visible = true;
                gvView.Columns[1].Visible = true;
                gvView.Columns[0].Visible = false;
                if (dsRefresh.Tables[0].Rows.Count == 0)
                {
                    btnApprove.Visible = false;
                }
                  
                //Rijwan:16-03-2016
                DataSet ds = AttendanceDAC.HR_EmpPenality_RejectedView(Convert.ToInt32(ViewState["EmpID"]));
                gvShowRej.DataSource = ds;
                gvShowRej.DataBind();
                gvShowRej.Visible = true;
                tblShowRej.Visible = true;
            }

        }


        protected void gvViewApproved_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "View")
            {
                tblViewApproved.Visible = false;
                tblAdd.Visible = false;
                tblShow.Visible = true;
                tblView.Visible = false;
                int ERID = Convert.ToInt32(e.CommandArgument);
                  
                //Rijwan:17-03-2016
               DataSet ds = AttendanceDAC.HR_EmpPenality_ViewAmtApproved(ERID);
                gvShow.DataSource = ds;
                gvShow.DataBind();
                if (ds.Tables.Count > 0)
                {
                    btnApprove.Visible = false;
                    gvShow.Columns[0].Visible = false;
                    gvShow.Columns[16].Visible = false;
                    gvShow.Columns[17].Visible = false;
                }
                else { Response.Redirect("EmpPenalties.aspx"); } //Rijwan:17-03-2016

            }

        }
        protected void gvRejectedview_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "View")
            {
                btnApprove.Visible = true;
                tblAdd.Visible = false;
                tblShow.Visible = true;
                tblView.Visible = false;
                int ERID = Convert.ToInt32(e.CommandArgument);
                  
                //Rijwan:18-03-2016
                DataSet ds = AttendanceDAC.HR_EmpPenality_AmtRejected(ERID);
                gvShow.DataSource = ds;
                gvShow.DataBind();
                if (ds.Tables[0].Rows.Count == 0) { Response.Redirect("EmpPenalties.aspx.aspx"); }//Rijwan:17-03-2016

            }
            if (e.CommandName == "Approve")
            {
                int ERID = Convert.ToInt32(e.CommandArgument);


            }

        }

        protected void GetWork(object sender, EventArgs e)
        {
            int Wsid = 0; int StatusWS = 1;
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@Search", txtSearchWorksite.Text);
            param[1] = new SqlParameter("@CompanyID", Convert.ToInt32(Session["CompanyID"]));
            param[2] = new SqlParameter("@WSID", Wsid);
            param[3] = new SqlParameter("@WSStatus", StatusWS);
            // FIllObject.FillDropDown(ref ddlWorksite, "G_GET_WorkSitebyFilter", param);
            FIllObject.FillDropDown(ref ddlWorksite, "HR_GetWorkSite_EmpPenaltiesGS", param);
            ListItem itmSelected = ddlWorksite.Items.FindByText(txtSearchWorksite.Text);
            if (itmSelected != null)
            {
                ddlWorksite.SelectedItem.Selected = false;
                itmSelected.Selected = true;
            }
            ddlWorksite_SelectedIndexChanged(sender, e);
        }

        protected void GetHeadEmp(object sender, EventArgs e)
        {
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@Search", txtSearchEmp.Text);
            param[1] = new SqlParameter("@CompanyID", Convert.ToInt32(Session["CompanyID"]));
            param[2] = new SqlParameter("@WorkSite", Convert.ToInt32(ddlWorksite.SelectedValue));
            param[3] = new SqlParameter("@Dept", Convert.ToInt32(ddlDepartment.SelectedValue));

            FIllObject.FillDropDown(ref ddlEmp, "HR_EmpRepaySearch_EmpPenaltiesGS", param);
            ListItem itmSelected = ddlEmp.Items.FindByText(txtSearchEmp.Text);
            if (itmSelected != null)
            {
                ddlEmp.SelectedItem.Selected = false;
                itmSelected.Selected = true;
            }

        }

        protected void GetDept(object sender, EventArgs e)
        {
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@Search", txtdept.Text);
            param[1] = new SqlParameter("@DeptID", 0);

            FIllObject.FillDropDown(ref ddlDepartment, "G_GET_DesignationbyFilter", param);
            ListItem itmSelected = ddlDepartment.Items.FindByText(txtdept.Text);
            if (itmSelected != null)
            {
                ddlDepartment.SelectedItem.Selected = false;
                itmSelected.Selected = true;
            }
            ddlDepartment_SelectedIndexChanged(sender, e);
            txtFilterEmp.Focus();
        }
        public void GetEmpBySiteDept()
        {
            int WorkSite = Convert.ToInt32(ddlWorksite.SelectedValue);
            ViewState["GSWork"] = Convert.ToInt32(ddlWorksite.SelectedValue);
            WorkSiteID = Convert.ToInt32(ViewState["GSWork"]);
            int Dept = Convert.ToInt32(ddlDepartment.SelectedValue);
            ViewState["GSdept"] = Convert.ToInt32(ddlDepartment.SelectedValue);
            DeptSearch = Convert.ToInt32(ViewState["GSdept"]);


            DataSet ds = AttendanceDAC.HR_EmpRepaySearch(WorkSite, Dept, Convert.ToInt32(Session["CompanyID"]));
            ddlEmp.DataSource = ds;
            ddlEmp.DataTextField = "name";
            ddlEmp.DataValueField = "EmpID";
            ddlEmp.DataBind();
            ddlEmp.Items.Insert(0, new ListItem("---SELECT---", "0", true));
        }
        protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetEmpBySiteDept();
        }
        protected void ddlWorksite_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetEmpBySiteDept();
        }
        protected void gvView_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void gvEdit_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Del")
            {

                int ID = Convert.ToInt32(e.CommandArgument);
                DataTable dtList = (DataTable)ViewState["ReimItems"];
                DataRow[] drSelected = null;
                drSelected = dtList.Select("ID='" + e.CommandArgument + "'");
                if (drSelected.Length > 0)
                    dtList.Rows.Remove(drSelected[0]);
                dtList.AcceptChanges();
                ViewState["ReimItems"] = dtList;
                gvRemiItems.DataSource = dtList;
                gvRemiItems.DataBind();

            }

        }
        protected void BtnEditSave_Click(object sender, EventArgs e)
        {

            string URL = Request.Url.Segments[Request.Url.Segments.Length - 1] + Request.Url.Query;

            foreach (GridViewRow gvRow in gvEdit.Rows)
            {
                DateTime DateOfSpent;
                Label lblEmpID = (Label)gvRow.Cells[2].FindControl("lblEmpID");
                Label lblRItem = (Label)gvRow.Cells[3].FindControl("lblRItemNo");
                DropDownList ddlunits = (DropDownList)gvRow.Cells[4].FindControl("ddlunits");
                int UnitOfMeasure = int.Parse(ddlunits.Text);
                if (UnitOfMeasure == 0)
                {
                    AlertMsg.MsgBox(Page, "Please select units of measure.!", AlertMsg.MessageType.Warning);
                    return;
                }
                TextBox txtRate = (TextBox)gvRow.Cells[5].FindControl("txtRate");
                double UnitRte;// = int.Parse(txtRate.Text);
                try
                {
                    UnitRte = double.Parse(txtRate.Text);

                }
                catch (Exception)
                {

                    AlertMsg.MsgBox(Page, "Unitrate can takes numbers only.!", AlertMsg.MessageType.Warning);
                    return;
                }
                if (txtRate.Text == "" || UnitRte <= 0)
                {
                    AlertMsg.MsgBox(Page, "Please enter proper unitrate.!");
                    return;
                }
                else
                {
                    UnitRte = double.Parse(txtRate.Text);
                }

                TextBox txtQty = (TextBox)gvRow.Cells[6].FindControl("txtQty");
                double Quantity;
                try
                {
                    Quantity = double.Parse(txtQty.Text);

                }
                catch (Exception)
                {

                    AlertMsg.MsgBox(Page, "Quantity can take numbers only.!", AlertMsg.MessageType.Warning);
                    return;
                }
                if (txtQty.Text == "" || Quantity <= 0)
                {
                    AlertMsg.MsgBox(Page, "Please enter proper quntity.!", AlertMsg.MessageType.Warning);
                    return;
                }
                else
                {
                    Quantity = double.Parse(txtQty.Text);

                }
                Label txtAmount = (Label)gvRow.Cells[7].FindControl("txtAmount");

                TextBox txtPurpose = (TextBox)gvRow.Cells[8].FindControl("txtPurpose");
                Label lblRItemID = (Label)gvRow.Cells[9].FindControl("lblRItemNo");
                TextBox txtSpentOn = (TextBox)gvRow.Cells[10].FindControl("txtSpentOn");
                try
                {
                    DateOfSpent = CODEUtility.ConvertToDate(txtSpentOn.Text.Trim(), DateFormat.DayMonthYear);

                }
                catch (Exception)
                {

                    AlertMsg.MsgBox(Page, "Please select proper date.!", AlertMsg.MessageType.Warning);
                    return;
                }

                FileUpload UploadProof = (FileUpload)gvRow.Cells[11].FindControl("UploadProof");

                if (UploadProof.HasFile)
                {
                    DateTime MyDate = DateTime.Now;
                    MyString = MyDate.ToString("ddMMyyhhmmss");
                    extension = System.IO.Path.GetExtension(UploadProof.PostedFile.FileName).ToLower();
                    string storePath = Server.MapPath("~") + "/" + "EmpPenaltyProof/";
                    if (!Directory.Exists(storePath))
                        Directory.CreateDirectory(storePath);
                    UploadProof.PostedFile.SaveAs(storePath + "/" + MyString + extension);
                }
                string Proof = MyString + extension;
                int ReimburseID = Convert.ToInt32(lblRItem.Text), AUID = Convert.ToInt32(ddlunits.SelectedValue), PK = Convert.ToInt32(ViewState["PK"]);
                double Qty = Convert.ToDouble(txtQty.Text), UnitRate = Convert.ToDouble(txtRate.Text);
                string Description = txtPurpose.Text;
                DateOfSpent = CODEUtility.ConvertToDate(txtSpentOn.Text.Trim(), DateFormat.DayMonthYear);
                //Rijwan:16-03-2016
                AttendanceDAC.HR_UpdateEmpPenalityItems(ReimburseID, AUID, Qty, UnitRate, Description, DateOfSpent, Proof, PK);
                if (URL == "EmpPenalties.aspx?key=2")
                {
                    tblShow.Visible = true;
                    
                    int EmpID = Convert.ToInt32(lblEmpID.Text);
                    //Rijwan:18-03-2016
                    DataSet dsShow = AttendanceDAC.HR_Emp_Penalty_AmtPayable(EmpID);
                    gvShow.Visible = true;
                    gvShow.DataSource = dsShow;
                    gvShow.DataBind();
                    tblShow.Visible = true;
                    tblEdit.Visible = false;
                    btnApprove.Visible = true;
                    tblView.Visible = true;
                    gvView.Visible = true;
                    DataSet dsPen = AttendanceDAC.HR_EmpPenalties_NotApproved();
                  
                    BindPagerPending();
                    gvView.Columns[0].Visible = true;
                    gvView.Columns[1].Visible = false;
                    foreach (GridViewRow gvRowP in gvView.Rows)
                    {
                        Label Emp = (Label)gvRowP.FindControl("lblEmpID");
                        int EmpIDP = Convert.ToInt32(Emp.Text);

                        if (EmpIDP == EmpID)
                        {
                            CheckBox chk = new CheckBox();
                            chk = (CheckBox)gvRowP.FindControl("chkApproval");
                            chk.Checked = true;
                        }
                    }

                }
                if (URL == "EmpPenalties.aspx?key=3")
                {
                    int EmpID = Convert.ToInt32(lblEmpID.Text);
                    btnApprove.Visible = true;
                    tblShow.Visible = false;
                    tblShowRej.Visible = true;
                    //Rijwan : 16-03-2016
                   DataSet dsRview = AttendanceDAC.HR_EmpPenality_RejectedView(EmpID);
                    gvShowRej.DataSource = dsRview;
                    gvShowRej.DataBind();
                    tblEdit.Visible = false;
                    tblView.Visible = true;
                    gvView.Visible = true;
                    
                    //Rijwan : 16-03-2016
                    DataSet dsRej = AttendanceDAC.HR_EmpPanalityRejected();
                    
                    BindPagerPending();
                   
                    foreach (GridViewRow gvRowView in gvView.Rows)
                    {
                        Label Emp = (Label)gvRowView.FindControl("lblEmpID");
                        int EmpIDIS = Convert.ToInt32(Emp.Text);

                        if (EmpIDIS == EmpID)
                        {
                            CheckBox chk = new CheckBox();
                            chk = (CheckBox)gvRowView.FindControl("chkRSelect");
                            chk.Checked = true;
                        }
                        if (UploadProof.HasFile)
                        {
                            DateTime MyDate = DateTime.Now;
                            MyString = MyDate.ToString("ddMMyyhhmmss");
                            extension = System.IO.Path.GetExtension(UploadProof.PostedFile.FileName).ToLower();
                            string storePath = Server.MapPath("~") + "/" + "EmpPenaltyProof/";
                            if (!Directory.Exists(storePath))
                                Directory.CreateDirectory(storePath);
                            UploadProof.PostedFile.SaveAs(storePath + "/" + MyString + extension);
                        }
                    }

                }
            }


        }
        public void CalculateAmtEdt()
        {
            foreach (GridViewRow gvRow in gvEdit.Rows)
            {

                TextBox txtRate = (TextBox)gvRow.Cells[5].FindControl("txtRate");
                double UnitRate;
                try
                {
                    UnitRate = double.Parse(txtRate.Text);

                }
                catch (Exception)
                {

                    AlertMsg.MsgBox(Page, "Unitrate can takes numbers only.!", AlertMsg.MessageType.Warning);
                    return;
                }
                if (txtRate.Text == "" || UnitRate <= 0)
                {
                    AlertMsg.MsgBox(Page, "Please enter proper unitrate.!", AlertMsg.MessageType.Warning);
                    return;
                }
                else
                {
                    UnitRate = double.Parse(txtRate.Text);
                }


                TextBox txtQty = (TextBox)gvRow.Cells[6].FindControl("txtQty");
                double Quantity;
                try
                {
                    Quantity = double.Parse(txtQty.Text);

                }
                catch (Exception)
                {

                    AlertMsg.MsgBox(Page, "Quantity can take numbers only.!", AlertMsg.MessageType.Warning);
                    return;
                }
                if (txtQty.Text == "" || Quantity <= 0)
                {
                    AlertMsg.MsgBox(Page, "Please enter proper quntity.!", AlertMsg.MessageType.Warning);
                    return;
                }
                else
                {
                    Quantity = double.Parse(txtQty.Text);

                }

                double Amount = UnitRate * Quantity;
                Label txtAmount = (Label)gvRow.Cells[7].FindControl("txtAmount");
                txtAmount.Text = Convert.ToString(Amount);
                ViewState["Amount"] = Amount;

            }
        }
        protected void txtUnitreteEdit_TextChanged(object sender, EventArgs e)
        {
            CalculateAmtEdt();

        }
        protected void txtQuantityEdit_TextChanged(object sender, EventArgs e)
        {
            CalculateAmtEdt();
        }
        protected void ddlFilterEmp_SelectedIndexChanged(object sender, EventArgs e)
        {
            EmpReimbursementPendingRejPaging.CurrentPage = 1;
            txtFilterEmpID.Text = "";
            int EmpID = Convert.ToInt32(ddlFilterEmp.SelectedValue);
              
            //Rijwan:18-03-2016
         DataSet   ds = AttendanceDAC.HR_Emppenlity_NotApprovedByEmpID(EmpID);
            BindPagerPending();
            
            gvView.Visible = true;
            gvView.Columns[0].Visible = true;
            int Rows = Convert.ToInt32(ds.Tables[0].Rows.Count);
            if (Rows < 1) { btnApprove.Visible = false; }
            else
            {
                btnApprove.Visible = true;
            }
            tblShow.Visible = false;
        }

        protected void btnFilter_Click(object sender, EventArgs e)
        {
            EmpReimbursementPendingRejPaging.CurrentPage = 1;
            ddlFilterEmp.SelectedIndex = -1;
            if (txtFilterEmpID.Text == "")
            {
                if (txtFilterEmp.Text == "" && txtFilterEmpID.Text == "") { AlertMsg.MsgBox(Page, "Please Enter Employee Id/Name", AlertMsg.MessageType.Warning); }
                else
                {
                      
                    string EmpName = txtFilterEmp.Text;

                    DataSet ds = AttendanceDAC.HR_SerchEmp_Reimburse(EmpName, Convert.ToInt32(Session["CompanyID"]));
                    ddlFilterEmp.DataSource = ds.Tables[0];
                    ddlFilterEmp.DataTextField = "name";
                    ddlFilterEmp.DataValueField = "EmpID";
                    ddlFilterEmp.DataBind();
                    ddlFilterEmp.Items.Insert(0, new ListItem("---SELECT EMPLOYEE---", "0", true));
                }
            }
            else
            {
                string URL = Request.Url.Segments[Request.Url.Segments.Length - 1] + Request.Url.Query;
                if (URL == "EmpPenalties.aspx?key=2")
                {

                    int EmpID = Convert.ToInt32(txtFilterEmpID.Text);
                      
                   
                    BindPagerPending();
                    gvView.Visible = true;
                    gvView.Columns[0].Visible = true;
                    btnApprove.Visible = false;
                    tblShow.Visible = false;

                }
                if (URL == "EmpPenalties.aspx?key=3")
                {
                    int EmpID = Convert.ToInt32(txtFilterEmpID.Text);
                      
                    //Rijwan:18-03-2016
                  DataSet  ds = AttendanceDAC.HR_EmpPenlity_RejectedByEmpID(EmpID);
                   
                    BindPagerPending();
                    gvView.Visible = true;
                    gvView.Columns[0].Visible = true;
                    btnApprove.Visible = false;
                    tblShow.Visible = false;
                }
                if (URL == "EmpPenalties.aspx?key=4")
                {
                    //BindPagerPending();
                    int EmpID = Convert.ToInt32(txtFilterEmpID.Text);
                      
                    //Rijwan:18-03-2016
                    DataSet ds = AttendanceDAC.HR_EmpPenlity_TransferdByEmpID(EmpID);
                    //nookesh 02/07/2017
                    //BindPagerTransAmt();
                    gvTransfered.DataSource = ds;
                    gvTransfered.DataBind();
                    gvTransfered.Visible = true;
                    // gvView.Columns[0].Visible = gvView.Columns[1].Visible = false;
                    btnApprove.Visible = false;
                   // tblShow.Visible = false;
                   // tblView.Visible = false; 
                    trACPosting.Visible = true;
                }
            }
        }
        
        protected void ddlTransfer_SelectedIndexChanged(object sender, EventArgs e)
        {
            EmpReimbursementAprovedPaging.CurrentPage = 1;
            int EmpID = Convert.ToInt32(ddlTransfer.SelectedValue);
            txtTranferSearch.Text = "";
            if (ddlTransfer.SelectedIndex == 0)
            {
                BindPager();
            }
            else
            {
                EmployeeApprovedByEmpID(objHrCommon, EmpID);
                  
               
                gvViewApproved.Visible = true;
                tblViewApproved.Visible = true;
                gvViewApproved.Columns[0].Visible = true;
                btnTransferAcc.Visible = false;
            }
        }
        public void EmployeeApprovedByEmpID(HRCommon objHrCommon, int EmpID)
        {

            try
            {
                objHrCommon.PageSize = EmpReimbursementAprovedPaging.ShowRows;
                objHrCommon.CurrentPage = EmpReimbursementAprovedPaging.CurrentPage;
                  
                //Rijwan:18-03-2016
              DataSet  ds = AttendanceDAC.HR_EmpPenlity_EmpTranserByEmpIDByPaging(objHrCommon, EmpID);

                if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0)
                {
                    gvViewApproved.DataSource = ds;

                    EmpReimbursementAprovedPaging.Bind(objHrCommon.CurrentPage, objHrCommon.TotalPages, objHrCommon.NoofRecords, objHrCommon.PageSize);

                }
                else
                {
                    gvViewApproved.EmptyDataText = "No Records Found";
                    EmpReimbursementAprovedPaging.Visible = false;
                }
                gvViewApproved.DataBind();


            }
            catch (Exception e)
            {
                throw e;
            }

        }
        protected void btnTranfer_Click(object sender, EventArgs e)
        {
            EmpReimbursementAprovedPaging.CurrentPage=1;
            if (txtTransferEmpID.Text == "")
            {
                BindPager();
                  
                string EmpName = txtTranferSearch.Text;
                DataSet ds = AttendanceDAC.HR_SerchEmp_Reimburse(EmpName, Convert.ToInt32(Session["CompanyID"]));
                ddlTransfer.DataSource = ds.Tables[0];
                ddlTransfer.DataTextField = "name";
                ddlTransfer.DataValueField = "EmpID";
                ddlTransfer.DataBind();
                ddlTransfer.Items.Insert(0, new ListItem("---SELECT EMPLOYEE---", "0", true));
            }
            else
            {
                ddlTransfer.SelectedIndex = 0;
                int EmpID = Convert.ToInt32(txtTransferEmpID.Text);
                EmployeeApprovedByEmpID(objHrCommon, EmpID);
                  
               
                gvViewApproved.Visible = true;
                gvViewApproved.Columns[0].Visible = true;
                btnTransferAcc.Visible = false;
                if (gvViewApproved.Rows.Count == 0)
                    btnTransferAcc.Enabled = false;
            }
        }
        protected void gvViewApproved_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.Header)
                {
                    CheckBox chk = (CheckBox)e.Row.FindControl("chkSelectAll");
                    chk.Attributes.Add("onclick", String.Format("javascript:return SelectAll(this,'{0}','chkToTransfer');", gvViewApproved.ClientID));
                }
            }
            catch (Exception Ex)
            {
                //report error
            }
        }
        protected void btnTransferAcc_Click(object sender, EventArgs e)
        {
            DataSet dsTransferDetail = new DataSet("TranserDataSet");
            DataTable dtTDT = new DataTable("TranserTable");
            dtTDT.Columns.Add(new DataColumn("CreditAmt", typeof(System.Double)));
            dtTDT.Columns.Add(new DataColumn("DebitAmt", typeof(System.Double)));
            dtTDT.Columns.Add(new DataColumn("Sequence", typeof(System.Int32)));
            dtTDT.Columns.Add(new DataColumn("EmpID", typeof(System.Int32)));
            dtTDT.Columns.Add(new DataColumn("CompanyID", typeof(System.Int32)));
            dtTDT.Columns.Add(new DataColumn("VocherID", typeof(System.Int32)));
            dtTDT.Columns.Add(new DataColumn("WorkSiteId", typeof(System.Int32)));
            dtTDT.Columns.Add(new DataColumn("ERID", typeof(System.Int32)));
            dsTransferDetail.Tables.Add(dtTDT);
            int EmpID = 0;
            Double TotAmt = 0;
            foreach (GridViewRow gvRow in gvViewApproved.Rows)
            {
                CheckBox chk = new CheckBox();
                chk = (CheckBox)gvRow.FindControl("chkToTransfer");
                if (chk.Checked)
                {
                    TotAmt = 0;
                    Label lblEmp = (Label)gvRow.FindControl("lblEmpID");
                    Label lblERID = (Label)gvRow.FindControl("lblERID");
                    Label lblAmount = (Label)gvRow.FindControl("lblAmount");

                    int ERID = Convert.ToInt32(lblERID.Text);
                    AttendanceDAC.HR_EmpPenlitySetStatusTransfered(ERID);
                    EmpID = Convert.ToInt32(lblEmp.Text);
                    double Amt = Convert.ToDouble(lblAmount.Text);
                    TotAmt = TotAmt + Amt;
                    DataSet dsLed = AttendanceDAC.HR_EmpLedger(CompanyID, EmpID);
                    int VocherID = Convert.ToInt32(dsLed.Tables[0].Rows[0]["VocherID"]);
                    int WorkSiteId = Convert.ToInt32(dsLed.Tables[0].Rows[0]["WorkSiteId"]);

                    DataRow dr = dtTDT.NewRow();
                    dr["EmpID"] = EmpID;
                    dr["CompanyID"] = CompanyID;
                    dr["DebitAmt"] = Amt;
                    dr["CreditAmt"] = 0.00000;
                    dr["VocherID"] = VocherID;
                    dr["WorkSiteId"] = WorkSiteId;
                    dr["ERID"] = ERID;
                    if (dtTDT.Rows.Count > 0)
                    {
                        dtTDT.Rows.RemoveAt(0);
                        dtTDT.AcceptChanges();
                    }

                    dtTDT.Rows.Add(dr);

                    string Remarks = "Employee Penality";
                   
                    dsTransferDetail.AcceptChanges();
                    //Rijwan:18-03-2016
                    DataSet ds = AttendanceDAC.HMS_EmpPenlityTranserAccXML(dsTransferDetail, Remarks, TotAmt,  Convert.ToInt32(Session["UserId"]));
                  
                }

            }
            
            Response.Redirect("EmpPenalties.aspx");
        }
        protected void gvTransfered_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "View")
            {
                int ERID = Convert.ToInt32(e.CommandArgument);
                tblShow.Visible = true;
                  
                //Rijwan:18-03-2016
               DataSet ds = AttendanceDAC.HR_EmpPenlity_ViewTransfered(ERID);
                gvShow.DataSource = ds;
                gvShow.DataBind();
                gvShow.Columns[16].Visible = false;
            }

            if (e.CommandName == "Edit")
            {
                int ERID = Convert.ToInt32(e.CommandArgument);

            }
        }

        protected void chkApproval_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkbox = (CheckBox)sender;
            GridViewRow gvr = (GridViewRow)checkbox.NamingContainer;
            Label txt = (Label)gvr.Cells[2].FindControl("lblEmpID");
            tblAdd.Visible = false;
            tblShow.Visible = true;
            tblView.Visible = true;
            int EmpID = Convert.ToInt32(txt.Text);
              
           
            DataSet ds = AttendanceDAC.HR_Emp_Penalty_AmtPayable(EmpID);
            gvShow.DataSource = ds;
            gvShow.DataBind();
            foreach (GridViewRow gvRow in gvView.Rows)
            {
                CheckBox chk = new CheckBox();
                chk = (CheckBox)gvRow.FindControl("chkApproval");
                Label lbl = (Label)gvRow.FindControl("lblEmpID");
                int ERIDToChk = Convert.ToInt32(lbl.Text);
                if (ERIDToChk == EmpID)
                {
                    chk.Checked = true;
                }
                else { chk.Checked = false; }
            }

            if (gvShow.Rows.Count == 0)
            {
                btnApprove.Visible = false;

            }
            else
            {
                btnApprove.Visible = true;
            }
            string url = "EmpPenalties.aspx?key=2";
            int UserId = Convert.ToInt32(Session["UserId"]);
            int RoleId = Convert.ToInt32(Session["RoleId"].ToString());
            int ModuleId = 1;
            SqlParameter[] parm1 = new SqlParameter[3];
            parm1[0] = new SqlParameter("@ModuleId", ModuleId);
            parm1[1] = new SqlParameter("@RoleId", RoleId);
            parm1[2] = new SqlParameter("@URL", url);
            DataSet dsCp = SqlHelper.ExecuteDataset("CP_GetPageAccess", parm1);
            if (dsCp != null && dsCp.Tables.Count > 0 && dsCp.Tables[0].Rows.Count > 0)
            {
                if (dsCp.Tables[0].Rows[0]["Editable"].ToString() == "True")
                    btnApprove.Visible = true;
                else
                    btnApprove.Visible = false;
            }

        }
        protected void chkRSelect_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkbox = (CheckBox)sender;
            GridViewRow gvr = (GridViewRow)checkbox.NamingContainer;
            Label txt = (Label)gvr.Cells[2].FindControl("lblEmpID");
            tblAdd.Visible = false;
            //tblShow.Visible = true;
            tblView.Visible = true;
            tblShow.Visible = false;
            int EmpID = Convert.ToInt32(txt.Text);
              
            //Rijwan:17-03-2016
            DataSet ds = AttendanceDAC.HR_EmpPenality_RejectedView(EmpID);
            gvShowRej.DataSource = ds;
            gvShowRej.DataBind();
            gvShowRej.Visible = true;
            tblShowRej.Visible = true;
            foreach (GridViewRow gvRow in gvView.Rows)
            {
                CheckBox chk = new CheckBox();
                chk = (CheckBox)gvRow.FindControl("chkRSelect");
                Label lbl = (Label)gvRow.FindControl("lblEmpID");
                int ERIDToChk = Convert.ToInt32(lbl.Text);
                if (ERIDToChk == EmpID)
                {
                    chk.Checked = true;
                }
                else { chk.Checked = false; }
            }
            if (gvShowRej.Rows.Count == 0)
            {
                btnApprove.Visible = false;
            }
            else
            {
                btnApprove.Visible = true;
            }

        }

        protected void chkTASelect_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkbox = (CheckBox)sender;
            GridViewRow gvr = (GridViewRow)checkbox.NamingContainer;
            Label txt = (Label)gvr.Cells[1].FindControl("lblERID");
            int ERID = Convert.ToInt32(txt.Text);
            tblShow.Visible = true;
              
            //Rijwan:18-03-2016
            DataSet ds = AttendanceDAC.HR_EmpPenlity_ViewTransfered(ERID);
            gvShow.DataSource = ds;
            gvShow.DataBind();
            btnApprove.Visible = false;
            gvShow.Columns[14].Visible = false;
            gvShow.Columns[15].Visible = false;
            foreach (GridViewRow gvRow in gvTransfered.Rows)
            {
                CheckBox chk = new CheckBox();
                chk = (CheckBox)gvRow.FindControl("chkTASelect");
                Label lbl = (Label)gvRow.FindControl("lblERID");
                int ERIDToChk = Convert.ToInt32(lbl.Text);
                if (ERIDToChk == ERID)
                {
                    chk.Checked = true;
                }
                else { chk.Checked = false; }
            }

        }

        protected void gvShowRej_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.Header)
                {
                    CheckBox chkMail = (CheckBox)e.Row.FindControl("chkSelectAll");
                    chkMail.Attributes.Add("onclick", String.Format("javascript:return SelectAll(this,'{0}','chkSelectOne');", gvShowRej.ClientID));
                }
            }
            catch (Exception Ex)
            {
                //report error
            }
        }
        protected void btnRApprove_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow gvRow in gvView.Rows)
            {
                Label Emp = (Label)gvRow.FindControl("lblEmpID");
                int EmpID = Convert.ToInt32(Emp.Text);
                ViewState["EmpID"] = EmpID;
                CheckBox chk = new CheckBox();
                chk = (CheckBox)gvRow.FindControl("chkRSelect");
                if (chk.Checked)
                {

                    foreach (GridViewRow gvRows in gvShowRej.Rows)
                    {
                        CheckBox chked = new CheckBox();
                        chked = (CheckBox)gvRows.FindControl("chkSelectOne");
                        Label lblERID = (Label)gvRows.FindControl("lblERID");
                        int ERID = Convert.ToInt32(lblERID.Text);
                        Label lblERItemID = (Label)gvRows.FindControl("lblPK");
                        int ERItemID = Convert.ToInt32(lblERItemID.Text);

                        if (chked.Checked)//&& ERID==ChkERID
                        {
                            //Rijwan:17-03-2016
                            AttendanceDAC.HR_EmpPenality_ApproveStatus(ERItemID);
                            if (ERID != Convert.ToInt32(ViewState["ERID"]))
                            {//Rijwan:17-03-2016
                                AttendanceDAC.HR_EmpPenality_UpdateAsApproved( Convert.ToInt32(Session["UserId"]), ERID);
                                ViewState["ERID"] = ERID;
                            }
                        }
                    }

                }
            }
            tblShow.Visible = false;
           
            DataSet dsRefresh = AttendanceDAC.HR_EmpPanalityRejected();// AttendanceDAC.HR_EmpReimNotApprovedByEmpID(Convert.ToInt32(ViewState["EmpID"]));
            
            BindPagerPending();
            gvView.Visible = true;
            gvView.Columns[0].Visible = true;
            if (dsRefresh.Tables[0].Rows.Count == 0)
            {
                btnApprove.Visible = false;
            }


        }
        protected void gvShowRej_RowEditing(object sender, GridViewEditEventArgs e)
        {

        }
        protected void gvShowRej_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {
                btnApprove.Visible = false;
                BindEmpList();
                //BindYears();
                GetWorkSites();
                GetDepartments();
                BindItem();
                tblAdd.Visible = false;
                btnSubmit.Visible = false;
                //tblView.Visible = false;
                //gvView.Visible = false;
                int PK = Convert.ToInt32(e.CommandArgument);
                ViewState["PK"] = PK;
                  
                //ds = AttendanceDAC.HR_EditReimburseItems(PK);
                // pratap date:16-03-2016
              DataSet  ds = AttendanceDAC.HR_Edit_Emp_Penalities_Items(PK);
                ddlEmp.SelectedValue = Convert.ToInt32(ds.Tables[0].Rows[0]["EmpID"]).ToString();
                gvEdit.DataSource = ds;
                gvEdit.DataBind();
                gvRemiItems.Visible = false;
                gvShow.Visible = false;
                tblEdit.Visible = true;
                gvEdit.Visible = true;
                foreach (GridViewRow gvRow in gvShowRej.Rows)
                {
                    CheckBox chk = new CheckBox();
                    chk = (CheckBox)gvRow.FindControl("chkSelectOne");
                    Label lbl = (Label)gvRow.FindControl("lblPK");
                    int ERIDToChk = Convert.ToInt32(lbl.Text);
                    if (ERIDToChk == PK)
                    {
                        chk.Checked = true;
                    }
                    else { chk.Checked = false; }
                }

            }

        }

        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionList(string prefixText, int count, string contextKey)
        {
            DataSet ds = AttendanceDAC.GetWorkSite_EmpPenalties(prefixText, SearchCompanyID);
            return ConvertStingArray(ds);// txtItems.ToArray();
        }

        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionListEmp(string prefixText, int count, string contextKey)
        {
            DataSet ds = AttendanceDAC.HR_EmpRepaySearch_EmpPenalties(prefixText, WorkSiteID, DeptSearch, SearchCompanyID);
            return ConvertStingArray(ds);// txtItems.ToArray();
        }

        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionListDep(string prefixText, int count, string contextKey)
        {
            DataSet ds = AttendanceDAC.GetSearchDesiginationFilter(prefixText, SearchCompanyID, 0);
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
        protected void btnRejReaSave_Click(object sender, EventArgs e)
        {
            string Reason = txtReason.Text;
            int PK = Convert.ToInt32(ViewState["PK"]);
            //Rijwan : 16-03-2016
            AttendanceDAC.HR_EmpPenalityRejectReason(Reason, PK);
            AlertMsg.MsgBox(Page, "Done!", AlertMsg.MessageType.Success);
            tblRejReason.Visible = false;
            btnApprove.Visible = true;

            foreach (GridViewRow gvRow in gvShow.Rows)
            {
                CheckBox chk = new CheckBox();
                chk = (CheckBox)gvRow.FindControl("chkSelectOne");
                if (chk.Checked)
                {
                    Label lblEmp = (Label)gvRow.Cells[3].FindControl("lblEmpID");
                    Label lblPK = (Label)gvRow.Cells[13].FindControl("lblPK");
                    Label lblERID = (Label)gvRow.Cells[14].FindControl("lblERID");
                    int ERID = Convert.ToInt32(lblERID.Text);
                    int EmpID = Convert.ToInt32(lblEmp.Text);
                    AttendanceDAC.HR_EmpPenlity_RejectedStatus(PK,  Convert.ToInt32(Session["UserId"]));
                    DataSet dsref = AttendanceDAC.HR_Emp_Penalty_AmtPayable(EmpID);
                    gvShow.DataSource = dsref;
                    gvShow.DataBind();
                    gvShow.Visible = true;
                    tblAdd.Visible = false;
                    tblShow.Visible = true;
                    tblView.Visible = false;
                      
                    DataSet ds = AttendanceDAC.HR_EmpPenalties_NotApproved();
                    
                    BindPagerPending();
                    gvView.Columns[0].Visible = true;
                    gvView.Columns[1].Visible = false;
                    tblView.Visible = true; gvView.Visible = true;
                    if (gvView.Rows.Count <= 0)
                    {
                        btnApprove.Visible = false;
                    }

                }

            }
        }

        protected void btnapprovegm_Click(object sender, EventArgs e)
        {
            string URL = Request.Url.Segments[Request.Url.Segments.Length - 1] + Request.Url.Query;

            //start
            if (URL == "EmpPenalties.aspx?key=5")
            {

                foreach (GridViewRow gvRows in gvViewApproved.Rows)
                {

                    CheckBox chk = new CheckBox();
                    chk = (CheckBox)gvRows.FindControl("chkToTransfer");
                    if (chk.Checked)
                    {
                        Label Emp = (Label)gvRows.FindControl("lblEmpID");
                        int EmpID = Convert.ToInt32(Emp.Text);
                        ViewState["EmpID"] = EmpID;
                        //foreach (GridViewRow gvRows in gvShow.Rows)
                        //{
                        //    CheckBox chked = new CheckBox();
                        //    chked = (CheckBox)gvRows.FindControl("chkToTransfer");


                        //    if (chked.Checked)//&& ERID==ChkERID
                        //    {
                                Label lblERID = (Label)gvRows.FindControl("lblERID");
                                int ERID = Convert.ToInt32(lblERID.Text);
                                Label lblERItemID = (Label)gvRows.FindControl("lblERItemID");
                                int ERItemID = Convert.ToInt32(lblERItemID.Text);
                                //Rijwan:16-03-2016
                                AttendanceDAC.HR_Emp_Penlity_CFoApproveStatus(ERItemID);
                                if (ERID != Convert.ToInt32(ViewState["ERID"]))
                                {//Rijwan:16-03-2016
                                    AttendanceDAC.HR_EmpPenlity_CFoUpdateAsApproved(Convert.ToInt32(Session["UserId"]), ERID);
                                    ViewState["ERID"] = ERID;
                                    Response.Redirect("EmpPenalties.aspx?key=6");
                                }
                        //    }

                        //}

                    }
                }
                //tblShow.Visible = false;

                //DataSet dsRefresh = AttendanceDAC.HR_EmpPenalties_NotApproved();// AttendanceDAC.HR_EmpReimNotApprovedByEmpID(Convert.ToInt32(ViewState["EmpID"]));
                //BindPagerPending();
                //gvView.Visible = true;
                //gvView.Columns[0].Visible = true;
                //gvView.Columns[1].Visible = false;
                //tblView.Visible = true;
                //tblShow.Visible = true;

                ////Rijwan:16-03-2016
                //DataSet ds = AttendanceDAC.HR_Emp_Penalty_AmtPayable(Convert.ToInt32(ViewState["EmpID"]));
                //gvShow.DataSource = ds;
                //gvShow.DataBind();
                //gvShow.Visible = true;

                //if (ds.Tables[0].Rows.Count == 0)
                //{
                //    btnApprove.Visible = false;
                //}

            }
            if (URL == "EmpPenalties.aspx?key=6")
            {

                foreach (GridViewRow gvRows in gvViewApproved.Rows)
                {

                    CheckBox chk = new CheckBox();
                    chk = (CheckBox)gvRows.FindControl("chkToTransfer");
                    if (chk.Checked)
                    {
                        Label Emp = (Label)gvRows.FindControl("lblEmpID");
                        int EmpID = Convert.ToInt32(Emp.Text);
                        ViewState["EmpID"] = EmpID;
                        //foreach (GridViewRow gvRows in gvShow.Rows)
                        //{
                        //    CheckBox chked = new CheckBox();
                        //    chked = (CheckBox)gvRows.FindControl("chkSelectOne");


                        //    if (chked.Checked)//&& ERID==ChkERID
                        //    {
                                Label lblERID = (Label)gvRows.FindControl("lblERID");
                                int ERID = Convert.ToInt32(lblERID.Text);
                                Label lblERItemID = (Label)gvRows.FindControl("lblERItemID");
                                int ERItemID = Convert.ToInt32(lblERItemID.Text);
                                //Rijwan:16-03-2016
                                AttendanceDAC.HR_EmpPenality_ApproveStatus(ERItemID);
                                if (ERID != Convert.ToInt32(ViewState["ERID"]))
                                {//Rijwan:16-03-2016
                                    AttendanceDAC.HR_EmpPenality_UpdateAsApproved(Convert.ToInt32(Session["UserId"]), ERID);
                                    ViewState["ERID"] = ERID;
                                    Response.Redirect("EmpPenalties.aspx");
                                }
                        //    }

                        //}

                    }
                }
                tblShow.Visible = false;

                DataSet dsRefresh = AttendanceDAC.HR_EmpPenalties_NotApproved();// AttendanceDAC.HR_EmpReimNotApprovedByEmpID(Convert.ToInt32(ViewState["EmpID"]));
                BindPagerPending();
                gvView.Visible = true;
                gvView.Columns[0].Visible = true;
                gvView.Columns[1].Visible = false;
                tblView.Visible = true;
                tblShow.Visible = true;

                //Rijwan:16-03-2016
                DataSet ds = AttendanceDAC.HR_Emp_Penalty_AmtPayable(Convert.ToInt32(ViewState["EmpID"]));
                gvShow.DataSource = ds;
                gvShow.DataBind();
                gvShow.Visible = true;

                if (ds.Tables[0].Rows.Count == 0)
                {
                    btnApprove.Visible = false;
                }

            }
        }

       
    }
}