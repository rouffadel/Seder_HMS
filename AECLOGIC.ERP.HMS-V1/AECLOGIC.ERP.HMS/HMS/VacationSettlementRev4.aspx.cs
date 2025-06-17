using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AECLOGIC.HMS.BLL;
using System.Data;
using System.Data.SqlClient;
using Aeclogic.Common.DAL;
using System.IO;
using System.Configuration;
using System.Collections;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using AjaxControlToolkit;
using AECLOGIC.ERP.COMMON;
using System.Globalization;
using AECLOGIC.ERP.HMS;
using System.Security.Cryptography;

namespace AECLOGIC.ERP.HMSV1
{
    public partial class VacationSettlementRev4V1 : AECLOGIC.ERP.COMMON.WebFormMaster
    {
        #region Variables
        string stt = "V";
        TableRow tblRow;
        int stmonth = 0; int edmonth = 0;
        int EMPIDPram = 0;
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
        int status, LID = 0;
        #endregion Variables
        #region Events
        protected void Page_Load(object sender, EventArgs e)
        {  
            try
            {
                Page.Form.Attributes.Add("enctype", "multipart/form-data");
                //ScriptManager.RegisterAsyncPostBackControl(fudDocument1);

                if (!IsPostBack)
                {
                    status = 0;
                    GetParentMenuId();
                    tractions.Visible = false;
                    trwarning.Visible = false;
                    tddetails.Visible = false;
                    tickimgk.Visible = false;
                    notfoundk.Visible = true;
                    dvadvatt.Visible = false;
                    tbshow.Visible = false;
                    BindAtttypes();
                    FIllObject.FillDropDown(ref ddlEmpDeAct, "sh_EmployeeExitReasons");
                    ddlEmpDeAct.SelectedIndex = 1;
                    txtDOS.Text = DateTime.Now.ToString("dd-MMM-yyyy");
                    ViewState["CateId"] = "";
                    ViewState["nCnt"] = "0";
                    if (Request.QueryString.Count > 0)
                    {
                        if (Request.QueryString.AllKeys.Contains("LID"))
                        {
                            if (Convert.ToInt32(Request.QueryString["LID"]) > 0)
                            {
                                LID = Convert.ToInt32(Request.QueryString["LID"]);
                            }
                        }
                    }
                    else
                        LID = 0;
                    if (Convert.ToInt32(Request.QueryString["key"]) == 1)
                    {
                        tblNew.Visible = true;
                        tblView.Visible = false;
                        tblEdit.Visible = false;
                        txtempid1.Value = Request.QueryString["empid"];
                        DataSet ds = AttendanceDAC.GetSearchEmpName(txtempid1.Value);
                        DataTable dt = ds.Tables[0];
                        foreach (DataRow row in dt.Rows)
                        {
                            if (row["ID"].ToString() == txtempid1.Value)
                            {
                                //string str = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(row["NAME"].ToString(), row["ID"].ToString());
                                txtename.Text = row["NAME"].ToString();
                                txtename.ReadOnly = true;
                                break;
                            }
                        }
                        if (Convert.ToInt32(Request.QueryString["id"]) == 1)
                        {
                            chkFinal.Checked = true;
                            dvfnactive.Visible = true;
                            lnkViewLeaveGrant.Visible = false;
                        }
                        else
                        {
                            chkFinal.Checked = false;
                            lnkViewLeaveGrant.Visible = true;
                        }
                        btnSearchLeaveDet_Click(sender, e);
                        Button1.Visible = false;
                    }
                    else
                    {
                        chkFinal.Enabled = true;
                        Button1.Visible = true;
                        Button1.Enabled = true;
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
                clsErrorLog.HMSEventLog(ex, "Vactional Settlement", "Page_Load", "001");
            }
        }
        void AdvancedLeaveAppOthPaging_ShowRowsClick(object sender, EventArgs e)
        {
            BindGridView();
        }
        void AdvancedLeaveAppOthPaging_FirstClick(object sender, EventArgs e)
        {
            BindGridView();
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
                    EmployeBind(objHrCommon);
                }
                catch (Exception DelDesig)
                {
                    AlertMsg.MsgBox(Page, DelDesig.Message.ToString(), AlertMsg.MessageType.Error);
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
                int Output = AttendanceDAC.Ins_Upd_T_HMS_vacationsattlement(0, Convert.ToInt32(txtempid.Text), dt_roj, Convert.ToDecimal(lblsal.Text), dt_LOS, Convert.ToDateTime(txtDOS.Text.Trim()), Convert.ToInt32(lblDW.Text), Convert.ToInt32(lblDD.Text), Convert.ToInt32(lblDeff.Text), Convert.ToInt32(lblTs.Text), Convert.ToDecimal(lblAlGross.Text), 1, lblOTHr.Text, lblA1.Text, lblA2.Text, lblA3.Text, lblA4.Text, lblA5.Text, lblA6.Text, lblA7.Text, lblD1.Text, lblD2.Text, lblD3.Text, lblD4.Text, lblD5.Text, lblD6.Text, lblD7.Text, lblnetAmt.Text, Convert.ToInt32(Session["UserId"]));
                if (Output == 1)
                    AlertMsg.MsgBox(Page, "Done", AlertMsg.MessageType.Success);
                else if (Output == 2)
                    AlertMsg.MsgBox(Page, "Updated", AlertMsg.MessageType.Success);
                else
                    AlertMsg.MsgBox(Page, "Already Exists", AlertMsg.MessageType.Warning);
                EmployeBind(objHrCommon);
                tblNewk.Visible = false;
                tblEdit.Visible = true;
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
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            DataSet dss = SQLDBUtil.ExecuteDataset("sh_dynamicviewscreation_rev4");
            EmployeBind(objHrCommon);
            QtyChanged(sender, e);
            if (txtempid1.Value != "" || txtempid1.Value != null)
            {
                lnkViewAttendance.Visible = true;
                lnkViewLeaveGrant.Visible = true; 
                lnksalryBrkup.Visible = true;
                btnPrint.Visible = true;
                txtManualPayableDays.Visible = true;
                tblN.Visible = true;
            }
        }
        protected void btnVacation_Click(object sender, EventArgs e)
        {
        }
        protected void btnAccPost_Click(object sender, EventArgs e)
        {
            try
            {
                //startdate();
                Double A1 = 0, A2 = 0, A3 = 0, A4 = 0, A5 = 0, A6 = 0, A7 = 0,A8=0, D1 = 0, D2 = 0, D3 = 0, D4 = 0, D5 = 0, D6 = 0, D7 = 0, AdjAmt = 0, EmpPen = 0, GratuityAmt = 0, D3LoanAmount = 0;
                string Remarks = ""; string A6Remarks = ""; string A7Remarks = ""; string A8Remarks = ""; string D6Remarks = ""; string D7Remarks = "", A1Label = "";string D3Remarks = "";string LoanProof = "";int LoanCheck = 0;string AirticketProof = "";
                string A2Label = "", A3Label = "", A4Label = "", A5Label = "", D1Label = "", D2Label = "", D3Label = "", D4Label = "", D5Label = "", GratutityRemarks = "";
                string A1Remarks = ""; string A2Remarks = ""; string A3Remarks = ""; string A4Remarks = ""; string A5Remarks = ""; string D1Remarks = ""; string D2Remarks = ""; string D4Remarks = ""; string D5Remarks = ""; string D8Remarks = ""; string D9Remarks = "";
                string GratuityRemarks1 = "";

                //foreach (DataListItem li in dtlvacation.Items)
                //{
                //    GridView gv = (GridView)li.FindControl("GVVacation1");
                foreach (GridViewRow row in GVVacation1.Rows)
                    {
                        if (row.RowIndex == 0)
                        {
                            TextBox txtA1 = (TextBox)row.FindControl("txtA1");
                            if (txtA1.Text != "")
                            {
                                A1 = Convert.ToDouble(txtA1.Text);
                                A1Label = row.Cells[4].Text.Trim();
                            }
                            else
                            {
                                A1 = 0;
                            }
                            TextBox txtDes = (TextBox)row.FindControl("txtA6");
                            A1Remarks = txtDes.Text.Trim();
                        }
                        if (row.RowIndex == 1)
                        {
                            TextBox txtA1 = (TextBox)row.FindControl("txtA1");
                            if (txtA1.Text != "")
                            {
                                A2 = Convert.ToDouble(txtA1.Text);
                                A2Label = row.Cells[4].Text.Trim();
                            }
                            else
                            {
                                A2 = 0;
                            }
                            TextBox txtDes = (TextBox)row.FindControl("txtA6");
                            A2Remarks = txtDes.Text.Trim();
                        }
                        if (row.RowIndex == 2)
                        {
                            TextBox txtA1 = (TextBox)row.FindControl("txtA1");
                            if (txtA1.Text != "")
                            {
                                A3 = Convert.ToDouble(txtA1.Text);
                                A3Label = row.Cells[4].Text.Trim();
                            }
                            else
                            {
                                A3 = 0;
                            }
                            TextBox txtDes = (TextBox)row.FindControl("txtA6");
                            A3Remarks = txtDes.Text.Trim();
                        }
                        if (row.RowIndex == 3)
                        {
                            TextBox txtA1 = (TextBox)row.FindControl("txtA1");
                            if (txtA1.Text != "")
                            {
                                A4 = Convert.ToDouble(txtA1.Text);
                                A4Label = row.Cells[4].Text.Trim();
                            }
                            else
                            {
                                A4 = 0;
                            }
                            TextBox txtDes = (TextBox)row.FindControl("txtA6");
                            A4Remarks = txtDes.Text.Trim();
                        }
                        if (row.RowIndex == 4)
                        {
                            TextBox txtA1 = (TextBox)row.FindControl("txtA1");
                            if (txtA1.Text != "")
                            {
                                A5 = Convert.ToDouble(txtA1.Text);
                                A5Label = row.Cells[4].Text.Trim();
                            }
                            else
                            {
                                A5 = 0;
                            }
                            AirticketProof = hdnAirticketFile.Value.ToString();
                            TextBox txtDes = (TextBox)row.FindControl("txtA6");
                            A5Remarks = txtDes.Text.Trim();
                        }
                        if (row.RowIndex == 5)
                        {
                            TextBox txtA1 = (TextBox)row.FindControl("txtA1");
                            if (txtA1.Text != "")
                                A6 = Convert.ToDouble(txtA1.Text);
                            else
                            {
                                A6 = 0;
                            }
                            TextBox txtDes = (TextBox)row.FindControl("txtA6");
                            A6Remarks = txtDes.Text.Trim();
                        }
                        if (row.RowIndex == 6)
                        {
                            TextBox txtA1 = (TextBox)row.FindControl("txtA1");
                            if (txtA1.Text != "")
                                A7 = Convert.ToDouble(txtA1.Text);
                            else
                            {
                                A7 = 0;
                            }
                            TextBox txtDes = (TextBox)row.FindControl("txtA6");
                            A7Remarks = txtDes.Text.Trim();
                        }
                        if (row.RowIndex == 7)
                        {
                            TextBox txtA1 = (TextBox)row.FindControl("txtA1");
                            if (txtA1.Text != "")
                                A8 = Convert.ToDouble(txtA1.Text);
                            else
                            {
                                A8 = 0;
                            }
                            TextBox txtDes = (TextBox)row.FindControl("txtA6");
                            A8Remarks = txtDes.Text.Trim();
                        }
                    if (row.RowIndex == 10)
                        {
                            TextBox txtDes = (TextBox)row.FindControl("txtA6");
                            D3Remarks = txtDes.Text.Trim();
                            CheckBox chkLoan = (CheckBox)row.FindControl("chkLoanSelect");
                            if (chkLoan.Checked)
                            { 
                                LoanCheck = 1;
                                LoanProof = hdnupload.Value.ToString();
                            }
                            if(hdnLoanAmount.Value.ToString() != "")
                                D3LoanAmount = Convert.ToDouble(hdnLoanAmount.Value.ToString());
                        }
                        if (chkFinal.Checked)
                        {
                        if (row.RowIndex == 8)
                        {
                            TextBox txtA1 = (TextBox)row.FindControl("txtA1");
                            if (txtA1.Text != "")
                            {
                                GratuityAmt = Convert.ToDouble(txtA1.Text);
                                GratutityRemarks = row.Cells[4].Text;
                            }
                            else
                            {
                                GratuityAmt = 0;
                            }
                            TextBox txtDes = (TextBox)row.FindControl("txtA6");
                            GratuityRemarks1 = txtDes.Text.Trim();
                        }
                        if (row.RowIndex == 9)
                        {
                            TextBox txtA1 = (TextBox)row.FindControl("txtA1");
                            if (txtA1.Text != "")
                            {
                                D1 = Convert.ToDouble(txtA1.Text);
                                D1Label = row.Cells[4].Text.Trim();
                            }
                            else
                            {
                                D1 = 0;
                            }
                            TextBox txtDes = (TextBox)row.FindControl("txtA6");
                            D1Remarks = txtDes.Text.Trim();
                        }
                        if (row.RowIndex == 10)
                        {
                            TextBox txtA1 = (TextBox)row.FindControl("txtA1");
                            if (txtA1.Text != "")
                            {
                                D2 = Convert.ToDouble(txtA1.Text);
                                D2Label = row.Cells[4].Text.Trim();
                            }
                            else
                            {
                                D2 = 0;
                            }
                            TextBox txtDes = (TextBox)row.FindControl("txtA6");
                            D2Remarks = txtDes.Text.Trim();
                        }
                        if (row.RowIndex == 11)
                        {
                            TextBox txtA1 = (TextBox)row.FindControl("txtA1");
                            if (txtA1.Text != "")
                            {
                                D3 = Convert.ToDouble(txtA1.Text);
                                D3Label = row.Cells[4].Text.Trim();
                            }
                            else
                            {
                                D3 = 0;
                            }
                            TextBox txtDes = (TextBox)row.FindControl("txtA6");
                            D3Remarks = txtDes.Text.Trim();
                        }
                        if (row.RowIndex == 12)
                        {
                            TextBox txtA1 = (TextBox)row.FindControl("txtA1");
                            if (txtA1.Text != "")
                            {
                                D4 = Convert.ToDouble(txtA1.Text);
                                D4Label = row.Cells[4].Text.Trim();
                            }
                            else
                            {
                                D4 = 0;
                            }
                            TextBox txtDes = (TextBox)row.FindControl("txtA6");
                            D4Remarks = txtDes.Text.Trim();
                        }
                        if (row.RowIndex == 13)
                        {
                            TextBox txtA1 = (TextBox)row.FindControl("txtA1");
                            if (txtA1.Text != "")
                            {
                                D5 = Convert.ToDouble(txtA1.Text);
                                D5Label = row.Cells[4].Text.Trim();
                            }
                            else
                            {
                                D5 = 0;
                            }
                            TextBox txtDes = (TextBox)row.FindControl("txtA6");
                            D5Remarks = txtDes.Text.Trim();
                        }
                            if (row.RowIndex == 14)
                            {
                                TextBox txtA1 = (TextBox)row.FindControl("txtA1");
                                if (txtA1.Text != "")
                                    D6 = Convert.ToDouble(txtA1.Text);
                                else
                                {
                                    D6 = 0;
                                }
                                TextBox txtDes = (TextBox)row.FindControl("txtA6");
                                D6Remarks = txtDes.Text.Trim();
                            }
                            if (row.RowIndex == 15)
                            {
                                TextBox txtA1 = (TextBox)row.FindControl("txtA1");
                                if (txtA1.Text != "")
                                    D7 = Convert.ToDouble(txtA1.Text);
                                else
                                {
                                    D7 = 0;
                                }
                                TextBox txtDes = (TextBox)row.FindControl("txtA6");
                                D7Remarks = txtDes.Text.Trim();
                            }
                            if (row.RowIndex == 16)
                            {
                                TextBox txtA1 = (TextBox)row.FindControl("txtA1");
                                if (txtA1.Text != "")
                                    AdjAmt = Convert.ToDouble(txtA1.Text);
                                else
                                {
                                    AdjAmt = 0;
                                }
                            TextBox txtDes = (TextBox)row.FindControl("txtA6");
                            D8Remarks = txtDes.Text.Trim();
                        }
                            if (row.RowIndex == 17)
                            {
                                TextBox txtA1 = (TextBox)row.FindControl("txtA1");
                                if (txtA1.Text != "")
                                    EmpPen = Convert.ToDouble(txtA1.Text);
                                else
                                    EmpPen = 0;

                            TextBox txtDes = (TextBox)row.FindControl("txtA6");
                            D9Remarks = txtDes.Text.Trim();
                        }
                            if (row.RowIndex == 18)
                            {
                                TextBox txtA1 = (TextBox)row.FindControl("txtA6");
                                Remarks = txtA1.Text.Trim();
                            }
                        }
                        else
                        {
                            if (row.RowIndex == 8)
                            {
                                TextBox txtA1 = (TextBox)row.FindControl("txtA1");
                                if (txtA1.Text != "")
                                {
                                    D1 = Convert.ToDouble(txtA1.Text);
                                    D1Label = row.Cells[4].Text.Trim();
                                }
                                else
                                {
                                    D1 = 0;
                                }
                            TextBox txtDes = (TextBox)row.FindControl("txtA6");
                            D1Remarks = txtDes.Text.Trim();
                        }
                            if (row.RowIndex == 9)
                            {
                                TextBox txtA1 = (TextBox)row.FindControl("txtA1");
                                if (txtA1.Text != "")
                                {
                                    D2 = Convert.ToDouble(txtA1.Text);
                                    D2Label = row.Cells[4].Text.Trim();
                                }
                                else
                                {
                                    D2 = 0;
                                }
                            TextBox txtDes = (TextBox)row.FindControl("txtA6");
                            D2Remarks = txtDes.Text.Trim();
                        }
                            if (row.RowIndex == 10)
                            {
                                TextBox txtA1 = (TextBox)row.FindControl("txtA1");
                                if (txtA1.Text != "")
                                {
                                    D3 = Convert.ToDouble(txtA1.Text);
                                    D3Label = row.Cells[4].Text.Trim();
                                }
                                else
                                {
                                    D3 = 0;
                                }
                            TextBox txtDes = (TextBox)row.FindControl("txtA6");
                            D3Remarks = txtDes.Text.Trim();
                        }
                            if (row.RowIndex == 11)
                            {
                                TextBox txtA1 = (TextBox)row.FindControl("txtA1");
                                if (txtA1.Text != "")
                                {
                                    D4 = Convert.ToDouble(txtA1.Text);
                                    D4Label = row.Cells[4].Text.Trim();
                                }
                                else
                                {
                                    D4 = 0;
                                }
                            TextBox txtDes = (TextBox)row.FindControl("txtA6");
                            D4Remarks = txtDes.Text.Trim();
                        }
                            if (row.RowIndex == 12)
                            {
                                TextBox txtA1 = (TextBox)row.FindControl("txtA1");
                                if (txtA1.Text != "")
                                {
                                    D5 = Convert.ToDouble(txtA1.Text);
                                    D5Label = row.Cells[4].Text.Trim();
                                }
                                else
                                {
                                    D5 = 0;
                                }
                            TextBox txtDes = (TextBox)row.FindControl("txtA6");
                            D5Remarks = txtDes.Text.Trim();
                        }
                            if (row.RowIndex == 13)
                            {
                                TextBox txtA1 = (TextBox)row.FindControl("txtA1");
                                if (txtA1.Text != "")
                                    D6 = Convert.ToDouble(txtA1.Text);
                                else
                                {
                                    D6 = 0;
                                }
                                TextBox txtDes = (TextBox)row.FindControl("txtA6");
                                D6Remarks = txtDes.Text.Trim();
                            }
                            if (row.RowIndex == 14)
                            {
                                TextBox txtA1 = (TextBox)row.FindControl("txtA1");
                                if (txtA1.Text != "")
                                    D7 = Convert.ToDouble(txtA1.Text);
                                else
                                {
                                    D7 = 0;
                                }
                                TextBox txtDes = (TextBox)row.FindControl("txtA6");
                                D7Remarks = txtDes.Text.Trim();
                            }
                            if (row.RowIndex == 15)
                            {
                                TextBox txtA1 = (TextBox)row.FindControl("txtA1");
                                if (txtA1.Text != "")
                                    AdjAmt = Convert.ToDouble(txtA1.Text);
                                else
                                {
                                    AdjAmt = 0;
                                }
                                TextBox txtDes = (TextBox)row.FindControl("txtA6");
                                D8Remarks = txtDes.Text.Trim();
                            }
                            if (row.RowIndex == 16)
                            {
                                TextBox txtA1 = (TextBox)row.FindControl("txtA1");
                                if (txtA1.Text != "")
                                    EmpPen = Convert.ToDouble(txtA1.Text);
                                else
                                    EmpPen = 0;
                                TextBox txtDes = (TextBox)row.FindControl("txtA6");
                                D9Remarks = txtDes.Text.Trim();
                            }
                            if (row.RowIndex == 17)
                            {
                                TextBox txtA6 = (TextBox)row.FindControl("txtA6");
                                Remarks = txtA6.Text.Trim();
                            }
                        }
                    }
                //}
                string Settlementname = "Vacation Settlement";
                if (chkFinal.Checked)
                {
                    Settlementname = "Final Settlement";
                    stt = "F";
                }
                int month1 = 0, year1 = 0;
                if (txtdate.Text != "" || txtdate.Text != string.Empty)
                {
                    month1 = (DateTime.ParseExact(txtdate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).Month);
                    year1 = (DateTime.ParseExact(txtdate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).Year);
                }
                DateTime Setdt = DateTime.ParseExact(txtdate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                int exittype = 0;
                string filenames = "", ext = "";
                int VsId = 1;
                if (fudDocument1.HasFile)
                {
                    SqlParameter[] parm = new SqlParameter[1];
                    parm[0] = new SqlParameter("@Empid", empid);
                    DataSet ds = SqlHelper.ExecuteDataset("sp_Count_Vsfs", parm);

                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                        VsId = Convert.ToInt32(ds.Tables[0].Rows[0]["VSId"].ToString());
                    filenames = fudDocument1.PostedFile.FileName;
                    ext = filenames.Split('.')[filenames.Split('.').Length - 1];

                    filenames = Server.MapPath("\\VSFS\\" + VsId + "." + ext);
                    fudDocument1.PostedFile.SaveAs(filenames);
                }
                if (chkFinal.Checked)
                    exittype = Convert.ToInt32(ddlEmpDeAct.SelectedValue);
                int id = AddVacationSettlementRev4(A1, A2, A3, A4, A5, A6, A7, D1, D2, D3, D4, D5, D6, D7, empid, Convert.ToInt32(Session["CompanyID"]),
                    Remarks, Amt, Settlementname, month1, year1, A6Remarks, A7Remarks, D6Remarks, D7Remarks, GratuityAmt, stt, lblPresendays.Text,
                    lblNoOfDays.Text, Convert.ToInt32(Session["vstatus"]), Setdt, AdjAmt, EmpPen, exittype
                    , A1Label, A2Label, A3Label, A4Label, A5Label, D1Label, D2Label, D3Label, D4Label, D5Label, GratutityRemarks,D3Remarks, LoanProof,
                    LoanCheck, AirticketProof,A1Remarks,A2Remarks,A3Remarks,A4Remarks,A5Remarks,D1Remarks,D2Remarks,D4Remarks,D5Remarks,D8Remarks,D9Remarks,
                    D3LoanAmount, GratuityRemarks1, ext, filenames,A8,A8Remarks);

                
                    if (id == 1)
                {  
                    tblNew.Visible = false;
                    tblView.Visible = true;
                    tblEdit.Visible = false;
                    AlertMsg.MsgBox(Page, "Saved Successfully", AlertMsg.MessageType.Success);
                    BindGridView();
                }
                else if (id == 2)
                {
                    tblNew.Visible = false;
                    tblView.Visible = true;
                    tblEdit.Visible = false;
                    AlertMsg.MsgBox(Page, "Updated Successfully", AlertMsg.MessageType.Success);
                    BindGridView();
                }
                else
                    AlertMsg.MsgBox(Page, "Already Saved", AlertMsg.MessageType.Warning);
            }
            catch (Exception ex)
            {
                clsErrorLog.HMSEventLog(ex, "Vactional Settlement", "btnAccPost_Click", "003");
            }
        }
        public static int AddVacationSettlementRev4(Double A1, Double A2, Double A3, Double A4, Double A5, Double A6, Double A7, Double D1, Double D2, 
            Double D3, Double D4, Double D5, Double D6, Double D7, int Empid, int Companyid, string Remarks, Double Totalamt, string Ledger, int month,
            int year, string A6Remarks, string A7Remarks, string D6Remarks, string D7Remarks, double gratuity, string form, string Presentdays, string NoOfDays,
            int Status, DateTime SettlementDate, Double AdjAmt, Double EmpPen, int ExitType
            , string A1Label, string A2Label, string A3Label, string A4Label, string A5Label, string D1Label, string D2Label, string D3Label,
            string D4Label, string D5Label, string GratutityRemarks, string D3Remarks, string LoanProof, int LoanCheck, string AirticketProof,
            string A1Remarks, string A2Remarks, string A3Remarks, string A4Remarks, string A5Remarks, string D1Remarks, string D2Remarks, 
            string D4Remarks, string D5Remarks, string D8Remarks, string D9Remarks, Double D3LoanAmount, 
            string GratuityRemarks1,string ext,string filepath,Double A8,string A8Remarks)
        {
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[67];
                sqlParams[0] = new SqlParameter("@A1", A1);
                sqlParams[1] = new SqlParameter("@A2", A2);
                sqlParams[2] = new SqlParameter("@A3", A3);
                sqlParams[3] = new SqlParameter("@A4", A4);
                sqlParams[16] = new SqlParameter("@A5", A5);
                sqlParams[18] = new SqlParameter("@A6", A6);
                sqlParams[19] = new SqlParameter("@A7", A7);
                sqlParams[4] = new SqlParameter("@D1", D1);
                sqlParams[5] = new SqlParameter("@D2", D2);
                sqlParams[6] = new SqlParameter("@D3", D3);
                sqlParams[7] = new SqlParameter("@D4", D4);
                sqlParams[8] = new SqlParameter("@D5", D5);
                sqlParams[20] = new SqlParameter("@D6", D6);
                sqlParams[21] = new SqlParameter("@D7", D7);
                sqlParams[9] = new SqlParameter("@Empid", Empid);
                sqlParams[10] = new SqlParameter("@Companyid", Companyid);
                sqlParams[11] = new SqlParameter("@TotAmt", Totalamt);
                sqlParams[12] = new SqlParameter("@Ledger", Ledger);
                sqlParams[13] = new SqlParameter("@month", month);
                sqlParams[14] = new SqlParameter("@Year", year);
                sqlParams[15] = new SqlParameter("@Remarks", Remarks);
                sqlParams[17] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
                sqlParams[17].Direction = ParameterDirection.ReturnValue;
                sqlParams[22] = new SqlParameter("@A6Remarks", A6Remarks);
                sqlParams[23] = new SqlParameter("@A7Remarks", A7Remarks);
                sqlParams[24] = new SqlParameter("@D6Remarks", D6Remarks);
                sqlParams[25] = new SqlParameter("@D7Remarks", D7Remarks);
                sqlParams[26] = new SqlParameter("@Gratuity", gratuity);
                sqlParams[27] = new SqlParameter("@form", form);
                if (@Presentdays != "")
                    sqlParams[28] = new SqlParameter("@PresentDays", Presentdays);
                else
                    sqlParams[28] = new SqlParameter("@PresentDays", 0);
                if (NoOfDays != "")
                    sqlParams[29] = new SqlParameter("@NoOfDayInMonth", NoOfDays);
                else
                    sqlParams[29] = new SqlParameter("@NoOfDayInMonth", 0);
                sqlParams[30] = new SqlParameter("@Status", Status);
                sqlParams[31] = new SqlParameter("@SettlementDate", SettlementDate);
                sqlParams[32] = new SqlParameter("@AdjAmt", AdjAmt);
                sqlParams[33] = new SqlParameter("@EmpPen", EmpPen);
                sqlParams[34] = new SqlParameter("@ExitType", ExitType);
                sqlParams[35] = new SqlParameter("@A1Label", A1Label);
                sqlParams[36] = new SqlParameter("@A2Label", A2Label);
                sqlParams[37] = new SqlParameter("@A3Label", A3Label);
                sqlParams[38] = new SqlParameter("@A4Label", A4Label);
                sqlParams[39] = new SqlParameter("@A5Label", A5Label);
                sqlParams[40] = new SqlParameter("@D1Label", D1Label);
                sqlParams[41] = new SqlParameter("@D2Label", D2Label);
                sqlParams[42] = new SqlParameter("@D3Label", D3Label);
                sqlParams[43] = new SqlParameter("@D4Label", D4Label);
                sqlParams[44] = new SqlParameter("@D5Label", D5Label);
                sqlParams[45] = new SqlParameter("@GratuityRemarks", GratutityRemarks);
                sqlParams[46] = new SqlParameter("@D3Remarks", D3Remarks);
                sqlParams[47] = new SqlParameter("@LoanProof", LoanProof);
                sqlParams[48] = new SqlParameter("@LoanCheck", LoanCheck);
                sqlParams[49] = new SqlParameter("@AirticketProof", AirticketProof);
                sqlParams[50] = new SqlParameter("@A1Remarks", A1Remarks);
                sqlParams[51] = new SqlParameter("@A2Remarks", A2Remarks);
                sqlParams[52] = new SqlParameter("@A3Remarks", A3Remarks);
                sqlParams[53] = new SqlParameter("@A4Remarks", A4Remarks);
                sqlParams[54] = new SqlParameter("@A5Remarks", A5Remarks);
                sqlParams[55] = new SqlParameter("@D1Remarks", D1Remarks);
                sqlParams[56] = new SqlParameter("@D2Remarks", D2Remarks);
                sqlParams[57] = new SqlParameter("@D4Remarks", D4Remarks);
                sqlParams[58] = new SqlParameter("@D5Remarks", D5Remarks);
                sqlParams[59] = new SqlParameter("@D8Remarks", D8Remarks);
                sqlParams[60] = new SqlParameter("@D9Remarks", D9Remarks);
                sqlParams[61] = new SqlParameter("@D3LoanAmount", D3LoanAmount);
                sqlParams[62] = new SqlParameter("@GratuityRemarks1", GratuityRemarks1);
                sqlParams[63] = new SqlParameter("@filetype", ext);
                sqlParams[64] = new SqlParameter("@filepath", filepath);
                sqlParams[65] = new SqlParameter("@A8", A8);
                sqlParams[66] = new SqlParameter("@A8Remarks", A8Remarks);
                SQLDBUtil.ExecuteNonQuery("HMS_InsVacationSettlementRev4", sqlParams);
                int id = (int)sqlParams[17].Value;

                return id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void lnkuploadclick(object sender, EventArgs e)
        {
            //if (Session["FileUpload1"] != null && (!flpup.HasFile))
            //{
            //    flpup = (FileUpload)Session["FileUpload1"];
            //}
            //hdnuploadFileRowValue
            LinkButton lnk = sender as LinkButton;
            GridViewRow gvrow = (GridViewRow)lnk.NamingContainer;
            if (gvrow.RowIndex == 4)
            {
                hdnuploadFileRowValue.Value = "Airticket";
            }
            else if (gvrow.RowIndex == 9)
            {
                hdnuploadFileRowValue.Value = "Loan";
            }
            ModalPopupExtender3.Show();
        }
        protected void loanuploadFile(Object sender, EventArgs e)
        {
            string strhiddenval = ""; string strhdnrowvalue = hdnuploadFileRowValue.Value;

            if (flpup.HasFile)
            {   
                DateTime MyDate = DateTime.Now;
                string strfilename = strhdnrowvalue + MyDate.ToString("ddMMyyhhmmss");
                string extension = System.IO.Path.GetExtension(flpup.PostedFile.FileName).ToLower();
                string storePath = Server.MapPath("~") + "/" + "EmpVacation/";
                if (!Directory.Exists(storePath))
                    Directory.CreateDirectory(storePath);
                strhiddenval = strfilename + extension;
                flpup.PostedFile.SaveAs(storePath + "/" + strhiddenval);
                Session["FileUpload1"] = flpup;
                if(strhdnrowvalue.Equals("Loan"))
                { 
                    hdnupload.Value = strhiddenval.ToString();
                    Label lblFileName = (Label)(((sender as Button).NamingContainer.FindControl("GVVacation1") as GridView).Rows[9].FindControl("lblFileName"));
                    if (lblFileName != null)
                    {
                        lblFileName.Visible = true;
                        lblFileName.Text = flpup.FileName.ToString();
                    }
                }
                else if(strhdnrowvalue.Equals("Airticket"))
                { 
                    hdnAirticketFile.Value = strhiddenval.ToString();
                    Label lblFileName = (Label)(((sender as Button).NamingContainer.FindControl("GVVacation1") as GridView).Rows[4].FindControl("lblFileName"));
                    if (lblFileName != null)
                    {
                        lblFileName.Visible = true;
                        lblFileName.Text = flpup.FileName.ToString();
                    }
                }
                
            }
        }
       protected void DocNavigateUrl(Object sender, EventArgs e)
        {
            //hlnkView
            LinkButton hlnkView = (LinkButton)sender;
            GridViewRow gvrow = (GridViewRow)hlnkView.NamingContainer;
            string Proof = "";
            if (gvrow.RowIndex == 4)
            {
                Proof = hdnAirticketFileEdit.Value.ToString();
            }
            else if (gvrow.RowIndex == 9)
            {
                Proof = hdnLoanuploadEdit.Value.ToString();
            }
            string ReturnVal = "/EmpVacation/" + Proof;
            string fullURL = "window.open('" + ReturnVal + "', '_blank' );";

            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", fullURL, true);
        }
     
        protected void btnAccPostUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                //startdate();
                Double A1 = 0, A2 = 0, A3 = 0, A4 = 0, A5 = 0, A6 = 0, A7 = 0, D1 = 0, D2 = 0, D3 = 0, D4 = 0, D5 = 0, D6 = 0, D7 = 0, AdjAmt = 0, EmpPen = 0, GratuityAmt = 0, D3LoanAmount = 0;
                string Remarks = ""; string A6Remarks = ""; string A7Remarks = ""; string D6Remarks = ""; string D7Remarks = "", A1Label = "";string D3Remarks = ""; string LoanProof = ""; int LoanCheck = 0; string AirticketProof = "";
                string A2Label = "", A3Label = "", A4Label = "", A5Label = "", D1Label = "", D2Label = "", D3Label = "", D4Label = "", D5Label = "", GratutityRemarks = "";
                string A1Remarks = ""; string A2Remarks = ""; string A3Remarks = ""; string A4Remarks = ""; string A5Remarks = ""; string D1Remarks = ""; string D2Remarks = ""; string D4Remarks = ""; string D5Remarks = ""; string D8Remarks = ""; string D9Remarks = "";
                string status = "V"; string GratuityRemarks1 = "";
                string statusRemark = "Vacational Settlement";
                foreach (DataListItem li in dtlvacationEdit.Items)
                {
                    GridView gv = (GridView)li.FindControl("GVVacationedit");
                    foreach (GridViewRow row in gv.Rows)
                    {
                        if (row.RowIndex == 0)
                        {
                            TextBox txtA1 = (TextBox)row.FindControl("txtA1");
                            if (txtA1.Text != "")
                            {
                                A1 = Convert.ToDouble(txtA1.Text);
                                A1Label = row.Cells[4].Text.Trim();
                            }
                            else
                            {
                                A1 = 0;
                            }
                            TextBox txtDes = (TextBox)row.FindControl("txtA6");
                            A1Remarks = txtDes.Text.Trim();
                        }
                        if (row.RowIndex == 1)
                        {
                            TextBox txtA1 = (TextBox)row.FindControl("txtA1");
                            if (txtA1.Text != "")
                            {
                                A2 = Convert.ToDouble(txtA1.Text);
                                A2Label = row.Cells[4].Text.Trim();
                            }
                            else
                            {
                                A2 = 0;
                            }
                            TextBox txtDes = (TextBox)row.FindControl("txtA6");
                            A2Remarks = txtDes.Text.Trim();
                        }
                        if (row.RowIndex == 2)
                        {
                            TextBox txtA1 = (TextBox)row.FindControl("txtA1");
                            if (txtA1.Text != "")
                            {
                                A3 = Convert.ToDouble(txtA1.Text);
                                A3Label = row.Cells[4].Text.Trim();
                            }
                            else
                            {
                                A3 = 0;
                            }
                            TextBox txtDes = (TextBox)row.FindControl("txtA6");
                            A3Remarks = txtDes.Text.Trim();
                        }
                        if (row.RowIndex == 3)
                        {
                            TextBox txtA1 = (TextBox)row.FindControl("txtA1");
                            if (txtA1.Text != "")
                            {
                                A4 = Convert.ToDouble(txtA1.Text);
                                A4Label = row.Cells[4].Text.Trim();
                            }
                            else
                            {
                                A4 = 0;
                            }
                            TextBox txtDes = (TextBox)row.FindControl("txtA6");
                            A4Remarks = txtDes.Text.Trim();
                        }
                        if (row.RowIndex == 4)
                        {
                            TextBox txtA1 = (TextBox)row.FindControl("txtA1");
                            if (txtA1.Text != "")
                            {
                                A5 = Convert.ToDouble(txtA1.Text);
                                A5Label = row.Cells[4].Text.Trim();
                            }
                            else
                            {
                                A5 = 0;
                            }
                            TextBox txtDes = (TextBox)row.FindControl("txtA6");
                            A5Remarks = txtDes.Text.Trim();
                        }
                        if (row.RowIndex == 5)
                        {
                            TextBox txtA1 = (TextBox)row.FindControl("txtA1");
                            if (txtA1.Text != "")
                                A6 = Convert.ToDouble(txtA1.Text);
                            else
                            {
                                A6 = 0;
                            }
                            TextBox txtDes = (TextBox)row.FindControl("txtA6");
                            A6Remarks = txtDes.Text.Trim();
                        }
                        if (row.RowIndex == 6)
                        {
                            TextBox txtA1 = (TextBox)row.FindControl("txtA1");
                            if (txtA1.Text != "")
                                A7 = Convert.ToDouble(txtA1.Text);
                            else
                            {
                                A7 = 0;
                            }
                            TextBox txtDes = (TextBox)row.FindControl("txtA6");
                            A7Remarks = txtDes.Text.Trim();
                        }
                        if (chkFinal1.Checked)
                        {
                            if (row.RowIndex == 7)
                            {
                                statusRemark = "Final Settlement";
                                status = "F";
                                TextBox txtA1 = (TextBox)row.FindControl("txtA1");
                                if (txtA1.Text != "")
                                {
                                    GratuityAmt = Convert.ToDouble(txtA1.Text);
                                    GratutityRemarks = row.Cells[4].Text.Trim();
                                }
                                else
                                {
                                    GratuityAmt = 0;
                                }
                                TextBox txtDes = (TextBox)row.FindControl("txtA6");
                                GratuityRemarks1 = txtDes.Text.Trim();
                            }
                            if (row.RowIndex == 8)
                            {
                                TextBox txtA1 = (TextBox)row.FindControl("txtA1");
                                if (txtA1.Text != "")
                                {
                                    D1 = Convert.ToDouble(txtA1.Text);
                                    D1Label = row.Cells[4].Text.Trim();
                                }
                                else
                                {
                                    D1 = 0;
                                }
                                TextBox txtDes = (TextBox)row.FindControl("txtA6");
                                D1Remarks = txtDes.Text.Trim();
                            }
                            if (row.RowIndex == 9)
                            {
                                TextBox txtA1 = (TextBox)row.FindControl("txtA1");
                                if (txtA1.Text != "")
                                {
                                    D2 = Convert.ToDouble(txtA1.Text);
                                    D1Label = row.Cells[4].Text.Trim();
                                }
                                else
                                {
                                    D2 = 0;
                                }
                                TextBox txtDes = (TextBox)row.FindControl("txtA6");
                                D2Remarks = txtDes.Text.Trim();
                            }
                            if (row.RowIndex == 10)
                            {
                                TextBox txtA1 = (TextBox)row.FindControl("txtA1");
                                if (txtA1.Text != "")
                                {
                                    D3 = Convert.ToDouble(txtA1.Text);
                                    D1Label = row.Cells[4].Text.Trim();
                                }
                                else
                                {
                                    D3 = 0;
                                }
                                TextBox txtDes = (TextBox)row.FindControl("txtA6");
                                D3Remarks = txtDes.Text.Trim();
                            }
                            if (row.RowIndex == 11)
                            {
                                TextBox txtA1 = (TextBox)row.FindControl("txtA1");
                                if (txtA1.Text != "")
                                {
                                    D4 = Convert.ToDouble(txtA1.Text);
                                    D4Label = row.Cells[4].Text.Trim();
                                }
                                else
                                {
                                    D4 = 0;
                                }
                                TextBox txtDes = (TextBox)row.FindControl("txtA6");
                                D4Remarks = txtDes.Text.Trim();
                            }
                            if (row.RowIndex == 12)
                            {
                                TextBox txtA1 = (TextBox)row.FindControl("txtA1");
                                if (txtA1.Text != "")
                                {
                                    D5 = Convert.ToDouble(txtA1.Text);
                                    D5Label = row.Cells[4].Text.Trim();
                                }
                                else
                                {
                                    D5 = 0;
                                }
                                TextBox txtDes = (TextBox)row.FindControl("txtA6");
                                D5Remarks = txtDes.Text.Trim();
                            }
                            if (row.RowIndex == 13)
                            {
                                TextBox txtA1 = (TextBox)row.FindControl("txtA1");
                                if (txtA1.Text != "")
                                    D6 = Convert.ToDouble(txtA1.Text);
                                else
                                {
                                    D6 = 0;
                                }
                                TextBox txtDes = (TextBox)row.FindControl("txtA6");
                                D6Remarks = txtDes.Text.Trim();
                            }
                            if (row.RowIndex == 14)
                            {
                                TextBox txtA1 = (TextBox)row.FindControl("txtA1");
                                if (txtA1.Text != "")
                                    D7 = Convert.ToDouble(txtA1.Text);
                                else
                                {
                                    D7 = 0;
                                }
                                TextBox txtDes = (TextBox)row.FindControl("txtA6");
                                D7Remarks = txtDes.Text.Trim();
                            }
                            if (row.RowIndex == 15)
                            {
                                TextBox txtA1 = (TextBox)row.FindControl("txtA1");
                                if (txtA1.Text != "")
                                    AdjAmt = Convert.ToDouble(txtA1.Text);
                                else
                                {
                                    AdjAmt = 0;
                                }
                                TextBox txtDes = (TextBox)row.FindControl("txtA6");
                                D8Remarks = txtDes.Text.Trim();
                            }
                            if (row.RowIndex == 16)
                            {
                                TextBox txtA1 = (TextBox)row.FindControl("txtA1");
                                if (txtA1.Text != "")
                                    EmpPen = Convert.ToDouble(txtA1.Text);
                                else
                                    EmpPen = 0;
                                TextBox txtDes = (TextBox)row.FindControl("txtA6");
                                D9Remarks = txtDes.Text.Trim();
                            }
                            if (row.RowIndex == 17)
                            {
                                TextBox txtA1 = (TextBox)row.FindControl("txtA6");
                                Remarks = txtA1.Text.Trim();
                            }
                        }
                        else
                        {
                            if (row.RowIndex == 7)
                            {
                                TextBox txtA1 = (TextBox)row.FindControl("txtA1");
                                if (txtA1.Text != "")
                                {
                                    D1 = Convert.ToDouble(txtA1.Text);
                                    D1Label = row.Cells[4].Text.Trim();
                                }
                                else
                                {
                                    D1 = 0;
                                }
                                TextBox txtDes = (TextBox)row.FindControl("txtA6");
                                D1Remarks = txtDes.Text.Trim();
                            }
                            if (row.RowIndex == 8)
                            {
                                TextBox txtA1 = (TextBox)row.FindControl("txtA1");
                                if (txtA1.Text != "")
                                {
                                    D2 = Convert.ToDouble(txtA1.Text);
                                    D2Label = row.Cells[4].Text.Trim();
                                }
                                else
                                {
                                    D2 = 0;
                                }
                                TextBox txtDes = (TextBox)row.FindControl("txtA6");
                                D2Remarks = txtDes.Text.Trim();
                            }
                            if (row.RowIndex == 9)
                            {
                                TextBox txtA1 = (TextBox)row.FindControl("txtA1");
                                if (txtA1.Text != "")
                                {
                                    D3 = Convert.ToDouble(txtA1.Text);
                                    D3Label = row.Cells[4].Text.Trim();
                                }
                                else
                                {
                                    D3 = 0;
                                }
                                TextBox txtDes = (TextBox)row.FindControl("txtA6");
                                D3Remarks = txtDes.Text.Trim();
                            }
                            if (row.RowIndex == 10)
                            {
                                TextBox txtA1 = (TextBox)row.FindControl("txtA1");
                                if (txtA1.Text != "")
                                {
                                    D4 = Convert.ToDouble(txtA1.Text);
                                    D4Label = row.Cells[4].Text.Trim();
                                }
                                else
                                {
                                    D4 = 0;
                                }
                                TextBox txtDes = (TextBox)row.FindControl("txtA6");
                                D4Remarks = txtDes.Text.Trim();
                            }
                            if (row.RowIndex == 11)
                            {
                                TextBox txtA1 = (TextBox)row.FindControl("txtA1");
                                if (txtA1.Text != "")
                                {
                                    D5 = Convert.ToDouble(txtA1.Text);
                                    D5Label = row.Cells[4].Text.Trim();
                                }
                                else
                                {
                                    D5 = 0;
                                }
                                TextBox txtDes = (TextBox)row.FindControl("txtA6");
                                D5Remarks = txtDes.Text.Trim();
                            }
                            if (row.RowIndex == 12)
                            {
                                TextBox txtA1 = (TextBox)row.FindControl("txtA1");
                                if (txtA1.Text != "")
                                    D6 = Convert.ToDouble(txtA1.Text);
                                else
                                {
                                    D6 = 0;
                                }
                                TextBox txtDes = (TextBox)row.FindControl("txtA6");
                                D6Remarks = txtDes.Text.Trim();
                            }
                            if (row.RowIndex == 13)
                            {
                                TextBox txtA1 = (TextBox)row.FindControl("txtA1");
                                if (txtA1.Text != "")
                                    D7 = Convert.ToDouble(txtA1.Text);
                                else
                                {
                                    D7 = 0;
                                }
                                TextBox txtDes = (TextBox)row.FindControl("txtA6");
                                D7Remarks = txtDes.Text.Trim();
                            }
                            if (row.RowIndex == 14)
                            {
                                TextBox txtA1 = (TextBox)row.FindControl("txtA1");
                                if (txtA1.Text != "")
                                    AdjAmt = Convert.ToDouble(txtA1.Text);
                                else
                                {
                                    AdjAmt = 0;
                                }
                                TextBox txtDes = (TextBox)row.FindControl("txtA6");
                                D8Remarks = txtDes.Text.Trim();
                            }
                            if (row.RowIndex == 15)
                            {
                                TextBox txtA1 = (TextBox)row.FindControl("txtA1");
                                if (txtA1.Text != "")
                                    EmpPen = Convert.ToDouble(txtA1.Text);
                                else
                                    EmpPen = 0;
                                TextBox txtDes = (TextBox)row.FindControl("txtA6");
                                D9Remarks = txtDes.Text.Trim();
                            }
                            if (row.RowIndex == 16)
                            {
                                TextBox txtDes = (TextBox)row.FindControl("txtA6");
                                Remarks = txtDes.Text.Trim();
                            }
                        }
                    }
                }
                int month1 = 0, year1 = 0;
                if (txtdate.Text != "" || txtdate.Text != string.Empty)
                {
                    month1 = (DateTime.ParseExact(txtdate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).Month);
                    year1 = (DateTime.ParseExact(txtdate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).Year);
                }
                DateTime Setdt = DateTime.ParseExact(txtdate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                int id = AddVacationSettlementRev4(A1, A2, A3, A4, A5, A6, A7, D1, D2, D3, D4, D5, D6, D7, empid, Convert.ToInt32(Session["CompanyID"]), Remarks, 
                    Amt, statusRemark, month1, year1, A6Remarks, A7Remarks, D6Remarks, D7Remarks, GratuityAmt, status, lblPresendays.Text, lblNoOfDays.Text,
                    Convert.ToInt32(Session["vstatus"]), Setdt, AdjAmt, EmpPen,
                      0, A1Label, A2Label, A3Label, A4Label, A5Label, D1Label, D2Label, D3Label, D4Label, D5Label, GratutityRemarks, D3Remarks, LoanProof,
                      LoanCheck, AirticketProof, A1Remarks, A2Remarks, A3Remarks, A4Remarks, A5Remarks, D1Remarks, D2Remarks, D4Remarks, D5Remarks, D8Remarks, 
                      D9Remarks, D3LoanAmount, GratuityRemarks1,"","",0,"");
                if (id == 1)
                {
                    tblNew.Visible = false;
                    tblView.Visible = true;
                    tblEdit.Visible = false;
                    AlertMsg.MsgBox(Page, "Saved Successfully", AlertMsg.MessageType.Success);
                    BindGridView();
                }
                else if (id == 2)
                {
                    tblNew.Visible = false;
                    tblView.Visible = true;
                    tblEdit.Visible = false;
                    AlertMsg.MsgBox(Page, "Updated Successfully", AlertMsg.MessageType.Success);
                    BindGridView();
                }
                else
                    AlertMsg.MsgBox(Page, "Already Saved", AlertMsg.MessageType.Warning);
            }
            catch (Exception ex)
            {
                clsErrorLog.HMSEventLog(ex, "Vacational Settlement", "btnAccPostUpdate_Click", "003");
            }
        }
        protected void GVVacation_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Edt")
            {
                Session["vstatus"] = 2;
                GridViewRow gvr = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                int index = Convert.ToInt32(gvr.RowIndex);
                GridViewRow row = gvVacation.Rows[index];
                txtempid1.Value = row.Cells[0].Text;
                txtdate.Text = row.Cells[2].Text;
                EmployeBindEdit(objHrCommon);
                QtyChangedEdit(sender, e);
                if (txtempid1.Value != "" || txtempid1.Value != null)
                {
                    lnkViewAttendance.Visible = true;
                    lnkViewLeaveGrant.Visible = true;
                    lnksalryBrkup.Visible = true;
                    btnPrint.Visible = true;
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
                {
                    BindGridView();
                    AlertMsg.MsgBox(Page, "Account Posted Successfully", AlertMsg.MessageType.Success);
                }
                else
                    AlertMsg.MsgBox(Page, "Already Account Posted", AlertMsg.MessageType.Warning);
            }
            if (e.CommandName == "Del")
            {
                SqlParameter[] sqlParams1 = new SqlParameter[3];
                sqlParams1[0] = new SqlParameter("@id", e.CommandArgument);
                sqlParams1[1] = new SqlParameter("@Companyid", Convert.ToInt32(Session["CompanyID"]));
                sqlParams1[2] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
                sqlParams1[2].Direction = ParameterDirection.ReturnValue;
                SQLDBUtil.ExecuteNonQuery("sh_VacationalSettlementRemove", sqlParams1);
                int id1 = (int)sqlParams1[2].Value;
                if (id1 == 3)
                {
                    BindGridView();
                    AlertMsg.MsgBox(Page, "Record Deleted Successfully", AlertMsg.MessageType.Success);
                }
                else
                    AlertMsg.MsgBox(Page, "Already Account Posted", AlertMsg.MessageType.Warning);
            }
            if (e.CommandName == "Print")
            {
                GridViewRow gvRow = (GridViewRow)(((Control)e.CommandSource).NamingContainer);
                int Empid; string date;
                Empid = Convert.ToInt32(e.CommandArgument);
                Label lblcutdate = (Label)gvVacation.Rows[gvRow.RowIndex].FindControl("lblcutdate");
                date = lblcutdate.Text;// (Convert.ToDateTime(lblcutdate.Text)).ToString(); //"dd/MM/yyyy");
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
        public DataView BindVacationDetails(string Id)
        {
            DataTable dt = new DataTable();
            DataRow row = dt.NewRow();
            chkFinal.Checked = true;
            SqlParameter[] sqlParams = new SqlParameter[1];
            sqlParams[0] = new SqlParameter("@id", Id);
            DataSet ds = SQLDBUtil.ExecuteDataset("sh_BindVacationDetails", sqlParams);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                /*ButtonField btnMeds = new ButtonField();
                //Initalize the DataField value.
                btnMeds.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                btnMeds.CommandName = "Meds";
                btnMeds.ButtonType = ButtonType.Button;
                btnMeds.Text = "Meds";
                btnMeds.Visible = true;*/
                tblNew.Visible = false;
                tblView.Visible = false;
                tblEdit.Visible = true;
                dt.Columns.Add("Description", typeof(string));
                dt.Columns.Add("Amount", typeof(string));
                dt.Columns.Add("Details", typeof(string));
                //dt.Columns.Add("Det", typeof(string));
                dt.Columns.Add("Additionals", typeof(string));
                dt.Rows.Add("A1: Salary For the Current Month Attendance ", ds.Tables[0].Rows[0]["A1"].ToString(), ds.Tables[0].Rows[0]["A1Label"].ToString(), ds.Tables[0].Rows[0]["A1Remarks"].ToString());
                dt.Rows.Add("A2: Encashment of AL(EAL)", ds.Tables[0].Rows[0]["A2"].ToString(), ds.Tables[0].Rows[0]["A2Label"].ToString(), ds.Tables[0].Rows[0]["A2Remarks"].ToString());
                dt.Rows.Add("A3: HRA for LOP ", ds.Tables[0].Rows[0]["A3"].ToString(), ds.Tables[0].Rows[0]["A3Label"].ToString(), ds.Tables[0].Rows[0]["A3Remarks"].ToString());
                dt.Rows.Add("A4: Over-Time Amount", ds.Tables[0].Rows[0]["A4"].ToString(), ds.Tables[0].Rows[0]["A4Label"].ToString(), ds.Tables[0].Rows[0]["A4Remarks"].ToString());
                dt.Rows.Add("A5: Air Ticket Reimbursement", ds.Tables[0].Rows[0]["A5"].ToString(), ds.Tables[0].Rows[0]["A5Label"].ToString(), ds.Tables[0].Rows[0]["A5Remarks"].ToString());
                dt.Rows.Add("A6: Exit Entry Visa Reimbursement", ds.Tables[0].Rows[0]["A6"].ToString(),"", ds.Tables[0].Rows[0]["A6Remarks"].ToString());
                dt.Rows.Add("A7: ", ds.Tables[0].Rows[0]["A7"].ToString(), "", ds.Tables[0].Rows[0]["A7Remarks"].ToString());
                if (chkFinal1.Checked)
                {
                    dt.Rows.Add("Gratuity ", ds.Tables[0].Rows[0]["gratuity"].ToString(), ds.Tables[0].Rows[0]["GratuityRemarks"].ToString(), ds.Tables[0].Rows[0]["GratuityRemarks1"].ToString());
                }
                dt.Rows.Add("D1: Transport & Food ", ds.Tables[0].Rows[0]["D1"].ToString(), ds.Tables[0].Rows[0]["D1Label"].ToString(), ds.Tables[0].Rows[0]["D1Remarks"].ToString());
                dt.Rows.Add("D2: Other manual deductions ", "0", "", ds.Tables[0].Rows[0]["D2Remarks"].ToString());
                dt.Rows.Add("D3: OutStanding  Advances", ds.Tables[0].Rows[0]["D3"].ToString(), ds.Tables[0].Rows[0]["D3Label"].ToString(), ds.Tables[0].Rows[0]["D3Remarks"].ToString());
                dt.Rows.Add("D4: Absent Penalty ", ds.Tables[0].Rows[0]["D4"].ToString(), ds.Tables[0].Rows[0]["D4Label"].ToString(), ds.Tables[0].Rows[0]["D4Remarks"].ToString());
                dt.Rows.Add("D5: Expat ", 0, ds.Tables[0].Rows[0]["D5Label"].ToString(), ds.Tables[0].Rows[0]["D5Remarks"].ToString());
                dt.Rows.Add("D6:", ds.Tables[0].Rows[0]["D6"].ToString(), "", ds.Tables[0].Rows[0]["D6Remarks"].ToString());
                dt.Rows.Add("D7:", ds.Tables[0].Rows[0]["D7"].ToString(), "", ds.Tables[0].Rows[0]["D7Remarks"].ToString());
                dt.Rows.Add("AdjAmt:", ds.Tables[0].Rows[0]["AdjAmt"].ToString(),"", ds.Tables[0].Rows[0]["D8Remarks"].ToString());
                dt.Rows.Add("Emp Penalities:", ds.Tables[0].Rows[0]["EmpPen"].ToString(), "", ds.Tables[0].Rows[0]["D9Remarks"].ToString());
                dt.Rows.Add("Remarks", "","", ds.Tables[0].Rows[0]["Remarks"].ToString());
                lblPresendays.Text = ds.Tables[1].Rows[0][0].ToString();
                lblNoOfDays.Text = ds.Tables[2].Rows[0][0].ToString();
                Session["vstatus"] = 2;
                Button4.Visible = true;
                hdnLoanuploadEdit.Value = ds.Tables[0].Rows[0]["LoanProof"].ToString();
                hdnAirticketFileEdit.Value = ds.Tables[0].Rows[0]["AirticketProof"].ToString();
                hdnLoanCheck.Value = ds.Tables[0].Rows[0]["LoanCheck"].ToString();
                hdnD3LoanAmtEdit.Value = ds.Tables[0].Rows[0]["D3LoanAmount"].ToString();
            }
            DataView dv = dt.DefaultView;
            return dv;
        }
        protected void GVVacation_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //GridView GridView1 = (GridView)sender;
            //DataListItem dlItem = (DataListItem)GridView1.Parent;
            //DataListItemEventArgs dle = new DataListItemEventArgs(dlItem);
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int index = e.Row.RowIndex;
                if (index == 0 || index == 1 || index == 8 || index == 10)
                {
                    TextBox txtQtyOrder10 = (TextBox)e.Row.FindControl("txtA1");
                    txtQtyOrder10.ReadOnly = true;
                }
                if (index == 0)
                {
                    // added by pratap date: 10-jan-2017
                    LinkButton lnkALGross = (LinkButton)e.Row.FindControl("lnkALGross");
                    lnkALGross.Visible = false;
                }
                // added by pratap date: 10-jan-2017
                if (index == 3)
                {
                    LinkButton lnkOT = (LinkButton)e.Row.FindControl("lnkOT");
                    lnkOT.Visible = true;
                }
                if (index == 4)
                {
                    LinkButton lnkAirTicket = (LinkButton)e.Row.FindControl("lnkAirTicket");
                    lnkAirTicket.Visible = true;
                }
                if (index == 1)
                {
                    // added by pratap date: 10-jan-2017
                    LinkButton lnkALGross = (LinkButton)e.Row.FindControl("lnkALGross");
                    lnkALGross.Visible = true;
                    //if (EALGlobal == 0)
                    //{
                    //    LinkButton lnkbtn = (LinkButton)e.Row.FindControl("lnkEAL");
                    //    lnkbtn.Visible = true;
                    //    lnkbtn.ForeColor = System.Drawing.Color.Red;
                    //}
                    SqlParameter[] sqlParams = new SqlParameter[1];
                    sqlParams[0] = new SqlParameter("@empid", txtempid1.Value.Trim());
                    DataSet ds = SQLDBUtil.ExecuteDataset("sh_GetEncashmentAL", sqlParams);
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        // LinkButton lnkbtn1 = (LinkButton)e.Row.FindControl("lnkEAL");
                        int nCnt = 0;
                        nCnt = Convert.ToInt32(ds.Tables[0].Rows[0]["Cnt"].ToString());
                        //if (nCnt == 0)
                        //{
                        //    lnkbtn1.Text = "No Master Data";
                        //    lnkbtn1.ForeColor = System.Drawing.Color.Red;
                        //    ViewState["nCnt"] = nCnt.ToString();
                        //    updpnl.Update();
                        //}
                        //else
                        //{
                        //    lnkbtn1.Text = "Master Data Available";
                        //    lnkbtn.ForeColor = System.Drawing.Color.Green;
                        //    ViewState["nCnt"] = nCnt.ToString();
                        //    updpnl.Update();
                        //}
                    }
                }
                if (Request.QueryString.AllKeys.Contains("Empid"))
                {
                    if (!chkFinal.Checked)
                    {
                        if (index == 4)
                        {  
                            LinkButton lnkupload = (LinkButton)e.Row.FindControl("lnkupload");
                            lnkupload.Visible = true;
                        }
                        if (index == 5 || index == 6 || index == 10 || index == 13 || index == 14)
                        {
                            TextBox txtA6 = (TextBox)e.Row.FindControl("txtA6");
                            txtA6.Visible = true;
                        }
                        if (index == 10)
                        {
                            LinkButton lnkLoans = (LinkButton)e.Row.FindControl("lnkLoans");
                            lnkLoans.Visible = true;
                            TextBox txtA1 = (TextBox)e.Row.FindControl("txtA1");
                            CheckBox chkLoan = (CheckBox)e.Row.FindControl("chkLoanSelect");
                            LinkButton lnkupload = (LinkButton)e.Row.FindControl("lnkupload");
                            hdnLoanAmount.Value = txtA1.Text; //to  save the original amount
                            if (txtA1.Text == "0.00")
                            {
                                chkLoan.Enabled = false;
                                chkLoan.Visible = false;
                                lnkupload.Visible = false; 
                            }
                            else
                            {
                                chkLoan.Enabled = true;
                                chkLoan.Visible = true;
                                lnkupload.Visible = true;
                            }
                        }
                        if (index == 11)
                        {
                            LinkButton lnkAbsentPenalities = (LinkButton)e.Row.FindControl("lnkAbsentPenalities");
                            // lnkAbsentPenalities.Visible = true;
                        }
                        if (index == 12)
                        {
                            Button btnCals = (Button)e.Row.FindControl("btnCal");
                            btnCals.Visible = true;
                        }
                        if (index == 16)
                        {
                            LinkButton lnkEmpPen = (LinkButton)e.Row.FindControl("lnkEmpPen");
                            lnkEmpPen.Visible = true;
                        }
                    }
                    else if (chkFinal.Checked)
                    {
                        if (index == 5 || index == 6 || index == 14 || index == 15)
                        {
                            TextBox txtA6 = (TextBox)e.Row.FindControl("txtA6");
                            txtA6.Visible = true;
                        }
                        if (index == 8)
                        {
                            LinkButton lnkGratuity = (LinkButton)e.Row.FindControl("lnkGratuity");
                            lnkGratuity.Visible = true;
                        }
                        if (index == 11)
                        {
                            LinkButton lnkLoans = (LinkButton)e.Row.FindControl("lnkLoans");
                            lnkLoans.Visible = true;
                        }
                        if (index == 12)
                        {
                            LinkButton lnkAbsentPenalities = (LinkButton)e.Row.FindControl("lnkAbsentPenalities");
                            // lnkAbsentPenalities.Visible = true;
                        }
                        if (index == 13)
                        {
                            Button btnCals = (Button)e.Row.FindControl("btnCal");
                            btnCals.Visible = true;
                        }
                        if (index == 17)
                        {
                            LinkButton lnkEmpPen = (LinkButton)e.Row.FindControl("lnkEmpPen");
                            lnkEmpPen.Visible = true;
                        }
                    }
                }
                else
                {
                    //Edit Mode
                    if (!chkFinal1.Checked)
                    {
                        if (index == 4)
                        {
                            LinkButton hlnkView = (LinkButton)e.Row.FindControl("hlnkView");
                            if (hdnAirticketFileEdit.Value.Trim() != "")
                            {
                                hlnkView.Visible = true;
                            }
                            else
                            {
                                hlnkView.Visible = false;
                            }
                        }
                        if (index == 10)
                        {
                        LinkButton lnkLoans = (LinkButton)e.Row.FindControl("lnkLoans");
                        lnkLoans.Visible = true;
                        if (hdnD3LoanAmtEdit.Value != "0.00" || hdnD3LoanAmtEdit.Value != "0")
                        {
                            CheckBox chkLoanSelect = (CheckBox)e.Row.FindControl("chkLoanSelectEdit");
                            int x = 0, ilnchk = 0; string strlnchk = hdnLoanCheck.Value.Trim();
                            if (int.TryParse(strlnchk, out x))
                            {
                                ilnchk = Convert.ToInt16(strlnchk);
                            }
                            if (ilnchk == 1)
                            {
                                chkLoanSelect.Visible = true;
                                chkLoanSelect.Checked = true;
                                chkLoanSelect.Enabled = false;
                            }
                            else
                            {
                                chkLoanSelect.Checked = false;
                                chkLoanSelect.Visible = false;
                            }
                            LinkButton hlnkView = (LinkButton)e.Row.FindControl("hlnkView");
                            if (hdnLoanuploadEdit.Value.Trim() != "")
                            {
                                hlnkView.Visible = true;
                            }
                            else
                            {
                                hlnkView.Visible = false;
                            }
                        }
                        }
                        if (index == 11)
                        {
                            LinkButton lnkAbsentPenalities = (LinkButton)e.Row.FindControl("lnkAbsentPenalities");
                            // lnkAbsentPenalities.Visible = true;;
                        }
                        if (index == 12)
                        {
                            Button btnCals = (Button)e.Row.FindControl("btnCal");
                            btnCals.Visible = true;
                        }
                        if (index == 16)
                        {
                            LinkButton lnkEmpPen = (LinkButton)e.Row.FindControl("lnkEmpPen");
                            lnkEmpPen.Visible = true;
                        }
                    }
                    else if (chkFinal1.Checked)
                    {
                        if (index == 8)
                        {
                            LinkButton lnkGratuity = (LinkButton)e.Row.FindControl("lnkGratuity");
                            lnkGratuity.Visible = true;
                        }
                        if (index == 11)
                        {
                            LinkButton lnkLoans = (LinkButton)e.Row.FindControl("lnkLoans");
                            lnkLoans.Visible = true;
                        }
                        if (index == 12)
                        {
                            LinkButton lnkAbsentPenalities = (LinkButton)e.Row.FindControl("lnkAbsentPenalities");
                            // lnkAbsentPenalities.Visible = true;
                        }
                        if (index == 13)
                        {
                            Button btnCals = (Button)e.Row.FindControl("btnCal");
                            btnCals.Visible = true;
                        }
                        if (index == 17)
                        {
                            LinkButton lnkEmpPen = (LinkButton)e.Row.FindControl("lnkEmpPen");
                            lnkEmpPen.Visible = true;
                        }
                    }
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
                if (txtempid1.Value != string.Empty)
                {
                    //foreach (DataListItem li in dtlvacation.Items)
                    //{
                    //    GridView gv = (GridView)FindControl("GVVacation1");
                        foreach (GridViewRow row in GVVacation1.Rows)
                        {
                            if (!chkFinal.Checked)
                            {
                                if (row.RowIndex == 11)
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
                                        else
                                            AlertMsg.MsgBox(Page, "Employee does not have any outstanding documentation fees for deduction", AlertMsg.MessageType.Warning);
                                    }
                                    else
                                        AlertMsg.MsgBox(Page, "Employee does not have any outstanding documentation fees for deduction", AlertMsg.MessageType.Warning);
                                }
                            }
                            else
                            {
                                if (row.RowIndex == 12)
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
                                        else
                                            AlertMsg.MsgBox(Page, "Employee does not have any outstanding documentation fees for deduction", AlertMsg.MessageType.Warning);
                                    }
                                    else
                                        AlertMsg.MsgBox(Page, "Employee does not have any outstanding documentation fees for deduction", AlertMsg.MessageType.Warning);
                                }
                            }
                        }
                    //}
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
                //foreach (DataListItem li in dtlvacation.Items)
                //{
                //    GridView gv = (GridView)li.FindControl("GVVacation1");
                    foreach (GridViewRow row in GVVacation1.Rows)
                    {
                        TextBox txtExpat = (TextBox)row.FindControl("txtA1");
                        if (!chkFinal.Checked)
                        {
                            if (row.RowIndex == 11)
                            {
                                txtExpat.Text = TotExpatAmt.ToString();
                            }
                        }
                        else
                        {
                            if (row.RowIndex == 12)
                            {
                                txtExpat.Text = TotExpatAmt.ToString();
                            }
                        }
                    }
                    Label lblval = (Label)GVVacation1.FooterRow.FindControl("lblvalue");
                    lblval.Text = Convert.ToString(Convert.ToDouble(lblval.Text) - Convert.ToDouble(TotExpatAmt));
                //}
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
            if (txtempid1.Value != "" || txtempid1.Value != null)
            {
                Empid = Convert.ToInt32(txtempid1.Value);
            }
            string url = "ViewAttendance.aspx?Empid=" + Empid + "&Month=" + AttMonth + "&Year=" + AttYear;
            string fullURL = "window.open('" + url + "', '_blank' );";
            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", fullURL, true);
        }
        protected void lnkViewLeaveGrant_Click(object sender, EventArgs e)
        {
            int Empid = 0;
            if (txtempid1.Value != "" || txtempid1.Value != null)
            {
                Empid = Convert.ToInt32(txtempid1.Value);
            }
            string url = "HRLeaveApplications.aspx?key=1&Empid=" + Empid;
            string fullURL = "window.open('" + url + "', '_blank' );";
            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", fullURL, true);
        }
        protected void lnkatt_add_click(object sender, EventArgs e)
        {
            BindAdvAtt();
        }
        protected void btnClose_Click(object sender, EventArgs e)
        {
            dvadvatt.Visible = false;
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
                e.Row.Cells[3].ToolTip = "A1=" + (e.Row.DataItem as DataRowView)["A1"].ToString() +
                                          "\nA2=" + (e.Row.DataItem as DataRowView)["A2"].ToString() +
                                          "\nA3=" + (e.Row.DataItem as DataRowView)["A3"].ToString() +
                                          "\nA4=" + (e.Row.DataItem as DataRowView)["A4"].ToString() +
                                          "\nA5=" + (e.Row.DataItem as DataRowView)["A5"].ToString() +
                                          "\nA6=" + (e.Row.DataItem as DataRowView)["A6"].ToString() +
                                          "\nA7=" + (e.Row.DataItem as DataRowView)["A7"].ToString() +
                                          "\ngratuity=" + (e.Row.DataItem as DataRowView)["gratuity"].ToString();
                e.Row.Cells[4].ToolTip = "D1=" + (e.Row.DataItem as DataRowView)["D1"].ToString() +
                                          "\nD2=" + (e.Row.DataItem as DataRowView)["D2"].ToString() +
                                          "\nD3=" + (e.Row.DataItem as DataRowView)["D3"].ToString() +
                                          "\nD4=" + (e.Row.DataItem as DataRowView)["D4"].ToString() +
                                          "\nD5=" + (e.Row.DataItem as DataRowView)["D5"].ToString() +
                                          "\nD6=" + (e.Row.DataItem as DataRowView)["D6"].ToString() +
                                          "\nD7=" + (e.Row.DataItem as DataRowView)["D7"].ToString() +
                                           "\nAdjAmt=" + (e.Row.DataItem as DataRowView)["AdjAmt"].ToString() +
                                            "\nEmpPen=" + (e.Row.DataItem as DataRowView)["EmpPenAmt"].ToString();
                if (Request.QueryString.AllKeys.Contains("Empid"))
                {
                    e.Row.Cells[6].Visible = false;
                    e.Row.Cells[7].Visible = false;
                }
                else
                {
                    e.Row.Cells[6].Visible = true;
                    e.Row.Cells[7].Visible = true;
                }
            }
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
                        sqlParam[3] = new SqlParameter("@MarkedBy", Convert.ToInt32(Session["UserId"]));
                        SQLDBUtil.ExecuteNonQuery("HMS_InsAdvAttendacne", sqlParam);
                    }
                }
                AlertMsg.MsgBox(Page, "Saved", AlertMsg.MessageType.Success);
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
                int EmpID = Convert.ToInt32(txtempid1.Value.Trim());
                lblmonth.Text = Month + "-" + Year;
                tblAtt.Rows.Clear();
                DataSet ds = objatt.GetAttendanceByMonth_Cursor(Convert.ToInt32(Month), Convert.ToInt32(Year), 0, 0, EmpID, string.Empty, null);
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
                if (txtempid1.Value != "" || txtempid1.Value != string.Empty)
                {
                    Empid = Convert.ToInt32(txtempid1.Value);
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
        //protected void lnkEAL_Click(object sender, EventArgs e)
        //{
        //    int Empid;
        //    if (txtempid1.Value != "" || txtempid1.Value != string.Empty)
        //    {
        //        Empid = Convert.ToInt32(txtempid1.Value);
        //    }
        //    else
        //        Empid = 0;
        //    string url = "";
        //    if (Convert.ToInt32(ViewState["nCnt"].ToString()) == 0)
        //        url = "Encashment_AL.aspx?id=1&Empid=" + Empid;
        //    else
        //        url = "Encashment_AL.aspx?id=2&Empid=" + Empid;
        //    string fullURL = "window.open('" + url + "', '_blank' );";
        //    ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", fullURL, true);
        //}
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
                try
                {
                    if (Request.QueryString.Count > 0)
                    {
                        if (Request.QueryString.AllKeys.Contains("LID"))
                        {
                            LID = Convert.ToInt32(Request.QueryString["LID"]);
                        }
                    }
                    else
                        LID = 0;
                    if (!chkFinal.Checked)
                    {
                        SqlParameter[] param = new SqlParameter[2];
                        param[0] = new SqlParameter("@EmpID", empid);
                        param[1] = new SqlParameter("@LID", LID);
                        DataSet ds = SQLDBUtil.ExecuteDataset("sh_GetVacationSettlementEmpDate", param);
                        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                        {
                            txtdate.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["LastDate"].ToString()).ToString("dd/MM/yyyy");
                        }
                        else
                        {
                            AlertMsg.MsgBox(Page, "NO future granted leave to consider for Settlement Make sure the employee has approved leave to process", AlertMsg.MessageType.Warning);
                            return;
                        }
                    }
                }
                catch (Exception)
                {
                    throw;
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
                int i;
                try
                {
                    SqlParameter[] p = new SqlParameter[4];
                    p[0] = new SqlParameter("@Year", Year);
                    p[1] = new SqlParameter("@Empid", empid);
                    if (chkFinal.Checked)
                        p[2] = new SqlParameter("@Form", "F");
                    else
                        p[2] = new SqlParameter("@Form", "V");
                    p[3] = new SqlParameter("@date", enddate);
                    i = Convert.ToInt32(SqlHelper.ExecuteScalar("HMS_CountVacationPost", p));
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                if (i != 0)
                {
                    btnAccPost.Visible = false;
                    fudDocument1.Visible= false;
                    AlertMsg.MsgBox(Page, "Already Posted To Accounts");
                    return;
                }
                else
                {
                    DataSet ds = new DataSet();
                    if (!chkFinal.Checked)
                    {
                        if (Request.QueryString.Count > 0)
                        {
                            if (Request.QueryString.AllKeys.Contains("LID"))
                            {
                                LID = Convert.ToInt32(Request.QueryString["LID"]);
                            }
                        }
                        else
                            LID = 0;
                        ds = AttendanceDAC.T_HMS_GetVacation(objHrCommon, empid, ename, 0, 0, month, year, stdt, enddate, LID);
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
                        sqlParams[5] = new SqlParameter("@Ename", DBNull.Value);
                        sqlParams[6] = new SqlParameter("@WSid", 0);
                        sqlParams[7] = new SqlParameter("@Deptid", 0);
                        sqlParams[8] = new SqlParameter("@month", month);
                        sqlParams[9] = new SqlParameter("@year", year);
                        sqlParams[10] = new SqlParameter("@StDate", stdt);
                        sqlParams[11] = new SqlParameter("@EndDate", enddate);
                        sqlParams[12] = new SqlParameter("@leavetype", DBNull.Value);
                        ds = SQLDBUtil.ExecuteDataset("T_HMS_GetEmployeeVacation_FinalSettlement_new", sqlParams);
                    }
                    int tablcnt = ds.Tables.Count;
                    if (ds != null && ds.Tables.Count != 0 && ds.Tables[tablcnt - 1].Rows.Count > 0)
                    {
                        ViewState["DataSet"] = ds.Tables[tablcnt - 1];
                        dtlvacation.DataSource = ds.Tables[tablcnt - 1];
                        dtlvacation.DataBind();
                        GVVacation1.DataSource = BindTransdetails(ds.Tables[tablcnt - 1].Rows[0]["Empid"].ToString());
                        GVVacation1.DataBind();
                        dtlvacation.Visible = true;
                        btnAccPost.Visible = true;
                        fudDocument1.Visible = true;
                    }
                    else
                    {
                        dtlvacation.DataSource = null;
                        dtlvacation.DataBind();
                        dtlvacation.Visible = false;
                        GVVacation1.DataSource = null;
                        GVVacation1.DataBind();
                        AlertMsg.MsgBox(Page, "No Record Found", AlertMsg.MessageType.Warning);
                        btnAccPost.Visible = false;
                        fudDocument1.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                clsErrorLog.HMSEventLog(ex, "Vactional Settlement", "EmployeBind", "007");
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
                    fudDocument1.Visible = false;
                    AlertMsg.MsgBox(Page, "Already Saved", AlertMsg.MessageType.Warning);
                    return;
                }
                else
                {
                    DataSet ds = new DataSet();
                    if (!chkFinal1.Checked)
                    {
                        ds = AttendanceDAC.T_HMS_GetVacation(objHrCommon, empid, ename, 0, 0, month, year, stdt, enddate, 0);
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
                        fudDocument1.Visible = true;
                    }
                    else
                    {
                        dtlvacationEdit.DataSource = null;
                        dtlvacationEdit.DataBind();
                        AlertMsg.MsgBox(Page, "No Record Found", AlertMsg.MessageType.Warning);
                        btnAccPost.Visible = false;
                        fudDocument1.Visible = false;
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
            //month = Dmonth.Month;
            Year = (DateTime.ParseExact(txtdate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).Year);
            if (Month == 1)
            {
                Month = 12;
                Year = Year - 1;
            }
            else
                Month = Month - 1;
            // string st = ddlMonth.SelectedItem.Value + "/" + startdate.Tables[0].Rows[0][0].ToString() + "/" + ddlYear.SelectedItem.Value;
            string st = Month + "/" + startdate.Tables[0].Rows[0][0].ToString() + "/" + Year;
            stdt = CodeUtil.ConvertToDate(st, CodeUtil.DateFormat.MonthDayYear);
        }
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
        public DataView BindTransdetails(string TransId)
        {
            DateTime dtt = Convert.ToDateTime(lblDate.Text);
            txtdate.Text = (dtt.ToString("dd/MM/yyyy")).ToString();
            int Month = 0;
            Month = Convert.ToInt32(dtt.Month);
            int Year = Convert.ToInt32(dtt.Year);
            if (Month == 1)
            {
                Month = 12;
                Year = Year - 1;
            }
            else
                Month = Month - 1;
            string st = Month + "/" + "21" + "/" + Year;
            DateTime dts = CODEUtility.ConvertToDate(st, DateFormat.MonthDayYear);
            DateTime ed = dts.AddMonths(1).AddDays(-1);
            Leaves.HR_OTCalculation(Convert.ToInt32(txtempid1.Value.Trim()), dts, ed, 1);
            DataTable dt = new DataTable();
            DataRow row = dt.NewRow();
            //DataColumn column = new DataColumn();
            dt.Columns.Add("Description", typeof(string));
            dt.Columns.Add("Amount", typeof(string));
            dt.Columns.Add("Details", typeof(string));
            dt.Columns.Add("Det", typeof(string));
            dt.Columns.Add("Additionals", typeof(string));

            if (Request.QueryString.Count > 0)
            {
                if (Request.QueryString.AllKeys.Contains("LID"))
                {
                    LID = Convert.ToInt32(Request.QueryString["LID"]);
                }
            }
            else
                LID = 0;
            SqlParameter[] sqlParams = new SqlParameter[8];
            sqlParams[0] = new SqlParameter("@empid", txtempid1.Value.Trim());
            sqlParams[1] = new SqlParameter("@settlementdate", DateTime.ParseExact(txtdate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture));
            if (!chkFinal.Checked)
                sqlParams[2] = new SqlParameter("@form", 'V');
            else
                sqlParams[2] = new SqlParameter("@form", 'F');
            int PassPayableDays = 0;
            decimal AlDays = 0;
            if ((txtManualPayableDays.Text.Trim() != "0") && (txtManualPayableDays.Text.Trim() != ""))
            {
                PassPayableDays = Convert.ToInt32(txtManualPayableDays.Text);
            }
            if ((txtAlDays.Text.Trim() != "0") && (txtAlDays.Text.Trim() != ""))
            {
                AlDays = Convert.ToDecimal(txtAlDays.Text);
            }
            if (PassPayableDays == 0)
                sqlParams[3] = new SqlParameter("@PassPayableDays", SqlDbType.Int);
            else
                sqlParams[3] = new SqlParameter("@PassPayableDays", PassPayableDays);
            sqlParams[4] = new SqlParameter("@LID", LID);
            if (chkEnch.Checked == true)
                sqlParams[5] = new SqlParameter("@Ench", "1");
            else
                sqlParams[5] = new SqlParameter("@Ench", "0");
            if (chkHRALOP.Checked == true)
                sqlParams[6] = new SqlParameter("@HRALOP", "1");
            else
                sqlParams[6] = new SqlParameter("@HRALOP", "0");
            if (AlDays == 0)
                sqlParams[7] = new SqlParameter("@AlDays", SqlDbType.Decimal);
            else
                sqlParams[7] = new SqlParameter("@AlDays", AlDays);
            DataSet ds = SQLDBUtil.ExecuteDataset("sh_GetVacationSettlementRev4ShowData", sqlParams);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                int tcount = ds.Tables.Count;
                int rw = 0;
                if (tcount == 4)
                    rw = 0;
                else
                    rw = tcount - 4;
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
                    dt.Rows.Add("A1: Salary For the Current Month Attendance ", ds.Tables[rw].Rows[0]["A1"].ToString(), ds.Tables[rw].Rows[0]["A1Label"].ToString());
                    dt.Rows.Add("A2: Encashment of AL(EAL)", ds.Tables[rw].Rows[0]["A2"].ToString(), ds.Tables[rw].Rows[0]["A2Label"].ToString());
                    dt.Rows.Add("A3: HRA for LOP", ds.Tables[rw].Rows[0]["A3"].ToString(), ds.Tables[rw].Rows[0]["A3Label"].ToString());
                    //dt.Rows.Add("A4:Air Ticket Reimbursement", "0");
                    dt.Rows.Add("A4: Over-Time Amount", ds.Tables[rw].Rows[0]["A4"].ToString(), ds.Tables[rw].Rows[0]["A4Label"].ToString());
                    dt.Rows.Add("A5: Air Ticket Reimbursement", ds.Tables[rw].Rows[0]["A5"].ToString(), ds.Tables[rw].Rows[0]["A5label"].ToString());
                    dt.Rows.Add("A6: Exit Entry Visa Reimbursement", 0);
                    dt.Rows.Add("A7: AL Based On Previous Basic", 0);
                    dt.Rows.Add("A8:", 0);
                    if (chkFinal.Checked)
                    {
                        decimal gamt = 0;
                        int accpost = 1;
                        HRCommon objcommon = new HRCommon();
                        objcommon.PageSize = GrtPaging.ShowRows;
                        objcommon.CurrentPage = GrtPaging.CurrentPage;
                        DateTime edt = DateTime.ParseExact(txtdate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                        DataSet dss = AttendanceDAC.GetGratuityDetails_Final(objcommon, CompanyID, 0, txtempid1.Value.Trim(), accpost, Convert.ToInt32(ddlEmpDeAct.SelectedValue), edt);
                        string gratdet = "";
                        if (dss != null && dss.Tables.Count > 0)
                        {
                            if (dss.Tables[0].Rows.Count > 0)
                            {
                                gamt = Convert.ToDecimal(dss.Tables[0].Rows[0]["gamt"].ToString());
                                gratdet = dss.Tables[0].Rows[0]["GratuityRemarks"].ToString();
                            }
                        }
                        dt.Rows.Add("Gratuity", gamt, gratdet);
                    }
                    dt.Rows.Add("D1: Transport & Food ", ds.Tables[rw].Rows[0]["D1"].ToString(), ds.Tables[rw].Rows[0]["D1Label"].ToString());
                    dt.Rows.Add("D2: Other manual deductions ", "0", "");
                    dt.Rows.Add("D3: OutStanding  Advances", ds.Tables[rw].Rows[0]["D3"].ToString(), ds.Tables[rw].Rows[0]["D3Label"].ToString());
                    dt.Rows.Add("D4: Absent Penalty ", ds.Tables[rw].Rows[0]["D4"].ToString(), ds.Tables[rw].Rows[0]["D4Label"].ToString());
                    dt.Rows.Add("D5: Expat ", 0, exptdetails, btnMeds);
                    dt.Rows.Add("D6:", 0);
                    dt.Rows.Add("D7:", 0);
                    dt.Rows.Add("D8: AdjAmt:", ds.Tables[rw].Rows[0]["AdjAmt"].ToString());
                    dt.Rows.Add("D9: Emp Penalities:", ds.Tables[rw].Rows[0]["EmpPenAmt"].ToString());
                    dt.Rows.Add("Remarks", "");
                    lblPresendays.Text = ds.Tables[rw + 1].Rows[0][0].ToString();
                    lblNoOfDays.Text = ds.Tables[rw + 2].Rows[0][0].ToString();
                    Session["vstatus"] = 1;
                }
                else
                {
                    dt.Rows.Add("A1: Salary For the Current Month Attendance ", "0");
                    dt.Rows.Add("A2: Encashment of AL(EAL)", ds.Tables[rw].Rows[0]["A2"].ToString(), ds.Tables[rw].Rows[0]["A2Label"].ToString());
                    dt.Rows.Add("A3: HRA for LOP", ds.Tables[rw].Rows[0]["A3"].ToString(), ds.Tables[rw].Rows[0]["A3Label"].ToString());
                    dt.Rows.Add("A4: Over-Time Amount", "0");
                    dt.Rows.Add("A5: Air Ticket Reimbursement", ds.Tables[rw].Rows[0]["A5"].ToString(), ds.Tables[rw].Rows[0]["A5label"].ToString());
                    dt.Rows.Add("A6: Exit Entry Visa Reimbursement", 0);
                    dt.Rows.Add("A7: AL On Previous Basic", 0);
                    dt.Rows.Add("A8:", 0);
                    dt.Rows.Add("D1: (Transport & Food) ", "0");
                    dt.Rows.Add("D2: Other manual deductions ", "0", "");
                    dt.Rows.Add("D3: OutStanding  Advances", "0");
                    dt.Rows.Add("D4: Absent Penalty ", "0");
                    dt.Rows.Add("D5: Expat ", 0);
                    dt.Rows.Add("D6:", 0);
                    dt.Rows.Add("D7:", 0);
                    dt.Rows.Add("D8: AdjAmt:", 0);
                    dt.Rows.Add("D9: Emp Penalities:", 0);
                    dt.Rows.Add("Remarks", "");
                    lblPresendays.Text = ds.Tables[rw + 1].Rows[0][0].ToString();
                    lblNoOfDays.Text = ds.Tables[rw + 2].Rows[0][0].ToString();
                    Session["vstatus"] = 1;
                }
            }
            else
                AlertMsg.MsgBox(Page, "No Records Found", AlertMsg.MessageType.Warning);
            DataView dv = dt.DefaultView;
            return dv;
        }
        protected void loanAmountChange(object sender, EventArgs e)
        {
            CheckBox chkLoan = (CheckBox)sender;
            GridViewRow row = (GridViewRow)chkLoan.NamingContainer;
            TextBox loanAmt = (TextBox)row.FindControl("txtA1");
            Label lblval = (Label)GVVacation1.FooterRow.FindControl("lblvalue");
            string[] strValue = lblval.Text.ToString().Split(' ');
            decimal Total = 0;
            if(strValue.Count() > 0 && chkLoan.Checked == true)
            {  
                Total = Convert.ToDecimal(strValue[0]) + Convert.ToDecimal(loanAmt.Text);
                loanAmt.Text = "0.00";
            }
            else if(chkLoan.Checked == false)
            {
                loanAmt.Text = hdnLoanAmount.Value.ToString();
                Total = Convert.ToDecimal(strValue[0]) - Convert.ToDecimal(loanAmt.Text);
            }
            lblval.Text = (Math.Round(Total)).ToString("#,#", CultureInfo.InvariantCulture) + " SAR";
            Amt = Convert.ToDouble(Total);
        }
        protected void QtyChanged(object sender, EventArgs e)
        {
            decimal resultD1 = 0, resultD2 = 0;
            decimal totalA = 0, totalD = 0, Total = 0;
            //foreach (DataListItem li in dtlvacation.Items)
            //{
            //    GridView gv = (GridView)li.FindControl("GVVacation1");
                foreach (GridViewRow row in GVVacation1.Rows)
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
                    chkFinal1.Checked = false;
                    if (!chkFinal.Checked)
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
                            if(row.RowIndex == 10)
                            {
                                 CheckBox chkLoan = (CheckBox)row.FindControl("chkLoanSelect");
                                if(chkLoan.Checked.Equals("true"))
                                    totalD += 0;
                                else if (decimal.TryParse(tb.Text.Trim(), out sum))
                                    totalD += sum;
                            }
                            else if (decimal.TryParse(tb.Text.Trim(), out sum))
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
                            if (row.RowIndex == 11)
                            {
                                CheckBox chkLoan = (CheckBox)row.FindControl("chkLoanSelect");
                                if (chkLoan.Checked.Equals("true"))
                                    totalD += 0;
                                else if (decimal.TryParse(tb.Text.Trim(), out sum))
                                    totalD += sum;
                            }
                            else if (decimal.TryParse(tb.Text.Trim(), out sum))
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
                    if (Total < 0)
                        Total = Convert.ToDecimal(Total);
                    Label lblval = (Label)GVVacation1.FooterRow.FindControl("lblvalue");
                    lblval.Text = (Math.Round(Total)).ToString("#,#", CultureInfo.InvariantCulture) + " SAR";
                    Amt = Convert.ToDouble(Total);
                }
            //}
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
                    chkFinal.Checked = false;
                    if (!chkFinal1.Checked)
                    {
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
                    }
                    else
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
                    if (Total < 0)
                        Total = Convert.ToDecimal(Total);
                    Label lblval = (Label)gv.FooterRow.FindControl("lblvalue");
                    lblval.Text = Math.Round(Total).ToString("#,#", CultureInfo.InvariantCulture) + " SAR";
                    Amt = Convert.ToDouble(Total);
                }
            }
            //BindTransdetails("0");
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
            SqlParameter[] sqlParams = new SqlParameter[6];
            sqlParams[4] = new SqlParameter("@Empid", empid);
            sqlParams[0] = new SqlParameter("@CurrentPage", objHrCommon.CurrentPage);
            sqlParams[1] = new SqlParameter("@PageSize", objHrCommon.PageSize);
            sqlParams[2] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
            sqlParams[2].Direction = ParameterDirection.ReturnValue;
            sqlParams[3] = new SqlParameter("@NoofRecords", System.Data.SqlDbType.Int);
            sqlParams[3].Direction = ParameterDirection.Output;
            if (chkFinal1.Checked)
                sqlParams[5] = new SqlParameter("@Status", "F");
            else
                sqlParams[5] = new SqlParameter("@Status", "V");
            DataSet ds = SQLDBUtil.ExecuteDataset("sh_GETVacationPendingDetails", sqlParams);
            objHrCommon.NoofRecords = (int)sqlParams[3].Value;
            objHrCommon.TotalPages = (int)sqlParams[2].Value;
            gvVacation.DataSource = ds;
            gvVacation.DataBind();
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
        public DataSet FillAttandanceType()
        {
            return (DataSet)ViewState["AttTypes"];
        }
        private void BindAtttypes()
        {
            DataSet ds = AttendanceDAC.GetAttendanceType();
            ViewState["AttTypes"] = ds;
        }
        #endregion Methods
        protected void GVVacationUpdate_RowDataBound(object sender, GridViewRowEventArgs e)
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
                if (index == 5 || index == 6 ||  index == 12 || index == 13)
                {
                    TextBox txtA6 = (TextBox)e.Row.FindControl("txtA6");
                    txtA6.Visible = true;
                }
            }
        }
        protected void lnksalryBrkup_Click(object sender, EventArgs e)
        {
            int Empid = 0;
            if (txtempid1.Value != "" || txtempid1.Value != null)
            {
                Empid = Convert.ToInt32(txtempid1.Value);
            }
            string url = "EmpSalhikesV2.aspx?Empid=" + Empid;
            string fullURL = "window.open('" + url + "', '_blank' );";
            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", fullURL, true);
        }
        protected void btnSearch_Click1(object sender, EventArgs e)
        {
            try
            {
                AdvancedLeaveAppOthPaging.CurrentPage = 1;
                int empid = 0;
                if (txtempid1.Value != "" || txtempid1.Value != string.Empty)
                {
                    empid = Convert.ToInt32(txtempid1.Value);
                    EMPIDPram = empid;
                }
                else
                    empid = 0;
                if (Request.QueryString.Count > 0)
                {
                    if (Request.QueryString.AllKeys.Contains("LID"))
                    {
                        LID = Convert.ToInt32(Request.QueryString["LID"]);
                    }
                }
                else
                    LID = 0;
                try
                {
                    lblDate.Text = "";
                    SqlParameter[] param1 = new SqlParameter[2];
                    param1[0] = new SqlParameter("@EmpID", empid);
                    param1[1] = new SqlParameter("@LID", LID);
                    DataSet ds1 = SQLDBUtil.ExecuteDataset("sh_GetVacationSettlementEmpDate", param1);
                    if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
                    {
                        lblDate.Text = Convert.ToDateTime(ds1.Tables[0].Rows[0]["LastDate"].ToString()).ToString("dd/MMM/yyyy");
                        DateTime now = Convert.ToDateTime(lblDate.Text);
                        var startDate = new DateTime(now.Year, now.Month, 1);
                        var EndDate = Convert.ToDateTime(lblDate.Text);
                        txtManualPayableDays.Text = (((EndDate - startDate).TotalDays) + 1).ToString();
                    }
                }
                catch (Exception)
                {
                    throw;
                }
                if (txtempid1.Value != "" || txtempid1.Value != null)
                {
                    lnkViewAttendance.Visible = true;
                    lnkViewLeaveGrant.Visible = true;
                    lnksalryBrkup.Visible = true;
                    btnPrint.Visible = true;
                }
                if (Request.QueryString.Count > 0)
                {
                    if (Request.QueryString.AllKeys.Contains("LID"))
                    {
                        LID = Convert.ToInt32(Request.QueryString["LID"]);
                    }
                    else
                        LID = 0;
                    if (!chkFinal.Checked)
                    {
                        SqlParameter[] param = new SqlParameter[3];
                        param[0] = new SqlParameter("@EmpID", empid);
                        param[1] = new SqlParameter("@CompanyID", CompanyID);
                        param[2] = new SqlParameter("@LID", LID);
                        DataSet ds = SQLDBUtil.ExecuteDataset("sh_VacationSettlementEmpInfo", param);
                        if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0)
                        {
                            gvGranted.DataSource = ds.Tables[0];
                            gvGranted.DataBind();
                            btnSearch.Visible = true;
                            chkEnch.Visible = false;
                            chkHRALOP.Visible = true;
                            tblNew.Visible = true;
                        }
                        else
                        {
                            gvGranted.EmptyDataText = "No Records Found";
                            gvGranted.DataBind();
                            AlertMsg.MsgBox(Page, "NO Pending Records", AlertMsg.MessageType.Warning);
                        }
                    }
                }
                btnMonthfirst_Click(sender, e);
                BindGridView();
            }
            catch (Exception)
            {
                throw;
            }
        }
        // added by pratap date: 09-jan-2017
        protected void btnSearchLeaveDet_Click(object sender, EventArgs e)
        {
            //SqlParameter[] sqlParams22 = new SqlParameter[1];
            //sqlParams22[0] = new SqlParameter("@empid", txtempid1.Value.Trim());
            //DataSet ds22 = SQLDBUtil.ExecuteDataset("sh_previousmonthsalarystatus", sqlParams22);
            //if (ds22 != null && ds22.Tables.Count > 0 && ds22.Tables[0].Rows.Count > 0 && ds22.Tables[0].Rows[0][0].ToString() == "1")
            //{
            //    SqlParameter[] sqlParams2 = new SqlParameter[1];
            //    sqlParams2[0] = new SqlParameter("@empid", txtempid1.Value.Trim());
            //    DataSet ds2 = SQLDBUtil.ExecuteDataset("sh_openingbalancecheckingleaveaccount", sqlParams2);
            //    if (ds2 != null && ds2.Tables.Count > 0 && ds2.Tables[0].Rows.Count > 0)
            //    {
            //        SqlParameter[] SP = new SqlParameter[1];
            //        SP[0] = new SqlParameter("@empid", Convert.ToInt32(txtempid1.Value));
            //        DataSet dss1 = SQLDBUtil.ExecuteDataset("sh_EmpExistsinSederFileds", SP);
            //        if (dss1.Tables[0].Rows.Count > 0)
            //        {
            try
            {
                try
                {
                    int empid = 0;
                    if (txtempid1.Value != "" || txtempid1.Value != string.Empty)
                    {
                        empid = Convert.ToInt32(txtempid1.Value);
                        EMPIDPram = empid;
                    }
                    else
                        empid = 0;
                    try
                    {
                        lblDate.Text = "";
                        if (Request.QueryString.Count > 0)
                        {
                            if (Request.QueryString.AllKeys.Contains("LID"))
                            {
                                LID = Convert.ToInt32(Request.QueryString["LID"]);
                            }
                        }
                        else
                            LID = 0;
                        SqlParameter[] param1 = new SqlParameter[2];
                        param1[0] = new SqlParameter("@EmpID", empid);
                        param1[1] = new SqlParameter("@LID", LID);
                        DataSet ds1 = SQLDBUtil.ExecuteDataset("sh_GetVacationSettlementEmpDate", param1);
                        if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
                        {
                            lblDate.Text = Convert.ToDateTime(ds1.Tables[0].Rows[0]["LastDate"].ToString()).ToString("dd/MMM/yyyy");
                            DateTime now = Convert.ToDateTime(lblDate.Text);
                            var startDate = new DateTime(now.Year, now.Month, 1);
                            var EndDate = Convert.ToDateTime(lblDate.Text);
                            txtManualPayableDays.Text = (((EndDate - startDate).TotalDays) + 1).ToString();
                            LVDate.Text = ds1.Tables[0].Rows[0]["LFrom"].ToString();
                            RDate.Text = ds1.Tables[0].Rows[0]["LTo"].ToString();
                        }
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                    if (txtempid1.Value != "" || txtempid1.Value != null)
                    {
                        lnkViewAttendance.Visible = true;
                        lnkViewLeaveGrant.Visible = true;
                        lnksalryBrkup.Visible = true;
                        btnPrint.Visible = true;
                        tblN.Visible = true;
                        btnSearch.Visible = true;
                    }
                    if (!chkFinal.Checked)
                    {
                        if (Request.QueryString.Count > 0)
                        {
                            if (Request.QueryString.AllKeys.Contains("LID"))
                            {
                                LID = Convert.ToInt32(Request.QueryString["LID"]);
                            }
                        }
                        else
                            LID = 0;
                        SqlParameter[] param = new SqlParameter[3];
                        param[0] = new SqlParameter("@EmpID", empid);
                        param[1] = new SqlParameter("@CompanyID", CompanyID);
                        param[2] = new SqlParameter("@LID", LID);
                        DataSet ds = SQLDBUtil.ExecuteDataset("sh_VacationSettlementEmpInfo", param);
                        if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0)
                        {
                            gvGranted.DataSource = ds.Tables[0];
                            gvGranted.DataBind();
                            chkHRALOP.Visible = true;
                            chkEnch.Visible = false;
                            dtlvacation.Visible = false;
                            btnAccPost.Visible = false;
                            fudDocument1.Visible = false;
                            btnMonthfirst_Click(sender, e);
                            tbshow.Visible = true;
                        }
                        else
                        {
                            tbshow.Visible = false;
                            gvGranted.EmptyDataText = "No Records Found";
                            gvGranted.DataBind();
                            AlertMsg.MsgBox(Page, "NO future granted leave to consider for Settlement Make sure the employee has approved leave to process", AlertMsg.MessageType.Warning);
                        }
                    }
                    else
                    {
                        SqlParameter[] param1 = new SqlParameter[1];
                        param1[0] = new SqlParameter("@EmpID", EMPIDPram);
                        DataSet dss = SQLDBUtil.ExecuteDataset("sh_GetFinalExistStatus", param1);
                        if (dss != null && dss.Tables.Count > 0 && dss.Tables[0].Rows.Count > 0)
                        {
                            txtdate.Text = dss.Tables[0].Rows[0][1].ToString();
                            lblDate.Text = dss.Tables[0].Rows[0][1].ToString();
                            LVDate.Text = dss.Tables[0].Rows[0]["LFrom"].ToString();
                            RDate.Text = dss.Tables[0].Rows[0]["LTo"].ToString();
                            DateTime now = Convert.ToDateTime(lblDate.Text);
                            var startDate = new DateTime(now.Year, now.Month, 1);
                            var EndDate = Convert.ToDateTime(lblDate.Text);
                            txtManualPayableDays.Text = (((EndDate - startDate).TotalDays) + 1).ToString();
                            tbshow.Visible = true;
                            btnMonthfirst_Click(sender, e);
                            SqlParameter[] parms1 = new SqlParameter[1];
                            parms1[0] = new SqlParameter("@FEID", dss.Tables[0].Rows[0][2].ToString());
                            DataSet ds = SQLDBUtil.ExecuteDataset("Sh_finalExitRemarksView", parms1);
                            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                            {
                                gvRemarks.DataSource = ds.Tables[0];
                                tblremarks.Visible = true;
                            }
                            else
                            {
                                gvRemarks.DataSource = null;
                                tblremarks.Visible = false;
                            }
                            gvRemarks.DataBind();
                        }
                        else
                        {
                            tbshow.Visible = false;
                            AlertMsg.MsgBox(Page, "Employee final exit application is not processed to proceed", AlertMsg.MessageType.Warning);
                        }
                    }
                }
                catch (Exception)
                {
                    //  throw;
                }
            }
            catch (Exception)
            {
                //throw;
            }
            // }
            //        else
            //        {
            //            // btnSearch.Visible = false;
            //            tbshow.Visible = false;
            //            chkEnch.Visible = false;
            //            chkFinal.Visible = false;
            //            dtlvacation.Visible = false;
            //            btnAccPost.Visible = false;
            //            AlertMsg.MsgBox(Page, "No LVRD. Please enter LVRD", AlertMsg.MessageType.Warning);
            //        }
            //    }
            //    else
            //    {
            //        AlertMsg.MsgBox(Page, "Please Check Leave Account", AlertMsg.MessageType.Warning);
            //    }
            //}
            //else
            //{
            //    AlertMsg.MsgBox(Page, "Previous month salary not calculated to this employee", AlertMsg.MessageType.Warning);
            //}
        }
        protected void gvGranted_RowCommand(object sender, GridViewCommandEventArgs e)
        {
        }
        protected void gvGranted_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if ((e.Row.DataItem as DataRowView)["Reason"].ToString() == "")
                {
                    LinkButton lnkreason = (LinkButton)e.Row.FindControl("lnkReason");
                    lnkreason.Visible = false;
                }
                e.Row.Cells[12].ToolTip = (e.Row.DataItem as DataRowView)["GrantedBy"].ToString();
                e.Row.Cells[7].ToolTip = (e.Row.DataItem as DataRowView)["Reason"].ToString();
                e.Row.Cells[16].ToolTip = (e.Row.DataItem as DataRowView)["LeaveName"].ToString();
            }
        }
        protected string FormatInput(object Status)
        {
            string retValue = "";
            string input = Status.ToString();
            if (input == "-1")
            {
                retValue = "--SELECT--";
            }
            if (input == "0")
            {
                retValue = "Applied";
            }
            if (input == "1")
            {
                retValue = "In-Process";
            }
            if (input == "2")
            {
                retValue = "Granted";
            }
            if (input == "3")
            {
                retValue = "Rejected";
            }
            return retValue;
        }
        private void CellNameWriting_Head(ref TableRow tblRow, int Width, string NameCell)
        {
            TableCell tcName = new TableCell();
            tcName.Text = NameCell;
            tcName.Style.Add("font-weight", "bold");
            tcName.Style.Add("border", " solid navy 1px");
            tcName.Width = Width;
            tblRow.Cells.Add(tcName);
        }
        private void CellNameWriting(ref TableRow tblRow, int Width, string NameCell)
        {
            TableCell tcName = new TableCell();
            tcName.Text = NameCell;
            tcName.Style.Add("border", " solid navy 1px");
            tcName.Width = Width;
            tblRow.Cells.Add(tcName);
        }
        private void CellNameWriting_ForDates(ref TableRow tblRow, int Width, string NameCell)
        {
            TableCell tcName = new TableCell();
            tcName.Text = NameCell;
            tcName.Style.Add("font-weight", "bold");
            tcName.Style.Add("border", " solid navy 1px");
            if (stmonth == edmonth)
            {
                tblAtt.Style.Add("background-color", "light Blue");
            }
            else
                tcName.Style.Add("background-color", "#87cefa");
            tcName.Style.Add("text-align", "center");
            tcName.Width = Width;
            tblRow.Cells.Add(tcName);
        }
        private void CellNameWriting_Red(ref TableRow tblRow, int Width, string NameCell, Boolean IsHead, Boolean IsAtt)
        {
            TableCell tcName = new TableCell();
            tcName.Text = NameCell;
            tcName.Style.Add("color", "red");
            if (IsHead)
                tcName.Style.Add("font-weight", "bold");
            tcName.Style.Add("border", " solid navy 1px");
            if (IsAtt)
                if (stmonth == edmonth)
                {
                    tblAtt.Style.Add("background-color", "light Blue");
                }
                else
                    tcName.Style.Add("background-color", "#87cefa");
            else
                tcName.Style.Add("background-color", "#A1F9DB");
            tcName.Style.Add("text-align", "center");
            tcName.Width = Width;
            tblRow.Cells.Add(tcName);
        }
        private void CellNameWriting_Green(ref TableRow tblRow, int Width, string NameCell, Boolean IsHead)
        {
            TableCell tcName = new TableCell();
            tcName.Text = NameCell;
            tcName.Style.Add("color", "green");
            if (IsHead)
                if (stmonth == edmonth)
                {
                    tblAtt.Style.Add("background-color", "light Blue");
                }
                else
                    tcName.Style.Add("font-weight", "bold");
            tcName.Style.Add("border", " solid navy 1px");
            tcName.Style.Add("text-align", "center");
            tcName.Width = Width;
            tblRow.Cells.Add(tcName);
        }
        private void CellNameWriting_Green(ref TableRow tblRow, int Width, string NameCell, Boolean IsHead, Boolean IsAtt)
        {
            TableCell tcName = new TableCell();
            tcName.Text = NameCell;
            tcName.Style.Add("color", "green");
            if (IsHead)
                tcName.Style.Add("font-weight", "bold");
            tcName.Style.Add("border", " solid navy 1px");
            if (IsAtt)
                if (stmonth == edmonth)
                {
                    tblAtt.Style.Add("background-color", "light Blue");
                }
                else
                    tcName.Style.Add("background-color", "#87cefa");
            else
                tcName.Style.Add("background-color", "#A1F9DB");
            tcName.Style.Add("text-align", "center");
            tcName.Width = Width;
            tblRow.Cells.Add(tcName);
        }
        private void CellNameWriting_Green_P(ref TableRow tblRow, int Width, string NameCell, Boolean IsHead, Boolean IsAtt, Boolean isOuttym)
        {
            TableCell tcName = new TableCell();
            tcName.Text = NameCell;
            if (isOuttym)
                tcName.Style.Add("color", "green");
            else
            {
                tcName.Style.Add("color", "Navy"); tcName.Style.Add("font-weight", "bold");
            }
            if (IsHead)
                tcName.Style.Add("font-weight", "bold");
            tcName.Style.Add("border", " solid navy 1px");
            if (IsAtt)
                if (stmonth == edmonth)
                {
                    tblAtt.Style.Add("background-color", "light Blue");
                }
                else
                    tcName.Style.Add("background-color", "#87cefa");
            else
                tcName.Style.Add("background-color", "#A1F9DB");
            tcName.Style.Add("text-align", "center");
            tcName.Width = Width;
            tblRow.Cells.Add(tcName);
        }
        private void CellNameWriting_Blue(ref TableRow tblRow, int Width, string NameCell, Boolean IsHead, Boolean IsAtt)
        {
            TableCell tcName = new TableCell();
            tcName.Text = NameCell;
            tcName.Style.Add("color", "blue");
            if (IsHead)
                tcName.Style.Add("font-weight", "bold");
            tcName.Style.Add("border", " solid navy 1px");
            if (IsAtt)
                if (stmonth == edmonth)
                {
                    tblAtt.Style.Add("background-color", "light Blue");
                }
                else
                    tcName.Style.Add("background-color", "#87cefa");
            else
                tcName.Style.Add("background-color", "#A1F9DB");
            tcName.Style.Add("text-align", "center");
            tcName.Width = Width;
            tblRow.Cells.Add(tcName);
        }
        private void CellNameWriting_Gray(ref TableRow tblRow, int Width, string NameCell, Boolean IsHead, Boolean IsAtt)
        {
            TableCell tcName = new TableCell();
            tcName.Text = NameCell;
            tcName.Style.Add("color", "Ornge");
            if (IsHead)
                tcName.Style.Add("font-weight", "bold");
            tcName.Style.Add("border", " solid navy 1px");
            if (IsAtt)
                if (stmonth == edmonth)
                {
                    tblAtt.Style.Add("background-color", "light Blue");
                }
                else
                    tcName.Style.Add("background-color", "#87cefa");
            else
                tcName.Style.Add("background-color", "#A1F9DB");
            tcName.Style.Add("text-align", "center");
            tcName.Width = Width;
            tblRow.Cells.Add(tcName);
        }
        // added by pratap date:10-jan-2017
        protected void lnkALGross_click(object sender, EventArgs e)
        {
            try
            {
                int Empid = 0;
                if (txtempid1.Value != "" || txtempid1.Value != null)
                {
                    Empid = Convert.ToInt32(txtempid1.Value);
                }
                int LID = 5;
                string url = "LeavesAvailableDetails.aspx?EMPID=" + Empid + "&LID=" + LID;
                string fullURL = "window.open('" + url + "', '_blank' );";
                ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", fullURL, true);
            }
            catch (Exception)
            {
                throw;
            }
        }
        // 
        protected void lnkOT_click(object sender, EventArgs e)
        {
            try
            {
                int Empid = 0;
                if (txtempid1.Value != "" || txtempid1.Value != null)
                {
                    Empid = Convert.ToInt32(txtempid1.Value);
                }
                int Month, Year;
                Month = (DateTime.ParseExact(txtdate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).Month);
                //month = Dmonth.Month;
                Year = (DateTime.ParseExact(txtdate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).Year);
                string url = "OTPayments.aspx?EMPID=" + Empid + "&Month=" + Month + "&Year=" + Year;
                string fullURL = "window.open('" + url + "', '_blank' );";
                ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", fullURL, true);
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected void lnkEmpPen_click(object sender, EventArgs e)
        {
            try
            {
                int Empid = 0;
                if (txtempid1.Value != "" || txtempid1.Value != null)
                {
                    Empid = Convert.ToInt32(txtempid1.Value);
                }
                string url = "EmpPenalties.aspx?EMPID=" + Empid;
                string fullURL = "window.open('" + url + "', '_blank' );";
                ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", fullURL, true);
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected void lnkGratuity_click(object sender, EventArgs e)
        {
            try
            {
                int Empid = 0;
                if (txtempid1.Value != "" || txtempid1.Value != null)
                {
                    Empid = Convert.ToInt32(txtempid1.Value);
                }
                string url = "Gratuity.aspx?EMPID=" + Empid + "&state=3&Date=" + txtdate.Text + "&EmpDeAct=" + ddlEmpDeAct.SelectedValue;
                string fullURL = "window.open('" + url + "', '_blank' );";
                ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", fullURL, true);
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected void lnkLoans_click(object sender, EventArgs e)
        {
            try
            {
                int Empid = 0;
                if (txtempid1.Value != "" || txtempid1.Value != null)
                {
                    Empid = Convert.ToInt32(txtempid1.Value);
                }
                string url = "EmpAdvances.aspx?EMPID=" + Empid;
                string fullURL = "window.open('" + url + "', '_blank' );";
                ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", fullURL, true);
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected void lnkABS_click(object sender, EventArgs e)
        {
            try
            {
                int Empid = 0;
                if (txtempid1.Value != "" || txtempid1.Value != null)
                {
                    Empid = Convert.ToInt32(txtempid1.Value);
                }
                int Month, Year;
                Month = (DateTime.ParseExact(txtdate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).Month);
                //month = Dmonth.Month;
                Year = (DateTime.ParseExact(txtdate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).Year);
                string url = "AbsPenalities.aspx?EMPID=" + Empid + "&Month=" + Month + "&Year=" + Year;
                string fullURL = "window.open('" + url + "', '_blank' );";
                ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", fullURL, true);
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected void btnMonthfirst_Click(object sender, EventArgs e)
        {
            try
            {
                tblN.Visible = true;
                if (!chkFinal.Checked)
                {
                    SqlParameter[] param = new SqlParameter[1];
                    param[0] = new SqlParameter("@EmpID", EMPIDPram);
                    DataSet ds = SQLDBUtil.ExecuteDataset("sh_GetVacationSettlementEmpDate", param);
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        txtdate.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["LastDate"].ToString()).ToString("dd/MM/yyyy");
                    }
                    else
                    {
                        txtdate.Text = Convert.ToDateTime(DateTime.Now.ToString()).ToString("dd/MM/yyyy");
                    }
                }
                else
                {
                    txtdate.Text = Convert.ToDateTime(DateTime.Now.ToString()).ToString("dd/MM/yyyy");
                }
                EmpListPaging.Visible = false;
                try
                {
                }
                catch { }
                if (txtEmpName.Text == "" || txtEmpName.Text == null)
                {
                    txtEmpNameHidden.Value = "";
                }
                string Name = null;
                try
                {
                    if (EMPIDPram == 0 || EMPIDPram == null)
                    {
                        if (txtEmpName.Text != "" || txtEmpName.Text != null)
                        {
                            EMPIDPram = Convert.ToInt32(txtEmpNameHidden.Value == "" ? "0" : txtEmpNameHidden.Value);
                        }
                        Name = "";
                    }
                }
                catch { }
                int month = (DateTime.ParseExact(txtdate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).Month);
                int monthtext = month;
                int year = (DateTime.ParseExact(txtdate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).Year);
                int yeartext = year; int year1 = year;
                int startdate = 1;
                if (startdate != 1)
                {
                    if (month == 1)
                    {
                        month = 12;
                        year = year - 1;
                        year1 = year + 1;
                    }
                    else
                        month = month - 1;
                }
                string st = month + "/" + startdate + "/" + year;
                DateTime dt = CODEUtility.ConvertToDate(st, DateFormat.MonthDayYear);// DateTime.ParseExact(st, "MM/dd/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                DateTime dtEnd = dt.AddMonths(1);
                int EmpNatureID = 0;
                int DepartmentID = 0, WorkSiteID = 0;
                try
                {
                    DepartmentID = 0;
                }
                catch { } try
                {
                    WorkSiteID = 0;
                }
                catch { }
                DateTime StartDate = dt, EndDate = dtEnd;
                List<DateTime> dateList = new List<DateTime>();
                int DayInterval = 1;
                int TotalPages = 1;//returnVale
                int NoofRecords = 100;// return value
                int PageSize = EmpListPaging.ShowRows;
                int CurrentPage = EmpListPaging.CurrentPage;
                try
                {
                    if (Convert.ToInt32(ViewState["WSID"]) > 0)
                        WorkSiteID = Convert.ToInt32(ViewState["WSID"]);
                }
                catch { }
                DataSet dsEPMData = AttendanceDAC.HR_GetAttandanceByPaging(EMPIDPram, 2, DepartmentID, EmpNatureID, StartDate, EndDate
                    , CurrentPage, PageSize, ref NoofRecords, ref TotalPages, Name, 0);
                tblAtt.Rows.Clear();
                tblAtt.Style.Add("border", "solid red 1px");
                tblAtt.Style.Add("border-collapse", "collapse");
                //2 
                Boolean isFirst = true;
                TableRow tblHeadRow = new TableRow();
                TableRow tblDepartRow = new TableRow();
                tblRow = new TableRow();
                int DeptID = 0;
                Hashtable ht = new Hashtable();
                int WidthP = 30;
                int WidthPName = 300;
                foreach (DataRow drEMP in dsEPMData.Tables[2].Rows)
                {
                    tblHeadRow = new TableRow();
                    //nookesh start
                    if (isFirst)
                    {
                        TableRow rowNew = new TableRow();
                        tblAtt.Controls.Add(rowNew);
                        TableCell cellNew0 = new TableCell();
                        TableCell cellNew = new TableCell();
                        rowNew.Style.Add("border", " solid navy 1px");
                        cellNew.Style.Add("background-color", "#87cefa");
                        cellNew.Style.Add("font-weight", "bold");
                        cellNew.Style.Add("Text-align", "Center");
                        for (int row = 0; row < 1; row++)
                        {
                            for (int col = 0; col < 3; col++)
                            {
                                if (col > 0)
                                {
                                    switch (Convert.ToInt32(monthtext))
                                    {
                                        case 1:
                                            cellNew0.Text = "".ToString();
                                            //int year1 = year + 1;                                         
                                            cellNew.Text = "January".ToString() + " " + year1;
                                            cellNew.ColumnSpan = 31;
                                            break;
                                        case 2:
                                            cellNew0.Text = "".ToString();
                                            cellNew.Text = "February".ToString() + " " + year;
                                            cellNew.ColumnSpan = 28;
                                            break;
                                        case 3:
                                            cellNew0.Text = "".ToString();
                                            cellNew.Text = "March".ToString() + " " + year;
                                            cellNew.ColumnSpan = 31;
                                            break;
                                        case 4:
                                            cellNew0.Text = "".ToString();
                                            cellNew.Text = "April".ToString() + " " + year;
                                            cellNew.ColumnSpan = 30;
                                            break;
                                        case 5:
                                            cellNew0.Text = "".ToString();
                                            cellNew.Text = "May".ToString() + year;
                                            cellNew.ColumnSpan = 31;
                                            break;
                                        case 6:
                                            cellNew0.Text = "".ToString();
                                            cellNew.Text = "June".ToString() + " " + year;
                                            cellNew.ColumnSpan = 30;
                                            break;
                                        case 7:
                                            cellNew0.Text = "".ToString();
                                            cellNew.Text = "July".ToString() + " " + year;
                                            cellNew.ColumnSpan = 31;
                                            break;
                                        case 8:
                                            cellNew0.Text = "".ToString();
                                            cellNew.Text = "Augest".ToString() + " " + year;
                                            cellNew.ColumnSpan = 31;
                                            break;
                                        case 9:
                                            cellNew0.Text = "".ToString();
                                            cellNew.Text = "September".ToString() + " " + year;
                                            cellNew.ColumnSpan = 30;
                                            break;
                                        case 10:
                                            cellNew0.Text = "".ToString();
                                            cellNew.Text = "October".ToString() + " " + year;
                                            cellNew.ColumnSpan = 31;
                                            break;
                                        case 11:
                                            cellNew0.Text = "".ToString();
                                            cellNew.Text = "November".ToString() + " " + year;
                                            cellNew.ColumnSpan = 30;
                                            break;
                                        case 12:
                                            cellNew0.Text = "".ToString();
                                            cellNew.Text = "December".ToString() + " " + year;
                                            cellNew.ColumnSpan = 31;
                                            break;
                                        default:
                                            cellNew.Text = "".ToString();
                                            break;
                                    }
                                }
                                else
                                    cellNew.Text = "".ToString();
                            }
                            rowNew.Controls.Add(cellNew0);
                            rowNew.Controls.Add(cellNew);
                            int x = CheckLeapYear(year1);
                            if (x == 1 && monthtext == 2)
                            {
                                cellNew.ColumnSpan = 29;
                            }
                        }
                        //nookesh
                    }
                    tblRow = new TableRow();
                    tblDepartRow = null;
                    ht = new Hashtable();
                    if (isFirst)
                        // for Header
                        CellNameWriting_Head(ref tblHeadRow, WidthPName, "Name");
                    CellNameWriting(ref tblRow, WidthPName, drEMP["Name"].ToString());
                    StartDate = dt;
                    while (StartDate.AddDays(DayInterval - 1) < EndDate)
                    {
                        string stdt = StartDate.ToString();
                        string[] stm = stdt.ToString().Split('/');
                        stmonth = Convert.ToInt32(stm[0]);
                        string eddt = EndDate.ToString();
                        string[] edt = eddt.ToString().Split('/');
                        edmonth = Convert.ToInt32(edt[0]);
                        if (isFirst)
                        {
                            // for Header Dates
                            CellNameWriting_ForDates(ref tblHeadRow, WidthP, StartDate.Day.ToString());
                        }
                        try
                        {
                            DataRow[] drsAtt = dsEPMData.Tables[1].Select("Date = '" + StartDate + "' and EMPID='" + drEMP["ID"] + "'");
                            if (drsAtt.Length > 0)
                            {
                                switch (Convert.ToInt32(drsAtt[0]["Status"]))
                                {
                                    case 1:
                                        CellNameWriting_Red(ref tblRow, WidthP, drsAtt[0]["ShortName"].ToString(), false, true);
                                        break;
                                    case 2:
                                        if (Convert.ToInt32(drsAtt[0]["isOutTime"]) == 0)
                                            CellNameWriting_Green_P(ref tblRow, WidthP, drsAtt[0]["ShortName"].ToString(), false, true, false);
                                        else
                                            CellNameWriting_Green_P(ref tblRow, WidthP, drsAtt[0]["ShortName"].ToString(), false, true, true);
                                        break;
                                    case 7:
                                        CellNameWriting_Green(ref tblRow, WidthP, drsAtt[0]["ShortName"].ToString(), false, true);
                                        break;
                                    case 8:
                                        CellNameWriting_Green(ref tblRow, WidthP, drsAtt[0]["ShortName"].ToString(), false, true);
                                        break;
                                    case 9:
                                        CellNameWriting_Blue(ref tblRow, WidthP, drsAtt[0]["ShortName"].ToString(), false, true);
                                        break;
                                    default:
                                        CellNameWriting_Gray(ref tblRow, WidthP, drsAtt[0]["ShortName"].ToString(), false, true);
                                        break;
                                        ;
                                }
                                if (ht.ContainsKey(drsAtt[0]["Status"]))
                                    ht[drsAtt[0]["Status"]] = Convert.ToInt32(ht[drsAtt[0]["Status"]]) + 1;
                                else
                                    ht.Add(drsAtt[0]["Status"], 1);
                            }
                            else
                                CellNameWriting_Red(ref tblRow, WidthP, "-", false, true);
                            if (ht.ContainsKey(0))
                                ht[0] = Convert.ToInt32(ht[0]) + 1;
                            else
                                ht.Add(0, 1);
                            StartDate = StartDate.AddDays(DayInterval);
                            //dateList.Add(StartDate);
                        }
                        catch { }
                    }
                    if (isFirst)
                        CellNameWriting_Green(ref tblHeadRow, WidthP, "Scope", true);
                    string ValueNo = "0";
                    if (ht.ContainsKey(0))
                        ValueNo = ht[0].ToString();
                    CellNameWriting_Green(ref tblRow, WidthP, ValueNo, false);
                    foreach (DataRow drAM in dsEPMData.Tables[0].Rows)
                    {
                        if (isFirst)
                        {
                            // for Header
                            string Namestring = drAM["Name"].ToString();
                            switch (Convert.ToInt32(drAM["ID"]))
                            {
                                case 0:
                                    CellNameWriting_Green(ref tblHeadRow, WidthP, Namestring, true, false);
                                    break;
                                case 1:
                                    CellNameWriting_Red(ref tblHeadRow, WidthP, Namestring, true, false);
                                    break;
                                case 2:
                                    CellNameWriting_Green(ref tblHeadRow, WidthP, Namestring, true, false);
                                    break;
                                case 7:
                                    CellNameWriting_Green(ref tblHeadRow, WidthP, Namestring, true, false);
                                    break;
                                case 8:
                                    CellNameWriting_Green(ref tblHeadRow, WidthP, Namestring, true, false);
                                    break;
                                case 9:
                                    CellNameWriting_Blue(ref tblHeadRow, WidthP, Namestring, true, false);
                                    break;
                                default:
                                    CellNameWriting_Gray(ref tblHeadRow, WidthP, Namestring, true, false);
                                    break;
                                    ;
                            }
                        }
                        ValueNo = "0";
                        if (ht.ContainsKey(drAM["ID"]))
                            ValueNo = ht[drAM["ID"]].ToString();
                        switch (Convert.ToInt32(drAM["ID"]))
                        {
                            case 0:
                                CellNameWriting_Green(ref tblRow, WidthP, ValueNo, false, false);
                                break;
                            case 1:
                                CellNameWriting_Red(ref tblRow, WidthP, ValueNo, false, false);
                                break;
                            case 2:
                                CellNameWriting_Green(ref tblRow, WidthP, ValueNo, false, false);
                                break;
                            case 7:
                                CellNameWriting_Green(ref tblRow, WidthP, ValueNo, false, false);
                                break;
                            case 8:
                                CellNameWriting_Green(ref tblRow, WidthP, ValueNo, false, false);
                                break;
                            case 9:
                                CellNameWriting_Blue(ref tblRow, WidthP, ValueNo, false, false);
                                break;
                            default:
                                CellNameWriting_Gray(ref tblRow, WidthP, ValueNo, false, false);
                                break;
                                ;
                        }
                    }
                    if (isFirst)
                        tblAtt.Rows.Add(tblHeadRow);
                    if (tblDepartRow != null)
                        tblAtt.Rows.Add(tblDepartRow);
                    tblAtt.Rows.Add(tblRow);
                    tblN.Visible = true;
                }
            }
            catch (Exception ex)
            {
                AlertMsg.MsgBox(Page, ex.Message.ToString(), AlertMsg.MessageType.Error);
            }
        }
        public int CheckLeapYear(int intyear)
        {
            if (intyear % 4 == 0 && intyear % 100 != 0 || intyear % 400 == 0)
                return 1; // It is a leap year
            else
                return 0; // Not a leap year
        }
        protected void lnkAirTicket_click(object sender, EventArgs e)
        {
            try
            {
                int Empid = 0;
                if (txtempid1.Value != "" || txtempid1.Value != null)
                {
                    Empid = Convert.ToInt32(txtempid1.Value);
                }
                // modify by pratap date: 18-jan-2017 
                SqlParameter[] sqlParams = new SqlParameter[1];
                sqlParams[0] = new SqlParameter("@EmpID", Empid);
                DataSet ds = SQLDBUtil.ExecuteDataset("sh_GetAirticketConfigByEmpiDorNot", sqlParams);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    int key = 2;
                    string url = "EmpAirTicketAuthorisations.aspx?EMPID=" + Empid + "&Key=" + key;
                    string fullURL = "window.open('" + url + "', '_blank' );";
                    ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", fullURL, true);
                }
                else
                {
                    AlertMsg.MsgBox(Page, "Employee Vs Airticket is NOT configured", AlertMsg.MessageType.Warning);
                    return;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected void chkFinal_CheckedChanged(object sender, EventArgs e)
        {
            if (chkFinal.Checked)
            {
                dvfnactive.Visible = true;
                EMPIDPram = Convert.ToInt32(txtempid1.Value);
                SqlParameter[] param1 = new SqlParameter[1];
                param1[0] = new SqlParameter("@EmpID", EMPIDPram);
                DataSet dss = SQLDBUtil.ExecuteDataset("sh_GetFinalExistStatus", param1);
                if (dss != null && dss.Tables.Count > 0 && dss.Tables[0].Rows.Count > 0)
                {
                    //txtdate.Text = dss.Tables[0].Rows[0][1].ToString();
                    //lblDate.Text = dss.Tables[0].Rows[0][1].ToString();
                    //DateTime now = Convert.ToDateTime(lblDate.Text);
                    //var startDate = new DateTime(now.Year, now.Month, 1);
                    //var EndDate = Convert.ToDateTime(lblDate.Text);
                    //txtManualPayableDays.Text = (((EndDate - startDate).TotalDays) + 1).ToString();
                    //tbshow.Visible = true;
                    //btnMonthfirst_Click(sender, e);
                }
                else
                {
                    tbshow.Visible = false;
                    AlertMsg.MsgBox(Page, "Employee final exit application is not processed to proceed", AlertMsg.MessageType.Warning);
                }
            }
            else
            {
                dvfnactive.Visible = false;
                tblremarks.Visible = false;
            }
        }
    }
}