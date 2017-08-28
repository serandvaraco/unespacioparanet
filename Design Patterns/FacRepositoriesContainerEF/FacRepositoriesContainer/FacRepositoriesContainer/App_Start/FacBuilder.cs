using Autofac;
using Autofac.Integration.Mvc;
using FacRepositoriesContainer.Models;
using FacRepositoriesContainer.Repository;
using FacRepositoriesContainer.UoW;
using System;
using System.Reflection;
using System.Web.Mvc;

namespace FacRepositoriesContainer.App_Start
{
    public class FacBuilder
    {
        public static void RegisterAutoFac(Assembly assembly) {

            var builder = new ContainerBuilder();
            builder.RegisterControllers(assembly);
            builder.RegisterFilterProvider();

            builder.RegisterType<AlbumsRepositories>().As<IAlbumsRepository>();
            builder.RegisterType<UoWModel>().As<IUoWModel>().InstancePerDependency().SingleInstance().WithParameter("dbContext", new AlbumsContext());

            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));  
        }

    }
}