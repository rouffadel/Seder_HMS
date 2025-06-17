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
    public partial class OB_LOP : AECLOGIC.ERP.COMMON.WebFormMaster
    {
        DataSet ds = new DataSet();
        int mid = 0;
        bool viewall, Editable;
        string menuname;
        string menuid;

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

                DataSet ds = new DataSet();
                ds = AttendanceDAC.HR_GetEmployeeGratuity_LOP(objHrCommon,txtSEmpId.Text.Trim(),txtSEmpName.Text.Trim());
                if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0)
                {
                    gvEmp.DataSource = ds;
                    gvEmp.DataBind();
                }
                else
                {
                    gvEmp.DataSource = null;
                    gvEmp.DataBind();
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
            try
            {
                string id =  Convert.ToInt32(Session["UserId"]).ToString();
            }
            catch
            {
                Response.Redirect("Home.aspx");
            }


            topmenu.MenuId = GetParentMenuId();
            topmenu.ModuleId = ModuleID; ;
            topmenu.RoleID = Convert.ToInt32(Session["RoleId"].ToString());
            topmenu.SelectedMenu = Convert.ToInt32(mid.ToString());
            topmenu.DataBind();
            Session["menuname"] = menuname;
            Session["menuid"] = menuid;
            Session["MId"] = mid;
            if (!IsPostBack)
            {

                ViewState["CateId"] = "";
                tblEdit.Visible = true;
                EmployeBind(objHrCommon);
                tblNew.Visible = false;
                gvEmp.Visible = true;
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
        public int GetParentMenuId()
        {
            string URL = Request.Url.Segments[Request.Url.Segments.Length - 1] + Request.Url.Query;
            int RoleId = Convert.ToInt32(Session["RoleId"].ToString());
            int ModuleId = ModuleID; ;

            DataSet ds = new DataSet();

            ds = AttendanceDAC.GetAllowed(RoleId, ModuleId, URL);
            int MenuId = 0;
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                MenuId = Convert.ToInt32(ds.Tables[0].Rows[0]["Under"]);
                gvEmp.Columns[1].Visible = Convert.ToBoolean(ds.Tables[0].Rows[0]["Editable"].ToString());
                ViewState["Editable"] = Editable = (bool)ds.Tables[0].Rows[0]["Editable"];
                ViewState["ViewAll"] = (bool)ds.Tables[0].Rows[0]["ViewAll"];
                viewall = (bool)ViewState["ViewAll"];
                menuname = ds.Tables[0].Rows[0]["menuname"].ToString();
                menuid = MenuId.ToString();
                btnSubmit.Enabled = (bool)ds.Tables[0].Rows[0]["Editable"];
                mid = Convert.ToInt32(ds.Tables[0].Rows[0]["MenuId"].ToString());
                btnSubmit.Enabled = Editable;
            }
            return MenuId;
        }
        //public void BindGrid()
        //{
        //    ds = AttendanceDAC.GetCategoriesLists();
        //    gvEmp.DataSource = ds;
        //    gvEmp.DataBind();

        //}

        //public string GetText()
        //{

        //    if (rblCat.SelectedValue == "1")
        //    {
        //        return "Deactivate";
        //    }
        //    else
        //    {
        //        return "Activate";
        //    }
        //}

        public void BindDetails(int ID)
        {

            tblEdit.Visible = false;
            tblNew.Visible = true;
            ds = AttendanceDAC.GetGratuity_LOP_Details(ID);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                txtEmpId.Text = ds.Tables[0].Rows[0]["EmpId"].ToString();
                txtEmpName.Text = ds.Tables[0].Rows[0]["EmpName"].ToString();
                txtLOP.Text = ds.Tables[0].Rows[0]["LOP"].ToString();

            }
        }
        protected void gvWages_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int ID = Convert.ToInt32(e.CommandArgument);
             
                if (e.CommandName == "Edt")
                {
                    BindDetails(ID);
                }
                else
                {
                    //AttendanceDAC.HR_Upd_CategoryStatus(ID);
                    EmployeBind(objHrCommon);
                }
            }
            catch (Exception CatDel)
            {
                AlertMsg.MsgBox(Page, CatDel.Message.ToString(),AlertMsg.MessageType.Error);
            }

        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            EmployeBind(objHrCommon);
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                int id=0;
                if (btnSubmit.Text == "Save")
                    id = 1;
                else
                    id = 2;


                int Output = AttendanceDAC.InsUpdGratuity_LOP(0,txtEmpId.Text.Trim(), txtEmpName.Text.Trim(),Convert.ToInt32(txtLOP.Text.Trim()),id);
                EmployeBind(objHrCommon);
                Clear();
                if (Output == 1)
                {
                    AlertMsg.MsgBox(Page, "Done.! ");
                }
                else if (Output == 2)
                {
                    AlertMsg.MsgBox(Page, "Already Exists.!");

                }
                else if (Output == 3)
                    AlertMsg.MsgBox(Page, "Updated.!");

            }
            catch (Exception Cat)
            {
                AlertMsg.MsgBox(Page, Cat.Message.ToString(),AlertMsg.MessageType.Error);
            }


        }
        public void Clear()
        {
            txtEmpId.Text = "";
            txtEmpName.Text = "";
            txtLOP.Text = "";
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
        //protected void rblDesg_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    EmployeBind(objHrCommon);
        //}
        protected void gvEmp_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton lnkEdt = (LinkButton)e.Row.FindControl("lnkEdit");
                LinkButton lnkDel = (LinkButton)e.Row.FindControl("lnkDel");

                lnkEdt.Enabled = lnkDel.Enabled = Editable;
            }
        }
    }

}