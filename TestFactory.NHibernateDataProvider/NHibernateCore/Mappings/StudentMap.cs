using FluentNHibernate.Mapping;
using NHibernate.Mapping;
using TestFactory.Business.Models;
using ForeignKey = FluentNHibernate.Conventions.Helpers.ForeignKey;

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

            HasMany(x => x.Marks).KeyColumn("StudentId");
        }
    }
}
