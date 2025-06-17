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
using AECLOGIC.HMS.BLL;
namespace AECLOGIC.ERP.HMS
{
    public partial class AccessRights : AECLOGIC.ERP.COMMON.WebFormMaster
    {
        int mid = 0;
        bool viewall;
        string menuname;
        string menuid;
        AttendanceDAC objRights = new AttendanceDAC();
        HRCommon objHrCommon = new HRCommon();
        protected override void OnInit(EventArgs e)
        {
            ModuleID = 1;
            base.OnInit(e);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
          
            try
            {
                if (!IsPostBack)
                {
                    GetParentMenuId();
                    BindWorkSites();
                    BindDepartments();
                }
                GetRoles();
            }
            catch (Exception ex)
            {
                AlertMsg.MsgBox(Page, "Sorry for the inconvience. Try again.\nError:" + ex.Message.ToString(),AlertMsg.MessageType.Error);
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
                grdEmployees.Columns[4].Visible = Convert.ToBoolean(ds.Tables[0].Rows[0]["Editable"].ToString());
            }

            return MenuId;
        }
        public void BindUsers()
        {
            DataSet ds = objRights.GetEmployeeList();
            if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0)
            {

            }

        }
        public static DataSet dsRoles = new DataSet();
        public static ArrayList alRoles = new ArrayList();
        public int GetRolesIndex(string Value)
        {
            return alRoles.IndexOf(Value);
        }

        public DataSet GetRoles()
        {
            alRoles = new ArrayList();
            dsRoles = objRights.GetRoles();
            foreach (DataRow dr in dsRoles.Tables[0].Rows)
                alRoles.Add(dr["RoleID"].ToString());
            return dsRoles;
        }

        public DataSet BindRoles()
        {
            return dsRoles;
        }
        public void BindWorkSites()
        {

            try
            {
                DataSet ds = AttendanceDAC.GetWorkSite(0, '1', Convert.ToInt32(Session["CompanyID"]));
                if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ddlworksites.DataSource = ds.Tables[0];
                    ddlworksites.DataTextField = "Site_Name";
                    ddlworksites.DataValueField = "Site_ID";
                    ddlworksites.DataBind();
                }
                ddlworksites.Items.Insert(0, new ListItem("---ALL---", "0"));

            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public void BindDepartments()
        {
            try
            {
                DataSet ds = (DataSet)objRights.GetDaprtmentList();
                if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ddldepartments.DataValueField = "DepartmentUId";
                    ddldepartments.DataTextField = "DeptName";
                    ddldepartments.DataSource = ds;
                    ddldepartments.DataBind();
                    ddldepartments.Items.Insert(0, new ListItem("---ALL---", "0"));
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        protected void grdEmployees_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "AssignRole")
                {
                    GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).Parent.Parent;
                    DropDownList ddlroles = (DropDownList)grdEmployees.Rows[row.RowIndex].FindControl("ddlRoles");
                    if (ddlroles.SelectedItem.Value == "1" && Session["RoleId"].ToString() != "6")
                    {
                        AlertMsg.MsgBox(this.Page, "Sorry! You are unauthorized to assign Administrator role.");
                        return;
                    }
                    objHrCommon.EmpID = Convert.ToDouble(e.CommandArgument);
                    objHrCommon.RoleID = Convert.ToInt32(ddlroles.SelectedItem.Value);
                    objHrCommon.UserID =  Convert.ToInt32(Session["UserId"]);
                    objRights.CreateUserRoles(objHrCommon);
                }
            }
            catch (Exception ex)
            {
                AlertMsg.MsgBox(Page, "Sorry for the inconvience. Try again.\nError:" + ex.Message.ToString(),AlertMsg.MessageType.Error);
            }
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtempid.Text.Trim() == String.Empty)
                {
                    objHrCommon.EmpID = 0;
                }
                else
                {
                    objHrCommon.EmpID = Convert.ToInt32(txtempid.Text);
                }
                objHrCommon.SiteID = Convert.ToInt32(ddlworksites.SelectedItem.Value);
                objHrCommon.DeptID = Convert.ToDouble(ddldepartments.SelectedItem.Value);
                objHrCommon.FName = txtusername.Text;
                DataSet ds = objRights.SearchEmpList(objHrCommon);
                if (ds != null && ds.Tables.Count != 0)
                {
                    grdEmployees.DataSource = ds;

                } grdEmployees.DataBind();

            }
            catch (Exception ex)
            {
                AlertMsg.MsgBox(Page, "Sorry for the inconvinience. Try again.\nError:" + ex.Message.ToString(),AlertMsg.MessageType.Error);
            }
        }
    }
}