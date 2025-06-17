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
    public partial class Wages : AECLOGIC.ERP.COMMON.WebFormMaster
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
                btnSubmit.Visible = true;
                gvWages.Visible = false;

            }
           
            if (!IsPostBack)
            {
                GetParentMenuId();
                ViewState["WagesID"] = "";
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
                gvWages.Columns[2].Visible = Convert.ToBoolean(ds.Tables[0].Rows[0]["Editable"].ToString());
                menuid = MenuId.ToString();
                btnSubmit.Enabled = (bool)ds.Tables[0].Rows[0]["Editable"];
                mid = Convert.ToInt32(ds.Tables[0].Rows[0]["MenuId"].ToString());
            }
            return MenuId;
        }
        public void BindGrid()
        {
            tblEdit.Visible = true;
            tblNew.Visible = false;
            pnltblNew.Visible = false;
           DataSet ds = PayRollMgr.GetWagesList();
            gvWages.DataSource = ds;
            gvWages.DataBind();
        }
        public void BindDetails(int ID)
        {
            tblEdit.Visible = false;
            tblNew.Visible = true;
            pnltblNew.Visible = true;
            DataSet ds = PayRollMgr.GetWagesDetails(ID);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                txtName.Text = ds.Tables[0].Rows[0]["Name"].ToString();
                txtSName.Text = ds.Tables[0].Rows[0]["LongName"].ToString();
            }
        }
        protected void gvWages_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int ID = Convert.ToInt32(e.CommandArgument);
            ViewState["WagesID"] = ID;
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
                int WagesID = 0;
                if (ViewState["WagesID"].ToString() != null && ViewState["WagesID"].ToString() != string.Empty)
                {
                    WagesID = Convert.ToInt32(ViewState["WagesID"].ToString());
                }
                int CompanyID = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["CompanyID"].ToString());

                int Output = PayRollMgr.InsUpdWages(WagesID, txtName.Text.Trim(), txtSName.Text, CompanyID);
                
                if (Output == 1)
                    AlertMsg.MsgBox(Page, "Inserted sucessfull.!");
                else if (Output == 2)
                    AlertMsg.MsgBox(Page, "Already exists.!");
                else
                    AlertMsg.MsgBox(Page, "Udated sucessfully.!");
                BindGrid();
                Clear();
               
            }
            catch (Exception Wages)
            {
                AlertMsg.MsgBox(Page, Wages.Message.ToString(),AlertMsg.MessageType.Error);
            }

        }
        public void Clear()
        {
            txtName.Text = "";
            txtSName.Text = "";
            ViewState["WagesID"] = "";
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
    }
}