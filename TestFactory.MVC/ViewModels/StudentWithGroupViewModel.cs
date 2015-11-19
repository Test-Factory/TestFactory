using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFactory.Business.Models;

namespace TestFactory.MVC.ViewModels
{
    public class StudentWithGroupViewModel
    {
        [ScaffoldColumn(false)]
        public virtual string Id { get; set; }

        public virtual string FirstName { get; set; }

        public virtual string LastName { get; set; }

        public virtual string ShortName { get; set; }

        public virtual int Year { get; set; }

        [ScaffoldColumn(false)]
        public virtual string GroupId { get; set; }

        public virtual IList<Mark> Marks { get; set; }
    }
}
