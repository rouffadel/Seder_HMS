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
using Aeclogic.Common.DAL;
using AECLOGIC.ERP.COMMON;
using System.Collections.Generic;
namespace AECLOGIC.ERP.HMS
{
    public partial class Contacts : AECLOGIC.ERP.COMMON.WebFormMaster
    {
        AttendanceDAC objContacts = new AttendanceDAC();
        HRCommon objHrCommon = new HRCommon();
        bool viewall;
        int mid = 0;
        string menuname;
        string menuid;
        string Domain = System.Configuration.ConfigurationManager.AppSettings["DomainName"].ToString();
        protected override void OnInit(EventArgs e)
        {
            ModuleID = 1;
            base.OnInit(e);
            EmpListPaging.FirstClick += new Paging.PageFirst(EmpListPaging_FirstClick);
            EmpListPaging.PreviousClick += new Paging.PagePrevious(EmpListPaging_FirstClick);
            EmpListPaging.NextClick += new Paging.PageNext(EmpListPaging_FirstClick);
            EmpListPaging.LastClick += new Paging.PageLast(EmpListPaging_FirstClick);
            EmpListPaging.ChangeClick += new Paging.PageChange(EmpListPaging_FirstClick);
            EmpListPaging.ShowRowsClick += new Paging.ShowRowsChange(EmpListPaging_ShowRowsClick);
            EmpListPaging.CurrentPage = 1;
        }
        void EmpListPaging_ShowRowsClick(object sender, EventArgs e)
        {
            EmpListPaging.CurrentPage = 1;
            BindPager();
        }
        void EmpListPaging_FirstClick(object sender, EventArgs e)
        {
            if (hdnSearchChange.Value == "1")
                EmpListPaging.CurrentPage = 1;
            BindPager();
            hdnSearchChange.Value = "0";
        }
        void BindPager()
        {
            objHrCommon.PageSize = EmpListPaging.CurrentPage;
            objHrCommon.CurrentPage = EmpListPaging.ShowRows;
            BindContacts(objHrCommon);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            ddlTrade.Attributes.Add("onchange", "javascript:return SearchModified();");
            ddlContGroup.Attributes.Add("onchange", "javascript:return SearchModified();");
            if (!IsPostBack)
            {
                Page.Form.Attributes.Add("enctype", "multipart/form-data");
                GetParentMenuId();
                ViewState["ContactId"] = 0; ViewState["ext"] = string.Empty;
                if (Request.QueryString.Count > 0)
                {
                    mainview.ActiveViewIndex = 0;
                    BindCategories();
                    BindContactGroup();
                    BindContacts(objHrCommon);
                    imgemp.Visible = false;
                }
                else
                {
                    int ID = Convert.ToInt32(Request.QueryString["key"]);
                    mainview.ActiveViewIndex = 1;
                    if (ID == 1)
                    {
                    }
                    BindCategories();
                    BindContactGroup();
                    BindContacts(objHrCommon);
                }
            }
        }
        public void BindContactGroup()
        {
            DataSet ds = AttendanceDAC.HR_GetContactGroup();
            ddlContactGroup.DataSource = ds;
            ddlContactGroup.DataTextField = "GroupName";
            ddlContactGroup.DataValueField = "GID";
            ddlContactGroup.DataBind();
            ddlContactGroup.Items.Insert(0, new ListItem("--SELECT--", "0"));
            ddlContGroup.DataSource = ds;
            ddlContGroup.DataTextField = "GroupName";
            ddlContGroup.DataValueField = "GID";
            ddlContGroup.DataBind();
            ddlContGroup.Items.Insert(0, new ListItem("--SELECT--", "0"));
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
                gvContacts.Columns[4].Visible = Convert.ToBoolean(ds.Tables[0].Rows[0]["Editable"].ToString());
                gvContacts.Columns[5].Visible = Convert.ToBoolean(ds.Tables[0].Rows[0]["Editable"].ToString());
                btnSave.Visible = Convert.ToBoolean(ds.Tables[0].Rows[0]["Editable"].ToString());
            }
            return MenuId;
        }
        private void BindCategories()
        {
            DataSet ds = SqlHelper.ExecuteDataset("HR_getContactTypes");
            ddlCategory.DataValueField = ds.Tables[0].Columns[0].ToString();
            ddlCategory.DataTextField = ds.Tables[0].Columns[1].ToString();
            ddlCategory.DataSource = ds;
            //ddlResourcetype.DataSource = LookUp.GetMaterialRecords();
            ddlCategory.DataBind();
            ddlCategory.Items.Insert(0, new ListItem("--SELECT--", "0"));
            ddlTrade.DataValueField = ds.Tables[0].Columns[0].ToString();
            ddlTrade.DataTextField = ds.Tables[0].Columns[1].ToString();
            ddlTrade.DataSource = ds;
            //ddlResourcetype.DataSource = LookUp.GetMaterialRecords();
            ddlTrade.DataBind();
            ddlTrade.Items.Insert(0, new ListItem("--SELECT--", "0"));
        }
        public void BindContacts(HRCommon objHrCommon)
        {
            objHrCommon.PageSize = EmpListPaging.ShowRows;
            objHrCommon.CurrentPage = EmpListPaging.CurrentPage;
            objHrCommon.CID = Convert.ToInt32(ddlTrade.SelectedValue);
            objHrCommon.RepName = txtRefName.Text; //string.Empty;
            objHrCommon.GID = Convert.ToInt32(ddlContGroup.SelectedValue);
            DataSet  ds = objContacts.SearchContactsList(objHrCommon);
            if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0)
            {
                gvContacts.DataSource = ds;
            }
            gvContacts.DataBind();
            EmpListPaging.Bind(objHrCommon.CurrentPage, objHrCommon.TotalPages, objHrCommon.NoofRecords, objHrCommon.PageSize);
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            int ContactID;
            if (ViewState["ContactId"] != null && ViewState["ContactId"].ToString() != "0")
            {
                ContactID = Convert.ToInt32(ViewState["ContactId"].ToString());
            }
            else
            {
                ContactID = 0;
            }
            int CID = Convert.ToInt32(ddlCategory.SelectedValue);
            string FName = txtFName.Text;
            string MName = txtMName.Text;
            string LName = txtLName.Text;
            string Phone1;
            Phone1 = txtphone1.Text;
            string Phone2;
            Phone2 = txtphone2.Text;
            string EMailID = txtEmail.Text;
            string web = txtSite.Text;
            string Address = txtAddress.Text;
            string Notes = txtNotes.Text;
            string Designation = txtDesig.Text;
            string Fax = txtFax.Text;
            int UserID =  Convert.ToInt32(Session["UserId"]);
            int GID = Convert.ToInt32(ddlContactGroup.SelectedValue);
            string filename = "", ext = "", path = "";
            if (flImage.PostedFile != null)
            {
                filename = flImage.PostedFile.FileName;
            }
            if (filename != "" || filename != null)
            {
                ext = filename.Split('.')[filename.Split('.').Length - 1];
            }
            else
            {
                if (ViewState["ext"].ToString() != "")
                {
                    ext = ViewState["ext"].ToString();
                }
                else
                {
                    ext = "";
                }
            }
            int? phExt = null;
            if (txtphExt.Text.Trim() != "")
            {
                phExt = Convert.ToInt32(txtphExt.Text.Trim());
            }
            try
            {
                int ContID = AttendanceDAC.HR_InsUpContactsDetails(ContactID, CID, FName, MName, LName, Phone1, Phone2, EMailID, web, Address, Notes, UserID, GID, ext, txtIM.Text, Designation, Fax, phExt);
                if (filename != "")
                {
                    if (ContID != 0)
                    {
                        path = Server.MapPath(".\\ContactImages\\" + ContID + "." + ext);
                        try { PicUtil.ConvertPic(path, 128, 160, path); }
                        catch { AlertMsg.MsgBox(Page, "Unable to compress image!"); }
                        try
                        {
                            flImage.PostedFile.SaveAs(path);
                        }
                        catch { throw new Exception("FileProof.PostedFile.SaveAs(path)"); }
                    }
                }
                BindContacts(objHrCommon);
                mainview.ActiveViewIndex = 1;
                try
                {
                    imgemp.ImageUrl = "ContactImages/" + ContID + "." + ext;
                }
                catch { }
            }
            catch (Exception contacts)
            {
                AlertMsg.MsgBox(Page, contacts.Message.ToString(),AlertMsg.MessageType.Error);
            }
            if (btnSave.Text == "Save")
                AlertMsg.MsgBox(Page, "Saved");
            else
                AlertMsg.MsgBox(Page, "Updated");
        }
        protected string BindImg(string CID, string ext)
        {
            return "ContactImages/" + CID + "." + ext;
        }
        protected string Email(string MailID)
        {
            return "mailto:" + MailID + "&cc=dms@" + Domain;
        }
        protected bool WebVisible(string Web)
        {
            if (Web != "")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        protected string Web(string Website)
        {
            if (Website != "")
            {
                return "javascript:return window.open('http://" + Website + "','_blank')";
            }
            else
            {
                return null;
            }
        }
        protected bool NotesVisible(string Notes)
        {
            if (Notes != "")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        protected string ViewImg(string CID, string ext)
        {
            if (ext != "")
            {
                return "javascript:return window.open('ViewImage.aspx?Id=" + CID + "&ext=" + ext + "', '_blank','height=300, width=350, status=no, resizable= yes, menubar=no, toolbar=no, location=0, scrollbars=yes')";
            }
            else
            {
                return null;
            }
        }
        protected bool ImgVisible(string ext)
        {
            if (ext != "")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        protected void gvContacts_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Edt")
            {
                int ContactId = Convert.ToInt32(e.CommandArgument);
                ViewState["ContactId"] = ContactId;
                DataSet ds = AttendanceDAC.HR_GetConatactDetails(ContactId);
                ViewState["ext"] = ds.Tables[0].Rows[0]["ext"].ToString();
                try
                {
                    imgemp.Visible = false;
                    if (ViewState["ext"].ToString() != "")
                    {
                        imgemp.Visible = true;
                        imgemp.ImageUrl = "ContactImages/" + ContactId + "." + ViewState["ext"].ToString();
                    }
                }
                catch { }
                BindContactDetails(ContactId);
                btnSave.Text = "Update";
                mainview.ActiveViewIndex = 0;
                BindContacts(objHrCommon);
            }
            if (e.CommandName == "Del")
            {
                int ContactId = Convert.ToInt32(e.CommandArgument);
                objContacts.DeleteContact(ContactId);
                AlertMsg.MsgBox(Page, "Deleted Successfully");
                BindContacts(objHrCommon);
            }
        }
        public void BindContactDetails(int ContactId)
        {
          DataSet  ds = objContacts.GetContactsDetails(ContactId);
            if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0)
            {
                try
                {
                    ddlCategory.SelectedValue = ds.Tables[0].Rows[0]["CID"].ToString();
                    //txtOther.Text = ds.Tables[0].Rows[0][""].ToString();
                    txtFName.Text = ds.Tables[0].Rows[0]["FName"].ToString();
                    txtMName.Text = ds.Tables[0].Rows[0]["MName"].ToString();
                    txtLName.Text = ds.Tables[0].Rows[0]["LName"].ToString();
                    txtphone1.Text = ds.Tables[0].Rows[0]["Phone1"].ToString();
                    txtphone2.Text = ds.Tables[0].Rows[0]["Phone2"].ToString();
                    txtEmail.Text = ds.Tables[0].Rows[0]["EMailID"].ToString();
                    txtSite.Text = ds.Tables[0].Rows[0]["web"].ToString();
                    txtAddress.Text = ds.Tables[0].Rows[0]["Address"].ToString().Replace("<br/>", "\n");
                    txtNotes.Text = ds.Tables[0].Rows[0]["Notes"].ToString().Replace("<br/>", "\n");
                    ddlContactGroup.SelectedValue = ds.Tables[0].Rows[0]["GID"].ToString();
                    txtIM.Text = ds.Tables[0].Rows[0]["IM"].ToString();
                    txtDesig.Text = ds.Tables[0].Rows[0]["Designation"].ToString();
                    txtFax.Text = Convert.ToString(ds.Tables[0].Rows[0]["Fax"]);
                    txtphExt.Text = Convert.ToString(ds.Tables[0].Rows[0]["PhExt"]);
                }
                catch { AlertMsg.MsgBox(Page, "Unable to bind some values!"); }
            }
        }
        protected void lnkAdd_Click(object sender, EventArgs e)
        {
            lnkAdd.Visible = false;
            txtOther.Visible = true; btnaddnew.Visible = true; txtOther.Text = "";
        }
        protected void btnaddnew_Click(object sender, EventArgs e)
        {
            lnkAdd.Visible = true;
            txtOther.Visible = false; btnaddnew.Visible = false;
            string ContactType = txtOther.Text;
            if (ContactType != "")
            {
                int output = AttendanceDAC.HR_InsUpContactType(ContactType);
                BindCategories();
                if (output == 1)
                    AlertMsg.MsgBox(Page, "Done!");
                else
                    AlertMsg.MsgBox(Page, "Already Exists!");
            }
            else
            {
                AlertMsg.MsgBox(Page, "Field Should not be Empty!");
            }
        }
        protected void btncancel_Click(object sender, EventArgs e)
        {
            ddlCategory.SelectedIndex = 0;
            txtAddress.Text = txtNotes.Text = txtphone1.Text = txtphone2.Text = txtEmail.Text = txtMName.Text = txtLName.Text = txtFName.Text = "";
        }
        protected void btnFind_Click(object sender, EventArgs e)
        {
            EmpListPaging.CurrentPage = 1;
            objHrCommon.PageSize = EmpListPaging.ShowRows;
            objHrCommon.CurrentPage = EmpListPaging.CurrentPage;
            objHrCommon.CID = Convert.ToInt32(ddlTrade.SelectedValue);
            objHrCommon.RepName = txtRefName.Text.Trim();
            objHrCommon.GID = Convert.ToInt32(ddlContGroup.SelectedValue);
            DataSet ds = objContacts.SearchContactsList(objHrCommon);
            if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0)
            {
                gvContacts.DataSource = ds;
            }
            gvContacts.DataBind();
            EmpListPaging.Bind(objHrCommon.CurrentPage, objHrCommon.TotalPages, objHrCommon.NoofRecords, objHrCommon.PageSize);
        }
        protected void lnkNewGroup_Click(object sender, EventArgs e)
        {
            lnkNewGroup.Visible = false;
            txtNewGroup.Visible = true;
            btnaddGroup.Visible = true;
            txtNewGroup.Text = "";
        }
        protected void btnaddGroup_Click(object sender, EventArgs e)
        {
            int GID = 0;
            if (txtNewGroup.Text != "")
            {
                int output = AttendanceDAC.HR_InsUpContactGroup(GID, txtNewGroup.Text);
                BindContactGroup();
                if (output == 1)
                    AlertMsg.MsgBox(Page, "Done!");
                else if (output == 2)
                    AlertMsg.MsgBox(Page, "Alredy Exists!");
                else
                    AlertMsg.MsgBox(Page, "Updated!");
                lnkNewGroup.Visible = true;
                txtNewGroup.Visible = false;
                btnaddGroup.Visible = false;
            }
            else
            {
                AlertMsg.MsgBox(Page, "Enter Group!");
            }
        }
        protected void ddlTrade_SelectedIndexChanged(object sender, EventArgs e)
        {
            EmpListPaging.CurrentPage = 1;
        }
        protected void ddlContGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            EmpListPaging.CurrentPage = 1;
        }
        //this is done by chaitanya for googlesearch
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetConName(string prefixText, int count, string contextKey)
        {
           DataSet ds = AttendanceDAC.BindCONname_googlesearch(prefixText);
            DataTable dt = ds.Tables[0];
            List<string> items = new List<string>(count);
            var rtval = new Dictionary<string, string>();
            foreach (DataRow row in dt.Rows)
            {
                string str = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(row["Name"].ToString(), row["Id"].ToString());//DepartmentName
                items.Add(str);
            }
            return items.ToArray(); ;// txtItems.ToArray();
        }
        protected void gvContacts_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton lnkedt = (LinkButton)e.Row.FindControl("lnkedt");
                LinkButton lnkDel = (LinkButton)e.Row.FindControl("lnkdel");
                lnkedt.Enabled = lnkDel.Enabled = Editable;
            }
        }
    }
}