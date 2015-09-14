using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestFactory.Business.Models
{
    public class Student : BaseModel
    {
        public virtual string FirstName { get; set; }

        public virtual string LastName { get; set; }

        public virtual Group Group { get; set; }
    }
}
