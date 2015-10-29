using System.Collections.Generic;
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

        public void DeleteByGroupId(string id)
        {
            var groupForUserList = provider.GetByGroupId(id);
            
            foreach (var el in groupForUserList)
            {
                provider.Delete(el.Id);
            }
        }
    }
}
