using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;
using AECLOGIC.HMS.BLL;
using System.Globalization;
using DataAccessLayer;
using Aeclogic.Common.DAL;
using AECLOGIC.ERP.COMMON;

namespace AECLOGIC.ERP.HMS
{


    public partial class BillsApproval : AECLOGIC.ERP.COMMON.WebFormMaster
    {
        #region LoadEvents
        int EmpId = 0;
        int mid = 0; string menuname; string menuid;
        int stateid; int StatusId;

        DataAccessLayer.BillsApproval objBillsApproval = new DataAccessLayer.BillsApproval();

        public static DataSet dsvdr = new DataSet();
        public static ArrayList alVdr = new ArrayList();

        public static DataSet dswo = new DataSet();
        public static ArrayList alWo = new ArrayList();

        public static DataSet dsws = new DataSet();
        public static ArrayList alWS = new ArrayList();
        DataTable dtws = new DataTable(); DataTable dtwo = new DataTable(); DataTable dtvdr = new DataTable();
        protected override void OnInit(EventArgs e)
        {
            ModuleID = 1;
            base.OnInit(e);
            pgGoods.FirstClick += new Paging.PageFirst(TasksPaging_FirstClick);
            pgGoods.PreviousClick += new Paging.PagePrevious(TasksPaging_FirstClick);
            pgGoods.NextClick += new Paging.PageNext(TasksPaging_FirstClick);
            pgGoods.LastClick += new Paging.PageLast(TasksPaging_FirstClick);
            pgGoods.ChangeClick += new Paging.PageChange(TasksPaging_FirstClick);
            pgGoods.ShowRowsClick += new Paging.ShowRowsChange(TasksPaging_ShowRowsClick);
            pgGoods.CurrentPage = 1;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            {
                 dswo = SQLDBUtil.ExecuteDataset("MMS_DDL_WorkOrder");
                DataRow dr = dswo.Tables[0].NewRow();
                dr["ID"] = 0;
                dr["Name"] = "None"; dswo.Tables[0].Rows.InsertAt(dr, 0);
                dswo.AcceptChanges();

                dtwo = dswo.Tables[0];

                dsvdr = SQLDBUtil.ExecuteDataset("MMS_DDL_VendorDetails");

                dtvdr = dsvdr.Tables[0];


                dsws = SQLDBUtil.ExecuteDataset("MMS_DDL_WorkSite");
                dtws = dsws.Tables[0];

                if (Request.QueryString.Count > 0)
                {
                    stateid = Convert.ToInt32(Request.QueryString["state"]);//.Trim();
                    if (stateid == 2)
                    {
                        lblStatus.Visible = true; ddlStatus.Visible = true;
                    }
                    else
                    {
                        lblStatus.Visible = false; ddlStatus.Visible = false;
                    }
                }
                else
                { lblStatus.Visible = false; ddlStatus.Visible = false; }

                this.Page.MaintainScrollPositionOnPostBack = true;
                if (!IsPostBack)
                {
                    GetParentMenuId();
                    SetUpScreen();
                }

                if ((bool)ViewState["ViewAll"])
                {
                    gvAutoBilling.Columns[0].Visible = true;
                    btnApprove.Visible = true;
                }


                else
                {
                    btnApprove.Visible = false;
                    gvAutoBilling.Columns[0].Visible = false;
                }
                if ((bool)ViewState["Editable"])
                    gvAutoBilling.Columns[10].Visible = true;
                else
                    gvAutoBilling.Columns[10].Visible = false;



                selected();

            }
            btnUpdateQtys.Visible = false;
        }




        #endregion LoadEvents
        #region GridPaging

        void TasksPaging_ShowRowsClick(object sender, EventArgs e)
        {
            pgGoods.CurrentPage = 1;
            BindPager();
        }

        void TasksPaging_FirstClick(object sender, EventArgs e)
        {

            BindPager();
        }


        void BindPager()
        {
            objBillsApproval.PageSize = pgGoods.CurrentPage;
            objBillsApproval.CurrentPage = pgGoods.ShowRows;
            BindDataGrid(objBillsApproval);
        }

        #endregion GridPaging


        private void BindDataGrid(DataAccessLayer.BillsApproval objCommon)
        {
            StatusId = Convert.ToInt32(ddlStatus.SelectedValue);
            int ModuleId = 7;
            objCommon.PageSize = pgGoods.ShowRows; ;
            objCommon.CurrentPage = pgGoods.CurrentPage;

            int WSId;
            if (ddlWorkSites.SelectedIndex != 0)

                WSId = Convert.ToInt32(ddlWorkSites.SelectedValue);
            else
                WSId = 0;

            int VendorID;
            if (ddlVendor.SelectedIndex != 0)

                VendorID = Convert.ToInt32(ddlVendor.SelectedValue);
            else
                VendorID = 0;

            DataSet ds = new DataSet();
            string State = "1";

            try
            {
                if (Request.QueryString.Count > 0)
                    State = Request.QueryString["state"].Trim();
            }
            catch (Exception e)
            {
                throw e;
            }
            switch (State)
            {
                case "1":
                    ds = objCommon.GetMMS_BillsGoodsApproval(objCommon, VendorID,
                                                 txtPONo.Text == String.Empty ? (int?)null : int.Parse(txtPONo.Text),
                                                 txtBillNo.Text == String.Empty ? (int?)null : int.Parse(txtBillNo.Text), ModuleId, WSId);
                    btnApprove.Text = "Approve All";
                    break;
                case "2":
                    ds = objCommon.GetMMS_BillsGoodsApproved(objCommon, VendorID,
                                                txtPONo.Text == String.Empty ? (int?)null : int.Parse(txtPONo.Text),
                                                txtBillNo.Text == String.Empty ? (int?)null : int.Parse(txtBillNo.Text), ModuleId, StatusId, WSId);
                    btnApprove.Text = "Reject All";
                    break;
                case "3":
                    ds = objCommon.GetMMS_BillsGoodsRejected(objCommon, VendorID,
                                              txtPONo.Text == String.Empty ? (int?)null : int.Parse(txtPONo.Text),
                                              txtBillNo.Text == String.Empty ? (int?)null : int.Parse(txtBillNo.Text), ModuleId, WSId);
                    btnApprove.Text = "Confirm All";
                    break;
                default:
                    break;
            }
            if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
            {
                gvAutoBilling.DataSource = ds;
            }
            gvAutoBilling.DataBind();
            pgGoods.Visible = true;
            btnApprove.Visible = true;
            pgGoods.Bind(objCommon.CurrentPage, objCommon.TotalPages, objCommon.NoofRecords, objCommon.PageSize);
        }

        #region OnClick


        protected void lnkGoods_Click(object sender, EventArgs e)
        {
            Response.Redirect("BillsApproval.aspx#");
        }


        protected void btClear_Click(object sender, EventArgs e)
        {
            txtPONo.Text = "";
            txtBillNo.Text = "";
        }


        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindDataGrid(objBillsApproval);
        }


        #endregion OnClick

        public string PONavigateUrl(string POID)
        {
            int val = 1;
            bool chk = true;
            return "javascript:return window.open('ProPurchaseOrderPrint.aspx?id=" + POID + "&PON=" + val + "&tot=" + chk + "' , '_blank')";
        }

        protected void GVAutoBilling_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int BillNo = 0;
            BillNo = Convert.ToInt32(e.CommandArgument);
            EmpId =  Convert.ToInt32(Session["UserId"]);
            int CompanyId = 10;
            if (e.CommandName == "Edt")
            {
                string strGDNIdS = e.CommandArgument.ToString().Replace("&nbsp;", "");
                DataSet dsMMS_GRNeDditUpdate = objBillsApproval.MMS_Bills_GDN_Update(int.Parse(strGDNIdS));
                if (dsMMS_GRNeDditUpdate != null && dsMMS_GRNeDditUpdate.Tables.Count != 0 && dsMMS_GRNeDditUpdate.Tables[0].Rows.Count > 0)
                {
                    gvEditQtys.DataSource = dsMMS_GRNeDditUpdate;
                    gvEditQtys.DataBind();
                    btnApprove.Visible = false;
                    gvEditQtys.Visible = true;
                    btnUpdateQtys.Visible = true;
                }
            }
            else if (e.CommandName == "Approve")
            {
                objBillsApproval.MMS_ApproveBill(BillNo, CompanyId, EmpId); btnApprove.Visible = true;
            }
            else if (e.CommandName == "Reject")
            {
                btnApprove.Visible = true;
                objBillsApproval.MMS_ChangeBillStatus(BillNo, 0, EmpId);
            }
            else if (e.CommandName == "Confirm")
            {
                btnApprove.Visible = true;
                objBillsApproval.MMS_ApproveBill(BillNo, CompanyId, EmpId);// btnApprove.Visible = true;
            }
            BindPager();
        }

        protected void gvAutoBilling_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Button lnk = (Button)e.Row.Cells[11].Controls[1];
                try
                {
                    switch (Request.QueryString["state"].Trim())
                    {
                        case "1":
                            lnk.Text = "Approve";
                            lnk.CommandName = "Approve";
                            // lnkedit.Visible = true;
                            break;
                        case "2":
                            lnk.Text = "Reject";
                            lnk.CommandName = "Reject";
                            // lnkedit.Visible = false;

                            //if (StatusId == 2)
                            //    lnk.Text = "Confirmed";
                            break;
                        case "3":
                            lnk.Text = "Confirm";
                            lnk.CommandName = "Confirm";
                            //lnkedit.Visible = true;

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

        private void SetUpScreen()
        {
            objBillsApproval.CurrentPage = pgGoods.CurrentPage;
            objBillsApproval.PageSize = 10;// Config.ShowRows;
            pgGoods.Visible = false;
            btnApprove.Visible = false;
            FillDropDownVendor();
            FillWorkSiteDropDown();

            BindDataGrid(objBillsApproval);

        }



        public void selected()
        {
            string URL = Request.Url.Segments[Request.Url.Segments.Length - 1] + Request.Url.Query;
            if (URL == "BillsApproval.aspx?state=1" || URL == "BillsApproval.aspx")
            {
                lnkApprovals.CssClass = "lnkselected";
                this.Title += "-> Corroborate";

            }
            else if (URL == "BillsApproval.aspx?state=2")
            {
                lnkApproved.CssClass = "lnkselected";
                lnkApprovals.CssClass = "";
                this.Title += "-> Approved";
            }
            else if (URL == "BillsApproval.aspx?state=3")
            {
                lnkRejected.CssClass = "lnkselected";
                lnkApprovals.CssClass = "";
                this.Title += "-> Rejected";
            }
        }

        public int GetParentMenuId()
        {

            int Id;
            if (Session["Guest"] != null)
                Id = 2;
            else
                Id = 1;
            string URL;
            if (Request.QueryString.Count > 0)
            {
                URL = Request.Url.Segments[Request.Url.Segments.Length - 1];
            }
            else
                URL = Request.Url.Segments[Request.Url.Segments.Length - 1] + Request.Url.Query;

            Session["CurrentPage"] = URL;

            int RoleId = Convert.ToInt32(Session["RoleId"].ToString());
            int ModuleId = ModuleID;;

            DataSet ds = AttendanceDAC.GetAllowed(RoleId, ModuleId, URL);//, Id);
            int MenuId = 0;
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ViewState["Editable"] = (bool)ds.Tables[0].Rows[0]["Editable"];
                ViewState["ViewAll"] = (bool)ds.Tables[0].Rows[0]["ViewAll"];
                MenuId = Convert.ToInt32(ds.Tables[0].Rows[0]["Under"]);
            }
            return MenuId;
        }

        protected void btnApprove_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow gvr in gvAutoBilling.Rows)
            {
                CheckBox chk = new CheckBox();
                Label lbl = new Label();
                lbl = (Label)gvr.Cells[12].FindControl("lblBillNo");
                chk = (CheckBox)gvr.Cells[0].FindControl("chkBill");
                if (chk.Checked)
                {
                    int BillNo = 0;
                    BillNo = Convert.ToInt32(lbl.Text);
                    EmpId =  Convert.ToInt32(Session["UserId"]);
                    int CompanyId = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["CompanyID"].ToString());

                    if (stateid == 2)//for Arroved
                    {
                        objBillsApproval.MMS_ChangeBillStatus(BillNo, 0, EmpId);
                    }
                    else if (stateid == 3)//for reject
                    {
                        objBillsApproval.MMS_ChangeBillStatus(BillNo, 2, EmpId);
                    }
                    else                 //for Approval
                    {
                        objBillsApproval.MMS_ApproveBill(BillNo, CompanyId, EmpId);
                    }
                }
            }
            BindPager();
        }
        protected void btnUpdateQtys_OnClick(object sender, EventArgs e)
        {
            foreach (GridViewRow gvRow in gvEditQtys.Rows)
            {
                int WO = 0;
                int WOVendor = 0;
                int GDNID = int.Parse(gvEditQtys.DataKeys[gvRow.RowIndex][0].ToString());
                int GDNITEMID = int.Parse(gvEditQtys.DataKeys[gvRow.RowIndex][1].ToString());
                DropDownList ddlWS = new DropDownList();
                ddlWS = (DropDownList)gvRow.FindControl("ddlWS");
                DropDownList ddlvdr = new DropDownList();
                ddlvdr = (DropDownList)gvRow.FindControl("ddlVDR");
                DropDownList ddlWO = new DropDownList();
                ddlWO = (DropDownList)gvRow.FindControl("ddlWO");
                TextBox dispDate = (TextBox)gvRow.Cells[5].Controls[1];
                TextBox dispQty = (TextBox)gvRow.Cells[6].Controls[1];
                TextBox inQty = (TextBox)gvRow.Cells[7].Controls[1];
                TextBox acptQty = (TextBox)gvRow.Cells[8].Controls[1];
                TextBox TS = (TextBox)gvRow.Cells[9].Controls[1];
                TextBox Dist = (TextBox)gvRow.Cells[10].Controls[1];
                int distance;
                if (Dist.Text != "")
                    distance = Convert.ToInt32(Dist.Text);
                else
                    distance = 0;

                if (ddlWO.SelectedIndex != 0)
                {
                    WO = Convert.ToInt32(ddlWO.SelectedValue); //ddlWO.SelectedValue == "0" ? 0 : int.Parse(ddlWO.SelectedValue);
                    WOVendor = objBillsApproval.GetVendorId(WO);
                }

                objBillsApproval.MMS_Bills_Upd_GdnItems(GDNID, GDNITEMID, CODEUtility.ConvertToDate(dispDate.Text, DateFormat.DayMonthYear), decimal.Parse(dispQty.Text), decimal.Parse(inQty.Text), decimal.Parse(acptQty.Text), int.Parse(TS.Text), Convert.ToInt32(ddlWS.SelectedValue), Convert.ToInt32(ddlvdr.SelectedValue), WO, WOVendor, distance);
            }
            AlertMsg.MsgBox(Page, "Updated Sucessfully.");
            btnUpdateQtys.Visible = false;
            gvEditQtys.Visible = false;
            btnApprove.Visible = true;

        }
        private void FillDropDownVendor()
        {
            DataSet ds = SqlHelper.ExecuteDataset("MMS_DDL_ServiceBillsVendors");
            ddlVendor.DataValueField = ds.Tables[0].Columns[1].ToString();
            ddlVendor.DataTextField = ds.Tables[0].Columns[0].ToString();
            ddlVendor.Items.Add(new ListItem("Select Vendor ", "-1"));
            ddlVendor.DataSource = ds;// LookUp.PopulateEntityDropDown(LookUp.EntityRelationships.ServiceVendor);
            ddlVendor.DataBind();

        }
        private void FillWorkSiteDropDown()
        {
            DataSet ds = SqlHelper.ExecuteDataset("MMS_DDL_WorkSite");
            ddlWorkSites.DataValueField = ds.Tables[0].Columns[0].ToString();
            ddlWorkSites.DataTextField = ds.Tables[0].Columns[1].ToString(); ddlWorkSites.Items.Add(new ListItem(" Select WorkSite", "-1"));
            ddlWorkSites.DataSource = ds;// LookUp.PopulateEntityDropDown(LookUp.EntityRelationships.WorkSite);
            ddlWorkSites.DataBind();
        }
        public int GetWOIndex(string WOId)
        {
            return dtwo.Rows.IndexOf(dtwo.Select("ID='" + WOId + "'")[0]);
        }



        public int GetWSIndex(string WSId)
        {

            return dtws.Rows.IndexOf(dtws.Select("ID='" + WSId + "'")[0]);
        }



        public int GetVdrIndex(string vdrId)
        {

            return dtvdr.Rows.IndexOf(dtvdr.Select("ID='" + vdrId + "'")[0]);

        }
        public DataTable BindWO()
        {

            return dtwo;
        }
        public DataTable BindVendor()
        {

            return dtvdr;
        }
        public DataTable BindWS()
        {
            return dtws;
        }

    }

}