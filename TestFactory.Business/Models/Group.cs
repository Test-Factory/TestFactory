
using System.Collections.Generic;
namespace TestFactory.Business.Models
{
    public class Group : BaseModel
    {
        public virtual string FullName { get; set; }

        public virtual string ShortName { get; set; }

        public virtual string FacultyId { get; set; }

        public virtual int Year { get; set; }

        public virtual IList<Subject> Subjects {get; set;}
    }
}
