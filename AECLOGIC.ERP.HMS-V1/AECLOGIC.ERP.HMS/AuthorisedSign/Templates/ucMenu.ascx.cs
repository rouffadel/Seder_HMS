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
using Aeclogic.Common.DAL;

/* This Web User Control created by Suresh Gidugu on 22 july  2010.
 * 
 * Prerequisites:
 * Developer must place the Includes Folder under the root and its Contains CSS,JS,Images Folders.
 * CSS      : base.css and sddm.css must be called at your main page.
 * Images   : arrow-down-title.jpg, arrow-up-title.jpg
 * JS       : _lightbox.js, _prototype.js, _scriptaculous.js, collapsemenu.js, effects.js, litebox-1.0.js, moo.fx.js, prototype.lite.js, Tooltip.js, xc.js, xpmenuv21.js, onload.js
 * Note: if onload.js 
 * */
namespace AECLOGIC.ERP.COMMON
{
    public partial class ucMenu1 : System.Web.UI.UserControl
    {
        int SubItemCnt = 0;
        DataSet _DataSource;
        int _RoleId;
        int _MenuId;
        bool flag;
        string _mname;
        string _mname1;
        bool flag2 = false; string mname;
        int _MId;
        int m = 0;
        string n;


        public int MId
        {
            get { return _MId; }
            set { _MId = value; }
        }
        public string Mname
        {
            get { return _mname; }
            set { _mname = value; }
        }
        public string Mname1
        {
            get { return _mname1; }
            set { _mname1 = value; }
        }
        public int MenuId
        {
            get { return _MenuId; }
            set { _MenuId = value; }
        }
        public int RoleId
        {
            get { return _RoleId; }
            set { _RoleId = value; }
        }

        public DataSet DataSource
        {
            get { return _DataSource; }
            set { _DataSource = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        public override void DataBind()
        {
            navbar.Controls.Clear();
            DataRow[] drMainMenus = null;
            if (txtsearch.Text != "")
            {
                int ModuleId = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["ModuleId"]);
                Common DALC = new Common();

                DataSource = DALC.GetUcMenu(ModuleId, int.Parse(Session["RoleId"].ToString()), txtsearch.Text);
                drMainMenus = DataSource.Tables[0].Select("Under is NULL", "PreOrder Asc");

            }
            else
            {
                drMainMenus = DataSource.Tables[0].Select("Under is NULL", "PreOrder Asc");
            }
            //Mname = (string) DataSource.Tables[0].Select("Menuname='" + MenuId + "'").ToString();
            foreach (DataRow drm in DataSource.Tables[0].Rows)
            {
                if (drm["MenuId"].ToString() == MenuId.ToString())
                {
                    // flag2 = true;
                    m = Convert.ToInt32(drm["MenuId"].ToString());
                    // Mname1 = drm["MenuName"].ToString();
                }
            }
            foreach (DataRow dr in drMainMenus)
            {

                AddMainDiv(navbar, dr["MenuName"].ToString(), dr["MenuID"].ToString(), dr["Help"].ToString());

            }
            //  txtsearch.Text = "";
        }

        protected void AddMainDiv(System.Web.UI.HtmlControls.HtmlGenericControl navbar, String MenuText, String MenuId, String Help)
        {
            System.Web.UI.HtmlControls.HtmlGenericControl mainDiv = new System.Web.UI.HtmlControls.HtmlGenericControl("div");
            mainDiv.Attributes.Add("class", "mainDiv");
            navbar.Controls.Add(mainDiv);
            AddMainDivItems(mainDiv, MenuText, MenuId, Help);
        }

        protected void AddMainDivItems(System.Web.UI.HtmlControls.HtmlGenericControl mainDiv, String MenuText, String MenuId, String Help)
        {
            //add anchor tag
            System.Web.UI.HtmlControls.HtmlGenericControl ATag = new System.Web.UI.HtmlControls.HtmlGenericControl("A");
            ATag.Attributes.Add("href", "#");
            if (Help.Trim() != String.Empty)
            {
                ATag.Attributes.Add("onmouseover", "if(t1)t1.Show(event,'" + Help + "')");
                ATag.Attributes.Add("onmouseout", "if(t1)t1.Hide(event)");
            }
            ATag.InnerText = MenuText;

            //add topItem
            System.Web.UI.HtmlControls.HtmlGenericControl topItem = new System.Web.UI.HtmlControls.HtmlGenericControl("div");
            topItem.Attributes.Add("class", "topItem");
            topItem.Controls.Add(ATag);
            mainDiv.Controls.Add(topItem);

            //add dropMenu
            System.Web.UI.HtmlControls.HtmlGenericControl dropMenu = new System.Web.UI.HtmlControls.HtmlGenericControl("div");
            dropMenu.Attributes.Add("class", "dropMenu");
            AddDropMenu(dropMenu, MenuId);
            mainDiv.Controls.Add(dropMenu);

        }

        protected void AddDropMenu(System.Web.UI.HtmlControls.HtmlGenericControl dropMenu, String MenuId)
        {
            System.Web.UI.HtmlControls.HtmlGenericControl subMenu = new System.Web.UI.HtmlControls.HtmlGenericControl("div");
            subMenu.Attributes.Add("class", "subMenu");
            subMenu.Style.Add("display", "inline");
            dropMenu.Controls.Add(subMenu);

            //flag2 = true;
            DataRow[] drSubMenus = DataSource.Tables[0].Select("Under='" + MenuId + "'", "PreOrder Asc");
            foreach (DataRow dr in drSubMenus)
            {

                int mid = 0;
                mid = Convert.ToInt32(dr["MenuID"].ToString());
                int Menu = Convert.ToInt32(MenuId);
                if (m == mid)
                {
                    flag2 = true;
                    n = dr["MenuName"].ToString();
                }

                if (mid == MId)
                {
                    ViewState["mid"] = mid;
                    flag = true;
                    mname = dr["MenuName"].ToString();
                    subMenu.Attributes.Add("class", "selected");
                }
                AddsubItem(subMenu, dr["MenuName"].ToString(), dr["URL"].ToString(), dr["Help"].ToString());

            }
        }

        protected void AddsubItem(System.Web.UI.HtmlControls.HtmlGenericControl subMenu, String SubItem, String NavigateUrl, string Help)
        {
            System.Web.UI.HtmlControls.HtmlGenericControl subItem = new System.Web.UI.HtmlControls.HtmlGenericControl("div");
            subItem.Attributes.Add("class", "subItem");
            subMenu.Controls.Add(subItem);
            System.Web.UI.HtmlControls.HtmlGenericControl ATag = new System.Web.UI.HtmlControls.HtmlGenericControl("A");
            ATag.ID = SubItem + (SubItemCnt++).ToString();
            ATag.Attributes.Add("href","../HMS/"+ NavigateUrl);
            if (Help.Trim() != String.Empty)
            {
                ATag.Attributes.Add("onmouseover", "if(t1)t1.Show(event,'" + Help + "')");
                ATag.Attributes.Add("onmouseout", "if(t1)t1.Hide(event)");
            }
            ATag.InnerText = SubItem;
            if (flag)
                if (mname.ToString() == SubItem.ToString())
                {

                    ATag.Attributes.Add("class", "selected");
                    flag = false;
                }
            if (flag2)
                if (n != null)
                    if (n.ToString() == SubItem.ToString())
                    {

                        ATag.Attributes.Add("class", "selected");
                        flag2 = false;
                    }
            //if (Mname1 != null)
            //{
            //    if (Mname1.ToString() == SubItem.ToString())
            //        ATag.Attributes.Add("class", "selected");
            //}
            subItem.Controls.Add(ATag);
            //ATag.InnerText = SubItem;
            //if (Mname != null)
            //{
            //    if (Mname.ToString() == SubItem.ToString())
            //        ATag.Attributes.Add("class", "selected");
            //}
            //if (Mname1 != null)
            //{
            //    if (Mname1.ToString() == SubItem.ToString())
            //        ATag.Attributes.Add("class", "selected");
            //}
            //subItem.Controls.Add(ATag);



        }

        protected void txtsearch_Init(object sender, EventArgs e)
        {

        }
        protected void btnUndoSearch_Click(object sender, ImageClickEventArgs e)
        {
            txtsearch.Text = "";

        }


        protected void SearchButton_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                int iMenuID = 0;
                int ModuleId = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["ModuleId"]);
                Common DALC = new Common();

                try
                {
                    DataSource = DALC.GetUcMenu(ModuleId, int.Parse(Session["RoleId"].ToString()), "");
                    iMenuID = Convert.ToInt32(txtsearch.Text);
                    DataRow[] drMainMenus = DataSource.Tables[0].Select("MenuId='" + iMenuID.ToString() + "'", "PreOrder Asc");
                    if (drMainMenus.Length > 0 && drMainMenus[0]["URL"].ToString().Trim() != "#" && drMainMenus[0]["URL"].ToString().Trim() != "")
                    {

                        Response.Redirect(drMainMenus[0]["URL"].ToString());
                    }
                    else
                    {
                        txtsearch.Text = "";
                        DataBind();
                    }

                }
                catch
                {
                    DataBind();
                    txtsearch.Text = "";
                }

            }
            catch
            {
            }
        }
    }
}