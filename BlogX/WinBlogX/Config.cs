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
    using System.Reflection;
    using System.IO;
    using System.Drawing;
    using System.Collections;
    using System.ComponentModel;
    using System.Windows.Forms;
    using System.Xml.Serialization;

    public class WinBlogXConfig
    {
        StartDialogConfig startDialog = new StartDialogConfig();
        public StartDialogConfig StartDialog { get { return startDialog; } set { startDialog = value; } }

        static string FileName { get { return new Uri(new Uri(Assembly.GetEntryAssembly().EscapedCodeBase), "WinBlogXSettings.config").LocalPath; } } 

        public static WinBlogXConfig Load()
        {
            
            XmlSerializer ser = new XmlSerializer(typeof(WinBlogXConfig));
            if (File.Exists(FileName))
            {
                using (StreamReader reader = new StreamReader(FileName))
                {
                    return (WinBlogXConfig)ser.Deserialize(reader);
                }
            }
            else
            {
                return new WinBlogXConfig();
            }
            
        }
        public static void Save(WinBlogXConfig config)
        {
            XmlSerializer ser = new XmlSerializer(typeof(WinBlogXConfig));
            using (StreamWriter writer = new StreamWriter(FileName))
            {
                ser.Serialize(writer, config);
            }
        }
    }

    public class StartDialogConfig
    {
        StringCollection mru = new StringCollection();

        public StringCollection Mru { get { return mru; } set { mru = value; } }

        public void AddMru(string path)
        {
            if (!Mru.Contains(path))
            {
                Mru.Insert(0, path);
            }
        }
    }

    #region Collections
    /// <summary>
    /// A collection of elements of type String
    /// </summary>
    public class StringCollection: System.Collections.CollectionBase
    {
        /// <summary>
        /// Initializes a new empty instance of the StringCollection class.
        /// </summary>
        public StringCollection()
        {
            // empty
        }

        /// <summary>
        /// Initializes a new instance of the StringCollection class, containing elements
        /// copied from an array.
        /// </summary>
        /// <param name="items">
        /// The array whose elements are to be added to the new StringCollection.
        /// </param>
        public StringCollection(String[] items)
        {
            this.AddRange(items);
        }

        /// <summary>
        /// Initializes a new instance of the StringCollection class, containing elements
        /// copied from another instance of StringCollection
        /// </summary>
        /// <param name="items">
        /// The StringCollection whose elements are to be added to the new StringCollection.
        /// </param>
        public StringCollection(StringCollection items)
        {
            this.AddRange(items);
        }

        /// <summary>
        /// Adds the elements of an array to the end of this StringCollection.
        /// </summary>
        /// <param name="items">
        /// The array whose elements are to be added to the end of this StringCollection.
        /// </param>
        public virtual void AddRange(String[] items)
        {
            foreach (String item in items)
            {
                this.List.Add(item);
            }
        }

        /// <summary>
        /// Adds the elements of another StringCollection to the end of this StringCollection.
        /// </summary>
        /// <param name="items">
        /// The StringCollection whose elements are to be added to the end of this StringCollection.
        /// </param>
        public virtual void AddRange(StringCollection items)
        {
            foreach (String item in items)
            {
                this.List.Add(item);
            }
        }

        /// <summary>
        /// Adds an instance of type String to the end of this StringCollection.
        /// </summary>
        /// <param name="value">
        /// The String to be added to the end of this StringCollection.
        /// </param>
        public virtual void Add(String value)
        {
            this.List.Add(value);
        }

        /// <summary>
        /// Determines whether a specfic String value is in this StringCollection.
        /// </summary>
        /// <param name="value">
        /// The String value to locate in this StringCollection.
        /// </param>
        /// <returns>
        /// true if value is found in this StringCollection;
        /// false otherwise.
        /// </returns>
        public virtual bool Contains(String value)
        {
            return this.List.Contains(value);
        }

        /// <summary>
        /// Return the zero-based index of the first occurrence of a specific value
        /// in this StringCollection
        /// </summary>
        /// <param name="value">
        /// The String value to locate in the StringCollection.
        /// </param>
        /// <returns>
        /// The zero-based index of the first occurrence of the _ELEMENT value if found;
        /// -1 otherwise.
        /// </returns>
        public virtual int IndexOf(String value)
        {
            return this.List.IndexOf(value);
        }

        /// <summary>
        /// Inserts an element into the StringCollection at the specified index
        /// </summary>
        /// <param name="index">
        /// The index at which the String is to be inserted.
        /// </param>
        /// <param name="value">
        /// The String to insert.
        /// </param>
        public virtual void Insert(int index, String value)
        {
            this.List.Insert(index, value);
        }

        /// <summary>
        /// Gets or sets the String at the given index in this StringCollection.
        /// </summary>
        public virtual String this[int index]
        {
            get
            {
                return (String) this.List[index];
            }
            set
            {
                this.List[index] = value;
            }
        }

        /// <summary>
        /// Removes the first occurrence of a specific String from this StringCollection.
        /// </summary>
        /// <param name="value">
        /// The String value to remove from this StringCollection.
        /// </param>
        public virtual void Remove(String value)
        {
            this.List.Remove(value);
        }

        /// <summary>
        /// Type-specific enumeration class, used by StringCollection.GetEnumerator.
        /// </summary>
        public class Enumerator: System.Collections.IEnumerator
        {
            private System.Collections.IEnumerator wrapped;

            public Enumerator(StringCollection collection)
            {
                this.wrapped = ((System.Collections.CollectionBase)collection).GetEnumerator();
            }

            public String Current
            {
                get
                {
                    return (String) (this.wrapped.Current);
                }
            }

            object System.Collections.IEnumerator.Current
            {
                get
                {
                    return (String) (this.wrapped.Current);
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
        /// Returns an enumerator that can iterate through the elements of this StringCollection.
        /// </summary>
        /// <returns>
        /// An object that implements System.Collections.IEnumerator.
        /// </returns>        
        public new virtual StringCollection.Enumerator GetEnumerator()
        {
            return new StringCollection.Enumerator(this);
        }
    }
    #endregion
}
