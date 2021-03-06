﻿using TestFactory.Business.DataProviderContracts;
using NHibernate.Criterion;
using TestFactory.Business.Models;
using System.Collections.Generic;

namespace TestFactory.NHibernateDataProvider.DataProviders
{
    public class NHibernateGroupDataProvider : NHibernateDataProviderBase<Group>, IGroupDataProvider
    {
        public int GetCount(string gropId)
        {
            return Execute(session =>
            {
                var criteria = session
                    .CreateCriteria(typeof(Student))
                    .Add(Restrictions.Eq("GroupId", gropId))
                    .SetProjection(Projections.CountDistinct("Id"));
                return (int)criteria.UniqueResult();
            });
        }

        public Group GetByShortName(string shortName)
        {
            return Execute(session =>
            {
                return session
                    .CreateCriteria<Group>()
                    .Add(Restrictions.Eq("ShortName", shortName))
                    .UniqueResult<Group>();
            });
        }

        public IList<Group> GetListForFaculty(string facultyId)
        {
            return Execute(session =>
            {
                return session
                    .CreateCriteria(typeof(Group))
                    .Add(Restrictions.Eq("FacultyId", facultyId))
                    .List<Group>();
            });
        }
    }
}
