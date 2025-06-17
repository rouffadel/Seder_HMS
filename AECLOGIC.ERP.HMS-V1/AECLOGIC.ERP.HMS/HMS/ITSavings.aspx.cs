using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using AECLOGIC.HMS.BLL;
using Aeclogic.Common.DAL;
using System.Data.SqlClient;

namespace AECLOGIC.ERP.HMS
{
    public partial class ITSavings : AECLOGIC.ERP.COMMON.WebFormMaster
    {
         
        static string strurl = string.Empty;

        int mid = 0;
        bool viewall;
        string menuname;
        string menuid;
        bool Editable;
        protected override void OnInit(EventArgs e)
        {
            ModuleID = 1;
            base.OnInit(e);
        }
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                Page.Form.Attributes.Add("enctype", "multipart/form-data");
                GetParentMenuId();
                if (Request.UrlReferrer != null)
                    strurl = Request.UrlReferrer.ToString();
                ViewState["DedID"] = "";
                ViewState["Proof"] = "";
                tblEdit.Visible = true;
                lnkBtnEdit.CssClass = "lnkselected";
                lnkAdd.CssClass = "linksunselected";
                
                BindGrid();
               
                BindAssessYear();
                if (Request.QueryString.Count > 0)
                {
                    ViewState["EmpID"] = Convert.ToInt32(Request.QueryString["EmpID"]);
                }
                else
                {
                    ViewState["EmpID"] =  Convert.ToInt32(Session["UserId"]);
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
                Editable = (bool)ds.Tables[0].Rows[0]["Editable"];
                gvLeaveProfile.Columns[4].Visible = Convert.ToBoolean(ds.Tables[0].Rows[0]["Editable"].ToString());
                btnSubmit.Enabled = Editable;
            }
            return MenuId;
        }
        public void BindSections()
        {
            DataSet ds = SQLDBUtil.ExecuteDataset("[HR_Get_IT_SectionsList_New]", new SqlParameter[] { new SqlParameter("@FinYearID", ddlAssessmentYear.SelectedValue) });
                //PayRollMgr.GetIT_SectionsList(Convert.ToInt32(ddlAssessmentYear.SelectedValue));
            if (ds != null)
            {
                ddlSections.DataSource = ds;
                ddlSections.DataValueField = "SectionID";
                ddlSections.DataTextField = "SectionName";
                ddlSections.DataBind();
                ddlSections.Items.Insert(0, new ListItem("--Select--", "0"));
            }
            else
            {
                ddlSections.DataSource = null;
                ddlSections.DataBind();
                
            }
        }
        public void BindAssessYear()
        {
            DataSet ds = PayRollMgr.GetFinacialYearList();
            ddlAssessmentYear.DataSource = ds;
            ddlAssessmentYear.DataValueField = "FinYearId";
            ddlAssessmentYear.DataTextField = "Name";
            ddlAssessmentYear.DataBind();
            ddlAssessmentYear.Items.Insert(0, new ListItem("--Select--", "0"));

            ddlFinYear.DataSource = ds;
            ddlFinYear.DataValueField = "FinYearId";
            ddlFinYear.DataTextField = "Name";
            ddlFinYear.DataBind();
        }
        public void BindGrid()
        {
            tblEdit.Visible = true;
            tblNew.Visible = false;


            int EmpID = Convert.ToInt32( Convert.ToInt32(Session["UserId"]).ToString());
            DataSet ds = PayRollMgr.GetIT_SavingsList(EmpID, Convert.ToInt32(Session["FinYearID"]));
            gvLeaveProfile.DataSource = ds;
            gvLeaveProfile.DataBind();
        }
        public void BindDetails(int ID)
        {
            tblEdit.Visible = false;
            tblNew.Visible = true;
            DataSet ds = PayRollMgr.GetIT_SavingsDetails(ID);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                txtAmount.Text = ds.Tables[0].Rows[0]["Amount"].ToString();
                txtItemName.Text = ds.Tables[0].Rows[0]["ItemName"].ToString();
                try { ddlSections.SelectedValue = ds.Tables[0].Rows[0]["SectionID"].ToString(); }
                catch { }
                try { ddlAssessmentYear.SelectedValue = ds.Tables[0].Rows[0]["AssYearId"].ToString(); }
                catch { }
                ViewState["Proof"] = ds.Tables[0].Rows[0]["Proof"].ToString();
            }
        }
        protected void gvLeaveProfile_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int ID = Convert.ToInt32(e.CommandArgument);
            ViewState["DedID"] = ID;
            if (e.CommandName == "Edt")
            {
                BindDetails(ID);
            }
            else
            {

            }
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                int DedID = 0;

                string filename = "", ext = "", path = "";
                if (FUDProof.HasFile)
                {
                    filename = FUDProof.PostedFile.FileName;
                }
                if (filename != "")
                {
                    ext = filename.Split('.')[filename.Split('.').Length - 1];
                }
                else
                {
                    if (ViewState["Proof"].ToString() != "")
                    {
                        ext = ViewState["Proof"].ToString();
                    }
                    else
                    {
                        ext = "";
                    }
                }


                int EmpID = Convert.ToInt32( Convert.ToInt32(Session["UserId"]).ToString());

                if (ViewState["DedID"].ToString() != null && ViewState["DedID"].ToString() != string.Empty)
                {
                    DedID = Convert.ToInt32(ViewState["DedID"].ToString());
                }

                DedID = Convert.ToInt32(PayRollMgr.InsUpdateIT_Savings(DedID, EmpID, Convert.ToInt32(ddlSections.SelectedValue), txtItemName.Text, Convert.ToDouble(txtAmount.Text), Convert.ToInt32(ddlAssessmentYear.SelectedValue), ext, EmpID));


                if (filename != "")
                {
                    if (DedID != 0)
                    {
                        path = Server.MapPath(".\\DedProofs\\" + DedID + "." + ext);
                        try
                        {
                            FUDProof.PostedFile.SaveAs(path);
                        }
                        catch { throw new Exception("FUDProof.PostedFile.SaveAs(path)"); }

                    }
                }
                AlertMsg.MsgBox(Page, "Done.! ");

                BindGrid();
                Clear();
            }
            catch (Exception ITSavings)
            {
                AlertMsg.MsgBox(Page, ITSavings.Message.ToString(),AlertMsg.MessageType.Error);
            }
        }
        public void Clear()
        {
            txtItemName.Text = "";
            txtAmount.Text = "";
            ddlSections.SelectedIndex = 0;
            ViewState["DedID"] = "";
            ViewState["Proof"] = "";
        }
        protected void lnkAdd_Click(object sender, EventArgs e)
        {
            tblNew.Visible = true;
            tblEdit.Visible = false;
            lnkAdd.CssClass = "lnkselected";
            lnkBtnEdit.CssClass = "linksunselected";
        }
        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            tblEdit.Visible = true;
            tblNew.Visible = false;
            lnkBtnEdit.CssClass = "lnkselected";
            lnkAdd.CssClass = "linksunselected";
        }
        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect(strurl);
        }
        protected void ddlFinYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            int EmpID = Convert.ToInt32(ViewState["EmpID"]);
            DataSet ds = PayRollMgr.GetIT_SavingsList(EmpID, Convert.ToInt32(ddlFinYear.SelectedValue));
            gvLeaveProfile.DataSource = ds;
            gvLeaveProfile.DataBind();
        }
       

        public string DocNavigateUrl(string Id, string Ex)
        {
            string ReturnVal = "";
            ReturnVal = "./DedProofs/" + Id + "." + Ex;
            return ReturnVal;
        }

        protected void ddlAssessmentYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindSections();
        }
    }
}
