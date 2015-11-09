using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestFactory.MVC.ViewModels
{
    public class FrequencyMarkForFacultyByCategoryViewModel
    {
        public virtual string Faculty { get; set; }
        public virtual string Code { get; set; }
        public virtual string CategoryId { get; set; }
        public virtual int Value { get; set; }
        public virtual int Count { get; set; }
    }
}
