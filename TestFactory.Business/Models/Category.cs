using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestFactory.Business.Models
{
    public class Category : BaseModel
    {
        public virtual string Name { get; set; }

        public virtual string Code { get; set; }

        public virtual string ShortDescription { get; set; }

        public virtual string LongDescription { get; set; }

        //public virtual string Specialization { get; set; }
    }
}
