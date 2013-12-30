using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Homework.App
{
    public class Global : HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            RouteTable.Routes.MapMvcAttributeRoutes();
        }
    }
}