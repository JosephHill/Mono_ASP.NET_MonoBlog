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
    using Anderson.Chris.BlogX.Runtime;

    /// <summary>
    /// Summary description for BlogXEditting.
    /// </summary>
    [WebService(Namespace="http://www.simplegeek.com")]
    public class BlogXEditing : System.Web.Services.WebService
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

        public BlogXEditing()
        {
            BlogXData.Resolver = new ResolveFileCallback(ResolvePath);
            data = new BlogXData();
            data.Days.Sort(new DaySorter());

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
        public bool CanEdit(string username, string password)
        {
            try
            {
                if (HttpContext.Current.Request.IsAuthenticated)
                {
                    UserToken token = SiteSecurity.GetToken(User.Identity.Name);
                    if (token.Role == "admin") return true;
                }

                return SiteSecurity.Login(username, password).Role == "admin";
            }
            catch
            {
                return false;
            }
        }

        [WebMethod]
        public void DeleteEntry(string entryId, string username, string password)
        {
            if (SiteSecurity.Login(username, password).Role != "admin")
            {
                throw new Exception("Invalid Password");
            }
            EntryIdCache ecache = new EntryIdCache();
            ecache.Ensure(data);

            DateTime found = new DateTime();

            foreach (EntryIdCacheEntry ece in ecache.Entries)
            {
                if (ece.EntryId == entryId)
                {
                    found = ece.Date;
                    break;
                }
            }

            foreach (DayEntry day in data.Days)
            {
                if (day.Date == found)
                {
                    day.Load();

                    for (int i=0; i<day.Entries.Count; i++)
                    {
                        if (day.Entries[i].EntryId == entryId)
                        {
                            day.Entries.RemoveAt(i);
                            day.Save();
                            BlogXUtils.PingWeblogsCom();
                            return;
                        }
                    }
                }
            }
        }

        [WebMethod]
        public string UpdateEntry(Entry entry, string username, string password)
        {
            if (SiteSecurity.Login(username, password).Role != "admin")
            {
                throw new Exception("Invalid Password");
            }
            foreach (DayEntry day in data.Days)
            {
                if (day.Date == entry.Created.Date)
                {
                    day.Load();
                    
                    foreach (Entry found in day.Entries)
                    {
                        if (found.EntryId == entry.EntryId)
                        {
                            found.Categories = entry.Categories;
                            found.Content = entry.Content;
                            found.Title = entry.Title;
                            found.Description = entry.Description;
                            found.Modify();

                            day.Save();
                            data.IncrementEntryChange();
                            BlogXUtils.PingWeblogsCom();

                            return entry.EntryId;
                        }
                    }
                }
            }

            return "not found";
        }

        [WebMethod]
        public string CreateEntry(Entry entry, string username, string password)
        {
            if (SiteSecurity.Login(username, password).Role != "admin")
            {
                throw new Exception("Invalid Password");
            }
            bool added = false;

            // ensure that the entryId was filled in
            //
            if (entry.EntryId == null || entry.EntryId.Length == 0)
            {
                entry.EntryId = Guid.NewGuid().ToString();
            }

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

            return entry.EntryId;
        }
    }
}
