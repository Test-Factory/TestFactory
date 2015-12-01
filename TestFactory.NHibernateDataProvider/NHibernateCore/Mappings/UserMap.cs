using FluentNHibernate.Mapping;
using TestFactory.Business.Models;

namespace NHibernateDataProviders.NHibernateCore.Mappings
{
    public class UserMap : ClassMap<User>
    {
        public UserMap()
        {
            Id(x => x.Id);

            Map(x => x.Email);

            Map(x => x.Password);

            Map(x => x.PasswordSalt);

            Map(x => x.FirstName);

            Map(x => x.FacultyId);

            Map(x => x.Roles_id);

            References(x => x.Roles).Class<Role>().Not.LazyLoad();         
        }
    }
}
