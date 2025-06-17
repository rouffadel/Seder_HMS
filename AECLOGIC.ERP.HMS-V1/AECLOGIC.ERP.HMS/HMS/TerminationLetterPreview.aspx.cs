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

namespace AECLOGIC.ERP.HMS
{
    public partial class TerminationLetterPreview : AECLOGIC.ERP.COMMON.WebFormMaster
    {
        AttendanceDAC objApp = new AttendanceDAC();
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
                DataSet ds;

                if (Request.QueryString.Count > 0)
                {
                    ds = new DataSet();
                    ds = AttendanceDAC.HR_EmpTermination(Convert.ToInt32(Request.QueryString["EmpID"]));

                    if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
                    {
                        lblEmp.Text = Request.QueryString["EmpID"].ToString();
                        lblName.Text = ds.Tables[0].Rows[0]["name"].ToString();
                        lblAddress.Text = ds.Tables[0].Rows[0]["ResAddress"].ToString();
                        lblCity.Text = ds.Tables[0].Rows[0]["ResCity"].ToString();
                        lblState.Text = ds.Tables[0].Rows[0]["ResState"].ToString();
                        lblCountry.Text = ds.Tables[0].Rows[0]["ResCountry"].ToString(); ;
                        lblPin.Text = " - " + ds.Tables[0].Rows[0]["ResPin"].ToString(); ;
                        lblPhone.Text = ds.Tables[0].Rows[0]["Mobile"].ToString();
                        lblEmpName.Text = ds.Tables[0].Rows[0]["name"].ToString();
                        

                        lblDate.Text = "Date : " + DateTime.Now.GetDateTimeFormats()[10];

                        

                    }
                }
            }
        }
        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("EmpTermination.aspx");
        }
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            Response.Redirect("Documents.aspx?key=2");
        }
    }
}
