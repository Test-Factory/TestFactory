using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestFactory.MVC.ViewModels
{
    public class MarkWebModel : BaseViewModel
    {
        public virtual string StudentId { get; set; }

        public virtual string CategoryId { get; set; }

        public virtual int Value { get; set; }
    }
}
