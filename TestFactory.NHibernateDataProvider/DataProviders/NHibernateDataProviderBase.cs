using System;
using System.Collections.Generic;
using System.Linq;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Linq;
using NHibernateDataProviders.NHibernateCore;
using TestFactory.Business.Models;

namespace TestFactory.NHibernateDataProvider.DataProviders
{
    public class NHibernateDataProviderBase<TEntity>    where TEntity: BaseModel
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
                    action(session);
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

        public virtual void Create(TEntity model)
        {
            Execute(session =>
            {
                using (var transaction = session.BeginTransaction())
                {
                    session.Save(model);
                    transaction.Commit();
                }
            });
        }

        public virtual void Update(TEntity model)
        {
            Execute(session =>
            {
                using (var transaction = session.BeginTransaction())
                {
                    session.Update(model);
                    transaction.Commit();
                }
            });
        }
        public virtual void Delete(string id)
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
