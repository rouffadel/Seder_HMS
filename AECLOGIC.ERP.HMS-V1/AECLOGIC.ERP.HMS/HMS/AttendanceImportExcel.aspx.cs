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
namespace AECLOGIC.ERP.HMS
{
    public partial class AttendanceImportExcel : AECLOGIC.ERP.COMMON.WebFormMaster
    {
        Dictionary<string, int> dcVal;
        protected override void OnInit(EventArgs e)
        {
            ModuleID = 1;
            base.OnInit(e);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ViewState["ImpType"] = 0;
                try
                {
                    ViewState["ImpType"] = Request.QueryString["TypeID"].ToString();
                }
                catch { }
            }
        }

        protected void btnImport_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(ViewState["ImpType"]) == 0)

                ExcelImportMethod();
            else if (Convert.ToInt32(ViewState["ImpType"]) == 1)
                NotePadImportMethod();
        }

        private void NotePadImportMethod()
        {
        }
        private void ExcelImportMethod()
        {
            gvMapping.DataSource = null;
            gvMapping.DataBind();

            dvTUQD.Visible = true;
            DVMAP.Visible = false;
            try
            {
                if (fileupload.PostedFile.FileName != null && fileupload.PostedFile.FileName != string.Empty)
                {
                    string filename = "", ext = "", path = "";

                    filename = fileupload.PostedFile.FileName;
                    ext = filename.Split('.')[filename.Split('.').Length - 1];
                    path = Server.MapPath(".\\Reports\\" + "Attendance" + "." + ext);
                    fileupload.PostedFile.SaveAs(path);

                    string UploadFileName = Server.MapPath(".\\Reports\\" + "Attendance" + "." + ext);
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
                    DataSet ds = new DataSet();
                    OleDbDataAdapter da = new OleDbDataAdapter("SELECT TOP 15 * FROM [" + sheetName + "]", strConn);

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

                    MapExcel();
                }
                else
                {
                    AlertMsg.MsgBox(Page,"Plz Browse the File");

                }
            }
            catch (Exception ex)
            {
                AlertMsg.MsgBox(Page,"Excel formate is not in the expected format. Plz Same data copy to new Sheet and Upload it");
            }
        }



        private void MapExcel()
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


                DataTable dt = new DataTable();
                dt.Columns.Add("SrNo", typeof(System.Int32));
                dt.Columns.Add("EmpID", typeof(System.Int32));
                dt.Columns.Add("EmpName", typeof(System.String));
                dt.Columns.Add("Date", typeof(System.DateTime));
                dt.Columns.Add("InTime", typeof(System.String));
                dt.Columns.Add("OutTime", typeof(System.String));
                dt.Columns.Add("Remarks", typeof(System.String));


                DataTable dtError = new DataTable();
                ViewState["dtError"] = dtError;

                dtError.Columns.Add("SrNo", typeof(System.Int32));
                dtError.Columns.Add("EmpID", typeof(System.Int32));
                dtError.Columns.Add("EmpName", typeof(System.String));
                dtError.Columns.Add("Date", typeof(System.DateTime));
                dtError.Columns.Add("InTime", typeof(System.String));
                dtError.Columns.Add("OutTime", typeof(System.String));
                dtError.Columns.Add("Remarks", typeof(System.String));

                int i = 0;
                int Error = 0;
                foreach (DataRow drTasks in dsTasks.Tables[0].Rows)
                {
                    i++;
                    Error++;
                    DateTime Date = new DateTime();

                    try
                    {
                        DataRow dr = null;
                        try
                        {
                            Date = Convert.ToDateTime(drTasks[dcVal[txtDate.Text.Trim().ToUpper()]]);
                        }
                        catch { };

                        dr = dt.NewRow();
                        dr["SrNo"] = i;
                        dr["EmpID"] = drTasks[dcVal[txtEmpID.Text.Trim().ToUpper()]];
                        dr["EmpName"] = drTasks[dcVal[txtEmpName.Text.Trim().ToUpper()]];
                        dr["Date"] = Date;
                        if (drTasks[dcVal[txtInTime.Text]].ToString() != "")
                        {
                            DateTime DtIntime = DateTime.Parse(drTasks[dcVal[txtInTime.Text.Trim().ToUpper()]].ToString());
                            dr["InTime"] = DtIntime.ToString("hh:mm");
                        }
                        else
                        {
                            dr["InTime"] = null;
                        }
                        if (drTasks[dcVal[txtOutTime.Text]].ToString() != "")
                        {
                            DateTime DtOutTime = DateTime.Parse(drTasks[dcVal[txtOutTime.Text.Trim().ToUpper()]].ToString());
                            dr["OutTime"] = DtOutTime.ToString("hh:mm");
                        }
                        else
                        {
                            dr["OutTime"] = null;
                        }
                        if (drTasks[dcVal[txtRemarks.Text]].ToString() != "")
                        {
                            dr["Remarks"] = drTasks[dcVal[txtRemarks.Text.Trim().ToUpper()]];
                        }
                        else
                        {
                            dr["Remarks"] = null;
                        }
                        dt.Rows.Add(dr);
                    }
                    catch
                    {
                        AlertMsg.MsgBox(Page,"Excel formate is not in the expected format. Plz Same data copy to new Sheet and Upload it");

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
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet dsAttendance = new DataSet("AttendanceDataSet");
                DataTable dt = new DataTable("AttendanceTable");

                dt.Columns.Add("EmpID", typeof(System.Int32));
                dt.Columns.Add("EmpName", typeof(System.String));
                dt.Columns.Add("Date", typeof(System.DateTime));
                dt.Columns.Add("InTime", typeof(System.String));
                dt.Columns.Add("OutTime", typeof(System.String));
                dt.Columns.Add("Remarks", typeof(System.String));

                dsAttendance.Tables.Add(dt);

                int EmpID = 0;
                foreach (GridViewRow gvRow in gvMapping.Rows)
                {
                    DataRow dr = dt.NewRow();
                    if (gvRow.Cells[1].Text != "&nbsp;")
                    {
                        dr["EmpID"] = Convert.ToInt32(gvRow.Cells[1].Text);

                        dr["EmpName"] = gvRow.Cells[2].Text;
                        if (gvRow.Cells[3].Text != "")
                        {
                            dr["Date"] = CODEUtility.ConvertToDate(gvRow.Cells[3].Text, DateFormat.DayMonthYear);
                        }
                        else
                        {
                            dr["Date"] = null;
                        }

                        if (gvRow.Cells[4].Text != "&nbsp;")
                        {
                            dr["InTime"] = gvRow.Cells[4].Text;
                        }
                        else
                        {
                            dr["InTime"] = null;
                        }

                        if (gvRow.Cells[5].Text != "&nbsp;")
                        {
                            dr["OutTime"] = gvRow.Cells[5].Text;
                        }
                        else
                        {
                            dr["OutTime"] = null;
                        }

                        if (gvRow.Cells[6].Text != "&nbsp;")
                        {
                            dr["Remarks"] = gvRow.Cells[6].Text;
                        }
                        else
                        {
                            dr["Remarks"] = null;
                        }


                        dt.Rows.Add(dr);
                    }



                }

                dsAttendance.AcceptChanges();
                DataSet ds = AttendanceDAC.HMS_InsUpdateAttendanceXML(dsAttendance,  Convert.ToInt32(Session["UserId"]));
            }
            catch (Exception)
            {
                AlertMsg.MsgBox(Page,"Some of the Excel cells are empty; plz remove and upload it");

            }

        }
    }
}