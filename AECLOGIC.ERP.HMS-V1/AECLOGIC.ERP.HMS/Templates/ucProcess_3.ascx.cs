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
    public partial class ucProcess_3 : System.Web.UI.UserControl
    {

        //  public UcProcess ucSHProces;
        private int _ModuleID = 0;
        public int ModuleID
        {
            //get { return _ModuleID; }
            //set { _ModuleID = value; }
            get { return Convert.ToInt16(Session["ModuleID"]); }
            set { Session["ModuleID"] = value; }
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
                //int Menu_ID = GetParentMenuId();
                //    hdfMenuID.Value = Menu_ID.ToString();
                CurPageVal.Value = "0";
                BindFavorites(Convert.ToInt32(Session["UserId"].ToString()));
                Session["EncUserID"] = CrypHelper.Encode(Session["UserId"].ToString());
                Application["EncModuleID"] = CrypHelper.Encode(ModuleID.ToString());
                hdnEncModuleID.Value = CrypHelper.Encode(ModuleID.ToString());
                Ajax.Utility.RegisterTypeForAjax(typeof(Help1));
                BindGroups();
                BindFavGroups(null);
                //Session["CurrentPage"] = Request.Url.Segments[Request.Url.Segments.Length - 1] + Request.Url.Query;
                //string URL = Session["CurrentPage"].ToString();
                //DataSet dsHlep = PageHelp(URL, ModuleID);
                //using (dsHlep = PageHelp(Session["CurrentPage"].ToString(), ModuleID))
                //{
                //    if (dsHlep != null && dsHlep.Tables.Count > 0 && dsHlep.Tables[0].Rows.Count > 0)
                //    {
                //        if (dsHlep.Tables.Count > 0)
                //        {
                //            if (dsHlep.Tables[0].Rows.Count > 0)
                //            {
                //                DataRow dr = dsHlep.Tables[0].Rows[0];
                //                BindProc(Convert.ToInt32(dr["MenuId"].ToString()));
                //            }
                //        }
                //    }
                //}
                imgMarkFav.Alt = "Mark Favourite";
                imgMarkFav.Src = "../IMAGES/MarkFavIcon.png";
                imgMarkFav.Style.Add("Title", "Mark Favourite");
                if (WebFormMaster._pageArraylist != null && Session["CurrentPage"] != null)
                {
                    foreach (DictionaryEntry de in WebFormMaster._pageArraylist)
                    {
                        if (de.Key.ToString().Split('/')[2].ToString() == Session["CurrentPage"].ToString())
                        {
                            imgMarkFav.Src = "../IMAGES/UnMarkFavIcon.ico";
                            imgMarkFav.Alt = "Un Mark Favourite";
                            imgMarkFav.Style.Add("Title", "Un Mark Favourite");
                            CurPageVal.Value = de.Value.ToString();
                            return;
                        }
                        else
                        {
                            imgMarkFav.Alt = "Mark Favourite";
                            imgMarkFav.Src = "../IMAGES/MarkFavIcon.png";
                            imgMarkFav.Style.Add("Title", "Mark Favourite");
                        }
                    }
                }
            }
        }
        private DataSet PageHelp(string URL, int ModuleId)
        {
            try
            {
                //DataSet ds = new DataSet();
                SqlParameter[] objParam = new SqlParameter[2];
                objParam[0] = new SqlParameter("@URL", URL);
                objParam[1] = new SqlParameter("@ModuleId", ModuleId);
                return SQLDBUtil.ExecuteDataset("CP_Get_PageHelp", objParam);
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #region Self help
        public void BindProc(int menuid)
        {
            //lblQLID.Text = MenuID; if (value == "") { lblQLID.Visible = false; qlID.Visible = false; } else { lblQLID.Visible = true; qlID.Visible = true; }
            //if (value.Trim() != String.Empty)
            //{
            Session["curMenuid"] = menuid;
            using (DataSet ds = GEN_GetProcessList(Convert.ToInt32(menuid)))
            {
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    lblError.Text = "";
                    gvEV.Visible = true;
                    //divPath.Visible = false;

                    gvEV.DataSource = ds;
                    gvEV.DataBind();
                }
                else
                {
                    lblError.Text = "<b>Oops!</b><br />No Processes available.<br /><br />";
                    gvEV.Visible = false;
                    // divPath.Visible = false;
                    gvEV.DataSource = null;
                    gvEV.DataBind();
                }
            }
            // }
        }
        public static DataSet GEN_GetProcessList(int MenuID)
        {
            try
            {
                SqlParameter[] objParam = new SqlParameter[2];
                objParam[0] = new SqlParameter("@fCase", 4);
                objParam[1] = new SqlParameter("@fkID", MenuID);
                return SQLDBUtil.ExecuteDataset("wfProcessPages_ddl", objParam);
            }
            catch
            {
                return null;
            }
        }
        protected void gvEV_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                

                int ID = Convert.ToInt32(e.CommandArgument);
                //sAlertMsg.MsgBox(Page, "WC", AlertMsg.MessageType.Success);
                
                if (e.CommandName == "Edt")
                {
                   

                    SqlParameter[] p = new SqlParameter[2];
                    p[0] = new SqlParameter("@fCase", 3);
                    p[1] = new SqlParameter("@fkID", ID);
                    DataSet ds = SqlHelper.ExecuteDataset("wfProcessPages_ddl", p);
                   

                    if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                    {
                       

                        StringBuilder sb = new StringBuilder();
                        //HtmlGenericControl crumbsDiv = (HtmlGenericControl)Page.Master.FindControl("ContentPlaceHolder1").FindControl("crumbs");
                        //if (crumbsDiv != null)
                        //{                            
                        //    crumbsDiv.Style.Remove("display");
                        //    crumbsDiv.Style.Add("display", "block");
                        //}
                        sb.AppendLine(@" <span id='closeProc' class='close__crumb open'>  </span><div id=""crumbs"">");
                        sb.AppendLine("<ul>");
                        int Menu_ID = 0;
                        try {
                             Menu_ID = GetParentMenuId();
                            GridViewRow rw = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                            Menu_ID = Convert.ToInt32(((Label)rw.FindControl("lblMenuID")).Text);
                        }
                        catch { }
                        // int Menu_ID = GetParentMenuId();
                        //int Menu_ID = Convert.ToInt32(hdfMenuID.Value);

                        int valu = 1;
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                         

                            string handler = ds.Tables[0].Rows[i][2].ToString();
                            string menuText = ds.Tables[0].Rows[i][0].ToString();
                            string title = ds.Tables[0].Rows[i][1].ToString();
                            string line = "";

                            // string temmid = Session["curMenuid"].ToString();
                            // string temmid_2 = ds.Tables[0].Rows[i][3].ToString();
                            //if (Session["curMenuid"] != null && Session["curMenuid"].ToString().Length > 0 && Convert.ToInt32(Session["curMenuid"].ToString()) > 0
                            //    && (Session["curMenuid"].ToString()) == ds.Tables[0].Rows[i][3].ToString())
                            if (Menu_ID.ToString().Length > 0 && Menu_ID > 0
                              && (Menu_ID.ToString()) == ds.Tables[0].Rows[i][3].ToString())
                            {
                                line = String.Format(@"<li><a href=""{0}"" class=""wfactive"" title=""{2}"" target=""_blank""   >{1}</a>", handler, menuText, title);
                            }
                            else
                            {
                                line = String.Format(@"<li ><a href=""{0}""  title=""{2}"" target=""_blank""  onclick=""checklist(this);""  >{1}</a>", handler, menuText, title);
                            }
                            sb.Append(line);
                            sb.Append("</li>");
                            valu = valu + 2;
                        }
                        sb.Append("</ul>");

                     

                        //sb.Append(@"<button value=""Close"" id=""btnCrumbs"" onclick=""divReset()"">close</button>");
                        sb.Append("</div>");
                        ViewState["ProcessData"] = sb.ToString();
                        HtmlGenericControl InnerProcessDiv = (HtmlGenericControl)Page.Master.FindControl("divPath");
                       

                        if (InnerProcessDiv != null)
                        {
                       

                            InnerProcessDiv.InnerHtml = ViewState["ProcessData"].ToString();
                            InnerProcessDiv.Style.Remove("display");
                            InnerProcessDiv.Style.Add("display", "block");
                            

                        }
                        //    ucSHProces = (UcProcess)Page.Master.FindControl("UcProcess");
                        //   ucSHProces.Result = sb.ToString();

                    }
                    else
                    {
                        // divPath.InnerHtml = "No Process";
                    }
                }
            }
            catch (Exception CatDel)
            {
                
                //clsErrorLog.HMSEventLog(ex, "ReqfromOMS", "gvEV_RowCommand", "003"); 
            }
        }

        public int GetParentMenuId()
        {
            int MenuId = 0;
            try
            {
                string URL = Request.Url.Segments[Request.Url.Segments.Length - 1] + Request.Url.Query;
                if (URL == null)
                    URL = Session["CurrentPage"].ToString(); 
                else if (URL.ToString().Trim() == "")
                    URL = Session["CurrentPage"].ToString();
                //AlertMsg.MsgBox(Page, URL);
                int RoleId = Convert.ToInt32(Session["RoleId"].ToString());
                //int ModuleId = ModuleID;
                //DataSet ds = new DataSet();
                //ProcDept objProc = new ProcDept();
                //ds = ProcDept.GetAllowed(RoleId, ModuleId, URL);
                DataSet ds = Common.CP_GetMenuIDbyURL(RoleId, URL);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    MenuId = Convert.ToInt32(ds.Tables[0].Rows[0]["MenuId"]);

                }
                if(MenuId==0)
               AlertMsg.MsgBox(Page, MenuId.ToString() + " " + URL);
            }
            catch { }
            return MenuId;
        }
        #endregion
        public void Show()
        {
            this.tbHelp.Visible = true;
        }
        public void Hide()
        {
            this.tbHelp.Visible = false;
        }
        #region HelpMethods
        public string Who
        {
            set
            {
                if (value.Trim() != String.Empty)
                {
                    hdntsWho.Value = Server.HtmlEncode(value.Trim());
                    lnkWho.Attributes.Add("onmouseover", "if(t1)t1.Show(event,$get('hdntsWho').value)");
                    lnkWho.Attributes.Add("onmouseout", "if(t1)t1.Hide(event)");
                }
            }
        }
        public string How
        {
            set
            {
                if (value.Trim() != String.Empty)
                {
                    hdntsHow.Value = Server.HtmlEncode(value.Trim());
                    lnkHow.Attributes.Add("onmouseover", "if(t1)t1.Show(event,$get('hdntsHow').value)");
                    lnkHow.Attributes.Add("onmouseout", "if(t1)t1.Hide(event)");
                }
            }
        }
        public string What
        {
            set
            {
                if (value.Trim() != String.Empty)
                {
                    hdntsWhat.Value = Server.HtmlEncode(value.Trim());
                    lnkWhat.Attributes.Add("onmouseover", "if(t1)t1.Show(event,$get('hdntsWhat').value)");
                    lnkWhat.Attributes.Add("onmouseout", "if(t1)t1.Hide(event)");
                }
            }
        }
        public string When
        {
            set
            {
                if (value.Trim() != String.Empty)
                {
                    hdntsWhen.Value = Server.HtmlEncode(value.Trim());
                    lnkWhen.Attributes.Add("onmouseover", "if(t1)t1.Show(event,$get('hdntsWhen').value)");
                    lnkWhen.Attributes.Add("onmouseout", "if(t1)t1.Hide(event)");
                }
            }
        }
        public string Where
        {
            set
            {
                if (value.Trim() != String.Empty)
                {
                    hdntsWhere.Value = Server.HtmlEncode(value.Trim());
                    lnkWhere.Attributes.Add("onmouseover", "if(t1)t1.Show(event,$get('hdntsWhere').value)");
                    lnkWhere.Attributes.Add("onmouseout", "if(t1)t1.Hide(event)");
                }
            }
        }
        public string Why
        {
            set
            {
                if (value.Trim() != String.Empty)
                {
                    hdntsWhy.Value = Server.HtmlEncode(value.Trim());
                    lnkWhy.Attributes.Add("onmouseover", "if(t1)t1.Show(event,$get('hdntsWhy').value)");
                    lnkWhy.Attributes.Add("onmouseout", "if(t1)t1.Hide(event)");
                }
            }
        }
        public int AutoPopDelay { get; set; }
        public string TutorialURL
        {
            set
            {
                if (value.Trim() != String.Empty)
                {
                    lnkTutorials.Attributes.Add("href", value);
                }
            }
        }
        public void BindGroups()
        {
            //DataSet ds = new DataSet();
            DataSet ds = Common.BindFavGroups(Convert.ToInt32(Session["UserID"]));
            ddlGroup.DataSource = ds;
            ddlGroup.DataTextField = "Group";
            ddlGroup.DataValueField = "FavGroupID";
            ddlGroup.DataBind();
            ddlGroup.Items.Insert(0, "--Select--");
        }
        #endregion
        public string MenuID
        {
            set
            {
                lblQLID.Text = value; if (value == "") { lblQLID.Visible = false; qlID.Visible = false; } else { lblQLID.Visible = true; qlID.Visible = true; }
                if (value.Trim() != String.Empty)
                {
                    BindProc(Convert.ToInt32(value));
                }
            }
        }
        int Reval = 0;
        #region AjaxMethods
        [Ajax.AjaxMethod()]
        public string DelRecord(string PageID)
        {
            if (PageID != "[object Object]")
            {
                Common.DeleFav(Convert.ToInt32(PageID));
            }
            return "";
        }
        [Ajax.AjaxMethod()]
        public string GenInsertNewGrp(string EmpID, string GrpName)
        {
            int? GrpID = null;
            Common.GEN_InserFavGrp(ref GrpID, Convert.ToInt32(CrypHelper.Decode(EmpID)), GrpName, 1);
            return GrpID.ToString();
        }
        [Ajax.AjaxMethod()]
        public string SaveasFavPage(string EmpID, string CurPage, string ModuleID, string FavName, int GrpID)
        {
            Reval = Common.G_InsertFavLink(Convert.ToInt32(CrypHelper.Decode(EmpID)), CurPage, Convert.ToInt32(CrypHelper.Decode(ModuleID)), FavName, GrpID);
            return Reval.ToString();
        }
        [Ajax.AjaxMethod()]
        public string GEN_GetGroupNamebyEmpNFavGrpID(string EmpID, string FaVGrpID)
        {
            int? GrpID = null;
            DataSet ds = Common.GEN_InserFavGrp(ref GrpID, Convert.ToInt32(CrypHelper.Decode(EmpID)), FaVGrpID, 2);
            return ds.Tables[0].Rows[0]["Group"].ToString();
        }
        [Ajax.AjaxMethod()]
        public void UpdateGroupText(string GrpID, string UserID, string GrPName, int ID)
        {
            int? GroupID = Convert.ToInt32(GrpID);
            if (GrPName.Trim() != "")
            {
                using (Common.GEN_InserFavGrp(ref GroupID, Convert.ToInt32(CrypHelper.Decode(UserID)), GrPName, ID))
                {
                }
            }
        }
        [Ajax.AjaxMethod()]
        public void BindUpdatedGroups(string UserID, DropDownList ddl)
        {
            //DataSet ds = new DataSet();
            DataSet ds = Common.BindFavGroups(Convert.ToInt32(CrypHelper.Decode(UserID)));
            ddl.DataSource = ds;
            ddl.DataTextField = "Group";
            ddl.DataValueField = "FavGroupID";
            ddl.DataBind();
            ddl.Items.Insert(0, "--Select--");
        }
        [Ajax.AjaxMethod()]
        public void UpdateGroupID(string EmpID, string GrpID, string Direction)
        {
            try
            {
                Common.GEN_FAV_SetGroupOrder(Convert.ToInt32(CrypHelper.Decode(EmpID)), Convert.ToInt32(GrpID), Convert.ToInt32(Direction));
            }
            catch { }
        }
        #endregion
        protected void btnimgClose_Click(object sender, EventArgs e)
        {
            mpeHelpView.Hide();
        }
        protected void btnClose_Click(object sender, EventArgs e)
        {
            mpeHelpView.Hide();
        }
        void BindFavGroups(int? GrpID)
        {
            using (DataSet ds = Common.GEN_InserFavGrp(ref GrpID, Convert.ToInt32(Session["UserId"]), null, 2))
            {
                rpFAVGrps.DataSource = ds;
                rpFAVGrps.DataBind();
            }
        }
        protected void rpFAVGrps_ItemDataBound(object sender, System.Web.UI.WebControls.RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == System.Web.UI.WebControls.ListItemType.Item || e.Item.ItemType == System.Web.UI.WebControls.ListItemType.AlternatingItem)
            {
                Label lblGp = (Label)e.Item.FindControl("lblGp");
                HtmlTable trRow = (HtmlTable)e.Item.FindControl("rptRow");
                HtmlInputText txtGrpEditText = (HtmlInputText)e.Item.FindControl("txtGrpEditText");
                HtmlInputHidden hdnEdit = (HtmlInputHidden)e.Item.FindControl("hdnEdit");
                HtmlInputImage lnkEdit = (HtmlInputImage)e.Item.FindControl("lnkEdit");
                lnkEdit.Style.Add("Title", "Edit Group");
                lnkEdit.Attributes.Add("onclick", "javascript:return EditGroupName('" + lnkEdit.ClientID + "','" + txtGrpEditText.ClientID + "','" + hdnEdit.Value + "','" + lblGp.ClientID + "');");
                HtmlInputImage lnkDel = (HtmlInputImage)e.Item.FindControl("lnkDel");
                lnkDel.Attributes.Add("onclick", "javascript:return DeleteFavGroup('" + hdnEdit.Value + "','" + trRow.ClientID + "');");
                HtmlInputImage imgup = (HtmlInputImage)e.Item.FindControl("imgup");
                HtmlInputImage imgdown = (HtmlInputImage)e.Item.FindControl("imgdown");
                imgup.Attributes.Add("onclick", "javascript:return UpdateGroupOrder('" + trRow.ClientID + "','" + hdnEdit.Value + "','" + -1 + "')");//-1 for down direction
                imgdown.Attributes.Add("onclick", "javascript:return UpdateGroupOrder('" + trRow.ClientID + "','" + hdnEdit.Value + "','" + 1 + "')");//1 for Up direction
            }
        }
    }
}