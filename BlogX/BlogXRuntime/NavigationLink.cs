namespace Anderson.Chris.BlogX.Runtime
{
    using System;
    using System.Xml.Serialization;
    using System.Collections;

    [XmlRoot("links")]
    public class NavigationRoot
    {
        NavigationLinkCollection items = new NavigationLinkCollection();

        [XmlElement("link")]
        public NavigationLinkCollection Items { get { return items; } set { items = value; } }
    }

    public class NavigationLink
    {
        string name;
        string url;

        [XmlElement("name")]
        public string Name { get { return name; } set { name = value; } }
        [XmlElement("url")]
        public string Url { get { return url; } set { url = value; } } 
    }
    #region Collection
    /// <summary>
    /// A collection of elements of type NavigationLink
    /// </summary>
    public class NavigationLinkCollection: System.Collections.CollectionBase
    {
        /// <summary>
        /// Initializes a new empty instance of the NavigationLinkCollection class.
        /// </summary>
        public NavigationLinkCollection()
        {
            // empty
        }

        /// <summary>
        /// Initializes a new instance of the NavigationLinkCollection class, containing elements
        /// copied from an array.
        /// </summary>
        /// <param name="items">
        /// The array whose elements are to be added to the new NavigationLinkCollection.
        /// </param>
        public NavigationLinkCollection(NavigationLink[] items)
        {
            this.AddRange(items);
        }

        /// <summary>
        /// Initializes a new instance of the NavigationLinkCollection class, containing elements
        /// copied from another instance of NavigationLinkCollection
        /// </summary>
        /// <param name="items">
        /// The NavigationLinkCollection whose elements are to be added to the new NavigationLinkCollection.
        /// </param>
        public NavigationLinkCollection(NavigationLinkCollection items)
        {
            this.AddRange(items);
        }

        /// <summary>
        /// Adds the elements of an array to the end of this NavigationLinkCollection.
        /// </summary>
        /// <param name="items">
        /// The array whose elements are to be added to the end of this NavigationLinkCollection.
        /// </param>
        public virtual void AddRange(NavigationLink[] items)
        {
            foreach (NavigationLink item in items)
            {
                this.List.Add(item);
            }
        }

        /// <summary>
        /// Adds the elements of another NavigationLinkCollection to the end of this NavigationLinkCollection.
        /// </summary>
        /// <param name="items">
        /// The NavigationLinkCollection whose elements are to be added to the end of this NavigationLinkCollection.
        /// </param>
        public virtual void AddRange(NavigationLinkCollection items)
        {
            foreach (NavigationLink item in items)
            {
                this.List.Add(item);
            }
        }

        /// <summary>
        /// Adds an instance of type NavigationLink to the end of this NavigationLinkCollection.
        /// </summary>
        /// <param name="value">
        /// The NavigationLink to be added to the end of this NavigationLinkCollection.
        /// </param>
        public virtual void Add(NavigationLink value)
        {
            this.List.Add(value);
        }

        /// <summary>
        /// Determines whether a specfic NavigationLink value is in this NavigationLinkCollection.
        /// </summary>
        /// <param name="value">
        /// The NavigationLink value to locate in this NavigationLinkCollection.
        /// </param>
        /// <returns>
        /// true if value is found in this NavigationLinkCollection;
        /// false otherwise.
        /// </returns>
        public virtual bool Contains(NavigationLink value)
        {
            return this.List.Contains(value);
        }

        /// <summary>
        /// Return the zero-based index of the first occurrence of a specific value
        /// in this NavigationLinkCollection
        /// </summary>
        /// <param name="value">
        /// The NavigationLink value to locate in the NavigationLinkCollection.
        /// </param>
        /// <returns>
        /// The zero-based index of the first occurrence of the _ELEMENT value if found;
        /// -1 otherwise.
        /// </returns>
        public virtual int IndexOf(NavigationLink value)
        {
            return this.List.IndexOf(value);
        }

        /// <summary>
        /// Inserts an element into the NavigationLinkCollection at the specified index
        /// </summary>
        /// <param name="index">
        /// The index at which the NavigationLink is to be inserted.
        /// </param>
        /// <param name="value">
        /// The NavigationLink to insert.
        /// </param>
        public virtual void Insert(int index, NavigationLink value)
        {
            this.List.Insert(index, value);
        }

        /// <summary>
        /// Gets or sets the NavigationLink at the given index in this NavigationLinkCollection.
        /// </summary>
        public virtual NavigationLink this[int index]
        {
            get
            {
                return (NavigationLink) this.List[index];
            }
            set
            {
                this.List[index] = value;
            }
        }

        /// <summary>
        /// Removes the first occurrence of a specific NavigationLink from this NavigationLinkCollection.
        /// </summary>
        /// <param name="value">
        /// The NavigationLink value to remove from this NavigationLinkCollection.
        /// </param>
        public virtual void Remove(NavigationLink value)
        {
            this.List.Remove(value);
        }

        /// <summary>
        /// Type-specific enumeration class, used by NavigationLinkCollection.GetEnumerator.
        /// </summary>
        public class Enumerator: System.Collections.IEnumerator
        {
            private System.Collections.IEnumerator wrapped;

            public Enumerator(NavigationLinkCollection collection)
            {
                this.wrapped = ((System.Collections.CollectionBase)collection).GetEnumerator();
            }

            public NavigationLink Current
            {
                get
                {
                    return (NavigationLink) (this.wrapped.Current);
                }
            }

            object System.Collections.IEnumerator.Current
            {
                get
                {
                    return (NavigationLink) (this.wrapped.Current);
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
        /// Returns an enumerator that can iterate through the elements of this NavigationLinkCollection.
        /// </summary>
        /// <returns>
        /// An object that implements System.Collections.IEnumerator.
        /// </returns>        
        public new virtual NavigationLinkCollection.Enumerator GetEnumerator()
        {
            return new NavigationLinkCollection.Enumerator(this);
        }
    }
    #endregion
}
