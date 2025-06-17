using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using AECLOGIC.HMS.BLL;
using System.Collections;
using DataAccessLayer;
using AECLOGIC.ERP.COMMON;
using Aeclogic.Common.DAL;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Data.SqlClient;
using System.Configuration;
namespace AECLOGIC.ERP.HMS.HMS
{
    public partial class employeejobwisedes : AECLOGIC.ERP.COMMON.WebFormMaster
    {
        string str = string.Empty;
        HRCommon objCommon = new HRCommon();
        HRCommon objHrCommon = new HRCommon();
        string menuid; static int CompanyID;
        int mid = 0; string menuname; bool viewall; bool Editable; int empid = 0;
        static int WS;
        //int empid;
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
         void EmpListPaging_ShowRowsClick(object sender, EventArgs e)
        {
           EmpListPaging.CurrentPage = 1;
            BindPager();
        }
        void EmpListPaging_FirstClick(object sender, EventArgs e)
        {
            BindPager();
        }
        public void BindPager()
        {
            objHrCommon.PageSize = EmpListPaging.CurrentPage;
            objHrCommon.CurrentPage = EmpListPaging.ShowRows;
            EmployeBind(objHrCommon);
           // dvadd.Visible = true;
        }
        // Search
        void EmployeBind(HRCommon objHrCommon)
        {
            try
            {
                objHrCommon.PageSize = EmpListPaging.ShowRows;
                objHrCommon.CurrentPage = EmpListPaging.CurrentPage;
                int? EmpID = null;
                int? desid = null;
                int? respid = null;
                int? Desgitid = null;
                int? categid = null;
                int? wstid = null;
                if(txtEmpname.Text!="")
                {
                    EmpID = Convert.ToInt32(ddlWorkSite_hid.Value == "" ? "0" : ddlWorkSite_hid.Value); ;
                }
                if (desigination.Text != "")
                {
                    Desgitid = Convert.ToInt32(desigination_hid.Value == "" ? "0" : desigination_hid.Value); ;
                }
                if (specification.Text != "")
                {
                    categid = Convert.ToInt32(specification_hid.Value == "" ? "0" : specification_hid.Value); ;
                }
                if (worksite.Text != "")
                {
                    wstid = Convert.ToInt32(worksite_hid.Value == "" ? "0" : worksite_hid.Value); ;
                }
                SqlParameter[] sqlParams = new SqlParameter[10];
                sqlParams[0] = new SqlParameter("@CurrentPage", objHrCommon.CurrentPage);
                sqlParams[1] = new SqlParameter("@PageSize", objHrCommon.PageSize);
                sqlParams[2] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
                sqlParams[2].Direction = ParameterDirection.ReturnValue;
                sqlParams[3] = new SqlParameter("@NoofRecords", System.Data.SqlDbType.Int);
                sqlParams[3].Direction = ParameterDirection.Output;
                sqlParams[4] = new SqlParameter("@EmpID", EmpID);
                sqlParams[5] = new SqlParameter("@desid", desid);
                sqlParams[6] = new SqlParameter("@respid", respid);
                sqlParams[7] = new SqlParameter("@Desgitid", Desgitid);
                sqlParams[8] = new SqlParameter("@categid", categid);
                sqlParams[9] = new SqlParameter("@wstid", wstid);
                DataSet ds= SqlHelper.ExecuteDataset("HR_Get_Employee_JobWise_EMP", sqlParams);
                objHrCommon.NoofRecords = (int)sqlParams[3].Value;
                objHrCommon.TotalPages = (int)sqlParams[2].Value;
                if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0)
                {
                    gvjobterm.DataSource = ds;
                    gvjobterm.DataBind();
                }
                else
                {
                    gvjobterm.DataSource = null;
                    gvjobterm.DataBind();
                }
                EmpListPaging.Bind(objHrCommon.CurrentPage, objHrCommon.TotalPages, objHrCommon.NoofRecords, objHrCommon.PageSize);
            }
            catch (Exception e)
            {
                throw e;
            }
          //  dvadd.Visible = false;
        }
        public string DocNavigateUrlnew(string ProofID, string Empid)
        {
            string ReturnVal = "";
            ReturnVal = "JobResponsibility/" + Empid + ProofID;
            return ReturnVal;
        }
        public bool Visble(string Ext)
        {
            if (Ext != "")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                CompanyID = Convert.ToInt32(Session["CompanyID"].ToString());
                if (!IsPostBack)
                {
                    if (Request.QueryString.Count > 0)
                {
                    int id = Convert.ToInt32(Request.QueryString["key"]);
                    if (id == 1)
                    {
                        dvView.Visible = true;
                    }
                    else
                    {
                        dvView.Visible = false;
                        BindPager();
                    }
                }
                else
                {
                    dvView.Visible = false;
                    BindPager();
                }
                    BindPager();
                }
            }
            catch (Exception ex)
            {
                AlertMsg.MsgBox(Page, ex.Message.ToString(),AlertMsg.MessageType.Error);
            }
        }
        //for employee
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionListEmployee(string prefixText, int count, string contextKey)
        {
            DataSet ds = AttendanceDAC.HR_GoogleSerac_jobwisedes(prefixText.Trim(), WS);
            DataTable dt = ds.Tables[0];
            List<string> items = new List<string>(count);
            var rtval = new Dictionary<string, string>();
            foreach (DataRow row in dt.Rows)
            {
                string str = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(row["Name"].ToString(), row["ID"].ToString());
                items.Add(str);
            }
            return items.ToArray(); ;// txtItems.ToArray();
        }
        protected void txtSearchWorksite_TextChanged(object sender, EventArgs e)
        {
            WS = Convert.ToInt32(worksite_hid.Value == "" ? "0" : worksite_hid.Value); ;
        }
        //for job description
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionList(string prefixText, int count, string contextKey)
        {
            DataSet ds = AttendanceDAC.get_employewise_jobdescription(prefixText.Trim());//WSID
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
        //for job responsiblities
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionjobRespList(string prefixText, int count, string contextKey)
        {
            DataSet ds = AttendanceDAC.get_employewise_resp(prefixText.Trim());//WSID
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
        //for desigination
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionjobDesginationList(string prefixText, int count, string contextKey)
        {
            DataSet ds = AttendanceDAC.get_employewise_desgi(prefixText.Trim());//WSID
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
        //for Specification
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionjobSpecificationList(string prefixText, int count, string contextKey)
        {
            DataSet ds = AttendanceDAC.get_employewise_spec(prefixText.Trim());//WSID
            DataTable dt = ds.Tables[0];
            List<string> items = new List<string>(count);
            var rtval = new Dictionary<string, string>();
            foreach (DataRow row in dt.Rows)
            {
                string str = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(row["Name"].ToString(), row["ID"].ToString());
                items.Add(str);
            }
            return items.ToArray(); ;// txtItems.ToArray();
        }
        //for worksite
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionjobWorkList (string prefixText, int count, string contextKey)
        {
            DataSet ds = AttendanceDAC.getworksite(prefixText.Trim());//WSID
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
        protected void gvjobterm_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int empid = Convert.ToInt32(e.CommandArgument);
            if (e.CommandName == "Sav")
            {
                GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                FileUpload UploadProof = (FileUpload)row.FindControl("UploadProof");
                string filename = "", ext = string.Empty, path = "";
                filename = UploadProof.PostedFile.FileName;
                if (filename != "")
                {
                    ext = filename.Split('.')[filename.Split('.').Length - 1];
                    SqlParameter[] sqlParams = new SqlParameter[4];
                    sqlParams[0] = new SqlParameter("@CreatedBy",  Convert.ToInt32(Session["UserId"]));
                    sqlParams[1] = new SqlParameter("@Ext", ext);
                    sqlParams[2] = new SqlParameter("@Empid", empid);
                    sqlParams[3] = new SqlParameter("@fcase", 1);
                    SqlHelper.ExecuteNonQuery("sh_JobresponsibilityProof", sqlParams);
                    path = Server.MapPath("~\\hms\\JobResponsibility\\" + empid + "." + ext);
                    UploadProof.PostedFile.SaveAs(path);
                    AlertMsg.MsgBox(Page, "Saved");
                    BindPager();
                }
            }
        }
        protected void gvjobterm_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            string Desc = "";
            string Resp = "";
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string img = string.Empty;
                int empid = Convert.ToInt32((e.Row.DataItem as DataRowView)["empid"].ToString());
                SqlParameter[] sqlParams = new SqlParameter[10];
                sqlParams[0] = new SqlParameter("@CurrentPage", objHrCommon.CurrentPage);
                sqlParams[1] = new SqlParameter("@PageSize", objHrCommon.PageSize);
                sqlParams[2] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
                sqlParams[2].Direction = ParameterDirection.ReturnValue;
                sqlParams[3] = new SqlParameter("@NoofRecords", System.Data.SqlDbType.Int);
                sqlParams[3].Direction = ParameterDirection.Output;
                sqlParams[4] = new SqlParameter("@EmpID", empid);
                sqlParams[5] = new SqlParameter("@desid", System.Data.SqlDbType.Int);
                sqlParams[6] = new SqlParameter("@respid", System.Data.SqlDbType.Int);
                sqlParams[7] = new SqlParameter("@Desgitid", System.Data.SqlDbType.Int);
                sqlParams[8] = new SqlParameter("@categid", System.Data.SqlDbType.Int);
                sqlParams[9] = new SqlParameter("@wstid", System.Data.SqlDbType.Int);
              DataSet  ds = SQLDBUtil.ExecuteDataset("HR_Get_Employee_JobWise_EMP", sqlParams);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    Desc = ds.Tables[0].Rows[0]["jobdescription"].ToString();
                    img = ds.Tables[0].Rows[0]["Path"].ToString();
                    Resp = ds.Tables[0].Rows[0]["respdescription"].ToString();
                    //ddlresp.SelectedValue = ds.Tables[0].Rows[0]["respid"].ToString();
                }
                if (img == "NoImage")
                {
                    HyperLink A1= (HyperLink)e.Row.FindControl("A1");
                    A1.Visible = false;
                }
                e.Row.Cells[6].ToolTip = Desc;
                e.Row.Cells[5].ToolTip = Resp;
            }
            //in gridview jobresponsibility data is wraped
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[5].Attributes.Add("style", "word-break:break-all;word-wrap:break-word;");
            }
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
           // dvadd.Visible = false;
            EmpListPaging.CurrentPage = 1;
            EmployeBind(objHrCommon);
        }
    }
}