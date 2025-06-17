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
using System.Data.SqlClient;
using Aeclogic.Common.DAL;
using AECLOGIC.HMS.BLL;
using System.IO;
namespace AECLOGIC.ERP.HMS
{
    public partial class DirectPO : AECLOGIC.ERP.COMMON.WebFormMaster
    {
        bool viewall;
        bool Editable; string menuname; string menuid; int mid = 0; int Worksite = 0;

        private GridSort objSort;
        private DataSet dsGoodsGroup;
        protected override void OnInit(EventArgs e)
        {
            ModuleID = 1;
            base.OnInit(e);
        }
        protected void Page_Load(object sender, EventArgs e)
        {

            DataTable dtGoodsList = new DataTable();
            if (!IsPostBack)
            {
                GetParentMenuId();
                FIllObject.FillGridview(ref GVTERMS, "PM_GET_Terms");
                FIllObject.FillDropDown(ref ddlForProject, "PM_GetWorkSites");
                FIllObject.FillDropDown(ref ddlPayment, "PM_BillGenType");
            }
            Worksite = Convert.ToInt32(ddlForProject.SelectedValue);
        }



        protected void GVTERMS_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                ((CheckBox)e.Row.FindControl("cbSelectAll")).Attributes.Add("onclick", "javascript:SelectAll('" + ((CheckBox)e.Row.FindControl("cbSelectAll")).ClientID + "')");
            }
        }


        protected void btnAddNewTerm_Click(object sender, EventArgs e)
        {

            SqlParameter[] parms = new SqlParameter[3];
            parms[0] = new SqlParameter("@ID", 3);

            parms[1] = new SqlParameter("@VenId", 1); // TvIndent.SelectedValue);
            parms[2] = new SqlParameter("@EnqId", 1); // TvIndent.SelectedNode.Parent.Value);
            SqlHelper.ExecuteNonQuery("PM_INSERTREMARKS", parms);


            foreach (GridViewRow row in GVAdditionalTerms.Rows)
            {
                TextBox txtterm = new TextBox();
                txtterm = (TextBox)row.FindControl("TXTTERMS");
                if (txtterm.Text.Trim() != "")
                {
                    SqlParameter[] parms1 = new SqlParameter[4];
                    parms1[0] = new SqlParameter("@ID", 1);
                    parms1[1] = new SqlParameter("@Remarks", txtterm.Text);
                    parms1[2] = new SqlParameter("@VenId", 1); // TvIndent.SelectedValue);
                    parms1[3] = new SqlParameter("@EnqId", 1); // TvIndent.SelectedNode.Parent.Value);
                    SqlHelper.ExecuteNonQuery("PM_INSERTREMARKS", parms1);
                }
            }


            parms = new SqlParameter[4];
            parms[0] = new SqlParameter("@ID", 1);
            parms[1] = new SqlParameter("@Remarks", TxtTerms.Text);
            parms[2] = new SqlParameter("@VenId", 1); //TvIndent.SelectedValue);
            parms[3] = new SqlParameter("@EnqId", 1); //TvIndent.SelectedNode.Parent.Value);
            SqlHelper.ExecuteNonQuery("PM_INSERTREMARKS", parms);

            parms = new SqlParameter[4];
            parms[0] = new SqlParameter("@ID", 2);
            parms[1] = new SqlParameter("@Remarks", TxtTerms.Text);
            parms[2] = new SqlParameter("@VenId", 1); // TvIndent.SelectedValue);
            parms[3] = new SqlParameter("@EnqId", 1); // TvIndent.SelectedNode.Parent.Value);
            FIllObject.FillGridview(ref GVAdditionalTerms, "PM_INSERTREMARKS", parms);

            TxtTerms.Text = string.Empty;


        }

       
        protected void RbSetList_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (GridViewRow row in GVTERMS.Rows)
            {
                CheckBox chk = new CheckBox();
                chk = (CheckBox)row.FindControl("chk");
                chk.Checked = false;
            }

             
            SqlParameter[] p = new SqlParameter[2];
            p[0] = new SqlParameter("@ID", 3);
            p[1] = new SqlParameter("@SETID", RbSetList.SelectedValue);
            DataSet ds = SqlHelper.ExecuteDataset("PM_TERMSET", p);
            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (GridViewRow row in GVTERMS.Rows)
                {
                    CheckBox chk = new CheckBox();
                    Label lbltermid = new Label();
                    chk = (CheckBox)row.FindControl("chk");
                    lbltermid = (Label)row.FindControl("lblid");

                    foreach (DataRow Drow in ds.Tables[0].Rows)
                    {
                        if (Drow[0].ToString() == lbltermid.Text)
                        {
                            chk.Checked = true;
                        }
                    }
                }
            }

        }
        public int GetParentMenuId()
        {
            string URL = Request.Url.Segments[Request.Url.Segments.Length - 1] + Request.Url.Query;
            int RoleId = Convert.ToInt32(Session["RoleId"].ToString());
            int ModuleId = ModuleID;;

             
            ProcDept objProc = new ProcDept();
            DataSet ds = ProcDept.GetAllowed(RoleId, ModuleId, URL);
            int MenuId = 0;
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                MenuId = Convert.ToInt32(ds.Tables[0].Rows[0]["Under"]);
                Editable = (bool)ds.Tables[0].Rows[0]["Editable"];

            }
            return MenuId;
        }
    }
}