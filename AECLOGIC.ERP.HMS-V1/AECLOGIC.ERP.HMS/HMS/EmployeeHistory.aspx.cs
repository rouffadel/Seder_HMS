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
    public partial class EmployeeHistory : AECLOGIC.ERP.COMMON.WebFormMaster
    {
        #region Declaration

        int OrderID = 0, Direction = 0;
        int mid = 0;
        bool viewall;
        string menuname;
        string menuid;
        string ReturnVal = "";
        HRCommon objHrCommon = new HRCommon();

        #endregion Declaration
        #region Pagin
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            EmpHisPaging.FirstClick += new Paging.PageFirst(EmpHisPaging_FirstClick);
            EmpHisPaging.PreviousClick += new Paging.PagePrevious(EmpHisPaging_FirstClick);
            EmpHisPaging.NextClick += new Paging.PageNext(EmpHisPaging_FirstClick);
            EmpHisPaging.LastClick += new Paging.PageLast(EmpHisPaging_FirstClick);
            EmpHisPaging.ChangeClick += new Paging.PageChange(EmpHisPaging_FirstClick);
            EmpHisPaging.ShowRowsClick += new Paging.ShowRowsChange(EmpHisPaging_ShowRowsClick);
            EmpHisPaging.CurrentPage = 1;
        }
        void EmpHisPaging_ShowRowsClick(object sender, EventArgs e)
        {
            EmpHisPaging.CurrentPage = 1;
            BindPager();
        }
        void EmpHisPaging_FirstClick(object sender, EventArgs e)
        {
            BindPager();
        }
        void BindPager()
        {
            objHrCommon.PageSize = EmpHisPaging.CurrentPage;
            objHrCommon.CurrentPage = EmpHisPaging.ShowRows;
            GetEmpDetails(objHrCommon);
        }
        #endregion Paging
        #region PageLoad
        protected void Page_Load(object sender, EventArgs e)
        {
           
            if (!IsPostBack)
            {
                BindPager();
            }
        }
        #endregion PageLoad
        #region SupportingMethods
      
        #endregion SupportingMethods

        public void GetEmpDetails(HRCommon objHrCommon)
        {
            try
            {
                int? EmpID = null;

                objHrCommon.PageSize = EmpHisPaging.ShowRows;
                objHrCommon.CurrentPage = EmpHisPaging.CurrentPage;

                DataSet dsEmpHis = AttendanceDAC.GetEmpHisDetails(objHrCommon, OrderID, Direction, 'y', EmpID);

                if (dsEmpHis != null && dsEmpHis.Tables.Count != 0 && dsEmpHis.Tables[0].Rows.Count > 0)
                {
                    grvEmpHistory.DataSource = dsEmpHis;
                    grvEmpHistory.DataBind();
                }
                EmpHisPaging.Bind(objHrCommon.CurrentPage, objHrCommon.TotalPages, objHrCommon.NoofRecords, objHrCommon.PageSize);

            }
            catch (Exception e)
            {
                throw e;
            }
        }
        protected void grvEmpHistory_OnSelectedIndexChanged(object sender, EventArgs e)
        {
        }
        protected void grvEmpHistory_Sorting(object sender, GridViewSortEventArgs e)
        {
            switch (e.SortExpression)
            {
                case "Name":
                    OrderID = 0;
                    Direction = 0;
                    break;
                case "PAN":
                    OrderID = 1;
                    Direction = 1;
                    break;
            }
            BindPager();
        }
    }
}