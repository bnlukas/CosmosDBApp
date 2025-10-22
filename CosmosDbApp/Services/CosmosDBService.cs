using CosmosDbApp.Models;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Configuration;

namespace CosmosDbApp.Services;

public class CosmosDBService : ICosmosDbService
{
    private readonly Container _container;
    private readonly Database _database;

    public CosmosDbService(IConfiguration configuration)
    {
        var endpoint  = configuration["CosmosDB:Endpoint"];
        var key =  configuration["CosmosDB:Key"];
        var databaseName = configuration["CosmosDB:Database"];
        var containerName = configuration["CosmosDB:Container"];
        
        var client = new CosmosClient(endpoint, key);
        _database = client.GetDatabase(databaseName);
        _container = _database.GetContainer(containerName);
        
    }

    public async Task AddSupportMessageAsync(SupportMessage Message)
    {
        if (string.IsNullOrEmpty(Message.Id))
        {
            Message.Id = Guid.NewGuid().ToString();
        }
        
        Message.CreatedDate = DateTime.UtcNow;

        await _container.UpsertItemAsync(
            item: Message,
            partitionKey: new PartitionKey(Message.Id));
    }

    public async Task<List<SupportMessage>> GetSupportMessagesAsync()
    {
        var query = "SELECT * FROM c ORDER BY c.CreatedDate DESC";
            
        var queryDefinition = new QueryDefinition(query);
        using FeedIterator<SupportMessage> feed = _container.GetItemQueryIterator<SupportMessage>(queryDefinition);

        List<SupportMessage> messages = new();
            
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

    public async Task<SupportMessage?> GetSupportMessageByIdAsync(string Id)
    {
        try
        {
            ItemResponse<SupportMessage> response
                = await _container.ReadItemAsync<SupportMessage>
                (id: Id,
                    partitionKey: new PartitionKey(Id));
            return response.Resource;
        }
        catch (CosmosException ex)
            when
                (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            return null;
        }
    }
    
}