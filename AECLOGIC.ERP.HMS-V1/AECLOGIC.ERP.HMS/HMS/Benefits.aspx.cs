using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using AECLOGIC.HMS.BLL;
using System.Data.SqlClient;
using AECLOGIC.ERP.COMMON;
using Aeclogic.Common.DAL;
using System.Collections.Generic;
using AECLOGIC.ERP.HMS;
using System.Linq;

namespace AECLOGIC.ERP.HMSV1
{
    public partial class BenefitsV1 : AECLOGIC.ERP.COMMON.WebFormMaster
    {
        int Id = 1; static int Keyid = 0;
        int mid = 0;
        bool viewall;
        string menuname;
        string menuid;
        static int SearchCompanyID;
        static int WorkSiteID;
        static int DeptSearch;
        String MyString;
        string extension;
        bool Editable;

        int CompanyID = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["CompanyID"].ToString());
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

            EmpReimbursementTransferdPaging.FirstClick += new Paging.PageFirst(EmpReimbursementTransferdPaging_FirstClick);
            EmpReimbursementTransferdPaging.PreviousClick += new Paging.PagePrevious(EmpReimbursementTransferdPaging_FirstClick);
            EmpReimbursementTransferdPaging.NextClick += new Paging.PageNext(EmpReimbursementTransferdPaging_FirstClick);
            EmpReimbursementTransferdPaging.LastClick += new Paging.PageLast(EmpReimbursementTransferdPaging_FirstClick);
            EmpReimbursementTransferdPaging.ChangeClick += new Paging.PageChange(EmpReimbursementTransferdPaging_FirstClick);
            EmpReimbursementTransferdPaging.ShowRowsClick += new Paging.ShowRowsChange(EmpReimbursementTransferdPaging_ShowRowsClick);
            EmpReimbursementTransferdPaging.CurrentPage = 1;
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
            BindGrid(objHrCommon);
        }
       
        protected void Page_Load(object sender, EventArgs e)
        {
            Ajax.Utility.RegisterTypeForAjax(typeof(AjaxDAL));
            DataTable dtReimburseList = new DataTable();
            SearchCompanyID = Convert.ToInt32(Session["CompanyID"]);
          

            if (!IsPostBack)
            {
                bindyear();
                BindItem();

            
                  // T_HMS_EmpBenefit
            //status=1-Pending,2-Approved,3-rejected
            if (Request.QueryString["key"] != null && Request.QueryString["key"] != string.Empty)
                {
                    Keyid = Convert.ToInt32(Request.QueryString["key"].ToString());
                }
                else
                {
                    tblAdd.Visible = true;
                    tblView.Visible = false;
                    tbledt.Visible = false;
                    Keyid = 0;
                }


                if (Keyid == 1)
                {
                    gvBenefit.Columns[0].Visible = false;
                    gvBenefit.Columns[6].Visible = true;
                    gvBenefit.Columns[7].Visible = true;
                    gvBenefit.Columns[8].Visible = true;
                    gvBenefit.Columns[9].Visible = true;
                    gvBenefit.Columns[10].Visible = true;
                    BindPager();
                    tblAdd.Visible = false;
                    tblView.Visible = true;
                    tblacposted.Visible = false;
                    btnTransferAcc.Visible = false;
                }
                else  if (Keyid == 2)
                {
                    //6789
                    gvBenefit.Columns[0].Visible = true;
                    gvBenefit.Columns[6].Visible = true;
                    gvBenefit.Columns[7].Visible = true;
                    gvBenefit.Columns[8].Visible = true;
                    gvBenefit.Columns[9].Visible = false;
                    gvBenefit.Columns[10].Visible = false;
                    gvBenefit.Columns[11].Visible = false;
                    gvBenefit.Columns[12].Visible = false;
                  //  gvBenefit.Columns[13].Visible = false;

                    gvBenefit.Columns[7].ControlStyle.ForeColor = System.Drawing.Color.Green;

                    BindPager();

                    tblAdd.Visible = false;
                    tblView.Visible = true;
                    tblacposted.Visible = false;
                }
                else if (Keyid == 3)
                {
                    gvBenefit.Columns[0].Visible = false;
                    gvBenefit.Columns[6].Visible = true;
                    gvBenefit.Columns[7].Visible = true;
                    gvBenefit.Columns[8].Visible = true;
                    gvBenefit.Columns[9].Visible = false;
                    gvBenefit.Columns[10].Visible = false;
                    gvBenefit.Columns[11].Visible = false;
                    gvBenefit.Columns[12].Visible = false;
                    gvBenefit.Columns[7].ControlStyle.ForeColor = System.Drawing.Color.Red;

                    BindPager();

                    tblAdd.Visible = false;
                    tblView.Visible = true;
                    tblacposted.Visible = false;
                    btnTransferAcc.Visible = false;
                }
                else if (Keyid == 4)
                {
                    tblacposted.Visible = true;
                    tblAdd.Visible = false;
                    tblView.Visible = false;
                    btnTransferAcc.Visible = false;
                    BindPagerTransAmt();
                }
            }
               

               
        }
        void EmpReimbursementTransferdPaging_ShowRowsClick(object sender, EventArgs e)
        {
            EmpReimbursementTransferdPaging.CurrentPage = 1;
            BindPagerTransAmt();
        }
        void EmpReimbursementTransferdPaging_FirstClick(object sender, EventArgs e)
        {
             EmpReimbursementTransferdPaging.CurrentPage = 1;
            BindPagerTransAmt();
        }

        private void BindPagerTransAmt()
        {
            int EmpID=0;
            if (TextBox1.Text == "" || TextBox1.Text == null)
                {
                    HiddenField1.Value = "";
                }
                else if (TextBox1.Text != "" || TextBox1.Text != null)
                {
                    EmpID = Convert.ToInt32(HiddenField1.Value == "" ? "0" : HiddenField1.Value);
                }

                else
                {
                    EmpID = 0;
                }

            objHrCommon.CurrentPage  = EmpReimbursementTransferdPaging.CurrentPage;
            objHrCommon.PageSize = EmpReimbursementTransferdPaging.ShowRows;
            SqlParameter[] sqlParams = new SqlParameter[5];
            sqlParams[0] = new SqlParameter("@CurrentPage", objHrCommon.CurrentPage);
            sqlParams[1] = new SqlParameter("@PageSize", objHrCommon.PageSize);
            sqlParams[2] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
            sqlParams[2].Direction = ParameterDirection.ReturnValue;
            sqlParams[3] = new SqlParameter("@NoofRecords", System.Data.SqlDbType.Int);
            sqlParams[3].Direction = ParameterDirection.Output;
            sqlParams[4] = new SqlParameter("@Empid", EmpID);
            DataSet ds = SQLDBUtil.ExecuteDataset("sh_BenefitAcposted", sqlParams);
            objHrCommon.NoofRecords = (int)sqlParams[3].Value;
            objHrCommon.TotalPages = (int)sqlParams[2].Value;

            if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0)
            {
                gvTransfered.DataSource = ds;

                EmpReimbursementTransferdPaging.Bind(objHrCommon.CurrentPage, objHrCommon.TotalPages, objHrCommon.NoofRecords, objHrCommon.PageSize);

            }
            else
            {
                gvTransfered.EmptyDataText = "No Records Found";
                EmpReimbursementTransferdPaging.Visible = false;

            }
            gvTransfered.DataBind();



        }

        public void BindGrid(HRCommon objHrCommon)
        {
            // T_HMS_EmpBenefit
            //status=1-Pending,2-Approved,3-rejected
            try
            {
                objHrCommon.PageSize = EmpListPaging.ShowRows;
                objHrCommon.CurrentPage = EmpListPaging.CurrentPage;
                int EmpID = 0;

                if (txtEmpName.Text == "" || txtEmpName.Text == null)
                {
                    txtEmpNameHidden.Value = "";
                }
                else if (txtEmpName.Text != "" || txtEmpName.Text != null)
                {
                    EmpID = Convert.ToInt32(txtEmpNameHidden.Value == "" ? "0" : txtEmpNameHidden.Value);
                }

                else
                {
                    EmpID = 0;
                }
                SqlParameter[] objParam = new SqlParameter[7];
                objParam[0] = new SqlParameter("@fCase", 4);
                if (Keyid == 1)
                {
                    objParam[1] = new SqlParameter("@status", 1);
                }
                else if (Keyid == 2)
                    objParam[1] = new SqlParameter("@status", 2);
                else if (Keyid == 3)
                    objParam[1] = new SqlParameter("@status", 3);
                objParam[2] = new SqlParameter("@Empid", EmpID);

                objParam[3] = new SqlParameter("@CurrPage", objHrCommon.CurrentPage);
                objParam[4] = new SqlParameter("@PageSize", objHrCommon.PageSize);
                objParam[5] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
                objParam[5].Direction = ParameterDirection.ReturnValue;
                objParam[6] = new SqlParameter("@NrRecords", System.Data.SqlDbType.Int);
                objParam[6].Direction = ParameterDirection.Output;



                DataSet ds = SQLDBUtil.ExecuteDataset("SP_T_HMS_EmpBenefit_Insert_Update_Delete_VIEW_Paging_Select", objParam);
                objHrCommon.NoofRecords = (int)objParam[6].Value;
                objHrCommon.TotalPages = (int)objParam[5].Value;
                if (ds.Tables[0].Rows.Count > 0)
                {
                    gvBenefit.DataSource = ds;
                    EmpListPaging.Bind(objHrCommon.CurrentPage, objHrCommon.TotalPages, objHrCommon.NoofRecords, objHrCommon.PageSize);
                    EmpListPaging.Visible = true;
                    gvBenefit.DataBind();
                    string url = "Benefits.aspx?key=1";
                    int UserId = Convert.ToInt32(Session["UserId"]);
                    int RoleId = Convert.ToInt32(Session["RoleId"].ToString());
                    int ModuleId = 1;
                    SqlParameter[] parm1 = new SqlParameter[3];
                    parm1[0] = new SqlParameter("@ModuleId", ModuleId);
                    parm1[1] = new SqlParameter("@RoleId", RoleId);
                    parm1[2] = new SqlParameter("@URL", url);
                    DataSet dsCp = SqlHelper.ExecuteDataset("CP_GetPageAccess", parm1);
                    if (dsCp != null && dsCp.Tables.Count > 0 && dsCp.Tables[0].Rows.Count > 0)
                    {
                        if (dsCp.Tables[0].Rows[0]["Editable"].ToString() == "True")
                            gvBenefit.Columns[9].Visible = true;
                        else
                            gvBenefit.Columns[9].Visible = false;
                    }

                }
                else
                {
                    gvBenefit.DataSource = null;
                    EmpListPaging.Visible = false;
                    gvBenefit.DataBind();
                }
                
            }
            catch { }

        }
        public string DocNavigateUrlnew(string ProofID, string Lid)
        {
            string ReturnVal = "";
            string[] str;
            string ss = "";
            if (ProofID != string.Empty)
            {
                if (ProofID.Contains('.'))
                {
                    str = ProofID.Split('.');
                    ss = Lid + '.' + str[1];
                }
            }
            if (ss != ProofID)
                ReturnVal = "~/hms/Benifits/" + ss;
            else
                ReturnVal = ProofID;
            return ReturnVal;
        }
        public bool Visble(string Ext)
        {
            if (Ext != "")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionList_EmpName(string prefixText, int count, string contextKey)
        {
            DataSet ds = AttendanceDAC.GetSearchEmpName(prefixText);
            DataTable dt = ds.Tables[0];
            List<string> items = new List<string>(count);
            var rtval = new Dictionary<string, string>();
            foreach (DataRow row in dt.Rows)
            {
                string str = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(row["NAME"].ToString(), row["ID"].ToString());
                items.Add(str);
            }

            return items.ToArray(); 

        }

        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]

        public static string[] GetCompletionList(string prefixText)
        {

            int siteid = 0, Deptid = 0;
            return ConvertStingArray1(AttendanceDAC.GetEmployees_By_WS_Dept(prefixText, siteid, Deptid));
        }

        public static string[] ConvertStingArray1(DataSet ds)
        {
            String[] rtval = Array.ConvertAll(ds.Tables[0].Select(), new Converter<DataRow, string>(DataRowToDouble));
            return rtval;
        }

        protected void ddlWorksite_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetEmpBySiteDept();
        }
        public void GetEmpBySiteDept()
        {
            int WorkSite,Dept;
            if (ddlWorksite.SelectedValue != "")
            {
                WorkSite = Convert.ToInt32(ddlWorksite.SelectedValue);
            }
            else
                WorkSite = 0;
            if (ddlDepartment.SelectedValue!= "")
            {
                Dept = Convert.ToInt32(ddlDepartment.SelectedValue);
            }
             else
                Dept = 0;

            ViewState["GSWork"] = WorkSite;
            WorkSiteID = Convert.ToInt32(ViewState["GSWork"]);
            ViewState["GSdept"] = Dept;
            DeptSearch = Convert.ToInt32(ViewState["GSdept"]);

            
            DataSet ds = AttendanceDAC.HR_EmpRepaySearch(WorkSite, Dept, Convert.ToInt32(Session["CompanyID"]));
            ddlEmp.DataSource = ds;
            ddlEmp.DataTextField = "name";
            ddlEmp.DataValueField = "EmpID";
            ddlEmp.DataBind();
            ddlEmp.Items.Insert(0, new ListItem("---SELECT---", "0", true));
        }
        protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetEmpBySiteDept();
        }
        protected void GetDept(object sender, EventArgs e)
        {
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@Search", txtdept.Text);
            param[1] = new SqlParameter("@DeptID", 0);
            param[2] = new SqlParameter("@CompanyID", 1);

            FIllObject.FillDropDown(ref ddlDepartment, "G_GET_DesignationbyFilter", param);
            ListItem itmSelected = ddlDepartment.Items.FindByText(txtdept.Text);
            if (itmSelected != null)
            {
                ddlDepartment.SelectedItem.Selected = false;
                itmSelected.Selected = true;
            }
            ddlDepartment_SelectedIndexChanged(sender, e);
            //txtFilterEmp.Focus();
        }
        protected void GetHeadEmp(object sender, EventArgs e)
        {
            int WSID = 0, Deptid = 0;
            if (ddlWorksite.SelectedValue == "")
            {
                WSID = 0;
            }
            if (ddlDepartment.SelectedValue == "")
            {
                Deptid = 0;
            }


            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@Search", txtSearchEmp.Text);
            param[1] = new SqlParameter("@CompanyID", Convert.ToInt32(Session["CompanyID"]));
            param[2] = new SqlParameter("@WorkSite", WSID);
            param[3] = new SqlParameter("@Dept", Deptid);

            FIllObject.FillDropDown(ref ddlEmp, "HR_EmpRepaySearch_EmpPenaltiesGS", param);
            ListItem itmSelected = ddlEmp.Items.FindByText(txtSearchEmp.Text);
            if (itmSelected != null)
            {
                ddlEmp.SelectedItem.Selected = false;
                itmSelected.Selected = true;
            }

        }

        private void GetWorkSites()
        {
            
            AttendanceDAC ADAC = new AttendanceDAC();
            DataSet ds = AttendanceDAC.GetWorkSite(0, '1', Convert.ToInt32(Session["CompanyID"]));
            ddlWorksite.DataSource = ds.Tables[0];
            ddlWorksite.DataTextField = "Site_Name";
            ddlWorksite.DataValueField = "Site_ID";
            ddlWorksite.DataBind();
            ddlWorksite.Items.Insert(0, new ListItem("---ALL---", "0", true));
        }
        protected void GetWork(object sender, EventArgs e)
        {
            int Wsid = 0; int StatusWS = 1;
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@Search", txtSearchWorksite.Text);
            param[1] = new SqlParameter("@CompanyID", Convert.ToInt32(Session["CompanyID"]));
            param[2] = new SqlParameter("@WSID", Wsid);
            param[3] = new SqlParameter("@WSStatus", StatusWS);
            FIllObject.FillDropDown(ref ddlWorksite, "HR_GetWorkSite_EmpPenaltiesGS", param);
            ListItem itmSelected = ddlWorksite.Items.FindByText(txtSearchWorksite.Text);
            if (itmSelected != null)
            {
                ddlWorksite.SelectedItem.Selected = false;
                itmSelected.Selected = true;
            }
            ddlWorksite_SelectedIndexChanged(sender, e);
        }
        protected void txtSearch_Click(object sender, EventArgs e)
        {
            if (txtFilter.Text == "")
            {
                
                string EmpName = string.Empty;
                int WorkSite = Convert.ToInt32(ddlWorksite.SelectedValue);
                int Dept = Convert.ToInt32(ddlDepartment.SelectedValue);
                DataSet ds = AttendanceDAC.HR_SerchEmp_Reimburse(EmpName, Convert.ToInt32(Session["CompanyID"]));
                ddlEmp.DataSource = ds.Tables[1];
                ddlEmp.DataTextField = "name";
                ddlEmp.DataValueField = "EmpID";
                ddlEmp.DataBind();
                ddlEmp.Items.Insert(0, new ListItem("---SELECT---", "0", true));
            }
            else
            {
                
                string EmpName = txtFilter.Text;
                int WorkSite = Convert.ToInt32(ddlWorksite.SelectedValue);
                int Dept = Convert.ToInt32(ddlDepartment.SelectedValue);
                DataSet ds = AttendanceDAC.HR_SerchEmp_Reimburse(EmpName, Convert.ToInt32(Session["CompanyID"]));
                ddlEmp.DataSource = ds.Tables[1];
                ddlEmp.DataTextField = "name";
                ddlEmp.DataValueField = "EmpID";
                ddlEmp.DataBind();
                ddlEmp.Items.Insert(0, new ListItem("---SELECT---", "0", true));
            }
        }
        public void BindItem()
        {
            
            DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetOtherBenefit");
            FIllObject.FillDropDown(ref ddlItem, "HR_GetOtherBenefit");
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            // T_HMS_EmpBenefit
            //status=1-Pending,2-Approved,3-rejected

            if (txtAmount.Text != "" && txtAmount.Text != null && ddlItem.SelectedIndex > 0 && txtRemarks.Text != "")
              //  && Convert.ToInt32(ddlEmp.SelectedValue) != 0
            {
                try
                {
                    string extension = string.Empty;
                    if (fuUploadProof.HasFile)
                    {

                        string Filename = fuUploadProof.PostedFile.FileName.ToLower();
                        extension = System.IO.Path.GetExtension(fuUploadProof.PostedFile.FileName).ToLower();

                    }
                    else
                    {
                        AlertMsg.MsgBox(Page, "Please upload Proof.!", AlertMsg.MessageType.Warning);
                        return;
                    }
                    int EMPID = Convert.ToInt32(ddlEmp.SelectedValue);
                    int Bid = 0;
                    SqlParameter[] objParam = new SqlParameter[10];
                    objParam[0] = new SqlParameter("@Empid", EMPID);
                    objParam[1] = new SqlParameter("@BenfitAmt", txtAmount.Text);
                    objParam[2] = new SqlParameter("@month", ddlmonth.SelectedValue);
                    objParam[3] = new SqlParameter("@year", ddlyear.SelectedValue);
                    objParam[4] = new SqlParameter("@status", 1);
                    objParam[5] = new SqlParameter("@CreatedBy",  Convert.ToInt32(Session["UserId"]));
                    objParam[6] = new SqlParameter("@fCase", 1);
                    objParam[7] = new SqlParameter("@Remarks",txtRemarks.Text);
                    objParam[8] = new SqlParameter("@Proof", extension);
                    objParam[9] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
                    objParam[9].Direction = ParameterDirection.ReturnValue;
                    int OutPut = SQLDBUtil.ExecuteNonQuery("SP_T_HMS_EmpBenefit_Insert_Update_Delete_VIEW_Paging_Select", objParam);
                    Bid = Convert.ToInt32(objParam[9].Value);
                    if (OutPut == 1)
                    {

                        if (Bid > 0)
                        {
                            if (fuUploadProof.HasFile)
                            {
                                string Filename = fuUploadProof.PostedFile.FileName.ToLower();
                                extension = System.IO.Path.GetExtension(fuUploadProof.PostedFile.FileName).ToLower();
                                string storePath = Server.MapPath("~") + "/" + "HMS/Benifits/" + Convert.ToInt32(Bid);
                                fuUploadProof.PostedFile.SaveAs(storePath + extension);
                            }
                        }
                    }
                    AlertMsg.MsgBox(Page, "Saved");
                    Clear();
                }
                catch
                {

                }

            }
            else
            {
                if (ddlItem.SelectedIndex == 0)
                {
                    AlertMsg.MsgBox(Page, "Select Reimburse Item");
                    ddlItem.Focus();
                    return;
                }
                else if (txtAmount.Text == "")
                {
                    AlertMsg.MsgBox(Page, "Enter Amount");
                    txtAmount.Focus();
                    return;
                }
                else if (Convert.ToInt32(ddlEmp.SelectedValue) == 0)
                {
                    AlertMsg.MsgBox(Page, "Select Employee");
                    txtSearchEmp.Focus();
                    return;
                }
                else if (txtRemarks.Text == "")
                {
                    AlertMsg.MsgBox(Page, "Please Enter Remarks");
                    txtRemarks.Focus();
                    return;
                }
            }
        }

        public void Clear()
        {
            ddlWorksite.Items.Clear();
            txtSearchWorksite.Text = string.Empty;

            ddlDepartment.Items.Clear();
            txtdept.Text = string.Empty;
            txtRemarks.Text = string.Empty;
            txtAmount.Text = string.Empty;
            ddlEmp.Items.Clear();
            txtSearchEmp.Text = string.Empty;
        }
        void bindyear()
        {
            FIllObject.FillDropDown(ref ddlyear, "HMS_YearWise");
            DataSet ds = AttendanceDAC.GetCalenderYear();
            if (ds.Tables[0].Rows[0]["PreviousMonth"].ToString() == "0")
            {
                ddlmonth.SelectedValue = "12";

                int CurrentYear = Convert.ToInt32(ds.Tables[0].Rows[0]["CurrentYear"]);
                int PreviousYear = CurrentYear - 1;
                ddlyear.Items.FindByValue(PreviousYear.ToString()).Selected = true;

            }
            //if we are in same year and same month
            else
            {
                ddlmonth.SelectedValue = ds.Tables[0].Rows[0]["CurrentMonth"].ToString();
                if (ddlyear.Items.FindByValue(ds.Tables[0].Rows[0]["CurrentYear"].ToString()) != null)
                {
                    ddlyear.Items.FindByValue(ds.Tables[0].Rows[0]["CurrentYear"].ToString()).Selected = true;
                }
                else
                {
                    ddlyear.SelectedIndex = 0;
                    //ddlYear.Items.Count - 1
                }
            }
        }

        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionList(string prefixText, int count, string contextKey)
        {
            DataSet ds = AttendanceDAC.GetWorkSite_EmpPenalties(prefixText, SearchCompanyID);
            return ConvertStingArray(ds);// txtItems.ToArray();
        }

        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionListEmp(string prefixText, int count, string contextKey)
        {
            DataSet ds = AttendanceDAC.HR_EmpRepaySearch_EmpPenalties(prefixText, WorkSiteID, DeptSearch, SearchCompanyID);
            return ConvertStingArray(ds);// txtItems.ToArray();
        }

        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionListDep(string prefixText, int count, string contextKey)
        {
            DataSet ds = AttendanceDAC.GetSearchDesiginationFilter(prefixText, SearchCompanyID, 0);
            return ConvertStingArray(ds);// txtItems.ToArray();
        }
        public static string[] ConvertStingArray(DataSet ds)
        {
            string[] rtval = Array.ConvertAll(ds.Tables[0].Select(), new Converter<DataRow, string>(DataRowToDouble));
            return rtval;
        }
        public static string DataRowToDouble(DataRow dr)
        {
            return dr["Name"].ToString();
        }
        protected void gvBenefit_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                // T_HMS_EmpBenefit
                //status=1-Pending,2-Approved,3-rejected
                int Bid = Convert.ToInt32(e.CommandArgument);
                ViewState["Bid"] = Bid;

                if (e.CommandName == "Approve")
                {
                    SqlParameter[] objParam = new SqlParameter[3];
                    objParam[0] = new SqlParameter("@fCase", 5);
                    objParam[1] = new SqlParameter("@status", 2);
                    objParam[2] = new SqlParameter("@Bid", Bid);
                    SQLDBUtil.ExecuteNonQuery("SP_T_HMS_EmpBenefit_Insert_Update_Delete_VIEW_Paging_Select", objParam);
                    AlertMsg.MsgBox(Page, "Approved");
                    BindPager();
                    tbledt.Visible = false;
                }
                else if (e.CommandName == "Reject")
                {
                    SqlParameter[] objParam = new SqlParameter[3];
                    objParam[0] = new SqlParameter("@fCase", 5);
                    objParam[1] = new SqlParameter("@status", 3);
                    objParam[2] = new SqlParameter("@Bid", Bid);
                    SQLDBUtil.ExecuteNonQuery("SP_T_HMS_EmpBenefit_Insert_Update_Delete_VIEW_Paging_Select", objParam);
                    AlertMsg.MsgBox(Page, "Rejected");
                    BindPager();
                    tbledt.Visible = false;
                }
                else if (e.CommandName == "Edt")
                {
                   
                    SqlParameter[] objParam = new SqlParameter[2];
                    objParam[0] = new SqlParameter("@fCase", 6);
                    objParam[1] = new SqlParameter("@Bid", Bid);
                    DataSet ds = SQLDBUtil.ExecuteDataset("SP_T_HMS_EmpBenefit_Insert_Update_Delete_VIEW_Paging_Select", objParam);
                    if(ds.Tables[0].Rows.Count>0)
                    {
                        FIllObject.FillDropDown(ref ddlEdtYear, "HMS_YearWise");
                        ddlEdtMonth.SelectedValue = ds.Tables[0].Rows[0]["month"].ToString();
                        ddlEdtYear.SelectedValue = ds.Tables[0].Rows[0]["year"].ToString();
                        txtEdtAmt.Text = ds.Tables[0].Rows[0]["BenfitAmt"].ToString();
                        tbledt.Visible = true;
                    }
                }
                else if (e.CommandName == "Del")
                {
                    SqlParameter[] objParam = new SqlParameter[2];
                    objParam[0] = new SqlParameter("@fCase", 3);
                    objParam[1] = new SqlParameter("@Bid", Bid);
                    SQLDBUtil.ExecuteNonQuery("SP_T_HMS_EmpBenefit_Insert_Update_Delete_VIEW_Paging_Select", objParam);
                    AlertMsg.MsgBox(Page, "Deleted");
                    BindPager();
                    tbledt.Visible = false;
                }


            }
            catch { }
        }

        protected void gvBenefit_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.Header)
                {
                    CheckBox chkMail = (CheckBox)e.Row.FindControl("chkSelectAll");
                    chkMail.Attributes.Add("onclick", String.Format("javascript:SelectAll(this,'{0}','chkToTransfer');", gvBenefit.ClientID));
                }
            }
            catch
            {
               
            }
        }

        protected void btnsearch_Click(object sender, EventArgs e)
        {
            EmpListPaging.CurrentPage = 1;
            BindPager();
        }

        protected void btnupdate_Click(object sender, EventArgs e)
        {
            SqlParameter[] objParam = new SqlParameter[6];
            objParam[0] = new SqlParameter("@fCase", 2);
            objParam[1] = new SqlParameter("@Bid", Convert.ToInt32(ViewState["Bid"]));
            objParam[2] = new SqlParameter("@BenfitAmt", txtEdtAmt.Text);
            objParam[3] = new SqlParameter("@month", ddlEdtMonth.SelectedValue);
            objParam[4] = new SqlParameter("@year", ddlyear.SelectedValue);
            objParam[5] = new SqlParameter("@Modifiedby",  Convert.ToInt32(Session["UserId"]));
            SQLDBUtil.ExecuteNonQuery("SP_T_HMS_EmpBenefit_Insert_Update_Delete_VIEW_Paging_Select", objParam);
            AlertMsg.MsgBox(Page, "Updated");
            BindPager();
            txtEdtAmt.Text = string.Empty;
            tbledt.Visible = false;
        }
        protected void btnTransferAcc_Click(object sender, EventArgs e)
        {
            DataSet dsTransferDetail = new DataSet("TranserDataSet");
            DataTable dtTDT = new DataTable("TranserTable");
            dtTDT.Columns.Add(new DataColumn("CreditAmt", typeof(System.Double)));
            dtTDT.Columns.Add(new DataColumn("DebitAmt", typeof(System.Double)));
            dtTDT.Columns.Add(new DataColumn("Sequence", typeof(System.Int32)));
            dtTDT.Columns.Add(new DataColumn("EmpID", typeof(System.Int32)));
            dtTDT.Columns.Add(new DataColumn("CompanyID", typeof(System.Int32)));
            dtTDT.Columns.Add(new DataColumn("VocherID", typeof(System.Int32)));
            dtTDT.Columns.Add(new DataColumn("WorkSiteId", typeof(System.Int32)));
            dtTDT.Columns.Add(new DataColumn("ERID", typeof(System.Int32)));
            dsTransferDetail.Tables.Add(dtTDT);
            int EmpID = 0, count = 0;
            Double TotAmt = 0;
            foreach (GridViewRow gvRow in gvBenefit.Rows)
            {

                CheckBox chk = new CheckBox();
                chk = (CheckBox)gvRow.FindControl("chkToTransfer");
                if (chk.Checked)
                {
                    chk = (CheckBox)gvRow.FindControl("chkToTransfer");

                    Label lblEmp = (Label)gvRow.FindControl("lblEmpid");
                    Label lblBid = (Label)gvRow.FindControl("lblBid");
                    Label lblAmount = (Label)gvRow.FindControl("lblAmt");

                    int Bid = Convert.ToInt32(lblBid.Text);
                    EmpID = Convert.ToInt32(lblEmp.Text);
                    double Amt = Convert.ToDouble(lblAmount.Text);
                    TotAmt = TotAmt + Amt;
                    DataSet dsLed = AttendanceDAC.HR_EmpLedger(CompanyID, EmpID);
                    int VocherID = Convert.ToInt32(dsLed.Tables[0].Rows[0]["VocherID"]);
                    int WorkSiteId = Convert.ToInt32(dsLed.Tables[0].Rows[0]["WorkSiteId"]);

                    DataRow dr = dtTDT.NewRow();
                    dr["EmpID"] = EmpID;
                    dr["CompanyID"] = CompanyID;
                    dr["DebitAmt"] = Amt;
                    dr["CreditAmt"] = 0.00000;
                    dr["VocherID"] = VocherID;
                    dr["WorkSiteId"] = WorkSiteId;
                    dr["ERID"] = Bid;
                    dtTDT.Rows.Add(dr);

                    string Remarks = "Benefit Amount";
                    dsTransferDetail.AcceptChanges();
                    DataSet ds = AttendanceDAC.HMS_BenefitAccXML(dsTransferDetail, Remarks, TotAmt,  Convert.ToInt32(Session["UserId"]));

                    count = count + 1;
                }

            }
            if (count > 0)
            {
                AlertMsg.MsgBox(Page, "A/c Posted Done !");

            }
            else
            {
                AlertMsg.MsgBox(Page, "Select atleast one Record !");

            }
        }

        protected void btnsearchacposted_Click(object sender, EventArgs e)
        {
            EmpReimbursementTransferdPaging.CurrentPage = 1;
            BindPagerTransAmt();
        }


    }
}