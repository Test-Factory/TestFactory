using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFactory.Business.Components.Managers;
using TestFactory.Business.Models;

namespace TestFactory.Business.DataProviderContracts
{
    public interface IAverageMarkForFacultyDataProvider: IDataProviderForView<AverageMarkForFaculty>
    {
        IList<AverageMarkForFaculty> GetMarksForFaculty(string id);
    }
}
