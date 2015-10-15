using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestFactory.Business.Models
{
    public class GroupForUser : BaseModel
    {
        public virtual string GroupId { get; set; }

        public virtual string UserId { get; set; }
    }
}
