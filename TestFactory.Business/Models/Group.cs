using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestFactory.Business.Models
{
    public class Group : BaseModel
    {
        public virtual string FullName { get; set; }

        public virtual string ShortName { get; set; }

        public virtual IList<Student> Students { get; set; }

        public Group()
        {
            Students = new List<Student>();
        }
    }
}
