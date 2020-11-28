//=-------
// Copyright 2003, Chris Anderson
// 
//   Provided as is, with no warrenty, etc.
//   License is granted to use, copy, modify, 
//   with or without credit to me, just don't
//   blame me if it doesn't work.
//=-------
namespace Anderson.Chris.BlogX.WindowsClient
{
    using System;
    using System.IO;
    using System.Xml.Serialization;
    using System.Windows.Forms;

    public class WinBlogXProject
    {
        static Version current = new Version(1,1,0,0);

        Version version = new Version(0,0,0,0);
        string editingUrl = "http://localhost/weblogx/blogxediting.asmx";
        string browsingUrl = "http://localhost/weblogx/blogxbrowsing.asmx";
        string configEditingUrl = "http://localhost/weblogx/configediting.asmx";
        string localCacheDir = "";
        string name;

        public Version Version { get { return version; } set { version = value; } }
        public string EditingUrl { get { return editingUrl;} set { editingUrl = value; } }
        public string BrowsingUrl { get { return browsingUrl;} set { browsingUrl = value; } }
        public string ConfigEditingUrl { get { return configEditingUrl;} set { configEditingUrl = value; } }
        public string LocalCacheDir { get { return localCacheDir;} set { localCacheDir = value; } }
        public string Name { get { return name;} set { name = value; } }

        public void Upgrade()
        {
            if (version < new Version(1,1,0,0))
            {
                ConfigEditingUrl = new Uri(new Uri(BrowsingUrl), "configediting.asmx").ToString();
                version = current;
            }
        }
        

        [XmlIgnore]
        public bool IsDefault
        {
            get
            {
                return editingUrl == "http://localhost/weblogx/blogxediting.asmx" &&
                    browsingUrl == "http://localhost/weblogx/blogxbrowsing.asmx" &&
                    configEditingUrl == "http://localhost/weblogx/configediting.asmx" &&
                    localCacheDir == "";
            }
        }
    }
}
