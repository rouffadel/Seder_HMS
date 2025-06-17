using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using AECLOGIC.HMS.BLL;
using AECLOGIC.ERP.COMMON;
namespace AECLOGIC.ERP.HMS
{
    public partial class ConfigAirTicket : AECLOGIC.ERP.COMMON.WebFormMaster
    {
        //   
        int mid = 0;
        bool viewall, Editable;
        string menuname;
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
                if (rblDesg.SelectedValue == "1")
                {
                    Status = true;
                }
                //   
                DataSet ds = AttendanceDAC.T_HMS_ConfigAirTicketdetailsbyID_status(objHrCommon, Status, 0);
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

            }
            catch (Exception e)
            {
                throw e;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            lblStatus.Text = String.Empty;
            if (!IsPostBack)
            {
                GetParentMenuId();
                Ajax.Utility.RegisterTypeForAjax(typeof(AjaxDAL));
                BindLocations(ddlfromCity);
                BindLocations(ddlToCity);
               // FIllObject.FillDropDown(ref  ddlrelation, "get_T_HMS_Relation");
                FIllObject.FillDropDown(ref  ddlpassengerType, "get_T_HMS_PassengerType");
                FIllObject.FillDropDown(ref  ddlBookingClass, "get_T_HMS_BookingClass");
                bindAirlnies();
            
                ViewState["CateId"] = "";
                if ((Convert.ToInt32(Request.QueryString["key"]) == 1) &&(Request.QueryString.Count>1))  // modify by pratap for getting error  page not opening date: 06-FEB-2017
                {
                    this.Title = "Air Ticket Configuration";
                    tblNewk.Visible = true;
                    tblEdit.Visible = false;
                    gvRMItem.Visible = false;

                    //string url = "ConfigAirTicket.aspx?key=1&FromCityID=" + FromCityID + "&ToCityID=" + ToCityID + " &BookingID= " + BookingClassID + " &passegerid= " + passegerid;
                    if (ddlfromCity.Items.FindByValue( Request.QueryString["FromCityID"].ToString())!= null)
                    {
                        ddlfromCity.SelectedValue = Request.QueryString["FromCityID"].ToString();
                    }
                    if (ddlToCity.Items.FindByValue(Request.QueryString["ToCityID"].ToString()) != null)
                    {
                        ddlToCity.SelectedValue = Request.QueryString["ToCityID"].ToString();
                    }
                    if (ddlpassengerType.Items.Count > 0)
                        if (ddlpassengerType.Items.FindByText("Adult") != null)
                            ddlpassengerType.Items.FindByText("Adult").Selected = true;


                    if (ddlBookingClass.Items.Count > 0)
                        if (ddlBookingClass.Items.FindByText("Economy Class") != null)
                            ddlBookingClass.Items.FindByText("Economy Class").Selected = true;       
                    //if (ddlBookingClass.Items.FindByValue(Request.QueryString["BookingID"].ToString()) != null)
                    //{
                    //    ddlBookingClass.SelectedValue = Request.QueryString["BookingID"].ToString();
                    //}
                    //if (ddlpassengerType.Items.FindByValue(Request.QueryString["passegerid"].ToString()) != null)
                    //{
                    //    ddlpassengerType.SelectedValue = Request.QueryString["passegerid"].ToString();
                    //}
                } 
                else if ((Convert.ToInt32(Request.QueryString["key"]) == 1))
                {
                    this.Title = "Air Ticket Configuration";
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

            }
        }

        #region CodeBYkAUSHAL

        void bindAirlnies()
        {
            objHrCommon.PageSize = 100;
            objHrCommon.CurrentPage = 1;
            DataSet ds = AttendanceDAC.HR_GetAirlineByStatus(objHrCommon, true);
            if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0)
            {
                ddlAirlines.DataSource = ds;
                ddlAirlines.DataTextField = "name";
                ddlAirlines.DataValueField = "id";
                ddlAirlines.DataBind();
                ddlAirlines.Items.Insert(0, new ListItem("---Select---", "0"));
            }
        }
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
        #endregion
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
                gvRMItem.Columns[1].Visible = Convert.ToBoolean(ds.Tables[0].Rows[0]["Editable"].ToString());
                ViewState["ViewAll"] = (bool)ds.Tables[0].Rows[0]["ViewAll"];
                viewall = (bool)ViewState["ViewAll"];
                btnSubmit.Enabled = Editable = Convert.ToBoolean(ds.Tables[0].Rows[0]["Editable"].ToString());
                menuname = ds.Tables[0].Rows[0]["menuname"].ToString();
                menuid = MenuId.ToString();
                mid = Convert.ToInt32(ds.Tables[0].Rows[0]["MenuId"].ToString());
            }
            return MenuId;
        }
       
        public void BindDetails(int ID)
        {
            objHrCommon.PageSize = 10;
            objHrCommon.CurrentPage = 1;
            DataSet ds = AttendanceDAC.T_HMS_ConfigAirTicketdetailsbyID_status(objHrCommon, true, ID);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                
                tblEdit.Visible = false;
                tblNewk.Visible = true;
                try { ddlfromCity.SelectedValue = ds.Tables[0].Rows[0][2].ToString(); }
                catch { ddlfromCity.SelectedIndex = 0; };
                try { ddlToCity.SelectedValue = ds.Tables[0].Rows[0][3].ToString(); }
                catch { ddlToCity.SelectedIndex = 0; };
                try
                {
                    ddlAirlines.SelectedValue = ds.Tables[0].Rows[0][4].ToString();
                }
                catch { ddlAirlines.SelectedIndex = 0; }
                ddlpassengerType.SelectedValue = ds.Tables[0].Rows[0][5].ToString();
                ddlBookingClass.SelectedValue = ds.Tables[0].Rows[0][6].ToString();
                txtrate.Text = ds.Tables[0].Rows[0][7].ToString();
                btnSubmit.Text = "Update";
            
            }
        }

        protected void btn_reste_Click(object sender,EventArgs e)
        {
            ddlfromCity.SelectedIndex =0;
            ddlToCity.SelectedIndex = 0;
            ddlAirlines.SelectedIndex = 0;
            ddlpassengerType.SelectedIndex = 0;
            ddlBookingClass.SelectedIndex = 0;
            txtrate.Text = "";
            btnSubmit.Text = "Submit";
            ViewState["CateId"] = "0";
        }
        protected void gvWages_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int ID = Convert.ToInt32(e.CommandArgument);
            ViewState["CateId"] = ID;
            bool Status = true;
            if (rblDesg.SelectedValue == "1")
            {
                Status = false;
            }
            if (e.CommandName == "Edt")
            {
               
                BindDetails(ID);
            }
            else
            {
                try
                {
                    AttendanceDAC.HMS_ActiveInActiveItems(ID, Status, "HR_Upd_ConfigAirTicketStatus");
                    EmployeBind(objHrCommon);

                }
                catch (Exception DelDesig)
                {
                    AlertMsg.MsgBox(Page, DelDesig.Message.ToString(),AlertMsg.MessageType.Error);
                }
            }
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
                int Output = AttendanceDAC.T_HMS_CRUD_ConfigAirTicket(CateId, Convert.ToInt32(ddlfromCity.SelectedValue), Convert.ToInt32(ddlToCity.SelectedValue), Convert.ToInt32(ddlAirlines.SelectedValue), Convert.ToInt32(ddlpassengerType.SelectedValue), Convert.ToInt32(ddlBookingClass.SelectedValue), Convert.ToDecimal(txtrate.Text.Trim()),  Convert.ToInt32(Session["UserId"]), 1);
               
                if (Output == 1)
                {
                   // AlertMsg.MsgBox(Page, "Done.!!");

                    lblStatus.Text = "Done.!!!";
                    lblStatus.ForeColor = System.Drawing.Color.Green;
                }
                else if (Output == 2)
                {
                    //AlertMsg.MsgBox(Page, "Already Exists.!");
                    lblStatus.Text = "Already Exists.!!";
                    lblStatus.ForeColor = System.Drawing.Color.Red;

                }
                else
                {
                  //  AlertMsg.MsgBox(Page, "Updated.!");
                    lblStatus.Text = "Updated.!!!";
                    lblStatus.ForeColor = System.Drawing.Color.Green;
                }
                EmployeBind(objHrCommon);
                tblNewk.Visible = false;
                tblEdit.Visible = true;
                gvRMItem.Visible = true;
                Clear();


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
        

        protected void btnsearch_Click(object sender, EventArgs e)
        {

            try
            {
                EmpListPaging.CurrentPage = 1;
                objHrCommon.PageSize = EmpListPaging.ShowRows;
                objHrCommon.CurrentPage = EmpListPaging.CurrentPage;
                bool Status = false;
                if (rblDesg.SelectedValue == "1")
                {
                    Status = true;
                }

                DataSet ds = AttendanceDAC.T_HMS_ConfigAirTicketdetailsbyID_status_Search(objHrCommon, Status, 0, Convert.ToInt32(AirLinesId_hd.Value == "" ? "0" : AirLinesId_hd.Value), Convert.ToInt32(PassengrType_hd.Value == "" ? "0" : PassengrType_hd.Value), Convert.ToInt32(BookngCls_hd.Value == "" ? "0" : BookngCls_hd.Value), Convert.ToInt32(FrmCity_hd.Value == "" ? "0" : FrmCity_hd.Value), Convert.ToInt32(Tocity_hd.Value == "" ? "0" : Tocity_hd.Value));



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
                if(txtAirlines.Text=="" ||txtAirlines.Text==null)
                    AirLinesId_hd.Value = string.Empty;
                if( txtPaasngrType.Text==""|| txtPaasngrType.Text==null)
                    PassengrType_hd.Value = string.Empty;
                if(txtBookngCls.Text==""||txtBookngCls.Text==null)
                    BookngCls_hd.Value = string.Empty;
                if(txtfrmcity.Text==""||txtfrmcity.Text==null)
                      FrmCity_hd.Value = string.Empty;
                 if(txttocity.Text==""||txttocity.Text==null)
                       Tocity_hd.Value = string.Empty;
                   
                
              
            

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        #region WebMethods

        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionList(string prefixText, int count, string contextKey)
        {
            DataSet ds = AttendanceDAC.GetAirLiens_Search(prefixText);
            DataTable dt = ds.Tables[0];
            List<string> items = new List<string>(count);
            var rtval = new Dictionary<string, string>();
            foreach (DataRow row in dt.Rows)
            {
                string str = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(row["Name"].ToString(), row["ID"].ToString());
                items.Add(str);
            }

            return items.ToArray();// txtItems.ToArray()
        }


        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetPassengerType(string prefixText, int count, string contextKey)
        {
            DataSet ds = AttendanceDAC.GetPassengerType_Search(prefixText);
            DataTable dt = ds.Tables[0];
            List<string> items = new List<string>(count);
            var rtval = new Dictionary<string, string>();
            foreach (DataRow row in dt.Rows)
            {
                string str = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(row["Name"].ToString(), row["ID"].ToString());
                items.Add(str);
            }

            return items.ToArray(); // txtItems.ToArray()
        }

        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetBookingClass_Search(string prefixText, int count, string contextKey)
        {
            DataSet ds = AttendanceDAC.GetBookingClass_Search(prefixText);
            DataTable dt = ds.Tables[0];
            List<string> items = new List<string>(count);
            var rtval = new Dictionary<string, string>();
            foreach (DataRow row in dt.Rows)
            {
                string str = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(row["Name"].ToString(), row["ID"].ToString());
                items.Add(str);
            }

            return items.ToArray(); // txtItems.ToArray()
        }

        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] Get_City_Search(string prefixText, int count, string contextKey)
        {
            DataSet ds = AttendanceDAC.Get_City_Search(prefixText);
            DataTable dt = ds.Tables[0];
            List<string> items = new List<string>(count);
            var rtval = new Dictionary<string, string>();
            foreach (DataRow row in dt.Rows)
            {
                string str = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(row["Name"].ToString(), row["ID"].ToString());
                items.Add(str);
            }

            return items.ToArray(); // txtItems.ToArray()
        }
        #endregion

        protected void btnnewcity_Click(object sender, EventArgs e)
        {
            pnlAddCity.Visible = true;

            DataSet ds = AttendanceDAC.GetCountry();
            ddlCityCountry.DataSource = ds;
            ddlCityCountry.DataTextField = ds.Tables[0].Columns["CountryName"].ToString();
            ddlCityCountry.DataValueField = ds.Tables[0].Columns["CountryId"].ToString();
            ddlCityCountry.DataBind();
            ddlCityCountry.Items.Insert(0, new ListItem("---Select---", "0"));
           // ddlCityCountry.SelectedValue = ddlComuCoun.SelectedValue;


            DataSet dsState = AttendanceDAC.PM_State(0);
            ddlCityState.DataSource = dsState;
            ddlCityState.DataTextField = dsState.Tables[0].Columns["StateName"].ToString();
            ddlCityState.DataValueField = dsState.Tables[0].Columns["StateId"].ToString();
            ddlCityState.DataBind();
            ddlCityState.Items.Insert(0, new ListItem("---Select---", "0"));
            //ddlCityState.SelectedValue = ddlComuState.SelectedValue;

            //ddlComuCoun.SelectedValue = ddlCityCountry.SelectedValue;
        }
        protected void btnCityCancel_Click(object sender, EventArgs e)
        {
            pnlAddCity.Visible = false;

        }

        protected void btnCitySubmit_Click(object sender, EventArgs e)
        {
            int StateID = Convert.ToInt32(ddlCityState.SelectedValue);
            string CityName = txtCityName.Text;
            int Output = AttendanceDAC.InsCity(StateID, CityName);
            pnlAddCity.Visible = false;
            BindLocations(ddlfromCity);
            BindLocations(ddlToCity);
        }

    }
}