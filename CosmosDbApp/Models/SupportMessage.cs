using System.ComponentModel.DataAnnotations;

namespace CosmosDbApp.Models
{
    public class SupportMessage
    {
        
        public string Id { get; set; } = string.Empty;

        [Required (ErrorMessage = "Dit navn, thanks")] 
        [StringLength(100, ErrorMessage = "Alt for langt, unfortunately")]
        public string CustomerName { get; set; } = string.Empty; 
        
        [Required(ErrorMessage = "Din mail, thanks")]
        [EmailAddress(ErrorMessage = "Vil du være sød at indtaske in vaild email?")]
        public string Email { get; set; } = string.Empty;
        
        [Required]
        [Phone(ErrorMessage = "Please indtast et gyldigt telefonnummer")]
        public string PhoneNumber { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "Please skriv en beskrivelse")]
        [StringLength(1000, ErrorMessage = "To long, unfortunately")]
        public string Description { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "Please enter a support category")]
        public SupportCategory Category { get; set; }
        
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        public Status Status { get; set; } = Status.NEW; 
    }

    public enum SupportCategory
    {
        [Display(Name =" Teknisk opsætning")]
        TECHNICAL_SETUP, 
        [Display(Name ="Gør det selv reservedele")]
        DIY_PARTS, 
        [Display(Name ="Forslag til forbedringer")]
        FEATURE_REQUEST,
        [Display(Name =" Find forhandler")]
        FIND_DEALER,
        [Display(Name ="Andmod om katalog")]
        REQUEST_CATALOG,
        [Display(Name =" Andet")]
        OTHER
    }

    public enum Status
    {
        [Display(Name =" Ny")]
        NEW,
        [Display(Name ="I gang")]
        IN_PROGRESS,
        [Display(Name ="Løst")]
        RESOLVED,
        [Display(Name ="Lukket")]
        CLOSED
    }
    }

