using Aeclogic.Common.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Configuration;
using System.Web.UI.WebControls;
using System.Web.UI.DataVisualization.Charting;
using System.Drawing;
namespace AECLOGIC.ERP.HMS
{
    public partial class AdminDefault : AECLOGIC.ERP.COMMON.WebFormMaster
    {
       // private static string _Companyname = ConfigurationSettings.AppSettings["Company"].ToString();
        int color = 1;
        protected override void OnInit(EventArgs e)
        {
            ModuleID = 1;
            Session["ModuleID"] = ModuleID;
            base.OnInit(e);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
             //  chDayWiseReQQty.Series["Series1"].IsValueShownAsLabel = true;
            #region commented by kk on 19-9-2016
            //if (User.Identity.IsAuthenticated)
            //{
            //    DataSet ds = new DataSet();
            //    ds = ProcDept.GetLoginSessions(User.Identity.Name.ToString(), ModuleID);
            //    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            //    {
            //        // Get Role Id from DataBase
            //        try
            //        {
            //            if (ds.Tables.Count > 1)
            //            {
            //                if (ds.Tables[0].Rows.Count > 0)
            //                {
            //                    Session["UserId"] = ds.Tables[0].Rows[0]["empid"].ToString();
            //                    Session["Loginname"] = ds.Tables[0].Rows[0]["Name"].ToString();
            //                    Session["UserName"] = ds.Tables[0].Rows[0]["Name"].ToString();
            //                    Session["Site"] = ds.Tables[0].Rows[0]["Categary"].ToString();
            //                    if (ds.Tables[1].Rows.Count > 0)
            //                    {
            //                        Session["RoleId"] = ds.Tables[1].Rows[0]["RoleId"].ToString();
            //                        Session["RoleName"] = ds.Tables[1].Rows[0]["RoleName"].ToString();
            //                    }
            //                    if (ds.Tables[2].Rows.Count > 0)
            //                    {
            //                        Session["CompanyID"] = ds.Tables[2].Rows[0]["CompanyID"].ToString();
            //                        Session["CompanyName"] = ds.Tables[2].Rows[0]["CompanyName"].ToString();
            //                    }
            //                    //Added by pratap date: 27-02-2016 O12 BUG.
            //                    Session["CompanyID"] = ds.Tables[2].Rows[0]["CompanyID"].ToString();
            //                    Session["CompanyName"] = ds.Tables[2].Rows[0]["CompanyName"].ToString();
            //                    //if (ds.Tables.Count > 3)
            //                    //{
            //                    //    Session["CPID"] = ds.Tables[2].Rows[0]["CPID"].ToString();
            //                    //}
            //                }
            //                // Response.Redirect("POReportStatus.aspx");
            //            }
            //            else
            //            {
            //                Response.Redirect("Home.aspx");
            //            }
            //        }
            //        catch (Exception ex)
            //        {
            //            throw ex;
            //        }
                //}
                //else
                //{
                //    Response.Redirect("Home.aspx");
                //} }
#endregion
            if(!IsPostBack)
            {
                //LoadChart();
                //Loadnotes();
                //LoadBills();
                //LoadChart1();
                //LoadChart2();

                //Populating a DataTable from database.
       // DataTable dt = this.GetData();
 
                 SqlParameter[] parms1 = new SqlParameter[2];
                parms1[0] = new SqlParameter("@empid", Session["UserID"].ToString());
                parms1[1] = new SqlParameter("@moduleid", ModuleID.ToString());
                DataSet ds = SqlHelper.ExecuteDataset("sg_NotificationsEmpwise_Graphs", parms1);

        //Building an HTML string.
        StringBuilder html = new StringBuilder();
        html.Append("<table border = '1'>");
        int i = 1;
        double d = (Double)ds.Tables[1].Rows.Count / 2;
        double trcount = Math.Round(d);
        if (d % 2 != 0)
            trcount = trcount + 1;

       // int trcount = 3;
        int Graphcount = 0;
        while (i <= trcount)
        {
            html.Append("<tr>");
            int j=1;
            if (Graphcount <= ds.Tables[1].Rows.Count-1)
            {
                while (j <= 2 && Graphcount <= ds.Tables[1].Rows.Count-1)
                {
                    html.Append("<td>");
                    Session["CHARTID"] = String.Empty;
                    Chart Chart1 = new Chart();
                    Chart1.ID = "Chart" + Graphcount;
                    Session["CHARTID"] = "Chart" + Graphcount;
                    if (Graphcount == 2)
                    {
 
                    }
                    DataRow[] dr = ds.Tables[0].Select("pageid = " + ds.Tables[1].Rows[Graphcount]["pageid"]);
                    DataTable dt1 = dr.CopyToDataTable();
                    createChart(dt1, Chart1); 
                    Chart1.Titles.Add(new Title(dt1.Rows[0]["MenuPath"].ToString(), Docking.Top, new Font("Times New Roman", 12f, FontStyle.Bold), Color.Black));
                    Chart1.Series[0].LegendUrl = dt1.Rows[0]["pagename"].ToString();
                    Chart1.Series[0].Url = dt1.Rows[0]["pagename"].ToString();
                    Chart1.ChartAreas["ChartArea1"].AxisX.MajorGrid.Enabled = false;
                    Chart1.ChartAreas["ChartArea1"].AxisY.MajorGrid.Enabled = false;
                    PlaceHolder1.Controls.Add(Chart1);
                    html.Append("</td>");
                    j++;
                    Graphcount++;
                }
            }
            html.Append("</tr>");
            i++;
        }
 
        html.Append("</table>");
        PlaceHolder1.Controls.Add(new Literal { Text = html.ToString() });
            }
        }

        private void createChart(DataTable dt, Chart chart1)
        {
            chart1.Series.Add(new Series());

            chart1.ChartAreas.Add(new ChartArea());
            chart1.ChartAreas[0].Area3DStyle.Enable3D = false;

            foreach (DataRow item in dt.Rows)
            {
                var point = new System.Web.UI.DataVisualization.Charting.DataPoint();
                point.SetValueXY(item["Stage"], item["Nos"]);
                point.PostBackValue = "A";
                chart1.Series[0].Points.Add(point);
                chart1.DataBind();
            }
            string chartname = Session["CHARTID"].ToString();
            int id = Convert.ToInt32(chartname.Replace("Chart", ""));

            DataTable table = new DataTable();
            table.Columns.Add("id", typeof(int));
            table.Columns.Add("code", typeof(string));
            table.Rows.Add(1, "#7FAAFF");
            table.Rows.Add(2, "#01b8aa");
            table.Rows.Add(3, "#caa5c2");
            table.Rows.Add(4, "#feab85");
            table.Rows.Add(5, "#8ad4eb");
            table.Rows.Add(6, "#b887ad");
            table.Rows.Add(7, "#a8babd");
            table.Rows.Add(8, "#425457");
            table.Rows.Add(9, "#aa8c09");
            if(color==9)
            {
               color = 1;
            }
            Color colour = ColorTranslator.FromHtml(table.Rows[color]["code"].ToString());
            Color transparent = Color.FromArgb(128, colour);
            chart1.Series[0].Color = colour;
            chart1.Series[0].IsValueShownAsLabel = true;
            chart1.Width = new System.Web.UI.WebControls.Unit(500, System.Web.UI.WebControls.UnitType.Pixel);
            color++;
        }




        void LoadDataReqQty()
        {
            try
            {
                //int IndentReqId=Convert.ToInt32( ViewState["IndentReqId"] )= IndentReqId;
                DataSet ds = ds = SqlHelper.ExecuteDataset("V_PMS_MyMRs");  
                ViewState["dsgrpTb"] = ds;
               // LoadChart();
            }
            catch { }
        }

































        //void LoadChart1()
        //{
        //    try
        //    {
        //        SqlParameter[] p = new SqlParameter[2];
        //        p[0] = new SqlParameter("@empid", Session["UserId"].ToString());
        //        p[1] = new SqlParameter("@pageid", 465);
        //        DataSet ds = ds = SqlHelper.ExecuteDataset("sg_NotificationsEmpwise_Graphs", p);

        //        var series1 = Chart1.Series[0];
        //        foreach (DataRow item in ds.Tables[0].Rows)
        //        {
        //            var point = new System.Web.UI.DataVisualization.Charting.DataPoint();
        //            point.SetValueXY(item["Stage"], item["Nos"]);
        //            point.PostBackValue = "A";
        //            series1.Points.Add(point);
        //            Chart1.DataBind();
        //        }


        //    }
        //    catch { }
        //}

        //void LoadChart2()
        //{
        //    try
        //    {
        //        SqlParameter[] p = new SqlParameter[2];
        //        p[0] = new SqlParameter("@empid", Session["UserId"].ToString());
        //        p[1] = new SqlParameter("@pageid", 751);
        //        DataSet ds = ds = SqlHelper.ExecuteDataset("sg_NotificationsEmpwise_Graphs", p);

        //        var series1 = Chart2.Series[0];
        //        foreach (DataRow item in ds.Tables[0].Rows)
        //        {
        //            var point = new System.Web.UI.DataVisualization.Charting.DataPoint();
        //            point.SetValueXY(item["Stage"], item["Nos"]);
        //            point.PostBackValue = "B";
        //            series1.Points.Add(point);
        //            Chart2.DataBind();
        //        }


        //    }
        //    catch { }
        //}









        //void LoadChart()
        //{
        //    try
        //    {
        //         SqlParameter[] p = new SqlParameter[1];
        //         p[0] = new SqlParameter("@raisedby", Session["UserId"].ToString());               
        //        DataSet ds = ds = SqlHelper.ExecuteDataset("PMS_getRaised",p);///
        //        /// // Chart 
        //        var series = chDayWiseReQQty.Series[0];
        //        foreach (DataRow item in ds.Tables[0].Rows)
        //        {
        //            var point = new System.Web.UI.DataVisualization.Charting.DataPoint();
        //            point.SetValueXY(item["Status"], item["count"]);
        //            point.PostBackValue = item["Status"].ToString();
        //            //point.MarkerImage = exam.PictureUrl;
        //            //point.Label = item["ReqQty"].ToString();
        //            //point.LegendToolTip = item["ReqQty"].ToString();
        //            //point.ToolTip = item["ReQDt"].ToString();
        //            series.Points.Add(point);
        //        }
        //        //chDayWiseReQQty.DataSource = ds.Tables[0];
        //        chDayWiseReQQty.DataBind();// ViewState["dtgrpTb"]
        //    }
        //    catch { }
        //}
        //void Loadnotes()
        //{
        //    try
        //    {
        //         SqlParameter[] p = new SqlParameter[1];
        //         p[0] = new SqlParameter("@UserID", Session["UserId"].ToString());               
        //        DataSet ds = ds = SqlHelper.ExecuteDataset("SP_PMS_PurchaseNote_Chart",p);///
        //        /// // Chart 
        //        var series1 = Chart1.Series[0];
        //        var series2 = Chart1.Series[1];
        //        var series3 = Chart1.Series[2];
        //        var series4 = Chart1.Series[3];
        //        foreach (DataRow item in ds.Tables[0].Rows)
        //        {
        //            var point = new System.Web.UI.DataVisualization.Charting.DataPoint();
        //            point.SetValueXY(item["Process"], item["Drafts"]);
        //            point.PostBackValue = "D";
        //            //point.MarkerImage = exam.PictureUrl;
        //            //point.Label = item["ReqQty"].ToString();
        //            //point.LegendToolTip = item["ReqQty"].ToString();
        //            //point.ToolTip = item["ReQDt"].ToString();
        //            series1.Points.Add(point);
        //            var point1 = new System.Web.UI.DataVisualization.Charting.DataPoint();
        //            point1.SetValueXY(item["Process"], item["Arrival"]);
        //            point1.PostBackValue = "A";
        //            //point.MarkerImage = exam.PictureUrl;
        //            //point.Label = item["ReqQty"].ToString();
        //            //point.LegendToolTip = item["ReqQty"].ToString();
        //            //point.ToolTip = item["ReQDt"].ToString();
        //            series2.Points.Add(point1);
        //            var point2 = new System.Web.UI.DataVisualization.Charting.DataPoint();
        //            point2.SetValueXY(item["Process"], item["QA"]);
        //            point2.PostBackValue = "Q";
        //            //point.MarkerImage = exam.PictureUrl;
        //            //point.Label = item["ReqQty"].ToString();
        //            //point.LegendToolTip = item["ReqQty"].ToString();
        //            //point.ToolTip = item["ReQDt"].ToString();
        //            series3.Points.Add(point2);
        //            var point3 = new System.Web.UI.DataVisualization.Charting.DataPoint();
        //            point3.SetValueXY(item["Process"], item["Received"]);
        //            point3.PostBackValue = "R";
        //            //point.MarkerImage = exam.PictureUrl;
        //            //point.Label = item["ReqQty"].ToString();
        //            //point.LegendToolTip = item["ReqQty"].ToString();
        //            //point.ToolTip = item["ReQDt"].ToString();
        //            series4.Points.Add(point3);
        //        }
        //        //chDayWiseReQQty.DataSource = ds.Tables[0];
        //        Chart1.DataBind();// ViewState["dtgrpTb"]
        //    }
        //    catch { }
        //}
        //void LoadBills()
        //{
        //    try
        //    {
        //        SqlParameter[] p = new SqlParameter[1];
        //        p[0] = new SqlParameter("@UserID", Session["UserId"].ToString());
        //        DataSet ds = ds = SqlHelper.ExecuteDataset("SP_PMS_PurchaseBills_Chart", p);///
        //        /// // Chart 
        //        var series1 = Chart2.Series[0];
        //        var series2 = Chart2.Series[1];
        //        var series3 = Chart2.Series[2];
        //        var series4 = Chart2.Series[3];
        //        var series5 = Chart2.Series[4];
        //        foreach (DataRow item in ds.Tables[0].Rows)
        //        {
        //            var point = new System.Web.UI.DataVisualization.Charting.DataPoint();
        //            point.SetValueXY(item["Process"], item["Waiting"]);
        //            point.PostBackValue = "W";
        //            //point.MarkerImage = exam.PictureUrl;
        //            //point.Label = item["ReqQty"].ToString();
        //            //point.LegendToolTip = item["ReqQty"].ToString();
        //            //point.ToolTip = item["ReQDt"].ToString();
        //            series1.Points.Add(point);
        //            var point1 = new System.Web.UI.DataVisualization.Charting.DataPoint();
        //            point1.SetValueXY(item["Process"], item["A/C Drafts"]);
        //            point1.PostBackValue = "D";
        //            //point.MarkerImage = exam.PictureUrl;
        //            //point.Label = item["ReqQty"].ToString();
        //            //point.LegendToolTip = item["ReqQty"].ToString();
        //            //point.ToolTip = item["ReQDt"].ToString();
        //            series2.Points.Add(point1);
        //            var point2 = new System.Web.UI.DataVisualization.Charting.DataPoint();
        //            point2.SetValueXY(item["Process"], item["A/C Posted"]);
        //            point2.PostBackValue = "P";
        //            //point.MarkerImage = exam.PictureUrl;
        //            //point.Label = item["ReqQty"].ToString();
        //            //point.LegendToolTip = item["ReqQty"].ToString();
        //            //point.ToolTip = item["ReQDt"].ToString();
        //            series3.Points.Add(point2);
        //            var point3 = new System.Web.UI.DataVisualization.Charting.DataPoint();
        //            point3.SetValueXY(item["Process"], item["Rejected"]);
        //            point3.PostBackValue = "R";
        //            //point.MarkerImage = exam.PictureUrl;
        //            //point.Label = item["ReqQty"].ToString();
        //            //point.LegendToolTip = item["ReqQty"].ToString();
        //            //point.ToolTip = item["ReQDt"].ToString();
        //            series4.Points.Add(point3);
        //            var point5 = new System.Web.UI.DataVisualization.Charting.DataPoint();
        //            point5.SetValueXY(item["Process"], item["UnBilled"]);
        //            point5.PostBackValue = "U";
        //            //point.MarkerImage = exam.PictureUrl;
        //            //point.Label = item["ReqQty"].ToString();
        //            //point.LegendToolTip = item["ReqQty"].ToString();
        //            //point.ToolTip = item["ReQDt"].ToString();
        //            series5.Points.Add(point5);
        //        }
        //        //chDayWiseReQQty.DataSource = ds.Tables[0];
        //        Chart2.DataBind();// ViewState["dtgrpTb"]
        //    }
        //    catch { }
        //}
        protected void Chart2_Click(object sender, ImageMapEventArgs e)
        {
            string x = Convert.ToString(e.PostBackValue);
        }
        protected void Chart1_Click(object sender, ImageMapEventArgs e)
        {
            string x = Convert.ToString(e.PostBackValue);
            if(x=="D")
            {
                Response.Redirect("../PMS/SearchPO.aspx?key=0");
            }
        }
        protected void chDayWiseReQQty_Click(object sender, ImageMapEventArgs e)
        {
            string x = Convert.ToString(e.PostBackValue);
            if (x == "PM Approvals")
            {
                Response.Redirect("../PMS/AssignIndent.aspx?type=PM");
            }  
            if (x == "Final Approvals")
            {
                Response.Redirect("../PMS/AssignIndent.aspx?type=apr");
            } 
            if (x == "Enquiries")
            {
                Response.Redirect("../PMS/ProcEnquiry.aspx");
            } 
            if (x == "Assignments")
            {
                Response.Redirect("../PMS/AssignIndent.aspx");
            }  
            if (x == "CM Approvals")
            {
                Response.Redirect("../PMS/AssignIndent.aspx?type=CM");
            } 
            if (x == "Quotations")
            {
                Response.Redirect("../PMS/ProcQuotations.aspx");
            } 
            if (x == "Purchase Orders")
            {
                Response.Redirect("../PMS/SearchPO.aspx?Key=0");
            }
            if (x == "Rejected")
            {
                Response.Redirect("../PMS/ProcEditRejectIndent.aspx");
            }
            if (x == "Releases")
            {
                Response.Redirect("../PMS/ProPurchaseOrder.aspx");
            }
        }
    }
}
