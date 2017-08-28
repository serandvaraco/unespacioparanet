using FacRepositoriesContainer.App_Start;
using FacRepositoriesContainer.Models;
using System.Data.Entity;
using System.Web.Mvc;
using System.Web.Routing;

namespace FacRepositoriesContainer
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
