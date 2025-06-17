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
using System.Data.SqlClient;
using System.Collections.Generic;
using AECLOGIC.HMS.BLL;
using AECLOGIC.ERP.COMMON;


namespace AECLOGIC.ERP.HMS
{
    public partial class EmployeeWorkStatus : AECLOGIC.ERP.COMMON.WebFormMaster
    {
        AttendanceDAC objEmployee = new AttendanceDAC();
        AttendanceDAC objRights = new AttendanceDAC();
        HRCommon objHrCommon = new HRCommon();
          
        int mid = 0;
        bool viewall;
        string menuname;
        string menuid;

        protected override void OnInit(EventArgs e)
        {
            ModuleID = 1;
            base.OnInit(e);
            // btnSave.Attributes.Add("onclick", "javascript:return ValidateSave('" + txtCategoryName.ClientID + "');");

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
        void BindPager()
        {

            objHrCommon.PageSize = EmpListPaging.CurrentPage;
            objHrCommon.CurrentPage = EmpListPaging.ShowRows;
            EmployeBind(objHrCommon);
        }


        protected void Page_Load(object sender, EventArgs e)
        {

            lblDate.Text = "Date: " + DateTime.Now.ToString(ConfigurationManager.AppSettings["DateDisplayFormat"]);
            if (!IsPostBack)
            {
                BindWorkSites();
                BindDepartments();
                EmployeBind(objHrCommon);
            }

        }
      
        void EmployeBind(HRCommon objHrCommon)
        {

            try
            {
                objHrCommon.PageSize = EmpListPaging.ShowRows;
                objHrCommon.CurrentPage = EmpListPaging.CurrentPage;
                objHrCommon.SiteID = Convert.ToInt32(ddlworksites.SelectedItem.Value);
                objHrCommon.DeptID = Convert.ToDouble(ddldepartments.SelectedItem.Value);
                if (rbActive.Checked)
                {
                    objHrCommon.CurrentStatus = 'y';
                }
                else
                {
                    objHrCommon.CurrentStatus = 'n';
                }
               int  empid = Convert.ToInt32(empname_hid.Value == "" ? "0" : empname_hid.Value);
               objHrCommon.FName = "";
                  
                gveditkbipl.DataSource = null;
                gveditkbipl.DataBind();
                DataSet ds = (DataSet)objEmployee.GetEmployeesWorkStatus(objHrCommon, Convert.ToInt32(Session["CompanyID"]), empid);
                if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0)
                {
                    gveditkbipl.DataSource = ds;
                    gveditkbipl.DataBind();

                }
                EmpListPaging.Bind(objHrCommon.CurrentPage, objHrCommon.TotalPages, objHrCommon.NoofRecords, objHrCommon.PageSize);

            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public string GetWorkSite(string WSid)
        {
            string retVal = "";
            try
            {
                DataSet ds = (DataSet)ViewState["WorkSites"];
                retVal = ds.Tables[0].Select("Site_ID='" + WSid + "'")[0]["Site_Name"].ToString();
            }
            catch { }
            return retVal;
        }
        public string GetDepartment(string DeptId)
        {
            string retVal = "";
            try
            {
                DataSet ds = (DataSet)ViewState["Departments"];
                retVal = ds.Tables[0].Select("DepartmentUId='" + DeptId + "'")[0]["DepartmentName"].ToString();
            }
            catch { }
            return retVal;
        }
        public void BindWorkSites()
        {

            try
            {
                FIllObject.FillDropDown(ref ddlworksites, "HR_GetWorkSite_By_EmpWorkStatus");


                DataSet ds = AttendanceDAC.GetWorkSite(0, '1', Convert.ToInt32(Session["CompanyID"]));
                ViewState["WorkSites"] = ds;
               
                    if (Convert.ToInt32(Session["MonitorSite"]) != 0)
                    {
                        ddlworksites.Items.FindByValue(Session["MonitorSite"].ToString()).Selected = true;
                        ddlworksites.Enabled = false;
                    }
                
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public void BindDepartments()
        {
            try
            {

                DataSet ds = (DataSet)objRights.GetDaprtmentList();
                ViewState["Departments"] = ds;
              
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        protected void EmpdataBound()
        {
            SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings["strConn"]);
            cn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from T_G_EmployeeMaster where Status='y' order by fname";
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = null; 
            da.Fill(ds, "temp");
            gveditkbipl.DataSource = ds;
            gveditkbipl.DataBind();
            cn.Close();

        }

       
        protected void Desabial(int Id)
        {

            try
            {
                SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings["strConn"]);
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "spDeletekbiplPhoneList";
                SqlParameter p1 = new SqlParameter("@Id", Id);
                p1.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(p1);
                int n = cmd.ExecuteNonQuery();
                if (n > 0)
                {
                    AlertMsg.MsgBox(Page, "Delete Employee");
                    ClientScript.RegisterStartupScript(typeof(System.String), "str", "<script type='text/javascript'>alert('Delete Employee')</script>");

                }
                EmpdataBound();


            }
            catch
            {
            }
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionList(string prefixText, int count, string contextKey)
        {
            DataSet ds = AttendanceDAC.HMS_googlesearch_employeeworkstatus(prefixText);
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

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            EmpListPaging.CurrentPage = 1;
            objHrCommon.SiteID = Convert.ToInt32(ddlworksites.SelectedItem.Value);
            objHrCommon.DeptID = Convert.ToDouble(ddldepartments.SelectedItem.Value);
            objHrCommon.FName = txtusername.Text;
            EmployeBind(objHrCommon);
        }
        protected void rbActive_CheckedChanged(object sender, EventArgs e)
        {
            EmployeBind(objHrCommon);
        }
        protected void rbInActive_CheckedChanged(object sender, EventArgs e)
        {
            EmployeBind(objHrCommon);
        }
        protected void ddlworksites_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindDeparmetBySite(Convert.ToInt32(ddlworksites.SelectedValue));
        }
        public void BindDeparmetBySite(int Site)
        {
            DataSet ds = AttendanceDAC.BindDeparmetBySite(Site, Convert.ToInt32(Session["CompanyID"]));
            ddldepartments.DataSource = ds;
            ddldepartments.DataTextField = "DeptName";
            ddldepartments.DataValueField = "DepartmentUId";
            ddldepartments.DataBind();
            ddldepartments.Items.Insert(0, new ListItem("---ALL---", "0", true));
        }
    }
}