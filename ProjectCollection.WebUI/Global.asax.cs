using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace ProjectCollection.WebUI
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {
            // this.Server.Transfer("~/pages/login.aspx", false);
            // this.Server.Transfer("~/pages/error.aspx", false);
        }
    }
}