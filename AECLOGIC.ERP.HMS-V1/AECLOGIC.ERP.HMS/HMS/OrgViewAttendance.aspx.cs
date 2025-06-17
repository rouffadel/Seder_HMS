using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AECLOGIC.HMS.BLL;

namespace AECLOGIC.ERP.HMS
{
    public partial class OrgViewAttendance : AECLOGIC.ERP.COMMON.WebFormMaster
    {
        TableRow tblRow;
        TableCell tcName, tcDesig, tcDate;
        DataSet ds = new DataSet();
        protected override void OnInit(EventArgs e)
        {
            ModuleID = 1;
            base.OnInit(e);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            DataSet dstemp = new DataSet();
            lblHead.Text = "View Attendance";
            if (!IsPostBack)
            {
                BindYears();
                GetMonthlyReport();
            }
        }

        public void BindYears()
        {
            ds = AttendanceDAC.GetCalenderYear();

            int i = 0;
            int Maxyear = Convert.ToInt32(ds.Tables[0].Rows[0]["MaxYear"].ToString());
            for (int Minyear = Convert.ToInt32(ds.Tables[0].Rows[0]["MinYear"].ToString()); Minyear <= Maxyear; Minyear++)
            {
                ddlYear.Items.Insert(i, new ListItem(Convert.ToString(Minyear), Convert.ToString(Minyear)));
                i = i + 1;
            }
            ddlMonth.SelectedValue = ds.Tables[0].Rows[0]["CurrentMonth"].ToString();
            ddlYear.SelectedValue = ds.Tables[0].Rows[0]["CurrentYear"].ToString();
        }
        protected void GetMonthlyReport()
        {
            tblAtt.Rows.Clear();
            DataSet ds = new DataSet();
            AttendanceDAC objAtt = new AttendanceDAC();
            int empid = 0;
            empid = Convert.ToInt32(Request.QueryString["EmpId"].ToString());
            string Name = string.Empty;
            ds = objAtt.GetAttendanceByMonth_Cursor(Convert.ToInt32(ddlMonth.SelectedValue), Convert.ToInt32(ddlYear.SelectedValue), 0, 0, empid, Name, null);
            int count = ds.Tables.Count;
            if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
            {
                int Present = 0;
                if (ddlMonth.SelectedIndex != 0)
                {
                    DateTime dt = new DateTime(Convert.ToInt32(ddlYear.SelectedValue), Convert.ToInt32(ddlMonth.SelectedValue), 1);
                    DateTime dtEnd = dt.AddMonths(1);
                    tblRow = new TableRow();
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
                    }
                    tcDate = new TableCell();
                    tcDate.Text = "Presents";
                    tcDate.Style.Add("font-weight", "bold");
                    tcDate.Width = 60;
                    tblRow.Cells.Add(tcDate);
                    tblAtt.Rows.Add(tblRow);

                    dt = new DateTime(Convert.ToInt32(ddlYear.SelectedValue), Convert.ToInt32(ddlMonth.SelectedValue), 1);
                    dtEnd = dt.AddMonths(1);
                    for (int j = 0; j < count - 1; j++)
                    {
                        tblRow = new TableRow();
                        tcName = new TableCell();
                        if (ds.Tables[j].Rows.Count != 0)
                        {
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
                                                if (ds.Tables[j].Rows[k][1].ToString() == "P")
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
                            tcDate = new TableCell();
                            tcDate.Text = Present.ToString();
                            tcDate.Width = 60;
                            tblRow.Cells.Add(tcDate);

                            tblAtt.Rows.Add(tblRow);
                            dt = new DateTime(Convert.ToInt32(ddlYear.SelectedValue), Convert.ToInt32(ddlMonth.SelectedValue), 1);
                            dtEnd = dt.AddMonths(1);
                        }
                    }
                }
            }
        }

        protected void ddlMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetMonthlyReport();
        }
        protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetMonthlyReport();
        }
        protected void btnShow_Click(object sender, EventArgs e)
        {
            GetMonthlyReport();
        }
    }
}
