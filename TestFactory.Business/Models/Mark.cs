using System.ComponentModel.DataAnnotations;

namespace TestFactory.Business.Models
{
    public class Mark : BaseModel
    {
        public virtual string StudentId { get; set; }

        public virtual string CategoryId { get; set; }

        [Required]
        public virtual int Value { get; set; }
    }
}
