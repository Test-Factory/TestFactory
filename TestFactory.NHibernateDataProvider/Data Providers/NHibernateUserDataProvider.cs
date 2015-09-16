using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFactory.Business.Models;
using TestFactory.Business.Data_Provider_Contracts;
using NHibernate.Criterion;

namespace NHibernateDataProviders.Data_Providers
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

        public bool NewUser(User user)
        {
            return true;
        }
    }
}
