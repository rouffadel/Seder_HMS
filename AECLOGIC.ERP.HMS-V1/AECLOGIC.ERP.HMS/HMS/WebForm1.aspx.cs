using AECLOGIC.HMS.BLL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
namespace AECLOGIC.ERP.HMS.HMS
{
    public partial class WebForm1 : AECLOGIC.ERP.COMMON.WebFormMaster
    {
        protected override void OnInit(EventArgs e)
        {
            ModuleID = 1;
          
            base.OnInit(e);
          
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            lblWarning.Visible = false;
            if (!IsPostBack)
            {
                GetParentMenuId();
                grvEmp.DataSource = GetEmpData();
                grvEmp.DataBind();
            }

        }
        public int GetParentMenuId()
        {
            string URL = Request.Url.Segments[Request.Url.Segments.Length - 1] + Request.Url.Query;
            int RoleId = Convert.ToInt32(Session["RoleId"].ToString());
            int ModuleId = ModuleID; ;



            DataSet ds = AttendanceDAC.GetAllowed(RoleId, ModuleId, URL);
            int MenuId = 0;
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                MenuId = Convert.ToInt32(ds.Tables[0].Rows[0]["Under"]);
            }
            return MenuId;
        }
        public DataSet GetEmpData()
        {
            string cs = ConfigurationManager.AppSettings["strConn"];
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection())
            {
                con.ConnectionString = cs; con.Open();
                using (SqlDataAdapter adapter = new SqlDataAdapter("Select DepartmentUId,DepartmentName from T_G_DepartmentMaster", con))
                { adapter.Fill(dt); };
                con.Close();
            } ds.Tables.Add(dt); return ds;
        }
        public DataSet UpdateData(Int32 id, string fn)
        {
            String strConnString = ConfigurationManager.AppSettings["strConn"];
            SqlConnection con = new SqlConnection(strConnString);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "UpdateDepartmentDetails";
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
            cmd.Parameters.Add("@fn", SqlDbType.VarChar).Value = fn;
            cmd.Connection = con;
            try { con.Open(); cmd.ExecuteNonQuery(); }
            catch (Exception ex) { throw ex; }
            finally { con.Close(); con.Dispose(); }
            DataSet ds = GetEmpData();
            return ds;
        }
        protected void grvEmp_RowEditing(object sender, GridViewEditEventArgs e)
        {
            lblWarning.Visible = false;
            grvEmp.EditIndex = e.NewEditIndex;
            grvEmp.DataSource = GetEmpData();
            grvEmp.DataBind();
        }
        protected void grvEmp_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            var lblID = (Label)grvEmp.Rows[e.RowIndex].FindControl("lblId");
            var txtName = (TextBox)grvEmp.Rows[e.RowIndex].FindControl("txtName");
            int id = 0; int.TryParse(lblID.Text, out id);

            if (txtName.Text.Trim() == "")
            {
                lblWarning.Text = "Please provide valid text";
                lblWarning.Visible = true;
                return;
            }

            grvEmp.DataSource = UpdateData(id, txtName.Text);
            grvEmp.EditIndex = -1;
            grvEmp.DataBind();
        }
        protected void grvEmp_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            grvEmp.EditIndex = -1; grvEmp.DataSource = GetEmpData();
            grvEmp.DataBind();
        }
        protected void grvEmp_SelectedIndexChanged(object sender, EventArgs e)
        {            //var empID = grvEmp.Rows[grvEmp.EditIndex].Cells[0].Text;       
        }
    }
}