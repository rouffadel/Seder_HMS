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
    public partial class EditMultipleAttendance : AECLOGIC.ERP.COMMON.WebFormMaster
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
                   // BindEmpNatures();
                  // dstemp = BindWorkSite(dstemp);
                    BindEmployees();
                    BindYears();
                }
                catch
                {
                    AlertMsg.MsgBox(Page, "Unable to bind..!", AlertMsg.MessageType.Error);
                }
            }
        }
        //public void BindEmpNatures()
        //{
        //    DataSet  ds = Leaves.GetEmpNatureList(1);
        //    ddlEmpNature.DataSource = ds;
        //    ddlEmpNature.DataTextField = "Nature";
        //    ddlEmpNature.DataValueField = "NatureOfEmp";
        //    ddlEmpNature.DataBind();
        //    ddlEmpNature.Items.Insert(0, new ListItem("---All---", "0"));
        //}
        protected void ddlEmpNature_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindEmployees();
        }
        protected void btnMonthReport_Click(object sender, EventArgs e)
        {
            // 2- DayReport
            BindGrid(2);
        }
        //private DataSet BindWorkSite(DataSet dstemp)
        //{
        //    dstemp = AttendanceDAC.GetHMS_DDL_WorkSite( Convert.ToInt32(Session["UserId"]), ModuleID, Convert.ToInt32(Session["CompanyID"]));
        //    ddlWorksite.DataSource = dstemp.Tables[0];
        //    ddlWorksite.DataTextField = "Name";
        //    ddlWorksite.DataValueField = "ID";
        //    ddlWorksite.DataBind();
        //    ddlWorksite.Items.Insert(0, new ListItem("---ALL---", "0"));
        //    return dstemp;
        //}
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
            //if (ddlWorksite.SelectedItem.Value != "0")
            //{
            //    WsId = Convert.ToInt32(ddlWorksite.SelectedItem.Value);
            //}
            //if (ddlDepartment.SelectedItem.Value != "0")
            //{
            //    DeptID = Convert.ToInt32(ddlDepartment.SelectedItem.Value);
            //}
            //if (ddlEmpNature.SelectedItem.Value != "0")
            //{
            //    EmpNatureID = Convert.ToInt32(ddlEmpNature.SelectedItem.Value);
            //}
            DataSet dsEmps = objAtt.GetEmployeesByWSDEptNatureOT(WsId, DeptID, EmpNatureID);
            ddlEmp.DataSource = dsEmps;
            ddlEmp.DataTextField = "Name";
            ddlEmp.DataValueField = "EmpId";
            ddlEmp.DataBind();
            ddlEmp.Items.Insert(0, new ListItem("---Select---", "0"));
        }
        //protected void ddlWorksite_SelectedIndexChanged1(object sender, EventArgs e)
        //{
        //    BindDeparmetBySite(Convert.ToInt32(ddlWorksite.SelectedValue));
        //    BindEmployees();
        //}
        //public void BindDeparmetBySite(int Site)
        //{
        //    DataSet ds = AttendanceDAC.BindDeparmetBySite(Site, Convert.ToInt32(Session["CompanyID"]));
        //    ddlDepartment.DataSource = ds;
        //    ddlDepartment.DataTextField = "DeptName";
        //    ddlDepartment.DataValueField = "DepartmentUId";
        //    ddlDepartment.DataBind();
        //    ddlDepartment.Items.Insert(0, new ListItem("---ALL---", "0", true));
        //}
        protected void ddlDepartment_SelectedIndexChanged1(object sender, EventArgs e)
        {
            BindEmployees();
        }
        public void BindGrid(int RepType)
        {
            int? Month = null;
            int? Year = null;
            DateTime? Date = null;
            if (txtDate.Text == "")
            {
                Date = null;
            }
            else
                Date = CodeUtilHMS.ConvertToDate_ddMMMyyy(txtDate.Text.Trim(), CodeUtilHMS.DateFormat.ddMMMyyyy);
            if (RepType == 2)
            {
                if (ddlMonth.SelectedItem.Value != "0")
                {
                    Month = Convert.ToInt32(ddlMonth.SelectedItem.Value);
                }
                else
                {
                    AlertMsg.MsgBox(Page, "Select Month",AlertMsg.MessageType.Warning);
                }
                Year = Convert.ToInt32(ddlYear.SelectedItem.Value);
            }
            DataSet dsEmpAtt = objAtt.GetAttMultiplelist(Date, Month, Year, Convert.ToInt32(ddlEmp.SelectedItem.Value), RepType);
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
        }
        protected void gdvAttend_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "save")
            {
                int MID = Convert.ToInt32(e.CommandArgument);
                int retval;
                LinkButton lnkSave = new LinkButton();
                GridViewRow selectedRow = (GridViewRow)(((Control)e.CommandSource).NamingContainer);
                TextBox txtIN = (TextBox)selectedRow.FindControl("txtIN");
                TextBox txtOUT = (TextBox)selectedRow.FindControl("txtOUT");
                TextBox txtremarks = (TextBox)selectedRow.FindControl("txtRemarks");
                Label lblEmpID = (Label)selectedRow.FindControl("lblEmpID");
                Label lblDate = (Label)selectedRow.FindControl("lblDate");
                if (txtIN.Text != null && txtIN.Text != "" && txtOUT.Text != null && txtOUT.Text != "")
                {
                    if (Convert.ToDateTime(txtIN.Text) < Convert.ToDateTime(txtOUT.Text))
                    {
                        txtOUT.Text = txtOUT.Text;
                    }
                    else
                    {
                        AlertMsg.MsgBox(Page, "OUT TIME should be greater than IN TIME", AlertMsg.MessageType.Warning);
                        txtOUT.Text = string.Empty;
                        txtOUT.Focus();
                        return;
                    }
                }
                retval = Convert.ToInt32(AttendanceDAC.UpdateMultipleAtt(Convert.ToInt32(lblEmpID.Text), txtIN.Text.Trim(), txtOUT.Text.Trim(), txtremarks.Text, Convert.ToInt32(Session["UserId"]), MID, CodeUtilHMS.ConvertToDate_ddMMMyyy(lblDate.Text.Trim(), CodeUtilHMS.DateFormat.ddMMMyyyy)));
                if (retval == 3)
                {
                    AlertMsg.MsgBox(Page, "Intime/Outtime exist in previous entries in the same day", AlertMsg.MessageType.Warning);
                }
                else
                {
                    AlertMsg.MsgBox(Page, "Done");
                }
                BindGrid(2); // 2 means month report
            }
        }
    }
}