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
    public partial class VisitorsLog : AECLOGIC.ERP.COMMON.WebFormMaster
    {

        AttendanceDAC objAtt = new AttendanceDAC();
        DataSet ds = new DataSet();
        int mid = 0;
        bool viewall, Editable;
        string menuname;
        string menuid;
        DataSet dswork = new DataSet();
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
            try
            {
                string id =  Convert.ToInt32(Session["UserId"]).ToString();
            }
            catch
            {
                Response.Redirect("Home.aspx");
            }

            topmenu.MenuId = GetParentMenuId();
            topmenu.ModuleId = ModuleID;;
            topmenu.RoleID = Convert.ToInt32(Session["RoleId"].ToString());
            topmenu.SelectedMenu = Convert.ToInt32(mid.ToString());
            topmenu.DataBind();
            Session["menuname"] = menuname;
            Session["menuid"] = menuid;
            Session["MId"] = mid;

            if (!IsPostBack)
            {
                trremarks.Visible = false;
                trsoccurance.Visible = false;
                txtFromDate.Text = DateTime.Now.AddMonths(-1).ToString("dd/MM/yyyy");
                txtTodate.Text = DateTime.Now.ToString("dd/MM/yyyy");


                txtIntime.Text = DateTime.Now.ToString("hh:mm tt");
                txtOutTime.Text = DateTime.Now.ToString("hh:mm tt");
                txtDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                ViewState["VlogID"] = 0;
                BindPager();
            }
        }

        public int GetParentMenuId()
        {
            string URL = Request.Url.Segments[Request.Url.Segments.Length - 1] + Request.Url.Query;
            int RoleId = Convert.ToInt32(Session["RoleId"].ToString());
            int ModuleId = ModuleID;;

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
                //btnSave.Enabled = (bool)ds.Tables[0].Rows[0]["Editable"];
                mid = Convert.ToInt32(ds.Tables[0].Rows[0]["MenuId"].ToString());
                btnSubmit.Enabled = Editable;
            }
            return MenuId;
        }
        public void BindGrid(HRCommon objHrCommon)//int EmpID)
        {
            try
            {
                int EmpID =  Convert.ToInt32(Session["UserId"]);

                objHrCommon.PageSize = EmpWorkPaging.ShowRows;
                objHrCommon.CurrentPage = EmpWorkPaging.CurrentPage;
                dswork = AttendanceDAC.GetEmpVisitorLog(objHrCommon, CODEUtility.ConvertToDate(txtFromDate.Text.Trim(), DateFormat.DayMonthYear), CODEUtility.ConvertToDate(txtTodate.Text.Trim(), DateFormat.DayMonthYear), txtSeaSeeking.Text.Trim(), txtSeaCompnay.Text.Trim());

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
        public void BindDetails(int VlogID)
        {
            DataSet ds = new DataSet();
            ds = AttendanceDAC.GetEmpVisitorLogDets(VlogID);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {

                txtDate.Text = ds.Tables[0].Rows[0]["LogDate"].ToString();
                txtvisName.Text = ds.Tables[0].Rows[0]["VisitorName"].ToString();
                txtCompName.Text = ds.Tables[0].Rows[0]["CompanyName"].ToString();
                txtMobile.Text = ds.Tables[0].Rows[0]["MobileNum"].ToString();
                txtPurpose.Text = ds.Tables[0].Rows[0]["Purpose"].ToString();
                txtDesignation.Text = ds.Tables[0].Rows[0]["Designation"].ToString();
                txtRemarks.Text = ds.Tables[0].Rows[0]["Remarks"].ToString();
                txtIntime.Text = ds.Tables[0].Rows[0]["TimeIN"].ToString();
                txtOutTime.Text = ds.Tables[0].Rows[0]["TimeOut"].ToString();
            }
        }


        protected void btnSubmit_Click(object sender, EventArgs e)
        {

            if (txtDate.Text != "")
            {

                int EmpID = Convert.ToInt32(ViewState["EmpID"]);
                int VlogID = Convert.ToInt32(ViewState["VlogID"].ToString());
                Int64? MobileNo = null;

                if (txtMobile.Text.Trim() != "")
                {
                    MobileNo = Convert.ToInt64(txtMobile.Text.Trim());
                }
                int retval = Convert.ToInt32(AttendanceDAC.InsertVisitorLog(VlogID,
                                                                            CODEUtility.ConvertToDate(txtDate.Text.Trim(), DateFormat.DayMonthYear),
                                                                           txtIntime.Text,
                                                                           txtvisName.Text,
                                                                           txtCompName.Text,
                                                                           MobileNo,
                                                                           txtPurpose.Text,
                                                                           txtDesignation.Text,
                                                                           txtOutTime.Text, txtRemarks.Text,
                                                                            Convert.ToInt32(Session["UserId"])
                                                                           ));
                if (retval == 3)
                {
                    AlertMsg.MsgBox(Page, "Same data exist");
                }
                else
                {
                    AlertMsg.MsgBox(Page, "Done!");
                    BindPager();

                    txtDate.Text = "";
                    txtvisName.Text = "";
                    txtCompName.Text = "";
                    txtMobile.Text = "";
                    txtPurpose.Text = "";
                    txtDesignation.Text = "";
                    txtRemarks.Text = "";
                    txtIntime.Text = DateTime.Now.ToString("hh:mm tt");
                    txtOutTime.Text = DateTime.Now.ToString("hh:mm tt");
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
                    int VlogID = Convert.ToInt32(e.CommandArgument);
                    ViewState["VlogID"] = VlogID;
                    DataSet ds = new DataSet();
                    trremarks.Visible = true;
                    trsoccurance.Visible = true;
                    BindDetails(VlogID);
                    BindPager();
                    btnSubmit.Enabled = true;
                }

                if (e.CommandName == "Save")
                {
                    int VlogID = Convert.ToInt32(e.CommandArgument);
                    ViewState["VlogID"] = VlogID;

                    LinkButton lnkSave = new LinkButton();
                    GridViewRow selectedRow = (GridViewRow)(((Control)e.CommandSource).NamingContainer);
                    TextBox txtouttime = (TextBox)selectedRow.FindControl("txtOutTime");
                    TextBox txtremarks = (TextBox)selectedRow.FindControl("txtRemarks");

                    AttendanceDAC.UpdateVisLog(VlogID, txtouttime.Text, txtremarks.Text);
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
                if (txtOut.Text.Trim() == "")
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