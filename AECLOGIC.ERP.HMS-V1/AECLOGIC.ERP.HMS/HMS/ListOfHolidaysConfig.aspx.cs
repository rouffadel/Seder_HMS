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
    public partial class ListOfHolidaysConfig : AECLOGIC.ERP.COMMON.WebFormMaster
    {
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
                //hlnkAdd.Visible = false;
                this.Title = "Add Holidays";
                tblNew.Visible = true;
                pnltblNew.Visible = true;
                tblEdit.Visible = false;
                gvLeaveProfile.Visible = false;
            }
            if (Convert.ToInt32(Request.QueryString["key"]) == 2)
            {
                //hlnkAdd.Visible = false;
                this.Title = "Edit Holidays";
                tblNew.Visible = false;
                pnltblNew.Visible = false;
                tblEdit.Visible = true;
                gvLeaveProfile.Visible = true;
            }
            if (!IsPostBack)
            {
                ViewState["HDId"] = "";
                BindHDType();
                BindEmpNture();
                BindYears();
                if ((Convert.ToInt32(Request.QueryString["key"]) == 2) || (Convert.ToInt32(Request.QueryString["key"]) == 0))
                {
                    tblEdit.Visible = true;
                    BindGrid();
                }
                if (Convert.ToInt32(Request.QueryString["key"]) == 0)
                {
                    //hlnkAdd.Visible = Convert.ToBoolean(ViewState["Editable"]);
                }
            }
        }
        public int GetParentMenuId()
        {
            string URL = Request.Url.Segments[Request.Url.Segments.Length - 1] + Request.Url.Query;
            int RoleId = Convert.ToInt32(Session["RoleId"].ToString());
            int ModuleId = ModuleID;;
         DataSet   ds = AttendanceDAC.GetAllowed(RoleId, ModuleId, URL);
            int MenuId = 0;
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                MenuId = Convert.ToInt32(ds.Tables[0].Rows[0]["Under"]);
                gvLeaveProfile.Columns[4].Visible = Convert.ToBoolean(ds.Tables[0].Rows[0]["Editable"].ToString());
                btnSubmit.Enabled = (bool)ds.Tables[0].Rows[0]["Editable"];
            }
            return MenuId;
        }
        public void BindHDType()
        {
            DataSet ds = Leaves.GetTypeofHolidaysList(1);
            ddlHDType.DataSource = ds;
            ddlHDType.DataTextField = "ShortName";
            ddlHDType.DataValueField = "HDType";
            ddlHDType.DataBind();
            ddlHDType.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        public void BindEmpNture()
        {
            DataSet ds = Leaves.GetEmpNatureList(1);
            ddlProfileType.DataSource = ds;
            ddlProfileType.DataTextField = "Nature";
            ddlProfileType.DataValueField = "NatureOfEmp";
            ddlProfileType.DataBind();
            ddlProfileType.Items.Insert(0, new ListItem("--Select--", "0"));
            // DataSet ds = AttendanceDAC.HR_GetEmpNature();
            ddlEmpNature.DataSource = ds;
            ddlEmpNature.DataValueField = "NatureofEmp";
            ddlEmpNature.DataTextField = "Nature";
            ddlEmpNature.DataBind();
            //ddlEmpNature.Items.Insert(0, new ListItem("---SELECT---", "0", true));
        }
        public void BindGrid()
        {
            DataSet ds = Leaves.GetListofHDList(Convert.ToInt32(ddlYear.SelectedValue), Convert.ToInt32(ddlEmpNature.SelectedValue));
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                gvLeaveProfile.DataSource = ds;
            }
            gvLeaveProfile.DataBind();
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
            ddlYear.SelectedValue = ds.Tables[0].Rows[0]["CurrentYear"].ToString();
        }
        public void BindDetails(int ID)
        {
            tblEdit.Visible = false;
            tblNew.Visible = true;
            pnltblNew.Visible = true;
            DataSet ds = Leaves.GetListofHDDetails(ID);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                txtHoliday.Text = ds.Tables[0].Rows[0]["Holiday"].ToString();
                txtDate.Text = ds.Tables[0].Rows[0]["Date"].ToString();
                ddlHDType.SelectedValue = ds.Tables[0].Rows[0]["HDType"].ToString();
                ddlProfileType.SelectedValue = ds.Tables[0].Rows[0]["NatureOfEmp"].ToString();
                if (ds.Tables[0].Rows[0]["IsActive"].ToString() == "True")
                {
                    chkStatus.Checked = true;
                }
                else
                {
                    chkStatus.Checked = false;
                }
            }
        }
        protected void gvLeaveProfile_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int ID = Convert.ToInt32(e.CommandArgument);
            ViewState["HDId"] = ID;
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
                int HDId = 0;
                int Out = 1; 
                int Status = 1;
                if (ViewState["HDId"].ToString() != null && ViewState["HDId"].ToString() != string.Empty)
                {
                    HDId = Convert.ToInt32(ViewState["HDId"].ToString());
                }
                if (chkStatus.Checked == false)
                {
                    Status = 0;
                }
               Leaves.InsUpdateListofHD(HDId, txtHoliday.Text.Trim(), CODEUtility.ConvertToDate(txtDate.Text, DateFormat.DayMonthYear), Convert.ToInt32(ddlHDType.SelectedValue), Convert.ToInt32(ddlProfileType.SelectedValue), Status);
               if (HDId == 0 && Out == 1)
               {
                   AlertMsg.MsgBox(Page, "Done.!");
               }
               else
               {
                   AlertMsg.MsgBox(Page, "Updated");
               }
                Clear();
                BindGrid();
            }
            catch (Exception LstOfHoliday)
            {
                AlertMsg.MsgBox(Page, LstOfHoliday.Message.ToString(),AlertMsg.MessageType.Error);
            }
        }
        public void Clear()
        {
            txtHoliday.Text = "";
            txtDate.Text = "";
            ddlProfileType.SelectedIndex = 0;
            ddlHDType.SelectedIndex = 0;
            ViewState["HDId"] = "";
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
        protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGrid();
        }
        protected void ddlEmpNature_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGrid();
        }
    }
}