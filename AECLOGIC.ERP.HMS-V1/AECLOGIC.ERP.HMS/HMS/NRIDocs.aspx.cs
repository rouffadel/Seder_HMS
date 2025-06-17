using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using AECLOGIC.HMS.BLL;
using System.Collections;
using DataAccessLayer;
using AECLOGIC.ERP.COMMON;
using Aeclogic.Common.DAL;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Data.SqlClient;
using AECLOGIC.ERP.HMS;


namespace AECLOGIC.ERP.HMSV1
{
    public partial class NRIDocsV1 : AECLOGIC.ERP.COMMON.WebFormMaster
    {
        HRCommon objCommon = new HRCommon();
        HRCommon objHrCommon = new HRCommon();
        AttendanceDAC objAtt = new AttendanceDAC();
        SRNService objSRN = new SRNService();
        int mid = 0;
        bool viewall;
        string menuname;
        string menuid;
        int countfordate = 0;
        protected override void OnInit(EventArgs e)
        {
            ModuleID = 1;
            base.OnInit(e);
            // btnSave.Attributes.Add("onclick", "javascript:return ValidateSave('" + txtCategoryName.ClientID + "');");
            PageTax.FirstClick += new Paging.PageFirst(EmpListPaging_FirstClick);
            PageTax.PreviousClick += new Paging.PagePrevious(EmpListPaging_FirstClick);
            PageTax.NextClick += new Paging.PageNext(EmpListPaging_FirstClick);
            PageTax.LastClick += new Paging.PageLast(EmpListPaging_FirstClick);
            PageTax.ChangeClick += new Paging.PageChange(EmpListPaging_FirstClick);
            PageTax.ShowRowsClick += new Paging.ShowRowsChange(EmpListPaging_ShowRowsClick);
            PageTax.CurrentPage = 1;
            Paging1.FirstClick += new Paging.PageFirst(EmpListPaging_FirstClick);
            Paging1.PreviousClick += new Paging.PagePrevious(EmpListPaging_FirstClick);
            Paging1.NextClick += new Paging.PageNext(EmpListPaging_FirstClick);
            Paging1.LastClick += new Paging.PageLast(EmpListPaging_FirstClick);
            Paging1.ChangeClick += new Paging.PageChange(EmpListPaging_FirstClick);
            Paging1.ShowRowsClick += new Paging.ShowRowsChange(EmpListPaging_ShowRowsClick);
            Paging1.CurrentPage = 1;
        }
        void EmpListPaging_ShowRowsClick(object sender, EventArgs e)
        {
            PageTax.CurrentPage = 1;
            Paging1.CurrentPage = 1;
            ddlItems_SelectedIndexChanged(sender, e);
            BindPager();
        }
        void EmpListPaging_FirstClick(object sender, EventArgs e)
        {
            ddlItems_SelectedIndexChanged(sender, e);
            BindPager();
        }
        void BindPager()
        {
            objCommon.PageSize = PageTax.ShowRows;
            objCommon.CurrentPage = PageTax.CurrentPage;
            BindView();
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
                retVal = ds.Tables[0].Select("DepartmentUId='" + DeptId + "'")[0]["DeptName"].ToString();
            }
            catch { }
            return retVal;
        }
        void BindView()
        {
            try
            {
                objCommon.PageSize = PageTax.ShowRows;
                objCommon.CurrentPage = PageTax.CurrentPage;
                int? SiteID = null;
                int? DeptID = null;
                int? DesigID = null;
                int? EmpID = null;
                if (ddlRecWs.SelectedValue != "0" && ddlRecWs.SelectedValue != string.Empty)
                {
                    SiteID = Convert.ToInt32(ddlRecWs.SelectedValue);
                }
                if (ddlRecDept.SelectedValue != "0" && ddlRecDept.SelectedValue != String.Empty)
                {
                    DeptID = Convert.ToInt32(ddlRecDept.SelectedValue);
                }
                if (ddlRecDesg.SelectedValue != "0" && ddlRecDesg.SelectedValue != string.Empty)
                {
                    DesigID = Convert.ToInt32(ddlRecDesg.SelectedValue);
                }
                if (ddlSearcMech.SelectedValue != "0" && ddlSearcMech.SelectedValue != string.Empty)
                {
                    EmpID = Convert.ToInt32(ddlSearcMech.SelectedValue);
                }
                int chkhijripost = 0;
                if (chkhijriPost.Checked)
                    chkhijripost = 1;
                string sponsor = "";
                if (txtPSponsor.Text != "")
                    sponsor = txtPSponsor.Text.Trim();
                SqlParameter[] P = new SqlParameter[12];
                P[0] = new SqlParameter("@CurrentPage", objCommon.CurrentPage);
                P[1] = new SqlParameter("@PageSize", objCommon.PageSize);
                P[2] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
                P[2].Direction = ParameterDirection.ReturnValue;
                P[3] = new SqlParameter("@NoofRecords", System.Data.SqlDbType.Int);
                P[3].Direction = ParameterDirection.Output;
                P[4] = new SqlParameter("@ResourceID", ResourceID);
                P[5] = new SqlParameter("@WsID", SiteID);
                P[6] = new SqlParameter("@DeptID", DeptID);
                P[7] = new SqlParameter("@DesigID", DesigID);
                P[8] = new SqlParameter("@EmpID", EmpID);
                P[9] = new SqlParameter("@DocName", ddlRecItems.SelectedItem.ToString());
                P[10] = new SqlParameter("@Hijri", chkhijripost);
                P[11] = new SqlParameter("@sponsor", sponsor);
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetReconsiledSRNItems", P);
                objCommon.NoofRecords = (int)P[3].Value;
                objCommon.TotalPages = (int)P[2].Value;
                //DataSet ds = AttendanceDAC.HR_GetReconsiledSRNItems(objCommon, Convert.ToInt32(ddlRecItems.SelectedValue), SiteID, DeptID, DesigID, EmpID, ddlRecItems.SelectedItem.ToString(), chkhijripost);
                if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0)
                {
                    Session["gvRecosiledDT"] = ds.Tables[0];
                    gvRecosiled.DataSource = ds;
                    gvRecosiled.DataBind();
                    gvRecosiled.Visible = true;
                    PageTax.Visible = true;
                    PageTax.Bind(objCommon.CurrentPage, objCommon.TotalPages, objCommon.NoofRecords, objCommon.PageSize);
                    //Paging1.Visible = true;
                }
                else
                {
                    gvRecosiled.DataSource = null;
                    gvRecosiled.DataBind();
                }
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
                AttendanceDAC objRights = new AttendanceDAC();
                DataSet ds = objRights.GetWorkSite_By_Employees(Convert.ToInt32(Session["CompanyID"]), Convert.ToInt32(Session["RoleId"]));
                ViewState["WorkSites"] = ds;
                DataSet dss = AttendanceDAC.BindDeparmetBySite(0, Convert.ToInt32(Session["CompanyID"]));
                ViewState["Departments"] = dss;
                ViewState["NRIDocs"] = "";
                lblGroup.Visible = lblNewItems.Visible = ddlGroup.Visible = ddlNewItems.Visible = btnAddNew.Visible = false;
                tblUnRecon.Visible = true;
                tblReconciled.Visible = false;
                trReconciled.Visible = false;
                BindResourceTypes();
                PageTax.Visible = false;
                btnUpdate.Visible = false;
                btnProcess.Visible = false;
                ViewState["RecDataset"] = "";
            }
        }
        public int GetMechIndex(string Value)
        {
            return AllMechinaries.IndexOf(Value);
        }
        public static ArrayList AllMechinaries = new ArrayList();
        public int GetParentMenuId()
        {
            string URL = Request.Url.Segments[Request.Url.Segments.Length - 1] + Request.Url.Query;
            int RoleId = Convert.ToInt32(Session["RoleId"].ToString());
            int ModuleId = ModuleID; ;
            DataSet ds = Common.GetAllowed(RoleId, ModuleId, URL);
            int MenuId = 0;
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                MenuId = Convert.ToInt32(ds.Tables[0].Rows[0]["Under"]);
                btnAddNew.Enabled = (bool)ds.Tables[0].Rows[0]["Editable"];
            }
            return MenuId;
        }
        public void BindMechinaries()
        {
            DataSet ds = objAtt.GetEmployeesByWSDEptNature(null, null, null);
            DataRow dr;
            dr = ds.Tables[0].NewRow();
            dr[0] = 0;
            dr[1] = "--Select--";
            ds.Tables[0].Rows.InsertAt(dr, 0);
            ViewState["Machinery"] = ds;
            ddlSearcMech.DataSource = ds;
            ddlSearcMech.DataTextField = "Name";
            ddlSearcMech.DataValueField = "EmpId";
            ddlSearcMech.DataBind();
            AllMechinaries = new ArrayList();
            foreach (DataRow drMech in ds.Tables[0].Rows)
                AllMechinaries.Add(drMech["EmpId"].ToString());
            BindEMpByWO();
        }
        public void BindResourceTypes()
        {
            DataSet ds = AttendanceDAC.HR_GetSatuatoryItems();
            ddlItems.DataSource = ds;
            ddlItems.DataTextField = "ResourceName";
            ddlItems.DataValueField = "ResourceID";
            ddlItems.DataBind();
            ddlItems.Items.Insert(0, new ListItem("--Select--", "0"));
            ddlItems.Items.Add(new ListItem("<<Add New>>", "-1"));
            ddlRecItems.DataSource = ds;
            ddlRecItems.DataTextField = "ResourceName";
            ddlRecItems.DataValueField = "ResourceID";
            ddlRecItems.DataBind();
            ddlRecItems.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        protected void rbTaxasion_SelectedIndexChanged(object sender, EventArgs e)
        {
            chkhijriPost.Checked = false;
            tblEdit.Visible = false;
            trEdit.Visible = false;
            tblFinalEdit.Visible = false;
            int Item = Convert.ToInt32(rbTaxasion.SelectedValue);
            if (Item == 1)
            {
                tblUnRecon.Visible = true;
                tblReconciled.Visible = false;
                trReconciled.Visible = false;
                // btnSearch_Click(sender, e);
            }
            else
            {
                tblUnRecon.Visible = false;
                tblReconciled.Visible = true;
                trReconciled.Visible = true;
                GetDepartments();
                GetWorkSites();
                BindDesignations();
                BindMechinaries();
            }
        }
        protected void ddlItems_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlItems.SelectedValue != "-1")
                {
                    objCommon.PageSize = PageTax.ShowRows;
                    objCommon.CurrentPage = PageTax.CurrentPage;
                    objHrCommon.PageSize = Paging1.ShowRows;
                    objHrCommon.CurrentPage = Paging1.CurrentPage;
                    gvUnReconciled.DataSource = null;
                    gvUnReconciled.DataBind();
                    lblGroup.Visible = lblNewItems.Visible = ddlGroup.Visible = ddlNewItems.Visible = btnAddNew.Visible = false;
                    int ResourceID = Convert.ToInt32(ddlItems.SelectedValue);
                    int? WO = null;
                    if (txtWO.Text == null || txtWO.Text == string.Empty)
                        WO = null;
                    else
                        WO = Convert.ToInt32(txtWO.Text);
                    DataSet ds = AttendanceDAC.HR_GetSRNItemsDetail(WO, ResourceID, objHrCommon);
                    gvUnReconciled.DataSource = ds;
                    gvUnReconciled.DataBind();
                    //tblSaving.Visible = false;
                    //Paging1.Bind(objCommon.CurrentPage, objCommon.TotalPages, objCommon.NoofRecords, objCommon.PageSize);
                    Paging1.Bind(objHrCommon.CurrentPage, objHrCommon.TotalPages, objHrCommon.NoofRecords, objHrCommon.PageSize);
                    Paging1.Visible = true;
                }
                else
                {
                    lblGroup.Visible = lblNewItems.Visible = ddlGroup.Visible = ddlNewItems.Visible = btnAddNew.Visible = true;
                    DataSet ds = AttendanceDAC.SP_PM_SearchCategoriesByService();
                    ddlGroup.DataSource = ds;
                    ddlGroup.DataTextField = "Category_Name";
                    ddlGroup.DataValueField = "Category_Id";
                    ddlGroup.DataBind();
                    ddlGroup.Items.Insert(0, new ListItem("--Select--", "0"));
                }
            }
            catch { }
        }
        public string PONavigateUrl(string POID)
        {
            bool Fals = false;
            return "javascript:return window.open('ProPurchaseOrderPrint.aspx?id=" + POID + "&PON=" + 1 + "&tot=" + Fals + "' , '_blank')";
        }
        public string ViewImage(string obj, string ID)
        {
            string ReturnVal = "";
            if (obj != "")
            {
                ReturnVal = "javascript:return window.open('./ScanedDocuments/" + ID + obj + "', '_blank')";
            }
            foreach (GridViewRow row in gvRecosiled.Rows)
            {
                LinkButton lb = (LinkButton)row.FindControl("lnkImage");
                lb.Visible = false;
            }
            return ReturnVal;
        }
        public string ViewInvImage(string obj, string SrnItemID)
        {
            string ReturnVal = "";
            if (obj != "")
            {
                ReturnVal = "javascript:return window.open('/SDNItemsImages/" + SrnItemID + "." + obj + "', '_blank')";
            }
            return ReturnVal;
        }
        public bool ViewVisible(string SrnItemID, int Status)
        {
            bool status = false;
            DataSet ds = objSRN.MMS_CheckPicInSRNNItems(int.Parse(SrnItemID));
            if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0)
            {
                int ImgCount = Convert.ToInt32(ds.Tables[1].Rows[0]["Invoiceimg"]);
                if (Status == 1)
                {
                    if (ImgCount > 0)
                        status = false;
                    else
                        status = true;
                }
                else
                {
                    if (ImgCount > 0)
                        status = true;
                    else
                        status = false;
                }
            }
            return status;
        }
        public string Reconsolise(string SrnItemID, string ResourceID, string SRNID)
        {
            return null;
        }
        public void BindReconsileGrid(int ResourID)
        {
            if (ddlRecItems.SelectedItem.Value != "0")
            {
                if (ResourID == 0)
                {
                    ResourID = Convert.ToInt32(ddlRecItems.SelectedItem.Value);
                }
                int? SiteID = null;
                int? DeptID = null;
                int? DesigID = null;
                int? EmpID = null;
                string DocName = null;
                if (ddlRecWs.SelectedItem.Value != "0")
                {
                    SiteID = Convert.ToInt32(ddlRecWs.SelectedItem.Value);
                }
                if (ddlRecDept.SelectedItem.Value != "0")
                {
                    DeptID = Convert.ToInt32(ddlRecDept.SelectedItem.Value);
                }
                if (ddlRecDesg.SelectedItem.Value != "0")
                {
                    DesigID = Convert.ToInt32(ddlRecDesg.SelectedItem.Value);
                }
                if (ddlSearcMech.SelectedItem.Value != "0")
                {
                    EmpID = Convert.ToInt32(ddlSearcMech.SelectedItem.Value);
                }
                if (ddlRecItems.SelectedItem.Value != "0")
                {
                    DocName = ddlRecItems.SelectedItem.ToString();
                }
                gvRecosiled.DataSource = null;
                gvRecosiled.DataBind();
                int chkhijripost = 0;
                if (chkhijriPost.Checked)
                    chkhijripost = 1;
                DataSet ds = AttendanceDAC.HR_GetReconsiledSRNItems(objCommon, ResourID, SiteID, DeptID, DesigID, EmpID, DocName, chkhijripost);
                if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0)
                {
                    gvRecosiled.DataSource = ds;
                    gvRecosiled.DataBind();
                    PageTax.Bind(objCommon.CurrentPage, objCommon.TotalPages, objCommon.NoofRecords, objCommon.PageSize);
                }
            }
            else
            {
                AlertMsg.MsgBox(Page, "Select Documenttype", AlertMsg.MessageType.Warning);
                //lblStatus.Text = "Select Documenttype";
                //lblStatus.ForeColor = System.Drawing.Color.Red;
            }
        }
        public void BindEMpByWO()
        {
            DataSet ds = null;
            if (chkallEmp.Checked)
            {
                ds = objAtt.GetEmployeesByWSDEptNature(null, null, null);
            }
            else
            {
                SqlParameter[] objParam = new SqlParameter[1];
                objParam[0] = new SqlParameter("@WONO", Session["GRNItemID"]);
                ds = SQLDBUtil.ExecuteDataset("HMS_GETEMP_By_WO", objParam);
            }
            DataRow dr;
            dr = ds.Tables[0].NewRow();
            dr[0] = 0;
            dr[1] = "--Select--";
            ds.Tables[0].Rows.InsertAt(dr, 0);
            ViewState["BindEMPBYWO"] = ds.Tables[0];
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionListEmp(string prefixText, int count, string contextKey)
        {
            DataSet ds = AttendanceDAC.HR_GoogleSearchEmp(prefixText);
            return ConvertStingArray(ds);
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
        protected void gvUnReconciled_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (chkhijri.Checked)
                    chkhijri.Checked = false;
                if (chkallEmp.Checked)
                    chkallEmp.Checked = false;
                gvEdit.Visible = true;
                Accordion1.Visible = true;
                lblProgessdetails.Visible = true;
                GridViewRow gvrow = (GridViewRow)((LinkButton)e.CommandSource).Parent.Parent;
                int GRNItemID = Convert.ToInt32(e.CommandArgument);
                Session["GRNItemID"] = GRNItemID;
                // added this by pratap dt: 26 dec 2017
                if (e.CommandName == "PrePaidExpenses")
                {
                    int WoNo = Convert.ToInt32(e.CommandArgument);
                    Label lblSRNItemID = (Label)gvrow.FindControl("lblSRNItemID");
                    Label lblResourceID = (Label)gvrow.FindControl("lblResourceID");
                    AttendanceDAC.sa_PrePaidExpensesAdjustedTransaction(WoNo, Convert.ToInt32(lblSRNItemID.Text),
                        Convert.ToInt32(lblResourceID.Text), Convert.ToInt32(Session["UserId"]), 1, CompanyID);
                    AlertMsg.MsgBox(Page, "Done", AlertMsg.MessageType.Success);
                    ddlItems_SelectedIndexChanged(null, null);
                }
                else if (e.CommandName == "Reconse")
                {
                    // added this by pratap dt: 19 mar 2018
                    lblgBillTransID.Text = "0";
                    GridViewRow gvrow1 = (GridViewRow)((LinkButton)e.CommandSource).Parent.Parent;
                    Label lblBillTransID = (Label)gvrow1.FindControl("lblBillTransID");
                    lblgBillTransID.Text=lblBillTransID.Text;

                    gvEdit.DataSource = null;
                    gvEdit.DataBind();
                    GetDepartments();
                    GetWorkSites();
                    //BindMechinaries();
                    BindDesignations();
                    DataSet ds = HR_SRNIDbySRNItemID(GRNItemID, 0);
                    Session["SRNItemIDs"] = ds.Tables[0].Rows[0]["SRNItemID"].ToString();
                    Session["SRNIDs"] = ds.Tables[0].Rows[0]["SRNID"].ToString();
                    DataTable datatable2 = new DataTable();
                    //showing all employees checkbox
                    string chkemp = ds.Tables[0].Rows[0]["chkShowAllEmps"].ToString();
                    if (chkemp != null && chkemp != "")
                    {
                        chkallEmp.Checked = Convert.ToBoolean(chkemp);
                    }
                    BindMechinaries();
                    if (Convert.ToInt32(ds.Tables[0].Rows[0]["Qty"]) > 0)
                    {
                        DateTime dt = Convert.ToDateTime(ds.Tables[0].Rows[0]["CreatedOn"]);
                        string d = dt.Date.ToString("dd MMMM yyyy");
                        ViewState["Datedet"] = ds.Tables[0];
                        lblProgessdetails.Text = "Work Order No : " + ds.Tables[0].Rows[0]["POID"].ToString() + " , Work Order Date : " + d + " , No.Of Records : " + Convert.ToInt32(ds.Tables[0].Rows[0]["Qty"]).ToString() + " ;";
                        ViewState["RecDataset"] = ds;
                        DataTable datatable1 = (DataTable)ds.Tables[0];
                        for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
                        {
                            datatable2.Columns.Add(ds.Tables[0].Columns[i].ColumnName);
                        }
                        datatable2.Columns.Add("ID");  // T_HR_EmpStatututoryItems   table primary ID
                        datatable2.Columns["ID"].DefaultValue = 0;
                        for (int k = 0; k < Convert.ToInt32(ds.Tables[0].Rows[0]["Qty"]); k++)
                        {
                            foreach (DataRow dr1 in datatable1.Rows)
                            {
                                object[] row = dr1.ItemArray;
                                datatable2.Rows.Add(row);
                            }
                        }
                        gvEdit.DataSource = null;
                        gvEdit.DataBind();
                        gvEdit.DataSource = datatable2;
                        gvEdit.DataBind();
                        foreach (GridViewRow gvRow in gvEdit.Rows)
                        {
                            DropDownList ddlCity = (DropDownList)gvRow.Cells[9].FindControl("ddlCity");
                            DataSet dscity = objAtt.GetEmployeesByWSDCity();
                            ddlCity.DataSource = dscity;
                            ddlCity.DataTextField = "CItyName";
                            ddlCity.DataValueField = "CityID";
                            ddlCity.DataBind();
                            ddlCity.Items.Insert(0, new ListItem("--Select--"));
                        }
                        btnProcess.Visible = false;
                    }
                    else
                    {
                        int i = 0;
                        /*foreach (GridViewRow gvRow in gvEdit.Rows)
                        {
                            string txtTemFrom = DateTime.Now.Date.ToString("dd/MM/yyyy");
                            txtTemFrom = txtTemFrom.Replace('-', '/');
                            AECLOGIC.ERP.COMMON.HijriGregDatePicker txtFrom = ((AECLOGIC.ERP.COMMON.HijriGregDatePicker)gvRow.Cells[5].FindControl("txtVFrom"));
                            txtFrom.setGregorianText(txtTemFrom);
                            DateTime tempFromDate = DateTime.ParseExact(txtTemFrom, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                            CultureInfo arabicCulture = new CultureInfo("ar-SA");
                            txtFrom.setHijriDateText(tempFromDate.ToString("dd/MM/yyyy", arabicCulture.DateTimeFormat));
                        }*/
                        gvEdit.DataSource = null;
                        gvEdit.DataBind();
                        datatable2 = (DataTable)ds.Tables[0];
                        gvEdit.DataSource = datatable2;
                        gvEdit.DataBind();
                        DateTime dt = Convert.ToDateTime(ds.Tables[0].Rows[0]["CreatedOn"]);
                        string d = dt.Date.ToString("dd MMMM yyyy");
                        lblProgessdetails.Text = "Work Order No : " + ds.Tables[0].Rows[0]["POID"].ToString() + " ," + " Work Order Date : " + d + " , No.Of Records : " + ds.Tables[0].Rows.Count + " ;";
                        foreach (GridViewRow gvRow in gvEdit.Rows)
                        {
                            DropDownList ddlMachinery = (DropDownList)gvRow.Cells[4].FindControl("ddlMachinery");
                            ddlMachinery.SelectedValue = Convert.ToInt32(ds.Tables[0].Rows[i]["EmpID"]).ToString();
                            //Added by Rijwan for DDl City :- 19-04-2016
                            DropDownList ddlCity = (DropDownList)gvRow.Cells[9].FindControl("ddlCity");
                            //ddlCity.SelectedValue = Convert.ToInt32(ds.Tables[0].Rows[i]["IssuePlace"]).ToString();
                            DataSet dscity = objAtt.GetEmployeesByWSDCity();
                            ddlCity.DataSource = dscity;
                            ddlCity.DataTextField = "CItyName";
                            ddlCity.DataValueField = "CityID";
                            ddlCity.DataBind();
                            ddlCity.Items.Insert(0, new ListItem("--Select--"));
                            try
                            {
                                int count = Convert.ToInt32(ds.Tables[0].Rows[i]["IssuePlace"]);
                                if (count >= 0)
                                {
                                    ddlCity.SelectedValue = Convert.ToInt32(ds.Tables[0].Rows[i]["IssuePlace"]).ToString();
                                }
                            }
                            catch { ddlCity.SelectedValue = "0"; }
                            TextBox txtNumber = (TextBox)gvRow.Cells[7].FindControl("txtNumber");
                            txtNumber.Text = ds.Tables[0].Rows[i]["Numeber"].ToString();
                            TextBox txtAltNumber = (TextBox)gvRow.Cells[8].FindControl("txtAltNumber");
                            txtAltNumber.Text = ds.Tables[0].Rows[i]["AltNumber"].ToString();
                            TextBox txtIssuer = (TextBox)gvRow.Cells[9].FindControl("txtIssuer");
                            txtIssuer.Text = ds.Tables[0].Rows[i]["Issuer"].ToString();
                            TextBox txtRemarks = (TextBox)gvRow.Cells[10].FindControl("txtRemarks");
                            txtRemarks.Text = ds.Tables[0].Rows[i]["Remarks"].ToString();
                            i++;
                        }
                        foreach (GridViewRow gvRow in gvEdit.Rows)
                        {
                            DropDownList ddlMachinery = (DropDownList)gvRow.FindControl("ddlMachinery");
                            TextBox txtvfrom = (TextBox)gvRow.FindControl("txtVFrom");
                            TextBox txtvTo = (TextBox)gvRow.FindControl("txtVTo");
                            TextBox txtNumber = (TextBox)gvRow.FindControl("txtNumber");
                            TextBox txtaltno = (TextBox)gvRow.FindControl("txtAltNumber");
                            DropDownList ddlCity = (DropDownList)gvRow.FindControl("ddlCity");
                            TextBox txtIssuer = (TextBox)gvRow.FindControl("txtIssuer");
                            if (ddlMachinery.SelectedIndex == 0 || txtvfrom.Text == string.Empty || txtvTo.Text == string.Empty || txtNumber.Text == string.Empty || txtaltno.Text == string.Empty || ddlCity.SelectedIndex == 0 || txtIssuer.Text == string.Empty)
                            {
                                btnProcess.Visible = false;
                            }
                            else
                            {
                                btnProcess.Visible = true;
                            }
                        }
                    }
                    tblReconciled.Visible = true;
                    trReconciled.Visible = false;
                    tblUnRecon.Visible = true;
                    tblEdit.Visible = true;
                    trEdit.Visible = true;
                    PageTax.Visible = false;
                    gvRecosiled.Visible = false;
                }
                foreach (GridViewRow gvRow in gvEdit.Rows)
                {
                    DropDownList ddlMachinery = (DropDownList)gvRow.FindControl("ddlMachinery");
                    TextBox txtvfrom = (TextBox)gvRow.FindControl("txtVFrom");
                    TextBox txtvTo = (TextBox)gvRow.FindControl("txtVTo");
                    TextBox txtNumber = (TextBox)gvRow.FindControl("txtNumber");
                    TextBox txtaltno = (TextBox)gvRow.FindControl("txtAltNumber");
                    DropDownList ddlCity = (DropDownList)gvRow.FindControl("ddlCity");
                    TextBox txtIssuer = (TextBox)gvRow.FindControl("txtIssuer");
                    if (ddlMachinery.SelectedIndex == 0 || txtvfrom.Text == string.Empty || txtvTo.Text == string.Empty || txtNumber.Text == string.Empty || txtaltno.Text == string.Empty || ddlCity.SelectedIndex == 0 || txtIssuer.Text == string.Empty)
                    {
                        btnProcess.Visible = false;
                    }
                    else
                    {
                        btnProcess.Visible = true;
                    }
                }
                btnUpdate.Visible = true;
            }
            catch (Exception ex) { }
        }
        protected void gvRecosiled_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            objCommon.PageSize = PageTax.ShowRows;
            objCommon.CurrentPage = PageTax.CurrentPage;
            if (e.CommandName == "Del")
            {
                int ID = Convert.ToInt32(e.CommandArgument);
                AttendanceDAC.HR_DelReconsledItem(ID);
                BindReconsileGrid(Convert.ToInt32(ddlItems.SelectedValue));
            }
            if (e.CommandName == "Edt")
            {
                tblFinalEdit.Visible = true;
                int ID = Convert.ToInt32(e.CommandArgument);
                gvFinalEdit.DataSource = null;
                gvFinalEdit.DataBind();
                DataSet ds = AttendanceDAC.HR_GetEmpStaturyItems(ID);
                if (ds != null)
                {
                    gvFinalEdit.DataSource = ds;
                    gvFinalEdit.DataBind();
                    tblReconciled.Visible = true;
                    trReconciled.Visible = true;
                    tblUnRecon.Visible = false;
                    tblEdit.Visible = false;
                    trEdit.Visible = false;
                }
            }
            if (e.CommandName == "View")
            {
                GridViewRow gvr = (GridViewRow)(((Control)e.CommandSource).NamingContainer);
                Label lblEmpid = (Label)gvr.FindControl("lblEmpid");
                Response.Redirect("empdocuments.aspx?eid=" + lblEmpid.Text);
            }
            else if (e.CommandName == "Uplaod")
            {
            }
        }
        protected void gvEdit_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int ID = Convert.ToInt32(e.CommandArgument);
            if (e.CommandName == "Edt")
            {
                try
                {
                    GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).Parent.Parent;
                    Label lblESItemsID = (Label)gvEdit.Rows[row.RowIndex].FindControl("lblESItemsID");
                    if (Convert.ToInt32(lblESItemsID.Text) > 0)
                    {
                        DropDownList ddlMach = (DropDownList)gvEdit.Rows[row.RowIndex].FindControl("ddlMachinery");
                        DropDownList ddlCity = (DropDownList)gvEdit.Rows[row.RowIndex].FindControl("ddlCity");
                        Label lblSRNID = (Label)gvEdit.Rows[row.RowIndex].FindControl("lblSRNID");
                        TextBox txtNumber = (TextBox)gvEdit.Rows[row.RowIndex].FindControl("txtNumber");
                        TextBox txtAltNumber = (TextBox)gvEdit.Rows[row.RowIndex].FindControl("txtAltNumber");
                        TextBox txtIssuer = (TextBox)gvEdit.Rows[row.RowIndex].FindControl("txtIssuer");
                        TextBox txtRemarks = (TextBox)gvEdit.Rows[row.RowIndex].FindControl("txtRemarks");
                        TextBox txtFrom = (TextBox)gvEdit.Rows[row.RowIndex].FindControl("txtVFrom");
                        TextBox txtTo = (TextBox)gvEdit.Rows[row.RowIndex].FindControl("txtVTo");
                        string ext = string.Empty;
                        ext = "";
                        int chkhij = 0;
                        if (chkhijri.Checked)
                            chkhij = 1;
                        DateTime fromDateValue;
                        string F = txtFrom.Text;
                        string T = txtTo.Text;
                        if (txtFrom.Text != "" && txtTo.Text != "")
                        {
                            try
                            {
                                DateTime fromdate = CodeUtilHMS.ConvertToDate(txtFrom.Text, CodeUtilHMS.DateFormat.DayMonthYear);
                                DateTime todate = CodeUtilHMS.ConvertToDate(txtTo.Text, CodeUtilHMS.DateFormat.DayMonthYear);
                                if (fromdate > todate)
                                {
                                    AlertMsg.MsgBox(Page, "To date Should be GreaterThan From date", AlertMsg.MessageType.Warning);
                                    //lblStatus.Text = "To date Should be GreaterThan From date";
                                    //lblStatus.ForeColor = System.Drawing.Color.Red;
                                    return;
                                }
                            }
                            catch
                            {
                                AlertMsg.MsgBox(Page, "Date Is Invalid", AlertMsg.MessageType.Error);
                                //lblStatus.Text = "Date Is Invalid";
                                //lblStatus.ForeColor = System.Drawing.Color.Red;
                                return;
                            }
                        }
                        if (chkhijri.Checked)
                        {
                            if (txtFrom.Text != "" || txtTo.Text != "")
                            {
                                try
                                {
                                    CultureInfo arabicCulture = new CultureInfo("ar-SA");
                                    var strFormat = new[] { "dd/MM/yyyy", "yyyy-MM-dd", "d/MM/yyy", "d/M/yyyy", "dd/M/yyyy" };
                                    CultureInfo gregorianCulture = new CultureInfo("en-US");
                                    DateTime FromA = new DateTime();
                                    DateTime ToA = new DateTime();
                                    string strFrom = GetDateFromHijri(txtFrom.Text);
                                    string strTo = GetDateFromHijri(txtTo.Text);
                                    if (strFrom != "")
                                        F = txtFrom.Text = strFrom;
                                    if (strTo != "")
                                        T = txtTo.Text = strTo;
                                    if (strFrom == "" && strTo == "") // go for arabic conversion
                                    {   
                                        if (txtFrom.Text == "" && txtTo.Text != "")
                                        {
                                            F = "";
                                            ToA = DateTime.ParseExact(txtTo.Text, strFormat, arabicCulture.DateTimeFormat, DateTimeStyles.AllowWhiteSpaces);
                                            txtTo.Text = ToA.ToString("dd/MM/yyyy");
                                        }
                                        else if (txtTo.Text == "" && txtFrom.Text != "")
                                        {
                                            T = "";
                                            FromA = DateTime.ParseExact(txtFrom.Text, strFormat, arabicCulture.DateTimeFormat, DateTimeStyles.AllowWhiteSpaces);
                                            txtFrom.Text = FromA.ToString("dd/MM/yyyy");
                                        }
                                        else if (txtFrom.Text != "" && txtTo.Text != "")
                                        {
                                            ToA = DateTime.ParseExact(txtTo.Text, strFormat, arabicCulture.DateTimeFormat, DateTimeStyles.AllowWhiteSpaces);
                                            FromA = DateTime.ParseExact(txtFrom.Text, strFormat, arabicCulture.DateTimeFormat, DateTimeStyles.AllowWhiteSpaces);
                                            txtTo.Text = ToA.ToString("dd/MM/yyyy");
                                            txtFrom.Text = FromA.ToString("dd/MM/yyyy");
                                        }
                                    }
                                    else 
                                    {
                                        if (strFrom != "" && strTo == "")
                                        {
                                            if (txtTo.Text != "")
                                            {
                                                ToA = DateTime.ParseExact(txtTo.Text, strFormat, arabicCulture.DateTimeFormat, DateTimeStyles.AllowWhiteSpaces);
                                                txtTo.Text = ToA.ToString("dd/MM/yyyy");
                                            }
                                            FromA = DateTime.ParseExact(txtFrom.Text, strFormat, gregorianCulture.DateTimeFormat, DateTimeStyles.AllowWhiteSpaces);
                                        }
                                        else if (strFrom == "" && strTo != "")
                                        {
                                            if (txtFrom.Text != "")
                                            {
                                                FromA = DateTime.ParseExact(txtFrom.Text, strFormat, arabicCulture.DateTimeFormat, DateTimeStyles.AllowWhiteSpaces);
                                                txtFrom.Text = FromA.ToString("dd/MM/yyyy");
                                            }
                                            ToA = DateTime.ParseExact(txtTo.Text, strFormat, gregorianCulture.DateTimeFormat, DateTimeStyles.AllowWhiteSpaces);
                                        }
                                        else if (strFrom != "" && strTo != "")
                                        {
                                            ToA = DateTime.ParseExact(txtTo.Text, strFormat, gregorianCulture.DateTimeFormat, DateTimeStyles.AllowWhiteSpaces);
                                            FromA = DateTime.ParseExact(txtFrom.Text, strFormat, gregorianCulture.DateTimeFormat, DateTimeStyles.AllowWhiteSpaces);
                                        }
                                    }
                                    F = FromA.ToString("dd/MM/yyyy");
                                    T = ToA.ToString("dd/MM/yyyy");
                                }
                                catch
                                {
                                    AlertMsg.MsgBox(Page, "Hijri Date Is Invalid", AlertMsg.MessageType.Error);
                                    //lblStatus.Text = "Hijri Date Is Invalid";
                                    //lblStatus.ForeColor = System.Drawing.Color.Red;
                                    return;
                                }
                            }
                        }
                        var formats = new[] { "dd/MM/yyyy", "yyyy-MM-dd" };
                        if ((DateTime.TryParseExact(F, formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out fromDateValue) && DateTime.TryParseExact(T, formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out fromDateValue)) || (txtFrom.Text == "" || txtFrom.Text == string.Empty || txtTo.Text == "" || txtTo.Text == string.Empty))
                        {
                            ID = Convert.ToInt32(HR_InsUpReconsolisedItemsBy_ID(Convert.ToInt32(lblESItemsID.Text), ID, Convert.ToInt32(lblSRNID.Text), txtFrom.Text, txtTo.Text, Convert.ToInt32(Session["UserId"]), Convert.ToInt32(ddlMach.SelectedValue)//for saving data properlly by Rt
                                                                      , txtNumber.Text, txtAltNumber.Text, Convert.ToInt32(ddlCity.SelectedValue == "--Select--" ? "0" : ddlCity.SelectedValue), txtIssuer.Text, txtRemarks.Text, ext, chkhij, Convert.ToInt32(chkallEmp.Checked ? "1" : "0")));
                        }
                        else
                        {
                            AlertMsg.MsgBox(Page, "Date Is Invalid", AlertMsg.MessageType.Error);
                            //lblStatus.Text = "Date Is Invalid";
                            //lblStatus.ForeColor = System.Drawing.Color.Red;
                            return;
                        }
                        if (ID != 0)
                        {
                            AlertMsg.MsgBox(Page, "Saved Sucessfully!", AlertMsg.MessageType.Success);
                            //lblStatus.Text = "Saved Sucessfully";
                            //lblStatus.ForeColor = System.Drawing.Color.Green;
                            gvEdit.Visible = false;
                            Accordion1.Visible = false;
                            btnUpdate.Visible = false;
                            btnProcess.Visible = false;
                            trEdit.Visible = false;
                            lblProgessdetails.Text = string.Empty;
                        }
                    }
                    else
                    {
                        foreach (GridViewRow gvRow in gvEdit.Rows)
                        {
                            Label lblSRNItemID = (Label)gvRow.FindControl("lblSRNItemID");
                            int SRNItemIDs = 0;
                            if (SRNItemIDs == 0)
                            {
                                SRNItemIDs = Convert.ToInt32(lblSRNItemID.Text);
                            }
                            TextBox txtFrom = (TextBox)gvEdit.Rows[row.RowIndex].FindControl("txtVFrom");
                            TextBox txtTo = (TextBox)gvEdit.Rows[row.RowIndex].FindControl("txtVTo");
                            DropDownList ddlMach = (DropDownList)gvRow.FindControl("ddlMachinery");
                            DropDownList ddlCity = (DropDownList)gvRow.FindControl("ddlCity");
                            Label lblSRNID = (Label)gvRow.FindControl("lblSRNID");
                            TextBox txtNumber = (TextBox)gvRow.FindControl("txtNumber");
                            TextBox txtAltNumber = (TextBox)gvRow.FindControl("txtAltNumber");
                            TextBox txtIssuer = (TextBox)gvRow.FindControl("txtIssuer");
                            TextBox txtRemarks = (TextBox)gvRow.FindControl("txtRemarks");
                            if (txtFrom.Text != "")
                            {
                                string ext = string.Empty;
                                ext = "";
                                int chkhij = 0;
                                if (chkhijri.Checked)
                                    chkhij = 1;
                                DateTime fromDateValue;
                                string F = txtFrom.Text;
                                string T = txtTo.Text;
                                if (txtFrom.Text != "" && txtTo.Text != "")
                                {
                                    try
                                    {
                                        DateTime fromdate = CodeUtilHMS.ConvertToDate(txtFrom.Text, CodeUtilHMS.DateFormat.DayMonthYear);
                                        DateTime todate = CodeUtilHMS.ConvertToDate(txtTo.Text, CodeUtilHMS.DateFormat.DayMonthYear);
                                        if (fromdate > todate)
                                        {
                                            AlertMsg.MsgBox(Page, "To date Should be GreaterThan From date", AlertMsg.MessageType.Warning);
                                            //lblStatus.Text = "To date Should be GreaterThan From date";
                                            //lblStatus.ForeColor = System.Drawing.Color.Red;
                                            return;
                                        }
                                    }
                                    catch
                                    {
                                        AlertMsg.MsgBox(Page, "Date Is Invalid", AlertMsg.MessageType.Warning);
                                        //lblStatus.Text = "Date Is Invalid";
                                        //lblStatus.ForeColor = System.Drawing.Color.Red;
                                        return;
                                    }
                                }
                                if (chkhijri.Checked)
                                {
                                    CultureInfo arabicCulture = new CultureInfo("ar-SA");
                                    string strFormat = "dd/MM/yyyy";
                                    string strFromDate = GetDateFromHijri(txtFrom.Text);
                                    string strToDate = GetDateFromHijri(txtTo.Text);
                                    if (txtFrom.Text != "" || txtTo.Text != "")
                                    {
                                        try
                                        {
                                            DateTime FromA = new DateTime();
                                            DateTime ToA = new DateTime();
                                            if (txtFrom.Text == "" && txtTo.Text != "")
                                            {
                                                F = "";
                                                ToA = DateTime.ParseExact(txtTo.Text, strFormat, arabicCulture.DateTimeFormat, DateTimeStyles.AllowWhiteSpaces);
                                            }
                                            else if (txtTo.Text == "" && txtFrom.Text != "")
                                            {
                                                T = "";
                                                FromA = DateTime.ParseExact(txtFrom.Text, strFormat, arabicCulture.DateTimeFormat, DateTimeStyles.AllowWhiteSpaces);
                                            }
                                            else if (txtFrom.Text != "" && txtTo.Text != "")
                                            {
                                                ToA = DateTime.ParseExact(txtTo.Text, strFormat, arabicCulture.DateTimeFormat, DateTimeStyles.AllowWhiteSpaces);
                                                FromA = DateTime.ParseExact(txtFrom.Text, strFormat, arabicCulture.DateTimeFormat, DateTimeStyles.AllowWhiteSpaces);
                                            }
                                            F = FromA.ToString("dd/MM/yyyy");
                                            T = ToA.ToString("dd/MM/yyyy");
                                        }
                                        catch
                                        {
                                            AlertMsg.MsgBox(Page, "Hijri Date Is Invalid", AlertMsg.MessageType.Error);
                                            //lblStatus.Text = "Hijri Date Is Invalid";
                                            //lblStatus.ForeColor = System.Drawing.Color.Red;
                                            return;
                                        }
                                    }
                                }
                                var formats = new[] { "dd/MM/yyyy", "yyyy-MM-dd" };
                                if ((DateTime.TryParseExact(F, formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out fromDateValue) && DateTime.TryParseExact(T, formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out fromDateValue)) || (txtFrom.Text == "" || txtFrom.Text == string.Empty || txtTo.Text == "" || txtTo.Text == string.Empty))
                                {
                                    ID = Convert.ToInt32(HR_InsUpReconsolisedItemsBy_ID(Convert.ToInt32(lblESItemsID.Text), SRNItemIDs, Convert.ToInt32(lblSRNID.Text), txtFrom.Text, txtTo.Text, Convert.ToInt32(Session["UserId"]), Convert.ToInt32(ddlMach.SelectedValue) //for ITSavings based OnAbortTransaction ID
                                                                              , txtNumber.Text, txtAltNumber.Text, Convert.ToInt32(ddlCity.SelectedValue == "--Select--" ? "0" : ddlCity.SelectedValue), txtIssuer.Text, txtRemarks.Text, ext, chkhij, Convert.ToInt32(chkallEmp.Checked ? "1" : "0")));
                                }
                                else
                                {
                                    AlertMsg.MsgBox(Page, "Date Is Invalid", AlertMsg.MessageType.Error);
                                    //lblStatus.Text = "Date Is Invalid";
                                    //lblStatus.ForeColor = System.Drawing.Color.Red;
                                }
                            }
                        }
                        if (ID != 0)
                        {
                            AlertMsg.MsgBox(Page, "Saved Successfully", AlertMsg.MessageType.Success);
                            //lblStatus.Text = "Saved Successfully";
                            //lblStatus.ForeColor = System.Drawing.Color.Green;
                            gvEdit.Visible = false;
                            Accordion1.Visible = false;
                            btnUpdate.Visible = false;
                            btnProcess.Visible = false;
                            lblProgessdetails.Text = string.Empty;
                        }
                    }
                }
                catch (Exception ex)
                {
                    AlertMsg.MsgBox(Page, "Unable to update!", AlertMsg.MessageType.Error);
                    //    lblStatus.Text = "Unable to update!";
                    //lblStatus.ForeColor = System.Drawing.Color.Red;
                }
            }
        }
        protected void btnAddNew_Click(object sender, EventArgs e)
        {
            lblGroup.Visible = lblNewItems.Visible = ddlGroup.Visible = ddlNewItems.Visible = btnAddNew.Visible = false;
            int ResurceID = Convert.ToInt32(ddlNewItems.SelectedValue);
            if (ddlItems.Items.FindByText(ddlNewItems.SelectedItem.Text) == null)
            {
                AttendanceDAC.HR_InsNewSatuatoryItems(ResurceID);
                BindResourceTypes();
            }
            else
            {
                AlertMsg.MsgBox(Page, "Already Exist", AlertMsg.MessageType.Warning);
                //lblStatus.Text = "Already Exist";
                //lblStatus.ForeColor = System.Drawing.Color.Red;
            }
        }
        protected void ddlGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            int GroupID = Convert.ToInt32(ddlGroup.SelectedValue);
            DataSet ds = AttendanceDAC.PM_GroupWiseItems_IndentByService(GroupID);
            ddlNewItems.DataSource = ds;
            ddlNewItems.DataTextField = "item_desc";
            ddlNewItems.DataValueField = "item_id";
            ddlNewItems.DataBind();
            ddlNewItems.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        protected void gvFinalEdit_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int ID = Convert.ToInt32(e.CommandArgument);
            if (e.CommandName == "Edt")
            {
                try
                {
                    GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).Parent.Parent;
                    DropDownList ddlMach = (DropDownList)gvFinalEdit.Rows[row.RowIndex].FindControl("ddlMachinery");
                    DropDownList ddlCity = (DropDownList)gvFinalEdit.Rows[row.RowIndex].FindControl("ddlCity");
                    Label lblSRNID = (Label)gvFinalEdit.Rows[row.RowIndex].FindControl("lblSRNID");
                    Label lblESItemsID = (Label)gvFinalEdit.Rows[row.RowIndex].FindControl("lblESItemsID"); //for getiing lblESItemsID..
                    TextBox txtNumber = (TextBox)gvFinalEdit.Rows[row.RowIndex].FindControl("txtNumber");
                    TextBox txtAltNumber = (TextBox)gvFinalEdit.Rows[row.RowIndex].FindControl("txtAltNumber");
                    TextBox txtIssuer = (TextBox)gvFinalEdit.Rows[row.RowIndex].FindControl("txtIssuer");
                    TextBox txtRemarks = (TextBox)gvFinalEdit.Rows[row.RowIndex].FindControl("txtRemarks");
                    FileUpload ImgUpload = (FileUpload)gvFinalEdit.Rows[row.RowIndex].FindControl("ImgUpload");
                    Label lblImgExta = (Label)gvFinalEdit.Rows[row.RowIndex].FindControl("lblImgExta");
                    TextBox txtFrom = (TextBox)gvEdit.FindControl("txtVFrom");
                    TextBox txtTo = (TextBox)gvEdit.FindControl("txtVTo");
                    string filename = "", ext = string.Empty, path = "";
                    filename = ImgUpload.PostedFile.FileName;
                    if (filename != "")
                    {
                        ext = filename.Split('.')[filename.Split('.').Length - 1];
                    }
                    else
                    {
                        if (ID != 0)
                        {
                            ext = lblImgExta.Text;
                        }
                        else
                        {
                            ext = "";
                        }
                    }
                    if (txtFrom.Text != "" && txtTo.Text != "")
                    {
                        int chkhij = 0;
                        if (chkhijri.Checked)
                            chkhij = 1;
                        ID = HR_InsUpReconsolisedItemsBy_ID(Convert.ToInt32(lblESItemsID), ID, Convert.ToInt32(lblSRNID.Text), txtFrom.Text, txtTo.Text, Convert.ToInt32(Session["UserId"]), Convert.ToInt32(ddlMach.SelectedValue)//for saving the Based on ID
                            , txtNumber.Text, txtAltNumber.Text, Convert.ToInt32(ddlCity.SelectedValue == "--Select--" ? "0" : ddlCity.SelectedValue), txtIssuer.Text, txtRemarks.Text, ext, chkhij, Convert.ToInt32(chkallEmp.Checked ? "1" : "0"));
                        if (filename != "")
                        {
                            if (ID != 0)
                            {
                                path = Server.MapPath(".\\NRIDocs\\" + ID + "." + ext);
                                ImgUpload.PostedFile.SaveAs(path);
                            }
                        }
                        tblUnRecon.Visible = false;
                        tblReconciled.Visible = true;
                        trReconciled.Visible = true;
                        BindView();
                        tblFinalEdit.Visible = false;
                        tblEdit.Visible = false;
                        trEdit.Visible = false;
                        AlertMsg.MsgBox(Page, "Done!", AlertMsg.MessageType.Success);
                        //lblStatus.Text = "Done!";
                        //lblStatus.ForeColor = System.Drawing.Color.Green;
                    }
                    else
                    {
                        AlertMsg.MsgBox(Page, "Enter Valid Dates!", AlertMsg.MessageType.Warning);
                        //lblStatus.Text = "Enter Valid Dates!";
                        //lblStatus.ForeColor = System.Drawing.Color.Red;
                    }
                }
                catch
                {
                    AlertMsg.MsgBox(Page, "Unable to update!", AlertMsg.MessageType.Error);
                    //lblStatus.Text = "Unable to update!";
                    //lblStatus.ForeColor = System.Drawing.Color.Red;
                }
            }
        }
        int? WSID = null;
        int? DeptID = null;
        int? DesigID = null;
        int? RecType = null;
        int? ResourceID = null;
        protected void btnGo_Click(object sender, EventArgs e)
        {
            gvEdit.Visible = false;
            Accordion1.Visible = false;//for refresh problem by RT
            lblProgessdetails.Text = "";
            ddlItems_SelectedIndexChanged(sender, e);
            btnUpdate.Visible = false;
            btnProcess.Visible = false;
        }
        protected void btnRecSearch_Click(object sender, EventArgs e)
        {
            if (ddlRecItems.SelectedItem.Value != "0")
            {
                if (ddlRecWs.SelectedItem.Value != "0")
                {
                    WSID = Convert.ToInt32(ddlRecWs.SelectedItem.Value);
                }
                if (ddlRecDept.SelectedItem.Value != "0")
                {
                    DeptID = Convert.ToInt32(ddlRecDept.SelectedItem.Value);
                }
                RecType = Convert.ToInt32(rbTaxasion.SelectedItem.Value);
                if (ddlRecItems.SelectedItem.Value != "0")
                {
                    ResourceID = Convert.ToInt32(ddlRecItems.SelectedItem.Value);
                }
                DataSet dsReEmp = objAtt.GetEmpByWSDEptNatureForDoc(WSID, DeptID, DesigID, ResourceID, RecType);
                DataRow dr;
                dr = dsReEmp.Tables[0].NewRow();
                dr[0] = 0;
                dr[1] = "--All--";
                dsReEmp.Tables[0].Rows.InsertAt(dr, 0);
                ViewState["Machinery"] = dsReEmp;
                AllMechinaries = new ArrayList();
                foreach (DataRow drMech in dsReEmp.Tables[0].Rows)
                    AllMechinaries.Add(drMech["EmpId"].ToString());
                BindView();
            }
            else
            {
                AlertMsg.MsgBox(Page, "Select Document Type", AlertMsg.MessageType.Warning);
                //lblStatus.Text = "Select Document Type!";
                //lblStatus.ForeColor = System.Drawing.Color.Red;
            }
        }
        protected void btnExpactExporttoExcel_Click(object sender, EventArgs e)
        {
            if (ddlRecItems.SelectedItem.Value != "0")
            {
                if (Session["gvRecosiledDT"] != null)
                {
                    DataTable dtExpact = FetchExportToExcelData((DataTable)Session["gvRecosiledDT"]);
                    //dtExpact = (DataTable)Session["gvRecosiledDT"];
                    ExportFileUtil.ExportToExcel(dtExpact, "ExpactDocsEmpDetails");
                }
                else
                {
                    AlertMsg.MsgBox(Page, "No Employees in the Grid", AlertMsg.MessageType.Warning);
                }
            }
            else
            {
                AlertMsg.MsgBox(Page, "Select Document Type", AlertMsg.MessageType.Warning);
            }
        }
        public DataTable FetchExportToExcelData(DataTable dtpresentData)
        {   
            DataTable dtNewData = new DataTable();
            string[] newColToAdd = { "EmpName:Emp Name", "DOB:DOB", "Age:Age", "Design:Designation", "Category:Trades", "Categary:Worksite", "DeptNo:Department", "From:Valid From[dd/mm/yyyy]", "To:Valid UoTo[dd/mm/yyyy]", "Numeber:Id Number", "AltNumber:Sponsorship", "IssuePlace:Issue City", "Issuer:Issuer", "Status:Status" };
            for (int i = 0; i < newColToAdd.Count(); i++)
            {
                DataColumn dtcol = new DataColumn();
                dtNewData.Columns.Add(dtcol);
                dtNewData.Columns[i].ColumnName = newColToAdd[i].Split(':')[0].ToString();
                dtNewData.Columns[i].Caption = newColToAdd[i].Split(':')[1].ToString();

                /*DataColumn dtcol = new DataColumn();
                dtcol.ColumnName = newColToAdd[i].Split(':')[0].ToString();
                dtcol.DataType = System.Type.GetType("System.String");
                dtcol.Caption = newColToAdd[i].Split(':')[1].ToString();

                dtNewData.Columns.Add(dtcol);*/
            }
            for (int i = 0; i < dtpresentData.Rows.Count; i++)
            {
                dtNewData.Rows.Add(new string[] { dtpresentData.Rows[i]["EmpName"].ToString(), dtpresentData.Rows[i]["DOB"].ToString(),
                    dtpresentData.Rows[i]["Age"].ToString(), dtpresentData.Rows[i]["Design"].ToString(), dtpresentData.Rows[i]["Category"].ToString(),
                    GetWorkSite(dtpresentData.Rows[i]["Categary"].ToString()) , GetDepartment(dtpresentData.Rows[i]["DeptNo"].ToString()), dtpresentData.Rows[i]["From"].ToString(),
                dtpresentData.Rows[i]["To"].ToString(), dtpresentData.Rows[i]["Numeber"].ToString(), dtpresentData.Rows[i]["AltNumber"].ToString(),
                dtpresentData.Rows[i]["IssuePlace"].ToString(), dtpresentData.Rows[i]["Issuer"].ToString(), dtpresentData.Rows[i]["Status"].ToString()});
            }
            
            return dtNewData;
        }
        public void GetDepartments()
        {
            DataSet dsDept = objAtt.GetDepartments(0);
            ddlRecDept.DataSource = dsDept.Tables[0];
            ddlRecDept.DataTextField = "DeptName";
            ddlRecDept.DataValueField = "DepartmentUId";
            ddlRecDept.DataBind();
            ddlRecDept.Items.Insert(0, new ListItem("---ALL---", "0", true));
        }
        public void GetWorkSites()
        {
            DataSet dsWs = objAtt.GetWorkSiteByEmpID(Convert.ToInt32(Session["UserId"]), Convert.ToInt32(Session["CompanyID"]), Convert.ToInt32(Session["RoleId"]));
            ddlRecWs.DataSource = dsWs.Tables[0];
            ddlRecWs.DataTextField = "Site_Name";
            ddlRecWs.DataValueField = "Site_ID";
            ddlRecWs.DataBind();
            ddlRecWs.Items.Insert(0, new ListItem("---All---", "0", true));
        }
        public void BindDesignations()
        {
            DataSet ds = (DataSet)objAtt.GetDesignations();
            ddlRecDesg.DataSource = ds;
            ddlRecDesg.DataTextField = "Designation";
            ddlRecDesg.DataValueField = "DesigId";
            ddlRecDesg.DataBind();
            ddlRecDesg.Items.Insert(0, new ListItem("---ALL---", "0", true));
        }
        protected void gvFinalEdit_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    TextBox txtTemFrom = (TextBox)e.Row.FindControl("txtTempFrom");
                    AECLOGIC.ERP.COMMON.HijriGregDatePicker txtFrom = ((AECLOGIC.ERP.COMMON.HijriGregDatePicker)e.Row.FindControl("txtVFrom"));
                    txtFrom.setGregorianText(txtTemFrom.Text);
                    AECLOGIC.ERP.COMMON.HijriGregDatePicker txtTo = ((AECLOGIC.ERP.COMMON.HijriGregDatePicker)e.Row.FindControl("txtVTo"));
                    TextBox txtTemTo = (TextBox)e.Row.FindControl("txtTempTO");
                    txtTo.setGregorianText(txtTemTo.Text);
                    DateTime tempFromDate = DateTime.ParseExact(txtTemFrom.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    DateTime tempToDate = DateTime.ParseExact(txtTemTo.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    CultureInfo arabicCulture = new CultureInfo("ar-SA");
                    txtFrom.setHijriDateText(tempFromDate.ToString("dd/MM/yyyy", arabicCulture.DateTimeFormat));
                    txtTo.setHijriDateText(tempToDate.ToString("dd/MM/yyyy", arabicCulture.DateTimeFormat));
                }
            }
            catch (Exception ex) { }
        }
        public static DateTime ParseDateTime(string dateString)
        {
            DateTime dateTime;
            if (!DateTime.TryParse(dateString, CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out dateTime))
            {
                if (!DateTime.TryParse(dateString, CultureInfo.CurrentCulture, DateTimeStyles.AssumeUniversal, out dateTime))
                {
                    try
                    {
                        dateTime = DateTime.Parse(dateString, CultureInfo.InvariantCulture, DateTimeStyles.AssumeLocal);
                    }
                    catch (FormatException)
                    {
                        // Try to extract at least year from the string (the longest digits substring)
                        var yearMatch = Regex.Matches(dateString, @"\d{4}").Cast<Match>().FirstOrDefault();
                        if (yearMatch == null || string.IsNullOrWhiteSpace(yearMatch.Value))
                        {
                            throw;
                        }
                        // Only year really matters for Max and Min values of DateTime
                        var year = int.Parse(yearMatch.Value, CultureInfo.InvariantCulture);
                        // Try to determine what do we have (Min or Max value)
                        if (year == DateTime.MaxValue.Year)
                        {
                            dateTime = DateTime.MaxValue;
                        }
                        else
                        {
                            if (year == DateTime.MinValue.Year)
                            {
                                dateTime = DateTime.MinValue;
                            }
                            else
                            {
                                throw;
                            }
                        }
                    }
                }
            }
            dateTime = dateTime.ToUniversalTime();
            return dateTime;
        }
        protected void btnProcess_Click(object sender, EventArgs e)
        {
            string confirmValue = Request.Form["confirm_value"];
            if (confirmValue.Contains("Yes"))
            {
                int ID = Convert.ToInt32(Session["SRNIDs"]);
                int BillTrasID = Convert.ToInt32(lblgBillTransID.Text);
                SqlParameter[] parm = new SqlParameter[1];
                parm[0] = new SqlParameter("@BillTrID", BillTrasID);
                DataSet ds= SQLDBUtil.ExecuteDataset("sh_CheckPrepadiExpenses", parm);
                if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0) {
                    if (Convert.ToInt32(ds.Tables[0].Rows[0]["cnt"]) == 0) {
                        string msg=ds.Tables[1].Rows[0]["Mess"].ToString();
                        AlertMsg.MsgBox(Page,msg, AlertMsg.MessageType.Warning);
                        return;
                    }
                }

                AttendanceDAC.CompleteProcessExpatDocumentation(ID);
                AlertMsg.MsgBox(Page, "W/0 Closed Successfully", AlertMsg.MessageType.Success);
                //lblStatus.Text = "W/0 Closed Successfully";
                //lblStatus.ForeColor = System.Drawing.Color.Green;
                ddlItems_SelectedIndexChanged(sender, e);
                gvEdit.Visible = false;
                Accordion1.Visible = false;
                btnUpdate.Visible = false;
                btnProcess.Visible = false;
                lblProgessdetails.Text = string.Empty;
            }
            else
            {
                return;
            }
        }
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                int ID = 0;
                int SRNItemIDs = Convert.ToInt32(Session["SRNItemIDs"]); //for getting the SRNItemIDs by RT
                foreach (GridViewRow gvRow in gvEdit.Rows)
                {
                    Label lblSRNItemID = (Label)gvRow.FindControl("lblSRNItemID");
                    Label lblESItemsID = (Label)gvRow.FindControl("lblESItemsID");
                    if (SRNItemIDs == 0)
                    {
                        SRNItemIDs = Convert.ToInt32(lblSRNItemID.Text);
                    }
                    TextBox txtFrom = (TextBox)gvRow.FindControl("txtVFrom");
                    TextBox txtTo = (TextBox)gvRow.FindControl("txtVTo");
                    DropDownList ddlMach = (DropDownList)gvRow.FindControl("ddlMachinery");
                    DropDownList ddlCity = (DropDownList)gvRow.FindControl("ddlCity");
                    Label lblSRNID = (Label)gvRow.FindControl("lblSRNID");
                    TextBox txtNumber = (TextBox)gvRow.FindControl("txtNumber");
                    TextBox txtAltNumber = (TextBox)gvRow.FindControl("txtAltNumber");
                    TextBox txtIssuer = (TextBox)gvRow.FindControl("txtIssuer");
                    TextBox txtRemarks = (TextBox)gvRow.FindControl("txtRemarks");
                    FileUpload ImgUpload = (FileUpload)gvRow.FindControl("ImgUpload");
                    string ext = string.Empty, fname = string.Empty;
                    ext = "";
                    fname = "";
                    int chkhij = 0;
                    if (chkhijri.Checked)
                        chkhij = 1;
                    DateTime fromDateValue;
                    string F = txtFrom.Text;
                    string T = txtTo.Text;
                    if (txtFrom.Text != "" && txtTo.Text != "")
                    {
                        DateTime fromdate = CodeUtilHMS.ConvertToDate(txtFrom.Text, CodeUtilHMS.DateFormat.DayMonthYear);
                        DateTime todate = CodeUtilHMS.ConvertToDate(txtTo.Text, CodeUtilHMS.DateFormat.DayMonthYear);
                        if (fromdate > todate)
                        {
                            AlertMsg.MsgBox(Page, "To date Should be GreaterThan From date", AlertMsg.MessageType.Warning);
                            //lblStatus.Text = "To date Should be GreaterThan From date";
                            //lblStatus.ForeColor = System.Drawing.Color.Red;
                            return;
                        }
                    }
                    if (chkhijri.Checked)
                    {   
                        if (txtFrom.Text != "" || txtTo.Text != "")
                        {
                            try
                            {
                                CultureInfo arabicCulture = new CultureInfo("ar-SA");
                                var strFormat = new[] { "dd/MM/yyyy", "yyyy-MM-dd", "d/MM/yyy", "d/M/yyyy", "dd/M/yyyy" };
                                CultureInfo gregorianCulture = new CultureInfo("en-US");
                                string strFrom, strTo;    
                                strFrom = GetDateFromHijri(txtFrom.Text);
                                strTo = GetDateFromHijri(txtTo.Text);
                                if (strFrom != "")
                                    F = txtFrom.Text = strFrom;
                                if (strTo != "")
                                    T = txtTo.Text = strTo;
                                DateTime FromA = new DateTime();
                                DateTime ToA = new DateTime();
                                
                                if (strFrom == "" && strTo == "") // go for default arabic conversion, if both the dates are not there in DB
                                {
                                    if (txtFrom.Text == "" && txtTo.Text != "")
                                    {
                                        F = "";
                                        ToA = DateTime.ParseExact(txtTo.Text, strFormat, arabicCulture.DateTimeFormat, DateTimeStyles.AllowWhiteSpaces);
                                        txtTo.Text = ToA.ToString("dd/MM/yyyy");
                                    }
                                    else if (txtTo.Text == "" && txtFrom.Text != "")
                                    {
                                        T = "";
                                        FromA = DateTime.ParseExact(txtFrom.Text, strFormat, arabicCulture.DateTimeFormat, DateTimeStyles.AllowWhiteSpaces);
                                        txtFrom.Text = FromA.ToString("dd/MM/yyyy");
                                    }
                                    else if (txtFrom.Text != "" && txtTo.Text != "")
                                    {
                                        ToA = DateTime.ParseExact(txtTo.Text, strFormat, arabicCulture.DateTimeFormat, DateTimeStyles.AllowWhiteSpaces);
                                        FromA = DateTime.ParseExact(txtFrom.Text, strFormat, arabicCulture.DateTimeFormat, DateTimeStyles.AllowWhiteSpaces);
                                        txtTo.Text = ToA.ToString("dd/MM/yyyy");
                                        txtFrom.Text = FromA.ToString("dd/MM/yyyy");
                                    }
                                }
                                else
                                {
                                    if (strFrom != "" && strTo == "")
                                    {
                                        if (txtTo.Text != "")
                                        {
                                            ToA = DateTime.ParseExact(txtTo.Text, strFormat, arabicCulture.DateTimeFormat, DateTimeStyles.AllowWhiteSpaces);
                                            txtTo.Text = ToA.ToString("dd/MM/yyyy");
                                        }
                                        FromA = DateTime.ParseExact(txtFrom.Text, strFormat, gregorianCulture.DateTimeFormat, DateTimeStyles.AllowWhiteSpaces);
                                    }
                                    else if (strFrom == "" && strTo != "")
                                    {
                                        if (txtFrom.Text != "")
                                        {
                                            FromA = DateTime.ParseExact(txtFrom.Text, strFormat, arabicCulture.DateTimeFormat, DateTimeStyles.AllowWhiteSpaces);
                                            txtFrom.Text = FromA.ToString("dd/MM/yyyy");
                                        }
                                        ToA = DateTime.ParseExact(txtTo.Text, strFormat, gregorianCulture.DateTimeFormat, DateTimeStyles.AllowWhiteSpaces);
                                    }
                                    else if (strFrom != "" && strTo != "")
                                    {
                                        ToA = DateTime.ParseExact(txtTo.Text, strFormat, gregorianCulture.DateTimeFormat, DateTimeStyles.AllowWhiteSpaces);
                                        FromA = DateTime.ParseExact(txtFrom.Text, strFormat, gregorianCulture.DateTimeFormat, DateTimeStyles.AllowWhiteSpaces);
                                    }
                                }
                                F = FromA.ToString("dd/MM/yyyy");
                                T = ToA.ToString("dd/MM/yyyy");
                            }
                            catch
                            {
                                AlertMsg.MsgBox(Page, "Hijri Date Is Invalid", AlertMsg.MessageType.Error);
                                //lblStatus.Text = "Hijri Date Is Invalid";
                                //lblStatus.ForeColor = System.Drawing.Color.Red;
                                return;
                            }
                        }
                    }
                    var formats = new[] { "dd/MM/yyyy", "yyyy-MM-dd" };
                    if ((DateTime.TryParseExact(F, formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out fromDateValue) && DateTime.TryParseExact(T, formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out fromDateValue)) || (txtFrom.Text == "" || txtFrom.Text == string.Empty || txtTo.Text == "" || txtTo.Text == string.Empty))
                    {
                        // do for valid date
                        /*ID = Convert.ToInt32(AttendanceDAC.HR_InsUpReconsolisedItemsBy_ID(Convert.ToInt32(lblESItemsID.Text), SRNItemIDs, Convert.ToInt32(lblSRNID.Text), txtFrom.Text, txtTo.Text, Convert.ToInt32(Session["UserId"]), Convert.ToInt32(ddlMach.SelectedValue) //for ITSavings based OnAbortTransaction ID
                        , txtNumber.Text, txtAltNumber.Text, Convert.ToInt32(ddlCity.SelectedValue == "--Select--" ? "0" : ddlCity.SelectedValue), txtIssuer.Text, txtRemarks.Text, ext, chkhij));*/
                        ID = Convert.ToInt32(HR_InsUpReconsolisedItemsBy_ID(Convert.ToInt32(lblESItemsID.Text), SRNItemIDs, Convert.ToInt32(lblSRNID.Text), txtFrom.Text, txtTo.Text, Convert.ToInt32(Session["UserId"]), Convert.ToInt32(ddlMach.SelectedValue) //for ITSavings based OnAbortTransaction ID
                        , txtNumber.Text, txtAltNumber.Text, Convert.ToInt32(ddlCity.SelectedValue == "--Select--" ? "0" : ddlCity.SelectedValue), txtIssuer.Text, txtRemarks.Text, ext, chkhij, Convert.ToInt32(chkallEmp.Checked ? "1" : "0")));
                    }
                    else
                    {
                        if (txtFrom.Text != "" || txtFrom.Text != string.Empty || txtTo.Text != "" || txtTo.Text != string.Empty)
                        {
                            countfordate++;
                        }
                    }
                }
                if (countfordate > 0)
                {
                    AlertMsg.MsgBox(Page, "Some Records Not Saved Due to Invalid Date", AlertMsg.MessageType.Warning);
                    //lblStatus.Text = "Some Records Not Saved Due to Invalid Date";
                    //lblStatus.ForeColor = System.Drawing.Color.Red;
                }
                if (ID != 0 && countfordate == 0)
                {
                    AlertMsg.MsgBox(Page, "Saved Successfully", AlertMsg.MessageType.Success);
                    //lblStatus.Text = "Saved Successfully";
                    //lblStatus.ForeColor = System.Drawing.Color.Green;
                    ddlItems_SelectedIndexChanged(null, null); // added by pratap dt: 26 dec 2017
                    gvEdit.Visible = false;
                    Accordion1.Visible = false;
                    btnUpdate.Visible = false;
                    btnProcess.Visible = false;
                }
                lblProgessdetails.Text = string.Empty;
            }
            catch (Exception ex)
            {
            }
        }
        public static int HR_InsUpReconsolisedItemsBy_ID(int ID, int SRNItemID, int SRNID, string From, string To, int CreatedBy, int EmpID
                                                  , string Numeber, string AltNumber, int IssuePlace, string Issuer, string Remarks, string Ext, int chkhji, int chkShowAllEmps)
        {
            try
            {
                int retval;
                SqlParameter[] objParam = new SqlParameter[16];
                objParam[13] = new SqlParameter("@ID", ID);
                objParam[0] = new SqlParameter("@SRNItemID", SRNItemID);
                objParam[1] = new SqlParameter("@SRNID", SRNID);
                if (From == string.Empty)
                {
                    objParam[2] = new SqlParameter("@SFrom", DBNull.Value);
                }
                else
                { objParam[2] = new SqlParameter("@SFrom", From); }
                if (To == string.Empty)
                {
                    objParam[3] = new SqlParameter("@STo", DBNull.Value);
                }
                else
                {
                    objParam[3] = new SqlParameter("@STo", To);
                }
                objParam[4] = new SqlParameter("@CreatedBy", CreatedBy);
                objParam[5] = new SqlParameter("@EmpID", EmpID);
                objParam[6] = new SqlParameter("@Numeber", Numeber);
                objParam[7] = new SqlParameter("@AltNumber", AltNumber);
                objParam[8] = new SqlParameter("@IssuePlace", IssuePlace);
                objParam[9] = new SqlParameter("@Issuer", Issuer);
                objParam[10] = new SqlParameter("@Remarks", Remarks);
                objParam[11] = new SqlParameter("@Ext", Ext);
                objParam[12] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
                objParam[12].Direction = ParameterDirection.ReturnValue;
                objParam[14] = new SqlParameter("@chkhji", chkhji);
                objParam[15] = new SqlParameter("@chkShowAllEmps", chkShowAllEmps);
                SQLDBUtil.ExecuteNonQuery("HR_InsUpReconsolisedItems_RT_14_04_2016", objParam);
                retval = (int)objParam[12].Value;
                return retval;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void chkhijri_CheckedChanged(object sender, EventArgs e)
        {
            DataSet ds = null;
            if (chkhijri.Checked)
            {
                ds = HR_SRNIDbySRNItemID(Convert.ToInt32(Session["GRNItemID"]), 1);
            }
            else
            {
                ds = HR_SRNIDbySRNItemID(Convert.ToInt32(Session["GRNItemID"]), 0);
            }
            gvEdit.Visible = true;
            Accordion1.Visible = true;
            lblProgessdetails.Visible = true;
            gvEdit.DataSource = null;
            gvEdit.DataBind();
            GetDepartments();
            GetWorkSites();
            BindMechinaries();
            BindDesignations();
            Session["SRNItemIDs"] = ds.Tables[0].Rows[0]["SRNItemID"].ToString();
            Session["SRNIDs"] = ds.Tables[0].Rows[0]["SRNID"].ToString();
            DataTable datatable2 = new DataTable();
            if (Convert.ToInt32(ds.Tables[0].Rows[0]["Qty"]) > 0)
            {
                DateTime dt = Convert.ToDateTime(ds.Tables[0].Rows[0]["CreatedOn"]);
                string d = dt.Date.ToString("dd MMMM yyyy");
                ViewState["Datedet"] = ds.Tables[0];
                lblProgessdetails.Text = "Work Order No : " + ds.Tables[0].Rows[0]["POID"].ToString() + " , Work Order Date : " + d + " , No.Of Records : " + Convert.ToInt32(ds.Tables[0].Rows[0]["Qty"]).ToString() + " ;";
                ViewState["RecDataset"] = ds;
                DataTable datatable1 = (DataTable)ds.Tables[0];
                for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
                {
                    datatable2.Columns.Add(ds.Tables[0].Columns[i].ColumnName);
                }
                datatable2.Columns.Add("ID");  // T_HR_EmpStatututoryItems   table primary ID
                datatable2.Columns["ID"].DefaultValue = 0;
                for (int k = 0; k < Convert.ToInt32(ds.Tables[0].Rows[0]["Qty"]); k++)
                {
                    foreach (DataRow dr1 in datatable1.Rows)
                    {
                        object[] row = dr1.ItemArray;
                        datatable2.Rows.Add(row);
                    }
                }
                gvEdit.DataSource = datatable2;
                gvEdit.DataBind();
                foreach (GridViewRow gvRow in gvEdit.Rows)
                {
                    DropDownList ddlCity = (DropDownList)gvRow.Cells[9].FindControl("ddlCity");
                    DataSet dscity = objAtt.GetEmployeesByWSDCity();
                    ddlCity.DataSource = dscity;
                    ddlCity.DataTextField = "CItyName";
                    ddlCity.DataValueField = "CityID";
                    ddlCity.DataBind();
                    ddlCity.Items.Insert(0, new ListItem("--Select--"));
                }
                btnProcess.Visible = false;
            }
            else
            {
                int i = 0;
               /* foreach (GridViewRow gvRow in gvEdit.Rows)
                {
                    string txtTemFrom = DateTime.Now.Date.ToString("dd/MM/yyyy");
                    txtTemFrom = txtTemFrom.Replace('-', '/');
                    AECLOGIC.ERP.COMMON.HijriGregDatePicker txtFrom = ((AECLOGIC.ERP.COMMON.HijriGregDatePicker)gvRow.Cells[5].FindControl("txtVFrom"));
                    txtFrom.setGregorianText(txtTemFrom);
                    DateTime tempFromDate = DateTime.ParseExact(txtTemFrom, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    CultureInfo arabicCulture = new CultureInfo("ar-SA");
                    txtFrom.setHijriDateText(tempFromDate.ToString("dd/MM/yyyy", arabicCulture.DateTimeFormat));
                }*/
                datatable2 = (DataTable)ds.Tables[0];
                gvEdit.DataSource = datatable2;
                gvEdit.DataBind();
                DateTime dt = Convert.ToDateTime(ds.Tables[0].Rows[0]["CreatedOn"]);
                string d = dt.Date.ToString("dd MMMM yyyy");
                lblProgessdetails.Text = "Work Order No : " + ds.Tables[0].Rows[0]["POID"].ToString() + " ," + " Work Order Date : " + d + " , No.Of Records : " + ds.Tables[0].Rows.Count + " ;";
                foreach (GridViewRow gvRow in gvEdit.Rows)
                {
                    DropDownList ddlMachinery = (DropDownList)gvRow.Cells[4].FindControl("ddlMachinery");
                    ddlMachinery.SelectedValue = Convert.ToInt32(ds.Tables[0].Rows[i]["EmpID"]).ToString();
                    //Added by Rijwan for DDl City :- 19-04-2016
                    DropDownList ddlCity = (DropDownList)gvRow.Cells[9].FindControl("ddlCity");
                    //ddlCity.SelectedValue = Convert.ToInt32(ds.Tables[0].Rows[i]["IssuePlace"]).ToString();
                    DataSet dscity = objAtt.GetEmployeesByWSDCity();
                    ddlCity.DataSource = dscity;
                    ddlCity.DataTextField = "CItyName";
                    ddlCity.DataValueField = "CityID";
                    ddlCity.DataBind();
                    ddlCity.Items.Insert(0, new ListItem("--Select--"));
                    try
                    {
                        int count = Convert.ToInt32(ds.Tables[0].Rows[i]["IssuePlace"]);
                        if (count >= 0)
                        {
                            ddlCity.SelectedValue = Convert.ToInt32(ds.Tables[0].Rows[i]["IssuePlace"]).ToString();
                        }
                    }
                    catch { ddlCity.SelectedValue = "0"; }
                    TextBox txtNumber = (TextBox)gvRow.Cells[7].FindControl("txtNumber");
                    txtNumber.Text = ds.Tables[0].Rows[i]["Numeber"].ToString();
                    TextBox txtAltNumber = (TextBox)gvRow.Cells[8].FindControl("txtAltNumber");
                    txtAltNumber.Text = ds.Tables[0].Rows[i]["AltNumber"].ToString();
                    TextBox txtIssuer = (TextBox)gvRow.Cells[9].FindControl("txtIssuer");
                    txtIssuer.Text = ds.Tables[0].Rows[i]["Issuer"].ToString();
                    TextBox txtRemarks = (TextBox)gvRow.Cells[10].FindControl("txtRemarks");
                    txtRemarks.Text = ds.Tables[0].Rows[i]["Remarks"].ToString();
                    i++;
                }
                btnProcess.Visible = true;
            }
            tblReconciled.Visible = true;
            trReconciled.Visible = false;
            tblUnRecon.Visible = true;
            tblEdit.Visible = true;
            trEdit.Visible = true;
            PageTax.Visible = false;
            gvRecosiled.Visible = false;
            foreach (GridViewRow gvRow in gvEdit.Rows)
            {
                DropDownList ddlMachinery = (DropDownList)gvRow.FindControl("ddlMachinery");
                TextBox txtvfrom = (TextBox)gvRow.FindControl("txtVFrom");
                TextBox txtvTo = (TextBox)gvRow.FindControl("txtVTo");
                TextBox txtNumber = (TextBox)gvRow.FindControl("txtNumber");
                TextBox txtaltno = (TextBox)gvRow.FindControl("txtAltNumber");
                DropDownList ddlCity = (DropDownList)gvRow.FindControl("ddlCity");
                TextBox txtIssuer = (TextBox)gvRow.FindControl("txtIssuer");
                if (ddlMachinery.SelectedIndex == 0 || txtvfrom.Text == string.Empty || txtvTo.Text == string.Empty || txtNumber.Text == string.Empty || txtaltno.Text == string.Empty || ddlCity.SelectedIndex == 0 || txtIssuer.Text == string.Empty)
                {
                    btnProcess.Visible = false;
                }
                else
                {
                    btnProcess.Visible = true;
                }
            }
            /*if (chkhijri.Checked)
            {
                foreach (GridViewRow gvRow in gvEdit.Rows)
                {
                    string txtTemFrom = DateTime.Now.Date.ToString("dd/MM/yyyy");
                    txtTemFrom = txtTemFrom.Replace('-', '/');
                    AECLOGIC.ERP.COMMON.HijriGregDatePicker txtFrom = ((AECLOGIC.ERP.COMMON.HijriGregDatePicker)gvRow.Cells[5].FindControl("txtVFromHijriCal"));
                    txtFrom.setGregorianText(txtTemFrom);
                    DateTime tempFromDate = DateTime.ParseExact(txtTemFrom, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    CultureInfo arabicCulture = new CultureInfo("ar-SA");
                    txtFrom.setHijriDateText(tempFromDate.ToString("dd/MM/yyyy", arabicCulture.DateTimeFormat));
                    txtFrom.Visible = true;
                }
            }*/
            //btnUpdate.Visible = true;
        }
        protected void ddlMachinery_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddl_status = (DropDownList)sender;
            GridViewRow row = (GridViewRow)ddl_status.Parent.Parent;
            int idx = row.RowIndex;
            DropDownList ddlemp = (DropDownList)row.Cells[0].FindControl("ddlMachinery");
            int status = 0;
            foreach (GridViewRow gvRow in gvEdit.Rows)
            {
                DropDownList ddlempall = (DropDownList)gvRow.FindControl("ddlMachinery");
                if (ddlemp.SelectedValue == ddlempall.SelectedValue && gvRow.RowIndex != idx)
                {
                    status = 1;
                }
            }
            if (status == 1)
            {
                ddlemp.SelectedIndex = 0;
                AlertMsg.MsgBox(Page, "Employee Already Selected for this W/O", AlertMsg.MessageType.Warning);
                //lblStatus.Text = "Employee Already Selected for this W/O";
                //lblStatus.ForeColor = System.Drawing.Color.Red;
            }
            else
            {
                SqlParameter[] p = new SqlParameter[2];
                p[0] = new SqlParameter("@Empid", ddlemp.SelectedValue);
                p[1] = new SqlParameter("@Hijri", chkhijri.Checked);
                DataSet ds = SqlHelper.ExecuteDataset("USP_HMS_GetPreviousExpactdet", p);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    TextBox lblNumber = (TextBox)row.Cells[0].FindControl("txtNumber");
                    lblNumber.Text = ds.Tables[0].Rows[0]["numeber"].ToString();
                    DropDownList ddlcity = (DropDownList)row.Cells[0].FindControl("ddlCity");
                    if (Convert.ToInt32(ds.Tables[0].Rows[0]["IssuePlace"]) == 0 || ds.Tables[0].Rows[0]["IssuePlace"] == null)
                        ddlcity.SelectedIndex = 0;
                    else
                        ddlcity.SelectedValue = ds.Tables[0].Rows[0]["IssuePlace"].ToString();
                    TextBox txtAltNumber = (TextBox)row.Cells[0].FindControl("txtAltNumber");
                    txtAltNumber.Text = ds.Tables[0].Rows[0]["AltNumber"].ToString();
                    TextBox txtfrmdate = (TextBox)row.Cells[0].FindControl("txtVFrom");
                    txtfrmdate.Text = ds.Tables[0].Rows[0]["To"].ToString();
                    TextBox txtVTo = (TextBox)row.Cells[0].FindControl("txtVTo");
                    txtVTo.Text = ds.Tables[0].Rows[0]["AddTo"].ToString();
                    TextBox txtIssuer = (TextBox)row.Cells[0].FindControl("txtIssuer");
                    txtIssuer.Text = ds.Tables[0].Rows[0]["Issuer"].ToString();
                }
            }
        }
        protected void gvRecosiled_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string Details = "";
                Details = Details + "TransID : " + (e.Row.DataItem as DataRowView)["TransID"].ToString() + "\n PONAME : " + (e.Row.DataItem as DataRowView)["PONAME"].ToString() + "\n ApprovedBy : " + (e.Row.DataItem as DataRowView)["CheckedBy"].ToString();
                e.Row.Cells[15].ToolTip = Details;
            }
        }
        protected void chkallEmp_CheckedChanged(object sender, EventArgs e)
        {
            DataSet ds = null;
            if (chkhijri.Checked)
            {
                ds = HR_SRNIDbySRNItemID(Convert.ToInt32(Session["GRNItemID"]), 1);
            }
            else
            {
                ds = HR_SRNIDbySRNItemID(Convert.ToInt32(Session["GRNItemID"]), 0);
            }
            gvEdit.Visible = true;
            Accordion1.Visible = true;
            lblProgessdetails.Visible = true;
            gvEdit.DataSource = null;
            gvEdit.DataBind();
            GetDepartments();
            GetWorkSites();
            BindMechinaries();
            BindDesignations();
            Session["SRNItemIDs"] = ds.Tables[0].Rows[0]["SRNItemID"].ToString();
            Session["SRNIDs"] = ds.Tables[0].Rows[0]["SRNID"].ToString();
            DataTable datatable2 = new DataTable();
            if (Convert.ToInt32(ds.Tables[0].Rows[0]["Qty"]) > 0)
            {
                DateTime dt = Convert.ToDateTime(ds.Tables[0].Rows[0]["CreatedOn"]);
                string d = dt.Date.ToString("dd MMMM yyyy");
                ViewState["Datedet"] = ds.Tables[0];
                lblProgessdetails.Text = "Work Order No : " + ds.Tables[0].Rows[0]["POID"].ToString() + " , Work Order Date : " + d + " , No.Of Records : " + Convert.ToInt32(ds.Tables[0].Rows[0]["Qty"]).ToString() + " ;";
                ViewState["RecDataset"] = ds;
                DataTable datatable1 = (DataTable)ds.Tables[0];
                for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
                {
                    datatable2.Columns.Add(ds.Tables[0].Columns[i].ColumnName);
                }
                datatable2.Columns.Add("ID");  // T_HR_EmpStatututoryItems   table primary ID
                datatable2.Columns["ID"].DefaultValue = 0;
                for (int k = 0; k < Convert.ToInt32(ds.Tables[0].Rows[0]["Qty"]); k++)
                {
                    foreach (DataRow dr1 in datatable1.Rows)
                    {
                        object[] row = dr1.ItemArray;
                        datatable2.Rows.Add(row);
                    }
                }
                gvEdit.DataSource = datatable2;
                gvEdit.DataBind();
                foreach (GridViewRow gvRow in gvEdit.Rows)
                {
                    DropDownList ddlCity = (DropDownList)gvRow.Cells[9].FindControl("ddlCity");
                    DataSet dscity = objAtt.GetEmployeesByWSDCity();
                    ddlCity.DataSource = dscity;
                    ddlCity.DataTextField = "CItyName";
                    ddlCity.DataValueField = "CityID";
                    ddlCity.DataBind();
                    ddlCity.Items.Insert(0, new ListItem("--Select--"));
                }
                btnProcess.Visible = false;
            }
            else
            {
                int i = 0;
                foreach (GridViewRow gvRow in gvEdit.Rows)
                {
                    string txtTemFrom = DateTime.Now.Date.ToString("dd/MM/yyyy");
                    txtTemFrom = txtTemFrom.Replace('-', '/');
                    AECLOGIC.ERP.COMMON.HijriGregDatePicker txtFrom = ((AECLOGIC.ERP.COMMON.HijriGregDatePicker)gvRow.Cells[5].FindControl("txtVFrom"));
                    txtFrom.setGregorianText(txtTemFrom);
                    DateTime tempFromDate = DateTime.ParseExact(txtTemFrom, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    CultureInfo arabicCulture = new CultureInfo("ar-SA");
                    txtFrom.setHijriDateText(tempFromDate.ToString("dd/MM/yyyy", arabicCulture.DateTimeFormat));
                }
                datatable2 = (DataTable)ds.Tables[0];
                gvEdit.DataSource = datatable2;
                gvEdit.DataBind();
                DateTime dt = Convert.ToDateTime(ds.Tables[0].Rows[0]["CreatedOn"]);
                string d = dt.Date.ToString("dd MMMM yyyy");
                lblProgessdetails.Text = "Work Order No : " + ds.Tables[0].Rows[0]["POID"].ToString() + " ," + " Work Order Date : " + d + " , No.Of Records : " + ds.Tables[0].Rows.Count + " ;";
                foreach (GridViewRow gvRow in gvEdit.Rows)
                {
                    DropDownList ddlMachinery = (DropDownList)gvRow.Cells[4].FindControl("ddlMachinery");
                    ddlMachinery.SelectedValue = Convert.ToInt32(ds.Tables[0].Rows[i]["EmpID"]).ToString();
                    //Added by Rijwan for DDl City :- 19-04-2016
                    DropDownList ddlCity = (DropDownList)gvRow.Cells[9].FindControl("ddlCity");
                    DataSet dscity = objAtt.GetEmployeesByWSDCity();
                    ddlCity.DataSource = dscity;
                    ddlCity.DataTextField = "CItyName";
                    ddlCity.DataValueField = "CityID";
                    ddlCity.DataBind();
                    ddlCity.Items.Insert(0, new ListItem("--Select--"));
                    try
                    {
                        int count = Convert.ToInt32(ds.Tables[0].Rows[i]["IssuePlace"]);
                        if (count >= 0)
                        {
                            ddlCity.SelectedValue = Convert.ToInt32(ds.Tables[0].Rows[i]["IssuePlace"]).ToString();
                        }
                    }
                    catch { ddlCity.SelectedValue = "0"; }
                    TextBox txtNumber = (TextBox)gvRow.Cells[7].FindControl("txtNumber");
                    txtNumber.Text = ds.Tables[0].Rows[i]["Numeber"].ToString();
                    TextBox txtAltNumber = (TextBox)gvRow.Cells[8].FindControl("txtAltNumber");
                    txtAltNumber.Text = ds.Tables[0].Rows[i]["AltNumber"].ToString();
                    TextBox txtIssuer = (TextBox)gvRow.Cells[9].FindControl("txtIssuer");
                    txtIssuer.Text = ds.Tables[0].Rows[i]["Issuer"].ToString();
                    TextBox txtRemarks = (TextBox)gvRow.Cells[10].FindControl("txtRemarks");
                    txtRemarks.Text = ds.Tables[0].Rows[i]["Remarks"].ToString();
                    i++;
                }
                btnProcess.Visible = true;
            }
            tblReconciled.Visible = true;
            trReconciled.Visible = false;
            tblUnRecon.Visible = true;
            tblEdit.Visible = true;
            trEdit.Visible = true;
            PageTax.Visible = false;
            gvRecosiled.Visible = false;
        }
        protected void chkhijriPost_CheckedChanged(object sender, EventArgs e)
        {
            btnRecSearch_Click(sender, e);
        }
        protected void Unnamed_Click(object sender, EventArgs e)
        {
        }
        public bool ViewVisiblePrePaidExp(string BillTransID, string DetailEntered, string PrePaidExpAdjTransID)
        {
            bool status = false;
            if (BillTransID == "0")
                status = false;
            else if (PrePaidExpAdjTransID != "0")
                status = false;
            else if (DetailEntered == "True")
                status = true;
            return status;
        }
        public string GetHijriDate(string strEDate, string strDDate)
        {
            string strReturnHDate = string.Empty;
            if (strEDate != null && strEDate != "" && chkhijri.Checked)
            {
                DateTime dtFromDate = CodeUtilHMS.ConvertToDate(strEDate, CodeUtilHMS.DateFormat.DayMonthYear);// Convert.ToDateTime(strEDate);
                SqlParameter[] objParam = new SqlParameter[3];
                objParam[0] = new SqlParameter("@DMonth", dtFromDate.Month);
                objParam[1] = new SqlParameter("@DYear", dtFromDate.Year);
                objParam[2] = new SqlParameter("@DateForm", "H");

                DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetHijriDates", objParam);
               
                if(ds.Tables[0].Rows.Count > 0)
                {
                    string strDay = dtFromDate.Day.ToString();
                    
                    for (int i=0; i< ds.Tables[0].Rows.Count; i++)
                    {
                        string[] iary = ds.Tables[0].Rows[i]["EDays"].ToString().Split(',');
                        string[] iary1 = ds.Tables[0].Rows[i]["HDays"].ToString().Split(',');
                        int iindex = Array.IndexOf(iary, strDay);
                        if(iindex != -1)
                        {   
                            strReturnHDate = iary1[iindex] + "/" + ds.Tables[0].Rows[i]["HMonth"] + "/" + ds.Tables[0].Rows[i]["HYear"];
                            return strReturnHDate;
                        }
                    }
                }
                else
                {
                    //strReturnHDate = strDDate;
                    return strDDate;
                }
            }
            else if(strEDate != null && strEDate != "")
            {
                //strReturnHDate = strEDate;
                return strEDate;
            }
            return strReturnHDate;
        }
        public string GetDateFromHijri(string strHDate)
        {
            string strReturnEDate = string.Empty;
            if (strHDate != null && strHDate != "")
            {
                DateTime dtDate = CodeUtilHMS.ConvertToDate(strHDate, CodeUtilHMS.DateFormat.DayMonthYear);
                SqlParameter[] objParam = new SqlParameter[3];
                objParam[0] = new SqlParameter("@DMonth", dtDate.Month);
                objParam[1] = new SqlParameter("@DYear", dtDate.Year);
                objParam[2] = new SqlParameter("@DateForm", "E");

                DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetHijriDates", objParam);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    string strDay = dtDate.Day.ToString();

                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        string[] iary = ds.Tables[0].Rows[i]["HDays"].ToString().Split(',');
                        string[] iary1 = ds.Tables[0].Rows[i]["EDays"].ToString().Split(',');
                        int iindex = Array.IndexOf(iary, strDay);
                        if (iindex != -1)
                        {
                            strReturnEDate =  iary1[iindex] + "/" + ds.Tables[0].Rows[i]["EMonth"] + "/" + ds.Tables[0].Rows[i]["EYear"];
                            return strReturnEDate;
                        }
                    }
                }
            }
            return strReturnEDate;
        }
        public static DataSet HR_SRNIDbySRNItemID(int SRNItemID, int hijri)
        {
            try
            {
                SqlParameter[] objParam = new SqlParameter[2];
                objParam[0] = new SqlParameter("@SRNItemID", SRNItemID);
                objParam[1] = new SqlParameter("@Hijri", hijri);
                DataSet ds = SQLDBUtil.ExecuteDataset("HR_SRNIDbySRNItemID", objParam);
                return ds;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            gvEdit.DataSource = null;
            gvEdit.DataBind();
            SqlParameter[] objParam = new SqlParameter[5];
            objParam[0] = new SqlParameter("@SRNItemID", Session["SRNItemIDs"]);
            objParam[1] = new SqlParameter("@Hijri", 0);
            if (txtFromDay.Text != "")
            {
                DateTime Frmdt = CodeUtilHMS.ConvertToDate_ddMMMyyy(txtFromDay.Text.Trim(), CodeUtilHMS.DateFormat.ddMMMyyyy);
                objParam[2] = new SqlParameter("@FromDate", Frmdt);
            }
            else
                objParam[2] = new SqlParameter("@FromDate", DBNull.Value);
            if (txtToDay.Text != "")
            {
                DateTime Todt = CodeUtilHMS.ConvertToDate_ddMMMyyy(txtToDay.Text.Trim(), CodeUtilHMS.DateFormat.ddMMMyyyy);
                objParam[3] = new SqlParameter("@ToDate", Todt);
            }
            else
                objParam[3] = new SqlParameter("@ToDate", DBNull.Value);
            objParam[4] = new SqlParameter("@sponsor", txtSponsor.Text.Trim());
            DataSet ds = SQLDBUtil.ExecuteDataset("HR_SRNIDbySRNItemID", objParam);
            DataTable datatable2 = new DataTable();
            //if (Convert.ToInt32(ds.Tables[0].Rows[0]["Qty"]) > 0)
            //{
                DateTime dt = Convert.ToDateTime(ds.Tables[0].Rows[0]["CreatedOn"]);
                string chkemp = ds.Tables[0].Rows[0]["chkShowAllEmps"].ToString();
                if (chkemp != null)
                {
                if (chkemp == "0")
                    chkallEmp.Checked = false;
                else
                    chkallEmp.Checked = true;
                }
                string d = dt.Date.ToString("dd MMMM yyyy");
                lblProgessdetails.Text = "Work Order No : " + ds.Tables[0].Rows[0]["POID"].ToString() + " , Work Order Date : " + d + " , No.Of Records : " + Convert.ToInt32(ds.Tables[0].Rows[0]["Qty"]).ToString() + " ;";
                ViewState["RecDataset"] = ds;
                DataTable datatable1 = (DataTable)ds.Tables[0];
                for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
                {
                    datatable2.Columns.Add(ds.Tables[0].Columns[i].ColumnName);
                }
               // datatable2.Columns.Add("ID");  // T_HR_EmpStatututoryItems   table primary ID
               // datatable2.Columns["ID"].DefaultValue = 0;
                for (int k = 0; k < Convert.ToInt32(ds.Tables[0].Rows.Count); k++)
                {
                    foreach (DataRow dr1 in datatable1.Rows)
                    {
                        object[] row = dr1.ItemArray;
                        datatable2.Rows.Add(row);
                    }
                }
                gvEdit.DataSource = null;
                gvEdit.DataBind();
                gvEdit.DataSource = datatable2;
                gvEdit.DataBind();
                btnProcess.Visible = false;
               int pi = 0;
                foreach (GridViewRow gvRow in gvEdit.Rows)
                {
                    if (gvEdit.Rows.Count > pi)
                    {
                        DropDownList ddlMachinery = (DropDownList)gvRow.Cells[4].FindControl("ddlMachinery");
                        ddlMachinery.SelectedValue = Convert.ToInt32(ds.Tables[0].Rows[pi]["EmpID"]).ToString();
                        //Added by Rijwan for DDl City :- 19-04-2016
                        DropDownList ddlCity = (DropDownList)gvRow.Cells[9].FindControl("ddlCity");
                        //ddlCity.SelectedValue = Convert.ToInt32(ds.Tables[0].Rows[i]["IssuePlace"]).ToString();
                        DataSet dscity = objAtt.GetEmployeesByWSDCity();
                        ddlCity.DataSource = dscity;
                        ddlCity.DataTextField = "CItyName";
                        ddlCity.DataValueField = "CityID";
                        ddlCity.DataBind();
                        ddlCity.Items.Insert(0, new ListItem("--Select--"));
                        try
                        {
                            int count = Convert.ToInt32(ds.Tables[0].Rows[pi]["IssuePlace"]);
                            if (count >= 0)
                            {
                                ddlCity.SelectedValue = Convert.ToInt32(ds.Tables[0].Rows[pi]["IssuePlace"]).ToString();
                            }
                        }
                        catch { ddlCity.SelectedValue = "0"; }
                        TextBox txtNumber = (TextBox)gvRow.Cells[7].FindControl("txtNumber");
                        txtNumber.Text = ds.Tables[0].Rows[pi]["Numeber"].ToString();
                        TextBox txtAltNumber = (TextBox)gvRow.Cells[8].FindControl("txtAltNumber");
                        txtAltNumber.Text = ds.Tables[0].Rows[pi]["AltNumber"].ToString();
                        TextBox txtvfrom = (TextBox)gvRow.FindControl("txtVFrom");
                        TextBox txtvTo = (TextBox)gvRow.FindControl("txtVTo");
                        TextBox txtIssuer = (TextBox)gvRow.Cells[9].FindControl("txtIssuer");
                        txtIssuer.Text = ds.Tables[0].Rows[pi]["Issuer"].ToString();
                        TextBox txtRemarks = (TextBox)gvRow.Cells[10].FindControl("txtRemarks");
                        txtRemarks.Text = ds.Tables[0].Rows[pi]["Remarks"].ToString();
                    }
                    pi++;
                }
          //  }
        }
    }
}
 