using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate;
using FluentNHibernate;
using NHibernate.Cfg;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;

namespace NHibernateDataProviders.NHibernateCore
{
    public class Helper
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
                              .ConnectionString(
                                  @"Server=tcp:ead9qcxrdo.database.windows.net,1433;Database=abitcareerTest;User ID=abitcareer@ead9qcxrdo;Password=ISMabit3;Trusted_Connection=False;Encrypt=True;Connection Timeout=30;")
                              .ShowSql()
                )
                .Mappings(m =>
                          m.FluentMappings
                              .AddFromAssemblyOf<Helper>())
                //.ExposeConfiguration(cfg => new SchemaExport(cfg)
                //                                .Create(true, true))
                              .BuildSessionFactory();
        }
    }
}
