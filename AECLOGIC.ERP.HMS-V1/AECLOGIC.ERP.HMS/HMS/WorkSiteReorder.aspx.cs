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
    public partial class WorkSiteReorder : AECLOGIC.ERP.COMMON.WebFormMaster
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
                BindWorkSites();
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
               
                menuid = MenuId.ToString();
                mid = Convert.ToInt32(ds.Tables[0].Rows[0]["MenuId"].ToString());
                btnDown.Enabled = btnDown.Enabled = btnFirst.Enabled = btnLast.Enabled = btnSubmit.Enabled = btnUp.Enabled = (bool)ds.Tables[0].Rows[0]["Editable"];

            }
            return MenuId;
        }
        public void BindWorkSites()
        {
            lstWorkStes.DataSource = objAtt.GetWorkSiteOrder(0, '1');
            lstWorkStes.DataTextField = "Site_Name";
            lstWorkStes.DataValueField = "Site_ID";
            lstWorkStes.DataBind();


        }

        protected void btnUp_Click(object sender, EventArgs e)
        {
            try
            {
                if (lstWorkStes.SelectedIndex != 0 && lstWorkStes.SelectedIndex != -1)
                {

                    ListItem item = lstWorkStes.SelectedItem;
                    int index = lstWorkStes.SelectedIndex;
                    lstWorkStes.Items.RemoveAt(index);
                    lstWorkStes.Items.Insert(index - 1, item);
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
                if (lstWorkStes.Items.Count != 0)
                {

                    if (lstWorkStes.SelectedIndex != lstWorkStes.Items.Count - 1 && lstWorkStes.SelectedIndex != -1)
                    {

                        ListItem item = lstWorkStes.SelectedItem;
                        int index = lstWorkStes.SelectedIndex;
                        lstWorkStes.Items.RemoveAt(index);
                        lstWorkStes.Items.Insert(index + 1, item);
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
                if (lstWorkStes.SelectedIndex != 0 && lstWorkStes.SelectedIndex != -1)
                {

                    ListItem item = lstWorkStes.SelectedItem;
                    int index = lstWorkStes.SelectedIndex;
                    lstWorkStes.Items.RemoveAt(index);
                    lstWorkStes.Items.Insert(0, item);
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
                int Count = lstWorkStes.Items.Count;
                if (lstWorkStes.Items.Count != 0)
                {

                    if (lstWorkStes.SelectedIndex != lstWorkStes.Items.Count - 1 && lstWorkStes.SelectedIndex != -1)
                    {

                        ListItem item = lstWorkStes.SelectedItem;
                        int index = lstWorkStes.SelectedIndex;

                        lstWorkStes.Items.RemoveAt(index);
                        lstWorkStes.Items.Insert(Count - 1, item);
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
                int Count = lstWorkStes.Items.Count;
                for (int i = 0; i < Count; i++)
                {
                    int SiteID = Convert.ToInt32(lstWorkStes.Items[i].Value.ToString());
                    int Order = i + 1;
                    objAtt.WorksiteDisplayoder(SiteID, Order);

                }
                AlertMsg.MsgBox(Page, "Worksites Re-Order Successfully");
            }
            catch (Exception)
            {

                throw;
            }



        }
    }
}