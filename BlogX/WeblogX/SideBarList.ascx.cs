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
    using Anderson.Chris.BlogX.Runtime;

    /// <summary>
    ///		Summary description for SideBarList.
    /// </summary>
    public abstract class SideBarList : System.Web.UI.UserControl
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
            heading.Controls.Add(new LiteralControl("Links"));
            section.Controls.Add(heading);

            HtmlGenericControl list = new HtmlGenericControl("ul");
            section.Controls.Add(list);

            try
            {
                string fullPath = Server.MapPath(SiteConfig.GetSiteConfig().ContentDir + fileName);
                if (File.Exists(fullPath))
                {
                    NavigatorXml nav;
                    using (Stream s = File.OpenRead(fullPath))
                    {
                        XmlSerializer ser = new XmlSerializer(typeof(NavigatorXml));
                        nav = (NavigatorXml)ser.Deserialize(s);
                    }
                    foreach (NavigatorItem navitem in nav.Items)
                    {
                        HtmlGenericControl item = new HtmlGenericControl("li");
                        list.Controls.Add(item);

                        HyperLink catLink = new HyperLink();
                        catLink.Text = navitem.Name;
                        catLink.NavigateUrl = navitem.Url;
                        item.Controls.Add(catLink);
                    }
                }
                else
                {
                    NavigationRoot nav;
                    fullPath = Server.MapPath("SiteConfig/" + fileName);
                    if (File.Exists(fullPath))
                    {
                        using (Stream s = File.OpenRead(fullPath))
                        {
                            XmlSerializer ser = new XmlSerializer(typeof(NavigationRoot));
                            nav = (NavigationRoot)ser.Deserialize(s);
                        }
                        foreach (NavigationLink navitem in nav.Items)
                        {
                            HtmlGenericControl item = new HtmlGenericControl("li");
                            list.Controls.Add(item);

                            HyperLink catLink = new HyperLink();
                            catLink.Text = navitem.Name;
                            catLink.NavigateUrl = navitem.Url;
                            item.Controls.Add(catLink);
                        }
                    }
                    else
                    {
                        section.Controls.Add(new LiteralControl("Add '" + fileName + "' to your SiteConfig directory<br />"));
                    }
                }
            }
            catch
            {
                section.Controls.Add(new LiteralControl("There was an error processing '" + fileName + "'<br />"));
            }
        }
    }

    [XmlRoot("navigator")]
    public class NavigatorXml
    {
        NavigatorItemCollection items = new NavigatorItemCollection();

        [XmlElement("item")]
        public NavigatorItemCollection Items { get { return items; } set { items = value; } }
    }

    public class NavigatorItem
    {
        string name;
        string url;

        [XmlAttribute("name")]
        public string Name { get { return name; } set { name = value; } }
        [XmlAttribute("pagename")]
        public string Url { get { return url; } set { url = value; } } 
    }
    #region Collection
    /// <summary>
    /// A collection of elements of type NavigatorItem
    /// </summary>
    public class NavigatorItemCollection: System.Collections.CollectionBase
    {
        /// <summary>
        /// Initializes a new empty instance of the NavigatorItemCollection class.
        /// </summary>
        public NavigatorItemCollection()
        {
            // empty
        }

        /// <summary>
        /// Initializes a new instance of the NavigatorItemCollection class, containing elements
        /// copied from an array.
        /// </summary>
        /// <param name="items">
        /// The array whose elements are to be added to the new NavigatorItemCollection.
        /// </param>
        public NavigatorItemCollection(NavigatorItem[] items)
        {
            this.AddRange(items);
        }

        /// <summary>
        /// Initializes a new instance of the NavigatorItemCollection class, containing elements
        /// copied from another instance of NavigatorItemCollection
        /// </summary>
        /// <param name="items">
        /// The NavigatorItemCollection whose elements are to be added to the new NavigatorItemCollection.
        /// </param>
        public NavigatorItemCollection(NavigatorItemCollection items)
        {
            this.AddRange(items);
        }

        /// <summary>
        /// Adds the elements of an array to the end of this NavigatorItemCollection.
        /// </summary>
        /// <param name="items">
        /// The array whose elements are to be added to the end of this NavigatorItemCollection.
        /// </param>
        public virtual void AddRange(NavigatorItem[] items)
        {
            foreach (NavigatorItem item in items)
            {
                this.List.Add(item);
            }
        }

        /// <summary>
        /// Adds the elements of another NavigatorItemCollection to the end of this NavigatorItemCollection.
        /// </summary>
        /// <param name="items">
        /// The NavigatorItemCollection whose elements are to be added to the end of this NavigatorItemCollection.
        /// </param>
        public virtual void AddRange(NavigatorItemCollection items)
        {
            foreach (NavigatorItem item in items)
            {
                this.List.Add(item);
            }
        }

        /// <summary>
        /// Adds an instance of type NavigatorItem to the end of this NavigatorItemCollection.
        /// </summary>
        /// <param name="value">
        /// The NavigatorItem to be added to the end of this NavigatorItemCollection.
        /// </param>
        public virtual void Add(NavigatorItem value)
        {
            this.List.Add(value);
        }

        /// <summary>
        /// Determines whether a specfic NavigatorItem value is in this NavigatorItemCollection.
        /// </summary>
        /// <param name="value">
        /// The NavigatorItem value to locate in this NavigatorItemCollection.
        /// </param>
        /// <returns>
        /// true if value is found in this NavigatorItemCollection;
        /// false otherwise.
        /// </returns>
        public virtual bool Contains(NavigatorItem value)
        {
            return this.List.Contains(value);
        }

        /// <summary>
        /// Return the zero-based index of the first occurrence of a specific value
        /// in this NavigatorItemCollection
        /// </summary>
        /// <param name="value">
        /// The NavigatorItem value to locate in the NavigatorItemCollection.
        /// </param>
        /// <returns>
        /// The zero-based index of the first occurrence of the _ELEMENT value if found;
        /// -1 otherwise.
        /// </returns>
        public virtual int IndexOf(NavigatorItem value)
        {
            return this.List.IndexOf(value);
        }

        /// <summary>
        /// Inserts an element into the NavigatorItemCollection at the specified index
        /// </summary>
        /// <param name="index">
        /// The index at which the NavigatorItem is to be inserted.
        /// </param>
        /// <param name="value">
        /// The NavigatorItem to insert.
        /// </param>
        public virtual void Insert(int index, NavigatorItem value)
        {
            this.List.Insert(index, value);
        }

        /// <summary>
        /// Gets or sets the NavigatorItem at the given index in this NavigatorItemCollection.
        /// </summary>
        public virtual NavigatorItem this[int index]
        {
            get
            {
                return (NavigatorItem) this.List[index];
            }
            set
            {
                this.List[index] = value;
            }
        }

        /// <summary>
        /// Removes the first occurrence of a specific NavigatorItem from this NavigatorItemCollection.
        /// </summary>
        /// <param name="value">
        /// The NavigatorItem value to remove from this NavigatorItemCollection.
        /// </param>
        public virtual void Remove(NavigatorItem value)
        {
            this.List.Remove(value);
        }

        /// <summary>
        /// Type-specific enumeration class, used by NavigatorItemCollection.GetEnumerator.
        /// </summary>
        public class Enumerator: System.Collections.IEnumerator
        {
            private System.Collections.IEnumerator wrapped;

            public Enumerator(NavigatorItemCollection collection)
            {
                this.wrapped = ((System.Collections.CollectionBase)collection).GetEnumerator();
            }

            public NavigatorItem Current
            {
                get
                {
                    return (NavigatorItem) (this.wrapped.Current);
                }
            }

            object System.Collections.IEnumerator.Current
            {
                get
                {
                    return (NavigatorItem) (this.wrapped.Current);
                }
            }

            public bool MoveNext()
            {
                return this.wrapped.MoveNext();
            }

            public void Reset()
            {
                this.wrapped.Reset();
            }
        }

        /// <summary>
        /// Returns an enumerator that can iterate through the elements of this NavigatorItemCollection.
        /// </summary>
        /// <returns>
        /// An object that implements System.Collections.IEnumerator.
        /// </returns>        
        public new virtual NavigatorItemCollection.Enumerator GetEnumerator()
        {
            return new NavigatorItemCollection.Enumerator(this);
        }
    }
    #endregion
}
