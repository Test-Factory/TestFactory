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

            Map(x => x.Name);

            Map(x => x.FacultyId);
        }
    }
}
