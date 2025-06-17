using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Globalization;
using DataAccessLayer;
using AECLOGIC.HMS.BLL;
using Aeclogic.Common.DAL;
namespace AECLOGIC.ERP.HMS
{
    public partial class SRN : AECLOGIC.ERP.COMMON.WebFormMaster
    {
        #region LoadEvents
        int mid = 0, count, Ret = 0;
        string menuname, menuid, CloseMsg = "", RejMsg = "", message = "", Msg = "";
        bool viewall;
        // DataAccessLayer.dlGdn objGDN = new DataAccessLayer.dlGdn();
        Common objCommon = new Common();
        dlSRN objSRN = new dlSRN();
        int WSID; int TopRows;
        protected override void OnInit(EventArgs e)
        {
            ModuleID = 1;
            base.OnInit(e);
        
            
        }

        protected void Page_Load(object sender, EventArgs e)
        {
          
            if (!IsPostBack)
            {
                GetParentMenuId();
                FIllObject.FillEmptyDropDown(ref ddlDestination);
                FIllObject.FillEmptyDropDown(ref ddlDestRepre);
                Ajax.Utility.RegisterTypeForAjax(typeof(SRN));
                SetUpScreen(); ddlWorkSite.SelectedValue = "1"; FillVendorDropDown(); // BindDepartments();
                if (Request.QueryString["ID"] != null && Request.QueryString["ID"] != string.Empty)
                {
                    int ReqType = 0;
                    int SRNID = Convert.ToInt32(Request.QueryString["ID"].ToString());
                    if (Request.QueryString.Count == 2)
                    {
                        ReqType = Convert.ToInt32(Request.QueryString[1].ToString());
                    }
                    if (ReqType != 2)
                    {
                        EditSRNDetails(SRNID.ToString());
                    }
                    if (ReqType == 1 || ReqType == 2)
                    {
                        trRemNote.Visible = true;
                        tdReminder.Visible = true;
                        chkReminder.Checked = true;
                        DataSet dsRID = dlSRN.HR_GetReminderBySRND(SRNID);
                        if (dsRID.Tables.Count == 1)
                            if (dsRID.Tables[0].Rows.Count > 0)
                            {
                                chkReminder.Checked = true;
                                trRemNote.Visible = true;
                                tdReminder.Visible = true;
                                txtValidFrom.Text = dsRID.Tables[0].Rows[0]["ValidUpto"].ToString();
                                txtRemindDays.Text = dsRID.Tables[0].Rows[0]["RemindStart"].ToString();
                                if (ReqType == 1)
                                    txtReminder.Text = "Regularised WO w.r.t Old WO:" + dsRID.Tables[0].Rows[0]["WONO"].ToString() + " " + dsRID.Tables[0].Rows[0]["RemindText"].ToString();
                                else
                                    txtReminder.Text = "Re-Created WO w.r.t Old WO:" + dsRID.Tables[0].Rows[0]["WONO"].ToString() + " " + dsRID.Tables[0].Rows[0]["RemindText"].ToString();
                            }
                        try { FillListBoxPO(); }
                        catch { }
                    }

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
                            dtRow["Rate"] = ds.Tables[0].Rows[i]["Rate"].ToString();
                            dtRow["Unit"] = ds.Tables[0].Rows[i]["Unit"].ToString();
                            dtRow["SRNItemID"] = ds.Tables[0].Rows[i]["SRNItemID"].ToString();
                            dtSRN.Rows.Add(dtRow);
                        }
                    }
                    hdFlag.Value = "0";
                }
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
             
          DataSet  ds = AttendanceDAC.GetAllowed(RoleId, ModuleId, URL);
            int MenuId = 0;
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                MenuId = Convert.ToInt32(ds.Tables[0].Rows[0]["Under"]);
               
                gvItemDetails.Columns[2].Visible = (bool)ds.Tables[0].Rows[0]["Editable"];

               
                btnGetPO.Enabled = (bool)ds.Tables[0].Rows[0]["Editable"];
                btnViewPO.Enabled = (bool)ds.Tables[0].Rows[0]["Editable"];
                btnAddDetails.Enabled = (bool)ds.Tables[0].Rows[0]["Editable"];
                btnSave.Enabled = (bool)ds.Tables[0].Rows[0]["Editable"];
            }
            return MenuId;
        }
        #region FillDropDown
      
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
           

        }

        
        private void FillListBoxPO()
        {
            lstPODetails.Items.Clear();
            lstPODetails.Items.Add(new ListItem("No WOrk Order Orders Found", "0"));
            // modify by pratap dt: 22-dec-2017
            //DataSet ds = dlSRN.GetMMS_MIS_lstWODetails(int.Parse(ddlVendor.SelectedValue), int.Parse(ddlWorkSite.SelectedValue), 1, 
            //    Convert.ToInt32(Application["ModuleId"].ToString()), Convert.ToInt32(Session["CompanyID"].ToString()));
            DataSet ds = dlSRN.GetMMS_MIS_lstWODetails(int.Parse(ddlVendor.SelectedValue), int.Parse(ddlWorkSite.SelectedValue), 1,
                ModuleID, CompanyID);
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

            DataSet ds = SQLDBUtil.ExecuteDataset("MMS_DDL_Resources");
            lstItemDetails.DataSource = ds;
            lstItemDetails.DataTextField = "Name";
            lstItemDetails.DataValueField = "ID";
            lstItemDetails.DataBind();
            lstItemDetails.Items.Add(new ListItem("Select Material", "0"));
             
            lstItemDetails.Enabled = true;
        }
        private void FillWorkSiteDropDown()
        {
            ddlWorkSite.Items.Clear();
             
            AttendanceDAC ADAC = new AttendanceDAC();
            DataSet ds = AttendanceDAC.GetWorkSite(0, '1', Convert.ToInt32(Session["CompanyID"]));
            ddlWorkSite.DataSource = ds;
            ddlWorkSite.DataTextField = "Site_Name";
            ddlWorkSite.DataValueField = "Site_ID";
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
                //var Res = LookUp.PopulateEntityDropDown(LookUp.EntityRelationships.Executive, int.Parse(ID));

                DataSet ds = SQLDBUtil.ExecuteDataset("MMS_DDL_EmployeeExecutive");
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    StrRetVal += ds.Tables[0].Rows[0]["Name"].ToString() + "@" + ds.Tables[0].Rows[0]["ID"].ToString().ToString() + "|";
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
                DataSet ds = AttendanceDAC.HR_getPartName(Convert.ToInt32(SRNID));
                ViewState["ResourceID"] = SRNID;
                if (ds.Tables.Count > 0)
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
        //By ravitheja for On change fo  ExcQty texbox validation alert
        protected void QtyChanged(object sender, EventArgs e)
        {

            TextBox txtRelQty = (TextBox)sender;
            int exeQty = Convert.ToInt32(txtRelQty.Text!=string.Empty?txtRelQty.Text:"0");
            GridViewRow gvr = (GridViewRow)txtRelQty.NamingContainer;
            int rowindex1 = gvr.RowIndex;
            int balqty = Convert.ToInt32(gvItemDetails.Rows[rowindex1].Cells[4].Text != string.Empty ? gvItemDetails.Rows[rowindex1].Cells[3].Text : "0");
            if (exeQty> balqty)
             {
                AlertMsg.MsgBox(Page, "Check Exe Qty");
            }            
        }
        protected void Vendor_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillListBoxPO();
            SetUpScreen();
            
        }

        protected void lstPODetails_IndexChanged(object sender, EventArgs e)
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
            if (gvItemDetails.Rows.Count > 0)
            {
                AlertMsg.MsgBox(Page, "Delete previous Items before Changing the PO!");
            }
        }
        #endregion OnRowCommand
        #region MyMethods
        private void SRNItemsInsertAndUpdate(int SRNId)
        {
            try
            {

                DateTime CreatedOn; int CreatedBy; int SRNID; int PodetID; int ResourceID; decimal Qty = 0; decimal ReqQty; int Au_ID;
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

        private void EditSRNDetails(string SRNID)
        {
            hdFlag.Value = "1";
            DataSet ds = (DataSet)dlSRN.GetMMS_SRN_Grid(int.Parse(SRNID));
            if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0)
            {
                ddlWorkSite.SelectedValue = ds.Tables[0].Rows[0]["WorkSite"].ToString();
                BindAllDDLs(Convert.ToInt32(ddlWorkSite.SelectedValue), 0);
                ddlVendor.SelectedValue = ds.Tables[0].Rows[0]["vendor_id"].ToString();
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
                    lstItemDetails.DataSource = dsItemDetails;
                    lstItemDetails.DataValueField = "ID";
                    lstItemDetails.DataTextField = "Name";
                    lstItemDetails.DataBind();
                    lstItemDetails.SelectedIndex = 0;
                }
                lstPODetails.Items.Clear();
                DataSet dsPODetails = dlSRN.GetMMS_SRN_EditlstPODetails(int.Parse(SRNID), 1);
                if (dsPODetails != null && dsPODetails.Tables.Count != 0 && dsPODetails.Tables[0].Rows.Count > 0)
                {
                    lstPODetails.DataSource = dsPODetails;
                    lstPODetails.DataValueField = "ID";
                    lstPODetails.DataTextField = "Name";
                    lstPODetails.DataBind();
                    lstPODetails.SelectedIndex = 0;
                }
                if (gvItemDetails.Rows.Count != 0)
                {
                    ddlVendor.Enabled = false;
                    ddlWorkSite.Enabled = false;
                }
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
       
        private void BindAllDDLs(int Worksite, int TopRows)
        {
            SqlParameter[] par = new SqlParameter[2];
            par[0] = new SqlParameter("@WSID", Worksite);
            if (TopRows != 0)
                par[1] = new SqlParameter("@TopRows", TopRows);
            else
                par[1] = new SqlParameter("@TopRows", System.Data.SqlDbType.Int);
            DataSet ds = SQLDBUtil.ExecuteDataset("MMS_DDL_SDNsBind", par);
            ddlVendor.DataSource = ds.Tables[5];
            ddlVendor.DataBind();
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
                            //By arvitheja for changinf Get bal qty
                            DataSet ds = (DataSet)dlSRN.GetMMS_HMS_GDNPurchaseOrderdetails(int.Parse(lstPODetails.SelectedValue), int.Parse(lstItemDetails.Items[index].Value));
                            if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0)
                            {
                                dtRow = dtSRNItem.NewRow();
                                dtRow["PoName"] = lstPODetails.SelectedItem.Text;
                                dtRow["POID"] = int.Parse(lstPODetails.SelectedValue);
                                ViewState["POID"] = dtRow["POID"];
                                dtRow["Item"] = lstItemDetails.Items[index].Text;
                                dtRow["Qty"] = ds.Tables[0].Rows[0]["Qty"].ToString();
                                dtRow["RelQty"] = ds.Tables[0].Rows[0]["BalQty"].ToString();
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

                            }
                        }
                    }
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
            try
            {
                int SRNId; int CreatedBy; DateTime CreatedOn; int VId; int Vendor_ID;
                string TripSheet = null; int WO; int WorkSite; DateTime StartDate;
                int OriginRepr = 0;

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
                 double UpdateBalqty=0;
                    int updatePOdetId=0;
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
                     updatePOdetId=POdetId;
                        if (State != "0")
                        {
                            if (BalQty < RcvdQty)
                            {
                                AlertMsg.MsgBox(Page,"Exe.Qty exceeding Balance Qty");
                                
                                return;
                               
                            }

                            else
                            {
                                if (BalQty == RcvdQty)
                                {
                                    Common.EMS_CLOSEPOFromGDN(POdetId, Empid);
                                    Ret = Ret + 1;
                                    Msg = "";

                                    Msg = gvr.Cells[1].Text;
                                    if (Ret > 1)
                                        CloseMsg = CloseMsg + "," + Msg;
                                    else
                                        CloseMsg = CloseMsg + Msg;
                                }
                                else  if (BalQty > RcvdQty)
                                {
                                UpdateBalqty = BalQty - RcvdQty;
                                    
                                   
                                    Ret = Ret + 1;
                                    Msg = "";

                                    Msg = gvr.Cells[1].Text;
                                    if (Ret > 1)
                                        CloseMsg = CloseMsg + "," + Msg;
                                    else
                                        CloseMsg = CloseMsg + Msg;

                                }
                                DataRow drDetId = null;
                                drDetId = dtDetId.NewRow();
                                drDetId["PoDetId"] = POdetId.ToString();
                                dtDetId.Rows.Add(drDetId);
                                ViewState["dtDetId"] = dtDetId;
                            }
                        }
                        else
                        {
                            
                            count = count + 1;
                            Msg = "";
                            Msg = gvr.Cells[1].Text;
                            if (count > 1)
                                RejMsg = Msg + "," + Msg;
                            else
                                RejMsg = RejMsg + Msg;
                        }
                    }

                    //END PO Closing


                    Msg = " SRN Created.";
                    bool isNewRecord = btnSave.Text.Trim().ToLower().Contains("save");
                    if (isNewRecord)
                    {

                        SRNId = 0;
                        CreatedBy =  Convert.ToInt32(Session["UserId"]);
                        CreatedOn = DateTime.Now;
                        StartDate = DateTime.Now;


                        VId = 0;// null;
                        Vendor_ID = int.Parse(ddlVendor.SelectedValue);

                        WO = Convert.ToInt32(lstPODetails.SelectedValue);
                        WorkSite = Convert.ToInt32(ddlWorkSite.SelectedValue);

                        try
                        {
                            DateTime dtTemp = DateTime.ParseExact(txtDate.Text, "dd/MM/yyyy", CultureInfo.CurrentCulture, DateTimeStyles.AllowWhiteSpaces);
                            string Date = dtTemp.Day.ToString() + "/" + dtTemp.Month.ToString() + "/" + dtTemp.Year.ToString();
                            dtTemp = dtTemp.AddHours(int.Parse(ddlstarttime.SelectedItem.Text.Trim())).AddMinutes(int.Parse(txtMinutes.Text.Trim()));
                            if (ddlTimeFormat.SelectedItem.Text.Trim() == "PM")
                                dtTemp = dtTemp.AddHours(12);
                            StartDate = dtTemp;//DateTime.ParseExact(txtDate.Text, "dd/MM/yyyy", CultureInfo.CurrentCulture, DateTimeStyles.AllowWhiteSpaces);
                        }
                        catch
                        {
                        }
                        SRNId = Convert.ToInt32(dlSRN.MMS_InsertSRNs(SRNId, CreatedBy, CreatedOn, VId, Vendor_ID, 0, Convert.ToInt32(ddlDestination.SelectedValue),Convert.ToInt32(ddlDestRepre.SelectedValue),
                            TripSheet, WO, WorkSite, StartDate, OriginRepr, ModuleID));
                        int id = Convert.ToInt32(dlSRN.MMS_UpdateBalqtyonPurchaseOrderdetails(updatePOdetId,UpdateBalqty));
                        ViewState["SRNID"] = SRNId;
                        SRNItemsInsertAndUpdate(SRNId);
                        int RID = 0;
                        DateTime? ValidFrom = null;
                        if (txtValidFrom.Text.Trim() != null)
                            ValidFrom = CODEUtility.ConvertToDate(txtValidFrom.Text.Trim(), DateFormat.DayMonthYear);

                        DateTime? ValidUpto = null;
                        if (txtValidUpto.Text.Trim() != null)
                            ValidUpto = CODEUtility.ConvertToDate(txtValidUpto.Text.Trim(), DateFormat.DayMonthYear);

                        DateTime? DueDate = null;
                        if (txtDueDate.Text.Trim() != null)
                            DueDate = CODEUtility.ConvertToDate(txtDueDate.Text.Trim(), DateFormat.DayMonthYear);

                        int RemindDays = Convert.ToInt32(txtRemindDays.Text);

                        if (chkReminder.Checked)
                        {
                            dlSRN.HR_InsUpReminder(RID, SRNId, ValidFrom, ValidUpto, RemindDays, txtReminder.Text, WO, ModuleID, DueDate);
                        }
                    }
                    else
                    {
                        Msg = " SRN Updated.";
                        SRNId = Convert.ToInt32(ViewState["SRNID"]);

                        SRNItemsInsertAndUpdate(SRNId);

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

                        WorkSite = Convert.ToInt32(ddlWorkSite.SelectedValue);
                        lstItemDetails.Items.Clear();
                    }
                    DataTable dtSRNItem = (DataTable)ViewState["dtSRNItem"];
                    ViewState["dtSRNItem"] = null;

                    gvItemDetails.DataSource = "";
                    gvItemDetails.DataBind();

                    AlertMsg.MsgBox(Page, "#" + SRNId + Msg);
                    if (!isNewRecord)
                        Response.Redirect("SRNStatus.aspx");
                }
            }

            catch (Exception ex)
            {

                AlertMsg.MsgBox(Page, ex.Message.ToString(),AlertMsg.MessageType.Error);
            }
            ddlVendor.Enabled = true;
            ddlWorkSite.Enabled = true; 
            FillListBoxPO();
            lstItemDetails.Items.Clear();
        }
        public string PONavigateUrl(string POID)
        {
            bool Fals = false;
            return "javascript:return window.open('ProPurchaseOrderPrint.aspx?id=" + POID + "&PON=" + 1 + "&tot=" + Fals + "' , '_blank')";
        }
         
        protected void btnGetPO_Click(object sender, EventArgs e)
        {
            FillListBoxPO();
            lstItemDetails.Items.Clear();
        }
        
        
        protected void lnkAllVendors_Click(object sender, EventArgs e)
        {
            FillVendorDropDown();
        }
        
        #endregion

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
        }


        protected void ddlVendor_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            FillListBoxPO();

        }

        public void Clear()
        {
            FillListBoxPO();
            lstPODetails.Items.Clear();
            lstItemDetails.Items.Clear();

        }


        protected void btnback_Click(object sender, EventArgs e)
        {
            Response.Redirect("SRNStatus.aspx");
        }
        protected void chkReminder_CheckedChanged(object sender, EventArgs e)
        {
            if (chkReminder.Checked)
            {
                tdReminder.Visible = true; trRemNote.Visible = true;
            }
            else
            {
                tdReminder.Visible = false; trRemNote.Visible = false;
            }
        }
      

        protected void btnDest_Click1(object sender, EventArgs e)
        {
            WSID = Convert.ToInt32(ddlWorkSite.SelectedValue);
            SqlParameter[] par = new SqlParameter[2];
            par[0] = new SqlParameter("@WSID", WSID);
            if (txtDest.Text != string.Empty && txtDest.Text != "")
                par[1] = new SqlParameter("@Filter", txtDest.Text);
            else
                par[1] = new SqlParameter("@Filter", System.Data.SqlDbType.Int);
            FIllObject.FillDropDown(ref ddlDestination, "MMS_DDL_SRNDestinationsByWS", par);
        }

        protected void btnDestRep_Click1(object sender, EventArgs e)
        {
            FillDestinationRepresentativeDropDown();
        }

      

    }

       
    #region ListSRNItems
    public class ListSRNItems
    {
        string _PO = "";
        string _Item = "";
        string _srnItem = "";
        public ListSRNItems(string PO, string Item, string GDNItem)
        {
            _PO = PO;
            _Item = Item;
            _srnItem = SRNItem;
        }
        public string PONo
        {
            set
            {
                _PO = value;
            }
            get
            {
                return _PO;
            }
        }
        public string ItemDetails
        {
            set
            {
                _Item = value;
            }
            get
            {
                return _Item;
            }
        }
        public string SRNItem
        {
            set
            {
                _srnItem = value;
            }
            get
            {
                return _srnItem;
            }
        }
    }
    #endregion ListSRNItems
}


//sp_helptext cartesian product
