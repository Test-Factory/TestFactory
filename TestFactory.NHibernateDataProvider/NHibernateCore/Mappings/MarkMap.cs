using FluentNHibernate.Mapping;
using TestFactory.Business.Models;

namespace TestFactory.NHibernateDataProvider.NHibernateCore.Mappings
{
    public class MarkMap : ClassMap<Mark>
    {
        public MarkMap()
        {
            Id(x => x.Id);

            Map(x => x.StudentId).Not.Nullable();

            Map(x => x.CategoryId).Not.Nullable().Not.LazyLoad();

            Map(x => x.Value);
        }
    }
}
