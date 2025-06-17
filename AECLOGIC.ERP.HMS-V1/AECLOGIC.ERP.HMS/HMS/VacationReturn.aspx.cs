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
using System.Web.UI.WebControls;
using System.Web;

namespace AECLOGIC.ERP.HMSV1
{
    public partial class VacationReturnV1 : AECLOGIC.ERP.COMMON.WebFormMaster
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

            EmpNoReturnPageIng.FirstClick += new Paging.PageFirst(EmpNoReturnPageIng_FirstClick);
            EmpNoReturnPageIng.PreviousClick += new Paging.PagePrevious(EmpNoReturnPageIng_FirstClick);
            EmpNoReturnPageIng.NextClick += new Paging.PageNext(EmpNoReturnPageIng_FirstClick);
            EmpNoReturnPageIng.LastClick += new Paging.PageLast(EmpNoReturnPageIng_FirstClick);
            EmpNoReturnPageIng.ChangeClick += new Paging.PageChange(EmpNoReturnPageIng_FirstClick);
            EmpNoReturnPageIng.ShowRowsClick += new Paging.ShowRowsChange(EmpNoReturnPageIng_ShowRowsClick);
            EmpNoReturnPageIng.CurrentPage = 1;
            
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
               // tblExpenses.Visible = false;
                //gvChildDetails.Visible = false;
                //btnSubmit.Visible = false;
                if (Request.QueryString.Count > 0)
                {
                    int status = Convert.ToInt32(Request.QueryString["Apr"]);
                    if (status == 4)
                    {
                        tblNoReturn.Visible = true;
                        tblView.Visible = false;
                        tblAdd.Visible = false;
                        BindNoRtnGrid();
                    }
                    else
                    {
                        tblView.Visible = true;
                        tblAdd.Visible = false;
                        tblNoReturn.Visible = false;
                        BindGrid();
                    }
                }
                else
                {
                    tblNoReturn.Visible = false;
                    tblView.Visible = false;
                    tblAdd.Visible = true;
                    // BindItem();
                }
               // BindLocations(ddlfromCity);
                //BindLocations(ddlToCity);
                //FIllObject.FillDropDown(ref ddlBookingClass, "get_T_HMS_BookingClass");
                //FIllObject.FillDropDown(ref ddlTravelMode, "sh_ddlBindTravelMode");
            }
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
        void EmpNoReturnPageIng_ShowRowsClick(object sender, EventArgs e)
        {
            EmpNoReturnPageIng.CurrentPage = 1;
            BindNoRtnGrid();
        }
        void EmpNoReturnPageIng_FirstClick(object sender, EventArgs e)
        {
            BindNoRtnGrid();
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (ddlEmp_hid.Value != "")
            {
                SqlParameter[] parm = new SqlParameter[1];
                parm[0] = new SqlParameter("@Empid", ddlEmp_hid.Value);
                DataSet ds = SqlHelper.ExecuteDataset("sp_Count_Vacationreturn", parm);
                

                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    AlertMsg.MsgBox(Page, "Record Already Exist!");

                } else
                {
                    string filenames = "", ext = "";
                    int id = 1;
                    if (fudDocument.HasFile)
                    {
                        if (ds != null && ds.Tables[1].Rows.Count > 0)
                            id = Convert.ToInt32(ds.Tables[1].Rows[0]["VRId"].ToString());
                        filenames = fudDocument.PostedFile.FileName;
                        //ViewState["filename"] = filename;
                        ext = filenames.Split('.')[filenames.Split('.').Length - 1];
                        //ViewState["ext"] = ext;
                        // filenames = Server.MapPath("\\BussinessTrip\\" + ddlEmp_hid.Value + "." + ext);

                        filenames = Server.MapPath("\\VacationReturn\\" + id + "." + ext);
                        fudDocument.PostedFile.SaveAs(filenames);
                        SqlParameter[] parms = new SqlParameter[7];
                        parms[0] = new SqlParameter("@Empid", ddlEmp_hid.Value);
                        parms[1] = new SqlParameter("@RDate", CodeUtilHMS.ConvertToDate_ddMMMyyy(txtFrom.Text.Trim(), CodeUtilHMS.DateFormat.ddMMMyyyy));
                        parms[2] = new SqlParameter("@Remarks", txtRemarks.Text);
                        parms[3] = new SqlParameter("@CreatedBy", Session["UserId"]);
                        parms[4] = new SqlParameter("@filetype", ext);
                        parms[5] = new SqlParameter("@filepath", filenames);
                        parms[6] = new SqlParameter("@VRId", System.Data.SqlDbType.Int);
                        parms[6].Direction = ParameterDirection.ReturnValue;
                        int Output = SqlHelper.ExecuteNonQuery("sh_insertVacationReturn", parms);
                        if (Output > 0) { AlertMsg.MsgBox(Page, "Record Saved! "); }
                        else if (Output != 1 && Output != 2 && Output != 3) { AlertMsg.MsgBox(Page, "Already Exists!"); }
                        if (Output > 0)
                        {
                            clear();
                        }

                    }
                    else
                    {
                        AlertMsg.MsgBox(Page, "Please Upload the Document!");

                    }
                }
        
            }
            else
            {
                AlertMsg.MsgBox(Page, "Select Employee", AlertMsg.MessageType.Warning);
            }
        }
        protected void btnFilter_Click(object sender, EventArgs e)
        {
            gvView.DataSource = null;
            gvView.DataBind();

            BindGrid();
        }
                
        protected void gvView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int ID = Convert.ToInt32(e.CommandArgument);
            if (e.CommandName == "Apr")
            {
                GridViewRow gvRow = (GridViewRow)((LinkButton)e.CommandSource).Parent.Parent;
                int index = gvRow.RowIndex;
                TextBox txtRemarks = (TextBox)gvView.Rows[gvRow.RowIndex].FindControl("txtRemarks");
                string Remarks = txtRemarks.Text.Trim();

                int status = Convert.ToInt32(Request.QueryString["Apr"].ToString());
                SqlParameter[] parms = new SqlParameter[6];
                parms[0] = new SqlParameter("@ID", ID);
                parms[1] = new SqlParameter("@fCase", 1);
                parms[2] = new SqlParameter("@Status", status); 
                parms[3] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
                parms[3].Direction = ParameterDirection.ReturnValue;
                parms[4] = new SqlParameter("@StatusRemarks", Remarks);
                parms[5] = new SqlParameter("@Createdby", Convert.ToInt32(Session["UserId"]));

                int Output = SqlHelper.ExecuteNonQuery("SP_th_VacationReturn_Update", parms);
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
                       // LinkButton lnkEdit = (LinkButton)e.Row.FindControl("lnkEdit");
                        Label lblEStatus = (Label)e.Row.FindControl("lblEStatus");
                        LinkButton lnkApprove = (LinkButton)e.Row.FindControl("lnkApprove");
                        //LinkButton lnkACC = (LinkButton)e.Row.FindControl("lnkACC");
                        LinkButton lnkReject = (LinkButton)e.Row.FindControl("lnkReject");
                        TextBox txtRemarks=(TextBox)e.Row.FindControl("txtRemarks");
                        //lnkACC.Visible = false;
                        if (Request.QueryString["key"] == "3")
                        {
                            lnkReject.Visible = false;
                            lnkApprove.Visible = false;
                            txtRemarks.Enabled = false;
                        }
                        else
                        {
                            lnkReject.Visible = false;
                            lnkApprove.Visible = true;
                            txtRemarks.Enabled = true;
                        }


                    }
                }
            }
            catch (Exception Ex)
            {
                //report error
            }
        }
        public static DataSet GetEmployeeVacation(String SearchKey, int siteid, int Deptid)
        {
            try
            {
                SqlParameter[] sqlPrms = new SqlParameter[4];
                sqlPrms[0] = new SqlParameter("@search", SearchKey);
                sqlPrms[1] = new SqlParameter("@Siteid", siteid);
                if (siteid == 0)
                {
                    sqlPrms[1] = new SqlParameter("@Siteid", SqlDbType.Int);
                }
                sqlPrms[2] = new SqlParameter("@Deptid", Deptid);
                if (Deptid == 0)
                {
                    sqlPrms[2] = new SqlParameter("@Deptid", SqlDbType.Int);
                }
                sqlPrms[3] = new SqlParameter("@Userid", Convert.ToInt32(HttpContext.Current.Session["UserId"]));
                return SQLDBUtil.ExecuteDataset("HMS_GetEmployeeOnVacation", sqlPrms);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionList_Employee(string prefixText, int count, string contextKey)
        {
           
            DataSet ds = GetEmployeeVacation(prefixText.Trim(), 0, 0);//WSID
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
        
        #endregion Events
        #region method
        private void clear()
        {
            TxtEmp.Text = string.Empty;
            ddlEmp_hid.Value = "";
            txtFrom.Text = "";
            txtRemarks.Text = string.Empty;
        }
        private void BindNoRtnGrid()
        {
            try
            {
                // gvChildDetails.Visible = false;
                objHrCommon.PageSize = EmpNoReturnPageIng.ShowRows;
                objHrCommon.CurrentPage = EmpNoReturnPageIng.CurrentPage;
                int status = Convert.ToInt32(Request.QueryString["Apr"]);
                int ID = 0;
                if (txtID.Text.Trim() != string.Empty)
                {
                    ID = Convert.ToInt32(txtID.Text);
                }
                SqlParameter[] parms = new SqlParameter[5];
                parms[0] = new SqlParameter("@CurrPage", objHrCommon.CurrentPage);
                parms[1] = new SqlParameter("@PageSize", objHrCommon.PageSize);
                parms[2] = new SqlParameter("@NrRecords", System.Data.SqlDbType.Int);
                parms[2].Direction = ParameterDirection.Output;
                parms[3] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
                parms[3].Direction = ParameterDirection.ReturnValue;
                parms[4] = new SqlParameter("@UserId", Convert.ToInt32(Session["UserId"]));
                DataSet ds = SqlHelper.ExecuteDataset("sh_BindNoReturnToWork", parms);
                objHrCommon.NoofRecords = (int)parms[2].Value;
                objHrCommon.TotalPages = (int)parms[3].Value;
                if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0)
                {
                    gvNoReturn.DataSource = ds;
                    gvNoReturn.DataBind();
                }
                else
                {
                    gvNoReturn.DataSource = null;
                    gvNoReturn.DataBind();
                }
                EmpNoReturnPageIng.Visible = false;
                EmpNoReturnPageIng.Bind(objHrCommon.CurrentPage, objHrCommon.TotalPages, objHrCommon.NoofRecords, objHrCommon.PageSize);
            }
            catch (Exception ex) { clsErrorLog.HMSEventLog(ex, "BussinessTrip", "BindGrid", "004"); }
        }

        private void BindGrid()
        {
            try
            {
               // gvChildDetails.Visible = false;
                objHrCommon.PageSize = EmpReimbursementPendingRejPaging.ShowRows;
                objHrCommon.CurrentPage = EmpReimbursementPendingRejPaging.CurrentPage;
                int status = Convert.ToInt32(Request.QueryString["Apr"]);
                int ID = 0;
                if (txtID.Text.Trim() != string.Empty)
                {
                    ID = Convert.ToInt32(txtID.Text);
                }
                SqlParameter[] parms = new SqlParameter[7];
                parms[0] = new SqlParameter("@ID", ID);
                parms[1] = new SqlParameter("@CurrPage", objHrCommon.CurrentPage);
                parms[2] = new SqlParameter("@PageSize", objHrCommon.PageSize);
                parms[3] = new SqlParameter("@NrRecords", System.Data.SqlDbType.Int);
                parms[3].Direction = ParameterDirection.Output;
                parms[4] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
                parms[4].Direction = ParameterDirection.ReturnValue;
                parms[5] = new SqlParameter("@Status", status);
                parms[6] = new SqlParameter("@UserId", Convert.ToInt32(Session["UserId"]));
                DataSet ds = SqlHelper.ExecuteDataset("sh_BindVacationReturn", parms);
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
        
        
        #endregion method
    }
}
