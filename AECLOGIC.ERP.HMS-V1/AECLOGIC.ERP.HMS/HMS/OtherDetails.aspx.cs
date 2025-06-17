using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using AECLOGIC.HMS.BLL;
using System.Collections;
using DataAccessLayer;
using SMSConfig;
using System.IO;
using System.Drawing;

namespace AECLOGIC.ERP.HMS
{
    public partial class OtherDetails : AECLOGIC.ERP.COMMON.WebFormMaster
    {
        int mid = 0;
        bool viewall;
        string menuname;
        string menuid;
        string ReturnVal = "";
        bool Editable; static string strurl = string.Empty;

        static int EmpID;
        protected override void OnInit(EventArgs e)
        {
            ModuleID = 1;
            base.OnInit(e);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
           
            ViewState["EmID"] = Request.QueryString["EmpID"];
          


            if (!IsPostBack)
            {
                if (Request.QueryString.Count > 0)
                {
                    dvEditDocs.Visible = true;
                    pnl11.Visible = false;
                    BindPassportDetails();
                    BindEmergencyDetails();
                    BindInsurenceDetails();

                }

                ViewState["PassportImg"] = "";
                strurl = Request.UrlReferrer.ToString();

                if (Request.QueryString[0].ToString() != "" && Request.QueryString[0].ToString() != "0")
                {
                    EmpID = Convert.ToInt32(Request.QueryString[0].ToString());
                  
                }
                else
                {
                    AlertMsg.MsgBox(Page, "Unable to view");
                    Response.Redirect("EmployeeList.aspx");

                }
            }
        }
     

        public void BindPassportDetails()
        {
           
            DataSet dsPassport = AttendanceDAC.GetPassportDetails(Convert.ToInt32(ViewState["EmID"]));
            if (dsPassport != null && dsPassport.Tables.Count > 0 && dsPassport.Tables[0].Rows.Count > 0)
            {
                GVPassPort.DataSource = dsPassport;

            }
            else
            {
                GVPassPort.EmptyDataText = "No Records Found";
            }
            GVPassPort.DataBind();
        }

        public void BindEmergencyDetails()
        {
            
            DataSet  dsEmergency = AttendanceDAC.GetEmergDetails(Convert.ToInt32(ViewState["EmID"]));
            if (dsEmergency != null && dsEmergency.Tables.Count > 0 && dsEmergency.Tables[0].Rows.Count > 0)
            {
                GVEmergency.DataSource = dsEmergency;
             

            }
            else
            {
                GVEmergency.EmptyDataText = "No Records Found";
            }
            GVEmergency.DataBind();

        }
        public void BindInsurenceDetails()
        {
            
            DataSet dsInsurence = AttendanceDAC.GetInsuranceDetails(Convert.ToInt32(ViewState["EmID"]));
            if (dsInsurence != null && dsInsurence.Tables.Count > 0 && dsInsurence.Tables[0].Rows.Count > 0)
            {
                GVInsurance.DataSource = dsInsurence;
            }
            else
            {
                GVInsurance.EmptyDataText = "No Records Found";
            }
            GVInsurance.DataBind();
        }
        protected void lnkdownload_img(object sender, EventArgs e)
        {
            try
            {
                string str = ViewState["PassportImg"].ToString();
                string filename = MapPath("Passport/" + EmpID + "." + str);
                Response.ContentType = "image/JPEG";
                Response.AddHeader("Content-Disposition", "attachment; filename=" + filename + "");

                Response.TransmitFile(filename);
                Response.End();
            }
            catch (Exception ex)
            {
                AlertMsg.MsgBox(Page, ex.ToString());
            }
        }

        protected void lnk_delimage(object sender, EventArgs e)
        {
            try
            {
                int i = AttendanceDAC.UPdateimg(EmpID);
                if (i == 1)
                {
                    string str = ViewState["PassportImg"].ToString();
                    string filename = MapPath("Passport/" + EmpID + "." + str);
                    FileInfo fi = new FileInfo(filename);
                    if (fi.Exists)
                        fi.Delete();
                    lnkdownload.Visible = false;
                    lnkdel.Visible = false;
                    ViewState["PassportImg"] = "";
                }

            }
            catch (Exception ex)
            {
                AlertMsg.MsgBox(Page, ex.ToString());
            }
        }
        protected void btnPassport_Click(object sender, EventArgs e)
        {
            if (txtPassportNo.Text.Trim() == "")
            {
                AlertMsg.MsgBox(Page, "Enter passport number");
                return;
            }

            if (txtIssueDate.Text.Trim() == "")
            {
                AlertMsg.MsgBox(Page, "Enter issue date");
                return;
            }
            if (txtExpiryDate.Text.Trim() == "")
            {
                AlertMsg.MsgBox(Page, "Enter expiry date");
                return;
            }



            string filename = "", ext = string.Empty, path = "";

            filename = ImgUpload.PostedFile.FileName;

            if (filename != "")
            {
                ext = filename.Split('.')[filename.Split('.').Length - 1];
            }
            else
            {
                if (EmpID != 0)
                {
                    ext = ViewState["PassportImg"].ToString();
                }
                else
                {
                    ext = "";
                }
            }

            AttendanceDAC.HR_InsUpPassportDts(EmpID, txtPassportNo.Text, txtIssuer.Text, txtIssuePlace.Text, CODEUtility.ConvertToDate(txtIssueDate.Text.Trim(), DateFormat.DayMonthYear), CODEUtility.ConvertToDate(txtExpiryDate.Text.Trim(), DateFormat.DayMonthYear), txtRemarks.Text, ext);

            if (filename != "")
            {
                if (EmpID != 0)
                {
                    path = Server.MapPath(".\\Passport\\" + EmpID + "." + ext);
                    ImgUpload.PostedFile.SaveAs(path);
                    lnkdownload.Visible = true;
                    lnkdel.Visible = true;
                }
                else
                {
                    lnkdownload.Visible = false;
                    lnkdel.Visible = false;
                }
            }

            AlertMsg.MsgBox(Page, "Done !");
            BindPassportDetails();
        }

        protected void btnEmargency_Click(object sender, EventArgs e)
        {
            if (txtContactName.Text.Trim() == "" && txtRelation.Text.Trim() == "" && txtEmail.Text.Trim() == "" && txtContactMobile.Text.Trim() == "" && txtContactPhone.Text.Trim() == "")
            {
                AlertMsg.MsgBox(Page, "Enter minimum fields");
                return;
            }

            DateTime? DateofMarriage = null;

            if (txtDateofmarriage.Text.Trim() != "")
            {

                DateofMarriage = CODEUtility.ConvertToDate(txtDateofmarriage.Text.Trim(), DateFormat.DayMonthYear);
            }

            AttendanceDAC.InsUpdEmergDetails(EmpID, txtContactMobile.Text.Trim(), txtContactName.Text.Trim(), txtRelation.Text.Trim(), txtContactPhone.Text.Trim(), DateofMarriage, txtEmail.Text.Trim(),
                                                    txtORContactMob.Text.Trim(), txtORContactName.Text.Trim(), txtORRelation.Text.Trim(), txtORContactPhone.Text.Trim(), txtOREmail.Text.Trim(), Convert.ToInt32(ddlMariSta.SelectedItem.Value)
                                                    );
            AlertMsg.MsgBox(Page, "Done !");
            BindEmergencyDetails();
        }

        protected void btnInsurancet_Click(object sender, EventArgs e)
        {
            if (txtPolicyNo.Text.Trim() == "")
            {
                AlertMsg.MsgBox(Page, "Enter policy number");
                return;
            }
            if (txtCertificateNo.Text.Trim() == "")
            {
                AlertMsg.MsgBox(Page, "Enter certificate number");
                return;
            }
            if (txtMonthlyPrem.Text.Trim() == "")
            {
                AlertMsg.MsgBox(Page, "Enter monthly premium");
                return;
            }
            if (txtInsIssueDate.Text.Trim() == "")
            {
                AlertMsg.MsgBox(Page, "Enter issue date");
                return;
            }
            if (txtInsExpDate.Text.Trim() == "")
            {
                AlertMsg.MsgBox(Page, "Enter expiry date");
                return;
            }



            string ext = string.Empty;

            

            AttendanceDAC.HR_InsUpdInsuranceDetails(EmpID, txtPolicyNo.Text.Trim(), Convert.ToDecimal(txtMonthlyPrem.Text.Trim()),
                                                    txtCertificateNo.Text.Trim(), CODEUtility.ConvertToDate(txtInsIssueDate.Text.Trim(), DateFormat.DayMonthYear),
                                                    CODEUtility.ConvertToDate(txtInsExpDate.Text.Trim(), DateFormat.DayMonthYear), txtInsRemarks.Text, ext);

            

            AlertMsg.MsgBox(Page, "Done !");
            BindInsurenceDetails();
        }

        protected void ddlMariSta_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlMariSta.SelectedItem.Value == "2")
            {
                trMarriage.Visible = true;
            }
            else
            {
                trMarriage.Visible = false;
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect(strurl);
        }

        protected void lnkEdit_Click(object sender, EventArgs e)
        {


            lnkEdit.BackColor = Color.Lavender;
            lnkAdd.BackColor = Color.White;
            pnl11.Visible = false;
            dvEditDocs.Visible = true;

        }

        protected void lnkAdd_Click(object sender, EventArgs e)
        {
            lnkAdd.BackColor = Color.Lavender;
            lnkEdit.BackColor = Color.White;
            tb.ActiveTabIndex = 0;
            pnl11.Visible = true;
            dvEditDocs.Visible = false;

        }

        protected void GVPassPort_RowCommand(object sender, GridViewCommandEventArgs e)
        {
              if (e.CommandName == "Edt")
              {
                  int EmpID = Convert.ToInt32(ViewState["EmID"]);
                  
                  DataSet  dsPassport = AttendanceDAC.GetPassportDetails(Convert.ToInt32(ViewState["EmID"]));
                  if (dsPassport != null && dsPassport.Tables.Count > 0 && dsPassport.Tables[0].Rows.Count > 0)
                  {
                      txtPassportNo.Text = dsPassport.Tables[0].Rows[0]["PassportNo"].ToString();
                      txtIssuer.Text = dsPassport.Tables[0].Rows[0]["Issuer"].ToString();
                      txtIssuePlace.Text = dsPassport.Tables[0].Rows[0]["IssuePlace"].ToString();
                      if (dsPassport.Tables[0].Rows[0]["IssueDate"].ToString() != "")
                      {
                          txtIssueDate.Text = dsPassport.Tables[0].Rows[0]["IssueDate"].ToString();
                      }
                      if (dsPassport.Tables[0].Rows[0]["ExpiryDate"].ToString() != "")
                      {
                          txtExpiryDate.Text = dsPassport.Tables[0].Rows[0]["ExpiryDate"].ToString();
                      }
                      txtRemarks.Text = dsPassport.Tables[0].Rows[0]["PasportRemarks"].ToString();

                      ViewState["PassportImg"] = dsPassport.Tables[0].Rows[0]["PassportExt"].ToString();
                      if (ViewState["PassportImg"].ToString().Trim() != "")
                      {
                          lnkdownload.Visible = true;
                          lnkdel.Visible = true;
                      }
                      else
                      {
                          lnkdownload.Visible = false;
                          lnkdel.Visible = false;
                      }
                  }
                  dvEditDocs.Visible = false;
                  pnl11.Visible = true;
                  tb.ActiveTabIndex = 0;
              }
        }

     
        protected void GVEmergency_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Edt")
            {
                int EmpID = Convert.ToInt32(ViewState["EmID"]);
                
                DataSet dsEmergency = AttendanceDAC.GetEmergDetails(EmpID);
                if (dsEmergency != null && dsEmergency.Tables.Count > 0 && dsEmergency.Tables[0].Rows.Count > 0)
                {
                    txtContactName.Text = dsEmergency.Tables[0].Rows[0]["EmergContactName"].ToString();
                    txtRelation.Text = dsEmergency.Tables[0].Rows[0]["EmergRelation"].ToString();
                    txtEmail.Text = dsEmergency.Tables[0].Rows[0]["EmergEmail"].ToString();
                    txtContactMobile.Text = dsEmergency.Tables[0].Rows[0]["EmergContact"].ToString();
                    txtContactPhone.Text = dsEmergency.Tables[0].Rows[0]["EmergResiPhone"].ToString();

                    txtORContactName.Text = dsEmergency.Tables[0].Rows[0]["OREmergContactName"].ToString();
                    txtORRelation.Text = dsEmergency.Tables[0].Rows[0]["OREmergRelation"].ToString();
                    txtOREmail.Text = dsEmergency.Tables[0].Rows[0]["OREmergEmail"].ToString();
                    txtORContactMob.Text = dsEmergency.Tables[0].Rows[0]["OREmergContact"].ToString();
                    txtORContactPhone.Text = dsEmergency.Tables[0].Rows[0]["OREmergResiPhone"].ToString();

                    if (dsEmergency.Tables[0].Rows[0]["MaritalStat"].ToString() != "")
                    {
                        ddlMariSta.SelectedValue = dsEmergency.Tables[0].Rows[0]["MaritalStat"].ToString();
                    }

                    if (dsEmergency.Tables[0].Rows[0]["MaritalStat"].ToString() == "2")
                    {
                        trMarriage.Visible = true;
                        if (dsEmergency.Tables[0].Rows[0]["Dateofmarriage"].ToString() != "")
                        {
                            txtDateofmarriage.Text = dsEmergency.Tables[0].Rows[0]["Dateofmarriage"].ToString();
                        }
                    }
                    else
                    {
                        trMarriage.Visible = false;
                    }

                    dvEditDocs.Visible = false;
                    pnl11.Visible = true;
                    tb.ActiveTabIndex = 1;

                }
            }
        }

        protected void GVInsurance_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Edt")
            {
                int EmpID = Convert.ToInt32(ViewState["EmID"]);
                
                DataSet dsInsurence = AttendanceDAC.GetInsuranceDetails(EmpID);
                if (dsInsurence != null && dsInsurence.Tables.Count > 0 && dsInsurence.Tables[0].Rows.Count > 0)
                {
                    txtPolicyNo.Text = dsInsurence.Tables[0].Rows[0]["PolicyNo"].ToString();
                    txtMonthlyPrem.Text = dsInsurence.Tables[0].Rows[0]["MonthlyPremium"].ToString();
                    txtCertificateNo.Text = dsInsurence.Tables[0].Rows[0]["CertificateNo"].ToString();
                    if (dsInsurence.Tables[0].Rows[0]["IssueDate"].ToString() != "")
                    {
                        txtInsIssueDate.Text = dsInsurence.Tables[0].Rows[0]["IssueDate"].ToString();
                    }
                    if (dsInsurence.Tables[0].Rows[0]["ExpiryDate"].ToString() != "")
                    {
                        txtInsExpDate.Text = dsInsurence.Tables[0].Rows[0]["ExpiryDate"].ToString();
                    }
                    txtInsRemarks.Text = dsInsurence.Tables[0].Rows[0]["Remarks"].ToString();

                    dvEditDocs.Visible = false;
                    pnl11.Visible = true;
                    tb.ActiveTabIndex = 2;
                }
            }
        }


    }
}