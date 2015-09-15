using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestFactory.Business.Components.Managers
{
    public abstract class BaseManager<T, TProvider>
    {
        protected TProvider provider;

        public BaseManager(TProvider provider)
        {
            this.provider = provider;
        }

    }
}
