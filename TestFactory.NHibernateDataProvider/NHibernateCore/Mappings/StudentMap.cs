using FluentNHibernate.Mapping;
using TestFactory.Business.Models;

namespace NHibernateDataProviders.NHibernateCore.Mappings
{
    public class StudentMap : ClassMap<Student>
    {
        public StudentMap()
        {
            Table("Student");

            Id(x => x.Id);

            Map(x => x.FirstName);

            Map(x => x.LastName);

            Map(x => x.GroupId).Not.Nullable();

            Map(x => x.Year);

            HasMany(x => x.Marks).KeyColumn("StudentId").Cascade.All().Not.LazyLoad().Inverse();
        }
    }
}
