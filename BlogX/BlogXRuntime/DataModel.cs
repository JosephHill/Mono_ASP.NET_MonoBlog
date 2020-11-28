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
    using System.Collections;

    public class BlogCoreData
    {
        long entryChangeCount;
        long extraChangeCount;

        [XmlIgnore]
        public string FileName { get { return "blogdata.xml"; } }
        public long EntryChangeCount { get { return entryChangeCount; } set { entryChangeCount = value; } }
        public long ExtraChangeCount { get { return extraChangeCount; } set { extraChangeCount = value; } }
    }

    #region EntryId
    public class EntryIdCache
    {
        long changeNumber;

        EntryIdCacheEntryCollection entries = new EntryIdCacheEntryCollection();
        [XmlIgnore]
        public string FileName { get { return "entryCache.xml"; } }

        public long ChangeNumber { get { return changeNumber; } set { changeNumber = value; } }
        public EntryIdCacheEntryCollection Entries { get { return entries; } set { entries = value; } }

        public void Ensure(BlogXData data)
        {
            Load();
            if (changeNumber != data.CurrentEntryChangeCount)
            {
                Build(data);
                Save();
            }
        }

        public void Build(BlogXData data)
        {
            changeNumber = data.CurrentEntryChangeCount;
            Hashtable build = new Hashtable();
            entries.Clear();

            foreach (DayEntry day in data.Days)
            {
                day.Load();

                foreach (Entry entry in day.Entries)
                {
                    EntryIdCacheEntry ec = new EntryIdCacheEntry();
                    ec.Date = day.Date;
                    ec.EntryId = entry.EntryId;
                    entries.Add(ec);
                }
            }
        }

        public void Load()
        {
            string fullPath = BlogXData.ResolvePath(FileName);
            if (File.Exists(fullPath))
            {
                XmlSerializer ser = new XmlSerializer(typeof(EntryIdCache));
                using (StreamReader reader = new StreamReader(fullPath))
                {
                    EntryIdCache e = (EntryIdCache)ser.Deserialize(reader);
                    this.entries = e.entries;
                    this.changeNumber = e.ChangeNumber;
                }
            }
        }
        public void Save()
        {
            XmlSerializer ser = new XmlSerializer(typeof(EntryIdCache));
            using (StreamWriter writer = new StreamWriter(BlogXData.ResolvePath(FileName)))
            {
                ser.Serialize(writer, this);
            }
        }
    }
    public class EntryIdCacheEntry
    {
        string entryId;
        DateTime date;

        public string EntryId { get { return entryId; } set { entryId = value; } }
        public DateTime Date { get { return date; } set { date = value; } }
    }
    #endregion

    #region Category
    public class CategoryCache
    {
        long changeNumber;
        CategoryCacheEntryCollection entries = new CategoryCacheEntryCollection();

        [XmlIgnore]
        public string FileName { get { return "categoryCache.xml"; } }

        public long ChangeNumber { get { return changeNumber; } set { changeNumber = value; } }
        public CategoryCacheEntryCollection Entries { get { return entries; } set { entries = value; } }

        public void Ensure(BlogXData data)
        {
            Load();
            if (changeNumber != data.CurrentEntryChangeCount)
            {
                Build(data);
                Save();
            }
        }

        public void Build(BlogXData data)
        {
            changeNumber = data.CurrentEntryChangeCount;
            Hashtable build = new Hashtable();

            foreach (DayEntry day in data.Days)
            {
                day.Load();

                foreach (Entry entry in day.Entries)
                {
                    foreach (string cat in entry.GetSplitCategories())
                    {
                        if (!build.Contains(cat))
                        {
                            build[cat] = new CategoryCacheEntryDetailCollection();
                        }
                        CategoryCacheEntryDetail entryDetail = new CategoryCacheEntryDetail();
                        entryDetail.DayDate = day.Date;
                        entryDetail.EntryId = entry.EntryId;

                        ((CategoryCacheEntryDetailCollection)(build[cat])).Add(entryDetail);
                    }
                }
            }

            entries.Clear();
            foreach (DictionaryEntry de in build)
            {
                CategoryCacheEntry entry = new CategoryCacheEntry();
                entry.Name = (string)de.Key;
                entry.EntryDetails = (CategoryCacheEntryDetailCollection)de.Value;
                entries.Add(entry);
            }
        }

        public void Load()
        {
            string fullPath = BlogXData.ResolvePath(FileName);
            if (File.Exists(fullPath))
            {
                XmlSerializer ser = new XmlSerializer(typeof(CategoryCache));
                using (StreamReader reader = new StreamReader(fullPath))
                {
                    CategoryCache e = (CategoryCache)ser.Deserialize(reader);
                    this.entries = e.entries;
                    this.changeNumber = e.ChangeNumber;
                }
            }
        }
        public void Save()
        {
            XmlSerializer ser = new XmlSerializer(typeof(CategoryCache));
            using (StreamWriter writer = new StreamWriter(BlogXData.ResolvePath(FileName)))
            {
                ser.Serialize(writer, this);
            }
        }
    }

    public class CategoryCacheEntry
    {
        string name;
        CategoryCacheEntryDetailCollection entryDetails = new CategoryCacheEntryDetailCollection();

        public string Name { get { return name; } set { name = value; } }
        public CategoryCacheEntryDetailCollection EntryDetails { get { return entryDetails; } set { entryDetails = value; } }
    }

    public class CategoryCacheEntryDetail
    {
        string entryId;
        DateTime dayDate;

        public string EntryId { get { return entryId; } set { entryId = value; } }
        public DateTime DayDate { get { return dayDate; } set { dayDate = value.Date; } }
    }
    #endregion

    #region Core Entry
    public class EntryBase
    {
        string content;
        DateTime created;
        DateTime modified;
        string entryId;

        public string Content { get { return content; } set { content = value; } }
        public DateTime Created { get { return created; } set { created = value; } }
        public DateTime Modified { get { return modified; } set { modified = value; } }
        public string EntryId { get { return entryId; } set { entryId = value; } }

        public void Initialize()
        {
            Created = DateTime.Now;
            Modified = DateTime.Now;
            EntryId = Guid.NewGuid().ToString();
        }

        public void Modify()
        {
            Modified = DateTime.Now;
        }
    }

    public class DayEntry
    {
        EntryCollection entries = new EntryCollection();
        DateTime date;
        bool loaded;

        public string FileName { get { return date.ToString("yyyy-MM-dd") + ".dayentry.xml"; } }

        public DateTime Date { get { return date; } set { date = value.Date; } }
        public EntryCollection Entries { get { return entries; } }
        public void Initialize()
        {
            date = DateTime.Now.Date;
        }
        public void Load()
        {
            if (entries.Count > 0) loaded = true;
            if (loaded) return;

            if (BlogXData.Resolver != null)
            {
                XmlSerializer ser = new XmlSerializer(typeof(DayEntry));
                using (StreamReader reader = new StreamReader(BlogXData.ResolvePath(FileName)))
                {
                    DayEntry e = (DayEntry)ser.Deserialize(reader);
                    this.entries = e.entries;
                }
            }
            loaded = true;
        }
        public void Save()
        {
            XmlSerializer ser = new XmlSerializer(typeof(DayEntry));
            using (StreamWriter writer = new StreamWriter(BlogXData.ResolvePath(FileName)))
            {
                ser.Serialize(writer, this);
            }
        }
    }

    public class Entry : EntryBase
    {
        string title;
        string description;
        string categories;
        public string Description { get { return description; } set { description = value; } }
        public string Title { get { return title; } set { title = value; } }
        public string Categories { get { return categories; } set { categories = value; } }
        
        public string[] GetSplitCategories()
        {
            if (categories == null || categories.Length == 0) 
            {
                return new string[0];
            }
            else
            {
                return Categories.Split(';');
            }
        }
    }
    #endregion

    public class LogDataItem
    {
        string urlRequested;
        string urlReferrer;
        string userAgent;
        DateTime requested;

        public string UrlRequested { get { return urlRequested; } set { urlRequested = value; } }
        public string UrlReferrer { get { return urlReferrer; } set { urlReferrer = value; } }
        public string UserAgent { get { return userAgent; } set { userAgent = value; } }
        public DateTime Requested { get { return requested; } set { requested = value; } }
    }

    public class DayExtra
    {
        CommentCollection comments = new CommentCollection();
        DateTime date;
        bool loaded;

        public string FileName { get { return date.ToString("yyyy-MM-dd") + ".dayextra.xml"; } }
        public DateTime Date { get { return date; } set { date = value.Date; } }
        public CommentCollection Comments { get { return comments; } }
        public void Initialize()
        {
            date = DateTime.Now.Date;
        }

        public CommentCollection GetCommentsFor(string entryId)
        {
            Load();
            CommentCollection filtered = new CommentCollection();

            foreach (Comment c in Comments)
            {
                if (c.TargetEntryId == entryId)
                {
                    filtered.Add(c);
                }
            }
            return filtered;
        }

        public void Load()
        {
            if (comments.Count > 0) loaded = true;
            if (loaded) return;

            if (BlogXData.Resolver != null)
            {
                string fullPath = BlogXData.ResolvePath(FileName);
                if (File.Exists(fullPath))
                {
                    XmlSerializer ser = new XmlSerializer(typeof(DayExtra));
                    using (StreamReader reader = new StreamReader(fullPath))
                    {
                        DayExtra e = (DayExtra)ser.Deserialize(reader);
                        this.comments = e.comments;
                    }
                }
            }
            loaded = true;
        }
        public void Save()
        {
            XmlSerializer ser = new XmlSerializer(typeof(DayExtra));
            using (StreamWriter writer = new StreamWriter(BlogXData.ResolvePath(FileName)))
            {
                ser.Serialize(writer, this);
            }
        }
    }

    public class Comment : EntryBase
    {
        string author;
        string authorEmail;
        string authorHomepage;
        string targetEntryId;

        public string TargetEntryId { get { return targetEntryId; } set { targetEntryId = value; } }
        public string Author { get { return author; } set { author = value; } }
        public string AuthorEmail { get { return authorEmail; } set { authorEmail = value; } }
        public string AuthorHomepage { get { return authorHomepage; } set { authorHomepage = value; } }
    }
}
