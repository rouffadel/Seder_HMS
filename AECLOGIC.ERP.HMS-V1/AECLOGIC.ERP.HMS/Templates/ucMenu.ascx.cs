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
        
        public string ModulePrefix
        {

            get { return Session["ModulePrefix"].ToString(); }
            set { Session["ModulePrefix"] = value; }
        }

        public int ModuleID
        {

            get { return Convert.ToInt16(Session["ModuleID"]); }
            set { Session["ModuleID"] = value; }
        }


        public string GetPageName(string URL)
        {
            string url = URL;

            char[] patt = { '/' };
            char[] p = { '.' };
            string[] arr = url.Split(patt);
            url = arr[arr.Length - 1];


            if (url.IndexOf('.') >-1 )
            {
                url = url.Remove(url.IndexOf('.'));
            }

            return url;
        }
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
           // SearchButton.Click += SearchButton_Click;    
            SearchButton.Click += SearchButton_Click;
        }

        void SearchButton_Click(object sender, EventArgs e)
        {
            try
            {
                int iMenuID = 0;

                Common DALC = new Common();

                try
                {
                    DataSource = DALC.GetUcMenu(ModuleID, int.Parse(Session["RoleId"].ToString()), "");
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

        void SearchButton_Click(object sender, ImageClickEventArgs e)
        {
            
        }

       

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            if (ModuleID == 9) // display only for OMS module
            {

             //   prjDiv.Visible = true;
            }
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
                
                Common DALC = new Common();

                DataSource = DALC.GetUcMenu(ModuleID, int.Parse(Session["RoleId"].ToString()), txtsearch.Text);
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
            //foreach (DataRow dr in drMainMenus)
            //{
            //    AddMainDivNew(navbar, dr["MenuName"].ToString(), dr["MenuID"].ToString(), dr["Help"].ToString());
            //}




            foreach (DataRow dr in drMainMenus)
            {

                AddMainDiv(navbar, dr["MenuName"].ToString(), dr["MenuID"].ToString(), dr["Help"].ToString());

            }
            //  txtsearch.Text = "";
        }

        protected void AddMainDivNew(System.Web.UI.HtmlControls.HtmlGenericControl navbar, String MenuText, String MenuId, String Help)
        {
            System.Web.UI.HtmlControls.HtmlGenericControl mainDiv = new System.Web.UI.HtmlControls.HtmlGenericControl("li");
            mainDiv.Attributes.Add("class", "treeview");
            navbar.Controls.Add(mainDiv);
            AddMainDivItemsNew(mainDiv, MenuText, MenuId, Help);
        }

        protected void AddMainDivItemsNew(System.Web.UI.HtmlControls.HtmlGenericControl mainDiv, String MenuText, String MenuId, String Help)
        {
            //add anchor tag
            System.Web.UI.HtmlControls.HtmlGenericControl ATag = new System.Web.UI.HtmlControls.HtmlGenericControl("A");
            ATag.Attributes.Add("href", "#");
            if (Help.Trim() != String.Empty)
            {
                ATag.Attributes.Add("onmouseover", "if(t1)t1.Show(event,'" + Help + "')");
                ATag.Attributes.Add("onmouseout", "if(t1)t1.Hide(event)");
            }
          //  ATag.InnerText = MenuText;
            ATag.InnerHtml = MenuText+ "<i class='fa fa-angle-left pull-right'></i>";
            //add Anchor Tag
            mainDiv.Controls.Add(ATag);

            //add treeView Menu ul
            System.Web.UI.HtmlControls.HtmlGenericControl dropMenuul = new System.Web.UI.HtmlControls.HtmlGenericControl("ul");
            dropMenuul.Attributes.Add("class", "treeview-menu");
            AddDropMenuli(dropMenuul, MenuId);
            mainDiv.Controls.Add(dropMenuul);

        }

        private void AddDropMenuli(System.Web.UI.HtmlControls.HtmlGenericControl menuUl , string strMenuId)
        {
            DataRow[] drSubMenus = DataSource.Tables[0].Select("Under='" + strMenuId + "'", "PreOrder Asc");
            foreach (DataRow dr in drSubMenus)
            {
                int mid = 0;
                mid = Convert.ToInt32(dr["MenuID"].ToString());
                int Menu = Convert.ToInt32(strMenuId);
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
                    menuUl.Attributes.Add("class", "treeview-menu menu-open");
                }
                string NavigateUrl = dr["URL"].ToString();
                string SubItem = dr["MenuName"].ToString();
                string Help = dr["Help"].ToString();
                System.Web.UI.HtmlControls.HtmlGenericControl ATag = new System.Web.UI.HtmlControls.HtmlGenericControl("A");
                ATag.Attributes.Add("href", "#");
                System.Web.UI.HtmlControls.HtmlGenericControl submenu = new System.Web.UI.HtmlControls.HtmlGenericControl("li");
                ATag.ID = SubItem + (SubItemCnt++).ToString();
               
                //ATag.Attributes.Add("href", "../" + ModulePrefix + "/" + NavigateUrl);
                if (ConfigurationManager.AppSettings["ReleaseMode"] == "TRUE")
                {
                    ATag.Attributes.Add("href", Request.ApplicationPath + "/" + ModulePrefix + "/" + NavigateUrl);
                }
                else
                {
                    ATag.Attributes.Add("href", "../" + ModulePrefix + "/" + NavigateUrl);
                }
                //  ATag.Attributes.Add("href", NavigateUrl);
                if (Help.Trim() != String.Empty)
                {
                    ATag.Attributes.Add("onmouseover", "if(t1)t1.Show(event,'" + Help + "')");
                    ATag.Attributes.Add("onmouseout", "if(t1)t1.Hide(event)");
                }
                ATag.InnerText = SubItem;
                if (flag)
                {
                    if (mname.ToString() == SubItem.ToString())
                    {

                        ATag.Attributes.Add("class", "selected");
                        flag = false;
                    }
                }
                if (flag2)
                {
                    if (n != null)
                        if (n.ToString() == SubItem.ToString())
                        {

                            ATag.Attributes.Add("class", "selected");
                            flag2 = false;
                        }
                }
                submenu.Controls.Add(ATag);
                menuUl.Controls.Add(submenu);
            }
        }



        protected void lnkChangeProject_Click(object sender, EventArgs e)
        {
            if (ConfigurationManager.AppSettings["ReleaseMode"] == "TRUE")
            { 
                            Response.Redirect(HttpContext.Current.Request.ApplicationPath +"/OMS/DashBoard.aspx?mode=select");

            }
            else
            {

                Response.Redirect("../" + ModulePrefix + "/DashBoard.aspx?mode=select");
            }
           // string pagename = GetPageName(Request.Url.PathAndQuery);

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

           
            //System.Web.UI.HtmlControls.HtmlGenericControl iLeftIcon = new System.Web.UI.HtmlControls.HtmlGenericControl("i");
            //iLeftIcon.Attributes.Add("class", "fa fa-angle-left pull-right");
            //ATag.InnerHtml ="<i class='fa fa-angle-left pull-right'></i>";
           // ATag.Controls.Add(iLeftIcon);

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
            int i=0;
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
                AddsubItem(subMenu, dr["MenuName"].ToString(), dr["URL"].ToString(), dr["Help"].ToString(), Convert.ToInt32(drSubMenus[i].ItemArray[0]));
                i++;
            }
        }

        protected void AddsubItem(System.Web.UI.HtmlControls.HtmlGenericControl subMenu, String SubItem, String NavigateUrl, string Help,int submenu)
        {
            DataSet ds = Common.PendingCount(submenu);
            
            System.Web.UI.HtmlControls.HtmlGenericControl subItem = new System.Web.UI.HtmlControls.HtmlGenericControl("div");
            subItem.Attributes.Add("class", "subItem");
            subMenu.Controls.Add(subItem);
            System.Web.UI.HtmlControls.HtmlGenericControl ATag = new System.Web.UI.HtmlControls.HtmlGenericControl("A");
            ATag.ID = SubItem + (SubItemCnt++).ToString();
            //ATag.Attributes.Add("href", "../" + ModulePrefix + "/" + NavigateUrl);
            if (ConfigurationManager.AppSettings["ReleaseMode"] == "TRUE")
            {
                ATag.Attributes.Add("href", Request.ApplicationPath+"/" + ModulePrefix + "/" + NavigateUrl);
            }
            else
            {
                ATag.Attributes.Add("href", "../" + ModulePrefix + "/" + NavigateUrl);
            }
          //  ATag.Attributes.Add("href", NavigateUrl);
            if (Help.Trim() != String.Empty)
            {
                ATag.Attributes.Add("onmouseover", "if(t1)t1.Show(event,'" + Help + "')");
                ATag.Attributes.Add("onmouseout", "if(t1)t1.Hide(event)");
            }
            ATag.InnerText = SubItem;

            //HtmlGenericControl devCount = new HtmlGenericControl("div");
            //devCount.Attributes.Add("style", "text-align:right; padding-right:0px;");
            
            if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0)
            {
                HtmlGenericControl spanCount = new HtmlGenericControl("span");
                spanCount.Attributes.Add("class", " pull-right label label-danger");
                //spanCount.Attributes["align"] = "Right";
                spanCount.InnerText = ds.Tables[0].Rows[0][0].ToString();
                //devCount.Controls.Add(spanCount);
                ATag.Controls.Add(spanCount);
            }
            if (ds != null && ds.Tables.Count != 0 && ds.Tables.Contains("1"))
            {
                if (ds.Tables[1].Rows[0][0].ToString() != "0")
                {
                    HtmlGenericControl spanCount1 = new HtmlGenericControl("span");
                    spanCount1.Attributes.Add("class", "pull-right label label-warning");
                    //spanCount1.Attributes["align"] = "Right";
                    spanCount1.InnerText = ds.Tables[1].Rows[0][0].ToString();
                   // devCount.Controls.Add(spanCount1);
                    ATag.Controls.Add(spanCount1);
                }
            }
            if (ds != null && ds.Tables.Count != 0 && ds.Tables.Contains("2"))
            {
                if (ds.Tables[2].Rows[0][0].ToString() != "0")
                {
                    HtmlGenericControl spanCount2 = new HtmlGenericControl("span");
                    spanCount2.Attributes.Add("class", "pull-right label label-success");
                    //spanCount2.Attributes["align"] = "Right";
                    spanCount2.InnerText = ds.Tables[2].Rows[0][0].ToString();
                   // devCount.Controls.Add(spanCount2);
                    ATag.Controls.Add(spanCount2);
                }
            }
           // ATag.Controls.Add(devCount);


            if(GetPageName(Request.RawUrl)== GetPageName(NavigateUrl))
            {
                if (Request.RawUrl.Split('/')[Request.RawUrl.Split('/').Length-1]== NavigateUrl)
                 ATag.Attributes.Add("class", "selected");
            }
            //if (flag)
            //    if (mname.ToString() == SubItem.ToString())
            //    {

            //        ATag.Attributes.Add("class", "selected");
            //        flag = false;
            //    }
            //if (flag2)
            //    if (n != null)
            //        if (n.ToString() == SubItem.ToString())
            //        {

            //            ATag.Attributes.Add("class", "selected");
            //            flag2 = false;
            //        }
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
        protected void btnUndoSearch_Click(object sender, EventArgs e)
        {
            txtsearch.Text = "";

        }


       
    }
}