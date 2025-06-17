using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccessLayer;
using AECLOGIC.ERP.COMMON;
using Aeclogic.Common.DAL;
using System.Data.SqlClient;
using AECLOGIC.HMS.BLL;
using System.Data;
using System.Collections;
namespace AECLOGIC.ERP.HMS
{
    public partial class CustodianDocs : AECLOGIC.ERP.COMMON.WebFormMaster
    {
        int mid = 0;
        bool viewall;
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
        protected void Page_Load(object sender, EventArgs e)
        {
            lblStatus.Text = String.Empty;
            if (!IsPostBack)
            {
                Page.Form.Attributes.Add("enctype", "multipart/form-data");
                BindDocDDL();
                tblAllView.Visible = true;
                tblEmpView.Visible = false;
                GetSites();
                BindDataGrid();
                ViewState["DocID"] = 0;
                lblname.Visible = false;
            }
        }
        private void BindDocDDL()
        {
            DataSet ds = SQLDBUtil.ExecuteDataset("sh_GetCustodianDocs");
            ddldoctype.DataSource = ds.Tables[0];
            ddldoctype.DataTextField = "DocumentName";
            ddldoctype.DataValueField = "DocId";
            ddldoctype.DataBind();
            ddldoctype.Items.Insert(0, new ListItem("--Select--", "0"));
            if (ddldoctype.Items.Count > 0)
                ddldoctype.SelectedIndex = 1;
        }
        void EmpListPaging_ShowRowsClick(object sender, EventArgs e)
        {
            EmpListPaging.CurrentPage = 1;
            BindDataGrid();
        }
        void EmpListPaging_FirstClick(object sender, EventArgs e)
        {
            BindDataGrid();
        }
        public void BindDataGrid()
        {
            if (txtempid.Text == "")
                ddlEmpid_hid.Value = string.Empty;
            if (txtSWorkSite.Text == "")
                ddlWorkSite_hid.Value = string.Empty;
            objHrCommon.PageSize = EmpListPaging.ShowRows;
            objHrCommon.CurrentPage = EmpListPaging.CurrentPage;
            int WSID = Convert.ToInt32(ddlWorkSite_hid.Value == "" ? "0" : ddlWorkSite_hid.Value);
            int Empid = Convert.ToInt32(ddlEmpid_hid.Value == "" ? "0" : ddlEmpid_hid.Value);
            // int Empid =  Convert.ToInt32(Session["UserId"]);
            int doctype = Convert.ToInt32(ddldoctype.SelectedValue);
            int custodystatus = Convert.ToInt32(RadioButtonList1.SelectedValue);
            DataSet ds = AttendanceDAC.HR_EmpListForCustoDocs_Paging(objHrCommon, Empid, 0, doctype, custodystatus);
            if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0)
            {
                gvEmpList.DataSource = ds;
            }
            else
            {
                gvEmpList.EmptyDataText = "No Records Found";
            }
            gvEmpList.DataBind();
            EmpListPaging.Bind(objHrCommon.CurrentPage, objHrCommon.TotalPages, objHrCommon.NoofRecords, objHrCommon.PageSize);
            if (txtempid.Text == "")
                ddlEmpid_hid.Value = string.Empty;
            if (txtSWorkSite.Text == "")
                ddlWorkSite_hid.Value = string.Empty;
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionListEmp(string prefixText, int count, string contextKey)
        {
            DataSet ds = AttendanceDAC.HR_GoogleSearchEmp(prefixText);
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
        public static string[] GetCompletionList(string prefixText, int count, string contextKey)
        {
            DataSet ds = AttendanceDAC.GetWorkSiteLeaveActive(prefixText);
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
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            EmpListPaging.CurrentPage = 1;
            BindDataGrid();
        }
        protected void gvEmpList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            ViewState["EmpId"] = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).Parent.Parent;
            lblname.Visible = true;
            Label name = (Label)gvEmpList.Rows[row.RowIndex].FindControl("lblEmpName");
            lblname.Text = ViewState["EmpId"].ToString() + name.Text;
            if (e.CommandName == "Sel")
            {
                tblAllView.Visible = false;
                tblEmpView.Visible = true;
                BindEmployees();
                GetDocs();
            }
            if (e.CommandName == "Del")
            {
            }
        }
        public void GetDocs()
        {
            DataSet ds = AttendanceDAC.GetEmpProofs(Convert.ToInt32(ViewState["EmpId"]));
            GvEmpDocs.DataSource = ds.Tables[0];
            GvEmpDocs.DataBind();
        }
        public DataSet BindEmployees()
        {
            try
            {
                int SiteID = 0;
                DataSet DsSiteID = AttendanceDAC.GetSiteIDByEmpid(Convert.ToInt32(ViewState["EmpId"]));
                if (DsSiteID.Tables.Count > 0)
                {
                    if (DsSiteID.Tables[0].Rows.Count > 0)
                    {
                        SiteID = Convert.ToInt32(DsSiteID.Tables[0].Rows[0]["SiteID"].ToString());
                    }
                    else
                        SiteID = 0;
                }
                return BindCustodyHolder(SiteID);
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected void GvEmpDocs_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Upld")
                {
                    string filename = "", ext = string.Empty, path = "";
                    GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).Parent.Parent;
                    DropDownList grdddlworksites = (DropDownList)GvEmpDocs.Rows[row.RowIndex].FindControl("grdddlworksites");
                    DropDownList grdddlHeads = (DropDownList)GvEmpDocs.Rows[row.RowIndex].FindControl("grdddlHeads");
                    DropDownList grdNewddlHeads = (DropDownList)GvEmpDocs.Rows[row.RowIndex].FindControl("grdNewddlHeads");
                    int InvoiceCustHolderID = 0;
                    if (grdddlHeads.Visible == true)
                    {
                        InvoiceCustHolderID = Convert.ToInt32(grdddlHeads.SelectedItem.Value);
                    }
                    if (grdNewddlHeads.Visible == true)
                    {
                        InvoiceCustHolderID = Convert.ToInt32(grdNewddlHeads.SelectedItem.Value);
                    }
                    Label lblActID = (Label)GvEmpDocs.Rows[row.RowIndex].FindControl("lblDocID");
                    FileUpload fuc = (FileUpload)GvEmpDocs.Rows[row.RowIndex].FindControl("fuUploadProof");
                    Label lblProofID = (Label)GvEmpDocs.Rows[row.RowIndex].FindControl("lblProofID");
                    filename = fuc.PostedFile.FileName;
                    if (filename != "")
                    {
                        ext = filename.Split('.')[filename.Split('.').Length - 1];
                    }
                    else
                    {
                        if (lblProofID.Text != "")
                        {
                            ext = lblProofID.Text;
                        }
                        else
                        {
                            ext = "";
                        }
                    }
                    int ProofID = 0;
                    if (filename != "")
                    {
                        ProofID = AttendanceDAC.HMS_InsUpdEMpProofs(ProofID, Convert.ToInt32(lblActID.Text), Convert.ToInt32(Session["UserId"]), Convert.ToInt32(ViewState["EmpId"]), ext, Convert.ToInt32(grdddlworksites.SelectedItem.Value), InvoiceCustHolderID);
                        if (ProofID != 0)
                        {
                            path = Server.MapPath("~\\hms\\EmpCustodianProofs\\" + ProofID + "." + ext);
                            fuc.PostedFile.SaveAs(path);
                            // AlertMsg.MsgBox(Page, "Documents HandedOver..!");
                            lblStatus.Text = "Documents HandedOver..!";
                            lblStatus.ForeColor = System.Drawing.Color.Green;
                        }
                    }
                    else
                    {
                        AlertMsg.MsgBox(Page, "Please Upload Proof", AlertMsg.MessageType.Warning);
                        lblStatus.Text = "Please Upload Proof";
                        lblStatus.ForeColor = System.Drawing.Color.Red;
                        return;
                    }
                    //}
                    GetDocs();
                }
            }
            catch { }
        }
        protected void grdddlworksites_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList grdddlworksites = (DropDownList)sender;
            GridViewRow rownum = (GridViewRow)grdddlworksites.NamingContainer;
            int SitedID = Convert.ToInt32(grdddlworksites.SelectedItem.Value);
            DropDownList grdddlHeads = (DropDownList)rownum.FindControl("grdddlHeads");
            DropDownList grdNewddlHeads = (DropDownList)rownum.FindControl("grdNewddlHeads");
            grdNewddlHeads.Visible = true;
            grdddlHeads.Visible = false;
            DataSet DsEmployees = AttendanceDAC.GetInvCustodyHolder(SitedID);
            ViewState["RetHeads"] = DsEmployees;
            if (DsEmployees.Tables[0].Rows.Count > 0)
            {
                grdNewddlHeads.DataSource = DsEmployees;
                grdNewddlHeads.DataTextField = "name";
                grdNewddlHeads.DataValueField = "EmpId";
                grdNewddlHeads.DataBind();
                grdNewddlHeads.Items.Insert(0, new ListItem("---Select---", "0"));
            }
        }
        public DataSet BindSites()
        {
            return (DataSet)ViewState["RetSites"];
        }
        public DataSet BindHeads()
        {
            RetHeads = (DataSet)ViewState["RetHeads"];
            return RetHeads;
        }
        public static ArrayList alSites = new ArrayList();
        public static ArrayList alHeads = new ArrayList();
        public DataSet RetSites = new DataSet();
        public DataSet RetHeads = new DataSet();
        public DataSet GetSites()
        {
            try
            {
                int CompanyID = Convert.ToInt32(Session["CompanyID"].ToString());
                DataSet ds = AttendanceDAC.GetWorkSites(CompanyID);
                if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0)
                {
                    //ddlWorkSite.DataSource = ds.Tables[0];
                    //ddlWorkSite.DataTextField = "Site_Name";
                    //ddlWorkSite.DataValueField = "Site_ID";
                    //ddlWorkSite.DataBind();
                    //ddlWorkSite.Items.Insert(0, new ListItem("---Select---", "0"));
                }
                DataRow drRole;
                drRole = ds.Tables[0].NewRow();
                drRole[0] = 0;
                drRole[1] = "--Select--";
                ds.Tables[0].Rows.InsertAt(drRole, 0);
                ViewState["RetSites"] = ds;
                alSites = new ArrayList();
                foreach (DataRow dr in ds.Tables[0].Rows)
                    alSites.Add(dr["Site_ID"].ToString());
                return RetSites;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public int GetSiteIndex(string Value)
        {
            if (Value != string.Empty && Value != "")
            {
                BindCustodyHolder(Convert.ToInt32(Value));
            }
            return alSites.IndexOf(Value);
        }
        private DataSet BindCustodyHolder(int SiteID)
        {
            DataSet ds = AttendanceDAC.GetInvCustodyHolder(SiteID);
            if (ds != null)
            {
                ViewState["RetHeads"] = ds;
            }
            alHeads = new ArrayList();
            foreach (DataRow dr in ds.Tables[0].Rows)
                alHeads.Add(dr["EmpId"].ToString());
            return RetHeads;
        }
        public int GetInvHolderIndex(string Value)
        {
            if (Value != "0")
            {
                return alHeads.IndexOf(Value);
            }
            else
            {
                return 0;
            }
        }
        public string DocNavigateUrl(string ProofID, string Ext)
        {
            string ReturnVal = "";
            ReturnVal = "~/hms/EmpCustodianProofs/" + ProofID + '.' + Ext;
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
        protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindDataGrid();
        }
        protected void ddldoctype_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindDataGrid();
        }
    }
}