using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using TestFactory.Business.DataProviderContracts;
using TestFactory.Business.Components.Managers;
using TestFactory.NHibernateDataProvider.DataProviders;
using TestFactory.Business.Components.Rols;
using TestFactory.Components;
using TestFactory.Business.Components;

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
            builder.RegisterType<RolsProvider>();

            builder.RegisterType<NHibernateRoleDataProvider>()
                .As<IRoleDataProvider>();

            builder.RegisterType<NHibernateUserDataProvider>()
                .As<IUserDataProvider>();

            builder.RegisterType<NHibernateGroupDataProvider>()
                .As<IGroupDataProvider>();

            builder.RegisterType<NHibernateStudentDataProvider>()
                .As<IStudentDataProvider>();

            builder.RegisterType<UserViewContext>().As<UserContext>();

            var container = builder.Build();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}