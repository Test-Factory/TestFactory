using TestFactory.Business.Models;
using NHibernate;
using NHibernateDataProviders.NHibernateCore;
using TestFactory.Business.DataProviderContracts;

namespace TestFactory.NHibernateDataProvider.DataProviders
{
    public class NHibernateRoleDataProvider :  NHibernateDataProviderBase<Role>, IRoleDataProvider
    {
        public Role GetRole(string rolename)
        {
            using (ISession session = SessionHelper.OpenSession())
            {
                return session.CreateCriteria(typeof(Role))
                    .Add(NHibernate.Criterion.Restrictions.Eq("Name", rolename))
                    .UniqueResult<Role>();
            }
        }

        public bool IsUserInRole(string username, string rolename)
        {
            using (ISession session = SessionHelper.OpenSession())
            {
                var usr = session.CreateCriteria(typeof(User))
                                .Add(NHibernate.Criterion.Restrictions.Eq("Roles", username))
                                .UniqueResult<User>();

                    if (usr.Roles.Name.Equals(rolename))
                    {
                        return true;
                    }

                return false;
            }
        }
    }
}
