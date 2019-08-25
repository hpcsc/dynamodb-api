using System.Collections.Generic;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.Runtime;
using DynamoDBApi.Models;

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

        public async Task<WidgetState> FindBy(int userId, int organizationId)
        {
            using (var context = CreateDynamoDbContext())
            {
                return await context.LoadAsync<WidgetState>(userId, organizationId);
            }
        }
        
        private DynamoDBContext CreateDynamoDbContext()
        {
            var config = new AmazonDynamoDBConfig
            {
                ServiceURL = _serviceUrl
            };

            var client = new AmazonDynamoDBClient(new BasicAWSCredentials(_accessKey, _secretKey), config);

            return new DynamoDBContext(client);
        }
    }

}