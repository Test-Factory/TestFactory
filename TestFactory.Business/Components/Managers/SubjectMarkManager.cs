using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFactory.Business.Models;
using TestFactory.Business.DataProviderContracts;

namespace TestFactory.Business.Components.Managers
{
    public class SubjectMarkManager : BaseManager<SubjectMark, ISubjectMarkDataProvider>
    {
        public SubjectMarkManager(ISubjectMarkDataProvider provider) : base(provider) { }

        public IList<SubjectMark> GetMarkForSubject(string subjectId)
        {
            return provider.GetMarkForSubject(subjectId);
        }
    }
}
