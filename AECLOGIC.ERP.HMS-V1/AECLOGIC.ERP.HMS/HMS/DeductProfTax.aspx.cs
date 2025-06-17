using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using AECLOGIC.HMS.BLL;
namespace AECLOGIC.ERP.HMS
{
    public partial class DeductProfTax : AECLOGIC.ERP.COMMON.WebFormMaster
    {
        int mid = 0;
        bool viewall;
        string menuname;
        string menuid;
        protected override void OnInit(EventArgs e)
        {
            ModuleID = 1;
            base.OnInit(e);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                string id =  Convert.ToInt32(Session["UserId"]).ToString();
            }
            catch
            {
                Response.Redirect("Home.aspx");
            }
            if (Convert.ToInt32(Request.QueryString["key"]) == 1)
            {
                tblNew.Visible = true;
                pnltblNew.Visible = true;
                tblEdit.Visible = false;
            }
            if (!IsPostBack)
            {
                GetParentMenuId();
                ViewState["Id"] = "";
                BindFinancilayear();
                BindStates();
                if (Convert.ToInt32(Request.QueryString["key"]) == 0)
                {
                    tblEdit.Visible = true;
                    BindGrid();
                }
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
                gvAllowances.Columns[6].Visible = Convert.ToBoolean(ds.Tables[0].Rows[0]["Editable"].ToString());
                btnSubmit.Enabled = (bool)ds.Tables[0].Rows[0]["Editable"];
            }
            return MenuId;
        }
        public void BindWages()
        {
           DataSet  ds = PayRollMgr.GetWagesList();
            ddlWages.DataSource = ds;
            ddlWages.DataValueField = "WagesID";
            ddlWages.DataTextField = "Name";
            ddlWages.DataBind();
            ddlWages.Items.Insert(0, new ListItem("--Select--", "0"));
            ddlWages.Items.Add(new ListItem("Gross Pay", "-1"));
        }
        public void BindFinancilayear()
        {
            DataSet ds = PayRollMgr.GetFinacialYearList();
            ddlFinancial.DataSource = ds;
            ddlFinancial.DataValueField = "FinYearId";
            ddlFinancial.DataTextField = "FinancialYear";
            ddlFinancial.DataBind();
            ddlFinancial.Items.Insert(0, new ListItem("--Select--", "0"));
            ddlFinYear.DataSource = ds;
            ddlFinYear.DataValueField = "FinYearId";
            ddlFinYear.DataTextField = "FinancialYear";
            ddlFinYear.DataBind();
            int CurrentFinYear = ds.Tables[0].Rows.Count;
           // string FromDate = CODEUtility.ConvertToDate(ds.Tables[0].Rows[0]["FromDate"].ToString(), DateFormat.DayMonthYear).ToString();
            //string ToDate = CODEUtility.ConvertToDate(ds.Tables[0].Rows[0]["TODate"].ToString(), DateFormat.DayMonthYear).ToString();
            DateTime dfrdt = Convert.ToDateTime(ds.Tables[0].Rows[0]["FromDate"].ToString());
            DateTime dtodt = Convert.ToDateTime(ds.Tables[0].Rows[0]["TODate"].ToString());
            string FromDate = dfrdt.ToString("dd/MM/yyyy");
            string ToDate = dtodt.ToString("dd/MM/yyyy");
            string[] str = new string[3];
            str = ToDate.Split('/');
            string YearTime = str[2];
            string[] strYr = new string[2];
            strYr = YearTime.Split(' ');
            int Year = Convert.ToInt32(strYr[0]);
            int Month = Convert.ToInt32(str[0]);
            int Day = Convert.ToInt32(str[1]);
            int CYear = Convert.ToInt32(DateTime.Now.Year);
            int CMonth = Convert.ToInt32(DateTime.Now.Month);
            int CDay = Convert.ToInt32(DateTime.Now.Day);
            int PreviousFinYear = CurrentFinYear - (Year - CYear);
            if (CYear < Year)
            {
                if (CMonth <= Month)
                {
                    if (CDay <= Day)        //Not Necessary
                    {
                        ddlFinYear.SelectedValue = PreviousFinYear.ToString();
                    }
                }
            }
            else
            {
                ddlFinYear.SelectedIndex = CurrentFinYear;
            }
        }
        public void BindStates()
        {
            DataSet ds = PayRollMgr.GetStatesList();
            ddlState.DataSource = ds;
            ddlState.DataValueField = "stateid";
            ddlState.DataTextField = "statename";
            ddlState.DataBind();
            ddlState.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        public void BindGrid()
        {
            tblEdit.Visible = true;
            tblNew.Visible = false;
            pnltblNew.Visible = false;
            //ds = PayRollMgr.GetDeductProfTaxList(Convert.ToInt32(Session["FinYearID"]));
            DataSet ds = PayRollMgr.GetDeductProfTaxList(Convert.ToInt32(ddlFinYear.SelectedValue));
            gvAllowances.DataSource = ds;
            gvAllowances.DataBind();
        }
        public void BindDetails(int ID)
        {
            tblEdit.Visible = false;
            tblNew.Visible = true;
            pnltblNew.Visible = true;
            DataSet ds = PayRollMgr.GetDeductProfTaxDetails(ID);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                txtMinAmount.Text = ds.Tables[0].Rows[0]["AmtMin"].ToString();
                txtMaxAmount.Text = ds.Tables[0].Rows[0]["AmtMax"].ToString();
                txtAmt.Text = ds.Tables[0].Rows[0]["Amount"].ToString();
                ddlFinancial.SelectedValue = ds.Tables[0].Rows[0]["FinYearId"].ToString();
                ddlState.SelectedValue = ds.Tables[0].Rows[0]["StateId"].ToString();
                ddlWages.SelectedValue = ds.Tables[0].Rows[0]["WagesID"].ToString();
            }
        }
        protected void gvWages_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int ID = Convert.ToInt32(e.CommandArgument);
            ViewState["Id"] = ID;
            if (e.CommandName == "Edt")
            {
                BindDetails(ID);
            }
            else
            {
            }
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                int Id = 0;
                if (ViewState["Id"].ToString() != null && ViewState["Id"].ToString() != string.Empty)
                {
                    Id = Convert.ToInt32(ViewState["Id"].ToString());
                }
                Double? MaxAmt = null;
                if (txtMaxAmount.Text != String.Empty)
                    MaxAmt = Convert.ToDouble(txtMaxAmount.Text);
                int OUtPut = PayRollMgr.InsUpdateDeductProfTax(Id, Convert.ToInt32(ddlState.SelectedValue), Convert.ToInt32(ddlFinancial.SelectedValue), Convert.ToInt32(ddlWages.SelectedValue), Convert.ToDouble(txtMinAmount.Text), MaxAmt, Convert.ToDouble(txtAmt.Text));
                BindGrid();
                Clear();
                if (OUtPut == 1)
                    AlertMsg.MsgBox(Page, "Inserted sucessfully.!");
                else if (OUtPut == 2)
                    AlertMsg.MsgBox(Page, "Already exists.!");
                else if (OUtPut == 3)
                    AlertMsg.MsgBox(Page, "Updated sucessfully.!");
                else
                    AlertMsg.MsgBox(Page, "Range already exists.!");
            }
            catch (Exception DedProof)
            {
                AlertMsg.MsgBox(Page, DedProof.Message.ToString(),AlertMsg.MessageType.Error);
            }
        }
        public void Clear()
        {
            txtMinAmount.Text = "";
            txtMaxAmount.Text = "";
            txtAmt.Text = "";
            ddlFinancial.SelectedIndex = 0;
            ddlState.SelectedIndex = 0;
            ddlWages.SelectedIndex = 0;
            ViewState["Id"] = "";
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
        protected void ddlFinYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            int EmpID = Convert.ToInt32(ViewState["EmpID"]);
            DataSet ds = PayRollMgr.GetDeductProfTaxList(Convert.ToInt32(ddlFinYear.SelectedValue));
            gvAllowances.DataSource = ds;
            gvAllowances.DataBind();
        }
    }
}