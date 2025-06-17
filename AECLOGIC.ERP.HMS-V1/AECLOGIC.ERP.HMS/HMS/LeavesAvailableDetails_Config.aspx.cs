using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AECLOGIC.ERP.HMS.HMS
{
    public partial class LeavesAvailableDetails_Config : AECLOGIC.ERP.COMMON.WebFormMaster
    {
        static string strurl = string.Empty;

        protected override void OnInit(EventArgs e)
        {
            ModuleID = 1;
            base.OnInit(e);

        }
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                   
                    BindYears();
                    LoadData();
                    ViewState["RefUrl"] = Request.UrlReferrer.ToString();
                    Session["Empid"] = null;
                 
                }
            }
            catch (Exception ex)
            {
                clsErrorLog.HMSEventLog(ex, "LeaveAvailableDetails", "Page_Load", "001");

            }

        }

        public void BindYears()
        {
            try
            {
                DataSet ds = AECLOGIC.HMS.BLL.AttendanceDAC.HR_GetCalenderYears();
                if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ddlYear.DataValueField = "AssYearId";
                    ddlYear.DataTextField = "AssessmentYear";
                    ddlYear.DataSource = ds;
                    ddlYear.DataBind();
                    ddlYear.Items.Insert(0, new ListItem("--All--", "0"));
                    ddlYear.SelectedIndex = 0;// (ddlYear.Items.Count - 1);
                }
            }
            catch (Exception e)
            {
                //throw e;
            }
        }

        private void LoadData()
        {

            try
            {
                int EmpID = Convert.ToInt32(Request.QueryString["EMPID"]);
                int LID = Convert.ToInt32(Request.QueryString["LID"]);
                DataSet ds = Leaves.GetApplicableLeavesDetailsByPaging(EmpID, LID, Convert.ToInt32(ddlYear.SelectedValue));
                ViewState["DataSet"] = ds;
                dtlWOProgress.DataSource = ds;
                dtlWOProgress.DataBind();
                lblEMPName.Text = ds.Tables[2].Rows[0]["EmpName"].ToString(); lblEMPDOJ.Text = ds.Tables[2].Rows[0]["DateOfJoin"].ToString();
                lblGrade.Text = ds.Tables[2].Rows[0]["Grade"].ToString(); ///lblclosingBal.Text = ds.Tables[2].Rows[0]["BalC"].ToString(); 
                txtempname_hid.Value = lblGrade.Text;
                txtempID.Value = Request.QueryString["EMPID"];
                lblLVRD.Text = ds.Tables[2].Rows[0]["LVRD"].ToString();
                if (lblLVRD.Text.Contains("LVRD"))
                    lblLVRD.ForeColor = System.Drawing.Color.Red;
                lblCalByPD.Text = ds.Tables[2].Rows[0]["PDDays"].ToString();
                btnTypeLea.Text = lblGrade.Text;
                if (lblGrade.Text.Contains("Set"))
                    lblGrade.Text = "";
                else
                    btnTypeLea.Text = "";
                lblActDt.Text = ds.Tables[2].Rows[0]["ActionDt"].ToString();
                if (lblActDt.Text.Contains("SET"))
                    lblActDt.ForeColor = System.Drawing.Color.Red;
                lnkActDt.Text = lblActDt.Text;
                if (lblActDt.Text.Contains("Set"))
                {
                    lblActDt.Text = "";
                    AlertMsg.MsgBox(Page, "SET LVRD and A/c Start Date");
                    btnsave.Enabled = false;
                }
                else
                    lnkActDt.Text = "";
                lnkLVRD.Text = lblLVRD.Text;
                if (lblLVRD.Text.Contains("Set"))
                {
                    lblLVRD.Text = "";
                    AlertMsg.MsgBox(Page, "SET LVRD and A/c Start Date");
                    btnsave.Enabled = false;
                }
                else
                    lnkLVRD.Text = "";
            }
            catch { }
        }
        public string TotCrBal = "0.00";
        public string TotDrBal = "0.00";
        public DataView BindTransdetails(string TransId)
        {
            DataSet dstrans = (DataSet)ViewState["DataSet"];
            TotCrBal = Convert.ToDecimal(dstrans.Tables[1].Compute("Sum(Cr)", "ID='" + TransId + "'")).ToString("0.00");
            TotDrBal = Convert.ToDecimal(dstrans.Tables[1].Compute("Sum(Dr)", "ID='" + TransId + "'")).ToString("0.00");
            DataView dv = dstrans.Tables[1].DefaultView;
            dv.RowFilter = "ID='" + TransId + "'";
            return dv;
        }

     

        protected void dtlWOProgress_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            try
            {
              
                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    Label lblCFWD = (Label)e.Item.FindControl("lblCFWD");
                    if (Convert.ToBoolean(lblCFWD.Text) == true)
                        lblCFWD.Text = "YES";
                    else
                        lblCFWD.Text = "NO";
                }
            }
            catch (Exception ex) { AlertMsg.MsgBox(Page, ex.Message.ToString(),AlertMsg.MessageType.Error); }
        }

        protected void gvEditTasks_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    try
                    {
                        HiddenField _hdn = (HiddenField)e.Row.FindControl("HidOpenID");
                        Label txtCr_lbl = (Label)e.Row.FindControl("txtCr_lbl");
                        TextBox txtCr = (TextBox)e.Row.FindControl("txtCr");
                        txtCr_lbl.Visible = false;
                        txtCr.Visible = true;
                        HiddenField HidOpenID_IsCr = (HiddenField)e.Row.FindControl("HidOpenID_IsCr");
                        if (Convert.ToBoolean(HidOpenID_IsCr.Value) == false)
                        {
                            txtCr_lbl.Visible = true;
                            txtCr.Visible = false;
                            LinkButton ID = (LinkButton)e.Row.FindControl("lnkEdit");
                            ID.Visible = false;
                        }
                        if (Convert.ToBoolean(_hdn.Value) == false)
                        {
                            txtCr_lbl.Visible = true;
                            txtCr.Visible = false;
                            // txtCr = (TextBox)e.Row.FindControl("txtCr");
                            LinkButton ID = (LinkButton)e.Row.FindControl("lnkEdit");
                            ID.Visible = false;
                            TextBox txtActionDt = (TextBox)e.Row.FindControl("txtActionDt");
                            txtActionDt.Visible = false;

                        }
                        else
                        {
                            TextBox txtActionDt = (TextBox)e.Row.FindControl("txtActionDt");
                            txtActionDt.Text = "01 JAN " + DateTime.Today.ToString("yyyy");
                            txtActionDt.ReadOnly = true;
                        }
                    }
                    catch { }
                }
            }
            catch (Exception ex)
            {
                clsErrorLog.HMSEventLog(ex, "LeaveAvailableDetails", "gvEditTasks_RowDataBound", "002");

            }
        }
        // protected void btnsave_Click(object sender, EventArgs e)
        protected void btnsave_Click(object sender, EventArgs e)
        {
            dtlWOProgress.Visible = true;
            btnsave.Enabled = false;
            try
            {
              
                try
                {
                    DateTime dtSync = DateTime.Today;
                    if (ddlYear.SelectedIndex == 0)
                        dtSync = DateTime.Today;
                    else
                    {
                        if (DateTime.Today.Year == Convert.ToInt32(ddlYear.SelectedItem.Text))
                            dtSync = DateTime.Today;
                        else
                        {
                            if (Convert.ToInt32(ddlYear.SelectedItem.Text) < 2016)
                            {
                                AlertMsg.MsgBox(Page, "System Start from year 2016. ");
                                return;
                            }
                            else
                            {
                                dtSync = CodeUtilHMS.ConvertToDate("31 DEC " + ddlYear.SelectedItem.Text, CodeUtilHMS.DateFormat.ddMMMyyyy);
                            }

                        }
                    }
                    int UID = Convert.ToInt32(AECLOGIC.ERP.COMMON.clSession.cmnUserId);
                    int EmpID = Convert.ToInt32(Request.QueryString["EMPID"]);
                    Leaves.HR_LeaveSyncCal(EmpID,
                         dtSync, UID);
                    AlertMsg.MsgBox(Page, "Refreshed");
                    LoadData();


                }
                catch (Exception ex)
                {
                    AlertMsg.MsgBox(Page, ex.Message.ToString(),AlertMsg.MessageType.Error);
                }
            }
            catch (Exception ex)
            {
                AlertMsg.MsgBox(Page, ex.Message.ToString(),AlertMsg.MessageType.Error);
            }
            btnsave.Enabled = true;
        }
        protected void gvEditTasks_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Edt") //1 for Update
                {
                    int LTID = Convert.ToInt32(e.CommandArgument);
                    GridViewRow gvr = (GridViewRow)((LinkButton)e.CommandSource).Parent.Parent;
                    HiddenField ID = (HiddenField)gvr.FindControl("HidOpenID");


                    if (Convert.ToBoolean(ID.Value) == true)
                    {
                        TextBox txtCr = (TextBox)gvr.FindControl("txtCr");
                        decimal Cr = Convert.ToDecimal(txtCr.Text);
                        decimal Dr = 0;// Convert.ToDecimal(txtDr.Text);
                        int EmpId = Convert.ToInt32(AECLOGIC.ERP.COMMON.clSession.cmnUserId);
                        TextBox txtActionDt = (TextBox)gvr.FindControl("txtActionDt");
                        DateTime ActionDate = CodeUtilHMS.ConvertToDate(txtActionDt.Text, CodeUtilHMS.DateFormat.ddMMMyyyy);
                        Leaves.InsUpdateTypeofLeaves(LTID, Cr, Dr, EmpId, ActionDate);
                        AlertMsg.MsgBox(Page, "Submit");
                        btnsave_Click(null, null);
                        //LoadData();
                    }
              
                }

              
            }
            catch (Exception ex)
            {

                AlertMsg.MsgBox(Page, ex.Message.ToString(),AlertMsg.MessageType.Error);

            }
        }

        protected void btnback_Click(object sender, EventArgs e)
        {
            object refUrl = ViewState["RefUrl"];
            if (refUrl != null)
            {
                Session["Empid"] = Convert.ToInt32(Request.QueryString["EMPID"]);
                Response.Redirect((string)refUrl);
            }
        }

        protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            try { LoadData(); }
            catch { }
        }

    }
}