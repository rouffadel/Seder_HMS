using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using Aeclogic.Common.DAL;
using ProcurementDAC;

namespace AECLOGIC.ERP.HMS
{
    public partial class QuickTrans : AECLOGIC.ERP.COMMON.WebFormMaster
    {
        string msg = "";
        bool viewall;
        object EmpIdExists = null;

        static int Type; static int AssetType;
        bool Editable; string menuname; string menuid; int mid = 0; int Worksite = 0;
        private GridSort objSort;
        ProcDept objProcdept = new ProcDept();
        ArrayList al = new ArrayList();

        private DataSet dsGoodsGroup; int MntrWS = 0;
        protected override void OnInit(EventArgs e)
        {
            ModuleID = 1;
            base.OnInit(e);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                MntrWS = Convert.ToInt32(ConfigurationManager.AppSettings["HeadOffice"].ToString());
               
                DataTable dtGoodsList = new DataTable();
                if (!IsPostBack)
                {
                    GetParentMenuId();
                    MainView.ActiveViewIndex = 0;

                    btnNewItem.Attributes.Add("onclick", "javascript:return AddNewItem();");
                    btnMove.Attributes.Add("onclick", "javascript:return validateMove();");
                    BtnAddtax.Attributes.Add("onclick", "javascript:return validatetax();");
                    BtnAddlumpsum.Attributes.Add("onclick", "javascript:return validatelumptax();");
                    btnAddNewTerm.Attributes.Add("onclick", "javascript:return validateterms();");

                    ddlVendor.Items.Add(new ListItem("Select", "0"));
                    ddlRcvr.Items.Add(new ListItem("--Select--", "0"));
                    ddlAltRcvr.Items.Add(new ListItem("--Select--", "0"));
                    FIllObject.FillEmptyDropDown(ref ddlProjects);

                    objSort = new GridSort();
                    ViewState["Sort"] = objSort;
                    FIllObject.FillDropDown(ref ddltax, "GetRateTax");
                    FIllObject.FillDropDown(ref ddllumptax, "GetLumpsumTax");
                    ViewState["dtTerms"] = null;
                    BindTerms();
                    Terms();
                    GVTERMS.DataSource = null;
                    GVTERMS.DataBind();
                    GVAdditionalTerms.DataSource = null;
                    GVAdditionalTerms.DataBind();
                    int CompanyID = Convert.ToInt32(Session["CompanyID"].ToString());
                    if (Request.QueryString["token"] == null)
                    {

                        SqlParameter[] p = new SqlParameter[1];
                        if (CompanyID != null)
                            p[0] = new SqlParameter("@CompanyID", CompanyID);
                        else
                            p[0] = new SqlParameter("@CompanyID", SqlDbType.Int);
                        DataSet ds = SqlHelper.ExecuteDataset("PM_GetWorkSites", p);
                        ddlWorksite.DataSource = ds;
                        ddlWorksite.DataTextField = "Name";
                        ddlWorksite.DataValueField = "ID";
                        ddlWorksite.DataBind();

                        SqlParameter[] par = new SqlParameter[1];
                        par[0] = new SqlParameter("@EMPID",  Convert.ToInt32(Session["UserId"]));
                        DataSet dsWS = SqlHelper.ExecuteDataset("MMS_GetEmployeeWorksite", par);
                        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                        {
                            if (ddlWorksite.Items.FindByValue(dsWS.Tables[0].Rows[0]["WorkSite"].ToString()) != null)
                            {
                                ddlWorksite.SelectedValue = dsWS.Tables[0].Rows[0]["WorkSite"].ToString();
                            }
                            else
                            {
                                ddlWorksite.Items.Insert(0, new ListItem(dsWS.Tables[0].Rows[0]["WorkSite"].ToString(), dsWS.Tables[0].Rows[0]["Name"].ToString()));
                                ddlWorksite.DataBind();
                            }
                        }
                        BindPrj();

                      

                    }
                   
                    FIllObject.FillDropDown(ref ddlPayment, "PM_BillGenType");
                    if (ddlPayment.Items.Count > 0)
                        ddlPayment.SelectedIndex = 1;

                    SqlParameter[] P = new SqlParameter[2];
                    P[0] = new SqlParameter("@ID", 4);
                    P[1] = new SqlParameter("@type", 2);
                    FIllObject.FillDropDown(ref ddlTermsList, "PM_TERMSET", P);
                    if (ddlTermsList.Items.FindByValue("7") != null)
                        ddlTermsList.SelectedValue = "7";
                    dtGoodsList.Columns.Add(new DataColumn("GoodsID", typeof(System.Int32)));
                    dtGoodsList.Columns.Add(new DataColumn("Goods", typeof(System.String)));
                    dtGoodsList.Columns.Add(new DataColumn("Specification", typeof(System.String)));
                    dtGoodsList.Columns.Add(new DataColumn("Uom", typeof(System.String)));
                    dtGoodsList.Columns.Add(new DataColumn("Mileage", typeof(System.String)));
                    dtGoodsList.Columns.Add(new DataColumn("Purpose", typeof(System.String)));
                    dtGoodsList.Columns.Add(new DataColumn("Requiredon", typeof(System.DateTime)));
                    dtGoodsList.Columns.Add(new DataColumn("Quantity", typeof(System.Double)));
                    dtGoodsList.Columns.Add(new DataColumn("BasicRate", typeof(System.Double)));
                    dtGoodsList.Columns.Add(new DataColumn("Budget", typeof(System.Double)));
                    ViewState["Goods"] = dtGoodsList;
                    dtGoodsList = (DataTable)ViewState["Goods"];
                    dlIndents.DataSource = dtGoodsList;
                    dlIndents.DataBind();

                  
                    dsRoles = objProcdept.GetAuDetails();

                    foreach (DataRow drRole in dsRoles.Tables[0].Rows)
                        alRoles.Add(drRole["Au_Id"].ToString());
                   
                    htUOM = new Hashtable();
                    ViewState["htUOM"] = htUOM;

                    if (Request.QueryString["token"] != null)
                    {
                        ddlPayment.Enabled = false;
                        btnMove.Style.Remove("display"); btnMove.Style.Add("display", "none");
                        tableAddResources.Style.Remove("display"); tableAddResources.Style.Add("display", "none");
                        btnNewItem.Style.Remove("display"); btnNewItem.Style.Add("display", "none");
                        if (Request.QueryString["M"] == "1")
                        {
                            MainView.ActiveViewIndex = 1;
                            lblType.Text = "Goods";
                            lblGroupSearch.Text = "Child Procurement Goods Groups";
                            lblItemsSearch.Text = "Goods/Resource";
                            Type = 1;
                            AssetType = 1;
                            ViewState["AssetType"] = AssetType;
                            ParentGroups();
                        }
                        else
                        {
                            MainView.ActiveViewIndex = 1;
                            lblType.Text = "Services";
                            lblParent.Text = "Parent Accounts Services Group";
                            lblGroupSearch.Text = "Child Procurement Services Groups";
                            lblItemsSearch.Text = "Filter Services Items";
                            Type = 2;
                            AssetType = 0;
                            ViewState["AssetType"] = AssetType;
                            ParentGroups();
                        }

                        if (Request.QueryString["P"] != null)
                        {
                            DataSet dsProj = SqlHelper.ExecuteDataset("[OMS_DDL_GetPROJECTS]");
                            DataRow drProj = dsProj.Tables[0].Select("ProjectID=" + Request.QueryString["P"])[0];
                            ddlProjects.Items.Clear();
                            ddlProjects.Items.Insert(0, new ListItem(drProj["ProjectName"].ToString(), drProj["ProjectID"].ToString()));
                            ddlProjects.Items.Insert(0, new ListItem("---Select---", "0"));
                            if (Request.QueryString["M"] == "1")
                            {
                                txtpofor.Text = txtReqNo.Text = "Purchase for Proj# " + drProj["ProjectName"].ToString();

                            }
                            else
                            {
                                txtpofor.Text = txtReqNo.Text = "Hire/Service for Proj# " + drProj["ProjectName"].ToString();

                            }
                            SqlParameter[] sqlParms = new SqlParameter[1];
                            sqlParms[0] = new SqlParameter("@ProjectID", Convert.ToInt32(Request.QueryString["P"]));
                            FIllObject.FillDropDown(ref ddlWorksite, "GEN_DDL_WorkSitesByProject", sqlParms);
                            ddlProjects.SelectedValue = Request.QueryString["P"];
                            ddlProjects.Enabled = false;
                            ddlWorksite.AutoPostBack = false;
                            if (ddlWorksite.Items.Count > 1)
                                ddlWorksite.SelectedIndex = 1;
                            btnSearcWorksite.Visible = txtSearchWorksite.Visible = false;
                        }
                        AutoFillResource();

                        //recivers & Monitors
                        FIllObject.FillEmptyDropDown(ref ddlRcvr);
                        FIllObject.FillEmptyDropDown(ref ddlAltMnStaff);
                        FIllObject.FillEmptyDropDown(ref ddlMnStaff);
                        FIllObject.FillEmptyDropDown(ref ddlAltRcvr);
                        BindMonitors();
                        ddlRcvr.Items.Add(new ListItem(Session["Username"].ToString(),  Convert.ToInt32(Session["UserId"]).ToString()));
                        ddlAltMnStaff.Items.Add(new ListItem(Session["Username"].ToString(),  Convert.ToInt32(Session["UserId"]).ToString()));
                        ddlMnStaff.Items.Add(new ListItem(Session["Username"].ToString(),  Convert.ToInt32(Session["UserId"]).ToString()));
                        ddlAltRcvr.Items.Add(new ListItem(Session["Username"].ToString(),  Convert.ToInt32(Session["UserId"]).ToString()));
                        ddlRcvr.SelectedValue = ddlAltMnStaff.SelectedValue = ddlMnStaff.SelectedValue = ddlAltRcvr.SelectedValue =  Convert.ToInt32(Session["UserId"]).ToString();
                    }


                }
                dvAddRcvrs.Visible = false;
                dvAddAltRcvrs.Visible = false;
                dvAddMntrs.Visible = false;
                dvAddAltMntr.Visible = false;
                btnAddNewRcvr.Attributes.Add("OnClick", "javascript:return ValidateRcvrs();");
                btnAddNewAltRcvr.Attributes.Add("OnClick", "javascript:return ValidateAltRcvrs();");
                btnAddMntr.Attributes.Add("OnClick", "javascript:return ValidateMntr();");
                btnAddAltMntr.Attributes.Add("OnClick", "javascript:return ValidateAltMntr();");
                Worksite = Convert.ToInt32(ddlWorksite.SelectedValue);
                Call_CalculateValue();
                WSEnable();

            }
            catch (Exception ex)
            {
                AlertMsg.MsgBox(Page, ex.ToString() + ex.StackTrace);
            }
        }

        private void AutoFillResource()
        {

            htUOM = (Hashtable)ViewState["htUOM"];

            ProcDept objProcdept = new ProcDept();
            DataTable dtGoodsList = new DataTable();
            DataRow dtRow;
            dtGoodsList = (DataTable)ViewState["Goods"];
             
            string strDuplicateItems = "Duplicated Items are not allowed.\nDetails:\n";
            bool duplicatesFound = false;
            SqlParameter[] sqlprms = new SqlParameter[1];
            sqlprms[0] = new SqlParameter("@Token", Request.QueryString["token"]);
            DataSet dsRes = SqlHelper.ExecuteDataset("[OMS_GetResourcesForQuickTrans]", sqlprms);
            if (dsRes.Tables[0].Rows.Count == 0)
            {
                btnSubmit.Enabled = false;
                AlertMsg.MsgBox(Page, "Invalid operation! No resources selected to do transaction.");
                Response.Redirect("QuickTrans.aspx");
            }
            foreach (DataRow indexItem in dsRes.Tables[0].Rows)
            {
                DataRow[] dtRows = dtGoodsList.Select("GoodsID=" + indexItem["ResourceID"]);
                if (dtRows.Length > 0)
                {
                    strDuplicateItems += indexItem["Spec"] + "\n";
                    duplicatesFound = true;
                }
                else
                {

                    dtRow = dtGoodsList.NewRow();
                    dtRow["GoodsID"] = indexItem["ResourceID"];
                    dtRow["Goods"] = indexItem["Spec"];
                    dtRow["Specification"] = "NA";
                    dtRow["Uom"] = indexItem["AU_ID"];

                    dtRow["Purpose"] = indexItem["Purpose"];
                    dtRow["Requiredon"] = (DateTime)indexItem["ReqOn"];
                    dtRow["Quantity"] = indexItem["Quantity"];
                    dtRow["BasicRate"] = indexItem["Rate"];
                    dtRow["Budget"] = Convert.ToDouble(indexItem["Quantity"]) * Convert.ToDouble(indexItem["Rate"]);
                    dtGoodsList.Rows.Add(dtRow);


                }

                if (!htUOM.Contains(indexItem["ResourceID"].ToString()))
                {
                    dsUOM = objProcdept.GetUnitsByResource(Convert.ToInt32(indexItem["ResourceID"]), Convert.ToInt32(Request.QueryString["M"]));
                    htUOM.Add(indexItem["ResourceID"].ToString(), dsUOM.Copy());
                }
            }

            dtGoodsList.AcceptChanges();
            ViewState["Goods"] = dtGoodsList;

            if (duplicatesFound)
                AlertMsg.MsgBox(Page, strDuplicateItems);

            dtGoodsList = (DataTable)ViewState["Goods"];
            dlIndents.DataSource = dtGoodsList;
            dlIndents.DataBind();
            dlIndents.Columns[10].Visible = false;
            if (dlIndents.Rows.Count > 0)
                dlIndents.Visible = true;
            else
                dlIndents.Visible = false;

            BindVendors("", false);
            string strScript = "<script language='javascript'> CalculatePOValue();</script>";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "CalculatePO", strScript, false);

            WSEnable();

        }

        private void WSEnable()
        {
            if (dlIndents.Rows.Count > 0)
                ddlWorksite.Enabled = false;
            else
                ddlWorksite.Enabled = true;
        }

        public static DataSet dsRoles = new DataSet();

        public static ArrayList alRoles = new ArrayList();

        #region Methods
        void GoodsGroupFilter(string RowFilter, EventArgs e)
        {
            dsGoodsGroup = (DataSet)ViewState["dsGoodsGroup"];
            DataView dvGoodsgroup = dsGoodsGroup.Tables[0].DefaultView;
            dvGoodsgroup.RowFilter = RowFilter;
            if (dvGoodsgroup.Count != 0)
                FIllObject.FillListBox(ref lbxGGroup, dvGoodsgroup, "Category", "category_Id");
            else
            {
                lbxGGroup.Items.Clear();
                lbxGGroup.Items.Insert(0, new ListItem("No Records", "0"));
            }
            if (lbxGGroup.Items.Count == 0)
            {
                lbxItems.Items.Clear();
                lbxGGroup.Items.Add(new ListItem("No Records", "0"));
                lbxItems.Items.Add(new ListItem("No Records", "0"));
            }
            else
            {
                lbxGGroup.SelectedIndex = 0;
                lbxGGroup_SelectedIndexChanged(this, e);
            }
        }

        public int GetRolesIndex(string Value)
        {
            return alRoles.IndexOf(Value);
        }

        private string CreateFolder(string FolderName)
        {

            if (!Directory.Exists(Server.MapPath(".\\Quotations\\" + FolderName)))
            {
                DirectoryInfo oDirectoryInfo;
                oDirectoryInfo = Directory.CreateDirectory(Server.MapPath(".\\Quotations\\" + FolderName));

            }

            return FolderName;

        }

        public void ParentGroups()
        {
            DataSet dsParentGroups = new DataSet();
            dsParentGroups = DirectPO_Obj.HMSGetParentGroups(AssetType, ModuleID);
            FIllObject.FillListBox(ref listParentAcGroup, dsParentGroups.Tables[0], "Name", "ID");
        }

        private void FillRcvrDropDowns()
        {
            SqlParameter[] p = new SqlParameter[2];
            p[0] = new SqlParameter("@Id", 2);
            p[1] = new SqlParameter("@Worksite", Worksite);
             
          DataSet  ds = SqlHelper.ExecuteDataset("PMS_GetMonitors", p);
            if (ds != null && ds.Tables.Count > 0)
            {
                try
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddlRcvr.DataSource = ds.Tables[0];
                        ddlRcvr.DataTextField = ds.Tables[0].Columns[1].ToString();
                        ddlRcvr.DataValueField = ds.Tables[0].Columns[0].ToString();
                        ddlRcvr.DataBind();
                     
                    }

                    if (ds.Tables[1].Rows.Count > 0)
                    {
                        ddlAltRcvr.DataSource = ds.Tables[1];
                        ddlAltRcvr.DataTextField = ds.Tables[1].Columns[1].ToString();
                        ddlAltRcvr.DataValueField = ds.Tables[1].Columns[0].ToString();
                        ddlAltRcvr.DataBind();
                       

                    }
                }
                catch { }
                ViewState["Rcvrs"] = ds;
            }
          
        }

        private void FillMntrDropDowns()
        {
            SqlParameter[] p = new SqlParameter[2];
            p[0] = new SqlParameter("@Id", 1);
            p[1] = new SqlParameter("@Worksite", MntrWS);
             
           DataSet   ds = SqlHelper.ExecuteDataset("PMS_GetMonitors", p);
            if (ds != null && ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlMnStaff.DataSource = ds.Tables[0];
                    ddlMnStaff.DataTextField = ds.Tables[0].Columns[1].ToString();
                    ddlMnStaff.DataValueField = ds.Tables[0].Columns[0].ToString();
                    ddlMnStaff.DataBind();
                    ddlMnStaff.SelectedIndex = 1;
                    //ddlMnStaff.Items.Insert(0, new ListItem("---Select---", "0"));
                }
                if (ds.Tables[1].Rows.Count > 0)
                {
                    ddlAltMnStaff.DataSource = ds.Tables[1];
                    ddlAltMnStaff.DataTextField = ds.Tables[1].Columns[1].ToString();
                    ddlAltMnStaff.DataValueField = ds.Tables[1].Columns[0].ToString();
                    ddlAltMnStaff.DataBind();
                    ddlAltMnStaff.SelectedIndex = 1;
                    // ddlAltMnStaff.Items.Insert(0, new ListItem("---Select---", "0"));
                }
                if (ddlAltMnStaff.Items.Count > 0)
                {
                    ddlAltMnStaff.Items.RemoveAt(0);
                }
                ddlAltMnStaff.Items.Insert(0, new ListItem("---Select---", "0"));

                if (ddlMnStaff.Items.Count > 0)
                {
                    ddlMnStaff.Items.RemoveAt(0);
                }
                ddlMnStaff.Items.Insert(0, new ListItem("---Select---", "0"));
                ViewState["Mntrs"] = ds;
            }
        }

        void GetWorksite()
        {
             
            SqlParameter[] p = new SqlParameter[2];
            p[0] = new SqlParameter("@EnqId", 1);
            DataSet ds = SqlHelper.ExecuteDataset("PMS_GetWorksite", p);
            if (ds != null)
                if (ds.Tables.Count > 0)
                    if (ds.Tables[0].Rows.Count > 0)
                        Worksite = Convert.ToInt32(ds.Tables[0].Rows[0][0]);
        }

        void Terms()
        {

            DataSet ds = SqlHelper.ExecuteDataset("PM_GET_Terms");
            DataTable Terms = new DataTable();
            Terms.Columns.Add("TermId", typeof(Int32));
            Terms.Columns.Add("Term", typeof(String));
            Terms.AcceptChanges();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dRow in ds.Tables[0].Rows)
                {
                    DataRow drTerm = Terms.NewRow();
                    drTerm["TermId"] = dRow["TermId"];
                    drTerm["Term"] = dRow["Term"];
                    Terms.Rows.Add(drTerm);
                }

                Terms.AcceptChanges();
            }
            ViewState["Terms"] = Terms;
        }

        private void GroupAndAccParentGroupReverseBind()
        {
            if (lbxItems.SelectedValue != "0" && lbxItems.SelectedValue != "")
            {
                int Group1 = 0;
                if (lbxGGroup.SelectedValue != "" && lbxGGroup.SelectedValue != null)
                    Group1 = Convert.ToInt32(lbxGGroup.SelectedValue);
                int ResId = Convert.ToInt32(lbxItems.SelectedValue);
                string ResName = lbxItems.SelectedItem.Text;
             

                  int catType;
                if (ViewState["AssetType"] != null)
                    AssetType = Convert.ToInt32(ViewState["AssetType"]);
                if (AssetType == 1)
                    catType = 1;
                else
                    catType = 2;
                DataSet ds = ProcDept.GetGroupIDByResource(ResId, catType);
                lbxGGroup.AutoPostBack = false;
                int Group2 = Convert.ToInt32(ds.Tables[0].Rows[0]["Category_Id"].ToString());
                if (Group1 != 0)
                {
                    if (Group1 != Group2)
                    {
                        if (lbxGGroup.Items.FindByValue(ds.Tables[0].Rows[0]["Category_Id"].ToString()) != null)
                        {
                            lbxGGroup.SelectedValue = ds.Tables[0].Rows[0]["Category_Id"].ToString();
                            //AlertMsg.MsgBox(Page, ResName + " is Under " + lbxGGroup.SelectedItem.Text + " Group");
                        }
                        else
                        {
                            string Category_Id = ds.Tables[0].Rows[0]["Category_Id"].ToString();
                            string Category_Name = ds.Tables[0].Rows[0]["Category_Name"].ToString();
                            lbxGGroup.Items.Insert(1, new ListItem(Category_Name, Category_Id));
                            lbxGGroup.SelectedIndex = 1;
                        }
                        AlertMsg.MsgBox(Page, ResName + " is Under " + lbxGGroup.SelectedItem.Text + " Group");
                        int parentGroup = 0;
                        string GroupName = "0";
                        if (listParentAcGroup.SelectedItem != null)
                        {
                            parentGroup = Convert.ToInt32(listParentAcGroup.SelectedItem.Value);
                            GroupName = listParentAcGroup.SelectedItem.Text;
                        }

                       
                        if (ViewState["AssetType"] != null)
                            AssetType = Convert.ToInt32(ViewState["AssetType"]);
                        DataSet dsAsset = ProcDept.GetAssetTpeIDByResource(ResId, AssetType);
                        listParentAcGroup.AutoPostBack = false;
                        int Group3 = Convert.ToInt32(dsAsset.Tables[0].Rows[0]["AssetTypeId"].ToString());

                        if (parentGroup != Group3)
                        {
                            if (listParentAcGroup.Items.FindByValue(dsAsset.Tables[0].Rows[0]["AssetTypeId"].ToString()) != null)
                            {
                                listParentAcGroup.SelectedValue = dsAsset.Tables[0].Rows[0]["AssetTypeId"].ToString();
                                AlertMsg.MsgBox(Page, lbxGGroup.SelectedItem.Text + " is Under " + listParentAcGroup.SelectedItem.Text + " Group");
                            }
                            else
                            {
                                string ParentAcGroupID = dsAsset.Tables[0].Rows[0]["AssetTypeId"].ToString();
                                string ParentAcGroupName = dsAsset.Tables[0].Rows[0]["AssetType"].ToString();
                                listParentAcGroup.Items.Insert(0, new ListItem(ParentAcGroupName, ParentAcGroupID));
                                listParentAcGroup.SelectedIndex = 0;
                            }

                        }
                    }
                }
                else
                {
                    if (lbxGGroup.Items.FindByValue(ds.Tables[0].Rows[0]["Category_Id"].ToString()) != null)
                        lbxGGroup.SelectedValue = ds.Tables[0].Rows[0]["Category_Id"].ToString();
                    else
                    {
                        string Category_Id = ds.Tables[0].Rows[0]["Category_Id"].ToString();
                        string Category_Name = ds.Tables[0].Rows[0]["Category_Name"].ToString();
                        lbxGGroup.Items.Insert(0, new ListItem(Category_Name, Category_Id));
                        lbxGGroup.SelectedIndex = 0;
                    }

                    if (ViewState["AssetType"] != null)
                        AssetType = Convert.ToInt32(ViewState["AssetType"]);
                    DataSet dsAsset = ProcDept.GetAssetTpeIDByResource(ResId, AssetType);

                    if (listParentAcGroup.Items.FindByValue(dsAsset.Tables[0].Rows[0]["AssetTypeId"].ToString()) != null)
                        listParentAcGroup.SelectedValue = dsAsset.Tables[0].Rows[0]["AssetTypeId"].ToString();
                    else
                    {
                        string ParentAcGroupID = dsAsset.Tables[0].Rows[0]["AssetTypeId"].ToString();
                        string ParentAcGroupName = dsAsset.Tables[0].Rows[0]["AssetType"].ToString();
                        listParentAcGroup.Items.Insert(0, new ListItem(ParentAcGroupName, ParentAcGroupID));
                        listParentAcGroup.SelectedIndex = 0;
                    }

                }

                listParentAcGroup.AutoPostBack = true;
                lbxGGroup.AutoPostBack = true;
            }
        }

        private DataSet GetEmployesByDept(int Worksite, int DeptId)
        {
            SqlParameter[] p = new SqlParameter[4];
            p[0] = new SqlParameter("@ID", 2);
            p[1] = new SqlParameter("@DeptId", DeptId);
            p[2] = new SqlParameter("@WorkSite", Worksite);
             
          DataSet  ds = SqlHelper.ExecuteDataset("PMS_GetEmployesByDept", p);
            return ds;
        }

        private DataSet GetMonitorsByDept(int Worksite, int DeptId, int HeadOffice)
        {

            SqlParameter[] p = new SqlParameter[4];
            p[0] = new SqlParameter("@ID", 1);
            p[1] = new SqlParameter("@DeptId", DeptId);
            p[2] = new SqlParameter("@WorkSite", Worksite);
            p[3] = new SqlParameter("@HeadOfficeWS", HeadOffice);
            DataSet ds = SqlHelper.ExecuteDataset("PMS_GetEmployesByDept", p);
            return ds;
        }

        public int GetParentMenuId()
        {
            string URL = Request.Url.Segments[Request.Url.Segments.Length - 1] + Request.Url.Query;
            int RoleId = Convert.ToInt32(Session["RoleId"].ToString());
            int ModuleId = ModuleID;;

             
            ProcDept objProc = new ProcDept();
          DataSet  ds = ProcDept.GetAllowed(RoleId, ModuleId, URL);
            int MenuId = 0;
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                MenuId = Convert.ToInt32(ds.Tables[0].Rows[0]["Under"]);
               
                dlIndents.Columns[9].Visible = (bool)ds.Tables[0].Rows[0]["Editable"];
              
                Editable = (bool)ds.Tables[0].Rows[0]["Editable"];

                btnMove.Enabled = btnSubmit.Enabled = btnAddNewTerm.Enabled = BtnAddlumpsum.Enabled = BtnAddtax.Enabled = Editable;
            }
            return MenuId;
        }

        #endregion Methods

        #region Bindmethods

        public void BindWorksites()
        {
            if (Session["Site"].ToString() != "1")
            {
                ddlWorksite.Items.FindByValue(Session["Site"].ToString()).Selected = true;
                SqlParameter[] p = new SqlParameter[1];
                p[0] = new SqlParameter("@Worksite", ddlWorksite.SelectedValue);
                FIllObject.FillDropDown(ref ddlProjects, "OMS_GetProjectByWorksite", p);
                ddlWorksite.Enabled = false;
            }
            else
            {
                ddlWorksite.Items.FindByValue(Session["Site"].ToString()).Selected = true;
                SqlParameter[] p1 = new SqlParameter[1];
                p1[0] = new SqlParameter("@Worksite", ddlWorksite.SelectedValue);
                FIllObject.FillDropDown(ref ddlProjects, "OMS_GetProjectByWorksite", p1);
                ddlWorksite.Enabled = true;
            }

        }

        public DataSet BindRoles()
        {
            return dsRoles;
        }

        void BindGroupListbox()
        {
            int? AssetTypeID = null;
            if (listParentAcGroup.SelectedIndex > -1)
            {
                AssetTypeID = Convert.ToInt32(listParentAcGroup.SelectedItem.Value);
            }
            if (Type == 2)
            {
                lbltext.Text = "(Hiring)";
            }
            else
            {
                lbltext.Text = "(Purchase)";
            }
            SqlParameter[] p = new SqlParameter[4];
            p[0] = new SqlParameter("@id", 1);
            p[1] = new SqlParameter("@Cat", "");
            p[2] = new SqlParameter("@Cat_Type", Type);
            p[3] = new SqlParameter("@AssetType", AssetTypeID);
            dsGoodsGroup = SqlHelper.ExecuteDataset("SP_PM_SearchCategories", p);
            ViewState["dsGoodsGroup"] = dsGoodsGroup;
            //  FIllObject.FillListBox(ref lbxGGroup, dsGoodsGroup.Tables[0], "Category_Name", "Category_Id");
            FIllObject.FillListBox(ref lbxGGroup, dsGoodsGroup.Tables[0], "Category", "Category_Id");
            BindGroupstoListBoxItems();

            if (lbxGGroup.Items.Count == 0)
            {
                lbxGGroup.Items.Add(new ListItem("No Records", "0"));
                lbxItems.Items.Clear();
                lbxItems.Items.Add(new ListItem("No Records", "0"));
            }
            else
            {
                lbxGGroup.SelectedIndex = 0;
            }
        }

        void BindGroupstoListBoxItems()
        {
            string strScript = "<script language='javascript'>ShowGroups('" + lbxItems.ClientID + "');</script>";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "PopupCP", strScript, false);
        }

        protected void BindVendors(string Filter, Boolean isFilter)
        {
            ddlVendor.Items.Clear();

            foreach (GridViewRow row in dlIndents.Rows)
            {
                Label lblitemid = new Label();
                lblitemid = (Label)row.FindControl("itemid");

                SqlDataReader dr;
                SqlParameter[] parms = new SqlParameter[2];
                if (isFilter)
                    parms[0] = new SqlParameter("@itemid", SqlDbType.Int);
                else
                    parms[0] = new SqlParameter("@itemid", lblitemid.Text);

                parms[1] = new SqlParameter("@Filter", Filter);

                dr = SqlHelper.ExecuteReader("PM_GetItemSuppliers", parms);
                if (dr.HasRows)
                {
                    while (dr.Read())
                        ddlVendor.Items.Add(new ListItem(dr[1].ToString(), dr[0].ToString()));
                }
                dr.Close();

            }

            SortedList si = new SortedList();
            foreach (ListItem item in ddlVendor.Items)
            {
                if (si.ContainsValue(item.Value) == false) { si.Add(item.Text, item.Value); }

            }
            ddlVendor.DataSource = si;
            ddlVendor.DataTextField = "Key";
            ddlVendor.DataValueField = "Value";
            ddlVendor.DataBind();

            ddlVendor.Items.Insert(0, new ListItem("Select", "0"));
        }

        private void BindTerms()
        {

            DataSet ds = SqlHelper.ExecuteDataset("PM_GET_Terms");

            DataTable dtTerms = new DataTable();
            dtTerms.Columns.Add("TermId", typeof(Int32));
            dtTerms.Columns.Add("Term", typeof(String));
            dtTerms.Columns.Add("ShortTerm", typeof(String));
            dtTerms.Columns.Add("SLNO", typeof(Int32));
            dtTerms.AcceptChanges();

            DataTable dtChkedTerms = new DataTable();
            dtChkedTerms.Columns.Add("TermId", typeof(Int32));
            dtChkedTerms.Columns.Add("Term", typeof(String));
            dtChkedTerms.Columns.Add("SLNO", typeof(Int32));
            dtChkedTerms.AcceptChanges();


            lstTerms.Items.Clear();
            // lstTerms.Items.Insert(0, new ListItem("--------Select Term--------------", "0"));
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dRow in ds.Tables[0].Rows)
                {
                    ListItem li1 = new ListItem(dRow["ShortTerm"].ToString(), dRow["TermId"].ToString());
                    li1.Attributes.Add("title", dRow["Term"].ToString());
                    lstTerms.Items.Add(li1);
                    DataRow drTerm = dtTerms.NewRow();
                    drTerm["TermId"] = dRow["TermId"];
                    drTerm["Term"] = dRow["Term"];
                    drTerm["ShortTerm"] = dRow["ShortTerm"];
                    dtTerms.Rows.Add(drTerm);
                }

                dtTerms.AcceptChanges();
            }

            ViewState["dtTerms"] = dtTerms;
            ViewState["dtChkedTerms"] = dtChkedTerms;

        }

        void BindMntrsDepts()
        {

            // dvMntrs.Visible = false;
            Worksite = Convert.ToInt32(ddlWorksite.SelectedValue);
            SqlParameter[] p = new SqlParameter[1];
            p[0] = new SqlParameter("@Worksite", MntrWS);

            DataSet ds = SqlHelper.ExecuteDataset("PMS_GetDepts", p);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ddlMntrsDept.DataSource = ds;
                ddlMntrsDept.DataTextField = ds.Tables[0].Columns[1].ToString();
                ddlMntrsDept.DataValueField = ds.Tables[0].Columns[0].ToString();


                ddlAltMntrsDept.DataSource = ds;
                ddlAltMntrsDept.DataTextField = ds.Tables[0].Columns[1].ToString();
                ddlAltMntrsDept.DataValueField = ds.Tables[0].Columns[0].ToString();


            }
            ddlMntrsDept.DataBind();
            ddlMntrsDept.Items.Insert(0, new ListItem("----Select Dept----", "0"));
            ddlAltMntrsDept.DataBind();
            ddlAltMntrsDept.Items.Insert(0, new ListItem("----Select Dept----", "0"));
        }

        void BindMonitors()
        {
            int ProjId = Convert.ToInt32(ddlWorksite.SelectedValue);
            SqlParameter[] p = new SqlParameter[2];
            int Val = 1;
            if (chkEmp.Checked)
                Val = 0;
            p[0] = new SqlParameter("@PId", Val); //if @PId=1 for Head Office  else other Worksites
            p[1] = new SqlParameter("@SiteId", ProjId);
            FIllObject.FillDropDown(ref ddlAltMnStaff, "PM_GetMonitorEmployees", p);
            FIllObject.FillDropDown(ref ddlMnStaff, "PM_GetMonitorEmployees", p);
            if (ddlMnStaff.Items.Count > 1)
            {
                EmpIdExists = ddlMnStaff.Items.FindByValue( Convert.ToInt32(Session["UserId"]).ToString());
                if (EmpIdExists != null)
                {
                    ddlMnStaff.SelectedValue =  Convert.ToInt32(Session["UserId"]).ToString();
                    EmpIdExists = null;
                }
                else
                    ddlMnStaff.SelectedIndex = 1;
            }
            if (ddlAltMnStaff.Items.Count > 1)
            {
                EmpIdExists = ddlAltMnStaff.Items.FindByValue( Convert.ToInt32(Session["UserId"]).ToString());
                if (EmpIdExists != null)
                {
                    ddlAltMnStaff.SelectedValue =  Convert.ToInt32(Session["UserId"]).ToString();
                    EmpIdExists = null;
                }
                else
                    ddlAltMnStaff.SelectedIndex = 1;
            }
        }

        void BindReceivers(int ProjectId)
        {
            SqlParameter[] p = new SqlParameter[2];
            int Val = 2;
            if (chkEmp.Checked)
                Val = 0;
            p[0] = new SqlParameter("@SiteId", ProjectId);
            p[1] = new SqlParameter("@PId", Val); //if @PId=1 for Head Office  else other Worksites
            FIllObject.FillDropDown(ref ddlAltRcvr, "PM_GetMonitorEmployees", p);
            if (ddlAltRcvr.Items.Count > 1)
            {
                EmpIdExists = ddlAltRcvr.Items.FindByValue( Convert.ToInt32(Session["UserId"]).ToString());
                if (EmpIdExists != null)
                {
                    ddlAltRcvr.SelectedValue =  Convert.ToInt32(Session["UserId"]).ToString();
                    EmpIdExists = null;

                }
                else
                    if (ddlAltRcvr.SelectedIndex == 0)
                        ddlAltRcvr.SelectedIndex = 1;
            }
            SqlParameter[] p1 = new SqlParameter[2];
            p1[0] = new SqlParameter("@SiteId", ProjectId);
            p1[1] = new SqlParameter("@PId", Val); //if @PId=1 for Head Office  else other Worksites
            FIllObject.FillDropDown(ref ddlRcvr, "PM_GetMonitorEmployees", p1);
            if (ddlRcvr.Items.Count > 1)
            {
                EmpIdExists = ddlRcvr.Items.FindByValue( Convert.ToInt32(Session["UserId"]).ToString());
                if (EmpIdExists != null)
                {
                    ddlRcvr.SelectedValue =  Convert.ToInt32(Session["UserId"]).ToString();
                    EmpIdExists = null;
                }
                else
                    if (ddlRcvr.SelectedIndex == 0)
                        ddlRcvr.SelectedIndex = 1;
            }
        }

        void BindStrRcvrsDepts()
        {
            // dvRcvrs.Visible = false;

            Worksite = Convert.ToInt32(ddlWorksite.SelectedValue);
            SqlParameter[] p = new SqlParameter[1];
            p[0] = new SqlParameter("@Worksite", Worksite);

            DataSet ds = SqlHelper.ExecuteDataset("PMS_GetDepts", p);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ddlStrRcvrDept.DataSource = ds;
                ddlStrRcvrDept.DataTextField = ds.Tables[0].Columns[1].ToString();
                ddlStrRcvrDept.DataValueField = ds.Tables[0].Columns[0].ToString();

                ddlAltRcvrDept.DataSource = ds;
                ddlAltRcvrDept.DataTextField = ds.Tables[0].Columns[1].ToString();
                ddlAltRcvrDept.DataValueField = ds.Tables[0].Columns[0].ToString();

            }
            ddlStrRcvrDept.DataBind();
            ddlStrRcvrDept.Items.Insert(0, new ListItem("----Select Dept----", "0"));
            ddlAltRcvrDept.DataBind();
            ddlAltRcvrDept.Items.Insert(0, new ListItem("----Select Dept----", "0"));
        }

        #endregion Bindmethods

        #region BtnOnclick
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            SqlParameter[] p = new SqlParameter[4];
            p[0] = new SqlParameter("@id", 1);
            p[1] = new SqlParameter("@Desc", txtFilter.Text);
            p[2] = new SqlParameter("@GroupId", 0);
            p[3] = new SqlParameter("@Cat_Type", Type);
            FIllObject.FillListBox(ref lbxItems, "PM_GroupWiseItems_Indent", p);
            if (lbxItems.Items.Count == 0) { lbxItems.Items.Add(new ListItem("No Records", "0")); }
            txtFilter.Text = "";
        }

        protected void btnSearchGroups_Click(object sender, EventArgs e)
        {
            BindGroupListbox();
            GoodsGroupFilter("Category_Name like '%" + txtFilterGroups.Text.Trim() + "%'", e);
        }

        protected void btnMove_Click(object sender, EventArgs e)
        {
            if (lbxItems.SelectedValue == "" || lbxGGroup.SelectedValue == "0")
                   {
                       AlertMsg.MsgBox(Page, "No Records Found");
                   }
            else{
            if (ddlWorksite.SelectedValue != "0" && ddlWorksite.SelectedValue != "")
            {
                htUOM = (Hashtable)ViewState["htUOM"];

                ProcDept objProcdept = new ProcDept();
                DataTable dtGoodsList = new DataTable();
                DataRow dtRow;
                dtGoodsList = (DataTable)ViewState["Goods"];
                 
                ListItem item = null;
                string strDuplicateItems = "Duplicated Items are not allowed.\nDetails:\n";
                bool duplicatesFound = false;
                int tmpUOM = 0;

                foreach (int indexItem in lbxItems.GetSelectedIndices())
                {
                    item = lbxItems.Items[indexItem];
                    DataRow[] dtRows = dtGoodsList.Select("GoodsID=" + item.Value);
                    if (dtRows.Length > 0)
                    {
                        strDuplicateItems += item.Text + "\n";
                        duplicatesFound = true;
                    }
                    else
                    {
                        DataSet ds = objProcdept.GetItemDescriptionByID(Convert.ToInt32(item.Value));
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            dtRow = dtGoodsList.NewRow();
                            dtRow["GoodsID"] = item.Value;
                            dtRow["Goods"] = item.Text;
                            dtRow["Specification"] = "";
                            dtRow["Uom"] = ds.Tables[0].Rows[0]["uom"].ToString();
                            //dtRow["Mileage"] = ds.Tables[0].Rows[0]["uom"].ToString();
                            tmpUOM = Convert.ToInt32(ds.Tables[0].Rows[0]["uom"].ToString());

                            dtRow["Purpose"] = "";
                            dtRow["Requiredon"] = DateTime.Now;
                            dtRow["Quantity"] = "1";
                            dtRow["BasicRate"] = ds.Tables[0].Rows[0]["basicrate"].ToString();
                            dtRow["Budget"] = 0;
                            dtGoodsList.Rows.Add(dtRow);
                            item.Selected = false;
                        }
                    }
                }

                //string[,] ArrayVal = new string[20,20];

                //int ii = 0;

                foreach (GridViewRow row in dlIndents.Rows)
                {

                    TextBox txtspec = new TextBox(); txtspec = (TextBox)row.FindControl("txtSpec");
                    TextBox txtpur = new TextBox(); txtpur = (TextBox)row.FindControl("txtPurpose");
                    TextBox txtqty = new TextBox(); txtqty = (TextBox)row.FindControl("txtQty");
                    TextBox txtdate = new TextBox(); txtdate = (TextBox)row.FindControl("txtReq");
                    TextBox txtrate = new TextBox(); txtrate = (TextBox)row.FindControl("txtrate");
                    Label lblItemId = new Label(); lblItemId = (Label)row.FindControl("itemid");
                    DataRow[] drSelected = dtGoodsList.Select("GoodsID='" + lblItemId.Text + "'");

                    drSelected[0]["Specification"] = txtspec.Text;
                    drSelected[0]["Purpose"] = txtpur.Text;
                    drSelected[0]["Requiredon"] = FIllObject.changedate(txtdate.Text);
                    drSelected[0]["Quantity"] = txtqty.Text;
                    drSelected[0]["BasicRate"] = txtrate.Text;
                    DropDownList ddlunits = new DropDownList(); ddlunits = (DropDownList)row.FindControl("ddlunits");
                    drSelected[0]["Uom"] = ddlunits.SelectedValue;
                    DropDownList ddlMunits = new DropDownList(); ddlMunits = (DropDownList)row.FindControl("ddlMunits");
                    drSelected[0]["Mileage"] = ddlMunits.SelectedValue;
                  

                }

                if (!htUOM.Contains(item.Value.ToString()))
                {
                    dsUOM = objProcdept.GetUnitsByResource(Convert.ToInt32(item.Value), Type);
                    htUOM.Add(item.Value.ToString(), dsUOM.Copy());
                    if (dsUOM.Tables[0].Select("ID=" + tmpUOM.ToString()).Length == 0)
                    {
                        dtRow = dtGoodsList.Select("GoodsID=" + item.Value)[0];
                        dtRow["Uom"] = "2";
                    }
                }

                dtGoodsList.AcceptChanges();
                ViewState["Goods"] = dtGoodsList;

                if (duplicatesFound)
                    AlertMsg.MsgBox(Page, strDuplicateItems);

                dtGoodsList = (DataTable)ViewState["Goods"];
                dlIndents.DataSource = dtGoodsList;
                dlIndents.DataBind();
                if (dlIndents.Rows.Count > 0)
                    dlIndents.Visible = true;
                else
                    dlIndents.Visible = false;

                BindVendors("", false);
                string strScript = "<script language='javascript'> CalculatePOValue();</script>";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "CalculatePO", strScript, false);


                WSEnable();
            }
            else { AlertMsg.MsgBox(Page, "Select Worksite!"); }
        }
        }

        protected void BtnGroupSearch_Click(object sender, EventArgs e)
        {
            txtFilterGroups.Text = "";
            GoodsGroupFilter("", e);
        }

        protected void BtnAddtax_Click(object sender, EventArgs e)
        {
            if (gvtax.Rows.Count == 0)
            {
                SqlParameter[] parms1 = new SqlParameter[3];
                parms1[0] = new SqlParameter("@id", 7);
                parms1[1] = new SqlParameter("@value", 0);
                parms1[2] = new SqlParameter("@equid", 1); // TvIndent.SelectedNode.Parent.Value);
                SqlHelper.ExecuteNonQuery("Temp_Tax", parms1);
            }
            foreach (GridViewRow row in gvtax.Rows)
            {
                Label lbltxid = new Label();
                lbltxid = (Label)row.FindControl("lbltxid");
                if (ddltax.SelectedValue == lbltxid.Text)
                {
                    AlertMsg.MsgBox(Page, ddltax.SelectedItem.Text + " Already Added");
                    return;
                }
            }

            SqlParameter[] parms = new SqlParameter[4];
            parms[0] = new SqlParameter("@id", 1);
            parms[1] = new SqlParameter("@taxid", ddltax.SelectedValue);
            parms[2] = new SqlParameter("@value", txttax.Text);
            parms[3] = new SqlParameter("@equid", 1);//TvIndent.SelectedNode.Parent.Value);
            SqlHelper.ExecuteNonQuery("Temp_Tax", parms);

            parms = new SqlParameter[2];
            parms[0] = new SqlParameter("@id", 3);
            parms[1] = new SqlParameter("@equid", 1); // TvIndent.SelectedNode.Parent.Value);
            FIllObject.FillGridview(ref gvtax, "Temp_Tax", parms);
            string strScript = "<script language='javascript' type='text/javascript'>   CalculatePOValue(); </script>";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "PopupCP", strScript, false);
            txttax.Text = "";
            ddltax.SelectedIndex = 0;
        }

        protected void btnAddNewTerm_Click(object sender, EventArgs e)
        {

            UpdateAditionalTerms();

            SqlParameter[] parms = new SqlParameter[4];
            parms[0] = new SqlParameter("@ID", 1);
            parms[1] = new SqlParameter("@Remarks", TxtTerms.Text);
            parms[2] = new SqlParameter("@VenId", 1); //TvIndent.SelectedValue);
            parms[3] = new SqlParameter("@EnqId", 1); //TvIndent.SelectedNode.Parent.Value);
            SqlHelper.ExecuteNonQuery("PM_INSERTREMARKS", parms);

            parms = new SqlParameter[4];
            parms[0] = new SqlParameter("@ID", 2);
            parms[1] = new SqlParameter("@Remarks", TxtTerms.Text);
            parms[2] = new SqlParameter("@VenId", 1); // TvIndent.SelectedValue);
            parms[3] = new SqlParameter("@EnqId", 1); // TvIndent.SelectedNode.Parent.Value);
            FIllObject.FillGridview(ref GVAdditionalTerms, "PM_INSERTREMARKS", parms);

            TxtTerms.Text = string.Empty;

            Accordion1.SelectedIndex = 1;

            string strScript = "<script language='javascript' type='text/javascript'>   CalculatePOValue(); </script>";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "PopupCP", strScript, false);
        }

        protected void BtnAddlumpsum_Click(object sender, EventArgs e)
        {

            if (GVlumpsum.Rows.Count == 0)
            {
                SqlParameter[] parms1 = new SqlParameter[3];
                parms1[0] = new SqlParameter("@id", 8);
                parms1[1] = new SqlParameter("@value", 0);
                parms1[2] = new SqlParameter("@equid", 1); // TvIndent.SelectedNode.Parent.Value);
                SqlHelper.ExecuteNonQuery("Temp_Tax", parms1);
            }

            foreach (GridViewRow row in GVlumpsum.Rows)
            {
                Label lbltxid = new Label();
                lbltxid = (Label)row.FindControl("lblltaxid");
                if (ddllumptax.SelectedValue == lbltxid.Text)
                {
                    AlertMsg.MsgBox(Page, ddllumptax.SelectedItem.Text + " Already Added");
                    return;
                }
            }

            SqlParameter[] parms = new SqlParameter[4];
            parms[0] = new SqlParameter("@id", 2);
            parms[1] = new SqlParameter("@taxid", ddllumptax.SelectedValue);
            parms[2] = new SqlParameter("@value", txtlumpsum.Text);
            parms[3] = new SqlParameter("@equid", 1); // TvIndent.SelectedNode.Parent.Value);
            SqlHelper.ExecuteNonQuery("Temp_Tax", parms);

            parms = new SqlParameter[2];
            parms[0] = new SqlParameter("@id", 4);
            parms[1] = new SqlParameter("@equid", 1); //TvIndent.SelectedNode.Parent.Value);
            FIllObject.FillGridview(ref GVlumpsum, "Temp_Tax", parms);
            string strScript = "<script language='javascript' type='text/javascript'>   CalculatePOValue(); </script>";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "PopupCP", strScript, false);
            ddllumptax.SelectedIndex = 0;
            txtlumpsum.Text = "";
        }

        protected void btnGoods_Click(object sender, EventArgs e)
        {
            MainView.ActiveViewIndex = 1;
            lblType.Text = "Goods";
            lblGroupSearch.Text = "Child Procurement Goods Groups";
            lblItemsSearch.Text = "Goods/Resource";
            Type = 1;
            AssetType = 1;
            ViewState["AssetType"] = AssetType;
            ParentGroups();
            chkTDS.Visible = false;
            lblPOFor.Text = "PO For";

        }

        protected void btnService_Click(object sender, EventArgs e)
        {
            MainView.ActiveViewIndex = 1;
            lblType.Text = "Services";
            lblParent.Text = "Parent Accounts Services Group";
            lblGroupSearch.Text = "Child Procurement Services Groups";
            lblItemsSearch.Text = "Filter Services Items";
            Type = 2;
            AssetType = 0;
            ViewState["AssetType"] = AssetType;
            ParentGroups();
            lblPOFor.Text = "WO For";
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            int UserId;
            UserId =  Convert.ToInt32(Session["UserId"]);
            SqlParameter[] Parm_Check = new SqlParameter[4];
            if (UserId != 0)
                Parm_Check[0] = new SqlParameter("@Empid", UserId);
            else
                Parm_Check[0] = new SqlParameter("@Empid", SqlDbType.Int);

            Parm_Check[1] = new SqlParameter("@POvalue", Convert.ToDouble(lblrate.Text));
            Parm_Check[2] = new SqlParameter("@Status_POWISE", 1);
            Parm_Check[3] = new SqlParameter("@Status_DayWise", 1);
            Parm_Check[2].Direction = ParameterDirection.Output;
            Parm_Check[3].Direction = ParameterDirection.Output;

            SqlHelper.ExecuteScalar("PMS_CheckPoLimits", Parm_Check);

            int POLimit = Convert.ToInt32(Parm_Check[2].Value);
            int DayLimit = Convert.ToInt32(Parm_Check[3].Value);

            if (POLimit != 0 && DayLimit != 0)
            {
                if (ddlVendor.SelectedValue == "0" || ddlVendor.SelectedValue == "")
                {
                    AlertMsg.MsgBox(Page, "Select Vendor");
                    return;
                }
                SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["strConn"].ToString());
                if (con.State == ConnectionState.Closed) con.Open();
                SqlTransaction trans = con.BeginTransaction(); ;

                try
                {

                    UserId =  Convert.ToInt32(Session["UserId"]);
                    int CompanyID = Convert.ToInt32(Session["CompanyID"].ToString());
                    int IndentId = DirectPO_Obj.InsertIndent(UserId, ddlWorksite.SelectedValue, txtReqNo.Text, Type, Convert.ToInt32(ddlProjects.SelectedValue), trans, CompanyID);

                    foreach (GridViewRow row in dlIndents.Rows)
                    {
                        TextBox Spec = new TextBox(); Spec = (TextBox)row.FindControl("txtSpec");
                        TextBox Pupose = new TextBox(); Pupose = (TextBox)row.FindControl("txtPurpose");
                        TextBox requiredon = new TextBox(); requiredon = (TextBox)row.FindControl("txtReq");
                        TextBox qty = new TextBox(); qty = (TextBox)row.FindControl("txtQty");
                        TextBox rate = new TextBox(); rate = (TextBox)row.FindControl("txtrate");
                        Label lblitemid = new Label(); lblitemid = (Label)row.FindControl("itemid");
                        DropDownList ddlunits = (DropDownList)row.FindControl("ddlunits");
                        DropDownList ddlMunits = (DropDownList)row.FindControl("ddlMunits");

                        DirectPO_Obj.InsertIndentItems(lblitemid.Text, IndentId.ToString(), Convert.ToInt32(ddlunits.SelectedValue), Convert.ToDouble(qty.Text), Spec.Text.Replace("\n", "<br/>"), Pupose.Text.Replace("\n", "<br/>"), FIllObject.changedate(requiredon.Text), Convert.ToDouble(rate.Text), Convert.ToInt32(ddlMunits.SelectedValue), trans);

                    }

                    int EnqID = DirectPO_Obj.InsertEnquiry(IndentId.ToString(), UserId, ddlVendor.SelectedValue, trans);
                    decimal TDS;
                    if (txtTDS.Text != "")
                        TDS = Convert.ToDecimal(txtTDS.Text);
                    else
                        TDS = 0;

                    int offerid = DirectPO_Obj.InsertOffer(EnqID.ToString(), ddlVendor.SelectedValue, trans, UserId, TDS, txtDelivery.Text, txtPayment.Text, txtMOT.Text);

                    DirectPO_Obj.InsertOfferItems(offerid.ToString(), IndentId.ToString(), trans);

                    foreach (GridViewRow row in gvtax.Rows)
                    {
                        Label lblTaxid = new Label();
                        lblTaxid = (Label)row.FindControl("lbltxid");
                        DirectPO_Obj.InsertOfferTax(lblTaxid.Text, Convert.ToDouble(row.Cells[1].Text), offerid.ToString(), trans, "3");
                    }

                    foreach (GridViewRow row in GVlumpsum.Rows)
                    {
                        Label lblTaxid = new Label();
                        lblTaxid = (Label)row.FindControl("lblltaxid");
                        DirectPO_Obj.InsertOfferTax(lblTaxid.Text, Convert.ToDouble(row.Cells[1].Text), offerid.ToString(), trans, "4");
                    }

                    foreach (GridViewRow row in GVTERMS.Rows)
                    {
                        CheckBox chk = new CheckBox(); chk = (CheckBox)row.FindControl("chk");
                        if (chk.Checked)
                        {
                            Label lblTermid = new Label(); lblTermid = (Label)row.FindControl("lblid");
                            TextBox txtterm = new TextBox(); txtterm = (TextBox)row.FindControl("TXTTERMS");
                            DirectPO_Obj.InsertOfferTerms(lblTermid.Text, txtterm.Text, offerid.ToString(), trans);
                        }
                    }

                    foreach (GridViewRow row in GVAdditionalTerms.Rows)
                    {
                        TextBox txtterm = new TextBox(); txtterm = (TextBox)row.FindControl("TXTTERMS");
                        DirectPO_Obj.InsertOfferTerms("0", txtterm.Text, offerid.ToString(), trans);
                    }

                  
                    DateTime? dtFromDate = null;
                    if (txtDate.Text == "")
                        txtDate.Text = DateTime.Now.Date.ToString();
                    dtFromDate = DateTime.Now;

                    int poid = DirectPO_Obj.InsertPO(offerid.ToString(), UserId, txtpofor.Text, Convert.ToDouble(lblrate.Text.Replace(",", "")), trans, Convert.ToInt32(ddlPayment.SelectedValue), dtFromDate, CompanyID);
                    if (poid > 0)
                    {
                        DirectPO_Obj.InsertPODetails(poid.ToString(), IndentId.ToString(), trans);
                        SqlParameter[] par = new SqlParameter[6];
                        par[0] = new SqlParameter("@POID", poid);
                        par[1] = new SqlParameter("@Receiver", ddlRcvr.SelectedValue);
                        par[2] = new SqlParameter("@AltRcvr", ddlAltRcvr.SelectedValue);
                        par[3] = new SqlParameter("@Monitor", ddlMnStaff.SelectedValue);
                        par[4] = new SqlParameter("@AltMntr", ddlAltMnStaff.SelectedValue);
                        par[5] = new SqlParameter("@Worksite", ddlWorksite.SelectedValue);
                       
                        SqlHelper.ExecuteNonQuery("PM_InsertMonitorsAtReleases", par);
                        txtReqNo.Text = ""; txtpofor.Text = "";
                        ddlPayment.SelectedIndex = 0;
                        ddlProjects.SelectedIndex = 0;
                        ddlWorksite.SelectedIndex = 0;
                        ddlMnStaff.SelectedIndex = 0;
                        ddlAltMnStaff.SelectedIndex = 0;
                        ddlRcvr.SelectedIndex = 0;
                        ddlAltRcvr.SelectedIndex = 0;
                        ddlTermsList.SelectedIndex = 0;

                        FIllObject.FillEmptygridview(ref dlIndents);
                        FIllObject.FillEmptygridview(ref gvtax);
                        FIllObject.FillEmptygridview(ref GVlumpsum);
                        FIllObject.FillEmptygridview(ref GVAdditionalTerms);
                        FIllObject.FillEmptygridview(ref GVTERMS);
                        TxtTerms.Text = "";
                        FIllObject.FillEmptyDropDown(ref ddlVendor);
                        BindTerms();
                        SqlParameter[] p = new SqlParameter[2];
                        p[0] = new SqlParameter("@ID", 3);
                        p[1] = new SqlParameter("@POID", poid);

                        int pono = Convert.ToInt32(SqlHelper.ExecuteScalar(trans, "PM_DIRECTPO_INSERT_PODETAILS", p));
                        lblPONo.Text = pono.ToString("0000");
                        trans.Commit();

                        SqlParameter[] tran = new SqlParameter[1];
                        tran[0] = new SqlParameter("@POID", pono);
                        DataSet ResultSet = SqlHelper.ExecuteDataset("MMS_QuickTrans", tran);

                        // IF Quick trans from OMS 
                        if (Request.QueryString["token"] != null)
                        {
                            try
                            {
                                SqlParameter[] sqlprms = new SqlParameter[2];
                                sqlprms[0] = new SqlParameter("@Token", Request.QueryString["token"]);
                                sqlprms[1] = new SqlParameter("@IndentID", IndentId);
                                DataSet dsRes = SqlHelper.ExecuteDataset("[OMS_UpdateProjectIndents]", sqlprms);
                            }
                            catch
                            {
                            }
                        }

                        string msg = "Purchase Order Raised with PO No";
                        //string ModuleName = "  for ";
                        string ModuleName = "";
                        if (lblType.Text == "Goods")  //RBlist.SelectedValue
                        {
                            lblRecievedNoteType.Text = "Goods Received Note(GRN) No";
                            lnkPO.Text = "Click Here to view Purchase Order(PO)";
                            lblIndentType.Text = "Purchase Order (PO)No";
                            msg = "Purchase Order Raised with PO No";
                            // ModuleName = "";
                        }
                        else
                        {
                            lblRecievedNoteType.Text = "Service Received Note(SRN) No";
                            lblIndentType.Text = "Work Order(WO) No";
                            lnkPO.Text = "Click Here to view Work Order(WO)";
                            msg = "Work Order Raised with WO No";
                            // ModuleName = "";
                        }
                        lnkPO.NavigateUrl = "ProPurchaseOrderPrint.aspx?id=" + poid + "&PON=" + Rblist.SelectedValue + "&tot=true";
                        if (ResultSet != null && ResultSet.Tables.Count > 0 && ResultSet.Tables[0].Rows.Count > 0)
                        {
                            try
                            {
                                lblRecievedNote.Text = Convert.ToInt32(ResultSet.Tables[0].Rows[0]["DNNo"]).ToString("0000");
                                lblBillNo.Text = Convert.ToInt32(ResultSet.Tables[0].Rows[0]["BillNo"]).ToString("0000");
                                lblTransID.Text = Convert.ToInt32(ResultSet.Tables[0].Rows[0]["TransID"]).ToString("0000");
                                if (lblType.Text == "Goods" && ResultSet.Tables[0].Rows[0]["Mode"].ToString() == "2")  //RBlist.SelectedValue
                                {
                                    lblRecievedNoteType.Text = "Equiptment Received Note(ERN) No";
                                }
                             
                            }
                            catch (Exception)
                            {

                            }

                        }
                        MainView.ActiveViewIndex = 2;


                      
                        DataTable dtGoodsList = (DataTable)ViewState["Goods"];
                        dtGoodsList.Rows.Clear();
                        ViewState["Goods"] = dtGoodsList;
                        string filename = "", ext = string.Empty, path = ""; string Newfolder = ""; string file = "";
                        string filename2 = "", ext2 = string.Empty, path2 = ""; string file2 = "";

                        if (fupQuatations.HasFile)
                        {
                            filename = fupQuatations.PostedFile.FileName;
                            if (filename != "")
                            {
                                ext = filename.Split('.')[filename.Split('.').Length - 1];
                                file = filename.Split('.')[filename.Split('.').Length - 2];
                               

                            }
                            else
                            {
                                if (ViewState["Image"].ToString() != "")
                                {
                                    ext = ViewState["Image"].ToString();
                                }
                                else
                                {
                                    ext = "";
                                }
                            }

                        }

                        if (filename != "")
                        {
                            Newfolder = CreateFolder(offerid.ToString());
                            path = Server.MapPath(".") + "\\" + "Quotations" + "\\" + Newfolder + "\\" + file + "." + ext;
                            ViewState["fupquataion"] = path;
                            try
                            {
                                fupQuatations.PostedFile.SaveAs(path);
                            }
                            catch
                            {
                                throw new Exception("fupQuatations.PostedFile.SaveAs(path)");
                            }

                        }

                        if (!Directory.Exists(Server.MapPath(".\\Quotations" + "\\" + Newfolder)))
                            Directory.CreateDirectory(Server.MapPath(".\\Quotations" + "\\" + Newfolder));
                    }
                    ddlWorksite.Enabled = true;
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    AlertMsg.MsgBox(Page, ex.Message.ToString(),AlertMsg.MessageType.Error);
                }
            }
            else
            {
                if (POLimit == 0) { AlertMsg.MsgBox(Page,"Amount Exceeding your Purchase Limit"); }
                if (DayLimit == 0) { AlertMsg.MsgBox(Page,"Amount Exceeding your Day Purchase Limit"); }
            }
        }

        //void AlertMsg.MsgBox(Page,string alert)
        //{
        //    string strScript = "<script language='javascript'>alert(\'" + alert + "\');</script>";
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "PopupCP", strScript, false);
        //}


        protected void btnAddNewRcvr_Click(object sender, EventArgs e)
        {
           
            string value = ddlNewRcvrsByDept.SelectedValue;
            string text = ddlNewRcvrsByDept.SelectedItem.Text;


            if (ddlRcvr.Items.FindByValue(value) == null)
            {
                ddlRcvr.Items.Add(new ListItem(ddlNewRcvrsByDept.SelectedItem.Text, ddlNewRcvrsByDept.SelectedValue));
                msg = "Done!";
            }
            else
            {
                msg = "Already Exists!";
            }

            AlertMsg.MsgBox(Page, msg);

            ddlRcvr.SelectedValue = ddlNewRcvrsByDept.SelectedValue;
            dvAddRcvrs.Visible = false;
        }

        protected void btnAddNewAltRcvr_Click(object sender, EventArgs e)
        {
            string value = ddlNewAltRcvrByDept.SelectedValue;
            string text = ddlNewAltRcvrByDept.SelectedItem.Text;
            if (ddlAltRcvr.Items.FindByValue(value) == null)
            {
                ddlAltRcvr.Items.Add(new ListItem(ddlNewAltRcvrByDept.SelectedItem.Text, ddlNewAltRcvrByDept.SelectedValue));
                msg = "Done!";
            }
            else
            {
                msg = "Already Exists!";
            }

            AlertMsg.MsgBox(Page, msg);
            ddlAltRcvr.SelectedValue = ddlNewAltRcvrByDept.SelectedValue;
            dvAddAltRcvrs.Visible = false;

        }

        protected void btnAddMntr_Click(object sender, EventArgs e)
        {
            string value = ddlAddNewMntrByDept.SelectedValue;
            string text = ddlAddNewMntrByDept.SelectedItem.Text;
            if (ddlMnStaff.Items.FindByValue(value) == null)
            {
                ddlMnStaff.Items.Add(new ListItem(ddlAddNewMntrByDept.SelectedItem.Text, ddlAddNewMntrByDept.SelectedValue));
                msg = "Done!";
            }
            else
            {
                msg = "Already Exists!";
            }

            AlertMsg.MsgBox(Page, msg);
            ddlMnStaff.SelectedValue = ddlAddNewMntrByDept.SelectedValue;
            dvAddMntrs.Visible = false;
        }

        protected void btnAddAltMntr_Click(object sender, EventArgs e)
        {
            string value = ddlNewAltMntrByDept.SelectedValue;
            string text = ddlNewAltMntrByDept.SelectedItem.Text;
            if (ddlAltMnStaff.Items.FindByValue(value) == null)
            {
                ddlAltMnStaff.Items.Add(new ListItem(ddlNewAltMntrByDept.SelectedItem.Text, ddlNewAltMntrByDept.SelectedValue));
                msg = "Done!";
            }
            else
            {
                msg = "Already Exists!";
            }

            AlertMsg.MsgBox(Page, msg);

            ddlAltMnStaff.SelectedValue = ddlNewAltMntrByDept.SelectedValue;
            dvAddAltMntr.Visible = false;

        }

        void dsSaveGVTERMS(ref DataTable dsGVTERMS)//GVTERMS
        {
            foreach (GridViewRow row in GVTERMS.Rows)
            {
               
                TextBox txtremarks = new TextBox();
                txtremarks = (TextBox)row.FindControl("TXTTERMS");
                Label lblid = new Label();
                lblid = (Label)row.FindControl("lblid");
                DataRow[] drs = dsGVTERMS.Select("TermId='" + lblid.Text.Trim() + "'");
                if (drs.Length > 0)
                {
                    drs[0]["Term"] = txtremarks.Text;
                }
               
            }
            dsGVTERMS.AcceptChanges();
        }

        protected void btnIncludeTerm_Click(object sender, EventArgs e)
        {
            DataTable dtChkedTerms = (DataTable)ViewState["dtChkedTerms"];
            DataTable dtTerms = (DataTable)ViewState["dtTerms"];

            DataTable dtExist = new DataTable();
            dtExist.Columns.Add("TermId", typeof(Int32));
            int SLNO = 1;
            dsSaveGVTERMS(ref dtChkedTerms);
            SLNO = dtChkedTerms.Rows.Count + 1;
            foreach (int index in lstTerms.GetSelectedIndices())
            {
                ListItem li = lstTerms.Items[index];
                DataRow[] drArray = dtTerms.Select("TermId=" + li.Value);//lstTerms.SelectedValue);//["TERMID"].ToString());
                if (drArray != null && drArray.Length > 0)
                {
                    DataRow drChk = dtChkedTerms.NewRow();
                    drChk["TermId"] = drArray[0].ItemArray[0].ToString();
                    drChk["Term"] = drArray[0].ItemArray[1].ToString();
                    drChk["SLNO"] = SLNO;
                    dtChkedTerms.Rows.Add(drChk);
                    dtChkedTerms.AcceptChanges();
                    DataRow drExist = dtExist.NewRow();
                    drExist["TermId"] = drArray[0].ItemArray[0].ToString();
                    dtExist.Rows.Add(drExist);
                    dtExist.AcceptChanges();
                    SLNO++;
                }

                foreach (DataRow dr in dtTerms.Rows)
                {
                    foreach (DataRow drE in dtExist.Rows)
                    {
                        if (drE["TermId"].ToString() == dr["TermId"].ToString())
                        {
                            dr.Delete();
                            break;
                        }
                    }

                }
                dtTerms.AcceptChanges();
            }
            ViewState["dtTerms"] = dtTerms;
            ViewState["dtChkedTerms"] = dtChkedTerms;

           
            lstTerms.Items.Clear();
            foreach (DataRow dRow in dtTerms.Rows)
            {
                ListItem li1 = new ListItem(dRow["ShortTerm"].ToString(), dRow["TermId"].ToString());
                li1.Attributes.Add("title", dRow["Term"].ToString());
                lstTerms.Items.Add(li1);
            }
            GVTERMS.DataSource = dtChkedTerms;
            GVTERMS.DataBind();
            foreach (GridViewRow row in GVTERMS.Rows)
            {
                CheckBox chk = (CheckBox)row.FindControl("chk");
                chk.Checked = true;
                chk.Enabled = false;
            }
            Accordion1.SelectedIndex = 0;
            string strScript = "<script language='javascript' type='text/javascript'>   CalculatePOValue(); </script>";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "PopupCP", strScript, false);
        }

        protected void btnVendorSearch_Click(object sender, EventArgs e)
        {
            BindVendors(txtvendorSearch.Text, false);
          

        }

        #endregion BtnOnclick

        #region lnkclick

        protected void lnkAddRcvrs_Click(object sender, EventArgs e)
        {
            dvAddRcvrs.Visible = true;
            if (ddlStrRcvrDept.SelectedIndex > 0)
            {
                FIllObject.FillEmptyDropDown(ref ddlNewRcvrsByDept);
            }
            //ddlNewRcvrsByDept.SelectedIndex = 0;
        }

        protected void lnkAddAltRcvr_Click(object sender, EventArgs e)
        {
            dvAddAltRcvrs.Visible = true;
            if (ddlAltRcvrDept.SelectedIndex > 0)
            {
                FIllObject.FillEmptyDropDown(ref ddlNewAltRcvrByDept);
            }

        }

        protected void lnkAddMntrs_Click(object sender, EventArgs e)
        {
            dvAddMntrs.Visible = true;
            if (ddlMntrsDept.SelectedIndex > 0)
            {
                FIllObject.FillEmptyDropDown(ref ddlAddNewMntrByDept);
            }

        }

        protected void lnkAddAltMntr_Click(object sender, EventArgs e)
        {
            dvAddAltMntr.Visible = true;
            if (ddlAltMntrsDept.SelectedIndex > 0)
            {
                FIllObject.FillEmptyDropDown(ref ddlNewAltMntrByDept);
            }

        }

        protected void lnkRefreshVendor_Click(object sender, EventArgs e)
        {
            BindVendors("", false);
        }

        protected void lnkRefreshTaxs_Click(object sender, EventArgs e)
        {
            FIllObject.FillDropDown(ref ddltax, "GetRateTax");
        }

        protected void lnkRefreshLumpsums_Click(object sender, EventArgs e)
        {
            FIllObject.FillDropDown(ref ddllumptax, "GetLumpsumTax");

        }

        #endregion lnkclick

        #region Rowcommand
        protected void gvtax_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            ddltax.SelectedIndex = 0;
            txttax.Text = "";

            if (e.CommandName == "lnkDel")
            {
                SqlParameter[] parms = new SqlParameter[4];
                parms[0] = new SqlParameter("@id", 5);
                parms[1] = new SqlParameter("@taxid", e.CommandArgument);
                parms[2] = new SqlParameter("@value", 0);
                parms[3] = new SqlParameter("@equid", 1); // TvIndent.SelectedNode.Parent.Value);
                SqlHelper.ExecuteNonQuery("Temp_Tax", parms);

                FIllObject.FillEmptygridview(ref gvtax);

                parms = new SqlParameter[2];
                parms[0] = new SqlParameter("@id", 3);
                parms[1] = new SqlParameter("@equid", 1); // TvIndent.SelectedNode.Parent.Value);
                FIllObject.FillGridview(ref gvtax, "Temp_Tax", parms);
                string strScript = "<script language='javascript' type='text/javascript'>   CalculatePOValue(); </script>";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "PopupCP", strScript, false);
            }
        }

        protected void GVlumpsum_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            ddllumptax.SelectedIndex = 0;
            txtlumpsum.Text = "";

            if (e.CommandName == "lnkDel")
            {
                SqlParameter[] parms = new SqlParameter[4];
                parms[0] = new SqlParameter("@id", 6);
                parms[1] = new SqlParameter("@taxid", e.CommandArgument);
                parms[2] = new SqlParameter("@value", 0);
                parms[3] = new SqlParameter("@equid", 1); // TvIndent.SelectedNode.Parent.Value);
                SqlHelper.ExecuteNonQuery("Temp_Tax", parms);

                FIllObject.FillEmptygridview(ref GVlumpsum);

                parms = new SqlParameter[2];
                parms[0] = new SqlParameter("@id", 4);
                parms[1] = new SqlParameter("@equid", 1); // TvIndent.SelectedNode.Parent.Value);
                FIllObject.FillGridview(ref GVlumpsum, "Temp_Tax", parms);
                string strScript = "<script language='javascript' type='text/javascript'>   CalculatePOValue(); </script>";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "PopupCP", strScript, false);
            }
        }

        protected void GVTERMS_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Del")
            {
                DataTable dtChkedTerms = (DataTable)ViewState["dtChkedTerms"];
                DataTable dtTerms = (DataTable)ViewState["dtTerms"];
                DataTable Terms = (DataTable)ViewState["Terms"];
                dsSaveGVTERMS(ref dtChkedTerms);
                DataRow[] drArray = Terms.Select("TermId=" + e.CommandArgument.ToString());

                if (drArray != null && drArray.Length > 0)
                {
                    string ShortTerm = drArray[0].ItemArray[1].ToString();
                    string Term = drArray[0].ItemArray[1].ToString();
                    string TermId = drArray[0].ItemArray[0].ToString();
                    if (ShortTerm.Length > 151)
                    {
                        string AddTerm = "";
                        int len = 0;
                        foreach (char c in ShortTerm)
                        {
                            if (len < 151)
                            {
                                AddTerm = AddTerm + c;
                            }
                            else if (len > 150 && len < 161)
                            {
                                AddTerm = AddTerm + ".";
                            }
                            if (len > 161)
                            {
                                break;
                            }

                            len = len + 1;
                        }
                        ShortTerm = AddTerm;
                    }
                    DataRow drNewTerm = dtTerms.NewRow();
                    drNewTerm["TermId"] = TermId;
                    drNewTerm["Term"] = Term;
                    drNewTerm["ShortTerm"] = ShortTerm;
                    dtTerms.Rows.Add(drNewTerm);
                    dtTerms.AcceptChanges();
                    foreach (DataRow dr in dtChkedTerms.Rows)
                    {
                        if (drArray[0].ItemArray[0].ToString() == dr["TermId"].ToString())
                        {
                            dr.Delete();
                            break;
                        }

                    }
                    dtChkedTerms.AcceptChanges();
                    int SLNO = 1;
                    foreach (DataRow dr in dtChkedTerms.Rows)
                    {
                        dr["SLNO"] = SLNO;
                        SLNO++;
                    }
                }
                ViewState["dtChkedTerms"] = dtChkedTerms;
                ViewState["dtTerms"] = dtTerms;
                lstTerms.Items.Clear();
                foreach (DataRow dRow in dtTerms.Rows)
                {
                    ListItem li1 = new ListItem(dRow["ShortTerm"].ToString(), dRow["TermId"].ToString());
                    li1.Attributes.Add("title", dRow["Term"].ToString());
                    lstTerms.Items.Add(li1);
                }
                GVTERMS.DataSource = dtChkedTerms;
                GVTERMS.DataBind();
                foreach (GridViewRow row in GVTERMS.Rows)
                {
                    CheckBox chk = (CheckBox)row.FindControl("chk");
                    chk.Checked = true;
                    chk.Enabled = false;

                }
                AlertMsg.MsgBox(Page, "Done");
            }
            string strScript = "<script language='javascript' type='text/javascript'>   CalculatePOValue(); </script>";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "PopupCP", strScript, false);
        }

        #endregion RowCommand

        #region RowdataBound
        protected void GVTERMS_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                ((CheckBox)e.Row.FindControl("cbSelectAll")).Attributes.Add("onclick", "javascript:SelectAll('" + ((CheckBox)e.Row.FindControl("cbSelectAll")).ClientID + "')");
            }
        }
        protected void dlIndents_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                TextBox txtQty = (TextBox)e.Row.FindControl("txtQty");
                TextBox txtRate = (TextBox)e.Row.FindControl("txtrate");
                Label lblBudget = (Label)e.Row.FindControl("lbl");
                txtQty.Attributes.Add("OnChange", "javascript:return Multiply(this,'" + txtQty.ClientID + "','" + txtRate.ClientID + "','" + lblBudget.ClientID + "');");
                txtRate.Attributes.Add("OnChange", "javascript:return Multiply(this,'" + txtQty.ClientID + "','" + txtRate.ClientID + "','" + lblBudget.ClientID + "');");

            }
        }

        #endregion RowDataBound

        #region IndexChanged
        protected void lbxGGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlParameter[] p = new SqlParameter[4];
            p[0] = new SqlParameter("@id", 1);
            p[1] = new SqlParameter("@Desc", txtFilter.Text);
            p[2] = new SqlParameter("@GroupId", lbxGGroup.SelectedValue);
            p[3] = new SqlParameter("@Cat_Type", Type);
            if (lbxGGroup.SelectedValue != "0" && lbxGGroup.SelectedValue != "")
            {
                FIllObject.FillListBox(ref lbxItems, "PM_GroupWiseItems_Indent", p);
                BindGroupstoListBoxItems();
            }
            if (lbxItems.Items.Count == 0)
            {
                lbxItems.Items.Add(new ListItem("No Records", "0"));

            }

            txtFilter.Text = "";


        }

        protected void listParentAcGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            lbxGGroup.Items.Clear();
            lbxItems.Items.Clear();
            BindGroupListbox();
            lbxGGroup_SelectedIndexChanged(this, e);

        }

        protected void ddlStrRcvrDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            dvAddRcvrs.Visible = true;
            if (ddlStrRcvrDept.SelectedIndex == 0)
            {
                lblNewRcvr.Visible = false; ddlNewRcvrsByDept.Visible = false; btnAddNewRcvr.Visible = false;
            }
            else
            {
                lblNewRcvr.Visible = true; ddlNewRcvrsByDept.Visible = true; btnAddNewRcvr.Visible = true;
                int DeptId = Convert.ToInt32(ddlStrRcvrDept.SelectedValue);
                DataSet ds = GetEmployesByDept(Worksite, DeptId);
                if (ds != null)
                    if (ds.Tables.Count > 0)
                        if (ds.Tables[0].Rows.Count > 0)
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                ddlNewRcvrsByDept.DataSource = ds;
                                ddlNewRcvrsByDept.DataTextField = ds.Tables[0].Columns[1].ToString();
                                ddlNewRcvrsByDept.DataValueField = ds.Tables[0].Columns[0].ToString();
                                ddlNewRcvrsByDept.DataBind();
                                ddlNewRcvrsByDept.Items.Insert(0, new ListItem("--Select--", "0"));
                            }
            }
        }


        protected void ddlForProject_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlWorksite.SelectedValue != "" && ddlWorksite.SelectedValue != "0")
            {
                BindDetails();
            }
        }

        private void BindPrj()
        {
            int CompanyID = Convert.ToInt32(Session["CompanyID"].ToString());
            int ProjId = Convert.ToInt32(ddlProjects.SelectedValue);
            BindReceivers(ProjId);
            SqlParameter[] p = new SqlParameter[2];
            p[0] = new SqlParameter("@Worksite", ddlWorksite.SelectedValue);
            if (CompanyID != null)
                p[1] = new SqlParameter("@CompanyID", CompanyID);
            else
                p[1] = new SqlParameter("@CompanyID", SqlDbType.Int);
            FIllObject.FillDropDown(ref ddlProjects, "OMS_GetProjectByWorksite", p);
            if (ddlProjects.Items.Count > 1)
            {
                ddlProjects.SelectedIndex = 1;
            }
            FillRcvrDropDowns();
            FillMntrDropDowns();
            BindStrRcvrsDepts();
            BindMntrsDepts();
        }

        private void BindDetails()
        {
            int CompanyID = Convert.ToInt32(Session["CompanyID"].ToString());
            int ProjId = Convert.ToInt32(ddlProjects.SelectedValue);
            BindReceivers(ProjId);
            SqlParameter[] p = new SqlParameter[2];
            p[0] = new SqlParameter("@Worksite", ddlWorksite.SelectedValue);
            if (CompanyID != null)
                p[1] = new SqlParameter("@CompanyID", CompanyID);
            else
                p[1] = new SqlParameter("@CompanyID", SqlDbType.Int);
            FIllObject.FillDropDown(ref ddlProjects, "OMS_GetProjectByWorksite", p);
            if (ddlProjects.Items.Count > 1)
            {
                ddlProjects.SelectedIndex = 1;
            }

        }

        protected void ddlAltRcvrDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            dvAddAltRcvrs.Visible = true;
            if (ddlAltRcvrDept.SelectedIndex == 0)
            {
                lblNewAltRcvr.Visible = false; ddlNewAltRcvrByDept.Visible = false; btnAddNewAltRcvr.Visible = false;
            }
            else
            {
                lblNewAltRcvr.Visible = true; ddlNewAltRcvrByDept.Visible = true; btnAddNewAltRcvr.Visible = true;
                int DeptId = Convert.ToInt32(ddlAltRcvrDept.SelectedValue);
                DataSet ds = GetEmployesByDept(Worksite, DeptId);
                if (ds != null)
                    if (ds.Tables.Count > 0)
                        if (ds.Tables[0].Rows.Count > 0)
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                ddlNewAltRcvrByDept.DataSource = ds;
                                ddlNewAltRcvrByDept.DataTextField = ds.Tables[0].Columns[1].ToString();
                                ddlNewAltRcvrByDept.DataValueField = ds.Tables[0].Columns[0].ToString();
                                ddlNewAltRcvrByDept.DataBind();
                                ddlNewAltRcvrByDept.Items.Insert(0, new ListItem("--Select--", "0"));

                            }
            }

        }

        protected void ddlMntrsDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            dvAddMntrs.Visible = true;
            if (ddlMntrsDept.SelectedIndex == 0)
            {
                lblAddMntr.Visible = false; ddlAddNewMntrByDept.Visible = false; btnAddMntr.Visible = false;
            }
            else
            {
                lblAddMntr.Visible = true; ddlAddNewMntrByDept.Visible = true; btnAddMntr.Visible = true;
                int DeptId = Convert.ToInt32(ddlMntrsDept.SelectedValue);
                DataSet ds = GetMonitorsByDept(Worksite, DeptId, MntrWS);
                if (ds != null)
                    if (ds.Tables.Count > 0)
                        if (ds.Tables[0].Rows.Count > 0)
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                ddlAddNewMntrByDept.DataSource = ds;
                                ddlAddNewMntrByDept.DataTextField = ds.Tables[0].Columns[1].ToString();
                                ddlAddNewMntrByDept.DataValueField = ds.Tables[0].Columns[0].ToString();
                                ddlAddNewMntrByDept.DataBind();
                                ddlAddNewMntrByDept.Items.Insert(0, new ListItem("--Select--", "0"));

                            }
            }
        }

        protected void ddlAltMntrsDept_SelectedIndexChanged(object sender, EventArgs e)
        {

            dvAddAltMntr.Visible = true;
            if (ddlAltMntrsDept.SelectedIndex == 0)
            {
                lblNewAltMntr.Visible = false; ddlNewAltMntrByDept.Visible = false; btnAddAltMntr.Visible = false;
            }
            else
            {
                lblNewAltMntr.Visible = true; ddlNewAltMntrByDept.Visible = true; btnAddAltMntr.Visible = true;
                int DeptId = Convert.ToInt32(ddlAltMntrsDept.SelectedValue);
                DataSet ds = GetMonitorsByDept(Worksite, DeptId, MntrWS);
                if (ds != null)
                    if (ds.Tables.Count > 0)
                        if (ds.Tables[0].Rows.Count > 0)
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                ddlNewAltMntrByDept.DataSource = ds;
                                ddlNewAltMntrByDept.DataTextField = ds.Tables[0].Columns[1].ToString();
                                ddlNewAltMntrByDept.DataValueField = ds.Tables[0].Columns[0].ToString();
                                ddlNewAltMntrByDept.DataBind();
                                ddlNewAltMntrByDept.Items.Insert(0, new ListItem("--Select--", "0"));


                            }
            }
        }

        protected void ddlTermsList_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (GridViewRow row in GVTERMS.Rows)
            {
                CheckBox chk = new CheckBox();
                chk = (CheckBox)row.FindControl("chk");
                chk.Checked = false;
            }

            BindTerms();
             
            SqlParameter[] p = new SqlParameter[2];
            p[0] = new SqlParameter("@ID", 3);
            p[1] = new SqlParameter("@SETID", ddlTermsList.SelectedValue);
          DataSet  ds = SqlHelper.ExecuteDataset("PM_TERMSET", p);
            if (ds.Tables[0].Rows.Count > 0)
            {

                foreach (DataRow Drow in ds.Tables[0].Rows)
                {
                   
                    DataTable dtChkedTerms = (DataTable)ViewState["dtChkedTerms"];
                    DataTable dtTerms = (DataTable)ViewState["dtTerms"];
                    DataTable Terms = (DataTable)ViewState["Terms"];

                    DataTable dtExist = new DataTable();
                    dtExist.Columns.Add("TermId", typeof(Int32));

                    DataRow[] drArray = dtTerms.Select("TermId=" + Drow["TERMID"].ToString());
                    if (drArray != null && drArray.Length > 0)
                    {
                        DataRow drChk = dtChkedTerms.NewRow();
                        drChk["TermId"] = drArray[0].ItemArray[0].ToString();
                        drChk["Term"] = drArray[0].ItemArray[1].ToString();
                        dtChkedTerms.Rows.Add(drChk);
                        dtChkedTerms.AcceptChanges();
                        DataRow drExist = dtExist.NewRow();
                        drExist["TermId"] = drArray[0].ItemArray[0].ToString();
                        dtExist.Rows.Add(drExist);
                        dtExist.AcceptChanges();
                    }

                    foreach (DataRow dr in dtTerms.Rows)
                    {
                        foreach (DataRow drE in dtExist.Rows)
                        {
                            if (drE["TermId"].ToString() == dr["TermId"].ToString())
                            {
                                dr.Delete();
                                break;
                            }
                        }

                    }
                    dtTerms.AcceptChanges();

                    ViewState["dtTerms"] = dtTerms;
                    ViewState["dtChkedTerms"] = dtChkedTerms;
                    lstTerms.Items.Clear();
                    // lstTerms.Items.Insert(0, new ListItem("------------Select Term-----------", "0"));
                    foreach (DataRow dRow in dtTerms.Rows)
                    {
                        ListItem li1 = new ListItem(dRow["ShortTerm"].ToString(), dRow["TermId"].ToString());
                        li1.Attributes.Add("title", dRow["Term"].ToString());
                        lstTerms.Items.Add(li1);
                    }
                    GVTERMS.DataSource = dtChkedTerms;
                    GVTERMS.DataBind();
                    foreach (GridViewRow row in GVTERMS.Rows)
                    {
                        CheckBox chk = (CheckBox)row.FindControl("chk");
                        chk.Checked = true;
                        chk.Enabled = false;
                    }
                }
            }
            else
            {
                GVTERMS.DataSource = null;
                GVTERMS.DataBind();
            }

            string strScript = "<script language='javascript' type='text/javascript'>   CalculatePOValue(); </script>";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "PopupCP", strScript, false);
        }

        protected void lbxItems_SelectedIndexChanged(object sender, EventArgs e)
        {
            GroupAndAccParentGroupReverseBind();
        }

        #endregion IndexChanged


        protected void dlIndents_ItemCommand(object source, DataListCommandEventArgs e)
        {
            DataTable dtGoodsList = new DataTable();
            dtGoodsList = (DataTable)ViewState["Goods"];
            if (e.CommandName == "Del")
            {
                dtGoodsList.Rows.RemoveAt(e.Item.ItemIndex);
            }
            ViewState["Goods"] = dtGoodsList;
            dlIndents.DataSource = dtGoodsList;
            dlIndents.DataBind();

            int i = 0;
            foreach (GridViewRow row in dlIndents.Rows)
            {
                i = i + 1;
                row.Cells[0].Text = i.ToString();
            }
            if (dlIndents.Rows.Count > 0)
                dlIndents.Visible = true;
            else
                dlIndents.Visible = false;

        }


        protected void dlIndents_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            DataTable dtGoodsList = (DataTable)ViewState["Goods"];
            DataRow[] drSelected = null;
            foreach (GridViewRow row in dlIndents.Rows)
            {
                TextBox txtspec = new TextBox(); txtspec = (TextBox)row.FindControl("txtSpec");
                TextBox txtpur = new TextBox(); txtpur = (TextBox)row.FindControl("txtPurpose");
                TextBox txtqty = new TextBox(); txtqty = (TextBox)row.FindControl("txtQty");
                TextBox txtdate = new TextBox(); txtdate = (TextBox)row.FindControl("txtReq");
                TextBox txtrate = new TextBox(); txtrate = (TextBox)row.FindControl("txtrate");
                Label lblItemId = new Label(); lblItemId = (Label)row.FindControl("itemid");

                drSelected = dtGoodsList.Select("GoodsID='" + lblItemId.Text + "'");
                drSelected[0]["Specification"] = txtspec.Text;
                drSelected[0]["Purpose"] = txtpur.Text;
                drSelected[0]["Requiredon"] = CodeUtil.ConverttoDate(txtdate.Text, CodeUtil.DateFormat.DayMonthYear);
                drSelected[0]["Quantity"] = txtqty.Text;
                drSelected[0]["BasicRate"] = txtrate.Text;

               
            }

            LinkButton lnk = new LinkButton();
            lnk = (LinkButton)dlIndents.Rows[e.RowIndex].FindControl("lnk");

            drSelected = dtGoodsList.Select("GoodsID='" + lnk.CommandArgument + "'");

            if (drSelected.Length > 0)
                dtGoodsList.Rows.Remove(drSelected[0]);

            dtGoodsList.AcceptChanges();

            dtGoodsList.AcceptChanges();
            ViewState["Goods"] = dtGoodsList;
            if (dtGoodsList.Rows.Count != 0)
                dlIndents.DataSource = dtGoodsList;
            dlIndents.DataBind();
            if (dlIndents.Rows.Count > 0)
                dlIndents.Visible = true;
            else
                dlIndents.Visible = false;
            //BindVendors();
            WSEnable();
        }

       



      

        protected void chkTDS_CheckedChanged(object sender, EventArgs e)
        {
            if (chkTDS.Checked)
            {
                dvTDS.Visible = true;
                int VendorId = Convert.ToInt32(ddlVendor.SelectedValue);
                SqlParameter[] p = new SqlParameter[1];
                p[0] = new SqlParameter("@VendorId", VendorId);
                 
             DataSet   ds = SqlHelper.ExecuteDataset("PMS_GetVendorPANCardNo", p);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {

                    if (ds.Tables[0].Rows[0]["PANCardNo"].ToString() != "" && ds.Tables[0].Rows[0]["PANCardNo"] != null)
                    {
                        lblMsg.Visible = false;
                        //txtTDS.Enabled = false;
                    }
                    else
                    {
                        lblMsg.Visible = true;
                        lblMsg.Text = "Vendor does't have the PAN Card No. Please Update the Vendor details."; //  txtTDS.Enabled = true;

                    }

                }



            }
            else
            {
                txtTDS.Text = ""; dvTDS.Visible = false;
            }
            string strScript = "<script language='javascript'> CalculatePOValue();</script>";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "CalculatePO", strScript, false);
        }

        protected void TXTTERMS_TextChanged(object sender, EventArgs e)
        {
            try
            {
                TextBox txtTerms = (TextBox)sender;
                GridViewRow gvr = (GridViewRow)(((Control)sender).NamingContainer);
                int TermId = Convert.ToInt32(GVTERMS.DataKeys[gvr.RowIndex].Values[0]);
                DataTable dtChkedTerms = (DataTable)ViewState["dtChkedTerms"];
                DataTable dtTerms = (DataTable)ViewState["dtTerms"];

                foreach (DataRow dr in dtChkedTerms.Rows)
                {
                    if (Convert.ToInt32(dr["TermId"].ToString()) == TermId)
                    {
                        dr["Term"] = txtTerms.Text;
                        break;
                    }
                }
                dtChkedTerms.AcceptChanges();

                ViewState["dtChkedTerms"] = dtChkedTerms;
                ViewState["dtTerms"] = dtTerms;
                GVTERMS.DataSource = dtChkedTerms;
                GVTERMS.DataBind();
                foreach (GridViewRow gvRow in GVTERMS.Rows)
                {
                    CheckBox chk = (CheckBox)gvRow.FindControl("chk");
                    chk.Checked = true;
                    chk.Enabled = false;
                }
                string strScript = "<script language='javascript' type='text/javascript'>   CalculatePOValue(); </script>";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "PopupCP", strScript, false);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

       

       
        protected void btnSearcWorksite_Click(object sender, EventArgs e)
        {
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@Search", txtSearchWorksite.Text);
            param[1] = new SqlParameter("@CompanyID", Convert.ToInt32(Session["CompanyID"]));
            FIllObject.FillDropDown(ref ddlWorksite, "G_GET_WorkSitebyFilter", param);
            FIllObject.FillEmptyDropDown(ref ddlProjects);
        }
        protected void chkEmp_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                int ProjId = Convert.ToInt32(ddlWorksite.SelectedValue);
                BindReceivers(ProjId);
                BindMonitors();
            }
            catch { }
            string strScript = "<script language='javascript'> CalculatePOValue();</script>";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "CalculatePO", strScript, false);
        }
        void UpdateAditionalTerms()
        {
            try
            {
                foreach (GridViewRow row in GVAdditionalTerms.Rows)
                {
                    TextBox txtterm = new TextBox();
                    txtterm = (TextBox)row.FindControl("TXTTERMS");
                    Label lblterID = new Label();
                    lblterID = (Label)row.FindControl("lblid");
                    if (txtterm.Text.Trim() != "")
                    {
                        SqlParameter[] parms1 = new SqlParameter[5];
                        parms1[0] = new SqlParameter("@ID", 6);
                        parms1[1] = new SqlParameter("@Remarks", txtterm.Text);
                        parms1[2] = new SqlParameter("@VenId", 1);
                        parms1[3] = new SqlParameter("@EnqId", 1);
                        parms1[4] = new SqlParameter("@Termid", Convert.ToInt32(lblterID.Text));
                        SqlHelper.ExecuteNonQuery("PM_INSERTREMARKS", parms1);
                    }
                }
            }
            catch { }
        }

        protected void GVAdditionalTerms_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {

                UpdateAditionalTerms();
                GridViewRow gvr = (GridViewRow)((LinkButton)e.CommandSource).Parent.Parent;
                TextBox txtTerm = (TextBox)gvr.FindControl("TXTTERMS");
                Label lblterID = new Label();
                lblterID = (Label)gvr.FindControl("lblid");
                SqlParameter[] parms = new SqlParameter[5];
                parms[0] = new SqlParameter("@ID", 5);
                parms[1] = new SqlParameter("@Remarks", txtTerm.Text);
                parms[2] = new SqlParameter("@VenId", 1);
                parms[3] = new SqlParameter("@EnqId", 1);
                parms[4] = new SqlParameter("@Termid", Convert.ToInt32(lblterID.Text));
                SqlHelper.ExecuteNonQuery("PM_INSERTREMARKS", parms);

                parms = new SqlParameter[4];
                parms[0] = new SqlParameter("@ID", 2);
                parms[1] = new SqlParameter("@Remarks", TxtTerms.Text);
                parms[2] = new SqlParameter("@VenId", 1);
                parms[3] = new SqlParameter("@EnqId", 1);
                FIllObject.FillGridview(ref GVAdditionalTerms, "PM_INSERTREMARKS", parms);
            }
            catch { }
        }
      

        public static Hashtable htUOM = new Hashtable();
        public static DataSet dsUOM = new DataSet();
        public DataSet BindResUnits(string ResID)
        {
            if (!htUOM.Contains(ResID))
            {
                dsUOM = objProcdept.GetUnitsByResource(Convert.ToInt32(ResID));
                htUOM.Add(ResID, dsUOM.Copy());
            }

            dsUOM = (DataSet)htUOM[ResID];
            return dsUOM;

        }

        public int GetIndexUnits(string ResID, string Value)
        {
            int Val;
            dsUOM = (DataSet)htUOM[ResID];
            if (dsUOM.Tables[0].Select("ID=" + Value).Length > 0)
            {
                DataRow dr = dsUOM.Tables[0].Select("ID=" + Value)[0];

                Val = dsUOM.Tables[0].Rows.IndexOf(dr);
            }
            else
            {
                Val = 0;
            }
            return Val;

        }


        void Call_CalculateValue()
        {
            try
            {
                //decimal ofrval = 0; decimal Val = 0;
                if (ViewState["Goods"] != null)
                {

                    string s = lblrate.Text;
                    DataTable dtGoodsList = (DataTable)ViewState["Goods"];
                    DataRow[] drSelected = null;
                    foreach (GridViewRow row in dlIndents.Rows)
                    {
                        int index = row.RowIndex;
                        TextBox txtQty = new TextBox(); txtQty = (TextBox)row.FindControl("txtQty");
                        TextBox txtRate = new TextBox(); txtRate = (TextBox)row.FindControl("txtrate");
                        TextBox txtSpec = new TextBox(); txtSpec = (TextBox)row.FindControl("txtSpec");
                        Label lblBudget = new Label(); lblBudget = (Label)row.FindControl("lbl");
                        DropDownList ddlUnits = new DropDownList(); ddlUnits = (DropDownList)row.FindControl("ddlunits");
                        TextBox txtdate = new TextBox(); txtdate = (TextBox)row.FindControl("txtReq");
                        TextBox txtpur = new TextBox(); txtpur = (TextBox)row.FindControl("txtPurpose");

                     
                        al.Add(ddlUnits.SelectedValue);
                        ViewState["al"] = al;
                        dtGoodsList.Rows[index]["Specification"] = txtSpec.Text;
                        dtGoodsList.Rows[index]["Quantity"] = txtQty.Text;
                        dtGoodsList.Rows[index]["BasicRate"] = txtRate.Text;
                        dtGoodsList.Rows[index]["Budget"] = (Convert.ToDecimal(dtGoodsList.Rows[index]["Quantity"]) * Convert.ToDecimal(dtGoodsList.Rows[index]["BasicRate"])).ToString("0.00");
                       

                        try
                        {

                            DropDownList ddlunits = new DropDownList(); ddlunits = (DropDownList)row.FindControl("ddlunits");
                            dtGoodsList.Rows[index]["Uom"] = ddlunits.SelectedValue;
                            DropDownList ddlMunits = new DropDownList(); ddlMunits = (DropDownList)row.FindControl("ddlMunits");
                            dtGoodsList.Rows[index]["Mileage"] = ddlMunits.SelectedValue;
                            dtGoodsList.Rows[index]["Requiredon"] = CodeUtil.ConverttoDate(txtdate.Text, CodeUtil.DateFormat.DayMonthYear);
                            dtGoodsList.Rows[index]["Purpose"] = txtpur.Text;

                        }
                        catch { }
                    }
                    lblrate.Text = hfOfrVal.Value.ToString();
                    dtGoodsList.AcceptChanges();
                    ViewState["Goods"] = dtGoodsList;
                    dlIndents.DataSource = dtGoodsList;
                    dlIndents.DataBind();

                }
            }
            catch { AlertMsg.MsgBox(Page, ""); }
        }

    }
}