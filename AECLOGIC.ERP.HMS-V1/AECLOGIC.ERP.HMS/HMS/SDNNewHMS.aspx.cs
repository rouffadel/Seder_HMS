using System;
using System.Collections;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using DataAccessLayer;
using System.Data;
using AECLOGIC.ERP.COMMON;
using AECLOGIC.HMS.BLL;
using Aeclogic.Common.DAL;
using System.Collections.Generic;
using System.Globalization;
using System.Data.SqlClient;
using AECLOGIC.ERP.HMS;
using DataAccessLayer;


namespace AECLOGIC.ERP.HMS
{
    public partial class SDNNewHMS : AECLOGIC.ERP.COMMON.WebFormMaster
    {
        #region LoadEvents
        int mid = 0; string menuname; string menuid;
        DataAccessLayer.GDN objGDN = new DataAccessLayer.GDN();
        Common1 obj = new Common1();
       
        SRNService obj1 = new SRNService();

        int WSID; int TopRows; string CloseMsg = ""; string RejMsg = ""; string message = ""; int count; int Ret = 0;

        protected override void OnInit(EventArgs e)
        {
            ModuleID = 1;
            base.OnInit(e);

        }

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {

                SetUpScreen();
                if (Request.QueryString["ID"] != null && Request.QueryString["ID"] != string.Empty)
                {
                    int SRNID = Convert.ToInt32(Request.QueryString["ID"].ToString());
                    EditSRNDetails(SRNID.ToString());
                    lblSRNID.Visible = true;
                    lblSRNID.Text = "(SDN NO: " + SRNID.ToString() + " )";
                    btnBack.Visible = true;

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
                            dtRow["BQty"] = ds.Tables[0].Rows[i]["BQty"].ToString();
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
                gvItemDetails.DataSource = dtSRN;
                gvItemDetails.DataBind();
                if (gvItemDetails.Rows.Count == 0)
                {
                    ddlVendor.Enabled = true;
                    ddlWorkSite.Enabled = true;
                    btnSave.Enabled = false;
                }
                else
                {
                    ddlVendor.Enabled = false;
                    ddlWorkSite.Enabled = false;
                    btnSave.Enabled = true;
                }
            }
        }

        private void SetUpScreen()
        {
          
            FillWorkSiteDropDown();
            LoadDropDown();
        }

        #endregion SetUpScreen

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
        }

        private void FillListBoxPO()
        {
            int ModuleId = Convert.ToInt32(ConfigurationManager.AppSettings["ModuleId"]);

            lstPODetails.Items.Clear();
            lstPODetails.Items.Add(new ListItem("No WOrk Order Orders Found", "-1"));
            DataSet ds = dlSRN.GetMMS_MIS_lstWODetails(int.Parse(ddlVendor.SelectedValue), int.Parse(ddlWorkSite.SelectedValue), 1, ModuleId, Convert.ToInt32(Session["CompanyID"]));
            if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0)
            {
                lstPODetails.DataValueField = "ID";
                lstPODetails.DataTextField = "Name";
                lstPODetails.DataSource = ds;
                lstPODetails.DataBind();
                lstPODetails.Enabled = true;
            }
        }

        private void FillListBoxMaterial()
        {
            lstItemDetails.Items.Add(new ListItem("Select Material", "-1"));
            lstItemDetails.DataBind();
            lstItemDetails.Enabled = true;
        }

        private void FillWorkSiteDropDown()
        {
            int EmpID = Convert.ToInt32( Convert.ToInt32(Session["UserId"]).ToString());
            int CompanyID = Convert.ToInt32(Session["CompanyID"].ToString());
            SqlParameter[] par = new SqlParameter[2];
            par[0] = new SqlParameter("@CompanyID", CompanyID);
            par[1] = new SqlParameter("@EMPID", EmpID);
            FIllObject.FillDropDown(ref ddlWorkSite, "MMS_DDL_WorkSite", par);

        }

        #endregion FillDropDown

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
            dtSRNItem.Columns.Add("AuIDs", typeof(System.String));
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
                    AlertMsg.MsgBox(Page, "Deleted");
                    GridBind(Convert.ToInt32(ViewState["SRNID"]));
                }
                else
                {
                    if (hdFlag.Value == "1")
                        GridBind(Convert.ToInt32(ViewState["SRNID"]));
                    DataRow[] dtRow = dtSRNItem.Select("SRNItemID='" + SRNItemID + "'");
                    if (dtRow.Length != 0)
                        dtSRNItem.Rows.Remove(dtRow[0]);
                    AlertMsg.MsgBox(Page, "Deleted");
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
           
        }

        protected void lstPODetails_IndexChanged(object sender, EventArgs e)
        {
            DataSet ds = dlSRN.GetMMS_MIS_lstItemDetails(int.Parse(lstPODetails.SelectedValue), 1);
            if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0)
            {
                lstItemDetails.DataValueField = "ID";
                lstItemDetails.DataTextField = "Name";
                lstItemDetails.DataSource = ds;
                lstItemDetails.DataBind();
                lstItemDetails.Enabled = true;
            }
            FillDestinationRepresentativeDropDownByWO();

            if (gvItemDetails.Rows.Count > 0)
            {
                AlertMsg.MsgBox(Page, "Delete previous Items before Changing the PO!");
            }
        }

        #endregion 

        #region MyMethods

        private void SRNItemsInsertAndUpdate(int scopeIdentity)
        {
            try
            {
                using (MMSIDataContext dc = new MMSIDataContext())
                {
                    T_MMS_SRNItem gi;
                    var SRNItems = (from gis in dc.T_MMS_SRNItems
                                    join g in dc.T_MMS_SRNs on gis.SRNID equals g.SRNID
                                    where gis.SRNID == scopeIdentity
                                    select gis);
                    foreach (T_MMS_SRNItem git in SRNItems)
                    {
                        git.IsActive = 0;
                        dc.SubmitChanges();
                    }
                    List<ListSRNItems> lsttvalue = new List<ListSRNItems>();
                    DataTable dt = (DataTable)ViewState["dtDetId"];
                    foreach (GridViewRow gvDetails in gvItemDetails.Rows)
                    {


                        int SRNItemID = int.Parse(gvItemDetails.DataKeys[gvDetails.RowIndex][3].ToString());

                        string id = gvItemDetails.DataKeys[gvDetails.RowIndex][0].ToString();
                        DataRow[] drArray = dt.Select("PodetId='" + id + "'");

                        if (drArray.Length > 0)
                        {
                            if (btnSave.Text.ToLower().Trim() == "save")
                            {
                                gi = new T_MMS_SRNItem();
                                gi.CreatedBy =  Convert.ToInt32(Session["UserId"]);
                                gi.CreatedOn = DateTime.Now;
                                gi.IsActive = 1;
                                gi.SRNID = scopeIdentity;
                                gi.PodetID = int.Parse(gvItemDetails.DataKeys[gvDetails.RowIndex][0].ToString());
                                gi.ResourceID = int.Parse(gvItemDetails.DataKeys[gvDetails.RowIndex][1].ToString());
                                if (gvDetails.Cells[3].Text != string.Empty)
                                    gi.Qty = decimal.Parse(gvDetails.Cells[3].Text);
                                if (gvDetails.Cells[5].Text != string.Empty)
                                    gi.ReqQty = decimal.Parse(gvDetails.Cells[5].Text); //Bal Qty
                                gi.Au_ID = int.Parse(((Label)gvDetails.FindControl("AuID")).Text);

                                if (((Label)gvDetails.FindControl("AuIDs")).Text != "")
                                {
                                    gi.Au_IDS = int.Parse(((Label)gvDetails.FindControl("AuIDs")).Text);
                                }
                                else
                                {
                                    gi.Au_IDS = null;
                                }

                                gi.ItemRate = decimal.Parse(((Label)gvDetails.FindControl("Rate")).Text);
                                gi.RcvdQty = decimal.Parse(((TextBox)gvDetails.FindControl("txtRelQty")).Text);
                                gi.ReqQty = decimal.Parse(((TextBox)gvDetails.FindControl("txtRelQty")).Text);
                                dc.T_MMS_SRNItems.InsertOnSubmit(gi);
                                dc.SubmitChanges();
                                int POID = int.Parse(gvItemDetails.DataKeys[gvDetails.RowIndex][2].ToString());
                                lsttvalue.Add(new ListSRNItems(POID.ToString(), gi.ResourceID.ToString(), gi.SRNItemID.ToString()));
                            }
                            else
                            {
                                gi = (from s in dc.T_MMS_SRNItems.Where(a => a.SRNItemID == SRNItemID) select s).SingleOrDefault();
                              
                                if (gi == null)
                                {
                                    gi = new T_MMS_SRNItem();
                                    gi.CreatedBy =  Convert.ToInt32(Session["UserId"]);
                                    gi.CreatedOn = DateTime.Now;
                                    gi.IsActive = 1;
                                    gi.SRNID = scopeIdentity;
                                    gi.PodetID = int.Parse(gvItemDetails.DataKeys[gvDetails.RowIndex][0].ToString());
                                    gi.ResourceID = int.Parse(gvItemDetails.DataKeys[gvDetails.RowIndex][1].ToString());
                                    if (gvDetails.Cells[3].Text != string.Empty)
                                        gi.Qty = decimal.Parse(gvDetails.Cells[3].Text);
                                    if (gvDetails.Cells[5].Text != string.Empty)
                                        gi.ReqQty = decimal.Parse(gvDetails.Cells[5].Text); //Bal Qty
                                    gi.Au_ID = int.Parse(((Label)gvDetails.FindControl("AuID")).Text);
                                    //gi.Au_ID = gvDetails.Cells[2].Text;
                                    if (((Label)gvDetails.FindControl("AuIDs")).Text != "")
                                    {
                                        gi.Au_IDS = int.Parse(((Label)gvDetails.FindControl("AuIDs")).Text);
                                    }
                                    else
                                    {
                                        gi.Au_IDS = null;
                                    }

                                    gi.ItemRate = decimal.Parse(((Label)gvDetails.FindControl("Rate")).Text);
                                    gi.RcvdQty = decimal.Parse(((TextBox)gvDetails.FindControl("txtRelQty")).Text);
                                    gi.ReqQty = decimal.Parse(((TextBox)gvDetails.FindControl("txtRelQty")).Text);
                                    dc.T_MMS_SRNItems.InsertOnSubmit(gi);
                                    dc.SubmitChanges();
                                    //scopeIdentityGDNItem = gi.SRNItemID;
                                    int POID = int.Parse(gvItemDetails.DataKeys[gvDetails.RowIndex][2].ToString());
                                    lsttvalue.Add(new ListSRNItems(POID.ToString(), gi.ResourceID.ToString(), gi.SRNItemID.ToString()));
                                }
                                else
                                {
                                    gi.RcvdQty = decimal.Parse(((TextBox)gvDetails.FindControl("txtRelQty")).Text);
                                    gi.ReqQty = decimal.Parse(((TextBox)gvDetails.FindControl("txtRelQty")).Text);
                                    gi.IsActive = 1;
                                    dc.SubmitChanges();
                                }
                            }
                        }





                        dc.SubmitChanges();
                    }
                }
            }
            catch (Exception e)
            {
                clsErrorLog.Log(e);
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
                if (ds.Tables[0].Rows[0]["VId"].ToString() == "")
                    ddlVehicle.SelectedValue = "-1";
                else
                    if (ddlVehicle.Items.FindByValue(ds.Tables[0].Rows[0]["VId"].ToString()) != null)
                        ddlVehicle.SelectedValue = ds.Tables[0].Rows[0]["VId"].ToString();
                    else
                    {
                        string VId = ds.Tables[0].Rows[0]["VId"].ToString();
                        string VehRegnNr = ds.Tables[0].Rows[0]["VehRegnNr"].ToString();
                        ddlVehicle.Items.Insert(1, new ListItem(VehRegnNr, VId));
                        ddlVehicle.SelectedIndex = 1;
                    }


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

                if (ds.Tables[0].Rows[0]["InvoiceNo"].ToString() != "")
                    txtInvoice.Text = ds.Tables[0].Rows[0]["InvoiceNo"].ToString();

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

        protected void LoadDropDown()
        {
            txtAllVeh.Text = ""; txtVendor.Text = ""; 
            txtDest.Text = ""; txtDestRep.Text = "";
            FIllObject.FillEmptyDropDown(ref ddlVehicle);
            FIllObject.FillEmptyDropDown(ref ddlVendor);
            FIllObject.FillEmptyDropDown(ref ddlDestination);
            FIllObject.FillEmptyDropDown(ref ddlDestRepre);
            WSID = Convert.ToInt32(ddlWorkSite.SelectedValue);
            if (txtNoOfRec.Text != string.Empty && txtNoOfRec.Text != "")
                TopRows = Convert.ToInt32(txtNoOfRec.Text);
            else
                TopRows = 0;
            SqlParameter[] par = new SqlParameter[2];
            par[0] = new SqlParameter("@WSID", WSID);
            if (TopRows != 0)
                par[1] = new SqlParameter("@TopRows", TopRows);
            else
                par[1] = new SqlParameter("@TopRows", System.Data.SqlDbType.Int);
            DataSet ds = SQLDBUtil.ExecuteDataset("MMS_DDL_SDNsBind", par);
            ddlVehicle.DataSource = ds.Tables[1]; ddlVehicle.DataBind();
            ddlVendor.DataSource = ds.Tables[5]; ddlVendor.DataBind();
            ddlDestination.DataSource = ds.Tables[3]; ddlDestination.DataBind();
            ds.Clear();
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

       

        #endregion 

        #region OnClick

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("SRNStatus.aspx?ID=1");
        }

        protected void BtnGo_Click(object sender, EventArgs e)
        {
            LoadDropDown();
        }

        protected void btnAllVeh_Click(object sender, EventArgs e)
        {
            FillVehicleNameDropDown();
        }

        protected void ddlVendor_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillListBoxPO();
            lstItemDetails.DataSource = "";
            lstItemDetails.DataBind();
            lstItemDetails.Items.Clear();
          
        }

        protected void lnkAllVendors_Click(object sender, EventArgs e)
        {
            FillVendorDropDown();
        }

       

        protected void btnDest_Click(object sender, EventArgs e)
        {
            FillDestinationDropDown();
        }

        protected void btnGetPO_Click(object sender, EventArgs e)
        {

            FillListBoxPO();
            lstItemDetails.Items.Clear();
        }

        protected void btnAddDetails_Click(object sender, EventArgs e)
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
                            DataSet ds = (DataSet)obj1.GetMMS_SDNPurchaseOrderdetails(int.Parse(lstPODetails.SelectedValue), int.Parse(lstItemDetails.Items[index].Value));
                            if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0)
                            {
                                dtRow = dtSRNItem.NewRow();
                                dtRow["PoName"] = lstPODetails.SelectedItem.Text;
                                dtRow["POID"] = int.Parse(lstPODetails.SelectedValue);
                                ViewState["POID"] = dtRow["POID"];
                                dtRow["Item"] = lstItemDetails.Items[index].Text;
                                dtRow["Qty"] = ds.Tables[0].Rows[0]["Qty"].ToString();
                                dtRow["RelQty"] = ds.Tables[0].Rows[0]["Qty"].ToString();
                                dtRow["PodetID"] = ds.Tables[0].Rows[0]["PodetID"].ToString();
                                dtRow["Itemid"] = ds.Tables[0].Rows[0]["Itemid"].ToString();
                                dtRow["BalQty"] = ds.Tables[0].Rows[0]["BalQty"].ToString();
                                dtRow["BQty"] = ds.Tables[0].Rows[0]["BQty"].ToString();
                                dtRow["State"] = ds.Tables[0].Rows[0]["State"].ToString();
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

        protected void btnDestRep_Click(object sender, EventArgs e)
        {
            FillDestinationRepresentativeDropDown();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            DataTable dtDetId = new DataTable();
            dtDetId.Columns.Add("PoDetId", typeof(System.Int32));

            if (gvItemDetails.Rows.Count == 0)
            {
                AlertMsg.MsgBox(Page, "Please Add goods!!!");
                btnSave.Enabled = false;
            }

            else
            {
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
                                objGDN.ClosePo(POdetId, Empid);
                                Ret = Ret + 1;
                                string msg;
                                msg = gvr.Cells[1].Text;
                                if (Ret > 1)
                                    CloseMsg = CloseMsg + "," + msg;
                                else
                                    CloseMsg = CloseMsg + msg;
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
                        string Msg;
                        Msg = gvr.Cells[1].Text;
                        if (count > 1)
                            RejMsg = Msg + "," + Msg;
                        else
                            RejMsg = RejMsg + Msg;
                    }

                }
                if (dtDetId.Rows.Count > 0)
                {

                    try
                    {
                      
                        using (MMSIDataContext dc = new MMSIDataContext())
                        {
                            //WorkSite Id must save in DB.
                            //This code Review is compulsory.
                            T_MMS_SRN srn;
                            int scopeIdentity = 0;
                            string msg = " SRN Created.";
                            message = "Created";
                            bool isNewRecord = btnSave.Text.Trim().ToLower().Contains("save");
                            if (isNewRecord)
                            {
                                srn = new T_MMS_SRN();
                                srn.CreatedBy =  Convert.ToInt32(Session["UserId"]);
                                srn.CreatedOn = DateTime.Now;
                                srn.IsActive = 1;
                                // srn.ModuleId = 7;
                                if (ddlVehicle.SelectedValue.ToString() != "-1")

                                    srn.VId = int.Parse(ddlVehicle.SelectedValue);
                                else
                                    srn.VId = null;
                                srn.Vendor_ID = int.Parse(ddlVendor.SelectedValue);
                                srn.Destination = int.Parse(ddlDestination.SelectedValue);
                                srn.DestRepr = int.Parse(ddlDestRepre.SelectedValue);
                                Nullable<int> x = null;


                                srn.ServiceStatus = (int)LookUp.ServiceStatus.Waiting;


                                if (txtTripSheet.Text != string.Empty)
                                    srn.TripSheet = txtTripSheet.Text;
                                srn.InvoiceNo = txtInvoice.Text == null ? "" : txtInvoice.Text.Trim();
                                srn.WO = Convert.ToInt32(lstPODetails.SelectedValue);
                                srn.WorkSite = Convert.ToInt32(ddlWorkSite.SelectedValue);
                                try
                                {
                                    DateTime dtTemp = DateTime.ParseExact(txtDate.Text, "dd/MM/yyyy", CultureInfo.CurrentCulture, DateTimeStyles.AllowWhiteSpaces);
                                    // DateTime dtTemp = DateTime.Parse(txtDate.Text);
                                    string Date = dtTemp.Day.ToString() + "/" + dtTemp.Month.ToString() + "/" + dtTemp.Year.ToString();
                                    dtTemp = dtTemp.AddHours(int.Parse(ddlstarttime.SelectedItem.Text.Trim())).AddMinutes(int.Parse(txtMinutes.Text.Trim()));
                                    if (ddlTimeFormat.SelectedItem.Text.Trim() == "PM")
                                        dtTemp = dtTemp.AddHours(12);
                                    srn.StartDate = dtTemp;//DateTime.ParseExact(txtDate.Text, "dd/MM/yyyy", CultureInfo.CurrentCulture, DateTimeStyles.AllowWhiteSpaces);
                                }
                                catch
                                {
                                }
                                dc.T_MMS_SRNs.InsertOnSubmit(srn);
                                dc.SubmitChanges();
                                scopeIdentity = srn.SRNID;
                                ViewState["SRNID"] = srn.SRNID;
                                SRNItemsInsertAndUpdate(scopeIdentity);



                            }
                            else
                            {
                                msg = " SRN Updated.";
                                scopeIdentity = Convert.ToInt32(ViewState["SRNID"]);
                                SRNItemsInsertAndUpdate(scopeIdentity);
                                srn = (from s in dc.T_MMS_SRNs.Where(a => a.SRNID == scopeIdentity) select s).SingleOrDefault();
                                if (ddlVehicle.SelectedValue != string.Empty)
                                    srn.VId = int.Parse(ddlVehicle.SelectedValue);
                                btnSave.Text = "Save";
                                try
                                {
                                    DateTime dtTemp = DateTime.ParseExact(txtDate.Text, "dd/MM/yyyy", CultureInfo.CurrentCulture, DateTimeStyles.AllowWhiteSpaces);
                                    dtTemp = dtTemp.AddHours(int.Parse(ddlstarttime.SelectedItem.Text.Trim())).AddMinutes(int.Parse(txtMinutes.Text.Trim()));
                                    if (ddlTimeFormat.SelectedItem.Text.Trim() == "PM")
                                        dtTemp = dtTemp.AddHours(12);
                                    srn.StartDate = dtTemp;//DateTime.ParseExact(txtDate.Text, "dd/MM/yyyy", CultureInfo.CurrentCulture, DateTimeStyles.AllowWhiteSpaces);

                                }
                                catch
                                {
                                }
                                srn.UpdatedBy =  Convert.ToInt32(Session["UserId"]);
                                srn.UpdatedOn = DateTime.Now;
                                if (ddlVendor.SelectedValue != "-1")
                                    srn.Vendor_ID = int.Parse(ddlVendor.SelectedValue);
                                srn.Destination = int.Parse(ddlDestination.SelectedValue);
                                srn.DestRepr = int.Parse(ddlDestRepre.SelectedValue);
                                srn.InvoiceNo = txtInvoice.Text == null ? "" : txtInvoice.Text.Trim();
                                srn.WO = Convert.ToInt32(lstPODetails.SelectedValue);
                                srn.ServiceStatus = (int)LookUp.GoodsStatus.SupplyerYard;
                                srn.Mode = (int)LookUp.TransMode.Close;
                                srn.WorkSite = Convert.ToInt32(ddlWorkSite.SelectedValue);

                                if (txtTripSheet.Text != string.Empty)
                                    srn.TripSheet = txtTripSheet.Text;


                                dc.SubmitChanges();
                                lstItemDetails.Items.Clear();
                            }
                            DataTable dtSRNItem = (DataTable)ViewState["dtSRNItem"];
                            ViewState["dtSRNItem"] = null;

                            gvItemDetails.DataSource = "";
                            gvItemDetails.DataBind();
                            lblSRNID.Visible = false;


                            if (Ret == 0 && count == 0)
                            {
                                AlertMsg.MsgBox(Page, srn.SRNID + " PSDN " + message);
                            }
                            else if (Ret == 0 && count != 0)
                            {
                                //if (RejMsg == "")
                                //{ RejMsg = count.ToString()+" item(s)"; }
                                AlertMsg.MsgBox(Page, RejMsg + " Qty(s) Exceeded the total item(s) Qty! So these item(s) were not added in PSDN!   PSDN# " + srn.SRNID + " done with remaining items(s)! ");
                            }
                            else if (Ret != 0 && count == 0)
                            {
                                AlertMsg.MsgBox(Page, CloseMsg + " Qty(s) reached the total item(s) Qty, these  item(s) were closed!  PSDN# " + srn.SRNID + " done!. ");
                            }
                            else if (Ret != 0 && count != 0)
                            {
                                AlertMsg.MsgBox(Page, RejMsg + " Qty(s)  exceeded the total item(s) Qty! So these items were not added in PSDN! " + CloseMsg + " Qty(s) reached the total item(s) Qty, these  item(s) were closed!   PSDN# " + srn.SRNID + " done! ");
                            }
                        }
                      

                    }
                    catch (Exception ex)
                    {
                        // clsErrorLog.Log(ex);
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

        protected void btnSubmit_Click(object sender, EventArgs e)
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


        #endregion


    }
}