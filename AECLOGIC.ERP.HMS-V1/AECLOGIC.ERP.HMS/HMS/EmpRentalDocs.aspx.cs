using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using AECLOGIC.HMS.BLL;
using System.Web.SessionState;
using AECLOGIC.ERP.COMMON;

namespace AECLOGIC.ERP.HMS
{
    public partial class EmpRentalDocs : AECLOGIC.ERP.COMMON.WebFormMaster
    {
        HRCommon objHrCommon = new HRCommon();
        static int SearchCompanyID;
        static int Siteid;
        protected override void OnInit(EventArgs e)
        {
            ModuleID = 1;
            base.OnInit(e);
            EmpRentalDocsPaging.FirstClick += new Paging.PageFirst(EmpRentalDocsPaging_FirstClick);
            EmpRentalDocsPaging.PreviousClick += new Paging.PagePrevious(EmpRentalDocsPaging_FirstClick);
            EmpRentalDocsPaging.NextClick += new Paging.PageNext(EmpRentalDocsPaging_FirstClick);
            EmpRentalDocsPaging.LastClick += new Paging.PageLast(EmpRentalDocsPaging_FirstClick);
            EmpRentalDocsPaging.ChangeClick += new Paging.PageChange(EmpRentalDocsPaging_FirstClick);
            EmpRentalDocsPaging.ShowRowsClick += new Paging.ShowRowsChange(EmpRentalDocsPaging_ShowRowsClick);
            EmpRentalDocsPaging.CurrentPage = 1;
        }
        void EmpRentalDocsPaging_ShowRowsClick(object sender, EventArgs e)
        {
            EmpRentalDocsPaging.CurrentPage = 1;
            BindPager();
        }
        void EmpRentalDocsPaging_FirstClick(object sender, EventArgs e)
        {
            BindPager();
        }
        void BindPager()
        {
            objHrCommon.PageSize = EmpRentalDocsPaging.CurrentPage;
            objHrCommon.CurrentPage = EmpRentalDocsPaging.ShowRows;
            BindGrid(objHrCommon);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            
            
            SearchCompanyID = Convert.ToInt32(Session["CompanyID"]);
            
            Ajax.Utility.RegisterTypeForAjax(typeof(AjaxDAL));
            if (!IsPostBack)
            {
                GetParentMenuId();
                BindWorksites();
               

                Ajax.Utility.RegisterTypeForAjax(typeof(AjaxDAL));
                ViewState["EmpID"] = 0; ViewState["HRDocID"] = 0;
                if (Request.QueryString.Count > 0)
                {
                    tblView.Visible = false;
                    tblEdit.Visible = true;
                    pnltblEdit.Visible = true;
                }
                else
                {

                    trEmpSearch.Visible = true;
                    tblView.Visible = true;
                    tblEdit.Visible = false;
                    pnltblEdit.Visible = false;
                }
                BindPager();
                BindEmpList();
                ddlEmp.Enabled = true;
                ViewState["Proof"] = "";
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

                gvView.Columns[9].Visible = Convert.ToBoolean(ds.Tables[0].Rows[0]["Editable"].ToString());
              
                btnSave.Enabled = (bool)ds.Tables[0].Rows[0]["Editable"];
               
            }
            return MenuId;
        }
        public void BindWorksites()
        {

            FIllObject.FillDropDown(ref ddlWs, "HR_GetWorkSite_By_RentalDocs");
           
        }
        void BindGrid(HRCommon objHrCommon)
        {

            try
            {

                objHrCommon.PageSize = EmpRentalDocsPaging.ShowRows;
                objHrCommon.CurrentPage = EmpRentalDocsPaging.CurrentPage;
                if (Convert.ToBoolean(ViewState["ViewAll"]))
                {
                    int Emp = 0;
                    if (txtEmpID.Text != "")
                    {
                        Emp = Convert.ToInt32(txtEmpID.Text);
                    }
                    int WS = Convert.ToInt32(ddlWs.SelectedValue);
                    int Dept = Convert.ToInt32(ddlDept.SelectedValue);
                    string EmpName = txtEmpName.Text;

                    DataSet ds = AttendanceDAC.HR_GetRentalDocsByPaging(objHrCommon, WS, Dept, Emp, EmpName);
                    if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        gvView.DataSource = ds;
                        gvView.DataBind();
                        trEmpSearch.Visible = true;
                        EmpRentalDocsPaging.Visible = true;
                    }
                    else
                    {
                        EmpRentalDocsPaging.Visible = false;
                        gvView.EmptyDataText = "No Records Found";
                        gvView.DataSource = null;
                        gvView.DataBind();
                    }
                }
                else
                {
                    DataSet ds = AttendanceDAC.HR_GetRentalDocsByUserIDByPaging(objHrCommon,  Convert.ToInt32(Session["UserId"]));
                    gvView.DataSource = ds;
                    gvView.DataBind();
                    gvView.Columns[9].Visible = false;
                    trEmpSearch.Visible = false;

                }
                EmpRentalDocsPaging.Bind(objHrCommon.CurrentPage, objHrCommon.TotalPages, objHrCommon.NoofRecords, objHrCommon.PageSize);

            }
            catch (Exception e)
            {
                throw e;
            }
        }
       

        protected void Worksidechangewithdep(object sender, EventArgs e)
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@Search", txtSearchWorksite.Text);
            FIllObject.FillDropDown(ref ddlWs, "HR_GetWorkSiteGoogleSear_By_RentalDocs", param);
            ListItem itmSelected = ddlWs.Items.FindByText(txtSearchWorksite.Text);
            if (itmSelected != null)
            {
                ddlWs.SelectedItem.Selected = false;
                itmSelected.Selected = true;
                //FillProjects();
            }
            ddlWs_SelectedIndexChanged(sender, e);
        }
        protected void GetDepartment(object sender, EventArgs e)
        {
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@Search", txtSearchdept.Text);
            param[1] = new SqlParameter("@CompanyID", Convert.ToInt32(Session["CompanyID"]));
            param[2] = new SqlParameter("@SiteID", ddlWs.SelectedItem.Value);
            FIllObject.FillDropDown(ref ddlDept, "HMS_googlesearch_GetDepartmentBySite", param);
            ListItem itmSelected = ddlDept.Items.FindByText(txtSearchdept.Text);
            if (itmSelected != null)
            {
                ddlDept.SelectedItem.Selected = false;
                itmSelected.Selected = true;
            }
        }
        public void BindEmpList()
        {
              
            string EmpName = string.Empty;
            DataSet ds = AttendanceDAC.HR_GetEmployeeBySearch(EmpName);
            ddlEmp.DataSource = ds.Tables[1];
            ddlEmp.DataTextField = "name";
            ddlEmp.DataValueField = "EmpID";
            ddlEmp.DataBind();
            ddlEmp.Items.Insert(0, new ListItem("---SELECT---", "0", true));
        }
        protected void gvView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int ID = Convert.ToInt32(e.CommandArgument);
            ViewState["HRDocID"] = ID;
            tblView.Visible = false;
            tblEdit.Visible = true;
            pnltblEdit.Visible = true;
            if (e.CommandName == "Edt")
            {
                ddlEmp.Enabled = false;
                DataSet ds = AttendanceDAC.HR_GetRentalDocsByID(ID);
                ViewState["EmpID"] = ds.Tables[0].Rows[0]["EmpID"].ToString();
                ddlEmp.SelectedValue = ds.Tables[0].Rows[0]["EmpID"].ToString();
                txtAmount.Text = ds.Tables[0].Rows[0]["Amount"].ToString();
                txtFrom.Text = ds.Tables[0].Rows[0]["FromDate1"].ToString();
                txtUpto.Text = ds.Tables[0].Rows[0]["ToDate1"].ToString();
                ViewState["Proof"] = ds.Tables[0].Rows[0]["Proof"].ToString();
            }
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ddlEmp.SelectedIndex = 0;
            txtAmount.Text = txtFrom.Text = txtUpto.Text = "";
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                int sts;
                int EmpID = Convert.ToInt32(ViewState["EmpID"]);
                int HRDocID = Convert.ToInt32(ViewState["HRDocID"]);

                if (EmpID == 0)
                {
                    EmpID = Convert.ToInt32(ddlEmp.SelectedValue);
                }
                string filename = "", ext = "", path = "";
                filename = FileProof.PostedFile.FileName;
                if (filename != "")
                {
                    ext = filename.Split('.')[filename.Split('.').Length - 1];
                }
                else
                {
                    if (ViewState["Proof"].ToString() != "")
                    {
                        ext = ViewState["Proof"].ToString();
                    }
                    else
                    {
                        ext = "";
                    }
                }
                try
                {
                    DateTime From = CODEUtility.ConvertToDate(txtFrom.Text, DateFormat.DayMonthYear);

                    DateTime? Upto = null;
                    try
                    {

                        if (txtUpto.Text != "")
                        {
                            Upto = CODEUtility.ConvertToDate(txtUpto.Text, DateFormat.DayMonthYear);
                        }
                        double Amount = 0.0;
                        if (txtAmount.Text != "")
                            Amount = Convert.ToDouble(txtAmount.Text);
                        sts = AttendanceDAC.HR_InsUpRentalDocs(HRDocID, EmpID, Amount, ext, From, Upto,  Convert.ToInt32(Session["UserId"]));
                    }
                    catch (Exception)
                    {
                        AlertMsg.MsgBox(Page, "Please select proper TO date.!");
                        return;

                    }
                }
                catch (Exception)
                {
                    AlertMsg.MsgBox(Page, "Please select proper from date.!");
                    return;
                }
                if (filename != "")
                {
                    if (EmpID != 0)
                    {
                        path = Server.MapPath(".\\RentalDocs\\" + EmpID + "." + ext);
                        try
                        {
                            FileProof.PostedFile.SaveAs(path);
                        }
                        catch { throw new Exception("FileProof.PostedFile.SaveAs(path)"); }
                    }
                }

                if (sts == 2)
                {
                    AlertMsg.MsgBox(Page, "This employee ID has been already listed for same from-upto date!");
                    return;
                }
                AlertMsg.MsgBox(Page, "Done");
                BindPager();
                tblView.Visible = true;
                tblEdit.Visible = false;
                pnltblEdit.Visible = false;
            }
            catch (Exception EmpRental)
            {
                AlertMsg.MsgBox(Page, EmpRental.Message.ToString(),AlertMsg.MessageType.Error);
            }

        }
        public string ProofView(string EmpID, string ext)
        {
            return "javascript:return window.open('./RentalDocs/" + EmpID + "." + ext + "', '_blank')";
        }
        public bool Visible(string ext)
        {

            if (ext != "")
            {
                return true;
            }
            else
                return false;
        }
        protected void gvView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblDocID = (Label)e.Row.FindControl("lblDocID");
                CheckBox chkApprove = (CheckBox)e.Row.FindControl("chkApprove");
                string Status = "false";
                if (chkApprove.Checked)
                {
                    Status = "true";
                }
                chkApprove.Attributes.Add("onclick", "javascript:return UpdateApproval('" + lblDocID.Text + "')");
                // chkApprove.Attributes.Add("onclick", "javascript:return UpdateApproval('" + chkApprove.ClientID + "','" + lblDocID.ClientID + "',,'" +  Convert.ToInt32(Session["UserId"]).ToString() + "')");

            }

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            EmpRentalDocsPaging.CurrentPage = 1;
            int DeptNo = Convert.ToInt32(ddlDept.SelectedValue);
            int SiteID = Convert.ToInt32(ddlWs.SelectedValue);
            int EmpID = 0; string EmpName = "";
            if (txtEmpID.Text != "")
            {
                try
                {
                    EmpID = Convert.ToInt32(txtEmpID.Text);
                }
                catch
                {
                    AlertMsg.MsgBox(Page, "Check The Data you have given!");
                }
            }
            try
            {
                EmpName = txtEmpName.Text;
            }
            catch
            {
                AlertMsg.MsgBox(Page, "Check The Data you have given!");
            }
            int Emp = 0;
            if (txtEmpID.Text != "")
            {
                Emp = Convert.ToInt32(txtEmpID.Text);
            }
            BindPager();

          


        }
        //Added by Rijwan:22-03-2016
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute]
        public static string[] GetCompletionList(string prefixText, int count, string contextKey)
        {
            DataSet ds = AttendanceDAC.GetGGoogleSear_By_RentalDocs(prefixText);
            return ConvertStingArray(ds);
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute]
        public static string[] GetCompletionListdept(string prefixText, int count, string contextKey)
        {
            DataSet ds = AttendanceDAC.GetDepartmentGoogleSerc(prefixText, SearchCompanyID, Siteid);
            return ConvertStingArray(ds);
        }

        public static string[] ConvertStingArray(DataSet ds)
        {
            string[] rtval = Array.ConvertAll(ds.Tables[0].Select(), new Converter<DataRow, string>(DataRowTotable));
            return rtval;
        }
        public static string DataRowTotable(DataRow dr)
        {
            return dr["Name"].ToString();
        }
        protected void ddlWs_SelectedIndexChanged(object sender, EventArgs e)
        {
            EmpRentalDocsPaging.CurrentPage = 1;
            BindDeparmetBySite(Convert.ToInt32(ddlWs.SelectedValue));
            Siteid = Convert.ToInt32(ddlWs.SelectedValue);

        }

        public void BindDeparmetBySite(int Site)
        {
            DataSet ds = AttendanceDAC.BindDeparmetBySite(Site, Convert.ToInt32(Session["CompanyID"]));
            ddlDept.DataSource = ds;
            ddlDept.DataTextField = "DeptName";
            ddlDept.DataValueField = "DepartmentUId";
            ddlDept.DataBind();
            ddlDept.Items.Insert(0, new ListItem("---ALL---", "0", true));
        }
        protected void ddlDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            EmpRentalDocsPaging.CurrentPage = 1;

        }
    }
}
