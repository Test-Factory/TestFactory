using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestFactory.Business.DataProviderContracts
{
    public interface IDataProvider <T>
    {
        IList<T> GetList();

        T GetById(string id);

        void Create(T model);

        void Update(T model);

        void Delete(string id);

    }
}
