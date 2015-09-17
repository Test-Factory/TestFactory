using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using NHibernateDataProviders.Data_Providers;
using TestFactory.Business.Data_Provider_Contracts;
using TestFactory.NHibernateDataProvider.Data_Providers;
using TestFactory.Business.Components.Managers;

namespace TestFactory.App_Start
{
    public class AutofacConfig
    {
        public static void RegisterDependencies()
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(Assembly.GetExecutingAssembly());

            builder.RegisterType<UserManager>();
            builder.RegisterType<StudentManager>();
            builder.RegisterType<GroupManager>();

            builder.RegisterType<NHibernateUserDataProvider>()
                .As<IUserDataProvider>();

            builder.RegisterType<NHibernateGroupDataProvider>()
                .As<IGroupDataProvider>();

            builder.RegisterType<NHibernateStudentDataProvider>()
                .As<IStudentDataProvider>();

            builder.RegisterType<UserManager>();

            builder.RegisterType<StudentManager>();

            var container = builder.Build();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}