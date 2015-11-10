using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFactory.Business.DataProviderContracts;
using TestFactory.Business.Models;

namespace TestFactory.Business.Components.Managers
{
    public class StudentWithGroupManager  : BaseManagerForView<StudentWithGroup, IStudentWithGroupDataProvider>
    {
        public StudentWithGroupManager(IStudentWithGroupDataProvider provider) : base(provider) { }

        public IList<StudentWithGroup> GetByGroupId(string groupId)
        {
            return provider.GetByGroupId(groupId);
        }
    }
}
