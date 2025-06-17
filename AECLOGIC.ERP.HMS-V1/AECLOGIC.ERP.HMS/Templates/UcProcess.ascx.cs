using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
//using DataAccessLayer;
using Aeclogic.Common.DAL;
using System.Data.SqlClient;
using System.Text;

namespace AECLOGIC.ERP.COMMON
{
    public partial class UcProcess : System.Web.UI.UserControl
    {
        private int _ModuleID = 0;
        public int ModuleID
        {
            get { return _ModuleID; }
            set { _ModuleID = value; }
        }
        public string Result
        {
            set
            {
                if (value.Trim() != String.Empty)
                {
                    divPath_wfProcesses.InnerHtml = value;
                    divPath_wfProcesses.Visible = true;
                }
            }
        }

        public static Hashtable _pageArraylist;
        public void BindFavorites(int EmpID)
        {
            _pageArraylist = new Hashtable();
            if (Common.HomePageUrls(EmpID).Tables.Count > 1 && Common.HomePageUrls(EmpID).Tables[2].Rows.Count > 0)
            {
                foreach (DataRow dr in Common.HomePageUrls(EmpID).Tables[2].Rows)
                {
                    if (!_pageArraylist.Contains(dr["URL"].ToString()))
                        _pageArraylist.Add(dr["URL"].ToString(), dr["FavID"].ToString());
                }
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["EncUserID"] = CrypHelper.Encode(Session["UserId"].ToString());
                Application["EncModuleID"] = CrypHelper.Encode(ModuleID.ToString());               
            }           
        }
        #region HelpMethods
        public int AutoPopDelay { get; set; }        
        #endregion   
        public static object DataSource { get; set; }
    }
}