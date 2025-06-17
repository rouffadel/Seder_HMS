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

namespace AECLOGIC.ERP.HMS.HMS
{
    public partial class EmployeeEvalutionBlockList : AECLOGIC.ERP.COMMON.WebFormMaster
    {
        #region Variables
       
        static int Siteid;
        static int SearchCompanyID;
        static int EDeptid = 0;
        static int CompanyID = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["CompanyID"]);
      
        static int WSiteid;
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

                    if (Request.QueryString.Count > 0)
                    {
                        objHrCommon.CurrentPage = EmpListPaging.CurrentPage;
                        objHrCommon.PageSize = EmpListPaging.ShowRows;
                        EmployeBind(objHrCommon);
                        tblvew.Visible = true;
                    }
                    else
                    {
                        tblvew.Visible = false;

                    }
                  

                    DataSet dss = SQLDBUtil.ExecuteDataset("HR_Get_FinancialYearList");
                   
                    DDlFinyr.DataSource = dss.Tables[0];
                    DDlFinyr.DataValueField = "FinYearId";
                    DDlFinyr.DataTextField = "Name";
                    DDlFinyr.DataBind();
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

     
        public static DataSet GetAllowed(int RoleId, int ModuleId, string URL)
        {
              
            SqlParameter[] objParam = new SqlParameter[3];
            objParam[0] = new SqlParameter("@RoleId", RoleId);
            objParam[1] = new SqlParameter("@ModuleId", ModuleId);
            objParam[2] = new SqlParameter("@URL", URL);
          DataSet  ds = SQLDBUtil.ExecuteDataset("CP_GetPageAccess", objParam);
            return ds;
        }

   


        public void GetEmpCriteria()
        {
            try
            {
               
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

                if (DDlFinyr.Text != String.Empty)
                    parms[5] = new SqlParameter("@FinancialYear", Convert.ToInt32(DDlFinyr.SelectedItem.Text));
                else
                    parms[5] = new SqlParameter("@FinancialYear", DBNull.Value);
                if (Convert.ToInt32(ddlSMonth.SelectedValue) > 0)
                    parms[6] = new SqlParameter("@Month", Convert.ToInt32(ddlSMonth.SelectedValue));
                else
                    parms[6] = new SqlParameter("@Month", DBNull.Value);
              DataSet  ds = SQLDBUtil.ExecuteDataset("HMS_Get_EMPCreteria_blockLIst", parms);
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
                   
                }
                else
                {
                    gvEmpcrdet.DataBind();
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

                  
                    int emp = Convert.ToInt32(e.CommandArgument.ToString());
                    objHrCommon.CurrentPage = EmpListPaging.CurrentPage;
                    objHrCommon.PageSize = EmpListPaging.ShowRows;
                    ViewState["EmpID"] = e.CommandArgument.ToString();
                    if (textsearchemp.Text != String.Empty)
                    {
                        //  emp = textsearchemp.Text.Substring(0, 4);   
                    }


                    gvEmPCriteria.Visible = false;
                    gvEmpcrdet.Visible = true;
                    ddlSMonth.SelectedValue = "0";
                    gvEmpcrdet.DataSource = null;
                    gvEmpcrdet.DataBind();
                    EmpListPaging.Visible = false;
                    EdtViewAccordion.Visible = false;
                    finyear.Visible = true;
                    HideGrid.Visible = true;
                    Hide.Visible = true;
                 
                }
                catch { }

            }
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
           
        }

       

        // By Nazima google search for EmpName/ID
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute]


        public static string[] GetCompletionListemp(string prefixText, int count)
        {
            DataSet ds = AttendanceDAC.HR_GoogleSearchEmpBySiteDept_Evalution(prefixText, WSiteid, EDeptid, "Y", CompanyID);
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

        }

        protected void Evalution_TextChanged(object sender, EventArgs e)
        {
            decimal total1 = 0; decimal total2 = 0;
            foreach (GridViewRow gvr in GridView1.Rows)
            {
                Label lbl = (Label)gvr.FindControl("lblperf");
                TextBox Evalution = (TextBox)gvr.FindControl("Evalution");
                Label wtgprsnt = (Label)gvr.FindControl("wtgprsnt");
                Button ButtonAdd = (Button)gvr.FindControl("ButtonAdd");
                decimal str = Convert.ToDecimal(Evalution.Text);
                decimal weight = Convert.ToDecimal(wtgprsnt.Text);
                decimal total = (str * weight) / 100;
                lbl.Text = total.ToString();
                Label lblperf = (Label)gvr.FindControl("lblperf");
                decimal qty = Convert.ToDecimal(lblperf.Text);
                total1 = total1 + qty;
                ViewState["Total"] = total1.ToString();
              

            }
            GridView1.FooterRow.Cells[5].Text = total1.ToString();


        }

        protected void ADD_Click(object sender, EventArgs e)
        {
            gvEmPCriteria.Visible = false;
            int EmpID = Convert.ToInt32(ViewState["EmpID"]);
          

        }
        float total = 0; float wTotal = 0; int pTotal = 0;
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

        protected void ButtonAdd_Click(object sender, EventArgs e)
        {
            decimal Total = Convert.ToDecimal(ViewState["Total"]);
            if (Total != 0)
            {
                if (Convert.ToInt32(ddlSMonth.SelectedValue) > 0)
                {
                      
                    int EMPID = Convert.ToInt32(ViewState["EmpID"]);
                   
                    int idmast = 0;

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
                    if (ViewState["idmast"] != "0")
                    {
                        AlertMsg.MsgBox(Page, "Inserted Sucessfully.");
                        Response.Redirect("EmployeeEvaluation.aspx");
                    }
                }
                else
                {
                    AlertMsg.MsgBox(Page, "Select Month.");
                }
            }
            else
            {
                AlertMsg.MsgBox(Page, "Enter Evaluation Atleast one");
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
                try { 
                if (textsearchemp.Text != String.Empty)
                    EmpID = Convert.ToInt32(textsearchemp.Text.Substring(0, 4));
                }
                catch { textsearchemp.Text = ""; }
                if (TxtDept.Text.Trim() != "")
                {
                    Dept = Convert.ToInt32(TxtDept_hid.Value == "" ? "0" : TxtDept_hid.Value);

                }
                if (Txtwrk.Text.Trim() != "")
                {
                    WS = Convert.ToInt32(Txtwrk_hid.Value == "" ? "0" : Txtwrk_hid.Value);

                }
                  
                DataSet ds = AttendanceDAC.HR_Getemployeeclearence_EMP_BlackList(objHrCommon, EmpID, WS, Dept, Convert.ToInt32(Session["CompanyID"]));
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
                    Label empname = (Label)gvr.FindControl("lblEmpN");
                    Label lblYear = (Label)gvr.FindControl("lblYear");
                    Label lblMonth = (Label)gvr.FindControl("lblMonth");
                    if(empname.Text!=null)
                    lblEmp.Text = empname.Text;
                    ddlSMonth.SelectedValue = lblMonth.Text;
                    int emp = Convert.ToInt32(e.CommandArgument.ToString());
                    objHrCommon.CurrentPage = EmpListPaging.CurrentPage;
                    objHrCommon.PageSize = EmpListPaging.ShowRows;
                    ViewState["EmpID"] = e.CommandArgument.ToString();
                    if (textsearchemp.Text != String.Empty)
                    {
                        //  emp = textsearchemp.Text.Substring(0, 4);   
                    }

                    SqlParameter[] parms = new SqlParameter[7];
                    parms[0] = new SqlParameter("@CurrentPage", 1);
                    parms[1] = new SqlParameter("@PageSize", 100);
                    parms[2] = new SqlParameter("@NoofRecords", SqlDbType.Int);
                    parms[2].Direction = ParameterDirection.Output;
                    parms[3] = new SqlParameter();
                    parms[3].Direction = ParameterDirection.ReturnValue;
                    if (ViewState["EmpID"] != String.Empty)
                        parms[4] = new SqlParameter("@EmpID", ViewState["EmpID"]);
                    else
                        parms[4] = new SqlParameter("@EmpID", DBNull.Value);


                    parms[5] = new SqlParameter("@FinancialYear", lblYear.Text);
                    parms[6] = new SqlParameter("@Month", lblMonth.Text);
                  DataSet  dsk = SQLDBUtil.ExecuteDataset("HMS_Get_EMPCreteriaDetails", parms);
                    objHrCommon.NoofRecords = (int)parms[2].Value;
                    objHrCommon.TotalPages = (int)parms[3].Value;
                    GridView1.Visible = true;
                    GridView1.DataSource = dsk;
                    GridView1.DataBind();

                    gvEmpcrdet.Visible = false;
                    GridView1.Visible = true;
                    gvEmPCriteria.Visible = false;
                    //Hide.Visible = true;
                    EmpListPaging.Visible = false;
                    EdtViewAccordion.Visible = false;
                    finyear.Visible = true;
                    HideGrid.Visible = true;
                    Hide.Visible = true;

                }
                catch { }

            }
        }

        protected void btnAddCret_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(ddlSMonth.SelectedValue) > 0)
            {
                SqlParameter[] parms = new SqlParameter[7];
                parms[0] = new SqlParameter("@CurrentPage", 1);
                parms[1] = new SqlParameter("@PageSize", 100);
                parms[2] = new SqlParameter("@NoofRecords", SqlDbType.Int);
                parms[2].Direction = ParameterDirection.Output;
                parms[3] = new SqlParameter();
                parms[3].Direction = ParameterDirection.ReturnValue;
                if (ViewState["EmpID"] != String.Empty)
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

                gvEmpcrdet.Visible = false;
                GridView1.Visible = true;
                EmpListPaging.Visible = false;
            }
            else
            {
                AlertMsg.MsgBox(Page, "Select Month.");
            }
        }
    }
}