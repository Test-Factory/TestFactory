using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestFactory.Business.Models
{
    public class UserRole : BaseModel
    {
        public virtual string RoleName { get; set; }
    }
}
