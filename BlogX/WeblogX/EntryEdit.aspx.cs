namespace Anderson.Chris.BlogX.WebClient
{
    using System;
    using System.Text;
    using System.Collections;
    using System.Configuration;
    using System.Web.Configuration;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Web;
    using System.Web.SessionState;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using System.Web.UI.HtmlControls;
    using Anderson.Chris.BlogX.Runtime;

    /// <summary>
    /// Summary description for EntryEdit.
    /// </summary>
    public class EntryEdit : System.Web.UI.Page
    {
        protected System.Web.UI.WebControls.TextBox entryTitle;
        protected System.Web.UI.WebControls.Label Label1;
        protected System.Web.UI.WebControls.Button save;
        protected System.Web.UI.WebControls.TextBox entryAbstract;
        protected System.Web.UI.WebControls.Label Label2;
        protected System.Web.UI.WebControls.TextBox entryContent;
        protected System.Web.UI.WebControls.TextBox entryCategories;

        class DaySorter : IComparer
        {
            public int Compare(object left, object right)
            {
                return ((DayEntry)right).Date.CompareTo(((DayEntry)left).Date);
            }
        }
        class EntrySorter : IComparer
        {
            public int Compare(object left, object right)
            {
                return ((Entry)right).Created.CompareTo(((Entry)left).Created);
            }
        }
        string ResolvePath(string file)
        {
            return Server.MapPath(SiteConfig.GetSiteConfig().ContentDir + file);
        }
 
        
        private void Page_Load(object sender, System.EventArgs e)
        {
            if (SiteSecurity.IsInRole("admin") == false) 
            {
                Response.Redirect("~/FormatPage.aspx?path=SiteConfig/accessdenied.format.html");
            }
            // Put user code to initialize the page here
        }

		#region Web Form Designer generated code
        override protected void OnInit(EventArgs e)
        {
            BlogXData.Resolver = new ResolveFileCallback(ResolvePath);

            //
            // CODEGEN: This call is required by the ASP.NET Web Form Designer.
            //
            InitializeComponent();
            base.OnInit(e);
        }
		
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {    
            this.save.Click += new System.EventHandler(this.save_Click);
            this.Load += new System.EventHandler(this.Page_Load);

        }
		#endregion

        private void save_Click(object sender, System.EventArgs e)
        {
            if (SiteSecurity.IsInRole("admin"))
            {
                BlogXData data = new BlogXData();

                bool added = false;

                Entry entry = new Entry();
                entry.Initialize();
                entry.Title = entryTitle.Text;
                entry.Description = entryAbstract.Text;
                entry.Content = entryContent.Text;
                entry.Categories = entryCategories.Text;

                foreach (DayEntry day in data.Days)
                {
                    if (day.Date == entry.Created.Date)
                    {
                        added = true;
                        day.Load();
                        day.Entries.Add(entry);
                        day.Save();
                        data.IncrementEntryChange();
                        BlogXUtils.PingWeblogsCom();
                        break;
                    }
                }
                if (!added)
                {
                    DayEntry newDay = new DayEntry();
                    newDay.Date = entry.Created.Date;
                    newDay.Entries.Add(entry);
                    newDay.Save();
                            
                    data.IncrementEntryChange();
                    BlogXUtils.PingWeblogsCom();
                }

                entryTitle.Text = "";
                entryAbstract.Text = "";
                entryContent.Text = "";
                entryCategories.Text = "";

                Response.Redirect("default.aspx", false);
            }
        }

    }
}
