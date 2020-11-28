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
    using System.Text.RegularExpressions;
    using Anderson.Chris.BlogX.Runtime;

    /// <summary>
    ///		Summary description for EntryView.
    /// </summary>
    public abstract class SingleCommentView : System.Web.UI.UserControl
    {
        Comment comment;
        string dateFormat = "MM/dd/yyyy h:mm tt";
        bool obfuscateEmail = true;

        public bool ObfuscateEmail { get { return obfuscateEmail; } set { obfuscateEmail = value; } }

        public string DateFormat
        {
            get
            {
                return dateFormat;
            }
            set
            {
                dateFormat = value;
            }
        }
        public Comment Comment 
        {
            get
            {
                return comment;
            }
            set
            {
                comment = value;
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

        string SpamBlocker(string email)
        {
            if (ObfuscateEmail)
            {
                email = Regex.Replace(email, "@", "AT NOSPAM");
                email = Regex.Replace(email, @"\.", " dot ");
            }
            return email;
        }

        private void Page_PreRender(object sender, System.EventArgs e)
        {
            Control root = this;
            HtmlGenericControl entry = new HtmlGenericControl("div");
            entry.Attributes["class"] = "comment";
            root.Controls.Add(entry);

            HtmlGenericControl entryTitle = new HtmlGenericControl("h3");
            entryTitle.Attributes["class"] = "commentTitle";
            entryTitle.Controls.Add(new LiteralControl(comment.Created.ToString(dateFormat)));
            entry.Controls.Add(entryTitle);

            Label entryBody = new Label();
            entryBody.CssClass = "commentBody";
            entryBody.Controls.Add(new LiteralControl(Regex.Replace(comment.Content, "\n", "<br />")));
            entry.Controls.Add(entryBody);

            HtmlGenericControl footer = new HtmlGenericControl("p");
            footer.Attributes["class"] = "commentFooter";
            entry.Controls.Add(footer);

            string authorLink = null;
            if (comment.AuthorHomepage != null && comment.AuthorHomepage.Length > 0)
            {
                authorLink = comment.AuthorHomepage;
            }
            else if (comment.AuthorEmail != null && comment.AuthorEmail.Length > 0)
            {
                authorLink = "mailto:" + SpamBlocker(comment.AuthorEmail);
            }
            if (authorLink != null)
            {
                if (SiteConfig.GetSiteConfig().LinksInComments)
                {
                    HyperLink link = new HyperLink();
                    link.Attributes["class"] = "permalink";
                    link.NavigateUrl = authorLink;
                    link.Text = comment.Author;
                    footer.Controls.Add(link);
                }
                else
                {
                    Label l = new Label();
                    l.Attributes["class"] = "permalink";
                    l.Text = comment.Author;
                    footer.Controls.Add(l);
                    footer.Controls.Add(new LiteralControl(" | "));

                    Label address = new Label();
                    address.Attributes["class"] = "permalink";
                    address.Text = authorLink;
                    footer.Controls.Add(address);
                }
            }
            else
            {
                Label l = new Label();
                l.Attributes["class"] = "permalink";
                l.Text = comment.Author;
                footer.Controls.Add(l);
            }

            if (comment.AuthorEmail != null && comment.AuthorEmail.Length > 0)
            {
                footer.Controls.Add(new LiteralControl(" | "));

                HtmlGenericControl mailto = new HtmlGenericControl("span");
                mailto.Attributes["class"] = "comments";
                footer.Controls.Add(mailto);

                HyperLink link = new HyperLink();
                link.NavigateUrl = "mailto:" + SpamBlocker(comment.AuthorEmail);
                link.Text = SpamBlocker(comment.AuthorEmail);
                mailto.Controls.Add(link);
            }

        }
    }
}
