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

namespace AECLOGIC.ERP.HMS
{
    public partial class EmpSalHikes : AECLOGIC.ERP.COMMON.WebFormMaster
    {
        AttendanceDAC objAtt = new AttendanceDAC();
        AjaxDAL Aj = new AjaxDAL();
          
        static string strurl = string.Empty;
          int   EmpId =0; 
                  int  EmpSalID = 0;
                  string empsaltext = "0";
        protected override void OnInit(EventArgs e)
        {
            ModuleID = 1;
            base.OnInit(e);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
        try{ 
             Ajax.Utility.RegisterTypeForAjax(typeof(AjaxDAL));
            if (!IsPostBack)
            {
                    Ajax.Utility.RegisterTypeForAjax(typeof(AjaxDAL));
                try { strurl = Request.UrlReferrer.ToString(); }
                catch { }
                ViewState["Id"] = "";
                ViewState["EmpID"] = Convert.ToInt32(Request.QueryString["EmpID"]); 
               EmpId =Convert.ToInt32(Request.QueryString["EmpID"]); 
                tblEdit.Visible = true;
                tblaccordin.Visible = true;
                lnkEdit.CssClass = "lnkselected";
                lnkAdd.CssClass = "linksunselected";
                BindGrid();

                MyAccordion.Visible = false;
            }
        }
        catch (Exception ex)
        {
            clsErrorLog.HMSEventLog(ex, "EMPSalHike", "Page_Load", "001");
        }

        }
        public void BindGrid()
        {
            tblEdit.Visible = true;
            tblaccordin.Visible = true;
            tblNew.Visible = false;
            lnkEdit.CssClass = "lnkselected";
            lnkAdd.CssClass = "linksunselected";
            int EmpID = Convert.ToInt32(ViewState["EmpID"]);
           DataSet ds = AttendanceDAC.GetEmpSalList(EmpID);
            if (ds.Tables[0].Rows.Count > 0)
            {
                lblName.Text = ds.Tables[0].Rows[0]["name"].ToString();
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
             EmpSalID =   Convert.ToInt32 (ds.Tables[0].Rows[ds.Tables[0].Rows.Count-1 ]["EmpSalID"]);
             empsaltext = ds.Tables[0].Rows[ds.Tables[0].Rows.Count - 1]["Salary"].ToString();
             
        }
        public void BindDetails(int ID)
        {
           tblEdit.Visible = false;
           tblaccordin.Visible =  false ;
            tblNew.Visible = true;
          DataSet  ds = AttendanceDAC.GetEmpSalDetails(ID);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                txtSalary.Text = ds.Tables[0].Rows[0]["Salary"].ToString();
                txtDate.Text = ds.Tables[0].Rows[0]["FromDate"].ToString();
            }
        }
        protected void gvWages_RowCommand(object sender, GridViewCommandEventArgs e)
        {
          try{  int ID = Convert.ToInt32(e.CommandArgument);
            int EmpID = Convert.ToInt32(ViewState["EmpID"].ToString());

            ViewState["Id"] = ID;
            if (e.CommandName == "Edt")
            {
                BindDetails(ID);
            }
            else if (e.CommandName == "brkup")
            {
                  
                GridViewRow gvr = (GridViewRow)(((Control)e.CommandSource).NamingContainer);
                Label lblAudited = (Label)gvr.FindControl("lblAudited");
                Label lblSalary = (Label)gvr.FindControl("lblSalary");
              
                if (lblAudited.Text == "False")
                {
                    empsaltext = lblSalary.Text;
                    EmpId = EmpID; //Response.Redirect("EmpPayRoleConfig.aspx?EmpID=" + EmpID + "&ID=" + ID + "&Salary=" + lblSalary.Text);
                    EmpSalID = ID;
                    Bind();
                    MyAccordion.Visible = true;
                }
                else
                {
                    AlertMsg.MsgBox(Page, "Financial year Audited:");
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
         try{   int EmpSalID = 0;
            if (ViewState["Id"].ToString() != null && ViewState["Id"].ToString() != string.Empty)
            {
                EmpSalID = Convert.ToInt32(ViewState["Id"].ToString());
            }
            int returnval;

            returnval = Convert.ToInt32(AttendanceDAC.InsUpdateEmpSal(EmpSalID, Convert.ToInt32(ViewState["EmpID"]), Convert.ToInt32(txtSalary.Text),  Convert.ToInt32(Session["UserId"]), CODEUtility.ConvertToDate(txtDate.Text.Trim(), DateFormat.DayMonthYear)));

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

    #region  wagesGrid_BP
      protected void grdWages_RowDataBound(object sender, GridViewRowEventArgs e)
        {

          try{  if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton lnkEdt = (LinkButton)e.Row.FindControl("lnkEdt");
                CheckBox chkWages = (CheckBox)e.Row.FindControl("chkWages");
                TextBox txtwagepercent = (TextBox)e.Row.FindControl("txtwagepercent");
                TextBox txtCentageValue = (TextBox)e.Row.FindControl("txtCentageValue1");
                txtCentageValue.Attributes.Add("OnChange", "javascript:return WagesPercentageCal(this,'" + txtwagepercent.ClientID + "','" + txtCentageValue.ClientID + "','" + empsaltext + "');");
                lnkEdt.Attributes.Add("onclick", "javascript:return UpdateWages(this,'" + lnkEdt.CommandArgument.ToString() + "','" + EmpId  + "','" + chkWages.ClientID + "','" + "0" + "','" +  Convert.ToInt32(Session["UserId"]).ToString() + "','" + txtwagepercent.ClientID + "','" + EmpSalID + "');");

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
                lnkEdt.Attributes.Add("onclick", "javascript:return UpdateAllowances(this,'" + lnkEdt.CommandArgument.ToString() + "','" + EmpId  + "','" + chkAllowances.ClientID + "','" + "0" + "','" +  Convert.ToInt32(Session["UserId"]).ToString() + "','" + txtAllowancepercent.ClientID + "','" + EmpSalID + "','" + chkAllowancesIT.ClientID + "');");
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
               

                txtCentageValue.Attributes.Add("OnChange", "javascript:return ContrPercentageCal(this,'" + lnkEdt.CommandArgument.ToString() + "','" + txtCContributionpercent.ClientID + "','" + txtCentageValue.ClientID + "','" + empsaltext + "','" + EmpId + "','" + EmpSalID + "');");
                lnkEdt.Attributes.Add("onclick", "javascript:return UpdateEmpContribution(this,'" + lnkEdt.CommandArgument.ToString() + "','" +  EmpId + "','" + chkCContribution.ClientID + "','" + "0" + "','" +  Convert.ToInt32(Session["UserId"]).ToString() + "','" + txtCContributionpercent.ClientID + "','" + EmpSalID + "','" + chkCContributionIT.ClientID + "');");
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
                lnkEdt.Attributes.Add("onclick", "javascript:return UpdateEmpDeductions(this,'" + lnkEdt.CommandArgument.ToString() + "','" +  EmpId + "','" + chkDStatutory.ClientID + "','" + "0" + "','" +  Convert.ToInt32(Session["UserId"]).ToString() + "','" + txtDStatutorypercent.ClientID + "','" + EmpSalID + "','" + chkDStatutoryIT.ClientID + "');");
            }
        }

        protected void grdNonCTCComponts_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton lnkEdt = (LinkButton)e.Row.FindControl("lnkEdt4");

                CheckBox chkComp = (CheckBox)e.Row.FindControl("chkNonCTCComp");
                TextBox txtCentageValue = (TextBox)e.Row.FindControl("txtCentageValue");

                lnkEdt.Attributes.Add("onclick", "javascript:return UpdateEmpNonCTCComp(this,'" + lnkEdt.CommandArgument.ToString() + "','" +EmpId  + "','" + chkComp.ClientID + "','" + txtCentageValue.ClientID + "','" + EmpSalID + "');");
            }

        }

      
    }
#endregion


}
