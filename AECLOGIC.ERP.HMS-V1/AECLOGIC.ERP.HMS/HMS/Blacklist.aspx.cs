using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using AECLOGIC.HMS.BLL;
using AECLOGIC.ERP.COMMON;
using System.Data.SqlClient;
using Aeclogic.Common.DAL;


namespace AECLOGIC.ERP.HMS.HMS
{
    public partial class Blacklist : AECLOGIC.ERP.COMMON.WebFormMaster
    {
       
        #region Variables
        int mid = 0;
        bool viewall, Editable;
        static int Siteid;
        static int SearchCompanyID;
        static int EDeptid = 0;
        static int Empid;
        static int CompanyID = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["CompanyID"]);
        string menuname;
        static int WSiteid;
        string menuid;
        HRCommon objHrCommon = new HRCommon();
        //private GridSort objSort;
        // DataSet dsEvaluation;
        //int CompanyID = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["CompanyID"]);

        #endregion Variables

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
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                string id = Session["UserId"].ToString();
            }
            catch
            {
                Response.Redirect("Login.aspx");
            }

            topmenu.MenuId = GetParentMenuId();
            topmenu.ModuleId = ModuleID; ;
            topmenu.RoleID = Convert.ToInt32(Session["RoleId"]);
            topmenu.SelectedMenu = Convert.ToInt32(mid);
            topmenu.DataBind();
            Session["menuname"] = menuname;
            Session["menuid"] = menuid;
            Session["MId"] = mid;
            if (!IsPostBack)
            {
                objHrCommon.CurrentPage = EmpListPaging.CurrentPage;
                objHrCommon.PageSize = EmpListPaging.ShowRows;
                EmployeBind(objHrCommon);
                tblvew.Visible = true;
                //DataSet dss = new DataSet();
                DataSet dss = SQLDBUtil.ExecuteDataset("HR_Get_FinancialYearList");
                DDlFinyr.DataSource = dss.Tables[0];
                DDlFinyr.DataValueField = "FinYearId";
                DDlFinyr.DataTextField = "Name";
                DDlFinyr.DataBind();
            }
             
        }

        void EmpListPaging_ShowRowsClick(object sender, EventArgs e)
        {
            EmpListPaging.CurrentPage = 1;
            // GetEmpCriteria();
        }
        void EmpListPaging_FirstClick(object sender, EventArgs e)
        {
            //  GetEmpCriteria();
        }

        public int GetParentMenuId()
        {
            if (Session["UserId"] == null)
            {
                Response.Redirect("Logon.aspx");
            }
            string URL = Request.Url.Segments[Request.Url.Segments.Length - 1] + Request.Url.Query;
            int RoleId = Convert.ToInt32(Session["RoleId"]);
            int ModuleId = ModuleID; ;

            //DataSet ds = new DataSet();

            DataSet ds = AttendanceDAC.GetAllowed(RoleId, ModuleId, URL);
            int MenuId = 0;
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                MenuId = Convert.ToInt32(ds.Tables[0].Rows[0]["Under"]);
                ViewState["Editable"] = Editable = (bool)ds.Tables[0].Rows[0]["Editable"];
                //gdvWS.Columns[2].Visible = Convert.ToBoolean(ds.Tables[0].Rows[0]["Editable"].ToString());
                ViewState["ViewAll"] = (bool)ds.Tables[0].Rows[0]["ViewAll"];
                // lnkState.Visible = Convert.ToBoolean(ds.Tables[0].Rows[0]["Editable"]);
               // btnSubmit.Enabled = Convert.ToBoolean(ds.Tables[0].Rows[0]["Editable"]);
                viewall = (bool)ViewState["ViewAll"];
                menuname = ds.Tables[0].Rows[0]["menuname"].ToString();
                menuid = MenuId.ToString();
                mid = Convert.ToInt32(ds.Tables[0].Rows[0]["MenuId"]);
                //btnSubmit.Enabled = Editable;
            }
            return MenuId;
        }
        public static DataSet GetAllowed(int RoleId, int ModuleId, string URL)
        {
            //DataSet ds = new DataSet();
            SqlParameter[] objParam = new SqlParameter[3];
            objParam[0] = new SqlParameter("@RoleId", RoleId);
            objParam[1] = new SqlParameter("@ModuleId", ModuleId);
            objParam[2] = new SqlParameter("@URL", URL);
            DataSet ds = SQLDBUtil.ExecuteDataset("CP_GetPageAccess", objParam);
            return ds;
        }

        protected void btnSearch_Click(object sender, EventArgs e)
       {
            EmployeBind(objHrCommon);
        }
        void EmployeBind(HRCommon objHrCommon)
        {

            try
            {
                objHrCommon.PageSize = EmpListPaging.ShowRows;
                objHrCommon.CurrentPage = EmpListPaging.CurrentPage;
                int? EmpID = null;
                int? Dept = null;
                int? WS = null;
                if (textsearchemp.Text != "")
                {
                    //  EmpID = Convert.ToInt32(textsearchemp.Text.Substring(0, 4));
                    EmpID = Convert.ToInt32(textsearchemp_hid.Value == "" ? "0" : textsearchemp_hid.Value);
                }
                if (TxtDept.Text.Trim() != "")
                {
                    Dept = Convert.ToInt32(TxtDept_hid.Value == "" ? "0" : TxtDept_hid.Value);

                }

                if (Txtwrk.Text.Trim() != "")
                {
                    WS = Convert.ToInt32(Txtwrk_hid.Value == "" ? "0" : Txtwrk_hid.Value);
                }
                //DataSet ds = new DataSet();
                // DataSet ds = AttendanceDAC.HMS_GetEmpClearencePaging(objHrCommon, txtname.Text);
                DataSet ds = AttendanceDAC.HR_GetemployeeBlack_EMP(objHrCommon, EmpID, WS, Dept, Convert.ToInt32(Session["CompanyID"]));
                if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0)
                {
                    gvEmPCriteria2.DataSource = ds;
                    gvEmPCriteria2.DataBind();
                    finyear.Visible = false;
                   
                }
                else
                {
                    gvEmPCriteria2.DataSource = null;
                    gvEmPCriteria2.DataBind();
                    finyear.Visible = false;
                  
                }
                EmpListPaging.Bind(objHrCommon.CurrentPage, objHrCommon.TotalPages, objHrCommon.NoofRecords, objHrCommon.PageSize);

            }
            catch (Exception e)
            {
                throw e;
            }
        }
        protected void GetDept(object sender, EventArgs e)
        {

           
        }

        protected void gvEmPCriteria2_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Name")
            {
                try
                {

                    //int index = int.Parse(e.CommandArgument.ToString());
                    //GridViewRow row = GridView1.Rows[index];

                    GridViewRow gvr = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                    Label empname = (Label)gvr.FindControl("lblEmpName");
                    lblEmp.Text = empname.Text;

                    //LinkButton EmpName = (LinkButton)gvr.FindControl("EmpName");
                    //ViewState["Name"] = EmpName.Text;
                    //  ViewState["FinancialYear"] = CtrID.Text;
                    // tblvew.Visible = true;
                    //tblAdd.Visible = false;
                    int emp = Convert.ToInt32(e.CommandArgument.ToString());
                    objHrCommon.CurrentPage = EmpListPaging.CurrentPage;
                    objHrCommon.PageSize = EmpListPaging.ShowRows;
                    ViewState["EmpID"] = e.CommandArgument.ToString();
                    if (textsearchemp.Text != String.Empty)
                    {
                       //   emp = textsearchemp.Text.Substring(0, 4);   
                    }

                    //CreterialDetails();

                    gvEmPCriteria2.Visible = false;
                    gvEmpcrdet.Visible = true;
                    //Hide.Visible = true;
                    EmpListPaging.Visible = false;
                    EdtViewAccordion.Visible = false;
                    finyear.Visible = true;
                    //btnAddCret.Visible = false;

                    //EmplistDetailsPaging.Visible = true;
                    // EmplistDetailsPaging.Bind(objHrCommon.CurrentPage, objHrCommon.TotalPages, objHrCommon.NoofRecords, objHrCommon.PageSize);
                }
                catch { }
           }
        }

        protected void btnEmpcrsearch_Click(object sender, EventArgs e)
        {
            GetEmpCriteria();
        }
        public void GetEmpCriteria()
        {
            try
            {
                // tblvew.Visible = true;
                //tblAdd.Visible = false;
               // string emp = "";
                objHrCommon.CurrentPage = EmpListPaging.CurrentPage;
                objHrCommon.PageSize = EmpListPaging.ShowRows;
                if (textsearchemp.Text != String.Empty)
                 ViewState["EmpID"] = textsearchemp.Text.Substring(0, 4);
                //DataSet ds = new DataSet();

                SqlParameter[] parms = new SqlParameter[7];
                parms[0] = new SqlParameter("@CurrentPage", objHrCommon.CurrentPage);
                parms[1] = new SqlParameter("@PageSize", objHrCommon.PageSize);
                parms[2] = new SqlParameter("@NoofRecords", SqlDbType.Int);
                parms[2].Direction = ParameterDirection.Output;
                parms[3] = new SqlParameter();
                parms[3].Direction = ParameterDirection.ReturnValue;
                string str=ViewState["EmpID"].ToString();
                if (str != String.Empty)
                    parms[4] = new SqlParameter("@EmpID", ViewState["EmpID"]);
                else
                    parms[4] = new SqlParameter("@EmpID", DBNull.Value);

                if (DDlFinyr.Text != String.Empty)
                    parms[5] = new SqlParameter("@FinancialYear", Convert.ToInt32(DDlFinyr.SelectedItem.Text));
                else
                    parms[5] = new SqlParameter("@FinancialYear", DBNull.Value);
                if (Convert.ToInt32(ddlSMonth.SelectedValue) > 0)
                    parms[6] = new SqlParameter("@Month", Convert.ToInt32(ddlSMonth.SelectedValue));
                else
                    parms[6] = new SqlParameter("@Month", DBNull.Value);
                DataSet ds = SQLDBUtil.ExecuteDataset("HMS_EmpCriteriablack", parms);
                objHrCommon.NoofRecords = (int)parms[2].Value;
                objHrCommon.TotalPages = (int)parms[3].Value;
                gvEmpcrdet.Visible = true;
                gvEmpcrdet.DataSource = null;
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    gvEmpcrdet.DataSource = ds;
                    gvEmpcrdet.DataBind();
                    GridView1.Visible = false;
                    gvEmPCriteria2.Visible = false;
                    // HideGrid.Visible = false;
                    if (Convert.ToInt32(ddlSMonth.SelectedValue) > 0)
                    {
                        //btnAddCret.Visible = false;
                    }

                    else
                    {
                        gvEmpcrdet.DataBind();

                        //btnAddCret.Visible = true;
                    }

                    EmpListPaging.Visible = true;
              EmpListPaging.Bind(objHrCommon.CurrentPage, objHrCommon.TotalPages, objHrCommon.NoofRecords, objHrCommon.PageSize);
                }
                else
                { gvEmpcrdet.DataBind();
                EmpListPaging.Bind(objHrCommon.CurrentPage, objHrCommon.TotalPages, objHrCommon.NoofRecords, objHrCommon.PageSize);
                }
            }
            catch { }
           

            
        }

        DataSet dsk = new DataSet();

        protected void gvEmpcrdet_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Name")
            {
                try
                {


                    GridViewRow gvr = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                    Label empname = (Label)gvr.FindControl("lblEmpName");
                    Label lblYear = (Label)gvr.FindControl("lblYear");
                    Label lblMonth = (Label)gvr.FindControl("lblMonth");
                    lblEmp.Text = empname.Text;

                    int emp = Convert.ToInt32(e.CommandArgument.ToString());
                    objHrCommon.CurrentPage = EmpListPaging.CurrentPage;
                    objHrCommon.PageSize = EmpListPaging.ShowRows;
                    ViewState["EmpID"] = e.CommandArgument.ToString();
                    if (textsearchemp.Text != String.Empty)
                    {
                         //emp = textsearchemp.Text.Substring(0, 4);   
                    }

                    SqlParameter[] parms = new SqlParameter[7];
                    parms[0] = new SqlParameter("@CurrentPage", 1);
                    parms[1] = new SqlParameter("@PageSize", 100);
                    parms[2] = new SqlParameter("@NoofRecords", SqlDbType.Int);
                    parms[2].Direction = ParameterDirection.Output;
                    parms[3] = new SqlParameter();
                    parms[3].Direction = ParameterDirection.ReturnValue;
                    if (ViewState["textsearchemp"] != String.Empty)
                        parms[4] = new SqlParameter("@EmpID", ViewState["EmpID"]);
                    else
                        parms[4] = new SqlParameter("@EmpID", DBNull.Value);


                    parms[5] = new SqlParameter("@FinancialYear", lblYear.Text);
                    parms[6] = new SqlParameter("@Month", lblMonth.Text);
                    dsk = SQLDBUtil.ExecuteDataset("HMS_Get_EMPCreteriaDetails", parms);
                    objHrCommon.NoofRecords = (int)parms[2].Value;
                    objHrCommon.TotalPages = (int)parms[3].Value;
                    GridView1.Visible = true;
                    GridView1.DataSource = dsk;
                    GridView1.DataBind();
                    //total = Convert.ToInt32(dsk.Tables[0].Rows[0]["Total"].ToString());
                    //HideGrid.Visible = true;
                    gvEmpcrdet.Visible = false;
                    GridView1.Visible = true;

                    gvEmPCriteria2.Visible = false;
                    gvEmpcrdet.Visible = false;
                    //Hide.Visible = true;
                    EmpListPaging.Visible = false;
                    EdtViewAccordion.Visible = false;
                    finyear.Visible = true;
                    EmpListPaging.Visible = false;
                }
                catch { }

            }
        }
        

        protected void Hide_Click(object sender, EventArgs e)
        {
        
        }
        //fror worksite
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletiondeptWorkList(string prefixText, int count, string contextKey)
        {
            DataSet ds = AttendanceDAC.GetGoogleblackName(prefixText);
            DataTable dt = ds.Tables[0];
            List<string> items = new List<string>(count);
            var rtval = new Dictionary<string, string>();
            foreach (DataRow row in dt.Rows)
            {
                string str = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(row["Name"].ToString(), row["ID"].ToString());
                items.Add(str);
            }

           // return ConvertStingArray(AttendanceDAC.GetGoogleblackName(prefixText));// txtItems.ToArray();
            return items.ToArray();

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

        //for department
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletiondeptList(string prefixText, int count, string contextKey)
        {
            DataSet ds = AttendanceDAC.GetSearchDeptBlack(prefixText, CompanyID, Siteid);
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
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionListemp(string prefixText, int count, string contextKey)
        {
            DataSet ds = AttendanceDAC.HR_GoogleSearchEmpBlack(prefixText, Empid);
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

      
        float total = 0;
        float wTotal = 0; int pTotal = 0;
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    Label lblperf = (Label)e.Row.FindControl("lblperf");
                    Label wtgprsnt = (Label)e.Row.FindControl("wtgprsnt");
                    if (lblperf.Text != string.Empty)
                    {
                        float qty = float.Parse(lblperf.Text);
                        total = total + qty;
                        float wtot = float.Parse(wtgprsnt.Text);
                        wTotal = wTotal + wtot;
                    }
                   
                }
                if (e.Row.RowType == DataControlRowType.Footer)
                {
                    ViewState["Total"] = total.ToString();
                    Label lblTotalqty = (Label)e.Row.FindControl("lblTotalqty");
                    Label lbltotalwqty = (Label)e.Row.FindControl("lbltotalwqty");
                    lblTotalqty.Text = total.ToString();
                    lbltotalwqty.Text = wTotal.ToString();
                }
            }
            catch (Exception ex)
            {

                throw;
            }

        }
        }

}

     

    
        
    

     