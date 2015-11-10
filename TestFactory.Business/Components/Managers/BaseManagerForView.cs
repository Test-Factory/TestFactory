using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFactory.Business.DataProviderContracts;

namespace TestFactory.Business.Components.Managers
{
    public class BaseManagerForView<T, TProvider>
        where T: class
        where TProvider: IDataProviderForView<T>
    {
        protected TProvider provider;

        protected BaseManagerForView(TProvider provider)
        {
            this.provider = provider;
        }

        public virtual IEnumerable<T> GetList()
        {
           return provider.GetList();
        }

        public virtual T GetById(string id)
        {
            return provider.GetById(id);
        }
    }
}
