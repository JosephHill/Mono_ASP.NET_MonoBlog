namespace Anderson.Chris.BlogX.WindowsClient
{
    using System;
    using System.Drawing;
    using System.Collections;
    using System.ComponentModel;
    using System.Windows.Forms;

    /// <summary>
    /// Summary description for WaitDialog.
    /// </summary>
    public class WaitDialog : BlogXForm
    {
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.ComponentModel.IContainer components;
        private System.Windows.Forms.Timer timer1;
        private XButton kill;

        DateTime startTime;

        public WaitDialog()
        {
            startTime = DateTime.Now;
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();

            //
            // TODO: Add any constructor code after InitializeComponent call
            //
        }

        public double TotalMilliseconds 
        {
            get
            {
                return (DateTime.Now - startTime).TotalMilliseconds;
            }
        }

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

		#region Windows Form Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(WaitDialog));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.kill = new Anderson.Chris.BlogX.WindowsClient.XButton();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(5, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(576, 122);
            this.label1.TabIndex = 0;
            this.label1.Text = @"Yes, I know showing a wait dialog is wrong. You should really never show one that lacks a cancel button. But for now, this is better than a random pause in the application. At least you know that you are waiting for something to happen. I'll work on fixing this later.";
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Lucida Console", 12F);
            this.label2.Location = new System.Drawing.Point(512, 112);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 16);
            this.label2.TabIndex = 1;
            this.label2.Text = "00000";
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 150;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // kill
            // 
            this.kill.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(120)), ((System.Byte)(255)), ((System.Byte)(255)), ((System.Byte)(255)));
            this.kill.Font = new System.Drawing.Font("Tahoma", 10F);
            this.kill.Location = new System.Drawing.Point(8, 104);
            this.kill.Name = "kill";
            this.kill.Size = new System.Drawing.Size(88, 24);
            this.kill.TabIndex = 2;
            this.kill.Text = "Kill Program";
            this.kill.Click += new System.EventHandler(this.kill_Click);
            // 
            // WaitDialog
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(9, 20);
            this.BackColor = System.Drawing.SystemColors.Info;
            this.ClientSize = new System.Drawing.Size(586, 132);
            this.ControlBox = false;
            this.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                          this.kill,
                                                                          this.label2,
                                                                          this.label1});
            this.DockPadding.All = 5;
            this.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "WaitDialog";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Annoying wait dialog";
            this.ResumeLayout(false);

        }
		#endregion

        private void timer1_Tick(object sender, System.EventArgs e)
        {
            label2.Text = TotalMilliseconds.ToString("00000");
        }

        private void kill_Click(object sender, System.EventArgs e)
        {
            if (MessageBox.Show("This will quite WinBlogX. Continue?", "WinBlogX", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                BeginInvoke(new MethodInvoker(Application.Exit));
            }
        }
    }
}
