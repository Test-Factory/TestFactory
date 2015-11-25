using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;
using NHibernate.Mapping;
using TestFactory.Business.Models;

namespace TestFactory.NHibernateDataProvider.NHibernateCore.Mappings
{
    public class SubjectMap  : ClassMap<Subject>
    {
        public SubjectMap()
        {
            Id(x => x.Id);

            Map(x => x.LongName);

            Map(x => x.ShortName);

            Map(x => x.FacultyId);

            HasManyToMany<Group>(x => x.Groups)
                .Table("Subject_Group")
                .ParentKeyColumn("SubjectId")
                .ChildKeyColumn("GroupId")
                .Inverse()
                .Cascade.SaveUpdate()
                .Not.LazyLoad();
        }
    }
}
