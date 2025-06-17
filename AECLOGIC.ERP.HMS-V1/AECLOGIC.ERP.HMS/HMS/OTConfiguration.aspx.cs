using AECLOGIC.HMS.BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
namespace AECLOGIC.ERP.HMS.HMS
{
    public partial class OTConfiguration : AECLOGIC.ERP.COMMON.WebFormMaster
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
                ViewState["OTConfigID"] = "";
                BindFinancilayear();
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
            int ModuleId = ModuleID; ;
          DataSet  ds = AttendanceDAC.GetAllowed(RoleId, ModuleId, URL);
            int MenuId = 0;
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                MenuId = Convert.ToInt32(ds.Tables[0].Rows[0]["Under"]);
               Editable = (bool)ds.Tables[0].Rows[0]["Editable"];
                gvLeaveProfile.Columns[3].Visible = Convert.ToBoolean(ds.Tables[0].Rows[0]["Editable"].ToString());
                btnDown.Enabled = btnFirst.Enabled = btnLast.Enabled = btnSubmit.Enabled = btnOrder.Enabled = btnUp.Enabled = Convert.ToBoolean(ds.Tables[0].Rows[0]["Editable"].ToString());
                mid = Convert.ToInt32(ds.Tables[0].Rows[0]["MenuId"].ToString());
            }
            return MenuId;
        }
        public void BindFinancilayear()
        {
            DataSet ds = PayRollMgr.GetWagesList();
            DataRow dr = ds.Tables[0].NewRow();
            dr["WagesID"] = 0;
            dr["LongName"] = "Salary";
            ds.Tables[0].Rows.Add(dr);
            ds.Tables[0].AcceptChanges();
            ddlWages.DataSource = ds.Tables[0];
            ddlWages.DataTextField = "LongName";
            ddlWages.DataValueField = "WagesID";
            ddlWages.DataBind();
            ddlWages.Items.Insert(0, new ListItem("--Select--", "-1"));
            ds = Leaves.T_G_Leaves_GetTypeOfLeavesList_OT();
            ddlLeaveType.DataSource = ds;
            ddlLeaveType.DataTextField = "Name";
            ddlLeaveType.DataValueField = "LeaveType";
            ddlLeaveType.DataBind();
            ddlLeaveType.Items.Insert(0, new ListItem("--Select--", "0"));
            ds = PayRollMgr.GetOTDetails(0);
            ddlOTTypes.DataSource = ds;
            ddlOTTypes.DataTextField = "Name";
            ddlOTTypes.DataValueField = "OTID";
            ddlOTTypes.DataBind();
            ddlOTTypes.Items.Insert(0, new ListItem("--Select--", "-1"));
        }
        public void BindGrid()
        {
            tblEdit.Visible = true;
            tblNew.Visible = false;
            pnltblNew.Visible = false;
           DataSet ds = PayRollMgr.HR_GetMasterOT(0);
            gvLeaveProfile.DataSource = ds;
            gvLeaveProfile.DataBind();
            lstDepartments.DataSource = ds;
            lstDepartments.DataTextField = "OTName";
            lstDepartments.DataValueField = "OTConfigID";
            lstDepartments.DataBind();
        }
        public void BindList()
        {
            DataSet dset = PayRollMgr.HR_GetMasterOT(0);
            lstDepartments.DataSource = dset;
            lstDepartments.DataTextField = "OTName";
            lstDepartments.DataValueField = "OTConfigID";
            lstDepartments.DataBind();
        }
        public void BindDetails(int ID)
        {
            try
            {
                tblEdit.Visible = false;
                tblNew.Visible = true;
                pnltblNew.Visible = true;
                DataSet ds = PayRollMgr.HR_GetMasterOT(ID);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    double WagesCentage = Convert.ToDouble(ds.Tables[0].Rows[0]["OTValue"].ToString());// * 100;
                    txtOTValue.Text = WagesCentage.ToString();
                    txtCentage.Text = ds.Tables[0].Rows[0]["OTName"].ToString();
                    ddlWages.SelectedValue = ds.Tables[0].Rows[0]["CalOn"].ToString();
                    ddlLeaveType.SelectedValue = ds.Tables[0].Rows[0]["LeavTypeID"].ToString();
                    ddlOTTypes.SelectedValue = ds.Tables[0].Rows[0]["OTID"].ToString();
                    double OTSDValue = Convert.ToDouble(ds.Tables[0].Rows[0]["OTSDValue"].ToString());// * 100;
                    txtOTSDValue.Text = OTSDValue.ToString();
                }
            }
            catch { }
        }
        protected void gvLeaveProfile_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int ID = Convert.ToInt32(e.CommandArgument);
                ViewState["OTConfigID"] = ID;
                if (e.CommandName == "Edt")
                {
                    BindDetails(ID);
                }
                else
                {
                }
            }
            catch { }
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                int WPID = 0;
                if (ViewState["OTConfigID"].ToString() != null && ViewState["OTConfigID"].ToString() != string.Empty)
                {
                    WPID = Convert.ToInt32(ViewState["OTConfigID"].ToString());
                }
                double WagesPercentage = (Convert.ToDouble(txtOTValue.Text));
                double OTSDPercentage = (Convert.ToDouble(txtOTSDValue.Text));
                //int OutPut = 0;
                int OutPut = PayRollMgr.HR_OTConfigurationCreation(WPID, Convert.ToInt32(ddlLeaveType.SelectedValue),
                    txtCentage.Text, WagesPercentage, "", Convert.ToInt32(ddlOTTypes.SelectedValue), Convert.ToInt32(ddlWages.SelectedValue), OTSDPercentage);
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
            ddlWages.SelectedIndex = 0;
            ViewState["OTConfigID"] = "";
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
            DataSet ds = PayRollMgr.HR_GetMasterOT(0);
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