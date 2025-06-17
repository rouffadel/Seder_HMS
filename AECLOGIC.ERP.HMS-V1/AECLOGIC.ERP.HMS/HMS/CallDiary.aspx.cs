using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AECLOGIC.HMS.BLL;
using System.Data;
using System.Configuration;
using AECLOGIC.ERP.COMMON;
namespace AECLOGIC.ERP.HMS
{
    public partial class CallDiary : AECLOGIC.ERP.COMMON.WebFormMaster
    {
        AttendanceDAC objAtt = new AttendanceDAC();
         
        int mid = 0;
        bool viewall, Editable;
        string menuname;
        string menuid;
        HRCommon objHrCommon = new HRCommon();
        string[] Companies;
        protected override void OnInit(EventArgs e)
        {
            ModuleID = 1;
            base.OnInit(e);
            EmpWorkPaging.FirstClick += new Paging.PageFirst(EmpWorkPaging_FirstClick);
            EmpWorkPaging.PreviousClick += new Paging.PagePrevious(EmpWorkPaging_FirstClick);
            EmpWorkPaging.NextClick += new Paging.PageNext(EmpWorkPaging_FirstClick);
            EmpWorkPaging.LastClick += new Paging.PageLast(EmpWorkPaging_FirstClick);
            EmpWorkPaging.ChangeClick += new Paging.PageChange(EmpWorkPaging_FirstClick);
            EmpWorkPaging.ShowRowsClick += new Paging.ShowRowsChange(EmpWorkPaging_ShowRowsClick);
            EmpWorkPaging.CurrentPage = 1;
        }
        void EmpWorkPaging_ShowRowsClick(object sender, EventArgs e)
        {
            EmpWorkPaging.CurrentPage = 1;
            BindPager();
        }
        void EmpWorkPaging_FirstClick(object sender, EventArgs e)
        {
            BindPager();
        }
        void BindPager()
        {
            objHrCommon.PageSize = EmpWorkPaging.CurrentPage;
            objHrCommon.CurrentPage = EmpWorkPaging.ShowRows;
            BindGrid(objHrCommon);
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                string id =  Convert.ToInt32(Session["UserId"]).ToString();
            }
            catch
            {
                Response.Redirect("Home.aspx");
            }

            if (!IsPostBack)
            {
                GetParentMenuId();
                txtFromDate.Text = DateTime.Now.AddMonths(-1).ToString("dd/MM/yyyy");
                txtTodate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                ViewState["DiaryID"] = 0;
                BindEmployees();

                BindPager();
            }

        }


        public int GetParentMenuId()
        {
            string URL = Request.Url.Segments[Request.Url.Segments.Length - 1] + Request.Url.Query;
            int RoleId = Convert.ToInt32(Session["RoleId"].ToString());
            int ModuleId = ModuleID;;
            DataSet ds = AttendanceDAC.GetAllowed(RoleId, ModuleId, URL);
            int MenuId = 0;
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                MenuId = Convert.ToInt32(ds.Tables[0].Rows[0]["Under"]);
                Editable = (bool)ds.Tables[0].Rows[0]["Editable"];
                btnSubmit.Enabled = Editable;
            }
            return MenuId;
        }
        public void BindGrid(HRCommon objHrCommon)//int EmpID)
        {
            try
            {

                objHrCommon.PageSize = EmpWorkPaging.ShowRows;
                objHrCommon.CurrentPage = EmpWorkPaging.CurrentPage;
                int? SeaEmpID = null;
                int? SeaWsID = null;


                if (ddlSeaEmp.SelectedItem.Value != "0")
                {
                    SeaEmpID = Convert.ToInt32(ddlSeaEmp.SelectedItem.Value);
                }
             DataSet   dswork = AttendanceDAC.GetEmpCallDiary(objHrCommon, SeaEmpID, Convert.ToInt32(Session["CompanyID"]), CODEUtility.ConvertToDate(txtFromDate.Text.Trim(), DateFormat.DayMonthYear), CODEUtility.ConvertToDate(txtTodate.Text.Trim(), DateFormat.DayMonthYear), txtSeaSeeking.Text.Trim(), txtSeaCompnay.Text.Trim());
                if (dswork != null && dswork.Tables.Count != 0 && dswork.Tables[0].Rows.Count > 0)
                {
                    grdCallDiary.DataSource = dswork;
                    grdCallDiary.DataBind();
                }
                else
                {
                    grdCallDiary.DataSource = null;
                    grdCallDiary.DataBind();
                }
                EmpWorkPaging.Bind(objHrCommon.CurrentPage, objHrCommon.TotalPages, objHrCommon.NoofRecords, objHrCommon.PageSize);

            }
            catch (Exception e)
            {
                throw e;
            }

        }
        public void BindDetails(int DiaryID)
        {

            DataSet dswork = AttendanceDAC.GetEmpCallDiaryDets(DiaryID);
            if (dswork != null && dswork.Tables.Count > 0 && dswork.Tables[0].Rows.Count > 0)
            {
                txtCaller.Text = dswork.Tables[0].Rows[0]["Caller"].ToString();
                txtCompany.Text = dswork.Tables[0].Rows[0]["Company"].ToString();
                txtMessage.Text = dswork.Tables[0].Rows[0]["Message"].ToString();
                ddlEmp.SelectedValue = dswork.Tables[0].Rows[0]["Seeking"].ToString();

            }
        }


        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {

                int EmpID = Convert.ToInt32(ViewState["EmpID"]);
                int DiaryID = Convert.ToInt32(ViewState["DiaryID"].ToString());

                int RetVal;
                RetVal = Convert.ToInt32(AttendanceDAC.InsertCallerDiary(DiaryID, txtCaller.Text, txtCompany.Text, Convert.ToInt32(ddlEmp.SelectedItem.Value), txtMessage.Text));
                BindPager();
                AlertMsg.MsgBox(Page, "Done!");

                Response.Redirect("CallDiary.aspx");
            }
            catch (Exception EmpWork)
            {
                AlertMsg.MsgBox(Page, EmpWork.Message.ToString(),AlertMsg.MessageType.Error);
            }
        }

        public void BindEmployees()
        {
            int? WsID = null;
            int? DeptID = null;
            DataSet ds = objAtt.GetEmpByWSAndDept(WsID, DeptID);

            ddlEmp.DataSource = ds.Tables[0];
            ddlEmp.DataTextField = "name";
            ddlEmp.DataValueField = "EmpID";
            ddlEmp.DataBind();
            ddlEmp.Items.Insert(0, new ListItem("---SELECT---", "0", true));

            ddlSeaEmp.DataSource = ds.Tables[0];
            ddlSeaEmp.DataTextField = "name";
            ddlSeaEmp.DataValueField = "EmpID";
            ddlSeaEmp.DataBind();
            ddlSeaEmp.Items.Insert(0, new ListItem("---All---", "0", true));



        }

        protected void grdCallDiary_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {

                if (e.CommandName == "Edt")
                {
                    int DiaryID = Convert.ToInt32(e.CommandArgument);
                    ViewState["DiaryID"] = DiaryID;
                     

                    BindDetails(DiaryID);
                    BindPager();
                    btnSubmit.Enabled = true;
                }

                if (e.CommandName == "Delete")
                {

                }
            }
            catch (Exception EmpWorkDel)
            {
                AlertMsg.MsgBox(Page, EmpWorkDel.Message.ToString(),AlertMsg.MessageType.Error);
            }
        }
        protected void btnDaySearch_Click(object sender, EventArgs e)
        {
            BindPager();

        }

        //start For Compnay&caller Autotext extender

        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionList(string prefixText, int count, string contextKey)
        {
            DataSet ds = AttendanceDAC.GetSearchCompany(prefixText);
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


        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetVistList(string prefixText, int count, string contextKey)
        {
            DataSet ds = AttendanceDAC.GetSearchVisitor(prefixText);
            return ConvertStingArray(ds);// txtItems.ToArray();
        }

        //End  For Compnay&caller Autotext extender



    }
}