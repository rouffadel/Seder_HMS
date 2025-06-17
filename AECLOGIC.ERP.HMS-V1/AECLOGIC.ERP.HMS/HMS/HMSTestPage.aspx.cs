using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AECLOGIC.ERP.HMS.HMS
{
    public partial class HMSTestPage : AECLOGIC.ERP.COMMON.WebFormMaster
    {


        protected override void OnInit(EventArgs e)
        {
            ModuleID = 1;
            base.OnInit(e);
            //EmpListPaging.FirstClick += new Paging.PageFirst(EmpListPaging_FirstClick);
            //EmpListPaging.PreviousClick += new Paging.PagePrevious(EmpListPaging_FirstClick);
            //EmpListPaging.NextClick += new Paging.PageNext(EmpListPaging_FirstClick);
            //EmpListPaging.LastClick += new Paging.PageLast(EmpListPaging_FirstClick);
            //EmpListPaging.ChangeClick += new Paging.PageChange(EmpListPaging_FirstClick);
            //EmpListPaging.ShowRowsClick += new Paging.ShowRowsChange(EmpListPaging_ShowRowsClick);
            //EmpListPaging.CurrentPage = 1;
        }

        
    }
}