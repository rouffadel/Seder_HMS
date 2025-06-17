using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccessLayer;
using AECLOGIC.ERP.COMMON;
using Aeclogic.Common.DAL;
using System.Data.SqlClient;
using AECLOGIC.HMS.BLL;
using System.Data;
using System.Collections;
using System.IO;
namespace AECLOGIC.ERP.HMS
{
    public partial class VacationSetteled : AECLOGIC.ERP.COMMON.WebFormMaster
    {
        #region Variables
        int mid = 0;
        bool viewall;
        string menuname, Ext;
        string menuid;
        HRCommon objHrCommon = new HRCommon();
        int AttMonth, AttYear;
        string Form = "";
        #endregion Variables
        #region Events
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGrid();
                tblEdit.Visible = false;
            }
        }
        void AdvancedLeaveAppOthPaging_ShowRowsClick(object sender, EventArgs e)
        {
            AdvancedLeaveAppOthPaging.CurrentPage = 1;
            BindGrid();
        }
        void AdvancedLeaveAppOthPaging_FirstClick(object sender, EventArgs e)
        {
            BindGrid();
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            AdvancedLeaveAppOthPaging.CurrentPage = 1;
            BindGrid();
            tblEdit.Visible = false;
        }
        protected void gvVacation_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if ((e.Row.DataItem as DataRowView)["A6"].ToString() == "" || (e.Row.DataItem as DataRowView)["A7"].ToString() == "" || (e.Row.DataItem as DataRowView)["D6"].ToString() == "" || (e.Row.DataItem as DataRowView)["D7"].ToString() == "")
                {
                    (e.Row.DataItem as DataRowView)["A6"] = 0;
                    (e.Row.DataItem as DataRowView)["A7"] = 0;
                    (e.Row.DataItem as DataRowView)["D6"] = 0;
                    (e.Row.DataItem as DataRowView)["D7"] = 0;
                }
                e.Row.Cells[2].ToolTip = "A1=" + (e.Row.DataItem as DataRowView)["A1"].ToString() +
                                          "\nA2=" + (e.Row.DataItem as DataRowView)["A2"].ToString() +
                                          "\nA3=" + (e.Row.DataItem as DataRowView)["A3"].ToString() +
                                          "\nA4=" + (e.Row.DataItem as DataRowView)["A4"].ToString() +
                                          "\nA5=" + (e.Row.DataItem as DataRowView)["A5"].ToString() +
                                          "\nA6=" + (e.Row.DataItem as DataRowView)["A6"].ToString() +
                                          "\nA7=" + (e.Row.DataItem as DataRowView)["A7"].ToString()+
                                          "\ngratuity=" + (e.Row.DataItem as DataRowView)["gratuity"].ToString();
                e.Row.Cells[3].ToolTip = "D1=" + (e.Row.DataItem as DataRowView)["D1"].ToString() +
                                          "\nD2=" + (e.Row.DataItem as DataRowView)["D2"].ToString() +
                                          "\nD3=" + (e.Row.DataItem as DataRowView)["D3"].ToString() +
                                          "\nD4=" + (e.Row.DataItem as DataRowView)["D4"].ToString() +
                                          "\nD5=" + (e.Row.DataItem as DataRowView)["D5"].ToString() +
                                          "\nD6=" + (e.Row.DataItem as DataRowView)["D6"].ToString() +
                                          "\nD7=" + (e.Row.DataItem as DataRowView)["D7"].ToString() +
                                           "\nAdjAmt=" + (e.Row.DataItem as DataRowView)["AdjAmt"].ToString() +
                                            "\nEmpPen=" + (e.Row.DataItem as DataRowView)["EmpPen"].ToString(); 
            }
        }
        #endregion Events
        #region Methods
        protected override void OnInit(EventArgs e)
        {
            ModuleID = 1;
            base.OnInit(e);
            AdvancedLeaveAppOthPaging.FirstClick += new Paging.PageFirst(AdvancedLeaveAppOthPaging_FirstClick);
            AdvancedLeaveAppOthPaging.PreviousClick += new Paging.PagePrevious(AdvancedLeaveAppOthPaging_FirstClick);
            AdvancedLeaveAppOthPaging.NextClick += new Paging.PageNext(AdvancedLeaveAppOthPaging_FirstClick);
            AdvancedLeaveAppOthPaging.LastClick += new Paging.PageLast(AdvancedLeaveAppOthPaging_FirstClick);
            AdvancedLeaveAppOthPaging.ChangeClick += new Paging.PageChange(AdvancedLeaveAppOthPaging_FirstClick);
            AdvancedLeaveAppOthPaging.ShowRowsClick += new Paging.ShowRowsChange(AdvancedLeaveAppOthPaging_ShowRowsClick);
            AdvancedLeaveAppOthPaging.CurrentPage = 1;
        }
        public int GetParentMenuId()
        {
            string URL = Request.Url.Segments[Request.Url.Segments.Length - 1] + Request.Url.Query;
            int RoleId = Convert.ToInt32(Session["RoleId"].ToString());
            int ModuleId = ModuleID;
            DataSet ds = AttendanceDAC.GetAllowed(RoleId, ModuleId, URL);
            int MenuId = 0;
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                MenuId = Convert.ToInt32(ds.Tables[0].Rows[0]["Under"]);
                ViewState["Editable"] = (bool)ds.Tables[0].Rows[0]["Editable"];
                ViewState["ViewAll"] = (bool)ds.Tables[0].Rows[0]["ViewAll"];
                viewall = (bool)ViewState["ViewAll"];
                menuname = ds.Tables[0].Rows[0]["menuname"].ToString();
                menuid = MenuId.ToString();
                mid = Convert.ToInt32(ds.Tables[0].Rows[0]["MenuId"].ToString());
            }
            return MenuId;
        }
        void EmployeBindEdit(HRCommon objHrCommon)
        {
            try
            {
                int empid = 0;
                int month = 0;
                int year = 0;
                string ename = "";
                if (txtEmpNameHidden.Value != "" || txtEmpNameHidden.Value != string.Empty)
                {
                    empid = Convert.ToInt32(txtEmpNameHidden.Value);
                }
                else
                    empid = 0;
                if (txtEmpName.Text != "" || txtEmpName.Text != string.Empty)
                {
                    string s = txtEmpName.Text.ToString();
                    int index = s.IndexOf(' ');
                    ename = s.Substring(index + 1);
                }
                else
                    ename = null;
                if (txtdate.Text != "" || txtdate.Text != string.Empty)
                {
                    month = (DateTime.ParseExact(txtdate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).Month);
                    //month = Dmonth.Month;
                    year = (DateTime.ParseExact(txtdate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).Year);
                }
                DataSet startdate = AttendanceDAC.GetStartDate();
                // for Jan 2016 selection pay slip showing by Gana
                int Month, Year;
                Month = (DateTime.ParseExact(txtdate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).Month);
                //month = Dmonth.Month;
                Year = (DateTime.ParseExact(txtdate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).Year);
                AttMonth = Month;
                AttYear = Year;
                string st = Month + "/" + "01" + "/" + Year;
                DateTime stdt = CODEUtility.ConvertToDate(st, DateFormat.MonthDayYear);
                DateTime enddate = DateTime.ParseExact(txtdate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                int i = 0;
                try
                {
                    if (Session["vstatus"].ToString() == "1")
                    {
                        SqlParameter[] p = new SqlParameter[3];
                        p[0] = new SqlParameter("@Year", Year);
                        p[1] = new SqlParameter("@Empid", empid);
                        p[2] = new SqlParameter("@Form", "V");
                        i = Convert.ToInt32(SqlHelper.ExecuteScalar("HMS_CountVacationPost", p));
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                if (i != 0)
                {
                   // btnAccPost.Visible = false;
                    AlertMsg.MsgBox(Page, "Already Saved");
                    return;
                }
                else
                {
                    DataSet ds = new DataSet();
                    if (Form=="V")
                    {
                        ds = AttendanceDAC.T_HMS_GetVacation(objHrCommon, empid, ename, 0, 0, month, year, stdt, enddate,0);
                    }
                    else
                    {
                        SqlParameter[] sqlParams = new SqlParameter[13];
                        sqlParams[0] = new SqlParameter("@CurrentPage", objHrCommon.CurrentPage);
                        sqlParams[1] = new SqlParameter("@PageSize", objHrCommon.PageSize);
                        sqlParams[2] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
                        sqlParams[2].Direction = ParameterDirection.ReturnValue;
                        sqlParams[3] = new SqlParameter("@NoofRecords", System.Data.SqlDbType.Int);
                        sqlParams[3].Direction = ParameterDirection.Output;
                        sqlParams[4] = new SqlParameter("@Empid", empid);
                        sqlParams[5] = new SqlParameter("@Ename", ename);
                        sqlParams[6] = new SqlParameter("@WSid", 0);
                        sqlParams[7] = new SqlParameter("@Deptid", 0);
                        sqlParams[8] = new SqlParameter("@month", month);
                        sqlParams[9] = new SqlParameter("@year", year);
                        sqlParams[10] = new SqlParameter("@StDate", stdt);
                        sqlParams[11] = new SqlParameter("@EndDate", enddate);
                        sqlParams[12] = new SqlParameter("@leavetype", DBNull.Value);
                        ds = SQLDBUtil.ExecuteDataset("T_HMS_GetEmployeeVacation_FinalSettlement", sqlParams);
                    }
                    int tablcnt = ds.Tables.Count;
                    if (ds != null && ds.Tables.Count != 0 && ds.Tables[tablcnt - 1].Rows.Count > 0)
                    {
                        ViewState["DataSet"] = ds.Tables[tablcnt - 1];
                        dtlvacationEdit.DataSource = ds.Tables[tablcnt - 1];
                        dtlvacationEdit.DataBind();
                    }
                    else
                    {
                        dtlvacationEdit.DataSource = null;
                        dtlvacationEdit.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                clsErrorLog.HMSEventLog(ex, "Vactional Settlement", "EmployeBind", "007");
            }
        }
        static double Amt = 0.0;
        protected void QtyChangedEdit(object sender, EventArgs e)
        {
            decimal resultD1 = 0, resultD2 = 0;
            decimal totalA = 0, totalD = 0, Total = 0;
            foreach (DataListItem li in dtlvacationEdit.Items)
            {
                GridView gv = (GridView)li.FindControl("GVVacationedit");
                foreach (GridViewRow row in gv.Rows)
                {
                    TextBox Bookqty = (TextBox)row.FindControl("txtA1");
                    if (!chkFinal1.Checked)
                    {
                        if (row.RowIndex <= 7)
                        {
                            TextBox tb = (TextBox)row.FindControl("txtA1");
                            decimal sum;
                            if (decimal.TryParse(tb.Text.Trim(), out sum))
                            {
                                totalA += sum;
                            }
                        }
                        else
                        {
                            TextBox tb = (TextBox)row.FindControl("txtA1");
                            decimal sum;
                            if (decimal.TryParse(tb.Text.Trim(), out sum))
                            {
                                totalD += sum;
                            }
                        }
                        Total = totalA - totalD;
                    }
                    else
                    {
                        if (row.RowIndex <= 8)
                        {
                            TextBox tb = (TextBox)row.FindControl("txtA1");
                            decimal sum;
                            if (decimal.TryParse(tb.Text.Trim(), out sum))
                            {
                                totalA += sum;
                            }
                        }
                        else
                        {
                            TextBox tb = (TextBox)row.FindControl("txtA1");
                            decimal sum;
                            if (decimal.TryParse(tb.Text.Trim(), out sum))
                            {
                                totalD += sum;
                            }
                        }
                        Total = totalA - totalD;
                    }
                    Label lblval = (Label)gv.FooterRow.FindControl("lblvalue");
                    lblval.Text = Total.ToString();
                    Amt = Convert.ToDouble(Total);
                }
            }
        }
        string exptdetails = string.Empty;
        public DataView BindVacationDetails(string Id)
        {
            DataTable dt = new DataTable();
            DataRow row = dt.NewRow();
            SqlParameter[] sqlParams = new SqlParameter[1];
            sqlParams[0] = new SqlParameter("@id", Id);
            DataSet ds = SQLDBUtil.ExecuteDataset("sh_BindVacationDetails", sqlParams);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ButtonField btnMeds = new ButtonField();
                //Initalize the DataField value.
                btnMeds.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                btnMeds.CommandName = "Meds";
                btnMeds.ButtonType = ButtonType.Button;
                btnMeds.Text = "Meds";
                btnMeds.Visible = true;
                tblEdit.Visible = true;
                dt.Columns.Add("Description", typeof(string));
                dt.Columns.Add("Amount", typeof(string));
                dt.Columns.Add("Details", typeof(string));
                dt.Columns.Add("Det", typeof(string));
                dt.Rows.Add("A1: Salary For the Current Month Attendance ", ds.Tables[0].Rows[0]["A1"].ToString(), ds.Tables[0].Rows[0]["A1Label"].ToString());
                dt.Rows.Add("A2: Encashment of AL(EAL)", ds.Tables[0].Rows[0]["A2"].ToString(), ds.Tables[0].Rows[0]["A2Label"].ToString());
                dt.Rows.Add("A3: HRA for LOP ", ds.Tables[0].Rows[0]["A3"].ToString(), ds.Tables[0].Rows[0]["A3Label"].ToString());
                dt.Rows.Add("A4: Over-Time Amount", ds.Tables[0].Rows[0]["A4"].ToString(), ds.Tables[0].Rows[0]["A4Label"].ToString());
                dt.Rows.Add("A5: Air Ticket Reimbursement", ds.Tables[0].Rows[0]["A5"].ToString(), ds.Tables[0].Rows[0]["A5Label"].ToString());
                dt.Rows.Add("A6: Exit Entry Visa Reimbursement", ds.Tables[0].Rows[0]["A6"].ToString());
                dt.Rows.Add("A7: ", ds.Tables[0].Rows[0]["A7"].ToString());
                if (chkFinal1.Checked)
                {
                    dt.Rows.Add("Gratuity ", ds.Tables[0].Rows[0]["gratuity"].ToString(), ds.Tables[0].Rows[0]["GratuityRemarks"].ToString());
                }
                dt.Rows.Add("D1: Transport & Food ", ds.Tables[0].Rows[0]["D1"].ToString(), ds.Tables[0].Rows[0]["D1Label"].ToString());
                dt.Rows.Add("D2: Other manual deductions ", "0", "");
                dt.Rows.Add("D3: OutStanding  Advances", ds.Tables[0].Rows[0]["D3"].ToString());
                dt.Rows.Add("D4: Absent Penalty ", ds.Tables[0].Rows[0]["D4"].ToString(), ds.Tables[0].Rows[0]["D4Label"].ToString());
                dt.Rows.Add("D5: Expat ", 0, exptdetails, btnMeds);
                dt.Rows.Add("D6:", ds.Tables[0].Rows[0]["D6"].ToString());
                dt.Rows.Add("D7:", ds.Tables[0].Rows[0]["D7"].ToString());
                dt.Rows.Add("AdjAmt:", ds.Tables[0].Rows[0]["AdjAmt"].ToString());
                dt.Rows.Add("Emp Penalities:", ds.Tables[0].Rows[0]["EmpPen"].ToString());
                dt.Rows.Add("Remarks", "");
            }
            DataView dv = dt.DefaultView;
            return dv;
        }
        public void BindGrid()
        {
            try
            {
            objHrCommon.PageSize = AdvancedLeaveAppOthPaging.ShowRows;
            objHrCommon.CurrentPage = AdvancedLeaveAppOthPaging.CurrentPage;
            int   empid =0;
            if (txtEmpName.Text != "" || txtEmpName.Text != null)
                empid = Convert.ToInt32(txtEmpNameHidden.Value == "" ? "0" : txtEmpNameHidden.Value);
            else
                txtEmpNameHidden.Value = String.Empty;
           SqlParameter[] sqlParams = new SqlParameter[6];
           sqlParams[4] = new SqlParameter("@Empid", empid);
           sqlParams[0] = new SqlParameter("@CurrentPage", objHrCommon.CurrentPage);
           sqlParams[1] = new SqlParameter("@PageSize", objHrCommon.PageSize);
           sqlParams[2] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
           sqlParams[2].Direction = ParameterDirection.ReturnValue;
           sqlParams[3] = new SqlParameter("@NoofRecords", System.Data.SqlDbType.Int);
           sqlParams[3].Direction = ParameterDirection.Output;
            if(chkFinal1.Checked)
           sqlParams[5] = new SqlParameter("@Form", "F");
            else
                sqlParams[5] = new SqlParameter("@Form", "V");
           DataSet ds = SQLDBUtil.ExecuteDataset("HMS_GET_VacationDetails", sqlParams);
           objHrCommon.NoofRecords = (int)sqlParams[3].Value;
           objHrCommon.TotalPages = (int)sqlParams[2].Value;
           gvVacation.DataSource = ds;
           gvVacation.DataBind();
           AdvancedLeaveAppOthPaging.Bind(objHrCommon.CurrentPage, objHrCommon.TotalPages, objHrCommon.NoofRecords, objHrCommon.PageSize);
           txtEmpNameHidden.Value = "";
            }
            catch 
            {
            }
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionList_EmpName(string prefixText, int count, string contextKey)
        {
            DataSet ds = AttendanceDAC.GetSearchEmpName(prefixText);
            DataTable dt = ds.Tables[0];
            List<string> items = new List<string>(count);
            var rtval = new Dictionary<string, string>();
            foreach (DataRow row in dt.Rows)
            {
                string str = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(row["NAME"].ToString(), row["ID"].ToString());
                items.Add(str);
            }
            return items.ToArray(); ;// txtItems.ToArray();
        }
        #endregion Methods
        protected void gvVacation_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Edt")
            {
                GridViewRow gvRow = (GridViewRow)(((Control)e.CommandSource).NamingContainer);
                if(!chkFinal1.Checked)
                 Form = "V";
                else
                    Form = "F";
                Label lblcutdate = (Label)gvVacation.Rows[gvRow.RowIndex].FindControl("lblcutdate");
                txtdate.Text = lblcutdate.Text;
                EmployeBindEdit(objHrCommon);
                QtyChangedEdit(sender, e);
                tblEdit.Visible = true;
              //  BindVacationDetails(e.CommandArgument.ToString());
            }
            if (e.CommandName == "Print")
            {
                GridViewRow gvRow = (GridViewRow)(((Control)e.CommandSource).NamingContainer);
                int Empid; string date;
                    Empid = Convert.ToInt32(e.CommandArgument);
                    Label lblcutdate = (Label)gvVacation.Rows[gvRow.RowIndex].FindControl("lblcutdate");
                    date = (Convert.ToDateTime(lblcutdate.Text)).ToString("dd/MM/yyyy");
                               bool i;
                if (chkFinal1.Checked)
                    i = true;
                else
                    i = false;
                string url = "VacationalPrint.aspx?Empid=" + Empid + "&Date=" + date + "&Checked=" + i;
                string fullURL = "window.open('" + url + "', '_blank' );";
                ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", fullURL, true);
            }
        }
    }
}