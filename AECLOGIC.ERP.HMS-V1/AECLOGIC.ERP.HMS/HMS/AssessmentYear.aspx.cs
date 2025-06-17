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
    public partial class AssessmentYear : AECLOGIC.ERP.COMMON.WebFormMaster
    {
        #region Declaration
        int mid = 0;
        bool viewall;
        string menuname;
        string menuid;
        #endregion Declaration

        #region Pageload
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!IsPostBack)
            {
                if (Convert.ToInt32(Request.QueryString["id"]) == 1)
                {
                    dvAdd.Visible = false;
                    dvEdit.Visible = true;
                }
                else
                {
                    dvAdd.Visible = true;
                    dvEdit.Visible = false;
                }
                GetParentMenuId();
                ViewState["FinYearId"] = "";
                BindGrid();
            }
        }
        #endregion Pageload
        protected override void OnInit(EventArgs e)
        {
            ModuleID = 1;
            base.OnInit(e);
        }

        #region Supporting Methods
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
            }
            return MenuId;
        }
        public void BindGrid()
        {
            int assigyearid = 0; if (txtsearchassignment.Text.Trim() != "")
            { assigyearid = Convert.ToInt32(ddlaasignid_hid.Value == "" ? "0" : ddlaasignid_hid.Value); }
            DataSet ds = PayRollMgr.HR_Get_AssessmentYearList_New(assigyearid);
            gvFinancialYear.DataSource = ds;
            gvFinancialYear.DataBind();
        }
        public void BindDetails(int FinYearId)
        {
            dvEdit.Visible = false;
            dvAdd.Visible = true;
            DataSet ds = PayRollMgr.GetAssessmentYearDetails(FinYearId);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                txtFromDate.Text = ds.Tables[0].Rows[0]["FromDate"].ToString();
                txtToDate.Text = ds.Tables[0].Rows[0]["ToDate"].ToString();
                txtName.Text = ds.Tables[0].Rows[0]["AssessmentYear"].ToString();
            }
        }
        public void Clear()
        {
            txtFromDate.Text = "";
            txtToDate.Text = "";
            ViewState["FinYearId"] = "";
        }
        #endregion Supporting Methods

        #region Events
        protected void gvFinancialYear_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int FinYearId = Convert.ToInt32(e.CommandArgument);
            ViewState["FinYearId"] = FinYearId;
            if (e.CommandName == "Edt")
            {
                BindDetails(FinYearId);
            }
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            int? FinYearId = null;
            if (ViewState["FinYearId"].ToString() != null && ViewState["FinYearId"].ToString() != string.Empty)
            {
                FinYearId = Convert.ToInt32(ViewState["FinYearId"].ToString());
            }
            int Output = PayRollMgr.InsUpdateAssessmentYear(FinYearId, CodeUtilHMS.ConvertToDate_ddMMMyyy(txtFromDate.Text.Trim(), CodeUtilHMS.DateFormat.ddMMMyyyy), CodeUtilHMS.ConvertToDate_ddMMMyyy(txtToDate.Text.Trim(), CodeUtilHMS.DateFormat.ddMMMyyyy), txtName.Text);
            if (Output == 1)
                AlertMsg.MsgBox(Page, "Inserted sucessfully.!");
            else if (Output == 2)
                AlertMsg.MsgBox(Page, "Already exists.!",AlertMsg.MessageType.Warning);
            else
                AlertMsg.MsgBox(Page, "Updated sucessfully.!");
            BindGrid();
            Clear();
            dvEdit.Visible = true;
            dvAdd.Visible = false;
        }
        #endregion Events

        protected void txtFromDate_TextChanged(object sender, EventArgs e)
        {
          DateTime frmdate=  CodeUtilHMS.ConvertToDate_ddMMMyyy(txtFromDate.Text.Trim(), CodeUtilHMS.DateFormat.ddMMMyyyy);
          txtToDate.Text = frmdate.AddYears(1).ToString("dd MMM yyyy");
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletiondeptList(string prefixText, int count, string contextKey)
        {
            DataSet ds = AttendanceDAC.SH_CalenderYear(prefixText);
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
