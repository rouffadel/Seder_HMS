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

namespace AECLOGIC.ERP.HMS
{
    public partial class ShiftBonus : AECLOGIC.ERP.COMMON.WebFormMaster
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
                ViewState["BID"] = 0;
                tblView.Visible = true;
                tblAdd.Visible = false;
                BindGrid();
                 
              DataSet  ds = AttendanceDAC.HR_getBonusDefaults();
                if (ds.Tables[0].Rows.Count != 0)
                {
                    txtBonusPercent.Text = Convert.ToInt32(ds.Tables[0].Rows[0][1]).ToString();
                    txtTrgtS1.Text = Convert.ToInt32(ds.Tables[0].Rows[0][2]).ToString();
                    txtTrgtS2.Text = Convert.ToInt32(ds.Tables[0].Rows[0][3]).ToString();
                    txtTrgtS3.Text = Convert.ToInt32(ds.Tables[0].Rows[0][4]).ToString();
                }
                CalendarExtender2.Format = ConfigurationManager.AppSettings["DateFormat"].ToString();
                txtDate.Text = DateTime.Now.ToString(ConfigurationManager.AppSettings["DateFormat"]);
            }
        }
        public void BindGrid()
        {

            DataSet ds = AttendanceDAC.HR_GetBonus();
            gvView.DataSource = ds;
            gvView.DataBind();
        }
        public void BindDetails(int BID)
        {

            DataSet ds = AttendanceDAC.HR_GetBonusDetailsByID(BID);

            txtDate.Text = ds.Tables[0].Rows[0]["DateofWork"].ToString();
            txtBonusPercent.Text = ds.Tables[0].Rows[0]["BonusPercent"].ToString();
            txtCS1.Text = ds.Tables[0].Rows[0]["ShiftACollection"].ToString();
            txtCS2.Text = ds.Tables[0].Rows[0]["ShiftBCollection"].ToString();
            txtCS3.Text = ds.Tables[0].Rows[0]["ShiftCCollection"].ToString();
            txtTrgtS1.Text = ds.Tables[0].Rows[0]["ShiftATarget"].ToString();
            txtTrgtS2.Text = ds.Tables[0].Rows[0]["ShiftBTarget"].ToString();
            txtTrgtS3.Text = ds.Tables[0].Rows[0]["ShiftCTarget"].ToString();
            lblBS1.Text = ds.Tables[0].Rows[0]["ShiftABonus"].ToString();
            lblBS2.Text = ds.Tables[0].Rows[0]["ShiftBBonus"].ToString();
            lblBS3.Text = ds.Tables[0].Rows[0]["ShiftCBonus"].ToString();
            lblRevnuS1.Text = ds.Tables[0].Rows[0]["ShiftARevenue"].ToString();
            lblRevnuS2.Text = ds.Tables[0].Rows[0]["ShiftBRevenue"].ToString();
            lblRevnuS3.Text = ds.Tables[0].Rows[0]["ShiftCRevenue"].ToString();
        }
        protected void txtCS1_TextChanged(object sender, EventArgs e)
        {
            double Percent = Convert.ToDouble(txtBonusPercent.Text);
            double S1Target = Convert.ToDouble(txtTrgtS1.Text);
            double S1Collect = Convert.ToDouble(txtCS1.Text);
            double S1Revenue = S1Collect - S1Target;
            if (S1Revenue > 0)
            {
                lblRevnuS1.Text = S1Revenue.ToString("N2");
                lblBS1.Text = (Convert.ToDouble(lblRevnuS1.Text) * Percent / 100).ToString("N2");
            }
            else
            {
                lblRevnuS1.Text = string.Empty;
                lblBS1.Text = string.Empty;
            }
           

        }
        protected void txtCS2_TextChanged(object sender, EventArgs e)
        {
            double Percent = Convert.ToDouble(txtBonusPercent.Text);
            double S2Target = Convert.ToDouble(txtTrgtS2.Text);
            double S2Collect = Convert.ToDouble(txtCS2.Text);
            double S2Revenue = S2Collect - S2Target;
            if (S2Revenue > 0)
            {
                lblRevnuS2.Text = S2Revenue.ToString("N2");
                lblBS2.Text = (Convert.ToDouble(lblRevnuS2.Text) * Percent / 100).ToString("N2");
            }
            else
            {
                lblRevnuS2.Text = string.Empty;
                lblBS2.Text = string.Empty;
            }
        }
        protected void txtCS3_TextChanged(object sender, EventArgs e)
        {
            double Percent = Convert.ToDouble(txtBonusPercent.Text);
            double S3Target = Convert.ToDouble(txtTrgtS3.Text);
            double S3Collect = Convert.ToDouble(txtCS3.Text);
            double S3Revenue = S3Collect - S3Target;
            if (S3Revenue > 0)
            {
                lblRevnuS3.Text = S3Revenue.ToString("N2");
                lblBS3.Text = (Convert.ToDouble(lblRevnuS3.Text) * Percent / 100).ToString("N2");
            }
            else
            {
                lblRevnuS3.Text = string.Empty;
                lblBS3.Text = string.Empty;
            }
        }
        protected void txtBonusPercent_TextChanged(object sender, EventArgs e)
        {
            double Percent = Convert.ToDouble(txtBonusPercent.Text);
            if (txtCS1.Text != "" && txtCS2.Text != "" && txtCS3.Text != "")
            {
                if (lblRevnuS1.Text != "")
                {
                    lblBS1.Text = (Convert.ToDouble(lblRevnuS1.Text) * Percent / 100).ToString("N2");
                }
                else
                {
                    lblBS1.Text = "";
                }
                if (lblRevnuS2.Text != "")
                {
                    lblBS2.Text = (Convert.ToDouble(lblRevnuS2.Text) * Percent / 100).ToString("N2");
                }
                else
                {
                    lblBS2.Text = "";
                }
                if (lblRevnuS3.Text != "")
                {
                    lblBS3.Text = (Convert.ToDouble(lblRevnuS3.Text) * Percent / 100).ToString("N2");
                }
                else
                {
                    lblBS3.Text = "";
                }
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            int BID = Convert.ToInt32(ViewState["BID"]);
            DateTime Date = DateTime.Now;
            double ShiftATarget = Convert.ToDouble(txtTrgtS1.Text == "" ? "0" : txtTrgtS1.Text);
            double ShiftBTarget = Convert.ToDouble(txtTrgtS2.Text == "" ? "0" : txtTrgtS2.Text);
            double ShiftCTarget = Convert.ToDouble(txtTrgtS3.Text == "" ? "0" : txtTrgtS3.Text);
            double ShiftACollection = Convert.ToDouble(txtCS1.Text == "" ? "0" : txtCS1.Text);
            double ShiftBCollection = Convert.ToDouble(txtCS2.Text == "" ? "0" : txtCS2.Text);
            double ShiftCCollection = Convert.ToDouble(txtCS3.Text == "" ? "0" : txtCS3.Text);
            double ShiftARevenue = Convert.ToDouble(lblRevnuS1.Text == "" ? "0" : lblRevnuS1.Text);
            double ShiftBRevenue = Convert.ToDouble(lblRevnuS2.Text == "" ? "0" : lblRevnuS2.Text);
            // ddlEmp.SelectedValue = ds.Tables[0].Rows[0]["AssignerEmpID"].ToString() == "" ? "0" : ds.Tables[0].Rows[0]["AssignerEmpID"].ToString();
            double ShiftCRevenue = Convert.ToDouble(lblRevnuS3.Text == "" ? "0" : lblRevnuS3.Text);
            double BonusPercent = Convert.ToDouble(txtBonusPercent.Text);
            double ShiftABonus = Convert.ToDouble(lblBS1.Text == "" ? "0" : lblBS1.Text);
            double ShiftBBonus = Convert.ToDouble(lblBS2.Text == "" ? "0" : lblBS2.Text);
            double ShiftCBonus = Convert.ToDouble(lblBS3.Text == "" ? "0" : lblBS3.Text);
            int SubmittedBy =  Convert.ToInt32(Session["UserId"]);
            DateTime SubmittedOn = DateTime.Now;
            int UpdatedBy =  Convert.ToInt32(Session["UserId"]);
            DateTime UpdatedOn = DateTime.Now;
            AttendanceDAC.HR_InsUpBonus(BID, Date, ShiftATarget, ShiftBTarget, ShiftCTarget, ShiftACollection, ShiftBCollection, ShiftCCollection, ShiftARevenue, ShiftBRevenue, ShiftCRevenue, BonusPercent, ShiftABonus, ShiftBBonus, ShiftCBonus, SubmittedBy, SubmittedOn, UpdatedBy, UpdatedOn);
            BindGrid();
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {

            DataSet ds = AttendanceDAC.HR_getBonusDefaults();
            if (ds.Tables[0].Rows.Count != 0)
            {
                txtBonusPercent.Text = Convert.ToInt32(ds.Tables[0].Rows[0][1]).ToString();
                txtTrgtS1.Text = Convert.ToInt32(ds.Tables[0].Rows[0][2]).ToString();
                txtTrgtS2.Text = Convert.ToInt32(ds.Tables[0].Rows[0][3]).ToString();
                txtTrgtS3.Text = Convert.ToInt32(ds.Tables[0].Rows[0][4]).ToString();
            }
            CalendarExtender2.Format = ConfigurationManager.AppSettings["DateFormat"].ToString();
            txtDate.Text = DateTime.Now.ToString(ConfigurationManager.AppSettings["DateFormat"]);
            txtCS1.Text = txtCS2.Text = txtCS3.Text = "";
            lblBS1.Text = lblBS2.Text = lblBS3.Text = lblRevnuS1.Text = lblRevnuS2.Text = lblRevnuS3.Text = string.Empty;
        }
        protected void gvView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int BID = Convert.ToInt32(e.CommandArgument);
            if (e.CommandName == "view")
            {
                ViewState["BID"] = BID;
                tblView.Visible = false;
                tblAdd.Visible = true;
                BindDetails(BID);
            }
            else
            {
                AttendanceDAC.HR_DelBonus(BID);
                BindGrid();
            }
        }
        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            tblAdd.Visible = true;
            tblView.Visible = false;
            tblSettings.Visible = false;

            btnCancel_Click(sender, e);
        }
        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            tblAdd.Visible = false;
            tblView.Visible = true;
            tblSettings.Visible = false;

        }
        protected void lnkSettings_Click(object sender, EventArgs e)
        {
            tblSettings.Visible = true;
            tblAdd.Visible = false;
            tblView.Visible = false;

            DataSet ds = AttendanceDAC.HR_getBonusDefaults();
            if (ds.Tables[0].Rows.Count != 0)
            {
                txtDBonusPercent.Text = Convert.ToInt32(ds.Tables[0].Rows[0][1]).ToString();
                txtDTS1.Text = Convert.ToInt32(ds.Tables[0].Rows[0][2]).ToString();
                txtDTS2.Text = Convert.ToInt32(ds.Tables[0].Rows[0][3]).ToString();
                txtDTS3.Text = Convert.ToInt32(ds.Tables[0].Rows[0][4]).ToString();
            }

        }
        protected void btnDSave_Click(object sender, EventArgs e)
        {
            double DBPercent = Convert.ToDouble(txtDBonusPercent.Text);
            double DShftAT = Convert.ToDouble(txtDTS1.Text);
            double DShftBT = Convert.ToDouble(txtDTS2.Text);
            double DShftCT = Convert.ToDouble(txtDTS3.Text);
            AttendanceDAC.HR_InsUpDefult(DBPercent, DShftAT, DShftBT, DShftCT);
        }
    }
}
