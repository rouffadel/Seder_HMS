using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using AECLOGIC.HMS.BLL;
using AECLOGIC.ERP.COMMON;
using System.Data.SqlClient;

namespace AECLOGIC.ERP.HMS
{
    public partial class SimRegistration : AECLOGIC.ERP.COMMON.WebFormMaster
    {
        HRCommon objHrCommon = new HRCommon();

        protected override void OnInit(EventArgs e)
        {
            ModuleID = 1;
            base.OnInit(e);
        
            #region Registration
            SimRegistrationPaging.FirstClick += new Paging.PageFirst(EmployeeChangesPaging_FirstClick);
            SimRegistrationPaging.PreviousClick += new Paging.PagePrevious(EmployeeChangesPaging_FirstClick);
            SimRegistrationPaging.NextClick += new Paging.PageNext(EmployeeChangesPaging_FirstClick);
            SimRegistrationPaging.LastClick += new Paging.PageLast(EmployeeChangesPaging_FirstClick);
            SimRegistrationPaging.ChangeClick += new Paging.PageChange(EmployeeChangesPaging_FirstClick);
            SimRegistrationPaging.ShowRowsClick += new Paging.ShowRowsChange(EmployeeChangesPaging_ShowRowsClick);
            SimRegistrationPaging.CurrentPage = 1;
            #endregion Registration

            #region Allotment
            SimRegAllotmentPaging.FirstClick += new Paging.PageFirst(SimRegAllotmentPaging_FirstClick);
            SimRegAllotmentPaging.PreviousClick += new Paging.PagePrevious(SimRegAllotmentPaging_FirstClick);
            SimRegAllotmentPaging.NextClick += new Paging.PageNext(SimRegAllotmentPaging_FirstClick);
            SimRegAllotmentPaging.LastClick += new Paging.PageLast(SimRegAllotmentPaging_FirstClick);
            SimRegAllotmentPaging.ChangeClick += new Paging.PageChange(SimRegAllotmentPaging_FirstClick);
            SimRegAllotmentPaging.ShowRowsClick += new Paging.ShowRowsChange(SimRegAllotmentPaging_ShowRowsClick);
            SimRegAllotmentPaging.CurrentPage = 1;
            #endregion Allotment

        }

        #region Registration
        void EmployeeChangesPaging_ShowRowsClick(object sender, EventArgs e)
        {
            SimRegistrationPaging.CurrentPage = 1;
            BindPager();
        }
        void EmployeeChangesPaging_FirstClick(object sender, EventArgs e)
        {
            if (hdnSearchChange.Value == "1")
                SimRegistrationPaging.CurrentPage = 1;
            BindPager();
            hdnSearchChange.Value = "0";
        }
        void BindPager()
        {

            objHrCommon.PageSize = SimRegistrationPaging.CurrentPage;
            objHrCommon.CurrentPage = SimRegistrationPaging.ShowRows;
            BindConfing(objHrCommon);

        }
        #endregion Registration

        #region Allotment
        void SimRegAllotmentPaging_ShowRowsClick(object sender, EventArgs e)
        {
            SimRegAllotmentPaging.CurrentPage = 1;
            BindPagerAllotment();
        }
        void SimRegAllotmentPaging_FirstClick(object sender, EventArgs e)
        {
            if (hdnSearchChangeAllotment.Value == "1")
                SimRegAllotmentPaging.CurrentPage = 1;
            BindPagerAllotment();
            hdnSearchChangeAllotment.Value = "0";
        }
        void BindPagerAllotment()
        {

            objHrCommon.PageSize = SimRegAllotmentPaging.CurrentPage;
            objHrCommon.CurrentPage = SimRegAllotmentPaging.ShowRows;
            BindAllotedSims(objHrCommon);


        }
        #endregion Allotment

        int mid = 0; bool viewall, Editable; string menuname; string menuid;
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                GetParentMenuId();
                ViewState["PID"] = 0;
                ddlSim.Enabled = true;
                ddlRMProvider.Enabled = true;
                ddlCategory.Enabled = true;
                if (Request.QueryString.Count > 0)
                {
                    int ID = Convert.ToInt32(Request.QueryString["key"].ToString());
                    if (ID == 1)
                    {
                        tblConfigView.Visible = false;
                        tblConfigAdd.Visible = false;
                        tblsimAltmentView.Visible = true;
                        tblSimAltmnt.Visible = false;
                        BindAllotedSims(objHrCommon);
                    }
                    if (ID == 2)
                    {
                        tblConfigView.Visible = false;
                        tblConfigAdd.Visible = false;
                        tblsimAltmentView.Visible = false;
                        tblSimAltmnt.Visible = true;
                    }
                }
                else
                {
                    tblConfigView.Visible = true;
                    tblConfigAdd.Visible = false;
                    tblsimAltmentView.Visible = false;
                    tblSimAltmnt.Visible = false;
                    BindConfing(objHrCommon);

                }
                BindProvider();
                BindWorksite();
                BindRegtdProviders();
                BindSimsStatus();

            }
        }
        void BindConfing(HRCommon objHrCommon)
        {

            try
            {
                Boolean Status;
                objHrCommon.PageSize = SimRegistrationPaging.ShowRows;
                objHrCommon.CurrentPage = SimRegistrationPaging.CurrentPage;
                if (rbStatus.SelectedValue == "1")
                {
                    Status = true;
                }
                else
                {
                    Status = false;
                }
                
            DataSet  ds = AttendanceDAC.HR_GetMobileSimsByPaging(objHrCommon, Status, Convert.ToInt32(Session["CompanyID"]));
            

                if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0)
                {
                    gvSimView.DataSource = ds;

                    SimRegistrationPaging.Bind(objHrCommon.CurrentPage, objHrCommon.TotalPages, objHrCommon.NoofRecords, objHrCommon.PageSize);

                }
                else
                {
                    gvSimView.EmptyDataText = "No Records Found";
                    SimRegistrationPaging.Visible = false;

                }
                gvSimView.DataBind();


            }
            catch (Exception e)
            {
                throw e;
            }
        }
        
        
        void BindAllotedSims(HRCommon objHrCommon)
        {

            try
            {
                objHrCommon.PageSize = SimRegAllotmentPaging.ShowRows;
                objHrCommon.CurrentPage = SimRegAllotmentPaging.CurrentPage;


                DataSet ds = AttendanceDAC.HR_GetEmpSimsByPaging(objHrCommon, Convert.ToInt32(Session["CompanyID"]));

                if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0)
                {
                    gvRSimsView.DataSource = ds;

                    SimRegAllotmentPaging.Bind(objHrCommon.CurrentPage, objHrCommon.TotalPages, objHrCommon.NoofRecords, objHrCommon.PageSize);

                }
                else
                {
                    gvRSimsView.EmptyDataText = "No Records Found";
                    SimRegAllotmentPaging.Visible = false;

                }
                gvRSimsView.DataBind();


            }
            catch (Exception e)
            {
                throw e;
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

               
                btnSave.Enabled = (bool)ds.Tables[0].Rows[0]["Editable"];
                Editable = (bool)ds.Tables[0].Rows[0]["Editable"];
               
                btnSave.Enabled = btnSubmit.Enabled = Editable;
            }
            return MenuId;
        }
        public void BindSimsStatus()
        {
            DataSet ds = AttendanceDAC.HR_GetSimCount();
            lblAlloted.Text = ds.Tables[0].Rows[0][0].ToString();
            lblPending.Text = ds.Tables[1].Rows[0][0].ToString();
        }

        public void BindProvider()
        {
            DataSet ds = AttendanceDAC.HR_GetMobileProviders(txtFilter.Text.Trim());
            ddlProvider.DataSource = ds;
            ddlProvider.DataTextField = "vendor_name";
            ddlProvider.DataValueField = "vendor_id";
            ddlProvider.DataBind();
            ddlProvider.Items.Insert(0, new ListItem("---Select---", "0", true));
        }
        public void BindRegtdProviders()
        {
            DataSet ds = AttendanceDAC.HR_GetRegtdSims();
            ddlRMProvider.DataSource = ds;
            ddlRMProvider.DataTextField = "vendor_name";
            ddlRMProvider.DataValueField = "Provider";
            ddlRMProvider.DataBind();
            ddlRMProvider.Items.Insert(0, new ListItem("---Select---", "0", true));
        }
        public void BindWorksite()
        {
            
            AttendanceDAC ADAC = new AttendanceDAC();
            DataSet ds = AttendanceDAC.GetWorkSite(0, '1', Convert.ToInt32(Session["CompanyID"]));
            ddlws.DataSource = ds.Tables[0];
            ddlws.DataTextField = "Site_Name";
            ddlws.DataTextField = "Site_Name";
            ddlws.DataValueField = "Site_ID";
            ddlws.DataBind();
            ddlws.Items.Insert(0, new ListItem("---Select---", "0", true));
        }

        protected void btnFilter_Click(object sender, EventArgs e)
        {
            
            BindProvider();
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                int PID = Convert.ToInt32(ViewState["PID"]);
                int Provider = Convert.ToInt32(ddlProvider.SelectedValue);
                int Category = Convert.ToInt32(ddlCat.SelectedValue);
                DateTime StartDate = CODEUtility.ConvertToDate(txtSFrom.Text.Trim(), DateFormat.DayMonthYear);
                DateTime? Upto = null;
                if (txtSUpto.Text != "")
                {
                    Upto = CODEUtility.ConvertToDate(txtSUpto.Text.Trim(), DateFormat.DayMonthYear);
                }
                bool Status = ChkStaus.Checked;
                int Worksite = Convert.ToInt32(ddlws.SelectedValue);
                long SimNo = Convert.ToInt64(txtMobileNo.Text);
                int RetSatus = Convert.ToInt32(AttendanceDAC.HR_InsUpMobileSims(PID, Provider, SimNo, Status, Worksite, StartDate, Upto, Category));
                if (RetSatus == 2)
                {
                    AlertMsg.MsgBox(Page, "Already Exists");
                }
                else
                {
                    AlertMsg.MsgBox(Page, "Done");

                }


            
                BindConfing(objHrCommon);

                ViewState["PID"] = 0;
                tblConfigView.Visible = true;
                tblConfigAdd.Visible = false;
            }
            catch (Exception SimReg)
            {
                AlertMsg.MsgBox(Page, SimReg.Message.ToString(),AlertMsg.MessageType.Error);
            }


        }
        protected void gvSimView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int ID = Convert.ToInt32(e.CommandArgument);
            if (e.CommandName == "Edt")
            {
                ViewState["PID"] = ID;
                tblConfigAdd.Visible = true;
                tblConfigView.Visible = false;
                DataSet ds = AttendanceDAC.HR_GetMobileSimByID(ID);
                ddlCat.SelectedValue = ds.Tables[0].Rows[0]["Category"].ToString();
                ddlProvider.SelectedValue = ds.Tables[0].Rows[0]["Provider"].ToString();
                ddlws.SelectedValue = ds.Tables[0].Rows[0]["Worksite"].ToString();
                ChkStaus.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["Status"]);
                txtMobileNo.Text = ds.Tables[0].Rows[0]["SimNo"].ToString();
                txtSFrom.Text = ds.Tables[0].Rows[0]["ServiceFrom1"].ToString();
                txtSUpto.Text = ds.Tables[0].Rows[0]["Upto1"].ToString();
            }
        }
        protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            int ID = Convert.ToInt32(ddlCategory.SelectedValue);
            DataSet ds = AttendanceDAC.HR_GetNotAllottedSims(ID);
            ddlSim.DataSource = ds;
            ddlSim.DataTextField = "SimNo";
            ddlSim.DataValueField = "PID";
            ddlSim.DataBind();
            lblSimcount.Text = ddlSim.Items.Count.ToString() + " Sim(s) found!";
            ddlSim.Items.Insert(0, new ListItem("---Select---", "0", true));
        }
        protected void ddlSim_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindEmployees();
        }

        void BindEmployees()
        {
            long ID = Convert.ToInt64(ddlSim.SelectedItem.Text);
            DataSet ds = AttendanceDAC.HR_GetEmployeesBySite(ID);
            ddlEmptoAllot.DataSource = ds;
            ddlEmptoAllot.DataTextField = "Name";
            ddlEmptoAllot.DataValueField = "EmpID";
            ddlEmptoAllot.DataBind();
            ddlEmptoAllot.Items.Insert(0, new ListItem("---Select---", "0", true));
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                int SID = Convert.ToInt32(ViewState["SID"]);

                long SIMNo = 0;
                if (ddlSim.SelectedItem.Text != "---Select---")
                    SIMNo = Convert.ToInt64(ddlSim.SelectedItem.Text);
                else
                {
                    AlertMsg.MsgBox(Page, "Please select sim.!");
                    return;
                }
                int AllottedTo = 0;
                if (ddlEmptoAllot.SelectedValue != "0")
                    AllottedTo = Convert.ToInt32(ddlEmptoAllot.SelectedValue);
                else
                {
                    AlertMsg.MsgBox(Page, "Please select allotted to.!");
                    return;

                }
                DateTime AllottedFrom = CODEUtility.ConvertToDate(txtAllotedon.Text.Trim(), DateFormat.DayMonthYear);
                DateTime? Upto = null;
                if (txtAllotedUpto.Text != "")
                {
                    Upto = CODEUtility.ConvertToDate(txtAllotedUpto.Text.Trim(), DateFormat.DayMonthYear);
                }
                int UpdatedBy =  Convert.ToInt32(Session["UserId"]);
                double AmountLimit = Convert.ToDouble(txtAmountLimit.Text);
                int Output = AttendanceDAC.HR_InsUpdEmpSims(SID, SIMNo, AllottedTo, AllottedFrom, Upto, UpdatedBy, AmountLimit);
                if (Output == 1)
                    AlertMsg.MsgBox(Page, "Inserted sucessfully.!");
                else if (Output == 2)
                    AlertMsg.MsgBox(Page, "Already exists.!");
                else
                    AlertMsg.MsgBox(Page, "Updated sucessfully.!");
                //AlertMsg.MsgBox(Page, "Done!");
            }
            catch (Exception SimRegi)
            {
                AlertMsg.MsgBox(Page, SimRegi.Message.ToString(),AlertMsg.MessageType.Error);
            }
        }
        protected void addnew_Click(object sender, EventArgs e)
        {
            tblConfigView.Visible = false;
            tblConfigAdd.Visible = true;
            tblsimAltmentView.Visible = false;
            tblSimAltmnt.Visible = false;
            ddlProvider.SelectedIndex = 0;
            ddlCat.SelectedIndex = 0;
            txtMobileNo.Text = "";
            txtSFrom.Text = "";
            txtSUpto.Text = "";
            ddlws.SelectedIndex = 0;
        }
        public string Allotted()
        {
            return "javascript:return window.open('MobileAllottementList.aspx');";
        }
        public string NotAllotted()
        {
            return "javascript:return window.open('MobileAllottementList.aspx?key=1');";
        }

        protected void gvRSimsView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int ID = Convert.ToInt32(e.CommandArgument);
            if (e.CommandName == "Edt")
            {
                try
                {
                    tblSimAltmnt.Visible = true;
                    tblsimAltmentView.Visible = false;
                    DataSet ds = AttendanceDAC.HR_GetEmpSimByID(ID);
                    ViewState["SID"] = ds.Tables[0].Rows[0]["SID"].ToString();
                    ddlRMProvider.SelectedValue = ds.Tables[0].Rows[0]["Provider"].ToString();
                    ddlRMProvider.Enabled = false;
                    ddlCategory.SelectedValue = ds.Tables[0].Rows[0]["Category"].ToString();
                    ddlCategory.Enabled = false;
                    
                    txtAllotedon.Text = ds.Tables[0].Rows[0]["AllottedFrom1"].ToString();
                    txtAllotedUpto.Text = ds.Tables[0].Rows[0]["Upto1"].ToString();
                    txtAmountLimit.Text = ds.Tables[0].Rows[0]["AmountLimit"].ToString();

                    int CID = Convert.ToInt32(ddlCategory.SelectedValue);
                    DataSet dssim = AttendanceDAC.HR_GetNotAllottedSims(CID);//Nookesh:Changed SP name from  HR_GetAllottedSims to HR_GetNotAllottedSims for binding the DDl in SimRegistration.aspx
                    ddlSim.DataSource = dssim;
                    ddlSim.DataTextField = "SimNo";
                    ddlSim.DataValueField = "PID";
                    ddlSim.DataBind();
                    if (ddlSim.Items.FindByValue(ds.Tables[0].Rows[0]["PID"].ToString()) != null)
                    {
                        ddlSim.SelectedValue = ds.Tables[0].Rows[0]["PID"].ToString();
                    }
                    ddlSim.Enabled = false;
                    DataSet dsEmp = AttendanceDAC.HR_GetEmployeesBySimID(Convert.ToInt32(ds.Tables[0].Rows[0]["SID"]));
                    ddlEmptoAllot.DataSource = dsEmp;
                    ddlEmptoAllot.DataTextField = "Name";
                    ddlEmptoAllot.DataValueField = "EmpID";
                    ddlEmptoAllot.DataBind();


                    if (ddlEmptoAllot.Items.FindByValue(ds.Tables[0].Rows[0]["AllottedTo"].ToString()) != null)
                    {
                        ddlEmptoAllot.SelectedValue = ds.Tables[0].Rows[0]["AllottedTo"].ToString();
                    }

                }
                catch { }
            }
        }

        protected void rbStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            SimRegistrationPaging.CurrentPage = 1;
            BindConfing(objHrCommon);

        }



        protected void gvSimView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton lnkEdt = (LinkButton)e.Row.FindControl("lblEdit");
                lnkEdt.Enabled = Editable;
            }
        }
        protected void gvRSimsView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton lnkEdt = (LinkButton)e.Row.FindControl("lnkEdit");
                lnkEdt.Enabled = Editable;
            }
        }

        protected void btnEmpFilter_Click(object sender, EventArgs e)
        {
            SqlParameter[] P = new SqlParameter[2];
            P[0] = new SqlParameter("@EmpName", txtEmpFilter.Text);
            P[1] = new SqlParameter("@Empid",txtEmpidFilter.Text);
            FIllObject.FillDropDown(ref ddlEmptoAllot, "HR_SerchEmp_SimReg", P);
        }
        protected void btnReset_Click(object sender, EventArgs e)
        {
            ddlRMProvider.SelectedIndex = 0;
            ddlCategory.SelectedIndex = 0;
            ddlSim.SelectedIndex = 0;
            ddlEmptoAllot.SelectedIndex = 0;
            txtAllotedon.Text = "";
            txtAllotedUpto.Text = "";
            txtAmountLimit.Text = "";
        }
    }
}
