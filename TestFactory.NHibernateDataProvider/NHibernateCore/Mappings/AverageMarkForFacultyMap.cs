using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;
using TestFactory.Business.Components.Managers;
using TestFactory.Business.Models;

namespace TestFactory.NHibernateDataProvider.NHibernateCore.Mappings
{
    public class AverageMarkForFacultyMap: ClassMap<AverageMarkForFaculty>
    {
        public AverageMarkForFacultyMap()
        {
            Map(x => x.Faculty);

            Map(x => x.Code);

            Id(x => x.Id);

            Map(x => x.Average);
        }
    }
}
