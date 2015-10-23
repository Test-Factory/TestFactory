using System;

namespace TestFactory.Business.Models
{
    public class BaseModel
    {
        public virtual string Id { get; set; }

        public BaseModel()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}
