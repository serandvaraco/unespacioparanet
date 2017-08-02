using Autofac;
using Autofac.Integration.Mvc;
using FacRepositoriesContainer.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace FacRepositoriesContainer
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            var builder = new ContainerBuilder();
            builder.RegisterControllers(typeof(Global).Assembly);
            builder.RegisterFilterProvider();

            builder.RegisterType<ContextModel>().As<IContextModel>().InstancePerDependency().SingleInstance();
            builder.RegisterType<uowRep>().As<IuowRep>().InstancePerDependency().SingleInstance(); ;


            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

        }
    }
}
