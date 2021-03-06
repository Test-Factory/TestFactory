﻿using System.Reflection;
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

            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            builder.RegisterType<UserManager>();

            builder.RegisterType<StudentManager>();

            builder.RegisterType<StudentWithGroupManager>();

            builder.RegisterType<GroupManager>();

            builder.RegisterType<MarkManager>();

            builder.RegisterType<SubjectManager>();

            builder.RegisterType<SubjectMarkManager>();

            builder.RegisterType<FacultyManager>();

            builder.RegisterType<RoleManager>();

            builder.RegisterType<RolsProvider>();

            builder.RegisterType<CategoryManager>();

            builder.RegisterType<AverageMarkForFacultyManager>();

            builder.RegisterType<FrequencyMarkForFacultyByCategoryManager>();

            builder.RegisterType<NHibernateFacultyDataProvider>()
                .As<IFacultyDataProvider>();

            builder.RegisterType<NHibernateRoleDataProvider>()
                .As<IRoleDataProvider>();

            builder.RegisterType<NHibernateAverageMarkForFacultyDataProvider>()
                .As<IAverageMarkForFacultyDataProvider>();

            builder.RegisterType<NHibernateFrequencyMarkForFacultyByCategoryDataProvider>()
               .As<IFrequencyMarkForFacultyByCategoryDataProvider>();

            builder.RegisterType<NHibernateUserDataProvider>()
                .As<IUserDataProvider>();

            builder.RegisterType<NHibernateGroupDataProvider>()
                .As<IGroupDataProvider>();

            builder.RegisterType<NHibernateStudentWithGroupDataProvider>()
                .As<IStudentWithGroupDataProvider>();

            builder.RegisterType<NHibernateSubjectDataProvider>()
               .As<ISubjectDataProvider>();

            builder.RegisterType<NHibernateSubjectMarkDataProvider>()
               .As<ISubjectMarkDataProvider>();

            builder.RegisterType<NHibernateStudentDataProvider>()
               .As<IStudentDataProvider>();

            builder.RegisterType<NHibernateMarkDataProvider>()
                .As<IMarkDataProvider>();

            builder.RegisterType<NHibernateCategoryDataProvider>()
                .As<ICategoryDataProvider>();
          
            var container = builder.Build();
            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}