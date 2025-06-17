using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AECLOGIC.HMS.BLL;
using System.Data;
using System.Data.SqlClient;
using Aeclogic.Common.DAL;
using System.IO;
using System.Configuration;
using System.Collections;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using AECLOGIC.ERP.COMMON;

namespace AECLOGIC.ERP.HMS
{
    public partial class ManPowerIndent : AECLOGIC.ERP.COMMON.WebFormMaster
    {
        HRCommon objMpReq = new HRCommon();
        int mid = 0;
        string menuname;
        string menuid;
        int edittxtColNo = 10;
        int btnUpdateColNo = 11;
        int btnApprColNo = 12;
        int btnRejectColNo = 13;
        protected void Page_Load(object sender, EventArgs e)
        {
           
            if (!IsPostBack)
            {
                bindddl();
                ViewState["KeyAction"] = "";
                if (Request.QueryString["key"] != null && Request.QueryString["key"] != "")
                {

                    pnltblEdit.Visible = false;
                    pnlDetails.Visible = true;

                }
                else
                {
                    pnltblEdit.Visible = true;
                    pnlDetails.Visible = true;
                }

                FIllObject.FillDropDown(ref ddsrg, "HMS_GetManPower_Group");
                if (ddsrg.Items.Count > 0)
                {
                    ddsrg.SelectedIndex = 1;
                    BindResources(txtnewsearch.Text.Trim());
                }
                //Specialisation
                FIllObject.FillDropDown(ref ddlSpecialisation, "HR_GetCategories");
                //Designation
                FIllObject.FillDropDown(ref ddlDesignation, "HR_GetDesignations");
                OMSManPowerRequistion(objMpReq, 0);
            }
        }
     
        void BindResources(string ResouceSearch)
        {
            SqlParameter[] sp1 = new SqlParameter[3];
            sp1[0] = new SqlParameter("@GroupId", ddsrg.SelectedValue);
            sp1[1] = new SqlParameter("@ResouceSearch", ResouceSearch);
            sp1[2] = new SqlParameter("@cat_type", 2);
            FIllObject.FillListBox(ref lbxItems, "HMS_GroupWiseItems", sp1);
            if (lbxItems.Items.Count == 0)
            {
                lbxItems.Items.Add(new ListItem("No Records", "0"));
            }
        }

        protected override void OnInit(EventArgs e)
        {
            ModuleID = 1;
            base.OnInit(e);

            MPReq.FirstClick += new Paging.PageFirst(MPReq_FirstClick);
            MPReq.PreviousClick += new Paging.PagePrevious(MPReq_FirstClick);
            MPReq.NextClick += new Paging.PageNext(MPReq_FirstClick);
            MPReq.LastClick += new Paging.PageLast(MPReq_FirstClick);
            MPReq.ChangeClick += new Paging.PageChange(MPReq_FirstClick);
            MPReq.ShowRowsClick += new Paging.ShowRowsChange(MPReq_ShowRowsClick);
            MPReq.CurrentPage = 1;
        }

        void MPReq_ShowRowsClick(object sender, EventArgs e)
        {
            MPReq.CurrentPage = 1;
            BindManPowerPager();
        }
        void MPReq_FirstClick(object sender, EventArgs e)
        {
            BindManPowerPager();

        }
        void BindManPowerPager()
        {

            objMpReq.PageSize = MPReq.ShowRows;
            objMpReq.CurrentPage = MPReq.CurrentPage;
            if (Request.QueryString["key"] != null && Request.QueryString["key"] != "")
            {
                OMSManPowerRequistion(objMpReq, Convert.ToInt32(Request.QueryString["key"].ToString()));
            }
            else
                OMSManPowerRequistion(objMpReq, 0);
        }

        protected void btnSearchNew_Click(object sender, EventArgs e)
        {
            BindResources(txtnewsearch.Text.Trim());
        }

        protected void ddsrg_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindResources(txtnewsearch.Text.Trim());
        }

        protected void btnADDMultiple_Click(object sender, EventArgs e)
        {
            ListItem item = null;
            DateTime Frmdt = CodeUtilHMS.ConvertToDate_ddMMMyyy(txtFrom.Text.Trim(), CodeUtilHMS.DateFormat.ddMMMyyyy);
            int DesignID = Convert.ToInt32(ddlDesignation.SelectedValue);
            int CatID = Convert.ToInt32(ddlSpecialisation.SelectedValue);
            decimal nMpNos = Convert.ToDecimal(txtMPNos.Text);
            foreach (int indexItem in lbxItems.GetSelectedIndices())
            {
                item = lbxItems.Items[indexItem];
                AttendanceDAC.OMS_Insert_ManPower_Requisition(Convert.ToInt32(Session["CompanyID"]), Convert.ToInt32(item.Value), DesignID, 
                    CatID, Frmdt, nMpNos,  Convert.ToInt32(Session["UserId"]),Convert.ToInt32(ddlWorksiteID.SelectedValue),
                    Convert.ToInt32(ddlProjectID.SelectedValue),
                    txtReqReference.Text.Trim());
                           }            
         
            AlertMsg.MsgBox(Page, "Done");
            OMSManPowerRequistion(objMpReq, 0);
        }
        void OMSManPowerRequistion(HRCommon objMpReq, int Key)
        {
            try
            {
                objMpReq.PageSize = MPReq.ShowRows;
                objMpReq.CurrentPage = MPReq.CurrentPage;
                 
                int KeyNew = 0;
                if (Request.QueryString["key"] != null && Request.QueryString["key"] != "")
                {
                    KeyNew = Convert.ToInt32(Request.QueryString["key"].ToString());
                }
               
                DateTime Frmdt = CodeUtilHMS.ConvertToDate_ddMMMyyy(txtFromDate.Text.Trim(), CodeUtilHMS.DateFormat.ddMMMyyyy);
                DateTime Todt = CodeUtilHMS.ConvertToDate_ddMMMyyy(txtToDate.Text.Trim(), CodeUtilHMS.DateFormat.ddMMMyyyy);
             DataSet   ds = AttendanceDAC.HMS_GET_MANPOWER_REQUISITION(objMpReq, Convert.ToInt32(Session["CompanyID"]), KeyNew,
                    Convert.ToInt32(ddlWorkSite22.SelectedValue), Convert.ToInt32(ddlProject1.SelectedValue), Convert.ToInt32(ddlDesignation1.SelectedValue),
                    Convert.ToInt32(ddlSpecialisation1.SelectedValue), Frmdt, Todt);
                if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0)
                {
                    gvMPReq.DataSource = ds;
                    MPReq.Bind(objMpReq.CurrentPage, objMpReq.TotalPages, objMpReq.NoofRecords, objMpReq.PageSize);
                    MPReq.Visible = true;
                }
                else
                {
                    gvMPReq.DataSource = null;
          
                    gvMPReq.EmptyDataText = "No Records Found";
                    MPReq.Visible = false;
                }
                
                if (Request.QueryString["key"] != null && Request.QueryString["key"] != "")
                {
                   
                    gvMPReq.Columns[edittxtColNo].Visible = false;
                    if (Request.QueryString["key"] == "0")
                    {
                       
                        gvMPReq.Columns[edittxtColNo].Visible = true;
                        gvMPReq.Columns[btnUpdateColNo].Visible = true;
                        gvMPReq.Columns[btnApprColNo].Visible = true;
                        gvMPReq.Columns[btnRejectColNo].Visible = true;
                    }
                    else if (Request.QueryString["key"] == "1")
                    {
                        gvMPReq.Columns[btnUpdateColNo].Visible = true;
                        gvMPReq.Columns[btnApprColNo].Visible = false;
                        gvMPReq.Columns[btnRejectColNo].Visible = false;
                    }
                    else if (Request.QueryString["key"] == "2")
                    {
                        gvMPReq.Columns[btnUpdateColNo].Visible = true;
                        gvMPReq.Columns[btnApprColNo].Visible = false;
                        gvMPReq.Columns[btnRejectColNo].Visible = false;
                    }
                    else
                    {
                        gvMPReq.Columns[9].Visible = false;
                        gvMPReq.Columns[10].Visible = false;
                    }
                }
                else
                {
                  
                    gvMPReq.Columns[edittxtColNo].Visible = true;
                    gvMPReq.Columns[btnUpdateColNo].Visible = true;
                    gvMPReq.Columns[btnApprColNo].Visible = false;
                    gvMPReq.Columns[btnRejectColNo].Visible = false;
                  
                }

                gvMPReq.DataBind();

            }
            catch (Exception)
            {

                throw;
            }


        }
        protected void gvMPReq_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Upd")
            {
                int OMSMPReqID = Convert.ToInt32(e.CommandArgument);
                GridViewRow rownum = (GridViewRow)(((Control)e.CommandSource).NamingContainer);
                Label lblOMSMPReqID = (Label)rownum.FindControl("lblOMSMPReqID");
                TextBox txtHours = (TextBox)rownum.FindControl("txtMPHours");
                if(txtHours.Text!="")
                {
                    if(Convert.ToDecimal(txtHours.Text)==0)
                    {
                        AlertMsg.MsgBox(Page, "Enter Manpower (No's)");
                        return;
                    }
                }
                int UserID = Convert.ToInt32( Convert.ToInt32(Session["UserId"]).ToString());
                SqlParameter[] sqlParams = new SqlParameter[3];

                sqlParams[0] = new SqlParameter("@OMSMPReqID", OMSMPReqID);
                sqlParams[1] = new SqlParameter("@UserID", UserID);
                sqlParams[2] = new SqlParameter("@Hours", Convert.ToDecimal(txtHours.Text));
                SQLDBUtil.ExecuteNonQuery("Update_Hours_HMS_ManPower_Requisition", sqlParams);
                AlertMsg.MsgBox(Page, "Done");
                OMSManPowerRequistion(objMpReq, 0);                

            }
            if (e.CommandName == "Approve")
            {
                int ID = -1;
                ID = 0;   // approve from OMS            
                int OMSMPReqID = Convert.ToInt32(e.CommandArgument);
                GridViewRow rownum = (GridViewRow)(((Control)e.CommandSource).NamingContainer);
                Label lblOMSMPReqID = (Label)rownum.FindControl("lblOMSMPReqID");
                int UserID = Convert.ToInt32( Convert.ToInt32(Session["UserId"]).ToString());
                SqlParameter[] sqlParams = new SqlParameter[3];
                sqlParams[0] = new SqlParameter("@OMSMPReqID", Convert.ToInt32(lblOMSMPReqID.Text));
                sqlParams[1] = new SqlParameter("@UserID", UserID);
                sqlParams[2] = new SqlParameter("@ID", ID);
                SQLDBUtil.ExecuteNonQuery("Update_Status_OMS_ManPower_Requisition", sqlParams);
                AlertMsg.MsgBox(Page, "Done");
                Response.Redirect("~/hms/ManPowerIndent.aspx?Key=1");
             
            }
            if (e.CommandName == "Reject")
            {
                int ID = 1;
                int OMSMPReqID = Convert.ToInt32(e.CommandArgument);
                GridViewRow rownum = (GridViewRow)(((Control)e.CommandSource).NamingContainer);
                Label lblOMSMPReqID = (Label)rownum.FindControl("lblOMSMPReqID");
                int UserID = Convert.ToInt32( Convert.ToInt32(Session["UserId"]).ToString());
                SqlParameter[] sqlParams = new SqlParameter[3];
                sqlParams[0] = new SqlParameter("@OMSMPReqID", Convert.ToInt32(lblOMSMPReqID.Text));
                sqlParams[1] = new SqlParameter("@UserID", UserID);
                sqlParams[2] = new SqlParameter("@ID", ID);
                SQLDBUtil.ExecuteNonQuery("Update_Status_OMS_ManPower_Requisition", sqlParams);
                AlertMsg.MsgBox(Page, "Done");
                Response.Redirect("~/hms/ManPowerIndent.aspx?Key=2");
            }
        }

        protected void gvMPReq_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    if (Request.QueryString["key"] != null && Request.QueryString["key"] != "")
                    {
                        if (Request.QueryString["key"] == "0")
                        {
                            LinkButton lnkUpdate = (LinkButton)e.Row.FindControl("lnkUpdate");
                            LinkButton lnkReject = (LinkButton)e.Row.FindControl("lnkReject");
                            lnkReject.Visible = true;
                        }
                       
                        else
                        {
                            LinkButton lnkUpdate = (LinkButton)e.Row.FindControl("lnkUpdate");
                            LinkButton lnkReject = (LinkButton)e.Row.FindControl("lnkReject");
                            lnkReject.Visible = false;
                            lnkUpdate.Visible = false;
                        }

                    }
                    else
                    {
                        LinkButton lnkUpdate = (LinkButton)e.Row.FindControl("lnkUpdate");
                        LinkButton lnkReject = (LinkButton)e.Row.FindControl("lnkReject");
                        lnkReject.Visible = false;
                        lnkUpdate.Visible = true;
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        private void bindddl()
        {
            SqlParameter[] parms1 = new SqlParameter[1];
            parms1[0] = new SqlParameter("@fCase", 1);
            FIllObject.FillDropDown(ref ddlWorksiteID, "T_OMS_Machinery_Requisition_Unplanned_ddl", parms1);

            SqlParameter[] parms2 = new SqlParameter[1];
            parms2[0] = new SqlParameter("@fCase", 2);
            FIllObject.FillDropDown(ref ddlProjectID, "T_OMS_Machinery_Requisition_Unplanned_ddl", parms2);


            SqlParameter[] parms3 = new SqlParameter[1];
            parms3[0] = new SqlParameter("@fCase", 1);
            FIllObject.FillDropDown(ref ddlWorkSite22, "T_OMS_Machinery_Requisition_Unplanned_ddl", parms3);

            SqlParameter[] parms4 = new SqlParameter[1];
            parms4[0] = new SqlParameter("@fCase", 2);
            FIllObject.FillDropDown(ref ddlProject1, "T_OMS_Machinery_Requisition_Unplanned_ddl", parms4);

            //Specialisation
            FIllObject.FillDropDown(ref ddlSpecialisation1, "HR_GetCategories");
            //Designation
            FIllObject.FillDropDown(ref ddlDesignation1, "HR_GetDesignations");

            txtFromDate.Text = DateTime.Now.ToString(DateDisplayFormat);
            txtToDate.Text = DateTime.Now.ToString(DateDisplayFormat);
            DateTime Fromdt=FirstDayOfMonth( DateTime.Now);
            DateTime Todt=LastDayOfMonth( DateTime.Now);
            txtFromDate.Text = Fromdt.ToString(DateDisplayFormat);
            txtToDate.Text = Todt.ToString(DateDisplayFormat);
        }

        public DateTime FirstDayOfMonth(DateTime dateTime)
        {
           return new DateTime(dateTime.Year, dateTime.Month, 1);
        }


        public DateTime LastDayOfMonth(DateTime dateTime)
        {
           DateTime firstDayOfTheMonth = new DateTime(dateTime.Year, dateTime.Month, 1);
           return firstDayOfTheMonth.AddMonths(1).AddDays(-1);
        }
      
        private void FillProjects1()
        {
            SqlParameter[] p = new SqlParameter[2];
            p[0] = new SqlParameter("@Worksite", ddlWorkSite22.SelectedValue);

            if (CompanyID != null)
                p[1] = new SqlParameter("@CompanyID", CompanyID);
            else
                p[1] = new SqlParameter("@CompanyID", SqlDbType.Int);

            FIllObject.FillDropDown(ref ddlProject1, "OMS_GetProjectByWorksite", p);
            try
            {
                if (ddlProject1.Items.Count > 1)
                    ddlProject1.SelectedIndex = 1;
                else
                    ddlProject1.SelectedIndex = 0;
            }
            catch { }
        }
        protected void ddlWorksiteID_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillProjects();

        }
        
        private void FillProjects()
        {
            SqlParameter[] p = new SqlParameter[2];
            p[0] = new SqlParameter("@Worksite", ddlWorksiteID.SelectedValue);

            if (CompanyID != null)
                p[1] = new SqlParameter("@CompanyID", CompanyID);
            else
                p[1] = new SqlParameter("@CompanyID", SqlDbType.Int);

            FIllObject.FillDropDown(ref ddlProjectID, "OMS_GetProjectByWorksite", p);
            try
            {
                if (ddlProjectID.Items.Count > 1)
                    ddlProjectID.SelectedIndex = 1;
                else
                    ddlProjectID.SelectedIndex = 0;

            }
            catch { }
        }
        protected void btnHMSMpSearch_Click(object sender, EventArgs e)
        {
            OMSManPowerRequistion(objMpReq, 1);
        }

        protected void ddlWorkSite22_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillProjects1();
        }
    } 
}