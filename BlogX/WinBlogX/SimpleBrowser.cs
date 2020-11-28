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
    using System.Diagnostics;
    using System.IO;
    using System.Collections;
    using System.Windows.Forms;
    using System.Drawing;
    using System.Xml.Serialization;
    using Anderson.Chris.BlogX.Runtime;
    using Anderson.Chris.BlogX.Services;

    /// <summary>
    /// Summary description for Form1.
    /// </summary>
    public class SimpleBrowser : BlogXForm
    {
        #region Designer Fields

        private System.Windows.Forms.ListView entryList;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private Anderson.Chris.BlogX.WindowsClient.HtmlViewer entryContent;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.ComponentModel.Container components = null;
        private System.Windows.Forms.MainMenu mainMenu1;
        private System.Windows.Forms.MenuItem fileHeader;
        private System.Windows.Forms.MenuItem exitMenu;
        private System.Windows.Forms.MenuItem debugHeader;
        private System.Windows.Forms.MenuItem downloadMenu;
        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.MenuItem newWindowMenu;
        private System.Windows.Forms.MenuItem entryHeader;
        private System.Windows.Forms.MenuItem addNewMenu;
        private System.Windows.Forms.MenuItem editMenu;
        private System.Windows.Forms.MenuItem viewCommentsMenu;
        private System.Windows.Forms.MenuItem editFilesMenu;
        private System.Windows.Forms.ComboBox dayList;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.LinkLabel addNewLink;
        private System.Windows.Forms.LinkLabel editFilesLink;
        private System.Windows.Forms.LinkLabel editEntryLink;
        private System.Windows.Forms.LinkLabel viewCommentsLink;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        #endregion

        WaitDialog dialog;

        BlogXBrowsing browse = new BlogXBrowsing();
        WinBlogXProject project;

        DateTime selectedDate;
        DayEntry viewingDay;
        DayExtra viewingDayExtra;
        private System.Windows.Forms.LinkLabel deleteEntryLink;
        private System.Windows.Forms.MenuItem deleteMenu;
        Entry viewingEntry;

        public SimpleBrowser(WinBlogXProject project)
        {
            InitializeComponent();

            Project = project;
        }

        public WinBlogXProject Project
        {
            get
            {
                return project;
            }
            set
            {
                ViewingDay = null;
                ViewingEntry = null;

                project = value;

                if (project == null)
                {
                    BeginInvoke(new MethodInvoker(Close));
                    return;
                }

                browse.Url = project.BrowsingUrl;
                Text = project.Name + " - WinBlogX";
                this.dayList.SelectedIndexChanged -= new System.EventHandler(this.dayList_SelectedIndexChanged);
                Debug.WriteLine("Begin GetDaysWithEntries");
                browse.BeginGetDaysWithEntries(new AsyncCallback(GetDaysWithEntriesCallback), browse);
                StartOperation();
            }
        }

        delegate void UICallbackDateTimeArray(DateTime[] dates);
        void GetDaysWithEntriesUICallback(DateTime[] dates)
        {
            EndOperation();
            Debug.WriteLine("End GetDaysWithEntries");
            LoadDayListDates(dates);
            this.dayList.SelectedIndexChanged += new System.EventHandler(this.dayList_SelectedIndexChanged);
        }
        void GetDaysWithEntriesCallback(IAsyncResult result)
        {
            DateTime[] dates = ((BlogXBrowsing)result.AsyncState).EndGetDaysWithEntries(result);
            BeginInvoke(new UICallbackDateTimeArray(GetDaysWithEntriesUICallback), new object[] { dates});
        }

        DateTime SelectedDate
        {
            get
            {
                return selectedDate;
            }
            set
            {
                selectedDate = value;
                Debug.WriteLine("Begin GetDayEntry");
                browse.BeginGetDayEntry(selectedDate, new AsyncCallback(SelectedDateChangedCallback), browse);
                StartOperation();
            }
        }
        delegate void UICallbackDayEntry(DayEntry entry);
        void SelectedDateChangedUICallback(DayEntry entry)
        {
            EndOperation();
            Debug.WriteLine("End GetDayEntry");
            ViewingDay = entry;
        }
        void SelectedDateChangedCallback(IAsyncResult result)
        {
            DayEntry data = ((BlogXBrowsing)result.AsyncState).EndGetDayEntry(result);
            BeginInvoke(new UICallbackDayEntry(SelectedDateChangedUICallback), new object[] { data });
        }

        DayEntry ViewingDay
        {
            get
            {
                return viewingDay;
            }
            set
            {
                entryList.Items.Clear();
                
                viewingDay = value;

                if (viewingDay != null)
                {
                    Debug.WriteLine("Begin GetDayExtra");
                    browse.BeginGetDayExtra(viewingDay.Date, new AsyncCallback(ViewingDayChangedCallback), browse);
                    StartOperation();
                }
            }
        }
        delegate void UICallbackDayExtra(DayExtra extra);
        void ViewingDayChangedUICallback(DayExtra extra)
        {
            EndOperation();
            Debug.WriteLine("End GetDayExtra");
            viewingDayExtra = extra;

            foreach (Entry entry in viewingDay.Entries)
            {
                ListViewItem item = new ListViewItem();
                item.Text = entry.Created.ToString("hh:mm tt");
                item.SubItems.Add(entry.Categories);
                item.SubItems.Add(entry.Title);
                item.SubItems.Add(viewingDayExtra.GetCommentsFor(entry.EntryId).Count.ToString());
                item.SubItems.Add(entry.EntryId);
                item.Tag = entry;
                entryList.Items.Add(item);
            }
        }
        void ViewingDayChangedCallback(IAsyncResult result)
        {
            DayExtra extra = ((BlogXBrowsing)result.AsyncState).EndGetDayExtra(result);
            BeginInvoke(new UICallbackDayExtra(ViewingDayChangedUICallback), new object[] { extra });
        }

        Entry ViewingEntry
        {
            get
            {
                return viewingEntry;
            }
            set
            {
                entryContent.Html = "<html></html>";
                viewingEntry = value;

                if (viewingEntry != null)
                {
                    entryContent.Html = viewingEntry.Content;
                }
                editEntryLink.Visible = viewCommentsLink.Visible = deleteEntryLink.Visible = (viewingEntry != null);
            }
        }

        int operationCount = 0;
        void StartOperation()
        {
            this.Show();
            Application.DoEvents();

            operationCount++;
            if (operationCount == 1)
            {
                dialog = new WaitDialog();
                while (operationCount > 0 && dialog.TotalMilliseconds < 1000)
                {
                    Application.DoEvents();
                }
                if (operationCount > 0)
                {
                    dialog.ShowDialog(this);
                }
                dialog.Dispose();
            }
        }
        void EndOperation()
        {
            operationCount--;
            if (operationCount == 0)
            {
                dialog.DialogResult = DialogResult.Cancel;
                dialog.Close();
                dialog.Visible = false;
                while (dialog.Visible)
                {
                    Application.DoEvents();
                }
                dialog.Dispose();
            }
        }

        #region Windows Form Designer generated code
        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose( bool disposing )
        {
            if( disposing )
            {
                if (components != null) 
                {
                    components.Dispose();
                }
            }
            base.Dispose( disposing );
        }

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(SimpleBrowser));
            this.entryList = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.entryContent = new Anderson.Chris.BlogX.WindowsClient.HtmlViewer();
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.fileHeader = new System.Windows.Forms.MenuItem();
            this.newWindowMenu = new System.Windows.Forms.MenuItem();
            this.editFilesMenu = new System.Windows.Forms.MenuItem();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.exitMenu = new System.Windows.Forms.MenuItem();
            this.entryHeader = new System.Windows.Forms.MenuItem();
            this.addNewMenu = new System.Windows.Forms.MenuItem();
            this.editMenu = new System.Windows.Forms.MenuItem();
            this.viewCommentsMenu = new System.Windows.Forms.MenuItem();
            this.debugHeader = new System.Windows.Forms.MenuItem();
            this.downloadMenu = new System.Windows.Forms.MenuItem();
            this.dayList = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.editFilesLink = new System.Windows.Forms.LinkLabel();
            this.viewCommentsLink = new System.Windows.Forms.LinkLabel();
            this.editEntryLink = new System.Windows.Forms.LinkLabel();
            this.addNewLink = new System.Windows.Forms.LinkLabel();
            this.label3 = new System.Windows.Forms.Label();
            this.deleteEntryLink = new System.Windows.Forms.LinkLabel();
            this.deleteMenu = new System.Windows.Forms.MenuItem();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // entryList
            // 
            this.entryList.Anchor = (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
                | System.Windows.Forms.AnchorStyles.Left) 
                | System.Windows.Forms.AnchorStyles.Right);
            this.entryList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.entryList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
                                                                                        this.columnHeader1,
                                                                                        this.columnHeader5,
                                                                                        this.columnHeader2,
                                                                                        this.columnHeader4,
                                                                                        this.columnHeader3});
            this.entryList.Font = new System.Drawing.Font("Tahoma", 10F);
            this.entryList.FullRowSelect = true;
            this.entryList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.entryList.HideSelection = false;
            this.entryList.Location = new System.Drawing.Point(12, 28);
            this.entryList.Name = "entryList";
            this.entryList.Size = new System.Drawing.Size(548, 159);
            this.entryList.TabIndex = 2;
            this.entryList.View = System.Windows.Forms.View.Details;
            this.entryList.DoubleClick += new System.EventHandler(this.entryList_DoubleClick);
            this.entryList.SelectedIndexChanged += new System.EventHandler(this.entryList_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Time";
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Category";
            this.columnHeader5.Width = 75;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Title";
            this.columnHeader2.Width = 381;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Comments";
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "EntryId";
            // 
            // entryContent
            // 
            this.entryContent.Anchor = (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
                | System.Windows.Forms.AnchorStyles.Left) 
                | System.Windows.Forms.AnchorStyles.Right);
            this.entryContent.Html = null;
            this.entryContent.Location = new System.Drawing.Point(16, 28);
            this.entryContent.Name = "entryContent";
            this.entryContent.Size = new System.Drawing.Size(536, 120);
            this.entryContent.TabIndex = 4;
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
                                                                                      this.fileHeader,
                                                                                      this.entryHeader,
                                                                                      this.debugHeader});
            // 
            // fileHeader
            // 
            this.fileHeader.Index = 0;
            this.fileHeader.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
                                                                                       this.newWindowMenu,
                                                                                       this.editFilesMenu,
                                                                                       this.menuItem1,
                                                                                       this.exitMenu});
            this.fileHeader.Text = "&File";
            // 
            // newWindowMenu
            // 
            this.newWindowMenu.Index = 0;
            this.newWindowMenu.Text = "&New Window";
            this.newWindowMenu.Click += new System.EventHandler(this.newWindowMenu_Click);
            // 
            // editFilesMenu
            // 
            this.editFilesMenu.Index = 1;
            this.editFilesMenu.Text = "&Edit Server Files";
            this.editFilesMenu.Click += new System.EventHandler(this.editFilesMenu_Click);
            // 
            // menuItem1
            // 
            this.menuItem1.Index = 2;
            this.menuItem1.Text = "-";
            // 
            // exitMenu
            // 
            this.exitMenu.Index = 3;
            this.exitMenu.Text = "&Close";
            this.exitMenu.Click += new System.EventHandler(this.exitMenu_Click);
            // 
            // entryHeader
            // 
            this.entryHeader.Index = 1;
            this.entryHeader.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
                                                                                        this.addNewMenu,
                                                                                        this.editMenu,
                                                                                        this.deleteMenu,
                                                                                        this.viewCommentsMenu});
            this.entryHeader.Text = "&Entry";
            this.entryHeader.Popup += new System.EventHandler(this.entryHeader_Popup);
            // 
            // addNewMenu
            // 
            this.addNewMenu.Index = 0;
            this.addNewMenu.Text = "&Add New";
            this.addNewMenu.Click += new System.EventHandler(this.addNewMenu_Click);
            // 
            // editMenu
            // 
            this.editMenu.Index = 1;
            this.editMenu.Text = "&Edit";
            this.editMenu.Click += new System.EventHandler(this.editMenu_Click);
            // 
            // viewCommentsMenu
            // 
            this.viewCommentsMenu.Index = 3;
            this.viewCommentsMenu.Text = "&View Comments";
            this.viewCommentsMenu.Click += new System.EventHandler(this.viewCommentsMenu_Click);
            // 
            // debugHeader
            // 
            this.debugHeader.Index = 2;
            this.debugHeader.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
                                                                                        this.downloadMenu});
            this.debugHeader.Text = "Debug";
            // 
            // downloadMenu
            // 
            this.downloadMenu.Index = 0;
            this.downloadMenu.Text = "Download All";
            this.downloadMenu.Click += new System.EventHandler(this.downloadMenu_Click);
            // 
            // dayList
            // 
            this.dayList.Anchor = ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
                | System.Windows.Forms.AnchorStyles.Right);
            this.dayList.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.dayList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.dayList.Font = new System.Drawing.Font("Tahoma", 10F);
            this.dayList.Location = new System.Drawing.Point(348, 0);
            this.dayList.Name = "dayList";
            this.dayList.Size = new System.Drawing.Size(212, 25);
            this.dayList.TabIndex = 5;
            this.dayList.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.dayList_DrawItem);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(228, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(124, 23);
            this.label1.TabIndex = 6;
            this.label1.Text = "View Entries For:";
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(16, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(124, 23);
            this.label2.TabIndex = 6;
            this.label2.Text = "Entry Content:";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                                 this.entryContent,
                                                                                 this.label2});
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(144, 200);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(563, 160);
            this.panel1.TabIndex = 7;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Transparent;
            this.panel2.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                                 this.dayList,
                                                                                 this.entryList,
                                                                                 this.label1});
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(144, 5);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(563, 195);
            this.panel2.TabIndex = 8;
            // 
            // splitter1
            // 
            this.splitter1.BackColor = System.Drawing.Color.Navy;
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.splitter1.Location = new System.Drawing.Point(144, 197);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(563, 3);
            this.splitter1.TabIndex = 9;
            this.splitter1.TabStop = false;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Transparent;
            this.panel3.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                                 this.panel4});
            this.panel3.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel3.Location = new System.Drawing.Point(5, 5);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(139, 355);
            this.panel3.TabIndex = 10;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(255)), ((System.Byte)(192)), ((System.Byte)(192)));
            this.panel4.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                                 this.editFilesLink,
                                                                                 this.deleteEntryLink,
                                                                                 this.viewCommentsLink,
                                                                                 this.editEntryLink,
                                                                                 this.addNewLink,
                                                                                 this.label3});
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(139, 152);
            this.panel4.TabIndex = 0;
            // 
            // editFilesLink
            // 
            this.editFilesLink.BackColor = System.Drawing.Color.Transparent;
            this.editFilesLink.Dock = System.Windows.Forms.DockStyle.Top;
            this.editFilesLink.Font = new System.Drawing.Font("Tahoma", 10F);
            this.editFilesLink.Location = new System.Drawing.Point(0, 123);
            this.editFilesLink.Name = "editFilesLink";
            this.editFilesLink.Size = new System.Drawing.Size(139, 25);
            this.editFilesLink.TabIndex = 2;
            this.editFilesLink.TabStop = true;
            this.editFilesLink.Text = "Configure Server";
            this.editFilesLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.editFilesLink_LinkClicked);
            // 
            // viewCommentsLink
            // 
            this.viewCommentsLink.BackColor = System.Drawing.Color.Transparent;
            this.viewCommentsLink.Dock = System.Windows.Forms.DockStyle.Top;
            this.viewCommentsLink.Font = new System.Drawing.Font("Tahoma", 10F);
            this.viewCommentsLink.Location = new System.Drawing.Point(0, 73);
            this.viewCommentsLink.Name = "viewCommentsLink";
            this.viewCommentsLink.Size = new System.Drawing.Size(139, 25);
            this.viewCommentsLink.TabIndex = 4;
            this.viewCommentsLink.TabStop = true;
            this.viewCommentsLink.Text = "View Entry Comments";
            this.viewCommentsLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.viewCommentsLink_LinkClicked);
            // 
            // editEntryLink
            // 
            this.editEntryLink.BackColor = System.Drawing.Color.Transparent;
            this.editEntryLink.Dock = System.Windows.Forms.DockStyle.Top;
            this.editEntryLink.Font = new System.Drawing.Font("Tahoma", 10F);
            this.editEntryLink.Location = new System.Drawing.Point(0, 48);
            this.editEntryLink.Name = "editEntryLink";
            this.editEntryLink.Size = new System.Drawing.Size(139, 25);
            this.editEntryLink.TabIndex = 3;
            this.editEntryLink.TabStop = true;
            this.editEntryLink.Text = "Edit Entry";
            this.editEntryLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.editEntryLink_LinkClicked);
            // 
            // addNewLink
            // 
            this.addNewLink.BackColor = System.Drawing.Color.Transparent;
            this.addNewLink.Dock = System.Windows.Forms.DockStyle.Top;
            this.addNewLink.Font = new System.Drawing.Font("Tahoma", 10F);
            this.addNewLink.Location = new System.Drawing.Point(0, 23);
            this.addNewLink.Name = "addNewLink";
            this.addNewLink.Size = new System.Drawing.Size(139, 25);
            this.addNewLink.TabIndex = 1;
            this.addNewLink.TabStop = true;
            this.addNewLink.Text = "Add New Entry";
            this.addNewLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.addNewLink_LinkClicked);
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(190)), ((System.Byte)(255)), ((System.Byte)(255)), ((System.Byte)(255)));
            this.label3.Dock = System.Windows.Forms.DockStyle.Top;
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(139, 23);
            this.label3.TabIndex = 0;
            this.label3.Text = "Tasks";
            // 
            // deleteEntryLink
            // 
            this.deleteEntryLink.BackColor = System.Drawing.Color.Transparent;
            this.deleteEntryLink.Dock = System.Windows.Forms.DockStyle.Top;
            this.deleteEntryLink.Font = new System.Drawing.Font("Tahoma", 10F);
            this.deleteEntryLink.Location = new System.Drawing.Point(0, 98);
            this.deleteEntryLink.Name = "deleteEntryLink";
            this.deleteEntryLink.Size = new System.Drawing.Size(139, 25);
            this.deleteEntryLink.TabIndex = 5;
            this.deleteEntryLink.TabStop = true;
            this.deleteEntryLink.Text = "Delete Entry";
            this.deleteEntryLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.deleteEntryLink_LinkClicked);
            // 
            // deleteMenu
            // 
            this.deleteMenu.Index = 2;
            this.deleteMenu.Text = "&Delete";
            this.deleteMenu.Click += new System.EventHandler(this.deleteMenu_Click);
            // 
            // SimpleBrowser
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(7, 17);
            this.ClientSize = new System.Drawing.Size(712, 365);
            this.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                          this.splitter1,
                                                                          this.panel2,
                                                                          this.panel1,
                                                                          this.panel3});
            this.DockPadding.All = 5;
            this.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.ForeColor = System.Drawing.Color.White;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Menu = this.mainMenu1;
            this.Name = "SimpleBrowser";
            this.Text = "Form1";
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.ResumeLayout(false);

        }
		#endregion

        private void dayList_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (dayList.SelectedItem != null)
            {
                SelectedDate = (DateTime)dayList.SelectedItem;
            }
            else
            {
                SelectedDate = new DateTime();
            }
        }

        private void entryList_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            Entry selected = null;
            if (entryList.SelectedItems.Count > 0)
            {
                selected = entryList.SelectedItems[0].Tag as Entry;
            }
            ViewingEntry = selected;
        }

        private void exitMenu_Click(object sender, System.EventArgs e)
        {
            Close();
        }

        BlogEditor edit;
        private void EditEntry(bool createNew)
        {
            if (edit != null)
            {
                edit.Dispose();
                edit = null;
            }
            edit = new BlogEditor();
            edit.Browse = browse;
            edit.Project = project;
            if (!createNew)
            {
                edit.Day = ViewingDay;
                edit.Entry = ViewingEntry;
            }
            if (edit.ShowDialog(this) == DialogResult.OK)
            {
                BlogXEditing editing = new BlogXEditing();
                editing.Url = project.EditingUrl;

                if (edit.Day == null)
                {
                    editing.BeginCreateEntry(edit.Entry, edit.Username, edit.Password, new AsyncCallback(CreateEntryCallback), editing);
                }
                else
                {
                    editing.BeginUpdateEntry(edit.Entry, edit.Username, edit.Password, new AsyncCallback(UpdateEntryCallback), editing);
                }
                StartOperation();
            }
            else
            {
                if (edit != null)
                {
                    edit.Dispose();
                    edit = null;
                }
            }
        }

        void DeleteEntry()
        {
            if (MessageBox.Show("Do you wish to delete this entry?", "WinBlogX", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                BlogXEditing editing = new BlogXEditing();
                editing.Url = project.EditingUrl;
                string username = "chrisan", password = "chrisanx";
                if (!editing.CanEdit(username, password))
                {
                }
                editing.BeginDeleteEntry(ViewingEntry.EntryId, username, password, new AsyncCallback(DeleteEntryCallback), editing);
                StartOperation();
            }
        }

        void DeleteEntryCallback(IAsyncResult result)
        {
            ((BlogXEditing)result.AsyncState).EndDeleteEntry(result);
            BeginInvoke(new MethodInvoker(EditCompleteUICallback));
        }
        void CreateEntryCallback(IAsyncResult result)
        {
            ((BlogXEditing)result.AsyncState).EndCreateEntry(result);
            BeginInvoke(new MethodInvoker(EditCompleteUICallback));
        }
        void UpdateEntryCallback(IAsyncResult result)
        {
            ((BlogXEditing)result.AsyncState).EndUpdateEntry(result);
            BeginInvoke(new MethodInvoker(EditCompleteUICallback));
        }
        DateTime editCompleteSelectedDate;
        void EditCompleteUICallback()
        {
            EndOperation();
            editCompleteSelectedDate = SelectedDate;
            this.dayList.SelectedIndexChanged -= new System.EventHandler(this.dayList_SelectedIndexChanged);
            if (edit != null)
            {
                edit.Dispose();
                edit = null;
            }

            Debug.WriteLine("Begin GetDaysWithEntries");
            browse.BeginGetDaysWithEntries(new AsyncCallback(EditCompleteGetDaysCallback), browse);
            StartOperation();
        }
        void EditCompleteGetDaysCallback(IAsyncResult result)
        {
            DateTime[] dates = ((BlogXBrowsing)result.AsyncState).EndGetDaysWithEntries(result);
            BeginInvoke(new UICallbackDateTimeArray(EditCompleteGetDaysUICallback), new object[] { dates });
        }
        void EditCompleteGetDaysUICallback(DateTime[] dates)
        {
            EndOperation();
            LoadDayListDates(dates);
            Debug.WriteLine("End GetDaysWithEntries");
            this.dayList.SelectedIndexChanged += new System.EventHandler(this.dayList_SelectedIndexChanged);
            dayList.SelectedItem = editCompleteSelectedDate;
        }

        void LoadDayListDates(DateTime[] dates)
        {
            dayList.Items.Clear();
            foreach (DateTime date in dates)
            {
                dayList.Items.Add(date);
            }
        }

        string ResolvePathToCache(string path)
        {
            return Path.Combine(project.LocalCacheDir, path);
        }
        private void downloadMenu_Click(object sender, System.EventArgs e)
        {
            ResolveFileCallback old = BlogXData.Resolver;
            BlogXData.Resolver = new ResolveFileCallback(ResolvePathToCache);

            Debug.WriteLine("Begin GetDaysWithEntries");
            DateTime[] dates = browse.GetDaysWithEntries();
            Debug.WriteLine("End GetDaysWithEntries");

            foreach (DateTime date in dates)
            {
                Debug.WriteLine("Begin GetDayEntry");
                DayEntry dayEntry = browse.GetDayEntry(date);
                Debug.WriteLine("End GetDayEntry");
                dayEntry.Save();

                Debug.WriteLine("Begin GetDayExtra");
                DayExtra dayExtra = browse.GetDayExtra(date);
                Debug.WriteLine("End GetDayExtra");
                if (dayExtra.Comments.Count > 0)
                {
                    dayExtra.Save();
                }
            }

            BlogXData.Resolver = old;
        }

        private void editCommentsMenu_Click(object sender, System.EventArgs e)
        {
            if (ViewingEntry != null)
            {
                using (BlogComment editor = new BlogComment())
                {
                    editor.Entry = ViewingEntry;
                    editor.Extra = viewingDayExtra;
                    editor.Browse = browse;

                    if (editor.ShowDialog() == DialogResult.OK)
                    {
                        Entry old = ViewingEntry;
                        ViewingDay = ViewingDay;
                        ViewingEntry = old;
                    }
                }
            }
        }

        private void newWindowMenu_Click(object sender, System.EventArgs e)
        {
            new Start().Show();
        }

        private void entryHeader_Popup(object sender, System.EventArgs e)
        {
            editMenu.Enabled = viewCommentsMenu.Enabled = deleteMenu.Enabled = (ViewingEntry != null);
        }

        private void addNewMenu_Click(object sender, System.EventArgs e)
        {
            EditEntry(true);
        }

        private void editMenu_Click(object sender, System.EventArgs e)
        {
            EditEntry(false);        
        }

        private void viewCommentsMenu_Click(object sender, System.EventArgs e)
        {
            if (ViewingEntry != null)
            {
                BlogComment editor = new BlogComment();
                editor.Entry = ViewingEntry;
                editor.Extra = viewingDayExtra;
                editor.Browse = browse;

                if (editor.ShowDialog() == DialogResult.OK)
                {
                    Entry old = ViewingEntry;
                    ViewingDay = ViewingDay;
                    ViewingEntry = old;
                }
            }
        }

        private void editFilesMenu_Click(object sender, System.EventArgs e)
        {
            using (EditServerFile esf = new EditServerFile())
            {
                esf.Project = project;
                esf.ShowDialog(this);
            }
        }

        private void dayList_DrawItem(object sender, System.Windows.Forms.DrawItemEventArgs e)
        {
            string text = "";
            if (e.Index != -1)
            {
                text = ((DateTime)((ComboBox)sender).Items[e.Index]).ToString("MMM dd, yyyy");
            }
            e.DrawBackground();
            e.Graphics.DrawString(text, ((ComboBox)sender).Font, Brushes.Black, e.Bounds);
            e.DrawFocusRectangle();
        
        }

        private void entryList_DoubleClick(object sender, System.EventArgs e)
        {
            if (entryList.SelectedItems.Count > 0)
            {
                editMenu_Click(sender, e);
            }
        }

        private void addNewLink_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
        {
            addNewMenu_Click(null, null);
        }

        private void editEntryLink_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
        {
            if (ViewingEntry != null)
            {
                editMenu_Click(null, null);
            }
        }

        private void editFilesLink_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
        {
            editFilesMenu_Click(null, null);
        }

        private void viewCommentsLink_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
        {
            if (ViewingEntry != null)
            {
                editCommentsMenu_Click(null, null);
            }
        }

        private void deleteEntryLink_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
        {
            if (ViewingEntry != null)
            {
                deleteMenu_Click(null, null);
            }
        }

        private void deleteMenu_Click(object sender, System.EventArgs e)
        {
            DeleteEntry();
        }
    }
}
