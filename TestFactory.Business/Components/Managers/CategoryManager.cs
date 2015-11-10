using TestFactory.Business.DataProviderContracts;
using TestFactory.Business.Models;

namespace TestFactory.Business.Components.Managers
{
    public class CategoryManager : BaseManager<Category, ICategoryDataProvider>
    {
        public CategoryManager(ICategoryDataProvider provider) : base(provider) { }
    }
}
