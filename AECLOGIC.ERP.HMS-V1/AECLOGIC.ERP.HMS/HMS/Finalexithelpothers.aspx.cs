using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using AECLOGIC.HMS.BLL;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using AECLOGIC.ERP.COMMON;
using AECLOGIC.ERP.HMS.HRClasses;
using Aeclogic.Common.DAL;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;

namespace AECLOGIC.ERP.HMSV1
{
    public partial class FinalexithelpothersV1 : AECLOGIC.ERP.COMMON.WebFormMaster
    {
        int mid = 0;
        bool viewall;
        string menuname;
        string menuid;
        bool Editable;
        static int Siteid;
        static int WSiteid;
        static int EDeptid = 0;


        AttendanceDAC objRights = new AttendanceDAC();
        MasterPage objmaster = new MasterPage();
        HRCommon objHrCommon = new HRCommon();

        static int ModID;
        static int Userid;
        protected override void OnInit(EventArgs e)
        {
            ModuleID = 1;
            base.OnInit(e);
            EmpReimbursementAprovedPaging.FirstClick += new Paging.PageFirst(EmpReimbursementAprovedPaging_FirstClick);
            EmpReimbursementAprovedPaging.PreviousClick += new Paging.PagePrevious(EmpReimbursementAprovedPaging_FirstClick);
            EmpReimbursementAprovedPaging.NextClick += new Paging.PageNext(EmpReimbursementAprovedPaging_FirstClick);
            EmpReimbursementAprovedPaging.LastClick += new Paging.PageLast(EmpReimbursementAprovedPaging_FirstClick);
            EmpReimbursementAprovedPaging.ChangeClick += new Paging.PageChange(EmpReimbursementAprovedPaging_FirstClick);
            EmpReimbursementAprovedPaging.ShowRowsClick += new Paging.ShowRowsChange(EmpReimbursementAprovedPaging_ShowRowsClick);
            EmpReimbursementAprovedPaging.CurrentPage = 1;
        }
        void EmpReimbursementAprovedPaging_ShowRowsClick(object sender, EventArgs e)
        {
            EmpReimbursementAprovedPaging.CurrentPage = 1;
            BindPager();
        }
        void EmpReimbursementAprovedPaging_FirstClick(object sender, EventArgs e)
        {
            // EmpReimbursementAprovedPaging.CurrentPage = 1;
            empid_hd.Value = "0";
            BindPager();
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            EmpReimbursementAprovedPaging.CurrentPage = 1;
            BindPager();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {


                Userid = Convert.ToInt32(Convert.ToInt32(Session["UserId"]).ToString());

                if (!IsPostBack)
                {
                    Page.Form.Attributes.Add("enctype", "multipart/form-data");
                    //BindPager();
                }
            }
            catch { }
        }

        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetEmpidList(string prefixText, int count, string contextKey)
        {
            DataSet ds = AttendanceDAC.GetGoogleSearch_by_EmpName_All(prefixText);
            DataTable dt = ds.Tables[0];
            List<string> items = new List<string>(count);
            var rtval = new Dictionary<string, string>();
            foreach (DataRow row in dt.Rows)
            {
                string str = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(row["Name"].ToString(), row["ID"].ToString());
                items.Add(str);
            }
            return items.ToArray(); ;
        }




        protected void btnsave_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime date; int result = 1, count = 0;
                foreach (GridViewRow gvr in gdvAttend.Rows)
                {


                    CheckBox chk = (CheckBox)gvr.FindControl("chkAll");
                    if (chk.Checked)
                    {
                        Label lblEmpID = (Label)gvr.FindControl("lblEmpID");
                        TextBox txtfrom = (TextBox)gvr.FindControl("txtExitFrom");
                        DropDownList ddlEmpDeAct = (DropDownList)gvr.FindControl("ddlEmpDeAct");
                        if (txtfrom.Text != String.Empty)
                        {
                            //if (Convert.ToDateTime(txtfrom.Text).Day < DateTime.Now.Day)
                            //{
                            //    AlertMsg.MsgBox(Page, "Back date not allowed!", AlertMsg.MessageType.Warning);
                            //    return;
                            //}
                            if (Convert.ToDateTime(DateTime.Now) > Convert.ToDateTime(txtfrom.Text))
                            {

                                AlertMsg.MsgBox(Page, "Back date not allowed!", AlertMsg.MessageType.Warning);
                                return;
                            }
                            date = CodeUtilHMS.ConvertToDate_ddMMMyyy(txtfrom.Text, CodeUtilHMS.DateFormat.ddMMMyyyy);
                        }
                        else
                        {
                            AlertMsg.MsgBox(Page, "Select Exit Date",AlertMsg.MessageType.Warning);
                            txtfrom.Focus();
                            return;
                        }
                        if (ddlEmpDeAct.SelectedIndex == 0)
                        {
                            AlertMsg.MsgBox(Page, "Please select Exit Type!", AlertMsg.MessageType.Warning);
                            return;

                        }
                        if (txtfrom.Text.Trim() != "")
                        {
                            if (Convert.ToInt32(Session["UserId"]) != 1 )
                            {
                                DateTime dt = DateTime.Now;
                                DateTime LeaveFrom = CodeUtil.ConvertToDate_ddMMMyyy(txtfrom.Text.Trim(), CodeUtil.DateFormat.ddMMMyyyy);

                                if ((LeaveFrom.Date - dt.Date).Days < 59)
                                {
                                    AlertMsg.MsgBox(Page, "Request should be Applied 60 Days prior to the Exit Date.", AlertMsg.MessageType.Warning);
                                    txtfrom.Text = string.Empty;
                                    return;

                                }
                            }

                        }
                        else
                        {
                            AlertMsg.MsgBox(Page, "Please enter Exit Date.", AlertMsg.MessageType.Warning);
                            return;
                        }
                        string filename = "", ext = string.Empty, path = "";
                        FileUpload fuc = (FileUpload)gvr.FindControl("fuUploadProof");


                        filename = fuc.PostedFile.FileName;


                        if (filename != "")
                        {
                            ext = filename.Split('.')[filename.Split('.').Length - 1];
                        }
                        else
                        {
                            ext = "";
                        }

                        TextBox txtreason = (TextBox)gvr.FindControl("txtReason");
                        SqlParameter[] parms = new SqlParameter[10];
                        parms[0] = new SqlParameter("@fCase", 1);
                        parms[1] = new SqlParameter("@Empid", lblEmpID.Text);
                        parms[2] = new SqlParameter("@FEFrom", date);
                        parms[3] = new SqlParameter("@Status", 1);
                        parms[4] = new SqlParameter("@isactive", 1);
                        parms[5] = new SqlParameter("@CreatedBy", Convert.ToInt32(Session["UserId"]));
                        parms[6] = new SqlParameter("@Reason", txtreason.Text.Trim());
                        parms[7] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
                        parms[7].Direction = ParameterDirection.ReturnValue;
                        parms[8] = new SqlParameter("@ext", ext);
                        parms[9] = new SqlParameter("@Rid", Convert.ToInt32(ddlEmpDeAct.SelectedItem.Value));
                        SQLDBUtil.ExecuteNonQuery("SP_th_FinalExit_Insert_Update_Delete_VIEW_Paging_Select", parms);
                        result = Convert.ToInt32(parms[7].Value);
                        //if (result == 2)
                        //{
                        //    count++;
                        //}

                        if (filename != "")
                        {
                            path = Server.MapPath("~\\hms\\EmpFinalProofs\\" + lblEmpID.Text + "." + ext);
                            fuc.PostedFile.SaveAs(path);
                        }
                    }


                }
                if (count > 0)
                {
                    AlertMsg.MsgBox(Page, "Some of records Already Exist !");
                }
                else
                {
                    BindPager();
                    AlertMsg.MsgBox(Page, "Saved");
                }
            }
            catch { }

        }
        void BindPager()
        {
            try
            {
                int Empid;
                if (txtempid.Text == "")
                {
                    empid_hd.Value = "";

                }
                Empid = Convert.ToInt32(empid_hd.Value == "" ? "0" : empid_hd.Value);
                objHrCommon.PageSize = EmpReimbursementAprovedPaging.ShowRows;
                objHrCommon.CurrentPage = EmpReimbursementAprovedPaging.CurrentPage;
                SqlParameter[] sqlParams = new SqlParameter[5];
                sqlParams[0] = new SqlParameter("@CurrentPage", objHrCommon.CurrentPage);
                sqlParams[1] = new SqlParameter("@PageSize", objHrCommon.PageSize);
                sqlParams[2] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
                sqlParams[2].Direction = ParameterDirection.ReturnValue;
                sqlParams[3] = new SqlParameter("@NoofRecords", System.Data.SqlDbType.Int);
                sqlParams[3].Direction = ParameterDirection.Output;
                sqlParams[4] = new SqlParameter("@EmpID", Empid);
                DataSet ds = SQLDBUtil.ExecuteDataset("sh_FinalExithelpOthers", sqlParams);
                objHrCommon.NoofRecords = (int)sqlParams[3].Value;
                objHrCommon.TotalPages = (int)sqlParams[2].Value;
                if (ds.Tables[0].Rows.Count > 0)
                {
                    gdvAttend.DataSource = ds.Tables[0];

                }
                else
                    gdvAttend.DataSource = null;
                gdvAttend.DataBind();
                EmpReimbursementAprovedPaging.Bind(objHrCommon.CurrentPage, objHrCommon.TotalPages, objHrCommon.NoofRecords, objHrCommon.PageSize);
            }
            catch { }
        }

        protected void ddlEmpDeAct_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {

                   // DataSet ds = new DataSet();
                    //DataTable dt = new DataTable();

                    
                     DropDownList ddlEmpDeAct = (DropDownList)e.Row.FindControl("ddlEmpDeAct");
                    if (ddlEmpDeAct != null)
                    {
                        FIllObject.FillDropDown(ref ddlEmpDeAct, "sh_EmployeeExitReasons");
                        ddlEmpDeAct.SelectedIndex = 1;
                    }
                }
            }
            catch (Exception ex)
            {
                clsErrorLog.HMSEventLog(ex, "FinalExitHelp", "ddlEmpDeAct_RowDataBound", "008");

            }
        }

        protected void gdvAttend_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {


                if (e.CommandName == "Upld")
                {
                    DateTime date; int result = 1, count = 0;
                    string filename = "", ext = string.Empty, path = "";
                    GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).Parent.Parent;
                    Label lblEmpID = (Label)gdvAttend.Rows[row.RowIndex].FindControl("lblEmpID");
                    TextBox txtfrom = (TextBox)gdvAttend.Rows[row.RowIndex].FindControl("txtExitFrom");
                    DropDownList ddlEmpDeAct = (DropDownList)gdvAttend.Rows[row.RowIndex].FindControl("ddlEmpDeAct");
                    if (txtfrom.Text != String.Empty)
                    {
                        if (Convert.ToDateTime(DateTime.Now) > Convert.ToDateTime(txtfrom.Text))
                        {

                            AlertMsg.MsgBox(Page, "Back date not allowed!", AlertMsg.MessageType.Warning);
                            return;
                        }
                        date = CodeUtilHMS.ConvertToDate_ddMMMyyy(txtfrom.Text, CodeUtilHMS.DateFormat.ddMMMyyyy);
                    }
                    else
                    {
                        AlertMsg.MsgBox(Page, "Select Exit Date");
                        txtfrom.Focus();
                        return;
                    }
                    if (ddlEmpDeAct.SelectedIndex == 0)
                    {
                        AlertMsg.MsgBox(Page, "Please select Exit Type!", AlertMsg.MessageType.Warning);
                        return;

                    }
                    TextBox txtReason = (TextBox)gdvAttend.Rows[row.RowIndex].FindControl("txtReason");
                    
                    int InvoiceCustHolderID = 0;

                    FileUpload fuc = (FileUpload)gdvAttend.Rows[row.RowIndex].FindControl("fuUploadProof");


                    filename = fuc.PostedFile.FileName;


                    if (filename != "")
                    {
                        ext = filename.Split('.')[filename.Split('.').Length - 1];
                    }
                    else
                    {
                        ext = "";
                    }

                    SqlParameter[] parms = new SqlParameter[10];
                    parms[0] = new SqlParameter("@fCase", 1);
                    parms[1] = new SqlParameter("@Empid", lblEmpID.Text);
                    parms[2] = new SqlParameter("@FEFrom", date);
                    parms[3] = new SqlParameter("@Status", 1);
                    parms[4] = new SqlParameter("@isactive", 1);
                    parms[5] = new SqlParameter("@CreatedBy", Convert.ToInt32(Session["UserId"]));
                    parms[6] = new SqlParameter("@Reason", txtReason.Text.Trim());
                    parms[7] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
                    parms[7].Direction = ParameterDirection.ReturnValue;
                    parms[8] = new SqlParameter("@ext", ext);
                    parms[9] = new SqlParameter("@Rid", Convert.ToInt32(ddlEmpDeAct.SelectedItem.Value));
                    SQLDBUtil.ExecuteNonQuery("SP_th_FinalExit_Insert_Update_Delete_VIEW_Paging_Select", parms);
                    result = Convert.ToInt32(parms[7].Value);

                    if (filename != "")
                    {
                        path = Server.MapPath("~\\hms\\EmpFinalProofs\\" + lblEmpID.Text + "." + ext);
                        fuc.PostedFile.SaveAs(path);
                    }

                    BindPager();
                    AlertMsg.MsgBox(Page, "Saved");
                }
            }
            catch { }
        }
    }
}