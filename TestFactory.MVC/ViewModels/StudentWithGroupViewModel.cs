﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestFactory.MVC.ViewModels
{
    public class StudentWithGroupViewModel
    {
        public virtual string Id { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual string ShortName { get; set; }
        public virtual string GroupId { get; set; }
    }
}
