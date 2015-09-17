using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestFactory.MVC.ViewModels
{
    public class BaseViewModel
    {
        public BaseViewModel()
        {
            Id = Guid.NewGuid().ToString();
        }

        [ScaffoldColumn(false)]
        public virtual string Id { get; set; }
    }
}
