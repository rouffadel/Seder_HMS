using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccessLayer;
using System.Data;
using Aeclogic.Common.DAL;
using System.Collections.Generic;
using System.Globalization;
using System.Data.SqlClient;
using System.Configuration;
using AECLOGIC.HMS.BLL;


namespace AECLOGIC.ERP.HMS
{
    public partial class SRN123 : AECLOGIC.ERP.COMMON.WebFormMaster
    {
        #region LoadEvents
        int mid = 0; string menuname; string menuid;
        bool viewall; int SRNId;
        //DataAccessLayer.dlGdn objGDN = new DataAccessLayer.dlGdn();
        Common objCommon = new Common();
        dlSRN objSRN = new dlSRN();
        int WSID; int TopRows; string CloseMsg = ""; string RejMsg = ""; string message = ""; int count; int Ret = 0;
        protected override void OnInit(EventArgs e)
        {
            ModuleID = 1;
            base.OnInit(e);
        
            //txtVendorSearch.Attributes.Add("onblur", "javascript:SearchVendorJS('" + ddlVendor.ClientID + "', '" + txtVendorSearch.ClientID + "')");
            //txtVehicleSearch.Attributes.Add("onblur", "javascript:SearchVehicleJS('" + ddlVehicle.ClientID + "', '" + txtVehicleSearch.ClientID + "')");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                string id =  Convert.ToInt32(Session["UserId"]).ToString();
            }
            catch
            {
                Response.Redirect("Home.aspx");
            }

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
                Ajax.Utility.RegisterTypeForAjax(typeof(SRN));
                SetUpScreen();

                if (Request.QueryString["ID"] != null && Request.QueryString["ID"] != string.Empty)
                {
                    int SRNID = Convert.ToInt32(Request.QueryString["ID"].ToString());
                    EditSRNDetails(SRNID.ToString());
                    lblSRNID.Text = "(SDN NO: " + SRNID.ToString() + " )";
                    btnback.Visible = true;
                }
                else
                {
                    DateTime Time = DateTime.Now;
                    if (DateTime.Now.Hour >= 12)
                    {
                        ddlTimeFormat.SelectedValue = "2";
                        txtMinutes.Text = DateTime.Now.Minute.ToString();
                        ddlstarttime.SelectedValue = Convert.ToInt32(DateTime.Now.Hour - 12).ToString();
                    }
                    else
                    {
                        ddlTimeFormat.SelectedValue = "1";
                        txtMinutes.Text = DateTime.Now.Minute.ToString();
                        ddlstarttime.SelectedValue = DateTime.Now.Hour.ToString();
                    }
                    txtDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                }
                DataTable dtAccParts = GDNAccPartsDatatable();
                ViewState["dtAccParts"] = dtAccParts;

                DataTable dtSRNItem = SRNItemsDatatable();
                ViewState["dtSRNItem"] = dtSRNItem;
            }
            if (Session["NewDriver"] != null)
            {
                ddlVehicle.Items.Clear();
                FillVehicleNameDropDown();
                Session["NewDriver"] = null;
            }

        }
        #endregion LoadEvents
        #region SetUpScreen
        private void GridBind(int SRNID)
        {
            if (ViewState["dtSRNItem"] != null)
            {
                DataTable dtSRN = (DataTable)ViewState["dtSRNItem"];
                if (hdFlag.Value == "1")
                {
                    DataSet ds = dlSRN.GetMMS_SRN_gvItemDetails(SRNID, 1);
                    if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            DataRow dtRow = ds.Tables[0].Rows[i];
                            dtRow = dtSRN.NewRow();
                            dtRow["PoName"] = ds.Tables[0].Rows[i]["PoName"].ToString();
                            dtRow["POID"] = ds.Tables[0].Rows[i]["POID"].ToString();
                            dtRow["Item"] = ds.Tables[0].Rows[i]["Item"].ToString();
                            dtRow["Qty"] = ds.Tables[0].Rows[i]["Qty"].ToString();
                            dtRow["RelQty"] = ds.Tables[0].Rows[i]["RelQty"].ToString();
                            dtRow["PodetID"] = ds.Tables[0].Rows[i]["PodetID"].ToString();
                            dtRow["Itemid"] = ds.Tables[0].Rows[i]["Itemid"].ToString();
                            dtRow["BalQty"] = ds.Tables[0].Rows[i]["BalQty"].ToString();
                            dtRow["Au_Name"] = ds.Tables[0].Rows[i]["Au_Name"].ToString();
                            dtRow["AuID"] = ds.Tables[0].Rows[i]["AuID"].ToString();
                            //dtRow["AuIDs"] = ds.Tables[0].Rows[i]["AuID"].ToString();
                            dtRow["Rate"] = ds.Tables[0].Rows[i]["Rate"].ToString();
                            dtRow["Unit"] = ds.Tables[0].Rows[i]["Unit"].ToString();
                            dtRow["SRNItemID"] = ds.Tables[0].Rows[i]["SRNItemID"].ToString();
                            dtSRN.Rows.Add(dtRow);
                        }
                    }
                    hdFlag.Value = "0";
                }
                //GetPoUnits(poid, woid);
                gvItemDetails.DataSource = dtSRN;
                gvItemDetails.DataBind();
                if (gvItemDetails.Rows.Count > 0)
                {
                    ddlVendor.Enabled = true;
                    ddlWorkSite.Enabled = true;
                }
                else
                {
                    ddlVendor.Enabled = false;
                    ddlWorkSite.Enabled = false;
                }
            }
        }
        private void SetUpScreen()
        {
            FillWorkSiteDropDown();
            LoadDropDown();
        }
        # region FillDropDownByWS
        private void FillDestinationRepresentativeDropDownByWO()
        {
            if (lstPODetails.SelectedValue != "")
            {
                int WSID = Convert.ToInt32(ddlWorkSite.SelectedValue);
                int POID = Convert.ToInt32(lstPODetails.SelectedValue);
                SqlParameter[] par = new SqlParameter[2];
                par[0] = new SqlParameter("@POID", POID);
                par[1] = new SqlParameter("@Prjid", WSID);
                FIllObject.FillDropDown(ref ddlDestRepre, "MMS_DDL_DestinationRepsByPO", par);
            }
        }
        # endregion
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
                gvItemDetails.Columns[2].Visible = (bool)ds.Tables[0].Rows[0]["Editable"];

                viewall = (bool)ViewState["ViewAll"];
                menuname = ds.Tables[0].Rows[0]["menuname"].ToString();
                menuid = MenuId.ToString();
                mid = Convert.ToInt32(ds.Tables[0].Rows[0]["MenuId"].ToString());
                btnGetPO.Enabled = (bool)ds.Tables[0].Rows[0]["Editable"];
                btnViewPO.Enabled = (bool)ds.Tables[0].Rows[0]["Editable"];
                btnAddDetails.Enabled = (bool)ds.Tables[0].Rows[0]["Editable"];
                btnSubmit.Enabled = (bool)ds.Tables[0].Rows[0]["Editable"];
                btnSave.Enabled = (bool)ds.Tables[0].Rows[0]["Editable"];
            }
            return MenuId;
        }
        #region FillDropDown
        private void FillVehicleNameDropDown()
        {
            WSID = Convert.ToInt32(ddlWorkSite.SelectedValue);
            SqlParameter[] par = new SqlParameter[2];
            par[0] = new SqlParameter("@WSID", WSID);
            if (txtAllVeh.Text != string.Empty && txtAllVeh.Text != "")
                par[1] = new SqlParameter("@Filter", txtAllVeh.Text);
            else
                par[1] = new SqlParameter("@Filter", System.Data.SqlDbType.Int);
            FIllObject.FillDropDown(ref ddlVehicle, "MMS_DDL_SRNVehiclesByWS", par);
            //LookUp.PopulateEntityDropDown(LookUp.EntityRelationships.TptVehicle);

        }
        private void FillVendorDropDown()
        {
            WSID = Convert.ToInt32(ddlWorkSite.SelectedValue);
            SqlParameter[] par = new SqlParameter[2];
            par[0] = new SqlParameter("@WSID", WSID);
            if (txtVendor.Text != string.Empty && txtVendor.Text != "")
                par[1] = new SqlParameter("@Filter", txtVendor.Text);
            else
                par[1] = new SqlParameter("@Filter", System.Data.SqlDbType.Int);
            FIllObject.FillDropDown(ref ddlVendor, "MMS_DDL_SRNVendorDetailsByWS", par);
            // LookUp.PopulateEntityDropDown(LookUp.EntityRelationships.ServiceVendor);

        }
        private void FillOriginDropDown()
        {
            WSID = Convert.ToInt32(ddlWorkSite.SelectedValue);
            SqlParameter[] par = new SqlParameter[2];
            par[0] = new SqlParameter("@WSID", WSID);
            if (txtOrigin.Text != string.Empty && txtOrigin.Text != "")
                par[1] = new SqlParameter("@Filter", txtOrigin.Text);
            else
                par[1] = new SqlParameter("@Filter", System.Data.SqlDbType.Int);
            FIllObject.FillDropDown(ref ddlOrigin, "MMS_DDL_SRNOriginsByWS", par);
            // LookUp.PopulateEntityDropDown(LookUp.EntityRelationships.Locations);
        }
        private void FillDestinationDropDown()
        {
            WSID = Convert.ToInt32(ddlWorkSite.SelectedValue);
            SqlParameter[] par = new SqlParameter[2];
            par[0] = new SqlParameter("@WSID", WSID);
            if (txtDest.Text != string.Empty && txtDest.Text != "")
                par[1] = new SqlParameter("@Filter", txtDest.Text);
            else
                par[1] = new SqlParameter("@Filter", System.Data.SqlDbType.Int);
            FIllObject.FillDropDown(ref ddlDestination, "MMS_DDL_SRNDestinationsByWS", par);
            // LookUp.PopulateEntityDropDown(LookUp.EntityRelationships.Locations);

        }
        private void FillOriginRepresentativeDropDown()
        {
            WSID = Convert.ToInt32(ddlWorkSite.SelectedValue);
            SqlParameter[] par = new SqlParameter[2];
            par[0] = new SqlParameter("@WSID", WSID);
            if (txtOriRep.Text != string.Empty && txtOriRep.Text != "")
                par[1] = new SqlParameter("@Filter", txtOriRep.Text);
            else
                par[1] = new SqlParameter("@Filter", System.Data.SqlDbType.Int);
            FIllObject.FillDropDown(ref ddlOriginRepre, "MMS_DDL_SRNOriginRepsByWS", par);
            //LookUp.PopulateEntityDropDown(LookUp.EntityRelationships.EmployeeMaster);

        }
        private void FillDestinationRepresentativeDropDown()
        {
            WSID = Convert.ToInt32(ddlWorkSite.SelectedValue);
            SqlParameter[] par = new SqlParameter[2];
            par[0] = new SqlParameter("@WSID", WSID);
            if (txtDestRep.Text != string.Empty && txtDestRep.Text != "")
                par[1] = new SqlParameter("@Filter", txtDestRep.Text);
            else
                par[1] = new SqlParameter("@Filter", System.Data.SqlDbType.Int);
            FIllObject.FillDropDown(ref ddlDestRepre, "MMS_DDL_SRNDestinationRepsByWS", par);
            // LookUp.PopulateEntityDropDown(LookUp.EntityRelationships.EmployeeMaster);

        }
        private void FillListBoxPO()
        {
            int CompanyID = Convert.ToInt32(Session["CompanyID"].ToString());
            lstPODetails.Items.Clear();
            lstPODetails.Items.Add(new ListItem("No WOrk Order Orders Found", "0"));
            DataSet ds = dlSRN.GetMMS_MIS_lstWODetails(int.Parse(ddlVendor.SelectedValue), int.Parse(ddlWorkSite.SelectedValue), 1, Convert.ToInt32(Application["ModuleId"].ToString()), CompanyID);
            if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0)
            {
                lstPODetails.DataSource = ds;
                lstPODetails.DataValueField = "ID";
                lstPODetails.DataTextField = "Name";
                lstPODetails.DataBind();
                lstPODetails.Enabled = true;
            }
        }
        private void FillListBoxMaterial()
        {
            lstItemDetails.Items.Clear();
            DataSet ds = new DataSet();
            ds = SQLDBUtil.ExecuteDataset("MMS_DDL_Resources");
            lstItemDetails.DataSource = ds;
            lstItemDetails.DataTextField = "Name";
            lstItemDetails.DataValueField = "ID";
            lstItemDetails.DataBind();
            lstItemDetails.Items.Add(new ListItem("Select Material", "0"));
            // LookUp.PopulateEntityDropDown(LookUp.EntityRelationships.Resources);
            lstItemDetails.Enabled = true;
        }
        private void FillWorkSiteDropDown()
        {
            int CompanyID = Convert.ToInt32(Session["CompanyID"].ToString());
            ddlWorkSite.Items.Clear();
            DataSet ds = new DataSet();
            ds = Common.GetDropDownWorkSite(CompanyID);
            ddlWorkSite.DataSource = ds;
            ddlWorkSite.DataTextField = "Name";
            ddlWorkSite.DataValueField = "ID";
            ddlWorkSite.DataBind();
            ddlWorkSite.Items.Insert(0, new ListItem(" Select WorkSite", "0"));
            // LookUp.PopulateEntityDropDown(LookUp.EntityRelationships.WorkSite);
        }
        #endregion FillDropDown

        #endregion SetUpScreen
        #region DataTable

        private DataTable GDNAccPartsDatatable()
        {
            DataTable dtAccParts = new DataTable();
            dtAccParts.Columns.Add("AcID", typeof(System.Int32));
            dtAccParts.Columns.Add("PartName", typeof(System.String));
            dtAccParts.Columns.Add("itemId", typeof(System.Int32));
            dtAccParts.Columns.Add("PoID", typeof(System.Int32));
            return dtAccParts;
        }

        private DataTable SRNItemsDatatable()
        {
            DataTable dtSRNItem = new DataTable();
            dtSRNItem.Columns.Add("PodetID", typeof(System.Int32));
            dtSRNItem.Columns["PodetID"].Unique = true;
            dtSRNItem.Columns.Add("Itemid", typeof(System.Int32));
            dtSRNItem.Columns.Add("POID", typeof(System.Int32));
            dtSRNItem.Columns.Add("PoName", typeof(System.String));
            dtSRNItem.Columns.Add("Item", typeof(System.String));
            dtSRNItem.Columns.Add("Qty", typeof(System.Double));
            dtSRNItem.Columns.Add("RelQty", typeof(System.Double));
            dtSRNItem.Columns.Add("BalQty", typeof(System.String));
            dtSRNItem.Columns.Add("BQty", typeof(System.Double));
            dtSRNItem.Columns.Add("State", typeof(System.Int32));
            dtSRNItem.Columns.Add("Au_Name", typeof(System.String));
            dtSRNItem.Columns.Add("AuID", typeof(System.Int32));
            dtSRNItem.Columns.Add("AuIDs", typeof(System.Int32));
            dtSRNItem.Columns.Add("Unit", typeof(System.String));
            dtSRNItem.Columns.Add("Rate", typeof(System.String));
            dtSRNItem.Columns.Add("SRNItemID", typeof(System.Int32));
            return dtSRNItem;
        }

        #endregion DataTable
        #region AjaxMethod

        [Ajax.AjaxMethod()]
        public string PopulateDDL(string ID)
        {
            string StrRetVal = "";
            try
            {
                var Res = LookUp.PopulateEntityDropDown(LookUp.EntityRelationships.Executive, int.Parse(ID));
                foreach (var item in Res)
                {
                    StrRetVal += item.Name + "@" + item.ID.ToString() + "|";
                }
                return StrRetVal;
            }
            catch
            {
                return StrRetVal;
            }
        }
        [Ajax.AjaxMethod()]
        public string SearchVendor(string strText)
        {
            string strSearch = "";
            DataSet dsMMS_Search = dlSRN.MMS_DDL_SearchVendor(strText);
            if (dsMMS_Search != null && dsMMS_Search.Tables.Count != 0 && dsMMS_Search.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < dsMMS_Search.Tables[0].Rows.Count; i++)
                {
                    strSearch += dsMMS_Search.Tables[0].Rows[i]["Name"].ToString() + "@" + dsMMS_Search.Tables[0].Rows[i]["ID"].ToString() + "|";
                }
            }
            return strSearch;
        }

        [Ajax.AjaxMethod()]
        public string SearchVehicle(string strText)
        {
            string strSearch = "";
            DataSet dsMMS_Search = dlSRN.MMS_DDL_SearchVehicle(strText);
            if (dsMMS_Search != null && dsMMS_Search.Tables.Count != 0 && dsMMS_Search.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < dsMMS_Search.Tables[0].Rows.Count; i++)
                {
                    strSearch += dsMMS_Search.Tables[0].Rows[i]["Name"].ToString() + "@" + dsMMS_Search.Tables[0].Rows[i]["ID"].ToString() + "|";
                }
            }
            return strSearch;
        }
        #endregion AjaxMethod
        #region OnRowCommand
        protected void Grid_OnRowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Edt")
            {
                string SRNID = e.CommandArgument.ToString().Replace("&nbsp;", "");
                using (MMSIDataContext dc = new MMSIDataContext())
                {
                    var AccParts = (from ac in dc.T_MMS_AcmParts
                                    where ac.ResourceID == int.Parse(SRNID) && ac.IsActive == 1
                                    select new { ID = ac.AcId, Name = ac.PartName });

                    ViewState["ResourceID"] = SRNID;
                    chkList.DataSource = AccParts;
                    chkList.DataBind();
                    if (AccParts.Count() > 0)
                    {
                        dvParts.Visible = true;
                    }
                    else
                    {
                        string strScript = "<script language='javascript' type='text/javascript'> alert('AccParts'); </script>";
                        ClientScript.RegisterStartupScript(typeof(Page), "PopupCP", strScript);
                        dvParts.Visible = false;
                    }
                    GridViewRow gvr = (GridViewRow)(((Control)e.CommandSource).NamingContainer);
                    int index = gvr.RowIndex;
                    ViewState["ItemId"] = int.Parse(gvItemDetails.DataKeys[index][1].ToString());
                    ViewState["PoID"] = int.Parse(gvItemDetails.DataKeys[index][2].ToString());
                }
            }
            if (e.CommandName == "Item")
            {
                GridViewRow gvr = (GridViewRow)(((Control)e.CommandSource).NamingContainer);
                int index = gvr.RowIndex;
                ViewState["ItemId"] = int.Parse(gvItemDetails.DataKeys[index][1].ToString());
                ViewState["PoID"] = int.Parse(gvItemDetails.DataKeys[index][2].ToString());
            }
            if (e.CommandName == "Del")
            {
                DataTable dtSRNItem = (DataTable)ViewState["dtSRNItem"];
                GridViewRow gvr = (GridViewRow)(((Control)e.CommandSource).NamingContainer);
                int index = gvr.RowIndex;
                int SRNItemID = int.Parse(gvItemDetails.DataKeys[index][3].ToString());
                if (SRNItemID == 0)
                {
                    int ItemId = int.Parse(gvItemDetails.DataKeys[index][1].ToString());
                    DataRow[] dtRow = dtSRNItem.Select("Itemid='" + ItemId + "'");
                    if (dtRow.Length != 0)
                        dtSRNItem.Rows.Remove(dtRow[0]);
                    GridBind(Convert.ToInt32(ViewState["SRNID"]));
                }
                else
                {
                    // int ItemId = int.Parse(gvItemDetails.DataKeys[index][1].ToString());
                    if (hdFlag.Value == "1")
                        GridBind(Convert.ToInt32(ViewState["SRNID"]));
                    DataRow[] dtRow = dtSRNItem.Select("SRNItemID='" + SRNItemID + "'");
                    if (dtRow.Length != 0)
                        dtSRNItem.Rows.Remove(dtRow[0]);
                    GridBind(Convert.ToInt32(ViewState["SRNID"]));
                }
            }
        }

        protected void Grid_GDNOnRowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Edt")
            {
                string SRNID = e.CommandArgument.ToString().Replace("&nbsp;", "");
                EditSRNDetails(SRNID);
            }
        }

        protected void gvGDNEdit_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if ((e.Row.RowType != DataControlRowType.Header) && (e.Row.RowType == DataControlRowType.DataRow))
            {
                LinkButton lnkBtn = (LinkButton)e.Row.Cells[1].Controls[1];
                e.Row.Cells[6].Attributes.Add("onclick", "javascript:return ChkGDN('" + lnkBtn.CommandArgument + "');");
            }
        }

        protected void Vendor_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillListBoxPO();
            SetUpScreen();
            //if (btnSave.Text.Trim().ToLower().Contains("save"))
            //{
            ddlDestRepre.Items.Clear();
            SqlParameter[] p = new SqlParameter[1];
            p[0] = new SqlParameter("@Site_ID", int.Parse(ddlWorkSite.SelectedValue));
            DataSet ds = new DataSet();
            ds = SQLDBUtil.ExecuteDataset("MMS_DDL_EmployeeExecutive", p);
            ddlDestRepre.DataValueField = ds.Tables[0].Columns[0].ToString();
            ddlDestRepre.DataTextField = ds.Tables[0].Columns[1].ToString();
            ddlDestRepre.Items.Add(new ListItem("Select Destination Representative     ", "-1"));
            ddlDestRepre.DataSource = ds;// LookUp.PopulateEntityDropDown(LookUp.EntityRelationships.EmployeeMaster);
            ddlDestRepre.DataBind();
            //ddlDestRepre.DataSource = //LookUp.PopulateEntityDropDown(LookUp.EntityRelationships.Executive, int.Parse(ddlWorkSite.SelectedValue));

        }

        protected void lstPODetails_IndexChanged(object sender, EventArgs e)
        {
            if (gvItemDetails.Rows.Count > 0)
            {
                AlertMsg.MsgBox(Page, "Multiple WOs can not be delivered together. Delete previous WO Items if other WO items to be delivered!");
                if (ViewState["WOID"] != null)
                {
                    lstPODetails.SelectedValue = ViewState["WOID"].ToString();
                    btnSave.Enabled = true;
                }
            }
            else
            {
                DataSet ds = dlSRN.GetMMS_MIS_lstItemDetails(int.Parse(lstPODetails.SelectedValue), 1);
                if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0)
                {
                    lstItemDetails.DataSource = ds;
                    lstItemDetails.DataValueField = "ID";
                    lstItemDetails.DataTextField = "Name";
                    lstItemDetails.DataBind();
                    lstItemDetails.Enabled = true;
                }
                FillDestinationRepresentativeDropDownByWO();
            }
        }
        #endregion OnRowCommand
        #region MyMethods
        private void SRNItemsInsertAndUpdate(int SRNId)
        {
            try
            {

                DateTime CreatedOn; int CreatedBy, SRNID, PodetID, ResourceID, Au_ID; decimal Qty = 0, ReqQty;
                decimal ItemRate; decimal RcvdQty;

                foreach (GridViewRow gvDetails in gvItemDetails.Rows)
                {
                    int SRNItemID = int.Parse(gvItemDetails.DataKeys[gvDetails.RowIndex][3].ToString());
                    if (btnSave.Text.ToLower().Trim() == "save")
                    {

                        CreatedBy =  Convert.ToInt32(Session["UserId"]);
                        CreatedOn = DateTime.Now;

                        SRNID = SRNId;

                        PodetID = int.Parse(gvItemDetails.DataKeys[gvDetails.RowIndex][0].ToString());
                        ResourceID = int.Parse(gvItemDetails.DataKeys[gvDetails.RowIndex][1].ToString());
                        if (gvDetails.Cells[3].Text != string.Empty)
                            Qty = decimal.Parse(gvDetails.Cells[3].Text);
                        if (gvDetails.Cells[5].Text != string.Empty)
                            ReqQty = decimal.Parse(gvDetails.Cells[5].Text); //Bal Qty
                        Au_ID = int.Parse(((Label)gvDetails.FindControl("AuID")).Text);
                        ItemRate = decimal.Parse(((Label)gvDetails.FindControl("Rate")).Text);
                        RcvdQty = decimal.Parse(((TextBox)gvDetails.FindControl("txtRelQty")).Text);
                        ReqQty = decimal.Parse(((TextBox)gvDetails.FindControl("txtRelQty")).Text);

                        int POID = int.Parse(gvItemDetails.DataKeys[gvDetails.RowIndex][2].ToString());
                        dlSRN.MMS_Ins_Upd_SRNItems(SRNItemID, CreatedBy, SRNID, PodetID, ResourceID, Qty, Au_ID, ItemRate, ReqQty);
                        //lsttvalue.Add(new ListSRNItems(POID.ToString(), gi.ResourceID.ToString(), gi.SRNItemID.ToString()));
                    }
                    else
                    {

                        if (SRNItemID == null)
                        {
                            CreatedBy =  Convert.ToInt32(Session["UserId"]);
                            CreatedOn = DateTime.Now;

                            SRNID = SRNId;
                            PodetID = int.Parse(gvItemDetails.DataKeys[gvDetails.RowIndex][0].ToString());
                            ResourceID = int.Parse(gvItemDetails.DataKeys[gvDetails.RowIndex][1].ToString());
                            if (gvDetails.Cells[3].Text != string.Empty)
                                Qty = decimal.Parse(gvDetails.Cells[3].Text);
                            if (gvDetails.Cells[5].Text != string.Empty)
                                ReqQty = decimal.Parse(gvDetails.Cells[5].Text); //Bal Qty
                            Au_ID = int.Parse(((Label)gvDetails.FindControl("AuID")).Text);

                            ItemRate = decimal.Parse(((Label)gvDetails.FindControl("Rate")).Text);
                            RcvdQty = decimal.Parse(((TextBox)gvDetails.FindControl("txtRelQty")).Text);
                            ReqQty = decimal.Parse(((TextBox)gvDetails.FindControl("txtRelQty")).Text);

                            int POID = int.Parse(gvItemDetails.DataKeys[gvDetails.RowIndex][2].ToString());
                            dlSRN.MMS_Ins_Upd_SRNItems(SRNItemID, CreatedBy, SRNID, PodetID, ResourceID, Qty, Au_ID, ItemRate, ReqQty);
                        }
                        else
                        {
                            RcvdQty = decimal.Parse(((TextBox)gvDetails.FindControl("txtRelQty")).Text);
                            ReqQty = decimal.Parse(((TextBox)gvDetails.FindControl("txtRelQty")).Text);
                            dlSRN.MMS_Upd_SRNItems(SRNItemID, RcvdQty, ReqQty);
                        }
                    }
                }

            }
            catch (Exception e)
            {
                AlertMsg.MsgBox(Page, e.Message.ToString(),AlertMsg.MessageType.Error);
            }
        }
        private void BindGridgvItemDetails(string SRNID)
        {
            DataSet ds = dlSRN.GetMMS_SRN_gvItemDetails(int.Parse(SRNID), 1);
            if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0)
            {
                gvItemDetails.DataSource = ds;
                gvItemDetails.DataBind();
            }
        }

        public string GenerateGDN(string SRNID)
        {
            return Convert.ToDouble(SRNID).ToString("#0000");
            // return "GDN#" + Convert.ToDouble(SRNID).ToString("#0000");
        }

        public string GetAcParts(string ID)
        {
            string strReturn;
            strReturn = String.Format("window.showModalDialog('AccParts.aspx?ID={0}' ,'','dialogWidth:450px; dialogHeight:350px; center:yes');", ID);
            return strReturn;
        }

        public string GetCompliance(string ID)
        {
            string strReturn;
            strReturn = String.Format("window.showModalDialog('GoodsItems.aspx?ID=0&GDN={0}' ,'','dialogWidth:520px; dialogHeight:400px; center:yes');", ID);
            return strReturn;
        }

        //private void EditSRNDetails(string SRNID)
        //{
        //    hdFlag.Value = "1";
        //    DataSet ds = (DataSet)dlSRN.GetMMS_SRN_Grid(int.Parse(SRNID));
        //    if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0)
        //    {
        //        ddlWorkSite.SelectedValue = ds.Tables[0].Rows[0]["WorkSite"].ToString();
        //        BindAllDDLs(Convert.ToInt32(ddlWorkSite.SelectedValue), 0);
        //        if (ds.Tables[0].Rows[0]["VId"].ToString() == "")
        //            ddlVehicle.SelectedValue = "-1";
        //        else
        //            ddlVehicle.SelectedValue = ds.Tables[0].Rows[0]["VId"].ToString();
        //        ddlVendor.SelectedValue = ds.Tables[0].Rows[0]["vendor_id"].ToString();
        //        ddlOrigin.SelectedValue = ds.Tables[0].Rows[0]["Origin"].ToString();
        //        ddlDestination.SelectedValue = ds.Tables[0].Rows[0]["Destination"].ToString();
        //        if (ds.Tables[0].Rows[0]["OriginRepr"].ToString() != "")
        //            ddlOriginRepre.SelectedValue = ds.Tables[0].Rows[0]["OriginRepr"].ToString();
        //        ddlDestRepre.SelectedValue = ds.Tables[0].Rows[0]["DestRepr"].ToString();
        //        if (ds.Tables[0].Rows[0]["TripSheet"].ToString() != "")
        //            txtTripSheet.Text = ds.Tables[0].Rows[0]["TripSheet"].ToString();
        //        txtDate.Text = ds.Tables[0].Rows[0]["StartDate1"].ToString();
        //        int Hr = Convert.ToInt32(ds.Tables[0].Rows[0]["hour"]);
        //        int Min = Convert.ToInt32(ds.Tables[0].Rows[0]["minute"]);
        //        string Meridian = ds.Tables[0].Rows[0]["Meridian"].ToString();
        //        if (Meridian == "AM")
        //            ddlTimeFormat.SelectedValue = "1";

        //        else
        //            ddlTimeFormat.SelectedValue = "2";
        //        if (Hr > 12)
        //        {
        //            Hr = Hr - 12;
        //            ddlstarttime.SelectedValue = Hr.ToString();
        //        }
        //        else if (Hr <= 12)

        //            ddlstarttime.SelectedValue = Hr.ToString();
        //        txtMinutes.Text = Min.ToString();

        //        btnSave.Enabled = true;
        //        btnSave.Text = "Update";

        //        ViewState["SRNID"] = SRNID;
        //        BindGridgvItemDetails(SRNID);
        //        lstItemDetails.Items.Clear();
        //        DataSet dsItemDetails = dlSRN.GetMMS_SRN_EditlstItemDetails(int.Parse(SRNID), 1);
        //        if (dsItemDetails != null && dsItemDetails.Tables.Count != 0 && dsItemDetails.Tables[0].Rows.Count > 0)
        //        {
        //            lstItemDetails.DataSource = dsItemDetails;
        //            lstItemDetails.DataValueField = "ID";
        //            lstItemDetails.DataTextField = "Name";
        //            lstItemDetails.DataBind();
        //            lstItemDetails.SelectedIndex = 0;
        //        }
        //        lstPODetails.Items.Clear();
        //        DataSet dsPODetails = dlSRN.GetMMS_SRN_EditlstPODetails(int.Parse(SRNID), 1);
        //        if (dsPODetails != null && dsPODetails.Tables.Count != 0 && dsPODetails.Tables[0].Rows.Count > 0)
        //        {
        //            lstPODetails.DataSource = dsPODetails;
        //            lstPODetails.DataValueField = "ID";
        //            lstPODetails.DataTextField = "Name";
        //            lstPODetails.DataBind();
        //            lstPODetails.SelectedIndex = 0;
        //        }
        //        if (gvItemDetails.Rows.Count != 0)
        //        {
        //            ddlVendor.Enabled = false;
        //            ddlWorkSite.Enabled = false;
        //        }
        //    }
        //}

        private void EditSRNDetails(string SRNID)
        {
            hdFlag.Value = "1";
            DataSet ds = (DataSet)dlSRN.GetMMS_SRN_Grid(int.Parse(SRNID));
            if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["VId"].ToString() == "")
                    ddlVehicle.SelectedValue = "-1";
                else
                    // ddlVehicle.SelectedValue = ds.Tables[0].Rows[0]["VId"].ToString();
                    if (ddlVehicle.Items.FindByValue(ds.Tables[0].Rows[0]["VId"].ToString()) != null)
                        ddlVehicle.SelectedValue = ds.Tables[0].Rows[0]["VId"].ToString();
                    else
                    {
                        string VId = ds.Tables[0].Rows[0]["VId"].ToString();
                        string VehRegnNr = ds.Tables[0].Rows[0]["VehRegnNr"].ToString();
                        ddlVehicle.Items.Insert(1, new ListItem(VehRegnNr, VId));
                        ddlVehicle.SelectedIndex = 1;
                    }

                //ddlVendor.SelectedValue = ds.Tables[0].Rows[0]["vendor_id"].ToString();

                if (ds.Tables[0].Rows[0]["vendor_id"].ToString() != "")
                {
                    if (ddlVendor.Items.FindByValue(ds.Tables[0].Rows[0]["vendor_id"].ToString()) != null)
                        ddlVendor.SelectedValue = ds.Tables[0].Rows[0]["vendor_id"].ToString();
                    else
                    {
                        string vendor_id = ds.Tables[0].Rows[0]["vendor_id"].ToString();
                        string Vendor_Name = ds.Tables[0].Rows[0]["Vendor_Name"].ToString();

                        ddlVendor.Items.Insert(1, new ListItem(Vendor_Name, vendor_id));
                        ddlVendor.SelectedIndex = 1;
                    }
                }

                //ddlOrigin.SelectedValue = ds.Tables[0].Rows[0]["Origin"].ToString();
                if (ds.Tables[0].Rows[0]["Origin"].ToString() != "")
                {
                    if (ddlOrigin.Items.FindByValue(ds.Tables[0].Rows[0]["Origin"].ToString()) != null)
                        ddlOrigin.SelectedValue = ds.Tables[0].Rows[0]["Origin"].ToString();
                    else
                    {
                        string Origin = ds.Tables[0].Rows[0]["Origin"].ToString();
                        string OriginName = ds.Tables[0].Rows[0]["OriginName"].ToString();
                        ddlOrigin.Items.Insert(1, new ListItem(OriginName, Origin));
                        ddlOrigin.SelectedIndex = 1;
                    }
                }

                //ddlDestination.SelectedValue = ds.Tables[0].Rows[0]["Destination"].ToString();
                if (ds.Tables[0].Rows[0]["Destination"].ToString() != "")
                {
                    if (ddlDestination.Items.FindByValue(ds.Tables[0].Rows[0]["Destination"].ToString()) != null)
                        ddlDestination.SelectedValue = ds.Tables[0].Rows[0]["Destination"].ToString();
                    else
                    {
                        string Destination = ds.Tables[0].Rows[0]["Destination"].ToString();
                        string DestinationName = ds.Tables[0].Rows[0]["DestinationName"].ToString();
                        ddlDestination.Items.Insert(1, new ListItem(DestinationName, Destination));
                        ddlDestination.SelectedIndex = 1;
                    }
                }

                //if (ds.Tables[0].Rows[0]["OriginRepr"].ToString() != "")
                //    ddlOriginRepre.SelectedValue = ds.Tables[0].Rows[0]["OriginRepr"].ToString();
                if (ds.Tables[0].Rows[0]["OriginRepr"].ToString() != "")
                {
                    if (ddlOriginRepre.Items.FindByValue(ds.Tables[0].Rows[0]["OriginRepr"].ToString()) != null)
                        ddlOriginRepre.SelectedValue = ds.Tables[0].Rows[0]["OriginRepr"].ToString();
                    else
                    {
                        string OriginRepr = ds.Tables[0].Rows[0]["OriginRepr"].ToString();
                        string Name = ds.Tables[0].Rows[0]["Name"].ToString();
                        ddlOriginRepre.Items.Insert(1, new ListItem(Name, OriginRepr));
                        ddlOriginRepre.SelectedIndex = 1;
                    }
                }

                //ddlDestRepre.SelectedValue = ds.Tables[0].Rows[0]["DestRepr"].ToString();
                if (ds.Tables[0].Rows[0]["DestRepr"].ToString() != "")
                {
                    if (ddlDestRepre.Items.FindByValue(ds.Tables[0].Rows[0]["DestRepr"].ToString()) != null)
                        ddlDestRepre.SelectedValue = ds.Tables[0].Rows[0]["DestRepr"].ToString();
                    else
                    {
                        string DestRepr = ds.Tables[0].Rows[0]["DestRepr"].ToString();
                        string Name = ds.Tables[0].Rows[0]["DestName"].ToString();
                        ddlDestRepre.Items.Insert(1, new ListItem(Name, DestRepr));
                        ddlDestRepre.SelectedIndex = 1;
                    }
                }

                ddlWorkSite.SelectedValue = ds.Tables[0].Rows[0]["WorkSite"].ToString();
                if (ds.Tables[0].Rows[0]["TripSheet"].ToString() != "")
                    txtTripSheet.Text = ds.Tables[0].Rows[0]["TripSheet"].ToString();

                txtDate.Text = ds.Tables[0].Rows[0]["StartDate1"].ToString();
                int Hr = Convert.ToInt32(ds.Tables[0].Rows[0]["hour"]);
                int Min = Convert.ToInt32(ds.Tables[0].Rows[0]["minute"]);
                string Meridian = ds.Tables[0].Rows[0]["Meridian"].ToString();

                if (Meridian == "AM")
                    ddlTimeFormat.SelectedValue = "1";

                else
                    ddlTimeFormat.SelectedValue = "2";
                if (Hr > 12)
                {
                    Hr = Hr - 12;
                    ddlstarttime.SelectedValue = Hr.ToString();
                }
                else if (Hr <= 12)

                    ddlstarttime.SelectedValue = Hr.ToString();
                txtMinutes.Text = Min.ToString();
                btnSave.Enabled = true;
                btnSave.Text = "Update";

                ViewState["SRNID"] = SRNID;
                BindGridgvItemDetails(SRNID);
                lstItemDetails.Items.Clear();
                DataSet dsItemDetails = dlSRN.GetMMS_SRN_EditlstItemDetails(int.Parse(SRNID), 1);
                if (dsItemDetails != null && dsItemDetails.Tables.Count != 0 && dsItemDetails.Tables[0].Rows.Count > 0)
                {
                    lstItemDetails.DataValueField = "ID";
                    lstItemDetails.DataTextField = "Name";
                    lstItemDetails.DataSource = dsItemDetails;
                    lstItemDetails.DataBind();
                    lstItemDetails.SelectedIndex = 0;
                }
                lstPODetails.Items.Clear();
                DataSet dsPODetails = dlSRN.GetMMS_SRN_EditlstPODetails(int.Parse(SRNID), 1);
                if (dsPODetails != null && dsPODetails.Tables.Count != 0 && dsPODetails.Tables[0].Rows.Count > 0)
                {
                    lstPODetails.DataValueField = "ID";
                    lstPODetails.DataTextField = "Name";
                    lstPODetails.DataSource = dsPODetails;
                    lstPODetails.DataBind();
                    lstPODetails.SelectedIndex = 0;
                }

            }
            if (gvItemDetails.Rows.Count != 0)
            {
                ddlVendor.Enabled = false;
                ddlWorkSite.Enabled = false;
            }
        }
        private void ResetTextboxesRecursive(Control ctrl)
        {
            if (ctrl is TextBox)
                (ctrl as TextBox).Text = string.Empty;
            else
            {
                foreach (Control childControl in ctrl.Controls)
                {
                    this.ResetTextboxesRecursive(childControl);
                }
            }
            if (ctrl is DropDownList)
                (ctrl as DropDownList).SelectedIndex = -1;
            else
            {
                foreach (Control childControl in ctrl.Controls)
                {
                    this.ResetTextboxesRecursive(childControl);
                }
            }
        }
        protected void LoadDropDown()
        {
            txtAllVeh.Text = ""; txtVendor.Text = ""; txtOrigin.Text = ""; txtDest.Text = ""; txtDestRep.Text = ""; txtOriRep.Text = "";
            FIllObject.FillEmptyDropDown(ref ddlVehicle);
            FIllObject.FillEmptyDropDown(ref ddlVendor);
            FIllObject.FillEmptyDropDown(ref ddlOrigin);
            FIllObject.FillEmptyDropDown(ref ddlDestination);
            FIllObject.FillEmptyDropDown(ref ddlDestRepre);
            FIllObject.FillEmptyDropDown(ref ddlOriginRepre);
            WSID = Convert.ToInt32(ddlWorkSite.SelectedValue);
            if (txtNoOfRec.Text != string.Empty && txtNoOfRec.Text != "")
                TopRows = Convert.ToInt32(txtNoOfRec.Text);
            else
                TopRows = 0;
            BindAllDDLs(WSID, TopRows);
        }
        private void BindAllDDLs(int Worksite, int TopRows)
        {
            SqlParameter[] par = new SqlParameter[2];
            par[0] = new SqlParameter("@WSID", Worksite);
            if (TopRows != 0)
                par[1] = new SqlParameter("@TopRows", TopRows);
            else
                par[1] = new SqlParameter("@TopRows", System.Data.SqlDbType.Int);
            DataSet ds = SQLDBUtil.ExecuteDataset("MMS_DDL_SDNsBind", par);
            ddlVehicle.DataSource = ds.Tables[1];
            ddlVehicle.DataBind();
            ddlVendor.DataSource = ds.Tables[5];
            ddlVendor.DataBind();
            ddlOrigin.DataSource = ds.Tables[4];
            ddlOrigin.DataBind();
            ddlDestination.DataSource = ds.Tables[3];
            ddlDestination.DataBind();
            ddlOriginRepre.DataSource = ds.Tables[0];
            ddlOriginRepre.DataBind();
            ddlDestRepre.DataSource = ds.Tables[2];
            ddlDestRepre.DataBind();
            ds.Clear();
        }
        #endregion
        #region OnClick
        protected void btnAddDetails_OnClick(object sender, EventArgs e)
        {
            if (lstItemDetails.SelectedValue == "")
            {
                AlertMsg.MsgBox(Page, "Select goods!");
            }
            else
            {
                try
                {
                    DataTable dtSRNItem = (DataTable)ViewState["dtSRNItem"];
                    if (dtSRNItem == null)
                    {
                        dtSRNItem = SRNItemsDatatable();
                        ViewState["SRNID"] = 0;
                    }
                    DataRow dtRow = null;
                    for (int index = 0; index < lstItemDetails.Items.Count; index++)
                    {
                        if (lstItemDetails.Items[index].Selected)
                        {
                            DataSet ds = (DataSet)dlSRN.GetMMS_GDNPurchaseOrderdetails(int.Parse(lstPODetails.SelectedValue), int.Parse(lstItemDetails.Items[index].Value));
                            if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0)
                            {
                                dtRow = dtSRNItem.NewRow();
                                dtRow["PoName"] = lstPODetails.SelectedItem.Text;
                                dtRow["POID"] = int.Parse(lstPODetails.SelectedValue);
                                ViewState["POID"] = dtRow["POID"];
                                dtRow["Item"] = lstItemDetails.Items[index].Text;
                                dtRow["Qty"] = ds.Tables[0].Rows[0]["Qty"].ToString();
                                dtRow["RelQty"] = ds.Tables[0].Rows[0]["BQty"].ToString();
                                dtRow["PodetID"] = ds.Tables[0].Rows[0]["PodetID"].ToString();
                                dtRow["Itemid"] = ds.Tables[0].Rows[0]["Itemid"].ToString();
                                dtRow["BalQty"] = ds.Tables[0].Rows[0]["BalQty"].ToString();
                                dtRow["State"] = ds.Tables[0].Rows[0]["State"].ToString();
                                dtRow["BQty"] = ds.Tables[0].Rows[0]["BQty"].ToString();
                                dtRow["Au_Name"] = ds.Tables[0].Rows[0]["Au_Name"].ToString();
                                dtRow["AuID"] = ds.Tables[0].Rows[0]["AuID"].ToString();
                                dtRow["AuIDs"] = ds.Tables[0].Rows[0]["AuID"].ToString();
                                dtRow["Rate"] = ds.Tables[0].Rows[0]["Rate"].ToString();
                                dtRow["Unit"] = ds.Tables[0].Rows[0]["Unit"].ToString();
                                dtRow["SRNItemID"] = 0;
                                dtSRNItem.Rows.Add(dtRow);
                                ViewState["WOID"] = lstPODetails.SelectedValue;
                                // ViewState["SRNID"] = 0;
                                // ViewState["ProjectId"] = Offers.ProjectId;

                            }
                        }
                    }
                    //  ViewState["ProjectId"] = objGDN.GetMMS_GDNOffers(int.Parse(lstPODetails.SelectedValue));
                    if (dtSRNItem != null)
                        ViewState["dtSRNItem"] = dtSRNItem;
                    GridBind(Convert.ToInt32(ViewState["SRNID"]));
                    btnSave.Enabled = true;
                }
                catch (Exception ex)
                {
                    if (ex.Message.Contains("constrained to be unique"))
                        AlertMsg.MsgBox(Page, " Already added");
                    else
                        AlertMsg.MsgBox(Page, ex.Message.ToString(),AlertMsg.MessageType.Error);
                }
            }
        }
        protected void btnSave_OnClick(object sender, EventArgs e)
        {

            int CreatedBy; DateTime CreatedOn; int VId = 0; int Vendor_ID = 0; int Origin; int Destination;
            DateTime UpdatedOn; int DestRepr; string TripSheet = null; int WO; int WorkSite; DateTime StartDate;
            int OriginRepr = 0; int UpdatedBy;

            DataTable dtDetId = new DataTable();
            dtDetId.Columns.Add("PoDetId", typeof(System.Int32));
            if (gvItemDetails.Rows.Count == 0)
            {
                AlertMsg.MsgBox(Page, "Please Add goods!!!");
                btnSave.Enabled = false;
            }

            else
            {
                // For PO Closing

                int Empid = Convert.ToInt32( Convert.ToInt32(Session["UserId"]).ToString());
                double BalQty;
                double RcvdQty; string State; int POdetId;
                foreach (GridViewRow gvr in gvItemDetails.Rows)
                {
                    TextBox txtRcvdQty = new TextBox();
                    Label lblBalQty = new Label();
                    lblBalQty = (Label)gvr.FindControl("lblBQty");
                    txtRcvdQty = (TextBox)gvr.FindControl("txtRelQty");
                    RcvdQty = Convert.ToDouble(txtRcvdQty.Text);
                    BalQty = Convert.ToDouble(lblBalQty.Text);
                    Label lblState = new Label();
                    lblState = (Label)gvr.FindControl("lblState");
                    State = lblState.Text;
                    Label lblPodetId = new Label();
                    lblPodetId = (Label)gvr.FindControl("lblPodetId");
                    POdetId = Convert.ToInt32(lblPodetId.Text);

                    if (State != "0")
                    {
                        if (BalQty < RcvdQty)
                        {
                            count = count + 1;
                            string Msg;
                            Msg = gvr.Cells[1].Text;
                            if (count > 1)
                                RejMsg = Msg + "," + Msg;
                            else
                                RejMsg = RejMsg + Msg;
                        }

                        else
                        {
                            if (BalQty == RcvdQty)
                            {
                                Common.EMS_CLOSEPOFromGDN(POdetId, Empid);
                                Ret = Ret + 1;
                                string msg;
                                msg = gvr.Cells[1].Text;
                                if (Ret > 1)
                                    CloseMsg = CloseMsg + "," + msg;
                                else
                                    CloseMsg = CloseMsg + msg;
                            }
                            DataRow drDetId = null;
                            //DataTable dtDetId = (DataTable)ViewState["dtDetId"];
                            drDetId = dtDetId.NewRow();
                            drDetId["PoDetId"] = POdetId.ToString();
                            dtDetId.Rows.Add(drDetId);
                            ViewState["dtDetId"] = dtDetId;
                        }
                    }
                    else
                    {
                        count = count + 1;
                        string Msg;
                        Msg = gvr.Cells[1].Text;
                        if (count > 1)
                            RejMsg = Msg + "," + Msg;
                        else
                            RejMsg = RejMsg + Msg;
                    }
                }

                //END PO Closing
                if (dtDetId.Rows.Count > 0)
                {
                    try
                    {

                        string Smsg = " SRN Created.";

                        bool isNewRecord = btnSave.Text.Trim().ToLower().Contains("save");
                        if (isNewRecord)
                        {

                            SRNId = 0;
                            CreatedBy =  Convert.ToInt32(Session["UserId"]);
                            CreatedOn = DateTime.Now;
                            StartDate = DateTime.Now;

                            if (ddlVehicle.SelectedValue.ToString() != "0")

                                VId = int.Parse(ddlVehicle.SelectedValue);
                            else
                                VId = 0;// null;
                            Vendor_ID = int.Parse(ddlVendor.SelectedValue);
                            Origin = int.Parse(ddlOrigin.SelectedValue);
                            Destination = int.Parse(ddlDestination.SelectedValue);
                            if (ddlOriginRepre.SelectedValue != "0")
                                OriginRepr = int.Parse(ddlOriginRepre.SelectedValue);
                            DestRepr = int.Parse(ddlDestRepre.SelectedValue);

                            if (txtTripSheet.Text != string.Empty)
                                TripSheet = txtTripSheet.Text;

                            WO = Convert.ToInt32(lstPODetails.SelectedValue);
                            WorkSite = Convert.ToInt32(ddlWorkSite.SelectedValue);

                            try
                            {
                                DateTime dtTemp = DateTime.ParseExact(txtDate.Text, "dd/MM/yyyy", CultureInfo.CurrentCulture, DateTimeStyles.AllowWhiteSpaces);
                                // DateTime dtTemp = DateTime.Parse(txtDate.Text);
                                string Date = dtTemp.Day.ToString() + "/" + dtTemp.Month.ToString() + "/" + dtTemp.Year.ToString();
                                dtTemp = dtTemp.AddHours(int.Parse(ddlstarttime.SelectedItem.Text.Trim())).AddMinutes(int.Parse(txtMinutes.Text.Trim()));
                                if (ddlTimeFormat.SelectedItem.Text.Trim() == "PM")
                                    dtTemp = dtTemp.AddHours(12);
                                StartDate = dtTemp;//DateTime.ParseExact(txtDate.Text, "dd/MM/yyyy", CultureInfo.CurrentCulture, DateTimeStyles.AllowWhiteSpaces);
                            }
                            catch
                            {
                            }
                            // srn.StartDate = ddlstarttime.SelectedItem.Text.Trim() + ' ' + txtMinutes.Text.Trim() + ' ' + ddlTimeFormat.SelectedItem.Text.Trim();
                            SRNId = Convert.ToInt32(dlSRN.MMS_InsertSRNs(SRNId, CreatedBy, CreatedOn, VId, Vendor_ID, Origin, Destination, DestRepr, TripSheet, WO, WorkSite, StartDate, OriginRepr, Convert.ToInt32(Application["ModuleId"].ToString())));


                            ViewState["SRNID"] = SRNId;
                            SRNItemsInsertAndUpdate(SRNId);
                        }
                        else
                        {
                            Smsg = " SRN Updated.";
                            SRNId = Convert.ToInt32(ViewState["SRNID"]);
                            StartDate = DateTime.Now;
                            SRNItemsInsertAndUpdate(SRNId);

                            if (ddlVehicle.SelectedValue != string.Empty)
                                VId = int.Parse(ddlVehicle.SelectedValue);
                            btnSave.Text = "Save";
                            try
                            {
                                DateTime dtTemp = DateTime.ParseExact(txtDate.Text, "dd/MM/yyyy", CultureInfo.CurrentCulture, DateTimeStyles.AllowWhiteSpaces);
                                dtTemp = dtTemp.AddHours(int.Parse(ddlstarttime.SelectedItem.Text.Trim())).AddMinutes(int.Parse(txtMinutes.Text.Trim()));
                                if (ddlTimeFormat.SelectedItem.Text.Trim() == "PM")
                                    dtTemp = dtTemp.AddHours(12);
                                StartDate = dtTemp;//DateTime.ParseExact(txtDate.Text, "dd/MM/yyyy", CultureInfo.CurrentCulture, DateTimeStyles.AllowWhiteSpaces);
                            }
                            catch
                            {
                            }

                            CreatedBy =  Convert.ToInt32(Session["UserId"]);
                            CreatedOn = DateTime.Now;
                            if (ddlVendor.SelectedValue != "0")
                                Vendor_ID = int.Parse(ddlVendor.SelectedValue);
                            Origin = int.Parse(ddlOrigin.SelectedValue);
                            Destination = int.Parse(ddlDestination.SelectedValue);
                            if (ddlOriginRepre.SelectedValue != "0")
                                OriginRepr = int.Parse(ddlOriginRepre.SelectedValue);
                            DestRepr = int.Parse(ddlDestRepre.SelectedValue);
                            WO = Convert.ToInt32(lstPODetails.SelectedValue);
                            WorkSite = Convert.ToInt32(ddlWorkSite.SelectedValue);
                            if (txtTripSheet.Text != string.Empty)
                                TripSheet = txtTripSheet.Text;
                            SRNId = Convert.ToInt32(dlSRN.MMS_InsertSRNs(SRNId, CreatedBy, CreatedOn, VId, Vendor_ID, Origin, Destination, DestRepr, TripSheet, WO, WorkSite, StartDate, OriginRepr, Convert.ToInt32(Application["ModuleId"].ToString())));
                            lstItemDetails.Items.Clear();

                        }
                        DataTable dtSRNItem = (DataTable)ViewState["dtSRNItem"];
                        ViewState["dtSRNItem"] = null;

                        gvItemDetails.DataSource = "";
                        gvItemDetails.DataBind();

                        //AlertMsg.MsgBox(Page, "#" + SRNId + Smsg);
                        //if (!isNewRecord)
                        //    Response.Redirect("SRNStatus.aspx");


                        if (Ret == 0 && count == 0)
                        {
                            AlertMsg.MsgBox(Page, SRNId + " PSDN " + message);
                        }
                        else if (Ret == 0 && count != 0)
                        {
                            //if (RejMsg == "")
                            //{ RejMsg = count.ToString()+" item(s)"; }
                            AlertMsg.MsgBox(Page, RejMsg + " Qty(s) Exceeded the total item(s) Qty! So these item(s) were not added in PSDN!   PSDN# " + SRNId + " done with remaining items(s)! ");
                        }
                        else if (Ret != 0 && count == 0)
                        {
                            AlertMsg.MsgBox(Page, CloseMsg + " Qty(s) reached the total item(s) Qty, these  item(s) were closed!  PSDN# " + SRNId + " done!. ");
                            FillListBoxPO();
                            lstItemDetails.Items.Clear();
                        }
                        else if (Ret != 0 && count != 0)
                        {
                            AlertMsg.MsgBox(Page, RejMsg + " Qty(s)  exceeded the total item(s) Qty! So these items were not added in PSDN! " + CloseMsg + " Qty(s) reached the total item(s) Qty, these  item(s) were closed!   PSDN# " + SRNId + " done! ");

                        }

                    }
                    catch (Exception ex)
                    {
                        AlertMsg.MsgBox(Page, ex.Message.ToString(),AlertMsg.MessageType.Error);
                    }
                    ddlVendor.Enabled = true;
                }
                else
                {
                    { AlertMsg.MsgBox(Page, " PSDN creation terminated! For  Given  item(s) dispatch quantity exceeds balance quantity due to be supplied of the PO quantity. Either amend the PO quantity or edit the dispacth quantity to proceed."); }
                }
            }
        }
        protected void btnSubmit_OnClick(object sender, EventArgs e)
        {
            DataTable dtAccParts = (DataTable)ViewState["dtAccParts"];
            if (dtAccParts == null)
                dtAccParts = GDNAccPartsDatatable();
            for (int index = 0; index < chkList.Items.Count; index++)
            {
                if (chkList.Items[index].Selected)
                {
                    DataRow dtRow = null;
                    dtRow = dtAccParts.NewRow();
                    dtRow["AcID"] = chkList.Items[index].Value;
                    dtRow["PartName"] = chkList.Items[index].Text;
                    dtRow["ItemId"] = Convert.ToInt32(ViewState["ItemId"]);
                    dtRow["PoID"] = Convert.ToInt32(ViewState["PoID"]);
                    dtAccParts.Rows.Add(dtRow);
                    dvParts.Visible = false;
                }
            }
            if (dtAccParts != null)
                ViewState["dtAccParts"] = dtAccParts;
        }
        protected void btnGetPO_Click(object sender, EventArgs e)
        {
            FillListBoxPO();
            lstItemDetails.Items.Clear();
        }
        protected void BtnGo_Click(object sender, EventArgs e)
        {
            LoadDropDown();
        }
        protected void btnAllVeh_Click(object sender, EventArgs e)
        {
            FillVehicleNameDropDown();
        }
        protected void lnkAllVendors_Click(object sender, EventArgs e)
        {
            FillVendorDropDown();
        }
        protected void lnkAllOrigin_Click(object sender, EventArgs e)
        {
            FillOriginDropDown();
        }
        protected void btnDest_Click(object sender, EventArgs e)
        {
            FillDestinationDropDown();
        }
        protected void btnOriRep_Click(object sender, EventArgs e)
        {
            FillOriginRepresentativeDropDown();
        }
        protected void btnDestRep_Click(object sender, EventArgs e)
        {
            FillDestinationRepresentativeDropDown();
        }
        protected void btnViewPO_Click(object sender, EventArgs e)
        {

        }
        #endregion
        protected void ddlVendor_SelectedIndexChanged(object sender, EventArgs e)
        {
            int SiteID = Convert.ToInt32(ddlWorkSite.SelectedItem.Value);
            FillOriginRepresentativeDropDown();
            FillDestinationRepresentativeDropDown();
            FillListBoxPO();
        }
        protected void btnback_Click(object sender, EventArgs e)
        {
            Response.Redirect("SRNStatus.aspx");
        }
        protected void ddlWorkSite_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadDropDown();

        }
    }

}