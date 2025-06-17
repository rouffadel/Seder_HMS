using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AECLOGIC.HMS.BLL;
using System.Data;
using System.Data.SqlClient;

namespace AECLOGIC.ERP.HMS
{
    public partial class ExceRptInOutTimes : AECLOGIC.ERP.COMMON.WebFormMaster
    {
        int mid = 0;
        bool viewall;
        string menuname;
        string menuid;
        static DateTime? Date;
        static int? Month;
        static int? Year;
        static int WHSType;
        AttendanceDAC objAtt = new AttendanceDAC();
        protected override void OnInit(EventArgs e)
        {
            ModuleID = 1;
            base.OnInit(e);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindYears();
            }
        }

    

        public void BindYears()
        {
              
          DataSet  ds = AttendanceDAC.GetCalenderYear();

            int i = 0;
            int Maxyear = Convert.ToInt32(ds.Tables[0].Rows[0]["MaxYear"].ToString());
            for (int Minyear = Convert.ToInt32(ds.Tables[0].Rows[0]["MinYear"].ToString()); Minyear <= Maxyear; Minyear++)
            {
                ddlYear.Items.Insert(0, new ListItem(Convert.ToString(Minyear), Convert.ToString(Minyear)));
                i = i + 1;
            }
            ddlMonth.SelectedValue = ds.Tables[0].Rows[0]["CurrentMonth"].ToString();
            ddlYear.SelectedValue = ds.Tables[0].Rows[0]["CurrentYear"].ToString();
        }

        protected void btnMonthReport_Click(object sender, EventArgs e)
        {
            Date = null;
            if (ddlMonth.SelectedItem.Value != "0")
            {
                Month = Convert.ToInt32(ddlMonth.SelectedItem.Value);
            }
            if (ddlYear.SelectedItem.Value != "0")
            {
                Year = Convert.ToInt32(ddlYear.SelectedItem.Value);
            }
            BindGrid();
        }

        public void BindGrid()
        {
              
            int? EmpID = null;
            if (txtEmpID.Text.Trim() != "")
            {
                EmpID = Convert.ToInt32(txtEmpID.Text);
            }
            DataSet ds = ExceReports.ExceRptInOutTimesByEmpID(Month, Year, EmpID, txtEmpName.Text);
            grdAllEmployees.DataSource = ds;
            grdAllEmployees.DataBind();
        }
    }
}