using System;
using AECLOGIC.ERP.COMMON;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using Aeclogic.Common.DAL;
namespace AECLOGIC.ERP.HMS
{
    public partial class EmpReconSal : AECLOGIC.ERP.COMMON.WebFormMaster
    {
        bool Editable = false;
        bool ViewAll; string menuname; string menuid; int mid = 0; int modid = 0;
        static int Month, Year;
        protected override void OnInit(EventArgs e)
        {
            if (Request.QueryString.Count > 0)
            {
                modid = Convert.ToInt32(Request.QueryString["modid"].ToString());
                ModuleID = modid;
            }
            base.OnInit(e);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FIllObject.FillDropDown(ref ddlYear, "HMS_YearWise");
            }
        }
        protected void gvmnth_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Sel")
                {
                    GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).Parent.Parent;
                    int wsid = Convert.ToInt32(e.CommandArgument);
                    Label lblmonth = (Label)row.FindControl("lblmnth");
                    Label lblyear = (Label)row.FindControl("lblyear");
                    foreach (GridViewRow row1 in gvmnth.Rows)
                    {
                        LinkButton lnkdisplay = (LinkButton)row1.FindControl("lnkEdit");
                        lnkdisplay.CssClass = "btn btn-primary";
                    }
                    LinkButton lnkdisplaygrid = (LinkButton)row.FindControl("lnkEdit");
                    lnkdisplaygrid.CssClass = "btn btn-success";
                    lblincmng.Visible = true;
                    Label1.Visible = true;
                    Month = Convert.ToInt32(lblmonth.Text);
                    Year = Convert.ToInt32(lblyear.Text);
                    SqlParameter[] sqlParams = new SqlParameter[3];
                    sqlParams[0] = new SqlParameter("@Month", Convert.ToInt32(lblmonth.Text));
                    sqlParams[1] = new SqlParameter("@Year", Convert.ToInt32(lblyear.Text));
                    sqlParams[2] = new SqlParameter("@wsid", wsid);
                  DataSet   ds = SQLDBUtil.ExecuteDataset("sh_ReconEmpSalWSwise", sqlParams);
                    if (ds.Tables.Count > 0)
                    {
                        gvwswise.DataSource = ds.Tables[0];
                        gvout.DataSource = ds.Tables[1];
                    }
                    else
                    {
                        gvwswise.DataSource = null;
                        gvout.DataSource = null;
                    }
                    gvwswise.DataBind();
                    gvout.DataBind();
                }
            }
            catch { }
        }
        protected void btnsearch_Click(object sender, EventArgs e)
        {
            if (ddlMonth.SelectedIndex > 0 && ddlYear.SelectedIndex > 0)
            {
                SqlParameter[] sqlParams = new SqlParameter[2];
                sqlParams[0] = new SqlParameter("@Month", Convert.ToInt32(ddlMonth.SelectedValue));
                sqlParams[1] = new SqlParameter("@Year", Convert.ToInt32(ddlYear.SelectedValue));
                DataSet ds = SQLDBUtil.ExecuteDataset("sh_ReconEmpSal", sqlParams);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    gvmnth.DataSource = ds.Tables[0];
                }
                else
                    gvmnth.DataSource = null;
                gvmnth.DataBind();
            }
            else
            {
                AlertMsg.MsgBox(Page, "Please Select Month/Year !");
            }
        }
        protected void gvwswise_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "att")
            {
                int Empid = Convert.ToInt32(e.CommandArgument);
                Navigate(Empid);
            }
        }
        protected void gvout_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "att")
            {
                int Empid = Convert.ToInt32(e.CommandArgument);
                Navigate(Empid);
            }
        }
        void Navigate(int Empid)
        {
            string url = "EmpWorksiteMonthWise.aspx?Empid=" + Empid + "&Month=" + Month + "&Year=" + Year;
            string fullURL = "window.open('" + url + "', '_blank' );";
            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", fullURL, true);
        }
    }
}