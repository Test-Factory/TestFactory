using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using NHibernate;
using FluentNHibernate;
using NHibernate.Cfg;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate.Proxy;
using NHibernate.Tool.hbm2ddl;

namespace NHibernateDataProviders.NHibernateCore
{
    public class SessionHelper
    {
        private static ISessionFactory sessionFactory;

        public static ISession OpenSession()
        {
            if (sessionFactory == null)
            {
                InitializeSessionFactory();
            }
            return sessionFactory.OpenSession();
        }

        private static void InitializeSessionFactory()
        {
            sessionFactory = Fluently.Configure()                
                .Database(MsSqlConfiguration.MsSql2012
                              .ConnectionString(c => c.FromConnectionStringWithKey("DefaultConnection"))
                              .ShowSql()
                )
                .Mappings(m =>
                          m.FluentMappings
                              .AddFromAssemblyOf<SessionHelper>())
              //  .ExposeConfiguration(cfg => new SchemaExport(cfg)
                                 //               .Create(true, true))
                              .BuildSessionFactory();
        }
    }
}
