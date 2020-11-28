//=-------
// Copyright 2003, Chris Anderson
// 
//   Provided as is, with no warrenty, etc.
//   License is granted to use, copy, modify, 
//   with or without credit to me, just don't
//   blame me if it doesn't work.
//=-------
namespace Anderson.Chris.BlogX.WindowsClient
{
    using System;
    using System.IO;
    using System.Xml.Serialization;
    using System.Collections;
    using System.Windows.Forms;
    using System.Configuration;
    using Anderson.Chris.BlogX.Runtime;

    /// <summary>
    /// Summary description for App.
    /// </summary>
    class AppMain
    {
        [STAThread]
        static void Main(string[] args) 
        {
            Application.Run(new App());
        }
    }

    class App : ApplicationContext
    {
        // UNDONE: make this per thread?
        static App current;
        internal static App Current { get { return current; } }

        public App()
        {
            current = this;
            WinBlogXConfig config = WinBlogXConfig.Load();
            if (config.StartDialog.Mru.Count > 0)
            {
                string path = config.StartDialog.Mru[0];
                if (File.Exists(path))
                {
                    WinBlogXProject proj = null;

                    XmlSerializer ser = new XmlSerializer(typeof(WinBlogXProject));
                    using (StreamReader reader = new StreamReader(path))
                    {
                        proj = ser.Deserialize(reader) as WinBlogXProject;
                    }
                    if (proj != null)
                    {
                        proj.Upgrade();

                        SimpleBrowser browser = new SimpleBrowser(proj);
                        browser.Show();
                        return;
                    }
                }
            }

            new Start().Show();
        }

        IList forms = new ArrayList();
        public void AddForm(Form form)
        {
            forms.Add(form);
        }
        public void RemoveForm(Form form)
        {
            forms.Remove(form);
            if (forms.Count == 0)
            {
                ExitThread();
            }
        }
    }

    public class BlogXForm : Form
    {
        XPaintTheme theme = new XPaintTheme();

        public BlogXForm()
        {
            SetStyle(ControlStyles.Opaque | ControlStyles.AllPaintingInWmPaint | ControlStyles.DoubleBuffer | ControlStyles.ResizeRedraw, true);
            if (App.Current != null)
            {
                App.Current.AddForm(this);
            }
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (disposing)
            {
                if (App.Current != null)
                {
                    App.Current.RemoveForm(this);
                }
            }
        }

        protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
        {
            XPaint.PaintFormBackground(e.Graphics, ClientRectangle, theme);
        }
    }
}
