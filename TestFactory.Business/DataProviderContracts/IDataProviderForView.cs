using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestFactory.Business.DataProviderContracts
{
    public interface IDataProviderForView <T>
    {
        IList<T> GetList();

        T GetById(string id);

    }
}
