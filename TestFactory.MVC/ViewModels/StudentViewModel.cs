using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TestFactory.Business.Models;

namespace TestFactory.MVC.ViewModels
{
    public class StudentViewModel : BaseViewModel
    {
        [Required]
        [DataType(DataType.Text)]
        [StringLength(255)]
        public virtual string FirstName { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [StringLength(255)]
        public virtual string LastName { get; set; }
        [Required]
        public virtual string GroupId { get; set; }

        public virtual IList<Mark> Marks { get; set; }
    }
}
