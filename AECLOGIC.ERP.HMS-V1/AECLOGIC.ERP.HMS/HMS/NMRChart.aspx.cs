using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Collections.Generic;
using AECLOGIC.HMS.BLL;

namespace AECLOGIC.ERP.HMS
{
    public partial class NMRChart : AECLOGIC.ERP.COMMON.WebFormMaster
    {
        AttendanceDAC objAtt = new AttendanceDAC();
        int status;
         
        int mid = 0;
        bool viewall;
        string menuname;
        string menuid;
        int id;
        DataSet EMPResultSet;
        DataSet dsEmployeesAttendance = new DataSet();
        protected override void OnInit(EventArgs e)
        {
            ModuleID = 1;
            base.OnInit(e);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
           
            if (!IsPostBack)
            {
                string strScript = "";
                hdn.Value = "";
                GetProject(1);
                BindAttandanceCount();
            }
            EMPResultSet = objAtt.GetNMS();
        }

        private void BindAttandanceCount()
        {
            DataSet ds = AttendanceDAC.T_HR_GetNMRAttendanceCount();
            lbltot.Text = ds.Tables[0].Rows[0][0].ToString();
            lblpresent.Text = ds.Tables[1].Rows[0][0].ToString();
            lblabsent.Text = (Convert.ToInt32(ds.Tables[0].Rows[0][0]) - Convert.ToInt32(ds.Tables[1].Rows[0][0])).ToString();
        }
    
        private void AddNodes(ref TreeNode tn)
        {
            try
            {
                int SiteId = Convert.ToInt32(tn.Parent.Value);
                int DeptId = Convert.ToInt32(tn.Value);
                DataRow[] drNMRRows = EMPResultSet.Tables[0].Select("SiteId='" + SiteId.ToString() + "' and DeptID='" + DeptId.ToString() + "'");
                TreeNode tnNMR = null;
                foreach (DataRow drRow in drNMRRows)
                {
                    tnNMR = new TreeNode();
                    tnNMR.Value = drRow["NMRId"].ToString();
                    tnNMR.Text = drRow["NMRName"].ToString();
                    tnNMR.SelectAction = TreeNodeSelectAction.None;
                    tn.ChildNodes.Add(tnNMR);
                }
            }
            catch { }
        }



        public void GetProject(int PID)
        {
            EMPResultSet = objAtt.GetProjectManagersForOC(Convert.ToInt32(Session["CompanyID"]));
            dsEmployeesAttendance = objAtt.GetNMRAttendanceForOC();
            foreach (DataRow drRow in EMPResultSet.Tables[0].Rows)
            {
                DataRow[] drAtt = dsEmployeesAttendance.Tables[0].Select("SiteId='" + drRow["Prjid"].ToString() + "'");
                int P = 0;
                int T = 0;
                if (drAtt.Length > 0)
                {
                    P = Convert.ToInt32(drAtt[0]["SitePresent"]);
                    T = Convert.ToInt32(drAtt[0]["SiteTotal"]);
                }
                int A = T - P;
                TreeNode newNode = new TreeNode();
                newNode.Text = "<span title='' class='tvOrgPrj'>" + drRow["Site_Name"].ToString() + "</span>" + "( P: " + P + ";" + " A: " + A + ";" + " Tot: " + T + " )";
                newNode.Value = drRow["Prjid"].ToString();
                newNode.SelectAction = TreeNodeSelectAction.Expand;
                tvProjects.Nodes.Add(newNode);
                GetDeptMenu(ref newNode);

            }
        }

        public void GetDeptMenu(ref TreeNode node)
        {
            int PrjID = Convert.ToInt32(node.Value);
            EMPResultSet = objAtt.GetNMS();
            DataRow[] drRows = EMPResultSet.Tables[0].Select("SiteId='" + PrjID.ToString() + "'");
            List<string> Depts = new List<string>();
            foreach (DataRow drRow in drRows)
            {
                if (!Depts.Contains(drRow["DeptId"].ToString().Trim()))
                {
                    Depts.Add(drRow["DeptId"].ToString().Trim());
                    DataRow[] drAtt = dsEmployeesAttendance.Tables[0].Select("SiteId='" + node.Value.ToString() + "' and DeptId='" + drRow["DeptId"].ToString() + "'");
                    int P = 0;
                    int T = 0;
                    if (drAtt.Length > 0)
                    {
                        P = Convert.ToInt32(drAtt[0]["Present"]);
                        T = Convert.ToInt32(drAtt[0]["Total"]);
                    }
                    int A = T - P;
                    DataRow[] drNMRRows = EMPResultSet.Tables[0].Select("SiteId='" + PrjID.ToString() + "' and DeptID='" + drRow["DeptId"].ToString() + "'");
                    TreeNode newNode = new TreeNode();
                    newNode.Text = "<span title='' class='tvOrgDept'>" + drRow["DepartmentName"].ToString() + "</span>" + "( P: " + P + ";" + " A: " + A + ";" + " Tot: " + T + " )";
                    newNode.Value = drRow["DeptID"].ToString();
                    newNode.SelectAction = TreeNodeSelectAction.Select;
                    node.ChildNodes.Add(newNode);
                }
            }
        }

       
        protected void tvProjects_SelectedNodeChanged(object sender, EventArgs e)
        {
            TreeNode trNode = tvProjects.SelectedNode;
            if (trNode.ChildNodes.Count == 0)
            {
                AddNodes(ref trNode);
                if (trNode.ChildNodes.Count > 0)
                {
                    trNode.SelectAction = TreeNodeSelectAction.Expand;
                    trNode.Expand();
                }
            }
        }


    }
}
