using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestFactory.Business.Models
{
     public class FrequencyMarkForFacultyByCategory
    {
        public virtual string Faculty { get; set; }
        public virtual string Code { get; set; }
        public virtual string Id { get; set; }
        public virtual float Count { get; set; }
    }
}
