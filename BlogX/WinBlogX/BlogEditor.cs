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
    using System.Text;
    using System.Drawing;
    using System.Collections;
    using System.ComponentModel;
    using System.Windows.Forms;
    using Anderson.Chris.BlogX.Runtime;
    using Anderson.Chris.BlogX.Services;

    /// <summary>
    /// Summary description for BlogEditor.
    /// </summary>
    class BlogEditor : BlogXForm
    {
        #region Designer Fields
        private HtmlEditor designDescription;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Splitter splitter1;
        private System.ComponentModel.Container components = null;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TextBox passwordText;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.CheckedListBox categoryList;
        private XButton addCategory;
        private System.Windows.Forms.TextBox newCategory;
        private XButton cancel;
        private XButton save;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.TextBox userNameText;
        private System.Windows.Forms.Label passwordLabel;
        private System.Windows.Forms.Label userNameLabel;
        private System.Windows.Forms.TextBox description;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox title;
        private System.Windows.Forms.Label label2;
        #endregion
        Entry entry;
        DayEntry day;
        BlogXBrowsing browse;
        WinBlogXProject project;
        bool loaded;

        public BlogEditor()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();
        }

        public string Password
        {
            get
            {
                return passwordText.Text;
            }
            set
            {
                passwordText.Text = value;
            }
        }

        public string Username
        {
            get
            {
                return userNameText.Text;
            }
            set
            {
                userNameText.Text = value;
            }
        }

        public WinBlogXProject Project
        {
            get
            {
                return project;
            }
            set
            {
                project = value;
                if (project != null)
                {
                    BlogXEditing editing = new BlogXEditing();
                    editing.Url = Project.EditingUrl;
                    bool needUserAndPassword = !editing.CanEdit(Username, Password);
                    userNameText.Visible = needUserAndPassword;
                    passwordText.Visible = needUserAndPassword;
                    passwordLabel.Visible = needUserAndPassword;
                    userNameLabel.Visible = needUserAndPassword;
                }
            }
        }

        public BlogXBrowsing Browse
        {
            get
            {
                return browse;
            }
            set
            {
                browse = value;

                categoryList.Items.Clear();
                
                foreach (string category in browse.GetCategoryList())
                {
                    categoryList.Items.Add(category);
                }
            }
        }

        public Entry Entry
        {
            get
            {
                if (entry == null)
                {
                    entry = new Entry();
                    entry.Initialize();
                }
                return entry;
            }
            set
            {
                entry = value;
                if (loaded)
                {
                    SetupUI();
                }
            }
        }

        public DayEntry Day
        {
            get { return day; }
            set { day = value; }
        }
        
        void SetupUI()
        {
            designDescription.Html = Entry.Content;
            title.Text = Entry.Title;
            description.Text = Entry.Description;

            foreach (string s in Entry.GetSplitCategories())
            {
                categoryList.SetItemChecked(categoryList.Items.IndexOf(s), true);
            }
            loaded = true;        
        }

        string FilterToBody(string html)
        {
            int beginBody = html.IndexOf("<BODY>");
            int endBody = html.IndexOf("</BODY>");
            if (beginBody != -1 && endBody != -1)
            {
                string s = html.Substring(beginBody + 6, endBody - (beginBody + 6)).Trim();
                return s;
            }
            else
            {
                return html;
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
            System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(BlogEditor));
            this.designDescription = new Anderson.Chris.BlogX.WindowsClient.HtmlEditor();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.categoryList = new System.Windows.Forms.CheckedListBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.newCategory = new System.Windows.Forms.TextBox();
            this.addCategory = new Anderson.Chris.BlogX.WindowsClient.XButton();
            this.label1 = new System.Windows.Forms.Label();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.passwordText = new System.Windows.Forms.TextBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.save = new Anderson.Chris.BlogX.WindowsClient.XButton();
            this.panel5 = new System.Windows.Forms.Panel();
            this.cancel = new Anderson.Chris.BlogX.WindowsClient.XButton();
            this.passwordLabel = new System.Windows.Forms.Label();
            this.userNameText = new System.Windows.Forms.TextBox();
            this.userNameLabel = new System.Windows.Forms.Label();
            this.description = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.title = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panel2.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // designDescription
            // 
            this.designDescription.Dock = System.Windows.Forms.DockStyle.Fill;
            this.designDescription.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
            this.designDescription.Html = "<body xmlns=\"http://www.w3.org/1999/xhtml\">\r\n    htmlDescription</body>";
            this.designDescription.Location = new System.Drawing.Point(5, 104);
            this.designDescription.Name = "designDescription";
            this.designDescription.Size = new System.Drawing.Size(478, 293);
            this.designDescription.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.DockPadding.Bottom = 5;
            this.panel1.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
            this.panel1.Location = new System.Drawing.Point(5, 5);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(678, 99);
            this.panel1.TabIndex = 6;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Transparent;
            this.panel2.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                                 this.categoryList,
                                                                                 this.panel4,
                                                                                 this.label1});
            this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel2.DockPadding.Bottom = 150;
            this.panel2.DockPadding.Left = 5;
            this.panel2.Location = new System.Drawing.Point(483, 104);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(200, 293);
            this.panel2.TabIndex = 7;
            // 
            // categoryList
            // 
            this.categoryList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.categoryList.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
            this.categoryList.IntegralHeight = false;
            this.categoryList.Location = new System.Drawing.Point(5, 48);
            this.categoryList.Name = "categoryList";
            this.categoryList.Size = new System.Drawing.Size(195, 95);
            this.categoryList.TabIndex = 0;
            // 
            // panel4
            // 
            this.panel4.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                                 this.newCategory,
                                                                                 this.addCategory});
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(5, 23);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(195, 25);
            this.panel4.TabIndex = 2;
            // 
            // newCategory
            // 
            this.newCategory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.newCategory.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
            this.newCategory.Name = "newCategory";
            this.newCategory.Size = new System.Drawing.Size(147, 24);
            this.newCategory.TabIndex = 0;
            this.newCategory.Text = "";
            // 
            // addCategory
            // 
            this.addCategory.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(120)), ((System.Byte)(255)), ((System.Byte)(255)), ((System.Byte)(255)));
            this.addCategory.Dock = System.Windows.Forms.DockStyle.Right;
            this.addCategory.Location = new System.Drawing.Point(147, 0);
            this.addCategory.Name = "addCategory";
            this.addCategory.Size = new System.Drawing.Size(48, 25);
            this.addCategory.TabIndex = 1;
            this.addCategory.Text = " Add";
            this.addCategory.Click += new System.EventHandler(this.addCategory_Click);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Location = new System.Drawing.Point(5, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(195, 23);
            this.label1.TabIndex = 1;
            this.label1.Text = "Categories";
            // 
            // splitter1
            // 
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Right;
            this.splitter1.Location = new System.Drawing.Point(480, 104);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 293);
            this.splitter1.TabIndex = 9;
            this.splitter1.TabStop = false;
            // 
            // passwordText
            // 
            this.passwordText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.passwordText.Dock = System.Windows.Forms.DockStyle.Left;
            this.passwordText.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
            this.passwordText.Location = new System.Drawing.Point(299, 0);
            this.passwordText.Name = "passwordText";
            this.passwordText.PasswordChar = '*';
            this.passwordText.Size = new System.Drawing.Size(133, 24);
            this.passwordText.TabIndex = 1;
            this.passwordText.Text = "";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Transparent;
            this.panel3.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                                 this.save,
                                                                                 this.panel5,
                                                                                 this.cancel,
                                                                                 this.passwordText,
                                                                                 this.passwordLabel,
                                                                                 this.userNameText,
                                                                                 this.userNameLabel});
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(5, 397);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(678, 24);
            this.panel3.TabIndex = 11;
            // 
            // save
            // 
            this.save.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(120)), ((System.Byte)(255)), ((System.Byte)(255)), ((System.Byte)(255)));
            this.save.Dock = System.Windows.Forms.DockStyle.Right;
            this.save.Location = new System.Drawing.Point(524, 0);
            this.save.Name = "save";
            this.save.Size = new System.Drawing.Size(75, 24);
            this.save.TabIndex = 2;
            this.save.Text = "Save";
            this.save.Click += new System.EventHandler(this.save_Click);
            // 
            // panel5
            // 
            this.panel5.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel5.Location = new System.Drawing.Point(599, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(4, 24);
            this.panel5.TabIndex = 14;
            // 
            // cancel
            // 
            this.cancel.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(120)), ((System.Byte)(255)), ((System.Byte)(255)), ((System.Byte)(255)));
            this.cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancel.Dock = System.Windows.Forms.DockStyle.Right;
            this.cancel.Location = new System.Drawing.Point(603, 0);
            this.cancel.Name = "cancel";
            this.cancel.Size = new System.Drawing.Size(75, 24);
            this.cancel.TabIndex = 3;
            this.cancel.Text = "Cancel";
            this.cancel.Click += new System.EventHandler(this.cancel_Click);
            // 
            // passwordLabel
            // 
            this.passwordLabel.AutoSize = true;
            this.passwordLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.passwordLabel.Location = new System.Drawing.Point(218, 0);
            this.passwordLabel.Name = "passwordLabel";
            this.passwordLabel.Size = new System.Drawing.Size(81, 17);
            this.passwordLabel.TabIndex = 11;
            this.passwordLabel.Text = "Password: ";
            // 
            // userNameText
            // 
            this.userNameText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.userNameText.Dock = System.Windows.Forms.DockStyle.Left;
            this.userNameText.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
            this.userNameText.Location = new System.Drawing.Point(85, 0);
            this.userNameText.Name = "userNameText";
            this.userNameText.Size = new System.Drawing.Size(133, 24);
            this.userNameText.TabIndex = 0;
            this.userNameText.Text = "";
            // 
            // userNameLabel
            // 
            this.userNameLabel.AutoSize = true;
            this.userNameLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.userNameLabel.Name = "userNameLabel";
            this.userNameLabel.Size = new System.Drawing.Size(85, 17);
            this.userNameLabel.TabIndex = 16;
            this.userNameLabel.Text = "User Name:";
            // 
            // description
            // 
            this.description.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.description.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
            this.description.Location = new System.Drawing.Point(128, 48);
            this.description.Multiline = true;
            this.description.Name = "description";
            this.description.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.description.Size = new System.Drawing.Size(552, 48);
            this.description.TabIndex = 14;
            this.description.Text = "";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
            this.label3.Location = new System.Drawing.Point(8, 48);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(117, 23);
            this.label3.TabIndex = 15;
            this.label3.Text = "Description";
            // 
            // title
            // 
            this.title.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.title.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
            this.title.Location = new System.Drawing.Point(128, 8);
            this.title.Name = "title";
            this.title.Size = new System.Drawing.Size(552, 36);
            this.title.TabIndex = 12;
            this.title.Text = "";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
            this.label2.Location = new System.Drawing.Point(8, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 29);
            this.label2.TabIndex = 13;
            this.label2.Text = "Title  ";
            // 
            // BlogEditor
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(7, 17);
            this.ClientSize = new System.Drawing.Size(688, 426);
            this.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                          this.description,
                                                                          this.label3,
                                                                          this.title,
                                                                          this.label2,
                                                                          this.splitter1,
                                                                          this.designDescription,
                                                                          this.panel2,
                                                                          this.panel1,
                                                                          this.panel3});
            this.DockPadding.All = 5;
            this.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
            this.ForeColor = System.Drawing.Color.White;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "BlogEditor";
            this.Text = "BlogEditor";
            this.Load += new System.EventHandler(this.BlogEditor_Load);
            this.panel2.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }
		#endregion

        private void BlogEditor_Load(object sender, System.EventArgs e)
        {
            // Let the editor load...
            Application.DoEvents();

            // then setup the UI...
            BeginInvoke(new MethodInvoker(SetupUI));
        }

        private void addCategory_Click(object sender, EventArgs e)
        {
            categoryList.Items.Add(newCategory.Text, true);
            newCategory.Text = "";
        }

        private void save_Click(object sender, System.EventArgs e)
        {
            BlogXEditing editing = new BlogXEditing();
            editing.Url = Project.EditingUrl;
            if (!editing.CanEdit(Username, Password))
            {
                MessageBox.Show("Invalid Username/Password Combination");
                return;
            }
            editing.Dispose();

            entry.Content = designDescription.Html;
            
            if (day == null)
            {
                DateTime timeCalc;
                DateTime dateCalc;
                timeCalc = DateTime.Now;
                dateCalc = DateTime.Now;

                entry.Created = new DateTime(dateCalc.Year, dateCalc.Month, dateCalc.Day, timeCalc.Hour, timeCalc.Minute, timeCalc.Second, timeCalc.Millisecond);
            }
            StringBuilder sb = new StringBuilder();
            bool needSemi = false;
            foreach (string category in categoryList.CheckedItems)
            {
                if (needSemi) sb.Append(";");
                sb.Append(category);
                needSemi = true;
            }
            entry.Categories = sb.ToString();
            entry.Title = title.Text;
            entry.Description = description.Text;
            entry.Content  = designDescription.Html;
            entry.Modify();
            DialogResult = DialogResult.OK;
        }

        private void cancel_Click(object sender, System.EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
