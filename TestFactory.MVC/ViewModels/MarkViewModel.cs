using System.ComponentModel.DataAnnotations;

namespace TestFactory.MVC.ViewModels
{
    public class MarkViewModel : BaseViewModel
    {
        [Required]
        [ScaffoldColumn(false)]
        public virtual string StudentId { get; set; }

        [Required]
        [ScaffoldColumn(false)]
        public virtual string CategoryId { get; set; }

        [Required]
        [Range(0, 100)]
        public virtual int Value { get; set; }
    }
}
