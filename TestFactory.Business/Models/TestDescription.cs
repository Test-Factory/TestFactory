using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestFactory.Business.Models
{
    public class TestDescription : BaseModel
    {
        public virtual string Category { get; set; }

        public virtual string Code { get; set; }

        public virtual string ShortDescription { get; set; }

        public virtual string LongDescription { get; set; }
    }
}
