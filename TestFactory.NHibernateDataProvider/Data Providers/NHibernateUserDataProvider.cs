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
    public class NHibernateUserDataProvider : NHibernateDataProviderBase<Users>, IUsersDataProvider
    {
        public Users GetByEmail(string email)
        {
            return Execute(session =>
            {
                return session
                    .CreateCriteria<Users>()
                    .Add(Restrictions.Eq("Email", email))
                    .UniqueResult<Users>();
            });
        }
    }
}
