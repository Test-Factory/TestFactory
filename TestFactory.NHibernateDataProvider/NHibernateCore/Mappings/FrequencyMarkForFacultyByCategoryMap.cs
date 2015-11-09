using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;
using TestFactory.Business.Models;

namespace TestFactory.NHibernateDataProvider.NHibernateCore.Mappings
{
    public class FrequencyMarkForFacultyByCategoryMap: ClassMap<FrequencyMarkForFacultyByCategory>
     {
        public FrequencyMarkForFacultyByCategoryMap()
        {
            Map(x => x.Faculty);

            Map(x => x.Code);

            Id(x => x.Id);

            Map(x => x.Value);

            Map(x => x.Count);
        }
    }
}
