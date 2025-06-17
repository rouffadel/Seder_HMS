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
using Aeclogic.Common.DAL;
using Microsoft.Ajax.Utilities;
using System.Xml;

namespace AECLOGIC.ERP.HMSV1
{
    public partial class AdvancedLeaveApplicationV1 : AECLOGIC.ERP.COMMON.WebFormMaster
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
        string connectionString = ConfigurationManager.ConnectionStrings["ERPConnectionString"].ConnectionString;
        public string AirTicket, HelpAirTicket, ExitReEntry, HelpExitReEntry, Guarantor, HelpGuarantor, GuarantorName = "-N/A-", HelpGuarantorName = "-N/A-";
        //public int ExitReEntry, Guarantor;  
        static int ModID;
        static int Userid;
        public enum TravelMode
        {
            AirTicketC = 1,//"Company Ticket",
            AirTicketS = 2,///"Self Ticket",
            ExitReEntryY = 3,// "Yes",
            ExitReEntryN = 4,//"No",
            GuarantorY = 5,//"Yes",
            GuarantorN = 6//"No"
        }
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
                   // tblTravelHelpOthers.Visible = true;
                   // tblTravel.Visible = false;
                }
            }
            else
            {
                objHrCommon.PageSize = AdvancedLeaveAppPaging.CurrentPage;
                objHrCommon.CurrentPage = AdvancedLeaveAppPaging.ShowRows;
                BindLeaveDetails(objHrCommon);
            }
        }
        private void BindLocations(DropDownList ddl)
        {
            DataSet ds = AttendanceDAC.CMS_Get_City();
            ddl.DataSource = ds;
            ddl.DataTextField = ds.Tables[0].Columns["CItyName"].ToString();
            ddl.DataValueField = ds.Tables[0].Columns["CityID"].ToString();
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("---Select---", "0"));
        }
        /* private void GetEmlName(DropDownList ddlId)
         {
             DataSet ds = AttendanceDAC.HMS_Get_EmpName();
             ddlId.DataSource = ds;
             ddlId.DataTextField = ds.Tables[0].Columns["Guarantor"].ToString();
             ddlId.DataBind();
             ddlId.Items.Insert(0, new ListItem("---Select---", "0"));
         }*/
        private void BindDays()
        {
            List<int> MyList = new List<int>();
            //MyList.Add("Select");
            MyList.Add(15);
            MyList.Add(30);
            MyList.Add(45);
            MyList.Add(60);
            MyList.Add(90);
            foreach (int lst in MyList)
            {
                ddlNoDays.Items.Add(lst.ToString());
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {

            try
            {
                SearchCompanyID = Convert.ToInt32(Session["CompanyID"]);
                Userid = Convert.ToInt32(Convert.ToInt32(Session["UserId"]).ToString());

                if (!IsPostBack)
                {
                    Page.Form.Attributes.Add("enctype", "multipart/form-data");
                    GetParentMenuId();
                    ViewState["AppliedOn"] = DateTime.Now.ToString("dd MMM yyyy");
                    ViewState["LID"] = 0;
                    ViewState["ReqLeaves"] = 0;
                    ViewState["TotalLeaves"] = 0;
                    ViewState["Status"] = 0;
                    BindEmpList();
                    BindYears();
                    BindLeaveTypes();
                    //BindDataGrid();
                    if (Request.QueryString.Count > 0)
                    {
                        int id = Convert.ToInt32(Request.QueryString["key"].ToString());
                        if (id == 1)
                        {
                            rbperiod.Checked = true;
                            int EmpId = Convert.ToInt32(Session["UserId"]);
                            ViewState["EmpID"] = EmpId;
                            tblmain.Visible = true;
                            tblTravel.Visible = true;
                            tblTravelHelpOthers.Visible = false;
                            tblView.Visible = false;
                            tblReply.Visible = false;
                            BindGrid(EmpId);
                            //BindDays();                            
                            BindLocations(ddlFrPlace);
                            BindLocations(ddlToPlace);
                            dvLeaveApply.Visible = false;
                        }
                        else if (id == 2)
                        {
                            dvLeaveApply.Visible = true;
                            tblmain.Visible = false;
                            tblTravel.Visible = false;
                            tblView.Visible = false;
                            BindWorkSites();
                            BindDepartments();
                            BindPager();
                            dvgr.Visible = true;
                            BindLocations(ddlHelpFrPlace);
                            BindLocations(ddlHelpToPlace);
                            tblTravelHelpOthers.Visible = true;
                        }

                    }
                    else
                    {
                        int EmpID = Convert.ToInt32(Session["UserId"]);
                        ViewState["EmpID"] = EmpID;
                        BindPager();
                        tblTravel.Visible = false;
                        tblTravelHelpOthers.Visible = false;
                        tblmain.Visible = false;
                        tblReply.Visible = false;
                        dvLeaveApply.Visible = false;
                    }
                    //try
                    //{

                    //    ViewState["WSID"] = 0;
                    //    //if (Convert.ToInt32(Session["RoleId"].ToString()) == 7)
                    //    //{
                    //    try
                    //    {
                    //        DataSet ds = clViewCPRoles.HR_DailyAttStatus(Convert.ToInt32(Session["UserId"]));
                    //        ViewState["WSID"] = ds.Tables[0].Rows[0]["ID"].ToString();
                    //        ddlWS.SelectedValue = ds.Tables[0].Rows[0]["ID"].ToString();
                    //        //ddlWS.Enabled = false;
                    //        txtSearchWorksite.Text = ds.Tables[0].Rows[0]["Name"].ToString();
                    //        //txtSearchWorksite.ReadOnly = true;
                    //    }
                    //    catch
                    //    {
                    //    }
                    //    //}
                    //}
                    //catch { }
                }
            }
            catch (Exception ex)
            {
                clsErrorLog.HMSEventLog(ex, "AdvancedLeaveApplication", "Page_Load", "001");

            }
        }
        public void BindLeaveTypes()
        {
            DataSet ds = Leaves.GetTypeofLeavesList();



            ddlSumLeaveType.DataSource = ds.Tables[0];
            ddlSumLeaveType.DataTextField = "Name";
            ddlSumLeaveType.DataValueField = "LeaveType";
            ddlSumLeaveType.DataBind();
            ddlSumLeaveType.Items.Insert(0, new ListItem("--Select--", "0"));
            ddlLeaveTypeSelf.DataSource = ds.Tables[1];
            ddlLeaveTypeSelf.DataTextField = "Name";
            ddlLeaveTypeSelf.DataValueField = "TypeId";
            ddlLeaveTypeSelf.DataBind();
            ddlLeaveTypeSelf.Items.Insert(0, new ListItem("--Select--", "0"));

        }
        protected void ddlLeaveTypeSelf_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlLeaveTypeSelf.SelectedValue == "1")
                ddlSumLeaveType.SelectedValue = "5";
            else if (ddlLeaveTypeSelf.SelectedValue == "2")
                ddlSumLeaveType.SelectedValue = "13";
            else if (ddlLeaveTypeSelf.SelectedValue == "3")
                ddlSumLeaveType.SelectedValue = "22";
            else if (ddlLeaveTypeSelf.SelectedValue == "4")
                ddlSumLeaveType.SelectedValue = "13";
            else if (ddlLeaveTypeSelf.SelectedValue == "5")
                ddlSumLeaveType.SelectedValue = "4";
            else if (ddlLeaveTypeSelf.SelectedValue == "6")
                ddlSumLeaveType.SelectedValue = "23";
            else
                ddlSumLeaveType.SelectedValue = "0";

        }
        public DataSet BindGrdLeaveTypes()
        {
            DataSet ds = Leaves.GetTypeofLeavesList();
            return ds;
        }

        //added by pratap date: 25-03-2016
        //changes the SP here from T_G_Leaves_GetTypeOfLeavesList_BasedOnGender to T_G_Leaves_GetTypeOfLeavesList_BasedOnGender_Employeeportal
        //for loding the Leaves and update them as soon as Leaves applied
        public DataSet BindGrdLeaveTypesBasedOnGender(int EmpID)
        {
            DataSet ds = Leaves.T_G_Leaves_GetTypeOfLeavesList_BasedOnGender_Employeeportal(EmpID);
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
                    EmpID = 0;
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

                    if (Convert.ToInt32(Session["UserId"]) > 0)
                    {
                        EmpID = Convert.ToInt32(Session["UserId"]);
                    }

                }
                catch { }
                DataSet ds = AttendanceDAC.HR_GetLeaveDetailsByPaging(objHrCommon, EmpID, WSID, Month, Year, Convert.ToInt32(Session["CompanyID"]));
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
        public void BindYears()
        {
            DataSet ds = AttendanceDAC.GetCalenderYear();

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
                ViewState["Editable"] = Editable = (bool)ds.Tables[0].Rows[0]["Editable"];
                ViewState["ViewAll"] = (bool)ds.Tables[0].Rows[0]["ViewAll"];
                viewall = (bool)ViewState["ViewAll"];
                btnSubmit.Enabled = btnsave.Enabled = btnReply.Enabled = Editable;
                btnSubmit.Visible = btnsave.Visible = btnReply.Visible = Editable;
            }
            return MenuId;
        }

        public void BindGrid(int? EmpID)
        {
            DataSet ds1 = AttendanceDAC.GetAvalableLeaves_LC(EmpID);
            if (ds1.Tables.Count > 0)
            {
                if (ds1.Tables[2].Rows.Count == 0)
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
                    btnApply.Enabled = true;
                    tblmain.Visible = true;
                    tblTravel.Visible = true;
                }
                else
                {
                    AlertMsg.MsgBox(Page, "Leave already applied in between these days", AlertMsg.MessageType.Error);
                    btnApply.Enabled = false;
                    tblmain.Visible = false;
                    tblTravel.Visible = false;
                    return;
                }


            }

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                AdvancedLeaveAppOthPaging.CurrentPage = 1;
                BindPager();
               // tblTravelHelpOthers.Visible = false;
                //tblTravel.Visible = false;
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
                if (Emp_hid.Value != "")
                {
                    if(Emp_hid.Value!="")
                    EmpID = Convert.ToInt32(Emp_hid.Value);
                    int? WS = null;

                    WS = Convert.ToInt32(ddlWs_hid.Value == "" ? "0" : ddlWs_hid.Value);
                    int? Dept = null;
                    if (ddlDept.SelectedIndex != 0)
                        Dept = Convert.ToInt32(ddlDept.SelectedValue);
                    try
                    {

                        if (Convert.ToInt32(ViewState["WSID"]) > 0)
                        {
                            objHrCommon.SiteID = Convert.ToInt32(ViewState["WSID"]);
                            WS = Convert.ToInt32(ViewState["WSID"]);
                        }

                    }
                    catch { }
                    DataSet ds = AttendanceDAC.GetAvalableLeavesByPaging(objHrCommon, EmpID, WS, Dept, Convert.ToInt32(Session["CompanyID"]));
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
                DataSet ds = (DataSet)objRights.GetDaprtmentList();
                ViewState["Departments"] = ds;
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
                DataSet ds = AttendanceDAC.GetWorkSite(0, '1', Convert.ToInt32(Session["CompanyID"]));
                ViewState["WorkSites"] = ds;

                FIllObject.FillDropDown(ref ddlWS, "HR_GetWorkSite_By_GetAvailableLeaves");
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
                    if (ddlSumLeaveType.SelectedValue != "0")
                    {
                        if (CodeUtil.ConvertToDate_ddMMMyyy(txtStPrd.Text, CodeUtil.DateFormat.ddMMMyyyy) <= CodeUtil.ConvertToDate_ddMMMyyy(txtEndPrd.Text, CodeUtil.DateFormat.ddMMMyyyy))
                        {
                            DateTime dtFrom = CodeUtil.ConvertToDate_ddMMMyyy(txtStPrd.Text, CodeUtil.DateFormat.ddMMMyyyy);
                            DateTime dtTo = CodeUtil.ConvertToDate_ddMMMyyy(txtEndPrd.Text, CodeUtil.DateFormat.ddMMMyyyy);
                            TimeSpan T = dtTo - dtFrom;//CodeUtil.ConvertToDate_ddMMMyyy(txtEndPrd.Text, CodeUtil.DateFormat.ddMMMyyyy) - CodeUtil.ConvertToDate_ddMMMyyy(txtStPrd.Text, CodeUtil.DateFormat.ddMMMyyyy);
                            ReqLeaves = Convert.ToInt32(T.TotalDays + 1);
                            txtNoofDays.Text = ReqLeaves.ToString();
                            txtRsnleaves.Text = ReqLeaves.ToString();
                            forDays.Visible = false;
                            forPeriod.Visible = true;
                            NotAllowDays.Visible = false;
                            NotAllowforPeriod.Visible = true;
                            if (Convert.ToInt32(ddlSumLeaveType.SelectedItem.Value) == 23)
                            {
                                if (dtFrom.DayOfWeek.ToString().Equals("Wednesday") || dtFrom.DayOfWeek.ToString().Equals("Thursday") || dtFrom.DayOfWeek.ToString().Equals("Friday"))
                                {

                                }
                                else
                                {
                                    AlertMsg.MsgBox(Page, "Applied From Day should Be Wednesday or Thursday or Friday.", AlertMsg.MessageType.Warning);
                                    return;
                                }
                                //TimeSpan t = txtGrsntUtl - GrantedFrom;
                                //double NrOfDays = t.TotalDays;
                                if (ReqLeaves != 3)
                                {
                                    AlertMsg.MsgBox(Page, "For Umra Leave No of Days  Must be 3 Days.", AlertMsg.MessageType.Warning);
                                    return;
                                }
                            }
                        }
                        else
                        {
                            AlertMsg.MsgBox(Page, "Select Proper Dates", AlertMsg.MessageType.Warning);

                            return;
                        }

                    }
                    else
                    {
                        AlertMsg.MsgBox(Page, "Select Leave type", AlertMsg.MessageType.Warning);

                        return;
                    }
                    if (txtStPrd.Text.Trim() != "")
                    {
                        if (Convert.ToInt32(Session["UserId"])!=1 && ddlLeaveTypeSelf.SelectedValue == "2")
                        {
                            DateTime dt = DateTime.Now;
                            DateTime LeaveFrom = CodeUtil.ConvertToDate_ddMMMyyy(txtStPrd.Text.Trim(), CodeUtil.DateFormat.ddMMMyyyy);

                            if ((LeaveFrom.Date - dt.Date).Days < 44)
                            {
                                AlertMsg.MsgBox(Page, "Leave should be Applied 45 Days prior to the From Date.", AlertMsg.MessageType.Warning);
                                txtStPrd.Text = string.Empty;
                                return;

                            }
                        }
                       
                    }
                    else
                    {
                        AlertMsg.MsgBox(Page, "Please enter Dates.", AlertMsg.MessageType.Warning);
                        return;
                    }

                }

                ViewState["ReqLeaves"] = ReqLeaves;
                decimal inAl = 0;
                if (Convert.ToInt32(Request.QueryString["key"].ToString()) == 1)
                    EmpID = Convert.ToInt32(ViewState["EmpID"]);
                DataSet ds1 = AttendanceDAC.GetAvlLeavesCount(EmpID, Convert.ToInt32(ddlSumLeaveType.SelectedItem.Value));
                if (ds1.Tables.Count > 0)
                {
                    gvAvailLeaves.DataSource = ds1;
                    decimal tot = 0;
                    if (ds1.Tables[0].Rows.Count > 0)
                    {
                        tot = Convert.ToDecimal(ds1.Tables[0].Rows[0]["LeaveCount"]);
                    }
                    ViewState["TotalLeaves"] = tot;
                    if (Convert.ToInt32(ddlLeaveTypeSelf.SelectedItem.Value) == 2 )
                    {

                        SqlParameter[] objParam = new SqlParameter[3];
                        objParam[0] = new SqlParameter("@EmpID", EmpID);
                        if (txtStPrd.Text.Trim() != "")
                            objParam[1] = new SqlParameter("@settlementdate", CodeUtil.ConvertToDate_ddMMMyyy(txtStPrd.Text, CodeUtil.DateFormat.ddMMMyyyy));
                        objParam[2] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
                        objParam[2].Direction = ParameterDirection.ReturnValue;
                        SQLDBUtil.ExecuteNonQuery("fn_GetEMPALBalanceBYEMPIDFromDate", objParam);
                        inAl = Convert.ToDecimal(objParam[2].Value);

                        DataSet dst = AttendanceDAC.GetAvalableLeaves_LC(EmpID);
                        if (dst.Tables.Count > 0)
                        {
                            if (dst.Tables[2].Rows.Count == 0)
                            {
                                if (Convert.ToInt32(Request.QueryString["key"].ToString()) == 1)
                                {
                                    if (dst != null && dst.Tables[0].Rows.Count > 0)
                                    {
                                        for (int i = 0; i < dst.Tables[0].Rows.Count; i++)
                                        {
                                            if (dst.Tables[0].Rows[i]["LeaveType"].ToString() == "5")
                                                dst.Tables[0].Rows[i]["Bal"] = inAl;
                                        }
                                    }
                                    dst.AcceptChanges();
                                    gvAvailLeaves.DataSource = dst.Tables[0];
                                    gvAvailLeaves.DataBind();
                                    gvEMPID.DataSource = dst.Tables[1];
                                    gvEMPID.DataBind();
                                }
                            }
                        }
                        tot = inAl;
                    }

                    
                    if (tot == 0)
                    {
                        AlertMsg.MsgBox(this.Page, "You Have No Leaves! even though u want leaves, these leaves go to LOP ", AlertMsg.MessageType.Info);
                        tblNotAllowed.Visible = true;
                        pnltblNotAllow.Visible = true;
                        tblAllowed.Visible = false;
                        pnltblAllowed.Visible = false;
                    }
                    if (ReqLeaves > tot)
                    {
                        AlertMsg.MsgBox(this.Page, "You Have No Sufficient Leaves! If u want all required leaves, the extra leaves go to LOP ", AlertMsg.MessageType.Info);
                        tblNotAllowed.Visible = true;
                        pnltblNotAllow.Visible = true;
                        tblAllowed.Visible = false;
                        pnltblAllowed.Visible = false;
                    }
                    else
                    {
                        AlertMsg.MsgBox(this.Page, "You Have Sufficient Leaves to Apply!", AlertMsg.MessageType.Info);
                        tblAllowed.Visible = true;
                        pnltblAllowed.Visible = true;
                        tblNotAllowed.Visible = false;
                        pnltblNotAllow.Visible = false;
                    }
                }

                if (Convert.ToInt32(Request.QueryString["key"].ToString()) == 2)
                {
                    dvgr.Visible = true;
                    tblTravel.Visible = false;
                    tblTravelHelpOthers.Visible = true;
                    tblNotAllowed.Visible = false;
                    pnltblNotAllow.Visible = false;
                }
            }
            catch (Exception ex)
            {
                clsErrorLog.HMSEventLog(ex, "AdvancedLeaveApplication", "btnSubmit_Click", "003");

            }
        }

        string SendUC;
        int LID;
        string CompanyTecket = string.Empty;
        string SelfTicket = string.Empty;
        protected void btnApply_Click(object sender, EventArgs e)
        {
            try
            {
                try
                {

                    String MyString = string.Empty;
                    string extension = string.Empty;
                    if (fuUploadProof.HasFile)
                    {

                        string Filename = fuUploadProof.PostedFile.FileName.ToLower();
                        extension = System.IO.Path.GetExtension(fuUploadProof.PostedFile.FileName).ToLower();

                    }
                    if (rdbTktCompany.Checked == true)
                    {
                        AirTicket = "Company Ticket";
                    }
                    if (rdbTktSelf.Checked == true)
                    {
                        AirTicket = "Self Ticket";
                    }
                    if (rdbNone.Checked == true)
                    {
                        AirTicket = "No Ticket";
                    }

                    if (rdbExitEntryReq.Checked == true)
                    {
                        ExitReEntry = "Required";
                    }
                    if (rdbExitEntryNotReq.Checked == true)
                    {
                        ExitReEntry = "Not Required";
                    }
                    if (rdbGuarantorReq.Checked == true)
                    {
                        Guarantor = "Yes";
                    }
                    if (rdbGuarantorNotReq.Checked == true)
                    {
                        Guarantor = "No";                        
                    }
                    //Sabir Husen
                    if(rdbNone.Checked == false && (ddlFrPlace.SelectedItem.ToString() == "---Select---" || ddlToPlace.SelectedItem.ToString() == "---Select--"))
                    {
                        AlertMsg.MsgBox(this.Page, "Please select From place and To place!", AlertMsg.MessageType.Warning);
                        return;
                    }
                    if (rdbExitEntryReq.Checked == true)
                    {
                        if (ddlNoDays.SelectedItem.ToString() == "--Select--")
                        {
                            AlertMsg.MsgBox(this.Page, "Please Select No Of days!", AlertMsg.MessageType.Warning);
                            return;
                        }
                    }
                    if (rdbGuarantorReq.Checked == true)
                    {
                        if (txtSearchEmpApr.Text == "")
                        {
                            AlertMsg.MsgBox(this.Page, "Please provide Guarantor Id!", AlertMsg.MessageType.Warning);
                            return;
                        }
                    }

                    if (Convert.ToInt32(ViewState["LID"]) == 0)
                    {
                        DateTime LeaveFrom, LeaveUntil;
                        if (rbdays.Checked)
                        {


                            LeaveFrom = CodeUtil.ConvertToDate_ddMMMyyy(txtAFrom.Text.Trim(), CodeUtil.DateFormat.ddMMMyyyy);
                            LeaveUntil = CodeUtil.ConvertToDate_ddMMMyyy(txtAUntil.Text.Trim(), CodeUtil.DateFormat.ddMMMyyyy);

                        }
                        else
                        {
                            LeaveFrom = CodeUtil.ConvertToDate_ddMMMyyy(txtStPrd.Text.Trim(), CodeUtil.DateFormat.ddMMMyyyy);
                            LeaveUntil = CodeUtil.ConvertToDate_ddMMMyyy(txtEndPrd.Text.Trim(), CodeUtil.DateFormat.ddMMMyyyy);
                        }
                        
                        if (LeaveFrom <= LeaveUntil)
                        {

                            int EmpID = Convert.ToInt32(Session["UserId"]);
                            int appdays;
                            if (txtReqLeaves.Text != "" || txtReqLeaves.Text != string.Empty)
                            {
                                appdays = Convert.ToInt32(txtReqLeaves.Text);
                            }
                            else
                            {
                                appdays = Convert.ToInt32(txtNoofDays.Text);
                            }
                            int totdays = 0;
                            decimal inAl = 0;

                            if (Convert.ToInt32(ddlSumLeaveType.SelectedItem.Value) == 5)
                            {
                                SqlParameter[] objParam = new SqlParameter[3];
                                objParam[0] = new SqlParameter("@EmpID", EmpID);
                                if (txtStPrd.Text.Trim() != "")
                                    objParam[1] = new SqlParameter("@settlementdate", CodeUtil.ConvertToDate_ddMMMyyy(txtStPrd.Text, CodeUtil.DateFormat.ddMMMyyyy));
                                objParam[2] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
                                objParam[2].Direction = ParameterDirection.ReturnValue;
                                SQLDBUtil.ExecuteNonQuery("fn_GetEMPALBalanceBYEMPIDFromDate", objParam);
                                inAl = Convert.ToDecimal(objParam[2].Value);
                                totdays = Convert.ToInt32(inAl);
                            }
                            else
                                totdays = AttendanceDAC.LeavesDaysCount(EmpID, LeaveFrom, LeaveUntil);


                            if (totdays > appdays)
                            {
                                AlertMsg.MsgBox(this.Page, "Check No.Of Leaves you have Entered!", AlertMsg.MessageType.Warning);
                            }
                            else
                            {

                                string Applied = ViewState["AppliedOn"].ToString();
                                DateTime AppliedOn = CodeUtil.ConvertToDate_ddMMMyyy(Applied, CodeUtil.DateFormat.ddMMMyyyy);

                                objHrCommon.LID = Convert.ToInt32(ViewState["LID"]);
                                int OutPut = HR_InsUpLeaveApplication(objHrCommon, Convert.ToInt32(Session["UserId"]), AppliedOn, LeaveFrom, LeaveUntil, txtApReson.Text, Convert.ToInt32(ViewState["Status"]), Convert.ToInt32(ddlSumLeaveType.SelectedItem.Value), extension, "", AirTicket, Convert.ToInt32(ddlFrPlace.SelectedItem.Value), Convert.ToInt32(ddlToPlace.SelectedItem.Value), ExitReEntry, Convert.ToInt32(ddlNoDays.SelectedItem.Value), Guarantor, txtSearchEmpApr.Text, Convert.ToInt32(ddlLeaveTypeSelf.SelectedItem.Value));
                                LID = objHrCommon.LID;
                                if (fuUploadProof.HasFile)
                                {
                                    string Filename = fuUploadProof.PostedFile.FileName.ToLower();
                                    extension = System.IO.Path.GetExtension(fuUploadProof.PostedFile.FileName).ToLower();
                                    string storePath = Server.MapPath("~") + "/" + "HMS/LeaveApplications/" + Convert.ToInt32(LID);
                                    fuUploadProof.PostedFile.SaveAs(storePath + extension);
                                }
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
                                    string Leaveurl = "ViewEmpLeaveDetails.aspx?LID=" + LID;
                                    string fullURLLeave = "window.open('" + Leaveurl + "', '_blank' );";
                                    ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", fullURLLeave, true);
                                }
                                else if (OutPut == 2)
                                    AlertMsg.MsgBox(Page, "Already applied in between these days", AlertMsg.MessageType.Warning);
                                else
                                    AlertMsg.MsgBox(Page, "Updated.!");
                                tblmain.Visible = false;
                                tblTravel.Visible = false;
                                tblTravelHelpOthers.Visible = false;
                                tblAllowed.Visible = false;
                                pnltblAllowed.Visible = false;
                                tblView.Visible = true;

                            }

                            BindPager();

                        }

                        else
                        {
                            AlertMsg.MsgBox(Page, "Check Dates", AlertMsg.MessageType.Warning);
                        }
                    }
                    else
                    {
                        DateTime LeaveFrom = CodeUtil.ConvertToDate_ddMMMyyy(txtAFrom.Text.Trim(), CodeUtil.DateFormat.ddMMMyyyy);
                        DateTime LeaveUntil = CodeUtil.ConvertToDate_ddMMMyyy(txtAUntil.Text.Trim(), CodeUtil.DateFormat.ddMMMyyyy);
                        if (LeaveFrom < LeaveUntil)
                        {

                            string Applied = ViewState["AppliedOn"].ToString();
                            DateTime AppliedOn = CodeUtil.ConvertToDate_ddMMMyyy(Applied, CodeUtil.DateFormat.ddMMMyyyy);
                            objHrCommon.LID = Convert.ToInt32(ViewState["LID"]);
                            int OutPut = HR_InsUpLeaveApplication(objHrCommon, Convert.ToInt32(Session["UserId"]), AppliedOn, LeaveFrom, LeaveUntil, txtReason.Text, Convert.ToInt32(ViewState["Status"]), Convert.ToInt32(ddlSumLeaveType.SelectedItem.Value), extension, "", AirTicket, Convert.ToInt32(ddlFrPlace.SelectedItem.Value), Convert.ToInt32(ddlToPlace.SelectedItem.Value), ExitReEntry, Convert.ToInt32(ddlNoDays.SelectedItem.Value), Guarantor, txtSearchEmpApr.Text, Convert.ToInt32(ddlLeaveTypeSelf.SelectedItem.Value));
                            if (OutPut == 1)
                            {
                                if (fuUploadProof.HasFile)
                                {
                                    string Filename = fuUploadProof.PostedFile.FileName.ToLower();
                                    extension = System.IO.Path.GetExtension(fuUploadProof.PostedFile.FileName).ToLower();
                                    string storePath = Server.MapPath("~") + "/" + "HMS/LeaveApplications/" + Convert.ToInt32(LID);
                                    fuUploadProof.PostedFile.SaveAs(storePath + extension);
                                }
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
                                AlertMsg.MsgBox(Page, "Aleready applied in between these days", AlertMsg.MessageType.Warning);
                            else
                                AlertMsg.MsgBox(Page, "Updated.!");
                            tblmain.Visible = false;
                            tblTravel.Visible = false;
                            tblTravelHelpOthers.Visible = false;
                            tblAllowed.Visible = false;
                            pnltblAllowed.Visible = false;
                            tblView.Visible = true;

                            BindPager();
                        }

                        else
                        {
                            AlertMsg.MsgBox(Page, "Check Dates", AlertMsg.MessageType.Warning);
                        }
                    }
                }
                catch (Exception ex)
                {
                    AlertMsg.MsgBox(Page, ex.Message.ToString(), AlertMsg.MessageType.Error);
                }
            }
            catch (Exception ex)
            {
                clsErrorLog.HMSEventLog(ex, "AdvancedLeaveApplication", "btnApply_Click", "004");

            }

        }
        int Fromplace = 0;
        int Toplace = 0;

        private int HR_InsUpLeaveApplication(HRCommon objHrCommon, int EmpID, DateTime AppliedOn, DateTime LeaveFrom, DateTime LeaveUntil, string Reason, int Status, int LeaveType, string Ext, string Remarks, string AirTicket, int FromPlace, int ToPlace, string ExitReEntry, int NoOfDays, string Guarantor, string GuarantorName,int TypeId)
        {
            try
            {
                SqlParameter[] objParam = new SqlParameter[19];
                objParam[0] = new SqlParameter("@LID", SqlDbType.Int);
                objParam[0].Direction = ParameterDirection.InputOutput;
                objParam[0].Value = objHrCommon.LID;
                objParam[1] = new SqlParameter("@EmpID", EmpID);
                objParam[2] = new SqlParameter("@AppliedOn", AppliedOn);
                objParam[3] = new SqlParameter("@LeaveFrom", LeaveFrom);
                objParam[4] = new SqlParameter("@LeaveUntil", LeaveUntil);
                objParam[5] = new SqlParameter("@Reason", Reason);
                objParam[6] = new SqlParameter("@Status", Status);
                objParam[7] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
                objParam[7].Direction = ParameterDirection.ReturnValue;
                objParam[8] = new SqlParameter("@LeaveType", LeaveType);
                objParam[9] = new SqlParameter("@ext", Ext);
                objParam[10] = new SqlParameter("@Remarks", Remarks);

                objParam[11] = new SqlParameter("@AirTicket", AirTicket);
                if (rdbTktCompany.Checked == true)
                {
                    objParam[12] = new SqlParameter("@FromPlace", FromPlace);
                    objParam[13] = new SqlParameter("@ToPlace", ToPlace);
                }
                if (rdbTktSelf.Checked == true)
                {
                    objParam[12] = new SqlParameter("@FromPlace", FromPlace);
                    objParam[13] = new SqlParameter("@ToPlace", ToPlace);
                }
                if (rdbNone.Checked == true)
                {
                    objParam[12] = new SqlParameter("@FromPlace", Fromplace);
                    objParam[13] = new SqlParameter("@ToPlace", Toplace);
                }
                
                //objParam[12] = new SqlParameter("@FromPlace", FromPlace);
                //objParam[13] = new SqlParameter("@ToPlace", ToPlace);
                objParam[14] = new SqlParameter("@ExitReEntry", ExitReEntry);
                if (rdbExitEntryReq.Checked == true)
                {
                    if (ddlNoDays.SelectedItem.ToString() == "--Select--")
                    {
                        AlertMsg.MsgBox(Page, "Please Select days", AlertMsg.MessageType.Warning);
                    }
                    else
                    {
                        objParam[15] = new SqlParameter("@NoOfDays", Convert.ToInt32(NoOfDays));
                    }
                }
                else
                {
                    objParam[15] = new SqlParameter("@NoOfDays", Convert.ToInt32(NoOfDays));
                }
                objParam[16] = new SqlParameter("@Guarantor", Guarantor);
                if (rdbGuarantorReq.Checked == true)
                {
                    if (txtSearchEmpApr.Text == "")
                    {
                        AlertMsg.MsgBox(Page, "Please provide Guarantor Id", AlertMsg.MessageType.Warning);
                    }
                    else
                    {
                        objParam[17] = new SqlParameter("@GuarantorName", Convert.ToString(GuarantorName));
                    }
                }
                else
                {
                    objParam[17] = new SqlParameter("@GuarantorName", GuarantorName);
                }
                objParam[18] = new SqlParameter("@TypeId", TypeId);

                int OutPut = SQLDBUtil.ExecuteNonQuery("HR_InsUpLeaveApplication", objParam);
                if (objParam[0] != null)
                {
                    objHrCommon.LID = Convert.ToInt32(objParam[0].Value);
                }
                return Convert.ToInt32(objParam[7].Value);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        int HelpFromplace = 0;
        int HelpToplace = 0;
        public int HR_InsUpLeaveApplication_SaveProof(HRCommon objHrCommon, int EmpID, DateTime AppliedOn, DateTime LeaveFrom, DateTime LeaveUntil, string Reason, int Status, int LeaveType, string Proof, string HelpAirTicket, int HelpFromPlace, int HelpToPlace, string HelpExitReEntry, int HelpNoOfDays, string HelpGuarantor, string HelpGuarantorName,int TypeId)
        {
            try
            {
                SqlParameter[] objParam = new SqlParameter[19];
                objParam[0] = new SqlParameter("@LID", SqlDbType.Int);
                objParam[0].Direction = ParameterDirection.InputOutput;
                objParam[0].Value = objHrCommon.LID;
                objParam[1] = new SqlParameter("@EmpID", EmpID);
                objParam[2] = new SqlParameter("@AppliedOn", AppliedOn);
                objParam[3] = new SqlParameter("@LeaveFrom", LeaveFrom);
                objParam[4] = new SqlParameter("@LeaveUntil", LeaveUntil);
                objParam[5] = new SqlParameter("@Reason", Reason);
                objParam[6] = new SqlParameter("@Status", Status);
                objParam[7] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
                objParam[7].Direction = ParameterDirection.ReturnValue;
                objParam[8] = new SqlParameter("@LeaveType", LeaveType);
                objParam[9] = new SqlParameter("@Proof", Proof);

                objParam[10] = new SqlParameter("@AirTicket", HelpAirTicket);
                if (rdbTktCompany.Checked == true)
                {
                    objParam[11] = new SqlParameter("@FromPlace", HelpFromPlace);
                    objParam[12] = new SqlParameter("@ToPlace", HelpToPlace);
                }
                if (rdbTktSelf.Checked == true)
                {
                    objParam[11] = new SqlParameter("@FromPlace", HelpFromPlace);
                    objParam[12] = new SqlParameter("@ToPlace", HelpToPlace);
                }
                if (rdbHelpNone.Checked == true)
                {
                    objParam[11] = new SqlParameter("@FromPlace", HelpFromplace);
                    objParam[12] = new SqlParameter("@ToPlace", HelpToplace);
                }               
                objParam[13] = new SqlParameter("@ExitReEntry", HelpExitReEntry);
                if (rdbHelpExitEntryReq.Checked == true)
                {
                    if (ddlHelpNoDays.SelectedItem.ToString() == "--Select--")
                    {
                        AlertMsg.MsgBox(Page, "Please Select days", AlertMsg.MessageType.Warning);
                    }
                    else
                    {
                        objParam[14] = new SqlParameter("@NoOfDays", Convert.ToInt32(HelpNoOfDays));
                    }
                }
                else
                {
                    objParam[14] = new SqlParameter("@NoOfDays", HelpNoOfDays);
                }
                objParam[15] = new SqlParameter("@Guarantor", HelpGuarantor);
                if (rdbHelpGuarantorReq.Checked == true)
                {
                    if (txtHelpSearchEmpApr.Text == "")
                    {
                        AlertMsg.MsgBox(Page, "Please provide Guarantor Id", AlertMsg.MessageType.Warning);
                    }
                    else
                    {
                        objParam[16] = new SqlParameter("@GuarantorName", Convert.ToString(HelpGuarantorName));
                    }
                }
                else
                {
                    objParam[16] = new SqlParameter("@GuarantorName", HelpGuarantorName);
                }
                objParam[17] = new SqlParameter("@TypeId", TypeId); //
                objParam[18] = new SqlParameter("@CreatedBy", Convert.ToInt32(Session["UserId"]));
                SQLDBUtil.ExecuteNonQuery("HR_InsUpLeaveApplicationwith_proof", objParam);
                if (objParam[0] != null)
                {
                    objHrCommon.LID = Convert.ToInt32(objParam[0].Value);
                }
                return Convert.ToInt32(objParam[7].Value);
            }
            catch (Exception e)
            {
                throw e;
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

            //if (ddlEmp_hid.Value != "")
            //    ddlEmp.SelectedValue = ddlEmp_hid.Value;
            ListItem itmSelected = ddlEmp.Items.FindByText(txtSearchEmp.Text);
            if (itmSelected != null)
            {
                ddlEmp.SelectedItem.Selected = false;
                itmSelected.Selected = true;
            }
            if (txtSearchEmp.Text != "")
            {
                if (ddlEmp.DataSource != null)
                    ddlEmp.SelectedIndex = 1;
            } //ddlEmp.SelectedIndex = 1; 
           // tblTravel.Visible = false;
           // tblTravelHelpOthers.Visible = false;
        }

        protected void btnRequest_Click(object sender, EventArgs e)
        {
            try
            {
                String MyString = string.Empty;
                string extension = string.Empty;
                if (fuUploadProof.HasFile)
                {

                    string Filename = fuUploadProof.PostedFile.FileName.ToLower();
                    extension = System.IO.Path.GetExtension(fuUploadProof.PostedFile.FileName).ToLower();

                }
                //Sabir Husen
                if (rdbTktCompany.Checked == true)
                {
                    AirTicket = "Company Ticket";
                }
                if (rdbTktSelf.Checked == true)
                {
                    AirTicket = "Self Ticket";
                }
                if (rdbNone.Checked == true)
                {
                    AirTicket = "No Ticket";
                }
                if (rdbExitEntryReq.Checked == true)
                {
                    ExitReEntry = "Required";
                }
                if (rdbExitEntryNotReq.Checked == true)
                {
                    ExitReEntry = "Not Required";
                }
                if (rdbGuarantorReq.Checked == true)
                {
                    Guarantor = "Yes";
                }
                if (rdbGuarantorNotReq.Checked == true)
                {
                    Guarantor = "No";
                }
                
                if (ddlFrPlace.SelectedItem.ToString() == "---Select---" || ddlToPlace.SelectedItem.ToString() == "---Select--")
                {
                    AlertMsg.MsgBox(this.Page, "Please select From place and To place!", AlertMsg.MessageType.Warning);
                    return;
                }
                if (rdbExitEntryReq.Checked == true)
                {
                    if (ddlNoDays.SelectedItem.ToString() == "--Select--")
                    {
                        AlertMsg.MsgBox(this.Page, "Please Select No Of days!", AlertMsg.MessageType.Warning);
                        return;
                    }
                }
                if (rdbGuarantorReq.Checked == true)
                {
                    if (txtSearchEmpApr.Text == "")
                    {
                        AlertMsg.MsgBox(this.Page, "Please provide Guarantor Id!", AlertMsg.MessageType.Warning);
                        return;
                    }
                }
                if (Convert.ToInt32(ViewState["LID"]) == 0)
                {
                    DateTime LeaveFrom = DateTime.Now, LeaveUntil = DateTime.Now;
                    if (rbdays.Checked)
                    {
                        LeaveFrom = CodeUtil.ConvertToDate_ddMMMyyy(txtNAFrom.Text.Trim(), CodeUtil.DateFormat.ddMMMyyyy);
                        LeaveUntil = CodeUtil.ConvertToDate_ddMMMyyy(txtNAUntil.Text.Trim(), CodeUtil.DateFormat.ddMMMyyyy);
                    }
                    else if (rbperiod.Checked)
                    {
                        LeaveFrom = CodeUtil.ConvertToDate_ddMMMyyy(txtStPrd.Text.Trim(), CodeUtil.DateFormat.ddMMMyyyy);
                        LeaveUntil = CodeUtil.ConvertToDate_ddMMMyyy(txtEndPrd.Text.Trim(), CodeUtil.DateFormat.ddMMMyyyy);
                    }

                    if (LeaveFrom <= LeaveUntil)
                    {
                        TimeSpan span = LeaveUntil.Subtract(LeaveFrom);
                        int totdays = span.Days + 1;
                        int appdays = 0;
                        if (rbdays.Checked)
                        {
                            appdays = Convert.ToInt32(txtReqLeaves.Text);
                        }
                        else if (rbperiod.Checked)
                        {
                            appdays = Convert.ToInt32(txtRsnleaves.Text);
                        }


                        if (totdays > appdays)
                        {
                            AlertMsg.MsgBox(this.Page, "Check No.Of Leaves u have Entered!", AlertMsg.MessageType.Warning);
                        }
                        else
                        {
                            string Applied = ViewState["AppliedOn"].ToString();
                            DateTime AppliedOn = CodeUtil.ConvertToDate_ddMMMyyy(Applied, CodeUtil.DateFormat.ddMMMyyyy);
                            objHrCommon.LID = Convert.ToInt32(ViewState["LID"]);
                            int OutPut = HR_InsUpLeaveApplication(objHrCommon, Convert.ToInt32(Session["UserId"]), AppliedOn, LeaveFrom, LeaveUntil, txtReason.Text, Convert.ToInt32(ViewState["Status"]), Convert.ToInt32(ddlSumLeaveType.SelectedItem.Value), extension, "", AirTicket, Convert.ToInt32(ddlFrPlace.SelectedItem.Value), Convert.ToInt32(ddlToPlace.SelectedItem.Value), ExitReEntry, Convert.ToInt32(ddlNoDays.SelectedItem.Value), Guarantor, txtSearchEmpApr.Text, Convert.ToInt32(ddlLeaveTypeSelf.SelectedItem.Value));
                            if (OutPut == 1)
                            {
                                if (fuUploadProof.HasFile)
                                {
                                    string Filename = fuUploadProof.PostedFile.FileName.ToLower();
                                    extension = System.IO.Path.GetExtension(fuUploadProof.PostedFile.FileName).ToLower();
                                    string storePath = Server.MapPath("~") + "/" + "HMS/LeaveApplications/" + Convert.ToInt32(LID);
                                    fuUploadProof.PostedFile.SaveAs(storePath + extension);
                                }
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
                                AlertMsg.MsgBox(Page, "Already leave applied between these dates.!", AlertMsg.MessageType.Warning);
                            else
                                AlertMsg.MsgBox(Page, "Updated !");
                            BindPager();
                        }
                    }
                    else
                    {
                        AlertMsg.MsgBox(Page, "Check Dates", AlertMsg.MessageType.Warning);
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
                        int Output = HR_InsUpLeaveApplication(objHrCommon, Convert.ToInt32(Session["UserId"]), AppliedOn, LeaveFrom, LeaveUntil, txtReason.Text, Convert.ToInt32(ViewState["Status"]), Convert.ToInt32(ddlSumLeaveType.SelectedItem.Value), "", "", AirTicket, Convert.ToInt32(ddlFrPlace.SelectedItem.Value), Convert.ToInt32(ddlToPlace.SelectedItem.Value), ExitReEntry, Convert.ToInt32(ddlNoDays.SelectedItem.Value), Guarantor, txtSearchEmpApr.Text, Convert.ToInt32(ddlLeaveTypeSelf.SelectedItem.Value));
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
                            AlertMsg.MsgBox(Page, "Already leave applied between these dates.!", AlertMsg.MessageType.Error);
                        else
                            AlertMsg.MsgBox(Page, "Updated sucessfully.!");
                        BindPager();
                        Response.Redirect("AdvancedLeaveApplication.aspx");
                    }
                    else
                    {
                        AlertMsg.MsgBox(Page, "Check Dates", AlertMsg.MessageType.Warning);
                    }
                }

            }
            catch (Exception AdvLeaApp)
            {
                AlertMsg.MsgBox(Page, AdvLeaApp.Message.ToString(), AlertMsg.MessageType.Error);
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
                        DataSet ds = AttendanceDAC.HR_GetLeaveDetailsByID(LID);
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
                        tblView.Visible = false;
                        tblReply.Visible = false;
                        BindPager();

                    }
                    if (e.CommandName == "Del")
                    {
                        int LID = Convert.ToInt32(e.CommandArgument);
                        int EmpID = Convert.ToInt32(Session["UserId"]);
                        AttendanceDAC.HR_DelLeaveApplication(LID);
                        BindPager();
                        tblReply.Visible = false;
                    }
                    if (e.CommandName == "Rep")
                    {
                        int LID = Convert.ToInt32(e.CommandArgument);
                        ViewState["LID"] = LID;
                        DataSet ds = AttendanceDAC.HR_LeaveAppReply(LID);
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
        protected void rdbExitEntryNotReq_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbExitEntryNotReq.Checked == true)
            {
                ddlNoDays.SelectedIndex = 0;
                ddlNoDays.Enabled = false;
            }
        }
        protected void rdbExitEntryReq_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbExitEntryReq.Checked == true)
            {
                ddlNoDays.Enabled = true;
            }
        }

        protected void rdbGuarantorReq_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbGuarantorReq.Checked == true)
            {
                //lblGuarantorId.Visible = true;
                txtSearchEmpApr.Enabled = true;
                //txtSearchEmpApr.ReadOnly = false;
                txtSearchEmpApr.Text = "";
                txtSearchEmpApr.Focus();
            }
        }
        //string NoGuarantor = "-N/A-";
        protected void rdbGuarantorNotReq_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbGuarantorNotReq.Checked == true)
            {
                //lblGuarantorId.Visible = false;
                txtSearchEmpApr.Enabled = false;
                txtSearchEmpApr.Text = GuarantorName.ToString();
                //txtSearchEmpApr.ReadOnly = true;                
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
                tblReply.Visible = false;
            }
            catch (Exception RplyToHR)
            {
                AlertMsg.MsgBox(Page, RplyToHR.Message.ToString(), AlertMsg.MessageType.Error);
            }

        }
        protected void ddlWS_SelectedIndexChanged(object sender, EventArgs e)
        {
            WSiteid = Convert.ToInt32(ddlWS.SelectedValue);

        }
        protected void btnsave_Click(object sender, EventArgs e)
        {
            //place a validation for leaves by pratap date: 16-04-2016
            try
            {
                if(Emp_hid.Value== "")
                {
                    AlertMsg.MsgBox(this.Page, "Please Search the Employee!", AlertMsg.MessageType.Warning);
                    return;

                }
                if (rdbHelpTktCompany.Checked == true)
                {
                    HelpAirTicket = "Company Ticket";
                }
                if (rdbHelpTktSelf.Checked == true)
                {
                    HelpAirTicket = "Self Ticket";
                }
                if (rdbHelpNone.Checked == true)
                {
                    HelpAirTicket = "No Ticket";
                }
                if (rdbHelpExitEntryReq.Checked == true)
                {
                    HelpExitReEntry = "Required";
                }
                if (rdbHelpExitEntryNotReq.Checked == true)
                {
                    HelpExitReEntry = "Not Required";
                }
                if (rdbHelpGuarantorReq.Checked == true)
                {
                    HelpGuarantor = "Yes";
                }
                if (rdbHelpGuarantorNotReq.Checked == true)
                {
                    HelpGuarantor = "No";
                }
                if (rdbHelpNone.Checked==false && ( ddlHelpFrPlace.SelectedItem.ToString() == "---Select---" || ddlHelpToPlace.SelectedItem.ToString() == "---Select--"))
                {
                    AlertMsg.MsgBox(this.Page, "Please select From Place and To Place!", AlertMsg.MessageType.Warning);
                    return;
                }
                if (rdbHelpExitEntryReq.Checked == true)
                {
                    if (ddlHelpNoDays.SelectedItem.ToString() == "--Select--")
                    {
                        AlertMsg.MsgBox(this.Page, "Please Select No Of days!", AlertMsg.MessageType.Warning);
                        return;
                    }
                }
                if (rdbHelpGuarantorReq.Checked == true)
                {
                    if (txtHelpSearchEmpApr.Text == "")
                    {
                        AlertMsg.MsgBox(this.Page, "Please provide Guarantor Id!", AlertMsg.MessageType.Warning);
                        return;
                    }
                }
                foreach (GridViewRow gvr in gdvAttend.Rows)
                {
                    CheckBox chk = (CheckBox)gvr.FindControl("chkAll");
                    if (chk.Checked)
                    {
                        Label lblEmpID = (Label)gvr.Cells[1].FindControl("lblEmpID");
                        DropDownList grdddlLeaveType = (DropDownList)gvr.Cells[11].FindControl("grdddlLeaveType");
                        DropDownList ddlLeaveType = (DropDownList)gvr.Cells[10].FindControl("ddlLeaveType");
                        Label lblApplyDays = (Label)gvr.Cells[6].FindControl("txtNoofDays");
                        double balLeaves = 0;
                        if (grdddlLeaveType.Items.Count > 0)
                        {
                            string str = grdddlLeaveType.SelectedItem.ToString();
                            int stratPOS = str.IndexOf('(');
                            if (stratPOS > 0)
                            {
                                int EndPOS = str.IndexOf(')');
                                balLeaves = Convert.ToDouble(str.Substring(stratPOS + 1, (EndPOS - stratPOS - 1)).ToString());
                                if(lblApplyDays.Text=="")
                                {
                                    AlertMsg.MsgBox(Page, "Please select From Date and To Date..", AlertMsg.MessageType.Warning);
                                    return;
                                }
                                if ((Convert.ToInt32(lblApplyDays.Text)) > balLeaves)
                                {
                                    string Message = "EmpID: " + lblEmpID.Text + "  " + str.Substring(0, stratPOS) + " balance  is not sufficient";
                                    AlertMsg.MsgBox(Page, Message, AlertMsg.MessageType.Info);
                                    return;
                                }
                            }
                        }
                        if(ddlLeaveType.Items.Count>0 && ddlLeaveType.SelectedValue=="1")
                        {
                          
                            decimal AlBal = 0;
                            SqlParameter[] objParam = new SqlParameter[1];
                            objParam[0] = new SqlParameter("@EmpID", lblEmpID.Text);
                            DataSet ds = SqlHelper.ExecuteDataset("SP_GetAlBalonGrade", objParam);
                           
                            if (ds != null && ds.Tables.Count > 0)
                            {
                                if (lblALBalValue.Text != "" && lblALBalValue.Text != "0")
                                    AlBal = Convert.ToDecimal(lblALBalValue.Text);
                                else
                                {
                                    string Message = "EmpID: " + lblEmpID.Text + " AL  balance  is not sufficient for applied  Days";
                                    AlertMsg.MsgBox(Page, Message, AlertMsg.MessageType.Warning);
                                    return;
                                }
                                if (Convert.ToDecimal(ds.Tables[0].Rows[0][1]) > AlBal)
                                {
                                    string Message = "EmpID: " + lblEmpID.Text + " AL  balance  is not sufficient for applied  Days";
                                    AlertMsg.MsgBox(Page, Message, AlertMsg.MessageType.Warning);
                                    return;
                                }
                            }
                            else
                            {
                                string Message = "EmpID: " + lblEmpID.Text + " AL  balance  is not sufficient for applied  Days";
                                AlertMsg.MsgBox(Page, Message, AlertMsg.MessageType.Warning);
                                return;

                            }
                            //if (lblALBalValue.Text != "" && lblALBalValue.Text != "0")
                            //    AlBal = Convert.ToDecimal(lblALBalValue.Text);
                            //if(((Convert.ToDecimal(lblApplyDays.Text))*90/100)>AlBal)
                            //{
                            //    string Message = "EmpID: " + lblEmpID.Text + " AL  balance  is not sufficient for applied  Days";
                            //    AlertMsg.MsgBox(Page, Message, AlertMsg.MessageType.Warning);
                            //    return;
                            //}
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
                            DropDownList grdddlLeaveType = (DropDownList)gvr.Cells[11].FindControl("grdddlLeaveType");
                            DropDownList ddlLeaveType = (DropDownList)gvr.Cells[10].FindControl("ddlLeaveType");
                            if (txtAppOn.Text != "")
                            {
                                try
                                {
                                    DateTime AppliedOn = CodeUtil.ConvertToDate_ddMMMyyy(txtAppOn.Text, CodeUtil.DateFormat.ddMMMyyyy);
                                    TextBox txtGrntFrm = (TextBox)gvr.Cells[4].FindControl("txtGrsntFrm");
                                    TextBox txtLeaveFrm = (TextBox)gvr.Cells[4].FindControl("txtGrsntFrm");
                                    TextBox txtGrntUtl = (TextBox)gvr.Cells[5].FindControl("txtGrsntUtl");
                                    if(txtGrntFrm.Text=="")
                                    {
                                        AlertMsg.MsgBox(Page, "Please select Applied From Date!", AlertMsg.MessageType.Warning);
                                        return;
                                    }
                                    else if (txtGrntUtl.Text == "")
                                    {
                                        AlertMsg.MsgBox(Page, "Please select Applied Untill Date!", AlertMsg.MessageType.Warning);
                                        return;
                                    }
                                    if (txtGrntFrm.Text != "")
                                    {
                                        try
                                        {
                                            DateTime GrantedFrom = CodeUtil.ConvertToDate_ddMMMyyy(txtGrntFrm.Text, CodeUtil.DateFormat.ddMMMyyyy);
                                            DateTime txtGrsntUtl = CodeUtil.ConvertToDate_ddMMMyyy(txtGrntUtl.Text, CodeUtil.DateFormat.ddMMMyyyy);

                                            DateTime LeaveFrom = CodeUtil.ConvertToDate_ddMMMyyy(txtLeaveFrm.Text, CodeUtil.DateFormat.ddMMMyyyy);

                                            DateTime TodayDate = CodeUtil.ConvertToDate_ddMMMyyy(txtLeaveFrm.Text, CodeUtil.DateFormat.ddMMMyyyy);

                                           // TextBox txtGrntUtl = (TextBox)gvr.Cells[5].FindControl("txtGrsntUtl");

                                            if (Convert.ToDateTime(TodayDate) > Convert.ToDateTime(LeaveFrom))
                                            {

                                                AlertMsg.MsgBox(Page, "From date Back date not allowed!", AlertMsg.MessageType.Warning);
                                                return;
                                            }
                                            if (Convert.ToInt32(Session["UserId"])!=1 && ddlLeaveType.SelectedItem.Value == "2")
                                            {
                                                DateTime dt = DateTime.Now;

                                                if ((LeaveFrom.Date - dt.Date).Days < 44)
                                                {
                                                    AlertMsg.MsgBox(Page, "Leave should be Applied 45 Days prior to the From Date.", AlertMsg.MessageType.Warning);
                                                    return;

                                                }
                                            }
                                            if (Convert.ToDateTime(TodayDate) > Convert.ToDateTime(txtGrntUtl.Text))
                                            {

                                                AlertMsg.MsgBox(Page, "Todate Back date not allowed!", AlertMsg.MessageType.Warning);
                                                return;
                                            }
                                            //if (dtDay.DayOfWeek.ToString().Equals("Friday")) -- 23 LeaveFrom
                                            if (Convert.ToInt32(grdddlLeaveType.SelectedItem.Value)==23)
                                            {
                                                if (GrantedFrom.DayOfWeek.ToString().Equals("Wednesday") || GrantedFrom.DayOfWeek.ToString().Equals("Thursday") || GrantedFrom.DayOfWeek.ToString().Equals("Friday"))
                                                {

                                                }else
                                                {
                                                    AlertMsg.MsgBox(Page, "Applied From Day should Be Wednesday or Thursday or Friday.", AlertMsg.MessageType.Warning);
                                                    return;
                                                }
                                                TimeSpan t = txtGrsntUtl - GrantedFrom;
                                                double NrOfDays = t.TotalDays;
                                                if(NrOfDays+1!=3)
                                                {
                                                    AlertMsg.MsgBox(Page, "For Umra Leave No of Days  Must be 3 Days.", AlertMsg.MessageType.Warning);
                                                    return;
                                                }
                                            }

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
                                                            string Filename = UploadProof1.PostedFile.FileName.ToLower();
                                                            extension = System.IO.Path.GetExtension(UploadProof1.PostedFile.FileName).ToLower();
                                                        }
                                                        string Proof = MyString + extension;

                                                        TextBox txtReason = (TextBox)gvr.Cells[7].FindControl("txtReason");
                                                        DropDownList grdddlLeaveType1 = (DropDownList)gvr.Cells[11].FindControl("grdddlLeaveType");
                                                        DropDownList ddlLeaveType1 = (DropDownList)gvr.Cells[10].FindControl("ddlLeaveType");
                                                        int OutPut = HR_InsUpLeaveApplication_SaveProof(objHrCommon, EmpID, AppliedOn, LeaveFrom, LeaveUntil, txtReason.Text, Convert.ToInt32(ViewState["Status"]), Convert.ToInt32(grdddlLeaveType1.SelectedItem.Value), Proof, HelpAirTicket, Convert.ToInt32(ddlHelpFrPlace.SelectedItem.Value), Convert.ToInt32(ddlHelpToPlace.SelectedItem.Value), HelpExitReEntry, Convert.ToInt32(ddlHelpNoDays.SelectedItem.Value), HelpGuarantor, txtHelpSearchEmpApr.Text,Convert.ToInt16(ddlLeaveType1.SelectedItem.Value));

                                                        //objHrCommon.LID = Convert.ToInt32(ViewState["LID"]);
                                                        LID = objHrCommon.LID;
                                                        if (OutPut == 1)
                                                        {
                                                            if (LID > 0)
                                                            {
                                                                if (UploadProof1.HasFile)
                                                                {
                                                                    string Filename = UploadProof1.PostedFile.FileName.ToLower();
                                                                    extension = System.IO.Path.GetExtension(UploadProof1.PostedFile.FileName).ToLower();
                                                                    string storePath = Server.MapPath("~") + "/" + "HMS/LeaveApplications/" + Convert.ToInt32(LID);
                                                                    UploadProof1.PostedFile.SaveAs(storePath + extension);
                                                                }
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
                                                            Emp_hid.Value = "";
                                                            //gdvAttend.DataSource = null;
                                                            //gdvAttend.DataBind();
                                                           



                                                        }
                                                        else if (OutPut == 2)
                                                            AlertMsg.MsgBox(Page, "Already leave applied between these dates.!", AlertMsg.MessageType.Error);
                                                        chk.Checked = false;
                                                        txtGrntFrm.Text = "";
                                                        txtGrntUtl.Text = "";

                                                    }
                                                    else
                                                    {
                                                        AlertMsg.MsgBox(Page, "Check Dates", AlertMsg.MessageType.Warning);
                                                    }


                                                }
                                                catch (Exception)
                                                {

                                                    //throw ex.InnerException;
                                                    AlertMsg.MsgBox(Page, "Please select proper date.!", AlertMsg.MessageType.Error);
                                                    return;
                                                }
                                            }
                                            else
                                            {
                                                AlertMsg.MsgBox(Page, "Please select granted untill.!", AlertMsg.MessageType.Warning);
                                                return;
                                            }

                                        }   //for granted frm
                                        catch (Exception)   //for granted from
                                        {
                                            AlertMsg.MsgBox(Page, "Please select  date.!", AlertMsg.MessageType.Warning);
                                            return;
                                        }
                                    }
                                    else
                                    {
                                        AlertMsg.MsgBox(Page, "Please select granted from.!", AlertMsg.MessageType.Warning);
                                        return;
                                    }
                                }   //try for applied on
                                catch (Exception)  //for applied on
                                {
                                    AlertMsg.MsgBox(Page, "Please select proper date.!", AlertMsg.MessageType.Warning);
                                    return;
                                }
                            }
                            else
                            {
                                AlertMsg.MsgBox(Page, "Please select appliedon.!", AlertMsg.MessageType.Warning);
                                return;
                            }
                        }
                    }

                }
                catch (Exception ex1)
                {
                    AlertMsg.MsgBox(Page, ex1.Message.ToString(), AlertMsg.MessageType.Error);
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
                        string Filename = UploadProof1.PostedFile.FileName.ToLower();
                        extension = System.IO.Path.GetExtension(UploadProof1.PostedFile.FileName).ToLower();
                        string storePath = Server.MapPath("~") + "/" + "HMS/LeaveApplications/" + Convert.ToInt32(LID);
                        UploadProof1.PostedFile.SaveAs(storePath + extension);
                    }
                    string Proof = MyString + extension;
                }
            }
            catch (Exception ex)
            {
                clsErrorLog.HMSEventLog(ex, "AdvancedLeaveApplication", "gdvattend_RowCommand", "007");

            }


        }

        public void BindEmpList()
        {
            string EmpName = string.Empty;
            int WorkSite = Convert.ToInt32(0);// Default HO
            int Dept = Convert.ToInt32(null);
            DataSet ds = AttendanceDAC.HR_SearchEmpBySiteDept(WorkSite, Dept, "y", Convert.ToInt32(Session["CompanyID"]));
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
                    txtappon.Text = DateTime.Now.ToString("dd MMM yyyy");

                    DataSet ds = new DataSet();
                    DataTable dt = new DataTable();
                    LinkButton lblLAvl = (LinkButton)e.Row.FindControl("lblLAvl");
                    string strLEaves = "";

                    Label lblEmpId = (Label)e.Row.FindControl("lblEmpID");
                    decimal TotalLeaves = 0;
                    if (lblEmpId != null)
                    {
                        int LEmpId = Convert.ToInt32(lblEmpId.Text);
                        DropDownList ddlLeaveType = (DropDownList)e.Row.FindControl("grdddlLeaveType");
                        DropDownList drpLeaveType = (DropDownList)e.Row.FindControl("ddlLeaveType");
                        if (ddlLeaveType != null)
                        {
                            ds = BindGrdLeaveTypesBasedOnGender(LEmpId);
                            ddlLeaveType.DataSource = ds.Tables[0];
                            ddlLeaveType.DataTextField = "Name1";
                            ddlLeaveType.DataValueField = "LeaveType";
                            ddlLeaveType.DataBind();
                            drpLeaveType.DataSource = ds.Tables[1];
                            drpLeaveType.DataTextField = "Name";
                            drpLeaveType.DataValueField = "TypeId";
                            drpLeaveType.DataBind();
                            if (ds != null && ds.Tables.Count > 2)
                                lblALBalValue.Text = ds.Tables[2].Rows[0]["ALBal"].ToString();
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
       
        protected void ddlLeaveType_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddlLeaveType = (DropDownList)sender;
            GridViewRow thisGridViewRow = (GridViewRow)ddlLeaveType.Parent.Parent;
            DropDownList grdddlLeaveType = (DropDownList)thisGridViewRow.Cells[11].FindControl("grdddlLeaveType");
            if (ddlLeaveType.SelectedValue == "1")
                grdddlLeaveType.SelectedValue = "5";
            else if (ddlLeaveType.SelectedValue == "2")
                grdddlLeaveType.SelectedValue = "13";
            else if(ddlLeaveType.SelectedValue == "3")
                grdddlLeaveType.SelectedValue = "22";
            else if(ddlLeaveType.SelectedValue == "4")
                grdddlLeaveType.SelectedValue = "13";
            else if(ddlLeaveType.SelectedValue == "5")
                grdddlLeaveType.SelectedValue = "4";
            else if(ddlLeaveType.SelectedValue == "6")
                grdddlLeaveType.SelectedValue = "23";
            else
                grdddlLeaveType.SelectedValue = "13";

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

            DataTable dt = ds.Tables[0];
            List<string> items = new List<string>(count);
            var rtval = new Dictionary<string, string>();
            foreach (DataRow row in dt.Rows)
            {
                string str = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(row["Name"].ToString(), row["ID"].ToString());
                items.Add(str);
            }

            return items.ToArray(); ;
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
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionList_Employee(string prefixText, int count, string contextKey)
        {
            DataSet ds = AttendanceDAC.HMS_Service_DLL_Employee_By_WS_Dept_googlesearch(prefixText.Trim(), 0, 0);//WSID
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

        protected void rdbTktCompany_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbTktCompany.Checked == true)
            {
                AirTicket = "Company Ticket";
                ddlFrPlace.Enabled = true;
                ddlToPlace.Enabled = true;
                ddlFrPlace.SelectedIndex = 0;
                ddlToPlace.SelectedIndex = 0;
            }
        }

        protected void rdbTktSelf_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbTktSelf.Checked == true)
            {
                AirTicket = "Self Ticket";
                ddlFrPlace.Enabled = true;
                ddlToPlace.Enabled = true;
                ddlFrPlace.SelectedIndex = 0;
                ddlToPlace.SelectedIndex = 0;
            }
        }

        protected void rdbHelpTktCompany_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbHelpTktCompany.Checked == true)
            {
                HelpAirTicket = "Company Ticket";
                ddlHelpFrPlace.Enabled = true;
                ddlHelpToPlace.Enabled = true;
            }
        }

        protected void rdbHelpTktSelf_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbHelpTktSelf.Checked == true)
            {
                HelpAirTicket = "Self Ticket";
                ddlHelpFrPlace.Enabled = true;
                ddlHelpToPlace.Enabled = true;
            }
        }
        protected void rdbNone_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbNone.Checked == true)
            {
                AirTicket = "No Ticket";
                ddlFrPlace.SelectedValue = "0";
                ddlToPlace.SelectedValue = "0";
                ddlFrPlace.Enabled = false;
                ddlToPlace.Enabled = false;
            }
            else
            {
                ddlFrPlace.Enabled = true;
                ddlToPlace.Enabled = true;
            }
        }

        protected void rdbHelpNone_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbHelpNone.Checked == true)
            {
                HelpAirTicket = "No Ticket";
                ddlHelpFrPlace.SelectedValue = "0";
                ddlHelpToPlace.SelectedValue = "0";
                ddlHelpFrPlace.Enabled = false;
                ddlHelpToPlace.Enabled = false;
            }
            else
            {
                ddlHelpFrPlace.Enabled = true;
                ddlHelpToPlace.Enabled = true;
            }
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
        protected void rdbHelpGuarantorReq_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbHelpGuarantorReq.Checked == true)
            {
                //lblHelpGuarantorId.Visible = true;
                txtHelpSearchEmpApr.Enabled = true;
                //txtSearchEmpApr.ReadOnly = false;
                txtHelpSearchEmpApr.Text = "";
                txtHelpSearchEmpApr.Focus();
            }
        }

        protected void rdbHelpGuarantorNotReq_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbHelpGuarantorNotReq.Checked == true)
            {
                //lblHelpGuarantorId.Visible = false;
                txtHelpSearchEmpApr.Enabled = false;
                txtHelpSearchEmpApr.Text = HelpGuarantorName.ToString();
                //txtSearchEmpApr.ReadOnly = true;                
            }
        }

        protected void rdbHelpExitEntryReq_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbHelpExitEntryReq.Checked == true)
            {
                ddlHelpNoDays.Enabled = true;
            }
        }

        protected void rdbHelpExitEntryNotReq_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbHelpExitEntryNotReq.Checked == true)
            {
                ddlHelpNoDays.SelectedIndex = 0;
                ddlHelpNoDays.Enabled = false;
            }
        }
        protected void txtGrsntFrm_Click(object sender, EventArgs e)
        {
            try
            {
                TextBox thisTextBox = (TextBox)sender;
                GridViewRow thisGridViewRow = (GridViewRow)thisTextBox.Parent.Parent;
                Label lblEmpID = (Label)thisGridViewRow.FindControl("lblEmpID");
                TextBox txtGrsntFrm = (TextBox)thisGridViewRow.FindControl("txtGrsntFrm");
                //if (txtGrsntFrm.Text.Trim() == "" )
                //    txtNoofDays.Text = "N/A";
                //else
                //{

                    DateTime dtGrsntFrm = CodeUtil.ConvertToDate_ddMMMyyy(txtGrsntFrm.Text, CodeUtil.DateFormat.ddMMMyyyy);
                if (lblEmpID.Text != string.Empty)
                {
                    string strLEaves = "";

                    decimal TotalLeaves = 0;
                    SqlParameter[] objParam = new SqlParameter[2];
                    objParam[0] = new SqlParameter("@EmpID", lblEmpID.Text);
                    if (txtGrsntFrm.Text.Trim() != "")
                        objParam[1] = new SqlParameter("@FromDate", txtGrsntFrm.Text);

                    DataSet ds = SqlHelper.ExecuteDataset("T_G_Leaves_GetTypeOfLeavesList_BasedOnDate", objParam);
                    int TableCount = ds.Tables.Count;
                    DropDownList grdddlLeaveType = (DropDownList)thisGridViewRow.FindControl("grdddlLeaveType");
                    DropDownList ddlLeaveType = (DropDownList)thisGridViewRow.FindControl("ddlLeaveType");
                    if (ddlLeaveType != null)
                    {
                        grdddlLeaveType.DataSource = ds.Tables[0];
                        grdddlLeaveType.DataTextField = "Name1";
                        grdddlLeaveType.DataValueField = "LeaveType";
                        grdddlLeaveType.DataBind();
                        if (ds != null && ds.Tables.Count > 1)
                            lblALBalValue.Text = ds.Tables[1].Rows[0]["ALBal"].ToString();
                        else
                            lblALBalValue.Text = "0";
                        if (ddlLeaveType.SelectedValue == "1")
                            grdddlLeaveType.SelectedValue = "5";
                        else if (ddlLeaveType.SelectedValue == "2")
                            grdddlLeaveType.SelectedValue = "13";
                        else if (ddlLeaveType.SelectedValue == "3")
                            grdddlLeaveType.SelectedValue = "22";
                        else if (ddlLeaveType.SelectedValue == "4")
                            grdddlLeaveType.SelectedValue = "13";
                        else if (ddlLeaveType.SelectedValue == "5")
                            grdddlLeaveType.SelectedValue = "4";
                        else if (ddlLeaveType.SelectedValue == "6")
                            grdddlLeaveType.SelectedValue = "23";
                        else
                            grdddlLeaveType.SelectedValue = "13";

                        DataTable dt = ds.Tables[0];
                        foreach (DataRow dr in dt.Rows) // Loop over the rows.
                        {
                            if (dr["Leavebalance"].ToString() != "0.00")
                            {
                                strLEaves = strLEaves + dr["ShortName"].ToString() + "\t " + dr["Leavebalance"].ToString() + "\n";
                                TotalLeaves = TotalLeaves + Convert.ToDecimal(dr["Leavebalance"].ToString());
                            }

                        }
                        thisGridViewRow.Cells[9].ToolTip = strLEaves;
                        LinkButton lblLAvl = (LinkButton)thisGridViewRow.FindControl("lblLAvl");

                        if (lblLAvl != null)
                            lblLAvl.Text = TotalLeaves.ToString("0.00");
                    }

                }
                else
                    AlertMsg.MsgBox(Page, "Check FromDate", AlertMsg.MessageType.Warning);
            //}
            }
            catch (Exception ex)
            {
                clsErrorLog.HMSEventLog(ex, "AdvancedLeaveApplication", "txtGrsntFrm_Click", "011");

            }
          //  tblTravel.Visible = false;
           // tblTravelHelpOthers.Visible = false;
        }

        protected void Unnamed_Click(object sender, EventArgs e)
        {
            try
            {
                TextBox thisTextBox = (TextBox)sender;
                GridViewRow thisGridViewRow = (GridViewRow)thisTextBox.Parent.Parent;
                Label lblEmpID = (Label)thisGridViewRow.FindControl("lblEmpID");
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
                        return;
                    }
                    TimeSpan t = dtGrsntUtl - dtGrsntFrm;
                    double NrOfDays = t.TotalDays;
                    txtNoofDays.Text = (NrOfDays + 1).ToString();
                }
                //if (lblEmpID.Text != string.Empty && txtNoofDays.Text != string.Empty && txtGrsntFrm.Text != string.Empty && txtGrsntUtl.Text != string.Empty)
                //{
                //    string strLEaves = "";

                //    decimal TotalLeaves = 0;
                //    SqlParameter[] objParam = new SqlParameter[4];
                //    objParam[0] = new SqlParameter("@EmpID", lblEmpID.Text);
                //    objParam[1] = new SqlParameter("@UID", Convert.ToInt32(Session["UserId"]));
                //    objParam[2] = new SqlParameter("@FromDate", txtGrsntFrm.Text);
                //    objParam[3] = new SqlParameter("@ToDate", txtGrsntUtl.Text);

                //    DataSet ds = SqlHelper.ExecuteDataset("T_G_Leaves_GetTypeOfLeavesList_BasedOnGender_Employeeportal_new", objParam);
                //    int TableCount = ds.Tables.Count;
                //    DropDownList ddlLeaveType = (DropDownList)thisGridViewRow.FindControl("grdddlLeaveType");
                //    if (ddlLeaveType != null)
                //    {
                //        ddlLeaveType.DataSource = ds.Tables[TableCount - 1];
                //        ddlLeaveType.DataTextField = "Name1";
                //        ddlLeaveType.DataValueField = "LeaveType";
                //        ddlLeaveType.DataBind();
                //        DataTable dt = ds.Tables[TableCount - 1];
                //        foreach (DataRow dr in dt.Rows) // Loop over the rows.
                //        {
                //            if (dr["Leavebalance"].ToString() != "0.00")
                //            {
                //                strLEaves = strLEaves + dr["ShortName"].ToString() + "\t " + dr["Leavebalance"].ToString() + "\n";
                //                TotalLeaves = TotalLeaves + Convert.ToDecimal(dr["Leavebalance"].ToString());
                //            }

                //        }
                //        thisGridViewRow.Cells[9].ToolTip = strLEaves;
                //        LinkButton lblLAvl = (LinkButton)thisGridViewRow.FindControl("lblLAvl");

                //        if (lblLAvl != null)
                //            lblLAvl.Text = TotalLeaves.ToString("0.00");
                //    }

                //}
                //else
                //    AlertMsg.MsgBox(Page, "Check FromDate/ToDate", AlertMsg.MessageType.Warning);


            }
            catch (Exception ex)
            {
                clsErrorLog.HMSEventLog(ex, "AdvancedLeaveApplication", "Unnamed_Click", "011");

            }

        }
    }
}