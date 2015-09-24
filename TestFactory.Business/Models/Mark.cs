using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestFactory.Business.Models
{
    public class Mark : BaseModel
    {
        public virtual string StudentId { get; set; }

        //TODO:  public virtual string CategoryId { get; set; }

        //public virtual TestDescription Category { get; set; }

        public virtual int Value { get; set; }
    }
}
