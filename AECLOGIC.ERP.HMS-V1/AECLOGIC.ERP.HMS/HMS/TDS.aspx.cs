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
    public partial class TDS : AECLOGIC.ERP.COMMON.WebFormMaster
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
            if (!IsPostBack)
            {
                GetParentMenuId();
                ViewState["Id"] = "";
                tblEdit.Visible = true;
                lnkEdit.CssClass = "lnkselected";
                lnkAdd.CssClass = "linksunselected";
                BindAssessYear();
                BindAssessType();
                BindGrid();
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
                gvAllowances.Columns[5].Visible = (bool)ds.Tables[0].Rows[0]["Editable"];
            }
            return MenuId;
        }
        public void BindAssessYear()
        {
            DataSet ds = PayRollMgr.GetFinacialYearList();
            ddlAssessmentYear.DataSource = ds;
            ddlAssessmentYear.DataValueField = "FinYearId";
            ddlAssessmentYear.DataTextField = "Name";
            ddlAssessmentYear.DataBind();
            ddlAssessmentYear.Items.Insert(0, new ListItem("--Select--", "0"));
            ddlFinYear.DataSource = ds;
            ddlFinYear.DataValueField = "FinYearId";
            ddlFinYear.DataTextField = "Name";
            ddlFinYear.DataBind();
            int CurrentFinYear = ds.Tables[0].Rows.Count;
          //  string FromDate = CODEUtility.ConvertToDate(ds.Tables[0].Rows[0]["FromDate"].ToString(), DateFormat.DayMonthYear).ToString();
           // string ToDate = CODEUtility.ConvertToDate(ds.Tables[0].Rows[0]["TODate"].ToString(), DateFormat.DayMonthYear).ToString();
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
        public void BindAssessType()
        {
            DataSet ds = PayRollMgr.GetAssessmentTypeList();
            ddlAssessmentType.DataSource = ds;
            ddlAssessmentType.DataValueField = "AssesseId";
            ddlAssessmentType.DataTextField = "Assesse";
            ddlAssessmentType.DataBind();
            ddlAssessmentType.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        public void BindGrid()
        {
            tblEdit.Visible = true;
            tblNew.Visible = false;
            pnltblNew.Visible = false;
            lnkEdit.CssClass = "lnkselected";
            lnkAdd.CssClass = "linksunselected";
            DataSet ds = PayRollMgr.GetTDSList(Convert.ToInt32(ddlFinYear.SelectedValue));
            gvAllowances.DataSource = ds;
            gvAllowances.DataBind();
        }
        public void BindDetails(int ID)
        {
            tblEdit.Visible = false;
            tblNew.Visible = true;
            pnltblNew.Visible = true;
            DataSet ds = PayRollMgr.GetTDSDetails(ID);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                txtRangeFrom.Text = ds.Tables[0].Rows[0]["RangeFrom"].ToString();
                txtRangeTo.Text = ds.Tables[0].Rows[0]["RangeTo"].ToString();
                double Rate = Convert.ToDouble(ds.Tables[0].Rows[0]["Rate"].ToString()) * 100;
                txtRate.Text = Rate.ToString();
                ddlAssessmentYear.SelectedValue = ds.Tables[0].Rows[0]["AssYearId"].ToString();
                ddlAssessmentType.SelectedValue = ds.Tables[0].Rows[0]["AssesseId"].ToString();
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
        public void BindTDS()
        {
            try
            {
                int Id = 0;
                if (ViewState["Id"].ToString() != null && ViewState["Id"].ToString() != string.Empty)
                {
                    Id = Convert.ToInt32(ViewState["Id"].ToString());
                }
                double RangeTo = 0;
                if (txtRangeTo.Text != "")
                    RangeTo = Convert.ToDouble(txtRangeTo.Text);
                double Rate = (Convert.ToDouble(txtRate.Text)) / (100);
                int output = PayRollMgr.InsUpdTDS(Id, Convert.ToInt32(ddlAssessmentType.SelectedValue), Convert.ToInt32(ddlAssessmentYear.SelectedValue), Convert.ToDouble(txtRangeFrom.Text), RangeTo, Rate);
                BindGrid();
                if (output == 1)
                    AlertMsg.MsgBox(Page, "Inserted sucessfully.!");
                else if (output == 2)
                    AlertMsg.MsgBox(Page, "Already exists.!");
                else if (output == 3)
                    AlertMsg.MsgBox(Page, "Updated.");
                else
                    AlertMsg.MsgBox(Page, "Range already exists.!");
                Clear();
            }
            catch (Exception TDS)
            {
                AlertMsg.MsgBox(Page, TDS.Message.ToString(),AlertMsg.MessageType.Error);
            }
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            BindTDS();
        }
        protected void ddlFinYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            int EmpID = Convert.ToInt32(ViewState["EmpID"]);
            DataSet ds = PayRollMgr.GetTDSList(Convert.ToInt32(ddlFinYear.SelectedValue));
            gvAllowances.DataSource = ds;
            gvAllowances.DataBind();
        }
        public void Clear()
        {
            txtRangeFrom.Text = "";
            txtRangeTo.Text = "";
            txtRate.Text = "";
            ddlAssessmentType.SelectedIndex = 0;
            ddlAssessmentYear.SelectedIndex = 0;
            ViewState["Id"] = "";
        }
        protected void lnkAdd_Click(object sender, EventArgs e)
        {
            tblNew.Visible = true;
            pnltblNew.Visible = true;
            tblEdit.Visible = false;
            lnkAdd.CssClass = "lnkselected";
            lnkEdit.CssClass = "linksunselected";
        }
        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            tblEdit.Visible = true;
            tblNew.Visible = false;
            pnltblNew.Visible = false;
            lnkEdit.CssClass = "lnkselected";
            lnkAdd.CssClass = "linksunselected";
        }
    }
}
