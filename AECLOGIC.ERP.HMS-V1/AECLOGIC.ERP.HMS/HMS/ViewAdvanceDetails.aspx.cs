using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using AECLOGIC.HMS.BLL;
using System.Configuration;
using System.Data.SqlClient;
using Aeclogic.Common.DAL;
using AECLOGIC.ERP.HMS;

namespace AECLOGIC.ERP.HMSV1
{
    public partial class ViewAdvanceDetailsV1 : AECLOGIC.ERP.COMMON.WebFormMaster
    {
        protected override void OnInit(EventArgs e)
        {
            ModuleID = 1;
            base.OnInit(e);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGrid();
            }
        }
        private void BindGrid()
        {
            if (Request.QueryString[0].ToString() != "")
            {
                try
                {
                    DataSet dsLaonID = AttendanceDAC.HR_GetLoanDetailsByID(Convert.ToInt32(Request.QueryString[0]), 0, ConfigurationManager.AppSettings["CompanyId"].ToString());
                    gvLoanDetails.DataSource = dsLaonID;
                    gvLoanDetails.DataBind();
                    lblEmpID.Text = dsLaonID.Tables[1].Rows[0]["EmpID"].ToString();
                    lblIssuedOn.Text = dsLaonID.Tables[1].Rows[0]["LoanIssuedDate1"].ToString();
                    lblloanAmt.Text = dsLaonID.Tables[1].Rows[0]["LoanAmount"].ToString();
                    lblLoanID.Text = dsLaonID.Tables[1].Rows[0]["LoanID"].ToString();
                    lblName.Text = dsLaonID.Tables[1].Rows[0]["Name"].ToString();
                    if (dsLaonID.Tables.Count > 1 && dsLaonID.Tables[2].Rows.Count > 0)
                        txtReverseAmount.Text = dsLaonID.Tables[2].Rows[0]["DueAmount"].ToString();
                    else
                        txtReverseAmount.Text = "0";

                    //displaying the approval details
                    SqlParameter[] parms1 = new SqlParameter[1];
                    parms1[0] = new SqlParameter("@loanID", Convert.ToInt32(Request.QueryString[0]));
                    DataSet dsApprovals = SQLDBUtil.ExecuteDataset("Sh_LoanRemarksView", parms1);
                    for (int i = 0; i < dsApprovals.Tables[0].Rows.Count; i++)
                    {
                        string strRank = dsApprovals.Tables[0].Rows[i]["Rank"].ToString();
                        switch (strRank) {
                            case "1":
                                lblRequestBy.Text = dsApprovals.Tables[0].Rows[i]["CreatedBy"] + " " + dsApprovals.Tables[0].Rows[i]["Name"];
                                break;
                            case "3":
                                lblPMApprovalBy.Text = dsApprovals.Tables[0].Rows[i]["CreatedBy"] + " " + dsApprovals.Tables[0].Rows[i]["Name"];
                                break;
                            case "4":
                                lblCOOBy.Text = dsApprovals.Tables[0].Rows[i]["CreatedBy"] + " " + dsApprovals.Tables[0].Rows[i]["Name"];
                                break;
                            case "5":
                                lblHrApprovalBy.Text = dsApprovals.Tables[0].Rows[i]["CreatedBy"] + " " + dsApprovals.Tables[0].Rows[i]["Name"];
                                break;
                            case "6":
                                if (dsApprovals.Tables[0].Rows[i]["Status"].ToString().Equals("GM Approval"))
                                    lblGMApprovalBy.Text = dsApprovals.Tables[0].Rows[i]["CreatedBy"] + " " + dsApprovals.Tables[0].Rows[i]["Name"];
                                break;
                            case "0":
                                if (dsApprovals.Tables[0].Rows[i]["Status"].ToString().Equals("CFO Approval"))
                                    lblCFOApprovalBy.Text = dsApprovals.Tables[0].Rows[i]["CreatedBy"] + " " + dsApprovals.Tables[0].Rows[i]["Name"];
                                break;
                            default:
                                break;
                        }
                    }   
                }
                catch { }
            }
        }
        protected void btnReverseLoan_Click(object sender, EventArgs e)
        {
            try
            {
                if (lblEmpID.Text != "")
                {
                    if (lblLoanID.Text != "")
                    {
                        if (txtReverseAmount.Text != "" && txtReverseAmount.Text != "0")
                        {
                            SqlParameter[] parm = new SqlParameter[6];
                            parm[0] = new SqlParameter("@EMPID ", lblEmpID.Text);
                            parm[1] = new SqlParameter("@Amount", txtReverseAmount.Text);
                            parm[2] = new SqlParameter("@LoanID", lblLoanID.Text);
                            parm[3] = new SqlParameter("@userID ", Session["UserId"]);
                            parm[4] = new SqlParameter("@CompanyID", 1);
                            parm[5] = new SqlParameter("@case", 1);
                            SQLDBUtil.ExecuteNonQuery("sh_ReverseAndEnteringLoanOpenBal", parm);
                            BindGrid();
                           // AlertMsg.MsgBox(Page, "Saved Successfully");
                        }
                        else
                            AlertMsg.MsgBox(Page, "Check Reverse Loan Amount", AlertMsg.MessageType.Warning);
                    }
                    else
                        AlertMsg.MsgBox(Page, "Check Loan ID", AlertMsg.MessageType.Warning);
                }
                else
                    AlertMsg.MsgBox(Page, "Check Employee ID", AlertMsg.MessageType.Warning);
            }
            catch (Exception)
            {
               // throw;
            }
        }
        protected void btnNewLoan_Click(object sender, EventArgs e)
        {
            try
            {
                if (lblEmpID.Text != "")
                {
                    if (txtLoanAmount.Text != "" && txtLoanAmount.Text != "0")
                    {
                        if (txtNoEMI.Text != "" && txtNoEMI.Text != "0")
                        {
                            SqlParameter[] parm = new SqlParameter[6];
                            parm[0] = new SqlParameter("@EMPID ", lblEmpID.Text);
                            parm[1] = new SqlParameter("@Amount", txtLoanAmount.Text);
                            parm[2] = new SqlParameter("@userID ", Session["UserId"]);
                            parm[3] = new SqlParameter("@CompanyID", 1);
                            parm[4] = new SqlParameter("@case", 2);
                            parm[5] = new SqlParameter("@EMI", txtNoEMI.Text);
                            SQLDBUtil.ExecuteNonQuery("sh_ReverseAndEnteringLoanOpenBal", parm);
                            AlertMsg.MsgBox(Page, "Loan Issued Successfully");
                        }
                        else
                            AlertMsg.MsgBox(Page, "Check NO of Installments", AlertMsg.MessageType.Warning);
                    }
                    else
                        AlertMsg.MsgBox(Page, "Check Loan Amount", AlertMsg.MessageType.Warning);
                }
                else
                    AlertMsg.MsgBox(Page, "Check Employee ID", AlertMsg.MessageType.Warning);
            }
            catch (Exception)
            {
            //    throw;
            }
        }
    }
}