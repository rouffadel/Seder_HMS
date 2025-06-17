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
using AECLOGIC.ERP.HMS.HRClasses;
namespace AECLOGIC.ERP.HMS
{
    public partial class DepartmentOrder : AECLOGIC.ERP.COMMON.WebFormMaster
    {
        AttendanceDAC objAtt = new AttendanceDAC();
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
                BindDepartments();
               
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
                btnDown.Enabled = btnDown.Enabled = btnFirst.Enabled = btnLast.Enabled = btnSubmit.Enabled = btnUp.Enabled = (bool)ds.Tables[0].Rows[0]["Editable"];
                btnDown.Visible = btnDown.Visible = btnFirst.Visible = btnLast.Visible = btnSubmit.Visible = btnUp.Visible = (bool)ds.Tables[0].Rows[0]["Editable"];

            }
            return MenuId;
        }
        public void BindDepartments()
        {

            lstDepartments.DataSource = objAtt.GetDepartmentsByPreOrder(0);
            lstDepartments.DataTextField = "DepartmentName";
            lstDepartments.DataValueField = "DepartmentUId";
            lstDepartments.DataBind();


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
        protected void btnSubmit_Click(object sender, EventArgs e)
        {

            try
            {
                int Count = lstDepartments.Items.Count;
                for (int i = 0; i < Count; i++)
                {
                    int DeptID = Convert.ToInt32(lstDepartments.Items[i].Value.ToString());
                    int Order = i + 1;
                    objAtt.DeptDisplayoder(DeptID, Order);

                }
                AlertMsg.MsgBox(Page, "Departments Re-Order Successfully");
            }
            catch (Exception)
            {

                throw;
            }



        }
    }
}