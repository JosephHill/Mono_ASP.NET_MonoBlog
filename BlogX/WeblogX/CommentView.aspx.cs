namespace Anderson.Chris.BlogX.WebClient
{
    using System;
    using System.Collections;
    using System.Configuration;
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
    /// Summary description for CommentView.
    /// </summary>
    public class CommentView : System.Web.UI.Page
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

        protected System.Web.UI.WebControls.Label Label1;
        protected System.Web.UI.WebControls.Label Label2;
        protected System.Web.UI.WebControls.Label Label3;
        protected System.Web.UI.WebControls.Label Label4;
        protected System.Web.UI.WebControls.TextBox name;
        protected System.Web.UI.WebControls.TextBox email;
        protected System.Web.UI.WebControls.TextBox homepage;
        protected System.Web.UI.WebControls.TextBox comment;
        protected System.Web.UI.WebControls.Button add;
        protected System.Web.UI.WebControls.CheckBox rememberMe;
        protected System.Web.UI.HtmlControls.HtmlGenericControl comments;
        protected System.Web.UI.HtmlControls.HtmlGenericControl content;
        protected System.Web.UI.WebControls.Label Label6;
        protected System.Web.UI.WebControls.Label Label5;
        protected System.Web.UI.WebControls.TextBox securityWord;
        protected System.Web.UI.HtmlControls.HtmlGenericControl commentSpamGuard;
        protected System.Web.UI.WebControls.Image securityWordImage;
 
        BlogXData data = new BlogXData();

        private void Page_Load(object sender, System.EventArgs e)
        {
            if (!IsPostBack)
            {
                string entryId = Request.QueryString["entryId"];
                if (entryId != null && entryId.Length > 0)
                {
                    entryId = entryId.ToUpper();
                }
                else
                {
                    entryId = Request.PathInfo.Substring(1).ToUpper();
                }
                ViewState["entryId"] = entryId;

                if (Request.Cookies["email"] != null)
                {
                    email.Text = Request.Cookies["email"].Value;
                    name.Text = Request.Cookies["name"].Value;
                    homepage.Text = Request.Cookies["homepage"].Value;
                }
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
            this.add.Click += new System.EventHandler(this.add_Click);
            this.Load += new System.EventHandler(this.Page_Load);
            this.PreRender += new System.EventHandler(this.CommentView_PreRender);

        }
		#endregion

        private void CommentView_PreRender(object sender, System.EventArgs e)
        {
            string entryId = (string)ViewState["entryId"];
            EntryIdCache ecache = new EntryIdCache();
            ecache.Ensure(data);
            Control root = comments;

            DateTime found = new DateTime();

            foreach (EntryIdCacheEntry ece in ecache.Entries)
            {
                if (ece.EntryId.ToUpper() == entryId)
                {
                    found = ece.Date;
                    entryId = ece.EntryId;
                    break;
                }
            }

            bool obfuscateEmail = SiteConfig.GetSiteConfig().ObfuscateEmail;
            DayExtra extra = data.GetDayExtra(found);
            extra.Load();

            foreach (DayEntry day in data.Days)
            {
                if (day.Date == found)
                {
                    day.Load();
                    foreach (Entry entry in day.Entries)
                    {
                        if (entry.EntryId == entryId)
                        {
                            EntryView entryView = (EntryView)LoadControl("EntryView.ascx");
                            entryView.Data = data;
                            entryView.Entry = entry;
                            entryView.Extra = extra;
                            comments.Controls.Add(entryView);
                        }
                    }
                }
            }

            commentSpamGuard.Visible = SiteConfig.GetSiteConfig().CommentSpamGuard;
            if (SiteConfig.GetSiteConfig().CommentSpamGuard)
            {
                System.Security.Cryptography.RandomNumberGenerator r = System.Security.Cryptography.RandomNumberGenerator.Create();
                byte[] randomData = new byte[4];
                r.GetBytes(randomData);
                int code = 0;
                for (int i=0; i<randomData.Length; i++)
                {
                    code += randomData[i];
                }
                code = code % SiteConfig.GetCommentWords().Length;
                securityWordImage.ImageUrl = BlogXUtils.RelativeToRoot("verifyimagegen.ashx?code=" + code);
                ViewState["spamCode"] = code;
            }


            foreach (Comment c in extra.GetCommentsFor(entryId))
            {
                SingleCommentView view = (SingleCommentView)LoadControl("SingleCommentView.ascx");
                view.Comment = c;
                view.ObfuscateEmail = obfuscateEmail;
                root.Controls.Add(view);
            }
        }

        private void add_Click(object sender, System.EventArgs e)
        {
            if (SiteConfig.GetSiteConfig().CommentSpamGuard)
            {
                string word = SiteConfig.GetCommentWords()[(int)ViewState["spamCode"]];
                if (!word.Equals(securityWord.Text))
                {
                    // UNDONE : Should show an error page... 
                    //
                    return;
                }
            }
            
            string entryId = ViewState["entryId"].ToString().ToUpper();

            if (rememberMe.Checked)
            {
                Response.Cookies.Add(new HttpCookie("name", name.Text));
                Response.Cookies.Add(new HttpCookie("email", email.Text));
                Response.Cookies.Add(new HttpCookie("homepage", homepage.Text));
            }
            EntryIdCache ecache = new EntryIdCache();
            ecache.Ensure(data);

            DateTime dateFound = new DateTime();
            bool found = false;

            foreach (EntryIdCacheEntry ece in ecache.Entries)
            {
                if (ece.EntryId.ToUpper() == entryId)
                {
                    dateFound = ece.Date;
                    entryId = ece.EntryId;
                    found = true;
                    break;
                }
            }

            if (found)
            {
                if (SiteConfig.GetSiteConfig().AllowComments)
                {
                    DayExtra extra = data.GetDayExtra(dateFound);
                    extra.Load();
                    Comment c = new Comment();
                    c.Initialize();
                    c.Author = name.Text;
                    c.AuthorEmail = email.Text;
                    c.AuthorHomepage = homepage.Text;
                    c.Content = Server.HtmlEncode(comment.Text);
                    c.TargetEntryId = entryId;
                    extra.Comments.Add(c);
                    extra.Save();

                    data.IncrementExtraChange();
                }

                name.Text = "";
                email.Text = "";
                homepage.Text = "";
                comment.Text = "";
                Response.Redirect("default.aspx", false);
            }
        }
    }
}
