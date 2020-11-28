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
    ///		Summary description for SignIn.
    /// </summary>
    public abstract class SignIn : System.Web.UI.UserControl
    {
        protected System.Web.UI.WebControls.TextBox username;
        protected System.Web.UI.WebControls.TextBox password;
        protected System.Web.UI.WebControls.Button doSignIn;
        protected System.Web.UI.WebControls.CheckBox rememberCheckbox;

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
            this.doSignIn.Click += new System.EventHandler(this.doSignIn_Click);
            this.Load += new System.EventHandler(this.Page_Load);

        }
		#endregion

        private void doSignIn_Click(object sender, System.EventArgs e)
        {
            UserToken token = SiteSecurity.Login(username.Text, password.Text);
            if (token != null)
            {
                FormsAuthentication.SetAuthCookie(token.Name, rememberCheckbox.Checked);
                Response.Redirect(Request.ApplicationPath);
            }
        }
    }
}
