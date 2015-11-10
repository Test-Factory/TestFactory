using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        [ScaffoldColumn(false)]
        public virtual int Year { get; set; }

        public virtual IList<Mark> Marks { get; set; }
    }
}
