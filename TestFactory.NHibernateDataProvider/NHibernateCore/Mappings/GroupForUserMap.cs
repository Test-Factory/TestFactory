using FluentNHibernate.Mapping;
using TestFactory.Business.Models;

namespace TestFactory.NHibernateDataProvider.NHibernateCore.Mappings
{
    public class GroupForUserMap : ClassMap<GroupForUser>
    {
        public GroupForUserMap()
        {
            Id(x  => x.Id);

            Map(x => x.GroupId).Not.Nullable();

            Map(x => x.UserId).Not.Nullable();
        }
    }
}
