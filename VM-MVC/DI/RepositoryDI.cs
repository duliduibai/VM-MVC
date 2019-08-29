using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VM.IRepository;
using VM.Repository;

namespace VM_MVC.DI
{
    public static class RepositoryDI
    {
        public static void RegisterRepository(this ContainerBuilder builder)
        {
            //builder.RegisterType<VmDbContext>().SingleInstance();
            builder.RegisterType<VmDbContext>().InstancePerDependency();
            builder.RegisterType<ProductRepository>().As<IProductRepository>().InstancePerRequest();
            builder.RegisterType<OrderRepository>().As<IOrderRepository>().InstancePerRequest();
            builder.RegisterType<AccountRepository>().As<IAccountRepository>().InstancePerRequest();
        }
    }
}