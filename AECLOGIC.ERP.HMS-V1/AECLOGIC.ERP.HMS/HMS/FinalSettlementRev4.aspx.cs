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
namespace AECLOGIC.ERP.HMS
{
    public partial class FinalSettlementRev4 : AECLOGIC.ERP.COMMON.WebFormMaster
    {
        #region Variables
        string exptdetails = string.Empty;
        int AttMonth, AttYear;
        AttendanceDAC objatt = new AttendanceDAC();
         
        int mid = 0;
        static double Amt = 0.0;
        decimal gamt = 0;
        static int empid = 0;
        static int chk = 0;
        static decimal oty = 0;
        bool viewall, Editable;
        string menuname;
        string menuid;
        HRCommon objHrCommon = new HRCommon();
        DateTime stdt;
        DateTime eddt;
        int status = 0;
        #endregion Variables

        #region Events
        protected override void OnInit(EventArgs e)
        {
            ModuleID = 1;
            base.OnInit(e);
        }
        void EmpListPaging_ShowRowsClick(object sender, EventArgs e)
        {
            BindPager();
        }
        void EmpListPaging_FirstClick(object sender, EventArgs e)
        {
            BindPager();
        }
        protected void btnAccPostUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                //startdate();
                Double A1 = 0, A2 = 0, A3 = 0, A4 = 0, A5 = 0, A6 = 0, A7 = 0, D1 = 0, D2 = 0, D3 = 0, D4 = 0, D5 = 0, D6 = 0, D7 = 0, AdjAmt = 0, EmpPen = 0, Gratuityamt=0;
                string Remarks = ""; string A6Remarks = ""; string A7Remarks = ""; string D6Remarks = ""; string D7Remarks = "", A1Label = "";string D3Remarks = "";
                string A2Label = "", A3Label = "", A4Label = "", A5Label = "", D1Label = "", D2Label = "", D3Label = "", D4Label = "", D5Label = "", GratutityRemarks = "";
                foreach (DataListItem li in dtlvacationEdit.Items)
                {

                    GridView gv = (GridView)li.FindControl("GVVacationedit");

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
                            Gratuityamt = Convert.ToDouble(txtA1.Text);
                        }
                        if (row.RowIndex == 8)
                        {
                            TextBox txtA1 = (TextBox)row.FindControl("txtA1");
                            D1 = Convert.ToDouble(txtA1.Text);
                        }
                        if (row.RowIndex == 9)
                        {
                            TextBox txtA1 = (TextBox)row.FindControl("txtA1");
                            D2 = Convert.ToDouble(txtA1.Text);
                        }
                        if (row.RowIndex == 10)
                        {
                            TextBox txtA1 = (TextBox)row.FindControl("txtA1");
                            D3 = Convert.ToDouble(txtA1.Text);
                        }
                        if (row.RowIndex == 11)
                        {
                            TextBox txtA1 = (TextBox)row.FindControl("txtA1");
                            D4 = Convert.ToDouble(txtA1.Text);
                        }
                        if (row.RowIndex == 12)
                        {
                            TextBox txtA1 = (TextBox)row.FindControl("txtA1");
                            D5 = Convert.ToDouble(txtA1.Text);
                        }
                        if (row.RowIndex == 13)
                        {
                            TextBox txtA1 = (TextBox)row.FindControl("txtA1");
                            D6 = Convert.ToDouble(txtA1.Text);
                            TextBox txtDes = (TextBox)row.FindControl("txtA6");
                            D6Remarks = txtDes.Text.Trim();
                        }
                        if (row.RowIndex == 14)
                        {
                            TextBox txtA1 = (TextBox)row.FindControl("txtA1");
                            if (txtA1.Text != "")
                                D7 = Convert.ToDouble(txtA1.Text);
                            else
                                D7 = 0;
                            TextBox txtDes = (TextBox)row.FindControl("txtA6");
                            D7Remarks = txtDes.Text.Trim();
                        }
                        if (row.RowIndex == 15)
                        {
                            TextBox txtA1 = (TextBox)row.FindControl("txtA1");
                            AdjAmt = Convert.ToDouble(txtA1.Text);

                        }
                        if (row.RowIndex == 16)
                        {
                            TextBox txtA1 = (TextBox)row.FindControl("txtA1");
                            EmpPen = Convert.ToDouble(txtA1.Text);

                        }
                        if (row.RowIndex == 17)
                        {
                            TextBox txtA1 = (TextBox)row.FindControl("txtA1");
                            Remarks = txtA1.Text.Trim();

                        }
                        //AttendanceDAC.HR_SavePaySLIP_Vacation(empid, stdt, 1);
                    }
                }
                int month1 = 0, year1 = 0;
                if (txtdate.Text != "" || txtdate.Text != string.Empty)
                {


                    month1 = (DateTime.ParseExact(txtdate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).Month);
                    //month = Dmonth.Month;
                    year1 = (DateTime.ParseExact(txtdate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).Year);
                }
                DateTime Setdt = DateTime.ParseExact(txtdate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                int id = AttendanceDAC.AddVacationSettlementRev4(A1, A2, A3, A4, A5, A6, A7, D1, D2, D3, D4, D5, D6, D7, empid, Convert.ToInt32(Session["CompanyID"]), Remarks, Amt, "Final Settlement", month1, year1, A6Remarks, A7Remarks, D6Remarks, D7Remarks, Gratuityamt, "F", lblPresendays.Text, lblNoOfDays.Text, Convert.ToInt32(Session["vstatus"]), Setdt, AdjAmt, EmpPen,0
                    , A1Label, A2Label, A3Label, A4Label, A5Label, D1Label, D2Label, D3Label, D4Label, D5Label, GratutityRemarks, D3Remarks);
                if (id == 1)
                {
                    tblNew.Visible = false;
                    tblView.Visible = true;
                    tblEdit.Visible = false;
                    AlertMsg.MsgBox(Page, "Saved Successfully");
                    BindGridView();
                }
                else if (id == 2)
                {
                    tblNew.Visible = false;
                    tblView.Visible = true;
                    tblEdit.Visible = false;
                    AlertMsg.MsgBox(Page, "Updated Successfully");
                    BindGridView();
                }
                else
                    AlertMsg.MsgBox(Page, "Already Saved");


            }
            catch (Exception ex)
            {
                clsErrorLog.HMSEventLog(ex, "Final Settlement", "btnAccPostUpdate_Click", "003");
            }

        }
       
        protected void gvFinal_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Edt")
            {
                Session["vstatus"] = 2;
                GridViewRow gvr = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);


                int index = Convert.ToInt32(gvr.RowIndex);
                GridViewRow row = gvFinal.Rows[index];
                txtempid1.Value = row.Cells[0].Text;
                txtdate.Text = row.Cells[2].Text;
                EmployeBindEdit(objHrCommon);
                QtyChangedEdit(sender, e);
                if (txtempid1.Value != "" || txtempid1.Value != null)
                {
                    lnkViewAttendance.Visible = true;
                    //lnkViewLeaveGrant.Visible = true;
                    //btnPrint.Visible = true;
                }

            }
            if (e.CommandName == "App")
            {
                SqlParameter[] sqlParams = new SqlParameter[3];
                sqlParams[0] = new SqlParameter("@id", e.CommandArgument);
                sqlParams[1] = new SqlParameter("@Companyid", Convert.ToInt32(Session["CompanyID"]));
                sqlParams[2] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
                sqlParams[2].Direction = ParameterDirection.ReturnValue;
                SQLDBUtil.ExecuteNonQuery("sh_VacationalSettlementAccountPosting", sqlParams);
                int id = (int)sqlParams[2].Value;
                if (id == 3)
                    AlertMsg.MsgBox(Page, "Account Posted Successfully");
                else
                    AlertMsg.MsgBox(Page, "Already Account Posted");
                BindGridView();
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
           
            try
            {

              
                if (!IsPostBack)
                {
                    GetParentMenuId();
                    BindCompanyList();
                    // FIllObject.FillDropDown(ref ddlworksite, "HR_GetWorkSite");
                    tractions.Visible = false;
                    trwarning.Visible = false;
                    tddetails.Visible = false;
                    tickimgk.Visible = false;
                    notfoundk.Visible = true;
                    dvadvatt.Visible = false;
                    /// BindDesignations();
                    BindAtttypes();
                    txtDOS.Text = DateTime.Now.ToString("dd-MMM-yyyy");
                    ViewState["CateId"] = "";
                    if (Convert.ToInt32(Request.QueryString["key"]) == 1)
                    {
                        tblNew.Visible = true;
                        tblView.Visible = false;
                        tblEdit.Visible = false;
                    }
                    else
                    {
                        tblNew.Visible = false;
                        tblView.Visible = true;
                        tblEdit.Visible = false;
                        AdvancedLeaveAppOthPaging.CurrentPage = 1;
                        BindGridView();
                    }
                }
            }
            catch (Exception ex)
            {
                clsErrorLog.HMSEventLog(ex, "Final Settlement", "Page_Load", "001");
            }
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
                    lblDOJ.Text = "";//Convert.ToDateTime(dsApp.Tables[0].Rows[0]["doj"]).ToString(System.Web.Configuration.WebConfigurationManager.AppSettings["DateFormat"]);
                    lblsal.Text = "";// dsApp.Tables[0].Rows[0]["salary"].ToString();
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
                    //AttendanceDAC.HR_Upd_DesigStatus(ID, Status);
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
                clsErrorLog.HMSEventLog(ex, "Final Settlement", "btnSubmit_Click", "002");
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
        protected void ddlworksite_SelectedIndexChanged(object sender, EventArgs e)
        {
            // BindDeparmetBySite(Convert.ToInt32(ddlworksite.SelectedValue));
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            EmployeBind(objHrCommon);
            QtyChanged(sender, e);
            if (txtempid1.Value != "" || txtempid1.Value.Length > 0)
            {
                lnkViewAttendance.Visible = true;
            }   
        }
        protected void btnSearch_Click1(object sender, EventArgs e)
        {
            BindGridView();
        }
        protected void btnVacation_Click(object sender, EventArgs e)
        {
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


                        // if (oty > Convert.ToInt32(dt.Rows[0]["GrantedDays"]))
                        // { AlertMsg.MsgBox(Page, "Please Enter EAL Less than Granted Days"); chk = 0; return; }
                        //else
                        //{
                        //    chk = 1;
                        //}
                    }


                    if (chk == 1)
                    {
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
                    //if (row.RowIndex == 0 || row.RowIndex == 1 || row.RowIndex == 2 || row.RowIndex == 3 || row.RowIndex == 4 || row.RowIndex == 5)
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

                    Label lblval = (Label)gv.FooterRow.FindControl("lblvalue");
                    lblval.Text = Total.ToString();
                    Amt = Convert.ToDouble(Total);
                }


            }

            //BindTransdetails("0");
        }
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
                            //month = Dmonth.Month;
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
                    //if (row.RowIndex == 0 || row.RowIndex == 1 || row.RowIndex == 2 || row.RowIndex == 3 || row.RowIndex == 4 || row.RowIndex == 5)
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

            //BindTransdetails("0");
        }
        protected void btnAccPost_Click(object sender, EventArgs e)
        {
            try
            {
                startdate();
                Double A1 = 0, A2 = 0, A3 = 0, A4 = 0, A5 = 0, A6 = 0, A7 = 0, D1 = 0, D2 = 0, D3 = 0, D4 = 0, D5 = 0, D6 = 0, D7 = 0, Gratuityamt = 0,AdjAmt=0,EmpPen=0;
                string Remarks = ""; string A6Remarks = ""; string A7Remarks = ""; string D6Remarks = ""; string D7Remarks = "", A1Label = "";string D3Remarks = "";
                string A2Label = "", A3Label = "", A4Label = "", A5Label = "", D1Label = "", D2Label = "", D3Label = "", D4Label = "", D5Label = "", GratutityRemarks = "";
                foreach (DataListItem li in dtlvacation.Items)
                {

                    GridView gv = (GridView)li.FindControl("GVVacation");

                    foreach (GridViewRow row in gv.Rows)
                    {
                        if (row.RowIndex == 0)
                        {
                            TextBox txtA1 = (TextBox)row.FindControl("txtA1");
                            if (txtA1.Text.Trim() != "")
                                A1 = Convert.ToDouble(txtA1.Text);
                            else
                                A1 = 0;
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
                            Gratuityamt = Convert.ToDouble(txtA1.Text);
                        }
                        if (row.RowIndex == 8)
                        {
                            TextBox txtA1 = (TextBox)row.FindControl("txtA1");
                            D1 = Convert.ToDouble(txtA1.Text);
                        }
                        if (row.RowIndex == 9)
                        {
                            TextBox txtA1 = (TextBox)row.FindControl("txtA1");
                            D2 = Convert.ToDouble(txtA1.Text);
                        }
                        if (row.RowIndex == 10)
                        {
                            TextBox txtA1 = (TextBox)row.FindControl("txtA1");
                            D3 = Convert.ToDouble(txtA1.Text);
                        }
                        if (row.RowIndex == 11)
                        {
                            TextBox txtA1 = (TextBox)row.FindControl("txtA1");
                            D4 = Convert.ToDouble(txtA1.Text);
                        }
                        if (row.RowIndex == 12)
                        {
                            TextBox txtA1 = (TextBox)row.FindControl("txtA1");
                            D5 = Convert.ToDouble(txtA1.Text);
                        }
                        if (row.RowIndex == 13)
                        {
                            TextBox txtA1 = (TextBox)row.FindControl("txtA1");
                            D6 = Convert.ToDouble(txtA1.Text);
                            TextBox txtDes = (TextBox)row.FindControl("txtA6");
                            D6Remarks = txtDes.Text.Trim();
                        }
                        if (row.RowIndex == 14)
                        {
                            TextBox txtA1 = (TextBox)row.FindControl("txtA1");
                            if (txtA1.Text != "")
                                D7 = Convert.ToDouble(txtA1.Text);
                            else
                                D7 = 0;
                            TextBox txtDes = (TextBox)row.FindControl("txtA6");
                            D7Remarks = txtDes.Text.Trim();
                        }
                        if (row.RowIndex == 15)
                        {
                            TextBox txtA1 = (TextBox)row.FindControl("txtA1");
                            AdjAmt = Convert.ToDouble(txtA1.Text);

                        }
                        if (row.RowIndex == 16)
                        {
                            TextBox txtA1 = (TextBox)row.FindControl("txtA1");
                            EmpPen = Convert.ToDouble(txtA1.Text);

                        }
                        if (row.RowIndex == 17)
                        {
                            TextBox txtA1 = (TextBox)row.FindControl("txtA1");
                            Remarks = txtA1.Text.Trim();

                        }
                        //AttendanceDAC.HR_SavePaySLIP_Vacation(empid, stdt, 1);
                    }
                }
                int month1 = 0, year1 = 0;
                if (txtdate.Text != "" || txtdate.Text != string.Empty)
                {
                    month1 = (DateTime.ParseExact(txtdate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).Month);
                    year1 = (DateTime.ParseExact(txtdate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).Year);
                }
                DateTime Setdt = DateTime.ParseExact(txtdate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                int id = AttendanceDAC.AddVacationSettlementRev4(A1, A2, A3, A4, A5, A6, A7, D1, D2, D3, D4, D5, D6, D7, empid, Convert.ToInt32(Session["CompanyID"]), Remarks, Amt, "Final Settlement", month1, year1, A6Remarks, A7Remarks, D6Remarks, D7Remarks, Gratuityamt, "F", lblPresendays.Text, lblNoOfDays.Text, Convert.ToInt32(Session["vstatus"]), Setdt,AdjAmt,EmpPen,0,
                    A1Label,A2Label,A3Label,A4Label,A5Label,D1Label,D2Label,D3Label,D4Label,D5Label,GratutityRemarks, D3Remarks);
                if (id == 1)
                {
                    tblNew.Visible = false;
                    tblView.Visible = true;
                    tblEdit.Visible = false;
                    AlertMsg.MsgBox(Page, "Saved Successfully");
                    BindGridView();
                }
                else if (id == 2)
                {
                    tblNew.Visible = false;
                    tblView.Visible = true;
                    tblEdit.Visible = false;
                    AlertMsg.MsgBox(Page, "Updated Successfully");
                    BindGridView();
                }
                else
                    AlertMsg.MsgBox(Page, "Already Saved");
            }
            catch (Exception ex)
            {
                clsErrorLog.HMSEventLog(ex, "Final Settlement", "btnAccPost_Click", "008");
            }
        }
       
        protected void dtlvacation_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            GridView GridView1 = (GridView)e.Item.FindControl("GridView1");
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
                    LinkButton lnkbtnadv = (LinkButton)e.Row.FindControl("lnkatt_add");
                    lnkbtnadv.Visible = true;
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
      
       
      
        protected void btnCal_Click(object sender, EventArgs e)
        {
            try
            {


                HRCommon objcommon = new HRCommon();
                objcommon.PageSize = GrtPaging.ShowRows;
                objcommon.CurrentPage = GrtPaging.CurrentPage;
                //if (Convert.ToInt32(ddlworksite.SelectedValue) > 0)
                //{
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
            catch (Exception ex)
            {
                clsErrorLog.HMSEventLog(ex, "Final Settlement", "btnCal_Click", "003");
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
                clsErrorLog.HMSEventLog(ex, "Final Settlement", "btnUpdate_Click", "004");
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
            catch (Exception ex)
            {
                clsErrorLog.HMSEventLog(ex, "Final Settlement", "gvExpactdetails_RowDataBound", "005");
            }
        }
        protected void lnkViewAttendance_Click(object sender, EventArgs e)
        {
            try
            {
                int Empid = 0;
                if (txtempid1.Value != "" || txtempid1.Value != null)
                {
                    Empid = Convert.ToInt32(txtempid1.Value);
                }

                string url = "ViewAttendance.aspx?Empid=" + Empid + "&Month=" + AttMonth + "&Year=" + AttYear;
                string fullURL = "window.open('" + url + "', '_blank' );";
                ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", fullURL, true);
            }
            catch (Exception ex)
            {
                clsErrorLog.HMSEventLog(ex, "Final Settlement", "lnkViewAttendance_Click", "006");
            }
        }
        protected void lnkatt_add_click(object sender, EventArgs e)
        {
            BindAdvAtt();
        }
        protected void gvAdvAttendance_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "UPD")
            {
                int EmpID = Convert.ToInt32(e.CommandArgument);
                string ddlAttendance = "ddlAttendance";
                GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                SqlParameter[] sqlParams = new SqlParameter[2];
                sqlParams[0] = new SqlParameter("@empid", txtempid1.Value.Trim());
                sqlParams[1] = new SqlParameter("@settlementdate", DateTime.ParseExact(txtdate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture));
                DataSet dss = SQLDBUtil.ExecuteDataset("USP_AdvAtt_Vacational", sqlParams);
                if (dss != null && dss.Tables.Count > 0)
                {
                    for (int i = 1; i <= dss.Tables[0].Rows.Count; i++)
                    {
                        DateTime Date2 = CodeUtilHMS.ConvertToDate(dss.Tables[0].Rows[i - 1][2].ToString(), CodeUtilHMS.DateFormat.DayMonthYear);
                        DropDownList ddl = (DropDownList)row.FindControl(ddlAttendance + i);
                        SqlParameter[] sqlParam = new SqlParameter[4];
                        sqlParam[0] = new SqlParameter("@Empid", EmpID);
                        sqlParam[1] = new SqlParameter("@Status", ddl.SelectedValue);
                        sqlParam[2] = new SqlParameter("@date", Date2);
                        sqlParam[3] = new SqlParameter("@MarkedBy",  Convert.ToInt32(Session["UserId"]));

                        SQLDBUtil.ExecuteNonQuery("HMS_InsAdvAttendacne", sqlParam);
                    }
                }

                AlertMsg.MsgBox(Page, "Saved !");

            }
        }
        protected void gvAdvAttendance_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Footer)
            {

            }
            else
            {
                if (e.Row.RowType == DataControlRowType.Header)
                {
                    SqlParameter[] sqlParams = new SqlParameter[2];
                    sqlParams[0] = new SqlParameter("@empid", txtempid1.Value.Trim());
                    sqlParams[1] = new SqlParameter("@settlementdate", DateTime.ParseExact(txtdate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture));
                    DataSet dss = SQLDBUtil.ExecuteDataset("USP_AdvAtt_Vacational", sqlParams);
                    if (dss != null && dss.Tables.Count > 0)
                    {
                        for (int i = 0; i < dss.Tables[0].Rows.Count; i++)
                        {
                            e.Row.Cells[i + 2].Text = dss.Tables[0].Rows[i][0].ToString();
                            gvAdvAttendance.Columns[i + 2].Visible = true;
                        }
                    }
                }
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    try
                    {

                        string ddlAttendance = "ddlAttendance";
                        SqlParameter[] sqlParams = new SqlParameter[2];
                        sqlParams[0] = new SqlParameter("@empid", txtempid1.Value.Trim());
                        sqlParams[1] = new SqlParameter("@settlementdate", DateTime.ParseExact(txtdate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture));
                        DataSet dss = SQLDBUtil.ExecuteDataset("USP_AdvAtt_Vacational", sqlParams);
                        if (dss != null && dss.Tables.Count > 0)
                        {
                            for (int i = 1; i <= dss.Tables[0].Rows.Count; i++)
                            {
                                DropDownList ddl = (DropDownList)e.Row.FindControl(ddlAttendance + i);
                                ddl.SelectedValue = Convert.ToInt32(dss.Tables[0].Rows[i - 1][1].ToString()).ToString();
                            }
                        }

                    }
                    catch (Exception ex) { }
                }
            }
        }
        protected void btnClose_Click(object sender, EventArgs e)
        {
            dvadvatt.Visible = false;
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
                #region xxx
                DateTime stdt = new DateTime();
                DateTime enddate = new DateTime();

                if (txtempid1.Value != "" || txtempid1.Value.Length>0 || txtempid1.Value != string.Empty)
                {
                   empid = Convert.ToInt32(txtempid1.Value);                    
                }
                else
                    empid = 0;

                if (txtename.Text != "" || txtename.Text != string.Empty)
                {
                        string s = txtename.Text.ToString();
                        int index = s.IndexOf(' ');
                        ename = s.Substring(index + 1);                   
                }
                else
                    ename = null;


                int Month, Year;
               
                    #region date region

                    if (txtdate.Text != "" || txtdate.Text != string.Empty)
                    {
                        month = (DateTime.ParseExact(txtdate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).Month);
                        //month = Dmonth.Month;
                        year = (DateTime.ParseExact(txtdate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).Year);
                    }

                    DataSet startdate = AttendanceDAC.GetStartDate();
                    // for Jan 2016 selection pay slip showing by Gana

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

                    // string st = ddlMonth.SelectedItem.Value + "/" + startdate.Tables[0].Rows[0][0].ToString() + "/" + ddlYear.SelectedItem.Value;
                    string st = Month + "/" + startdate.Tables[0].Rows[0][0].ToString() + "/" + Year;
                    stdt = CODEUtility.ConvertToDate(st, DateFormat.MonthDayYear);
                    enddate = DateTime.ParseExact(txtdate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

                    #endregion
               
                int i = 0;
                try
                {
                    SqlParameter[] p = new SqlParameter[3];
                    p[0] = new SqlParameter("@Year", Year);
                    p[1] = new SqlParameter("@Empid", empid);
                    p[2] = new SqlParameter("@Form", "F");
                    i = Convert.ToInt32(SqlHelper.ExecuteScalar("HMS_CountVacationPost", p));
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                #endregion

                if (i != 0)
                {
                    btnAccPost.Visible = false;
                    AlertMsg.MsgBox(Page, "Already Posted To Accounts");
                    return;
                }
                else
                {
                     
                    // ds = AttendanceDAC.T_HMS_GetVacation(objHrCommon, empid, ename, Convert.ToInt32(ddlworksite.SelectedValue), Convert.ToInt32(ddldepartment.SelectedValue), month, year, stdt, enddate);
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

      DataSet  ds = SQLDBUtil.ExecuteDataset("T_HMS_GetEmployeeVacation_FinalSettlement", sqlParams);
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
                clsErrorLog.HMSEventLog(ex, "Final Settlement", "EmployeBind", "007");
            }
        }
        void EmployeBindEdit(HRCommon objHrCommon)
        {
            try
            {
                int month = 0;
                int year = 0;
                string ename = "";
                if (txtempid1.Value != "" || txtempid1.Value != string.Empty)
                {

                    empid = Convert.ToInt32(txtempid1.Value);
                }
                else
                    empid = 0;
                if (txtename.Text != "" || txtename.Text != string.Empty)
                {
                    string s = txtename.Text.ToString();
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
                    btnAccPost.Visible = false;
                    AlertMsg.MsgBox(Page, "Already Saved");
                    return;
                }
                else
                {
                    DataSet ds = null;
                    if (!chkEnch.Checked)
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
                        btnAccPost.Visible = true;
                    }
                    else
                    {
                        dtlvacationEdit.DataSource = null;
                        dtlvacationEdit.DataBind();
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
        public DataView BindTransdetails(string TransId)
        {
            try
            {
                DataTable dt = new DataTable();
                DataRow row = dt.NewRow();
                dt.Columns.Add("Description", typeof(string));
                dt.Columns.Add("Amount", typeof(string));
                dt.Columns.Add("Details", typeof(string));
                dt.Columns.Add("Det", typeof(string));
                SqlParameter[] sqlParams = new SqlParameter[3];
                sqlParams[0] = new SqlParameter("@empid", txtempid1.Value.Trim());
                sqlParams[1] = new SqlParameter("@settlementdate", DateTime.ParseExact(txtdate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture));
                sqlParams[2] = new SqlParameter("@form", 'F');
                DataSet ds = SQLDBUtil.ExecuteDataset("USP_VactionalSettlementRev4", sqlParams);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (!chkEnch.Checked)
                    {

                        ButtonField btnMeds = new ButtonField();
                        //Initalize the DataField value.
                        btnMeds.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                        btnMeds.CommandName = "Meds";
                        btnMeds.ButtonType = ButtonType.Button;
                        btnMeds.Text = "Meds";
                        btnMeds.Visible = true;

                        //Add the newly created bound field to the GridView.

                        dt.Rows.Add("A1:Salary For the Current Month Attendance ", ds.Tables[0].Rows[0]["A1"].ToString(), ds.Tables[0].Rows[0]["A1Label"].ToString());
                        dt.Rows.Add("A2:Encashment of AL(EAL)", ds.Tables[0].Rows[0]["A2"].ToString(), ds.Tables[0].Rows[0]["A2Label"].ToString());
                        dt.Rows.Add("A3:HRA for LOP", ds.Tables[0].Rows[0]["A3"].ToString(), ds.Tables[0].Rows[0]["A3Label"].ToString());
                        dt.Rows.Add("A4:Over-Time Amount", ds.Tables[0].Rows[0]["A4"].ToString(), ds.Tables[0].Rows[0]["A4Label"].ToString());
                        dt.Rows.Add("A5:Air Ticket Reimbursement", "0");
                        dt.Rows.Add("A6:Exit Entry Visa Reimbursement", 0);
                        dt.Rows.Add("A7:", 0);
                        decimal gamt = 0;
                        int accpost = 1;
                        HRCommon objcommon = new HRCommon();
                        objcommon.PageSize = GrtPaging.ShowRows;
                        objcommon.CurrentPage = GrtPaging.CurrentPage;
                        DateTime edt = DateTime.ParseExact(txtdate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                        DataSet dss = AttendanceDAC.GetGratuityDetails_Final(objcommon, CompanyID, 0, txtempid1.Value.Trim(), accpost, Convert.ToInt32(ddlEmpDeAct.SelectedValue), edt);
                        if (dss != null && dss.Tables.Count > 0)
                        {
                            if (dss.Tables[0].Rows.Count > 0)
                            {
                                gamt = Convert.ToDecimal(dss.Tables[0].Rows[0]["gamt"].ToString());
                            }
                        }
                        dt.Rows.Add("Gratuity", gamt);
                        dt.Rows.Add("D1: DYNAMISM ", ds.Tables[0].Rows[0]["D1"].ToString(), ds.Tables[0].Rows[0]["D1Label"].ToString());
                        dt.Rows.Add("D2: Other manual deductions ", "0", "");
                        dt.Rows.Add("D3:OutStanding  Advances", ds.Tables[0].Rows[0]["D3"].ToString());

                        dt.Rows.Add("D4:Absent Penalty ", ds.Tables[0].Rows[0]["D4"].ToString(), ds.Tables[0].Rows[0]["D4Label"].ToString());
                        dt.Rows.Add("D5:Expat ", 0, exptdetails, btnMeds);
                        dt.Rows.Add("D6:", 0);
                        dt.Rows.Add("D7:", 0);
                        dt.Rows.Add("AdjAmt:", ds.Tables[0].Rows[0]["AdjAmt"].ToString());
                        dt.Rows.Add("Emp Pen:", ds.Tables[0].Rows[0]["EmpPenAmt"].ToString());
                        dt.Rows.Add("Remarks", "");
                        lblPresendays.Text = ds.Tables[1].Rows[0][0].ToString();
                        lblNoOfDays.Text = ds.Tables[2].Rows[0][0].ToString();
                        Session["vstatus"] = 1;
                    }
                    else
                    {

                        dt.Rows.Add("A1:Salary For the Current Month Attendance ", "0");
                        //decimal EAL = Convert.ToDecimal(ds.Tables[16].Rows[0]["HRA"].ToString());
                        //decimal EALGlobal = EAL;

                        dt.Rows.Add("A2:Encashment of AL(EAL)", ds.Tables[0].Rows[0]["A2"].ToString(), ds.Tables[0].Rows[0]["A2Label"].ToString());
                        dt.Rows.Add("A3:HRA for LOP", ds.Tables[0].Rows[0]["A3"].ToString(), ds.Tables[0].Rows[0]["A3Label"].ToString());
                        dt.Rows.Add("A4:Over-Time Amount", "0");
                        dt.Rows.Add("A5:Air Ticket Reimbursement", ds.Tables[0].Rows[0]["A5"].ToString(), ds.Tables[0].Rows[0]["A5label"].ToString());
                        dt.Rows.Add("A6:Exit Entry Visa Reimbursement", 0);
                        dt.Rows.Add("A7:", 0);
                        dt.Rows.Add("D1: DYNAMISM ", "0");
                        dt.Rows.Add("D2: Other manual deductions ", "0", "");
                        dt.Rows.Add("D3:OutStanding  Advances", "0");
                        dt.Rows.Add("D4:Absent Penalty ", "0");
                        dt.Rows.Add("D5:Expat ", 0);
                        dt.Rows.Add("D6:", 0);
                        dt.Rows.Add("D7:", 0);
                        dt.Rows.Add("AdjAmt:", ds.Tables[0].Rows[0]["AdjAmt"].ToString());
                        dt.Rows.Add("Emp Pen:", ds.Tables[0].Rows[0]["EmpPenAmt"].ToString());
                        dt.Rows.Add("Remarks", "");
                        lblPresendays.Text = ds.Tables[1].Rows[0][0].ToString();
                        lblNoOfDays.Text = ds.Tables[2].Rows[0][0].ToString();
                        Session["vstatus"] = 1;
                    }
                }

                DataView dv = dt.DefaultView;
                return dv;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public void BindGridGratuity(HRCommon objcommon, int WSID)
        {
            try
            {
                int CompanyID = Convert.ToInt32(ddlFCompany.SelectedValue);
                objcommon.PageSize = GrtPaging.ShowRows;
                objcommon.CurrentPage = GrtPaging.CurrentPage;
                 
                int accpost = 1;
                DataTable datatable2 = new DataTable();
                DataSet ds = AttendanceDAC.GetGratuityDetails_Final(objcommon, CompanyID, 0, txtempid1.Value.Trim(), accpost, 1, Convert.ToDateTime("05/13/2016"));
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {

                    }
                }
            }
            catch (Exception ex)
            {
                clsErrorLog.HMSEventLog(ex, "Final Settlement", "BindGridGratuity", "008");
            }
        }
        public void BindCompanyList()
        {
            DataSet dscomp = AttendanceDAC.HR_GetCompanyList();

            ddlFCompany.DataSource = dscomp;
            ddlFCompany.DataValueField = "companyid";
            ddlFCompany.DataTextField = "companyname";
            ddlFCompany.DataBind();

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
              
                btnSubmit.Enabled = Editable = Convert.ToBoolean(ds.Tables[0].Rows[0]["Editable"].ToString());
               
            }
            return MenuId;
        }
        public void BindGrid()
        {
            DataSet ds = AttendanceDAC.GetDesignationsList();
        }
        public void BindDetails(int ID)
        {
            objHrCommon.PageSize = 10;
            objHrCommon.CurrentPage = 1;
            DataSet ds = AttendanceDAC.T_HMS_empVsAirTicketsAuth_LISTbyID_status(objHrCommon, true, ID, null, null);
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
             
                DataSet ds = PayRollMgr.GetPaySLIP_CTH(EmpID, Convert.ToDateTime(txtDOS.Text.Trim()));
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
       
        public void Clear()
        {
            //   txtName.Text = "";
            ViewState["CateId"] = "";
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
        public DataSet FillAttandanceType()
        {

            return (DataSet)ViewState["AttTypes"];

        }
        private void BindAtttypes()
        {
             
          DataSet  ds = AttendanceDAC.GetAttendanceType();
            ViewState["AttTypes"] = ds;
        }
        private void BindAdvAtt()
        {
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[2];
                sqlParams[0] = new SqlParameter("@empid", txtempid1.Value.Trim());
                sqlParams[1] = new SqlParameter("@settlementdate", DateTime.ParseExact(txtdate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture));
                DataSet ds = SQLDBUtil.ExecuteDataset("sp_PivotedResultSet_AttendanceAdvance_Vacational", sqlParams);
                if (ds != null && ds.Tables.Count > 0)
                {
                    gvAdvAttendance.DataSource = ds.Tables[0];
                    gvAdvAttendance.DataBind();
                    dvadvatt.Visible = true;
                }
            }
            catch (Exception ex) { }
        }
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

                tblNew.Visible = false;
                tblView.Visible = false;
                tblEdit.Visible = true;


                //DataColumn column = new DataColumn();
                dt.Columns.Add("Description", typeof(string));
                dt.Columns.Add("Amount", typeof(string));
                dt.Columns.Add("Details", typeof(string));
                dt.Columns.Add("Det", typeof(string));

                dt.Rows.Add("A1:Salary For the Current Month Attendance ", ds.Tables[0].Rows[0]["A1"].ToString());
                dt.Rows.Add("A2:Encashment of AL(EAL)", ds.Tables[0].Rows[0]["A2"].ToString());
                dt.Rows.Add("A3:HRA for LOP", ds.Tables[0].Rows[0]["A3"].ToString());
                dt.Rows.Add("A4:Over-Time Amount", ds.Tables[0].Rows[0]["A4"].ToString());
                dt.Rows.Add("A5:Air Ticket Reimbursement", "0");
                dt.Rows.Add("A6:Exit Entry Visa Reimbursement", 0);
                dt.Rows.Add("A7:", 0);
                dt.Rows.Add("D1: DYNAMISM ", ds.Tables[0].Rows[0]["D1"].ToString());
                dt.Rows.Add("D2: Other manual deductions ", "0", "");
                dt.Rows.Add("D3:OutStanding  Advances", ds.Tables[0].Rows[0]["D3"].ToString());

                dt.Rows.Add("D4:Absent Penalty ", ds.Tables[0].Rows[0]["D4"].ToString());
                dt.Rows.Add("D5:Expat ", 0, exptdetails, btnMeds);
                dt.Rows.Add("D6:", 0);
                dt.Rows.Add("D7:", 0);
                dt.Rows.Add("AdjAmt:", ds.Tables[0].Rows[0]["AdjAmt"].ToString());
                dt.Rows.Add("Emp Pen:", ds.Tables[0].Rows[0]["EmpPenAmt"].ToString());
                dt.Rows.Add("Remarks", "");
                lblPresendays.Text = ds.Tables[1].Rows[0][0].ToString();
                lblNoOfDays.Text = ds.Tables[2].Rows[0][0].ToString();
                Session["vstatus"] = 2;
                Button4.Visible = true;
            }
            DataView dv = dt.DefaultView;
            return dv;
        }
        protected void gvFinal_RowDataBound(object sender, GridViewRowEventArgs e)
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
                e.Row.Cells[3].ToolTip = "A1=" + (e.Row.DataItem as DataRowView)["A1"].ToString() +
                                          "\nA2=" + (e.Row.DataItem as DataRowView)["A2"].ToString() +
                                          "\nA3=" + (e.Row.DataItem as DataRowView)["A3"].ToString() +
                                          "\nA4=" + (e.Row.DataItem as DataRowView)["A4"].ToString() +
                                          "\nA5=" + (e.Row.DataItem as DataRowView)["A5"].ToString() +
                                          "\nA6=" + (e.Row.DataItem as DataRowView)["A6"].ToString() +
                                          "\nA7=" + (e.Row.DataItem as DataRowView)["A7"].ToString();

                e.Row.Cells[4].ToolTip = "D1=" + (e.Row.DataItem as DataRowView)["D1"].ToString() +
                                          "\nD2=" + (e.Row.DataItem as DataRowView)["D2"].ToString() +
                                          "\nD3=" + (e.Row.DataItem as DataRowView)["D3"].ToString() +
                                          "\nD4=" + (e.Row.DataItem as DataRowView)["D4"].ToString() +
                                          "\nD5=" + (e.Row.DataItem as DataRowView)["D5"].ToString() +
                                          "\nD6=" + (e.Row.DataItem as DataRowView)["D6"].ToString() +
                                          "\nD7=" + (e.Row.DataItem as DataRowView)["D7"].ToString();
            }
        }
        public void BindGridView()
        {
            objHrCommon.PageSize = AdvancedLeaveAppOthPaging.ShowRows;
            objHrCommon.CurrentPage = AdvancedLeaveAppOthPaging.CurrentPage;

            int empid = 0;
            if (txtEmpName.Text != "" || txtEmpName.Text != null)
                empid = Convert.ToInt32(txtEmpNameHidden.Value == "" ? "0" : txtEmpNameHidden.Value);
            else
                txtEmpNameHidden.Value = String.Empty;

             

            SqlParameter[] sqlParams = new SqlParameter[5];
            sqlParams[4] = new SqlParameter("@Empid", empid);
            sqlParams[0] = new SqlParameter("@CurrentPage", objHrCommon.CurrentPage);
            sqlParams[1] = new SqlParameter("@PageSize", objHrCommon.PageSize);
            sqlParams[2] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
            sqlParams[2].Direction = ParameterDirection.ReturnValue;
            sqlParams[3] = new SqlParameter("@NoofRecords", System.Data.SqlDbType.Int);
            sqlParams[3].Direction = ParameterDirection.Output;
            DataSet ds = SQLDBUtil.ExecuteDataset("HMS_GET_FinalSetteldPendingDetails", sqlParams);
            objHrCommon.NoofRecords = (int)sqlParams[3].Value;
            objHrCommon.TotalPages = (int)sqlParams[2].Value;
            gvFinal.DataSource = ds;
            gvFinal.DataBind();
            AdvancedLeaveAppOthPaging.Bind(objHrCommon.CurrentPage, objHrCommon.TotalPages, objHrCommon.NoofRecords, objHrCommon.PageSize);
            txtEmpNameHidden.Value = "";

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


    }
}