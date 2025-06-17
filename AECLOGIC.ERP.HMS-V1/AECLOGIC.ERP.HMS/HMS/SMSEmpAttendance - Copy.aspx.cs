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

namespace AECLOGIC.ERP.HMS
{
    public partial class SMSEmpAttendance : WebFormMaster
    {
        int mid = 0;
        bool viewall;
        string menuname;
        string menuid;
        bool Editable;

        HRCommon objHrCommon = new HRCommon();
        AttendanceDAC objRights = new AttendanceDAC();
        protected override void OnInit(EventArgs e)
        {
            SMSEMPAttendancePaging.FirstClick += new Paging.PageFirst(SMSEMPAttendancePaging_FirstClick);
            SMSEMPAttendancePaging.PreviousClick += new Paging.PagePrevious(SMSEMPAttendancePaging_FirstClick);
            SMSEMPAttendancePaging.NextClick += new Paging.PageNext(SMSEMPAttendancePaging_FirstClick);
            SMSEMPAttendancePaging.LastClick += new Paging.PageLast(SMSEMPAttendancePaging_FirstClick);
            SMSEMPAttendancePaging.ChangeClick += new Paging.PageChange(SMSEMPAttendancePaging_FirstClick);
            SMSEMPAttendancePaging.ShowRowsClick += new Paging.ShowRowsChange(SMSEMPAttendancePaging_ShowRowsClick);
            SMSEMPAttendancePaging.CurrentPage = 1;
        }
        void SMSEMPAttendancePaging_ShowRowsClick(object sender, EventArgs e)
        {
            SMSEMPAttendancePaging.CurrentPage = 1;
            BindPager();
        }
        void SMSEMPAttendancePaging_FirstClick(object sender, EventArgs e)
        {
            if (hdnSearchChange.Value == "1")
                SMSEMPAttendancePaging.CurrentPage = 1;
            BindPager();
            hdnSearchChange.Value = "0";
        }
        void BindPager()
        {

            objHrCommon.PageSize = SMSEMPAttendancePaging.CurrentPage;
            objHrCommon.CurrentPage = SMSEMPAttendancePaging.ShowRows;
            BindEmpDetails(objHrCommon);

        }

        public int GetParentMenuId()
        {
            string URL = Request.Url.Segments[Request.Url.Segments.Length - 1] + Request.Url.Query;
            int RoleId = Convert.ToInt32(Session["RoleId"].ToString());
            int ModuleId = Convert.ToInt32(Application["ModuleId"].ToString());

            DataSet ds = new DataSet();

            ds = AttendanceDAC.GetAllowed(RoleId, ModuleId, URL);
            int MenuId = 0;
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                MenuId = Convert.ToInt32(ds.Tables[0].Rows[0]["Under"]);
                ViewState["Editable"] = Editable = (bool)ds.Tables[0].Rows[0]["Editable"];
                ViewState["ViewAll"] = (bool)ds.Tables[0].Rows[0]["ViewAll"];
                viewall = (bool)ViewState["ViewAll"];
                menuname = ds.Tables[0].Rows[0]["menuname"].ToString();
                menuid = MenuId.ToString();
                mid = Convert.ToInt32(ds.Tables[0].Rows[0]["MenuId"].ToString());
                btnPunchAll.Enabled = Editable;

            }
            return MenuId;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            Ajax.Utility.RegisterTypeForAjax(typeof(AjaxDAL));

            topmenu.MenuId = GetParentMenuId();
            topmenu.ModuleId = Convert.ToInt32(Application["ModuleId"].ToString());
            topmenu.RoleID = Convert.ToInt32(Session["RoleId"].ToString());
            topmenu.SelectedMenu = Convert.ToInt32(mid.ToString());
            topmenu.DataBind();

            Session["menuname"] = menuname;
            Session["menuid"] = menuid;
            Session["MId"] = mid;

            if (!IsPostBack)
            {
                BindAttendanceType();
                BindWS();
                BindDept();
                BindShifts();
                BindPager();
            }
        }

        void BindEmpDetails(HRCommon objHrCommon)
        {
            try
            {
                objHrCommon.PageSize = SMSEMPAttendancePaging.ShowRows;
                objHrCommon.CurrentPage = SMSEMPAttendancePaging.CurrentPage;
                dvgrd.Visible = true;
                int? WS = null;
                int? Shift = null;
                int? Dept = null;

                if (ddlWS.SelectedIndex != 0)
                    WS = Convert.ToInt32(ddlWS.SelectedValue);
                if (ddlDept.SelectedIndex != 0)
                    Dept = Convert.ToInt32(ddlDept.SelectedValue);

                if (ddlShift.SelectedIndex != 0)
                    Shift = Convert.ToInt32(ddlShift.SelectedValue);
                DataSet ds = new DataSet();
                ds = AttendanceDAC.GetAttendanceBySMSByPaging(objHrCommon, WS, Dept, Shift, Convert.ToInt32(Session["CompanyID"]));
                if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0)
                {
                    grdSMSAttendance.DataSource = ds;
                    SMSEMPAttendancePaging.Bind(objHrCommon.CurrentPage, objHrCommon.TotalPages, objHrCommon.NoofRecords, objHrCommon.PageSize);
                    btnPunchAll.Visible = true;
                }
                else
                {
                    grdSMSAttendance.EmptyDataText = "No Records Found";
                    SMSEMPAttendancePaging.Visible = false;
                    btnPunchAll.Visible = false;
                }
                grdSMSAttendance.DataBind();


            }
            catch (Exception e)
            {
                throw e;
            }
        }

        #region ddl
        public void BindWS()
        {
            DataSet ds = new DataSet();
            // ds = objRights.GetWorkSite(0, '1');
            ds = objRights.GetWorkSiteByEmpID(Convert.ToInt32(Session["UserId"]), Convert.ToInt32(Session["CompanyID"]), Convert.ToInt32(Session["RoleId"]));
            ddlWS.DataSource = ds.Tables[0];
            ddlWS.DataTextField = "Site_Name";
            ddlWS.DataValueField = "Site_ID";
            ddlWS.DataBind();
            if (ds.Tables[0].Rows.Count == 0)
            {
                ddlWS.Items.Insert(0, new ListItem("---ALL---", "0", true));
            }

        }
        public void BindDept()
        {
            DataSet ds = new DataSet();
            ds = objRights.GetDepartments(0);
            ddlDept.DataSource = ds.Tables[0];
            ddlDept.DataTextField = "Deptname";
            //ddlDept.DataTextField = "DepartmentName";
            ddlDept.DataValueField = "DepartmentUId";
            ddlDept.DataBind();
            ddlDept.Items.Insert(0, new ListItem("...All...", "0", true));

        }
        public void BindShifts()
        {
            DataSet dsShift = new DataSet();
            dsShift = AttendanceDAC.GetShiftsList();
            ddlShift.DataSource = dsShift.Tables[0];
            ddlShift.DataTextField = "Name";
            ddlShift.DataValueField = "ShiftID";
            ddlShift.DataBind();
            ddlShift.Items.Insert(0, new ListItem("---ALL---", "0", true));
        }
        #endregion ddl

        public DataSet BindStatus()
        {
            return (DataSet)ViewState["RetStatus"];
        }
        public DataSet BindAttendanceType()
        {
            DataSet ds = new DataSet();
            ds = AttendanceDAC.GetAttendanceType();
            DataRow drRole;
            drRole = ds.Tables[0].NewRow();
            ViewState["RetStatus"] = ds;

            alStatus = new ArrayList();
            foreach (DataRow dr in ds.Tables[0].Rows)
                alStatus.Add(dr["ID"].ToString());
            return RetStatus;
        }


        public static ArrayList alStatus = new ArrayList();
        public int GetStatusIndex(string Value)
        {
            return alStatus.IndexOf(Value);
        }
        public DataSet RetStatus = new DataSet();

        #region Events

        protected void grdSMSAttendance_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            GridViewRow gvr = (GridViewRow)((LinkButton)e.CommandSource).Parent.Parent;
            Label lblTrans = (Label)grdSMSAttendance.Rows[gvr.RowIndex].FindControl("lblTransID");
            int TransID = int.Parse(lblTrans.Text);

            int UserID = Convert.ToInt32(Session["UserId"]);
            if (e.CommandName == "Punch")
            {
                AttendanceDAC.UpdateSMSEmpStatus(TransID, UserID);
            }
            if (e.CommandName == "Del")
            {
                AttendanceDAC.UpdateSMSEmpStatus(TransID, UserID);
            }
            BindPager();

        }
        protected void grdSMSAttendance_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    Label lblEmpId = (Label)e.Row.Cells[2].FindControl("lblEmpID");
                    int EmpID = int.Parse(lblEmpId.Text);

                    TextBox txt = (TextBox)e.Row.Cells[6].FindControl("txtIN");


                    Label lblSiteID = (Label)e.Row.Cells[12].FindControl("lblSiteID");
                    int SiteID = int.Parse(lblSiteID.Text);

                    DropDownList grdddlStatus = (DropDownList)e.Row.Cells[5].FindControl("grdddlAttStatus");

                    Label lblTransId = (Label)e.Row.Cells[11].FindControl("lblTransID");
                    int TransID = int.Parse(lblTransId.Text);

                    LinkButton lnkDel = (LinkButton)e.Row.FindControl("lnkDel");
                    lnkDel.Enabled = Editable;


                    LinkButton lnkPun = (LinkButton)e.Row.FindControl("lnkPunch");
                    lnkPun.Enabled = Editable;
                    //if(Editable == true)  
                    lnkPun.Attributes.Add("onclick", "javascript:return CheckLeaveCombination(this,'" + EmpID + "','" + txt.ClientID + "','" + SiteID + "','" + Session["UserId"].ToString() + "','" + e.Row.ClientID + "','" + txt.ClientID + "','" + grdddlStatus.ClientID + "');");

                }
            }
            catch
            {

            }
        }
        protected void btnPunchAll_Click(object sender, EventArgs e)
        {

            foreach (GridViewRow gvr in grdSMSAttendance.Rows)
            {
                CheckBox chk = (CheckBox)gvr.FindControl("chkAll");

                if (chk.Checked)
                {
                    Label lblEmpId = (Label)gvr.Cells[2].FindControl("lblEmpID");
                    int EmpID = int.Parse(lblEmpId.Text);

                    TextBox txt = (TextBox)gvr.Cells[6].FindControl("txtIN");
                    string InTime = txt.Text;

                    Label lblSiteID = (Label)gvr.Cells[12].FindControl("lblSiteID");
                    int SiteID = int.Parse(lblSiteID.Text);

                    Label lblTransID = (Label)gvr.Cells[11].FindControl("lblTransID");
                    int TransID = int.Parse(lblTransID.Text);

                    DropDownList grdddlStatus = (DropDownList)gvr.Cells[5].FindControl("grdddlAttStatus");
                    int Status = Convert.ToInt32(grdddlStatus.SelectedItem.Value);

                    int UserID = Convert.ToInt32(Session["UserId"]);

                    AttendanceDAC.HR_MarkAttandaceBySMS(EmpID, Status, SiteID, UserID, InTime);
                    AttendanceDAC.UpdateSMSEmpStatus(TransID, UserID);
                }

                //else
                //    AlertMsg.MsgBox(Page, "Plz Select Atleast One Employee");

            }
            BindPager();

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindPager();
        }

        #endregion Events



    }
}