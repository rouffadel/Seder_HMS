using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using AECLOGIC.HMS.BLL;
using AECLOGIC.ERP.COMMON;

namespace AECLOGIC.ERP.HMS
{
    public partial class ListOfEmpMessConfiguration : AECLOGIC.ERP.COMMON.WebFormMaster
    {
        #region Declaration
        int mid = 0;
        bool viewall;
        string menuname;
        static int SearchCompanyID;
        string menuid;
        AttendanceDAC objatt = new AttendanceDAC();
        HRCommon objHrCommon = new HRCommon();
        #endregion Declaration

        #region Paging
        protected override void OnInit(EventArgs e)
        {
            ModuleID = 1;
            base.OnInit(e);
       
            ListOfEmpMessPaging.FirstClick += new Paging.PageFirst(ListOfEmpMessPaging_FirstClick);
            ListOfEmpMessPaging.PreviousClick += new Paging.PagePrevious(ListOfEmpMessPaging_FirstClick);
            ListOfEmpMessPaging.NextClick += new Paging.PageNext(ListOfEmpMessPaging_FirstClick);
            ListOfEmpMessPaging.LastClick += new Paging.PageLast(ListOfEmpMessPaging_FirstClick);
            ListOfEmpMessPaging.ChangeClick += new Paging.PageChange(ListOfEmpMessPaging_FirstClick);
            ListOfEmpMessPaging.ShowRowsClick += new Paging.ShowRowsChange(ListOfEmpMessPaging_ShowRowsClick);
            ListOfEmpMessPaging.CurrentPage = 1;
        }
        void ListOfEmpMessPaging_ShowRowsClick(object sender, EventArgs e)
        {
            ListOfEmpMessPaging.CurrentPage = 1;
            BindPager();
        }
        void ListOfEmpMessPaging_FirstClick(object sender, EventArgs e)
        {
            BindPager();
        }
        void BindPager()
        {

            objHrCommon.PageSize = ListOfEmpMessPaging.CurrentPage;
            objHrCommon.CurrentPage = ListOfEmpMessPaging.ShowRows;
            BindMessTypes(objHrCommon);
        }
        #endregion Paging

        #region PageLoad
        protected void Page_Load(object sender, EventArgs e)
        {
          
            if (!IsPostBack)
            {
                GetParentMenuId();
                if (Convert.ToInt32(Request.QueryString["id"]) != 1)
                {
                    dvAdd.Visible = true;
                }
                else
                {
                    dvEdit.Visible = true;
                }
                ViewState["MCID"] = "";
                FillMesstypes();
                FillWS();
                FillEmpNature();
                BindYears();
                BindPager();
            }
        }
        #endregion PageLoad

        protected void GetWork(object sender, EventArgs e)
        {

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@Search", txtSearchWorksite.Text);
            param[1] = new SqlParameter("@CompanyID", Convert.ToInt32(Session["CompanyID"]));
            FIllObject.FillDropDown(ref ddlWS, "G_GET_WorkSitebyFilter", param);
            ListItem itmSelected = ddlWS.Items.FindByText(txtSearchWorksite.Text);
            if (itmSelected != null)
            {
                ddlWS.SelectedItem.Selected = false;
                itmSelected.Selected = true;
            }
           
        }

        #region supporting methods

        public int GetParentMenuId()
        {
            string URL = Request.Url.Segments[Request.Url.Segments.Length - 1] + Request.Url.Query;
            int RoleId = Convert.ToInt32(Session["RoleId"].ToString());
            int ModuleId = ModuleID;;
         DataSet   ds = AttendanceDAC.GetAllowed(RoleId, ModuleId, URL);
            int MenuId = 0;
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                MenuId = Convert.ToInt32(ds.Tables[0].Rows[0]["Under"]);
               
                btnSubmit.Enabled = (bool)ds.Tables[0].Rows[0]["Editable"];
            }
            return MenuId;
        }
        public void BindMessTypes(HRCommon objHrCommon)
        {
            try
            {
                objHrCommon.PageSize = ListOfEmpMessPaging.ShowRows;
                objHrCommon.CurrentPage = ListOfEmpMessPaging.CurrentPage;
                int? WS = null;
                if (ddlWorkSite.SelectedValue != "0")
                    WS = Convert.ToInt32(ddlWorkSite.SelectedValue);
                int? MessType = null;
                if (ddlMessType1.SelectedValue != "0")
                    MessType = Convert.ToInt32(ddlMessType1.SelectedValue);
                int? Month = null;
                if (ddlMonth.SelectedValue != "0")
                    Month = Convert.ToInt32(ddlMonth.SelectedValue);
                int? Year = null;
                if (ddlYear.SelectedValue != "0")
                    Year = Convert.ToInt32(ddlYear.SelectedValue);
                int Status;
                if (rblMessStatus.SelectedValue == "1")
                    Status = 1;
                else
                    Status = 0;
                int? EmpNatID = null;
                if (ddlEmpNat.SelectedValue != "0")
                    EmpNatID = Convert.ToInt32(ddlEmpNat.SelectedValue);
                   
              DataSet  ds = AttendanceDAC.GetMessTypes(objHrCommon, WS, MessType, Month, Year, Status, EmpNatID);
                if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0)
                {
                    gvMessTypes.DataSource = ds;
                    gvMessTypes.DataBind();
                }
                else
                {
                    gvMessTypes.DataSource = null;
                    gvMessTypes.DataBind();
                }
                ListOfEmpMessPaging.Bind(objHrCommon.CurrentPage, objHrCommon.TotalPages, objHrCommon.NoofRecords, objHrCommon.PageSize);

            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public void GetMessTypeDetailsByMCID(int MCID)
        {

            DataSet ds = AttendanceDAC.GetSetupMesstypeDetailsByMCID(MCID);
            ddlMessType.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["MID"]);
            ddlWS.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["WsID"]);
            ddlProfileType.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["NatureID"]);
            txtFrmDate.Text = ds.Tables[0].Rows[0]["FromDate"].ToString();
            txtToDate.Text = ds.Tables[0].Rows[0]["ToDate"].ToString();
            txtAmount.Text = ds.Tables[0].Rows[0]["Value"].ToString();
            if (Convert.ToInt32(ds.Tables[0].Rows[0]["IsActive"]) == 1)
                chkStatus.Checked = true;
            else
                chkStatus.Checked = false;

        }
        public void CheangeStatusOfMessType(int MCID, int Status)
        {
            AttendanceDAC.ChangeStatusMesstypeDetails(MCID, Status);
        }

        public string GetText()
        {

            if (rblMessStatus.SelectedValue == "1")
            {
                return "InActive";
            }
            else
            {
                return "Active";
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
            ddlYear.SelectedValue = ds.Tables[0].Rows[0]["CurrentYear"].ToString();

        }
        #endregion supporting methods

        #region Fillddls

        public void FillMesstypes()
        {

            DataSet ds = AttendanceDAC.GetTypeOfMessCofigs(1);
            ddlMessType.DataSource = ds;
            ddlMessType.DataTextField = "Name";
            ddlMessType.DataValueField = "MID";
            ddlMessType.DataBind();
            ddlMessType.Items.Insert(0, new ListItem("---Select---", "0"));

            ddlMessType1.DataSource = ds;
            ddlMessType1.DataTextField = "Name";
            ddlMessType1.DataValueField = "MID";
            ddlMessType1.DataBind();
            ddlMessType1.Items.Insert(0, new ListItem("---All---", "0"));
        }
        public void FillWS()
        {

            DataSet ds = AttendanceDAC.GetWorkSite(0, '1', Convert.ToInt32(Session["CompanyID"]));
            ddlWS.DataSource = ds;
            ddlWS.DataTextField = "Site_Name";
            ddlWS.DataValueField = "Site_ID";
            ddlWS.DataBind();
            ddlWS.Items.Insert(0, new ListItem("---Select---", "0"));

            DataSet ds1 = AttendanceDAC.GetWorkSite_By_MessType();
            ddlWorkSite.DataSource = ds1;
            ddlWorkSite.DataTextField = "Site_Name";
            ddlWorkSite.DataValueField = "Site_ID";
            ddlWorkSite.DataBind();
            ddlWorkSite.Items.Insert(0, new ListItem("---All---", "0"));
        }
        public void FillEmpNature()
        {

            DataSet ds = Leaves.GetEmpNatureList(1);
            ddlProfileType.DataSource = ds;
            ddlProfileType.DataTextField = "Nature";
            ddlProfileType.DataValueField = "NatureOfEmp";
            ddlProfileType.DataBind();
            ddlProfileType.Items.Insert(0, new ListItem("---Select---", "0"));

            //ddlEmpNat
            ddlEmpNat.DataSource = ds;
            ddlEmpNat.DataTextField = "Nature";
            ddlEmpNat.DataValueField = "NatureOfEmp";
            ddlEmpNat.DataBind();
            ddlEmpNat.Items.Insert(0, new ListItem("---All---", "0"));

        }


        #endregion Filldds
        //Added By Rijwan for Worksite Google Search 
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionList(string prefixText, int count, string contextKey)
        {
            DataSet ds = AttendanceDAC.GetGoogleABCSearchWorkSite(prefixText, SearchCompanyID);
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

        #region Events
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                int? MCID = null;
                if (ViewState["MCID"].ToString() != null && ViewState["MCID"].ToString() != string.Empty)
                {
                    MCID = Convert.ToInt32(ViewState["MCID"].ToString());
                }
                int MID = Convert.ToInt32(ddlMessType.SelectedValue);
                int WS = Convert.ToInt32(ddlWS.SelectedValue);
                int EmpTypeID = Convert.ToInt32(ddlProfileType.SelectedValue);
                DateTime FrmDate = CODEUtility.ConvertToDate(txtFrmDate.Text, DateFormat.DayMonthYear);
                DateTime ToDate = CODEUtility.ConvertToDate(txtToDate.Text, DateFormat.DayMonthYear);
                double Amount = Convert.ToDouble(txtAmount.Text);
                int Status = 1;
                if (chkStatus.Checked == false)
                {
                    Status = 0;
                }
                int OutPut = AttendanceDAC.InsUpdateListofMessConfig(MCID, MID, WS, EmpTypeID, FrmDate, ToDate, Amount, Status,  Convert.ToInt32(Session["UserId"]));
                if (OutPut == 1)
                    AlertMsg.MsgBox(Page, "Inserted sucessfully.!");
                else if (OutPut == 2)
                    AlertMsg.MsgBox(Page, "Already exists.!");
                else
                    AlertMsg.MsgBox(Page, "Updated sucessfully.!");

                BindPager();
                //Clear();
                dvEdit.Visible = true;
                dvAdd.Visible = false;
            }
            catch (Exception LstOfHoliday)
            {
                AlertMsg.MsgBox(Page, LstOfHoliday.Message.ToString(),AlertMsg.MessageType.Error);
            }
        }
        protected void gvMessTypes_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int MCID = Convert.ToInt32(e.CommandArgument);
            ViewState["MCID"] = MCID;
            if (e.CommandName == "Edt")
            {
                GetMessTypeDetailsByMCID(MCID);
                dvAdd.Visible = true;
                dvEdit.Visible = false;
            }
            if (e.CommandName == "del")
            {
                GridViewRow gvr = (GridViewRow)((LinkButton)e.CommandSource).Parent.Parent;
                LinkButton lnkStatus = (LinkButton)gvMessTypes.Rows[gvr.RowIndex].FindControl("lnkDel");
                string MessStatus = lnkStatus.Text;
                int Status = 1;
                if (MessStatus != "Active")
                    Status = 0;

                CheangeStatusOfMessType(MCID, Status);
            }
            BindPager();
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindPager();

        }
        protected void rblMessStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListOfEmpMessPaging.CurrentPage = 1;
            BindPager();
        }
        #endregion Events
    }
}