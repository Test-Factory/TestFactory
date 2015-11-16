using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestFactory.Business.Models
{
    public class SubjectMark : BaseModel
    {
        public virtual string SubjectId { get; set; }

        public virtual string Value { get; set; }
    }
}
