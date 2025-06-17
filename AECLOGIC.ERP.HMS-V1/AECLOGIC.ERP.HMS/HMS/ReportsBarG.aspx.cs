using Aeclogic.Common.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.DataVisualization.Charting;
using System.Web.UI.WebControls;
namespace AECLOGIC.ERP.HMS.HMSV1
{
    public partial class reports_barV1 : AECLOGIC.ERP.COMMON.WebFormMaster
    {
        protected override void OnInit(EventArgs e)
        {
            ModuleID = 1;
            base.OnInit(e);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                string id =  Convert.ToInt32(Session["UserId"]).ToString();
            }
            catch
            {
                Response.Redirect("Home.aspx");
            }
            if (!IsPostBack)
            {
                LoadChart1();
                LoadChart2();
                LoadChart3();
                LoadChart4();
                LoadChart5();
                LoadChart6();
                LoadChart7();
                BindEmployeeDocDetails(); //in page load
            }
        }
        public bool IsEditable(string DocType)
        {

            if (DocType.Trim() == "Digital")
                return true;
            else
                return false;

        }
        public string DocNavigateUrl(string DocType, string EmpId, string DocID, string EmpDocID, string Value)
        {


            string ReturnVal = "";
            if (DocType.Trim() == "Digital")
            {

                ReturnVal = String.Format("AppointmentPreview.aspx?id={0}&DocID={1}&EmpDocID={2}", EmpId, DocID, EmpDocID);

            }
            else
                ReturnVal = "./scaneddocuments/" + EmpDocID + Value;
            return ReturnVal;
        }
        public void BindEmployeeDocDetails()
        {
            SqlParameter[] sqlParams = new SqlParameter[6];
            sqlParams[0] = new SqlParameter("@CurrentPage", 1);
            sqlParams[1] = new SqlParameter("@PageSize", 100);
            sqlParams[2] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
            sqlParams[2].Direction = ParameterDirection.ReturnValue;
            sqlParams[3] = new SqlParameter("@NoofRecords", System.Data.SqlDbType.Int);
            sqlParams[3].Direction = ParameterDirection.Output;
            sqlParams[4] = new SqlParameter("@EmpID", Convert.ToInt32(Session["UserId"]).ToString());
            sqlParams[5] = new SqlParameter("@DocName", DBNull.Value);
            DataSet ds = SQLDBUtil.ExecuteDataset("HR_GetEmpDocsByPaging", sqlParams);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                gvDocs.DataSource = ds.Tables[0];
            }
            else
            {
                gvDocs.EmptyDataText = "No Records Found";
            }
            gvDocs.DataBind();
        }

        void LoadChart1()
        {
            try
            {
                SqlParameter[] p = new SqlParameter[3];
                p[0] = new SqlParameter("@EmpID",  Convert.ToInt32(Session["UserId"]).ToString());
                p[1] = new SqlParameter("@ModuleID", ModuleID);
                p[2] = new SqlParameter("@year", DateTime.UtcNow.AddHours(5).AddMinutes(30).Year);
                DataSet ds = ds = SqlHelper.ExecuteDataset("SP_Chart_HMS_AdvanceRequested", p);///
                /// // Chart 
                var series = Chart1.Series[0];
                foreach (DataRow item in ds.Tables[0].Rows)
                {
                    var point = new System.Web.UI.DataVisualization.Charting.DataPoint();
                    point.SetValueXY(item["Recoveryfrom"], item["LoanAmount"]);
                    point.PostBackValue = item["Recoveryfrom"].ToString();
                  
                    series.Points.Add(point);
                }
                //Chart3.DataSource = ds.Tables[0];
                Chart1.DataBind();// ViewState["dtgrpTb"]
            }
            catch { }
        }
        void LoadChart2()
        {
            try
            {
                SqlParameter[] p = new SqlParameter[3];
                p[0] = new SqlParameter("@EmpId",  Convert.ToInt32(Session["UserId"]).ToString());
                p[1] = new SqlParameter("@Modid", ModuleID);
                p[2] = new SqlParameter("@year", DateTime.UtcNow.AddHours(5).AddMinutes(30).Year);
                DataSet ds = ds = SqlHelper.ExecuteDataset("SP_Chart_HMS_EmpAbsents", p);///
                /// // Chart 
                var series1 = Chart2.Series[0];
                foreach (DataRow item in ds.Tables[0].Rows)
                {
                    var point = new System.Web.UI.DataVisualization.Charting.DataPoint();
                    point.SetValueXY(item["ABMonth"], item["CountofAbsents"]);
                    point.PostBackValue = "D";
                  
                    series1.Points.Add(point);                  
                }
                Chart2.DataBind();// ViewState["dtgrpTb"]
            }
            catch { }
        }
        void LoadChart3()
        {
            try
            {
                SqlParameter[] p = new SqlParameter[3];
                p[0] = new SqlParameter("@EmpId",  Convert.ToInt32(Session["UserId"]).ToString());
                p[1] = new SqlParameter("@Modid", ModuleID);
                p[2] = new SqlParameter("@year", DateTime.UtcNow.AddHours(5).AddMinutes(30).Year);
                DataSet ds = ds = SqlHelper.ExecuteDataset("SP_Chart_HMS_OT", p);///
                /// // Chart 
                var series1 = Chart3.Series[0];
                var series2 = Chart3.Series[1];
                foreach (DataRow item in ds.Tables[0].Rows)
                {
                    var point = new System.Web.UI.DataVisualization.Charting.DataPoint();
                    point.SetValueXY(item["OTmonth"], item["OTHours"]);
                    point.PostBackValue = "H";
                   
                    series1.Points.Add(point);
                    var point2 = new System.Web.UI.DataVisualization.Charting.DataPoint();
                    point2.SetValueXY(item["OTmonth"], item["OTAmount"]);
                    point2.PostBackValue = "A";
                   
                    series2.Points.Add(point2);
                }
                Chart3.DataBind();// ViewState["dtgrpTb"]
            }
            catch { }
        }
        void LoadChart4()
        {
            try
            {
                SqlParameter[] p = new SqlParameter[3];
                p[0] = new SqlParameter("@EmpId",  Convert.ToInt32(Session["UserId"]).ToString());
                p[1] = new SqlParameter("@Modid", ModuleID);
                p[2] = new SqlParameter("@year", DateTime.UtcNow.AddHours(5).AddMinutes(30).Year);
                DataSet ds = ds = SqlHelper.ExecuteDataset("SP_Chart_HMS_LeaveStatus", p);///
                /// // Chart 
                var series1 = Chart4.Series[0];
                foreach (DataRow item in ds.Tables[0].Rows)
                {
                    var point = new System.Web.UI.DataVisualization.Charting.DataPoint();
                    point.SetValueXY(item["Status"], item["NoofDays"]);
                    point.PostBackValue = "L";
                   
                    series1.Points.Add(point);
                }
                //Chart3.DataSource = ds.Tables[0];
                Chart4.DataBind();// ViewState["dtgrpTb"]
            }
            catch { }
        }
        void LoadChart5()
        {
            try
            {
                SqlParameter[] p = new SqlParameter[3];
                p[0] = new SqlParameter("@EmpId",  Convert.ToInt32(Session["UserId"]).ToString());
                p[1] = new SqlParameter("@Modid", ModuleID);
                p[2] = new SqlParameter("@year", DateTime.UtcNow.AddHours(5).AddMinutes(30).Year);
                DataSet ds = ds = SqlHelper.ExecuteDataset("SP_Chart_HMS_Performance", p);///
                /// // Chart 
                var series1 = Chart5.Series[0];
                foreach (DataRow item in ds.Tables[0].Rows)
                {
                    var point = new System.Web.UI.DataVisualization.Charting.DataPoint();
                    point.SetValueXY(item["PMonth"], item["total"]);
                    point.PostBackValue = "D";
                   
                    series1.Points.Add(point);
                }
                //Chart3.DataSource = ds.Tables[0];
                Chart5.DataBind();// ViewState["dtgrpTb"]
            }
            catch { }
        }
        void LoadChart6()
        {
            try
            {
                SqlParameter[] p = new SqlParameter[3];
                p[0] = new SqlParameter("@EmpId",  Convert.ToInt32(Session["UserId"]).ToString());
                p[1] = new SqlParameter("@Modid", ModuleID);
                p[2] = new SqlParameter("@year", DateTime.UtcNow.AddHours(5).AddMinutes(30).Year);
                DataSet ds= SqlHelper.ExecuteDataset("SP_Chart_HMS_EmpGratuity", p);///
                /// // Chart 
                var series1 = Chart6.Series[0];
                foreach (DataRow item in ds.Tables[0].Rows)
                {
                    var point = new System.Web.UI.DataVisualization.Charting.DataPoint();
                    point.SetValueXY(item["GMonth"], item["GAmt"]);
                    point.PostBackValue = "D";
                   
                    series1.Points.Add(point);
                }
                //Chart3.DataSource = ds.Tables[0];
                Chart6.DataBind();// ViewState["dtgrpTb"]
            }
            catch { }
        }

        void LoadChart7()
        {
            try
            {
                SqlParameter[] p = new SqlParameter[3];
                p[0] = new SqlParameter("@EmpID", Convert.ToInt32(Session["UserId"]).ToString());
                p[1] = new SqlParameter("@ModuleID", ModuleID);
                p[2] = new SqlParameter("@year", DateTime.UtcNow.AddHours(5).AddMinutes(30).Year);
                DataSet ds = ds = SqlHelper.ExecuteDataset("sp_MyAttendance", p);///
                                                                                 /// // Chart 
                //var series = Chart1.Series[0];
                var series = Chart7.Series[0];
                foreach (DataRow item in ds.Tables[0].Rows)
                {
                    var point = new System.Web.UI.DataVisualization.Charting.DataPoint();
                    point.SetValueXY(item["Month"], item["AttandanceCount"]);
                    point.PostBackValue = item["Month"].ToString();

                    series.Points.Add(point);
                }
                //Chart3.DataSource = ds.Tables[0];
                Chart7.DataBind();// ViewState["dtgrpTb"]
            }
            catch { }
        }

        void LoadBills()
        {
            try
            {
                SqlParameter[] p = new SqlParameter[1];
                p[0] = new SqlParameter("@UserID",  Convert.ToInt32(Session["UserId"]).ToString());
                DataSet ds  = SqlHelper.ExecuteDataset("SP_PMS_PurchaseBills_Chart", p);///
                /// // Chart 
                var series1 = Chart2.Series[0];
                var series2 = Chart2.Series[1];
                var series3 = Chart2.Series[2];
                var series4 = Chart2.Series[3];
                var series5 = Chart2.Series[4];
                foreach (DataRow item in ds.Tables[0].Rows)
                {
                    var point = new System.Web.UI.DataVisualization.Charting.DataPoint();
                    point.SetValueXY(item["Process"], item["Waiting"]);
                    point.PostBackValue = "W";
                   
                    series1.Points.Add(point);
                    var point1 = new System.Web.UI.DataVisualization.Charting.DataPoint();
                    point1.SetValueXY(item["Process"], item["A/C Drafts"]);
                    point1.PostBackValue = "D";
                  
                    series2.Points.Add(point1);
                    var point2 = new System.Web.UI.DataVisualization.Charting.DataPoint();
                    point2.SetValueXY(item["Process"], item["A/C Posted"]);
                    point2.PostBackValue = "P";
                  
                    series3.Points.Add(point2);
                    var point3 = new System.Web.UI.DataVisualization.Charting.DataPoint();
                    point3.SetValueXY(item["Process"], item["Rejected"]);
                    point3.PostBackValue = "R";
                   
                    series4.Points.Add(point3);
                    var point5 = new System.Web.UI.DataVisualization.Charting.DataPoint();
                    point5.SetValueXY(item["Process"], item["UnBilled"]);
                    point5.PostBackValue = "U";
                   
                    series5.Points.Add(point5);
                }
                //Chart3.DataSource = ds.Tables[0];
                Chart2.DataBind();// ViewState["dtgrpTb"]
            }
            catch { }
        }
        protected void Chart2_Click(object sender, ImageMapEventArgs e)
        {
            string x = Convert.ToString(e.PostBackValue);
        }
        protected void Chart1_Click(object sender, ImageMapEventArgs e)
        {
            string x = Convert.ToString(e.PostBackValue);
            if (x == "D")
            {
                Response.Redirect("../PMS/SearchPO.aspx?key=0");
            }
        }
        protected void Chart3_Click(object sender, ImageMapEventArgs e)
        {
            string x = Convert.ToString(e.PostBackValue);
            if (x.Length>0)
            {
                Response.Redirect("../PMS/AssignIndent.aspx?type=PM");
            }
        }
    }
}