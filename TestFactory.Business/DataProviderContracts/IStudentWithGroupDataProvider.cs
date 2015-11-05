﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFactory.Business.Models;

namespace TestFactory.Business.DataProviderContracts
{
    public interface IStudentWithGroupDataProvider: IDataProviderForView<StudentWithGroup>
    {
        IEnumerable<StudentWithGroup> GetByGroupId(string groupId);
    }
}
