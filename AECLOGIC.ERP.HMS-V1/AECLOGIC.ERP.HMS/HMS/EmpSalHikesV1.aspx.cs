using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using AECLOGIC.HMS.BLL;
using System.Data.SqlClient;
using AECLOGIC.ERP.COMMON;

namespace AECLOGIC.ERP.HMS
{
    public partial class EmpSalHikesV1 : AECLOGIC.ERP.COMMON.WebFormMaster
    {

        AttendanceDAC objAtt = new AttendanceDAC();
        AttendanceDAC objEmployee = new AttendanceDAC();
        AttendanceDAC objRights = new AttendanceDAC();
        AjaxDAL Aj = new AjaxDAL();
        DataSet ds = new DataSet();
        HRCommon objHrCommon = new HRCommon();
        static string strurl = string.Empty; static int SearchCompanyID; static int? Roleid; static int Siteid;
        int EmpId = 0;
        int EmpSalID = 0;
        string empsaltext = "0";
        int QuerStrngEmpid = 0;
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
        void EmpListPaging_ShowRowsClick(object sender, EventArgs e)
        {
            EmpListPaging.CurrentPage = 1;
            BindPager();
        }
        void EmpListPaging_FirstClick(object sender, EventArgs e)
        {
            BindPager();
        }
        void BindPager()
        {
            objHrCommon.PageSize = EmpListPaging.CurrentPage;
            objHrCommon.CurrentPage = EmpListPaging.ShowRows;
            EmployeBind(objHrCommon);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            SearchCompanyID = Convert.ToInt32(Session["CompanyID"]);
            Roleid = Convert.ToInt32(Session["RoleId"].ToString());
            try
            {
                try
                {
                    string id = Session["UserId"].ToString();
                }
                catch
                {
                    Response.Redirect("Home.aspx");
                }
                Ajax.Utility.RegisterTypeForAjax(typeof(AjaxDAL));
                if (!IsPostBack)
                {
                    BindEmpNatures();
                    BindWorkSites();
                    BindDeparmetBySite(0);
                    BindCategories();
                    BindDesignations();
                    EmployeBind(objHrCommon);
                    Ajax.Utility.RegisterTypeForAjax(typeof(AjaxDAL));
                    try { strurl = Request.UrlReferrer.ToString(); }
                    catch { }
                    ViewState["Id"] = "";

                }
            }
            catch (Exception ex)
            {
                clsErrorLog.HMSEventLog(ex, "EMPSalHike", "Page_Load", "001");
            }

        }
        private void BindCategories()
        {
            DataSet ds = objEmployee.GetCategories();
            ddlSearCategory.DataSource = ds;
            ddlSearCategory.DataValueField = "CateId";
            ddlSearCategory.DataTextField = "Category";
            ddlSearCategory.DataBind();
            ddlSearCategory.Items.Insert(0, new ListItem("---ALL---", "0"));

        }
        private void BindDesignations()
        {
            DataSet ds = objEmployee.GetDesignations();

            ddlSearDesigantion.DataSource = ds;
            ddlSearDesigantion.DataValueField = "DesigId";
            ddlSearDesigantion.DataTextField = "Designation";
            ddlSearDesigantion.DataBind();
            ddlSearDesigantion.Items.Insert(0, new ListItem("---ALL---", "0"));
        }
        public void BindWorkSites()
        {

            try
            {
                DataSet ds = new DataSet();
                ds = objRights.GetWorkSite_By_Employees(Convert.ToInt32(Session["CompanyID"]), Convert.ToInt32(Session["RoleId"]));

                //ds = objRights.GetWorkSite(0, '1');
                ViewState["WorkSites"] = ds;
                if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0)
                {

                    ddlworksites.DataSource = ds.Tables[0];
                    ddlworksites.DataTextField = "Site_Name";
                    ddlworksites.DataValueField = "Site_ID";
                    ddlworksites.DataBind();
                    if (Convert.ToInt32(Session["MonitorSite"]) != 0)
                    {
                        ddlworksites.Items.FindByValue(Session["MonitorSite"].ToString()).Selected = true;
                        ddlworksites.Enabled = false;
                    }
                    else
                    {
                        ddlworksites.Items.Insert(0, new ListItem("---ALL---", "0"));
                    }

                }
                else
                {
                    ddlworksites.Items.Insert(0, new ListItem("---ALL---", "0"));
                }

            }
            catch (Exception ex)
            {
                clsErrorLog.HMSEventLog(ex, "EmployeeList", "BindWorksite", "003");
            }
        }
        public void BindGrid()
        {
            tblEdit.Visible = true;
            tblaccordin.Visible = true;
            tblNew.Visible = false;
            lnkEdit.CssClass = "lnkselected";
            lnkAdd.CssClass = "linksunselected";
            int EmpID = Convert.ToInt32(Cache["EmpID"]);
            ds = AttendanceDAC.GetEmpSalList(EmpID);
            if (ds.Tables[0].Rows.Count > 0)
            {
                lblName.Text =ds.Tables[0].Rows[0]["Empid"].ToString()+' '+ds.Tables[0].Rows[0]["name"].ToString();
                lblDept.Text = ds.Tables[0].Rows[0]["DepartmentName"].ToString();
                lbldesig.Text = ds.Tables[0].Rows[0]["Design"].ToString();
                lblDOJ.Text = ds.Tables[0].Rows[0]["Dateofjoin"].ToString();
            }
            else
            {
                lblName.Text = lblDept.Text = lbldesig.Text = lblDOJ.Text = "";
            }
            gvAllowances.DataSource = ds;
            gvAllowances.DataBind();
            EnableGvAllownce();
            EmpSalID = Convert.ToInt32(ds.Tables[0].Rows[ds.Tables[0].Rows.Count - 1]["EmpSalID"]);
            empsaltext = ds.Tables[0].Rows[ds.Tables[0].Rows.Count - 1]["Salary"].ToString();

        }
        public void BindDetails(int ID)
        {
            tblEdit.Visible = false;
            tblaccordin.Visible = false;
            tblNew.Visible = true;
            ds = AttendanceDAC.GetEmpSalDetails(ID);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                txtSalary.Text = ds.Tables[0].Rows[0]["Salary"].ToString();
                txtDate.Text = ds.Tables[0].Rows[0]["FromDate"].ToString();
            }
        }
        protected void gvWages_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int ID = Convert.ToInt32(e.CommandArgument);
                int EmpID = Convert.ToInt32(ViewState["EmpID"].ToString());

                ViewState["Id"] = ID;
                if (e.CommandName == "Edt")
                {
                    BindDetails(ID);
                }
                else if (e.CommandName == "brkup")
                {
                   
                    // btnApply.Visible = true;

                    DataSet ds = new DataSet();
                    foreach (GridViewRow row in gvAllowances.Rows)
                    {
                        LinkButton lnkBreakold = (LinkButton)row.FindControl("lnkBreak");
                        lnkBreakold.CssClass = "btn btn-warning";
                    }
                    GridViewRow gvr = (GridViewRow)(((Control)e.CommandSource).NamingContainer);


                    LinkButton lnkBreak = (LinkButton)gvr.FindControl("lnkBreak");
                    LinkButton lnkEdit = (LinkButton)gvr.FindControl("LinkButton1");
                    lnkBreak.CssClass = "btn btn-success";

                    Label lblAudited = (Label)gvr.FindControl("lblAudited");
                    Label lblSalary = (Label)gvr.FindControl("lblSalary");

                    if (lblAudited.Text == "False")
                    {
                        empsaltext = lblSalary.Text;
                        EmpId = EmpID; //Response.Redirect("EmpPayRoleConfig.aspx?EmpID=" + EmpID + "&ID=" + ID + "&Salary=" + lblSalary.Text);
                        EmpSalID = ID;
                        Bind();
                        


                        MyAccordion.Visible = true;
                        btnclose.Visible = true;
                    }
                    else
                    {
                        AlertMsg.MsgBox(Page, "Financial year Audited:");
                    }

                    if (gvr.RowIndex == (gvAllowances.Rows.Count - 1))
                    {

                        grdWages.Enabled = true;
                        grdAllowances.Enabled = true;
                        grdCContribution.Enabled = true;
                        grdStatutory.Enabled = true;
                        grdNonCTCComponts.Enabled = true;
                    }
                    else
                    {
                        //lnkEdt
                        foreach (GridViewRow row in grdWages.Rows)
                        {
                            LinkButton lnkEdt = (LinkButton)row.FindControl("lnkEdt");
                            lnkEdt.Visible = false;
                        }

                        foreach (GridViewRow row in grdAllowances.Rows)
                        {
                            LinkButton lnkEdt = (LinkButton)row.FindControl("lnkEdt1");
                            lnkEdt.Visible = false;
                        }

                        foreach (GridViewRow row in grdCContribution.Rows)
                        {
                            LinkButton lnkEdt = (LinkButton)row.FindControl("lnkEdt2");
                            lnkEdt.Visible = false;
                        }

                        foreach (GridViewRow row in grdStatutory.Rows)
                        {
                            LinkButton lnkEdt = (LinkButton)row.FindControl("lnkEdt3");
                            lnkEdt.Visible = false;
                        }

                        foreach (GridViewRow row in grdNonCTCComponts.Rows)
                        {
                            LinkButton lnkEdt = (LinkButton)row.FindControl("lnkEdt4");
                            lnkEdt.Visible = false;
                        }

                        grdWages.Enabled = false;
                        grdAllowances.Enabled = false;
                        grdCContribution.Enabled = false;
                        grdStatutory.Enabled = false;
                        grdNonCTCComponts.Enabled = false;
                    }


                }
            }
            catch (Exception ex)
            {
                clsErrorLog.HMSEventLog(ex, "EMPSalHike", "gvWages_RowCommand", "002");
            }
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                int EmpSalID = 0;
                if (ViewState["Id"].ToString() != null && ViewState["Id"].ToString() != string.Empty)
                {
                    EmpSalID = Convert.ToInt32(ViewState["Id"].ToString());
                }
                int returnval;

                returnval = Convert.ToInt32(AttendanceDAC.InsUpdateEmpSal(EmpSalID, Convert.ToInt32(ViewState["EmpID"]), Convert.ToInt32(txtSalary.Text), Convert.ToInt32(Session["UserId"]), CODEUtility.ConvertToDate(txtDate.Text.Trim(), DateFormat.DayMonthYear)));

                BindGrid();
                Clear();
                if (returnval == 1)
                {
                    AlertMsg.MsgBox(Page, "Inserted successfully! ");
                }
                else if (returnval == 4)
                    AlertMsg.MsgBox(Page, "Effective Date should be greater than existed privious one!");
                else
                {
                    AlertMsg.MsgBox(Page, "Updated successfully!");
                }
                ViewState["Id"] = "";
                MyAccordion.Visible = false;
            }
            catch (Exception ex)
            {
                clsErrorLog.HMSEventLog(ex, "EMPSalHike", "btnSubmit_Click", "003");
            }
        }
        public void Clear()
        {
            txtDate.Text = "";
            txtSalary.Text = "";
            ViewState["Id"] = "";
        }
        protected void lnkAdd_Click(object sender, EventArgs e)
        {
            ViewState["Id"] = String.Empty;
            tblNew.Visible = true;
            tblEdit.Visible = false;
            tblaccordin.Visible = false;
            lnkAdd.CssClass = "lnkselected";
            lnkEdit.CssClass = "linksunselected";

        }
        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            tblEdit.Visible = true;
            tblaccordin.Visible = true;
            tblNew.Visible = false;

            lnkEdit.CssClass = "lnkselected";
            lnkAdd.CssClass = "linksunselected";

        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect(strurl);
        }

        void Bind()
        {
            //cblWages
            using (DataSet ds = PayRollMgr.GetEmpWages(EmpId, EmpSalID))
            {
                grdWages.DataSource = ds;
                grdWages.DataBind();
            }
            //cblAllowences
            using (DataSet ds = PayRollMgr.GetEmpAllowancesList(EmpId, EmpSalID))
            {
                grdAllowances.DataSource = ds;
                grdAllowances.DataBind();
            }
            //cblContributions
            using (DataSet ds = PayRollMgr.GetEmpCoyContributionItemsList(EmpId, EmpSalID))
            {
                grdCContribution.DataSource = ds;
                grdCContribution.DataBind();
            }
            //cblDeductions
            using (DataSet ds = PayRollMgr.GetEmpDeductStatutoryList(EmpId, EmpSalID))
            {
                grdStatutory.DataSource = ds;
                grdStatutory.DataBind();
            }

            //cblDeductions
            using (DataSet ds = PayRollMgr.GetEmpNonCTCComponentsList(EmpId, EmpSalID))
            {
                grdNonCTCComponts.DataSource = ds;
                grdNonCTCComponts.DataBind();
            }
        }


        protected void grdWages_RowDataBound(object sender, GridViewRowEventArgs e)
        {


            try
            {

                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    LinkButton lnkEdt = (LinkButton)e.Row.FindControl("lnkEdt");
                    CheckBox chkWages = (CheckBox)e.Row.FindControl("chkWages");
                    TextBox txtwagepercent = (TextBox)e.Row.FindControl("txtwagepercent");
                    TextBox txtCentageValue = (TextBox)e.Row.FindControl("txtCentageValue1");
                    txtCentageValue.Attributes.Add("OnChange", "javascript:return WagesPercentageCal(this,'" + txtwagepercent.ClientID + "','" +
                        txtCentageValue.ClientID + "','" + empsaltext + "');");
                    lnkEdt.Attributes.Add("onclick", "javascript:return UpdateWages(this,'" + lnkEdt.CommandArgument.ToString()
                        + "','" + EmpId + "','" + chkWages.ClientID + "','" + "0" + "','" + Session["UserId"].ToString() + "','"
                        + txtwagepercent.ClientID + "','" + EmpSalID + "');");
                    //if (grdWages.Enabled == true)
                    //{
                    //    lnkEdt.Enabled = true;
                    //}
                    //else
                    //{
                    //    lnkEdt.Enabled = false;
                    //}

                }
            }
            catch (Exception ex)
            {
                clsErrorLog.HMSEventLog(ex, "EMPSalHike", "gvWages_RowCommand", "004");
            }
        }

        protected void grdAllowances_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton lnkEdt = (LinkButton)e.Row.FindControl("lnkEdt1");
                CheckBox chkAllowances = (CheckBox)e.Row.FindControl("chkAllowances");
                TextBox txtAllowancepercent = (TextBox)e.Row.FindControl("txtAllowancepercent");
                TextBox txtCentageValue1 = (TextBox)e.Row.FindControl("txtCentageValue2");

                CheckBox chkAllowancesIT = (CheckBox)e.Row.FindControl("chkAllowancesIT");


                txtCentageValue1.Attributes.Add("OnChange", "javascript:return AllowancesPercentageCal(this,'" + lnkEdt.CommandArgument.ToString() + "','" + txtAllowancepercent.ClientID + "','" + txtCentageValue1.ClientID + "','" + empsaltext + "','" + EmpId + "','" + EmpSalID + "');");
                lnkEdt.Attributes.Add("onclick", "javascript:return UpdateAllowances(this,'" + lnkEdt.CommandArgument.ToString() + "','" + EmpId + "','" + chkAllowances.ClientID + "','" + "0" + "','" + Session["UserId"].ToString() + "','" + txtAllowancepercent.ClientID + "','" + EmpSalID + "','" + chkAllowancesIT.ClientID + "');");
                //if (grdAllowances.Enabled == true)
                //{
                //    lnkEdt.Enabled = true;
                //}
                //else
                //{
                //    lnkEdt.Enabled = false;
                //}
            }
        }

        protected void grdCContribution_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton lnkEdt = (LinkButton)e.Row.FindControl("lnkEdt2");
                CheckBox chkCContribution = (CheckBox)e.Row.FindControl("chkCContribution");
                TextBox txtCContributionpercent = (TextBox)e.Row.FindControl("txtCContributionpercent");
                TextBox txtCentageValue = (TextBox)e.Row.FindControl("txtCentageValue3");

                CheckBox chkCContributionIT = (CheckBox)e.Row.FindControl("chkCContributionIT");
                //txtCContributionpercent.Enabled = false;
                //txtCentageValue.Enabled = false;

                txtCentageValue.Attributes.Add("OnChange", "javascript:return ContrPercentageCal(this,'" + lnkEdt.CommandArgument.ToString() + "','" + txtCContributionpercent.ClientID + "','" + txtCentageValue.ClientID + "','" + empsaltext + "','" + EmpId + "','" + EmpSalID + "');");
                lnkEdt.Attributes.Add("onclick", "javascript:return UpdateEmpContribution(this,'" + lnkEdt.CommandArgument.ToString() + "','" + EmpId + "','" + chkCContribution.ClientID + "','" + "0" + "','" + Session["UserId"].ToString() + "','" + txtCContributionpercent.ClientID + "','" + EmpSalID + "','" + chkCContributionIT.ClientID + "');");
                //if (grdCContribution.Enabled == true)
                //{
                //    lnkEdt.Enabled = true;
                //}
                //else
                //{
                //    lnkEdt.Enabled = false;
                //}
            }
        }

        protected void grdStatutory_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton lnkEdt = (LinkButton)e.Row.FindControl("lnkEdt3");

                CheckBox chkDStatutory = (CheckBox)e.Row.FindControl("chkDStatutory");
                TextBox txtDStatutorypercent = (TextBox)e.Row.FindControl("txtDStatutorypercent");
                TextBox txtCentageValue = (TextBox)e.Row.FindControl("txtCentageValue4");

                CheckBox chkDStatutoryIT = (CheckBox)e.Row.FindControl("chkDStatutoryIT");

                if (lnkEdt.CommandArgument.ToString() == "9")
                {
                    txtDStatutorypercent.Enabled = false;
                    txtCentageValue.Enabled = false;
                }
                txtCentageValue.Attributes.Add("OnChange", "javascript:return DedudPercentageCal(this,'" + lnkEdt.CommandArgument.ToString() + "','" + txtDStatutorypercent.ClientID + "','" + txtCentageValue.ClientID + "','" + empsaltext + "','" + EmpId + "','" + EmpSalID + "');");
                lnkEdt.Attributes.Add("onclick", "javascript:return UpdateEmpDeductions(this,'" + lnkEdt.CommandArgument.ToString() + "','" + EmpId + "','" + chkDStatutory.ClientID + "','" + "0" + "','" + Session["UserId"].ToString() + "','" + txtDStatutorypercent.ClientID + "','" + EmpSalID + "','" + chkDStatutoryIT.ClientID + "');");
                //if (grdStatutory.Enabled == true)
                //{
                //    lnkEdt.Enabled = true;
                //}
                //else
                //{
                //    lnkEdt.Enabled = false;
                //}
            }
        }

        protected void grdNonCTCComponts_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton lnkEdt = (LinkButton)e.Row.FindControl("lnkEdt4");

                CheckBox chkComp = (CheckBox)e.Row.FindControl("chkNonCTCComp");
                TextBox txtCentageValue = (TextBox)e.Row.FindControl("txtCentageValue");

                lnkEdt.Attributes.Add("onclick", "javascript:return UpdateEmpNonCTCComp(this,'" + lnkEdt.CommandArgument.ToString() + "','" + EmpId + "','" + chkComp.ClientID + "','" + txtCentageValue.ClientID + "','" + EmpSalID + "');");
                //if (grdNonCTCComponts.Enabled == true)
                //{
                //    lnkEdt.Enabled = true;
                //}
                //else
                //{
                //    lnkEdt.Enabled = false;
                //}
            }

        }

        protected void grdWages_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                string wagesid = e.CommandArgument.ToString();
                if (e.CommandName.Trim() == "edt")
                {
                    //CheckBox chkComp = (CheckBox)e.Row.FindControl("chkNonCTCComp");
                    //TextBox txtCentageValue = (TextBox)e.Row.FindControl("txtCentageValue");
                    //if (txtCentageValue.Text != "" && txtCentageValue.Text != null)
                    //{
                    //  // AjaxDAL.UpdateNOnCTCComponents(wagesid, lblEmpNo.Text, 0, EmpSalID.ToString (), txtCentageValue.Text.ToString());
                    //}

                }
            }
            catch { }

        }

        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute]
        public static string[] GetCompletionList(string prefixText, int count, string contextKey)
        {
            DataSet ds = AttendanceDAC.GetWorkSite_GoogleSech_By_Employees(prefixText, SearchCompanyID, Roleid);
            return ConvertStingArray(ds);
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute]
        public static string[] GetCompletionListdept(string prefixText, int count, string contextKey)
        {
            DataSet ds = AttendanceDAC.GetDepartmentGoogleSerc(prefixText, SearchCompanyID, Siteid);
            return ConvertStingArray(ds);
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute]
        public static string[] GetCompletionListdesi(string prefixText, int count, string contextKey)
        {
            DataSet ds = AttendanceDAC.GetGoogleSerachDesignations(prefixText);
            return ConvertStingArray(ds);
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute]
        public static string[] GetCompletionListCat(string prefixText, int count, string contextKey)
        {
            DataSet ds = AttendanceDAC.GetGoogleSerachCategory(prefixText);
            return ConvertStingArray(ds);
        }
        public static string[] ConvertStingArray(DataSet ds)
        {
            string[] rtval = Array.ConvertAll(ds.Tables[0].Select(), new Converter<DataRow, string>(DataRowTotable));
            return rtval;
        }
        public static string DataRowTotable(DataRow dr)
        {
            return dr["Name"].ToString();
        }
        protected void GetDepartmentSearch(object sender, EventArgs e)
        {
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@Search", txtSearchdept.Text);
            param[1] = new SqlParameter("@CompanyID", Convert.ToInt32(Session["CompanyID"]));
            param[2] = new SqlParameter("@SiteID", ddlworksites.SelectedItem.Value);
            FIllObject.FillDropDown(ref ddldepartments, "HMS_googlesearch_GetDepartmentBySite", param);
            ListItem itmSelected = ddldepartments.Items.FindByText(txtSearchdept.Text);
            if (itmSelected != null)
            {
                ddldepartments.SelectedItem.Selected = false;
                itmSelected.Selected = true;
            }
            if (txtSearchdept.Text != "") { ddldepartments.SelectedIndex = 1; }
        }
        protected void ddldepartments_SelectedIndexChanged(object sender, EventArgs e)
        {


        }
        protected void GetDesignation(object sender, EventArgs e)
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@Search", txtDesignationSerach.Text);
            FIllObject.FillDropDown(ref ddlSearDesigantion, "HR_GetSearchgoogleDesignations", param);
            ListItem itmSelected = ddlSearDesigantion.Items.FindByText(txtDesignationSerach.Text);
            if (itmSelected != null)
            {
                ddlSearDesigantion.SelectedItem.Selected = false;
                itmSelected.Selected = true;
            }
        }
        protected void GetCategory(object sender, EventArgs e)
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@Search", txtCategorySearch.Text);
            FIllObject.FillDropDown(ref ddlSearCategory, "HR_GoogleSearc_GetCategories", param);
            ListItem itmSelected = ddlSearCategory.Items.FindByText(txtCategorySearch.Text);
            if (itmSelected != null)
            {
                ddlSearCategory.SelectedItem.Selected = false;
                itmSelected.Selected = true;
            }
        }

        void EmployeBind(HRCommon objHrCommon)
        {
            try
            {
                objHrCommon.PageSize = EmpListPaging.ShowRows;
                objHrCommon.CurrentPage = EmpListPaging.CurrentPage;
                objHrCommon.SiteID = Convert.ToInt32(ddlworksites.SelectedItem.Value);
                objHrCommon.DeptID = Convert.ToDouble(ddldepartments.SelectedItem.Value);

                int? DesigID = null;
                int? CatID = null;
                if (ddlSearDesigantion.SelectedIndex > 0)
                    DesigID = Convert.ToInt32(ddlSearDesigantion.SelectedValue);

                if (ddlSearCategory.SelectedIndex > 0)
                    CatID = Convert.ToInt32(ddlSearCategory.SelectedValue);

                int? EmpNatureID = null;
                if (ddlEmpNature.SelectedValue != "0")
                    EmpNatureID = Convert.ToInt32(ddlEmpNature.SelectedValue);
                if (txtOldEmpID.Text == "")
                    objHrCommon.OldEmpID = null;

                objHrCommon.CurrentStatus = 'y';


                objHrCommon.FName = txtusername.Text;
                DataSet ds = new DataSet();
                gveditkbipl.DataSource = null;
                gveditkbipl.DataBind();
                Nullable<int> empid;
                if (Session["Empid"] == null || Session["Empid"] == "")
                {
                    empid = null;
                }
                else
                {
                    empid = Convert.ToInt32(Session["Empid"]);
                }
                // OrderID = Convert.ToInt32(ViewState["OrderID"]);

                try
                {

                    if (Convert.ToInt32(ViewState["WSID"]) > 0)
                        objHrCommon.SiteID = Convert.ToInt32(ViewState["WSID"]);
                }
                catch { }
                ds = (DataSet)objEmployee.GetEmployeesByPageOrderByAssID(objHrCommon, 2, 0, EmpNatureID, empid, Convert.ToInt32(Session["CompanyID"]), DesigID, CatID);
                //ds = (DataSet)objEmployee.GetEmployeesByPage(objHrCommon);
                Session["Empid"] = null;

                if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0)
                {
                    gveditkbipl.DataSource = ds;
                    gveditkbipl.DataBind();

                }
                EmpListPaging.Bind(objHrCommon.CurrentPage, objHrCommon.TotalPages, objHrCommon.NoofRecords, objHrCommon.PageSize);

            }
            catch (Exception ex)
            {
                clsErrorLog.HMSEventLog(ex, "EmployeeList", "EmployeeBind", "002");
            }
        }
        public void BindEmpNatures()
        {
            DataSet ds = new DataSet();
            ds = Leaves.GetEmpNatureList(1);
            ddlEmpNature.DataSource = ds;
            ddlEmpNature.DataTextField = "Nature";
            ddlEmpNature.DataValueField = "NatureOfEmp";
            ddlEmpNature.DataBind();
            ddlEmpNature.Items.Insert(0, new ListItem("---All---", "0"));
        }

        protected void gveditkbipl_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Revise")
            {

                QuerStrngEmpid = Convert.ToInt32(e.CommandArgument);
                Cache["EmpID"] = QuerStrngEmpid;
                tblmain.Visible = true;
                BindEmpDetails();
                foreach (GridViewRow row in gveditkbipl.Rows)
                {
                    LinkButton lnkReviseold = (LinkButton)row.FindControl("lnkRevise");
                    lnkReviseold.CssClass = "anchor__grd edit_grd";
                }
                  GridViewRow gvr = (GridViewRow)(((Control)e.CommandSource).NamingContainer);

                  LinkButton lnkRevise = (LinkButton)gvr.FindControl("lnkRevise");
                  lnkRevise.CssClass = "anchor__grd vw_grd";
                  btnclose.Visible = false;

            }
        }
        protected void ddlworksites_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindDeparmetBySite(Convert.ToInt32(ddlworksites.SelectedValue));
            Siteid = Convert.ToInt32(ddlworksites.SelectedValue);
            EmpListPaging.CurrentPage = 1;

        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            // OrderID = 0;
            try
            {
                //if (chkHis.Checked)
                //    gveditkbipl.Columns[1].Visible = true;
                //else
                //    gveditkbipl.Columns[1].Visible = false;

                SearchEmp();
            }
            catch (Exception ex)
            {
                clsErrorLog.HMSEventLog(ex, "EmployeeList", "btnSearch_Click", "006");
            }
        }

        public void SearchEmp()
        {
            tblmain.Visible = false;
            try
            {
                if (txtEmpID.Text == "")
                {
                    EmpListPaging.Visible = true;
                    objHrCommon.SiteID = Convert.ToInt32(ddlworksites.SelectedItem.Value);
                    objHrCommon.DeptID = Convert.ToDouble(ddldepartments.SelectedItem.Value);
                    objHrCommon.FName = txtusername.Text;
                    objHrCommon.OldEmpID = txtOldEmpID.Text;
                    try
                    {

                        if (Convert.ToInt32(ViewState["WSID"]) > 0)
                            objHrCommon.SiteID = Convert.ToInt32(ViewState["WSID"]);
                    }
                    catch { }
                    //objHrCommon.OldEmployeeID = txtOldEmpID.Text;
                    EmpListPaging.ShowRows=10;
                    EmpListPaging.CurrentPage=1;
                    EmployeBind(objHrCommon);
                }
                else
                {
                    EmpListPaging.Visible = false;
                    int EmpID = 0;
                    try
                    {
                        EmpID = Convert.ToInt32(txtEmpID.Text);
                        DataSet ds = new DataSet();
                        int Status = 1;
                        //if (rbInActive.Checked == true)
                        //    Status = 2;
                        ds = AttendanceDAC.HR_EmpGetEmpDetailsByID(EmpID, Status);
                        gveditkbipl.DataSource = ds;
                        gveditkbipl.DataBind();
                    }
                    catch { AlertMsg.MsgBox(Page, "Check the data you have given..!"); }


                }
            }
            catch (Exception ex)
            {
                clsErrorLog.HMSEventLog(ex, "EmployeeList", "SearchEMP", "007");
            }
        }
        public void BindDeparmetBySite(int Site)
        {
            DataSet ds = AttendanceDAC.BindDeparmetBySite(Site, Convert.ToInt32(Session["CompanyID"]));
            ViewState["Departments"] = ds;
            ddldepartments.DataSource = ds;
            ddldepartments.DataTextField = "DeptName";
            ddldepartments.DataValueField = "DepartmentUId";
            ddldepartments.DataBind();
            ddldepartments.Items.Insert(0, new ListItem("---ALL---", "0", true));
        }
        public string GetWorkSite(string WSid)
        {
            string retVal = "";
            try
            {
                DataSet ds = (DataSet)ViewState["WorkSites"];
                retVal = ds.Tables[0].Select("Site_ID='" + WSid + "'")[0]["Site_Name"].ToString();
            }
            catch { }
            return retVal;
        }
        public string GetDepartment(string DeptId)
        {
            string retVal = "";
            try
            {
                DataSet ds = (DataSet)ViewState["Departments"];
                retVal = ds.Tables[0].Select("DepartmentUId='" + DeptId + "'")[0]["DeptName"].ToString();
            }

            catch { }
            return retVal;
        }
        protected void Worksidechangewithdep(object sender, EventArgs e)
        {
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@Search", txtSearchWorksite.Text);
            param[1] = new SqlParameter("@CompanyID", Convert.ToInt32(Session["CompanyID"]));
            param[2] = new SqlParameter("@Role", Convert.ToInt32(Session["RoleId"]));
            // FIllObject.FillDropDown(ref ddlWorksite, "G_GET_WorkSitebyFilter", param);
            FIllObject.FillDropDown(ref ddlworksites, "HR_GoogleSearch_GetWorksite_By_Employees", param);
            ListItem itmSelected = ddlworksites.Items.FindByText(txtSearchWorksite.Text);
            if (itmSelected != null)
            {
                ddlworksites.SelectedItem.Selected = false;
                itmSelected.Selected = true;
                //FillProjects();
            }
            ddlworksites_SelectedIndexChanged(sender, e);
            txtSearchdept.Text = "";
        }

        void colorchange()
        {

        

          //  EmployeBind(objHrCommon);


         
            }

      

        void BindEmpDetails()
        {

          //  colorchange();

            if (Cache["EmpID"] != null && Cache["EmpID"].ToString().Length > 0)
            {
                ViewState["EmpID"] = Cache["EmpID"].ToString();

            }
            else
            {
                ViewState["EmpID"] = QuerStrngEmpid;

            }
            

            EmpId = Convert.ToInt32(ViewState["EmpID"]);
            tblEdit.Visible = true;
            tblaccordin.Visible = true;
            lnkEdit.CssClass = "lnkselected";
            lnkAdd.CssClass = "linksunselected";
            BindGrid();
            Bind();

            MyAccordion.Visible = false;
        }

        protected void gvAllowances_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lnkfrom = (Label)e.Row.FindControl("lblFromDate");
                LinkButton lnkEdt = (LinkButton)e.Row.FindControl("LinkButton1");
                DateTime dd = CODEUtility.ConvertToDate(lnkfrom.Text, DateFormat.DayMonthYear);
                if (dd.AddHours(24) < DateTime.UtcNow.AddHours(5).AddMinutes(30))
                {
                    lnkEdt.Enabled = false;
                }
            }

        }
        void EnableGvAllownce()
        {
            foreach (GridViewRow gvrow in gvAllowances.Rows)
            {
                LinkButton lnkEdt = (LinkButton)gvrow.FindControl("LinkButton1");
                LinkButton lnkBreak = (LinkButton)gvrow.FindControl("lnkBreak");
                if (gvrow.RowIndex == (gvAllowances.Rows.Count - 1))
                {
                    lnkEdt.Enabled = true;
                    lnkBreak.Enabled = true;
                    grdWages.Enabled = true;
                    grdAllowances.Enabled = true;
                    grdCContribution.Enabled = true;
                    grdStatutory.Enabled = true;
                    grdNonCTCComponts.Enabled = true;
                }
                else
                {
                    lnkEdt.Enabled = false;




                    grdWages.Enabled = false;
                    grdAllowances.Enabled = false;
                    grdCContribution.Enabled = false;
                    grdStatutory.Enabled = false;
                    grdNonCTCComponts.Enabled = false;
                }

                Label lnkfrom = (Label)gvrow.FindControl("lblFromDate");
                LinkButton lnkEdt1 = (LinkButton)gvrow.FindControl("LinkButton1");
                DateTime dd = CODEUtility.ConvertToDate(lnkfrom.Text, DateFormat.DayMonthYear);
                if (dd.AddHours(24) < DateTime.UtcNow.AddHours(5).AddMinutes(30))
                {
                    lnkEdt1.Enabled = false;
                }

            }
        }

        protected void btnclose_Click(object sender, EventArgs e)
        {
            tblaccordin.Visible = false;
            BindEmpDetails();
            btnclose.Visible = false;
            // btnApply.Visible = false;
        }

        protected void grdWages_RowCommand1(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "edt")
            {
                BindEmpDetails();
                MyAccordion.Visible = true;
            }
        }

        //protected void btnApply_Click(object sender, EventArgs e)
        //{

        //}
    }
}