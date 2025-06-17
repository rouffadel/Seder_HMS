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
using System.Collections.Generic;
namespace AECLOGIC.ERP.HMS
{
    public partial class AttendanceDevice : AECLOGIC.ERP.COMMON.WebFormMaster
    {
        bool viewall, Editable;
        static bool sttus;
        string ModuleId = System.Configuration.ConfigurationManager.AppSettings["ModuleId"].ToString();
        AttendanceDAC objAtt = new AttendanceDAC();
        HRCommon objHrCommon = new HRCommon();

        protected override void OnInit(EventArgs e)
        {
            ModuleID = 1;
            base.OnInit(e);
            AttendanceDevicePaging.FirstClick += new Paging.PageFirst(AttendanceDevicePaging_FirstClick);
            AttendanceDevicePaging.PreviousClick += new Paging.PagePrevious(AttendanceDevicePaging_FirstClick);
            AttendanceDevicePaging.NextClick += new Paging.PageNext(AttendanceDevicePaging_FirstClick);
            AttendanceDevicePaging.LastClick += new Paging.PageLast(AttendanceDevicePaging_FirstClick);
            AttendanceDevicePaging.ChangeClick += new Paging.PageChange(AttendanceDevicePaging_FirstClick);
            AttendanceDevicePaging.ShowRowsClick += new Paging.ShowRowsChange(AttendanceDevicePaging_ShowRowsClick);
            AttendanceDevicePaging.CurrentPage = 1;
        }
        void AttendanceDevicePaging_ShowRowsClick(object sender, EventArgs e)
        {
            AttendanceDevicePaging.CurrentPage = 1;
            BindPager();
        }
        void AttendanceDevicePaging_FirstClick(object sender, EventArgs e)
        {
            BindPager();
        }
        void BindPager()
        {

            objHrCommon.PageSize = AttendanceDevicePaging.CurrentPage;
            objHrCommon.CurrentPage = AttendanceDevicePaging.ShowRows;
            AttendanceDeviceBind(objHrCommon);
        }
        public void AttendanceDeviceBind(HRCommon objHrCommon)
        {
            try
            {
                objHrCommon.PageSize = AttendanceDevicePaging.ShowRows;
                objHrCommon.CurrentPage = AttendanceDevicePaging.CurrentPage;
                bool Status = false;
                if (rblAttendanceDev.SelectedValue == "1")
                {
                    Status = true;
                }
                else
                {
                    Status = false;
                }
                int BIOID = 0; if (txtsearchbiomettric.Text.Trim() != "")
                { BIOID = Convert.ToInt32(ddlbiometric_hid.Value == "" ? "0" : ddlbiometric_hid.Value); }
                DataSet ds = AttendanceDAC.HR_GetAttendanceDeviceByPaging(objHrCommon, Status, BIOID);
                if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0)
                {
                    gvAttenDev.DataSource = ds;
                    gvAttenDev.DataBind();
                }
                else
                {
                    gvAttenDev.DataSource = ds;
                    gvAttenDev.DataBind();
                }
                AttendanceDevicePaging.Bind(objHrCommon.CurrentPage, objHrCommon.TotalPages, objHrCommon.NoofRecords, objHrCommon.PageSize);
                gvAttenDev.Visible = true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (rblAttendanceDev.SelectedValue == "1")
            {
                sttus = true;
            }
            else
                sttus = false;
            if (!IsPostBack)
            {
                ViewState["DeviceID"] = null;
                dvadd.Visible = false;
                dvView.Visible = true;
                BindPager();
            }
        }
        public int GetParentMenuId()
        {
           
            string URL = Request.Url.Segments[Request.Url.Segments.Length - 1] + Request.Url.Query;
            int RoleId = Convert.ToInt32(Session["RoleId"].ToString());
            int ModuleId = ModuleID; ;

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
        protected void btnSubmit_Click(object sender, EventArgs e)
        {

            int status;
            try
            {
                int DeviceID;
                if (ViewState["DeviceID"] != null)
                    DeviceID = Convert.ToInt32(Session["DeviceID"]);

                if (chkStatus.Checked == true)
                    status = 1;
                else
                    status = 0;
                objAtt = new AttendanceDAC();
                int n = objAtt.HR_InsUpAttendanceDevice(Convert.ToInt32(ViewState["DeviceID"]), txtDevName.Text, txtLoc.Text, status,  Convert.ToInt32(Session["UserId"]));
                if (n == 1)
                {
                    AlertMsg.MsgBox(Page, "AttendanceDevice Added Sucessfully..!");
                    txtDevName.Text = string.Empty;
                    txtLoc.Text = string.Empty;
                    dvadd.Visible = false;
                    dvView.Visible = true;

                }
                else if (n == 2)
                    AlertMsg.MsgBox(Page, "Already Exists..!");
                else
                {
                    AlertMsg.MsgBox(Page, "Updated Sucessfully..!");
                    dvadd.Visible = false;
                    dvView.Visible = true;
                }
                BindPager();

            }
            catch (Exception AttDev)
            {
                AlertMsg.MsgBox(Page, AttDev.Message.ToString(),AlertMsg.MessageType.Error);
            }
        }

        protected void gvWages_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int DeviceID = Convert.ToInt32(e.CommandArgument);
                if (e.CommandName == "Edt")
                {
                    BindAttendanceDivice(DeviceID);
                }
                else
                    if (e.CommandName == "Sta")
                    {
                        bool Status;
                        GridViewRow gvr = (GridViewRow)(((Control)e.CommandSource).NamingContainer);
                        LinkButton lnkStatus = (LinkButton)gvr.FindControl("lnkStatus");
                        if (lnkStatus.Text == "In-Active")
                        {
                            Status = false;
                        }
                        else
                        {
                            Status = true;
                        }
                        AttendanceDAC.HMS_Upd_AttendanceDeviceStatus(DeviceID, Status);
                        AttendanceDeviceBind(objHrCommon);
                        AlertMsg.MsgBox(Page, "Done..!");
                    }
            }
            catch (Exception AttEdt)
            {
                AlertMsg.MsgBox(Page, AttEdt.Message.ToString(),AlertMsg.MessageType.Error);
            }

        }
        public void BindAttendanceDivice(int DeviceID)
        {
            DataSet dsAttDev = AttendanceDAC.GetAttendanceDeviceDetails(DeviceID);
            if (dsAttDev != null && dsAttDev.Tables.Count != 0 && dsAttDev.Tables[0].Rows.Count != 0)
            {
                txtDevName.Text = dsAttDev.Tables[0].Rows[0]["DeviceName"].ToString();
                txtLoc.Text = dsAttDev.Tables[0].Rows[0]["Location"].ToString();
                ViewState["DeviceID"] = dsAttDev.Tables[0].Rows[0]["DeviceID"].ToString();
                dvadd.Visible = true;
                dvView.Visible = false;
                btnSubmit.Text = "Update";
            }

           // AttendanceDeviceBind(objHrCommon);

        }
        protected void rblDesg_SelectedIndexChanged(object sender, EventArgs e)
        {
            AttendanceDevicePaging.CurrentPage = 1;
            AttendanceDeviceBind(objHrCommon);
        }



        protected void lnkAdd_Click1(object sender, EventArgs e)
        {
            dvadd.Visible = true;
            dvView.Visible = false;
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletiondeptList(string prefixText, int count, string contextKey)
        {
            DataSet ds = AttendanceDAC.SH_Biometric(prefixText,sttus);
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
        protected void lnkView_Click1(object sender, EventArgs e)
        {
            dvadd.Visible = false;
            dvView.Visible = true;
        }

        public int DeviceID { get; set; }
    }
}

    