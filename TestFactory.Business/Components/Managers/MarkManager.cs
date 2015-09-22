using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFactory.Business.DataProviderContracts;
using TestFactory.Business.Models;

namespace TestFactory.Business.Components.Managers
{
    public class MarkManager : BaseManager<Mark, IMarkDataProvider>
    {
        public MarkManager(IMarkDataProvider provider) : base(provider){}

        public IList<Mark> GetList(string studentId)
        {
            return provider.GetByStudentId(studentId);
        }
    }
}
