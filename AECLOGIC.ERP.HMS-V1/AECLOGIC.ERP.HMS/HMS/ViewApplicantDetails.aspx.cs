using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using AECLOGIC.HMS.BLL;

namespace AECLOGIC.ERP.HMS
{
    public partial class ViewApplicantDetails : AECLOGIC.ERP.COMMON.WebFormMaster
    {
        int mid = 0;
        bool viewall, Editable;
        string menuname;
        string menuid;
        protected override void OnInit(EventArgs e)
        {
            ModuleID = 1;
            base.OnInit(e);
        }

        AttendanceDAC objPosting = new AttendanceDAC();
        HRCommon objHrCommon = new HRCommon();
        static string prevPage = String.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {

            Session["menuname"] = menuname;
            Session["menuid"] = menuid;
            if (!IsPostBack)
            {
                try { prevPage = Request.UrlReferrer.ToString(); }
                catch { }
               
               

             
                if (Request.QueryString != null && Request.QueryString.ToString() != string.Empty)
                {
                    int ApplicantID = Convert.ToInt32(Request.QueryString["id"].ToString());
                    BindApplicantdetails(ApplicantID);
                }

            }
        }

        public void BindApplicantdetails(int ApplicantID)
        {
          
            DataSet ds = objPosting.GetApplicantdetails(ApplicantID);
            if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
            {
                lblAppliedFor.Text = ds.Tables[0].Rows[0]["Position"].ToString();
                lblDate.Text = ds.Tables[0].Rows[0]["CreatedOn"].ToString();
                lblFname.Text = ds.Tables[0].Rows[0]["FName"].ToString();
                lblMname.Text = ds.Tables[0].Rows[0]["MName"].ToString();
                lblSurname.Text = ds.Tables[0].Rows[0]["LName"].ToString();
                lblEmailID.Text = ds.Tables[0].Rows[0]["Email"].ToString();
                lblMobileNo.Text = ds.Tables[0].Rows[0]["Mobile"].ToString();
                lblPhone.Text = ds.Tables[0].Rows[0]["Phone"].ToString();
                lblDOB.Text = ds.Tables[0].Rows[0]["DOB"].ToString();
                lblFathersName.Text = ds.Tables[0].Rows[0]["Father"].ToString();
                lblPassportNo.Text = ds.Tables[0].Rows[0]["Passport"].ToString();
                lblpdoi.Text = ds.Tables[0].Rows[0]["PDOI"].ToString();
                lblpdoip.Text = ds.Tables[0].Rows[0]["PDOIPlace"].ToString();
                lblpdoe.Text = ds.Tables[0].Rows[0]["PDOE"].ToString();
                //lblposition.Text = ds.Tables[0].Rows[0]["Position"].ToString();
                lblMaritialStatus.Text = ds.Tables[0].Rows[0]["MaritalStatus"].ToString();
                lblCurrentCTC.Text = ds.Tables[0].Rows[0]["CurrentCTC"].ToString();
                lblExpectedCTC.Text = ds.Tables[0].Rows[0]["ExpectedCTC"].ToString();
                try { ddlApplicantStatus.SelectedValue = ds.Tables[0].Rows[0]["Status"].ToString(); }
                catch { ddlApplicantStatus.SelectedValue = "1"; }
                string str = ds.Tables[0].Rows[0]["Resume"].ToString();
                if (str != "")
                    str = str.Split('/')[str.Split('/').Length - 1];
                hyperlink1.NavigateUrl = "../HMS/Resumes/" + str;
                //txtRemarks.Text = ds.Tables[0].Rows[0]["Remarks"].ToString();
                ViewState["ResStr"] = ds.Tables[0].Rows[0]["Resume"].ToString();
              

                BindEducationalDetails(ApplicantID);
                BindExperienceDetails(ApplicantID);
                BindRemarksDetails(ApplicantID);
            }
        }
        public void BindRemarksDetails(int ApplicantID)
        {
         
            DataSet ds = objPosting.GetRemarksDetails(ApplicantID);
            if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
            {
                gdvRemarks.DataSource = ds;
            }
            gdvRemarks.DataBind();
        }
        public void BindEducationalDetails(int ApplicantID)
        {

         
            DataSet ds = objPosting.GetAcademicDetails(ApplicantID, 1);
            if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
            {
                dgvQualDetails.DataSource = ds;
            }
            dgvQualDetails.DataBind();

        }
        public void BindExperienceDetails(int ApplicantID)
        {
          
            DataSet ds = objPosting.GetEduDetails(ApplicantID, 1);
            if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
            {
                dgvPreviousEmpHist.DataSource = ds;
            }
            dgvPreviousEmpHist.DataBind();
        }
       

        protected void btnRemarks_Click(object sender, EventArgs e)
        {
            try
            {
               
                int frompage = 0;
                if (Request.QueryString.Count > 1)
                {
                    if (Request.QueryString["FPage"].ToString() != "")
                    {
                        frompage = Convert.ToInt32(Request.QueryString["FPage"].ToString());
                    }

                    if ((frompage == 3 && Convert.ToInt32(ddlApplicantStatus.SelectedItem.Value) == 3) || (frompage == 3 && Convert.ToInt32(ddlApplicantStatus.SelectedItem.Value) == 1))
                    {
                        objHrCommon.AppStatus = "3";
                    }
                    else
                    {
                        objHrCommon.AppStatus = ddlApplicantStatus.SelectedItem.Value;
                    }
                }
                else
                    objHrCommon.AppStatus = ddlApplicantStatus.SelectedItem.Value;

                objHrCommon.AppID = Convert.ToInt32(Request.QueryString["id"].ToString());
                int ApplicantID = Convert.ToInt32(Request.QueryString["id"].ToString());
                objHrCommon.Remarks = txtRemarks.Text;
                objHrCommon.Ranking = Convert.ToInt32(rblRanking.SelectedValue);
                objHrCommon.UserID =  Convert.ToInt32(Session["UserId"]);
                int result = objPosting.AddApplicantStatus(objHrCommon);
                BindRemarksDetails(ApplicantID);

                Response.Redirect("ViewSelectedApplicantList.aspx");

            

            }
            catch (Exception)
            {

                throw;
            }
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
           
            Response.Redirect(prevPage);

           

        }

    }
}