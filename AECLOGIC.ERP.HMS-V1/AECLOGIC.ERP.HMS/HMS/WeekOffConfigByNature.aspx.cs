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
    public partial class WeekOffConfigByNature : AECLOGIC.ERP.COMMON.WebFormMaster
    {
        int mid = 0;
        bool viewall;
        string menuname;
        string menuid;
        static string strurl = string.Empty;
        protected override void OnInit(EventArgs e)
        {
            ModuleID = 1;
            base.OnInit(e);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            Ajax.Utility.RegisterTypeForAjax(typeof(AjaxDAL));
            if (!IsPostBack)
            {
                GetParentMenuId();
                BindEmpNature();
                ddlEmpNature.SelectedValue = "1";
                BindGrid(1);
            }
        }
        public void BindGrid(int Nature)
        {
            DataSet ds = AttendanceDAC.HR_GetWOConfigByEmpNature(Nature);
            string[] strWeeks = new string[] { "SUN", "MON", "TUE", "WED", "THU", "FRI", "SAT" };
            for (int i = 1; i <= strWeeks.Length; i++)
            {
                for (int j = 1; j <= 5; j++)
                {
                    DataRow[] drS = ds.Tables[0].Select("WeekNo='" + j.ToString() + "' AND Day='" + i.ToString() + "'");
                    string chk = "chk" + j.ToString() + strWeeks[i - 1];
                    CheckBox chkbox = (CheckBox)tblWeeks.FindControl(chk);//"chk1SUN"
                    if (drS.Length > 0)
                        chkbox.Checked = true;
                    else
                        chkbox.Checked = false;
                }
            }
        }
        public void BindEmpNature()
        {
            DataSet ds = AttendanceDAC.HR_GetEmpNature();
            ddlEmpNature.DataSource = ds;
            ddlEmpNature.DataValueField = "NatureofEmp";
            ddlEmpNature.DataTextField = "Nature";
            ddlEmpNature.DataBind();
            ddlEmpNature.Items.Insert(0, new ListItem("---SELECT---", "0", true));
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
        protected void ddlEmpNature_SelectedIndexChanged(object sender, EventArgs e)
        {
            int EmpNature = Convert.ToInt32(ddlEmpNature.SelectedValue);
            BindGrid(EmpNature);
        }
    }
}