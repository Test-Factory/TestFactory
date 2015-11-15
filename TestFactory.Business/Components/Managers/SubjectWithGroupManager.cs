using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFactory.Business.DataProviderContracts;
using TestFactory.Business.Models;

namespace TestFactory.Business.Components.Managers
{
    public class SubjectWithGroupManager : BaseManagerForView<SubjectWithGroup, ISubjectWithGroupDataProvider>
    {
        public SubjectWithGroupManager(ISubjectWithGroupDataProvider provider) : base(provider) { }
    }
}
