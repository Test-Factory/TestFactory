using System;
using TestFactory.Business.Components.Lucene.Attributes;
using Lucene.Net.Documents;

namespace TestFactory.Business.Models
{
    public class BaseModel
    {
        [Storable(Type = Field.Index.NOT_ANALYZED)]
        public virtual string Id { get; set; }

        public BaseModel()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}
