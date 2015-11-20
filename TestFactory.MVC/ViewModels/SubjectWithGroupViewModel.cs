using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestFactory.MVC.ViewModels
{
    public class SubjectWithGroupViewModel
    {
        public virtual string Name { get; set; }
        public virtual string Id { get; set; }
        public virtual string GroupId { get; set; }
    }
}
