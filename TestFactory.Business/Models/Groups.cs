using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestFactory.Business.Models
{
    public class Groups : BaseModel
    {
        public virtual string FullName { get; set; }

        public virtual string ShortName { get; set; }

        public virtual IList<Students> Students { get; set; }

        public Groups()
        {
            Students = new List<Students>();
        }
    }
}
