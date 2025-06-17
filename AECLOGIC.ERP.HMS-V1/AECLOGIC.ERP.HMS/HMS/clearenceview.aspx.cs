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
    public partial class WebForm1 : AECLOGIC.ERP.COMMON.WebFormMaster
    {
        int mid = 0;
        bool viewall, Editable;
        int cidmast = 0;
        string menuname;
        string menuid;
        string name = "";
        // string //;
        string ModuleId = System.Configuration.ConfigurationManager.AppSettings["ModuleId"];
        int CompanyID = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["CompanyID"]);
        HRCommon objHrCommon = new HRCommon();
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
        void EmpListPaging_FirstClick(object sender, EventArgs e)
        {
            BindPager();
        }
        void EmpListPaging_ShowRowsClick(object sender, EventArgs e)
        {
            EmpListPaging.CurrentPage = 1;
            BindPager();
        }
        void BindPager()
        {
            objHrCommon.PageSize = EmpListPaging.CurrentPage;
            objHrCommon.CurrentPage = EmpListPaging.ShowRows;
            EmployeclearenceBind(objHrCommon);
        }
        void EmployeclearenceBind(HRCommon objHrCommon)
         {
            try
            {
                objHrCommon.PageSize = EmpListPaging.ShowRows;
                objHrCommon.CurrentPage = EmpListPaging.CurrentPage;
                int? EmpID = null;
                int? WS = null;
                int? Dept = null;
                EmpID = Convert.ToInt32(Request.QueryString["Empid"].ToString());
                SqlParameter[] parms = new SqlParameter[11];
                parms[0] = new SqlParameter("@clrearingemployeeid", DBNull.Value);
                parms[1] = new SqlParameter("@id", 2);
                parms[2] = new SqlParameter("@CurrentPage", objHrCommon.CurrentPage);
                parms[3] = new SqlParameter("@PageSize", objHrCommon.PageSize);
                parms[4] = new SqlParameter("@NoofRecords", 3);
                parms[4].Direction = ParameterDirection.Output;
                parms[5] = new SqlParameter();
                parms[5].Direction = ParameterDirection.ReturnValue;
                parms[6] = new SqlParameter("@Empid", EmpID);
                parms[7] = new SqlParameter("@Remarks", DBNull.Value);
                parms[8] = new SqlParameter("@isapproved", DBNull.Value);
                parms[9] = new SqlParameter("@cidmast", DBNull.Value);
                parms[10] = new SqlParameter("@clearid", DBNull.Value);
                DataSet dss = SqlHelper.ExecuteDataset("HR_InsUpClearenceEMP", parms);
                if (dss.Tables[0].Rows.Count == 0)
                {
                    int empid = Convert.ToInt32(Request.QueryString["Empid"].ToString());
                    SqlParameter[] sqlParams = new SqlParameter[7];
                    sqlParams[0] = new SqlParameter("@CurrentPage", objHrCommon.CurrentPage);
                    sqlParams[1] = new SqlParameter("@PageSize", objHrCommon.PageSize);
                    sqlParams[2] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
                    sqlParams[2].Direction = ParameterDirection.ReturnValue;
                    sqlParams[3] = new SqlParameter("@NoofRecords", 3);
                    sqlParams[3].Direction = ParameterDirection.Output;
                    sqlParams[4] = new SqlParameter("@Dept", Dept);
                    sqlParams[5] = new SqlParameter("@CompanyID", CompanyID);
                    sqlParams[6] = new SqlParameter("@empid", empid);
                    DataSet ds1 = SqlHelper.ExecuteDataset("HR_clearenceview", sqlParams);
                    objHrCommon.NoofRecords = (int)sqlParams[3].Value;
                    objHrCommon.TotalPages = (int)sqlParams[2].Value;
                    // ds = AttendanceDAC.HR_Getemployeeclearence(objHrCommon, Dept, Convert.ToInt32(Session["CompanyID"]), txtname.Text, empid);
                    if (ds1 != null && ds1.Tables.Count != 0 && ds1.Tables[ds1.Tables.Count-1].Rows.Count > 0)
                    {
                        gdvWSclr.DataSource = ds1.Tables[ds1.Tables.Count-1];
                        gdvWSclr.DataBind();
                    }
                    else
                    {
                      //  lnkclearedlist.Visible = true;
                        gdvWSclr.DataSource = null;
                        gdvWSclr.DataBind();
                        Label2.Visible = false;
                    }
                }
                else
                {
                    gdvWSclr.DataSource = dss;
                    gdvWSclr.DataBind();
                    int i = 0;
                    foreach (GridViewRow gvRow in gdvWSclr.Rows)
                    {
                        DropDownList ddlMachinery = (DropDownList)gvRow.Cells[0].FindControl("ddlMachinery");
                        ddlMachinery.SelectedValue = dss.Tables[0].Rows[i]["clrearingemployeeid"].ToString();
                        TextBox txtRemarks = (TextBox)gvRow.Cells[0].FindControl("txtRemarks");
                        txtRemarks.Text = dss.Tables[0].Rows[i]["Remarks"].ToString();
                        CheckBox chkapp = (CheckBox)gvRow.Cells[0].FindControl("chkApprove");
                        if (Convert.ToBoolean(dss.Tables[0].Rows[i]["isapproved"].ToString()) == true)
                            chkapp.Checked = true;
                        else
                            chkapp.Checked = false;
                        i++;
                    }
                }
                EmpListPaging.Bind(objHrCommon.CurrentPage, objHrCommon.TotalPages, objHrCommon.NoofRecords, objHrCommon.PageSize);
            }
            catch (Exception e)
            {
                clsErrorLog.HMSEventLog(e, "Clearenceview", "EmployeclearenceBind", "005");
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            gdvWSclr.DataSource = null;
            if (!IsPostBack)
            {
                gdvWSclr.DataSource = null;
                gdvWSclr.DataBind();
                EmployeclearenceBind(objHrCommon);
                txtname.Text = String.Empty;
                mainview.ActiveViewIndex = 0;
                name = Request.QueryString["Name"].ToString();
                lblname.Text = name;
            }
        }
        protected void gdvWSclr_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            foreach(GridViewRow gvr in gdvWSclr.Rows)
            {
                Label lblDeptID = (Label)gvr.FindControl("lblDept");
                DropDownList ddlMachinery = (DropDownList)gvr.FindControl("ddlMachinery");
                SqlParameter[] sp = new SqlParameter[1];
                sp[0] = new SqlParameter("@DeptNo", Convert.ToInt32(lblDeptID.Text));
                DataSet dsMachinery = SQLDBUtil.ExecuteDataset("Get_EmpName_ByDeptNo", sp);
                ddlMachinery.DataSource = dsMachinery;
                ddlMachinery.DataTextField = "Name";
                ddlMachinery.DataValueField = "empid";
                ddlMachinery.DataBind();
                ddlMachinery.Items.Insert(0, new ListItem("--Select--"));
              int  deptid = Convert.ToInt32(lblDeptID.Text);
              if (deptid == 4)
                {
                    ddlMachinery.Visible = false;
                 }
              else
              {
                  LinkButton lnkview = (LinkButton)gvr.FindControl("lnkmms");
                  lnkview.Text = "Process";
              }
                //pms for cleared text
              if (deptid == 6 || deptid == 10)
                {
                   int  EmpID = Convert.ToInt32(Request.QueryString["Empid"].ToString());
                objHrCommon.PageSize = EmpListPaging.ShowRows;
                objHrCommon.CurrentPage = EmpListPaging.CurrentPage;
                SqlParameter[] sqlParams = new SqlParameter[5];
                sqlParams[0] = new SqlParameter("@CurrentPage", objHrCommon.CurrentPage);
                sqlParams[1] = new SqlParameter("@PageSize", objHrCommon.PageSize);
                sqlParams[2] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
                sqlParams[2].Direction = ParameterDirection.ReturnValue;
                sqlParams[3] = new SqlParameter("@NoofRecords", System.Data.SqlDbType.Int);
                sqlParams[3].Direction = ParameterDirection.Output;
                sqlParams[4] = new SqlParameter("@Empid", EmpID);
                DataSet ds = SQLDBUtil.ExecuteDataset("Get_pms_Invoice_billing", sqlParams);
                objHrCommon.NoofRecords = (int)sqlParams[3].Value;
                objHrCommon.TotalPages = (int)sqlParams[2].Value;
                    if ( ds.Tables[0].Rows.Count == 0)
                    {
                        SqlParameter[] p = new SqlParameter[1];
                        p[0] = new SqlParameter("@empid", EmpID);
                        DataSet dsPMS=SqlHelper.ExecuteDataset("Sp_PMS_Clear",p);
                        DropDownList ddlmachinery = (DropDownList)gvr.FindControl("ddlMachinery");
                        ddlmachinery.SelectedValue = dsPMS.Tables[0].Rows[0]["TakingOverEmpid"].ToString();
                        Label lblgvhd = (Label)gvr.FindControl("lblgvhd");
                        lblgvhd.Text = "Cleared";
                        LinkButton lnkview = (LinkButton)gvr.FindControl("lnkmms");
                        lnkview.Text = "View";
                    }
                }
                //mms for cleared text
              if (deptid == 13)
              {
                int  EmpID = Convert.ToInt32(Request.QueryString["Empid"].ToString());
                  objHrCommon.PageSize = EmpListPaging.ShowRows;
                  objHrCommon.CurrentPage = EmpListPaging.CurrentPage;
                  SqlParameter[] param = new SqlParameter[8];
                  param[0] = new SqlParameter("@CurrentPage", objHrCommon.CurrentPage);
                  param[1] = new SqlParameter("@Pagesize", objHrCommon.PageSize);
                  param[2] = new SqlParameter("@ReturnValue", System.Data.SqlDbType.Int);
                  param[2].Direction = ParameterDirection.ReturnValue;
                  param[3] = new SqlParameter("@NoofRecords", System.Data.SqlDbType.Int);
                  param[3].Direction = ParameterDirection.Output;
                  param[4] = new SqlParameter("@ResourceID", DBNull.Value);
                  param[5] = new SqlParameter("@EmpID", EmpID);
                  param[6] = new SqlParameter("@WsID", DBNull.Value);
                  param[7] = new SqlParameter("@DeptID", DBNull.Value);
                  DataSet ds = SQLDBUtil.ExecuteDataset("MMS_GetEmpclearence_PersonalLoans", param);
                  objHrCommon.NoofRecords = (int)param[3].Value;
                  objHrCommon.TotalPages = (int)param[2].Value;
                  if (ds.Tables[0].Rows.Count == 0)
                  {
                      SqlParameter[] p = new SqlParameter[1];
                      p[0] = new SqlParameter("@empid", EmpID);
                      DataSet dsPMS = SqlHelper.ExecuteDataset("Sp_MMS_Clear", p);
                      DropDownList ddlmachinery = (DropDownList)gvr.FindControl("ddlMachinery");
                      ddlmachinery.SelectedValue = dsPMS.Tables[0].Rows[0]["TakingOverEmpid"].ToString();
                      Label lblgvhd = (Label)gvr.FindControl("lblgvhd");
                      lblgvhd.Text = "Cleared";
                      LinkButton lnkview = (LinkButton)gvr.FindControl("lnkmms");
                      lnkview.Text = "View";
                  }
              }
                //ems for cleared text
              if (deptid == 12)
              {
                  int EmpID = Convert.ToInt32(Request.QueryString["Empid"].ToString());
                  objHrCommon.PageSize = EmpListPaging.ShowRows;
                  objHrCommon.CurrentPage = EmpListPaging.CurrentPage;
                  SqlParameter[] sqlParams = new SqlParameter[9];
                  sqlParams[0] = new SqlParameter("@CurrentPage", objHrCommon.CurrentPage);
                  sqlParams[1] = new SqlParameter("@PageSize", objHrCommon.PageSize);
                  sqlParams[2] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
                  sqlParams[2].Direction = ParameterDirection.ReturnValue;
                  sqlParams[3] = new SqlParameter("@NoofRecords", System.Data.SqlDbType.Int);
                  sqlParams[3].Direction = ParameterDirection.Output;
                  sqlParams[4] = new SqlParameter("@SiteID", objHrCommon.SiteID);
                  sqlParams[5] = new SqlParameter("@EqipmetName", DBNull.Value);
                  if (EmpID != 0)
                  {
                      sqlParams[6] = new SqlParameter("@EmpID", EmpID);
                  }
                  else
                  {
                      sqlParams[6] = new SqlParameter("@EmpID", System.Data.SqlDbType.Int);
                  }
                  sqlParams[7] = new SqlParameter("@ResID", DBNull.Value);
                  if (CompanyID != 0)
                      sqlParams[8] = new SqlParameter("@CompanyID", CompanyID);
                  else
                      sqlParams[8] = new SqlParameter("@CompanyID", System.Data.SqlDbType.Int);
                  DataSet ds = SQLDBUtil.ExecuteDataset("EMS_GetMachineManager", sqlParams);
                  objHrCommon.NoofRecords = (int)sqlParams[3].Value;
                  objHrCommon.TotalPages = (int)sqlParams[2].Value;
                  if (ds.Tables[0].Rows.Count == 0)
                  {
                      SqlParameter[] p = new SqlParameter[1];
                      p[0] = new SqlParameter("@empid", EmpID);
                      DataSet dsPMS = SqlHelper.ExecuteDataset("Sp_EMS_Clear", p);
                      DropDownList ddlmachinery = (DropDownList)gvr.FindControl("ddlMachinery");
                      ddlmachinery.SelectedValue = dsPMS.Tables[0].Rows[0]["TakingOverEmpid"].ToString();
                      Label lblgvhd = (Label)gvr.FindControl("lblgvhd");
                      lblgvhd.Text = "Cleared";
                      LinkButton lnkview = (LinkButton)gvr.FindControl("lnkmms");
                      lnkview.Text = "View";
                  }
              }
              //for regestration
                if(deptid==14)
                {
                    objHrCommon.PageSize = EmpListPaging.ShowRows;
                    objHrCommon.CurrentPage = EmpListPaging.CurrentPage;
                    int EmpID = Convert.ToInt32(Request.QueryString["Empid"].ToString());
                    SqlParameter[] sqlPar = new SqlParameter[5];
                    sqlPar[0] = new SqlParameter("@CurrentPage", objHrCommon.CurrentPage);
                    sqlPar[1] = new SqlParameter("@PageSize", objHrCommon.PageSize);
                    sqlPar[2] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
                    sqlPar[2].Direction = ParameterDirection.ReturnValue;
                    sqlPar[3] = new SqlParameter("@NoofRecords", System.Data.SqlDbType.Int);
                    sqlPar[3].Direction = ParameterDirection.Output;
                    sqlPar[4] = new SqlParameter("@Empid", EmpID);
                    DataSet ds1 = SQLDBUtil.ExecuteDataset("Get_Emsstock", sqlPar);
                    objHrCommon.NoofRecords = (int)sqlPar[3].Value;
                    objHrCommon.TotalPages = (int)sqlPar[2].Value;
                    if (ds1.Tables[0].Rows.Count == 0)
                    {
                        SqlParameter[] p = new SqlParameter[1];
                        p[0] = new SqlParameter("@empid", EmpID);
                        DataSet dsPMS = SqlHelper.ExecuteDataset("Sp_EMS_regestration", p);
                        DropDownList ddlmachinery = (DropDownList)gvr.FindControl("ddlMachinery");
                        ddlmachinery.SelectedValue = dsPMS.Tables[0].Rows[0]["TakingOverEmpid"].ToString();
                        Label lblgvhd = (Label)gvr.FindControl("lblgvhd");
                        lblgvhd.Text = "Cleared";
                        LinkButton lnkview = (LinkButton)gvr.FindControl("lnkmms");
                        lnkview.Text = "View";
                    }
                }
            }
        }
        int Empid;
        protected void gdvWSclr_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "incostod")
            {
                Empid = Convert.ToInt32(Request.QueryString["Empid"].ToString());
                GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                Label lblDept = (Label)row.FindControl("lblDept");
                Label lblEmpID = (Label)gdvWSclr.FindControl("lblEmpID");
                GridViewRow gvr = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                DropDownList ddlmarchinayempid = (DropDownList)gvr.FindControl("ddlMachinery");
                Session["Empid"] = Empid;
                Session["Empname"] =Request.QueryString["Name"].ToString();
                Session["TakeHoverempid"] = ddlmarchinayempid.SelectedValue;
                Session["TakeHoverEmpname"] = ddlmarchinayempid.SelectedItem.ToString();
                int str = Convert.ToInt32(e.CommandArgument);
                //for ems
                if (str == 12 ||str==14)
                {
                    GridViewRow gvr1 = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                    DropDownList ddlmarchinayempid1 = (DropDownList)gvr.FindControl("ddlMachinery");
                    string Deptid = lblDept.Text;
                    if (ddlmarchinayempid1.SelectedIndex == 0)
                    {
                        AlertMsg.MsgBox(Page, "Select Taking OverEmployee",AlertMsg.MessageType.Warning);
                    }
                    else
                    {
                        Empid = Convert.ToInt32(Request.QueryString["Empid"].ToString());
                        Label lblEmpID1 = (Label)gdvWSclr.FindControl("lblEmpID");
                        Response.Redirect("EmsStock.aspx?Empid=" + Empid + "&key=" + 1 + "&Name=" + name+"&Deptid="+Deptid);
                    }
                }
                //for pms
                if (str == 6 || str == 10)
                {
                    GridViewRow gvr1 = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                    DropDownList ddlmarchinayempid1 = (DropDownList)gvr.FindControl("ddlMachinery");
                    string Deptid = lblDept.Text;
                    Session["Deptid"] = Deptid;
                    if (ddlmarchinayempid1.SelectedIndex == 0)
                    {
                        AlertMsg.MsgBox(Page, "Select Taking OverEmployee", AlertMsg.MessageType.Warning);
                    }
                    else
                    {
                        Empid = Convert.ToInt32(Request.QueryString["Empid"].ToString());
                        Label lblEmpID1 = (Label)gdvWSclr.FindControl("lblEmpID");
                        Response.Redirect("Pms_invoice_clearence.aspx?Empid=" + Empid + "&key=" + 1 + "&Name=" + name+"&Deptid="+ Deptid);
                    }
                }
                //for hms
                if (str == 4)
                {
                    GridViewRow gvr1 = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                    DropDownList ddlmarchinayempid1 = (DropDownList)gvr.FindControl("ddlMachinery");
                    {
                        Empid = Convert.ToInt32(Request.QueryString["Empid"].ToString());
                        Label lblEmpID1 = (Label)gdvWSclr.FindControl("lblEmpID");
                        Response.Redirect("HMS_Loan_clearence.aspx?Empid=" + Empid + "&key=" + 1 + "&Name=" + name);
                    }
                }
                //for mms
                if (str == 13)
                {
                    GridViewRow gvr1 = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                    DropDownList ddlmarchinayempid1 = (DropDownList)gvr.FindControl("ddlMachinery");
                    string Deptid = lblDept.Text;
                    if (ddlmarchinayempid1.SelectedIndex == 0)
                    {
                        AlertMsg.MsgBox(Page, "Select Taking OverEmployee", AlertMsg.MessageType.Warning);
                    }
                    else
                    {
                        Response.Redirect("Checkcustodi.aspx?Empid=" + Empid + "&key=" + 1 + "&Name=" + name +"&Deptid="+ Deptid);
                    }
                }
            }
        }
        public void BindEMpByWO()
        {
            //int empid = 0;
            int EmpID = Convert.ToInt32(Request.QueryString["Empid"].ToString());
            SqlParameter[] parms = new SqlParameter[1];
            parms[0] = new SqlParameter("@EmpId", EmpID);
            DataSet ds = SQLDBUtil.ExecuteDataset("Get_EmpName_by_department", parms);
            DataRow dr;
            dr = ds.Tables[0].NewRow();
            dr[0] = 0;
            ds.Tables[0].Rows.InsertAt(dr, 0);
            ViewState["BindEMPBYWO"] = ds.Tables[0];
            foreach (GridViewRow gvRow in gdvWSclr.Rows)
            {
                DropDownList ddlMachinery = (DropDownList)gvRow.Cells[0].FindControl("ddlMachinery");
                DataSet dsMachinery = SQLDBUtil.ExecuteDataset("Get_EmpName_All");
                ddlMachinery.DataSource = dsMachinery;
                ddlMachinery.DataTextField = "Name";
                ddlMachinery.DataValueField = "empid";
                ddlMachinery.DataBind();
                ddlMachinery.Items.Insert(0, new ListItem("--Select--"));
            }
        }
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            int ID = 0;
            int empid = 0;
            gdvWSclr.DataSource = null;
            if (Request.QueryString.Count > 0)
            {
                empid = Convert.ToInt32(Request.QueryString["Empid"].ToString());
            }
            int ID1;
            SqlParameter[] parms = new SqlParameter[1];
            parms[0] = new SqlParameter("@Empid", empid);
            DataSet dst = SqlHelper.ExecuteDataset("HR_GetemployeeClearancemaster", parms);
            if (dst != null && dst.Tables.Count > 0 && dst.Tables[0].Rows.Count > 0)
            {
                cidmast = Convert.ToInt32(dst.Tables[0].Rows[0]["CIDMAST"].ToString());
            }
            else
            {
                cidmast = 0;
            }
            foreach (GridViewRow gvRow in gdvWSclr.Rows)
            {
                objHrCommon.PageSize = EmpListPaging.ShowRows;
                objHrCommon.CurrentPage = EmpListPaging.CurrentPage;
                Label lblClearID = (Label)gvRow.FindControl("lblClearID");
                CheckBox chkApprove = (CheckBox)gvRow.FindControl("chkApprove");
                bool isapproved = false;
                if (chkApprove.Checked)
                    isapproved = true;
                DropDownList ddlMach = (DropDownList)gvRow.FindControl("ddlMachinery");
                TextBox txtRemarks = (TextBox)gvRow.FindControl("txtRemarks");
                if (Convert.ToInt32(ddlMach.SelectedValue == "--Select--" ? "0" : ddlMach.SelectedValue) > 0)
                    ID1 = Convert.ToInt32(AttendanceDAC.HR_InsUpClearenceEMP(objHrCommon, ID, Convert.ToInt32(ddlMach.SelectedValue), txtRemarks.Text, Convert.ToInt32(lblClearID.Text), isapproved, empid, cidmast));
                else
                    ID1 = Convert.ToInt32(AttendanceDAC.HR_InsUpClearenceEMP(objHrCommon, ID, Convert.ToInt32(0), txtRemarks.Text, Convert.ToInt32(lblClearID.Text), isapproved, empid, cidmast));
                cidmast = ID1;
            }
            AlertMsg.MsgBox(Page, "Done");
            Response.Redirect("Clearence.aspx");
        }
        protected void btnback_Click(object sender, EventArgs e)
        {
            Response.Redirect("Clearence.aspx");
        }
    }
}