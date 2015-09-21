using System;
using System.Collections.Generic;
using System.Linq;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Linq;
using NHibernateDataProviders.NHibernateCore;

namespace TestFactory.NHibernateDataProvider.DataProviders
{
    public class NHibernateDataProviderBase<TEntity>    where TEntity: class 
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
        protected void Execute(Action<ISession> action)
        {
            using (var session = CreateSession())
                {
                    action(session);
                }
        }

        public IList<TEntity> GetList()
        {
            return Execute(session =>
            {
                //var critetria = session.CreateCriteria(typeof (TEntity));
                //var listTEntity = 
                var listTEntity = session.Query<TEntity>().ToList();
                return listTEntity;
            });
        }

        public void Create(TEntity model)
        {
            Execute(session =>
            {
                using (var transaction = session.BeginTransaction())
                {
                    session.SaveOrUpdate(model);
                    transaction.Commit();
                }
            });
        }

        public void Update(TEntity model)
        {
            Execute(session =>
            {
                using (var transaction = session.BeginTransaction())
                {
                    session.SaveOrUpdate(model);
                    session.Flush();
                    transaction.Commit();
                }
            });
        }

        public void Delete(string id)
        {
            Execute(session =>
            {
                using (var transaction = session.BeginTransaction())
                {
                    session.Delete(session.Load(typeof(TEntity), id));
                    transaction.Commit();
                }

            });
        }

        public TEntity GetById(string id)
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
