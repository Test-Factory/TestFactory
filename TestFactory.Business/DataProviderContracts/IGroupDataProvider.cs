﻿using TestFactory.Business.Models;
using System.Collections.Generic;

namespace TestFactory.Business.DataProviderContracts
{
    public interface IGroupDataProvider : IDataProvider<Group>
    {
        int GetCount(string gropId);

        Group GetByShortName(string shortName);

        IList<Group> GetListForFaculty(string faculty);
    }
}
