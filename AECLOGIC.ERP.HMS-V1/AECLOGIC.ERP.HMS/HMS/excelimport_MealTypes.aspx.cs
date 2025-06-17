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
using System.Drawing;

namespace AECLOGIC.ERP.HMS
{
    public partial class excelimport_MealTypes : AECLOGIC.ERP.COMMON.WebFormMaster
    {


        Common1 obj = new Common1();
        int mid = 0; string menuname; string menuid;
        Common objCommon = new Common();
        HRCommon objHrCommon = new HRCommon();
        protected override void OnInit(EventArgs e)
        {
            ModuleID = 1;
            base.OnInit(e);
        }
        protected void Page_Load(object sender, EventArgs e)
        {


            if (!IsPostBack)
            {

                btnSave.Visible = false;
                pnlcolorshow.Visible = false;
            }

        }

        protected void btnImport_Click(object sender, EventArgs e)
        {
            gvMapping.DataSource = null;
            gvMapping.DataBind();
            gvMapping.Visible = false;
            btnSave.Visible = false;
            pnlcolorshow.Visible = false;
            lblTSError.Visible = false;
            lnkTSError.Visible = false;
            //DVMAP.Visible = false;
            try
            {
                if (fileupload.PostedFile.FileName != null && fileupload.PostedFile.FileName != string.Empty)
                {
                    string FileName = fileupload.PostedFile.FileName;
                    string UploadFileName = Server.MapPath("reports/" + DateTime.Now.ToFileTime().ToString() + System.IO.Path.GetExtension(FileName));
                    fileupload.SaveAs(UploadFileName);
                    String strConn = String.Format(System.Configuration.ConfigurationManager.AppSettings["ExcelConnString"], UploadFileName);

                    OleDbConnection OlConn = new OleDbConnection(strConn);
                    OlConn.Open();

                    DataTable sheetTable = OlConn.GetSchema("Tables");
                    DataRow rowSheetName = sheetTable.Rows[0];
                    String sheetName = rowSheetName[2].ToString();
                    OlConn.Close();
                    hdFileName.Value = strConn;
                    hdFile.Value = UploadFileName;
                    DataSet ds = null;                     
                    OleDbDataAdapter da = new OleDbDataAdapter("SELECT TOP 15 * FROM [" + sheetName + "]", strConn);
                    da.Fill(ds);
                    try
                    {
                        gvMapping.DataSource = ds.Tables[0].DefaultView;
                        gvMapping.DataBind();
                    }
                    catch (Exception rx)
                    {
                        AlertMsg.MsgBox(Page, "Sorry! Uploaded sheet was not in proper format, pls download a sample format");
                    }

                    pnlcolorshow.Visible = false;
                    if (gvMapping.Rows.Count > 0)
                    {
                        btnSave.Visible = true;
                        btnSave.Enabled = true;
                        gvMapping.Visible = true;
                    }
                    else
                        btnSave.Visible = false;

                    FileInfo fi = new FileInfo(hdFile.Value);
                    if (fi.Exists)
                        fi.Delete();
                }
            }
            catch (Exception ex) { AlertMsg.MsgBox(Page, ex.ToString()); }
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
        protected void allcheck(object sender, EventArgs e)
        {
            CheckBox chkall = (CheckBox)gvMapping.HeaderRow.FindControl("chkall");
            foreach (GridViewRow row in gvMapping.Rows)
            {
                CheckBox chkitem = (CheckBox)row.FindControl("chkitem");
                if (chkall.Checked == true)
                    chkitem.Checked = true;
                else
                    chkitem.Checked = false;
            }
        }
        protected void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("MealTypes", typeof(System.String));
                dt.Columns.Add("Short Name", typeof(System.String));
                dt.AcceptChanges();
                //MMSIDataContext dc = new MMSIDataContext();
                int stats = 1;
                foreach (GridViewRow row in gvMapping.Rows)
                {
                    CheckBox chkitem = (CheckBox)row.FindControl("chkitem");
                    if (chkitem.Checked == true)
                    {
                        string dept = Convert.ToString(((Label)row.FindControl("lblparti")).Text);
                        string shortnm = Convert.ToString(((Label)row.FindControl("lblshort")).Text);
                        int Output = AttendanceDAC.InsUpdateTypeofMess(0, dept, shortnm, stats, Convert.ToInt32( Convert.ToInt32(Session["UserId"]).ToString()));
                        if (Output == 1)
                            row.BackColor = Color.FromName("LightGreen");// AlertMsg.MsgBox(Page, "Inserted Sucessfully");
                        else if (Output == 2)
                        {
                            row.BackColor = Color.FromName("LightSalmon");// AlertMsg.MsgBox(Page, "Already Exists");
                            DataRow dr = dt.NewRow();
                            dr["MealTypes"] = dept;
                            dr["Short Name"] = shortnm;
                            dt.Rows.Add(dr);
                            dt.AcceptChanges();
                        }
                        else
                            row.BackColor = Color.FromName("Chartreuse");/// AlertMsg.MsgBox(Page, "Updated Sucessfully");

                    }
                }
                ViewState["dtdup"] = dt;
                pnlcolorshow.Visible = true;
                AlertMsg.MsgBox(Page, "Operation completed successfully!");
                btnSave.Enabled = false;
                lblTSError.Visible = true;
                lnkTSError.Visible = true;


            }
            catch (Exception ex)
            {
                AlertMsg.MsgBox(Page,ex.Message.ToString(),AlertMsg.MessageType.Error);
                //gvMapping.Visible = false;
                //btnSave.Visible = false;
                //Response.Redirect("./Importexcel.aspx");
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
        public DataSet GetDuplicateTS(string TS)
        {
            SqlParameter[] sqlParams = new SqlParameter[2];
            sqlParams[0] = new SqlParameter("@TS", TS);
            sqlParams[1] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
            sqlParams[1].Direction = ParameterDirection.ReturnValue;
             
          DataSet  ds = SQLDBUtil.ExecuteDataset("MMS_CheckTSExcel", sqlParams);
            return ds;


        }
        protected void lnkError_Click(object sender, EventArgs e)
        {
            DataTable dtError = (DataTable)ViewState["dtError"];
            Export_Click(dtError);
            lnkError.Visible = false;
            lblError.Visible = false;
        }
        protected void lnkTSError_Click(object sender, EventArgs e)
        {
            DataTable dtMultiTS = (DataTable)ViewState["dtdup"];
            Export_Click(dtMultiTS);
            lnkTSError.Visible = false;
            lblTSError.Visible = false;

        }
        protected void btnback_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Importexcel.aspx");
            btnback.Visible = false;
            lnkTSError.Visible = false;
            lblTSError.Visible = false;
            lnkError.Visible = false;
            lblError.Visible = false;
        }
    }
}
