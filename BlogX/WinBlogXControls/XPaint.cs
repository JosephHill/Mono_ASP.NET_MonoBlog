namespace Anderson.Chris.BlogX.WindowsClient
{
    using System;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.Drawing.Drawing2D;

    public class XPaint
    {
        static Bitmap xgem;

        private XPaint() {}

        static void EnsureGem()
        {
            if (xgem == null)
            {
                xgem = new Bitmap(typeof(XPaint), "xgem.png");
            }
        }

        public static void PaintFormBackground(Graphics target, Rectangle bounds, XPaintTheme theme)
        {
            theme.EnsureTheme(bounds);
            target.FillRectangle(theme.Background, bounds);
        }

        public static void PaintXGem(Graphics target, int x, int y)
        {
            EnsureGem();
            target.DrawImageUnscaled(xgem, x, y);
        }
    }

    public class XPaintTheme
    {
        Brush background;
        Rectangle sizeCalc = new Rectangle();

        public Brush Background { get { return background; } }

        public void EnsureTheme(Rectangle bounds)
        {
            if (bounds != sizeCalc || background == null)
            {
                sizeCalc = bounds;
                LinearGradientBrush bg = new LinearGradientBrush(bounds, Color.AliceBlue, Color.Navy, 90f);
                ColorBlend blend = new ColorBlend();
                blend.Colors = new Color[] { Color.Navy, Color.AliceBlue, Color.Navy, Color.Blue, Color.AliceBlue };
                blend.Positions = new float[] { 0f, .01f, .2f, .9f, 1f  };
                bg.InterpolationColors = blend;
                background = bg;

            }
        }
    }
}
