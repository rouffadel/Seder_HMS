using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using AECLOGIC.HMS.BLL;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using AECLOGIC.ERP.COMMON;
using AECLOGIC.ERP.HMS.HRClasses;
using Aeclogic.Common.DAL;

namespace AECLOGIC.ERP.HMS
{
    public partial class loanadj : AECLOGIC.ERP.COMMON.WebFormMaster
    {
       
        static int Siteid;
        
         
        AttendanceDAC objRights = new AttendanceDAC();
        MasterPage objmaster = new MasterPage();
        HRCommon objHrCommon = new HRCommon();

        static int ModID;
        static int Userid;
        protected override void OnInit(EventArgs e)
        {
            ModuleID = 1;
            base.OnInit(e);
            EmpReimbursementAprovedPaging.FirstClick += new Paging.PageFirst(EmpReimbursementAprovedPaging_FirstClick);
            EmpReimbursementAprovedPaging.PreviousClick += new Paging.PagePrevious(EmpReimbursementAprovedPaging_FirstClick);
            EmpReimbursementAprovedPaging.NextClick += new Paging.PageNext(EmpReimbursementAprovedPaging_FirstClick);
            EmpReimbursementAprovedPaging.LastClick += new Paging.PageLast(EmpReimbursementAprovedPaging_FirstClick);
            EmpReimbursementAprovedPaging.ChangeClick += new Paging.PageChange(EmpReimbursementAprovedPaging_FirstClick);
            EmpReimbursementAprovedPaging.ShowRowsClick += new Paging.ShowRowsChange(EmpReimbursementAprovedPaging_ShowRowsClick);
            EmpReimbursementAprovedPaging.CurrentPage = 1;
        }
        void EmpReimbursementAprovedPaging_ShowRowsClick(object sender, EventArgs e)
        {
            EmpReimbursementAprovedPaging.CurrentPage = 1;
            BindGrid();
        }
        void EmpReimbursementAprovedPaging_FirstClick(object sender, EventArgs e)
        {
            EmpReimbursementAprovedPaging.CurrentPage = 1;
            empid_hd.Value = "0";
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                try
                {
                    string id =  Convert.ToInt32(Session["UserId"]).ToString();
                }
                catch
                {
                    Response.Redirect("Home.aspx");
                }
                Userid = Convert.ToInt32( Convert.ToInt32(Session["UserId"]).ToString());

                if (!IsPostBack)
                {
                    bindyear();
                    BindGrid();
                }
            }
            catch { }
        }
      

        void bindyear()
        {
            FIllObject.FillDropDown(ref ddlyear, "HMS_YearWise");
            DataSet ds = AttendanceDAC.GetCalenderYear();
            if (ds.Tables[0].Rows[0]["PreviousMonth"].ToString() == "0")
            {
                ddlmonth.SelectedValue = "12";

                int CurrentYear = Convert.ToInt32(ds.Tables[0].Rows[0]["CurrentYear"]);
                int PreviousYear = CurrentYear - 1;
                ddlyear.Items.FindByValue(PreviousYear.ToString()).Selected = true;

            }
            //if we are in same year and same month
            else
            {
                ddlmonth.SelectedValue = ds.Tables[0].Rows[0]["CurrentMonth"].ToString();
                if (ddlyear.Items.FindByValue(ds.Tables[0].Rows[0]["CurrentYear"].ToString()) != null)
                {
                    ddlyear.Items.FindByValue(ds.Tables[0].Rows[0]["CurrentYear"].ToString()).Selected = true;
                }
                else
                {
                    ddlyear.SelectedIndex = 0;
                    //ddlYear.Items.Count - 1
                }
            }
        }

      
        public void BindGrid()
        {

            try
            {
                objHrCommon.PageSize =EmpReimbursementAprovedPaging.ShowRows;
                objHrCommon.CurrentPage = EmpReimbursementAprovedPaging.CurrentPage;
                 int EmpID = 0;
                 DataSet ds=null;
                if (txtempid.Text == "" || txtempid.Text == null)
                {
                    empid_hd.Value = "";
                }
                else if (txtempid.Text != "" || txtempid.Text != null)
                {
                    EmpID = Convert.ToInt32(empid_hd.Value == "" ? "0" : empid_hd.Value);
                }
                 string date = "01" + "/" + Convert.ToInt32(ddlmonth.SelectedValue) + "/" + Convert.ToInt32(ddlyear.SelectedItem.Text);
                DateTime dt = CODEUtility.ConvertToDate(date, DateFormat.DayMonthYear);
                var lastDayOfMonth = dt.AddMonths(1).AddDays(-1).Date;
                if (Request.QueryString.Count > 0)
                {
                    int key = Convert.ToInt32(Request.QueryString["key"]);
                    
                        SqlParameter[] parms = new SqlParameter[9];
                        parms[0] = new SqlParameter("@month", Convert.ToInt32(ddlmonth.SelectedValue));
                        parms[1] = new SqlParameter("@year", Convert.ToInt32(ddlyear.SelectedItem.Text));
                        parms[2] = new SqlParameter("@CurrPage", objHrCommon.CurrentPage);
                        parms[3] = new SqlParameter("@PageSize", objHrCommon.PageSize);
                        parms[4] = new SqlParameter("@NrRecords", System.Data.SqlDbType.Int);
                        parms[4].Direction = ParameterDirection.Output;
                        parms[5] = new SqlParameter("@Empid", EmpID);
                        parms[6] = new SqlParameter();
                        parms[6].Direction = ParameterDirection.ReturnValue;
                        if (key == 1)
                        {
                            parms[7] = new SqlParameter("@Status", 1);//Recmnd
                        }
                        else
                        {
                            parms[7] = new SqlParameter("@Status", 2);//Approved

                        }
                        parms[8] = new SqlParameter("@fcase", 2);//Approved

                        ds = SQLDBUtil.ExecuteDataset("Sh_INSUPDRecoveryApprovals", parms);
                        objHrCommon.NoofRecords = (int)parms[4].Value;
                        objHrCommon.TotalPages = (int)parms[6].Value;


                        if (key == 1)
                        {
                            btnrecmnd.Visible = true;
                            btnrecmnd.Text = "Approve";
                            btnrecmnd.CssClass = "btn btn-success";
                        }
                        else
                        {
                            btnrecmnd.Visible = false;
                        }


                }
                else
                {
                    SqlParameter[] parms = new SqlParameter[7];
                    parms[0] = new SqlParameter("@PayStartDate", dt);
                    parms[1] = new SqlParameter("@PayEndDate", lastDayOfMonth);
                    parms[2] = new SqlParameter("@CurrPage", objHrCommon.CurrentPage);
                    parms[3] = new SqlParameter("@PageSize", objHrCommon.PageSize);
                    parms[4] = new SqlParameter("@NrRecords", System.Data.SqlDbType.Int);
                    parms[4].Direction = ParameterDirection.Output;
                    parms[5] = new SqlParameter("@Empid", EmpID);
                    parms[6] = new SqlParameter();
                    parms[6].Direction = ParameterDirection.ReturnValue;

                    ds = SQLDBUtil.ExecuteDataset("sh_SalariesAdjustment", parms);
                    objHrCommon.NoofRecords = (int)parms[4].Value;
                    objHrCommon.TotalPages = (int)parms[6].Value;
                }



                if (ds.Tables[0].Rows.Count > 0)
                {
                    gvVeiw.DataSource = ds.Tables[0];
                    EmpReimbursementAprovedPaging.Bind(objHrCommon.CurrentPage, objHrCommon.TotalPages, objHrCommon.NoofRecords, objHrCommon.PageSize);
                }
                else
                    gvVeiw.DataSource = null;
                gvVeiw.DataBind();



            }
            catch { }
        }
      
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetEmpidList(string prefixText, int count, string contextKey)
        {
            DataSet ds = AttendanceDAC.GetGoogleSearch_by_EmpName_All(prefixText);
            DataTable dt = ds.Tables[0];
            List<string> items = new List<string>(count);
            var rtval = new Dictionary<string, string>();
            foreach (DataRow row in dt.Rows)
            {
                string str = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(row["Name"].ToString(), row["ID"].ToString());
                items.Add(str);
            }
            return items.ToArray(); ;
        }

        protected void gvVeiw_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.Header)
                {
                    CheckBox chkMail = (CheckBox)e.Row.FindControl("chkSelectAll");
                    chkMail.Attributes.Add("onclick", String.Format("javascript:SelectAll(this,'{0}','chkToTransfer');", gvVeiw.ClientID));
                }
            }
            catch
            {

            }
        }

        protected void btnrecmnd_Click(object sender, EventArgs e)
        {
            int EmpID = 0, count = 0;
            int status = 1;
            int Key = 0;
           // recmnd -1
            //approved -2
            //Th_RecoveryApprovals 
            foreach (GridViewRow gvRow in gvVeiw.Rows)
            {
              
                CheckBox chk = new CheckBox();
                chk = (CheckBox)gvRow.FindControl("chkToTransfer");
                if (chk.Checked)
                {
                    Label lblEmp = (Label)gvRow.FindControl("lblEmpID");
                    Label lbladjdays = (Label)gvRow.FindControl("lbladjdays");
                    Label lblAdjAmt = (Label)gvRow.FindControl("lblAdjAmt");
                    Label lblmonth = (Label)gvRow.FindControl("lblmonth");
                    Label lblyear = (Label)gvRow.FindControl("lblyear");
                    Label lblrowid = (Label)gvRow.FindControl("lblrowid");
                   
                    if (Request.QueryString.Count > 0)
                    {
                        Key = Convert.ToInt32(Request.QueryString["key"]);

                    }

                    SqlParameter[] parms = new SqlParameter[8];
                    parms[0] = new SqlParameter("@fcase", 1);
                    parms[1] = new SqlParameter("@id", Convert.ToInt32(lblrowid.Text));
                    parms[2] = new SqlParameter("@Empid", Convert.ToInt32(lblEmp.Text));
                    parms[3] = new SqlParameter("@month", Convert.ToInt32(lblmonth.Text));
                    parms[4] = new SqlParameter("@year", Convert.ToInt32(lblyear.Text));
                    parms[5] = new SqlParameter("@adjdays", Convert.ToInt32(lbladjdays.Text));
                    parms[6] = new SqlParameter("@adjamt", Convert.ToDecimal(lblAdjAmt.Text));
                    if(Key==1)
                        parms[7] = new SqlParameter("@Status", 2);
                     else
                        parms[7] = new SqlParameter("@Status", 1);
                    SQLDBUtil.ExecuteNonQuery("Sh_INSUPDRecoveryApprovals", parms);


                    count = count + 1;
                }

            }
            if (count > 0)
            {
                if (Key == 1)
                    AlertMsg.MsgBox(Page, "Approved !");
                else
                {
                    AlertMsg.MsgBox(Page, "Recommended !");

                }
                BindGrid();
            }
            else
            {
                AlertMsg.MsgBox(Page, "Select atleast one Record !");

            }
           
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            EmpReimbursementAprovedPaging.CurrentPage = 1;
            BindGrid();
        }
    }
}