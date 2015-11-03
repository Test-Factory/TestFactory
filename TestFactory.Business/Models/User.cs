using System.Collections.Generic;

namespace TestFactory.Business.Models
{
    public class User : BaseModel
    {
        public virtual string Email { get; set; }

        public virtual string Password { get; set; }

        public virtual string PasswordSalt { get; set; }

        public virtual string FirstName { get; set; }

        public virtual string LastName { get; set; }

        public virtual string Faculty { get; set; }

        public virtual IList<Group> AdminGroup { get; set; }

        public virtual Role Roles{ get; set; }

        public User()
        {
            AdminGroup = new List<Group>();

          
        }
    }
}
