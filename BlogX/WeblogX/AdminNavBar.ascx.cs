//= - - - - - - -
// Copyright 2003, Chris Anderson
// 
//   Provided as is, with no warrenty, etc.
//   License is granted to use, copy, modify, 
//   with or without credit to me, just don't
//   blame me if it doesn't work.
//= - - - - - - -
namespace Anderson.Chris.BlogX.WebClient
{
    using System;
    using System.Data;
    using System.Drawing;
    using System.Web;
    using System.Web.Security;
    using System.Web.UI.WebControls;
    using System.Web.UI.HtmlControls;

    /// <summary>
    ///		Summary description for AdminNavBar.
    /// </summary>
    public abstract class AdminNavBar : System.Web.UI.UserControl
    {
        protected System.Web.UI.WebControls.LinkButton logout;

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
            this.logout.Click += new System.EventHandler(this.logout_Click);
            this.Load += new System.EventHandler(this.Page_Load);

        }
		#endregion

        private void logout_Click(object sender, System.EventArgs e)
        {
            // Log User Off from Cookie Authentication System
            FormsAuthentication.SignOut();
      
            // Invalidate roles token
            Response.Cookies["portalroles"].Value = null;
            Response.Cookies["portalroles"].Expires = new System.DateTime(1999, 10, 12);
            Response.Cookies["portalroles"].Path = "/";
        
            // Redirect user back to the Portal Home Page
            Response.Redirect(Request.ApplicationPath);
        }
    }
}
