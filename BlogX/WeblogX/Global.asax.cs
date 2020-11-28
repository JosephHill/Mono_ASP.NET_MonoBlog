//=-------
// Copyright 2003, Chris Anderson
// 
//   Provided as is, with no warrenty, etc.
//   License is granted to use, copy, modify, 
//   with or without credit to me, just don't
//   blame me if it doesn't work.
//=-------
namespace Anderson.Chris.BlogX.WebClient 
{
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Web;
    using System.Web.Security;
    using System.Security.Principal;
    using System.Web.SessionState;
    using Anderson.Chris.BlogX.Runtime;

    /// <summary>
    /// Summary description for Global.
    /// </summary>
    public class Global : System.Web.HttpApplication
    {
        public Global()
        {
            InitializeComponent();
        }	

        protected void Application_Start(Object sender, EventArgs e)
        {
        }

        protected void Session_Start(Object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(Object sender, EventArgs e)
        {

        }

        protected void Application_EndRequest(Object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(Object sender, EventArgs e)
        {
            if (Request.IsAuthenticated == true) 
            {
                string role = null;

                // Create the roles cookie if it doesn't exist yet for this session.
                if ((Request.Cookies["portalroles"] == null) || (Request.Cookies["portalroles"].Value == "")) 
                {
                    // Get roles from UserRoles table, and add to cookie
                    UserToken token = SiteSecurity.GetToken(User.Identity.Name);
                    if (token != null)
                    {
                        role = token.Role;
                
                        // Create a cookie authentication ticket.
                        FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(
                            1,                              // version
                            Context.User.Identity.Name,     // user name
                            DateTime.Now,                   // issue time
                            DateTime.Now.AddHours(1),       // expires every hour
                            false,                          // don't persist cookie
                            role                            // roles
                            );

                        // Encrypt the ticket
                        String cookieStr = FormsAuthentication.Encrypt(ticket);

                        // Send the cookie to the client
                        Response.Cookies["portalroles"].Value = cookieStr;
                        Response.Cookies["portalroles"].Path = "/";
                        Response.Cookies["portalroles"].Expires = DateTime.Now.AddMinutes(1);
                    }
                    else
                    {
                        // This is hit for the case where the user
                        // has a cookie that points to an out of date
                        // user name. Basically we have to un-authenticate
                        // and redirect... 
                        //

                        // Log User Off from Cookie Authentication System
                        FormsAuthentication.SignOut();
      
                        // Invalidate roles token
                        Response.Cookies["portalroles"].Value = null;
                        Response.Cookies["portalroles"].Expires = new System.DateTime(1999, 10, 12);
                        Response.Cookies["portalroles"].Path = "/";
                    }
                }
                else 
                {

                    // Get roles from roles cookie
                    FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(Context.Request.Cookies["portalroles"].Value);

                    role = ticket.UserData;
                }

                // Add our own custom principal to the request containing the roles in the auth ticket
                Context.User = new GenericPrincipal(Context.User.Identity, new string[] {role});
            }
        }

        protected void Application_Error(Object sender, EventArgs e)
        {

        }

        protected void Session_End(Object sender, EventArgs e)
        {

        }

        protected void Application_End(Object sender, EventArgs e)
        {

        }
			
		#region Web Form Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {    
        }
		#endregion
    }
}

