using CosmosDbApp.Models;
namespace CosmosDbApp.Services;

public interface ICosmosDbService
{
    Task AddSupportMessageAsync(SupportMessage message);
    Task<List<SupportMessage>> GetSupportMessagesAsync();
    Task<SupportMessage?> GetSupportMessageByIdAsync(string id);
}