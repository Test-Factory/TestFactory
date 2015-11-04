using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;
using TestFactory.Business.Components.Managers;

namespace TestFactory.NHibernateDataProvider.NHibernateCore.Mappings
{
    public class AverageMarkForFacultyMap: ClassMap<AverageMarkForFaculty>
    {
        public AverageMarkForFacultyMap()
        {
            Id(x => x.Id);
            Map(x => x.Code);
            Map(x => x.Average);
        }
    }
}
