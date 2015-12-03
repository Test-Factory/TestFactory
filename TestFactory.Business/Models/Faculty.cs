using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestFactory.Business.Models
{
    public class Faculty : BaseModel
    {
        public virtual string Name { get; set; }

        public virtual IList<User> Users { get; set; }

        public Faculty()
        {
            Users = new List<User>();
        }
    }
}
