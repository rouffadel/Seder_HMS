using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using AECLOGIC.HMS.BLL;
using AECLOGIC.ERP.COMMON;
using System.Data.SqlClient;
using Aeclogic.Common.DAL;
using System.Globalization;
namespace AECLOGIC.ERP.HMS
{
    public partial class VacationSettlement : AECLOGIC.ERP.COMMON.WebFormMaster
    {
        #region Variables
        string exptdetails = string.Empty;
        decimal EALGlobal;
        int AttMonth, AttYear;
        AttendanceDAC objatt = new AttendanceDAC();
        DataSet ds = new DataSet();
        int mid = 0;
        static double Amt = 0.0;
        static int empid = 0;
        static int chk = 0;
        static decimal oty = 0;
        bool viewall, Editable;
        string menuname;
        string menuid;
        HRCommon objHrCommon = new HRCommon();
        DateTime stdt;
        DateTime eddt;
        #endregion Variables

        #region Events
        protected void Page_Load(object sender, EventArgs e)
        {
          
            try
            {

               
                if (!IsPostBack)
                {
                    GetParentMenuId();
                    FIllObject.FillDropDown(ref ddlworksite, "HR_GetWorkSite");
                    tractions.Visible = false;
                    trwarning.Visible = false;
                    tddetails.Visible = false;
                    tickimgk.Visible = false;
                    notfoundk.Visible = true;
                    BindDesignations();
                    txtDOS.Text = DateTime.Now.ToString("dd-MMM-yyyy");
                    ViewState["CateId"] = "";
                    if (Convert.ToInt32(Request.QueryString["key"]) == 1)
                    {
                        
                        tblNewk.Visible = true;
                        tblEdit.Visible = false;

                    }
                    else
                    {
                        tblNewk.Visible = false;
                        tblEdit.Visible = true;
                    }
                }
            }
            catch (Exception ex)
            {
                clsErrorLog.HMSEventLog(ex, "Vactional Settlement", "Page_Load", "001");
            }
        }
        void EmpListPaging_ShowRowsClick(object sender, EventArgs e)
        {
           
            BindPager();
        }
        void EmpListPaging_FirstClick(object sender, EventArgs e)
        {
            BindPager();
        }
        protected void txtempid_changed(object sender, EventArgs e)
        {
            if (txtempid.Text.Trim() != "")
            {
                AttendanceDAC objAtt = new AttendanceDAC();
                int empid = Convert.ToInt32(txtempid.Text.Trim());
                DataSet ds1 = objAtt.GetEmployeeDetails(empid);
                if (ds1.Tables[0].Rows.Count > 0)
                {
                    lblempdetails.Text = ds1.Tables[0].Rows[0]["FName"].ToString() + ' ' + ds1.Tables[0].Rows[0]["MName"].ToString() + ' ' + ds1.Tables[0].Rows[0]["LName"].ToString();
                    tickimgk.Visible = true;
                    notfoundk.Visible = false;
                    lblempdetails.BackColor = System.Drawing.Color.Yellow;
                    lblempdetails.ForeColor = System.Drawing.Color.Green;

                }
                else
                {
                    lblempdetails.Text = "Employee Id: [" + txtempid.Text.Trim() + "] Not found or Invalid!";
                    lblempdetails.ForeColor = System.Drawing.Color.Red;
                    lblempdetails.BackColor = System.Drawing.Color.Yellow;
                    txtempid.Text = "";
                    tickimgk.Visible = false;
                    notfoundk.Visible = true;
                }
                //Salary details
                DataSet dsApp = objAtt.GetAppDetails(empid);
                if (dsApp.Tables[0].Rows.Count > 0)
                {
                    trwarning.Visible = false;
                    tddetails.Visible = true;
                    tractions.Visible = true;
                    lblDOJ.Text = Convert.ToDateTime(dsApp.Tables[0].Rows[0]["doj"]).ToString("dd-MMM-yyyy"); //Convert.ToDateTime(dsApp.Tables[0].Rows[0]["doj"]).ToString(System.Web.Configuration.WebConfigurationManager.AppSettings["DateFormat"]);
                    lblsal.Text = dsApp.Tables[0].Rows[0]["salary"].ToString();
                    lblROJ.Text = "../../....";
                    lbllastsattlemtdt.Text = "../../....";
                    lbldurations.Text = dsApp.Tables[0].Rows[0]["Contract_Yr"].ToString() + " [Years] " + dsApp.Tables[0].Rows[0]["Contract_Month"].ToString() + " [Months] " + dsApp.Tables[0].Rows[0]["Contract_days"].ToString() + " [Days]";

                    try { lbldateofEndCOntract.Text = Convert.ToDateTime(dsApp.Tables[0].Rows[0]["EOC_date"]).ToString("dd-MMM-yyyy"); }// Convert.ToDateTime(dsApp.Tables[0].Rows[0]["EOC_date"]).ToString(System.Web.Configuration.WebConfigurationManager.AppSettings["DateFormat"]); }
                    catch
                    {
                        lbldateofEndCOntract.Text = "../../....";
                        trwarning.Visible = true;
                        tddetails.Visible = false;
                        tractions.Visible = false;
                    };
                }
                else
                {
                    lblDOJ.Text = "";
                    lblsal.Text = "";
                    lblROJ.Text = "";
                    lbllastsattlemtdt.Text = "";
                    lbldurations.Text = "";
                    lbldateofEndCOntract.Text = "";
                }
                calculations();
            }

        }
        protected void btn_reste_Click(object sender, EventArgs e)
        {

            txtempid.Text = "";
            lblempdetails.Text = "";


            btnSubmit.Text = "Submit";
            ViewState["CateId"] = "0";
        }
        protected void gvWages_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int ID = Convert.ToInt32(e.CommandArgument);
            ViewState["CateId"] = ID;
            bool Status = true;
            
            if (e.CommandName == "Edt")
            {

                BindDetails(ID);
            }
            else
            {
                try
                {
                   
                    EmployeBind(objHrCommon);

                }
                catch (Exception DelDesig)
                {
                    AlertMsg.MsgBox(Page, DelDesig.Message.ToString(),AlertMsg.MessageType.Error);
                }
            }
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                int CateId = 0;
                if (ViewState["CateId"].ToString() != null && ViewState["CateId"].ToString() != string.Empty)
                {
                    CateId = Convert.ToInt32(ViewState["CateId"].ToString());
                }
                DateTime dt_roj; //for re-join date
                if (lblROJ.Text != "../../....")
                    dt_roj = Convert.ToDateTime(lblROJ.Text);
                else
                    dt_roj = Convert.ToDateTime(lblDOJ.Text);
                DateTime dt_LOS;//For Last Vacation Sattlement date
                if (lbllastsattlemtdt.Text != "../../....")
                    dt_LOS = Convert.ToDateTime(lbllastsattlemtdt.Text);
                else
                    dt_LOS = Convert.ToDateTime(txtDOS.Text.Trim());////Convert.ToInt32(lblatt.Text)
                int Output = AttendanceDAC.Ins_Upd_T_HMS_vacationsattlement(0, Convert.ToInt32(txtempid.Text), dt_roj, Convert.ToDecimal(lblsal.Text), dt_LOS, Convert.ToDateTime(txtDOS.Text.Trim()), Convert.ToInt32(lblDW.Text), Convert.ToInt32(lblDD.Text), Convert.ToInt32(lblDeff.Text), Convert.ToInt32(lblTs.Text), Convert.ToDecimal(lblAlGross.Text), 1, lblOTHr.Text, lblA1.Text, lblA2.Text, lblA3.Text, lblA4.Text, lblA5.Text, lblA6.Text, lblA7.Text, lblD1.Text, lblD2.Text, lblD3.Text, lblD4.Text, lblD5.Text, lblD6.Text, lblD7.Text, lblnetAmt.Text,  Convert.ToInt32(Session["UserId"]));

                if (Output == 1)
                    AlertMsg.MsgBox(Page, "Done.!!");
                else if (Output == 2)
                    AlertMsg.MsgBox(Page, "Updated.!");
                else
                    AlertMsg.MsgBox(Page, "Already Exists.!");

                EmployeBind(objHrCommon);
                tblNewk.Visible = false;
                tblEdit.Visible = true;
                // gvRMItem.Visible = true;
                Clear();

            }
            catch (Exception ex)
            {
                clsErrorLog.HMSEventLog(ex, "Vactional Settlement", "btnSubmit_Click", "002");
            }

        }
        protected void lnkAdd_Click(object sender, EventArgs e)
        {
            tblNewk.Visible = true;
            tblEdit.Visible = false;

        }
        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            tblEdit.Visible = true;
            tblNewk.Visible = false;

        }
        protected void gvRMItem_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }
        protected void ddlworksite_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindDeparmetBySite(Convert.ToInt32(ddlworksite.SelectedValue));
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            EmployeBind(objHrCommon);
            QtyChanged(sender, e);
            if (txtempid1.Text != "" || txtempid1.Text != null)
            {
                lnkViewAttendance.Visible = true;
                lnkViewLeaveGrant.Visible = true;
                btnPrint.Visible = true;
            }

        }
        protected void btnVacation_Click(object sender, EventArgs e)
        {



        }
        protected void btnAccPost_Click(object sender, EventArgs e)
        {
            try
            {
                startdate();
                Double A1 = 0, A2 = 0, A3 = 0, A4 = 0, A5 = 0, A6 = 0, A7 = 0, D1 = 0, D2 = 0, D3 = 0, D4 = 0, D5 = 0, D6 = 0, D7 = 0;
                string Remarks = ""; string A6Remarks = ""; string A7Remarks = ""; string D6Remarks = ""; string D7Remarks = "";
                foreach (DataListItem li in dtlvacation.Items)
                {

                    GridView gv = (GridView)li.FindControl("GVVacation");

                    foreach (GridViewRow row in gv.Rows)
                    {
                        if (row.RowIndex == 0)
                        {
                            TextBox txtA1 = (TextBox)row.FindControl("txtA1");
                            A1 = Convert.ToDouble(txtA1.Text);
                        }
                        if (row.RowIndex == 1)
                        {
                            TextBox txtA1 = (TextBox)row.FindControl("txtA1");
                            A2 = Convert.ToDouble(txtA1.Text);
                        }
                        if (row.RowIndex == 2)
                        {


                            TextBox txtA1 = (TextBox)row.FindControl("txtA1");
                            if (txtA1.Text != "")
                                A3 = Convert.ToDouble(txtA1.Text);
                            else
                            {
                                A3 = 0;
                            }
                        }
                        if (row.RowIndex == 3)
                        {
                            TextBox txtA1 = (TextBox)row.FindControl("txtA1");
                            A4 = Convert.ToDouble(txtA1.Text);
                        }
                        if (row.RowIndex == 4)
                        {
                            TextBox txtA1 = (TextBox)row.FindControl("txtA1");
                            A5 = Convert.ToDouble(txtA1.Text);
                        }
                        if (row.RowIndex == 5)
                        {
                            TextBox txtA1 = (TextBox)row.FindControl("txtA1");
                            A6 = Convert.ToDouble(txtA1.Text);
                            TextBox txtDes = (TextBox)row.FindControl("txtA6");
                            A6Remarks = txtDes.Text.Trim();
                        }
                        if (row.RowIndex == 6)
                        {
                            TextBox txtA1 = (TextBox)row.FindControl("txtA1");
                            A7 = Convert.ToDouble(txtA1.Text);
                            TextBox txtDes = (TextBox)row.FindControl("txtA6");
                            A7Remarks = txtDes.Text.Trim();
                        }
                        if (row.RowIndex == 7)
                        {
                            TextBox txtA1 = (TextBox)row.FindControl("txtA1");
                            D1 = Convert.ToDouble(txtA1.Text);
                        }
                        if (row.RowIndex == 8)
                        {
                            TextBox txtA1 = (TextBox)row.FindControl("txtA1");
                            D2 = Convert.ToDouble(txtA1.Text);
                        }
                        if (row.RowIndex == 9)
                        {
                            TextBox txtA1 = (TextBox)row.FindControl("txtA1");
                            D3 = Convert.ToDouble(txtA1.Text);
                        }
                        if (row.RowIndex == 10)
                        {
                            TextBox txtA1 = (TextBox)row.FindControl("txtA1");
                            D4 = Convert.ToDouble(txtA1.Text);
                        }
                        if (row.RowIndex == 11)
                        {
                            TextBox txtA1 = (TextBox)row.FindControl("txtA1");
                            D5 = Convert.ToDouble(txtA1.Text);
                        }
                        if (row.RowIndex == 12)
                        {
                            TextBox txtA1 = (TextBox)row.FindControl("txtA1");
                            D6 = Convert.ToDouble(txtA1.Text);
                            TextBox txtDes = (TextBox)row.FindControl("txtA6");
                            D6Remarks = txtDes.Text.Trim();
                        }
                        if (row.RowIndex == 13)
                        {
                            TextBox txtA1 = (TextBox)row.FindControl("txtA1");
                            D7 = Convert.ToDouble(txtA1.Text);
                            TextBox txtDes = (TextBox)row.FindControl("txtA6");
                            D7Remarks = txtDes.Text.Trim();
                        }
                        if (row.RowIndex == 14)
                        {
                            TextBox txtA1 = (TextBox)row.FindControl("txtA1");
                            Remarks = txtA1.Text.Trim();

                        }

                        AttendanceDAC.HR_SavePaySLIP_Vacation(empid, stdt, 1);

                    }

                }
                int month1 = 0, year1 = 0;
                if (txtdate.Text != "" || txtdate.Text != string.Empty)
                {


                    month1 = (DateTime.ParseExact(txtdate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).Month);
                    //month = Dmonth.Month;
                    year1 = (DateTime.ParseExact(txtdate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).Year);
                }

                if (month1 == 1)
                {
                    month1 = 12;
                    year1 = year1 - 1;
                }
                else { month1 = month1 - 1; }

                int id = AttendanceDAC.AddVacationSettlement(A1, A2, A3, A4, A5, A6, A7, D1, D2, D3, D4, D5, D6, D7, empid, Convert.ToInt32(Session["CompanyID"]), Remarks, Amt, "Vacation Settlement", month1, year1, A6Remarks, A7Remarks, D6Remarks, D7Remarks, 0, "V");
                if (id == 1)
                    AlertMsg.MsgBox(Page, "A/C Updated");
                else
                    AlertMsg.MsgBox(Page, "Already Posted");

                Page_Load(sender, e);
            }
            catch (Exception ex)
            {
                clsErrorLog.HMSEventLog(ex, "Vactional Settlement", "btnAccPost_Click", "003");
            }

        }
        protected void dtlvacation_ItemCommand(object source, DataListCommandEventArgs e)
        {

        }
        protected void dtlvacation_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            GridView GridView1 = (GridView)e.Item.FindControl("GridView1");

        }
        protected void GVVacation_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Edt")
            {



            }
        }
        protected void GVVacation_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            GridView GridView1 = (GridView)sender;
            DataListItem dlItem = (DataListItem)GridView1.Parent;
            DataListItemEventArgs dle = new DataListItemEventArgs(dlItem);
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int index = e.Row.RowIndex;
                if (index == 0 || index == 1 || index == 7 || index == 9)
                {
                    TextBox txtQtyOrder10 = (TextBox)e.Row.FindControl("txtA1");

                    txtQtyOrder10.ReadOnly = true;



                }
                if (index == 0)
                {
                    LinkButton lnkbtn = (LinkButton)e.Row.FindControl("lnkatt_Viewk");
                    lnkbtn.Visible = true;
                }
                if (index == 1)
                {
                    if (EALGlobal == 0)
                    {
                        LinkButton lnkbtn = (LinkButton)e.Row.FindControl("lnkEAL");
                        lnkbtn.Visible = true;
                        lnkbtn.ForeColor = System.Drawing.Color.Red;
                    }


                }
                if (index == 11)
                {
                    Button btnCals = (Button)e.Row.FindControl("btnCal");
                    btnCals.Visible = true;
                }
                if (index == 5 || index == 6 || index == 12 || index == 13)
                {
                    TextBox txtA6 = (TextBox)e.Row.FindControl("txtA6");
                    txtA6.Visible = true;
                }

            }

        }
        protected void grd_Pre(object sender, EventArgs e)
        {



        }
        protected void GVVacation_RowEditing(object sender, GridViewEditEventArgs e)
        {
        }
        protected void GVVacation_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

        }
        protected void btnCal_Click(object sender, EventArgs e)
        {
            try
            {
                HRCommon objcommon = new HRCommon();
                objcommon.PageSize = GrtPaging.ShowRows;
                objcommon.CurrentPage = GrtPaging.CurrentPage;
                if (txtempid1.Text != string.Empty)
                {
                    foreach (DataListItem li in dtlvacation.Items)
                    {

                        GridView gv = (GridView)li.FindControl("GVVacation");

                        foreach (GridViewRow row in gv.Rows)
                        {

                            if (row.RowIndex == 9)
                            {
                                SqlParameter[] p = new SqlParameter[5];
                                p[0] = new SqlParameter("@Empid", empid);
                                p[1] = new SqlParameter("@CurrentPage", GrtPaging.CurrentPage);
                                p[2] = new SqlParameter("@PageSize", GrtPaging.ShowRows);
                                p[3] = new SqlParameter("@NoofRecords", System.Data.SqlDbType.Int);
                                p[3].Direction = ParameterDirection.Output;
                                p[4] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
                                p[4].Direction = ParameterDirection.ReturnValue;
                                gvExpactdetails.DataSource = null;
                                gvExpactdetails.DataBind();
                                DataSet dse = SqlHelper.ExecuteDataset("USP_VacationalSettlement_Expact", p);
                                objcommon.NoofRecords = (int)p[3].Value;
                                objcommon.TotalPages = (int)p[4].Value;
                                if (dse != null && dse.Tables.Count > 0)
                                {
                                    if (dse.Tables[0].Rows.Count > 0)
                                    {
                                        gvExpactdetails.DataSource = dse;
                                        gvExpactdetails.DataBind();
                                        GrtPaging.Bind(objcommon.CurrentPage, objcommon.TotalPages, objcommon.NoofRecords, objcommon.PageSize);
                                        this.ModalPopupExtender1.Show();
                                    }
                                }
                                else
                                    AlertMsg.MsgBox(Page, "Employee does not have any outstanding documentation fees for deduction");

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                clsErrorLog.HMSEventLog(ex, "Vactional Settlement", "btnCal_Click", "004");
            }
        }
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                decimal TotExpatAmt = 0;
                foreach (GridViewRow gvRow in gvExpactdetails.Rows)
                {
                    CheckBox chk = new CheckBox();
                    chk = (CheckBox)gvRow.FindControl("chkEToTransfer");
                    if (chk.Checked)
                    {
                        Label lblAmount = (Label)gvRow.FindControl("lblAmount");
                        TotExpatAmt = TotExpatAmt + Convert.ToDecimal(lblAmount.Text);
                        Label lblSRNID = (Label)gvRow.FindControl("lblSEmpId");
                        Label lblFromDate = (Label)gvRow.FindControl("lblFromDate");
                        Label lblTo = (Label)gvRow.FindControl("lblTo");
                        exptdetails = exptdetails + "SRNITEMID:" + lblSRNID + ";FromDate=" + lblFromDate.Text + ";ToDate=" + lblTo.Text + ";Amount=" + lblAmount.Text;
                    }
                }
                foreach (DataListItem li in dtlvacation.Items)
                {
                    GridView gv = (GridView)li.FindControl("GVVacation");
                    foreach (GridViewRow row in gv.Rows)
                    {
                        TextBox txtExpat = (TextBox)row.FindControl("txtA1");
                        if (row.RowIndex == 9)
                        {
                            txtExpat.Text = TotExpatAmt.ToString();
                        }
                    }
                    Label lblval = (Label)gv.FooterRow.FindControl("lblvalue");
                    lblval.Text = Convert.ToString(Convert.ToDouble(lblval.Text) - Convert.ToDouble(TotExpatAmt));
                }
            }
            catch (Exception ex)
            {
                clsErrorLog.HMSEventLog(ex, "Vactional Settlement", "btnUpdate_Click", "005");
            }
        }
        protected void gvExpactdetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.Header)
                {
                    CheckBox chkMail = (CheckBox)e.Row.FindControl("chkESelectAll");
                    chkMail.Attributes.Add("onclick", String.Format("javascript:SelectAll(this,'{0}','chkApproval');", gvExpactdetails.ClientID));
                }

            }
            catch (Exception ex) { }
        }
        protected void lnkViewAttendance_Click(object sender, EventArgs e)
        {
            int Empid = 0;
            if (txtempid1.Text != "" || txtempid1.Text != null)
            {
                Empid = Convert.ToInt32(txtempid1.Text);
            }




         

            string url = "ViewAttendance.aspx?Empid=" + Empid + "&Month=" + AttMonth + "&Year=" + AttYear;
            string fullURL = "window.open('" + url + "', '_blank' );";
            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", fullURL, true);

        }
        protected void lnkViewLeaveGrant_Click(object sender, EventArgs e)
        {

            int Empid = 0;
            if (txtempid1.Text != "" || txtempid1.Text != null)
            {
                Empid = Convert.ToInt32(txtempid1.Text);
            }
            string url = "HRLeaveApplications.aspx?key=1&Empid=" + Empid;
            string fullURL = "window.open('" + url + "', '_blank' );";
            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", fullURL, true);
        }
        protected void lnkatt_Viewk_click(object sender, EventArgs e)
        {
            try
            {
                int Month;
                DataSet startdate = AttendanceDAC.GetStartDate();
                string[] sdt = txtdate.Text.Split('/');

                Month = Convert.ToInt32(sdt[1]);
                int Year = Convert.ToInt32(sdt[2]);
                AttendanceDAC att = new AttendanceDAC();
                string Date = Month + "/" + startdate.Tables[0].Rows[0][0] + "/" + Year;
                int EmpID = Convert.ToInt32(txtempid1.Text.Trim());

                lblmonth.Text = Month + "-" + Year;
                tblAtt.Rows.Clear();
            
                DataSet ds = objatt.GetAttendanceByMonth_Cursor(Convert.ToInt32(Month), Convert.ToInt32(Year), Convert.ToInt32(ddldepartment.SelectedValue), Convert.ToInt32(ddlworksite.SelectedValue), EmpID, string.Empty, null);
                int count = ds.Tables.Count;
                Double Present = 0; int Scope = 0; int Absent = 0; int OD = 0; int CL = 0; int EL = 0; int SL = 0; int LA = 0;
                double OBCL, OBEL, OBSL, CCL, CEL, CSL, AL;
                OBCL = OBEL = OBSL = CCL = CEL = CSL = AL = 0;
                DateTime dt = Convert.ToDateTime(Month + "/" + startdate.Tables[0].Rows[0][0] + "/" + Year);
                DateTime dtEnd = dt.AddMonths(1);

                TableRow tblRow = new TableRow();
                TableCell tcName = new TableCell();
                TableCell tcEmpID = new TableCell();
                TableCell tcDate = new TableCell();
                for (int i = 1; dt != dtEnd; i++)
                {
                    tcDate = new TableCell();
                    tcDate.Text = i.ToString();
                    tcDate.Style.Add("font-weight", "bold");
                    tcDate.Width = 60;
                    tblRow.Cells.Add(tcDate);
                    dt = dt.AddDays(1);
                }
                dt = Convert.ToDateTime(Month + "/" + startdate.Tables[0].Rows[0][0] + "/" + Year);
                tblAtt.Rows.Add(tblRow);

                System.Collections.Generic.List<DateTime> listHolidays = new System.Collections.Generic.List<DateTime>();
                foreach (DataRow dr in ds.Tables[ds.Tables.Count - 1].Rows)
                {
                    listHolidays.Add(Convert.ToDateTime(dr["Date"]));
                }
                tblRow = new TableRow();
                for (int j = 0; j < count - 1; j++)
                {


                    Present = 0; Scope = 0; Absent = 0; OD = CL = EL = SL = 0;
                    for (int i = 1; dt != dtEnd; i++)               // Dates no of days
                    {
                        tcDate = new TableCell();
                        DateTime dtCurrent = new DateTime(Convert.ToInt32(Year), Convert.ToInt32(Month), i);
                        if (listHolidays.Contains(dtCurrent))
                        {
                            if (ds.Tables[j].Rows[0][9].ToString() == "1")
                            {
                                tcDate.Text = "PH";
                                tcDate.Style.Add("background-color", "lightgreen");
                            }
                            else
                            {
                                tcDate.Text = "-";
                            }
                        }

                        else
                        {
                            Scope++;
                        }

                        if (ds.Tables[j].Rows.Count > 0)
                        {


                            for (int k = 0; k < ds.Tables[j].Rows.Count; k++)    // No of Emp
                            {

                                if (ds.Tables[j].Rows[k][2].ToString() != "")
                                {
                                    if (dt == Convert.ToDateTime(ds.Tables[j].Rows[k][2].ToString()).Date)
                                    {
                                        tcDate.Text = ds.Tables[j].Rows[k][1].ToString();
                                        if (ds.Tables[j].Rows[k][1].ToString() == "P" || ds.Tables[j].Rows[k][1].ToString() == "OD")
                                        {
                                            tcDate.Style.Add("color", "green");
                                            //Present++;
                                        }

                                        else if (ds.Tables[j].Rows[k][1].ToString() == "WO")
                                        {
                                            tcDate.Style.Add("color", "red");
                                        }
                                        else if (ds.Tables[j].Rows[k][1].ToString() == "HD")
                                        {
                                            tcDate.Style.Add("color", "green");
                                            Present = Present + 0.5;
                                        }
                                        if (ds.Tables[j].Rows[k][1].ToString() == "P")
                                        {
                                            Present++;
                                        }
                                        if (ds.Tables[j].Rows[k][1].ToString() == "A")
                                        {
                                            Absent++;
                                        }
                                        if (ds.Tables[j].Rows[k][1].ToString() == "OD")
                                        {
                                            OD++;
                                        }
                                        if (ds.Tables[j].Rows[k][1].ToString() == "CL")
                                        {
                                            CL++;
                                        }
                                        if (ds.Tables[j].Rows[k][1].ToString() == "EL")
                                        {
                                            EL++;
                                        }
                                        if (ds.Tables[j].Rows[k][1].ToString() == "SL")
                                        {
                                            SL++;
                                        }
                                        break;
                                    }
                                    else
                                    {
                                        tcDate.Style.Add("color", "red");
                                        if (listHolidays.Contains(dtCurrent))
                                        {
                                            if (ds.Tables[j].Rows[0][9].ToString() == "1")
                                            {
                                                tcDate.Text = "PH";
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

                                else
                                {
                                    if (listHolidays.Contains(dtCurrent))
                                    {
                                        if (ds.Tables[j].Rows[0][9].ToString() == "1")
                                        {
                                            tcDate.Text = "PH";
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



                        }

                        tcDate.Width = 60;
                        tblRow.Cells.Add(tcDate);
                        dt = dt.AddDays(1);
                        tblAtt.Rows.Add(tblRow);
                    }
                }
                //For Applied Holidays rules if any on employee id and fro selected month/year
                DataSet ds_HoliDayRules = AttendanceDAC.HR_GetHolidayNonPayRules(EmpID, CODEUtility.ConvertToDate(Date, DateFormat.DayMonthYear));
                if (ds_HoliDayRules.Tables[0].Rows.Count > 0)
                {
                    pnlNonHoliday.Visible = true;
                    pnlNonHoliday1.Visible = true;
                    pnlNonHoliday3.Visible = true;
                    pnlNonHoliday4.Visible = true;
                    pnlNonHoliday2.Visible = true;
                    grd_nonPayRules.DataSource = ds_HoliDayRules;
                    grd_nonPayRules.DataBind();
                    lblpayDH.Text = (Convert.ToInt32(lblpayD.Text) - ds_HoliDayRules.Tables[0].Rows.Count).ToString();
                    lblnonpayDH.Text = (Convert.ToInt32(lblNonD.Text) + ds_HoliDayRules.Tables[0].Rows.Count).ToString();
                }
                else
                {
                    pnlNonHoliday.Visible = false;
                    pnlNonHoliday1.Visible = false;
                    pnlNonHoliday2.Visible = false;
                    pnlNonHoliday3.Visible = false;
                    pnlNonHoliday4.Visible = false;
                }
                ModalPopupExtender2.Show();
            }
            catch (Exception Ex)
            {
                ModalPopupExtender2.Show();
            }
        }
        protected void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                int Empid; string date;
                if (txtempid1.Text != "" || txtempid1.Text != string.Empty)
                {

                    Empid = Convert.ToInt32(txtempid1.Text);
                }
                else
                    Empid = 0;
                if (txtdate.Text != "" || txtdate.Text != string.Empty)
                {
                    date = txtdate.Text;
                }
                else
                    date = "";
                bool i;
                if (chkEnch.Checked)
                    i = true;
                else
                    i = false;
                string url = "VacationalPrint.aspx?Empid=" + Empid + "&Date=" + date + "&Checked=" + i;
                string fullURL = "window.open('" + url + "', '_blank' );";
                ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", fullURL, true);
            }
            catch (Exception ex)
            {
                clsErrorLog.HMSEventLog(ex, "Vactional Settlement", "btnPrint_Click", "006");
            }
        }
        protected void lnkEAL_Click(object sender, EventArgs e)
        {
            int Empid;
            if (txtempid1.Text != "" || txtempid1.Text != string.Empty)
            {

                Empid = Convert.ToInt32(txtempid1.Text);
            }
            else
                Empid = 0;
            string url = "Encashment_AL.aspx?id=1&Empid=" + Empid;
            string fullURL = "window.open('" + url + "', '_blank' );";
            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", fullURL, true);
        }
        #endregion Events

        #region Methods
        void BindPager()
        {

           
            EmployeBind(objHrCommon);
        }
        void EmployeBind(HRCommon objHrCommon)
        {
            try
            {
                int month = 0;
                int year = 0;
                string ename = "";
                if (txtempid1.Text != "" || txtempid1.Text != string.Empty)
                {

                    empid = Convert.ToInt32(txtempid1.Text);
                }
                else
                    empid = 0;
                if (txtename.Text != "" || txtename.Text != string.Empty)
                {

                    ename = txtename.Text;
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
                if (Month == 1)
                {
                    if ((DateTime.ParseExact(txtdate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).Day) < Convert.ToInt32(startdate.Tables[0].Rows[0][0].ToString()))
                    {
                        Month = 12;
                        Year = Year - 1;
                    }
                }
                else
                {
                    if ((DateTime.ParseExact(txtdate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).Day) < Convert.ToInt32(startdate.Tables[0].Rows[0][0].ToString()))
                        Month = Month - 1;
                }

                AttMonth = Month;
                AttYear = Year;
                string st = Month + "/" + startdate.Tables[0].Rows[0][0].ToString() + "/" + Year;
                DateTime stdt = CODEUtility.ConvertToDate(st, DateFormat.MonthDayYear);
                DateTime enddate = DateTime.ParseExact(txtdate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                int i;
                try
                {
                    SqlParameter[] p = new SqlParameter[3];
                    p[0] = new SqlParameter("@Year", Year);
                    p[1] = new SqlParameter("@Empid", empid);
                    p[2] = new SqlParameter("@Form", "V");
                    i = Convert.ToInt32(SqlHelper.ExecuteScalar("HMS_CountVacationPost", p));
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                if (i != 0)
                {
                    btnAccPost.Visible = false;
                    AlertMsg.MsgBox(Page, "Already Posted To Accounts");
                    return;
                }
                else
                {
                    DataSet ds = new DataSet();
                    if (!chkEnch.Checked)
                    {
                        ds = AttendanceDAC.T_HMS_GetVacation(objHrCommon, empid, ename, Convert.ToInt32(ddlworksite.SelectedValue), Convert.ToInt32(ddldepartment.SelectedValue), month, year, stdt, enddate,0);
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
                        sqlParams[6] = new SqlParameter("@WSid", Convert.ToInt32(ddlworksite.SelectedValue));
                        sqlParams[7] = new SqlParameter("@Deptid", Convert.ToInt32(ddldepartment.SelectedValue));
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
                        dtlvacation.DataSource = ds.Tables[tablcnt - 1];
                        dtlvacation.DataBind();
                        btnAccPost.Visible = true;
                    }
                    else
                    {
                        dtlvacation.DataSource = null;
                        dtlvacation.DataBind();
                        AlertMsg.MsgBox(Page, "No Record Found");
                        btnAccPost.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                clsErrorLog.HMSEventLog(ex, "Vactional Settlement", "EmployeBind", "007");
            }
        }
        int Years(DateTime start, DateTime end)
        {
            return (end.Year - start.Year - 1) +
                (((end.Month > start.Month) ||
                ((end.Month == start.Month) && (end.Day >= start.Day))) ? 1 : 0);
        }
        public void calculations()
        {
            try
            {
                // 1. for days of works
                lblDW.Text = (Convert.ToDateTime(txtDOS.Text) - Convert.ToDateTime(lblDOJ.Text)).TotalDays.ToString();
                //2. Disallowed days ??
                //3. Deff=DW-dd
                lblDeff.Text = (Convert.ToInt32(lblDW.Text) - Convert.ToInt32(lblDD.Text)).ToString();
                //4. Total Services=EOC-DOJ
                lblTs.Text = (Convert.ToDateTime(lbldateofEndCOntract.Text) - Convert.ToDateTime(lblDOJ.Text)).TotalDays.ToString();
                //5. Al Gross = lbldeff/11
                lblAlGross.Text = (Convert.ToInt32(lblDeff.Text) / 11).ToString();


                int EmpID = Convert.ToInt32(txtempid.Text.Trim());
              
                ds = PayRollMgr.GetPaySLIP_CTH(EmpID, Convert.ToDateTime(txtDOS.Text.Trim()));
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[5].Rows.Count > 0)
                {
                    lblatt.Text = ds.Tables[5].Rows[0]["WorkingDays"].ToString();
                    lblA1.Text = ds.Tables[9].Rows[0]["NetAmount"].ToString(); //ds.Tables[1].Rows[0]["Value"].ToString();
                    lblA2.Text = (Convert.ToDecimal(lblAlGross.Text) * (Convert.ToDecimal(ds.Tables[0].Rows[0]["MontlyCTC"]) / 30)).ToString();
                }
                //Gratutity Calculations
                int yr = Years(Convert.ToDateTime(lblDOJ.Text), Convert.ToDateTime(lbldateofEndCOntract.Text));

                if (yr < 5)
                {
                    lblA5.Text = "0";
                    if (Convert.ToDateTime(lbldateofEndCOntract.Text) <= DateTime.Now)

                        lblA4.Text = (Convert.ToDecimal(ds.Tables[0].Rows[0]["MontlyCTC"]) / 2).ToString();
                    else
                        lblA4.Text = (Convert.ToDecimal(ds.Tables[0].Rows[0]["MontlyCTC"]) / 3).ToString();
                }
                else
                {
                    lblA4.Text = "0";
                    if (Convert.ToDateTime(lbldateofEndCOntract.Text) <= DateTime.Now)

                        lblA5.Text = ds.Tables[0].Rows[0]["MontlyCTC"].ToString();
                    else
                        lblA5.Text = ((Convert.ToDecimal(ds.Tables[0].Rows[0]["MontlyCTC"]) * 2) / 3).ToString();
                }

            }
            catch
            { }

        }
        protected void startdate()
        {
            DataSet startdate = AttendanceDAC.GetStartDate();
            int Month, Year;
            Month = (DateTime.ParseExact(txtdate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).Month);
           
            Year = (DateTime.ParseExact(txtdate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).Year);
            if (Month == 1)
            {
                Month = 12;
                Year = Year - 1;
            }
            else
                Month = Month - 1;
          
            string st = Month + "/" + startdate.Tables[0].Rows[0][0].ToString() + "/" + Year;
            stdt = CodeUtil.ConvertToDate(st, CodeUtil.DateFormat.MonthDayYear);

           
        }
        protected override void OnInit(EventArgs e)
        {
            ModuleID = 1;
            base.OnInit(e);

           
        }
        public int GetParentMenuId()
        {
            string URL = Request.Url.Segments[Request.Url.Segments.Length - 1] + Request.Url.Query;
            int RoleId = Convert.ToInt32(Session["RoleId"].ToString());
            int ModuleId = ModuleID; ;

         

            DataSet ds = AttendanceDAC.GetAllowed(RoleId, ModuleId, URL);
            int MenuId = 0;
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                MenuId = Convert.ToInt32(ds.Tables[0].Rows[0]["Under"]);
              
                ViewState["ViewAll"] = (bool)ds.Tables[0].Rows[0]["ViewAll"];
                viewall = (bool)ViewState["ViewAll"];
                btnSubmit.Enabled = Editable = Convert.ToBoolean(ds.Tables[0].Rows[0]["Editable"].ToString());
                menuname = ds.Tables[0].Rows[0]["menuname"].ToString();
                menuid = MenuId.ToString();
                mid = Convert.ToInt32(ds.Tables[0].Rows[0]["MenuId"].ToString());
            }
            return MenuId;
        }
        public void BindGrid()
        {
            ds = AttendanceDAC.GetDesignationsList();
          

        }
        public void BindDetails(int ID)
        {
            objHrCommon.PageSize = 10;
            objHrCommon.CurrentPage = 1;
            ds = AttendanceDAC.T_HMS_empVsAirTicketsAuth_LISTbyID_status(objHrCommon, true, ID, null, null);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                txtempid.Text = ds.Tables[0].Rows[0][2].ToString();
                AttendanceDAC objAtt = new AttendanceDAC();
                int empid = Convert.ToInt32(txtempid.Text.Trim());
                DataSet ds1 = objAtt.GetEmployeeDetails(empid);
                if (ds1.Tables[0].Rows.Count > 0)
                {

                    lblempdetails.Text = ds1.Tables[0].Rows[0]["FName"].ToString() + ' ' + ds1.Tables[0].Rows[0]["MName"].ToString() + ' ' + ds1.Tables[0].Rows[0]["LName"].ToString();
                    tickimgk.Visible = true;
                    notfoundk.Visible = false;
                    lblempdetails.BackColor = System.Drawing.Color.Yellow;
                    lblempdetails.ForeColor = System.Drawing.Color.Green;
                }

                tblEdit.Visible = false;
                tblNewk.Visible = true;

                btnSubmit.Text = "Update";

            }
        }
        public void Clear()
        {
           
            ViewState["CateId"] = "";
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionList(string prefixText, int count, string contextKey)
        {

            DataSet ds = AttendanceDAC.GetSearch_by_EmpName(prefixText);
            DataTable dt = ds.Tables[0];
            List<string> items = new List<string>(count);
            var rtval = new Dictionary<string, string>();
            foreach (DataRow row in dt.Rows)
            {
                string str = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(row["Name"].ToString(), row["EmpID"].ToString());
                items.Add(str);
            }

            return items.ToArray(); ;// txtItems.ToArray();

        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionListEmpid(string prefixText, int count, string contextKey)
        {

            DataSet ds = AttendanceDAC.Get_Search_by_Empid(prefixText);
            DataTable dt = ds.Tables[0];
            List<string> items = new List<string>(count);
            var rtval = new Dictionary<string, string>();
            foreach (DataRow row in dt.Rows)
            {
                string str = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(row["Name"].ToString(), row["Name"].ToString());
                items.Add(str);
            }

            return items.ToArray(); ;// txtItems.ToArray();

        }
        public void BindDeparmetBySite(int Site)
        {
            DataSet ds = AttendanceDAC.BindDeparmetBySite(Site, Convert.ToInt32(Session["CompanyID"]));
            ddldepartment.DataSource = ds;
            ddldepartment.DataTextField = "DeptName";
            ddldepartment.DataValueField = "DepartmentUId";
            ddldepartment.DataBind();
            ddldepartment.Items.Insert(0, new ListItem("---ALL---", "0", true));
        }
        public void BindDesignations()
        {
          
            DataSet ds = (DataSet)objatt.GetDesignations();

            ddldesignation.DataSource = ds;
            ddldesignation.DataTextField = "Designation";
            ddldesignation.DataValueField = "DesigId";
            ddldesignation.DataBind();
            ddldesignation.Items.Insert(0, new ListItem("---ALL---", "0", true));
        }
        public DataView BindTransdetails(string TransId)
        {
            // int resultD1 = 0, resultD2 = 0;
            DataSet d1 = new DataSet();
            DataTable dt = new DataTable();
            DataRow row = dt.NewRow();
            DataColumn column = new DataColumn();
            int Month;
            string[] sdt = txtdate.Text.Split('/');

            Month = Convert.ToInt32(sdt[1]);
            int Year = Convert.ToInt32(sdt[2]);

            int valofSync = 0;
            try
            {
                
            }
            catch { }

         

            string st = Month + "/" + sdt[0] + "/" + Year;
            DateTime edt = DateTime.ParseExact(txtdate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            AttendanceDAC.HR_SavePaySLIP(Convert.ToInt32(TransId), st, valofSync,string.Empty);
         
            ds = PayRollMgr.GetPaySLIP(Convert.ToInt32(TransId), edt);
            double salamount = 0;
            string Details = "";
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[5].Rows.Count > 0)
            {
              
                salamount = Convert.ToDouble(ds.Tables[1].Rows[0]["Value"].ToString());
                for (int k = 0; k < ds.Tables[2].Rows.Count; k++)
                {
                    salamount = salamount + Convert.ToDouble(ds.Tables[2].Rows[k]["Value"].ToString());
                }
                for (int k = 0; k < ds.Tables[23].Rows.Count; k++)
                {
                    salamount = salamount + Convert.ToDouble(ds.Tables[23].Rows[k]["Value"].ToString());
                }

            
                Details = ds.Tables[15].Rows[0]["Details"].ToString();
                lblA2.Text = (Convert.ToDecimal(lblAlGross.Text) * (Convert.ToDecimal(ds.Tables[0].Rows[0]["MontlyCTC"]) / 30)).ToString();
            }

            int month1 = 0, year1 = 0, day = 0;

            if (txtdate.Text != "" || txtdate.Text != string.Empty)
            {


                month1 = (DateTime.ParseExact(txtdate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).Month);
              
                year1 = (DateTime.ParseExact(txtdate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).Year);
                day = (DateTime.ParseExact(txtdate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).Day);
            }

            if (month1 == 1)
            {
                month1 = 12;
                year1 = year1 - 1;
            }
            else
            {
                if (day < 21)
                    month1 = month1 - 1;

            }


            string Absamount = "0";
            string AbsentDetails = "";
            d1 = AttendanceDAC.HR_AbsentPenalitiesByPaging_vacationsettlement(objHrCommon, 0, 0, 0, month1, year1, empid);
            dt.Columns.Add("Description", typeof(string));
            dt.Columns.Add("Amount", typeof(string));
            dt.Columns.Add("Details", typeof(string));
            string Airtktamount = "0";
            string AirtktDetails = "";

            if (!chkEnch.Checked)
            {
                if (d1 != null && d1.Tables.Count > 0 && d1.Tables.Cast<DataTable>().Any(table => table.Rows.Count != 0))
                {
                    Absamount = d1.Tables[0].Rows[0]["Amount"].ToString(); //ds.Tables[1].Rows[0]["Value"].ToString();
                    AbsentDetails = "Occurance=" + d1.Tables[0].Rows[0]["Occurance"].ToString() + "; Absents=" + d1.Tables[0].Rows[0]["Absents"].ToString() + "; Penalities=" + d1.Tables[0].Rows[0]["Penalities"].ToString() + ";Limited=" + d1.Tables[0].Rows[0]["Limited"].ToString() + "; Amount=" + d1.Tables[0].Rows[0]["Amount"].ToString() + ";";
                }


                ButtonField btnMeds = new ButtonField();
             
                btnMeds.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                btnMeds.CommandName = "Meds";
                btnMeds.ButtonType = ButtonType.Button;
                btnMeds.Text = "Meds";
                btnMeds.Visible = true;



                //Add the newly created bound field to the GridView.
                dt.Columns.Add("Det", typeof(string));

                int daysinmonth = System.DateTime.DaysInMonth(year1, month1);




                double Total = 0;

                DataTable dstrans = (DataTable)ViewState["DataSet"];
                dt.Rows.Add("A1:Salary For the Current Month Attendance ", String.Format("{0:0.00}", salamount), Details);
                Total = salamount;
                decimal EAL = Convert.ToDecimal(ds.Tables[16].Rows[0]["HRA"].ToString());
                EALGlobal = EAL;
              
                string A2Label = string.Empty;
                A2Label = ds.Tables[19].Rows[0]["A2Label"].ToString();
                dt.Rows.Add("A2:Encashment of AL(EAL)", EAL, A2Label);
                Total = Total + Convert.ToDouble(EAL);
                double OT;
                string otdet = "";
                if (dstrans.Rows[0]["Amount"] != null || dstrans.Rows[0]["Amount"].ToString() != "")
                {
                    otdet = "OT Rate Per Hour = Month Basic Salary(" + String.Format("{0:0.00}", ds.Tables[0].Rows[0]["MontlyCTC"].ToString()) + ")/(DaysInaMonth(" + ds.Tables[24].Rows[0]["daysinmonth"].ToString() + ")*(Work Shift Hours- Break Time)(" + dstrans.Rows[0]["shiftours"].ToString() + "))=" + String.Format("{0:0.00}", Convert.ToDecimal(ds.Tables[0].Rows[0]["MontlyCTC"].ToString()) / (Convert.ToDecimal(ds.Tables[24].Rows[0]["daysinmonth"].ToString()) * Convert.ToDecimal(dstrans.Rows[0]["shiftours"].ToString()))) + " ; OT Hours = " + dstrans.Rows[0]["OTHours"].ToString() + " OT Amount = OT Rate Per Hour*OT_Hours*Pay-Coeff;";
                }
                else
                {


                }
                Total = Total + Convert.ToDouble(dstrans.Rows[0]["Amount"]);

                dt.Rows.Add("A3:Over-Time Amount", dstrans.Rows[0]["Amount"], otdet);
                dt.Rows.Add("A4:Air Ticket Reimbursement", "0");
                dt.Rows.Add("A5:Exit Entry Visa Reimbursement", "0");
                dt.Rows.Add("A6:", 0);
                dt.Rows.Add("A7:", 0);
                decimal TransAllowance = Convert.ToDecimal(ds.Tables[17].Rows[0]["D1"].ToString());
                Total = Total + Convert.ToDouble(TransAllowance);
                decimal FoodAllowance = Convert.ToDecimal(ds.Tables[18].Rows[0]["D2"].ToString());
                Total = Total + Convert.ToDouble(FoodAllowance);
                dt.Rows.Add("D1: DYNAMISM ", (TransAllowance + FoodAllowance), ds.Tables[21].Rows[0]["D1Label"].ToString() + ds.Tables[22].Rows[0]["D2Label"].ToString());
                Total = Total + Convert.ToDouble(dstrans.Rows[0]["Amount"]);
                dt.Rows.Add("D2: Other manual deductions ", "0", "");
                Total = Total + Convert.ToDouble(dstrans.Rows[0]["Amount"]);
                dt.Rows.Add("D3:OutStanding  Advances", ds.Tables[20].Rows[0]["D3"].ToString());
             
                dt.Rows.Add("D4:Absent Penalty ", Absamount, AbsentDetails);
                dt.Rows.Add("D5:Expat ", 0, exptdetails, btnMeds);
                dt.Rows.Add("D6:", 0);
                dt.Rows.Add("D7:", 0);
                dt.Rows.Add("Remarks", "");
             
            }
            else
            {
                double Total = 0;
                dt.Rows.Add("A1:Salary For the Current Month Attendance ", "0");
                decimal EAL = Convert.ToDecimal(ds.Tables[16].Rows[0]["HRA"].ToString());
                EALGlobal = EAL;
             
                string A2Label = string.Empty;
                A2Label = ds.Tables[19].Rows[0]["A2Label"].ToString();
                dt.Rows.Add("A2:Encashment of AL(EAL)", EAL, A2Label);

              
                DataSet dsg = AttendanceDAC.empVsAirTicketsAuth_LISTbyID(Convert.ToInt32(ds.Tables[0].Rows[0]["Empid"].ToString()));
                if (dsg != null && dsg.Tables.Count > 0 && dsg.Tables.Cast<DataTable>().Any(table => table.Rows.Count != 0))
                {
                    Airtktamount = dsg.Tables[0].Rows[0]["Amount"].ToString();
                    AirtktDetails = "From City - " + dsg.Tables[0].Rows[0]["from_city"].ToString() + "; To City-" + dsg.Tables[0].Rows[0]["to_city"].ToString() + "; No. tickets =" + dsg.Tables[0].Rows[0]["Tickets"].ToString() + "; Fare = " + dsg.Tables[0].Rows[0]["fare_rate"].ToString() + ";";

                }
                else
                {
                    Airtktamount = "0";
                }


                Total = Total + Convert.ToDouble(EAL);
                dt.Rows.Add("A3:Over-Time Amount", "0");
                dt.Rows.Add("A4:Air Ticket Reimbursement", Airtktamount, AirtktDetails);
                dt.Rows.Add("A5:Exit Entry Visa Reimbursement", "0");
                dt.Rows.Add("A6:", 0);
                dt.Rows.Add("A7:", 0);
                dt.Rows.Add("D1: DYNAMISM ", "0");
                dt.Rows.Add("D2: Other manual deductions ", "0", "");
                dt.Rows.Add("D3:OutStanding  Advances", "0");
                dt.Rows.Add("D4:Absent Penalty ", "0");
                dt.Rows.Add("D5:Expat ", 0);
                dt.Rows.Add("D6:", 0);
                dt.Rows.Add("D7:", 0);
                dt.Rows.Add("Remarks", "");
            }
            DataView dv = dt.DefaultView;
            return dv;
        }
        protected void QtyChanged(object sender, EventArgs e)
        {
            decimal resultD1 = 0, resultD2 = 0;
            decimal totalA = 0, totalD = 0, Total = 0;
            foreach (DataListItem li in dtlvacation.Items)
            {

                GridView gv = (GridView)li.FindControl("GVVacation");

                foreach (GridViewRow row in gv.Rows)
                {

                    TextBox Bookqty = (TextBox)row.FindControl("txtA1");

                    if (row.RowIndex == 1)
                    {
                        TextBox txtQtyOrder10 = (TextBox)row.FindControl("txtA1");
                        oty = Convert.ToDecimal(txtQtyOrder10.Text);

                        DataTable dt = (DataTable)ViewState["DataSet"];


                      
                    }


                    if (chk == 1)
                    {
                        int month1 = 0, year1 = 0;
                        if (txtdate.Text != "" || txtdate.Text != string.Empty)
                        {


                            month1 = (DateTime.ParseExact(txtdate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).Month);
                          
                            year1 = (DateTime.ParseExact(txtdate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).Year);
                        }

                        if (month1 == 1)
                        {
                            month1 = 12;
                            year1 = year1 - 1;
                        }
                        else { month1 = month1 - 1; }
                        int daysinmonth = System.DateTime.DaysInMonth(year1, month1);

                        int EmpSalID, Value = 0, AmoundD = 0;
                        DataSet dds = AttendanceDAC.GetEmpSalList(empid);
                        EmpSalID = Convert.ToInt32(dds.Tables[0].Rows[dds.Tables[0].Rows.Count - 1]["EmpSalID"]);

                        DataSet ds2 = PayRollMgr.GetEmpAllowancesList(empid, EmpSalID);
                        if (ds2 != null && ds2.Tables.Count > 0)
                        {
                            Value = Convert.ToInt32(ds2.Tables[0].Rows[1]["Value"]); //ds.Tables[1].Rows[0]["Value"].ToString();
                        }

                        resultD1 = oty * (Value / daysinmonth);

                        DataSet ds3 = PayRollMgr.GetEmpNonCTCComponentsList(empid, EmpSalID);
                        if (ds3 != null && ds3.Tables.Count > 0)
                        {
                            AmoundD = Convert.ToInt32(ds3.Tables[0].Rows[0]["Amount"]); //ds.Tables[1].Rows[0]["Value"].ToString();
                        }
                        resultD2 = oty * (AmoundD / daysinmonth);

                    }
                  
                    if (row.RowIndex <= 6)
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

                    Label lblval = (Label)gv.FooterRow.FindControl("lblvalue");
                    lblval.Text = Total.ToString();
                    Amt = Convert.ToDouble(Total);
                }


            }

           
        }
        #endregion Methods
    }
}