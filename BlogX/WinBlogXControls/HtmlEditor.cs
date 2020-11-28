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
    using System.Collections;
    using System.ComponentModel;
    using System.Drawing;
    using System.Data;
    using System.Windows.Forms;
    using System.Text.RegularExpressions;
    using Anderson.Chris.BlogX.WindowsClient.Html;

    /// <summary>
    /// Summary description for HtmlEditor.
    /// </summary>
    public class HtmlEditor : System.Windows.Forms.UserControl
    {
        #region Designer Fields
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private Anderson.Chris.BlogX.WindowsClient.Html.HtmlEditor designDescription;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TextBox htmlDescription;
        private System.ComponentModel.IContainer components;
        private System.Windows.Forms.ToolBar toolBar1;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ToolBarButton boldButton;
        private System.Windows.Forms.ToolBarButton italicButton;
        private System.Windows.Forms.ToolBarButton linkButton;
        private System.Windows.Forms.ToolBarButton bulletButton;
        private System.Windows.Forms.ToolBarButton numberButton;
        #endregion

        bool loaded;

        public HtmlEditor()
        {
            // This call is required by the Windows.Forms Form Designer.
            InitializeComponent();

            designDescription.DesignModeEnabled = true;

            Bitmap b = new Bitmap(typeof(HtmlEditor), "toolbarstrip.bmp");
            imageList1.Images.AddStrip(b);
            imageList1.TransparentColor = b.GetPixel(0, 0);
            boldButton.ImageIndex = 0;
            italicButton.ImageIndex = 1;
            linkButton.ImageIndex = 2;
            bulletButton.ImageIndex = 3;
            numberButton.ImageIndex = 4;
        }

        public void BulletedList()
        {
            if (tabControl1.SelectedIndex == 0 && loaded)
            {
                designDescription.TextFormatting.SetHtmlFormat(HtmlFormat.UnorderedList);
            }
        }
        public void NumberedList()
        {
            if (tabControl1.SelectedIndex == 0 && loaded)
            {
                designDescription.TextFormatting.SetHtmlFormat(HtmlFormat.OrderedList);
            }
        }
        public void Bold()
        {
            if (tabControl1.SelectedIndex == 0 && loaded)
            {
                designDescription.TextFormatting.ToggleBold();
            }
            else
            {
                htmlDescription.SelectedText = "<b>" + htmlDescription.SelectedText + "</b>";
            }
        }

        public void Italic()
        {
            if (tabControl1.SelectedIndex == 0 && loaded)
            {
                designDescription.TextFormatting.ToggleItalics();
            }
            else
            {
                htmlDescription.SelectedText = "<i>" + htmlDescription.SelectedText + "</i>";
            }
        }


        public void InsertHyperlink()
        {
            if (tabControl1.SelectedIndex == 0 && loaded)
            {
                designDescription.Document.InsertHyperlink(null, null);
            }
            else
            {
                htmlDescription.SelectedText = "<a href=\"[url]\">" + htmlDescription.SelectedText + "</i>";
            }
        }

        public string Html
        {
            get
            {
                if (tabControl1.SelectedIndex == 0 && loaded)
                {
                    return FilterToBody(designDescription.SaveHtml());
                }
                else
                {
                    return htmlDescription.Text;
                }
            }
            set
            {
                htmlDescription.Text = value;
                if (loaded)
                {
                    designDescription.LoadHtml(htmlDescription.Text);
                }
            }
        }
		#region Component Designer generated code
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
            this.components = new System.ComponentModel.Container();
            System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(HtmlEditor));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.designDescription = new Anderson.Chris.BlogX.WindowsClient.Html.HtmlEditor();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.htmlDescription = new System.Windows.Forms.TextBox();
            this.toolBar1 = new System.Windows.Forms.ToolBar();
            this.boldButton = new System.Windows.Forms.ToolBarButton();
            this.italicButton = new System.Windows.Forms.ToolBarButton();
            this.linkButton = new System.Windows.Forms.ToolBarButton();
            this.bulletButton = new System.Windows.Forms.ToolBarButton();
            this.numberButton = new System.Windows.Forms.ToolBarButton();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
            this.tabControl1.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                                      this.tabPage1,
                                                                                      this.tabPage2});
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 55);
            this.tabControl1.Multiline = true;
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(600, 265);
            this.tabControl1.TabIndex = 9;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                                   this.designDescription});
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(592, 236);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Design";
            // 
            // designDescription
            // 
            this.designDescription.Dock = System.Windows.Forms.DockStyle.Fill;
            this.designDescription.Enabled = true;
            this.designDescription.Name = "designDescription";
            this.designDescription.Size = new System.Drawing.Size(592, 236);
            this.designDescription.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                                   this.htmlDescription});
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new System.Drawing.Size(592, 250);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "HTML";
            this.tabPage2.Visible = false;
            // 
            // htmlDescription
            // 
            this.htmlDescription.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.htmlDescription.Dock = System.Windows.Forms.DockStyle.Fill;
            this.htmlDescription.Multiline = true;
            this.htmlDescription.WordWrap = false;
            this.htmlDescription.Name = "htmlDescription";
            this.htmlDescription.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.htmlDescription.Font = new Font("Lucida Console", 10);
            this.htmlDescription.Size = new System.Drawing.Size(592, 250);
            this.htmlDescription.TabIndex = 5;
            this.htmlDescription.Text = "";
            // 
            // toolBar1
            // 
            this.toolBar1.Appearance = System.Windows.Forms.ToolBarAppearance.Flat;
            this.toolBar1.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
                                                                                        this.boldButton,
                                                                                        this.italicButton,
                                                                                        this.linkButton,
                                                                                        this.bulletButton,
                                                                                        this.numberButton});
            this.toolBar1.ButtonSize = new System.Drawing.Size(52, 38);
            this.toolBar1.DropDownArrows = true;
            this.toolBar1.ImageList = this.imageList1;
            this.toolBar1.Name = "toolBar1";
            this.toolBar1.ShowToolTips = true;
            this.toolBar1.Size = new System.Drawing.Size(600, 55);
            this.toolBar1.TabIndex = 10;
            this.toolBar1.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(this.toolBar1_ButtonClick);
            // 
            // boldButton
            // 
            this.boldButton.Tag = "bold";
            this.boldButton.Text = "Bold";
            // 
            // italicButton
            // 
            this.italicButton.Tag = "italic";
            this.italicButton.Text = "Italic";
            // 
            // linkButton
            // 
            this.linkButton.Tag = "link";
            this.linkButton.Text = "Link";
            // 
            // bulletButton
            // 
            this.bulletButton.Tag = "bullet";
            this.bulletButton.Text = "Bullet";
            // 
            // numberButton
            // 
            this.numberButton.Tag = "number";
            this.numberButton.Text = "Number";
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth24Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(32, 32);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // HtmlEditor
            // 
            this.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                          this.tabControl1,
                                                                          this.toolBar1});
            this.Name = "HtmlEditor";
            this.Size = new System.Drawing.Size(600, 320);
            this.Load += new System.EventHandler(this.HtmlEditor_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);

        }
		#endregion

        void SetupUI()
        {
            if (htmlDescription.Text.Length == 0)
            {
                designDescription.LoadHtml("<html xmlns=\"http://www.w3.org/1999/xhtml\"></html>");
            }
            else
            {
                designDescription.LoadHtml(htmlDescription.Text);
            }
            loaded = true;        
        }

        string FilterToBody(string html)
        {
            int beginBody = html.IndexOf("<body>");
            int endBody = html.IndexOf("</body>");
            if (beginBody != -1 && endBody != -1)
            {
                string s = Regex.Replace(html.Substring(beginBody, endBody + "</body>".Length - beginBody).Trim(), "<body>", "<body xmlns=\"http://www.w3.org/1999/xhtml\">");
                return s;
            }
            else
            {
                int beginBody2 = html.IndexOf("<body xmlns=\"http://www.w3.org/1999/xhtml\">");
                int endBody2 = html.IndexOf("</body>");
                if (beginBody2 != -1 && endBody2 != -1)
                {
                    string s = html.Substring(beginBody2, endBody2 + "</body>".Length - beginBody2).Trim();
                    return s;
                }
                else
                {
                    return html;
                }
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (tabControl1.SelectedIndex == 0)
            {
                designDescription.LoadHtml(htmlDescription.Text);
            }
            else
            {
                htmlDescription.Text = FilterToBody(designDescription.SaveHtml());
            }
        }

        private void HtmlEditor_Load(object sender, System.EventArgs e)
        {
            // Let the editor load...
            Application.DoEvents();

            // then setup the UI...
            BeginInvoke(new MethodInvoker(SetupUI));
        }

        private void toolBar1_ButtonClick(object sender, System.Windows.Forms.ToolBarButtonClickEventArgs e)
        {
            switch (e.Button.Tag.ToString())
            {
                case "link":
                    InsertHyperlink();
                    break;
                case "bold":
                    Bold();
                    break;
                case "italic":
                    Italic();
                    break;
                case "bullet":
                    BulletedList();
                    break;
                case "number":
                    NumberedList();
                    break;
            }
        }
    }
}
