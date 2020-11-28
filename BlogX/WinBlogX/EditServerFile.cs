namespace Anderson.Chris.BlogX.WindowsClient
{
    using System;
    using System.IO;
    using System.Text.RegularExpressions;
    using System.Drawing;
    using System.Collections;
    using System.ComponentModel;
    using System.Windows.Forms;
    using Anderson.Chris.BlogX.Services;
    using Anderson.Chris.BlogX.Runtime;

    /// <summary>
    /// Summary description for EditServerFile.
    /// </summary>
    public class EditServerFile : BlogXForm
    {
        #region Designer Fields

        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.TextBox fileText;
        private XButton save;
        private System.Windows.Forms.TreeView fileList;
        private System.Windows.Forms.TextBox password;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox userName;
        private XButton login;
        #endregion

        WinBlogXProject proj;
        ConfigEditing config;
        string viewingFile;
        bool dirty = false;


        public EditServerFile()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();

            //
            // TODO: Add any constructor code after InitializeComponent call
            //
        }

        public WinBlogXProject Project
        {
            get
            {
                return proj;
            }
            set
            {
                proj = value;
                config = new ConfigEditing();
                config.Url = proj.ConfigEditingUrl;
                UpdateFiles();
            }
        }

        bool Dirty
        {
            get
            {
                return dirty;
            }
            set
            {
                dirty = value;
                save.Enabled = dirty;
            }
        }

        string ViewingFile
        {
            get
            {
                return viewingFile;
            }
            set
            {
                if (IsOkToChange())
                {
                    viewingFile = value;
                    if (viewingFile == null)
                    {
                        fileText.Text = "";
                    }
                    else
                    {
                        fileText.Text = new StreamReader(new MemoryStream(config.ReadFile(viewingFile, userName.Text, password.Text))).ReadToEnd();
                    }
                    Dirty = false;
                }
            }
        }

        #region Windows Form Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(EditServerFile));
            this.fileText = new System.Windows.Forms.TextBox();
            this.save = new Anderson.Chris.BlogX.WindowsClient.XButton();
            this.fileList = new System.Windows.Forms.TreeView();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.password = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.userName = new System.Windows.Forms.TextBox();
            this.login = new Anderson.Chris.BlogX.WindowsClient.XButton();
            this.SuspendLayout();
            // 
            // fileText
            // 
            this.fileText.AcceptsReturn = true;
            this.fileText.AcceptsTab = true;
            this.fileText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.fileText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fileText.Font = new System.Drawing.Font("Lucida Console", 10F);
            this.fileText.Location = new System.Drawing.Point(187, 70);
            this.fileText.Multiline = true;
            this.fileText.Name = "fileText";
            this.fileText.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.fileText.Size = new System.Drawing.Size(483, 330);
            this.fileText.TabIndex = 4;
            this.fileText.Text = "";
            this.fileText.WordWrap = false;
            this.fileText.TextChanged += new System.EventHandler(this.fileText_TextChanged);
            // 
            // save
            // 
            this.save.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
            this.save.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(120)), ((System.Byte)(255)), ((System.Byte)(255)), ((System.Byte)(255)));
            this.save.Enabled = false;
            this.save.Location = new System.Drawing.Point(596, 44);
            this.save.Name = "save";
            this.save.TabIndex = 5;
            this.save.Text = "Save";
            this.save.Click += new System.EventHandler(this.save_Click);
            // 
            // fileList
            // 
            this.fileList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.fileList.Dock = System.Windows.Forms.DockStyle.Left;
            this.fileList.Font = new System.Drawing.Font("Tahoma", 10F);
            this.fileList.ImageIndex = -1;
            this.fileList.Location = new System.Drawing.Point(10, 70);
            this.fileList.Name = "fileList";
            this.fileList.SelectedImageIndex = -1;
            this.fileList.Size = new System.Drawing.Size(174, 330);
            this.fileList.TabIndex = 3;
            this.fileList.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.fileList_AfterSelect);
            this.fileList.BeforeSelect += new System.Windows.Forms.TreeViewCancelEventHandler(this.fileList_BeforeSelect);
            // 
            // splitter1
            // 
            this.splitter1.BackColor = System.Drawing.Color.Navy;
            this.splitter1.Location = new System.Drawing.Point(184, 70);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 330);
            this.splitter1.TabIndex = 5;
            this.splitter1.TabStop = false;
            // 
            // password
            // 
            this.password.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.password.Font = new System.Drawing.Font("Tahoma", 10F);
            this.password.Location = new System.Drawing.Point(332, 24);
            this.password.Name = "password";
            this.password.PasswordChar = '*';
            this.password.Size = new System.Drawing.Size(144, 24);
            this.password.TabIndex = 1;
            this.password.Text = "";
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(332, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(144, 16);
            this.label2.TabIndex = 8;
            this.label2.Text = "Password:";
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(8, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(176, 48);
            this.label1.TabIndex = 6;
            this.label1.Text = "Type in password, then file list will be populated";
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Location = new System.Drawing.Point(184, 8);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(144, 16);
            this.label3.TabIndex = 8;
            this.label3.Text = "User Name:";
            // 
            // userName
            // 
            this.userName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.userName.Font = new System.Drawing.Font("Tahoma", 10F);
            this.userName.Location = new System.Drawing.Point(184, 24);
            this.userName.Name = "userName";
            this.userName.Size = new System.Drawing.Size(144, 24);
            this.userName.TabIndex = 0;
            this.userName.Text = "";
            // 
            // login
            // 
            this.login.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(120)), ((System.Byte)(255)), ((System.Byte)(255)), ((System.Byte)(255)));
            this.login.Location = new System.Drawing.Point(480, 4);
            this.login.Name = "login";
            this.login.TabIndex = 2;
            this.login.Text = "Login";
            this.login.Click += new System.EventHandler(this.login_Click);
            // 
            // EditServerFile
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(7, 17);
            this.ClientSize = new System.Drawing.Size(680, 410);
            this.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                          this.login,
                                                                          this.password,
                                                                          this.label2,
                                                                          this.label1,
                                                                          this.fileText,
                                                                          this.splitter1,
                                                                          this.fileList,
                                                                          this.save,
                                                                          this.label3,
                                                                          this.userName});
            this.DockPadding.Bottom = 10;
            this.DockPadding.Left = 10;
            this.DockPadding.Right = 10;
            this.DockPadding.Top = 70;
            this.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.ForeColor = System.Drawing.Color.White;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "EditServerFile";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Edit Server Files";
            this.Closing += new System.ComponentModel.CancelEventHandler(this.EditServerFile_Closing);
            this.ResumeLayout(false);

        }
		#endregion

        void UpdateFiles()
        {
            try
            {
                string[] files = config.GetFiles(userName.Text, password.Text);
                fileList.Nodes.Clear();
                TreeNode root = new TreeNode("Files");
                fileList.Nodes.Add(root);
                foreach (string file in files)
                {
                    TreeNode fileNode = new TreeNode();
                    fileNode.Text = file;
                    root.Nodes.Add(fileNode);
                    fileNode.Expand();
                }
                ViewingFile = null;
            }
            catch
            {
                fileList.Nodes.Clear();
            }
   
        }


        void Save()
        {
            MemoryStream ms = new MemoryStream();
            StreamWriter sw = new StreamWriter(ms);
            sw.Write(fileText.Text);
            sw.Flush();
            sw.Close();
            config.UpdateFile(viewingFile, ms.ToArray(), userName.Text, password.Text);
            Dirty = false;
        }

        private void fileList_AfterSelect(object sender, System.Windows.Forms.TreeViewEventArgs e)
        {
            ViewingFile = e.Node.Text;
        }

        private void save_Click(object sender, System.EventArgs e)
        {
            Save();
        }

        private void fileList_BeforeSelect(object sender, System.Windows.Forms.TreeViewCancelEventArgs e)
        {
            e.Cancel = !IsOkToChange();
        }

        private void fileText_TextChanged(object sender, System.EventArgs e)
        {
            Dirty = true;
        }

        private void EditServerFile_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = !IsOkToChange();
        }

        bool IsOkToChange()
        {
            if (dirty && viewingFile != null)
            {
                DialogResult r = MessageBox.Show("There are changes, do you want to save?", "WinBlogX", MessageBoxButtons.YesNoCancel);
                switch (r)
                {
                    case DialogResult.Yes:
                        Save();
                        return !dirty;
                    case DialogResult.No:
                        return true;
                    case DialogResult.Cancel:
                        return false;
                }
            }
            return true;
        }

        private void login_Click(object sender, System.EventArgs e)
        {
            UpdateFiles();
        }
    }
}
