namespace Anderson.Chris.BlogX.WebClient
{
    using System;
    using System.IO;
    using System.Xml.Serialization;
    using System.Web;
    using System.Web.Security;

    public class SiteSecurity
    {
        public static bool IsInRole(string role)
        {
            return HttpContext.Current.User.IsInRole(role);
        }

        public static SiteSecurityConfig GetSecurity()
        {
            XmlSerializer ser = new XmlSerializer(typeof(SiteSecurityConfig));
            SiteSecurityConfig value;

            string fullPath = HttpContext.Current.Server.MapPath("SiteConfig/siteSecurity.config");
            if (File.Exists(fullPath))
            {
                using (StreamReader reader = new StreamReader(fullPath))
                {
                    value = (SiteSecurityConfig)ser.Deserialize(reader);
                }
            }
            else
            {
                value = new SiteSecurityConfig();
            }
            return value;
        }

        public static void AddUser(string userName, string password, string role)
        {
            SiteSecurityConfig ssc = GetSecurity();
            User user = new User();
            user.Name = userName;
            user.Password = password;
            user.Role = role;
            ssc.Users.Add(user);

            XmlSerializer ser = new XmlSerializer(typeof(SiteSecurityConfig));
            using (StreamWriter writer = new StreamWriter(HttpContext.Current.Server.MapPath("SiteConfig/siteSecurity.config")))
            {
                ser.Serialize(writer, ssc);
            }
        }

        public static UserToken Login(string userName, string password)
        {
            SiteSecurityConfig ssc = GetSecurity();
            foreach (User user in ssc.Users)
            {
                if (user.Name.ToUpper() == userName.ToUpper())
                {
                    if (user.Password == password)
                    {
                        return user.ToToken();
                    }
                }
            }
            return null;
        }

        public static UserToken GetToken(string userName)
        {
            SiteSecurityConfig ssc = GetSecurity();
            foreach (User user in ssc.Users)
            {
                if (user.Name.ToUpper() == userName.ToUpper())
                {
                    return user.ToToken();
                }
            }
            return null;
        }
    }

    public class SiteSecurityConfig
    {
        UserCollection users = new UserCollection();

        public UserCollection Users { get { return users; } }
    }

    public class UserToken
    {
        string name;
        string role;

        public string Name { get { return name; } set { name = value; } }
        public string Role { get { return role; } set { role = value; } }
    }

    public class User
    {
        string name;
        string password;
        string role;

        public string Name { get { return name; } set { name = value; } }
        public string Password { get { return password; } set { password = value; } }
        public string Role { get { return role; } set { role = value; } }

        public UserToken ToToken()
        {
            UserToken token = new UserToken();
            token.Name = name;
            token.Role = role;
            return token;
        }
    }

    #region UserCollection
    /// <summary>
    /// A collection of elements of type User
    /// </summary>
    public class UserCollection: System.Collections.CollectionBase
    {
        /// <summary>
        /// Initializes a new empty instance of the UserCollection class.
        /// </summary>
        public UserCollection()
        {
            // empty
        }

        /// <summary>
        /// Initializes a new instance of the UserCollection class, containing elements
        /// copied from an array.
        /// </summary>
        /// <param name="items">
        /// The array whose elements are to be added to the new UserCollection.
        /// </param>
        public UserCollection(User[] items)
        {
            this.AddRange(items);
        }

        /// <summary>
        /// Initializes a new instance of the UserCollection class, containing elements
        /// copied from another instance of UserCollection
        /// </summary>
        /// <param name="items">
        /// The UserCollection whose elements are to be added to the new UserCollection.
        /// </param>
        public UserCollection(UserCollection items)
        {
            this.AddRange(items);
        }

        /// <summary>
        /// Adds the elements of an array to the end of this UserCollection.
        /// </summary>
        /// <param name="items">
        /// The array whose elements are to be added to the end of this UserCollection.
        /// </param>
        public virtual void AddRange(User[] items)
        {
            foreach (User item in items)
            {
                this.List.Add(item);
            }
        }

        /// <summary>
        /// Adds the elements of another UserCollection to the end of this UserCollection.
        /// </summary>
        /// <param name="items">
        /// The UserCollection whose elements are to be added to the end of this UserCollection.
        /// </param>
        public virtual void AddRange(UserCollection items)
        {
            foreach (User item in items)
            {
                this.List.Add(item);
            }
        }

        /// <summary>
        /// Adds an instance of type User to the end of this UserCollection.
        /// </summary>
        /// <param name="value">
        /// The User to be added to the end of this UserCollection.
        /// </param>
        public virtual void Add(User value)
        {
            this.List.Add(value);
        }

        /// <summary>
        /// Determines whether a specfic User value is in this UserCollection.
        /// </summary>
        /// <param name="value">
        /// The User value to locate in this UserCollection.
        /// </param>
        /// <returns>
        /// true if value is found in this UserCollection;
        /// false otherwise.
        /// </returns>
        public virtual bool Contains(User value)
        {
            return this.List.Contains(value);
        }

        /// <summary>
        /// Return the zero-based index of the first occurrence of a specific value
        /// in this UserCollection
        /// </summary>
        /// <param name="value">
        /// The User value to locate in the UserCollection.
        /// </param>
        /// <returns>
        /// The zero-based index of the first occurrence of the _ELEMENT value if found;
        /// -1 otherwise.
        /// </returns>
        public virtual int IndexOf(User value)
        {
            return this.List.IndexOf(value);
        }

        /// <summary>
        /// Inserts an element into the UserCollection at the specified index
        /// </summary>
        /// <param name="index">
        /// The index at which the User is to be inserted.
        /// </param>
        /// <param name="value">
        /// The User to insert.
        /// </param>
        public virtual void Insert(int index, User value)
        {
            this.List.Insert(index, value);
        }

        /// <summary>
        /// Gets or sets the User at the given index in this UserCollection.
        /// </summary>
        public virtual User this[int index]
        {
            get
            {
                return (User) this.List[index];
            }
            set
            {
                this.List[index] = value;
            }
        }

        /// <summary>
        /// Removes the first occurrence of a specific User from this UserCollection.
        /// </summary>
        /// <param name="value">
        /// The User value to remove from this UserCollection.
        /// </param>
        public virtual void Remove(User value)
        {
            this.List.Remove(value);
        }

        /// <summary>
        /// Type-specific enumeration class, used by UserCollection.GetEnumerator.
        /// </summary>
        public class Enumerator: System.Collections.IEnumerator
        {
            private System.Collections.IEnumerator wrapped;

            public Enumerator(UserCollection collection)
            {
                this.wrapped = ((System.Collections.CollectionBase)collection).GetEnumerator();
            }

            public User Current
            {
                get
                {
                    return (User) (this.wrapped.Current);
                }
            }

            object System.Collections.IEnumerator.Current
            {
                get
                {
                    return (User) (this.wrapped.Current);
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
        /// Returns an enumerator that can iterate through the elements of this UserCollection.
        /// </summary>
        /// <returns>
        /// An object that implements System.Collections.IEnumerator.
        /// </returns>        
        public new virtual UserCollection.Enumerator GetEnumerator()
        {
            return new UserCollection.Enumerator(this);
        }
    }
    #endregion
}
