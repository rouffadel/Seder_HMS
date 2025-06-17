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
    public partial class NMRAttendance : AECLOGIC.ERP.COMMON.WebFormMaster
    {
        int mid = 0;
        bool viewall;
        string menuname;
        string menuid;
         protected override void OnInit(EventArgs e)
        {
            ModuleID = 1;
            base.OnInit(e);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            lblStatus.Text = String.Empty;
            AttendanceDAC objAtt = new AttendanceDAC();
            Ajax.Utility.RegisterTypeForAjax(typeof(AjaxDAL));
            if ( Convert.ToInt32(Session["UserId"]) == null)
            {
                Response.Redirect("Home.aspx");
            }
            else
            {
                lblDate.Text = "Date: " + DateTime.Now.ToString(ConfigurationManager.AppSettings["DateDisplayFormat"]);
                hdn.Value = "0";
              
                if (!IsPostBack)
                {
                    GetParentMenuId();
                    try
                    {
                     

                      DataSet  dstemp = BindGrid(objAtt);
                        try
                        {
                            if (Convert.ToInt32(Session["MonitorSite"]) != 0)
                            {
                                ddlWorksite.Items.FindByValue(Session["MonitorSite"].ToString()).Selected = true;
                                ddlWorksite.Enabled = false;
                            }
                            else
                            {
                                if (Session["Type"].ToString() != "1" && Session["Site"].ToString() != "1")
                                {
                                    ddlWorksite.Items.FindByValue(Session["Site"].ToString()).Selected = true;
                                    ddlWorksite.Enabled = false;

                                }
                                else
                                {
                                    if (ddlWorksite.Items.Count > 0)
                                    {
                                        ddlWorksite.Items[1].Selected = true;
                                        ddlWorksite.Enabled = true;
                                    }
                                }
                            }
                        }
                        catch
                        {

                        }
                        //  ddlDepartment.Items[0].Selected = true;

                    }
                    catch
                    {
                        //AlertMsg.MsgBox(Page, "Try Again!");
                        lblStatus.Text = "Try Again!";
                        lblStatus.ForeColor = System.Drawing.Color.Red;
                    }

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
                ViewState["Editable"] = (bool)ds.Tables[0].Rows[0]["Editable"];
                gdvAttend.Columns[6].Visible = Convert.ToBoolean(ds.Tables[0].Rows[0]["Editable"].ToString());
               
            }
            return MenuId;
        }
        private DataSet BindGrid(AttendanceDAC objAtt)
        {
            DataSet dstemp;
            if (Convert.ToInt32(Session["MonitorSite"]) != 0)
            {
                dstemp = objAtt.GetTodayNMRAttendance(0, Convert.ToInt32(Session["MonitorSite"]));
            }
            else
            {

                dstemp = objAtt.GetTodayNMRAttendance(0, 1);
            }


            gdvAttend.DataSource = dstemp.Tables[0];
            gdvAttend.DataBind();

            ddlDepartment.DataSource = dstemp.Tables[2];
            ddlDepartment.DataTextField = "DepartmentName";
            ddlDepartment.DataValueField = "DepartmentUId";
            ddlDepartment.DataBind();
            ddlDepartment.Items.Insert(0, new ListItem("---ALL---", "0", true));

            ddlWorksite.DataSource = dstemp.Tables[1];
            ddlWorksite.DataTextField = "Site_Name";
            ddlWorksite.DataValueField = "Site_ID";
            ddlWorksite.DataBind();
            ddlWorksite.Items.Insert(0, new ListItem("---ALL---", "0", true));
           
            return dstemp;


        }
        public DataSet BindAttendanceType()
        {
             
           DataSet ds = AttendanceDAC.GetAttendanceType();
            return ds;
        }
        private DataSet GetDepartments(AttendanceDAC objAtt, DataSet dstemp)
        {
            dstemp = objAtt.GetDepartments(0);
            ddlDepartment.DataSource = dstemp.Tables[0];
            ddlDepartment.DataTextField = "DeptName";
            //ddlDepartment.DataTextField = "DepartmentName";
            ddlDepartment.DataValueField = "DepartmentUId";
            ddlDepartment.DataBind();
            ddlDepartment.Items.Insert(0, new ListItem("---ALL---", "0", true));
            return dstemp;
        }

        private DataSet GetWorkSites(AttendanceDAC objAtt, DataSet dstemp)
        {
            dstemp = AttendanceDAC.GetWorkSite(0, '1', Convert.ToInt32(Session["CompanyID"]));
            ddlWorksite.DataSource = dstemp.Tables[0];
            ddlWorksite.DataTextField = "Site_Name";
            ddlWorksite.DataValueField = "Site_ID";
            ddlWorksite.DataBind();
            ddlWorksite.Items.Insert(0, new ListItem("---ALL---", "0", true));
            return dstemp;
            ddlWorksite.SelectedItem.Value = "1";
        }
        protected void ddl_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                AttendanceDAC objAtt = new AttendanceDAC();
                DataSet dstemp = new DataSet();
                dstemp = objAtt.GetTodayNMRAttendance(Convert.ToInt32(ddlDepartment.SelectedValue), Convert.ToInt32(ddlWorksite.SelectedValue));
                gdvAttend.DataSource = dstemp.Tables[0];
                gdvAttend.DataBind();

                ddlDepartment.DataSource = dstemp.Tables[2];
                ddlDepartment.DataTextField = "DepartmentName";
                ddlDepartment.DataValueField = "DepartmentUId";
                ddlDepartment.DataBind();
                ddlDepartment.Items.Insert(0, new ListItem("---ALL---", "0", true));
            }
            catch
            {
               // AlertMsg.MsgBox(Page, "Try Again!");
                lblStatus.Text = "Try Again!";
                lblStatus.ForeColor = System.Drawing.Color.Red;
            }

        }
        protected void gdvAttend_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DropDownList ddlStatus = (DropDownList)e.Row.FindControl("ddlStatus");
                Button btnHdn = (Button)e.Row.FindControl("btnHid");
                CheckBox chkOut = (CheckBox)e.Row.FindControl("chkOut");
                TextBox txtOut = (TextBox)e.Row.FindControl("txtOUT");
                TextBox txt = (TextBox)e.Row.FindControl("txtIN");
                TextBox txtRemarks = (TextBox)e.Row.FindControl("txtRemarks");
                LinkButton lnkUpd = (LinkButton)e.Row.FindControl("btnUpdate");
                lnkUpd.Enabled = Convert.ToBoolean(ViewState["Editable"]);
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
                ddlStatus.Attributes.Add("onchange", "javascript:return CheckLeaveCombination(this,'" + EmpId + "','" + txt.ClientID + "','" + ddlWorksite.ClientID + "','" +  Convert.ToInt32(Session["UserId"]).ToString() + "','" + e.Row.ClientID + "','" + txt.ClientID + "');");
                chkOut.Attributes.Add("onclick", "javascript:return GetOutTime('" + chkOut.ClientID + "','" + DateTime.Now.ToShortTimeString() + "','" + txtOut.ClientID + "','" + txt.ClientID + "','" + EmpId + "')");
                ddlStatus.Attributes.Add("onchange", "javascript:return GetInTime('" + ddlStatus.ClientID + "','" + txt.ClientID + "');");
                lnkUpd.Attributes.Add("onclick", "javascript:return MarkNMRAtt('" + ddlStatus.ClientID + "','" + EmpId + "','" + txt.ClientID + "','" + txtOut.ClientID + "','" + txtRemarks.ClientID + "')");

            }
        }
    }
}
