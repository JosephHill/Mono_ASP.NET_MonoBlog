namespace Anderson.Chris.BlogX.WebClient
{
    using System;
    using System.Configuration;
    using System.Data;
    using System.IO;
    using System.Drawing;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using System.Web.UI.HtmlControls;
    using System.Xml.Serialization;
    using System.Net;
    using Anderson.Chris.BlogX.Runtime;

    /// <summary>
    ///		Summary description for SideBarList.
    /// </summary>
    public abstract class SideBarOpmlList : System.Web.UI.UserControl
    {
        string fileName;

        public string FileName
        {
            get
            {
                return fileName;
            }
            set
            {
                fileName = value;
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
            this.PreRender += new System.EventHandler(this.SideBarList_PreRender);

        }
		#endregion

        private void SideBarList_PreRender(object sender, System.EventArgs e)
        {
            HtmlGenericControl section = new HtmlGenericControl("div");
            section.Attributes["class"] = "section";
            this.Controls.Add(section);
            
            HtmlGenericControl heading = new HtmlGenericControl("h3");
            heading.Controls.Add(new LiteralControl("Blogroll"));
            section.Controls.Add(heading);

            HtmlGenericControl list = new HtmlGenericControl("ul");
            section.Controls.Add(list);

            try
            {
                bool foundOpml = false;
                Opml nav = null;

                string fullPath = Server.MapPath("SiteConfig/opml.xml");
                if ((File.Exists(fullPath)) && (foundOpml == false))
                {
                    foundOpml = true;
                    fileName = fullPath;

                    XmlSerializer ser;
                    using (Stream s = File.OpenRead(fullPath))
                    {
                        ser = new XmlSerializer(typeof(Opml));
                        nav = (Opml)ser.Deserialize(s);
                    }
                }

                string urlPath = Server.MapPath("SiteConfig/opml.txt");
                if ((File.Exists(urlPath)) && (foundOpml == false))
                {
                    foundOpml = true;
                    string url;
                    fileName = urlPath;

                    using (StreamReader t = File.OpenText(urlPath))
                    {
                        url = t.ReadLine();
                    }

                    WebRequest wq = WebRequest.Create(url);
                    using (WebResponse wr = wq.GetResponse())
                    {
                        XmlSerializer ser;
                        using (Stream s = wr.GetResponseStream())
                        {
                            ser = new XmlSerializer(typeof(Opml));
                            nav = (Opml)ser.Deserialize(s);
                        }
                    }
                }

                if (foundOpml == true)
                {
                    foreach (OpmlOutline navitem in nav.body.outline)
                    {
                        HtmlGenericControl item = new HtmlGenericControl("li");
                        list.Controls.Add(item);

                        HyperLink catLink = new HyperLink();
                        catLink.Text = navitem.title;
                        catLink.NavigateUrl = navitem.htmlUrl;
                        item.Controls.Add(catLink);
                    }
                }
                else
                {
                    section.Controls.Add(new LiteralControl("Add 'opml.xml' or 'opml.txt' to your SiteConfig directory<br />"));
                }
            }
            catch
            {
                section.Controls.Add(new LiteralControl("There was an error processing '" + fileName + "'<br />"));
            }
        }
    }
}

