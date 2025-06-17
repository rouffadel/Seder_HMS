using Aeclogic.Common.DAL;
using AECLOGIC.ERP.COMMON;
using AECLOGIC.ERP.HMS.HRClasses;
using AECLOGIC.HMS.BLL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using AECLOGIC.ERP.HMS;
using System.Web.UI;

namespace AECLOGIC.ERP.HMSV1
{
    public partial class BussinesstripHelpOthersV1 : AECLOGIC.ERP.COMMON.WebFormMaster
    {
        #region Variables
        static int mid;
        static int ExpEmpid;
        static int cid;
        static int editstatus;
        static int deletestatus;
        static int ERIDNO;
        HRCommon objHrCommon = new HRCommon();
        #endregion Variables
        #region Events
        protected override void OnInit(EventArgs e)
        {
            ModuleID = 1;
            base.OnInit(e);
            EmpReimbursementPendingRejPaging.FirstClick += new Paging.PageFirst(EmpReimbursementPendingRejPaging_FirstClick);
            EmpReimbursementPendingRejPaging.PreviousClick += new Paging.PagePrevious(EmpReimbursementPendingRejPaging_FirstClick);
            EmpReimbursementPendingRejPaging.NextClick += new Paging.PageNext(EmpReimbursementPendingRejPaging_FirstClick);
            EmpReimbursementPendingRejPaging.LastClick += new Paging.PageLast(EmpReimbursementPendingRejPaging_FirstClick);
            EmpReimbursementPendingRejPaging.ChangeClick += new Paging.PageChange(EmpReimbursementPendingRejPaging_FirstClick);
            EmpReimbursementPendingRejPaging.ShowRowsClick += new Paging.ShowRowsChange(EmpReimbursementPendingRejPaging_ShowRowsClick);
            EmpReimbursementPendingRejPaging.CurrentPage = 1;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            Label1.Text = String.Empty;
            cid = Convert.ToInt32(Session["CompanyID"]);
            Ajax.Utility.RegisterTypeForAjax(typeof(AjaxDAL));
            if (!IsPostBack)
            {
                editstatus = 0;
                hdn_ID.Value = "0";
                tblExpenses.Visible = false;
                gvChildDetails.Visible = false;
                btnSubmit.Visible = false;
                if (Request.QueryString.Count > 0)
                {
                    tblView.Visible = true;
                    tblAdd.Visible = false;
                    BindGrid();
                }
                else
                {
                    tblView.Visible = false;
                    tblAdd.Visible = true;
                    // BindItem();
                }
                BindLocations(ddlfromCity);
                BindLocations(ddlToCity);
                this.Bindworksite();
                FIllObject.FillDropDown(ref ddlBookingClass, "get_T_HMS_BookingClass");
                FIllObject.FillDropDown(ref ddlTravelMode, "sh_ddlBindTravelMode");
            }
        }
        public void Bindworksite()
        {
            FIllObject.FillDropDown(ref this.ddlWorksite, "sh_ddlBindToWorksite");
        }
        protected void chkApproval_CheckedChanged(object sender, EventArgs e)
        {
        }
        protected void chkRSelect_CheckedChanged(object sender, EventArgs e)
        {
        }
        void EmpReimbursementPendingRejPaging_ShowRowsClick(object sender, EventArgs e)
        {
            EmpReimbursementPendingRejPaging.CurrentPage = 1;
            BindGrid();
        }
        void EmpReimbursementPendingRejPaging_FirstClick(object sender, EventArgs e)
        {
            BindGrid();
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (ddlEmp_hid.Value != "")
            {
                DataSet dsBussinessDeta = new DataSet("TBussinessDataSet");
                DataTable dtTDT = new DataTable("BussinessTable");
                dtTDT.Columns.Add(new DataColumn("EmpID", typeof(System.Int32)));
                dtTDT.Columns.Add(new DataColumn("EmpName", typeof(System.String)));
                dtTDT.Columns.Add(new DataColumn("TravelmodeID", typeof(System.Int32)));
                dtTDT.Columns.Add(new DataColumn("Travelmode", typeof(System.String)));
                dtTDT.Columns.Add(new DataColumn("BookingClassID", typeof(System.Int32)));
                dtTDT.Columns.Add(new DataColumn("BookingClass", typeof(System.String)));
                dtTDT.Columns.Add(new DataColumn("FromCity", typeof(System.Int32)));
                dtTDT.Columns.Add(new DataColumn("FCity", typeof(System.String)));
                dtTDT.Columns.Add(new DataColumn("ToCity", typeof(System.Int32)));
                dtTDT.Columns.Add(new DataColumn("TCity", typeof(System.String)));
                dtTDT.Columns.Add(new DataColumn("FromDate", typeof(System.String)));
                dtTDT.Columns.Add(new DataColumn("ToDate", typeof(System.String)));
                dtTDT.Columns.Add(new DataColumn("Remarks", typeof(System.String)));
                dtTDT.Columns.Add(new DataColumn("filetype", typeof(System.String)));
                dtTDT.Columns.Add(new DataColumn("filePath", typeof(System.String)));
                dtTDT.Columns.Add(new DataColumn("WorksiteID", typeof(System.String)));
                dtTDT.Columns.Add(new DataColumn("Worksite", typeof(System.String)));
                dsBussinessDeta.Tables.Add(dtTDT);
                DataRow dr;
                if (gvRemiItems.Rows.Count > 0)
                {
                    foreach (GridViewRow gvRow in gvRemiItems.Rows)
                    {
                        Label lblEmp = (Label)gvRow.FindControl("lblEmpID");
                        Label lblEmpName = (Label)gvRow.FindControl("lblEmpName");
                        Label lblTravelModeID = (Label)gvRow.FindControl("lblTravelModeID");
                        Label lblTravelMode = (Label)gvRow.FindControl("lblTravelMode");
                        Label lblBookingclassID = (Label)gvRow.FindControl("lblBookingclassID");
                        Label lblBookingclass = (Label)gvRow.FindControl("lblBookingclass");
                        Label lblFromCity = (Label)gvRow.FindControl("lblFromCity");
                        Label lblFCity = (Label)gvRow.FindControl("lblFCity");
                        Label lblToCity = (Label)gvRow.FindControl("lblToCity");
                        Label lblTCity = (Label)gvRow.FindControl("lblTCity");
                        Label lblFromDate = (Label)gvRow.FindControl("lblFromDate");
                        Label lblToDate = (Label)gvRow.FindControl("lblToDate");
                        Label lblRemarks = (Label)gvRow.FindControl("lblRemarks");
                        Label lblfiletype = (Label)gvRow.FindControl("lblfiletype");
                        Label lblfilePath = (Label)gvRow.FindControl("lblFilePath");
                        Label lblworksiteid = (Label)gvRow.FindControl("lblworksiteid");
                        Label lblworksite = (Label)gvRow.FindControl("lblworksite");
                        {
                            if (ddlEmp_hid.Value != lblEmp.Text.Trim())
                            {
                                dr = dtTDT.NewRow();
                                dr["EmpID"] = lblEmp.Text;
                                dr["EmpName"] = lblEmpName.Text.Trim();
                                dr["TravelmodeID"] = lblTravelModeID.Text;
                                dr["Travelmode"] = lblTravelMode.Text;
                                dr["BookingClassID"] = lblBookingclassID.Text;
                                dr["BookingClass"] = lblBookingclass.Text;
                                dr["FromCity"] = lblFromCity.Text;
                                dr["FCity"] = lblFCity.Text;
                                dr["ToCity"] = lblToCity.Text;
                                dr["TCity"] = lblTCity.Text;
                                dr["FromDate"] = lblFromDate.Text.Trim();
                                dr["ToDate"] = lblToDate.Text.Trim();
                                dr["Remarks"] = lblRemarks.Text;
                                dr["filetype"] = lblfiletype.Text;
                                dr["filePath"] = lblfilePath.Text;
                                dr["worksiteid"] = lblworksiteid.Text;
                                dr["worksite"] = lblworksite.Text;
                                dtTDT.Rows.Add(dr);
                                dsBussinessDeta.AcceptChanges();
                            }
                            else
                            {
                                AlertMsg.MsgBox(Page, "Employee Already Selected", AlertMsg.MessageType.Warning);
                                return;
                            }
                        }
                    }
                }
                dr = dtTDT.NewRow();
                dr["EmpID"] = ddlEmp_hid.Value;
                dr["EmpName"] = TxtEmp.Text.Trim();
                dr["TravelmodeID"] = ddlTravelMode.SelectedValue;
                dr["Travelmode"] = ddlTravelMode.SelectedItem.Text;
                dr["BookingClassID"] = ddlBookingClass.SelectedValue;
                dr["BookingClass"] = ddlBookingClass.SelectedItem.Text;
                dr["FromCity"] = ddlfromCity.SelectedValue;
                dr["FCity"] = ddlfromCity.SelectedItem.Text;
                dr["ToCity"] = ddlToCity.SelectedValue;
                dr["TCity"] = ddlToCity.SelectedItem.Text;
                dr["FromDate"] = txtFrom.Text.Trim();
                dr["ToDate"] = txtTo.Text.Trim();
                dr["Remarks"] = txtRemarks.Text;
                string filenames = "", ext = "";
                if (fudDocument.HasFile)
                {
                    filenames = fudDocument.PostedFile.FileName;
                    //ViewState["filename"] = filename;
                    ext = filenames.Split('.')[filenames.Split('.').Length - 1];
                    //ViewState["ext"] = ext;
                    // filenames = Server.MapPath("\\BussinessTrip\\" + ddlEmp_hid.Value + "." + ext);
                    DataSet ds = SqlHelper.ExecuteDataset("sp_Count_BussinesstripChild");
                    int id = Convert.ToInt32(ds.Tables[0].Rows[0]["Cid"].ToString());
                    filenames = Server.MapPath("\\BussinessTrip\\" + id + "." + ext);
                    fudDocument.PostedFile.SaveAs(filenames);
                }
                dr["filetype"] = ext;
                dr["filePath"] = filenames;
                dr["worksiteid"] = ddlWorksite.SelectedValue;
                dr["worksite"] = ddlWorksite.SelectedItem.Text;
                dtTDT.Rows.Add(dr);
                dsBussinessDeta.AcceptChanges();
                ViewState["BussinessTrip"] = dsBussinessDeta;
                gvRemiItems.DataSource = dsBussinessDeta;
                gvRemiItems.DataBind();
                gvRemiItems.Visible = true;
                btnSubmit.Visible = true;
                clear();
                editstatus = 0;
            }
            else
            {
                AlertMsg.MsgBox(Page, "Select Employee", AlertMsg.MessageType.Warning);
            }
        }
        protected void btnFilter_Click(object sender, EventArgs e)
        {
            gvChildDetails.DataSource = null;
            gvChildDetails.DataBind();
            gvView.DataSource = null;
            gvView.DataBind();
            gvChildDetails.Visible = false;
            tblExpenses.Visible = false;
            BindGrid();
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet dsBussinessDeta = new DataSet("TBussinessDataSet");
                DataTable dtTDT = new DataTable("BussinessTable");
                dtTDT.Columns.Add(new DataColumn("EmpID", typeof(System.Int32)));
                dtTDT.Columns.Add(new DataColumn("EmpName", typeof(System.String)));
                dtTDT.Columns.Add(new DataColumn("TravelmodeID", typeof(System.Int32)));
                dtTDT.Columns.Add(new DataColumn("Travelmode", typeof(System.String)));
                dtTDT.Columns.Add(new DataColumn("BookingClassID", typeof(System.Int32)));
                dtTDT.Columns.Add(new DataColumn("BookingClass", typeof(System.String)));
                dtTDT.Columns.Add(new DataColumn("FromCity", typeof(System.Int32)));
                dtTDT.Columns.Add(new DataColumn("FCity", typeof(System.String)));
                dtTDT.Columns.Add(new DataColumn("ToCity", typeof(System.Int32)));
                dtTDT.Columns.Add(new DataColumn("TCity", typeof(System.String)));
                dtTDT.Columns.Add(new DataColumn("FromDate", typeof(System.String)));
                dtTDT.Columns.Add(new DataColumn("ToDate", typeof(System.String)));
                dtTDT.Columns.Add(new DataColumn("Remarks", typeof(System.String)));
                dtTDT.Columns.Add(new DataColumn("filetype", typeof(System.String)));
                dtTDT.Columns.Add(new DataColumn("filePath", typeof(System.String)));
                dtTDT.Columns.Add(new DataColumn("worksiteid", typeof(System.Int32)));
                dtTDT.Columns.Add(new DataColumn("worksite", typeof(System.String)));
                dsBussinessDeta.Tables.Add(dtTDT);
                DataRow dr;
                if (gvRemiItems.Rows.Count > 0)
                {
                    foreach (GridViewRow gvRow in gvRemiItems.Rows)
                    {
                        Label lblEmp = (Label)gvRow.FindControl("lblEmpID");
                        Label lblEmpName = (Label)gvRow.FindControl("lblEmpName");
                        Label lblTravelModeID = (Label)gvRow.FindControl("lblTravelModeID");
                        Label lblTravelMode = (Label)gvRow.FindControl("lblTravelMode");
                        Label lblBookingclassID = (Label)gvRow.FindControl("lblBookingclassID");
                        Label lblBookingclass = (Label)gvRow.FindControl("lblBookingclass");
                        Label lblFromCity = (Label)gvRow.FindControl("lblFromCity");
                        Label lblFCity = (Label)gvRow.FindControl("lblFCity");
                        Label lblToCity = (Label)gvRow.FindControl("lblToCity");
                        Label lblTCity = (Label)gvRow.FindControl("lblTCity");
                        Label lblFromDate = (Label)gvRow.FindControl("lblFromDate");
                        Label lblToDate = (Label)gvRow.FindControl("lblToDate");
                        Label lblRemarks = (Label)gvRow.FindControl("lblRemarks");
                        Label lblfiletype = (Label)gvRow.FindControl("lblfiletype");
                        Label lblFilePath = (Label)gvRow.FindControl("lblFilePath");
                        Label lblworksiteid = (Label)gvRow.FindControl("lblworksiteid");
                        Label lblworksite = (Label)gvRow.FindControl("lblworksite");
                        dr = dtTDT.NewRow();
                        dr["EmpID"] = lblEmp.Text;
                        dr["EmpName"] = lblEmpName.Text.Trim();
                        dr["TravelmodeID"] = lblTravelModeID.Text;
                        dr["Travelmode"] = lblTravelMode.Text;
                        dr["BookingClassID"] = lblBookingclassID.Text;
                        dr["BookingClass"] = lblBookingclass.Text;
                        dr["FromCity"] = lblFromCity.Text;
                        dr["FCity"] = lblFCity.Text;
                        dr["ToCity"] = lblToCity.Text;
                        dr["TCity"] = lblTCity.Text;
                        dr["FromDate"] = CodeUtilHMS.ConvertToDate_ddMMMyyy(lblFromDate.Text.Trim(), CodeUtilHMS.DateFormat.ddMMMyyyy);
                        dr["ToDate"] = CodeUtilHMS.ConvertToDate_ddMMMyyy(lblToDate.Text.Trim(), CodeUtilHMS.DateFormat.ddMMMyyyy);
                        dr["Remarks"] = lblRemarks.Text;
                        dr["filetype"] = lblfiletype.Text;
                        dr["filePath"] = lblFilePath.Text;
                        dr["worksiteid"] = lblworksiteid.Text;
                        dr["worksite"] = lblworksite.Text;
                        dtTDT.Rows.Add(dr);
                        dsBussinessDeta.AcceptChanges();
                    }
                    SqlParameter[] parms = new SqlParameter[5];
                    parms[0] = new SqlParameter("@BussinessDetails", dsBussinessDeta.GetXml());
                    parms[1] = new SqlParameter("@ID", hdn_ID.Value);
                    parms[2] = new SqlParameter("@Date", DateTime.Now.Date);
                    parms[4] = new SqlParameter("@created", Session["UserId"]);
                    parms[3] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int); parms[3].Direction = ParameterDirection.ReturnValue;
                    int Output = SqlHelper.ExecuteNonQuery("sh_insertbussinesstrip", parms);
                    if (Output > 0) { AlertMsg.MsgBox(Page, "Saved! "); }
                    else if (Output != 1 && Output != 2 && Output != 3) { AlertMsg.MsgBox(Page, "Already Exists!"); }
                    if (Output > 0)
                    {
                        clear();
                        gvRemiItems.DataSource = null;
                        gvRemiItems.DataBind();
                    }
                }
                else
                {
                    AlertMsg.MsgBox(Page, "No Records Found", AlertMsg.MessageType.Warning);
                }
            }
            catch (Exception ex)
            {
                //throw;
            }
        }
        protected void gvRemiItems_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                tblExpenses.Visible = false;
                if (e.CommandName == "Edt")
                {
                    editstatus = 1;
                    int ID = Convert.ToInt32(e.CommandArgument);
                    DataSet ds = (DataSet)ViewState["BussinessTrip"];
                    DataTable dtList = ds.Tables[0];
                    DataRow[] drSelected = null;
                    drSelected = dtList.Select("EmpID='" + e.CommandArgument + "'");
                    ddlEmp_hid.Value = drSelected[0].ItemArray[0].ToString();
                    TxtEmp.Text = drSelected[0].ItemArray[1].ToString();
                    int tid = Convert.ToInt32(drSelected[0].ItemArray[2].ToString());
                    ddlTravelMode.SelectedValue = tid.ToString();
                    int bid = Convert.ToInt32(drSelected[0].ItemArray[4].ToString());
                    ddlBookingClass.SelectedValue = bid.ToString();
                    int fid = Convert.ToInt32(drSelected[0].ItemArray[6].ToString());
                    ddlfromCity.SelectedValue = fid.ToString();
                    int toid = Convert.ToInt32(drSelected[0].ItemArray[8].ToString());
                    ddlToCity.SelectedValue = toid.ToString();
                    txtFrom.Text = drSelected[0].ItemArray[10].ToString();
                    txtTo.Text = drSelected[0].ItemArray[11].ToString();
                    txtRemarks.Text = drSelected[0].ItemArray[12].ToString();
                    int num6 = Convert.ToInt32(drSelected[0].ItemArray[15].ToString());
                    this.ddlWorksite.SelectedValue = num6.ToString();
                }
                if (e.CommandName == "Del")
                {
                    int ID = Convert.ToInt32(e.CommandArgument);
                    if (deletestatus != 1)
                    {
                        DataSet ds = (DataSet)ViewState["BussinessTrip"];
                        DataTable dtList = ds.Tables[0];
                        DataRow[] drSelected = null;
                        drSelected = dtList.Select("EmpID='" + e.CommandArgument + "'");
                        if (drSelected.Length > 0)
                        {
                            dtList.Rows.Remove(drSelected[0]);
                        }
                        dtList.AcceptChanges();
                        ViewState["BussinessTrip"] = dtList;
                        gvRemiItems.DataSource = dtList;
                        gvRemiItems.DataBind();
                    }
                    else
                    {
                        SqlParameter[] parms = new SqlParameter[3];
                        parms[0] = new SqlParameter("@ID", hdn_ID.Value);
                        parms[1] = new SqlParameter("@EMPID", ID);
                        parms[2] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
                        parms[2].Direction = ParameterDirection.ReturnValue;
                        int Output = SqlHelper.ExecuteNonQuery("sh_deleteBusinesstripchild", parms);
                        if ((int)parms[2].Value == 4)
                        {
                            BindGrid();
                            AlertMsg.MsgBox(Page, "Record Rejected!");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }
        protected void gvView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int ID = Convert.ToInt32(e.CommandArgument);
            tblExpenses.Visible = false;
            gvShow.Visible = false;
            if (e.CommandName == "Edt")
            {
                deletestatus = 1;
                tblAdd.Visible = true;
                tblView.Visible = false;
                gvView.Visible = false;
                hdn_ID.Value = ID.ToString();
                SqlParameter[] parms = new SqlParameter[2];
                parms[0] = new SqlParameter("@ID", ID);
                parms[1] = new SqlParameter("@fCase", 2);
                DataSet ds = SqlHelper.ExecuteDataset("SP_th_BussinessTrip_Update_Delete_VIEW_Paging_Select", parms);
                gvRemiItems.DataSource = ds;
                gvRemiItems.DataBind();
                ViewState["BussinessTrip"] = ds;
                gvRemiItems.Visible = true;
                btnSubmit.Visible = true;
            }
            if (e.CommandName == "Apr")
            {
                GridViewRow gvRow = (GridViewRow)((LinkButton)e.CommandSource).Parent.Parent;
                int index = gvRow.RowIndex;
                TextBox txtRemarks = (TextBox)gvView.Rows[gvRow.RowIndex].FindControl("txtRemarks");
                string Remarks = txtRemarks.Text.Trim();

                int status = Convert.ToInt32(Request.QueryString["Apr"].ToString());
                SqlParameter[] parms = new SqlParameter[6];
                parms[0] = new SqlParameter("@ID", ID);
                parms[1] = new SqlParameter("@fCase", 3);
                parms[2] = new SqlParameter("@Status", status);
                parms[3] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
                parms[3].Direction = ParameterDirection.ReturnValue;
                parms[4] = new SqlParameter("@StatusRemarks", Remarks);
                parms[5] = new SqlParameter("@Createdby", Convert.ToInt32(Session["UserId"]));

                int Output = SqlHelper.ExecuteNonQuery("SP_th_BussinessTrip_Update_Delete_VIEW_Paging_Select", parms);
                if ((int)parms[3].Value == 3)
                {
                    BindGrid();
                    AlertMsg.MsgBox(Page, "Record Approved!");
                }
            }
            if (e.CommandName == "Rej")
            {
                GridViewRow gvRow = (GridViewRow)((LinkButton)e.CommandSource).Parent.Parent;
                int index = gvRow.RowIndex;
                TextBox txtRemarks = (TextBox)gvView.Rows[gvRow.RowIndex].FindControl("txtRemarks");
                string Remarks = txtRemarks.Text.Trim();
                if (Remarks != "")
                {
                    SqlParameter[] parms = new SqlParameter[4];
                    parms[0] = new SqlParameter("@ID", ID);
                    parms[1] = new SqlParameter("@fCase", 4);
                    parms[2] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
                    parms[2].Direction = ParameterDirection.ReturnValue;
                    parms[3] = new SqlParameter("@Remarks", Remarks);
                    int Output = SqlHelper.ExecuteNonQuery("SP_th_BussinessTrip_Update_Delete_VIEW_Paging_Select", parms);
                    //if ((int)parms[2].Value == 4)
                    //{
                    BindGrid();
                    AlertMsg.MsgBox(Page, "Record Rejected!");
                    //}
                }
                else
                {
                    AlertMsg.MsgBox(Page, "Enter Remarks!", AlertMsg.MessageType.Warning);
                }
            }
            if (e.CommandName == "View")
            {
                SqlParameter[] parms = new SqlParameter[2];
                parms[0] = new SqlParameter("@ID", ID);
                parms[1] = new SqlParameter("@fCase", 5);
                DataSet dss = SqlHelper.ExecuteDataset("SP_th_BussinessTrip_Update_Delete_VIEW_Paging_Select", parms);
                gvChildDetails.Visible = true;
                gvChildDetails.DataSource = dss;
                gvChildDetails.DataBind();
            }
            if (e.CommandName == "BussinessView")
            {
                string url = "ViewBusinessTrip.aspx?BID=" + ID;
                string fullURL = "window.open('" + url + "', '_blank' );";
                ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", fullURL, true);
            }
            if (e.CommandName == "ACC")
            {
                GridViewRow gvRow = (GridViewRow)((LinkButton)e.CommandSource).Parent.Parent;
                int index = gvRow.RowIndex;
                TextBox txtRemarks = (TextBox)gvView.Rows[gvRow.RowIndex].FindControl("txtRemarks");
                string Remarks = txtRemarks.Text.Trim();

                SqlParameter[] parms = new SqlParameter[4];
                parms[0] = new SqlParameter("@mid", ID);
                parms[1] = new SqlParameter("@userid", Convert.ToInt32(Session["UserId"]));
                parms[2] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
                parms[2].Direction = ParameterDirection.ReturnValue;
                parms[3] = new SqlParameter("@StatusRemarks", Remarks);
                SqlHelper.ExecuteNonQuery("sh_BussinesstripAccPosting", parms);
                int Output = Convert.ToInt32(parms[2].Value);
                BindGrid();
                AlertMsg.MsgBox(Page, "Account posted!");
            }
        }
        protected void gvView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    if (Request.QueryString.Count > 0)
                    {
                        LinkButton lnkView = (LinkButton)e.Row.FindControl("lnkView");
                        LinkButton lnkEdit = (LinkButton)e.Row.FindControl("lnkEdit");
                        Label lblEStatus = (Label)e.Row.FindControl("lblEStatus");
                        LinkButton lnkApprove = (LinkButton)e.Row.FindControl("lnkApprove");
                        LinkButton lnkACC = (LinkButton)e.Row.FindControl("lnkACC");
                        LinkButton lnkReject = (LinkButton)e.Row.FindControl("lnkReject");
                        lnkACC.Visible = false;
                        if (Request.QueryString["key"] == "0")
                        {
                            lnkReject.Visible = false;
                            lnkApprove.Visible = false;
                            lnkEdit.Visible = false;
                            lnkACC.Visible = false;
                        }
                        if (Request.QueryString["key"] == "3")
                        {
                            if (lblEStatus.Text != "0")
                            {
                                lnkView.Text = "Add Allowance";
                                lnkApprove.Enabled = false;
                                lnkView.Enabled = true;
                            }
                            else
                            {
                                lnkReject.Visible = false;
                                lnkApprove.Enabled = true;
                                lnkEdit.Visible = false;
                            }
                        }
                        else
                        {
                            lnkView.Text = "View";
                        }
                        if (Request.QueryString["key"] == "4")
                        {
                            lnkACC.Visible = true;
                            lnkApprove.Visible = false;
                            lnkEdit.Visible = false;
                        }
                        if (Request.QueryString["key"] == "5")
                        {
                            lnkACC.Visible = false;
                            lnkApprove.Visible = false;
                            lnkEdit.Visible = false;
                            lnkReject.Visible = false;
                        }

                    }
                }
            }
            catch (Exception Ex)
            {
                //report error
            }
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
        protected void gvRemiItems_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
        }
        protected void txtUnitrete_TextChanged(object sender, EventArgs e)
        {
            CalculateAmt();
        }
        protected void txtQuantity_TextChanged(object sender, EventArgs e)
        {
            CalculateAmt();
        }
        protected void gvChildDetails_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Exp")
            {
                gvShow.Visible = false;
                gvExpenses.DataSource = null;
                gvExpenses.DataBind();
                int ID = Convert.ToInt32(e.CommandArgument);
                mid = ID;
                GridViewRow gvRow = (GridViewRow)((LinkButton)e.CommandSource).Parent.Parent;
                int index = gvRow.RowIndex;
                Label lblEmpID = (Label)gvChildDetails.Rows[gvRow.RowIndex].FindControl("lblEmpID");
                ExpEmpid = Convert.ToInt32(lblEmpID.Text);
                tblExpenses.Visible = true;
                BindItem();
                DataTable dtReimburseList = new DataTable();
                dtReimburseList.Columns.Add(new DataColumn("ID", typeof(System.Int32)));
                dtReimburseList.Columns.Add(new DataColumn("RItemID", typeof(System.Int32)));
                dtReimburseList.Columns.Add(new DataColumn("RItem", typeof(System.String)));
                dtReimburseList.Columns.Add(new DataColumn("EmpID", typeof(System.String)));
                dtReimburseList.Columns.Add(new DataColumn("AUID", typeof(System.String)));
                dtReimburseList.Columns.Add(new DataColumn("Purpose", typeof(System.String)));
                dtReimburseList.Columns.Add(new DataColumn("Qty", typeof(System.Double)));
                dtReimburseList.Columns.Add(new DataColumn("UnitRate", typeof(System.Double)));
                dtReimburseList.Columns.Add(new DataColumn("Amount", typeof(System.Double)));
                dtReimburseList.Columns.Add(new DataColumn("DateOfSpent", typeof(System.String)));
                dtReimburseList.Columns.Add(new DataColumn("Proof", typeof(System.String)));
                dtReimburseList.Columns.Add(new DataColumn("ERID", typeof(System.Int32)));
                ViewState["ReimItems"] = dtReimburseList;
                dtReimburseList = (DataTable)ViewState["ReimItems"];
                btnExpenses.Visible = false;
                DataSet dsAU = AttendanceDAC.GetAu();
                DataRow dr = dsAU.Tables[0].NewRow();
                dr["Au_Id"] = 0;
                dr["Au_Name"] = "---Select---";
                dsAU.Tables[0].Rows.InsertAt(dr, 0);
                dsAU.AcceptChanges();
                ArrayList alUnitIndexes = new ArrayList();
                foreach (DataRow row in dsAU.Tables[0].Rows)
                {
                    alUnitIndexes.Add(row["Au_Id"].ToString().Trim());
                }
                ViewState["alUnitIndexes"] = alUnitIndexes;
                ViewState["dsAU"] = dsAU;
            }
            if (e.CommandName == "Det")
            {
                int ID = Convert.ToInt32(e.CommandArgument);
                SqlParameter[] parms = new SqlParameter[3];
                parms[0] = new SqlParameter("@ID", ID);
                parms[1] = new SqlParameter("@fcase", 6);
                parms[2] = new SqlParameter("@TStatus", 1);
                DataSet ds = SqlHelper.ExecuteDataset("SP_th_BussinessTrip_Update_Delete_VIEW_Paging_Select", parms);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    gvShow.DataSource = ds;
                    gvShow.DataBind();
                    gvShow.Visible = true;
                }
                else
                {
                    gvShow.Visible = false;
                }
            }

            if (e.CommandName == "Edt")
            {
                editstatus = 1;
                gvShow.Visible = false;
                int id = Convert.ToInt32(e.CommandArgument);
                GridViewRow gvRow = (GridViewRow)((LinkButton)e.CommandSource).Parent.Parent;
                int index = gvRow.RowIndex;
                Label lblEmpID = (Label)gvChildDetails.Rows[gvRow.RowIndex].FindControl("lblEmpID");
                ExpEmpid = Convert.ToInt32(lblEmpID.Text);
                ERIDNO = id;
                tblExpenses.Visible = true;
                BindItem();
                DataSet dsAU = AttendanceDAC.GetAu();
                DataRow dr = dsAU.Tables[0].NewRow();
                dr["Au_Id"] = 0;
                dr["Au_Name"] = "---Select---";
                dsAU.Tables[0].Rows.InsertAt(dr, 0);
                dsAU.AcceptChanges();
                ArrayList alUnitIndexes = new ArrayList();
                foreach (DataRow row in dsAU.Tables[0].Rows)
                {
                    alUnitIndexes.Add(row["Au_Id"].ToString().Trim());
                }
                ViewState["alUnitIndexes"] = alUnitIndexes;
                ViewState["dsAU"] = dsAU;
                SqlParameter[] parms = new SqlParameter[2];
                parms[0] = new SqlParameter("@ID", id);
                parms[1] = new SqlParameter("@fcase", 6);
                DataSet ds = SqlHelper.ExecuteDataset("SP_th_BussinessTrip_Update_Delete_VIEW_Paging_Select", parms);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    gvExpenses.DataSource = ds;
                    gvExpenses.DataBind();
                    gvExpenses.Visible = true;
                }
                DataTable dt = (DataTable)ds.Tables[0];
                ViewState["ReimItems"] = dt;
            }
        }
        protected void gvChildDetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (Request.QueryString.Count > 0)
                {
                    LinkButton lnkExpenses = (LinkButton)e.Row.FindControl("lnkExpenses");
                    Label lblTransid = (Label)e.Row.FindControl("lblTransid");
                    LinkButton lnkDetails = (LinkButton)e.Row.FindControl("lnkDetails");
                    LinkButton lnkEdit = (LinkButton)e.Row.FindControl("lnkEdit");
                    Label lblERID = (Label)e.Row.FindControl("lblERID");
                    lnkExpenses.Visible = false;
                    if (Request.QueryString["key"] == "0")
                    {
                        if (lblERID.Text != "0")
                        {
                            lnkDetails.Visible = true;
                            lnkEdit.Visible = false;
                            lnkExpenses.Visible = false;
                        }
                        else
                        {
                            lnkDetails.Visible = false;
                            lnkExpenses.Visible = false;
                            lnkEdit.Visible = false;
                        }
                    }
                    if (Request.QueryString["key"] == "3")
                    {
                        if (lblERID.Text != "0")
                        {
                            lnkDetails.Visible = true;
                            lnkEdit.Visible = true;
                            lnkExpenses.Visible = false;
                        }
                        else
                        {
                            lnkExpenses.Visible = true;
                            lnkDetails.Visible = false;
                            lnkEdit.Visible = false;
                        }
                    }
                    if (Request.QueryString["key"] == "4")
                    {
                        lnkDetails.Visible = true;
                        lnkExpenses.Visible = false;
                        lnkEdit.Visible = false;
                    }
                    if (Request.QueryString["key"] == "5")
                    {
                        lnkDetails.Visible = true;
                        lblTransid.Visible = true;
                        lnkEdit.Visible = false;
                        lnkExpenses.Visible = false;
                    }

                }
            }
        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            int Id = 0;
            int[] Secarrary = lstItems.GetSelectedIndices();
            if (Secarrary.Length > 0)
            {
                AttendanceDAC ADAC = new AttendanceDAC();
                DataTable dtReimburseList = new DataTable();
                DataRow dtRow;
                dtReimburseList = (DataTable)ViewState["ReimItems"];
                ListItem item = null;
                string EmpID = Convert.ToInt32(ExpEmpid).ToString();
                foreach (int indexItem in lstItems.GetSelectedIndices())
                {
                    item = lstItems.Items[indexItem];
                    dtRow = dtReimburseList.NewRow();
                    if (ViewState["Id"] != null)
                    {
                        Id = Convert.ToInt32(ViewState["Id"]);
                    }
                    SqlParameter[] parms = new SqlParameter[3];
                    parms[0] = new SqlParameter("@EmpId", Convert.ToInt32(ExpEmpid));
                    parms[1] = new SqlParameter("@RItemNo", Convert.ToInt32(item.Value));
                    parms[2] = new SqlParameter("@Mid", mid);
                    DataSet ds = SqlHelper.ExecuteDataset("sh_GetExpenseValue", parms);
                    string Rvalue = "1";
                    String NoOfDays = "1";
                    string Ttype = "0";
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        Rvalue = ds.Tables[0].Rows[0][1].ToString();
                        NoOfDays = ds.Tables[0].Rows[0][2].ToString();
                        Ttype = ds.Tables[0].Rows[0][3].ToString();
                    }
                    dtRow["ID"] = Id;
                    dtRow["RItemID"] = item.Value;
                    dtRow["RItem"] = item.Text;
                    dtRow["EmpID"] = Convert.ToInt32(ExpEmpid);
                    dtRow["Purpose"] = "";
                    dtRow["Qty"] = Rvalue;  //"1";
                    if (item.Value == "3")
                    {
                        dtRow["Amount"] = (Convert.ToDouble(Rvalue) * (Convert.ToDouble(NoOfDays) - 1));//.ToString();
                        dtRow["UnitRate"] = (Convert.ToInt32(NoOfDays) - 1);//.ToString();
                    }
                    else if (item.Value == "4")
                    {
                        dtRow["Amount"] = Convert.ToDouble(Rvalue);//.ToString();
                        dtRow["UnitRate"] = "1";
                    }
                    else
                    {
                        if (NoOfDays == "0")
                            NoOfDays = "1";
                        dtRow["Amount"] = Convert.ToDouble(Rvalue) * Convert.ToDouble(NoOfDays);//.ToString();
                        dtRow["UnitRate"] = Convert.ToInt32(NoOfDays);//.ToString();
                    }
                    //dtRow["Amount"] = "0";
                    //dtRow["UnitRate"] = "0";
                    dtRow["AUID"] = "2";
                    dtRow["DateOfSpent"] = DateTime.Now.ToString("dd MMM yyyy");
                    dtRow["Proof"] = "";
                    dtRow["ERID"] = "0";
                    item.Selected = false;
                    if (Ttype != "1" && item.Value == "4") { 
                    }
                    else
                    {
                        if ((Convert.ToDouble(Rvalue) * Convert.ToDouble(NoOfDays) > 0))
                        {
                            dtReimburseList.Rows.Add(dtRow);
                            Id = Id + 1;
                            ViewState["Id"] = Id;
                        }
                    }
                
                }
                foreach (GridViewRow row in gvExpenses.Rows)
                {
                    TextBox txtqty = (TextBox)row.FindControl("txtQty");
                    TextBox txtAmount = (TextBox)row.FindControl("txtAmount");
                    TextBox txtrate = (TextBox)row.FindControl("txtRate");
                    Label lblItemId = (Label)row.FindControl("lblRItem");
                    DropDownList ddlunits = new DropDownList();
                    ddlunits = (DropDownList)row.FindControl("ddlunits");
                    ddlunits.SelectedValue = "2";
                    DataRow[] drSelected = dtReimburseList.Select("RItem='" + lblItemId.Text + "'");
                    drSelected[0]["Purpose"] = "";
                    drSelected[0]["Qty"] = txtqty.Text;
                    drSelected[0]["EmpID"] = Convert.ToInt32(ExpEmpid);
                    if (txtrate.Text == "") { Convert.ToDouble(txtrate.Text = "0"); }
                    drSelected[0]["UnitRate"] = Convert.ToDouble(txtrate.Text);
                    drSelected[0]["AUID"] = "2";
                    drSelected[0]["DateOfSpent"] = DateTime.Now.ToString("dd MMM yyyy");
                    drSelected[0]["Proof"] = "";
                    drSelected[0]["ERID"] = "0";
                }
                dtReimburseList.AcceptChanges();
                ViewState["ReimItems"] = dtReimburseList;
                dtReimburseList = (DataTable)ViewState["ReimItems"];
                gvExpenses.DataSource = dtReimburseList;
                gvExpenses.DataBind();
                btnExpenses.Visible = true;
            }
            else
            {
                AlertMsg.MsgBox(Page, "Select Item", AlertMsg.MessageType.Warning);
            }
        }
        protected void gvExpenses_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Del")
            {
                int ID = Convert.ToInt32(e.CommandArgument);
                if (editstatus == 0)
                {
                    DataTable dtList = (DataTable)ViewState["ReimItems"];
                    DataRow[] drSelected = null;
                    drSelected = dtList.Select("ID='" + e.CommandArgument + "'");
                    GridViewRow gvRow = (GridViewRow)((LinkButton)e.CommandSource).Parent.Parent;
                    int index = gvRow.RowIndex;
                    if (index>=0)
                    {
                        dtList.Rows.RemoveAt(index);
                    }
                    dtList.AcceptChanges();
                    ViewState["ReimItems"] = dtList;
                    gvExpenses.DataSource = dtList;
                    gvExpenses.DataBind();
                }
                else
                {
                    int id = Convert.ToInt32(e.CommandArgument);
                    GridViewRow gvRow = (GridViewRow)((LinkButton)e.CommandSource).Parent.Parent;
                    int index = gvRow.RowIndex;
                    Label lblRItemNo = (Label)gvExpenses.Rows[gvRow.RowIndex].FindControl("lblRItemNo");
                    SqlParameter[] parms = new SqlParameter[3];
                    parms[0] = new SqlParameter("@ERID", id);
                    parms[1] = new SqlParameter("@RItemNo", lblRItemNo.Text);
                    parms[2] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
                    parms[2].Direction = ParameterDirection.ReturnValue;
                    SqlHelper.ExecuteDataset("sh_deleteexpense", parms);
                    if ((int)parms[2].Value == 4)
                    {
                        SqlParameter[] parms1 = new SqlParameter[2];
                        parms1[0] = new SqlParameter("@ID", id);
                        parms1[1] = new SqlParameter("@fcase", 6);
                        DataSet ds = SqlHelper.ExecuteDataset("SP_th_BussinessTrip_Update_Delete_VIEW_Paging_Select", parms1);
                        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                        {
                            gvExpenses.DataSource = ds;
                            gvExpenses.DataBind();
                            DataTable dt = (DataTable)ds.Tables[0];
                            ViewState["ReimItems"] = dt;
                        }
                        AlertMsg.MsgBox(Page, "Record Deleted");
                    }
                }
            }


        }
        protected void btnExpenses_Click(object sender, EventArgs e)
        {
            int ERID = 0;
            if (editstatus == 1)
            {
                ERID = ERIDNO;
            }

            DataSet dsRemItems = new DataSet("RemItemDataSet");
            DataTable dt = new DataTable("RemItemTable");
            dt.Columns.Add(new DataColumn("EmpID", typeof(System.Int32)));
            dt.Columns.Add(new DataColumn("RItemID", typeof(System.Int32)));
            dt.Columns.Add(new DataColumn("Uom", typeof(System.String)));
            dt.Columns.Add(new DataColumn("Purpose", typeof(System.String)));
            dt.Columns.Add(new DataColumn("Quantity", typeof(System.Double)));
            dt.Columns.Add(new DataColumn("Rate", typeof(System.Double)));
            dt.Columns.Add(new DataColumn("SpentOn", typeof(System.String)));
            dt.Columns.Add(new DataColumn("DOS", typeof(System.String)));
            dt.Columns.Add(new DataColumn("Proof", typeof(System.String)));
            dt.Columns.Add(new DataColumn("Status", typeof(System.Int32)));
            dsRemItems.Tables.Add(dt);
            foreach (GridViewRow gvRow in gvExpenses.Rows)
            {
                Label lblEmpID = (Label)gvRow.Cells[2].FindControl("lblEmpID");
                Label lblRItem = (Label)gvRow.Cells[3].FindControl("lblRItemNo");
                DropDownList ddlunits = (DropDownList)gvRow.Cells[4].FindControl("ddlunits");
                int UnitOfMeasure = int.Parse("2");
                if (UnitOfMeasure == 0)
                {
                    AlertMsg.MsgBox(Page, "Please select units of measure.!", AlertMsg.MessageType.Warning);
                    return;
                }
                TextBox txtRate = (TextBox)gvRow.Cells[5].FindControl("txtRate");
                double UnitRate;// = int.Parse(txtRate.Text);
                try
                {
                    UnitRate = double.Parse(txtRate.Text);
                }
                catch (Exception)
                {
                    AlertMsg.MsgBox(Page, "Unitrate can takes numbers only.!", AlertMsg.MessageType.Warning);
                    return;
                }
                if (txtRate.Text == "" || UnitRate <= 0)
                {
                    AlertMsg.MsgBox(Page, "Please enter proper unitrate.!", AlertMsg.MessageType.Warning);
                    return;
                }
                else
                {
                    UnitRate = double.Parse(txtRate.Text);
                }
                TextBox txtQty = (TextBox)gvRow.Cells[6].FindControl("txtQty");
                double Quantity;
                try
                {
                    Quantity = double.Parse(txtQty.Text);
                }
                catch (Exception)
                {
                    AlertMsg.MsgBox(Page, "Quantity can take numbers only.!", AlertMsg.MessageType.Warning);
                    return;
                }
                if (txtQty.Text == "" || Quantity <= 0)
                {
                    AlertMsg.MsgBox(Page, "Please enter proper quntity.!", AlertMsg.MessageType.Warning);
                    return;
                }
                else
                {
                    Quantity = double.Parse(txtQty.Text);
                }
                TextBox txtAmount = (TextBox)gvRow.Cells[7].FindControl("txtAmount");
                Label lblRItemID = (Label)gvRow.Cells[9].FindControl("lblRItemNo");
                DataRow dr = dt.NewRow();
                dr["RItemID"] = Convert.ToInt32(lblRItemID.Text);
                dr["EmpID"] = Convert.ToInt32(lblEmpID.Text);
                ViewState["EmpView"] = Convert.ToInt32(lblEmpID.Text);
                dr["Uom"] = Convert.ToInt32(2);
                dr["Rate"] = Convert.ToDouble(txtRate.Text);
                dr["Quantity"] = Convert.ToDouble(txtQty.Text);
                dr["Purpose"] = "";
                dr["SpentOn"] = DateTime.Now;
                dr["DOS"] = DateTime.Now.ToString(ConfigurationManager.AppSettings["DateFormat"]);
                dr["Proof"] = "";
                dr["Status"] = 1;
                dt.Rows.Add(dr);
            }
            dsRemItems.AcceptChanges();
            try
            {
                //DataSet ds = AttendanceDAC.HR_InsUpdRemItems(dsRemItems);
                SQLDBUtil.ExecuteDataset("HMS_RemItemsNewXML", new SqlParameter[] { new SqlParameter("@RemItems", dsRemItems.GetXml()), new SqlParameter("@mid", mid), new SqlParameter("@ERID", ERID) });
                mid = 0;
                ExpEmpid = 0;
                tblExpenses.Visible = false;
                BindGrid();
            }
            catch (Exception ex)
            {
                AlertMsg.MsgBox(Page, ex.Message.ToString(), AlertMsg.MessageType.Error);
                return;
            }
            gvExpenses.Visible = false;
            tblAdd.Visible = false;
        }
        #endregion Events
        #region method
        private void clear()
        {
            TxtEmp.Text = string.Empty;
            ddlEmp_hid.Value = "";
            txtFrom.Text = "";
            txtTo.Text = "";
            ddlfromCity.SelectedIndex = 0;
            ddlToCity.SelectedIndex = 0;
            ddlTravelMode.SelectedIndex = 0;
            ddlBookingClass.SelectedIndex = 0;
            txtRemarks.Text = string.Empty;
            ddlWorksite.SelectedIndex = 0;
        }
        private void BindGrid()
        {
            try
            {
                gvChildDetails.Visible = false;
                objHrCommon.PageSize = EmpReimbursementPendingRejPaging.ShowRows;
                objHrCommon.CurrentPage = EmpReimbursementPendingRejPaging.CurrentPage;
                int status = Convert.ToInt32(Request.QueryString["key"]);
                int ID = 0;
                if (txtID.Text.Trim() != string.Empty)
                {
                    ID = Convert.ToInt32(txtID.Text);
                }
                SqlParameter[] parms = new SqlParameter[6];
                parms[0] = new SqlParameter("@ID", ID);
                parms[1] = new SqlParameter("@CurrPage", objHrCommon.CurrentPage);
                parms[2] = new SqlParameter("@PageSize", objHrCommon.PageSize);
                parms[3] = new SqlParameter("@NrRecords", System.Data.SqlDbType.Int);
                parms[3].Direction = ParameterDirection.Output;
                parms[4] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
                parms[4].Direction = ParameterDirection.ReturnValue;
                parms[5] = new SqlParameter("@Status", status);
                DataSet ds = SqlHelper.ExecuteDataset("sh_bindBussinesstrip", parms);
                objHrCommon.NoofRecords = (int)parms[3].Value;
                objHrCommon.TotalPages = (int)parms[4].Value;
                if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0)
                {
                    gvView.DataSource = ds;
                    gvView.DataBind();
                }
                else
                {
                    gvView.DataSource = null;
                    gvView.DataBind();
                }
                EmpReimbursementPendingRejPaging.Visible = false;
                EmpReimbursementPendingRejPaging.Bind(objHrCommon.CurrentPage, objHrCommon.TotalPages, objHrCommon.NoofRecords, objHrCommon.PageSize);
                GetParentMenuId();
            }
            catch (Exception ex) { clsErrorLog.HMSEventLog(ex, "BussinessTrip", "BindGrid", "004"); }
        }
        public void GetParentMenuId()
        {
            string URL = Request.Url.Segments[Request.Url.Segments.Length - 1] + Request.Url.Query;
            int RoleId = Convert.ToInt32(Session["RoleId"].ToString());
            int ModuleId = 1;


            ProcDept objProc = new ProcDept();
            DataSet ds = ProcDept.GetAllowed(RoleId, ModuleId, URL);
            int MenuId = 0;

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                MenuId = Convert.ToInt32(ds.Tables[0].Rows[0]["Under"]);
                Editable = (bool)ds.Tables[0].Rows[0]["Editable"];

                if (Convert.ToBoolean(ds.Tables[0].Rows[0]["ViewAll"].ToString()) == false)
                {
                    gvView.Columns[9].Visible = Convert.ToBoolean(ds.Tables[0].Rows[0]["Allowed"].ToString());


                    gvView.Columns[10].Visible = Convert.ToBoolean(ds.Tables[0].Rows[0]["Editable"].ToString());
                    gvView.Columns[11].Visible = Convert.ToBoolean(ds.Tables[0].Rows[0]["Editable"].ToString());
                    gvView.Columns[12].Visible = Convert.ToBoolean(ds.Tables[0].Rows[0]["Editable"].ToString());

                }
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
        public void BindItem()
        {
            DataSet ds = AECLOGIC.ERP.HMS.PayRollMgr.GetReimbursementItemsList();
            lstItems.DataSource = ds.Tables[0];
            lstItems.DataTextField = "Name";
            lstItems.DataValueField = "RMItemId";
            lstItems.DataBind();
        }
        public DataSet GetAUDataSet()
        {
            return (DataSet)ViewState["dsAU"];
        }
        public int GetAUIndex(string AUID)
        {
            ArrayList alUnitIndexes = (ArrayList)ViewState["alUnitIndexes"];
            return alUnitIndexes.IndexOf(AUID.Trim());
        }
        public void CalculateAmt()
        {
            foreach (GridViewRow gvRow in gvExpenses.Rows)
            {
                TextBox txtRate = (TextBox)gvRow.Cells[5].FindControl("txtRate");
                double UnitRate;// = int.Parse(txtRate.Text);
                try
                {
                    UnitRate = double.Parse(txtRate.Text);
                }
                catch (Exception)
                {
                    AlertMsg.MsgBox(Page, "Unitrate can takes numbers only.!", AlertMsg.MessageType.Warning);
                    return;
                }
                //if (txtRate.Text == "" || UnitRate <= 0)
                //{
                //    AlertMsg.MsgBox(Page, "Please enter proper unitrate.!", AlertMsg.MessageType.Warning);
                //    return;
                //}
                //else
                //{
                UnitRate = double.Parse(txtRate.Text);
                //}
                TextBox txtQty = (TextBox)gvRow.Cells[6].FindControl("txtQty");
                double Quantity;
                try
                {
                    Quantity = double.Parse(txtQty.Text);
                }
                catch (Exception)
                {
                    AlertMsg.MsgBox(Page, "Quantity can take numbers only.!", AlertMsg.MessageType.Warning);
                    return;
                }
                if (txtQty.Text == "" || Quantity <= 0)
                {
                    AlertMsg.MsgBox(Page, "Please enter proper quntity.!", AlertMsg.MessageType.Warning);
                    return;
                }
                else
                {
                    Quantity = double.Parse(txtQty.Text);
                }
                double Amount = UnitRate * Quantity;
                TextBox txtAmount = (TextBox)gvRow.Cells[7].FindControl("txtAmount");
                txtAmount.Text = Convert.ToString(Amount);
                ViewState["Amount"] = Amount;
            }
        }
        #endregion method
    }
}
