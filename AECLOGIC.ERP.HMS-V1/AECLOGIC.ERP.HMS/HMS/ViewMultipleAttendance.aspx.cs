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
    public partial class ViewMultipleAttendance : AECLOGIC.ERP.COMMON.WebFormMaster
    {
        int mid = 0;
        bool viewall;
        string menuname;
        string menuid;
        AttendanceDAC objAtt = new AttendanceDAC();
        protected override void OnInit(EventArgs e)
        {
            ModuleID = 1;
            base.OnInit(e);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            DataSet dstemp = new DataSet();
            if (!IsPostBack)
            {
                try
                {
                    BindEmpNatures();
                    dstemp = BindWorkSite(dstemp);
                   // dstemp = BindDepartments(dstemp);
                    BindEmployees();
                    BindYears();
                    txtDay.Text = Convertdate(DateTime.Now.ToShortDateString());
                }
                catch
                {
                    AlertMsg.MsgBox(Page, "Unable to bind..!");
                }
            }
        }
        public void BindEmpNatures()
        {
            DataSet ds = Leaves.GetEmpNatureList(1);
            ddlEmpNature.DataSource = ds;
            ddlEmpNature.DataTextField = "Nature";
            ddlEmpNature.DataValueField = "NatureOfEmp";
            ddlEmpNature.DataBind();
            ddlEmpNature.Items.Insert(0, new ListItem("---All---", "0"));
        }
        protected void ddlEmpNature_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindEmployees();
        }
        protected void btnDaySearch_Click(object sender, EventArgs e)
        {
            // 1- DayReport
            BindGrid(1);
        }
        // added here by pratap date: 18-04-2016 Sync to Normal Attendance
        protected void btnSynctoNormal_Click(object sender, EventArgs e)
        {
            int Result = 0;
            foreach (GridViewRow gvr in gvMultipleAtt.Rows)
              {
                    Label lblempid = (Label)gvr.FindControl("lblEmpid");
                      int x = objAtt.MultiLog_To_NormalLog_Sync(CODEUtility.ConvertToDate(txtDay.Text.Trim(), DateFormat.DayMonthYear),
                                     Convert.ToInt32(lblempid.Text), Convert.ToInt32(Session["UserId"]));
                   Result++;  
               }
            if (Result>0)
            {
                AlertMsg.MsgBox(Page, "done");
            }
            BindGrid(1);
        }
        protected void btnMonthReport_Click(object sender, EventArgs e)
        {
            // 2- DayReport
            BindGrid(2);
        }
        private DataSet BindWorkSite(DataSet dstemp)
        {
            //dstemp = objAtt.GetWorkSite(0, '1');
            dstemp = AttendanceDAC.GetHMS_DDL_WorkSite( Convert.ToInt32(Session["UserId"]),ModuleID, Convert.ToInt32(Session["CompanyID"]));
            ddlWorksite.DataSource = dstemp.Tables[0];
            ddlWorksite.DataTextField = "Name";
            ddlWorksite.DataValueField = "ID";
            ddlWorksite.DataBind();
            ddlWorksite.Items.Insert(0, new ListItem("---ALL---", "0"));
            return dstemp;
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
        private string Convertdate(string StrDate)
        {
            if (StrDate != "")
            {
                StrDate = StrDate.Split('/')[1].ToString() + "/" + StrDate.Split('/')[0].ToString() + "/" + StrDate.Split('/')[2].ToString();
            }
            return StrDate;
        }
        public void BindEmployees()
        {
            int? WsId = null;
            int? DeptID = null;
            int? EmpNatureID = null;
            if (ddlWorksite.SelectedItem.Value != "0")
            {
                WsId = Convert.ToInt32(ddlWorksite.SelectedItem.Value);
            }
            if (ddlDepartment.SelectedItem.Value != "0")
            {
                DeptID = Convert.ToInt32(ddlDepartment.SelectedItem.Value);
            }
            if (ddlEmpNature.SelectedItem.Value != "0")
            {
                EmpNatureID = Convert.ToInt32(ddlEmpNature.SelectedItem.Value);
            }
            DataSet dsEmps = objAtt.GetEmployeesByWSDEptNatureOT(WsId, DeptID, EmpNatureID);
            ddlEmp.DataSource = dsEmps;
            ddlEmp.DataTextField = "Name";
            ddlEmp.DataValueField = "EmpId";
            ddlEmp.DataBind();
            ddlEmp.Items.Insert(0, new ListItem("---Select---", "0"));
        }
        protected void ddlWorksite_SelectedIndexChanged1(object sender, EventArgs e)
        {
            BindDeparmetBySite(Convert.ToInt32(ddlWorksite.SelectedValue));
            BindEmployees();
        }
        public void BindDeparmetBySite(int Site)
        {
            DataSet ds = AttendanceDAC.BindDeparmetBySite(Site, Convert.ToInt32(Session["CompanyID"]));
            ddlDepartment.DataSource = ds;
            ddlDepartment.DataTextField = "DeptName";
            ddlDepartment.DataValueField = "DepartmentUId";
            ddlDepartment.DataBind();
            ddlDepartment.Items.Insert(0, new ListItem("---ALL---", "0", true));
        }
        protected void ddlDepartment_SelectedIndexChanged1(object sender, EventArgs e)
        {
            BindEmployees();
        }
        public void BindGrid(int RepType)
        {
            //if (ddlEmp.SelectedItem.Value != "0")
            //{
                int? Month = null;
                int? Year = null;
                if (RepType == 2)
                {
                    if (ddlMonth.SelectedItem.Value != "0")
                    {
                        Month = Convert.ToInt32(ddlMonth.SelectedItem.Value);
                    }
                    else
                    {
                        AlertMsg.MsgBox(Page, "Select Month");
                    }
                    Year = Convert.ToInt32(ddlYear.SelectedItem.Value);
                }
                int SyncFlag;
                //Sync
                if (rblstStatus.SelectedValue == "1")
                {
                    SyncFlag = 1;
                }
                else
                {  // non-Sync
                    SyncFlag = 0;
                }
                // pratap date:18-04-2016
                DataSet dsEmpAtt = objAtt.GetAttMultiplelist_WithSync(CODEUtility.ConvertToDate(txtDay.Text.Trim(), 
                    DateFormat.DayMonthYear), Month, Year, Convert.ToInt32(ddlEmp.SelectedItem.Value), RepType, SyncFlag);
                if (dsEmpAtt != null && dsEmpAtt.Tables.Count > 0 && dsEmpAtt.Tables[0].Rows.Count > 0)
                {
                    gvMultipleAtt.DataSource = dsEmpAtt;
                    gvMultipleAtt.DataBind();
                }
                else
                {
                    gvMultipleAtt.DataSource = null;
                    gvMultipleAtt.DataBind();
                }
            //}
            //else
            //{
            //    AlertMsg.MsgBox(Page, "Select Employee");
            //}
        }
    }
}