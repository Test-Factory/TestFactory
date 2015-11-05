using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFactory.Business.DataProviderContracts;

namespace TestFactory.Business.Components.Managers
{
    public class AverageMarkForFacultyManager: BaseManagerForView<AverageMarkForFaculty, IAverageMarkForFacultyDataProvider>
    {
        public AverageMarkForFacultyManager(IAverageMarkForFacultyDataProvider provider) : base(provider) { }
    }
}
