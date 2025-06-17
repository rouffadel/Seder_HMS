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
    public partial class CompContribution : AECLOGIC.ERP.COMMON.WebFormMaster
    {
         
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
                tblEdit.Visible = false;
                tblOrder.Visible = false;


            }
            if (Convert.ToInt32(Request.QueryString["key"]) == 2)
            {
                tblEdit.Visible = false;
                tblNew.Visible = false;
                tblOrder.Visible = true;
                BindList();

            }
            if (!IsPostBack)
            {
                GetParentMenuId();
                ViewState["Id"] = "";
                BindFinancilayear();
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
                btnDown.Enabled = btnFirst.Enabled = btnLast.Enabled = btnSubmit.Enabled = btnOrder.Enabled = btnUp.Enabled = Convert.ToBoolean(ds.Tables[0].Rows[0]["Editable"].ToString());
            }
            return MenuId;
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
        }
        public void BindParollItems()
        {
            DataSet ds = PayRollMgr.GetCoyContributionItemsList(1); //Company Contribution Items
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


            DataSet ds = PayRollMgr.GetCoyContributionList(Convert.ToInt32(Session["FinYearID"]));
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
            DataSet ds = PayRollMgr.GetCoyContributionDetails(ID);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                txtAmtLimit.Text = ds.Tables[0].Rows[0]["AmountLimit"].ToString();
                txtContributionRate.Text = ds.Tables[0].Rows[0]["ContrRate"].ToString();
                txtWageLimit.Text = ds.Tables[0].Rows[0]["WageLimit"].ToString();
                ddlFinancial.SelectedValue = ds.Tables[0].Rows[0]["FinYearId"].ToString();
                ddlItem.SelectedValue = ds.Tables[0].Rows[0]["Itemid"].ToString();
                ddlWages.SelectedValue = ds.Tables[0].Rows[0]["WagesID"].ToString();
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
                PayRollMgr.InsUpdateCoyContribution(Id, Convert.ToInt32(ddlItem.SelectedValue), Convert.ToInt32(ddlFinancial.SelectedValue), Convert.ToInt32(ddlWages.SelectedValue), Convert.ToDouble(txtContributionRate.Text), Convert.ToDouble(txtWageLimit.Text), Convert.ToDouble(txtAmtLimit.Text));
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
            catch (Exception ComCon)
            {
                AlertMsg.MsgBox(Page, ComCon.Message.ToString(),AlertMsg.MessageType.Error);
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
            tblEdit.Visible = false;
            tblOrder.Visible = false;

        }
        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            tblEdit.Visible = true;
            tblNew.Visible = false;
            tblOrder.Visible = false;

        }

        protected void lnkOrder_Click(object sender, EventArgs e)
        {
            tblEdit.Visible = false;
            tblNew.Visible = false;
            tblOrder.Visible = true;

        }
        protected void ddlFinYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            int EmpID = Convert.ToInt32(ViewState["EmpID"]);
            DataSet ds = PayRollMgr.GetCoyContributionList(Convert.ToInt32(ddlFinYear.SelectedValue));
            gvAllowances.DataSource = ds;
            gvAllowances.DataBind();
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
                    PayRollMgr.CoyContributionDisplayoder(ID, Order);
                }
                AlertMsg.MsgBox(Page, "Re-Order Successfully");
            }
            catch (Exception ComOrd)
            {
                AlertMsg.MsgBox(Page, ComOrd.Message.ToString(),AlertMsg.MessageType.Error);
            }



        }
    }
}