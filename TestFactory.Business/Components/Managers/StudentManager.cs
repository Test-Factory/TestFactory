using System.Collections.Generic;
using TestFactory.Business.DataProviderContracts;
using TestFactory.Business.Models;
using System.IO;
using TestFactory.Business.Components.Lucene;
using System.Globalization;

namespace TestFactory.Business.Components.Managers
{
    public class StudentManager: BaseManager<Student, IStudentDataProvider>
    {
        public StudentManager(IStudentDataProvider provider) : base(provider) { }

        public IList<Student> GetList(string groupId)
        {
            return provider.GetByGroupId(groupId);
        }

        private string luceneDirectory
        {
            get
            {
                return Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "App_Data", CultureInfo.CurrentCulture.ToString(), "lucene_index");
            }
        }
  
        public void AddLuceneIndex(IList<Student> students)
        {
            var searcher = new Searcher<Student>(luceneDirectory);
            searcher.ClearIndex();
            searcher.AddUpdateIndex(students);
        }

        public IList<string> Search(string name)
        {
            var searcher = new Searcher<Student>(luceneDirectory);
            var list = searcher.Search(name, "Name");
            var result = new List<string>();

            foreach (var item in list)
            {
                if (!result.Contains(item.Id))
                {
                    result.Add(item.Id);
                }
            }
            return result;
        }
    }
}
