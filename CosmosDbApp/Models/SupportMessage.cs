using System.ComponentModel.DataAnnotations;

namespace CosmosDbApp.Models
{
    public class SupportMessage
    {
        
        public string Id { get; set; } = string.Empty;

        [Required] [StringLength(100)]
        public string CustomerName { get; set; } = string.Empty; 
        
        [Required]
        public string Email { get; set; } = string.Empty;
        
        [Required] 
        public string PhoneNumber { get; set; } = string.Empty;
        
        [Required]
        [StringLength(100)]
        public string Description { get; set; } = string.Empty;
        
        [Required]
        public SupportCategory Category { get; set; }
        
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        public Status Status { get; set; } = Status.NEW; 
    }

    public enum SupportCategory
    {
        TECHNICAL_SETUP, 
        DIY_PARTS, 
        FEATURE_REQUEST,
        FIND_DEALER,
        REQUEST_CATALOG,
        OTHER
    }

    public enum Status
    {
        NEW,
        IN_PROGRESS,
        RESOLVED,
        CLOSED
    }
    }

