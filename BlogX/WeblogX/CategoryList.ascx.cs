namespace Anderson.Chris.BlogX.WebClient
{
    using System;
    using System.Data;
    using System.Drawing;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using System.Web.UI.HtmlControls;
    using Anderson.Chris.BlogX.Runtime;

    /// <summary>
    ///		Summary description for CategoryList.
    /// </summary>
    public abstract class CategoryList : System.Web.UI.UserControl
    {
        BlogXData data;

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

            

        private void Page_Load(object sender, System.EventArgs e)
        {
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
            this.PreRender += new System.EventHandler(this.CategoryList_PreRender);

        }
		#endregion

        private void CategoryList_PreRender(object sender, System.EventArgs e)
        {
            CategoryCache cache = new CategoryCache();
            cache.Ensure(data);

            HtmlGenericControl section = new HtmlGenericControl("div");
            section.Attributes["class"] = "section";
            this.Controls.Add(section);
            
            HtmlGenericControl heading = new HtmlGenericControl("h3");
            heading.Controls.Add(new LiteralControl("Categories"));
            section.Controls.Add(heading);

            HtmlGenericControl list = new HtmlGenericControl("ul");
            section.Controls.Add(list);

            foreach (CategoryCacheEntry catEntry in cache.Entries)
            {
                HtmlGenericControl item = new HtmlGenericControl("li");
                list.Controls.Add(item);

                HyperLink catLink = new HyperLink();
                catLink.Text = catEntry.Name;
                catLink.NavigateUrl = BlogXUtils.RelativeToRoot("CategoryView.aspx/" + catEntry.Name);
                item.Controls.Add(catLink);
                item.Controls.Add(new LiteralControl(" ("));
                HyperLink rssLink = new HyperLink();
                rssLink.Text = "rss";
                rssLink.NavigateUrl = BlogXUtils.RelativeToRoot("BlogXBrowsing.asmx/GetRssCategory?categoryName=" + catEntry.Name);
                item.Controls.Add(rssLink);
                item.Controls.Add(new LiteralControl(")"));
            }

            
        }
    }
}
