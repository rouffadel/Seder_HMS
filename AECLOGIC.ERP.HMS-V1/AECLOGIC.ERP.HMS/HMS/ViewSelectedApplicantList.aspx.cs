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
using System.Data.SqlClient;
using AECLOGIC.HMS.BLL;
using AECLOGIC.ERP.COMMON;

namespace AECLOGIC.ERP.HMS
{
    public partial class ViewSelectedApplicantList : AECLOGIC.ERP.COMMON.WebFormMaster
    {


        int mid = 0;
        bool viewall;
        string menuname;
        string menuid;
        static int SearchCompanyID;

        AttendanceDAC objPosting = new AttendanceDAC();
        HRCommon objHrCommon = new HRCommon();


        protected override void OnInit(EventArgs e)
        {
            ModuleID = 1;
            base.OnInit(e);
        
            ViewSelApplilstPaging.FirstClick += new Paging.PageFirst(ViewSelApplilstPaging_FirstClick);
            ViewSelApplilstPaging.PreviousClick += new Paging.PagePrevious(ViewSelApplilstPaging_FirstClick);
            ViewSelApplilstPaging.NextClick += new Paging.PageNext(ViewSelApplilstPaging_FirstClick);
            ViewSelApplilstPaging.LastClick += new Paging.PageLast(ViewSelApplilstPaging_FirstClick);
            ViewSelApplilstPaging.ChangeClick += new Paging.PageChange(ViewSelApplilstPaging_FirstClick);
            ViewSelApplilstPaging.ShowRowsClick += new Paging.ShowRowsChange(ViewSelApplilstPaging_ShowRowsClick);
            ViewSelApplilstPaging.CurrentPage = 1;
        }
        void ViewSelApplilstPaging_ShowRowsClick(object sender, EventArgs e)
        {
            ViewSelApplilstPaging.CurrentPage = 1;
            BindPager();
        }
        void ViewSelApplilstPaging_FirstClick(object sender, EventArgs e)
        {
            BindPager();
        }
        void BindPager()
        {

            objHrCommon.PageSize = ViewSelApplilstPaging.CurrentPage;
            objHrCommon.CurrentPage = ViewSelApplilstPaging.ShowRows;
            ApplicantListDataBind(objHrCommon);
        }
        protected void Page_Load(object sender, EventArgs e)
        {

            
            SearchCompanyID = Convert.ToInt32(Session["CompanyID"]);
          
            if (!IsPostBack)
            {
                GetParentMenuId();
                BindWorksite();
                BindPositions();
                BindPager();
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
                ViewState["ViewAll"] = (bool)ds.Tables[0].Rows[0]["ViewAll"];
                gvApplicantList.Columns[6].Visible = Convert.ToBoolean(ds.Tables[0].Rows[0]["Editable"].ToString());
                gvApplicantList.Columns[5].Visible = Convert.ToBoolean(ds.Tables[0].Rows[0]["Editable"].ToString());
                viewall = (bool)ViewState["ViewAll"];
                menuname = ds.Tables[0].Rows[0]["menuname"].ToString();
                menuid = MenuId.ToString();
                mid = Convert.ToInt32(ds.Tables[0].Rows[0]["MenuId"].ToString());
            }
            return MenuId;
        }

        protected void btnSearcWorksite(object sender, EventArgs e)
        {

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@Search", txtSearchWorksite.Text);
            param[1] = new SqlParameter("@CompanyID", Convert.ToInt32(Session["CompanyID"]));
           
            FIllObject.FillDropDown(ref ddlWs, "G_GET_WorkSitebyFilter", param);
            ListItem itmSelected = ddlWs.Items.FindByText(txtSearchWorksite.Text);
            if (itmSelected != null)
            {
                ddlWs.SelectedItem.Selected = false;
                itmSelected.Selected = true;
            }
        }

        protected void GetDept(object sender, EventArgs e)
        {

            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@Search", txtdept.Text);
            FIllObject.FillDropDown(ref ddlPosition, "HR_GetPositionListFilterGS", param);
            ListItem itmSelected = ddlPosition.Items.FindByText(txtdept.Text);
            if (itmSelected != null)
            {
                ddlPosition.SelectedItem.Selected = false;
                itmSelected.Selected = true;
            }
            if(itmSelected != null)
            { ddlPosition.SelectedIndex = 1; }
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

        public void ApplicantListDataBind(HRCommon objHrCommon)
        {
            objHrCommon.AppStatus = "3";

            objHrCommon.PosID = 0;
            if (ddlPosition.SelectedValue != "0" && ddlPosition.SelectedValue != "")
                objHrCommon.PosID = Convert.ToInt32(ddlPosition.SelectedValue);
            objHrCommon.PageSize = ViewSelApplilstPaging.ShowRows;
            objHrCommon.CurrentPage = ViewSelApplilstPaging.CurrentPage;
            int? WSID = null;
            if (ddlWs.SelectedValue != "0")
                WSID = Convert.ToInt32(ddlWs.SelectedValue);

            DataSet  ds = AttendanceDAC.GetApplicantListByPaging(objHrCommon, WSID, Convert.ToInt32(Session["CompanyID"]));
            if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0)
            {
                ds.Tables[0].Columns.Add("FPage", typeof(int));
                foreach (DataRow row in ds.Tables[0].Rows)
                {

                    row["FPage"] = 3;   
                }



                gvApplicantList.DataSource = ds;
                gvApplicantList.DataBind();
                ViewSelApplilstPaging.Visible = true;
            }
            else
            {
                ViewSelApplilstPaging.Visible = false;
               
                gvApplicantList.DataBind();
               
            }
            ViewSelApplilstPaging.Bind(objHrCommon.CurrentPage, objHrCommon.TotalPages, objHrCommon.NoofRecords, objHrCommon.PageSize);
        }
        public void BindWorksite()
        {
            try
            {

                DataSet ds = objPosting.GetWorkSiteByEmpID(0, Convert.ToInt32(Session["CompanyID"]), Convert.ToInt32(Session["RoleId"]));
                if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0)
                {

                    ddlWs.DataSource = ds.Tables[0];
                    ddlWs.DataTextField = "Site_Name";
                    ddlWs.DataValueField = "Site_ID";
                    ddlWs.DataBind();
                }
                ddlWs.Items.Insert(0, new ListItem("---ALL---", "0"));

            }
            catch (Exception e)
            {
                throw e;
            }

        }
        public void BindPositions()
        {
            try
            {

                DataSet ds = (DataSet)objPosting.GetPositionList_ViewApplicantList();
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
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionList(string prefixText, int count, string contextKey)
        {
            DataSet ds = AttendanceDAC.GetGoogleABCSearchWorkSite(prefixText, SearchCompanyID);
            return ConvertStingArray(ds);// txtItems.ToArray();
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionListDep(string prefixText, int count, string contextKey)
        {
            DataSet ds = AttendanceDAC.GetGoogleABCSearchPosition(prefixText);
            return ConvertStingArray(ds);// txtItems.ToArray();
        }
        public static string[] ConvertStingArray(DataSet ds)
        {
            string[] rtval = Array.ConvertAll(ds.Tables[0].Select(), new Converter<DataRow, string>(DataRowToDouble));
            return rtval;
        }
        public static string DataRowToDouble(DataRow dr)
        {
            return dr["Name"].ToString();
        }
        
        protected void Showall_Click(object sender, EventArgs e)
        {
            ViewSelApplilstPaging.CurrentPage = 1;
            BindPager();

        }
    }
}