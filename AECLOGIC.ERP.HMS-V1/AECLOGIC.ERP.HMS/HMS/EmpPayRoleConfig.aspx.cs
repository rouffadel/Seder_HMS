using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using AECLOGIC.HMS.BLL;

namespace AECLOGIC.ERP.HMS
{
    public partial class EmpPayRoleConfig : AECLOGIC.ERP.COMMON.WebFormMaster
    {
        AttendanceDAC objAtt = new AttendanceDAC();
        AjaxDAL Aj = new AjaxDAL();
        int EmpId;
        int mid = 0;
        int PayType = 0;
        bool viewall;
        string menuname;
        string menuid;
        static string strurl = string.Empty;
        int EmpSalID = 0;
        Decimal EmpSal = 0;
        protected override void OnInit(EventArgs e)
        {
            ModuleID = 1;
            base.OnInit(e);
        }
        protected void Page_Load(object sender, EventArgs e)
        {

            Ajax.Utility.RegisterTypeForAjax(typeof(AjaxDAL));
            if (Request.QueryString["EmpID"] != null)
            {
                EmpId = Convert.ToInt32(Request.QueryString["EmpID"]);
                hdnEMPId.Value = EmpId.ToString();
            }
            if (Request.QueryString["ID"] != null)
            {
                EmpSalID = Convert.ToInt32(Request.QueryString["ID"]);
            }
            if (!IsPostBack)
            {
                Ajax.Utility.RegisterTypeForAjax(typeof(AjaxDAL));
                strurl = Request.UrlReferrer.ToString();
                txtFromDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                lblSal.Text = Request.QueryString["Salary"];
               
                using (DataSet dsEmpDetails = objAtt.GetEmpDetailsBYEmpID(EmpId))
                {
                    lblEmpNo.Text = dsEmpDetails.Tables[0].Rows[0]["EmpId"].ToString();
                    lblName.Text = dsEmpDetails.Tables[0].Rows[0]["Name"].ToString();
                    lblWorkSite.Text = dsEmpDetails.Tables[0].Rows[0]["WorkSite"].ToString();
                    lblCategory.Text = dsEmpDetails.Tables[0].Rows[0]["Category"].ToString();
                    lblDesignation.Text = dsEmpDetails.Tables[0].Rows[0]["Designation"].ToString();
                    lblDepartment.Text = dsEmpDetails.Tables[0].Rows[0]["Department"].ToString();
                }
                Bind();
               

            }

        }
     
        void Bind()
        {
            ListItem listItem = null;

            //cblWages
            using (DataSet ds = PayRollMgr.GetEmpWages(EmpId, EmpSalID))
            {
                grdWages.DataSource = ds;
                grdWages.DataBind();
            }
            //cblAllowences
            using (DataSet ds = PayRollMgr.GetEmpAllowancesList(EmpId, EmpSalID))
            {
                grdAllowances.DataSource = ds;
                grdAllowances.DataBind();
            }
            //cblContributions
            using (DataSet ds = PayRollMgr.GetEmpCoyContributionItemsList(EmpId, EmpSalID))
            {
                grdCContribution.DataSource = ds;
                grdCContribution.DataBind();
            }
            //cblDeductions
            using (DataSet ds = PayRollMgr.GetEmpDeductStatutoryList(EmpId, EmpSalID))
            {
                grdStatutory.DataSource = ds;
                grdStatutory.DataBind();
            }

            //cblDeductions
            using (DataSet ds = PayRollMgr.GetEmpNonCTCComponentsList(EmpId, EmpSalID))
            {
                grdNonCTCComponts.DataSource = ds;
                grdNonCTCComponts.DataBind();
            }
        }



        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect(strurl);
        }

        protected void grdWages_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton lnkEdt = (LinkButton)e.Row.FindControl("lnkEdt");
                CheckBox chkWages = (CheckBox)e.Row.FindControl("chkWages");
                TextBox txtwagepercent = (TextBox)e.Row.FindControl("txtwagepercent");
                TextBox txtCentageValue = (TextBox)e.Row.FindControl("txtCentageValue");
                txtCentageValue.Attributes.Add("OnChange", "javascript:return WagesPercentageCal(this,'" + txtwagepercent.ClientID + "','" + txtCentageValue.ClientID + "','" + lblSal.Text + "');");
                lnkEdt.Attributes.Add("onclick", "javascript:return UpdateWages(this,'" + lnkEdt.CommandArgument.ToString() + "','" + lblEmpNo.Text + "','" + chkWages.ClientID + "','" + "0" + "','" +  Convert.ToInt32(Session["UserId"]).ToString() + "','" + txtwagepercent.ClientID + "','" + EmpSalID + "');");

            }
        }

        protected void grdAllowances_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton lnkEdt = (LinkButton)e.Row.FindControl("lnkEdt");
                CheckBox chkAllowances = (CheckBox)e.Row.FindControl("chkAllowances");
                TextBox txtAllowancepercent = (TextBox)e.Row.FindControl("txtAllowancepercent");
                TextBox txtCentageValue = (TextBox)e.Row.FindControl("txtCentageValue");

                CheckBox chkAllowancesIT = (CheckBox)e.Row.FindControl("chkAllowancesIT");


                txtCentageValue.Attributes.Add("OnChange", "javascript:return AllowancesPercentageCal(this,'" + lnkEdt.CommandArgument.ToString() + "','" + txtAllowancepercent.ClientID + "','" + txtCentageValue.ClientID + "','" + lblSal.Text + "','" + lblEmpNo.Text + "','" + EmpSalID + "');");
                lnkEdt.Attributes.Add("onclick", "javascript:return UpdateAllowances(this,'" + lnkEdt.CommandArgument.ToString() + "','" + lblEmpNo.Text + "','" + chkAllowances.ClientID + "','" + "0" + "','" +  Convert.ToInt32(Session["UserId"]).ToString() + "','" + txtAllowancepercent.ClientID + "','" + EmpSalID + "','" + chkAllowancesIT.ClientID + "');");
            }
        }

        protected void grdCContribution_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton lnkEdt = (LinkButton)e.Row.FindControl("lnkEdt");
                CheckBox chkCContribution = (CheckBox)e.Row.FindControl("chkCContribution");
                TextBox txtCContributionpercent = (TextBox)e.Row.FindControl("txtCContributionpercent");
                TextBox txtCentageValue = (TextBox)e.Row.FindControl("txtCentageValue");

                CheckBox chkCContributionIT = (CheckBox)e.Row.FindControl("chkCContributionIT");

                txtCentageValue.Attributes.Add("OnChange", "javascript:return ContrPercentageCal(this,'" + lnkEdt.CommandArgument.ToString() + "','" + txtCContributionpercent.ClientID + "','" + txtCentageValue.ClientID + "','" + lblSal.Text + "','" + lblEmpNo.Text + "','" + EmpSalID + "');");
                lnkEdt.Attributes.Add("onclick", "javascript:return UpdateEmpContribution(this,'" + lnkEdt.CommandArgument.ToString() + "','" + lblEmpNo.Text + "','" + chkCContribution.ClientID + "','" + "0" + "','" +  Convert.ToInt32(Session["UserId"]).ToString() + "','" + txtCContributionpercent.ClientID + "','" + EmpSalID + "','" + chkCContributionIT.ClientID + "');");
            }
        }

        protected void grdStatutory_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton lnkEdt = (LinkButton)e.Row.FindControl("lnkEdt");

                CheckBox chkDStatutory = (CheckBox)e.Row.FindControl("chkDStatutory");
                TextBox txtDStatutorypercent = (TextBox)e.Row.FindControl("txtDStatutorypercent");
                TextBox txtCentageValue = (TextBox)e.Row.FindControl("txtCentageValue");

                CheckBox chkDStatutoryIT = (CheckBox)e.Row.FindControl("chkDStatutoryIT");

                if (lnkEdt.CommandArgument.ToString() == "9")
                {
                    txtDStatutorypercent.Enabled = false;
                    txtCentageValue.Enabled = false;
                }
                txtCentageValue.Attributes.Add("OnChange", "javascript:return DedudPercentageCal(this,'" + lnkEdt.CommandArgument.ToString() + "','" + txtDStatutorypercent.ClientID + "','" + txtCentageValue.ClientID + "','" + lblSal.Text + "','" + lblEmpNo.Text + "','" + EmpSalID + "');");
                lnkEdt.Attributes.Add("onclick", "javascript:return UpdateEmpDeductions(this,'" + lnkEdt.CommandArgument.ToString() + "','" + lblEmpNo.Text + "','" + chkDStatutory.ClientID + "','" + "0" + "','" +  Convert.ToInt32(Session["UserId"]).ToString() + "','" + txtDStatutorypercent.ClientID + "','" + EmpSalID + "','" + chkDStatutoryIT.ClientID + "');");
            }
        }

        protected void grdNonCTCComponts_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton lnkEdt = (LinkButton)e.Row.FindControl("lnkEdt");

                CheckBox chkComp = (CheckBox)e.Row.FindControl("chkNonCTCComp");
                TextBox txtCentageValue = (TextBox)e.Row.FindControl("txtCentageValue");

                lnkEdt.Attributes.Add("onclick", "javascript:return UpdateEmpNonCTCComp(this,'" + lnkEdt.CommandArgument.ToString() + "','" + lblEmpNo.Text + "','" + chkComp.ClientID + "','" + txtCentageValue.ClientID + "','" + EmpSalID + "');");
            }

        }

    }
}