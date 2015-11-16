using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestFactory.MVC.ViewModels
{
    public class SubjectMarkViewModel : BaseViewModel
    {
        public virtual string SubjectId { get; set; }

        public virtual string Value { get; set; }

        public SubjectMarkViewModel()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}
