using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;
//using HumanResource;
//using DataAccessLayer;
using Aeclogic.Common.DAL;
using System.Configuration;
namespace AECLOGIC.ERP.COMMON
{
    public partial class topmenu : System.Web.UI.UserControl
    {

        private int _RoleID;
        private int _MenuId;
        private int _ModuleId;
        private int _SelectedMenu;
        #region public
        public int SelectedMenu
        {
            get { return _SelectedMenu; }
            set { _SelectedMenu = value; }
        }
        public int ModuleId
        {
            get { return _ModuleId; }
            set { _ModuleId = value; }

        }
        public int RoleID
        {
            get { return _RoleID; }
            set { _RoleID = value; }

        }
        public int MenuId
        {
            get { return _MenuId; }
            set { _MenuId = value; }
        }
        #endregion public
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void DataBind()
        {
            //  AttendanceDAC objTopMenu = new AttendanceDAC();
            //DataAccessLayer.CP_DAL objdl = new DataAccessLayer.CP_DAL();
            pnltopmenu.Controls.Clear();

            // using (DataSet ds = AttendanceDAC.GetLinks(MenuId, RoleID))
            Common cmnObj = new Common();
            using (DataSet ds = cmnObj.GetLinks(MenuId, RoleID))
            {
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    foreach (DataRow dr in ds.Tables[1].Rows)
                    {
                        int menuid = 0, parent = 0;
                        menuid = Convert.ToInt32(dr["MenuId"].ToString());
                        parent = Convert.ToInt32(dr["Parent"].ToString());
                        if (pnltopmenu.Controls.Count > 0)
                        {
                            Label lbl = new Label();
                            lbl.Text = " | ";
                            lbl.ID = UniqueID;
                            pnltopmenu.Controls.Add(lbl);
                        }
                        HyperLink lnk = new HyperLink();
                        lnk.ID = UniqueID;
                        lnk.Text = dr["MenuName"].ToString();
                        if (ConfigurationManager.AppSettings["ReleaseMode"] == "TRUE")
                        {
                            lnk.NavigateUrl = HttpContext.Current.Request.ApplicationPath + "/" + Session["ModulePrefix"].ToString() + "/" + dr["URL"].ToString();
                        }
                        else
                        {
                            lnk.NavigateUrl = "../" + Session["ModulePrefix"].ToString() + "/" + dr["URL"].ToString();
                        }
                    
                        //lnk.NavigateUrl ="../"+Session["ModulePrefix"].ToString()+"/"+ dr["URL"].ToString();
                        //lnk.NavigateUrl = dr["URL"].ToString();
                        if (parent == menuid)
                            lnk.CssClass = "selected";
                        pnltopmenu.Controls.Add(lnk);
                    }
                if (ds.Tables[1].Rows.Count > 0)
                {
                    pnltopmenu.Controls.Add(new LiteralControl("</BR>"));
                }
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    int menuid = 0;
                    menuid = Convert.ToInt32(dr["MenuId"].ToString());
                    if (pnltopmenu.Controls.Count > 0 + (ds.Tables[1].Rows.Count * 2))
                    {
                        Label lbl = new Label();
                        lbl.Text = " | ";
                        lbl.ID = UniqueID;
                        pnltopmenu.Controls.Add(lbl);
                    }
                    HyperLink lnk = new HyperLink();
                    lnk.ID = UniqueID;
                    lnk.Text = dr["MenuName"].ToString();
                    if(ConfigurationManager.AppSettings["ReleaseMode"] == "TRUE")
                    {
                        lnk.NavigateUrl =HttpContext.Current.Request.ApplicationPath+"/" + Session["ModulePrefix"].ToString() + "/" + dr["URL"].ToString();
                    }
                    else
                    {
                        lnk.NavigateUrl = "../" + Session["ModulePrefix"].ToString() + "/" + dr["URL"].ToString();
                    }
                    

                    if (SelectedMenu == menuid)
                        lnk.CssClass = "selected";
                    pnltopmenu.Controls.Add(lnk);
                }
            }

        }
    }


}
