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
using System.Data.SqlClient;
using AECLOGIC.HMS.BLL;
using AECLOGIC.ERP.COMMON;
using Aeclogic.Common.DAL;
using System.Collections.Generic;
namespace AECLOGIC.ERP.HMS.HMS
{
    public partial class HMS_Relations : AECLOGIC.ERP.COMMON.WebFormMaster
    {
        private GridSort objSort;
        DataSet dsPassengerType;
        //int status;
        int mid = 0;
        bool viewall, Editable;
        string menuname;
        string menuid;
        int Status;
        HRCommon objHrCommon = new HRCommon();
        protected override void OnInit(EventArgs e)
        {
            ModuleID = 1;
            base.OnInit(e);
            // btnSave.Attributes.Add("onclick", "javascript:return ValidateSave('" + txtCategoryName.ClientID + "');");
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
                if (rblstStatus.SelectedValue == "1")
                {
                    Status = 1;
                }
                else
                {
                    Status = 0;
                }
              DataSet  ds = AttendanceDAC.HR_GetRelationTypeByStatus(objHrCommon, Status,0);
                if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0)
                {
                    gvEditRelationType.DataSource = ds;
                    gvEditRelationType.DataBind();
                }
                else
                {
                    gvEditRelationType.DataSource = null;
                    gvEditRelationType.DataBind();
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
            if (!IsPostBack)
            {
                GetParentMenuId();
                ViewState["RelationTypeID"] = "";
                btnSave.Attributes.Add("onclick", "javascript:return validatesave();");
                objSort = new GridSort();
                ViewState["Sort"] = objSort;
                EmployeBind(objHrCommon);
                mainview.ActiveViewIndex = 1;
                if (Request.QueryString.Count > 0)
                {
                    int id = Convert.ToInt32(Request.QueryString["key"].ToString());
                    if (id == 1)
                    {
                        mainview.ActiveViewIndex = 0;
                    }
                }
            }
        }
        public string GetText()
        {
            if (rblstStatus.SelectedValue == "1")
            {
                return "Delete";
            }
            else
            {
                return "Active";
            }
        }
        public int GetParentMenuId()
        {
            string URL = Request.Url.Segments[Request.Url.Segments.Length - 1] + Request.Url.Query;
            int RoleId = Convert.ToInt32(Session["RoleId"].ToString());
            int ModuleId = ModuleID; ;
          DataSet  ds = AttendanceDAC.GetAllowed(RoleId, ModuleId, URL);
            int MenuId = 0;
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                MenuId = Convert.ToInt32(ds.Tables[0].Rows[0]["Under"]);
                gvEditRelationType.Columns[1].Visible = Convert.ToBoolean(ds.Tables[0].Rows[0]["Editable"].ToString());
                btnSave.Enabled = (bool)ds.Tables[0].Rows[0]["Editable"];
            }
            return MenuId;
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                {
                    int RelationTypeID = 0;
                    if (ViewState["RelationTypeID"].ToString() != null && ViewState["RelationTypeID"].ToString() != string.Empty)
                    {
                        RelationTypeID = Convert.ToInt32(ViewState["RelationTypeID"].ToString());
                    }
                    int stats = 1;
                    if (chkStatus.Checked == true)
                    {
                        stats = 1;
                    }
                    else
                    {
                        stats = 0;
                    }
                    string RelationTypeName = txtRelationType.Text;
                    int Output = AttendanceDAC.InsUpdRelationType(RelationTypeID, RelationTypeName, stats,  Convert.ToInt32(Session["UserId"]));
                    if (Output == 1)
                    {
                        //AlertMsg.MsgBox(Page, "Inserted Sucessfully");
                        lblStatus.Text = "Inserted Sucessfully";
                        lblStatus.ForeColor = System.Drawing.Color.Green;
                    }
                    else if (Output == 2)
                    {
                        //AlertMsg.MsgBox(Page, "Already Exists");
                        lblStatus.Text = "Already Exists";
                        lblStatus.ForeColor = System.Drawing.Color.Red;
                    }
                    else
                    {
                        //AlertMsg.MsgBox(Page, "Updated Sucessfully");
                        lblStatus.Text = "Updated Sucessfully";
                        lblStatus.ForeColor = System.Drawing.Color.Green;
                    }
                    EmployeBind(objHrCommon);
                    mainview.ActiveViewIndex = 1;
                }
                ViewState["RelationTypeID"] = 0;
            }
            catch (Exception ex)
            {
                AlertMsg.MsgBox(Page, "Sorry for the inconvinience. Try again.\nError:" + ex.Message.ToString(),AlertMsg.MessageType.Error);
            }
        }
        protected void gvEditRelationType_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int RelationTypeID = Convert.ToInt32(e.CommandArgument);
            if (e.CommandName == "Edt")
            {
                ViewState["RelationTypeID"] = Convert.ToInt32(e.CommandArgument);
                int id = (Convert.ToInt32(ViewState["RelationTypeID"]));
                BindPassengerTypeDetails(id);
                btnSave.Text = "Update";
                mainview.ActiveViewIndex = 0;
            }
            if (e.CommandName == "Del")
            {
                try
                {
                    int Status;
                    GridViewRow gvr = (GridViewRow)(((Control)e.CommandSource).NamingContainer);
                    LinkButton lnkDel = (LinkButton)gvr.FindControl("lnkDel");
                    if (lnkDel.Text == "Active")
                    {
                        Status = 1;
                    }
                    else
                    {
                        Status = 0;
                    }
                    AttendanceDAC.UpdateRelationTypeStatus(RelationTypeID, Status);
                    EmployeBind(objHrCommon);
                    //  AlertMsg.MsgBox(Page, "Done..!");
                    lblStatus.Text = "Done..!!";
                    lblStatus.ForeColor = System.Drawing.Color.Green;
                }
                catch (Exception DeptHead)
                {
                    AlertMsg.MsgBox(Page, DeptHead.Message.ToString(),AlertMsg.MessageType.Error);
                }
            }
        }
        public void BindPassengerTypeDetails(int Id)
        {
            SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings["strConn"]);
            cn.Open();
            SqlDataAdapter da = new SqlDataAdapter("select * from T_HMS_Relation where  ID='" + Id + "'", cn);
            DataSet ds = new DataSet(); 
            da.Fill(ds, "temp");
            txtRelationType.Text = ds.Tables[0].Rows[0]["Name"].ToString();
            int status = Convert.ToInt32(ds.Tables[0].Rows[0]["active"].ToString());
            if (status == 1)
            {
                chkStatus.Checked = true;
            }
            else
            {
                chkStatus.Checked = false;
            }
            cn.Close();
        }
        protected void gvRelationType_Sorting(object sender, GridViewSortEventArgs e)
        {
            try
            {
                //SortGrid Object from ViewState
                objSort = (GridSort)ViewState["Sort"];
                //Get dataSet from ViewState
                dsPassengerType = (DataSet)ViewState["DataSet"];
                DataView dvDepartments = dsPassengerType.Tables[0].DefaultView;
                //Get SortExpresssion from sordGrid Object
                dvDepartments.Sort = objSort.GetSortExpression(e.SortExpression);
                gvEditRelationType.DataSource = dvDepartments;
                gvEditRelationType.DataBind();
                //reset SortGrid object which is in ViewState
                ViewState["Sort"] = objSort;
            }
            catch (Exception ex)
            {
            }
        }
        protected void rblstStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            EmpListPaging.CurrentPage = 1;
            EmployeBind(objHrCommon);
        }
        #region RijwanCodeforService
        protected void btnsearch_Click(object sender, EventArgs e)
        {
            try
            {
                EmpListPaging.CurrentPage = 1;
                objHrCommon.PageSize = EmpListPaging.ShowRows;
                objHrCommon.CurrentPage = EmpListPaging.CurrentPage;
                if (rblstStatus.SelectedValue == "1")
                {
                    Status = 1;
                }
                else
                {
                    Status = 0;
                }
             DataSet  ds = AttendanceDAC.HR_GetRelationTypeByStatus(objHrCommon, Status, Convert.ToInt32(RelType_hd.Value == "" ? "0" : RelType_hd.Value));
                if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0)
                {
                    gvEditRelationType.DataSource = ds;
                    gvEditRelationType.DataBind();
                }
                else
                {
                    gvEditRelationType.DataSource = null;
                    gvEditRelationType.DataBind();
                }
                EmpListPaging.Bind(objHrCommon.CurrentPage, objHrCommon.TotalPages, objHrCommon.NoofRecords, objHrCommon.PageSize);
                if (txtRelationType.Text == "" )
                    RelType_hd.Value = string.Empty;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionList(string prefixText, int count, string contextKey)
        {
            DataSet ds = AttendanceDAC.GetRelationtype_Search(prefixText);
            DataTable dt = ds.Tables[0];
            List<string> items = new List<string>(count);
            var rtval = new Dictionary<string, string>();
            foreach (DataRow row in dt.Rows)
            {
                string str = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(row["Name"].ToString(), row["ID"].ToString());
                items.Add(str);
            }
            return items.ToArray();// txtItems.ToArray()
        }
        #endregion RijwanCodeforService
        protected void gvEditRelationType_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton lnkEdt = (LinkButton)e.Row.FindControl("lnkEdt");
                LinkButton lnkDel = (LinkButton)e.Row.FindControl("lnkDel");
                lnkEdt.Enabled = lnkDel.Enabled = true;
            }
        }
    }
}