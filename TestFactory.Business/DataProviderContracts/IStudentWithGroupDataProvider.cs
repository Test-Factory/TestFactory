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
        IList<StudentWithGroup> GetByGroupId(string groupId);
    }
}
