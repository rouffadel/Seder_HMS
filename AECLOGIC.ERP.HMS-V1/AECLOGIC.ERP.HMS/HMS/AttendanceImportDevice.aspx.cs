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
    public partial class AttendanceImportDevice : AECLOGIC.ERP.COMMON.WebFormMaster
    {
        bool Editable;
        protected override void OnInit(EventArgs e)
        {
            ModuleID = 1;
            base.OnInit(e);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetParentMenuId();
            }
        }
        public int GetParentMenuId()
        {
            string URL = Request.Url.Segments[Request.Url.Segments.Length - 1] + Request.Url.Query;
            int RoleId = Convert.ToInt32(Session["RoleId"].ToString());
            int ModuleId = ModuleID;;
            DataSet ds = AttendanceDAC.GetAllowed(RoleId, ModuleId, URL);
            int MenuId = 0;
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                MenuId = Convert.ToInt32(ds.Tables[0].Rows[0]["Under"]);
                Button1.Enabled = Editable;

            }
            return MenuId;
        }
        protected void btnImport_Click(object sender, EventArgs e)
        {
            string FileName = fileupload.PostedFile.FileName;
            if (FileName != string.Empty)
            {
                string UploadFileName = Server.MapPath(".\\Reports\\" + "Device" + ".txt");// System.IO.Path.GetExtension(FileName));
                fileupload.SaveAs(UploadFileName);
                DataTable Dt = new DataTable("AttendanceTable");
                DataSet dsAttendance = new DataSet("AttendanceDataSet");
                Dt = ConvertTextDataToDataTabla(UploadFileName);
                Dt.TableName = "AttendanceTable";
                dsAttendance.Tables.Add(Dt);
                DataSet ds = AttendanceDAC.HMS_InsUpdateAttendanceXMLFromDevice(dsAttendance,  Convert.ToInt32(Session["UserId"]));

                AlertMsg.MsgBox(Page,"Done");

            }
            else
            {
                AlertMsg.MsgBox(Page,"Plz Browse the File");

            }
        }
        //void AlertMsg.MsgBox(Page,string alert)
        //{
        //    string strScript = "<script language='javascript'>alert(\'" + alert + "\');</script>";
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "PopupCP", strScript, false);
        //}
        DataTable ConvertTextDataToDataTabla(string Path)
        {

            DateTime DefaultOutTime;
            string MM = "00";
            if (txtMinutes.Text.Trim() != string.Empty)
            {
                MM = txtMinutes.Text.Trim();
            }
            DefaultOutTime = Convert.ToDateTime(ddlstarttime.SelectedItem.Value + ":" + MM + ddlTimeFormat.SelectedItem.Text);

            Dictionary<string, EmpAttendance> EmpAttList = new Dictionary<string, EmpAttendance>();
            DataTable dt = new DataTable();
            DataColumn dc = new DataColumn("EmpID", typeof(System.Int32));
            dt.Columns.Add(dc);
            dc = new DataColumn("Date", typeof(System.String));
            dt.Columns.Add(dc);
            dc = new DataColumn("InTime", typeof(System.String));
            dt.Columns.Add(dc);
            dc = new DataColumn("OutTime", typeof(System.String));
            dt.Columns.Add(dc);


            using (System.IO.StreamReader myFile =
                new System.IO.StreamReader(Path))
            {
                string myString = myFile.ReadToEnd();
                string[] lines = myString.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None);
                //string[] mystringLine = myString. Split("\r\n");
                foreach (string line in lines)
                {
                    try
                    {
                        string[] Words = line.Split(new string[] { "\t" }, StringSplitOptions.None);
                        DataRow drNew = null;

                        string[] DTime = Words[1].Split(new string[] { " " }, StringSplitOptions.None);

                        if (!EmpAttList.ContainsKey(Words[0].ToString().Trim() + '_' + DTime[0].ToString()))
                        {
                            EmpAttList.Add(Words[0].ToString().Trim() + '_' + DTime[0].ToString(), new EmpAttendance(Convert.ToInt32(Words[0]), Convert.ToDateTime(DTime[0].ToString()), Convert.ToDateTime(Words[1].ToString()), Convert.ToDateTime(Words[1].ToString())));
                            drNew = dt.NewRow();
                            drNew["EmpID"] = Words[0].ToString();
                            drNew["Date"] = Convert.ToDateTime(DTime[0]).ToString("dd MMM yyyy h:mtt");
                            drNew["InTime"] = Convert.ToDateTime(Words[1]).ToString("dd MMM yyyy h:mtt");

                            String X = Convert.ToString(DefaultOutTime.ToString("hh:mm tt"));
                            DefaultOutTime = Convert.ToDateTime(DTime[0].ToString() + ' ' + X);

                            drNew["OutTime"] = Convert.ToDateTime(DefaultOutTime).ToString("dd MMM yyyy h:mtt");
                        }
                        else
                        {
                            EmpAttendance objEMPAtt = EmpAttList[(Words[0].ToString().Trim() + '_' + DTime[0].ToString())];

                            String X = Convert.ToString(DefaultOutTime.ToString("hh:mm tt"));

                            DefaultOutTime = Convert.ToDateTime(DTime[0].ToString() + ' ' + X);

                            objEMPAtt.SetTime(Convert.ToDateTime(Words[1].ToString()), DefaultOutTime);

                            DataRow[] drSelect = dt.Select("EmpID=" + objEMPAtt.EmpID + " and Date='" + Convert.ToDateTime(objEMPAtt.Date).ToString("dd MMM yyyy h:mtt") + "'");
                            drSelect[0]["Intime"] = Convert.ToDateTime(objEMPAtt.InTime).ToString("dd MMM yyyy h:mtt");
                            drSelect[0]["OutTime"] = Convert.ToDateTime(objEMPAtt.OutTime).ToString("dd MMM yyyy h:mtt");

                            dt.AcceptChanges();
                        }

                        dt.Rows.Add(drNew);
                    }
                    catch (Exception ex)
                    {


                    }
                }
            }

            return dt;


        }

    }

    public class EmpAttendance
    {
        public EmpAttendance(int empID, DateTime date, DateTime inTime, DateTime outTime)
        {
            EmpID = empID;
            Date = date;
            InTime = inTime;
            OutTime = outTime;
        }

        private int _EmpID;

        public int EmpID
        {
            get { return _EmpID; }
            set { _EmpID = value; }
        }

        private DateTime? _InTime;

        public DateTime? InTime
        {
            get { return _InTime; }
            set { _InTime = value; }
        }

        private DateTime? _OutTime;

        public DateTime? OutTime
        {
            get { return _OutTime; }
            set { _OutTime = value; }
        }

        private DateTime? _Date;

        public DateTime? Date
        {
            get { return _Date; }
            set { _Date = value; }
        }

        public void SetTime(DateTime time, DateTime DefOutTime)
        {
            if (InTime > time)
                InTime = time;

            if (DefOutTime > time)
            {
                OutTime = DefOutTime;
            }
            else
            {
                OutTime = time;
            }

        }

    }
}