﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestFactory.Business.Models
{
    public class User : BaseModel
    {
        public virtual string Email { get; set; }

        public virtual string Password { get; set; }

        public virtual string PasswordSalt { get; set; }

        public virtual string FirstName { get; set; }

        public virtual string LastName { get; set; }

        public virtual IList<Group> AdminGroup { get; set; }

        public virtual bool Role { get; set; } 

        public User()
        {
            Id = Guid.NewGuid().ToString();
            AdminGroup = new List<Group>();
        }
    }
}