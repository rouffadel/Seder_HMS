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
using System.Collections.Generic;
using AECLOGIC.HMS.BLL;
using System.Linq;
using Aeclogic.Common.DAL;
using System.IO;
namespace AECLOGIC.ERP.HMS
{
    public partial class AllowanceBilling : AECLOGIC.ERP.COMMON.WebFormMaster
    {
        AttendanceDAC objAtt = new AttendanceDAC();
        bool viewall, Editable;
        int mid = 0;
        string menuname;
        string menuid;
        protected override void OnInit(EventArgs e)
        {
            if (Request.QueryString["ModuleID"] != null)
            {
                ModuleID = Convert.ToInt32(Request.QueryString["ModuleID"]);
            }
            else
            {
                ModuleID = 1;
            }
            base.OnInit(e);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetParentMenuId();
                BindddlPrepadiFor();
                ViewState["Auid"] = 0;
                ViewState["Advance"] = 0;
                ViewState["PodetID"] = 0;
                ViewState["HBLID"] = 0;
                ViewState["VendorID"] = 0;
                ViewState["WODate"] = string.Empty;
                ViewState["VendorName"] = string.Empty;
                Session["WONO"] = 0;
                BindYears();
                BindWorksites();
                GetLedger();
                if (Request.QueryString.Count > 0)
                {
                    //int id = Convert.ToInt32(Request.QueryString["key"].ToString());
                    int id = Convert.ToInt32(Request.QueryString["key"]);
                    if (id == 0)
                    {
                        tblCancelPO.Visible = true;
                        DataSet ds = AttendanceDAC.HR_WOViewToCancel(0, Convert.ToInt32(Session["CompanyID"]));
                        if (ds.Tables.Count > 0)
                        {
                            gvWOReport.DataSource = ds;
                            gvWOReport.DataBind();
                        }
                    }
                    if (id == 1)
                    {
                        BindGrid();
                        tblView.Visible = true;
                        tblVerify.Visible = false;
                    }
                    if (id == 2)
                    {
                        BindYears();
                        int WS = 1;
                        int Month = Convert.ToInt32(ddlMonth.SelectedValue);
                        int Year = Convert.ToInt32(ddlYear.SelectedValue);
                        DataSet ds = AttendanceDAC.HR_GetBilledPayments(WS, Month, Year);
                        gvVerify.DataSource = ds;
                        gvVerify.DataBind();
                        ddlWS.SelectedIndex = 1;
                        tblView.Visible = false;
                        tblVerify.Visible = true;
                    }
                }
                else
                {
                    tblCancelPO.Visible = true;
                    DataSet ds = AttendanceDAC.HR_WOViewToCancel(0, Convert.ToInt32(Session["CompanyID"]));
                    if (ds.Tables.Count > 0)
                    {
                        gvWOReport.DataSource = ds;
                        gvWOReport.DataBind();
                    }
                }
                chkWORenewal.Checked = false;
                //lblprePaidText.Visible = false;
                //ddlPrepadiFor.Visible = false;
                chkAR.Checked = false;
                Master.Page.ClientScript.RegisterStartupScript(this.GetType(), "abcd", "ShowHideDiv(" + chkAR.ClientID + ");", true);
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
                 Editable = (bool)ds.Tables[0].Rows[0]["Editable"];
                btnHLSave.Enabled = Editable;
            }
            return MenuId;
        }
        public void BindWorksites()
        {
            DataSet ds = AttendanceDAC.GetWorkSite(0, '1', Convert.ToInt32(Session["CompanyID"]));
            FIllObject.FillDropDown(ref ddlWorkSite, "HR_GetWorkSite_By_WO");
            ddlWS.DataSource = ds.Tables[0];
            ddlWS.DataTextField = "Site_Name";
            ddlWS.DataValueField = "Site_ID";
            ddlWS.DataBind();
            ddlWS.Items.Insert(0, new ListItem("---All---", "0", true));
            FIllObject.FillDropDown(ref ddlWSWOC, "HR_GetWorkSite_By_WO");
        }
        public void BindYears()
        {
            DataSet ds = AttendanceDAC.GetCalenderYear();
            if (ds.Tables[0].Rows.Count > 0)
            {
                int i = 0;
                int Maxyear = Convert.ToInt32(ds.Tables[0].Rows[0]["MaxYear"].ToString());
                for (int Minyear = Convert.ToInt32(ds.Tables[0].Rows[0]["MinYear"].ToString()); Minyear <= Maxyear; Minyear++)
                {
                    ddlYear.Items.Insert(i, new ListItem(Convert.ToString(Minyear), Convert.ToString(Minyear)));
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
        }
        public void BindGrid()
        {
            int? WorkSIteID = null;
            if (ddlWorkSite.SelectedItem.Value != "0")
            {
                WorkSIteID = Convert.ToInt32(ddlWorkSite.SelectedItem.Value);
            }
            DataSet ds = AttendanceDAC.HR_GetHiredLandBulidings(WorkSIteID, Convert.ToInt32(Session["CompanyID"]));
            if (ds.Tables[0].Rows.Count > 0)
            {
                gvView.DataSource = ds;
                gvView.DataBind();
            }
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionWorkList(string prefixText, int count, string contextKey)
        {
            DataSet ds = AttendanceDAC.HR_GetWorkSite_googlesearch_By_WO(prefixText);
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
        public static string[] GetCompletionWorksiteListsearch(string prefixText, int count, string contextKey)
        {
            DataSet ds = AttendanceDAC.HR_GetWorkSite_googlesearch_By_WO(prefixText);
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
        protected void GetWorksiteSearch(object sender, EventArgs e)
        {
            SqlParameter[] par = new SqlParameter[1];
            par[0] = new SqlParameter("@Search", txtSearchworksiteWOC.Text);
            FIllObject.FillDropDown(ref ddlWSWOC, "HR_GetWorkSite_googlesearch_By_WO", par);
            ListItem itmSelected = ddlWSWOC.Items.FindByText(txtSearchworksiteWOC.Text);
            if (itmSelected != null)
            {
                ddlWSWOC.SelectedItem.Selected = false;
                itmSelected.Selected = true;
            }
            ddlWSWOC_SelectedIndexChanged(sender, e);
        }
        protected void GetWorksite(object sender, EventArgs e)
        {
            SqlParameter[] par = new SqlParameter[1];
            par[0] = new SqlParameter("@Search", TextWorksite.Text);
            FIllObject.FillDropDown(ref ddlWorkSite, "HR_GetWorkSite_googlesearch_By_WO", par);
            ListItem itmSelected = ddlWorkSite.Items.FindByText(TextWorksite.Text);
            if (itmSelected != null)
            {
                ddlWorkSite.SelectedItem.Selected = false;
                itmSelected.Selected = true;
                //FillProjects();
            }
            ddlWorkSite_SelectedIndexChanged(sender, e);
        }
        public static string DataRowToDouble(DataRow dr)
        {
            return dr["Name"].ToString();
        }
        protected void ddlWorkSite_SelectedIndexChanged(object sender, EventArgs e)
        {
            lnkAll.Visible = false;
            int WS = Convert.ToInt32(ddlWorkSite.SelectedValue);
            DataSet ds = AttendanceDAC.HR_GetHiredLandBulidings(WS, Convert.ToInt32(Session["CompanyID"]));
            gvView.DataSource = ds;
            gvView.DataBind();
            gvView.Columns[8].Visible = false;
        }
        protected string FormatItem(object Status)
        {
            string retValue = "";
            string input = Status.ToString();
            if (input == "1")
            {
                retValue = "Land";
            }
            if (input == "2")
            {
                retValue = "Building";
            }
            return retValue;
        }
        protected string FormatType(object Status)
        {
            string retValue = "";
            string input = Status.ToString();
            if (input == "1")
            {
                retValue = "Instant";
            }
            if (input == "2")
            {
                retValue = "Week";
            }
            if (input == "3")
            {
                retValue = "FortNight";
            }
            if (input == "4")
            {
                retValue = "Monthly";
            }
            if (input == "5")
            {
                retValue = "Quarter-Yearly";
            }
            if (input == "6")
            {
                retValue = "Half-Yearly";
            }
            if (input == "7")
            {
                retValue = "Yearly";
            }
            return retValue;
        }
        public string PONavigateUrl(string POID)
        {
            bool Fals = false;
            return "javascript:return window.open('ProPurchaseOrderPrint.aspx?id=" + POID + "&PON=" + 1 + "&tot=" + Fals + "' , '_blank')";
        }
        public string DocNavigateUrl(string WONO)
        {
            return "javascript:return window.open('HiredItemDocs.aspx?id=" + WONO + "', '_blank')";
        }
        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            tblView.Visible = true;
            tblVerify.Visible = false;
        }
        protected string FormatInput(object Status)
        {
            string retValue = "";
            string input = Status.ToString();
            if (input == "1")
            {
                retValue = "Approved";
            }
            if (input == "2")
            {
                retValue = "Pending";
            }
            return retValue;
        }
        protected string Format(object Status)
        {
            string retValue = "";
            string input = Status.ToString();
            if (input == "0")
            {
                retValue = "Cancelled";
            }
            if (input == "1")
            {
                retValue = "Active";
            }
            if (input == "2")
            {
                retValue = "Close";
            }
            if (input == "3")
            {
                retValue = "Not Executed";
            }
            return retValue;
        }
        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            tblView.Visible = false;
            tblVerify.Visible = true;
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            int WS = Convert.ToInt32(ddlWS.SelectedValue);
            int Month = Convert.ToInt32(ddlMonth.SelectedValue);
            int Year = Convert.ToInt32(ddlYear.SelectedValue);
            DataSet ds = AttendanceDAC.HR_GetBilledPayments(WS, Month, Year);
            gvVerify.DataSource = ds;
            gvVerify.DataBind();
        }
        protected void ddlWSWOC_SelectedIndexChanged(object sender, EventArgs e)
        {
            int WS = Convert.ToInt32(ddlWSWOC.SelectedValue);
            DataSet ds = AttendanceDAC.HR_WOViewToCancel(WS, Convert.ToInt32(Session["CompanyID"]));
            gvWOReport.DataSource = ds;
            gvWOReport.DataBind();
            tblCancelPO.Visible = true;
            lnkShowAll0.Visible = false;
            tblHired.Visible = tblItemview.Visible = tblMain.Visible = false;
        }
        protected void gvWOReport_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Config")
            {
                //Master.Page.ClientScript.RegisterStartupScript(this.GetType(), "abcd", "ShowHideDiv(" + chkAR.ClientID + ");", true);
                chkWORenewal.Visible = false;
                gvWOReport.ShowFooter = false;
                tblMain.Visible = true;
                tblHired.Visible = true;
                int WONO = Convert.ToInt32(e.CommandArgument);
                string PoWo = WONO.ToString();
                Session["WONO"] = WONO;
                GetWs();
                txtWO.Text = WONO.ToString();
                DataSet ds = AttendanceDAC.HR_GetWODetails(WONO);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    lblHPurpose.Text = ds.Tables[0].Rows[0]["PONAME"].ToString();
                    lblHVName.Text = ds.Tables[0].Rows[0]["vendor_name"].ToString();
                    ViewState["VendorName"] = lblHVName.Text;
                    lblHVMobile.Text = ds.Tables[0].Rows[0]["mobile"].ToString();
                    lblHArmentOn.Text = ds.Tables[0].Rows[0]["PODate"].ToString();
                    ViewState["WODate"] = lblHArmentOn.Text;
                    txtHLVAddress.Text = ds.Tables[0].Rows[0]["vendor_address"].ToString();
                    ddlHLWS.SelectedValue = ds.Tables[0].Rows[0]["Site_ID"].ToString();
                    ViewState["VendorID"] = Convert.ToInt32(ds.Tables[0].Rows[0]["vendor_id"].ToString());
                    lblHLQty.Text = Convert.ToInt32(ds.Tables[0].Rows[0]["QTY"]).ToString();
                    trHLQty.Visible = true;
                    lblHAmount.Text = Convert.ToInt32(ds.Tables[0].Rows[0]["AMOUNT"]).ToString();
                    ddlHLHirType.SelectedValue = ds.Tables[0].Rows[0]["Typeid"].ToString();
                    trHLMonth2.Visible = true; //ddlHLHirType.SelectedIndex = 1;
                    ViewState["PodetID"] = ds.Tables[0].Rows[0]["PodetID"].ToString();
                    ViewState["Auid"] = ds.Tables[0].Rows[0]["Unit"].ToString();
                    txtHLFromDay.Text = lblHArmentOn.Text;
                    if (ds.Tables.Count > 1)
                    {
                        tblItemview.Visible = true;
                        gvItemview.DataSource = ds.Tables[1];
                        gvItemview.DataBind();
                    }
                    DataSet ds1 = AttendanceDAC.HR_GetItemAddress(WONO);
                    if (ds1.Tables[0].Rows.Count > 0)
                    {
                        ViewState["HBLID"] = ds1.Tables[0].Rows[0][0].ToString();
                        txtHLLAddress.Text = ds1.Tables[0].Rows[0][1].ToString();
                        txtHLBAOL.Text = ds1.Tables[0].Rows[0][2].ToString();
                        ddlType.SelectedValue = ds1.Tables[0].Rows[0][3].ToString();
                        txtAdvance.Text = ds1.Tables[0].Rows[0][4].ToString();
                        if (ds1.Tables[0].Rows[0][5].ToString() != "")
                        {
                            chkAR.Checked = Convert.ToBoolean(ds1.Tables[0].Rows[0][5]);
//                            ddlPrepadiFor.SelectedValue = ds1.Tables[0].Rows[0]["prePaidMonths"].ToString();
                        }
                        else
                        {
                            chkAR.Checked = false;
                        }
                        if (txtAdvance.Text == string.Empty)
                        {
                            txtAdvance.Text = "0.0";
                        }
                        ddlLedger.SelectedValue = ds1.Tables[0].Rows[0]["seLedID"].ToString();
                        ViewState["Advance"] = txtAdvance.Text;
                        txtHLFromDay.Text = ds1.Tables[0].Rows[0][6].ToString();
                    }
                    chkAR_CheckedChanged(null, null);
                    DataSet ds2 = AttendanceDAC.sh_GetHirePrepaidDet(WONO);
                     if (ds2.Tables[0].Rows.Count > 0)
                     {
                         ddlPrepadiFor.SelectedValue = ds2.Tables[0].Rows[0]["prePaidMonths"].ToString();
                         chkWORenewal.Visible = Convert.ToBoolean(ds2.Tables[1].Rows[0]["visible"]);
                         ddlPrepadiFor.Enabled = false;
                     }
                    DataSet dsSingle = AttendanceDAC.HR_GetSigleWODetails(Convert.ToInt32(ddlWSWOC.SelectedValue), WONO);
                    gvWOReport.DataSource = dsSingle;
                    gvWOReport.DataBind();
                    lnkShowAll0.Visible = true;
                }
            }
            if (e.CommandName == "Close")
            {
                int PONo = Convert.ToInt32(e.CommandArgument);
                LinkButton lnk = (LinkButton)e.CommandSource;
                GridViewRow gvr = (GridViewRow)lnk.Parent.Parent;
                Label lblItem = (Label)gvr.FindControl("lblItemId");
                int Itemid = Convert.ToInt32(lblItem.Text);
                AttendanceDAC.MMS_CLOSEPO(PONo, Itemid,  Convert.ToInt32(Session["UserId"]));
                AttendanceDAC.sa_ClosedWOPrePaidRent(PONo, CompanyID, Convert.ToInt32(Session["UserId"]));                
                int WS = Convert.ToInt32(ddlWSWOC.SelectedValue);
                DataSet ds = AttendanceDAC.HR_WOViewToCancel(WS, Convert.ToInt32(Session["CompanyID"]));
                gvWOReport.DataSource = ds;
                gvWOReport.DataBind();
                AlertMsg.MsgBox(Page, "Done!");
            }
        }
        protected void gvView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Del")
            {
                int WONO = Convert.ToInt32(e.CommandArgument);
                AttendanceDAC.HR_DelHiredLandsBuildings(WONO);
                DataSet ds = AttendanceDAC.HR_GetHiredLandBulidings(Convert.ToInt32(ddlWorkSite.SelectedValue), Convert.ToInt32(Session["CompanyID"]));
                gvView.DataSource = ds;
                gvView.DataBind();
            }
            if (e.CommandName == "Config")
            {
                gvWOReport.ShowFooter = false;
                tblMain.Visible = true;
                tblHired.Visible = true;
                int WONO = Convert.ToInt32(e.CommandArgument);
                string PoWo = WONO.ToString();
                Session["WONO"] = WONO;
                GetWs();
                txtWO.Text = WONO.ToString();
                DataSet ds = AttendanceDAC.HR_GetWODetails(WONO);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    lblHPurpose.Text = ds.Tables[0].Rows[0]["PONAME"].ToString();
                    lblHVName.Text = ds.Tables[0].Rows[0]["vendor_name"].ToString();
                    ViewState["VendorName"] = lblHVName.Text;
                    lblHVMobile.Text = ds.Tables[0].Rows[0]["mobile"].ToString();
                    lblHArmentOn.Text = ds.Tables[0].Rows[0]["PODate"].ToString();
                    ViewState["WODate"] = lblHArmentOn.Text;
                    txtHLVAddress.Text = ds.Tables[0].Rows[0]["vendor_address"].ToString();
                    ddlHLWS.SelectedValue = ds.Tables[0].Rows[0]["Site_ID"].ToString();
                    ViewState["VendorID"] = Convert.ToInt32(ds.Tables[0].Rows[0]["vendor_id"].ToString());
                    lblHAmount.Text = Convert.ToInt32(ds.Tables[0].Rows[0]["AMOUNT"]).ToString();
                    ddlHLHirType.SelectedValue = ds.Tables[0].Rows[0]["Typeid"].ToString();
                    trHLMonth2.Visible = true; //ddlHLHirType.SelectedIndex = 1;
                    ViewState["PodetID"] = ds.Tables[0].Rows[0]["PodetID"].ToString();
                    ViewState["Auid"] = ds.Tables[0].Rows[0]["Unit"].ToString();
                    txtHLFromDay.Text = lblHArmentOn.Text;
                    if (ds.Tables.Count > 1)
                    {
                        tblItemview.Visible = true;
                        gvItemview.DataSource = ds.Tables[1];
                        gvItemview.DataBind();
                    }
                    DataSet ds1 = AttendanceDAC.HR_GetItemAddress(WONO);
                    if (ds1.Tables[0].Rows.Count > 0)
                    {
                        ViewState["HBLID"] = ds1.Tables[0].Rows[0][0].ToString();
                        txtHLLAddress.Text = ds1.Tables[0].Rows[0][1].ToString();
                        txtHLBAOL.Text = ds1.Tables[0].Rows[0][2].ToString();
                        ddlType.SelectedValue = ds1.Tables[0].Rows[0][3].ToString();
                        txtAdvance.Text = ds1.Tables[0].Rows[0][4].ToString();
                        chkAR.Checked = Convert.ToBoolean(ds1.Tables[0].Rows[0][5]);
                        if (txtAdvance.Text == string.Empty)
                        {
                            txtAdvance.Text = "0.0";
                        }
                        ViewState["Advance"] = txtAdvance.Text;
                        txtHLFromDay.Text = ds1.Tables[0].Rows[0][6].ToString();
                    }
                    gvView.ShowFooter = false;
                    DataSet dsSingle = AttendanceDAC.HR_GetHiredLandBulidingsByID(Convert.ToInt32(ddlWorkSite.SelectedValue), WONO);
                    gvView.DataSource = dsSingle;
                    gvView.DataBind();
                    lnkAll.Visible = true;
                }
            }
        }
        public void GetLedger()
        {
            DataSet ds = AttendanceDAC.HR_GetSDLedgers();
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlLedger.DataSource = ds;
                ddlLedger.DataTextField = "Ledger";
                ddlLedger.DataValueField = "LedgerID";
                ddlLedger.DataBind();
                ddlLedger.Items.Insert(0, new ListItem("---SELECT---", "0", true));
            }
        }
        public void GetWs()
        {
            DataSet dstemp = AttendanceDAC.GetWorkSite(0, '1', Convert.ToInt32(Session["CompanyID"]));
            ddlHLWS.DataSource = dstemp.Tables[0];
            ddlHLWS.DataTextField = "Site_Name";
            ddlHLWS.DataValueField = "Site_ID";
            ddlHLWS.DataBind();
            ddlHLWS.Items.Insert(0, new ListItem("---SELECT---", "0", true));
        }
        protected void btnHLSave_Click(object sender, EventArgs e)
        {
            double poitemQty = Convert.ToDouble(lblHLQty.Text);
            double Rate=0;
            double totAmt= Convert.ToDouble(lblHAmount.Text);
            Rate=totAmt/poitemQty;
            double prepaidMonths = 0;
            double PrePaidSecAmt = 0;
            int PrePaidRentQty = 0;
            if (chkAR.Checked == true)
            {
                PrePaidRentQty = Convert.ToInt32(ddlPrepadiFor.SelectedValue);
                prepaidMonths = Convert.ToDouble(ddlPrepadiFor.SelectedValue);
                if (prepaidMonths > poitemQty)
                {
                    AlertMsg.MsgBox(Page, "Prepaid for (Months) Should Not greater than PO Qty!");
                    return;
                }
                PrePaidSecAmt = Math.Round(prepaidMonths * Rate, 2);
            }            
            int VendorID = Convert.ToInt32(ViewState["VendorID"]);
            int PODetID = Convert.ToInt32(ViewState["PodetID"]);
            string VendorName = ViewState["VendorName"].ToString();
            Label lbl = new Label(); lbl.Text = ViewState["WODate"].ToString();
            DateTime WoDate = CODEUtility.ConvertToDate(lbl.Text.Trim(), DateFormat.DayMonthYear);
            int WONo = Convert.ToInt32(txtWO.Text);
            int Item = Convert.ToInt32(ddlType.SelectedValue);
            DateTime AgremtOn = CODEUtility.ConvertToDate(lblHArmentOn.Text.Trim(), DateFormat.DayMonthYear);
            string Pupose = lblHPurpose.Text;
            string VName = lblHVName.Text;
            string VMobile = lblHVMobile.Text;
            string VAddress = txtHLVAddress.Text;
            int HireType = Convert.ToInt32(ddlHLHirType.SelectedValue);
            int HBLID = Convert.ToInt32(ViewState["HBLID"]);
            double MonthlyRent = Convert.ToDouble(lblHAmount.Text.ToString());
            DateTime HireFrom = CODEUtility.ConvertToDate(txtHLFromDay.Text.Trim(), DateFormat.DayMonthYear);
            string ItemAddress = txtHLLAddress.Text;
            string ItemSpecification = txtHLBAOL.Text;
            int ForWS = Convert.ToInt32(ddlHLWS.SelectedValue);
            int SubmittedBy =  Convert.ToInt32(Session["UserId"]);
            int ForWorkSite = Convert.ToInt32(ddlHLWS.SelectedValue);
            double Advance = Convert.ToDouble(txtAdvance.Text);
            bool IsAdvanceRent = Convert.ToBoolean(chkAR.Checked);
            string Unit = ViewState["Auid"].ToString();
            AttendanceDAC.HR_InsUpHiredLandBuildings(HBLID, Convert.ToInt32(ViewState["PodetID"]), WONo, WoDate, VendorID, VendorName, Item, HireType, HireFrom,
                                                   MonthlyRent, ItemAddress, ItemSpecification, SubmittedBy, ForWorkSite, Advance, IsAdvanceRent, Unit);
            int LedgerID = Convert.ToInt32(ddlLedger.SelectedValue);
            bool IsWoRenewal = Convert.ToBoolean(chkWORenewal.Checked);            
            //AttendanceDAC.HR_InsUpHireAdvance(WONo,  Convert.ToInt32(Session["UserId"]), Advance, LedgerID);
            AttendanceDAC.sa_InsertPrepaidSecurityDep(WONo, Convert.ToInt32(Session["UserId"]), Advance, LedgerID, PrePaidSecAmt,
                CompanyID, PrePaidRentQty, IsWoRenewal);
            AlertMsg.MsgBox(Page, "Done", AlertMsg.MessageType.Success);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect",
            "window.location='" +Request.ApplicationPath + "HMS/AllowanceBilling.aspx?key=1';", true);
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect",
            //"alert('Done'); window.location='" +
            //Request.ApplicationPath + "HMS/AllowanceBilling.aspx?key=1';", true);
        }
        protected string GetTotal1()
        {
            int WS = Convert.ToInt32(ddlWorkSite.SelectedValue);
            if (WS == 0) { WS = 1; }
            DataSet ds = AttendanceDAC.HR_GetHiredLandBulidings(WS, Convert.ToInt32(Session["CompanyID"]));
            string total = ds.Tables[1].Rows[0][0].ToString();
            return total.ToString();
        }
        protected string GetTotal2()
        {
            int WS = Convert.ToInt32(ddlWS.SelectedValue);
            int Month = Convert.ToInt32(ddlMonth.SelectedValue);
            int Year = Convert.ToInt32(ddlYear.SelectedValue);
            DataSet ds = AttendanceDAC.HR_GetBilledPayments(WS, Month, Year);
            string total = ds.Tables[1].Rows[0][0].ToString();
            return total.ToString();
        }
        protected string GetTotal3()
        {
            int WS = Convert.ToInt32(ddlWSWOC.SelectedValue);
            DataSet ds = AttendanceDAC.HR_WOViewToCancel(WS, Convert.ToInt32(Session["CompanyID"]));
            string total = ds.Tables[1].Rows[0][0].ToString();
            return total.ToString();
        }
        protected void BtnItemsSelect_Click(object sender, EventArgs e)
        {
        }
        protected void lnkAmtHike_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "pop", "window.open('./HireAmountHike.aspx?WOID=" + Convert.ToInt32(Session["PoWo"]) + "&Amt=" + lblHAmount.Text + "&type=" + Convert.ToInt32(ddlHLHirType.SelectedValue) + "' , '_blank')", true);
            tblHired.Visible = true;
        }
        protected void lnkShowAll0_Click(object sender, EventArgs e)
        {
            gvWOReport.ShowFooter = true;
            int WS = Convert.ToInt32(ddlWSWOC.SelectedValue);
            DataSet ds = AttendanceDAC.HR_WOViewToCancel(WS, Convert.ToInt32(Session["CompanyID"]));
            gvWOReport.DataSource = ds;
            gvWOReport.DataBind();
            tblMain.Visible = tblItemview.Visible = tblHired.Visible = false;
        }
        protected void lnkAll_Click(object sender, EventArgs e)
        {
            gvView.ShowFooter = true;
            DataSet dsSingle = AttendanceDAC.HR_GetHiredLandBulidings(Convert.ToInt32(ddlWorkSite.SelectedValue), Convert.ToInt32(Session["CompanyID"]));
            gvView.DataSource = dsSingle;
            gvView.DataBind();
        }
        protected void gvView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hlnkDoc = (HyperLink)e.Row.FindControl("hlnkDoc");
                LinkButton lnkConfig = (LinkButton)e.Row.FindControl("lnkConfig");
                LinkButton lnkDel = (LinkButton)e.Row.FindControl("lnkDel");
                hlnkDoc.Enabled = lnkDel.Enabled = lnkConfig.Enabled = Editable;
            }
        }
        private void BindddlPrepadiFor()
        {
            ddlPrepadiFor.Items.AddRange(Enumerable.Range(1, 100).Select(e => new ListItem(e.ToString())).ToArray());
        }
        protected void chkAR_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAR.Checked == true)
                divPrePaid.Visible = true;
            else
                divPrePaid.Visible = false;
        }
    }
}