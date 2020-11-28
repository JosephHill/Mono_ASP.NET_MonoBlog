using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Web;
using System.Web.Services;

namespace Anderson.Chris.BlogX.WebClient
{
    /// <summary>
    /// Summary description for VerifyImageGen.
    /// </summary>
    public class VerifyImageGen : System.Web.IHttpHandler
    {
        public bool IsReusable { get { return true; } } 

        public static string WordFromCode(int code)
        {
            return SiteConfig.GetCommentWords()[code];
        }

        public void ProcessRequest(HttpContext ctx) 
        { 
            ctx.Response.ContentType = "image/jpeg";
            ctx.Response.Clear();
            
            int width = 200, height = 40;

            Bitmap bmp = new Bitmap(width, height,PixelFormat.Format24bppRgb);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                using (LinearGradientBrush b = new LinearGradientBrush(new Rectangle(0, 0, width, height),
                           Color.FromArgb(30, 30, 30),
                           Color.White,
                           90.0f))
                {
                    g.FillRectangle(b, 0, 0, width, height);
                }

                for (int x=0; x<width; x+=10)
                {
                    g.DrawLine(Pens.Black, x, 0, x, height);
                }
                for (int y=0; y<height; y+=10)
                {
                    g.DrawLine(Pens.Black, 0, y, width, y);
                }
                string word = WordFromCode(int.Parse(ctx.Request.QueryString["code"]));
                using (StringFormat fmt = new StringFormat())
                {
                    using (Brush b = new SolidBrush(Color.FromArgb(190, 255, 255, 255)))
                    {
                        fmt.Alignment = StringAlignment.Center;
                        fmt.LineAlignment = StringAlignment.Center;
                        g.DrawString(word, new Font("Times New Roman", 14.0f, FontStyle.Bold), b, new Rectangle(0, 0, width, height), fmt);
                    }
                }
            }

            bmp.Save(ctx.Response.OutputStream, ImageFormat.Jpeg);
            ctx.Response.Flush();
        } 
    }
}
