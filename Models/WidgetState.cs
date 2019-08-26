using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using Newtonsoft.Json;

namespace DynamoDBApi.Models
{
    [DynamoDBTable("WidgetStates")]
    public class WidgetState
    {
        [DynamoDBHashKey]
        public int OrganizationId { get; private set; }
        [DynamoDBRangeKey]
        public string SortKey { get; private set; }
        public int? UserId { get; private set; }
        public string WidgetName { get; private set; }
        [DynamoDBProperty("States", typeof(StatesJsonConverter))]
        public dynamic States { get; private set; }

        public WidgetState()
        {
        }

        public WidgetState(int organizationId, int? userId, string widgetName, dynamic states)
        {
            UserId = userId;
            OrganizationId = organizationId;
            SortKey = CombineIntoCompositeKey(userId, widgetName);
            WidgetName = widgetName;
            States = states;
        }

        private string CombineIntoCompositeKey(int? userId, string widgetName)
        {
            var userIdValue = userId.HasValue ? userId.Value.ToString() : string.Empty;
            return $"{userIdValue}-{widgetName}";
        }
    }
    
}