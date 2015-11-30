using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TestFactory.Components
{
    public class CustomAttribute : ValidationAttribute
    {
        private static string myStudents;

        public CustomAttribute(string students)
        {
            myStudents = students;
        }
        public override bool IsValid(object value)
        {



            return base.IsValid(value);
        }

    }
}