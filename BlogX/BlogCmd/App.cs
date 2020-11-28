//=-------
// Copyright 2003, Chris Anderson
// 
//   Provided as is, with no warrenty, etc.
//   License is granted to use, copy, modify, 
//   with or without credit to me, just don't
//   blame me if it doesn't work.
//=-------
namespace Anderson.Chris.BlogX.RadioPort
{
    using System;
    using System.Xml;
    using System.IO;
    using System.Text;
    using System.Collections;
    using Anderson.Chris.BlogX.Runtime;

    class App
    {
        static string FileSystemResolver(string file)
        {
            return Path.Combine(to, file);
        }

        static string from;
        static string to;

        static int Main(string[] args)
        {
            Console.WriteLine("Radio Data Importer");
            Console.WriteLine("");

            foreach (string arg in args)
            {
                if (arg.Length > 6 && arg.ToLower().StartsWith("/from:"))
                {
                    from = arg.Substring(6).Trim();
                    if (from[0] == '\"' && from[from.Length] == '\"')
                    {
                        from = from.Substring(1, from.Length - 2);
                    }
                }
                else if (arg.Length > 4 && arg.ToLower().StartsWith("/to:"))
                {
                    to = arg.Substring(4).Trim();
                    if (to[0] == '\"' && to[from.Length] == '\"')
                    {
                        to = to.Substring(1, to.Length - 2);
                    }
                }
                else
                {
                    break;
                }
            }

            if (from == null || to == null || from.Length == 0 || to.Length == 0)
            {
                Console.WriteLine("Usage: blogcmd /from:<radio directory> /to:<output directory>");
                Console.WriteLine("");
                return -1;
            }
            

            BlogXData.Resolver = new ResolveFileCallback(FileSystemResolver);

            Console.WriteLine("Importing entries...");

            ArrayList tables = new ArrayList();

            XmlDocument masterDoc = new XmlDocument();
            StringBuilder sb = new StringBuilder();
            sb.Append("<tables>");

            foreach (FileInfo file in new DirectoryInfo(from).GetFiles("*.xml"))
            {
                XmlDocument entry = new XmlDocument();
                entry.Load(file.FullName);
                sb.Append(entry.FirstChild.NextSibling.OuterXml);
            }
            sb.Append("</tables>");

            masterDoc.Load(new StringReader(sb.ToString()));

            foreach (XmlNode node in masterDoc.FirstChild)
            {
                RadioTable table = new RadioTable();
                table.Name = node.Attributes["name"].Value;
                foreach (XmlNode child in node)
                {
                    switch (child.Name)
                    {
                        case "date":
                            table.Data[child.Attributes["name"].Value] = DateTime.Parse(child.Attributes["value"].Value);
                            break;
                        case "boolean":
                            table.Data[child.Attributes["name"].Value] = bool.Parse(child.Attributes["value"].Value);
                            break;
                        case "string":
                            table.Data[child.Attributes["name"].Value] = child.Attributes["value"].Value;
                            break;
                        case "table":
                            if (child.Attributes["name"].Value == "categories")
                            {
                                foreach (XmlNode catNode in child)
                                {
                                    if (catNode.Name == "boolean" && catNode.Attributes["value"].Value == "true")
                                    {
                                        if (table.Data.Contains("categories"))
                                        {
                                            table.Data["categories"] = (string)table.Data["categories"] + ";" + catNode.Attributes["name"].Value;
                                        }
                                        else 
                                        {
                                            table.Data["categories"] = catNode.Attributes["name"].Value;
                                        }
                                    }
                                }
                            }
                            break;
                    }
                }
                tables.Add(table);
            }

            BlogXData data = new BlogXData();
            
            foreach (RadioTable table in tables)
            {
                DateTime date = table.When.Date;
                DayEntry dayEntry = null;
                foreach (DayEntry target in data.Days)
                {
                    if (target.Date == date)
                    {
                        dayEntry = target;
                        break;
                    }
                }
                if (dayEntry == null)
                {
                    dayEntry = new DayEntry();
                    dayEntry.Date = date;
                    data.Days.Add(dayEntry);
                }
                
                Entry entry = new Entry();
                entry.Created = table.When;
                entry.Title = table.Title;
                entry.Content = table.Text;
                entry.Categories = table.Categories;
                entry.EntryId = table.UniqueId;
                dayEntry.Entries.Add(entry);
                data.IncrementEntryChange();
            }

            Console.WriteLine("Saving entries...");
            foreach (DayEntry day in data.Days)
            {
                day.Save();
            }

            Console.WriteLine("Creating comment cache...");
            CategoryCache cache = new CategoryCache();
            cache.Ensure(data);

            Console.WriteLine("Creating entry cache...");
            EntryIdCache ecache = new EntryIdCache();
            ecache.Ensure(data);

            return 0;
        }
    }

    class RadioTable
    {
        Hashtable data = new Hashtable();
        string name;

        public RadioTable() {}

        public string Name { get { return name; } set { name = value; } }
        public string UniqueId { get { return int.Parse(name).ToString(); } }
        public IDictionary Data { get { return data; } }
        public DateTime When { get { return (DateTime)Data["when"]; } }
        public string Text { get { return (string)Data["text"]; } }
        public string Title { get { return (string)Data["title"]; } }
        public string Categories { get { return (string)Data["categories"]; } }
    }
}
