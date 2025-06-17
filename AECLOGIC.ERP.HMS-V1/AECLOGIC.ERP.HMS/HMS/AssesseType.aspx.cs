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
    public partial class AssesseType : AECLOGIC.ERP.COMMON.WebFormMaster
    {
        int mid = 0;
        bool viewall;
        string menuname;
        string menuid;
        bool Editable;
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
                gvFinancialYear.Columns[3].Visible = Convert.ToBoolean(ds.Tables[0].Rows[0]["Editable"].ToString());
                Editable = (bool)ds.Tables[0].Rows[0]["Editable"];
                btnSubmit.Enabled = (bool)ds.Tables[0].Rows[0]["Editable"];
                btnSubmit.Enabled = Editable;
            }
            return MenuId;
        }
        public void BindGrid()
        {
            tblEdit.Visible = true;
            tblNew.Visible = false;
            pnltblNew.Visible = false;


            DataSet  ds = PayRollMgr.GetAssessmentTypeList();
            gvFinancialYear.DataSource = ds;
            gvFinancialYear.DataBind();
        }
        public void BindDetails(int Id)
        {
            tblEdit.Visible = false;
            tblNew.Visible = true;
            pnltblNew.Visible = true;
            DataSet ds = PayRollMgr.GetAssessmentTypeDetails(Id);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                txtName.Text = ds.Tables[0].Rows[0]["Assesse"].ToString();
                if (ds.Tables[0].Rows[0]["SrCitezen"].ToString() != "")
                {
                    rdblSrCitizen.SelectedValue = ds.Tables[0].Rows[0]["SrCitezen"].ToString();
                }
                else
                {
                    rdblSrCitizen.SelectedValue = "False";
                }
                rdblstgender.SelectedValue = ds.Tables[0].Rows[0]["Gender"].ToString();
                txtAge.Text = ds.Tables[0].Rows[0]["Age"].ToString();
            }
        }
        protected void gvFinancialYear_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int Id = Convert.ToInt32(e.CommandArgument);
            ViewState["Id"] = Id;
            if (e.CommandName == "Edt")
            {
                BindDetails(Id);
            }
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                int Id = 0;
                if (!string.IsNullOrEmpty(ViewState["Id"].ToString()))
                {
                    Id = Convert.ToInt32(ViewState["Id"].ToString());
                }
                int Age = Convert.ToInt32(txtAge.Text);
                int Output = PayRollMgr.InsUpdAssessType(Id, txtName.Text, Convert.ToBoolean(rdblSrCitizen.SelectedValue), Convert.ToChar(rdblstgender.SelectedValue), Age);
                if (Output == 1)
                    AlertMsg.MsgBox(Page, "Inserted sucessfully.!");
                else if (Output == 2)
                    AlertMsg.MsgBox(Page, "Already exists.!");
                else
                    AlertMsg.MsgBox(Page, "Updated sucessfully.!");
                BindGrid();
                Clear();
            }
            catch (Exception AssType)
            {
                AlertMsg.MsgBox(Page, AssType.Message.ToString(),AlertMsg.MessageType.Error);
            }
        }
        public void Clear()
        {
            txtName.Text = "";
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
        protected void gvFinancialYear_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton lnkedt = (LinkButton)e.Row.FindControl("lnkEdit");
                lnkedt.Enabled = Editable;
            }
        }
    }
}