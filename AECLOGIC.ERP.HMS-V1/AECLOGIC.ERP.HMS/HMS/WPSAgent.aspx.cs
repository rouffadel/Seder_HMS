using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using AECLOGIC.HMS.BLL;

namespace AECLOGIC.ERP.HMS
{
    public partial class Default2 : AECLOGIC.ERP.COMMON.WebFormMaster
    {
        int mid = 0;
        bool viewall, Editable;
        string menuname;
        string menuid;
        bool Status;
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
                BindGrid();
            }
        }

        public void BindGrid()
        {
            DataSet dsAgent = new DataSet();
            dsAgent = AttendanceDAC.GetWPSDetails(null);
            gvEditdept.DataSource = dsAgent;
            gvEditdept.DataBind();
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
                gvEditdept.Columns[1].Visible = Convert.ToBoolean(ds.Tables[0].Rows[0]["Editable"].ToString());
                menuid = MenuId.ToString();
                mid = Convert.ToInt32(ds.Tables[0].Rows[0]["MenuId"].ToString());
            }
            return MenuId;
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            int ID = 0;
            int retVal;
            if (ViewState["ID"].ToString() != "")
            {
                ID = Convert.ToInt32(ViewState["ID"].ToString());
            }

            retVal = Convert.ToInt32(AttendanceDAC.HR_InsUpdWPSDetails(Convert.ToInt32(Session["CompanyID"]), txtAgentID.Text.Trim(), ID));
            if (retVal == 1 || retVal == 2)
            {
                AlertMsg.MsgBox(Page, "Done !");
            }
            else
            {
                AlertMsg.MsgBox(Page, "Already Exist !");
            }
            BindGrid();
            txtAgentID.Text = "";
            ViewState["ID"] = "";
        }
        protected void gvEditdept_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int ID = Convert.ToInt32(e.CommandArgument);
            if (e.CommandName == "Edt")
            {
                ViewState["ID"] = ID;
                DataSet dsdets = new DataSet();
                dsdets = AttendanceDAC.GetWPSDetails(ID);
                if (dsdets != null && dsdets.Tables[0].Rows.Count > 0 && dsdets.Tables.Count > 0)
                {
                    txtAgentID.Text = dsdets.Tables[0].Rows[0]["AgentID"].ToString();
                }
            }
        }
    }
}