using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Web.Services;
using System.Data.SqlClient;
using AECLOGIC.HMS.BLL;

namespace AECLOGIC.ERP.HMS
{

    public partial class OrgChart1 : AECLOGIC.ERP.COMMON.WebFormMaster
    {
        AttendanceDAC objAtt = new AttendanceDAC();
        int status;
         

        SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings["strConn"].ToString());
        int mid = 0;
        bool viewall;
        string menuname;
        string menuid;
        DataSet EMPResultSet;
        DataSet EMPResultSet1;
        string imgPath = "~/HMS/EmpImages/";
        static int SearchCompanyID;
        static int Empdeptid = 0;
        protected override void OnInit(EventArgs e)
        {
            ModuleID = 1;
            base.OnInit(e);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Ajax.Utility.RegisterTypeForAjax(typeof(AjaxDAL));
            try
            {
                string id =  Convert.ToInt32(Session["UserId"]).ToString();
                SearchCompanyID = Convert.ToInt32(Session["CompanyID"]);
            }
            catch
            {
                Response.Redirect("Home.aspx");
            }
           
            int Item = Convert.ToInt32(rbTaxasion.SelectedValue);
            tblDisplay.Visible = false;
            if (Item == 1)
            {
                OldOrgChart.Visible = true;
                tblDisplay.Visible = false;
                NewOrgChart.Visible = false;
                NewOrgChartwithImages.Visible = false;
                searchCriteria.Visible = false;
                checkeditems.Visible = true;
            }
            else if (Item == 2)
            {
                searchCriteria.Visible = true;
                OldOrgChart.Visible = false;
                tblDisplay.Visible = false;
                NewOrgChartwithImages.Visible = false;
                NewOrgChart.Visible = true;
                checkeditems.Visible = false;
            }
            else if (Item == 3)
            {
                searchCriteria.Visible = true;
                OldOrgChart.Visible = false;
                tblDisplay.Visible = false;
                NewOrgChart.Visible = false;
                NewOrgChartwithImages.Visible = true;
                checkeditems.Visible = false;
            }
           
            Ajax.Utility.RegisterTypeForAjax(typeof(AjaxDAL));
            if (!IsPostBack)
            {
                this.tvProjects.Attributes.Add("onmouseover", "RightClick(event);");
                string strScript = "";
                if (Request.QueryString.Count > 0)
                {
                    if (Request.QueryString["id"].ToString() == "1")
                    {
                        status = 1;
                        SearchCompanyID = Convert.ToInt32(Session["CompanyID"]);
                        strScript = "<script language='javascript' type='text/javascript' >chkChange(true);</script>";
                        ClientScript.RegisterStartupScript(typeof(Page), "str", "<script type='text/javascript'>init();</script>");
                        ClientScript.RegisterStartupScript(typeof(Page), "str", "<script type='text/javascript'>initEdit();</script>");
                        
                    }
                    else
                    {
                        status = 0;
                        strScript = "<script language='javascript' type='text/javascript' >chkChange(false);</script>";
                        ClientScript.RegisterStartupScript(typeof(Page), "str", "<script type='text/javascript'>init();</script>");
                        ClientScript.RegisterStartupScript(typeof(Page), "str", "<script type='text/javascript'>initEdit();</script>");
                       
                    }
                    Page.RegisterStartupScript("chkChange", strScript);
                }

                TreeNode newRoot = new TreeNode();
                TreeNode nodeChairman = new TreeNode();
                EMPResultSet1 = objAtt.GetDirectors(Convert.ToInt32(Session["CompanyID"]));
                TreeNode nodeDirector = new TreeNode();

               

                EMPResultSet = objAtt.GetExecutivDirectors(Convert.ToInt32(Session["CompanyID"]));

                if (EMPResultSet.Tables[0].Rows.Count > 0)
                {
                    newRoot = new TreeNode();
                    newRoot.Text = "<span title='' class='tvOrgPrj'>" + "Top Management" + "</span>";
                    newRoot.Value = "";
                    newRoot.SelectAction = TreeNodeSelectAction.Select;
                    tvProjects.Nodes.Add(newRoot);

                    foreach (DataRow drDirctor in EMPResultSet1.Tables[0].Rows)
                    {
                        nodeDirector = new TreeNode();
                        nodeDirector.SelectAction = TreeNodeSelectAction.Select;
                        nodeDirector.Value = drDirctor[0].ToString();
                        nodeDirector.Text = "<span title='" + drDirctor[0].ToString() + "' class='tvOrgDirectos'>" + drDirctor[1].ToString() + "<span title='" + "' class='tvOrgDesignation'>" + "(Director)" + "</span>";
                        if (status != 0)
                        {
                            if (drDirctor["image"].ToString() != "")
                                nodeDirector.ImageUrl = imgPath + drDirctor[0].ToString() + "_thumb." + drDirctor["image"].ToString();
                            else
                                nodeDirector.ImageUrl = imgPath + "0_thumb.jpg";
                        }
                        newRoot.ChildNodes.Add(nodeDirector);
                    }


                    foreach (DataRow drDirctor in EMPResultSet.Tables[0].Rows)
                    {
                        nodeDirector = new TreeNode();
                        nodeDirector.Value = drDirctor[0].ToString();

                        if (drDirctor[4].ToString() == "2")
                        {
                            nodeDirector.Text = "<span title='" + drDirctor[0].ToString() + "' class='tvOrgDirectos'>" + drDirctor[1].ToString() + "<span title='" + drDirctor[0].ToString() + "' class='tvOrgDesignation'>" + "(" + drDirctor[2].ToString() + ")" + "</span></span>";
                        }
                        else
                            if (drDirctor[4].ToString() == "7")
                            {
                                nodeDirector.Text = "<span title='" + drDirctor[0].ToString() + "' class='tvODNodeHead'>" + drDirctor[1].ToString() + "<span title='" + drDirctor[0].ToString() + "' class='tvODNodeHead'>" + " (" + drDirctor[2].ToString() + ")" + "</span></span>";
                            }
                            else
                            {
                                nodeDirector.Text = "<span title='" + drDirctor[0].ToString() + "' class='tvObsentNode'>" + drDirctor[1].ToString() + "<span  title='" + drDirctor[0].ToString() + "' class='tvObsentNode'>" + " (" + drDirctor[2].ToString() + ")" + "</span></span>";
                            }
                        if (status != 0)
                        {
                            if (drDirctor["image"].ToString() != "")
                                nodeDirector.ImageUrl = imgPath + drDirctor[0].ToString() + "_thumb." + drDirctor["image"].ToString();
                            else
                                nodeDirector.ImageUrl = imgPath + "0_thumb.jpg";
                        }
                        newRoot.ChildNodes.Add(nodeDirector);
                    }
                    newRoot.Collapse();

                }

              GetProject(1);       
                
                
          
                strScript = "<script language='javascript' type='text/javascript'>HideDisplay(); </script>";
                Page.RegisterStartupScript("pre", strScript);

            }
        }
        protected void rbTaxasion_SelectedIndexChanged(object sender, EventArgs e)
        {
           
            int Item = Convert.ToInt32(rbTaxasion.SelectedValue);
            tblDisplay.Visible = false;
            if (Item == 1)
            {
                OldOrgChart.Visible = true;
                tblDisplay.Visible = false;
                NewOrgChart.Visible =false;
                NewOrgChartwithImages.Visible = false;
                searchCriteria.Visible = false;
                checkeditems.Visible = true;
            }
            else if (Item == 2)
            {
                searchCriteria.Visible = true;
                OldOrgChart.Visible = false;
                  tblDisplay.Visible = false;
                  NewOrgChartwithImages.Visible = false;
                NewOrgChart.Visible = true;
                checkeditems.Visible = false;
                ClientScript.RegisterStartupScript(typeof(Page), "str", "<script type='text/javascript'>init();</script>");
             
            }
            else if (Item == 3)
            {
                searchCriteria.Visible = true;
                OldOrgChart.Visible = false;
                tblDisplay.Visible = false;
                NewOrgChart.Visible = false;
                NewOrgChartwithImages.Visible = true;
                checkeditems.Visible = false;
                ClientScript.RegisterStartupScript(typeof(Page), "str", "<script type='text/javascript'>initEdit();</script>");
            }
        }
        [WebMethod]
        public static List<object> GetChartData()
        {
            //select EmpId,FName,Designation, 1 as reprting from T_G_EmployeeMaster
            string query = "SELECT EmpId as EmployeeId, FName as Name, Designation as Designation, Mgnr as ReportingManager";
            query += " FROM T_G_EmployeeMaster where Status='y'";
            string constr = "Password=adminAEC@34%;Persist Security Info=True;User ID=sa;Initial catalog=cloud; Data Source=192.168.10.3; Connection Timeout=120";
           
            SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings["strConn"].ToString());
            using (SqlConnection con = new SqlConnection(cn.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    List<object> chartData = new List<object>();
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = con;
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            chartData.Add(new object[]
                    {
                         sdr["EmployeeId"], sdr["Name"], sdr["Designation"] , sdr["ReportingManager"]
                    });
                        }
                    }
                    con.Close();
                    return chartData;
                }
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
                ViewState["Editable"] = (bool)ds.Tables[0].Rows[0]["Editable"];
                ViewState["ViewAll"] = (bool)ds.Tables[0].Rows[0]["ViewAll"];
                viewall = (bool)ViewState["ViewAll"];
                menuname = ds.Tables[0].Rows[0]["menuname"].ToString();
                menuid = MenuId.ToString();
                mid = Convert.ToInt32(ds.Tables[0].Rows[0]["MenuId"].ToString());
            }
            return MenuId;
        }

        private void AddNodes(ref TreeNode tn, DataTable dt, string Id)
        {
            try
            {
                DataRow[] drHeads = null;
                if (dt.Columns.Contains("DepartmentName"))
                    drHeads = dt.Select("Mgnr='" + Id.ToString() + "'", "DepartmentName asc");
                else
                    drHeads = dt.Select("Mgnr='" + Id.ToString() + "'");

                Hashtable lstDepet = new Hashtable();
                foreach (DataRow dr in drHeads)
                {
                    tn.SelectAction = TreeNodeSelectAction.Expand;
                    TreeNode tnHead = null;

                    if (dr["Mgnr"].ToString() != "0")
                    {
                        if (dr["Status"].ToString() == "y")
                        {
                            if (dr["attendancestatus"].ToString() == "2")
                            {
                                tnHead = new TreeNode("<span title='" + dr[0].ToString() + "' class='tvOrgNode'>" + dr["Name"].ToString() + "<span title='" + dr[0].ToString() + "' class='tvOrgDesignation'>" + " (" + dr["Design"].ToString() + " " + dr["Category"].ToString() + ")" + "</span></span>", dr[0].ToString());
                            }
                            else
                                if (dr["attendancestatus"].ToString() == "7")
                                {
                                    tnHead = new TreeNode("<span title='" + dr[0].ToString() + "' class='tvODNodeGeneral'>" + dr["Name"].ToString() + "<span title='" + dr[0].ToString() + "' class='tvODNodeGeneral'>" + " (" + dr["Design"].ToString() + " " + dr["Category"].ToString() + ")" + "</span></span>", dr[0].ToString());
                                }
                                else
                                {
                                    tnHead = new TreeNode("<span title='" + dr[0].ToString() + "' class='tvObsentNode'>" + dr["Name"].ToString() + "<span title='" + dr[0].ToString() + "' class='tvObsentNode'>" + " (" + dr["Design"].ToString() + " " + dr["Category"].ToString() + ")" + "</span></span>", dr[0].ToString());
                                }

                            if (status != 0)
                            {
                                if (dr["image"].ToString() != "")
                                    tnHead.ImageUrl = imgPath + dr[0].ToString() + "_thumb." + dr["image"].ToString();
                                else
                                    tnHead.ImageUrl = imgPath + "0_thumb.jpg";
                            }
                            tnHead.SelectAction = TreeNodeSelectAction.Expand;
                            tn.ChildNodes.Add(tnHead);
                            AddNodes(ref tnHead, dt, dr[0].ToString());
                        }
                        else
                            if (dr["Type"].ToString() == "3" && dr["Status"].ToString() == "n")
                            {
                                tnHead = new TreeNode("<span title='' class='tvOrgNode'>" + "Vacant" + "<span title='' class='tvOrgDesignation'>" + " (" + dr["Design"].ToString() + " " + dr["Category"].ToString() + ")" + "</span></span>", dr[0].ToString());
                                if (status != 0)
                                {
                                    tnHead.ImageUrl = imgPath + "0_thumb.jpg";
                                }
                                tnHead.SelectAction = TreeNodeSelectAction.Expand;
                                tn.ChildNodes.Add(tnHead);
                                AddNodes(ref tnHead, dt, dr[0].ToString());
                            }
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
        public void GetProject(int PID)
        {
            EMPResultSet = objAtt.GetProjectManagersForOC(Convert.ToInt32(Session["CompanyID"]));
             dsEmployeesAttendance = objAtt.GetAttendanceForOC(Convert.ToInt32(Session["CompanyID"]));
            Hashtable htAttendance = null;
            foreach (DataRow drRow in EMPResultSet.Tables[0].Rows)
            {

                //int PrjID = Convert.ToInt32(drRow["Prjid"].ToString());
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
                TreeNode nodeManager = new TreeNode();

                if (drRow["status"].ToString() == "1")
                    nodeManager.Text = "<span title='" + drRow["MgnrId"].ToString() + "' class='tvOrgPrjMan'>" + drRow["name"].ToString() + "<span title='" + drRow["MgnrId"].ToString() + "' class='tvOrgDesignation'>" + " (" + drRow["Designation"].ToString() + " " + drRow["Category"].ToString() + ")" + "</span></span>";
                else
                    nodeManager.Text = "<span class='tvOrgPrjMan'>" + "Vacant" + "</span>";

                nodeManager.Value = drRow["MgnrId"].ToString();
                nodeManager.SelectAction = TreeNodeSelectAction.Expand;
                if (status != 0)
                {
                    if (drRow["image"].ToString() != "" && (drRow["status"].ToString() == "1"))
                        nodeManager.ImageUrl = imgPath + drRow["MgnrId"].ToString() + "_thumb." + drRow["image"].ToString();
                    else
                        nodeManager.ImageUrl = imgPath + "0_thumb.jpg";
                }
                newNode.ChildNodes.Add(nodeManager);
                tvProjects.Nodes.Add(newNode);
                GetDeptMenu(ref nodeManager);

            }
            lblabsent.Text = (Convert.ToInt32(dsEmployeesAttendance.Tables[0].Rows[0]["SiteTotal"]) - Convert.ToInt32(dsEmployeesAttendance.Tables[0].Rows[0]["SitePresent"])).ToString();
            lblpresent.Text = dsEmployeesAttendance.Tables[0].Rows[0]["SitePresent"].ToString();
            lbltot.Text = dsEmployeesAttendance.Tables[0].Rows[0]["SiteTotal"].ToString();
        }

        public void GetDeptMenu(ref TreeNode node)
        {
            int PrjID = Convert.ToInt32(node.Parent.Value);

            Boolean headrepeat = false ;
            int deptid = 0;
            EMPResultSet = objAtt.GetDeptHeadsForOC(PrjID);
          //  TreeNode newNode = new TreeNode();
            if (EMPResultSet.Tables[0].Rows.Count > 0)
            {
           
                foreach (DataRow drRow in EMPResultSet.Tables[0].Rows)
                {
                    if (drRow["Strength"].ToString() != "0")
                    {
                        DataRow[] drAtt = dsEmployeesAttendance.Tables[0].Select("SiteId='" + node.Parent.Value.ToString() + "' and DeptId='" + drRow["DepartmentUId"].ToString() + "'");
                        int P = 0;
                        int T = 0;
                        if (drAtt.Length > 0)
                        {
                            P = Convert.ToInt32(drAtt[0]["Present"]);
                            T = Convert.ToInt32(drAtt[0]["Total"]);
                        }
                        int A = T - P;


                        if (true)
                        {
                            
                            if (deptid != Convert.ToInt32(drRow["DepartmentUId"]))
                                headrepeat = false;
                            else if (deptid == Convert.ToInt32(drRow["DepartmentUId"]))
                                headrepeat = true;
                            TreeNode newNode = new TreeNode();
                           // if (headrepeat == false)
                           // {
                               
                                newNode.Text = "<span title='' class='tvOrgDept'>" + drRow["DepartmentName"].ToString() + "</span>" + "( P: " + P + ";" + " A: " + A + ";" + " Tot: " + T + " )"; ;
                                newNode.Value = drRow["DepartmentUId"].ToString();
                                newNode.SelectAction = TreeNodeSelectAction.Expand;
                              
                           // }
                          

                            deptid = Convert.ToInt32(drRow["DepartmentUId"]);

                            TreeNode nodeHead = new TreeNode();

                            if (drRow["headstatus"].ToString() == "1")
                            {
                                if (drRow["attendancestatus"].ToString() == "2")
                                {

                                    nodeHead.Text = "<span title='" + drRow["headid"].ToString() + "' class='tvOrgDeptHead'>" + drRow["name"].ToString() + "<span title='" + drRow["headid"].ToString() + "' class='tvOrgDesignation'>" + " (" + drRow["Designation"].ToString() + " " + drRow["Category"].ToString() + ")" + "</span></span>";
                                }
                                else
                                    if (drRow["attendancestatus"].ToString() == "7")
                                    {
                                        nodeHead.Text = "<span title='" + drRow["headid"].ToString() + "' class='tvODNodeHead'>" + drRow["name"].ToString() + "<span title='" + drRow["headid"].ToString() + "' class='tvODNodeHead'>" + " (" + drRow["Designation"].ToString() + " " + drRow["Category"].ToString() + ")" + "</span></span>";
                                    }
                                    else
                                    {
                                        nodeHead.Text = "<span title='" + drRow["headid"].ToString() + "' class='tvObsentNode'>" + drRow["name"].ToString() + "<span title='" + drRow["headid"].ToString() + "' class='tvObsentNode'>" + " (" + drRow["Designation"].ToString() + " " + drRow["Category"].ToString() + ")" + "</span></span>";
                                    }
                                if (status != 0)
                                {
                                    if (drRow["image"].ToString() != "")
                                        nodeHead.ImageUrl = imgPath + drRow["headid"].ToString() + "_thumb." + drRow["image"].ToString();
                                    else
                                        nodeHead.ImageUrl = imgPath + "0_thumb.jpg";
                                }
                                nodeHead.Value = drRow["headid"].ToString();

                                nodeHead.SelectAction = TreeNodeSelectAction.Select;

                                newNode.ChildNodes.Add(nodeHead);
                                node.ChildNodes.Add(newNode);
                                //GetProjMenu(ref nodeHead);
                            }
                            else
                            {
                                nodeHead.Text = "<span title=''> Vacant </span>";
                                if (status != 0)
                                {
                                    nodeHead.ImageUrl = imgPath + "0_thumb.jpg";
                                }
                                nodeHead.Value = drRow["headid"].ToString();

                                nodeHead.SelectAction = TreeNodeSelectAction.Select;

                                newNode.ChildNodes.Add(nodeHead);
                                node.ChildNodes.Add(newNode);
                              
                            }


                        }
                    }
                }
            }
        }

        public void GetProjMenu(ref TreeNode node)
        {
            try
            {
                
                int WSId = Convert.ToInt32(node.Parent.Parent.Parent.Value);
                int DeptID = Convert.ToInt32(node.Parent.Value);
                int EmpId = Convert.ToInt32(node.Value);

                EMPResultSet = objAtt.GetHeadDetails(WSId, DeptID, EmpId);
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
            ConnectingString = ConfigurationManager.AppSettings["strConn"].ToString();
            SqlConnection conn = new SqlConnection(ConnectingString);
            SqlDataAdapter DBAdapter = null;
            DataSet ResultDataSet = new DataSet();
            try
            {
                DBAdapter = new SqlDataAdapter(QueryString, conn);
                DBAdapter.Fill(ResultDataSet);
                conn.Close();
            }
            catch
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

      

        protected void btnSearchToP_Click(object sender, EventArgs e)
        {
            
            int Item = Convert.ToInt32(rbTaxasion.SelectedValue);
           
             if (Item == 2)
            {

                searchCriteria.Visible = true;
                OldOrgChart.Visible = false;
                  tblDisplay.Visible = false;
                  NewOrgChartwithImages.Visible = false;
                NewOrgChart.Visible = true;
                ClientScript.RegisterStartupScript(typeof(System.String), "str", "<script type='text/javascript'>init();</script>");
            }
            else if (Item == 3)
            {
                searchCriteria.Visible = true;
                OldOrgChart.Visible = false;
                tblDisplay.Visible = false;
                NewOrgChart.Visible = false;
                NewOrgChartwithImages.Visible = true;
                ClientScript.RegisterStartupScript(typeof(Page), "str", "<script type='text/javascript'>initEdit();</script>");
            }
        }
        
        protected void ddlWS_SelectedIndexChanged(object sender, EventArgs e)
        {
            int Item = Convert.ToInt32(rbTaxasion.SelectedValue);
              if (Item == 2)
            {
                searchCriteria.Visible = true;
                OldOrgChart.Visible = false;
                  tblDisplay.Visible = false;
                  NewOrgChartwithImages.Visible = false;
                NewOrgChart.Visible = true;
                checkeditems.Visible = false;
                ClientScript.RegisterStartupScript(typeof(Page), "str", "<script type='text/javascript'>init();</script>");
             
            }
            else if (Item == 3)
            {
                searchCriteria.Visible = true;
                OldOrgChart.Visible = false;
                tblDisplay.Visible = false;
                NewOrgChart.Visible = false;
                NewOrgChartwithImages.Visible = true;
                checkeditems.Visible = false;
                ClientScript.RegisterStartupScript(typeof(Page), "str", "<script type='text/javascript'>initEdit();</script>");
            }
           
            
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionList(string prefixText, int count, string contextKey)
        {
            DataSet ds = AttendanceDAC.GetGoogleABCSearchWorkSite(prefixText, SearchCompanyID);
            DataTable dt = ds.Tables[0];
            List<string> items = new List<string>(count);
            var rtval = new Dictionary<string, string>();
            foreach (DataRow row in dt.Rows)
            {
                string str = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(row["Name"].ToString(), row["ID"].ToString());
                items.Add(str);
            }

            return items.ToArray(); // txtItems.ToArray();
        }
        
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]

        public static string[] GetCompletionListDep(string prefixText, int count, string contextKey)
        {
            DataSet ds = AttendanceDAC.GetSearchDesiginationFilter(prefixText, SearchCompanyID, Empdeptid);
            DataTable dt = ds.Tables[0];
            List<string> items = new List<string>(count);
            var rtval = new Dictionary<string, string>();
            foreach (DataRow row in dt.Rows)
            {
                string str = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(row["Name"].ToString(), row["ID"].ToString());
                items.Add(str);
            }

            return items.ToArray();
        }
        protected void ddlDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            int Item = Convert.ToInt32(rbTaxasion.SelectedValue);
            if (Item == 2)
            {
                searchCriteria.Visible = true;
                OldOrgChart.Visible = false;
                tblDisplay.Visible = false;
                NewOrgChartwithImages.Visible = false;
                NewOrgChart.Visible = true;
                checkeditems.Visible = false;
                ClientScript.RegisterStartupScript(typeof(Page), "str", "<script type='text/javascript'>init();</script>");

            }
            else if (Item == 3)
            {
                searchCriteria.Visible = true;
                OldOrgChart.Visible = false;
                tblDisplay.Visible = false;
                NewOrgChart.Visible = false;
                NewOrgChartwithImages.Visible = true;
                checkeditems.Visible = false;
                ClientScript.RegisterStartupScript(typeof(Page), "str", "<script type='text/javascript'>initEdit();</script>");
            }
        }

    }
}
