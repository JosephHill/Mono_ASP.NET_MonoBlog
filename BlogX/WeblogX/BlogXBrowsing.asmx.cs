namespace Anderson.Chris.BlogX.WebClient
{
    using System;
    using System.Configuration;
    using System.Collections;
    using System.ComponentModel;
    using System.Data;
    using System.Diagnostics;
    using System.Web;
    using System.Web.Services;
    using System.IO;
    using System.Xml;
    using System.Xml.Serialization;
    using Anderson.Chris.BlogX.Runtime;

    /// <summary>
    /// Summary description for BlogXBrowsing.
    /// </summary>
    [WebService(Namespace="http://www.simplegeek.com")]
    public class BlogXBrowsing : System.Web.Services.WebService
    {
        BlogXData data;
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

        public BlogXBrowsing()
        {
            BlogXData.Resolver = new ResolveFileCallback(ResolvePath);
            data = new BlogXData();
//HACK: Removed because of runtime error (in Mono?)
//            data.Days.Sort(new DaySorter());

            //CODEGEN: This call is required by the ASP.NET Web Services Designer
            InitializeComponent();
        }

        string ResolvePath(string file)
        {
            return Server.MapPath(SiteConfig.GetSiteConfig().ContentDir + file);
        }
		#region Component Designer generated code
		
        //Required by the Web Services Designer 
        private IContainer components = null;
				
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose( bool disposing )
        {
            if(disposing && components != null)
            {
                components.Dispose();
            }
            base.Dispose(disposing);		
        }
		
		#endregion

        [WebMethod]
        public string GetRssUrl()
        {
            return new Uri(new Uri(SiteConfig.GetSiteConfig().Root),"BlogXBrowsing.asmx/GetRss?").ToString();
        }

        [WebMethod]
        public XmlNode GetRss()
        {
            int maxDays = SiteConfig.GetSiteConfig().RssDayCount;
            return GetRssWithDayCount(maxDays);
        }

        [WebMethod]
        public XmlNode GetRssCategory(string categoryName)
        {
            return GetRssCore(categoryName, 0);
        }

        [WebMethod]
        public XmlNode GetRssWithDayCount(int maxDayCount)
        {
            return GetRssCore(null, maxDayCount);
        }

        private EntryCollection BuildEntries(BlogXData data, string category, int maxDayCount)
        {
            EntryCollection entryList = new EntryCollection();

            if (category != null)
            {
                int entryCount = SiteConfig.GetSiteConfig().RssEntryCount;
                category = category.ToUpper();
                CategoryCache cache = new CategoryCache();
                cache.Ensure(data);

                foreach (CategoryCacheEntry catEntry in cache.Entries)
                {
                    if (catEntry.Name.ToUpper() == category)
                    {
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
                                            entryCount--;
                                            entryList.Add(entry);
                                            if (entryCount < 0) break;
                                        }
                                    }
                                }
                                if (entryCount < 0) break;

                            } // foreach (DayEntry day
                            if (entryCount < 0) break;

                        } // foreach (CategoryCacheEntryDetail
                    }

                    if (entryCount < 0) break;
                } // foreach (CategoryCacheEntry
            }
            else
            {
                int dayCount = 0;
                data.Days.Sort(new DaySorter());
                foreach (DayEntry day in data.Days)
                {
                    day.Load();
                    dayCount++;

                    foreach (Entry entry in day.Entries)
                    {
                        entryList.Add(entry);
                    }

                    if (dayCount >= maxDayCount) break;
                }
            }
            entryList.Sort(new EntrySorter());
            return entryList;
        }

        private XmlNode GetRssCore(string category, int maxDayCount)
        {
            BlogXData.Resolver = new ResolveFileCallback(ResolvePath);

            RssRoot r = new RssRoot();
            RssChannel ch = new RssChannel();
            SiteConfig config = SiteConfig.GetSiteConfig();
            ch.Title = config.Title;
            ch.Link = config.Root;
            ch.Copyright = config.Copyright;
            ch.ManagingEditor = config.Contact;
            ch.WebMaster = config.Contact;
            r.Channels.Add(ch);

            BlogXData data = new BlogXData();
            EntryCollection entries = BuildEntries(data, category, maxDayCount);

            using (StreamWriter sw = new StreamWriter(Server.MapPath(Path.Combine("logs","dump.txt"))))
            {
                DateTime latest = new DateTime(0);

                foreach (Entry entry in entries)
                {
                    DateTime created = entry.Created;
                    if (created > latest) latest = created;
                }

                HttpRequest req = Context.Request;
                HttpResponse res = Context.Response;
                bool notModified = false;

                // Check the request's if-modified-since field. 
                // If it matches our last build date, then the
                // caller already has the most recent data and
                // we can abort the whole process with an 
                // error 304: Not Modified

                string ifModifiedSince = req.Headers["if-modified-since"];
                if (ifModifiedSince != null)
                {
                    DateTime modDate = latest;
                    modDate = new DateTime(modDate.Year, modDate.Month, modDate.Day, modDate.Hour, modDate.Minute, modDate.Second);
                    try 
                    { 
                        DateTime ifModDate = DateTime.Parse(ifModifiedSince);
                        notModified = (ifModDate == modDate);
                    }
                    catch{}
                }

                // Also check the request for an etag header and
                // see if it maches our date.  The rules are if
                // both headers are there, they both have to match.  But,
                // if only one header is there only it has to match.

                string etag = req.Headers["etag"];
                if (etag != null)
                {
                    notModified = (etag.Equals(latest.Ticks.ToString()));
                }

                if (notModified)
                {
                    res.StatusCode = 304;
                    res.SuppressContent = true;
                    return null;
                }

                // Either no one used if-modified-since or we have
                // new data.  Record the last modified time and
                // etag in the http header for next time.

                res.Cache.SetLastModified(latest);
                res.Cache.SetETag(latest.Ticks.ToString());
            }

            BlogXUtils.TrackReferrer(Context.Request, Server);

            foreach (Entry entry in entries)
            {
                RssItem item = new RssItem();
                item.Title = entry.Title;
                item.Guid = item.Link = new Uri(new Uri(config.Root), " PermaLink.aspx/" + entry.EntryId).ToString();
                item.Comments = new Uri(new Uri(config.Root), "CommentView.aspx/" + entry.EntryId).ToString();
                if (entry.Categories != null && entry.Categories.Length > 0)
                {
                    item.Category = entry.GetSplitCategories()[0];
                }
                item.PubDate = entry.Created.ToString("R");
                if (ch.LastBuildDate == null || ch.LastBuildDate.Length == 0)
                {
                    ch.LastBuildDate = item.PubDate;
                }
                if (entry.Description != null && entry.Description.Trim().Length > 0)
                {
                    item.Description = entry.Description;
                }
                else
                {
                    item.Description = entry.Content;
                }

                XmlDocument doc2 = new XmlDocument();
                try
                {
                    doc2.LoadXml(entry.Content);
                    item.Body = (XmlElement)doc2.SelectSingleNode("//*[local-name() = 'body'][namespace-uri()='http://www.w3.org/1999/xhtml']");
                }
                catch {}

                ch.Items.Add(item);
            }

            XmlSerializer ser = new XmlSerializer(typeof(RssRoot));
            StringWriter writer = new StringWriter();
            ser.Serialize(writer, r);

            XmlDocument doc = new XmlDocument();
            doc.Load(new StringReader(writer.ToString()));

            return doc.FirstChild.NextSibling;
        }

        [WebMethod]
        public LogDataItemCollection GetReferrerLog(DateTime date)
        {
            return BlogXUtils.GetReferrerLog(date);
        }

        [WebMethod]
        public string[] GetCategoryList()
        {
            CategoryCache cache = new CategoryCache();
            cache.Ensure(data);

            string[] cats = new string[cache.Entries.Count];
            for (int i=0; i<cats.Length; i++)
            {
                cats[i] = cache.Entries[i].Name;
            }

            return cats;
        }

        [WebMethod]
        public EntryCollection GetCategoryEntries(string categoryName)
        {
            CategoryCache cache = new CategoryCache();
            cache.Ensure(data);

            EntryCollection entryList = new EntryCollection();

            foreach (CategoryCacheEntry catEntry in cache.Entries)
            {
                if (catEntry.Name.ToUpper() == categoryName)
                {
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
            return entryList;
        }

        [WebMethod]
        public DateTime[] GetDaysWithEntries()
        {
            DateTime[] days = new DateTime[data.Days.Count];
            for (int i=0; i<days.Length; i++)
            {
                days[i] = data.Days[i].Date;
            }
            return days;
        }

        [WebMethod]
        public DayEntry GetDayEntry(DateTime date)
        {
            foreach (DayEntry entry in data.Days)
            {
                if (entry.Date == date)
                {
                    entry.Load();
                    entry.Entries.Sort(new EntrySorter());
                    return entry;
                }
            }
            return null;
        }

        [WebMethod]
        public DayExtra GetDayExtra(DateTime date)
        {
            DayExtra ex = data.GetDayExtra(date);
            ex.Load();
            return ex;
        }
    }
}


/*
fix:

namespace LiveCast.Article
{
    using LiveCast.Kernel;
    using LiveCast.Storage;
    using System;
    using System.Configuration;
    using System.Collections;
    using System.ComponentModel;
    using System.Data;
    using System.Diagnostics;
    using System.Web;
    using System.Web.Services;
    using System.IO;
    using System.Xml;
    using System.Xml.Serialization;

    /// <summary>
    ///     Rss is an XML syndication feed for articles.
    /// </summary>
    [WebService()]
    public class Rss : System.Web.Services.WebService
    {
        public Rss()
        {
            //CODEGEN: This call is required by the ASP.NET Web Services Designer
            InitializeComponent();
        }

		#region Component Designer generated code
		
        //Required by the Web Services Designer 
        private IContainer components = null;
				
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose( bool disposing )
        {
            if(disposing && components != null)
            {
                components.Dispose();
            }
            base.Dispose(disposing);		
        }
		
		#endregion

        [WebMethod]
        public XmlNode GetRss(int document)
        {
            const string defaultPage = "default.aspx";

            Documents docs = (Documents)Services.GetService(Context, typeof(Documents));
            Navigation nav = (Navigation)Services.GetService(Context, typeof(Navigation));
            MarkedDocuments markedDocs = (MarkedDocuments)Services.GetService(Context, typeof(MarkedDocuments));
            Document doc = null;

            string title = SiteConfig.Current.ApplicationName;

            // Our goal here is to find a document collection to walk.  We look to see
            // if we were passed in a valid doc.  if so, we then check to see if
            // that document is the "main" page document.  if so, we discard it.  
            // If no document can be found, we use the global collection.

            doc = docs.FromID(document);
            if (doc != null)
            {
                MarkedDocument markedDoc = markedDocs.FromName(MarkedDocumentNames.Default);
                if (markedDoc == null || !markedDoc.Document.Equals(doc))
                {
                    docs = doc.Children;
                    title = string.Format("{0} - {1}", title, doc.Title);
                }
            }

            docs.ResetOptions();
            docs.SortOrder = DocumentSortOrder.CreationDate;
            docs.FlattenChildren = true;

            // Configure the RSS XML root and RssChannel.
            //
            RssRoot r = new RssRoot();
            RssChannel ch = new RssChannel();

            ch.Title = title;
            ch.Copyright = SiteConfig.Current.Copyright;

            Users users = (Users)Services.GetService(Context, typeof(Users));
            User admin = users.FromName("administrator");
            if (admin != null && admin.Email.Length > 0)
            {
                ch.ManagingEditor = admin.Email;
                ch.WebMaster = admin.Email;
            }

            if (doc != null && doc.User.Email.Length > 0)
            {
                ch.ManagingEditor = doc.User.Email;
            }

            string docUrl = defaultPage;
            if (doc != null) 
            {
                docUrl = nav.CreateUrl(docUrl, doc);
            }
            ch.Link = nav.CreateAbsoluteUrl(docUrl);
            r.Channels.Add(ch);

            // Now start walking documents.
            //
            int maxItems = SiteConfig.Current.SyndicationMaxItems;
            int itemCount = 0;
            //int maxDays = SiteConfig.Current.SummaryMaxDays;

            ArticleManager articles = (ArticleManager)Services.GetService(Context, typeof(ArticleManager));

            foreach(Document d in docs)
            {
                if (++itemCount > maxItems) 
                {
                    break;
                }

                Article article = articles.FromDocument(d);

                // If this article has children, then skip it.
                //
                article.ArticleDocument.Children.ResetOptions();
                if (article.ArticleDocument.Children.Count > 0)
                {
                    continue;
                }

                RssItem item = new RssItem();
                item.Title = article.Title;
                item.Guid = item.Link = nav.CreateAbsoluteUrl(nav.CreateUrl(defaultPage, d));

                item.Category = d.Parent.Title;
                item.PubDate = d.CreationDate.ToString("R");

                if (ch.LastBuildDate == null || ch.LastBuildDate.Length == 0)
                {
                    ch.LastBuildDate = item.PubDate;

                    HttpRequest req = Context.Request;
                    HttpResponse res = Context.Response;
                    bool notModified = false;

                    // Check the request's if-modified-since field. 
                    // If it matches our last build date, then the
                    // caller already has the most recent data and
                    // we can abort the whole process with an 
                    // error 304: Not Modified

                    string ifModifiedSince = req.Headers["if-modified-since"];
                    if (ifModifiedSince != null)
                    {
                        DateTime modDate = d.ModifiedDate;
                        modDate = new DateTime(modDate.Year, modDate.Month, modDate.Day, modDate.Hour, modDate.Minute, modDate.Second);
                        try 
                        { 
                            DateTime ifModDate = DateTime.Parse(ifModifiedSince);
                            notModified = (ifModDate == modDate);
                        }
                        catch{}
                    }

                    // Also check the request for an etag header and
                    // see if it maches our date.  The rules are if
                    // both headers are there, they both have to match.  But,
                    // if only one header is there only it has to match.

                    string etag = req.Headers["etag"];
                    if (etag != null)
                    {
                        notModified = (etag.Equals(d.ModifiedDate.Ticks.ToString()));
                    }

                    if (notModified)
                    {
                        res.StatusCode = 304;
                        res.SuppressContent = true;
                        break;
                    }

                    // Either no one used if-modified-since or we have
                    // new data.  Record the last modified time and
                    // etag in the http header for next time.

                    res.Cache.SetLastModified(d.ModifiedDate);
                    res.Cache.SetETag(d.ModifiedDate.Ticks.ToString());
                }

                item.Description = article.Description;

                XmlDocument d2 = new XmlDocument();
                try
                {
                    string xhtmlDescription = string.Format("<body xmlns=\"http://www.w3.org/1999/xhtml\">{0}</body>", article.Content);
                    d2.LoadXml(xhtmlDescription);
                    item.Body = (XmlElement)d2.SelectSingleNode("//*[local-name() = 'body'][namespace-uri()='http://www.w3.org/1999/xhtml']");
                }
                catch
                {
                    // Body of this article doesn't conform to XHTML spec.  
                    // Just eat it.
                }

                ch.Items.Add(item);
            }

            XmlSerializer ser = new XmlSerializer(typeof(RssRoot));
            StringWriter writer = new StringWriter();
            ser.Serialize(writer, r);

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(new StringReader(writer.ToString()));

            return xmlDoc.FirstChild.NextSibling;
        }
    }
}

*/