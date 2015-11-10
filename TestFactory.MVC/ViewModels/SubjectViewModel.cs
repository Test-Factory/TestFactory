using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestFactory.MVC.ViewModels
{
    public class SubjectViewModel: BaseViewModel
    {
        public virtual string Name { get; set; }
        public virtual string GroupId { get; set; }
    }
}
