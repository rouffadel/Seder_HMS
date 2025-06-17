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
    public partial class OfferLetterList : AECLOGIC.ERP.COMMON.WebFormMaster
    {

        int mid = 0;
        bool viewall;
        string menuname;
        string menuid;

        AttendanceDAC objApp = new AttendanceDAC();
        HRCommon objHrCommon = new HRCommon();
        protected override void OnInit(EventArgs e)
        {
            ModuleID = 1;
            base.OnInit(e);
        
            ViewOfferlilstPaging.FirstClick += new Paging.PageFirst(ViewApplilstPaging_FirstClick);
            ViewOfferlilstPaging.PreviousClick += new Paging.PagePrevious(ViewApplilstPaging_FirstClick);
            ViewOfferlilstPaging.NextClick += new Paging.PageNext(ViewApplilstPaging_FirstClick);
            ViewOfferlilstPaging.LastClick += new Paging.PageLast(ViewApplilstPaging_FirstClick);
            ViewOfferlilstPaging.ChangeClick += new Paging.PageChange(ViewApplilstPaging_FirstClick);
            ViewOfferlilstPaging.ShowRowsClick += new Paging.ShowRowsChange(ViewApplilstPaging_ShowRowsClick);
            ViewOfferlilstPaging.CurrentPage = 1;
        }
        void ViewApplilstPaging_ShowRowsClick(object sender, EventArgs e)
        {
            ViewOfferlilstPaging.CurrentPage = 1;
            BindPager();
        }
        void ViewApplilstPaging_FirstClick(object sender, EventArgs e)
        {
            BindPager();
        }
        void BindPager()
        {
            objHrCommon.PageSize = ViewOfferlilstPaging.CurrentPage;
            objHrCommon.CurrentPage = ViewOfferlilstPaging.ShowRows;
            ApplicantOfferList(objHrCommon);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
           
            if (!IsPostBack)
            {
                GetParentMenuId();
                BindPositions();
                BindPager();
            }
        }
        
        public void BindPositions()
        {
            try
            {


                DataSet ds = (DataSet)objApp.GetPositionList_OfferApplicantList();
                if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ddlPosition.DataValueField = "PosID";
                    ddlPosition.DataTextField = "Position";
                    ddlPosition.DataSource = ds;
                    ddlPosition.DataBind();
                    ddlPosition.Items.Insert(0, new ListItem("--ALL--", "0"));

                }

            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public int GetParentMenuId()
        {
            string URL = Request.Url.Segments[Request.Url.Segments.Length - 1] + Request.Url.Query;
            int RoleId = Convert.ToInt32(Session["RoleId"].ToString());
            int ModuleId = ModuleID;;

             

            DataSet ds = AttendanceDAC.GetAllowed(RoleId, ModuleId, URL);
            int MenuId = 0;
            if (ds.Tables.Count > 0)
            {
                MenuId = Convert.ToInt32(ds.Tables[0].Rows[0]["Under"]);
                gvApplicantList.Columns[4].Visible = (bool)ds.Tables[0].Rows[0]["Editable"];
                gvApplicantList.Columns[6].Visible = (bool)ds.Tables[0].Rows[0]["Editable"];
                gvApplicantList.Columns[7].Visible = (bool)ds.Tables[0].Rows[0]["Editable"];
            }
            return MenuId;
        }

        private void ApplicantOfferList(HRCommon objHrCommon)
        {
            objHrCommon.PageSize = ViewOfferlilstPaging.ShowRows;
            objHrCommon.CurrentPage = ViewOfferlilstPaging.CurrentPage;
            int? WSID = null;
            if (Convert.ToInt32(ddlWs_hid.Value == "" ? "0" : ddlWs_hid.Value) != 0)
                WSID = Convert.ToInt32(Convert.ToInt32(ddlWs_hid.Value == "" ? "0" : ddlWs_hid.Value));
            int? POSID = null;
            if (ddlPosition.SelectedValue != "0" && ddlPosition.SelectedValue != "")
                POSID = Convert.ToInt32(ddlPosition.SelectedValue);

            DataSet ds = AttendanceDAC.ApplicantOfferListByPaging(objHrCommon, WSID, POSID, Convert.ToInt32(Session["CompanyID"]));
            if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0)
            {
                gvApplicantList.DataSource = ds;
                gvApplicantList.DataBind();
                ViewOfferlilstPaging.Visible = true;

            }
            else
            {
                ViewOfferlilstPaging.Visible = false;
                gvApplicantList.DataSource = null;
                gvApplicantList.DataBind();
            }
            ViewOfferlilstPaging.Bind(objHrCommon.CurrentPage, objHrCommon.TotalPages, objHrCommon.NoofRecords, objHrCommon.PageSize);
        }

       


        protected string FormatInput(object EntryType)
        {
            string retValue = "";
            string input = EntryType.ToString();
            if (input == "1")
            {
                retValue = "Globally";
            }
            if (input == "2")
            {
                retValue = "Locally";
            }
            return retValue;
        }
        public string ShowLetter(object AppID)
        {
            return "javascript:return window.open('OfferLetterPreview.aspx?id=" + AppID + "', '_blank')";
        }
        protected void gvApplicantList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Del")
            {
                int AppID = Convert.ToInt32(e.CommandArgument);
                objHrCommon.AppID = AppID;
                objApp.DeleteApplicant(objHrCommon);
                BindPager();

            }
            if (e.CommandName == "Accept")
            {
                int AppID = Convert.ToInt32(e.CommandArgument);
                int Status = 5;
                objApp.UpdateAppIDStaus(AppID, Status);
                Response.Redirect("ViewAcceptedOfferList.aspx");
            }
            if (e.CommandName == "Reject")
            {
                int AppID = Convert.ToInt32(e.CommandArgument);
                int Status = 0;
                objApp.UpdateAppIDStaus(AppID, Status);
                BindPager();

            }
        }
        protected void gvApplicantList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            foreach (GridViewRow gvr in gvApplicantList.Rows)
            {
                LinkButton lnk1 = (LinkButton)gvr.Cells[9].FindControl("lnkAccept");
                lnk1.Enabled = Convert.ToBoolean(ViewState["Editable"]);
                LinkButton lnk2 = (LinkButton)gvr.Cells[10].FindControl("lnkReject");
                lnk2.Enabled = Convert.ToBoolean(ViewState["Editable"]);
            }

            try
            {
                Control id = (Control)e.Row.Cells[5].Controls[0];
                string Appid = Convert.ToString(gvApplicantList.DataKeys[e.Row.RowIndex].Value);
                HyperLink lnk = (HyperLink)id;
                lnk.Attributes.Add("onclick", "javascript:return showpreview(" + Appid + ");");
            }
            catch { }
        }
        protected void Showall_Click(object sender, EventArgs e)
        {
            ViewOfferlilstPaging.CurrentPage = 1;
            BindPager();
        }
        protected void btnExpToXL_Click(object sender, EventArgs e)
        {
            int? WSID = null;
            if (Convert.ToInt32(ddlWs_hid.Value == "" ? "0" : ddlWs_hid.Value) != 0)
                WSID = Convert.ToInt32(Convert.ToInt32(ddlWs_hid.Value == "" ? "0" : ddlWs_hid.Value));
            int? PosID = null;
            if (ddlPosition.SelectedValue != "0")
                PosID = Convert.ToInt32(ddlPosition.SelectedValue);
            DataSet ds = AttendanceDAC.ApplicantOfferListExportToXL(WSID, PosID);
            ExportFileUtil.ExportToExcel(ds.Tables[0], "", "#EFEFEF", "#E6e6e6", "OfferLetterList");
        }

        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionList(string prefixText, int count, string contextKey)
        {
            DataSet ds = AttendanceDAC.HR_GetWorkSite_By_OfferList_googlesearch(prefixText.Trim());
            DataTable dt = ds.Tables[0];
            List<string> items = new List<string>(count);
            var rtval = new Dictionary<string, string>();
            foreach (DataRow row in dt.Rows)
            {
                string str = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(row["Name"].ToString(), row["ID"].ToString());
                items.Add(str);
            }

            return items.ToArray();  

        }



      
       
    }
}