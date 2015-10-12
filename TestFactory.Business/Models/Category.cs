using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestFactory.Business.Models
{
    public class Category : BaseModel
    {
        public virtual string Name { get; set; }

        public virtual string Code { get; set; }

        public virtual string ACloseTypes { get; set; }

        public virtual string OppositeType { get; set; }

        public virtual string Features { get; set; }

        public virtual string PreferredAreasOfActivity { get; set; }

        public virtual string Details {get; set;}

        public virtual string Likes { get; set; }

        public virtual string Appreciate { get; set; }
    }
}
