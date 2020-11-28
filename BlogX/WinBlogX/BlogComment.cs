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
    using System.Drawing;
    using System.Collections;
    using System.ComponentModel;
    using System.Windows.Forms;
    using Anderson.Chris.BlogX.Runtime;
    using Anderson.Chris.BlogX.Services;

    /// <summary>
    /// Summary description for BlogComment.
    /// </summary>
    class BlogComment : BlogXForm
    {
        #region Designer fields

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.ListView commentList;
        private HtmlViewer commentPreview;
        private System.ComponentModel.Container components = null;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox homepageText;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox authorText;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox emailText;
        private Anderson.Chris.BlogX.WindowsClient.HtmlEditor commentEditor;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        private XButton add;
        #endregion
        BlogXBrowsing browse;
        DayExtra extra;
        Entry entry;
        Comment selected;

        public BlogComment()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();
        }
        
        Comment Selected
        {
            get
            {
                return selected;
            }
            set
            {
                selected = value;
                if (selected != null)
                {
                    commentPreview.Html = selected.Content;
                }
                else
                {
                    commentPreview.Html= "<html></html>";
                }
            }
        }


        void SetupUI()
        {
            Selected = null;
            CommentCollection validComments = extra.GetCommentsFor(entry.EntryId);
            commentList.Items.Clear();
            foreach (Comment c in validComments)
            {
                ListViewItem item = new ListViewItem();
                item.Text = c.Created.ToString();
                item.SubItems.Add(c.Content);
                item.SubItems.Add(c.Author);
                item.Tag = c;
                commentList.Items.Add(item);
            }
        }

        public BlogXBrowsing Browse { get { return browse; } set { browse = value; } }
        public DayExtra Extra { get { return extra; } set { extra = value; } }
        public Entry Entry { get { return entry; } set { entry = value; } }

        #region Windows Form Designer generated code
        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose( bool disposing )
        {
            if( disposing )
            {
                if(components != null)
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
            System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(BlogComment));
            this.commentList = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.commentPreview = new Anderson.Chris.BlogX.WindowsClient.HtmlViewer();
            this.panel1 = new System.Windows.Forms.Panel();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.label4 = new System.Windows.Forms.Label();
            this.homepageText = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.authorText = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.emailText = new System.Windows.Forms.TextBox();
            this.commentEditor = new Anderson.Chris.BlogX.WindowsClient.HtmlEditor();
            this.label1 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.add = new Anderson.Chris.BlogX.WindowsClient.XButton();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // commentList
            // 
            this.commentList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
                                                                                          this.columnHeader1,
                                                                                          this.columnHeader2,
                                                                                          this.columnHeader3});
            this.commentList.Dock = System.Windows.Forms.DockStyle.Left;
            this.commentList.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
            this.commentList.FullRowSelect = true;
            this.commentList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.commentList.HideSelection = false;
            this.commentList.Name = "commentList";
            this.commentList.Size = new System.Drawing.Size(340, 104);
            this.commentList.TabIndex = 0;
            this.commentList.View = System.Windows.Forms.View.Details;
            this.commentList.SelectedIndexChanged += new System.EventHandler(this.commentList_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Date";
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Content";
            this.columnHeader2.Width = 210;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Author";
            // 
            // commentPreview
            // 
            this.commentPreview.AllowInPlaceNavigation = false;
            this.commentPreview.Border3d = false;
            this.commentPreview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.commentPreview.FlatScrollBars = false;
            this.commentPreview.Html = "<!DOCTYPE HTML PUBLIC \"-//W3C//DTD HTML 4.0 Transitional//EN\">\r\n\r\n\r\n";
            this.commentPreview.Location = new System.Drawing.Point(343, 0);
            this.commentPreview.Name = "commentPreview";
            this.commentPreview.ScriptEnabled = false;
            this.commentPreview.ScriptObject = null;
            this.commentPreview.ScrollBarsEnabled = true;
            this.commentPreview.Size = new System.Drawing.Size(273, 104);
            this.commentPreview.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                                 this.commentPreview,
                                                                                 this.splitter1,
                                                                                 this.commentList});
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(10, 32);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(616, 104);
            this.panel1.TabIndex = 2;
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(340, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 104);
            this.splitter1.TabIndex = 2;
            this.splitter1.TabStop = false;
            // 
            // label4
            // 
            this.label4.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Location = new System.Drawing.Point(420, 256);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(96, 16);
            this.label4.TabIndex = 15;
            this.label4.Text = "Homepage:";
            // 
            // homepageText
            // 
            this.homepageText.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
            this.homepageText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.homepageText.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
            this.homepageText.Location = new System.Drawing.Point(420, 272);
            this.homepageText.Name = "homepageText";
            this.homepageText.Size = new System.Drawing.Size(212, 24);
            this.homepageText.TabIndex = 14;
            this.homepageText.Text = "http://www.simplegeek.com";
            // 
            // label3
            // 
            this.label3.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Location = new System.Drawing.Point(420, 212);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(96, 16);
            this.label3.TabIndex = 13;
            this.label3.Text = "Author:";
            // 
            // authorText
            // 
            this.authorText.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
            this.authorText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.authorText.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
            this.authorText.Location = new System.Drawing.Point(420, 228);
            this.authorText.Name = "authorText";
            this.authorText.Size = new System.Drawing.Size(212, 24);
            this.authorText.TabIndex = 12;
            this.authorText.Text = "Chris Anderson";
            // 
            // label2
            // 
            this.label2.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(420, 168);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(96, 16);
            this.label2.TabIndex = 11;
            this.label2.Text = "Email:";
            // 
            // emailText
            // 
            this.emailText.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
            this.emailText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.emailText.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
            this.emailText.Location = new System.Drawing.Point(420, 184);
            this.emailText.Name = "emailText";
            this.emailText.Size = new System.Drawing.Size(212, 24);
            this.emailText.TabIndex = 10;
            this.emailText.Text = "chris_l_anderson@hotmail.com";
            // 
            // commentEditor
            // 
            this.commentEditor.Anchor = (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
                | System.Windows.Forms.AnchorStyles.Left) 
                | System.Windows.Forms.AnchorStyles.Right);
            this.commentEditor.Html = "<body xmlns=\"http://www.w3.org/1999/xhtml\">\r\n</body>";
            this.commentEditor.Location = new System.Drawing.Point(12, 164);
            this.commentEditor.Name = "commentEditor";
            this.commentEditor.Size = new System.Drawing.Size(400, 208);
            this.commentEditor.TabIndex = 17;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(12, 140);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(612, 24);
            this.label1.TabIndex = 18;
            this.label1.Text = "New Comment";
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Dock = System.Windows.Forms.DockStyle.Top;
            this.label5.Location = new System.Drawing.Point(10, 10);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(616, 22);
            this.label5.TabIndex = 19;
            this.label5.Text = "Old Comments";
            // 
            // add
            // 
            this.add.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
            this.add.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(120)), ((System.Byte)(255)), ((System.Byte)(255)), ((System.Byte)(255)));
            this.add.Enabled = false;
            this.add.Location = new System.Drawing.Point(556, 352);
            this.add.Name = "add";
            this.add.TabIndex = 20;
            this.add.Text = "Add";
            this.add.Click += new System.EventHandler(this.add_Click);
            // 
            // BlogComment
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(7, 17);
            this.ClientSize = new System.Drawing.Size(636, 382);
            this.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                          this.add,
                                                                          this.label1,
                                                                          this.commentEditor,
                                                                          this.label4,
                                                                          this.homepageText,
                                                                          this.label3,
                                                                          this.authorText,
                                                                          this.label2,
                                                                          this.emailText,
                                                                          this.panel1,
                                                                          this.label5});
            this.DockPadding.All = 10;
            this.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
            this.ForeColor = System.Drawing.Color.White;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "BlogComment";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Comments";
            this.Load += new System.EventHandler(this.BlogComment_Load);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }
		#endregion

        private void BlogComment_Load(object sender, System.EventArgs e)
        {
            // Let the editor load...
            Application.DoEvents();

            // then setup the UI...
            BeginInvoke(new MethodInvoker(SetupUI));
        }

        private void commentList_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            Comment toSelect = null;
            if (commentList.SelectedItems.Count > 0)
            {
                toSelect = commentList.SelectedItems[0].Tag as Comment;
            }
            Selected = toSelect;
        }

        private void add_Click(object sender, EventArgs e)
        {
            Comment c = new Comment();
            c.Initialize();
            c.Author = authorText.Text;
            c.AuthorEmail = emailText.Text;
            c.AuthorHomepage = homepageText.Text;
            c.Content = commentEditor.Html;
            c.TargetEntryId = Entry.EntryId;

            browse.AddComment(c);

            DialogResult = DialogResult.OK;
        }
    }
}
