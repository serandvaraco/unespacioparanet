using System;
using System.Reflection;
using Autofac;
using Autofac.Integration.Mvc;
using AlbumsSharing.Model.Repositories;
using AlbumsSharing.Model;
using AlbumsSharing.Model.Entities;
using System.Web.Mvc;

namespace AlbumsSharing.App_Start
{
    public class FacBuilder
    {
        internal static void RegisterAutoFac(Assembly assembly)
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(assembly);
            builder.RegisterFilterProvider();

            builder.RegisterType<AlbumsRepository>().As<IAlbumsRepository>();
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerDependency()
                .SingleInstance().WithParameter("albums", new AlbumsContext());

            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

        }
    }
}