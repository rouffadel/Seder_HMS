using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using AECLOGIC.HMS.BLL;
namespace AECLOGIC.ERP.HMS
{
    public partial class EmpdocumentsEditing : AECLOGIC.ERP.COMMON.WebFormMaster
    {
          AttendanceDAC objDoc = new AttendanceDAC();
        HRCommon objHrCommon = new HRCommon();
         
        protected override void OnInit(EventArgs e)
        {
            ModuleID = 1;
            base.OnInit(e);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
           
            if (!IsPostBack)
            {
                int DocID = Convert.ToInt32(Request.QueryString["DocID"]);
                BindDocument(DocID);
            }
        }
       
        public void BindDocument(int DocID)
        {
          DataSet  ds = AttendanceDAC.GetDocumentDetails(DocID, 0);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                int EmpId = Convert.ToInt32(Request.QueryString["EmpId"]);
                DataSet EmpDs = objDoc.GetEmployeeDetails(EmpId);
                if (EmpDs != null && EmpDs.Tables.Count > 0 && EmpDs.Tables[0].Rows.Count > 0)
                {
                    string str = ds.Tables[0].Rows[0]["Value"].ToString();
                    DocEditor.Text = str.Replace("[EMPLOYEE NAME]", EmpDs.Tables[0].Rows[0]["EmpName"].ToString()).Replace("[DESIGNATION]", EmpDs.Tables[0].Rows[0]["Designation"].ToString()).Replace("[ADDRESS]", EmpDs.Tables[0].Rows[0]["ResAddress"].ToString()).Replace("[name]", EmpDs.Tables[0].Rows[0]["EmpName"].ToString())
                .Replace("[DATE]", DateTime.Now.ToString("dd/MM/yyyy")).Replace("[WORKSITE]", EmpDs.Tables[0].Rows[0]["Site_Name"].ToString()).Replace("[DEPARTMENT]", EmpDs.Tables[0].Rows[0]["DepartmentName"].ToString()).Replace("[COMPANY NAME]", ConfigurationSettings.AppSettings["Company"].ToString());
                }
                else
                {
                    AlertMsg.MsgBox(Page, "No RecordsFound");

                }
            }
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            int EmpId = Convert.ToInt32(Request.QueryString["EmpId"]);
            int DocID = Convert.ToInt32(Request.QueryString["DocID"]);
            string DocName = Request.QueryString["DocName"].ToString();
            int EmpDocID = 0;

            AttendanceDAC.AddUpdateEmpDocsGeneral(EmpId, DocID, 1, DocEditor.Text, CODEUtility.ConvertToDate(txtAppDate.Text.Trim(), DateFormat.DayMonthYear), DocName, EmpDocID);
            Response.Redirect("empdocuments.aspx?eid=" + EmpId);
        }
    }
}