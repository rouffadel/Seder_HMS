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
    public partial class WageAllowance : AECLOGIC.ERP.COMMON.WebFormMaster
    {
        int mid = 0;
        bool viewall;
        string menuname;
        string menuid;
        protected override void OnInit(EventArgs e)
        {
            ModuleID = 1;
            base.OnInit(e);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!IsPostBack)
            {
                GetParentMenuId();
                BindYears();
                ViewState["TCTC"] = "";
                ViewState["TActualpay"] = "";
                ViewState["TDiff"] = "";
            }
        }
        public int GetParentMenuId()
        {
            string URL = Request.Url.Segments[Request.Url.Segments.Length - 1] + Request.Url.Query;
            int RoleId = Convert.ToInt32(Session["RoleId"].ToString());
            int ModuleId = ModuleID;;

           

           DataSet ds = AttendanceDAC.GetAllowed(RoleId, ModuleId, URL);
            int MenuId = 0;
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                MenuId = Convert.ToInt32(ds.Tables[0].Rows[0]["Under"]);
               
                menuname = ds.Tables[0].Rows[0]["menuname"].ToString();
                menuid = MenuId.ToString();

                mid = Convert.ToInt32(ds.Tables[0].Rows[0]["MenuId"].ToString());

            }
            return MenuId;
        }
        public void BindYears()
        {
           
          DataSet  ds = AttendanceDAC.GetCalenderYear();

            int i = 0;
            int Maxyear = Convert.ToInt32(ds.Tables[0].Rows[0]["MaxYear"].ToString());
            for (int Minyear = Convert.ToInt32(ds.Tables[0].Rows[0]["MinYear"].ToString()); Minyear <= Maxyear; Minyear++)
            {
                ddlYear.Items.Insert(i, new ListItem(Convert.ToString(Minyear), Convert.ToString(Minyear)));
                i = i + 1;
            }
            ddlMonth.SelectedValue = "0";
            ddlYear.SelectedValue = ds.Tables[0].Rows[0]["CurrentYear"].ToString();

        }
        protected void ddlMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataSet ds = AttendanceDAC.HR_GetAllowanceWages(Convert.ToInt32(ddlMonth.SelectedValue), Convert.ToInt32(ddlYear.SelectedValue), Convert.ToInt32(Session["CompanyID"]));
            double TCTC = 0.0;
            double TActPay = 0.0;
            double TDiff = 0.0;
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                double CTC = Convert.ToDouble(ds.Tables[0].Rows[i]["CTC"].ToString());
                double ActPay = Convert.ToDouble(ds.Tables[0].Rows[i]["Actualpay"].ToString());
                double Diff = Convert.ToDouble(ds.Tables[0].Rows[i]["Diff"].ToString());
                TCTC += CTC;
                TActPay += ActPay;
                TDiff += Diff;
            }
            ViewState["TCTC"] = TCTC.ToString("N2");
            ViewState["TActualpay"] = TActPay.ToString("N2"); ;
            ViewState["TDiff"] = TDiff.ToString("N2"); ;
            if (ds != null && ds.Tables.Count != null && ds.Tables[0].Rows.Count > 0)
            {
                gvView.DataSource = ds;
            }
            else
            {
                gvView.EmptyDataText = "No Records Found";
            }
            gvView.DataBind();
        }
    }
}