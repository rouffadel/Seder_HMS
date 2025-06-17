using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using AECLOGIC.HMS.BLL;
using AECLOGIC.ERP.COMMON;
namespace AECLOGIC.ERP.HMS
{
    public partial class Documents : AECLOGIC.ERP.COMMON.WebFormMaster
    {
        AttendanceDAC objDoc = new AttendanceDAC();
        HRCommon objHrCommon = new HRCommon();
          
        int mid = 0;
        bool viewall;
        string menuname;
        string menuid;
        static string strUrl = string.Empty;
        static bool status;
        protected override void OnInit(EventArgs e)
        {
            ModuleID = 1;
            base.OnInit(e);
            // btnSave.Attributes.Add("onclick", "javascript:return ValidateSave('" + txtCategoryName.ClientID + "');");
            DocumentsPaging.FirstClick += new Paging.PageFirst(DocumentsPaging_FirstClick);
            DocumentsPaging.PreviousClick += new Paging.PagePrevious(DocumentsPaging_FirstClick);
            DocumentsPaging.NextClick += new Paging.PageNext(DocumentsPaging_FirstClick);
            DocumentsPaging.LastClick += new Paging.PageLast(DocumentsPaging_FirstClick);
            DocumentsPaging.ChangeClick += new Paging.PageChange(DocumentsPaging_FirstClick);
            DocumentsPaging.ShowRowsClick += new Paging.ShowRowsChange(DocumentsPaging_ShowRowsClick);
            DocumentsPaging.CurrentPage = 1;
        }
        void DocumentsPaging_ShowRowsClick(object sender, EventArgs e)
        {
            DocumentsPaging.CurrentPage = 1;
            BindPager();
        }
        void DocumentsPaging_FirstClick(object sender, EventArgs e)
        {
            BindPager();
        }
        void BindPager()
        {

            objHrCommon.PageSize = DocumentsPaging.CurrentPage;
            objHrCommon.CurrentPage = DocumentsPaging.ShowRows;
            Bindgrid(objHrCommon);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (rbShowActive.Checked)
            {
                status = true;
            }
            else
            {
                status = false;
            }
            if (Convert.ToInt32(Request.QueryString["key"]) == 2)
            {
                MainView.ActiveViewIndex = 0;
                BindDetails(2);
            }
            if (Convert.ToInt32(Request.QueryString["key"]) == 1)
            {
                MainView.ActiveViewIndex = 1;
                btnBack.Visible = true;
            }
            if (!IsPostBack)
            {
                GetParentMenuId();
                BindDocClass();
                ViewState["DocID"] = "";
                BindPager();
                if (Convert.ToInt32(Request.QueryString["key"]) != 1)
                {
                    MainView.ActiveViewIndex = 1;
                }
                if (Convert.ToInt32(Request.QueryString["key"]) == 1)
                {
                    MainView.ActiveViewIndex = 0;
                }
            }
            
        }
        public int GetParentMenuId()
        {
            string URL = Request.Url.Segments[Request.Url.Segments.Length - 1] + Request.Url.Query;
            int RoleId = Convert.ToInt32(Session["RoleId"].ToString());
            int ModuleId = ModuleID;;

              

         DataSet  ds = AttendanceDAC.GetAllowed(RoleId, ModuleId, URL);
            int MenuId = 0;
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                MenuId = Convert.ToInt32(ds.Tables[0].Rows[0]["Under"]);
                gdvDoc.Columns[2].Visible = Convert.ToBoolean(ds.Tables[0].Rows[0]["Editable"].ToString());
                gdvDoc.Columns[1].Visible = Convert.ToBoolean(ds.Tables[0].Rows[0]["Editable"].ToString());
                menuid = MenuId.ToString();
                btnSubmit.Enabled = (bool)ds.Tables[0].Rows[0]["Editable"];
            }
            return MenuId;
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["DocID"].ToString() != "" && ViewState["DocID"].ToString() != string.Empty)
                {
                    objHrCommon.DocID = Convert.ToInt32(ViewState["DocID"].ToString());
                }
                else
                {
                    objHrCommon.DocID = 0;
                }
                objHrCommon.DocName = txtDocName.Text.Trim();
                objHrCommon.DocText = DocEditor.Text;
                objHrCommon.UserID = Convert.ToInt32( Convert.ToInt32(Session["UserId"]).ToString());
                int OutPut = AttendanceDAC.Ins_UpdDocument(objHrCommon, ModuleID, Convert.ToInt32(ddlDocClss.SelectedValue), Convert.ToInt32(Session["CompanyID"].ToString()));
                if (OutPut == 1)
                {
                    //AlertMsg.MsgBox(Page, "Inserted sucessfully.!");
                    lblStatus.Text = "Inserted sucessfully.!";
                    lblStatus.ForeColor = System.Drawing.Color.Green;
                }
                else if (OutPut == 2)
                {
                   // AlertMsg.MsgBox(Page, "Already exists.!");
                    lblStatus.Text = "Already exists.!";
                    lblStatus.ForeColor = System.Drawing.Color.Red;
                }
                else
                    //    AlertMsg.MsgBox(Page, "Updated sucessfully.!");
                    //Response.Redirect("~/hms/Documents.aspx");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "alert('Updated sucessfully...');window.location='Documents.aspx';", true);
               
                Bindgrid(objHrCommon);
                btnSubmit.Text = "Submit";
                txtDocName.Text = "";
                DocEditor.Text = "";
                ViewState["DocID"] = "";
            }
            catch (Exception Doc)
            {
               // AlertMsg.MsgBox(Page, Doc.Message.ToString(),AlertMsg.MessageType.Error);
                lblStatus.Text = Doc.Message.ToString();
                lblStatus.ForeColor = System.Drawing.Color.Red;
            }

        }

        void Bindgrid(HRCommon objHrCommon)
        {

            try
            {
                objHrCommon.PageSize = DocumentsPaging.ShowRows;
                objHrCommon.CurrentPage = DocumentsPaging.CurrentPage;

                int? DocClassID = null;
                if (ddlDocClass.SelectedIndex != 0)
                    DocClassID = Convert.ToInt32(ddlDocClass.SelectedValue);
                string DocName = null;
                if (SearchtxtDocName.Text != null)
                    DocName = SearchtxtDocName.Text;
                int ModuleId = ModuleID;;
                  
                bool Status = false;
                if (rbShowActive.Checked)
                {
                    Status = true;
                }
                 int DOCID = 0; if (txtDocName.Text.Trim() != "")
                 { DOCID = Convert.ToInt32(ddlDoc_hid.Value == "" ? "0" : ddlDoc_hid.Value); }
                 DataSet ds = AttendanceDAC.GetDocumentsListByPaging(objHrCommon, ModuleId, DocName, DocClassID, Convert.ToInt32(Session["CompanyID"]), Status, DOCID);
                if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0)
                {
                    gdvDoc.DataSource = ds;
                    gdvDoc.DataBind();
                }
                else
                {
                    gdvDoc.DataSource = null;
                    gdvDoc.DataBind();
                }
                DocumentsPaging.Bind(objHrCommon.CurrentPage, objHrCommon.TotalPages, objHrCommon.NoofRecords, objHrCommon.PageSize);

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private void BindDocClass()
        {
            int ModuleId = ModuleID;;
            DataSet ds = AttendanceDAC.DMS_GetDocClass(ModuleId);
            ddlDocClss.DataSource = ds;
            ddlDocClss.DataTextField = ds.Tables[0].Columns["Name"].ToString();
            ddlDocClss.DataValueField = ds.Tables[0].Columns["ID"].ToString();
            ddlDocClss.DataBind();
            ddlDocClss.Items.Insert(0, new ListItem("---Select---", "0"));
            ddlDocClass.DataSource = ds;
            ddlDocClass.DataTextField = ds.Tables[0].Columns["Name"].ToString();
            ddlDocClass.DataValueField = ds.Tables[0].Columns["ID"].ToString();
            ddlDocClass.DataBind();
            ddlDocClass.Items.Insert(0, new ListItem("---Select---", "0"));

        }


        protected void gdvDoc_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int DocID = Convert.ToInt32(e.CommandArgument);
                int Status = 0;
                if (e.CommandName == "Edt")
                {
                    MainView.ActiveViewIndex = 0;
                    BindDetails(DocID);
                    btnSubmit.Text = "Update";
                }
                else
                    if (e.CommandName == "Status")
                    {
                        GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).Parent.Parent;
                        LinkButton lnkstatus = (LinkButton)gdvDoc.Rows[row.RowIndex].FindControl("lnkstatus");
                        if (lnkstatus.Text == "Deactivate")
                        {
                            Status = 0;
                        }
                        else
                        {
                            Status = 1;
                        }
                        UpdatesStatus(DocID, Status);
                        Bindgrid(objHrCommon);
                    }
            }
            catch (Exception DocEdt)
            {
                AlertMsg.MsgBox(Page, DocEdt.Message.ToString(),AlertMsg.MessageType.Error);
            }

        }
        public void UpdatesStatus(int DocID, int Status)
        {
            AttendanceDAC.UpdateDocStatus(DocID, Status);
        }
        public void BindDetails(int DocID)
        {
            DataSet ds = AttendanceDAC.GetDocumentDetails(DocID, 0);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                txtDocName.Text = ds.Tables[0].Rows[0]["DocName"].ToString();
                DocEditor.Text = ds.Tables[0].Rows[0]["Value"].ToString();
                ViewState["DocID"] = ds.Tables[0].Rows[0]["DocId"].ToString();
                ddlDocClss.SelectedValue = ds.Tables[0].Rows[0]["ClassID"].ToString();
                int TermLetter = Convert.ToInt32(ViewState["DocID"]);
                if (TermLetter == 2)
                {
                    tblEmpID.Visible = true;
                }
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
          
            EditView1.Visible = true;
            btnBack.Visible = false;
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            DocumentsPaging.CurrentPage = 1;
            BindPager();
        }

        protected void rbShowActive_CheckedChanged(object sender, EventArgs e)
        {
            BindPager();

        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletiondeptList(string prefixText, int count, string contextKey)
        {
            DataSet ds = AttendanceDAC.SH_DocMaster(prefixText,status);
            DataTable dt = ds.Tables[0];
            List<string> items = new List<string>(count);
            var rtval = new Dictionary<string, string>();
            foreach (DataRow row in dt.Rows)
            {
                string str = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(row["Name"].ToString(), row["ID"].ToString());
                items.Add(str);
            }

            return items.ToArray(); ;// txtItems.ToArray();

        }

       
    }
}