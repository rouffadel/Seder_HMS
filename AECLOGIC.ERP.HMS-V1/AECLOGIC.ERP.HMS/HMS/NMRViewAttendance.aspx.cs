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


namespace AECLOGIC.ERP.HMS
{
    public partial class NMRViewAttendance : AECLOGIC.ERP.COMMON.WebFormMaster
    {

        int mid = 0;
        bool viewall;
        string menuname;
        string menuid;

        TableRow tblRow;
        TableCell tcName, tcDesig, tcDate, tcScope, tcEmpID, tcLnkbtn, tcPaySlip;
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
            lblHead.Text = "View Attendance";
           
            if (!IsPostBack)
            {

                try
                {
                    AttendanceDAC objAtt = new AttendanceDAC();

                    txtDay.Text = DateTime.Now.ToString(ConfigurationManager.AppSettings["DateFormat"]);
                    hdn.Value = "day";
                    BindYears();
                    if (Request.QueryString.Count > 0)
                    {


                    }
                    else
                    {

                        if (Convert.ToInt32(Session["MonitorSite"]) != 0)
                        {
                            ddlWorksite.Items.FindByValue(Session["MonitorSite"].ToString()).Selected = true;
                            ddlWorksite.Enabled = false;
                        }
                        else
                        {

                            GetDayReport();
                            ddlWorksite.SelectedValue = Session["Site"].ToString();
                            ddlWorksite_SelectedIndexChanged(ddlWorksite, e);
                        }



                    }
                    btnDaySearch.Attributes.Add("onclick", "javascript:return Validate('" + DateTime.Now.ToShortDateString() + "','" + CODEUtility.ConvertToDate(txtDay.Text.Trim(), DateFormat.DayMonthYear) + "');");
                   


                }
                catch
                {

                }


            }
        }
      
        protected void btnDaySearch_Click(object sender, EventArgs e)
        {
            hdn.Value = "day";
            GetDayReport();

        }
        private void BindYears()
        {
            for (int jLoop = 2000; jLoop <= DateTime.Now.Year; jLoop++)
            {
                ddlYear.Items.Add(jLoop.ToString());
            }
        }

        protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (hdn.Value == "day")
                GetDayReport();
            if (hdn.Value == "MON")
                GetMonthlyReport();
        }
        protected void ddlWorksite_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (hdn.Value == "day")
                GetDayReport();
            if (hdn.Value == "MON")
                GetMonthlyReport();
        }
        protected void ddlMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            hdn.Value = "MON";
            GetMonthlyReport();
        }
        protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            hdn.Value = "MON";
            GetMonthlyReport();
        }

        protected void GetDayReport()
        {
              
            AttendanceDAC objAtt = new AttendanceDAC();
            TimeSpan Work;
            DateTime Intime = DateTime.Now, OutTime = DateTime.Now;
            int DeptID; int WSID;
            try { DeptID = Convert.ToInt32(ddlDepartment.SelectedValue); }
            catch { DeptID = 0; }
            try { WSID = Convert.ToInt32(ddlWorksite.SelectedValue); }
            catch { WSID = 0; }

            DataSet ds = null;
            if (txtDay.Text != "")
            {
               // txtDay.Text = txtDay.Text.Replace('-', '/');
                ds = objAtt.GetNMRAttendanceByDay_Cursor(CODEUtility.ConvertToDate(txtDay.Text.Trim(), DateFormat.DayMonthYear), DeptID, WSID, 0);
                DataSet Filter = AttendanceDAC.HR_GetNMR_WS_DeptFilter(WSID);
                ddlDepartment.DataSource = Filter.Tables[1];
                ddlDepartment.DataTextField = "DepartmentName";
                ddlDepartment.DataValueField = "DepartmentUId";
                ddlDepartment.DataBind();
                ddlDepartment.Items.Insert(0, new ListItem("---ALL---", "0", true));

                ddlWorksite.DataSource = Filter.Tables[0];
                ddlWorksite.DataTextField = "Site_Name";
                ddlWorksite.DataValueField = "Site_ID";
                ddlWorksite.DataBind();
                ddlWorksite.Items.Insert(0, new ListItem("---ALL---", "0", true));
            }

            int count = ds.Tables.Count;
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                tblAtt.Rows.Clear();

                tblRow = new TableRow();
                tcName = new TableCell();
                tcName.Text = "Name";
                tcName.Style.Add("font-weight", "bold");
                tcName.Width = 200;
                tblRow.Cells.Add(tcName);
                tcDesig = new TableCell();
                tcDesig.Text = "Department";
                tcDesig.Style.Add("font-weight", "bold");
                tcDesig.Width = 200;
                tblRow.Cells.Add(tcDesig);
                tcDate = new TableCell();
                tcDate.Text = "In Time";
                tcDate.Style.Add("font-weight", "bold");
                tcDate.Width = 80;
                tblRow.Cells.Add(tcDate);
                tcDate = new TableCell();
                tcDate.Text = "Out Time";
                tcDate.Style.Add("font-weight", "bold");
                tcDate.Width = 80;
                tblRow.Cells.Add(tcDate);

                tcDate = new TableCell();
                tcDate.Text = "Total Work";
                tcDate.Style.Add("font-weight", "bold");
                tcDate.Width = 80;
                tblRow.Cells.Add(tcDate);

                tcDate = new TableCell();
                tcDate.Text = "Remarks";
                tcDate.Style.Add("font-weight", "bold");
                tcDate.Width = 200;
                tblRow.Cells.Add(tcDate);

                tblAtt.Rows.Add(tblRow);

                for (int j = 0; j < count; j++)
                {
                    tblRow = new TableRow();
                    tcName = new TableCell();
                    if (ds.Tables[j].Rows.Count != 0)
                    {
                        tcName.Text = ds.Tables[j].Rows[0][3].ToString();
                        tcName.Width = 200;
                        tblRow.Cells.Add(tcName);
                        tcDesig = new TableCell();
                        tcDesig.Text = ds.Tables[j].Rows[0][6].ToString();
                        tcDesig.Width = 200;
                        tblRow.Cells.Add(tcDesig);

                        tcDate = new TableCell();
                        if (ds.Tables[j].Rows[0][4].ToString() != "")
                        {
                            tcDate.Text = ds.Tables[j].Rows[0][4].ToString();
                           Intime = Convert.ToDateTime(ds.Tables[j].Rows[0][4]);
                        }
                        else
                            tcDate.Text = "-";
                        tcDate.Width = 80;
                        tblRow.Cells.Add(tcDate);

                        tcDate = new TableCell();
                        if (ds.Tables[j].Rows[0][5].ToString() != "")
                        {
                            tcDate.Text = ds.Tables[j].Rows[0][5].ToString();
                           OutTime = Convert.ToDateTime(ds.Tables[j].Rows[0][5]);
                        }
                        else
                            tcDate.Text = "-";
                        tcDate.Width = 80;
                        tblRow.Cells.Add(tcDate);
                        Work = OutTime.Subtract(Intime);

                        tcDate = new TableCell();
                        if (ds.Tables[j].Rows[0][5].ToString() != "" || ds.Tables[j].Rows[0][4].ToString() != "")
                        {
                            tcDate.Text = Work.ToString() + " Hrs";
                        }
                        if (ds.Tables[j].Rows[0][5].ToString() == "" || ds.Tables[j].Rows[0][4].ToString() == "")
                        {
                            tcDate.Text = "00:00:00 Hrs";
                        }
                        tcDate.Width = 80;
                        tblRow.Cells.Add(tcDate);

                        tcDate = new TableCell();
                        if (ds.Tables[j].Rows[0][7].ToString() != "")
                            tcDate.Text = ds.Tables[j].Rows[0][7].ToString();
                        else
                            tcDate.Text = "NA";
                        tcDate.Width = 100;
                        tblRow.Cells.Add(tcDate);
                        tblAtt.Rows.Add(tblRow);
                    }
                }
            }
        }

        protected void GetMonthlyReport()
        {
            DataSet ds = null;
            AttendanceDAC objAtt = new AttendanceDAC();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ds = objAtt.GetNMRAttendanceByMonth_Cursor(Convert.ToInt32(ddlMonth.SelectedValue), Convert.ToInt32(ddlYear.SelectedValue), Convert.ToInt32(ddlDepartment.SelectedValue), Convert.ToInt32(ddlWorksite.SelectedValue), 0);

                DataSet Filter = AttendanceDAC.HR_GetNMR_WS_DeptFilter(Convert.ToInt32(ddlWorksite.SelectedValue));
                ddlDepartment.DataSource = Filter.Tables[1];
                ddlDepartment.DataTextField = "DepartmentName";
                ddlDepartment.DataValueField = "DepartmentUId";
                ddlDepartment.DataBind();
                ddlDepartment.Items.Insert(0, new ListItem("---ALL---", "0", true));

                ddlWorksite.DataSource = Filter.Tables[0];
                ddlWorksite.DataTextField = "Site_Name";
                ddlWorksite.DataValueField = "Site_ID";
                ddlWorksite.DataBind();
                ddlWorksite.Items.Insert(0, new ListItem("---ALL---", "0", true));
            }

            int count = ds.Tables.Count;
            if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
            {

                int Present, Scope = 0;
                if (ddlMonth.SelectedIndex != 0)
                {
                    DateTime dt = new DateTime(Convert.ToInt32(ddlYear.SelectedValue), Convert.ToInt32(ddlMonth.SelectedValue), 1);
                    DateTime dtEnd = dt.AddMonths(1);

                    tblRow = new TableRow();

                    tcEmpID = new TableCell();
                    tcEmpID.Text = "EmpID";
                    tcEmpID.Style.Add("font-weight", "bold");
                    tcEmpID.Width = 40;
                    tblRow.Cells.Add(tcEmpID);

                    tcName = new TableCell();
                    tcName.Text = "Name";
                    tcName.Style.Add("font-weight", "bold");
                    tcName.Width = 300;
                    tblRow.Cells.Add(tcName);

                    tcDesig = new TableCell();
                    tcDesig.Text = "Department";
                    tcDesig.Style.Add("font-weight", "bold");
                    tcDesig.Width = 200;
                    tblRow.Cells.Add(tcDesig);

                    for (int i = 1; dt != dtEnd; i++)
                    {
                        tcDate = new TableCell();
                        tcDate.Text = i.ToString();
                        tcDate.Style.Add("font-weight", "bold");
                        tcDate.Width = 60;
                        tblRow.Cells.Add(tcDate);
                        dt = dt.AddDays(1);
                        Scope++;

                    }


                    tcScope = new TableCell();
                    tcScope.Text = "Scope";
                    tcScope.Style.Add("font-weight", "bold");
                    tcScope.Width = 60;
                    tblRow.Cells.Add(tcScope);

                    tcDate = new TableCell();
                    tcDate.Text = "Presents";
                    tcDate.Style.Add("font-weight", "bold");
                    tcDate.Width = 60;
                    tblRow.Cells.Add(tcDate);


                    tblAtt.Rows.Add(tblRow);

                    dt = new DateTime(Convert.ToInt32(ddlYear.SelectedValue), Convert.ToInt32(ddlMonth.SelectedValue), 1);
                    dtEnd = dt.AddMonths(1);
                    for (int j = 0; j < count; j++)
                    {
                        tblRow = new TableRow();
                        tcName = new TableCell();
                        tcEmpID = new TableCell();

                        if (ds.Tables[j].Rows.Count != 0)
                        {
                            if (ds.Tables[j].Rows[0][0].ToString() != null && ds.Tables[j].Rows[0][0].ToString() != "" && ds.Tables[j].Rows[0][0].ToString() != string.Empty)
                            {
                                tcEmpID.Text = ds.Tables[j].Rows[0][0].ToString();
                                tcEmpID.Width = 300;
                                tblRow.Cells.Add(tcEmpID);
                            }
                            if (ds.Tables[j].Rows[0][3].ToString() != null && ds.Tables[j].Rows[0][3].ToString() != "" && ds.Tables[j].Rows[0][3].ToString() != string.Empty)
                            {
                                tcName.Text = ds.Tables[j].Rows[0][3].ToString();
                                tcName.Width = 300;
                                tblRow.Cells.Add(tcName);
                            }

                            tcDesig = new TableCell();
                            if (ds.Tables[j].Rows[0][3].ToString() != null && ds.Tables[j].Rows[0][3].ToString() != "" && ds.Tables[j].Rows[0][3].ToString() != string.Empty)
                            {
                                tcDesig.Text = ds.Tables[j].Rows[0][6].ToString();
                                tcDesig.Width = 200;
                                tblRow.Cells.Add(tcDesig);
                            }

                            Present = 0;
                            for (int i = 1; dt != dtEnd; i++)
                            {
                                tcDate = new TableCell();
                                if (ds.Tables[j].Rows.Count > 0)
                                {
                                    for (int k = 0; k < ds.Tables[j].Rows.Count; k++)
                                    {
                                        if (ds.Tables[j].Rows[k][2].ToString() != "")
                                        {
                                            if (dt == Convert.ToDateTime(ds.Tables[j].Rows[k][2]).Date)
                                            {
                                                tcDate.Text = ds.Tables[j].Rows[k][1].ToString();
                                                if (ds.Tables[j].Rows[k][1].ToString() == "P" || ds.Tables[j].Rows[k][1].ToString() == "OD")
                                                {
                                                    tcDate.Style.Add("color", "green");
                                                    Present++;

                                                }
                                                else
                                                    tcDate.Style.Add("color", "red");
                                                break;
                                            }
                                            else
                                            {
                                                tcDate.Text = "-";
                                            }

                                        }
                                        else
                                        {
                                            tcDate.Text = "-";
                                        }
                                    }
                                }
                                tcDate.Width = 60;
                                tblRow.Cells.Add(tcDate);
                                dt = dt.AddDays(1);
                            }

                            tcScope = new TableCell();
                            tcScope.Text = Scope.ToString();
                            tcScope.HorizontalAlign = HorizontalAlign.Right;
                            tcScope.Width = 60;
                            tblRow.Cells.Add(tcScope);


                            tcDate = new TableCell();
                            tcDate.Text = Present.ToString();
                            tcDate.HorizontalAlign = HorizontalAlign.Right;
                            tcDate.Width = 60;
                            tblRow.Cells.Add(tcDate);

                          

                            tblAtt.Rows.Add(tblRow);
                            dt = new DateTime(Convert.ToInt32(ddlYear.SelectedValue), Convert.ToInt32(ddlMonth.SelectedValue), 1); ;
                            dtEnd = dt.AddMonths(1);
                        }
                    }

                }
            }
        }
    }
}