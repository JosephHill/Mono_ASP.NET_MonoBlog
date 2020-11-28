//=-------
// Copyright 2003, Chris Anderson
// 
//   Provided as is, with no warrenty, etc.
//   License is granted to use, copy, modify, 
//   with or without credit to me, just don't
//   blame me if it doesn't work.
//=-------
namespace Anderson.Chris.BlogX.Runtime
{
    using System;
    using System.Xml.Serialization;
    using System.IO;

    public delegate string ResolveFileCallback(string file);

    public class BlogXData
    {
        DayEntryCollection entries;
        BlogCoreData data;

        public static ResolveFileCallback Resolver = null;

        public static string ResolvePath(string file)
        {
            return Resolver(file);
        }

        public BlogXData()
        {
        }
                
        public DayEntryCollection Days 
        { 
            get 
            { 
                if (entries == null)
                {
                    entries = new DayEntryCollection();
                    foreach (FileInfo fi in new DirectoryInfo(BlogXData.ResolvePath(".")).GetFiles("*.dayentry.xml"))
                    {
                        string fileName = fi.Name;
                        DayEntry day = new DayEntry();
                        day.Date = DateTime.Parse(fileName.Substring(0, fileName.Length - ".dayentry.xml".Length));
                        entries.Add(day);
                    }
                }
                return entries; 
            } 
        }

        public DayExtra GetDayExtra(DateTime day)
        {
            DayExtra d = new DayExtra();
            d.Date = day;
            return d;
        }

        public long CurrentEntryChangeCount 
        { 
            get 
            { 
                LoadBlogCore();
                return data.EntryChangeCount;
            }
        }

        public long CurrentExtraChangeCount 
        { 
            get 
            { 
                LoadBlogCore();
                return data.ExtraChangeCount;
            }
        }

        public void IncrementEntryChange()
        {
            LoadBlogCore();
            data.EntryChangeCount++;
            SaveBlogCore();
        }

        public void IncrementExtraChange()
        {
            LoadBlogCore();
            data.ExtraChangeCount++;
            SaveBlogCore();
        }

        void LoadBlogCore()
        {
            string fullPath = BlogXData.ResolvePath("blogdata.xml");
            if (File.Exists(fullPath))
            {
                using (StreamReader reader = new StreamReader(fullPath))
                {
                    XmlSerializer ser = new XmlSerializer(typeof(BlogCoreData));
                    data = (BlogCoreData)ser.Deserialize(reader);
                }
            }
            else 
            {
                data = new BlogCoreData();
                SaveBlogCore();
            }
        }

        void SaveBlogCore()
        {
            using (StreamWriter writer = new StreamWriter(BlogXData.ResolvePath("blogdata.xml")))
            {
                XmlSerializer ser = new XmlSerializer(typeof(BlogCoreData));
                ser.Serialize(writer, data);
            }
        }

        #region DummyData
        public static void CreateDummyData()
        {
            for (int year=2000; year<2003; year++) 
            {
                for (int day=1; day<28; day++) 
                {
                    for (int month=1; month<13; month++) 
                    {
                        DayEntry dayEntry = new DayEntry();
                        dayEntry.Initialize();
                        dayEntry.Date = new DateTime(year, month, day);

                        DayExtra dayExtra = new DayExtra();
                        dayExtra.Initialize();
                        dayExtra.Date = new DateTime(year, month, day);

                        for (int i=0; i<18; i++)
                        {

                            Entry e = new Entry();
                            e.Initialize();
                            e.Content = dayEntry.Date.DayOfWeek.ToString() + @"<br /> " + e.EntryId + @"
Hello <i>world</i> Hello <i>world</i> 
Hello <i>world</i> Hello <i>world</i> Hello <i>world</i> Hello <i>world</i> 
Hello <i>world</i> Hello <i>world</i> Hello <i>world</i> Hello <i>world</i> 
Hello <i>world</i> Hello <i>world</i> Hello <i>world</i> Hello <i>world</i> 
Hello <i>world</i> Hello <i>world</i> Hello <i>world</i> Hello <i>world</i> 
Hello <i>world</i> Hello <i>world</i> Hello <i>world</i> Hello <i>world</i> 
Hello <i>world</i> Hello <i>world</i> Hello <i>world</i> Hello <i>world</i> 
Hello <i>world</i> Hello <i>world</i> Hello <i>world</i> Hello <i>world</i> 
Hello <i>world</i> Hello <i>world</i> Hello <i>world</i> Hello <i>world</i> 
Hello <i>world</i> Hello <i>world</i> Hello <i>world</i> Hello <i>world</i> 
Hello <i>world</i> Hello <i>world</i> Hello <i>world</i> Hello <i>world</i> 
Hello <i>world</i> Hello <i>world</i> Hello <i>world</i> Hello <i>world</i> 
item #" + i;
                            e.Title = "["+ dayEntry.Date.DayOfWeek.ToString() + "] title " + i;
                            switch (i % 4)
                            {
                                case 0:
                                    e.Categories = "Software";
                                    break;
                                case 1:
                                    e.Categories = "Borg;";
                                    break;
                                case 2:
                                    e.Categories = "Software;Borg;";
                                    break;
                                case 3:
                                    e.Categories = "";
                                    break;
                            }
                            dayEntry.Entries.Add(e);

                            for (int j=0; j<(i/3); j++)
                            {
                                Comment c = new Comment();
                                c.Initialize();
                                c.TargetEntryId = e.EntryId;
                                c.Author = "Chris Anderson";
                                c.AuthorEmail="chris_l_anderson@hotmail.com";
                                c.AuthorHomepage="http://www.simplegeek.com";
                                c.Content = j + " this is <i>a</i> comment. this is <i>a</i> comment. this is <i>a</i> comment. this is <i>a</i> comment. this is <i>a</i> comment. this is <i>a</i> comment. this is <i>a</i> comment. this is <i>a</i> comment.";
                                dayExtra.Comments.Add(c);
                            }
                        }

                        dayEntry.Save();
                        dayExtra.Save();
                    }
                }
            }
        }
        #endregion
    }
}
