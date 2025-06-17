using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using AECLOGIC.HMS.BLL;
using DataAccessLayer;
using AECLOGIC.ERP.COMMON;
using Aeclogic.Common.DAL;
using System.Configuration;
using System.Data.SqlClient;
namespace AECLOGIC.ERP.HMS
{
    public partial class Gratuity : AECLOGIC.ERP.COMMON.WebFormMaster
    {
        #region Variables
        int mid = 0;
        bool viewall;
        string menuname;
        int stateid;
        static int SiteSearch;
        int s = 0;
        static int EmpID;
        string menuid; static int CompanyID; static int SiteID, deptid;
        HRCommon objcommon = new HRCommon();
        AttendanceDAC objAtt = new AttendanceDAC();
        #endregion Variables
        #region Events
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    s = 0;
                    Page.Form.Attributes.Add("enctype", "multipart/form-data");
                    BindCompanyList();
                    BindYears();
                    //GetWorkSites();
                    txtCutDate.Text = DateTime.Now.Date.ToString("dd MMM yyyy");
                    txtFinalDate.Text = DateTime.Now.Date.ToString("dd MMM yyyy");
                    FIllObject.FillDropDown(ref ddlEmpDeAct, "sh_EmployeeExitReasons");
                    ddlEmpDeAct.SelectedIndex = 1;
                }
                CompanyID = Convert.ToInt32(Session["CompanyID"].ToString());
                if (Request.QueryString.Count > 0)
                {
                    if (Request.QueryString.AllKeys.Contains("EMPID"))
                    {
                        stateid = Convert.ToInt32(Request.QueryString["state"]);
                        txtFinalDate.Text = Request.QueryString["Date"];
                        ddlFAccPost.SelectedIndex = 0;
                        txtFEmpId.Text = "0000" + Request.QueryString["EMPID"];
                        SqlParameter[] p = new SqlParameter[1];
                        p[0] = new SqlParameter("@Empid", Request.QueryString["EMPID"]);
                        DataSet dstemp = SqlHelper.ExecuteDataset("HMS_GetWSByEmpID", p);
                        ddlFWorksite_hid.Value = dstemp.Tables[0].Rows[0][0].ToString();
                        ddlEmpDeAct.SelectedValue = Request.QueryString["EmpDeAct"];
                        BindGratuity();
                    }
                }
                if (Request.QueryString.Count > 0)
                {
                    stateid = Convert.ToInt32(Request.QueryString["state"]);//.Trim();
                    if (stateid == 1)
                    {
                        tblCutOff.Visible = true; tblMonthly.Visible = false; tblFinal.Visible = false; tblMcal.Visible = false; tblcal.Visible = false; btnTransferAcc.Visible = false;
                    }
                    else if (stateid == 2)
                    {
                        tblCutOff.Visible = false; tblMonthly.Visible = true; tblFinal.Visible = false; tblMcal.Visible = false; tblcal.Visible = false; btnTransferAcc.Visible = false;
                    }
                    else if (stateid == 3)
                    {
                        tblCutOff.Visible = false; tblMonthly.Visible = false; tblFinal.Visible = true; tblMcal.Visible = false; tblcal.Visible = false; btnTransferAcc.Visible = false;
                    }
                }
                else
                {
                    tblCutOff.Visible = false; tblMonthly.Visible = false; tblFinal.Visible = false;
                }
            }
            catch (Exception ex)
            {
                clsErrorLog.HMSEventLog(ex, "Gratuity", "Page_Load", "001");
            }
        }
        private void BindGratuity()
        {
            try
            {
                CompanyID = Convert.ToInt32(Session["CompanyID"].ToString());
                objcommon.PageSize = GrtPaging.ShowRows;
                objcommon.CurrentPage = GrtPaging.CurrentPage;
                if (txtFinalDate.Text != string.Empty)
                {
                    int accpost = Convert.ToInt32(ddlFAccPost.SelectedValue);
                    int b = 0;
                    DataTable datatable2 = new DataTable();
                    string[] fdate = txtFinalDate.Text.Split('/');
                    string fdt = fdate[1] + "/" + fdate[0] + "/" + fdate[2];
                    DateTime edt = CodeUtil.ConvertToDate(fdt, CodeUtil.DateFormat.MonthDayYear);
                    SiteID = Convert.ToInt32(ddlFWorksite_hid.Value);
                    DataSet ds = AttendanceDAC.GetGratuityDetails_Final(objcommon, CompanyID, SiteID, Request.QueryString["EMPID"], accpost, Convert.ToInt32(ddlEmpDeAct.SelectedValue), edt);
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            btnTransferAcc.Visible = true;
                            tblFCal.Visible = false;
                            if (b == 0)
                            {
                                for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
                                {
                                    datatable2.Columns.Add(ds.Tables[0].Columns[i].ColumnName);
                                }
                                b = 1;
                                DataTable datatable1 = (DataTable)ds.Tables[0];
                                foreach (DataRow dr1 in datatable1.Rows)
                                {
                                    object[] row = dr1.ItemArray;
                                    datatable2.Rows.Add(row);
                                }
                            }
                            else
                            {
                                DataTable datatable1 = (DataTable)ds.Tables[0];
                                foreach (DataRow dr1 in datatable1.Rows)
                                {
                                    object[] row = dr1.ItemArray;
                                    datatable2.Rows.Add(row);
                                }
                            }
                        }
                        else if (txtFEmpId.Text != string.Empty)
                        {
                            AlertMsg.MsgBox(Page, "Gratuity on account of this employees has already been posted to accounts");
                        }
                    }
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            gvFinal.DataSource = datatable2;
                            gvFinal.Visible = true;
                            gvEmpDetails.Visible = false;
                            tblcal.Visible = false;
                            if (Convert.ToInt32(ddlAccPost.SelectedValue) == 1)
                                btnTransferAcc.Visible = true;
                            else
                                btnTransferAcc.Visible = false;
                        }
                    }
                    else
                    {
                        gvFinal.DataSource = null;
                        gvFinal.EmptyDataText = "No Records Found";
                        GrtPaging.Visible = false;
                    }
                    gvFinal.DataBind();
                }
                else
                {
                    AlertMsg.MsgBox(Page, "Select Settlement Date");
                }
                //}
                GrtPaging.Bind(objcommon.CurrentPage, objcommon.TotalPages, objcommon.NoofRecords, objcommon.PageSize);
            }
            catch (Exception ex)
            {
                clsErrorLog.HMSEventLog(ex, "Gratuity", "BindGratuity", "006");
            }
        }
        protected void btnExcelExport_Click(object sender, EventArgs e)
        {
            if (ddlMMonth.SelectedIndex != 0)
            {
                try
                {
                    DataSet dss = SQLDBUtil.ExecuteDataset("sh_provisionmonthlyexcelcolumns");
                    DataSet ds = SQLDBUtil.ExecuteDataset("sh_provisionmonthlyexcel");
                    ExportToFileRev4(dss, "", "#EFEFEF", "#E6e6e6", "Provision_Details", "Provisions (Vacations, Ticket & Indemnity) :" + "" + "-" + Convert.ToString(ddlMMonth.SelectedItem) + "-" + Convert.ToString(ddlMYear.SelectedItem), "", "", ds);
                }
                catch (Exception ex)
                {
                    clsErrorLog.HMSEventLog(ex, "Gratuity", "btnExcelExport_Click", "006");
                }
            }
            else
            {
                AlertMsg.MsgBox(Page, "Select Month");
                return;
            }
        }
        private void ExportToFileRev4(DataSet sqlDataSet, String ItemColor, String AltItemColor, String CrossItemColor, String FileName, String Head, String SubHead, String Titil, DataSet ds)
        {
            ExportToFileRev4(sqlDataSet, ItemColor, AltItemColor, CrossItemColor, FileName + ".xls", "application/vnd.xls", Head, SubHead, Titil, ds);
        }
        private void ExportToFileRev4(DataSet sqlDataSet, String ItemColor, String AltItemColor, String CrossItemColor, String FileName, String ContentType, String Head, String SubHead, String Titil, DataSet ds)
        {
            //Clear the response
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + FileName);
            HttpContext.Current.Response.Charset = "";
            HttpContext.Current.Response.ContentType = ContentType;
            HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.UTF8;
            //Initialize the string that is used to build the file.
            HttpContext.Current.Response.Write("<table>");
            //Enumerate the field names and the records that are used to build 
            //the file.
            int CountSalary = sqlDataSet.Tables[0].Rows.Count;
            int CountClounms = CountSalary;
            if (Head.Trim() != "")
            {
                HttpContext.Current.Response.Write("<tr style='color:Black; background-color:White; font-weight:bold; font-size:26px; align-content:center; border-bottom-width:medium;'>");
                HttpContext.Current.Response.Write("<td colspan=" + CountClounms + ">" + Head.ToString() + "</td>");//colspan=" + CountClounms + " 
                HttpContext.Current.Response.Write("</tr>");
            }
            HttpContext.Current.Response.Write("<tr style='color:White; background-color:Navy; font-weight:bold; font-size:20px; border-bottom-width:medium;'>");
            foreach (DataRow dr in sqlDataSet.Tables[0].Rows)
            {
                if (dr["Name"].ToString() != "PayStartDt" && dr["Name"].ToString() != "PayEndDt")
                    HttpContext.Current.Response.Write("<td>" + dr["Name"].ToString() + "</td>");
            }
            HttpContext.Current.Response.Write("</tr>");
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {//write in new row
                HttpContext.Current.Response.Write("<TR>");
                for (int j = 0; j < ds.Tables[0].Columns.Count; j++)
                {
                    HttpContext.Current.Response.Write("<Td>");
                    HttpContext.Current.Response.Write(ds.Tables[0].Rows[i][j].ToString());
                    HttpContext.Current.Response.Write("</Td>");
                }
                HttpContext.Current.Response.Write("</TR>");
            }
            HttpContext.Current.Response.Write("</table>");
            HttpContext.Current.Response.End();
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                //Added by Rijwan : 26-04-2016
                GrtPaging.CurrentPage = 1;
                DataSet dsg = SqlHelper.ExecuteDataset("UPS_Gratuityconfig");
                if (dsg != null && dsg.Tables[0].Rows.Count > 0)
                {
                    if (stateid == 1)
                    {
                        if (Convert.ToInt32(ddlAccPost.SelectedValue) == 1)
                        {
                            gvEmpDetails.Visible = true;
                            gvGratuity.Visible = false;
                        }
                        else
                        {
                            gvEmpDetails.Visible = false;
                            gvGratuity.Visible = true;
                        }
                    }
                    else if (stateid == 2)
                    {
                        if (Convert.ToInt32(ddlMAccPost.SelectedValue) == 1)
                        {
                            gvEmpDetails.Visible = true;
                            gvMGratuity.Visible = false;
                        }
                        else
                        {
                            gvEmpDetails.Visible = false;
                            gvMGratuity.Visible = true;
                        }
                    }
                    else if (stateid == 3)
                    {
                        if (Convert.ToInt32(ddlFAccPost.SelectedValue) == 1)
                        {
                            gvEmpDetails.Visible = true;
                            gvFinal.Visible = false;
                        }
                        else
                        {
                            gvEmpDetails.Visible = false;
                            gvFinal.Visible = true;
                        }
                    }
                    else { }
                    if (stateid == 1)
                    {
                        if (ddlWorksite_hid.Value != string.Empty)
                            SiteID = Convert.ToInt32(ddlWorksite_hid.Value);
                        else
                            SiteID = 0;
                        if (ddlDepartment_hid.Value != string.Empty)
                            deptid = Convert.ToInt32(ddlDepartment_hid.Value);
                        else
                            deptid = 0;
                        try
                        {
                            if (txtName.Text == string.Empty)
                                EmpID = 0;
                            else if (txtName.Text.Length > 4)
                                EmpID = Convert.ToInt32(txtName.Text.Substring(0, 4));
                        }
                        catch { }
                    }
                    else if (stateid == 2)
                    {
                        if (ddlMWorksite_hid.Value != string.Empty)
                            SiteID = Convert.ToInt32(ddlMWorksite_hid.Value);
                        else
                            SiteID = 0;
                        if (ddlMDepartment_hid.Value != string.Empty)
                            deptid = Convert.ToInt32(ddlMDepartment_hid.Value);
                        else
                            deptid = 0;
                        if (txtMEmpId.Text == string.Empty)
                            EmpID = 0;
                        else if (txtMEmpId.Text.Length > 4)
                            EmpID = Convert.ToInt32(txtMEmpId.Text.Substring(0, 4));
                    }
                    else if (stateid == 3)
                    {
                        if (ddlFWorksite_hid.Value != string.Empty)
                            SiteID = Convert.ToInt32(ddlFWorksite_hid.Value);
                        else
                            SiteID = 0;
                        if (ddlFDepartment_hid.Value != string.Empty)
                            deptid = Convert.ToInt32(ddlFDepartment_hid.Value);
                        else
                            deptid = 0;
                        if (txtFEmpId.Text == string.Empty && txtFEmpId.Text.Length < 4)
                            EmpID = 0;
                        else if (txtFEmpId.Text.Length > 4)
                            EmpID = Convert.ToInt32(txtFEmpId.Text.Substring(0, 4));
                    }
                    tblcal.Visible = false;
                    if (stateid == 2)
                    {
                        if (Convert.ToInt32(ddlMAccPost.SelectedValue) == 1)
                        {
                            if (ddlMMonth.SelectedIndex > 0)
                            {
                                if (EmpID != 0)
                                {
                                    BindEmployeeDetails(SiteID, EmpID, deptid);
                                }
                                else
                                {
                                    BindEmployeeDetails(SiteID, EmpID, deptid);
                                }
                            }
                            else
                            {
                                AlertMsg.MsgBox(Page, "Select Month/Year");
                            }
                        }
                        else
                        {
                            //gvEmpDetails.Visible = false;
                            BindGridEmp(objcommon, SiteID);
                        }
                    }
                    else if (stateid == 1)
                    {
                        //if (ddlWorksite_hid.Value != string.Empty)
                        //{
                        if (Convert.ToInt32(ddlAccPost.SelectedValue) == 1)
                        {
                            if (txtCutDate.Text != string.Empty && s == 0)
                            {
                                if (EmpID != 0)
                                {
                                    BindEmployeeDetails(SiteID, EmpID, deptid);
                                    txtCutDate.Text = DateTime.Now.Date.ToString("dd MMM yyyy");
                                }
                                else
                                {
                                    BindEmployeeDetails(SiteID, EmpID, deptid);
                                    txtCutDate.Text = DateTime.Now.Date.ToString("dd MMM yyyy");
                                }
                            }
                        }
                        else
                        {
                            gvEmpDetails.Visible = false;
                            BindGridEmp(objcommon, SiteID);
                        }
                    }
                    else if (stateid == 3)
                    {
                        if (Convert.ToInt32(ddlFAccPost.SelectedValue) == 1)
                        {
                            if (EmpID != 0)
                            {
                                BindEmployeeDetails_Final(SiteID, EmpID, deptid);
                                txtFinalDate.Text = DateTime.Now.Date.ToString("dd MMM yyyy");
                            }
                            else
                            {
                                BindEmployeeDetails_Final(SiteID, EmpID, deptid);
                                txtFinalDate.Text = DateTime.Now.Date.ToString("dd MMM yyyy");
                            }
                        }
                        else
                        {
                            gvEmpDetails.Visible = false;
                            BindGridEmp(objcommon, SiteID);
                        }
                    }
                    txtCutDate.Text = DateTime.Now.Date.ToString("dd MMM yyyy");
                    //}
                }
                else
                    AlertMsg.MsgBox(Page, "Check Gratuity Configuration details", AlertMsg.MessageType.Warning);
            }
            catch (Exception ex)
            {
                clsErrorLog.HMSEventLog(ex, "Gratuity", "btnSearch_Click", "002");
            }
        }
        void GrtPaging_ShowRowsClick(object sender, EventArgs e)
        {
            GrtPaging.CurrentPage = 1;
            BindPager();
        }
        void GrtPaging_FirstClick(object sender, EventArgs e)
        {
            BindPager();
        }
        protected void btnCaluculate_Click(object sender, EventArgs e)
        {
            try
            {
                Label lblDate = new Label();
                //Added by Rijwan : 27-04-2016
                int i = 0;
                foreach (GridViewRow gvRow in gvEmpDetails.Rows)
                {
                    CheckBox chk = new CheckBox();
                    chk = (CheckBox)gvRow.FindControl("chkEToTransfer");
                    if (stateid == 1)
                    {
                        lblDate = (Label)gvRow.FindControl("lblDoj");
                    }
                    if (chk.Checked)
                    {
                        i = 1;
                    }
                }
                if (i == 1)
                {
                    objcommon.CurrentPage = GrtPaging.CurrentPage;
                    objcommon.PageSize = GrtPaging.ShowRows;
                    if (stateid == 1)
                    {
                        if (ddlWorksite_hid.Value != string.Empty)
                            SiteID = Convert.ToInt32(ddlWorksite_hid.Value);
                        else
                            SiteID = 0;
                        if (Convert.ToDateTime(lblDate.Text) > Convert.ToDateTime(txtCutDate.Text))
                        {
                            tblcal.Visible = true;
                            AlertMsg.MsgBox(Page, "Cut of Date should not accept Past date of DOJ!!!", AlertMsg.MessageType.Warning);
                            return;
                        }
                    }
                    else if (stateid == 2)
                    {
                        if (ddlMWorksite_hid.Value != string.Empty)
                            SiteID = Convert.ToInt32(ddlMWorksite_hid.Value);
                        else
                            SiteID = 0;
                    }
                    else if (stateid == 3)
                    {
                        if (ddlFWorksite_hid.Value != string.Empty)
                            SiteID = Convert.ToInt32(ddlFWorksite_hid.Value);
                        else
                            SiteID = 0;
                    }
                    gvFinal.Visible = false;
                    gvMGratuity.Visible = false;
                    gvGratuity.Visible = false;
                    BindGrid(objcommon, SiteID);
                }
                else
                {
                    if (stateid == 1)
                    {
                        tblcal.Visible = true;
                    }
                    else if (stateid == 2)
                    {
                        tblMcal.Visible = true;
                    }
                    else if (stateid == 3)
                    {
                        tblFCal.Visible = true;
                    }
                    AlertMsg.MsgBox(Page, "Please select atleast one checkbox", AlertMsg.MessageType.Warning);
                }
            }
            catch (Exception ex)
            {
                clsErrorLog.HMSEventLog(ex, "Gratuity", "btnCaluculate_Click", "003");
            }
        }
        protected void btClear_Click(object sender, EventArgs e)
        {
            //ddlWorksite.SelectedIndex = 0;
            ddlCompany.SelectedIndex = 0;
            //ddlYear.SelectedIndex = 0;
            txtName.Text = txtFEmpId.Text = txtMEmpId.Text = txtMSearchWorksite.Text = txtFSearchWorksite.Text = txtSearchWorksite.Text = textFdept.Text = textFdept.Text = textMdept.Text = "";
        }
        protected void gvGratuity_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.Header)
                {
                    CheckBox chkMail = (CheckBox)e.Row.FindControl("chkSelectAll");
                    chkMail.Attributes.Add("onclick", String.Format("javascript:SelectAll(this,'{0}','chkApproval');", gvGratuity.ClientID));
                }
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    if (Convert.ToInt32(ddlAccPost.SelectedValue) == 2) { e.Row.Cells[9].Enabled = false; }
                    //Added by Rijwan :27-04-2016
                    DateTime dt1 = Convert.ToDateTime(System.DateTime.Now.ToString("dd MMM yyyy"));
                    int lop = 0;
                    int empid = Convert.ToInt32((e.Row.DataItem as DataRowView)["EmpId"].ToString());
                    Double ArrAmount = Convert.ToDouble((e.Row.DataItem as DataRowView)["arrears"].ToString());
                    Double YearN = Convert.ToDouble((e.Row.DataItem as DataRowView)["Years"].ToString());
                    DateTime dt = Convert.ToDateTime(System.DateTime.Now.ToString("dd MMM yyyy"));
                    DataSet ds = PayRollMgr.InsUpdate_Encashment_AL_Grid(objcommon, 5, empid, dt, 0, 0, null);
                    try
                    {
                        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                        {
                            dt1 = Convert.ToDateTime(ds.Tables[0].Rows[0]["LVRD"].ToString());
                            try
                            {
                                lop = Convert.ToInt32(ds.Tables[0].Rows[0]["lop2"].ToString());
                            }
                            catch { lop = 0; }
                        }
                    }
                    catch { }
                    string ASALk = (e.Row.DataItem as DataRowView)["salary"].ToString();
                    string Allowance = (e.Row.DataItem as DataRowView)["Allowance"].ToString();
                    string GRTDAYSD = (e.Row.DataItem as DataRowView)["gdays"].ToString();
                    double ASAL = Convert.ToDouble(ASALk);
                    int GRTDAYS = Convert.ToInt32(GRTDAYSD.ToString());
                    int yearcal = 365;
                    double diff3 = (ASAL / yearcal) * GRTDAYS;
                    double Allow = Convert.ToDouble(Allowance);
                    double diffallow = (Allow);
                    diffallow = Math.Round(diffallow, 1, MidpointRounding.AwayFromZero);
                    diff3 = Math.Round(diff3, 1, MidpointRounding.AwayFromZero);
                    diff3 = diff3 + diffallow;
                    Double diff4 = Math.Round(diff3, 1, MidpointRounding.AwayFromZero);
                    string GRTAMTYear = (e.Row.DataItem as DataRowView)["gamt"].ToString();
                    double basic = (Convert.ToDouble(ASALk) - Allow);
                    DateTime doj = Convert.ToDateTime((e.Row.DataItem as DataRowView)["DOJ"].ToString());
                    DateTime dtt = Convert.ToDateTime((e.Row.DataItem as DataRowView)["cutdate"].ToString());
                    int diff = Convert.ToInt32(dtt.Subtract(doj).TotalDays.ToString("#"))+1;
                    int diff1 = Convert.ToInt32(diff - lop);
                    e.Row.Cells[4].ToolTip = "Basic = " + basic + "\nAllowance= " + Allow.ToString();
                    e.Row.Cells[5].ToolTip = "No. of Days(Cutoff Date - DOJ) = " + diff + "\nLoss of Pay= " + lop + "\nEligible Days = " + diff1;
                    e.Row.Cells[6].ToolTip = "Eligible Days = " + diff1 + "/365";
                    e.Row.Cells[8].ToolTip = "Amount = (Annual Basic salary(" + basic + ")/365 * Gratuity Day(Per Year))+(Allowance" + "(" + diffallow + ")" + "/365 * Gratuity Day(Per Year)) = " + diff4;
                    e.Row.Cells[9].ToolTip = "Gratuity Amount Per Year " + "(" + GRTAMTYear + ")" + " * No of Years" + "(" + YearN + ")" + "=" + ArrAmount;
                }
            }
            catch (Exception ex)
            {
                clsErrorLog.HMSEventLog(ex, "Gratuity", "gvGratuity_RowDataBound", "004");
            }
        }
        protected void gvEmpDetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.Header)
                {
                    CheckBox chkMail = (CheckBox)e.Row.FindControl("chkESelectAll");
                    chkMail.Attributes.Add("onclick", String.Format("javascript:SelectAll(this,'{0}','chkApproval');", gvEmpDetails.ClientID));
                }
            }
            catch (Exception ex)
            {
                clsErrorLog.HMSEventLog(ex, "Gratuity", "gvEmpDetails_RowDataBound", "005");
            }
        }
        protected void gvMGratuity_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.Header)
                {
                    CheckBox chkMMail = (CheckBox)e.Row.FindControl("chkMSelectAll");
                    chkMMail.Attributes.Add("onclick", String.Format("javascript:SelectAll(this,'{0}','chkApproval');", gvMGratuity.ClientID));
                }
            }
            catch (Exception ex)
            {
                clsErrorLog.HMSEventLog(ex, "Gratuity", "gvMGratuity_RowDataBound", "006");
            }
        }
        protected void gvFinal_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.Header)
                {
                    CheckBox chkFMail = (CheckBox)e.Row.FindControl("chkFSelectAll");
                    chkFMail.Attributes.Add("onclick", String.Format("javascript:SelectAll(this,'{0}','chkApproval');", gvFinal.ClientID));
                }
                if (Convert.ToInt32(ddlAccPost.SelectedValue) == 2) { e.Row.Cells[9].Enabled = false; }
                //Added by Rijwan :27-04-2016
                DateTime dt1 = Convert.ToDateTime(System.DateTime.Now.ToString("dd MMM yyyy"));
                int lop = 0;
                int empid = Convert.ToInt32((e.Row.DataItem as DataRowView)["EmpId"].ToString());
                //Double ArrAmount = Convert.ToDouble((e.Row.DataItem as DataRowView)["arrears"].ToString());
                Double YearN = Convert.ToDouble((e.Row.DataItem as DataRowView)["Years"].ToString());
                DateTime dt = Convert.ToDateTime(System.DateTime.Now.ToString("dd MMM yyyy"));
                DataSet ds = PayRollMgr.InsUpdate_Encashment_AL_Grid(objcommon, 5, empid, dt, 0, 0, null);
                try
                {
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        dt1 = Convert.ToDateTime(ds.Tables[0].Rows[0]["LVRD"].ToString());
                        try
                        {
                            lop = Convert.ToInt32(ds.Tables[0].Rows[0]["lop2"].ToString());
                        }
                        catch { lop = 0; }
                    }
                }
                catch { }
                string ASALk = (e.Row.DataItem as DataRowView)["salary"].ToString();
                string GRTDAYSD = (e.Row.DataItem as DataRowView)["gdays"].ToString();
                string Remarks = (e.Row.DataItem as DataRowView)["GratuityRemarks"].ToString();
                double ASAL = Convert.ToDouble(ASALk);
                int GRTDAYS = Convert.ToInt32(GRTDAYSD.ToString());
                int yearcal = 365;
                double diff3 = (ASAL / yearcal) * GRTDAYS;
                Double diff4 = Math.Round(diff3, 1, MidpointRounding.AwayFromZero);
                e.Row.Cells[8].ToolTip = "Amount = " + Remarks;
                e.Row.Cells[9].ToolTip = "Gratuity Amount Per Year " + "(" + diff4 + ")" + " * No of Years" + "(" + YearN + ")" + "=";
                DateTime doj = Convert.ToDateTime((e.Row.DataItem as DataRowView)["DOJ"].ToString());
                //DateTime dtt = Convert.ToDateTime((e.Row.DataItem as DataRowView)["cutdate"].ToString());
                //DateTime dtt = CodeUtil.ConvertToDate(txtFinalDate.Text, CodeUtil.DateFormat.MonthDayYear);
                string stDate;
                string[] std = txtFinalDate.Text.Split('/');
                stDate = std[1] + "/" + std[0] + "/" + std[2];
                DateTime dtt = Convert.ToDateTime(stDate);
                    //Convert.ToDateTime(txtFinalDate.Text);
                string Allowance = (e.Row.DataItem as DataRowView)["Allowance"].ToString();
                double basic = (Convert.ToDouble(ASALk) - Convert.ToDouble(Allowance));
                int diff = Convert.ToInt32((dtt-doj).TotalDays)+1;
                int diff1 = Convert.ToInt32(diff - lop);
                e.Row.Cells[5].ToolTip = "No. of Days(Cutoff Date - DOJ) = " + diff + "\nLoss of Pay= " + lop + "\nEligible Days = " + diff1;
                e.Row.Cells[4].ToolTip = "Basic = " + basic + "\nAllowance= " + Allowance.ToString();
                e.Row.Cells[6].ToolTip = "Eligible Days = " + diff1 + "/365";
            }
            catch (Exception ex)
            {
                clsErrorLog.HMSEventLog(ex, "Gratuity", "gvGratuity_RowDataBound", "004");
            }
        }
        protected void gvEmp_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.Header)
                {
                    CheckBox chkFMail = (CheckBox)e.Row.FindControl("chkESelectAll");
                    chkFMail.Attributes.Add("onclick", String.Format("javascript:SelectAll(this,'{0}','chkApproval');", gvFinal.ClientID));
                }
            }
            catch (Exception ex)
            {
                clsErrorLog.HMSEventLog(ex, "Gratuity", "gvEmp_RowDataBound", "008");
            }
        }
        protected void btnTransferAcc_Click(object sender, EventArgs e)
        {
            try
            {
                if (stateid == 1)
                {
                    DataSet dsTransferDetail = new DataSet("TranserDataSet");
                    DataTable dtTDT = new DataTable("TranserTable");
                    dtTDT.Columns.Add(new DataColumn("EmpID", typeof(System.Int32)));
                    dtTDT.Columns.Add(new DataColumn("Doj", typeof(System.DateTime)));
                    dtTDT.Columns.Add(new DataColumn("DebitAmt", typeof(System.Double)));
                    dtTDT.Columns.Add(new DataColumn("CreditAmt", typeof(System.Double)));
                    dtTDT.Columns.Add(new DataColumn("Totaldays", typeof(System.Int32)));
                    dtTDT.Columns.Add(new DataColumn("GratuityDays", typeof(System.Int32)));
                    dtTDT.Columns.Add(new DataColumn("GratuityAmt", typeof(System.Double)));
                    dtTDT.Columns.Add(new DataColumn("Basic", typeof(System.Double)));
                    dtTDT.Columns.Add(new DataColumn("GYear", typeof(System.Double)));
                    dtTDT.Columns.Add(new DataColumn("arrearamount", typeof(System.Double)));
                    dtTDT.Columns.Add(new DataColumn("month", typeof(System.Double)));
                    dtTDT.Columns.Add(new DataColumn("year", typeof(System.Double)));
                    dsTransferDetail.Tables.Add(dtTDT);
                    int EmpID = 0;
                    Double TotAmt = 0;
                    //DateTime edt121 = Convert.ToDateTime(txtCutDate.Text);
                    // txtCutDate.Text = edt121.ToString("dd/MM/yyyy");
                    string[] sdt = txtCutDate.Text.Split('/');
                    string st = sdt[1] + "/" + sdt[0] + "/" + sdt[2];
                    DateTime edt = CodeUtil.ConvertToDate(st, CodeUtil.DateFormat.MonthDayYear);
                    foreach (GridViewRow gvRow in gvGratuity.Rows)
                    {
                        CheckBox chk = new CheckBox();
                        chk = (CheckBox)gvRow.FindControl("chkToTransfer");
                        if (chk.Checked)
                        {
                            Label lblEmp = (Label)gvRow.FindControl("lblEmpId");
                            Label lblDoj = (Label)gvRow.FindControl("lblDoj");
                            Label lblBasic = (Label)gvRow.FindControl("lblBasic");
                            Label lblNoDay = (Label)gvRow.FindControl("lblNoDay");
                            Label lblGratuityDays = (Label)gvRow.FindControl("lblGratuityDays");
                            Label lblGratuityAmt = (Label)gvRow.FindControl("lblGratuityAmt");
                            Label lblGYears = (Label)gvRow.FindControl("lblYears");
                            TextBox lblarrearamount = (TextBox)gvRow.FindControl("lblArrearsAmt");
                            if (lblGratuityAmt.Text != string.Empty && lblGratuityDays.Text != string.Empty && lblarrearamount.Text != string.Empty)
                            {
                                int ERID = 0;
                                //AttendanceDAC.HR_EmpPenlitySetStatusTransfered(ERID);
                                EmpID = Convert.ToInt32(lblEmp.Text);
                                DateTime edt12 = Convert.ToDateTime(lblDoj.Text);
                                lblDoj.Text = edt12.ToString("dd/MM/yyyy");
                                string[] sdt1 = lblDoj.Text.Split('/');
                                string st1 = sdt1[1] + "/" + sdt1[0] + "/" + sdt1[2];
                                DateTime edt1 = CodeUtil.ConvertToDate(st1, CodeUtil.DateFormat.MonthDayYear);
                                DataRow dr = dtTDT.NewRow();
                                dr["EmpID"] = EmpID;
                                dr["Doj"] = edt1;
                                dr["DebitAmt"] = lblGratuityAmt.Text;
                                dr["CreditAmt"] = 0.00000;
                                dr["Totaldays"] = lblNoDay.Text;
                                dr["GratuityDays"] = lblGratuityDays.Text;
                                dr["GratuityAmt"] = lblGratuityAmt.Text;
                                dr["Basic"] = lblBasic.Text;
                                dr["GYear"] = lblGYears.Text;
                                dr["arrearamount"] = lblarrearamount.Text;
                                dr["month"] = sdt[1];
                                dr["year"] = sdt[2];
                                dtTDT.Rows.Add(dr);
                                string Remarks = "Gratuity";
                                dsTransferDetail.AcceptChanges();
                                DataSet ds = AttendanceDAC.HMS_GratuityTranserAccXML(dsTransferDetail, Convert.ToInt32(Session["UserId"]), Remarks, edt);
                            }
                        }
                    }
                    gvGratuity.Visible = false;
                    AlertMsg.MsgBox(Page, "A/C Updated!");
                    //Response.Redirect("Gratuity.aspx?state=1");
                }
                else if (stateid == 2)
                {
                    DataSet dsTransferDetail = new DataSet("TranserDataSet");
                    DataTable dtTDT = new DataTable("TranserTable");
                    dtTDT.Columns.Add(new DataColumn("EmpID", typeof(System.Int32)));
                    dtTDT.Columns.Add(new DataColumn("Doj", typeof(System.DateTime)));
                    dtTDT.Columns.Add(new DataColumn("DebitAmt", typeof(System.Double)));
                    dtTDT.Columns.Add(new DataColumn("CreditAmt", typeof(System.Double)));
                    dtTDT.Columns.Add(new DataColumn("Totaldays", typeof(System.Int32)));
                    dtTDT.Columns.Add(new DataColumn("GratuityDays", typeof(System.Int32)));
                    dtTDT.Columns.Add(new DataColumn("GratuityAmt", typeof(System.Double)));
                    dtTDT.Columns.Add(new DataColumn("Basic", typeof(System.Double)));
                    dtTDT.Columns.Add(new DataColumn("GYear", typeof(System.Double)));
                    dtTDT.Columns.Add(new DataColumn("arrearamount", typeof(System.Double)));
                    dtTDT.Columns.Add(new DataColumn("month", typeof(System.Double)));
                    dtTDT.Columns.Add(new DataColumn("year", typeof(System.Double)));
                    dsTransferDetail.Tables.Add(dtTDT);
                    int EmpID = 0;
                    Double TotAmt = 0;
                    DateTime edt121 = Convert.ToDateTime(txtMCutdate.Text);
                    txtMCutdate.Text = edt121.ToString("dd/MM/yyyy");
                    string[] sdt = txtMCutdate.Text.Split('/');
                    string st = sdt[1] + "/" + sdt[0] + "/" + sdt[2];
                    DateTime edt = CodeUtil.ConvertToDate(st, CodeUtil.DateFormat.MonthDayYear);
                    foreach (GridViewRow gvRow in gvMGratuity.Rows)
                    {
                        CheckBox chk = new CheckBox();
                        chk = (CheckBox)gvRow.FindControl("chkMToTransfer");
                        if (chk.Checked)
                        {
                            Label lblEmp = (Label)gvRow.FindControl("lblMEmpId");
                            Label lblDoj = (Label)gvRow.FindControl("lblMDoj");
                            Label lblBasic = (Label)gvRow.FindControl("lblMBasic");
                            Label lblNoDay = (Label)gvRow.FindControl("lblMDays");
                            Label lblGratuityDays = (Label)gvRow.FindControl("lblMGDays");
                            Label lblGratuityAmt = (Label)gvRow.FindControl("lblMGAmt");
                            Label lblGYears = (Label)gvRow.FindControl("lblMYears");
                            Label lblclosingbalance = (Label)gvRow.FindControl("lblclosingbalance");
                            if (lblGratuityAmt.Text != string.Empty && lblGratuityDays.Text != string.Empty)
                            {
                                int ERID = 0;
                                //AttendanceDAC.HR_EmpPenlitySetStatusTransfered(ERID);
                                EmpID = Convert.ToInt32(lblEmp.Text);
                                DateTime edt12 = Convert.ToDateTime(lblDoj.Text);
                                lblDoj.Text = edt12.ToString("dd/MM/yyyy");
                                string[] sdt1 = lblDoj.Text.Split('/');
                                string st1 = sdt1[1] + "/" + sdt1[0] + "/" + sdt1[2];
                                DateTime edt1 = CodeUtil.ConvertToDate(st1, CodeUtil.DateFormat.MonthDayYear);
                                DataRow dr = dtTDT.NewRow();
                                dr["EmpID"] = EmpID;
                                dr["Doj"] = edt1;
                                dr["DebitAmt"] = lblGratuityAmt.Text;
                                dr["CreditAmt"] = 0.00000;
                                dr["Totaldays"] = lblNoDay.Text;
                                dr["GratuityDays"] = lblGratuityDays.Text;
                                dr["GratuityAmt"] = lblGratuityAmt.Text;
                                dr["Basic"] = lblBasic.Text;
                                dr["GYear"] = lblGYears.Text;
                                dr["arrearamount"] = lblclosingbalance.Text;
                                dr["month"] = ddlMMonth.SelectedValue;
                                dr["year"] = ddlMYear.SelectedValue;
                                dtTDT.Rows.Add(dr);
                                string Remarks = "Gratuity";
                                dsTransferDetail.AcceptChanges();
                                DataSet ds = AttendanceDAC.HMS_GratuityTranserAccXML_Month(dsTransferDetail, Convert.ToInt32(Session["UserId"]), Remarks, edt);
                            }
                        }
                    }
                    gvMGratuity.Visible = false;
                    AlertMsg.MsgBox(Page, "A/C Updated!");
                    // Response.Redirect("Gratuity.aspx?state=2");
                }
                else if (stateid == 3)
                {
                    DataSet dsTransferDetail = new DataSet("TranserDataSet");
                    DataTable dtTDT = new DataTable("TranserTable");
                    dtTDT.Columns.Add(new DataColumn("EmpID", typeof(System.Int32)));
                    dtTDT.Columns.Add(new DataColumn("Doj", typeof(System.DateTime)));
                    dtTDT.Columns.Add(new DataColumn("DebitAmt", typeof(System.Double)));
                    dtTDT.Columns.Add(new DataColumn("CreditAmt", typeof(System.Double)));
                    dtTDT.Columns.Add(new DataColumn("Totaldays", typeof(System.Int32)));
                    dtTDT.Columns.Add(new DataColumn("GratuityDays", typeof(System.Int32)));
                    dtTDT.Columns.Add(new DataColumn("GratuityAmt", typeof(System.Double)));
                    dtTDT.Columns.Add(new DataColumn("Basic", typeof(System.Double)));
                    dtTDT.Columns.Add(new DataColumn("GYear", typeof(System.Double)));
                    dtTDT.Columns.Add(new DataColumn("arrearamount", typeof(System.Double)));
                    dsTransferDetail.Tables.Add(dtTDT);
                    int EmpID = 0;
                    Double TotAmt = 0;
                    foreach (GridViewRow gvRow in gvFinal.Rows)
                    {
                        CheckBox chk = new CheckBox();
                        chk = (CheckBox)gvRow.FindControl("chkFToTransfer");
                        if (chk.Checked)
                        {
                            Label lblEmp = (Label)gvRow.FindControl("lblFEmpId");
                            Label lblDoj = (Label)gvRow.FindControl("lblFDoj");
                            Label lblBasic = (Label)gvRow.FindControl("lblFBasic");
                            Label lblNoDay = (Label)gvRow.FindControl("lblFDays");
                            Label lblGratuityDays = (Label)gvRow.FindControl("lblFGDays");
                            Label lblGratuityAmt = (Label)gvRow.FindControl("lblFGAmt");
                            Label lblGYears = (Label)gvRow.FindControl("lblFYears");
                            //TextBox lblarrearamount = (TextBox)gvRow.FindControl("lblMArrearsAmt");
                            if (lblGratuityAmt.Text != string.Empty && lblGratuityDays.Text != string.Empty && lblNoDay.Text != string.Empty && lblBasic.Text != string.Empty)
                            {
                                int ERID = 0;
                                //AttendanceDAC.HR_EmpPenlitySetStatusTransfered(ERID);
                                EmpID = Convert.ToInt32(lblEmp.Text);
                                DateTime edt12 = Convert.ToDateTime(lblDoj.Text);
                                lblDoj.Text = edt12.ToString("dd/MM/yyyy");
                                string[] sdt1 = lblDoj.Text.Split('/');
                                string st1 = sdt1[1] + "/" + sdt1[0] + "/" + sdt1[2];
                                DateTime edt1 = CodeUtil.ConvertToDate(st1, CodeUtil.DateFormat.MonthDayYear);
                                DataRow dr = dtTDT.NewRow();
                                dr["EmpID"] = EmpID;
                                dr["Doj"] = edt1;
                                dr["DebitAmt"] = lblGratuityAmt.Text;
                                dr["CreditAmt"] = 0.00000;
                                dr["Totaldays"] = lblNoDay.Text;
                                dr["GratuityDays"] = lblGratuityDays.Text;
                                dr["GratuityAmt"] = lblGratuityAmt.Text;
                                dr["Basic"] = lblBasic.Text;
                                dr["GYear"] = lblGYears.Text;
                                dr["arrearamount"] = lblGratuityAmt.Text;
                                dtTDT.Rows.Add(dr);
                                string Remarks = "Gratuity";
                                dsTransferDetail.AcceptChanges();
                                DataSet ds = AttendanceDAC.HMS_GratuityTranserAccXML_Final(dsTransferDetail, Convert.ToInt32(Session["UserId"]), Remarks);
                            }
                        }
                    }
                    gvFinal.Visible = false;
                    AlertMsg.MsgBox(Page, "A/C Updated!");
                    //Response.Redirect("Gratuity.aspx?state=3");
                }
                txtCutDate.Text = DateTime.Now.Date.ToString("dd MMM yyyy");
            }
            catch (Exception ex)
            {
                clsErrorLog.HMSEventLog(ex, "Gratuity", "btnTransferAcc_Click", "009");
            }
        }
        #endregion Events
        #region Methods
        private string Convertdate(string StrDate)
        {
            if (StrDate != "")
            {
                try { StrDate = StrDate.Split('/')[1].ToString() + "/" + StrDate.Split('/')[0].ToString() + "/" + StrDate.Split('/')[2].ToString(); }
                catch { StrDate = StrDate.Split('-')[1].ToString() + "/" + StrDate.Split('-')[0].ToString() + "/" + StrDate.Split('-')[2].ToString(); }
            }
            return StrDate;
        }
        private void GetWorkSites()
        {
            DataSet dsWS = Common.GetWorkSites(CompanyID);
        }
        void BindPager()
        {
            try
            {
                objcommon.PageSize = GrtPaging.ShowRows;
                objcommon.CurrentPage = GrtPaging.CurrentPage;
                //if (txtCutDate.Text != string.Empty)
                //    BindGrid(objcommon, SiteID);
                //Added by Rijwan :26-04-2016
                if (stateid == 1)
                {
                    if (Convert.ToInt32(ddlAccPost.SelectedValue) == 1)
                    {
                        BindEmployeeDetails(SiteID, EmpID, deptid);
                    }
                    else
                    {
                        BindGridEmp(objcommon, 0);
                    }
                }
                else if (stateid == 2)
                {
                    if (Convert.ToInt32(ddlMAccPost.SelectedValue) == 1)
                    {
                        BindEmployeeDetails(SiteID, EmpID, deptid);
                    }
                    else
                    {
                        BindGridEmp(objcommon, 0);
                    }
                }
                else if (stateid == 3)
                {
                    if (Convert.ToInt32(ddlFAccPost.SelectedValue) == 1)
                    {
                        BindEmployeeDetails(SiteID, EmpID, deptid);
                    }
                    else
                    {
                        BindGridEmp(objcommon, 0);
                    }
                }
                else { }
            }
            catch (Exception ex)
            {
                clsErrorLog.HMSEventLog(ex, "Gratuity", "BindPager", "010");
            }
        }
        public void BindCompanyList()
        {
            DataSet dscomp = AttendanceDAC.HR_GetCompanyList();
            ddlCompany.DataSource = dscomp;
            ddlCompany.DataValueField = "companyid";
            ddlCompany.DataTextField = "companyname";
            ddlCompany.DataBind();
            ddlMCompany.DataSource = dscomp;
            ddlMCompany.DataValueField = "companyid";
            ddlMCompany.DataTextField = "companyname";
            ddlMCompany.DataBind();
            ddlFCompany.DataSource = dscomp;
            ddlFCompany.DataValueField = "companyid";
            ddlFCompany.DataTextField = "companyname";
            ddlFCompany.DataBind();
        }
        public void BindYears()
        {
            FIllObject.FillDropDown(ref ddlYear, "HMS_YearWise");
            DataSet ds = AttendanceDAC.GetCalenderYear();
            int i = 0;
            int Maxyear = Convert.ToInt32(ds.Tables[0].Rows[0]["MaxYear"].ToString());
            for (int Minyear = Convert.ToInt32(ds.Tables[0].Rows[0]["MinYear"].ToString()); Minyear <= Maxyear; Minyear++)
            {
                ddlYear.Items.Insert(0, new ListItem(Convert.ToString(Minyear), Convert.ToString(Minyear)));
                ddlMYear.Items.Insert(0, new ListItem(Convert.ToString(Minyear), Convert.ToString(Minyear)));
                i = i + 1;
            }
            ddlMonth.SelectedValue = ds.Tables[0].Rows[0]["CurrentMonth"].ToString();
            ddlYear.SelectedValue = ds.Tables[0].Rows[0]["CurrentYear"].ToString();
            ddlMYear.SelectedValue = ds.Tables[0].Rows[0]["CurrentYear"].ToString();
        }
        public void BindGrid(HRCommon objcommon, int WSID)
        {
            try
            {
                string mestBG = "1";
                try
                {
                    //  int year = Convert.ToInt32(ddlYear.SelectedValue);
                    if (stateid == 1)
                    {
                        CompanyID = Convert.ToInt32(ddlCompany.SelectedValue);
                        objcommon.PageSize = GrtPaging.ShowRows;
                        objcommon.CurrentPage = GrtPaging.CurrentPage;
                        DataSet ds = new DataSet();
                        DateTime edt12 = Convert.ToDateTime(txtCutDate.Text);
                        txtCutDate.Text = edt12.ToString("dd/MM/yyyy");
                        string[] sdt = txtCutDate.Text.Split('/');
                        string st = sdt[1] + "/" + sdt[0] + "/" + sdt[2];
                        // DateTime edt = DateTime.ParseExact(st, "MM/dd/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                        DateTime edt = CodeUtil.ConvertToDate(st, CodeUtil.DateFormat.MonthDayYear);
                        int month = Convert.ToInt32(ddlMonth.SelectedValue);
                        int year = Convert.ToInt32(ddlYear.SelectedValue);
                        int accpost = Convert.ToInt32(ddlAccPost.SelectedValue);
                        int b = 0;
                        DataTable datatable2 = new DataTable();
                        foreach (GridViewRow gvRow in gvEmpDetails.Rows)
                        {
                            CheckBox chk = new CheckBox();
                            chk = (CheckBox)gvRow.FindControl("chkEToTransfer");
                            if (chk.Checked)
                            {
                                Label lblsEmp = (Label)gvRow.FindControl("lblSEmpId");
                                ds = AttendanceDAC.GetGratuityDetails(objcommon, CompanyID, SiteID, lblsEmp.Text.Trim(), month, year, edt, accpost);
                                if (ds != null && ds.Tables.Count > 0)
                                {
                                    if (ds.Tables[0].Rows.Count > 0)
                                    {
                                        btnTransferAcc.Visible = true;
                                        if (b == 0)
                                        {
                                            for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
                                            {
                                                datatable2.Columns.Add(ds.Tables[0].Columns[i].ColumnName);
                                            }
                                            b = 1;
                                            DataTable datatable1 = (DataTable)ds.Tables[0];
                                            foreach (DataRow dr1 in datatable1.Rows)
                                            {
                                                object[] row = dr1.ItemArray;
                                                datatable2.Rows.Add(row);
                                            }
                                        }
                                        else
                                        {
                                            DataTable datatable1 = (DataTable)ds.Tables[0];
                                            foreach (DataRow dr1 in datatable1.Rows)
                                            {
                                                object[] row = dr1.ItemArray;
                                                datatable2.Rows.Add(row);
                                            }
                                        }
                                    }
                                    else if (txtName.Text != string.Empty)
                                    {
                                        AlertMsg.MsgBox(Page, "Gratuity on account of this employees has already been posted to accounts");
                                    }
                                }
                            }
                        }
                        if (ds != null && ds.Tables.Count > 0)
                        {
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                gvGratuity.DataSource = datatable2;
                                gvGratuity.Visible = true;
                                gvEmpDetails.Visible = false;
                                tblcal.Visible = false;
                                if (Convert.ToInt32(ddlAccPost.SelectedValue) == 1)
                                    btnTransferAcc.Visible = true;
                                else
                                    btnTransferAcc.Visible = false;
                            }
                        }
                        else
                        {
                            gvGratuity.DataSource = null;
                            gvGratuity.EmptyDataText = "No Records Found";
                            GrtPaging.Visible = false;
                        }
                        gvGratuity.DataBind();
                    }
                    else if (stateid == 2)
                    {
                        CompanyID = Convert.ToInt32(ddlMCompany.SelectedValue);
                        string ss = string.Empty;
                        objcommon.PageSize = GrtPaging.ShowRows;
                        objcommon.CurrentPage = GrtPaging.CurrentPage;
                        DataSet ds = new DataSet();
                        int month = Convert.ToInt32(ddlMMonth.SelectedValue);
                        int year = Convert.ToInt32(ddlMYear.SelectedValue);
                        int accpost = Convert.ToInt32(ddlMAccPost.SelectedValue);
                        int b = 0;
                        DataTable datatable2 = new DataTable();
                        foreach (GridViewRow gvRow in gvEmpDetails.Rows)
                        {
                            CheckBox chk = new CheckBox();
                            chk = (CheckBox)gvRow.FindControl("chkEToTransfer");
                            if (chk.Checked)
                            {
                                Label lblsEmp = (Label)gvRow.FindControl("lblSEmpId");
                                ds = AttendanceDAC.GetGratuityDetails_Monthly(objcommon, CompanyID, SiteID, lblsEmp.Text.Trim(), month, year, accpost);
                                if (ds != null && ds.Tables.Count > 0)
                                {
                                    if (ds.Tables[0].Rows.Count > 0)
                                    {
                                        if (ds.Tables[0].Rows[0][0].ToString() != "Already calculated for this employee for this month")
                                        {
                                            btnTransferAcc.Visible = true;
                                            tblMcal.Visible = false;
                                            if (b == 0)
                                            {
                                                for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
                                                {
                                                    datatable2.Columns.Add(ds.Tables[0].Columns[i].ColumnName);
                                                }
                                                b = 1;
                                                DataTable datatable1 = (DataTable)ds.Tables[0];
                                                foreach (DataRow dr1 in datatable1.Rows)
                                                {
                                                    object[] row = dr1.ItemArray;
                                                    datatable2.Rows.Add(row);
                                                }
                                            }
                                            else
                                            {
                                                DataTable datatable1 = (DataTable)ds.Tables[0];
                                                foreach (DataRow dr1 in datatable1.Rows)
                                                {
                                                    object[] row = dr1.ItemArray;
                                                    datatable2.Rows.Add(row);
                                                }
                                            }
                                        }
                                        else
                                        {
                                            tblMcal.Visible = true;
                                            AlertMsg.MsgBox(Page, "Gratuity on account of this employees has already been posted to accounts for this month", AlertMsg.MessageType.Warning);
                                            return;
                                        }
                                    }
                                    else if (txtMEmpId.Text != string.Empty)
                                    {
                                        AlertMsg.MsgBox(Page, "Gratuity on account of this employees has already been posted to accounts", AlertMsg.MessageType.Warning);
                                    }
                                    else
                                    {
                                        ss = ss + lblsEmp.Text.Trim() + ",";
                                    }
                                }
                            }
                        }
                        if (ds != null && ds.Tables.Count > 0)
                        {
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                txtMCutdate.Text = ds.Tables[0].Rows[0]["CutDate"].ToString();
                                gvMGratuity.DataSource = datatable2;
                                gvMGratuity.Visible = true;
                                gvEmpDetails.Visible = false;
                                tblFCal.Visible = false;
                                if (Convert.ToInt32(ddlAccPost.SelectedValue) == 1)
                                    btnTransferAcc.Visible = true;
                                else
                                    btnTransferAcc.Visible = false;
                                if (ss != string.Empty)
                                {
                                    AlertMsg.MsgBox(Page, "Gratuity on account of this employees has already been posted to accounts- Emp Id:" + ss);
                                }
                            }
                            else
                            {
                                AlertMsg.MsgBox(Page, "Gratuity on account of this employees has already been posted to accounts");
                            }
                        }
                        else
                        {
                            gvMGratuity.DataSource = null;
                            gvMGratuity.EmptyDataText = "No Records Found";
                            GrtPaging.Visible = false;
                        }
                        gvMGratuity.DataBind();
                    }
                    else if (stateid == 3)
                    {
                        CompanyID = Convert.ToInt32(ddlFCompany.SelectedValue);
                        DataSet ds = new DataSet();
                        objcommon.PageSize = GrtPaging.ShowRows;
                        objcommon.CurrentPage = GrtPaging.CurrentPage;
                        if (txtFinalDate.Text != string.Empty)
                        {
                            int accpost = Convert.ToInt32(ddlFAccPost.SelectedValue);
                            int b = 0;
                            DataTable datatable2 = new DataTable();
                            DateTime edt12 = Convert.ToDateTime(txtFinalDate.Text);
                            txtFinalDate.Text = edt12.ToString("dd/MM/yyyy");
                            string[] fdate = txtFinalDate.Text.Split('/');
                            string fdt = fdate[1] + "/" + fdate[0] + "/" + fdate[2];
                            DateTime edt = CodeUtil.ConvertToDate(fdt, CodeUtil.DateFormat.MonthDayYear);
                            foreach (GridViewRow gvRow in gvEmpDetails.Rows)
                            {
                                CheckBox chk = new CheckBox();
                                chk = (CheckBox)gvRow.FindControl("chkEToTransfer");
                                if (chk.Checked)
                                {
                                    Label lblsEmp = (Label)gvRow.FindControl("lblSEmpId");
                                    ds = AttendanceDAC.GetGratuityDetails_Final(objcommon, CompanyID, SiteID, lblsEmp.Text.Trim(), accpost, Convert.ToInt32(ddlEmpDeAct.SelectedValue), edt);
                                    if (ds != null && ds.Tables.Count > 0)
                                    {
                                        if (ds.Tables[0].Rows.Count > 0)
                                        {
                                            btnTransferAcc.Visible = true;
                                            tblFCal.Visible = false;
                                            if (b == 0)
                                            {
                                                for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
                                                {
                                                    datatable2.Columns.Add(ds.Tables[0].Columns[i].ColumnName);
                                                }
                                                b = 1;
                                                DataTable datatable1 = (DataTable)ds.Tables[0];
                                                foreach (DataRow dr1 in datatable1.Rows)
                                                {
                                                    object[] row = dr1.ItemArray;
                                                    datatable2.Rows.Add(row);
                                                }
                                            }
                                            else
                                            {
                                                DataTable datatable1 = (DataTable)ds.Tables[0];
                                                foreach (DataRow dr1 in datatable1.Rows)
                                                {
                                                    object[] row = dr1.ItemArray;
                                                    datatable2.Rows.Add(row);
                                                }
                                            }
                                        }
                                        else if (txtFEmpId.Text != string.Empty)
                                        {
                                            AlertMsg.MsgBox(Page, "Gratuity on account of this employees has already been posted to accounts");
                                        }
                                    }
                                }
                            }
                            if (ds != null && ds.Tables.Count > 0)
                            {
                                if (ds.Tables[0].Rows.Count > 0)
                                {
                                    gvFinal.DataSource = datatable2;
                                    gvFinal.Visible = true;
                                    gvEmpDetails.Visible = false;
                                    tblcal.Visible = false;
                                    if (Convert.ToInt32(ddlAccPost.SelectedValue) == 1)
                                        btnTransferAcc.Visible = true;
                                    else
                                        btnTransferAcc.Visible = false;
                                }
                            }
                            else
                            {
                                gvFinal.DataSource = null;
                                gvFinal.EmptyDataText = "No Records Found";
                                GrtPaging.Visible = false;
                            }
                            gvFinal.DataBind();
                        }
                        else
                        {
                            AlertMsg.MsgBox(Page, "Select Settlement Date");
                        }
                    }
                    GrtPaging.Bind(objcommon.CurrentPage, objcommon.TotalPages, objcommon.NoofRecords, objcommon.PageSize);
                }
                catch (Exception e)
                {
                    // throw e;
                    AlertMsg.MsgBox(Page, e.Message, AlertMsg.MessageType.Warning);
                }
            }
            catch (Exception ex)
            {
                clsErrorLog.HMSEventLog(ex, "Gratuity", "BindGrid", "011");
            }
        }
        public void BindGridEmp(HRCommon objcommon, int WSID)
        {
            try
            {
                //  int year = Convert.ToInt32(ddlYear.SelectedValue);
                int month = 0;
                CompanyID = Convert.ToInt32(ddlCompany.SelectedValue);
                objcommon.PageSize = GrtPaging.ShowRows;
                objcommon.CurrentPage = GrtPaging.CurrentPage;
                DateTime edt122 = Convert.ToDateTime(txtCutDate.Text);
                txtCutDate.Text = edt122.ToString("dd/MM/yyyy");
                txtFinalDate.Text = edt122.ToString("dd/MM/yyyy");
                DateTime edt = DateTime.ParseExact(txtCutDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                month = Convert.ToInt32(ddlMonth.SelectedValue);
                if (stateid == 1)
                {
                    if (Convert.ToInt32(ddlAccPost.SelectedValue) == 2)
                    {
                        month = Convert.ToInt32(ddlMonth.SelectedValue);
                    }
                }
                else if (stateid == 2)
                {
                    if (Convert.ToInt32(ddlMAccPost.SelectedValue) == 2)
                    {
                        month = Convert.ToInt32(ddlMMonth.SelectedValue);
                    }
                }
                int year = Convert.ToInt32(ddlYear.SelectedValue);
                string empid = "";
                int accpost = 0;
                int wsids = 0;
                int Deptid = 0;
                int id = 0;
                if (stateid == 1)
                {
                    //if (txtEmpID.Text != string.Empty)
                    //    empid = txtEmpID.Text.Trim();
                    if (txtName.Text != string.Empty)
                        empid = txtName.Text.Substring(0, 4);
                    else
                        empid = null;
                    accpost = Convert.ToInt32(ddlAccPost.SelectedValue);
                    if (ddlWorksite_hid.Value != string.Empty)
                        wsids = Convert.ToInt32(ddlWorksite_hid.Value);
                    if (ddlDepartment_hid.Value != string.Empty)
                        Deptid = Convert.ToInt32(ddlDepartment_hid.Value);
                    id = 1;
                }
                else if (stateid == 2)
                {
                    if (txtMEmpId.Text != string.Empty)
                        empid = txtMEmpId.Text.Substring(0, 4);
                    else
                        empid = null;
                    accpost = Convert.ToInt32(ddlMAccPost.SelectedValue);
                    if (ddlMWorksite_hid.Value != string.Empty)
                        wsids = Convert.ToInt32(ddlMWorksite_hid.Value);
                    if (ddlMDepartment_hid.Value != string.Empty)
                        Deptid = Convert.ToInt32(ddlMDepartment_hid.Value);
                    id = 2;
                }
                else if (stateid == 3)
                {
                    if (txtFEmpId.Text != string.Empty)
                        empid = txtFEmpId.Text.Substring(0, 4);
                    else
                        empid = null;
                    accpost = Convert.ToInt32(ddlFAccPost.SelectedValue);
                    if (ddlFWorksite_hid.Value != string.Empty)
                        wsids = Convert.ToInt32(ddlFWorksite_hid.Value);
                    if (ddlFDepartment_hid.Value != string.Empty)
                        Deptid = Convert.ToInt32(ddlFDepartment_hid.Value);
                    id = 3;
                }
                DataSet ds = AttendanceDAC.GetGratuityDetailsSearch(objcommon, CompanyID, WSID, Deptid, empid, month, year, edt, accpost, id);
                if (stateid == 1)
                {
                    if (ds != null && ds.Tables[0].Rows.Count > 0 && ds.Tables.Count > 0)
                    {
                        gvGratuity.Visible = true;
                        gvGratuity.DataSource = ds;
                        GrtPaging.Bind(objcommon.CurrentPage, objcommon.TotalPages, objcommon.NoofRecords, objcommon.PageSize);
                    }
                    else
                    {
                        gvGratuity.DataSource = null;
                        gvGratuity.EmptyDataText = "No Records Found";
                        GrtPaging.Visible = false;
                    }
                    gvGratuity.DataBind();
                }
                else if (stateid == 2)
                {
                    if (ds != null && ds.Tables[0].Rows.Count > 0 && ds.Tables.Count > 0)
                    {
                        gvMGratuity.Visible = true;
                        gvMGratuity.DataSource = ds;
                        GrtPaging.Bind(objcommon.CurrentPage, objcommon.TotalPages, objcommon.NoofRecords, objcommon.PageSize);
                    }
                    else
                    {
                        gvMGratuity.DataSource = null;
                        gvMGratuity.EmptyDataText = "No Records Found";
                        GrtPaging.Visible = false;
                    }
                    gvMGratuity.DataBind();
                }
                else if (stateid == 3)
                {
                    if (ds != null && ds.Tables[0].Rows.Count > 0 && ds.Tables.Count > 0)
                    {
                        gvFinal.Visible = true;
                        gvFinal.DataSource = ds;
                        GrtPaging.Bind(objcommon.CurrentPage, objcommon.TotalPages, objcommon.NoofRecords, objcommon.PageSize);
                    }
                    else
                    {
                        gvFinal.DataSource = null;
                        gvFinal.EmptyDataText = "No Records Found";
                        GrtPaging.Visible = false;
                    }
                    gvFinal.DataBind();
                }
            }
            catch (Exception ex)
            {
                clsErrorLog.HMSEventLog(ex, "Gratuity", "BindGridEmp", "012");
            }
        }
        protected override void OnInit(EventArgs e)
        {
            ModuleID = 1;
            base.OnInit(e);
            GrtPaging.FirstClick += new Paging.PageFirst(GrtPaging_FirstClick);
            GrtPaging.PreviousClick += new Paging.PagePrevious(GrtPaging_FirstClick);
            GrtPaging.NextClick += new Paging.PageNext(GrtPaging_FirstClick);
            GrtPaging.LastClick += new Paging.PageLast(GrtPaging_FirstClick);
            GrtPaging.ChangeClick += new Paging.PageChange(GrtPaging_FirstClick);
            GrtPaging.ShowRowsClick += new Paging.ShowRowsChange(GrtPaging_ShowRowsClick);
            GrtPaging.CurrentPage = 1;
        }
        protected void Getdept(object sender, EventArgs e)
        {
            //Added by Rijwan :25-04-2016
            if (stateid == 1)
            {
                if (textdept.Text == "") { ddlDepartment_hid.Value = ""; }
            }
            else if (stateid == 2)
            {
                if (textMdept.Text == "") { ddlMDepartment_hid.Value = ""; }
            }
            else if (stateid == 3)
            {
                if (textFdept.Text == "") { ddlFDepartment_hid.Value = ""; }
            }
            else { }
        }
        private void BindEmployeeDetails_Final(int siteid, int empid, int deptid)
        {
            try
            {
                //Added by Rijwan : 26-04-2016
                objcommon.CurrentPage = GrtPaging.CurrentPage;
                objcommon.PageSize = GrtPaging.ShowRows;
                SqlParameter[] p = new SqlParameter[10];
                p[0] = new SqlParameter("@siteid", siteid);
                p[1] = new SqlParameter("@Empid", empid);
                p[2] = new SqlParameter("@deptid", deptid);
                p[3] = new SqlParameter("@CurrentPage", objcommon.CurrentPage);
                p[4] = new SqlParameter("@PageSize", objcommon.PageSize);
                p[5] = new SqlParameter("@NoofRecords", System.Data.SqlDbType.Int);
                p[5].Direction = ParameterDirection.Output;
                p[6] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
                p[6].Direction = ParameterDirection.ReturnValue;
                if (stateid == 1)
                    p[7] = new SqlParameter("@id", 1);
                else if (stateid == 2)
                    p[7] = new SqlParameter("@id", 2);
                else if (stateid == 3)
                    p[7] = new SqlParameter("@id", 3);
                if (stateid == 2)
                {
                    p[8] = new SqlParameter("@month", ddlMMonth.SelectedValue);
                    p[9] = new SqlParameter("@year", ddlMYear.SelectedValue);
                }
                else
                {
                    p[8] = new SqlParameter("@month", System.Data.SqlDbType.Int);
                    p[9] = new SqlParameter("@year", System.Data.SqlDbType.Int);
                }
                gvEmpDetails.DataSource = null;
                gvEmpDetails.DataBind();
                DataSet ds = SqlHelper.ExecuteDataset("USP_BindEmployeeDetails_Gratuity_Final", p);
                objcommon.NoofRecords = (int)p[5].Value;
                objcommon.TotalPages = (int)p[6].Value;
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count <= 0) { AlertMsg.MsgBox(Page, "Gratuity on account of this employees has already been posted to accounts"); }
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        tblEmp.Visible = true;
                        tblcal.Visible = false;
                        btnTransferAcc.Visible = false;
                        gvEmpDetails.DataSource = ds;
                        gvEmpDetails.DataBind();
                        if (stateid == 1)
                            tblcal.Visible = true;
                        else if (stateid == 2)
                            tblMcal.Visible = true;
                        else if (stateid == 3)
                            tblFCal.Visible = true;
                    }
                }
                GrtPaging.Bind(objcommon.CurrentPage, objcommon.TotalPages, objcommon.NoofRecords, objcommon.PageSize);
            }
            catch (Exception ex)
            {
                clsErrorLog.HMSEventLog(ex, "Gratuity", "BindEmployeeDetails_Final", "013");
            }
        }
        private void BindEmployeeDetails(int siteid, int empid, int deptid)
        {
            try
            {
                //Added by Rijwan : 26-04-2016
                objcommon.CurrentPage = GrtPaging.CurrentPage;
                objcommon.PageSize = GrtPaging.ShowRows;
                SqlParameter[] p = new SqlParameter[10];
                p[0] = new SqlParameter("@siteid", siteid);
                p[1] = new SqlParameter("@Empid", empid);
                p[2] = new SqlParameter("@deptid", deptid);
                p[3] = new SqlParameter("@CurrentPage", objcommon.CurrentPage);
                p[4] = new SqlParameter("@PageSize", objcommon.PageSize);
                p[5] = new SqlParameter("@NoofRecords", System.Data.SqlDbType.Int);
                p[5].Direction = ParameterDirection.Output;
                p[6] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
                p[6].Direction = ParameterDirection.ReturnValue;
                if (stateid == 1)
                    p[7] = new SqlParameter("@id", 1);
                else if (stateid == 2)
                    p[7] = new SqlParameter("@id", 2);
                else if (stateid == 3)
                    p[7] = new SqlParameter("@id", 3);
                if (stateid == 2)
                {
                    p[8] = new SqlParameter("@month", ddlMMonth.SelectedValue);
                    p[9] = new SqlParameter("@year", ddlMYear.SelectedValue);
                }
                else
                {
                    p[8] = new SqlParameter("@month", System.Data.SqlDbType.Int);
                    p[9] = new SqlParameter("@year", System.Data.SqlDbType.Int);
                }
                gvEmpDetails.DataSource = null;
                gvEmpDetails.DataBind();
                DataSet ds = SqlHelper.ExecuteDataset("USP_BindEmployeeDetails_Gratuity", p);
                objcommon.NoofRecords = (int)p[5].Value;
                objcommon.TotalPages = (int)p[6].Value;
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count <= 0) { AlertMsg.MsgBox(Page, "Gratuity on account of this employees has already been posted to accounts!", AlertMsg.MessageType.Warning); }
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        tblEmp.Visible = true;
                        tblcal.Visible = false;
                        btnTransferAcc.Visible = false;
                        gvEmpDetails.DataSource = ds;
                        gvEmpDetails.DataBind();
                        if (stateid == 1)
                            tblcal.Visible = true;
                        else if (stateid == 2 || stateid == 3)
                            tblMcal.Visible = true;
                    }
                }
                GrtPaging.Bind(objcommon.CurrentPage, objcommon.TotalPages, objcommon.NoofRecords, objcommon.PageSize);
            }
            catch (Exception ex)
            {
                clsErrorLog.HMSEventLog(ex, "Gratuity", "BindEmployeeDetails", "014");
            }
        }
        protected void GetWork(object sender, EventArgs e)
        {
            //Added by Rijwan :25-04-2016
            if (stateid == 1)
            {
                if (txtSearchWorksite.Text == "") { SiteSearch = 0; ddlWorksite_hid.Value = ""; }
                else
                {
                    SiteSearch = Convert.ToInt32(ddlWorksite_hid.Value == "" ? "0" : ddlWorksite_hid.Value);
                }
                if (Convert.ToInt32(ddlAccPost.SelectedValue) == 1)
                {
                    // tblcal.Visible = true;
                }
            }
            else if (stateid == 2)
            {
                if (txtMSearchWorksite.Text == "") { SiteSearch = 0; ddlMWorksite_hid.Value = ""; }
                else
                {
                    SiteSearch = Convert.ToInt32(ddlMWorksite_hid.Value == "" ? "0" : ddlMWorksite_hid.Value);
                }
                if (Convert.ToInt32(ddlMAccPost.SelectedValue) == 1)
                {
                    // tblMcal.Visible = true;
                }
            }
            else if (stateid == 3)
            {
                if (txtFSearchWorksite.Text == "") { SiteSearch = 0; ddlFWorksite_hid.Value = ""; }
                else
                {
                    SiteSearch = Convert.ToInt32(ddlFWorksite_hid.Value == "" ? "0" : ddlFWorksite_hid.Value);
                }
                if (Convert.ToInt32(ddlFAccPost.SelectedValue) == 1)
                {
                    //tblMcal.Visible = true;
                }
            }
            else { }
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionList(string prefixText, int count, string contextKey)
        {
            //DataSet ds = AttendanceDAC.GetGoogleABCSearchWorkSite(prefixText, SearchCompanyID);
            //return ConvertStingArray(ds);// txtItems.ToArray();
            DataSet ds = AttendanceDAC.GetWorkSiteActive(prefixText);
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
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionList_dept(string prefixText, int count, string contextKey)
        {
            DataSet ds = AttendanceDAC.BindDeparmetBySite_googlesearch(prefixText, 1, CompanyID);
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
        //Added by Rijwan : 26-04-2016
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionListEmp(string prefixText, int count, string contextKey)
        {
            DataSet ds = AttendanceDAC.GetGoogleSerachAllEmployee(prefixText);
            DataTable dt = ds.Tables[0];
            List<string> items = new List<string>(count);
            var rtval = new Dictionary<string, string>();
            foreach (DataRow row in dt.Rows)
            {
                string str = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(row["Name"].ToString(), row["ID"].ToString());
                items.Add(str);
            }
            return items.ToArray(); // txtItems.ToArray();
        }
        #endregion Methods
    }
}