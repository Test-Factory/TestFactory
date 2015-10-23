
namespace TestFactory.Business.Models
{
    public class GroupForUser : BaseModel
    {
        public virtual string GroupId { get; set; }

        public virtual string UserId { get; set; }
    }
}
