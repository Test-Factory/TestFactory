using System.ComponentModel.DataAnnotations;

namespace TestFactory.MVC.ViewModels
{
    public class MarkWebModel : BaseViewModel
    {
        [Required]
        public virtual string StudentId { get; set; }

        [Required]
        public virtual string CategoryId { get; set; }

        [Required]
        [Range(0, 100)]
        public virtual int Value { get; set; }
    }
}
