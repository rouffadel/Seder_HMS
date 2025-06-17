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
using System.Data.SqlClient;
using Aeclogic.Common.DAL;
using System.Collections.Generic;


namespace AECLOGIC.ERP.HMS
{
    public partial class EmployeeEvaluation : AECLOGIC.ERP.COMMON.WebFormMaster
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

                if (!IsPostBack)
                {
                    GetParentMenuId();
                    if (Request.QueryString.Count > 0)
                    {
                        //GetEmpCriteria();
                        objHrCommon.CurrentPage = EmpListPaging.CurrentPage;
                        objHrCommon.PageSize = EmpListPaging.ShowRows;
                        EmployeBind(objHrCommon);
                        tblAdd.Visible = false;
                        tblvew.Visible = true;

                        int key = Convert.ToInt32(Request.QueryString["key"]);
                        if (key == 1)
                        {
                            gvEmPCriteria.Columns[6].Visible = false;
                            gvEmPCriteria.Columns[7].Visible = true;
                           
                        }
                        else if (key == 2)
                        {
                            gvEmPCriteria.Columns[6].Visible = true;
                            gvEmPCriteria.Columns[7].Visible = false;
                          
                        }
                    }
                    else
                    {
                        tblAdd.Visible = true;
                        tblvew.Visible = false;

                    }
                      
                  DataSet  ds = SQLDBUtil.ExecuteDataset("HMS_GetEMPID");
                    ddlEmpid.DataSource = ds.Tables[0];
                    ddlEmpid.DataValueField = "ID";
                    ddlEmpid.DataTextField = "name";

                    ddlEmpid.DataBind();
                    ddlEmpid.Items.Insert(0, new ListItem("---ALL---", "0", true));

                    DataSet  dss = SQLDBUtil.ExecuteDataset("HR_Get_FinancialYearList");
                    ddlFinyear.DataSource = dss.Tables[0];
                    ddlFinyear.DataValueField = "FinYearId";
                    ddlFinyear.DataTextField = "Name";
                    ddlFinyear.DataBind();
                    ddlFinyear.Items.Insert(0, new ListItem("--ALL--", "2", true));
                    DDlFinyr.DataSource = dss.Tables[0];
                    DDlFinyr.DataValueField = "FinYearId";
                    DDlFinyr.DataTextField = "Name";
                    DDlFinyr.DataBind();
                    DDlFinyr.Items.Insert(0, new ListItem("--ALL--", "0", true));
                    Hide.Visible = false;
                    HideGrid.Visible = false;
                    finyear.Visible = false;
                }


                int EmpId =  Convert.ToInt32(Session["UserId"]);
            }

            catch (Exception ex)
            {
                AlertMsg.MsgBox(Page, ex.Message.ToString(),AlertMsg.MessageType.Error);
            }
        }

        void EmplistDetailsPaging_ShowRowsClick(object sender, EventArgs e)
        {
            //EmplistDetailsPaging.CurrentPage = 1;
            //GetEmpCriteria();
        }
        void EmplistDetailsPaging_FirstClick(object sender, EventArgs e)
        {
            //GetEmpCriteria();

        }

        void EmpListPaging_ShowRowsClick(object sender, EventArgs e)
        {
            EmpListPaging.CurrentPage = 1;
            EmployeBind(objHrCommon);
        }
        void EmpListPaging_FirstClick(object sender, EventArgs e)
        {
            EmployeBind(objHrCommon);
        }

        public int GetParentMenuId()
        {
            if ( Convert.ToInt32(Session["UserId"]) == null)
            {
                Response.Redirect("Logon.aspx");
            }
            string URL = Request.Url.Segments[Request.Url.Segments.Length - 1] + Request.Url.Query;
            int RoleId = Convert.ToInt32(Session["RoleId"]);
            int ModuleId = ModuleID; ;



            DataSet ds = AttendanceDAC.GetAllowed(RoleId, ModuleId, URL);
            int MenuId = 0;
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                MenuId = Convert.ToInt32(ds.Tables[0].Rows[0]["Under"]);
                btnSubmit.Enabled = Convert.ToBoolean(ds.Tables[0].Rows[0]["Editable"]);
              
            }
            return MenuId;
        }
        public static DataSet GetAllowed(int RoleId, int ModuleId, string URL)
        {
              
            SqlParameter[] objParam = new SqlParameter[3];
            objParam[0] = new SqlParameter("@RoleId", RoleId);
            objParam[1] = new SqlParameter("@ModuleId", ModuleId);
            objParam[2] = new SqlParameter("@URL", URL);
            DataSet ds = SQLDBUtil.ExecuteDataset("CP_GetPageAccess", objParam);
            return ds;
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("CriteriaID");
                dt.Columns.Add("Criteria");
                dt.Columns.Add("WeightagePercent");
                dt.Columns.Add("Evalution");
                dt.Columns.Add("Performance");
                int status = 0;
                foreach (GridViewRow gvRow in GridView1.Rows)
                {
                    DataRow dr = dt.NewRow();
                    dr["CriteriaID"] = ((Label)gvRow.FindControl("CtrID")).Text;
                    dr["Criteria"] = ((Label)gvRow.FindControl("CtrName")).Text;
                    dr["WeightagePercent"] = ((Label)gvRow.FindControl("wtgprsnt")).Text;
                    dr["Evalution"] = ((TextBox)gvRow.FindControl("Evalution")).Text;
                    dr["Performance"] = ((Label)gvRow.FindControl("lblperf")).Text;
                    dt.Rows.Add(dr);
                    if (((Label)gvRow.FindControl("CtrID")).Text == ddlCrtID.SelectedValue)
                        status = 1;
                }

                if (status == 0)
                {
                    SqlParameter[] p = new SqlParameter[1];
                    p[0] = new SqlParameter("@CriteriaId", ddlCrtID.SelectedValue);
                    DataSet ds1 = SQLDBUtil.ExecuteDataset("criterialweighpercenate", p);




                    DataRow dr1 = dt.NewRow();
                    dr1["CriteriaID"] = ddlCrtID.SelectedValue;
                    dr1["Criteria"] = ddlCrtID.SelectedItem.Text;
                    dr1["WeightagePercent"] = ds1.Tables[0].Rows[0]["weightagepercent"].ToString();
                    if (ds1.Tables[1].Rows.Count > 0)// property Performance doesnt exits
                    {
                        dr1["Performance"] = ds1.Tables[1].Rows[0]["Performance"].ToString();
                    }
                    else
                    {
                        dr1["Performance"] = "0";
                    }
                    dr1["Evalution"] = "0";
                    dt.Rows.Add(dr1);

                    tblAdd.Visible = false;

                }
                else
                {
                    AlertMsg.MsgBox(Page, "Criteria already assigned to this employee");
                }
                GridView1.DataSource = null;
                GridView1.DataSource = dt;
                GridView1.DataBind();

            }
            catch
            {

            }
        }


        public void GetEmpCriteria()
        {
            try
            {
                // tblvew.Visible = true;
                //tblAdd.Visible = false;
                string emp = "";
                objHrCommon.CurrentPage = EmpListPaging.CurrentPage;
                objHrCommon.PageSize = EmpListPaging.ShowRows;
                if (textsearchemp.Text != String.Empty)
                    emp = textsearchemp.Text.Substring(0, 4);
                  

                SqlParameter[] parms = new SqlParameter[7];
                parms[0] = new SqlParameter("@CurrentPage", objHrCommon.CurrentPage);
                parms[1] = new SqlParameter("@PageSize", objHrCommon.PageSize);
                parms[2] = new SqlParameter("@NoofRecords", SqlDbType.Int);
                parms[2].Direction = ParameterDirection.Output;
                parms[3] = new SqlParameter();
                parms[3].Direction = ParameterDirection.ReturnValue;
                if (ViewState["EmpID"] != String.Empty)
                    parms[4] = new SqlParameter("@EmpID", ViewState["EmpID"]);
                else
                    parms[4] = new SqlParameter("@EmpID", DBNull.Value);

                if (DDlFinyr.SelectedIndex!=0)
                    parms[5] = new SqlParameter("@FinancialYear", Convert.ToInt32(DDlFinyr.SelectedItem.Text));
                else
                    parms[5] = new SqlParameter("@FinancialYear", DBNull.Value);
                if (Convert.ToInt32(ddlSMonth.SelectedValue) > 0)
                    parms[6] = new SqlParameter("@Month", Convert.ToInt32(ddlSMonth.SelectedValue));
                else
                    parms[6] = new SqlParameter("@Month", DBNull.Value);
                DataSet ds = SQLDBUtil.ExecuteDataset("HMS_Get_EMPCreteria", parms);
                objHrCommon.NoofRecords = (int)parms[2].Value;
                objHrCommon.TotalPages = (int)parms[3].Value;
                gvEmpcrdet.Visible = true;

                gvEmpcrdet.DataSource = null;
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    gvEmpcrdet.DataSource = ds;
                    gvEmpcrdet.DataBind();
                    GridView1.Visible = false;
                    gvEmPCriteria.Visible = false;
                    HideGrid.Visible = false;

                    if (Convert.ToInt32(ddlSMonth.SelectedValue) > 0)
                        btnAddCret.Visible = false;
                }
                else
                {
                    gvEmpcrdet.DataBind();
                    btnAddCret.Visible = true;
                    GridView1.Visible = false;
                    HideGrid.Visible = false;
                }


            }
            catch { }

        }


        protected void gvEmPCriteria_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            if (e.CommandName == "DEL")
            {
                int Empid = Convert.ToInt32(e.CommandArgument);
                DeleteContact(Empid);
                AlertMsg.MsgBox(Page, "Deleted Successfully");
                GetEmpCriteria();
            }
            if (e.CommandName == "Name")
            {
                try
                {

                 
                    GridViewRow gvr = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                    Label empname = (Label)gvr.FindControl("lblEmpName");
                    lblEmp.Text = empname.Text;
                    ddlSMonth.SelectedValue = "0";
                  
                    int emp = Convert.ToInt32(e.CommandArgument.ToString());
                    objHrCommon.CurrentPage = EmpListPaging.CurrentPage;
                    objHrCommon.PageSize = EmpListPaging.ShowRows;
                    ViewState["EmpID"] = e.CommandArgument.ToString();
                    if (textsearchemp.Text != String.Empty)
                    {
                        //  emp = textsearchemp.Text.Substring(0, 4);   
                    }

                    //CreterialDetails();
                    gvEmpcrdet.DataSource = null;
                    gvEmpcrdet.DataBind();
                    gvEmPCriteria.Visible = false;
                    gvEmpcrdet.Visible = true;
                    Hide.Visible = true;
                    EmpListPaging.Visible = false;
                    EdtViewAccordion.Visible = false;
                    finyear.Visible = true;
                    btnAddCret.Visible = false;
                    HideGrid.Visible = false;
                    GetEmpCriteria();
                    
                }
                catch { }

            }
            if (e.CommandName == "white")
            {
                int emp = Convert.ToInt32(e.CommandArgument.ToString());
                UpdBlackEmp(emp, 1);
            }
            if (e.CommandName == "Black")
            {
                int emp = Convert.ToInt32(e.CommandArgument.ToString());
                UpdBlackEmp(emp, 0);
            }
        }

        private void CreterialDetails()
        {

        }
        public void DeleteContact(int Empid)
        {
            try
            {
                SqlParameter[] objParam = new SqlParameter[1];
                objParam[0] = new SqlParameter("@ContactID", Empid);
                SQLDBUtil.ExecuteNonQuery("HR_DeleteCriteria", objParam);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            EmpListPaging.CurrentPage = 1;
            EmployeBind(objHrCommon);
            //GetEmpCriteria();
        }

       

        // By Nazima google search for EmpName/ID
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute]



        public static string[] ConvertStingArray(DataSet ds)
        {
            string[] rtval = Array.ConvertAll(ds.Tables[0].Select(), new Converter<DataRow, string>(DataRowTotable));
            return rtval;
        }
        public static string DataRowTotable(DataRow dr)
        {
            return dr["Name"].ToString();
        }

        protected void Hide_Click(object sender, EventArgs e)
        {
            EmployeBind(objHrCommon);
            GridView1.Visible = false;
            gvEmPCriteria.Visible = true;
            Hide.Visible = false;
            HideGrid.Visible = false;
            finyear.Visible = false;
            tblvew.Visible = true;
            EdtViewAccordion.Visible = true;
            gvEmpcrdet.Visible = false;

        }

        protected void Evalution_TextChanged(object sender, EventArgs e)
        {
            decimal total1 = 0; decimal total2 = 0;
           

            TextBox txtevaluation = (TextBox)sender;
            GridViewRow gr = (GridViewRow)txtevaluation.Parent.Parent;

            TextBox Evalution1 = (TextBox)gr.FindControl("Evalution");
            Label wtgprsnt1 = (Label)gr.FindControl("wtgprsnt");

            if (Evalution1.Text != "")
            {
                if (Convert.ToDecimal(Evalution1.Text) > Convert.ToDecimal(wtgprsnt1.Text))
                {
                    AlertMsg.MsgBox(Page, "Evaluation Can not be morethan Weightage");
                    Evalution1.Text = String.Empty;
                    Evalution1.Focus();
                    return;
                }
            }
            foreach (GridViewRow gvr in GridView1.Rows)
            {
                TextBox Evalution = (TextBox)gvr.FindControl("Evalution");
                if (Evalution.Text != "")
                {
                    total1 = total1 + Convert.ToDecimal(Evalution.Text);
                }
            }
            Label lblevaluation = (Label)GridView1.FooterRow.FindControl("lblevaluation");

            lblevaluation.Text = total1.ToString();



        }

        protected void ADD_Click(object sender, EventArgs e)
        {
            tblAdd.Visible = true;
            gvEmPCriteria.Visible = false;

            SqlParameter[] p = new SqlParameter[1];
            p[0] = new SqlParameter("@EmpID", Convert.ToInt32(ViewState["EmpID"]));

            DataSet ds1 = SQLDBUtil.ExecuteDataset("HMS_GetCriteriaIDEMP", p);
            ddlCrtID.DataSource = ds1.Tables[0];
            ddlCrtID.DataValueField = "criteriaId";
            ddlCrtID.DataTextField = "Criteria";

            ddlCrtID.DataBind();
            ddlCrtID.Items.Insert(0, new ListItem("---ALL---", "0", true));

            int EmpID = Convert.ToInt32(ViewState["EmpID"]);
            ddlEmpid.SelectedValue = EmpID.ToString();
            btnSubmit.Enabled = true;

        }
        decimal total = 0;
        float wTotal = 0; int pTotal = 0;
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    Label lblperf = (Label)e.Row.FindControl("lblperf");
                    Label wtgprsnt = (Label)e.Row.FindControl("wtgprsnt");
                   
                        float wtot = float.Parse(wtgprsnt.Text);
                        wTotal = wTotal + wtot;
                
                    TextBox Evalution = (TextBox)e.Row.FindControl("Evalution");
                    if (Evalution.Text != string.Empty)
                    {
                        total = total + Math.Round(Convert.ToDecimal(Evalution.Text));
                    }
                    else
                    {
                        total = total + 0;
                    }



                }
                if (e.Row.RowType == DataControlRowType.Footer)
                {
                    ViewState["Total"] = total.ToString();
                    Label lblevaluation = (Label)e.Row.FindControl("lblevaluation");
                    Label lbltotalwqty = (Label)e.Row.FindControl("lbltotalwqty");
                    lblevaluation.Text = total.ToString();
                    lbltotalwqty.Text = wTotal.ToString();
                }
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        protected void ButtonAdd_Click(object sender, EventArgs e)
        {
            decimal Total = Convert.ToDecimal(ViewState["Total"]);
           
            if (Convert.ToInt32(ddlSMonth.SelectedValue) > 0 && DDlFinyr.SelectedIndex>0)
            {
                  
                int EMPID = Convert.ToInt32(ViewState["EmpID"]);
             
                int idmast = 0, count = 0; decimal total1 = 0;
                foreach (GridViewRow gvRow in GridView1.Rows)
                {
                    Label wtgprsnt1 = (Label)gvRow.FindControl("wtgprsnt");
                    TextBox Evalution1 = (TextBox)gvRow.FindControl("Evalution");
                    if (Evalution1.Text != "")
                    {
                        if (Convert.ToDecimal(Evalution1.Text) > Convert.ToDecimal(wtgprsnt1.Text))
                        {
                            count++;

                        }
                        total1 = total1 + Convert.ToDecimal(Evalution1.Text);
                        Label lblevaluation = (Label)GridView1.FooterRow.FindControl("lblevaluation");
                        lblevaluation.Text = total1.ToString();
                    }
                }
                ViewState["Total"] = total1.ToString();
                if (count == 0)
                {

                    foreach (GridViewRow gvRow in GridView1.Rows)
                    {
                        Label CtrID = (Label)gvRow.FindControl("CtrID");
                        Label wtgprsnt = (Label)gvRow.FindControl("wtgprsnt");
                        TextBox Evalution = (TextBox)gvRow.FindControl("Evalution");
                        Label lblperf = (Label)gvRow.FindControl("lblperf");

                        SqlParameter[] parms = new SqlParameter[10];
                        parms[0] = new SqlParameter("@EmpID", Convert.ToInt32(ViewState["EmpID"]));
                        parms[1] = new SqlParameter("@FinancialYear", DDlFinyr.SelectedItem.Text);
                        parms[2] = new SqlParameter("@Name", ViewState["Name"]);
                        parms[3] = new SqlParameter("@Total", ViewState["Total"]);
                        parms[4] = new SqlParameter("@Month", ddlSMonth.SelectedValue);
                        parms[5] = new SqlParameter("@evalution", Evalution.Text);
                        parms[6] = new SqlParameter("@criteriaid", CtrID.Text);
                        parms[7] = new SqlParameter("@performance", lblperf.Text);
                        parms[8] = new SqlParameter("@idmast", ViewState["idmast"]);
                        parms[9] = new SqlParameter("@ReturnValue", System.Data.SqlDbType.Int);
                        parms[9].Direction = ParameterDirection.ReturnValue;
                        SQLDBUtil.ExecuteNonQuery("EmployeeEvaluationTotal", parms);
                        ViewState["idmast"] = parms[9].Value;
                    }
                    if (ViewState["idmast"].ToString() != "0")
                    {
                        AlertMsg.MsgBox(Page, "Saved !");
                     
                        GetEmpCriteria();

                    }
                }
                else
                {
                    AlertMsg.MsgBox(Page, "Evaluation Can not be morethan Weightage");
                    return;
                }
            }
            else
            {
                AlertMsg.MsgBox(Page, "Select Month/Year !");
                return;
            }
         
        }

        //fror worksite
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletiondeptWorkList(string prefixText, int count, string contextKey)
        {
           
            DataSet ds = AttendanceDAC.HR_GetWorkSite_By_empVsAirTicketsAuth(prefixText);
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

        //for department
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletiondeptList(string prefixText, int count, string contextKey)
        {
         
            DataSet ds = AttendanceDAC.GetSearchDesiginationFilterActive(prefixText, CompanyID, Siteid);
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
        void EmployeBind(HRCommon objHrCommon)
        {

            try
            {
                objHrCommon.PageSize = EmpListPaging.ShowRows;
                objHrCommon.CurrentPage = EmpListPaging.CurrentPage;
                int? EmpID = null;
                int? Dept = null;
                int? WS = null;
                if (textsearchemp.Text != String.Empty)
                    EmpID = Convert.ToInt32(textsearchemp.Text.Substring(0, 4));
                if (TxtDept.Text.Trim() != "")
                {
                    Dept = Convert.ToInt32(TxtDept_hid.Value == "" ? "0" : TxtDept_hid.Value);

                }
                if (Txtwrk.Text.Trim() != "")
                {
                    WS = Convert.ToInt32(Txtwrk_hid.Value == "" ? "0" : Txtwrk_hid.Value);

                }
                  
                int key = 1;
                if (Request.QueryString.Count > 0)
                {

                    key = Convert.ToInt32(Request.QueryString["key"]);

                }

              DataSet  ds = AttendanceDAC.HR_Getemployeeclearence_EMP(objHrCommon, EmpID, WS, Dept, Convert.ToInt32(Session["CompanyID"]));
                if (key == 2)
                {
                    ds = AttendanceDAC.HR_Getemployeeclearence_EMP_BlackList(objHrCommon, EmpID, WS, Dept, Convert.ToInt32(Session["CompanyID"]));
                }


                if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0)
                {
                    gvEmPCriteria.DataSource = ds;
                    gvEmPCriteria.DataBind();
                }
                else
                {
                    gvEmPCriteria.DataSource = null;
                    gvEmPCriteria.DataBind();
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

            // WS = Convert.ToInt32(Txtwrk_hid.Value == "" ? "0" : Txtwrk_hid.Value); ;
            WSiteid = Convert.ToInt32(Txtwrk_hid.Value == "" ? "0" : Txtwrk_hid.Value);
            Siteid = Convert.ToInt32(Txtwrk_hid.Value == "" ? "0" : Txtwrk_hid.Value);
        }

        protected void btnEmpcrsearch_Click(object sender, EventArgs e)
        {
            EmpListPaging.CurrentPage = 1;
                GetEmpCriteria();
         
        }

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
                    ddlSMonth.SelectedValue = lblMonth.Text;
                    int emp = Convert.ToInt32(e.CommandArgument.ToString());
                    objHrCommon.CurrentPage = EmpListPaging.CurrentPage;
                    objHrCommon.PageSize = EmpListPaging.ShowRows;
                    ViewState["EmpID"] = e.CommandArgument.ToString();
                    if (textsearchemp.Text != String.Empty)
                    {
                        // emp = textsearchemp.Text.Substring(0, 4);   
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
                DataSet    dsk = SQLDBUtil.ExecuteDataset("HMS_Get_EMPCreteriaDetails", parms);
                    objHrCommon.NoofRecords = (int)parms[2].Value;
                    objHrCommon.TotalPages = (int)parms[3].Value;
                    GridView1.Visible = true;
                    GridView1.DataSource = dsk;
                    GridView1.DataBind();
                    //total = Convert.ToInt32(dsk.Tables[0].Rows[0]["Total"].ToString());
                    HideGrid.Visible = true;
                    gvEmpcrdet.Visible = true;
                    GridView1.Visible = true;

                    gvEmPCriteria.Visible = false;
                    // gvEmpcrdet.Visible = false;
                    Hide.Visible = true;
                    EmpListPaging.Visible = false;
                    EdtViewAccordion.Visible = false;
                    finyear.Visible = true;
                    EmpListPaging.Visible = false;

                    foreach (GridViewRow row in gvEmpcrdet.Rows)
                    {
                        LinkButton lnkdisplay = (LinkButton)row.FindControl("lnkview");
                        lnkdisplay.CssClass = "btn btn-primary";
                    }

                    LinkButton lnkdisplaygrid = (LinkButton)gvr.FindControl("lnkview");
                    lnkdisplaygrid.CssClass = "btn btn-success";
                }
                catch { }

            }

        }
        public void UpdBlackEmp(int Empid, int Isblacklist)
        {
            SqlParameter[] parms = new SqlParameter[3];
            parms[0] = new SqlParameter("@Empid", Empid);
            parms[1] = new SqlParameter("@CreatedBY",  Convert.ToInt32(Session["UserId"]));
            parms[2] = new SqlParameter("@IsBlacklist", Isblacklist);
            DataSet dsk = SQLDBUtil.ExecuteDataset("sh_UpdBlacklist", parms);
            if (Isblacklist == 0)
            {
                AlertMsg.MsgBox(Page, "Employee BlackListed !");
            }
            else
            {
                AlertMsg.MsgBox(Page, "Employee WhiteListed !");
            }
            EmployeBind(objHrCommon);
        }

        protected void btnAddCret_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(ddlSMonth.SelectedValue) > 0 && Convert.ToInt32(DDlFinyr.SelectedValue) > 0)
            {
                string date = "01" + "/" + Convert.ToInt32(ddlSMonth.SelectedValue) + "/" + Convert.ToInt32(DDlFinyr.SelectedItem.Text);
                DateTime dt = CODEUtility.ConvertToDate(date, DateFormat.DayMonthYear);
                var lastDayOfMonth = dt.AddMonths(1).AddDays(-1).Date;
                if (lastDayOfMonth < DateTime.Now)
                {
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

                    if (DDlFinyr.Text != String.Empty)
                        parms[5] = new SqlParameter("@FinancialYear", DDlFinyr.SelectedItem.Text);
                    parms[6] = new SqlParameter("@Month", ddlSMonth.SelectedValue);
                    DataSet dsk = SQLDBUtil.ExecuteDataset("HMS_Get_EMPCreteriaDetails", parms);
                    objHrCommon.NoofRecords = (int)parms[2].Value;
                    objHrCommon.TotalPages = (int)parms[3].Value;
                    GridView1.Visible = true;
                    GridView1.DataSource = dsk;
                    GridView1.DataBind();

                    HideGrid.Visible = true;
                    gvEmpcrdet.Visible = false;
                    GridView1.Visible = true;
                    btnAddCret.Visible = false;
                    EmpListPaging.Visible = false;
                }
                else
                {
                    gvEmpcrdet.DataSource = null;
                    gvEmpcrdet.DataBind();
                    AlertMsg.MsgBox(Page, "Future/Current Month Not Acceptable.");
                }
            }
            else
            {
                AlertMsg.MsgBox(Page, "Select Month/Year !");
            }
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionListemp(string prefixText, int count, string contextKey)
        {
            DataSet ds = AttendanceDAC.GetGoogleSearch(prefixText, Empid);
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

        protected void lnktotal_Click(object sender, EventArgs e)
        {
            decimal total1 = 0;
            foreach (GridViewRow row in GridView1.Rows)
            {
                TextBox Evalution = (TextBox)row.FindControl("Evalution");
                
                if (Evalution.Text != string.Empty)
                {
                    total1 = total1 + Math.Round(Convert.ToDecimal(Evalution.Text)); 
                    

                }
                else
                {
                    total1 = total1 + 0;
                }
            }
            GridViewRow rowf = GridView1.FooterRow;
            Label lblevaluation = (Label)rowf.FindControl("lblevaluation");
            lblevaluation.Text =" : "+ total1.ToString();
            
        }
    }
}















