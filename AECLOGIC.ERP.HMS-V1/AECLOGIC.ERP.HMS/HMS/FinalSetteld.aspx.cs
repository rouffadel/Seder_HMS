using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccessLayer;
using AECLOGIC.ERP.COMMON;
using Aeclogic.Common.DAL;
using System.Data.SqlClient;
using AECLOGIC.HMS.BLL;
using System.Data;
using System.Collections;
using System.IO;

namespace AECLOGIC.ERP.HMS
{
    public partial class FinalSetteld : AECLOGIC.ERP.COMMON.WebFormMaster
    {
        int mid = 0;
        bool viewall;
        string menuname, Ext;
        string menuid;
        HRCommon objHrCommon = new HRCommon();

        protected override void OnInit(EventArgs e)
        {
            ModuleID = 1;
            base.OnInit(e);


            AdvancedLeaveAppOthPaging.FirstClick += new Paging.PageFirst(AdvancedLeaveAppOthPaging_FirstClick);
            AdvancedLeaveAppOthPaging.PreviousClick += new Paging.PagePrevious(AdvancedLeaveAppOthPaging_FirstClick);
            AdvancedLeaveAppOthPaging.NextClick += new Paging.PageNext(AdvancedLeaveAppOthPaging_FirstClick);
            AdvancedLeaveAppOthPaging.LastClick += new Paging.PageLast(AdvancedLeaveAppOthPaging_FirstClick);
            AdvancedLeaveAppOthPaging.ChangeClick += new Paging.PageChange(AdvancedLeaveAppOthPaging_FirstClick);
            AdvancedLeaveAppOthPaging.ShowRowsClick += new Paging.ShowRowsChange(AdvancedLeaveAppOthPaging_ShowRowsClick);
            AdvancedLeaveAppOthPaging.CurrentPage = 1;
        }

        void AdvancedLeaveAppOthPaging_ShowRowsClick(object sender, EventArgs e)
        {
            AdvancedLeaveAppOthPaging.CurrentPage = 1;
            BindGrid();
        }
        void AdvancedLeaveAppOthPaging_FirstClick(object sender, EventArgs e)
        {
            BindGrid();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
           
            if (!IsPostBack)
            {
                BindGrid();
            }
        }

   

        public void BindGrid()
        {
            objHrCommon.PageSize = AdvancedLeaveAppOthPaging.ShowRows;
            objHrCommon.CurrentPage = AdvancedLeaveAppOthPaging.CurrentPage;

            int empid = 0;
            if (txtEmpName.Text != "" || txtEmpName.Text != null)
                empid = Convert.ToInt32(txtEmpNameHidden.Value == "" ? "0" : txtEmpNameHidden.Value);
            else
                txtEmpNameHidden.Value = String.Empty;

             

            SqlParameter[] sqlParams = new SqlParameter[5];
            sqlParams[4] = new SqlParameter("@Empid", empid);
            sqlParams[0] = new SqlParameter("@CurrentPage", objHrCommon.CurrentPage);
            sqlParams[1] = new SqlParameter("@PageSize", objHrCommon.PageSize);
            sqlParams[2] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
            sqlParams[2].Direction = ParameterDirection.ReturnValue;
            sqlParams[3] = new SqlParameter("@NoofRecords", System.Data.SqlDbType.Int);
            sqlParams[3].Direction = ParameterDirection.Output;
           DataSet ds = SQLDBUtil.ExecuteDataset("HMS_GET_FinalSetteldDetails", sqlParams);
            objHrCommon.NoofRecords = (int)sqlParams[3].Value;
            objHrCommon.TotalPages = (int)sqlParams[2].Value;
            gvFinal.DataSource = ds;
            gvFinal.DataBind();
            AdvancedLeaveAppOthPaging.Bind(objHrCommon.CurrentPage, objHrCommon.TotalPages, objHrCommon.NoofRecords, objHrCommon.PageSize);
            txtEmpNameHidden.Value = "";

        }

        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionList_EmpName(string prefixText, int count, string contextKey)
        {
            DataSet ds = AttendanceDAC.GetSearchEmpName(prefixText);
            DataTable dt = ds.Tables[0];
            List<string> items = new List<string>(count);
            var rtval = new Dictionary<string, string>();
            foreach (DataRow row in dt.Rows)
            {
                string str = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(row["NAME"].ToString(), row["ID"].ToString());
                items.Add(str);
            }

            return items.ToArray(); 

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindGrid();

        }

        protected void gvFinal_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                if ((e.Row.DataItem as DataRowView)["A6"].ToString() == "" || (e.Row.DataItem as DataRowView)["A7"].ToString() == "" || (e.Row.DataItem as DataRowView)["D6"].ToString() == "" || (e.Row.DataItem as DataRowView)["D7"].ToString() == "")
                {
                    (e.Row.DataItem as DataRowView)["A6"] = 0;
                    (e.Row.DataItem as DataRowView)["A7"] = 0;
                    (e.Row.DataItem as DataRowView)["D6"] = 0;
                    (e.Row.DataItem as DataRowView)["D7"] = 0;
                }
                e.Row.Cells[3].ToolTip = "A1=" + (e.Row.DataItem as DataRowView)["A1"].ToString() +
                                          "\nA2=" + (e.Row.DataItem as DataRowView)["A2"].ToString() +
                                          "\nA3=" + (e.Row.DataItem as DataRowView)["A3"].ToString() +
                                          "\nA4=" + (e.Row.DataItem as DataRowView)["A4"].ToString() +
                                          "\nA5=" + (e.Row.DataItem as DataRowView)["A5"].ToString() +
                                          "\nA6=" + (e.Row.DataItem as DataRowView)["A6"].ToString() +
                                          "\nA7=" + (e.Row.DataItem as DataRowView)["A7"].ToString();

                e.Row.Cells[4].ToolTip = "D1=" + (e.Row.DataItem as DataRowView)["D1"].ToString() +
                                          "\nD2=" + (e.Row.DataItem as DataRowView)["D2"].ToString() +
                                          "\nD3=" + (e.Row.DataItem as DataRowView)["D3"].ToString() +
                                          "\nD4=" + (e.Row.DataItem as DataRowView)["D4"].ToString() +
                                          "\nD5=" + (e.Row.DataItem as DataRowView)["D5"].ToString() +
                                          "\nD6=" + (e.Row.DataItem as DataRowView)["D6"].ToString() +
                                          "\nD7=" + (e.Row.DataItem as DataRowView)["D7"].ToString();
            }     
        }
    }
}