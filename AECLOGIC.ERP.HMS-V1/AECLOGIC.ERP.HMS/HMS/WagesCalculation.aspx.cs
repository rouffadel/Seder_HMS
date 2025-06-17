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
    public partial class WagesCalculation : AECLOGIC.ERP.COMMON.WebFormMaster
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
                tblEdit.Visible = true;
                tblNew.Visible = false;
                pnltblNew.Visible = false;
                tblOrder.Visible = false;
                pnltblOrder.Visible = false;
                trFyear.Visible = false;
            }
            if (Convert.ToInt32(Request.QueryString["key"]) == 2)
            {
                tblNew.Visible = true;
                pnltblNew.Visible = true;
                tblEdit.Visible = false;
                tblOrder.Visible = false;
                pnltblOrder.Visible = false;
                trFyear.Visible = false;
            }
            if (!IsPostBack)
            {
                GetParentMenuId();
                ViewState["WPID"] = "";
                BindFinancilayear();
                BindWages();
                if (Convert.ToInt32(Request.QueryString["key"]) == 0)
                {
                    BindGrid();
                    tblEdit.Visible = true;
                    trFyear.Visible = true;
                }
                if (Convert.ToInt32(Request.QueryString["key"]) == 3)
                {
                    trFyear.Visible = false;
                    tblEdit.Visible = false;
                    tblNew.Visible = false;
                    pnltblNew.Visible = false;
                    tblOrder.Visible = true;
                    pnltblOrder.Visible = true;
                    BindList();
                }
            }
        }
        public int GetParentMenuId()
        {
            string URL = Request.Url.Segments[Request.Url.Segments.Length - 1] + Request.Url.Query;
            int RoleId = Convert.ToInt32(Session["RoleId"].ToString());
            int ModuleId = ModuleID;;
          DataSet  ds = AttendanceDAC.GetAllowed(RoleId, ModuleId, URL);
            int MenuId = 0;
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                MenuId = Convert.ToInt32(ds.Tables[0].Rows[0]["Under"]);
                gvLeaveProfile.Columns[3].Visible = Convert.ToBoolean(ds.Tables[0].Rows[0]["Editable"].ToString());
                menuid = MenuId.ToString();
                btnDown.Enabled = btnFirst.Enabled = btnLast.Enabled = btnSubmit.Enabled = btnOrder.Enabled = btnUp.Enabled = Convert.ToBoolean(ds.Tables[0].Rows[0]["Editable"].ToString());
                mid = Convert.ToInt32(ds.Tables[0].Rows[0]["MenuId"].ToString());
            }
            return MenuId;
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
            ddlFinYear.DataTextField = "Name";
            ddlFinYear.DataBind();
            int CurrentFinYear = ds.Tables[0].Rows.Count;
            string FromDate = CodeUtilHMS.ConvertToDate_ddMMMyyy(ds.Tables[0].Rows[0]["FromDate"].ToString(),CodeUtilHMS.DateFormat.ddMMMyyyy).ToString();
            string ToDate = CodeUtilHMS.ConvertToDate_ddMMMyyy(ds.Tables[0].Rows[0]["TODate"].ToString(), CodeUtilHMS.DateFormat.ddMMMyyyy).ToString();
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
        public void BindWages()
        {
          DataSet  ds = PayRollMgr.GetWagesList();
            ddlWages.DataSource = ds;
            ddlWages.DataValueField = "WagesID";
            ddlWages.DataTextField = "Name";
            ddlWages.DataBind();
            ddlWages.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        public void BindGrid()
        {
            tblEdit.Visible = true;
            tblNew.Visible = false;
            pnltblNew.Visible = false;
            DataSet ds = PayRollMgr.GetWages_CalculationsList(Convert.ToInt32(ddlFinYear.SelectedValue));
            gvLeaveProfile.DataSource = ds;
            gvLeaveProfile.DataBind();
            lstDepartments.DataSource = ds;
            lstDepartments.DataTextField = "Name";
            lstDepartments.DataValueField = "WPID";
            lstDepartments.DataBind();
        }
        public void BindList()
        {
            DataSet dset = new DataSet();
            dset = PayRollMgr.GetWages_CalculationsList(Convert.ToInt32(Session["FinYearID"]));
            lstDepartments.DataSource = dset;
            lstDepartments.DataTextField = "Name";
            lstDepartments.DataValueField = "WPID";
            lstDepartments.DataBind();
        }
        public void BindDetails(int ID)
        {
            tblEdit.Visible = false;
            tblNew.Visible = true;
            pnltblNew.Visible = true;
            DataSet ds = PayRollMgr.GetWages_CalculationsDetails(ID);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                double WagesCentage = Convert.ToDouble(ds.Tables[0].Rows[0]["CentageOnCTC"].ToString()) * 100;
                txtCentage.Text = WagesCentage.ToString();
                ddlFinancial.SelectedValue = ds.Tables[0].Rows[0]["FinYearId"].ToString();
                ddlWages.SelectedValue = ds.Tables[0].Rows[0]["WagesID"].ToString();
            }
        }
        protected void gvLeaveProfile_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int ID = Convert.ToInt32(e.CommandArgument);
            ViewState["WPID"] = ID;
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
                int WPID = 0;
                if (ViewState["WPID"].ToString() != null && ViewState["WPID"].ToString() != string.Empty)
                {
                    WPID = Convert.ToInt32(ViewState["WPID"].ToString());
                }
                double WagesPercentage = (Convert.ToDouble(txtCentage.Text)) / (100);
                int OutPut = PayRollMgr.InsUpdWages_Calculations(WPID, Convert.ToInt32(ddlFinancial.SelectedValue), Convert.ToInt32(ddlWages.SelectedValue), WagesPercentage);
                if (OutPut == 1)
                    AlertMsg.MsgBox(Page, "Inserted Sucessfully.!");
                else if (OutPut == 2)
                    AlertMsg.MsgBox(Page, "Already exists.!");
                else
                    AlertMsg.MsgBox(Page, "Updated sucessfull.!");
                BindGrid();
                Clear();
            }
            catch (Exception WagCal)
            {
                AlertMsg.MsgBox(Page, WagCal.Message.ToString(),AlertMsg.MessageType.Error);
            }
        }
        public void Clear()
        {
            txtCentage.Text = "";
            ddlFinancial.SelectedIndex = 0;
            ddlWages.SelectedIndex = 0;
            ViewState["WPID"] = "";
        }
        protected void lnkAdd_Click(object sender, EventArgs e)
        {
            tblNew.Visible = true;
            pnltblNew.Visible = true;
            tblEdit.Visible = false;
            tblOrder.Visible = false;
            pnltblOrder.Visible = false;
        }
        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            tblEdit.Visible = true;
            tblNew.Visible = false;
            pnltblNew.Visible = false;
            tblOrder.Visible = false;
            pnltblOrder.Visible = false;
        }
        protected void lnkOrder_Click(object sender, EventArgs e)
        {
            tblEdit.Visible = false;
            tblNew.Visible = false;
            pnltblNew.Visible = false;
            tblOrder.Visible = true;
            pnltblOrder.Visible = true;
        }
        protected void btnUp_Click(object sender, EventArgs e)
        {
            try
            {
                if (lstDepartments.SelectedIndex != 0 && lstDepartments.SelectedIndex != -1)
                {
                    ListItem item = lstDepartments.SelectedItem;
                    int index = lstDepartments.SelectedIndex;
                    lstDepartments.Items.RemoveAt(index);
                    lstDepartments.Items.Insert(index - 1, item);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected void btnDown_Click(object sender, EventArgs e)
        {
            try
            {
                if (lstDepartments.Items.Count != 0)
                {
                    if (lstDepartments.SelectedIndex != lstDepartments.Items.Count - 1 && lstDepartments.SelectedIndex != -1)
                    {
                        ListItem item = lstDepartments.SelectedItem;
                        int index = lstDepartments.SelectedIndex;
                        lstDepartments.Items.RemoveAt(index);
                        lstDepartments.Items.Insert(index + 1, item);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected void btnFirst_Click(object sender, EventArgs e)
        {
            try
            {
                if (lstDepartments.SelectedIndex != 0 && lstDepartments.SelectedIndex != -1)
                {
                    ListItem item = lstDepartments.SelectedItem;
                    int index = lstDepartments.SelectedIndex;
                    lstDepartments.Items.RemoveAt(index);
                    lstDepartments.Items.Insert(0, item);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected void btnLast_Click(object sender, EventArgs e)
        {
            try
            {
                int Count = lstDepartments.Items.Count;
                if (lstDepartments.Items.Count != 0)
                {
                    if (lstDepartments.SelectedIndex != lstDepartments.Items.Count - 1 && lstDepartments.SelectedIndex != -1)
                    {
                        ListItem item = lstDepartments.SelectedItem;
                        int index = lstDepartments.SelectedIndex;
                        lstDepartments.Items.RemoveAt(index);
                        lstDepartments.Items.Insert(Count - 1, item);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected void btnOrder_Click(object sender, EventArgs e)
        {
            try
            {
                int Count = lstDepartments.Items.Count;
                for (int i = 0; i < Count; i++)
                {
                    int ID = Convert.ToInt32(lstDepartments.Items[i].Value.ToString());
                    int Order = i + 1;
                    PayRollMgr.WagesDisplayoder(ID, Order);
                }
                AlertMsg.MsgBox(Page, "Re-Order Successfully");
            }
            catch (Exception ex1)
            {
                AlertMsg.MsgBox(Page, ex1.Message.ToString(),AlertMsg.MessageType.Error);
            }
        }
        protected void ddlFinYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            int EmpID = Convert.ToInt32(ViewState["EmpID"]);
            DataSet ds = PayRollMgr.GetWages_CalculationsList(Convert.ToInt32(ddlFinYear.SelectedValue));
            gvLeaveProfile.DataSource = ds;
            gvLeaveProfile.DataBind();
        }
        protected void gvLeaveProfile_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton lnkedt = (LinkButton)e.Row.FindControl("lnkEdit");
                lnkedt.Enabled = Editable;
            }
        }
    }
}