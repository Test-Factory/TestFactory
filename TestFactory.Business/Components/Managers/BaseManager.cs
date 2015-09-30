using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using TestFactory.Business.DataProviderContracts;
using TestFactory.Business.Models;

namespace TestFactory.Business.Components.Managers
{
    public abstract class BaseManager<T, TProvider>
        where T: BaseModel
        where TProvider: IDataProvider<T>

    {
        protected TProvider provider;

        protected BaseManager(TProvider provider)
        {
            this.provider = provider;
        }

        public virtual void Create(T model)
        {
            provider.Create(model);
        }

        public virtual IList<T> GetList()
        {
           return provider.GetList();
        }

        public virtual T GetById(string id)
        {
            return provider.GetById(id);
        }

        public virtual void Update(T model)
        {
            provider.Update(model);
        }
    }
}
