namespace Anderson.Chris.BlogX.Runtime
{
    using System;
    using System.Xml;
    using System.Xml.Serialization;

    [XmlRoot("rss", Namespace="")]
    public class RssRoot
    {
        string version = "2.0";
        RssChannelCollection channels = new RssChannelCollection();

        [XmlAttribute("version")]
        public string Version { get { return version; } set { version = value; } }
        [XmlElement("channel")]
        public RssChannelCollection Channels { get { return channels; } set { channels = value; } }
    }

    [XmlRoot("channel")]
    public class RssChannel
    {
        string title;
        string link;
        string description = "";
        string copyright;
        string lastBuildDate;
        string generator = "ChrisAn's BlogX";
        string managingEditor;
        string webMaster;
        RssItemCollection items = new RssItemCollection();

        [XmlElement("title")]
        public string Title { get { return title; } set { title = value; } }
        [XmlElement("link")]
        public string Link { get { return link; } set { link = value; } }
        [XmlElement("description", IsNullable=false)]
        public string Description { get { return description; } set { description = value; } }
        [XmlElement("copyright")]
        public string Copyright { get { return copyright; } set { copyright = value; } }
        [XmlElement("lastBuildDate")]
        public string LastBuildDate { get { return lastBuildDate; } set { lastBuildDate = value; } }
        [XmlElement("generator")]
        public string Generator { get { return generator; } set { generator = value; } }
        [XmlElement("managingEditor")]
        public string ManagingEditor { get { return managingEditor; } set { managingEditor = value; } }
        [XmlElement("webMaster")]
        public string WebMaster { get { return webMaster; } set { webMaster = value; } }
        [XmlElement("item")]
        public RssItemCollection Items { get { return items; } set { items = value; } }
    }

    [XmlRoot("item")]
    public class RssItem
    {
        string title;
        string guid;
        string link;
        string pubDate;
        string description;
        string comments;
        string category;
        XmlElement body;

        [XmlElement("title")]
        public string Title { get { return title; } set { title = value; } }
        [XmlElement("guid")]
        public string Guid { get { return guid; } set { guid = value; } }
        [XmlElement("link")]
        public string Link { get { return link; } set { link = value; } }
        [XmlElement("pubDate")]
        public string PubDate { get { return pubDate; } set { pubDate = value; } }
        [XmlElement("description")]
        public string Description { get { return description; } set { description = value; } }
        [XmlAnyElement]
        public XmlElement Body { get { return body; } set { body = value; } }
        [XmlElement("comments")]
        public string Comments { get { return comments; } set { comments = value; } }
        [XmlElement("category")]
        public string Category { get { return category; } set { category = value; } }
    }
}
