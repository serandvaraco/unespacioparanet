using AlbumsSharing.App_Start;
using AlbumsSharing.Model.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace AlbumsSharing
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            Database.SetInitializer(new AlbumsInitializer());
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            FacBuilder.RegisterAutoFac(typeof(MvcApplication).Assembly); 
        }
    }
}
