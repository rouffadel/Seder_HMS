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

    public partial class CompContributionItems : AECLOGIC.ERP.COMMON.WebFormMaster
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
            
            if (!IsPostBack)
            {
                GetParentMenuId();
                ViewState["ContItemid"] = "";
                if (Request.QueryString["Id"] == "1")
                {
                    tblEdit.Visible = false;
                    tblNew.Visible = true;
                    pnltblNew.Visible = true;
                    tblOrder.Visible = false;

                }
                else if (Request.QueryString["Id"] == "2")
                {
                    tblOrder.Visible = true;
                    tblNew.Visible = false;
                    pnltblNew.Visible = false;
                    tblEdit.Visible = false;
                }
                else
                {
                    tblType.Visible = true;
                    tblOrder.Visible = false; ;
                    tblNew.Visible = false;
                    pnltblNew.Visible = false;
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
                gvAllowances.Columns[2].Visible = Convert.ToBoolean(ds.Tables[0].Rows[0]["Editable"].ToString());
                btnDown.Enabled = btnFirst.Enabled = btnLast.Enabled = btnSubmit.Enabled = btnOrder.Enabled = btnUp.Enabled = Convert.ToBoolean(ds.Tables[0].Rows[0]["Editable"].ToString());
                mid = Convert.ToInt32(ds.Tables[0].Rows[0]["MenuId"].ToString());
            }
            return MenuId;
        }
        public void BindGrid()
        {
            tblEdit.Visible = true;
            tblNew.Visible = false;
            pnltblNew.Visible = false;
            tblType.Visible = true;


            int ID = Convert.ToInt32(rbItemsList.SelectedValue);
            if (ID == 1)
            {
                ds = PayRollMgr.GetCoyContributionItemsList(1);
                gvAllowances.DataSource = ds;
                gvAllowances.DataBind();

            }
            else
            {
                ds = PayRollMgr.GetCoyContributionItemsList(2);
                gvAllowances.DataSource = ds;
                gvAllowances.DataBind();
            }





            lstDepartments.DataSource = ds;
            lstDepartments.DataTextField = "Name";
            lstDepartments.DataValueField = "Itemid";
            lstDepartments.DataBind();
        }
        public void BindDetails(int ID)
        {
            tblEdit.Visible = false;
            tblNew.Visible = true;
            pnltblNew.Visible = true;
         DataSet   ds = PayRollMgr.GetCoyContributionItemsDetails(ID);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                txtName.Text = ds.Tables[0].Rows[0]["ShortName"].ToString();
                txtSName.Text = ds.Tables[0].Rows[0]["LongName"].ToString();
                ddlAccess.SelectedValue = ds.Tables[0].Rows[0]["Access"].ToString();
            }
        }
        protected void gvWages_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int ID = Convert.ToInt32(e.CommandArgument);
            ViewState["ContItemid"] = ID;
            if (e.CommandName == "Edt")
            {
                tblType.Visible = false;
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
                int AllowId = 0;
                if (ViewState["ContItemid"].ToString() != null && ViewState["ContItemid"].ToString() != string.Empty)
                {
                    AllowId = Convert.ToInt32(ViewState["ContItemid"].ToString());
                }

                int OutPut = PayRollMgr.InsUpdCoyContributionItems(AllowId, txtName.Text.Trim(), txtSName.Text, Convert.ToInt32(ddlAccess.SelectedValue), Convert.ToInt32(System.Configuration.ConfigurationSettings.AppSettings["CompanyID"].ToString()));
                if (OutPut == 1)
                    AlertMsg.MsgBox(Page, "Inserted sucessfully.!");
                else if (OutPut == 2)
                    AlertMsg.MsgBox(Page, "Already exists.!");
                else
                    AlertMsg.MsgBox(Page, "Updated sucessfully.!");

                BindGrid();
                Clear();

            }
            catch (Exception CompConItems)
            {
                AlertMsg.MsgBox(Page, CompConItems.Message.ToString(),AlertMsg.MessageType.Error);
            }
        }
        public void Clear()
        {
            txtName.Text = "";
            txtSName.Text = "";
            ViewState["ContItemid"] = "";
        }
        protected void lnkAdd_Click(object sender, EventArgs e)
        {

            tblNew.Visible = true;
            pnltblNew.Visible = true;
            tblEdit.Visible = false;
            tblOrder.Visible = false;

        }
        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            tblEdit.Visible = true;
            tblNew.Visible = false;
            pnltblNew.Visible = false;
            tblOrder.Visible = false;

        }
        protected void lnkOrder_Click(object sender, EventArgs e)
        {
            tblEdit.Visible = false;
            tblNew.Visible = false;
            pnltblNew.Visible = false;
            tblOrder.Visible = true;

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
                    PayRollMgr.CoyContributionItemsDisplayoder(ID, Order);
                }
                AlertMsg.MsgBox(Page, "Re-Order Successfully");
            }
            catch (Exception ex)
            {
                AlertMsg.MsgBox(Page, ex.Message.ToString(),AlertMsg.MessageType.Error);
            }



        }
        protected void rbItemsList_SelectedIndexChanged(object sender, EventArgs e)
        {
            int ID = Convert.ToInt32(rbItemsList.SelectedValue);
            if (ID == 1)
            {
                ds = PayRollMgr.GetCoyContributionItemsList(1);
                gvAllowances.DataSource = ds;
                gvAllowances.DataBind();

            }
            else
            {
                ds = PayRollMgr.GetCoyContributionItemsList(2);
                gvAllowances.DataSource = ds;
                gvAllowances.DataBind();
            }
        }
    }
}