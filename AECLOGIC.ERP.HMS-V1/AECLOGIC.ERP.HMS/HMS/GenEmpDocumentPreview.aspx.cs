using System;
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
    public partial class GenEmpDocumentPreview : AECLOGIC.ERP.COMMON.WebFormMaster
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
                int EmpID = Convert.ToInt32(Request.QueryString["EmpID"]);
                int EmpDocID = Convert.ToInt32(Request.QueryString["EmpDocID"]);
                  
              DataSet  ds = objDoc.GetAppDetails(EmpID);

                lblName.Text = ds.Tables[0].Rows[0]["name"].ToString();
                lblAddress.Text = ds.Tables[0].Rows[0]["ResAddress"].ToString().Replace("\n", "<br/>");
                lblCity.Text = ds.Tables[0].Rows[0]["ResCity"].ToString();
                lblState.Text = ds.Tables[0].Rows[0]["ResState"].ToString();
                lblCountry.Text = ds.Tables[0].Rows[0]["ResCountry"].ToString();
                lblPin.Text = " - " + ds.Tables[0].Rows[0]["ResPIN"].ToString();
                if (ds.Tables[0].Rows[0]["ResPhone"].ToString() != "")
                {
                    lblPhone.Text = "Mobile : " + ds.Tables[0].Rows[0]["ResPhone"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Mailid"].ToString() != "")
                {
                    lblEmail.Text = "Email : " + ds.Tables[0].Rows[0]["Mailid"].ToString();
                }
                BindDocument(DocID, EmpID, EmpDocID);
            }
        }

        public void BindDocument(int DocID, int EmpID)
        {
            DataSet ds = AttendanceDAC.GetDocumentDetails(DocID, EmpID);

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                //lbldocTYpe.Text = ds.Tables[0].Rows[0]["DocName"].ToString();
                TextEditor.InnerHtml = ds.Tables[0].Rows[0]["Value"].ToString();
            }
        }
        public void BindDocument(int DocID, int EmpID, int EmpDocID)
        {
            DataSet ds = AttendanceDAC.HR_GetDocumentDetailsByEmpDocID(DocID, EmpID, EmpDocID);

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                //lbldocTYpe.Text = ds.Tables[0].Rows[0]["DocName"].ToString();
                TextEditor.InnerHtml = ds.Tables[0].Rows[0]["Value"].ToString();
            }
        }
    }
}
