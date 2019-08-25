using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;

namespace DynamoDBApi.Models
{
    [DynamoDBTable("WidgetStates")]
    public class WidgetState
    {
        [DynamoDBHashKey]
        public int UserId { get; private set; }
        [DynamoDBRangeKey]
        public int OrganizationId { get; private set; }
        [DynamoDBProperty]
        public string WidgetName { get; private set; }
        [DynamoDBProperty("States", typeof(StatesConverter))]
        public ImmutableDictionary<string, DynamoDBEntry> States { get; private set; }


        public WidgetState()
        {
        }

        public WidgetState(int userId, int organizationId, string widgetName)
        {
            UserId = userId;
            OrganizationId = organizationId;
            WidgetName = widgetName;
        }
    }
    
    public class StatesConverter : IPropertyConverter
    {
        public DynamoDBEntry ToEntry(object value)
        {
            throw new System.NotImplementedException();
        }

        public object FromEntry(DynamoDBEntry entry)
        {
            var document = entry.AsDocument();
            return document.ToImmutableDictionary();
        }
    }
}