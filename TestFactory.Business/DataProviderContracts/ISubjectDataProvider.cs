using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFactory.Business.Models;

namespace TestFactory.Business.DataProviderContracts
{
    public interface ISubjectDataProvider    : IDataProvider<Subject>
    {
        IList<Subject> GetForFaculty(string faculty);
        Subject GetByShortName(string shortName);
        Subject GetByLongName(string longName);
    }
}
