namespace Anderson.Chris.BlogX.WebClient
{
    using System;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Threading;
    using System.IO;
    using System.Net;
    using System.Web;
    using System.Configuration;
    using System.Web.Util;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using Anderson.Chris.BlogX.Runtime;

    /// <summary>
    /// Summary description for BlogXUtils.
    /// </summary>
    class BlogXUtils
    {
        static ReaderWriterLock readerLock = new ReaderWriterLock();

        public static void PingWeblogsCom()
        {
            SiteConfig config = SiteConfig.GetSiteConfig(); 
            if (config.NotifyWebLogsDotCom)
            {
                PingWeblogsCom(config.Title, config.Root);
            }
        }

        public static string RelativeToRoot(string relative)
        {
            return RootUrl(HttpContext.Current.Request) + "/" + relative;
        }

        public static string RootUrl(HttpRequest request)
        {
            string full = request.Url.ToString();

            if (request.PathInfo != null && request.PathInfo.Length > 0)
            {
                full = full.Substring(0, full.Length - request.PathInfo.Length - 1);
            }
            full = full.Substring(0, full.LastIndexOf('/'));

            return full;

        }


        // from: http://www.learnmobile.net/MobileNews/Default.aspx?Key=29
        //
        private static void PingWeblogsCom(string blogName, string blogUrl)
        {
            StringBuilder soapCall = new StringBuilder();
            soapCall.AppendFormat("<?xml version=\"1.0\" ?>{0}", Environment.NewLine);
            soapCall.Append("<SOAP-ENV:Envelope ");
            soapCall.Append("SOAP-ENV:encodingStyle=\"http://schemas.xmlsoap.org/soap/encoding/\" ");
            soapCall.Append("xmlns:SOAP-ENC=\"http://schemas.xmlsoap.org/soap/encoding/\" ");
            soapCall.Append("xmlns:SOAP-ENV=\"http://schemas.xmlsoap.org/soap/envelope/\" ");
            soapCall.Append("xmlns:xsd=\"http://www.w3.org/1999/XMLSchema\" ");
            soapCall.AppendFormat("xmlns:xsi=\"http://www.w3.org/1999/XMLSchema-instance\">{0}", Environment.NewLine);
            soapCall.AppendFormat("<SOAP-ENV:Body>{0}", Environment.NewLine);
            soapCall.AppendFormat("<ping>{0}", Environment.NewLine);
            soapCall.AppendFormat("<weblogname xsi:type=\"xsd:string\">{0}", Environment.NewLine);
            soapCall.Append(blogName);
            soapCall.Append(Environment.NewLine);
            soapCall.AppendFormat("</weblogname>{0}", Environment.NewLine);
            soapCall.AppendFormat("<weblogurl xsi:type=\"xsd:string\">{0}", Environment.NewLine);
            soapCall.Append(blogUrl);
            soapCall.Append(Environment.NewLine);
            soapCall.AppendFormat("</weblogurl>{0}", Environment.NewLine);
            soapCall.AppendFormat("</ping>{0}", Environment.NewLine);
            soapCall.AppendFormat("</SOAP-ENV:Body>{0}", Environment.NewLine);
            soapCall.AppendFormat("</SOAP-ENV:Envelope>{0}", Environment.NewLine);

            byte[] SomeBytes = null;

            HttpWebRequest req = (HttpWebRequest)WebRequest.Create("http://rpc.weblogs.com/weblogUpdates");
            req.Method = "POST";
            req.ContentType = "text/xml;charset=\"us-ascii\"";
            req.Headers.Add("SOAPAction","\"/weblogUpdates\"");
            SomeBytes = Encoding.UTF8.GetBytes(soapCall.ToString());
            req.ContentLength = SomeBytes.Length;

            Stream RequestStream = null;
            RequestStream = req.GetRequestStream();
            RequestStream.Write(SomeBytes, 0, SomeBytes.Length);
            RequestStream.Close();
        }

        public static void TrackReferrer(HttpRequest request, HttpServerUtility server)
        {
            readerLock.AcquireWriterLock(250);
            try 
            {
                WriteReferrer(request, server);
            }
            catch
            {
            }
            finally 
            {
                readerLock.ReleaseLock();
            }
        }

        private static string GetReferrerLogText(HttpServerUtility server, DateTime date)
        {
            string result = null;

            readerLock.AcquireReaderLock(250);
            try
            {
                using (StreamReader sw = new StreamReader(server.MapPath(SiteConfig.GetSiteConfig().LogDir + DateTime.Now.Date.ToString("yyyy-MM-dd") + ".referrer.log")))
                {
                    result = sw.ReadToEnd();
                }
            }
            catch
            {
            }
            finally
            {
                readerLock.ReleaseLock();
            }

            return result;
        }

        public static LogDataItemCollection GetReferrerLog(DateTime date)
        {
            LogDataItemCollection logs = new LogDataItemCollection();

            string log = BlogXUtils.GetReferrerLogText(HttpContext.Current.Server, DateTime.Now);
            Regex parser = new Regex(@"e2\ttime(?<time>.*)\turl\t(?<url>.*)\turlreferrer\t(?<urlreferrer>.*)\tuseragent\t(?<useragent>.*)");

            foreach (Match match in parser.Matches(log))
            {
                LogDataItem item = new LogDataItem();
                item.Requested = DateTime.Parse(match.Groups["time"].Value);
                item.UrlRequested = match.Groups["url"].Value;
                item.UrlReferrer = match.Groups["urlreferrer"].Value;
                item.UserAgent = match.Groups["useragent"].Value;
                logs.Add(item);
            }

            return logs;
        }

        static void WriteReferrer(HttpRequest request, HttpServerUtility server)
        {
            using (StreamWriter sw = new StreamWriter(server.MapPath(SiteConfig.GetSiteConfig().LogDir + DateTime.Now.Date.ToString("yyyy-MM-dd") + ".referrer.log"), true))
            {
                sw.WriteLine("e2\ttime\t{0}\turl\t{1}\turlreferrer\t{2}\tuseragent\t{3}", DateTime.Now.ToString("s"), request.Url, request.UrlReferrer, request.UserAgent);
            }
        }

        public static void AddContentTo(string path, Control target)
        {
            if (path.ToLower().EndsWith(".format.html"))
            {
                string fullpath = HttpContext.Current.Server.MapPath(path);
                string value = "";
                using (StreamReader reader = new StreamReader(fullpath))
                {
                    value = reader.ReadToEnd();
                }
                target.Controls.Add(new LiteralControl(value));
            }

        }
    }
}
