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
    public partial class EmsStock : AECLOGIC.ERP.COMMON.WebFormMaster
    {
        int mid = 0;
        bool viewall, Editable;
        string menuname;
        string menuid;
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
             BindGrid_gvems(objHrCommon);
             BindGrid_gvalereadycleared(objHrCommon);
             BindGrid_gdvreg(objHrCommon);
             BindGrid_gvregalreadycleared(objHrCommon);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
               lblname.Text = Session["Empname"].ToString();
                lblTakeoverEmp.Text = Session["TakeHoverEmpname"].ToString();
                EmpListPaging.Visible = false;
               BindGrid_gvems(objHrCommon);
               BindGrid_gvalereadycleared(objHrCommon);
               BindGrid_gvregalreadycleared(objHrCommon);
                BindGrid_gdvreg(objHrCommon);
                lnkems.Visible = false;
                lnkems1.Visible = false;
            }
        }
        protected void lnkems_search(object sender, EventArgs e)
        {
            EmpListPaging.Visible = true;
            BindGrid_gvems(objHrCommon);
            gdvreg.Visible = false;
            gvems.Visible = true;
        }
        public void BindGrid_gvems(HRCommon objHrCommon)
        {
            EmpID = Convert.ToInt32(Request.QueryString["Empid"].ToString());
            objHrCommon.PageSize = EmpListPaging.ShowRows;
            objHrCommon.CurrentPage = EmpListPaging.CurrentPage;
            SqlParameter[] sqlParams = new SqlParameter[9];
            sqlParams[0] = new SqlParameter("@CurrentPage", objHrCommon.CurrentPage);
            sqlParams[1] = new SqlParameter("@PageSize", objHrCommon.PageSize);
            sqlParams[2] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
            sqlParams[2].Direction = ParameterDirection.ReturnValue;
            sqlParams[3] = new SqlParameter("@NoofRecords", System.Data.SqlDbType.Int);
            sqlParams[3].Direction = ParameterDirection.Output;
            sqlParams[4] = new SqlParameter("@SiteID", objHrCommon.SiteID);
            sqlParams[5] = new SqlParameter("@EqipmetName", DBNull.Value);
            if (EmpID != 0)
            {
                sqlParams[6] = new SqlParameter("@EmpID", EmpID);
            }
            else
            {
                sqlParams[6] = new SqlParameter("@EmpID", System.Data.SqlDbType.Int);
            }
            sqlParams[7] = new SqlParameter("@ResID", DBNull.Value);
            if (CompanyID != 0)
                sqlParams[8] = new SqlParameter("@CompanyID", CompanyID);
            else
                sqlParams[8] = new SqlParameter("@CompanyID", System.Data.SqlDbType.Int);
       DataSet  ds = SQLDBUtil.ExecuteDataset("EMS_GetMachineManager", sqlParams);
            objHrCommon.NoofRecords = (int)sqlParams[3].Value;
            objHrCommon.TotalPages = (int)sqlParams[2].Value;
            if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0)
            {
                gvems.DataSource = ds;
                gvems.DataBind();
                Label11.Visible = true;
            }
            else
            {
                Label12.Visible = false;
                Label11.Visible = false;
                Label9.Visible = false;
                lnkems.Visible = false;
                gvems.Visible = false;
                gvems.DataSource = ds;
                gvems.DataBind();
            }
            EmpListPaging.Bind(objHrCommon.CurrentPage, objHrCommon.TotalPages, objHrCommon.NoofRecords, objHrCommon.PageSize);
        }
        //for equipment alearedy cleared items
        public void BindGrid_gvalereadycleared(HRCommon objHrCommon)
        {
             EmpID = Convert.ToInt32(Request.QueryString["Empid"].ToString());
            objHrCommon.PageSize = EmpListPaging.ShowRows;
            objHrCommon.CurrentPage = EmpListPaging.CurrentPage;
            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@CurrentPage", objHrCommon.CurrentPage);
            param[1] = new SqlParameter("@Pagesize", objHrCommon.PageSize);
            param[2] = new SqlParameter("@ReturnValue", System.Data.SqlDbType.Int);
            param[2].Direction = ParameterDirection.ReturnValue;
            param[3] = new SqlParameter("@NoofRecords", System.Data.SqlDbType.Int);
            param[3].Direction = ParameterDirection.Output;
            param[4] = new SqlParameter("@Empid", EmpID);
            DataSet ds = SQLDBUtil.ExecuteDataset("HR_EMSGetAlreadyClearedEMp", param);
            objHrCommon.NoofRecords = (int)param[3].Value;
            objHrCommon.TotalPages = (int)param[2].Value;
            if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0)
            {
                gvalereadycleared.DataSource = ds;
                gvalereadycleared.DataBind();
                Label10.Visible = true;
            }
            else
            {
                Label10.Visible = false;
                gvalereadycleared.DataSource = ds;
                gvalereadycleared.DataBind();
            }
            EmpListPaging.Bind(objHrCommon.CurrentPage, objHrCommon.TotalPages, objHrCommon.NoofRecords, objHrCommon.PageSize);
        }
        //for regestration already cleared employee
        public void BindGrid_gvregalreadycleared(HRCommon objHrCommon)
        {
             EmpID = Convert.ToInt32(Request.QueryString["Empid"].ToString());
            objHrCommon.PageSize = EmpListPaging.ShowRows;
            objHrCommon.CurrentPage = EmpListPaging.CurrentPage;
            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@CurrentPage", objHrCommon.CurrentPage);
            param[1] = new SqlParameter("@Pagesize", objHrCommon.PageSize);
            param[2] = new SqlParameter("@ReturnValue", System.Data.SqlDbType.Int);
            param[2].Direction = ParameterDirection.ReturnValue;
            param[3] = new SqlParameter("@NoofRecords", System.Data.SqlDbType.Int);
            param[3].Direction = ParameterDirection.Output;
            param[4] = new SqlParameter("@Empid", EmpID);
            DataSet ds = SQLDBUtil.ExecuteDataset("HR_EMSregestrationGetAlreadyClearedEMp", param);
            objHrCommon.NoofRecords = (int)param[3].Value;
            objHrCommon.TotalPages = (int)param[2].Value;
            if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0)
            {
                gvregalreadycleared.DataSource = ds;
                gvregalreadycleared.DataBind();
                Label12.Visible = true;
            }
            else
            {
                Label12.Visible = false;
                gvregalreadycleared.DataSource = ds;
                gvregalreadycleared.DataBind();
            }
            EmpListPaging.Bind(objHrCommon.CurrentPage, objHrCommon.TotalPages, objHrCommon.NoofRecords, objHrCommon.PageSize);
        }
        public void BindGrid_gdvreg(HRCommon objHrCommon)
        {
            EmpID = Convert.ToInt32(Request.QueryString["Empid"].ToString());
            objHrCommon.PageSize = EmpListPaging.ShowRows;
            objHrCommon.CurrentPage = EmpListPaging.CurrentPage;
            SqlParameter[] sqlParams = new SqlParameter[5];
            sqlParams[0] = new SqlParameter("@CurrentPage", objHrCommon.CurrentPage);
            sqlParams[1] = new SqlParameter("@PageSize", objHrCommon.PageSize);
            sqlParams[2] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
            sqlParams[2].Direction = ParameterDirection.ReturnValue;
            sqlParams[3] = new SqlParameter("@NoofRecords", System.Data.SqlDbType.Int);
            sqlParams[3].Direction = ParameterDirection.Output;
            sqlParams[4] = new SqlParameter("@Empid", EmpID);
            DataSet ds = SQLDBUtil.ExecuteDataset("Get_Emsstock", sqlParams);
            objHrCommon.NoofRecords = (int)sqlParams[3].Value;
            objHrCommon.TotalPages = (int)sqlParams[2].Value;
            if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0)
            {
                gdvreg.DataSource = ds;
                gdvreg.DataBind();
                Label13.Visible = true;
            }
            else
            {
                Label13.Visible = false;
                Label7.Visible = false;
                lnkems1.Visible = false;
                gdvreg.Visible = false;
                gdvreg.DataSource = ds;
                gdvreg.DataBind();
            }
            EmpListPaging.Bind(objHrCommon.CurrentPage, objHrCommon.TotalPages, objHrCommon.NoofRecords, objHrCommon.PageSize);
        }
        protected void lnkems1_search(object sender, EventArgs e)
        {
            BindGrid_gdvreg(objHrCommon);
            gvems.Visible = false;
            gdvreg.Visible = true;
        }
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow gvrow in gvems.Rows)
            {
                CheckBox chkselect = (CheckBox)gvrow.FindControl("chkItemmms");
                if (chkselect.Checked)
                {
                    try
                    {
                        // Label lblResourceId = (Label)gvrow.FindControl("lblResourceID");
                        //handover employeeid
                        string Empname = Session["Empname"].ToString();
                        string[] words = Empname.Split(' ');
                        string Empid = words[0];
                        //take over employeeid
                        string TakeEmpname = Session["TakeHoverEmpname"].ToString();
                        string[] words1 = TakeEmpname.Split(' ');
                        string TakeEmpid = words1[0];
                        //subresid
                        Label subresid = (Label)gvrow.FindControl("lblEqptID");
                        int UserID =  Convert.ToInt32(Session["UserId"]);
                        int Deptid = Convert.ToInt32(Request.QueryString["Deptid"].ToString());
                        //for equipment
                        Label lblclearenceitem = (Label)gvrow.FindControl("lblEqptName");
                        string Clearenceitem = lblclearenceitem.Text;
                            //these parameter is used for inserting table EmpClearenceTable for equipment
                            SqlParameter[] par = new SqlParameter[5];
                            par[0] = new SqlParameter("@HandingOverEMpid", Empid);
                            par[1] = new SqlParameter("@TakingOverEmpid", TakeEmpid);
                            par[2] = new SqlParameter("@Deptid", Deptid);
                            par[3] = new SqlParameter("@ClearenceItems", Clearenceitem);
                            par[4] = new SqlParameter("@Createdby", Convert.ToInt32( Convert.ToInt32(Session["UserId"]).ToString()));
                            SQLDBUtil.ExecuteNonQuery("EMSAlreadyClearenceTable", par);
                            //these below code is used for updateing regestation and equipment
                            SqlParameter[] param = new SqlParameter[4];
                            param[0] = new SqlParameter("@HandEmpid", Empid);
                            param[1] = new SqlParameter("@TakeEmpid", TakeEmpid);
                            param[2] = new SqlParameter("@subresid", Convert.ToInt32(subresid.Text));
                            param[3] = new SqlParameter("@UserID", UserID);
                            SQLDBUtil.ExecuteNonQuery("UPD_Equipment_EMS_Clearence", param);
                            AlertMsg.MsgBox(Page, "Done");
                            BindGrid_gvems(objHrCommon);
                            BindGrid_gvalereadycleared(objHrCommon);
                    }
                    catch (Exception ex)
                    {
                        AlertMsg.MsgBox(Page, ex.Message.ToString(),AlertMsg.MessageType.Error);
                    }
                }
            }
            foreach (GridViewRow gvrow in gdvreg.Rows)
            {
                CheckBox chkselect = (CheckBox)gvrow.FindControl("chlitm");
                 if (chkselect.Checked)
                 { 
                     //HandOverEmpid
                     //TakeoverEmpid
                     //ActId
                     //EqptID
                     string Empname = Session["Empname"].ToString();
                     string[] words = Empname.Split(' ');
                     string HandEmpid = words[0];
                     string TakeEmpname = Session["TakeHoverEmpname"].ToString();
                     string[] words1 = TakeEmpname.Split(' ');
                     string TakeEmpid = words1[0];
                     Label Eqptidfull = (Label)gvrow.FindControl("Label2");
                     string[] Eqptid1 = Eqptidfull.Text.Split('/');
                     string Eqptid = Eqptid1[1];
                     Label ActId = (Label)gvrow.FindControl("Label1");
                     int ActId1 = Convert.ToInt32(ActId.Text);
                     //for regestration
                     Label lblregitemcleared = (Label)gvrow.FindControl("Label3");
                     string clrdregstnitems = lblregitemcleared.Text;
                     //these parameter is used for inserting table EmpClearenceTable for regestration
                     int Deptid = Convert.ToInt32(Request.QueryString["Deptid"].ToString());
                     SqlParameter[] pa = new SqlParameter[5];
                     pa[0] = new SqlParameter("@HandingOverEMpid", HandEmpid);
                     pa[1] = new SqlParameter("@TakingOverEmpid", TakeEmpid);
                     pa[2] = new SqlParameter("@Deptid", Deptid);
                     pa[3] = new SqlParameter("@ClearenceItems", clrdregstnitems);
                     pa[4] = new SqlParameter("@Createdby", Convert.ToInt32( Convert.ToInt32(Session["UserId"]).ToString()));
                     SQLDBUtil.ExecuteNonQuery("EMSregestrationAlreadyClearenceTable", pa);
                     SqlParameter[] parms = new SqlParameter[4];
                     parms[0]=new SqlParameter("@HandEmpid",HandEmpid);
                     parms[1] = new SqlParameter("@TakeEmpid", TakeEmpid);
                     parms[2] = new SqlParameter("@Eqptid", Eqptid);
                     parms[3] = new SqlParameter("@ActId1", ActId1);
                    //  
                     SQLDBUtil.ExecuteNonQuery("UPD_Registration_EMS_Clearence", parms);
                     BindGrid_gdvreg(objHrCommon);
                     BindGrid_gvregalreadycleared(objHrCommon);
                 }
            }
            AlertMsg.MsgBox(Page, "Done");
        }
        protected void btnback_Click(object sender, EventArgs e)
        {
            EmpID = Convert.ToInt32(Request.QueryString["Empid"].ToString());
            Session["Empid"] = EmpID;
            string Ename = Session["Empname"].ToString();
            Response.Redirect("clearenceview.aspx?Empid=" + EmpID + "&key=" + 1 + "&Name=" + Ename);
        }
    }
}