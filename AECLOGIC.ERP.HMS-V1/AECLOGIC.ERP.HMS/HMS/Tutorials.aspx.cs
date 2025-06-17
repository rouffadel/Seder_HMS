using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DL=DataAccessLayer;
using AECLOGIC.HMS.BLL;

namespace AECLOGIC.ERP.HMS
{
    public partial class Tutorials : AECLOGIC.ERP.COMMON.WebFormMaster
    {
        int MenuId = 0;
        int ModuleId = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["ModuleId"]);
        protected override void OnInit(EventArgs e)
        {
            ModuleID = 1;
            base.OnInit(e);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["menu"] != null && Request.QueryString["menu"] != string.Empty)
                {
                    MenuId = Convert.ToInt32(Request.QueryString["menu"]);
                }
            }
            GridBind();
        }
        private void GridBind()
        {
            DataSet ds = new DataSet();

            ds = AttendanceDAC.CP_GetTutorials(ModuleId, MenuId);
            gvTutirials.DataSource = ds;
            gvTutirials.DataBind();
        }

        public string ShowVideos(string Path)
        {

            string returnfile = "";

            if (Path != null)
            {

                // returnfile = Server.MapPath("./Tutorials/") + Path;
                returnfile = "../../Tutorials/" + Path;
            }

            return "javascript:return window.location='Download.aspx?file=" + returnfile + "'; return false;";

        }
    }
}