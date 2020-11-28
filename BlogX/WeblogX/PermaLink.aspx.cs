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
    using System.Configuration;
    using System.Collections;
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
    /// Summary description for PermaLink.
    /// </summary>
    public class PermaLink : System.Web.UI.Page
    {
        protected HtmlGenericControl content;
    
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
 
        BlogXData data = new BlogXData();

        private void Page_Load(object sender, System.EventArgs e)
        {
            BlogXUtils.TrackReferrer(Request, Server);

            EntryIdCache ecache = new EntryIdCache();
            ecache.Ensure(data);

            Control root = content;
            string entryId = Request.QueryString["guid"];
            if (entryId != null && entryId.Length > 0)
            {
                entryId = entryId.ToUpper();
            }
            else
            {
                entryId = Request.PathInfo.Substring(1).ToUpper();
            }
            DateTime found = new DateTime();

            foreach (EntryIdCacheEntry ece in ecache.Entries)
            {
                if (ece.EntryId.ToUpper() == entryId)
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
                    DayExtra extra = data.GetDayExtra(day.Date);
                    foreach (Entry entry in day.Entries)
                    {
                        if (entryId.ToUpper() == entry.EntryId.ToUpper())
                        {
                            EntryView view = (EntryView)LoadControl("EntryView.ascx");
                            view.DateFormat = "MM/dd/yyyy h:mm tt";
                            view.Data = data;
                            view.Extra = extra;
                            view.Entry = entry;
                            root.Controls.Add(view);

                            break;
                        }
                    }
                }
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

        }
		#endregion
    }
}
