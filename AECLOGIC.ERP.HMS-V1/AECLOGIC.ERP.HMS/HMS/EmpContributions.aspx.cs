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



namespace AECLOGIC.ERP.HMS
{
    public partial class EmpContributions : AECLOGIC.ERP.COMMON.WebFormMaster
    {
        int mid = 0;
        bool viewall;
        string menuname;
        string menuid;
        AttendanceDAC ADAC = new AttendanceDAC();
        int CompanyID = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["CompanyID"].ToString());
        protected override void OnInit(EventArgs e)
        {
            ModuleID = 1;
            base.OnInit(e);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
        
            if (!IsPostBack)
            {
                GetParentMenuId();
                gvApprove.Visible = false;
                BindYears();
                BindEmpList();
                BindApproveEmpList();
                BindItem();
                BindUnit();
                ViewState["Date"] = string.Empty;
                ViewState["EmpID"] = 0;
                if (Convert.ToInt32(Session["RoleId"]) == 8 || Convert.ToInt32(Session["RoleId"]) == 6)
                {
                    BindGrid();
                }
                else
                {
                    BindGridForEmp( Convert.ToInt32(Session["UserId"]));
                }
                gvImburse.Columns[0].Visible = false;
                btnApprove.Visible = false;
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
                btnSubmit.Enabled = (bool)ds.Tables[0].Rows[0]["Editable"];
               
            }
            return MenuId;
        }
        public void BindGrid()
        {

            DataSet ds = AttendanceDAC.HR_GetEmpReimbursement();
            gvImburse.DataSource = ds;
            gvImburse.DataBind();

        }

        public void BindGridForEmp(int EmpID)
        {

            DataSet ds = AttendanceDAC.HR_GetEmpReimbursementByEmpID(EmpID);
            gvImburse.DataSource = ds;
            gvImburse.DataBind();

        }

        public void BindEmpList()
        {
            DataSet ds = new DataSet(); 
            string EmpName = string.Empty;
            ddlEmp.DataSource = ds.Tables[1];
            ddlEmp.DataTextField = "name";
            ddlEmp.DataValueField = "EmpID";
            ddlEmp.DataBind();
            ddlEmp.Items.Insert(0, new ListItem("---SELECT---", "0", true));

        }
        public void BindApproveEmpList()
        {
            DataSet ds = new DataSet();   
            string EmpName = string.Empty;
            ddlSelectEmp.DataSource = ds.Tables[1];
            ddlSelectEmp.DataTextField = "name";
            ddlSelectEmp.DataValueField = "EmpID";
            ddlSelectEmp.DataBind();
            ddlSelectEmp.Items.Insert(0, new ListItem("---SELECT---", "0", true));

        }
        public void BindUnit()
        {
               
            DataSet ds = AttendanceDAC.GetAu();
            ddlUnit.DataSource = ds;
            ddlUnit.DataTextField = "Au_Name";
            ddlUnit.DataValueField = "Au_ID";
            ddlUnit.DataBind();
            ddlUnit.Items.Insert(0, new ListItem("---SELECT---", "0", true));
        }
        public void BindItem()
        {

            DataSet ds = PayRollMgr.GetReimbursementItemsList();
            ddlItem.DataSource = ds.Tables[0];
            ddlItem.DataTextField = "Name";
            ddlItem.DataValueField = "RMItemId";
            ddlItem.DataBind();
            ddlItem.Items.Insert(0, new ListItem("---SELECT---", "0", true));
        }

        public void BindYears()
        {

            DataSet  ds = AttendanceDAC.GetCalenderYear();

            int i = 0;
            int Maxyear = Convert.ToInt32(ds.Tables[0].Rows[0]["MaxYear"].ToString());
            for (int Minyear = Convert.ToInt32(ds.Tables[0].Rows[0]["MinYear"].ToString()); Minyear <= Maxyear; Minyear++)
            {
                ddlYear.Items.Insert(i, new ListItem(Convert.ToString(Minyear), Convert.ToString(Minyear)));
                i = i + 1;
            }
            ddlMonth.SelectedValue = ds.Tables[0].Rows[0]["PreviousMonth"].ToString();
            ddlYear.SelectedValue = ds.Tables[0].Rows[0]["CurrentYear"].ToString();

        }
        protected void txtRate_TextChanged(object sender, EventArgs e)
        {
            if (txtRate.Text != "" && txtQuantity.Text != "")
            {
                txtAmount.Text = Convert.ToInt32(Convert.ToInt32(txtQuantity.Text) * Convert.ToInt32(txtRate.Text)).ToString();
            }
            if (txtRate.Text == "")
            {
                txtRate.Text = "0";
            }
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            int RID = Convert.ToInt32(ViewState["RID"]);
            string Date = ViewState["Date"].ToString();
            DateTime ReportedOn;
            if (RID == 0)
            {
                ReportedOn = DateTime.Now;
            }
            else
            {
                ReportedOn = CODEUtility.ConvertToDate(Date, DateFormat.DayMonthYear);

            }
            int EmpID = Convert.ToInt32(ddlEmp.SelectedItem.Value);
            int Month = Convert.ToInt32(ddlMonth.SelectedItem.Value);
            int Year = Convert.ToInt32(ddlYear.SelectedItem.Value);
            int ReimburseID = Convert.ToInt32(ddlItem.SelectedItem.Value);
            string Amount = txtAmount.Text;
            int UOMID = Convert.ToInt32(ddlUnit.SelectedItem.Value);
            int Quantity = Convert.ToInt32(txtQuantity.Text);
            string UnitRate = txtRate.Text;
            string Explanation = txtExplnation.Text;
            string UnitType = ddlUnit.SelectedItem.Text;
            string ReimburseType = ddlItem.SelectedItem.Text;
           
            String MyString = string.Empty;
            string extension = string.Empty;
            if (fileUpload.HasFile)
            {
                DateTime MyDate = DateTime.Now;
                MyString = MyDate.ToString("ddMMyyhhmmss");
                extension = System.IO.Path.GetExtension(fileUpload.PostedFile.FileName).ToLower();
            }
            if (fileUpload.HasFile)
            {
                string storePath = Server.MapPath("~") + "/" + "EmpReimbureseProof/";
                if (!Directory.Exists(storePath))
                    Directory.CreateDirectory(storePath);
                fileUpload.PostedFile.SaveAs(storePath + "/" + MyString + extension);
            }
            string Proof = MyString + extension;

            AttendanceDAC.HR_InsUpEmpReimbursment(RID, EmpID, Month, Year, ReimburseID, UOMID, Quantity, UnitRate, Explanation, Proof, Amount,  Convert.ToInt32(Session["UserId"]), UnitType, ReimburseType, ReportedOn);

            // AttendanceDAC.HR_InsUpEmpReimbursment(RID, EmpID, Month, Year, ReimburseID, UOMID, Quantity, UnitRate, Explanation, Proof, Amount,  Convert.ToInt32(Session["UserId"]));
            if (Convert.ToInt32(Session["RoleId"]) == 8 || Convert.ToInt32(Session["RoleId"]) == 6)
            {
                BindGrid();
            }
            else
            {
                BindGridForEmp( Convert.ToInt32(Session["UserId"]));
            }
            AlertMsg.MsgBox(Page, "Saved!");

        }
        public string DocNavigateUrl(string Proof)
        {
            string ReturnVal = "";
            string Value = Proof.Split('.')[Proof.Split('.').Length - 1];
            ReturnVal = "./EmpReimbureseProof/" + Proof;
            if (ReturnVal == "./EmpReimbureseProof/")
            {
                return null;
            }
            else
            {
                return ReturnVal;
            }
        }
       
        protected void gvImburse_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "edt")
            {
                int RID = Convert.ToInt32(e.CommandArgument);
                int UserID =  Convert.ToInt32(Session["UserId"]);
                ViewState["RID"] = RID;

                   
              DataSet  ds = AttendanceDAC.HR_GetReimbursByRID(RID);
                ViewState["Date"] = ds.Tables[0].Rows[0]["Date"].ToString();
                txtAmount.Text = ds.Tables[0].Rows[0]["Amount"].ToString();
                txtExplnation.Text = ds.Tables[0].Rows[0]["Explanation"].ToString();
                txtQuantity.Text = ds.Tables[0].Rows[0]["Quantity"].ToString();
                txtRate.Text = ds.Tables[0].Rows[0]["UnitRate"].ToString();
                ddlUnit.SelectedValue = ds.Tables[0].Rows[0]["UOMID"].ToString();
                ddlYear.SelectedValue = ds.Tables[0].Rows[0]["Year"].ToString();
                ddlMonth.SelectedValue = ds.Tables[0].Rows[0]["Month"].ToString();
                ddlItem.SelectedValue = ds.Tables[0].Rows[0]["ReimburseID"].ToString();
                ddlEmp.SelectedValue = ds.Tables[0].Rows[0]["EmpID"].ToString();
            }

            if (e.CommandName == "Del")
            {
                int RID = Convert.ToInt32(e.CommandArgument);
                int UserID =  Convert.ToInt32(Session["UserId"]);
                   
                AttendanceDAC.HR_DelEmpReimburse(RID);
                if (Convert.ToInt32(Session["RoleId"]) == 8 || Convert.ToInt32(Session["RoleId"]) == 6)
                {
                    BindGrid();
                }
                else
                {
                    BindGridForEmp( Convert.ToInt32(Session["UserId"]));
                }
                btnCancel_Click(sender, e);
                AlertMsg.MsgBox(Page, "Deleted!");
            }
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            txtAmount.Text = "";
            txtExplnation.Text = "";
            txtQuantity.Text = "";
            txtRate.Text = "";
            ddlUnit.SelectedIndex = 0;
            ddlYear.SelectedIndex = 0;
            ddlMonth.SelectedIndex = 0;
            ddlItem.SelectedIndex = 0;
            ddlEmp.SelectedIndex = 0;
        }
        protected void txtQuantity_TextChanged(object sender, EventArgs e)
        {
            if (txtRate.Text != "" && txtQuantity.Text != "")
            {
                txtAmount.Text = Convert.ToInt32(Convert.ToInt32(txtQuantity.Text) * Convert.ToInt32(txtRate.Text)).ToString();
            }
            if (txtQuantity.Text == "")
            {
                txtQuantity.Text = "0";
            }
        }

        protected void gvImburse_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.Header)
                {
                    CheckBox chkMail = (CheckBox)e.Row.FindControl("chkSelectAll");
                    chkMail.Attributes.Add("onclick", String.Format("javascript:return SelectAll(this,'{0}','chkSelectOne');", gvImburse.ClientID));
                }
            }
            catch (Exception Ex)
            {
                //report error
            }
        }
        protected void btnApprove_Click(object sender, EventArgs e)
        {
            DataSet ds = new DataSet("ApproveDateset");
            DataTable dt = new DataTable("ApproveTable");
            dt.Columns.Add(new DataColumn("RID", typeof(System.Int32)));
            ds.Tables.Add(dt);

            int EmpID = 0;
            Double TotAmount = 0;
            string AllRIDs = "";
            string ReimburseTypes = "";
            foreach (GridViewRow gvRow in gvImburse.Rows)
            {
                CheckBox chk = new CheckBox();
                chk = (CheckBox)gvRow.FindControl("chkSelectOne");
                if (chk.Checked)
                {
                  

                    ViewState["EmpID"] = int.Parse(gvImburse.DataKeys[gvRow.RowIndex][2].ToString());
                    DataRow dr = dt.NewRow();
                    dr["EmpID"] = Convert.ToInt32(ViewState["EmpID"]);
                    dt.Rows.Add(dr);
                }

            }
          

            ds.AcceptChanges();
           
            gvApprove.DataSource = ds;
            gvApprove.DataBind();
            tblApprove.Visible = true;
            gvApprove.Visible = true;


        }

        protected void btnShow_Click(object sender, EventArgs e)
        {
            if (txtEmp.Text == "")
            {
                int EmpID = Convert.ToInt32(ddlSelectEmp.SelectedValue);
                   
              DataSet  ds = AttendanceDAC.HR_GetEmpReimbursementByEmpID(EmpID);
                gvImburse.DataSource = ds;
                gvImburse.DataBind();
                gvImburse.Columns[0].Visible = true;
                btnApprove.Visible = true;
                btnShow.Visible = true;
            }
            else
            {
                int EmpID = Convert.ToInt32(txtEmp.Text);

                DataSet ds = AttendanceDAC.HR_GetEmpReimbursementByEmpID(EmpID);
                gvImburse.DataSource = ds;
                gvImburse.DataBind();
                gvImburse.Columns[0].Visible = true;
                btnApprove.Visible = true;
                btnShow.Visible = true;
            }

        }

    }
}