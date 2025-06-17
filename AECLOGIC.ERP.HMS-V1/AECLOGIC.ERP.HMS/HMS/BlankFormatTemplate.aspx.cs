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
using System.IO;
namespace AECLOGIC.ERP.HMS.HMS
{
    public partial class BlankFormatTemplate : AECLOGIC.ERP.COMMON.WebFormMaster
    {
        int mid = 0;
        bool viewall;
        string menuname,Ext;
        string menuid;
        HRCommon objHrCommon = new HRCommon();
        protected override void OnInit(EventArgs e)
        {
            ModuleID = 1;
            base.OnInit(e);
            AdvancedLeaveAppOthPaging.FirstClick += new Paging.PageFirst(AdvancedLeaveAppOthPaging_FirstClick);
            AdvancedLeaveAppOthPaging.PreviousClick += new Paging.PagePrevious(AdvancedLeaveAppOthPaging_FirstClick);
            AdvancedLeaveAppOthPaging.NextClick += new Paging.PageNext(AdvancedLeaveAppOthPaging_FirstClick);
            AdvancedLeaveAppOthPaging.LastClick += new Paging.PageLast(AdvancedLeaveAppOthPaging_FirstClick);
            AdvancedLeaveAppOthPaging.ChangeClick += new Paging.PageChange(AdvancedLeaveAppOthPaging_FirstClick);
            AdvancedLeaveAppOthPaging.ShowRowsClick += new Paging.ShowRowsChange(AdvancedLeaveAppOthPaging_ShowRowsClick);
            AdvancedLeaveAppOthPaging.CurrentPage = 1;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Page.Form.Attributes.Add("enctype", "multipart/form-data");
                BindGrid();
                BindCategory();
                BindDocs();
                if (Request.QueryString.Count > 0)
                {
                    tblView.Visible = false;
                    tblAdd.Visible = true;
                }
                else
                {
                    tblView.Visible = true;
                    tblAdd.Visible = false;
                }
            }
        }
        void AdvancedLeaveAppOthPaging_ShowRowsClick(object sender, EventArgs e)
        {
            AdvancedLeaveAppOthPaging.CurrentPage = 1;
            BindGrid();
        }
        void AdvancedLeaveAppOthPaging_FirstClick(object sender, EventArgs e)
        {
            BindGrid();
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            AdvancedLeaveAppOthPaging.CurrentPage = 1;
            BindGrid();
        }
        protected void gvBlankDocs_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Dlt")
            {
                GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).Parent.Parent;
                Label lblDocID = (Label)row.FindControl("lblDocId");
                SqlParameter[] sqlParams = new SqlParameter[1];
                sqlParams[0] = new SqlParameter("@DocId", Convert.ToInt32(lblDocID.Text.Trim()));
                SQLDBUtil.ExecuteNonQuery("HMS_DeleteDocs", sqlParams);
                AlertMsg.MsgBox(Page, "Deleted");
                BindGrid();
            }
            if (e.CommandName == "Edt")
            {
                GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).Parent.Parent;
                Label lblDocID = (Label)row.FindControl("lblDocId");
                SqlParameter[] sqlParams = new SqlParameter[1];
                sqlParams[0] = new SqlParameter("@DocId", lblDocID.Text.Trim());
                Session["DocId"] = lblDocID.Text.Trim();
                DataSet ds = SQLDBUtil.ExecuteDataset("HMS_GETUPD_BlankDocs", sqlParams);
                if(ds.Tables[0].Rows.Count>0)
                {
                    tblAdd.Visible = true;
                    tblView.Visible = false;
                    ddlCategory.SelectedValue = ds.Tables[0].Rows[0]["Category"].ToString();
                    txtDocName.Text = ds.Tables[0].Rows[0]["Name"].ToString();
                    btnAdd.Text = "Update";
                }
            }
        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlCategory.SelectedItem.Value != string.Empty && txtDocName.Text != string.Empty)
                {
                    string filename = string.Empty, ext = string.Empty, path = string.Empty;
                    filename = fuUploadProof.PostedFile.FileName;
                    if (filename != string.Empty)
                    {
                        ext = filename.Split('.')[filename.Split('.').Length - 1];
                    }
                    else
                    {
                        AlertMsg.MsgBox(Page, "Please Upload Proof");
                        return;
                    }
                    SqlParameter[] sqlParams = new SqlParameter[6];
                    if (Session["DocId"] !=  string.Empty || Session["DocId"] != null)
                    {
                        sqlParams[5] = new SqlParameter("@DocId", Session["DocId"]);
                    }
                    else
                        sqlParams[5] = new SqlParameter("@DocId", SqlDbType.Int);
                    sqlParams[0] = new SqlParameter("@Name", txtDocName.Text.Trim());
                    sqlParams[1] = new SqlParameter("@Category", ddlCategory.SelectedValue);
                    sqlParams[2] = new SqlParameter("@CreatedBy",  Convert.ToInt32(Session["UserId"]));
                    sqlParams[3] = new SqlParameter("@Ext", ext);
                    sqlParams[4] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
                    sqlParams[4].Direction = ParameterDirection.ReturnValue;
                    SqlHelper.ExecuteNonQuery("HMS_INSUPD_BlankDocs", sqlParams);
                    int Id = Convert.ToInt32(sqlParams[4].Value);
                    if (Id != 0)
                    {
                        path = Server.MapPath("~\\hms\\BlankDocs\\" + Id + "." + ext);
                        fuUploadProof.PostedFile.SaveAs(path);
                        AlertMsg.MsgBox(Page, "Saved");
                        txtDocName.Text = String.Empty;
                        tblAdd.Visible = false;
                        tblView.Visible = true;
                        BindGrid();
                        BindCategory();
                        BindDocs();
                    }
                }
                else
                {
                    AlertMsg.MsgBox(Page, "Fill All Fields.");
                }
            }
            catch(Exception ex)
            {
                AlertMsg.MsgBox(Page, "Already Exist !");
            }
        }
        public void BindGrid()
        {
            objHrCommon.PageSize = AdvancedLeaveAppOthPaging.ShowRows;
            objHrCommon.CurrentPage = AdvancedLeaveAppOthPaging.CurrentPage;
            int DocId = 0,Category=0;
            if (ddlSCategory.SelectedValue !=  string.Empty)
                Category =Convert.ToInt32(ddlSCategory.SelectedValue);
            if (ddlDocName.SelectedValue !=  string.Empty)
                DocId = Convert.ToInt32(ddlDocName.SelectedValue);
            SqlParameter[] sqlParams = new SqlParameter[6];
            sqlParams[0] = new SqlParameter("@CurrentPage", objHrCommon.CurrentPage);
            sqlParams[1] = new SqlParameter("@PageSize", objHrCommon.PageSize);
            sqlParams[2] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
            sqlParams[2].Direction = ParameterDirection.ReturnValue;
            sqlParams[3] = new SqlParameter("@NoofRecords", System.Data.SqlDbType.Int);
            sqlParams[3].Direction = ParameterDirection.Output;
            sqlParams[4] = new SqlParameter("@CategoryId", Category);
            sqlParams[5] = new SqlParameter("@DocId", DocId);
            DataSet ds = SQLDBUtil.ExecuteDataset("HMS_Get_BlankDocs", sqlParams);
            objHrCommon.NoofRecords = (int)sqlParams[3].Value;
            objHrCommon.TotalPages = (int)sqlParams[2].Value;
            AdvancedLeaveAppOthPaging.Bind(objHrCommon.CurrentPage, objHrCommon.TotalPages, objHrCommon.NoofRecords, objHrCommon.PageSize);
            gvBlankDocs.DataSource = ds;
            gvBlankDocs.DataBind();
        }
        public void  BindCategory()
        {
            FIllObject.FillDropDown(ref ddlCategory,"HMS_Get_CategoryDocs");
            FIllObject.FillDropDown(ref ddlSCategory, "HMS_Get_CategoryDocs");
        }
        public void BindDocs()
        {
            SqlParameter[] sqlParams = new SqlParameter[1];
            sqlParams[0] = new SqlParameter("@CategoryId", ddlCategory.SelectedValue);
            FIllObject.FillDropDown(ref ddlDocName, "HMS_Get_BlankDocsSearch", sqlParams);
        }
        protected void btnSaveCategory_Click(object sender, EventArgs e)
        {
            SqlParameter[] sqlParams = new SqlParameter[1];
            sqlParams[0] = new SqlParameter("@CategoryName", txtCategory.Text.Trim());
            SQLDBUtil.ExecuteNonQuery("HMS_INS_CategoryDocs", sqlParams);
            AlertMsg.MsgBox(Page,"Category Saved");
            BindCategory();
            lblCategaory.Visible = false;
            txtCategory.Visible = false;
            btnSaveCategory.Visible = false;
            lnkAddCategory.Visible = false;
            txtCategory.Text = string.Empty;
        }
        public string DocNavigateUrl(string ProofID, string Ext)
        {
             string ReturnVal = string.Empty;
                ReturnVal = "~/hms/BlankDocs/" + ProofID + '.' + Ext;
                return ReturnVal;
        }
        public bool Visble(string Ext)
        {
            if (Ext != string.Empty)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        protected void lnkAddCategory_Click(object sender, EventArgs e)
        {
            lblCategaory.Visible = true;
            txtCategory.Visible = true;
            btnSaveCategory.Visible = true;
            lnkAddCategory.Visible = false;
        }
        protected void ddlSCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindDocs();
        }
        protected void lnkDownload_Click(object sender, EventArgs e)
        {
            GridViewRow row = ((LinkButton)sender).NamingContainer as GridViewRow;
            Label lblExt = (Label)row.FindControl("lblExt");
            Ext = lblExt.Text;
            string filePath = (sender as LinkButton).CommandArgument;
            if (File.Exists(Server.MapPath("~/hms/BlankDocs/" + filePath + "." + Ext)))
            {
                Response.ContentType = ContentType;
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(filePath));
                Response.TransmitFile(Server.MapPath("~/hms/BlankDocs/" + filePath + "." + Ext));
                Response.WriteFile(Server.MapPath("~/hms/BlankDocs/" + filePath + "." + Ext));
                Response.End();
            }
            else
            {
                AlertMsg.MsgBox(Page, "No Proof Uploaded");
            }
        }
    }
}