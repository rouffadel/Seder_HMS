using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.IO;
using DataAccessLayer;
using Aeclogic.Common.DAL;
using AECLOGIC.HMS.BLL;
//using CompanyDac;

namespace AECLOGIC.ERP.HMS
{
    public partial class ServicesImportExcel : AECLOGIC.ERP.COMMON.WebFormMaster
    {

        Dictionary<string, int> dcVal;

        //Common obj = new Common();
        int mid = 0; bool viewall; string menuname; string menuid;
        SRNService objSRN = new SRNService();

        protected void Page_Load(object sender, EventArgs e)
        {


            if (!IsPostBack)
            {
                SetUpScreen();
                btnMapExcel.Attributes.Add("onclick", "javascript:return ValidateSave();");
            }

        }
        protected override void OnInit(EventArgs e)
        {
            ModuleID = 1;
            base.OnInit(e);
        }
        protected void btnImport_Click(object sender, EventArgs e)
        {
            gvMapping.DataSource = null;
            gvMapping.DataBind();

            dvTUQD.Visible = true;
            DVMAP.Visible = false;
            try
            {
                if (fileupload.PostedFile.FileName != null && fileupload.PostedFile.FileName != string.Empty)
                {
                    string FileName = fileupload.PostedFile.FileName;
                    string UploadFileName = Server.MapPath("reports/" + DateTime.Now.ToFileTime().ToString() + System.IO.Path.GetExtension(FileName));

                    fileupload.SaveAs(UploadFileName);
                    String strConn = "Provider=Microsoft.Jet.OLEDB.4.0;" + "Data Source='" + UploadFileName + "';" + "Extended Properties=Excel 8.0;";
                    OleDbConnection OlConn = new OleDbConnection(strConn);
                    try
                    {
                        OlConn.Open();
                    }
                    catch
                    {
                        strConn = "Provider=Microsoft.ACE.OLEDB.12.0;" + "Data Source='" + UploadFileName + "';" + "Extended Properties=Excel 12.0;";
                        OlConn.Open();
                    }
                    DataTable sheetTable = OlConn.GetSchema("Tables");
                    DataRow rowSheetName = sheetTable.Rows[0];
                    String sheetName = rowSheetName[2].ToString();
                    OlConn.Close();
                    hdFileName.Value = strConn;
                    hdFile.Value = UploadFileName;
                     
                    OleDbDataAdapter da = new OleDbDataAdapter("SELECT TOP 15 * FROM [" + sheetName + "]", strConn);
                    DataSet ds = null;
                    da.Fill(ds);
                    dcVal = new Dictionary<string, int>();
                    int j = 0;
                    for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
                    {
                        ds.Tables[0].Columns[i].ColumnName = "NewCol$" + i.ToString();
                    }
                    for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
                    {

                        if (i < 26)
                        {
                            ds.Tables[0].Columns[i].ColumnName = Convert.ToChar(i + 65).ToString();
                            dcVal.Add(ds.Tables[0].Columns[i].ColumnName, i);
                        }
                        else
                        {
                            if ((i - ((j + 1) * 26)) > 25)
                            {
                                j++;
                            }
                            ds.Tables[0].Columns[i].ColumnName = Convert.ToChar(j + 65).ToString() + Convert.ToChar(i - ((j + 1) * 26) + 65).ToString();
                            dcVal.Add(ds.Tables[0].Columns[i].ColumnName, i);
                        }
                    }
                    ViewState["dcVal"] = dcVal;

                    GridViewExcel.DataSource = ds.Tables[0].DefaultView;
                    GridViewExcel.DataBind();

                    dvTUQD.Visible = true;
                    DVMAP.Visible = true;


                }
            }
            catch (Exception ex) { AlertMsg.MsgBox(Page,ex.Message.ToString(),AlertMsg.MessageType.Error); }
        }

        protected void Vendor_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillPODropDown();
            ddlDestRepre.Items.Clear();
          

             
            SqlParameter[] p = new SqlParameter[1];
            p[0] = new SqlParameter("@Site_ID", int.Parse(ddlWorkSite.SelectedValue));
            DataSet ds = SqlHelper.ExecuteDataset("MMS_DDL_EmployeeExecutive", p);
            ddlDestRepre.DataValueField = ds.Tables[0].Columns[0].ToString();
            ddlDestRepre.DataTextField = ds.Tables[0].Columns[1].ToString(); ddlDestRepre.Items.Add(new ListItem("Select Destination Representative", "-1"));
            ddlDestRepre.DataSource = ds;// LookUp.PopulateEntityDropDown(LookUp.EntityRelationships.Executive, int.Parse(ddlWorkSite.SelectedValue));
            ddlDestRepre.DataBind();
        }

        protected void btnMapExcel_Click(object sender, EventArgs e)
        {
            try
            {
               

                OleDbConnection OlConn = new OleDbConnection(hdFileName.Value);
                OlConn.Open();
                DataTable sheetTable = OlConn.GetSchema("Tables");
                DataRow rowSheetName = sheetTable.Rows[0];
                String sheetName = rowSheetName[2].ToString();
                OlConn.Close();

                DataSet dsTasks = new DataSet();
                OleDbDataAdapter da = new OleDbDataAdapter("SELECT * FROM [" + sheetName + "]", hdFileName.Value);

                da.Fill(dsTasks);
                dcVal = (Dictionary<string, int>)ViewState["dcVal"];
                string Units = string.Empty;
                string Rate = string.Empty;
                string Task = string.Empty;

                DataTable dt = new DataTable();
                dt.Columns.Add("Date", typeof(System.DateTime));
                dt.Columns.Add("Vendor", typeof(System.String));
                dt.Columns.Add("VendorID", typeof(System.String));
                dt.Columns.Add("WorkSite", typeof(System.String));
                dt.Columns.Add("WorkSiteID", typeof(System.String));
                dt.Columns.Add("ResourceID", typeof(System.String));
                dt.Columns.Add("Vehicle", typeof(System.String));
                dt.Columns.Add("PONO", typeof(System.Int32));
                dt.Columns.Add("TripSheet", typeof(System.String));
                dt.Columns.Add("RoyaltyChalanaNO", typeof(System.Int32));
                dt.Columns.Add("Qty", typeof(System.String));
                dt.Columns.Add("RepAtDest", typeof(System.String));
                dt.Columns.Add("RepAtOrigin", typeof(System.String));
                dt.Columns.Add("Material", typeof(System.String));
                dt.Columns.Add("OriginID", typeof(System.String));
                dt.Columns.Add("DestID", typeof(System.String));
                dt.Columns.Add("SrNo", typeof(System.Int32));

                DataTable dtError = new DataTable();
                ViewState["dtError"] = dtError;
                dtError.Columns.Add("Date");
                dtError.Columns.Add("Vehicle");
                dtError.Columns.Add("Vendor");
                dtError.Columns.Add("WorkSite");
                dtError.Columns.Add("PONO");
                dtError.Columns.Add("TripSheet");
                dtError.Columns.Add("RoyaltyChalanaNO");
                dtError.Columns.Add("Qty");
                dtError.Columns.Add("RepAtDest");
                dtError.Columns.Add("RepAtOrigin");
                dtError.Columns.Add("Material");
                dtError.Columns.Add("SrNo", typeof(System.Int32));
                int i = 0;
                int Error = 0;
                foreach (DataRow drTasks in dsTasks.Tables[0].Rows)
                {
                    i++;
                    Error++;
                    DateTime Date = new DateTime();
                    DateTime Date1 = new DateTime();
                    try
                    {
                        DataRow dr = null;
                        Date = (DateTime)drTasks[dcVal[txtDate.Text.Trim().ToUpper()]];


                        dr = dt.NewRow();
                        dr["SrNo"] = i;
                        dr["Date"] = Date;

                        dr["Vendor"] = ddlVendor.SelectedItem.Text;
                        dr["VendorID"] = ddlVendor.SelectedValue;
                        dr["PONO"] = ddlPONO.SelectedValue;                         //modified
                        dr["WorkSite"] = ddlWorkSite.SelectedItem.Text;
                        dr["WorkSiteID"] = ddlWorkSite.SelectedValue;
                        dr["Material"] = ddlResourcetype.SelectedItem.Text;
                        dr["ResourceID"] = ddlResourcetype.SelectedValue;
                        dr["RepAtDest"] = ddlDestRepre.SelectedValue;
                        dr["RepAtOrigin"] = ddlOriRepre.SelectedValue;
                        if (drTasks[dcVal[txtVehicleNo.Text.Trim().ToUpper()]].ToString() == "")
                            throw new Exception("");
                        else
                            dr["Vehicle"] = drTasks[dcVal[txtVehicleNo.Text.Trim().ToUpper()]];
                        if (drTasks[dcVal[txtQty.Text.Trim().ToUpper()]].ToString() == "")
                            throw new Exception("");
                        else
                            dr["Qty"] = drTasks[dcVal[txtQty.Text.Trim().ToUpper()]];
                        dr["TripSheet"] = drTasks[dcVal[txtTripSheet.Text.Trim().ToUpper()]];
                        dr["RoyaltyChalanaNO"] = drTasks[dcVal[txtroyaltychalana.Text.Trim().ToUpper()]];
                        dr["OriginID"] = ddlOrigin.SelectedValue;
                        dr["DestID"] = ddlDestination.SelectedValue;
                        dt.Rows.Add(dr);
                    }
                    catch
                    {
                        if (drTasks[dcVal[txtVehicleNo.Text.Trim().ToUpper()]].ToString().Trim() != "" &&
                            drTasks[dcVal[txtQty.Text.Trim().ToUpper()]].ToString().Trim() != "" &&
                            drTasks[dcVal[txtTripSheet.Text.Trim().ToUpper()]].ToString().Trim() != "" &&
                            drTasks[dcVal[txtroyaltychalana.Text.Trim().ToUpper()]].ToString().Trim() != "")
                        {
                            DataRow dr = null;
                            dr = dtError.NewRow();
                            dr["SrNo"] = Error;
                            dr["Date"] = Date;
                            dr["Vendor"] = ddlVendor.SelectedItem.Text;
                            dr["PONO"] = ddlPONO.SelectedItem.Text;                 //modified
                            dr["WorkSite"] = ddlWorkSite.SelectedItem.Text;
                            dr["Vehicle"] = drTasks[dcVal[txtVehicleNo.Text.Trim().ToUpper()]];
                            dr["Qty"] = drTasks[dcVal[txtQty.Text.Trim().ToUpper()]];
                            dr["TripSheet"] = drTasks[dcVal[txtTripSheet.Text.Trim().ToUpper()]];
                            dr["RoyaltyChalanaNO"] = drTasks[dcVal[txtroyaltychalana.Text.Trim().ToUpper()]];
                            //dr["RepAtDest"] = drTasks[dcVal[txtSupplyer.Text.Trim().ToUpper()]];
                            dr["Material"] = ddlResourcetype.SelectedItem.Text;
                            dtError.Rows.Add(dr);
                        }
                    }
                }

                GridViewExcel.DataSource = null;
                GridViewExcel.DataBind();

                gvMapping.Visible = true;
                gvMapping.DataSource = dt;
                gvMapping.DataBind();
                ViewState["dtError"] = dtError;

                dvTUQD.Visible = false;
                DVMAP.Visible = false;
                FileInfo fi = new FileInfo(hdFile.Value);
                if (fi.Exists)
                    fi.Delete();
                btnSave.Visible = true;
            }
            catch (Exception ex) { AlertMsg.MsgBox(Page,ex.Message.ToString(),AlertMsg.MessageType.Error); }
        }

        void Export_Click(DataTable dt)
        {
            string attachment = "attachment; filename=Error.xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/vnd.ms-excel";

            string tab = "";

            foreach (DataColumn dc in dt.Columns)
            {
                Response.Write(tab + dc.ColumnName);
                tab = "\t";
            }
            Response.Write("\n");

            int i;
            foreach (DataRow dr in dt.Rows)
            {
                tab = "";
                for (i = 0; i < dt.Columns.Count; i++)
                {
                    Response.Write(tab + dr[i].ToString());
                    tab = "\t";
                }
                Response.Write("\n");
            }
            Response.End();
        }

        //void AlertMsg.MsgBox(Page,string alert)
        //{
        //    string strScript = "<script language='javascript'>alert(\'" + alert + "\');</script>";
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "PopupCP", strScript, false);
        //}

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {

                int ModuleID = Convert.ToInt32(Application["ModuleId"]); ;
                foreach (GridViewRow row in gvMapping.Rows)
                {
                    int PONO;

                    int VendorID = Convert.ToInt32(((Label)row.FindControl("VendorID")).Text);
                    PONO = Convert.ToInt32(((Label)row.FindControl("PONo")).Text);   //modified
                    int WorkSiteID = Convert.ToInt32(((Label)row.FindControl("WorkSiteID")).Text);
                    int OriginID = Convert.ToInt32(((Label)row.FindControl("OriginID")).Text);
                    int DestID = Convert.ToInt32(((Label)row.FindControl("DestID")).Text);
                    int RepAtDest = Convert.ToInt32(((Label)row.FindControl("RepAtDest")).Text);
                    int RepAtOrigin = Convert.ToInt32(((Label)row.FindControl("RepAtOrigin")).Text);
                    int Material = Convert.ToInt32(((Label)row.FindControl("ResourceID")).Text);
                    DateTime dt = CODEUtility.ConvertToDate(row.Cells[1].Text, DateFormat.DayMonthYear);
                    SqlParameter[] Parms = new SqlParameter[13];
                    Parms[0] = new SqlParameter("@VendorID", VendorID);
                    Parms[1] = new SqlParameter("@OriginID", OriginID);
                    Parms[2] = new SqlParameter("@DestID", DestID);
                    Parms[3] = new SqlParameter("@RepAtDest", RepAtDest);
                    Parms[13] = new SqlParameter("@OriginRepr", RepAtOrigin);
                    Parms[4] = new SqlParameter("@DATE", dt);
                    Parms[5] = new SqlParameter("@WorkSiteID", WorkSiteID);
                    Parms[6] = new SqlParameter("@UserId", Convert.ToInt32(Session["LoginId"].ToString()));
                    string Vehicle = VehicelConvert(row.Cells[4].Text);
                    Parms[7] = new SqlParameter("@VehRegnNr", Vehicle);
                    Parms[8] = new SqlParameter("@Material", Material);
                    Parms[9] = new SqlParameter("@PONO", PONO);
                    Parms[10] = new SqlParameter("@Qty", row.Cells[5].Text);
                    Parms[11] = new SqlParameter("@TripSheet", row.Cells[6].Text.Replace("&nbsp;", ""));
                    SqlHelper.ExecuteNonQuery("MMS_IMPORTSRNFROMEXCEL", Parms);

                }
                AlertMsg.MsgBox(Page,"Data Inserted Succesfully");
               
                DataTable dtError = (DataTable)ViewState["dtError"];
                if (dtError.Rows.Count > 0)
                    Export_Click(dtError);
                Response.Redirect("ServicesImportExcel.aspx");
            }
            catch (Exception ex)
            {
                AlertMsg.MsgBox(Page,ex.Message.ToString(),AlertMsg.MessageType.Error);
                gvMapping.Visible = false;
                btnSave.Visible = false;
            }
        }

        private string VehicelConvert(string RegNo)
        {
            Char[] ch = RegNo.Trim().ToCharArray();
            string St2 = "", St1 = "";
            Boolean isflg = true;
            for (int i = ch.Length - 1; i >= 0; i--)
            {
                Char c = ch[i];
                if (isflg)
                {
                    if (Char.IsNumber(c))
                    {
                        St1 = c.ToString() + St1;
                    }
                    else
                    {
                        St2 = c.ToString() + St2;
                        isflg = false;
                    }
                }
                else
                {
                    St2 = c.ToString() + St2;
                }
            }
            return (St1 + ", " + St2).ToString();
        }

       

        #region SetUpScreen

        private void SetUpScreen()
        {

            FillVendorDropDown();
            FillWorkSiteDropDown();
            FillOriginDropDown();
            FillDestinationDropDown();
            FillDestinationRepresentativeDropDown();
            FillResourceTypeDropDown();
            FillOriginRepresentativeDropDown();
        }

        #endregion SetUpScreen

        #region DropDown

        private void FillPODropDown()
        {
            ddlPONO.Items.Clear();
            DataSet ds = objSRN.GetMMS_MIS_lstWODetails(int.Parse(ddlVendor.SelectedValue), int.Parse(ddlWorkSite.SelectedValue), 1, Convert.ToInt32(Application["ModuleId"]));
            if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0)
            {
                ddlPONO.DataValueField = "ID";
                ddlPONO.DataTextField = "Name";
                ddlPONO.DataSource = ds;
                ddlPONO.DataBind();
            }
            else
            {
                ddlPONO.Items.Add(new ListItem("No WOrk Order Orders Found", "-1"));
            }
        }



        private void FillVendorDropDown()
        {
             
          DataSet  ds = SqlHelper.ExecuteDataset("MMS_DDL_VendorDetails");
            ddlVendor.DataValueField = ds.Tables[0].Columns[1].ToString();
            ddlVendor.DataTextField = ds.Tables[0].Columns[0].ToString();
            ddlVendor.Items.Add(new ListItem("Select Vendor ", "-1"));
            ddlVendor.DataSource = ds;// LookUp.PopulateEntityDropDown(LookUp.EntityRelationships.VendorDetails);
            ddlVendor.DataBind();
        }

        private void FillWorkSiteDropDown()
        {
            ddlWorkSite.Items.Add(new ListItem(" Select WorkSite", "-1"));
            FIllObject.FillDropDown(ref ddlWorkSite, "MMS_DDL_WorkSite");

        }

        private void FillOriginDropDown()
        {


            DataSet ds = SqlHelper.ExecuteDataset("MMS_DDL_Locations");

            ddlOrigin.DataValueField = ds.Tables[0].Columns[0].ToString();
            ddlOrigin.DataTextField = ds.Tables[0].Columns[1].ToString();
            ddlOrigin.DataSource = ds; ddlOrigin.Items.Add(new ListItem("Select Origin", "-1"));
            ddlOrigin.DataBind();
        }

        private void FillDestinationRepresentativeDropDown()
        {

            DataSet ds = SqlHelper.ExecuteDataset("MMS_DDL_EmployeeMaster");
            ddlDestRepre.DataValueField = ds.Tables[0].Columns[1].ToString();
            ddlDestRepre.DataTextField = ds.Tables[0].Columns[0].ToString(); ddlDestRepre.Items.Add(new ListItem("Select Destination Representative     ", "-1"));
            ddlDestRepre.DataSource = ds;
            ddlDestRepre.DataBind();
        }

        private void FillOriginRepresentativeDropDown()
        {

            DataSet ds = SqlHelper.ExecuteDataset("MMS_DDL_EmployeeMaster");
            ddlOriRepre.DataValueField = ds.Tables[0].Columns[1].ToString();
            ddlOriRepre.DataTextField = ds.Tables[0].Columns[0].ToString(); ddlOriRepre.Items.Add(new ListItem("Select Origin Representative", "-1"));
            ddlOriRepre.DataSource = ds;
            ddlOriRepre.DataBind();
        }

        private void FillDestinationDropDown()
        {

            DataSet ds = SqlHelper.ExecuteDataset("MMS_DDL_Locations");
            ddlDestination.DataValueField = ds.Tables[0].Columns[0].ToString();
            ddlDestination.DataTextField = ds.Tables[0].Columns[1].ToString();
            ddlDestination.DataSource = ds;
            ddlDestination.Items.Add(new ListItem("Select Destination", "-1"));
            ddlDestination.DataBind();
        }

        private void FillResourceTypeDropDown()
        {

            DataSet ds = SqlHelper.ExecuteDataset("MMS_DDL_Resources");
            ddlResourcetype.DataValueField = ds.Tables[0].Columns[0].ToString();
            ddlResourcetype.DataTextField = ds.Tables[0].Columns[1].ToString();
            ddlResourcetype.DataSource = ds;
            ddlResourcetype.Items.Add(new ListItem("Select Material Type", "-1"));
            ddlResourcetype.DataBind();
        }

        #endregion DropDown

       

    
    }
}
