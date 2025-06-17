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
    public partial class EmployeeTypes : AECLOGIC.ERP.COMMON.WebFormMaster
    {
         
        int mid = 0;
        bool viewall, Editable;
        string menuname;
        string menuid;
        HRCommon objHrCommon = new HRCommon();
        static bool sttus;
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
                bool Status = false;
                if (rblDesg.SelectedValue == "1")
                {
                    Status = true;
                }
                int EmpTyID = 0; if (txtseaechemployee.Text.Trim() != "")
                { EmpTyID = Convert.ToInt32(ddlEmployee_hid.Value == "" ? "0" : ddlEmployee_hid.Value); }
                DataSet ds = AttendanceDAC.HR_GetGetEmployeeTypeByStatus(objHrCommon, Status, EmpTyID);
                if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0)
                {
                    gvRMItem.DataSource = ds;
                    gvRMItem.DataBind();
                    EmpListPaging.Visible = true;
                }
                else
                {
                    EmpListPaging.Visible = false;
                    gvRMItem.DataSource = null;
                    gvRMItem.DataBind();
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
            if (rblDesg.SelectedValue == "1")
            {
                sttus = true;
            }
            else
                sttus = false;
            lblStatus.Text = String.Empty;  
            if (!IsPostBack)
            {
                GetParentMenuId();
                ViewState["CateId"] = string.Empty;

                if (Convert.ToInt32(Request.QueryString["key"]) == 1)
                {
                    this.Title = "Add Employee Type";
                    tblNew.Visible = true;
                    tblEdit.Visible = false;
                    gvRMItem.Visible = false;

                }
                else
                {
                    tblNew.Visible = false;
                    tblEdit.Visible = true;
                    gvRMItem.Visible = true;
                    EmployeBind(objHrCommon);
                }

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
                gvRMItem.Columns[1].Visible = Convert.ToBoolean(ds.Tables[0].Rows[0]["Editable"].ToString());
                btnSubmit.Enabled = Editable = Convert.ToBoolean(ds.Tables[0].Rows[0]["Editable"].ToString());
            }
            return MenuId;
        }
       
        public void BindDetails(int ID)
        {
            tblEdit.Visible = false;
            tblNew.Visible = true;
          DataSet  ds = AttendanceDAC.GetEmployeeTypesListDetails(ID);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                txtName.Text = ds.Tables[0].Rows[0]["EMpType"].ToString();

            }
        }
        protected void gvWages_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int ID = Convert.ToInt32(e.CommandArgument);
            ViewState["CateId"] = ID;
            bool Status = true;
            if (rblDesg.SelectedValue == "1")
            {
                Status = false;
            }
            if (e.CommandName == "Edt")
            {
                BindDetails(ID);
            }
            else
            {
                try
                {
                    //AttendanceDAC.HR_Upd_emptypeStatus(ID, Status);
                    AttendanceDAC.HR_Upd_DesigStatus_employeetypes(ID, Status);
                    lblStatus.Text = "Done.!!";
                    lblStatus.ForeColor = System.Drawing.Color.Green;
                    EmployeBind(objHrCommon);

                }
                catch (Exception DelDesig)
                {
                    AlertMsg.MsgBox(Page, DelDesig.Message.ToString(),AlertMsg.MessageType.Error);
                }
            }
        }

        public string GetText()
        {

            if (rblDesg.SelectedValue == "1")
            {
                return "Deactivate";
            }
            else
            {
                return "Activate";
            }
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                int? CateId = 0;
                if (ViewState["CateId"].ToString() != null && ViewState["CateId"].ToString() != string.Empty)
                {
                    CateId = Convert.ToInt32(ViewState["CateId"].ToString());
                }
                int Status = 0;
                if (chkStatus.Checked == true)
                    Status = 1;
                int EmpID = Convert.ToInt32(Session["UserId"]);
                int Output = AttendanceDAC.InsUpdEmployeeType(ref CateId, txtName.Text.ToString(), Status, EmpID);
                EmployeBind(objHrCommon);
                tblNew.Visible = false;
                tblEdit.Visible = true;
                gvRMItem.Visible = true;
                Clear();
                if (Output == 1)
                {
                 //   AlertMsg.MsgBox(Page, "Done.!!");
                    lblStatus.Text = "Done.!!";
                    lblStatus.ForeColor = System.Drawing.Color.Green;
                }
                else if (Output == 2)
                {
                    //AlertMsg.MsgBox(Page, "Already Exists.!");
                    lblStatus.Text = "Already Exists.!";
                    lblStatus.ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                   // AlertMsg.MsgBox(Page, "Updated.!");
                    lblStatus.Text = "Updated.!";
                    lblStatus.ForeColor = System.Drawing.Color.Green;
                }


            }
            catch (Exception AddDesignation)
            {
                AlertMsg.MsgBox(Page, AddDesignation.Message.ToString(),AlertMsg.MessageType.Error);
                lblStatus.Text = AddDesignation.Message.ToString();
                lblStatus.ForeColor = System.Drawing.Color.Red;
            }

        }
        public void Clear()
        {
            txtName.Text = "";
            ViewState["CateId"] = "";
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
        protected void rblDesg_SelectedIndexChanged(object sender, EventArgs e)
        {
            EmpListPaging.CurrentPage = 1;

            EmployeBind(objHrCommon);
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletiondeptList(string prefixText, int count, string contextKey)
        {
            DataSet ds = AttendanceDAC.SH_EmployeeType(prefixText, sttus);
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
        protected void btnsearch_Click(object sender, EventArgs e)
        {
            BindPager();
        }
       
    }
}