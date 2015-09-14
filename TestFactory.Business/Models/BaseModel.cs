using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
