using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using AECLOGIC.HMS.BLL;
using System.Collections.Generic;

namespace AECLOGIC.ERP.HMS
{
    public partial class EmptotDetails : AECLOGIC.ERP.COMMON.WebFormMaster
    {
        int mid = 0;
        bool viewall;
        string menuname;
        string menuid;
        static int WSID = 0;
        static int Site = 0;
        static int CompanyID;
        AttendanceDAC objAtt = new AttendanceDAC();
        protected override void OnInit(EventArgs e)
        {
            ModuleID = 1;
            base.OnInit(e);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
           
            if (!IsPostBack)
            {
                BindEmployees();
            }
        }

        public void BindEmployees()
        {
            int? WsId = null;
            int? DeptID = null;
            int? EmpNatureID = null;

           
            if (txtSearchWorksite.Text.Trim() != "")
            {
                WsId = Convert.ToInt32(ddlWorksite_hid.Value == "" ? "0" : ddlWorksite_hid.Value);

            }
           
            if (txtdepartment.Text.Trim() != "")
            {
                DeptID = Convert.ToInt32(ddlDepartment_hid.Value == "" ? "0" : ddlDepartment_hid.Value);

            }


           DataSet dsEmps = objAtt.GetEmployeesByWSDEptNature(WsId, DeptID, EmpNatureID);
            ddlEmp.DataSource = dsEmps;
            ddlEmp.DataTextField = "Name";
            ddlEmp.DataValueField = "EmpId";
            ddlEmp.DataBind();
            ddlEmp.Items.Insert(0, new ListItem("---Select---", "0"));

        }
        public void BindGrid()
        {
            if (ddlEmp.SelectedItem.Value != "0")
            {

                DataSet dsEmpDts = AttendanceDAC.GetEmployeesTotDets(Convert.ToInt32(ddlEmp.SelectedItem.Value));
                if (dsEmpDts != null && dsEmpDts.Tables.Count > 0)
                {
                    if (dsEmpDts.Tables[0].Rows.Count > 0)
                    {
                        gvPersonaldets.DataSource = dsEmpDts.Tables[0];
                        gvPersonaldets.DataBind();
                    }

                    if (dsEmpDts.Tables[1].Rows.Count > 0)
                    {
                        gvJobDetails.DataSource = dsEmpDts.Tables[1];
                        gvJobDetails.DataBind();
                    }
                    if (dsEmpDts.Tables[2].Rows.Count > 0)
                    {
                        gvWorkingDetails.DataSource = dsEmpDts.Tables[2];
                        gvWorkingDetails.DataBind();
                    }
                    if (dsEmpDts.Tables[3].Rows.Count > 0)
                    {
                        gvLeavedetails.DataSource = dsEmpDts.Tables[3];
                        gvLeavedetails.DataBind();
                    }
                    if (dsEmpDts.Tables[4].Rows.Count > 0)
                    {
                        gvSalHikes.DataSource = dsEmpDts.Tables[4];
                        gvSalHikes.DataBind();
                    }
                    if (dsEmpDts.Tables[5].Rows.Count > 0)
                    {
                        gvDocs.DataSource = dsEmpDts.Tables[5];
                        gvDocs.DataBind();
                    }
                }

            }
            else
            {

                AlertMsg.MsgBox(Page, "Select Employee");

            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindGrid();
        }

        protected void btnfilter_Click(object sender, EventArgs e)
        {
            int? WsId = null;
            int? DeptID = null;
            int? EmpNatureID = null;

           
            if (txtSearchWorksite.Text.Trim() != "")
            {
                WsId = Convert.ToInt32(ddlWorksite_hid.Value == "" ? "0" : ddlWorksite_hid.Value);

            }
           
            if (txtdepartment.Text.Trim() != "")
            {
                DeptID = Convert.ToInt32(ddlDepartment_hid.Value == "" ? "0" : ddlDepartment_hid.Value);

            }

            FIllObject.FillEmptyDropDown(ref ddlEmp);
            SqlParameter[] param=new SqlParameter[4];
            param[0]=new SqlParameter("@DeptID",DeptID);
             param[1]=new SqlParameter("@WSID",WsId);
             param[2]=new SqlParameter("@EmpNatureID",EmpNatureID);
             param[3] = new SqlParameter("@Filter", txtemp.Text);
            FIllObject.FillDropDown(ref ddlEmp, "HR_GetEmpByWSDEptNature_Filter",param);

        }

        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionList(string prefixText, int count, string contextKey)
        {
          
            DataSet ds = AttendanceDAC.HR_GetWorkSite_By_googlesearch_EmpList(prefixText.Trim(), WSID);
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
        protected void GetWork(object sender, EventArgs e)
        {

            CompanyID = Convert.ToInt32(Session["CompanyID"]);
            WSID = Convert.ToInt32(ddlWorksite_hid.Value == "" ? "0" : ddlWorksite_hid.Value); ;
            BindEmployees();
            //  WSId = Convert.ToInt32(ddlWs_hid.Value == "" ? "0" : ddlWs_hid.Value); ;
        }

        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionList_Department(string prefixText, int count, string contextKey)
        {
            
            DataSet ds = AttendanceDAC.HR_googlesearch_GetDepartmentBySite(prefixText.Trim(), WSID, CompanyID);
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
        protected void GetDept(object sender, EventArgs e)
        {
            Site = 0;
            Site = Convert.ToInt32(ddlDepartment_hid.Value == "" ? "0" : ddlDepartment_hid.Value); ;
            CompanyID = Convert.ToInt32(Session["CompanyID"]);
            BindEmployees();

        }

    }
}