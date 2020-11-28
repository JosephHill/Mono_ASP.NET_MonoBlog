namespace Anderson.Chris.BlogX.WebClient
{
    using System;
    using System.Configuration;
    using System.Data;
    using System.Drawing;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using System.Web.UI.HtmlControls;
    using System.Collections;
    using Anderson.Chris.BlogX.Runtime;

    /// <summary>
    ///		Summary description for EntryView.
    /// </summary>
    public abstract class EntryView : System.Web.UI.UserControl
    {
        Entry entry;
        DayExtra extra;
        BlogXData data;
        bool entryTitleAsLink = false;
        string dateFormat = "h:mm tt";

        public string DateFormat { get { return dateFormat; } set { dateFormat = value; } }
        public bool EntryTitleAsLink { get { return entryTitleAsLink; } set { entryTitleAsLink = value; } }

        public BlogXData Data
        {
            get
            {
                return data;
            }
            set
            {
                data = value;
            }
        }
        public Entry Entry 
        {
            get
            {
                return entry;
            }
            set
            {
                entry = value;
            }
        }
        public DayExtra Extra 
        {
            get
            {
                return extra;
            }
            set
            {
                extra = value;
            }
        }
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
            BlogXData.Resolver = new ResolveFileCallback(ResolvePath);
            // Put user code to initialize the page here
        }

		#region Web Form Designer generated code
        override protected void OnInit(EventArgs e)
        {
            //
            // CODEGEN: This call is required by the ASP.NET Web Form Designer.
            //
            InitializeComponent();
            base.OnInit(e);
        }
		
        ///		Required method for Designer support - do not modify
        ///		the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Load += new System.EventHandler(this.Page_Load);
            this.PreRender += new System.EventHandler(this.Page_PreRender);

        }
		#endregion

        private void Page_PreRender(object sender, System.EventArgs e)
        {
            Control root = this;
            HtmlGenericControl entryControl = new HtmlGenericControl("div");
            entryControl.Attributes["class"] = "entry";
            root.Controls.Add(entryControl);

            HtmlGenericControl entryTitle = new HtmlGenericControl("h3");
            entryTitle.Attributes["class"] = "entryTitle";
            if (EntryTitleAsLink)
            {
                HyperLink entryTitleLink = new HyperLink();
                entryTitleLink.NavigateUrl = BlogXUtils.RelativeToRoot("PermaLink.aspx/" + entry.EntryId);
                entryTitleLink.Text = entry.Title;
                entryTitle.Controls.Add(entryTitleLink);
            }
            else
            {
                entryTitle.Controls.Add(new LiteralControl(entry.Title));
            }
            entryControl.Controls.Add(entryTitle);

            HtmlGenericControl entryBody = new HtmlGenericControl("div");
            entryBody.Attributes["class"] = "entryBody";
            entryBody.Controls.Add(new LiteralControl(entry.Content));
            entryControl.Controls.Add(entryBody);

            HtmlGenericControl entryFooter = new HtmlGenericControl("p");
            entryFooter.Attributes["class"] = "entryFooter";
            entryControl.Controls.Add(entryFooter);

            HyperLink entryDate = new HyperLink();
            entryDate.CssClass = "permalink";
            entryDate.NavigateUrl = BlogXUtils.RelativeToRoot("PermaLink.aspx/" + entry.EntryId);
            entryDate.Text = entry.Created.ToString(dateFormat);
            entryFooter.Controls.Add(entryDate);


            if (SiteConfig.GetSiteConfig().AllowComments)
            {
                entryFooter.Controls.Add(new LiteralControl(" | "));

                extra.Load();
                CommentCollection entryComments = extra.GetCommentsFor(entry.EntryId);
                HtmlGenericControl comments = new HtmlGenericControl("span");
                comments.Attributes["class"] = "comments";

                if (entryComments.Count == 0)
                {
                    HyperLink comment = new HyperLink();
                    comment.Text = "Add comment";
                    comment.NavigateUrl = BlogXUtils.RelativeToRoot("CommentView.aspx/" + entry.EntryId);
                    comments.Controls.Add(comment);
                }
                else
                {
                    HyperLink comment = new HyperLink();
                    comment.Text = "Comments [" + entryComments.Count + "]";
                    comment.NavigateUrl = BlogXUtils.RelativeToRoot("CommentView.aspx/" + entry.EntryId);
                    comments.Controls.Add(comment);
                }
                entryFooter.Controls.Add(comments);
            }


            if (entry.Categories != null && entry.Categories.Length > 0)
            {
                entryFooter.Controls.Add(new LiteralControl(" | "));

                HtmlGenericControl categories = new HtmlGenericControl("span");
                categories.Attributes["class"] = "categories";

                foreach (string cat in entry.GetSplitCategories())
                {
                    categories.Controls.Add(new LiteralControl(" #"));

                    HyperLink category = new HyperLink();
                    category.Text = cat;
                    category.NavigateUrl = BlogXUtils.RelativeToRoot("CategoryView.aspx/" + cat);
                    categories.Controls.Add(category);
                }

                entryFooter.Controls.Add(categories);
            }

        }
    }
}
