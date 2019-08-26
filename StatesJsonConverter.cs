using System;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using Newtonsoft.Json;

namespace DynamoDBApi
{
    public class StatesJsonConverter : IPropertyConverter
    {
        public object FromEntry(DynamoDBEntry entry)
        {
            var primitive = entry as Primitive;
            if (!(primitive?.Value is string) || string.IsNullOrEmpty((string)primitive.Value))
                throw new ArgumentOutOfRangeException();
            return JsonConvert.DeserializeObject(primitive.Value as string);
        }

        public DynamoDBEntry ToEntry(object value)
        {
            var jsonString = JsonConvert.SerializeObject(value);
            return new Primitive(jsonString);
        }
    }
}