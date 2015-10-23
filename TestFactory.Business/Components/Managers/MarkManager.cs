using System.Collections.Generic;
using TestFactory.Business.DataProviderContracts;
using TestFactory.Business.Models;

namespace TestFactory.Business.Components.Managers
{
    public class MarkManager : BaseManager<Mark, IMarkDataProvider>
    {
        public MarkManager(IMarkDataProvider provider) : base(provider){}

        public IList<Mark> Get(string studentId)
        {
            return provider.GetByStudentId(studentId);
        }
    }
}
