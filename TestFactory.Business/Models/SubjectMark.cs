using System.ComponentModel.DataAnnotations;

namespace TestFactory.Business.Models
{
    public class SubjectMark : BaseModel
    {
        public virtual string SubjectId { get; set; }

        public virtual string StudentId { get; set; }

        [Required]
        public virtual string Value { get; set; }
    }
}
