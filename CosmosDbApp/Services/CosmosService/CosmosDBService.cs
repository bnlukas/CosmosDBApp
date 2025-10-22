using CosmosDbApp.Models; 
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Configuration;

namespace CosmosDbApp.Services
{
    public class CosmosDbService : ICosmosDbService
    {
        private readonly Container _container;

        public CosmosDbService(IConfiguration configuration)
        {
            // Hent konfiguration fra appsettings.json
            var endpoint = configuration["CosmosDb:Endpoint"];
            var key = configuration["CosmosDb:Key"];
            var databaseName = configuration["CosmosDb:DatabaseName"];
            var containerName = configuration["CosmosDb:ContainerName"];
            
            // Opret CosmosClient - præcis som i Microsofts artikel
            var client = new CosmosClient(endpoint, key);
            
            // Hent database og container - præcis som i Microsofts artikel
            var database = client.GetDatabase(databaseName);
            _container = database.GetContainer(containerName);
        }

        public async Task AddSupportMessageAsync(SupportMessage message)
        {
            if (string.IsNullOrEmpty(message.Id))
            {
                message.Id = Guid.NewGuid().ToString();
            }
            
            message.CreatedDate = DateTime.UtcNow;

            // Brug UpsertItemAsync - præcis som i Microsofts artikel
            await _container.UpsertItemAsync(
                item: message,
                partitionKey: new PartitionKey(message.Id)
            );
        }

        public async Task<List<SupportMessage>> GetSupportMessagesAsync()
        {
            var query = "SELECT * FROM c ORDER BY c.CreatedDate DESC";
            
            // Brug GetItemQueryIterator - præcis som i Microsofts artikel  
            var queryDefinition = new QueryDefinition(query);
            using FeedIterator<SupportMessage> feed = _container.GetItemQueryIterator<SupportMessage>(queryDefinition);

            List<SupportMessage> messages = new();
            
            // Loop gennem resultater - præcis som i Microsofts artikel
            while (feed.HasMoreResults)
            {
                FeedResponse<SupportMessage> response = await feed.ReadNextAsync();
                foreach (SupportMessage message in response)
                {
                    messages.Add(message);
                }
            }

            return messages;
        }

        public async Task<SupportMessage?> GetSupportMessageByIdAsync(string id)
        {
            try
            {
                // Brug ReadItemAsync - præcis som i Microsofts artikel
                ItemResponse<SupportMessage> response = await _container.ReadItemAsync<SupportMessage>(
                    id: id,
                    partitionKey: new PartitionKey(id)
                );
                return response.Resource;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
        }
    }
}