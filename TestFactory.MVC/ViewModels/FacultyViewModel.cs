using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestFactory.MVC.ViewModels
{
    public class FacultyViewModel : BaseViewModel
    {
        public virtual string Name { get; set; }
        public virtual List<UserViewModel> Users { get; set; }
        public FacultyViewModel()
        {
            Users = new List<UserViewModel>();
        }
    }
}
