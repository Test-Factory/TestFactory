using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Linq;
using NHibernateDataProviders.NHibernateCore;

namespace TestFactory.NHibernateDataProvider.DataProviders
{
    public class NHibernateDataProviderBaseForView  <TEntity> where TEntity: class
    {
        private ISession CreateSession()
        {
            return SessionHelper.OpenSession();
        }

        protected T Execute<T>(Func<ISession, T> func)
        {
            using (var session = CreateSession())
            {
                return func(session);
            }
        }

        protected void Execute(Action<ISession> action)
        {
            using (var session = CreateSession())
            {
                try
                {
                    action(session);
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public virtual IList<TEntity> GetList()
        {
            return Execute(session =>
            {
                var listTEntity = session.Query<TEntity>().ToList();
                return listTEntity;
            });
        }

        public virtual TEntity GetById(string id)
        {
            return Execute(session =>
            {
                return session
                    .CreateCriteria<TEntity>()
                    .Add(Restrictions.Eq("Id", id))
                    .UniqueResult<TEntity>();
            });
        }

    }
}
