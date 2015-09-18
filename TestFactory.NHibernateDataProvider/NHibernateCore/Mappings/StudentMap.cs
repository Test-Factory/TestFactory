using FluentNHibernate.Mapping;
using TestFactory.Business.Models;

namespace NHibernateDataProviders.NHibernateCore.Mappings
{
    public class StudentMap : ClassMap<Student>
    {
        public StudentMap()
        {
            Id(x => x.Id);

            Map(x => x.FirstName);

            Map(x => x.LastName);
           
            Map(x => x.GroupId);

            //References(x => x.Group).Column("GroupId").Class<Group>();
        }
    }
}
