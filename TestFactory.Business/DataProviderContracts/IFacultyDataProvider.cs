﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFactory.Business.Models;

namespace TestFactory.Business.DataProviderContracts
{
    public interface IFacultyDataProvider : IDataProvider<Faculty>
    {
        IList<Faculty> GetByName(string name);
    }
}
