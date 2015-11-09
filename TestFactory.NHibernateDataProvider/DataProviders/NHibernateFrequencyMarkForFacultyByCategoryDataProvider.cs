﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate.Linq;
using TestFactory.Business.DataProviderContracts;
using TestFactory.Business.Models;

namespace TestFactory.NHibernateDataProvider.DataProviders
{
    public class NHibernateFrequencyMarkForFacultyByCategoryDataProvider: NHibernateDataProviderBaseForView<FrequencyMarkForFacultyByCategory>, IFrequencyMarkForFacultyByCategoryDataProvider
    {
        public IEnumerable<FrequencyMarkForFacultyByCategory> GetMarksForFaculty(string faculty)
        {
            return Execute(session =>
            {
                IEnumerable<FrequencyMarkForFacultyByCategory> frequencyMarks = session
                                     .Query<FrequencyMarkForFacultyByCategory>()
                                     .Where(f => f.Faculty == faculty)
                                     .ToList<FrequencyMarkForFacultyByCategory>();
                return frequencyMarks;
            });
            return null;
        }
    }
}
