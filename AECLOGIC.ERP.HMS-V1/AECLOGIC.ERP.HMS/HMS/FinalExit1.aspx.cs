using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using AECLOGIC.HMS.BLL;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using AECLOGIC.ERP.COMMON;
using AECLOGIC.ERP.HMS.HRClasses;
using Aeclogic.Common.DAL;

namespace AECLOGIC.ERP.HMS
{
    public partial class FinalExit1 : AECLOGIC.ERP.COMMON.WebFormMaster
    {


        int mid = 0;
        bool viewall;
        string menuname;
        string menuid;
        bool Editable;
        static int Siteid;
        static int SearchCompanyID;
        static int WSiteid;
        static int EDeptid = 0;


        AttendanceDAC objRights = new AttendanceDAC();
        MasterPage objmaster = new MasterPage();
        HRCommon objHrCommon = new HRCommon();

        static int ModID;
        static int Userid;
        protected override void OnInit(EventArgs e)
        {
            ModuleID = 1;
            base.OnInit(e);
            EmpReimbursementAprovedPaging.FirstClick += new Paging.PageFirst(EmpReimbursementAprovedPaging_FirstClick);
            EmpReimbursementAprovedPaging.PreviousClick += new Paging.PagePrevious(EmpReimbursementAprovedPaging_FirstClick);
            EmpReimbursementAprovedPaging.NextClick += new Paging.PageNext(EmpReimbursementAprovedPaging_FirstClick);
            EmpReimbursementAprovedPaging.LastClick += new Paging.PageLast(EmpReimbursementAprovedPaging_FirstClick);
            EmpReimbursementAprovedPaging.ChangeClick += new Paging.PageChange(EmpReimbursementAprovedPaging_FirstClick);
            EmpReimbursementAprovedPaging.ShowRowsClick += new Paging.ShowRowsChange(EmpReimbursementAprovedPaging_ShowRowsClick);
            EmpReimbursementAprovedPaging.CurrentPage = 1;
        }
        void EmpReimbursementAprovedPaging_ShowRowsClick(object sender, EventArgs e)
        {
            EmpReimbursementAprovedPaging.CurrentPage = 1;
            BindPager();
        }
        void EmpReimbursementAprovedPaging_FirstClick(object sender, EventArgs e)
        {
            empid_hd.Value = "0";
            BindPager();

        }


        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                SearchCompanyID = Convert.ToInt32(Session["CompanyID"]);
                Userid = Convert.ToInt32(Convert.ToInt32(Session["UserId"]).ToString());

                if (!IsPostBack)
                {
                    BindPager();
                }
            }
            catch { }
        }


        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime date;
                if (txtfrom.Text != String.Empty)
                    date = CodeUtilHMS.ConvertToDate_ddMMMyyy(txtfrom.Text, CodeUtilHMS.DateFormat.ddMMMyyyy);
                else
                {
                    AlertMsg.MsgBox(Page, "Select Exit Date");
                    txtfrom.Focus();
                    return;
                }
                int fCase = 0, status = 1;
                if (btnSave.Text == "Save")
                    fCase = 1;
                else
                    fCase = 2;
                SqlParameter[] parms = new SqlParameter[9];
                parms[0] = new SqlParameter("@fCase", fCase);
                parms[1] = new SqlParameter("@Empid", Convert.ToInt32(Session["UserId"]));
                parms[2] = new SqlParameter("@FEFrom", date);
                parms[3] = new SqlParameter("@Status", status);
                parms[4] = new SqlParameter("@isactive", 1);
                parms[5] = new SqlParameter("@CreatedBy", Convert.ToInt32(Session["UserId"]));
                parms[6] = new SqlParameter("@Reason", txtreason.Text.Trim());
                if (ViewState["FEID"] != null)
                    parms[7] = new SqlParameter("@FEID", Convert.ToInt32(ViewState["FEID"]));
                else
                    parms[7] = new SqlParameter("@FEID", SqlDbType.Int);
                parms[8] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
                parms[8].Direction = ParameterDirection.ReturnValue;
                SQLDBUtil.ExecuteNonQuery("SP_th_FinalExit_Insert_Update_Delete_VIEW_Paging_Select", parms);
                int result = Convert.ToInt32(parms[8].Value);
                if (result == 2)
                {
                    AlertMsg.MsgBox(Page, "Already Exist !");
                }
                else
                {
                    AlertMsg.MsgBox(Page, "Saved");
                    clear();
                }
                btnSave.Text = "Save";
                BindGrid();


            }
            catch { }
        }

        void BindPager()
        {
            //objHrCommon.PageSize = EmpReimbursementAprovedPaging.CurrentPage;
            //objHrCommon.CurrentPage = EmpReimbursementAprovedPaging.ShowRows;
            if (Request.QueryString.Count > 0)
            {
                int id = Convert.ToInt32(Request.QueryString["key"].ToString());
                BindProcessGrid(id);
                tblNew.Visible = false;
                tblview.Visible = false;
                tblProcess.Visible = true;
            }
            else
            {
                BindGrid();
            }
        }
        public void BindGrid()
        {

            try
            {

                tblview.Visible = true;
                SqlParameter[] parms = new SqlParameter[5];
                parms[0] = new SqlParameter("@fCase", 4);
                parms[1] = new SqlParameter("@Empid", Convert.ToInt32(Session["UserId"]));
                parms[2] = new SqlParameter("@CurrPage", 1);
                parms[3] = new SqlParameter("@PageSize", 10);
                parms[4] = new SqlParameter("@NrRecords", System.Data.SqlDbType.Int);
                parms[4].Direction = ParameterDirection.Output;
                DataSet ds = SQLDBUtil.ExecuteDataset("SP_th_FinalExit_Insert_Update_Delete_VIEW_Paging_Select", parms);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    gvVeiw.DataSource = ds.Tables[0];
                }
                else
                    gvVeiw.DataSource = null;
                gvVeiw.DataBind();

            }
            catch { }
        }
        protected void btnclear_Click(object sender, EventArgs e)
        {
            clear();

        }
        public int GetParentMenuId()
        {
            string URL = Request.Url.Segments[Request.Url.Segments.Length - 1] + Request.Url.Query;
            int RoleId = Convert.ToInt32(Session["RoleId"].ToString());
            int ModuleId = ModuleID; ;
            DataSet ds = AttendanceDAC.GetAllowed(RoleId, ModuleId, URL);
            int MenuId = 0;
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                MenuId = Convert.ToInt32(ds.Tables[0].Rows[0]["Under"]);
                //ViewState["Editable"] = Editable = (bool)ds.Tables[0].Rows[0]["Editable"];

                
                
            }
            return MenuId;
        }

        private void clear()
        {
            txtfrom.Text = String.Empty;
            txtreason.Text = String.Empty;
        }

        protected void gvVeiw_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            ViewState["FEID"] = String.Empty;
            int FEID = Convert.ToInt32(e.CommandArgument.ToString());
            if (e.CommandName == "Edt")
            {
                ViewState["FEID"] = FEID;
                SqlParameter[] parms = new SqlParameter[2];
                parms[0] = new SqlParameter("@fCase", 5);
                parms[1] = new SqlParameter("@FEID", FEID);
                DataSet ds = SQLDBUtil.ExecuteDataset("SP_th_FinalExit_Insert_Update_Delete_VIEW_Paging_Select", parms);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    txtfrom.Text = ds.Tables[0].Rows[0]["From"].ToString();
                    txtreason.Text = ds.Tables[0].Rows[0]["Reason"].ToString();
                    btnSave.Text = "Update";

                }
            }
            else if (e.CommandName == "Del")
            {
                SqlParameter[] parms = new SqlParameter[2];
                parms[0] = new SqlParameter("@fCase", 3);
                parms[1] = new SqlParameter("@FEID", FEID);
                SQLDBUtil.ExecuteNonQuery("SP_th_FinalExit_Insert_Update_Delete_VIEW_Paging_Select", parms);
                AlertMsg.MsgBox(Page, "Deleted");
                BindGrid();
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {

            if (Request.QueryString.Count > 0)
            {
                int id = Convert.ToInt32(Request.QueryString["key"].ToString());
                BindProcessGrid(id);
            }
        }

        public void BindProcessGrid(int status)
        {
            int Empid;
            if (txtempid.Text == "")
            {
                empid_hd.Value = "";

            }
            Empid = Convert.ToInt32(empid_hd.Value == "" ? "0" : empid_hd.Value);
            try
            {
                objHrCommon.PageSize = EmpReimbursementAprovedPaging.ShowRows;
                objHrCommon.CurrentPage = EmpReimbursementAprovedPaging.CurrentPage;


                tblview.Visible = true;
                SqlParameter[] parms = new SqlParameter[7];
                parms[0] = new SqlParameter("@fCase", 4);
                parms[1] = new SqlParameter("@Empid", Empid);
                parms[2] = new SqlParameter("@CurrPage", objHrCommon.CurrentPage);
                parms[3] = new SqlParameter("@PageSize", objHrCommon.PageSize);
                parms[4] = new SqlParameter("@NrRecords", System.Data.SqlDbType.Int);
                parms[4].Direction = ParameterDirection.Output;
                parms[5] = new SqlParameter("@Status", status);
                parms[6] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
                parms[6].Direction = ParameterDirection.ReturnValue;
                DataSet ds = SQLDBUtil.ExecuteDataset("SP_th_FinalExit_Insert_Update_Delete_VIEW_Paging_Select", parms);

                objHrCommon.NoofRecords = (int)parms[4].Value;
                objHrCommon.TotalPages = (int)parms[6].Value;
                if (ds.Tables[0].Rows.Count > 0)
                {
                    gvViewApproved.DataSource = ds.Tables[0];
                }
                else
                    gvViewApproved.DataSource = null;
                gvViewApproved.DataBind();
                EmpReimbursementAprovedPaging.Bind(objHrCommon.CurrentPage, objHrCommon.TotalPages, objHrCommon.NoofRecords, objHrCommon.PageSize);

            }
            catch { }

        }

        protected void gvViewApproved_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int FEID = Convert.ToInt32(e.CommandArgument.ToString());
            int id = 0;
            if (Request.QueryString.Count > 0)
            {
                id = Convert.ToInt32(Request.QueryString["key"].ToString());
            }
            int status = 1;
            if (e.CommandName == "Rec")
            {

                if (id == 1)
                    status = 5;
                else if (id == 5)
                    status = 2;
                else if (id == 2)
                    status = 3;

                GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).Parent.Parent;
                TextBox txtremarks = (TextBox)row.FindControl("txtremarks");
                UpdRemarks(FEID, id, txtremarks.Text);

                updstatus(FEID, status);
                tblremarks.Visible = false;
            }
            else if (e.CommandName == "App")
            {
                status = 3;
                GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).Parent.Parent;
                TextBox txtremarks = (TextBox)row.FindControl("txtremarks");
                UpdRemarks(FEID, id, txtremarks.Text);

                updstatus(FEID, status);
                tblremarks.Visible = false;

            }
            else if (e.CommandName == "Rej")
            {
                if (id == 3)
                    status = 6;
                else
                    status = 4;
                GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).Parent.Parent;
                TextBox txtremarks = (TextBox)row.FindControl("txtremarks");
                UpdRemarks(FEID, id, txtremarks.Text);

                updstatus(FEID, status);
                tblremarks.Visible = false;

            }
            else if (e.CommandName == "view")
            {
                foreach (GridViewRow row in gvViewApproved.Rows)
                {
                    LinkButton lnkReviseold = (LinkButton)row.FindControl("lnkView");
                    lnkReviseold.CssClass = "btn btn-primary";

                    Label lblstatus = (Label)row.FindControl("lblstatus");
                    if (id == 4)
                    {
                        lblstatus.Text = "Rejected";
                        lblstatus.ForeColor = System.Drawing.Color.Red;
                    }
                    if (id == 3)
                    {
                        lblstatus.Text = "Granted";
                        lblstatus.ForeColor = System.Drawing.Color.LimeGreen;
                    }

                }
                GridViewRow gvr = (GridViewRow)(((Control)e.CommandSource).NamingContainer);

                LinkButton lnkRevise = (LinkButton)gvr.FindControl("lnkView");
                lnkRevise.CssClass = "btn btn-success";
                tblremarks.Visible = true;
                SqlParameter[] parms1 = new SqlParameter[1];
                parms1[0] = new SqlParameter("@FEID", FEID);
                DataSet ds = SQLDBUtil.ExecuteDataset("Sh_finalExitRemarksView", parms1);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    gvRemarks.DataSource = ds.Tables[0];
                }
                else
                    gvRemarks.DataSource = null;
                gvRemarks.DataBind();

            }

            if (status == 2 || status == 5)
                AlertMsg.MsgBox(Page, "Recommended");
            if (status == 3)
                AlertMsg.MsgBox(Page, "Approved");
            if (status == 4)
                AlertMsg.MsgBox(Page, "Rejected");




        }
        public void updstatus(int FEID, int status)
        {
            SqlParameter[] parms = new SqlParameter[3];
            parms[0] = new SqlParameter("@fCase", 6);
            parms[1] = new SqlParameter("@Status", status);
            parms[2] = new SqlParameter("@FEID", FEID);
            SQLDBUtil.ExecuteNonQuery("SP_th_FinalExit_Insert_Update_Delete_VIEW_Paging_Select", parms);
            BindPager();
        }
        public void UpdRemarks(int FEID, int id, string Remarks)
        {
            SqlParameter[] parms = new SqlParameter[4];
            parms[0] = new SqlParameter("@FEID", FEID);
            parms[1] = new SqlParameter("@id", id);
            parms[2] = new SqlParameter("@Remarks", Remarks);
            parms[3] = new SqlParameter("@Userid", Convert.ToInt32(Session["UserId"]));
            SQLDBUtil.ExecuteNonQuery("Sh_finalExitRemarks", parms);
            // AlertMsg.MsgBox(Page, "Remarks Saved !");
            BindPager();

        }

        protected void gvViewApproved_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (Request.QueryString.Count > 0)
            {
                int id = Convert.ToInt32(Request.QueryString["key"].ToString());
                if (id == 1 || id == 5)
                {
                    //Pending
                    e.Row.Cells[4].Visible = false;
                    e.Row.Cells[5].Visible = true;
                    e.Row.Cells[3].Visible = true;
                    e.Row.Cells[6].Visible = false;
                    if (e.Row.RowType == DataControlRowType.DataRow)
                    {
                        LinkButton btn = (LinkButton)e.Row.FindControl("lnkReject");
                        btn.Text = "Reject";
                    }

                }
                else if (id == 2)
                {
                    //Recommend
                    e.Row.Cells[3].Visible = false;
                    e.Row.Cells[5].Visible = true;
                    e.Row.Cells[4].Visible = true;
                    e.Row.Cells[6].Visible = false;
                    if (e.Row.RowType == DataControlRowType.DataRow)
                    {
                        LinkButton btn = (LinkButton)e.Row.FindControl("lnkReject");
                        btn.Text = "Reject";
                    }

                }
                else if (id == 3)
                {
                    //Approved
                    e.Row.Cells[3].Visible = false;
                    e.Row.Cells[4].Visible = false;
                    // e.Row.Cells[6].Visible = false;
                    e.Row.Cells[2].Visible = false;
                    e.Row.Cells[3].Visible = false;
                    e.Row.Cells[6].Visible = true;
                    e.Row.Cells[5].Visible = true;
                    //LinkButton btn = (LinkButton)e.Row.FindControl("lnkReject");
                    //btn.Text = "Cancel";
                    if (e.Row.RowType == DataControlRowType.DataRow)
                    {
                        LinkButton btn = (LinkButton)e.Row.FindControl("lnkReject");
                        btn.Text = "Cancel";
                        e.Row.Cells[6].Text = "Granted";
                        e.Row.Cells[6].ForeColor = System.Drawing.Color.LimeGreen;
                    }
                }
                else if (id == 4)
                {
                    //Rejected
                    e.Row.Cells[4].Visible = false;
                    e.Row.Cells[5].Visible = false;
                    // e.Row.Cells[6].Visible = false;
                    e.Row.Cells[2].Visible = false;
                    e.Row.Cells[3].Visible = false;
                    e.Row.Cells[7].Visible = true;
                    if (e.Row.RowType == DataControlRowType.DataRow)
                    {
                        e.Row.Cells[6].Text = "Rejected";
                        e.Row.Cells[6].ForeColor = System.Drawing.Color.Red;
                    }
                }
                else if (id == 6)
                {
                    //Rejected
                    e.Row.Cells[4].Visible = false;
                    e.Row.Cells[5].Visible = false;
                    // e.Row.Cells[6].Visible = false;
                    e.Row.Cells[2].Visible = false;
                    e.Row.Cells[3].Visible = false;
                    e.Row.Cells[7].Visible = true;
                    if (e.Row.RowType == DataControlRowType.DataRow)
                    {
                        e.Row.Cells[6].Text = "Cancelled";
                        e.Row.Cells[6].ForeColor = System.Drawing.Color.Red;
                    }
                }

            }

        }

        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetEmpidList(string prefixText, int count, string contextKey)
        {
            DataSet ds = AttendanceDAC.GetGoogleSearch_by_EmpName_All(prefixText);
            DataTable dt = ds.Tables[0];
            List<string> items = new List<string>(count);
            var rtval = new Dictionary<string, string>();
            foreach (DataRow row in dt.Rows)
            {
                string str = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(row["Name"].ToString(), row["ID"].ToString());
                items.Add(str);
            }
            return items.ToArray(); ;
        }
    }
}