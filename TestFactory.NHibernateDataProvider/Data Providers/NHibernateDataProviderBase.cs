using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFactory.Business.Models;
using NHibernate;
using System.Linq;

namespace NHibernateDataProviders.Data_Providers
{
    public class NHibernateDataProviderBase<TEntity>
    {
        private ISession CreateSession()
        {
            return Helper.OpenSession();
        }

        protected T Execute<T>(Func<ISession, T> func)
        {
            using (var session = CreateSession())
            {
                return func(session);
            }
        }
    }
}
