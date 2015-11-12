using Lucene.Net.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFactory.Business.Components.Lucene.Attributes;
using System.ComponentModel;
using System.Reflection;

namespace TestFactory.Business.Components.Lucene
{
    public class ExtractorStandardBehaviours<T>
        where T : new()
    {
        public static T ToTValue(Document doc)
        {
            T res = new T();
            var properties = typeof(T).GetProperties();
            foreach (PropertyInfo property in properties)
            {
                var attr = property.GetCustomAttribute<Storable>();
                if (attr == null)
                    continue;
                string fieldName = property.Name;
                var val = TypeDescriptor.GetConverter(property.PropertyType)
                            .ConvertFrom(doc.Get(fieldName));
                property.SetValue(res, val);
            }
            return res;
        }

        public static Document ToDoc(T TValue)
        {
            var doc = new Document();
            var properties = typeof(T).GetProperties();
            var concatName = ConcatName(properties, TValue);
            doc.Add(new Field("Name", concatName.ToString(), Field.Store.YES, Field.Index.ANALYZED));

            foreach (PropertyInfo property in properties)
            {
                var attr = property.GetCustomAttribute<Storable>();
                if (attr == null)
                    continue;
                string fieldName = property.Name;
                object fieldValue = property.GetValue(TValue);

                doc.Add(new Field(fieldName, fieldValue.ToString(), Field.Store.YES, attr.Type));
            }
            return doc;
        }

        private static string ConcatName(PropertyInfo[] properties, T TValue)
        {
            string name = "";
            foreach (PropertyInfo property in properties)
            {
                var attrName = property.GetCustomAttribute<FieldName>();
                if (attrName == null)
                    continue;
                object stringValue = property.GetValue(TValue);
                name += stringValue + " ";
            }
            return name;
        }
    }
}
