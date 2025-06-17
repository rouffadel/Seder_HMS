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



namespace AECLOGIC.ERP.HMS
{
    public partial class EmployeeAttendanceDevice : AECLOGIC.ERP.COMMON.WebFormMaster
    {
      
        bool Editable;

        AttendanceDAC objAtt = new AttendanceDAC();
         
        HRCommon objHrCommon = new HRCommon();
        protected override void OnInit(EventArgs e)
        {
            ModuleID = 1;
            base.OnInit(e);
            EmployeeAttendanceDevicePaging.FirstClick += new Paging.PageFirst(EmployeeAttendanceDevicePaging_FirstClick);
            EmployeeAttendanceDevicePaging.PreviousClick += new Paging.PagePrevious(EmployeeAttendanceDevicePaging_FirstClick);
            EmployeeAttendanceDevicePaging.NextClick += new Paging.PageNext(EmployeeAttendanceDevicePaging_FirstClick);
            EmployeeAttendanceDevicePaging.LastClick += new Paging.PageLast(EmployeeAttendanceDevicePaging_FirstClick);
            EmployeeAttendanceDevicePaging.ChangeClick += new Paging.PageChange(EmployeeAttendanceDevicePaging_FirstClick);
            EmployeeAttendanceDevicePaging.ShowRowsClick += new Paging.ShowRowsChange(EmployeeAttendanceDevicePaging_ShowRowsClick);
            EmployeeAttendanceDevicePaging.CurrentPage = 1;
        }
        void EmployeeAttendanceDevicePaging_ShowRowsClick(object sender, EventArgs e)
        {
            EmployeeAttendanceDevicePaging.CurrentPage = 1;
            BindPager();
        }
        void EmployeeAttendanceDevicePaging_FirstClick(object sender, EventArgs e)
        {
            BindPager();
        }
        void BindPager()
        {

            objHrCommon.PageSize = EmployeeAttendanceDevicePaging.CurrentPage;
            objHrCommon.CurrentPage = EmployeeAttendanceDevicePaging.ShowRows;
            EmployeeAttendanceDeviceBind(objHrCommon);

        }
        public void EmployeeAttendanceDeviceBind(HRCommon objHrCommon)
        {
            try
            {

                objHrCommon.PageSize = EmployeeAttendanceDevicePaging.ShowRows;
                objHrCommon.CurrentPage = EmployeeAttendanceDevicePaging.CurrentPage;
                int DeviceID = Convert.ToInt32(ddlDevName.SelectedValue);
                 
              DataSet  ds = AttendanceDAC.HR_GetEmployeeAttendanceDeviceByPaging(objHrCommon, 1, DeviceID, Convert.ToInt32(Session["CompanyID"]));
                if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0)
                {
                    gvEmpAttDev.DataSource = ds;
                    gvEmpAttDev.DataBind();
                }
                else
                {
                    gvEmpAttDev.DataSource = ds;
                    gvEmpAttDev.DataBind();
                }
                EmployeeAttendanceDevicePaging.Bind(objHrCommon.CurrentPage, objHrCommon.TotalPages, objHrCommon.NoofRecords, objHrCommon.PageSize);
                gvEmpAttDev.Visible = true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                ViewState["DeviceID"] = "";
                dvAddEmpUnderDev.Visible = true;
                BindAttDevice();
                BindDeviceName(0);
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
                btnSubmit.Enabled = Convert.ToBoolean(ds.Tables[0].Rows[0]["Editable"].ToString());
               
                btnSave.Enabled = Convert.ToBoolean(ds.Tables[0].Rows[0]["Editable"].ToString()); ;

            }
            return MenuId;
        }
        public void BindDeviceName(int DeviceID)
        {
           
          DataSet  ds = AttendanceDAC.GetDeviceDetails();
            ddlDevName.DataSource = ds.Tables[0];
            ddlDevName.DataTextField = "DeviceName";
            ddlDevName.DataValueField = "DeviceID";
            ddlDevName.DataBind();
            ddlDevName.Items.Insert(0, new ListItem("--Select--", "0"));
        }


        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            lblEmpAttDevHeadLine.Visible = true;
            EmployeeAttendanceDevicePaging.Visible = true;
            EmployeeAttendanceDeviceBind(objHrCommon);
        }
        protected void gvEmpAttDev_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Upd")
            {
               
                GridViewRow gvrDHID = (GridViewRow)(((Control)e.CommandSource).NamingContainer);
                Label lblDHID = (Label)gvrDHID.FindControl("lblDHID");
                int DHID = int.Parse(lblDHID.Text);
                GridViewRow gvr = (GridViewRow)(((Control)e.CommandSource).NamingContainer);
                TextBox lnkDevEmpID = (TextBox)gvr.FindControl("txtDeviceEmpID");
                int DeviceEmpID = int.Parse(lnkDevEmpID.Text);
                GridViewRow gvrHMSEmpID = (GridViewRow)(((Control)e.CommandSource).NamingContainer);
                TextBox lnkHMSEmpID = (TextBox)gvrHMSEmpID.FindControl("txtHMSEmpID");
                int HMSEmpID = int.Parse(lnkHMSEmpID.Text);
                int OutPut = AttendanceDAC.HMS_Upd_AttendanceDeviceHMSEmpID(DHID, DeviceEmpID, HMSEmpID);
                EmployeeAttendanceDeviceBind(objHrCommon);
                if (OutPut == 3)
                {
                    AlertMsg.MsgBox(Page, "Updated Done!");
                }
                else if (OutPut == 4)
                    AlertMsg.MsgBox(Page, "HMSEmpID Doesn't Exists!");
                else if (OutPut == 5)
                    AlertMsg.MsgBox(Page, "DeviceEmpID Already Exists!");
                else if (OutPut == 6)
                    AlertMsg.MsgBox(Page, "HMSEmpID Already Exists!");
                else
                    AlertMsg.MsgBox(Page, "Already Exists!..");


            }


        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            int? DHID = null;
            int? DeviceID = Convert.ToInt32(ddlDeviceName.SelectedValue);
            int DeviceEmpID = Convert.ToInt32(txtDevEmpId.Text);
            int HmsEmpID = Convert.ToInt32(txtHMSEmpID.Text);
            int OutPut = AttendanceDAC.InsDevHmsEmpID(DHID, DeviceID, DeviceEmpID, HmsEmpID);
            if (OutPut == 1)
                AlertMsg.MsgBox(Page, "Inserted Sucessfully!..");
            else if (OutPut == 2)
                AlertMsg.MsgBox(Page, "DeviceEmployeeID/HMS EmployeeID Already Exists!..");
            else
                AlertMsg.MsgBox(Page, "HMSEmpID Doesn't Exists!..");
            ddlDevName.DataSource = null;
            txtDevEmpId.Text = "";
            txtHMSEmpID.Text = "";

        }
        public void BindAttDevice()
        {
            DataSet dsAttDev = new DataSet();
            dsAttDev = AttendanceDAC.GetDeviceDetails();
            ddlDeviceName.DataSource = dsAttDev;
            ddlDeviceName.DataTextField = "DeviceName";
            ddlDeviceName.DataValueField = "DeviceID";
            ddlDeviceName.DataBind();
            ddlDeviceName.Items.Insert(0, new ListItem("...Select...", "0"));

        }
        protected void gvEmpAttDev_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton lnk = (LinkButton)e.Row.FindControl("lnkUpdate");
                lnk.Enabled = Editable;
            }

        }
    }

}