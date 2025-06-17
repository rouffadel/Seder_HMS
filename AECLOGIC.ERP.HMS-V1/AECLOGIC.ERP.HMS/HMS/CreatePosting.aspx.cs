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
    public partial class CreatePosting : AECLOGIC.ERP.COMMON.WebFormMaster
    {
       
        AttendanceDAC objPosting = new AttendanceDAC();
        HRCommon objHrCommon = new HRCommon();
        protected override void OnInit(EventArgs e)
        {
            ModuleID = 1;
            base.OnInit(e);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
           
            if (txtposts.Text == "")
                txtposts.Text = "1";

            if (!IsPostBack)
            {
                GetParentMenuId();
                lnkAdd.Attributes.Add("onclick", "javascript:return AddNewItem();");

                txtFromDate.Text = DateTime.Today.AddDays(1).ToString(ConfigurationManager.AppSettings["DateFormat"]);
                txtToDate.Text = DateTime.Today.AddDays(10).ToString(ConfigurationManager.AppSettings["DateFormat"]);

                rbClosed.Checked = true;
                BindCategories();
                BindDesignations();
                BindWorkSites();
                BindDepartments();
                BindInterViewType();

                if (Request.QueryString["id"] != null && Request.QueryString["id"] != string.Empty)
                {
                    int PosID = Convert.ToInt32(Request.QueryString["id"]);
                    BindJobDetails(PosID);
                }
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
                btnSubmit.Enabled = Button1.Enabled = lnkAdd.Enabled = (bool)ds.Tables[0].Rows[0]["Editable"];
            }
            return MenuId;
        }
        public void BindDepartments()
        {
            try
            {
                 
                DataSet ds = (DataSet)objPosting.GetDaprtmentList();
                if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ddldepartment.DataValueField = "DepartmentUId";
                    ddldepartment.DataTextField = "DeptName";
                    ddldepartment.DataSource = ds;
                    ddldepartment.DataBind();
                    ddldepartment.Items.Insert(0, new ListItem("--Select--", "0"));

                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void BindWorkSites()
        {

            try
            {
                 
                DataSet ds = objPosting.GetWorkSiteByEmpID( Convert.ToInt32(Session["UserId"]), Convert.ToInt32(Session["CompanyID"]), Convert.ToInt32(Session["RoleId"]));
                if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0)
                {

                    ddlWS.DataSource = ds.Tables[0];
                    ddlWS.DataTextField = "Site_Name";
                    ddlWS.DataValueField = "Site_ID";
                    ddlWS.DataBind();
                }
                ddlWS.Items.Insert(0, new ListItem("---Select---", "0"));

            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public void BindInterViewType()
        {
            try
            {
                 
                DataSet ds = (DataSet)objPosting.GetInterViewType();
                if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ddlinterviewtype.DataValueField = "InterviewTypeId";
                    ddlinterviewtype.DataTextField = "InterviewType";
                    ddlinterviewtype.DataSource = ds;
                    ddlinterviewtype.DataBind();
                    ddlinterviewtype.Items.Insert(0, new ListItem("--Select--", "0"));
                }

            }
            catch (Exception e)
            {
                throw e;
            }


        }
        private void BindDesignations()
        {
            DataSet ds = objPosting.GetDesignations();
            ddlDesig.DataSource = ds;
            ddlDesig.DataTextField = "Designation";
            ddlDesig.DataValueField = "DesigId";
            ddlDesig.DataBind();
            ddlDesig.Items.Insert(0, new ListItem("---Select---", "0"));
        }
        public void BindJobDetails(int PosID)
        {
            try
            {
                 
                DataSet ds = (DataSet)objPosting.GetJobDetails(PosID);
                if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0)
                {
                    txtPosition.Text = ds.Tables[0].Rows[0]["Position"].ToString();
                    ddldepartment.SelectedValue = ds.Tables[0].Rows[0]["DeptID"].ToString();
                    txtdescription.Text = ds.Tables[0].Rows[0]["Description"].ToString();
                    txtposts.Text = ds.Tables[0].Rows[0]["Posts"].ToString();
                    txtFromDate.Text = ds.Tables[0].Rows[0]["FromDate"].ToString();
                    txtToDate.Text = ds.Tables[0].Rows[0]["ToDate"].ToString();
                    txttimings.Text = ds.Tables[0].Rows[0]["Timings"].ToString();
                    txtQualifications.Text = ds.Tables[0].Rows[0]["Qualifications"].ToString();
                    ddlinterviewtype.SelectedValue = ds.Tables[0].Rows[0]["InterviewTypeID"].ToString();

                    if (ds.Tables[0].Rows[0]["Status"].ToString() == "True")
                    {
                        // rbOpened.Checked = true;
                        rbClosed.Checked = true;
                    }
                    else
                    {
                        rbClosed.Checked = false;

                    }

                }

            }
            catch (Exception e)
            {
                throw e;
            }



        }
        private void BindCategories()
        {
            DataSet ds = objPosting.GetCategories();
            DataTable dt = ds.Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                ddlCategory.Items.Add(new ListItem(dr["Category"].ToString(), dr["CateId"].ToString()));
            }
            ddlCategory.Items.Insert(0, new ListItem("---Select---", "0"));
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                int PosID = 0;
                if (Request.QueryString["id"] != null && Request.QueryString["id"] != string.Empty)
                {
                    objHrCommon.PosID = Convert.ToInt32(Request.QueryString["id"]);
                }
                else
                {
                    objHrCommon.PosID = PosID;
                }
                objHrCommon.Position = txtPosition.Text;

                objHrCommon.DeptID = Convert.ToDouble(ddldepartment.SelectedValue);
                objHrCommon.Description = txtdescription.Text;
                objHrCommon.Posts = Convert.ToInt32(txtposts.Text);
                objHrCommon.FromDate = CODEUtility.ConvertToDate(txtFromDate.Text.Trim(), DateFormat.DayMonthYear);
                objHrCommon.ToDate = CODEUtility.ConvertToDate(txtToDate.Text.Trim(), DateFormat.DayMonthYear);
                objHrCommon.Timings = txttimings.Text;
                objHrCommon.Qualification = txtQualifications.Text;
                objHrCommon.InterviewTypeID = Convert.ToInt32(ddlinterviewtype.SelectedValue);
                int? WSID = null;
                if (ddlWS.SelectedValue != "0")
                    WSID = Convert.ToInt32(ddlWS.SelectedValue);
                double FrmExp = 0;
                double? ToExp = null;
                try
                {
                    if (txtFrm.Text != "")
                        FrmExp = Convert.ToDouble(txtFrm.Text);
                    if (txtTo.Text != "")
                        ToExp = Convert.ToDouble(txtTo.Text);
                }
                catch (Exception)
                {
                    AlertMsg.MsgBox(Page, "Invalid experience.!");
                }

                if (rbClosed.Checked == true)
                {

                    objHrCommon.Status = true;
                }

                else
                {
                    objHrCommon.Status = false;
                }
                int DesigID = Convert.ToInt32(ddlDesig.SelectedValue);
                int Trade = Convert.ToInt32(ddlCategory.SelectedValue);
                int ret = Convert.ToInt32(objPosting.CreatePosting(objHrCommon, WSID, FrmExp, ToExp, DesigID, Trade));

                if (ret == 1)
                {
                    AlertMsg.MsgBox(Page, "Created!");
                    Response.Redirect("ViewPostingList.aspx");
                }
                else
                {
                    AlertMsg.MsgBox(Page, "Updated!");
                    Response.Redirect("ViewPostingList.aspx");
                }

            }
            catch (Exception ex)
            {
                AlertMsg.MsgBox(Page, "Sorry for the inconvinience. Try again.\nError:" + ex.Message.ToString(),AlertMsg.MessageType.Error);
            }
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {

            ddldepartment.SelectedIndex = 0;
            txtdescription.Text = "";
            txtposts.Text = "";
            txtFromDate.Text = "";
            txtToDate.Text = "";
            txttimings.Text = "";
            txtQualifications.Text = "";
            ddlinterviewtype.SelectedIndex = 0;
            // added here by pratap date:15-04-2016 bug no:176
            txtPosition.Text="";
            ddlDesig.SelectedIndex = 0;
            ddlCategory.SelectedIndex = 0;
            ddlWS.SelectedIndex = 0;            
            txtFrm.Text="";
            txtTo.Text = "";
        }
        protected void Button1_Click(object sender, EventArgs e)
        {

        }

        protected void Button1_Click1(object sender, EventArgs e)
        {
            Response.Redirect("CreatePosting.aspx");
        }

        protected void lnkAdd_Click(object sender, EventArgs e)
        {
            BindInterViewType();
        }

    }
}