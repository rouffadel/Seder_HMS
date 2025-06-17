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
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using DataAccessLayer;
using Aeclogic.Common.DAL;
using System.Reflection;
using System.Diagnostics;
using AECLOGIC.HMS.BLL;
using AECLOGIC.ERP.COMMON;
using System.Collections.Generic;

namespace AECLOGIC.ERP.HMS
{
    public partial class SRNStatus : AECLOGIC.ERP.COMMON.WebFormMaster
    {
        #region PageLoad

        int EmpId = 0;
            static int WSId=0;
        static char Staus='1';
        static int CompanyID;
        int mid = 0; string menuname; string menuid;
        Common obj = new Common();
        SRNService objSRN = new SRNService();
        int ID = 1; int stateid = 1; int srnid = 0;

        protected override void OnInit(EventArgs e)
        {
            ModuleID = 1;
            base.OnInit(e);
        
            TasksPaging.FirstClick += new Paging.PageFirst(TasksPaging_FirstClick);
            TasksPaging.PreviousClick += new Paging.PagePrevious(TasksPaging_FirstClick);
            TasksPaging.NextClick += new Paging.PageNext(TasksPaging_FirstClick);
            TasksPaging.LastClick += new Paging.PageLast(TasksPaging_FirstClick);
            TasksPaging.ChangeClick += new Paging.PageChange(TasksPaging_FirstClick);
            TasksPaging.ShowRowsClick += new Paging.ShowRowsChange(TasksPaging_ShowRowsClick);
            TasksPaging.CurrentPage = 1;
        }

        protected void Page_Load(object sender, EventArgs e)
        {


          
            Ajax.Utility.RegisterTypeForAjax(typeof(SRNStatus));
           
            CompanyID = Convert.ToInt32(Session["CompanyID"]);
           

            if (Request.QueryString.Count > 0)
                ID = Convert.ToInt32(Request.QueryString["ID"]);

            Session["SRN"] = ID;
            this.Page.MaintainScrollPositionOnPostBack = true;
            if (!IsPostBack)
            {

                SetUpScreen();
                if (Request.QueryString.Count > 0)
                {
                    stateid = Convert.ToInt32(Request.QueryString["Id"]);
                    gvRecieve.Visible = true;
                    if (Request.QueryString["SRNID"] != null)
                    {
                        srnid = Convert.ToInt32(Request.QueryString["SRNID"]);
                    }
                }
                btnSubmit.Attributes.Add("onclick", "javascript:return Validate('" + ID.ToString() + "'); ");


            }

            if (Convert.ToInt32(Request.QueryString["ID"]) == 1)
            {
                btnSubmit.Visible = false;
                btnSendBack.Visible = false;

            }
            if (ID == 1)
            {
                gvRecieve.Columns[5].Visible = false;
                // gvSRN.Columns[0].Visible = false;
                gvSRN.Columns[12].Visible = false;
                gvSRN.Columns[6].Visible = false;
                btnSelectSrn.Text = "Receive All";
                btnSubmit.Text = "Receive";

            }
            else if (ID == 2)
            {
                gvRecieve.Columns[5].Visible = true;
                gvSRN.Columns[10].Visible = false;
                gvSRN.Columns[11].Visible = false; gvSRN.Columns[12].Visible = false;
                btnSelectSrn.Text = "Approve All";

                btnSubmit.Text = "Approve";
                gvSRN.Columns[6].Visible = false;

                if (srnid != 0)
                {
                    BindDataGrid(objSRN);
                }

            }
            else
            {
                gvRecieve.Columns[5].Visible = false;
                gvSRN.Columns[9].Visible = false;
                btnSubmit.Visible = false;
                gvSRN.Columns[10].Visible = false;
                gvSRN.Columns[11].Visible = true;
                gvSRN.Columns[12].Visible = true;
                btnSelectSrn.Visible = false;
                gvSRN.Columns[6].Visible = false;
                if (srnid != 0)
                {
                    BindDataGrid(objSRN);
                }



            }
            selected();
        }

        void TasksPaging_ShowRowsClick(object sender, EventArgs e)
        {
            TasksPaging.CurrentPage = 1;
            BindPager();
        }

        void TasksPaging_FirstClick(object sender, EventArgs e)
        {
            objSRN.PageSize = TasksPaging.CurrentPage;
            objSRN.CurrentPage = TasksPaging.ShowRows;
            BindPager();
        }

        void BindPager()
        {
            objSRN.PageSize = TasksPaging.CurrentPage;
            objSRN.CurrentPage = TasksPaging.ShowRows;
            BindDataGrid(objSRN);
        }
        #endregion

        #region SetupScreen

        private void FillDropDownVendor()
        {
           
             
           DataSet ds = SQLDBUtil.ExecuteDataset("MMS_DDL_VENDORINFO");
            ddlVendor.DataSource = ds;
            ddlVendor.DataTextField = ds.Tables[0].Columns[0].ToString();
            ddlVendor.DataValueField = ds.Tables[0].Columns[1].ToString();
            ddlVendor.Items.Insert(0, new ListItem("Select Vendor Details", "0"));
            ddlVendor.DataBind();

        }

       

        #endregion

        #region OnClick

        protected void btClear_Click(object sender, EventArgs e)
        {
            ddlVendor.SelectedIndex = 0;
            txtPONo.Text = "";
            txtSRNNo.Text = "";
            txtGdnFromDate.Text = ""; txtGdnToDate.Text = "";
            txtSearchWorksite.Text = "";
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            TasksPaging.CurrentPage = 1;
            BindDataGrid(objSRN);
        }

        protected void btnSubmit_OnClick(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(ID.ToString());

            DataTable dtbl = new DataTable("SRNTbl");
            dtbl.Columns.Add(new DataColumn("SRNItemId", typeof(System.Int32)));
            GDNSDataSet.Tables.Add(dtbl);
            foreach (GridViewRow gvRow in gvRecieve.Rows)
            {

                Label lblSRNItemId = new Label();
                lblSRNItemId = (Label)gvRow.FindControl("lblSRNItemID");
                DataRow dr = dtbl.NewRow();
                dr["SRNItemId"] = Convert.ToInt32(lblSRNItemId.Text);
                dtbl.Rows.Add(dr);



                EmpId = Convert.ToInt32(Session["LoginId"]);
                int Distance = 0; decimal RcvdQty = 0;

                Label lblSrnItemId = (Label)gvRow.FindControl("lblSRNItemID");
                ViewState["srnItemId"] = lblSrnItemId.Text;
                TextBox txtArrQty = (TextBox)gvRow.FindControl("txtArrivedQuantity");
                TextBox txtRcvdQty = (TextBox)gvRow.FindControl("txtRcvdQty");
                TextBox txtComments = (TextBox)gvRow.FindControl("txtComments");
                DropDownList ddlChkBy = (DropDownList)gvRow.FindControl("ddlCheckedInBy");
                TextBox txtDistance = (TextBox)gvRow.FindControl("txtKMs");
                Label lblDistance = (Label)gvRow.FindControl("lbldistance");

                int SrnItemId = Convert.ToInt32(lblSrnItemId.Text);
                int RecivedBy = Convert.ToInt32(ddlChkBy.SelectedValue);
                string Comments = Convert.ToString(txtComments.Text);
                if (id == 1)
                {
                    if (txtDistance.Text != "")
                        Distance = Convert.ToInt32(txtDistance.Text);
                    RcvdQty = Convert.ToDecimal(txtArrQty.Text);

                }
                else if (id == 2)
                {
                    if (lblDistance.Text != "")
                        Distance = Convert.ToInt32(lblDistance.Text);
                    RcvdQty = Convert.ToDecimal(txtRcvdQty.Text);
                }
                objSRN.MMS_UpdateSrnItems(SrnItemId, id, RcvdQty, RecivedBy, Comments, Distance, EmpId);

            }
            GDNSDataSet.AcceptChanges();
            DataSet dsGDNS = new DataSet("SRNDataSet");
            DataTable dt = new DataTable("SRNTable");
            dt.Columns.Add(new DataColumn("SRNId", typeof(System.Int32)));
            dt.Columns.Add(new DataColumn("WOId", typeof(System.Int32)));
            dsGDNS.Tables.Add(dt);
            if (id == 2)
            {
                DataSet ds = objSRN.MMS_GetGdnsFromXML(GDNSDataSet);
                int c = 0;
                foreach (DataRow drnew in ds.Tables[0].Rows)
                {
                    drnew["SRnId"] = ds.Tables[0].Rows[c]["SRNID"].ToString();
                    int WOTypeId = 0, WOId = 0;
                    DataSet ds1 = new DataSet();
                    SqlParameter[] p = new SqlParameter[1];
                    p[0] = new SqlParameter("@srnId", (int)drnew["SRnId"]);
                    ds1 = SQLDBUtil.ExecuteDataset("MMS_GetServiceWOId", p);
                    if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
                    {
                        WOTypeId = Convert.ToInt32(ds1.Tables[0].Rows[0]["TypeId"]);
                        WOId = Convert.ToInt32(ds1.Tables[0].Rows[0]["POId"]);
                        if (WOTypeId == 1)
                        {
                            DataRow dr = dt.NewRow();
                            dr["SRNId"] = (int)drnew["SRnId"];
                            dr["WOId"] = WOId;
                            dt.Rows.Add(dr);
                            dsGDNS.AcceptChanges();
                            SqlParameter[] par = new SqlParameter[1];
                            par[0] = new SqlParameter("@SRNIDs", dsGDNS.GetXml());
                            SQLDBUtil.ExecuteNonQuery("MMS_ServiceInstantBillingByXML", par);
                           
                        }
                        ds1.Clear();
                    }
                    c = c + 1;
                }
            }
            gvRecieve.Visible = false;
            gvSRN.Visible = true;
            TasksPaging.Visible = true;
            btnSubmit.Visible = false;
            SetUpScreen();
            BindDataGrid(objSRN);
        }

        protected void btnSendBack_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow gvRow in gvSRN.Rows)
            {
                if (((CheckBox)gvRow.Cells[0].Controls[1]).Checked)
                {
                    int RetVal;
                    int UserId =  Convert.ToInt32(Session["UserId"]);
                    int SRNID;
                    Label lblSRNID = new Label();
                    lblSRNID = (Label)gvRow.Cells[1].FindControl("SrnName");
                    SRNID = Convert.ToInt32(lblSRNID.Text);
                    RetVal = Convert.ToInt32(objSRN.MMS_SRN_SendBack(SRNID, UserId));

                    if (RetVal == 0)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, typeof(Page), "ale", "window.alert('SRN already in Billing,Unable to SendBack!');", true);
                       
                    }
                    else
                    {

                        ScriptManager.RegisterStartupScript(this.Page, typeof(Page), "ale", "window.alert('Send Back Successfully!');", true);

                    }
                    gvRecieve.Visible = false;
                    gvSRN.Visible = true;
                    TasksPaging.Visible = true;
                    SetUpScreen();
                }
            }
            BindDataGrid(objSRN);
        }

        protected void btnSelectSrn_Click(object sender, EventArgs e)
        {
            DataSet dsSRNS = new DataSet("SRNDataSet");
            DataTable dt = new DataTable("SRNTable");
            dt.Columns.Add(new DataColumn("SRNId", typeof(System.Int32)));
            dsSRNS.Tables.Add(dt);


            gvRecieve.Visible = true;
            gvSRN.Visible = false;
            btnSendBack.Visible = false;
            foreach (GridViewRow gvRow in gvSRN.Rows)
            {
                CheckBox chk = new CheckBox();
                chk = (CheckBox)gvRow.FindControl("chkPrereq");
                
                if (chk.Checked)
                {
                    ViewState["SRNID"] = int.Parse(gvSRN.DataKeys[gvRow.RowIndex][0].ToString());
                    DataRow dr = dt.NewRow();
                    dr["SRNID"] = Convert.ToInt32(ViewState["SRNID"]);
                    dt.Rows.Add(dr);
                }
            }
            dsSRNS.AcceptChanges();
            DataSet ds = objSRN.MMS_gvAllSRNsBind(dsSRNS, ID);
            if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0)
            {

            }
            else
            {
                btnSubmit.Visible = false;
                TasksPaging.Visible = false;
            }
            gvRecieve.DataSource = ds;
            gvRecieve.DataBind();

            btnSubmit.Visible = true;
            TasksPaging.CurrentPage = 1;
            BindDataGrid(objSRN);
            btnSelectSrn.Visible = false;
            btnSendBack.Visible = false;
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            btnDelete.Visible = true;
            foreach (GridViewRow gvRow in gvSRN.Rows)
            {
                if (((CheckBox)gvRow.Cells[0].Controls[1]).Checked)
                {
                    int UserId =  Convert.ToInt32(Session["UserId"]);
                    int SRNID;
                    Label lblSRNID = new Label();
                    lblSRNID = (Label)gvRow.Cells[1].FindControl("SrnName");
                    SRNID = Convert.ToInt32(lblSRNID.Text);
                    
                    objSRN.MMS_DeleteSRN(SRNID, UserId);
                    gvRecieve.Visible = false;
                    gvSRN.Visible = true;
                    TasksPaging.Visible = true;
                    SetUpScreen();
                }
            }
            BindDataGrid(objSRN);
        }

        #endregion

        #region Methods

        private void BindDataGrid(SRNService objSRN)
        {
            try
            {
                DateTime? dtTodate = null;
                DateTime? dtFromDate = null;
                if (txtGdnFromDate.Text != string.Empty)
                {

                    dtFromDate = CodeUtil.ConverttoDate(txtGdnFromDate.Text, CodeUtil.DateFormat.DayMonthYear); //Convert.ToDateTime(txtGdnFromDate.Text);
                    if (txtGdnToDate.Text != string.Empty)
                    {
                        dtTodate = CodeUtil.ConverttoDate(txtGdnToDate.Text, CodeUtil.DateFormat.DayMonthYear).AddDays(1).AddMilliseconds(-1);
                    }
                    else
                    {
                        dtTodate = CodeUtil.ConverttoDate(txtGdnFromDate.Text, CodeUtil.DateFormat.DayMonthYear).AddMilliseconds(-1);
                    }
                }
                objSRN.PageSize = TasksPaging.ShowRows; ;
                objSRN.CurrentPage = TasksPaging.CurrentPage;
                 
                int SRNNo = 0;
                int WONo = 0; int VendorID = 0; int WSId = 0; int ModuleId;
                if (Convert.ToBoolean(ViewState["ViewAll"]) == true)
                    ModuleId = 0;
                else
                    ModuleId = ModuleID;
                string srn = string.Empty;
                if (srnid != 0)
                {
                    srn = srnid.ToString();
                }

                if (srn != "")
                {
                    SRNNo = Convert.ToInt32(srn);
                }
                else
                {
                    if (txtSRNNo.Text == string.Empty)
                        SRNNo = 0;
                    else
                        SRNNo = Convert.ToInt32(txtSRNNo.Text);
                }

                if (txtPONo.Text != "")
                    WONo = Convert.ToInt32(txtPONo.Text);

               
                if (Convert.ToInt32(ddlWorkSites_hid.Value == "" ? "0" : ddlWorkSites_hid.Value) != 0)

                    WSId = Convert.ToInt32(Convert.ToInt32(ddlWorkSites_hid.Value == "" ? "0" : ddlWorkSites_hid.Value));
                else
                    WSId = 0;


                if (ddlVendor.SelectedIndex != 0)

                    VendorID = Convert.ToInt32(ddlVendor.SelectedValue);
                else
                    VendorID = 0;
                DataSet ds = null;

                if (ID > 0)
                {
                    ds = objSRN.GetMMS_SRN_New(objSRN, SRNNo, WONo, WSId, VendorID, ID, ModuleId, dtFromDate, dtTodate);

                }
                if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
                {
                    gvSRN.DataSource = ds;
                    if (ID == 3)
                    {
                        gvSRN.Visible = true;
                        btnSelectSrn.Visible = false;
                        btnDelete.Visible = true;
                    }
                    else

                        btnSelectSrn.Visible = true;
                    TasksPaging.Visible = true;
                    if (ID == 1)
                        btnSendBack.Visible = false;
                    else
                        btnSendBack.Visible = true;
                }
                gvSRN.DataBind();
                TasksPaging.Bind(objSRN.CurrentPage, objSRN.TotalPages, objSRN.NoofRecords, objSRN.PageSize);//, objSRN.PageSize
            }
            catch
            {

            }
        }

        public void selected()
        {
            string URL = Request.Url.Segments[Request.Url.Segments.Length - 1] + Request.Url.Query;
           
        }

     

        public string PONavigateUrl(string POID)
        {
            int val = 1;
            bool chk = true;
            return "javascript:return window.open('ProPurchaseOrderPrint.aspx?id=" + POID + "&PON=" + val + "&tot=" + chk + "' , '_blank')";
        }

        public string GetProSaleOrderPrints(string ID)
        {
            string strReturn;
            strReturn = String.Format("window.open('ProSaleOrderPrint.aspx?id=" + ID + "&PON=" + 2 + "&tot=" + true + "' , '_blank');", ID);
            return strReturn;
        }

        private void SetUpScreen()
        {
            FillDropDownVendor();
           
            objSRN.CurrentPage = TasksPaging.CurrentPage;
            
        }

        public string GetGoodsItems(string ID)
        {
            string strReturn;

            strReturn = String.Format("window.showModalDialog('GetServiceItems.aspx?ID=0&SRN={0}' ,'','dialogWidth:520px; dialogHeight:400px; center:yes');", ID);

            return strReturn;
        }

        public DataSet BindDropDownList()
        {

            DataSet ds = SQLDBUtil.ExecuteDataset("MMS_DDL_EmployeeMaster");
            return ds;
        }

        


        //void AlertMsg.MsgBox(Page,string alert)
        //{
        //    string strScript = "<script language='javascript'>alert(\'" + alert + "\');</script>";
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "PopupCP", strScript, false);
        //}


        #endregion

        #region RowCommand

        protected void gvSRN_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                CheckBox chkHeader = (CheckBox)e.Row.FindControl("chkHeader");
                chkHeader.Attributes.Add("onclick", String.Format("javascript:return SelectAll(this,'{0}','chkPrereq');", gvSRN.ClientID));
            }
            else if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton lnk = (LinkButton)e.Row.Cells[9].Controls[1];


                try
                {
                    if (Request.QueryString["ID"] != null)
                    {
                        switch (Request.QueryString["ID"].Trim())
                        {
                            case "1":
                                lnk.Text = "Receive";
                                lnk.CommandName = "Receive";

                                break;
                            case "2":
                                lnk.Text = "Approve";
                                lnk.CommandName = "Approve";

                                break;
                            case "3":
                                break;
                            default:
                                break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        protected void gvSRN_RowCommand(object sender, GridViewCommandEventArgs e)
        {


            if (e.CommandName == "Edt")
            {
                int SrnId = 0; string Remark = "Edited";
                SrnId = Convert.ToInt32(e.CommandArgument);
                string Method = System.Reflection.MethodBase.GetCurrentMethod().Name;
               

                Response.Redirect("SDNNewHMS.aspx?ID=" + SrnId);


            }
            else if (e.CommandName == "Del")
            {
                int SrnId = 0;
                int UserId =  Convert.ToInt32(Session["UserId"]);
                SrnId = Convert.ToInt32(e.CommandArgument);
                int RetVal = Convert.ToInt32(objSRN.MMS_DeleteSRN(SrnId, UserId));

                if (RetVal == 0)
                {
                    ScriptManager.RegisterStartupScript(this.Page, typeof(Page), "ale", "window.alert('SRN already in Billing,Unable to SendBack!');", true);
                   
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, typeof(Page), "ale", "window.alert('Send Back Successfully!');", true);

                }
                BindDataGrid(objSRN);
                
            }


        }

      

        DataSet GDNSDataSet = new DataSet("SRNs");

        protected void gvRecieve_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblDistance = (Label)e.Row.FindControl("lbldistance");// Cells[5].Controls[1];
                TextBox txtDistance = (TextBox)e.Row.FindControl("txtKMs");// Cells[5].Controls[2];
                Label lblRcvdQty = (Label)e.Row.FindControl("lblArvdQty");
                TextBox txtArrivedQuantity = (TextBox)e.Row.FindControl("txtArrivedQuantity");
                try
                {
                    switch (Request.QueryString["ID"].Trim())
                    {
                        case "1":
                            lblRcvdQty.Visible = false;
                            txtArrivedQuantity.Visible = true;
                            txtDistance.Visible = true;

                            lblDistance.Visible = false;
                            break;
                        case "2":
                            lblRcvdQty.Visible = true;
                            txtArrivedQuantity.Visible = false;
                            lblDistance.Visible = true;

                            txtDistance.Visible = false;
                            break;
                        case "3":
                            lblRcvdQty.Visible = true;
                            txtArrivedQuantity.Visible = false;
                            lblDistance.Visible = true;
                            txtDistance.Visible = false;

                            
                            break;
                        default:
                            break;
                    }
                }
                catch
                {
                }



            }

        }


        #endregion

        #region AjaxMethods

        [Ajax.AjaxMethod()]
        public string MMS_CLOSEPO(string PONO, string Itemid)
        {
            QA.MMS_CLOSEPO(PONO, Itemid);
            return "";

        }

        [Ajax.AjaxMethod()]
        public string SRNProofs(string SRNID)
        {
            string StrRet = "";
            DataSet ds = objSRN.MMS_CheckPicInSRNNItems(int.Parse(SRNID));
            if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0)
            {
                int ImgCount = Convert.ToInt32(ds.Tables[1].Rows[0]["Invoiceimg"]);
                int GdnItemCount = Convert.ToInt32(ds.Tables[2].Rows[0]["SrnItemId"]);
                
                if (ImgCount > 0)
                    return StrRet = "1";
                else
                    return StrRet = "0";
            }
            return StrRet;
        }


        [Ajax.AjaxMethod()]
        public string SRNItems(string SRNID)
        {
            string StrRet = "";
            DataSet ds = objSRN.MMS_CheckPicInSRNNItems(int.Parse(SRNID));
            if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0)
            {
                int GdnItemCount = Convert.ToInt32(ds.Tables[0].Rows[0]["SrnItemId"]);
                return GdnItemCount.ToString();
            }
            return StrRet;
        }

        #endregion

        public bool IsDelete()
        {
            bool IsDelete = (bool)ViewState["Editable"];
            if (IsDelete == true)


                return true;
            else
                return false;

        }

        public bool IsEditble()
        {
            bool IsEditble = (bool)ViewState["Editable"];
            if (IsEditble == true)


                return true;
            else
                return false;


        }
        public bool ViewVisible(string SrnItemID, int Status)
        {
            bool status = false;
            DataSet ds = objSRN.MMS_CheckPicInSRNNItems(int.Parse(SrnItemID));
            if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0)
            {
                int ImgCount = Convert.ToInt32(ds.Tables[1].Rows[0]["Invoiceimg"]);
                if (Status == 1)
                {
                    if (ImgCount > 0)
                        status = false;
                    else
                        status = true;
                }
                else
                {
                    if (ImgCount > 0)
                        status = true;
                    else
                        status = false;

                }
            }
            return status;
        }



        public string ViewInvImage(string obj, string SrnItemID)
        {
            string ReturnVal = "";
            if (obj != "")
            {
                ReturnVal = "javascript:return window.open('./SDNItemsImages/" + SrnItemID + "." + obj + "', '_blank')";
            }
            return ReturnVal;

        }

        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionList(string prefixText, int count, string contextKey)
        {
           
            DataSet ds = AttendanceDAC.HR_GetWorkSite_basedon_Wsid_googlesearch(prefixText.Trim(),WSId,Staus,CompanyID);
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
        protected void GetWork(object sender, EventArgs e)
        {

            CompanyID = Convert.ToInt32(Session["CompanyID"]);
            WSId = Convert.ToInt32(ddlWorkSites_hid.Value == "" ? "0" : ddlWorkSites_hid.Value); ;
          
        }

    }
}

