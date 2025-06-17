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
using System.Collections.Generic;
using AECLOGIC.HMS.BLL;
using SMSConfig;
using System.Web.Mail;
using System.Net.Mime;
using DataAccessLayer;
using System.IO;
using AECLOGIC.ERP.HMS;
using AECLOGIC.ERP.HMS.HRObjects;
namespace AECLOGIC.ERP.HMS
{
    public partial class CreateNewEmployee : AECLOGIC.ERP.COMMON.WebFormMaster
    {
        bool Editable = false; bool ViewAll = false;
        AttendanceDAC objAtt = new AttendanceDAC();
        HRCommon obj = new HRCommon();
        HRCommon objHrCommon = new HRCommon();
       // DataSet ds = new DataSet();
        AttendanceDAC objRights = new AttendanceDAC();
        int mid = 0; bool viewall; string menuname; string menuid;
        string Vpath, VthumbPath;
        private string siteurl = System.Configuration.ConfigurationManager.AppSettings["SiteUrl"].ToString();
        string RegEmailID = System.Configuration.ConfigurationManager.AppSettings["RegEmailID"].ToString();
        string SMTPServer = System.Configuration.ConfigurationManager.AppSettings["SMTPServer"].ToString();
        string WebSiteID = System.Configuration.ConfigurationManager.AppSettings["WebSiteID"].ToString();
        string Company = System.Configuration.ConfigurationManager.AppSettings["Company"].ToString();
        string SMSUserID = System.Configuration.ConfigurationManager.AppSettings["SMSUserID"].ToString();
        string SMSPassword = System.Configuration.ConfigurationManager.AppSettings["SMSPassword"].ToString();
        string City = System.Configuration.ConfigurationManager.AppSettings["City"].ToString();
        string State = System.Configuration.ConfigurationManager.AppSettings["State"].ToString();
        string Country = System.Configuration.ConfigurationManager.AppSettings["Country"].ToString();
        AttendanceDAC objApp = new AttendanceDAC();
        protected override void OnInit(EventArgs e)
        {
            ModuleID = 1;
            base.OnInit(e);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.CacheControl = "no-cache";
            Response.AddHeader("Pragma", "no-cache");
            Response.Expires = -1;

            chkAddress.Attributes.Add("onclick", "javascript:return CheckAddress();");
            txtDepndAge.Attributes.Add("onchange", "javascript:return FDOB();");
            txtDOB.Attributes.Add("onchange", "javascript:return DOB();");

            if (!IsPostBack)
            {
                GetParentMenuId();
                try
                {

                    chkPWD.Checked = true;
                    lblDomain.Value = ConfigurationManager.AppSettings["DomainName"];
                    Session["FD"] = null; ViewState["EID"] = "";
                    ViewState["dsFam"] = ""; ViewState["index"] = "";
                    trDepdAge.Visible = trDepdBGrp.Visible = trDepdGender.Visible = trDepdName.Visible = false;
                    txtDOB.Text = "01/01/1981";
                    txtDoj.Text = DateTime.Now.ToString(ConfigurationManager.AppSettings["DateFormat"]);
                    int RoleID = Convert.ToInt32(Session["RoleId"].ToString());
                    int ModuleId = ModuleID;;
                    string Url = Request.Url.Segments[Request.Url.Segments.Length - 1] + Request.Url.Query;

                    BindCategories();
                    BindDesignations();
                    BindWorkSites();
                    BindDepartments();
                    BindWorkSiteMangers();
                    Employeenature();
                    BindBanks();
                    BindLocations();

                    FIllObject.FillDropDown(ref ddlCountry, "PM_Country");


                    ddlBranch.Items.Insert(0, new ListItem("--Select--", "0"));

                    BindStates(1);


                    ddlEmpnature.SelectedValue = "1";

                    ddlShift.SelectedValue = "1";

                    ViewState["EmpID"] = 0;

                    ViewState["Edt"] = 0;
                    ViewState["CityID"] = 0;
                    if (Request.QueryString.Count > 0)
                    {
                        int Id = int.Parse(Request.QueryString["Id"].ToString());
                        ViewState["EmpID"] = Id;
                        BindEmployeeDetails(Id);
                        BindEmployeeAddressDetails(Id);
                    }
                    else
                    {
                        txtCity.Text = City;
                        txtState.Text = State;
                        txtCountry.Text = Country;
                        txtPer_City.Text = City;
                        txtPer_State.Text = State;
                        txtPer_Country.Text = Country;
                    }
                }
                catch { AlertMsg.MsgBox(Page, "Unable to bind some default Values..!"); }
                txtName.Focus();

            }

            string filename = "", ext = "";


            if (fudPhoto.HasFile)
            {
                filename = fudPhoto.PostedFile.FileName;
                ViewState["filename"] = filename;
                ext = filename.Split('.')[filename.Split('.').Length - 1];
                ViewState["ext"] = ext;
                filename = Server.MapPath(".\\EmpImages\\" + filename);
                fudPhoto.PostedFile.SaveAs(filename);
            }



            tblNewLocation.Style.Remove("display");

            if (DdlLocation.SelectedValue == "-1")
                tblNewLocation.Style.Add("display", "block");
            else
                tblNewLocation.Style.Add("display", "none");


        }

        private void BindStates(int CountryId)
        {
            try
            {
                SqlParameter[] parms = new SqlParameter[1];
                parms[0] = new SqlParameter("@CountryId", CountryId);

                FIllObject.FillDropDown(ref DdlState, "PM_State", parms);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void BindBanks()
        {
            DataSet ds = AttendanceDAC.HR_GetBanks();
            ddlBank.DataSource = ds;
            ddlBank.DataValueField = "BankID";
            ddlBank.DataTextField = "BankName";
            ddlBank.DataBind();
            ddlBank.Items.Insert(0, new ListItem("--Select--", "0"));
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
                btnSubmit.Enabled = (bool)ds.Tables[0].Rows[0]["Editable"];
                btnSubmit.Enabled = (bool)ds.Tables[0].Rows[0]["Editable"];
            }

            return MenuId;
        }

        private void Employeenature()
        {
             
            DataSet ds = Leaves.GetEmpNatureList(1);
            ddlEmpnature.DataSource = ds.Tables[0];
            ddlEmpnature.DataTextField = "Nature";
            ddlEmpnature.DataValueField = "NatureOfEmp";
            ddlEmpnature.DataBind();
            ddlEmpnature.Items.Insert(0, new ListItem("---Select---", "0", true));
        }

        private void BindEmployeeAddressDetails(int Id)
        {
            DataSet dsApp = objAtt.GetAppDetails(Id);
            if (dsApp.Tables.Count > 0)
            {
                if (dsApp.Tables[0].Rows.Count > 0)
                {
                    if (dsApp.Tables[0].Rows[0]["bloodgroup"].ToString() != "")
                    {
                        {
                            ddlBldGrp.SelectedItem.Text = dsApp.Tables[0].Rows[0]["bloodgroup"].ToString();
                        }
                    }
                    else
                    {
                        ddlBldGrp.SelectedIndex = 0;
                    }



                    txtDoj.Text = Convert.ToDateTime(dsApp.Tables[0].Rows[0]["doj"]).ToString(ConfigurationManager.AppSettings["DateFormat"]);
                    txtSal.Text = dsApp.Tables[0].Rows[0]["salary"].ToString();

                    //Present Address
                    txtAddress.Text = dsApp.Tables[0].Rows[0]["ResAddress"].ToString().Replace("<br/>", "\n");
                    txtCity.Text = dsApp.Tables[0].Rows[0]["ResCity"].ToString();
                    txtState.Text = dsApp.Tables[0].Rows[0]["ResState"].ToString();
                    txtCountry.Text = dsApp.Tables[0].Rows[0]["ResCountry"].ToString();
                    txtPIN.Text = dsApp.Tables[0].Rows[0]["ResPin"].ToString();
                    txtPhone.Text = dsApp.Tables[0].Rows[0]["ResPhone"].ToString();
                    txtMailId.Text = dsApp.Tables[0].Rows[0]["Mailid"].ToString();

                    ddlManager.SelectedValue = dsApp.Tables[0].Rows[0]["Mgnr"].ToString();


                    //Permenent Address
                    if (dsApp.Tables[0].Rows[0]["sameaddress"].ToString() == "True")
                    {
                        chkAddress.Checked = true;
                        if (chkAddress.Checked == true)
                        {
                            txtPer_Address.Text = dsApp.Tables[0].Rows[0]["ResAddress"].ToString().Replace("<br/>", "\n");
                            txtPer_PIN.Text = dsApp.Tables[0].Rows[0]["ResPin"].ToString();
                            txtPer_Phone.Text = dsApp.Tables[0].Rows[0]["ResPhone"].ToString();
                            txtPer_City.Text = dsApp.Tables[0].Rows[0]["ResCity"].ToString();
                            txtPer_State.Text = dsApp.Tables[0].Rows[0]["ResState"].ToString();
                            txtPer_Country.Text = dsApp.Tables[0].Rows[0]["ResCountry"].ToString();
                        }
                        txtPer_Address.Enabled = false;
                        txtPer_City.Enabled = false;
                        txtPer_State.Enabled = false;
                        txtPer_Country.Enabled = false;
                        txtPer_PIN.Enabled = false;
                        txtPer_Phone.Enabled = false;
                    }
                    else
                    {
                        chkAddress.Checked = false;
                        txtPer_Address.Text = dsApp.Tables[0].Rows[0]["PerAddress"].ToString().Replace("<br/>", "\n");
                        txtPer_City.Text = dsApp.Tables[0].Rows[0]["PerCity"].ToString();
                        txtPer_State.Text = dsApp.Tables[0].Rows[0]["PerState"].ToString();
                        txtPer_Country.Text = dsApp.Tables[0].Rows[0]["PerCountry"].ToString();
                        txtPer_PIN.Text = dsApp.Tables[0].Rows[0]["PerPin"].ToString();
                        txtPer_Phone.Text = dsApp.Tables[0].Rows[0]["PerPhone"].ToString();
                    }


                }
            }
        }

        private void BindEmp(int AppID)
        {
            DataSet ds3 = objAtt.GetAppOfferDetails(AppID);
            if (ds3 != null && ds3.Tables.Count != 0 && ds3.Tables[0].Rows.Count > 0)
            {


                objHrCommon.AppID = AppID;
                objHrCommon.DeptID = Convert.ToDouble(ds3.Tables[0].Rows[0]["DeptID"].ToString());
                txtName.Text = ds3.Tables[0].Rows[0]["FName"].ToString();
                txtMName.Text = ds3.Tables[0].Rows[0]["MName"].ToString();
                txtLName.Text = ds3.Tables[0].Rows[0]["LName"].ToString();
                txtUsername.Enabled = false;
                txtPassword.Enabled = false;
                txtReenterPassword.Enabled = false;
               
                txtDoj.Text = Convert.ToDateTime(ds3.Tables[0].Rows[0]["ReqDOJ"]).ToString(ConfigurationManager.AppSettings["DateFormat"]);
                txtSal.Text = ds3.Tables[0].Rows[0]["Salary"].ToString();

                //Present Address
                txtAddress.Text = ds3.Tables[0].Rows[0]["Address"].ToString().Replace("<br/>", "\n");
                txtCity.Text = ds3.Tables[0].Rows[0]["City"].ToString();
                txtState.Text = ds3.Tables[0].Rows[0]["State"].ToString();
                txtCountry.Text = ds3.Tables[0].Rows[0]["Country"].ToString();
                txtPIN.Text = ds3.Tables[0].Rows[0]["pin"].ToString();
                txtPhone.Text = ds3.Tables[0].Rows[0]["Phone"].ToString();
                txtMobile1.Text = ds3.Tables[0].Rows[0]["Mobile"].ToString();
                txtMailId.Text = ds3.Tables[0].Rows[0]["Email"].ToString();


                txtDOB.Text = Convert.ToDateTime(ds3.Tables[0].Rows[0]["DOB"]).ToString(ConfigurationManager.AppSettings["DateFormat"]);
               
            }
        }

        private void BindEmployeeDetails(int Id)
        {
            ViewState["EID"] = Id;
            DataSet ds1 = objAtt.GetEmployeeDetails(Id);
            //Personal Details
            txtName.Text = ds1.Tables[0].Rows[0]["FName"].ToString();
            txtMName.Text = ds1.Tables[0].Rows[0]["MName"].ToString();
            txtLName.Text = ds1.Tables[0].Rows[0]["LName"].ToString();
            txtAccNo.Text = ds1.Tables[0].Rows[0]["AccountNumber"].ToString();
            txtUsername.Text = ds1.Tables[0].Rows[0]["UserName"].ToString();
            txtPassword.Text = ds1.Tables[0].Rows[0]["Password"].ToString();
            txtDOB.Text = Convert.ToDateTime(ds1.Tables[0].Rows[0]["DOB"]).ToString(ConfigurationManager.AppSettings["DateFormat"]);
            txtMobile1.Text = ds1.Tables[0].Rows[0]["Mobile1"].ToString();
            txtMobile2.Text = ds1.Tables[0].Rows[0]["Mobile2"].ToString();
            txtskypeid.Text = ds1.Tables[0].Rows[0]["skypeid"].ToString();
            txtMailId.Text = ds1.Tables[0].Rows[0]["Mailid"].ToString();
            txtMole1.Text = ds1.Tables[0].Rows[0]["Mole1"].ToString();
            txtMole2.Text = ds1.Tables[0].Rows[0]["Mole2"].ToString();
            txtPan.Text = ds1.Tables[0].Rows[0]["PANNumber"].ToString();
            DdlLocation.SelectedValue = ds1.Tables[0].Rows[0]["POB"].ToString();
            txtOldEmpID.Text = ds1.Tables[0].Rows[0]["OldEmpID"].ToString();
            txtQualifcation.Text = ds1.Tables[0].Rows[0]["Qualification"].ToString();
            ddlWorksite.SelectedValue = ds1.Tables[0].Rows[0]["Categary"].ToString();
            ddldept.SelectedValue = ds1.Tables[0].Rows[0]["DeptNo"].ToString();
           
            DataSet dsFamily = AttendanceDAC.HR_GetFamilyDetails(Id);

            ViewState["Edt"] = 0;
            if (dsFamily.Tables[0].Rows.Count > 0)
            {
                ViewState["Edt"] = 1;

                gvFamily.DataSource = dsFamily;
                gvFamily.DataBind();

                FamilyDetails FDetails = new FamilyDetails();
                foreach (DataRow drRow in dsFamily.Tables[0].Rows)
                {

                    FDetails.dtFamily.AdddtFamilyRow(Convert.ToInt32(drRow["RID"]), drRow["Name"].ToString(), drRow["DOB"].ToString(), drRow["Gender"].ToString(),
                        drRow["BloodGroup"].ToString(), drRow["Relation"].ToString(), Convert.ToInt32(drRow["EmpID"]));
                }

                Session["FD"] = (FamilyDetails)FDetails;
            }
            if (ds1.Tables[0].Rows[0]["BankID"].ToString() != "")
            {
                try
                {
                    ddlBank.SelectedValue = ds1.Tables[0].Rows[0]["BankID"].ToString();
                    DataSet ds = AttendanceDAC.HR_GetBankBranches(Convert.ToInt32(ds1.Tables[0].Rows[0]["BankID"]));
                    ddlBranch.DataSource = ds;
                    ddlBranch.DataValueField = "BranchID";
                    ddlBranch.DataTextField = "BranchName";
                    ddlBranch.DataBind();
                }
                catch { }

            }
            if (ds1.Tables[0].Rows[0]["BranchID"].ToString() != "")
            {
                try
                {
                    ddlBranch.SelectedValue = ds1.Tables[0].Rows[0]["BranchID"].ToString();
                }
                catch { }
            }



            if (txtUsername.Text == "" || txtUsername.Text == string.Empty)
            {
                txtUsername.Enabled = true;
                txtPassword.Enabled = true;
                txtReenterPassword.Enabled = true;
            }
            else
            {
                txtUsername.Enabled = false;
                txtPassword.Enabled = false;
                txtReenterPassword.Enabled = false;
            }
            txtAltvMail.Text = ds1.Tables[0].Rows[0]["AltMail"].ToString();
            imgEmp.Visible = true;
            imgEmp.ImageUrl = ".\\EmpImages\\" + Id + "." + ds1.Tables[0].Rows[0]["Image"].ToString();
            //Job Details
            ddlDesignation.SelectedValue = ds1.Tables[0].Rows[0]["DesgID"].ToString();
            ddlCategory.SelectedValue = ds1.Tables[0].Rows[0]["CateId"].ToString();
            ddlEmpType.SelectedValue = ds1.Tables[0].Rows[0]["Type"].ToString();
            ddlWorksite.SelectedValue = ds1.Tables[0].Rows[0]["Categary"].ToString();
            ddldept.SelectedValue = ds1.Tables[0].Rows[0]["DeptNo"].ToString();
            ddlEmpnature.SelectedValue = ds1.Tables[0].Rows[0]["EmpNature"].ToString();
            ddlShift.SelectedValue = ds1.Tables[0].Rows[0]["Shift"].ToString();
            rdblstgender.SelectedValue = ds1.Tables[0].Rows[0]["Gender"].ToString();
            // GetManagers();
            int? ReptEmpID = null;
            ReptEmpID = Id;
            BindDeptHeadForReporting(ReptEmpID);



            if (ds1.Tables[0].Rows[0]["Mgnr"].ToString() != "")
            {
                if (ddlManager.Items.FindByValue(ds1.Tables[0].Rows[0]["Mgnr"].ToString()) != null)
                {
                    ddlManager.SelectedValue = ds1.Tables[0].Rows[0]["Mgnr"].ToString();
                }
                else
                {


                }
            }
            else
            {
                ddlManager.Enabled = true;       
            }

            chkPWD.Checked = false;
            if (chkPWD.Checked)
            {
                txtUsername.Enabled = true;
                txtPassword.Enabled = true;
                txtReenterPassword.Enabled = true;
            }
            else
            {
                txtUsername.Enabled = false;
                txtPassword.Enabled = false;
                txtReenterPassword.Enabled = false;
            }

        }

        private void BindWorkSites()
        {
            

            DataSet ds = AttendanceDAC.GetWorkSite(0, '1', Convert.ToInt32(Session["CompanyID"]));
            ddlWorksite.DataSource = ds.Tables[0];
            ddlWorksite.DataTextField = "Site_Name";
            ddlWorksite.DataValueField = "Site_ID";
            ddlWorksite.DataBind();
            ddlWorksite.Items.Insert(0, new ListItem("---Select---", "0", true));
        }



        private void BindWorkSiteMangers()
        {
            int EmpID = 0;
            if (Request.QueryString.Count > 0)
                EmpID = int.Parse(Request.QueryString["Id"].ToString());
            int Type = Convert.ToInt32(ddlEmpType.SelectedValue);
            int SiteID = Convert.ToInt32(ddlWorksite.SelectedValue);
            int DeptID = Convert.ToInt32(ddldept.SelectedValue);
            DataSet dsMan = objAtt.GetMgnr(SiteID, DeptID, Type, EmpID, Convert.ToInt32(Session["CompanyID"]));
            if (dsMan.Tables[0].Rows.Count > 0)
            {
                ddlManager.Items.Insert(0, new ListItem("Department Head", dsMan.Tables[0].Rows[0][0].ToString(), true));
            }
            else
            {
                ddlManager.Items.Insert(0, new ListItem("Department Head", "-1", true));
            }

            ddlManager.DataSource = dsMan.Tables[0];
            ddlManager.DataTextField = "Name";
            ddlManager.DataValueField = "EmpID";
            ddlManager.DataBind();
            ddlManager.Items.Insert(0, new ListItem("---Select---", "0", true));
        }

        public void BindDeptHeadForReporting(int? EmpID)
        {
            BindWorkSiteMangers();

        }

        private void BindDepartments()
        {
            DataSet ds = objAtt.GetDaprtmentList();

            DataTable dt = ds.Tables[0];

            foreach (DataRow dr in dt.Rows)
            {
                ddldept.Items.Add(new ListItem(dr["DeptName"].ToString(), dr["DepartmentUId"].ToString()));
            }
            ddldept.Items.Insert(0, new ListItem("---Select---", "0"));

        }

        private void BindDesignations()
        {
            DataSet ds = objAtt.GetDesignations();
            DataTable dt = ds.Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                ddlDesignation.Items.Add(new ListItem(dr["Designation"].ToString(), dr["DesigId"].ToString()));
            }
            ddlDesignation.Items.Insert(0, new ListItem("---Select---", "0"));
        }

        private void BindCategories()
        {
            DataSet ds = objAtt.GetCategories();
            DataTable dt = ds.Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                ddlCategory.Items.Add(new ListItem(dr["Category"].ToString(), dr["CateId"].ToString()));
            }
            ddlCategory.Items.Insert(0, new ListItem("---Select---", "0"));
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {

                int EmpID = Convert.ToInt32(ViewState["EmpID"]);
                int UserID = Convert.ToInt32( Convert.ToInt32(Session["UserId"]).ToString());
                try
                {
                    string filename = "", ext = "", path = "", ThumbPath = "";

                    if (int.Parse(ddldept.SelectedValue) <= 0)
                    {
                        AlertMsg.MsgBox(Page, "Select Department", AlertMsg.MessageType.Warning);
                        return;
                    }
                    ext = Convert.ToString(ViewState["ext"]);

                    int empType = Convert.ToInt32(ddlEmpType.SelectedValue);
                    SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings["strConn"]);
                    cn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "New_Edit_Employee";

                    SqlParameter p1 = new SqlParameter("@DeptNo", int.Parse(ddldept.SelectedValue));
                    cmd.Parameters.Add(p1);

                    SqlParameter p2 = new SqlParameter("@FName", txtName.Text);
                    cmd.Parameters.Add(p2);

                    string MName = null;
                    if (txtMName.Text != "")
                    {
                        MName = txtMName.Text;
                    }

                    SqlParameter p18 = new SqlParameter("@MName", MName);
                    cmd.Parameters.Add(p18);

                    SqlParameter p19 = new SqlParameter("@LName", txtLName.Text);
                    cmd.Parameters.Add(p19);

                    SqlParameter p3 = new SqlParameter("@UserName", txtUsername.Text);
                    cmd.Parameters.Add(p3);

                    SqlParameter p7 = new SqlParameter("@Password", FormsAuthentication.HashPasswordForStoringInConfigFile(txtPassword.Text, "MD5"));
                    cmd.Parameters.Add(p7);

                    SqlParameter p4 = new SqlParameter("@DesgID", ddlDesignation.SelectedValue);
                    cmd.Parameters.Add(p4);

                    SqlParameter pCate = new SqlParameter("@CateId", ddlCategory.SelectedValue);
                    cmd.Parameters.Add(pCate);

                    SqlParameter p5 = new SqlParameter("@Mobile1", txtMobile1.Text);
                    cmd.Parameters.Add(p5);

                    SqlParameter p6 = new SqlParameter("@Mobile2", txtMobile2.Text);
                    cmd.Parameters.Add(p6);

                    SqlParameter p8 = new SqlParameter("@Type", int.Parse(ddlEmpType.SelectedValue));
                    cmd.Parameters.Add(p8);

                    SqlParameter p9 = new SqlParameter("@Mailid", txtMailId.Text);
                    cmd.Parameters.Add(p9);

                    SqlParameter p20 = new SqlParameter("@AltMail", txtAltvMail.Text);
                    cmd.Parameters.Add(p20);

                    SqlParameter p10 = new SqlParameter("@skypeid", txtskypeid.Text);
                    cmd.Parameters.Add(p10);

                    SqlParameter p13 = new SqlParameter("@Categary", Convert.ToInt32(ddlWorksite.SelectedValue));
                    cmd.Parameters.Add(p13);
                    try
                    {
                        SqlParameter p14 = new SqlParameter("@Mgnr", Convert.ToInt32(ddlManager.SelectedValue));
                        cmd.Parameters.Add(p14);
                    }
                    catch
                    {
                        SqlParameter p14 = new SqlParameter("@Mgnr", -1);
                        cmd.Parameters.Add(p14);
                    }

                    SqlParameter p15 = new SqlParameter("@Image", ext);
                    cmd.Parameters.Add(p15);

                    SqlParameter p26 = new SqlParameter("@Gender", Convert.ToChar(rdblstgender.SelectedValue));
                    cmd.Parameters.Add(p26);

                    SqlParameter p27 = new SqlParameter("@AccountNumber", txtAccNo.Text);
                    cmd.Parameters.Add(p27);

                    SqlParameter p28 = new SqlParameter("@Shift", Convert.ToInt32(ddlShift.SelectedValue));
                    cmd.Parameters.Add(p28);

                    SqlParameter p29 = new SqlParameter("@BankID", Convert.ToInt32(ddlBank.SelectedValue));
                    cmd.Parameters.Add(p29);
                    try
                    {
                        SqlParameter p30 = new SqlParameter("@BranchID", Convert.ToInt32(ddlBranch.SelectedValue));
                        cmd.Parameters.Add(p30);
                    }
                    catch
                    {
                        SqlParameter p30 = new SqlParameter("@BranchID", 0);
                        cmd.Parameters.Add(p30);
                    }
                    SqlParameter p31 = new SqlParameter("@PanNumber", txtPan.Text);
                    cmd.Parameters.Add(p31);

                    SqlParameter p32 = new SqlParameter("@OldEmpID", txtOldEmpID.Text);
                    cmd.Parameters.Add(p32);

                    SqlParameter p33 = new SqlParameter("@POB", Convert.ToInt32(DdlLocation.SelectedValue));
                    cmd.Parameters.Add(p33);

                    SqlParameter p34 = new SqlParameter("@Qualification", txtQualifcation.Text);
                    cmd.Parameters.Add(p34);

                    int Empid = 0;
                    int status_id = 1;
                    if (Request.QueryString.Count > 0)
                    {
                        try
                        {
                            int AppID = int.Parse(Request.QueryString["AppID"].ToString());
                        }
                        catch
                        {
                            Empid = int.Parse(Request.QueryString["Id"].ToString());
                            status_id = 2;
                        }
                    }
                    SqlParameter p12 = new SqlParameter("@EmpId", Empid);
                    cmd.Parameters.Add(p12);

                    SqlParameter p11 = new SqlParameter("@id", status_id);
                    cmd.Parameters.Add(p11);

                    SqlParameter p16 = new SqlParameter("@Eid", SqlDbType.Int);
                    p16.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(p16);

                    SqlParameter p17 = new SqlParameter("@EmpType", empType);
                    cmd.Parameters.Add(p17);
                    SqlParameter p21 = new SqlParameter("@DOB", CODEUtility.ConvertToDate(txtDOB.Text.Trim(), DateFormat.DayMonthYear)); //Convertdate(txtDOB.Text.ToString())
                    cmd.Parameters.Add(p21);

                    SqlParameter p22 = new SqlParameter("@Mole1", txtMole1.Text);
                    cmd.Parameters.Add(p22);

                    SqlParameter p23 = new SqlParameter("@Mole2", txtMole2.Text);
                    cmd.Parameters.Add(p23);

                    SqlParameter p24 = new SqlParameter("@UserID", UserID);
                    cmd.Parameters.Add(p24);

                    SqlParameter p25 = new SqlParameter("@EmpNature", int.Parse(ddlEmpnature.SelectedValue));
                    cmd.Parameters.Add(p25);

                    int n = cmd.ExecuteNonQuery();
                    if (n > 0)
                    {
                        AttendanceDAC objAtt = new AttendanceDAC();

                        if (Request.QueryString.Count > 0)
                        {
                            if (ext != "")
                            {
                                Vpath = Server.MapPath(".\\EmpImages\\" + ViewState["filename"].ToString());
                                VthumbPath = Server.MapPath(".\\EmpImages\\" + ViewState["filename"].ToString());

                                path = Server.MapPath(".\\EmpImages\\" + p12.Value + "." + ext);
                                ThumbPath = Server.MapPath(".\\EmpImages\\" + p12.Value + "_thumb." + ext);
                                if (path != "")
                                {
                                    string Vf = Path.GetFileName(path);
                                    System.IO.File.Delete(path);
                                }
                                string Vfilename = Path.GetFileName(Vpath);
                                Vfilename.Replace(Vfilename, path);
                                System.IO.File.Move(Vpath, path);

                                PicUtil.ConvertPic(path, 128, 160, path);
                                PicUtil.ConvertPic(path, 19, 24, ThumbPath);
                                Vpath = null;
                                path = null;

                                try
                                {
                                    imgEmp.ImageUrl = ".\\EmpImages\\" + p12.Value + "." + ext;
                                }
                                catch (Exception ex)
                                {
                                    throw ex;
                                }
                            }

                            if (txtPIN.Text.Length >= 6 || txtPer_PIN.Text.Length >= 6 || txtPIN.Text.Length == 0)
                            {
                                objAtt.UpdateAppDetails(Convert.ToInt32(p12.Value), txtAddress.Text.Replace("\n", "<br/>"), txtCity.Text, txtState.Text, txtCountry.Text, txtPhone.Text, txtPIN.Text, chkAddress.Checked, txtPer_Address.Text.Replace("\n", "<br/>"), txtPer_City.Text, txtPer_State.Text, txtPer_Country.Text, txtPer_Phone.Text, txtPer_PIN.Text, ddlBldGrp.SelectedItem.Text, Convert.ToInt32(txtSal.Text), CODEUtility.ConvertToDate(txtDoj.Text.Trim(), DateFormat.DayMonthYear));
                            }
                            else
                            {
                                AlertMsg.MsgBox(Page, "Invalid PIN!");
                                return;
                            }

                            ViewState["EID"] = p12.Value;
                            if (Session["FD"] != null)
                            {
                                DataSet ds = (DataSet)Session["FD"];
                                ds.Namespace = "";
                                AttendanceDAC.HMS_InsupFamilyDetailsXml(ds, Convert.ToInt32(ViewState["EID"]), 1);
                            }
                        }
                        else
                        {
                            if (txtPIN.Text.Length >= 6 || txtPer_PIN.Text.Length >= 6 || txtPIN.Text.Length == 0)
                            {
                                objAtt.AddAppDetails(Convert.ToInt32(p16.Value), txtAddress.Text.Replace("\n", "<br/>"), txtCity.Text, txtState.Text, txtCountry.Text, txtPhone.Text, txtPIN.Text, chkAddress.Checked, txtPer_Address.Text.Replace("\n", "<br/>"), txtPer_City.Text, txtPer_State.Text, txtPer_Country.Text, txtPer_Phone.Text, txtPer_PIN.Text, ddlBldGrp.SelectedItem.Text, Convert.ToInt32(txtSal.Text), CODEUtility.ConvertToDate(txtDoj.Text.Trim(), DateFormat.DayMonthYear));
                            }
                            else
                            {
                                AlertMsg.MsgBox(Page, "Invalid PIN!");
                                return;
                            }

                            Session["AppId"] = p16.Value;
                            ViewState["EID"] = p16.Value;
                            if (Session["FD"] != null)
                            {
                                DataSet ds = (DataSet)Session["FD"];
                                ds.Namespace = "";
                                AttendanceDAC.HMS_InsupFamilyDetailsXml(ds, Convert.ToInt32(ViewState["EID"]), 1);
                            }
                            if (ext != "")
                            {
                                try
                                {
                                    Vpath = Server.MapPath(".\\EmpImages\\" + ViewState["filename"].ToString());
                                    VthumbPath = Server.MapPath(".\\EmpImages\\" + ViewState["filename"].ToString());

                                    path = Server.MapPath(".\\EmpImages\\" + p16.Value + "." + ext);
                                    ThumbPath = Server.MapPath(".\\EmpImages\\" + p16.Value + "_thumb." + ext);
                                    if (path != "")
                                    {
                                        string Vf = Path.GetFileName(path);
                                        System.IO.File.Delete(path);
                                    }
                                    string Vfilename = Path.GetFileName(Vpath);
                                    Vfilename.Replace(Vfilename, path);
                                    System.IO.File.Move(Vpath, path);

                                    PicUtil.ConvertPic(path, 128, 160, path);
                                    PicUtil.ConvertPic(path, 19, 24, ThumbPath);
                                    Vpath = null;
                                    path = null;
                                }
                                catch (Exception ex)
                                {

                                    throw ex;
                                }

                            }
                            SMSCAPI m = new SMSCAPI();
                            
                            string msg = String.Format("Congrats! You are appointed as {0} under {1} dept of {2} site -{3}", ddlDesignation.SelectedItem.Text.Trim().Replace(" ", "_"), ddldept.SelectedItem.Text.Trim().Replace(" ", "_"), ddlWorksite.SelectedItem.Text.Trim().Replace(" ", "_"), Company.Replace(" ", "_"));
                            try { m.SendSMS(SMSUserID, SMSPassword, txtMobile1.Text, msg); }
                            catch { AlertMsg.MsgBox(Page, "Sending SMS Faild..!"); }
                            try { SendMailToApplicant(); }
                            catch { AlertMsg.MsgBox(Page, "Sending Mail Faild..!"); }
                            try
                            {
                                Response.Redirect("Appointment.aspx?id=" + Session["AppId"].ToString() + "&DocID=" + 1);

                            }
                            catch { }
                           

                        }
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(typeof(System.String), "str", "<script type='text/javascript'>alert('Already record exists with same name or mailid or date of birth')</script>");
                    }

                    try
                    {
                        if (Request.QueryString["Join"].ToString() == "1")
                        {
                            AttendanceDAC.HR_UpdateUserNamePasswordByJoin(Convert.ToInt32(Request.QueryString["Id"]), txtUsername.Text, FormsAuthentication.HashPasswordForStoringInConfigFile(txtPassword.Text, "MD5"));
                        }
                    }
                    catch
                    {

                    }
                    ddlDesignation.SelectedIndex = 0;
                    ddlCategory.SelectedIndex = 0;
                    txtMobile1.Text = "";
                    txtMobile2.Text = "";
                    txtName.Text = "";
                    txtUsername.Text = "";
                    txtPassword.Text = "";
                    ddlEmpType.SelectedIndex = 0;
                    txtMailId.Text = "";
                    txtskypeid.Text = "";
                    ddldept.SelectedIndex = 0;

                    Response.Redirect("EmployeeList.aspx");
                }
                catch (Exception eee)
                {
                    AlertMsg.MsgBox(Page, eee.Message.ToString(),AlertMsg.MessageType.Error);
                }
                // }

            }
            catch (Exception Create)
            {
                AlertMsg.MsgBox(Page, Create.Message.ToString(),AlertMsg.MessageType.Error);
            }
        }

        public void SendMailToApplicant()
        {

            try
            {
                string ToMailID = string.Empty;
                string Name = string.Empty;
                string Designation = string.Empty;
                string Salary = string.Empty;
                string DateOfJoin = string.Empty;
                string JobType = string.Empty;
                 
               
                MailMessage msgMail = new MailMessage();

                msgMail.To = txtMailId.Text;// ToMailID;
                msgMail.From = RegEmailID;
                msgMail.Subject = "Appointment Letter";
                string bodyMessage = "";
                string imagescr = siteurl + "Images/logo1";
                string imagebackground = siteurl + "Images/middal_bg";
                string date = DateTime.Now.ToString();
                string date1 = Convert.ToDateTime(date).ToString("MMMM dd, yyyy hh:mm tt");
                string date2 = Convert.ToDateTime(date).ToString("MMMM dd, yyyy");
                bodyMessage = bodyMessage + "<table width='560' border='0' cellspacing='0' cellpadding='0' align='center' style='font-family:Arial, Helvetica, sans-serif; font-size:11px; color:#333333; border:#999999 solid 1px;'>";
                bodyMessage = bodyMessage + " <tr>";
                bodyMessage = bodyMessage + "<td><img src=" + imagescr + " /></td>";
                bodyMessage = bodyMessage + " </tr>";

                bodyMessage = bodyMessage + "<td style='font-size:16px; font-weight:bold; color:#666666; padding-left:34px;line-height:29px;color:#ffffff'> " + Company + " Offer Letter Confirmation</td>";
                bodyMessage = bodyMessage + "</tr>";
                bodyMessage = bodyMessage + "</table></td>";
                bodyMessage = bodyMessage + "</tr>";
                bodyMessage = bodyMessage + "<tr>";
                bodyMessage = bodyMessage + "<td background=" + imagebackground + ">";
                bodyMessage = bodyMessage + "<table width='100%' border='0' cellspacing='0' cellpadding='0'>";
                bodyMessage = bodyMessage + "<tr valign='top'>";
                bodyMessage = bodyMessage + "<td style='padding:10px'>";
                //bodyMessage = bodyMessage + "<p style='padding:0px 0px 15px 0px; margin:0px; font-size:14px; font-weight:bold; color:#999999;'>" + date2 + "</p>";
                bodyMessage = bodyMessage + "<p style='padding:0px 0px 15px 0px; margin:0px; font-family:Arial, Helvetica, sans-serif; font-size:12px; color:#333333;'>Hi " + txtName.Text + txtMName.Text + txtLName.Text + ",</p>";
                bodyMessage = bodyMessage + "<p style='padding:0px 0px 15px 0px; margin:0px; font-family:Arial, Helvetica, sans-serif; font-size:12px; color:#333333;'>Welcome to " + Company + "</p>";

                bodyMessage = bodyMessage + "<p style='padding:0px 0px 15px 0px; margin:0px; font-family:Arial, Helvetica, sans-serif; font-size:12px; color:#333333;'>" + Company + " Offers to you</p>";

                bodyMessage = bodyMessage + "<p style='padding:0px 0px 15px 0px; margin:0px; font-family:Arial, Helvetica, sans-serif; font-size:12px; color:#333333;'>Your Designation : " + ddlDesignation.SelectedItem.Text + " </p>";

                bodyMessage = bodyMessage + "<p style='padding:0px 0px 15px 0px; margin:0px; font-family:Arial, Helvetica, sans-serif; font-size:12px; color:#333333;'>Your Salary : " + txtSal + " </p>";

                bodyMessage = bodyMessage + "<p style='padding:0px 0px 15px 0px; margin:0px; font-family:Arial, Helvetica, sans-serif; font-size:12px; color:#333333;'>Date Of Join : " + txtDoj + " </p>";

              
                bodyMessage = bodyMessage + "<p style='padding:0px 0px 15px 0px; margin:0px; font-family:Arial, Helvetica, sans-serif; font-size:12px; color:#333333;'>Thank  you.</p>";
                bodyMessage = bodyMessage + "<p style='padding:0px 0px 15px 0px; margin:0px; font-family:Arial, Helvetica, sans-serif; font-size:12px; color:#333333;'>From,<br /> " + Company + "<br /><a href='" + WebSiteID + "' target='_blank'>" + WebSiteID + "</a></p>";
                bodyMessage = bodyMessage + "<td>";
                bodyMessage = bodyMessage + " </tr>";

                bodyMessage = bodyMessage + "</table>";
                bodyMessage = bodyMessage + "</td>";
                bodyMessage = bodyMessage + "</tr>";
                bodyMessage = bodyMessage + "</table>";




                msgMail.BodyFormat = MailFormat.Html;
                msgMail.Body = bodyMessage;
                SmtpMail.SmtpServer = SMTPServer;
                SmtpMail.Send(msgMail);

            }
            catch (Exception)
            {

                throw;
            }

        }

        protected void ddlWorksite_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindWorkSiteMangers();

           
            ddlManager.Enabled = true;
            ddldept.Focus();

        }

        protected void ddldept_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindWorkSiteMangers();
            int? ReptEmpID = null;
            if (Request.QueryString.Count > 0)
            {
                ReptEmpID = int.Parse(Request.QueryString["Id"].ToString());
            }

            BindDeptHeadForReporting(ReptEmpID);

            ddlManager.Focus();
        }

        private void GetManagers()
        {

            int DeptID = Convert.ToInt32(ddldept.SelectedValue);
            int WSID = Convert.ToInt32(ddlWorksite.SelectedValue);
            
            DataSet ds = objAtt.GetEmpDetails(DeptID, WSID);
            DataSet dsHead =null;

            SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings["strConn"]);
            cn.Open();

            SqlDataAdapter da;
            da = new SqlDataAdapter("select headid from T_HR_Prj_Dept head  where deptid='" + ddldept.SelectedValue + "'  and prjid=" + ddlWorksite.SelectedValue, cn);
            da.Fill(dsHead);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlManager.DataSource = ds.Tables[0];
                ddlManager.DataTextField = "Name";
                ddlManager.DataValueField = "EmpID";
                ddlManager.DataBind();

            }
            if (dsHead.Tables[0].Rows.Count > 0)
            {
                ddlManager.Items.Insert(0, new ListItem("Department Head", dsHead.Tables[0].Rows[0][0].ToString(), true));
            }
            else
            {
                ddlManager.Items.Insert(0, new ListItem("Department Head", "-1", true));
            }

            if (ddlEmpType.SelectedItem.Value != "4")
                ddlManager.Enabled = false;
            else
                ddlManager.Enabled = true;
        }

        protected void txtUsername_TextChanged(object sender, EventArgs e)
        {
            if (txtUsername.Text != "")
            {
                lblUserAvailable.Visible = false;
                System.Threading.Thread.Sleep(3000);
                obj.Username = txtUsername.Text.Trim();

                int i = (int)objAtt.UserNameAvailable(obj); //, Convert.ToInt32(Session["CompanyID"]));
                if (chkPWD.Checked)
                {
                    if (i > 0)
                    {
                        lblUserAvailable.Visible = true;
                        lblUserAvailable.ForeColor = System.Drawing.Color.Red;
                        lblUserAvailable.Text = "Username not available";
                        btnSubmit.Visible = false;
                    }
                    else
                    {
                        lblUserAvailable.ForeColor = System.Drawing.Color.Green;
                        lblUserAvailable.Visible = true;
                        lblUserAvailable.Text = "Username Available";
                        btnSubmit.Visible = true;
                    }
                }
            }
            else
            {
                lblUserAvailable.Text = "";
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("EmployeeList.aspx");
        }

        protected void ddlBank_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int BankID = Convert.ToInt32(ddlBank.SelectedValue);
                DataSet ds = AttendanceDAC.HR_GetBankBranches(BankID);
                ddlBranch.DataSource = ds;
                ddlBranch.DataValueField = "BranchID";
                ddlBranch.DataTextField = "BranchName";
                ddlBranch.DataBind();
                ddlBranch.Items.Insert(0, new ListItem("--Select--", "0"));
            }
            catch { AlertMsg.MsgBox(Page, "Unable to bind Branch Details..!"); }
        }

        protected void ddlDependies_SelectedIndexChanged(object sender, EventArgs e)
        {

            int ddlDependvalue = Convert.ToInt32(ddlDependies.SelectedItem.Value);

            if (ViewState["Edt"].ToString() != "1")
                ViewState["Edt"] = 0;
            int ID = Convert.ToInt32(ddlDependies.SelectedValue);
            trDepdAge.Visible = trDepdBGrp.Visible = trDepdGender.Visible = trDepdName.Visible = true;
            if (ID == 0)
            {
                trDepdAge.Visible = trDepdBGrp.Visible = trDepdGender.Visible = trDepdName.Visible = false;
            }
            ddlDepGender.Enabled = false;
           
            if (ID == 1)
            {
                ddlDepGender.SelectedValue = "1";
            }
            else if (ID == 2)
            {
                ddlDepGender.SelectedValue = "0";
            }
            else if (ID == 3)
            {

                if (rdblstgender.SelectedItem.Text == "Male")
                {
                    ddlDepGender.SelectedValue = "0";
                }
                else
                {
                    ddlDepGender.SelectedValue = "1";
                }

            }
            else if (ID == 4)
            {
                ddlDepGender.SelectedValue = "1";
            }
            else if (ID == 5)
            {
                ddlDepGender.SelectedValue = "0";
            }
        }

        protected void btnDepdAdd_Click(object sender, EventArgs e)
        {
            FamilyDetails FD;
            if (btnDepdAdd.Text != "Update")
            {
                if (Session["FD"] == null)
                    FD = new FamilyDetails();
                else
                    FD = (FamilyDetails)Session["FD"];

            }

            else
                FD = (FamilyDetails)Session["FD"];
            try
            {

                int Dependencies = Convert.ToInt32(ddlDependies.SelectedValue);
                int AlreadyAddedDependencies;
                if (ViewState["Edt"].ToString() == "" || ViewState["Edt"].ToString() == "0")
                {
                    if (Session["FD"] == null)
                        Session["FD"] = FD;

                    FD = (FamilyDetails)Session["FD"];




                    if (ViewState["Edt"].ToString() == "1")
                    {
                        for (int i = 0; i < FD.Tables[0].Rows.Count; i++)
                        {
                            AlreadyAddedDependencies = Convert.ToInt32(FD.dtFamily.Rows[i][0]);
                            if (Dependencies == AlreadyAddedDependencies)
                            {
                                AlertMsg.MsgBox(Page, "Selected Dependencies details already exists");
                            }
                            else
                                foreach (GridViewRow gvr in gvFamily.Rows)
                                {
                                    FD = (FamilyDetails)Session["FD"];
                                    int index = Convert.ToInt32(ViewState["index"]);
                                    FD.dtFamily.Rows[index].BeginEdit();
                                    FD.dtFamily.Rows[index][0] = Convert.ToInt32(ddlDependies.SelectedValue);
                                    FD.dtFamily.Rows[index][1] = txtDepndName.Text;
                                    FD.dtFamily.Rows[index][2] = txtDepndAge.Text;
                                    FD.dtFamily.Rows[index][3] = ddlDepGender.SelectedItem.Text;
                                    FD.dtFamily.Rows[index][4] = txtDepndBGroup.Text;
                                    FD.dtFamily.Rows[index][5] = ddlDependies.SelectedItem.Text;
                                    FD.dtFamily.Rows[index].EndEdit();
                                    FD.dtFamily.Rows[index].AcceptChanges();
                                    Session["FD"] = FD;
                                    if (ViewState["EID"].ToString() != "" && ViewState["EID"].ToString() != "0")
                                        AttendanceDAC.HMS_InsupFamilyDetailsXml(FD, Convert.ToInt32(ViewState["EID"]), 1);
                                    btnDepdAdd.Text = "save";
                                }
                            FD.AcceptChanges();



                        }

                    }
                    else
                    {

                        FD.dtFamily.AdddtFamilyRow(Convert.ToInt32(ddlDependies.SelectedValue), txtDepndName.Text, txtDepndAge.Text, (ddlDepGender.SelectedItem.Text),
                                                    txtDepndBGroup.Text, ddlDependies.SelectedItem.Text, Convert.ToInt32(ViewState["Edt"]));

                        Session["FD"] = FD;

                    }




                }
                else
                {
                    if (btnDepdAdd.Text != "Update")
                    {
                        for (int i = 0; i < FD.Tables[0].Rows.Count; i++)
                        {
                            AlreadyAddedDependencies = Convert.ToInt32(FD.dtFamily.Rows[i][0]);
                            if (Dependencies == AlreadyAddedDependencies)
                            {
                                AlertMsg.MsgBox(Page, "Selected Dependencies details already exists");
                                return;
                            }
                            else
                            {
                            }

                        }
                        FD.dtFamily.AdddtFamilyRow(Convert.ToInt32(ddlDependies.SelectedValue), txtDepndName.Text, txtDepndAge.Text, (ddlDepGender.SelectedItem.Text),
                                                    txtDepndBGroup.Text, ddlDependies.SelectedItem.Text, Convert.ToInt32(ViewState["Edt"]));

                        Session["FD"] = FD;
                    }
                    else
                    {

                        foreach (GridViewRow gvr in gvFamily.Rows)
                        {
                            Session["FD"] = FD;
                            FD = (FamilyDetails)Session["FD"];
                            int index = Convert.ToInt32(ViewState["index"]);
                            FD.dtFamily.Rows[index].BeginEdit();
                            FD.dtFamily.Rows[index][0] = Convert.ToInt32(ddlDependies.SelectedValue);
                            FD.dtFamily.Rows[index][1] = txtDepndName.Text;
                            FD.dtFamily.Rows[index][2] = txtDepndAge.Text;
                            FD.dtFamily.Rows[index][3] = ddlDepGender.SelectedItem.Text;
                            FD.dtFamily.Rows[index][4] = txtDepndBGroup.Text;
                            FD.dtFamily.Rows[index][5] = ddlDependies.SelectedItem.Text;
                            FD.dtFamily.Rows[index].EndEdit();
                            FD.dtFamily.Rows[index].AcceptChanges();
                            Session["FD"] = FD;
                            if (ViewState["EID"].ToString() != "" && ViewState["EID"].ToString() != "0")
                                AttendanceDAC.HMS_InsupFamilyDetailsXml(FD, Convert.ToInt32(ViewState["EID"]), 1);
                            btnDepdAdd.Text = "save";
                        }
                    }
                    FD.AcceptChanges();

                }
                ddlDependies.Enabled = true;
                ddlDepGender.Enabled = false;
                ddlDependies.SelectedIndex = 0;
                if (ViewState["Edt"].ToString() == "")
                    ViewState["Edt"] = 0;
                if (ViewState["EID"].ToString() != "")
                    ddlDependies.SelectedValue = "0";
                txtDepndName.Text = "";
                txtDepndAge.Text = "";
                txtDepndBGroup.Text = "";
                ddlDepGender.SelectedValue = "1";
                ViewState["Edt"] = 0;
                gvFamily.DataSource = (DataSet)Session["FD"];
                gvFamily.DataBind();



            }
            catch { AlertMsg.MsgBox(Page, "Task Failed..!"); }
        }

        protected void gvFamily_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            try
            {
                GridViewRow gvRow = (GridViewRow)((LinkButton)e.CommandSource).Parent.Parent;
                int index = gvRow.RowIndex;
                ViewState["index"] = index;
                if (e.CommandName == "del")
                {
                    FamilyDetails FD1 = (FamilyDetails)Session["FD"];
                    FD1.dtFamily.Rows[index].Delete();
                    Session["FD"] = FD1;
                    gvFamily.DataSource = FD1;
                    gvFamily.DataBind();
                }
                if (e.CommandName == "edt")
                {
                    ddlDependies.Enabled = false;
                    ddlDepGender.Enabled = false;
                    trDepdAge.Visible = trDepdBGrp.Visible = trDepdGender.Visible = trDepdName.Visible = true;
                    ViewState["Edt"] = 1;
                    ddlDependies.SelectedValue = e.CommandArgument.ToString();
                    txtDepndName.Text = gvRow.Cells[2].Text;
                    txtDepndAge.Text = gvRow.Cells[3].Text;
                    if (gvRow.Cells[5].Text == "Male")
                    {
                        ddlDepGender.SelectedValue = "1";
                    }
                    else
                    {
                        ddlDepGender.SelectedValue = "0";
                    }
                    if (gvRow.Cells[4].Text != "&nbsp;")
                        txtDepndBGroup.Text = gvRow.Cells[4].Text;
                    else
                        txtDepndBGroup.Text = "";

                    btnDepdAdd.Text = "Update";

                }
            }
            catch { AlertMsg.MsgBox(Page, "Task Failed..!"); }
        }

        protected void txtName_TextChanged(object sender, EventArgs e)
        {
            string MailID = txtName.Text;
            if (MailID != "")
            {
                DataSet ds = AttendanceDAC.HR_ChkMail(txtName.Text.ToLower() + "@" + ConfigurationManager.AppSettings["WebSiteID"].Remove(0, 11).ToString());
                lblMailWarn.Text = "";
                if (ds.Tables[0].Rows[0][0].ToString() == "0")
                {
                    txtMailId.Text = txtName.Text.ToLower() + "@" + ConfigurationManager.AppSettings["WebSiteID"].Remove(0, 11).ToString();
                    txtUsername.Text = txtName.Text.Trim();
                    if (Request.QueryString.Count == 0)
                    {
                        obj.Username = txtUsername.Text.Trim();

                        int i = (int)objAtt.UserNameAvailable(obj);  //, Convert.ToInt32(Session["CompanyID"]));
                        if (chkPWD.Checked)
                        {
                            if (i > 0)
                            {
                                lblUserAvailable.Visible = true;
                                lblUserAvailable.ForeColor = System.Drawing.Color.Red;
                                lblUserAvailable.Text = "Username not available";
                                txtUsername.Text = "";
                            }
                            else
                            {
                                lblUserAvailable.ForeColor = System.Drawing.Color.Green;
                                lblUserAvailable.Visible = true;
                                lblUserAvailable.Text = "Username Available";
                                txtUsername.Text = txtName.Text.ToLower();
                            }
                        }

                    }
                }
                else
                {
                    lblMailWarn.Text = txtName.Text.ToLower() + "@" + ConfigurationManager.AppSettings["WebSiteID"].Remove(0, 11).ToString() + " is already exists.!";
                }

            }

        }

        private void BindLocations()
        {
            DataSet ds = AttendanceDAC.CMS_Get_City();
            DdlLocation.DataSource = ds;
            DdlLocation.DataTextField = ds.Tables[0].Columns["CItyName"].ToString();
            DdlLocation.DataValueField = ds.Tables[0].Columns["CityID"].ToString();
            DdlLocation.DataBind();
            DdlLocation.Items.Insert(0, new ListItem("---Select---", "0"));
            int c = ds.Tables[0].Rows.Count;
            DdlLocation.Items.Insert(c + 1, new ListItem("New", "-1"));
        }

        protected void DdlLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (DdlLocation.SelectedItem.Text.ToLower() == "new")
                {
                    //tblNewLocation.Visible = true;
                    txtNewLocation.Text = "";
                    ddlCountry.SelectedIndex = 0;
                    DdlState.SelectedIndex = 0;

                }
                else
                {
                    //  tblNewLocation.Visible = false;
                    txtNewLocation.Text = "";
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        protected void txtSaveLocatin_Click(object sender, EventArgs e)
        {
            int CityID;
            if (ViewState["CityID"].ToString() != "0" && ViewState["CityID"].ToString() != string.Empty)
            {
                CityID = Convert.ToInt32(ViewState["CityID"].ToString());
            }
            else
            {
                CityID = 0;
            }
            //HR_InsNewLocation
            int Output = AttendanceDAC.HR_InsNewLocation(CityID, txtNewLocation.Text, Convert.ToInt32(DdlState.SelectedValue));
           
            BindLocations();
            DdlLocation.Items.FindByText(txtNewLocation.Text).Selected = true;
            tblNewLocation.Visible = false;
           
        }

        protected void ddlEmpType_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindWorkSiteMangers();
            ddlEmpnature.Focus();
        }
        protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int CountryId = Convert.ToInt32(ddlCountry.SelectedValue);
                BindStates(CountryId);
                tblNewLocation.Style.Remove("display");
                tblNewLocation.Style.Add("display", "block");

            }
            catch (Exception ex)
            {

                throw;
            }
        }

        protected void chkPWD_CheckedChanged(object sender, EventArgs e)
        {
            if (chkPWD.Checked == true)
            {
                txtUsername.Text = txtName.Text;
                txtUsername.Enabled = true;
                txtPassword.Enabled = true;
                txtReenterPassword.Enabled = true;
                lblUserAvailable.Visible = true;
                txtUsername_TextChanged(sender, e);

            }
            else
            {
                txtUsername.Text = "";
                txtUsername.Enabled = false;
                txtPassword.Enabled = false;
                txtReenterPassword.Enabled = false;
                lblUserAvailable.Visible = false;


            }
        }
        protected void btnNextPer_Click(object sender, EventArgs e)
        {
            tabPerDetails.Enabled = false;
            tabUPDImage.Enabled = true;

            // tabDepen.Enabled = true;
            btnNextUPD.Focus();
           
        }
        protected void btnNextUPD_Click(object sender, EventArgs e)
        {
            tabUPDImage.Enabled = false;
            tabDepen.Enabled = true;
            btnNextDepen.Focus();

        }
        protected void btnNextDepen_Click(object sender, EventArgs e)
        {
            tabDepen.Enabled = false;
            tabJobDetails.Enabled = true;
            btnJobNext.Focus();
        }
        protected void btnJobNext_Click(object sender, EventArgs e)
        {
            tabJobDetails.Enabled = false;
            tabCommunication.Enabled = true;
            btnNextAdd.Focus();
        }
        protected void btnNextAdd_Click(object sender, EventArgs e)
        {
            tabCommunication.Enabled = false;
            tabAccDetails.Enabled = true;
            btnNextAccDetails.Focus();
        }
        protected void btnNextAccDetails_Click(object sender, EventArgs e)
        {
            tabAccDetails.Enabled = false;
            tabAuthen.Enabled = true;
            btnPreAuethen.Focus();
            btnSubmit.Visible = true;
        }
        protected void btnPreAuethen_Click(object sender, EventArgs e)
        {
            tabAccDetails.Enabled = true;
            tabAuthen.Enabled = false;
            btnPreAccDetails.Focus();
        }
        protected void btnPreAccDetails_Click(object sender, EventArgs e)
        {
            tabCommunication.Enabled = true;
            tabAccDetails.Enabled = false;
            btnPreAdd.Focus();
        }
        protected void btnPreAdd_Click(object sender, EventArgs e)
        {
            tabJobDetails.Enabled = true;
            tabCommunication.Enabled = false;
            btnPre.Focus();
        }
        protected void btnPre_Click(object sender, EventArgs e)
        {
            tabDepen.Enabled = true;
            tabJobDetails.Enabled = false;
            btnPreDepen.Focus();
        }
        protected void btnPretUPD_Click(object sender, EventArgs e)
        {
            tabPerDetails.Enabled = true;
            tabUPDImage.Enabled = false;
            btnNextPer.Focus();
        }
        protected void btnPreDepen_Click(object sender, EventArgs e)
        {

            tabUPDImage.Enabled = true;
            tabDepen.Enabled = false;


        }

    }
}