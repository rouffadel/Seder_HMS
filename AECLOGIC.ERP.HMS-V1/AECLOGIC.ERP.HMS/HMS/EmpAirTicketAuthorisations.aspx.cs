using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using AECLOGIC.HMS.BLL;
using AECLOGIC.ERP.COMMON;
using System.Text;
using System.Data.SqlClient;
using Aeclogic.Common.DAL;
using AECLOGIC.ERP.HMS;

namespace AECLOGIC.ERP.HMSV1
{
    public partial class EmpAirTicketAuthorisationsV1 : AECLOGIC.ERP.COMMON.WebFormMaster
    {
        int mid = 0;
        bool viewall, Editable;
        string menuname;
        static int Siteid;
        static int SearchCompanyID;
        static int WSiteid;
        static int EDeptid = 0;
        string menuid;
        HRCommon objHrCommon = new HRCommon();
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
                int EmpID = 0;
                 if (rblDesg.SelectedValue == "1")
                {
                    Status = true;
                }
                 if (textempid.Text.Trim() != "")
                 {
                     EmpID = Convert.ToInt32(ddlsemp_hid.Value == "" ? "0" : ddlsemp_hid.Value);
                 }
                 if (Request.QueryString.Count > 1)
                 {
                     if (Request.QueryString["Empid"] != null && Request.QueryString["Empid"] != string.Empty)
                     {
                         EmpID = Convert.ToInt32(Request.QueryString["Empid"].ToString());
                     }
                 }
                int? WSID =null;int? DeptNo=null;
                 if (txtSearchWorksite.Text!= "")
                {
                   WSID = Convert.ToInt32(ddlWorksite_hid.Value);
                }
                 if (txtdeptsearch.Text != "")
                 {
                     DeptNo = Convert.ToInt32(ddlsdepartment_hid.Value);
                 }
                //THIS SP. is used to populate the master Data by Pratap on 19 April 2016   
                //CREATE PROC HMS_EmpAirTicketAuthorisations_aspx
               // ds = AttendanceDAC.T_HMS_empVsAirTicketsAuth_LISTbyID_status(objHrCommon, Status, EmpID,WSID,DeptNo);
            DataSet  ds = AttendanceDAC.T_HMS_empVsAirTicketsAuth_LISTbyID_status_New(objHrCommon, Status, 0, WSID, DeptNo,EmpID);
                if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0)
                {
                    gvRMItem.DataSource = ds;
                    gvRMItem.DataBind();
                    EmpListPaging.Visible = true;
                }
                else
                {
                    EmpListPaging.Visible = false;
                   gvRMItem.DataSource = null;
                    gvRMItem.DataBind();
                }
                EmpListPaging.Bind(objHrCommon.CurrentPage, objHrCommon.TotalPages, objHrCommon.NoofRecords, objHrCommon.PageSize);
                if (Request.QueryString.Count > 1)
                {
                    if (Request.QueryString["Empid"] != null && Request.QueryString["Empid"] != string.Empty)
                    {
                        DisplayUsageDetails();
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            SearchCompanyID = Convert.ToInt32(Session["CompanyID"]);
            if (!IsPostBack)
            {
                GetParentMenuId();
                // added here by pratap date:23-04-2016                
                rowEmpDetails.Visible = false;
                tickimgk.Visible = false;
                notfoundk.Visible = true;
                FIllObject.FillDropDown(ref  ddlfrequency , "PM_BillGenType");
                if (ddlfrequency.Items.Count > 0)
                    ddlfrequency.SelectedIndex = 0;
                BindLocations(ddlfromCity);
                BindLocations(ddlToCity);
                FIllObject.FillDropDown(ref  ddlpassengerType, "get_T_HMS_PassengerType");
                FIllObject.FillDropDown(ref  ddlBookingClass, "get_T_HMS_BookingClass");
                FIllObject.FillDropDown(ref  ddlrelation, "get_T_HMS_Relation");
                ViewState["CateId"] = "";
                if (Convert.ToInt32(Request.QueryString["key"]) == 1)
                {
                    this.Title = "Emp Vs Air Ticket";
                    tblNewk.Visible = true;
                    tblEdit.Visible = false;
                    gvRMItem.Visible = false;
                }
                else
                {
                    tblNewk.Visible = false;
                    tblEdit.Visible = true;
                    gvRMItem.Visible = true;
                    EmployeBind(objHrCommon);
                   // EmployeBind(objHrCommon);
                }
                //// added by pratap date:23-04-2016
                //FirstGridViewRow();
            }
        }
        #region CodeBYkAUSHAL
        private void BindLocations(  DropDownList ddl)
        {
            DataSet ds = AttendanceDAC.CMS_Get_City();
            ddl.DataSource = ds;
            ddl.DataTextField = ds.Tables[0].Columns["CItyName"].ToString();
            ddl.DataValueField = ds.Tables[0].Columns["CityID"].ToString();
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("---Select---", "0"));
            int c = ds.Tables[0].Rows.Count;
            //ddl.Items.Insert(c + 1, new ListItem("New", "-1"));
        }
       protected void txtempid_changed(object sender,EventArgs e)
        {
           if (txtempid.Text.Trim ()!="")
           {
               AttendanceDAC objAtt = new AttendanceDAC();
               int empid = Convert.ToInt32(txtempid.Text.Trim());
               DataSet ds1 = objAtt.GetEmployeeDetails(empid);
               if (ds1.Tables[0].Rows.Count > 0)
               {
               lblempdetails.Text = ds1.Tables[0].Rows[0]["FName"].ToString() + ' ' + ds1.Tables[0].Rows[0]["MName"].ToString() + ' ' + ds1.Tables[0].Rows[0]["LName"].ToString();
                   tickimgk.Visible = true;
                   notfoundk.Visible = false;
                   lblempdetails.BackColor = System.Drawing.Color.Yellow ;
                   lblempdetails.ForeColor = System.Drawing.Color.Green ;
               }
               else
               {
                   lblempdetails.Text = "Employee Id: [" + txtempid.Text.Trim() + "] Not found or Invalid!";
                   lblempdetails.ForeColor = System.Drawing.Color.Red;
                   lblempdetails.BackColor = System.Drawing.Color.Yellow;
                   txtempid.Text = "";
                   tickimgk.Visible = false ;
                   notfoundk.Visible = true ;
               }
           }
        }
        #endregion
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
                //gvRMItem.Columns[1].Visible = Convert.ToBoolean(ds.Tables[0].Rows[0]["Editable"].ToString());
               // btnSubmit.Enabled = Editable = Convert.ToBoolean(ds.Tables[0].Rows[0]["Editable"].ToString());
            }
            return MenuId;
        }
        public void VsAirTicketAccount_Sync(int ID)
        {
            int res = 0;
            res=AttendanceDAC.T_HMS_empVsAirTicketAccount_Sync(ID);
            if (res==1)
            {
                AlertMsg.MsgBox(Page, "Ticket Account updated");
            }
        }
        public void BindDetails(int ID)
        {
            try
            {
                objHrCommon.PageSize = 10;
                objHrCommon.CurrentPage = 1;
                // pratap date:06-07-2016
                DataSet ds = AttendanceDAC.T_HMS_empVsAirTicketsAuth_LISTbyID_status_New(objHrCommon, true, ID, null, null, null);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    txtempid.Text = ds.Tables[0].Rows[0][2].ToString();
                    AttendanceDAC objAtt = new AttendanceDAC();
                    int empid = Convert.ToInt32(txtempid.Text.Trim());
                    DataSet ds1 = objAtt.GetEmployeeDetails(empid);
                    if (ds1.Tables[0].Rows.Count > 0)
                    {
                        lblempdetails.Text = ds1.Tables[0].Rows[0]["FName"].ToString() + ' ' + ds1.Tables[0].Rows[0]["MName"].ToString() + ' ' + ds1.Tables[0].Rows[0]["LName"].ToString();
                        tickimgk.Visible = true;
                        notfoundk.Visible = false;
                        lblempdetails.BackColor = System.Drawing.Color.Yellow;
                        lblempdetails.ForeColor = System.Drawing.Color.Green;
                    }
                    tblEdit.Visible = false;
                    tblNewk.Visible = true;
                    // txtempid.Text = ds.Tables[0].Rows[0][2].ToString();
                    ddlrelation.SelectedValue = ds.Tables[0].Rows[0][3].ToString();
                    ddlpassengerType.SelectedValue = ds.Tables[0].Rows[0][4].ToString();
                    ddlBookingClass.SelectedValue = ds.Tables[0].Rows[0][5].ToString();
                    ddlfromCity.SelectedValue = ds.Tables[0].Rows[0][6].ToString();
                    ddlToCity.SelectedValue = ds.Tables[0].Rows[0][7].ToString();
                    ddlfrequency.SelectedValue = ds.Tables[0].Rows[0][8].ToString();
                    // added here Tickets by pratap date:19-04-2016
                    txtTickets.Text = ds.Tables[0].Rows[0]["Tickets"].ToString();
                    btnSubmit.Text = "Update";
                }
            }
            catch { }
        }
        protected void btn_reste_Click(object sender,EventArgs e)
        {
            ddlfromCity.SelectedIndex =0;
            ddlToCity.SelectedIndex = 0;
            ddlpassengerType.SelectedIndex = 0;
            ddlBookingClass.SelectedIndex = 0;
            txtempid.Text = "";
            lblempdetails.Text = "";
            ddlfrequency.SelectedIndex = 0;
            ddlrelation.SelectedIndex = 0;
            btnSubmit.Text = "Submit";
            ViewState["CateId"] = "0";
        }
        protected void gvWages_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //rowEmpDetails.Visible = false;
            int ID = Convert.ToInt32(e.CommandArgument);
            ViewState["CateId"] = ID;
            bool Status = true;
            if (rblDesg.SelectedValue == "1")
            {
                Status = false;
            }
            if (e.CommandName == "ViewPage")
            {
                GridViewRow Row = (GridViewRow)(((Button)e.CommandSource).NamingContainer);
                int row = Row.RowIndex;
                Label lblTO_cityID = (Label)gvRMItem.Rows[row].FindControl("lblTO_cityID");
                Label lblfrom_cityID = (Label)gvRMItem.Rows[row].FindControl("lblfrom_cityID");
                Label lblbookingClassID = (Label)gvRMItem.Rows[row].FindControl("lblbookingClassID");
                Label lblPassengerTypeID = (Label)gvRMItem.Rows[row].FindControl("lblPassengerTypeID");
                int ToCityID = 0; int FromCityID = 0; int BookingClassID = 0; int passegerid = 0;
                if (lblTO_cityID.Text!="")
                    ToCityID= Convert.ToInt32(lblTO_cityID.Text);
                if (lblfrom_cityID.Text != "")
                    FromCityID = Convert.ToInt32(lblfrom_cityID.Text);
                if (lblbookingClassID.Text != "")
                    BookingClassID = Convert.ToInt32(lblbookingClassID.Text);
                if (lblPassengerTypeID.Text != "")
                    passegerid = Convert.ToInt32(lblPassengerTypeID.Text);
                string url = "ConfigAirTicket.aspx?key=1&FromCityID=" + FromCityID + "&ToCityID=" + ToCityID + " &BookingID= " + BookingClassID + " &passegerid= " + passegerid;
                string fullURL = "window.open('" + url + "', '_blank' );";
                ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", fullURL, true);
            }
            else if (e.CommandName == "Edt")
            {
              BindDetails(ID);
            }
            else if (e.CommandName == "LVRD")
            {
                VsAirTicketAccount_Sync(ID);
                EmployeBind(objHrCommon);
            }
            // added here by pratap date:21-04-2016
            // when click amount link button it will display the grid
            else if (e.CommandName == "Amount")
            {
                // added by pratap date:23-04-2016                
                GridViewRow oItem = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
                int row = oItem.RowIndex;
                Label lblName = (Label)gvRMItem.Rows[row].FindControl("Label3");
                Label lblProfilerType = (Label)gvRMItem.Rows[row].FindControl("lblProfilerType");
                Label Label1 = (Label)gvRMItem.Rows[row].FindControl("Label1");
                Label Label7 = (Label)gvRMItem.Rows[row].FindControl("Label7");
                Label lblEmpidNew = (Label)gvRMItem.Rows[row].FindControl("lblEmpidNew");
                Label lblTO_cityID = (Label)gvRMItem.Rows[row].FindControl("lblTO_cityID");
                Label Label115 = (Label)gvRMItem.Rows[row].FindControl("Label115");
                //if(Convert.ToInt32(Label115.Text)<1)
                //{
                //    AlertMsg.MsgBox(Page, "Ticktets ");
                //    return;
                //}
                int Sync = AttendanceDAC.GetSalary_Syncd(Convert.ToInt32(lblEmpidNew.Text));
                if (Sync==0)
                {
                    AlertMsg.MsgBox(Page, "Check your latest Salaries are Synced , before you run this process");
                    return;
                }
                lblToCityID.Text = lblTO_cityID.Text;
                lblMinimumWorkingDays.Text = "";
                lblEmpidTwo.Text = "";
                lblEmpName.Text = "";
                lblCity.Text = "";
                lblTickets.Text = "";
                lblEmpName.Text = lblName.Text;
                lblEmpName.Text = lblEmpName.Text;
                lblCity.Text="From city: " + lblProfilerType.Text + "-----To city: " + Label1.Text;
                //lblTickets.Text = Label7.Text;
                lblTickets.Text = Label115.Text;
                lblEmpidTwo.Text = lblEmpidNew.Text;
                DateTime CurrentDate = DateTime.Now;
               // DataSet ds=new DataSet();                
                lblMinimumWorkingDays.Text = AttendanceDAC.GetMinimum_WorkingDaysByEmpid(Convert.ToInt32(lblEmpidTwo.Text)).ToString();                  
                lblDaysOfWork.Text= AttendanceDAC.Get_Total_Working_Days_Emp(Convert.ToInt32(lblEmpidTwo.Text), CurrentDate.ToString("MM/dd/yyyy")).ToString();
                BindTicketDetails(Convert.ToInt32(lblEmpidTwo.Text));
                rowEmpDetails.Visible = true;                
            }
            else
            {
                try
                {
                    //Added By Nazima on 18/04/1992 for Deactivate hyperlink is not working
                    AttendanceDAC.HMS_ActiveInActiveItems(ID, Status, "HR_Upd_empVsAirTicketsAuthStatus");
                    EmployeBind(objHrCommon);
                }
                catch (Exception DelDesig)
                {
                    AlertMsg.MsgBox(Page, DelDesig.Message.ToString(),AlertMsg.MessageType.Error);
                }
            }
        }
        protected void BindTicketDetails(int Empid)
        {
            DataTable dt = new DataTable();
            DataSet ds = AttendanceDAC.Get_EmpVsAirTicketsUseage_Details(Empid);
            if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0)
            {
                gvTicketsInfo.DataSource = ds;
                gvTicketsInfo.DataBind();
                dt = ds.Tables[0];
                ViewState["CurrentTable"] = dt;
                DataRow dr = null;
                AddEmptyRow(dt, dr);
            }
            else
            {
                FirstGridViewRow();
            }
        }
        private void FirstGridViewRow()
        {
            DataTable dt = new DataTable();
            DataRow dr = null;
            dt.Columns.Add(new DataColumn("DET_ID", typeof(string)));
            dt.Columns.Add(new DataColumn("Slno", typeof(string)));
            dt.Columns.Add(new DataColumn("EmpName", typeof(string)));
            dt.Columns.Add(new DataColumn("detrelationID", typeof(string)));            
            dt.Columns.Add(new DataColumn("Relation", typeof(string)));
            dt.Columns.Add(new DataColumn("detPassengerTypeID", typeof(string)));            
            dt.Columns.Add(new DataColumn("PassType", typeof(string)));
            dt.Columns.Add(new DataColumn("detbookingClassID", typeof(string)));                        
            dt.Columns.Add(new DataColumn("BookingClass", typeof(string)));
            //dt.Columns.Add(new DataColumn("CITRFilingStatusDate", typeof(string)));
            dt.Columns.Add(new DataColumn("AirLinesID", typeof(string)));                        
            dt.Columns.Add(new DataColumn("AirLines", typeof(string)));
            dt.Columns.Add(new DataColumn("DueDate", typeof(string)));
            dt.Columns.Add(new DataColumn("NextDueDate", typeof(string)));
            dt.Columns.Add(new DataColumn("Amount", typeof(string)));
            dt.Columns.Add(new DataColumn("RecStatus", typeof(string)));
            dt.Columns.Add(new DataColumn("NoofTickets",typeof(string)));
            dt.Columns.Add(new DataColumn("JVTransID", typeof(string)));
            ViewState["CurrentTable"] = dt;
            AddEmptyRow(dt, dr);
        }
        private void AddEmptyRow(DataTable dt, DataRow dr)
        {
            dr = dt.NewRow();
            dt.Rows.Add(dr);
            gvTicketsInfo.DataSource = dt;
            gvTicketsInfo.DataBind();        
        }

        public string GetText()
        {
            if (rblDesg.SelectedValue == "1")
            {
                return "Deactivate";
            }
            else
            {
                return "Activate";
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                int CateId = 0;
                if (ViewState["CateId"].ToString() != null && ViewState["CateId"].ToString() != string.Empty)
                {
                    CateId = Convert.ToInt32(ViewState["CateId"].ToString());
                }
                SqlParameter[] sqlParams = new SqlParameter[5];
                sqlParams[0] = new SqlParameter("@relationID", Convert.ToInt32(ddlrelation.SelectedValue));
                sqlParams[1] = new SqlParameter("@Passenger_typeID", Convert.ToInt32(ddlpassengerType.SelectedValue));
                sqlParams[2] = new SqlParameter("@Booking_ClassID", Convert.ToInt32(ddlBookingClass.SelectedValue));
                sqlParams[3] = new SqlParameter("@from_CityID", Convert.ToInt32(ddlfromCity.SelectedValue));
                sqlParams[4] = new SqlParameter("@To_CityID", Convert.ToInt32(ddlToCity.SelectedValue));
                DataSet  ds = SQLDBUtil.ExecuteDataset("Hs_ConfigAirTicketCheck", sqlParams);
                if(ds.Tables[0]!=null && ds.Tables[0].Rows.Count>0)
                {
                    lblmsg.Text = "";
                #region submit
                // here passsing the tickets by pratap date:19-04-2016
                int Output = AttendanceDAC.T_HMS_empVsAirTicketsAuth_Ins_upd(CateId, Convert.ToInt32(txtempid.Text.Trim()), 
                    Convert.ToInt32(ddlrelation.SelectedValue), Convert.ToInt32(ddlpassengerType.SelectedValue), Convert.ToInt32(ddlBookingClass.SelectedValue),
                    Convert.ToInt32(ddlfromCity.SelectedValue), Convert.ToInt32(ddlToCity.SelectedValue), Convert.ToInt32 (ddlfrequency.SelectedValue), 
                     Convert.ToInt32(Session["UserId"]), 0, 1,Convert.ToDecimal(txtTickets.Text));
                if (Output == 1)
                {
                    AlertMsg.MsgBox(Page, "Inserted Sucessfully");
                }
                else if (Output == 2)
                {
                    AlertMsg.MsgBox(Page, "Already Exists.!");
                }
                else
                    AlertMsg.MsgBox(Page, "Updated Sucessfully");
                #endregion
                EmpListPaging.CurrentPage = 1;
                EmployeBind(objHrCommon);
                tblNewk.Visible = false;
                tblEdit.Visible = true;
                gvRMItem.Visible = true;
                Clear();
                }
                else
                {
                    lblmsg.Text = "You must config Air Ticket Config befor proceeding";
                }
            }
            catch (Exception AddDesignation)
            {
                AlertMsg.MsgBox(Page, AddDesignation.Message.ToString(),AlertMsg.MessageType.Error);
            }
        }
        public void Clear()
        {
         //   txtName.Text = "";
            ViewState["CateId"] = "";
        }
        protected void lnkAdd_Click(object sender, EventArgs e)
        {
            tblNewk.Visible = true;
            tblEdit.Visible = false;
        }
        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            tblEdit.Visible = true;
            tblNewk.Visible = false;
        }
        protected void rblDesg_SelectedIndexChanged(object sender, EventArgs e)
        {
            EmployeBind(objHrCommon);
        }
        protected void gvRMItem_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton lnkEdt = (LinkButton)e.Row.FindControl("lnkEdit");
                LinkButton lnkDel = (LinkButton)e.Row.FindControl("lnkDel");
               // lnkEdt.Enabled = lnkDel.Enabled = Editable;
            }
        }
        protected void btnADD_Click(object sender, EventArgs e)
        {
            try
            {
                if (ChkTicketsValidation().Equals(0))
                    AddNewRow();
                else
                    return;
            }
            catch (Exception ex)
            {
               // lblMessage.Text = "ButtonAdd_Click :" + ex.Message;
            }
        }
        private int ChkTicketsValidation()
        {
            // modify by pratap date: 06-FEB-2017
            TextBox txtNoofTickets11 = (TextBox)gvTicketsInfo.FooterRow.FindControl("txtNoofTickets1");
            if ((txtNoofTickets11.Text == "0.00") || (txtNoofTickets11.Text == "0"))
            {
                AlertMsg.MsgBox(Page, "Invalid No of Ticktets ");
                return 1;
            }
            string DetIDnew = ""; double nTotlTickets = 0;
            foreach (GridViewRow row in gvTicketsInfo.Rows)
            {
                DetIDnew = ((Label)row.FindControl("lblDETID")).Text;
                if (DetIDnew == "-1")
                {
                    Label lblNoT = ((Label)row.FindControl("lblNoofTickets"));
                    if (lblNoT.Text.Equals(""))
                        lblNoT.Text = "0";
                    nTotlTickets = nTotlTickets + Convert.ToDouble(((Label)row.FindControl("lblNoofTickets")).Text);
                }
            }
            nTotlTickets = nTotlTickets + Convert.ToDouble(txtNoofTickets11.Text);
            if (nTotlTickets > (Convert.ToDouble(lblTickets.Text)))
            {
                AlertMsg.MsgBox(Page, "Limit exceeds!");
                return 1;
            }
            return 0;
        }
        private void AddNewRow()
        {
             try
            {
                if (ViewState["CurrentTable"] != null)
                {
                    DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
                    //NoofTickets
                    TextBox txtNoofTickets = (TextBox)gvTicketsInfo.FooterRow.FindControl("txtNoofTickets1");
                    Label lblSlno = (Label)gvTicketsInfo.FooterRow.FindControl("lblSlno");
                    //txtEmpName                    
                    TextBox txtEmpName = (TextBox)gvTicketsInfo.FooterRow.FindControl("txtEmpName1");
                    //ddlRelation
                    DropDownList ddlRelation = gvTicketsInfo.FooterRow.FindControl("ddlRelation1") as DropDownList;
                    //ddlPassengerType
                    DropDownList ddlPassengerType = gvTicketsInfo.FooterRow.FindControl("ddlPassengerType1") as DropDownList;
                    //ddlBookingClass
                    DropDownList ddlBookingClass = gvTicketsInfo.FooterRow.FindControl("ddlBookingClass1") as DropDownList;
                    //ddlAirLines
                    DropDownList ddlAirLines = gvTicketsInfo.FooterRow.FindControl("ddlAirLines1") as DropDownList;
                    //txtDueDate
                    TextBox txtDueDate = (TextBox)gvTicketsInfo.FooterRow.FindControl("txtDueDate1");
                    //txtNewDueDate
                    TextBox txtNextDueDate1 = (TextBox)gvTicketsInfo.FooterRow.FindControl("txtNextDueDate1");
                    //txtAmount
                    TextBox txtAmount = (TextBox)gvTicketsInfo.FooterRow.FindControl("txtAmount1");
                    dtCurrentTable.Rows.RemoveAt(dtCurrentTable.Rows.Count - 1);
                    double nAmount = 0;
                    //if (ddlRelation.SelectedItem.Text=="Children")
                    //{
                    //    nAmount = Convert.ToDouble(txtAmount.Text) * 0.5;
                    //}
                    //else
                    //{
                        nAmount = Convert.ToDouble(txtAmount.Text);
                    //}
                    dtCurrentTable.Rows.Add(-1, Convert.ToInt32(dtCurrentTable.Rows.Count+1), txtEmpName.Text, ddlRelation.SelectedValue, ddlRelation.SelectedItem.ToString(), 
                        ddlPassengerType.SelectedValue,ddlPassengerType.SelectedItem.ToString(),
                        ddlBookingClass.SelectedValue, ddlBookingClass.SelectedItem.ToString(),
                        ddlAirLines.SelectedValue, ddlAirLines.SelectedItem.ToString(),
                        txtDueDate.Text, txtNextDueDate1.Text,
                        nAmount.ToString(), "NewData", txtNoofTickets.Text,"0");  
                    dtCurrentTable.AcceptChanges();
                    gvTicketsInfo.DataSource = dtCurrentTable;
                    gvTicketsInfo.DataBind();
                    DataRow drCurrentRow1 = dtCurrentTable.NewRow();
                    dtCurrentTable.Rows.Add(drCurrentRow1);                    
                    ViewState["CurrentTable"] = dtCurrentTable;
                    txtEmpName.Text = "";
                    txtEmpName.Focus();
                }
                else
                {
                    //Response.Write("ViewState is null");
                }
            }
            catch (Exception ex)
            {
                //lblMessage.Text = "AddNewRow: " + ex.Message;
            }
        }
        protected void gvTicketsInfo_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                if (ViewState["CurrentTable"] != null)
                {
                    DataTable dt = (DataTable)ViewState["CurrentTable"];
                    int rowIndex = Convert.ToInt32(e.RowIndex);
                    if (dt.Rows.Count > 0)
                    {
                        //lblRecStatus
                        Label lblRecStatus = gvTicketsInfo.Rows[e.RowIndex].FindControl("lblRecStatus") as Label;
                        //lblDETID
                        Label lblDETID = gvTicketsInfo.Rows[e.RowIndex].FindControl("lblDETID") as Label;
                        //txtEmpName
                        TextBox txtEmpName = gvTicketsInfo.Rows[e.RowIndex].FindControl("txtEmpName") as TextBox;
                        //ddlRelation                    
                        DropDownList ddlRelation = gvTicketsInfo.Rows[e.RowIndex].FindControl("ddlRelation") as DropDownList;
                        //ddlPassengerType
                        DropDownList ddlPassengerType = gvTicketsInfo.Rows[e.RowIndex].FindControl("ddlPassengerType") as DropDownList;
                        //ddlBookingClass
                        DropDownList ddlBookingClass = gvTicketsInfo.Rows[e.RowIndex].FindControl("ddlBookingClass") as DropDownList;
                        //ddlAirLines
                        DropDownList ddlAirLines = gvTicketsInfo.Rows[e.RowIndex].FindControl("ddlAirLines") as DropDownList;
                        //txtDueDate
                        TextBox txtDueDate = gvTicketsInfo.Rows[e.RowIndex].FindControl("txtDueDate") as TextBox;
                        //txtDueDate
                        TextBox txtNextDueDate = gvTicketsInfo.Rows[e.RowIndex].FindControl("txtNextDueDate") as TextBox;
                        //txtAmount
                        TextBox txtAmount = gvTicketsInfo.Rows[e.RowIndex].FindControl("txtAmount") as TextBox;
                        //txtNoofTickets
                        TextBox txtNoofTickets = gvTicketsInfo.Rows[e.RowIndex].FindControl("txtNoofTickets") as TextBox;
                        //
                        dt.Rows[rowIndex]["RecStatus"] = "UpdateData";
                        dt.Rows[rowIndex]["DET_ID"] = lblDETID.Text;
                        dt.Rows[rowIndex]["EmpName"] = txtEmpName.Text;                        
                        dt.Rows[rowIndex]["detrelationID"] =ddlRelation.SelectedValue;
                        dt.Rows[rowIndex]["Relation"] = ddlRelation.SelectedItem.ToString();
                        dt.Rows[rowIndex]["detPassengerTypeID"] = ddlPassengerType.SelectedValue;
                        dt.Rows[rowIndex]["PassType"] = ddlPassengerType.SelectedItem.ToString();
                        dt.Rows[rowIndex]["detbookingClassID"] = ddlBookingClass.SelectedValue;
                        dt.Rows[rowIndex]["BookingClass"]=ddlBookingClass.SelectedItem.ToString();
                        dt.Rows[rowIndex]["AirLinesID"]=ddlAirLines.SelectedValue;
                        dt.Rows[rowIndex]["AirLines"] = ddlAirLines.SelectedItem.ToString();
                        dt.Rows[rowIndex]["DueDate"] = txtDueDate.Text;
                        dt.Rows[rowIndex]["NextDueDate"] = txtNextDueDate.Text;
                        dt.Rows[rowIndex]["Amount"] = txtAmount.Text;
                        dt.Rows[rowIndex]["NoofTickets"] = txtNoofTickets.Text;
                        dt.AcceptChanges();                                                
                        ViewState["CurrentTable"] = dt;
                        gvTicketsInfo.EditIndex = -1;
                        gvTicketsInfo.DataSource = dt;
                        gvTicketsInfo.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        int total=0;
        protected void gvTicketsInfo_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.Footer)
                {
                    // DateTime CurrentDate;                  
                    TextBox txtDueDate1 = e.Row.FindControl("txtDueDate1") as TextBox;
                    TextBox txtNextDueDate1 = e.Row.FindControl("txtNextDueDate1") as TextBox;
                    if (txtDueDate1 != null)
                    {
                        //  txtDueDate1.Text = CurrentDate.ToString("dd MMM yyyy");
                       DataSet ds = AttendanceDAC.GetDueDateBy_EMPID(Convert.ToInt32(lblEmpidTwo.Text));
                        txtDueDate1.Text = ds.Tables[0].Rows[0]["LVRD"].ToString();
                        if(txtNextDueDate1!=null)
                        {
                            txtNextDueDate1.Text = ds.Tables[0].Rows[0]["NEXTDUEDATE"].ToString();
                        }
                    }
                    TextBox txtEmpName = e.Row.FindControl("txtEmpName1") as TextBox;
                    if (txtEmpName!=null)
                    {
                        if (gvTicketsInfo.Rows.Count == 1)  
                        {
                            txtEmpName.Text = lblEmpName.Text;
                        }
                        else
                            txtEmpName.Text = "";
                    }
                    //ddlRelation
                    DropDownList ddlRelation1 = e.Row.FindControl("ddlRelation1") as DropDownList;
                    if (ddlRelation1 != null)
                    {
                        FIllObject.FillDropDown(ref  ddlRelation1, "get_T_HMS_Relation");
                        if (ddlRelation1.Items.Count > 0)
                            if (ddlRelation1.Items.FindByText("Self") != null)
                                    ddlRelation1.Items.FindByText("Self").Selected = true;
                    }
                    //ddlPassengerType
                    DropDownList ddlPassengerType1 = e.Row.FindControl("ddlPassengerType1") as DropDownList;
                    if (ddlPassengerType1 != null)
                    {
                        FIllObject.FillDropDown(ref  ddlPassengerType1, "get_T_HMS_PassengerType");
                        if (ddlPassengerType1.Items.Count > 0)
                            if (ddlPassengerType1.Items.FindByText("Adult") != null)
                                    ddlPassengerType1.Items.FindByText("Adult").Selected = true;
                    }
                    //ddlBookingClass
                    DropDownList ddlBookingClass1 = e.Row.FindControl("ddlBookingClass1") as DropDownList;
                    if (ddlBookingClass1 != null)
                    {
                        FIllObject.FillDropDown(ref  ddlBookingClass1, "get_T_HMS_BookingClass");
                        if (ddlBookingClass1.Items.Count > 0)
                            if (ddlBookingClass1.Items.FindByText("Economy Class") != null)
                                ddlBookingClass1.Items.FindByText("Economy Class").Selected = true;
                        if (ddlBookingClass1.Items.FindByText("Economy") != null)
                            ddlBookingClass1.Items.FindByText("Economy").Selected = true;                        
                    }
                    //ddlAirLines
                    DropDownList ddlAirLines1 = e.Row.FindControl("ddlAirLines1") as DropDownList;
                    if (ddlAirLines1 != null)
                    {
                        bindAirlnies(ddlAirLines1, Convert.ToInt32(lblToCityID.Text));
                          if (ddlAirLines1.Items.Count>1)
                          {
                              ddlAirLines1.Items.FindByValue("1");
                              ddlAirLines1.SelectedIndex = 1;
                              GetFare(ddlAirLines1, e);
                          }
                    }
                }
                else if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    Label lblDed = ((Label)e.Row.FindControl("lblDETID"));
                    if (lblDed.Text == "")
                    {
                        LinkButton lnkEdit = (LinkButton)e.Row.FindControl("lnkEdit");
                        if (lnkEdit != null)
                        {
                            lnkEdit.Visible = false;
                        }
                        LinkButton lnkDelete = (LinkButton)e.Row.FindControl("lnkDelete");
                        if (lnkDelete != null)
                        {
                            lnkDelete.Visible = false;
                        }
                        Button btnJournal = (Button)e.Row.FindControl("btnJournal");
                        if (btnJournal!=null)
                        {
                            btnJournal.Visible = false;
                        }
                        lblDed.Text = "-1";
                        ((Label)e.Row.FindControl("lblRecStatus")).Text = "NewData";
                    }
                    DropDownList ddlRelation = e.Row.FindControl("ddlRelation") as DropDownList;
                    if (ddlRelation!=null)
                        FIllObject.FillDropDown(ref  ddlRelation, "get_T_HMS_Relation");
                    if (ddlRelation != null)
                    {
                        if (e.Row.Cells[4].Text != "&nbsp;")
                        {
                            ddlRelation.SelectedValue = e.Row.Cells[4].Text;
                        }
                    }
                    //GetWorkStatusListIntoViewState(cmdITRFilingStatus, e, indCITRFilingStatusID);
                    //ddlPassengerType
                    DropDownList ddlPassengerType = e.Row.FindControl("ddlPassengerType") as DropDownList;
                    if (ddlPassengerType != null)
                        FIllObject.FillDropDown(ref  ddlPassengerType, "get_T_HMS_PassengerType");
                    if (ddlPassengerType != null)
                    {
                        if (e.Row.Cells[6].Text != "&nbsp;")
                        {
                            ddlPassengerType.SelectedValue = e.Row.Cells[6].Text;
                        }
                    }
                    //ddlBookingClass
                    DropDownList ddlBookingClass = e.Row.FindControl("ddlBookingClass") as DropDownList;
                    if (ddlBookingClass != null)
                    FIllObject.FillDropDown(ref  ddlBookingClass, "get_T_HMS_BookingClass");
                    if (ddlBookingClass != null)
                    {
                        if (e.Row.Cells[8].Text != "&nbsp;")
                        {
                            ddlBookingClass.SelectedValue = e.Row.Cells[8].Text;
                        }
                    }
                    //ddlAirLines
                    DropDownList ddlAirLines = e.Row.FindControl("ddlAirLines") as DropDownList;
                    if (ddlAirLines!=null)
                        bindAirlnies(ddlAirLines, Convert.ToInt32(lblToCityID.Text));
                    if (ddlAirLines != null)
                    {
                        if (e.Row.Cells[10].Text != "&nbsp;")
                        {
                            ddlAirLines.SelectedValue = e.Row.Cells[10].Text;
                        }
                    }
                    
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        void bindAirlnies(DropDownList ddl,int Cityid)
        {   
            DataSet ds = AttendanceDAC.GetAirline_ByCityWise(Cityid);
            if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0)
            {
                ddl.DataSource = ds;
                ddl.DataTextField = "name";
                ddl.DataValueField = "id";
                ddl.DataBind();
                ddl.Items.Insert(0, new ListItem("---Select---", "0"));
            }
            else
            {
                ddl.Items.Insert(0, new ListItem("---Select---", "0"));
            }
        }
        protected void gvTicketsInfo_RowEditing(object sender, GridViewEditEventArgs e)
        {
            try
            {
                DataTable dt = (DataTable)ViewState["CurrentTable"];
                if (dt.Rows.Count > 0)
                {
                    gvTicketsInfo.EditIndex = e.NewEditIndex;
                    gvTicketsInfo.DataSource = dt;
                    gvTicketsInfo.DataBind();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void gvTicketsInfo_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                if (ViewState["CurrentTable"] != null)
                {
                    DataTable dt = (DataTable)ViewState["CurrentTable"];                    
                    if (dt.Rows.Count > 1)
                    {
                        dt.Rows.Remove(dt.Rows[e.RowIndex]);
                        ViewState["CurrentTable"] = dt;
                        gvTicketsInfo.DataSource = dt;
                        gvTicketsInfo.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void gvTicketsInfo_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            try
            {
                gvTicketsInfo.EditIndex = -1;
                if (ViewState["CurrentTable"] != null)
                {
                    DataTable dt = (DataTable)ViewState["CurrentTable"];
                    if (dt.Rows.Count > 0)
                    {
                        gvTicketsInfo.DataSource = dt;
                        gvTicketsInfo.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                //lblMessage.Text = "gvCorporateITR_RowCancelingEdit: " + ex.Message;
            }
        }
        protected void GetFare(object sender, EventArgs e)
        {
            DropDownList ddlAirlines = (DropDownList)sender;
            GridViewRow row = (GridViewRow)ddlAirlines.Parent.Parent;
            int idx = row.RowIndex;
            DropDownList ddlBookingClass = row.FindControl("ddlBookingClass1") as DropDownList;
            DropDownList ddlPassengerType = row.FindControl("ddlPassengerType1") as DropDownList;
            TextBox txtAmount1 = row.FindControl("txtAmount1") as TextBox;
            DataSet ds=null;
            if (Convert.ToInt32(ViewState["CateId"].ToString())>0)
                ds = AttendanceDAC.HMS_EmpVsAirTicketsAuth_Details(Convert.ToInt32(ViewState["CateId"].ToString()));
            int FromcityID = 0;
            int ToCityID = 0;
            if (ds.Tables[0].Rows.Count > 0)
            {
                FromcityID = Convert.ToInt32(ds.Tables[0].Rows[0]["from_cityID"].ToString());
                ToCityID = Convert.ToInt32(ds.Tables[0].Rows[0]["TO_cityID"].ToString());
            }
            DataSet ds1 = new DataSet();
            if (ddlBookingClass != null && ddlAirlines != null && ddlPassengerType != null)
                 ds1=AttendanceDAC.HMS_GetTicket_Fare(Convert.ToInt32(ddlAirlines.SelectedValue.ToString()),
                    Convert.ToInt32(ddlPassengerType.SelectedValue.ToString()), Convert.ToInt32(ddlBookingClass.SelectedValue.ToString()),ToCityID);            
            if (ds1.Tables[0].Rows.Count > 0)
                txtAmount1.Text = Convert.ToString(ds1.Tables[0].Rows[0]["Fare_rate"].ToString());
            else
                txtAmount1.Text = "0.00";                
        }
        protected void GetFare1(object sender, EventArgs e)
        {
            DropDownList ddlAirlines = (DropDownList)sender;
            GridViewRow row = (GridViewRow)ddlAirlines.Parent.Parent;
            int idx = row.RowIndex;
            DropDownList ddlBookingClass = row.FindControl("ddlBookingClass") as DropDownList;
            DropDownList ddlPassengerType = row.FindControl("ddlPassengerType") as DropDownList;
            TextBox txtAmount1 = row.FindControl("txtAmount") as TextBox;
            DataSet ds=null;
            if (Convert.ToInt32(ViewState["CateId"].ToString()) > 0)
                ds = AttendanceDAC.HMS_EmpVsAirTicketsAuth_Details(Convert.ToInt32(ViewState["CateId"].ToString()));
            int FromcityID = 0;
            int ToCityID = 0;
            if (ds.Tables[0].Rows.Count > 0)
            {
                FromcityID = Convert.ToInt32(ds.Tables[0].Rows[0]["from_cityID"].ToString());
                ToCityID = Convert.ToInt32(ds.Tables[0].Rows[0]["TO_cityID"].ToString());
            }
            DataSet ds1 = new DataSet();
            if (ddlBookingClass != null && ddlAirlines != null && ddlPassengerType != null)            
                ds1 = AttendanceDAC.HMS_GetTicket_Fare(Convert.ToInt32(ddlAirlines.SelectedValue.ToString()),
                   Convert.ToInt32(ddlPassengerType.SelectedValue.ToString()), Convert.ToInt32(ddlBookingClass.SelectedValue.ToString()), ToCityID);
            if (ds1.Tables[0].Rows.Count > 0)
                txtAmount1.Text = Convert.ToString(ds1.Tables[0].Rows[0]["Fare_rate"].ToString());
            else
                txtAmount1.Text = "0.00";
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                AttendanceDAC objAtt = new AttendanceDAC();
                string strDueDate = "", strRecStatus = "", DETID = "";
                string strNextDueDate = "";
                int n = 0;
                string strAmt = "0.00"; string strtxtNoofTickets = "0";
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("<?xml version=\"1.0\"?>");
                sb.AppendLine("   <NonCorporateITR>");
               
                foreach (GridViewRow row in gvTicketsInfo.Rows)
                {
                    DETID = ((Label)row.FindControl("lblDETID")).Text;
                    //strEmpName = ((Label)row.FindControl("lblEmpName")).Text;
                    TextBox txtEmpName = (TextBox)gvTicketsInfo.FooterRow.FindControl("txtEmpName1");
                    strtxtNoofTickets = ((Label)row.FindControl("lblNoofTickets")).Text.Trim().ToString();
                    TextBox txtNoofTickets = (TextBox)gvTicketsInfo.FooterRow.FindControl("txtNoofTickets1");
                    string strNoT = txtNoofTickets.Text.Trim().ToString();

                    if (DETID.ToString() != "")
                    {
                        if ((strtxtNoofTickets != "" && strtxtNoofTickets != "0.00" && strtxtNoofTickets != "0") || (strNoT != "" && strNoT!= "0.00" && strNoT != "0"))
                        { 
                        sb.AppendLine(" <DETAILS>");
                        strRecStatus = ((Label)row.FindControl("lblRecStatus")).Text;
                        sb.AppendLine("         <RecStatus>" + strRecStatus + "</RecStatus>");
                        sb.AppendLine("         <DETID>" + int.Parse(DETID) + "</DETID>");
                        //sb.AppendLine("         <ID>" + Convert.ToInt32(ViewState["CateId"].ToString()) + "</ID>");
                        //sb.AppendLine("         <empid>" + lblEmpidTwo.Text + "</empid>"); // nned to fetch for insert
                        //ddlRelation
                        DropDownList ddlRelation = gvTicketsInfo.FooterRow.FindControl("ddlRelation1") as DropDownList;
                        //ddlPassengerType
                        DropDownList ddlPassengerType = gvTicketsInfo.FooterRow.FindControl("ddlPassengerType1") as DropDownList;
                        //ddlBookingClass
                        DropDownList ddlBookingClass = gvTicketsInfo.FooterRow.FindControl("ddlBookingClass1") as DropDownList;
                        //ddlAirLines
                        DropDownList ddlAirLines = gvTicketsInfo.FooterRow.FindControl("ddlAirLines1") as DropDownList;

                        TextBox txtAmount = (TextBox)gvTicketsInfo.FooterRow.FindControl("txtAmount1");

                        sb.AppendLine("         <Name>" + txtEmpName.Text.ToString() + "</Name>");

                        if(row.Cells[4].Text.Equals("&nbsp;"))
                            sb.AppendLine("         <detrelationID>" + int.Parse(ddlRelation.SelectedValue) + "</detrelationID>");
                        else
                            sb.AppendLine("         <detrelationID>" + int.Parse(row.Cells[4].Text) + "</detrelationID>");
                            
                        if (row.Cells[6].Text.Equals("&nbsp;"))
                            sb.AppendLine("         <detPassengerTypeID>" + int.Parse(ddlPassengerType.SelectedValue) + "</detPassengerTypeID>");
                        else
                            sb.AppendLine("         <detPassengerTypeID>" + int.Parse(row.Cells[6].Text) + "</detPassengerTypeID>");

                        if (row.Cells[6].Text.Equals("&nbsp;"))
                            sb.AppendLine("         <detbookingClassID>" + int.Parse(ddlBookingClass.SelectedValue) + "</detbookingClassID>");
                        else
                            sb.AppendLine("         <detbookingClassID>" + int.Parse(row.Cells[8].Text) + "</detbookingClassID>");

                        if (row.Cells[6].Text.Equals("&nbsp;"))
                            sb.AppendLine("         <detAirLinesID>" + int.Parse(ddlAirLines.SelectedValue) + "</detAirLinesID>");
                        else
                            sb.AppendLine("         <detAirLinesID>" + int.Parse(row.Cells[10].Text) + "</detAirLinesID>");

                        //TicketsAmount
                        strDueDate = ((Label)row.FindControl("lblDueDate")).Text;
                        DateTime DueDate = CodeUtilHMS.ConvertToDate_ddMMMyyy(strDueDate, CodeUtilHMS.DateFormat.ddMMMyyyy);
                        if (strDueDate != "")
                        {
                            sb.AppendLine("         <DueDate>" + DueDate.ToString("MM/dd/yyyy") + "</DueDate>");
                        }
                        else
                        {
                            sb.AppendLine("         <DueDate>" + System.DBNull.Value + "</DueDate>");
                        }
                        strNextDueDate = ((Label)row.FindControl("lblNextDueDate")).Text;
                        DateTime NextDueDate = CodeUtilHMS.ConvertToDate_ddMMMyyy(strNextDueDate, CodeUtilHMS.DateFormat.ddMMMyyyy);
                        if (strDueDate != "")
                        {
                            sb.AppendLine("         <NextDueDate>" + NextDueDate.ToString("MM/dd/yyyy") + "</NextDueDate>");
                        }
                        else
                        {
                            sb.AppendLine("         <NextDueDate>" + System.DBNull.Value + "</NextDueDate>");
                        }
                        //lblAmount
                        strAmt = ((Label)row.FindControl("lblAmount")).Text;
                       
                        if(strAmt.Equals("") && txtAmount.Text != "")
                            sb.AppendLine("         <TicketsAmount>" + decimal.Parse(txtAmount.Text) + "</TicketsAmount>");
                        else if(strAmt != "")
                            sb.AppendLine("         <TicketsAmount>" + decimal.Parse(strAmt) + "</TicketsAmount>");
                        else
                            sb.AppendLine("         <TicketsAmount>0.00</TicketsAmount>");

                        //txtNoofTickets
                        if (strtxtNoofTickets.Equals("") && txtNoofTickets.Text != "")
                            sb.AppendLine("         <NoofTickets>"+ decimal.Parse(txtNoofTickets.Text) + "</NoofTickets>");
                        else if (strtxtNoofTickets != "")
                            sb.AppendLine("         <NoofTickets>" + decimal.Parse(strtxtNoofTickets) + "</NoofTickets>");
                        else
                            sb.AppendLine("         <NoofTickets>0.00</NoofTickets>");

                        sb.AppendLine(" </DETAILS>");
                        }
                    }
                }
                    sb.AppendLine("   </NonCorporateITR>");                    
                    n = AttendanceDAC.T_HMS_Insert_EmpVsAirTicketsUseage_Details(Convert.ToInt32(lblEmpidTwo.Text),
                        Convert.ToInt32(ViewState["CateId"].ToString()), Convert.ToInt32( Convert.ToInt32(Session["UserId"]).ToString()), sb.ToString());
                    if (Convert.ToInt32(n) > 0)
                    {
                        AlertMsg.MsgBox(Page, "Successfully Saved");
                        BindTicketDetails(Convert.ToInt32(lblEmpidTwo.Text));
                        EmployeBind(objHrCommon);
                    }
                    else
                    {
                        AlertMsg.MsgBox(Page, "Error While Saveing");
                    }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //for worksite
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionList(string prefixText, int count, string contextKey)
        {
            //DataSet ds = AttendanceDAC.GetGoogleABCSearchWorkSite(prefixText, SearchCompanyID);
            //return ConvertStingArray(ds);// txtItems.ToArray();
           // WSID = 0;
            DataSet ds = AttendanceDAC.HR_GetWorkSite_By_empVsAirTicketsAuth(prefixText);
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
        protected void txtSearchWorksite_TextChanged(object sender, EventArgs e)
        {
            WSiteid = Convert.ToInt32(ddlWorksite_hid.Value == "" ? "0" : ddlWorksite_hid.Value);
            Siteid = Convert.ToInt32(ddlWorksite_hid.Value == "" ? "0" : ddlWorksite_hid.Value);
        }
        //for department
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionDepartmentList(string prefixText, int count, string contextKey)
        {
            //DataSet ds = AttendanceDAC.GetGoogleABCSearchWorkSite(prefixText, SearchCompanyID);
            //return ConvertStingArray(ds);// txtItems.ToArray();
            // WSID = 0;
            DataSet ds = AttendanceDAC.GetSearchDesiginationFilterActive(prefixText, SearchCompanyID, Siteid);
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
        //for employee
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionemployeeList(string prefixText, int count, string contextKey)
        {
            //DataSet ds = AttendanceDAC.GetGoogleABCSearchWorkSite(prefixText, SearchCompanyID);
            //return ConvertStingArray(ds);// txtItems.ToArray();
            // WSID = 0;
            DataSet ds = AttendanceDAC.HR_GoogleSearchEmpBySiteDept(prefixText, WSiteid, EDeptid, "Y", SearchCompanyID);
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
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            rowEmpDetails.Visible = false;
            gvTicketsInfo.DataSource = null;
            gvTicketsInfo.DataBind();
            EmpListPaging.CurrentPage = 1;
            BindPager();
        }
       // added by pratap date: 11-jan-2017
        protected void DisplayUsageDetails()
        {
            try
            {
                if (gvRMItem.Rows.Count > 0)
                {
                    //GridViewRow oItem = (GridViewRow)((LinkButton)gvRMItem.CommandSource).NamingContainer;
                    int row = 0;
                    Label lblEmpAirID = (Label)gvRMItem.Rows[row].FindControl("lblEmpAirID");
                    ViewState["CateId"] = lblEmpAirID.Text;
                    Label lblName = (Label)gvRMItem.Rows[row].FindControl("Label3");
                    Label lblProfilerType = (Label)gvRMItem.Rows[row].FindControl("lblProfilerType");
                    Label Label1 = (Label)gvRMItem.Rows[row].FindControl("Label1");
                    Label Label7 = (Label)gvRMItem.Rows[row].FindControl("Label7");
                    Label lblEmpidNew = (Label)gvRMItem.Rows[row].FindControl("lblEmpidNew");
                    Label lblTO_cityID = (Label)gvRMItem.Rows[row].FindControl("lblTO_cityID");
                    Label Label115 = (Label)gvRMItem.Rows[row].FindControl("Label115");
                    int Sync = AttendanceDAC.GetSalary_Syncd(Convert.ToInt32(lblEmpidNew.Text));
                    if (Sync == 0)
                    {
                        AlertMsg.MsgBox(Page, "Check your latest Salaries are Synced , before you run this process");
                        return;
                    }
                    lblToCityID.Text = lblTO_cityID.Text;
                    lblMinimumWorkingDays.Text = "";
                    lblEmpidTwo.Text = "";
                    lblEmpName.Text = "";
                    lblCity.Text = "";
                    lblTickets.Text = "";
                    lblEmpName.Text = lblName.Text;
                    lblEmpName.Text = lblEmpName.Text;
                    lblCity.Text = "From city: " + lblProfilerType.Text + "-----To city: " + Label1.Text;
                    //lblTickets.Text = Label7.Text;
                    lblTickets.Text = Label115.Text;
                    lblEmpidTwo.Text = lblEmpidNew.Text;
                    DateTime CurrentDate = DateTime.Now;
                    lblMinimumWorkingDays.Text = AttendanceDAC.GetMinimum_WorkingDaysByEmpid(Convert.ToInt32(lblEmpidTwo.Text)).ToString();
                    lblDaysOfWork.Text = AttendanceDAC.Get_Total_Working_Days_Emp(Convert.ToInt32(lblEmpidTwo.Text), CurrentDate.ToString("MM/dd/yyyy")).ToString();
                    BindTicketDetails(Convert.ToInt32(lblEmpidTwo.Text));
                    rowEmpDetails.Visible = true;
                }
            }
            catch (Exception)
            {
            }
        }
        protected void gvTicketsInfo_RowCommand(object sender, GridViewCommandEventArgs e)
        {
        }
        protected void btnJournal_Click(object sender, EventArgs e)
        {
            try
            {
                GridViewRow gvr = ((Button)sender).Parent.Parent as GridViewRow;
                Label lblDETID = (Label)gvr.FindControl("lblDETID");
                if(lblDETID.Text=="-1")
                {
                    AlertMsg.MsgBox(Page, "AirTicket Information is not saved!!");
                    return;
                }
                Button btnJournal = (Button)gvr.FindControl("btnJournal");
                if(btnJournal!=null)
                {
                    if(btnJournal.Text!="Process")
                    {
                        AlertMsg.MsgBox(Page, "Already Journal Posted!!");
                        return;
                    }
                }
                int EmpID = Convert.ToInt32(lblEmpidTwo.Text);
                Label lblEmpName = (Label)gvr.FindControl("lblEmpName");
                Label lblAmount = (Label)gvr.FindControl("lblAmount");
                Label lblAirLines = (Label)gvr.FindControl("lblAirLines");
                Label lblBookingClass = (Label)gvr.FindControl("lblBookingClass");
                Label lblPassengerType = (Label)gvr.FindControl("lblPassengerType");
                Label lblRelation = (Label)gvr.FindControl("lblRelation");
                Label lblNoofTickets = (Label)gvr.FindControl("lblNoofTickets");
                DateTime? TransTime = null;
                // Label lblAdvanceValue = (Label)gvr.FindControl("lblAdvanceValue");
                string Remarks = "AirTicket Info Name: " + lblEmpName.Text + "; Relation : " + lblRelation.Text + "; PassengerType: " + lblPassengerType.Text
                    + " lblBookingClass: " + lblBookingClass.Text + " lblAirLines: " + lblAirLines.Text + " lblNoofTickets: " + lblNoofTickets.Text;
                SqlParameter[] objParam = new SqlParameter[8];
                // objParam[0] = new SqlParameter("@TransID", 0);
                objParam[0] = new SqlParameter("@TransTime", TransTime);
                objParam[1] = new SqlParameter("@Remarks", Remarks);
                objParam[2] = new SqlParameter("@CompanyID", CompanyID);
                objParam[3] = new SqlParameter("@UserId", Convert.ToInt32( Convert.ToInt32(Session["UserId"]).ToString()));
                objParam[4] = new SqlParameter("@EmpID", EmpID);
                objParam[5] = new SqlParameter("@Amount", (Convert.ToDouble(lblAmount.Text) * (Convert.ToDouble(lblNoofTickets.Text))));
                objParam[6] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
                objParam[6].Direction = ParameterDirection.ReturnValue;
                objParam[7] = new SqlParameter("@AirTicketDetID", Convert.ToInt32(lblDETID.Text));
                int res = SQLDBUtil.ExecuteNonQuery("sh_InsertAirticketJournal", objParam);
                res = Convert.ToInt32(objParam[6].Value);
                btnSave_Click(sender, e);
                string str = "Transaction Done: "  ;
                AlertMsg.MsgBox(Page, str);
                BindTicketDetails(Convert.ToInt32(lblEmpidTwo.Text));
                EmployeBind(objHrCommon);
            }
            catch (Exception ex)
            {
                //throw ex;
                AlertMsg.MsgBox(Page, ex.Message.ToString(),AlertMsg.MessageType.Error);
            }
        }
        public string GetJVText(string JV)
        {
            if (JV == "0")
                return "Process";
            else
                return JV;
        }
        public bool GetURL(string AirlinesCount)
        {
            if (Convert.ToInt32(AirlinesCount.ToString())>0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}