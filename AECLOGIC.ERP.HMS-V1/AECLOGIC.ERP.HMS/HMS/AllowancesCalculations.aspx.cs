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
    public partial class AllowancesCalculations : AECLOGIC.ERP.COMMON.WebFormMaster
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
                tblOrder.Visible = false;
                pnltblOrder.Visible = false;
            }
            if (!IsPostBack)
            {
                GetParentMenuId();
                ViewState["AllowCalcId"] = "";
                BindFinancilayear();
                BindAllowances();
                if (Convert.ToInt32(Request.QueryString["key"]) == 0)
                {
                    tblEdit.Visible = true;
                    BindGrid();
                }
                if (Convert.ToInt32(Request.QueryString["key"]) == 2)
                {
                    tblEdit.Visible = false;
                    tblNew.Visible = false;
                    pnltblNew.Visible = false;
                    tblOrder.Visible = true;
                    pnltblOrder.Visible = true;
                    BindList();
                    gvAllowances.Visible = false;
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
                gvAllowances.Columns[5].Visible = Convert.ToBoolean(ds.Tables[0].Rows[0]["Editable"].ToString());
                btnDown.Enabled = btnFirst.Enabled = btnLast.Enabled = btnSubmit.Enabled = btnOrder.Enabled = btnUp.Enabled = Convert.ToBoolean(ds.Tables[0].Rows[0]["Editable"].ToString());
            }
            return MenuId;
        }
        public void BindWages()
        {
            DataSet ds = PayRollMgr.GetWagesList();
            ddlWages.DataSource = ds;
            ddlWages.DataValueField = "WagesID";
            ddlWages.DataTextField = "Name";
            ddlWages.DataBind();
            ddlWages.Items.Insert(0, new ListItem("--Select--", "0"));
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
            string FromDate = CodeUtilHMS.ConvertToDate_ddMMMyyy(ds.Tables[0].Rows[0]["FromDate"].ToString(), CodeUtilHMS.DateFormat.ddMMMyyyy).ToString();
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
        public void BindAllowances()
        {
            DataSet ds = PayRollMgr.GetAllowancesList();
            ddlAllowances.DataSource = ds;
            ddlAllowances.DataValueField = "AllowId";
            ddlAllowances.DataTextField = "Name";
            ddlAllowances.DataBind();
            ddlAllowances.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        public void BindGrid()
        {
            tblEdit.Visible = true;
            tblNew.Visible = false;
            pnltblNew.Visible = false;
            DataSet ds = PayRollMgr.GetAllowance_CalculationsList(Convert.ToInt32(ddlFinYear.SelectedValue));
            gvAllowances.DataSource = ds;
            gvAllowances.DataBind();
            lstDepartments.DataSource = ds;
            lstDepartments.DataTextField = "Name";
            lstDepartments.DataValueField = "AllowCalcId";
            lstDepartments.DataBind();
        }
        public void BindList()
        {
            DataSet dset = PayRollMgr.GetAllowance_CalculationsList(Convert.ToInt32(Session["FinYearID"]));
            lstDepartments.DataSource = dset;
            lstDepartments.DataTextField = "Name";
            lstDepartments.DataValueField = "AllowCalcId";
            lstDepartments.DataBind();
        }
        public void BindDetails(int ID)
        {
            tblEdit.Visible = false;
            tblNew.Visible = true;
            pnltblNew.Visible = true;
            DataSet ds = PayRollMgr.GetAllowance_CalculationsDetails(ID);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                txtLimited.Text = ds.Tables[0].Rows[0]["LimitedTo"].ToString();
                double AllowPer = Convert.ToDouble(ds.Tables[0].Rows[0]["Percentage"].ToString()) * 100;
                txtPercentage.Text = AllowPer.ToString();
                ddlFinancial.SelectedValue = ds.Tables[0].Rows[0]["FinYearId"].ToString();
                ddlAllowances.SelectedValue = ds.Tables[0].Rows[0]["AllowId"].ToString();
                ddlWages.SelectedValue = ds.Tables[0].Rows[0]["CalculatedOn"].ToString();
            }
        }
        protected void gvWages_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int ID = Convert.ToInt32(e.CommandArgument);
            ViewState["AllowCalcId"] = ID;
            if (e.CommandName == "Edt")
            {
                BindDetails(ID);
            }
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                int AllowCalcId = 0;
                if (ViewState["AllowCalcId"].ToString() != null && ViewState["AllowCalcId"].ToString() != string.Empty)
                {
                    AllowCalcId = Convert.ToInt32(ViewState["AllowCalcId"].ToString());
                }
                double AllowancesPer = (Convert.ToDouble(txtPercentage.Text)) / (100);
                if (txtLimited.Text == "")
                    txtLimited.Text = "0";
                int OutPut = PayRollMgr.InsUpdAllowance_Calculation(AllowCalcId, Convert.ToInt32(ddlAllowances.SelectedValue), Convert.ToInt32(ddlFinancial.SelectedValue), AllowancesPer, Convert.ToInt32(ddlWages.SelectedValue), Convert.ToDouble(txtLimited.Text));
                if (OutPut == 1)
                    AlertMsg.MsgBox(Page, "Inserted sucessfully.!");
                else if (OutPut == 2)
                    AlertMsg.MsgBox(Page, "Already exists.!");
                else
                    AlertMsg.MsgBox(Page, "Updated sucessfully.!");
                BindGrid();
                Clear();
            }
            catch (Exception All)
            {
                AlertMsg.MsgBox(Page, All.Message.ToString(),AlertMsg.MessageType.Error);
            }
        }
        public void Clear()
        {
            txtLimited.Text = "";
            txtPercentage.Text = "";
            ddlFinancial.SelectedIndex = 0;
            ddlAllowances.SelectedIndex = 0;
            ViewState["AllowCalcId"] = "";
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
                    PayRollMgr.AllowancesCalDisplayoder(ID, Order);
                }
                AlertMsg.MsgBox(Page, "Re-Order Successfully");
            }
            catch (Exception ex)
            {
                AlertMsg.MsgBox(Page, ex.Message.ToString(),AlertMsg.MessageType.Error);
            }
        }
        protected void ddlFinYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            int EmpID = Convert.ToInt32(ViewState["EmpID"]);
            DataSet ds = ds = PayRollMgr.GetAllowance_CalculationsList(Convert.ToInt32(ddlFinYear.SelectedValue));
            gvAllowances.DataSource = ds;
            gvAllowances.DataBind();
        }
    }
}