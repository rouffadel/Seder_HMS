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
    public partial class LeavesCombinations : AECLOGIC.ERP.COMMON.WebFormMaster
    {
        TableRow tblRow;
        TableCell HLeaves, LeaveTYpes;
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
            Ajax.Utility.RegisterTypeForAjax(typeof(AjaxLeaveCombinations));
            BindData();
            if (!IsPostBack)
            {
                strurl = Request.UrlReferrer.ToString();
                Session["CurrentTable"] = "";
            }
        }
        public void BindData()
        {
           DataSet ds = Leaves.GetTypeofLeavesList();
            DataSet dsLC = new DataSet();
            dsLC = Leaves.GetLeaveCombination();
            tblRow = new TableRow();
            LeaveTYpes = new TableCell();
            LeaveTYpes.Text = "Leave Type";
            LeaveTYpes.Style.Add("font-weight", "bold");
            LeaveTYpes.Width = 100;
            tblRow.Cells.Add(LeaveTYpes);
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                HLeaves = new TableCell();
                //HLeaves.Text = dr["Name"].ToString();
                HLeaves.Text = dr["ShortName"].ToString();
                HLeaves.Style.Add("font-weight", "bold");
                HLeaves.Width = 60;
                tblRow.Cells.Add(HLeaves);
            }
            tblLeaveCombination.Rows.Add(tblRow);
            int Count = ds.Tables[0].Rows.Count;
            for (int j = 0; j < Count; j++)
            {
                tblRow = new TableRow();
                HLeaves = new TableCell();
                if (ds.Tables[0].Rows.Count != 0)
                {
                    if (ds.Tables[0].Rows[j][1].ToString() != null && ds.Tables[0].Rows[j][1].ToString() != "" && ds.Tables[0].Rows[j][1].ToString() != string.Empty)
                    {
                        HLeaves.Text = ds.Tables[0].Rows[j][1].ToString();
                        HLeaves.Width = 175;
                        tblRow.Cells.Add(HLeaves);
                    }
                    tblLeaveCombination.Rows.Add(tblRow);
                }
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    HLeaves = new TableCell();
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        for (int k = 0; k < ds.Tables[0].Rows.Count; k++)
                        {
                            CheckBox chk = new CheckBox();
                            chk.ID = ds.Tables[0].Rows[i]["LeaveType"].ToString() + "," + ds.Tables[0].Rows[j]["LeaveType"].ToString();
                            chk.Attributes.Add("onclick", "javascript:return UpdateLC(this,'" + ds.Tables[0].Rows[i]["LeaveType"].ToString() + "','" + ds.Tables[0].Rows[j]["LeaveType"].ToString() + "');");
                            HLeaves.Width = 60;
                            HLeaves = new TableCell();
                            HLeaves.Controls.Add(chk);
                            DataRow[] drSelected = dsLC.Tables[0].Select("Leave1='" + ds.Tables[0].Rows[i]["LeaveType"].ToString() + "' and Leave2='" + ds.Tables[0].Rows[j]["LeaveType"].ToString() + "'");
                            if (i == j) { chk.Checked = true; chk.Enabled = false; }
                            if (i - 1 == j) { chk.Enabled = false; }
                            if (i - 2 == j) { chk.Enabled = false; }
                            if (i - 3 == j) { chk.Enabled = false; }
                            if (i - 4 == j) { chk.Enabled = false; }
                            if (i - 5 == j) { chk.Enabled = false; }
                            if (i - 6 == j) { chk.Enabled = false; }
                            if (i - 7 == j) { chk.Enabled = false; }
                            if (i - 8 == j) { chk.Enabled = false; }
                            if (i - 9 == j) { chk.Enabled = false; }
                            if (i - 10 == j) { chk.Enabled = false; }
                            if (i - 11 == j) { chk.Enabled = false; }
                            if (i - 12 == j) { chk.Enabled = false; }
                            if (i - 13 == j) { chk.Enabled = false; }
                            if (i - 14 == j) { chk.Enabled = false; }
                            if (drSelected.Length > 0)
                            {
                                if (drSelected[0]["Status"].ToString() == "True")
                                    chk.Checked = true;
                            }
                        }
                    }
                    tblRow.Cells.Add(HLeaves);
                    tblLeaveCombination.Rows.Add(tblRow);
                }
            }
            Session["CurrentTable"] = tblLeaveCombination;
        }
        protected void btnBack_Click(object sender, EventArgs e)
        {
            Visible = false;
            Response.Redirect(strurl);
        }
    }
}
