using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFactory.Business.DataProviderContracts;
using TestFactory.Business.Models;

namespace TestFactory.Business.Components.Managers
{
    public class SubjectManager   : BaseManager<Subject, ISubjectDataProvider>
    {
        public SubjectManager(ISubjectDataProvider provider) : base(provider) { }

        public IList<Subject> GetForFaculty(string facultyId)
        {
            return provider.GetForFaculty(facultyId);
        }
        public bool SubjectIsAlreadyExist(string shortName, string longName)
        {
            var shortNameSubject = provider.GetByShortName(shortName) != null? true : false;
            var longNameSubject = provider.GetByLongName(longName) != null? true : false;
            return (shortNameSubject && longNameSubject);
        }
    }
}
