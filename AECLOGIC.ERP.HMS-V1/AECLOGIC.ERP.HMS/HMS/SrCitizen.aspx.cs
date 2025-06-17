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
    public partial class SrCitizen : AECLOGIC.ERP.COMMON.WebFormMaster
    {
        DataSet ds = new DataSet();
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
            topmenu.MenuId = GetParentMenuId();
            topmenu.ModuleId = ModuleID;;
            topmenu.RoleID = Convert.ToInt32(Session["RoleId"].ToString());
            topmenu.SelectedMenu = Convert.ToInt32(mid.ToString());
            topmenu.DataBind();
            Session["menuname"] = menuname;
            Session["menuid"] = menuid;
            if (!IsPostBack)
            {

                ViewState["Id"] = "";
                tblEdit.Visible = true;
                lnkEdit.CssClass = "lnkselected";
                lnkAdd.CssClass = "linksunselected";
                BindGrid();
                BindAssessYear();

            }
        }
        public int GetParentMenuId()
        {
            string URL = Request.Url.Segments[Request.Url.Segments.Length - 1] + Request.Url.Query;
            int RoleId = Convert.ToInt32(Session["RoleId"].ToString());
            int ModuleId = ModuleID;;

            DataSet ds = new DataSet();

            ds = AttendanceDAC.GetAllowed(RoleId, ModuleId, URL);
            int MenuId = 0;
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                MenuId = Convert.ToInt32(ds.Tables[0].Rows[0]["Under"]);
                ViewState["Editable"] = (bool)ds.Tables[0].Rows[0]["Editable"];
                ViewState["ViewAll"] = (bool)ds.Tables[0].Rows[0]["ViewAll"];
                gvAllowances.Columns[3].Visible = Convert.ToBoolean(ds.Tables[0].Rows[0]["Editable"].ToString());
                viewall = (bool)ViewState["ViewAll"];
                menuname = ds.Tables[0].Rows[0]["menuname"].ToString();
                menuid = MenuId.ToString();
                mid = Convert.ToInt32(ds.Tables[0].Rows[0]["MenuId"].ToString());
            }
            return MenuId;
        }
        public void BindAssessYear()
        {
            ds = PayRollMgr.GetAssessmentYearList();
            ddlAssessmentYear.DataSource = ds;
            ddlAssessmentYear.DataValueField = "AssYearId";
            ddlAssessmentYear.DataTextField = "AssessmentYear";
            ddlAssessmentYear.DataBind();
            ddlAssessmentYear.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        public void BindGrid()
        {
            tblEdit.Visible = true;
            tblNew.Visible = false;
            lnkEdit.CssClass = "lnkselected";
            lnkAdd.CssClass = "linksunselected";

            ds = PayRollMgr.GetSrCitezenList();
            gvAllowances.DataSource = ds;
            gvAllowances.DataBind();
        }
        public void BindDetails(int ID)
        {
            tblEdit.Visible = false;
            tblNew.Visible = true;
            ds = PayRollMgr.GetSrCitezenDetails(ID);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                txtAge.Text = ds.Tables[0].Rows[0]["AgeFrom"].ToString();
                rdblstgender.SelectedValue = ds.Tables[0].Rows[0]["Gender"].ToString();
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
            int Id = 0;
            if (ViewState["Id"].ToString() != null && ViewState["Id"].ToString() != string.Empty)
            {
                Id = Convert.ToInt32(ViewState["Id"].ToString());
            }
            PayRollMgr.InsUpdateSrCitezen(Convert.ToInt32(ddlAssessmentYear.SelectedValue), Convert.ToChar(rdblstgender.SelectedValue), Convert.ToInt32(txtAge.Text), Id);
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
        public void Clear()
        {
            txtAge.Text = "";
            ViewState["Id"] = "";
        }
        protected void lnkAdd_Click(object sender, EventArgs e)
        {

            tblNew.Visible = true;
            tblEdit.Visible = false;
            lnkAdd.CssClass = "lnkselected";
            lnkEdit.CssClass = "linksunselected";
        }
        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            tblEdit.Visible = true;
            tblNew.Visible = false;
            lnkEdit.CssClass = "lnkselected";
            lnkAdd.CssClass = "linksunselected";
        }
    }
}