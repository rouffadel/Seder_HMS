using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using AECLOGIC.HMS.BLL;

namespace AECLOGIC.ERP.HMS
{
    public partial class Advertisements : AECLOGIC.ERP.COMMON.WebFormMaster
    {
        AttendanceDAC objDoc = new AttendanceDAC();
        HRCommon objHrCommon = new HRCommon();
        DataSet ds = new DataSet();
        int mid = 0;
        bool viewall;
        string menuname;
        string menuid;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Convert.ToInt32(Request.QueryString["key"]) == 1)
            {
                mvAddvtizmnt.ActiveViewIndex = 0;

            }
           
            if (!IsPostBack)
            {
                GetParentMenuId();
                ViewState["AdvtID"] = "";
                if (Convert.ToInt32(Request.QueryString["key"]) != 1)
                {
                    mvAddvtizmnt.ActiveViewIndex = 1;
                    Bindgrid();
                }

            }
        }
        protected override void OnInit(EventArgs e)
        {
            ModuleID = 1;
            base.OnInit(e);
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
                btnSubmit.Enabled = (bool)ds.Tables[0].Rows[0]["Editable"];
            }
            return MenuId;
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["AdvtID"].ToString() != "")
                {
                    int AdvtID = Convert.ToInt32(ViewState["AdvtID"].ToString());
                    string AdvtName = txtAdvtName.Text;
                    String Description = DocEditor.Text;
                    DateTime Date = DateTime.Now;
                    AttendanceDAC.InsUpAdvt(AdvtID, Date, AdvtName, Description);

                }
                else
                {
                    int AdvtID = 0;
                    string AdvtName = txtAdvtName.Text;
                    String Description = DocEditor.Text;
                    DateTime Date = DateTime.Now;
                    AttendanceDAC.InsUpAdvt(AdvtID, Date, AdvtName, Description);

                }
                AlertMsg.MsgBox(Page, "Done!");
                Bindgrid();
                ViewState["AdvtID"] = "";
                DocEditor.Text = "";
                Response.Redirect("Advertisements.aspx");
            }
            catch (Exception Adv)
            {
                AlertMsg.MsgBox(Page, Adv.Message.ToString(),AlertMsg.MessageType.Error);
            }

        }

        public void Bindgrid()
        {
            ds = AttendanceDAC.GetAdvertisements();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                gvAdvtment.DataSource = ds;
            }
            gvAdvtment.DataBind();
        }
        protected void gvAdvtment_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int AdvtID = Convert.ToInt32(e.CommandArgument);
                int Status = 0;
                if (e.CommandName == "Edt")
                {

                    mvAddvtizmnt.ActiveViewIndex = 0;
                    BindDetails(AdvtID);
                    Bindgrid();
                    btnSubmit.Text = "Update";
                }
                if (e.CommandName == "Status")
                {
                    GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).Parent.Parent;
                    LinkButton lnkstatus = (LinkButton)gvAdvtment.Rows[row.RowIndex].FindControl("lnkstatus");
                    if (lnkstatus.Text == "Active")
                    {
                        Status = 0;
                    }
                    else
                    {
                        Status = 1;
                    }
                    AttendanceDAC.DeleteAdvt(AdvtID, Status);

                    Bindgrid();
                }
            }
            catch (Exception)
            {

                throw;
            }

        }

        public void BindDetails(int AdvtID)
        {
            ds = AttendanceDAC.GetAdvertisementById(AdvtID);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                txtAdvtName.Text = ds.Tables[0].Rows[0]["AdvtName"].ToString();
                DocEditor.Text = ds.Tables[0].Rows[0]["Description"].ToString();
                ViewState["AdvtID"] = ds.Tables[0].Rows[0]["AdvtID"].ToString();
            }
        }


    }
}