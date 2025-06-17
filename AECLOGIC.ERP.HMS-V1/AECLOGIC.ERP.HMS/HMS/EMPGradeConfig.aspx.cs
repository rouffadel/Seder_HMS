using AECLOGIC.ERP.HMS.HRClasses;
using AECLOGIC.HMS.BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AECLOGIC.ERP.HMS.HMS
{
    public partial class EMPGradeConfig : AECLOGIC.ERP.COMMON.WebFormMaster
    {
          
        int mid = 0;
        bool viewall;
        string menuname;
        string menuid;
        static string strurl = string.Empty;
        static bool status;
        protected override void OnInit(EventArgs e)
        {
            ModuleID = 1;
            base.OnInit(e);

        }

     
        protected void Page_Load(object sender, EventArgs e)
        {
            lblStatus.Text = String.Empty;
            btnSave.Attributes.Add("onclick", @" this.value=""Uploading..."" ;this.disabled = true; " + this.Page.ClientScript.GetPostBackEventReference(btnSave, null) + ";");
            try
            {
                if (rblDesg.SelectedValue=="1")
                    status = true;
                else
                    status = false;
                if (!IsPostBack)
                {
                    GetParentMenuId();
                    ViewState["LEId"] = "";
                    BindEmpNture();
                    BindGrid();
                    if (Convert.ToInt32(Request.QueryString["key"]) == 1)
                    {

                        this.Title = "Add Entitlement";
                        tblNew.Visible = true;
                        pnltblNew.Visible = true;
                        tblEdit.Visible = false;
                        gvEMPTrade.Visible = false;

                    }
                    else
                    {
                        tblEdit.Visible = true;
                        gvEMPTrade.Visible = true;
                    }
                }
            }
            catch (Exception ex)
            {
                clsErrorLog.HMSEventLog(ex, "EMPGradeConfig", "Page_Load", "001");

            }
           
        }
        public int GetParentMenuId()
        {
            string URL = Request.Url.Segments[Request.Url.Segments.Length - 1] + Request.Url.Query;
            int RoleId = Convert.ToInt32(Session["RoleId"].ToString());
            int ModuleId = ModuleID; ;

              

          DataSet  ds = AttendanceDAC.GetAllowed(RoleId, ModuleId, URL);
            int MenuId = 0;
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                MenuId = Convert.ToInt32(ds.Tables[0].Rows[0]["Under"]);
                
                gvEMPTrade.Columns[5].Visible = Convert.ToBoolean(ds.Tables[0].Rows[0]["Editable"].ToString());
                btnSave.Enabled = (bool)ds.Tables[0].Rows[0]["Editable"];
            }
            return MenuId;
        }
        public void BindGrid()
        {
            tblEdit.Visible = true;
            gvEMPTrade.Visible = true;
            tblNew.Visible = false;
            pnltblNew.Visible = false;
            bool Status = false;
            if (rblDesg.SelectedValue == "1")
            {
                Status = true;
            }
            int position = 0; if (txtsearchposition.Text.Trim() != "")
               { position = Convert.ToInt32(ddlposition_hid.Value == "" ? "0" : ddlposition_hid.Value); }
            int Medical = 0; if (txtsearchmedical.Text.Trim() != "")
            { Medical = Convert.ToInt32(ddlmedical_hid.Value == "" ? "0" : ddlmedical_hid.Value); }
            DataSet ds = clempGradesConfig.HR_GetEMPGradesDetails_Filter(Status, position, Medical);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                gvEMPTrade.DataSource = ds;
            }
            gvEMPTrade.DataBind();

        }
        public void BindEmpNture()
        {
            DataSet ds = clempGradesConfig.HR_GetallEMPTradsddls();
            ddlPositionCategory.DataSource = ds.Tables[0];
            ddlPositionCategory.DataTextField = "Name";
            ddlPositionCategory.DataValueField = "ID";
            ddlPositionCategory.DataBind();
            ddlEntitlement.DataSource = ds.Tables[1];
            ddlEntitlement.DataTextField = "Name";
            ddlEntitlement.DataValueField = "ID";
            ddlEntitlement.DataBind();
            ddlMedical.DataSource = ds.Tables[2];
            ddlMedical.DataTextField = "Name";
            ddlMedical.DataValueField = "ID";
            ddlMedical.DataBind();
        }
      
        public void BindDetails(int LEId)
        {
            tblEdit.Visible = false;
            tblNew.Visible = true;
            pnltblNew.Visible = true;
          DataSet  ds = clempGradesConfig.HR_GetEMPGradesDetailsbyID(LEId);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ddlPositionCategory.SelectedValue = ds.Tables[0].Rows[0]["PositionCategory"].ToString();
                txtGrade.Text = ds.Tables[0].Rows[0]["Grade"].ToString();
                txtBasicFrom.Text = ds.Tables[0].Rows[0]["SalaryFrom"].ToString();
                txtBasicTO.Text = ds.Tables[0].Rows[0]["SalaryTo"].ToString();
                txtHousing.Text = ds.Tables[0].Rows[0]["HRA"].ToString();
                txtTpt.Text = ds.Tables[0].Rows[0]["Transport"].ToString();
                txtFood.Text = ds.Tables[0].Rows[0]["Food"].ToString();
                txtMobile.Text = ds.Tables[0].Rows[0]["Mobile"].ToString();
                txtAL.Text = ds.Tables[0].Rows[0]["AnnualLeave"].ToString();
                ddlEntitlement.SelectedValue = ds.Tables[0].Rows[0]["FamilyEntitlement"].ToString();
                txtTickets.Text = ds.Tables[0].Rows[0]["Tickets"].ToString();
                txtVISA.Text = ds.Tables[0].Rows[0]["ExitEntryVISA"].ToString();
                ddlMedical.SelectedValue = ds.Tables[0].Rows[0]["Medical"].ToString();
                txtMedicalNos.Text = ds.Tables[0].Rows[0]["MedicalNos"].ToString();
                chkStatus.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["IsActive"].ToString() == "1" ? "true" : "false");
                btnSave.Text = "Update";

            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {

                int LEId = 0;
                if (ViewState["LEId"].ToString() != null && ViewState["LEId"].ToString() != string.Empty)
                {
                    LEId = Convert.ToInt32(ViewState["LEId"].ToString());
                }
                 int Status = 0;
                if (chkStatus.Checked == true)
                    Status = 1;
                clempGradesConfig.HR_AddEditEMPGrades(LEId,
                    Convert.ToInt32(ddlPositionCategory.SelectedValue), txtGrade.Text.ToString(),
                    Convert.ToDecimal(txtBasicFrom.Text), Convert.ToDecimal(txtBasicTO.Text),
                    Convert.ToDecimal(txtHousing.Text), Convert.ToDecimal(txtTpt.Text),
                    Convert.ToDecimal(txtFood.Text), Convert.ToDecimal(txtMobile.Text),
                    Convert.ToInt32(txtAL.Text), Convert.ToInt32(ddlEntitlement.SelectedValue),
                     Convert.ToDecimal(txtTickets.Text), Convert.ToDecimal(txtVISA.Text),
                     Convert.ToInt32(ddlMedical.SelectedValue), Convert.ToInt32(txtMedicalNos.Text), Status,
                      Convert.ToInt32(Session["UserId"])
                    );
                if (LEId == 0)
                {
                   AlertMsg.MsgBox(Page, "Done.! ");
                    
                }
                else
                {
                    AlertMsg.MsgBox(Page, "Updated");
                    

                }
                BindGrid();

            }
            catch (Exception ex)
            {
                clsErrorLog.HMSEventLog(ex, "EMPGradeConfig", "btnSave_Click", "002");
                AlertMsg.MsgBox(Page, "Record Already Existed",AlertMsg.MessageType.Warning);
               

            }

        }
        protected void rblDesg_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGrid();
        }
        protected void gvLeaveProfile_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int ID = Convert.ToInt32(e.CommandArgument);
                bool Status = true;
                if (rblDesg.SelectedValue == "1")
                {
                    Status = false;
                }
                ViewState["LEId"] = ID;
                if (e.CommandName == "Edt")
                {
                    BindDetails(ID);
                }
                else if (e.CommandName == "Del")
                {
                    AttendanceDAC.HMS_ActiveInActiveItems(ID, Status, "HR_Upd_ConfigEmpGrades");
                    BindGrid();
                    AlertMsg.MsgBox(Page, "Updated.! ");
                }
            }
            catch (Exception ex)
            {
                clsErrorLog.HMSEventLog(ex, "EMPGradeConfig", "gvLeaveProfile_rowCommand", "003");

            }
        }
   
        protected void lnkAdd_Click(object sender, EventArgs e)
        {

            tblNew.Visible = true;
            pnltblNew.Visible = true;
            tblEdit.Visible = false;

        }
        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            tblEdit.Visible = true;
            tblNew.Visible = false;
            pnltblNew.Visible = false;

        }

        protected void ddlEmpNature_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                BindGrid();
            }
            catch (Exception ex)
            {
                clsErrorLog.HMSEventLog(ex, "EMPGradeConfig", "ddlEmpNature_SelectedIndexChanged", "004");

            }
        }
        public string GetText()
        {

            if (rblDesg.SelectedValue == "1")
            {
                return "Deactivate";
            }
            else
            {
                return "Activate";
            }
        }
        protected void btnback_Click(object sender, EventArgs e)
        {
           
            tblNew.Visible = false;
            tblEdit.Visible = true;
            pnltblNew.Visible = false;
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletiondeptList(string prefixText, int count, string contextKey)
        {
            DataSet ds = AttendanceDAC.SH_EmpGrades_position(prefixText,status);
            DataTable dt = ds.Tables[0];
            List<string> items = new List<string>(count);
            var rtval = new Dictionary<string, string>();
            foreach (DataRow row in dt.Rows)
            {
                string str = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(row["Name"].ToString(), row["ID"].ToString());
                items.Add(str);
            }

            return items.ToArray(); ;// txtItems.ToArray();

        }

        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletiondeptListMedical(string prefixText, int count, string contextKey)
        {
            DataSet ds = AttendanceDAC.SH_EmpGrades_Medical(prefixText, status);
            DataTable dt = ds.Tables[0];
            List<string> items = new List<string>(count);
            var rtval = new Dictionary<string, string>();
            foreach (DataRow row in dt.Rows)
            {
                string str = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(row["Name"].ToString(), row["ID"].ToString());
                items.Add(str);
            }

            return items.ToArray(); ;// txtItems.ToArray();

        }

        protected void btnsearch_Click(object sender, EventArgs e)
        {
            BindGrid();
        }


    }
}