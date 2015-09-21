﻿using FluentNHibernate.Mapping;
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

            References( x => x.Group, "GroupId") // you'll need 'Property' in your class definition too
                .Class<Group>().Not.LazyLoad();
           
        }
    }
}
