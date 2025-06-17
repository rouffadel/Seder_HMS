using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using AECLOGIC.HMS.BLL;

namespace AECLOGIC.ERP.HMS
{
    public partial class PassportVisa : AECLOGIC.ERP.COMMON.WebFormMaster
    {
        #region Declaration
        int mid = 0;
        bool viewall;
        string menuname;
        string menuid;
        HRCommon objHrCommon = new HRCommon();
        AttendanceDAC objatt = new AttendanceDAC();
        #endregion Declaration
        protected override void OnInit(EventArgs e)
        {
            ModuleID = 1;
            base.OnInit(e);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
           
            if (!IsPostBack)
            {
                BindGrid();
                ViewState["ID"] = "";
            }
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            int ID = 0;
            if (ViewState["ID"].ToString() != null && ViewState["ID"].ToString() != string.Empty)
            {
                ID = Convert.ToInt32(ViewState["ID"].ToString());
            }
            int returnval;

            returnval = Convert.ToInt32(AttendanceDAC.InsUpdNRIDocTypes(ID, txtDocName.Text.Trim(),  Convert.ToInt32(Session["UserId"])));

            BindGrid();

            if (returnval == 1 || returnval == 2)
            {
                AlertMsg.MsgBox(Page, "Done ");
            }
            else
            {
                AlertMsg.MsgBox(Page, "Check Dates");
            }
            ViewState["ID"] = "";
            txtDocName.Text = "";
        }

        public void BindGrid()
        {
            DataSet DsDoc = AttendanceDAC.GetNRIDocTypes(null);
            gvDocs.DataSource = DsDoc;
            gvDocs.DataBind();
        }
        protected void gvDocs_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int ID = Convert.ToInt32(e.CommandArgument);
            ViewState["ID"] = ID;
            if (e.CommandName == "Edt")
            {
                BindDetails(ID);
            }
        }

        public void BindDetails(int ID)
        {
            DataSet DsDocDets = AttendanceDAC.GetNRIDocTypes(ID);
            txtDocName.Text = DsDocDets.Tables[0].Rows[0]["DocName"].ToString();
        }
    }
}