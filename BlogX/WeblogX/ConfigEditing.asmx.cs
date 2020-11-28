namespace Anderson.Chris.BlogX.WebClient
{
    using System;
    using System.IO;
    using System.Globalization;
    using System.Collections;
    using System.ComponentModel;
    using System.Data;
    using System.Diagnostics;
    using System.Web;
    using System.Web.Services;
    using System.Xml.Serialization;
    using Anderson.Chris.BlogX.Runtime;

    /// <summary>
    /// Summary description for ConfigEditing.
    /// </summary>
    [WebService(Namespace="http://www.simplegeek.com")]
    public class ConfigEditing : System.Web.Services.WebService
    {
        public ConfigEditing()
        {
            InitializeComponent();
        }

		#region Component Designer generated code
		
        //Required by the Web Services Designer 
        private IContainer components = null;
				
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose( bool disposing )
        {
            if(disposing && components != null)
            {
                components.Dispose();
            }
            base.Dispose(disposing);		
        }
		
		#endregion

        void ValidatePath(string path)
        {
            FileInfo fi = new FileInfo(Path.GetFullPath(path));
            string root = HttpContext.Current.Server.MapPath("SiteConfig/").ToUpper(CultureInfo.InvariantCulture);
            if (fi.FullName.Length > root.Length)
            {
                string compare = fi.FullName.ToUpper(CultureInfo.InvariantCulture);
                if (compare.StartsWith(root))
                {
                    return;
                }
            }
                
            throw new Exception("Invalid path");
        }

        [WebMethod]
        public string[] GetFiles(string username, string password)
        {
            if (SiteSecurity.Login(username, password).Role != "admin")
            {
                throw new Exception("Invalid Password");
            }

            string fullPath = HttpContext.Current.Server.MapPath("SiteConfig/");
            DirectoryInfo di = new DirectoryInfo(fullPath);
            FileInfo[] files = di.GetFiles();
            string[] results = new string[files.Length];
            for (int i=0; i<files.Length; i++)
            {
                results[i] = files[i].Name;
            }
            return results;
        }

        [WebMethod]
        public byte[] ReadFile(string fileName, string username, string password)
        {
            if (SiteSecurity.Login(username, password).Role != "admin")
            {
                throw new Exception("Invalid Password");
            }

            string fullPath = HttpContext.Current.Server.MapPath("SiteConfig/" + fileName);
            ValidatePath(fullPath);
            if (File.Exists(fullPath))
            {
                using (StreamReader reader = new StreamReader(fullPath))
                {
                    string value = reader.ReadToEnd();
                    MemoryStream ms = new MemoryStream();
                    StreamWriter sw = new StreamWriter(ms);
                    sw.Write(value);
                    sw.Flush();
                    sw.Close();

                    return ms.ToArray();
                }
            }
            return new byte[0];
        }

        [WebMethod]
        public void UpdateFile(string fileName, byte[] content, string username, string password)
        {
            if (SiteSecurity.Login(username, password).Role != "admin")
            {
                throw new Exception("Invalid Password");
            }

            string fullPath = HttpContext.Current.Server.MapPath("SiteConfig/" + fileName);
            ValidatePath(fullPath);
            FileStream writer = null;
            try
            {
                if (File.Exists(fullPath))
                {
                    writer = new FileStream(fullPath, FileMode.Truncate, FileAccess.Write);
                }
                else
                {
                    writer = new FileStream(fullPath, FileMode.Create, FileAccess.Write);
                }
                writer.Write(content, 0, content.Length);
            }
            finally 
            {
                if (writer != null)
                {
                    writer.Close();
                }
            }
        }

        [WebMethod]
        public NavigationRoot ReadNavigation(string username, string password)
        {
            if (SiteSecurity.Login(username, password).Role != "admin")
            {
                throw new Exception("Invalid Password");
            }

            string fullPath = HttpContext.Current.Server.MapPath("SiteConfig/navigation.xml");
            if (File.Exists(fullPath))
            {
                XmlSerializer ser = new XmlSerializer(typeof(NavigationRoot));
                using (StreamReader reader = new StreamReader(fullPath))
                {
                    NavigationRoot nav = (NavigationRoot)ser.Deserialize(reader);
                    return nav;
                }
            }
            else
            {
                return new NavigationRoot();
            }
        }

        [WebMethod]
        public void UpdateNavigation(NavigationRoot navigation, string username, string password)
        {
            if (SiteSecurity.Login(username, password).Role != "admin")
            {
                throw new Exception("Invalid Password");
            }

            string fullPath = HttpContext.Current.Server.MapPath("SiteConfig/navigation.xml");
            XmlSerializer ser = new XmlSerializer(typeof(NavigationRoot));
            using (StreamWriter writer = new StreamWriter(fullPath))
            {
                ser.Serialize(writer, navigation);
            }
        }
    }
}
