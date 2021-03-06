namespace Anderson.Chris.BlogX.WebClient
{
    using System;
    using System.Reflection;
    using System.Configuration;
    using System.Data;
    using System.Drawing;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using System.Web.UI.HtmlControls;

    /// <summary>
    ///		Summary description for BlogXFooter.
    /// </summary>
    public abstract class BlogXFooter : System.Web.UI.UserControl
    {
        private void Page_Load(object sender, System.EventArgs e)
        {
            // Put user code to initialize the page here
        }

		#region Web Form Designer generated code
        override protected void OnInit(EventArgs e)
        {
            //
            // CODEGEN: This call is required by the ASP.NET Web Form Designer.
            //
            InitializeComponent();
            base.OnInit(e);
        }
		
        ///		Required method for Designer support - do not modify
        ///		the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Load += new System.EventHandler(this.Page_Load);
            this.PreRender += new System.EventHandler(this.BlogXFooter_PreRender);

        }
		#endregion

        private void BlogXFooter_PreRender(object sender, System.EventArgs e)
        {
            Controls.Add(new LiteralControl("<!-- Generated by BlogX v." +
                Assembly.GetExecutingAssembly().GetName().Version.ToString() +
                " -->"));
        }
    }
}
