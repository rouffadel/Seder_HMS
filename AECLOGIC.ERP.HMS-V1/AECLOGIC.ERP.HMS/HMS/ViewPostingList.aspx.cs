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
using System.Drawing;
using AECLOGIC.ERP.COMMON;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace AECLOGIC.ERP.HMS
{
    public partial class ViewPostings : AECLOGIC.ERP.COMMON.WebFormMaster
    {

        int mid = 0;
        bool viewall;
        string menuname;
        string menuid;
        static bool status1=true;

        AttendanceDAC objPosting = new AttendanceDAC();
        HRCommon objHrCommon = new HRCommon();

         protected override void OnInit(EventArgs e)
        {
            ModuleID = 1;
            base.OnInit(e);
        
            ViewPostinglstPaging.FirstClick += new Paging.PageFirst(ViewPostinglstPaging_FirstClick);
            ViewPostinglstPaging.PreviousClick += new Paging.PagePrevious(ViewPostinglstPaging_FirstClick);
            ViewPostinglstPaging.NextClick += new Paging.PageNext(ViewPostinglstPaging_FirstClick);
            ViewPostinglstPaging.LastClick += new Paging.PageLast(ViewPostinglstPaging_FirstClick);
            ViewPostinglstPaging.ChangeClick += new Paging.PageChange(ViewPostinglstPaging_FirstClick);
            ViewPostinglstPaging.ShowRowsClick += new Paging.ShowRowsChange(ViewPostinglstPaging_ShowRowsClick);
            ViewPostinglstPaging.CurrentPage = 1;
        }
        void ViewPostinglstPaging_ShowRowsClick(object sender, EventArgs e)
        {
            ViewPostinglstPaging.CurrentPage = 1;
            BindPager();
        }
        void ViewPostinglstPaging_FirstClick(object sender, EventArgs e)
        {
            BindPager();
        }
        void BindPager()
        {

            objHrCommon.PageSize = ViewPostinglstPaging.CurrentPage;
            objHrCommon.CurrentPage = ViewPostinglstPaging.ShowRows;
            BindJobOpenings(objHrCommon);
        }
        protected void Page_Load(object sender, EventArgs e)
        {

            
            if (!IsPostBack)
            {
                GetParentMenuId();
                gvPosting.Visible = true;
                BindPager();

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
                ViewState["Editable"] = (bool)ds.Tables[0].Rows[0]["Editable"];
                ViewState["ViewAll"] = (bool)ds.Tables[0].Rows[0]["ViewAll"];
                viewall = (bool)ViewState["ViewAll"];
                menuname = ds.Tables[0].Rows[0]["menuname"].ToString();
                menuid = MenuId.ToString();
                mid = Convert.ToInt32(ds.Tables[0].Rows[0]["MenuId"].ToString());
                gvPosting.Columns[9].Visible = (bool)ds.Tables[0].Rows[0]["Editable"];
            }
            return MenuId;
        }


        public void BindJobOpenings(HRCommon objHrCommon)
        {
            try
            {
                objHrCommon.PageSize = ViewPostinglstPaging.ShowRows;
                objHrCommon.CurrentPage = ViewPostinglstPaging.CurrentPage;
                objHrCommon.AppStatus = null;
                int Status = 1;
                if (rblstStatus.SelectedItem.Text == "Closed Positions")
                {
                    Status = 0;
                }
                DateTime? dt = null;
                if (txtDate.Text != "")
                    dt = CODEUtility.ConvertToDate(txtDate.Text, DateFormat.DayMonthYear);
                  
                DataSet ds = AttendanceDAC.GetJobOpeningsListByStatus(objHrCommon, Status, Convert.ToInt32(Convert.ToInt32(ddlWs_hid.Value == "" ? "0" : ddlWs_hid.Value)), txtExp.Text, txtposition.Text, dt, Convert.ToInt32(Session["CompanyID"]));
                if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0)
                {
                    gvPosting.DataSource = ds;
                    gvPosting.DataBind();
                    ViewPostinglstPaging.Visible = true;
                }
                else
                {
                    ViewPostinglstPaging.Visible = false;
                    gvPosting.DataSource = null;
                    gvPosting.DataBind();
                }
                ViewPostinglstPaging.Bind(objHrCommon.CurrentPage, objHrCommon.TotalPages, objHrCommon.NoofRecords, objHrCommon.PageSize);

            }
            catch (Exception e)
            {
                throw e;
            }



        }


        

        
        

        protected void rblstStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindPager();
            status1 = rblstStatus.SelectedIndex == 0 ? true : false;
        }
        protected void gvPosting_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "View")
            {
                string PosID = e.CommandArgument.ToString();
                LinkButton lnk = new LinkButton();

                lnk.Attributes.Add("onclick", "javascript:return AddNewItem();");
            }
        }
       
        protected void gvPosting_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    LinkButton lnkbtn = (LinkButton)e.Row.Cells[9].Controls[1];

                    string PosID = Convert.ToString(gvPosting.DataKeys[e.Row.RowIndex].Value);
                    lnkbtn.Attributes.Add("onclick", "javascript:return AddNewItem();");
                }
            }
            catch { }

        }

        public bool IsEditable(string DocType)
        {
            if (DocType.Trim() == "Digital")
                return true;
            else
                return false;

        }

        public string DocNavigateUrl(string PosID)
        {
            string ReturnVal = "";
            ReturnVal = String.Format("EditPostingDetails.aspx?PosID={0}", PosID);
            return ReturnVal;
        }

        
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            ViewPostinglstPaging.CurrentPage = 1;
            BindPager();
        }

        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionList(string prefixText, int count, string contextKey)
        {
            int StatusSearch;
            if (status1 == true)
                StatusSearch = 1;
            else
                StatusSearch = 0;


            DataSet ds = AttendanceDAC.GetGoogleSearch_by_Worksite_by_position(prefixText.Trim(), StatusSearch);
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