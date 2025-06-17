using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using DataAccessLayer;
using AECLOGIC.HMS.BLL;
using Aeclogic.Common.DAL;
namespace AECLOGIC.ERP.HMS
{
    public partial class ProPurchaseOrderPrint : WebFormMaster
    {
        int mid = 0; bool viewall; string menuname; string menuid;
        // Common1 obj = new Common1();
        // Common objCommon = new Common();
        protected override void OnInit(EventArgs e)
        {
           // ModuleID = 1;
            base.OnInit(e);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Convert.ToInt32(Request.QueryString["Id"]) > 0)
            {
                if (!IsPostBack)
                {
                    try
                    {
                        //Body.Style.Add("background", "url('IMAGES/CANCEL.jpg') white center no-repeat fixed"); 
                        BindPO();
                        if (Request.QueryString[1].ToString().Trim() == "1") //with out head and footer
                        {
                            trFooter.Visible = false;
                            TblHeader.Visible = false;
                            Label lbl = new Label();
                            lbl.Text = string.Empty;
                            lbl.Height = Unit.Pixel(100);
                            trHeader.Controls.Add(lbl);
                            TDPrint.Height = Unit.Pixel(650).ToString();
                        }
                        else  //with head and footer
                        {
                            trFooter.Visible = true;
                            TblHeader.Visible = true;
                            TDPrint.Height = Unit.Pixel(600).ToString();
                        }
                    }
                    catch (Exception Ex) { AlertMsg.MsgBox(Page,Ex.Message.ToString(),AlertMsg.MessageType.Error); }
                }
            }
            else
                AlertMsg.MsgBox(Page,"No PO/Wo Not Found");
        }
        //void AlertMsg.MsgBox(Page,string alert)
        //{
        //    string strScript = "<script language='javascript'>alert(\'" + alert + "\');</script>";
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "PopupCP", strScript, false);
        //}
        public string v = "2";
        void BindPO()
        {
            SqlParameter[] p = new SqlParameter[1];
            p[0] = new SqlParameter("@POID", Request.QueryString["id"]);
            DataSet ds = SQLDBUtil.ExecuteDataset("PM_PURCHASEORDER_DETAILS", p);
            if (ds.Tables.Count == 0) return;
            if (ds.Tables[1].Rows.Count > 0)
            {
                GVdetails.DataSource = ds.Tables[1];
                DataRow[] drSelected = ds.Tables[1].Select("ITEMDESC='<b>TOTAL AMOUNT</b>'");
                if (drSelected.Length > 0)
                    ds.Tables[1].Rows.Remove(drSelected[0]);
                lblvalueinwords.Visible = false;
                lblvalueinwordsLabel.Visible = false;
                //}
                GVdetails.DataBind();
            }
            if (ds.Tables[2].Rows.Count > 0)
            {
                GVTerms.DataSource = ds.Tables[2];
                GVTerms.DataBind();
            }
            lblBudgetCode.Text = "";
            lblHeadOfficeAddress.Text = "";
            lblindentdate.Text = ds.Tables[0].Rows[0]["INDENTDATE"].ToString();
            lblIndentNumberDate.Text = ds.Tables[0].Rows[0]["INDENTID"].ToString() + ": " + ds.Tables[0].Rows[0]["INDENT_NUMBER"].ToString();
            if (v.Trim() == "1")
                lblNote.Text = "Purchase Order For<br/>" + ds.Tables[0].Rows[0]["NOTE"].ToString();
            else
                lblNote.Text = "Work Order For<br/>" + ds.Tables[0].Rows[0]["NOTE"].ToString();
            lblPODate.Text = ds.Tables[0].Rows[0]["PODATE"].ToString();
            lblPOName.Text = ds.Tables[0].Rows[0]["PONAME"].ToString();
            int Ammedid = Convert.ToInt32(ds.Tables[0].Rows[0]["AmmentNo"].ToString());
            if (Ammedid > 0)
            {
                lblPOName.Text = lblPOName.Text + " / AMD-" + Ammedid.ToString();
            }
            int pono = Convert.ToInt32(ds.Tables[0].Rows[0]["PONO"].ToString());
            lblPoNO.Text = pono.ToString("000000");
            lblProject.Text = ds.Tables[0].Rows[0]["SITE_NAME"].ToString();
            lblTIN.Text = "28704408724(Andhra Pradesh)<br/>10050834031(Bihar)";
            lblVendorAddress.Text = ds.Tables[0].Rows[0]["VENDOR_NAME"].ToString() + "<br/>" + ds.Tables[0].Rows[0]["VENDOR_ADDRESS"].ToString();
            lblworksiteAddress.InnerHtml = ds.Tables[0].Rows[0]["SITE_ADDRESS"].ToString();  //.Replace(",", "<br/>"); ;
            double povaluee = Convert.ToDouble(ds.Tables[3].Rows[0][0].ToString());
            int j = Convert.ToInt32(Math.Ceiling(povaluee));
            string val = NumberToText(j);
            if (val.StartsWith("and") == true) { val = val.Remove(0, 3); }
            lblvalueinwords.Text = "<b>Rupees " + val + " Only </b> ";
            lblHeadOfficeAddress.Text = ds.Tables[4].Rows[0]["site_address"].ToString().Replace("<br/>", " ").Replace("\r", " ");
            lblVendorRep.Text = ds.Tables[0].Rows[0]["VENDOR_NAME"].ToString();
            SqlParameter[] pm = new SqlParameter[1];
            pm[0] = new SqlParameter("@Indentid", ds.Tables[0].Rows[0]["INDENTID"].ToString());
            DataSet DsStoreHead = SQLDBUtil.ExecuteDataset("PM_GetStoreHead", pm);
            SqlParameter[] par = new SqlParameter[1];
            par[0] = new SqlParameter("@POID", Request.QueryString["id"]);
            DataSet dsRcvr = SQLDBUtil.ExecuteDataset("PM_GetGoodsReceivers", par);
            if (dsRcvr.Tables.Count > 0 && dsRcvr.Tables[0].Rows.Count > 0)
            {
                Tr3.Visible = true;
                string str = string.Empty;
                if (dsRcvr.Tables[0].Rows.Count > 0)
                    str = "1 " + dsRcvr.Tables[0].Rows[0][0].ToString();
                if (dsRcvr.Tables[1].Rows.Count > 0)
                {
                    if (dsRcvr.Tables[0].Rows[0][0].ToString() != dsRcvr.Tables[1].Rows[0][0].ToString())
                        str = "1 " + dsRcvr.Tables[0].Rows[0][0].ToString() + " <br/>" + "2 " + dsRcvr.Tables[1].Rows[0][0].ToString();
                }
                lblstoreHead.Text = str;
                string str2 = string.Empty;
                if (dsRcvr.Tables[2].Rows.Count > 0)
                    str2 = "1 " + dsRcvr.Tables[2].Rows[0][0].ToString();
                if (dsRcvr.Tables[3].Rows.Count > 0)
                {
                    if (dsRcvr.Tables[2].Rows[0][0].ToString() != dsRcvr.Tables[3].Rows[0][0].ToString())
                        str2 = "1 " + dsRcvr.Tables[2].Rows[0][0].ToString() + " <br/>" + "2 " + dsRcvr.Tables[3].Rows[0][0].ToString();
                }
                lblMonitors.Text = str2;
            }
            else if (DsStoreHead.Tables[0].Rows.Count > 0)
            {
                string st = string.Empty;
                if (DsStoreHead.Tables[0].Rows.Count == 1)
                {
                    st = "1 " + DsStoreHead.Tables[0].Rows[0][0].ToString();
                }
                else
                {
                    st = "1 " + DsStoreHead.Tables[0].Rows[0][0].ToString() + " <br/>" + "2 " + DsStoreHead.Tables[0].Rows[1][0].ToString();
                }
                lblstoreHead.Text = st;
            }
            if (v.Trim() == "1")
            {
                lblfor.Text = "Purchase Order For:"; LBLpoNo_text.Text = "PO Ref No:"; lbldatewo_po.Text = "PO Date:";
            }
            else
            {
                lblfor.Text = "Work Order For:"; LBLpoNo_text.Text = "WO Ref No:"; lbldatewo_po.Text = "WO Date:";
            }
            lblvendrepcontact.Text = ds.Tables[0].Rows[0]["CONTACT_PERSON"].ToString() + "   " + ds.Tables[0].Rows[0]["MOBILE"].ToString();
            pm = new SqlParameter[2];
            pm[0] = new SqlParameter("@Poid", Request.QueryString["id"]);
            pm[1] = new SqlParameter("@IndentId", ds.Tables[0].Rows[0]["INDENTID"].ToString());
            DataSet dSRep = SQLDBUtil.ExecuteDataset("PM_PORasiedBy", pm);
            lblBssName.Text = dSRep.Tables[0].Rows[0][0].ToString();
            lblBssDesc.Text = dSRep.Tables[0].Rows[0][1].ToString();
            lblIndentedBy.Text = dSRep.Tables[0].Rows[1][0].ToString();
            lblVettedBy.Text = dSRep.Tables[0].Rows[2][0].ToString();
            dSRep.Dispose();
        }
        public string NumberToText(int number)
        {
            if (number == 0) return "Zero";
            if (number == -2147483648) return "Minus Two Hundred and Fourteen Crore Seventy Four Lakh Eighty Three Thousand Six Hundred and Forty Eight";
            int[] num = new int[4];
            int first = 0;
            int u, h, t;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            if (number < 0)
            {
                sb.Append("Minus ");
                number = -number;
            }
            string[] words0 = { "", "One ", "Two ", "Three ", "Four ", "Five ", "Six ", "Seven ", "Eight ", "Nine " };
            string[] words1 = { "Ten ", "Eleven ", "Twelve ", "Thirteen ", "Fourteen ", "Fifteen ", "Sixteen ", "Seventeen ", "Eighteen ", "Nineteen " };
            string[] words2 = { "Twenty ", "Thirty ", "Forty ", "Fifty ", "Sixty ", "Seventy ", "Eighty ", "Ninety " };
            string[] words3 = { "Thousand ", "Lakh ", "Crore " };
            num[0] = number % 1000; // units
            num[1] = number / 1000;
            num[2] = number / 100000;
            num[1] = num[1] - 100 * num[2]; // thousands
            num[3] = number / 10000000; // crores
            num[2] = num[2] - 100 * num[3]; // lakhs
            for (int i = 3; i > 0; i--)
            {
                if (num[i] != 0)
                {
                    first = i;
                    break;
                }
            }
            for (int i = first; i >= 0; i--)
            {
                if (num[i] == 0) continue;
                u = num[i] % 10; // ones
                t = num[i] / 10;
                h = num[i] / 100; // hundreds
                t = t - 10 * h; // tens
                if (h > 0) sb.Append(words0[h] + "Hundred ");
                if (u > 0 || t > 0)
                {
                    if (h > 0 || i == 0) sb.Append("and ");
                    if (t == 0)
                        sb.Append(words0[u]);
                    else if (t == 1)
                        sb.Append(words1[u]);
                    else
                        sb.Append(words2[t - 2] + words0[u]);
                }
                if (i != 0) sb.Append(words3[i - 1]);
            }
            return sb.ToString().TrimEnd();
        }
        public string ImageUrl(string Val)
        {
            string path = Server.MapPath(Val);
            if (!System.IO.File.Exists(path))
            {
                path = "ItemImages/nopic.JPG";
            }
            else
            {
                path = Val;
            }
            return path;
        }
    }
}