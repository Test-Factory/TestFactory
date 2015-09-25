using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFactory.Business.DataProviderContracts;
using TestFactory.Business.Models;

namespace TestFactory.Business.Components.Managers
{
    public class CategoryManager : BaseManager<Category, ICategoryDataProvider>
    {
        public CategoryManager(ICategoryDataProvider provider) : base(provider) { }

        public IList<Category> GetList()
        {
            return provider.GetList();
        } 
    }
}
