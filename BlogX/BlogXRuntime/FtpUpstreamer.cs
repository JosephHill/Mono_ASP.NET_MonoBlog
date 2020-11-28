//=-------
// Copyright 2003, Chris Anderson
//
//  
//   Original source code by Joe Beda
//
//
//   Provided as is, with no warrenty, etc.
//   License is granted to use, copy, modify, 
//   with or without credit to me, just don't
//   blame me if it doesn't work.
//=-------
namespace Anderson.Chris.BlogX.Runtime
{
    using System;
    using System.IO;
    using System.Runtime.Serialization;
    using System.Runtime.Serialization.Formatters.Binary;

    #region FileAction Event
    public delegate void FileActionEventHandler(object sender, FileActionEventArgs e);
    public class FileActionEventArgs : EventArgs
    {
        string action;
        string file;

        public FileActionEventArgs(string action, string file)
        {
            this.action = action;
            this.file = file;
        }

        public string Action { get { return action; } }
        public string File { get { return file; } }
    }
    #endregion

    /// <summary>
    /// Summary description for FtpUpstreamer.
    /// </summary>
    public class FtpUpstreamer
    {
        string ftpServer;
        string ftpServerRootPath;
        string ftpServerUserName;
        string ftpServerPassword;
        bool   ftpUsePassive = true;
        string localRoot;
        string upstreamerStateFile;

        public FtpUpstreamer()
        {
        }

        public event FileActionEventHandler FileAction;
        public string FtpServer { get { return ftpServer; } set { ftpServer = value; } }
        public string FtpServerRootPath { get { return ftpServerRootPath; } set { ftpServerRootPath = value; } }
        public string FtpServerUserName { get { return ftpServerUserName; } set { ftpServerUserName = value; } }
        public string FtpServerPassword { get { return ftpServerPassword; } set { ftpServerPassword = value; } }
        public bool   FtpUsePassive  { get { return ftpUsePassive ; } set { ftpUsePassive  = value; } }
        public string LocalRoot { get { return localRoot; } set { localRoot = value; } }
        public string UpstreamerStateFile { get { return upstreamerStateFile; } set { upstreamerStateFile = value; } }
        
        private StringToDateTimeAssociation fileTimes = new StringToDateTimeAssociation();
        
        public void Upstream()
        {
            LoadUpstreamerState();

            try
            {
                using (FtpConnection Ftpcon = new FtpConnection(FtpServer, 21, FtpServerUserName, FtpServerPassword))
                {                
                    Ftpcon.UsePassive = FtpUsePassive;
                    UpstreamDir(new DirectoryInfo(LocalRoot), FtpServerRootPath, Ftpcon);
                }
            }
            finally
            {
                SaveUpstreamerState();
            }
        }

        private void UpstreamDir(DirectoryInfo di, string FtpPath, FtpConnection Ftpcon)
        {
            bool fDirectoryEnsured = false;

            foreach(FileInfo file in di.GetFiles())
            {
                if (    !fileTimes.ContainsKey(file.FullName.ToLower())
                    ||  (fileTimes[file.FullName.ToLower()] != file.LastWriteTime))
                {
                    if (!fDirectoryEnsured)
                    {
                        Ftpcon.EnsureDirectory(FtpPath);
                        fDirectoryEnsured = true;
                    }

                    Ftpcon.Put(file.FullName, file.Name, null);
                    if (FileAction != null) FileAction(this, new FileActionEventArgs("upload", file.FullName));
                    fileTimes[file.FullName.ToLower()] = file.LastWriteTime;
                }                    
            }

            foreach(DirectoryInfo diChild in di.GetDirectories())
            {
                UpstreamDir(diChild, FtpPath+"/"+diChild.Name, Ftpcon);
            }
        }

        private void SaveUpstreamerState()
        {
            using(FileStream strm = File.Open(UpstreamerStateFile, FileMode.Create, FileAccess.ReadWrite, FileShare.None))
            {
                IFormatter formatter = new BinaryFormatter();
                formatter.Serialize(strm, fileTimes);
            }
        }

        private void LoadUpstreamerState()
        {
            if (File.Exists(UpstreamerStateFile))
            {
                using(FileStream strm = File.Open(UpstreamerStateFile, FileMode.Open, FileAccess.Read, FileShare.None))
                {
                    IFormatter formatter = new BinaryFormatter();
                    fileTimes = (StringToDateTimeAssociation) formatter.Deserialize(strm);
                }
            }
        }
    }

    #region StringToDateTimeAssociation

    /// <summary>
    /// A dictionary with keys of type String and values of type DateTime
    /// </summary>
    [Serializable]
    public class StringToDateTimeAssociation: System.Collections.DictionaryBase
    {
        /// <summary>
        /// Initializes a new empty instance of the StringToDateTimeAssociation class
        /// </summary>
        public StringToDateTimeAssociation()
        {
            // empty
        }

        /// <summary>
        /// Gets or sets the DateTime associated with the given String
        /// </summary>
        /// <param name="key">
        /// The String whose value to get or set.
        /// </param>
        public virtual DateTime this[String key]
        {
            get
            {
                return (DateTime) this.Dictionary[key];
            }
            set
            {
                this.Dictionary[key] = value;
            }
        }

        /// <summary>
        /// Adds an element with the specified key and value to this StringToDateTimeAssociation.
        /// </summary>
        /// <param name="key">
        /// The String key of the element to add.
        /// </param>
        /// <param name="value">
        /// The DateTime value of the element to add.
        /// </param>
        public virtual void Add(String key, DateTime value)
        {
            this.Dictionary.Add(key, value);
        }

        /// <summary>
        /// Determines whether this StringToDateTimeAssociation contains a specific key.
        /// </summary>
        /// <param name="key">
        /// The String key to locate in this StringToDateTimeAssociation.
        /// </param>
        /// <returns>
        /// true if this StringToDateTimeAssociation contains an element with the specified key;
        /// otherwise, false.
        /// </returns>
        public virtual bool Contains(String key)
        {
            return this.Dictionary.Contains(key);
        }

        /// <summary>
        /// Determines whether this StringToDateTimeAssociation contains a specific key.
        /// </summary>
        /// <param name="key">
        /// The String key to locate in this StringToDateTimeAssociation.
        /// </param>
        /// <returns>
        /// true if this StringToDateTimeAssociation contains an element with the specified key;
        /// otherwise, false.
        /// </returns>
        public virtual bool ContainsKey(String key)
        {
            return this.Dictionary.Contains(key);
        }

        /// <summary>
        /// Determines whether this StringToDateTimeAssociation contains a specific value.
        /// </summary>
        /// <param name="value">
        /// The DateTime value to locate in this StringToDateTimeAssociation.
        /// </param>
        /// <returns>
        /// true if this StringToDateTimeAssociation contains an element with the specified value;
        /// otherwise, false.
        /// </returns>
        public virtual bool ContainsValue(DateTime value)
        {
            foreach (DateTime item in this.Dictionary.Values)
            {
                if (item == value)
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Removes the element with the specified key from this StringToDateTimeAssociation.
        /// </summary>
        /// <param name="key">
        /// The String key of the element to remove.
        /// </param>
        public virtual void Remove(String key)
        {
            this.Dictionary.Remove(key);
        }

        /// <summary>
        /// Gets a collection containing the keys in this StringToDateTimeAssociation.
        /// </summary>
        public virtual System.Collections.ICollection Keys
        {
            get
            {
                return this.Dictionary.Keys;
            }
        }

        /// <summary>
        /// Gets a collection containing the values in this StringToDateTimeAssociation.
        /// </summary>
        public virtual System.Collections.ICollection Values
        {
            get
            {
                return this.Dictionary.Values;
            }
        }
    }


    #endregion
}

