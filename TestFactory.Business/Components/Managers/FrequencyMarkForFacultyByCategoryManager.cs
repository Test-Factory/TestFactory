using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFactory.Business.DataProviderContracts;
using TestFactory.Business.Models;

namespace TestFactory.Business.Components.Managers
{
    public class FrequencyMarkForFacultyByCategoryManager :BaseManagerForView<FrequencyMarkForFacultyByCategory, IFrequencyMarkForFacultyByCategoryDataProvider>
    {
        public FrequencyMarkForFacultyByCategoryManager(IFrequencyMarkForFacultyByCategoryDataProvider provider) : base(provider) { }
        public IEnumerable<FrequencyMarkForFacultyByCategory> GetMarksForFaculty(string id)
        {
            return provider.GetMarksForFaculty(id);
        }
   
    }
}
