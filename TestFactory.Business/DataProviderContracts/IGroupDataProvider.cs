﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFactory.Business.Models;

namespace TestFactory.Business.Data_Provider_Contracts
{
    public interface IGroupDataProvider
    {
        void Delete(string name);

        void Create();
    }
}
