using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AECLOGIC.HMS.BLL;
using System.Data;


namespace AECLOGIC.ERP.HMS
{
    public partial class EmpEduExpDetails : AECLOGIC.ERP.COMMON.WebFormMaster
    {
        #region Declaration
        int mid = 0;
        bool viewall;
        string menuname;
        string menuid;
        AttendanceDAC objExp = new AttendanceDAC();
        HRCommon objHrCommon = new HRCommon();

        #endregion Declaration

        #region PageLoad

        protected void Page_Load(object sender, EventArgs e)
        {

            EmpQuaExpTab.ActiveTabIndex = 0;
            ViewState["EmID"] = Request.QueryString["EmpID"];
            if (Request.QueryString.Count > 0)
            {
                dvEditExp.Visible = true;
                dvAdd.Visible = false;
                GetQualificationDetails();
                GetExpDetails();
            }
            else
            {
                dvEditExp.Visible = false;
                dvAdd.Visible = true;
            }
            if (!IsPostBack)
            {
                ViewState["EduID"] = null;
                ViewState["ExpID"] = null;
                FillComuCity();
                fillYears();
            }
        }

        #endregion PageLoad
        protected override void OnInit(EventArgs e)
        {
            ModuleID = 1;
            base.OnInit(e);
        }
        #region Supporting Methods

        public void GetQualificationDetails()
        {
              
           DataSet ds = AttendanceDAC.GetEducationDetails(Convert.ToInt32(ViewState["EmID"]));
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                grdQualifi.DataSource = ds;
            }
            else
            {
                grdQualifi.EmptyDataText = "No Records Found";
            }
            grdQualifi.DataBind();

        }
        public void GetExpDetails()
        {

            DataSet ds = AttendanceDAC.GetEmpExpDetails(Convert.ToInt32(ViewState["EmID"]));
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                grdEmpExp.DataSource = ds;
            }
            else
            {
                grdEmpExp.EmptyDataText = "No Records Found";
            }
            grdEmpExp.DataBind();

        }
        void FillComuCity()
        {
            DataSet ds = AttendanceDAC.GetAllCities(null);
            ddlCity.DataSource = ds;
            ddlCity.DataTextField = ds.Tables[0].Columns["CityName"].ToString();
            ddlCity.DataValueField = ds.Tables[0].Columns["CityID"].ToString();
            ddlCity.DataBind();
            ddlCity.Items.Insert(0, new ListItem("---Select---", "0"));
           
        }
      
        public void InsQualificationDetails()
        {
            string Qualification, Institute, Specilization;
            Qualification = txtQuli.Text;
            Institute = txtInstitute.Text;
            Specilization = txtSpecilization.Text;
            double Percentage = Convert.ToDouble(txtPercentage.Text);
            int Yop = Convert.ToInt32(ddlYOP.SelectedValue);
            int EduID = Convert.ToInt32(ViewState["EduID"]);
            int OutPut = objExp.InsUpdEmpQua(Qualification, Institute, Yop, Specilization, Percentage,
                    Convert.ToInt32(ddlFullPartTime.SelectedValue), Convert.ToInt32(ViewState["EmID"]), EduID);
            if (OutPut == 1)
                AlertMsg.MsgBox(Page, "Inserted sucessfully.!");
            else if (OutPut == 2)
                AlertMsg.MsgBox(Page, "Already exists.(May be same Qualification Name or Passing Years Existed!)");
            else
                AlertMsg.MsgBox(Page, "Updated sucessfully.!");
            GetQualificationDetails();

        }
        public void InsExperianceDetails()
        {
            try
            {
                string Organization, Designation;
                int Type = Convert.ToInt32(ddlPermanent.SelectedValue);
                DateTime Fromdate = CODEUtility.ConvertToDate(txtFromDate.Text, DateFormat.DayMonthYear);
                DateTime Todate = CODEUtility.ConvertToDate(txtToDate.Text, DateFormat.DayMonthYear);
                int ExpID = Convert.ToInt32(ViewState["ExpID"]);
                double CTC = Convert.ToDouble(txtCTC.Text);
                Organization = txtOrg.Text;
                int CityID = Convert.ToInt32(ddlCity.SelectedValue);
                //City = txtCity.Text;
                Designation = txtDesig.Text;
                int OutPut = objExp.InsUpdEmpExp(Organization, CityID, Type, Fromdate, Todate, Designation, Convert.ToInt32(ViewState["EmID"]), ExpID, CTC);
                if (OutPut == 1)
                    AlertMsg.MsgBox(Page, "Inserted sucessfully.!");
                else if (OutPut == 2)
                    AlertMsg.MsgBox(Page, "Already exists.!(May be same Organisation Name existed!)");
                else
                    AlertMsg.MsgBox(Page, "Updated sucessfully.!");
            }
            catch (Exception)
            {
                AlertMsg.MsgBox(Page, "Select Proper Date.!");
            }
            GetExpDetails();

        }
        public void fillYears()
        {
            int year = System.DateTime.Now.Year;

            for (int intCount = 1950; intCount <= year; intCount++)
            {
                ddlYOP.Items.Add(intCount.ToString());
            }

        }
        public void TabIndexPosistion()
        {
            if (EmpQuaExpTab.ActiveTabIndex == 0)
            {
                GetQualificationDetails();
            }
            else
            {
                GetExpDetails();
            }
        }

        #endregion Supporting Methods

        #region Events

        protected void btnExpSubmit_Click(object sender, EventArgs e)
        {
            InsExperianceDetails();
            ClearExp();
        }
        public void ClearExp()
        {
            txtOrg.Text = "";
            txtDesig.Text = "";
            txtCTC.Text = "";
            ddlCity.SelectedIndex = 0;
            txtFromDate.Text = "";
            txtToDate.Text = "";
        }
        public void ClearQuli()
        {
            txtQuli.Text = "";
            txtInstitute.Text = "";
            txtSpecilization.Text = "";
            txtPercentage.Text = "";
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            InsQualificationDetails();
            ClearQuli();
        }
        protected void TabContainer1_ActiveTabChanged(object sender, EventArgs e)
        {
            TabIndexPosistion();
        }
        protected void grdQualifi_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Edt")
            {
                FillComuCity();
                int EduID = Convert.ToInt32(e.CommandArgument);
                ViewState["EduID"] = EduID;
                  
            DataSet  ds = AttendanceDAC.GetEduDetailsByEduID(EduID);
                txtQuli.Text = ds.Tables[0].Rows[0]["Qualification"].ToString();
                txtInstitute.Text = ds.Tables[0].Rows[0]["Institute"].ToString();
                ddlYOP.SelectedValue = Convert.ToInt32(ds.Tables[0].Rows[0]["YOP"]).ToString();
                txtSpecilization.Text = ds.Tables[0].Rows[0]["Specialization"].ToString();
                txtPercentage.Text = ds.Tables[0].Rows[0]["Percentage"].ToString();
                ddlFullPartTime.SelectedValue = Convert.ToInt32(ds.Tables[0].Rows[0]["Mode"]).ToString();
                dvEditExp.Visible = false;
                dvAdd.Visible = true;
                tb.ActiveTabIndex = 0;
            }
        }
        protected void grdEmpExp_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Edt")
            {
                int ExpID = Convert.ToInt32(e.CommandArgument);
                ViewState["ExpID"] = ExpID;

                DataSet ds = AttendanceDAC.GetExpDetailsByEXpID(ExpID);
                txtOrg.Text = ds.Tables[0].Rows[0]["Organization"].ToString();
                txtDesig.Text = ds.Tables[0].Rows[0]["Designation"].ToString();
                txtCTC.Text = ds.Tables[0].Rows[0]["CTC"].ToString();
                ddlCity.SelectedValue = ds.Tables[0].Rows[0]["City"].ToString();
                ddlPermanent.SelectedValue = Convert.ToInt32(ds.Tables[0].Rows[0]["Type"]).ToString();
                txtFromDate.Text = ds.Tables[0].Rows[0]["FromDate"].ToString();
                txtToDate.Text = ds.Tables[0].Rows[0]["ToDate"].ToString();
                dvEditExp.Visible = false;
                dvAdd.Visible = true;
                tb.ActiveTabIndex = 1;
            }
        }
        protected void lnkAdd_Click(object sender, EventArgs e)
        {
            lnkAdd.BackColor = Color.Lavender;
            lnkEdit.BackColor = Color.White;
            tb.ActiveTabIndex = 0;
            dvAdd.Visible = true;
            dvEditExp.Visible = false;
        }
        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            lnkEdit.BackColor = Color.Lavender;
            lnkAdd.BackColor = Color.White;
            dvAdd.Visible = false;
            dvEditExp.Visible = true;
        }
        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("EmployeeList.aspx");
        }
        #endregion Events
    }

}