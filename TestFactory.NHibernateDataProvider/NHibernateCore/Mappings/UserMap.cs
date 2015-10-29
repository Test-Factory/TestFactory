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

            Map(x => x.LastName);

            Map(x => x.Faculty);

            References(x => x.Roles).Class<Role>().Not.LazyLoad();

            HasMany(x => x.ManagerGroup).KeyColumn("UserId").Cascade.All().Not.LazyLoad();
        }
    }
}
