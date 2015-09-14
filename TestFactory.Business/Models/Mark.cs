using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestFactory.Business.Models
{
    public class Mark : BaseModel
    {
        public virtual Student Students { get; set; }

        public virtual TestDescription Category { get; set; }

        public virtual int Mark { get; set; }
    }
}
