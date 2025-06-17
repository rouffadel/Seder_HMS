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
    public partial class EmpPFAccount : AECLOGIC.ERP.COMMON.WebFormMaster
    {
        AttendanceDAC objEmployee = new AttendanceDAC();
        int? EmpID = null;
        protected override void OnInit(EventArgs e)
        {
            ModuleID = 1;
            base.OnInit(e);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                ViewState["PFID"] = null;
                BindPFCode();
                BindPFAccountDetails();
            }
        }

        #region Bind
        void BindPFCode()
        {
            DataSet ds = AttendanceDAC.GetPFDetails();
            ddlPFRegCode.DataSource = ds;
            ddlPFRegCode.DataTextField = "Code";
            ddlPFRegCode.DataValueField = "PFID";
            ddlPFRegCode.DataBind();
            ddlPFRegCode.Items.Insert(0, new ListItem("---ALL---", "0"));
        }

        #endregion Bind

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            int? PFID = null;
            if (ViewState["PFID"] != null)
                PFID = Convert.ToInt32(ViewState["PFID"]);
            int OfficeID = Convert.ToInt32(ddlPFRegCode.SelectedValue);
            int AccountNO = Convert.ToInt32(txtAccount.Text);

            if (Request.QueryString.Count > 0)
                EmpID = Convert.ToInt32(Request.QueryString["id"].ToString());
            int CreatedBy =  Convert.ToInt32(Session["UserId"]);

            int Output = AttendanceDAC.InsUpdGetPFAccount(PFID, OfficeID, AccountNO, EmpID, CreatedBy);
            if (Output == 1)
                AlertMsg.MsgBox(Page, "Done.!");
            else
                AlertMsg.MsgBox(Page, "Updated.!");


        }
        public void BindPFAccountDetails()
        {
            if (Request.QueryString.Count > 0)
                EmpID = Convert.ToInt32(Request.QueryString["id"].ToString());
            DataSet dsPF = AttendanceDAC.GetPFAccountDetails(EmpID);
            if (dsPF != null && dsPF.Tables[0].Rows.Count > 0)
            {
                txtAccount.Text = dsPF.Tables[0].Rows[0]["AccountNO"].ToString();
                ddlPFRegCode.SelectedValue = dsPF.Tables[0].Rows[0]["OfficeID"].ToString();
                ViewState["PFID"] = dsPF.Tables[0].Rows[0]["PFID"].ToString();
            }

        }

    }
}