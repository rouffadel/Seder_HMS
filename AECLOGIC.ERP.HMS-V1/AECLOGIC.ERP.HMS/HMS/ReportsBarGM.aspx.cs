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

namespace AECLOGIC.ERP.HMS.HMS
{
    public partial class ReportsBarGM : AECLOGIC.ERP.COMMON.WebFormMaster
    {
        protected override void OnInit(EventArgs e)
        {
            ModuleID = 1;
            base.OnInit(e);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
           
            if (!IsPostBack)
            {
                LoadChart1();
                LoadChart2();
               
            }
        }
        void LoadChart1()
        {
            try
            {
                SqlParameter[] p = new SqlParameter[1];
                 p[0] = new SqlParameter("@modid", ModuleID);
               DataSet ds = ds = SqlHelper.ExecuteDataset("SP_Chart_HMS_TransitEmployees", p);///
                /// // Chart 
                var series = Chart1.Series[0];
                foreach (DataRow item in ds.Tables[0].Rows)
                {
                    var point = new System.Web.UI.DataVisualization.Charting.DataPoint();
                    point.SetValueXY(item["WorkSite"], item["NoofEmployees"]);
                    point.PostBackValue = item["WorkSite"].ToString();
                    point.LabelToolTip = "No of Employees";
                    point.LegendText = "No of Employees";
                  
                    series.Points.Add(point);
                }
               
                Chart1.DataBind();// ViewState["dtgrpTb"]
            }
            catch { }
        }
        void LoadChart2()
        {
            try
            {

                SqlParameter[] p = new SqlParameter[1];
                p[0] = new SqlParameter("@modid", ModuleID);
                DataSet ds = ds = SqlHelper.ExecuteDataset("SP_Chart_HMS_TransitEmployees", p);///
                /// // Chart 
                var series = Chart2.Series[0];
                foreach (DataRow item in ds.Tables[0].Rows)
                {
                    var point = new System.Web.UI.DataVisualization.Charting.DataPoint();
                    point.SetValueXY(item["WorkSite"], item["NoofEmployees"]);
                    point.PostBackValue = item["WorkSite"].ToString();
                    point.LabelToolTip = "No of Employees";
                    point.LegendText = "No of Employees";
                   
                    series.Points.Add(point);
                }
               
                Chart2.DataBind();// ViewState["dtgrpTb"]

            }
            catch { }
        }
      
        void LoadBills()
        {
            try
            {
                SqlParameter[] p = new SqlParameter[1];
                p[0] = new SqlParameter("@UserID",  Convert.ToInt32(Session["UserId"]).ToString());
                DataSet ds = ds = SqlHelper.ExecuteDataset("SP_PMS_PurchaseBills_Chart", p);///
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
            if (x.Length > 0)
            {
                Response.Redirect("../PMS/AssignIndent.aspx?type=PM");
            }
        }
    }
}