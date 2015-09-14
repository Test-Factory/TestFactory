using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestFactory.Business.Models
{
    public class Users : BaseModel
    {
        public virtual string Email { get; set; }

        public virtual string Password { get; set; }

        public virtual string PasswordSalt { get; set; }

        public virtual string FirstName { get; set; }

        public virtual string LastName { get; set; }

        public Users()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}
