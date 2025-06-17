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
using AECLOGIC.HMS.BLL;
namespace AECLOGIC.ERP.HMS
{
    public partial class ClientChart1 : AECLOGIC.ERP.COMMON.WebFormMaster
    {
        AttendanceDAC objAtt = new AttendanceDAC();
        protected override void OnInit(EventArgs e)
        {
            ModuleID = 1;
            base.OnInit(e);
        }

        SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings["strConn"].ToString());
        SqlDataAdapter adp;
        int id;
        DataSet EMPResultSet;
        DataSet dsProject = new DataSet();
        string imgPath = "~/EmpImages/";
        protected void Page_Load(object sender, EventArgs e)
        {
            Ajax.Utility.RegisterTypeForAjax(typeof(AjaxDAL));
           
            if (!IsPostBack)
            {
                BindOrgs();
            }
            string strScript = "<script language='javascript' type='text/javascript'>HideDisplay(); </script>";
            Page.RegisterStartupScript("pre", strScript);
        }
        private void BindTree(int OrgID)
        {
            tvProjects.Nodes.Clear();
            this.tvProjects.Attributes.Add("onmouseover", "RightClick(event);");
            string strScript = "";
            hdn.Value = "";
           
            TreeNode newRoot = new TreeNode();
            newRoot.Text = "<span title='' class='tvOrgPrj'>" + ddlorgs.SelectedItem.Text + "</span>"; ;
            newRoot.Value = OrgID.ToString();
            newRoot.SelectAction = TreeNodeSelectAction.Expand;
            tvProjects.Nodes.Add(newRoot);
            // GetDeptMenu(ref newRoot);
            GetProject(ref newRoot, OrgID);
            strScript = "<script language='javascript' type='text/javascript'>HideDisplay(); </script>";
            Page.RegisterStartupScript("pre", strScript);
        }

        public void BindOrgs()
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@CompanyID", Convert.ToInt32(Session["CompanyID"]));
            FIllObject.FillDropDown(ref ddlorgs, "HMS_Org_List",param);
        }

        private void AddNodes(ref TreeNode tn, DataTable dt, string Id)
        {
            try
            {
                DataRow[] drHeads = null;

                if (dt.Columns.Contains("DeptID"))
                    drHeads = dt.Select("ManagerID='" + Id.ToString() + "'", "DeptID asc");
                else
                    drHeads = dt.Select("ManagerID='" + Id.ToString() + "'");

                Hashtable lstDepet = new Hashtable();
                foreach (DataRow dr in drHeads)
                {
                    tn.SelectAction = TreeNodeSelectAction.Expand;
                    TreeNode tnHead = null;

                    if (dr["ManagerID"].ToString() != "0")
                    {

                        tnHead = new TreeNode("<span title='" + dr[0].ToString() + "' class='tvOrgNode'>" + dr["EmpName"].ToString() + "<span title='" + dr[0].ToString() + "' class='tvOrgDesignation'>" + "(" + dr["Designation"].ToString() + ")" + "</span></span>", dr[0].ToString());
                        tnHead.SelectAction = TreeNodeSelectAction.Expand;
                        tn.ChildNodes.Add(tnHead);
                        AddNodes(ref tnHead, dt, dr[0].ToString());
                    }
                    else
                    {
                        AddNodes(ref tn, dt, dr[0].ToString());
                    }
                }
            }
            catch { }
        }

        DataSet dsEmployeesAttendance = new DataSet();

        void BindOffices(ref TreeNode tnParent, DataTable dtOffices)
        {
            DataRow[] drRows = dtOffices.Select("Under='" + tnParent.Value + "'");
            foreach (DataRow drRow in drRows)
            {

                TreeNode newNode = new TreeNode();
                newNode.Text = "<span title='' class='tvOrgPrj'>" + drRow["OfficeName"].ToString() + "</span>";
                newNode.Value = drRow["OfficeId"].ToString();
                newNode.SelectAction = TreeNodeSelectAction.Expand;

                //code added by me

                TreeNode nodeHead = new TreeNode();
                nodeHead.Text = "<span title='" + drRow["EmpId"].ToString() + "' class='tvOrgDeptHead'>" + drRow["EmpName"].ToString() + "(" + drRow["Designation"].ToString() + ")" + "<span title='" + drRow["EmpId"].ToString() + "' class='tvOrgDesignation'>" + "</span></span>";
                nodeHead.Value = drRow["EmpId"].ToString();
                nodeHead.SelectAction = TreeNodeSelectAction.Expand;

                //end
                tnParent.ChildNodes.Add(newNode);
                newNode.ChildNodes.Add(nodeHead);
                GetDeptMenu(ref nodeHead);
                BindOffices(ref newNode, EMPResultSet.Tables[0]);
            }
        }

        public void GetProject(ref TreeNode tnParent, int PID)
        {
            EMPResultSet = objAtt.GetofficesForClientOC(PID);
            ViewState["Departments"] = objAtt.GetDeptHeadsForClientOC(PID);

            DataRow[] drRows = EMPResultSet.Tables[0].Select("Under is null");
            foreach (DataRow drRow in drRows)
            {

                TreeNode newNode = new TreeNode();
                newNode.Text = "<span title='' class='tvOrgPrj'>" + drRow["OfficeName"].ToString() + "</span>";
                newNode.Value = drRow["OfficeId"].ToString();
                newNode.SelectAction = TreeNodeSelectAction.Expand;
                tnParent.ChildNodes.Add(newNode);

                TreeNode nodeHead = new TreeNode();
                nodeHead.Text = "<span title='" + drRow["EmpId"].ToString() + "' class='tvOrgDeptHead'>" + drRow["EmpName"].ToString() + "(" + drRow["Designation"].ToString() + ")" + "<span title='" + drRow["EmpId"].ToString() + "' class='tvOrgDesignation'>" + "</span></span>";
                nodeHead.Value = drRow["EmpId"].ToString();
                nodeHead.SelectAction = TreeNodeSelectAction.Expand;
                newNode.ChildNodes.Add(nodeHead);
                GetDeptMenu(ref nodeHead);
                BindOffices(ref newNode, EMPResultSet.Tables[0]);
            }
        }

        public void GetDeptMenu(ref TreeNode node)
        {
            int OfficeId = Convert.ToInt32(node.Parent.Value);
            DataSet dsEMPResultSet = (DataSet)ViewState["Departments"];
            DataRow[] drRows = dsEMPResultSet.Tables[0].Select("OfficeId='" + OfficeId + "'");
            if (drRows.Length > 0)
            {
                foreach (DataRow drRow in drRows)
                {
                    TreeNode newNode1 = new TreeNode();
                    newNode1.Text = "<span title='' class='tvOrgDept'>" + drRow["DeptName"].ToString() + "</span>";
                    newNode1.Value = drRow["DeptID"].ToString();
                    newNode1.SelectAction = TreeNodeSelectAction.Expand;

                    TreeNode nodeHead1 = new TreeNode();
                    nodeHead1.Text = "<span title='" + drRow["EmpId"].ToString() + "' class='tvOrgDeptHead'>" + drRow["EmpName"].ToString() + "(" + drRow["Designation"].ToString() + ")" + "<span title='" + drRow["EmpId"].ToString() + "' class='tvOrgDesignation'>" + "</span></span>";
                    nodeHead1.Value = drRow["EmpId"].ToString();
                    nodeHead1.SelectAction = TreeNodeSelectAction.Select;
                    newNode1.ChildNodes.Add(nodeHead1);
                    node.ChildNodes.Add(newNode1);
                }
            }
        }

        public void GetProjMenu(ref TreeNode node)
        {
            try
            {
                int ClientID = Convert.ToInt32(tvProjects.Nodes[0].Value);
                int OfficeId = Convert.ToInt32(node.Parent.Parent.Parent.Value);
                int DeptID = Convert.ToInt32(node.Parent.Value);
                int EmpId = Convert.ToInt32(node.Value);

                EMPResultSet = objAtt.GetHeadDetailsForClients(ClientID, DeptID, EmpId, OfficeId);
                if (EMPResultSet.Tables[0].Rows.Count > 0)
                {
                    AddNodes(ref node, EMPResultSet.Tables[0], node.Value);
                }
            }
            catch { }
        }

        public DataSet ExecuteQuery(string QueryString)
        {
            string ConnectingString = null;
            SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings["strConn"].ToString());
            SqlConnection conn = new SqlConnection(ConnectingString);
            SqlDataAdapter DBAdapter = null;
            DataSet ResultDataSet = new DataSet();
            try
            {
                DBAdapter = new SqlDataAdapter(QueryString, conn);
                DBAdapter.Fill(ResultDataSet);
                conn.Close();
            }
            catch (Exception ex)
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                Response.Write("Unable to Connect to the DataBase");
            }
            return ResultDataSet;
        }

        protected void tvProjects_SelectedNodeChanged(object sender, EventArgs e)
        {
            TreeNode trNode = tvProjects.SelectedNode;
            trNode.ChildNodes.Clear();
            GetProjMenu(ref trNode);
            trNode.SelectAction = TreeNodeSelectAction.SelectExpand;
            trNode.Expand();
        }

        protected void tvProjects_TreeNodeDataBound(object sender, TreeNodeEventArgs e)
        {

        }

        protected void btnShow_Click(object sender, EventArgs e)
        {
            int OrgID = Convert.ToInt32(ddlorgs.SelectedItem.Value);
            if (OrgID != 0)
            {
                tdtreeview.Visible = true;

                BindTree(OrgID);
            }
            else
            {
                AlertMsg.MsgBox(Page, "Select Organization");
            }
        }
    }
}