using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using AECLOGIC.HMS.BLL;
using AECLOGIC.ERP.COMMON;
using System.Data.SqlClient;
using Aeclogic.Common.DAL;
namespace AECLOGIC.ERP.HMS
{
    public partial class CriteriaofEmpfor_Evaluation : AECLOGIC.ERP.COMMON.WebFormMaster
    {
        #region Variables
        int mid = 0;
        bool viewall, Editable;
        string menuname;
        string menuid;
        //private GridSort objSort;
        //DataSet dsEvaluation;
        //int CompanyID = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["CompanyID"]);
        HRCommon objHrCommon = new HRCommon();
        #endregion Variables

        #region Events
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

           
            EmpListPaging.Visible = false;
            if (!IsPostBack)
            {
                GetParentMenuId();
                if (Request.QueryString.Count > 0)
                {
                    int id = Convert.ToInt32(Request.QueryString["key"]);
                    if (id == 1)
                    {
                        btnSubmit.Text = "Save";
                        Newvieew.Visible = true;
                        EditView.Visible = false;
                    }
                    else
                    {
                        Newvieew.Visible = false;
                        EditView.Visible = true;
                        BindPager();
                    }
                }
                else
                {
                    Newvieew.Visible = false;
                    EditView.Visible = true;
                    BindPager();
                }

            }

        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            EmpListPaging.CurrentPage = 1;
            EmployeBind(objHrCommon);
        }

        // Insert OR Update
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                int id = 0;
                if (btnSubmit.Text == "Save")
                    id = 1;
                else
                    id = 2;

                SqlParameter[] parms = new SqlParameter[9];
                parms[0] = new SqlParameter("@Criteria", TxtCrt.Text.Trim());
                parms[1] = new SqlParameter("@id", id);
                parms[2] = new SqlParameter("@CurrentPage", objHrCommon.CurrentPage);
                parms[3] = new SqlParameter("@PageSize", objHrCommon.PageSize);
                parms[4] = new SqlParameter("@NoofRecords", 3);
                parms[4].Direction = ParameterDirection.Output;
                parms[5] = new SqlParameter();
                parms[5].Direction = ParameterDirection.ReturnValue;
                parms[6] = new SqlParameter("@CriteriaID", CrtID.Text.Trim());
                parms[7] = new SqlParameter("@WeightagePercent", txtPercentage.Text.Trim());

                if (chkMandatry.Checked)
                    parms[8] = new SqlParameter("@Optional", 1);
                else
                    parms[8] = new SqlParameter("@Optional", 0);

                int Output = SqlHelper.ExecuteNonQuery("USP_HMS_CriteriaofEmpfor_Evaluation_Insert_Update_Delete", parms);
                if (Output == 1)
                {
                    AlertMsg.MsgBox(Page, "Done.! ");
                    EmployeBind(objHrCommon);
                    EditView.Visible = true;
                    Newvieew.Visible = false;
                }
                else if (Output == 2)
                {
                    AlertMsg.MsgBox(Page, "Already Exists.!");
                }
                else if (Output == 3)
                {
                    AlertMsg.MsgBox(Page, "Already Exists.!");
                    EditView.Visible = true;
                    Newvieew.Visible = false;
                }
                else if (Output != 1 && Output != 2 && Output != 3)
                {
                    AlertMsg.MsgBox(Page, "Already Exists.!");
                }
            }
            catch (Exception Cat)
            {
                AlertMsg.MsgBox(Page, Cat.Message.ToString(),AlertMsg.MessageType.Error);
            }
            if (TxtCrt.Text == "")
            {
                AlertMsg.MsgBox(Page, "criteria is Required");
            }
            if (txtPercentage.Text == "")
            {
                AlertMsg.MsgBox(Page, "Enter Percentage");
            }

        }
        // On Edit displaying control details to text box
        protected void gvEV_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int ID = Convert.ToInt32(e.CommandArgument);

                if (e.CommandName == "Edt")
                {
                    BindDetails(ID);
                    btnSubmit.Text = "Update";
                }
              else if (e.CommandName == "Del")
                {
                    try
                    {
                        bool status;
                        if (rbShowActive.Checked)
                            status = false;
                        else
                            status = true;
                        SqlParameter[] parm = new SqlParameter[2];
                        parm[0] = new SqlParameter("@CriteriaID", ID);
                        parm[1] = new SqlParameter("@Status", status);
                        SQLDBUtil.ExecuteNonQuery("sh_DltCriteriaEvaluation", parm);
                        if (status)
                            AlertMsg.MsgBox(Page, "Activated !");
                        else
                            AlertMsg.MsgBox(Page, "Deactivated !");

                        BindPager();
                    }
                    catch (Exception DelDesig)
                    {
                        AlertMsg.MsgBox(Page, DelDesig.Message.ToString(),AlertMsg.MessageType.Error);
                    }
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
        protected bool GetStatus(string str)
        {
            if (str == "1")
                return true;
            else
                return false;
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
        #endregion Events

        #region Methods
        public void BindPager()
        {

            objHrCommon.PageSize = EmpListPaging.CurrentPage;
            objHrCommon.CurrentPage = EmpListPaging.ShowRows;
            EmployeBind(objHrCommon);
        }
        protected void rbShow_CheckedChanged(object sender, EventArgs e)
        {
            BindPager();
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
        // Search
        void EmployeBind(HRCommon objHrCommon)
        {
            try
            {
                bool status;


                if (rbShowActive.Checked)
                    status = true;
                else
                    status = false;

                objHrCommon.PageSize = EmpListPaging.ShowRows;
                objHrCommon.CurrentPage = EmpListPaging.CurrentPage;

                SqlParameter[] parms = new SqlParameter[10];
                if (TxtScrteria.Text != string.Empty)
                    parms[0] = new SqlParameter("@Criteria", TxtScrteria.Text.Trim());
                else
                    parms[0] = new SqlParameter("@Criteria", DBNull.Value);
                parms[1] = new SqlParameter("@id", 5);
                parms[2] = new SqlParameter("@CurrentPage", objHrCommon.CurrentPage);
                parms[3] = new SqlParameter("@PageSize", objHrCommon.PageSize);
                parms[4] = new SqlParameter("@NoofRecords", 3);
                parms[4].Direction = ParameterDirection.Output;
                parms[5] = new SqlParameter();
                parms[5].Direction = ParameterDirection.ReturnValue;
                if (txSCrtID.Text != string.Empty)
                    parms[6] = new SqlParameter("@CriteriaID", txSCrtID.Text.Trim());
                else
                    parms[6] = new SqlParameter("@CriteriaID", DBNull.Value);
                parms[7] = new SqlParameter("@WeightagePercent", DBNull.Value);
                parms[8] = new SqlParameter("@Optional", DBNull.Value);
                parms[9] = new SqlParameter("@status", status);
                DataSet ds = SqlHelper.ExecuteDataset("USP_HMS_CriteriaofEmpfor_Evaluation_Insert_Update_Delete", parms);
                objHrCommon.NoofRecords = (int)parms[4].Value;
                objHrCommon.TotalPages = (int)parms[5].Value;
                //TextBox txtUserInfo = (TextBox)gvEV.FindControl("yourControlName");
                CheckBox chk = (CheckBox)gvEV.FindControl("chkselect");

                if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ViewState["Dataset"] = ds;
                    gvEV.DataSource = ds;
                    gvEV.DataBind();
                   
                }
                else
                {
                    gvEV.DataSource = null;
                    gvEV.DataBind();
                }
                EmpListPaging.Visible = false;
                EmpListPaging.Bind(objHrCommon.CurrentPage, objHrCommon.TotalPages, objHrCommon.NoofRecords, objHrCommon.PageSize);

            }
            catch (Exception e)
            {
                throw e;
            }
        }
        // Menu
        public int GetParentMenuId()
        {
            string URL = Request.Url.Segments[Request.Url.Segments.Length - 1] + Request.Url.Query;
            int RoleId = Convert.ToInt32(Session["RoleId"]);
            int ModuleId = ModuleID; ;
            DataSet ds = AttendanceDAC.GetAllowed(RoleId, ModuleId, URL);
            int MenuId = 0;
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                MenuId = Convert.ToInt32(ds.Tables[0].Rows[0]["Under"]);
                Editable = (bool)ds.Tables[0].Rows[0]["Editable"];
                btnSubmit.Enabled = Convert.ToBoolean(ds.Tables[0].Rows[0]["Editable"]);
                btnSubmit.Enabled = Editable;
            }
            return MenuId;
        }

        // Retriving Details Based on Primary Key ID
        public void BindDetails(int ID)
        {
            try
            {
                objHrCommon.PageSize = EmpListPaging.ShowRows;
                objHrCommon.CurrentPage = EmpListPaging.CurrentPage;
                bool status;
                if (rbShowActive.Checked)
                    status = true;
                else
                    status = false;


                SqlParameter[] parms = new SqlParameter[10];
                parms[0] = new SqlParameter("@Criteria", string.Empty);
                parms[1] = new SqlParameter("@id", 5);
                parms[2] = new SqlParameter("@CurrentPage", objHrCommon.CurrentPage);
                parms[3] = new SqlParameter("@PageSize", objHrCommon.PageSize);
                parms[4] = new SqlParameter("@NoofRecords", 3);
                parms[4].Direction = ParameterDirection.Output;
                parms[5] = new SqlParameter();
                parms[5].Direction = ParameterDirection.ReturnValue;
                parms[6] = new SqlParameter("@CriteriaID", ID);
                parms[7] = new SqlParameter("@WeightagePercent", DBNull.Value);
                parms[8] = new SqlParameter("@Optional", DBNull.Value);
                parms[9] = new SqlParameter("@status", status);


                DataSet ds = SqlHelper.ExecuteDataset("USP_HMS_CriteriaofEmpfor_Evaluation_Insert_Update_Delete", parms);
                objHrCommon.NoofRecords = (int)parms[4].Value;
                objHrCommon.TotalPages = (int)parms[5].Value;

                if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0)
                {
                    EditView.Visible = false;
                    Newvieew.Visible = true;
                    CrtID.Text = ds.Tables[0].Rows[0]["CriteriaID"].ToString();
                    TxtCrt.Text = ds.Tables[0].Rows[0]["Criteria"].ToString();
                    txtPercentage.Text = ds.Tables[0].Rows[0]["WeightagePercent"].ToString();

                    if (Convert.ToInt32(ds.Tables[0].Rows[0]["Optional"]) == 1)
                        chkMandatry.Checked = true;
                    else
                        chkMandatry.Checked = false;

                }
                else
                {

                }

            }
            catch (Exception e)
            {
                throw e;
            }
        }
        #endregion Methods

        protected void TxtScrteria_TextChanged(object sender, EventArgs e)
        {

        }
        //By NAzima for Criteria Googlesearch
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute]


        public static string[] GetCompletionCrtList(string prefixText, int count)
        {
            DataSet ds = AttendanceDAC.GetGoogleSearchCriteriaName(prefixText);
            return ConvertStingArray(ds);

        }

        public static string[] ConvertStingArray(DataSet ds)
        {
            string[] rtval = Array.ConvertAll(ds.Tables[0].Select(), new Converter<DataRow, string>(DataRowTotable));
            return rtval;
        }
        public static string DataRowTotable(DataRow dr)
        {
            return dr["Criteria"].ToString();
        }

        protected void chkselect_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chk = (CheckBox)sender;
            GridViewRow gr = (GridViewRow)chk.Parent.Parent;
            //  string s = gvEV.DataKeys[gr.RowIndex].Value.ToString();
            Label CrtID = (Label)gr.FindControl("CrtID");
            Label Criteria = (Label)gr.FindControl("Criteria");
            Label Percentage = (Label)gr.FindControl("Percentage");

            SqlParameter[] parms = new SqlParameter[9];
            parms[0] = new SqlParameter("@Criteria", Criteria.Text.Trim());
            parms[1] = new SqlParameter("@id", 2);
            parms[2] = new SqlParameter("@CurrentPage", objHrCommon.CurrentPage);
            parms[3] = new SqlParameter("@PageSize", objHrCommon.PageSize);
            parms[4] = new SqlParameter("@NoofRecords", 3);
            parms[4].Direction = ParameterDirection.Output;
            parms[5] = new SqlParameter();
            parms[5].Direction = ParameterDirection.ReturnValue;
            parms[6] = new SqlParameter("@CriteriaID", CrtID.Text.Trim());
            parms[7] = new SqlParameter("@WeightagePercent", Percentage.Text.Trim());

            if (chk.Checked)
                parms[8] = new SqlParameter("@Optional", 1);
            else
                parms[8] = new SqlParameter("@Optional", 0);

            int Output = SqlHelper.ExecuteNonQuery("USP_HMS_CriteriaofEmpfor_Evaluation_Insert_Update_Delete", parms);
            AlertMsg.MsgBox(Page,"Updated Successfully");
        }

    }
}