﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestFactory.Business.Models
{
    public class Mark : BaseModel
    {
        public virtual string StudentId { get; set; }

        public virtual Category Category { get; set; }//testdesc - > category 

        public virtual int Value { get; set; }
    }
}
