using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFactory.Business.Data_Provider_Contracts;
using TestFactory.Business.Models;

namespace TestFactory.Business.Components.Managers
{
    public class StudentManager: BaseManager<Student, IStudentDataProvider>
    {
        public StudentManager(IStudentDataProvider provider) : base(provider) { }
    }
}
