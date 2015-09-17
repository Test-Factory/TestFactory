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
        public void Create(T model)
        {
            provider.Create(model);
        }

        public IList<T> GetList()
        {
           return provider.GetList();
           
        }
        public T GetById(string id)
        {
            return provider.GetById(id);

        }

        public void Update(T model)
        {
            provider.Update(model);
        }

        public void Delete(string id)
        {
            provider.Delete(id);
        }

        public T GetById(string id)
        {
            return provider.GetById(id);
        }
    }
}
