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
    public partial class ClearenceEmp : AECLOGIC.ERP.COMMON.WebFormMaster
    {
        int mid = 0;
        bool viewall, Editable;
        string menuname;
        string menuid;
        string ModuleId = System.Configuration.ConfigurationManager.AppSettings["ModuleId"];
        int CompanyID = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["CompanyID"]);
        HRCommon objHrCommon = new HRCommon();
        AttendanceDAC objAtt = new AttendanceDAC();
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
            EmployeBind(objHrCommon);
           
        }
        protected void rbShow_CheckedChanged(object sender, EventArgs e)
        {
            EmpListPaging.CurrentPage = 1;
            BindPager();
        }
        public string GetText()
        {

            if (rbShowActive.Checked)
            {
                return "Deactivate";
            }
            else
            {
                return "Active";
            }
        }
        void EmployeBind(HRCommon objHrCommon)
        {

            try
            {
                bool status;


                if (rbShowActive.Checked)
                    status = true;
                else
                    status = false;

                objHrCommon.PageSize = EmpListPaging.ShowRows;
                objHrCommon.CurrentPage = EmpListPaging.CurrentPage;
                objHrCommon.Status = status;
                int? Dept = null;
                txtname.Text = null;
                if (txtSearchDeprt.Text.Trim() != "")
                {
                    Dept = Convert.ToInt32(txtSearchDeprt_hid.Value == "" ? "0" : txtSearchDeprt_hid.Value);

                }

                
                DataSet ds = AttendanceDAC.HR_Getemployeeclearence(objHrCommon, Dept, Convert.ToInt32(Session["CompanyID"]), txtname.Text);
                if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0)
                {
                    gdvWS.DataSource = ds;
                    gdvWS.DataBind();
                }
                else
                {
                    gdvWS.DataSource = null;
                    gdvWS.DataBind();
                }
                EmpListPaging.Bind(objHrCommon.CurrentPage, objHrCommon.TotalPages, objHrCommon.NoofRecords, objHrCommon.PageSize);

            }
            catch (Exception e)
            {
                throw e;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetParentMenuId();
                BindCountry();
                EmployeBind(objHrCommon);
                txtname.Text = String.Empty;
                mainview.ActiveViewIndex = 1;
                if (Request.QueryString.Count > 0)
                {
                    int id = Convert.ToInt32(Request.QueryString["id"].ToString());
                    if (id == 1)
                    {
                        mainview.ActiveViewIndex = 0;
                    }
                }
            }
        }
        private void BindCountry()
        {
            FIllObject.FillDropDown(ref DptName, "HMS_Get_department");
        }
        public int GetParentMenuId()
        {
            string URL = Request.Url.Segments[Request.Url.Segments.Length - 1] + Request.Url.Query;
            int RoleId = Convert.ToInt32(Session["RoleId"]);
            int ModuleId = ModuleID; ;

            DataSet ds = AttendanceDAC.GetAllowed(RoleId, ModuleId, URL);
            int MenuId = 0;
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                MenuId = Convert.ToInt32(ds.Tables[0].Rows[0]["Under"]);
                 btnSubmit.Enabled = (bool)ds.Tables[0].Rows[0]["Editable"]; ;
            }
            return MenuId;
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (btnSubmit.Text.ToLower().Trim() == "save")
            {
                int DeptID = Convert.ToInt32(DptName.SelectedItem.Value);
                int id = 1;
                int CompanyID = Convert.ToInt32(Session["CompanyID"]);
                if (txtname.Text =="")
                {
                    AlertMsg.MsgBox(Page, "Please Add Clearence Type");
                    return;
                }
                else
                {
                    DataSet ds = AttendanceDAC.AddClearence(id, txtname.Text.Trim(), DeptID);
                    AlertMsg.MsgBox(Page, "Done");
                }
            }
            else
            {
                if (btnSubmit.Text.Trim() == "Update")
                {
                    mainview.ActiveViewIndex = 0;
                    btnSubmit.Text = "Update";

                   
                    SqlParameter[] sqlParams = new SqlParameter[2];
                    sqlParams[0] = new SqlParameter("@ID", ViewState["clearid"]);
                    sqlParams[1] = new SqlParameter("@ItemName", txtname.Text.Trim());
                    
                    DataSet ds = SQLDBUtil.ExecuteDataset("Update_clearence", sqlParams);
                    AlertMsg.MsgBox(Page, "Updated");

                }
            }
            EmployeBind(objHrCommon);
            mainview.ActiveViewIndex = 1;
            
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            EmployeBind(objHrCommon);
        }

        protected void gdvWS_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string itemname = Convert.ToString(e.CommandArgument);
            txtname.Text = itemname.ToString();
            if (e.CommandName == "Edt")
            {
                mainview.ActiveViewIndex = 0;
                btnSubmit.Text = "Update";
                GridViewRow gvr = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                Label ID = (Label)gvr.FindControl("lblitem");
                int id = Convert.ToInt32(ID.Text);
                GridViewRow gvr1 = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                Label deptid1 = (Label)gvr1.FindControl("lbdeptid");
                int deptid2 = Convert.ToInt32(deptid1.Text);
                SqlParameter[] p = new SqlParameter[2];
                p[0] = new SqlParameter("@ID", id);
                p[1] = new SqlParameter("@deptid", deptid2);
                
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetEmployeeClearenceDetails", p);

                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    txtname.Text = ds.Tables[0].Rows[0]["ItemName"].ToString();
                    DptName.SelectedValue = ds.Tables[0].Rows[0]["deptid"].ToString();
                }
                ViewState["clearid"] = id;
               
            }
            if (e.CommandName == "del")
            {
                GridViewRow gvr = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                Label id = (Label)gvr.FindControl("lblitem");

                    try
                    {
                        bool status;
                        if (rbShowActive.Checked)
                            status = false;
                        else
                            status = true;
                        SqlParameter[] parm = new SqlParameter[2];
                        parm[0] = new SqlParameter("@ID", Convert.ToInt32(id.Text));
                        parm[1] = new SqlParameter("@Status", status);
                        SQLDBUtil.ExecuteNonQuery("sh_DltItemClearence", parm);
                        if (status)
                            AlertMsg.MsgBox(Page, "Activated !");
                        else
                            AlertMsg.MsgBox(Page, "Deactivated !");

                        BindPager();
                    }
                    catch (Exception DelDesig)
                    {
                        AlertMsg.MsgBox(Page, DelDesig.Message.ToString(),AlertMsg.MessageType.Error);
                    }


                   
            }
        }
        public void BindMechinaries()
        {
            
            DataSet ds = objAtt.GetEmployeesByWSDEptNature(null, null, null);
            DataRow dr;
            dr = ds.Tables[0].NewRow();
            dr[0] = 0;
            dr[1] = "--Select--";
            ds.Tables[0].Rows.InsertAt(dr, 0);
            ViewState["Machinery"] = ds;
            AllMechinaries = new ArrayList();
            foreach (DataRow drMech in ds.Tables[0].Rows)
                AllMechinaries.Add(drMech["EmpId"].ToString());
            BindEMpByWO();

        }
        public int GetMechIndex(string Value)
        {
            return AllMechinaries.IndexOf(Value);
        }
        public static ArrayList AllMechinaries = new ArrayList();
       
        public void BindEMpByWO()
        {
            
            DataSet ds = SQLDBUtil.ExecuteDataset("Get_EmpName_All");

            DataRow dr;
            dr = ds.Tables[0].NewRow();
            dr[0] = 0;
            ds.Tables[0].Rows.InsertAt(dr, 0);
            ViewState["BindEMPBYWO"] = ds.Tables[0];

        }

        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionList(string prefixText, int count, string contextKey)
        {
            DataSet ds = AttendanceDAC.HR_GetDepartment_googlesearch(prefixText);
            DataTable dt = ds.Tables[0];
            List<string> items = new List<string>(count);
            var rtval = new Dictionary<string, string>();
            foreach (DataRow row in dt.Rows)
            {
                string str = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(row["Departmentname"].ToString(), row["departmentuid"].ToString());
                items.Add(str);
            }

            return items.ToArray(); ;// txtItems.ToArray();

        }

        void bindgvclearence(string itemname)
        {
            int? Dept = null;
            
            objHrCommon.PageSize = EmpListPaging.ShowRows;
            objHrCommon.CurrentPage = EmpListPaging.CurrentPage;
            DataSet ds = AttendanceDAC.HR_Getemployeeclearence(objHrCommon, Dept, Convert.ToInt32(Session["CompanyID"]), txtname.Text);
            if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0)
            {
                gdvWS.DataSource = ds;
                gdvWS.DataBind();
                DptName.SelectedValue = ds.Tables[0].Rows[0]["DepartmentUid"].ToString();
                txtname.Text = ds.Tables[0].Rows[0]["ItemName"].ToString();
                mainview.ActiveViewIndex = 0;
              
            }
        }

    }
}