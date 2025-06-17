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
using AECLOGIC.HMS.BLL;
using AECLOGIC.ERP.COMMON;
using System.Data.SqlClient;
using Aeclogic.Common.DAL;
using System.Collections.Generic;
namespace AECLOGIC.ERP.HMS
{
    public partial class Checkcustodi : AECLOGIC.ERP.COMMON.WebFormMaster
    {
        static int WS;
        string name;
        string machineEmpname;
        HRCommon objHrCommon = new HRCommon();
        int EmpID;
        string ModuleId = System.Configuration.ConfigurationManager.AppSettings["ModuleId"];
        int CompanyID = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["CompanyID"]);
        protected override void OnInit(EventArgs e)
        {
            ModuleID = 1;
            base.OnInit(e);
            EmpListPaging.FirstClick += new Paging.PageFirst(EmpListPaging_FirstClick);
            EmpListPaging.PreviousClick += new Paging.PagePrevious(EmpListPaging_FirstClick);
            EmpListPaging.NextClick += new Paging.PageNext(EmpListPaging_FirstClick);
            EmpListPaging.LastClick += new Paging.PageLast(EmpListPaging_FirstClick);
            EmpListPaging.ChangeClick += new Paging.PageChange(EmpListPaging_FirstClick);
            EmpListPaging.ShowRowsClick += new Paging.ShowRowsChange(EmpListPaging_ShowRowsClick);
            EmpListPaging.CurrentPage = 1;
        }
        void EmpListPaging_FirstClick(object sender, EventArgs e)
        {
            BindPager();
        }
        void EmpListPaging_ShowRowsClick(object sender, EventArgs e)
        {
            EmpListPaging.CurrentPage = 1;
            BindPager();
        }
        void BindPager()
        {
            objHrCommon.PageSize = EmpListPaging.CurrentPage;
            objHrCommon.CurrentPage = EmpListPaging.ShowRows;
            BindGrid_gvmms(objHrCommon);
            BindGrid_gvalereadycleared(objHrCommon);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            // added by pratap for input sting is not correct format. date:22-05-2016
            if (!IsPostBack)
            {
                txtname.Text = String.Empty;
                lblname.Text = Session["Empname"].ToString();
                lblTakeoverEmp.Text = Session["TakeHoverEmpname"].ToString();
                EmpListPaging.Visible = false;
                BindGrid_gvmms(objHrCommon);
                BindGrid_gvalereadycleared(objHrCommon);
                btnUpdate.Visible = true;
            }
        }
        //hms
        protected void lnkhms_search(object sender, EventArgs e)
        {
            EmpListPaging.Visible = true;
        }
        //ems
        protected void lnkems_search(object sender, EventArgs e)
        {
            EmpListPaging.Visible = true;
        }
        //mms
        protected void lnkmms_search(object sender, EventArgs e)
        {
            EmpListPaging.Visible = true;
            BindGrid_gvmms(objHrCommon);
        }
        public void BindGrid_gvmms(HRCommon objHrCommon)
        {
            EmpID = Convert.ToInt32(Request.QueryString["Empid"].ToString());
            objHrCommon.PageSize = EmpListPaging.ShowRows;
            objHrCommon.CurrentPage = EmpListPaging.CurrentPage;
            SqlParameter[] param = new SqlParameter[8];
            param[0] = new SqlParameter("@CurrentPage", objHrCommon.CurrentPage);
            param[1] = new SqlParameter("@Pagesize", objHrCommon.PageSize);
            param[2] = new SqlParameter("@ReturnValue", System.Data.SqlDbType.Int);
            param[2].Direction = ParameterDirection.ReturnValue;
            param[3] = new SqlParameter("@NoofRecords", System.Data.SqlDbType.Int);
            param[3].Direction = ParameterDirection.Output;
            param[4] = new SqlParameter("@ResourceID", DBNull.Value);
            param[5] = new SqlParameter("@EmpID", EmpID);
            param[6] = new SqlParameter("@WsID", DBNull.Value);
            param[7] = new SqlParameter("@DeptID", DBNull.Value);
            DataSet ds = SQLDBUtil.ExecuteDataset("MMS_GetEmpclearence_PersonalLoans", param);
            objHrCommon.NoofRecords = (int)param[3].Value;
            objHrCommon.TotalPages = (int)param[2].Value;
            if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0)
            {
                gvmms.DataSource = ds;
                gvmms.DataBind();
            }
            else
            {
                gvmms.DataSource = ds;
                gvmms.DataBind();
                Label1.Visible = false;
            }
            EmpListPaging.Bind(objHrCommon.CurrentPage, objHrCommon.TotalPages, objHrCommon.NoofRecords, objHrCommon.PageSize);
        }
        //for alearedy cleared items
        public void BindGrid_gvalereadycleared(HRCommon objHrCommon)
        {
            EmpID = Convert.ToInt32(Request.QueryString["Empid"].ToString());
            objHrCommon.PageSize = EmpListPaging.ShowRows;
            objHrCommon.CurrentPage = EmpListPaging.CurrentPage;
            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@CurrentPage", objHrCommon.CurrentPage);
            param[1] = new SqlParameter("@Pagesize", 10000000);
            param[2] = new SqlParameter("@ReturnValue", System.Data.SqlDbType.Int);
            param[2].Direction = ParameterDirection.ReturnValue;
            param[3] = new SqlParameter("@NoofRecords", System.Data.SqlDbType.Int);
            param[3].Direction = ParameterDirection.Output;
            param[4] = new SqlParameter("@Empid", EmpID);
            DataSet ds = SQLDBUtil.ExecuteDataset("HR_MMSGetAlreadyClearedEMp", param);
            objHrCommon.NoofRecords = (int)param[3].Value;
            objHrCommon.TotalPages = (int)param[2].Value;
            if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0)
            {
                gvalereadycleared.DataSource = ds;
                gvalereadycleared.DataBind();
            }
            else
            {
                gvalereadycleared.DataSource = ds;
                gvalereadycleared.DataBind();
                Label2.Visible = false;
            }
            EmpListPaging.Bind(objHrCommon.CurrentPage, objHrCommon.TotalPages, objHrCommon.NoofRecords, objHrCommon.PageSize);
        }
        //pms
        protected void lnkpms_search(object sender, EventArgs e)
        {
            EmpListPaging.Visible = true;
        }
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow gvrow in gvmms.Rows)
            {
                CheckBox chkselect = (CheckBox)gvrow.FindControl("chkItemmms");
                if (chkselect.Checked)
                {
                    btnUpdate.Visible = true;
                    try
                    {
                        Label lblResourceId = (Label)gvrow.FindControl("lblResourceID");
                        //string Empname = Session["Empname"].ToString();
                        //string[] words = Empname.Split(' ');
                        string Empid = Session["Empid"].ToString();
                        string TakeEmpname = Session["TakeHoverEmpname"].ToString();
                        string[] words1 = TakeEmpname.Split(' ');
                        string TakeEmpid = words1[0];
                        int Deptid = Convert.ToInt32(Request.QueryString["Deptid"].ToString());
                        Label lblclearenceitem = (Label)gvrow.FindControl("lblresourcename");
                        string Clearenceitem = lblclearenceitem.Text;
                        //these parameter is used for inserting table EmpClearenceTable
                        SqlParameter[] p = new SqlParameter[5];
                        p[0] = new SqlParameter("@HandingOverEMpid", Empid);
                        p[1] = new SqlParameter("@TakingOverEmpid", TakeEmpid);
                        p[2] = new SqlParameter("@Deptid", Deptid);
                        p[3] = new SqlParameter("@ClearenceItems", Clearenceitem);
                        p[4] = new SqlParameter("@Createdby", Convert.ToInt32(Convert.ToInt32(Session["UserId"]).ToString()));
                        SQLDBUtil.ExecuteNonQuery("MMSAlreadyClearenceTable", p);
                        //these below  parameter is used for transfer the items   
                        SqlParameter[] param = new SqlParameter[4];
                        param[0] = new SqlParameter("@ResourceID", Convert.ToInt32(lblResourceId.Text));
                        param[1] = new SqlParameter("@HandEmpID", Convert.ToInt32(Empid.ToString()));
                        param[2] = new SqlParameter("@TakeEmpID", Convert.ToInt32(TakeEmpid.ToString()));
                        param[3] = new SqlParameter("@UserID", Convert.ToInt32(Convert.ToInt32(Session["UserId"]).ToString()));
                        SQLDBUtil.ExecuteNonQuery("HMS_EMP_Clearence", param);
                        AlertMsg.MsgBox(Page, "Done");
                        Label2.Visible = true;
                        BindGrid_gvmms(objHrCommon);
                        BindGrid_gvalereadycleared(objHrCommon);
                    }
                    catch (Exception ex)
                    {
                        AlertMsg.MsgBox(Page, ex.Message.ToString(), AlertMsg.MessageType.Error);
                    }
                }
            }
        }
        protected void btnback_Click(object sender, EventArgs e)
        {
            EmpID = Convert.ToInt32(Request.QueryString["Empid"].ToString());
            Session["Empid"] = EmpID;
            string mname = Session["Empname"].ToString();
            Response.Redirect("clearenceview.aspx?Empid=" + EmpID + "&key=" + 1 + "&Name=" + mname);
        }
    }
}