using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using TestFactory.Business.DataProviderContracts;
using TestFactory.Business.Components.Managers;
using TestFactory.NHibernateDataProvider.DataProviders;
using TestFactory.Business.Components.Rols;

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

            builder.RegisterType<MarkManager>();

            builder.RegisterType<RolsProvider>();

            builder.RegisterType<TestDescriptionManager>();

            builder.RegisterType<NHibernateRoleDataProvider>()
                .As<IRoleDataProvider>();

            builder.RegisterType<NHibernateUserDataProvider>()
                .As<IUserDataProvider>();

            builder.RegisterType<NHibernateGroupDataProvider>()
                .As<IGroupDataProvider>();

            builder.RegisterType<NHibernateStudentDataProvider>()
                .As<IStudentDataProvider>();

            builder.RegisterType<NHibernateMarkDataProvider>()
                .As<IMarkDataProvider>();

            builder.RegisterType<NHibernateTestDescriptionDataProvider>()
                .As<ITestDescriptionDataProvider>();

            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            var container = builder.Build();
            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}