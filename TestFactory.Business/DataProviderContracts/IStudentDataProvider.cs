using System.Collections.Generic;
using TestFactory.Business.Models;

namespace TestFactory.Business.DataProviderContracts
{
    public interface IStudentDataProvider : IDataProvider<Student>
    {
        IList<Student> GetByGroupId(string groupId);

        void Delete(string id);
    }
}
