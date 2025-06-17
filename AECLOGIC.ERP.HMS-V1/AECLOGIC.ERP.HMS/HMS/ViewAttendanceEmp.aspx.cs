using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using AECLOGIC.HMS.BLL;
using System.Collections.Generic;
using AECLOGIC.ERP.COMMON;
using AECLOGIC.ERP.HMS.HRClasses;

namespace AECLOGIC.ERP.HMS.HMS
{
    public partial class ViewAttendanceEmp : AECLOGIC.ERP.COMMON.WebFormMaster
    {
        int mid = 0; int EMPIDPram = 0;
        bool viewall;
        string menuname;
        string menuid;
        static int CompanyID = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["CompanyID"].ToString());
        static int Role;
        static int EmpID;
        static int SiteSearch;
        static int status = 1;
        TableRow tblRow;
        TableCell tcName, tcDesig, tcDate, tcStatus, tcShift, tcEmpID, tcLnkbtn, tcScope, tcAbsent, tcOD, tcCL, tcEL, tcSL, tcLeavesApp,
            tcOBCL, tcOBEL, tcOBSL, tcCurCL, tcCurEL, tcCurSL, tcAdjLeaves, tcLOP, tcMarkedBy, tcUpdatedBy;
        static int ModID;
        static int Userid; int stmonth = 0; int edmonth = 0;

        AttendanceDAC objAtt = new AttendanceDAC();
        protected override void OnInit(EventArgs e)
        {
            ModuleID = 1;
            base.OnInit(e);

        }

        protected void Page_Load(object sender, EventArgs e)
        {
           
                try
                {
                    string id = Session["UserID"].ToString();
                }
                catch
                {
                    Response.Redirect("Logon.aspx");
                }
                if (Request.QueryString["ID"] != null && Request.QueryString["ID"] != string.Empty)
                {

                    ViewState["ID"] = Convert.ToInt32(Request.QueryString["ID"].ToString());

                }
                if (Request.QueryString["ddlYear"] != null && Request.QueryString["ddlYear"] != string.Empty)
                {
                    ViewState["ddlYear"] = Convert.ToInt32(Request.QueryString["ddlYear"].ToString());
                }
                if (Request.QueryString["ddlMonth"] != null && Request.QueryString["ddlMonth"] != string.Empty)
                {
                    ViewState["ddlMonth"] = Convert.ToInt32(Request.QueryString["ddlMonth"].ToString());
                }

                DataSet dstemp = new DataSet();
                topmenu.MenuId = GetParentMenuId();
                topmenu.ModuleId = ModuleID; ;
                topmenu.RoleID = Convert.ToInt32(Session["RoleId"].ToString());
                topmenu.SelectedMenu = Convert.ToInt32(mid.ToString());
                topmenu.DataBind();
                Session["menuname"] = menuname;
                Session["menuid"] = menuid;
                Userid = Convert.ToInt32(Session["UserId"].ToString());
                if (!IsPostBack)
                {
                   GetDayReport();
                   
                }
            
        }
    
        public int GetParentMenuId()
        {
            string URL = Request.Url.Segments[Request.Url.Segments.Length - 1] + Request.Url.Query;
            int RoleId = Convert.ToInt32(Session["RoleId"].ToString());
            int ModuleId = ModuleID; ;

            DataSet ds = new DataSet();

            ds = AttendanceDAC.GetAllowed(RoleId, ModuleId, URL);
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

        protected void GetDayReport()
        {
            DataSet ds = new DataSet();
            AttendanceDAC objAtt = new AttendanceDAC();
            TimeSpan Work;
            DateTime Intime = DateTime.Now, OutTime = DateTime.Now;
            string URL = Request.Url.Segments[Request.Url.Segments.Length - 1] + Request.Url.Query;
            int RoleId = Convert.ToInt32(Session["RoleId"].ToString());
            int ModuleId = ModuleID; 

            //ds = AttendanceDAC.GetAllowed(RoleId, ModuleId, URL);

            // int DayInterval = 1;
            int TotalPages = 1;//returnVale
            int NoofRecords = 100;// return value
            if ((Convert.ToBoolean(ds.Tables[0].Rows[0]["ViewAll"].ToString()) == false))
            {

                //ddlDepartment.Visible = false; ddlWorksite.Visible = false;
                int empid = Convert.ToInt32(Session["UserId"]);
               // ds = objAtt.GetAttendanceByDay_Cursor(Date, 0, 0, empid, string.Empty, Convert.ToInt32(Session["CompanyID"]), 1, 10, ref NoofRecords, ref TotalPages);
            }


            int count = ds.Tables.Count;
            if (ds != null && ds.Tables.Count > 0)
            {
                tblRow = new TableRow();

                tcName = new TableCell();
                tcName.Text = "Name";
                tcName.Style.Add("font-weight", "bold");
                tcName.Width = 200;
                tblRow.Cells.Add(tcName);

                tcStatus = new TableCell();
                tcStatus.Text = "Status";
                tcStatus.Style.Add("font-weight", "bold");
                tcStatus.Width = 120;
                tblRow.Cells.Add(tcStatus);

                tcDate = new TableCell();
                tcDate.Text = "Shift";
                tcDate.Style.Add("font-weight", "bold");
                tcDate.Width = 80;
                tblRow.Cells.Add(tcDate);

                tcDate = new TableCell();
                tcDate.Text = "In Time";
                tcDate.Style.Add("font-weight", "bold");
                tcDate.Width = 80;
                tblRow.Cells.Add(tcDate);

                tcDate = new TableCell();
                tcDate.Text = "Out Time";
                tcDate.Style.Add("font-weight", "bold");
                tcDate.Width = 80;
                tblRow.Cells.Add(tcDate);

                tcDate = new TableCell();
                tcDate.Text = "Total Work";
                tcDate.Style.Add("font-weight", "bold");
                tcDate.Width = 90;
                tblRow.Cells.Add(tcDate);

                tcStatus = new TableCell();
                tcStatus.Text = "Remarks";
                tcStatus.Style.Add("font-weight", "bold");
                tcStatus.Width = 150;
                tblRow.Cells.Add(tcStatus);

                tcMarkedBy = new TableCell();
                tcMarkedBy.Text = "Marked By";
                tcMarkedBy.Style.Add("font-weight", "bold");
                tcMarkedBy.Width = 50;

                tblRow.Cells.Add(tcMarkedBy);

                tcUpdatedBy = new TableCell();
                tcUpdatedBy.Text = "Updated By";
                tcUpdatedBy.Style.Add("font-weight", "bold");
                tcUpdatedBy.Width = 50;
                tblRow.Cells.Add(tcUpdatedBy);


                tblAtt.Rows.Add(tblRow);


                string Department = String.Empty;

                for (int j = 0; j < count; j++)
                {
                    tblRow = new TableRow();
                    tcName = new TableCell();
                    if (ds.Tables[j].Rows.Count != 0)
                    {
                        tcName.Text = ds.Tables[j].Rows[0][3].ToString();
                        tcName.Width = 200;
                        tblRow.Cells.Add(tcName);

                        tcStatus = new TableCell();
                        tcStatus.Text = ds.Tables[j].Rows[0][1].ToString();
                        tcStatus.Width = 80;
                        tblRow.Cells.Add(tcStatus);

                        tcShift = new TableCell();
                        try { tcShift.Text = ds.Tables[j].Rows[0][8].ToString(); }
                        catch { }
                        tcShift.Width = 80;
                        tblRow.Cells.Add(tcShift);

                        tcDate = new TableCell();
                        if (ds.Tables[j].Rows[0][4].ToString() != "")
                        {
                            tcDate.Text = ds.Tables[j].Rows[0][4].ToString();
                            Intime = Convert.ToDateTime(ds.Tables[j].Rows[0][4].ToString());
                        }
                        else
                            tcDate.Text = "-";
                        tcDate.Width = 80;
                        tblRow.Cells.Add(tcDate);

                        tcDate = new TableCell();
                        if (ds.Tables[j].Rows[0][5].ToString() != "")
                        {
                            tcDate.Text = ds.Tables[j].Rows[0][5].ToString();
                            OutTime = Convert.ToDateTime(ds.Tables[j].Rows[0][5].ToString());
                        }
                        else
                            tcDate.Text = "-";
                        tcDate.Width = 80;
                        tblRow.Cells.Add(tcDate);
                        Work = OutTime.Subtract(Intime);

                        tcDate = new TableCell();
                        if (ds.Tables[j].Rows[0][5].ToString() != "" || ds.Tables[j].Rows[0][4].ToString() != "")
                        {
                            tcDate.Text = Work.ToString(@"hh\:mm") + " Hrs";
                        }
                        if (ds.Tables[j].Rows[0][5].ToString() == "" || ds.Tables[j].Rows[0][4].ToString() == "")
                        {
                            tcDate.Text = "00:00 Hrs";
                        }
                        tcDate.Width = 80;
                        tblRow.Cells.Add(tcDate);

                        tcDate = new TableCell();
                        if (ds.Tables[j].Rows[0][7].ToString() != "")
                            tcDate.Text = ds.Tables[j].Rows[0][7].ToString();
                        else
                            tcDate.Text = "NA";
                        tcDate.Width = 80;
                        tblRow.Cells.Add(tcDate);
                        //tblRow.Cells.Add(tcEmpID);

                        tcMarkedBy = new TableCell();
                        tcMarkedBy.Text = ds.Tables[j].Rows[0]["MarkedByid"].ToString();
                        tcMarkedBy.ToolTip = ds.Tables[j].Rows[0]["MarkedBy"].ToString();
                        tcMarkedBy.Width = 40;
                        tblRow.Cells.Add(tcMarkedBy);

                        tcUpdatedBy = new TableCell();
                        tcUpdatedBy.Text = ds.Tables[j].Rows[0]["UpdatedByid"].ToString();
                        tcUpdatedBy.ToolTip = ds.Tables[j].Rows[0]["UpdatedBy"].ToString();
                        tcUpdatedBy.Width = 40;
                        tblRow.Cells.Add(tcUpdatedBy);
                        tblAtt.Rows.Add(tblRow);


                    }
                }
            }

        }
    }
}