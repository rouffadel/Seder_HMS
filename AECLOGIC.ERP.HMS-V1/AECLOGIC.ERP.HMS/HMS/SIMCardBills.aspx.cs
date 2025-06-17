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
    public partial class SIMCardBills : AECLOGIC.ERP.COMMON.WebFormMaster
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

                ViewState["ID"] = "";
                tblEdit.Visible = true;
                lnkEdit.CssClass = "lnkselected";
                lnkAdd.CssClass = "linksunselected";
                BindYears();
                BindMobileNo(0);
                BindGrid(0, 0, 0);

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
                
                gvLeaveProfile.Columns[5].Visible = Convert.ToBoolean(ds.Tables[0].Rows[0]["Editable"].ToString());
               
            }
            return MenuId;
        }
        public void BindMobileNo(int EmpID)
        {
            DataSet ds = AttendanceDAC.GetEmpSimsList(EmpID);
            ddlmobileNo.DataSource = ds;
            ddlmobileNo.DataValueField = "CSID";
            ddlmobileNo.DataTextField = "MobileNo";
            ddlmobileNo.DataBind();
            ddlmobileNo.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        public void BindYears()
        {
            DataSet ds = AttendanceDAC.GetCalenderYear();

            int i = 0;
            int Maxyear = Convert.ToInt32(ds.Tables[0].Rows[0]["MaxYear"].ToString());
            for (int Minyear = Convert.ToInt32(ds.Tables[0].Rows[0]["MinYear"].ToString()); Minyear <= Maxyear; Minyear++)
            {
                ddlYear.Items.Insert(i, new ListItem(Convert.ToString(Minyear), Convert.ToString(Minyear)));
                i = i + 1;
            }
            ddlMonth.SelectedValue = ds.Tables[0].Rows[0]["CurrentMonth"].ToString();
            ddlYear.SelectedValue = ds.Tables[0].Rows[0]["CurrentYear"].ToString();

        }
        public void BindGrid(int EmpID, int Month, int Year)
        {
            tblEdit.Visible = true;
            tblNew.Visible = false;

            DataSet ds = AttendanceDAC.GetSimBillsList(EmpID, Month, Year);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                gvLeaveProfile.DataSource = ds;
            }
            gvLeaveProfile.DataBind();

        }

        public void BindDetails(int ID)
        {
            tblEdit.Visible = false;
            tblNew.Visible = true;
            DataSet ds = AttendanceDAC.GetSimBillsDetails(ID);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                txtEmpID.Text = ds.Tables[0].Rows[0]["EmpID"].ToString();
                ddlmobileNo.SelectedValue = ds.Tables[0].Rows[0]["MobileNo"].ToString();
                ddlMonth.SelectedValue = ds.Tables[0].Rows[0]["Month"].ToString();
                txtAmountLt.Text = ds.Tables[0].Rows[0]["BillAmount"].ToString();
                ddlYear.SelectedValue = ds.Tables[0].Rows[0]["Year"].ToString();
            }
        }
        protected void gvLeaveProfile_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int ID = Convert.ToInt32(e.CommandArgument);
            ViewState["ID"] = ID;
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
            int ID = 0;
            if (ViewState["ID"].ToString() != null && ViewState["ID"].ToString() != string.Empty)
            {
                ID = Convert.ToInt32(ViewState["ID"].ToString());
            }

            if (ID == 0)
            {
                AlertMsg.MsgBox(Page, "Done.! ");
            }
            else
            {
                AlertMsg.MsgBox(Page, "Updated");

            }
            Clear();
            BindGrid(0, 0, 0);

        }
        protected void txtEmpID_TextChanged(object sender, EventArgs e)
        {
            int EmpID = Convert.ToInt32(txtEmpID.Text);
            BindMobileNo(EmpID);
        }
        public void Clear()
        {
            txtEmpID.Text = "";
            ddlmobileNo.SelectedValue = "0";
            ddlMonth.SelectedValue = "0";
            txtAmountLt.Text = "";

            ViewState["ID"] = "";
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
