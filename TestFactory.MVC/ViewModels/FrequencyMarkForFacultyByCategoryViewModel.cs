using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestFactory.MVC.ViewModels
{
    public class FrequencyMarkForFacultyByCategoryViewModel
    {
        [ScaffoldColumn(false)]
        public virtual string FacultyId { get; set; }

        public virtual string Code { get; set; }

        [ScaffoldColumn(false)]
        public virtual string CategoryId { get; set; }

        public virtual int Value { get; set; }

        public virtual int Count { get; set; }
    }
}
