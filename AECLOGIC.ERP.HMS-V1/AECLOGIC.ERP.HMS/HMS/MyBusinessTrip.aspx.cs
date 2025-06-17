using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AECLOGIC.HMS.BLL;
using System.Data;
using System.Data.SqlClient;
using Aeclogic.Common.DAL;
using System.IO;
using System.Configuration;
using System.Collections;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using AECLOGIC.ERP.COMMON;
using AECLOGIC.ERP.HMS.HRClasses;
namespace AECLOGIC.ERP.HMS
{
    public partial class MyBusinessTrip : AECLOGIC.ERP.COMMON.WebFormMaster
    {
        int Id = 1;
        int mid = 0;
        bool viewall;
        string menuname;
        string menuid;
        String MyString;
        string extension;
        bool Editable;
        static int WSID = 0;
        static char WSStatus = '1';
        static int Deptid = 0;
        static int siteid = 0;
        static int CompanyID = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["CompanyID"].ToString());
        HRCommon objHrCommon = new HRCommon();
        protected override void OnInit(EventArgs e)
        {
            ModuleID = 1;
            base.OnInit(e);
        }
        #region PendingRej
        void EmpReimbursementPendingRejPaging_ShowRowsClick(object sender, EventArgs e)
        {
            if (Convert.ToInt32(Request.QueryString["key"]) == 2)
                BindPagerPending();
            if (Convert.ToInt32(Request.QueryString["key"]) == 3)
            {
                BindPagerPending();
            }
            if (Convert.ToInt32(Request.QueryString["key"]) == 4)       //A/C Posted
            {
                BindPagerPending();
            }
        }
        void EmpReimbursementPendingRejPaging_FirstClick(object sender, EventArgs e)
        {
            if (Convert.ToInt32(Request.QueryString["key"]) == 2)
                BindPagerPending();
            if (Convert.ToInt32(Request.QueryString["key"]) == 3)
            {
                BindPagerPending();                                 //rejected
            }
            if (Convert.ToInt32(Request.QueryString["key"]) == 4)
            {
                BindPagerPending();                                 //A/C Posted
            }
        }
        void BindPagerPending()
        {
            int EmpID = 0;
        }
        #endregion PendingRej
        protected void Page_Load(object sender, EventArgs e)
        {
            Ajax.Utility.RegisterTypeForAjax(typeof(AjaxDAL));
            DataTable dtReimburseList = new DataTable();
            if (!IsPostBack)
            {
                Page.Form.Attributes.Add("enctype", "multipart/form-data");
                btnSubmit.Visible = false;
                DataSet dsAU = AttendanceDAC.GetAu();
                DataRow dr = dsAU.Tables[0].NewRow();
                dr["Au_Id"] = 0;
                dr["Au_Name"] = "---Select---";
                dsAU.Tables[0].Rows.InsertAt(dr, 0);
                dsAU.AcceptChanges();
                ArrayList alUnitIndexes = new ArrayList();
                foreach (DataRow row in dsAU.Tables[0].Rows)
                {
                    alUnitIndexes.Add(row["Au_Id"].ToString().Trim());
                }
                ViewState["alUnitIndexes"] = alUnitIndexes;
                ViewState["dsAU"] = dsAU;
                ViewState["PK"] = 0;
                ViewState["EmpID"] = 0;
                ViewState["ERID"] = 0;
                WSID = 0;
                        tblAdd.Visible = true;
                        BindItem();
                        dtReimburseList.Columns.Add(new DataColumn("ID", typeof(System.Int32)));
                        dtReimburseList.Columns.Add(new DataColumn("RItemID", typeof(System.Int32)));
                        dtReimburseList.Columns.Add(new DataColumn("RItem", typeof(System.String)));
                        dtReimburseList.Columns.Add(new DataColumn("EmpID", typeof(System.String)));
                        dtReimburseList.Columns.Add(new DataColumn("AUID", typeof(System.String)));
                        dtReimburseList.Columns.Add(new DataColumn("Purpose", typeof(System.String)));
                        dtReimburseList.Columns.Add(new DataColumn("Qty", typeof(System.Double)));
                        dtReimburseList.Columns.Add(new DataColumn("UnitRate", typeof(System.Double)));
                        dtReimburseList.Columns.Add(new DataColumn("Amount", typeof(System.Double)));
                        dtReimburseList.Columns.Add(new DataColumn("DateOfSpent", typeof(System.String)));
                        dtReimburseList.Columns.Add(new DataColumn("Proof", typeof(System.String)));
                        ViewState["ReimItems"] = dtReimburseList;
                        dtReimburseList = (DataTable)ViewState["ReimItems"];
                        gvRemiItems.DataSource = dtReimburseList;
                        gvRemiItems.DataBind();
                try
                {
                    ViewState["WSID"] = 0;
                    if (Convert.ToInt32(Session["RoleId"].ToString()) == 7)
                    {
                        try
                        {
                            DataSet ds = clViewCPRoles.HR_DailyAttStatus( Convert.ToInt32(Session["UserId"]));
                            ViewState["WSID"] = ds.Tables[0].Rows[0]["ID"].ToString();
                        }
                        catch { }
                    }
                }
                catch { }
            }
        }
        public DataSet GetAUDataSet()
        {
            return (DataSet)ViewState["dsAU"];
        }
        public int GetAUIndex(string AUID)
        {
            ArrayList alUnitIndexes = (ArrayList)ViewState["alUnitIndexes"];
            return alUnitIndexes.IndexOf(AUID.Trim());
        }
        public string DocNavigateUrl(string Proof)
        {
            string ReturnVal = "";
            string Value = Proof.Split('.')[Proof.Split('.').Length - 1];
            ReturnVal = "./EmpReimbureseProof/" + Proof;
            if (ReturnVal == "./EmpReimbureseProof/")
            {
                return null;
            }
            else
            {
                return "javascript:return window.open('" + ReturnVal + "', '_blank')";
            }
        }
        public string ViewItemsNavigateUrl(string ERID)
        {
            return "javascript:return window.open('ViewReimbursements.aspx?key=" + ERID + "', '_blank')";
        }
        public void BindItem()
        {
          DataSet ds = PayRollMgr.GetReimbursementItemsList();
            lstItems.DataSource = ds.Tables[0];
            lstItems.DataTextField = "Name";
            lstItems.DataValueField = "RMItemId";
            lstItems.DataBind();
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
                int[] Secarrary = lstItems.GetSelectedIndices();
                if (Secarrary.Length > 0)
                {
                    AttendanceDAC ADAC = new AttendanceDAC();
                    DataTable dtReimburseList = new DataTable();
                    DataRow dtRow;
                    dtReimburseList = (DataTable)ViewState["ReimItems"];
                    ListItem item = null;
                    string EmpID =  Convert.ToInt32(Session["UserId"]).ToString();
                    foreach (int indexItem in lstItems.GetSelectedIndices())
                    {
                        item = lstItems.Items[indexItem];
                        dtRow = dtReimburseList.NewRow();
                        if (ViewState["Id"] != null)
                        {
                            Id = Convert.ToInt32(ViewState["Id"]);
                        }
                        dtRow["ID"] = Id;
                        dtRow["RItemID"] = item.Value;
                        dtRow["RItem"] = item.Text;
                        dtRow["EmpID"] =  Convert.ToInt32(Session["UserId"]);
                        dtRow["Purpose"] = "";
                        dtRow["Qty"] = "1";
                        dtRow["Amount"] = "0";
                        dtRow["UnitRate"] = "0";
                        dtRow["DateOfSpent"] = DateTime.Now.ToString("dd MMM yyyy");
                        dtRow["Proof"] = "";
                        dtReimburseList.Rows.Add(dtRow);
                        item.Selected = false;
                        Id = Id + 1;
                        ViewState["Id"] = Id;
                    }
                    foreach (GridViewRow row in gvRemiItems.Rows)
                    {
                        TextBox txtpur = (TextBox)row.FindControl("txtPurpose");
                        TextBox txtqty = (TextBox)row.FindControl("txtQty");
                        TextBox txtAmount = (TextBox)row.FindControl("txtAmount");
                        TextBox txtrate = (TextBox)row.FindControl("txtrate");
                        Label lblItemId = (Label)row.FindControl("lblRItem");
                        TextBox txtSpentOn = (TextBox)row.FindControl("txtSpentOn");
                        DropDownList ddlunits = new DropDownList();
                        ddlunits = (DropDownList)row.FindControl("ddlunits");
                        FileUpload UploadProof = (FileUpload)row.FindControl("UploadProof");
                        DataRow[] drSelected = dtReimburseList.Select("RItem='" + lblItemId.Text + "'");
                        drSelected[0]["Purpose"] = txtpur.Text;
                        drSelected[0]["Qty"] = txtqty.Text;//
                        drSelected[0]["EmpID"] =  Convert.ToInt32(Session["UserId"]);
                        if (txtrate.Text == "") { Convert.ToDouble(txtrate.Text = "0"); }
                        drSelected[0]["UnitRate"] = Convert.ToDouble(txtrate.Text);
                        drSelected[0]["AUID"] = ddlunits.SelectedValue;
                        drSelected[0]["DateOfSpent"] = DateTime.Now.ToString("dd MMM yyyy");
                        drSelected[0]["Proof"] = UploadProof.FileName;
                    }
                    dtReimburseList.AcceptChanges();
                    ViewState["ReimItems"] = dtReimburseList;
                    dtReimburseList = (DataTable)ViewState["ReimItems"];
                    gvRemiItems.DataSource = dtReimburseList;
                    gvRemiItems.DataBind();
                    btnSubmit.Visible = true;
                }
                else
                {
                    AlertMsg.MsgBox(Page, "Select Item",AlertMsg.MessageType.Warning);
                }
        }
        protected void gvRemiItems_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Del")
            {
                int ID = Convert.ToInt32(e.CommandArgument);
                DataTable dtList = (DataTable)ViewState["ReimItems"];
                DataRow[] drSelected = null;
                drSelected = dtList.Select("ID='" + e.CommandArgument + "'");
                if (drSelected.Length > 0)
                    dtList.Rows.Remove(drSelected[0]);
                dtList.AcceptChanges();
                ViewState["ReimItems"] = dtList;
                gvRemiItems.DataSource = dtList;
                gvRemiItems.DataBind();
            }
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            DataSet dsRemItems = new DataSet("RemItemDataSet");
            DataTable dt = new DataTable("RemItemTable");
            dt.Columns.Add(new DataColumn("EmpID", typeof(System.Int32)));
            dt.Columns.Add(new DataColumn("RItemID", typeof(System.Int32)));
            dt.Columns.Add(new DataColumn("Uom", typeof(System.String)));
            dt.Columns.Add(new DataColumn("Purpose", typeof(System.String)));
            dt.Columns.Add(new DataColumn("Quantity", typeof(System.Double)));
            dt.Columns.Add(new DataColumn("Rate", typeof(System.Double)));
            dt.Columns.Add(new DataColumn("SpentOn", typeof(System.String)));
            dt.Columns.Add(new DataColumn("DOS", typeof(System.String)));
            dt.Columns.Add(new DataColumn("Proof", typeof(System.String)));
            dt.Columns.Add(new DataColumn("Status", typeof(System.Int32)));
            dsRemItems.Tables.Add(dt);
            foreach (GridViewRow gvRow in gvRemiItems.Rows)
            {
                Label lblEmpID = (Label)gvRow.Cells[2].FindControl("lblEmpID");
                Label lblRItem = (Label)gvRow.Cells[3].FindControl("lblRItemNo");
                DropDownList ddlunits = (DropDownList)gvRow.Cells[4].FindControl("ddlunits");
                int UnitOfMeasure = int.Parse(ddlunits.Text);
                if (UnitOfMeasure == 0)
                {
                    AlertMsg.MsgBox(Page, "Please select units of measure.!",AlertMsg.MessageType.Warning);
                    return;
                }
                TextBox txtRate = (TextBox)gvRow.Cells[5].FindControl("txtRate");
                double UnitRate;// = int.Parse(txtRate.Text);
                try
                {
                    UnitRate = double.Parse(txtRate.Text);
                }
                catch (Exception)
                {
                    AlertMsg.MsgBox(Page, "Unitrate can takes numbers only.!", AlertMsg.MessageType.Warning);
                    return;
                }
                if (txtRate.Text == "" || UnitRate <= 0)
                {
                    AlertMsg.MsgBox(Page, "Please enter proper unitrate.!", AlertMsg.MessageType.Warning);
                    return;
                }
                else
                {
                    UnitRate = double.Parse(txtRate.Text);
                }
                TextBox txtQty = (TextBox)gvRow.Cells[6].FindControl("txtQty");
                double Quantity;
                try
                {
                    Quantity = double.Parse(txtQty.Text);
                }
                catch (Exception)
                {
                    AlertMsg.MsgBox(Page, "Quantity can take numbers only.!", AlertMsg.MessageType.Warning);
                    return;
                }
                if (txtQty.Text == "" || Quantity <= 0)
                {
                    AlertMsg.MsgBox(Page, "Please enter proper quntity.!", AlertMsg.MessageType.Warning);
                    return;
                }
                else
                {
                    Quantity = double.Parse(txtQty.Text);
                }
                TextBox txtAmount = (TextBox)gvRow.Cells[7].FindControl("txtAmount");
                TextBox txtPurpose = (TextBox)gvRow.Cells[8].FindControl("txtPurpose");
                Label lblRItemID = (Label)gvRow.Cells[9].FindControl("lblRItemNo");
                TextBox txtSpentOn = (TextBox)gvRow.Cells[10].FindControl("txtSpentOn");
                try
                {
                    DateTime DateOfSpent = CodeUtilHMS.ConvertToDate_ddMMMyyy(txtSpentOn.Text.Trim(), CodeUtilHMS.DateFormat.ddMMMyyyy);
                }
                catch (Exception)
                {
                    AlertMsg.MsgBox(Page, "Please select proper date.!", AlertMsg.MessageType.Warning);
                    return;
                }
                FileUpload UploadProof = (FileUpload)gvRow.Cells[11].FindControl("UploadProof");
                String MyString = string.Empty;
                string extension = string.Empty;
                if (UploadProof.HasFile)
                {
                    DateTime MyDate = DateTime.Now;
                    MyString = MyDate.ToString("ddMMyyhhmmss");
                    extension = System.IO.Path.GetExtension(UploadProof.PostedFile.FileName).ToLower();
                }
                if (UploadProof.HasFile)
                {
                    string storePath = Server.MapPath("~") + "/" + "EmpReimbureseProof/";
                    if (!Directory.Exists(storePath))
                        Directory.CreateDirectory(storePath);
                    UploadProof.PostedFile.SaveAs(storePath + "/" + MyString + extension);
                }
                string Proof = MyString + extension;
                DataRow dr = dt.NewRow();
                dr["RItemID"] = Convert.ToInt32(lblRItemID.Text);
                dr["EmpID"] = Convert.ToInt32(lblEmpID.Text);
                ViewState["EmpView"] = Convert.ToInt32(lblEmpID.Text);
                dr["Uom"] = Convert.ToInt32(ddlunits.SelectedValue);
                dr["Rate"] = Convert.ToDouble(txtRate.Text);
                dr["Quantity"] = Convert.ToDouble(txtQty.Text);
                dr["Purpose"] = txtPurpose.Text.ToString();
                dr["SpentOn"] = txtSpentOn.Text;
                dr["DOS"] = DateTime.Now.ToString(ConfigurationManager.AppSettings["DateFormat"]);
                dr["Proof"] = Proof;
                dr["Status"] = 1;
                dt.Rows.Add(dr);
            }
            dsRemItems.AcceptChanges();
            try
            {
                DataSet ds = AttendanceDAC.HR_InsUpdRemItems(dsRemItems);
            }
            catch (Exception ex)
            {
                AlertMsg.MsgBox(Page, ex.Message.ToString(),AlertMsg.MessageType.Error);
                return;
            }
            gvRemiItems.Visible = false;
            tblAdd.Visible = false;
            if (Convert.ToBoolean(ViewState["ViewAll"]) == true)
            {
                Response.Redirect("EmpReimbursement.aspx?key=2");
            }
            else { Response.Redirect("EmpReimbursement.aspx?key=1"); }
        }
        protected void txtUnitrete_TextChanged(object sender, EventArgs e)
        {
            CalculateAmt();
        }
        protected void txtQuantity_TextChanged(object sender, EventArgs e)
        {
            CalculateAmt();
        }
        public void CalculateAmt()
        {
            foreach (GridViewRow gvRow in gvRemiItems.Rows)
            {
                TextBox txtRate = (TextBox)gvRow.Cells[5].FindControl("txtRate");
                double UnitRate;// = int.Parse(txtRate.Text);
                try
                {
                    UnitRate = double.Parse(txtRate.Text);
                }
                catch (Exception)
                {
                    AlertMsg.MsgBox(Page, "Unitrate can takes numbers only.!", AlertMsg.MessageType.Warning);
                    return;
                }
                if (txtRate.Text == "" || UnitRate <= 0)
                {
                    AlertMsg.MsgBox(Page, "Please enter proper unitrate.!", AlertMsg.MessageType.Warning);
                    return;
                }
                else
                {
                    UnitRate = double.Parse(txtRate.Text);
                }
                TextBox txtQty = (TextBox)gvRow.Cells[6].FindControl("txtQty");
                double Quantity;
                try
                {
                    Quantity = double.Parse(txtQty.Text);
                }
                catch (Exception)
                {
                    AlertMsg.MsgBox(Page, "Quantity can take numbers only.!", AlertMsg.MessageType.Warning);
                    return;
                }
                if (txtQty.Text == "" || Quantity <= 0)
                {
                    AlertMsg.MsgBox(Page, "Please enter proper quntity.!", AlertMsg.MessageType.Warning);
                    return;
                }
                else
                {
                    Quantity = double.Parse(txtQty.Text);
                }
                double Amount = UnitRate * Quantity;
                TextBox txtAmount = (TextBox)gvRow.Cells[7].FindControl("txtAmount");
                txtAmount.Text = Convert.ToString(Amount);
                ViewState["Amount"] = Amount;
            }
        }
        protected void gvView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {
                BindItem();
                tblAdd.Visible = true;
                int EmpID = Convert.ToInt32(e.CommandArgument);
              DataSet  ds = AttendanceDAC.HR_EmpReimburseAmtPayable(EmpID);
                gvRemiItems.DataSource = ds;
                gvRemiItems.DataBind();
                gvRemiItems.Visible = true;
                btnSubmit.Visible = true;
            }
            if (e.CommandName == "Approve")
            {
                int EmpID = Convert.ToInt32(e.CommandArgument);
                DataSet ds = AttendanceDAC.HR_EmpReimEmployeesByEmpID(EmpID);
                double Amount = Convert.ToDouble(ds.Tables[0].Rows[0]["Amount"]);
            }
        }
        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            tblAdd.Visible = false;
        }
        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            tblAdd.Visible = true;
        }
        // for department
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionList_Department(string prefixText, int count, string contextKey)
        {
            Deptid = 0;
            DataSet ds = AttendanceDAC.GoogleSerch_TaskAssignment_GetDaprtment(prefixText.Trim(), Deptid);
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
        //added for worksite
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionWorksiteList(string prefixText, int count, string contextKey)
        {
            DataSet ds = AttendanceDAC.HR_GetWorkSite_basedon_Wsid_googlesearch(prefixText.Trim(), WSID, WSStatus, CompanyID);
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
        protected void GetWorksite(object sender, EventArgs e)
        {
            CompanyID = Convert.ToInt32(Session["CompanyID"]);
            btnSubmit.Visible = false;
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionList_Employee(string prefixText, int count, string contextKey)
        {
            Deptid = 0;
            DataSet ds = AttendanceDAC.HMS_Service_DLL_Employee_By_WS_Dept_googlesearch(prefixText.Trim(), WSID, Deptid);//WSID
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
        protected void gvEdit_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Del")
            {
                int ID = Convert.ToInt32(e.CommandArgument);
                DataTable dtList = (DataTable)ViewState["ReimItems"];
                DataRow[] drSelected = null;
                drSelected = dtList.Select("ID='" + e.CommandArgument + "'");
                if (drSelected.Length > 0)
                    dtList.Rows.Remove(drSelected[0]);
                dtList.AcceptChanges();
                ViewState["ReimItems"] = dtList;
                gvRemiItems.DataSource = dtList;
                gvRemiItems.DataBind();
            }
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionFilterEmpList(string prefixText, int count, string contextKey)
        {
            DataSet ds = AttendanceDAC.GetEmployeesByTravel_googlesearch_Exp(prefixText.Trim());
            DataTable dt = ds.Tables[0];
            List<string> items = new List<string>(count);
            var rtval = new Dictionary<string, string>();
            foreach (DataRow row in dt.Rows)
            {
                string str = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(row["Name"].ToString(), row["ID"].ToString());
                items.Add(str);
            }
            return items.ToArray();
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionEmpTransferList(string prefixText, int count, string contextKey)
        {
            DataSet ds = AttendanceDAC.GetEmployeesByTravel_googlesearch_Exp(prefixText.Trim());
            DataTable dt = ds.Tables[0];
            List<string> items = new List<string>(count);
            var rtval = new Dictionary<string, string>();
            foreach (DataRow row in dt.Rows)
            {
                string str = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(row["Name"].ToString(), row["ID"].ToString());
                items.Add(str);
            }
            return items.ToArray();
        }
    }
}