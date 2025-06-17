using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

using Aeclogic.Common.DAL;

namespace AECLOGIC.ERP.HMS
{
    public partial class SRNPreReq : AECLOGIC.ERP.COMMON.WebFormMaster
    {
        SRNService objSRN = new SRNService();
        int EmpId; int SrnId; int Id;
        protected override void OnInit(EventArgs e)
        {
            ModuleID = 1;
            base.OnInit(e);
        }
        protected void Page_Load(object sender, EventArgs e)
        {

            Id = Convert.ToInt32(Session["SRN"]);
            Ajax.Utility.RegisterTypeForAjax(typeof(SRNPreReq));
            if (Request.QueryString["SRNID"] != null && Request.QueryString["SRNID"] != string.Empty)
            {
                SrnId = Convert.ToInt32(Request.QueryString["SRNID"]);
            }
            if (!IsPostBack)
            {
                BindGrid();
                btnSubmit.Attributes.Add("onclick", "javascript:return Validate('" + Id.ToString() + "'); ");
            }
            if (Id == 1)
            {
                gvRecieve.Columns[5].Visible = false;
                this.Title = "SDN Received";

            }
            else if (Id == 2)
            {
                gvRecieve.Columns[5].Visible = true;
                this.Title = "SDN Approved";
            }

        }

        public void BindGrid()
        {
            if (Request.QueryString["SRNID"] != null && Request.QueryString["SRNID"] != string.Empty)
            {
                SrnId = Convert.ToInt32(Request.QueryString["SRNID"]);

            }

            
          DataSet  ds = objSRN.MMS_GetSrnItems(SrnId, Id);
            // ds = objSRN.MMS_GetSrnItems(SrnId, Id);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                gvRecieve.DataSource = ds;
                gvRecieve.DataBind();
            }
            else
                gvRecieve.EmptyDataText = "No records Found";


        }

        public static DataSet BindDropDownList()
        {

            DataSet ds = SQLDBUtil.ExecuteDataset("MMS_DDL_EmployeeMaster");
            return ds;
        }


        protected void btnSubmit_OnClick(object sender, EventArgs e)
        {
            try
            {
                string Url;
                DataSet SRNSDataSet = new DataSet("SRNs");
                //int id = Convert.ToInt32(ID);
                DataTable dtbl = new DataTable("SRNTbl");
                dtbl.Columns.Add(new DataColumn("SRNItemId", typeof(System.Int32)));
                SRNSDataSet.Tables.Add(dtbl);

                foreach (GridViewRow gvRow in gvRecieve.Rows)
                {


                    Label lblSRNItemId = new Label();
                    lblSRNItemId = (Label)gvRow.FindControl("lblSRNItemID");
                    DataRow dr = dtbl.NewRow();
                    dr["SRNItemId"] = Convert.ToInt32(lblSRNItemId.Text);
                    dtbl.Rows.Add(dr);


                    EmpId =  Convert.ToInt32(Session["UserId"]);
                    int Distance = 0; decimal RcvdQty = 0;
                    // int id = Convert.ToInt32(ID.ToString());
                    Label lblSrnItemId = (Label)gvRow.Cells[0].Controls[0].FindControl("lblSRNItemID");
                    ViewState["srnItemId"] = lblSrnItemId.Text;
                    TextBox txtArrQty = (TextBox)gvRow.Cells[4].Controls[1].FindControl("txtArrivedQuantity");
                    TextBox txtRcvdQty = (TextBox)gvRow.Cells[5].Controls[0].FindControl("txtRcvdQty");
                    TextBox txtComments = (TextBox)gvRow.FindControl("txtComments");
                    DropDownList ddlChkBy = (DropDownList)gvRow.FindControl("ddlCheckedInBy");
                    Label lblDistance = (Label)gvRow.Cells[6].Controls[0].FindControl("lbldistance");
                    TextBox txtDistance = (TextBox)gvRow.Cells[6].Controls[1].FindControl("txtKMs");
                    Label lblGDNQuantity = (Label)gvRow.Cells[6].Controls[0].FindControl("lblGDNQuantity");
                    decimal arrqty =0;
                    if (Id == 1)
                    {
                        if (txtDistance.Text != "")
                            Distance = Convert.ToInt32(txtDistance.Text);
                        arrqty = Convert.ToDecimal(txtArrQty.Text);
                        Url = "SRNStatus.aspx?ID=1";
                    }
                    else if (Id == 2)
                    {
                        if (lblDistance.Text != "")
                            Distance = Convert.ToInt32(lblDistance.Text);
                        arrqty = Convert.ToDecimal(txtRcvdQty.Text);
                        Url = "SRNStatus.aspx?ID=2";
                    }
                   
                    int gdnqty = Convert.ToInt32(Convert.ToDouble(lblGDNQuantity.Text));

                    if (arrqty <= gdnqty)
                    {

                        int SrnItemId = Convert.ToInt32(lblSrnItemId.Text);
                        int RecivedBy = Convert.ToInt32(ddlChkBy.SelectedValue);
                        string Comments = Convert.ToString(txtComments.Text);
                        if (Id == 1)
                        {
                            if (txtDistance.Text != "")
                                Distance = Convert.ToInt32(txtDistance.Text);
                            RcvdQty = Convert.ToDecimal(txtArrQty.Text);
                            Url = "SRNStatus.aspx?ID=1";
                        }
                        else if (Id == 2)
                        {
                            if (lblDistance.Text != "")
                                Distance = Convert.ToInt32(lblDistance.Text);
                            RcvdQty = Convert.ToDecimal(txtRcvdQty.Text);
                            Url = "SRNStatus.aspx?ID=2";
                        }
                   
                        objSRN.MMS_UpdateSrnItems(SrnItemId, Id, RcvdQty, RecivedBy, Comments, Distance, EmpId);
                    }
                    else
                    {
                        AlertMsg.MsgBox(Page,"Received Quantity not more than SRN quantity ");
                        return;
                    }
                }
                SRNSDataSet.AcceptChanges();
                DataSet dsGDNS = new DataSet("SRNDataSet");
                DataTable dt = new DataTable("SRNTable");
                dt.Columns.Add(new DataColumn("SRNId", typeof(System.Int32)));
                dt.Columns.Add(new DataColumn("WOId", typeof(System.Int32)));
                dsGDNS.Tables.Add(dt);
                if (Id == 2)
                {
                    DataSet dsSRN = objSRN.MMS_GetGdnsFromXML(SRNSDataSet);
                    int c = 0;
                    foreach (DataRow drnew in dsSRN.Tables[0].Rows)
                    {
                        drnew["SRnId"] = dsSRN.Tables[0].Rows[c]["SRNID"].ToString();
                        int WOTypeId = 0, WOId = 0;
                        DataSet ds1 = new DataSet();
                        SqlParameter[] p = new SqlParameter[1];
                        p[0] = new SqlParameter("@srnId", (int)drnew["SRnId"]);
                        ds1 = SQLDBUtil.ExecuteDataset("MMS_GetServiceWOId", p);
                        if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
                        {
                            WOTypeId = Convert.ToInt32(ds1.Tables[0].Rows[0]["TypeId"]);
                            WOId = Convert.ToInt32(ds1.Tables[0].Rows[0]["POId"]);
                            if (WOTypeId == 1)
                            {
                                DataRow dr = dt.NewRow();
                                dr["SRNId"] = (int)drnew["SRnId"];
                                dr["WOId"] = WOId;
                                dt.Rows.Add(dr);
                                dsGDNS.AcceptChanges();
                                SqlParameter[] par = new SqlParameter[1];
                                par[0] = new SqlParameter("@SRNIDs", dsGDNS.GetXml());
                                SQLDBUtil.ExecuteNonQuery("MMS_ServiceInstantBillingByXML", par);
                            }
                            ds1.Clear();
                        }
                        c = c + 1;
                    }
                }


                DataSet ds = SRNService.MMS_GETSRNITEMID(Convert.ToInt32(ViewState["srnItemId"]));
                int TypeId = 0; string PONo = "";
                if (ds != null && ds.Tables.Count > 0)
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        int srnItemId = Convert.ToInt32(dr["SRNItemId"]);
                        int PoDetId = Convert.ToInt32(dr["PodetID"]);
                        string Itemid = dr["ResourceID"].ToString();
                        int RcvdQty = Convert.ToInt32(dr["RcvdQty"]);
                        int Qty = Convert.ToInt32(dr["Qty"]);

                        if (RcvdQty == Qty)
                        {
                            DataSet dstype = new DataSet();
                            dstype = QA.MMS_GetPOTypeId(PoDetId);
                            if (dstype != null && dstype.Tables.Count > 0)
                            {

                                TypeId = Convert.ToInt32(dstype.Tables[0].Rows[0]["TYPEID"]);
                                PONo = dstype.Tables[0].Rows[0]["PONO"].ToString();
                            }
                            if (TypeId == 1)
                            {


                                string strScript = "<script language='javascript' type='text/javascript'>   ClosePo(" + PONo + "," + Itemid + "); </script>";
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "PopupCP", strScript, false);


                            }
                            else
                                Response.Redirect("SRNStatus.aspx?ID=3&SRNID=" + SrnId);
                        }
                        else
                            Response.Redirect("SRNStatus.aspx?ID=3&SRNID=" + SrnId);
                    }
            }

                

            catch (Exception ex)
            {
                if (Id == 1)
                {
                    Response.Redirect("SRNStatus.aspx?ID=2&SRNID=" + SrnId);
                }

            }



            

        }
        
        protected void gvRecieve_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblDistance = (Label)e.Row.FindControl("lbldistance");// Cells[5].Controls[1];
                TextBox txtDistance = (TextBox)e.Row.FindControl("txtKMs");// Cells[5].Controls[2];
                Label lblRcvdQty = (Label)e.Row.FindControl("lblArvdQty");
                TextBox txtArrivedQuantity = (TextBox)e.Row.FindControl("txtArrivedQuantity");
                try
                {
                    switch (Id)
                    {
                        case 1:
                            lblRcvdQty.Visible = false;
                            txtArrivedQuantity.Visible = true;
                            txtDistance.Visible = true;

                            lblDistance.Visible = false;
                            break;
                        case 2:
                            lblRcvdQty.Visible = true;
                            txtArrivedQuantity.Visible = false;
                            lblDistance.Visible = true;

                            txtDistance.Visible = false;
                            break;
                        case 3:
                            lblRcvdQty.Visible = true;
                            txtArrivedQuantity.Visible = false;
                            lblDistance.Visible = true;
                            txtDistance.Visible = false;

                            //lnk.Text = "Completed";
                            //lnk.CommandName = "Completed";
                            break;
                        default:
                            break;
                    }
                }
                catch
                {
                }
            }

        }
        [Ajax.AjaxMethod()]
        public string MMS_CLOSEPO(string PONO, string Itemid)
        {
            QA.MMS_CLOSEPO(PONO, Itemid);
            return "";

        }
        //void AlertMsg.MsgBox(Page,string alert)
        //{
        //    string strScript = "<script language='javascript'>alert(\'" + alert + "\');</script>";
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "PopupCP", strScript, false);
        //}
    }
}
