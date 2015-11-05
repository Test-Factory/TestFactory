using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestFactory.Business.DataProviderContracts
{
    public interface IDataProviderForView <T>
    {
        IEnumerable<T> GetList();

        T GetById(string id);

    }
}
