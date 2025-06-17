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
    public partial class EmpMessAttendance : AECLOGIC.ERP.COMMON.WebFormMaster
    {
        #region Declaration
        int mid = 0;
        bool viewall;
        string menuname;
        string menuid;
        HRCommon objHrCommon = new HRCommon();
        AttendanceDAC objatt = new AttendanceDAC();
        #endregion Declaration

        #region Paging
        protected override void OnInit(EventArgs e)
        {
            ModuleID = 1;
            base.OnInit(e);
            EmpMessAttPaging.FirstClick += new Paging.PageFirst(EmpMessAttPaging_FirstClick);
            EmpMessAttPaging.PreviousClick += new Paging.PagePrevious(EmpMessAttPaging_FirstClick);
            EmpMessAttPaging.NextClick += new Paging.PageNext(EmpMessAttPaging_FirstClick);
            EmpMessAttPaging.LastClick += new Paging.PageLast(EmpMessAttPaging_FirstClick);
            EmpMessAttPaging.ChangeClick += new Paging.PageChange(EmpMessAttPaging_FirstClick);
            EmpMessAttPaging.ShowRowsClick += new Paging.ShowRowsChange(EmpMessAttPaging_ShowRowsClick);
            EmpMessAttPaging.CurrentPage = 1;
        }
        void EmpMessAttPaging_ShowRowsClick(object sender, EventArgs e)
        {
            EmpMessAttPaging.CurrentPage = 1;
            BindPager();
        }
        void EmpMessAttPaging_FirstClick(object sender, EventArgs e)
        {
            BindPager();
        }
        void BindPager()
        {

            objHrCommon.PageSize = EmpMessAttPaging.CurrentPage;
            objHrCommon.CurrentPage = EmpMessAttPaging.ShowRows;
            BindEmpDetails(objHrCommon);
        }
        #endregion Paging

        #region PageLoad
        protected void Page_Load(object sender, EventArgs e)
        {
           
            if (!IsPostBack)
            {
                GetMessCat();
                txtDate.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");
                FillWS();
                FillEmpNature();
               
                BindPager();
            }
        }
        #endregion PageLoad

        #region Fill DropDows And CheckBoxS
        public void FillWS()
        {
            FIllObject.FillDropDown(ref ddlWorkSite, "HR_GetWorkSite_By_EmpMessAttendance");
        }
        public void FillEmpNature()
        {
             
          DataSet  ds = Leaves.GetEmpNatureList(1);
            ddlEmpNature.DataSource = ds;
            ddlEmpNature.DataTextField = "Nature";
            ddlEmpNature.DataValueField = "NatureOfEmp";
            ddlEmpNature.DataBind();
            ddlEmpNature.Items.Insert(0, new ListItem("---All---", "0"));
        }
       
        public DataSet BindEmpAttendanceType()
        {

            DataSet ds = AttendanceDAC.GetTypeOfMessCofigs(1);
            return ds;
        }

        #endregion Fill DropDows And CheckBox

        #region SuportingMethods
     
        public void BindEmpDetails(HRCommon objHrCommon)
        {
            try
            {
                objHrCommon.PageSize = EmpMessAttPaging.ShowRows;
                objHrCommon.CurrentPage = EmpMessAttPaging.CurrentPage;
                int? WS = null;
                if (ddlWorkSite.SelectedValue != "0")
                    WS = Convert.ToInt32(ddlWorkSite.SelectedValue);
                int? Dept = null;
                if (ddlDept.SelectedValue != "0")
                    Dept = Convert.ToInt32(ddlDept.SelectedValue);
                int? EmpNatureID = null;
                if (ddlEmpNature.SelectedValue != "0")
                    EmpNatureID = Convert.ToInt32(ddlEmpNature.SelectedValue);
                DateTime datesrch = CODEUtility.ConvertToDate(txtDate.Text, DateFormat.DayMonthYear);
                int? EmpID = null;
                if ((bool)ViewState["ViewAll"] == false)
                    EmpID =  Convert.ToInt32(Session["UserId"]);


                DataSet ds = AttendanceDAC.GetEmpMessDetails(objHrCommon, WS, Dept, EmpNatureID, datesrch, EmpID, Convert.ToInt32(Session["CompanyID"]));
                if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0)
                {
                    grdEmpMessAtt.DataSource = ds;
                    EmpMessAttPaging.Bind(objHrCommon.CurrentPage, objHrCommon.TotalPages, objHrCommon.NoofRecords, objHrCommon.PageSize);
                }
                else
                {
                    grdEmpMessAtt.EmptyDataText = "No Records Found";
                    EmpMessAttPaging.Visible = false;
                }
                grdEmpMessAtt.DataBind();

            }
            catch (Exception)
            {
                throw;
            }

        }
        public void GetMessCat()
        {

            DataSet ds = AttendanceDAC.GetTypeOfMessCofigs(1);
            ChkLstAll.DataSource = ds;
            ChkLstAll.DataTextField = "Name";
            ChkLstAll.DataValueField = "MID";
            ChkLstAll.DataBind();
        }
        #endregion SuportingMethods

        #region Events


        protected void chkMessTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow gvr = (GridViewRow)(((Control)sender).NamingContainer);
            CheckBoxList chklist = (CheckBoxList)gvr.Cells[3].Controls[1];
            Label lblEmpID = (Label)gvr.Cells[0].Controls[1];
            int EmpID = Convert.ToInt32(lblEmpID.Text);
            Label lblWSID = (Label)gvr.Cells[4].Controls[1];
            int WSID = int.Parse(lblWSID.Text);
            int UserID =  Convert.ToInt32(Session["UserId"]);
            DateTime CreatedOn = CODEUtility.ConvertToDate(txtDate.Text, DateFormat.DayMonthYear);
            foreach (ListItem item in chklist.Items)
            {
                int MID = Convert.ToInt32(item.Value);
                int Status = 0;
                if (item.Selected)
                {
                    Status = 1;
                }
                AttendanceDAC.InsUpdEmpMessAtt(MID, EmpID, Status, WSID, UserID, CreatedOn);


            }
        }
        protected void grdEmpMessAtt_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int? WSID = null;
                int? DeptID = null;
                int? EmpNatureID = null;
                DateTime datesrch = CODEUtility.ConvertToDate(txtDate.Text, DateFormat.DayMonthYear);
                if (ddlWorkSite.SelectedValue != "0")
                    WSID = Convert.ToInt32(ddlWorkSite.SelectedValue);
                if (ddlDept.SelectedValue != "0")
                    DeptID = Convert.ToInt32(ddlDept.SelectedValue);
                if (ddlEmpNature.SelectedValue != "0")
                    EmpNatureID = Convert.ToInt32(ddlEmpNature.SelectedValue);
                int? EmployeeID = null;
                if ((bool)ViewState["ViewAll"] == false)
                    EmployeeID =  Convert.ToInt32(Session["UserId"]);

                DataSet ds = AttendanceDAC.GetMessAttDetails(WSID, DeptID, EmpNatureID, datesrch, EmployeeID);

                if ((bool)ViewState["ViewAll"] == true)
                {
                    #region 1
                    
                    Label lblEmpID = (Label)e.Row.FindControl("lblEmpID");
                    int EmpID = int.Parse(lblEmpID.Text);
                    CheckBoxList chklist = (CheckBoxList)e.Row.FindControl("chkMessTypes");
                    if (ds.Tables[1].Rows.Count != 0)
                    {
                        #region checkbox loop
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            if (EmpID == Convert.ToInt32(dr["EmpID"]))
                            {
                                foreach (ListItem item in chklist.Items)
                                {

                                    int MID = Convert.ToInt32(item.Value);
                                    #region 2
                                    if (MID == Convert.ToInt32(dr["MID"]))
                                    {
                                        if (Convert.ToInt32(dr["Status"]) == 1)
                                        {
                                            item.Selected = true;

                                        }
                                    }
                                    #endregion 2
                                    if ((bool)ViewState["ViewAll"] == false)
                                    {
                                        item.Enabled = false;
                                    }
                                }
                            }
                        }
                        #endregion checkbox loop
                       
                    }
                    #endregion 1
                    ddlWorkSite.Enabled = true;
                    ddlDept.Enabled = true;
                    ddlEmpNature.Enabled = true;
                    EmpMessAttPaging.Visible = true;
                }
                else
                {
                    Label lblEmpID = (Label)e.Row.FindControl("lblEmpID");
                    int EmpID = int.Parse(lblEmpID.Text);
                    CheckBoxList chklist = (CheckBoxList)e.Row.FindControl("chkMessTypes");
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        if (EmpID == Convert.ToInt32(dr["EmpID"]))
                        {
                            foreach (ListItem item in chklist.Items)
                            {

                                int MID = Convert.ToInt32(item.Value);
                                if (MID == Convert.ToInt32(dr["MID"]))
                                {
                                    if (Convert.ToInt32(dr["Status"]) == 1)
                                    {
                                        item.Selected = true;
                                    }
                                }
                                if ((bool)ViewState["ViewAll"] == false)
                                {
                                    item.Enabled = false;
                                }

                            }
                        }

                    }
                    foreach (ListItem item in chklist.Items)
                    {
                        if (ds.Tables[0].Rows.Count == 0)
                        {
                            item.Enabled = false;
                        }
                    }

                    ddlWorkSite.Enabled = false;
                    ddlDept.Enabled = false;
                    ddlEmpNature.Enabled = false;
                    EmpMessAttPaging.Visible = false;
                }
            }
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindPager();
        }
        protected void btnAll_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (GridViewRow gvr in grdEmpMessAtt.Rows)
                {
                    CheckBox chk = (CheckBox)gvr.FindControl("chkAll");             //Gridview checkbox list
                    if (chk.Checked)                                                //Gridview checkbox is checked
                    {
                        Label lblEmpId = (Label)gvr.Cells[0].FindControl("lblEmpID");
                        int EmpID = int.Parse(lblEmpId.Text);
                        Label lblWSID = (Label)gvr.Cells[4].FindControl("lblWS");
                        int WSID = int.Parse(lblWSID.Text);
                        DateTime CreatedOn = CODEUtility.ConvertToDate(txtDate.Text, DateFormat.DayMonthYear);
                       
                        for (int i = 0; i < ChkLstAll.Items.Count; i++)             //Search criteria checkbox list
                        {
                            int Status = 0;
                            if (ChkLstAll.Items[i].Selected == true)                //Searcg criteria checkbox is checked
                            {
                                Status = 1;
                            }
                            int MID = Convert.ToInt32(ChkLstAll.Items[i].Value);
                            int UserID =  Convert.ToInt32(Session["UserId"]);
                            AttendanceDAC.InsUpdEmpMessAtt(MID, EmpID, Status, WSID, UserID, CreatedOn);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                AlertMsg.MsgBox(Page, ex.Message.ToString(),AlertMsg.MessageType.Error);
            }
            BindPager();
        }
        #endregion Events

        protected void ddlWorkSite_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindDeparmetBySite(Convert.ToInt32(ddlWorkSite.SelectedValue));
        }

        public void BindDeparmetBySite(int Site)
        {
            DataSet ds = AttendanceDAC.BindDeparmetBySite(Site, Convert.ToInt32(Session["CompanyID"]));
            ddlDept.DataSource = ds;
            ddlDept.DataTextField = "DeptName";
            ddlDept.DataValueField = "DepartmentUId";
            ddlDept.DataBind();
            ddlDept.Items.Insert(0, new ListItem("---ALL---", "0", true));
        
        }
    }

}