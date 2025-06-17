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
    public partial class HMS_Airline : AECLOGIC.ERP.COMMON.WebFormMaster
    {
        private GridSort objSort;
        DataSet dsDepartments;
        int mid = 0;
        bool viewall, Editable;
        string menuname;
        string menuid;
        bool Status;
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
        protected void Page_Load(object sender, EventArgs e)
        {
            lblStatus.Text = String.Empty;
            if (!IsPostBack)
            {
                GetParentMenuId();
                ViewState["ID"] = "";
                btnSbmt.Attributes.Add("onclick", "javascript:return validatesave();");
                objSort = new GridSort();
                ViewState["Sort"] = objSort;
                AirlineNMBind(objHrCommon);
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
        void EmpListPaging_ShowRowsClick(object sender, EventArgs e)
        {
            EmpListPaging.CurrentPage = 1;
            BindPager();
        }
        void EmpListPaging_FirstClick(object sender, EventArgs e)
        {
            BindPager();
        }
        public int GetParentMenuId()
        {
            string URL = Request.Url.Segments[Request.Url.Segments.Length - 1] + Request.Url.Query;
            int RoleId = Convert.ToInt32(Session["RoleId"].ToString());
            int ModuleId = ModuleID; ;
            DataSet ds = AttendanceDAC.GetAllowed(RoleId, ModuleId, URL);
            int MenuId = 0;
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                MenuId = Convert.ToInt32(ds.Tables[0].Rows[0]["Under"]);
                Editable = (bool)ds.Tables[0].Rows[0]["Editable"];
                gvArlNM.Columns[1].Visible = Convert.ToBoolean(ds.Tables[0].Rows[0]["Editable"].ToString());
                btnSbmt.Enabled = (bool)ds.Tables[0].Rows[0]["Editable"];
                btnSbmt.Enabled = Editable;
            }
            return MenuId;
        }
        protected void btnSbmt_Click(object sender, EventArgs e)
        {
            try
            {
                {
                    int ID = 0;
                    if (ViewState["ID"].ToString() != null && ViewState["ID"].ToString() != string.Empty)
                    {
                        ID = Convert.ToInt32(ViewState["ID"].ToString());
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
                    string Name = txtArLn.Text.Trim();
                    int userid = Convert.ToInt32(Session["UserId"]);
                    int Output = AttendanceDAC.InsUpdAirlineNm(ID, Name, stats, userid);
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
                    AirlineNMBind(objHrCommon);
                    mainview.ActiveViewIndex = 1;
                }
                ViewState["ID"] = 0;
                txtArLn.Text = "";
            }
            catch (Exception ex)
            {
                // AlertMsg.MsgBox(Page, "Sorry for the inconvinience. Try again.\nError:" + ex.Message.ToString(),AlertMsg.MessageType.Error);
                lblStatus.Text = "Sorry for the inconvinience. Try again.\nError:" + ex.Message;
                lblStatus.ForeColor = System.Drawing.Color.Red;
            }
        }
        void BindPager()
        {
            objHrCommon.PageSize = EmpListPaging.CurrentPage;
            objHrCommon.CurrentPage = EmpListPaging.ShowRows;
            AirlineNMBind(objHrCommon);
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
        void AirlineNMBind(HRCommon objHrCommon)
        {
            try
            {
                // bool status;
                objHrCommon.PageSize = EmpListPaging.ShowRows;
                objHrCommon.CurrentPage = EmpListPaging.CurrentPage;
                if (rblstStatus.SelectedValue == "1")
                {
                    Status = true;
                }
                else
                {
                    Status = false;
                }
                DataSet ds = AttendanceDAC.HR_GetAirlineByStatus(objHrCommon, Status);
                if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0)
                {
                    gvArlNM.DataSource = ds;
                    gvArlNM.DataBind();
                }
                else
                {
                    gvArlNM.DataSource = null;
                    gvArlNM.DataBind();
                }
                EmpListPaging.Bind(objHrCommon.CurrentPage, objHrCommon.TotalPages, objHrCommon.NoofRecords, objHrCommon.PageSize);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        protected void rblstStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            EmpListPaging.CurrentPage = 1;
            AirlineNMBind(objHrCommon);
        }
        public void BindAirplnDetails(int Id)
        {
            //SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["strConn"]);
            //con.Open();
            //SqlDataAdapter da = new SqlDataAdapter("select * from T_HMS_Airline where ID ='" + Id + "'", con);
            //DataSet ds = null; 
            //da.Fill(ds, "temp");
            SqlParameter[] sqlParams = new SqlParameter[1];
            sqlParams[0] = new SqlParameter("@id", Id);
            DataSet ds = SQLDBUtil.ExecuteDataset("sh_AirlinesEdt", sqlParams);
            txtArLn.Text = ds.Tables[0].Rows[0]["Name"].ToString();
            int status = Convert.ToInt32(ds.Tables[0].Rows[0]["active"].ToString());
            if (status == 1)
            {
                chkStatus.Checked = true;
            }
            else
            {
                chkStatus.Checked = false;
            }
            // con.Close();
        }
        protected void gvArlNM_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int ID = Convert.ToInt32(e.CommandArgument);
            int active = Convert.ToInt32(e.CommandArgument);
            if (e.CommandName == "Edt")
            {
                ViewState["ID"] = Convert.ToInt32(e.CommandArgument);
                int id = (Convert.ToInt32(ViewState["ID"]));
                BindAirplnDetails(id);
                btnSbmt.Text = "Update";
                mainview.ActiveViewIndex = 0;
            }
            if (e.CommandName == "Del")
            {
                try
                {
                    bool Status;
                    GridViewRow gvr = (GridViewRow)(((Control)e.CommandSource).NamingContainer);
                    LinkButton lnkDel = (LinkButton)gvr.FindControl("lnkDel");
                    if (lnkDel.Text == "Active")
                    {
                        Status = true;
                    }
                    else
                    {
                        Status = false;
                    }
                    AttendanceDAC.UpdateAirlineStatus(ID, Status);
                    AirlineNMBind(objHrCommon);
                    //  AlertMsg.MsgBox(Page, "Done..!");
                    lblStatus.Text = "Done..!!";
                    lblStatus.ForeColor = System.Drawing.Color.Green;
                }
                catch (Exception DeptHead)
                {
                    AlertMsg.MsgBox(Page, DeptHead.Message.ToString(), AlertMsg.MessageType.Error);
                }
            }
        }
        protected void gvArlNM_Sorting(object sender, GridViewSortEventArgs e)
        {
            try
            {
                objSort = (GridSort)ViewState["Sort"];
                dsDepartments = (DataSet)ViewState["DataSet"];
                DataView dvDepartments = dsDepartments.Tables[0].DefaultView;
                dvDepartments.Sort = objSort.GetSortExpression(e.SortExpression);
                gvArlNM.DataSource = dvDepartments;
                gvArlNM.DataBind();
                ViewState["Sort"] = objSort;
            }
            catch (Exception ex)
            {
            }
        }
        #region RijwanCodeforService
        protected void btnsearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtAirtype.Text == "")
                    AirType_hd.Value = string.Empty;
                EmpListPaging.CurrentPage = 1;
                objHrCommon.PageSize = EmpListPaging.ShowRows;
                objHrCommon.CurrentPage = EmpListPaging.CurrentPage;
                if (rblstStatus.SelectedValue == "1")
                {
                    Status = true;
                }
                else
                {
                    Status = false;
                }
                DataSet ds = AttendanceDAC.HR_GetAirlineByStatus_New(objHrCommon, Status, Convert.ToInt32(AirType_hd.Value == "" ? "0" : AirType_hd.Value));
                if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0)
                {
                    gvArlNM.DataSource = ds;
                    gvArlNM.DataBind();
                }
                else
                {
                    gvArlNM.DataSource = null;
                    gvArlNM.DataBind();
                }
                EmpListPaging.Bind(objHrCommon.CurrentPage, objHrCommon.TotalPages, objHrCommon.NoofRecords, objHrCommon.PageSize);
                if (txtAirtype.Text == "")
                    AirType_hd.Value = string.Empty;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionList(string prefixText, int count, string contextKey)
        {
            DataSet ds = AttendanceDAC.GetAirLiens_Search(prefixText);
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
    }
}