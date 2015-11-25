using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestFactory.Business.Models
{
    public class Subject  : BaseModel
    {
        public virtual string LongName { get; set; }
        public virtual string ShortName { get; set; }

        public virtual string FacultyId { get; set; }

        public virtual IList<Group> Groups { get; set; }
    }
}
