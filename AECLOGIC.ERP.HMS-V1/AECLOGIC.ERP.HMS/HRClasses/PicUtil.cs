using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;

/// <summary>
/// Summary description for PicUtil
/// </summary>
public class PicUtil
{
	public PicUtil()
	{
		
	}

    public static void ConvertPic(string SourceFilename,int Width,int Height,String SaveAs)
        {
            byte[] content;
            try
            {
                System.Drawing.Image imgOut = null;
                Bitmap loBMP = new Bitmap(SourceFilename);
 
               
 
 
                System.Drawing.Imaging.ImageFormat loFormat = loBMP.RawFormat;
                if (loBMP.Width <= Width && loBMP.Height <= Height)
                {
                    imgOut = loBMP.GetThumbnailImage(loBMP.Width, loBMP.Height, null, IntPtr.Zero);
                }
                else
                {
                    imgOut = loBMP.GetThumbnailImage(Width, Height, null, IntPtr.Zero);
                }
 
                System.Drawing.Image imgPhoto = imgOut;
               
                Bitmap bmPhoto = new Bitmap(Width, Height, PixelFormat.Format16bppRgb565);
                bmPhoto.SetResolution(imgPhoto.HorizontalResolution, imgPhoto.VerticalResolution);
                Graphics grPhoto = Graphics.FromImage(bmPhoto);
 
                System.Drawing.Image imgone = imgOut;
                
                grPhoto.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighSpeed;
                grPhoto.DrawImage(imgPhoto, new Rectangle(0, 0, Width, Height), 0, 0, Width, Height, GraphicsUnit.Pixel);
 
                SizeF crSize = new SizeF();
                int yPixlesFromBottom = (int)(Height * 0.5);
                float yPosFromBottom = ((Height - yPixlesFromBottom) - (crSize.Height / 2));
                float xCenterOfImg = (Width / 2);
                StringFormat StrFormat = new StringFormat();
                StrFormat.Alignment = StringAlignment.Center;
                Bitmap bmimg = new Bitmap(bmPhoto);
                bmimg.SetResolution(imgPhoto.HorizontalResolution, imgPhoto.VerticalResolution);
                Graphics grimg = Graphics.FromImage(bmimg);
                ImageAttributes imageAttributes = new ImageAttributes();
                ColorMap colorMap = new ColorMap();
                colorMap.OldColor = Color.FromArgb(255, 0, 255, 0);
                colorMap.NewColor = Color.FromArgb(0, 0, 0, 0);
                ColorMap[] remapTable = { colorMap };
                imageAttributes.SetRemapTable(remapTable, ColorAdjustType.Bitmap);
                float[][] colorMatrixElements = {
 
                                                new float[] {0.0f,  0.0f,  0.0f,  0.0f, 0.0f},      
                                                new float[] {0.0f,  0.0f,  0.0f,  0.0f, 0.0f},       
                                                new float[] {0.0f,  0.0f,  0.0f,  0.0f, 0.0f},       
                                                new float[] {0.0f,  0.0f,  0.0f,  0.0f, 0.0f},       
                                                new float[] {0.0f,  0.0f,  0.0f,  0.0f, 0.0f}
                                            };
 
                ColorMatrix wmColorMatrix = new ColorMatrix(colorMatrixElements);
                imageAttributes.SetColorMatrix(wmColorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
                int xPosOfWm = ((Width - Width));
                int yPosOfWm = 0;
                grimg.DrawImage(imgone, new Rectangle(xPosOfWm, yPosOfWm, Width, Height), 0, 0, Width, Height, GraphicsUnit.Pixel, imageAttributes);
                imgPhoto = bmimg;
                grPhoto.Dispose();
                grimg.Dispose();
                loBMP.Dispose();
                FileInfo fi = new FileInfo(SaveAs);
                if (fi.Exists)
                    fi.Delete();
                imgPhoto.Save(SaveAs, ImageFormat.Jpeg);
                imgPhoto.Dispose();
                imgone.Dispose();
                loBMP.Dispose();
                loBMP = null;
 
 
               
            }
            catch (Exception ex)
            {
                throw ex;
            }
          
 
        }
 
        protected static byte[] ReadFileToByteArray(string fileName)
        {
           
                FileStream fileStream = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Read);
                long len;
                len = fileStream.Length;
                Byte[] fileAsByte = new Byte[len];
                fileStream.Read(fileAsByte, 0, fileAsByte.Length);
                MemoryStream memoryStream = new MemoryStream(fileAsByte);
                byte[] content = memoryStream.ToArray();
                fileStream.Dispose();
                return content;
            
        }
}

