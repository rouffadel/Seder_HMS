using System;

using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
//using DataAccessLayer;
using Aeclogic.Common.DAL;
namespace AECLOGIC.ERP.COMMON
{
    public partial class Help1 : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CurPageVal.Value = "0";
                WebFormMaster.BindFavorites(Convert.ToInt32(Session["UserId"].ToString()));
                Session["EncUserID"] = CrypHelper.Encode(Session["UserId"].ToString());
                Application["EncModuleID"] = CrypHelper.Encode(Application["ModuleID"].ToString());
                Ajax.Utility.RegisterTypeForAjax(typeof(Help1));
                BindGroups();
                BindFavGroups(null);

            }
            imgMarkFav.Alt = "Mark Favourite";
            imgMarkFav.Src = "./IMAGES/MarkFavIcon.png";
            imgMarkFav.Style.Add("Title", "Mark Favourite");
            if (WebFormMaster._pageArraylist != null && Session["CurrentPage"] != null)
            {
                foreach (DictionaryEntry de in WebFormMaster._pageArraylist)
                {
                    if (de.Key.ToString().Split('/')[2].ToString() == Session["CurrentPage"].ToString())
                    {
                        imgMarkFav.Src = "./IMAGES/UnMarkFavIcon.ico";
                        imgMarkFav.Alt = "Un Mark Favourite";
                        imgMarkFav.Style.Add("Title", "Un Mark Favourite");
                        CurPageVal.Value = de.Value.ToString();
                        return;
                    }
                    else
                    {
                        imgMarkFav.Alt = "Mark Favourite";
                        imgMarkFav.Src = "./IMAGES/MarkFavIcon.png";
                        imgMarkFav.Style.Add("Title", "Mark Favourite");

                    }
                }
            }
        }
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
            DataSet ds = new DataSet();
            ds = Common.BindFavGroups(Convert.ToInt32(Session["UserID"]));
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
                    using (DataSet ds = Common.GEN_GetMenuSelfHelp(Convert.ToInt32(value)))
                    {
                        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                        {
                            if (ds.Tables[0].Rows[0][0].ToString().Trim() == "")
                                tdselfContent.InnerHtml = "<b>Oops!</b><br />No help content available.<br /><br />";
                            else
                                tdselfContent.InnerHtml = ds.Tables[0].Rows[0][0].ToString();
                        }
                    }
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
            DataSet ds = new DataSet();
            ds = Common.BindFavGroups(Convert.ToInt32(CrypHelper.Decode(UserID)));
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