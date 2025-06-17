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
using System.Data.SqlClient;
using AECLOGIC.HMS.BLL;
namespace AECLOGIC.ERP.HMS
{
    public partial class CreateApplicant : AECLOGIC.ERP.COMMON.WebFormMaster
    {

        int mid = 0;
        bool viewall;
        string menuname;
        string menuid;
        AttendanceDAC objPosting = new AttendanceDAC();
        HRCommon objHrCommon = new HRCommon();
        

        private string Resumepath = string.Empty;
        protected override void OnInit(EventArgs e)
        {
            ModuleID = 1;
            base.OnInit(e);
        }
        protected void Page_Load(object sender, EventArgs e)
        {

            Resumepath = Server.MapPath("Resumes/");
            if (!IsPostBack)
            {
                Page.Form.Attributes.Add("enctype", "multipart/form-data");
                GetParentMenuId();
               
                BindPosition();
                ViewState["AppId"] = 0;


              
                dvAcademic.Visible = false;
                dvEmployment.Visible = false;
            }
        }
        public void BindPosition()
        {
            try
            {

                 
                DataSet ds = (DataSet)objPosting.GetPositionList();
                if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ddlPosition.DataValueField = "PosID";
                    ddlPosition.DataTextField = "Position";
                    ddlPosition.DataSource = ds;
                    ddlPosition.DataBind();
                    ddlPosition.Items.Insert(0, new ListItem("--Select--", "0"));

                }

            }
            catch (Exception e)
            {
                throw e;
            }




        }

        public int GetParentMenuId()
        {
            string URL = Request.Url.Segments[Request.Url.Segments.Length - 1] + Request.Url.Query;
            int RoleId = Convert.ToInt32(Session["RoleId"].ToString());
            int ModuleId = ModuleID;;

             

            DataSet ds = AttendanceDAC.GetAllowed(RoleId, ModuleId, URL);
            int MenuId = 0;
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                MenuId = Convert.ToInt32(ds.Tables[0].Rows[0]["Under"]);
               
                btnSave.Enabled = (bool)ds.Tables[0].Rows[0]["Editable"];
            }
            return MenuId;
        }
        private void PreviousEmpDataBind()
        {
            dgvPreviousEmpHist.DataSource = objPosting.GetEduDetails(Convert.ToInt32(ViewState["AppId"]));
            dgvPreviousEmpHist.DataBind();
        }
        private void QualDetailsDataBind()
        {
            dgvQualDetails.DataSource = objPosting.GetAcademicDetails(Convert.ToInt32(ViewState["AppId"]));
            dgvQualDetails.DataBind();
        }
        protected void btnAddAcademicDetails_Click(object sender, EventArgs e)
        {
            try
            {
                objHrCommon.AppID = Convert.ToInt32(ViewState["AppId"]);
                objHrCommon.Qualification = txtQualification.Text;
                objHrCommon.Institute = txtCollegeInsUniv.Text;
                objHrCommon.YOP = Convert.ToInt32(txtYearofPass.Text);
                objHrCommon.Specialization = txtSpecialization.Text;
                objHrCommon.Percentage = Convert.ToDouble(txtPercentage.Text);
                objHrCommon.Mode = ddlFullPartTime.SelectedValue;
                objPosting.AddAcademicDetails(objHrCommon);
                dgvQualDetails.DataSource = objPosting.GetEduDetails(objHrCommon.AppID);
                dgvQualDetails.DataBind();
                txtQualification.Text = string.Empty;
                txtCollegeInsUniv.Text = string.Empty;
                txtYearofPass.Text = string.Empty;
                txtSpecialization.Text = string.Empty;
                txtPercentage.Text = string.Empty;

            }
            catch (Exception ex1)
            {
                AlertMsg.MsgBox(Page, ex1.Message.ToString(),AlertMsg.MessageType.Error);
            }

        }
        protected void btnAddEmp_Click(object sender, EventArgs e)
        {
            try
            {
                objHrCommon.AppID = Convert.ToInt32(ViewState["AppId"]);
                objHrCommon.Organization = txtOrganization.Text;
                objHrCommon.City = txtCity.Text;
                objHrCommon.Type = ddlPermanent.SelectedValue;

                if (txtDurationFrom.Text != String.Empty)
                    objHrCommon.FromDate = CODEUtility.ConvertToDate(txtDurationFrom.Text.Trim(), DateFormat.DayMonthYear);
                //objHrCommon.FromDate = CODEUtility.Convertdate(txtDurationFrom.Text);
                if (txtDurationTo.Text != String.Empty)
                    objHrCommon.ToDate = CODEUtility.ConvertToDate(txtDurationTo.Text.Trim(), DateFormat.DayMonthYear);
                //objHrCommon.ToDate = CODEUtility.Convertdate(txtDurationTo.Text);

                objHrCommon.Designation = txtDesignation.Text;
                objHrCommon.CurrentCTC = Convert.ToDouble(txtCTC.Text);
                objPosting.AddempExperience(objHrCommon);

                dgvPreviousEmpHist.DataSource = objPosting.GetAcademicDetails(objHrCommon.AppID);
                dgvPreviousEmpHist.DataBind();

                txtOrganization.Text = string.Empty;
                txtCity.Text = string.Empty;
                txtDurationFrom.Text = string.Empty;
                txtDurationTo.Text = string.Empty;
            }
            catch (Exception ex2)
            {
                AlertMsg.MsgBox(Page, ex2.Message.ToString(),AlertMsg.MessageType.Error);
            }
        }
        protected void dgvQualDetails_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Delete")
                {
                    objPosting.DelAcademicDetails(Convert.ToInt32(dgvQualDetails.DataKeys[e.Item.ItemIndex]));
                    QualDetailsDataBind();
                }
            }
            catch (Exception)
            {
                throw;
            }

        }

        public void BindPositions()
        {
            try
            {
                 
                DataSet ds = (DataSet)objPosting.GetPositionList();
                if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ddlPosition.DataValueField = "PosID";
                    ddlPosition.DataTextField = "Position";
                    ddlPosition.DataSource = ds;
                    ddlPosition.DataBind();
                    ddlPosition.Items.Insert(0, "--Select--");
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        protected void dgvPreviousEmpHist_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Delete")
                {
                    objPosting.DelEmpExperience(Convert.ToInt32(dgvPreviousEmpHist.DataKeys[e.Item.ItemIndex]));
                    PreviousEmpDataBind();
                }
            }
            catch (Exception)
            {

                throw;
            }

        }
        protected void btnFinish_Click(object sender, EventArgs e)
        {
            ViewState["AppId"] = string.Empty;
            AlertMsg.MsgBox(Page, "Done!");
            Response.Redirect("ViewApplicantList.aspx");
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (txtMobileNumber.Text == string.Empty && txtHomeNumber.Text==string.Empty)
            {
                AlertMsg.MsgBox(Page,"Please Enter Contact Number");
                txtMobileNumber.Focus();
                return;
            }
            try
            {
                objHrCommon.FName = txtFirstName.Text;
                objHrCommon.MName = txtMiddleName.Text;
                objHrCommon.LName = txtSurnname.Text;
                objHrCommon.Email = txtEmailId.Text;
                objHrCommon.Address = txtaddress.Text;
                objHrCommon.City = txtcity1.Text;
                objHrCommon.State = txtstate.Text;
                objHrCommon.Country = txtcountry.Text;
                objHrCommon.Pin = Convert.ToInt32(txtpin.Text);
                objHrCommon.Mobile = txtMobileNumber.Text;
                objHrCommon.Phone = txtHomeNumber.Text;
                objHrCommon.DOB = CODEUtility.ConvertToDate(txtDateofBirth.Text.Trim(), DateFormat.DayMonthYear);
                objHrCommon.Father = txtFathersName.Text;
                objHrCommon.Passport = txtPassportNumber.Text;
                if (txtDatePlaceIssue.Text != String.Empty)
                    objHrCommon.PDOI = CODEUtility.ConvertToDate(txtDatePlaceIssue.Text.Trim(), DateFormat.DayMonthYear);
                //objHrCommon.PDOI = CODEUtility.Convertdate(txtDatePlaceIssue.Text);

                objHrCommon.PDOIPlace = txtPlaceIssue.Text;
                if (txtDateOfExpiry.Text != String.Empty)
                    objHrCommon.PDOE = CODEUtility.ConvertToDate(txtDateOfExpiry.Text.Trim(), DateFormat.DayMonthYear);
                //objHrCommon.PDOE = CODEUtility.Convertdate(txtDateOfExpiry.Text);
                objHrCommon.PosID = Convert.ToInt32(ddlPosition.SelectedValue);

                objHrCommon.MaritalSta = Convert.ToByte(ddlMatrialStatus.SelectedValue);
                objHrCommon.CurrentCTC = Convert.ToDouble(txtCurrentCTC.Text);
                objHrCommon.ExpectedCT = Convert.ToDouble(txtExpectedCTC.Text);
                objHrCommon.CurrentLocation = txtcurrentlocation.Text;
                objHrCommon.Gender = rdblstgender.SelectedItem.Text;

                String fname = string.Empty;
                if (fuResume.FileName != null && fuResume.PostedFile.ContentLength > 0)
                {
                    fname = fuResume.PostedFile.FileName.Substring(fuResume.PostedFile.FileName.LastIndexOf('.'), fuResume.PostedFile.FileName.Length - fuResume.PostedFile.FileName.LastIndexOf('.'));

                    objHrCommon.Resume = fname;
                }

                objHrCommon.Status = true;
                objHrCommon.AppID = 0;
                ViewState["AppId"] = objPosting.AddApplicant(objHrCommon);
                String SaveLocation = Resumepath + ViewState["AppId"].ToString() + fname;
                fuResume.PostedFile.SaveAs(SaveLocation);
                dvAcademic.Visible = true;
                dvEmployment.Visible = true;
                AlertMsg.MsgBox(Page, "Submited Enter below details");
            }
            catch (Exception ex)
            {
                AlertMsg.MsgBox(Page, "Sorry for the inconvinience. Try again.\nError:" + ex.Message.ToString(),AlertMsg.MessageType.Error);
            }
        }

        protected void txtEmailId_TextChanged(object sender, EventArgs e)
        {
            string MailID = txtEmailId.Text;
             
            DataSet ds = AttendanceDAC.HR_CheckMailID(MailID);
            if (ds.Tables[0].Rows[0][0].ToString() != "0")
            {
                txtEmailId.Text = string.Empty;
                AlertMsg.MsgBox(Page, "Invalid MailID!");
            }


        }
    }

}