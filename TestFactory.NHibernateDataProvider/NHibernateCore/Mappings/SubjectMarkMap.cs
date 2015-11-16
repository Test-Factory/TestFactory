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
    public class SubjectMarkMap : ClassMap<SubjectMark>
    {
        public SubjectMarkMap()
        {
            Id(x => x.Id);

            Map(x => x.SubjectId).Not.Nullable();

            Map(x => x.StudentId);

            Map(x => x.Value);
        }
    }
}
