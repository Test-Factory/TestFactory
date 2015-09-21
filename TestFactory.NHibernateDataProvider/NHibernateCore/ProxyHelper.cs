using NHibernate;
using NHibernate.Proxy;

namespace NHibernateDataProviders.NHibernateCore
{
    public static class ProxyHelper
    {
        public static T Unproxy<T>(this T obj, ISession session)
        {
            if (!NHibernateUtil.IsInitialized(obj))
            {
                NHibernateUtil.Initialize(obj);
            }

            if (obj is INHibernateProxy)
            {
                return (T)session.GetSessionImplementation().PersistenceContext.Unproxy(obj);
            }
            return obj;
        }
    }
}