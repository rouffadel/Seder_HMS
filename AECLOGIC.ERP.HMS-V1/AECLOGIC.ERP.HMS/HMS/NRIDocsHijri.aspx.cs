using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using AECLOGIC.HMS.BLL;
using System.Collections;
using DataAccessLayer;
using AECLOGIC.ERP.COMMON;
using Aeclogic.Common.DAL;
using System.Globalization;
using AECLOGIC.ERP.HMS.HMS;
using System.Text.RegularExpressions;

namespace AECLOGIC.ERP.HMS
{
    public partial class NRIDocsHijri : AECLOGIC.ERP.COMMON.WebFormMaster
    {
        HRCommon objCommon = new HRCommon();
        AttendanceDAC objAtt = new AttendanceDAC();
        SRNService objSRN = new SRNService();
        DataSet ds = new DataSet();
        int mid = 0;
        bool viewall;
        string menuname;
        string menuid;
        protected override void OnInit(EventArgs e)
        {
            ModuleID = 1;

            base.OnInit(e);
            // btnSave.Attributes.Add("onclick", "javascript:return ValidateSave('" + txtCategoryName.ClientID + "');");
            PageTax.FirstClick += new Paging.PageFirst(EmpListPaging_FirstClick);
            PageTax.PreviousClick += new Paging.PagePrevious(EmpListPaging_FirstClick);
            PageTax.NextClick += new Paging.PageNext(EmpListPaging_FirstClick);
            PageTax.LastClick += new Paging.PageLast(EmpListPaging_FirstClick);
            PageTax.ChangeClick += new Paging.PageChange(EmpListPaging_FirstClick);
            PageTax.ShowRowsClick += new Paging.ShowRowsChange(EmpListPaging_ShowRowsClick);
            PageTax.CurrentPage = 1;
        }
     
        void EmpListPaging_ShowRowsClick(object sender, EventArgs e)
        {
            PageTax.CurrentPage = 1;
            BindPager();
        }
        void EmpListPaging_FirstClick(object sender, EventArgs e)
        {
            BindPager();
        }
        void BindPager()
        {

            objCommon.PageSize = PageTax.ShowRows;
            objCommon.CurrentPage = PageTax.CurrentPage;
            BindView();
        }
        void BindView()
        {
            try
            {
                objCommon.PageSize = PageTax.ShowRows;
                objCommon.CurrentPage = PageTax.CurrentPage;

                int? SiteID = null;
                int? DeptID = null;
                int? DesigID = null;
                int? EmpID = null;

                if (ddlRecWs.SelectedItem.Value != "0")
                {
                    SiteID = Convert.ToInt32(ddlRecWs.SelectedItem.Value);
                }
                if (ddlRecDept.SelectedItem.Value != "0")
                {
                    DeptID = Convert.ToInt32(ddlRecDept.SelectedItem.Value);
                }
                if (ddlRecDesg.SelectedItem.Value != "0")
                {
                    DesigID = Convert.ToInt32(ddlRecDesg.SelectedItem.Value);
                }
                if (ddlSearcMech.SelectedItem.Value != "0")
                {
                    EmpID = Convert.ToInt32(ddlSearcMech.SelectedItem.Value);
                }

                gvRecosiled.DataSource = null;
                gvRecosiled.DataBind();
                PageTax.Visible = false;
                ds = AttendanceDAC.HR_GetReconsiledSRNItems(objCommon, Convert.ToInt32(ddlRecItems.SelectedValue), SiteID, DeptID, DesigID, EmpID,"",0);
                // modify by pratap to change the Gregorian date to Hijri Date date: 12-02-2016
                DataTable dtHijri = new DataTable();
                dtHijri = ds.Tables[0];
                System.Globalization.CultureInfo arabicCulture = new CultureInfo("ar-SA");
                CultureInfo englishCulture = new CultureInfo("en-US");
                foreach (DataRow row in dtHijri.Rows)
                {
                    DateTime tempDate = ParseDateTime(Convert.ToString(row["From"]));
                    row["From"] = tempDate.ToString("dd/MM/yyyy", arabicCulture.DateTimeFormat);
                    DateTime tempDate1 = ParseDateTime(Convert.ToString(row["To"]));
                    row["To"] = tempDate1.ToString("dd/MM/yyyy", arabicCulture.DateTimeFormat);
                }
                dtHijri.AcceptChanges();
                if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0)
                {
                    //gvRecosiled.DataSource = ds;
                    gvRecosiled.DataSource = dtHijri;
                    gvRecosiled.DataBind();
                    PageTax.Visible = true;
                    PageTax.Bind(objCommon.CurrentPage, objCommon.TotalPages, objCommon.NoofRecords, objCommon.PageSize);
                }

            }
            catch (Exception e)
            {
                throw e;
            }
        }
        // added by pratap date: 13-02-2016 to filter the employees
        protected void ddlRecItems_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlWorksite.SelectedItem.Value != "0")
            {
                WSID = Convert.ToInt32(ddlWorksite.SelectedItem.Value);
            }
            if (ddlDepartment.SelectedItem.Value != "0")
            {
                DeptID = Convert.ToInt32(ddlDepartment.SelectedItem.Value);

            }
            //if (ddlDesif2.SelectedItem.Value != "0")
            //{
            //    DesigID = Convert.ToInt32(ddlDesif2.SelectedItem.Value);

            //}
            //RecType = Convert.ToInt32(rbTaxasion.SelectedItem.Value);
            if (ddlRecItems.SelectedItem.Value != "0")
            {
                ResourceID = Convert.ToInt32(ddlRecItems.SelectedItem.Value);
            }

            ds = objAtt.GetEmployeesByWSDEptNature_ByTransaction(WSID, DeptID, null, ResourceID);

            //DataRow dr;
            //dr = ds.Tables[0].NewRow();
            //dr[0] = 0;
            //dr[1] = "--Select--";
            //ds.Tables[0].Rows.InsertAt(dr, 0);
            //ViewState["Machinery"] = ds;

            ddlSearcMech.DataSource = ds;
            ddlSearcMech.DataTextField = "Name";
            ddlSearcMech.DataValueField = "EmpId";
            ddlSearcMech.DataBind();
            ddlSearcMech.Items.Insert(0, new ListItem("--All--", "0"));
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            topmenu.MenuId = GetParentMenuId();
            topmenu.ModuleId = Convert.ToInt32(Application["ModuleId"].ToString());
            topmenu.RoleID = Convert.ToInt32(Session["RoleId"].ToString());
            topmenu.SelectedMenu = Convert.ToInt32(mid.ToString());
            topmenu.DataBind();
            Session["menuname"] = menuname;
            Session["menuid"] = menuid;
            Session["MId"] = mid;
            if (!IsPostBack)
            {
                ViewState["NRIDocs"] = "";
                lblGroup.Visible = lblNewItems.Visible = ddlGroup.Visible = ddlNewItems.Visible = btnAddNew.Visible = false;
                tblUnRecon.Visible = true;
                tblReconciled.Visible = false;
                trReconciled.Visible = false;

                BindResourceTypes();
                BindMechinaries();
                GetDepartments();
                GetWorkSites();
                BindDesignations();
                //  BindReconsileGrid();
                // tblSaving.Visible = false;
                PageTax.Visible = false;

                ViewState["RecDataset"] = "";
                //btnSearch_Click(sender, e);
                rbTaxasion_SelectedIndexChanged(sender, e);
            }
        }
        public int GetMechIndex(string Value)
        {
            return AllMechinaries.IndexOf(Value);
        }
        public static ArrayList AllMechinaries = new ArrayList();
        public int GetParentMenuId()
        {
            string URL = Request.Url.Segments[Request.Url.Segments.Length - 1] + Request.Url.Query;
            int RoleId = Convert.ToInt32(Session["RoleId"].ToString());
            int ModuleId = Convert.ToInt32(Application["ModuleId"].ToString());
            DataSet ds = new DataSet();
            ds = Common.GetAllowed(RoleId, ModuleId, URL);
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
                btnAddNew.Enabled = (bool)ds.Tables[0].Rows[0]["Editable"];
                gvUnReconciled.Columns[4].Visible = (bool)ds.Tables[0].Rows[0]["Editable"];
                gvEdit.Columns[10].Visible = (bool)ds.Tables[0].Rows[0]["Editable"];
                gvRecosiled.Columns[12].Visible = (bool)ds.Tables[0].Rows[0]["Editable"];
                gvRecosiled.Columns[13].Visible = (bool)ds.Tables[0].Rows[0]["Editable"];
                gvFinalEdit.Columns[10].Visible = (bool)ds.Tables[0].Rows[0]["Editable"];

            }

            return MenuId;
        }
        public void BindMechinaries()
        {
            DataSet ds = new DataSet();

            // modify by pratap to get the employees by transaction wise date:13-02-2016
            //ds = objAtt.GetEmployeesByWSDEptNature(null, null, null);
            ds = objAtt.GetEmployeesByWSDEptNature_ByTransaction(null, null, null, null);
            //ddlMechinery.DataSource=ds;
            //ddlMechinery.DataTextField = "SubResourceName";
            //ddlMechinery.DataValueField = "SubResID";
            //ddlMechinery.DataBind();
            //ddlMechinery.Items.Insert(0,new ListItem("--Select--"));

            //DataRow dr;
            //dr = ds.Tables[0].NewRow();
            //dr[0] = 0;
            //dr[1] = "--Select--";
            //ds.Tables[0].Rows.InsertAt(dr, 0);
            ViewState["Machinery"] = ds;


            ddlSearcMech.DataSource = ds;
            ddlSearcMech.DataTextField = "Name";
            ddlSearcMech.DataValueField = "EmpId";
            ddlSearcMech.DataBind();
            ddlSearcMech.Items.Insert(0, new ListItem("--All--", "0"));

            AllMechinaries = new ArrayList();
            foreach (DataRow drMech in ds.Tables[0].Rows)
                AllMechinaries.Add(drMech["EmpId"].ToString());

        }
        public void BindResourceTypes()
        {
            DataSet ds = new DataSet();
            // added by pratap date:13-02-2016
            //ds = AttendanceDAC.HR_GetSatuatoryItems();
            ds = AttendanceDAC.HR_GetSatuatoryItems_ByTransaction();
            ddlItems.DataSource = ds;
            ddlItems.DataTextField = "ResourceName";
            ddlItems.DataValueField = "ResourceID";
            ddlItems.DataBind();
            ddlItems.Items.Insert(0, new ListItem("--Select--", "0"));
            ddlItems.Items.Add(new ListItem("<<Add New>>", "-1"));

            ddlRecItems.DataSource = ds;
            ddlRecItems.DataTextField = "ResourceName";
            ddlRecItems.DataValueField = "ResourceID";
            ddlRecItems.DataBind();
            ddlRecItems.Items.Insert(0, new ListItem("--Select--", "0"));
        }

        protected void rbTaxasion_SelectedIndexChanged(object sender, EventArgs e)
        {
            tblEdit.Visible = false;
            trEdit.Visible = false;
            tblFinalEdit.Visible = false;
            int Item = Convert.ToInt32(rbTaxasion.SelectedValue);
            if (Item == 1)
            {
                tblUnRecon.Visible = true;
                tblReconciled.Visible = false;
                trReconciled.Visible = false;
                btnSearch_Click(sender, e);
            }
            else
            {
                tblUnRecon.Visible = false;
                tblReconciled.Visible = true;
                trReconciled.Visible = true;
                btnRecSearch_Click(sender, e);
                // BindReconsileGrid();
            }
        }
        protected void ddlItems_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlItems.SelectedValue != "-1")
                {

                    gvUnReconciled.DataSource = null;
                    gvUnReconciled.DataBind();
                    lblGroup.Visible = lblNewItems.Visible = ddlGroup.Visible = ddlNewItems.Visible = btnAddNew.Visible = false;
                    int ResourceID = Convert.ToInt32(ddlItems.SelectedValue);
                    DataSet ds = AttendanceDAC.HR_GetSRNItems(ResourceID);
                    gvUnReconciled.DataSource = ds;
                    gvUnReconciled.DataBind();
                    //tblSaving.Visible = false;
                }

                else
                {
                    lblGroup.Visible = lblNewItems.Visible = ddlGroup.Visible = ddlNewItems.Visible = btnAddNew.Visible = true;


                    DataSet ds = new DataSet();
                    ds = AttendanceDAC.SP_PM_SearchCategoriesByService();
                    ddlGroup.DataSource = ds;
                    ddlGroup.DataTextField = "Category_Name";
                    ddlGroup.DataValueField = "Category_Id";
                    ddlGroup.DataBind();
                    ddlGroup.Items.Insert(0, new ListItem("--Select--", "0"));

                }
            }
            catch { }

        }
        public string PONavigateUrl(string POID)
        {
            bool Fals = false;
            return "javascript:return window.open('ProPurchaseOrderPrint.aspx?id=" + POID + "&PON=" + 1 + "&tot=" + Fals + "' , '_blank')";
        }
        //protected void btnSave_Click(object sender, EventArgs e)
        //{
        //    int MechenaryID = Convert.ToInt32(ddlMechinery.SelectedValue);
        //    DateTime From = CodeUtil.ConverttoDate(txtfrom.Text, CodeUtil.DateFormat.DayMonthYear);
        //    DateTime To = CodeUtil.ConverttoDate(txtTo.Text, CodeUtil.DateFormat.DayMonthYear);
        //    DataAccessLayer.DALMachinery.EMS_InsUpReconsolisedItems(Convert.ToInt32(ViewState["SRNItemID"]), Convert.ToInt32(ViewState["SRNID"]), From, To,  Convert.ToInt32(Session["UserId"]));
        //    int ResourceID = Convert.ToInt32(ddlItems.SelectedValue);
        //    DataSet ds = DataAccessLayer.DALMachinery.EMS_GetSRNItems(ResourceID);
        //    gvUnReconciled.DataSource = ds;
        //    gvUnReconciled.DataBind();
        //    tblUnRecon.Visible =true;
        //    tblSaving.Visible = false;
        //    AlertMsg.MsgBox(Page, "Done!");
        //}
        public string ViewImage(string obj, string ID)
        {
            string ReturnVal = "";
            if (obj != "")
            {
                ReturnVal = "javascript:return window.open('./NRIDocs/" + ID + "." + obj + "', '_blank')";
            }
            return ReturnVal;

        }

        public string ViewInvImage(string obj, string SrnItemID)
        {
            string ReturnVal = "";
            if (obj != "")
            {
                ReturnVal = "javascript:return window.open('./SDNItemsImages/" + SrnItemID + "." + obj + "', '_blank')";
            }
            return ReturnVal;

        }
        public bool ViewVisible(string SrnItemID, int Status)
        {
            bool status = false;
            DataSet ds = objSRN.MMS_CheckPicInSRNNItems(int.Parse(SrnItemID));
            if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0)
            {
                int ImgCount = Convert.ToInt32(ds.Tables[1].Rows[0]["Invoiceimg"]);
                if (Status == 1)
                {
                    if (ImgCount > 0)
                        status = false;
                    else
                        status = true;
                }
                else
                {
                    if (ImgCount > 0)
                        status = true;
                    else
                        status = false;

                }
            }
            return status;
        }
        public string Reconsolise(string SrnItemID, string ResourceID, string SRNID)
        {
            //tblUnRecon.Visible = true;
            //tblSaving.Visible = false;
            //ViewState["SRNItemID"] = SrnItemID;
            //ViewState["SRNID"] = SRNID;
            //DataSet ds = DataAccessLayer.DALMachinery.EMS_GetMechinariesOwnByResourceID(ResourceID);
            //ddlMechinery.DataSource = ds;
            //ddlMechinery.DataBind();
            //ddlMechinery.Items.Insert(0,new ListItem("--Select--"));
            return null;
        }
        public void BindReconsileGrid(int ResourID)
        {

            if (ddlRecItems.SelectedItem.Value != "0")
            {
                if (ResourID == 0)
                {
                    ResourID = Convert.ToInt32(ddlRecItems.SelectedItem.Value);
                }
                int? SiteID = null;
                int? DeptID = null;
                int? DesigID = null;
                int? EmpID = null;

                if (ddlRecWs.SelectedItem.Value != "0")
                {
                    SiteID = Convert.ToInt32(ddlRecWs.SelectedItem.Value);
                }
                if (ddlRecDept.SelectedItem.Value != "0")
                {
                    DeptID = Convert.ToInt32(ddlRecDept.SelectedItem.Value);
                }
                if (ddlRecDesg.SelectedItem.Value != "0")
                {
                    DesigID = Convert.ToInt32(ddlRecDesg.SelectedItem.Value);
                }
                if (ddlSearcMech.SelectedItem.Value != "0")
                {
                    EmpID = Convert.ToInt32(ddlSearcMech.SelectedItem.Value);
                }

                gvRecosiled.DataSource = null;
                gvRecosiled.DataBind();
                DataSet ds = AttendanceDAC.HR_GetReconsiledSRNItems(objCommon, ResourID, SiteID, DeptID, DesigID, EmpID,"",0);
                if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0)
                {
                    gvRecosiled.DataSource = ds;
                    gvRecosiled.DataBind();
                    PageTax.Bind(objCommon.CurrentPage, objCommon.TotalPages, objCommon.NoofRecords, objCommon.PageSize);
                }
            }

            else
            {
                AlertMsg.MsgBox(Page, "Select Documenttype");
            }
        }

        protected void gvUnReconciled_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int GRNItemID = Convert.ToInt32(e.CommandArgument);
                if (e.CommandName == "Reconse")
                {
                    gvEdit.DataSource = null;
                    gvEdit.DataBind();

                    DataSet ds = AttendanceDAC.HR_SRNIDbySRNItemID(GRNItemID,0);
                    ViewState["RecDataset"] = ds;
                    gvEdit.DataSource = ds;
                    gvEdit.DataBind();
                    tblReconciled.Visible = false;
                    trReconciled.Visible = false;
                    tblUnRecon.Visible = false;
                    tblEdit.Visible = true;
                    trEdit.Visible = true;

                }
            }
            catch { }
        }

        protected void gvRecosiled_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            gvFinalEdit.Visible = false;
            if (e.CommandName == "Del")
            {
                int ID = Convert.ToInt32(e.CommandArgument);
                AttendanceDAC.HR_DelReconsledItem(ID);
                BindReconsileGrid(Convert.ToInt32(ddlItems.SelectedValue));
            }
            if (e.CommandName == "Edt")
            {
                txtWONO1.Text = "";
                txtWoName1.Text = "";
                txtApprovedBy1.Text = "";
                HijriGregDatePicker1.setGregorianText("");
                HijriGregDatePicker2.setGregorianText("");
                HijriGregDatePicker1.setHijriDateText("");
                HijriGregDatePicker2.setHijriDateText("");
                txtAltNumber1.Text = "";
                txtIssuePlace1.Text = "";
                txtIssuer1.Text = "";
                txtRemarks1.Text = "";
                txtSRNID1.Text = "";
                txtSRNItemID1.Text = "";
                txtExtention1.Text = "";
                DataSet ds1 = new DataSet();
                ds1 = objAtt.GetEmployeesByWSDEptNature(null, null, null);
                ddlMachinery1.DataSource = ds1;
                ddlMachinery1.DataTextField = "Name";
                ddlMachinery1.DataValueField = "EmpId";
                ddlMachinery1.DataBind();
                ddlMachinery1.Items.Insert(0, new ListItem("--Select--"));

                GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).Parent.Parent;
                string str = row.Cells[7].Text;
                string str1 = row.Cells[8].Text;

                tblFinalEdit.Visible = true;
                int ID = Convert.ToInt32(e.CommandArgument);
                gvFinalEdit.DataSource = null;
                gvFinalEdit.DataBind();

                //DataRow dr;
                //dr = ds.Tables[0].NewRow();
                //dr[0] = 0;
                //dr[1] = "--Select--";
                //ds.Tables[0].Rows.InsertAt(dr, 0);
                DataSet ds = AttendanceDAC.HR_GetEmpStaturyItems(ID);
                DataTable dt = new DataTable();
                CultureInfo arabicCulture = new CultureInfo("ar-SA");
                CultureInfo englishCulture = new CultureInfo("en-US");


                dt = ds.Tables[0];
                foreach (DataRow dtrow in dt.Rows)
                {
                    txtExtention1.Text = dtrow["Ext"].ToString();
                    txtWONO1.Text = dtrow["PONO"].ToString();
                    txtWoName1.Text = dtrow["PONAME"].ToString();
                    txtApprovedBy1.Text = Convert.ToString(dtrow["ApprovedBy"].ToString());
                    txtCreatedOn1.Text = dtrow["CreatedOn"].ToString();
                    ddlMachinery1.SelectedValue = Convert.ToString(dtrow["EmpID"].ToString());
                    HijriGregDatePicker1.setGregorianText(Convert.ToString(dtrow["From"].ToString()));
                    HijriGregDatePicker2.setGregorianText(Convert.ToString(dtrow["To"].ToString()));

                    HijriGregDatePicker1.setHijriDateText(str);
                    HijriGregDatePicker2.setHijriDateText(str1);

                    //DateTime tempDate = ParseDateTime(Convert.ToString(dtrow["From"]));
                    //row["From"] = tempDate.ToString("dd/MM/yyyy", arabicCulture.DateTimeFormat);                
                    //DateTime tempDate = Convert.ToDateTime(Convert.ToString(dtrow["From"].ToString()));                
                    //HijriGregDatePicker1.setHijriDateText(tempDate.ToString("dd/MM/yyyy", arabicCulture.DateTimeFormat));
                    //DateTime tempDate1 = ParseDateTime(Convert.ToString(dtrow["To"].ToString()));
                    //HijriGregDatePicker2.setHijriDateText(tempDate1.ToString("dd/MM/yyyy", arabicCulture.DateTimeFormat));

                    txtNumber1.Text = Convert.ToString(dtrow["Numeber"].ToString());
                    txtAltNumber1.Text = Convert.ToString(dtrow["AltNumber"].ToString());
                    txtIssuePlace1.Text = dtrow["IssuePlace"].ToString();
                    txtIssuer1.Text = dtrow["Issuer"].ToString();
                    txtRemarks1.Text = dtrow["Remarks"].ToString();
                    txtSRNID1.Text = dtrow["SRNID"].ToString();
                    txtSRNItemID1.Text = dtrow["SRNItemID"].ToString();
                }
                if (ds != null)
                {
                    gvFinalEdit.DataSource = ds;
                    gvFinalEdit.DataBind();
                    tblReconciled.Visible = false;
                    trReconciled.Visible = false;
                    tblUnRecon.Visible = false;
                    tblEdit.Visible = false;
                    trEdit.Visible = false;
                }
            }



        }



        protected void gvEdit_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int ID = Convert.ToInt32(e.CommandArgument);
            if (e.CommandName == "Edt")
            {
                try
                {
                    GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).Parent.Parent;
                    TextBox txtFrom = (TextBox)gvEdit.Rows[row.RowIndex].FindControl("txtVFrom");
                    TextBox txtTo = (TextBox)gvEdit.Rows[row.RowIndex].FindControl("txtVTo");
                    DropDownList ddlMach = (DropDownList)gvEdit.Rows[row.RowIndex].FindControl("ddlMachinery");
                    Label lblSRNID = (Label)gvEdit.Rows[row.RowIndex].FindControl("lblSRNID");
                    DateTime From = CodeUtil.ConverttoDate(txtFrom.Text, CodeUtil.DateFormat.DayMonthYear);
                    DateTime To = CodeUtil.ConverttoDate(txtTo.Text, CodeUtil.DateFormat.DayMonthYear);

                    TextBox txtNumber = (TextBox)gvEdit.Rows[row.RowIndex].FindControl("txtNumber");
                    TextBox txtAltNumber = (TextBox)gvEdit.Rows[row.RowIndex].FindControl("txtAltNumber");
                    TextBox txtIssuePlace = (TextBox)gvEdit.Rows[row.RowIndex].FindControl("txtIssuePlace");
                    TextBox txtIssuer = (TextBox)gvEdit.Rows[row.RowIndex].FindControl("txtIssuer");
                    TextBox txtRemarks = (TextBox)gvEdit.Rows[row.RowIndex].FindControl("txtRemarks");

                    FileUpload ImgUpload = (FileUpload)gvEdit.Rows[row.RowIndex].FindControl("ImgUpload");

                    if (txtFrom.Text != "" && txtTo.Text != "")
                    {

                        string filename = "", ext = string.Empty, path = "";

                        filename = ImgUpload.PostedFile.FileName;

                        if (filename != "")
                        {
                            ext = filename.Split('.')[filename.Split('.').Length - 1];
                        }
                        else
                        {
                            if (ID != 0)
                            {
                                ext = ViewState["NRIDocs"].ToString();
                            }
                            else
                            {
                                ext = "";
                            }
                        }


                        ID = Convert.ToInt32(AttendanceDAC.HR_InsUpReconsolisedItems(ID, Convert.ToInt32(lblSRNID.Text), From, To,  Convert.ToInt32(Session["UserId"]), Convert.ToInt32(ddlMach.SelectedValue)
                                                                   , txtNumber.Text, txtAltNumber.Text, txtIssuePlace.Text, txtIssuer.Text, txtRemarks.Text, ext));

                        if (filename != "")
                        {
                            if (ID != 0)
                            {
                                path = Server.MapPath(".\\NRIDocs\\" + ID + "." + ext);
                                ImgUpload.PostedFile.SaveAs(path);
                            }
                        }

                        gvUnReconciled.DataSource = null;
                        gvUnReconciled.DataBind();
                        int ResourceID = Convert.ToInt32(ddlItems.SelectedValue);
                        DataSet ds = AttendanceDAC.HR_GetSRNItems(ResourceID);
                        gvUnReconciled.DataSource = ds;
                        gvUnReconciled.DataBind();
                        tblUnRecon.Visible = true;
                        tblEdit.Visible = false;
                        trEdit.Visible = false;

                        //tblSaving.Visible = false;
                        //AlertMsg.MsgBox(Page, "Done!");
                    }
                    else
                    {
                        AlertMsg.MsgBox(Page, "Enter valid Dates!");
                    }
                }
                catch { AlertMsg.MsgBox(Page, "Unable to update!"); }
            }
        }
        //protected void lnkAddNew_Click(object sender, EventArgs e)
        //{

        //    lblGroup.Visible = lblNewItems.Visible = ddlGroup.Visible = ddlNewItems.Visible = btnAddNew.Visible = true;


        //    DataSet ds = new DataSet();
        //    ds = AttendanceDAC.SP_PM_SearchCategoriesByService();
        //    ddlGroup.DataSource = ds;
        //    ddlGroup.DataTextField = "Category_Name";
        //    ddlGroup.DataValueField = "Category_Id";
        //    ddlGroup.DataBind();
        //    ddlGroup.Items.Insert(0, new ListItem("--Select--", "0"));

        //}
        protected void btnAddNew_Click(object sender, EventArgs e)
        {
            lblGroup.Visible = lblNewItems.Visible = ddlGroup.Visible = ddlNewItems.Visible = btnAddNew.Visible = false;

            int ResurceID = Convert.ToInt32(ddlNewItems.SelectedValue);
            AttendanceDAC.HR_InsNewSatuatoryItems(ResurceID);
            BindResourceTypes();
            AlertMsg.MsgBox(Page, "Done!");
        }
        protected void ddlGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            int GroupID = Convert.ToInt32(ddlGroup.SelectedValue);
            DataSet ds = AttendanceDAC.PM_GroupWiseItems_IndentByService(GroupID);
            ddlNewItems.DataSource = ds;
            ddlNewItems.DataTextField = "item_desc";
            ddlNewItems.DataValueField = "item_id";
            ddlNewItems.DataBind();
            ddlNewItems.Items.Insert(0, new ListItem("--Select--", "0"));

        }
        //protected void ddlSearcMech_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    DataSet ds = AttendanceDAC.HR_SearchReconsiledItems(Convert.ToInt32(ddlSearcMech.SelectedValue));
        //    gvRecosiled.DataSource = ds;
        //    gvRecosiled.DataBind();
        //    ddlRecItems.SelectedIndex = 0; ;

        //}
        protected void gvFinalEdit_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int ID = Convert.ToInt32(e.CommandArgument);
            if (e.CommandName == "Edt")
            {
                try
                {
                    GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).Parent.Parent;
                    TextBox txtFrom = (TextBox)gvFinalEdit.Rows[row.RowIndex].FindControl("txtVFrom");

                    TextBox txtTo = (TextBox)gvFinalEdit.Rows[row.RowIndex].FindControl("txtVTo");
                    DropDownList ddlMach = (DropDownList)gvFinalEdit.Rows[row.RowIndex].FindControl("ddlMachinery");
                    Label lblSRNID = (Label)gvFinalEdit.Rows[row.RowIndex].FindControl("lblSRNID");
                    DateTime From = CodeUtil.ConverttoDate(txtFrom.Text, CodeUtil.DateFormat.DayMonthYear);
                    DateTime To = CodeUtil.ConverttoDate(txtTo.Text, CodeUtil.DateFormat.DayMonthYear);

                    TextBox txtNumber = (TextBox)gvFinalEdit.Rows[row.RowIndex].FindControl("txtNumber");
                    TextBox txtAltNumber = (TextBox)gvFinalEdit.Rows[row.RowIndex].FindControl("txtAltNumber");
                    TextBox txtIssuePlace = (TextBox)gvFinalEdit.Rows[row.RowIndex].FindControl("txtIssuePlace");
                    TextBox txtIssuer = (TextBox)gvFinalEdit.Rows[row.RowIndex].FindControl("txtIssuer");
                    TextBox txtRemarks = (TextBox)gvFinalEdit.Rows[row.RowIndex].FindControl("txtRemarks");
                    FileUpload ImgUpload = (FileUpload)gvFinalEdit.Rows[row.RowIndex].FindControl("ImgUpload");
                    Label lblImgExta = (Label)gvFinalEdit.Rows[row.RowIndex].FindControl("lblImgExta");


                    string filename = "", ext = string.Empty, path = "";

                    filename = ImgUpload.PostedFile.FileName;

                    if (filename != "")
                    {
                        ext = filename.Split('.')[filename.Split('.').Length - 1];
                    }
                    else
                    {
                        if (ID != 0)
                        {
                            ext = lblImgExta.Text;
                        }
                        else
                        {
                            ext = "";
                        }
                    }
                    if (txtFrom.Text != "" && txtTo.Text != "")
                    {
                        ID = AttendanceDAC.HR_InsUpReconsolisedItems(ID, Convert.ToInt32(lblSRNID.Text), From, To,  Convert.ToInt32(Session["UserId"]), Convert.ToInt32(ddlMach.SelectedValue)
                            , txtNumber.Text, txtAltNumber.Text, txtIssuePlace.Text, txtIssuer.Text, txtRemarks.Text, ext);

                        if (filename != "")
                        {
                            if (ID != 0)
                            {
                                path = Server.MapPath(".\\NRIDocs\\" + ID + "." + ext);
                                ImgUpload.PostedFile.SaveAs(path);
                            }
                        }

                        tblUnRecon.Visible = false;
                        tblReconciled.Visible = true;
                        trReconciled.Visible = true;
                        //BindView();
                        tblFinalEdit.Visible = false;
                        //tblSaving.Visible = false;
                        tblEdit.Visible = false;
                        trEdit.Visible = false;

                        AlertMsg.MsgBox(Page, "Done!");
                    }
                    else
                    {
                        AlertMsg.MsgBox(Page, "Enter Valid Dates!");
                    }
                }
                catch { AlertMsg.MsgBox(Page, "Unable to update!"); }
            }
        }
        //protected void ddlRecItems_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    // BindReconsileGrid(Convert.ToInt32(ddlRecItems.SelectedValue));
        //    BindTaxition(objCommon);
        //    ddlSearcMech.SelectedIndex = 0;
        //}

        int? WSID = null;
        int? DeptID = null;
        int? DesigID = null;
        int? RecType = null;
        int? ResourceID = null;
        protected void btnSearch_Click(object sender, EventArgs e)
        {

            if (ddlWorksite.SelectedItem.Value != "0")
            {
                WSID = Convert.ToInt32(ddlWorksite.SelectedItem.Value);
            }
            if (ddlDepartment.SelectedItem.Value != "0")
            {
                DeptID = Convert.ToInt32(ddlDepartment.SelectedItem.Value);

            } if (ddlDesif2.SelectedItem.Value != "0")
            {
                DesigID = Convert.ToInt32(ddlDesif2.SelectedItem.Value);

            }
            RecType = Convert.ToInt32(rbTaxasion.SelectedItem.Value);
            if (ddlItems.SelectedItem.Value != "0")
            {
                ResourceID = Convert.ToInt32(ddlDesif2.SelectedItem.Value);
            }


            DataSet dsUnReEmp = new DataSet();
            dsUnReEmp = objAtt.GetEmpByWSDEptNatureForDoc(WSID, DeptID, DesigID, ResourceID, RecType);

            DataRow dr;
            dr = dsUnReEmp.Tables[0].NewRow();
            dr[0] = 0;
            dr[1] = "--Select--";
            dsUnReEmp.Tables[0].Rows.InsertAt(dr, 0);
            ViewState["Machinery"] = dsUnReEmp;

            // commented by pratap to filter the Empid here: 13-02-2016
            //ddlSearcMech.DataSource = dsUnReEmp;
            //ddlSearcMech.DataTextField = "Name";
            //ddlSearcMech.DataValueField = "EmpId";
            //ddlSearcMech.DataBind();
            //ddlSearcMech.Items.Insert(0, new ListItem("--All--", "0"));

            AllMechinaries = new ArrayList();
            foreach (DataRow drMech in dsUnReEmp.Tables[0].Rows)
                AllMechinaries.Add(drMech["EmpId"].ToString());

            if (ViewState["RecDataset"] != "")
            {
                gvEdit.DataSource = null;
                gvEdit.DataBind();
                DataSet dsRec = new DataSet();
                dsRec = (DataSet)ViewState["RecDataset"];
                gvEdit.DataSource = dsRec;
                gvEdit.DataBind();
            }
            else
            {
                gvEdit.DataSource = null;
                gvEdit.DataBind();
                PageTax.Visible = false;

            }

        }

        protected void btnRecSearch_Click(object sender, EventArgs e)
        {
            if (ddlRecItems.SelectedItem.Value != "0")
            {
                if (ddlRecWs.SelectedItem.Value != "0")
                {
                    WSID = Convert.ToInt32(ddlRecWs.SelectedItem.Value);
                }
                if (ddlRecDept.SelectedItem.Value != "0")
                {
                    DeptID = Convert.ToInt32(ddlRecDept.SelectedItem.Value);

                } if (ddlDesif2.SelectedItem.Value != "0")
                {
                    DesigID = Convert.ToInt32(ddlDesif2.SelectedItem.Value);

                }
                RecType = Convert.ToInt32(rbTaxasion.SelectedItem.Value);
                if (ddlRecItems.SelectedItem.Value != "0")
                {
                    ResourceID = Convert.ToInt32(ddlRecItems.SelectedItem.Value);
                }

                DataSet dsReEmp = new DataSet();

                dsReEmp = objAtt.GetEmpByWSDEptNatureForDoc(WSID, DeptID, DesigID, ResourceID, RecType);

                DataRow dr;
                dr = dsReEmp.Tables[0].NewRow();
                dr[0] = 0;
                dr[1] = "--All--";
                dsReEmp.Tables[0].Rows.InsertAt(dr, 0);
                ViewState["Machinery"] = dsReEmp;


                //ddlSearcMech.DataSource = dsReEmp;
                //ddlSearcMech.DataTextField = "Name";
                //ddlSearcMech.DataValueField = "EmpId";
                //ddlSearcMech.DataBind();


                AllMechinaries = new ArrayList();
                foreach (DataRow drMech in dsReEmp.Tables[0].Rows)
                    AllMechinaries.Add(drMech["EmpId"].ToString());

                BindView();
            }
            else
            {
                //AlertMsg.MsgBox(Page, "Select Document Type");

            }
        }


        public void GetDepartments()
        {
            DataSet dsDept = new DataSet();
            // modify by pratap to get the department by transaction dt:13-02-2016
            //dsDept = objAtt.GetDepartments(0);
            dsDept = objAtt.GetDepartments_ByTransaction(0);
            ddlDepartment.DataSource = dsDept.Tables[0];
            ddlDepartment.DataTextField = "DeptName";
            //ddlDepartment.DataTextField = "DepartmentName";
            ddlDepartment.DataValueField = "DepartmentUId";
            ddlDepartment.DataBind();
            ddlDepartment.Items.Insert(0, new ListItem("---ALL---", "0", true));


            ddlRecDept.DataSource = dsDept.Tables[0];
            ddlRecDept.DataTextField = "DeptName";
            //ddlDepartment.DataTextField = "DepartmentName";
            ddlRecDept.DataValueField = "DepartmentUId";
            ddlRecDept.DataBind();
            ddlRecDept.Items.Insert(0, new ListItem("---ALL---", "0", true));

        }
        public void GetWorkSites()
        {
            DataSet dsWs = new DataSet();
            // MODIFY BY PRATAP DATE: 13-02-2016
            //dsWs = objAtt.GetWorkSiteByEmpID( Convert.ToInt32(Session["UserId"]), Convert.ToInt32(Session["CompanyID"]), Convert.ToInt32(Session["RoleId"]));
            dsWs = objAtt.GetWorkSiteByEmpID_ByTransaction( Convert.ToInt32(Session["UserId"]), Convert.ToInt32(Session["CompanyID"]), Convert.ToInt32(Session["RoleId"]));

            ddlWorksite.DataSource = dsWs.Tables[0];
            ddlWorksite.DataTextField = "Site_Name";
            ddlWorksite.DataValueField = "Site_ID";
            ddlWorksite.DataBind();
            ddlWorksite.Items.Insert(0, new ListItem("---All---", "0", true));
            //ddlWorksite.SelectedItem.Value = "1";



            ddlRecWs.DataSource = dsWs.Tables[0];
            ddlRecWs.DataTextField = "Site_Name";
            ddlRecWs.DataValueField = "Site_ID";
            ddlRecWs.DataBind();
            ddlRecWs.Items.Insert(0, new ListItem("---All---", "0", true));
            // ddlRecWs.SelectedItem.Value = "1";
        }

        public void BindDesignations()
        {
            DataSet ds = new DataSet();
            // added by pratap date: 13-02-2016
            //ds = (DataSet)objAtt.GetDesignations();
            ds = (DataSet)objAtt.GetDesignations_ByTransaction();

            ddlDesif2.DataSource = ds;
            ddlDesif2.DataTextField = "Designation";
            ddlDesif2.DataValueField = "DesigId";
            ddlDesif2.DataBind();
            ddlDesif2.Items.Insert(0, new ListItem("---ALL---", "0", true));


            ddlRecDesg.DataSource = ds;
            ddlRecDesg.DataTextField = "Designation";
            ddlRecDesg.DataValueField = "DesigId";
            ddlRecDesg.DataBind();
            ddlRecDesg.Items.Insert(0, new ListItem("---ALL---", "0", true));

        }
        public static DateTime ParseDateTime(string dateString)
        {
            DateTime dateTime;
            if (!DateTime.TryParse(dateString, CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out dateTime))
            {
                if (!DateTime.TryParse(dateString, CultureInfo.CurrentCulture, DateTimeStyles.AssumeUniversal, out dateTime))
                {
                    try
                    {
                        dateTime = DateTime.Parse(dateString, CultureInfo.InvariantCulture, DateTimeStyles.AssumeLocal);
                    }
                    catch (FormatException)
                    {
                        // Try to extract at least year from the string (the longest digits substring)
                        var yearMatch = Regex.Matches(dateString, @"\d{4}").Cast<Match>().FirstOrDefault();
                        if (yearMatch == null || string.IsNullOrWhiteSpace(yearMatch.Value))
                        {
                            throw;
                        }

                        // Only year really matters for Max and Min values of DateTime
                        var year = int.Parse(yearMatch.Value, CultureInfo.InvariantCulture);

                        // Try to determine what do we have (Min or Max value)
                        if (year == DateTime.MaxValue.Year)
                        {
                            dateTime = DateTime.MaxValue;
                        }
                        else
                        {
                            if (year == DateTime.MinValue.Year)
                            {
                                dateTime = DateTime.MinValue;
                            }
                            else
                            {
                                throw;
                            }
                        }
                    }
                }
            }

            dateTime = dateTime.ToUniversalTime();
            return dateTime;
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {

            try
            {
                CultureInfo englishCulture = new CultureInfo("en-US");
                int ID = 0;
                DateTime From = CodeUtil.ConverttoDate(HijriGregDatePicker1.getGregorianDateText, CodeUtil.DateFormat.DayMonthYear);
                //string strFrom = HijriGregDatePicker2.getGregorianDateText;
                DateTime To = CodeUtil.ConverttoDate(HijriGregDatePicker2.getGregorianDateText, CodeUtil.DateFormat.DayMonthYear);
                //DateTime From1=CodeUtil.ConverttoDateConvertToDate(txtDate.Text, DateFormat.ddMMMyyyy)

                //DateTime From = new DateTime(Convert.ToInt32(HijriGregDatePicker1.getGregorianDateText.Split('/')[2]), Convert.ToInt32(HijriGregDatePicker1.getGregorianDateText.Split('/')[1]), Convert.ToInt32(HijriGregDatePicker1.getGregorianDateText.Split('/')[0]));
                //DateTime To = new DateTime(Convert.ToInt32(HijriGregDatePicker2.getGregorianDateText.Split('/')[2]), Convert.ToInt32(HijriGregDatePicker2.getGregorianDateText.Split('/')[1]), Convert.ToInt32(HijriGregDatePicker2.getGregorianDateText.Split('/')[0]));

                //TextBox2.Text = tempDate.ToString("dd/MM/yyyy");
                string filename = "", ext = string.Empty, path = "";

                filename = ImgUpload1.PostedFile.FileName;

                if (filename != "")
                {
                    ext = filename.Split('.')[filename.Split('.').Length - 1];
                }
                else
                {
                    if (ID != 0)
                    {
                        ext = txtExtention1.Text;
                    }
                    else
                    {
                        ext = "";
                    }
                }

                if (HijriGregDatePicker1.getGregorianDateText != "" && HijriGregDatePicker2.getGregorianDateText != "")
                {
                    ID = AttendanceDAC.HR_InsUpReconsolisedItems(Convert.ToInt32(txtSRNItemID1.Text), Convert.ToInt32(txtSRNID1.Text), From,
                        To,
                         Convert.ToInt32(Session["UserId"]), Convert.ToInt32(ddlMachinery1.SelectedValue)
                        , txtNumber1.Text, txtAltNumber1.Text, txtIssuePlace1.Text, txtIssuer1.Text, txtRemarks1.Text, ext);

                    if (filename != "")
                    {
                        if (ID != 0)
                        {
                            path = Server.MapPath(".\\NRIDocs\\" + ID + "." + ext);
                            ImgUpload1.PostedFile.SaveAs(path);
                        }
                    }
                    AlertMsg.MsgBox(Page, "Done!");
                    tblUnRecon.Visible = false;
                    tblReconciled.Visible = true;
                    trReconciled.Visible = true;
                    BindView();
                    tblFinalEdit.Visible = false;
                    //tblSaving.Visible = false;
                    tblEdit.Visible = false;
                    trEdit.Visible = false;
                }
                else
                {

                }
            }
            catch (Exception)
            {
                AlertMsg.MsgBox(Page, "Not Done!");
                throw;
            }
        }
    }
}