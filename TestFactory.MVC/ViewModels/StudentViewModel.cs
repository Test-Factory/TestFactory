using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestFactory.MVC.ViewModels
{
    public class StudentViewModel : BaseViewModel
    {
        [Required]
        [DataType(DataType.Text)]
        public virtual string FirstName { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public virtual string LastName { get; set; }
    }
}
