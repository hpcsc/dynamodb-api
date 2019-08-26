using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;
using Amazon.Runtime;
using DynamoDBApi.Models;
using Newtonsoft.Json;

namespace DynamoDBApi.Repositories
{
    public class WidgetStateRepository
    {
        private readonly string _serviceUrl;
        private readonly string _accessKey;
        private readonly string _secretKey;

        public WidgetStateRepository(string serviceUrl, string accessKey, string secretKey)
        {
            _serviceUrl = serviceUrl;
            _accessKey = accessKey;
            _secretKey = secretKey;
        }

        public async Task<IEnumerable<WidgetState>> FindBy(int organizationId, int userId)
        {
            using (var context = CreateDynamoDbClient())
            {
                var queryRequest = new QueryRequest
                {
                    TableName = "WidgetStates",
                    KeyConditionExpression = "OrganizationId = :organizationId AND begins_with(SortKey, :sortKey)",
                    ExpressionAttributeValues = new Dictionary<string, AttributeValue>
                    {
                        {":organizationId", new AttributeValue {N = organizationId.ToString()}},
                        {":sortKey", new AttributeValue {S = userId.ToString()}}
                    }
                };
                
                var response = await context.QueryAsync(queryRequest);
                return response.Items.Select(ToWidgetState);
            }
        }
        
        public async Task<IEnumerable<WidgetState>> FindBy(int organizationId)
        {
            using (var context = CreateDynamoDbClient())
            {
                var queryRequest = new QueryRequest
                {
                    TableName = "WidgetStates",
                    KeyConditionExpression = "OrganizationId = :organizationId AND begins_with(SortKey, :sortKey)",
                    ExpressionAttributeValues = new Dictionary<string, AttributeValue>
                    {
                        {":organizationId", new AttributeValue {N = organizationId.ToString()}},
                        {":sortKey", new AttributeValue {S = "-"}} // limit to only records without userId and therefore sortKey starting with -{widgetName}
                    }
                };
                
                var response = await context.QueryAsync(queryRequest);
                return response.Items.Select(ToWidgetState);
            }
        }

        private WidgetState ToWidgetState(Dictionary<string, AttributeValue> item)
        {
            return new WidgetState(
                int.Parse(item["OrganizationId"].N),
                item.ContainsKey("UserId") ? int.Parse(item["UserId"].N) : (int?)null,
                item["WidgetName"].S,
                JsonConvert.DeserializeObject(item["States"].S)
            );
        }
        
        public async Task Update(int organizationId, int? userId, string widgetName, dynamic states)
        {
            using (var context = new DynamoDBContext(CreateDynamoDbClient()))
            {
                await context.SaveAsync(new WidgetState(organizationId, userId, widgetName, states));
            }
        }
        
        private AmazonDynamoDBClient CreateDynamoDbClient()
        {
            var config = new AmazonDynamoDBConfig
            {
                ServiceURL = _serviceUrl
            };

            return new AmazonDynamoDBClient(new BasicAWSCredentials(_accessKey, _secretKey), config);
        }
    }

}