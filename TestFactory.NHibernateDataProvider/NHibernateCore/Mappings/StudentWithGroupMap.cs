using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using TestFactory.Business.Models;

namespace TestFactory.NHibernateDataProvider.NHibernateCore.Mappings
{
    public class StudentWithGroupMap   : ClassMap<StudentWithGroup>
    {
        public StudentWithGroupMap()
        {
            Table("StudentWithGroup");
            ReadOnly();
            Id(x => x.Id);
            Map(x => x.FirstName);

            Map(x => x.LastName); 

            Map(x => x.ShortName);

            Map(x => x.GroupId);

            HasMany(x => x.Marks).KeyColumn("StudentId").Cascade.All().Not.LazyLoad().Inverse();
        }
    }
}
