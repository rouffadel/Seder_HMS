using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using AECLOGIC.HMS.BLL;
using System.Data;
using System.Data.SqlClient;
using AECLOGIC.ERP.COMMON;
using Aeclogic.Common.DAL;
using AECLOGIC.ERP.HMS.HRClasses;
namespace AECLOGIC.ERP.HMSV1
{
    public partial class AttendanceV1 : AECLOGIC.ERP.COMMON.WebFormMaster
    {
        int mid = 0;
        bool viewall;
        string menuname;
        static int CompnanyID;
        static int Role;
        static int EmpID;
        static int Deptid;
        string menuid;
        AttendanceDAC objAtt = new AttendanceDAC();
        HRCommon objHrCommon = new HRCommon();
        DataSet dstemp = new DataSet();


        // int  ModuleId = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["ModuleId"].ToString());
        //        As per my information 
        //6th APR 2016 Changes In time working
        //12th APR 2016 on words not working 
        //am taken now 6th APR Changes 
        //afte that 6th changes need to including in this page 
        //        Audit Report					
        //Change set	Type	User	Date	Path	Comments
        //2806	edit	aecdev11	04/15/2016 14:54	$/AECLOGIC.ERP.HMS/AECLOGIC.ERP.HMS/HMS/Attendance.aspx	Check in for leave and attendence
        //2805	edit	aecdev6	04/15/2016 14:31	$/AECLOGIC.ERP.HMS/AECLOGIC.ERP.HMS/HMS/Attendance.aspx	set
        //2791	edit	aecdev6	04/14/2016 17:05	$/AECLOGIC.ERP.HMS/AECLOGIC.ERP.HMS/HMS/Attendance.aspx	Design changes done
        //2772	edit	aecdev4	04/14/2016 11:14	$/AECLOGIC.ERP.HMS/AECLOGIC.ERP.HMS/HMS/Attendance.aspx	Empid Placed in Search 
        //2766	edit	aecdev6	04/13/2016 22:40	$/AECLOGIC.ERP.HMS/AECLOGIC.ERP.HMS/HMS/Attendance.aspx	done mark all wrking 
        //2721	edit	aecdev6	04/12/2016 17:04	$/AECLOGIC.ERP.HMS/AECLOGIC.ERP.HMS/HMS/Attendance.aspx	Visible false for CL,EL,SL
        //2710	edit	aecdev4	04/12/2016 11:28	$/AECLOGIC.ERP.HMS/AECLOGIC.ERP.HMS/HMS/Attendance.aspx	ddlworksite,ddldepartment,attendace type visble=false
        //2579	edit	aecdev6	04/06/2016 10:55	$/AECLOGIC.ERP.HMS/AECLOGIC.ERP.HMS/HMS/Attendance.aspx	done
        //2556	edit	aecdev6	04/05/2016 12:44	$/AECLOGIC.ERP.HMS/AECLOGIC.ERP.HMS/HMS/Attendance.aspx	OUT link worked 
        //2376	edit	aecdev6	03/30/2016 17:31	$/AECLOGIC.ERP.HMS/AECLOGIC.ERP.HMS/HMS/Attendance.aspx	done

        protected override void OnInit(EventArgs e)
        {
            ModuleID = 1;
            base.OnInit(e);

            AddAttpaging.FirstClick += new Paging.PageFirst(AdvancedLeaveAppPaging_FirstClick);
            AddAttpaging.PreviousClick += new Paging.PagePrevious(AdvancedLeaveAppPaging_FirstClick);
            AddAttpaging.NextClick += new Paging.PageNext(AdvancedLeaveAppPaging_FirstClick);
            AddAttpaging.LastClick += new Paging.PageLast(AdvancedLeaveAppPaging_FirstClick);
            AddAttpaging.ChangeClick += new Paging.PageChange(AdvancedLeaveAppPaging_FirstClick);
            AddAttpaging.ShowRowsClick += new Paging.ShowRowsChange(AdvancedLeaveAppPaging_ShowRowsClick);
            AddAttpaging.CurrentPage = 1;


            OutTimePaging.FirstClick += new Paging.PageFirst(AdvancedLeaveAppPaging_FirstClick);
            OutTimePaging.PreviousClick += new Paging.PagePrevious(AdvancedLeaveAppPaging_FirstClick);
            OutTimePaging.NextClick += new Paging.PageNext(AdvancedLeaveAppPaging_FirstClick);
            OutTimePaging.LastClick += new Paging.PageLast(AdvancedLeaveAppPaging_FirstClick);
            OutTimePaging.ChangeClick += new Paging.PageChange(AdvancedLeaveAppPaging_FirstClick);
            OutTimePaging.ShowRowsClick += new Paging.ShowRowsChange(AdvancedLeaveAppPaging_ShowRowsClick);
            OutTimePaging.CurrentPage = 1;




        }


        void AdvancedLeaveAppPaging_ShowRowsClick(object sender, EventArgs e)
        {
            AddAttpaging.CurrentPage = 1;
            BindGrid(objAtt, dstemp);

            OutTimePaging.CurrentPage = 1;
            BindOut();


        }
        void AdvancedLeaveAppPaging_FirstClick(object sender, EventArgs e)
        {
            BindGrid(objAtt, dstemp);
            BindOut();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Ajax.Utility.RegisterTypeForAjax(typeof(AjaxDAL));
            lblDate.Text = "Date: " + DateTime.Now.ToString(ConfigurationManager.AppSettings["DateDisplayFormat"]);
            hdn.Value = "0";
            CompnanyID = Convert.ToInt32(Session["CompanyID"]);
            if (!IsPostBack)
            {
                GetParentMenuId();
                objHrCommon.CurrentPage = OutTimePaging.CurrentPage;
                objHrCommon.PageSize = OutTimePaging.ShowRows;
                DataSet dsOut = BindOut();
                if (dsOut.Tables[0].Rows.Count > 0)
                {
                    gvOutTime.DataSource = dsOut;
                    gvOutTime.DataBind();
                    tdOutTime.Visible = true;
                    OutTimePaging.Visible = true;
                    tdGap.Visible = true;
                    OutTimePaging.Bind(objHrCommon.CurrentPage, objHrCommon.TotalPages, objHrCommon.NoofRecords, objHrCommon.PageSize);
                }
                FillAttandanceType();
                FIllObject.FillDropDown(ref ddlempid, "HR_ddl_SearchEmpBySiteDept");


                Ajax.Utility.RegisterTypeForAjax(typeof(AjaxDAL));
                try
                {
                    string URL = Request.Url.Segments[Request.Url.Segments.Length - 1] + Request.Url.Query;
                    int RoleId = Convert.ToInt32(Session["RoleId"].ToString());
                    int ModuleId = ModuleID; ;
                    DataSet ds = AttendanceDAC.GetAllowed(RoleId, ModuleId, URL);

                    dstemp = GetWorkSites(objAtt, dstemp);
                    dstemp = GetDepartments(objAtt, dstemp);
                    dstemp = BindGrid(objAtt, dstemp);
                    ddlWorksite.SelectedValue = "0";
                    //ddlWorksite.SelectedValue = Session["Wsid"].ToString();
                    //if ((bool)ViewState["ViewAll"])
                    //{
                    //    ddlWorksite.Enabled = true;
                    //}
                    //else
                    //{
                    //    ddlWorksite.Enabled = false;
                    //}

                    FIllObject.FillDropDown(ref ddlShift, "HR_GetShifts");
                    ddlShift.SelectedValue = "1";
                    ddlDepartment.Items[0].Selected = true;
                }
                catch
                {
                }
                try
                {

                    ViewState["WSID"] = 0;
                    //if (Convert.ToInt32(Session["RoleId"].ToString()) == 7)
                    //{
                    try
                    {

                        DataSet ds = clViewCPRoles.HR_DailyAttStatus(Convert.ToInt32(Session["UserId"]));
                        ViewState["WSID"] = ds.Tables[0].Rows[0]["ID"].ToString();
                        ddlWorksite.SelectedValue = ds.Tables[0].Rows[0]["ID"].ToString();
                        // ddlWorksite.Enabled = false;
                        if (Convert.ToInt32(Request.QueryString.Count) > 0) // 1 means Dayreport
                        {
                            int WSID = Convert.ToInt32(Request.QueryString["WSID"].ToString());
                            ViewState["WSID"] = WSID;
                            ddlWorksite.SelectedValue = WSID.ToString();
                        }
                        BIndProject();
                        BindGrid();
                    }
                    catch { }
                    //}
                }
                catch { }
            }
            //Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "MyFunction()", true);
            if (Convert.ToInt32(Session["UserId"]) == 1)
            {
                //    exp.Style.Add("style", "display:none");
                //    used.Style.Add("style", "display:none");
                dvreverse.Style.Add("style", "display:none");
                dvreverse.Visible = true;
                dvreverse1.Visible = true;
                dvreverse2.Visible = true;
            }
            else
            {
                dvreverse.Style.Add("style", "display:block");
                dvreverse.Visible = false;
                dvreverse1.Visible = false;
                dvreverse2.Visible = false;
                //    exp.Style.Add("style", "display:block");
                //    used.Style.Add("style", "display:block");
            }
        }
        public int GetParentMenuId()
        {
            string URL = Request.Url.Segments[Request.Url.Segments.Length - 1] + Request.Url.Query;
            int RoleId = Convert.ToInt32(Session["RoleId"].ToString());
            int ModuleId = ModuleID; ;
            DataSet ds = new DataSet();
            ds = AttendanceDAC.GetAllowed(RoleId, ModuleId, URL);
            int MenuId = 0;
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                MenuId = Convert.ToInt32(ds.Tables[0].Rows[0]["Under"]);
                //ViewState["Editable"] = (bool)ds.Tables[0].Rows[0]["Editable"];
                //ViewState["ViewAll"] = (bool)ds.Tables[0].Rows[0]["ViewAll"];
                ViewState["Editable"] = (bool)ds.Tables[0].Rows[0]["Editable"];
                ViewState["ViewAll"] = (bool)ds.Tables[0].Rows[0]["ViewAll"];
                viewall = (bool)ViewState["ViewAll"];
                menuname = ds.Tables[0].Rows[0]["menuname"].ToString();
                menuid = MenuId.ToString();
                mid = Convert.ToInt32(ds.Tables[0].Rows[0]["MenuId"].ToString());
            }
            return MenuId;
        }
        private void FillAttandanceType()
        {
            DataSet ds = AttendanceDAC.GetAttendanceType();
            ddlAttType.DataSource = ds;
            ddlAttType.DataValueField = "ID";
            ddlAttType.DataTextField = "ShortName";
            ddlAttType.DataBind();
            ddlAttType.Items.Insert(0, new ListItem("--AttType--", "0"));
        }

        private DataSet BindOut()
        {
            try
            {
                objHrCommon.CurrentPage = OutTimePaging.CurrentPage;
                objHrCommon.PageSize = OutTimePaging.ShowRows;
                DataSet ds = AttendanceDAC.HR_GetUnOutPined(objHrCommon, Convert.ToInt32(Session["CompanyID"]));
                if (ds.Tables[0].Rows.Count > 0)
                {
                    gvOutTime.DataSource = ds;
                    gvOutTime.DataBind();
                    tdOutTime.Visible = true;
                    OutTimePaging.Visible = true;
                    tdGap.Visible = true;
                    OutTimePaging.Bind(objHrCommon.CurrentPage, objHrCommon.TotalPages, objHrCommon.NoofRecords, objHrCommon.PageSize);
                }



                return ds;

            }
            catch
            {
                DataSet ds = null;
                return ds;
            }
        }

        public DataSet BindAttendanceType()
        {
            DataSet ds = AttendanceDAC.GetAttendanceType();
            return ds;
        }
        private DataSet BindGrid(AttendanceDAC objAtt, DataSet dstemp)
        {
            try
            {
                objHrCommon.CurrentPage = AddAttpaging.CurrentPage;
                objHrCommon.PageSize = AddAttpaging.ShowRows;

                int WSid = 0, DeptID;
                WSid = Convert.ToInt32(ddlWorksite.SelectedValue);
                if (WSid == 0)
                {
                    SqlParameter[] par = new SqlParameter[1];
                    par[0] = new SqlParameter("@EmpID", (Convert.ToInt32(Session["UserId"])));
                    SqlDataReader Dr;
                    Dr = SqlHelper.ExecuteReader("HR_WorksiteByEmpID", par);

                    if (Dr.HasRows)
                    {
                        while (Dr.Read())
                            WSid = Convert.ToInt32(Dr["Categary"]);

                    }

                }
                Session["Wsid"] = WSid;

                try
                {
                    DeptID = Convert.ToInt32(ddlDepartment.SelectedValue);
                }
                catch
                {
                    DeptID = 0;
                }
                SqlParameter[] sqlParams = new SqlParameter[7];
                sqlParams[0] = new SqlParameter("@CurrentPage", objHrCommon.CurrentPage);
                sqlParams[1] = new SqlParameter("@PageSize", objHrCommon.PageSize);
                sqlParams[2] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
                sqlParams[2].Direction = ParameterDirection.ReturnValue;
                sqlParams[3] = new SqlParameter("@NoofRecords", System.Data.SqlDbType.Int);
                sqlParams[3].Direction = ParameterDirection.Output;
                sqlParams[4] = new SqlParameter("@WSID", WSid);
                sqlParams[5] = new SqlParameter("@DeptID", DeptID);
                sqlParams[6] = new SqlParameter("@UserId", Convert.ToInt32(Session["UserId"]));
                dstemp = SQLDBUtil.ExecuteDataset("HR_GetTodayAttendance_Paging_New", sqlParams);
                objHrCommon.NoofRecords = (int)sqlParams[3].Value;
                objHrCommon.TotalPages = (int)sqlParams[2].Value;

                //dstemp = objAtt.GetTodayAttendanceByPaging(objHrCommon, WSid, DeptID);
                if (dstemp.Tables[2].Rows.Count >= 1)
                {
                    gdvAttend.DataSource = dstemp.Tables[2];
                    gdvAttend.DataBind();
                    ddlShift.SelectedIndex = 1;

                }
                AddAttpaging.Visible = true;
                AddAttpaging.Bind(objHrCommon.CurrentPage, objHrCommon.TotalPages, objHrCommon.NoofRecords, objHrCommon.PageSize);

            }
            catch { }
            return dstemp;
        }
        private DataSet GetDepartments(AttendanceDAC objAtt, DataSet dstemp)
        {
            dstemp = objAtt.GetDepartments(0);
            ddlDepartment.DataSource = dstemp.Tables[0];
            ddlDepartment.DataTextField = "DeptName";
            ddlDepartment.DataValueField = "DepartmentUId";
            ddlDepartment.DataBind();
            ddlDepartment.Items.Insert(0, new ListItem("---ALL---", "0", true));
            return dstemp;
        }
        private DataSet GetWorkSites(AttendanceDAC objAtt, DataSet dstemp)
        {
            dstemp = AttendanceDAC.GetHMS_DDL_WorkSite(Convert.ToInt32(Session["UserId"]), ModuleID, Convert.ToInt32(Session["CompanyID"]));
            ddlWorksite.DataSource = dstemp.Tables[0];
            ddlWorksite.DataTextField = "Name";
            ddlWorksite.DataValueField = "ID";
            ddlWorksite.DataBind();
            ddlWorksite.Items.Insert(0, new ListItem("---All---", "0", true));
            return dstemp;
            ddlWorksite.SelectedItem.Value = "1";
        }
        protected void ddl_SelectedIndexChanged(object sender, EventArgs e)
        {
            ViewState["WSID"] = ddlWorksite.SelectedValue;
            if (ddlWorksite.SelectedValue != "0")
            {
                BIndProject();
                BindGrid();
            }
            else
            {
                ddlProject.DataSource = null;
                ddlProject.DataBind();
                gdvAttend.DataSource = null;
                gdvAttend.DataBind();
                ddlShift.SelectedIndex = 0;
            }
        }

        private void BIndProject()
        {
            int wsid = Convert.ToInt32(ddlWorksite.SelectedValue);
            SqlParameter[] objParam = new SqlParameter[1];
            if (Convert.ToInt32(ddlWorksite.SelectedValue) == 0)
                objParam[0] = new SqlParameter("@siteID", System.Data.SqlDbType.Int);
            else
                objParam[0] = new SqlParameter("@siteID", wsid);
            DataSet dss = SQLDBUtil.ExecuteDataset("sh_worksitewiseproject", objParam);
            if (dss != null && dss.Tables.Count > 0)
            {
                ddlProject.DataSource = dss.Tables[0];
                ddlProject.DataValueField = "ProjectID";
                ddlProject.DataTextField = "projectname";
                ddlProject.DataBind();
                ddlProject.Items.Insert(0, new ListItem("---Select---", "0", true));
                ViewState["Projects"] = dss.Tables[0];
            }
            else
                ViewState["Projects"] = null;
        }
        protected void gdvAttend_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DropDownList ddlStatus = (DropDownList)e.Row.FindControl("ddlStatus");
                DropDownList ddlGProjects = (DropDownList)e.Row.FindControl("ddlGProjects");

                Button btnHdn = (Button)e.Row.FindControl("btnHid");
                CheckBox chkOut = (CheckBox)e.Row.FindControl("chkOut");

                TextBox txtOut = (TextBox)e.Row.FindControl("txtOUT");
                TextBox txtRemarks = (TextBox)e.Row.FindControl("txtRemarks");
                TextBox txt = (TextBox)e.Row.FindControl("txtIN");

                if (btnHdn.CommandArgument != "")
                    ddlStatus.SelectedValue = btnHdn.CommandArgument;
                if (txtOut.Text != "")
                {
                    chkOut.Checked = true;
                    chkOut.Enabled = false;
                }
                if (txt.Text == "")
                {
                    chkOut.Enabled = true;
                    chkOut.Checked = false;
                    txtOut.Text = "";
                }
                string EmpId = gdvAttend.DataKeys[e.Row.RowIndex].Value.ToString();

                chkOut.Attributes.Add("onclick", "javascript:return GetOutTime('" + chkOut.ClientID + "','" + DateTime.Now.ToShortTimeString() + "','" + txtOut.ClientID + "','" + txt.ClientID + "','" + EmpId + "');");
                ddlStatus.Attributes.Add("onchange", "javascript:return CheckLeaveCombination('" + ddlStatus.ClientID + "','" + EmpId + "','" + txt.ClientID + "','" + ddlWorksite.ClientID + "','" + Convert.ToInt32(Session["UserId"]).ToString() + "','" + e.Row.ClientID + "','" + txt.ClientID + "','" + ddlGProjects.ClientID + "');");
                ddlGProjects.Attributes.Add("onchange", "javascript:return CheckLeaveCombination('" + ddlStatus.ClientID + "','" + EmpId + "','" + txt.ClientID + "','" + ddlWorksite.ClientID + "','" + Convert.ToInt32(Session["UserId"]).ToString() + "','" + e.Row.ClientID + "','" + txt.ClientID + "','" + ddlGProjects.ClientID + "');");
                txtRemarks.Attributes.Add("onchange", "javascript:return UpdateRemarks('" + EmpId + "','" + txtRemarks.ClientID + "');");
                string color = "#ff9900";
            }
        }
        protected void lnlUpdAll_Click(object sender, EventArgs e)
        {
            try
            {
                AttendanceDAC objAtt = new AttendanceDAC();
                foreach (GridViewRow gvr in gdvAttend.Rows)
                {
                    Label lblEmpID = (Label)gvr.Cells[0].FindControl("lblEmpID");
                    DropDownList ddlStatus = (DropDownList)gvr.Cells[2].Controls[1];
                    CheckBox chkOut = (CheckBox)gvr.Cells[4].FindControl("chkOut");
                    TextBox txtOut = (TextBox)gvr.Cells[6].FindControl("txtOUT");
                    TextBox txt = (TextBox)gvr.Cells[3].Controls[1];
                    TextBox txtRemarks = (TextBox)gvr.Cells[7].FindControl("txtRemarks");
                    int SiteID = Convert.ToInt32(ddlWorksite.SelectedValue);
                    int UserID = Convert.ToInt32(Session["UserId"]);
                    try
                    {

                        if (Convert.ToInt32(ViewState["WSID"]) > 0)
                        {

                            SiteID = Convert.ToInt32(ViewState["WSID"]);
                        }

                    }
                    catch { }
                    int i = objAtt.InsertFullAtt(Convert.ToInt32(lblEmpID.Text), Convert.ToInt32(ddlStatus.SelectedValue), DateTime.Now, txt.Text, txtOut.Text, txtRemarks.Text, SiteID, UserID);
                    if (i == 1)
                    {
                        hdn.Value = "1";
                    }
                }
            }
            catch (Exception ex)
            {
                AlertMsg.MsgBox(Page, "Sorry for the inconvinience. Try again.\nError:" + ex.Message.ToString(), AlertMsg.MessageType.Error);
            }
        }
        protected void gvOutTime_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                CheckBox chkOut = (CheckBox)e.Row.FindControl("chkOut");
                Label lblEmp = (Label)e.Row.FindControl("lblEmpID");
                int EmpId = Convert.ToInt32(lblEmp.Text);
                TextBox txtOut = (TextBox)e.Row.FindControl("txtOUT");
                TextBox txt = (TextBox)e.Row.FindControl("txtIN");
                chkOut.Attributes.Add("onclick", "javascript:return PINOutTime('" + chkOut.ClientID + "','" + DateTime.Now.ToShortTimeString() + "','" + txtOut.ClientID + "','" + txt.ClientID + "','" + EmpId + "')");
            }
        }
        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            tdOutTime.Visible = false;
            tdGap.Visible = false;
            objHrCommon.CurrentPage = OutTimePaging.CurrentPage;
            objHrCommon.PageSize = OutTimePaging.ShowRows;
            DataSet dsOut = AttendanceDAC.HR_GetUnOutPined(objHrCommon, Convert.ToInt32(Session["CompanyID"]));
            if (dsOut.Tables[0].Rows.Count > 0)
            {
                gvOutTime.DataSource = dsOut;
                gvOutTime.DataBind();
                tdOutTime.Visible = true;
                OutTimePaging.Visible = true;
                OutTimePaging.Visible = true;
                tdGap.Visible = true;
                OutTimePaging.Bind(objHrCommon.CurrentPage, objHrCommon.TotalPages, objHrCommon.NoofRecords, objHrCommon.PageSize);
            }
        }
        protected void ddlShift_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int DeptID = Convert.ToInt32(ddlDepartment.SelectedValue);
                int WSID = Convert.ToInt32(ddlWorksite.SelectedValue);
                int Shift = Convert.ToInt32(ddlShift.SelectedValue);

                try
                {

                    if (Convert.ToInt32(ViewState["WSID"]) > 0)
                    {

                        WSID = Convert.ToInt32(ViewState["WSID"]);
                    }

                }
                catch { }
                DataSet ds = AttendanceDAC.HR_GetTodayAttendanceByShift(DeptID, WSID, Shift, Convert.ToInt32(Session["CompanyID"]));
                gdvAttend.DataSource = ds;
                gdvAttend.DataBind();
            }
            catch
            {
                AlertMsg.MsgBox(Page, "No Records!");
            }
        }
        protected void btnPrint_Click(object sender, EventArgs e)
        {

            int empid = Convert.ToInt32(ddlempid.SelectedValue.ToString());
            if (TxtEmpID.Text != "") { empid = Convert.ToInt32(TxtEmpID.Text.ToString()); }

            int DeptID = Convert.ToInt32(ddlDepartment.SelectedValue);
            int WorkSite = Convert.ToInt32(ddlWorksite.SelectedValue);
            int ShiftID = Convert.ToInt32(ddlShift.SelectedValue);

            DataSet dss = SQLDBUtil.ExecuteDataset("sh_currentdateattendance");

            if (dss != null && dss.Tables.Count > 0 && dss.Tables[0].Rows.Count > 0)
                Response.Redirect("SiteEmployeeWorkDetails.aspx?DID=" + DeptID + "&WS=" + WorkSite + "&Shift=" + ShiftID + "&Empid=" + empid, true);
            else
                AlertMsg.MsgBox(Page, "To see report you need attendance marked", AlertMsg.MessageType.Warning);


        }
        protected void btnOK_Click(object sender, EventArgs e)
        {
            //if (ddlAttType.SelectedValue != "0")
            //{
            if (ddlWorksite.SelectedValue != "0")
            {
                objHrCommon.CurrentPage = AddAttpaging.CurrentPage;
                objHrCommon.PageSize = AddAttpaging.ShowRows;
                int AttType = Convert.ToInt32(ddlAttType.SelectedValue);
                DataSet ds = new DataSet("dsList");
                DataTable dt = new DataTable("dtList");
                dt.Columns.Add("EmpID", typeof(int));
                dt.Columns.Add("ProjectId", typeof(int));
                ds.Tables.Add(dt);
                foreach (GridViewRow gvr in gdvAttend.Rows)
                {
                    CheckBox chk = (CheckBox)gvr.FindControl("chkSelect");
                    if (chk.Checked)
                    {
                        DropDownList ddlGProjects = (DropDownList)gvr.FindControl("ddlGProjects");
                        DropDownList ddlAttTypes = (DropDownList)gvr.FindControl("ddlStatus");

                        Label lblEmpID = (Label)gvr.FindControl("lblEmpID");
                        DataRow dr = ds.Tables[0].NewRow();
                        if (ddlGProjects.SelectedItem.Value != "0")
                            dr["ProjectId"] = ddlGProjects.SelectedValue;
                        else
                            dr["ProjectId"] = "0";
                        if (AttType == 0)
                        {
                            if (ddlAttTypes.SelectedItem.Value != "0")
                                AttType = Convert.ToInt32(ddlAttTypes.SelectedValue);
                            else
                                AttType = 0;
                        }
                        dr["EmpID"] = lblEmpID.Text.ToString();
                        ds.Tables[0].Rows.Add(dr);
                    }
                }

                if (ds.Tables[0].Rows.Count > 0)
                {
                    AjaxDAL.HR_UpdAttendance(ds, AttType, Convert.ToInt32(Session["UserId"]), null);
                    AttendanceDAC objAtt = new AttendanceDAC();

                    int WSid, DeptID;
                    try
                    {
                        WSid = Convert.ToInt32(ddlWorksite.SelectedValue);

                    }
                    catch
                    {
                        WSid = 0;

                    }
                    try
                    {
                        DeptID = Convert.ToInt32(ddlDepartment.SelectedValue);
                    }
                    catch
                    {
                        DeptID = 0;
                    }



                    //DataSet dstemp = objAtt.GetTodayAttendanceByPaging(objHrCommon, WSid, DeptID);
                    SqlParameter[] sqlParams = new SqlParameter[7];
                    sqlParams[0] = new SqlParameter("@CurrentPage", objHrCommon.CurrentPage);
                    sqlParams[1] = new SqlParameter("@PageSize", objHrCommon.PageSize);
                    sqlParams[2] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
                    sqlParams[2].Direction = ParameterDirection.ReturnValue;
                    sqlParams[3] = new SqlParameter("@NoofRecords", System.Data.SqlDbType.Int);
                    sqlParams[3].Direction = ParameterDirection.Output;
                    sqlParams[4] = new SqlParameter("@WSID", WSid);
                    sqlParams[5] = new SqlParameter("@DeptID", DeptID);
                    sqlParams[6] = new SqlParameter("@UserId", Convert.ToInt32(Session["UserId"]));
                    DataSet dstemp = SQLDBUtil.ExecuteDataset("HR_GetTodayAttendance_Paging_New", sqlParams);
                    objHrCommon.NoofRecords = (int)sqlParams[3].Value;
                    objHrCommon.TotalPages = (int)sqlParams[2].Value;
                    gdvAttend.DataSource = dstemp.Tables[2];
                    gdvAttend.DataBind();
                    ddlAttType.SelectedValue = "0";
                }
                AddAttpaging.Bind(objHrCommon.CurrentPage, objHrCommon.TotalPages, objHrCommon.NoofRecords, objHrCommon.PageSize);
            }
            else
                AlertMsg.MsgBox(Page, "Select the Worksite", AlertMsg.MessageType.Info);
           // Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction1", "MyFunction()", true);
            if (Convert.ToInt32(Session["UserId"]) == 1)
            {
                //    exp.Style.Add("style", "display:none");
                //    used.Style.Add("style", "display:none");
                dvreverse.Style.Add("style", "display:none");
                dvreverse.Visible = true;
                dvreverse1.Visible = true;
                dvreverse2.Visible = true;
            }
            else
            {
                dvreverse.Style.Add("style", "display:block");
                dvreverse.Visible = false;
                dvreverse1.Visible = false;
                dvreverse2.Visible = false;
                //    exp.Style.Add("style", "display:block");
                //    used.Style.Add("style", "display:block");
            }
            //}
            //else
            //    AlertMsg.MsgBox(Page, "Select the Attendance Type", AlertMsg.MessageType.Info);
        }
        protected void btnDayExporttoExcel_Click(object sender, EventArgs e)
        {
            AttendanceDAC objAtt = new AttendanceDAC();
            DataSet dstemp = new DataSet();
            int? DeptID = null;
            if (ddlDepartment.SelectedValue != "0")
                DeptID = Convert.ToInt32(ddlDepartment.SelectedValue);
            int? ShiftID = null;
            if (ddlShift.SelectedValue != "0" && ddlShift.SelectedValue != "")
                ShiftID = Convert.ToInt32(ddlShift.SelectedValue);
            SqlDataReader dr = objAtt.GetTodayAttendanceForExportExcel(DeptID, Convert.ToInt32(ddlWorksite.SelectedValue), ShiftID);
            ExportFileUtil.ExportToExcel(dr, "Attendance");
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("AttendanceImportExcel.aspx");
        }

        internal static DataSet HR_GetWorkSite_By_SMSEmpAttendance()
        {
            try
            {
                DataSet ds = AttendanceDAC.HR_GetWorkSite_By_SMSEmpAttendance();
                return ds;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            GetEmpIDSearch();
        }

        protected void GetEmpIDSearch()
        {
            AttendanceDAC objAtt = new AttendanceDAC();
            DataSet dstemp = new DataSet();
            int DeptID;
            int empid;
            try
            {
                DeptID = Convert.ToInt32(ddlDepartment.SelectedValue);
            }
            catch
            {
                DeptID = 0;
            }
            try
            {
                empid = Convert.ToInt32(ddlempid.SelectedValue.ToString());
                if (TxtEmpID.Text != "") { empid = Convert.ToInt32(TxtEmpID.Text.ToString()); }

            }
            catch
            {
                empid = 0;
            }
            string Name;
            try
            {
                Name = txtename.Text;
            }
            catch
            {
                Name = null;
            }
            int wsid = Convert.ToInt32(ddlWorksite.SelectedValue);
            try
            {

                if (Convert.ToInt32(ViewState["WSID"]) > 0)
                {
                    objHrCommon.SiteID = Convert.ToInt32(ViewState["WSID"]);
                    wsid = Convert.ToInt32(ViewState["WSID"]);
                }

            }
            catch { }

            objHrCommon.CurrentPage = AddAttpaging.CurrentPage;
            objHrCommon.PageSize = AddAttpaging.ShowRows;

            dstemp = objAtt.HR_GetTodayAttendance_Paging_Empid(objHrCommon, wsid, DeptID, empid, Name);
            if (dstemp.Tables[2].Rows.Count >= 1)
            {
                gdvAttend.DataSource = dstemp.Tables[2];
                gdvAttend.DataBind();
                ddlShift.SelectedIndex = 1;
            }
            else
            {
                gdvAttend.DataSource = null;
                gdvAttend.DataBind();
                ddlShift.SelectedIndex = 0;
            }
            AddAttpaging.Bind(objHrCommon.CurrentPage, objHrCommon.TotalPages, objHrCommon.NoofRecords, objHrCommon.PageSize);
        }
        protected void btnIMportExcel_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("AttendanceImportExcel.aspx?TypeID=1");
            }
            catch { }
        }

        protected void ddlProject_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGrid();
        }

        private void BindGrid()
        {
            int wsid = Convert.ToInt32(ddlWorksite.SelectedValue);
            AttendanceDAC objAtt = new AttendanceDAC();
            int DeptID;
            try
            {
                DeptID = Convert.ToInt32(ddlDepartment.SelectedValue);
            }
            catch
            {
                DeptID = 0;
            }
            objHrCommon.CurrentPage = AddAttpaging.CurrentPage;
            objHrCommon.PageSize = AddAttpaging.ShowRows;

            try
            {

                if (Convert.ToInt32(ViewState["WSID"]) > 0)
                {
                    objHrCommon.SiteID = Convert.ToInt32(ViewState["WSID"]);
                    wsid = Convert.ToInt32(ViewState["WSID"]);
                }

            }
            catch { }
            //DataSet dstemp = objAtt.GetTodayAttendanceByPaging(objHrCommon, wsid, DeptID);
            SqlParameter[] sqlParams = new SqlParameter[7];
            sqlParams[0] = new SqlParameter("@CurrentPage", objHrCommon.CurrentPage);
            sqlParams[1] = new SqlParameter("@PageSize", objHrCommon.PageSize);
            sqlParams[2] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
            sqlParams[2].Direction = ParameterDirection.ReturnValue;
            sqlParams[3] = new SqlParameter("@NoofRecords", System.Data.SqlDbType.Int);
            sqlParams[3].Direction = ParameterDirection.Output;
            sqlParams[4] = new SqlParameter("@WSID", wsid);
            sqlParams[5] = new SqlParameter("@DeptID", DeptID);
            sqlParams[6] = new SqlParameter("@UserId", Convert.ToInt32(Session["UserId"]));
            DataSet dstemp = SQLDBUtil.ExecuteDataset("HR_GetTodayAttendance_Paging_New", sqlParams);
            objHrCommon.NoofRecords = (int)sqlParams[3].Value;
            objHrCommon.TotalPages = (int)sqlParams[2].Value;
            DataSet ds = AttendanceDAC.HR_ddl_SearchEmpBySiteDept(wsid);
            ddlempid.DataSource = ds.Tables[0];
            ddlempid.DataValueField = "EmpID";
            ddlempid.DataTextField = "name";

            ddlempid.DataBind();
            ddlempid.Items.Insert(0, new ListItem("---ALL---", "0", true));
            if (dstemp.Tables[2].Rows.Count >= 1)
            {
                gdvAttend.DataSource = dstemp.Tables[2];
                gdvAttend.DataBind();
                ddlShift.SelectedIndex = 1;
            }
            else
            {
                gdvAttend.DataSource = null;
                gdvAttend.DataBind();
                ddlShift.SelectedIndex = 0;
            }
            AddAttpaging.Bind(objHrCommon.CurrentPage, objHrCommon.TotalPages, objHrCommon.NoofRecords, objHrCommon.PageSize);

        }

    }
}