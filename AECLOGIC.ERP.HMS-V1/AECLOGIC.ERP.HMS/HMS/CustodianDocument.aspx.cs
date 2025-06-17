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

namespace AECLOGIC.ERP.HMS
{
    public partial class CustodianDocument : AECLOGIC.ERP.COMMON.WebFormMaster
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
            AdvancedLeaveAppOthPaging.FirstClick += new Paging.PageFirst(AdvancedLeaveAppOthPaging_FirstClick);
            AdvancedLeaveAppOthPaging.PreviousClick += new Paging.PagePrevious(AdvancedLeaveAppOthPaging_FirstClick);
            AdvancedLeaveAppOthPaging.NextClick += new Paging.PageNext(AdvancedLeaveAppOthPaging_FirstClick);
            AdvancedLeaveAppOthPaging.LastClick += new Paging.PageLast(AdvancedLeaveAppOthPaging_FirstClick);
            AdvancedLeaveAppOthPaging.ChangeClick += new Paging.PageChange(AdvancedLeaveAppOthPaging_FirstClick);
            AdvancedLeaveAppOthPaging.ShowRowsClick += new Paging.ShowRowsChange(AdvancedLeaveAppOthPaging_ShowRowsClick);
            AdvancedLeaveAppOthPaging.CurrentPage = 1;
        }
        void AdvancedLeaveAppOthPaging_ShowRowsClick(object sender, EventArgs e)
        {
            AdvancedLeaveAppOthPaging.CurrentPage = 1;
            BindDataGrid();
        }
        void AdvancedLeaveAppOthPaging_FirstClick(object sender, EventArgs e)
        {
            BindDataGrid();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
          
            if (!IsPostBack)
            {
                Page.Form.Attributes.Add("enctype", "multipart/form-data");
                GetParentMenuId();
                BindDataGrid();
                ViewState["DocID"] = 0;
            }
            if (Request.QueryString.Count > 0)
            {
                if (Request.QueryString["id"] == "1")
                {
                    tbladd.Visible = false;
                    tblList.Visible = true;
                }
                else
                {
                    tblList.Visible = false;
                    tbladd.Visible = true;
                    txtDocName.Focus();
                }
            }
        }

        public int GetParentMenuId()
        {
            string URL = Request.Url.Segments[Request.Url.Segments.Length - 1] + Request.Url.Query;
            int RoleId = Convert.ToInt32(Session["RoleId"].ToString());
            int ModuleId = ModuleID;
            DataSet ds = AttendanceDAC.GetAllowed(RoleId, ModuleId, URL);
            int MenuId = 0;
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                MenuId = Convert.ToInt32(ds.Tables[0].Rows[0]["Under"]);
                btnsave.Enabled = (bool)ds.Tables[0].Rows[0]["Editable"];
              
            }

            return MenuId;
        }
        public void BindDataGrid()
        {
            bool status;


            if (rbShowActive.Checked)
                status = true;
              else
                status = false;

            objHrCommon.PageSize = AdvancedLeaveAppOthPaging.ShowRows;
            objHrCommon.CurrentPage = AdvancedLeaveAppOthPaging.CurrentPage;
             
            SqlParameter[] sqlParams = new SqlParameter[5];
            sqlParams[0] = new SqlParameter("@CurrentPage", objHrCommon.CurrentPage);
            sqlParams[1] = new SqlParameter("@PageSize", objHrCommon.PageSize);
            sqlParams[2] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
            sqlParams[2].Direction = ParameterDirection.ReturnValue;
            sqlParams[3] = new SqlParameter("@NoofRecords", System.Data.SqlDbType.Int);
            sqlParams[3].Direction = ParameterDirection.Output;
            sqlParams[4] = new SqlParameter("@status", status);

            DataSet ds = SQLDBUtil.ExecuteDataset("HMS_CustodianDocsList1", sqlParams);
            objHrCommon.NoofRecords = (int)sqlParams[3].Value;
            objHrCommon.TotalPages = (int)sqlParams[2].Value;
            gvDocs.DataSource = ds;
            gvDocs.DataBind();
            AdvancedLeaveAppOthPaging.Bind(objHrCommon.CurrentPage, objHrCommon.TotalPages, objHrCommon.NoofRecords, objHrCommon.PageSize);
             
          
        }

        protected void btnsave_Click(object sender, EventArgs e)
        {
            string filename = "", ext = string.Empty, path = "";
            try
            {
                filename = fuUploadProof.PostedFile.FileName;


                if (filename != "")
                {
                    ext = filename.Split('.')[filename.Split('.').Length - 1];
                }
                else
                {
                    if (ViewState["Ext"] != null)
                    {
                        if (ViewState["Ext"].ToString() != "")
                        {
                            ext = ViewState["Ext"].ToString();
                        }
                    }
                    else
                    {
                        ext = "";
                    }
                }
                int DocID;
                if (ViewState["DocID"].ToString() != "0")
                {
                    DocID = Convert.ToInt32(ViewState["DocID"].ToString());
                }
                else
                {
                    DocID = 0;
                }
                string ActName = txtDocName.Text;
                int EmpID = Convert.ToInt32( Convert.ToInt32(Session["UserId"]).ToString());
                DocID = Convert.ToInt32(AttendanceDAC.InsUpdDocs(DocID, ActName, txtProcedure.Text, EmpID, ext));
                if (filename != "")
                {
                    path = Server.MapPath("~\\HMS\\CustodianDocs\\" + DocID + "." + ext);
                    fuUploadProof.PostedFile.SaveAs(path);
                }
                AlertMsg.MsgBox(Page, "Done");

                tbladd.Visible = false;
                tblList.Visible = true;
                AdvancedLeaveAppOthPaging.CurrentPage = 1;
                BindDataGrid();

            }
            catch
            {

            }
        }

        protected void gvDocs_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int DocID = Convert.ToInt32(e.CommandArgument);
            if (e.CommandName == "Edt")
            {
                BindDetails(DocID);
            }
            if (e.CommandName == "Del")
            {
                try
                {
                    bool status;
                    if (rbShowActive.Checked)
                        status = false;
                    else
                        status = true;
                    SqlParameter[] parm = new SqlParameter[2];
                    parm[0] = new SqlParameter("@DocID", DocID);
                    parm[1] = new SqlParameter("@Status", status);
                    SQLDBUtil.ExecuteNonQuery("sh_DltCustodianDocs", parm);
                    if(status)
                    AlertMsg.MsgBox(Page, "Activated !");
                    else
                        AlertMsg.MsgBox(Page, "Deactivated !");

                    BindDataGrid();
                }
                catch (Exception DelDesig)
                {
                    AlertMsg.MsgBox(Page, DelDesig.Message.ToString(),AlertMsg.MessageType.Error);
                }
            }
        }
        public string GetText()
        {

            if (rbShowActive.Checked)
            {
                return "Deactivate";
            }
            else
            {
                return "Active";
            }
        }

        protected void rbShow_CheckedChanged(object sender, EventArgs e)
        {
            //if (rbShowActive.Checked)
            //    StatusSearch = true;
            //else
            //    StatusSearch = false;
            //EmployeBind(objHrCommon);


            AdvancedLeaveAppOthPaging.CurrentPage = 1;
            BindDataGrid();

        }
        public void BindDetails(int DocID)
        {
            tbladd.Visible = true;
            tblList.Visible = false;
             
            DataSet ds = AttendanceDAC.GetCustodianDocsDetails(DocID);
            ViewState["DocID"] = ds.Tables[0].Rows[0]["DocID"].ToString();
            txtDocName.Text = ds.Tables[0].Rows[0]["DocumentName"].ToString();
            txtProcedure.Text = ds.Tables[0].Rows[0]["DocProcedure"].ToString();
            ViewState["Ext"] = ds.Tables[0].Rows[0]["Ext"].ToString();
            btnsave.Enabled = true;
        }

        public string DocNavigateUrl(string ProofID, string Ext)
        {
            string ReturnVal = "";
            ReturnVal = "~/HMS/CustodianDocs/" + ProofID + '.' + Ext;
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
    }
}