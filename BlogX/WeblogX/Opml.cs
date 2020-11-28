namespace Anderson.Chris.BlogX.WebClient
{
    using System;
    using System.Xml;
    using System.Xml.Serialization;


    [XmlRoot("opml")]
    public class Opml
    {
        public OpmlBody body;
    }

    [XmlRoot("body")]
    public class OpmlBody
    {
        [XmlElement("outline")]
        public OpmlOutline[] outline;
    }

    [XmlRoot("outline")]
    public class OpmlOutline
    {
        [XmlAttribute]
        public string title;
        [XmlAttribute]
        public string description;
        [XmlAttribute]
        public string xmlUrl;
        [XmlAttribute]
        public string htmlUrl;
        [XmlAttribute]
        public string language;
    }
}
