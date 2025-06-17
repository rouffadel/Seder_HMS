using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Globalization;

using Aeclogic.Common.DAL;
using AECLOGIC.HMS.BLL;
namespace AECLOGIC.ERP.HMS
{
    public partial class EditBillGoods : AECLOGIC.ERP.COMMON.WebFormMaster
    {
        int Mode; //int WSId = 0; 
        int GdnId = 0; int BillNO = 0; int PONo = 0;
        //int VendorId; 
        int ModuleId = 1;
       static int CompanyID;
       static int EMPID = 0;
       int? WSId = null; int? VendorId = null;
        SRNService objEdtGdns = new SRNService();

       static int EmpId; int mid = 0; string menuname; string menuid; int count;
        Common obj = new Common();
        protected override void OnInit(EventArgs e)
        {
            ModuleID = 1;
            base.OnInit(e);
        }
        protected void Page_Load(object sender, EventArgs e)
        {

            CompanyID = Convert.ToInt32(Session["CompanyID"].ToString());
            EmpId =  Convert.ToInt32(Session["UserId"]);

            {
               
                ModuleId = ModuleID;
                

                string URL = Request.Url.Segments[Request.Url.Segments.Length - 1] + Request.Url.Query;
                if (URL == "EditBillGoods.aspx?state=1" || URL == "EditBillGoods.aspx")
                {
                    Mode = 1;

                }
                else if (URL == "EditBillGoods.aspx?state=2")
                {
                    Mode = 2;


                }
                else if (URL == "EditBillGoods.aspx?state=3")
                {
                    Mode = 3;


                }
                if (!IsPostBack)
                {
                    GetParentMenuId();
                    SetUpScreen();
                    if (URL == "EditBillGoods.aspx?state=1" || URL == "EditBillGoods.aspx")
                    {
                        Mode = 1;
                        BindDropBillNos();
                    }
                    else if (URL == "EditBillGoods.aspx?state=2")
                    {
                        Mode = 2;
                        BindDropBillNos();

                    }
                    else if (URL == "EditBillGoods.aspx?state=3")
                    {
                        Mode = 3;
                        //BindDropBillNos();

                    }
                }
            }

            //selected();
        }

        private void SetUpScreen()
        {
            lstGdn3.SelectionMode = ListSelectionMode.Multiple;
          //  FillWorkSiteDropDown();
            FillDropDownVendor();
            // BindDropBillNos();
            //BindDataGrid();
        }

        public int GetParentMenuId()
        {

            int Id;
            if (Session["Guest"] != null)
                Id = 2;
            else
                Id = 1;
            string URL;
            if (Request.QueryString.Count > 0)
            {
                URL = Request.Url.Segments[Request.Url.Segments.Length - 1];
            }
            else
                URL = Request.Url.Segments[Request.Url.Segments.Length - 1] + Request.Url.Query;

            Session["CurrentPage"] = URL;

            //string URL = Request.Url.Segments[Request.Url.Segments.Length - 1] + Request.Url.Query;
            int RoleId = Convert.ToInt32(Session["RoleId"].ToString());
            int ModuleId = ModuleID;;

               

            DataSet ds = Common.GetAllowed(RoleId, ModuleId, URL);

            int MenuId = 0;
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
               
                MenuId = Convert.ToInt32(ds.Tables[0].Rows[0]["Under"]);
               

            }
            return MenuId;
        }

        private void FillDropDownVendor()
        {


               
            DataSet ds = SQLDBUtil.ExecuteDataset("MMS_DDL_ServiceBillsVendors");
            ddlVendor.DataValueField = "ID";
            ddlVendor.DataTextField = "Name";
            ddlVendor.Items.Add(new ListItem("------All------ ", "0"));
            ddlVendor.DataSource = ds;
            ddlVendor.DataBind();



        }

      

        private void BindDropBillNos()
        {

            FIllObject.FillEmptyDropDown(ref ddlBillNo);
            DataSet dsBillNo = objEdtGdns.MMS_GetBillNos(Mode, ModuleId);
            ddlBillNo.DataSource = dsBillNo;
            ddlBillNo.DataValueField = dsBillNo.Tables[0].Columns["BILLNO"].ToString();
            ddlBillNo.DataTextField = dsBillNo.Tables[0].Columns["BILLNO"].ToString();
            ddlBillNo.DataBind();
            if (ddlBillNo.SelectedValue != "0")
            {
                txtFrmDate.Text = dsBillNo.Tables[0].Rows[0][0].ToString(); ;
                txtToDate.Text = dsBillNo.Tables[0].Rows[0][1].ToString(); ;
            }
        }

        protected void btClear_Click(object sender, EventArgs e)
        {
            txtBillNo.Text = "";
            txtPONo.Text = "";
            txtgdn.Text = "";
            ddlVendor.SelectedIndex = 0;
            ddlVendor.SelectedIndex = 0;
        }

        private void BindDataGrid()
        {
         
           
            Mode = 3;
            if (ViewState["VendorID"] != null)
            {
                VendorId = Convert.ToInt32(ViewState["VendorID"]);
            }
            if (ViewState["WSID"] != null)
            {
                WSId = Convert.ToInt32(ViewState["WSID"]);
            }
           DataSet dsSerch = objEdtGdns.MMS_SerchGDNsForBillsAdjusting(objEdtGdns, VendorId, txtPONo.Text == String.Empty ? (int?)null : int.Parse(txtPONo.Text),
                txtBillNo.Text == string.Empty ? (int?)null : int.Parse(txtBillNo.Text), WSId,
                txtgdn.Text == string.Empty ? (int?)null : int.Parse(txtgdn.Text), Mode, ModuleId, CompanyID);
            /// if (dsSerch != null && dsSerch.Tables.Count != 0 && dsSerch.Tables[0].Rows.Count > 0)
            // {
            if (dsSerch.Tables[0].Rows.Count > 0)
            {

                lstGdn1.DataSource = dsSerch.Tables[0];
                lstGdn1.DataValueField = "GDNID";
                lstGdn1.DataTextField = "GDNID";
                lstGdn1.DataBind();
            }
            else { lstGdn1.DataSource = null; lstGdn1.Items.Clear(); lstGdn1.DataBind(); }
            if (dsSerch.Tables.Count >= 2 && dsSerch.Tables[1].Rows.Count > 0)
            {
                lstGdn3.Items.Clear();
                lstGdn3.SelectionMode = ListSelectionMode.Multiple;
                lstGdn3.DataSource = dsSerch.Tables[1];
                lstGdn3.DataValueField = "gdnid";
                lstGdn3.DataTextField = "gdnid";
                lstGdn3.DataBind();
                Hashtable htUnBilled = new Hashtable();
                foreach (DataRow dr in dsSerch.Tables[1].Rows)
                    htUnBilled.Add(dr["gdnid"].ToString(), dr["poid"].ToString());
                ViewState["htUnBilled"] = htUnBilled;
            }
            else { lstGdn3.DataSource = null; lstGdn3.Items.Clear(); lstGdn3.DataBind(); }
            BindBills(dsSerch);
            BindBillGDNs();

        }


        private void BindBills(DataSet dsSerch)
        {
            if (dsSerch.Tables[2].Rows.Count > 0)
            {
                ddlBillNo.Items.Clear();
                ddlBillNo.DataValueField = "BillNo";
                ddlBillNo.DataTextField = "BillNo";
                ddlBillNo.DataSource = dsSerch.Tables[2];
                ddlBillNo.DataBind();
            }
            else
            {
                ddlBillNo.Items.Clear();
                ddlBillNo.DataSource = null;
                ddlBillNo.DataBind();
                ddlBillNo.Items.Insert(0, new ListItem("No Bills Found", "0"));
            }
        }



        protected void ddlBillNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            int Val = Convert.ToInt32(ddlBillNo.SelectedValue);
            BindBillGDNs();
        }

        protected void BindBillGDNs()
        {
            if (ddlBillNo.SelectedValue != "0")
            {

                int BillNO = Convert.ToInt32(ddlBillNo.SelectedValue);
             DataSet   dsGdns = objEdtGdns.MMS_GetGdnsfromBill(BillNO, Mode);
                lstGdn2.DataSource = dsGdns;
                lstGdn2.DataValueField = "GDNID";
                lstGdn2.DataTextField = "GDNID";
                lstGdn2.DataBind();
                DataSet ds = objEdtGdns.MMS_GetBillDates(BillNO, Mode);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    txtFrmDate.Text = ds.Tables[0].Rows[0][0].ToString();
                    txtToDate.Text = ds.Tables[0].Rows[0][1].ToString();
                }
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            ListItem item = null;
            foreach (int index in lstGdn1.GetSelectedIndices())
            {
                item = lstGdn1.Items[index];
                GdnId = Convert.ToInt32(item.Text);
                objEdtGdns.MMS_MakeGdnUnBilled(GdnId, Mode, EmpId);
            }
            BindBillGDNs();
            BindDataGrid();
        }

        protected void btnForcetoBill_Click(object sender, EventArgs e)
        {
            int RetVal;

            ListItem item = null;
            foreach (int index in lstGdn3.GetSelectedIndices())
            {
                item = lstGdn3.Items[index];
                GdnId = Convert.ToInt32(lstGdn3.SelectedValue);
                BillNO = Convert.ToInt32(ddlBillNo.SelectedValue);
              DataSet  ds = objEdtGdns.MMS_ForcegdntoBill(BillNO, GdnId, Mode, EmpId);
                RetVal = Convert.ToInt32(ds.Tables[0].Rows[0][0]);
                if (RetVal == 0)
                { count = count + 1; }
                item.Selected = false;
            }
            if (count == 0)
            { AlertMsg.MsgBox(Page, "Done!"); }
            else
            { AlertMsg.MsgBox(Page, "Unable to force " + count + " GDNs to bill!PO must be same!!!"); }
            BindBillGDNs();
            BindDataGrid();

        }



        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindDataGrid();
        }

        protected void btClear_Click1(object sender, EventArgs e)
        {
            txtBillNo.Text = txtgdn.Text = txtPONo.Text = "";
            ddlVendor.SelectedIndex = 0;
          //  ddlWS.SelectedIndex = 0;
            txtSearchWorksite.Text = string.Empty;
        }

        protected void btnGenBill_Click(object sender, EventArgs e)
        {

            DataSet dsBill = new DataSet();
            DataTable dtBill = new DataTable();
            switch (Mode)
            {
                case 1:
                    dsBill.DataSetName = "GDNDataSet";
                    dtBill.TableName = "GDNTable";
                    dtBill.Columns.Add(new DataColumn("GDNId", typeof(System.Int32)));
                    dtBill.Columns.Add(new DataColumn("POId", typeof(System.Int32)));
                    dsBill.Tables.Add(dtBill);
                    break;
                case 2:
                    dsBill.DataSetName = "GDNDataSet";
                    dtBill.TableName = "GDNTable";
                    dtBill.Columns.Add(new DataColumn("GDNId", typeof(System.Int32)));
                    dtBill.Columns.Add(new DataColumn("WOId", typeof(System.Int32)));
                    dsBill.Tables.Add(dtBill);
                    break;
                case 3:
                    dsBill.DataSetName = "SRNDataSet";
                    dtBill.TableName = "SRNTable";
                    dtBill.Columns.Add(new DataColumn("SRNId", typeof(System.Int32)));
                    dtBill.Columns.Add(new DataColumn("WOId", typeof(System.Int32)));
                    dsBill.Tables.Add(dtBill);
                    break;
            }

            Hashtable htUnBilled = (Hashtable)ViewState["htUnBilled"];

            ListItem item = null;
            foreach (int index in lstGdn3.GetSelectedIndices())
            {

                item = lstGdn3.Items[index];

                DataRow drBill = dsBill.Tables[0].NewRow();
                drBill[0] = Convert.ToDouble(item.Text);
                drBill[1] = Convert.ToDouble(htUnBilled[item.Value]);
                dsBill.Tables[0].Rows.Add(drBill);

            }

            dsBill.AcceptChanges();
            SqlParameter[] par = new SqlParameter[2];
            par[0] = new SqlParameter("@GDNIDs", dsBill.GetXml());
            par[1] = new SqlParameter("@Mode", Mode);
            SQLDBUtil.ExecuteNonQuery("MMS_CreateNewBill", par);
            BindDataGrid();
            BindBillGDNs();

        }

        protected void btnChange_Click(object sender, EventArgs e)
        {
            DateTime? dtTodate = null;
            DateTime? dtFromDate = null;
            if (ddlBillNo.SelectedValue != "")
            {
                BillNO = Convert.ToInt32(ddlBillNo.SelectedValue);
                if (txtFrmDate.Text != string.Empty)
                {

                    dtFromDate = CodeUtil.ConverttoDate(txtFrmDate.Text, CodeUtil.DateFormat.DayMonthYear); //Convert.ToDateTime(txtGdnFromDate.Text);
                    if (txtToDate.Text != string.Empty)
                    {
                        dtTodate = CodeUtil.ConverttoDate(txtToDate.Text, CodeUtil.DateFormat.DayMonthYear).AddDays(1).AddMilliseconds(-31);
                    }
                    else
                    {
                        dtTodate = CodeUtil.ConverttoDate(txtFrmDate.Text, CodeUtil.DateFormat.DayMonthYear).AddSeconds(-31);
                    }
                }
                objEdtGdns.MMS_UpdateBillPeriod(BillNO, dtFromDate, dtTodate, Mode);
                AlertMsg.MsgBox(Page, "Done!");
            }

        }

        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionList(string prefixText, int count, string contextKey)
        {
          
            DataSet ds = AttendanceDAC.MMS_DDL_WorkSite_googlesearch(prefixText.Trim(),CompanyID,EMPID);
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


    }
}