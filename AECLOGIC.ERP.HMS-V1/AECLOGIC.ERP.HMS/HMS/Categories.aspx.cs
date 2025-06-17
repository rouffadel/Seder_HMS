using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using AECLOGIC.HMS.BLL;
using AECLOGIC.ERP.COMMON;
namespace AECLOGIC.ERP.HMS
{
    public partial class Categories : AECLOGIC.ERP.COMMON.WebFormMaster
    {
        int mid = 0;
        bool viewall, Editable;
        string menuname;
        string menuid;

        HRCommon objHrCommon = new HRCommon();
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
            BindPager();
        }
        void BindPager()
        {

            objHrCommon.PageSize = EmpListPaging.CurrentPage;
            objHrCommon.CurrentPage = EmpListPaging.ShowRows;
            EmployeBind(objHrCommon);
        }
        void EmployeBind(HRCommon objHrCommon)
        {
         
            try
            {
                objHrCommon.PageSize = EmpListPaging.ShowRows;
                objHrCommon.CurrentPage = EmpListPaging.CurrentPage;
                bool Status = false;
                if (rblCat.SelectedValue == "1")
                {
                    Status = true;
                }
                int categid = 0; if (txtsearchdept.Text.Trim() != "")
                { categid = Convert.ToInt32(ddldept_hid.Value == "" ? "0" : ddldept_hid.Value); }

                DataSet ds = AttendanceDAC.HR_GetCatogoryByStatus(objHrCommon, Status, categid);
                if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0)
                {
                    gvRMItem.DataSource = ds;
                    gvRMItem.DataBind();
                }
                else
                {
                    gvRMItem.DataSource = null;
                    gvRMItem.DataBind();
                }
                EmpListPaging.Bind(objHrCommon.CurrentPage, objHrCommon.TotalPages, objHrCommon.NoofRecords, objHrCommon.PageSize);

            }
            catch (Exception e)
            {
                throw e;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {

            lblStatus.Text = String.Empty;
            GetParentMenuId();
         
            if (!IsPostBack)
            {
                //GetParentMenuId();
                ViewState["CateId"] = "";
                tblEdit.Visible = true;
                if (Convert.ToInt32(Request.QueryString["key"]) == 1)
                {
                    this.Title = "Add Categories";
                    tblNew.Visible = true;
                    tblEdit.Visible = false;
                    gvRMItem.Visible = false;
                }
                else
                {
                    EmployeBind(objHrCommon);
                    tblNew.Visible = false;
                    tblEdit.Visible = true;
                    gvRMItem.Visible = true;
                }
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
                gvRMItem.Columns[1].Visible = Convert.ToBoolean(ds.Tables[0].Rows[0]["Editable"].ToString());
               Editable = (bool)ds.Tables[0].Rows[0]["Editable"];
               viewall = (bool)ds.Tables[0].Rows[0]["ViewAll"];
                btnSubmit.Enabled = (bool)ds.Tables[0].Rows[0]["Editable"];
                mid = Convert.ToInt32(ds.Tables[0].Rows[0]["MenuId"].ToString());
                btnSubmit.Enabled = Editable;
            }
            return MenuId;
        }
        public string GetText()
        {

            if (rblCat.SelectedValue == "1")
            {
                return "Delete";
            }
            else
            {
                return "Activate";
            }
        }

        public void BindDetails(int ID)
        {

            tblEdit.Visible = false;
            tblNew.Visible = true;
          DataSet  ds = AttendanceDAC.GetCategoriesDetails(ID);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                txtName.Text = ds.Tables[0].Rows[0]["Category"].ToString();

            }
        }
        protected void gvWages_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int ID = Convert.ToInt32(e.CommandArgument);
                bool Status = true;
                if (rblCat.SelectedValue == "1")
                {
                    Status = false;
                }
                ViewState["CateId"] = ID;
                if (e.CommandName == "Edt")
                {
                    BindDetails(ID);
                    btnSubmit.Text = "Update";
                }
                else
                {
                    AttendanceDAC.HR_Upd_CategoryStatus(ID, Status);
                    EmployeBind(objHrCommon);
                    if (rblCat.SelectedValue == "1")
                    {
                       // return "Delete";
                        lblStatus.Text = "Deleted !";
                        lblStatus.ForeColor = System.Drawing.Color.Green;
                    }
                    else
                    {
                        //return "Activate";
                        lblStatus.Text = "Activated !";
                        lblStatus.ForeColor = System.Drawing.Color.Green;
                    }
                }
            }
            catch (Exception CatDel)
            {
                AlertMsg.MsgBox(Page, CatDel.Message.ToString(),AlertMsg.MessageType.Error);
                lblStatus.Text = CatDel.Message.ToString();
                lblStatus.ForeColor = System.Drawing.Color.Red;
            }

        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                int CateId = 0;
                if (ViewState["CateId"].ToString() != null && ViewState["CateId"].ToString() != string.Empty)
                {
                    CateId = Convert.ToInt32(ViewState["CateId"].ToString());
                }

                if (!string.IsNullOrEmpty(txtName.Text.Trim()))
                {
                    int Output = AttendanceDAC.InsUpdCategories(CateId, txtName.Text.Trim());
                    EmployeBind(objHrCommon);
                    Clear();
                    if (Output == 1)
                    {
                       // AlertMsg.MsgBox(Page, "Done.! ");
                        lblStatus.Text = "Done!";
                        lblStatus.ForeColor = System.Drawing.Color.Green;

                    }
                    else if (Output == 2)
                    {
                        //AlertMsg.MsgBox(Page, "Already Exists.!");
                        lblStatus.Text = "Already Exists.!";
                        lblStatus.ForeColor = System.Drawing.Color.Red;

                    }
                    else
                    {
                     //   AlertMsg.MsgBox(Page, "Updated.!");
                        lblStatus.Text = "Updated.!";
                        lblStatus.ForeColor = System.Drawing.Color.Green;
                    }
                }
                else
                {
                    //AlertMsg.MsgBox(Page, "Please Enter Category.! ");
                    lblStatus.Text = "Please Enter Category.!";
                    lblStatus.ForeColor = System.Drawing.Color.Red;
                    txtName.Focus();
                    return ;

                }

            }
            catch (Exception Cat)
            {
                AlertMsg.MsgBox(Page, Cat.Message.ToString(),AlertMsg.MessageType.Error);
                lblStatus.Text = Cat.Message.ToString();
                lblStatus.ForeColor = System.Drawing.Color.Red;
            }
            
            tblEdit.Visible = true;
            tblNew.Visible = false;
            gvRMItem.Visible = true;
        }
        public void Clear()
        {
            txtName.Text = "";
            ViewState["CateId"] = "";
        }
        protected void lnkAdd_Click(object sender, EventArgs e)
        {

            tblNew.Visible = true;
            tblEdit.Visible = false;

        }
        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            tblEdit.Visible = true;
            tblNew.Visible = false;

        }
        protected void rblDesg_SelectedIndexChanged(object sender, EventArgs e)
        {
            EmpListPaging.CurrentPage = 1;
            EmployeBind(objHrCommon);
        }
        protected void gvRMItem_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{
            //    LinkButton lnkEdt = (LinkButton)e.Row.FindControl("lnkEdit");
            //    LinkButton lnkDel = (LinkButton)e.Row.FindControl("lnkDel");

            //    lnkEdt.Enabled = lnkDel.Enabled = Editable;
            //}
        }

        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletiondeptList(string prefixText, int count, string contextKey)
        {
            DataSet ds = AttendanceDAC.HR_CategoriesBySiteFilter(prefixText);
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

        protected void btnsearch_Click(object sender, EventArgs e)
        {
            EmployeBind(objHrCommon);
        }

    }

}