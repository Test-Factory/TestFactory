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

        public void AddNewCategory()
        {
            Category ct = new Category();

            ct.Name = "Артистичний";
            ct.Code = "A";
            ct.ACloseTypes = "";
            ct.OppositeType = "";
            ct.Features = "емоційна чутливість; розвинена інтуїція і уява; бажання виділятися з натавпу";
            ct.PreferredAreasOfActivity = "пов'язані з музикою, театром, кіно, літературою, образотворчим мистецтвом. Людей артистичного типу часто називають «творцями». Вони воліють вільні, нерегламентірованниевіди діяльності, які дозволяють їм творити і насолоджуватися результатами своєї творчості. Люди цього типу успішні у вивченні мов, живопису, музиці, театрі та літератури. Вони незалежні і часто потребують простору для самовираження. Люди артистичного типу володіють незвичайним поглядом на життя, глибоким емоційним сприйняттям дійсності, гнучкістю мислення. Вони будують відносини з людьми, спираючись на інтуїцію, емоції і уяву. Більшість людей такого типу воліють роботу з гнучким графіком, не виносять жорсткої регламентації, не орієнтуються на соціальні норми і схвалення громадськості";
            ct.Details = "";
            ct.Likes = "";
            ct.Appreciate = "";

            provider.Create(ct);
        }
    }
}
