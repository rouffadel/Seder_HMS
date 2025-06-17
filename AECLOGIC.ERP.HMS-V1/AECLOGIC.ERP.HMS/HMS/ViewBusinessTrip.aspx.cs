using Aeclogic.Common.DAL;
using AECLOGIC.ERP.COMMON;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AECLOGIC.ERP.HMS.HMSV1
{
    public partial class ViewBusinessTripV1 : System.Web.UI.Page
    {
        int ModuleID = 1;
        Paging_Objects PG = new Paging_Objects();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                try
                {
                    int BID = Convert.ToInt32(Request.QueryString["BID"]);
                    SqlParameter[] q = new SqlParameter[1];
                    q[0] = new SqlParameter("@MID", BID);
                    DataSet ds = SqlHelper.ExecuteDataset("Sp_BusinessTrip_View", q);
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        lblIndentId.Text = ds.Tables[0].Rows[0]["ID"].ToString();
                        lblWorksite.Text = ds.Tables[0].Rows[0]["worksite"].ToString();
                        lblProject.Text = ds.Tables[0].Rows[0]["ProjectName"].ToString();
                        lblDesigination.Text = ds.Tables[0].Rows[0]["Designation"].ToString();

                        lblcreatedBy.Text = ds.Tables[0].Rows[0]["Created By"].ToString();
                        lblprojectHead.Text = ds.Tables[0].Rows[0]["Project Head Approval"].ToString();
                        lblGmApproval.Text = ds.Tables[0].Rows[0]["GM Approval"].ToString();
                        lblHRReview.Text = ds.Tables[0].Rows[0]["HR Review"].ToString();
                        lblHRApproval.Text = ds.Tables[0].Rows[0]["HR Approval"].ToString();
                        lblAccountPostingBy.Text = ds.Tables[0].Rows[0]["Account Posting"].ToString();
                        lblcreatedBy.Text = ds.Tables[0].Rows[0]["Created By"].ToString();
                        gdvIndent.DataSource = ds.Tables[1];
                    }

                    gdvIndent.DataBind();
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[2].Rows.Count > 0)
                    {

                        gvShow.DataSource = ds.Tables[2];
                        gvShow.DataBind();
                        gvShow.Visible = true;
                    }
                    else
                    {
                        gvShow.Visible = false;
                    }
                    if (ds.Tables[0].Rows[0]["BarCodeSequence"].ToString() != "")
                        GenerateBarCode(ds.Tables[0].Rows[0]["BarCodeSequence"].ToString());
                }
                catch { }
            }
        }


        public void GenerateBarCode(string strBarcode)
        {
            string barCode = strBarcode;//"HHSCC1200";
            System.Web.UI.WebControls.Image imgBarCode = new System.Web.UI.WebControls.Image();
            using (Bitmap bitMap = new Bitmap(barCode.Length * 40, 80))
            {
                using (Graphics graphics = Graphics.FromImage(bitMap))
                {
                    Font oFont = new Font("IDAutomationHC39M Free Version", 16);
                    PointF point = new PointF(2f, 2f);
                    SolidBrush blackBrush = new SolidBrush(Color.Black);
                    SolidBrush whiteBrush = new SolidBrush(Color.White);
                    graphics.FillRectangle(whiteBrush, 0, 0, bitMap.Width, bitMap.Height);
                    graphics.DrawString("*" + barCode + "*", oFont, blackBrush, point);
                }
                using (MemoryStream ms = new MemoryStream())
                {
                    //bitMap.Save(ms, ImageFormat.Png);
                    //imgBarcode.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(ms.ToArray());
                    //imgBarcode.Visible = true;

                    bitMap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                    byte[] byteImage = ms.ToArray();

                    Convert.ToBase64String(byteImage);
                    imgBarCode.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(byteImage);
                    imgBarcodeBT.Visible = true;
                }
                plBarCodeBT.Controls.Add(imgBarCode);
            }

        }

    }
}