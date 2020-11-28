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
    using System.Xml;
    using System.Xml.Serialization;
    using System.IO;
    using System.Drawing;
    using System.Collections;
    using System.ComponentModel;
    using System.Windows.Forms;
    using Anderson.Chris.BlogX.Runtime;
    using Anderson.Chris.BlogX.Services;

    /// <summary>
    /// Summary description for ProjectEditor.
    /// </summary>
    public class ProjectEditor : BlogXForm
    {
        #region Designer Fields
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox editing;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox browsing;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox localCache;
        private XButton ok;
        private XButton cancel;
        private System.ComponentModel.Container components = null;
        private System.Windows.Forms.CheckBox detail;
        private System.Windows.Forms.TextBox baseUrl;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox config;
        private System.Windows.Forms.Label label5;
        #endregion

        WinBlogXProject project;

        public ProjectEditor()
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
                return project;
            }
            set
            {
                project = value;
                if (project.IsDefault)
                {
                    baseUrl.Text =  "http://localhost/weblogx/";
                }
                else
                {
                    detail.Checked = false;
                    browsing.Text = project.BrowsingUrl;
                    editing.Text = project.EditingUrl;
                    config.Text = project.ConfigEditingUrl;
                    localCache.Text = project.LocalCacheDir;
                }
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
            System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(ProjectEditor));
            this.label1 = new System.Windows.Forms.Label();
            this.editing = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.browsing = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.localCache = new System.Windows.Forms.TextBox();
            this.ok = new Anderson.Chris.BlogX.WindowsClient.XButton();
            this.cancel = new Anderson.Chris.BlogX.WindowsClient.XButton();
            this.detail = new System.Windows.Forms.CheckBox();
            this.baseUrl = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.config = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(8, 72);
            this.label1.Name = "label1";
            this.label1.TabIndex = 0;
            this.label1.Text = "Editing:";
            // 
            // editing
            // 
            this.editing.Anchor = ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
                | System.Windows.Forms.AnchorStyles.Right);
            this.editing.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.editing.Enabled = false;
            this.editing.Font = new System.Drawing.Font("Tahoma", 10F);
            this.editing.Location = new System.Drawing.Point(112, 72);
            this.editing.Name = "editing";
            this.editing.Size = new System.Drawing.Size(344, 24);
            this.editing.TabIndex = 1;
            this.editing.Text = "http://www.simplegeek.com/blogxediting.asmx";
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(8, 96);
            this.label2.Name = "label2";
            this.label2.TabIndex = 2;
            this.label2.Text = "Browing:";
            // 
            // browsing
            // 
            this.browsing.Anchor = ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
                | System.Windows.Forms.AnchorStyles.Right);
            this.browsing.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.browsing.Enabled = false;
            this.browsing.Font = new System.Drawing.Font("Tahoma", 10F);
            this.browsing.Location = new System.Drawing.Point(112, 96);
            this.browsing.Name = "browsing";
            this.browsing.Size = new System.Drawing.Size(344, 24);
            this.browsing.TabIndex = 3;
            this.browsing.Text = "http://www.simplegeek.com/blogxbrowsing.asmx";
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Location = new System.Drawing.Point(8, 144);
            this.label3.Name = "label3";
            this.label3.TabIndex = 4;
            this.label3.Text = "Local Cache:";
            // 
            // localCache
            // 
            this.localCache.Anchor = ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
                | System.Windows.Forms.AnchorStyles.Right);
            this.localCache.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.localCache.Enabled = false;
            this.localCache.Font = new System.Drawing.Font("Tahoma", 10F);
            this.localCache.Location = new System.Drawing.Point(112, 144);
            this.localCache.Name = "localCache";
            this.localCache.Size = new System.Drawing.Size(344, 24);
            this.localCache.TabIndex = 5;
            this.localCache.Text = "";
            // 
            // ok
            // 
            this.ok.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
            this.ok.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(120)), ((System.Byte)(255)), ((System.Byte)(255)), ((System.Byte)(255)));
            this.ok.Location = new System.Drawing.Point(276, 176);
            this.ok.Name = "ok";
            this.ok.Size = new System.Drawing.Size(88, 28);
            this.ok.TabIndex = 6;
            this.ok.Text = "OK";
            this.ok.Click += new System.EventHandler(this.ok_Clicked);
            // 
            // cancel
            // 
            this.cancel.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
            this.cancel.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(120)), ((System.Byte)(255)), ((System.Byte)(255)), ((System.Byte)(255)));
            this.cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancel.Location = new System.Drawing.Point(368, 176);
            this.cancel.Name = "cancel";
            this.cancel.Size = new System.Drawing.Size(88, 28);
            this.cancel.TabIndex = 7;
            this.cancel.Text = "Cancel";
            this.cancel.Click += new System.EventHandler(this.cancel_Clicked);
            // 
            // detail
            // 
            this.detail.BackColor = System.Drawing.Color.Transparent;
            this.detail.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.detail.Location = new System.Drawing.Point(80, 40);
            this.detail.Name = "detail";
            this.detail.Size = new System.Drawing.Size(324, 24);
            this.detail.TabIndex = 8;
            this.detail.Text = "Adjust Details (you don\'t need to do this)";
            this.detail.CheckedChanged += new System.EventHandler(this.detail_CheckedChanged);
            // 
            // baseUrl
            // 
            this.baseUrl.Anchor = ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
                | System.Windows.Forms.AnchorStyles.Right);
            this.baseUrl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.baseUrl.Font = new System.Drawing.Font("Tahoma", 10F);
            this.baseUrl.Location = new System.Drawing.Point(112, 8);
            this.baseUrl.Name = "baseUrl";
            this.baseUrl.Size = new System.Drawing.Size(344, 24);
            this.baseUrl.TabIndex = 9;
            this.baseUrl.Text = "http://www.simplegeek.com";
            this.baseUrl.TextChanged += new System.EventHandler(this.baseUrl_TextChanged);
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Location = new System.Drawing.Point(8, 12);
            this.label4.Name = "label4";
            this.label4.TabIndex = 10;
            this.label4.Text = "Base URL:";
            // 
            // config
            // 
            this.config.Anchor = ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
                | System.Windows.Forms.AnchorStyles.Right);
            this.config.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.config.Enabled = false;
            this.config.Font = new System.Drawing.Font("Tahoma", 10F);
            this.config.Location = new System.Drawing.Point(112, 120);
            this.config.Name = "config";
            this.config.Size = new System.Drawing.Size(344, 24);
            this.config.TabIndex = 12;
            this.config.Text = "http://www.simplegeek.com/configediting.asmx";
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Location = new System.Drawing.Point(8, 120);
            this.label5.Name = "label5";
            this.label5.TabIndex = 11;
            this.label5.Text = "Config:";
            // 
            // ProjectEditor
            // 
            this.AcceptButton = this.ok;
            this.AutoScaleBaseSize = new System.Drawing.Size(7, 17);
            this.CancelButton = this.cancel;
            this.ClientSize = new System.Drawing.Size(464, 210);
            this.ControlBox = false;
            this.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                          this.config,
                                                                          this.label5,
                                                                          this.label4,
                                                                          this.baseUrl,
                                                                          this.detail,
                                                                          this.cancel,
                                                                          this.ok,
                                                                          this.localCache,
                                                                          this.label3,
                                                                          this.browsing,
                                                                          this.label2,
                                                                          this.editing,
                                                                          this.label1});
            this.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.ForeColor = System.Drawing.Color.White;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimizeBox = false;
            this.Name = "ProjectEditor";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Project Editor";
            this.ResumeLayout(false);

        }
		#endregion

        private void ok_Clicked(object sender, EventArgs e)
        {
            project.BrowsingUrl = browsing.Text;
            project.EditingUrl = editing.Text;
            project.LocalCacheDir = localCache.Text;

            BlogXBrowsing browse = new BlogXBrowsing();
            browse.Url = project.BrowsingUrl;
            XmlNode nodeRoot = browse.GetRssWithDayCount(1);
            StringWriter sw = new StringWriter();
            nodeRoot.WriteTo(new XmlTextWriter(sw));
            XmlSerializer ser = new XmlSerializer(typeof(RssRoot));
            RssRoot root = (RssRoot)ser.Deserialize(new StringReader(sw.ToString()));

            project.Name = root.Channels[0].Title;

            if (project.LocalCacheDir == null || project.LocalCacheDir == "")
            {
                project.LocalCacheDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "WinBlogX\\Cache\\" + project.Name);
            }

            Directory.CreateDirectory(project.LocalCacheDir);

            DialogResult = DialogResult.OK;
        }

        private void cancel_Clicked(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void baseUrl_TextChanged(object sender, System.EventArgs e)
        {
            if (!detail.Checked)
            {
                try
                {
                    browsing.Text = new Uri(new Uri(baseUrl.Text), "blogxbrowsing.asmx").ToString();
                    editing.Text = new Uri(new Uri(baseUrl.Text), "blogxediting.asmx").ToString();
                    config.Text = new Uri(new Uri(baseUrl.Text), "configediting.asmx").ToString();
                }
                catch (UriFormatException)
                {
                }
            }
        }

        private void detail_CheckedChanged(object sender, System.EventArgs e)
        {
            editing.Enabled = browsing.Enabled = config.Enabled = localCache.Enabled = detail.Checked;
        }
    }
}
