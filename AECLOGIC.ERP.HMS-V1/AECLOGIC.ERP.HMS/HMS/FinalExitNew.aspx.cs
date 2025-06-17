using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using AECLOGIC.HMS.BLL;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using AECLOGIC.ERP.COMMON;
using AECLOGIC.ERP.HMS.HRClasses;

namespace AECLOGIC.ERP.HMS
{
    public partial class FinalExitNew : AECLOGIC.ERP.COMMON.WebFormMaster
    {
        int mid = 0;
        bool viewall;
        string menuname;
        string menuid;
        bool Editable;
        static int Siteid;
        static int SearchCompanyID;
        static int WSiteid;
        static int EDeptid = 0;
        DataSet ds = new DataSet();
        DataSet ds1 = new DataSet();
        AttendanceDAC objRights = new AttendanceDAC();
        MasterPage objmaster = new MasterPage();
        HRCommon objHrCommon = new HRCommon();

        static int ModID;
        static int Userid;
        protected override void OnInit(EventArgs e)
        {
            ModuleID = 1;
            base.OnInit(e);
            AdvancedLeaveAppPaging.FirstClick += new Paging.PageFirst(AdvancedLeaveAppPaging_FirstClick);
            AdvancedLeaveAppPaging.PreviousClick += new Paging.PagePrevious(AdvancedLeaveAppPaging_FirstClick);
            AdvancedLeaveAppPaging.NextClick += new Paging.PageNext(AdvancedLeaveAppPaging_FirstClick);
            AdvancedLeaveAppPaging.LastClick += new Paging.PageLast(AdvancedLeaveAppPaging_FirstClick);
            AdvancedLeaveAppPaging.ChangeClick += new Paging.PageChange(AdvancedLeaveAppPaging_FirstClick);
            AdvancedLeaveAppPaging.ShowRowsClick += new Paging.ShowRowsChange(AdvancedLeaveAppPaging_ShowRowsClick);
            AdvancedLeaveAppPaging.CurrentPage = 1;



            AdvancedLeaveAppOthPaging.FirstClick += new Paging.PageFirst(AdvancedLeaveAppOthPaging_FirstClick);
            AdvancedLeaveAppOthPaging.PreviousClick += new Paging.PagePrevious(AdvancedLeaveAppOthPaging_FirstClick);
            AdvancedLeaveAppOthPaging.NextClick += new Paging.PageNext(AdvancedLeaveAppOthPaging_FirstClick);
            AdvancedLeaveAppOthPaging.LastClick += new Paging.PageLast(AdvancedLeaveAppOthPaging_FirstClick);
            AdvancedLeaveAppOthPaging.ChangeClick += new Paging.PageChange(AdvancedLeaveAppOthPaging_FirstClick);
            AdvancedLeaveAppOthPaging.ShowRowsClick += new Paging.ShowRowsChange(AdvancedLeaveAppOthPaging_ShowRowsClick);
            AdvancedLeaveAppOthPaging.CurrentPage = 1;
            ModID = ModuleID;

        }
        void AdvancedLeaveAppPaging_ShowRowsClick(object sender, EventArgs e)
        {
            AdvancedLeaveAppPaging.CurrentPage = 1;
            BindPager();
        }
        void AdvancedLeaveAppPaging_FirstClick(object sender, EventArgs e)
        {
            BindPager();
        }


        void AdvancedLeaveAppOthPaging_ShowRowsClick(object sender, EventArgs e)
        {
            AdvancedLeaveAppOthPaging.CurrentPage = 1;
            BindPager();
        }
        void AdvancedLeaveAppOthPaging_FirstClick(object sender, EventArgs e)
        {
            BindPager();
        }



        void BindPager()
        {
            if (Request.QueryString.Count > 0)
            {
                if (Convert.ToInt32(Request.QueryString["key"].ToString()) == 2)
                {
                    objHrCommon.PageSize = AdvancedLeaveAppOthPaging.CurrentPage;
                    objHrCommon.CurrentPage = AdvancedLeaveAppOthPaging.ShowRows;
                    BindOtherEmp(objHrCommon);
                }
            }
            else
            {
                objHrCommon.PageSize = AdvancedLeaveAppPaging.CurrentPage;
                objHrCommon.CurrentPage = AdvancedLeaveAppPaging.ShowRows;
                BindLeaveDetails(objHrCommon);
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {

            try
            {
                try
                {
                    string id = Session["UserId"].ToString();
                }
                catch
                {
                    Response.Redirect("Home.aspx");
                }
                //ddlEmp.Items.Insert(0, new ListItem("---Select---", "0"));

                topmenu.MenuId = GetParentMenuId();
                topmenu.ModuleId = ModuleID; ;
                topmenu.RoleID = Convert.ToInt32(Session["RoleId"].ToString());
                topmenu.SelectedMenu = Convert.ToInt32(mid.ToString());
                SearchCompanyID = Convert.ToInt32(Session["CompanyID"]);
                topmenu.DataBind();
                Session["menuname"] = menuname;
                Session["menuid"] = menuid;
                Userid = Convert.ToInt32(Session["UserId"].ToString());

                if (!IsPostBack)
                {

                    // ViewState["AppliedOn"] = DateTime.Now.ToString(ConfigurationManager.AppSettings["DateFormat"]);
                    ViewState["AppliedOn"] = DateTime.Now.ToString("dd MMM yyyy");
                    ViewState["LID"] = 0;
                    ViewState["ReqLeaves"] = 0;
                    ViewState["TotalLeaves"] = 0;
                    ViewState["Status"] = 0;
                    BindEmpList();
                    //BindWorkSites();
                    //BindDepartments();
                    //BindEmployees();
                    BindYears();
                    BindLeaveTypes();
                    if (Request.QueryString.Count > 0)
                    {
                        int id = Convert.ToInt32(Request.QueryString["key"].ToString());
                        if (id == 1)
                        {
                            int EmpId = Convert.ToInt32(Session["UserId"]);
                            ViewState["EmpID"] = EmpId;
                            tblmain.Visible = true;
                            tblView.Visible = false;
                            tblReply.Visible = false;
                            BindGrid(EmpId);
                            dvLeaveApply.Visible = false;
                        }
                        else if (id == 2)
                        {
                            dvLeaveApply.Visible = true;
                            tblmain.Visible = false;
                            //BindEmployyes();
                            tblView.Visible = false;
                            BindWorkSites();
                            BindDepartments();
                            BindPager();
                            dvgr.Visible = true;

                        }

                    }
                    else
                    {
                        int EmpID = Convert.ToInt32(Session["UserId"]);
                        ViewState["EmpID"] = EmpID;
                        //tblView.Visible = true;
                        BindPager();
                        ////BindLeaveDetails(EmpID);
                        tblmain.Visible = false;
                        tblReply.Visible = false;
                        dvLeaveApply.Visible = false;

                        //BindLeaveTypes();
                    }
                    try
                    {

                        ViewState["WSID"] = 0;
                        if (Convert.ToInt32(Session["RoleId"].ToString()) == 7)
                        {
                            try
                            {

                                //string Worksit = Session["Site"].ToString();
                                DataSet ds = clViewCPRoles.HR_DailyAttStatus(Convert.ToInt32(Session["UserId"]));
                                ViewState["WSID"] = ds.Tables[0].Rows[0]["ID"].ToString();
                                ddlWS.SelectedValue = ds.Tables[0].Rows[0]["ID"].ToString();
                                ddlWS.Enabled = false;
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
                clsErrorLog.HMSEventLog(ex, "AdvancedLeaveApplication", "Page_Load", "001");

            }
        }
        public void BindLeaveTypes()
        {
            ds = Leaves.GetTypeofLeavesList();



            ddlSumLeaveType.DataSource = ds;
            ddlSumLeaveType.DataTextField = "Name";
            ddlSumLeaveType.DataValueField = "LeaveType";
            ddlSumLeaveType.DataBind();
            ddlSumLeaveType.Items.Insert(0, new ListItem("--Select--", "0"));



        }

        public DataSet BindGrdLeaveTypes()
        {
            ds = Leaves.GetTypeofLeavesList();
            return ds;
        }

        //added by pratap date: 25-03-2016
        //changes the SP here from T_G_Leaves_GetTypeOfLeavesList_BasedOnGender to T_G_Leaves_GetTypeOfLeavesList_BasedOnGender_Employeeportal
        //for loding the Leaves and update them as soon as Leaves applyed
        public DataSet BindGrdLeaveTypesBasedOnGender(int EmpID)
        {
            ds = Leaves.T_G_Leaves_GetTypeOfLeavesList_BasedOnGender_Employeeportal(EmpID);
            return ds;
        }

        public void BindLeaveDetails(HRCommon objHrCommon)
        {

            try
            {
                int EmpID = 0; int WSID = 0;
                objHrCommon.PageSize = AdvancedLeaveAppPaging.ShowRows;
                objHrCommon.CurrentPage = AdvancedLeaveAppPaging.CurrentPage;
                if (txtEmp.Text == "")
                {
                    EmpID = Convert.ToInt32(Session["UserId"]);
                }
                else
                {
                    EmpID = Convert.ToInt32(txtEmp.Text.Substring(0, 4));
                }
                if (txtworksiteemp.Text != "")
                {
                    WSID = Convert.ToInt32(txtworksiteemp.Text.Substring(0, 4));
                }
                int Year = Convert.ToInt32(ddlYear.SelectedValue);
                int Month = Convert.ToInt32(ddlMonth.SelectedValue);
                try
                {

                    if (Convert.ToInt32(ViewState["WSID"]) > 0)
                    {
                        objHrCommon.SiteID = Convert.ToInt32(ViewState["WSID"]);
                        WSID = Convert.ToInt32(ViewState["WSID"]);
                    }

                }
                catch { }
                DataSet ds = new DataSet();
                ds = AttendanceDAC.HR_GetLeaveDetailsByPaging(objHrCommon, EmpID, WSID, Month, Year, Convert.ToInt32(Session["CompanyID"]));
                if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0)
                {
                    gvLeaves.DataSource = ds;
                    AdvancedLeaveAppPaging.Visible = true;
                }
                else
                {
                    gvLeaves.EmptyDataText = "No Records Found";
                    AdvancedLeaveAppPaging.Visible = false;

                }
                gvLeaves.DataBind();
                AdvancedLeaveAppPaging.Bind(objHrCommon.CurrentPage, objHrCommon.TotalPages, objHrCommon.NoofRecords, objHrCommon.PageSize);

            }
            catch (Exception e)
            {
                throw e;
            }
        }
        //public void BindLeaveTypes()
        //{
        //    DataSet ds2 = new DataSet();
        //    ds2 = AttendanceDAC.HR_GetLeaveTypes();
        //    ddlL1.DataSource = ds2.Tables[0];
        //    ddlL1.DataValueField = "LeaveType";
        //    ddlL1.DataTextField = "ShortName";
        //    ddlL1.DataBind();

        //    ddlL2.DataSource = ds2.Tables[0];
        //    ddlL2.DataValueField = "LeaveType";
        //    ddlL2.DataTextField = "ShortName";
        //    ddlL2.DataBind();
        //}


        //if (!objMaster.ViewAll)
        //    Worksite = Convert.ToInt32(Session["CostCenterID"]);
        public void BindYears()
        {
            DataSet ds = new DataSet();
            ds = AttendanceDAC.GetCalenderYear();

            int i = 0;
            int Maxyear = Convert.ToInt32(ds.Tables[0].Rows[0]["MaxYear"].ToString());
            for (int Minyear = Convert.ToInt32(ds.Tables[0].Rows[0]["MinYear"].ToString()); Minyear <= Maxyear; Minyear++)
            {
                ddlYear.Items.Insert(0, new ListItem(Convert.ToString(Minyear), Convert.ToString(Minyear)));
                i = i + 1;
            }

            ddlMonth.SelectedValue = ds.Tables[0].Rows[0]["CurrentMonth"].ToString();
            if (ddlYear.Items.FindByValue(ds.Tables[0].Rows[0]["CurrentYear"].ToString()) != null)
            {
                ddlYear.Items.FindByValue(ds.Tables[0].Rows[0]["CurrentYear"].ToString()).Selected = true;
            }
            else
            {
                ddlYear.SelectedIndex = ddlYear.Items.Count - 1;
            }

            #region set defalult month and year

            //if (ds.Tables[0].Rows[0]["PreviousMonth"].ToString() == "0")
            //{
            //    ddlMonth.SelectedValue = "1";
            //}
            //else
            //{
            //    ddlMonth.SelectedIndex = Convert.ToInt32(ds.Tables[0].Rows[0]["PreviousMonth"].ToString() + 1);
            //}
            //ddlYear.Items.FindByValue(ds.Tables[0].Rows[0]["CurrentYear"].ToString()).Selected = true;

            #endregion set defalult month and year

        }

        public int GetParentMenuId()
        {
            string URL = Request.Url.Segments[Request.Url.Segments.Length - 1] + Request.Url.Query;
            int RoleId = Convert.ToInt32(Session["RoleId"].ToString());
            int ModuleId = ModuleID; ;

            DataSet ds = new DataSet();

            ds = AttendanceDAC.GetAllowed(RoleId, ModuleId, URL);
            int MenuId = 0;
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                MenuId = Convert.ToInt32(ds.Tables[0].Rows[0]["Under"]);
                ViewState["Editable"] = Editable = (bool)ds.Tables[0].Rows[0]["Editable"];
                ViewState["ViewAll"] = (bool)ds.Tables[0].Rows[0]["ViewAll"];
                viewall = (bool)ViewState["ViewAll"];

                menuname = ds.Tables[0].Rows[0]["menuname"].ToString();
                menuid = MenuId.ToString();
                mid = Convert.ToInt32(ds.Tables[0].Rows[0]["MenuId"].ToString());
                btnSubmit.Enabled = btnsave.Enabled = btnReply.Enabled = Editable;
                btnSubmit.Visible = btnsave.Visible = btnReply.Visible = Editable;
            }
            return MenuId;
        }
        //public void BindLeaveDetails(int EmpID)
        //{
        //    DataSet ds = new DataSet();
        //    ds = AttendanceDAC.HR_GetLeaveDetails(EmpID);
        //    gvLeaves.DataSource = ds;
        //    gvLeaves.DataBind();
        //}
        public void BindGrid(int? EmpID)
        {
            ds1 = AttendanceDAC.GetAvalableLeaves_LC(EmpID);
            if (ds1.Tables.Count > 0)
            {
                if (Convert.ToInt32(Request.QueryString["key"].ToString()) == 1)
                {
                    gvAvailLeaves.DataSource = ds1.Tables[0];
                    gvAvailLeaves.DataBind();
                    gvEMPID.DataSource = ds1.Tables[1];
                    gvEMPID.DataBind();
                }
                else if (Convert.ToInt32(Request.QueryString["key"].ToString()) == 2)
                {
                    gdvAttend.DataSource = ds1;
                    gdvAttend.DataBind();
                }
            }

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                AdvancedLeaveAppOthPaging.CurrentPage = 1;
                BindPager();
            }
            catch (Exception ex)
            {
                clsErrorLog.HMSEventLog(ex, "AdvancedLeaveApplication", "btnSearch_Click", "002");

            }
        }
        public void BindOtherEmp(HRCommon objHrCommon)
        {
            try
            {

                objHrCommon.PageSize = AdvancedLeaveAppOthPaging.ShowRows;
                objHrCommon.CurrentPage = AdvancedLeaveAppOthPaging.CurrentPage;

                int? EmpID = null;
                if (ddlEmp.SelectedIndex != 0)
                    EmpID = Convert.ToInt32(ddlEmp.SelectedValue);
                int? WS = null;
                if (ddlWS.SelectedIndex != 0)
                    WS = Convert.ToInt32(ddlWS.SelectedValue);
                int? Dept = null;
                if (ddlDept.SelectedIndex != 0)
                    Dept = Convert.ToInt32(ddlDept.SelectedValue);
                DataSet ds = new DataSet();
                try
                {

                    if (Convert.ToInt32(ViewState["WSID"]) > 0)
                    {
                        objHrCommon.SiteID = Convert.ToInt32(ViewState["WSID"]);
                        WS = Convert.ToInt32(ViewState["WSID"]);
                    }

                }
                catch { }
                ds = AttendanceDAC.GetAvalableLeavesByPaging(objHrCommon, EmpID, WS, Dept, Convert.ToInt32(Session["CompanyID"]));
                Session["DataSet"] = ds;
                if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0)
                {
                    gdvAttend.DataSource = ds;
                    gdvAttend.DataBind();
                    AdvancedLeaveAppOthPaging.Visible = btnsave.Visible = true;
                }
                else
                {
                    AdvancedLeaveAppOthPaging.Visible = btnsave.Visible = false;
                    gdvAttend.DataSource = null;
                    gdvAttend.DataBind();
                }
                AdvancedLeaveAppOthPaging.Bind(objHrCommon.CurrentPage, objHrCommon.TotalPages, objHrCommon.NoofRecords, objHrCommon.PageSize);

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void BindDepartments()
        {
            try
            {
                DataSet ds = new DataSet();
                ds = (DataSet)objRights.GetDaprtmentList();
                ViewState["Departments"] = ds;
                //if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0)
                //{
                //    ddlDept.DataValueField = "DepartmentUId";
                //    ddlDept.DataTextField = "DeptName";
                //    ddlDept.DataSource = ds;
                //    ddlDept.DataBind();
                //    ddlDept.Items.Insert(0, new ListItem("---ALL---", "0"));
                //}
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
                DataSet ds = new DataSet();
                ds = AttendanceDAC.GetWorkSite(0, '1', Convert.ToInt32(Session["CompanyID"]));
                ViewState["WorkSites"] = ds;

                FIllObject.FillDropDown(ref ddlWS, "HR_GetWorkSite_By_GetAvailableLeaves");

                //if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0)
                //{
                //    ddlWS.DataSource = ds.Tables[0];
                //    ddlWS.DataTextField = "Site_Name";
                //    ddlWS.DataValueField = "Site_ID";
                //    ddlWS.DataBind();
                //}
                //ddlWS.Items.Insert(0, new ListItem("---ALL---", "0"));

            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public void BindEmployyes()
        {
            int? EmpID = null;
            if (!(bool)ViewState["ViewAll"])
                EmpID = Convert.ToInt32(Session["UserId"]);

            BindGrid(EmpID);

        }

        int EmpID;
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                int ReqLeaves;
                ViewState["LID"] = 0;
                if (rbdays.Checked)
                {
                    ReqLeaves = Convert.ToInt32(txtReqLeaves.Text);
                    forDays.Visible = true;
                    forPeriod.Visible = false;
                    NotAllowDays.Visible = true;
                    NotAllowforPeriod.Visible = false;
                }
                else
                {
                    //CodeUtil.ConvertToDate_ddMMMyyy(txtGrantedFrom.Text.Trim(), CodeUtil.DateFormat.ddMMMyyyy);
                    // if (DateTime.ParseExact(txtStPrd.Text, "dd MMM yyyy", System.Globalization.CultureInfo.InvariantCulture) <= DateTime.ParseExact(txtEndPrd.Text, "dd MMM yyyy", System.Globalization.CultureInfo.InvariantCulture))
                    if (CodeUtil.ConvertToDate_ddMMMyyy(txtStPrd.Text, CodeUtil.DateFormat.ddMMMyyyy) <= CodeUtil.ConvertToDate_ddMMMyyy(txtEndPrd.Text, CodeUtil.DateFormat.ddMMMyyyy))
                    {
                        // TimeSpan T = DateTime.ParseExact(txtEndPrd.Text, "dd MMM yyyy", System.Globalization.CultureInfo.InvariantCulture) - DateTime.ParseExact(txtStPrd.Text, "dd MMM yyyy", System.Globalization.CultureInfo.InvariantCulture);
                        TimeSpan T = CodeUtil.ConvertToDate_ddMMMyyy(txtEndPrd.Text, CodeUtil.DateFormat.ddMMMyyyy) - CodeUtil.ConvertToDate_ddMMMyyy(txtStPrd.Text, CodeUtil.DateFormat.ddMMMyyyy);
                        ReqLeaves = Convert.ToInt32(T.TotalDays + 1);
                        txtNoofDays.Text = ReqLeaves.ToString();
                        txtRsnleaves.Text = ReqLeaves.ToString();
                        forDays.Visible = false;
                        forPeriod.Visible = true;
                        NotAllowDays.Visible = false;
                        NotAllowforPeriod.Visible = true;
                    }
                    else
                    {
                        AlertMsg.MsgBox(Page, "Select Proper Dates");

                        return;
                    }



                }
                ViewState["ReqLeaves"] = ReqLeaves;
                // lblCheck.Visible = false;
                if (Convert.ToInt32(Request.QueryString["key"].ToString()) == 1)
                    EmpID = Convert.ToInt32(ViewState["EmpID"]);
                //else
                //{
                //    if (ddlEmp.SelectedIndex != 0)
                //        EmpID = Convert.ToInt32(ddlEmp.SelectedValue);
                //}



                ds1 = AttendanceDAC.GetAvlLeavesCount(EmpID, Convert.ToInt32(ddlSumLeaveType.SelectedItem.Value));
                if (ds1.Tables.Count > 0)
                {
                    gvAvailLeaves.DataSource = ds1;
                    int tot = 0;
                    if (ds1.Tables[0].Rows.Count > 0)
                    {
                        tot = Convert.ToInt32(ds1.Tables[0].Rows[0]["LeaveCount"]);
                    }
                    ViewState["TotalLeaves"] = tot;

                    if (tot == 0)
                    {
                        AlertMsg.MsgBox(this.Page, "You Have No Leaves! even though u want leaves, these leaves go to LOP ");
                        tblNotAllowed.Visible = true;
                        pnltblNotAllow.Visible = true;
                        tblAllowed.Visible = false;
                        pnltblAllowed.Visible = false;
                    }
                    if (ReqLeaves > tot)
                    {
                        AlertMsg.MsgBox(this.Page, "You Have No Sufficient Leaves! If u want all required leaves, the extra leaves go to LOP ");
                        tblNotAllowed.Visible = true;
                        pnltblNotAllow.Visible = true;
                        tblAllowed.Visible = false;
                        pnltblAllowed.Visible = false;
                    }
                    else
                    {
                        AlertMsg.MsgBox(this.Page, "You Have Sufficient Leaves to Apply!");
                        tblAllowed.Visible = true;
                        pnltblAllowed.Visible = true;
                        tblNotAllowed.Visible = false;
                        pnltblNotAllow.Visible = false;
                        // btnCheck.Visible = true;
                        //start


                        //if (ReqLeaves <= Leave1)
                        //{
                        //    Leave1 = Leave1 - ReqLeaves;
                        //}
                        //else
                        //{
                        //    int adj = Leave1;
                        //    int ReqL = ReqLeaves - adj;
                        //}
                        //end
                    }
                }

                if (Convert.ToInt32(Request.QueryString["key"].ToString()) == 2)
                {
                    dvgr.Visible = true;
                    tblNotAllowed.Visible = false;
                    pnltblNotAllow.Visible = false;
                }
            }
            catch (Exception ex)
            {
                clsErrorLog.HMSEventLog(ex, "AdvancedLeaveApplication", "btnSubmit_Click", "003");

            }
        }

        protected void checkLeaves()
        {



        }
        //protected void btnCheck_Click(object sender, EventArgs e)
        //{
        //    int L1 = Convert.ToInt32(txtL1.Text);
        //    int L2 = Convert.ToInt32(txtL2.Text);
        //    int L3 = L1 + L2;
        //    if (L3 <= Convert.ToInt32(ViewState["TotalLeaves"]) && L1 <= Convert.ToInt32(ViewState["CL"]))
        //    {
        //        // lblCheck.Text = " Allowed! ";
        //        //AlertMsg.MsgBox(this.Page, "Allowed! ");
        //        int Leave1 = Convert.ToInt32(ddlL1.SelectedValue);
        //        int Leave2 = Convert.ToInt32(ddlL2.SelectedValue);
        //        DataSet ds3 = new DataSet();
        //        //lblCheck.Visible = true;
        //        ds3 = AttendanceDAC.GetHR_LeaveAcceptance(Leave1, Leave2);
        //        int leaveResult = Convert.ToInt32(ds3.Tables[0].Rows[0][0]);
        //        if (leaveResult == 0)
        //        {
        //            AlertMsg.MsgBox(this.Page, "Leave Combination not Allowed!");
        //            // lblCheck.Text = "Leave Combination not Allowed!";
        //        }
        //        else
        //        {
        //            btnApply.Visible = true;
        //            AlertMsg.MsgBox(this.Page, "Allowed");

        //            //lblCheck.Text = " Allowed! ";
        //        }
        //    }
        //    else
        //    {
        //        AlertMsg.MsgBox(this.Page, "Please Check CL or Total Leaves! ");
        //    }
        //}
        string SendUC;
        int LID;
        protected void btnApply_Click(object sender, EventArgs e)
        {
            try
            {
                try
                {
                    if (Convert.ToInt32(ViewState["LID"]) == 0)
                    {
                        DateTime LeaveFrom, LeaveUntil;
                        if (rbdays.Checked)
                        {
                            //CodeUtil.ConvertToDate_ddMMMyyy(txtGrantedFrom.Text.Trim(), CodeUtil.DateFormat.ddMMMyyyy);

                            LeaveFrom = CodeUtil.ConvertToDate_ddMMMyyy(txtAFrom.Text.Trim(), CodeUtil.DateFormat.ddMMMyyyy);
                            LeaveUntil = CodeUtil.ConvertToDate_ddMMMyyy(txtAUntil.Text.Trim(), CodeUtil.DateFormat.ddMMMyyyy);
                            //LeaveFrom = CodeUtil.ConvertToDate_ddMMMyyy(txtAFrom.Text.Trim(), CodeUtil.DateFormat.ddMMMyyyy);
                            //  LeaveUntil = CodeUtil.ConvertToDate_ddMMMyyy(txtAUntil.Text.Trim(), CodeUtil.DateFormat.ddMMMyyyy);
                        }
                        else
                        {
                            LeaveFrom = CodeUtil.ConvertToDate_ddMMMyyy(txtStPrd.Text.Trim(), CodeUtil.DateFormat.ddMMMyyyy);
                            LeaveUntil = CodeUtil.ConvertToDate_ddMMMyyy(txtEndPrd.Text.Trim(), CodeUtil.DateFormat.ddMMMyyyy);
                        }
                        if (LeaveFrom <= LeaveUntil)
                        {
                            int EmpID = Convert.ToInt32(Session["UserId"]);
                            //TimeSpan span = LeaveUntil.Subtract(LeaveFrom);
                            //int totdays = span.Days + 1;
                            int appdays;
                            if (txtReqLeaves.Text != "" || txtReqLeaves.Text != string.Empty)
                            {
                                appdays = Convert.ToInt32(txtReqLeaves.Text);
                            }
                            else
                            {
                                appdays = Convert.ToInt32(txtNoofDays.Text);
                            }

                            int totdays = AttendanceDAC.LeavesDaysCount(EmpID, LeaveFrom, LeaveUntil);


                            if (totdays > appdays)
                            {
                                AlertMsg.MsgBox(this.Page, "Check No.Of Leaves you have Entered!");
                            }
                            else
                            {
                                //LeaveFrom = CodeUtil.ConvertToDate_ddMMMyyy(txtAFrom.Text.Trim(), CodeUtil.DateFormat.ddMMMyyyy);
                                //LeaveUntil = CodeUtil.ConvertToDate_ddMMMyyy(txtAUntil.Text.Trim(), CodeUtil.DateFormat.ddMMMyyyy);
                                string Applied = ViewState["AppliedOn"].ToString();
                                DateTime AppliedOn = CodeUtil.ConvertToDate_ddMMMyyy(Applied, CodeUtil.DateFormat.ddMMMyyyy);
                                // DateTime AppliedOn = CodeUtil.ConvertToDate_ddMMMyyy(Applied, CodeUtil.DateFormat.DayMonthYear);
                                objHrCommon.LID = Convert.ToInt32(ViewState["LID"]);
                                int OutPut = AttendanceDAC.HR_InsUpLeaveApplication(objHrCommon, Convert.ToInt32(Session["UserId"]), AppliedOn, LeaveFrom, LeaveUntil, txtApReson.Text, Convert.ToInt32(ViewState["Status"]), Convert.ToInt32(ddlSumLeaveType.SelectedItem.Value));
                                LID = objHrCommon.LID;
                                if (OutPut == 1)
                                {
                                    if (LID > 0)
                                    {
                                        EmpID = Convert.ToInt32(Session["UserId"]);
                                        DataSet Sentds = AttendanceDAC.HR_SMS_LeaveApplicatonSent(EmpID);
                                        if (Sentds != null && Sentds.Tables.Count > 0 && Sentds.Tables[0].Rows.Count > 0)
                                        {
                                            int n = 0;
                                            if (Sentds != null && Sentds.Tables.Count > 0 && Sentds.Tables[0].Rows.Count > 0)
                                                n = Sentds.Tables[0].Columns.Count;
                                            for (int i = 0; i < n; i++)
                                            {
                                                string SentMble = Sentds.Tables[0].Rows[0][i].ToString();
                                                string EmpName = ds.Tables[0].Rows[0]["EmpName"].ToString();
                                                string FrmDate = LeaveFrom.ToString("ddMMyy");
                                                string EDate = LeaveUntil.ToString("ddMMyy");
                                                SendUC = "LAID: " + LID + " , " + EmpName + " " + FrmDate + "-" + EDate + " , " + txtApReson.Text;
                                                AECLOGIC.ERP.HMS.SMSService.ServiceSoapClient FResp = new AECLOGIC.ERP.HMS.SMSService.ServiceSoapClient();
                                                string JobCode = FResp.SendTextSMS("yudhi", "159487", SentMble.Trim(), SendUC, "AECERP");
                                                if (i == 2)
                                                {
                                                    AttendanceDAC.SMSUpDateLAJobCode(LID, JobCode);
                                                }
                                            }
                                        }

                                    }
                                    AlertMsg.MsgBox(Page, "Done");
                                }
                                else if (OutPut == 2)
                                    AlertMsg.MsgBox(Page, "Already applied in between these days");
                                else
                                    AlertMsg.MsgBox(Page, "Updated.!");
                                tblmain.Visible = false;
                                tblAllowed.Visible = false;
                                pnltblAllowed.Visible = false;
                                tblView.Visible = true;
                            }

                            BindPager();
                            //BindLeaveDetails(Convert.ToInt32(Session["UserId"]));
                            //Response.Redirect("AdvancedLeaveApplication.aspx");
                        }

                        else
                        {
                            AlertMsg.MsgBox(Page, "Check Dates");
                        }
                    }
                    else
                    {
                        DateTime LeaveFrom = CodeUtil.ConvertToDate_ddMMMyyy(txtAFrom.Text.Trim(), CodeUtil.DateFormat.ddMMMyyyy);
                        DateTime LeaveUntil = CodeUtil.ConvertToDate_ddMMMyyy(txtAUntil.Text.Trim(), CodeUtil.DateFormat.ddMMMyyyy);
                        if (LeaveFrom < LeaveUntil)
                        {

                            string Applied = ViewState["AppliedOn"].ToString();
                            //DateTime AppliedOn = CodeUtil.ConvertToDate_ddMMMyyy(Applied, CodeUtil.DateFormat.DayMonthYear);
                            DateTime AppliedOn = CodeUtil.ConvertToDate_ddMMMyyy(Applied, CodeUtil.DateFormat.ddMMMyyyy);
                            objHrCommon.LID = Convert.ToInt32(ViewState["LID"]);
                            int OutPut = AttendanceDAC.HR_InsUpLeaveApplication(objHrCommon, Convert.ToInt32(Session["UserId"]), AppliedOn, LeaveFrom, LeaveUntil, txtReason.Text, Convert.ToInt32(ViewState["Status"]), Convert.ToInt32(ddlSumLeaveType.SelectedItem.Value));
                            if (OutPut == 1)
                            {
                                if (LID > 0)
                                {
                                    EmpID = Convert.ToInt32(Session["UserId"]);
                                    DataSet Sentds = AttendanceDAC.HR_SMS_LeaveApplicatonSent(EmpID);
                                    if (Sentds != null && Sentds.Tables.Count > 0 && Sentds.Tables[0].Rows.Count > 0)
                                    {
                                        int n = 0;
                                        if (Sentds != null && Sentds.Tables.Count > 0)
                                            n = Sentds.Tables[0].Columns.Count;
                                        for (int i = 0; i < n; i++)
                                        {
                                            string SentMble = Sentds.Tables[0].Rows[0][i].ToString();
                                            string EmpName = ds.Tables[0].Rows[0]["EmpName"].ToString();
                                            string FrmDate = LeaveFrom.ToString("ddMMyy");
                                            string EDate = LeaveUntil.ToString("ddMMyy");
                                            SendUC = "LAID: " + LID + " , " + EmpName + " " + FrmDate + "-" + EDate + " , " + txtApReson.Text;
                                            AECLOGIC.ERP.HMS.SMSService.ServiceSoapClient FResp = new AECLOGIC.ERP.HMS.SMSService.ServiceSoapClient();
                                            string JobCode = FResp.SendTextSMS("yudhi", "159487", SentMble.Trim(), SendUC, "AECERP");
                                            if (i == 2)
                                            {
                                                AttendanceDAC.SMSUpDateLAJobCode(LID, JobCode);
                                            }
                                        }
                                    }
                                }
                                AlertMsg.MsgBox(Page, "Done");
                            }
                            else if (OutPut == 2)
                                AlertMsg.MsgBox(Page, "Aleready applied in between these days");
                            else
                                AlertMsg.MsgBox(Page, "Updated.!");
                            tblmain.Visible = false;
                            tblAllowed.Visible = false;
                            pnltblAllowed.Visible = false;
                            tblView.Visible = true;
                            BindPager();

                            //BindLeaveDetails(Convert.ToInt32(Session["UserId"]));
                            //Response.Redirect("AdvancedLeaveApplication.aspx");
                        }

                        else
                        {
                            AlertMsg.MsgBox(Page, "Check Dates");
                        }
                    }
                    //Response.Redirect("AdvancedLeaveApplication.aspx");
                }
                catch (Exception ex)
                {
                    AlertMsg.MsgBox(Page, ex.Message.ToString());
                }
            }
            catch (Exception ex)
            {
                clsErrorLog.HMSEventLog(ex, "AdvancedLeaveApplication", "btnApply_Click", "004");

            }

        }

        protected void GetEmployeeSearch(object sender, EventArgs e)
        {
            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@Search", txtSearchEmp.Text);
            param[1] = new SqlParameter("@CompanyID", SearchCompanyID);
            param[2] = new SqlParameter("@WS", ddlWS.SelectedValue);
            param[3] = new SqlParameter("@Status", "Y");
            param[4] = new SqlParameter("@Dept", ddlDept.SelectedValue);
            FIllObject.FillDropDown(ref ddlEmp, "HR_GoogleSerac_SearchEmpBySiteDept", param);


            ListItem itmSelected = ddlEmp.Items.FindByText(txtSearchEmp.Text);
            if (itmSelected != null)
            {
                ddlEmp.SelectedItem.Selected = false;
                itmSelected.Selected = true;
            }
            if (txtSearchEmp.Text != "") { ddlEmp.SelectedIndex = 1; } //ddlEmp.SelectedIndex = 1; 

        }

        protected void btnRequest_Click(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToInt32(ViewState["LID"]) == 0)
                {
                    DateTime LeaveFrom = CodeUtil.ConvertToDate_ddMMMyyy(txtNAFrom.Text.Trim(), CodeUtil.DateFormat.ddMMMyyyy);
                    DateTime LeaveUntil = CodeUtil.ConvertToDate_ddMMMyyy(txtNAUntil.Text.Trim(), CodeUtil.DateFormat.ddMMMyyyy);

                    if (LeaveFrom <= LeaveUntil)
                    {
                        TimeSpan span = LeaveUntil.Subtract(LeaveFrom);
                        int totdays = span.Days + 1;
                        int appdays = Convert.ToInt32(txtReqLeaves.Text);
                        if (totdays > appdays)
                        {
                            AlertMsg.MsgBox(this.Page, "Check No.Of Leaves u have Entered!");
                        }
                        else
                        {
                            string Applied = ViewState["AppliedOn"].ToString();
                            DateTime AppliedOn = CodeUtil.ConvertToDate_ddMMMyyy(Applied, CodeUtil.DateFormat.ddMMMyyyy);
                            objHrCommon.LID = Convert.ToInt32(ViewState["LID"]);
                            int OutPut = AttendanceDAC.HR_InsUpLeaveApplication(objHrCommon, Convert.ToInt32(Session["UserId"]), AppliedOn, LeaveFrom, LeaveUntil, txtReason.Text, Convert.ToInt32(ViewState["Status"]), Convert.ToInt32(ddlSumLeaveType.SelectedItem.Value));
                            if (OutPut == 1)
                            {
                                if (LID > 0)
                                {
                                    try
                                    {
                                        EmpID = Convert.ToInt32(Session["UserId"]);
                                        DataSet Sentds = AttendanceDAC.HR_SMS_LeaveApplicatonSent(EmpID);
                                        int n = 0;
                                        if (Sentds != null && Sentds.Tables.Count > 0 && Sentds.Tables[0].Rows.Count > 0)
                                            n = Sentds.Tables[0].Columns.Count;
                                        for (int i = 0; i < n; i++)
                                        {
                                            string SentMble = Sentds.Tables[0].Rows[0][i].ToString();
                                            string EmpName = ds.Tables[0].Rows[0]["EmpName"].ToString();
                                            string FrmDate = LeaveFrom.ToString("ddMMyy");
                                            string EDate = LeaveUntil.ToString("ddMMyy");
                                            SendUC = "LAID: " + LID + " , " + EmpName + " " + FrmDate + "-" + EDate + " , " + txtApReson.Text;
                                            AECLOGIC.ERP.HMS.SMSService.ServiceSoapClient FResp = new AECLOGIC.ERP.HMS.SMSService.ServiceSoapClient();
                                            string JobCode = FResp.SendTextSMS("yudhi", "159487", SentMble.Trim(), SendUC, "AECERP");
                                            if (i == 2)
                                            {
                                                AttendanceDAC.SMSUpDateLAJobCode(LID, JobCode);
                                            }
                                        }
                                    }
                                    catch { }
                                }

                                AlertMsg.MsgBox(Page, "Leave applied !");
                                Response.Redirect("AdvancedLeaveApplication.aspx");
                            }
                            else if (OutPut == 2)
                                AlertMsg.MsgBox(Page, "Already leave applied between these dates.!");
                            else
                                AlertMsg.MsgBox(Page, "Updated !");
                            BindPager();
                            ////BindLeaveDetails(Convert.ToInt32(Session["UserId"]));
                        }
                    }
                    else
                    {
                        AlertMsg.MsgBox(Page, "Check Dates");
                    }

                }
                else
                {

                    DateTime LeaveFrom = CodeUtil.ConvertToDate_ddMMMyyy(txtNAFrom.Text.Trim(), CodeUtil.DateFormat.ddMMMyyyy);
                    DateTime LeaveUntil = CodeUtil.ConvertToDate_ddMMMyyy(txtNAUntil.Text.Trim(), CodeUtil.DateFormat.ddMMMyyyy);
                    if (LeaveFrom <= LeaveUntil)
                    {
                        string Applied = ViewState["AppliedOn"].ToString();
                        DateTime AppliedOn = CodeUtil.ConvertToDate_ddMMMyyy(Applied, CodeUtil.DateFormat.ddMMMyyyy);
                        objHrCommon.LID = Convert.ToInt32(ViewState["LID"]);
                        int Output = AttendanceDAC.HR_InsUpLeaveApplication(objHrCommon, Convert.ToInt32(Session["UserId"]), AppliedOn, LeaveFrom, LeaveUntil, txtReason.Text, Convert.ToInt32(ViewState["Status"]), Convert.ToInt32(ddlSumLeaveType.SelectedItem.Value));
                        if (Output == 1)
                        {
                            if (LID > 0)
                            {
                                EmpID = Convert.ToInt32(Session["UserId"]);
                                DataSet Sentds = AttendanceDAC.HR_SMS_LeaveApplicatonSent(EmpID);
                                int n = 0;
                                if (Sentds != null && Sentds.Tables.Count > 0 && Sentds.Tables[0].Rows.Count > 0)
                                    n = Sentds.Tables[0].Columns.Count;
                                for (int i = 0; i < n; i++)
                                {
                                    string SentMble = Sentds.Tables[0].Rows[0][i].ToString();
                                    string EmpName = ds.Tables[0].Rows[0]["EmpName"].ToString();
                                    string FrmDate = LeaveFrom.ToString("ddMMyy");
                                    string EDate = LeaveUntil.ToString("ddMMyy");
                                    SendUC = "LAID: " + LID + " , " + EmpName + " " + FrmDate + "-" + EDate + " , " + txtApReson.Text;
                                    AECLOGIC.ERP.HMS.SMSService.ServiceSoapClient FResp = new AECLOGIC.ERP.HMS.SMSService.ServiceSoapClient();
                                    string JobCode = FResp.SendTextSMS("yudhi", "159487", SentMble.Trim(), SendUC, "AECERP");
                                    if (i == 2)
                                    {
                                        AttendanceDAC.SMSUpDateLAJobCode(LID, JobCode);
                                    }
                                }
                            }
                            AlertMsg.MsgBox(Page, "Done!.");
                        }
                        else if (Output == 2)
                            AlertMsg.MsgBox(Page, "Already leave applied between these dates.!");
                        else
                            AlertMsg.MsgBox(Page, "Updated sucessfully.!");
                        BindPager();
                        ////BindLeaveDetails(Convert.ToInt32(Session["UserId"]));
                        Response.Redirect("AdvancedLeaveApplication.aspx");
                    }
                    else
                    {
                        AlertMsg.MsgBox(Page, "Check Dates");
                    }
                }

            }
            catch (Exception AdvLeaApp)
            {
                AlertMsg.MsgBox(Page, AdvLeaApp.Message.ToString());
            }
        }

        public void SMSLeave(string Message, string strMobile)
        {
            AECLOGIC.ERP.HMS.SMSService.ServiceSoapClient FResp = new AECLOGIC.ERP.HMS.SMSService.ServiceSoapClient();
            string JobCode = FResp.SendTextSMS("yudhi", "159487", strMobile.Trim(), Message, "AECERP");
        }

        protected string FormatInput(object Status)
        {
            string retValue = "";
            string input = Status.ToString();
            if (input == "-1")
            {
                retValue = "--SELECT--";
            }
            if (input == "0")
            {
                retValue = "In-Process";
                //retValue = "Applied";     
            }
            if (input == "1")
            {
                retValue = "In-Process";
            }
            if (input == "2")
            {
                retValue = "Granted";
            }
            if (input == "3")
            {
                retValue = "Rejected";
            }
            return retValue;
        }
        protected void gvLeaves_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                try
                {
                    if (e.CommandName == "edt")
                    {
                        int LID = Convert.ToInt32(e.CommandArgument);
                        int EmpID = Convert.ToInt32(Session["UserId"]);
                        ViewState["LID"] = LID;
                        DataSet ds = new DataSet();
                        ds = AttendanceDAC.HR_GetLeaveDetailsByID(LID);
                        txtAFrom.Text = ds.Tables[0].Rows[0]["LeaveFrom"].ToString();//Convert.ToDateTime(ds.Tables[0].Rows[0]["LeaveFrom"]).ToString(ConfigurationManager.AppSettings["DateFormat"]);
                        txtAUntil.Text = ds.Tables[0].Rows[0]["LeaveUntil"].ToString();//Convert.ToDateTime(ds.Tables[0].Rows[0]["LeaveUntil"]).ToString(ConfigurationManager.AppSettings["DateFormat"]);
                        txtNAFrom.Text = ds.Tables[0].Rows[0]["LeaveFrom"].ToString();//Convert.ToDateTime(ds.Tables[0].Rows[0]["LeaveFrom"]).ToString(ConfigurationManager.AppSettings["DateFormat"]);
                        txtNAUntil.Text = ds.Tables[0].Rows[0]["LeaveUntil"].ToString();//Convert.ToDateTime(ds.Tables[0].Rows[0]["LeaveUntil"]).ToString(ConfigurationManager.AppSettings["DateFormat"]);
                        txtReason.Text = ds.Tables[0].Rows[0]["Reason"].ToString();
                        if (txtReason.Text == "" && txtReason.Text == string.Empty)
                        {
                            tblNotAllowed.Visible = false;
                            pnltblNotAllow.Visible = false;
                            tblAllowed.Visible = true;
                            pnltblAllowed.Visible = true;
                        }
                        else
                        {
                            tblNotAllowed.Visible = true;
                            pnltblNotAllow.Visible = true;
                            tblAllowed.Visible = false;
                            pnltblAllowed.Visible = false;
                        }
                        ViewState["Status"] = ds.Tables[0].Rows[0]["Status"].ToString();
                        ViewState["AppliedOn"] = ds.Tables[0].Rows[0]["AppliedOn"].ToString();//Convert.ToDateTime(ds.Tables[0].Rows[0]["AppliedOn"]).ToString(ConfigurationManager.AppSettings["DateFormat"]);
                        //tblAllowed.Visible = true;
                        tblView.Visible = false;
                        tblReply.Visible = false;
                        BindPager();
                        ////BindLeaveDetails(Convert.ToInt32(Session["UserId"]));
                        //ViewState["LID"] = 0;
                    }
                    if (e.CommandName == "Del")
                    {
                        int LID = Convert.ToInt32(e.CommandArgument);
                        int EmpID = Convert.ToInt32(Session["UserId"]);
                        AttendanceDAC.HR_DelLeaveApplication(LID);
                        BindPager();
                        ////BindLeaveDetails(Convert.ToInt32(Session["UserId"]));
                        tblReply.Visible = false;
                    }
                    if (e.CommandName == "Rep")
                    {
                        int LID = Convert.ToInt32(e.CommandArgument);
                        ViewState["LID"] = LID;
                        DataSet ds = new DataSet();
                        ds = AttendanceDAC.HR_LeaveAppReply(LID);
                        txtReply.Text = ds.Tables[0].Rows[0][0].ToString();
                        tblReply.Visible = true;
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            catch (Exception ex)
            {
                clsErrorLog.HMSEventLog(ex, "AdvancedLeaveApplication", "gvLeaves_RowCommand", "005");

            }
        }

        protected void GetWork(object sender, EventArgs e)
        {

            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@Search", txtSearchWorksite.Text);
            FIllObject.FillDropDown(ref ddlWS, "HR_GetWorkSite_By_GetAvailableLeavesFilter", param);
            ListItem itmSelected = ddlWS.Items.FindByText(txtSearchWorksite.Text);
            if (itmSelected != null)
            {
                ddlWS.SelectedItem.Selected = false;
                itmSelected.Selected = true;
            }
            ddlWS_SelectedIndexChanged1(sender, e);
            txtdept.Text = "";
        }

        protected void GetDept(object sender, EventArgs e)
        {
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@Search", txtdept.Text);
            param[1] = new SqlParameter("@CompanyID", SearchCompanyID);
            param[2] = new SqlParameter("@SiteID", Siteid);

            FIllObject.FillDropDown(ref ddlDept, "HR_GetDepartmentBySiteFilter", param);
            ListItem itmSelected = ddlDept.Items.FindByText(txtdept.Text);
            if (itmSelected != null)
            {
                ddlDept.SelectedItem.Selected = false;
                itmSelected.Selected = true;
            }
            if (txtdept.Text != "") { ddlDept.SelectedIndex = 1; }
            EDeptid = Convert.ToInt32(ddlDept.SelectedItem.Value);
        }
        protected void btnReply_Click(object sender, EventArgs e)
        {
            try
            {
                string CommentReply = txtReply.Text;
                AttendanceDAC.HR_LeaveAppEmpReply(CommentReply, Convert.ToInt32(ViewState["LID"]));
                BindPager();
                ////BindLeaveDetails(Convert.ToInt32(Session["UserId"]));
                tblReply.Visible = false;
            }
            catch (Exception RplyToHR)
            {
                AlertMsg.MsgBox(Page, RplyToHR.Message.ToString());
            }

        }

        #region GrantLeavesfor others

        //public void BindWorkSites()
        //{

        //    try
        //    {
        //        DataSet ds = new DataSet();
        //        ds = objRights.GetWorkSite(0, '1');
        //        if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0)
        //        {
        //            ddlWS.DataSource = ds.Tables[0];
        //            ddlWS.DataTextField = "Site_Name";
        //            ddlWS.DataValueField = "Site_ID";
        //            ddlWS.DataBind();
        //            ddlWS.Items.Insert(0, new ListItem("---ALL---", "0"));

        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        throw e;
        //    }
        //}
        //public void BindDepartments()
        //{
        //    try
        //    {
        //        DataSet ds = new DataSet();
        //        ds = (DataSet)objRights.GetDaprtmentList();
        //        if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0)
        //        {
        //            ddlDept.DataValueField = "DepartmentUId";
        //            ddlDept.DataTextField = "DepartmentName";
        //            ddlDept.DataSource = ds;
        //            ddlDept.DataBind();
        //            ddlDept.Items.Insert(0, new ListItem("---ALL---", "0"));

        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        throw e;
        //    }
        //}

        //public void BindEmployees()
        //{
        //    try
        //    {
        //        int? WS = null;
        //        int? Dept = null;
        //        if (ddlWS.SelectedIndex != 0)
        //            WS = Convert.ToInt32(ddlWS.SelectedValue);
        //        if (ddlDept.SelectedIndex != 0)
        //            Dept = Convert.ToInt32(ddlDept.SelectedValue);
        //        DataSet ds = new DataSet();
        //        ds = (DataSet)objRights.GetEmpByWSAndDept(WS, Dept);
        //        if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0)
        //        {
        //            ddlEmp.DataValueField = "EmpID";
        //            ddlEmp.DataTextField = "Name";
        //            ddlEmp.DataSource = ds;
        //            ddlEmp.DataBind();
        //            ddlEmp.Items.Insert(0, new ListItem("---Select---", "0"));

        //        }
        //        else
        //        {
        //            ddlEmp.Items.Clear();
        //            ddlEmp.Items.Insert(0, new ListItem("---Select---", "0"));
        //            if (ddlEmp.SelectedIndex == 0)
        //                tblmain.Visible = false;

        //        }

        //    }
        //    catch (Exception e)
        //    {
        //        throw e;
        //    }
        //}

        #endregion GrantLeavesfor others


        protected void ddlDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            //BindEmployees();
        }
        protected void ddlWS_SelectedIndexChanged(object sender, EventArgs e)
        {
            //BindEmployees();
            WSiteid = Convert.ToInt32(ddlWS.SelectedValue);

        }
        protected void ddlEmp_SelectedIndexChanged(object sender, EventArgs e)
        {
            //int EmpID = Convert.ToInt32(ddlEmp.SelectedValue);
            //if (ddlEmp.SelectedIndex != 0)
            //    tblmain.Visible = true;
            //else
            //    tblmain.Visible = false;

            //BindGrid(EmpID);
            //dvgr.Visible = false;
            //lblEnterleaves.Visible = false;
            ////lblName.Text = Convert.ToString(ddlEmp.SelectedItem.Text);
            //tblmain.Visible = false;
            //dvgr.Visible = true;
            //if (ddlEmp.SelectedIndex == 0)
            //    dvgr.Visible = false;



        }
        protected void btnsave_Click(object sender, EventArgs e)
        {
            // place a validation for leaves by pratap date: 16-04-2016
            try
            {
                foreach (GridViewRow gvr in gdvAttend.Rows)
                {
                    CheckBox chk = (CheckBox)gvr.FindControl("chkAll");
                    if (chk.Checked)
                    {
                        Label lblEmpID = (Label)gvr.Cells[1].FindControl("lblEmpID");
                        DropDownList grdddlLeaveType = (DropDownList)gvr.Cells[10].FindControl("grdddlLeaveType");
                        Label lblApplyDays = (Label)gvr.Cells[10].FindControl("txtNoofDays");
                        double balLeaves = 0;
                        if (grdddlLeaveType != null)
                        {
                            string str = grdddlLeaveType.SelectedItem.ToString();
                            int stratPOS = str.IndexOf('(');
                            if (stratPOS > 0)
                            {
                                int EndPOS = str.IndexOf(')');
                                balLeaves = Convert.ToDouble(str.Substring(stratPOS + 1, (EndPOS - stratPOS - 1)).ToString());
                                //AlertMsg.MsgBox(Page, balLeaves.ToString());
                                if ((Convert.ToInt32(lblApplyDays.Text)) > balLeaves)
                                {
                                    string Message = "EmpID: " + lblEmpID.Text + "  " + str.Substring(0, stratPOS) + " balance  is not sufficient";
                                    AlertMsg.MsgBox(Page, Message);
                                    return;
                                }
                            }
                        }
                    }
                }

                try
                {
                    foreach (GridViewRow gvr in gdvAttend.Rows)
                    {
                        CheckBox chk = (CheckBox)gvr.FindControl("chkAll");

                        if (chk.Checked)
                        {
                            int LID = Convert.ToInt32(ViewState["LID"]);
                            int GrantedBy = Convert.ToInt32(Session["UserId"]);
                            Label lblEmpId = (Label)gvr.Cells[1].FindControl("lblEmpID");
                            int EmpID = int.Parse(lblEmpId.Text);
                            TextBox txtAppOn = (TextBox)gvr.Cells[3].FindControl("txtAppOn");
                            if (txtAppOn.Text != "")
                            {
                                try
                                {
                                    // CodeUtil.ConvertToDate_ddMMMyyy(txtGrantedFrom.Text.Trim(), CodeUtil.DateFormat.ddMMMyyyy);
                                    DateTime AppliedOn = CodeUtil.ConvertToDate_ddMMMyyy(txtAppOn.Text, CodeUtil.DateFormat.ddMMMyyyy);
                                    TextBox txtGrntFrm = (TextBox)gvr.Cells[4].FindControl("txtGrsntFrm");
                                    if (txtGrntFrm.Text != "")
                                    {
                                        try
                                        {
                                            DateTime GrantedFrom = CodeUtil.ConvertToDate_ddMMMyyy(txtGrntFrm.Text, CodeUtil.DateFormat.ddMMMyyyy);

                                            TextBox txtLeaveFrm = (TextBox)gvr.Cells[4].FindControl("txtGrsntFrm");
                                            DateTime LeaveFrom = CodeUtil.ConvertToDate_ddMMMyyy(txtLeaveFrm.Text, CodeUtil.DateFormat.ddMMMyyyy);

                                            TextBox txtGrntUtl = (TextBox)gvr.Cells[5].FindControl("txtGrsntUtl");



                                            if (txtGrntUtl.Text != "")
                                            {
                                                try
                                                {
                                                    DateTime GrantedUntil = CodeUtil.ConvertToDate_ddMMMyyy(txtGrntUtl.Text, CodeUtil.DateFormat.ddMMMyyyy);

                                                    TextBox txtLeaveUtl = (TextBox)gvr.Cells[5].FindControl("txtGrsntUtl");
                                                    DateTime LeaveUntil = CodeUtil.ConvertToDate_ddMMMyyy(txtLeaveUtl.Text, CodeUtil.DateFormat.ddMMMyyyy);

                                                    if (LeaveFrom <= LeaveUntil)
                                                    {
                                                        FileUpload UploadProof1 = (FileUpload)gvr.Cells[6].FindControl("UploadProof");
                                                        String MyString = string.Empty;
                                                        string extension = string.Empty;
                                                        if (UploadProof1.HasFile)
                                                        {
                                                            DateTime MyDate = DateTime.Now;
                                                            MyString = MyDate.ToString("ddMMyyhhmmss");
                                                            string Filename = UploadProof1.PostedFile.FileName.ToLower();
                                                            extension = System.IO.Path.GetExtension(UploadProof1.PostedFile.FileName).ToLower();
                                                            //string storePath = Server.MapPath("~") + "/" + "LeaveApplications/" + Convert.ToInt32(Session["UserId"]);
                                                            // here changed the userid to empid date:23-04-2016
                                                            string storePath = Server.MapPath("~/hms") + "/" + "LeaveApplications/" + Convert.ToInt32(EmpID);
                                                            if (!Directory.Exists(storePath))
                                                                Directory.CreateDirectory(storePath);
                                                            UploadProof1.PostedFile.SaveAs(storePath + "/" + MyString + extension);
                                                        }
                                                        string Proof = MyString + extension;

                                                        TextBox txtReason = (TextBox)gvr.Cells[7].FindControl("txtReason");
                                                        DropDownList grdddlLeaveType = (DropDownList)gvr.Cells[10].FindControl("grdddlLeaveType");

                                                        // AttendanceDAC.InsLeavepermission(LID, EmpID, AppliedOn, LeaveFrom, LeaveUntil, GrantedFrom, GrantedUntil, GrantedBy, Proof);


                                                        //int OutPut = AttendanceDAC.HR_InsUpLeaveApplication(objHrCommon, EmpID, AppliedOn, LeaveFrom, LeaveUntil, txtReason.Text, Convert.ToInt32(ViewState["Status"]), Convert.ToInt32(grdddlLeaveType.SelectedItem.Value));
                                                        //By Ravitheja for proof saving in Table..
                                                        int OutPut = AttendanceDAC.HR_InsUpLeaveApplication_SaveProof(objHrCommon, EmpID, AppliedOn, LeaveFrom, LeaveUntil, txtReason.Text, Convert.ToInt32(ViewState["Status"]), Convert.ToInt32(grdddlLeaveType.SelectedItem.Value), Proof);
                                                        if (OutPut == 1)
                                                        {
                                                            if (LID > 0)
                                                            {
                                                                EmpID = Convert.ToInt32(Session["UserId"]);
                                                                DataSet Sentds = AttendanceDAC.HR_SMS_LeaveApplicatonSent(EmpID);
                                                                int n = 0;
                                                                if (Sentds != null && Sentds.Tables.Count > 0 && Sentds.Tables[0].Rows.Count > 0)
                                                                    n = Sentds.Tables[0].Columns.Count;
                                                                for (int i = 0; i < n; i++)
                                                                {
                                                                    string SentMble = Sentds.Tables[0].Rows[0][i].ToString();
                                                                    string EmpName = ds.Tables[0].Rows[0]["EmpName"].ToString();
                                                                    string FrmDate = LeaveFrom.ToString("ddMMyy");
                                                                    string EDate = LeaveUntil.ToString("ddMMyy");
                                                                    SendUC = "LAID: " + LID + " , " + EmpName + " " + FrmDate + "-" + EDate + " , " + txtApReson.Text;
                                                                    AECLOGIC.ERP.HMS.SMSService.ServiceSoapClient FResp = new AECLOGIC.ERP.HMS.SMSService.ServiceSoapClient();
                                                                    string JobCode = FResp.SendTextSMS("yudhi", "159487", SentMble.Trim(), SendUC, "AECERP");
                                                                    if (i == 2)
                                                                    {
                                                                        AttendanceDAC.SMSUpDateLAJobCode(LID, JobCode);
                                                                    }
                                                                }
                                                            }

                                                            AlertMsg.MsgBox(Page, "Leave applied !");

                                                        }
                                                        else if (OutPut == 2)
                                                            AlertMsg.MsgBox(Page, "Already leave applied between these dates.!");

                                                        BindPager();








                                                        chk.Checked = false;
                                                        txtGrntFrm.Text = "";
                                                        txtGrntUtl.Text = "";

                                                    }
                                                    else
                                                    {
                                                        AlertMsg.MsgBox(Page, "Check Dates");
                                                    }


                                                }
                                                catch (Exception)
                                                {
                                                    AlertMsg.MsgBox(Page, "Please select proper date.!");
                                                    return;
                                                }
                                            }
                                            else
                                            {
                                                AlertMsg.MsgBox(Page, "Please select granted untill.!");
                                                return;
                                            }

                                        }   //for granted frm
                                        catch (Exception)   //for granted from
                                        {
                                            AlertMsg.MsgBox(Page, "Please select  date.!");
                                            return;
                                        }
                                    }
                                    else
                                    {
                                        AlertMsg.MsgBox(Page, "Please select granted from.!");
                                        return;
                                    }
                                }   //try for applied on
                                catch (Exception)  //for applied on
                                {
                                    AlertMsg.MsgBox(Page, "Please select proper date.!");
                                    return;
                                }
                            }
                            else
                            {
                                AlertMsg.MsgBox(Page, "Please select appliedon.!");
                                return;
                            }
                        }
                    }

                }
                catch (Exception ex1)
                {
                    AlertMsg.MsgBox(Page, ex1.Message.ToString());
                }
            }
            catch (Exception ex)
            {
                clsErrorLog.HMSEventLog(ex, "AdvancedLeaveApplication", "btnsave_Click", "006");

            }
        }


        protected void gdvAttend_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "sub")
                {

                    int LID = Convert.ToInt32(ViewState["LID"]);
                    int GrantedBy = Convert.ToInt32(Session["UserId"]);


                    GridViewRow gvr = (GridViewRow)((LinkButton)e.CommandSource).Parent.Parent;

                    Label lablEmpId = (Label)gdvAttend.Rows[gvr.RowIndex].FindControl("lblEmpID");
                    int EmpID = int.Parse(lablEmpId.Text);

                    TextBox txtAppOn = (TextBox)gdvAttend.Rows[gvr.RowIndex].FindControl("txtAppOn");
                    DateTime AppliedOn = CodeUtil.ConvertToDate_ddMMMyyy(txtAppOn.Text, CodeUtil.DateFormat.ddMMMyyyy);

                    TextBox txtGrntFrm = (TextBox)gdvAttend.Rows[gvr.RowIndex].FindControl("txtGrsntFrm");
                    DateTime GrantedFrom = CodeUtil.ConvertToDate_ddMMMyyy(txtGrntFrm.Text, CodeUtil.DateFormat.ddMMMyyyy);

                    TextBox txtLeaveFrm = (TextBox)gdvAttend.Rows[gvr.RowIndex].FindControl("txtGrsntFrm");
                    DateTime LeaveFrom = CodeUtil.ConvertToDate_ddMMMyyy(txtLeaveFrm.Text, CodeUtil.DateFormat.ddMMMyyyy);


                    TextBox txtGrntUtl = (TextBox)gdvAttend.Rows[gvr.RowIndex].FindControl("txtGrsntUtl");
                    DateTime GrantedUntil = CodeUtil.ConvertToDate_ddMMMyyy(txtGrntUtl.Text, CodeUtil.DateFormat.ddMMMyyyy);


                    TextBox txtLeaveUtl = (TextBox)gdvAttend.Rows[gvr.RowIndex].FindControl("txtGrsntUtl");
                    DateTime LeaveUntil = CodeUtil.ConvertToDate_ddMMMyyy(txtLeaveUtl.Text, CodeUtil.DateFormat.ddMMMyyyy);

                    FileUpload UploadProof1 = (FileUpload)gdvAttend.Rows[gvr.RowIndex].FindControl("UploadProof");
                    String MyString = string.Empty;
                    string extension = string.Empty;
                    if (UploadProof1.HasFile)
                    {
                        DateTime MyDate = DateTime.Now;
                        MyString = MyDate.ToString("ddMMyyhhmmss");
                        string Filename = UploadProof1.PostedFile.FileName.ToLower();
                        extension = System.IO.Path.GetExtension(UploadProof1.PostedFile.FileName).ToLower();
                        string storePath = Server.MapPath("~") + "/" + "LeaveApplications/" + Convert.ToInt32(Session["UserId"]);
                        if (!Directory.Exists(storePath))
                            Directory.CreateDirectory(storePath);
                        UploadProof1.PostedFile.SaveAs(storePath + "/" + MyString + extension);
                    }
                    string Proof = MyString + extension;



                    //AttendanceDAC.InsLeavepermission(LID, EmpID, AppliedOn, LeaveFrom, LeaveUntil, GrantedFrom, GrantedUntil, GrantedBy, Proof);
                    //Response.Redirect("AdvancedLeaveApplication.aspx");



                }
            }
            catch (Exception ex)
            {
                clsErrorLog.HMSEventLog(ex, "AdvancedLeaveApplication", "gdvattend_RowCommand", "007");

            }


        }

        public void BindEmpList()
        {
            DataSet ds = new DataSet();
            string EmpName = string.Empty;
            int WorkSite = Convert.ToInt32(0);// Default HO
            int Dept = Convert.ToInt32(null);
            //ds = AttendanceDAC.HR_SearchReimburseEmp();
            ds = AttendanceDAC.HR_SearchEmpBySiteDept(WorkSite, Dept, "y", Convert.ToInt32(Session["CompanyID"]));
            ddlEmp.DataSource = ds.Tables[0];
            ddlEmp.DataTextField = "name";
            ddlEmp.DataValueField = "EmpID";
            ddlEmp.DataBind();
            lblCount.Text = ds.Tables[0].Rows.Count.ToString() + " Employees Found!"; ;
            ddlEmp.Items.Insert(0, new ListItem("---SELECT---", "0", true));
        }
        protected void gdvAttend_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    TextBox txtappon = (TextBox)e.Row.FindControl("txtAppOn");
                    //txtappon.Text = DateTime.Today.ToShortDateString();
                    txtappon.Text = DateTime.Now.ToString("dd MMM yyyy");
                    //txtappon.Text = CodeUtil.ConvertToDate_ddMMMyyy(DateTime.Today.ToShortDateString(), CodeUtil.CodeUtil.DateFormat.ddMMMyyyy);
                    //DataSet ds = new DataSet();
                    //ds = (DataSet)Session["DataSet"];
                    // e.Row.Cells[8].ToolTip = ConsoleColor.Yellow;
                    DataSet ds = new DataSet();
                    DataTable dt = new DataTable();
                    LinkButton lblLAvl = (LinkButton)e.Row.FindControl("lblLAvl");
                    string strLEaves = "";
                    //e.Row.Cells[8].ToolTip = "CL=" + (e.Row.DataItem as DataRowView)["CL"].ToString() + "\nSL=" + (e.Row.DataItem as DataRowView)["SL"].ToString() + "\nEL=" + (e.Row.DataItem as DataRowView)["EL"].ToString();
                    // pratap date:25-03-2016 
                    Label lblEmpId = (Label)e.Row.FindControl("lblEmpID");
                    decimal TotalLeaves = 0;
                    if (lblEmpId != null)
                    {
                        int LEmpId = Convert.ToInt32(lblEmpId.Text);
                        DropDownList ddlLeaveType = (DropDownList)e.Row.FindControl("grdddlLeaveType");
                        if (ddlLeaveType != null)
                        {
                            ds = BindGrdLeaveTypesBasedOnGender(LEmpId);
                            //ddlLeaveType.DataSource = BindGrdLeaveTypesBasedOnGender(LEmpId);
                            ddlLeaveType.DataSource = ds;
                            ddlLeaveType.DataTextField = "Name1";
                            ddlLeaveType.DataValueField = "LeaveType";
                            ddlLeaveType.DataBind();
                            dt = ds.Tables[0];
                            foreach (DataRow dr in dt.Rows) // Loop over the rows.
                            {
                                if (dr["Leavebalance"].ToString() != "0.00")
                                {
                                    strLEaves = strLEaves + dr["ShortName"].ToString() + "\t " + dr["Leavebalance"].ToString() + "\n";
                                    TotalLeaves = TotalLeaves + Convert.ToDecimal(dr["Leavebalance"].ToString());
                                }

                            }
                            e.Row.Cells[9].ToolTip = strLEaves;
                            if (lblLAvl != null)
                                lblLAvl.Text = TotalLeaves.ToString("0.00");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                clsErrorLog.HMSEventLog(ex, "AdvancedLeaveApplication", "gdvAttend_RowDataBound", "008");

            }
        }
        protected void ddlMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            //BindPager();

        }
        protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            //BindPager();

        }

        protected void btnSubmit_Click1(object sender, EventArgs e)
        {
            try
            {
                BindPager();
            }
            catch (Exception ex)
            {
                clsErrorLog.HMSEventLog(ex, "AdvancedLeaveApplication", "btnSubmit_Click1", "009");

            }
        }

        protected void gvLeaves_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton lnkDel = (LinkButton)e.Row.FindControl("lnkDel");
                LinkButton lnkRep = (LinkButton)e.Row.FindControl("lnkRep");
                lnkDel.Enabled = lnkRep.Enabled = Editable;
            }
        }

        protected void ddlWS_SelectedIndexChanged1(object sender, EventArgs e)
        {
            try
            {
                BindDeparmetBySite(Convert.ToInt32(ddlWS.SelectedValue));
                ViewState["SiteidS"] = Convert.ToInt32(ddlWS.SelectedValue);
                Siteid = Convert.ToInt32(ViewState["SiteidS"]);
            }
            catch (Exception ex)
            {
                clsErrorLog.HMSEventLog(ex, "AdvancedLeaveApplication", "ddlWS_SelectedIndexChanged1", "010");

            }
        }
        //Added By Rijwan for Worksite Google Search 
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionList(string prefixText, int count, string contextKey)
        {
            //DataSet ds = AttendanceDAC.GetWorkSiteLeaveActive(prefixText);

            DataSet ds = AttendanceDAC.GetWorkSites(prefixText, SearchCompanyID, Userid, ModID);

            return ConvertStingArray(ds);// txtItems.ToArray();
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionListDep(string prefixText, int count, string contextKey)
        {
            DataSet ds = AttendanceDAC.GetSearchDesiginationFilterActive(prefixText, SearchCompanyID, Siteid);
            return ConvertStingArray(ds);// txtItems.ToArray();
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionListEmp(string prefixText, int count, string contextKey)
        {
            DataSet ds = AttendanceDAC.HR_GoogleSearchEmpBySiteDept(prefixText, WSiteid, EDeptid, "Y", SearchCompanyID);
            return ConvertStingArray(ds);
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
            ddlDept.DataSource = ds;
            ddlDept.DataTextField = "DeptName";
            ddlDept.DataValueField = "DepartmentUId";
            ddlDept.DataBind();
            ddlDept.Items.Insert(0, new ListItem("---ALL---", "0", true));


        }

        public void txtAppOn_TextChanged(object sender, EventArgs e)
        {
            try
            {
                TextBox txtAppOn = (TextBox)gdvAttend.FindControl("txtAppOn");
                string currentDate = DateTime.Today.ToShortDateString();
                DateTime dtSuppliedDate = DateTime.Parse(txtAppOn.Text);
                if (dtSuppliedDate.Subtract(DateTime.Today).Days != 0)
                {
                    AlertMsg.MsgBox(Page, "Date should be  equal to are greater than present Date");
                }
            }
            catch { }
        }

        protected void txtAFrom_TextChanged(object sender, EventArgs e)
        {

            try
            {
                DateTime FromDate = Convert.ToDateTime(txtAFrom.Text);
                int days = Convert.ToInt32(txtReqLeaves.Text);
                DateTime Tilldate = FromDate.AddDays(days - 1);
                txtAUntil.Text = (Tilldate.Date).ToString("dd MMM yyyy");
            }
            catch { }

        }
        protected void rbperiod_CheckedChanged(object sender, EventArgs e)
        {
            lblperiod.Visible = true;
            lblEnterleaves.Visible = false;
            pnltblNotAllow.Visible = false;
            pnltblAllowed.Visible = false;
            txtStPrd.Text = string.Empty;
            txtEndPrd.Text = string.Empty;

        }

        protected void rbdays_CheckedChanged(object sender, EventArgs e)
        {
            txtReqLeaves.Text = string.Empty;
            txtAFrom.Text = string.Empty;
            txtAUntil.Text = string.Empty;
            lblEnterleaves.Visible = true;
            lblperiod.Visible = false;
            pnltblNotAllow.Visible = false;
            pnltblAllowed.Visible = false;
        }

        protected void txtNAFrom_TextChanged(object sender, EventArgs e)
        {
            try
            {
                DateTime FromDate = Convert.ToDateTime(txtNAFrom.Text);
                int days = Convert.ToInt32(txtReqLeaves.Text);
                DateTime Tilldate = FromDate.AddDays(days - 1);
                txtNAUntil.Text = (Tilldate.Date).ToString("dd MMM yyyy");
            }
            catch { }

        }
        protected void Unnamed_Click(object sender, EventArgs e)
        {
            try
            {
                TextBox thisTextBox = (TextBox)sender;
                GridViewRow thisGridViewRow = (GridViewRow)thisTextBox.Parent.Parent;
                // GridViewRow currentRow = (GridViewRow)((TextBox)sender).Parent;
                TextBox txtGrsntFrm = (TextBox)thisGridViewRow.FindControl("txtGrsntFrm");
                TextBox txtGrsntUtl = (TextBox)thisGridViewRow.FindControl("txtGrsntUtl");
                Label txtNoofDays = (Label)thisGridViewRow.FindControl("txtNoofDays");
                if (txtGrsntFrm.Text.Trim() == "" || txtGrsntUtl.Text.Trim() == "")
                    txtNoofDays.Text = "N/A";
                else
                {

                    DateTime dtGrsntFrm = CodeUtil.ConvertToDate_ddMMMyyy(txtGrsntFrm.Text, CodeUtil.DateFormat.ddMMMyyyy);
                    DateTime dtGrsntUtl = CodeUtil.ConvertToDate_ddMMMyyy(txtGrsntUtl.Text, CodeUtil.DateFormat.ddMMMyyyy);
                    if (dtGrsntUtl < dtGrsntFrm)
                    {
                        dtGrsntUtl = dtGrsntFrm;
                        txtGrsntUtl.Text = dtGrsntUtl.ToString("dd MMM yyyy");
                        AlertMsg.MsgBox(Page, "Applied Untill Date must be greater than Applied From");
                    }
                    TimeSpan t = dtGrsntUtl - dtGrsntFrm;
                    double NrOfDays = t.TotalDays;
                    txtNoofDays.Text = (NrOfDays + 1).ToString();
                }

            }
            catch (Exception ex)
            {
                clsErrorLog.HMSEventLog(ex, "AdvancedLeaveApplication", "Unnamed_Click", "011");

            }

        }
    }



}