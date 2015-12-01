using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFactory.Business.Models;

namespace TestFactory.NHibernateDataProvider.NHibernateCore.Mappings
{
    public class FacultyMap : ClassMap<Faculty>
    {
        public FacultyMap()
        {
            Table("Faculty");

            Id(x => x.Id);

            Map(x => x.Name);

            HasMany(x => x.Users).KeyColumn("FacultyId").Cascade.All().Not.LazyLoad().Inverse();
        }
    }
}
