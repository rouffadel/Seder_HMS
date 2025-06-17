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
    public partial class TodoListInfo : AECLOGIC.ERP.COMMON.WebFormMaster
    {

        public void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindNS();
                BindCompleted();
                BindDF();
                BindNF();

            }
        }
        protected override void OnInit(EventArgs e)
        {
            ModuleID = 1;
            base.OnInit(e);
        }
        public void BindNS()
        {
            int EmpD = 83;//
            DataSet ds = AttendanceDAC.HR_GetNotStarted(EmpD);
            gvNS.DataSource = ds;
            gvNS.DataBind();
        }
        public void BindCompleted()
        {
            int EmpD = 83;//
            DataSet ds = AttendanceDAC.HR_TaskCompleted(EmpD);
            gvTC.DataSource = ds;
            gvTC.DataBind();
        }
        public void BindDF()
        {
            int EmpD = 83;//
            DataSet ds = AttendanceDAC.HR_DueDateFinished(EmpD);
            gvDF.DataSource = ds;
            gvDF.DataBind();
        }
        public void BindNF()
        {
            int EmpD = 83;//
            DataSet ds = AttendanceDAC.HR_NotUpdatingTask(EmpD);
            gvNU.DataSource = ds;
            gvNU.DataBind();
        }
    }
}
