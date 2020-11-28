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
    using System.ComponentModel;
    using System.Configuration;
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
    public class CategoryView : System.Web.UI.Page
    {
    
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

        protected System.Web.UI.HtmlControls.HtmlGenericControl content;
        protected System.Web.UI.HtmlControls.HtmlGenericControl title;
 
        BlogXData data = new BlogXData();

        private void Page_Load(object sender, System.EventArgs e)
        {
            BlogXUtils.TrackReferrer(Request, Server);
            if (!IsPostBack)
            {
            }

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
            this.Load += new System.EventHandler(this.Page_Load);
            this.PreRender += new System.EventHandler(this.WebForm1_PreRender);

        }
		#endregion

        private void WebForm1_PreRender(object sender, System.EventArgs e)
        {
            Control root = content;
            string category = Request.QueryString["category"];
            if (category != null && category.Length > 0)
            {
                category = category.ToUpper();
            }
            else
            {
                category = Request.PathInfo.Substring(1).ToUpper();
            }

            CategoryCache cache = new CategoryCache();
            cache.Ensure(data);

            EntryCollection entryList = new EntryCollection();

            foreach (CategoryCacheEntry catEntry in cache.Entries)
            {
                if (catEntry.Name.ToUpper() == category)
                {
                    title.Controls.Clear();
                    title.Controls.Add(new LiteralControl(catEntry.Name));

                    foreach (CategoryCacheEntryDetail detail in catEntry.EntryDetails)
                    {
                        foreach (DayEntry day in data.Days)
                        {
                            if (day.Date == detail.DayDate)
                            {
                                day.Load();
                                foreach (Entry entry in day.Entries)
                                {
                                    if (entry.EntryId == detail.EntryId)
                                    {
                                        entryList.Add(entry);
                                    }
                                }
                            }
                        }
                    }
                }
            }

            entryList.Sort(new EntrySorter());
            foreach (Entry entry in entryList)
            {
                EntryView view = (EntryView)LoadControl("EntryView.ascx");
                view.DateFormat = "MM/dd/yyyy h:mm tt";
                view.Data = data;
                // UNDONE : perf problem!
                view.Extra = data.GetDayExtra(entry.Created.Date);
                view.Entry = entry;
                root.Controls.Add(view);
            }
        }
    }
}
