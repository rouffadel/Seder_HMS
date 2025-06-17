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
    public partial class ViewRoles : AECLOGIC.ERP.COMMON.WebFormMaster
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
           
            if (!IsPostBack)
            {
                GetParentMenuId();
                GetRoles();
               
                BindGrid();
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
                grdEmployees.Columns[5].Visible = Convert.ToBoolean(ds.Tables[0].Rows[0]["Editable"].ToString());
                
            }
            return MenuId;
        }
        public void BindGrid()
        {
            try
            {

                DataSet ds = (DataSet)objRights.GetUserRolesList();
                if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0)
                {
                    grdEmployees.DataSource = ds;
                    grdEmployees.DataBind();
                }
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public static DataSet dsRoles = new DataSet();
        public static ArrayList alRoles = new ArrayList();
        public int GetRolesIndex(string Value)
        {
            return alRoles.IndexOf(Value);
        }
        public bool RolesEnabled(string Value)
        {
            if (Value == "1" && Session["RoleId"].ToString() != "1")
                return false;
            else
                return true;
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
       
        protected void grdEmployees_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            GridViewRow row = null;
            DropDownList ddlroles = null;
            if (e.CommandName == "AssignRole" || e.CommandName == "DelRole")
            {
                row = (GridViewRow)((LinkButton)e.CommandSource).Parent.Parent;
                ddlroles = (DropDownList)grdEmployees.Rows[row.RowIndex].FindControl("ddlRoles");
            }

            if (ddlroles.SelectedItem.Value == "1" && Session["RoleId"].ToString() != "1")
            {
                AlertMsg.MsgBox(this.Page, "Sorry! You are unauthorized to change to Administrator role.");
                return;
            }
            if (e.CommandName == "AssignRole")
            {


                objHrCommon.EmpID = Convert.ToDouble(e.CommandArgument);
                
                objHrCommon.RoleID = Convert.ToInt32(ddlroles.SelectedItem.Value);
                objHrCommon.UserID =  Convert.ToInt32(Session["UserId"]);
                objRights.CreateUserRoles(objHrCommon);
                BindGrid();

            }

            if (e.CommandName == "DelRole")
            {

                objHrCommon.EmpID = Convert.ToDouble(e.CommandArgument);
              
                objHrCommon.RoleID = Convert.ToInt32(ddlroles.SelectedItem.Value);
                objHrCommon.UserID =  Convert.ToInt32(Session["UserId"]);
                objRights.DeleteUserRoles(objHrCommon);
                BindGrid();
            }


        }
      
   



    }
}