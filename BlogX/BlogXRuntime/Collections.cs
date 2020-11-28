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

    /// <summary>
    /// A collection of elements of type EntryIdCacheEntry
    /// </summary>
    public class EntryIdCacheEntryCollection: System.Collections.CollectionBase
    {
        /// <summary>
        /// Initializes a new empty instance of the EntryIdCacheEntryCollection class.
        /// </summary>
        public EntryIdCacheEntryCollection()
        {
            // empty
        }

        /// <summary>
        /// Initializes a new instance of the EntryIdCacheEntryCollection class, containing elements
        /// copied from an array.
        /// </summary>
        /// <param name="items">
        /// The array whose elements are to be added to the new EntryIdCacheEntryCollection.
        /// </param>
        public EntryIdCacheEntryCollection(EntryIdCacheEntry[] items)
        {
            this.AddRange(items);
        }

        /// <summary>
        /// Initializes a new instance of the EntryIdCacheEntryCollection class, containing elements
        /// copied from another instance of EntryIdCacheEntryCollection
        /// </summary>
        /// <param name="items">
        /// The EntryIdCacheEntryCollection whose elements are to be added to the new EntryIdCacheEntryCollection.
        /// </param>
        public EntryIdCacheEntryCollection(EntryIdCacheEntryCollection items)
        {
            this.AddRange(items);
        }

        /// <summary>
        /// Adds the elements of an array to the end of this EntryIdCacheEntryCollection.
        /// </summary>
        /// <param name="items">
        /// The array whose elements are to be added to the end of this EntryIdCacheEntryCollection.
        /// </param>
        public virtual void AddRange(EntryIdCacheEntry[] items)
        {
            foreach (EntryIdCacheEntry item in items)
            {
                this.List.Add(item);
            }
        }

        /// <summary>
        /// Adds the elements of another EntryIdCacheEntryCollection to the end of this EntryIdCacheEntryCollection.
        /// </summary>
        /// <param name="items">
        /// The EntryIdCacheEntryCollection whose elements are to be added to the end of this EntryIdCacheEntryCollection.
        /// </param>
        public virtual void AddRange(EntryIdCacheEntryCollection items)
        {
            foreach (EntryIdCacheEntry item in items)
            {
                this.List.Add(item);
            }
        }

        /// <summary>
        /// Adds an instance of type EntryIdCacheEntry to the end of this EntryIdCacheEntryCollection.
        /// </summary>
        /// <param name="value">
        /// The EntryIdCacheEntry to be added to the end of this EntryIdCacheEntryCollection.
        /// </param>
        public virtual void Add(EntryIdCacheEntry value)
        {
            this.List.Add(value);
        }

        /// <summary>
        /// Determines whether a specfic EntryIdCacheEntry value is in this EntryIdCacheEntryCollection.
        /// </summary>
        /// <param name="value">
        /// The EntryIdCacheEntry value to locate in this EntryIdCacheEntryCollection.
        /// </param>
        /// <returns>
        /// true if value is found in this EntryIdCacheEntryCollection;
        /// false otherwise.
        /// </returns>
        public virtual bool Contains(EntryIdCacheEntry value)
        {
            return this.List.Contains(value);
        }

        /// <summary>
        /// Return the zero-based index of the first occurrence of a specific value
        /// in this EntryIdCacheEntryCollection
        /// </summary>
        /// <param name="value">
        /// The EntryIdCacheEntry value to locate in the EntryIdCacheEntryCollection.
        /// </param>
        /// <returns>
        /// The zero-based index of the first occurrence of the _ELEMENT value if found;
        /// -1 otherwise.
        /// </returns>
        public virtual int IndexOf(EntryIdCacheEntry value)
        {
            return this.List.IndexOf(value);
        }

        /// <summary>
        /// Inserts an element into the EntryIdCacheEntryCollection at the specified index
        /// </summary>
        /// <param name="index">
        /// The index at which the EntryIdCacheEntry is to be inserted.
        /// </param>
        /// <param name="value">
        /// The EntryIdCacheEntry to insert.
        /// </param>
        public virtual void Insert(int index, EntryIdCacheEntry value)
        {
            this.List.Insert(index, value);
        }

        /// <summary>
        /// Gets or sets the EntryIdCacheEntry at the given index in this EntryIdCacheEntryCollection.
        /// </summary>
        public virtual EntryIdCacheEntry this[int index]
        {
            get
            {
                return (EntryIdCacheEntry) this.List[index];
            }
            set
            {
                this.List[index] = value;
            }
        }

        /// <summary>
        /// Removes the first occurrence of a specific EntryIdCacheEntry from this EntryIdCacheEntryCollection.
        /// </summary>
        /// <param name="value">
        /// The EntryIdCacheEntry value to remove from this EntryIdCacheEntryCollection.
        /// </param>
        public virtual void Remove(EntryIdCacheEntry value)
        {
            this.List.Remove(value);
        }

        /// <summary>
        /// Type-specific enumeration class, used by EntryIdCacheEntryCollection.GetEnumerator.
        /// </summary>
        public class Enumerator: System.Collections.IEnumerator
        {
            private System.Collections.IEnumerator wrapped;

            public Enumerator(EntryIdCacheEntryCollection collection)
            {
                this.wrapped = ((System.Collections.CollectionBase)collection).GetEnumerator();
            }

            public EntryIdCacheEntry Current
            {
                get
                {
                    return (EntryIdCacheEntry) (this.wrapped.Current);
                }
            }

            object System.Collections.IEnumerator.Current
            {
                get
                {
                    return (EntryIdCacheEntry) (this.wrapped.Current);
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
        /// Returns an enumerator that can iterate through the elements of this EntryIdCacheEntryCollection.
        /// </summary>
        /// <returns>
        /// An object that implements System.Collections.IEnumerator.
        /// </returns>        
        public new virtual EntryIdCacheEntryCollection.Enumerator GetEnumerator()
        {
            return new EntryIdCacheEntryCollection.Enumerator(this);
        }
    }
    /// <summary>
    /// A collection of elements of type Comment
    /// </summary>
    public class CommentCollection: System.Collections.CollectionBase
    {
        /// <summary>
        /// Initializes a new empty instance of the CommentCollection class.
        /// </summary>
        public CommentCollection()
        {
            // empty
        }

        /// <summary>
        /// Initializes a new instance of the CommentCollection class, containing elements
        /// copied from an array.
        /// </summary>
        /// <param name="items">
        /// The array whose elements are to be added to the new CommentCollection.
        /// </param>
        public CommentCollection(Comment[] items)
        {
            this.AddRange(items);
        }

        /// <summary>
        /// Initializes a new instance of the CommentCollection class, containing elements
        /// copied from another instance of CommentCollection
        /// </summary>
        /// <param name="items">
        /// The CommentCollection whose elements are to be added to the new CommentCollection.
        /// </param>
        public CommentCollection(CommentCollection items)
        {
            this.AddRange(items);
        }

        /// <summary>
        /// Adds the elements of an array to the end of this CommentCollection.
        /// </summary>
        /// <param name="items">
        /// The array whose elements are to be added to the end of this CommentCollection.
        /// </param>
        public virtual void AddRange(Comment[] items)
        {
            foreach (Comment item in items)
            {
                this.List.Add(item);
            }
        }

        /// <summary>
        /// Adds the elements of another CommentCollection to the end of this CommentCollection.
        /// </summary>
        /// <param name="items">
        /// The CommentCollection whose elements are to be added to the end of this CommentCollection.
        /// </param>
        public virtual void AddRange(CommentCollection items)
        {
            foreach (Comment item in items)
            {
                this.List.Add(item);
            }
        }

        /// <summary>
        /// Adds an instance of type Comment to the end of this CommentCollection.
        /// </summary>
        /// <param name="value">
        /// The Comment to be added to the end of this CommentCollection.
        /// </param>
        public virtual void Add(Comment value)
        {
            this.List.Add(value);
        }

        /// <summary>
        /// Determines whether a specfic Comment value is in this CommentCollection.
        /// </summary>
        /// <param name="value">
        /// The Comment value to locate in this CommentCollection.
        /// </param>
        /// <returns>
        /// true if value is found in this CommentCollection;
        /// false otherwise.
        /// </returns>
        public virtual bool Contains(Comment value)
        {
            return this.List.Contains(value);
        }

        /// <summary>
        /// Return the zero-based index of the first occurrence of a specific value
        /// in this CommentCollection
        /// </summary>
        /// <param name="value">
        /// The Comment value to locate in the CommentCollection.
        /// </param>
        /// <returns>
        /// The zero-based index of the first occurrence of the _ELEMENT value if found;
        /// -1 otherwise.
        /// </returns>
        public virtual int IndexOf(Comment value)
        {
            return this.List.IndexOf(value);
        }

        /// <summary>
        /// Inserts an element into the CommentCollection at the specified index
        /// </summary>
        /// <param name="index">
        /// The index at which the Comment is to be inserted.
        /// </param>
        /// <param name="value">
        /// The Comment to insert.
        /// </param>
        public virtual void Insert(int index, Comment value)
        {
            this.List.Insert(index, value);
        }

        /// <summary>
        /// Gets or sets the Comment at the given index in this CommentCollection.
        /// </summary>
        public virtual Comment this[int index]
        {
            get
            {
                return (Comment) this.List[index];
            }
            set
            {
                this.List[index] = value;
            }
        }

        /// <summary>
        /// Removes the first occurrence of a specific Comment from this CommentCollection.
        /// </summary>
        /// <param name="value">
        /// The Comment value to remove from this CommentCollection.
        /// </param>
        public virtual void Remove(Comment value)
        {
            this.List.Remove(value);
        }

        public void Sort()
        {
            this.InnerList.Sort();
        }
        public void Sort(IComparer comparer)
        {
            this.InnerList.Sort(comparer);
        }

        /// <summary>
        /// Type-specific enumeration class, used by CommentCollection.GetEnumerator.
        /// </summary>
        public class Enumerator: System.Collections.IEnumerator
        {
            private System.Collections.IEnumerator wrapped;

            public Enumerator(CommentCollection collection)
            {
                this.wrapped = ((System.Collections.CollectionBase)collection).GetEnumerator();
            }

            public Comment Current
            {
                get
                {
                    return (Comment) (this.wrapped.Current);
                }
            }

            object System.Collections.IEnumerator.Current
            {
                get
                {
                    return (Comment) (this.wrapped.Current);
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
        /// Returns an enumerator that can iterate through the elements of this CommentCollection.
        /// </summary>
        /// <returns>
        /// An object that implements System.Collections.IEnumerator.
        /// </returns>        
        public new virtual CommentCollection.Enumerator GetEnumerator()
        {
            return new CommentCollection.Enumerator(this);
        }
    }
    /// <summary>
    /// A collection of elements of type Entry
    /// </summary>
    public class EntryCollection: System.Collections.CollectionBase
    {
        /// <summary>
        /// Initializes a new empty instance of the EntryCollection class.
        /// </summary>
        public EntryCollection()
        {
            // empty
        }

        /// <summary>
        /// Initializes a new instance of the EntryCollection class, containing elements
        /// copied from an array.
        /// </summary>
        /// <param name="items">
        /// The array whose elements are to be added to the new EntryCollection.
        /// </param>
        public EntryCollection(Entry[] items)
        {
            this.AddRange(items);
        }

        /// <summary>
        /// Initializes a new instance of the EntryCollection class, containing elements
        /// copied from another instance of EntryCollection
        /// </summary>
        /// <param name="items">
        /// The EntryCollection whose elements are to be added to the new EntryCollection.
        /// </param>
        public EntryCollection(EntryCollection items)
        {
            this.AddRange(items);
        }

        /// <summary>
        /// Adds the elements of an array to the end of this EntryCollection.
        /// </summary>
        /// <param name="items">
        /// The array whose elements are to be added to the end of this EntryCollection.
        /// </param>
        public virtual void AddRange(Entry[] items)
        {
            foreach (Entry item in items)
            {
                this.List.Add(item);
            }
        }

        /// <summary>
        /// Adds the elements of another EntryCollection to the end of this EntryCollection.
        /// </summary>
        /// <param name="items">
        /// The EntryCollection whose elements are to be added to the end of this EntryCollection.
        /// </param>
        public virtual void AddRange(EntryCollection items)
        {
            foreach (Entry item in items)
            {
                this.List.Add(item);
            }
        }

        /// <summary>
        /// Adds an instance of type Entry to the end of this EntryCollection.
        /// </summary>
        /// <param name="value">
        /// The Entry to be added to the end of this EntryCollection.
        /// </param>
        public virtual void Add(Entry value)
        {
            this.List.Add(value);
        }

        /// <summary>
        /// Determines whether a specfic Entry value is in this EntryCollection.
        /// </summary>
        /// <param name="value">
        /// The Entry value to locate in this EntryCollection.
        /// </param>
        /// <returns>
        /// true if value is found in this EntryCollection;
        /// false otherwise.
        /// </returns>
        public virtual bool Contains(Entry value)
        {
            return this.List.Contains(value);
        }

        /// <summary>
        /// Return the zero-based index of the first occurrence of a specific value
        /// in this EntryCollection
        /// </summary>
        /// <param name="value">
        /// The Entry value to locate in the EntryCollection.
        /// </param>
        /// <returns>
        /// The zero-based index of the first occurrence of the _ELEMENT value if found;
        /// -1 otherwise.
        /// </returns>
        public virtual int IndexOf(Entry value)
        {
            return this.List.IndexOf(value);
        }

        /// <summary>
        /// Inserts an element into the EntryCollection at the specified index
        /// </summary>
        /// <param name="index">
        /// The index at which the Entry is to be inserted.
        /// </param>
        /// <param name="value">
        /// The Entry to insert.
        /// </param>
        public virtual void Insert(int index, Entry value)
        {
            this.List.Insert(index, value);
        }

        /// <summary>
        /// Gets or sets the Entry at the given index in this EntryCollection.
        /// </summary>
        public virtual Entry this[int index]
        {
            get
            {
                return (Entry) this.List[index];
            }
            set
            {
                this.List[index] = value;
            }
        }

        /// <summary>
        /// Removes the first occurrence of a specific Entry from this EntryCollection.
        /// </summary>
        /// <param name="value">
        /// The Entry value to remove from this EntryCollection.
        /// </param>
        public virtual void Remove(Entry value)
        {
            this.List.Remove(value);
        }

        public void Sort()
        {
            this.InnerList.Sort();
        }
        public void Sort(IComparer comparer)
        {
            this.InnerList.Sort(comparer);
        }

        /// <summary>
        /// Type-specific enumeration class, used by EntryCollection.GetEnumerator.
        /// </summary>
        public class Enumerator: System.Collections.IEnumerator
        {
            private System.Collections.IEnumerator wrapped;

            public Enumerator(EntryCollection collection)
            {
                this.wrapped = ((System.Collections.CollectionBase)collection).GetEnumerator();
            }

            public Entry Current
            {
                get
                {
                    return (Entry) (this.wrapped.Current);
                }
            }

            object System.Collections.IEnumerator.Current
            {
                get
                {
                    return (Entry) (this.wrapped.Current);
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
        /// Returns an enumerator that can iterate through the elements of this EntryCollection.
        /// </summary>
        /// <returns>
        /// An object that implements System.Collections.IEnumerator.
        /// </returns>        
        public new virtual EntryCollection.Enumerator GetEnumerator()
        {
            return new EntryCollection.Enumerator(this);
        }
    }
    /// <summary>
    /// A collection of elements of type DayEntry
    /// </summary>
    public class DayEntryCollection: System.Collections.CollectionBase
    {
        /// <summary>
        /// Initializes a new empty instance of the DayEntryCollection class.
        /// </summary>
        public DayEntryCollection()
        {
            // empty
        }

        /// <summary>
        /// Initializes a new instance of the DayEntryCollection class, containing elements
        /// copied from an array.
        /// </summary>
        /// <param name="items">
        /// The array whose elements are to be added to the new DayEntryCollection.
        /// </param>
        public DayEntryCollection(DayEntry[] items)
        {
            this.AddRange(items);
        }

        /// <summary>
        /// Initializes a new instance of the DayEntryCollection class, containing elements
        /// copied from another instance of DayEntryCollection
        /// </summary>
        /// <param name="items">
        /// The DayEntryCollection whose elements are to be added to the new DayEntryCollection.
        /// </param>
        public DayEntryCollection(DayEntryCollection items)
        {
            this.AddRange(items);
        }

        /// <summary>
        /// Adds the elements of an array to the end of this DayEntryCollection.
        /// </summary>
        /// <param name="items">
        /// The array whose elements are to be added to the end of this DayEntryCollection.
        /// </param>
        public virtual void AddRange(DayEntry[] items)
        {
            foreach (DayEntry item in items)
            {
                this.List.Add(item);
            }
        }

        /// <summary>
        /// Adds the elements of another DayEntryCollection to the end of this DayEntryCollection.
        /// </summary>
        /// <param name="items">
        /// The DayEntryCollection whose elements are to be added to the end of this DayEntryCollection.
        /// </param>
        public virtual void AddRange(DayEntryCollection items)
        {
            foreach (DayEntry item in items)
            {
                this.List.Add(item);
            }
        }

        /// <summary>
        /// Adds an instance of type DayEntry to the end of this DayEntryCollection.
        /// </summary>
        /// <param name="value">
        /// The DayEntry to be added to the end of this DayEntryCollection.
        /// </param>
        public virtual void Add(DayEntry value)
        {
            this.List.Add(value);
        }

        /// <summary>
        /// Determines whether a specfic DayEntry value is in this DayEntryCollection.
        /// </summary>
        /// <param name="value">
        /// The DayEntry value to locate in this DayEntryCollection.
        /// </param>
        /// <returns>
        /// true if value is found in this DayEntryCollection;
        /// false otherwise.
        /// </returns>
        public virtual bool Contains(DayEntry value)
        {
            return this.List.Contains(value);
        }

        /// <summary>
        /// Return the zero-based index of the first occurrence of a specific value
        /// in this DayEntryCollection
        /// </summary>
        /// <param name="value">
        /// The DayEntry value to locate in the DayEntryCollection.
        /// </param>
        /// <returns>
        /// The zero-based index of the first occurrence of the _ELEMENT value if found;
        /// -1 otherwise.
        /// </returns>
        public virtual int IndexOf(DayEntry value)
        {
            return this.List.IndexOf(value);
        }

        /// <summary>
        /// Inserts an element into the DayEntryCollection at the specified index
        /// </summary>
        /// <param name="index">
        /// The index at which the DayEntry is to be inserted.
        /// </param>
        /// <param name="value">
        /// The DayEntry to insert.
        /// </param>
        public virtual void Insert(int index, DayEntry value)
        {
            this.List.Insert(index, value);
        }

        /// <summary>
        /// Gets or sets the DayEntry at the given index in this DayEntryCollection.
        /// </summary>
        public virtual DayEntry this[int index]
        {
            get
            {
                return (DayEntry) this.List[index];
            }
            set
            {
                this.List[index] = value;
            }
        }

        /// <summary>
        /// Removes the first occurrence of a specific DayEntry from this DayEntryCollection.
        /// </summary>
        /// <param name="value">
        /// The DayEntry value to remove from this DayEntryCollection.
        /// </param>
        public virtual void Remove(DayEntry value)
        {
            this.List.Remove(value);
        }

        public void Sort()
        {
            this.InnerList.Sort();
        }
        public void Sort(IComparer comparer)
        {
            this.InnerList.Sort(comparer);
        }

        /// <summary>
        /// Type-specific enumeration class, used by DayEntryCollection.GetEnumerator.
        /// </summary>
        public class Enumerator: System.Collections.IEnumerator
        {
            private System.Collections.IEnumerator wrapped;

            public Enumerator(DayEntryCollection collection)
            {
                this.wrapped = ((System.Collections.CollectionBase)collection).GetEnumerator();
            }

            public DayEntry Current
            {
                get
                {
                    return (DayEntry) (this.wrapped.Current);
                }
            }

            object System.Collections.IEnumerator.Current
            {
                get
                {
                    return (DayEntry) (this.wrapped.Current);
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
        /// Returns an enumerator that can iterate through the elements of this DayEntryCollection.
        /// </summary>
        /// <returns>
        /// An object that implements System.Collections.IEnumerator.
        /// </returns>        
        public new virtual DayEntryCollection.Enumerator GetEnumerator()
        {
            return new DayEntryCollection.Enumerator(this);
        }
    }
    /// <summary>
    /// A collection of elements of type CategoryCacheEntryDetail
    /// </summary>
    public class CategoryCacheEntryDetailCollection: System.Collections.CollectionBase
    {
        /// <summary>
        /// Initializes a new empty instance of the CategoryCacheEntryDetailCollection class.
        /// </summary>
        public CategoryCacheEntryDetailCollection()
        {
            // empty
        }

        /// <summary>
        /// Initializes a new instance of the CategoryCacheEntryDetailCollection class, containing elements
        /// copied from an array.
        /// </summary>
        /// <param name="items">
        /// The array whose elements are to be added to the new CategoryCacheEntryDetailCollection.
        /// </param>
        public CategoryCacheEntryDetailCollection(CategoryCacheEntryDetail[] items)
        {
            this.AddRange(items);
        }

        /// <summary>
        /// Initializes a new instance of the CategoryCacheEntryDetailCollection class, containing elements
        /// copied from another instance of CategoryCacheEntryDetailCollection
        /// </summary>
        /// <param name="items">
        /// The CategoryCacheEntryDetailCollection whose elements are to be added to the new CategoryCacheEntryDetailCollection.
        /// </param>
        public CategoryCacheEntryDetailCollection(CategoryCacheEntryDetailCollection items)
        {
            this.AddRange(items);
        }

        /// <summary>
        /// Adds the elements of an array to the end of this CategoryCacheEntryDetailCollection.
        /// </summary>
        /// <param name="items">
        /// The array whose elements are to be added to the end of this CategoryCacheEntryDetailCollection.
        /// </param>
        public virtual void AddRange(CategoryCacheEntryDetail[] items)
        {
            foreach (CategoryCacheEntryDetail item in items)
            {
                this.List.Add(item);
            }
        }

        /// <summary>
        /// Adds the elements of another CategoryCacheEntryDetailCollection to the end of this CategoryCacheEntryDetailCollection.
        /// </summary>
        /// <param name="items">
        /// The CategoryCacheEntryDetailCollection whose elements are to be added to the end of this CategoryCacheEntryDetailCollection.
        /// </param>
        public virtual void AddRange(CategoryCacheEntryDetailCollection items)
        {
            foreach (CategoryCacheEntryDetail item in items)
            {
                this.List.Add(item);
            }
        }

        /// <summary>
        /// Adds an instance of type CategoryCacheEntryDetail to the end of this CategoryCacheEntryDetailCollection.
        /// </summary>
        /// <param name="value">
        /// The CategoryCacheEntryDetail to be added to the end of this CategoryCacheEntryDetailCollection.
        /// </param>
        public virtual void Add(CategoryCacheEntryDetail value)
        {
            this.List.Add(value);
        }

        /// <summary>
        /// Determines whether a specfic CategoryCacheEntryDetail value is in this CategoryCacheEntryDetailCollection.
        /// </summary>
        /// <param name="value">
        /// The CategoryCacheEntryDetail value to locate in this CategoryCacheEntryDetailCollection.
        /// </param>
        /// <returns>
        /// true if value is found in this CategoryCacheEntryDetailCollection;
        /// false otherwise.
        /// </returns>
        public virtual bool Contains(CategoryCacheEntryDetail value)
        {
            return this.List.Contains(value);
        }

        /// <summary>
        /// Return the zero-based index of the first occurrence of a specific value
        /// in this CategoryCacheEntryDetailCollection
        /// </summary>
        /// <param name="value">
        /// The CategoryCacheEntryDetail value to locate in the CategoryCacheEntryDetailCollection.
        /// </param>
        /// <returns>
        /// The zero-based index of the first occurrence of the _ELEMENT value if found;
        /// -1 otherwise.
        /// </returns>
        public virtual int IndexOf(CategoryCacheEntryDetail value)
        {
            return this.List.IndexOf(value);
        }

        /// <summary>
        /// Inserts an element into the CategoryCacheEntryDetailCollection at the specified index
        /// </summary>
        /// <param name="index">
        /// The index at which the CategoryCacheEntryDetail is to be inserted.
        /// </param>
        /// <param name="value">
        /// The CategoryCacheEntryDetail to insert.
        /// </param>
        public virtual void Insert(int index, CategoryCacheEntryDetail value)
        {
            this.List.Insert(index, value);
        }

        /// <summary>
        /// Gets or sets the CategoryCacheEntryDetail at the given index in this CategoryCacheEntryDetailCollection.
        /// </summary>
        public virtual CategoryCacheEntryDetail this[int index]
        {
            get
            {
                return (CategoryCacheEntryDetail) this.List[index];
            }
            set
            {
                this.List[index] = value;
            }
        }

        /// <summary>
        /// Removes the first occurrence of a specific CategoryCacheEntryDetail from this CategoryCacheEntryDetailCollection.
        /// </summary>
        /// <param name="value">
        /// The CategoryCacheEntryDetail value to remove from this CategoryCacheEntryDetailCollection.
        /// </param>
        public virtual void Remove(CategoryCacheEntryDetail value)
        {
            this.List.Remove(value);
        }

        public void Sort()
        {
            this.InnerList.Sort();
        }
        public void Sort(IComparer comparer)
        {
            this.InnerList.Sort(comparer);
        }

        /// <summary>
        /// Type-specific enumeration class, used by CategoryCacheEntryDetailCollection.GetEnumerator.
        /// </summary>
        public class Enumerator: System.Collections.IEnumerator
        {
            private System.Collections.IEnumerator wrapped;

            public Enumerator(CategoryCacheEntryDetailCollection collection)
            {
                this.wrapped = ((System.Collections.CollectionBase)collection).GetEnumerator();
            }

            public CategoryCacheEntryDetail Current
            {
                get
                {
                    return (CategoryCacheEntryDetail) (this.wrapped.Current);
                }
            }

            object System.Collections.IEnumerator.Current
            {
                get
                {
                    return (CategoryCacheEntryDetail) (this.wrapped.Current);
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
        /// Returns an enumerator that can iterate through the elements of this CategoryCacheEntryDetailCollection.
        /// </summary>
        /// <returns>
        /// An object that implements System.Collections.IEnumerator.
        /// </returns>        
        public new virtual CategoryCacheEntryDetailCollection.Enumerator GetEnumerator()
        {
            return new CategoryCacheEntryDetailCollection.Enumerator(this);
        }
    }
    /// <summary>
    /// A collection of elements of type CategoryCacheEntry
    /// </summary>
    public class CategoryCacheEntryCollection: System.Collections.CollectionBase
    {
        /// <summary>
        /// Initializes a new empty instance of the CategoryCacheEntryCollection class.
        /// </summary>
        public CategoryCacheEntryCollection()
        {
            // empty
        }

        /// <summary>
        /// Initializes a new instance of the CategoryCacheEntryCollection class, containing elements
        /// copied from an array.
        /// </summary>
        /// <param name="items">
        /// The array whose elements are to be added to the new CategoryCacheEntryCollection.
        /// </param>
        public CategoryCacheEntryCollection(CategoryCacheEntry[] items)
        {
            this.AddRange(items);
        }

        /// <summary>
        /// Initializes a new instance of the CategoryCacheEntryCollection class, containing elements
        /// copied from another instance of CategoryCacheEntryCollection
        /// </summary>
        /// <param name="items">
        /// The CategoryCacheEntryCollection whose elements are to be added to the new CategoryCacheEntryCollection.
        /// </param>
        public CategoryCacheEntryCollection(CategoryCacheEntryCollection items)
        {
            this.AddRange(items);
        }

        /// <summary>
        /// Adds the elements of an array to the end of this CategoryCacheEntryCollection.
        /// </summary>
        /// <param name="items">
        /// The array whose elements are to be added to the end of this CategoryCacheEntryCollection.
        /// </param>
        public virtual void AddRange(CategoryCacheEntry[] items)
        {
            foreach (CategoryCacheEntry item in items)
            {
                this.List.Add(item);
            }
        }

        /// <summary>
        /// Adds the elements of another CategoryCacheEntryCollection to the end of this CategoryCacheEntryCollection.
        /// </summary>
        /// <param name="items">
        /// The CategoryCacheEntryCollection whose elements are to be added to the end of this CategoryCacheEntryCollection.
        /// </param>
        public virtual void AddRange(CategoryCacheEntryCollection items)
        {
            foreach (CategoryCacheEntry item in items)
            {
                this.List.Add(item);
            }
        }

        /// <summary>
        /// Adds an instance of type CategoryCacheEntry to the end of this CategoryCacheEntryCollection.
        /// </summary>
        /// <param name="value">
        /// The CategoryCacheEntry to be added to the end of this CategoryCacheEntryCollection.
        /// </param>
        public virtual void Add(CategoryCacheEntry value)
        {
            this.List.Add(value);
        }

        /// <summary>
        /// Determines whether a specfic CategoryCacheEntry value is in this CategoryCacheEntryCollection.
        /// </summary>
        /// <param name="value">
        /// The CategoryCacheEntry value to locate in this CategoryCacheEntryCollection.
        /// </param>
        /// <returns>
        /// true if value is found in this CategoryCacheEntryCollection;
        /// false otherwise.
        /// </returns>
        public virtual bool Contains(CategoryCacheEntry value)
        {
            return this.List.Contains(value);
        }

        /// <summary>
        /// Return the zero-based index of the first occurrence of a specific value
        /// in this CategoryCacheEntryCollection
        /// </summary>
        /// <param name="value">
        /// The CategoryCacheEntry value to locate in the CategoryCacheEntryCollection.
        /// </param>
        /// <returns>
        /// The zero-based index of the first occurrence of the _ELEMENT value if found;
        /// -1 otherwise.
        /// </returns>
        public virtual int IndexOf(CategoryCacheEntry value)
        {
            return this.List.IndexOf(value);
        }

        /// <summary>
        /// Inserts an element into the CategoryCacheEntryCollection at the specified index
        /// </summary>
        /// <param name="index">
        /// The index at which the CategoryCacheEntry is to be inserted.
        /// </param>
        /// <param name="value">
        /// The CategoryCacheEntry to insert.
        /// </param>
        public virtual void Insert(int index, CategoryCacheEntry value)
        {
            this.List.Insert(index, value);
        }

        /// <summary>
        /// Gets or sets the CategoryCacheEntry at the given index in this CategoryCacheEntryCollection.
        /// </summary>
        public virtual CategoryCacheEntry this[int index]
        {
            get
            {
                return (CategoryCacheEntry) this.List[index];
            }
            set
            {
                this.List[index] = value;
            }
        }

        /// <summary>
        /// Removes the first occurrence of a specific CategoryCacheEntry from this CategoryCacheEntryCollection.
        /// </summary>
        /// <param name="value">
        /// The CategoryCacheEntry value to remove from this CategoryCacheEntryCollection.
        /// </param>
        public virtual void Remove(CategoryCacheEntry value)
        {
            this.List.Remove(value);
        }

        public void Sort()
        {
            this.InnerList.Sort();
        }
        public void Sort(IComparer comparer)
        {
            this.InnerList.Sort(comparer);
        }

        /// <summary>
        /// Type-specific enumeration class, used by CategoryCacheEntryCollection.GetEnumerator.
        /// </summary>
        public class Enumerator: System.Collections.IEnumerator
        {
            private System.Collections.IEnumerator wrapped;

            public Enumerator(CategoryCacheEntryCollection collection)
            {
                this.wrapped = ((System.Collections.CollectionBase)collection).GetEnumerator();
            }

            public CategoryCacheEntry Current
            {
                get
                {
                    return (CategoryCacheEntry) (this.wrapped.Current);
                }
            }

            object System.Collections.IEnumerator.Current
            {
                get
                {
                    return (CategoryCacheEntry) (this.wrapped.Current);
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
        /// Returns an enumerator that can iterate through the elements of this CategoryCacheEntryCollection.
        /// </summary>
        /// <returns>
        /// An object that implements System.Collections.IEnumerator.
        /// </returns>        
        public new virtual CategoryCacheEntryCollection.Enumerator GetEnumerator()
        {
            return new CategoryCacheEntryCollection.Enumerator(this);
        }
    }
    /// <summary>
    /// A collection of elements of type DayExtra
    /// </summary>
    public class DayExtraCollection: System.Collections.CollectionBase
    {
        /// <summary>
        /// Initializes a new empty instance of the DayExtraCollection class.
        /// </summary>
        public DayExtraCollection()
        {
            // empty
        }

        /// <summary>
        /// Initializes a new instance of the DayExtraCollection class, containing elements
        /// copied from an array.
        /// </summary>
        /// <param name="items">
        /// The array whose elements are to be added to the new DayExtraCollection.
        /// </param>
        public DayExtraCollection(DayExtra[] items)
        {
            this.AddRange(items);
        }

        /// <summary>
        /// Initializes a new instance of the DayExtraCollection class, containing elements
        /// copied from another instance of DayExtraCollection
        /// </summary>
        /// <param name="items">
        /// The DayExtraCollection whose elements are to be added to the new DayExtraCollection.
        /// </param>
        public DayExtraCollection(DayExtraCollection items)
        {
            this.AddRange(items);
        }

        /// <summary>
        /// Adds the elements of an array to the end of this DayExtraCollection.
        /// </summary>
        /// <param name="items">
        /// The array whose elements are to be added to the end of this DayExtraCollection.
        /// </param>
        public virtual void AddRange(DayExtra[] items)
        {
            foreach (DayExtra item in items)
            {
                this.List.Add(item);
            }
        }

        /// <summary>
        /// Adds the elements of another DayExtraCollection to the end of this DayExtraCollection.
        /// </summary>
        /// <param name="items">
        /// The DayExtraCollection whose elements are to be added to the end of this DayExtraCollection.
        /// </param>
        public virtual void AddRange(DayExtraCollection items)
        {
            foreach (DayExtra item in items)
            {
                this.List.Add(item);
            }
        }

        /// <summary>
        /// Adds an instance of type DayExtra to the end of this DayExtraCollection.
        /// </summary>
        /// <param name="value">
        /// The DayExtra to be added to the end of this DayExtraCollection.
        /// </param>
        public virtual void Add(DayExtra value)
        {
            this.List.Add(value);
        }

        /// <summary>
        /// Determines whether a specfic DayExtra value is in this DayExtraCollection.
        /// </summary>
        /// <param name="value">
        /// The DayExtra value to locate in this DayExtraCollection.
        /// </param>
        /// <returns>
        /// true if value is found in this DayExtraCollection;
        /// false otherwise.
        /// </returns>
        public virtual bool Contains(DayExtra value)
        {
            return this.List.Contains(value);
        }

        /// <summary>
        /// Return the zero-based index of the first occurrence of a specific value
        /// in this DayExtraCollection
        /// </summary>
        /// <param name="value">
        /// The DayExtra value to locate in the DayExtraCollection.
        /// </param>
        /// <returns>
        /// The zero-based index of the first occurrence of the _ELEMENT value if found;
        /// -1 otherwise.
        /// </returns>
        public virtual int IndexOf(DayExtra value)
        {
            return this.List.IndexOf(value);
        }

        /// <summary>
        /// Inserts an element into the DayExtraCollection at the specified index
        /// </summary>
        /// <param name="index">
        /// The index at which the DayExtra is to be inserted.
        /// </param>
        /// <param name="value">
        /// The DayExtra to insert.
        /// </param>
        public virtual void Insert(int index, DayExtra value)
        {
            this.List.Insert(index, value);
        }

        /// <summary>
        /// Gets or sets the DayExtra at the given index in this DayExtraCollection.
        /// </summary>
        public virtual DayExtra this[int index]
        {
            get
            {
                return (DayExtra) this.List[index];
            }
            set
            {
                this.List[index] = value;
            }
        }

        /// <summary>
        /// Removes the first occurrence of a specific DayExtra from this DayExtraCollection.
        /// </summary>
        /// <param name="value">
        /// The DayExtra value to remove from this DayExtraCollection.
        /// </param>
        public virtual void Remove(DayExtra value)
        {
            this.List.Remove(value);
        }

        public void Sort()
        {
            this.InnerList.Sort();
        }
        public void Sort(IComparer comparer)
        {
            this.InnerList.Sort(comparer);
        }

        /// <summary>
        /// Type-specific enumeration class, used by DayExtraCollection.GetEnumerator.
        /// </summary>
        public class Enumerator: System.Collections.IEnumerator
        {
            private System.Collections.IEnumerator wrapped;

            public Enumerator(DayExtraCollection collection)
            {
                this.wrapped = ((System.Collections.CollectionBase)collection).GetEnumerator();
            }

            public DayExtra Current
            {
                get
                {
                    return (DayExtra) (this.wrapped.Current);
                }
            }

            object System.Collections.IEnumerator.Current
            {
                get
                {
                    return (DayExtra) (this.wrapped.Current);
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
        /// Returns an enumerator that can iterate through the elements of this DayExtraCollection.
        /// </summary>
        /// <returns>
        /// An object that implements System.Collections.IEnumerator.
        /// </returns>        
        public new virtual DayExtraCollection.Enumerator GetEnumerator()
        {
            return new DayExtraCollection.Enumerator(this);
        }
    }

    /// <summary>
    /// A collection of elements of type RssItem
    /// </summary>
    public class RssItemCollection: System.Collections.CollectionBase
    {
        /// <summary>
        /// Initializes a new empty instance of the RssItemCollection class.
        /// </summary>
        public RssItemCollection()
        {
            // empty
        }

        /// <summary>
        /// Initializes a new instance of the RssItemCollection class, containing elements
        /// copied from an array.
        /// </summary>
        /// <param name="items">
        /// The array whose elements are to be added to the new RssItemCollection.
        /// </param>
        public RssItemCollection(RssItem[] items)
        {
            this.AddRange(items);
        }

        /// <summary>
        /// Initializes a new instance of the RssItemCollection class, containing elements
        /// copied from another instance of RssItemCollection
        /// </summary>
        /// <param name="items">
        /// The RssItemCollection whose elements are to be added to the new RssItemCollection.
        /// </param>
        public RssItemCollection(RssItemCollection items)
        {
            this.AddRange(items);
        }

        /// <summary>
        /// Adds the elements of an array to the end of this RssItemCollection.
        /// </summary>
        /// <param name="items">
        /// The array whose elements are to be added to the end of this RssItemCollection.
        /// </param>
        public virtual void AddRange(RssItem[] items)
        {
            foreach (RssItem item in items)
            {
                this.List.Add(item);
            }
        }

        /// <summary>
        /// Adds the elements of another RssItemCollection to the end of this RssItemCollection.
        /// </summary>
        /// <param name="items">
        /// The RssItemCollection whose elements are to be added to the end of this RssItemCollection.
        /// </param>
        public virtual void AddRange(RssItemCollection items)
        {
            foreach (RssItem item in items)
            {
                this.List.Add(item);
            }
        }

        /// <summary>
        /// Adds an instance of type RssItem to the end of this RssItemCollection.
        /// </summary>
        /// <param name="value">
        /// The RssItem to be added to the end of this RssItemCollection.
        /// </param>
        public virtual void Add(RssItem value)
        {
            this.List.Add(value);
        }

        /// <summary>
        /// Determines whether a specfic RssItem value is in this RssItemCollection.
        /// </summary>
        /// <param name="value">
        /// The RssItem value to locate in this RssItemCollection.
        /// </param>
        /// <returns>
        /// true if value is found in this RssItemCollection;
        /// false otherwise.
        /// </returns>
        public virtual bool Contains(RssItem value)
        {
            return this.List.Contains(value);
        }

        /// <summary>
        /// Return the zero-based index of the first occurrence of a specific value
        /// in this RssItemCollection
        /// </summary>
        /// <param name="value">
        /// The RssItem value to locate in the RssItemCollection.
        /// </param>
        /// <returns>
        /// The zero-based index of the first occurrence of the _ELEMENT value if found;
        /// -1 otherwise.
        /// </returns>
        public virtual int IndexOf(RssItem value)
        {
            return this.List.IndexOf(value);
        }

        /// <summary>
        /// Inserts an element into the RssItemCollection at the specified index
        /// </summary>
        /// <param name="index">
        /// The index at which the RssItem is to be inserted.
        /// </param>
        /// <param name="value">
        /// The RssItem to insert.
        /// </param>
        public virtual void Insert(int index, RssItem value)
        {
            this.List.Insert(index, value);
        }

        /// <summary>
        /// Gets or sets the RssItem at the given index in this RssItemCollection.
        /// </summary>
        public virtual RssItem this[int index]
        {
            get
            {
                return (RssItem) this.List[index];
            }
            set
            {
                this.List[index] = value;
            }
        }

        /// <summary>
        /// Removes the first occurrence of a specific RssItem from this RssItemCollection.
        /// </summary>
        /// <param name="value">
        /// The RssItem value to remove from this RssItemCollection.
        /// </param>
        public virtual void Remove(RssItem value)
        {
            this.List.Remove(value);
        }

        /// <summary>
        /// Type-specific enumeration class, used by RssItemCollection.GetEnumerator.
        /// </summary>
        public class Enumerator: System.Collections.IEnumerator
        {
            private System.Collections.IEnumerator wrapped;

            public Enumerator(RssItemCollection collection)
            {
                this.wrapped = ((System.Collections.CollectionBase)collection).GetEnumerator();
            }

            public RssItem Current
            {
                get
                {
                    return (RssItem) (this.wrapped.Current);
                }
            }

            object System.Collections.IEnumerator.Current
            {
                get
                {
                    return (RssItem) (this.wrapped.Current);
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
        /// Returns an enumerator that can iterate through the elements of this RssItemCollection.
        /// </summary>
        /// <returns>
        /// An object that implements System.Collections.IEnumerator.
        /// </returns>        
        public new virtual RssItemCollection.Enumerator GetEnumerator()
        {
            return new RssItemCollection.Enumerator(this);
        }
    }
    /// <summary>
    /// A collection of elements of type RssChannel
    /// </summary>
    public class RssChannelCollection: System.Collections.CollectionBase
    {
        /// <summary>
        /// Initializes a new empty instance of the RssChannelCollection class.
        /// </summary>
        public RssChannelCollection()
        {
            // empty
        }

        /// <summary>
        /// Initializes a new instance of the RssChannelCollection class, containing elements
        /// copied from an array.
        /// </summary>
        /// <param name="items">
        /// The array whose elements are to be added to the new RssChannelCollection.
        /// </param>
        public RssChannelCollection(RssChannel[] items)
        {
            this.AddRange(items);
        }

        /// <summary>
        /// Initializes a new instance of the RssChannelCollection class, containing elements
        /// copied from another instance of RssChannelCollection
        /// </summary>
        /// <param name="items">
        /// The RssChannelCollection whose elements are to be added to the new RssChannelCollection.
        /// </param>
        public RssChannelCollection(RssChannelCollection items)
        {
            this.AddRange(items);
        }

        /// <summary>
        /// Adds the elements of an array to the end of this RssChannelCollection.
        /// </summary>
        /// <param name="items">
        /// The array whose elements are to be added to the end of this RssChannelCollection.
        /// </param>
        public virtual void AddRange(RssChannel[] items)
        {
            foreach (RssChannel item in items)
            {
                this.List.Add(item);
            }
        }

        /// <summary>
        /// Adds the elements of another RssChannelCollection to the end of this RssChannelCollection.
        /// </summary>
        /// <param name="items">
        /// The RssChannelCollection whose elements are to be added to the end of this RssChannelCollection.
        /// </param>
        public virtual void AddRange(RssChannelCollection items)
        {
            foreach (RssChannel item in items)
            {
                this.List.Add(item);
            }
        }

        /// <summary>
        /// Adds an instance of type RssChannel to the end of this RssChannelCollection.
        /// </summary>
        /// <param name="value">
        /// The RssChannel to be added to the end of this RssChannelCollection.
        /// </param>
        public virtual void Add(RssChannel value)
        {
            this.List.Add(value);
        }

        /// <summary>
        /// Determines whether a specfic RssChannel value is in this RssChannelCollection.
        /// </summary>
        /// <param name="value">
        /// The RssChannel value to locate in this RssChannelCollection.
        /// </param>
        /// <returns>
        /// true if value is found in this RssChannelCollection;
        /// false otherwise.
        /// </returns>
        public virtual bool Contains(RssChannel value)
        {
            return this.List.Contains(value);
        }

        /// <summary>
        /// Return the zero-based index of the first occurrence of a specific value
        /// in this RssChannelCollection
        /// </summary>
        /// <param name="value">
        /// The RssChannel value to locate in the RssChannelCollection.
        /// </param>
        /// <returns>
        /// The zero-based index of the first occurrence of the _ELEMENT value if found;
        /// -1 otherwise.
        /// </returns>
        public virtual int IndexOf(RssChannel value)
        {
            return this.List.IndexOf(value);
        }

        /// <summary>
        /// Inserts an element into the RssChannelCollection at the specified index
        /// </summary>
        /// <param name="index">
        /// The index at which the RssChannel is to be inserted.
        /// </param>
        /// <param name="value">
        /// The RssChannel to insert.
        /// </param>
        public virtual void Insert(int index, RssChannel value)
        {
            this.List.Insert(index, value);
        }

        /// <summary>
        /// Gets or sets the RssChannel at the given index in this RssChannelCollection.
        /// </summary>
        public virtual RssChannel this[int index]
        {
            get
            {
                return (RssChannel) this.List[index];
            }
            set
            {
                this.List[index] = value;
            }
        }

        /// <summary>
        /// Removes the first occurrence of a specific RssChannel from this RssChannelCollection.
        /// </summary>
        /// <param name="value">
        /// The RssChannel value to remove from this RssChannelCollection.
        /// </param>
        public virtual void Remove(RssChannel value)
        {
            this.List.Remove(value);
        }

        /// <summary>
        /// Type-specific enumeration class, used by RssChannelCollection.GetEnumerator.
        /// </summary>
        public class Enumerator: System.Collections.IEnumerator
        {
            private System.Collections.IEnumerator wrapped;

            public Enumerator(RssChannelCollection collection)
            {
                this.wrapped = ((System.Collections.CollectionBase)collection).GetEnumerator();
            }

            public RssChannel Current
            {
                get
                {
                    return (RssChannel) (this.wrapped.Current);
                }
            }

            object System.Collections.IEnumerator.Current
            {
                get
                {
                    return (RssChannel) (this.wrapped.Current);
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
        /// Returns an enumerator that can iterate through the elements of this RssChannelCollection.
        /// </summary>
        /// <returns>
        /// An object that implements System.Collections.IEnumerator.
        /// </returns>        
        public new virtual RssChannelCollection.Enumerator GetEnumerator()
        {
            return new RssChannelCollection.Enumerator(this);
        }
    }
    /// <summary>
    /// A collection of elements of type LogDataItem
    /// </summary>
    public class LogDataItemCollection: System.Collections.CollectionBase
    {
        /// <summary>
        /// Initializes a new empty instance of the LogDataItemCollection class.
        /// </summary>
        public LogDataItemCollection()
        {
            // empty
        }

        /// <summary>
        /// Initializes a new instance of the LogDataItemCollection class, containing elements
        /// copied from an array.
        /// </summary>
        /// <param name="items">
        /// The array whose elements are to be added to the new LogDataItemCollection.
        /// </param>
        public LogDataItemCollection(LogDataItem[] items)
        {
            this.AddRange(items);
        }

        /// <summary>
        /// Initializes a new instance of the LogDataItemCollection class, containing elements
        /// copied from another instance of LogDataItemCollection
        /// </summary>
        /// <param name="items">
        /// The LogDataItemCollection whose elements are to be added to the new LogDataItemCollection.
        /// </param>
        public LogDataItemCollection(LogDataItemCollection items)
        {
            this.AddRange(items);
        }

        /// <summary>
        /// Adds the elements of an array to the end of this LogDataItemCollection.
        /// </summary>
        /// <param name="items">
        /// The array whose elements are to be added to the end of this LogDataItemCollection.
        /// </param>
        public virtual void AddRange(LogDataItem[] items)
        {
            foreach (LogDataItem item in items)
            {
                this.List.Add(item);
            }
        }

        /// <summary>
        /// Adds the elements of another LogDataItemCollection to the end of this LogDataItemCollection.
        /// </summary>
        /// <param name="items">
        /// The LogDataItemCollection whose elements are to be added to the end of this LogDataItemCollection.
        /// </param>
        public virtual void AddRange(LogDataItemCollection items)
        {
            foreach (LogDataItem item in items)
            {
                this.List.Add(item);
            }
        }

        /// <summary>
        /// Adds an instance of type LogDataItem to the end of this LogDataItemCollection.
        /// </summary>
        /// <param name="value">
        /// The LogDataItem to be added to the end of this LogDataItemCollection.
        /// </param>
        public virtual void Add(LogDataItem value)
        {
            this.List.Add(value);
        }

        /// <summary>
        /// Determines whether a specfic LogDataItem value is in this LogDataItemCollection.
        /// </summary>
        /// <param name="value">
        /// The LogDataItem value to locate in this LogDataItemCollection.
        /// </param>
        /// <returns>
        /// true if value is found in this LogDataItemCollection;
        /// false otherwise.
        /// </returns>
        public virtual bool Contains(LogDataItem value)
        {
            return this.List.Contains(value);
        }

        /// <summary>
        /// Return the zero-based index of the first occurrence of a specific value
        /// in this LogDataItemCollection
        /// </summary>
        /// <param name="value">
        /// The LogDataItem value to locate in the LogDataItemCollection.
        /// </param>
        /// <returns>
        /// The zero-based index of the first occurrence of the _ELEMENT value if found;
        /// -1 otherwise.
        /// </returns>
        public virtual int IndexOf(LogDataItem value)
        {
            return this.List.IndexOf(value);
        }

        /// <summary>
        /// Inserts an element into the LogDataItemCollection at the specified index
        /// </summary>
        /// <param name="index">
        /// The index at which the LogDataItem is to be inserted.
        /// </param>
        /// <param name="value">
        /// The LogDataItem to insert.
        /// </param>
        public virtual void Insert(int index, LogDataItem value)
        {
            this.List.Insert(index, value);
        }

        /// <summary>
        /// Gets or sets the LogDataItem at the given index in this LogDataItemCollection.
        /// </summary>
        public virtual LogDataItem this[int index]
        {
            get
            {
                return (LogDataItem) this.List[index];
            }
            set
            {
                this.List[index] = value;
            }
        }

        /// <summary>
        /// Removes the first occurrence of a specific LogDataItem from this LogDataItemCollection.
        /// </summary>
        /// <param name="value">
        /// The LogDataItem value to remove from this LogDataItemCollection.
        /// </param>
        public virtual void Remove(LogDataItem value)
        {
            this.List.Remove(value);
        }

        /// <summary>
        /// Type-specific enumeration class, used by LogDataItemCollection.GetEnumerator.
        /// </summary>
        public class Enumerator: System.Collections.IEnumerator
        {
            private System.Collections.IEnumerator wrapped;

            public Enumerator(LogDataItemCollection collection)
            {
                this.wrapped = ((System.Collections.CollectionBase)collection).GetEnumerator();
            }

            public LogDataItem Current
            {
                get
                {
                    return (LogDataItem) (this.wrapped.Current);
                }
            }

            object System.Collections.IEnumerator.Current
            {
                get
                {
                    return (LogDataItem) (this.wrapped.Current);
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
        /// Returns an enumerator that can iterate through the elements of this LogDataItemCollection.
        /// </summary>
        /// <returns>
        /// An object that implements System.Collections.IEnumerator.
        /// </returns>        
        public new virtual LogDataItemCollection.Enumerator GetEnumerator()
        {
            return new LogDataItemCollection.Enumerator(this);
        }
    }

}
