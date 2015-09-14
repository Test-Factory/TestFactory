using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestFactory.Business.Data_Provider_Contracts
{
    public interface IStudentsDataProvider
    {
        void Delete(string id);

        void Create();
    }
}
