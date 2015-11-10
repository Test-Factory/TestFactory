using FluentNHibernate.Mapping;
using TestFactory.Business.Models;

namespace NHibernateDataProviders.NHibernateCore.Mappings
{
    public class GroupMap : ClassMap<Group>
    {
        public GroupMap()
        {
            Id(x => x.Id);

            Map(x => x.FullName);

            Map(x => x.ShortName);

            Map(x => x.Faculty);

            Map(x => x.Year);
        }
    }
}
