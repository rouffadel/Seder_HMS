using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using AECLOGIC.HMS.BLL;
using System.Configuration;

namespace AECLOGIC.ERP.HMS
{
    public partial class EditNMRAttendance : AECLOGIC.ERP.COMMON.WebFormMaster
    {
        DataSet dstemp = new DataSet();
        AttendanceDAC objAtt = new AttendanceDAC();
        int mid = 0;
        bool viewall;
        string menuname;
        string menuid;
        static int WSId=0;
        static int DeptId=0;
        static char Staus= '1';
        static int CompanyID = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["CompanyID"].ToString());
        string Name;
      //  static int CompanyID = Convert.ToInt32(Session["CompanyID"]);
        protected override void OnInit(EventArgs e)
        {
            ModuleID = 1;
            base.OnInit(e);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
           
            if ( Convert.ToInt32(Session["UserId"]) == null)
            {
                Response.Redirect("Home.aspx");
            }
            else
            {
                if (!IsPostBack)
                {
                    try
                    {
                        txtDayCalederExtender.Format = ConfigurationManager.AppSettings["DateFormat"].ToString();
                        txtDay.Text = DateTime.Now.ToString(ConfigurationManager.AppSettings["DateFormat"]);
                        dstemp = BindGrid(objAtt, dstemp);

                        if (Convert.ToInt32(Session["MonitorSite"]) != 0)
                        {
                            
                        }
                        else
                        {
                            
                            ddl_SelectedIndexChanged(WSId, e);
                        }

                    }
                    catch
                    {
                        ddl_SelectedIndexChanged(WSId, e);

                    }
                }
            }
        }
     
        private DataSet BindGrid(AttendanceDAC objAtt, DataSet dstemp)
        {
            DateTime Date = CODEUtility.ConvertToDate(txtDay.Text.Trim(), DateFormat.DayMonthYear);
            Name = txtName.Text;
            if (Convert.ToInt32(Session["MonitorSite"]) != 0)
            {
                dstemp = objAtt.HR_GetTodayNMRAttendanceforEditing(0, Convert.ToInt32(Session["MonitorSite"]), Date, Name);
            }
            else
            {
                dstemp = objAtt.HR_GetTodayNMRAttendanceforEditing(0, 0, Date, Name);
            }
            gdvAttend.DataSource = dstemp.Tables[0];
            gdvAttend.DataBind();




            return dstemp;
        }
        public DataSet BindAttendanceType()
        {
            DataSet ds = AttendanceDAC.GetAttendanceType();
            return ds;
        }
      

        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionList(string prefixText, int count, string contextKey)
        {
            WSId = 0;
            DataSet ds = AttendanceDAC.GetWorkSite_by_Wsid(prefixText, WSId, Staus, CompanyID);
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

        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionList_dept(string prefixText, int count, string contextKey)
        {
           
         DataSet ds = AttendanceDAC.BindDeparmetBySite_googlesearch(prefixText, WSId, CompanyID);
            DataTable dt = ds.Tables[0];
            List<string> items = new List<string>(count);
            var rtval = new Dictionary<string, string>();
            foreach (DataRow row in dt.Rows)
            {
                string str = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(row["DepartmentName"].ToString(), row["DepartmentUId"].ToString());
                items.Add(str);
            }

            return items.ToArray(); ;// txtItems.ToArray();

        }

        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionList_empname(string prefixText, int count, string contextKey)
        {

            DataSet ds = AttendanceDAC.GetSearchEmpName_NMRName(prefixText);
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
            WSId = Convert.ToInt32(ddlWorksite_hid.Value == "" ? "0" : ddlWorksite_hid.Value); ;
            
        }

        protected void Getdept(object sender, EventArgs e)
        {

            DeptId = Convert.ToInt32(ddlDepartment_hid.Value == "" ? "0" : ddlDepartment_hid.Value); ;
        }


        public void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddl = (DropDownList)sender;
            GridViewRow gvr = (GridViewRow)(((Control)sender).NamingContainer);
            TextBox txt = (TextBox)gvr.Cells[3].Controls[1];
            if (ddl.SelectedIndex == 1)
            {
                txt.Text = DateTime.Now.ToShortTimeString();
            }
            else
            {
                txt.Text = "";
            }
        }
        public void chkOut_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chk = (CheckBox)sender;
            GridViewRow gvr = (GridViewRow)(((Control)sender).NamingContainer);

            TextBox txt = (TextBox)gvr.Cells[5].FindControl("txtOUT");
            if (chk.Checked == true)
            {
                txt.Text = DateTime.Now.ToShortTimeString();
            }
            else
            {
                txt.Text = "";
            }

        }
        protected void ddl_SelectedIndexChanged(object sender, EventArgs e)
        {
            DateTime Date = CODEUtility.ConvertToDate(txtDay.Text.Trim(), DateFormat.DayMonthYear);
            Name = txtName.Text;
            dstemp = objAtt.HR_GetTodayNMRAttendanceforEditing(Convert.ToInt32(DeptId), Convert.ToInt32(WSId), Date, Name);
            gdvAttend.DataSource = dstemp.Tables[0];
            gdvAttend.DataBind();


        }
        protected void gdvAttend_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DropDownList ddlStatus = (DropDownList)e.Row.FindControl("ddlStatus");
                Button btnHdn = (Button)e.Row.FindControl("btnHid");
                CheckBox chkOut = (CheckBox)e.Row.FindControl("chkOut");
                TextBox txtOut = (TextBox)e.Row.FindControl("txtOUT");
                TextBox txt = (TextBox)e.Row.FindControl("txtIN");
                //txt.Enabled = Convert.ToBoolean(ViewState["Editable"].ToString());
                LinkButton lnkUpd = (LinkButton)e.Row.FindControl("btnUpdate");
                //lnkUpd.Enabled = Convert.ToBoolean(ViewState["Editable"].ToString());
                TextBox txtRemarks = (TextBox)e.Row.FindControl("txtRemarks");
                Label lblEmp = (Label)e.Row.FindControl("lblEmpID");
                // int Row = Convert.ToInt32(e.Row.RowIndex);
                if (btnHdn.CommandArgument != "")
                    ddlStatus.SelectedValue = btnHdn.CommandArgument;
                if (txtOut.Text != "")
                {
                    chkOut.Checked = true;
                    chkOut.Enabled = false;
                }
                if (txt.Text == "")
                {
                    chkOut.Enabled = true;
                    chkOut.Checked = false;
                    txtOut.Text = "";
                }
                int EmpId = Convert.ToInt32(lblEmp.Text);
                chkOut.Attributes.Add("onclick", "javascript:return GetOutTime('" + chkOut.ClientID + "','" + DateTime.Now.ToShortTimeString() + "','" + txtOut.ClientID + "','" + txt.ClientID + "');");
            }
        }
        protected void gdvAttend_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            AttendanceDAC objAtt = new AttendanceDAC();
            int rowIndex = Convert.ToInt32(e.CommandArgument.ToString());
            GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).Parent.Parent;

            if (e.CommandName == "upd")
            {
                Label lblEmp = (Label)gdvAttend.Rows[row.RowIndex].Cells[1].FindControl("lblEmpID");
                // int Id = Convert.ToInt32(gdvAttend.DataKeys[rowIndex].Value);
                DropDownList ddlStatus = (DropDownList)gdvAttend.Rows[row.RowIndex].Cells[2].Controls[1];
                CheckBox chkOut = (CheckBox)gdvAttend.Rows[row.RowIndex].Cells[4].FindControl("chkOut");
                TextBox txtOut = (TextBox)gdvAttend.Rows[row.RowIndex].Cells[5].FindControl("txtOUT");
                TextBox txt = (TextBox)gdvAttend.Rows[row.RowIndex].Cells[3].Controls[1];
                TextBox txtRemarks = (TextBox)gdvAttend.Rows[row.RowIndex].Cells[6].FindControl("txtRemarks");
                int SiteID = Convert.ToInt32(WSId);
                int UserID =  Convert.ToInt32(Session["UserId"]);
                int EmpID = Convert.ToInt32(lblEmp.Text);
                //int i = Convert.ToInt32(objAtt.UpdateFullAtt(Convert.ToInt32(Id), Convert.ToInt32(ddlStatus.SelectedValue),Convert.ToDateTime(txtDay.Text), txt.Text, txtOut.Text, txtRemarks.Text));
                objAtt.hr_UpdateNMRFullAtt(Convert.ToInt32(EmpID), Convert.ToInt32(ddlStatus.SelectedValue), CODEUtility.ConvertToDate(txtDay.Text.Trim(), DateFormat.DayMonthYear), txt.Text, txtOut.Text, txtRemarks.Text);
            }
        }
        protected void txtDay_TextChanged(object sender, EventArgs e)
        {
            DateTime Date = CODEUtility.ConvertToDate(txtDay.Text.Trim(), DateFormat.DayMonthYear);
            Name = txtName.Text;
            dstemp = objAtt.HR_GetTodayNMRAttendanceforEditing(Convert.ToInt32(DeptId), Convert.ToInt32(WSId), Date, Name);
            gdvAttend.DataSource = dstemp.Tables[0];
            gdvAttend.DataBind();
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            DateTime Date = CODEUtility.ConvertToDate(txtDay.Text.Trim(), DateFormat.DayMonthYear);
            Name = txtName.Text;
           
            dstemp = objAtt.HR_GetTodayNMRAttendanceforEditing(Convert.ToInt32(DeptId), Convert.ToInt32(WSId), Date, Name);
            gdvAttend.DataSource = dstemp.Tables[0];
            gdvAttend.DataBind();
        }
    }
}