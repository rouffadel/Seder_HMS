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



    public partial class EditPostingDetails :System.Web.UI.Page
 
    {
        AttendanceDAC objPosting = new AttendanceDAC();
        HRCommon objHrCommon = new HRCommon();
        int ModuleID = 1;
       
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                if (rbClosed.Checked == false)
                {
                    btnInActive.Visible = true;
                    btnSubmit.Visible = false;
                }
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

            if (rbClosed.Checked == false)
            {
                btnInActive.Visible = true;
                btnSubmit.Visible = false;
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
                    ddldepartment.Items.Insert(0, "Select");
                }
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
                    ddlinterviewtype.Items.Insert(0, "Select");
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
                    ddlWS.Items.Insert(0, new ListItem("---Select---", "0"));
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
        public void BindJobDetails(int PosID)
        {
            try
            {

                DataSet ds = (DataSet)objPosting.GetJobDetails(PosID);
                if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ddlCategory.SelectedValue = ds.Tables[0].Rows[0]["Trade"].ToString();
                    ddlDesig.SelectedValue = ds.Tables[0].Rows[0]["DesigID"].ToString();
                    txtPosition.Text = ds.Tables[0].Rows[0]["Position"].ToString();
                    ddldepartment.SelectedValue = ds.Tables[0].Rows[0]["DeptID"].ToString();
                    txtdescription.Text = ds.Tables[0].Rows[0]["Description"].ToString();
                    ddlWS.SelectedValue = Convert.ToInt32(ds.Tables[0].Rows[0]["WsID"]).ToString();
                    txtFrm.Text = Convert.ToDouble(ds.Tables[0].Rows[0]["ExpFrom"]).ToString();
                    txtTo.Text = "";
                    if (ds.Tables[0].Rows[0]["ExpTo"].ToString() != "")
                        txtTo.Text = Convert.ToDouble(ds.Tables[0].Rows[0]["ExpTo"]).ToString();
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
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (rbClosed.Checked == false)
            {
                btnInActive_Click(this, e);
            }
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
                double ExpFrm = 0, ExpTo = 0;
                try
                {
                    ExpFrm = Convert.ToDouble(txtFrm.Text);
                    ExpTo = Convert.ToDouble(txtTo.Text);
                }
                catch (Exception)
                {
                    AlertMsg.MsgBox(Page, "Invalid experience.!");
                }
                if (rbClosed.Checked == false)
                {
                    objHrCommon.Status = false;

                }

                else
                {
                    objHrCommon.Status = true;
                }
                int DesigID = Convert.ToInt32(ddlDesig.SelectedValue);
                int TradeID = Convert.ToInt32(ddlCategory.SelectedValue);
                int ret = Convert.ToInt32(objPosting.CreatePosting(objHrCommon, WSID, ExpFrm, ExpTo, DesigID, TradeID));
                if (ret == 1)
                {
                    AlertMsg.MsgBox(Page, "Created!");
                    Response.Write("<script language='javascript'> { window.close();}</script>");
                    btnback.Attributes.Add("onclick", "window.close();");

                }
                else
                {
                    AlertMsg.MsgBox(Page, "Updated");
                    Response.Write("<script language='javascript'> { window.close();}</script>");
                    btnback.Attributes.Add("onclick", "window.close();");

                }


            }
            catch (Exception ex)
            {
                AlertMsg.MsgBox(Page, "Sorry for the inconvinience. Try again.\nError:" + ex.Message.ToString(),AlertMsg.MessageType.Error);
            }
        }

        protected void btnback_Click(object sender, EventArgs e)
        {
            Response.Write("<script language='javascript'> { window.close();}</script>");
            btnback.Attributes.Add("onclick", "window.close();");



        }
        protected void Back_Click(object sender, EventArgs e)
        {
            Response.Write("<script language='javascript'> { window.close();}</script>");
            Back.Attributes.Add("onclick", "window.close();");
        }
        protected void btnInActive_Click(object sender, EventArgs e)
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
                double ExpFrm = 0, ExpTo = 0;
                try
                {
                    ExpFrm = Convert.ToDouble(txtFrm.Text);
                    ExpTo = Convert.ToDouble(txtTo.Text);
                }
                catch (Exception)
                {

                    throw;
                }

                if (rbClosed.Checked == false)
                {
                    objHrCommon.Status = false;

                }

                else
                {
                    objHrCommon.Status = true;
                }
                int DesigID = Convert.ToInt32(ddlDesig.SelectedValue);
                int TradeID = Convert.ToInt32(ddlCategory.SelectedValue);

                int ret = Convert.ToInt32(objPosting.CreatePosting(objHrCommon, WSID, ExpFrm, ExpTo, DesigID, TradeID));
                if (ret == 1)
                {
                    AlertMsg.MsgBox(Page, "Created");
                    Response.Write("<script language='javascript'> { window.close();}</script>");
                    btnback.Attributes.Add("onclick", "window.close();");

                }
                else
                {
                    AlertMsg.MsgBox(Page, "Updated");
                    Response.Write("<script language='javascript'> { window.close();}</script>");
                    btnback.Attributes.Add("onclick", "window.close();");
                }
                //for refresh the below single page code by chaitanya
                Response.Redirect("ViewPostingList.aspx");

            }
            catch (Exception ex)
            {
                AlertMsg.MsgBox(Page, "Sorry for the inconvinience. Try again.\nError:" + ex.Message.ToString(),AlertMsg.MessageType.Error);
            }
        }
    }
