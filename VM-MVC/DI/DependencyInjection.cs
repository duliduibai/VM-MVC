using Autofac;
using Autofac.Integration.Mvc;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using VM_MVC.Models;

namespace VM_MVC.DI
{
    public static class DependencyInjection
    {
        public static void RegisterDI()
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(Assembly.GetExecutingAssembly());
            builder.RegisterRepository();
            builder.RegisterService();
            //builder.RegisterType<ShoppingCartBinder>().As<IModelBinder>().InstancePerDependency();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(builder.Build()));
        }
    }
}