namespace Anderson.Chris.BlogX.WindowsClient
{
    using System;
    using System.ComponentModel;
    using System.Windows.Forms;
    using System.Drawing;

    /// <summary>
    /// Summary description for XButton.
    /// </summary>
    public class XButton : Button
    {
        public XButton()
        {
            BackColor = Color.FromArgb(120, Color.White);
            FlatStyle = FlatStyle.Flat;
        }

        public bool ShouldSerializeBackColor()
        {
            return (BackColor == Color.FromArgb(120, Color.White));
        }

        [DefaultValue(FlatStyle.Flat)]
        public new System.Windows.Forms.FlatStyle FlatStyle
        {
            get
            {
                return base.FlatStyle;
            }
            set
            {
                base.FlatStyle = value;
            }
        }
    }
}
