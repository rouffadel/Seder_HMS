using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using AECLOGIC.HMS.BLL;
using System.Configuration;
namespace AECLOGIC.ERP.HMS
{
    public partial class EditgenEmpDocuments : AECLOGIC.ERP.COMMON.WebFormMaster
    {
        AttendanceDAC objDoc = new AttendanceDAC();
        HRCommon objHrCommon = new HRCommon();
        string strUrl = string.Empty;
        protected override void OnInit(EventArgs e)
        {
            ModuleID = 1;
            base.OnInit(e);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                strUrl = Request.UrlReferrer.ToString();
                int DocID = Convert.ToInt32(Request.QueryString["DocID"]);
                int EmpID = Convert.ToInt32(Request.QueryString["EmpID"]);
                BindDocument(DocID, EmpID);
            }
        }
     
        public void BindDocument(int DocID, int EmpID)
        {
            int EmpDocID = Convert.ToInt32(Request.QueryString["EmpDocID"]);
          DataSet  ds = AttendanceDAC.HR_GetDocumentDetailsByEmpDocID(DocID, EmpID, EmpDocID);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                txtAppDate.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["IssueDate"]).ToString(ConfigurationManager.AppSettings["DateFormat"]);
                DocEditor.Text = ds.Tables[0].Rows[0]["Value"].ToString();
            }
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            int EmpId = Convert.ToInt32(Request.QueryString["EmpId"]);
            int DocID = Convert.ToInt32(Request.QueryString["DocID"]);

            string DocName = Request.QueryString["DocName"].ToString();
            int EmpDocID = Convert.ToInt32(Request.QueryString["EmpDocID"]);
            DataSet dsEmpDetails = objDoc.GetAppDetails(EmpId);


            string StrText = DocEditor.Text.Replace("[DESIGNATION]", dsEmpDetails.Tables[0].Rows[0]["Designation"].ToString()).Replace("[EMPLOYEE NAME]", dsEmpDetails.Tables[0].Rows[0]["name"].ToString()).Replace("[ADDRESS]", dsEmpDetails.Tables[0].Rows[0]["ResAddress"].ToString().Replace("\n", "<br/>"))
                .Replace("[WORKSITE]", dsEmpDetails.Tables[0].Rows[0]["Site_Name"].ToString()).Replace("[DEPARTMENT]", dsEmpDetails.Tables[0].Rows[0]["DepartmentName"].ToString()).Replace("[COMPANY NAME]", ConfigurationSettings.AppSettings["Company"].ToString()).Replace("[DATE]", DateTime.Now.ToString("dd/MM/yyyy"));


            AttendanceDAC.AddUpdateEmpDocsGeneral(EmpId, DocID, 1, StrText, CODEUtility.ConvertToDate(txtAppDate.Text.Trim(), DateFormat.DayMonthYear), DocName, EmpDocID);
            Response.Redirect("empdocuments.aspx?eid=" + EmpId);
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("EmployeeList.aspx");

        }
    }
}