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
    public partial class CompContrib : AECLOGIC.ERP.COMMON.WebFormMaster
    {
         
        AjaxDAL Aj = new AjaxDAL();
       
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
                if (Convert.ToInt32(Request.QueryString["key"]) == 1)
                {
                    tblNew.Visible = true;
                    pnltblNew.Visible = true;
                    tblEdit.Visible = false;
                    tblOrder.Visible = false;
                    pnltblOrder.Visible = false;
                    Bind();
                }
                if (Convert.ToInt32(Request.QueryString["key"]) == 2)
                {
                    tblEdit.Visible = false;
                    tblNew.Visible = false;
                    pnltblNew.Visible = false;
                    tblOrder.Visible = true;
                    pnltblOrder.Visible = true;
                    BindList();

                }
                //topmenu.MenuId = GetParentMenuId();
                //topmenu.ModuleId = ModuleID;;
                //topmenu.RoleID = Convert.ToInt32(Session["RoleId"].ToString());
                //topmenu.SelectedMenu = Convert.ToInt32(mid.ToString());
                //topmenu.DataBind();
                //Session["menuname"] = menuname;
                //Session["menuid"] = menuid;
                ddlWages.Attributes.Add("onchange", "javascritpt:return ShowWages(this,'" + WagesAccordion.ClientID + "');");

                //changes
                ViewState["Id"] = "";
                BindFinancilayear();
                // BindWages();
                BindParollItems();
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
               // btnDown.Enabled = btnFirst.Enabled = btnLast.Enabled = btnSubmit.Enabled = btnOrder.Enabled = btnUp.Enabled = Convert.ToBoolean(ds.Tables[0].Rows[0]["Editable"].ToString());

            }
            return MenuId;
        }

        void Bind()
        {
            ListItem listItem = null;
            int ContriID = 0;
            if (ddlItem.SelectedValue != "")
            {
                ContriID = Convert.ToInt32(ddlItem.SelectedValue);
            }
            //cblWages
            using (DataSet ds = PayRollMgr.GetEmpWagesByContribution(ContriID))
            {
                cblWages.Items.Clear();
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    listItem = new ListItem(dr["Name"].ToString(), dr["WagesId"].ToString());
                    listItem.Selected = Convert.ToBoolean(dr["IsActive"]);
                    cblWages.Items.Add(listItem);


                }
            }
            //cblAllowences
            using (DataSet ds = PayRollMgr.GetEmpAllowancesByContribution(ContriID))
            {
                cblAllowences.Items.Clear();
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    listItem = new ListItem(dr["Name"].ToString(), dr["AllowId"].ToString());
                    listItem.Selected = Convert.ToBoolean(dr["IsActive"]);
                    cblAllowences.Items.Add(listItem);
                }
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
            ddlFinYear.DataTextField = "Name";
            ddlFinYear.DataBind();

            int CurrentFinYear = ds.Tables[0].Rows.Count;
            //string FromDate = CODEUtility.ConvertToDate(ds.Tables[0].Rows[0]["FromDate"].ToString(), DateFormat.DayMonthYear).ToString();
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
        public void BindParollItems()
        {
            DataSet ds = PayRollMgr.GetCoyContributionItemsList(1);
            ddlItem.DataSource = ds;
            ddlItem.DataValueField = "Itemid";
            ddlItem.DataTextField = "Name";
            ddlItem.DataBind();
            ddlItem.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        public void BindGrid()
        {
            tblEdit.Visible = true;
            tblNew.Visible = false;
            pnltblNew.Visible = false;

            DataSet ds = PayRollMgr.GetCoyContributionList(Convert.ToInt32(ddlFinYear.SelectedValue));
            gvAllowances.DataSource = ds;
            gvAllowances.DataBind();

            lstDepartments.DataSource = ds;
            lstDepartments.DataTextField = "payrollitem";
            lstDepartments.DataValueField = "CCIId";
            lstDepartments.DataBind();

        }
        public void BindList()
        {
            DataSet dset = PayRollMgr.GetCoyContributionList(Convert.ToInt32(Session["FinYearID"]));
            lstDepartments.DataSource = dset;
            lstDepartments.DataTextField = "payrollitem";
            lstDepartments.DataValueField = "CCIId";
            lstDepartments.DataBind();

        }
        public void BindDetails(int ID)
        {
            tblEdit.Visible = false;
            tblNew.Visible = true;
            pnltblNew.Visible = true;
            DataSet ds = PayRollMgr.GetCoyContributionDetails(ID);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                txtAmtLimit.Text = ds.Tables[0].Rows[0]["AmountLimit"].ToString();
                double CompConRate = Convert.ToDouble(ds.Tables[0].Rows[0]["ContrRate"].ToString()) * 100;
                txtContributionRate.Text = CompConRate.ToString();
                //txtContributionRate.Text = ds.Tables[0].Rows[0]["ContrRate"].ToString();
                txtWageLimit.Text = ds.Tables[0].Rows[0]["WageLimit"].ToString();
                ddlFinancial.SelectedValue = ds.Tables[0].Rows[0]["FinYearId"].ToString();
                ddlItem.SelectedValue = ds.Tables[0].Rows[0]["Itemid"].ToString();
                ddlWages.SelectedValue = ds.Tables[0].Rows[0]["WagesID"].ToString();
                if (ddlWages.SelectedValue == "3")
                {
                    WagesAccordion.Style["display"] = "block";

                }
                Bind();
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
                double CompContribRate = (Convert.ToDouble(txtContributionRate.Text)) / (100);
                int Output = PayRollMgr.InsUpdateCoyContribution(Id, Convert.ToInt32(ddlItem.SelectedValue), Convert.ToInt32(ddlFinancial.SelectedValue), Convert.ToInt32(ddlWages.SelectedValue), CompContribRate, Convert.ToDouble(txtWageLimit.Text), Convert.ToDouble(txtAmtLimit.Text));
                if (Output == 1)
                    AlertMsg.MsgBox(Page, "Inserted sucessfully.!");
                else if (Output == 2)
                    AlertMsg.MsgBox(Page, "Already exists.!");
                else
                    AlertMsg.MsgBox(Page, "Updated sucessfully.!");
                if (ddlWages.SelectedValue == "3")
                {
                    Save();
                }
                BindGrid();
                Clear();
            }
            catch (Exception CompContrib)
            {
                AlertMsg.MsgBox(Page, CompContrib.Message.ToString(),AlertMsg.MessageType.Error);
            }


        }
        public void Clear()
        {
            txtAmtLimit.Text = "";
            txtContributionRate.Text = "";
            txtWageLimit.Text = "";
            ddlFinancial.SelectedIndex = 0;
            ddlItem.SelectedIndex = 0;
            ddlWages.SelectedIndex = 0;
            ViewState["Id"] = "";
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
        protected void ddlFinYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            int EmpID = Convert.ToInt32(ViewState["EmpID"]);
            DataSet ds = PayRollMgr.GetCoyContributionList(Convert.ToInt32(ddlFinYear.SelectedValue));
            gvAllowances.DataSource = ds;
            gvAllowances.DataBind();
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
                    PayRollMgr.CoyContributionDisplayoder(ID, Order);
                }
                AlertMsg.MsgBox(Page, "Re-Order Successfully");
            }
            catch (Exception ex1)
            {
                AlertMsg.MsgBox(Page, ex1.Message.ToString(),AlertMsg.MessageType.Error);
            }



        }
        protected void ddlItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlWages.SelectedValue == "3")
            {
                WagesAccordion.Style["display"] = "block";
                Bind();
            }
        }

        public void Save()
        {
            string ContributionID = ddlItem.SelectedValue;

            //wages
            foreach (ListItem lstItm in cblWages.Items)
            {
                string wageid = lstItm.Value;
                bool Access = false;
                if (lstItm.Selected == true)
                {
                    Access = true;
                }
                Aj.ConfigWagesContributiion(ContributionID, wageid, Access);
            }
            //Allowances
            foreach (ListItem lstItm in cblAllowences.Items)
            {
                string allowid = lstItm.Value;
                bool Access = false;
                if (lstItm.Selected == true)
                {
                    Access = true;
                }
                Aj.ConfigAllowancesContributiion(ContributionID, allowid, Access);
            }
          
        }
    }
}