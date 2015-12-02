using System.Collections.Generic;

namespace TestFactory.Business.Models
{
    public class User : BaseModel
    {
        public virtual string Email { get; set; }

        public virtual string Password { get; set; }

        public virtual string PasswordSalt { get; set; }

        public virtual string FacultyId { get; set; }

        public virtual string Roles_id { get; set; }

        public virtual IList<Group> AdminGroup { get; set; }

        public virtual Role Roles{ get; set; }

        public User()
        {
            AdminGroup = new List<Group>();
        }
    }
}
