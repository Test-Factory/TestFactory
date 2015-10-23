using System.Collections.Generic;

namespace TestFactory.Business.Models
{
    public class Student : BaseModel
    {
        public virtual string FirstName { get; set; }

        public virtual string LastName { get; set; }
 
        public virtual string  GroupId { get; set; }

        public virtual IList<Mark> Marks { get; set; }

        public Student()
        {
            Marks = new List<Mark>();
        }
    }
}
