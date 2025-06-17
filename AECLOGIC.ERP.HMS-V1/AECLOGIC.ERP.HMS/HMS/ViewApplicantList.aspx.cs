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
    public partial class ViewApplicants : AECLOGIC.ERP.COMMON.WebFormMaster
    {
        int mid = 0;
        bool viewall, Editable;
        string menuname;
        string menuid;
        AttendanceDAC objPosting = new AttendanceDAC();
        HRCommon objHrCommon = new HRCommon();
        protected override void OnInit(EventArgs e)
        {
            ModuleID = 1;
            base.OnInit(e);
        
            ViewApplilstPaging.FirstClick += new Paging.PageFirst(ViewApplilstPaging_FirstClick);
            ViewApplilstPaging.PreviousClick += new Paging.PagePrevious(ViewApplilstPaging_FirstClick);
            ViewApplilstPaging.NextClick += new Paging.PageNext(ViewApplilstPaging_FirstClick);
            ViewApplilstPaging.LastClick += new Paging.PageLast(ViewApplilstPaging_FirstClick);
            ViewApplilstPaging.ChangeClick += new Paging.PageChange(ViewApplilstPaging_FirstClick);
            ViewApplilstPaging.ShowRowsClick += new Paging.ShowRowsChange(ViewApplilstPaging_ShowRowsClick);
            ViewApplilstPaging.CurrentPage = 1;
        }
        void ViewApplilstPaging_ShowRowsClick(object sender, EventArgs e)
        {
            ViewApplilstPaging.CurrentPage = 1;
            BindPager();
        }
        void ViewApplilstPaging_FirstClick(object sender, EventArgs e)
        {
            BindPager();
        }
        void BindPager()
        {

            objHrCommon.PageSize = ViewApplilstPaging.CurrentPage;
            objHrCommon.CurrentPage = ViewApplilstPaging.ShowRows;
            ApplicantListDataBind(objHrCommon);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
         

            if (!IsPostBack)
            {
                GetParentMenuId();
    objHrCommon.PosID = 0;
                objHrCommon.AppStatus = Convert.ToInt32(0).ToString();
             
                BindPager();
                BindPositions();

                if (Request.QueryString != null && Request.QueryString.ToString() != string.Empty)
                {
                    int ApplicantID = Convert.ToInt32(Request.QueryString["id"].ToString());
                    BindApplicantdetails(ApplicantID);
                }
                int Position = Convert.ToInt32(ddlPosition.SelectedValue == "" ? "0" : ddlPosition.SelectedValue);
                objHrCommon.PosID = Convert.ToInt32(Position);
                objHrCommon.AppStatus = ddlApplicantStatus.SelectedValue.ToString();
             
            }
        }
        public void BindApplicantdetails(int ApplicantID)
        {
          
            DataSet ds = objPosting.GetApplicantdetails(ApplicantID);
            if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
            {
                string str = ds.Tables[0].Rows[0]["Resume"].ToString();
                if (str != "")
                    str = str.Split('/')[str.Split('/').Length - 1];
           
                ViewState["ResStr"] = ds.Tables[0].Rows[0]["Resume"].ToString();
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
                ViewState["Editable"] = Editable = (bool)ds.Tables[0].Rows[0]["Editable"];
                ViewState["ViewAll"] = (bool)ds.Tables[0].Rows[0]["ViewAll"];
                viewall = (bool)ViewState["ViewAll"];
                menuname = ds.Tables[0].Rows[0]["menuname"].ToString();
                menuid = MenuId.ToString();
                mid = Convert.ToInt32(ds.Tables[0].Rows[0]["MenuId"].ToString());
                gvApplicantList.Columns[5].Visible = (bool)ds.Tables[0].Rows[0]["Editable"];
                gvApplicantList.Columns[6].Visible = (bool)ds.Tables[0].Rows[0]["Editable"];
            }
            return MenuId;
        }
        public void BindPositions()
        {
            try
            {
              
                DataSet ds = (DataSet)objPosting.GetPositionList();
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

        private void ApplicantListDataBind(HRCommon objHrCommon)
        {
            objHrCommon.PageSize = ViewApplilstPaging.ShowRows;
            objHrCommon.CurrentPage = ViewApplilstPaging.CurrentPage;
            if (ddlPosition.SelectedIndex != 0)
            {
                if (ddlPosition.SelectedIndex == -1)
                {
                    objHrCommon.PosID = 0;
                }
                else
                    objHrCommon.PosID = Convert.ToInt32(ddlPosition.SelectedItem.Value);
            }
            else
                objHrCommon.PosID = 0;
            int? WSID = null;
           
                if (Convert.ToInt32(ddlWs_hid.Value == "" ? "0" : ddlWs_hid.Value) != 0)
                WSID = Convert.ToInt32(Convert.ToInt32(ddlWs_hid.Value == "" ? "0" : ddlWs_hid.Value));
            objHrCommon.AppStatus = ddlApplicantStatus.SelectedItem.Value;
          

            DataSet ds = AttendanceDAC.GetApplicantListByPaging(objHrCommon, WSID, Convert.ToInt32(Session["CompanyID"]));
          


            if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0)
            {
                gvApplicantList.DataSource = ds;
                gvApplicantList.DataBind();
                ViewApplilstPaging.Visible = true;

            }
            else
            {
                ViewApplilstPaging.Visible = false;
                gvApplicantList.DataSource = null;
                gvApplicantList.DataBind();
            }
            ViewApplilstPaging.Bind(objHrCommon.CurrentPage, objHrCommon.TotalPages, objHrCommon.NoofRecords, objHrCommon.PageSize);


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

        protected void ddlPosition_SelectedIndexChanged(object sender, EventArgs e)
        {
            ViewApplilstPaging.CurrentPage = 1;

            try
            {
                objHrCommon.PosID = Convert.ToInt32(ddlPosition.SelectedValue);
                objHrCommon.AppStatus = ddlApplicantStatus.SelectedValue.ToString();
                BindPager();
             
            }
            catch (Exception)
            {
                // throw;
            }
        }
        public string DocNavigateUrl(string Res, string AppID)
        {
            string ReturnVal = "";
            ReturnVal = "../HMS/Resumes/" + Convert.ToInt32(AppID) + Res;
            return ReturnVal;
        }
        protected void Showall_Click(object sender, EventArgs e)
        {
            ViewApplilstPaging.CurrentPage = 1;
            objHrCommon.PosID = Convert.ToInt32(ddlPosition.SelectedValue);
            objHrCommon.AppStatus = ddlApplicantStatus.SelectedValue.ToString();
            BindPager();
           

        }
        protected void gvApplicantList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{
            //    LinkButton lnkEdt = (LinkButton)e.Row.FindControl("lnkEdt");
            //    LinkButton lnkDel = (LinkButton)e.Row.FindControl("lnkDel");

            //    lnkEdt.Enabled = lnkDel.Enabled = Editable;
            //}
        }

        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionList(string prefixText, int count, string contextKey)
        {
            DataSet ds = AttendanceDAC.HR_GetWorkSite_By_Applicants_googlesearch(prefixText.Trim());
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



      
    }
}