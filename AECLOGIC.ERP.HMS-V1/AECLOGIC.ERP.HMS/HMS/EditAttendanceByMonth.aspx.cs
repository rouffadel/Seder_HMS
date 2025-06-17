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
namespace AECLOGIC.ERP.HMS
{
    public partial class EditAttendanceByMonth : AECLOGIC.ERP.COMMON.WebFormMaster
    {
        AttendanceDAC objAtt = new AttendanceDAC();
        int mid = 0;
        bool viewall;
        string menuname;
        string menuid;
        string Name;
        protected override void OnInit(EventArgs e)
        {
            ModuleID = 1;
            base.OnInit(e);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            DataSet dstemp = new DataSet();
           
            Ajax.Utility.RegisterTypeForAjax(typeof(AjaxDAL));
            if (!IsPostBack)
            {

                try
                {


                    string URL = Request.Url.Segments[Request.Url.Segments.Length - 1] + Request.Url.Query;
                    int RoleId = Convert.ToInt32(Session["RoleId"].ToString());
                      
                    int ModuleId = ModuleID;;
                    DataSet ds = AttendanceDAC.GetAllowed(RoleId, ModuleId, URL);

                    dstemp = GetWorkSites(objAtt, dstemp);
                    dstemp = GetDepartments(objAtt, dstemp);
                    BindEmpList();
                    BindYears();

                    ddlWorksite.Items.FindByValue(Session["Site"].ToString()).Selected = true;
                    if ((bool)ViewState["ViewAll"])
                    {
                        ddlWorksite.Enabled = true;
                    }
                    else
                    {
                        ddlWorksite.Enabled = false;
                    }

                    ddlDept.Items[0].Selected = true;
                }
                catch
                {
                }
            }
        }

        #region Supporting Methods


        public void BindEmpAtt()
        {

            try
            {
                int EmpID = Convert.ToInt32(ddlEmp.SelectedValue);
                int Month = Convert.ToInt32(ddlMonth.SelectedValue);
                int Year = Convert.ToInt32(ddlYear.SelectedValue);

                  
                DataSet ds = AttendanceDAC.GetEmpMonthlyAttByEmpID(EmpID, Month, Year);
                if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0)
                {
                    gdvAttend.DataSource = ds;
                    gdvAttend.DataBind();
                    gdvAttend.Visible = true;
                }
                else
                {
                    gdvAttend.EmptyDataText = "No Records Found";
                }

            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public DataSet BindAttendanceType()
        {
              
            DataSet ds = AttendanceDAC.GetAttendanceType();
            return ds;
        }

      

        private DataSet GetWorkSites(AttendanceDAC objAtt, DataSet dstemp)
        {
            dstemp = objAtt.GetWorkSiteByEmpID( Convert.ToInt32(Session["UserId"]), Convert.ToInt32(Session["CompanyID"]), Convert.ToInt32(Session["RoleId"]));
            ddlWorksite.DataSource = dstemp.Tables[0];
            ddlWorksite.DataTextField = "Site_Name";
            ddlWorksite.DataValueField = "Site_ID";
            ddlWorksite.DataBind();

            return dstemp;
        }

        private DataSet GetDepartments(AttendanceDAC objAtt, DataSet dstemp)
        {
            dstemp = objAtt.GetDepartments(0);
            ddlDept.DataSource = dstemp.Tables[0];
            ddlDept.DataTextField = "Deptname";
            //ddlDept.DataTextField = "DepartmentName";
            ddlDept.DataValueField = "DepartmentUId";
            ddlDept.DataBind();
            ddlDept.Items.Insert(0, new ListItem("---ALL---", "0", true));
            return dstemp;
        }

        public void BindEmpList()
        {
              
            string EmpName = string.Empty;
            int WorkSite = Convert.ToInt32(ddlWorksite.SelectedValue);
            int Dept = 0;
            if (ddlDept.SelectedIndex != 0)
            {
                Dept = Convert.ToInt32(ddlDept.SelectedValue);
            }
            DataSet ds = AttendanceDAC.GetEmployeesListBysiteAndDept(WorkSite, Dept);
            ddlEmp.DataSource = ds.Tables[0];
            ddlEmp.DataTextField = "EmpName";
            ddlEmp.DataValueField = "EmpId";
            ddlEmp.DataBind();
            ddlEmp.Items.Insert(0, new ListItem("---SELECT---", "0", true));

        }


        public void BindYears()
        {
              
            DataSet ds = AttendanceDAC.GetCalenderYear();

            int i = 0;
            int Maxyear = Convert.ToInt32(ds.Tables[0].Rows[0]["MaxYear"].ToString());
            for (int Minyear = Convert.ToInt32(ds.Tables[0].Rows[0]["MinYear"].ToString()); Minyear <= Maxyear; Minyear++)
            {
                ddlYear.Items.Insert(0, new ListItem(Convert.ToString(Minyear), Convert.ToString(Minyear)));
                i = i + 1;
            }
            ddlMonth.SelectedValue = ds.Tables[0].Rows[0]["CurrentMonth"].ToString();
            ddlYear.SelectedValue = ds.Tables[0].Rows[0]["CurrentYear"].ToString();

        }

        #endregion Supporting Methods

        protected void ddlMonth_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void ddlDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindEmpList();
        }
        protected void ddlWorksite_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindEmpList();
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindEmpAtt();
        }



        public static ArrayList alStatus = new ArrayList();


        protected void gdvAttend_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DropDownList ddlStatus = (DropDownList)e.Row.FindControl("ddlStatus");
                Button btnHdn = (Button)e.Row.FindControl("btnHid");

                CheckBox chkOut = (CheckBox)e.Row.FindControl("chkOut");
                TextBox txtOut = (TextBox)e.Row.FindControl("txtOUT");
                TextBox txtRemarks = (TextBox)e.Row.FindControl("txtRemarks");
                TextBox txt = (TextBox)e.Row.FindControl("txtIN");
                Label lbldate = (Label)e.Row.FindControl("lblDate");
                DateTime dt = CODEUtility.ConvertToDate(lbldate.Text.Trim(), DateFormat.DayMonthYear);
                if (btnHdn.CommandArgument != "")
                    ddlStatus.SelectedValue = btnHdn.CommandArgument;
                if (txtOut.Text != "")
                {
                    chkOut.Checked = true;
                    chkOut.Enabled = true;
                }
                if (txt.Text == "")
                {
                    chkOut.Enabled = true;
                    chkOut.Checked = true;
                    txtOut.Text = "";
                }
                string EmpId = Convert.ToString(ddlEmp.SelectedValue);
                ddlStatus.Attributes.Add("onchange", "javascript:return CheckLeaveCombination(this,'" + EmpId + "','" + txt.ClientID + "','" + ddlWorksite.ClientID + "','" +  Convert.ToInt32(Session["UserId"]).ToString() + "','" + e.Row.ClientID + "','" + txt.ClientID + "','" + dt + "','" + txtOut.ClientID + "','" + txtRemarks.ClientID + "');");

                chkOut.Attributes.Add("onclick", "javascript:return CheckLeaveCombination1('" + EmpId + "','" + txt.ClientID + "','" + ddlWorksite.ClientID + "','" +  Convert.ToInt32(Session["UserId"]).ToString() + "','" + e.Row.ClientID + "','" + txt.ClientID + "','" + dt + "','" + txtOut.ClientID + "','" + txtRemarks.ClientID + "','" + ddlStatus.ClientID + "');");
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
    }
}