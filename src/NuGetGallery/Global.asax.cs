using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace NuGetGallery
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {

        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            bool result = false;

            // See if supplied credentials are correct
            string authHeader = Request.Headers["Authorization"];
            if ((authHeader != null) && (authHeader.StartsWith("Basic")))
            {
                // Parse username and password out of the HTTP headers
                authHeader = authHeader.Substring("Basic".Length).Trim();
                byte[] authHeaderBytes = Convert.FromBase64String(authHeader);
                authHeader = Encoding.UTF7.GetString(authHeaderBytes);
                string userName = authHeader.Split(':')[0];
                string password = authHeader.Split(':')[1];

                result = userName == "admin" && password == "12qwaszx";
            }

            if(!result)
            {
                Response.StatusCode = 401;
                Response.AppendHeader("WWW-Authenticate", "Basic");

                Response.Write("401 Unauthorized");
                Response.End();
            }
        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}