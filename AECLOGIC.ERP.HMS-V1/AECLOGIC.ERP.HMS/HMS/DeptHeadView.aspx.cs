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
    public partial class DeptHeadView : AECLOGIC.ERP.COMMON.WebFormMaster
    {
        AttendanceDAC ADAC = new AttendanceDAC();
        protected override void OnInit(EventArgs e)
        {
            ModuleID = 1;
            base.OnInit(e);
        }
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                GetWorkSites(0);
                ddlWSSearch.SelectedValue = "1";
                BindGrid();

            }


        }
        private void GetWorkSites(int SiteID)
        {

            DataSet ds = AttendanceDAC.GetWorkSite(SiteID, '1', Convert.ToInt32(Session["CompanyID"]));
            ddlWSSearch.DataSource = ds.Tables[0];
            ddlWSSearch.DataTextField = "Site_Name";
            ddlWSSearch.DataValueField = "Site_ID";
            ddlWSSearch.DataBind();
            ddlWSSearch.Items.Insert(0, new ListItem("--ALL--", "0"));
        }
        private void BindGrid()
        {
            DataSet dsHeads = ADAC.GetDeptHeadsAll(Convert.ToInt32(ddlWSSearch.SelectedValue), 0);
            gdvWS.DataSource = dsHeads.Tables[0];
            gdvWS.DataBind();

        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindGrid();
        }
        protected void ddlWS_SelectedIndexChanged(object sender, EventArgs e)
        {

            DataSet ds = ADAC.GetDeptHeads(Convert.ToInt32(ddlWSSearch.SelectedValue), 0);
            gdvWS.DataSource = ds.Tables[0];
            gdvWS.DataBind();
        }
    }
}