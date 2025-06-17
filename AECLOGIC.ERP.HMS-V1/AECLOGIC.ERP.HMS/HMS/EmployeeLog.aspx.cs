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
    public partial class EmployeeLog : AECLOGIC.ERP.COMMON.WebFormMaster
    {
        AttendanceDAC objAtt = new AttendanceDAC();
         
        int mid = 0;
        bool viewall, Editable;
        string menuname;
        string menuid;
        HRCommon objHrCommon = new HRCommon();

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
            if (!IsPostBack)
            {
                txtFromDate.Text = DateTime.Now.AddMonths(-1).ToString("dd/MM/yyyy");
                txtTodate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                trremarks.Visible = false;
                trsoccurance.Visible = false;
                txtIntime.Text = DateTime.Now.ToString("hh:mm tt");
                txtOutTime.Text = DateTime.Now.ToString("hh:mm tt");
                txtDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                ViewState["ElogID"] = 0;

                BindWorkSites();
                BindEmployees();
                BindPager();
            }

        }
        private void BindWorkSites()
        {
             

          DataSet  ds = AttendanceDAC.GetWorkSite(0, '1', Convert.ToInt32(Session["CompanyID"]));
            ddlWorksite.DataSource = ds.Tables[0];
            ddlWorksite.DataTextField = "Site_Name";
            ddlWorksite.DataValueField = "Site_ID";
            ddlWorksite.DataBind();
            ddlWorksite.Items.Insert(0, new ListItem("---Select---", "0", true));
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
        public int GetParentMenuId()
        {
            string URL = Request.Url.Segments[Request.Url.Segments.Length - 1] + Request.Url.Query;
            int RoleId = Convert.ToInt32(Session["RoleId"].ToString());
            int ModuleId = ModuleID;;

         DataSet   ds = AttendanceDAC.GetAllowed(RoleId, ModuleId, URL);
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
                int EmpID =  Convert.ToInt32(Session["UserId"]);
                int? SeaEmpID = null;
                int? SeaWsID = null;
                if (ddlSeaEmp.SelectedItem.Value != "0")
                {
                    SeaEmpID = Convert.ToInt32(ddlSeaEmp.SelectedItem.Value);
                }
                objHrCommon.PageSize = EmpWorkPaging.ShowRows;
                objHrCommon.CurrentPage = EmpWorkPaging.CurrentPage;
                DataSet dswork = AttendanceDAC.GetEmpLog(objHrCommon, CODEUtility.ConvertToDate(txtFromDate.Text.Trim(), DateFormat.DayMonthYear), CODEUtility.ConvertToDate(txtTodate.Text.Trim(), DateFormat.DayMonthYear), SeaEmpID, null);

                if (dswork != null && dswork.Tables.Count != 0 && dswork.Tables[0].Rows.Count > 0)
                {
                    grdVisitorsLog.DataSource = dswork;
                    grdVisitorsLog.DataBind();
                }
                else
                {
                    grdVisitorsLog.DataSource = null;
                    grdVisitorsLog.DataBind();

                }
                EmpWorkPaging.Bind(objHrCommon.CurrentPage, objHrCommon.TotalPages, objHrCommon.NoofRecords, objHrCommon.PageSize);

            }
            catch (Exception e)
            {
                throw e;
            }

        }
        public void BindDetails(int ElogID)
        {

            DataSet ds = AttendanceDAC.GetEmpLogDets(ElogID);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {

                txtDate.Text = ds.Tables[0].Rows[0]["LogDate"].ToString();
                txtDate.Enabled = false;
                ddlEmp.SelectedValue = ds.Tables[0].Rows[0]["EmpID"].ToString();
                ddlEmp.Enabled = false;
                txtCompName.Text = ds.Tables[0].Rows[0]["CompanyName"].ToString();
                txtPurpose.Text = ds.Tables[0].Rows[0]["Purpose"].ToString();
                txtRemarks.Text = ds.Tables[0].Rows[0]["Remarks"].ToString();
                txtIntime.Text = ds.Tables[0].Rows[0]["TimeIN"].ToString();
                txtIntime.Enabled = false;
                txtOutTime.Text = ds.Tables[0].Rows[0]["TimeOut"].ToString();
                if (txtOutTime.Text == string.Empty)
                {
                    txtOutTime.Text = DateTime.Now.ToString("hh:mm tt");

                }

                if (ds.Tables[0].Rows[0]["Worksite"].ToString() != string.Empty && ds.Tables[0].Rows[0]["Worksite"].ToString() != "0")
                {
                    ddlWorksite.SelectedValue = ds.Tables[0].Rows[0]["Worksite"].ToString();

                }
            }
        }


        protected void btnSubmit_Click(object sender, EventArgs e)
        {

            if (txtDate.Text != string.Empty)
            {

                int EmpID = Convert.ToInt32(ViewState["EmpID"]);
                int ElogID = Convert.ToInt32(ViewState["ElogID"].ToString());
                int WsID = 0;
                if (ddlWorksite.SelectedItem.Value != "0")
                {
                    WsID = Convert.ToInt32(ddlWorksite.SelectedItem.Value);
                }
                Int64? MobileNo = null;


                int retval = Convert.ToInt32(AttendanceDAC.InsertEmpLog(ElogID,
                                                                            CODEUtility.ConvertToDate(txtDate.Text.Trim(), DateFormat.DayMonthYear),
                                                                           txtIntime.Text,
                                                                           Convert.ToInt32(ddlEmp.SelectedItem.Value),
                                                                           txtCompName.Text,
                                                                           txtPurpose.Text,
                                                                           txtOutTime.Text,
                                                                           txtRemarks.Text,
                                                                            Convert.ToInt32(Session["UserId"])
                                                                           , WsID));
                if (retval == 3)
                {
                    AlertMsg.MsgBox(Page, "Same data exist");
                }
                else
                {
                    AlertMsg.MsgBox(Page, "Done!");
                    BindPager();

                    txtDate.Text = string.Empty;
                    txtCompName.Text = string.Empty;
                    txtPurpose.Text = string.Empty;
                    txtRemarks.Text = string.Empty;
                    txtIntime.Text = DateTime.Now.ToString("hh:mm tt");
                    txtOutTime.Text = DateTime.Now.ToString("hh:mm tt");
                    ddlEmp.SelectedItem.Value = "0";
                    ddlWorksite.SelectedItem.Value = "0";

                }
            }
            else
            {
                AlertMsg.MsgBox(Page, "Select Date");
            }
        }
        protected void grdVisitorsLog_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {

                if (e.CommandName == "Edt")
                {
                    int ElogID = Convert.ToInt32(e.CommandArgument);
                    ViewState["ElogID"] = ElogID;
                     
                    trremarks.Visible = true;
                    trsoccurance.Visible = true;
                    BindDetails(ElogID);
                    BindPager();
                    btnSubmit.Enabled = true;
                }

                if (e.CommandName == "Save")
                {
                    int ElogID = Convert.ToInt32(e.CommandArgument);
                    ViewState["ElogID"] = ElogID;

                    LinkButton lnkSave = new LinkButton();
                    GridViewRow selectedRow = (GridViewRow)(((Control)e.CommandSource).NamingContainer);
                    TextBox txtouttime = (TextBox)selectedRow.FindControl("txtOutTime");
                    TextBox txtremarks = (TextBox)selectedRow.FindControl("txtRemarks");

                    AttendanceDAC.UpdateEmpLog(ElogID, txtouttime.Text, txtremarks.Text);
                }
            }
            catch (Exception EmpWorkDel)
            {
                AlertMsg.MsgBox(Page, EmpWorkDel.Message.ToString(),AlertMsg.MessageType.Error);
            }
        }



        protected void grdVisitorsLog_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                TextBox txtOut = (TextBox)e.Row.FindControl("txtOutTime");
                TextBox txtremrks = (TextBox)e.Row.FindControl("txtRemarks");
                LinkButton lnkSave = (LinkButton)e.Row.FindControl("lnkSave");
                if (txtOut.Text.Trim() == string.Empty)
                {
                    txtOut.Text = DateTime.Now.ToString("hh:mm tt");
                }
                else
                {
                    txtOut.Enabled = false;
                    txtremrks.Enabled = false;
                    lnkSave.Enabled = false;
                }
            }
        }
        protected void btnDaySearch_Click(object sender, EventArgs e)
        {
            BindPager();
        }
    }

}