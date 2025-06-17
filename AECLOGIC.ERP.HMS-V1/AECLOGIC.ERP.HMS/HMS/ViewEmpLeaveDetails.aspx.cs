using System;
using AECLOGIC.ERP.COMMON;
using System.Data;
using AECLOGIC.ERP.HMS;
using System.Data.SqlClient;
using Aeclogic.Common.DAL;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;

namespace AECLOGIC.ERP.HMSV1
{
    public partial class ViewEmpLeaveDetailsV1 : System.Web.UI.Page//AECLOGIC.ERP.COMMON.WebFormMaster
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
               
                try
                {
                    int LID = Convert.ToInt32(Request.QueryString["LID"]);
                   
                 
                    SqlParameter[] p = new SqlParameter[1];
                    p[0] = new SqlParameter("@LID", LID);
                  
                    DataSet ds = SqlHelper.ExecuteDataset("HR_getEmployeeLeaveDetails", p);
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {

                        if (ds.Tables[0].Rows[0]["LID"].ToString() != "")
                        {
                            lblLID.Text = ds.Tables[0].Rows[0]["LID"].ToString();
                        }
                        else
                            lblLID.Text = "--";
                        if (ds.Tables[0].Rows[0]["WorkSite"].ToString() != "")
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
                        if (ds.Tables[0].Rows[0]["ReqBy"].ToString() != "")
                        {
                            lblReqBy.Text = ds.Tables[0].Rows[0]["ReqBy"].ToString();
                        }
                        else
                            lblReqBy.Text = "--";

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
            //System.Web.UI.WebControls.Image imgBarCode = new System.Web.UI.WebControls.Image();
            //string encodedText = EncodeCode128(barCode);

            //using (Bitmap bitMap = GenerateBarcode(encodedText))
            //{
            //    using (MemoryStream ms = new MemoryStream())
            //    {
            //        bitMap.Save(ms, ImageFormat.Png);
            //        byte[] byteImage = ms.ToArray();
            //        string base64String = Convert.ToBase64String(byteImage);
            //        imgBarCode.ImageUrl = "data:image/png;base64," + base64String;
            //        imgBarCode.Visible = true;
            //    }
            //}
            //plBarCodeLR.Controls.Add(imgBarCode);
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
                    imgBarcodeLR.Visible = true;
                }
                plBarCodeLR.Controls.Add(imgBarCode);
            }
        }

        
        static readonly string[] Code128Patterns = {
        "11011001100", "11001101100", "11001100110", "10010011000", "10010001100", "10001001100",
        "10011001000", "10011000100", "10001100100", "11001001000", "11001000100", "11000100100",
        "10110011100", "10011011100", "10011001110", "10111001100", "10011101100", "10011100110",
        "11001110010", "11001011100", "11001001110", "11011100100", "11001110100", "11101101110",
        "11101001100", "11100101100", "11100100110", "11101100100", "11100110100", "11100110010",
        "11011011000", "11011000110", "11000110110", "10100011000", "10001011000", "10001000110",
        "10110001000", "10001101000", "10001100010", "11010001000", "11000101000", "11000100010",
        "10110111000", "10110001110", "10001101110", "10111011000", "10111000110", "10001110110",
        "11101110110", "11010001110", "11000101110", "11011101000", "11011100010", "11011101110",
        "11101011000", "11101000110", "11100010110", "11101101000", "11101100010", "11100011010",
        "11101111010", "11001000010", "11110001010", "10100110000", "10100001100", "10010110000",
        "10010000110", "10000101100", "10000100110", "10110010000", "10110000100", "10011010000",
        "10011000010", "10000110100", "10000110010", "11000010010", "11001010000", "11110111010",
        "11000010100", "10001111010", "10100111100", "10010111100", "10010011110", "10111100100",
        "10011110100", "10011110010", "11110100100", "11110010100", "11110010010", "11011011110",
        "11011110110", "11110110110", "10101111000", "10100011110", "10001011110", "10111101000",
        "10111100010", "11110101000", "11110100010", "10111011110", "10111101110", "11101011110",
        "11110101110", "11010000100", "11010010000", "11010011100", "1100011101011"
    };

        // Map ASCII characters to Code 128 (simplified for demo purposes)
        static readonly int[] Code128Map = {
        0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19,
        20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37,
        38, 39, 40, 41, 42, 43, 44, 45, 46, 47, 48, 49, 50, 51, 52, 53, 54, 55,
        56, 57, 58, 59, 60, 61, 62, 63, 64, 65, 66, 67, 68, 69, 70, 71, 72, 73,
        74, 75, 76, 77, 78, 79, 80, 81, 82, 83, 84, 85, 86, 87, 88, 89, 90, 91,
        92, 93, 94, 95, 96, 97, 98, 99, 100, 101, 102, 103, 104, 105
    };


        private string EncodeCode128(string text)
        {
            string encoded = "11010000100"; // Start Code B (ASCII 104)
            int checksum = 104;

            for (int i = 0; i < text.Length; i++)
            {
                int index = text[i] - 32; // Adjust ASCII value to Code 128 character set
                encoded += Code128Patterns[index];
                checksum += index * (i + 1);
            }

            checksum %= 103;
            encoded += Code128Patterns[checksum]; // Add checksum pattern
            encoded += "1100011101011"; // Stop Code

            return encoded;
        }

        static Bitmap GenerateBarcode(string encodedText)
        {
            int barWidth = 2; // Width of each bar
            int height = 100; // Height of the barcode
            int width = encodedText.Length * barWidth;

            Bitmap bitmap = new Bitmap(width, height);
            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                graphics.Clear(Color.White);

                for (int i = 0; i < encodedText.Length; i++)
                {
                    Color color = encodedText[i] == '1' ? Color.Black : Color.White;
                    using (Brush brush = new SolidBrush(color))
                    {
                        graphics.FillRectangle(brush, i * barWidth, 0, barWidth, height);
                    }
                }
            }

            return bitmap;
        }



    }
}