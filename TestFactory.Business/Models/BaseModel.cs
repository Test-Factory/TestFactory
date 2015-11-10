using System;
using TestFactory.Business.Components.Lucene.Attributes;
using Lucene.Net.Documents;
using System.Web.Mvc;

namespace TestFactory.Business.Models
{
    public class BaseModel
    {
        [Storable(Type = Field.Index.NOT_ANALYZED)]
        [HiddenInput(DisplayValue = false)]
        public virtual string Id { get; set; }

        public BaseModel()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}
