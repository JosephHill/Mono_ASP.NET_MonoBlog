namespace Anderson.Chris.BlogX.WebClient
{
    using System;
    using System.Collections;
    using System.Xml.Serialization;
    using System.IO;
    using System.Web;
    using System.Web.UI;

    public class SiteConfig
    {
        string title;
        string contact;
        string root;
        string copyright;
        int rssDayCount = 10;
        int rssEntryCount = 50;
        bool entryTitleAsLink = false;
        bool allowComments = true;
        bool linksInComments = true;
        bool commentSpamGaurd = true;
        bool notifyWebLogsDotCom = false;
        bool obfuscateEmail = true;
        string editPassword;
        string contentDir;
        string logDir;

        public static SiteConfig GetSiteConfig()
        {
            XmlSerializer ser = new XmlSerializer(typeof(SiteConfig));
            SiteConfig config = null;

            using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("SiteConfig/site.config")))
            {
                config = ser.Deserialize(reader) as SiteConfig;
            }
            return config;
        }

        public static string[] GetCommentWords()
        {
            string[] words = null;
            string file = HttpContext.Current.Server.MapPath("SiteConfig/commentwords.txt");
            if (File.Exists(file))
            {
                ArrayList wordBuilder = new ArrayList();
                using (StreamReader reader = new StreamReader(file))
                {
                    words = reader.ReadToEnd().Split('\n');
                }
                for (int i=0; i<words.Length; i++)
                {
                    if (words[i] != null)
                    {
                        string word = words[i].Trim();
                        if (word.Length > 0)
                        {
                            wordBuilder.Add(word);
                        }
                    }
                }
                words = (string[])wordBuilder.ToArray(typeof(string));
            }
            return words;
        }

        public string Title { get { return title; } set { title = value; } }
        public string Contact { get { return contact; } set { contact = value; } }
        public string Root { get { return root; } set { root = value; } }
        public string Copyright { get { return copyright; } set { copyright = value; } }
        public int RssDayCount { get { return rssDayCount; } set { rssDayCount = value; } }
        public int RssEntryCount { get { return rssEntryCount; } set { rssEntryCount = value; } }
        public bool AllowComments { get { return allowComments; } set { allowComments = value; } }
        public bool LinksInComments { get { return linksInComments; } set { linksInComments = value; } }
        public bool CommentSpamGuard { get { return commentSpamGaurd; } set { commentSpamGaurd = value; } }
        public bool EntryTitleAsLink { get { return entryTitleAsLink; } set { entryTitleAsLink = value; } }
        public bool NotifyWebLogsDotCom { get { return notifyWebLogsDotCom; } set { notifyWebLogsDotCom = value; } }
        public bool ObfuscateEmail { get { return obfuscateEmail; } set { obfuscateEmail = value; } }
        public string EditPassword { get { return editPassword; } set { editPassword = value; } }
        public string ContentDir { get { return contentDir; } set { contentDir = value; } }
        public string LogDir { get { return logDir; } set { logDir = value; } }
    }
}
