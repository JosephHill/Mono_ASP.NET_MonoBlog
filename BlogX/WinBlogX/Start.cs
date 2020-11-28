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

    /// <summary>
    /// Summary description for Start.
    /// </summary>
    public class Start : BlogXForm
    {
        #region Designer Fields
        private System.Windows.Forms.RadioButton openExisting;
        private System.Windows.Forms.RadioButton createNew;
        private System.Windows.Forms.ListBox existingProjects;
        private Anderson.Chris.BlogX.WindowsClient.XButton ok;
        private Anderson.Chris.BlogX.WindowsClient.XButton cancel;
        private System.ComponentModel.Container components = null;
        #endregion

        public Start()
        {
            InitializeComponent();
            WinBlogXConfig config = WinBlogXConfig.Load();
            foreach (string file in config.StartDialog.Mru)
            {
                existingProjects.Items.Add(file);
            }
            existingProjects.Items.Add("More...");
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
            System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(Start));
            this.openExisting = new System.Windows.Forms.RadioButton();
            this.createNew = new System.Windows.Forms.RadioButton();
            this.existingProjects = new System.Windows.Forms.ListBox();
            this.ok = new Anderson.Chris.BlogX.WindowsClient.XButton();
            this.cancel = new Anderson.Chris.BlogX.WindowsClient.XButton();
            this.SuspendLayout();
            // 
            // openExisting
            // 
            this.openExisting.Anchor = ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
                | System.Windows.Forms.AnchorStyles.Right);
            this.openExisting.BackColor = System.Drawing.Color.Transparent;
            this.openExisting.Checked = true;
            this.openExisting.Location = new System.Drawing.Point(8, 8);
            this.openExisting.Name = "openExisting";
            this.openExisting.Size = new System.Drawing.Size(672, 24);
            this.openExisting.TabIndex = 0;
            this.openExisting.TabStop = true;
            this.openExisting.Text = "Open Existing";
            this.openExisting.DoubleClick += new System.EventHandler(this.openExisting_DoubleClick);
            // 
            // createNew
            // 
            this.createNew.Anchor = ((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
                | System.Windows.Forms.AnchorStyles.Right);
            this.createNew.BackColor = System.Drawing.Color.Transparent;
            this.createNew.Location = new System.Drawing.Point(8, 264);
            this.createNew.Name = "createNew";
            this.createNew.Size = new System.Drawing.Size(672, 24);
            this.createNew.TabIndex = 0;
            this.createNew.Text = "Create New";
            this.createNew.DoubleClick += new System.EventHandler(this.createNew_DoubleClick);
            // 
            // existingProjects
            // 
            this.existingProjects.Anchor = (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
                | System.Windows.Forms.AnchorStyles.Left) 
                | System.Windows.Forms.AnchorStyles.Right);
            this.existingProjects.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.existingProjects.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.existingProjects.Font = new System.Drawing.Font("Tahoma", 10F);
            this.existingProjects.IntegralHeight = false;
            this.existingProjects.ItemHeight = 34;
            this.existingProjects.Location = new System.Drawing.Point(24, 32);
            this.existingProjects.Name = "existingProjects";
            this.existingProjects.Size = new System.Drawing.Size(656, 224);
            this.existingProjects.TabIndex = 1;
            this.existingProjects.DoubleClick += new System.EventHandler(this.existingProjects_DoubleClick);
            this.existingProjects.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.existingProjects_DrawItem);
            this.existingProjects.Enter += new System.EventHandler(this.existingProjects_Enter);
            this.existingProjects.SelectedIndexChanged += new System.EventHandler(this.existingProjects_SelectedIndexChanged);
            // 
            // ok
            // 
            this.ok.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
            this.ok.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(120)), ((System.Byte)(255)), ((System.Byte)(255)), ((System.Byte)(255)));
            this.ok.Location = new System.Drawing.Point(520, 296);
            this.ok.Name = "ok";
            this.ok.TabIndex = 2;
            this.ok.Text = "OK";
            this.ok.Click += new System.EventHandler(this.ok_Click);
            // 
            // cancel
            // 
            this.cancel.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
            this.cancel.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(120)), ((System.Byte)(255)), ((System.Byte)(255)), ((System.Byte)(255)));
            this.cancel.Location = new System.Drawing.Point(600, 296);
            this.cancel.Name = "cancel";
            this.cancel.TabIndex = 2;
            this.cancel.Text = "Cancel";
            this.cancel.Click += new System.EventHandler(this.cancel_Click);
            // 
            // Start
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(7, 17);
            this.ClientSize = new System.Drawing.Size(688, 330);
            this.ControlBox = false;
            this.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                          this.ok,
                                                                          this.existingProjects,
                                                                          this.openExisting,
                                                                          this.createNew,
                                                                          this.cancel});
            this.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.ForeColor = System.Drawing.Color.White;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Start";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Start";
            this.ResumeLayout(false);

        }
		#endregion

        private void createNew_DoubleClick(object sender, System.EventArgs e)
        {
            ok.PerformClick();
        }

        private void openExisting_DoubleClick(object sender, System.EventArgs e)
        {
            ok.PerformClick();
        }

        private void existingProjects_DoubleClick(object sender, System.EventArgs e)
        {
            openExisting.Checked = true;
            if (existingProjects.SelectedItem.ToString() != "More...")
            {
                Open(existingProjects.SelectedItem.ToString());
            }
            else
            {
                ok_Click(sender, e);
            }
        }

        private void existingProjects_Enter(object sender, System.EventArgs e)
        {
            openExisting.Checked = true;
        }

        void Open(string path)
        {
            WinBlogXProject proj = null;

            XmlSerializer ser = new XmlSerializer(typeof(WinBlogXProject));
            using (StreamReader reader = new StreamReader(path))
            {
                proj = ser.Deserialize(reader) as WinBlogXProject;
            }
            if (proj != null)
            {
                proj.Upgrade();

                WinBlogXConfig config = WinBlogXConfig.Load();
                config.StartDialog.AddMru(path);
                WinBlogXConfig.Save(config);

                SimpleBrowser browser = new SimpleBrowser(proj);
                browser.Show();

                BeginInvoke(new MethodInvoker(Close));
            }
        }

        private void ok_Click(object sender, System.EventArgs e)
        {
            if (openExisting.Checked)
            {
                if (existingProjects.SelectedItem != null && existingProjects.SelectedItem.ToString() != "More...")
                {
                    Open(existingProjects.SelectedItem.ToString());
                }
                else
                {
                    OpenFileDialog dlg = new OpenFileDialog();
                    dlg.Filter = "WinBlogX Project Files (*.winblogx)|*.winblogx";
                    dlg.Title = "Open Project";
                    this.Visible = true;
                    if (dlg.ShowDialog(this) == DialogResult.OK)
                    {
                        Open(dlg.FileName);
                    }
                }
            }
            else
            {
                WinBlogXProject proj = new WinBlogXProject();
                proj.Upgrade();

                ProjectEditor editor = new ProjectEditor();
                editor.Project = proj;
                if (editor.ShowDialog(this) == DialogResult.OK)
                {
                    SaveFileDialog dlg = new SaveFileDialog();
                    dlg.Filter = "WinBlogX Project Files (*.winblogx)|*.winblogx";
                    if (dlg.ShowDialog(this) == DialogResult.OK)
                    {
                        XmlSerializer ser = new XmlSerializer(typeof(WinBlogXProject));
                        using (StreamWriter writer = new StreamWriter(dlg.OpenFile()))
                        {
                            ser.Serialize(writer, proj);
                        }
                        Open(dlg.FileName);
                    }
                }
            }
        }

        private void cancel_Click(object sender, System.EventArgs e)
        {
            Close();
        }

        private void existingProjects_SelectedIndexChanged(object sender, System.EventArgs e)
        {
        
        }


        Bitmap backing;
        Rectangle backingSize;

        private void existingProjects_DrawItem(object sender, System.Windows.Forms.DrawItemEventArgs e)
        {
            if (e.Bounds != backingSize)
            {
                backingSize = e.Bounds;
                backingSize.X = 0;
                backingSize.Y = 0;
                if (backing != null) backing.Dispose();
                backing = new Bitmap(e.Bounds.Width, e.Bounds.Height);
            }
            Graphics g = Graphics.FromImage(backing);
            g.FillRectangle(Brushes.White, backingSize);
            XPaint.PaintXGem(g, 0, 0);
            Rectangle textBounds = backingSize;
            textBounds.X += 34;
            textBounds.Width -= 34*2;
            StringFormat fmt = new StringFormat();
            fmt.LineAlignment = StringAlignment.Center;
            g.DrawString(((ListBox)sender).Items[e.Index].ToString(), e.Font, Brushes.Black, textBounds, fmt);
            if ((e.State & DrawItemState.Selected) != 0)
            {
                using (Brush b = new SolidBrush(Color.FromArgb(120, Color.Cyan)))
                {
                    g.FillRectangle(b, backingSize);
                }
            }
            g.Dispose();
            e.Graphics.DrawImageUnscaled(backing, e.Bounds.X, e.Bounds.Y);
            e.DrawFocusRectangle();
        }
    }
}
