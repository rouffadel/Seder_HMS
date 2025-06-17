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

namespace AECLOGIC.ERP.HMS
{
    public partial class site_NewDept : AECLOGIC.ERP.COMMON.WebFormMaster
    {
        private GridSort objSort;
        DataSet dsDepartments;
        //int status;
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
                    Status = true;
                }
                else
                {
                    Status = false;
                }
                int deptid = 0; if (txtsearchdept.Text.Trim() != "")
                { deptid = Convert.ToInt32(ddldept_hid.Value == "" ? "0" : ddldept_hid.Value); }

                DataSet ds = AttendanceDAC.HR_GetDepartmentsByStatus(objHrCommon, Status,deptid);
                if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0)
                {
                    gvEditdept.DataSource = ds;
                    gvEditdept.DataBind();

                }
                else
                {
                    gvEditdept.DataSource = null;
                    gvEditdept.DataBind();
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

            if (!IsPostBack)
            {
                GetParentMenuId();

                ViewState["DeptID"] = "";
                Button1.Attributes.Add("onclick", "javascript:return validatesave();");
                objSort = new GridSort();
                ViewState["Sort"] = objSort;
                EmployeBind(objHrCommon);
                mainview.ActiveViewIndex = 1;
                if (Request.QueryString.Count > 0)
                {
                    int id = Convert.ToInt32(Request.QueryString["id"].ToString());

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



            DataSet ds = AttendanceDAC.GetAllowed(RoleId, ModuleId, URL);
            int MenuId = 0;
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                MenuId = Convert.ToInt32(ds.Tables[0].Rows[0]["Under"]);
                Editable = (bool)ds.Tables[0].Rows[0]["Editable"];

                gvEditdept.Columns[1].Visible = Convert.ToBoolean(ds.Tables[0].Rows[0]["Editable"].ToString());

                Button1.Enabled = (bool)ds.Tables[0].Rows[0]["Editable"];

                Button1.Enabled = Editable;
            }
            return MenuId;
        }
        protected void Button1_Click(object sender, EventArgs e)
        {

            try
            {
                {
                    int DeptID = 0;
                    if (ViewState["DeptID"].ToString() != null && ViewState["DeptID"].ToString() != string.Empty)
                    {
                        DeptID = Convert.ToInt32(ViewState["DeptID"].ToString());
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
                    string DeptName = TextBox1.Text;
                    if (!string.IsNullOrWhiteSpace(DeptName) && !string.IsNullOrEmpty(DeptName))
                    {
                        int Output = AttendanceDAC.InsUpdDepartment(DeptID, DeptName, stats);
                        if (Output == 1)
                            AlertMsg.MsgBox(Page, "Inserted Sucessfully");
                        else if (Output == 2)
                            AlertMsg.MsgBox(Page, "Already Exists");
                        else
                            AlertMsg.MsgBox(Page, "Updated Sucessfully");

                        EmployeBind(objHrCommon);
                        mainview.ActiveViewIndex = 1;
                    }
                    else
                    {
                        AlertMsg.MsgBox(Page, "Please Enter Department");
                        TextBox1.Focus();
                        return;

                    }

                }

                ViewState["DeptID"] = 0;
            }
            catch (Exception ex)
            {
                AlertMsg.MsgBox(Page, "Sorry for the inconvinience. Try again.\nError:" + ex.Message.ToString(), AlertMsg.MessageType.Error);
            }
        }


        protected void gvEditdept_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int DeptID = Convert.ToInt32(e.CommandArgument);
            if (e.CommandName == "Edt")
            {

                ViewState["DeptID"] = Convert.ToInt32(e.CommandArgument);
                int id = (Convert.ToInt32(ViewState["DeptID"]));
                BindDeptDetails(id);
                Button1.Text = "Update";
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
                    AttendanceDAC.UpdateDepartmentStatus(DeptID, Status);
                    EmployeBind(objHrCommon);
                    AlertMsg.MsgBox(Page, "Done..!");
                }


                catch (Exception DeptHead)
                {
                    AlertMsg.MsgBox(Page, DeptHead.Message.ToString(), AlertMsg.MessageType.Error);
                }

            }

        }


        public void BindDeptDetails(int Id)
        {

            //SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings["strConn"]);
            //cn.Open();
            //SqlDataAdapter da = new SqlDataAdapter("select * from T_G_DepartmentMaster where  DepartmentUId='" + Id + "'", cn);
            //DataSet ds = new DataSet();
            //da.Fill(ds, "temp");

            SqlParameter[] p = new SqlParameter[1];
            p[0] = new SqlParameter("@deptid", Id);
            DataSet ds = SqlHelper.ExecuteDataset("SH_DepartmentMaster_Details", p);
            TextBox1.Text = ds.Tables[0].Rows[0]["DepartmentName"].ToString();
            int status = Convert.ToInt32(ds.Tables[0].Rows[0]["bitsStatus"].ToString());
            if (status == 1)
            {
                chkStatus.Checked = true;
            }
            else
            {
                chkStatus.Checked = false;
            }
           // cn.Close();

        }



        protected void gvEditdept_Sorting(object sender, GridViewSortEventArgs e)
        {
            try
            {
                //SortGrid Object from ViewState
                objSort = (GridSort)ViewState["Sort"];

                //Get dataSet from ViewState
                dsDepartments = (DataSet)ViewState["DataSet"];
                DataView dvDepartments = dsDepartments.Tables[0].DefaultView;
                //Get SortExpresssion from sordGrid Object
                dvDepartments.Sort = objSort.GetSortExpression(e.SortExpression);
                gvEditdept.DataSource = dvDepartments;
                gvEditdept.DataBind();
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
        protected void gvEditdept_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{
            //    LinkButton lnkEdt = (LinkButton)e.Row.FindControl("lnkEdt");
            //    LinkButton lnkDel = (LinkButton)e.Row.FindControl("lnkDel");

            //    lnkEdt.Enabled = lnkDel.Enabled = Editable;
            // }
        }

       
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletiondeptList(string prefixText, int count, string contextKey)
        {
            DataSet ds = AttendanceDAC.HR_DepartmentBySiteFilter(prefixText);
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
