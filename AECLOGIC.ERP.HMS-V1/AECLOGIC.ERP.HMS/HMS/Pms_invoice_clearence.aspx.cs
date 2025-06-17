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
    public partial class Pms_invoice_clearence : AECLOGIC.ERP.COMMON.WebFormMaster
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
            BindGrid_gvInVoice(objHrCommon);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblname.Text = Session["Empname"].ToString();
                lblTakeoverEmp.Text = Session["TakeHoverEmpname"].ToString();
                EmpListPaging.Visible = false;
                BindGrid_gvInVoice(objHrCommon);
                BindGrid_gvalereadycleared(objHrCommon);
            }
        }
        public void BindGrid_gvInVoice(HRCommon objHrCommon)
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
            DataSet ds = SQLDBUtil.ExecuteDataset("Get_pms_Invoice_billing", sqlParams);//added by nadeem,04/06/2016 ,changed sp "Get_pms_Invoice"(old),dataset is getting empty.
            objHrCommon.NoofRecords = (int)sqlParams[3].Value;
            objHrCommon.TotalPages = (int)sqlParams[2].Value;
            if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0)
            {
                gvInVoice.DataSource = ds;
                gvInVoice.DataBind();
                Label4.Visible = true;
            }
            else
            {
                gvInVoice.DataSource = ds;
                gvInVoice.DataBind();
                Label4.Visible = false;
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
            param[1] = new SqlParameter("@Pagesize", objHrCommon.PageSize);
            param[2] = new SqlParameter("@ReturnValue", System.Data.SqlDbType.Int);
            param[2].Direction = ParameterDirection.ReturnValue;
            param[3] = new SqlParameter("@NoofRecords", System.Data.SqlDbType.Int);
            param[3].Direction = ParameterDirection.Output;
            param[4] = new SqlParameter("@Empid", EmpID);
            DataSet ds = SQLDBUtil.ExecuteDataset("HR_PMSGetAlreadyClearedEMp", param);
            objHrCommon.NoofRecords = (int)param[3].Value;
            objHrCommon.TotalPages = (int)param[2].Value;
            if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0)
            {
                gvalereadycleared.DataSource = ds;
                gvalereadycleared.DataBind();
                Label2.Visible = true;
            }
            else
            {
                gvalereadycleared.DataSource = ds;
                gvalereadycleared.DataBind();
                Label2.Visible = false;
            }
            EmpListPaging.Bind(objHrCommon.CurrentPage, objHrCommon.TotalPages, objHrCommon.NoofRecords, objHrCommon.PageSize);
        }
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow gvrow in gvInVoice.Rows)
            {
                CheckBox chkselect = (CheckBox)gvrow.FindControl("chlitm");
                if (chkselect.Checked)
                {
                    //string Empname = Session["Empname"].ToString();
                    //string[] words = Empname.Split(' ');
                    string HandEmpid = Session["Empid"].ToString();
                    string TakeEmpname = Session["TakeHoverEmpname"].ToString();
                    string[] words1 = TakeEmpname.Split(' ');
                    string TakeEmpid = words1[0];
                    Label pono = (Label)gvrow.FindControl("lblpoID");
                    Label billno = (Label)gvrow.FindControl("lblInVoiceNo");
                    Label proofid = (Label)gvrow.FindControl("Label1");
                    int Deptid = Convert.ToInt32(Request.QueryString["Deptid"].ToString());
                    Label lblclearenceitem = (Label)gvrow.FindControl("lblpoName");
                    string Clearenceitem = lblclearenceitem.Text;
                    //these parameter is used for inserting table EmpClearenceTable
                    SqlParameter[] par = new SqlParameter[5];
                    par[0] = new SqlParameter("@HandingOverEMpid", HandEmpid);
                    par[1] = new SqlParameter("@TakingOverEmpid", TakeEmpid);
                    par[2] = new SqlParameter("@Deptid", Deptid);
                    par[3] = new SqlParameter("@ClearenceItems", Clearenceitem);
                    par[4] = new SqlParameter("@Createdby", Convert.ToInt32(Convert.ToInt32(Session["UserId"]).ToString()));
                    SQLDBUtil.ExecuteNonQuery("PMS_insertEmpClearence", par);
                    SqlParameter[] p = new SqlParameter[5];
                    p[0] = new SqlParameter("@HandEmpid", HandEmpid);
                    p[1] = new SqlParameter("@TakeEmpid", TakeEmpid);
                    p[2] = new SqlParameter("@PONO", Convert.ToInt32(pono.Text));
                    p[3] = new SqlParameter("@Billno", Convert.ToInt32(billno.Text));
                    p[4] = new SqlParameter("@proofid", Convert.ToInt32(proofid.Text));
                    SQLDBUtil.ExecuteNonQuery("Upd_Pms_invoice_clearence", p);
                    AlertMsg.MsgBox(Page, "Done");
                    BindGrid_gvInVoice(objHrCommon);
                    BindGrid_gvalereadycleared(objHrCommon);
                }
            }
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