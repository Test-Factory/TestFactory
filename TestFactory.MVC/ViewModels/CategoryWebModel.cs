using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestFactory.MVC.ViewModels
{
    public class CategoryWebModel   : BaseViewModel
    {
        public virtual string Name { get; set; }

        public virtual string Code { get; set; }

        public virtual string ShortDescription { get; set; }

        public virtual string LongDescription { get; set; }
    }
}
