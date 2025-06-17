using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using DataAccessLayer;
using AECLOGIC.HMS.BLL;
using AECLOGIC.ERP.COMMON;


namespace AECLOGIC.ERP.HMS
{
    public partial class EMPCustomReport : AECLOGIC.ERP.COMMON.WebFormMaster
    {
        bool viewall;
        int mid = 0;
        string menuname;
        string menuid;
        HRCommon objHrCommon = new HRCommon();

        protected override void OnInit(EventArgs e)
        {
            ModuleID = 1;
            base.OnInit(e);
            EMPCustomReportPaging.FirstClick += new Paging.PageFirst(EMPCustomReportPaging_FirstClick);
            EMPCustomReportPaging.PreviousClick += new Paging.PagePrevious(EMPCustomReportPaging_FirstClick);
            EMPCustomReportPaging.NextClick += new Paging.PageNext(EMPCustomReportPaging_FirstClick);
            EMPCustomReportPaging.LastClick += new Paging.PageLast(EMPCustomReportPaging_FirstClick);
            EMPCustomReportPaging.ChangeClick += new Paging.PageChange(EMPCustomReportPaging_FirstClick);
            EMPCustomReportPaging.ShowRowsClick += new Paging.ShowRowsChange(EMPCustomReportPaging_ShowRowsClick);
            EMPCustomReportPaging.CurrentPage = 1;
        }
        void EMPCustomReportPaging_ShowRowsClick(object sender, EventArgs e)
        {
            EMPCustomReportPaging.CurrentPage = 1;
            BindPager();
        }
        void EMPCustomReportPaging_FirstClick(object sender, EventArgs e)
        {
            BindPager();
        }
        public void BindPager()
        {

            objHrCommon.PageSize = EMPCustomReportPaging.CurrentPage;
            objHrCommon.CurrentPage = EMPCustomReportPaging.ShowRows;
            EmployeBind(objHrCommon);
        }

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                GetParentMenuId();
                EMPCustomReportPaging.Visible = false;

                btnExpExcel.Visible = false;
                
            }
        }
        public void EmployeBind(HRCommon objHrCommon)
        {

            try
            {

                objHrCommon.PageSize = EMPCustomReportPaging.ShowRows;
                objHrCommon.CurrentPage = EMPCustomReportPaging.CurrentPage;

                int? SiteID = null;
               
                if (Convert.ToInt32(ddlWS_hid.Value == "" ? "0" : ddlWS_hid.Value) != 0)
                    SiteID = Convert.ToInt32(Convert.ToInt32(ddlWS_hid.Value == "" ? "0" : ddlWS_hid.Value));

                  
               DataSet ds = AttendanceDAC.GetEmpDetailsForRpt(objHrCommon, SiteID, Convert.ToInt32(Session["CompanyID"]));
                if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0)
                {
                    gvReport.DataSource = ds;
                    gvReport.DataBind();
                    EMPCustomReportPaging.Visible = true;
                }
                else
                {
                    EMPCustomReportPaging.Visible = false;
                    gvReport.DataSource = null;
                    gvReport.DataBind();
                }
                EMPCustomReportPaging.Bind(objHrCommon.CurrentPage, objHrCommon.TotalPages, objHrCommon.NoofRecords, objHrCommon.PageSize);

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

              

         DataSet   ds = AttendanceDAC.GetAllowed(RoleId, ModuleId, URL);
            int MenuId = 0;
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                MenuId = Convert.ToInt32(ds.Tables[0].Rows[0]["Under"]);
                btnExpExcel.Enabled = (bool)ds.Tables[0].Rows[0]["Editable"];
            }
            return MenuId;
        }
       
        protected void btnSaveButton_Click(object sender, EventArgs e)
        {
            List<int> IndexList = new List<int>(); //(List<int>)Session["RepCols"];

            foreach (ListItem li in chkListFields.Items)
            {
                gvReport.Columns[Convert.ToInt32(li.Value)].Visible = li.Selected;
                if (!li.Selected)
                    IndexList.Add(Convert.ToInt32(li.Value));
            }
            Session["RepCols"] = IndexList;

            BindPager();

            btnExpExcel.Visible = true;

        }
        protected void btnExpExcel_Click(object sender, EventArgs e)
        {
            int? SiteID = null;
           
            if (Convert.ToInt32(ddlWS_hid.Value == "" ? "0" : ddlWS_hid.Value) != 0)
                SiteID = Convert.ToInt32(Convert.ToInt32(ddlWS_hid.Value == "" ? "0" : ddlWS_hid.Value));

              
           DataSet ds = AttendanceDAC.HR_EmpSalDetailsExpotToXLS(SiteID);
            ExportFileUtil.ExportToExcel(ds.Tables[0], "#EFEFEF", "#E6e6e6", "Report");
        }
       

        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionList(string prefixText, int count, string contextKey)
        {
            
            DataSet ds = AttendanceDAC.GetGoogleSearch_by_WorkSite(prefixText.Trim());
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