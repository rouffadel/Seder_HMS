using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AECLOGIC.HMS.BLL;
using System.Data;
using System.Data.SqlClient;
using Aeclogic.Common.DAL;
using System.IO;
using System.Configuration;
using System.Collections;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using AECLOGIC.ERP.COMMON;


namespace AECLOGIC.ERP.HMS
{
    public partial class VacationConfig : AECLOGIC.ERP.COMMON.WebFormMaster
    {
        AttendanceDAC objAtt = new AttendanceDAC();
        HRCommon objHrCommon = new HRCommon();
        protected override void OnInit(EventArgs e)
        {
            ModuleID = 1;
            base.OnInit(e);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            Ajax.Utility.RegisterTypeForAjax(typeof(AjaxDAL));
            BindGrid();
        }
        private void BindGrid()
        {
            SqlParameter[] param = new SqlParameter[0];

            DataSet ds = SQLDBUtil.ExecuteDataset("sh_BindVacationConfig");
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                GridAccess.DataSource = ds.Tables[0];
                GridAccess.DataBind();
            }
            else
            {
                GridAccess.DataSource = null;
                GridAccess.DataBind();
            }
        }
        protected void GridAccess_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                CheckBox chkVS = (CheckBox)e.Row.Cells[2].Controls[1];
                CheckBox chkFS = (CheckBox)e.Row.Cells[3].Controls[1];
                chkVS.Attributes.Add("onclick", "javascript:return UpdateVS('" + ((System.Data.DataRowView)(e.Row.DataItem)).Row.ItemArray[0].ToString() + "',this );");
                chkFS.Attributes.Add("onclick", "javascript:return UpdateFS('" + ((System.Data.DataRowView)(e.Row.DataItem)).Row.ItemArray[0].ToString() + "',this );");
            }
        }
    }
}