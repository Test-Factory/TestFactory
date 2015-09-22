using System.Collections.Generic;
using TestFactory.Business.Models;

namespace TestFactory.Business.DataProviderContracts
{
    public interface IMarkDataProvider : IDataProvider<Mark>
    {
        IList<Mark> GetByStudentId(string studentId);
    }
}
