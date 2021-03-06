﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestFactory.Business.Models
{
    public class StudentWithGroup
    {
        public virtual string Id { get; set; }

        public virtual string FirstName { get; set; }

        public virtual string LastName { get; set; }

        public virtual string ShortName { get; set; }

        public virtual string GroupId { get; set; }

        public virtual int Year { get; set; }

        public virtual IList<Mark> Marks { get; set; }
        public virtual IList<SubjectMark> SubjectMarks { get; set; }

        public StudentWithGroup()
        {
            Marks = new List<Mark>();
            SubjectMarks = new List<SubjectMark>();
        }
    }
}
