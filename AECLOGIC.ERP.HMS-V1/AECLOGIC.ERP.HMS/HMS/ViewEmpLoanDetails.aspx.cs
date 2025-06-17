using System;
using AECLOGIC.ERP.COMMON;
using System.Data;
using AECLOGIC.ERP.HMS;
using System.Data.SqlClient;
using Aeclogic.Common.DAL;
using System.Drawing;
using System.IO;

namespace AECLOGIC.ERP.HMSV1
{
    public partial class ViewEmpLoanDetailsV1 : System.Web.UI.Page//AECLOGIC.ERP.COMMON.WebFormMaster
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
               
                try
                {
                    int FEID = Convert.ToInt32(Request.QueryString["LoanId"]);
                   
                 
                    SqlParameter[] p = new SqlParameter[1];
                    p[0] = new SqlParameter("@LoanId", FEID);
                  
                    DataSet ds = SqlHelper.ExecuteDataset("HR_getEmployeeLoanDetails", p);
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {

                        if (ds.Tables[0].Rows[0]["LoanId"].ToString() != "")
                        {
                            lblLID.Text = ds.Tables[0].Rows[0]["LoanId"].ToString();
                        }
                        else
                            lblLID.Text = "--";
                        if (ds.Tables[0].Rows[0]["WorkSite"].ToString()!="")
                            {
                             lblWSite.Text = ds.Tables[0].Rows[0]["WorkSite"].ToString();
                             }
                        else
                            lblWSite.Text = "--";
                        if (ds.Tables[0].Rows[0]["ProjectName"].ToString() != "")
                        {
                            lblProj.Text = ds.Tables[0].Rows[0]["ProjectName"].ToString();
                        }
                        else
                            lblProj.Text = "--";
                        if (ds.Tables[0].Rows[0]["Designation"].ToString() != "")
                        {
                            lbldesig.Text = ds.Tables[0].Rows[0]["Designation"].ToString();
                        }
                        else
                            lbldesig.Text = "--";
                        if (ds.Tables[0].Rows[0]["RQName"].ToString() != "")
                        {
                            LblReqBy.Text = ds.Tables[0].Rows[0]["RQName"].ToString();
                        }
                        else
                            lblPHApp.Text = "--";

                        if (ds.Tables[0].Rows[0]["PHName"].ToString() != "")
                        {
                            lblPHApp.Text = ds.Tables[0].Rows[0]["PHName"].ToString();
                        }
                        else
                            lblPHApp.Text = "--";
                        if (ds.Tables[0].Rows[0]["DHName"].ToString() != "")
                        {
                            lblDHApp.Text = ds.Tables[0].Rows[0]["DHName"].ToString();
                        }
                        else
                            lblDHApp.Text = "--";

                        if (ds.Tables[0].Rows[0]["HRName"].ToString() != "")
                        {
                            lblHRApp.Text = ds.Tables[0].Rows[0]["HRName"].ToString();
                        }
                        else
                            lblHRApp.Text = "--";
                        if (ds.Tables[0].Rows[0]["GMName"].ToString() != "")
                        {
                            lblGMApp.Text = ds.Tables[0].Rows[0]["GMName"].ToString();
                        }
                        else
                            lblGMApp.Text = "--";
                        if (ds.Tables[0].Rows[0]["CFOName"].ToString() != "")
                        {
                            lblCfoApp.Text = ds.Tables[0].Rows[0]["CFOName"].ToString();
                        }
                        else
                            lblCfoApp.Text = "--";

                        gdvIndent.DataSource = ds.Tables[0];
                        gdvIndent.DataBind();
                        if (ds.Tables[0].Rows[0]["BarCodeSequence"].ToString() != "")
                            GenerateBarCode(ds.Tables[0].Rows[0]["BarCodeSequence"].ToString());
                    }

                  
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
                    imgBarcodeFE.Visible = true;
                }
                plBarCodeFE.Controls.Add(imgBarCode);
            }

        }


    }
}