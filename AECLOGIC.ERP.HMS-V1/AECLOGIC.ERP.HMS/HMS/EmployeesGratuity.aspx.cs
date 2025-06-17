using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using AECLOGIC.HMS.BLL;
using AECLOGIC.ERP.COMMON;




namespace AECLOGIC.ERP.HMS
{


    public partial class EmployeesGratuity : AECLOGIC.ERP.COMMON.WebFormMaster
    {
        HRCommon objHrCommon = new HRCommon();
        int mid = 0;
        bool viewall;
        string menuname;
        string menuid;
        bool Editable;

        protected void Page_Load(object sender, EventArgs e)
        {
           
            if (!IsPostBack)
            {
                GetParentMenuId();
                BindGrid();
            }
        }

        public int GetParentMenuId()
        {
            string URL = Request.Url.Segments[Request.Url.Segments.Length - 1] + Request.Url.Query;
            int RoleId = Convert.ToInt32(Session["RoleId"].ToString());
            int ModuleId = ModuleID;;
          DataSet  ds = AttendanceDAC.GetAllowed(RoleId, ModuleId, URL);
            int MenuId = 0;
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                MenuId = Convert.ToInt32(ds.Tables[0].Rows[0]["Under"]);
                gvEmployeesGratuity.Columns[2].Visible = Editable = Convert.ToBoolean(ds.Tables[0].Rows[0]["Editable"].ToString());
            }
            return MenuId;
        }

        protected override void OnInit(EventArgs e)
        {
            ModuleID = 1;
            base.OnInit(e);
            AllowancePaging.FirstClick += new Paging.PageFirst(AllowancePaging_FirstClick);
            AllowancePaging.PreviousClick += new Paging.PagePrevious(AllowancePaging_FirstClick);
            AllowancePaging.NextClick += new Paging.PageNext(AllowancePaging_FirstClick);
            AllowancePaging.LastClick += new Paging.PageLast(AllowancePaging_FirstClick);
            AllowancePaging.ChangeClick += new Paging.PageChange(AllowancePaging_FirstClick);
            AllowancePaging.ShowRowsClick += new Paging.ShowRowsChange(AllowancePaging_ShowRowsClick);
            AllowancePaging.CurrentPage = 1;
        }

        void AllowancePaging_ShowRowsClick(object sender, EventArgs e)
        {
            AllowancePaging.CurrentPage = 1;
            BindPager();
        }

        void AllowancePaging_FirstClick(object sender, EventArgs e)
        {
            BindPager();
        }

        void BindPager()
        {

            objHrCommon.PageSize = AllowancePaging.CurrentPage;
            objHrCommon.CurrentPage = AllowancePaging.ShowRows;
            BindGrid();
        }

        void BindGrid()
        {
            try
            {
                AllowancePaging.Visible = true;
                objHrCommon.PageSize = AllowancePaging.ShowRows;
                objHrCommon.CurrentPage = AllowancePaging.CurrentPage;
                //int EmpID =  Convert.ToInt32(Session["UserId"]);
                  
              DataSet  ds = PayRollMgr.EmployeeGratuityListByPaging(objHrCommon);
                if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0)
                {
                    gvEmployeesGratuity.DataSource = ds;
                    gvEmployeesGratuity.DataBind();
                }
                else
                    AllowancePaging.Visible = false;
            }
            catch { }
        }
    }

}