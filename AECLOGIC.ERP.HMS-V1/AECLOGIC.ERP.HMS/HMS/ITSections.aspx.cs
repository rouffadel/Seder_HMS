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
    public partial class ITSections : AECLOGIC.ERP.COMMON.WebFormMaster
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
                BindAssessYear();
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
                gvAllowances.Columns[3].Visible = Convert.ToBoolean(ds.Tables[0].Rows[0]["Editable"].ToString());
                btnSubmit.Enabled = (bool)ds.Tables[0].Rows[0]["Editable"];
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
          //  string ToDate = CODEUtility.ConvertToDate(ds.Tables[0].Rows[0]["TODate"].ToString(), DateFormat.DayMonthYear).ToString();
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
        public void BindGrid()
        {
            tblEdit.Visible = true;
            tblNew.Visible = false;
            pnltblNew.Visible = false;
            DataSet ds = PayRollMgr.GetIT_SectionsList(Convert.ToInt32(ddlFinYear.SelectedValue));
            //ds = PayRollMgr.GetIT_SectionsList(Convert.ToInt32(Session["FinYearID"]));
            gvAllowances.DataSource = ds;
            gvAllowances.DataBind();
        }
        public void BindDetails(int ID)
        {
            tblEdit.Visible = false;
            tblNew.Visible = true;
            pnltblNew.Visible = true;
            DataSet ds = PayRollMgr.GetIT_SectionsDetails(ID);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                txtSectionName.Text = ds.Tables[0].Rows[0]["SectionName"].ToString();
                txtLimit.Text = ds.Tables[0].Rows[0]["Sectionlimit"].ToString();
                ddlAssessmentYear.SelectedValue = ds.Tables[0].Rows[0]["AssYearId"].ToString();
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
                PayRollMgr.InsUpdateIT_Sections(Id, txtSectionName.Text.Trim(), Convert.ToDouble(txtLimit.Text), Convert.ToInt32(ddlAssessmentYear.SelectedValue));
                BindGrid();
                Clear();
                if (Id == 0)
                {
                    AlertMsg.MsgBox(Page, "Done.! ");
                }
                else
                {
                    AlertMsg.MsgBox(Page, "Updated");
                }
            }
            catch (Exception ITSections)
            {
                AlertMsg.MsgBox(Page, ITSections.Message.ToString(),AlertMsg.MessageType.Error);
            }
        }
        public void Clear()
        {
            txtLimit.Text = "";
            txtSectionName.Text = "";
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
            DataSet ds = PayRollMgr.GetIT_SectionsList(Convert.ToInt32(ddlFinYear.SelectedValue));
            gvAllowances.DataSource = ds;
            gvAllowances.DataBind();
        }
    }
}
