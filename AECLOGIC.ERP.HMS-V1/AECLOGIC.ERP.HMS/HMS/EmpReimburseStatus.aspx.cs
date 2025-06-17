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
    public partial class EmpReimburseStatus : AECLOGIC.ERP.COMMON.WebFormMaster
    {
        int Id = 1;
      
        HRCommon objHrCommon = new HRCommon();
        static int txtEmpID;

        protected override void OnInit(EventArgs e)
        {
            ModuleID = 1;
            base.OnInit(e);
            #region Approvedpaging
            EmpReimbursementStatusPaging.FirstClick += new Paging.PageFirst(EmpReimbursementStatusPaging_FirstClick);
            EmpReimbursementStatusPaging.PreviousClick += new Paging.PagePrevious(EmpReimbursementStatusPaging_FirstClick);
            EmpReimbursementStatusPaging.NextClick += new Paging.PageNext(EmpReimbursementStatusPaging_FirstClick);
            EmpReimbursementStatusPaging.LastClick += new Paging.PageLast(EmpReimbursementStatusPaging_FirstClick);
            EmpReimbursementStatusPaging.ChangeClick += new Paging.PageChange(EmpReimbursementStatusPaging_FirstClick);
            EmpReimbursementStatusPaging.ShowRowsClick += new Paging.ShowRowsChange(EmpReimbursementStatusPaging_ShowRowsClick);
            EmpReimbursementStatusPaging.CurrentPage = 1;
            #endregion Approvedpaging
        }

        #region Paging
        void EmpReimbursementStatusPaging_ShowRowsClick(object sender, EventArgs e)
        {
            EmpReimbursementStatusPaging.CurrentPage = 1;
            BindPager();
        }
        void EmpReimbursementStatusPaging_FirstClick(object sender, EventArgs e)
        {
            if (hdnSearchChangeStatus.Value == "1")
                EmpReimbursementStatusPaging.CurrentPage = 1;
            BindPager();
            hdnSearchChangeStatus.Value = "0";
        }
        void BindPager()
        {
            int EmpID =  Convert.ToInt32(Session["UserId"]); ;
            if (ddlFilterEmp.SelectedIndex != -1 && txtEmpID == 0)
            {
                EmpID = Convert.ToInt32(ddlFilterEmp.SelectedValue);
            }
            else
                EmpID = txtEmpID;
            txtEmpID = 0;
            objHrCommon.PageSize = EmpReimbursementStatusPaging.CurrentPage;
            objHrCommon.CurrentPage = EmpReimbursementStatusPaging.ShowRows;
            EmpReimburseStatusByEmpIDByPaging(objHrCommon, EmpID);

        }

        #endregion Paging


        protected void Page_Load(object sender, EventArgs e)
        {
           
            if (!IsPostBack)
            {
                
                DataSet dsAU = AttendanceDAC.GetAu();
                DataRow dr = dsAU.Tables[0].NewRow();
                dr["Au_Id"] = 0;
                dr["Au_Name"] = "---Select---";
                dsAU.Tables[0].Rows.InsertAt(dr, 0);
                dsAU.AcceptChanges();
                ArrayList alUnitIndexes = new ArrayList();
                foreach (DataRow row in dsAU.Tables[0].Rows)
                {
                    alUnitIndexes.Add(row["Au_Id"].ToString().Trim());
                }
                ViewState["alUnitIndexes"] = alUnitIndexes;
                ViewState["dsAU"] = dsAU;
                ViewState["EmpID"] = 0;
               
                BindPager();
                gvView.DataBind();
                if (Convert.ToInt32(Session["RoleID"]) == 8 || Convert.ToInt32(Session["RoleID"]) == 6)
                {
                    tblOther.Visible = true;
                   
                 DataSet   dsddl = AttendanceDAC.GetEmployeesByCompID(Convert.ToInt32(Session["CompanyID"]));

                    
                    ddlFilterEmp.DataSource = dsddl.Tables[0];
                    ddlFilterEmp.DataTextField = "name";
                    ddlFilterEmp.DataValueField = "EmpID";
                    ddlFilterEmp.DataBind();
                    ddlFilterEmp.Items.Insert(0, new ListItem("---SELECT EMPLOYEE---", "0", true));
                }
                else
                {

                    ViewState["EmpID"] =  Convert.ToInt32(Session["UserId"]);
                    tblOther.Visible = false;
                }

            }

        }
        //

        void EmpReimburseStatusByEmpIDByPaging(HRCommon objHrCommon, int EmpID)
        {
            try
            {
                objHrCommon.PageSize = EmpReimbursementStatusPaging.ShowRows;
                objHrCommon.CurrentPage = EmpReimbursementStatusPaging.CurrentPage;


                DataSet ds = AttendanceDAC.HR_EmpReimburseStatusByEmpIDByPaging(objHrCommon, EmpID);

                if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0)
                {
                    gvView.DataSource = ds;

                    EmpReimbursementStatusPaging.Bind(objHrCommon.CurrentPage, objHrCommon.TotalPages, objHrCommon.NoofRecords, objHrCommon.PageSize);

                }
                else
                {
                    gvView.EmptyDataText = "No Records Found";
                    EmpReimbursementStatusPaging.Visible = false;
                }
                gvView.DataBind();


            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public DataSet GetAUDataSet()
        {
            return (DataSet)ViewState["dsAU"];
        }
        protected void ddlFilterEmp_SelectedIndexChanged(object sender, EventArgs e)
        {
            int EmpID = Convert.ToInt32(ddlFilterEmp.SelectedValue);
           
            BindPager();
            gvView.DataBind();
            gvView.Visible = true;
            
            tblView.Visible = true;
            ViewState["EmpID"] = EmpID;
            txtFilterEmp.Text = "";

        }
        public int GetAUIndex(string AUID)
        {
            ArrayList alUnitIndexes = (ArrayList)ViewState["alUnitIndexes"];
            return alUnitIndexes.IndexOf(AUID.Trim());
        }
      
        public string DocNavigateUrl(string Proof)
        {
            string ReturnVal = "";
            string Value = Proof.Split('.')[Proof.Split('.').Length - 1];
            ReturnVal = "./EmpReimbureseProof/" + Proof;
            if (ReturnVal == "./EmpReimbureseProof/")
            {
                return null;
            }
            else
            {
                return ReturnVal;
            }
        }


        protected string FormatInput(object EntryType)
        {
            string retValue = "";
            string input = EntryType.ToString();
            if (input == "1")
            {
                retValue = "Pending";

            }
            if (input == "2")
            {
                retValue = "Approved";

            }
            if (input == "3")
            {
                retValue = "Rejected";
            }
            if (input == "4")
            {
                retValue = "Transfered";
            }
            return retValue;
        }
        protected void gvView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int Status = Convert.ToInt32(e.CommandArgument);

            DataSet ds = AttendanceDAC.HR_ReimburseStatusDetails(Convert.ToInt32(ViewState["EmpID"]), Status);
            if (Status == 3)
            {
                tblReject.Visible = true;
                tblView.Visible = true;
                tblCommon.Visible = false;
                gvShowRej.DataSource = ds;
                gvShowRej.DataBind();
            }
            else
            {
                tblReject.Visible = false;
                tblView.Visible = true;
                tblCommon.Visible = true;
                gvShow.DataSource = ds;
                gvShow.DataBind();
            }

        }
        protected void btnFilter_Click(object sender, EventArgs e)
        {
            ddlFilterEmp.SelectedIndex = -1;

            if (txtFilterEmpID.Text == "")
            {
                  
                string EmpName = txtFilterEmp.Text;
                DataSet ds = AttendanceDAC.HR_SerchEmp_Reimburse(EmpName, Convert.ToInt32(Session["CompanyID"]));
                ddlFilterEmp.DataSource = ds.Tables[1];
                ddlFilterEmp.DataTextField = "name";
                ddlFilterEmp.DataValueField = "EmpID";
                ddlFilterEmp.DataBind();
                ddlFilterEmp.Items.Insert(0, new ListItem("---SELECT EMPLOYEE---", "0", true));
            }
            else
            {
                txtEmpID = Convert.ToInt32(txtFilterEmpID.Text);
                BindPager();
                gvView.Visible = true;
                tblView.Visible = true;


            }
        }
    }
}
