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
        public void DeleteByStudentId(string studentId) 
        {
            IList<Mark> studentMarks = provider.GetByStudentId(studentId);
            foreach (var el in studentMarks)
            {
                provider.Delete(el.Id);
            }
        }
    }
}
