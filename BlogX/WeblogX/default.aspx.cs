//=-------
// Copyright 2003, Chris Anderson
// 
//   Provided as is, with no warrenty, etc.
//   License is granted to use, copy, modify, 
//   with or without credit to me, just don't
//   blame me if it doesn't work.
//=-------
namespace Anderson.Chris.BlogX.WebClient
{
    using System;
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
    /// Summary description for WebForm1.
    /// </summary>
    public class HomePage : System.Web.UI.Page
    {
        protected System.Web.UI.WebControls.Calendar blogCal;
        protected CategoryList categoryList;
        protected System.Web.UI.HtmlControls.HtmlGenericControl rightBar;
        protected HtmlGenericControl title;
    
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

        protected System.Web.UI.WebControls.PlaceHolder blogSidebar;
        protected System.Web.UI.WebControls.PlaceHolder blogContent;
 
        BlogXData data = new BlogXData();

        private void Page_Load(object sender, System.EventArgs e)
        {
            title.Controls.Clear();
            title.Controls.Add(new LiteralControl(SiteConfig.GetSiteConfig().Title));

            BlogXUtils.TrackReferrer(Request, Server);
            blogCal.SelectedDate = StartDate;
            blogCal.VisibleDate = StartDate;
        }

		#region Web Form Designer generated code
        override protected void OnInit(EventArgs e)
        {
            BlogXData.Resolver = new ResolveFileCallback(ResolvePath);
            data.Days.Sort(new DaySorter());

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
            this.blogCal.DayRender += new System.Web.UI.WebControls.DayRenderEventHandler(this.blogCal_DayRender);
            this.Load += new System.EventHandler(this.Page_Load);
            this.Init += new System.EventHandler(this.HomePage_Init);
            this.PreRender += new System.EventHandler(this.WebForm1_PreRender);

        }
		#endregion

        private void blogCal_DayRender(object sender, System.Web.UI.WebControls.DayRenderEventArgs e)
        {
            e.Day.IsSelectable = false;
            DateTime start = StartDate.Date;

            foreach (DayEntry day in data.Days)
            {
                if (e.Day.Date == day.Date.Date)
                {
                    e.Cell.Controls.Clear();
                    HyperLink link = new HyperLink();
                    link.Text = e.Day.DayNumberText;
                    link.NavigateUrl = "default.aspx?date=" + e.Day.Date.ToString("s");
                    e.Cell.Controls.Add(link);
                    break;
                }
            }        
        }

        DateTime StartDate
        {
            get
            {
                DateTime start = DateTime.Now;
            
                if (Request.QueryString["date"] != null && Request.QueryString["date"].Length > 0)
                {
                    try
                    {
                        start = DateTime.Parse(Request.QueryString["date"]);
                    }
                    catch 
                    {
                        start = DateTime.Now;
                    }
                }
                return start;
            }
        }

        private void WebForm1_PreRender(object sender, System.EventArgs e)
        {
            Control root = blogContent;
            int entryCount = 0;
            int dayCount = 0;
            DateTime start = StartDate;
            HtmlGenericControl date = null;

            SiteConfig config = SiteConfig.GetSiteConfig();

            categoryList.Data = data;
            
            bool needHeader = true;

            foreach (DayEntry day in data.Days)
            {
                if (day.Date <= start)
                {
                    if (dayCount < 7) 
                    {
                        day.Load();
                        day.Entries.Sort(new EntrySorter());
                        dayCount++;
                        DayExtra extra = data.GetDayExtra(day.Date);
                        needHeader = true;

                        foreach (Entry entry in day.Entries)
                        {
                            if (needHeader) 
                            {
                                date = new HtmlGenericControl("div");
                                date.Attributes["class"] = "date";
                                date.ID = day.Date.ToString("yyyy-MM-dd");

                                HtmlGenericControl dateHeader = new HtmlGenericControl("h2");
                                dateHeader.Attributes["class"] = "dateHeader";
                                dateHeader.Controls.Add(new LiteralControl(day.Date.ToString("MMM dd, yyyy")));
                                date.Controls.Add(dateHeader);

                                root.Controls.Add(date);
                                needHeader = false;
                            }
                            entryCount++;

                            EntryView view = (EntryView)LoadControl("EntryView.ascx");
                            view.Data = data;
                            view.EntryTitleAsLink = config.EntryTitleAsLink;
                            view.Extra = extra;
                            view.Entry = entry;
                            date.Controls.Add(view);
                        }

                    }
                }
            }

        
        }

        private void HomePage_Init(object sender, System.EventArgs e)
        {
            if (SiteSecurity.IsInRole("admin"))
            {
                AdminNavBar nav = (AdminNavBar)LoadControl("AdminNavBar.ascx");
                rightBar.Controls.Add(nav);
                rightBar.Controls.Add(new LiteralControl("<br />"));
            }
            if (!Request.IsAuthenticated)
            {
                SignIn signIn = (SignIn)LoadControl("SignIn.ascx");
                rightBar.Controls.Add(signIn);
                rightBar.Controls.Add(new LiteralControl("<br />"));
            }
        }
    }
}
