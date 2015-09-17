using NHibernate.Criterion;
using TestFactory.Business.DataProviderContracts;
using TestFactory.Business.Models;

namespace TestFactory.NHibernateDataProvider.DataProviders
{
    public class NHibernateUserDataProvider : NHibernateDataProviderBase<User>, IUserDataProvider
    {
        public User GetByEmail(string email)
        {
            return Execute(session =>
            {
                return session
                    .CreateCriteria<User>()
                    .Add(Restrictions.Eq("Email", email))
                    .UniqueResult<User>();
            });
        }
    }
}
