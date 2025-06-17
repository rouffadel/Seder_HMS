using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Configuration;
using AECLOGIC.HMS.BLL;
using AECLOGIC.ERP.COMMON;

namespace AECLOGIC.ERP.HMS
{
    public partial class EmpDataCard : AECLOGIC.ERP.COMMON.WebFormMaster
    {
        AttendanceDAC objEmployee = new AttendanceDAC();
        AttendanceDAC objRights = new AttendanceDAC();
        HRCommon objHrCommon = new HRCommon();
        DataSet ds = new DataSet();
        int mid = 0;
        bool viewall;
        string menuname;
        string menuid;
        protected override void OnInit(EventArgs e)
        {
            ModuleID = 1;
            // btnSave.Attributes.Add("onclick", "javascript:return ValidateSave('" + txtCategoryName.ClientID + "');");
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


        protected void Page_Load(object sender, EventArgs e)
        {

            try
            {
                string id =  Convert.ToInt32(Session["UserId"]).ToString();
            }
            catch
            {
                Response.Redirect("Home.aspx");
            }
            topmenu.MenuId = GetParentMenuId();
            topmenu.ModuleId = ModuleID;;
            topmenu.RoleID = Convert.ToInt32(Session["RoleId"].ToString());
            topmenu.SelectedMenu = Convert.ToInt32(mid.ToString());
            topmenu.DataBind();
            Session["menuname"] = menuname;
            Session["menuid"] = menuid;
            Session["MId"] = mid;
            if (!IsPostBack)
            {

                BindWorkSites();
                BindDepartments();
                EmployeBind(objHrCommon);
            }

        }
        void EmployeBind(HRCommon objHrCommon)
        {

            try
            {
                objHrCommon.PageSize = EmpListPaging.ShowRows;
                objHrCommon.CurrentPage = EmpListPaging.CurrentPage;
                objHrCommon.SiteID = Convert.ToInt32(ddlworksites.SelectedItem.Value);
                objHrCommon.DeptID = Convert.ToDouble(ddldepartments.SelectedItem.Value);
                objHrCommon.FName = txtusername.Text;
                objHrCommon.CurrentStatus = 'y';
                DataSet ds = new DataSet();
                gveditkbipl.DataSource = null;
                gveditkbipl.DataBind();
                // ds = (DataSet)objEmployee.GetEmployeesByPageForCompanyMobiles(objHrCommon);
                if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0)
                {
                    gveditkbipl.DataSource = ds;
                    gveditkbipl.DataBind();
                }
                EmpListPaging.Bind(objHrCommon.CurrentPage, objHrCommon.TotalPages, objHrCommon.NoofRecords, objHrCommon.PageSize);

            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public int GetParentMenuId()
        {
            string URL = Request.Url.Segments[Request.Url.Segments.Length - 1] + Request.Url.Query;
            int RoleId = Convert.ToInt32(Session["RoleId"].ToString());
            int ModuleId = ModuleID;;

            DataSet ds = new DataSet();

            ds = AttendanceDAC.GetAllowed(RoleId, ModuleId, URL);
            int MenuId = 0;
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                MenuId = Convert.ToInt32(ds.Tables[0].Rows[0]["Under"]);
                ViewState["Editable"] = (bool)ds.Tables[0].Rows[0]["Editable"];
                ViewState["ViewAll"] = (bool)ds.Tables[0].Rows[0]["ViewAll"];
                ViewState["ViewAll"] = (bool)ds.Tables[0].Rows[0]["ViewAll"];
                viewall = (bool)ViewState["ViewAll"];
                btnUpdateAll.Visible = (bool)ds.Tables[0].Rows[0]["Editable"];
                menuname = ds.Tables[0].Rows[0]["menuname"].ToString();
                menuid = MenuId.ToString();
                mid = Convert.ToInt32(ds.Tables[0].Rows[0]["MenuId"].ToString());
                //GVIndentStatus.Columns[7].Visible = Convert.ToBoolean(ds.Tables[0].Rows[0]["Editable"].ToString());
                //viewall = (bool)ViewState["ViewAll"];
            }
            return MenuId;
        }
        public string GetWorkSite(string WSid)
        {
            string retVal = "";
            try
            {
                DataSet ds = (DataSet)ViewState["WorkSites"];
                retVal = ds.Tables[0].Select("Site_ID='" + WSid + "'")[0]["Site_Name"].ToString();
            }
            catch { }
            return retVal;
        }
        public string GetDepartment(string DeptId)
        {
            string retVal = "";
            try
            {
                DataSet ds = (DataSet)ViewState["Departments"];
                retVal = ds.Tables[0].Select("DepartmentUId='" + DeptId + "'")[0]["DepartmentName"].ToString();
            }
            catch { }
            return retVal;
        }
        public void BindWorkSites()
        {

            try
            {
                DataSet ds = new DataSet();
                ds = AttendanceDAC.GetWorkSite(0, '1', Convert.ToInt32(Session["CompanyID"]));
                ViewState["WorkSites"] = ds;
                if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ddlworksites.DataSource = ds.Tables[0];
                    ddlworksites.DataTextField = "Site_Name";
                    ddlworksites.DataValueField = "Site_ID";
                    ddlworksites.DataBind();
                }
                ddlworksites.Items.Insert(0, new ListItem("---ALL---", "0"));

            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public void BindDepartments()
        {
            try
            {
                DataSet ds = new DataSet();
                ds = (DataSet)objRights.GetDaprtmentList();
                ViewState["Departments"] = ds;
                if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ddldepartments.DataValueField = "DepartmentUId";
                    ddldepartments.DataTextField = "DeptName";
                    //ddldepartments.DataTextField = "DepartmentName";
                    ddldepartments.DataSource = ds;
                    ddldepartments.DataBind();
                    ddldepartments.Items.Insert(0, new ListItem("---ALL---", "0"));
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        protected void EmpdataBound()
        {
            SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings["strConn"]);
            cn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cn;
            cmd.CommandType = CommandType.Text;
            //cmd.CommandText = "select * from kbipemp where Status='y' and [Type]!=1";
            cmd.CommandText = "select * from T_G_EmployeeMaster where Status='y' order by fname";
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds, "temp");
            gveditkbipl.DataSource = ds;
            gveditkbipl.DataBind();
            cn.Close();

        }
        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Edt")
            {

                int EmpID = Convert.ToInt32(e.CommandArgument);

                GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).Parent.Parent;
                CheckBox chk = (CheckBox)gveditkbipl.Rows[row.RowIndex].FindControl("chkSelect");
                if (chk.Checked == true)
                {
                    try
                    {
                        TextBox txtAmountLimit = (TextBox)gveditkbipl.Rows[row.RowIndex].FindControl("txtAmountLimit");
                        TextBox txtMobile2 = (TextBox)gveditkbipl.Rows[row.RowIndex].FindControl("txtMobile2");
                        AttendanceDAC.InsUpdateEmpSIMS(EmpID, Convert.ToInt64(txtMobile2.Text),  Convert.ToInt32(Session["UserId"]), Convert.ToDecimal(txtAmountLimit.Text));
                        AlertMsg.MsgBox(Page, "Succeesfully Updated");
                    }
                    catch (Exception ex)
                    {

                        AlertMsg.MsgBox(Page, "Sorry for the inconvinience. Try again.\nError:" + ex.Message.ToString(),AlertMsg.MessageType.Error);
                    }

                }

            }


        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            objHrCommon.SiteID = Convert.ToInt32(ddlworksites.SelectedItem.Value);
            objHrCommon.DeptID = Convert.ToDouble(ddldepartments.SelectedItem.Value);
            objHrCommon.FName = txtusername.Text;
            EmployeBind(objHrCommon);
        }
        protected void gveditkbipl_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }
        protected void gveditkbipl_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            foreach (GridViewRow gvr in gveditkbipl.Rows)
            {
                LinkButton lnkUpd = (LinkButton)gvr.Cells[9].FindControl("lnkEdit");
                lnkUpd.Enabled = Convert.ToBoolean(ViewState["Editable"]);
            }
        }
        protected void btnUpdateAll_Click(object sender, EventArgs e)
        {
            try
            {

                foreach (GridViewRow gvr in gveditkbipl.Rows)
                {
                    CheckBox chk = (CheckBox)gvr.Cells[0].FindControl("chkSelect");
                    if (chk.Checked == true)
                    {
                        Label lblEmpId = (Label)gvr.Cells[1].FindControl("lblEmpId");
                        TextBox txtAmountLimit = (TextBox)gvr.Cells[8].FindControl("txtAmountLimit");
                        TextBox txtMobile2 = (TextBox)gvr.Cells[7].FindControl("txtMobile2");
                        AttendanceDAC.InsUpdateEmpSIMS(Convert.ToInt32(lblEmpId.Text), Convert.ToInt64(txtMobile2.Text),  Convert.ToInt32(Session["UserId"]), Convert.ToDecimal(txtAmountLimit.Text));
                        AlertMsg.MsgBox(Page, "Succeesfully Updated");
                    }
                }
                AlertMsg.MsgBox(Page, "Succeesfully Updated");

            }
            catch (Exception ex)
            {

                AlertMsg.MsgBox(Page, "Sorry for the inconvinience. Try again.\nError:" + ex.Message.ToString(),AlertMsg.MessageType.Error);
            }
        }
        protected void gveditkbipl_RowEditing(object sender, GridViewEditEventArgs e)
        {

        }
    }
}