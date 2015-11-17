using System;
using System.ComponentModel.DataAnnotations;

namespace TestFactory.MVC.ViewModels
{
    public class SubjectMarkViewModel : BaseViewModel
    {
        public virtual string SubjectId { get; set; }

        public virtual string StudentId { get; set; }

        [Required]
        public virtual int? Value { get; set; }

        public SubjectMarkViewModel()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}
