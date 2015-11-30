using System.Collections.Generic;
using TestFactory.Business.Components.Lucene.Attributes;
using Lucene.Net.Documents;

namespace TestFactory.Business.Models
{
    public class Student : BaseModel
    {
        [FieldName]
        public virtual string FirstName { get; set; }

        [FieldName]
        public virtual string LastName { get; set; }
 
        public virtual string  GroupId { get; set; }

        public virtual IList<Mark> Marks { get; set; }
        public virtual IList<SubjectMark> SubjectMarks { get; set; }

        public virtual int Year { get; set; }

        public Student()
        {
            Marks = new List<Mark>();
            SubjectMarks = new List<SubjectMark>();
        }
    }
}
