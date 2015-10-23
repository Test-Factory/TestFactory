using System.ComponentModel.DataAnnotations;

namespace TestFactory.MVC.ViewModels
{
    public class BaseViewModel
    {
        [ScaffoldColumn(false)]
        public virtual string Id { get; set; }
    }
}
