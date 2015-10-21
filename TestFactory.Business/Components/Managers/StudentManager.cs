using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFactory.Business.DataProviderContracts;
using TestFactory.Business.Models;

namespace TestFactory.Business.Components.Managers
{
    public class StudentManager: BaseManager<Student, IStudentDataProvider>
    {
        public StudentManager(IStudentDataProvider provider) : base(provider) { }

        public IList<Student> GetList(string groupId)
        {
            return provider.GetByGroupId(groupId);
        }
        public void Delete(string id) 
        {
            provider.Delete(id);
        }
    }
}
