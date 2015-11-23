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

        public IList<SubjectMark> SetNullProportyForMark(IList<Student> students, IList<SubjectMark> markForSubject, string subjectId)
        {
            if (students.Count != markForSubject.Count)
            {
                foreach (var st in students)
                {
                    bool isToAssess = false;
                    foreach (var mr in markForSubject)
                    {
                        if (st.Id.Equals(mr.StudentId))
                        {
                            isToAssess = true;
                            break;
                        }
                    }
                    if (!isToAssess)
                    {
                        var newMark = new SubjectMark();
                        newMark.StudentId = st.Id;
                        newMark.SubjectId = subjectId;
                        newMark.Value = null;
                        markForSubject.Add(newMark);
                    }
                }
            }
            return markForSubject;
        }

    }
}
