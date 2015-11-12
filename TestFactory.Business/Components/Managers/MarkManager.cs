using System.Collections.Generic;
using System.Linq;
using TestFactory.Business.DataProviderContracts;
using TestFactory.Business.Models;

namespace TestFactory.Business.Components.Managers
{
    public class MarkManager : BaseManager<Mark, IMarkDataProvider>
    {
        public MarkManager(IMarkDataProvider provider) : base(provider){}

        public IList<Mark> Get(string studentId)
        {
            return provider.GetByStudentId(studentId);
        }

        public int CountMarksForCategory(string categoryId)
        {
            return provider.CountMarksForCategory(categoryId);
        }

        public IList<Mark> GetMarksByCategoryId(string categoryId)
        {
            return provider.GetMarksByCategoryId(categoryId);
        }

        public void DeleteByStudentId(string studentId)
        {
            IList<Mark> studentMarks = provider.GetByStudentId(studentId);
            foreach (var el in studentMarks)
            {
                provider.Delete(el.Id);
            }
        }

        public double AveregeMarkByCategoryId(string categoryId)
        {
            var marksByCategoryId = this.GetMarksByCategoryId(categoryId);
            var avaregeMark = marksByCategoryId.Average(m => m.Value);
            return avaregeMark;
        }
    }
}
