/*using CosmosDbApp.Models;

namespace CosmosDbApp.Services
{
    public class MockCosmosDbService : ICosmosDbService
    {
        private List<SupportMessage> _supportMessages = new();
        
        public MockCosmosDbService()
        {
            // Tilføj noget mock data ved opstart
            _supportMessages.Add(new SupportMessage 
            { 
                Id = "1",
                CustomerName = "Anders Hansen",
                Email = "anders@example.com",
                PhoneNumber = "12345678",
                Description = "Jeg har brug for hjælp til at samle min nye IBAS cykel",
                Category = SupportCategory.TECHNICAL_SETUP,
                CreatedDate = DateTime.UtcNow.AddDays(-2)
            });
            
            _supportMessages.Add(new SupportMessage 
            { 
                Id = "2",
                CustomerName = "Mette Petersen",
                Email = "mette@example.com", 
                PhoneNumber = "87654321",
                Description = "Jeg leder efter reservedele til min gamle IBAS cykel",
                Category = SupportCategory.DIY_PARTS,
                CreatedDate = DateTime.UtcNow.AddDays(-1)
            });
            
            _supportMessages.Add(new SupportMessage 
            { 
                Id = "3",
                CustomerName = "Lars Jensen",
                Email = "lars@example.com",
                Description = "Jeg synes I skal lave en el-cykel version",
                Category = SupportCategory.FEATURE_REQUEST,
                CreatedDate = DateTime.UtcNow.AddHours(-5)
            });
        }
        
        public Task AddSupportMessageAsync(SupportMessage message)
        {
            message.Id = Guid.NewGuid().ToString();
            message.CreatedDate = DateTime.UtcNow;
            _supportMessages.Add(message);
            return Task.CompletedTask;
        }

        public Task<List<SupportMessage>> GetSupportMessagesAsync()
        {
            // Returner mock data sorteret efter dato (nyeste først)
            return Task.FromResult(_supportMessages.OrderByDescending(m => m.CreatedDate).ToList());
        }

        public Task<SupportMessage?> GetSupportMessageByIdAsync(string id)
        {
            var message = _supportMessages.FirstOrDefault(m => m.Id == id);
            return Task.FromResult(message);
        }
    }
} */